using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics.Loaders.Fbx {
	/// <summary>
	/// An unattached property handle for an <see cref="FbxObject"/>.
	/// </summary>
	public class FbxPropertyHandle {
		public FbxClass Class { get; private set; }

		/// <summary>Get or set the property label.</summary>
		public string Label { get; set; }

		public string Name { get; private set; }

		public PropertyInfo Property { get; private set; }

		internal FbxPropertyHandle(FbxClass @class, PropertyInfo property) {
			Class = @class;
			Property = property;
			Name = property.Name;
		}
	}
}
