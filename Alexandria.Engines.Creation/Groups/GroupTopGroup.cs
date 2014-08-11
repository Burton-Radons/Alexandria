using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.Creation.Groups {
	/// <summary>A type of <see cref="TopGroup"/> that contains <see cref="Group"/>s rather than <see cref="Record"/>s.</summary>
	public class GroupTopGroup : TopGroup {
		/// <summary>Initialise the <see cref="GroupTopGroup"/>.</summary>
		/// <param name="module"></param>
		/// <param name="contentSize"></param>
		/// <param name="label"></param>
		internal GroupTopGroup(Module module, uint contentSize, uint label)
			: base(module, contentSize, label) {
		}

		protected override void LoadChildrenBase() {
		}
	}
}
