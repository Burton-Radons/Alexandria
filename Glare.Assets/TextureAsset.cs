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
	public class TextureAsset : Asset {
		public override object GlareObject { get { return Content; } }

		public Glare.Graphics.Texture Content { get; protected set; }

		public TextureAsset(AssetManager manager, Glare.Graphics.Texture texture, string name, string description = null)
			: base(manager, name, description) {
			Content = texture;
		}

		public override Control Browse() {
			Texture2D texture2d = Content as Texture2D;
			TextureLevel level;

			if (texture2d != null) {
				level = texture2d.Levels[0];
			} else
				throw new NotImplementedException();

			byte[] data = level.Read<byte>(TextureFormats.Vector4srgba);
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
