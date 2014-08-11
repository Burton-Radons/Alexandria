using Glare.Assets;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Games.Arcanum {
	public class Art : FolderAsset {
		internal Art(AssetLoader loader)
			: base(loader) {
				throw new NotImplementedException();
		}
	}

	public class ArtFormat : AssetFormat {
		internal ArtFormat(Game game)
			: base(game, typeof(Art), canLoad: true, extension: ".art") {
		}
	}
}
