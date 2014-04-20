using Glare.Assets;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria {
	public abstract class Game : PluginFormatAsset {
		readonly Engine engine;

		public Engine Engine { get { return engine; } }

		Game(AlexandriaPlugin plugin, Engine engine)
			: base(plugin) {
			this.engine = engine;
		}

		public Game(AlexandriaPlugin plugin) : this(plugin, null) { }
		public Game(Engine engine) : this((AlexandriaPlugin)engine.Plugin, engine) { }

		public virtual void Detect(GameInstanceList instances) {
		}
	}
}
