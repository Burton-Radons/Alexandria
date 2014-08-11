using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.Sciagi {
	/// <summary>
	/// Specifies the version of the engine.
	/// </summary>
	public enum EngineVersion {
		/// <summary>
		/// No/invalid value.
		/// </summary>
		None,

		/// <summary>Early SCI0.</summary>
		SCI0Early,

		/// <summary>SCI0.</summary>
		SCI0,
	}
}
