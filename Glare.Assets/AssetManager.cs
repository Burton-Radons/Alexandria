using Glare.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Assets {
	/// <summary>
	/// Global state manager.
	/// </summary>
	public abstract class AssetManager : NotifyingObject {
		bool DebuggingEnabledField;

		internal readonly Codex<AssetPlugin> plugins = new Codex<AssetPlugin>();

		static readonly PropertyInfo DebuggingEnabledProperty = GetProperty<AssetManager>("DebuggingEnabled");

		/// <summary>Get an enumerator over all enabled <see cref="AssetFormat"/>s.</summary>
		public IEnumerable<AssetFormat> AllEnabledFormats {
			get {
				foreach (AssetPlugin plugin in plugins) {
					foreach (AssetFormat format in plugin.AllEnabledFormats)
						if (format.IsEnabled)
							yield return format;
				}
			}
		}

		/// <summary>Get or set whether debugging is enabled. Exceptions should be passed on instead of caught. The default is based on whether a debugger is attached; if there is, then it is <c>true</c>. Otherwise the default is <c>false</c>.</summary>
		public virtual bool DebuggingEnabled {
			get { return DebuggingEnabledField; }
			set { SetProperty(DebuggingEnabledProperty, ref DebuggingEnabledField, ref value); }
		}

		/// <summary>Get the collection of <see cref="AssetPlugin"/>s.</summary>
		public Codex<AssetPlugin> Plugins { get { return plugins; } }

		/// <summary>
		/// Initialise the asset manager.
		/// </summary>
		public AssetManager() {
			new DefaultPlugin(this);
			DebuggingEnabled = Debugger.IsAttached;
		}

		/// <summary>Find the asset format with the given type amongst all enabled asset formats.</summary>
		/// <param name="type">The type of the <see cref="AssetFormat"/> to match.</param>
		/// <returns></returns>
		public AssetFormat GetEnabledFormat(Type type) {
			foreach (AssetFormat format in AllEnabledFormats)
				if (format.GetType() == type)
					return format;
			return null;
		}

		/// <summary>Find the <see cref="AssetFormat"/> with the given <see cref="Type"/> amongst all <see cref="AssetFormat"/>s.</summary>
		/// <typeparam name="TFormat"></typeparam>
		/// <returns></returns>
		public TFormat GetEnabledFormat<TFormat>() where TFormat : AssetFormat {
			return (TFormat)GetEnabledFormat(typeof(TFormat));
		}

		/// <summary>Retrieve whether a plugin asset is enabled.</summary>
		/// <param name="resource"></param>
		/// <returns></returns>
		internal protected abstract bool GetIsPluginAssetEnabled(PluginAsset resource);

		/// <summary>Set whether a plugin asset is enabled.</summary>
		/// <param name="resource"></param>
		/// <param name="value"></param>
		internal protected abstract void SetIsPluginAssetEnabled(PluginAsset resource, bool value);

		bool HasPluginType(Type pluginType) { foreach (AssetPlugin plugin in plugins) if (plugin.GetType() == pluginType) return true; return false; }

		/// <summary>Load an <see cref="Asset"/> from a file.</summary>
		/// <param name="path"></param>
		/// <param name="fileManager">The <see cref="FileManager"/> to use. If <c>null</c> (the default), the system file manager is used.</param>
		/// <param name="context"></param>
		/// <param name="byteOrder"></param>
		/// <returns></returns>
		public Asset LoadFile(string path, FileManager fileManager = null, Asset context = null, ByteOrder byteOrder = ByteOrder.LittleEndian) {
			if (fileManager == null)
				fileManager = FileManager.System;
			Stream stream = fileManager.OpenRead(path);
			return Load(stream, path, fileManager, context, byteOrder);
		}

		/// <summary>Load an <see cref="Asset"/> from a file asynchronously.</summary>
		/// <param name="path"></param>
		/// <param name="fileManager">The <see cref="FileManager"/> to use. If <c>null</c> (the default), the system file manager is used.</param>
		/// <param name="context"></param>
		/// <param name="byteOrder"></param>
		/// <param name="conflictResolver"></param>
		/// <param name="progressMonitor"></param>
		/// <param name="progressUpdateRate"></param>
		/// <returns></returns>
		public Task<Asset> LoadFileAsync(string path, FileManager fileManager = null, Asset context = null, ByteOrder byteOrder = ByteOrder.LittleEndian, ResolveLoadConflictCallback conflictResolver = null, AssetLoaderProgressCallback progressMonitor = null, TimeSpan? progressUpdateRate = null) {
			if (fileManager == null)
				fileManager = FileManager.System;
			Stream stream = fileManager.OpenRead(path);
			return LoadAsync(stream, path, fileManager, context, byteOrder, conflictResolver, progressMonitor, progressUpdateRate);
		}

		/// <summary>Load an <see cref="Asset"/>.</summary>
		/// <param name="stream"></param>
		/// <param name="name"></param>
		/// <param name="fileManager">The <see cref="FileManager"/> to use. If <c>null</c> (the default), the system file manager is used.</param>
		/// <param name="context"></param>
		/// <param name="byteOrder"></param>
		/// <returns></returns>
		public Asset Load(Stream stream, string name, FileManager fileManager = null, Asset context = null, ByteOrder byteOrder = ByteOrder.LittleEndian) {
			return Load(BigEndianBinaryReader.Create(byteOrder, stream), name, fileManager, context);
		}

		/// <summary>Load an <see cref="Asset"/> asynchronously.</summary>
		/// <param name="stream"></param>
		/// <param name="name"></param>
		/// <param name="fileManager">The <see cref="FileManager"/> to use. If <c>null</c> (the default), the system file manager is used.</param>
		/// <param name="context"></param>
		/// <param name="byteOrder"></param>
		/// <param name="conflictResolver"></param>
		/// <param name="progressMonitor"></param>
		/// <param name="progressUpdateRate"></param>
		/// <returns></returns>
		public Task<Asset> LoadAsync(Stream stream, string name, FileManager fileManager = null, Asset context = null, ByteOrder byteOrder = ByteOrder.LittleEndian, ResolveLoadConflictCallback conflictResolver = null, AssetLoaderProgressCallback progressMonitor = null, TimeSpan? progressUpdateRate = null) {
			return LoadAsync(BigEndianBinaryReader.Create(byteOrder, stream), name, fileManager, context, conflictResolver, progressMonitor, progressUpdateRate);
		}


		/// <summary>Load an <see cref="Asset"/></summary>
		/// <param name="reader"></param>
		/// <param name="name"></param>
		/// <param name="fileManager">The <see cref="FileManager"/> to use. If <c>null</c> (the default), the system file manager is used.</param>
		/// <param name="context"></param>
		/// <param name="conflictResolver"></param>
		/// <returns></returns>
		public Asset Load(BinaryReader reader, string name, FileManager fileManager = null, Asset context = null, ResolveLoadConflictCallback conflictResolver = null) {
			return AssetFormat.LoadAsset(new AssetLoader(this, reader, name, fileManager, context), AllEnabledFormats, conflictResolver);
		}

		/// <summary>Load an <see cref="Asset"/> asynchronously.</summary>
		/// <param name="reader"></param>
		/// <param name="name"></param>
		/// <param name="fileManager">The <see cref="FileManager"/> to use. If <c>null</c> (the default), the system file manager is used.</param>
		/// <param name="context"></param>
		/// <param name="conflictResolver"></param>
		/// <param name="progressMonitor"></param>
		/// <param name="progressUpdateRate"></param>
		/// <returns></returns>
		public Task<Asset> LoadAsync(BinaryReader reader, string name, FileManager fileManager, Asset context = null, ResolveLoadConflictCallback conflictResolver = null, AssetLoaderProgressCallback progressMonitor = null, TimeSpan? progressUpdateRate = null) {
			return AssetFormat.LoadAssetAsync(new AssetLoader(this, reader, name, fileManager, context), AllEnabledFormats, conflictResolver, progressMonitor, progressUpdateRate);
		}

		/// <summary>Load plugins from a search path matching a search pattern.</summary>
		/// <param name="searchPath"></param>
		/// <param name="searchPattern"></param>
		public void LoadPlugins(string searchPath, string searchPattern = "*.dll") {
			string[] paths = Directory.EnumerateFiles(searchPath, searchPattern, SearchOption.AllDirectories).ToArray();
			foreach (string path in paths)
				LoadPlugins(Assembly.LoadFile(path));
		}

		/// <summary>Load plugins from an assembly.</summary>
		/// <param name="assembly"></param>
		public void LoadPlugins(Assembly assembly) {
			foreach (Type pluginType in assembly.GetExportedTypes())
				LoadPlugin(pluginType);
		}

		/// <summary>Attempt to load a plugin type.</summary>
		/// <param name="pluginType"></param>
		public void LoadPlugin(Type pluginType) {
			if (pluginType.IsAbstract || !pluginType.IsSubclassOf(typeof(AssetPlugin)) || HasPluginType(pluginType))
				return;
			ConstructorInfo constructor = pluginType.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[] { typeof(AssetManager) }, null);
			if (constructor == null)
				throw new Exception(string.Format("Plugin {0} of assembly {1} does not have a ({2}) constructor.", pluginType.FullName, pluginType.Assembly.FullName, typeof(AssetManager).Name));
			AssetPlugin plugin = (AssetPlugin)constructor.Invoke(new object[] { this });
		}
	}

}
