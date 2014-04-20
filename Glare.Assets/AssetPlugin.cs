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
	public abstract class AssetPlugin : PluginFormatAsset {
		/// <summary>
		/// Get the <see cref="System.Reflection.Assembly"/> in which this <see cref="AssetPlugin"/> is declared.
		/// </summary>
		public Assembly DeclaringAssembly { get { return GetType().Assembly; } }

		public AssetPlugin(AssetManager manager, ResourceManager resourceManager)
			: base(manager, resourceManager) {
			manager.plugins.Add(this);
		}
	}

	public abstract class PluginAsset : Asset {
		/// <summary>Get or set whether this <see cref="PluginAsset"/> is enabled through <see cref="IsSelfEnabled"/>, or whether it's disabled by its hierarchy.</summary>
		public virtual bool IsEnabled {
			get { return IsSelfEnabled; }
		}

		/// <summary>Get or set whether this <see cref="PluginAsset"/> is specifically enabled or disabled.</summary>
		public bool IsSelfEnabled {
			get { return Manager.GetIsPluginAssetEnabled(this); }

			set { Manager.SetIsPluginAssetEnabled(this, value); }
		}

		public AssetPlugin Plugin { get; private set; }

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

	public abstract class PluginFormatAsset : PluginAsset {
		readonly RichList<AssetFormat> FormatsMutable = new RichList<AssetFormat>();

		public virtual IEnumerable<AssetFormat> AllFormats {
			get {
				foreach (AssetFormat format in FormatsMutable)
					yield return format;
			}
		}

		/// <summary>Get the resource formats.</summary>
		public ReadOnlyList<AssetFormat> Formats { get { return FormatsMutable; } }

		public PluginFormatAsset(AssetPlugin plugin) : base(plugin) { }

		internal PluginFormatAsset(AssetManager manager, ResourceManager resourceManager) : base(manager, resourceManager) { }

		protected void AddFormat(AssetFormat format) {
			if (format == null)
				throw new ArgumentNullException("format");
			if (format.Plugin != Plugin || FormatsMutable.Contains(format))
				throw new ArgumentException(format.Name + " cannot be added to " + Name + ".");
			FormatsMutable.Add(format);
		}
	}
}
