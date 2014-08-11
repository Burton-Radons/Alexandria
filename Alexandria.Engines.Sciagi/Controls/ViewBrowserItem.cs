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
	/// <summary>
	/// A browser for a <see cref="ViewBrowserFrame"/>.
	/// </summary>
	public partial class ViewBrowserItem : UserControl {
		/// <summary>
		/// Initialise the object.
		/// </summary>
		/// <param name="labelText"></param>
		/// <param name="frames"></param>
		public ViewBrowserItem(string labelText, IList<ViewBrowserFrame> frames) {
			InitializeComponent();

			label.Text = labelText;
			picture.Image = frames[0].Image;
			picture.Size = frames[0].Image.Size;
		}

		/// <summary>
		/// Initialise the object.
		/// </summary>
		/// <param name="labelText"></param>
		/// <param name="image"></param>
		public ViewBrowserItem(string labelText, Image image) : this(labelText, new ViewBrowserFrame[] { new ViewBrowserFrame(Vector2i.Zero, image) }) { }
	}

	/// <summary>Displays an image.</summary>
	public class ViewBrowserFrame {
		/// <summary>
		/// The offset this is displayed at.
		/// </summary>
		public Vector2i Offset { get; private set; }

		/// <summary>
		/// The image this displays.
		/// </summary>
		public Image Image { get; private set; }

		/// <summary>
		/// Initialise the object.
		/// </summary>
		/// <param name="offset"></param>
		/// <param name="image"></param>
		public ViewBrowserFrame(Vector2i offset, Image image) {
			Offset = offset;
			Image = image;
		}
	}
}
