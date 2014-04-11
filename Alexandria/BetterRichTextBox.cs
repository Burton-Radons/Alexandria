using Alexandria.Ole;
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

		readonly BetterRichTextBoxOleCallback OleCallback;

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
			OleCallback = new BetterRichTextBoxOleCallback(this);
		}

		protected override object CreateRichEditOleCallback() {
			return OleCallback;
		}

		[System.Runtime.InteropServices.DllImport("user32.dll")]
		private extern static IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

		public void BeginUpdate() { SendMessage(Handle, 0xb, (IntPtr)0, IntPtr.Zero); }
		public void EndUpdate() { SendMessage(Handle, 0xb, (IntPtr)1, IntPtr.Zero); }
	}

	class BetterRichTextBoxOleCallback : IRichEditOleCallback {
		readonly BetterRichTextBox Box;

		internal BetterRichTextBoxOleCallback(BetterRichTextBox box) {
			Box = box;
		}

		public int GetNewStorage(out IStorage ret) {
			throw new NotImplementedException();
		}

		public int GetInPlaceContext(IntPtr lplpFrame, IntPtr lplpDoc, IntPtr lpFrameInfo) {
			throw new NotImplementedException();
		}

		public int ShowContainerUI(int fShow) {
			throw new NotImplementedException();
		}

		public int QueryInsertObject(ref Guid lpclsid, IntPtr lpstg, int cp) {
			throw new NotImplementedException();
		}

		public int DeleteObject(IntPtr lpoleobj) {
			throw new NotImplementedException();
		}

		public int QueryAcceptData(System.Runtime.InteropServices.ComTypes.IDataObject lpdataobj, IntPtr lpcfFormat, int reco, int fReally, IntPtr hMetaPict) {
			throw new NotImplementedException();
		}

		public int ContextSensitiveHelp(int fEnterMode) {
			throw new NotImplementedException();
		}

		public int GetClipboardData(CHARRANGE lpchrg, int reco, IntPtr lplpdataobj) {
			throw new NotImplementedException();
		}

		public int GetDragDropEffect(bool fDrag, int grfKeyState, ref int pdwEffect) {
			throw new NotImplementedException();
		}

		public int GetContextMenu(short seltype, IntPtr lpoleobj, CHARRANGE lpchrg, out IntPtr hmenu) {
			throw new NotImplementedException();
		}
	}

}
