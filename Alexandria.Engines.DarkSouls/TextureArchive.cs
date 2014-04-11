using Alexandria.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Glare.Internal;
using Glare;
using Glare.Graphics;
using System.Resources;

namespace Alexandria.Engines.DarkSouls {
	public class TextureArchive : Folder {
		public const string Magic = "TPF\0";

		internal readonly Stream Stream;

		public DSPlatform Platform { get; private set; }

		public TextureArchive(Manager manager, BinaryReader reader, string name)
			: base(manager, name) {
			reader.RequireMagic(Magic);
			var totalSize = reader.ReadInt32();
			var count = reader.ReadInt32();
			ByteOrder order = ByteOrder.LittleEndian;

			int code = reader.ReadInt32();
			if (code == 0x20302) {
				totalSize = totalSize.ReverseBytes();
				count = count.ReverseBytes();
				order = ByteOrder.BigEndian;
				Platform = DSPlatform.PS3;
			} else if(code == 0x02030200) { // BigEndianBinaryReader, PS3
				Platform = DSPlatform.PS3;
			} else if (code != 0x20300)
				throw new InvalidDataException();

			for (int index = 0; index < count; index++)
				new TextureArchiveRecord(this, reader, order);
			Stream = reader.BaseStream;
		}

		public override Control Browse() {
			if (Children.Count == 1)
				return Children[0].BrowseContents();
			return base.Browse();
		}
	}

	public class TextureArchiveRecord : Asset {
		readonly int Offset;
		readonly int Size;
		readonly int Id;

		readonly Format Ps3Format;
		readonly Vector2i Ps3Dimensions;

		public TextureArchive Archive { get { return (TextureArchive)Parent; } }

		public override string DisplayName {
			get {
				return Name + string.Format(" (id {0:X}h, size {1})", Id, Size);
			}
		}

		public TextureArchiveRecord(TextureArchive archive, BinaryReader reader, ByteOrder order)
			: base(archive, "") {
			Offset = reader.ReadInt32(order);
			Size = reader.ReadInt32(order);

			if (Archive.Platform == DSPlatform.PS3) {
				int format = reader.ReadInt16(order);
				Id = reader.ReadInt16(order);
				Ps3Dimensions = new Vector2i(reader.ReadUInt16(), reader.ReadUInt16());

				switch (format) {
					case 0: Ps3Format = TextureFormats.DXT1; break;
					case 0x0500: Ps3Format = TextureFormats.DXT5; break;
					default: throw new NotSupportedException(string.Format("PS3 format 0x{0:X8} (dimensions {1}, data size {2}) is not known.", format, Ps3Dimensions, Size));
				}

				reader.RequireZeroes(8);
			} else {
				Id = reader.ReadInt32(order);
			}

			Name = reader.ReadStringzAtUInt32(order, Alexandria.Engines.DarkSouls.Archive.ShiftJis);
			reader.RequireZeroes(4);
		}

		public override Stream Open() {
			var stream = ((TextureArchive)Parent).Stream;
			byte[] data = new byte[Size];

			stream.Position = Offset;
			stream.Read(data, 0, data.Length);
			return new MemoryStream(data, false);
		}

		protected override Resource Load() {
			if (Archive.Platform == DSPlatform.PS3) {
				using (var stream = Open()) {
					Texture2D texture = new Texture2D();
					Format format = Ps3Format;
					Vector2i dimensions = Ps3Dimensions;
					byte[] data = new byte[format.AlignedByteSize(dimensions)];
					
					for (int level = 0; ; level++) {
						TextureLevel2D textureLevel = texture.Surface.Levels[level];
						int size = format.AlignedByteSize(dimensions);

						stream.Read(data, 0, size);
						textureLevel.DataCompressed(format, dimensions, data);

						if (dimensions == Vector2i.One)
							break;
						dimensions.X = Math.Max(1, (dimensions.X + 1) / 2);
						dimensions.Y = Math.Max(1, (dimensions.Y + 1) / 2);
					}

					return new Resources.Texture(Manager, texture, Name);
				}
			} else
				return base.Load();
		}
	}

	class TextureArchiveFormat : ResourceFormat {
		public TextureArchiveFormat(Engine engine)
			: base(engine, typeof(TextureArchive), canLoad: true) {
		}

		public override LoadMatchStrength LoadMatch(LoadInfo info) {
			return info.Reader.MatchMagic(TextureArchive.Magic) ? LoadMatchStrength.Medium : LoadMatchStrength.None;
		}

		public override Resource Load(LoadInfo info) {
			return new TextureArchive(Manager, info.Reader, info.Name);
		}
	}
}
