using Moki.Compiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moki.Syntax {
	public abstract class Expression : Node {
		public Expression(Marker marker) : base(marker) {
		}
	}
}
