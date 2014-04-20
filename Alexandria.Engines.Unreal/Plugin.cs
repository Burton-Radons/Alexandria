using Glare.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Engines.Unreal {
	public class Plugin : AlexandriaPlugin {
		public Plugin(AssetManager manager)
			: base(manager, Properties.Resources.ResourceManager) {
			AddEngine(new Engine(this));
		}
	}
}
