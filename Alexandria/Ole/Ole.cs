using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using FILETIME = System.Runtime.InteropServices.ComTypes.FILETIME;
using STATSTG = System.Runtime.InteropServices.ComTypes.STATSTG;

namespace Alexandria.Ole {

	[StructLayout(LayoutKind.Sequential)]
	public class CHARRANGE {
		public int cpMin;
		public int cpMax;
	}

	[ComImport(), Guid("0000000B-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IStorage {

		[return: MarshalAs(UnmanagedType.Interface)]
		IStream CreateStream(
		      [MarshalAs(UnmanagedType.BStr)] string pwcsName,
		      [MarshalAs(UnmanagedType.U4)] int grfMode,
		      [MarshalAs(UnmanagedType.U4)] int reserved1,
		      [MarshalAs(UnmanagedType.U4)] int reserved2);

		[return: MarshalAs(UnmanagedType.Interface)]
		IStream OpenStream(
		      [MarshalAs(UnmanagedType.BStr)] string pwcsName,

		       IntPtr reserved1,
		      [MarshalAs(UnmanagedType.U4)] int grfMode,
		      [MarshalAs(UnmanagedType.U4)] int reserved2);

		[return: MarshalAs(UnmanagedType.Interface)]
		IStorage CreateStorage(
		      [MarshalAs(UnmanagedType.BStr)] string pwcsName,
		      [MarshalAs(UnmanagedType.U4)] int grfMode,
		      [MarshalAs(UnmanagedType.U4)] int reserved1,
		      [MarshalAs(UnmanagedType.U4)] int reserved2);

		[return: MarshalAs(UnmanagedType.Interface)]
		IStorage OpenStorage(
		      [MarshalAs(UnmanagedType.BStr)] string pwcsName,

		       IntPtr pstgPriority,   // must be null
		      [MarshalAs(UnmanagedType.U4)] int grfMode,

		       IntPtr snbExclude,
		      [MarshalAs(UnmanagedType.U4)] int reserved);

		void CopyTo(
			int ciidExclude,
		       [MarshalAs(UnmanagedType.LPArray)] Guid[] pIIDExclude,

			IntPtr snbExclude,
		       [MarshalAs(UnmanagedType.Interface)] IStorage stgDest);

		void MoveElementTo(
		       [MarshalAs(UnmanagedType.BStr)] string pwcsName,
		       [MarshalAs(UnmanagedType.Interface)] IStorage stgDest,
		       [MarshalAs(UnmanagedType.BStr)] string pwcsNewName,
		       [MarshalAs(UnmanagedType.U4)] int grfFlags);

		void Commit(
			int grfCommitFlags);

		void Revert();

		void EnumElements(
		       [MarshalAs(UnmanagedType.U4)] int reserved1,
			// void *
			IntPtr reserved2,
		       [MarshalAs(UnmanagedType.U4)] int reserved3,
		       [Out, MarshalAs(UnmanagedType.Interface)]out object ppVal);                     // IEnumSTATSTG


		void DestroyElement(
		       [MarshalAs(UnmanagedType.BStr)] string pwcsName);


		void RenameElement(
		       [MarshalAs(UnmanagedType.BStr)] string pwcsOldName,
		       [MarshalAs(UnmanagedType.BStr)] string pwcsNewName);


		void SetElementTimes(
		       [MarshalAs(UnmanagedType.BStr)] string pwcsName,
		       FILETIME pctime,
		       FILETIME patime,
		       FILETIME pmtime);


		void SetClass(ref Guid clsid);
		void SetStateBits(int grfStateBits, int grfMask);

		void Stat(
		       [Out] STATSTG pStatStg,
			int grfStatFlag);
	}

	[ComImport(), Guid("00020D03-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IRichEditOleCallback {
		[PreserveSig]
		int GetNewStorage(out IStorage ret);

		[PreserveSig]
		int GetInPlaceContext(IntPtr lplpFrame, IntPtr lplpDoc, IntPtr lpFrameInfo);

		[PreserveSig]
		int ShowContainerUI(int fShow);

		[PreserveSig]
		int QueryInsertObject(ref Guid lpclsid, IntPtr lpstg, int cp);

		[PreserveSig]
		int DeleteObject(IntPtr lpoleobj);

		[PreserveSig]
		int QueryAcceptData(IDataObject lpdataobj, /* CLIPFORMAT* */ IntPtr lpcfFormat, int reco, int fReally, IntPtr hMetaPict);

		[PreserveSig]
		int ContextSensitiveHelp(int fEnterMode);

		[PreserveSig]
		int GetClipboardData(CHARRANGE lpchrg, int reco, IntPtr lplpdataobj);

		[PreserveSig]
		int GetDragDropEffect(bool fDrag, int grfKeyState, ref int pdwEffect);

		[PreserveSig]
		int GetContextMenu(short seltype, IntPtr lpoleobj, CHARRANGE lpchrg, out IntPtr hmenu);
	}

	public static class OleAutomation {
		const string DllName = "OleAut32";

		[DllImport(DllName, EntryPoint = "OleLoadPicture", CallingConvention = CallingConvention.Winapi)]
		public static extern int LoadPicture(IStream stream, int size, bool runmode, ref Guid riid, out IntPtr Obj);
			/*RESULT OleLoadPicture(
  _In_   LPSTREAM lpstream,
  _In_   LONG lSize,
  _In_   BOOL fRunmode,
  _In_   REFIID riid,
  _Out_  LPVOID *lplpvObj
);*/
	}

	public class IStreamWrapper : System.IO.Stream, IStream {
		readonly System.IO.Stream Stream;

		public IStreamWrapper(System.IO.Stream stream) {
			if (stream == null)
				throw new ArgumentNullException("stream");
			Stream = stream;
		}

		public void Clone(out IStream ppstm) {
			throw new NotImplementedException();
		}

		public void Commit(int grfCommitFlags) {
			throw new NotImplementedException();
		}

		public void CopyTo(IStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten) {
			throw new NotImplementedException();
		}

		public void LockRegion(long libOffset, long cb, int dwLockType) {
			throw new NotImplementedException();
		}

		public void Read(byte[] pv, int cb, IntPtr pcbRead) {
			int count = Stream.Read(pv, 0, cb);
			if (pcbRead != IntPtr.Zero)
				Marshal.WriteInt32(pcbRead, count);
		}

		public void Revert() {
		}

		public void Seek(long dlibMove, int dwOrigin, IntPtr plibNewPosition) {
			long result = Stream.Seek(dlibMove, (SeekOrigin)dwOrigin);
			if (plibNewPosition != IntPtr.Zero)
				Marshal.WriteInt64(plibNewPosition, result);
		}

		public void SetSize(long libNewSize) { Stream.SetLength(libNewSize); }

		public void Stat(out STATSTG pstatstg, int grfStatFlag) {
			throw new NotImplementedException();
		}

		public void UnlockRegion(long libOffset, long cb, int dwLockType) { }

		public void Write(byte[] pv, int cb, IntPtr pcbWritten) {
			int result = 0;

			try {
				Stream.Write(pv, 0, cb);
				result = cb;
			} catch (Exception) {
				return;
			}

			if(pcbWritten != IntPtr.Zero)
				Marshal.WriteInt32(pcbWritten, result);
		}

		public override bool CanRead { get { return Stream.CanRead; } }

		public override bool CanSeek { get { return Stream.CanSeek; } }

		public override bool CanWrite { get { return Stream.CanWrite; } }

		public override void Flush() { Stream.Flush(); }

		public override long Length { get { return Stream.Length; } }

		public override long Position { get { return Stream.Position; } set { Stream.Position = value; } }

		public override int Read(byte[] buffer, int offset, int count) { return Stream.Read(buffer, offset, count); }

		public override long Seek(long offset, System.IO.SeekOrigin origin) { return Stream.Seek(offset, origin); }

		public override void SetLength(long value) { Stream.SetLength(value); }

		public override void Write(byte[] buffer, int offset, int count) { Stream.Write(buffer, offset, count); }
	}
}
