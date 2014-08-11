// #define StackTraceUnknowns // Add a stack trace to the ResourceUnknownBlocks; this can be slow with many calls.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glare.Internal;
using System.IO;
using Glare.Framework;
using System.Diagnostics;

namespace Glare.Assets {
	/// <summary>
	/// A block of unknown data.
	/// </summary>
	public class UnknownBlock {
#if StackTraceUnknowns
		public StackTrace Trace { get; private set; }
#endif // StackTraceUnknowns

		/// <summary>Get the offset of the data.</summary>
		public long Offset { get; private set; }

		/// <summary>Get the type of the elements of the data.</summary>
		public Type Type { get; private set; }

		/// <summary>Get string representations of the values of the data.</summary>
		public string[] Strings { get; private set; }

		/// <summary>Get the values of the data.</summary>
		public object[] Values { get; private set; }

		/// <summary>Get an optional tag string, or <c>null</c> if there is none.</summary>
		public string Tag { get; private set; }

		/// <summary>Get a string that joins the <see cref="Strings"/> together.</summary>
		public string JoinedValues { get { return string.Join(" ", Strings); } }

		internal UnknownBlock(StackTrace trace, long offset, Type type, string[] strings, object[] values, string tag)
			: this(offset, type, strings, values, tag) {
#if StackTraceUnknowns
			Trace = trace;
#endif // StackTraceUnknowns
		}

		/// <summary>Initialise the block.</summary>
		/// <param name="offset"></param>
		/// <param name="type"></param>
		/// <param name="strings"></param>
		/// <param name="values"></param>
		/// <param name="tag"></param>
		public UnknownBlock(long offset, Type type, string[] strings, object[] values, string tag) {
			Offset = offset;
			Type = type;
			Strings = strings;
			Values = values;
			Tag = tag;
		}

		/// <summary>Convert to a string representation of the unknown block.</summary>
		/// <returns></returns>
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

	/// <summary>
	/// A collection of <see cref="UnknownBlock"/>s.
	/// </summary>
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

		/// <summary>Read a collection of bytes as an unknown block.</summary>
		/// <param name="reader"></param>
		/// <param name="count"></param>
		/// <param name="tag"></param>
		/// <param name="order"></param>
		public void ReadBytes(BinaryReader reader, int count, string tag = null, ByteOrder order = ByteOrder.LittleEndian) { Read(reader, typeof(Byte), ReadByte, count, tag, order); }

		/// <summary>Read a collection of 16-bit integers as an unknown block.</summary>
		/// <param name="reader"></param>
		/// <param name="count"></param>
		/// <param name="tag"></param>
		/// <param name="order"></param>
		public void ReadInt16s(BinaryReader reader, int count, string tag = null, ByteOrder order = ByteOrder.LittleEndian) { Read(reader, typeof(Int16), ReadInt16, count, tag, order); }

		/// <summary>Read a collection of 32-bit integers as an unknown block.</summary>
		/// <param name="reader"></param>
		/// <param name="count"></param>
		/// <param name="tag"></param>
		/// <param name="order"></param>
		public void ReadInt32s(BinaryReader reader, int count, string tag = null, ByteOrder order = ByteOrder.LittleEndian) { Read(reader, typeof(Int32), ReadInt32, count, tag, order); }

		/// <summary>Read a collection of 32-bit floating-point values as an unknown block.</summary>
		/// <param name="reader"></param>
		/// <param name="count"></param>
		/// <param name="tag"></param>
		/// <param name="order"></param>
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

		/// <summary>Convert the unknown blocks to a comma-separated list that starts with a comma, or "" if there are no elements.</summary>
		/// <returns></returns>
		public string ToCommaPrefixedList() {
			if (Count == 0)
				return "";
			return ", " + ToCommaSeparatedList();
		}

		/// <summary>Convert the unknown blocks to a comma-separated list.</summary>
		/// <returns></returns>
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
