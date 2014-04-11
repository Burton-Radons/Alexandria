using Glare.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria {
	public abstract class Plugin : PluginFormatResource {
		readonly ArrayBackedList<Engine> EnginesMutable = new ArrayBackedList<Engine>();
		readonly ArrayBackedList<Game> GamesMutable = new ArrayBackedList<Game>();

		public override IEnumerable<ResourceFormat> AllFormats {
			get {
				foreach (ResourceFormat format in Formats)
					yield return format;
				foreach (Engine engine in Engines)
					foreach (ResourceFormat format in engine.AllFormats)
						yield return format;
				foreach (Game game in Games)
					foreach (ResourceFormat format in game.AllFormats)
						yield return format;
			}
		}

		/// <summary>
		/// Get the <see cref="System.Reflection.Assembly"/> in which this <see cref="Plugin"/> is declared.
		/// </summary>
		public Assembly DeclaringAssembly { get { return GetType().Assembly; } }

		public ReadOnlyList<Engine> Engines { get { return EnginesMutable; } }

		public ReadOnlyList<Game> Games { get { return GamesMutable; } }

		public Plugin(Manager manager, ResourceManager resourceManager)
			: base(manager, resourceManager) {
			manager.plugins.Add(this);
		}

		protected void AddEngine(Engine engine) {
			if (engine == null)
				throw new ArgumentNullException("engine");
			if (engine.Plugin != this)
				throw new ArgumentException(engine.Name + " is not part of this " + Name + ".");
			if (EnginesMutable.Contains(engine))
				throw new ArgumentException(engine.Name + " is already added to this " + Name + ".");
			EnginesMutable.Add(engine);
		}

		protected void AddGame(Game game) {
			if (game == null)
				throw new ArgumentNullException("game");
			if (GamesMutable.Contains(game))
				throw new ArgumentException(game.Name + " is already added to this " + Name + ".");
			GamesMutable.Add(game);
		}
	}

	public abstract class PluginResource : Resource {
		/// <summary>Get or set whether this <see cref="PluginResource"/> is enabled through <see cref="IsSelfEnabled"/>, or whether it's disabled by its hierarchy. For example, if an <see cref="Engine"/> is disabled, then a <see cref="Game"/> within it is also disabled.</summary>
		public virtual bool IsEnabled {
			get { return IsSelfEnabled; }
		}


		/// <summary>Get or set whether this <see cref="PluginResource"/> is specifically enabled or disabled.</summary>
		public bool IsSelfEnabled {
			get { return !DisabledPluginResources.Contains(GetType().FullName); }

			set {
				if(value != IsSelfEnabled) {
					if(value)
						DisabledPluginResources.Remove(GetType().FullName);
					else
						DisabledPluginResources.Add(GetType().FullName);
					Properties.Settings.Default.Save();
				}	
			}
		}

		static StringCollection DisabledPluginResources { get { return Properties.Settings.Default.DisabledPluginResources ?? (Properties.Settings.Default.DisabledPluginResources = new System.Collections.Specialized.StringCollection()); } }

		public Plugin Plugin { get; private set; }

		public PluginResource(Plugin plugin) : base(plugin.Manager, plugin.ResourceManager) {
			Plugin = plugin;
		}

		internal PluginResource(Manager manager, ResourceManager resourceManager)
			: base(manager, resourceManager) {
			if (!(this is Plugin))
				throw new InvalidOperationException();
			Plugin = (Plugin)this;
		}
	}

	public abstract class PluginFormatResource : PluginResource {
		readonly ArrayBackedList<ResourceFormat> FormatsMutable = new ArrayBackedList<ResourceFormat>();

		public virtual IEnumerable<ResourceFormat> AllFormats {
			get {
				foreach (ResourceFormat format in FormatsMutable)
					yield return format;
			}
		}

		/// <summary>Get the resource formats.</summary>
		public ReadOnlyList<ResourceFormat> Formats { get { return FormatsMutable; } }

		public PluginFormatResource(Plugin plugin) : base(plugin) { }

		internal PluginFormatResource(Manager manager, ResourceManager resourceManager) : base(manager, resourceManager) { }

		protected void AddFormat(ResourceFormat format) {
			if (format == null)
				throw new ArgumentNullException("format");
			if (format.Plugin != Plugin || FormatsMutable.Contains(format))
				throw new ArgumentException(format.Name + " cannot be added to " + Name + ".");
			FormatsMutable.Add(format);
		}
	}
}
