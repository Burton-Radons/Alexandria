using Glare.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria
{
	public abstract class Engine : Resource
	{
		internal readonly ArrayBackedList<Game> GamesMutable = new ArrayBackedList<Game>();
		internal readonly ArrayBackedList<Loader> loaders = new ArrayBackedList<Loader>();
		readonly Plugin plugin;

		public ReadOnlyList<Game> Games { get { return GamesMutable; } }

		public ReadOnlyList<Loader> Loaders { get { return loaders; } }

		public Plugin Plugin { get { return plugin; } }

		public Engine(Plugin plugin, ResourceManager resourceManager)
			: base(plugin != null ? plugin.Manager : null, resourceManager)
		{
			if (plugin == null)
				throw new ArgumentNullException("plugin");
			this.plugin = plugin;
			plugin.EnginesMutable.Add(this);
		}

		public virtual void Detect(GameInstanceList instances) {
		}
	}
}
