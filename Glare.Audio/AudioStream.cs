using NVorbis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ToyMp3;
using Glare.Internal;

namespace Glare.Audio
{
	/// <summary>
	/// This acts as a stream for producing audio data.
	/// </summary>
	public abstract class AudioStream : Stream
	{
		public override bool CanRead { get { return true; } }

		public override bool CanWrite { get { return false; } }

		/// <summary>Get the format of the audio data this produces.</summary>
		public abstract AudioFormat Format { get; }

		/// <summary>Get the sample frequency of the audio data.</summary>
		public abstract Frequency Frequency { get; }

		/// <summary>Get the length of the stream in samples.</summary>
		public long LengthSamples { get { return Length / Format.SampleByteSize(); } }

		/// <summary>Get the length of the stream in time.</summary>
		public TimeSpan LengthTime { get { return ByteOffsetToTimeSpan(Length); } }

		/// <summary>Get or set the position of the stream in samples.</summary>
		public long PositionSample
		{
			get { return Position / Format.SampleByteSize(); }
			set { Position = value * Format.SampleByteSize(); }
		}

		/// <summary>Get or set the position of the stream in time.</summary>
		public TimeSpan PositionTime
		{
			get { return ByteOffsetToTimeSpan(Position); }
			set { Position = ByteOffsetFromTimeSpan(value); }
		}

		TimeSpan ByteOffsetToTimeSpan(long offset) { return TimeSpan.FromSeconds(Position / Frequency.InHertz / Format.SampleByteSize()); }
		long ByteOffsetFromTimeSpan(TimeSpan value) { return (long)(value.TotalSeconds * Frequency.InHertz * Format.SampleByteSize()); }

		public override void Flush() { }

		protected static Stream GetStream(string path) { return File.OpenRead(path); }
		protected static Stream GetStream(FileInfo file) { return file.OpenRead(); }
		protected static Stream GetStream(byte[] data) { return new MemoryStream(data, false); }
		protected static Stream GetStream(byte[] data, int offset, int length) { return new MemoryStream(data, offset, length, false); }
		protected static Stream GetStream(IntPtr data, int length) { unsafe { return GetStream(data.ToPointer(), length); } }
		protected unsafe static Stream GetStream(void* data, int length) { return new UnmanagedMemoryStream((byte*)data, length); }

		public AudioBuffer ReadToBuffer(AudioBuffer buffer)
		{
			byte[] data = new byte[Length - Position];
			int read = Read(data, 0, data.Length);
			buffer.Data(Format, data, 0, read, Frequency);
			return buffer;
		}

		public AudioBuffer ReadToBuffer(AudioContext context) { return ReadToBuffer(new AudioBuffer(context)); }

		public override long Seek(long offset, SeekOrigin origin)
		{
			switch (origin)
			{
				case SeekOrigin.Begin: return Position = offset;
				case SeekOrigin.Current: return Position += offset;
				case SeekOrigin.End: return Position = Length + offset;
				default: throw new NotSupportedException();
			}
		}

		public override void SetLength(long value) { throw new NotSupportedException(); }
		public override void Write(byte[] buffer, int offset, int count) { throw new NotSupportedException(); }
	}

	/// <summary>A simple wrapper around a stream.</summary>
	public class WrapperAudioStream : AudioStream
	{
		readonly Stream source;
		readonly AudioFormat format;
		readonly Frequency frequency;
		readonly bool closeSource;

		public override bool CanSeek { get { return source.CanSeek; } }

		public override AudioFormat Format { get { return format; } }

		public override Frequency Frequency { get { return frequency; } }

		public override long Length { get { return source.Length; } }

		public override long Position
		{
			get { return source.Position; }
			set { source.Position = value; }
		}

		public WrapperAudioStream(Stream source, AudioFormat format, Frequency frequency, bool closeSource = false)
		{
			if (source == null)
				throw new ArgumentNullException("source");
			this.source = source;
			this.format = format;
			this.frequency = frequency;
			this.closeSource = closeSource;
		}

		public override void Close()
		{
			base.Close();
			if (closeSource)
				source.Close();
		}

		public override int Read(byte[] buffer, int offset, int count) { return source.Read(buffer, offset, count); }
	}

	public class FilterAudioStream : AudioStream
	{
		readonly AudioStream source;
		readonly bool closeSourceOnClose;

		public AudioStream Source { get { return source; } }

		public FilterAudioStream(AudioStream source, bool closeSourceOnClose = false)
		{
			this.source = source;
			this.closeSourceOnClose = closeSourceOnClose;
		}

		public override void Close()
		{
			if (closeSourceOnClose)
				source.Close();
			base.Close();
		}

		public override AudioFormat Format { get { return source.Format; } }

		public override Frequency Frequency { get { return source.Frequency; } }

		public override bool CanSeek { get { return source.CanSeek; } }

		public override long Length { get { return source.Length; } }

		public override long Position
		{
			get { return source.Position; }
			set { source.Position = value; }
		}

		public override int Read(byte[] buffer, int offset, int count) { return source.Read(buffer, offset, count); }
	}

	public class Mp3AudioStream : AudioStream
	{
		Mp3Stream stream;
		long position;

		public Mp3AudioStream(Stream source)
		{
			stream = new Mp3Stream(source);
		}

		public Mp3AudioStream(string path) : this(GetStream(path)) { }
		public Mp3AudioStream(FileInfo file) : this(GetStream(file)) { }
		public Mp3AudioStream(byte[] data) : this(GetStream(data)) { }
		public Mp3AudioStream(byte[] data, int offset, int length) : this(GetStream(data, offset, length)) { }
		public Mp3AudioStream(IntPtr data, int length) : this(GetStream(data, length)) { }
		unsafe public Mp3AudioStream(void* data, int length) : this(GetStream(data, length)) { }

		public override AudioFormat Format
		{
			get
			{
				switch (stream.Channels)
				{
					case 1: return AudioFormat.MonoByte;
					case 2: return AudioFormat.StereoByte;
					default: throw new NotSupportedException();
				}
			}
		}

		public override Frequency Frequency { get { return Frequency.Hertz(stream.Samplerate); } }

		public override bool CanSeek { get { return false; } }

		public override long Length { get { return (long)(stream.Channels * stream.Samplerate * stream.LengthInSeconds); } }

		public override long Position
		{
			get { return position; }

			set
			{
				if (value != 0)
					throw new NotSupportedException("Only rewind is supported by this stream.");
				stream.Rewind();
			}
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			if (offset != 0)
				throw new NotSupportedException();
			int read = count;// stream.Read(buffer, count);

			// Only unsigned byte PCM data are supported by OpenAL, because it sucks.
			for (int index = 0; index < read; index++)
				buffer[offset + index] = (byte)((sbyte)buffer[offset + index] + 128);
			position += read;
			return read;
		}
	}

	public class VorbisAudioStream : AudioStream
	{
		VorbisReader reader;
		float[] floats = new float[1024];

		public override AudioFormat Format
		{
			get
			{
				switch (reader.Channels)
				{
					case 1: return AudioFormat.MonoSingle;
					case 2: return AudioFormat.StereoSingle;
					default: throw new NotSupportedException();
				}
			}
		}

		public override Frequency Frequency { get { return Frequency.Hertz(reader.SampleRate); } }

		public override bool CanSeek { get { return true; } }

		public override long Length { get { return ToBytes(reader.TotalTime); } }

		public override long Position
		{
			get { return ToBytes(reader.DecodedTime); }
			set { reader.DecodedTime = FromBytes(value); }
		}

		VorbisAudioStream(VorbisReader reader)
		{
			if (reader == null)
				throw new ArgumentNullException("reader");
			this.reader = reader;
		}

		VorbisAudioStream(IContainerReader containerReader) : this(new VorbisReader(containerReader)) { }
		VorbisAudioStream(IPacketProvider packetProvider) : this(new VorbisReader(packetProvider)) { }
		public VorbisAudioStream(Stream source, bool closeSource = false) : this(new VorbisReader(source, closeSource)) { }
		public VorbisAudioStream(string path) : this(GetStream(path)) { }
		public VorbisAudioStream(FileInfo file) : this(GetStream(file)) { }
		public VorbisAudioStream(byte[] data) : this(GetStream(data)) { }
		public VorbisAudioStream(byte[] data, int offset, int length) : this(GetStream(data, offset, length)) { }
		public VorbisAudioStream(IntPtr data, int length) : this(GetStream(data, length)) { }
		unsafe public VorbisAudioStream(void* data, int length) : this(GetStream(data, length)) { }

		TimeSpan FromBytes(long bytes) { return TimeSpan.FromSeconds(bytes / (double)reader.Channels / (double)reader.SampleRate / 4.0); }

		public override int Read(byte[] buffer, int offset, int count)
		{
			int samples = count / reader.Channels / 4;

			if (floats.Length < samples)
				floats = new float[samples * 2];
			int read = reader.ReadSamples(floats, 0, samples) * reader.Channels;
			IntPtr pointer;
			using(buffer.Pin(out pointer, offset, read * 4))
				Marshal.Copy(floats, 0, pointer, read);
				
			return read;
		}

		long ToBytes(TimeSpan time) { return (long)(time.TotalSeconds * reader.Channels * reader.SampleRate * 4); }
	}

	/// <summary>
	/// This <see cref="AudioStream"/> mixes another <see cref="AudioStream"/> to only have one channel, allowing the<see cref="AudioSource"/> to be positioned in the world.
	/// </summary>
	public class MixAudioStream : FilterAudioStream
	{
		byte[] data = new byte[1024];

		/// <summary>Get or set how the channels are mixed.</summary>
		public MixAudioStreamChannels Channels { get; set; }

		public MixAudioStream(AudioStream source, bool closeSourceOnClose = false, MixAudioStreamChannels channels = MixAudioStreamChannels.Mix)
			: base(source, closeSourceOnClose)
		{
			Channels = channels;
		}

		public override AudioFormat Format
		{
			get
			{
				switch (Source.Format)
				{
					case AudioFormat.MonoByte:
					case AudioFormat.StereoByte: return AudioFormat.MonoByte;
					case AudioFormat.MonoInt16:
					case AudioFormat.StereoInt16: return AudioFormat.MonoInt16;
					case AudioFormat.MonoSingle:
					case AudioFormat.StereoSingle: return AudioFormat.MonoSingle;
					default: throw new NotSupportedException();
				}
			}
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			MixAudioStreamChannels channels = Channels;
			IntPtr pointer;

			using (buffer.Pin(out pointer, offset, count))
			{
				int sourceCount = count * Source.Format.SampleChannelCount();
				if (sourceCount > data.Length)
					data = new byte[sourceCount];
				int read = Source.Read(data, offset, sourceCount);

				switch (Source.Format)
				{
					case AudioFormat.MonoByte:
					case AudioFormat.MonoInt16:
					case AudioFormat.MonoSingle:
						return Source.Read(buffer, offset, count);

					case AudioFormat.StereoByte:
						for (int index = 0; index < read - 1; index += 2)
						{
							switch (channels)
							{
								case MixAudioStreamChannels.Left: buffer[index / 2] = data[index]; break;
								case MixAudioStreamChannels.Right: buffer[index / 2] = data[index + 1]; break;
								case MixAudioStreamChannels.Mix: buffer[index / 2] = (byte)(((data[index] - 128) + (data[index + 1] - 128)) / 2 + 128); break;
								default: throw new NotSupportedException();
							}
						}
						return read / 2;

					case AudioFormat.StereoInt16:
						for (int index = 0; index < read - 3; index += 4)
						{
							short left = BitConverter.ToInt16(data, index);
							short right = BitConverter.ToInt16(data, index + 2);
							short output;

							switch (channels)
							{
								case MixAudioStreamChannels.Left: output = left; break;
								case MixAudioStreamChannels.Right: output = right; break;
								case MixAudioStreamChannels.Mix: output = (short)((left + right) / 2); break;
								default: throw new NotSupportedException();
							}

							buffer[index / 2] = (byte)output;
							buffer[index / 2 + 1] = (byte)(output >> 8);
						}
						return read / 2;

					case AudioFormat.StereoSingle:
						for (int index = 0; index < read - 7; index += 8)
						{
							float left = BitConverter.ToSingle(data, index);
							float right = BitConverter.ToSingle(data, index + 4);
							float output;

							switch (channels)
							{
								case MixAudioStreamChannels.Left: output = left; break;
								case MixAudioStreamChannels.Right: output = right; break;
								case MixAudioStreamChannels.Mix: output = (short)((left + right) / 2); break;
								default: throw new NotSupportedException();
							}

							Marshal.WriteInt32(pointer + index / 2, output.ToBits());
						}
						return read / 2;

					default:
						throw new NotSupportedException();
				}
			}
		}
	}
}
