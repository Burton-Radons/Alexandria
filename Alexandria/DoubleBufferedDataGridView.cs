using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alexandria {
	public class DoubleBufferedDataGridView : DataGridView {
		public DoubleBufferedDataGridView()
			: base() {
				DoubleBuffered = true;
		}
	}
}
