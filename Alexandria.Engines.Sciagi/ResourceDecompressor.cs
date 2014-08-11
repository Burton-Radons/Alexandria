using Alexandria.Compression;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.Sciagi {
	/// <summary>
	/// Handles decompression for <see cref="Resource"/> data.
	/// </summary>
	public struct ResourceDecompressor {
		/// <summary>Get the compressed size of the resource.</summary>
		readonly int CompressedSize;

		/// <summary>Get the uncompressed size of the resource.</summary>
		readonly int UncompressedSize;

		/// <summary>Get the input <see cref="BitStream"/>.</summary>
		BitStream Input;

		/// <summary>Get the end <see cref="Input"/> offset.</summary>
		readonly long InputEnd;

		/// <summary>Get the array that receives the output data.</summary>
		readonly byte[] OutputData;

		/// <summary>Get or set the offset to write to <see cref="OutputData"/>.</summary>
		int OutputOffset;

		/// <summary>Return whether the input position has reached the end.</summary>
		bool IsInputFinished { get { return Input.Position >= InputEnd; } }

		/// <summary>Return whether the output position has reached the end.</summary>
		bool IsOutputFinished { get { return OutputOffset >= UncompressedSize; } }

		/// <summary>Return whether the input or output has reached the end; that is, if <see cref="IsInputFinished"/> or <see cref="IsOutputFinished"/> is <c>true</c>.</summary>
		bool IsFinished { get { return Input.Position >= InputEnd || OutputOffset >= UncompressedSize; } }

		/// <summary>Get how many output bytes remain.</summary>
		int RemainingOutput { get { return UncompressedSize - OutputOffset; } }

		internal ResourceDecompressor(Stream input, int compressedSize, int uncompressedSize) {
			Input = new BitStream(input);
			CompressedSize = compressedSize;
			UncompressedSize = uncompressedSize;
			OutputData = new byte[uncompressedSize];
			InputEnd = input.Position + compressedSize;
			OutputOffset = 0;
		}

		void CheckEnd() {
			if (IsFinished)
				return;
			if (!IsInputFinished) {
				if (!IsOutputFinished)
					throw new InvalidDataException("Neither the input nor the output finished decompressing.");
				throw new InvalidDataException("The input did not finish decompressing.");
			} else if (!IsOutputFinished)
				throw new InvalidDataException("The output did not finish decompressing.");
		}

		/// <summary>Decompress input data.</summary>
		/// <param name="input"></param>
		/// <param name="compressedSize"></param>
		/// <param name="uncompressedSize"></param>
		/// <param name="compressionMode"></param>
		/// <returns></returns>
		public static byte[] Decompress(Stream input, int compressedSize, int uncompressedSize, CompressionMethod compressionMode) {
			switch (compressionMode) {
				case CompressionMethod.None:
					if (compressedSize != uncompressedSize)
						throw new Exception("CompressedSize and UncompressedSize are not equal with no compression.");
					var output = new byte[uncompressedSize];
					input.Read(output, 0, output.Length);
					return output;

				case CompressionMethod.Huffman:
					return new ResourceDecompressor(input, compressedSize, uncompressedSize).DecompressHuffman();

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

				case CompressionMethod.Lzs:
					return new ResourceDecompressor(input, compressedSize, uncompressedSize).DecompressLZS();

				default:
					throw new Exception("Unknown or unhandled compression mode " + compressionMode + ".");
			}
		}

		byte[] DecompressHuffman() {
			int nodeCount = Input.Stream.ReadByte();
			int terminator = Input.Stream.ReadByte() | 0x100;
			int code;

			byte[] nodes = new byte[nodeCount << 1];
			Input.Stream.Read(nodes, 0, nodes.Length);

			while ((code = ReadHuffmanCodeMSB(nodes)) != terminator && (code >= 0) && !IsFinished)
				Write((byte)code);

			CheckEnd();
			return OutputData;
		}

		byte[] DecompressLZS() {
			while (!IsFinished) {
				bool isCompressed = Input.BooleanMSB();
				if (isCompressed) { // Compressed bytes follow.
					int offsetCode = Input.ReadMSB(1); // 0 for 11 bits offset, 1 for 7 bits offset and possible end marker.
					int offset = Input.ReadMSB(offsetCode == 1 ? 7 : 11);

					if (offset == 0) {
						// Zero offset is an end marker for the 7-bit offset length.
						if (offsetCode == 1)
							break;
						throw new InvalidDataException("Zero offset.");
					}

					int count = ReadLZSRunLength();
					if (count == 0)
						throw new InvalidDataException("Length mismatch.");

					WriteMemory(offset, count);
				} else {// Literal byte follows
					byte literal = (byte)Input.ReadMSB(8);
					Write(literal);
				}
			}

			CheckEnd();
			return OutputData;
		}

		int ReadHuffmanCodeMSB(byte[] nodes) {
			int next, index = 1;

			while (nodes[index] != 0) {
				if (Input.BooleanMSB()) {
					next = nodes[index] & 0x0F;
					if (next == 0) {
						return Input.ReadMSB(8) | 0x100;
					}
				} else {
					next = (nodes[index] & 255) >> 4;
				}
				index += next << 1;
			}

			return (nodes[index - 1] & 255) | ((nodes[index] & 255) << 8);
		}

		int ReadLZSRunLength() {
			switch (Input.ReadMSB(2)) {
				case 0: return 2;
				case 1: return 3;
				case 2: return 4;
				default: //case 3:
					switch (Input.ReadMSB(2)) {
						case 0: return 5;
						case 1: return 6;
						case 2: return 7;
						default: //case 3:
							int length = 8, nibble;
							do {
								nibble = Input.ReadMSB(4);
								length += nibble;
							} while (nibble == 15);
							return length;
					}
			}
		}

		void Write(byte value) { OutputData[OutputOffset++] = value; }

		/// <summary>Copy a portion of the output buffer onto itself.</summary>
		/// <param name="offset">The positive offset from the current <see cref="OutputOffset"/> to read from.</param>
		/// <param name="count">The number of bytes to write.</param>
		void WriteMemory(int offset, int count) {
			int position = OutputOffset - offset;

			for (int index = 0; index < count; index++)
				Write(OutputData[position++]);
		}
	}
}
