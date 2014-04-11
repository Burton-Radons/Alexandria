using OpenTK;
using OpenTK.Audio.OpenAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glare.Internal;
using System.Runtime.InteropServices;

namespace Glare.Audio
{
	public class AudioContext : DisposableObject
	{
		AudioDevice device;
		ContextHandle handle;
		readonly ReadOnlyCollection<string> extensions;
		List<AudioContextAttribute> attributes;

		internal readonly Dictionary<int, WeakReference<AudioBuffer>> buffers = new Dictionary<int, WeakReference<AudioBuffer>>();
		internal readonly Dictionary<int, AudioStreamer> streamers = new Dictionary<int, AudioStreamer>();

		static AudioContext current;

		public IList<AudioContextAttribute> Attributes
		{
			get
			{
				int[] list = Get(AlcGetInteger.AllAttributes, Get(AlcGetInteger.AttributesSize));

				if (attributes == null)
				{
					attributes = new List<AudioContextAttribute>(list.Length / 2);

					for (int index = 0; index < list.Length - 1; index += 2)
					{
						int value = list[index + 1];
						switch ((AlcContextAttributes)list[index])
						{
							case AlcContextAttributes.Frequency: attributes.Add(new AudioContextAttribute.Frequency(Frequency.Hertz(value))); break;
							case AlcContextAttributes.Refresh: attributes.Add(new AudioContextAttribute.RefreshInterval(Frequency.Hertz(value))); break;
							case AlcContextAttributes.Sync: attributes.Add(new AudioContextAttribute.Synchronous()); break;
							case AlcContextAttributes.MonoSources: attributes.Add(new AudioContextAttribute.MonoSources(value)); break;
							case AlcContextAttributes.StereoSources: attributes.Add(new AudioContextAttribute.StereoSources(value)); break;
							case AlcContextAttributes.EfxMaxAuxiliarySends: attributes.Add(new AudioContextAttribute.MaxSourceEffects(value)); break;
							default: attributes.Add(new AudioContextAttribute.Unknown((AlcContextAttributes)list[index], value)); break;
						}
					}
				}
				return attributes;
			}
		}

		/// <summary>Get the audio device that this uses.</summary>
		public AudioDevice Device { get { return device; } }

		/// <summary>Get or set the current distance model.</summary>
		public AudioDistanceModel DistanceModel
		{
			get { return (AudioDistanceModel)Get(ALGetInteger.DistanceModel); }
			set { using (Bind()) AL.DistanceModel((ALDistanceModel)value); }
		}

		/// <summary>Get or set the exaggeration factor for the doppler effect.</summary>
		public double DopplerFactor
		{
			get { return Get(ALGetFloat.DopplerFactor); }
			set { using (Bind()) AL.DopplerFactor((float)value); }
		}

		/// <summary>Get the major version number of the effects extension.</summary>
		public int EffectsMajorVersion { get { return Get((ALGetInteger)AlEfxEnums.EfxMajorVersion); } }

		/// <summary>Get the minor version number of the effects extension.</summary>
		public int EffectsMinorVersion { get { return Get((ALGetInteger)AlEfxEnums.EfxMajorVersion); } }

		/// <summary>Get the collection of extensions that this <see cref="AudioContext"/> supports.</summary>
		public ReadOnlyCollection<string> Extensions { get { return extensions; } }

		internal ContextHandle Handle { get { return handle; } }

		/// <summary>Get or set the speed of sound.</summary>
		public Velocity SpeedOfSound
		{
			get { return Velocity.MetresPerSecond(Get(ALGetFloat.SpeedOfSound)); }
			set { using (Bind()) AL.SpeedOfSound((float)value.InMetresPerSecond); }
		}

		/// <summary>Get or set whether or not to perform processing on this <see cref="AudioContext"/>. This is <c>true</c> by default.</summary>
		public bool IsProcessing
		{
			set
			{
				if (value)
					Alc.ProcessContext(Handle);
				else
					Alc.SuspendContext(Handle);
				CheckError();
			}
		}

		/// <summary>Get the maximum number of auxiliary sends that can be attached to a source.</summary>
		public int MaxSourceEffects { get { return Get((ALGetInteger)AlEfxEnums.MaxAuxiliarySends); } }

		/// <summary>Get information about the specific renderer.</summary>
		public string Renderer { get { return Get(ALGetString.Renderer); } }

		/// <summary>Get information about the vendor.</summary>
		public string Vendor { get { return Get(ALGetString.Vendor); } }

		/// <summary>Get the version string in the form "[spec major number].[spec minor number] [optional vendor version information]</summary>
		public string VersionString { get { return Get(ALGetString.Version); } }

		public AudioContext(params AudioContextAttribute[] options) : this(AudioDevice.Default, (ICollection<AudioContextAttribute>)options) { }

		public AudioContext(AudioDevice device, params AudioContextAttribute[] options) : this(device, (ICollection<AudioContextAttribute>)options) { }

		public AudioContext(AudioDevice device, ICollection<AudioContextAttribute> options)
		{
			int[] realOptions = null;

			if (device == null)
				throw new ArgumentNullException("device");
			this.device = device;

			if (options != null)
			{
				int index = 0;
				realOptions = new int[options.Count * 2 + 1];
				foreach (AudioContextAttribute option in options)
				{
					if (option == null)
						throw new ArgumentNullException("option[]");
					realOptions[index + 0] = (int)option.Attribute;
					realOptions[index + 1] = option.BaseValue;
					index += 2;
				}
				realOptions[index] = 0;
			}

			handle = Alc.CreateContext(device.Handle, realOptions);
			CheckError();
			if (handle == null)
				throw new Exception();

			MakeCurrent();
			extensions = new ReadOnlyCollection<string>(Get(ALGetString.Extensions).Split(' '));
			SetupEffects();

			AL.Listener(ALListenerf.EfxMetersPerUnit, (float)Length.Universal(1).InMetres);
		}

		internal AudioContextBinding Bind() { return new AudioContextBinding(this); }

		internal void CheckError() { device.CheckError(); }

		protected override void DisposeBase()
		{
			if (object.ReferenceEquals(current, this))
				Alc.MakeContextCurrent(ContextHandle.Zero);
			current = null;
			Alc.DestroyContext(Handle);
			handle = ContextHandle.Zero;
		}

		internal AudioBuffer GetBuffer(int id)
		{
			if (id == 0)
				return null;
			WeakReference<AudioBuffer> bufferRef;
			if (!buffers.TryGetValue(id, out bufferRef))
				return null;
			AudioBuffer buffer;
			bufferRef.TryGetTarget(out buffer);
			return buffer;
		}

		internal int Get(AlcGetInteger param) { using (Bind()) return device.GetInt(param); }
		internal int[] Get(AlcGetInteger param, int count) { return Get(param, new int[count]); }
		internal int[] Get(AlcGetInteger param, int[] list) { using (Bind()) return device.GetInt(param, list); }
		internal int Get(ALGetInteger param) { using (Bind()) return AL.Get(param); }
		internal float Get(ALGetFloat param) { using (Bind()) return AL.Get(param); }
		internal string Get(ALGetString param) { using (Bind()) return AL.Get(param); }

		internal void GetProcAddress<T>(string functionName, out T result) where T : class
		{
			using (Bind())
			{
				IntPtr pointer = AL.GetProcAddress(functionName);
				CheckError();
				if (pointer == IntPtr.Zero)
					throw new Exception("Cannot find function '" + functionName + "'.");
				pointer.GetDelegateForFunctionPointer(out result);
			}
		}



		internal bool MakeCurrent()
		{
			if (Handle == null)
				return false;
			if (object.ReferenceEquals(current, this))
				return true;
			bool result = Alc.MakeContextCurrent(Handle);
			if (result) current = this;
			return result;
		}

		public void Pause(params AudioSource[] sources) { Pause((ICollection<AudioSource>)sources); }
		public void Pause(ICollection<AudioSource> sources) { using (Bind()) AL.SourcePause(sources.Count, ToIds(sources)); }

		public void Play(params AudioSource[] sources) { Play((ICollection<AudioSource>)sources); }
		public void Play(ICollection<AudioSource> sources) { using (Bind()) AL.SourcePlay(sources.Count, ToIds(sources)); }

		public void Rewind(params AudioSource[] sources) { Rewind((ICollection<AudioSource>)sources); }
		public void Rewind(ICollection<AudioSource> sources) { using (Bind()) AL.SourceRewind(sources.Count, ToIds(sources)); }

		public void Stop(params AudioSource[] sources) { Stop((ICollection<AudioSource>)sources); }
		public void Stop(ICollection<AudioSource> sources) { using (Bind()) AL.SourceStop(sources.Count, ToIds(sources)); }

		internal int[] ToIds(ICollection<AudioSource> sources)
		{
			int[] ids = new int[sources.Count];
			int index = 0;

			if (sources == null)
				throw new ArgumentNullException("sources");
			foreach (AudioSource source in sources)
			{
				if (source == null)
					throw new ArgumentNullException("sources[]");
				ids[index++] = source.Id;
			}

			return ids;
		}

		public void Update()
		{
			foreach (AudioStreamer streamer in streamers.Values)
				streamer.Update();
		}

		#region Effects

		bool effectsSetup;

		const CallingConvention ExternalCallingConvention = CallingConvention.Cdecl;

		[UnmanagedFunctionPointer(ExternalCallingConvention)]
		unsafe internal delegate void AlGenDestroyDelegate(int count, int* effects);

		[UnmanagedFunctionPointer(ExternalCallingConvention)]
		internal delegate void AlSetiDelegate(int effect, AlEfxEnums param, int value);

		[UnmanagedFunctionPointer(ExternalCallingConvention)]
		unsafe internal delegate void AlGetSetivDelegate(int effect, AlEfxEnums param, int* value);

		[UnmanagedFunctionPointer(ExternalCallingConvention)]
		unsafe internal delegate void AlSetfDelegate(int effect, AlEfxEnums param, float value);

		[UnmanagedFunctionPointer(ExternalCallingConvention)]
		unsafe internal delegate void AlGetSetfvDelegate(int effect, AlEfxEnums param, float* value);

		internal AlGenDestroyDelegate AlGenEffects;
		internal AlGenDestroyDelegate AlDeleteEffects;
		internal AlSetiDelegate AlEffecti;
		internal AlGetSetivDelegate AlEffectiv;
		internal AlSetfDelegate AlEffectf;
		internal AlGetSetfvDelegate AlEffectfv;
		internal AlGetSetivDelegate AlGetEffecti;
		internal AlGetSetivDelegate AlGetEffectiv;
		internal AlGetSetfvDelegate AlGetEffectf;
		internal AlGetSetfvDelegate AlGetEffectfv;

		internal AlGenDestroyDelegate AlGenFilters;
		internal AlGenDestroyDelegate AlDeleteFilters;
		internal AlSetiDelegate AlFilteri;
		internal AlGetSetivDelegate AlFilteriv;
		internal AlSetfDelegate AlFilterf;
		internal AlGetSetfvDelegate AlFilterfv;
		internal AlGetSetivDelegate AlGetFilteri;
		internal AlGetSetivDelegate AlGetFilteriv;
		internal AlGetSetfvDelegate AlGetFilterf;
		internal AlGetSetfvDelegate AlGetFilterfv;

		internal void SetupEffects()
		{
			if (effectsSetup)
				return;
			effectsSetup = true;

			if (!Device.IsExtensionPresent("ALC_EXT_EFX"))
				throw new InvalidOperationException("This device somehow doesn't support ALC_EXT_EFX, a better or newer implementation of OpenAL is mandatory!");

			GetProcAddress("alGenEffects", out AlGenEffects);
			GetProcAddress("alDeleteEffects", out AlDeleteEffects);
			GetProcAddress("alEffecti", out AlEffecti);
			GetProcAddress("alEffectiv", out AlEffectiv);
			GetProcAddress("alEffectf", out AlEffectf);
			GetProcAddress("alEffectfv", out AlEffectfv);
			GetProcAddress("alGetEffecti", out AlGetEffecti);
			GetProcAddress("alGetEffectiv", out AlGetEffectiv);
			GetProcAddress("alGetEffectf", out AlGetEffectf);
			GetProcAddress("alGetEffectfv", out AlGetEffectfv);

			GetProcAddress("alGenFilters", out AlGenFilters);
			GetProcAddress("alDeleteFilters", out AlDeleteFilters);
			GetProcAddress("alFilteri", out AlFilteri);
			GetProcAddress("alFilteriv", out AlFilteriv);
			GetProcAddress("alFilterf", out AlFilterf);
			GetProcAddress("alFilterfv", out AlFilterfv);
			GetProcAddress("alGetFilteri", out AlGetFilteri);
			GetProcAddress("alGetFilteriv", out AlGetFilteriv);
			GetProcAddress("alGetFilterf", out AlGetFilterf);
			GetProcAddress("alGetFilterfv", out AlGetFilterfv);
		}

		#endregion Effects
	}

	public abstract class AudioContextAttribute
	{
		readonly AlcContextAttributes attribute;
		readonly int value;

		internal AlcContextAttributes Attribute { get { return attribute; } }
		internal int BaseValue { get { return value; } }

		internal AudioContextAttribute(AlcContextAttributes attribute, int value) { this.attribute = attribute; this.value = value; }

		/// <summary>Frequency for mixing the output buffer.</summary>
		public class Frequency : AudioContextAttribute
		{
			public Glare.Frequency Value { get { return Glare.Frequency.Hertz(BaseValue); } }
			public Frequency(Glare.Frequency frequency) : base(AlcContextAttributes.Frequency, (int)Math.Round(frequency.InHertz)) { }
		}

		public class MaxSourceEffects : AudioContextAttribute
		{
			public int Count { get { return BaseValue; } }
			public MaxSourceEffects(int count) : base(AlcContextAttributes.EfxMaxAuxiliarySends, count) { }
		}

		/// <summary>A hint for how many sources should support mono data.</summary>
		public class MonoSources : AudioContextAttribute
		{
			public int Count { get { return BaseValue; } }
			public MonoSources(int count) : base(AlcContextAttributes.MonoSources, count) { }
		}

		/// <summary>Refresh interval in a frequency measurement.</summary>
		public class RefreshInterval : AudioContextAttribute
		{
			public Glare.Frequency Value { get { return Glare.Frequency.Hertz(BaseValue); } }
			public RefreshInterval(Glare.Frequency frequency) : base(AlcContextAttributes.Frequency, (int)Math.Round(frequency.InHertz)) { }
		}

		/// <summary>A hint for how many sources should support stereo data.</summary>
		public class StereoSources : AudioContextAttribute
		{
			public int Count { get { return BaseValue; } }
			public StereoSources(int count) : base(AlcContextAttributes.StereoSources, count) { }
		}

		/// <summary>A flag to indicate a synchronous context.</summary>
		public class Synchronous : AudioContextAttribute
		{
			public int Count { get { return BaseValue; } }
			public Synchronous() : base(AlcContextAttributes.Sync, 0) { }
		}

		public class Unknown : AudioContextAttribute
		{
			public new int Attribute { get { return (int)base.Attribute; } }
			public int Value { get { return BaseValue; } }
			internal Unknown(AlcContextAttributes attribute, int value) : base(attribute, value) { }
		}
	}

	internal struct AudioContextBinding : IDisposable
	{
		AudioContext context;

		public AudioContextBinding(AudioContext context)
		{
			this.context = context;
			if (!context.MakeCurrent())
				throw new Exception("Could not make the audio context current.");
		}

		public void Dispose()
		{
			context.CheckError();
			//Alc.MakeContextCurrent(ContextHandle.Zero);
		}
	}
}
