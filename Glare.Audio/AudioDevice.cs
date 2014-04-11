using OpenTK.Audio.OpenAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Glare.Internal;

namespace Glare.Audio
{
	public class AudioDevice : DisposableObject
	{
		static readonly Dictionary<string, AudioDevice> map = new Dictionary<string, AudioDevice>();

		ReadOnlyCollection<string> extensions;
		string deviceName;
		IntPtr handle;

		public static AudioDevice[] AllDevices { get { return ToDeviceList(AlcGetStringList.AllDevicesSpecifier); } }
		
		/// <summary>Get a list of all current capture devices.</summary>
		public static AudioDevice[] CaptureDevices { get { return ToDeviceList(AlcGetStringList.CaptureDeviceSpecifier); } }
		
		/// <summary>Get the number of capture samples available.</summary>
		public int CaptureSampleCount { get { return GetInt(AlcGetInteger.CaptureSamples); } }

		/// <summary>Get the default device. This may change during execution,</summary>
		public static AudioDevice Default { get { return Get(OpenTK.Audio.AudioContext.DefaultDevice); } }

		public ReadOnlyCollection<string> Extensions { get { return extensions; } }

		/// <summary>Get the default capture device. This may change during execution.</summary>
		public static AudioDevice DefaultCapture { get { return Get(Alc.GetString(IntPtr.Zero, AlcGetString.CaptureDefaultDeviceSpecifier)); } }

		/// <summary>Get a collection of devices that combine audio and capture.</summary>
		public static AudioDevice[] GeneralDevices { get { return ToDeviceList(AlcGetStringList.DeviceSpecifier); } }

		//public Version Version { get { return new Version(StaticGetInt(AlcGetInteger.MajorVersion), StaticGetInt(AlcGetInteger.MinorVersion)); } }
		public Version Version { get { return new Version(GetInt(AlcGetInteger.MajorVersion), GetInt(AlcGetInteger.MinorVersion)); } }

		internal IntPtr Handle
		{
			get
			{
				if (handle == IntPtr.Zero)
				{
					handle = Alc.OpenDevice(deviceName);
					if (handle == IntPtr.Zero)
						throw new Exception();
					extensions = new ReadOnlyCollection<string>(Alc.GetString(Handle, AlcGetString.Extensions).Split(' '));
				}
				return handle;
			}
		}

		AudioDevice(string deviceName)
		{
			this.deviceName = DebugName = deviceName;
		}

		public AudioCapture Capture(AudioFormat format, Frequency frequency, int bufferSampleSize = 1024 * 1024)
		{
			IntPtr result = Alc.CaptureOpenDevice(deviceName, (int)Math.Round(frequency.InHertz), (ALFormat)format, bufferSampleSize);
			if (result == IntPtr.Zero)
				throw new InvalidOperationException("Cannot open the capture device.");
			return new AudioCapture(this, result, format);
		}

		internal void CheckError()
		{
			var code = Alc.GetError(Handle);
			if (code == AlcError.NoError)
				return;
			throw new Exception("OpenAL error with device '" + deviceName + "' (" + code + "): " + Alc.GetString(IntPtr.Zero, (AlcGetString)code));
		}

		public void Close()
		{
			if(handle != IntPtr.Zero)
				Alc.CloseDevice(handle);
			handle = IntPtr.Zero;
		}

		protected override void DisposeBase()
		{
			Close();
		}

		void GetProcAddress<T>(string functionName, out T result) where T : class
		{
			IntPtr pointer = Alc.GetProcAddress(Handle, functionName);
			CheckError();
			if (pointer == IntPtr.Zero)
				throw new Exception("Cannot find function '" + functionName + "'.");
			pointer.GetDelegateForFunctionPointer(out result);
		}

		static AudioDevice Get(string deviceName)
		{
			if(deviceName == null)
				return null;
			lock (map)
			{
				AudioDevice result;
				if(!map.TryGetValue(deviceName, out result))
					result = map[deviceName] = new AudioDevice(deviceName);
				return result;
			}
		}

		/// <summary>Get the value of an enumeration, or 0 if there is no such enumeration.</summary>
		/// <param name="enumName"></param>
		/// <returns></returns>
		public int GetEnumValue(string enumName) { return Alc.GetEnumValue(Handle, enumName); }

		internal int GetInt(AlcGetInteger param) { int result; Alc.GetInteger(Handle, param, 1, out result); return result; }
		internal int[] GetInt(AlcGetInteger param, int count) { return GetInt(param, new int[count]); }
		internal int[] GetInt(AlcGetInteger param, int[] list) { Alc.GetInteger(Handle, param, list.Length, list); return list; }

		internal static int StaticGetInt(AlcGetInteger param) { int result; Alc.GetInteger(IntPtr.Zero, param, 1, out result); return result; }
		internal static int[] StaticGetInt(AlcGetInteger param, int count) { return StaticGetInt(param, new int[count]); }
		internal static int[] StaticGetInt(AlcGetInteger param, int[] list) { Alc.GetInteger(IntPtr.Zero, param, list.Length, list); return list; }

		/// <summary>Get whether the extension with the given name is present.</summary>
		/// <param name="extensionName">The name of the extension to test.</param>
		/// <returns>Wehther the extension exists.</returns>
		public bool IsExtensionPresent(string extensionName) { return Alc.IsExtensionPresent(Handle, extensionName); }

		static AudioDevice[] ToDeviceList(AlcGetStringList source)
		{
			var names = Alc.GetString(IntPtr.Zero, source);
			AudioDevice[] list = new AudioDevice[names.Count];

			for (int index = 0; index < names.Count; index++)
				list[index] = Get(names[index]);
			return list;
		}
	}
}
