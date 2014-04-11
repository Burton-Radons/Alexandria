using Alexandria.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Resources
{
	public class Folder : Resource
	{
		public Folder(Manager manager, string name, string description = null) : base(manager, name, description) { }
		public Folder(Folder parent, string name, string description = null) : base(parent, name, description) { }

		public override System.Windows.Forms.Control Browse() {
			return new FolderResourceBrowser(this);
		}
	}
}
