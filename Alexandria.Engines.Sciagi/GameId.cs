using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.Sciagi {
	/// <summary>
	/// A game identification for game-specific hacks.
	/// </summary>
	public enum GameId {
		/// <summary>
		/// Unknown or fan-made game.
		/// </summary>
		Unknown,

		/// <summary>Quest for Glory 1</summary>
		QuestForGlory1,

		/// <summary>Quest for Glory 2</summary>
		QuestForGlory2,

		/// <summary>Quest for Glory 3</summary>
		QuestForGlory3,
	}
}
