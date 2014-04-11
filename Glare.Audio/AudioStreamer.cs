using Glare.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Audio
{
	/// <summary>
	/// This streams <see cref="AudioBuffer"/> chunks to an <see cref="AudioSource"/> by streaming from a <see cref="AudioStream"/> that decompresses formats such as Mp3 or Vorbis.
	/// </summary>
	public class AudioStreamer : IDisposable
	{
		readonly AudioSource source;
		readonly AudioStream stream;
		readonly byte[] bufferSamples;
		readonly AudioBuffer[] buffers;
		readonly int bufferSampleCount;
		int id, currentBuffer, updateCount;
		static int nextId;

		public AudioContext Context { get { return source.Context; } }

		public AudioDevice Device { get { return source.Device; } }

		/// <summary>Get the <see cref="AudioSource"/> that this is streaming to.</summary>
		public AudioSource Source { get { return source; } }

		/// <summary>Get the <see cref="AudioStream"/> that this is streaming from.</summary>
		public AudioStream Stream { get { return stream; } }

		public int UpdateCount { get { return updateCount; } }

		public AudioStreamer(AudioSource source, AudioStream stream, int bufferSampleCount)
		{
			if (source == null)
				throw new ArgumentNullException("source");
			if (stream == null)
				throw new ArgumentNullException("stream");
			this.source = source;
			this.stream = stream;
			this.bufferSampleCount = bufferSampleCount;
			this.bufferSamples = new byte[stream.Format.SampleByteSize() * bufferSampleCount];

			buffers = new AudioBuffer[4];
			for (int index = 0; index < buffers.Length; index++)
			{
				buffers[index] = new AudioBuffer(Context);
				FillBuffer(buffers[index]);
			}

			source.Queue(buffers);

			lock (Context.streamers)
			{
				id = nextId++;
				Context.streamers[id] = this;
			}
		}

		~AudioStreamer()
		{
			Dispose();
		}

		public AudioStreamer(AudioSource source, AudioStream stream, TimeSpan bufferTime) : this(source, stream, BufferTimeToSamples(stream, bufferTime)) { }

		public AudioStreamer(AudioSource source, AudioStream stream) : this(source, stream, TimeSpan.FromSeconds(1.2)) { }

		static int BufferTimeToSamples(AudioStream stream, TimeSpan time)
		{
			if (stream == null)
				throw new ArgumentNullException("stream");
			return (int)(stream.Frequency.InHertz * time.TotalSeconds);
		}

		public void Dispose()
		{
			lock (Context.streamers)
				Context.streamers.Remove(id);
		}

		void FillBuffer(AudioBuffer buffer)
		{
			int read = stream.Read(bufferSamples, 0, bufferSamples.Length);
			buffer.Data(stream.Format, bufferSamples, 0, read / stream.Format.SampleByteSize(), stream.Frequency);
		}

		internal void Update()
		{
			if (source.BuffersProcessed > 1)
			{
				updateCount++;
				object proca = source.BuffersProcessed, quea = source.BuffersQueued, statea = source.State;
				source.Unqueue();
				object procb = source.BuffersProcessed, queb = source.BuffersQueued, stateb = source.State;
				FillBuffer(buffers[currentBuffer]);
				object procc = source.BuffersProcessed, quec = source.BuffersQueued, statec = source.State;
				source.Queue(buffers[currentBuffer]);
				object procd = source.BuffersProcessed, qued = source.BuffersQueued, stated = source.State;
				currentBuffer = (currentBuffer + 1) % buffers.Length;
				source.Play();
			}
		}
	}
}
