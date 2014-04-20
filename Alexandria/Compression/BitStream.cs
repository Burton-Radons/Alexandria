using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Compression {
	public struct BitStream {
		/// <summary>Get the <see cref="System.IO.Stream"/> to read bytes from.</summary>
		readonly Stream Stream;

		/// <summary>Get or set the number of bits that are currently buffered.</summary>
		public int BitCount;

		/// <summary>Get or set the buffered bit values.</summary>
		public uint Bits;

		public BitStream(Stream stream) {
			Stream = stream;
			BitCount = 0;
			Bits = 0;
		}

		/// <summary>Fill the bit buffer in LSB order.</summary>
		public void FetchLSB() {
			while (BitCount <= 24) {
				Bits |= (uint)(Stream.ReadByte() & 0xFF) << BitCount;
				BitCount += 8;
			}
		}

		/// <summary>Read some bits in LSB order.</summary>
		/// <param name="count"></param>
		/// <returns></returns>
		public int ReadLSB(int count) {
			if (BitCount < count)
				FetchLSB();

			int result = (int)(Bits & ~((~0) << count));
			Bits >>= count;
			BitCount -= count;
			return result;
		}

		/// <summary>Peek at the next number of bits in LSB order.</summary>
		/// <param name="count"></param>
		/// <returns></returns>
		public int PeekLSB(int count) {
			if (BitCount < count)
				FetchLSB();

			return (int)(Bits & ~((~0) << count));
		}

		/// <summary>Fill the bit buffer in MSB order.</summary>
		public void FetchMSB() {
			while (BitCount <= 24) {
				Bits |= (uint)(Stream.ReadByte() & 0xFF) << (24 - BitCount);
				BitCount += 8;
			}
		}

		/// <summary>Read some bits in MSB order.</summary>
		/// <param name="count"></param>
		/// <returns></returns>
		public int ReadMSB(int count) {
			if (BitCount < count)
				FetchMSB();

			int result = (int)((Bits >> (32 - count)) & ~((~0) << count));
			Bits <<= count;
			BitCount -= count;
			return result;
		}

		/// <summary>Peek at the next number of bits in MSB order.</summary>
		/// <param name="count"></param>
		/// <returns></returns>
		public int PeekMSB(int count) {
			if (BitCount < count)
				FetchMSB();

			return (int)((Bits >> (32 - count)) & ~((~0) << count));
		}
	}
}
