using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Games.Arcanum {
	/// <summary>Game class for Arcanum: Of Steamworks and Magick Obscura</summary>
	public class Game : Alexandria.Game {
		internal Game(Plugin plugin)
			: base(plugin, null) {
			AddFormat(new ArchiveFormat(this));
		}
	}
}
