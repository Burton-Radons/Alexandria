using Glare.Assets;
using Glare.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Alexandria.Engines.Sciagi.Resources {
	/// <summary>
	/// A palette resource.
	/// </summary>
	public class Palette : ResourceData {
		/// <summary>Get the colors of the palette.</summary>
		public ReadOnlyCollection<PaletteColor> Colors { get; private set; }

		/// <summary>Get the first color index.</summary>
		public int FirstIndex { get; private set; }

		/// <summary>Colors in the form of a simple array, offset by <see cref="FirstIndex"/>.</summary>
		public Codex<Color> FlatColors { get; private set; }

		/// <summary>A palette asset that depicts the same as <see cref="FlatColors"/>.</summary>
		public PaletteAsset PaletteAsset { get; private set; }

		internal Palette(AssetLoader loader)
			: base(loader) {
			BinaryReader reader = loader.Reader;

			Unknowns.ReadBytes(reader, 25); // Offset 0-24
			int firstIndex = reader.ReadByte(); // Offset 25-25
			Unknowns.ReadBytes(reader, 3); // Offset 26-28
			int count = reader.ReadUInt16(); // Offset 29-30
			Unknowns.ReadBytes(reader, 1); // Offset 31-31
			byte format = reader.ReadByte(); // Offset 32-32
			Unknowns.ReadBytes(reader, 4); // Offset 33-36
			// Offset 37

			PaletteColor[] colors = new PaletteColor[count];
			Colors = new ReadOnlyCollection<PaletteColor>(colors);
			FlatColors = new Codex<Color>(256);
			for (int index = 0; index < firstIndex; index++)
				FlatColors.Add(Color.Purple);
			for (int index = 0; index < count; index++) {
				PaletteColor color;

				switch (format) {
					case 0: // Variable (byte used, byte red, byte green, byte blue)
						color = new PaletteColor(reader.ReadByte() != 0, reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
						break;

					case 1: // Constant (byte red, byte green, byte blue)
						color = new PaletteColor(reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
						break;

					default:
						throw new NotImplementedException();
				}

				colors[index] = color;
				FlatColors.Add(color);
			}

			PaletteAsset = new PaletteAsset(Manager, Name ?? "Palette", FlatColors);
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
	}

	/// <summary>
	/// A color inside a <see cref="Palette"/>.
	/// </summary>
	public struct PaletteColor {
		/// <summary>Get whether this color is used.</summary>
		public readonly bool Used;

		/// <summary>Get the red attribute, from 0 (dark) to 255 (bright).</summary>
		public readonly byte Red;

		/// <summary>Get the green attribute, from 0 (dark) to 255 (bright).</summary>
		public readonly byte Green;

		/// <summary>Get the blue attribute, from 0 (dark) to 255 (bright).</summary>
		public readonly byte Blue;

		/// <summary>Initialise the color.</summary>
		/// <param name="used"></param>
		/// <param name="red"></param>
		/// <param name="green"></param>
		/// <param name="blue"></param>
		public PaletteColor(bool used, byte red, byte green, byte blue) {
			Used = used;
			Red = red;
			Green = green;
			Blue = blue;
		}

		/// <summary>Initialise the color.</summary>
		/// <param name="red"></param>
		/// <param name="green"></param>
		/// <param name="blue"></param>
		public PaletteColor(byte red, byte green, byte blue) {
			Used = true;
			Red = red;
			Green = green;
			Blue = blue;
		}

		/// <summary>Implicitly convert to a standard color.</summary>
		/// <param name="source"></param>
		/// <returns></returns>
		public static implicit operator Color(PaletteColor source) { return Color.FromArgb(source.Red, source.Green, source.Blue); }
	}
}
