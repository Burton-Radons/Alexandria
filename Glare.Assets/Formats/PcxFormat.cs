using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Glare.Assets.Formats {
	/// <summary>
	/// Format handler for the PCX graphics file format.
	/// </summary>
	public class PcxFormat : AssetFormat {
		const byte PaletteMagic = 0x0C;

		struct PcxHeader {
			public const int ByteSize = 128;
			public const byte RequiredSignature = 0x0A;

			public byte Signature;
			public byte Version;
			public PcxCompression Compression;
			public byte BitsPerPixel;
			public ushort MinimumX;
			public ushort MinimumY;
			public ushort MaximumX;
			public ushort MaximumY;
			public ushort DpiX;
			public ushort DpiY;
			byte Zero;
			public byte ColorPlaneCount;
			public ushort BytesPerScanLine;
			public PcxColorTableType ColorTableType;


			public int SizeX { get { return MaximumY - MinimumY + 1; } }

			public int SizeY { get { return MaximumX - MinimumX + 1; } }

			public bool IsValid { get { return
				Signature == RequiredSignature &&
				Version <= 5 &&
				BitsPerPixel > 0 && BitsPerPixel <= 8 &&
				typeof(PcxCompression).IsEnumDefined(Compression) &&
				SizeY > 0 && SizeX > 0; } }

			public PcxHeader(BinaryReader reader) {
				Signature = reader.ReadByte();
				Version = reader.ReadByte();
				Compression = (PcxCompression)reader.ReadByte();
				BitsPerPixel = reader.ReadByte();
				MinimumX = reader.ReadUInt16();
				MinimumY = reader.ReadUInt16();
				MaximumX = reader.ReadUInt16();
				MaximumY = reader.ReadUInt16();
				DpiX = reader.ReadUInt16();
				DpiY = reader.ReadUInt16();
				reader.BaseStream.Seek(48, SeekOrigin.Current); // Palette data.
				Zero = reader.ReadByte();
				ColorPlaneCount = reader.ReadByte();
				BytesPerScanLine = reader.ReadUInt16();
				ColorTableType = (PcxColorTableType)reader.ReadUInt16();
				reader.BaseStream.Seek(58, SeekOrigin.Current); // Filler
			}
		}

		enum PcxCompression : byte {
			None = 0,
			RLE = 1,
		}

		enum PcxColorTableType : ushort {
			ColorOrBlackAndWhite = 1,
			Grayscale = 2,
		}

		internal PcxFormat(DefaultPlugin plugin)
			: base(plugin, typeof(TextureAsset), canLoad: true, extension: ".pcx") {
		}

		/// <summary>Matches based on the 128-byte header.</summary>
		/// <param name="loader"></param>
		/// <returns></returns>
		public override LoadMatchStrength LoadMatch(AssetLoader loader) {
			if (loader.Length < PcxHeader.ByteSize)
				return LoadMatchStrength.None;
			PcxHeader header = new PcxHeader(loader.Reader);
			if (!header.IsValid)
				return LoadMatchStrength.None;
			return LoadMatchStrength.Strong;
		}

		/// <summary>Load the PCX image file.</summary>
		/// <param name="loader"></param>
		/// <returns></returns>
		public override Asset Load(AssetLoader loader) {
			BinaryReader reader = loader.Reader;
			PcxHeader header = new PcxHeader(reader);

			int pitch = (header.BitsPerPixel * header.ColorPlaneCount + 7) / 8 * header.SizeX;
			int[] data = new int[pitch * header.SizeY + 1];

			for (int row = 0; row < header.SizeY; row++) {
				switch (header.Compression) {
					case PcxCompression.None:
						for (int index = 0; index < pitch; index++)
							data[pitch * row + index] = reader.ReadByte();
						break;

					case PcxCompression.RLE:
						for (int offset = pitch * row, end = offset + header.BytesPerScanLine; offset < end; ) {
							byte value = reader.ReadByte();

							if (value < 0xC0) {
								data[offset++] = value;
							} else {
								int runEnd = offset + value - 0xC0;
								byte code = reader.ReadByte();

								while (offset < runEnd)
									data[offset++] = code;
							}
						}
						break;

					default:
						throw new NotImplementedException();
				}
			}

			PaletteAsset palette = null;

			if(header.ColorPlaneCount == 1) {
				if(reader.BaseStream.ReadByte() == PaletteMagic) {
					palette = PaletteAsset.ReadRgb(Manager, loader.Name + " Palette", reader, 1 << header.BitsPerPixel, 255);
				}
			}

			if(palette != null)
				return new IndexedTextureAsset(loader, palette, header.SizeX, header.SizeY, data, pitch);
			else
				throw new NotImplementedException();
		}
	}
}
