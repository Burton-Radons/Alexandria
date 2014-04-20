using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Compression {
	/// <summary>
	/// Lempel-Ziv-Welch compression support.
	/// </summary>
	public static class LZW {
		public static int Decompress(Stream source, byte[] output) {
			return Decompress(source, output, 0, output.Length);
		}

		public static int Decompress(Stream source, byte[] output, int start, int count) {
			var bits = new BitStream(source);
			int offset = start, end = start + count;

			int tokenBits = 9; // Number of bits in a token.
			int endToken = 0x1FF; // Last token to cause an increase in the bits.
			int currentToken = 0x0102; // First undefined token.
			int[] tokenOffsets = new int[4096]; // Indexes into output for each token.
			int[] tokenLengths = new int[4096]; // Byte length of each token.

			while (offset < end) {
				int token = bits.ReadLSB(tokenBits);

				if (token == 0x101) {
					break;
				} else if (token == 0x100) {
					tokenBits = 9;
					endToken = 0x1FF;
					currentToken = 0x0102;
				} else {
					int tokenLength; // Size of the token in bytes.

					if (token > 0xFF) {
						if (token >= currentToken)
							throw new InvalidDataException("LZW decompression encountered out-of-range token.");

						tokenLength = tokenLengths[token] + 1;
						int writeLength = tokenLength;
						int tokenOffset = tokenOffsets[token];

						if (end - offset < writeLength)
							writeLength = end - offset;

						for (int index = 0; index < writeLength; index++)
							output[offset++] = output[tokenOffset++];
					} else {
						tokenLength = 1;
						output[offset++] = (byte)token;
					}

					if (currentToken > endToken && tokenBits < 12) {
						tokenBits++;
						endToken = (endToken << 1) + 1;
					}

					if (currentToken <= endToken) {
						tokenOffsets[currentToken] = offset - tokenLength;
						tokenLengths[currentToken] = tokenLength;
						currentToken++;
					}
				}
			}

			return offset - start;
		}
	}
}
