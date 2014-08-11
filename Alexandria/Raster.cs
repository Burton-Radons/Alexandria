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

		int ColorA, ColorB;
		int BaseColorA, BaseColorB;
		int Stride;
		int DirtyMaxX, DirtyMaxY, DirtyMinX, DirtyMinY;
		Graphics Graphics4x;
		IList<Color> Palette;
		readonly byte[] ArgbRow;

		#endregion Internal fields

		#region Backing Fields

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly int ColorBlendBaseCount;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		int[] data;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool ditherBlend;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool ditherBlendOverride;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		Vector2i size;

		static Codex<Color> defaultBlendedEgaColors, defaultEgaColors, defaultEgaColorsWithTransparent;

		#endregion Backing fields

		#region Properties

		/// <summary>Get the color indices.</summary>
		public int[] Data { get { return data; } }

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

		/// <summary>Get a codex of the default EGA colors in a blended palette.</summary>
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

		/// <summary>Get or set whether to dither or blend.</summary>
		public bool DitherBlend { get { return ditherBlend; } set { ditherBlend = value; } }

		/// <summary>Get or set whether to override dithering and blending.</summary>
		public bool DitherBlendOverride {
			get {
				return ditherBlendOverride;
			}

			set {
				ditherBlendOverride = value;
				SetColor(BaseColorA, BaseColorB);
			}
		}

		/// <summary>Get the <see cref="Bitmap"/> image this writes to.</summary>
		public Bitmap Image { get; protected set; }

		/// <summary>Get or set a scaled <see cref="Bitmap"/> image that this writes to.</summary>
		public Bitmap Image4x { get; protected set; }

		/// <summary>Get the height in pixels of the raster.</summary>
		public int Height { get { return size.Y; } }

		/// <summary>
		/// Whether anything has actually changed.
		/// </summary>
		bool IsDirtied { get { return DirtyMinX != int.MaxValue; } }

		/// <summary>Get whether this raster is currently locked.</summary>
		public bool IsLocked { get { return LockCounter > 0; } }

		/// <summary>
		/// How many times Lock() has been called over Unlock(), used for nested Lock()s.
		/// </summary>
		public int LockCounter { get; protected set; }

		/// <summary>Get the dimensions of the raster.</summary>
		public Vector2i Size { get { return size; } }

		/// <summary>Get the width in pixels of the raster.</summary>
		public int Width { get { return size.X; } }

		#endregion Properties

		#region Constructors

		/// <summary>Initialize the <see cref="Raster"/>.</summary>
		/// <param name="size">The dimensions of the raster image.</param>
		/// <param name="palette">The palette to use for colours. This may be a regular palette or a colour blend palette.</param>
		/// <param name="colorBlend">If non-negative this is the number of colours in the base palette for a colour-blended palette.</param>
		/// <param name="sourceData"></param>
		public Raster(Vector2i size, IList<Color> palette, int? colorBlend = null, byte[] sourceData = null) {
			this.size = size;
			Palette = palette;

			Image = new Bitmap(size.X, size.Y, PixelFormat.Format32bppArgb);
			Image4x = new Bitmap(size.X * 4, size.Y * 4, PixelFormat.Format32bppArgb);
			Graphics4x = Graphics.FromImage(Image4x);

			ColorBlendBaseCount = colorBlend.GetValueOrDefault(16);
			ditherBlend = colorBlend.HasValue;
			ArgbRow = new byte[size.X * 4];
			data = new int[size.X * size.Y];
			Stride = size.X;

			if (sourceData != null) {
				Lock();
				sourceData.CopyTo(data, 0);
				DirtyAll();
				Unlock();
			}
		}

		/// <summary>Initialize the <see cref="Raster"/>.</summary>
		/// <param name="width">The width in pixels of the raster image.</param>
		/// <param name="height">The height in pixels of the raster image.</param>
		/// <param name="palette">The palette to use for colours. This may be a regular palette or a colour blend palette.</param>
		/// <param name="colorBlendBaseCount">If non-negative this is the number of colours in the base palette for a colour-blended palette.</param>
		/// <param name="data">The optional data for the raster.</param>
		public Raster(int width, int height, IList<Color> palette, int? colorBlendBaseCount = null, byte[] data = null)
			: this(new Vector2i(width, height), palette, colorBlendBaseCount, data) {
		}

		#endregion Constructors

		#region Methods

		/// <summary>Get the blend colour index in a blended palette.</summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <param name="colorCount"></param>
		/// <returns></returns>
		public static int BlendColorIndex(int a, int b, int colorCount) {
			if (a == b)
				return a;
			return colorCount + a * (colorCount - 1) + b - (b > a ? 1 : 0);
		}

		int BlendColorIndex(int a, int b) { return BlendColorIndex(a, b, ColorBlendBaseCount); }

		/// <summary>Create a blended palette, which blends colors from the perspective of either A and B or B and A, but unevenly (unless if <paramref name="blend"/> is <c>0.5</c>). The first colour has <paramref name="blend"/> influence on the result; the second colour has (1 - <paramref name="blend"/>) influence. <see cref="BlendColorIndex(int,int,int)"/> can be used to create proper indices.</summary>
		/// <param name="original">The original set of colours.</param>
		/// <param name="blend">The blending between colours used in a combination. Making this a value other than 0.5 puts more variety into the dithering.</param>
		/// <returns>The dithered palette values.</returns>
		public static Codex<Color> BlendPalette(IList<Color> original, double blend = 2.0 / 3.0) {
			int count = original.Count;
			var colors = new Color[count * count];
			double ta = blend, tb = 1 - blend;

			for (var a = 0; a < count; a++) {
				var ca = original[a];

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

		void SetupDrawImage(ref Vector2i position, ref Vector2i size, ref int[] data, ref int offset, ref int pitch) {
			if (size.X < 0)
				throw new ArgumentOutOfRangeException("Size's X field cannot be less than zero.", "size");
			if (size.Y < 0)
				throw new ArgumentOutOfRangeException("Size's Y field cannot be less than zero.", "size");
			if (data == null)
				throw new ArgumentNullException("data");
			if (offset < 0)
				throw new ArgumentOutOfRangeException("Offset cannot be less than zero.", "offset");
			if (offset + pitch * (long)size.Y < 0 || offset + pitch * (long)(size.Y - 1) + size.X > data.Length)
				throw new ArgumentOutOfRangeException("The combination of offset, pitch, and size are out of range for the data.", "pitch");

			if (position.X < 0) {
				size.X += position.X;
				offset -= position.X;
			}

			if (position.Y < 0) {
				size.Y += position.Y;
				offset -= position.Y * pitch;
			}

			if (position.X + size.X > Width)
				size.X = Width - position.X;

			if (position.Y + size.Y > Height)
				size.Y = Height - position.Y;

			if(size.X > 0 && size.Y > 0)
				DirtyArea(position.X, position.Y, position.X + size.X - 1, position.Y + size.Y - 1);
		}

		/// <summary>Draw the image data.</summary>
		/// <param name="position"></param>
		/// <param name="size"></param>
		/// <param name="data"></param>
		/// <param name="offset"></param>
		/// <param name="pitch"></param>
		/// <param name="maskIndex"></param>
		public void DrawImage(Vector2i position, Vector2i size, int[] data, int offset, int pitch, int maskIndex) {
			SetupDrawImage(ref position, ref size, ref data, ref offset, ref pitch);
			for (int y = 0; y < size.Y; y++) {
				int inputIndex = offset + y * pitch;
				int outputIndex = (y + position.Y) * Stride + position.X;
				int endInputIndex = inputIndex + size.X;

				for (; inputIndex < endInputIndex; inputIndex++, outputIndex++) {
					int value = data[inputIndex];

					if (value != maskIndex)
						this.data[outputIndex] = value;
				}
			}
		}

		/// <summary>Draw the image data, using the currently assigned color indices for any non-transparent parts of the image.</summary>
		/// <param name="position"></param>
		/// <param name="size"></param>
		/// <param name="data"></param>
		/// <param name="offset"></param>
		/// <param name="pitch"></param>
		/// <param name="maskIndex"></param>
		public void DrawImageSilhouette(Vector2i position, Vector2i size, int[] data, int offset, int pitch, int maskIndex) {
			SetupDrawImage(ref position, ref size, ref data, ref offset, ref pitch);
			for (int y = 0; y < size.Y; y++) {
				int inputIndex = offset + y * pitch;
				int outputIndex = (y + position.Y) * this.size.X + position.X;
				int endInputIndex = inputIndex + size.X;
				bool code = (position.X ^ y) != 1;

				for (; inputIndex < endInputIndex; inputIndex++, outputIndex++, code = !code) {
					int value = data[inputIndex];

					if (value != maskIndex)
						this.data[outputIndex] = code ? ColorB : ColorA;
				}
			}
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
						int index = data[input];
						Color color = index >= 0 && index < paletteCount ? Palette[index] : Color.Purple;
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

		/// <summary>Set the colour index to write.</summary>
		/// <param name="color"></param>
		public void SetColor(int color) { SetColor(color, color); }

		/// <summary>Set a dithered colour index to write.</summary>
		/// <param name="colorA"></param>
		/// <param name="colorB"></param>
		public void SetColor(int colorA, int colorB) {
			BaseColorA = colorA;
			BaseColorB = colorB;

			if (ditherBlend) {
				if (DitherBlendOverride) {
					ColorA = ProcessColor((byte)colorA);
					ColorB = ProcessColor((byte)colorB);
				} else
					ColorA = ColorB = BlendColorIndex((byte)colorA, (byte)colorB, ColorBlendBaseCount);
			} else {
				ColorA = colorA;
				ColorB = colorB;
			}
		}

		/// <summary>Get whether the coordinates are within the <see cref="Raster"/>.</summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public bool Contains(int x, int y) { return ContainsX(x) && ContainsY(y); }

		/// <summary>Get whehter the coordinates are within the <see cref="Raster"/>.</summary>
		/// <param name="position"></param>
		/// <returns></returns>
		public bool Contains(Vector2i position) { return Contains(position.X, position.Y); }

		/// <summary>Get whether the <see cref="Raster"/> contains this X coordinate.</summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public bool ContainsX(int value) { return value >= 0 && value < size.X; }

		/// <summary>Get whether the <see cref="Raster"/> contains this Y coordinate.</summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public bool ContainsY(int value) { return value >= 0 && value < size.Y; }

		/// <summary>Clamp the value so that it is within the bounds of the <see cref="Raster"/>.</summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public Vector2i Clamp(Vector2i value) { return new Vector2i(ClampX(value.X), ClampY(value.Y)); }

		/// <summary>Clamp the X coordinate to be within the bounds of the <see cref="Raster"/>.</summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public int ClampX(int value) { return value < 0 ? 0 : value >= size.X ? size.X - 1 : value; }

		/// <summary>Clamp the Y coordinate to be within the bounds of the <see cref="Raster"/>.</summary>
		/// <param name="value"></param>
		/// <returns></returns>
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

		/// <summary>Draw a pixel in the <see cref="Raster"/>.</summary>
		/// <param name="position"></param>
		public void DrawPixel(Vector2i position) { DrawPixel(position.X, position.Y); }

		/// <summary>
		/// Draw a pixel without first checking whether it's within bounds.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		void DrawPixelUnsafe(int x, int y) { data[PointerTo(x, y)] = ColorAt(x, y); }
		void DrawPixelUnsafe(Vector2i position) { DrawPixelUnsafe(position.X, position.Y); }

		/// <summary>Draw a horizontal line.</summary>
		/// <param name="sx"></param>
		/// <param name="y"></param>
		/// <param name="ex"></param>
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

		/// <summary>Draw a vertical line.</summary>
		/// <param name="x"></param>
		/// <param name="sy"></param>
		/// <param name="ey"></param>
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

		/// <summary>Draw a line using Sierra SCI's algorithm</summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
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

		/// <summary>Get the colour index that will be used with dithering at the given position.</summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public int ColorAt(int x, int y) { return ((x ^ y) & 1) != 0 ? ColorB : ColorA; }

		/// <summary>Get the colour index that will be used with dithering at the given position.</summary>
		/// <param name="position"></param>
		/// <returns></returns>
		public int ColorAt(Vector2i position) { return ColorAt(position.X, position.Y); }

		/// <summary>
		/// Floodfill drawing in such a way that specifically emulates SCI. Its logical cannot be changed without breaking things.
		/// </summary>
		/// <param name="at"></param>
		public void DrawFillSci(Vector2i at) {
			if (!Contains(at))
				return;
			DrawFillSci(at, GetPixel(at));
		}

		/// <summary>Perform a flood-fill in a way that Sierra's SCI engine does it.</summary>
		/// <param name="at"></param>
		/// <param name="matchColor"></param>
		public void DrawFillSci(Vector2i at, int matchColor) {
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

		void DrawFillAddToStackUnsafe(Stack<Vector2i> queue, int x, int y, int matchColor) {
			if (data[x + y * Stride] == matchColor)
				queue.Push(new Vector2i(x, y));
		}

		/// <summary>Clear the raster to a given index.</summary>
		/// <param name="value"></param>
		public void Clear(int value = 0) {
			value = ProcessColor(value);

			for (int index = 0, count = Stride * Height; index < count; index++)
				data[index] = value;
			DirtyArea(0, 0, Width - 1, Height - 1);
		}

		/// <summary>Get the index at a position, or return the default value if the position is out of range.</summary>
		/// <param name="position"></param>
		/// <param name="defaultValue">The default value to return if th position is out of range.</param>
		/// <returns></returns>
		public int GetPixel(Vector2i position, int defaultValue = 0) {
			if (Contains(position))
				return data[position.X + position.Y * Stride];
			return defaultValue;
		}

		int ProcessColor(int value) {
			value &= 15;
			if (ditherBlend)
				return BlendColorIndex(value, value);
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
