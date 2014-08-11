using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Alexandria.Engines.Unreal.Core;

namespace Alexandria.Engines.Unreal {
	/// <summary>
	/// An import into a <see cref="Package"/>.
	/// </summary>
	public class Import : Reference {
		/// <summary></summary>
		public Import(Package package, int index)
			: base(package, index) {
		}

		internal void Load(BinaryReader reader) {
			ClassPackageName = Package.ReadNameValue(reader);
			ClassName = Package.ReadNameValue(reader);
			PackageReference = Package.ResolveReference(reader.ReadInt32());
			Name = Package.ReadNameValue(reader);
		}

		/// <summary>
		/// Package file the imported object's class is defined in.
		/// </summary>
		public string ClassPackageName { get; protected set; }

		/// <summary>
		/// Name of the class of the object.
		/// </summary>
		public string ClassName { get; protected set; }

		/// <summary>
		/// Package this is defined in.
		/// </summary>
		public Reference PackageReference { get; protected set; }

		/// <summary></summary>
		public override RootObject Object {
			get { return AsExport.Object; }
		}

		/// <summary></summary>
		public override RootObject ResolveNewObject() {
			return State.CallFactory(ClassPackageName, ClassName, Name);
		}

		/// <summary></summary>
		public override string ToString() {
			return "Import(" + PackageReference.Name + ":" + Name + ", Class: " + ClassPackageName + ":" + ClassName + ")";
		}

		Export Link;

		/// <summary>
		/// Load the package this references and find the package.
		/// </summary>
		public override Export AsExport {
			get {
				if (Link == null) {
					if (ClassPackageName == "Core" && ClassName == "Package")
						Link = State.OpenPackage(Name).Export;
					else {
						var package = (Package)PackageReference.Object;

						Link = package.FindExport(ClassPackageName, ClassName, Name);
						if (Link == null)
							throw new Exception("Could not find " + ClassPackageName + "." + ClassName + " in " + package.Export.Name + ".");
					}
				}
				return Link;
			}
		}
	}

	/// <summary>
	/// A <see cref="List"/> of <see cref="Import"/>.
	/// </summary>
	public class ImportList : List<Import> {
		/// <summary></summary>
		public ImportList() : base() { }

		/// <summary></summary>
		public ImportList(int capacity) : base(capacity) { }
	}
}
