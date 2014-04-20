using Glare.Framework;
using Glare.Graphics;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Assets.Formats {
	/// <summary>
	/// DirectDraw Surface Format.
	/// </summary>
	public class DdsFormat : AssetFormat {
		public const string Magic = "DDS\x20";

		public const int HeaderSize = 124;

		[Flags]
		public enum Caps {
			None = 0,

			/// <summary>Optional; must be used on any file that contains more than one surface (a mipmap, a cubic environment map, or mipmapped volume texture).</summary>
			Complex = 0x8,

			/// <summary>Optional; should be used for a mipmap.</summary>
			MipMap = 0x400000,

			/// <summary>Required.</summary>
			Texture = 0x1000,
		}

		[Flags]
		public enum Caps2 {
			None = 0,

			/// <summary>Required for a cube map.</summary>
			CubeMap = 0x200,

			/// <summary>Required when these surfaces are stored in a cube map.</summary>
			CubeMapPositiveX = 0x400,

			/// <summary>Required when these surfaces are stored in a cube map.</summary>
			CubeMapNegativeX = 0x800,

			/// <summary>Required when these surfaces are stored in a cube map.</summary>
			CubeMapPositiveY = 0x1000,

			/// <summary>Required when these surfaces are stored in a cube map.</summary>
			CubeMapNegativeY = 0x2000,

			/// <summary>Required when these surfaces are stored in a cube map.</summary>
			CubeMapPositiveZ = 0x4000,

			/// <summary>Required when these surfaces are stored in a cube map.</summary>
			CubeMapNegativeZ = 0x8000,

			/// <summary>Required for a volume texture.</summary>
			Volume = 0x200000,
		}

		[Flags]
		public enum Flags {
			Caps = 1,
			Height = 2,
			Width = 4,
			Pitch = 8,
			PixelFormat = 0x1000,
			MipMapCount = 0x20000,
			LinearSize = 0x80000,
			Depth = 0x800000,
		}

		[Flags]
		public enum PixelFormatFlags : int {
			/// <summary>Texture contains alpha data; RGBAlphaBitMask contains valid data.</summary>
			AlphaPixels = 0x1,

			/// <summary>Used in some older DDS files for alpha channel only uncompressed data (RGBBitCount contains the alpha channel bitcount; ABitMask contains valid data)</summary>
			Alpha = 0x2,

			/// <summary>Texture contains compressed RGB data; FourCC contains valid data.</summary>
			FourCC = 0x4,

			/// <summary>Texture contains uncompressed RGB data; RGBBitCount and the RGB masks (RBitMask, GBitMask, BBitMask) contain valid data.</summary>
			RGB = 0x40,

			/// <summary>Used in some older DDS files for YUV uncompressed data (RGBBitCount contains the YUV bit count; RBitMask contains the Y mask, GBitMask contains the U mask, BBitMask contains the V mask).</summary>
			YUV = 0x200,

			/// <summary>Used in some older DDS files for single channel color uncompressed data (RGBBitCount contains the luminance channel bit count; RBitMask contains the channel mask). Can be combined with <see cref="AlphaPixels"/> for a two channel DDS file.</summary>
			Luminance = 0x20000,
		}

		public struct PixelFormat {
			public const int Size = 32;

			public PixelFormatFlags Flags;
			public string FourCC;
			public int RGBBitCount;
			public int RBitMask, GBitMask, BBitMask, ABitMask;

			public bool HasAlphaPixels { get { return (Flags & PixelFormatFlags.AlphaPixels) != 0; } }
			public bool HasFourCC { get { return (Flags & PixelFormatFlags.FourCC) != 0; } }
			public bool HasRGB { get { return (Flags & PixelFormatFlags.RGB) != 0; } }

			public PixelFormat(BinaryReader reader) {
				int size = reader.ReadInt32();
				if (size != Size)
					throw new InvalidDataException();
				Flags = (PixelFormatFlags)reader.ReadInt32();
				var fourcc = reader.ReadBytes(4);
				FourCC = Encoding.ASCII.GetString(fourcc);
				RGBBitCount = reader.ReadInt32();
				RBitMask = reader.ReadInt32();
				GBitMask = reader.ReadInt32();
				BBitMask = reader.ReadInt32();
				ABitMask = reader.ReadInt32();
			}

			public PixelFormat(Format format) {
				if (format.IsCompressed) {
					Flags = PixelFormatFlags.FourCC;
					FourCC = Formats[format];

					RGBBitCount = 0;
					RBitMask = GBitMask = BBitMask = ABitMask = 0;
				} else
					throw new NotImplementedException();
			}

			public bool MatchMasks(int rMask, int gMask, int bMask) {
				return HasRGB && !HasAlphaPixels && RBitMask == rMask && GBitMask == gMask && BBitMask == bMask;
			}

			public bool MatchMasks(int rMask, int gMask, int bMask, int aMask) {
				return HasRGB && HasAlphaPixels && RBitMask == rMask && GBitMask == gMask && BBitMask == bMask && ABitMask == aMask;
			}

			public void Write(BinaryWriter writer) {
				writer.Write(Size);
				writer.Write((int)Flags);
				for (int i = 0; i < 4; i++)
					writer.Write((byte)FourCC[i]);
				writer.Write(RGBBitCount);
				writer.Write(RBitMask);
				writer.Write(GBitMask);
				writer.Write(BBitMask);
				writer.Write(ABitMask);
			}
		}

		public const Flags RequiredFlags = Flags.Caps | Flags.Height | Flags.Width | Flags.PixelFormat;

		public static readonly Bidictionary<string, Format> Formats = new Bidictionary<string, Format>() {
			{ "DXT1", TextureFormats.DXT1 },
			{ "DXT3", TextureFormats.DXT3 },
			{ "DXT5", TextureFormats.DXT5 },
		};

		internal DdsFormat(DefaultPlugin plugin)
			: base(plugin, typeof(Texture), canLoad: true, canSave: true, extension: ".dds") {
		}

		public override LoadMatchStrength LoadMatch(AssetLoader context) {
			return context.Reader.MatchMagic(Magic) ? LoadMatchStrength.Medium : LoadMatchStrength.None;
		}

		public override Asset Load(AssetLoader context) {
			using (BinaryReader reader = context.Reader) {
				reader.RequireMagic(Magic);

				int headerSize = reader.ReadInt32();
				if (headerSize != HeaderSize)
					throw new InvalidDataException();
				Flags flags = (Flags)reader.ReadInt32();
				if ((flags & RequiredFlags) != RequiredFlags)
					throw new InvalidDataException();
				int height = reader.ReadInt32();
				int width = reader.ReadInt32();
				int pitchOrLinearSize = reader.ReadInt32();
				int depth = reader.ReadInt32();
				int mipMapCount = reader.ReadInt32();
				reader.BaseStream.Seek(4 * 11, SeekOrigin.Current); // Reserved
				var pixelFormat = new PixelFormat(reader);
				Caps caps = (Caps)reader.ReadInt32(); // Surface complexity
				if ((caps & Caps.Texture) == 0)
					throw new InvalidDataException();
				Caps2 caps2 = (Caps2)reader.ReadInt32(); // Surface type
				int caps3 = reader.ReadInt32();
				int caps4 = reader.ReadInt32();
				int reserved2 = reader.ReadInt32();

				Format format = null;

				if (pixelFormat.HasFourCC) {
					if (pixelFormat.FourCC == "DX10")
						throw new NotSupportedException();
					format = Formats[pixelFormat.FourCC];
				} else if (pixelFormat.HasRGB) {
					int b0 = 255, b1 = 255 << 8, b2 = 255 << 16, b3 = 255 << 24;

					if (pixelFormat.HasAlphaPixels && pixelFormat.RGBBitCount == 32) {
						if (pixelFormat.MatchMasks(b2, b1, b0, b3))
							format = TextureFormats.Vector4nbBGRA;
						else if (pixelFormat.MatchMasks(b0, b1, b2, b3))
							format = TextureFormats.Vector4nb;
					}
				}

				if (format == null)
					throw new NotSupportedException();

				if (caps2 != Caps2.None)
					throw new NotSupportedException("Cube maps or volume textures not supported.");

				int linearSize;

				if ((flags & Flags.Pitch) != 0)
					linearSize = pitchOrLinearSize * height;
				else if ((flags & Flags.LinearSize) != 0)
					linearSize = pitchOrLinearSize;
				else
					linearSize = format.AlignedByteSize(new Vector2i(width, height));

				byte[] data = new byte[linearSize];

				if ((flags & Flags.MipMapCount) == 0)
					mipMapCount = 1;

				Texture2D texture = new Texture2D();//format, new Vector2i(width, height));
				for (int level = 0; level < mipMapCount; level++) {
					if (reader.Read(data, 0, linearSize) != linearSize)
						throw new InvalidDataException();
					texture.Surface.Levels[level].DataCompressed(format, new Vector2i(width, height), data);

					width = Math.Max(1, (width + 1) / 2);
					height = Math.Max(1, (height + 1) / 2);
					linearSize = format.AlignedByteSize(width, height);
				}

				texture.Name = context.Name;
				return new TextureAsset(Manager, texture, context.Name);
			}
		}

		public override SaveCheckResult SaveCheck(Asset resource) {
			if(resource is TextureAsset)
				return new SaveCheckResult(true);
			return SaveCheckResult.False;
		}

		public override void Save(Asset resource, BinaryWriter writer) {
			base.Save(resource, writer);
		}

		public void Save(Texture texture, BinaryWriter writer) {
			Texture2D texture2d = texture as Texture2D;
			Vector3i dimensions;
			int pitchOrLinearSize;
			Flags flags = RequiredFlags | Flags.MipMapCount;
			int mipMapCount;

			dimensions = new Vector3i(texture2d.Dimensions, 1);
			for (mipMapCount = 0; texture2d.Levels[mipMapCount].Dimensions.Sum != 0; mipMapCount++) ;

			if (texture.Format.IsCompressed) {
				pitchOrLinearSize = texture.Format.AlignedByteSize(dimensions);
				flags |= Flags.LinearSize;
			} else {
				pitchOrLinearSize = texture.Format.AlignedBytePitch(dimensions.X);
				flags |= Flags.Pitch;
			}

			for (int index = 0; index < Magic.Length; index++)
				writer.Write((byte)Magic[index]);
			writer.Write(HeaderSize);
			writer.Write((int)flags);
			writer.Write(dimensions.X);
			writer.Write(dimensions.Y);
			writer.Write(pitchOrLinearSize);
			writer.Write(dimensions.Z);
			writer.Write(mipMapCount);
			for (int i = 0; i < 11; i++)
				writer.Write(0); // Reserved

			var pixelFormat = new PixelFormat(texture.Format);
			pixelFormat.Write(writer);

			writer.Write((int)(Caps.Texture));
			writer.Write((int)(Caps2.None));
			writer.Write(0); // DDSCaps3, reserved
			writer.Write(0); // DDSCaps4, reserved
			writer.Write(0); // Reserved

			byte[] data = new byte[texture.Format.AlignedByteSize(dimensions)];
			for (int level = 0; level < mipMapCount; level++) {
				TextureLevel textureLevel = texture2d.Surface.Levels[level];

				textureLevel.Read(data, 0, texture.Format);
				int size = texture.Format.AlignedByteSize(textureLevel.Dimensions);
				writer.Write(data, 0, size);
			}
#if false
				Texture2D texture = new Texture2D();//format, new Vector2i(width, height));
				for (int level = 0; level < mipMapCount; level++) {
					if (reader.Read(data, 0, linearSize) != linearSize)
						throw new InvalidDataException();
					texture.Surface.Levels[level].DataCompressed(format, new Vector2i(width, height), data);

					width = (width + 1) / 2;
					height = (height + 1) / 2;
					linearSize = format.AlignedByteSize(width, height);
				}

				texture.Name = path;
				return texture;
#endif
		}
	}
}
