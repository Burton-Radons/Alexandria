using Glare.Assets.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Assets {
	/// <summary>
	/// An asset that contains other assets.
	/// </summary>
	public class FolderAsset : Asset {
		/// <summary>
		/// Initialise the folder asset.
		/// </summary>
		/// <param name="manager"></param>
		/// <param name="name"></param>
		/// <param name="description"></param>
		public FolderAsset(AssetManager manager, string name, string description = null) : base(manager, name, description) { }

		/// <summary>
		/// Initialise the folder asset.
		/// </summary>
		/// <param name="parent"></param>
		/// <param name="name"></param>
		/// <param name="description"></param>
		public FolderAsset(FolderAsset parent, string name, string description = null) : base(parent, name, description) { }

		/// <summary>
		/// Initialise the folder asset.
		/// </summary>
		/// <param name="parent"></param>
		/// <param name="loader"></param>
		public FolderAsset(FolderAsset parent, AssetLoader loader) : base(parent, loader) { }

		/// <summary>
		/// Initialise the folder asset.
		/// </summary>
		/// <param name="manager"></param>
		/// <param name="loader"></param>
		public FolderAsset(AssetLoader loader) : base(loader) { }

		/// <summary>
		/// Provide a browser that shows the child assets as a tree.
		/// </summary>
		/// <param name="progressUpdateCallback"></param>
		/// <returns></returns>
		public override System.Windows.Forms.Control Browse(Action<double> progressUpdateCallback = null) {
			return new FolderAssetBrowser(this, progressUpdateCallback);
		}

		/// <summary>
		/// Provide a browser that shows the child assets as a tree.
		/// </summary>
		/// <param name="progressUpdateCallback"></param>
		/// <returns></returns>
		public override System.Windows.Forms.Control BrowseContents(Action<double> progressUpdateCallback = null) {
			return Browse(progressUpdateCallback);
		}
	}
}
