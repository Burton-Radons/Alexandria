using Glare.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Platforms.Wii {
	/// <summary>Plugin for the Nintendo Wii system.</summary>
	public class Plugin : AlexandriaPlugin {
		internal static ResourceManager OurResourceManager { get { return Properties.Resources.ResourceManager; } }

		internal Plugin(AssetManager manager)
			: base(manager, OurResourceManager) {
			AddEngine(new Platform(this));
		}
	}
}
