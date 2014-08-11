using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.Creation.Groups {
	/// <summary>A <see cref="ModuleGroupType.Top"/> <see cref="Group"/> that contains records or groups of a given type.</summary>
	public abstract class TopGroup : Group {
		public override Glare.Framework.ReadOnlyCodex<Glare.Assets.Asset> Children {
			get {
				LoadChildren();
				return base.Children;
			}
		}

		/// <summary>Get the 4-letter id of each asset contained within this group.</summary>
		public override string Name {
			get { return base.Name; }
			set { base.Name = value; }
		}

		protected bool ChildrenLoaded;

		internal TopGroup(Module module, uint contentSize, uint label) : base(module, contentSize, label, ModuleGroupType.Top) {
		}

		void LoadChildren() {
			if (ChildrenLoaded)
				return;

			ChildrenLoaded = true;
			LoadChildrenBase();
		}

		protected abstract void LoadChildrenBase();
	}
}
