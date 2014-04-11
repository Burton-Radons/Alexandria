using Glare;
using Glare.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Resources {
	public class IndexedTexture : Texture {
		public Vector2i Dimensions { get { return ((Texture2D)Content).Dimensions; } }

		public int Height { get { return Dimensions.Y; } }

		public byte[] Indices { get; protected set; }

		public Palette Palette { get; protected set; }

		public int Width { get { return Dimensions.X; } }

		public IndexedTexture(Manager manager, string name, Palette palette, int width, int height, byte[] indices)
			: this(manager, name) {
			Indices = indices;
			Palette = palette;
			Update(width, height);
		}

		protected IndexedTexture(Manager manager, string name)
			: base(manager, null, name) {
		}

		static int ConvertColor(Color color) { return color.B | (color.G << 8) | (color.R << 16) | (color.A << 24); }

		protected void Update(int width, int height) {
			if (Indices == null || Palette == null)
				return;

			int[] colors = new int[Palette.Colors.Count];
			for (int index = 0; index < colors.Length; index++)
				colors[index] = ConvertColor(Palette.Colors[index]);

			byte[] indices = Indices;
			int[] rgba = new int[width * height];
			int colorCount = colors.Length;
			int purple = ConvertColor(Color.Purple);

			for (int offset = 0, end = Math.Min(indices.Length, width * height); offset < end; offset++) {
				byte index = indices[offset];
				int color = index < colorCount ? colors[index] : purple;
				rgba[offset] = color;
			}

			var texture = new Texture2D();
			texture.MaxLod = 0;
			texture.MipmapFilter = TextureFilter.None;
			texture.Data(width, height, TextureFormats.Vector4srgba, rgba);
			Content = texture;
		}
	}
}
