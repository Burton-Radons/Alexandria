using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Resources {
	/// <summary>
	/// An archive file, which should contain only a hierarchy of <see cref="Folder"/> and <see cref="Asset"/>s.
	/// </summary>
	public class Archive : Folder {
		public Archive(Manager manager, string name, string description = null) : base(manager, name, description) {
		}
	}
}
