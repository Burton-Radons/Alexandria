using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alexandria {
	[System.ComponentModel.DesignerCategory("Code")]
	public class BetterRichTextBox : RichTextBox {
		const int WM_USER = 0x0400;
		const int EM_GETSCROLLPOS = WM_USER + 221;
		const int EM_SETSCROLLPOS = WM_USER + 222;

		public Point ScrollPosition {
			get {
				unsafe {
					Point point;
					SendMessage(Handle, EM_GETSCROLLPOS, IntPtr.Zero, new IntPtr(&point));
					return point;
				}
			}

			set {
				unsafe {
					SendMessage(Handle, EM_SETSCROLLPOS, IntPtr.Zero, new IntPtr(&value));
				}
			}
		}

		public BetterRichTextBox() {
			SetStyle(ControlStyles.DoubleBuffer, true);
		}

		[System.Runtime.InteropServices.DllImport("user32.dll")]
		private extern static IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

		public void BeginUpdate() { SendMessage(Handle, 0xb, (IntPtr)0, IntPtr.Zero); }
		public void EndUpdate() { SendMessage(Handle, 0xb, (IntPtr)1, IntPtr.Zero); }
	}

}
