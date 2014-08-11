using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics {
	public class TextureRectangle : FlatTexture {
		TextureSurfaceRectangle surface;

		protected override TextureSurface BaseSurface { get { return Surface; } }

		/// <summary>Get the <see cref="TextureLevelRectangle"/> collection of the <see cref="TextureRectangle"/>'s one <see cref="Surface"/>.</summary>
		public new TextureSurfaceLevelCollectionRectangle Levels { get { return Surface.Levels; } }

		/// <summary>Get the sole <see cref="TextureSurface"/> that makes up this <see cref="TextureRectangle"/>.</summary>
		public new TextureSurfaceRectangle Surface { get { return surface ?? (surface = new TextureSurfaceRectangle(this)); } }

		protected internal override TextureTarget Target { get { return TextureTarget.TextureRectangle; } }

		protected internal override GetPName TargetBinding { get { return GetPName.TextureBindingRectangle; } }

		public TextureRectangle()
			: base() {
		}

		public TextureRectangle(Format format, int width, int height) : this(format, new Vector2i(width, height)) { }
		public TextureRectangle(Format format, Vector2i dimensions) : base(format, new Vector4i(dimensions, 1, 1)) { }

		/// <summary>Prepare the texture with uninitialised data.</summary>
		/// <param name="levels">The number of levels to generate.</param>
		/// <param name="format">The texture format. This must be an item from <see cref="TextureFormats"/>.</param>
		/// <param name="dimensions">The width and height of the texture. Z and W must be 1.</param>
		public override void Storage(int levels, Format format, Vector4i dimensions) {
			Storage2D(levels, format, dimensions);
		}

		/// <summary>Prepare the texture with unitialised data.</summary>
		/// <param name="levels">The number of levels to generate.</param>
		/// <param name="format">The texture format. This must be an item from <see cref="TextureFormats"/>.</param>
		/// <param name="width">The width in pixels of the texture.</param>
		/// <param name="height">The height in pixels of the texture.</param>
		public void Storage(int levels, Format format, int width, int height) { Storage(levels, format, new Vector4i(width, height, 1, 1)); }

		/// <summary>Prepare the texture with unitialised data. This automatically generates the appropriate number of levels.</summary>
		/// <param name="format">The texture format. This must be an item from <see cref="TextureFormats"/>.</param>
		/// <param name="width">The width in pixels of the texture.</param>
		/// <param name="height">The height in pixels of the texture.</param>
		public void Storage(Format format, int width, int height) { Storage(format, new Vector4i(width, height, 1, 1)); }
	}

	public class TextureSurfaceRectangle : TextureSurface<TextureRectangle, TextureSurfaceRectangle, TextureLevelRectangle, TextureSurfaceLevelCollectionRectangle> {
		internal TextureSurfaceRectangle(TextureRectangle texture) : base(texture) { }

		protected internal override TextureLevelRectangle CreateLevel(int index) { return new TextureLevelRectangle(this, index); }
	}

	public class TextureLevelRectangle : TextureLevel<TextureRectangle, TextureSurfaceRectangle, TextureLevelRectangle, TextureSurfaceLevelCollectionRectangle> {
		internal TextureLevelRectangle(TextureSurfaceRectangle stack, int level) : base(stack, level) { }

		protected override void ReadBase(IntPtr data, Format format) {
			using (Texture.Lock())
				GL.GetTexImage(Target, Level, format.PixelFormat.Value, format.PixelType.Value, data);
		}
	}

	public class TextureSurfaceLevelCollectionRectangle : TextureSurfaceLevelCollection<TextureRectangle, TextureSurfaceRectangle, TextureLevelRectangle, TextureSurfaceLevelCollectionRectangle> {
		internal TextureSurfaceLevelCollectionRectangle() { }
	}
}
