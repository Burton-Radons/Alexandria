using Glare.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Platforms.SuperFamicom {
	/// <summary>Plugin for the Super Famicom system (Super Nintendo Entertainment System).</summary>
	public class Plugin : AlexandriaPlugin {
		internal static ResourceManager OurResourceManager { get { return Properties.Resources.ResourceManager; } }

		internal Plugin(AssetManager manager)
			: base(manager, OurResourceManager) {
			AddEngine(new Platform(this));
		}
	}
}
