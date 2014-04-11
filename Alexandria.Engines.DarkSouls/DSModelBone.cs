using Glare;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.DarkSouls {
	public class DSModelBone : DSModelObject {
		#region Internal

		internal const int DataSize = 4 * 32;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		int parentIndex, firstChildIndex, nextSiblingIndex, previousSiblingIndex;

		#endregion Internal

		#region Properties

		public Angle3 Angle { get; private set; }

		public Box3f Bounds { get; private set; }

		public DSModelBone FirstChild { get { return FromIndex(firstChildIndex); } }

		public string Name { get; private set; }

		public DSModelBone NextSibling { get { return FromIndex(nextSiblingIndex); } }

		public DSModelBone Parent { get { return FromIndex(parentIndex); } }

		public Vector3f Position { get; private set; }

		public DSModelBone PreviousSibling { get { return FromIndex(previousSiblingIndex); } }

		public Vector3f Scale { get; private set; }

		#endregion Properties

		internal DSModelBone(DSModel model, int index, BinaryReader reader)
			: base(model, index) {
			Position = reader.ReadVector3f();
			Name = reader.ReadStringzAtUInt32(Encoding);
			Angle = Angle3.Radians(reader.ReadVector3f());
			parentIndex = reader.ReadInt16();
			firstChildIndex = reader.ReadInt16();
			Scale = reader.ReadVector3f();
			nextSiblingIndex = reader.ReadInt16();
			previousSiblingIndex = reader.ReadInt16();
			var min = reader.ReadVector3f();
			Unknowns.ReadInt32s(reader, 1); // 0 or 1
			var max = reader.ReadVector3f();
			Bounds = new Box3f(min, max);
			reader.RequireZeroes(4 * 13);
		}

		internal void BuildCollada() {
		}

		DSModelBone FromIndex(int index) { return index < 0 ? null : Model.Bones[index]; }

		public override string ToString() {
			string text = string.Format("{0}('{1}'", GetType().Name, Name);
			AddReference(ref text, "Parent", Parent);
			AddReference(ref text, "Child", FirstChild);
			text += ", Position " + Position.ToShortString();
			if (Math.Abs(Scale.X * Scale.Y * Scale.Z - 1) > 0.1f)
				text += ", Scale " + Scale.ToShortString();
			if(Angle.AbsoluteSum > Glare.Angle.Degree)
				text += ", Angle " + Angle.ToShortString();
			text += ", Bounds " + Bounds.ToString();
			//AddReference(ref text, "Next", NextSibling);
			//AddReference(ref text, "Previous", PreviousSibling);
			foreach(UnknownBlock block in Unknowns)
				text += ", " + block.JoinedValues;
			return text + ")";
		}

		void AddReference(ref string text, string name, DSModelBone value) {
			if (value != null)
				text += ", " + name + " '" + value.Name + "'";
		}
	}
}
