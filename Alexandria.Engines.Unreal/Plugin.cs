using Glare.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Engines.Unreal {
	/// <summary>
	/// Plugin for the Unreal engine.
	/// </summary>
	public class Plugin : AlexandriaPlugin {
		internal Plugin(AssetManager manager)
			: base(manager, Properties.Resources.ResourceManager) {
			AddEngine(new Engine(this));
		}
	}
}
