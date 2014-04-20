using OpenGL = OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;
using System.Collections.ObjectModel;
using System.Reflection;

namespace Glare.Graphics {
	/// <summary>
	/// Describes a <see cref="Texture"/>'s pixel format.
	/// </summary>
	public class Format {
		internal const PixelInternalFormat PixelInternalFormat_R64f = (PixelInternalFormat)(-1);
		internal const PixelInternalFormat PixelInternalFormat_Rg64f = (PixelInternalFormat)(-2);
		internal const PixelInternalFormat PixelInternalFormat_Rgb64f = (PixelInternalFormat)(-3);
		internal const PixelInternalFormat PixelInternalFormat_Rgba64f = (PixelInternalFormat)(-4);

		internal const PixelType PixelType_Double = (PixelType)(-1);
		internal const PixelType PixelType_Fixed = (PixelType)(-2);

		static readonly Dictionary<Type, Format> systemTypes = new Dictionary<Type, Format>();

		static List<Format> list;
		internal static List<Format> BaseList { get { return list ?? (list = new List<Format>()); } }
		static readonly Dictionary<ActiveAttribType, Format> ByActiveAttributeType = new Dictionary<ActiveAttribType, Format>();

		internal readonly PixelFormat pixelFormat;
		internal readonly PixelInternalFormat pixelInternalFormat;
		internal readonly PixelType pixelType;
		internal readonly FormatChannelCollection channels;

		internal ActiveAttribType? ActiveAttribType;

		public int BitsPerSample {
			get {
				switch (pixelType) {
					case PixelType.UnsignedByte332: return 8;
					case PixelType.UnsignedShort4444: return 16;
					case PixelType.UnsignedShort5551: return 16;
					case PixelType.UnsignedInt8888: return 32;
					case PixelType.UnsignedInt1010102: return 32;
					case PixelType.UnsignedByte233Reversed: return 1;
					case PixelType.UnsignedShort565: return 16;
					case PixelType.UnsignedShort565Reversed: return 16;
					case PixelType.UnsignedShort4444Reversed: return 16;
					case PixelType.UnsignedShort1555Reversed: return 16;
					case PixelType.UnsignedInt8888Reversed: return 32;
					case PixelType.UnsignedInt2101010Reversed: return 32;
					case PixelType.UnsignedInt248: return 32;
					case PixelType.UnsignedInt10F11F11FRev: return 32;
					case PixelType.UnsignedInt5999Rev: return 32;
					case PixelType.Float32UnsignedInt248Rev: return 632;
					default:
						return BitsPerComponent * ComponentCount;
				}
			}
		}

		public int BytesPerSample { get { return (BitsPerSample + 7) / 8; } }

		public int BitsPerComponent {
			get {
				switch (pixelType) {
					//case PixelType.Bitmap:
					//return 1;
					case PixelType.Byte:
					case PixelType.UnsignedByte:
						return 8;
					case PixelType.Short:
					case PixelType.UnsignedShort:
					case PixelType.HalfFloat:
						return 2;
					case PixelType.Int:
					case PixelType.UnsignedInt:
					case PixelType.Float:
						return 32;
					case PixelType.Float32UnsignedInt248Rev:
						return 64;
					default:
						throw new NotImplementedException();
				}
			}
		}

		public int ComponentCount {
			get {
				switch (pixelFormat) {
					case PixelFormat.ColorIndex:
					case PixelFormat.DepthComponent:
					case PixelFormat.Red:
					case PixelFormat.Green:
					case PixelFormat.Blue:
					case PixelFormat.Alpha:
					case PixelFormat.Luminance:
					case PixelFormat.RedInteger:
					case PixelFormat.BlueInteger:
					case PixelFormat.AlphaInteger:
						return 1;
					case PixelFormat.LuminanceAlpha:
					case PixelFormat.Rg:
					case PixelFormat.RgInteger:
					case PixelFormat.DepthStencil:
						return 2;
					case PixelFormat.Rgb:
					case PixelFormat.Bgr:
					case PixelFormat.Ycrcb422Sgix:
					case PixelFormat.Ycrcb444Sgix:
					case PixelFormat.RgbInteger:
					case PixelFormat.BgrInteger:
						return 3;
					case PixelFormat.Rgba:
					case PixelFormat.AbgrExt:
					case PixelFormat.CmykExt:
					case PixelFormat.Bgra:
					case PixelFormat.RgbaInteger:
					case PixelFormat.BgraInteger:
						return 4;
					case PixelFormat.CmykaExt:
						return 5;
					default:
						throw new NotImplementedException();
				}
			}
		}

		/// <summary>Get the type of compression in use, or <see cref="FormatCompression.None"/> for no compression.</summary>
		public FormatCompression Compression { get; internal set; }

		/// <summary>Get whether <see cref="Compression"/> is a compressed format.</summary>
		public bool IsCompressed { get { return Compression != FormatCompression.None; } }

		public bool IsNormalized {
			get {
				switch (channels[0].Type) {
					case FormatChannelType.CompressedNormalized:
					case FormatChannelType.SignedNormalized:
					case FormatChannelType.UnsignedNormalized: return true;
					case FormatChannelType.Float:
					case FormatChannelType.SignedInteger:
					case FormatChannelType.UnsignedInteger: return false;
					default: throw new InvalidOperationException();
				}
			}
		}

		/// <summary>Get whether the RGB components are encoded in the sRGB color space.</summary>
		public bool IsSrgb { get; internal set; }

		/// <summary>Get the system type this corresponds to, or <c>null</c> if there is none.</summary>
		public Type Type { get; internal set; }

		internal VertexAttribPointerType? VertexAttribPonterType {
			get {
				switch (pixelType) {
					case PixelType.Byte: return VertexAttribPointerType.Byte;
					case PixelType.Float: return VertexAttribPointerType.Float;
					case PixelType.HalfFloat: return VertexAttribPointerType.HalfFloat;
					case PixelType.Int: return VertexAttribPointerType.Int;
					case PixelType.Short: return VertexAttribPointerType.Short;
					case PixelType.UnsignedByte: return VertexAttribPointerType.UnsignedByte;
					case PixelType.UnsignedInt: return VertexAttribPointerType.UnsignedInt;
					case PixelType.UnsignedShort: return VertexAttribPointerType.UnsignedShort;
					case PixelType_Double: return VertexAttribPointerType.Double;
					case PixelType_Fixed: return VertexAttribPointerType.Fixed;
					default: return null;
				}/*		
		UnsignedInt2101010Rev = 33640,
		Int2101010Rev = 36255,
*/
			}
		}

		internal Format(PixelInternalFormat internalFormat, PixelFormat format, PixelType type) {
			this.pixelInternalFormat = internalFormat;
			this.pixelFormat = format;
			this.pixelType = type;
		}

		internal Format(PixelInternalFormat internalFormat, PixelFormat format, PixelType type, FormatChannel[,] channels)
			: this(internalFormat, format, type) {
			this.channels = new FormatChannelCollection(channels);
			SetupChannels();
		}

		internal Format(PixelInternalFormat internalFormat, PixelFormat format, PixelType type, int rows, FormatChannelType channelType, int channelBits)
			: this(internalFormat, format, type, rows, 1, channelType, channelBits) { }

		internal Format(PixelInternalFormat internalFormat, PixelFormat format, PixelType type, int rows, int columns, FormatChannelType channelType, int channelBits)
			: this(internalFormat, format, type) {
			int realCount = (rows == -1 || rows == -2) ? 1 : (rows == -3) ? 2 : rows;
			FormatChannel[,] channels = new FormatChannel[rows, columns];

			if (rows == -1)
				channels[0, 0] = new FormatChannel(FormatChannelIndex.Depth, channelType, channelBits);
			else if (rows == -2)
				channels[1, 0] = new FormatChannel(FormatChannelIndex.Stencil, channelType, channelBits);
			else if (rows < 0)
				throw new Exception();
			else {
				for (int column = 0; column < columns; column++) {
					for (int row = 0; row < rows; row++) {
						FormatChannelIndex index;

						if (columns > 1)
							index = FormatChannelIndex.Cell;
						else if (format == PixelFormat.Bgr || format == PixelFormat.Bgra)
							switch (row) {
								case 0: index = FormatChannelIndex.Blue; break;
								case 1: index = FormatChannelIndex.Green; break;
								case 2: index = FormatChannelIndex.Red; break;
								case 3: index = FormatChannelIndex.Alpha; break;
								default: throw new Exception();
							} else switch (row) {
								case 0: index = FormatChannelIndex.Red; break;
								case 1: index = FormatChannelIndex.Green; break;
								case 2: index = FormatChannelIndex.Blue; break;
								case 3: index = FormatChannelIndex.Alpha; break;
								default: throw new Exception();
							}

						channels[row, column] = new FormatChannel(index, channelType, channelBits);
					}
				}
			}

			this.channels = new FormatChannelCollection(channels);
			SetupChannels();
		}

		static Format() {
			// Force construction
			new List<Format>(new Format[] { CommonFormats.Vector2ui, TextureFormats.Vector1nsb, VectorFormats.Vector1d });
			TextureFormats.Setup();

			foreach (Format format in BaseList)
				SetupFormat(format);
		}

		static void SetupFormat(Format format) {
			if (format.ActiveAttribType.HasValue) {
				try {
					ByActiveAttributeType.Add(format.ActiveAttribType.Value, format);
				} catch (Exception) {
					throw new Exception(format.ActiveAttribType.Value + " and " + format.pixelInternalFormat);
				}
			}

			if (format.Type != null) {
				if (systemTypes.ContainsKey(format.Type))
					throw new InvalidOperationException("Type " + format.Type.Name + " has already been added as " + systemTypes[format.Type] + ", but is being reregistered as " + format);
				systemTypes.Add(format.Type, format);
			}
		}

		internal static void FinishInit(Type type) {
			FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Static);
			foreach (FieldInfo field in fields) {
				Format format = (Format)field.GetValue(null);
				if (format == null)
					throw new InvalidOperationException("Field " + type.Name + "." + field.Name + " is not initialized!");
				SetupFormat(format);
				//BaseList.Add(format);
			}
		}

		public int AlignedByteSize(int width, int height = 1, int depth = 1, int count = 1) {
			if (IsCompressed) {
				int blockSize = this == TextureFormats.DXT1 ? 8 : 16;
				return Math.Max(1, ((width + 3) / 4)) * Math.Max(1, ((height + 3) / 4)) * blockSize * depth * count;
			} else
				return AlignedBytePitch(width) * height * depth * count;
		}

		public int AlignedByteSize(Vector2i size) { return AlignedByteSize(size.X, size.Y); }

		public int AlignedByteSize(Vector3i size) { return AlignedByteSize(size.X, size.Y, size.Z); }

		public int AlignedByteSize(Vector4i size) { return AlignedByteSize(size.X, size.Y, size.Z, size.W); }

		public int AlignedBytePitch(int width) { return (BytePitch(width) + 3) & ~3; }

		public int BytePitch(int width) { return (width * BitsPerSample + 7) / 8; }

		public int ByteSize(int width, int height = 1, int depth = 1, int count = 1) {
			if (IsCompressed) {
				int blockSize = this == TextureFormats.DXT1 ? 8 : 16;
				return Math.Max(1, ((width + 3) / 4)) * Math.Max(1, ((height + 3) / 4)) * blockSize * depth * count;
			} else {
				int pitchLast = (width * BitsPerSample + 7) / 8;
				int pitch = (pitchLast + 3) & ~3;

				if (height == 0)
					return 0;
				return (pitch * (height - 1) + pitchLast) * depth * count;
			}
		}

		public int ByteSize(Vector2i size) { return ByteSize(size.X, size.Y); }

		public int ByteSize(Vector3i size) { return ByteSize(size.X, size.Y, size.Z); }

		public int ByteSize(Vector4i size) { return ByteSize(size.X, size.Y, size.Z, size.W); }

		public static Format From<T>() { return From(typeof(T)); }

		public static Format From(Type type) {
			return systemTypes[type];
		}

		internal static Format From(ActiveAttribType type) {
			return ByActiveAttributeType[type];
		}

		void SetupChannels() {
			for (int row = 0, rowCount = channels.RowCount, columnCount = channels.ColumnCount; row < rowCount; row++)
				for (int column = 0; column < columnCount; column++) {
					FormatChannel channel = channels[row, column];
					channel.row = row;
					channel.column = column;
					channel.format = this;
				}
		}

		public override string ToString() {
			string text = GetType().Name + "(";
			if (Type != null)
				text += Type.Name;
			else
				text += pixelInternalFormat;
			return text + ")";
		}
	}

	public class FormatChannelCollection : IEnumerable<FormatChannel> {
		internal readonly FormatChannel[,] channels;

		public int ColumnCount { get { return channels.GetLength(1); } }

		public int RowCount { get { return channels.GetLength(0); } }

		internal FormatChannelCollection(FormatChannel[,] info) {
			this.channels = info;
		}

		public FormatChannel this[int row, int column] { get { return channels[row, column]; } }
		public FormatChannel this[int row] { get { return channels[row, 0]; } }

		public struct Enumerator : IEnumerator<FormatChannel> {
			int row, rowCount, column, columnCount;
			FormatChannel[,] channels;

			internal Enumerator(FormatChannelCollection collection) {
				this.channels = collection.channels;
				this.rowCount = collection.RowCount;
				this.columnCount = collection.ColumnCount;
				this.row = -1;
				this.column = 0;
			}

			public FormatChannel Current { get { return channels[row, column]; } }

			public void Dispose() { }

			object System.Collections.IEnumerator.Current { get { return Current; } }

			public bool MoveNext() {
				row++;
				if (row >= rowCount) {
					row = 0;
					column++;
				}
				return column < columnCount;
			}

			public void Reset() {
				row = -1;
				column = 0;
			}
		}

		public Enumerator GetEnumerator() { return new Enumerator(this); }

		IEnumerator<FormatChannel> IEnumerable<FormatChannel>.GetEnumerator() { return GetEnumerator(); }

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return GetEnumerator(); }
	}

	public enum FormatCompression {
		/// <summary>This is not a compressed format.</summary>
		None,

		/// <summary>Compress the texture using a compression method selected by the device.</summary>
		Generic,

		/// <summary>RGB, RGBA, sRGB, or sRGBA.</summary>
		DXT1,

		/// <summary>RGBA or sRGBA.</summary>
		DXT3,

		/// <summary>RGBA or sRGBA.</summary>
		DXT5,
	}

	public enum FormatChannelIndex {
		/// <summary>The channel is part of a matrix element.</summary>
		Cell,

		/// <summary>The fourth color channel.</summary>
		Alpha,

		/// <summary>The third color channel.</summary>
		Blue,

		/// <summary>A depth component.</summary>
		Depth,

		/// <summary>The second color channel.</summary>
		Green,

		/// <summary>The first color channel.</summary>
		Red,

		/// <summary>A stencil component.</summary>
		Stencil,
	}

	public enum FormatChannelType {
		/// <summary>The channel values are compressed values that are interpreted as 0.0 to 1.0.</summary>
		CompressedNormalized,

		/// <summary>The channel is encoded as a floating-point value.</summary>
		Float,

		/// <summary>The channel is encoded as a signed two's-complement integer.</summary>
		SignedInteger,

		/// <summary>The channel is encoded as a signed two's-complement integer with a value mapped to between -1.0 to 1.0. For example, an 8-bit channel would have integer values between -128 to 127, and interpreted values as -1.0 to 1.0.</summary>
		SignedNormalized,

		/// <summary>The channel is encoded as an unsigned two's-complement integer.</summary>
		UnsignedInteger,

		/// <summary>The channel is encoded as an unsigned integer with a value mapped to between 0.0 and 1.0. For example, an 8-bit channel would have integer values between 0 and 255, and interpreted values as 0.0 to 1.0.</summary>
		UnsignedNormalized,
	}

	public class FormatChannel {
		readonly int bits;
		readonly FormatChannelIndex channel;
		readonly FormatChannelType type;
		internal Format format;
		internal int row, column;

		/// <summary>Get the number of bits assigned to the channel. This may be 0 if this is a compressed format.</summary>
		public int Bits { get { return bits; } }

		/// <summary>Get the zero-based column index of this channel, from 0 to 3.</summary>
		public int Column { get { return column; } }

		/// <summary>Get the <see cref="Format"/> this is based on.</summary>
		public Format Format { get { return format; } }

		/// <summary>Get the zero-based row of this channel, from 0 to 3.</summary>
		public int Row { get { return row; } }

		public FormatChannelType Type { get { return type; } }

		internal FormatChannel(FormatChannelIndex channel, FormatChannelType type, int bits) {
			this.channel = channel;
			this.type = type;
			this.bits = bits;
		}
	}

	/// <summary>Formats that can be fed into shader program attributes.</summary>
	public static class VectorFormats {
		public static readonly Format Vector1d;
		public static readonly Format Vector2d;
		public static readonly Format Vector3d;
		public static readonly Format Vector4d;

		static VectorFormats() {
			Format.BaseList.AddRange(new Format[]
			{
				Vector1d = new Format(Format.PixelInternalFormat_R64f, PixelFormat.Red, Format.PixelType_Double, 1, FormatChannelType.Float, 64) { ActiveAttribType = OpenGL.ActiveAttribType.Double, Type = typeof(double) },
				Vector2d = new Format(Format.PixelInternalFormat_Rg64f, PixelFormat.Rg, Format.PixelType_Double, 2, FormatChannelType.Float, 64) { ActiveAttribType = OpenGL.ActiveAttribType.DoubleVec2, Type = typeof(Vector2d) },
				Vector3d = new Format(Format.PixelInternalFormat_Rgb64f, PixelFormat.Rgb, Format.PixelType_Double, 3, FormatChannelType.Float, 64) { ActiveAttribType = OpenGL.ActiveAttribType.DoubleVec3, Type = typeof(Vector3d) },
				Vector4d = new Format(Format.PixelInternalFormat_Rgba64f, PixelFormat.Rgba, Format.PixelType_Double, 4, FormatChannelType.Float, 64) { ActiveAttribType = OpenGL.ActiveAttribType.DoubleVec4, Type = typeof(Vector4d) },
			});
		}
		/*
		FloatMat2 = 35674,
		FloatMat3 = 35675,
		FloatMat4 = 35676,
		DoubleMat2 = 36678,
		DoubleMat3 = 36679,
		DoubleMat4 = 36680,
		DoubleMat2x3 = 36681,
		DoubleMat2x4 = 36682,
		DoubleMat3x2 = 36683,
		DoubleMat3x4 = 36684,
		DoubleMat4x2 = 36685,
		DoubleMat4x3 = 36686,
		 */

	}

	/// <summary>
	/// This contains all formats that can be used for textures.
	/// </summary>
	public static class TextureFormats {
		/// <summary>Single-precision floating point scalar corresponding to <see cref="System.Single"/>. Common to textures and vectors.</summary>
		public static Format Vector1f { get { return CommonFormats.Vector1f; } }

		/// <summary>Half-precision floating point scalar corresponding to <see cref="Glare.Float16"/>. Texture format only.</summary>
		public static readonly Format Vector1h;

		/// <summary>Normalized unsigned byte scalar corresponding to <see cref="Glare.NormalizedByte"/>. Texture format only.</summary>
		public static readonly Format Vector1nb;

		/// <summary>Normalized signed byte scalar corresponding to <see cref="Glare.NormalizedSByte"/>. Texture format only.</summary>
		public static readonly Format Vector1nsb;

		/// <summary>Two-dimensional single-precision floating point vector corresponding to <see cref="Glare.Vector2f"/>. Common to textures and vectors.</summary>
		public static Format Vector2f { get { return CommonFormats.Vector2f; } }

		/// <summary>Two-dimensional half-precision floating point vector corresponding to <see cref="Glare.Vector2h"/>. Texture format only.</summary>
		public static readonly Format Vector2h;

		/// <summary>Two-dimensional normalized unsigned byte vector corresponding to <see cref="Glare.Vector2nb"/>. Texture format only.</summary>
		public static readonly Format Vector2nb;

		/// <summary>Two-dimensional normalized signed byte vector corresponding to <see cref="Glare.Vector2nsb"/>. Texture format only.</summary>
		public static readonly Format Vector2nsb;

		/// <summary>Three-dimensional single-precision floating point vector corresponding to <see cref="Glare.Vector3f"/>. Common to textures and vectors.</summary>
		public static Format Vector3f { get { return CommonFormats.Vector3f; } }

		/// <summary>Three-dimensional half-precision floating point vector corresponding to <see cref="Glare.Vector3h"/>. Texture format only.</summary>
		public static readonly Format Vector3h;

		/// <summary>Three-dimensional normalized unsigned byte vector corresponding to <see cref="Glare.Vector3nb"/>. Texture format only.</summary>
		public static readonly Format Vector3nb;

		/// <summary>Three-dimensional normalized signed byte vector corresponding to <see cref="Glare.Vector3nsb"/>. Texture format only.</summary>
		public static readonly Format Vector3nsb;

		/// <summary>Three-dimensional normalized unsigned byte vector in SRGB color space corresponding to <see cref="Glare.Vector3srgba"/>. Texture format only.</summary>
		public static readonly Format Vector3srgb;

		/// <summary>Four-dimensional 8-bit unsigned byte vector corresponding to <see cref="Glare.Vector4b"/>. Texture format only.</summary>
		public static readonly Format Vector4b;

		/// <summary>Four-dimensional single-precision floating point vector corresponding to <see cref="Glare.Vector4f"/>. Common to textures and vectors.</summary>
		public static Format Vector4f { get { return CommonFormats.Vector4f; } }

		/// <summary>Four-dimensional half-precision floating point vector corresponding to <see cref="Glare.Vector4h"/>. Texture format only.</summary>
		public static readonly Format Vector4h;

		/// <summary>Four-dimensional normalized unsigned byte vector corresponding to <see cref="Glare.Vector4nb"/>. Texture format only.</summary>
		public static readonly Format Vector4nb;

		/// <summary>Four-dimensional normalized unsigned byte vector corresponding to <see cref="Glare.Vector4nb"/>, in BGRA order. Texture format only.</summary>
		public static readonly Format Vector4nbBGRA;

		/// <summary>Four-dimensional normalized signed byte vector corresponding to <see cref="Glare.Vector4nsb"/>. Texture format only.</summary>
		public static readonly Format Vector4nsb;

		/// <summary>Four-dimensional normalized unsigned byte vector in SRGB color space corresponding to <see cref="Glare.Vector4srgba"/>. Texture format only.</summary>
		public static readonly Format Vector4srgba;

		/// <summary>DXT1 compressed format.</summary>
		public static readonly Format DXT1;

		/// <summary>DXT3 compressed format.</summary>
		public static readonly Format DXT3;

		/// <summary>DXT5 compressed format.</summary>
		public static readonly Format DXT5;

		const int GL_R8_SNORM = 36756;

		internal static void Setup() {
		}

		static TextureFormats() {
			Vector1h = new Format(PixelInternalFormat.R16f, PixelFormat.Red, PixelType.HalfFloat, 1, FormatChannelType.Float, 16) { Type = typeof(Float16) };
			Vector1nb = new Format(PixelInternalFormat.R8, PixelFormat.Red, PixelType.UnsignedByte, 1, FormatChannelType.UnsignedNormalized, 8) { Type = typeof(NormalizedByte) };
			Vector1nsb = new Format((PixelInternalFormat)GL_R8_SNORM, PixelFormat.Red, PixelType.Byte, 1, FormatChannelType.SignedNormalized, 8) { Type = typeof(NormalizedSByte) };

			Vector2h = new Format(PixelInternalFormat.Rg16f, PixelFormat.Red, PixelType.HalfFloat, 2, FormatChannelType.Float, 16);
			Vector2nb = new Format(PixelInternalFormat.Rg8, PixelFormat.Rg, PixelType.UnsignedByte, 2, FormatChannelType.UnsignedNormalized, 8);
			Vector2nsb = new Format(PixelInternalFormat.Rg8Snorm, PixelFormat.Rg, PixelType.Byte, 2, FormatChannelType.SignedNormalized, 8);

			Vector3h = new Format(PixelInternalFormat.Rgb16f, PixelFormat.Red, PixelType.HalfFloat, 3, FormatChannelType.Float, 16);
			Vector3nb = new Format(PixelInternalFormat.Rgb8, PixelFormat.Rgb, PixelType.UnsignedByte, 3, FormatChannelType.UnsignedNormalized, 8);
			Vector3nsb = new Format(PixelInternalFormat.Rgb8Snorm, PixelFormat.Rg, PixelType.Byte, 3, FormatChannelType.SignedNormalized, 8);
			Vector3srgb = new Format(PixelInternalFormat.Srgb8, PixelFormat.Rgb, PixelType.UnsignedByte, 3, FormatChannelType.UnsignedNormalized, 8) { IsSrgb = true };

			Vector4b = new Format(PixelInternalFormat.Rgba8i, PixelFormat.Rgba, PixelType.UnsignedByte, 4, FormatChannelType.UnsignedInteger, 8) { Type = typeof(Vector4b) };
			Vector4h = new Format(PixelInternalFormat.Rgba16f, PixelFormat.Red, PixelType.HalfFloat, 4, FormatChannelType.Float, 16);
			Vector4nb = new Format(PixelInternalFormat.Rgba8, PixelFormat.Rgba, PixelType.UnsignedByte, 4, FormatChannelType.UnsignedNormalized, 8) { Type = typeof(Vector4nb) };
			Vector4nbBGRA = new Format(PixelInternalFormat.Rgba8, PixelFormat.Bgra, PixelType.UnsignedByte, 4, FormatChannelType.UnsignedNormalized, 8) { };
			Vector4nsb = new Format(PixelInternalFormat.Rgba8Snorm, PixelFormat.Rgba, PixelType.Byte, 4, FormatChannelType.SignedNormalized, 8) { Type = typeof(Vector4nsb) };
			Vector4srgba = new Format(PixelInternalFormat.Srgb8Alpha8, PixelFormat.Rgba, PixelType.UnsignedByte, 4, FormatChannelType.UnsignedNormalized, 8) { IsSrgb = true, Type = typeof(Vector4rgba) };

			DXT1 = new Format(PixelInternalFormat.CompressedRgbaS3tcDxt1Ext, PixelFormat.Rgb, PixelType.UnsignedByte, 3, FormatChannelType.CompressedNormalized, 0) { Compression = FormatCompression.DXT1 };
			DXT3 = new Format(PixelInternalFormat.CompressedRgbaS3tcDxt3Ext, PixelFormat.Rgb, PixelType.UnsignedByte, 4, FormatChannelType.CompressedNormalized, 0) { Compression = FormatCompression.DXT3 };
			DXT5 = new Format(PixelInternalFormat.CompressedRgbaS3tcDxt5Ext, PixelFormat.Rgb, PixelType.UnsignedByte, 4, FormatChannelType.CompressedNormalized, 0) { Compression = FormatCompression.DXT5 };

			Format.FinishInit(typeof(TextureFormats));
		}
	}

	/// <summary>This contains all of the valid <see cref="VectorFormats"/>, <see cref="TextureFormats"/>, and <see cref="CommonFormats"/> values.</summary>
	public static class Formats {
		/// <summary>One-dimensional double-precision floating-point vector corresponding to <see cref="Glare.Vector1d"/>. Vector format only.</summary>
		public static Format Vector1d { get { return VectorFormats.Vector1d; } }

		/// <summary>One-dimensional floating-point vector corresponding to <see cref="Glare.Vector1f"/>. Common to all uses.</summary>
		public static Format Vector1f { get { return CommonFormats.Vector1f; } }

		/// <summary>One-dimensional integer vector corresponding to <see cref="Glare.Vector1i"/>. Common to all uses.</summary>
		public static Format Vector1i { get { return CommonFormats.Vector1i; } }

		/// <summary>One-dimensional normalized unsigned byte vector corresponding to <see cref="Glare.Vector1nb"/>. Texture format only.</summary>
		public static Format Vector1nb { get { return TextureFormats.Vector1nb; } }

		/// <summary>One-dimensional normalized signed byte vector corresponding to <see cref="Glare.Vector1nsb"/>. Texture format only.</summary>
		public static Format Vector1nsb { get { return TextureFormats.Vector1nsb; } }

		/// <summary>Two-dimensional double-precision floating-point vector corresponding to <see cref="Glare.Vector2d"/>. Vector format only.</summary>
		public static Format Vector2d { get { return VectorFormats.Vector2d; } }

		/// <summary>Two-dimensional floating-point vector corresponding to <see cref="Glare.Vector2f"/>. Common to all uses.</summary>
		public static Format Vector2f { get { return CommonFormats.Vector2f; } }

		/// <summary>Two-dimensional integer vector corresponding to <see cref="Glare.Vector2i"/>. Common to all uses.</summary>
		public static Format Vector2i { get { return CommonFormats.Vector2i; } }

		/// <summary>Two-dimensional normalized unsigned byte vector corresponding to <see cref="Glare.Vector2nb"/>. Texture format only.</summary>
		public static Format Vector2nb { get { return TextureFormats.Vector2nb; } }

		/// <summary>Two-dimensional normalized signed byte vector corresponding to <see cref="Glare.Vector2nsb"/>. Texture format only.</summary>
		public static Format Vector2nsb { get { return TextureFormats.Vector2nsb; } }

		/// <summary>Three-dimensional double-precision floating-point vector corresponding to <see cref="Glare.Vector3d"/>. Vector format only.</summary>
		public static Format Vector3d { get { return VectorFormats.Vector3d; } }

		/// <summary>Three-dimensional floating-point vector corresponding to <see cref="Glare.Vector3f"/>. Common to all uses.</summary>
		public static Format Vector3f { get { return CommonFormats.Vector3f; } }

		/// <summary>Three-dimensional integer vector corresponding to <see cref="Glare.Vector3i"/>. Common to all uses.</summary>
		public static Format Vector3i { get { return CommonFormats.Vector3i; } }

		/// <summary>Three-dimensional normalized unsigned byte vector corresponding to <see cref="Glare.Vector3nb"/>. Texture format only.</summary>
		public static Format Vector3nb { get { return TextureFormats.Vector3nb; } }

		/// <summary>Three-dimensional normalized signed byte vector corresponding to <see cref="Glare.Vector3nsb"/>. Texture format only.</summary>
		public static Format Vector3nsb { get { return TextureFormats.Vector3nsb; } }

		/// <summary>Four-dimensional double-precision floating-point vector corresponding to <see cref="Glare.Vector4d"/>. Vector format only.</summary>
		public static Format Vector4d { get { return VectorFormats.Vector4d; } }

		/// <summary>Four-dimensional floating-point vector corresponding to <see cref="Glare.Vector4f"/>. Common to all uses.</summary>
		public static Format Vector4f { get { return CommonFormats.Vector4f; } }

		/// <summary>Four-dimensional integer vector corresponding to <see cref="Glare.Vector4i"/>. Common to all uses.</summary>
		public static Format Vector4i { get { return CommonFormats.Vector4i; } }

		/// <summary>Four-dimensional normalized unsigned byte vector corresponding to <see cref="Glare.Vector4nb"/>. Texture format only.</summary>
		public static Format Vector4nb { get { return TextureFormats.Vector4nb; } }

		/// <summary>Four-dimensional normalized signed byte vector corresponding to <see cref="Glare.Vector4nsb"/>. Texture format only.</summary>
		public static Format Vector4nsb { get { return TextureFormats.Vector4nsb; } }
	}

	/// <summary>This contains the list of all valid <see cref="Format"/> values that are common to both <see cref="VectorFormats"/> and <see cref="TextureFormats"/>.</summary>
	public static class CommonFormats {
		public static readonly Format Vector1b, Vector1f, Vector1i, Vector1sb, Vector1s, Vector1us;
		public static readonly Format Vector1ui;
		public static readonly Format Vector2f, Vector2i;
		public static readonly Format Vector2ui;
		public static readonly Format Vector3f, Vector3i;
		public static readonly Format Vector3ui;
		public static readonly Format Vector4f, Vector4i;
		public static readonly Format Vector4ui;

		static CommonFormats() {
			Format.BaseList.AddRange(new Format[]
			{
				Vector1b = new Format(PixelInternalFormat.R8ui, PixelFormat.Red, PixelType.UnsignedByte, 1, FormatChannelType.UnsignedInteger, 8) { Type = typeof(byte) },
				Vector1f = new Format(PixelInternalFormat.R32f, PixelFormat.Red, PixelType.Float, 1, FormatChannelType.Float, 32) { ActiveAttribType = ActiveAttribType.Float, Type = typeof(float) },
				Vector1i = new Format(PixelInternalFormat.R32i, PixelFormat.Red, PixelType.Int, 1, FormatChannelType.SignedInteger, 32) { ActiveAttribType = ActiveAttribType.Int, Type = typeof(int) },
				Vector1s = new Format(PixelInternalFormat.R16i, PixelFormat.Red, PixelType.Short, 1, FormatChannelType.SignedInteger, 16) { Type = typeof(short) },
				Vector1sb = new Format(PixelInternalFormat.R8i, PixelFormat.Red, PixelType.Byte, 1, FormatChannelType.SignedInteger, 8) { Type = typeof(sbyte) },
				Vector1ui = new Format(PixelInternalFormat.R32ui, PixelFormat.Red, PixelType.UnsignedInt, 1, FormatChannelType.UnsignedInteger, 32) { ActiveAttribType = ActiveAttribType.UnsignedInt, Type = typeof(uint) },
				Vector1us = new Format(PixelInternalFormat.R16ui, PixelFormat.Red, PixelType.UnsignedShort, 1, FormatChannelType.UnsignedInteger, 16) { Type = typeof(ushort) },

				Vector2f = new Format(PixelInternalFormat.Rg32f, PixelFormat.Rg, PixelType.Float, 2, FormatChannelType.Float, 32) { ActiveAttribType = ActiveAttribType.FloatVec2, Type = typeof(Vector2f) },
				Vector2i = new Format(PixelInternalFormat.Rg32i, PixelFormat.Rg, PixelType.Int, 2, FormatChannelType.SignedInteger, 32) { ActiveAttribType = ActiveAttribType.IntVec2, Type = typeof(Vector2i) },
				Vector2ui = new Format(PixelInternalFormat.Rg32ui, PixelFormat.Rg, PixelType.UnsignedInt, 2, FormatChannelType.UnsignedInteger, 32) { ActiveAttribType = ActiveAttribType.UnsignedIntVec2, Type = typeof(Vector2ui) },

				Vector3f = new Format(PixelInternalFormat.Rgb32f, PixelFormat.Rgb, PixelType.Float, 3, FormatChannelType.Float, 32) { ActiveAttribType = ActiveAttribType.FloatVec3, Type = typeof(Vector3f) },
				Vector3i = new Format(PixelInternalFormat.Rgb32i, PixelFormat.Rgb, PixelType.Int, 3, FormatChannelType.SignedInteger, 32) { ActiveAttribType = ActiveAttribType.IntVec3, Type = typeof(Vector3i) },
				Vector3ui = new Format(PixelInternalFormat.Rgb32ui, PixelFormat.Rgba, PixelType.UnsignedInt, 3, FormatChannelType.UnsignedInteger, 32) { ActiveAttribType = ActiveAttribType.UnsignedIntVec3, Type = typeof(Vector3ui) },

				Vector4f = new Format(PixelInternalFormat.Rgba32f, PixelFormat.Rgba, PixelType.Float, 4, FormatChannelType.Float, 32) { ActiveAttribType = ActiveAttribType.FloatVec4, Type = typeof(Vector4f) },
				Vector4i = new Format(PixelInternalFormat.Rgba32i, PixelFormat.Rgba, PixelType.Int, 4, FormatChannelType.SignedInteger, 32) { ActiveAttribType = ActiveAttribType.IntVec4, Type = typeof(Vector4i) },
				Vector4ui = new Format(PixelInternalFormat.Rgba32ui, PixelFormat.Rgba, PixelType.UnsignedInt, 4, FormatChannelType.UnsignedInteger, 32) { ActiveAttribType = ActiveAttribType.UnsignedIntVec4, Type = typeof(Vector4ui) },
			});
		}

		/*public static readonly TextureFormat Vector1nsb
		{
			get
			{
				return rnsb ?? (rnsb = new TextureFormat(PixelInternalFormat.r
			}
		}*/
	}
}
