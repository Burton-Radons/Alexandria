using Alexandria.Resources;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.DarkSouls {
	public enum ArchiveVariant {
		None,
		V3,
		V4,
		V5,
	}

	public class Archive : Resources.Archive {
		public const string ContentsExtensionEnd = "bdt";
		public const string HeadersExtensionEnd = "bhd", HeadersExtension5End = "bhd5";

		public const string ContentsMagicBDF3 = "BDF3";
		public const string ContentsMagicBDF4 = "BDF4";

		public const string HeadersMagicBHF3 = "BHF3";
		public const string HeadersMagicBHF4 = "BHF4";
		public const string HeadersMagicBHD5 = "BHD5";

		public const string PackageExtensionBND = ".bnd";
		public const string PackageMagicBND3 = "BND3";
		public const string PackageMagicBND4 = "BND4";

		internal readonly ArrayBackedList<ArchiveRecord> records = new ArrayBackedList<ArchiveRecord>();

		public Encoding Encoding { get { return IsBigEndian ? Encoding.BigEndianUnicode : Encoding.Unicode; } }
		public static readonly Encoding ShiftJis = Encoding.GetEncoding("shift-jis");

		public ArchiveGroupCollection Groups { get; private set; }

		/// <summary>Null or a string up to eight characters of the form "307D7R6", "14B27P30" or "14B24G16", depending upon archive type; maybe revision-control related?</summary>
		public string Id { get; private set; }

		public bool IsBigEndian { get; private set; }

		public bool IsDarkSoulsII { get; private set; }

		public bool IsHeaderlessPackage { get; private set; }

		public readonly BinaryReader DataReader;

		public readonly Stream DataStream;

		internal static readonly Dictionary<int, string> KnownFiles = new Dictionary<int, string>();

		public ReadOnlyList<ArchiveRecord> Records { get { return records; } }

		public ArchiveVariant Variant { get; private set; }

		static Archive() {
			KnownFiles = new Dictionary<int, string>();
			foreach (string content in new string[] { Properties.Resources.FileList1, Properties.Resources.FileList2 }) {
				string[] lines = content.Split(new string[] { "\x0D\x0A" }, StringSplitOptions.None);
				for (int index = 1; index < lines.Length - 1; index++)
					KnownFiles[HashFilename(lines[index])] = lines[index];
			}
		}

		static Stream OpenOtherFile(string path, LoaderFileOpener opener, string suffix, string suffix2 = null) {
			if (suffix2 != null) {
				try {
					return opener(path + suffix2, FileMode.Open, FileAccess.Read, FileShare.Read);
				} catch (FileNotFoundException) {
				}
			}

			return opener(path + suffix, FileMode.Open, FileAccess.Read, FileShare.Read);
		}

		static string RemoveSuffix(string path, string suffix, string suffix2 = null) {
			if (path.EndsWith(suffix))
				return path.Substring(0, path.Length - suffix.Length);
			if (suffix2 != null && path.EndsWith(suffix2))
				return path.Substring(0, path.Length - suffix2.Length);
			return path;
		}

		public Archive(Manager manager, BinaryReader reader, string path, LoaderFileOpener opener)
			: base(manager, "Dark Souls archive - " + path) {
			Groups = new ArchiveGroupCollection();

			string magic = reader.ReadString(4, Encoding.ASCII);
			string extension = System.IO.Path.GetExtension(path);

			// Switch to the headers file if we got a contents file.
			if (magic == ContentsMagicBDF3 || magic == ContentsMagicBDF4) {
				string headersPathBase = RemoveSuffix(path, ContentsExtensionEnd);
				Stream headersStream = OpenOtherFile(headersPathBase, opener, HeadersExtensionEnd, HeadersExtension5End);

				DataStream = reader.BaseStream;
				DataReader = reader;
				reader = BigEndianBinaryReader.Create(IsBigEndian ? ByteOrder.BigEndian : ByteOrder.LittleEndian, headersStream);
				magic = reader.ReadString(4, Encoding.ASCII);
			}

			if (magic == PackageMagicBND4) {
				IsBigEndian = true;
				IsDarkSoulsII = true;
			}

			// Get the variant.
			switch (magic) {
				case ContentsMagicBDF3:
				case HeadersMagicBHD5:
					Variant = ArchiveVariant.V5;
					break;

				case HeadersMagicBHF3:
				case PackageMagicBND3:
					Variant = ArchiveVariant.V3;
					break;

				case ContentsMagicBDF4:
				case HeadersMagicBHF4:
				case PackageMagicBND4:
					Variant = ArchiveVariant.V4;
					break;

				default:
					throw new NotImplementedException();
			}

			if (DataReader == null) {
				if (magic == PackageMagicBND4 || magic == PackageMagicBND3) {
					IsHeaderlessPackage = true;
					DataStream = reader.BaseStream;
					DataReader = reader;
					if (IsBigEndian)
						reader = new BigEndianBinaryReader(reader.BaseStream);
				} else if (magic == HeadersMagicBHD5 || magic == HeadersMagicBHF4 || magic == HeadersMagicBHF3) {
					string contentsPathBase = RemoveSuffix(path, HeadersExtensionEnd, HeadersExtension5End);
					DataStream = OpenOtherFile(contentsPathBase, opener, ContentsExtensionEnd);

					/*string contentsExtension =
						extension == HeadersExtensionTPFBHD ? ContentsExtensionTPFBDT :
						extension == HeadersExtensionHKXBHD ? ContentsExtensionHKXBDT :
						ContentsExtensionBDT;

					DataStream = opener(System.IO.Path.ChangeExtension(path, contentsExtension), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);*/
					DataReader = new BinaryReader(DataStream);
					if (IsBigEndian)
						reader = new BigEndianBinaryReader(reader.BaseStream);
				} else
					throw new NotImplementedException();
			}

			try {
				switch (Variant) {
					case ArchiveVariant.V3: {
							Id = reader.ReadStringz(8, Encoding.ASCII);

							int version = reader.ReadInt32();
							if (version != 0x74 && version != 0x54)
								throw new InvalidDataException();

							int recordCount = reader.ReadInt32();
							int totalHeaderSize = reader.ReadInt32(); // Either zero or the unaligned end of the last record's name before the first record's data.
							reader.RequireZeroes(8);

							for (int index = 0; index < recordCount; index++)
								new ArchiveRecord(this, index, reader, -1);
							break;
						}

					case ArchiveVariant.V4: {
							IsDarkSoulsII = true;

							int byteOrder = reader.ReadInt32();
							if (byteOrder == 257) {
								IsBigEndian = true;
								reader = new BigEndianBinaryReader(reader.BaseStream);
							} else if (byteOrder != 0x01010000)
								throw new Exception();

							reader.Require(0x10000);
							int recordCount = reader.ReadInt32();
							long recordOffset = reader.ReadInt64();
							if (recordOffset != 0x40)
								throw new Exception();
							Id = reader.ReadString(8, Encoding.ASCII);
							int recordHeaderSize = checked((int)reader.ReadInt64());
							if (recordHeaderSize != 0x1C && recordHeaderSize != 0x24)
								throw new Exception();
							long u2 = reader.ReadInt64(); // 0 or end of the name strings
							int u1 = reader.ReadInt16(); // 0x1C or 0x12E?
							reader.RequireZeroes(14);

							for (int index = 0; index < recordCount; index++)
								new ArchiveRecord(this, index, reader, recordHeaderSize);
							break;
						}

					case ArchiveVariant.V5:
						reader.Require(0xFF);
						reader.Require(1);
						uint totalFileSize = reader.ReadUInt32();
						int groupCount = reader.ReadInt32();
						int groupOffsets = reader.ReadInt32();

						reader.BaseStream.Position = groupOffsets;
						int[] groupInfo = new int[groupCount * 2];
						for (int index = 0; index < groupCount * 2; index++)
							groupInfo[index] = reader.ReadInt32();

						for (int index = 0; index < groupCount; index++) {
							reader.BaseStream.Position = groupInfo[index * 2 + 1];
							Groups.Add(new ArchiveGroup(this, reader, index, groupInfo[index * 2 + 0]));
						}
						break;

					default:
						throw new NotImplementedException(Variant + " is not implemented, durr");
				}
#if false
				if (IsDarkSoulsII) {
					if(!IsHeaderlessPackage)
						reader.RequireMagic(HeadersMagicBHF4);
				} else if(IsHeaderlessPackage) {
				} else {
				}
#endif
			} finally {
				if (!IsHeaderlessPackage)
					reader.Dispose();
			}

			SortChildrenRecursively();
		}

		public static int HashFilename(string input) {
			if (string.IsNullOrEmpty(input))
				return 0;
			int hash = 0;
			foreach (char ch in input)
				hash = hash * 0x25 + char.ToLowerInvariant(ch);
			return hash;
		}

	}

	public class ArchiveGroup {
		public Archive Archive { get; private set; }

		public int Index { get; private set; }

		public List<ArchiveRecord> Records { get; private set; }

		internal ArchiveGroup(Archive archive, BinaryReader reader, int index, int count) {
			Records = new List<ArchiveRecord>();
			Archive = archive;
			Index = index;
			for (int recordIndex = 0; recordIndex < count; recordIndex++)
				Records.Add(new ArchiveRecord(this, recordIndex, reader));
		}
	}

	public class ArchiveRecord : Asset {
		int Compression;
		long fixedUncompressedSize;

		public override LoaderFileOpener FileOpener { get { return MyFileOpener; } }

		Stream MyFileOpener(string name, FileMode mode, FileAccess access, FileShare share) {
			foreach (ArchiveRecord record in Archive.Records) {
				if (record.PathName == name)
					return record.Open();
			}
			throw new Exception("Archive doesn't contain file '" + name + "'.");
		}

		public Archive Archive {
			get {
				Archive archive;
				for (Resource parent = Parent; parent != null; parent = parent.Parent)
					if ((archive = parent as Archive) != null)
						return archive;
				throw new InvalidOperationException("Archive record is not part of an Archive???");
			}
		}

		public override string DisplayName {
			get {
				string text = Name;
				text += string.Format(" ({0}size {1})", Id != 0 ? string.Format("id {0:X}h, ", Id) : "", Size);
				return text;
			}
		}

		public ArchiveGroup Group { get; private set; }

		public int Id { get; private set; }

		public int Index { get; private set; }

		public long Offset { get; private set; }

		public override string PathName {
			get {
				string text = "";
				for (Resource resource = this; !(resource is Archive); resource = resource.Parent) {
					if (text.Length > 0)
						text = "/" + text;
					text = resource.Name + text;
				}
				return text;
			}
		}

		public long Size { get; private set; }

		internal ArchiveRecord(ArchiveGroup group, int index, BinaryReader reader)
			: this(group.Archive, index, reader, -1) {
			Group = group;
		}

		internal ArchiveRecord(Archive archive, int index, BinaryReader reader, int recordHeaderSize)
			: base(archive, "") {
			archive.records.Add(this);
			Index = index;

			switch (archive.Variant) {
				case ArchiveVariant.V3:
					reader.Require(0x40);
					Size = reader.ReadInt32();
					Offset = reader.ReadInt32();
					Id = reader.ReadInt32();
					Name = reader.ReadStringzAtUInt32(Archive.ShiftJis);
					int size2 = reader.ReadInt32();
					if (Size != size2)
						throw new Exception();
					fixedUncompressedSize = Size;
					break;

				case ArchiveVariant.V4:
					Compression = reader.ReadInt32(); // 2 or 3 - compression?
					reader.ReadMatch(-1);
					Size = reader.ReadInt64();
					if (recordHeaderSize == 0x1C) {
						Offset = reader.ReadInt64();
						Name = reader.ReadStringzAtUInt32(Archive.ShiftJis);
					} else {
						fixedUncompressedSize = reader.ReadInt64();
						Offset = reader.ReadUInt32();
						Unknowns.ReadInt32s(reader, 1);
						// 'Compression' = 2, Unknown = 0, Encoding = UTF8
						Name = reader.ReadStringzAtUInt32(Archive.ShiftJis);
					}
					break;

				case ArchiveVariant.V5:
					Id = reader.ReadInt32();
					Size = reader.ReadInt32();
					Offset = reader.ReadInt32();
					reader.RequireZeroes(4); // NameOffset, but zero?

					string foundName;
					if (!Archive.KnownFiles.TryGetValue(Id, out foundName))
						Name = string.Format("#{0:X}", Id);
					else
						Name = foundName;
					break;

				default:
					throw new NotImplementedException();
			}

			MoveIntoPath(Name);
		}

		public override Stream Open() {
			var archive = Archive;
			var reader = archive.DataReader;
			reader.BaseStream.Position = Offset;

			if (fixedUncompressedSize != Size && fixedUncompressedSize != 0) {
				if (Size == fixedUncompressedSize)
					return new MemoryStream(reader.ReadBytes(checked((int)fixedUncompressedSize)), false);
				else {
					byte[] data = new byte[fixedUncompressedSize];

					var deflate = new ICSharpCode.SharpZipLib.Zip.Compression.Streams.InflaterInputStream(reader.BaseStream);
					var read = deflate.Read(data, 0, data.Length);
					if (read != fixedUncompressedSize)
						throw new InvalidDataException();
					return new MemoryStream(data, false);
				}
			}

			var magic = reader.ReadString(4, Encoding.ASCII);

			if (magic == "DCX\0") {
				reader.Require(0x100);
				reader.RequireBE(0x18);
				reader.RequireBE(0x24);
				reader.RequireBE(0x24);
				int totalHeaderLength = reader.ReadInt32BE();
				reader.RequireMagic("DCS\0");
				int uncompressedSize = reader.ReadInt32BE();
				int compressedSize = reader.ReadInt32BE();
				reader.RequireMagic("DCP\0");

				string compressionMethod = reader.ReadStringz(4, Encoding.ASCII);

				reader.RequireBE(0x20);
				reader.Require(9);
				reader.Require(archive.IsDarkSoulsII ? 0x100 : 0);
				reader.RequireZeroes(8);
				reader.Require(archive.IsDarkSoulsII ? 0x11000 : 0x10100);
				reader.RequireMagic("DCA\0");
				int compressionHeaderLength = reader.ReadInt32BE();

				byte[] data = new byte[uncompressedSize];
				switch (compressionMethod) {
					case "DFLT":
						if (true) {
							var deflate = new ICSharpCode.SharpZipLib.Zip.Compression.Streams.InflaterInputStream(reader.BaseStream);

							var read = deflate.Read(data, 0, data.Length);
							if (read != uncompressedSize)
								throw new Exception();
							return new MemoryStream(data, false);
						}

					case "EDGE":
						if (true) {
							reader.RequireMagic("EgdT");
							reader.RequireBE(0x10100);
							reader.RequireBE(0x24); // sectionHeaderStart
							int alignment = reader.ReadInt32BE();
							if (alignment != 0x10)
								throw new InvalidDataException();
							int normalBlockSize = reader.ReadInt32BE();
							int lastBlockSize = reader.ReadInt32BE();
							int headerLength = reader.ReadInt32BE();
							int sectionCount = reader.ReadInt32BE();
							int sectionEntrySize = reader.ReadInt16BE();
							if (headerLength != 36 + sectionCount * sectionEntrySize)
								throw new InvalidDataException();
							reader.RequireZeroes(2);
							long blockHeaderStart = reader.BaseStream.Position;
							long blockDataStart = blockHeaderStart + sectionCount * sectionEntrySize;
							int offset = 0;

							for (int index = 0; index < sectionCount; index++) {
								reader.BaseStream.Position = blockHeaderStart + index * sectionEntrySize;
								long blockOffset = reader.ReadInt64BE();
								int blockSize = reader.ReadInt32BE();
								int blockMode = reader.ReadInt32BE();
								int uncompressedBlockSize = index < sectionCount - 1 ? normalBlockSize : lastBlockSize;

								reader.BaseStream.Position = blockDataStart + blockOffset;

								switch (blockMode) {
									case 0:
										if (blockSize < uncompressedBlockSize)
											throw new InvalidDataException();
										reader.Read(data, offset, uncompressedBlockSize);
										break;

									case 1:
										var deflate = new ICSharpCode.SharpZipLib.Zip.Compression.Streams.InflaterInputStream(reader.BaseStream, new ICSharpCode.SharpZipLib.Zip.Compression.Inflater(true));
										int deflateRead = deflate.Read(data, offset, uncompressedBlockSize);
										if (deflateRead != uncompressedBlockSize)
											throw new InvalidDataException();
										break;

									default:
										throw new NotSupportedException("Unsupported block mode " + blockMode);
								}

								offset += uncompressedBlockSize;
							}

							if (offset != data.Length)
								throw new InvalidDataException();
							return new MemoryStream(data, false);
						}

					default:
						throw new NotImplementedException(compressionMethod + " compression is not implemented.");
				}
			} else {
				reader.BaseStream.Seek(-4, SeekOrigin.Current);
				byte[] data = reader.ReadBytes(checked((int)Size));
				return new MemoryStream(data, false);
			}
		}

		public override BinaryReader OpenReader() {
			return BigEndianBinaryReader.Create(Archive.IsBigEndian ? ByteOrder.BigEndian : ByteOrder.LittleEndian, Open());
		}

		public override string ToString() {
			return string.Format("{0}({1}, Offset {2:X}h, Size {3})", GetType().Name, Name, Offset, Size);
		}

		static string ToString(byte value) {
			if (value > 32 && value < 128 && value != '\\')
				return ((char)value).ToString();
			return string.Format("\\x{0:2X}", value);
		}
	}

	public class ArchiveGroupCollection : List<ArchiveGroup> { }

	public class ArchiveFormat : ResourceFormat {
		public ArchiveFormat(Engine engine)
			: base(engine, typeof(Archive), canLoad: true) {
		}

		public override LoadMatchStrength LoadMatch(LoadInfo context) {
			var reader = context.Reader;
			string magic = reader.ReadString(4, Encoding.ASCII);
			if (magic == Archive.ContentsMagicBDF3 || magic == Archive.HeadersMagicBHD5 || magic == Archive.ContentsMagicBDF4 || magic == Archive.HeadersMagicBHF4 || magic == Archive.PackageMagicBND4 || magic == Archive.PackageMagicBND3 || magic == Archive.HeadersMagicBHF3)
				return LoadMatchStrength.Strong;
			return LoadMatchStrength.None;
		}

		public override Resource Load(LoadInfo context) {
			return new Archive(Manager, context.Reader, context.Name, context.Opener);
		}
	}
}
