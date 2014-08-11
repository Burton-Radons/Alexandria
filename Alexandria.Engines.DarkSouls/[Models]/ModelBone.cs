using Glare;
using Glare.Internal;
using Glare.Assets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.DarkSouls {
	/// <summary>
	/// A bone in a <see cref="Model"/>.
	/// </summary>
	public class ModelBone : ModelAsset {
		#region Internal

		internal const int DataSize = 4 * 32;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		int parentIndex, firstChildIndex, nextSiblingIndex, previousSiblingIndex;

		#endregion Internal

		#region Properties

		/// <summary>
		/// Get the angle of the bone.
		/// </summary>
		public Angle3 Angle { get; private set; }

		/// <summary>
		/// Get the bounding box of the bone.
		/// </summary>
		public Box3f Bounds { get; private set; }

		/// <summary>
		/// Get the first child of the bone, or <c>null</c> for none.
		/// </summary>
		public ModelBone FirstChild { get { return FromIndex(firstChildIndex); } }
		
		/// <summary>
		/// Get the next sibling of the bone, or <c>null</c> for none.
		/// </summary>
		public ModelBone NextSibling { get { return FromIndex(nextSiblingIndex); } }

		/// <summary>
		/// Get the parent of the bone, or <c>null</c> for none.
		/// </summary>
		public ModelBone ParentBone { get { return FromIndex(parentIndex); } }

		/// <summary>
		/// Get the position of the bone.
		/// </summary>
		public Vector3f Position { get; private set; }

		/// <summary>
		/// Get the previous sibling of the bone, or <c>null</c> for none.
		/// </summary>
		public ModelBone PreviousSibling { get { return FromIndex(previousSiblingIndex); } }

		/// <summary>
		/// Get the scaling factors of the bone.
		/// </summary>
		public Vector3f Scale { get; private set; }

		#endregion Properties

		internal ModelBone(FolderAsset folder, int index, AssetLoader loader)
			: base(folder, index, loader) {
			var reader = loader.Reader;

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

		ModelBone FromIndex(int index) { return index < 0 ? null : Model.Bones[index]; }

		/// <summary>
		/// Conver to a string representation.
		/// </summary>
		/// <returns></returns>
		public override string ToString() {
			string text = string.Format("{0}('{1}'", GetType().Name, Name);
			AddReference(ref text, "Parent", ParentBone);
			AddReference(ref text, "Child", FirstChild);
			text += ", Position " + Position.ToShortString();
			if (Math.Abs(Scale.X * Scale.Y * Scale.Z - 1) > 0.1f)
				text += ", Scale " + Scale.ToShortString();
			if (Angle.AbsoluteSum > Glare.Angle.Degrees(1))
				text += ", Angle " + Angle.ToShortString();
			text += ", Bounds " + Bounds.ToString();
			//AddReference(ref text, "Next", NextSibling);
			//AddReference(ref text, "Previous", PreviousSibling);
			foreach (UnknownBlock block in Unknowns)
				text += ", " + block.JoinedValues;
			return text + ")";
		}

		void AddReference(ref string text, string name, ModelBone value) {
			if (value != null)
				text += ", " + name + " '" + value.Name + "'";
		}
	}
}
