using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glare.Internal;
using System.Runtime.InteropServices;

namespace Glare.Graphics {
	public abstract class TextureObject {
		public Texture Texture { get { return TextureBase; } }

		protected abstract Texture TextureBase { get; }
	}

	/// <summary>
	/// A 2-dimensional <see cref="Texture"/> surface. For a <see cref="Texture2D"/>, for example, these are the individual LOD levels. For a <see cref="TextureCube"/>, these would be individual LOD levels of a face on the cube.
	/// </summary>
	public abstract class TextureLevel : TextureObject {
		protected readonly int level;

		public virtual Vector2i Dimensions {
			get {
				int dimensionsX, dimensionsY;
				using (Texture.Lock()) {
					GL.GetTexLevelParameter(Target, Level, GetTextureParameter.TextureWidth, out dimensionsX);
					Context.CheckError();
					GL.GetTexLevelParameter(Target, Level, GetTextureParameter.TextureHeight, out dimensionsY);
					return new Vector2i(dimensionsX, dimensionsY);
				}
			}
		}

		/// <summary>Get the LOD level of the <see cref="TextureLevel"/>.</summary>
		public int Level { get { return level; } }

		public TextureSurface Surface { get { return SurfaceBase; } }

		protected abstract TextureSurface SurfaceBase { get; }

		internal TextureTarget Target { get { return (TextureTarget)SurfaceBase.Target; } }

		protected override Texture TextureBase { get { return SurfaceBase.Texture; } }

		public TextureLevel(int level) {
			this.level = level;
		}

		public T[] Read<T>(Format format = null) where T : struct {
			if (format == null)
				format = Texture.Format;
			T[] data = new T[format.ByteSize(Dimensions)];
			Read(data, 0, format);
			return data;
		}

		public void Read<T>(T[] data, int start = 0, Format format = null) where T : struct {
			IntPtr pointer;
			if (format == null)
				format = Texture.Format;
			if (data == null)
				throw new ArgumentNullException("data");
			if (start + format.ByteSize(Dimensions) > data.Length)
				throw new ArgumentOutOfRangeException("start");
			using (data.Pin(out pointer, start))
				Read(pointer, format);
		}

		public void Read(IntPtr data, Format format) {
			if (format == null)
				format = Texture.Format;
			if (data == null)
				throw new ArgumentNullException("data");
			ReadBase(data, format);
		}

		protected virtual void ReadBase(IntPtr data, Format format) { throw new NotImplementedException(); }

		public void DataCompressed<T>(Format format, int width, int height, T[] data, int start = 0) where T : struct { DataCompressed(format, new Vector2i(width, height), data, start); }

		public void DataCompressed<T>(Format format, Vector2i dimensions, T[] data, int start = 0) where T : struct {
			IntPtr pointer;
			using (GetData(format, dimensions, data, start, out pointer))
				DataCompressed(format, dimensions, pointer);
		}

		public void DataCompressed(Format format, Vector2i dimensions, IntPtr data) {
			if (format == null || !format.IsCompressed)
				throw new ArgumentException("format");
			DataCompressedBase(format, dimensions, data);
		}

		protected virtual void DataCompressedBase(Format format, Vector2i dimensions, IntPtr data) { throw new NotImplementedException(); }

		GCPinnedHandle GetData<T>(Format format, Vector2i dimensions, T[] data, int start, out IntPtr pointer) {
			if (data == null)
				throw new ArgumentNullException("data");
			if (start + format.ByteSize(dimensions) / Marshal.SizeOf(typeof(T)) > data.Length)
				throw new ArgumentOutOfRangeException("start");
			return data.Pin(out pointer, start);
		}
	}

	public abstract class TextureLevel<TTexture, TTextureSurface, TTextureLevel, TTextureSurfaceLevelCollection> : TextureLevel
		where TTexture : Texture
		where TTextureSurface : TextureSurface<TTexture, TTextureSurface, TTextureLevel, TTextureSurfaceLevelCollection>
		where TTextureLevel : TextureLevel<TTexture, TTextureSurface, TTextureLevel, TTextureSurfaceLevelCollection>
		where TTextureSurfaceLevelCollection : TextureSurfaceLevelCollection<TTexture, TTextureSurface, TTextureLevel, TTextureSurfaceLevelCollection> {
		readonly TTextureSurface surface;

		public new TTexture Texture { get { return surface.Texture; } }

		protected override Texture TextureBase { get { return surface.Texture; } }

		public new TTextureSurface Surface { get { return surface; } }

		protected override TextureSurface SurfaceBase { get { return surface; } }

		internal TextureLevel(TTextureSurface stack, int level)
			: base(level) {
			this.surface = stack;
		}

		public Vector4i Size {
			get {
				int old = Texture.Bind();
				try {
					var target = Target;
					int x, y, z, w;

					GL.GetTexLevelParameter(target, level, GetTextureParameter.TextureWidth, out x);
					GL.GetTexLevelParameter(target, level, GetTextureParameter.TextureHeight, out y);
					GL.GetTexLevelParameter(target, level, GetTextureParameter.TextureDepth, out z);
					GL.GetTexLevelParameter(target, level, GetTextureParameter.TextureSamples, out w);
					return new Vector4i(x, y, z, w);
				} finally { Texture.Unbind(old); }
			}
		}
	}
}
