using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Glare.Internal;

namespace Glare.Graphics {
	public class Texture2D : FlatTexture, IResourceSource<Texture2D> {
		TextureSurface2D surface;

		Texture2D IResourceSource<Texture2D>.GetResourceValue() { return this; }

		protected override TextureSurface BaseSurface { get { return Surface; } }

		public Vector2i Dimensions { get { return Levels[0].Dimensions; } }

		/// <summary>Get the <see cref="TextureLevel2D"/> collection of the <see cref="Texture2D"/>'s one <see cref="Surface"/>.</summary>
		public new TextureSurfaceLevelCollection2D Levels { get { return Surface.Levels; } }

		/// <summary>Get the sole <see cref="TextureSurface"/> that makes up this <see cref="Texture2D"/>.</summary>
		public new TextureSurface2D Surface { get { return surface ?? (surface = new TextureSurface2D(this)); } }

		protected internal override TextureTarget Target { get { return TextureTarget.Texture2D; } }

		protected internal override GetPName TargetBinding { get { return GetPName.TextureBinding2D; } }

		public Texture2D()
			: base() {
		}

		public Texture2D(Format format, int width, int height) : this(format, new Vector2i(width, height)) { }
		public Texture2D(Format format, Vector2i dimensions) : base(format, new Vector4i(dimensions, 1, 1)) { }

		public Texture2D(string path) : base(path) { }

		public Texture2D Data(int width, int height, Format storageFormat, IntPtr pointer, Format uploadFormat = null) {
			Data2D(0, new Vector2i(width, height), storageFormat, pointer, uploadFormat);
			return this;
		}

		public Texture2D Data<T>(int width, int height, Format storageFormat, T[] data, int start = 0, Format uploadFormat = null) where T : struct {
			Data2D<T>(0, new Vector2i(width, height), storageFormat, data, start, uploadFormat);
			return this;
		}

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

	public class TextureSurface2D : TextureSurface<Texture2D, TextureSurface2D, TextureLevel2D, TextureSurfaceLevelCollection2D> {
		internal TextureSurface2D(Texture2D texture) : base(texture) { }

		protected internal override TextureLevel2D CreateLevel(int index) { return new TextureLevel2D(this, index); }
	}

	public class TextureLevel2D : TextureLevel<Texture2D, TextureSurface2D, TextureLevel2D, TextureSurfaceLevelCollection2D> {
		internal TextureLevel2D(TextureSurface2D stack, int level) : base(stack, level) { }

		protected override void ReadBase(IntPtr data, Format format) {
			using (Texture.Lock()) {
				if (format.IsCompressed)
					GL.GetCompressedTexImage((TextureTarget)Target, Level, data);
				else
					GL.GetTexImage((TextureTarget)Target, Level, format.pixelFormat, format.pixelType, data);
			}
		}

		protected override void DataCompressedBase(Format format, Vector2i dimensions, IntPtr data) {
			Texture.Format = format;
			int imageSize = format.ByteSize(dimensions);
			using (Texture.Lock())
				GL.CompressedTexImage2D((TextureTarget)Target, Level, format.pixelInternalFormat, dimensions.X, dimensions.Y, 0, imageSize, data);
		}
	}

	public class TextureSurfaceLevelCollection2D : TextureSurfaceLevelCollection<Texture2D, TextureSurface2D, TextureLevel2D, TextureSurfaceLevelCollection2D> {
		internal TextureSurfaceLevelCollection2D() { }
	}
}
