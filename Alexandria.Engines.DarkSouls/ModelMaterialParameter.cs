using Glare.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Alexandria.Engines.DarkSouls {
	public class DSModelMaterialParameter : ModelObject {
		#region Internal

		internal const int DataSize = 4 * 8;

		#endregion Internal

		#region Properties

		/// <summary>Get the <see cref="ModelMaterial"/> that uses this <see cref="Material"/>.</summary>
		public ModelMaterial Material { get; private set; }

		/// <summary>Get the zero-based index of this parameter in the <see cref="Material"/>.</summary>
		public int MaterialIndex { get; private set; }

		/// <summary>Get the filename of the texture or "" for none.</summary>
		public string Value { get; private set; }

		#endregion Properties

		internal DSModelMaterialParameter(ModelMaterial material, int materialIndex, int index, BinaryReader reader)
			: base(material, index, reader) {
			Material = material;
			MaterialIndex = materialIndex;

			Value = reader.ReadStringzAtUInt32(Encoding);
			Name = reader.ReadStringzAtUInt32(Encoding);
			Unknowns.ReadSingles(reader, 2);
			Unknowns.ReadInt16s(reader, 1); // Always 257?
			reader.RequireZeroes(2);

			reader.RequireZeroes(12);
		}

		public override string ToString() {
			return string.Format("{0}(at {5:X}h, {1} = \"{2}\", {3}, {4})", GetType().Name, Name, Value, Unknowns[0].JoinedValues, Unknowns[1].JoinedValues, DataOffset);
		}
	}
}
