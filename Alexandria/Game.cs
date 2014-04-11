using Glare.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria {
	public abstract class Game : PluginFormatResource {
		readonly Engine engine;

		public Engine Engine { get { return engine; } }

		Game(Plugin plugin, Engine engine)
			: base(plugin) {
			this.engine = engine;
		}

		public Game(Plugin plugin) : this(plugin, null) { }
		public Game(Engine engine) : this(engine.Plugin, engine) { }

		public virtual void Detect(GameInstanceList instances) {
		}
	}
}
