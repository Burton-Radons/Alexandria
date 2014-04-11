using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.DarkSouls
{
	public class Engine : Alexandria.Engine
	{
		public Engine(Plugin plugin)
			: base(plugin, Properties.Resources.ResourceManager)
		{
			new ArchiveLoader(this);
			new DSModelLoader(this);
			new StringArchiveLoader(this);
			new TableArchiveLoader(this);
			new TextureLoader(this);
			new TextureArchiveLoader(this);
		}
	}
}
