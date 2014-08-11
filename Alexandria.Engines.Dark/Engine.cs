using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.Dark {
	/// <summary>Engine for the Dark Engine.</summary>
	public class Engine : Alexandria.Engine {
		internal Engine(Plugin plugin)
			: base(plugin) {
			AddGame(new Thief1Game(this));
			AddGame(new Thief2Game(this));
			AddGame(new SystemShock2Game(this));
		}
	}
}
