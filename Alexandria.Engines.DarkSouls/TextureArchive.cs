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
using Glare.Framework;
using Glare.Assets;

namespace Alexandria.Engines.DarkSouls {
	/// <summary>
	/// A collection of textures.
	/// </summary>
	public class TextureArchive : FolderAsset {
		/// <summary>
		/// Get the magic id at the start of a texture archive file.
		/// </summary>
		public const string Magic = "TPF\0";

		internal readonly Stream Stream;

		/// <summary>
		/// Get the platform this is for.
		/// </summary>
		public DSPlatform Platform { get; private set; }

		/// <summary>
		/// Get the byte order of the textures.
		/// </summary>
		public ByteOrder ByteOrder { get; private set; }

		internal TextureArchive(AssetManager manager, BinaryReader reader, string name)
			: base(manager, name) {
			ByteOrder = ByteOrder.LittleEndian;

			reader.RequireMagic(Magic);
			var totalSize = reader.ReadInt32();
			var count = reader.ReadInt32();
			ByteOrder = ByteOrder.LittleEndian;

			int code = reader.ReadInt32();
			if (code == 0x20302) {
				totalSize = totalSize.ReverseBytes();
				count = count.ReverseBytes();
				ByteOrder = ByteOrder.BigEndian;
				Platform = DSPlatform.PS3;
			} else if(code == 0x02030200) { // BigEndianBinaryReader, PS3
				Platform = DSPlatform.PS3;
			} else if (code != 0x20300)
				throw new InvalidDataException();

			for (int index = 0; index < count; index++)
				new TextureArchiveRecord(this, reader, ByteOrder);
			Stream = reader.BaseStream;
		}

		/// <summary>
		/// Create a browser for the archive. If this contains just one texture, then it is browsed.
		/// </summary>
		/// <returns></returns>
		public override Control Browse(Action<double> progressUpdateCallback = null) {
			if (Children.Count == 1)
				return Children[0].BrowseContents(progressUpdateCallback);
			return base.Browse(progressUpdateCallback);
		}
	}

	/// <summary>
	/// A record in a <see cref="TextureArchive"/>.
	/// </summary>
	public class TextureArchiveRecord : DataAsset {
		readonly int Offset;
		readonly int Size;
		readonly int Id;

		readonly Format Ps3Format;
		readonly Vector2i Ps3Dimensions;

		/// <summary>
		/// The <see cref="TextureArchive"/> this is in.
		/// </summary>
		public TextureArchive Archive { get { return (TextureArchive)Parent; } }

		/// <summary>
		/// Get the display name.
		/// </summary>
		public override string DisplayName {
			get {
				return Name + string.Format(" (id {0:X}h, size {1})", Id, Size);
			}
		}

		internal TextureArchiveRecord(TextureArchive archive, BinaryReader reader, ByteOrder order)
			: base(archive, "") {
			Offset = reader.ReadInt32(order);
			Size = reader.ReadInt32(order);

			if (Archive.Platform == DSPlatform.PS3) {
				int format = reader.ReadInt16(order);
				Id = reader.ReadInt16(order);
				Ps3Dimensions = new Vector2i(reader.ReadUInt16(order), reader.ReadUInt16(order));

				switch (format) {
					case 0: Ps3Format = Glare.Graphics.Formats.DXT1; break;
					case 0x0500: Ps3Format = Glare.Graphics.Formats.DXT5; break;
					case 0x0900: Ps3Format = Glare.Graphics.Formats.Vector4nb; break;
					default: throw new NotSupportedException(string.Format("PS3 format 0x{0:X4} (dimensions {1}, data size {2}) is not known.", format, Ps3Dimensions, Size));
				}

				Unknowns.ReadInt32s(reader, 1);
				reader.RequireZeroes(4);
			} else {
				Id = reader.ReadInt32(order);
			}

			Name = reader.ReadStringzAtUInt32(order, Alexandria.Engines.DarkSouls.Archive.ShiftJis);
			reader.RequireZeroes(4);
		}

		/// <summary>
		/// Open the texture archive.
		/// </summary>
		/// <returns></returns>
		public override Stream Open() {
			var stream = ((TextureArchive)Parent).Stream;
			byte[] data = new byte[Size];

			stream.Position = Offset;
			stream.Read(data, 0, data.Length);
			return new MemoryStream(data, false);
		}

		/// <summary>
		/// Load the texture. This may be stored in a special format; otherwise it is DDS.
		/// </summary>
		/// <returns></returns>
		protected override Asset Load() {
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

					return new TextureAsset(Manager, Name, texture);
				}
			} else
				return base.Load();
		}
	}

	class TextureArchiveFormat : AssetFormat {
		public TextureArchiveFormat(Engine engine)
			: base(engine, typeof(TextureArchive), canLoad: true) {
		}

		public override LoadMatchStrength LoadMatch(AssetLoader info) {
			return info.Reader.MatchMagic(TextureArchive.Magic) ? LoadMatchStrength.Medium : LoadMatchStrength.None;
		}

		public override Asset Load(AssetLoader info) {
			return new TextureArchive(Manager, info.Reader, info.Name);
		}
	}
}
