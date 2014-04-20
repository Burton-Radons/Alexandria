using Glare.Graphics.Internal;
using Glare.Graphics.Loaders;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Platform;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics {
	public class Context {
		static Context current;

		internal readonly DrawBufferCollection drawBuffers;

		IGraphicsContext context;
		IWindowInfo window;

		internal Vector4d blendConstant;

		internal FrameBuffer drawFrameBuffer, readFrameBuffer;

		internal Program program;

		public static Context Current { get { return current; } }

		// ActiveTexture
		// ArrayBufferBinding

		public static Context Shared { get { return Device.sharedContext; } }

		public Context(IGraphicsContext context, IWindowInfo window) {
			CheckError();
			GLExt.Setup((IGraphicsContextInternal)context);
			this.context = context;
			this.window = window;
			CheckError();
			MakeCurrent();

			CheckError();
			DrawBuffer[] drawBuffers = new DrawBuffer[Device.GetInt32(GetPName.MaxDrawBuffers)];
			for (int index = 0; index < drawBuffers.Length; index++)
				drawBuffers[index] = new DrawBuffer(this, index);
			this.drawBuffers = new DrawBufferCollection(drawBuffers);
		}

		~Context() {
		}

		static Context() {
			Device.Initialize();
		}

		public static void CheckError() {
			CheckError(GL.GetError());
		}

		public static void CheckError(ErrorCode code) {
			if (code == ErrorCode.NoError)
				return;
			string response = "An OpenGL exception " + code + " occurred.";
			switch (code) {
				case ErrorCode.InvalidOperation: throw new InvalidOperationException(response);
				case ErrorCode.OutOfMemory: throw new OutOfMemoryException(response);
				default: throw new Exception(response);
			}
		}

		internal static bool CheckVersion(int major, int minor = 0, int revision = 0) { return Device.Capabilities.Version.Check(major, minor, revision); }

		internal static void Initialize() {
		}

		internal bool IsEnabled(EnableCap cap) { using (Lock()) return GL.IsEnabled(cap); }

		static internal ContextLock Lock() { return new ContextLock(); }

		public bool MakeCurrent() {
			try {
				context.MakeCurrent(window);
				current = this;
				return true;
			} catch (GraphicsContextException) {
				return false;
			}
		}

		/*internal bool MakeCurrent() {
			//if (context.IsDisposed)
			//				return false;
			//if (object.ReferenceEquals(current, this))
			//return true;
			try { context.MakeCurrent(window); } catch (GraphicsContextException) { return false; }
			current = this;
			return true;
		}*/

		internal void RequireVersion(int major, int minor) { Device.Capabilities.Version.Require(major, minor); }

		internal void SetEnabled(EnableCap cap, bool value) {
			using (Lock())
				if (value)
					GL.Enable(cap);
				else
					GL.Disable(cap);
		}
	}

	internal struct ContextLock : IDisposable {
		public void Dispose() {
			Context.CheckError();
		}
	}
}
