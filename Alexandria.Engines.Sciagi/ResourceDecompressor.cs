using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.Sciagi {
	public abstract class ResourceDecompressor {
		protected Stream Input;
		protected MemoryStream Output;
		protected int CompressedSize;
		protected int UncompressedSize;
		protected long End;
		protected byte[] OutputData;

		public static byte[] Decompress(Stream input, int compressedSize, int uncompressedSize, CompressionMethod compressionMode) {
			switch (compressionMode) {
				case CompressionMethod.None:
					if (compressedSize != uncompressedSize)
						throw new Exception("CompressedSize and UncompressedSize are not equal with no compression.");
					var output = new byte[uncompressedSize];
					input.Read(output, 0, output.Length);
					return output;

				case CompressionMethod.Huffman:
					return new Huffman(input, compressedSize, uncompressedSize).OutputData;

				case CompressionMethod.LZW:
					return new LZW(input, compressedSize, uncompressedSize).OutputData;

				default:
					throw new Exception("Unknown or unhandled compression mode " + compressionMode + ".");
			}
		}

		public ResourceDecompressor(Stream input, int compressedSize, int uncompressedSize) {
			Input = input;
			CompressedSize = compressedSize;
			UncompressedSize = uncompressedSize;
			OutputData = new byte[uncompressedSize];
			Output = new MemoryStream(OutputData);
			End = input.Position + compressedSize;
		}

		/** Number of bits currently buffered. */
		protected int BitCount = 0;

		/** Buffered bits. */
		protected uint Bits = 0;

		/** Fill the bit buffer in an LSB-ordered fashion. */
		protected void FetchLSB() {
			while (BitCount <= 24) {
				Bits |= (uint)(Input.ReadByte() & 0xFF) << BitCount;
				BitCount += 8;
			}
		}
		
		/** Read some bits in LSB order. */
		protected int BitsLSB(int count) {
			if (BitCount < count)
				FetchLSB();

			int result = (int)(Bits & ~((~0) << count));
			Bits >>= count;
			BitCount -= count;
			return result;
		}

		protected void FetchMSB() {
			while (BitCount <= 24) {
				Bits |= (uint)(Input.ReadByte() & 0xFF) << (24 - BitCount);
				BitCount += 8;
			}
		}

		protected int BitsMSB(int count) {
			if (BitCount < count) {
				FetchMSB();
			}

			int result = (int)((Bits >> (32 - count)) & ~((~0) << count));
			Bits <<= count;
			BitCount -= count;
			return result;
		}

		protected bool Finished { get { return Input.Position >= End; } }

		protected int RemainingOutput { get { return UncompressedSize - (int)Output.Position; } }

		class LZW : ResourceDecompressor {
			public LZW(Stream input, int compressedSize, int uncompressedSize)
				: base(input, compressedSize, uncompressedSize) {
				int bits = 9; // Number of bits in a token.
				int endToken = 0x1FF; // Last token to cause an increase in the bits.
				int currentToken = 0x0102; // First undefined token.
				int[] tokenOffsets = new int[4096]; // Indexes into output for each token.
				int[] tokenLengths = new int[4096]; // Byte length of each token.

				while (!Finished) {
					int token = BitsLSB(bits);

					if (token == 0x101) {
						break;
					} else if (token == 0x100) {
						bits = 9;
						endToken = 0x1FF;
						currentToken = 0x0102;
					} else {
						int tokenLength; // Size of the token in bytes.

						if (token > 0xFF) {
							if (token >= currentToken)
								throw new Exception("SCI LZW decompression encountered out-of-range token.");

							tokenLength = tokenLengths[token] + 1;
							int writeLength = tokenLength;
							int offset = tokenOffsets[token];

							if (RemainingOutput < writeLength) {
								writeLength = RemainingOutput;
							}

							Output.Write(OutputData, offset, writeLength);
						} else {
							tokenLength = 1;
							Output.WriteByte((byte)token);
						}

						if (currentToken > endToken && bits < 12) {
							bits++;
							endToken = (endToken << 1) + 1;
						}

						if (currentToken <= endToken) {
							tokenOffsets[currentToken] = (int)Output.Position - tokenLength;
							tokenLengths[currentToken] = tokenLength;
							currentToken++;
						}
					}
				}
			}
		}

		class Huffman : ResourceDecompressor {
			byte[] nodes;

			public Huffman(Stream input, int compressedSize, int uncompressedSize)
				: base(input, compressedSize, uncompressedSize) {
				int nodeCount = input.ReadByte();
				int terminator = input.ReadByte() | 0x100;
				int code;

				nodes = new byte[nodeCount << 1];
				input.Read(nodes, 0, nodes.Length);

				while ((code = ReadCode()) != terminator && (code >= 0) && !Finished) {
					Output.WriteByte((byte)code);
				}
			}

			int ReadCode() {
				int next, index = 1;

				while (nodes[index] != 0) {
					if (BitsMSB(1) != 0) {
						next = nodes[index] & 0x0F;
						if (next == 0) {
							return BitsMSB(8) | 0x100;
						}
					} else {
						next = (nodes[index] & 255) >> 4;
					}
					index += next << 1;
				}

				return (nodes[index - 1] & 255) | ((nodes[index] & 255) << 8);
			}
		}
	}
}
