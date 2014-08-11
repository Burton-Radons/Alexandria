using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Alexandria.Platforms.NintendoDS {
	/// <summary>
	/// Extension methods.
	/// </summary>
	public static class Extensions {
		/// <summary>Decompress LZ77-compressed data.</summary>
		/// <param name="reader"></param>
		/// <param name="inputEnd"></param>
		/// <param name="outputSize"></param>
		/// <param name="acceptInputOverflow"></param>
		/// <param name="ffEscape"></param>
		/// <returns></returns>
		public static byte[] DecompressLZ77(this BinaryReader reader, long inputEnd, int outputSize, bool acceptInputOverflow = false, bool ffEscape = false) {
			byte[] output = new byte[outputSize];
			int outputOffset = 0;
			Stream stream = reader.BaseStream;

			while (outputOffset < outputSize) {
				if (stream.Position >= inputEnd) {
					if (acceptInputOverflow) {
						byte[] slice = new byte[outputOffset];
						Array.Copy(output, slice, outputOffset);
						return slice;
					}

					throw new OverflowException(String.Format("Input overflow at inputOffset {0}, for outputOffset {1} of outputSize {2}.", stream.Position, outputOffset, outputSize));
				}

				var flags = reader.ReadByte();
				if (ffEscape && flags == 0xFF) {
					while (stream.Position < inputEnd && outputOffset < outputSize && (flags = reader.ReadByte()) != 0xFF)
						output[outputOffset++] = flags;
				} else {
					for (int flagBit = 7; flagBit >= 0 && outputOffset < outputSize && stream.Position < inputEnd; flagBit--) {
						if ((flags & (1 << flagBit)) != 0) {
							if (stream.Position + 2 > inputEnd) {
								reader.ReadByte();
								break;
							}

							var code = reader.ReadUInt16();
							var displacement = (((code & 15) << 8) | (code >> 8)) + 1;
							var length = ((code >> 4) & 15) + 3;

							for (int index = 0; index < length && outputOffset < outputSize; index++, outputOffset++)
								output[outputOffset] = (outputOffset - displacement >= 0) ? output[outputOffset - displacement] : (byte)0;
						} else
							output[outputOffset++] = reader.ReadByte();
					}
				}
			}

			return output;
		}

		/// <summary>Read a header and decompress data.</summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public static byte[] DecompressData(this BinaryReader reader, long inputEnd) {
			int header = reader.ReadInt32();
			var headerCompression = (header >> 4) & 15;
			var outputSize = (header >> 8);

			if (headerCompression != 1)
				throw new Exception("Unknown compression found in LZ77-compressed block.");
			return reader.DecompressLZ77(inputEnd, outputSize);
		}

		/// <summary>Read a two-byte color code for the Nintendo DS. They are stored as unsigned shorts, 5 bits per component, with an optional alpha bit.</summary>
		/// <param name="reader">What to read the color from.</param>
		/// <param name="useAlpha">Whether to use the alpha bit. The default is <c>false</c>.</param>
		/// <param name="forceTransparent">Whether to force a transparent color. The default is <c>false</c>.</param>
		/// <returns></returns>
		public static Color ReadNDSColor(this BinaryReader reader, bool useAlpha = false, bool forceTransparent = false) {
			ushort value = reader.ReadUInt16();
			int alpha = forceTransparent ? 0 : useAlpha ? (value >> 15) * 255 : 255;
			int red = (value & 31) * 255 / 31;
			int green = ((value >> 5) & 31) * 255 / 31;
			int blue = ((value >> 10) & 31) * 255 / 31;
			return Color.FromArgb(alpha, red, green, blue);
		}
	}
}
