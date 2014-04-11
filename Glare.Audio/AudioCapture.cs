using Glare.Internal;
using OpenTK.Audio.OpenAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Audio
{
	/// <summary>
	/// An audio capture context on a <see cref="AudioDevice"/>.
	/// </summary>
	public class AudioCapture : DisposableObject
	{
		AudioDevice device;
		IntPtr handle;
		AudioFormat format;

		public AudioDevice Device { get { return device; } }

		internal AudioCapture(AudioDevice device, IntPtr handle, AudioFormat format)
		{
			this.device = device;
			this.handle = handle;
			this.format = format;
		}

		protected override void DisposeBase()
		{
			Alc.CaptureCloseDevice(handle);
			handle = IntPtr.Zero;
		}

		public void Read(Array data, int first, int sampleCount)
		{
			IntPtr pointer;
			using (data.Pin(out pointer, first, sampleCount))
				Read(pointer, sampleCount);
		}

		public void Read<T>(T[] data)
		{
			if (data == null)
				throw new ArgumentNullException("data");
			Read(data, 0, data.Length * Marshal.SizeOf(typeof(T)) / format.SampleByteSize());
		}

		public void Read(IntPtr buffer, int sampleCount) { Alc.CaptureSamples(handle, buffer, sampleCount); }

		public void Read<T>(ref T data, int sampleCount) where T : struct { Alc.CaptureSamples(handle, ref data, sampleCount); }

		public T[] Read<T>(int sampleCount) where T : struct
		{
			T[] data = new T[sampleCount * format.SampleByteSize() / Marshal.SizeOf(typeof(T))];
			Read(data, 0, sampleCount);
			return data;
		}

		public void Start() { Alc.CaptureStart(handle); }

		public void Stop() { Alc.CaptureStop(handle); }
	}
}
