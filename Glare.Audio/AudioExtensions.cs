using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Audio
{
	public static class AudioExtensions
	{
		public static Type SampleChannelType(this AudioFormat format)
		{
			switch (format)
			{
				case AudioFormat.StereoByte:
				case AudioFormat.MonoByte: return typeof(byte);
				case AudioFormat.MonoDouble:
				case AudioFormat.StereoDouble: return typeof(double);
				case AudioFormat.MonoInt16:
				case AudioFormat.StereoInt16: return typeof(short);
				case AudioFormat.MonoSingle:
				case AudioFormat.StereoSingle: return typeof(float);
				default: throw new ArgumentException("format");
			}
		}

		public static int SampleChannelCount(this AudioFormat format)
		{
			switch (format)
			{
				case AudioFormat.MonoByte:
				case AudioFormat.MonoDouble:
				case AudioFormat.MonoInt16:
				case AudioFormat.MonoSingle:
					return 1;
				case AudioFormat.StereoByte:
				case AudioFormat.StereoDouble:
				case AudioFormat.StereoInt16:
				case AudioFormat.StereoSingle:
					return 2;
				default: throw new ArgumentException("format");
			}
		}

		public static int SampleByteSize(this AudioFormat format)
		{
			switch (format)
			{
				case AudioFormat.MonoByte: return 1;
				case AudioFormat.MonoDouble: return 8;
				case AudioFormat.MonoInt16: return 2;
				case AudioFormat.MonoSingle: return 4;
				case AudioFormat.StereoByte: return 1 * 2;
				case AudioFormat.StereoDouble: return 8 * 2;
				case AudioFormat.StereoInt16: return 2 * 2;
				case AudioFormat.StereoSingle: return 4 * 2;
				default: throw new ArgumentException("format");
			}
		}
	}
}
