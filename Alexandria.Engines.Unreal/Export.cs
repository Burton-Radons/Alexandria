using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Alexandria.Engines.Unreal.Core;

namespace Alexandria.Engines.Unreal {
	/// <summary>
	/// A symbol exported from a package.
	/// </summary>
	public class Export : Reference {
		internal Export(Package package, int index)
			: base(package, index) {
		}

		internal void Load(BinaryReader reader) {
			ObjectClassReference = Package.ReadReference(reader);
			SuperClassReference = Package.ReadReference(reader);
			Group = reader.ReadInt32();
			Name = Package.ReadNameValue(reader);
			Flags = (ExportFlag)reader.ReadUInt32();
			Size = UIndex.Read(reader);
			if (Size != 0)
				Offset = UIndex.Read(reader);
		}

		/// <summary>
		/// Flags applied to the export.
		/// </summary>
		public ExportFlag Flags { get; protected set; }

		/// <summary>
		/// Reference to the exported object's class.
		/// </summary>
		public Reference ObjectClassReference { get; protected set; }

		/// <summary>
		/// Reference to the exported object's super class.
		/// </summary>
		public Reference SuperClassReference { get; protected set; }

		public int Group { get; protected set; }

		/// <summary>
		/// Size of the object as stored in the package.
		/// </summary>
		public int Size { get; protected set; }

		/// <summary>
		/// Offset of the object's data in the package.
		/// </summary>
		public int Offset { get; protected set; }

		internal RootObject LoadedObject;

		/// <summary>
		/// Get the object this export refers to.
		/// </summary>
		public override RootObject Object {
			get {
				if (LoadedObject == null) {
					if (ObjectClassReference == null) {
						LoadedObject = new Class();
					} else
						LoadedObject = ObjectClassReference.ResolveNewObject();

					LoadedObject.Export = this;
					var reader = Package.Reader;
					var end = Offset + Size;
					reader.BaseStream.Position = (uint)Offset;

					if (Package.Game == PackageGame.ThiefDeadlyShadows) {
						uint u1 = 0, u2;
						if (!(LoadedObject is Class))
							u1 = reader.ReadUInt32();
						u2 = reader.ReadUInt32();
						if (u1 != 0 || u2 != 0)
							throw new InvalidOperationException();
					}

					LoadedObject.Load(reader, end);

					if (Name == "Sprintf")
						reader.BaseStream.Position = end;
					var position = reader.BaseStream.Position;
					if (position != end) {
						var text = "";
						if (position < end)
							text += "Object reader read too little; ";
						else
							text += "Object reader read too much; ";
						text += "Should have ended at position " + end + " but instead ended at " + position + " (" + (position - end) + " byte(s) off).";
						throw new Exception(text);
					}
				}

				return LoadedObject;
			}
		}

		public override string ToString() {
			return "Export(" + Name.ToString() + ", ObjectClass: " + ObjectClassReference + ", SuperClass: " + SuperClassReference + ", Group: " + Group + ", Size: " + Size + ", Offset: " + Offset + ")";
		}

		public override Export AsExport {
			get { return this; }
		}
	}


	/// <summary>
	/// A <see cref="List"/> of <see cref="Export"/>.
	/// </summary>
	public class ExportList : List<Export> {
		public ExportList() : base() { }
		public ExportList(int capacity) : base(capacity) { }
	}
}
