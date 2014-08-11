using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Engines.DarkSouls {
	/// <summary>Version of a <see cref="Model"/>.</summary>
	public enum ModelVersion {
		/// <summary>2.12, used for Dark Souls 1.</summary>
		DarkSouls = 0x2000C,

		/// <summary>2.16, used for Dark Souls 2.</summary>
		DarkSouls2 = 0x20010,
	}
}
