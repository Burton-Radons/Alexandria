using Alexandria.Engines.GoldBox.Resources;
using Glare.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.GoldBox {
	public class Plugin : AlexandriaPlugin {
		internal static ResourceManager OurResourceManager { get { return Properties.Resources.ResourceManager; } }

		public Plugin(AssetManager manager)
			: base(manager, OurResourceManager) {
			AddEngine(new Engine(this));
		}
	}
}
