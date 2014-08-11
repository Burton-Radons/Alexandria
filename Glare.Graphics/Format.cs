using Glare.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;
using System.Collections.ObjectModel;
using System.Reflection;
using GL = OpenTK.Graphics.OpenGL4;
using System.Runtime.InteropServices;

namespace Glare.Graphics {
	/// <summary>
	/// Describes a <see cref="Texture"/>'s pixel format.
	/// </summary>
	public class Format {
		internal const PixelType PixelType_Double = (PixelType)(-1);
		internal const PixelType PixelType_Fixed = (PixelType)(-2);

		protected static readonly Dictionary<Type, Format> systemTypes = new Dictionary<Type, Format>();

		static List<Format> list;
		internal static List<Format> BaseList { get { return list ?? (list = new List<Format>()); } }
		internal static readonly Dictionary<ActiveAttribType, Format> ByActiveAttributeType = new Dictionary<ActiveAttribType, Format>();
		
		internal GL.ActiveAttribType ActiveAttribType { get; private set; }

		public FormatChannelCollection Channels { get; private set; }

		/// <summary>Get the number of columns in the matrix.</summary>
		public int Columns { get; private set; }

		/// <summary>Get the type of an element of the matrix.</summary>
		public Type ElementType { get; private set; }

		/// <summary>Get whether the elements of the matrix are an integer (like <c>byte</c>, <c>int</c>, <c>long</c>).</summary>
		public bool IsInteger { get; private set; }

		/// <summary>Get whether the elements of the matrix are normalized to represent a limited range, either from -1 to 1 (if <see cref="IsUnsigned"/> is <c>false</c>) or 0 to 1 (if <see cref="IsUnsigned"/> is true).</summary>
		public bool IsNormalized { get; private set; }

		/// <summary>Get whether the components are in reverse order.</summary>
		public bool IsReversed { get; private set; }

		/// <summary>Get whether this is a valid texture format.</summary>
		public bool IsTextureFormat { get { return PixelFormat.HasValue; } }

		/// <summary>Get whether this is an internal texture storage format.</summary>
		public bool IsTextureInternalFormat { get { return PixelInternalFormat.HasValue; } }

		/// <summary>Get whether this is a valid vector format.</summary>
		public bool IsVectorFormat { get { return VertexAttribPointerType.HasValue; } } 

		/// <summary>Get whether the elements of the matrix are unsigned - if they are, they may be integer (detected with <see cref="IsInteger"/>) or normalized (detected with <see cref="IsNormalized"/>).</summary>
		public bool IsUnsigned { get; private set; }

		/// <summary>Get the number of rows in the matrix.</summary>
		public int Rows { get; private set; }

		/// <summary>Get the system type this corresponds to.</summary>
		public Type SystemType { get; private set; }

		/// <summary>Attempt to get a matching pixel format, or return <c>null</c> if there is none.</summary>
		internal GL.PixelFormat? PixelFormat { get; private set; }

		/// <summary>Attempt to get a matching pixel internal format for operations like <see cref="GL.GL.TexImage2D"/>, or return <c>null</c> if there is none.</summary>
		internal GL.PixelInternalFormat? PixelInternalFormat { get; private set; }

		internal GL.PixelType? PixelType { get; private set; }

		internal GL.VertexAttribPointerType? VertexAttribPointerType { get; private set; }

		public int BitsPerSample {
			get {
				switch (PixelType) {
					case GL.PixelType.UnsignedByte332: return 8;
					case GL.PixelType.UnsignedShort4444: return 16;
					case GL.PixelType.UnsignedShort5551: return 16;
					case GL.PixelType.UnsignedInt8888: return 32;
					case GL.PixelType.UnsignedInt1010102: return 32;
					case GL.PixelType.UnsignedByte233Reversed: return 1;
					case GL.PixelType.UnsignedShort565: return 16;
					case GL.PixelType.UnsignedShort565Reversed: return 16;
					case GL.PixelType.UnsignedShort4444Reversed: return 16;
					case GL.PixelType.UnsignedShort1555Reversed: return 16;
					case GL.PixelType.UnsignedInt8888Reversed: return 32;
					case GL.PixelType.UnsignedInt2101010Reversed: return 32;
					case GL.PixelType.UnsignedInt248: return 32;
					case GL.PixelType.UnsignedInt10F11F11FRev: return 32;
					case GL.PixelType.UnsignedInt5999Rev: return 32;
					case GL.PixelType.Float32UnsignedInt248Rev: return 632;
					default:
						return BitsPerComponent * ComponentCount;
				}
			}
		}

		public int BytesPerSample { get { return (BitsPerSample + 7) / 8; } }

		public int BitsPerComponent { get; protected set; }

		public int ComponentCount {
			get {
				switch (PixelFormat.Value) {
					case GL.PixelFormat.ColorIndex:
					case GL.PixelFormat.DepthComponent:
					case GL.PixelFormat.Red:
					case GL.PixelFormat.Green:
					case GL.PixelFormat.Blue:
					case GL.PixelFormat.Alpha:
					case GL.PixelFormat.Luminance:
					case GL.PixelFormat.RedInteger:
					case GL.PixelFormat.BlueInteger:
					case GL.PixelFormat.AlphaInteger:
						return 1;
					case GL.PixelFormat.LuminanceAlpha:
					case GL.PixelFormat.Rg:
					case GL.PixelFormat.RgInteger:
					case GL.PixelFormat.DepthStencil:
						return 2;
					case GL.PixelFormat.Rgb:
					case GL.PixelFormat.Bgr:
					case GL.PixelFormat.Ycrcb422Sgix:
					case GL.PixelFormat.Ycrcb444Sgix:
					case GL.PixelFormat.RgbInteger:
					case GL.PixelFormat.BgrInteger:
						return 3;
					case GL.PixelFormat.Rgba:
					case GL.PixelFormat.AbgrExt:
					case GL.PixelFormat.CmykExt:
					case GL.PixelFormat.Bgra:
					case GL.PixelFormat.RgbaInteger:
					case GL.PixelFormat.BgraInteger:
						return 4;
					case GL.PixelFormat.CmykaExt:
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
		
		/// <summary>Get whether the RGB components are encoded in the sRGB color space.</summary>
		public bool IsSrgb { get; internal set; }

		/// <summary>Construct a <see cref="Format"/>.</summary>
		/// <param name="elementType">The type of an element.</param>
		/// <param name="systemType">The type of the combined value, or <c>null</c> if there is none. If <paramref name="columns"/> is 1 and <paramref name="rows"/> is one (a scalar), and <paramref name="systemType"/> is <c>null</c>, then it will be assigned the value of <paramref name="elementType"/>. The default is <c>null</c>.</param>
		/// <param name="columns">The number of columns. This is 1 for scalars (with a <paramref name="rows"/> of 1) and vectors (with a <paramref name="rows"/> greater than 1), more than 1 for matrices. The default is <c>1</c>.</param>
		/// <param name="rows">The number of rows. This is 1 (with a <paramref name="column"/> of 1) for scalars, and greater for matrices. The default is <c>1</c>.</param>
		/// <param name="isSrgb">Get whether this is an sRgb color; <paramref name="columns"/> must be 1, and <paramref name="rows"/> must be 3 or 4. The default is <c>false</c>.</param>
		/// <param name="isReversed">Whether the components are reversed.</param>
		/// <param name="compression">What type of compression is in use. In this case <paramref name="elementType"/> and <paramref name="systemType"/> should be <c>null</c>.</param>
		internal Format(Type elementType, Type systemType = null, int columns = 1, int rows = 1, bool isSrgb = false, bool isReversed = false, FormatCompression compression = FormatCompression.None) {
			if (elementType == null && compression == FormatCompression.None)
				throw new ArgumentNullException("elementType");
			if (systemType == null && columns == 1 && rows == 1)
				systemType = elementType;

			ElementType = elementType;
			SystemType = systemType;
			Columns = columns;
			Rows = rows;

			Compression = compression;
			IsInteger = !IsCompressed && IsIntegerSet.Contains(ElementType);
			IsUnsigned = IsCompressed || IsUnsignedSet.Contains(ElementType);
			IsNormalized = IsCompressed || IsNormalizedSet.Contains(ElementType);
			IsReversed = isReversed;
			BitsPerComponent = IsCompressed ? 0 : Marshal.SizeOf(elementType) * 8;

			// Assign GL properties.
			if (!IsCompressed) {
				ActiveAttribType = (GL.ActiveAttribType)GetActiveAttribType(elementType, columns, rows);
				PixelFormat = (GL.PixelFormat?)GetPixelFormat(columns, rows, IsInteger, isReversed);
				PixelInternalFormat = (GL.PixelInternalFormat?)GetPixelInternalFormat(columns, rows, elementType);
				PixelType = (GL.PixelType?)GetPixelType(elementType);
				VertexAttribPointerType = (GL.VertexAttribPointerType?)GetVertexAttribPointerType(elementType);
			} else {
				switch (Compression) {
					case FormatCompression.DXT1:
						PixelInternalFormat = GL.PixelInternalFormat.CompressedRgbS3tcDxt1Ext;
						break;

					case FormatCompression.DXT3:
						PixelInternalFormat = GL.PixelInternalFormat.CompressedRgbaS3tcDxt3Ext;
						break;
						
					case FormatCompression.DXT5:
						PixelInternalFormat = GL.PixelInternalFormat.CompressedRgbaS3tcDxt5Ext;
						break;

					default:
						throw new NotImplementedException();
				}
			}

			if (ActiveAttribType != GL.ActiveAttribType.None)
				ByActiveAttributeType[ActiveAttribType] = this;

			Channels = GetFormatChannels();

			if (systemType != null) {
				Format conflict;
				if(systemTypes.TryGetValue(systemType, out conflict))
					throw new ArgumentException(string.Format("The format elementType={0} systemType={1} size={2}x{3} conflicts with the format elementType={4} size={5}x{6} which has the same systemType.", elementType, systemType, Columns, Rows, conflict.ElementType, conflict.Columns, conflict.Rows), "systemType");
				systemTypes.Add(systemType, this);
			}
		}

#if false

		internal Format(PixelInternalFormat? internalFormat, PixelFormat format, PixelType? type, int rows, int columns, FormatChannelType channelType, int channelBits)
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

		protected Format() {
			list.Add(this);
		}
#endif

		public int AlignedByteSize(int width, int height = 1, int depth = 1, int count = 1) {
			if (IsCompressed) {
				int blockSize = this == Formats.DXT1 ? 8 : 16;
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
				int blockSize = this == Formats.DXT1 ? 8 : 16;
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

		public override string ToString() {
			string text = GetType().Name + "(";
			if (SystemType != null)
				text += SystemType.Name;
			else
				text += PixelInternalFormat;
			return text + ")";
		}

		static readonly FormatChannelIndex[] FormatChannelVectorIndices = new FormatChannelIndex[] { FormatChannelIndex.Red, FormatChannelIndex.Green, FormatChannelIndex.Blue, FormatChannelIndex.Alpha };

		#region Property Calculators

		/// <summary>
		/// Requires <see cref="Rows"/>, <see cref="Columns"/>, <see cref="IsInteger"/>, <see cref="IsNormalized"/>, <see cref="IsUnsigned"/>, <see cref="BitsPerComponent"/>, <see cref="IsReversed"/>.
		/// </summary>
		/// <returns></returns>
		FormatChannelCollection GetFormatChannels() {
			FormatChannel[,] channels = new FormatChannel[Rows, Columns];

			FormatChannelType type;

			if (IsInteger) {
				type = IsUnsigned ? FormatChannelType.UnsignedInteger : FormatChannelType.SignedInteger;
			} else if (IsNormalized) {
				type = IsUnsigned ? FormatChannelType.UnsignedNormalized : FormatChannelType.SignedNormalized;
			} else {
				type = FormatChannelType.Float;
			}

			for (int column = 0; column < Columns; column++) {
				for (int row = 0; row < Rows; row++) {
					FormatChannelIndex channelIndex = Columns == 1 ? FormatChannelVectorIndices[IsReversed ? 3 - row : row] : FormatChannelIndex.Cell;
					FormatChannel channel = new FormatChannel(channelIndex, type, BitsPerComponent) {
						format = this,
						column = column,
						row = row,
					};
					channels[row, column] = channel;
				}
			}

			return new FormatChannelCollection(channels);
		}


		#endregion

		#region GL Property calculators

		#region Dictionaries, tables, and sets

		static readonly HashSet<Type> IsIntegerSet = new HashSet<Type>() { typeof(byte), typeof(sbyte), typeof(short), typeof(ushort), typeof(int), typeof(uint), typeof(long), typeof(ulong) };
		static readonly HashSet<Type> IsNormalizedSet = new HashSet<Type>() { typeof(NormalizedByte), typeof(NormalizedSByte), typeof(NormalizedInt16), typeof(NormalizedInt32) };
		static readonly HashSet<Type> IsUnsignedSet = new HashSet<Type>() { typeof(byte), typeof(ushort), typeof(uint), typeof(ulong), typeof(NormalizedByte) };

		static readonly CodeDictionary<int, int, ActiveAttribType> ActiveAttributeValues = new CodeDictionary<int, int, ActiveAttribType>() {
			// Scalars and vectors
			{ 1, 1, ActiveAttribType.Float, ActiveAttribType.Double, ActiveAttribType.Int, ActiveAttribType.UnsignedInt },
			{ 1, 2, ActiveAttribType.FloatVec2, ActiveAttribType.DoubleVec2, ActiveAttribType.IntVec2, ActiveAttribType.UnsignedIntVec2 },
			{ 1, 3, ActiveAttribType.FloatVec3, ActiveAttribType.DoubleVec3, ActiveAttribType.IntVec3, ActiveAttribType.UnsignedIntVec3 },
			{ 1, 4, ActiveAttribType.FloatVec4, ActiveAttribType.DoubleVec4, ActiveAttribType.IntVec4, ActiveAttribType.UnsignedIntVec4 },

			{ 2, 2, ActiveAttribType.FloatMat2, ActiveAttribType.DoubleMat2 },
			{ 2, 3, ActiveAttribType.FloatMat2x3, ActiveAttribType.DoubleMat2x3 },
			{ 2, 4, ActiveAttribType.FloatMat2x3, ActiveAttribType.DoubleMat2x4 },

			{ 3, 2, ActiveAttribType.FloatMat3x2, ActiveAttribType.DoubleMat3x2 },
			{ 3, 3, ActiveAttribType.FloatMat3, ActiveAttribType.DoubleMat3 },
			{ 3, 4, ActiveAttribType.FloatMat3x4, ActiveAttribType.DoubleMat3x4 },

			{ 4, 2, ActiveAttribType.FloatMat4x2, ActiveAttribType.DoubleMat4x2 },
			{ 4, 3, ActiveAttribType.FloatMat4x3, ActiveAttribType.DoubleMat4x3 },
			{ 4, 4, ActiveAttribType.FloatMat4, ActiveAttribType.DoubleMat4 },
		};

		/* Generic:
		One = 1, Two = 2, Three = 3, Four = 4,
		DepthComponent = 6402,
		Alpha = 6406,
		Rgb = 6407, Rgba = 6408,
		Luminance = 6409, LuminanceAlpha = 6410,
		CompressedAlpha = 34025,
		CompressedLuminance = 34026,
		CompressedLuminanceAlpha = 34027,
		CompressedIntensity = 34028,
		CompressedRgb = 34029,
		CompressedRgba = 34030,
		DepthStencil = 34041,
		 */

		/* Specific:
		R3G3B2 = 10768, Rgb2Ext = 32846, Rgb4 = 32847, Rgb5 = 32848, Rgb10 = 32850, Rgb12 = 32851, R11fG11fB10f = 35898, Rgb9E5 = 35901,
		Rgba2 = 32853, Rgba4 = 32854, Rgb5A1 = 32855, Rgb10A2 = 32857, Rgba12 = 32858, Rgb10A2ui = 36975,
		 * 
		Purposeful:
		DepthComponent16 = 33189, DepthComponent24 = 33190, DepthComponent24Sgix = 33190, DepthComponent32Sgix = 33191, DepthComponent32 = 33191, DepthComponent32f = 36012,
		CompressedRed = 33317, CompressedRg = 33318,
		Depth24Stencil8 = 35056, Depth32fStencil8 = 36013,

		Srgb = 35904, Srgb8 = 35905, SrgbAlpha = 35906, Srgb8Alpha8 = 35907,
		SluminanceAlpha = 35908, Sluminance8Alpha8 = 35909, Sluminance = 35910, Sluminance8 = 35911,
		CompressedSrgb = 35912, CompressedSrgbAlpha = 35913, CompressedSluminance = 35914, CompressedSluminanceAlpha = 35915,
		CompressedSrgbS3tcDxt1Ext = 35916, CompressedSrgbAlphaS3tcDxt1Ext = 35917, CompressedSrgbAlphaS3tcDxt3Ext = 35918, CompressedSrgbAlphaS3tcDxt5Ext = 35919,
		 * 
		 * Extensions:
		DualAlpha4Sgis = 33040, DualAlpha8Sgis = 33041, DualAlpha12Sgis = 33042, DualAlpha16Sgis = 33043,
		DualLuminance4Sgis = 33044, DualLuminance8Sgis = 33045, DualLuminance12Sgis = 33046, DualLuminance16Sgis = 33047,
		DualIntensity4Sgis = 33048, DualIntensity8Sgis = 33049, DualIntensity12Sgis = 33050, DualIntensity16Sgis = 33051,
		DualLuminanceAlpha4Sgis = 33052, DualLuminanceAlpha8Sgis = 33053,
		QuadAlpha4Sgis = 33054, QuadAlpha8Sgis = 33055,
		QuadLuminance4Sgis = 33056, QuadLuminance8Sgis = 33057,
		QuadIntensity4Sgis = 33058, QuadIntensity8Sgis = 33059,
		DepthComponent16Sgix = 33189,
		CompressedRgbS3tcDxt1Ext = 33776, CompressedRgbaS3tcDxt1Ext = 33777, CompressedRgbaS3tcDxt3Ext = 33778, CompressedRgbaS3tcDxt5Ext = 33779,
		RgbIccSgix = 33888, RgbaIccSgix = 33889,
		AlphaIccSgix = 33890,
		LuminanceIccSgix = 33891,
		IntensityIccSgix = 33892,
		LuminanceAlphaIccSgix = 33893,
		R5G6B5IccSgix = 33894,
		R5G6B5A8IccSgix = 33895,
		Alpha16IccSgix = 33896,
		Luminance16IccSgix = 33897,
		Intensity16IccSgix = 33898,
		Luminance16Alpha8IccSgix = 33899,
		Float32UnsignedInt248Rev = 36269,
		CompressedRedRgtc1 = 36283, CompressedSignedRedRgtc1 = 36284, CompressedRgRgtc2 = 36285, CompressedSignedRgRgtc2 = 36286, CompressedRgbaBptcUnorm = 36492, CompressedRgbBptcSignedFloat = 36494, CompressedRgbBptcUnsignedFloat = 36495,
		 * */

		static readonly CodeDictionary<Type, PixelInternalFormat> PixelInternalFormatValues = new CodeDictionary<Type, PixelInternalFormat>() {
			{ typeof(Byte), GL.PixelInternalFormat.R8ui, GL.PixelInternalFormat.Rg8ui, GL.PixelInternalFormat.Rgb8ui, GL.PixelInternalFormat.Rgba8ui },
			// Double does not have any entries
			{ typeof(Float16), GL.PixelInternalFormat.R16f, GL.PixelInternalFormat.Rg16f, GL.PixelInternalFormat.Rgb16f, GL.PixelInternalFormat.Rgba16f },
			{ typeof(Int16), GL.PixelInternalFormat.R16i, GL.PixelInternalFormat.Rg16i, GL.PixelInternalFormat.Rgb16i, GL.PixelInternalFormat.Rgba16i },
			{ typeof(Int32), GL.PixelInternalFormat.R32i, GL.PixelInternalFormat.Rg32i, GL.PixelInternalFormat.Rgb32i, GL.PixelInternalFormat.Rgba32i },
			// Int64 does not have any entries
			{ typeof(NormalizedByte), GL.PixelInternalFormat.R8, GL.PixelInternalFormat.Rg8, GL.PixelInternalFormat.Rgb8, GL.PixelInternalFormat.Rgba8 },
			// NormalizedInt16 does not have any entries
			// NormalizedInt32 does not have any entries
			{ typeof(NormalizedSByte), GL.PixelInternalFormat.R8Snorm, GL.PixelInternalFormat.Rg8Snorm, GL.PixelInternalFormat.Rgb8Snorm, GL.PixelInternalFormat.Rgba8Snorm },
			{ typeof(SByte), GL.PixelInternalFormat.R8i, GL.PixelInternalFormat.Rg8ui, GL.PixelInternalFormat.Rgb8ui, GL.PixelInternalFormat.Rgba8ui },
			{ typeof(Single), GL.PixelInternalFormat.R32f, GL.PixelInternalFormat.Rg32f, GL.PixelInternalFormat.Rgb32f, GL.PixelInternalFormat.Rgba32f },
			{ typeof(UInt16), GL.PixelInternalFormat.R16ui, GL.PixelInternalFormat.Rg16ui, GL.PixelInternalFormat.Rgb16ui, GL.PixelInternalFormat.Rgba16ui },
			{ typeof(UInt32), GL.PixelInternalFormat.R32ui, GL.PixelInternalFormat.Rg32ui, GL.PixelInternalFormat.Rgb32ui, GL.PixelInternalFormat.Rgba32ui },
			// UInt64 does not have any entries.
		};

		static readonly Dictionary<Type, PixelType> PixelTypeDictionary = new Dictionary<Type, PixelType>() {
			{ typeof(SByte), GL.PixelType.Byte },
			{ typeof(Byte), GL.PixelType.UnsignedByte },
			{ typeof(Int16), GL.PixelType.Short },
			{ typeof(UInt16), GL.PixelType.UnsignedShort },
			{ typeof(Int32), GL.PixelType.Int },
			{ typeof(UInt32), GL.PixelType.UnsignedInt },
			{ typeof(Single), GL.PixelType.Float },
			{ typeof(Float16), GL.PixelType.HalfFloat },
			{ typeof(NormalizedByte), GL.PixelType.UnsignedByte },
			{ typeof(NormalizedSByte), GL.PixelType.Byte },
			{ typeof(NormalizedInt16), GL.PixelType.Short },
			{ typeof(NormalizedInt32), GL.PixelType.Int },

			/* UnsignedByte332Ext = 32818, UnsignedByte332 = 32818,
			UnsignedShort4444Ext = 32819, UnsignedShort4444 = 32819, UnsignedShort5551Ext = 32820, UnsignedShort5551 = 32820,
			UnsignedInt8888 = 32821, UnsignedInt8888Ext = 32821, UnsignedInt1010102 = 32822, UnsignedInt1010102Ext = 32822,
			UnsignedByte233Reversed = 33634,
			UnsignedShort565 = 33635,
			UnsignedShort565Reversed = 33636 UnsignedShort4444Reversed = 33637, UnsignedShort1555Reversed = 33638, UnsignedInt8888Reversed = 33639,
			UnsignedInt2101010Reversed = 33640,
			UnsignedInt248 = 34042,
			UnsignedInt10F11F11FRev = 35899,
			UnsignedInt5999Rev = 35902,
			Float32UnsignedInt248Rev = 36269,*/
			// No entries for Int64, UInt64, or Double
		};

		static readonly Dictionary<Type, VertexAttribPointerType> VertexAttribPointerTypeDictionary = new Dictionary<Type, VertexAttribPointerType>() {
			{ typeof(Byte), GL.VertexAttribPointerType.UnsignedByte },
			{ typeof(SByte), GL.VertexAttribPointerType.Byte },
			{ typeof(Int16), GL.VertexAttribPointerType.Short },
			{ typeof(UInt16), GL.VertexAttribPointerType.UnsignedShort },
			{ typeof(Int32), GL.VertexAttribPointerType.Int },
			{ typeof(UInt32), GL.VertexAttribPointerType.UnsignedInt },
			{ typeof(Single), GL.VertexAttribPointerType.Float },
			{ typeof(Double), GL.VertexAttribPointerType.Double },
			{ typeof(Float16), GL.VertexAttribPointerType.HalfFloat },
			{ typeof(NormalizedByte), GL.VertexAttribPointerType.UnsignedByte },
			{ typeof(NormalizedInt16), GL.VertexAttribPointerType.Short },
			{ typeof(NormalizedInt32), GL.VertexAttribPointerType.Int },
			{ typeof(NormalizedSByte), GL.VertexAttribPointerType.Byte },

			/*Fixed = 5132,
			UnsignedInt2101010Rev = 33640,
			Int2101010Rev = 36255,*/
		};

		#endregion Tables, dictionaries, and sets

		static ActiveAttribType GetActiveAttribType(Type elementType, int columns, int rows) {
			int typeIndex;

			if (elementType == typeof(Single)) typeIndex = 0;
			else if (elementType == typeof(Double)) typeIndex = 1;
			else if (elementType == typeof(Int32)) typeIndex = 2;
			else if (elementType == typeof(UInt32)) typeIndex = 3;
			else return ActiveAttribType.None;

			return ActiveAttributeValues.Get(columns, rows, typeIndex).GetValueOrDefault(ActiveAttribType.None);
		}

		static PixelFormat? GetPixelFormat(int columns, int rows, bool isInteger, bool isReversed) {
			if (columns == 1) {
				switch (rows) {
					case 1: return isInteger ? GL.PixelFormat.RedInteger : GL.PixelFormat.Red;
					case 2: return isInteger ? GL.PixelFormat.RgInteger : GL.PixelFormat.Rg;
					case 3: return isInteger ? (isReversed ? GL.PixelFormat.BgrInteger : GL.PixelFormat.RgbInteger) : (isReversed ? GL.PixelFormat.Bgr : GL.PixelFormat.Rgb);
					case 4: return isInteger ? (isReversed ? GL.PixelFormat.BgraInteger : GL.PixelFormat.RgbaInteger) : (isReversed ? GL.PixelFormat.Bgra : GL.PixelFormat.Rgba);
					default: return null;
				}
			}
			return null;
		}

		static PixelInternalFormat? GetPixelInternalFormat(int columns, int rows, Type elementType) {
			if (columns != 1)
				return null;
			return PixelInternalFormatValues.Get(elementType, rows - 1);
		}

		static PixelType? GetPixelType(Type elementType) {
			if (elementType == null)
				return null;
			PixelType type;
			return PixelTypeDictionary.TryGetValue(elementType, out type) ? type : (PixelType?)null; 
		}

		static VertexAttribPointerType? GetVertexAttribPointerType(Type elementType) { return VertexAttribPointerTypeDictionary.TryGetValueOrNull(elementType); }

		class CodeDictionary<TCode1, TList> : Dictionary<TCode1, TList[]> where TList : struct {
			public new void Add(TCode1 code, params TList[] list) { base.Add(code, list); }

			public TList? Get(TCode1 code, int listIndex) {
				TList[] list;
				if (!this.TryGetValue(code, out list))
					return null;
				if (listIndex < 0 || listIndex >= list.Length)
					return null;
				return list[listIndex];
			}
		}

		class CodeDictionary<TCode1, TCode2, TList> : Dictionary<TCode1, Dictionary<TCode2, TList[]>> where TList : struct {
			public void Add(TCode1 code1, TCode2 code2, params TList[] list) {
				Dictionary<TCode2, TList[]> dictionary2 = this.TryGetValue(code1);

				if (dictionary2 == null)
					this[code1] = dictionary2 = new Dictionary<TCode2, TList[]>(1);
				dictionary2[code2] = list;
			}

			public TList? Get(TCode1 code1, TCode2 code2, int listIndex) {
				Dictionary<TCode2, TList[]> dictionary2 = this.TryGetValue(code1);
				if (dictionary2 == null)
					return null;
				TList[] list;
				if (!dictionary2.TryGetValue(code2, out list))
					return null;
				if (listIndex < 0 || listIndex >= list.Length)
					return null;
				return list[listIndex];
			}
		}

		#endregion Property calculators
	}

	/// <summary>A two-dimensional collection of <see cref="FormatChannel"/>s, indexed by <c>[row][column]</c>.</summary>
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
}
