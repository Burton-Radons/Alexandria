using Glare;
using Glare.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glare.Internal;
using Glare.Assets;

namespace Alexandria.Engines.GoldBox.Resources {
	/// <summary>An image.</summary>
	public class Image : FolderAsset {
		internal Image(AssetManager manager, BinaryReader reader, string name, Asset context)
			: base(manager, name) {
			using (reader) {
				int height = reader.ReadByte();
				int width = reader.ReadByte() * 8;
				reader.RequireZeroes(4);
				int frameCount = reader.ReadUInt16();
				if (frameCount == 0)
					throw new InvalidDataException();

				byte baseColorIndex = reader.ReadByte();
				int colorCount = reader.ReadByte() + 1;

				// Need to initialize palette.
				Color[] colors = new Color[256];

				for (int index = 0; index < 16; index++)
					colors[index] = Raster.DefaultEgaColors[index];

				for (int index = 0; index < colorCount; index++)
					colors[index + baseColorIndex] = Color.FromArgb(reader.ReadByte() * 255 / 63, reader.ReadByte() * 255 / 63, reader.ReadByte() * 255 / 63);

				PaletteAsset palette = new PaletteAsset(Manager, Name + " palette", colors);

				Unknowns.ReadBytes(reader, (colorCount + 1) / 2); // EGA color map, two colours per byte.
				Unknowns.ReadBytes(reader, 4);

				int remaining = checked((int)(reader.BaseStream.Length - reader.BaseStream.Position));

				if (remaining == width * height * frameCount + 28) {
					Unknowns.ReadBytes(reader, 28);
					remaining -= 28;
				}

				if (remaining != width * height * frameCount || (height != 0 && remaining % height != 0))
					throw new InvalidDataException("Expected file length is not correct.");

				int[] indices = new int[width * height];

				if (width == 0 || height == 0)
					AddChild(palette);
				else
					for (int index = 0; index < frameCount; index++) {
						reader.ReadBytesAsInt32(indices, 0, indices.Length);
						var resource = new IndexedTextureAsset(Manager, "Frame " + index, palette, width, height, indices);
						AddChild(resource);
					}

				// Hack for "over.dax" files to use their multiple palettes.
				if (context is ArchiveRecord && ((ArchiveRecord)context).Id >= 51 && string.Equals(Path.GetFileName(context.Parent.Name), "over.dax", StringComparison.InvariantCultureIgnoreCase)) {
					Archive archive = (Archive)context.Parent;

					for (int id = 1; id <= 4; id++) {
						palette = (PaletteAsset)archive.RecordsById[id].Contents.Children[0];
						var frame = new IndexedTextureAsset(Manager, "Palette " + id, palette, width, height, indices);
						AddChild(frame);
					}
				}
			}
		}

		/// <summary>Present the image.</summary>
		/// <returns></returns>
		public override System.Windows.Forms.Control Browse(Action<double> progressUpdateCallback = null) {
			if (Children.Count == 1)
				return Children[0].Browse();
			return base.Browse();
		}
	}

	class ImageFormat : AssetFormat {
		public ImageFormat(Engine engine)
			: base(engine, typeof(Image), canLoad: true) {
		}

		public override LoadMatchStrength LoadMatch(AssetLoader info) {
			var reader = info.Reader;
			long length = reader.BaseStream.Length;

			if (length < 11)
				return LoadMatchStrength.None;
			int height = reader.ReadByte();
			int width = reader.ReadByte() * 8;
			int zero1 = reader.ReadInt32();
			int imageCount = reader.ReadUInt16();
			int baseColorIndex = reader.ReadByte();
			int colorCount = reader.ReadByte() + 1;

			if (zero1 != 0 || imageCount == 0)
				return LoadMatchStrength.None;

			int sum = 10; // Header
			sum += colorCount * 3; // Palette
			sum += (colorCount + 1) / 2; // EGA palette indices
			sum += 4; // Unknown
			sum += width * height * imageCount; // Images
			if (sum != length && sum != length - 28)
				return LoadMatchStrength.None;
			return LoadMatchStrength.Medium;
		}

		public override Asset Load(AssetLoader info) {
			return new Image(Manager, info.Reader, info.Name, info.Context);
		}
	}
}
