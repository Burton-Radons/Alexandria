using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics.Loaders.Fbx {
	/// <summary>Tells us about how to serialize/deserialise an <see cref="FbxObject"/> type.</summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public class FbxClassAttribute : Attribute {
		public string FbxFileTypeName { get; set; }
		public string ObjectTypePrefix { get; set; }

		public FbxClassAttribute() {
			ObjectTypePrefix = "";
		}

		public FbxClassAttribute(string serializedName) : this() { }
	}
}
