using Glare.Assets.Formats;
using Glare.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Assets {
	/// <summary>
	/// A plugin describing <see cref="AssetFormat"/>s. Subclasses must have a single constructor (<see cref="AssetManager"/>). All <see cref="AssetPlugin"/>s in an assembly will be constructed if they are non-abstract.
	/// </summary>
	public abstract class AssetPlugin : PluginFormatAsset {
		/// <summary>
		/// Get the <see cref="System.Reflection.Assembly"/> in which this <see cref="AssetPlugin"/> is declared.
		/// </summary>
		public Assembly DeclaringAssembly { get { return GetType().Assembly; } }

		/// <summary>
		/// Initialise the plugin.
		/// </summary>
		/// <param name="manager"></param>
		/// <param name="resourceManager"></param>
		public AssetPlugin(AssetManager manager, ResourceManager resourceManager)
			: base(manager, resourceManager) {
			manager.plugins.Add(this);
		}
	}

	/// <summary>
	/// An asset within a <see cref="Plugin"/>.
	/// </summary>
	public abstract class PluginAsset : Asset {
		/// <summary>Get or set whether this <see cref="PluginAsset"/> is enabled through <see cref="IsSelfEnabled"/>, and is not disabled by its hierarchy.</summary>
		public virtual bool IsEnabled {
			get { return IsSelfEnabled; }
		}

		/// <summary>Get or set whether this <see cref="PluginAsset"/> is specifically enabled or disabled.</summary>
		public bool IsSelfEnabled {
			get { return Manager.GetIsPluginAssetEnabled(this); }

			set { Manager.SetIsPluginAssetEnabled(this, value); }
		}

		/// <summary>Get the <see cref="AssetPlugin"/> this is in.</summary>
		public AssetPlugin Plugin { get; private set; }

		/// <summary>Initialise the plugin asset.</summary>
		/// <param name="plugin"></param>
		public PluginAsset(AssetPlugin plugin)
			: base(plugin.Manager, plugin.ResourceManager) {
			Plugin = plugin;
		}

		internal PluginAsset(AssetManager manager, ResourceManager resourceManager)
			: base(manager, resourceManager) {
			if (!(this is AssetPlugin))
				throw new InvalidOperationException();
			Plugin = (AssetPlugin)this;
		}
	}

	/// <summary>
	/// A <see cref="PluginAsset"/> that provides <see cref="AssetFormat"/>s.
	/// </summary>
	public abstract class PluginFormatAsset : PluginAsset {
		readonly Codex<AssetFormat> FormatsMutable = new Codex<AssetFormat>();

		/// <summary>An enumerator over all enabled <see cref="AssetFormat"/>s in this asset or any child assets.</summary>
		public virtual IEnumerable<AssetFormat> AllEnabledFormats {
			get {
				foreach (AssetFormat format in FormatsMutable)
					if(format.IsEnabled)
						yield return format;
			}
		}

		/// <summary>Get the resource formats.</summary>
		public Codex<AssetFormat> Formats { get { return FormatsMutable; } }

		/// <summary>
		/// Initialise the plugin asset.
		/// </summary>
		/// <param name="plugin"></param>
		public PluginFormatAsset(AssetPlugin plugin) : base(plugin) { }

		internal PluginFormatAsset(AssetManager manager, ResourceManager resourceManager) : base(manager, resourceManager) { }

		/// <summary>Add a format to the collection of formats.</summary>
		/// <param name="format"></param>
		protected void AddFormat(AssetFormat format) {
			if (format == null)
				throw new ArgumentNullException("format");
			if (format.Plugin != Plugin || FormatsMutable.Contains(format))
				throw new ArgumentException(format.Name + " cannot be added to " + Name + ".");
			FormatsMutable.Add(format);
		}
	}
}
