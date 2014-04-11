using OpenTK.Audio.OpenAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Audio
{
	public class AudioSource : AudioResource
	{
		AudioFilter directFilter;
		readonly ObservableCollection<AudioBuffer> buffers = new ObservableCollection<AudioBuffer>();
		ReadOnlyObservableCollection<AudioBuffer> buffersReadOnly;
		AudioSourceEffectCollection effects;


		/*public enum ALSourceb
	{
		EfxDirectFilterGainHighFrequencyAuto = 131082,
		EfxAuxiliarySendFilterGainAuto = 131083,
		EfxAuxiliarySendFilterGainHighFrequencyAuto = 131084,
	}
	public enum ALGetSourcei
	{
		SourceState = 4112,
	}
	public enum ALSourcei
	{
		SourceType = 4135,
		EfxDirectFilter = 131077,
	}
		 * ALSourcef
		EfxAirAbsorptionFactor = 131079,
		EfxRoomRolloffFactor = 131080,
		EfxConeOuterGainHighFrequency = 131081,
		 * */

		/// <summary>
		/// A multiplier on the amount of air absorption applied to the source. This is multiplied by an internal air absorption gain high-frequency value of 0.994 (-0.05dB) per metre which represents normal atmorpheric humidity and temperature. The default is 0 which disables it, and values are clamped to the range 0 to 10. Changes to air absorption could represent conditions like an audio source being in a cloud of smoke, or an audio source that is suddenly visible in moving clouds.
		/// </summary>
		public double AirAbsorptionFactor
		{
			get { return Get(ALSourcef.EfxAirAbsorptionFactor); }
			set { Set(ALSourcef.EfxAirAbsorptionFactor, value); }
		}

		/// <summary>Get or set whether the intensity of the source's reflected sound is automatically attenuated according to source-listener distance and source directivity. The default is <c>true</c>.</summary>
		public bool AuxiliarySendFilterGainAuto
		{
			get { return Get(ALSourceb.EfxAuxiliarySendFilterGainAuto); }
			set { Set(ALSourceb.EfxAuxiliarySendFilterGainHighFrequencyAuto, value); }
		}

		/// <summary>Get or set whether the intensity of this source's reflected sound at high frequencies will be automatically attenuated according to the <see cref="ConeOuterGainHighFrequency"/>. The default is <c>true</c>. Making the source more directive at high frequencies will have the effect of reducing the amount of high frequencies in the reflected sound.</summary>
		public bool AuxiliarySendFilterGainHighFrequencyAuto
		{
			get { return Get(ALSourceb.EfxAuxiliarySendFilterGainHighFrequencyAuto); }
			set { Set(ALSourceb.EfxAuxiliarySendFilterGainHighFrequencyAuto, value); }
		}

		/// <summary>Get or set the <see cref="AudioBuffer"/> this source is playing.</summary>
		public AudioBuffer Buffer
		{
			get { return Context.GetBuffer(Get(ALGetSourcei.Buffer)); }
			set { Set(ALSourcei.Buffer, value != null ? value.Id : 0); }
		}

		/// <summary>Get the collection of <see cref="AudioBuffer"/>s that have been queued.</summary>
		public ReadOnlyObservableCollection<AudioBuffer> Buffers { get { return buffersReadOnly; } }

		/// <summary>Get the number of buffers that have been played by the source.</summary>
		public int BuffersProcessed { get { return Get(ALGetSourcei.BuffersProcessed); } }

		/// <summary>Get the number of buffers queued.</summary>
		public int BuffersQueued
		{
			get { return Get(ALGetSourcei.BuffersQueued); }
		}

		/// <summary>Get or set the playback position in bytes from the start of the buffer queue. For a compressed buffer, this may express an approximate offset within the compressed data.</summary>
		public int ByteOffset
		{
			get { return Get(ALGetSourcei.ByteOffset); }
			set { Set(ALSourcei.ByteOffset, value); }
		}

		/// <summary>Get or set the inner angle of the sound cone. The default is 360, and values are clamped to the range of 0 to 360 degrees.</summary>
		public Angle ConeInnerAngle
		{
			get { return Angle.Degrees(Get(ALSourcef.ConeInnerAngle)); }
			set { Set(ALSourcef.ConeInnerAngle, value.InDegrees); }
		}

		/// <summary>Get or set the outer angle of the sound cone. The default is 360, and values are clamped to the range of 0 to 360 degrees</summary>
		public Angle ConeOuterAngle
		{
			get { return Angle.Degrees(Get(ALSourcef.ConeOuterAngle)); }
			set { Set(ALSourcef.ConeOuterAngle, value.InDegrees); }
		}

		/// <summary>Get or set the gain multiplier when outside of the <see cref="ConeOuterAngle"/>.</summary>
		public double ConeOuterGain
		{
			get { return Get(ALSourcef.ConeOuterGain); }
			set { Set(ALSourcef.ConeOuterGain, value); }
		}

		/// <summary>
		/// This controls high frequency attenuation of sounds based on angle. Real-world sounds have greater high-frequency attenuation than low frequencies. The default is 0 and values are clamped to 0 to 10. At 0, directivity attenuation for high frequencies is 100 dB more than it is for low frequencies.
		/// </summary>
		public double ConeOuterGainHighFrequency
		{
			get { return Get(ALSourcef.EfxConeOuterGainHighFrequency); }
			set { Set(ALSourcef.EfxConeOuterGainHighFrequency, value); }
		}

		/// <summary>Get or set the filter to apply on the direct-path (dry signal) of the source.</summary>
		public AudioFilter DirectFilter
		{
			get { return directFilter; }
			set
			{
				Set(ALSourcei.EfxDirectFilter, value != null ? value.Id : 0);
				directFilter = value;
			}
		}

		/// <summary>Get or set the direction of the source. If this value is zero, the source is non-directional. The default is 0.</summary>
		public Vector3d Direction
		{
			get { return Get(ALSource3f.Direction); }
			set { Set(ALSource3f.Direction, value); }
		}

		/// <summary>Get the collection of effects that are applied to this audio source.</summary>
		public AudioSourceEffectCollection Effects { get { return effects ?? (effects = new AudioSourceEffectCollection(this)); } }

		/// <summary>Get or set whether the source's direct path is automatically filtered according to the orientation of the source relative to the listener and the <see cref="ConeOuterGainHighFrequency"/>. The default is <c>true</c>. If set to <c>true</c>, the source is not more directive at high frequncies and this property has no effect. Otherwise, the direct path will be brighter in front of the source than on the side or in the rear.</summary>
		public bool FilterGainHighFrequencyAuto
		{
			get { return Get(ALSourceb.EfxDirectFilterGainHighFrequencyAuto); }
			set { Set(ALSourceb.EfxDirectFilterGainHighFrequencyAuto, value); }
		}

		/// <summary>Get or set the gain (volume) of the audio source, from 0 to 1. The default is 1.</summary>
		public double Gain
		{
			get { return Get(ALSourcef.Gain); }
			set { Set(ALSourcef.Gain, value); }
		}

		public bool IsInitialState { get { return State == AudioSourceState.Initial; } }

		/// <summary>Get or set whether the source is to loop.</summary>
		public bool IsLooping
		{
			get { return Get(ALSourceb.Looping); }
			set { Set(ALSourceb.Looping, value); }
		}

		public bool IsPaused { get { return State == AudioSourceState.Paused; } }

		public bool IsPlaying { get { return State == AudioSourceState.Playing; } }

		/// <summary>
		/// Get or set whether the <see cref="Direction"/>, <see cref="Position"/>, and <see cref="Velocity"/> parameters are relative to the <see cref="AudioListener"/>.
		/// </summary>
		public bool IsRelative
		{
			get { return Get(ALSourceb.SourceRelative); }
			set { Set(ALSourceb.SourceRelative, value); }
		}

		/// <summary>Get whether the <see cref="State"/> is <see cref="AudioSourceState.Stopped"/> or <see cref="AudioSourceState.Initial"/>.</summary>
		public bool IsReset { get { var s = State; return s == AudioSourceState.Stopped || s == AudioSourceState.Initial; } }

		public bool IsStopped { get { return State == AudioSourceState.Stopped; } }

		/// <summary>Get or set the maximum distance that attenuation occurs to. Distances greater than this value are clamped to it. The default is infinity; values are clamped to at least 0.</summary>
		public Length MaxDistance
		{
			get { return Length.Metres(Get(ALSourcef.MaxDistance)); }
			set { Set(ALSourcef.MaxDistance, value.InMetres); }
		}

		/// <summary>Get or set the maximum gain that this source can have. This happens before the listener gain is applied. The default is 1; values are clamped to the range of 0 to 1.</summary>
		public double MaxGain
		{
			get { return Get(ALSourcef.MaxGain); }
			set { Set(ALSourcef.MaxGain, value); }
		}

		/// <summary>Get or set the minimum gain that is always guaranteed for this source. This happens before the listener gain is applied. The default is zero; values are clamped to the range of 0 to 1.</summary>
		public double MinGain
		{
			get { return Get(ALSourcef.MinGain); }
			set { Set(ALSourcef.MinGain, value); }
		}

		/// <summary>Get or set the playback position, relative to the beginning of all the queued buffers for the source. This is based on byte position, so a pitch-shifted source will have an exaggerated playback speed.</summary>
		public TimeSpan Offset
		{
			get { return TimeSpan.FromSeconds(Get(ALSourcef.SecOffset)); }
			set { Set(ALSourcef.SecOffset, value.TotalSeconds); }
		}

		/// <summary>Get or set a desired pitch shift. A double value increases the pitch by 12 semitones (one octave), while a half value decreases the pitch by 12 semitones (one octave). The default is 1.0, and values are clamped to greater than 0.</summary>
		public double Pitch
		{
			get { return Get(ALSourcef.Pitch); }
			set { Set(ALSourcef.Pitch, value); }
		}

		public Vector3 Position
		{
			get { return Vector3.Metres(Get(ALSource3f.Position)); }
			set { Set(ALSource3f.Position, value.InMetres); }
		}

		/// <summary>Get or set the reference distance to use for distance attenuation. The default is one metre; values are clamped to at least zero.</summary>
		public Length ReferenceDistance
		{
			get { return Length.Metres(Get(ALSourcef.ReferenceDistance)); }
			set { Set(ALSourcef.ReferenceDistance, value.InMetres); }
		}

		/// <summary>This scales attenuation based on inverse distance with rolloff for distances smaller than <see cref="MaxDistance"/>. Setting this to 0 disables distance attenuation. The default is 1.0, and values are clamped to at least 0.</summary>
		public double RolloffFactor
		{
			get { return Get(ALSourcef.RolloffFactor); }
			set { Set(ALSourcef.RolloffFactor, value); }
		}

		/// <summary>This attenuates reflected sound (early reflections and reverberation) according to source-listener distance. The default is 0, and values are clamped to 0 to 10.</summary>
		public double RoomRolloffFactor
		{
			get { return Get(ALSourcef.EfxRoomRolloffFactor); }
			set { Set(ALSourcef.EfxRoomRolloffFactor, value); }
		}

		/// <summary>Get or set the playback position in samples from the start of the buffer queue. For a compressed format this is an exact offset within the uncompressed data.</summary>
		public int SampleOffset
		{
			get { return Get(ALGetSourcei.SampleOffset); }
			set { Set(ALSourcei.SampleOffset, value); }
		}

		public AudioSourceState State { get { return (AudioSourceState)Get(ALGetSourcei.SourceState); } }

		public AudioSourceType Type { get { return (AudioSourceType)Get(ALGetSourcei.SourceType); } }

		public Velocity3 Velocity
		{
			get { return Velocity3.MetresPerSecond(Get(ALSource3f.Velocity)); }
			set { Set(ALSource3f.Velocity, value.InMetresPerSecond); }
		}

		public AudioSource(AudioContext context)
			: base(context, Create(context))
		{
			buffersReadOnly = new ReadOnlyObservableCollection<AudioBuffer>(buffers);
		}

		static int Create(AudioContext context)
		{
			using (context.Bind())
				return AL.GenSource();
		}

		protected override void DisposeBase()
		{
			if (Context.MakeCurrent())
				AL.DeleteSource(Id);
			base.DisposeBase();
			//ALSourcef.ReferenceDistance
		}

		internal bool Get(ALSourceb param) { bool value; using (Context.Bind()) AL.GetSource(Id, param, out value); return value; }
		internal int Get(ALGetSourcei param) { int value; using (Context.Bind()) AL.GetSource(Id, param, out value); return value; }
		internal double Get(ALSourcef param) { float value; using (Context.Bind()) AL.GetSource(Id, param, out value); return value; }
		internal Vector3d Get(ALSource3f param) { float x, y, z; using (Context.Bind()) AL.GetSource(Id, param, out x, out y, out z); return new Vector3d(x, y, z); }

		public void Pause() { using (Context.Bind()) AL.SourcePause(Id); }

		public void Play() { using (Context.Bind()) AL.SourcePlay(Id); }

		public void Queue(AudioBuffer buffer)
		{
			if (buffer == null)
				throw new ArgumentNullException("buffer");
			using (Context.Bind())
			{
				AL.SourceQueueBuffer(Id, buffer.Id);
				Context.CheckError();
				buffers.Add(buffer);
			}
		}

		public void Queue(IEnumerable<AudioBuffer> buffers)
		{
			if (buffers == null)
				throw new ArgumentNullException("buffers");
			using (Context.Bind())
			{
				foreach (AudioBuffer buffer in buffers)
				{
					if (buffer == null)
						throw new ArgumentNullException("buffers[]");
					AL.SourceQueueBuffer(Id, buffer.Id);
					Context.CheckError();
					this.buffers.Add(buffer);
				}
			}
		}

		public void Queue(params AudioBuffer[] buffers)
		{
			Queue((IEnumerable<AudioBuffer>)buffers);
		}

		public void Rewind() { using (Context.Bind()) AL.SourceRewind(Id); }

		internal void Set(ALSourceb param, bool value) { using (Context.Bind()) AL.Source(Id, param, value); }
		internal void Set(ALSourcef param, double value) { using (Context.Bind()) AL.Source(Id, param, (float)value); }
		internal void Set(ALSourcei param, int value) { using (Context.Bind()) AL.Source(Id, param, value); }
		internal void Set(ALSourcei param, AudioResource value) { Set(param, value != null ? value.Id : 0); }
		internal void Set(ALSource3f param, Vector3d value) { using (Context.Bind()) AL.Source(Id, param, (float)value.X, (float)value.Y, (float)value.Z); }
		internal void Set(ALSource3i param, Vector3i value) { using (Context.Bind()) AL.Source(Id, param, value.X, value.Y, value.Z); }

		public void Stop() { using (Context.Bind()) AL.SourceStop(Id); }

		public AudioBuffer Unqueue()
		{
			using (Context.Bind())
			{
				var id = AL.SourceUnqueueBuffer(Id);
				Context.CheckError();
				var buffer = buffers[0];
				buffers.RemoveAt(0);
				if (id != buffer.Id)
					throw new InvalidProgramException();
				return buffer;
			}
		}

		public AudioBuffer[] Unqueue(int count)
		{
			AudioBuffer[] result = new AudioBuffer[count];
			int[] ids = new int[count];

			using (Context.Bind())
				AL.SourceUnqueueBuffers(Id, count, ids);
			for (int index = 0; index < count; index++)
			{
				var buffer = buffers[0];
				buffers.RemoveAt(0);
				result[index] = Context.GetBuffer(ids[index]);
				if (buffer.Id != ids[index])
					throw new InvalidProgramException();
			}
			return result;
		}
	}

	public struct AudioSourceEffectSlot
	{
		public readonly AudioEffect Effect;
		public readonly AudioFilter Filter;

		public static readonly AudioSourceEffectSlot Empty = default(AudioSourceEffectSlot);

		public AudioSourceEffectSlot(AudioEffect effect) : this(effect, null) { }

		public AudioSourceEffectSlot(AudioEffect effect, AudioFilter filter)
		{
			if (effect == null)
				throw new ArgumentNullException("effect");
			Effect = effect;
			Filter = filter;
		}

		public static implicit operator AudioSourceEffectSlot(AudioEffect effect) { return new AudioSourceEffectSlot(effect); }
	}

	public class AudioSourceEffectCollection : IList<AudioSourceEffectSlot>
	{
		readonly AudioSource source;
		readonly List<AudioSourceEffectSlot> list = new List<AudioSourceEffectSlot>();

		public int Count { get { return list.Count; } }

		public bool IsReadOnly { get { return false; } }

		public AudioSourceEffectSlot this[int index]
		{
			get { return list[index]; }

			set
			{
				var was = list[index];
				Set(index);
				list[index] = value;
				RefAdd(value);
				RefRemove(was);
			}
		}

		internal AudioSourceEffectCollection(AudioSource source)
		{
			this.source = source;
		}

		public void Add(AudioSourceEffectSlot item) { Insert(Count, item); }

		public void Clear() { while (Count > 0) RemoveAt(Count - 1); }

		public bool Contains(AudioSourceEffectSlot item) { return list.Contains(item); }

		public void CopyTo(AudioSourceEffectSlot[] array, int arrayIndex) { list.CopyTo(array, arrayIndex); }

		public List<AudioSourceEffectSlot>.Enumerator GetEnumerator() { return list.GetEnumerator(); }
		IEnumerator<AudioSourceEffectSlot> IEnumerable<AudioSourceEffectSlot>.GetEnumerator() { return GetEnumerator(); }
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return GetEnumerator(); }

		public int IndexOf(AudioSourceEffectSlot item) { return list.IndexOf(item); }

		public void Insert(int index, AudioSourceEffectSlot item)
		{
			list.Insert(index, item);
			try
			{
				for (int update = index; update < Count; update++)
					Set(update);
			}
			catch (Exception exception)
			{
				list.RemoveAt(index);
				throw new Exception("Insert failed", exception);
			}

			RefAdd(item);
		}

		public bool Remove(AudioSourceEffectSlot item) { int index = IndexOf(item); if (index < 0) return false; RemoveAt(index); return true; }

		public void RemoveAt(int index)
		{
			var was = list[index];
			list.RemoveAt(index);
			try
			{
				for (int update = index; update < Count; update++)
					Set(update);
			}
			catch (Exception exception)
			{
				list.Insert(index, was);
				throw new Exception("Removal failed", exception);
			}

			SetNull(Count);
			RefRemove(was);
		}

		void Set(int index) { Set(index, list[index]); }
		void SetNull(int index) { Set(index, AudioSourceEffectSlot.Empty); }

		void Set(int index, AudioSourceEffectSlot item)
		{
			using (source.Context.Bind())
				AL.Source(source.Id, ALSource3i.EfxAuxiliarySendFilter, item.Effect != null ? item.Effect.Id : 0, index, item.Filter != null ? item.Filter.Id : 0);
		}

		void RefAdd(AudioSourceEffectSlot item)
		{
			if (item.Effect != null)
				item.Effect.sources.Add(source);
		}

		void RefRemove(AudioSourceEffectSlot item)
		{
			if (item.Effect != null)
				item.Effect.sources.Remove(source);
		}
	}
}
