using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glare.Internal;

namespace Alexandria.Engines.DarkSouls {
	public class DSModelBoneUnknown : DSModelObject {
		#region Internal

		internal const int DataSize = 4 * 16;

		#endregion Internal

		public DSModelBoneUnknown(DSModel model, int index, BinaryReader reader)
			: base(model, index) {
			Unknowns.ReadSingles(reader, 3);
			reader.Require(IsDS1 ? 0xFFFFFFFFu : 0xFF000000u);
			Unknowns.ReadSingles(reader, 3);
			Unknowns.ReadInt16s(reader, 2);
			Unknowns.ReadSingles(reader, 3);
			Unknowns.ReadInt16s(reader, 2);
			reader.RequireZeroes(4 * 4);
		}

		public override string ToString() {
			return string.Format("{0}({1})", GetType().Name, Unknowns.ToCommaSeparatedList());
		}
	}
}
