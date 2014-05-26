using Glare.Framework;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Assets {
	public class MarkingReader : BinaryReader {
		public struct UnreadBlock {
			public int Count { get { return Data.Length; } }
			public readonly int Offset;
			public readonly byte[] Data;

			public UnreadBlock(int offset, byte[] data) {
				Offset = offset;
				Data = data;
			}
		}

		readonly bool LeaveOpen;
		readonly BooleanArray IsRead;
		readonly ByteOrder ByteOrder;

		public MarkingReader(Stream source, ByteOrder byteOrder = ByteOrder.LittleEndian, bool leaveOpen = false)
			: base(source) {
			if (source == null)
				throw new ArgumentNullException("source");
			ByteOrder = byteOrder;
			LeaveOpen = leaveOpen;
			IsRead = new BooleanArray(source.Length);
		}

		protected override void Dispose(bool disposing) {
			if(!LeaveOpen)
				base.Dispose(disposing);
		}

		public string GetReport() {
			var blocks = GetUnreadBlocks();

			if (blocks.Count == 0)
				return null;

			long length = BaseStream.Length;
			var builder = new StringBuilder();
			int totalMissed = 0;
			foreach (var block in blocks)
				totalMissed += block.Count;

			builder.AppendFormat("{0} byte(s), {1} read, {2} missed ({3:F2}%)\n", length, length - totalMissed, totalMissed, totalMissed / (double)length);
			foreach (var block in blocks) {
				builder.AppendFormat("\tMissed {0}/{0:X}h at {1}/{1:X}h: ", block.Count, block.Offset);

				const int maxCount = 256;

				for (int index = 0; index < maxCount && index < block.Count; index++) {
					if (index > 0)
						builder.Append(", ");
					var value = block.Data[index];
					builder.AppendFormat("{0}", value);
					if (value > 15)
						builder.AppendFormat("/{0:X}h", value);
				}

				if (block.Count > maxCount)
					builder.Append("...");
				builder.Append("\n");
			}

			return builder.ToString();
		}

		public List<UnreadBlock> GetUnreadBlocks() {
			List<UnreadBlock> blocks = new List<UnreadBlock>();
			int offset = 0;
			long length = IsRead.Count;

			while (offset < length) {
				bool value = IsRead[offset];
				int next = IsRead.IndexOf(!value, offset + 1);

				if (next < 0)
					next = checked((int)length);

				if (!value)
					blocks.Add(new UnreadBlock(offset, BaseStream.ReadBytesAt(offset, next - offset)));
				offset = next;
			}

			return blocks;
		}

		public override int Read(byte[] buffer, int offset, int count) {
			int position = checked((int)BaseStream.Position);
			int read = BaseStream.Read(buffer, offset, count);

			IsRead.Set(position, read, true);
			return read;
		}

		public void Report(AssetLoader loader) {
			var blocks = GetUnreadBlocks();

			if (blocks == null || blocks.Count == 0)
				return;

			int length = IsRead.Count;
			int totalMissed = 0;
			foreach (var block in blocks)
				totalMissed += block.Count;

			loader.AddError(-1, "{0} byte(s), {1} read, {2} missed ({3:F2}%)", length, length - totalMissed, totalMissed, totalMissed / (double)length * 100);
			foreach (var block in blocks) {
				string message = string.Format("Missed {0}/{0:X}h at {1}/{1:X}h: ", block.Count, block.Offset);

				const int maxCount = 256;

				for (int index = 0; index < maxCount && index < block.Count; index++) {
					if (index > 0)
						message += ", ";
					var value = block.Data[index];
					message += string.Format("{0}", value);
					if (value > 15)
						message += string.Format("/{0:X}h", value);
				}

				if (block.Count > maxCount)
					message += "...";
				loader.AddError(block.Offset, message);
			}
		}

		/*public byte[] ToUnusedBytes() {
			const string mark = "0123456789ABCDEF";
			byte[] data = new byte[Data.Length];
			Data.CopyTo(data, 0);

			for (int offset = 0, count = IsRead.Count; offset < count; ) {
				bool value = IsRead[offset];
				int end = offset;

				if (value)
					for (; end < count && IsRead[end] == value; end++)
						data[end] = (byte)mark[end % mark.Length];
				else
					for (; end < count && IsRead[end] == value; end++) ;

				offset = end;
			}

			return data;
		}

		public override void Write(byte[] buffer, int offset, int count) { BaseStream.Write(buffer, offset, count); }

		public void WriteUnusedBytes(string path) {
			using (Stream target = File.Open(path, FileMode.Create))
				WriteUnusedBytes(target);
		}

		public void WriteUnusedBytes(Stream target) {
			target.Write(ToUnusedBytes(), 0, IsRead.Count);
		}*/
	}
}
