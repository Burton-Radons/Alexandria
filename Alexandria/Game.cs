using Glare.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria {
	public abstract class Game : Resource {
		readonly Engine engine;
		internal readonly ArrayBackedList<Loader> loaders = new ArrayBackedList<Loader>();

		public Engine Engine { get { return engine; } }

		public ReadOnlyList<Loader> Loaders { get { return loaders; } }

		public Game(Plugin plugin, string name, string description = null, Engine engine = null)
			: base(plugin.Manager, name, description) {
			if (plugin == null)
				throw new ArgumentNullException("plugin");
			plugin.GamesMutable.Add(this);

			this.engine = engine;
			if (engine != null)
				engine.GamesMutable.Add(this);
		}

		public virtual void Detect(GameInstanceList instances) { 
		}
	}
}
