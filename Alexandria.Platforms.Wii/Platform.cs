using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Platforms.Wii {
	/// <summary>Platform for the Nintendo Wii system.</summary>
	public class Platform : Alexandria.Platform {
		internal Platform(Plugin plugin)
			: base(plugin) {
			AddFormat(new NintendoOpticalDiscFormat(this));
			AddFormat(new PackageFormat(this));
		}
	}
}
