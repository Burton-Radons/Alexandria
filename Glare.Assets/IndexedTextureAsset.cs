using Glare;
using Glare.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Assets {
	/// <summary>
	/// A texture that is indexed by a palette.
	/// </summary>
	public class IndexedTextureAsset : TextureAsset {
		/// <summary>
		/// Get the dimensions of the texture.
		/// </summary>
		public Vector2i Dimensions { get { return ((Texture2D)Content).Dimensions; } }

		/// <summary>
		/// Get the height of the texture.
		/// </summary>
		public int Height { get { return Dimensions.Y; } }

		/// <summary>
		/// Get the indexed data of the texture.
		/// </summary>
		public int[] Indices { get; protected set; }

		/// <summary>
		/// Get the palette to use.
		/// </summary>
		public PaletteAsset Palette { get; protected set; }

		/// <summary>
		/// Get the bytes per row.
		/// </summary>
		public int Pitch { get; protected set; }

		/// <summary>Get the width in pixels of the texture.</summary>
		public int Width { get { return Dimensions.X; } }

		/// <summary>
		/// Initialise the texture.
		/// </summary>
		/// <param name="manager">The asset manager in use.</param>
		/// <param name="loader">The asset loader state.</param>
		/// <param name="palette"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="indices"></param>
		/// <param name="pitch"></param>
		public IndexedTextureAsset(AssetLoader loader, PaletteAsset palette, int width, int height, int[] indices, int pitch = -1)
			: this(loader) {
			Setup(palette, width, height, indices, pitch);
		}

		/// <summary>Initialise the texture.</summary>
		/// <param name="manager"></param>
		/// <param name="name"></param>
		/// <param name="palette"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="indices"></param>
		/// <param name="pitch"></param>
		public IndexedTextureAsset(AssetManager manager, string name, PaletteAsset palette, int width, int height, int[] indices, int pitch = -1)
			: this(manager, name) {
			Setup(palette, width, height, indices, pitch);
		}

		/// <summary>Initialise the texture.</summary>
		/// <param name="loader"></param>
		protected IndexedTextureAsset(AssetLoader loader)
			: base(loader, null) {
		}

		/// <summary>Initialise the texture.</summary>
		/// <param name="manager"></param>
		/// <param name="name"></param>
		protected IndexedTextureAsset(AssetManager manager, string name)
			: base(manager, name, null) {
		}

		/// <summary>Initialise the texture.</summary>
		/// <param name="parent"></param>
		/// <param name="loader"></param>
		protected IndexedTextureAsset(FolderAsset parent, AssetLoader loader) : base(parent, loader, null) { }

		static int ConvertColor(Color color) { return color.B | (color.G << 8) | (color.R << 16) | (color.A << 24); }

		/// <summary>Setup the texture.</summary>
		/// <param name="palette"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="indices"></param>
		/// <param name="pitch"></param>
		protected void Setup(PaletteAsset palette, int width, int height, int[] indices, int pitch = -1) {
			if (pitch < 0)
				pitch = width;
			Indices = indices;
			Palette = palette;
			Pitch = pitch;
			Update(width, height);
		}

		/// <summary>Update the texture.</summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		protected void Update(int width, int height) {
			if (Indices == null || Palette == null)
				return;

			int[] colors = new int[Palette.Colors.Count];
			for (int index = 0; index < colors.Length; index++)
				colors[index] = ConvertColor(Palette.Colors[index]);

			int[] indices = Indices;
			int[] rgba = new int[width * height];
			int colorCount = colors.Length;
			int purple = ConvertColor(Color.Purple);

			for (int row = 0; row < height; row++) {
				for (int input = row * Pitch, inputEnd = input + width, output = row * width; input < inputEnd; input++, output++) {
					int index = indices[input];
					int color = index < colorCount && index >= 0 ? colors[index] : purple;
					rgba[output] = color;
				}
			}

			var texture = new Texture2D();
			texture.MaxLod = 0;
			texture.MipmapFilter = TextureFilter.None;
			texture.Data(width, height, Glare.Graphics.Formats.Vector4srgba, rgba);
			Content = texture;
		}
	}
}
