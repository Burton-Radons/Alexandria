using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moki.Floop {
	/// <summary>
	/// A data source.
	/// </summary>
	public abstract class Source {
		/// <summary>The offset of the first position in the source.</summary>
		public virtual long Offset { get { return 0; } }

		/// <summary>Read a byte from the <see cref="Source"/>.</summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public abstract byte this[long offset] { get; }

		public abstract int Read(long offset, byte[] data, int dataOffset, int byteCount);
	}
}
