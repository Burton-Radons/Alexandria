using Moki.Compiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moki.Syntax {
	/// <summary>
	/// A description of a type.
	/// </summary>
	public abstract class TypeDeclaration : Node {
		public TypeDeclaration(Marker marker)
			: base(marker) {
		}
	}
}
