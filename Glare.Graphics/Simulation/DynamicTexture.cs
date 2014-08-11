using Glare.Graphics.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics.Simulation
{
	/// <summary>
	/// This is a texture manager for dynamics like deformable terrain and fluids.
	/// </summary>
	public abstract class DynamicTexture
	{
		readonly Context context;
		readonly Format format;
		readonly FrameBuffer frameBuffer;
		readonly Program mainProgram;
		readonly Vector4i dimensions;

		ProgramSubroutineUniform mainProgramAction;

		/// <summary>Get the size of the textures.</summary>
		public Vector4i Dimensions { get { return dimensions; } }

		/// <summary>Get the <see cref="Glare.Graphics.Format"/> to use for the textures.</summary>
		public Format Format { get { return format; } }

		/// <summary>Get the <see cref="Glare.Graphics.FrameBuffer"/> that is used for rendering.</summary>
		public FrameBuffer FrameBuffer { get { return frameBuffer; } }

		/// <summary>Get the <see cref="Glare.Graphics.Program"/> that's mainly used for rendering. Different <see cref="DynamicTexture"/> objects might not use this at all; it's only present for convenience.</summary>
		public Program MainProgram { get { return mainProgram; } }

		public DynamicTexture(Context context, Format format, Vector4i size)
		{
			if (context == null)
				throw new ArgumentNullException("context");
			if (format == null)
				throw new ArgumentNullException("format");
			this.format = format;
			this.frameBuffer = new FrameBuffer();
			this.mainProgram = new Program();
			this.dimensions = size;

			var builder = ShaderBuilder.CreateFromAssemblyResource("Glare.Graphics.Shaders.DynamicTexture.glsl");
			mainProgram.Shaders.AddRange(
				builder.VertexShader("Common", "Vertex"),
				builder.FragmentShader("Common", "Fragment"));
			mainProgram.MustLink();
			mainProgramAction = mainProgram.FragmentStage.Uniforms["Act"];
			mainProgram.Attributes["Position"].BindArray(new Vector2f[] {
				new Vector2f(-1, 1), new Vector2f(-1, -1),
				new Vector2f(1, 1), new Vector2f(1, -1) });
		}

		public DynamicTexture(Context context, Format format, Vector2i size) : this(context, format, size.X, size.Y) { }
		public DynamicTexture(Context context, Format format, Vector3i size) : this(context, format, size.X, size.Y, size.Z) { }
		public DynamicTexture(Context context, Format format, int sizeX, int sizeY, int sizeZ = 1, int sizeW = 1) : this(context, format, new Vector4i(sizeX, sizeY, sizeZ, sizeW)) { }
		
		public void Clear(double value, TextureLevel target) { Clear(new Vector4d(value), target); }

		public void Clear(Vector4d value, TextureLevel target)
		{
			mainProgram.Uniforms["ClearValue"].Set((Vector4f)value);
			Run(target, "Clear");
		}

		public void Clear(double value, TextureSurface target, int level = 0) { Clear(value, target.Levels[level]); }
		public void Clear(Vector4d value, TextureSurface target, int level = 0) { Clear(value, target.Levels[level]); }

		public void Clear(double value, FlatTexture target, int level = 0) { Clear(value, target.Levels[level]); }
		public void Clear(Vector4d value, FlatTexture target, int level = 0) { Clear(value, target.Levels[level]); }

		protected TextureCache2D CreateTextureCache2D(int initialCount = 0) { return new TextureCache2D(Format, Dimensions, initialCount); }

		protected Texture2D CreateTexture2D() { var texture = new Texture2D(); texture.Storage(Format, dimensions); return texture; }

		protected Texture3D CreateTexture3D() { var texture = new Texture3D(); texture.Storage(Format, dimensions); return texture; }

		protected TextureCube CreateTextureCube() { var texture = new TextureCube(); texture.Storage(Format, dimensions); return texture; }

		void Run(TextureLevel target, string action)
		{
			frameBuffer.Colors[0].Attach(target);
			mainProgramAction.Subroutine = mainProgramAction.Compatible[action];

			Device.FrameBuffer = frameBuffer;
			mainProgram.Draw(Primitive.Quads, 4);
			Device.FrameBuffer = null;
		}
	}
}
