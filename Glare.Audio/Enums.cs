using OpenTK.Audio.OpenAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Audio
{
	/// <summary>
	/// Formats with multiple channels will be played without 3D spatialization features, and are normally used for background music.
	/// </summary>
	public enum AudioFormat
	{
		None = 0,

		/// <summary>Single-channel unsigned 8-bit PCM, where 128 is the audio output level of 0.</summary>
		MonoByte = ALFormat.Mono8,

		/// <summary>Single-channel double-precision floating point PCM.</summary>
		MonoDouble = ALFormat.MonoDoubleExt,

		/// <summary>Single-channel signed 16-bit PCM.</summary>
		MonoInt16 = ALFormat.Mono16,

		MonoSingle = ALFormat.MonoFloat32Ext,

		/// <summary>Two-channel unsigned 8-bit PCM, where 128 is the audio output level of 0.</summary>
		StereoByte = ALFormat.Stereo8,

		StereoDouble = ALFormat.StereoDoubleExt,

		/// <summary>Two-channel signed 16-bit PCM.</summary>
		StereoInt16 = ALFormat.Stereo16,

		StereoSingle = ALFormat.StereoFloat32Ext,
	}
	/*
	MultiQuad8Ext = 4612,
	MultiQuad16Ext = 4613,#define AL_FORMAT_MONO_DOUBLE_EXT                0x10012
#define AL_FORMAT_STEREO_DOUBLE_EXT              0x10013
	MultiQuad32Ext = 4614,
	MultiRear8Ext = 4615,
	MultiRear16Ext = 4616,
	MultiRear32Ext = 4617,
	Multi51Chn8Ext = 4618,
	Multi51Chn16Ext = 4619,
	Multi51Chn32Ext = 4620,
	Multi61Chn8Ext = 4621,
	Multi61Chn16Ext = 4622,
	Multi61Chn32Ext = 4623,
	Multi71Chn8Ext = 4624,
	Multi71Chn16Ext = 4625,
	Multi71Chn32Ext = 4626,
	MonoIma4Ext = 4864,
	StereoIma4Ext = 4865,
	VorbisExt = 65539,
	MonoFloat32Ext = 65552,
	StereoFloat32Ext = 65553,
	MonoDoubleExt = 65554,
	StereoDoubleExt = 65555,
	MonoMuLawExt = 65556,
	StereoMuLawExt = 65557,
	MonoALawExt = 65558,
	StereoALawExt = 65559,
	Mp3Ext = 65568,*/


	/// <summary>
	/// This specifies the distance attenuation used for sample playback.
	/// </summary>
	public enum AudioDistanceModel
	{
		/// <summary>Sounds do not attenuate with distance.</summary>
		None = 0,
		InverseDistance = 53249,
		InverseDistanceClamped = 53250,
		LinearDistance = 53251,
		LinearDistanceClamped = 53252,
		ExponentDistance = 53253,
		ExponentDistanceClamped = 53254,
	}

	public enum AudioSourceState
	{
		Initial = ALSourceState.Initial,
		Paused = ALSourceState.Paused,
		Playing = ALSourceState.Playing,
		Stopped = ALSourceState.Stopped,
	}

	public enum AudioSourceType
	{
		/// <summary>The <see cref="AudioSource"/> is not attached to an <see cref="AudioBuffer"/>.</summary>
		Undetermined = ALSourceType.Undetermined,

		/// <summary>The <see cref="AudioSource"/> is attached to a static <see cref="AudioBuffer"/>.</summary>
		Static = ALSourceType.Static,

		/// <summary>The <see cref="AudioSource"/> is attached to a streaming <see cref="AudioBuffer"/>.</summary>
		Streaming = ALSourceType.Streaming,
	}

	/// <summary>
	/// This specifies how a <see cref="MixAudioStream"/> mixes together its samples.
	/// </summary>
	public enum MixAudioStreamChannels
	{
		/// <summary>Mix all channels together into a mono sample, producing the average.</summary>
		Mix,

		/// <summary>Select the left channel and produce a mono sample.</summary>
		Left,

		/// <summary>Select the right channel and produce a mono sample.</summary>
		Right,
	}

	/// <summary>
	/// This indicates the type of an <see cref="AudioEffect"/>.
	/// </summary>
	public enum AudioEffectType : int
	{
		Null = 0x0000,
#if false
		EaxReverb = 0x8000,
#endif
		Reverb = 0x0001,
		Chorus = 0x0002,
		Distortion = 0x0003,
		Echo = 0x0004,
		Flanger = 0x0005,
		FrequencyShifter = 0x0006,
		VocalMorpher = 0x0007,
		PitchShifter = 8,
		RingModulator = 9,
		AutoWah = 10,
		Compressor = 11,
		Equalizer = 12,
	}

	enum AlEfxEnums : int
	{
		FilterType = 0x8001,
		EffectType = 0x8001,
		FilterNull = 0,
		FilterLowPass = 1,
		FilterHighPass = 2,
		FilterBandPass = 3,
		EfxMajorVersion = 0x20001,
		EfxMinorVersion = 0x20002,
		MaxAuxiliarySends = 0x20003,

#if false
		EaxReverb_Density = 1,
		EaxReverb_Diffusion = 2,
		EaxReverb_Gain = 3,
		EaxReverb_GainHF = 4,
		EaxReverb_GainLF = 5,
		EaxReverb_DecayTime = 6,
		EaxReverb_DecayHFRatio = 7,
		EaxReverb_DecayLFRatio = 8,
		EaxReverb_ReflectionsGain = 9,
		EaxReverb_ReflectionsDelay = 10,
		EaxReverb_ReflectionsPan = 11,
		EaxReverb_LateReverbGain = 12,
		EaxReverb_LateReverbDelay = 13,
		EaxReverb_LateReverbPan = 14,
		EaxReverb_EchoTime = 15,
		EaxReverb_EchoDepth = 16,
		EaxReverb_ModulationTime = 17,
		EaxReverb_ModulationDepth = 18,
		EaxReverb_AirAbsorptionGainHF = 19,
		EaxReverb_HFReference = 20,
		EaxReverb_LFReference = 21,
		EaxReverb_RoomRolloffFactor = 22,
		EaxReverb_DecayHFLimit = 23,
#endif

		Reverb_Density = 1,
		Reverb_Diffusion = 2,
		Reverb_Gain = 3,
		Reverb_GainHF = 4,
		Reverb_DecayTime = 5,
		Reverb_DecayHFRatio = 6,
		Reverb_ReflectionsGain = 7,
		Reverb_ReflectionsDelay = 8,
		Reverb_LateReverbGain = 9,
		Reverb_LateReverbDelay = 10,
		Reverb_AirAbsorptionGainHF = 11,
		Reverb_RoomRolloffFactor = 12,
		Reverb_DecayHFLimit = 13,

		Chorus_Waveform = 1,
		Chorus_Phase = 2,
		Chorus_Rate = 3,
		Chorus_Depth = 4,
		Chorus_Feedback = 5,
		Chorus_Delay = 6,

#if false
		define AL_CHORUS_WAVEFORM                       0x0001
77 define AL_CHORUS_PHASE                          0x0002
78 define AL_CHORUS_RATE                           0x0003
79 define AL_CHORUS_DEPTH                          0x0004
80 define AL_CHORUS_FEEDBACK                       0x0005
81 define AL_CHORUS_DELAY                          0x0006
82
83 /* Distortion effect parameters */
84 define AL_DISTORTION_EDGE                       0x0001
85 define AL_DISTORTION_GAIN                       0x0002
86 define AL_DISTORTION_LOWPASS_CUTOFF             0x0003
87 define AL_DISTORTION_EQCENTER                   0x0004
88 define AL_DISTORTION_EQBANDWIDTH                0x0005
89
90 /* Echo effect parameters */
91 define AL_ECHO_DELAY                            0x0001
92 define AL_ECHO_LRDELAY                          0x0002
93 define AL_ECHO_DAMPING                          0x0003
94 define AL_ECHO_FEEDBACK                         0x0004
95 define AL_ECHO_SPREAD                           0x0005
96
97 /* Flanger effect parameters */
98 define AL_FLANGER_WAVEFORM                      0x0001
99 define AL_FLANGER_PHASE                         0x0002
100 define AL_FLANGER_RATE                          0x0003
101 define AL_FLANGER_DEPTH                         0x0004
102 define AL_FLANGER_FEEDBACK                      0x0005
103 define AL_FLANGER_DELAY                         0x0006
104
105 /* Frequency shifter effect parameters */
106 define AL_FREQUENCY_SHIFTER_FREQUENCY           0x0001
107 define AL_FREQUENCY_SHIFTER_LEFT_DIRECTION      0x0002
108 define AL_FREQUENCY_SHIFTER_RIGHT_DIRECTION     0x0003
109
110 /* Vocal morpher effect parameters */
111 define AL_VOCAL_MORPHER_PHONEMEA                0x0001
112 define AL_VOCAL_MORPHER_PHONEMEA_COARSE_TUNING  0x0002
113 define AL_VOCAL_MORPHER_PHONEMEB                0x0003
114 define AL_VOCAL_MORPHER_PHONEMEB_COARSE_TUNING  0x0004
115 define AL_VOCAL_MORPHER_WAVEFORM                0x0005
116 define AL_VOCAL_MORPHER_RATE                    0x0006
117
118 /* Pitchshifter effect parameters */
119 define AL_PITCH_SHIFTER_COARSE_TUNE             0x0001
120 define AL_PITCH_SHIFTER_FINE_TUNE               0x0002
121
122 /* Ringmodulator effect parameters */
123 define AL_RING_MODULATOR_FREQUENCY              0x0001
124 define AL_RING_MODULATOR_HIGHPASS_CUTOFF        0x0002
125 define AL_RING_MODULATOR_WAVEFORM               0x0003
126
127 /* Autowah effect parameters */
128 define AL_AUTOWAH_ATTACK_TIME                   0x0001
129 define AL_AUTOWAH_RELEASE_TIME                  0x0002
130 define AL_AUTOWAH_RESONANCE                     0x0003
131 define AL_AUTOWAH_PEAK_GAIN                     0x0004
132
133 /* Compressor effect parameters */
134 define AL_COMPRESSOR_ONOFF                      0x0001
135
136 /* Equalizer effect parameters */
137 define AL_EQUALIZER_LOW_GAIN                    0x0001
138 define AL_EQUALIZER_LOW_CUTOFF                  0x0002
139 define AL_EQUALIZER_MID1_GAIN                   0x0003
140 define AL_EQUALIZER_MID1_CENTER                 0x0004
141 define AL_EQUALIZER_MID1_WIDTH                  0x0005
142 define AL_EQUALIZER_MID2_GAIN                   0x0006
143 define AL_EQUALIZER_MID2_CENTER                 0x0007
144 define AL_EQUALIZER_MID2_WIDTH                  0x0008
145 define AL_EQUALIZER_HIGH_GAIN                   0x0009
146 define AL_EQUALIZER_HIGH_CUTOFF                 0x000A
#endif

		// Used with AudioSource
		DirectFilter = 0x20005,
		AuxiliarySendFilter = 0x20006,
		AirAbsorptionFactor = 0x20007,
		RoomRolloffFactor = 0x20008,
		ConeOuterGainHF = 0x20009,
		DirectFilterGainHF = 0x2000A,
		AuxiliarySendFilterGainAuto = 0x2000B,
		AuxiliarySendFilterGainHFAuto = 0x2000C,

		// AudioFilter.LowPass parameters
		LowPass_Gain = 1,
		LowPass_GainHF = 2,

		// AudioFilter.HighPass parameters
		HighPass_Gain = 1,
		HighPass_GainLF = 2,

		// AudioFilter.BandPass parameters
		BandPass_Gain = 1,
		BandPass_GainLF = 2,
		BandPass_GainHF = 3,

		// Filter type
		FilterFirstParameter = 0,
		FilterLastParameter = 0x8000,
	}
}