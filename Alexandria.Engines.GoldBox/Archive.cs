using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glare;
using Alexandria.Engines.GoldBox.Resources;
using System.Collections.ObjectModel;
using Glare.Assets;

namespace Alexandria.Engines.GoldBox {
	public class Archive : FolderAsset {
		internal BinaryReader Reader { get; private set; }

		public ReadOnlyDictionary<int, ArchiveRecord> RecordsById { get; private set; }

		public Archive(AssetManager manager, BinaryReader reader, string name, FileManager fileManager)
			: base(manager, name) {
			Reader = reader;

			int headerSize = reader.ReadUInt16();
			int count = headerSize / ArchiveRecord.HeaderSize;
			Dictionary<int, ArchiveRecord> recordsById = new Dictionary<int, ArchiveRecord>();

			for (int index = 0; index < count; index++) {
				var record = new ArchiveRecord(this, reader, index, headerSize + 2);
				recordsById[record.Id] = record;
			}

			RecordsById = new ReadOnlyDictionary<int, ArchiveRecord>(recordsById);
		}
	}

	public class ArchiveRecord : DataAsset {
		/// <summary>Size of a record entry in bytes.</summary>
		public const int HeaderSize = 9;

		public Archive Archive { get { return (Archive)Parent; } }

		public int CompressedSize { get; private set; }

		public byte Id { get; private set; }

		public int Offset { get; private set; }

		public int UncompressedSize { get; private set; }

		public ArchiveRecord(Archive archive, BinaryReader reader, int index, int dataOffset)
			: base(archive, "") {
			Id = reader.ReadByte();
			Offset = reader.ReadInt32() + dataOffset;
			UncompressedSize = reader.ReadUInt16();
			CompressedSize = reader.ReadUInt16();
			Name = Id + " (index " + index + ", size " + UncompressedSize + ")";
		}

		public override Stream Open() {
			Archive.Reader.BaseStream.Position = Offset;
			byte[] inputData = Archive.Reader.ReadBytes(CompressedSize);
			byte[] outputData = new byte[UncompressedSize];
			int output = 0, input = 0;
			int outputLength = outputData.Length, inputLength = inputData.Length;

			while (input < inputData.Length) {
				int count = (sbyte)inputData[input++];

				if (count < 0) {
					count = -count;
					byte value = inputData[input++];
					for (int index = 0; index < count; index++)
						outputData[output++] = value;
				} else {
					count++;
					for (int index = 0; index < count; index++)
						outputData[output++] = inputData[input++];
				}
			}

			if (output != outputData.Length)
				throw new InvalidDataException();
			return new MemoryStream(outputData);
		}

		/*protected override Resource Load() {
			BinaryReader reader = OpenReader();
			string name = Path.GetFileName(Archive.Name).ToLowerInvariant();

			switch(name) {
				case "back1.dax":
				case "bigpic1.dax":
				case "bigpic2.dax":
				case "bigpic3.dax":
				case "bigpic4.dax":
				case "bigpic5.dax":
				case "cpic1.dax":
				case "cpic2.dax":
				case "cpic3.dax":
				case "cpic4.dax":
				case "cpic5.dax":
				case "cpic6.dax":
				case "cpic7.dax":
				case "cpic8.dax":
				case "cpic9.dax":
				case "dungcom.dax":
				case "skygrnd.dax":
					return new Image(this, reader);

				case "bord.dax":
					switch (Id) {
						case 7:
						case 9:
						case 10:
							break;

						default:
							return new Image(this, reader);
					}
					break;

				default:
					break;
			}

			return new BinaryResource(Manager, Name, reader.ReadAllBytes());
		}*/
	}

	class ArchiveFormat : AssetFormat {
		public ArchiveFormat(Engine engine)
			: base(engine, typeof(Archive), canLoad: true) {
		}

		public override LoadMatchStrength LoadMatch(AssetLoader info) {
			var reader = info.Reader;
			long length = reader.BaseStream.Length;

			if (length < 2)
				return LoadMatchStrength.None;

			int headerLength = reader.ReadUInt16();
			if (headerLength == 0 || headerLength % ArchiveRecord.HeaderSize != 0 || length < headerLength + 2)
				return LoadMatchStrength.None;

			int dataOffset = headerLength + 2;
			int currentOffset = dataOffset;

			for (int index = 0; index < headerLength / 9; index++) {
				byte id = reader.ReadByte();
				int offset = reader.ReadInt32() + dataOffset;
				int uncompressedSize = reader.ReadUInt16();
				int compressedSize = reader.ReadUInt16();

				if (offset != currentOffset)
					return LoadMatchStrength.None;
				currentOffset += compressedSize;

				// Offset is out of the file.
				if (offset > length)
					return LoadMatchStrength.None;

				// File is empty but one of the sizes is not.
				if (compressedSize == 0 || uncompressedSize == 0)
					return LoadMatchStrength.None;

				// Uncompressed expansion can't be used as a metric. I tried; some files expand one out of seven bytes. It's nuts how bad their RLE compressor was. At a guess it stopped a data run for every repeating byte, resulting in far more data runs when only a repeat run of 4 or more is guaranteed to result in compression (because then you've saved at least two bytes, accounting for your repeat run length and the next run length bytes).
			}

			return LoadMatchStrength.Strong;
		}

		public override Asset Load(AssetLoader info) {
			return new Archive(Manager, info.Reader, info.Name, info.FileManager);
		}
	}
}