using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics.Rendering {
	public abstract class TextureCache {
		readonly Context platform;
		readonly Format format;
		readonly Vector4i dimensions;
		protected int currentIndex;

		public int CurrentIndex { get { return currentIndex; } }

		public Vector4i Dimensions { get { return dimensions; } }

		public Format Format { get { return format; } }

		public TextureCache(Format format, Vector4i dimensions) {
			this.format = format;
			this.dimensions = dimensions;
		}
	}

	public abstract class TextureCache<TTexture> : TextureCache where TTexture : Texture {
		readonly List<TTexture> list = new List<TTexture>();

		public TTexture Current { get { return list[currentIndex]; } }

		public TextureCache(Format format, Vector4i dimensions, int initialCount = 0)
			: base(format, dimensions) {
			Insert(initialCount);
			currentIndex = 0;
		}

		/// <summary>Move to the next texture in the cache.</summary>
		/// <returns></returns>
		public TTexture Advance() {
			currentIndex = (currentIndex + 1) % list.Count;
			return list[currentIndex];
		}

		/// <summary>Get a texture that is offset from the current position.</summary>
		/// <param name="offset">The offset from the current position. 0 is the current texture, 1 is the next one, -1 is the last, and so on.</param>
		/// <returns></returns>
		public TTexture Index(int offset) { return list[((currentIndex + offset) % list.Count + list.Count) % list.Count]; }

		/// <summary>
		/// Insert a texture into the end of the buffer.
		/// </summary>
		/// <returns></returns>
		public TTexture Insert() {
			TTexture texture = Texture.Create<TTexture>(Format, Dimensions);
			list.Insert(currentIndex, texture);
			currentIndex++;
			return texture;
		}

		/// <summary>Insert a number of textures.</summary>
		/// <param name="count"></param>
		public void Insert(int count) { while (count-- > 0) Insert(); }
	}

	public class TextureCache2D : TextureCache<Texture2D> {
		public TextureCache2D(Format format, Vector4i dimensions, int initialCount = 0) : base(format, dimensions, initialCount) { }
		public TextureCache2D(Format format, Vector2i dimensions, int initialCount = 0) : this(format, new Vector4i(dimensions, 1, 1), initialCount) { }
	}

	public class TextureCacheRectangle : TextureCache<TextureRectangle> {
		public TextureCacheRectangle(Format format, Vector4i dimensions, int initialCount = 0) : base(format, dimensions, initialCount) { }
		public TextureCacheRectangle(Format format, Vector2i dimensions, int initialCount = 0) : this(format, new Vector4i(dimensions, 1, 1), initialCount) { }
	}
}
