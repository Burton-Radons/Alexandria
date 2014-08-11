using Glare;
using Glare.Assets;
using Glare.Framework;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Glare.Assets {
	/// <summary>A collection of color values.</summary>
	public class PaletteAsset : Asset {
		/// <summary>Get the collection of colors.</summary>
		protected readonly Codex<Color> ColorsMutable = new Codex<Color>();

		/// <summary>Get the collection of colors.</summary>
		public Codex<Color> Colors { get { return ColorsMutable; } }

		/// <summary>Initialise the asset.</summary>
		/// <param name="manager"></param>
		/// <param name="name"></param>
		/// <param name="colors"></param>
		public PaletteAsset(AssetManager manager, string name, params Color[] colors) : this(manager, name, (IEnumerable<Color>)colors) { }

		/// <summary>Initialise the asset.</summary>
		/// <param name="folder"></param>
		/// <param name="name"></param>
		/// <param name="colors"></param>
		public PaletteAsset(FolderAsset folder, string name, params Color[] colors) : this(folder, name, (IEnumerable<Color>)colors) { }

		/// <summary>Initialise the asset.</summary>
		/// <param name="manager"></param>
		/// <param name="name"></param>
		/// <param name="colors"></param>
		public PaletteAsset(AssetManager manager, string name, IEnumerable<Color> colors)
			: base(manager, name) {
			if (colors != null)
				ColorsMutable.AddRange(colors);
		}

		/// <summary>Initialise the asset.</summary>
		/// <param name="folder"></param>
		/// <param name="name"></param>
		/// <param name="colors"></param>
		public PaletteAsset(FolderAsset folder, string name, IEnumerable<Color> colors)
			: base(folder, name) {
			if (colors != null)
				ColorsMutable.AddRange(colors);
		}

		/// <summary>Produces a picture box showing the colors.</summary>
		/// <returns></returns>
		public override Control Browse(Action<double> progressUpdateCallback = null) {
			int scale = 4;
			Bitmap bitmap = new Bitmap(64, Colors.Count * scale);
			PictureBox box = new PictureBox() { Image = bitmap };

			using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap)) {
				SolidBrush brush = new SolidBrush(Color.White);

				for (int index = 0; index < Colors.Count; index++) {
					brush.Color = Colors[index];
					graphics.FillRectangle(brush, 0, index * scale, bitmap.Width, scale);
				}
			}

			return box;
		}

		/// <summary>
		/// Read a <see cref="PaletteAsset"/> from the <see cref="BinaryReader"/>.
		/// </summary>
		/// <param name="manager"></param>
		/// <param name="name"></param>
		/// <param name="reader"></param>
		/// <param name="count"></param>
		/// <param name="max"></param>
		/// <param name="transparentIndex"></param>
		/// <param name="leftPadding">Colors that are added to the start of the palette, if non-null.</param>
		/// <param name="rightPadding">Colors that are added to the end of the palette, if non-null.</param>
		/// <returns></returns>
		public static PaletteAsset ReadRgb(AssetManager manager, string name, BinaryReader reader, int count, int max, int transparentIndex = -1, IEnumerable<Color> leftPadding = null, IEnumerable<Color> rightPadding = null) {
			PaletteAsset palette = new PaletteAsset(manager, name);

			if (leftPadding != null)
				foreach (Color color in leftPadding)
					palette.ColorsMutable.Add(color);

			for (int index = 0; index < count; index++) {
				int red = Math.Min(255, reader.ReadByte() * 255 / max);
				int green = Math.Min(255, reader.ReadByte() * 255 / max);
				int blue = Math.Min(255, reader.ReadByte() * 255 / max);

				palette.ColorsMutable.Add(index == transparentIndex ? Color.Transparent : Color.FromArgb(red, green, blue));
			}

			if (rightPadding != null)
				foreach (Color color in rightPadding)
					palette.ColorsMutable.Add(color);

			return palette;
		}
	}
}
