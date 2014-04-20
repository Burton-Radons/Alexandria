using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glare.Internal;

namespace Glare.Framework {
	public class BigEndianBinaryReader : BinaryReader {
		public BigEndianBinaryReader(Stream input) : base(input) { }
		public BigEndianBinaryReader(Stream input, Encoding encoding) : base(input, encoding) { }
		public BigEndianBinaryReader(Stream input, Encoding encoding, bool leaveOpen) : base(input, encoding, leaveOpen) { }

		public static BinaryReader Create(ByteOrder byteOrder, Stream input) {
			switch (byteOrder) {
				case ByteOrder.LittleEndian: return new BinaryReader(input);
				case ByteOrder.BigEndian: return new BigEndianBinaryReader(input);
				default: throw new NotSupportedException();
			}
		}

		readonly byte[] data = new byte[16];

		public override char ReadChar() { return (char)ReadUInt16(); }
		public override char[] ReadChars(int count) { throw new NotImplementedException(); }
		public override decimal ReadDecimal() { throw new NotImplementedException(); }
		public override double ReadDouble() { return base.ReadDouble().ReverseBytes(); }
		public override short ReadInt16() { return base.ReadInt16().ReverseBytes(); }
		public override int ReadInt32() { return base.ReadInt32().ReverseBytes(); }
		public override long ReadInt64() { return base.ReadInt64().ReverseBytes(); }
		public override float ReadSingle() { return base.ReadSingle().ReverseBytes(); }
		public override ushort ReadUInt16() { return base.ReadUInt16().ReverseBytes(); }
		public override uint ReadUInt32() { return base.ReadUInt32().ReverseBytes(); }
		public override ulong ReadUInt64() { throw new NotImplementedException(); }
	}

	public enum ByteOrder {
		None,
		LittleEndian,
		BigEndian,
	}
}
