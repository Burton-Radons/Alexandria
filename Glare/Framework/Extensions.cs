using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Glare.Framework {
	public static class ExtensionMethods {
		public static void AddRange(this ArrayBackedList<byte> list, BinaryReader reader, int byteCount) { list.InsertRange(list.pCount, reader, byteCount); }
		public static void AddRange(this ArrayBackedList<byte> list, Stream stream, int byteCount) { list.InsertRange(list.pCount, stream, byteCount); }

		public static void InsertRange(this ArrayBackedList<byte> list, int index, BinaryReader reader, int byteCount) {
			if (reader == null)
				throw new ArgumentNullException("reader");
			list.InsertRange(index, reader.BaseStream, byteCount);
		}

		public static void InsertRange(this ArrayBackedList<byte> list, int index, Stream stream, int byteCount) {
			if (stream == null)
				throw new ArgumentNullException("stream");
			if (byteCount < 0)
				throw new ArgumentOutOfRangeException("byteCount");

			list.pInsertSpace(index, byteCount);

			int count = stream.Read(list.pArray, index, byteCount);
			if (count < byteCount)
				list.pRemoveSpace(index + count, byteCount - count);
		}
	}
}
