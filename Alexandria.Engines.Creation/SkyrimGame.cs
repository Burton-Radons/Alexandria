using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.Creation {
	[GuidAttribute("E6F44D3E-79A2-4461-A5AF-259F212E0367")]
	public class SkyrimGame : Game {
		internal SkyrimGame(Engine engine)
			: base(engine, null) {
		}
	}
}
