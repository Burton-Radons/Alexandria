using Glare.Assets;
using Glare.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Glare.Assets {
	/// <summary>A simple <see cref="Texture"/> asset.</summary>
	public class TextureAsset : Asset {
		/// <summary>
		/// Get the texture.
		/// </summary>
		public override object GlareObject { get { return Content; } }

		/// <summary>Get the texture that is the content of this asset.</summary>
		public Glare.Graphics.Texture Content { get; protected set; }

		/// <summary>Initialise the asset.</summary>
		/// <param name="loader"></param>
		/// <param name="texture"></param>
		public TextureAsset(AssetLoader loader, Glare.Graphics.Texture texture)
			: base(loader) {
			Content = texture;
		}

		/// <summary>Initialise the asset.</summary>
		/// <param name="manager"></param>
		/// <param name="name"></param>
		/// <param name="texture"></param>
		public TextureAsset(AssetManager manager, string name, Texture texture)
			: base(manager, name) {
			Content = texture;
		}

		/// <summary>Initialise the asset.</summary>
		/// <param name="parent"></param>
		/// <param name="loader"></param>
		/// <param name="texture"></param>
		public TextureAsset(FolderAsset parent, AssetLoader loader, Glare.Graphics.Texture texture) : base(parent, loader) { }

		/// <summary>Display the texture in a window.</summary>
		/// <param name="progressUpdateCallback"></param>
		/// <returns></returns>
		public override Control Browse(Action<double> progressUpdateCallback = null) {
			Texture2D texture2d = Content as Texture2D;
			TextureLevel level;

			if (texture2d != null) {
				level = texture2d.Levels[0];
			} else
				throw new NotImplementedException();

			byte[] data = level.Read<byte>(Glare.Graphics.Formats.Vector4nbBGRA);//Vector4srgba);

			/*for (int index = 0; index < data.Length; index += 4) {
				byte a = data[index], b = data[index + 1], c = data[index + 2], d = data[index = 3];
				data[index + 0] = c;
				data[index + 1] = b;
				data[index + 2] = a;
				data[index + 3] = d;
			}*/

			PixelFormat pixelFormat = PixelFormat.Format32bppRgb;
			Bitmap bitmap = new Bitmap(level.Dimensions.X, level.Dimensions.Y, pixelFormat);
			BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.WriteOnly, pixelFormat);
			Marshal.Copy(data, 0, bitmapData.Scan0, bitmapData.Stride * bitmapData.Height);
			bitmap.UnlockBits(bitmapData);

			var box = new PictureBox() {
				SizeMode = PictureBoxSizeMode.AutoSize,
				Image = bitmap
			};

			var panel = new Panel() {
				AutoScroll = true,
				AutoSize = true,
				AutoSizeMode = AutoSizeMode.GrowAndShrink,
			};
			panel.Controls.Add(box);

			return panel;
		}

		/*public override Widget Browse() {
			Texture2D texture2d = texture as Texture2D;
			TextureLevel level;

			if (texture2d != null) {
				level = texture2d.Levels[0];
			} else
				throw new NotImplementedException();

			byte[] data = level.Read<byte>(Formats.Vector4nb);
			Pixbuf pixbuf = new Pixbuf(data, Colorspace.Rgb, true, 8, level.Dimensions.X, level.Dimensions.Y, level.Dimensions.X * 4);
			Gtk.Image image = new Gtk.Image(pixbuf);
			return image;
		}*/
	}
}
