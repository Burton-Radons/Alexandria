using Glare.Framework;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Assets {
	/// <summary>
	/// A wrapped stream that records what bytes are read, so it can report on what bytes are not read.
	/// </summary>
	public class MarkingStream : Stream {
		/// <summary>
		/// A block of unread data.
		/// </summary>
		public struct UnreadBlock {
			/// <summary>The number of bytes in the data.</summary>
			public int Count { get { return Data.Length; } }

			/// <summary>Get the offset of the data.</summary>
			public readonly int Offset;

			/// <summary>Get the data.</summary>
			public readonly byte[] Data;

			internal UnreadBlock(int offset, byte[] data) {
				Offset = offset;
				Data = data;
			}
		}

		readonly Stream Source;
		readonly bool LeaveOpen;
		readonly BooleanArray IsRead;

		/// <summary>Get the length in bytes of the stream.</summary>
		public override long Length { get { return Source.Length; } }

		/// <summary>Get or set the position of the stream.</summary>
		public override long Position {
			get { return Source.Position; }
			set { Source.Position = value; }
		}

		/// <summary>
		/// Create the marking stream.
		/// </summary>
		/// <param name="source">The stream to wrap.</param>
		/// <param name="leaveOpen">Whether to leave the stream open on <see cref="Dispose"/>.</param>
		public MarkingStream(Stream source, bool leaveOpen = false) {
			if (source == null)
				throw new ArgumentNullException("source");
			Source = source;
			IsRead = new BooleanArray(source.Length);
			LeaveOpen = leaveOpen;
		}

		/// <summary>Dispose of the data.</summary>
		/// <param name="disposing"></param>
		protected override void Dispose(bool disposing) {
			if (!LeaveOpen)
				Source.Dispose();
			base.Dispose(disposing);
		}

		/// <summary>Get all of the unread blocks in the stream.</summary>
		/// <returns></returns>
		public List<UnreadBlock> GetUnreadBlocks() {
			List<UnreadBlock> blocks = new List<UnreadBlock>();
			int offset = 0;
			long length = IsRead.Count;

			while (offset < length) {
				bool value = IsRead[offset];
				int next = (int)IsRead.IndexOf(!value, offset + 1);

				if (next < 0)
					next = checked((int)length);

				if (!value)
					blocks.Add(new UnreadBlock(offset, Source.ReadBytesAt(offset, next - offset)));
				offset = next;
			}

			return blocks;
		}

		/// <summary>Read data from the stream, marking whatever is read as having been so.</summary>
		/// <param name="buffer"></param>
		/// <param name="offset"></param>
		/// <param name="count"></param>
		/// <returns></returns>
		public override int Read(byte[] buffer, int offset, int count) {
			long position = Position;
			int read = Source.Read(buffer, offset, count);
			IsRead.Set(checked((int)position), count, true);
			return read;
		}

		/// <summary>Report unread blocks to the loader as errors.</summary>
		/// <param name="loader"></param>
		public void Report(AssetLoader loader) {
			var blocks = GetUnreadBlocks();

			if (blocks == null || blocks.Count == 0)
				return;

			int length = (int)IsRead.Count;
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
		}*/

		/// <summary>Write to the stream.</summary>
		/// <param name="buffer"></param>
		/// <param name="offset"></param>
		/// <param name="count"></param>
		public override void Write(byte[] buffer, int offset, int count) { Source.Write(buffer, offset, count); }

		/*public void WriteUnusedBytes(string path) {
			using (Stream target = File.Open(path, FileMode.Create))
				WriteUnusedBytes(target);
		}

		public void WriteUnusedBytes(Stream target) {
			target.Write(ToUnusedBytes(), 0, IsRead.Count);
		}*/

		/// <summary>Whether the stream can be read from.</summary>
		public override bool CanRead { get { return Source.CanRead; } }

		/// <summary>Whether the stream can seek.</summary>
		public override bool CanSeek { get { return Source.CanSeek; } }

		/// <summary>Whether the stream can be written to.</summary>
		public override bool CanWrite { get { return Source.CanWrite; } }

		/// <summary>Flush any changes to the stream.</summary>
		public override void Flush() { Source.Flush(); }

		/// <summary>Attempt to change the position of the stream.</summary>
		/// <param name="offset"></param>
		/// <param name="origin"></param>
		/// <returns></returns>
		public override long Seek(long offset, SeekOrigin origin) { return Source.Seek(offset, origin); }

		/// <summary>An invalid operation.</summary>
		/// <param name="value"></param>
		public override void SetLength(long value) { throw new NotSupportedException("This operation would be invalid with this stream."); }
	}
}
