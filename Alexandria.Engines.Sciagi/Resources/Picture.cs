using Alexandria.Engines.Sciagi.Controls;
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

namespace Alexandria.Engines.Sciagi.Resources {
	/// <summary>
	/// A picture made up of a series of drawing instructions.
	/// </summary>
	public class Picture : ResourceData {
		/// <summary>Get the collection of cels to draw on the picture, or empty if this is a vector picture.</summary>
		public ReadOnlyCodex<PictureCel> Cels { get; private set; }

		/// <summary>Get the dimensions of the picture.</summary>
		public Vector2i Dimensions { get; private set; }

		/// <summary>
		/// Get the collection of instructions for drawing the picture. For raster pictures made up of <see cref="Cels"/>, this contains synthetic instructions for drawing each <see cref="PictureCel"/>.
		/// </summary>
		public Codex<PictureInstruction> Instructions { get; private set; }

		/// <summary>Get the palette resource this uses, or <c>null</c> if it uses a fixed palette.</summary>
		public Palette Palette { get; private set; }

		internal Picture(AssetLoader loader)
			: base(loader) {
			BinaryReader reader = loader.Reader;
			ushort check = reader.ReadUInt16();
			Codex<PictureInstruction> instructions = new Codex<PictureInstruction>();
			Codex<PictureCel> cels = new Codex<PictureCel>();

			if (check == 0x0E) { // VGA picture
				int celCount = reader.ReadUInt16();
				Unknowns.ReadInt16s(reader, 1); // 0x2A
				int paletteOffset = reader.ReadInt32();
				Dimensions = new Vector2i(reader.ReadUInt16(), reader.ReadUInt16());

				reader.BaseStream.Position = paletteOffset;
				Palette = new Palette(loader);
				AddChild(Palette);

				for (int celIndex = 0; celIndex < celCount; celIndex++) {
					PictureCel cel = new PictureCel(this, celIndex, loader);

					cels.Add(cel);
					AddChild(cel);
					instructions.Add(new PictureInstruction.DrawCel(cel));
				}
			} else {
				Dimensions = new Vector2i(320, 190);
				reader.BaseStream.Seek(-2, SeekOrigin.Current);
				byte[] data = reader.ReadBytes(checked((int)reader.BaseStream.Length));
				Stream stream = new MemoryStream(data, false);
				PicturePatternFlags patternFlags = PicturePatternFlags.None;
				byte patternNumber = 0;

				while (true) {
					PictureInstruction instruction = PictureInstruction.ReadInstruction(stream, ref patternFlags, ref patternNumber);
					instructions.Add(instruction);
					if (instruction.IsQuit)
						break;
				}
			}

			Cels = cels;
			Instructions = instructions;
		}

		/// <summary>Create a browser for drawing the picture.</summary>
		/// <returns></returns>
		public override System.Windows.Forms.Control Browse(Action<double> progressUpdateCallback = null) {
			return new PictureBrowser(this);
		}
	}/*
        public struct Renderer {
            MemoryStream Reader;

        }

    }*/
}
