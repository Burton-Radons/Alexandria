using Moki.Compiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moki.Syntax {
	/// <summary>
	/// A <see cref="Statement"/> that declares a new variable or type.
	/// </summary>
	public abstract class Declaration : Statement {
		public Declaration(Marker marker)
			: base(marker) {
		}
	}
}
