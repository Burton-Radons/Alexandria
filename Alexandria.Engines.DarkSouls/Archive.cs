using Glare.Assets;
using Glare.Framework;
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
	/// <summary>
	/// Identifies the major <see cref="Archive"/> variant.
	/// </summary>
	public enum ArchiveVariant {
		/// <summary>No or invalid value.</summary>
		None,

		/// <summary>V3, used in Dark Souls.</summary>
		V3,

		/// <summary>Used in Dark Souls and Dark Souls 2.</summary>
		V4,

		/// <summary>Used in Dark Souls 2.</summary>
		V5,
	}

	/// <summary>
	/// A collection of files.
	/// </summary>
	public class Archive : ArchiveAsset {
		/// <summary>Get the extension of a normal contents file.</summary>
		public const string ContentsExtensionEnd = "bdt";

		/// <summary>Get the extension of a normal headers file.</summary>
		public const string HeadersExtensionEnd = "bhd", HeadersExtension5End = "bhd5";

		/// <summary>"BDF3" content magic; a V5 file.</summary>
		public const string ContentsMagicBDF3 = "BDF3";

		/// <summary>"BDF4" content magic.</summary>
		public const string ContentsMagicBDF4 = "BDF4";

		/// <summary>"BHF3" headers magic.</summary>
		public const string HeadersMagicBHF3 = "BHF3";

		/// <summary>"BHF4" headers magic.</summary>
		public const string HeadersMagicBHF4 = "BHF4";

		/// <summary>"BHD5" headers magic.</summary>
		public const string HeadersMagicBHD5 = "BHD5";

		/// <summary>Get the extension for a package file.</summary>
		public const string PackageExtensionBND = ".bnd";

		/// <summary>"BND3" package magic.</summary>
		public const string PackageMagicBND3 = "BND3";

		/// <summary>"BND4" package magic.</summary>
		public const string PackageMagicBND4 = "BND4";

		internal readonly Codex<ArchiveRecord> records = new Codex<ArchiveRecord>();

		readonly Dictionary<string, ArchiveRecord> RecordsByPath = new Dictionary<string, ArchiveRecord>();
		readonly Dictionary<int, ArchiveRecord> RecordsById = new Dictionary<int, ArchiveRecord>();

		/// <summary>Get the text encoding to use.</summary>
		public Encoding Encoding { get { return IsBigEndian ? Encoding.BigEndianUnicode : Encoding.Unicode; } }

		/// <summary>Get the "Shift-JIS" encoding which is used throughout Dark/Demon Souls.</summary>
		public static readonly Encoding ShiftJis = Encoding.GetEncoding("shift-jis");

		internal FileManager FileManager { get; private set; }

		/// <summary>Null or a string up to eight characters of the form "307D7R6", "14B27P30" or "14B24G16", depending upon archive type; maybe revision-control related?</summary>
		public string Id { get; private set; }

		/// <summary>Get whether this is a big-endian archive.</summary>
		public bool IsBigEndian { get; private set; }

		/// <summary>Get whether this is a Dark Souls 2 archive.</summary>
		public bool IsDarkSoulsII { get; private set; }

		/// <summary>Get whether this is a package archive, as opposed to a header/content pair of files.</summary>
		public bool IsHeaderlessPackage { get; private set; }

		/// <summary>Get the reader for the archive.</summary>
		public readonly BinaryReader DataReader;

		/// <summary>Get the stream for the archive.</summary>
		public readonly Stream DataStream;

		internal static readonly Dictionary<int, string> KnownFiles = new Dictionary<int, string>();

		/// <summary>Get the records in the archive.</summary>
		public ReadOnlyCodex<ArchiveRecord> Records { get { return records; } }

		/// <summary>Get the archive variant.</summary>
		public ArchiveVariant Variant { get; private set; }

		static Archive() {
			KnownFiles = new Dictionary<int, string>();
			foreach (string content in new string[] { Properties.Resources.FileList1, Properties.Resources.FileList2 }) {
				string[] lines = content.Split(new string[] { "\x0D\x0A" }, StringSplitOptions.None);
				for (int index = 1; index < lines.Length - 1; index++)
					KnownFiles[HashFilename(lines[index])] = lines[index];
			}
		}

		static Stream OpenOtherFile(string path, FileManager manager, string suffix, string suffix2 = null) {
			return (suffix2 != null ? manager.TryOpenRead(path + suffix2) : null) ?? manager.OpenRead(path + suffix);
		}

		static string RemoveSuffix(string path, string suffix, string suffix2 = null) {
			if (path.EndsWith(suffix))
				return path.Substring(0, path.Length - suffix.Length);
			if (suffix2 != null && path.EndsWith(suffix2))
				return path.Substring(0, path.Length - suffix2.Length);
			return path;
		}

		internal Archive(AssetManager manager, AssetLoader loader)
			: base(manager, "Dark Souls archive - " + loader.Name) {
			FileManager = new ArchiveFileManager(this);
			var reader = loader.Reader;
			var path = loader.Name;
			var context = loader.FileManager;

			string magic = reader.ReadString(4, Encoding.ASCII);
			string extension = System.IO.Path.GetExtension(path);

			// Switch to the headers file if we got a contents file.
			if (magic == ContentsMagicBDF3 || magic == ContentsMagicBDF4) {
				string headersPathBase = RemoveSuffix(path, ContentsExtensionEnd);
				Stream headersStream = OpenOtherFile(headersPathBase, context, HeadersExtensionEnd, HeadersExtension5End);

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
					DataStream = OpenOtherFile(contentsPathBase, context, ContentsExtensionEnd);

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
						int binCount = reader.ReadInt32();
						reader.Require(0x18); // Groups offset

						int[] binInfo = reader.ReadArrayInt32(binCount * 2);
						for (int index = 0, totalRecordIndex = 0; index < binCount; index++) {
							reader.BaseStream.Position = binInfo[index * 2 + 1];
							int count = binInfo[index * 2 + 0];

							// Each record in a group belongs to the same "bin"; 
							for (int recordIndex = 0; recordIndex < count; recordIndex++)
								new ArchiveRecord(this, totalRecordIndex, reader, -1);
						}

						break;

					default:
						throw new NotImplementedException(Variant + " is not implemented, durr");
				}
			} finally {
				if (!IsHeaderlessPackage)
					reader.Dispose();
			}

			SortChildrenRecursively();
			foreach (var record in Records) {
				RecordsById[record.Id] = record;
				RecordsByPath[record.PathName] = record;
			}
		}

		/// <summary>
		/// Find an <see cref="ArchiveRecord"/> by its id, or return <c>null</c> if not found.
		/// </summary>
		/// <param name="id">The id of the <see cref="ArchiveRecord"/> to search for.</param>
		/// <returns>The <see cref="ArchiveRecord"/> or <c>null</c> for none.</returns>
		public ArchiveRecord FindRecordById(int id) { return RecordsById.TryGetValue(id); }

		/// <summary>Find an <see cref="ArchiveRecord"/> by its path, or return <c>null</c> if not found.</summary>
		/// <param name="path">The path of the <see cref="ArchiveRecord"/> to search for.</param>
		/// <returns>The <see cref="ArchiveRecord"/> or <c>null</c> if not found.</returns>
		public ArchiveRecord FindRecordByPath(string path) { return RecordsByPath.TryGetValue(path); }

		/// <summary>Produce a hash of the filename used in Dark Souls main archives' record ids.</summary>
		/// <param name="input">The string to hash.</param>
		/// <returns>The hash value.</returns>
		public static int HashFilename(string input) {
			if (string.IsNullOrEmpty(input))
				return 0;
			int hash = 0;
			foreach (char ch in input)
				hash = hash * 0x25 + char.ToLowerInvariant(ch);
			return hash;
		}

		class ArchiveFileManager : FileManager {
			readonly Archive Archive;

			/// <summary>Get an id based on the archive name and the contexts.</summary>
			public override string Id {
				get {
					string id = Archive.Name;
					for (Asset context = Archive.LoadContext; context != null; context = context.LoadContext)
						id += "\n" + context.Name;
					return id;
				}
			}

			public ArchiveFileManager(Archive archive) {
				Archive = archive;
			}

			public override bool Exists(string path) {
				foreach (ArchiveRecord record in Archive.Records)
					if (record.PathName == path)
						return true;
				return false;
			}

			public override Stream Open(string path, FileMode mode, FileAccess access, FileShare share) {
				foreach (ArchiveRecord record in Archive.Records)
					if (record.PathName == path)
						return record.Open();
				throw new IOException("Archive doesn't contain file '" + path + "'.");
			}
		}
	}

	/// <summary>
	/// A record in an <see cref="Archive"/>.
	/// </summary>
	public class ArchiveRecord : DataAsset {
		int Compression;
		long fixedUncompressedSize;

		/// <summary>Get the archive this is in.</summary>
		public Archive Archive {
			get {
				Archive archive;
				for (Asset parent = Parent; parent != null; parent = parent.Parent)
					if ((archive = parent as Archive) != null)
						return archive;
				throw new InvalidOperationException("Archive record is not part of an Archive???");
			}
		}

		/// <summary>
		/// Get the <see cref="Asset.LoadContext"/> of the <see cref="Archive"/>.
		/// </summary>
		public Asset ArchiveContext { get { return Archive.LoadContext; } }

		/// <summary>Get a display name that includes the <see cref="Id"/> and <see cref="Size"/>.</summary>
		public override string DisplayName {
			get {
				string text = Name;
				text += string.Format(" ({0}size {1})", Id != 0 ? string.Format("id {0:X}h, ", Id) : "", Size);
				return text;
			}
		}

		/// <summary>
		/// Get the <see cref="FileManager"/> of the <see cref="Archive"/>.
		/// </summary>
		public override FileManager FileManager { get { return Archive.FileManager; } }

		/// <summary>
		/// Get the id of the record.
		/// </summary>
		public int Id { get; private set; }

		/// <summary>
		/// Get the zero-based index of the record in the <see cref="Archive"/>.
		/// </summary>
		public int Index { get; private set; }

		/// <summary>
		/// Get the offset of the record in the archive content data.
		/// </summary>
		public long Offset { get; private set; }

		/// <summary>Get the <see cref="Asset.Name"/> of the <see cref="Asset"/> along with its path.</summary>
		public override string PathName {
			get {
				string text = "";
				for (Asset resource = this; !(resource is Archive); resource = resource.Parent) {
					if (text.Length > 0)
						text = "/" + text;
					text = resource.Name + text;
				}
				return text;
			}
		}

		/// <summary>
		/// Get the size in bytes of the record data. This may not be the raw size, as the record could be compressed.
		/// </summary>
		public long Size { get; private set; }

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
					Id = DarkSouls.Archive.HashFilename(Name);
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

		/// <summary>
		/// Open the record, decompressing it as necessary.
		/// </summary>
		/// <returns>A <see cref="Stream"/> for reading the record.</returns>
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

		/*public override BinaryReader OpenReader() {
			return BigEndianBinaryReader.Create(Archive.IsBigEndian ? ByteOrder.BigEndian : ByteOrder.LittleEndian, Open());
		}*/

		/// <summary>
		/// Get a string representing the record.
		/// </summary>
		/// <returns>A string representing the record.</returns>
		public override string ToString() {
			return string.Format("{0}(Id 0x{4:X}, \"{1}\", Offset {2:X}h, Size {3})", GetType().Name, PathName, Offset, Size, Id);
		}

		static string ToString(byte value) {
			if (value > 32 && value < 128 && value != '\\')
				return ((char)value).ToString();
			return string.Format("\\x{0:2X}", value);
		}
	}

	/// <summary>
	/// The <see cref="AssetFormat"/> for handling an <see cref="Archive"/>.
	/// </summary>
	public class ArchiveFormat : AssetFormat {
		/// <summary>
		/// Initialise the archive format.
		/// </summary>
		/// <param name="engine">The engine to associate with.</param>
		public ArchiveFormat(Engine engine)
			: base(engine, typeof(Archive), canLoad: true) {
		}

		static byte[] LoadKey(string filename, string type) {
			var data = File.ReadAllBytes(filename);
			string pem = Encoding.ASCII.GetString(data);
			string header = String.Format("-----BEGIN {0}-----", type);
			string footer = String.Format("-----END {0}-----", type);
			int start = pem.IndexOf(header) + header.Length;
			int end = pem.IndexOf(footer, start);
			string base64 = pem.Substring(start, (end - start));
			return Convert.FromBase64String(base64);
		}

		/// <summary>
		/// Get whether the loader matches an archive.
		/// </summary>
		/// <param name="loader">The loader to test.</param>
		/// <returns>The match strength.</returns>
		public override LoadMatchStrength LoadMatch(AssetLoader loader) {
			var reader = loader.Reader;

			/*const string keyFile = @"D:\Steam\steamapps\common\Dark Souls II\Game\GameDataKeyCode.pem";
			var keyData = LoadKey(keyFile, "RSA PUBLIC KEY");
			//byte[] result = rsa.Decrypt(reader.ReadBytes(100));
			var bio = new BIO(keyData);
			

			var rsa = new RSA();*/

			string magic = reader.ReadString(4, Encoding.ASCII);
			if (magic == Archive.ContentsMagicBDF3 || magic == Archive.HeadersMagicBHD5 || magic == Archive.ContentsMagicBDF4 || magic == Archive.HeadersMagicBHF4 || magic == Archive.PackageMagicBND4 || magic == Archive.PackageMagicBND3 || magic == Archive.HeadersMagicBHF3)
				return LoadMatchStrength.Strong;
			return LoadMatchStrength.None;
		}

		/// <summary>
		/// Read the archive from the loader.
		/// </summary>
		/// <param name="loader">The loader to read from.</param>
		/// <returns>The new archive.</returns>
		public override Asset Load(AssetLoader loader) {
			return new Archive(Manager, loader);
		}
	}
}
