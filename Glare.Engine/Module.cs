using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Engine {
	/// <summary>
	/// A hierarchy of <see cref="Node"/> objects.
	/// </summary>
	[Serializable]
	public class Module {
		/// <summary>Get the minimum supported version number.</summary>
		public const int MinimumVersion = 1;

		/// <summary>Get the maximum supported version number.</summary>
		public const int CurrentVersion = 1;

		readonly List<Module> dependencies = new List<Module>();
		readonly int version = CurrentVersion;

		/// <summary>Get the dependencies of the module.</summary>
		public List<Module> Dependencies { get { return dependencies; } }

		/// <summary>Get the version number of the module.</summary>
		public int Version { get { return version; } }
	}
}
