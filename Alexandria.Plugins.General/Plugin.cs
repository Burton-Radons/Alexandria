using Glare;
using Glare.Assets;
using Glare.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Plugins.General {
	public class Plugin : Alexandria.AlexandriaPlugin {
		public Plugin(AssetManager manager)
			: base(manager, Properties.Resources.ResourceManager) {
			AddFormat(new LuaFormat(this));
		}
	}
}
