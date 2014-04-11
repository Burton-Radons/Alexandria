using Glare.Graphics.Internal;
using Glare.Graphics.Loaders;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics {
	public static class Graphics {
		static readonly Capabilities capabilities;
		static readonly double[] doubleArray = new double[16];
		static readonly int[] intArray = new int[256];
		static bool isResourceManagerSetup;
		static Texture2D whiteTexture;

		static readonly GameWindow sharedWindow;
		internal static readonly Context sharedContext;
		static readonly Toolkit toolkit;

		public static AlphaTest AlphaTest {
			get { return new AlphaTest((ComparisonFunction)GetInt32(GetPName.AlphaTestFuncQcom), GetDouble(GetPName.AlphaTestRefQcom)); }

			set {
				OpenTK.Graphics.OpenGL.GL.AlphaFunc((OpenTK.Graphics.OpenGL.AlphaFunction)value.Function, checked((float)value.Reference));
				GL.Enable((EnableCap)OpenTK.Graphics.OpenGL.EnableCap.AlphaTest);
			}
		}

		/// <summary>Set the <see cref="BlendState"/> for all <see cref="DrawBuffers"/>.</summary>
		public static BlendState Blend {
			set {
				foreach (DrawBuffer buffer in Context.drawBuffers)
					buffer.Blend = value;
			}
		}

		/// <summary>Get or set a constant color value that can be used with the <see cref="BlendState"/> for a <see cref="DrawBuffer"/>. The default is (0, 0, 0, 0).</summary>
		public static Vector4d BlendConstant {
			get { return Context.blendConstant; }
			set { Context.blendConstant = value; using (Lock()) GL.BlendColor((float)value.X, (float)value.Y, (float)value.Z, (float)value.W); }
		}

		/// <summary>Get the <see cref="Capabilities"/> of this <see cref="Graphics"/> platform.</summary>
		public static Capabilities Capabilities { get { return capabilities; } }

		static Context Context { get { return Context.Current; } }

		public static ComparisonFunction DepthTest {
			get { return (ComparisonFunction)GetInt32(GetPName.DepthFunc); }
			set {
				using (Lock()) {
					GL.Enable(EnableCap.DepthTest);
					GL.DepthFunc((DepthFunction)value);
				}
			}
		}

		public static bool DepthWrite {
			get { return GetInt32(GetPName.DepthWritemask) != 0; }
			set { using (Lock()) GL.DepthMask(value); }
		}

		/// <summary>Get the collection of <see cref="DrawBuffer"/> objects that can be rendered to.</summary>
		public static DrawBufferCollection DrawBuffers { get { return Context.drawBuffers; } }

		/// <summary>Get or set the <see cref="FrameBuffer"/> to draw to.</summary>
		public static FrameBuffer DrawFrameBuffer {
			get { return Context.drawFrameBuffer; }

			set {
				using (Lock())
					GL.BindFramebuffer(FramebufferTarget.DrawFramebuffer, value != null ? value.Id : 0);
				Context.drawFrameBuffer = value;
			}
		}

		/// <summary>
		/// Set the <see cref="DrawFrameBuffer"/> and <see cref="ReadFrameBuffer"/> to the same <see cref="Glare.Graphics.FrameBuffer"/>.
		/// </summary>
		public static FrameBuffer FrameBuffer {
			set {
				using (Lock())
					GL.BindFramebuffer(FramebufferTarget.Framebuffer, value != null ? value.Id : 0);
				Context.drawFrameBuffer = Context.readFrameBuffer = value;
			}
		}

		internal static Program Program {
			get { return Context.program; }

			private set {
				using (Lock())
					GL.UseProgram(value != null ? value.Id : 0);
				Context.program = value;
				if (value != null)
					foreach (ProgramAttribute attribute in value.Attributes)
						attribute.DoBind();
			}
		}

		public static FrameBuffer ReadFrameBuffer {
			get { return Context.readFrameBuffer; }

			set {
				using(Lock())
					GL.BindFramebuffer(FramebufferTarget.ReadFramebuffer, value != null ? value.Id : 0);
				Context.readFrameBuffer = value;
			}
		}

		public static Texture2D WhiteTexture {
			get {
				if (whiteTexture == null) {
					whiteTexture = new Texture2D();
					whiteTexture.Data(1, 1, Formats.Vector4nb, new Vector4nb[] { Vector4nb.One });
				}
				return whiteTexture;
			}
		}

		static Graphics() {
			Context.Initialize();
			OpenTK.Graphics.GraphicsContext.ShareContexts = true;
			toolkit = OpenTK.Toolkit.Init();

			sharedWindow = new GameWindow(10, 10, GraphicsMode.Default, "shared context window", GameWindowFlags.Default, DisplayDevice.Default, 4, 0, GraphicsContextFlags.Default);
			sharedContext = new Context(sharedWindow.Context, sharedWindow.WindowInfo);
			sharedWindow.Disposed += (sender, args) => { throw new Exception(); };

			capabilities = new Capabilities();
		}

		internal static void CheckError() { Context.CheckError(); }

		public static void Clear(Vector4d? color = null, double? depth = null, int? stencil = null) {
			ClearBufferMask mask = (ClearBufferMask)0;

			if (color.HasValue) {
				Vector4d vcolor = color.Value;
				mask |= ClearBufferMask.ColorBufferBit;
				GL.ClearColor((float)vcolor.X, (float)vcolor.Y, (float)vcolor.Z, (float)vcolor.W);
			}

			if (depth.HasValue) {
				mask |= ClearBufferMask.DepthBufferBit;
				GL.ClearDepth(depth.Value);
			}

			if (stencil != null) {
				mask |= ClearBufferMask.StencilBufferBit;
				GL.ClearStencil(stencil.Value);
			}

			GL.Clear(mask);
			CheckError();
		}

		public static void DispatchCompute(int numGroupsX = 1, int numGroupsY = 1, int numGroupsZ = 1) { using (Lock()) GL.DispatchCompute(numGroupsX, numGroupsY, numGroupsZ); }

		public static void DispatchCompute(Vector2i numGroups) { DispatchCompute(numGroups.X, numGroups.Y, 1); }

		public static void DispatchCompute(Vector3i numGroups) { DispatchCompute(numGroups.X, numGroups.Y, numGroups.Z); }

		internal static void DrawStart(Program program) {
			Program = program;
			program.OnDraw();
			GL.PrimitiveRestartIndex(-1);
		}

		internal static double GetDouble(GetPName pname) { double result; using (Lock()) GL.GetDouble(pname, out result); return result; }
		internal static int GetInt32(GetPName pname) { int result; using (Lock()) GL.GetInteger(pname, out result); return result; }
		internal static int GetInt32(GetIndexedPName pname, int index) { int result; using (Lock()) GL.GetInteger(pname, index, out result); return result; }

		internal static Vector2d GetVector2d(GetPName pname) {
			lock (doubleArray) {
				using (Lock())
					GL.GetDouble(pname, doubleArray);
				return new Vector2d(doubleArray);
			}
		}

		internal static Vector3i GetVector3i(GetIndexedPName pname) {
			Vector3i result;
			using (Lock()) {
				GL.GetInteger(pname, 0, out result.X);
				GL.GetInteger(pname, 1, out result.Y);
				GL.GetInteger(pname, 2, out result.Z);
			}
			return result;
		}

		internal static void Initialize() { }

		internal static ContextLock Lock() { return new ContextLock(); }

		public static void SetupResourceManager() {
			if (isResourceManagerSetup)
				return;
			isResourceManagerSetup = true;
			ResourceLoader.AddLoader(new DDSLoader());
		}
	}

	public struct AlphaTest {
		public ComparisonFunction Function;
		public double Reference;

		public AlphaTest(ComparisonFunction function, double reference) {
			Function = function;
			Reference = reference;
		}
	}
}
