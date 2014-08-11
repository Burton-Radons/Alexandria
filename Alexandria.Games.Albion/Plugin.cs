using Glare.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Games.Albion {
	/// <summary>The plugin for Albion.</summary>
	public class Plugin : Alexandria.AlexandriaPlugin {
		internal Plugin(AssetManager manager) : base(manager, Properties.Resources.ResourceManager) {
			AddGame(new Game(this));
		}
	}
}
