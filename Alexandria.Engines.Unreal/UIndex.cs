using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Alexandria.Engines.Unreal {
	/// <summary>
	/// A value stored as a compressed integer.
	/// </summary>
	public struct UIndex {
		/// <summary></summary>
		public UIndex(int value) {
			Value = value;
		}

		/// <summary></summary>
		public static UIndex Read(BinaryReader reader) {
			int initial = reader.ReadByte(), value = initial & 63, code = 0, shift = 6;

			if ((initial & 64) != 0) {
				do {
					value |= ((code = reader.ReadByte()) & 127) << shift;
					shift += 7;
				} while ((code & 128) != 0);
			}

			return (initial & 128) != 0 ? -value : value;
		}

		/// <summary></summary>
		public int Write(byte[] array, int index) {
			uint uvalue = checked((uint)Value);

			byte initial = (byte)((uvalue & 63u) | (uvalue > 63 ? 64u : 0u) | (Value < 0 ? 128u : 0u));
			array[index++] = initial;

			for (uvalue >>= 6; uvalue > 0; uvalue >>= 7) {
				byte code = (byte)((uvalue & 127u) | (uvalue > 127u ? 128u : 0u));
				array[index++] = code;
			}
			return index;
		}

		[ThreadStatic]
		static byte[] pBuffer;

		static byte[] Buffer { get { return pBuffer ?? (pBuffer = new byte[8]); } }

		/// <summary></summary>
		public void Write(Stream stream) {
			var buffer = Buffer;
			stream.Write(buffer, 0, Write(buffer, 0));
		}

		/// <summary>
		/// Get the number of bytes this value will take to store.
		/// </summary>
		public int StorageSizeBytes {
			get {
				var value = Math.Abs(Value);
				if (value < 128)
					return 1;
				throw new Exception();
			}
		}

		/// <summary></summary>
		public int Value;

		/// <summary></summary>
		public override string ToString() {
			string text = string.Format("UIndex {0} (", Value);
			int count = Write(Buffer, 0);
			for (int index = 0; index < count; index++) {
				if (index > 0) text += " ";
				text += string.Format("{0:X2}", Buffer[index]);
			}
			return text + ")";
		}

		/// <summary></summary>
		public static implicit operator int(UIndex self) { return self.Value; }

		/// <summary></summary>
		public static implicit operator UIndex(int self) { return new UIndex(self); }
	}
}
