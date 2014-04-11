using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Glare;

namespace Alexandria.Engines.Sciagi.Controls {
	public partial class ViewBrowserItem : UserControl {
		public ViewBrowserItem(string labelText, IList<ViewBrowserFrame> frames) {
			InitializeComponent();

			label.Text = labelText;
			picture.Image = frames[0].Image;
			picture.Size = frames[0].Image.Size;
		}

		public ViewBrowserItem(string labelText, Image image) : this(labelText, new ViewBrowserFrame[] { new ViewBrowserFrame(Vector2i.Zero, image) }) { }
	}

	public class ViewBrowserFrame {
		public Vector2i Offset { get; private set; }
		public Image Image { get; private set; }

		public ViewBrowserFrame(Vector2i offset, Image image) {
			Offset = offset;
			Image = image;
		}
	}
}
