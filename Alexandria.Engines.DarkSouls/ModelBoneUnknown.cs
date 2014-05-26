using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glare.Internal;
using Glare.Assets;

namespace Alexandria.Engines.DarkSouls {
	public class ModelBoneUnknown : ModelObject {
		#region Internal

		internal const int DataSize = 4 * 16;

		#endregion Internal

		internal ModelBoneUnknown(FolderAsset bonesFolder, int index, BinaryReader reader)
			: base(bonesFolder, index, reader) {
			Unknowns.ReadSingles(reader, 3);
			reader.Require(IsDS1 ? 0xFFFFFFFFu : 0xFF000000u);
			Unknowns.ReadSingles(reader, 3);
			Unknowns.ReadInt16s(reader, 2);
			Unknowns.ReadSingles(reader, 3);
			Unknowns.ReadInt16s(reader, 2);
			reader.RequireZeroes(4 * 4);
		}

		public override string ToString() {
			return string.Format("{0}(at {2:X}h{1})", GetType().Name, Unknowns.ToCommaPrefixedList(), DataOffset);
		}
	}
}
