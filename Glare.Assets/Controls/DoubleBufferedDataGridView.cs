using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Glare.Assets.Controls {
	/// <summary>
	/// A <see cref="DataGridView"/> that enables double buffering.
	/// </summary>
	public class DoubleBufferedDataGridView : DataGridView {
		/// <summary>
		/// Initialise the <see cref="DoubleBufferedDataGridView"/>.
		/// </summary>
		public DoubleBufferedDataGridView()
			: base() {
				DoubleBuffered = true;
		}
	}
}
