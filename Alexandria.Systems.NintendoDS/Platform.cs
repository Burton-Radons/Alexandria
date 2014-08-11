using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Platforms.NintendoDS {
	/// <summary>Platform for the Nintendo DS.</summary>
	public class Platform : Alexandria.Platform {
		internal Platform(Plugin plugin)
			: base(plugin) {
			AddFormat(new RomFormat(this));
		}
	}
}
