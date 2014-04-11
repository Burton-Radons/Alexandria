using Glare.Internal;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics
{
	/// <summary>
	/// A stack of <see cref="Texture"/> <see cref="TextureLevel"/>s by level of detail. A <see cref="Texture2D"/> has just one <see cref="TextureSurface"/>, but a <see cref="Texture3D"/> would have one for each level of the texture, and a <see cref="TextureCube"/> has a <see cref="TextureSurface"/> for each face of the cube.
	/// </summary>
	public abstract class TextureSurface
	{
		public TextureSurfaceLevelCollection Levels { get { return LevelsBase; } }

		protected abstract TextureSurfaceLevelCollection LevelsBase { get; }

		/// <summary>Get the <see cref="Glare.Graphics.Texture"/> this is a <see cref="TextureSurface"/> for. Subclasses of this class override this property with more specific types.</summary>
		public Texture Texture { get { return TextureBase; } }

		internal virtual TextureTarget Target { get { return TextureBase.Target; } }

		protected abstract Texture TextureBase { get; }
	}

	public abstract class TextureSurface<TTexture, TTextureSurface, TTextureLevel, TTextureSurfaceLevelCollection> : TextureSurface
		where TTexture : Texture
		where TTextureSurface : TextureSurface<TTexture, TTextureSurface, TTextureLevel, TTextureSurfaceLevelCollection>
		where TTextureLevel : TextureLevel<TTexture, TTextureSurface, TTextureLevel, TTextureSurfaceLevelCollection>
		where TTextureSurfaceLevelCollection : TextureSurfaceLevelCollection<TTexture, TTextureSurface, TTextureLevel, TTextureSurfaceLevelCollection>
	{
		readonly TTexture texture;
		readonly TTextureSurfaceLevelCollection levels;

		public new TTextureSurfaceLevelCollection Levels { get { return levels; } }

		protected override TextureSurfaceLevelCollection LevelsBase { get { return levels; } }

		public new TTexture Texture { get { return texture; } }

		protected override Texture TextureBase { get { return texture; } }

		internal TextureSurface(TTexture texture)
		{
			this.texture = texture;
			this.levels = Extensions.Construct<TTextureSurfaceLevelCollection>();
			this.levels.surface = (TTextureSurface)this;
		}

		protected internal abstract TTextureLevel CreateLevel(int index);
	}

	public abstract class TextureSurfaceLevelCollection
	{
		public TextureLevel this[int index] { get { return GetIndex(index); } }

		protected abstract TextureLevel GetIndex(int index);
	}

	public abstract class TextureSurfaceLevelCollection<TTexture, TTextureSurface, TTextureLevel, TTextureSurfaceLevelCollection> : TextureSurfaceLevelCollection
		where TTexture : Texture
		where TTextureSurface : TextureSurface<TTexture, TTextureSurface, TTextureLevel, TTextureSurfaceLevelCollection>
		where TTextureLevel : TextureLevel<TTexture, TTextureSurface, TTextureLevel, TTextureSurfaceLevelCollection>
		where TTextureSurfaceLevelCollection : TextureSurfaceLevelCollection<TTexture, TTextureSurface, TTextureLevel, TTextureSurfaceLevelCollection>
	{
		internal TTextureSurface surface;
		readonly Dictionary<int, TTextureLevel> levels = new Dictionary<int, TTextureLevel>();

		internal TextureSurfaceLevelCollection() { }

		public new TTextureLevel this[int index]
		{
			get
			{
				TTextureLevel level;

				if (!levels.TryGetValue(index, out level))
					levels[index] = level = surface.CreateLevel(index);
				return level;
			}
		}

		protected override TextureLevel GetIndex(int index) { return this[index]; }
	}
}
