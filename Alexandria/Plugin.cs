using Glare.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria {
	public abstract class Plugin : Resource {
		internal readonly ArrayBackedList<Engine> EnginesMutable = new ArrayBackedList<Engine>();
		internal readonly ArrayBackedList<Game> GamesMutable = new ArrayBackedList<Game>();
		internal readonly ArrayBackedList<Loader> LoadersMutable = new ArrayBackedList<Loader>();

		public IEnumerable<Loader> AllLoaders {
			get {
				foreach (Loader loader in Loaders)
					yield return loader;
				foreach (Engine engine in Engines)
					foreach (Loader loader in engine.Loaders)
						yield return loader;
				foreach (Game program in Games)
					foreach (Loader loader in program.Loaders)
						yield return loader;
			}
		}

		/// <summary>
		/// Get the <see cref="System.Reflection.Assembly"/> in which this <see cref="Plugin"/> is declared.
		/// </summary>
		public Assembly DeclaringAssembly { get { return GetType().Assembly; } }

		public ReadOnlyList<Engine> Engines { get { return EnginesMutable; } }

		public ReadOnlyList<Game> Games { get { return GamesMutable; } }

		/// <summary>General file loaders.</summary>
		public ReadOnlyList<Loader> Loaders { get { return LoadersMutable; } }

		public Plugin(Manager manager, ResourceManager resourceManager)
			: base(manager, resourceManager) {
			manager.plugins.Add(this);
		}
	}

	public class PluginsAttribute : Attribute {
		readonly ArrayBackedList<Type> types;

		public ReadOnlyList<Type> Types { get { return types; } }

		public PluginsAttribute(params Type[] types) {
			this.types = new ArrayBackedList<Type>(types);
		}
	}
}
