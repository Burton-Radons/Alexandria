using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Engines.Unreal {
	public class Plugin : Alexandria.Plugin {
		public Plugin(Manager manager)
			: base(manager, Properties.Resources.ResourceManager) {
			new Engine(this);
		}
	}
}
