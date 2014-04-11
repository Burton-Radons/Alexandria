using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charana {
	[Serializable]
	public class Module {
		public const int MinimumVersion = 1;
		public const int CurrentVersion = 1;

		readonly List<Module> dependencies = new List<Module>();
		readonly int version = CurrentVersion;

		public List<Module> Dependencies { get { return dependencies; } }

		public int Version { get { return version; } }
	}
}
