using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glare.Internal;

namespace Glare.Framework {
	/// <summary>
	/// A <see cref="BinaryReader"/> that reads in big-endian order.
	/// </summary>
	public class BigEndianBinaryReader : BinaryReader {
		/// <summary>Initialise the reader.</summary>
		/// <param name="input"></param>
		public BigEndianBinaryReader(Stream input) : base(input) { }

		/// <summary>Initialise the reader.</summary>
		/// <param name="input"></param>
		/// <param name="encoding"></param>
		public BigEndianBinaryReader(Stream input, Encoding encoding) : base(input, encoding) { }

		/// <summary>Create a <see cref="BinaryReader"/> or a <see cref="BigEndianBinaryReader"/> based upon a <see cref="ByteOrder"/>.</summary>
		/// <param name="byteOrder"></param>
		/// <param name="input"></param>
		/// <returns></returns>
		public static BinaryReader Create(ByteOrder byteOrder, Stream input) {
			switch (byteOrder) {
				case ByteOrder.LittleEndian: return new BinaryReader(input);
				case ByteOrder.BigEndian: return new BigEndianBinaryReader(input);
				default: throw new NotSupportedException();
			}
		}

		readonly byte[] data = new byte[16];

		/// <summary>Read a single character in the encoding specified when creating the reader.</summary>
		/// <returns></returns>
		public override char ReadChar() { throw new NotImplementedException(); }

		/// <summary>Read a set of characters.</summary>
		/// <param name="count"></param>
		/// <returns></returns>
		public override char[] ReadChars(int count) {
			char[] result = new char[count];
			for (int index = 0; index < count; index++)
				result[index] = ReadChar();
			return result;
		}

		/// <summary>Read a decimal value.</summary>
		/// <returns></returns>
		public override decimal ReadDecimal() { throw new NotImplementedException(); }

		/// <summary>Read a <see cref="Double"/> value in big-endian order.</summary>
		/// <returns></returns>
		public override double ReadDouble() { return base.ReadDouble().ReverseBytes(); }

		/// <summary>Read a <see cref="Int16"/> value in big-endian order.</summary>
		/// <returns></returns>
		public override short ReadInt16() { return base.ReadInt16().ReverseBytes(); }

		/// <summary>Read a <see cref="Int32"/> value in big-endian order.</summary>
		/// <returns></returns>
		public override int ReadInt32() { return base.ReadInt32().ReverseBytes(); }

		/// <summary>Read a <see cref="Int64"/> value in big-endian order.</summary>
		/// <returns></returns>
		public override long ReadInt64() { return base.ReadInt64().ReverseBytes(); }

		/// <summary>Read a <see cref="Single"/> value in big-endian order.</summary>
		/// <returns></returns>
		public override float ReadSingle() { return base.ReadSingle().ReverseBytes(); }

		/// <summary>Read a <see cref="UInt16"/> value in big-endian order.</summary>
		/// <returns></returns>
		public override ushort ReadUInt16() { return base.ReadUInt16().ReverseBytes(); }

		/// <summary>Read a <see cref="UInt32"/> value in big-endian order.</summary>
		/// <returns></returns>
		public override uint ReadUInt32() { return base.ReadUInt32().ReverseBytes(); }

		/// <summary>Read a <see cref="UInt64"/> value in big-endian order.</summary>
		/// <returns></returns>
		public override ulong ReadUInt64() { return base.ReadUInt64().ReverseBytes(); }

		/// <summary>Read a string that's stored as a 7-bit coded length followed by string data in the encoding specified when creating the reader.</summary>
		/// <returns></returns>
		public override string ReadString() {
			throw new NotImplementedException();
		}
	}

	/// <summary>This specifies byte order.</summary>
	public enum ByteOrder {
		/// <summary>
		/// No or invalid value.
		/// </summary>
		None,

		/// <summary>
		/// Little endian byte order (0x1234 would be encoded as (0x34, 0x12)).
		/// </summary>
		LittleEndian,

		/// <summary>
		/// Big endian byte order (0x1234 would be encoded as (0x12, 0x34)).
		/// </summary>
		BigEndian,
	}
}
