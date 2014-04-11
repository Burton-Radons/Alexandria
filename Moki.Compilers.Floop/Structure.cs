using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moki.Floop {
	/// <summary>The base class of all structured data classes containing <see cref="StructureProperty&lt;T&gt;"/> properties.</summary>
	public class Structure {
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly Source source;

		public Source Source { get { return source; } }
	}
}
