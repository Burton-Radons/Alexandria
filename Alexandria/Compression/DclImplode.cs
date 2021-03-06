﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Compression {
	/// <summary>
	/// Supports the PKWARE DCL Implode format.
	/// </summary>
	public static class DclImplode {
		enum Format : byte {
			Binary = 0,
			Ascii = 1,
		}

		enum Dictionary : byte {
			_1k = 4,
			_2k = 5,
			_4k = 6,
		}

		/// <summary>Size in bits of the copy length codes, as represented in <see cref="LengthCodes"/>.</summary>
		static readonly byte[] LengthBits = new byte[] { 0x3, 0x2, 0x3, 0x3, 0x4, 0x4, 0x4, 0x5, 0x5, 0x5, 0x5, 0x6, 0x6, 0x6, 0x7, 0x7 };

		/// <summary>Bit sequences that indicate the copy length. Each is <see cref="LengthBits"/> in size.</summary>
		static readonly byte[] LengthCodes = new byte[] { 0x5, 0x3, 0x1, 0x6, 0xa, 0x2, 0xc, 0x14, 0x4, 0x18, 0x8, 0x30, 0x10, 0x20, 0x40, 0x0 };

		/// <summary>Base length code value.</summary>
		static readonly int[] LengthBases = new int[] { 0x2, 0x3, 0x4, 0x5, 0x6, 0x7, 0x8, 0x9, 0xa, 0xc, 0x10, 0x18, 0x28, 0x48, 0x88, 0x108 };

		/// <summary>The number of bits stored in a length code that is added to determine the actual value.</summary>
		static readonly byte[] LengthExtraBits = new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x1, 0x2, 0x3, 0x4, 0x5, 0x6, 0x7, 0x8 };

		static readonly byte[] OffsetCodes = new byte[] { 0x3, 0xd, 0x5, 0x19, 0x9, 0x11, 0x1, 0x3e, 0x1e, 0x2e, 0xe, 0x36, 0x16, 0x26, 0x6, 0x3a, 0x1a, 0x2a, 0xa, 0x32, 0x12, 0x22, 0x42, 0x2, 0x7c, 0x3c, 0x5c, 0x1c, 0x6c, 0x2c, 0x4c, 0xc, 0x74, 0x34, 0x54, 0x14, 0x64, 0x24, 0x44, 0x4, 0x78, 0x38, 0x58, 0x18, 0x68, 0x28, 0x48, 0x8, 0xf0, 0x70, 0xb0, 0x30, 0xd0, 0x50, 0x90, 0x10, 0xe0, 0x60, 0xa0, 0x20, 0xc0, 0x40, 0x80, 0x0 };

		static readonly byte[] OffsetBits = new byte[] { 0x2, 0x4, 0x4, 0x5, 0x5, 0x5, 0x5, 0x6, 0x6, 0x6, 0x6, 0x6, 0x6, 0x6, 0x6, 0x6, 0x6, 0x6, 0x6, 0x6, 0x6, 0x6, 0x7, 0x7, 0x7, 0x7, 0x7, 0x7, 0x7, 0x7, 0x7, 0x7, 0x7, 0x7, 0x7, 0x7, 0x7, 0x7, 0x7, 0x7, 0x7, 0x7, 0x7, 0x7, 0x7, 0x7, 0x7, 0x7, 0x8, 0x8, 0x8, 0x8, 0x8, 0x8, 0x8, 0x8, 0x8, 0x8, 0x8, 0x8, 0x8, 0x8, 0x8, 0x8 };

		static readonly short[] CharCodes = new short[] { 0x490, 0xfe0, 0x7e0, 0xbe0, 0x3e0, 0xde0, 0x5e0, 0x9e0, 0x1e0, 0xb8, 0x62, 0xee0, 0x6e0, 0x22, 0xae0, 0x2e0, 0xce0, 0x4e0, 0x8e0, 0xe0, 0xf60, 0x760, 0xb60, 0x360, 0xd60, 0x560, 0x1240, 0x960, 0x160, 0xe60, 0x660, 0xa60, 0xf, 0x250, 0x38, 0x260, 0x50, 0xc60, 0x390, 0xd8, 0x42, 0x2, 0x58, 0x1b0, 0x7c, 0x29, 0x3c, 0x98, 0x5c, 0x9, 0x1c, 0x6c, 0x2c, 0x4c, 0x18, 0xc, 0x74, 0xe8, 0x68, 0x460, 0x90, 0x34, 0xb0, 0x710, 0x860, 0x31, 0x54, 0x11, 0x21, 0x17, 0x14, 0xa8, 0x28, 0x1, 0x310, 0x130, 0x3e, 0x64, 0x1e, 0x2e, 0x24, 0x510, 0xe, 0x36, 0x16, 0x44, 0x30, 0xc8, 0x1d0, 0xd0, 0x110, 0x48, 0x610, 0x150, 0x60, 0x88, 0xfa0, 0x7, 0x26, 0x6, 0x3a, 0x1b, 0x1a, 0x2a, 0xa, 0xb, 0x210, 0x4, 0x13, 0x32, 0x3, 0x1d, 0x12, 0x190, 0xd, 0x15, 0x5, 0x19, 0x8, 0x78, 0xf0, 0x70, 0x290, 0x410, 0x10, 0x7a0, 0xba0, 0x3a0, 0x240, 0x1c40, 0xc40, 0x1440, 0x440, 0x1840, 0x840, 0x1040, 0x40, 0x1f80, 0xf80, 0x1780, 0x780, 0x1b80, 0xb80, 0x1380, 0x380, 0x1d80, 0xd80, 0x1580, 0x580, 0x1980, 0x980, 0x1180, 0x180, 0x1e80, 0xe80, 0x1680, 0x680, 0x1a80, 0xa80, 0x1280, 0x280, 0x1c80, 0xc80, 0x1480, 0x480, 0x1880, 0x880, 0x1080, 0x80, 0x1f00, 0xf00, 0x1700, 0x700, 0x1b00, 0xb00, 0x1300, 0xda0, 0x5a0, 0x9a0, 0x1a0, 0xea0, 0x6a0, 0xaa0, 0x2a0, 0xca0, 0x4a0, 0x8a0, 0xa0, 0xf20, 0x720, 0xb20, 0x320, 0xd20, 0x520, 0x920, 0x120, 0xe20, 0x620, 0xa20, 0x220, 0xc20, 0x420, 0x820, 0x20, 0xfc0, 0x7c0, 0xbc0, 0x3c0, 0xdc0, 0x5c0, 0x9c0, 0x1c0, 0xec0, 0x6c0, 0xac0, 0x2c0, 0xcc0, 0x4c0, 0x8c0, 0xc0, 0xf40, 0x740, 0xb40, 0x340, 0x300, 0xd40, 0x1d00, 0xd00, 0x1500, 0x540, 0x500, 0x1900, 0x900, 0x940, 0x1100, 0x100, 0x1e00, 0xe00, 0x140, 0x1600, 0x600, 0x1a00, 0xe40, 0x640, 0xa40, 0xa00, 0x1200, 0x200, 0x1c00, 0xc00, 0x1400, 0x400, 0x1800, 0x800, 0x1000, 0x0 };

		static readonly byte[] CharBits = new byte[] { 0xb, 0xc, 0xc, 0xc, 0xc, 0xc, 0xc, 0xc, 0xc, 0x8, 0x7, 0xc, 0xc, 0x7, 0xc, 0xc, 0xc, 0xc, 0xc, 0xc, 0xc, 0xc, 0xc, 0xc, 0xc, 0xc, 0xd, 0xc, 0xc, 0xc, 0xc, 0xc, 0x4, 0xa, 0x8, 0xc, 0xa, 0xc, 0xa, 0x8, 0x7, 0x7, 0x8, 0x9, 0x7, 0x6, 0x7, 0x8, 0x7, 0x6, 0x7, 0x7, 0x7, 0x7, 0x8, 0x7, 0x7, 0x8, 0x8, 0xc, 0xb, 0x7, 0x9, 0xb, 0xc, 0x6, 0x7, 0x6, 0x6, 0x5, 0x7, 0x8, 0x8, 0x6, 0xb, 0x9, 0x6, 0x7, 0x6, 0x6, 0x7, 0xb, 0x6, 0x6, 0x6, 0x7, 0x9, 0x8, 0x9, 0x9, 0xb, 0x8, 0xb, 0x9, 0xc, 0x8, 0xc, 0x5, 0x6, 0x6, 0x6, 0x5, 0x6, 0x6, 0x6, 0x5, 0xb, 0x7, 0x5, 0x6, 0x5, 0x5, 0x6, 0xa, 0x5, 0x5, 0x5, 0x5, 0x8, 0x7, 0x8, 0x8, 0xa, 0xb, 0xb, 0xc, 0xc, 0xc, 0xd, 0xd, 0xd, 0xd, 0xd, 0xd, 0xd, 0xd, 0xd, 0xd, 0xd, 0xd, 0xd, 0xd, 0xd, 0xd, 0xd, 0xd, 0xd, 0xd, 0xd, 0xd, 0xd, 0xd, 0xd, 0xd, 0xd, 0xd, 0xd, 0xd, 0xd, 0xd, 0xd, 0xd, 0xd, 0xd, 0xd, 0xd, 0xd, 0xd, 0xd, 0xd, 0xd, 0xd, 0xd, 0xd, 0xd, 0xd, 0xc, 0xc, 0xc, 0xc, 0xc, 0xc, 0xc, 0xc, 0xc, 0xc, 0xc, 0xc, 0xc, 0xc, 0xc, 0xc, 0xc, 0xc, 0xc, 0xc, 0xc, 0xc, 0xc, 0xc, 0xc, 0xc, 0xc, 0xc, 0xc, 0xc, 0xc, 0xc, 0xc, 0xc, 0xc, 0xc, 0xc, 0xc, 0xc, 0xc, 0xc, 0xc, 0xc, 0xc, 0xc, 0xc, 0xc, 0xc, 0xd, 0xc, 0xd, 0xd, 0xd, 0xc, 0xd, 0xd, 0xd, 0xc, 0xd, 0xd, 0xd, 0xd, 0xc, 0xd, 0xd, 0xd, 0xc, 0xc, 0xc, 0xd, 0xd, 0xd, 0xd, 0xd, 0xd, 0xd, 0xd, 0xd, 0xd, 0xd };

		static int Truncate(int value, int bits) {
			return value & ((1 << bits) - 1);
		}

		static void FillBitBuffer(ref int bitBuffer, ref int bitCount, int requiredBits, Stream source) {
			for (; bitCount < requiredBits; bitCount += 8)
				bitBuffer |= (source.ReadByte() & 255) << bitCount;
		}

		static void RemoveBits(ref int bitBuffer, ref int bitCount, int bits) {
			bitBuffer >>= bits;
			bitCount -= bits;
		}

		static void WriteToOutput(byte value, byte[] output, ref int outputOffset, byte[] dictionary, int dictionaryTotalSize, ref int dictionaryOffset, ref int dictionarySize) {
			dictionary[dictionaryOffset++] = output[outputOffset++] = value;
			if (dictionarySize < dictionaryTotalSize)
				dictionarySize++;
			if (dictionaryOffset >= dictionaryTotalSize)
				dictionaryOffset = 0;
		}

		/// <summary>Decompress the <paramref name="source"/> into the <paramref name="output"/>, then return the number of decompressed bytes.</summary>
		/// <param name="source">The <see cref="Stream"/> to decompress from.</param>
		/// <param name="output">The <c>byte</c> array to store the decompressed data in.</param>
		/// <returns>The number of bytes written to <paramref name="output"/>.</returns>
		public static int Decompress(Stream source, byte[] output) {
			int outputOffset = 0;

			int dictionaryTotalSize; // Size of the dictionary in bits.
			int dictionaryOffset = 0;
			int dictionarySize = 0; // Number of bytes currently used in the dictionary.
			byte[] dictionary;

			int bitBuffer = 0, bitCount = 0;

			// Read header.
			var format = (Format)source.ReadByte();
			var dictionaryCode = (Dictionary)source.ReadByte();

			// Check header data format.
			if (format != Format.Ascii && format != Format.Binary)
				throw new Exception();

			// Check header dictionary.
			switch (dictionaryCode) {
				case Dictionary._1k: dictionaryTotalSize = 1024; break;
				case Dictionary._2k: dictionaryTotalSize = 2048; break;
				case Dictionary._4k: dictionaryTotalSize = 4096; break;
				default: throw new Exception();
			}

			dictionary = new byte[dictionaryTotalSize];

			while (outputOffset < output.Length) {
				// Ensure there are 16 bits on the bit buffer.
				FillBitBuffer(ref bitBuffer, ref bitCount, 16, source);

				bool isDictionaryIndex = (bitBuffer & 1) != 0;
				RemoveBits(ref bitBuffer, ref bitCount, 1);

				// If first bit is set, copy from dictionary.
				if (isDictionaryIndex) {
					int copyOffset, copyLength;

					// Find the length code value.
					int lengthCode;
					for (lengthCode = 0; Truncate(bitBuffer, LengthBits[lengthCode]) != LengthCodes[lengthCode]; lengthCode++) ;
					RemoveBits(ref bitBuffer, ref bitCount, LengthBits[lengthCode]);

					// Get the copy length and remove the bits from the bit buffer.
					copyLength = LengthBases[lengthCode] + Truncate(bitBuffer, LengthExtraBits[lengthCode]);
					RemoveBits(ref bitBuffer, ref bitCount, LengthExtraBits[lengthCode]);

					// This copy length indicates the end of the stream.
					if (copyLength == 519)
						break;

					FillBitBuffer(ref bitBuffer, ref bitCount, 14, source);

					// Determine the dictionary offset.
					int offsetCode;
					for (offsetCode = 0; Truncate(bitBuffer, OffsetBits[offsetCode]) != OffsetCodes[offsetCode]; offsetCode++) ;
					RemoveBits(ref bitBuffer, ref bitCount, OffsetBits[offsetCode]);
					int offsetBits = (copyLength == 2) ? 2 : (int)dictionaryCode;

					copyOffset = dictionarySize - 1 - ((offsetCode << offsetBits) + Truncate(bitBuffer, offsetBits));
					RemoveBits(ref bitBuffer, ref bitCount, offsetBits);

					while (copyLength-- > 0) {
						while (copyOffset < 0)
							copyOffset += dictionarySize;
						while (copyOffset >= dictionarySize)
							copyOffset -= dictionarySize;

						WriteToOutput(dictionary[copyOffset++], output, ref outputOffset, dictionary, dictionaryTotalSize, ref dictionaryOffset, ref dictionarySize);
					}
				} else { // Literal byte
					if (format == Format.Binary) {
						WriteToOutput((byte)bitBuffer, output, ref outputOffset, dictionary, dictionaryTotalSize, ref dictionaryOffset, ref dictionarySize);
						RemoveBits(ref bitBuffer, ref bitCount, 8);
					} else {
						int charCode;
						for (charCode = 0; Truncate(bitBuffer, CharBits[charCode]) != CharCodes[charCode]; charCode++) ;
						WriteToOutput((byte)charCode, output, ref outputOffset, dictionary, dictionaryTotalSize, ref dictionaryOffset, ref dictionarySize);
						RemoveBits(ref bitBuffer, ref bitCount, CharBits[charCode]);
					}
				}
			}

			return outputOffset;
		}
	}
}
