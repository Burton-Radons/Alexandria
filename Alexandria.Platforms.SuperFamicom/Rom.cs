using Glare.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Platforms.SuperFamicom {
	/// <summary>
	/// A Super Famicom Rom.
	/// </summary>
	public class Rom : FolderAsset {
		internal Rom(AssetLoader loader)
			: base(loader) {
		}
	}
}
