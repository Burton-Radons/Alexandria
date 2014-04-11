using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.DarkSouls
{
    public class Plugin : Alexandria.Plugin
    {
	    public Plugin(Manager manager)
		    : base(manager, Properties.Resources.ResourceManager)
	    {
		    new Engine(this);
	    }
    }
}
