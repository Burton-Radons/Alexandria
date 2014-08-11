using Glare.Graphics.Internal;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Glare.Internal;

namespace Glare.Graphics {
	public enum DepthStencilTextureMode {
		Depth,
		Stencil,
	}

	public abstract class Texture : SamplerResource, IResourceSource {
		Format format;

		object IResourceSource.GetResourceValue() { return this; }

		protected internal abstract TextureTarget Target { get; }
		protected internal abstract GetPName TargetBinding { get; }

		/*public DepthStencilTextureMode DepthStencilTextureMode
		{
			get { return GetIntParameter(GetTextureParameter.DepthTextureMode); }
			set { SetParameter(ArbD
		}*/

		/// <summary>Get or set the index of the lowest defined mipmap level. This is an integer value. The initial value is 0.</summary>
		public int BaseLevel {
			get { return Get1i(GetTextureParameter.TextureBaseLevel); }
			set { Set(TextureParameterName.TextureBaseLevel, value); }
		}

		/// <summary>Get the format of the texture data.</summary>
		public Format Format { get { return format; } internal set { format = value; } }

		internal Texture()
			: base(CreateTexture()) {
			//Set(TextureParameterName.GenerateMipmap, true);
		}

		internal Texture(Format format, Vector4i dimensions)
			: this() {
			Storage(format, dimensions);
		}

		public static TTexture Create<TTexture>() where TTexture : Texture {
			switch (typeof(TTexture).Name) {
				case "Texture1D": return (TTexture)(Texture)new Texture1D();
				case "Texture1DArray": return (TTexture)(Texture)new Texture1DArray();
				case "Texture2D": return (TTexture)(Texture)new Texture2D();
				case "Texture2DArray": return (TTexture)(Texture)new Texture2DArray();
				case "Texture3D": return (TTexture)(Texture)new Texture3D();
				case "TextureCube": return (TTexture)(Texture)new TextureCube();
				case "TextureCubeArray": return (TTexture)(Texture)new TextureCubeArray();
				default: throw new NotSupportedException();
			}
		}

		public static TTexture Create<TTexture>(Format format, Vector4i dimensions) where TTexture : Texture {
			var texture = Create<TTexture>();
			texture.Storage(format, dimensions);
			return texture;
		}

		public static TTexture Create<TTexture>(Format format, int dimensionsX, int dimensionsY = 1, int dimensionsZ = 1, int dimensionsW = 1) where TTexture : Texture {
			return Create<TTexture>(format, new Vector4i(dimensionsX, dimensionsY, dimensionsZ, dimensionsW));
		}

		public static TTexture Create<TTexture>(Format format, Vector2i dimensions, int dimensionsZ = 1, int dimensionsW = 1) where TTexture : Texture {
			return Create<TTexture>(format, dimensions.X, dimensions.Y, dimensionsZ, dimensionsW);
		}

		public static TTexture Create<TTexture>(Format format, Vector3i dimensions, int dimensionsW = 1) where TTexture : Texture {
			return Create<TTexture>(format, dimensions.X, dimensions.Y, dimensions.Z, dimensionsW);
		}

		/// <summary>Create a texture with the given dimensions that is filled with random data. It has <see cref="TextureFormats.Vector4nb"/>.</summary>
		/// <param name="context">The <see cref="Context"/> to create the texture with.</param>
		/// <param name="width">The width in pixels of the texture.</param>
		/// <param name="height">The height in pixels of the texture.</param>
		/// <returns>The random texture.</returns>
		public static Texture2D CreateRandom4nb(int width, int height) {
			byte[] data = new byte[width * height * 4];
			Random rng = new Random();
			rng.NextBytes(data);
			var texture = new Texture2D();
			texture.Data(width, height, Formats.Vector4nb, data);
			return texture;
		}

		public static Texture2D CreateRandom4nb(Vector2i dimensions) { return CreateRandom4nb(dimensions.X, dimensions.Y); }

		static int CreateTexture() { using (Context.Lock()) return GL.GenTexture(); }

		protected void Data2D(int level, Vector2i dimensions, Format storageFormat, IntPtr pointer, Format uploadFormat = null) {
			int maxSize = Device.Capabilities.MaxTextureDimension2D;

			if (level < 0)
				throw new ArgumentOutOfRangeException("level");
			if (dimensions.X < 0 || dimensions.X > maxSize)
				throw new ArgumentOutOfRangeException("dimensions.X");
			if (dimensions.Y < 0 || dimensions.Y > maxSize)
				throw new ArgumentOutOfRangeException("dimensions.Y");
			if (storageFormat == null)
				throw new ArgumentNullException("storageFormat");
			if (uploadFormat == null)
				uploadFormat = storageFormat;

			using (Lock()) {
				GL.TexImage2D(Target, level, storageFormat.PixelInternalFormat.Value, dimensions.X, dimensions.Y, 0, uploadFormat.PixelFormat.Value, uploadFormat.PixelType.Value, pointer);
				Context.CheckError();
			}
		}

		protected void Data2D<T>(int level, Vector2i dimensions, Format storageFormat, T[] data, int start = 0, Format uploadFormat = null) where T : struct {
			if (data == null)
				throw new ArgumentNullException("data");
			if (uploadFormat == null)
				uploadFormat = storageFormat;
			if (start > data.Length || start < 0)
				throw new ArgumentOutOfRangeException("data");
			if (dimensions.Product * uploadFormat.BytesPerSample / Marshal.SizeOf(typeof(T)) + start > data.Length)
				throw new ArgumentOutOfRangeException("data");

			IntPtr pointer;
			using (data.Pin(out pointer, start))
				Data2D(level, dimensions, storageFormat, pointer, uploadFormat);
		}

		protected override void DisposeBase() {
			if (Context.Shared.MakeCurrent())
				GL.DeleteTexture(Id);
		}

		protected internal int Bind() {
			int value = Device.GetInt32(TargetBinding);
			GL.BindTexture(Target, Id);
			Context.CheckError();
			return value;
		}

		public static int CalculateLevels(Vector4i dimensions) {
			int count = 1;
			if (dimensions.X == 0 || dimensions.Y == 0 || dimensions.Z == 0 || dimensions.W == 0)
				throw new Exception();
			while (true) {
				dimensions.X = Math.Max(1, dimensions.X / 2);
				dimensions.Y = Math.Max(1, dimensions.Y / 2);
				dimensions.Z = Math.Max(1, dimensions.Z / 2);
				dimensions.W = Math.Max(1, dimensions.W / 2);
				if (dimensions == Vector4i.One)
					return count;
				count++;
			}
		}

		public static int CalculateLevels(int x, int y = 1, int z = 1, int w = 1) { return CalculateLevels(new Vector4i(x, y, z, w)); }

		bool GetBoolean(GetTextureParameter pname) { return Get1i(pname) != 0; }
		int Get1i(GetTextureParameter pname) { int result; using (Lock()) GL.GetTexParameter(Target, pname, out result); return result; }
		protected override double Get1d(SamplerParameterName pname) { float result; using (Lock()) GL.GetTexParameter(Target, (GetTextureParameter)pname, out result); return result; }
		protected override int Get1i(SamplerParameterName pname) { return Get1i((GetTextureParameter)pname); }

		internal TextureLock Lock() { return new TextureLock(this); }

		void Set(TextureParameterName pname, int value) { using (Lock()) GL.TexParameter(Target, pname, value); }
		void Set(TextureParameterName pname, bool value) { Set(pname, value ? 1 : 0); }
		void Set(TextureParameterName pname, double value) { using (Lock()) GL.TexParameter(Target, pname, checked((float)value)); }
		protected override void Set(SamplerParameterName pname, double value) { Set((TextureParameterName)pname, value); }
		protected override void Set(SamplerParameterName pname, int value) { Set((TextureParameterName)pname, value); }

		/// <summary>Set the dimensions of the entire texture, assigning level-of-detail chains appropriately. Unused dimensions need to be 1.</summary>
		/// <param name="dimensions">The dimensions of the <see cref="Texture"/>.</param>
		/// <param name="format"></param>
		public abstract void Storage(int levels, Format format, Vector4i dimensions);

		public void Storage(Format format, Vector4i dimensions) { Storage(CalculateLevels(dimensions), format, dimensions); }

		protected void Storage2D(int levels, Format format, Vector4i dimensions) {
			if (dimensions.Z != 1 || dimensions.W != 1)
				throw new ArgumentException();
			if (format == null)
				throw new ArgumentNullException("format");
			this.format = format;

			using (Lock())
				GL.TexStorage2D((TextureTarget2d)Target, levels, (SizedInternalFormat)format.PixelInternalFormat.Value, dimensions.X, dimensions.Y);
		}

		protected internal void Unbind(int old) {
			GL.BindTexture(Target, Id);
			Context.CheckError();
		}
	}

	internal struct TextureLock : IDisposable {
		Texture texture;
		int old;

		public TextureLock(Texture texture) {
			this.texture = texture;
			this.old = texture.Bind();
		}

		public void Dispose() {
			try { Context.CheckError(); } finally { GL.BindTexture(texture.Target, old); }
		}
	}

	/// <summary>A <see cref="Texture"/> that has one or two dimensions, which includes <see cref="Texture1D"/>, <see cref="Texture1DArray"/>, and <see cref="Texture2D"/>.</summary>
	public abstract class FlatTexture : Texture {
		protected abstract TextureSurface BaseSurface { get; }

		/// <summary>Get the <see cref="TextureLevel"/> collection for the <see cref="Surface"/>.</summary>
		public TextureSurfaceLevelCollection Levels { get { return BaseSurface.Levels; } }

		/// <summary>Get the primary surface of the <see cref="LayeredTexture"/>.</summary>
		public TextureSurface Surface { get { return BaseSurface; } }

		internal FlatTexture() : base() { }
		internal FlatTexture(Format format, Vector4i dimensions) : base(format, dimensions) { }
	}

	/// <summary>A <see cref="Texture"/> that has three or four dimensions or multiple faces, which includes <see cref="Texture2DArray"/>, <see cref="Texture3D"/>, <see cref="TextureCube"/>, and <see cref="TextureCubeArray"/>.</summary>
	public abstract class LayeredTexture : Texture {
		internal LayeredTexture() : base() { }
		internal LayeredTexture(Format format, Vector4i dimensions) : base(format, dimensions) { }
	}
}
