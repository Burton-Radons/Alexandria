using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Glare.Assets;

namespace Glare.Assets.Controls {
	public partial class AssetBar : UserControl {
		Asset asset;

		public Asset Asset {
			get { return asset; }

			set {
				asset = value;
				label.Text = asset != null ? asset.DisplayName : "";
				new ToolTip().SetToolTip(label, label.Text);
			}
		}

		public AssetBar() {
			InitializeComponent();
		}

		private void OnButtonClick(object sender, EventArgs e) {
			if (Asset != null) {
				ContextMenuStrip contextMenu = Asset.GetContextMenu();
				contextMenu.Show(button, Point.Empty);
			}
		}

		private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e) {

		}

		private void button_Click(object sender, EventArgs e) {
			if (Asset != null) {
				var contextMenu = Asset.GetContextMenu();
				contextMenu.Show(MousePosition);
			}
		}
	}
}
