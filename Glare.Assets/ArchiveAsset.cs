using Glare.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Assets {
	/// <summary>
	/// An archive file, which should contain only a hierarchy of <see cref="FolderAsset"/> and <see cref="DataAsset"/>s.
	/// </summary>
	public class ArchiveAsset : FolderAsset {
		/// <summary>Get whether this archive can be modified.</summary>
		public virtual bool CanSave { get; private set; }

		public ArchiveAsset(AssetManager manager, string name, string description = null) : base(manager, name, description) {
		}
	}
}
