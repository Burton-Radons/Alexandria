using Glare.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.DarkSouls {
	/// <summary>
	/// The plugin for Dark Souls.
	/// </summary>
	public class Plugin : AlexandriaPlugin {
		/// <summary>
		/// Initialise the object.
		/// </summary>
		/// <param name="manager"></param>
		public Plugin(AssetManager manager)
			: base(manager, Properties.Resources.ResourceManager) {
			AddEngine(new Engine(this));
		}
	}
}
