using OpenTK.Audio.OpenAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Audio
{
	public enum AudioFilterType
	{
		Null = 0,
		LowPass = 1,
		HighPass = 2,
		BandPass = 3,
	}

	public abstract class AudioFilter : AudioResource
	{
		public AudioFilter(AudioContext context, AudioFilterType type) : base(context, Create(context))
		{
			using (context.Bind())
				context.AlFilteri(Id, AlEfxEnums.FilterType, (int)type);
		}

		static int Create(AudioContext context)
		{
			unsafe
			{
				int output;
				using (context.Bind())
					context.AlGenFilters(1, &output);
				return output;
			}
		}

		protected override void DisposeBase()
		{
			unsafe
			{
				int id = Id;
				if (Context.MakeCurrent())
					Context.AlDeleteFilters(1, &id);
				base.DisposeBase();
			}
		}



		float[] floats = new float[6];

		Vector3d Get3d(AlEfxEnums param)
		{
			unsafe
			{
				lock (floats)
					fixed (float* pointer = floats)
						using (Context.Bind())
							Context.AlGetFilterfv(Id, param, pointer);
				return new Vector3d(floats[0], floats[1], floats[2]);
			}
		}

		Vector3 Get3(AlEfxEnums param) { return Vector3.Metres(Get3d(param)); }

		int Geti(AlEfxEnums param)
		{
			unsafe
			{
				int result;
				using (Context.Bind())
					Context.AlGetFilteri(Id, param, &result);
				return result;
			}
		}

		float Getd(AlEfxEnums param)
		{
			unsafe
			{
				float result;
				using (Context.Bind())
					Context.AlGetFilterf(Id, param, &result);
				return result;
			}
		}

		void Set(AlEfxEnums param, int value)
		{
			unsafe
			{
				using (Context.Bind())
					Context.AlEffecti(Id, param, value);
			}
		}

		void Set(AlEfxEnums param, double value)
		{
			unsafe
			{
				using (Context.Bind())
					Context.AlEffectf(Id, param, (float)value);
			}
		}

		void Set(AlEfxEnums param, Vector3d value)
		{
			unsafe
			{
				Vector3f value3f = (Vector3f)value;
				using (Context.Bind())
					Context.AlEffectfv(Id, param, &value3f.X);
			}
		}

		public class Null : AudioFilter
		{
			public Null(AudioContext context) : base(context, AudioFilterType.Null) { }
		}

		/// <summary>Used to remove high-frequency content from a signal.</summary>
		public class LowPass : AudioFilter
		{
			public double Gain
			{
				get { return Getd(AlEfxEnums.LowPass_Gain); }
				set { Set(AlEfxEnums.LowPass_Gain, value); }
			}

			public double GainHF
			{
				get { return Getd(AlEfxEnums.LowPass_GainHF); }
				set { Set(AlEfxEnums.LowPass_GainHF, value); }
			}

			public LowPass(AudioContext context) : base(context, AudioFilterType.LowPass) { }
		}

		/// <summary>Used to remove low-frequency content from a signal.</summary>
		public class HighPass : AudioFilter
		{
			public double Gain
			{
				get { return Getd(AlEfxEnums.HighPass_Gain); }
				set { Set(AlEfxEnums.HighPass_Gain, value); }
			}

			public double GainLF
			{
				get { return Getd(AlEfxEnums.HighPass_GainLF); }
				set { Set(AlEfxEnums.HighPass_GainLF, value); }
			}

			public HighPass(AudioContext context) : base(context, AudioFilterType.HighPass) { }
		}

		/// <summary>Used to remove high and low frequency content from a signal.</summary>
		public class BandPass : AudioFilter
		{
			public double Gain
			{
				get { return Getd(AlEfxEnums.BandPass_Gain); }
				set { Set(AlEfxEnums.BandPass_Gain, value); }
			}

			public double GainLF
			{
				get { return Getd(AlEfxEnums.BandPass_GainLF); }
				set { Set(AlEfxEnums.BandPass_GainLF, value); }
			}

			public double GainHF
			{
				get { return Getd(AlEfxEnums.BandPass_GainHF); }
				set { Set(AlEfxEnums.BandPass_GainHF, value); }
			}

			public BandPass(AudioContext context) : base(context, AudioFilterType.BandPass) { }
		}
	}
}
