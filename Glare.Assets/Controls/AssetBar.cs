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
		Asset Asset { get; set; }

		public AssetBar() {
			InitializeComponent();
		}

		private void OnButtonClick(object sender, EventArgs e) {
			if (Asset != null) {
				ContextMenuStrip contextMenu = Asset.GetContextMenu();
				contextMenu.Show(button, Point.Empty);
			}
		}
	}
}
