using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.Creation {
	[GuidAttribute("05579F80-C245-490D-8C54-355BC7BD17E5")]
	public class Engine  : Alexandria.Engine {
		internal Engine(Plugin plugin) : base(plugin) {
			AddGame(new SkyrimGame(this));
			AddFormat(new ModuleFormat(this));
		}
	}
}
