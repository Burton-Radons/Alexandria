using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Games.Albion {
	class Game : Alexandria.Game {
		public Game(Plugin plugin)
			: base(plugin) {
			AddFormat(new LibraryFormat(this));
			AddFormat(new PaletteFormat(this));
		}
	}
}
