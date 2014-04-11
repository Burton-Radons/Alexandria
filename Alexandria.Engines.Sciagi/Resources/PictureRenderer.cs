using Glare;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.Sciagi.Resources {
	public class PictureRenderer {
		static readonly byte[] DefaultDithers = new byte[] {
			0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77,
			0x88, 0x99, 0xaa, 0xbb, 0xcc, 0xdd, 0xee, 0x88,
			0x88, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x88,
			0x88, 0xf9, 0xfa, 0xfb, 0xfc, 0xfd, 0xfe, 0xff,
			0x08, 0x91, 0x2a, 0x3b, 0x4c, 0x5d, 0x6e, 0x88
		};

		public static readonly byte[] PatternTextureOffsets = new byte[] {
			0x00, 0x18, 0x30, 0xc4, 0xdc, 0x65, 0xeb, 0x48,
			0x60, 0xbd, 0x89, 0x05, 0x0a, 0xf4, 0x7d, 0x7d,
			0x85, 0xb0, 0x8e, 0x95, 0x1f, 0x22, 0x0d, 0xdf,
			0x2a, 0x78, 0xd5, 0x73, 0x1c, 0xb4, 0x40, 0xa1,
			0xb9, 0x3c, 0xca, 0x58, 0x92, 0x34, 0xcc, 0xce,
			0xd7, 0x42, 0x90, 0x0f, 0x8b, 0x7f, 0x32, 0xed,
			0x5c, 0x9d, 0xc8, 0x99, 0xad, 0x4e, 0x56, 0xa6,
			0xf7, 0x68, 0xb7, 0x25, 0x82, 0x37, 0x3a, 0x51,
			0x69, 0x26, 0x38, 0x52, 0x9e, 0x9a, 0x4f, 0xa7,
			0x43, 0x10, 0x80, 0xee, 0x3d, 0x59, 0x35, 0xcf,
			0x79, 0x74, 0xb5, 0xa2, 0xb1, 0x96, 0x23, 0xe0,
			0xbe, 0x05, 0xf5, 0x6e, 0x19, 0xc5, 0x66, 0x49,
			0xf0, 0xd1, 0x54, 0xa9, 0x70, 0x4b, 0xa4, 0xe2,
			0xe6, 0xe5, 0xab, 0xe4, 0xd2, 0xaa, 0x4c, 0xe3,
			0x06, 0x6f, 0xc6, 0x4a, 0xa4, 0x75, 0x97, 0xe1
		};

		public static readonly byte[] PatternTextures = new byte[] {
			0x04, 0x29, 0x40, 0x24, 0x09, 0x41, 0x25, 0x45,
			0x41, 0x90, 0x50, 0x44, 0x48, 0x08, 0x42, 0x28,
			0x89, 0x52, 0x89, 0x88, 0x10, 0x48, 0xA4, 0x08,
			0x44, 0x15, 0x28, 0x24, 0x00, 0x0A, 0x24, 0x20,
		};

		public static readonly byte[][] PatternCircles = new byte[][] {
			new byte[] { 0x01, 0 },
			new byte[] { 0x72, 0x02, 0 },
			new byte[] { 0xCE, 0xF7, 0x7D, 0x0E, 0 },
			new byte[] { 0x1C, 0x3E, 0x7F, 0x7F, 0x7F, 0x3E, 0x1C, 0 },
			new byte[] { 0x38, 0xF8, 0xF3, 0xDF, 0x7F, 0xFF, 0xFD, 0xF7, 0x9F, 0x3F, 0x38, 0 },
			new byte[] { 0x70, 0xC0, 0x1F, 0xFE, 0xE3, 0x3F, 0xFF, 0xF7, 0x7F, 0xFF, 0xE7, 0x3F, 0xFE, 0xC3, 0x1F, 0xF8, 0 },
			new byte[] { 0xF0, 0x01, 0xFF, 0xE1, 0xFF, 0xF8, 0x3F, 0xFF, 0xDF, 0xFF, 0xF7, 0xFF, 0xFD, 0x7F, 0xFF, 0x9F, 0xFF, 0xE3, 0xFF, 0xF0, 0x1F, 0xF0, 0x01, 0 },
			new byte[] { 0xE0, 0x03, 0xF8, 0x0F, 0xFC, 0x1F, 0xFE, 0x3F, 0xFE, 0x3F, 0xFF, 0x7F, 0xFF, 0x7F, 0xFF, 0x7F, 0xFF, 0x7F, 0xFF, 0x7F, 0xFE, 0x3F, 0xFE, 0x3F, 0xFC, 0x1F, 0xF8, 0x0F, 0xE0, 0x03, 0 }
		};

		/// <summary>
		/// Number of colours in an EGA dither palette.
		/// </summary>
		public const int EgaDitherPaletteSize = 40;

		/// <summary>
		/// Number of EGA dither palettes.
		/// </summary>
		public const int EgaDitherPaletteCount = 4;

		/// <summary>
		/// Number of colours altogether in the EGA dither palettes (Size * Count).
		/// </summary>
		public const int EgaDitherPaletteColors = EgaDitherPaletteSize * EgaDitherPaletteCount;

		public readonly PictureCanvas Canvas;
		public bool DrawVisual = true, DrawPriority = true, DrawAuxiliary, DrawControl;

		public readonly byte[] DitherPalette = new byte[EgaDitherPaletteColors];
		public Vector2i Position, StartPosition;

		public PicturePatternFlags PatternFlags = PicturePatternFlags.None;
		public byte PatternNumber = 0;

		public bool UsePattern { get { return (PatternFlags & PicturePatternFlags.UsePattern) != 0; } }
		public int PatternSize { get { return (int)(PatternFlags & PicturePatternFlags.SizeMask); } }

		public void DrawMoveTo(Vector2i position) { StartPosition = Position = position; }

		public void DrawLineTo(Vector2i end) {
			if (DrawVisual)
				Canvas.VisualRaster.DrawLineSci(Position, end);
			if (DrawPriority)
				Canvas.PriorityRaster.DrawLineSci(Position, end);
			if (DrawAuxiliary)
				Canvas.AuxiliaryRaster.DrawLineSci(Position, end);
			if (DrawControl)
				Canvas.ControlRaster.DrawLineSci(Position, end);
			Position = end;
		}

		public void DrawFloodfill(Vector2i at) {
			if (DrawVisual)
				Canvas.VisualRaster.DrawFillSci(at, 15);
			if (DrawPriority)
				Canvas.PriorityRaster.DrawFillSci(at, 0);
			if (DrawAuxiliary)
				Canvas.AuxiliaryRaster.DrawFillSci(at, 0);
			if (DrawControl)
				Canvas.ControlRaster.DrawFillSci(at, 0);
			if (DrawAuxiliary) { }
		}

		void DrawPixel(Vector2i at) {
			if (DrawVisual)
				Canvas.VisualRaster.DrawPixel(at);
			if (DrawPriority)
				Canvas.PriorityRaster.DrawPixel(at);
			if (DrawAuxiliary)
				Canvas.AuxiliaryRaster.DrawPixel(at);
			if (DrawControl)
				Canvas.ControlRaster.DrawPixel(at);
		}

		void DrawPixel(int x, int y) {
			DrawPixel(new Vector2i(x, y));
		}

		public void DrawPatternAt(Vector2i position) {
			int size = PatternSize;
			int sx = Math.Max(position.X - size, 0);
			int sy = Math.Max(position.Y - size, 0);
			int ex = Math.Min(sx + size * 2 + 1, Canvas.Resolution.X - 1);
			int ey = Math.Min(sy + size * 2 + 1, Canvas.Resolution.Y - 1);
			int baseTextureOffset = PatternTextureOffsets[PatternNumber];
			int textureOffset = baseTextureOffset / 8;
			int textureBit = 7 - baseTextureOffset % 8, textureValue = PatternTextures[textureOffset % PatternTextures.Length];
			byte[] circleData = PatternCircles[size];
			int circleOffset = 1;
			int circleValue = circleData[0];
			int circleBit = 7;

			bool useRectangle = (PatternFlags & PicturePatternFlags.Rectangle) != 0;
			bool usePattern = (PatternFlags & PicturePatternFlags.UsePattern) != 0;

			Canvas.VisualRaster.DitherBlendOverride = false;

			for (int y = sy; y <= ey; y++) {
				for (int x = sx; x <= ex; x++) {
					if (textureBit < 0) {
						textureValue = PatternTextures[textureOffset++ % PatternTextures.Length];
						textureBit = 7;
					}

					bool textureOn = usePattern ? (textureValue & (1 << textureBit)) != 0 : true;

					if (useRectangle) {
						if (usePattern) {
							if (textureOn)
								DrawPixel(x, y);
							textureBit--;
						}
					} else {
						bool circleOn = (circleValue & 1) != 0;
						circleValue >>= 1;
						if (circleBit-- == 0) {
							circleBit = 7;
							circleValue = circleOffset >= circleData.Length ? 0 : circleData[circleOffset++];
						}

						if (circleOn) {
							if (textureOn)
								DrawPixel(x, y);
							textureBit--;
						}
					}
				}
			}

			Canvas.VisualRaster.DitherBlendOverride = false;
		}

		public PictureRenderer(PictureCanvas canvas) {
			Canvas = canvas;

			for (var index = 0; index < DitherPalette.Length; index++)
				DitherPalette[index] = DefaultDithers[index % EgaDitherPaletteSize];

			Canvas.Clear();
			Canvas.VisualRaster.SetColor(0, 0);
			Canvas.PriorityRaster.SetColor(0);
		}

		public void Render(ICollection<PictureInstruction> instructions) {
			if (instructions == null)
				throw new ArgumentNullException("instructions");
			Render(instructions, instructions.Count);
		}

		public void Render(IEnumerable<PictureInstruction> instructions, int count) {
			int index = 0;

			Canvas.Lock();
			foreach (PictureInstruction instruction in instructions) {
				if (index++ >= count)
					break;
				instruction.Render(this);
			}
			Canvas.Unlock();
		}
	}

	[Flags]
	public enum PicturePatternFlags {
		None = 0,
		SizeMask = 0x07,
		Rectangle = 0x10,
		UsePattern = 0x20,
	}


}
