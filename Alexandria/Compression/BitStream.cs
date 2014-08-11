using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Compression {
	/// <summary>
	/// Provides for reading bits from a <see cref="Stream"/>.
	/// </summary>
	public struct BitStream {
		/// <summary>Get the <see cref="System.IO.Stream"/> to read bytes from.</summary>
		public readonly Stream Stream;

		/// <summary>Get or set the number of bits that are currently buffered.</summary>
		public int BitCount;

		/// <summary>Get or set the buffered bit values.</summary>
		public uint Bits;

		/// <summary>Get the actual position of the <see cref="Stream"/>, accounting for any buffered bits.</summary>
		public long Position { get { return Stream.Position - (BitCount + 7) / 8; } }

		/// <summary>Initialise the bitstream with the given stream.</summary>
		/// <param name="stream">The <see cref="System.IO.Stream"/> to read from.</param>
		/// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
		public BitStream(Stream stream) {
			if (stream == null)
				throw new ArgumentNullException("stream");

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

		/// <summary>Read a bit in LSB order and return whether it's non-zero.</summary>
		/// <returns></returns>
		public bool BooleanLSB() { return ReadLSB(1) != 0; }

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

		/// <summary>Read a bit in MSB order and return whether it's non-zero.</summary>
		/// <returns></returns>
		public bool BooleanMSB() { return ReadMSB(1) != 0; }

		/// <summary>Convert to a string representation of the bit stream.</summary>
		/// <returns></returns>
		public override string ToString() {
			return string.Format("{0}(BitCount = {1}, Bits = {2}, Position = {3})", GetType().Name, BitCount, Bits, Position);
		}
	}
}
