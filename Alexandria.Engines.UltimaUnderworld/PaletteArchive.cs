using Glare.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Engines.UltimaUnderworld {
	public class PaletteArchive : FolderAsset {
		internal PaletteArchive(AssetLoader loader)
			: base(loader) {
				for (int index = 0; index < 8; index++) {
					PaletteAsset asset = PaletteAsset.ReadRgb(Manager, "Palette " + index, loader.Reader, 256, 63);
					AddChild(asset);
				}
		}
	}

	public class PaletteArchiveFormat : AssetFormat {
		internal PaletteArchiveFormat(Engine engine)
			: base(engine, typeof(PaletteArchive), canLoad: true, extension: ".dat") {
		}

		public override LoadMatchStrength LoadMatch(AssetLoader loader) {
			if (loader.Length != 8 * 768)
				return LoadMatchStrength.None;
			byte[] data = loader.Reader.ReadBytes(8 * 768);
			foreach (byte value in data)
				if (value > 63)
					return LoadMatchStrength.None;
			return LoadMatchStrength.Weak;
		}

		public override Asset Load(AssetLoader loader) {
			return new PaletteArchive(loader);
		}
	}
}
