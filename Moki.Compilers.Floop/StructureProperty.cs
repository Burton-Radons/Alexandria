using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moki.Floop {
	/// <summary>A value container derived from a <see cref="Source"/>.</summary>
	/// <typeparam name="T"></typeparam>
	public struct StructureProperty<T> {
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly long offset;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly Structure structure;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		T value;

		/// <summary>Get the offset of the <see cref="StructureProperty&lt;T&gt;"/> in the <see cref="Source"/>.</summary>
		public long Offset { get { return offset; } }

		/// <summary>Get the <see cref="Source"/> of the <see cref="Structure"/>.</summary>
		public Source Source { get { return structure.Source; } }

		/// <summary>Get the <see cref="Moki.Floop.Structure"/> this is in.</summary>
		public Structure Structure { get { return structure; } }

		/// <summary>Get the value of the field.</summary>
		public T Value {
			get { return value; }
		}
	}
}
