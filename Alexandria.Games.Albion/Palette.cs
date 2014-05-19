using Glare.Assets;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Games.Albion {
	public class Palette : PaletteAsset {
		internal Palette(AssetManager manager, AssetLoader loader)
			: base(manager, loader.Name) {
			using (var reader = loader.Reader) {
				while (!loader.AtEnd)
					ColorsMutable.Add(Color.FromArgb(reader.ReadByte(), reader.ReadByte(), reader.ReadByte()));
			}
		}
	}

	class PaletteFormat : AssetFormat {
		public PaletteFormat(Game game)
			: base(game, typeof(Palette), canLoad: true) {
		}

		public override LoadMatchStrength LoadMatch(AssetLoader loader) {
			string filename = Path.GetFileName(loader.Name);

			if (string.Equals(filename, Constants.PaletteStatic, StringComparison.InvariantCultureIgnoreCase))
				return LoadMatchStrength.Medium;
			if (loader.Context is LibraryRecord && string.Equals(Path.GetFileName(loader.Context.Parent.Name), Constants.PaletteLibrary, StringComparison.InvariantCultureIgnoreCase))
				return LoadMatchStrength.Medium;
			return LoadMatchStrength.None;
		}

		public override Asset Load(AssetLoader loader) {
			return new Palette(Manager, loader);
		}
	}
}
