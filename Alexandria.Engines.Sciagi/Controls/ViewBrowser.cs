using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Alexandria.Engines.Sciagi.Resources;
using Glare;

namespace Alexandria.Engines.Sciagi.Controls {
	/// <summary>
	/// A control to interact with a <see cref="Resources.View"/>.
	/// </summary>
	public partial class ViewBrowser : UserControl {
		/// <summary>
		/// The <see cref="Resources.View"/> to browse.
		/// </summary>
		public Resources.View View { get; private set; }

		/// <summary>
		/// Initialise the browser.
		/// </summary>
		/// <param name="view"></param>
		public ViewBrowser(Resources.View view) {
			View = view;

			InitializeComponent();

			foreach (var group in view.Groups) {
				string label = group.Index + ". Animation " + group.Animation.Index + (group.IsMirrored ? " (mirrored)" : "");
				groupsView.Items.Add(new ListViewItem(label));
			}

			foreach (var animation in view.Animations) {
				string label = animation.Index + ". " + animation.Unknowns.ToCommaSeparatedList() + " " + animation.Cells.Count + " frame" + (animation.Cells.Count == 1 ? "" : "s");
				animationsView.Items.Add(new ListViewItem(label));
			}

			Size smallImageSize = Size.Empty, largeImageSize = Size.Empty;

			

			foreach (ViewCell cell in view.Cells) {
				smallImageSize.Width = Math.Max(smallImageSize.Width, cell.Raster.Image.Width);
				smallImageSize.Height = Math.Max(smallImageSize.Height, cell.Raster.Image.Height);
				largeImageSize.Width = Math.Max(largeImageSize.Width, cell.Raster.Image4x.Width);
				largeImageSize.Height = Math.Max(largeImageSize.Height, cell.Raster.Image4x.Height);
			}

			foreach (ViewCell cell in view.Cells) {
				string label = cell.Index + ". ";
				if (cell.Offset != Vector2i.Zero)
					label += "offset " + cell.Offset.X + "x" + cell.Offset.Y + ", ";
				label += "size " + cell.Raster.Width + "x" + cell.Raster.Height;

				cellsView.Controls.Add(new ViewBrowserItem(label, cell.Raster.Image4x));
				//cellsView.Items.Add(new ListViewItem(label, cell.Index));
			}
		}
	}
}
