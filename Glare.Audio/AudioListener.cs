using OpenTK.Audio.OpenAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Audio
{
	public class AudioListener
	{
		readonly AudioContext context;

		public Vector3d Direction
		{
			get { Vector3d at, up; GetOrientation(out at, out up); return at; }
			set { SetOrientation(value, Up); }
		}

		public double Gain
		{
			get { return Get(ALListenerf.Gain); }
			set { Set(ALListenerf.Gain, value); }
		}

		public Vector3 Position
		{
			get { return Vector3.Metres(Get(ALListener3f.Position)); }
			set { Set(ALListener3f.Position, value.InMetres); }
		}

		/// <summary>Get or set the direction that is up for the listener. This is used for head orientation. The default is (0, 1, 0).</summary>
		public Vector3d Up
		{
			get { Vector3d at, up; GetOrientation(out at, out up); return up; }
			set { SetOrientation(Direction, value); }
		}

		public Velocity3 Velocity
		{
			get { return Velocity3.MetresPerSecond(Get(ALListener3f.Velocity)); }
			set { Set(ALListener3f.Velocity, value.InMetresPerSecond); }
		}

		internal AudioListener(AudioContext context)
		{
			this.context = context;
		}

		double Get(ALListenerf param) { float value; using (context.Bind()) AL.GetListener(param, out value); return value; }
		Vector3d Get(ALListener3f param) { float x, y, z; using (context.Bind()) AL.GetListener(param, out x, out y, out z); return new Vector3d(x, y, z); }
		void GetOrientation(out Vector3d at, out Vector3d up) { OpenTK.Vector3 iat, iup; using (context.Bind()) AL.GetListener(ALListenerfv.Orientation, out iat, out iup); at = new Vector3d(iat.X, iat.Y, iat.Z); up = new Vector3d(iup.X, iup.Y, iup.Z); }

		void Set(ALListenerf param, double value) { using (context.Bind()) AL.Listener(param, (float)value); }
		void Set(ALListener3f param, Vector3d value) { using (context.Bind()) AL.Listener(param, (float)value.X, (float)value.Y, (float)value.Z); }
		void SetOrientation(Vector3d at, Vector3d up) { OpenTK.Vector3 iat = new OpenTK.Vector3((float)at.X, (float)at.Y, (float)at.Z), iup = new OpenTK.Vector3((float)up.X, (float)up.Y, (float)up.Z); using (context.Bind()) AL.Listener(ALListenerfv.Orientation, ref iat, ref iup); }
	}
}
