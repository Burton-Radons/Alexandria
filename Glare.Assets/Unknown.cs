// #define StackTraceUnknowns // Add a stack trace to the ResourceUnknownBlocks; this can be slow with many calls.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glare.Internal;
using System.IO;
using Glare.Framework;

namespace Glare.Assets {
	public class UnknownBlock {
#if StackTraceUnknowns
		public StackTrace Trace { get; private set; }
#endif // StackTraceUnknowns
		public long Offset { get; private set; }
		public Type Type { get; private set; }
		public string[] Strings { get; private set; }
		public object[] Values { get; private set; }
		public string Tag { get; private set; }

		public string JoinedValues { get { return string.Join(" ", Strings); } }

		internal UnknownBlock(
#if StackTraceUnknowns
			StackTrace trace, 
#endif // StackTraceUnknowns
long offset, Type type, string[] strings, object[] values, string tag) {
#if StackTraceUnknowns
			Trace = trace;
#endif // StackTraceUnknowns
			Offset = offset;
			Type = type;
			Strings = strings;
			Values = values;
			Tag = tag;
		}

		public override string ToString() {
			string text = string.Format("{0}(", GetType().Name);
			if (Tag != null)
				text += string.Format("Tag {0}, ", Tag);
			text += string.Format("Type {2}, Value(s) {3}, Offset {1}", Offset, Type, JoinedValues);
#if StackTraceUnknowns
			text += ", At " + Trace.GetFrame(0);
#endif // StackTraceUnknowns
			return text + ")";
		}
	}

	public class UnknownList : List<UnknownBlock> {
		void Read(BinaryReader reader, Type type, Func<BinaryReader, ByteOrder, KeyValuePair<string, object>> read, int count, string tag, ByteOrder order) {
#if StackTraceUnknowns
			StackTrace trace = new StackTrace(2, true);
#endif // StackTraceUnknowns
			string[] strings = new string[count];
			object[] values = new object[count];

			long offset = reader.BaseStream.Position;
			for (int index = 0; index < count; index++) {
				var pair = read(reader, order);
				strings[index] = pair.Key;
				values[index] = pair.Value;
			}
			Add(new UnknownBlock(
#if StackTraceUnknowns
				trace,
#endif // StackTraceUnknowns
offset, type, strings, values, tag));
		}

		public void ReadBytes(BinaryReader reader, int count, string tag = null, ByteOrder order = ByteOrder.LittleEndian) { Read(reader, typeof(Byte), ReadByte, count, tag, order); }
		public void ReadInt16s(BinaryReader reader, int count, string tag = null, ByteOrder order = ByteOrder.LittleEndian) { Read(reader, typeof(Int16), ReadInt16, count, tag, order); }
		public void ReadInt32s(BinaryReader reader, int count, string tag = null, ByteOrder order = ByteOrder.LittleEndian) { Read(reader, typeof(Int32), ReadInt32, count, tag, order); }
		public void ReadSingles(BinaryReader reader, int count, string tag = null, ByteOrder order = ByteOrder.LittleEndian) { Read(reader, typeof(Single), ReadSingle, count, tag, order); }

		static KeyValuePair<string, object> ReadByte(BinaryReader reader, ByteOrder order) { return ToString(reader.ReadByte()); }
		static KeyValuePair<string, object> ReadInt16(BinaryReader reader, ByteOrder order) { return ToString(reader.ReadInt16(order)); }
		static KeyValuePair<string, object> ReadInt32(BinaryReader reader, ByteOrder order) { return ToString(reader.ReadInt32(order)); }
		static KeyValuePair<string, object> ReadSingle(BinaryReader reader, ByteOrder order) {
			var value = reader.ReadSingle(order);
			string text;

			if (value == float.MaxValue)
				text = "maxf";
			else if (value == float.MinValue)
				text = "minf";
			else if (Math.Abs(value) < 1e-7f && value != 0)
				text = "~" + (value < 0 ? "-" : "") + "0";
			else
				text = string.Format("{0}", value);
			return new KeyValuePair<string, object>(text, value);
		}

		static KeyValuePair<string, object> ToString(int value) {
			string text;

			if (value < 16)
				text = value.ToString();
			else
				text = string.Format("{0:X}h/{0}", value);
			return new KeyValuePair<string, object>(text, value);
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
				if (block.Tag != null)
					text += block.Tag + ": ";
				text += block.JoinedValues;
			}
			return text;
		}
	}
}
