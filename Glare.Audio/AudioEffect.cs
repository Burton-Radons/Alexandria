using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Audio
{
	public abstract class AudioEffect : AudioResource
	{
		internal readonly ObservableCollection<AudioSource> sources = new ObservableCollection<AudioSource>();
		readonly ReadOnlyObservableCollection<AudioSource> sourcesReadOnly;

		/// <summary>Get the collection of sources that use this effect.</summary>
		public ReadOnlyCollection<AudioSource> Sources { get { return sourcesReadOnly; } }

		/// <summary>
		/// Get the type of the audio effect.
		/// </summary>
		public AudioEffectType Type
		{
			get
			{
				int result;

				unsafe
				{
					using (Context.Bind())
						Context.AlGetEffecti(Id, AlEfxEnums.EffectType, &result);
				}

				return (AudioEffectType)result;
			}
		}

		public AudioEffect(AudioContext context, AudioEffectType type)
			: base(context, Create(context))
		{
			using (Context.Bind())
				Context.AlEffecti(Id, AlEfxEnums.EffectType, (int)type);
			sourcesReadOnly = new ReadOnlyObservableCollection<AudioSource>(sources);
		}


		static int Create(AudioContext context)
		{
			using (context.Bind())
			{
				int output;
				unsafe
				{
					context.AlGenEffects(1, &output);
					return output;
				}
			}
		}

		protected override void DisposeBase()
		{
			int id = Id;
			base.DisposeBase();
			if (Context.MakeCurrent())
				unsafe { Context.AlDeleteEffects(1, &id); }
		}

		float[] floats = new float[6];

		Vector3d Get3d(AlEfxEnums param)
		{
			unsafe
			{
				lock (floats)
					fixed (float* pointer = floats)
						using (Context.Bind())
							Context.AlGetEffectfv(Id, param, pointer);
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
					Context.AlGetEffecti(Id, param, &result);
				return result;
			}
		}

		float Getd(AlEfxEnums param)
		{
			unsafe
			{
				float result;
				using (Context.Bind())
					Context.AlGetEffectf(Id, param, &result);
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

		public class AutoWah : AudioEffect
		{
			// TODO
			public AutoWah(AudioContext context) : base(context, AudioEffectType.AutoWah) { }
		}

		/// <summary>
		/// This effect essentially replays the input audio accompanied by another slightly delayed version of the signal. This was originally intended to emulate the effect of several musicians playing the same notes simultaneously to create a thicker, more satisfying sound. The delay time of the delayed versions is modulated by an adjustable oscillating waveform to add some variation, causing some subtle shifts in the pitch of the delayed signals to emphasize the thickening effect.
		/// </summary>
		public class Chorus : AudioEffect
		{
			/// <summary>Get or set the average amount of time the sample is delayed before it is played back, and with <see cref="Feedback"/>, the amount of time between iterations of the sample. The default is 0.016s and values are clamped to the range 0 to 0.016s. Larger values lower the pitch. Smaller values make the chorus sound like a <see cref="Flanger"/>, but with different frequency characteristics.</summary>
			public TimeSpan Delay
			{
				get { return TimeSpan.FromSeconds(Getd(AlEfxEnums.Chorus_Delay)); }
				set { Set(AlEfxEnums.Chorus_Delay, value.TotalSeconds); }
			}

			/// <summary>Get or set the amount by which the delay time is modulated by the low-frequency oscillation. The default is 0.1 and values are clamped to 0.0 to 1.0.</summary>
			public double Depth
			{
				get { return Getd(AlEfxEnums.Chorus_Depth); }
				set { Set(AlEfxEnums.Chorus_Depth, value); }
			}

			/// <summary>Get or set the amount of the processed signal that is fed back to the input of the chorus effect. The default is 0.25 and values are clamped to -1 to 1. Negative values reverse the phase of the feedback signal. At full magnitude the identical sample will repeat endlessly. At lower magnitudes the sample will repeat and fade out over time. This parameter creates a "cascading" chorus effect.</summary>
			public double Feedback
			{
				get { return Getd(AlEfxEnums.Chorus_Feedback); }
				set { Set(AlEfxEnums.Chorus_Feedback, value); }
			}

			/// <summary>Get or set the phase difference between the left and right low frequency oscillations (LFO). The default is 90° and values range from -180° to 180°. At 0° the two LFOs are synchronized. This parameter creates the illusion of an expanded stereo field of the output signal.</summary>
			public Angle Phase
			{
				get { return Angle.Degrees(Geti(AlEfxEnums.Chorus_Phase)); }
				set { Set(AlEfxEnums.Chorus_Phase, (int)value.InDegrees); }
			}

			/// <summary>Get or set the modulation rate of the low frequency oscillation that controls the delay time of the delayed signals. The default is 1.1 and values are clamped to 0 to 10.</summary>
			public double Rate
			{
				get { return Getd(AlEfxEnums.Chorus_Rate); }
				set { Set(AlEfxEnums.Chorus_Rate, value); }
			}

			/// <summary>Get or set the waveform shape of the low frequency oscillation that controls the delay time of the delayed signals. The default is <see cref="ChorusWaveform.Triangle"/> and the other value is <see cref="ChorusWaveform.Sine"/>.</summary>
			public ChorusWaveform Waveform
			{
				get { return (ChorusWaveform)Geti(AlEfxEnums.Chorus_Waveform); }
				set { Set(AlEfxEnums.Chorus_Waveform, (int)value); }
			}

			public Chorus(AudioContext context) : base(context, AudioEffectType.Chorus) { }
		}

		public enum ChorusWaveform
		{
			Sine = 0,
			Triangle,
		}

		public class Compressor : AudioEffect
		{
			// TODO
			public Compressor(AudioContext context) : base(context, AudioEffectType.Compressor) { }
		}

		public class Distortion : AudioEffect
		{
			// TODO
			public Distortion(AudioContext context) : base(context, AudioEffectType.Distortion) { }
		}

#if false
		public class EaxReverb : AudioEffect
		{
			public double Density
			{
				get { return Getd(AlEfxEnums.EaxReverb_Density); }
				set { Set(AlEfxEnums.EaxReverb_Density, value); }
			}

			public double Diffusion
			{
				get { return Getd(AlEfxEnums.EaxReverb_Diffusion); }
				set { Set(AlEfxEnums.EaxReverb_Diffusion, value); }
			}

			public double Gain
			{
				get { return Getd(AlEfxEnums.EaxReverb_Gain); }
				set { Set(AlEfxEnums.EaxReverb_Gain, value); }
			}

			public double GainHF
			{
				get { return Getd(AlEfxEnums.EaxReverb_GainHF); }
				set { Set(AlEfxEnums.EaxReverb_GainHF, value); }
			}

			public double GainLF
			{
				get { return Getd(AlEfxEnums.EaxReverb_GainLF); }
				set { Set(AlEfxEnums.EaxReverb_GainLF, value); }
			}

			public TimeSpan DecayTime
			{
				get { return TimeSpan.FromSeconds(Getd(AlEfxEnums.EaxReverb_DecayTime)); }
				set { Set(AlEfxEnums.EaxReverb_DecayTime, value.TotalSeconds); }
			}

			public double DecayHFRatio
			{
				get { return Getd(AlEfxEnums.EaxReverb_DecayHFRatio); }
				set { Set(AlEfxEnums.EaxReverb_DecayHFRatio, value); }
			}

			public double DecayLFRatio
			{
				get { return Getd(AlEfxEnums.EaxReverb_DecayLFRatio); }
				set { Set(AlEfxEnums.EaxReverb_DecayLFRatio, value); }
			}

			public double ReflectionsGain
			{
				get { return Getd(AlEfxEnums.EaxReverb_ReflectionsGain); }
				set { Set(AlEfxEnums.EaxReverb_ReflectionsGain, value); }
			}

			public TimeSpan ReflectionsDelay
			{
				get { return TimeSpan.FromSeconds(Getd(AlEfxEnums.EaxReverb_ReflectionsDelay)); }
				set { Set(AlEfxEnums.EaxReverb_ReflectionsDelay, value.TotalSeconds); }
			}

			public Vector3d ReflectionsPan
			{
				get { return Get3d(AlEfxEnums.EaxReverb_ReflectionsPan); }
				set { Set(AlEfxEnums.EaxReverb_ReflectionsPan, value); }
			}

			public double LateReverbGain
			{
				get { return Getd(AlEfxEnums.EaxReverb_LateReverbGain); }
				set { Set(AlEfxEnums.EaxReverb_LateReverbGain, value); }
			}

			public TimeSpan LateReverbDelay
			{
				get { return TimeSpan.FromSeconds(Getd(AlEfxEnums.EaxReverb_LateReverbDelay)); }
				set { Set(AlEfxEnums.EaxReverb_LateReverbDelay, value.TotalSeconds); }
			}

			public Vector3d LateReverbPan
			{
				get { return Get3d(AlEfxEnums.EaxReverb_LateReverbPan); }
				set { Set(AlEfxEnums.EaxReverb_LateReverbPan, value); }
			}

			public double EchoTime
			{
				get { return Getd(AlEfxEnums.EaxReverb_EchoTime); }
				set { Set(AlEfxEnums.EaxReverb_EchoTime, value); }
			}

			public double EchoDepth
			{
				get { return Getd(AlEfxEnums.EaxReverb_EchoDepth); }
				set { Set(AlEfxEnums.EaxReverb_EchoDepth, value); }
			}

			public double ModulationTime
			{
				get { return Getd(AlEfxEnums.EaxReverb_ModulationTime); }
				set { Set(AlEfxEnums.EaxReverb_ModulationTime, value); }
			}

			public double ModulationDepth
			{
				get { return Getd(AlEfxEnums.EaxReverb_ModulationDepth); }
				set { Set(AlEfxEnums.EaxReverb_ModulationDepth, value); }
			}

			public double AirAbsorptionGainHF
			{
				get { return Getd(AlEfxEnums.EaxReverb_AirAbsorptionGainHF); }
				set { Set(AlEfxEnums.EaxReverb_AirAbsorptionGainHF, value); }
			}

			public Frequency HFReference
			{
				get { return Frequency.Hertz(Getd(AlEfxEnums.EaxReverb_HFReference)); }
				set { Set(AlEfxEnums.EaxReverb_HFReference, value.InHertz); }
			}

			public Frequency LFReference
			{
				get { return Frequency.Hertz(Getd(AlEfxEnums.EaxReverb_LFReference)); }
				set { Set(AlEfxEnums.EaxReverb_LFReference, value.InHertz); }
			}

			public double RoomRolloffFactor
			{
				get { return Getd(AlEfxEnums.EaxReverb_RoomRolloffFactor); }
				set { Set(AlEfxEnums.EaxReverb_RoomRolloffFactor, value); }
			}

			public double DecayHFLimit
			{
				get { return Getd(AlEfxEnums.EaxReverb_DecayHFLimit); }
				set { Set(AlEfxEnums.EaxReverb_DecayHFLimit, value); }
			}

			public EaxReverb(AudioContext context) : base(context, AudioEffectType.EaxReverb) { }
		}
#endif

		public class Echo : AudioEffect
		{
			// TODO
			public Echo(AudioContext context) : base(context, AudioEffectType.Echo) { }
		}

		public class Equalizer : AudioEffect
		{
			// TODO
			public Equalizer(AudioContext context) : base(context, AudioEffectType.Equalizer) { }
		}

		public class Flanger : AudioEffect
		{
			// TODO
			public Flanger(AudioContext context) : base(context, AudioEffectType.Flanger) { }
		}

		public class FrequencyShifter : AudioEffect
		{
			// TODO
			public FrequencyShifter(AudioContext context) : base(context, AudioEffectType.FrequencyShifter) { }
		}

		public class Null : AudioEffect
		{
			public Null(AudioContext context) : base(context, AudioEffectType.Null) { }
		}

		public class PitchShifter : AudioEffect
		{
			// TODO
			public PitchShifter(AudioContext context) : base(context, AudioEffectType.PitchShifter) { }
		}

		/// <summary>An environmental reverberation effect.</summary>
		public class Reverb : AudioEffect
		{
			/// <summary>Get or set the reverberation decay time. The default is 1.49, and it is clamped to 0.1 (a small room with very dead surfaces) to 20 (a large room with very live surfaces).</summary>
			public TimeSpan DecayTime
			{
				get { return TimeSpan.FromSeconds(Getd(AlEfxEnums.Reverb_DecayTime)); }
				set { Set(AlEfxEnums.Reverb_DecayTime, value.TotalSeconds); }
			}

			/// <summary>Get or set the reverb modal density that controls the coloration of the late reverb. Lowering the value adds more coloration to the late reverb. The default is 1 and values are clamped to 0 to 1.</summary>
			public double Density
			{
				get { return Getd(AlEfxEnums.Reverb_Density); }
				set { Set(AlEfxEnums.Reverb_Density, value); }
			}

			/// <summary>Get or set the reverb diffusion that controls the echo density in the reverberation decay. The default is 1 and values range from 0 to 1. Reducing diffusion gives the reverberation a more "grainy" character that is especially noticeable with percussive sound sources. If you set the value to 0, the later reverberation sounds like a succession of distinct echoes.</summary>
			public double Diffusion
			{
				get { return Getd(AlEfxEnums.Reverb_Diffusion); }
				set { Set(AlEfxEnums.Reverb_Diffusion, value); }
			}

			/// <summary>Get or set the master volume control for the reflected sound (both early reflections and reverberation) that the reverb effect adds to all sound sources. The default is 0.32 and values are clamped to 0 (-100dB) to 1 (0dB).</summary>
			public Gain Gain
			{
				get { return Glare.Gain.Linear(Getd(AlEfxEnums.Reverb_Gain)); }
				set { Set(AlEfxEnums.Reverb_Gain, value.InLinear); }
			}

			/// <summary>
			/// Get or set the distance-dependent attenuation at high frequencies caused by the propagation medium. The default is 0.994/m and values range from 0.892/m to 1/m. This applies to reflected sound only. This simulates sound transmission through foggy air, dry air, smoky atmosphere, and so on. The default of 0.994 roughly corresponds to typical conditions. Lowering the value simulates a more absorbent medium (more humidity in the air, for example), and raising the value simulates a less absorbent medium (dry desert air, for example).
			/// </summary>
			public Attenuation HighFrequencyAirAbsorption
			{
				get { return Attenuation.PerMetre(Getd(AlEfxEnums.Reverb_AirAbsorptionGainHF)); }
				set { Set(AlEfxEnums.Reverb_AirAbsorptionGainHF, value.InPerMetre); }
			}

			/// <summary>
			/// Get or set whether the high-frequency decay time automatically stays below a limit value that's derived from <see cref="HighFrequencyAirAbsorption"/>. This limit applies regardless of the value of <see cref="HighFrequencyDecayRatio"/>. While this limit is on, a more natural-sounding reverberation decay is maintained that allows you to increase the value of <see cref="DecayTime"/> without risking an unnaturally long decay time at high frequencies.
			/// </summary>
			public bool HighFrequencyDecayLimit
			{
				get { return Geti(AlEfxEnums.Reverb_DecayHFLimit) != 0; }
				set { Set(AlEfxEnums.Reverb_DecayHFLimit, value ? 1 : 0); }
			}

			/// <summary>Get or set the high-frequency quality of the <see cref="DecayTime"/>. The default is 0.83 and values are clamped to 0.1 to 2.0. You hear a more brilliant reverberation with a longer decay at high frequencies, and a more natural reverberation with a shorter decay.</summary>
			public double HighFrequencyDecayRatio
			{
				get { return Getd(AlEfxEnums.Reverb_DecayHFRatio); }
				set { Set(AlEfxEnums.Reverb_DecayHFRatio, value); }
			}

			/// <summary>Get or set the high frequency master control volume for the reflected sound. This controls a low-pass filter that applies globally to the reflected sound of all sound sources feeding the particular instance of the reverb effect.</summary>
			public Gain HighFrequencyGain
			{
				get { return Glare.Gain.Linear(Getd(AlEfxEnums.Reverb_GainHF)); }
				set { Set(AlEfxEnums.Reverb_GainHF, value.InLinear); }
			}

			/// <summary>Get or set the overall amount of later reverberation relative to <see cref="Gain"/>. The default is 1.26 (2 dB) and ranges from 0 (-100 dB) to 10 (20 dB).</summary>
			public Gain LateReverbGain
			{
				get { return Gain.Linear(Getd(AlEfxEnums.Reverb_LateReverbGain)); }
				set { Set(AlEfxEnums.Reverb_LateReverbGain, value.InLinear); }
			}

			/// <summary>Get or set the time of the beginning of the late reverberation relative to the time of the initial reflection. The default is 11ms and values are clamped to 0ms to 100ms. Reducing or increasing the delay is useful for simulating a smaller or larger room.</summary>
			public TimeSpan LateReverbDelay
			{
				get { return TimeSpan.FromSeconds(Getd(AlEfxEnums.Reverb_LateReverbDelay)); }
				set { Set(AlEfxEnums.Reverb_LateReverbDelay, value.TotalSeconds); }
			}

			/// <summary>Get or set the amount of delay between the arrival time of the direct path from the source to the first reflection from the source. The default is 0.007s and values are clamped to 0 to 300 milliseconds. Reducing the reflections delay simulates closer or more distant reflective surfaces, and therefore the perceived size of the room.</summary>
			public TimeSpan ReflectionsDelay
			{
				get { return TimeSpan.FromSeconds(Getd(AlEfxEnums.Reverb_ReflectionsDelay)); }
				set { Set(AlEfxEnums.Reverb_ReflectionsDelay, value.TotalSeconds); }
			}

			/// <summary>Get or set the overall amount of initial reflections relative to <see cref="Gain"/>. The default is 0.05, and values range from 0.0 (-100dB) to 3.16 (+10dB). Increasing this value simulates a more narrow space or closer walls. To simulate open or semi-open environments, you can maintain this value but reduce the value of the <see cref="LateReverbGain"/> property.</summary>
			public Gain ReflectionsGain
			{
				get { return Gain.Linear(Getd(AlEfxEnums.Reverb_ReflectionsGain)); }
				set { Set(AlEfxEnums.Reverb_ReflectionsGain, value.InLinear); }
			}

			/// <summary>
			/// Get or set the room rolloff factor. The default is 0 and values are clamped to 0 to 10. A value of 1 specifies that the reflected sound will decay by 6 dB every time the distance doubles. Any value other than 1 is equivalent to a scaling factor applied to ((Source listener distance) - <see cref="AudioSource.ReferenceDistance"/>). This has no effect if <see cref="AudioSource.AuxiliarySendFilterGainAuto"/> is <c>true</c> in the <see cref="AudioSource"/>, which is the default.
			/// </summary>
			public double RoomRolloffFactor
			{
				get { return Getd(AlEfxEnums.Reverb_RoomRolloffFactor); }
				set { Set(AlEfxEnums.Reverb_RoomRolloffFactor, value); }
			}

			public Reverb(AudioContext context) : base(context, AudioEffectType.Reverb) { }
		}

		public class RingModulator : AudioEffect
		{
			// TODO
			public RingModulator(AudioContext context) : base(context, AudioEffectType.RingModulator) { }
		}

		public class VocalMorpher : AudioEffect
		{
			// TODO
			public VocalMorpher(AudioContext context) : base(context, AudioEffectType.VocalMorpher) { }
		}
	}
}
