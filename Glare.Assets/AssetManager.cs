using Glare.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Assets {

	/// <summary>
	/// Global state manager.
	/// </summary>
	public abstract class AssetManager {
		internal readonly Codex<AssetPlugin> plugins = new Codex<AssetPlugin>();

		public IEnumerable<AssetFormat> AllFormats {
			get {
				foreach (AssetPlugin plugin in plugins) {
					foreach (AssetFormat format in plugin.AllFormats)
						yield return format;
				}
			}
		}

		public Codex<AssetPlugin> Plugins { get { return plugins; } }

		public AssetManager() {
			new DefaultPlugin(this);
		}

		public AssetFormat GetFormat(Type type) {
			foreach (AssetFormat format in AllFormats)
				if (format.GetType() == type)
					return format;
			return null;
		}

		public TFormat GetFormat<TFormat>() where TFormat : AssetFormat {
			return (TFormat)GetFormat(typeof(TFormat));
		}

		internal protected abstract bool GetIsPluginAssetEnabled(PluginAsset resource);

		internal protected abstract void SetIsPluginAssetEnabled(PluginAsset resource, bool value);

		bool HasPluginType(Type pluginType) { foreach (AssetPlugin plugin in plugins) if (plugin.GetType() == pluginType) return true; return false; }

		public Asset LoadFile(string path) {
			Stream stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
			return Load(stream, path, FileManager.System, null);
		}

		public Asset Load(Stream stream, string name, FileManager manager, Asset resourceContext) {
			return Load(new BinaryReader(stream), name, manager, resourceContext);
		}

		public Asset Load(BinaryReader reader, string name, FileManager manager, Asset resourceContext) {
			return AssetFormat.LoadResource(new AssetLoader(reader, name, manager, resourceContext), AllFormats, null);
		}

		public void LoadPlugins(string searchPath, string searchPattern = "*.dll") {
			foreach (string path in Directory.EnumerateFiles(searchPath, searchPattern, SearchOption.AllDirectories))
				LoadPlugins(Assembly.LoadFile(path));
		}

		public void LoadPlugins(Assembly assembly) {
			foreach (Type pluginType in assembly.GetExportedTypes())
				LoadPlugin(pluginType);
		}

		public void LoadPlugin(Type pluginType) {
			if (pluginType.IsAbstract || !pluginType.IsSubclassOf(typeof(AssetPlugin)) || HasPluginType(pluginType))
				return;
			ConstructorInfo constructor = pluginType.GetConstructor(new Type[] { typeof(AssetManager) });
			if (constructor == null)
				return;
			AssetPlugin plugin = (AssetPlugin)constructor.Invoke(new object[] { this });
		}
	}
}
