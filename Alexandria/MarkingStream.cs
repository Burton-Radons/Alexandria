using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria {
	public class MarkingStream : Stream {
		public struct UnreadBlock {
			public long Offset;
			public byte[] Data;
		}

		readonly bool LeaveOpen;
		readonly Stream Source;
		readonly bool[] IsRead;
		readonly byte[] Data;

		public MarkingStream(Stream source, bool leaveOpen = false) {
			if (source == null)
				throw new ArgumentNullException("source");
			Source = source;
			LeaveOpen = leaveOpen;
			IsRead = new bool[Source.Length];
			Data = new byte[Source.Length];

			long reset = Source.Position;
			source.Position = 0;
			source.Read(Data, 0, Data.Length);
			source.Position = reset;
		}

		public override bool CanRead { get { return Source.CanRead; } }

		public override bool CanSeek { get { return Source.CanSeek; } }

		public override bool CanWrite { get { return Source.CanWrite; } }

		public override void Flush() { Source.Flush(); }

		public override long Length { get { return Source.Length; } }

		public override long Position {
			get { return Source.Position; }
			set { Source.Position = value; }
		}

		public override int Read(byte[] buffer, int offset, int count) {
			int position = checked((int)Source.Position);
			int read = Source.Read(buffer, offset, count);

			for (int i = 0; i < read; i++)
				IsRead[position + i] = true;
			return read;
		}

		public override long Seek(long offset, SeekOrigin origin) { return Source.Seek(offset, origin); }

		public override void SetLength(long value) { Source.SetLength(value); }

		public byte[] ToUnusedBytes() {
			const string mark = "0123456789ABCDEF";
			byte[] data = new byte[Data.Length];
			Data.CopyTo(data, 0);

			for (int offset = 0, count = IsRead.Length; offset < count; ) {
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

		public override void Write(byte[] buffer, int offset, int count) { Source.Write(buffer, offset, count); }

		public void WriteUnusedBytes(string path) {
			using (Stream target = File.Open(path, FileMode.Create))
				WriteUnusedBytes(target);
		}

		public void WriteUnusedBytes(Stream target) {
			target.Write(ToUnusedBytes(), 0, IsRead.Length);
		}
	}
}
