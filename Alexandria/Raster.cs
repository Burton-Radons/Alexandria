using Glare;
using Glare.Framework;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria {
	/// <summary>
	/// An indexed raster image that can be useful for a variety of systems.
	/// </summary>
	public class Raster {
		#region Internal fields

		byte ColorA, ColorB;
		byte BaseColorA, BaseColorB;
		int Stride;
		int DirtyMaxX, DirtyMaxY, DirtyMinX, DirtyMinY;
		Graphics Graphics4x;
		IList<Color> Palette;
		readonly byte[] ArgbRow;

		#endregion Internal fields

		#region Backing Fields

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		byte[] data;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool ditherBlend;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool ditherBlendOverride;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		Vector2i size;

		static Codex<Color> defaultBlendedEgaColors, defaultEgaColors, defaultEgaColorsWithTransparent;

		#endregion Backing fields

		#region Properties

		public byte[] Data { get { return data; } }

		/// <summary>Get a palette containing the default colors for an EGA adapter.</summary>
		public static Codex<Color> DefaultEgaColors {
			get {
				return defaultEgaColors ?? (defaultEgaColors = new Codex<Color>() {
					Color.FromArgb(0, 0, 0),
					Color.FromArgb(0, 0, 170),
					Color.FromArgb(0, 170, 0),
					Color.FromArgb(0, 170, 170),
					Color.FromArgb(170, 0, 0),
					Color.FromArgb(170, 0, 170),
					Color.FromArgb(170, 85, 0),
					Color.FromArgb(170, 170, 170),
					Color.FromArgb(85, 85, 85),
					Color.FromArgb(85, 85, 255),
					Color.FromArgb(85, 255, 85),
					Color.FromArgb(85, 255, 255),
					Color.FromArgb(255, 85, 85),
					Color.FromArgb(255, 85, 255),
					Color.FromArgb(255, 255, 85),
					Color.FromArgb(255, 255, 255),
				});
			}
		}

		/// <summary>Get a palette containing the default EGA colors, with a color 16 that is transparent.</summary>
		public static Codex<Color> DefaultEgaColorsWithTransparent {
			get {
				if (defaultEgaColorsWithTransparent == null) {
					var list = new Codex<Color>(DefaultEgaColors);
					list.Add(Color.Transparent);
					defaultEgaColorsWithTransparent = list;
				}
				return defaultEgaColorsWithTransparent;
			}
		}

		public static Codex<Color> DefaultBlendedEgaColors {
			get { return defaultBlendedEgaColors ?? (defaultBlendedEgaColors = BlendPalette(DefaultEgaColors)); }
		}

		/// <summary>
		/// The area that was dirtied, or Int32Rect.Empty if !IsDirtied.
		/// </summary>
		Box2i DirtiedArea {
			get {
				if (!IsDirtied)
					return Box2i.Zero;
				return new Box2i(DirtyMinX, DirtyMinY, DirtyMaxX, DirtyMaxY);
			}
		}

		public bool DitherBlend { get { return ditherBlend; } set { ditherBlend = value; } }

		public Bitmap Image { get; protected set; }

		public Bitmap Image4x { get; protected set; }

		public int Height { get { return size.Y; } }

		/// <summary>
		/// Whether anything has actually changed.
		/// </summary>
		bool IsDirtied { get { return DirtyMinX != int.MaxValue; } }

		public bool IsLocked { get { return LockCounter > 0; } }

		/// <summary>
		/// How many times Lock() has been called over Unlock(), used for nested Lock()s.
		/// </summary>
		public int LockCounter { get; protected set; }

		public Vector2i Size { get { return size; } }

		public int Width { get { return size.X; } }

		#endregion Properties

		#region Constructors

		public Raster(Vector2i size, IList<Color> palette, bool colorBlend = false, byte[] sourceData = null) {
			this.size = size;
			Palette = palette;

			Image = new Bitmap(size.X, size.Y, PixelFormat.Format32bppArgb);
			Image4x = new Bitmap(size.X * 4, size.Y * 4, PixelFormat.Format32bppArgb);
			Graphics4x = Graphics.FromImage(Image4x);

			ditherBlend = colorBlend;
			ArgbRow = new byte[size.X * 4];
			data = new byte[size.X * size.Y];
			Stride = size.X;

			if (sourceData != null) {
				Lock();
				sourceData.CopyTo(data, 0);
				DirtyAll();
				Unlock();
			}
		}

		public Raster(int width, int height, IList<Color> palette, bool colorBlend = false, byte[] data = null)
			: this(new Vector2i(width, height), palette, colorBlend, data) {
		}

		#endregion Constructors

		#region Methods

		public static int BlendColorIndex(int a, int b, int colorCount) {
			if (a == b)
				return a;
			return colorCount + a * (colorCount - 1) + b - (b > a ? 1 : 0);
		}

		/// <summary>Returns a blend palette colour index, such that the first 16 colours are the normal EGA set (where a == b), and the subsequent colours are used for blending.
		/// This allows unblended colours to just use the normal palette. Spanzy!</summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static byte BlendColorIndex16(byte a, byte b) {
			return (byte)BlendColorIndex(a, b, 16);
		}

		/// <summary>Create a blended palette, which blends colors from the perspective of either A and B or B and A, but unevenly (unless if <paramref name="blend"/> is <c>0.5</c>). The first colour has <paramref name="blend"/> influence on the result; the second colour has (1 - <paramref name="blend"/>) influence. <see cref="BlendColorIndex"/> and <see cref="BlendColorIndex16"/> can be used to create proper indices.</summary>
		/// <param name="original"></param>
		/// <returns></returns>
		public static Codex<Color> BlendPalette(IList<Color> original, double blend = 2.0 / 3.0) {
			int count = original.Count;
			var colors = new Color[count * count];

			for (var a = 0; a < count; a++) {
				var ca = original[a];
				double ta = blend, tb = 1 - blend;

				for (var b = 0; b < count; b++) {
					var cb = original[b];
					colors[BlendColorIndex(a, b, count)] = Color.FromArgb(
						(int)(ca.R * ta + cb.R * tb),
						(int)(ca.G * ta + cb.G * tb),
						(int)(ca.B * ta + cb.B * tb));
				}
			}

			return new Codex<Color>(colors);
		}

		/// <summary>
		/// Ensure the point is within the dirty rectangle.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		void DirtyPoint(int x, int y) {
			if (x < DirtyMinX)
				DirtyMinX = x;
			if (y < DirtyMinY)
				DirtyMinY = y;
			if (x > DirtyMaxX)
				DirtyMaxX = x;
			if (y > DirtyMaxY)
				DirtyMaxY = y;
		}

		void DirtyAll() {
			DirtyArea(0, 0, Width - 1, Height - 1);
		}

		void DirtyArea(int minX, int minY, int maxX, int maxY) {
			DirtyPoint(minX, minY);
			DirtyPoint(maxX, maxY);
		}

		/// <summary>
		/// Add the dirtied rectangle to the bitmap, then clear the dirtied area.
		/// </summary>
		void Flush() {
			if (!IsLocked)
				return;
			if (IsDirtied) {
				BitmapData bitmapData = Image.LockBits(new Rectangle(DirtyMinX, DirtyMinY, DirtyMaxX - DirtyMinX + 1, DirtyMaxY - DirtyMinY + 1), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
				int paletteCount = Palette.Count;

				for (int y = 0; y < DirtyMaxY - DirtyMinY + 1; y++) {
					int rowWidth = DirtyMaxX - DirtyMinX + 1;
					IntPtr bitmapOutput = bitmapData.Scan0 + bitmapData.Stride * y;
					int input = (y + DirtyMinY) * Stride + DirtyMinX;

					for (int output = 0, end = rowWidth * 4; output < end; output += 4, input++) {
						byte index = data[input];
						Color color = index < paletteCount ? Palette[index] : Color.Purple;
						ArgbRow[output + 3] = color.A;
						ArgbRow[output + 2] = color.R;
						ArgbRow[output + 1] = color.G;
						ArgbRow[output + 0] = color.B;
					}

					Marshal.Copy(ArgbRow, 0, bitmapOutput, rowWidth * 4);
				}

				Image.UnlockBits(bitmapData);

				//using (Graphics graphics = Graphics.FromImage(Image4x)) {
				Graphics4x.InterpolationMode = InterpolationMode.NearestNeighbor;
				Graphics4x.Clear(Color.Black);
				Graphics4x.DrawImage(Image, new Rectangle(0, 0, Image4x.Width, Image4x.Height));
				//}
			}
			DirtyMinX = DirtyMinY = int.MaxValue;
			DirtyMaxX = DirtyMaxY = 0;
		}

		/// <summary>
		/// Start drawing to the bitmap.
		/// </summary>
		public void Lock() {
			if (LockCounter++ > 0)
				return;
			DirtyMinX = DirtyMinY = int.MaxValue;
			DirtyMaxX = DirtyMaxY = 0;
		}

		#endregion Methods

		public bool DitherBlendOverride {
			get {
				return ditherBlendOverride;
			}

			set {
				ditherBlendOverride = value;
				SetColor(BaseColorA, BaseColorB);
			}
		}

		public void SetColor(byte color) { SetColor(color, color); }

		public void SetColor(byte colorA, byte colorB) {
			BaseColorA = colorA;
			BaseColorB = colorB;

			if (ditherBlend) {
				if (DitherBlendOverride) {
					ColorA = ProcessColor(colorA);
					ColorB = ProcessColor(colorB);
				} else
					ColorA = ColorB = BlendColorIndex16(colorA, colorB);
			} else {
				ColorA = colorA;
				ColorB = colorB;
			}
		}

		public bool Contains(int x, int y) { return ContainsX(x) && ContainsY(y); }
		public bool Contains(Vector2i position) { return Contains(position.X, position.Y); }

		public bool ContainsX(int value) { return value >= 0 && value < size.X; }
		public bool ContainsY(int value) { return value >= 0 && value < size.Y; }

		public Vector2i Clamp(Vector2i value) { return new Vector2i(ClampX(value.X), ClampY(value.Y)); }

		public int ClampX(int value) { return value < 0 ? 0 : value >= size.X ? size.X - 1 : value; }
		public int ClampY(int value) { return value < 0 ? 0 : value >= size.Y ? size.Y - 1 : value; }

		int PointerTo(Vector2i position) { return position.X + position.Y * Stride; }
		int PointerTo(int x, int y) { return x + y * Stride; }

		/// <summary>
		/// Draw a pixel, if it's within the clipping range.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public void DrawPixel(int x, int y) {
			if (Contains(x, y))
				data[PointerTo(x, y)] = ColorAt(x, y);
		}

		public void DrawPixel(Vector2i position) { DrawPixel(position.X, position.Y); }

		/// <summary>
		/// Draw a pixel without first checking whether it's within bounds.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		void DrawPixelUnsafe(int x, int y) { data[PointerTo(x, y)] = ColorAt(x, y); }
		void DrawPixelUnsafe(Vector2i position) { DrawPixelUnsafe(position.X, position.Y); }

		public void DrawHorizontalLine(int sx, int y, int ex) {
			if (!ContainsY(y))
				return;
			if (sx > ex)
				Extensions.Swap(ref sx, ref ex);
			if (sx >= size.X || ex < 0)
				return;
			sx = ClampX(sx);
			ex = ClampX(ex);
			for (var x = sx; x <= ex; x++)
				DrawPixel(x, y);
		}

		public void DrawVerticalLine(int x, int sy, int ey) {
			if (!ContainsX(x))
				return;
			if (sy > ey)
				Extensions.Swap(ref sy, ref ey);
			if (sy >= size.Y || ey < 0)
				return;
			sy = ClampY(sy);
			ey = ClampY(ey);
			for (var y = sy; y <= ey; y++)
				DrawPixel(x, y);
		}

		public void DrawLineSci(Vector2i a, Vector2i b) {
			DrawLineSci(a.X, a.Y, b.X, b.Y);
		}

		/// <summary>
		/// Draw a line using SCI-specific logic. Changing this logic in any way will break images.
		/// </summary>
		/// <param name="sx"></param>
		/// <param name="sy"></param>
		/// <param name="ex"></param>
		/// <param name="ey"></param>
		public void DrawLineSci(int sx, int sy, int ex, int ey) {
			if (sy == ey) {
				DrawHorizontalLine(sx, sy, ex);
			} else if (sx == ex) {
				DrawVerticalLine(sx, sy, ey);
			} else {
				int dx = ex - sx, dy = ey - sy;
				int stepx = dx < 0 ? -1 : 1, stepy = dy < 0 ? -1 : 1, fraction;

				dx = Math.Abs(dx) << 1;
				dy = Math.Abs(dy) << 1;
				DrawPixel(ex, ey);
				DrawPixel(sx, sy);

				if (dx > dy) {
					fraction = dy - (dx >> 1);
					while (sx != ex) {
						if (fraction >= 0) {
							sy += stepy;
							fraction -= dx;
						}

						sx += stepx;
						fraction += dy;
						DrawPixel(sx, sy);
					}
				} else {
					fraction = dx - (dy >> 1);
					while (sy != ey) {
						if (fraction >= 0) {
							sx += stepx;
							fraction -= dy;
						}

						sy += stepy;
						fraction += dx;
						DrawPixel(sx, sy);
					}
				}
			}
		}

		public byte ColorAt(int x, int y) { return ((x ^ y) & 1) != 0 ? ColorB : ColorA; }
		public byte ColorAt(Vector2i position) { return ColorAt(position.X, position.Y); }

		/// <summary>
		/// Floodfill drawing in such a way that specifically emulates SCI. Its logical cannot be changed without breaking things.
		/// </summary>
		/// <param name="at"></param>
		public void DrawFillSci(Vector2i at) {
			if (!Contains(at))
				return;
			DrawFillSci(at, GetPixel(at));
		}

		public void DrawFillSci(Vector2i at, byte matchColor) {
			var stack = new Stack<Vector2i>(100);

			matchColor = ProcessColor(matchColor);
			if (!Contains(at) || GetPixel(at) != matchColor || matchColor == ColorA || matchColor == ColorB) {
				if (Contains(at))
					DrawPixelUnsafe(at);
				return;
			}
			stack.Push(at);

			while (stack.Count != 0) {
				var position = stack.Pop();
				int index = PointerTo(position);
				if (data[index] != matchColor)
					continue;
				DirtyPoint(position.X, position.Y);
				data[index] = ColorAt(position.X, position.Y);
				int start, end;
				index -= position.X;

				for (start = position.X; start > 0; start--) {
					if (data[index + start - 1] != matchColor)
						break;
					data[index + start - 1] = ColorAt(start - 1, position.Y);
				}

				for (end = position.X; end < size.X - 1; end++) {
					if (data[index + end + 1] != matchColor)
						break;
					data[index + end + 1] = ColorAt(end + 1, position.Y);
				}

				DirtyPoint(start, position.Y);
				DirtyPoint(end, position.Y);

				if (position.Y > 0) {
					if (position.Y < size.Y - 1) {
						for (int x = start; x <= end; x++) {
							DrawFillAddToStackUnsafe(stack, x, position.Y - 1, matchColor);
							DrawFillAddToStackUnsafe(stack, x, position.Y + 1, matchColor);
						}
					} else {
						for (int x = start; x <= end; x++)
							DrawFillAddToStackUnsafe(stack, x, position.Y - 1, matchColor);
					}
				} else if (position.Y < size.Y - 1) {
					for (int x = start; x <= end; x++)
						DrawFillAddToStackUnsafe(stack, x, position.Y + 1, matchColor);
				}
			}
		}

		void DrawFillAddToStackUnsafe(Stack<Vector2i> queue, int x, int y, byte matchColor) {
			if (data[x + y * Stride] == matchColor)
				queue.Push(new Vector2i(x, y));
		}

		public void Clear(byte value = 0) {
			value = ProcessColor(value);

			for (int index = 0, count = Stride * Height; index < count; index++)
				data[index] = value;
			DirtyArea(0, 0, Width - 1, Height - 1);
		}

		public byte GetPixel(Vector2i position) {
			if (Contains(position))
				return data[position.X + position.Y * Stride];
			return 0;
		}

		byte ProcessColor(byte value) {
			value &= 15;
			if (ditherBlend)
				return BlendColorIndex16(value, value);
			return value;
		}

		/// <summary>
		/// Stop drawing to the bitmap, flushing the changes.
		/// </summary>
		public void Unlock() {
			if (LockCounter <= 0)
				return;
			if (LockCounter == 1) {
				Flush();
			}
			LockCounter--;
		}
	}
}
