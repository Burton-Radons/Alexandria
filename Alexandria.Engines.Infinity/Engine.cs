using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.Infinity {
	public class Engine : Alexandria.Engine {
		internal Engine(Plugin plugin)
			: base(plugin) {
			AddFormat(new BiffArchiveFormat(this));
		}
	}
}
