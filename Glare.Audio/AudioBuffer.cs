using OpenTK.Audio.OpenAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Glare.Internal;
using ToyMp3;
using System.Collections.ObjectModel;

namespace Glare.Audio
{
	/// <summary>
	/// Storage for sample data.
	/// </summary>
	public class AudioBuffer : AudioResource
	{
		ObservableCollection<AudioSource> sources = new ObservableCollection<AudioSource>();
		ReadOnlyObservableCollection<AudioSource> sourcesReadOnly;
		AudioFormat format;

		/// <summary>Get the number of bits in a sample in the buffer data.</summary>
		public int BitsPerSample { get { return GetInt(ALGetBufferi.Bits); } }

		/// <summary>Get the number of channels in the buffer data.</summary>
		public int Channels { get { return GetInt(ALGetBufferi.Channels); } }

		public AudioFormat Format { get { return (AudioFormat)format; } }

		/// <summary>Get the frequency of the buffer data.</summary>
		public Frequency Frequency { get { return Frequency.Hertz(GetInt(ALGetBufferi.Frequency)); } }

		/// <summary>Get the size in bytes of the buffer data.</summary>
		public int Size { get { return GetInt(ALGetBufferi.Size); } }

		/// <summary>Get the collection of sources that have queued this buffer.</summary>
		public ReadOnlyObservableCollection<AudioSource> Sources { get { return sourcesReadOnly; } }

		public AudioBuffer(AudioContext context)
			: base(context, Create(context))
		{
			lock(context.buffers)
				context.buffers[Id] = new WeakReference<AudioBuffer>(this);
			sourcesReadOnly = new ReadOnlyObservableCollection<AudioSource>(sources);
		}

		static int Create(AudioContext context)
		{
			using (context.Bind())
				return AL.GenBuffer();
		}

		public void Data(AudioFormat format, IntPtr buffer, int bytes, Frequency frequency)
		{
			int bytesPerSample = format.SampleByteSize();
			using (Context.Bind())
				AL.BufferData(Id, (ALFormat)format, buffer, bytes / bytesPerSample * bytesPerSample, (int)Math.Round(frequency.InHertz));
			this.format = format;
		}

		public void Data<T>(AudioFormat format, T[] data, int first, int count, Frequency frequency) where T : struct
		{
			IntPtr buffer;
			using (data.Pin(out buffer, first, count))
			{
				Data(format, buffer, count * Marshal.SizeOf(typeof(T)), frequency);
			}
		}

		public void Data<T>(AudioFormat format, T[] data, Frequency frequency) where T : struct { Data(format, data, 0, data != null ? data.Length : 0, frequency); }

		public void DataPCMMono(byte[] pcmData, int first, int count, Frequency frequency) { Data(AudioFormat.MonoByte, pcmData, first, count, frequency); }
		public void DataPCMMono(double[] pcmData, int first, int count, Frequency frequency) { Data(AudioFormat.MonoDouble, pcmData, first, count, frequency); }
		public void DataPCMMono(float[] pcmData, int first, int count, Frequency frequency) { Data(AudioFormat.MonoSingle, pcmData, first, count, frequency); }
		public void DataPCMMono(short[] pcmData, int first, int count, Frequency frequency) { Data(AudioFormat.MonoInt16, pcmData, first, count, frequency); }

		public void DataPCMStereo(byte[] pcmData, int first, int count, Frequency frequency) { Data(AudioFormat.StereoByte, pcmData, first, count, frequency); }
		public void DataPCMStereo(double[] pcmData, int first, int count, Frequency frequency) { Data(AudioFormat.StereoDouble, pcmData, first, count, frequency); }
		public void DataPCMStereo(float[] pcmData, int first, int count, Frequency frequency) { Data(AudioFormat.StereoSingle, pcmData, first, count, frequency); }
		public void DataPCMStereo(short[] pcmData, int first, int count, Frequency frequency) { Data(AudioFormat.StereoInt16, pcmData, first, count, frequency); }

		public void DataFile(AudioFormat format, string path, Frequency frequency) { Data(format, File.ReadAllBytes(path), frequency); }
		public void DataFile(AudioFormat format, string path) { DataFile(format, path, Frequency.Zero); }

		protected override void DisposeBase()
		{
			if (Context.MakeCurrent())
				AL.DeleteBuffer(Id);
			lock(Context.buffers)
				Context.buffers.Remove(Id);
			base.DisposeBase();
		}

		internal int GetInt(ALGetBufferi param) { int result; using (Context.Bind()) AL.GetBuffer(Id, param, out result); return result; }


	}
}
