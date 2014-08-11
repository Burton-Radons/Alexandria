using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.UltimaUnderworld {
	public class Engine : Alexandria.Engine {
		internal Engine(Plugin plugin)
			: base(plugin) {
				AddFormat(new GraphicArchiveFormat(this));
			AddFormat(new PaletteArchiveFormat(this));
			AddFormat(new StringPackageFormat(this));

			AddGame(new GameUltimaUnderworld(this));
			AddGame(new GameUltimaUnderworld2(this));
			AddGame(new GameSystemShock(this));
		}
	}
}
