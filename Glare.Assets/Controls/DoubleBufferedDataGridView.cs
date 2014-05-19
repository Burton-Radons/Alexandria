using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Glare.Assets.Controls {
	public class DoubleBufferedDataGridView : DataGridView {
		public DoubleBufferedDataGridView()
			: base() {
				DoubleBuffered = true;
		}
	}
}
