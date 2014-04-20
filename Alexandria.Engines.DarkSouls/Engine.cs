using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.DarkSouls {
	public class Engine : Alexandria.Engine {
		public Engine(Plugin plugin)
			: base(plugin) {
			AddFormat(new ArchiveFormat(this));
			AddFormat(new DSModelFormat(this));
			AddFormat(new StringArchiveFormat(this));
			AddFormat(new TableArchiveFormat(this));
			//AddFormat(new TextureFormat(this));
			AddFormat(new TextureArchiveFormat(this));
			AddFormat(new FsslFormat(this));
		}
	}
}
