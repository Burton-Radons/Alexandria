using Glare.Assets;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.Sciagi {
	/// <summary>
	/// The SCI/AGI plugin.
	/// </summary>
	public class Plugin : AlexandriaPlugin {
		internal static ResourceManager OurResourceManager { get { return Properties.Resources.ResourceManager; } }

		internal Plugin(AssetManager manager)
			: base(manager, OurResourceManager) {
			AddFormat(new ResourceMapFormat(this));
		}
	}
}
