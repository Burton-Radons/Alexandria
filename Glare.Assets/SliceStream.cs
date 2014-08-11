using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Glare.Assets {
	/// <summary>
	/// A <see cref="System.IO.Stream"/> wrapper that encapsulates part of the stream.
	/// </summary>
	public class SliceStream : Stream {
		long position;

		readonly bool closeStream;
		readonly Stream stream;
		readonly long start, length;

		/// <summary>Return whether the wrapped stream can read.</summary>
		public override bool CanRead { get { return stream.CanRead; } }

		/// <summary>Returns <c>true</c>.</summary>
		public override bool CanSeek { get { return true; } }

		/// <summary>Get whether the wrapped stream can write.</summary>
		public override bool CanWrite { get { return stream.CanWrite; } }

		/// <summary>Get the length in bytes of the stream.</summary>
		public override long Length { get { return length; } }

		/// <summary>Get or set the position of the stream.</summary>
		public override long Position {
			get { return position; }

			set {
				if (value < 0 || value > length)
					throw new ArgumentOutOfRangeException("value");
				position = value;
			}
		}

		/// <summary>Initialise the <see cref="SliceStream"/>.</summary>
		/// <param name="stream"></param>
		/// <param name="start"></param>
		/// <param name="length"></param>
		/// <param name="closeStream">Whether to <see cref="Close"/> the <paramref name="stream"/> when this <see cref="Stream"/> is <see cref="Close"/>d.</param>
		public SliceStream(Stream stream, long start, long length, bool closeStream = true) {
			if (stream == null)
				throw new ArgumentNullException("stream");
			if (!stream.CanSeek)
				throw new ArgumentException("Stream must be seekable to be used with " + GetType().Name + ".", "stream");
			if (start < 0)
				throw new ArgumentOutOfRangeException("start");
			if(length < 0)
				throw new ArgumentOutOfRangeException("length");

			SliceStream sliceStream = stream as SliceStream;

			if (sliceStream != null) {
				if (start > sliceStream.length)
					throw new ArgumentOutOfRangeException("start");
				if (start + length > sliceStream.length)
					throw new ArgumentOutOfRangeException("length");

				this.stream = sliceStream.stream;
				this.start = start + sliceStream.start;
				this.length = length;
			} else {
				this.stream = stream;
				this.start = start;
				this.length = length;
			}

			this.closeStream = closeStream;
		}

		/// <summary><see cref="Close"/> the stream.</summary>
		~SliceStream() {
			Close();
		}

		/// <summary>Close the wrapped stream, if closeStream of the constructor was <c>true</c>.</summary>
		public override void Close() {
			base.Close();
			if(closeStream)
				stream.Close();
		}

		/// <summary>Flush the wrapped stream.</summary>
		public override void Flush() {
			stream.Flush();
		}

		/// <summary>Read from the wrapped stream.</summary>
		/// <param name="buffer"></param>
		/// <param name="offset"></param>
		/// <param name="count"></param>
		/// <returns></returns>
		public override int Read(byte[] buffer, int offset, int count) {
			int read;

			if (count > length - position)
				count = (int)(length - position);

			lock (stream) {
				stream.Position = position + start;
				read = stream.Read(buffer, offset, count);
			}

			position += read;
			return read;
		}

		/// <summary>Seek within the wrapped stream.</summary>
		/// <param name="offset"></param>
		/// <param name="origin"></param>
		/// <returns></returns>
		public override long Seek(long offset, SeekOrigin origin) {
			switch (origin) {
				case SeekOrigin.Begin: Position = offset; break;
				case SeekOrigin.Current: Position += offset; break;
				case SeekOrigin.End: Position = length + offset; break;
				default: throw new ArgumentException("origin");
			}

			return position;
		}

		/// <summary>Throw a <see cref="NotSupportedException"/>.</summary>
		/// <param name="value"></param>
		public override void SetLength(long value) {
			throw new NotSupportedException();
		}

		/// <summary>Write to the wrapped stream.</summary>
		/// <param name="buffer"></param>
		/// <param name="offset"></param>
		/// <param name="count"></param>
		public override void Write(byte[] buffer, int offset, int count) {
			if (count > length - position)
				count = (int)(length - position);

			lock (stream) {
				stream.Position = position;
				stream.Write(buffer, offset, count);
			}

			position += count;
		}
	}
}
