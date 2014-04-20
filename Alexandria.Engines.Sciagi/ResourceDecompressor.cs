using Alexandria.Compression;
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

				case CompressionMethod.Lzw:
					byte[] lzwOutput = new byte[uncompressedSize];
					int lzwRead = Alexandria.Compression.LZW.Decompress(input, lzwOutput);
					if (lzwRead != uncompressedSize)
						throw new InvalidDataException("Invalid LZW decompression.");
					return lzwOutput;
					//return new LZW(input, compressedSize, uncompressedSize).OutputData;

				case CompressionMethod.DclImplode:
					byte[] dclOutput = new byte[uncompressedSize];
					int dclRead = DclImplode.Decompress(input, dclOutput);
					if (dclRead != uncompressedSize)
						throw new Exception("Invalid DCL decompression.");
					return dclOutput;

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

		/// <summary>Read some bits in LSB order.</summary>
		/// <param name="count"></param>
		/// <returns></returns>
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

		/// <summary>Read some bits in MSB order.</summary>
		/// <param name="count"></param>
		/// <returns></returns>
		protected int BitsMSB(int count) {
			if (BitCount < count)
				FetchMSB();

			int result = (int)((Bits >> (32 - count)) & ~((~0) << count));
			Bits <<= count;
			BitCount -= count;
			return result;
		}

		int ReadHuffmanCodeMSB(byte[] nodes) {
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

		protected bool Finished { get { return Input.Position >= End; } }

		protected int RemainingOutput { get { return UncompressedSize - (int)Output.Position; } }

		class Huffman : ResourceDecompressor {
			byte[] nodes;

			public Huffman(Stream input, int compressedSize, int uncompressedSize)
				: base(input, compressedSize, uncompressedSize) {
				int nodeCount = input.ReadByte();
				int terminator = input.ReadByte() | 0x100;
				int code;

				nodes = new byte[nodeCount << 1];
				input.Read(nodes, 0, nodes.Length);

				while ((code = ReadCode()) != terminator && (code >= 0) && !Finished)
					Output.WriteByte((byte)code);
			}

			int ReadCode() { return ReadHuffmanCodeMSB(nodes); }
		}
	}
}
