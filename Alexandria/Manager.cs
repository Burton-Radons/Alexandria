using Alexandria.Resources;
using Glare;
using Glare.Graphics;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria {
	/// <summary>
	/// Global state manager.
	/// </summary>
	public class Manager {
		internal readonly ArrayBackedList<Plugin> plugins = new ArrayBackedList<Plugin>();

		public IEnumerable<Loader> AllLoaders {
			get {
				foreach (Plugin plugin in plugins) {
					foreach (Loader loader in plugin.AllLoaders)
						yield return loader;
				}
			}
		}

		public ReadOnlyList<Plugin> Plugins { get { return plugins; } }

		public Manager() {
		}

		public Resource LoadFile(string path) {
			Stream stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
			return Load(stream, path, Loader.SystemFileOpener, null);
		}

		public Resource Load(Stream stream, string name, LoaderFileOpener opener, Resource context) {
			return Load(new BinaryReader(stream), name, opener, context);
		}

		public Resource Load(BinaryReader reader, string name, LoaderFileOpener opener, Resource context) {
			Loader best = null;
			LoaderMatchLevel bestLevel = LoaderMatchLevel.None;

			long reset = reader.BaseStream.Position;
			foreach (Loader loader in AllLoaders) {
				LoaderMatchLevel matchLevel = loader.Match(reader, name, opener, context);
				reader.BaseStream.Position = reset;
				if (matchLevel > bestLevel) {
					bestLevel = matchLevel;
					best = loader;
				}
			}

			if (best == null) {
				byte[] data = reader.ReadBytes(checked((int)(reader.BaseStream.Length - reset)));
				return new Binary(this, name, data);
			}

			return best.Load(reader, name, opener, context);
		}

		public void LoadPlugins() {
			LoadPlugins(Directory.GetCurrentDirectory());
		}

		public void LoadPlugins(string searchPath) {
			foreach (string path in Directory.EnumerateFiles(searchPath, "Alexandria*.dll", SearchOption.AllDirectories)) {
				Assembly assembly = Assembly.LoadFile(path);

				foreach (Type pluginType in assembly.GetExportedTypes()) {
					if (pluginType.IsAbstract || !pluginType.IsSubclassOf(typeof(Plugin)))
						continue;
					ConstructorInfo constructor = pluginType.GetConstructor(new Type[] { typeof(Manager) });
					if (constructor == null)
						continue;
					Plugin plugin = (Plugin)constructor.Invoke(new object[] { this });
				}
			}
		}
	}
}
