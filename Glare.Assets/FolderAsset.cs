using Glare.Assets.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Assets {
	public class FolderAsset : Asset {
		public FolderAsset(AssetManager manager, string name, string description = null) : base(manager, name, description) { }
		public FolderAsset(FolderAsset parent, string name, string description = null) : base(parent, name, description) { }

		public override System.Windows.Forms.Control Browse() {
			return new FolderAssetBrowser(this);
		}
	}
}
