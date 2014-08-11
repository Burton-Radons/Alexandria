using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.GoldBox.Resources {
	/// <summary>Comparison operation in a script.</summary>
	public enum ScriptComparison {
		/// <summary>True if equal.</summary>
		Equal = 0x16,

		/// <summary>True if not equal.</summary>
		NotEqual = 0x17,

		/// <summary>True if less than or less than or equal to - the actual test is not known.</summary>
		LessThanOrLessThanOrEqualTo = 0x18,
	}
}
