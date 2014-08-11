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
	/// <summary>
	/// Provides better support for critical functionality than the builtin <see cref="RichTextBox"/>.
	/// </summary>
	[System.ComponentModel.DesignerCategory("Code")]
	public class BetterRichTextBox : RichTextBox {
		const int WM_USER = 0x0400;
		const int EM_GETSCROLLPOS = WM_USER + 221;
		const int EM_SETSCROLLPOS = WM_USER + 222;

		/// <summary>Get or set the scroll position.</summary>
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

		/// <summary>Initialize the <see cref="BetterRichTextBox"/>.</summary>
		public BetterRichTextBox() {
			SetStyle(ControlStyles.DoubleBuffer, true);
		}

		[System.Runtime.InteropServices.DllImport("user32.dll")]
		private extern static IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

		/// <summary>Start updating the text box.</summary>
		public void BeginUpdate() { SendMessage(Handle, 0xb, (IntPtr)0, IntPtr.Zero); }

		/// <summary>Finish updating the text box.</summary>
		public void EndUpdate() { SendMessage(Handle, 0xb, (IntPtr)1, IntPtr.Zero); }
	}

}
