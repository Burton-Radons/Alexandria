using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria
{
	public class UnknownBlock
	{
		public StackTrace Trace { get; private set; }
		public long Offset { get; private set; }
		public Type Type { get; private set; }
		public string[] Values { get; private set; }

		public string JoinedValues { get { return string.Join(" ", Values); } }

		public UnknownBlock(StackTrace trace, long offset, Type type, string[] values)
		{
			Trace = trace;
			Offset = offset;
			Type = type;
			Values = values;
		}

		public override string ToString()
		{
			return string.Format("{0}(Type {3}, Value(s) {4}, At {1}, Offset {2})", GetType().Name, Trace.GetFrame(0), Offset, Type, JoinedValues);
		}
	}

	public class UnknownList : List<UnknownBlock>
	{
		static string ToString(int value)
		{
			if (value < 16)
				return value.ToString();
			return string.Format("{0:X}h/{0}", value);
		}

		void Read(BinaryReader reader, Type type, Func<BinaryReader, string> read, int count)
		{
			StackTrace trace = new StackTrace(2, true);
			string[] values = new string[count];

			long offset = reader.BaseStream.Position;
			for (int index = 0; index < count; index++)
				values[index] = read(reader);
			Add(new UnknownBlock(trace, offset, type, values));
		}

		public void ReadBytes(BinaryReader reader, int count) { Read(reader, typeof(Byte), ReadByte, count); }
		public void ReadInt16s(BinaryReader reader, int count) { Read(reader, typeof(Int16), ReadInt16, count); }
		public void ReadInt32s(BinaryReader reader, int count) { Read(reader, typeof(Int32), ReadInt32, count); }
		public void ReadSingles(BinaryReader reader, int count) { Read(reader, typeof(Single), ReadSingle, count); }

		static string ReadByte(BinaryReader reader) { return ToString(reader.ReadByte()); }
		static string ReadInt16(BinaryReader reader) { return ToString(reader.ReadInt16()); }
		static string ReadInt32(BinaryReader reader) { return ToString(reader.ReadInt32()); }
		static string ReadSingle(BinaryReader reader) {
			var value = reader.ReadSingle();
			if (value == float.MaxValue)
				return "maxf";
			if (value == float.MinValue)
				return "minf";
			if (Math.Abs(value) < 1e-7f && value != 0)
				return "~" + (value < 0 ? "-" : "") + "0";
			return string.Format("{0}", value);
		}

		public string ToCommaPrefixedList() {
			if (Count == 0)
				return "";
			return ", " + ToCommaSeparatedList();
		}

		public string ToCommaSeparatedList() {
			string text = "";
			foreach (UnknownBlock block in this) {
				if (text.Length > 0)
					text += ", ";
				text += block.JoinedValues;
			}
			return text;
		}
	}
}
