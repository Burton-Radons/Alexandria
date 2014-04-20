using Glare;
using Glare.Assets;
using Glare.Framework;
using Glare.Graphics;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alexandria {
	public class AlexandriaManager : AssetManager {
		static StringCollection DisabledPluginResources { get { return Properties.Settings.Default.DisabledPluginResources ?? (Properties.Settings.Default.DisabledPluginResources = new System.Collections.Specialized.StringCollection()); } }

		protected override bool GetIsPluginAssetEnabled(PluginAsset resource) {
			return !DisabledPluginResources.Contains(resource.GetType().FullName);
		}

		public void LoadPlugins() {
			LoadPlugins(Application.StartupPath, "Alexandria*.dll");
		}

		protected override void SetIsPluginAssetEnabled(PluginAsset resource, bool value) {
			if (value != GetIsPluginAssetEnabled(resource)) {
				if (value)
					DisabledPluginResources.Remove(GetType().FullName);
				else
					DisabledPluginResources.Add(GetType().FullName);
				Properties.Settings.Default.Save();
			}
		}
	}
}
