using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moki.Compiler {
	/// <summary>
	/// Source code.
	/// </summary>
	public class Source {
		public int Length { get { return Code.Length; } }

		public string Name { get; private set; }

		public string Code { get; private set; }

		public Source(string name, string code) {
			if (name == null)
				throw new ArgumentNullException("name");
			this.Name = name;
			this.Code = code;
		}
	}
}
