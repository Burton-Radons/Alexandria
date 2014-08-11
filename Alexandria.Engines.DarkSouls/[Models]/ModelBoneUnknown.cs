using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glare.Internal;
using Glare.Assets;

namespace Alexandria.Engines.DarkSouls {
	/// <summary>
	/// An unknown type related to bones.
	/// </summary>
	public class ModelBoneUnknown : ModelAsset {
		#region Internal

		internal const int DataSize = 4 * 16;

		#endregion Internal

		internal ModelBoneUnknown(FolderAsset bonesFolder, int index, AssetLoader loader)
			: base(bonesFolder, index, loader) {
			var reader = loader.Reader;

			Unknowns.ReadSingles(reader, 3);
			reader.Require(IsDS1 ? 0xFFFFFFFFu : 0xFF000000u);
			Unknowns.ReadSingles(reader, 3);
			Unknowns.ReadInt16s(reader, 2);
			Unknowns.ReadSingles(reader, 3);
			Unknowns.ReadInt16s(reader, 2);
			reader.RequireZeroes(4 * 4);
		}

		/// <summary>
		/// Convert to a string representation of the object.
		/// </summary>
		/// <returns></returns>
		public override string ToString() {
			return string.Format("{0}({1})", GetType().Name, Unknowns.ToCommaPrefixedList());
		}
	}
}
