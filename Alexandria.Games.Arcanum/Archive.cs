using Glare.Assets;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Games.Arcanum {
	/// <summary>
	/// An archive file.
	/// </summary>
	public class Archive : ArchiveAsset {
		internal const int Magic = 0x44415431;

		internal readonly BinaryReader Reader;

		internal Archive(AssetLoader loader)
			: base(loader) {
			BinaryReader reader = Reader = loader.Reader;

			loader.Position = loader.Length - 12;
			reader.Require(Magic);

			int namesSize = reader.ReadInt32(); // Number of bytes of the headerSize that are record names, sometimes (always?) plus a few bytes.
			int headersSize = reader.ReadInt32();
			reader.BaseStream.Position = loader.Length - headersSize;

			int count = reader.ReadInt32();
			for (int index = 0; index < count; index++) {
				int nameLength = reader.ReadInt32();
				string name = reader.ReadString(nameLength - 1, Encoding.ASCII);
				reader.Require((byte)0);
				int id = reader.ReadInt32();
				ArchiveRecordMode mode = (ArchiveRecordMode)reader.ReadInt32();
				int size = reader.ReadInt32();
				int sizeCompressed = reader.ReadInt32();
				uint offset = reader.ReadUInt32();

				if (mode != ArchiveRecordMode.Directory)
					new ArchiveRecord(this, name, id, mode, size, sizeCompressed, offset);
			}
		}
	}

	public enum ArchiveRecordMode {
		Uncompressed = 1,
		Deflate = 2,
		Directory = 1024,
	}

	public class ArchiveRecord : DataAsset {
		readonly uint Offset;
		readonly int SizeCompressed;
		ArchiveRecordMode Mode;

		public Archive Archive { get; private set; }

		public int Id { get; private set; }

		public int Size { get; private set; }

		internal ArchiveRecord(Archive archive, string name, int id, ArchiveRecordMode mode, int size, int sizeCompressed, uint offset)
			: base(archive, name) {
			Archive = archive;
			MoveIntoPath(name);
			Id = id;
			Mode = mode;
			Size = size;
			SizeCompressed = sizeCompressed;
			Offset = offset;
		}

		public override Stream Open() {
			lock (Archive) {
				BinaryReader reader = Archive.Reader;
				reader.BaseStream.Position = Offset;

				byte[] data = new byte[Size];

				switch (Mode) {
					case ArchiveRecordMode.Deflate:
						var inflater = new ICSharpCode.SharpZipLib.Zip.Compression.Inflater();
						inflater.SetInput(reader.ReadBytes(SizeCompressed), 0, SizeCompressed);
						if (inflater.Inflate(data, 0, Size) != Size)
							throw new InvalidDataException();
						break;

					case ArchiveRecordMode.Uncompressed:
						if (reader.Read(data, 0, Size) != Size)
							throw new InvalidDataException();
						break;

					default:
						throw new NotSupportedException();
				}

				return new MemoryStream(data, false);
			}
		}
	}

	/// <summary>Format for the <see cref="Archive"/> type.</summary>
	public class ArchiveFormat : AssetFormat {
		internal ArchiveFormat(Game game) : base(game, typeof(Archive), canLoad: true, extension: ".dat") { }

		public override LoadMatchStrength LoadMatch(AssetLoader loader) {
			BinaryReader reader = loader.Reader;

			if (loader.Length < 12)
				return LoadMatchStrength.None;

			loader.Position = loader.Length - 12;
			int magic = reader.ReadInt32();
			if (magic != Archive.Magic)
				return LoadMatchStrength.None;

			int namesSize = reader.ReadInt32();
			int headerSize = reader.ReadInt32();
			if (loader.Length < headerSize + 4)
				return LoadMatchStrength.None;

			loader.Position = loader.Length - 4 - headerSize;
			if (reader.ReadInt32() != loader.Length - headerSize)
				return LoadMatchStrength.None;

			return LoadMatchStrength.Strong;
		}

		public override Asset Load(AssetLoader loader) {
			return new Archive(loader);
		}
	}
}
