using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria {
	public class GameInstance {
		readonly Game game;
		readonly string path;

		public GameInstance(Game game, string path) {
			if (game == null)
				throw new ArgumentNullException("game");
			if (path == null)
				throw new ArgumentNullException("path");

			this.game = game;
			this.path = path;
		}
	}

	public class GameInstanceList : ObservableCollection<GameInstance> {
	}
}
