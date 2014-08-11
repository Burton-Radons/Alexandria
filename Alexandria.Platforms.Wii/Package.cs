using Glare.Assets;
using Glare.Framework;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace Alexandria.Platforms.Wii {
	/// <summary>
	/// A collection of objects.
	/// </summary>
	public class Package : FolderAsset {
		internal BinaryReader Reader { get; private set; }

		public ReadOnlyCodex<PackageString> Strings { get; private set; }

		internal Package(AssetLoader loader)
			: base(loader) {
			loader.MakeBigEndian();
			BinaryReader reader = Reader = loader.Reader;

			ushort versionMajor = reader.ReadUInt16();
			ushort versionMinor = reader.ReadUInt16();
			reader.RequireZeroes(4);

			int stringCount = reader.ReadInt32();
			Codex<PackageString> strings = new Codex<PackageString>(stringCount);
			for (int index = 0; index < stringCount; index++)
				strings.Add(new PackageString(this, index, reader));
			Strings = strings;

			int fileCount = reader.ReadInt32();
			for (int index = 0; index < fileCount; index++)
				new PackageFile(this, index, loader);
		}
	}

	public class PackageString {
		/// <summary>Possibly ID.</summary>
		public uint CRC { get; private set; }

		public int Index { get; private set; }

		public string Name { get; private set; }

		public Package Package { get; private set; }

		public PackageType Type { get; private set; }

		internal PackageString(Package package, int index, BinaryReader reader) {
			Index = index;
			Package = package;

			Type = (PackageType)reader.ReadUInt32();
			CRC = reader.ReadUInt32();

			int nameLength = reader.ReadInt32();
			Name = reader.ReadString(nameLength, Encoding.ASCII);
		}
	}

	public class PackageFile : DataAsset {
		/// <summary>Get the compression mode.</summary>
		public PackageCompression Compression { get; private set; }

		/// <summary>Possibly ID.</summary>
		public uint CRC { get; private set; }

		public override string DisplayName {
			get {
				return string.Format("{0} ({1:8X}h, {2} byte(s), {3} compression)", Extensions.EnumCharsToStringBE(Type), CRC, Size, Compression);
			}
		}

		/// <summary>Get the zero-based index of this <see cref="PackageFile"/> in the <see cref="Package"/>.</summary>
		public int Index { get; private set; }

		/// <summary>Get the offset of the <see cref="PackageFile"/>'s data from the start of the <see cref="Package"/>.</summary>
		public uint Offset { get; private set; }

		/// <summary>Get the containing <see cref="Package"/>.</summary>
		public new Package Parent { get { return (Package)base.Parent; } }

		/// <summary>Get the size in bytes of the file when compressed.</summary>
		public int Size { get; private set; }

		/// <summary>Get the type of the file.</summary>
		public PackageType Type { get; private set; }

		internal PackageFile(Package package, int index, AssetLoader loader)
			: base(package, "") {
			BinaryReader reader = loader.Reader;

			Index = index;

			Compression = (PackageCompression)reader.ReadInt32();
			Type = (PackageType)reader.ReadInt32();
			CRC = reader.ReadUInt32();
			Size = reader.ReadInt32();
			Offset = reader.ReadUInt32();
		}

		public override Stream Open() {
			BinaryReader reader = Parent.Reader;

			switch (Compression) {
				case PackageCompression.None:
					return new SliceStream(reader.BaseStream, Offset, Size, closeStream: false);

				case PackageCompression.Deflate:
					lock (reader.BaseStream) {
						reader.BaseStream.Position = Offset;
						uint decompressedSize = reader.ReadUInt32();
						byte[] data = new byte[decompressedSize];
						Stream compressedStream = new DeflateStream(reader.BaseStream, CompressionMode.Decompress);
						compressedStream.Read(data, 0, data.Length);
						return new MemoryStream(data, 0, data.Length);
					}

				default:
					throw new NotSupportedException("Unsupported or invalid compression mode " + Compression + ".");
			}
		}
	}

	/// <summary>Specifies the compression of a <see cref="PackageFile"/>.</summary>
	public enum PackageCompression : uint {
		/// <summary>No compression.</summary>
		None = 0,

		/// <summary><c>uint</c> uncompressed length followed by compressed data.</summary>
		Deflate = 1,
	}

	/// <summary>Type of an object in a <see cref="Package"/>.</summary>
	public enum PackageType : uint {
		Animation = ('A' << 24) + ('N' << 16) + ('I' << 8) + 'M',

		Event = ('E' << 24) + ('V' << 16) + ('N' << 8) + 'T',

		/// <summary>Font object ("FONT").</summary>
		Font = ('F' << 24) + ('O' << 16) + ('N' << 8) + 'T',

		Hint = ('H' << 24) + ('I' << 16) + ('N' << 8) + 'T',

		Part = ('P' << 24) + ('A' << 16) + ('R' << 8) + 'T',

		Path = ('P' << 24) + ('A' << 16) + ('T' << 8) + 'H',

		Scan = ('S' << 24) + ('C' << 16) + ('A' << 8) + 'N',

		/// <summary>Strings object ("STRG").</summary>
		String = ('S' << 24) + ('T' << 16) + ('R' << 8) + 'G',

		/// <summary>Texture object ("TXTR").</summary>
		Texture = ('T' << 24) + ('X' << 16) + ('T' << 8) + 'R',

		UnknownAFSM = ('A' << 24) + ('F' << 16) + ('S' << 8) + 'M',
		UnknownAGSC = ('A' << 24) + ('G' << 16) + ('S' << 8) + 'C',
		UnknownANCS = ('A' << 24) + ('N' << 16) + ('C' << 8) + 'S',
		UnknownATBL = ('A' << 24) + ('T' << 16) + ('B' << 8) + 'L',
		UnknownCINF = ('C' << 24) + ('I' << 16) + ('N' << 8) + 'F',
		UnknownCMDL = ('C' << 24) + ('M' << 16) + ('D' << 8) + 'L',
		UnknownCRSC = ('C' << 24) + ('R' << 16) + ('S' << 8) + 'C',
		UnknownCSKR = ('C' << 24) + ('S' << 16) + ('K' << 8) + 'R',
		UnknownCTWK = ('C' << 24) + ('T' << 16) + ('W' << 8) + 'K',
		UnknownDCLN = ('D' << 24) + ('C' << 16) + ('L' << 8) + 'N',
		UnknownDGRP = ('D' << 24) + ('G' << 16) + ('R' << 8) + 'P',
		UnknownDPSC = ('D' << 24) + ('P' << 16) + ('S' << 8) + 'C',
		UnknownDUMB = ('D' << 24) + ('U' << 16) + ('M' << 8) + 'B',
		UnknownELSC = ('E' << 24) + ('L' << 16) + ('S' << 8) + 'C',
		UnknownFRME = ('F' << 24) + ('R' << 16) + ('M' << 8) + 'E',
		UnknownMAPA = ('M' << 24) + ('A' << 16) + ('P' << 8) + 'A',
		UnknownMAPU = ('M' << 24) + ('A' << 16) + ('P' << 8) + 'U',
		UnknownMLVL = ('M' << 24) + ('L' << 16) + ('V' << 8) + 'L',
		UnknownMREA = ('M' << 24) + ('R' << 16) + ('E' << 8) + 'A',
		UnknownSAVW = ('S' << 24) + ('A' << 16) + ('V' << 8) + 'W',
		UnknownSWHC = ('S' << 24) + ('W' << 16) + ('H' << 8) + 'C',
		UnknownWPSC = ('W' << 24) + ('P' << 16) + ('S' << 8) + 'C',
	}

	/// <summary>Language identifier used in <see cref="Package"/>s.</summary>
	public enum PackageLanguage : uint {
		/// <summary>English language ("ENGL").</summary>
		English = 0x454E474C,

		/// <summary>French language ("FREN").</summary>
		French = 0x4652454E,

		/// <summary>German language ("GERM").</summary>
		German = 0x4745524D,

		/// <summary>Italian language ("ITAL").</summary>
		Italian = 0x4954414C,

		/// <summary>Japanese language ("JAPN").</summary>
		Japanese = 0x4A41504E,

		/// <summary>Spanish language ("SPAN").</summary>
		Spanish = 0x5350414E,
	}

	public class PackageFormat : AssetFormat {
		public PackageFormat(Platform platform)
			: base(platform, typeof(Package), canLoad: true, extension: ".pak") {
		}

		public override LoadMatchStrength LoadMatch(AssetLoader loader) {
			BinaryReader reader = loader.Reader;

			if (loader.Length < 64)
				return LoadMatchStrength.None;
			int count, stringLength;
			if (reader.ReadUInt16BE() != 3 || reader.ReadUInt16BE() != 5 || // Version number
				reader.ReadUInt32BE() != 0 || // Zero
				(count = reader.ReadInt32BE()) * 12 + 12 > loader.Length || count == 0 || // Import count
				!reader.MatchAscii(4) || // Type
				reader.ReadUInt32BE() == 0 || // CRC/Id
				(stringLength = reader.ReadInt32BE()) == 0 || stringLength + 20 >= loader.Length || // Import name length
				!reader.MatchAscii(stringLength)) // Import name
				return LoadMatchStrength.None;
			return LoadMatchStrength.Medium;
		}

		public override Asset Load(AssetLoader loader) {
			return new Package(loader);
		}
	}
}
