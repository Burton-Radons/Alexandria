using Alexandria.Resources;
using Glare;
using Glare.Graphics;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alexandria {
	/// <summary>
	/// Global state manager.
	/// </summary>
	public class Manager {
		internal readonly ArrayBackedList<Plugin> plugins = new ArrayBackedList<Plugin>();

		public IEnumerable<ResourceFormat> AllFormats {
			get {
				foreach (Plugin plugin in plugins) {
					foreach (ResourceFormat format in plugin.AllFormats)
						yield return format;
				}
			}
		}

		public ReadOnlyList<Plugin> Plugins { get { return plugins; } }

		public Manager() {
		}

		public Resource LoadFile(string path) {
			Stream stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
			return Load(stream, path, LoadInfo.SystemFileOpener, null);
		}

		public Resource Load(Stream stream, string name, LoaderFileOpener opener, Resource context) {
			return Load(new BinaryReader(stream), name, opener, context);
		}

		public Resource Load(BinaryReader reader, string name, LoaderFileOpener opener, Resource resourceContext) {
			return ResourceFormat.LoadResource(new LoadInfo(reader, name, opener, resourceContext), AllFormats, null);
			/*ResourceFormat best = null;
			LoadMatchStrength bestLevel = LoadMatchStrength.None;
			LoadInfo context = new LoadInfo(reader, name, opener, resourceContext);

			long reset = reader.BaseStream.Position;
			foreach (ResourceFormat format in AllFormats) {
				LoadMatchStrength matchLevel = format.LoadMatch(context);
				reader.BaseStream.Position = reset;
				if (matchLevel > bestLevel) {
					bestLevel = matchLevel;
					best = format;
				}
			}

			if (best == null) {
				byte[] data = reader.ReadBytes(checked((int)(reader.BaseStream.Length - reset)));
				return new Binary(this, name, data);
			}

			return best.Load(context);*/
		}

		public void LoadPlugins() {
			LoadPlugins(Application.StartupPath, "Alexandria*.dll");
		}

		public void LoadPlugins(string searchPath, string searchPattern = "*.dll") {
			foreach (string path in Directory.EnumerateFiles(searchPath, searchPattern, SearchOption.AllDirectories)) {
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
