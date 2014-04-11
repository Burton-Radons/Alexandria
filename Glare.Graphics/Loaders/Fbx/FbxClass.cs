using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics.Loaders.Fbx {
	/// <summary>
	/// <see cref="FbxObject"/> class information.
	/// </summary>
	/// <remarks>
	/// This is called "FbxClassId" in the FBX SDK, because it was passed around as "FbxClassId", not "FbxClass*", giving it a different usage profile.
	/// </remarks>
	public class FbxClass {
		static readonly Dictionary<Type, FbxClass> list = new Dictionary<Type, FbxClass>();

		/// <summary>Get the FBX file object type string.</summary>
		public string FbxFileTypeName { get; private set; }

		public string Name { get; private set; }

		public string ObjectTypePrefix { get; private set; }

		/// <summary>Get the base <see cref="FbxClass"/> or <c>null</c> if there is none.</summary>
		public FbxClass Parent { get; private set; }

		/// <summary>Get the type this refers to, or <c>null</c> if it's a runtime class.</summary>
		public Type Type { get; private set; }

		FbxClass(Type type) {
			Type = type;
			FbxFileTypeName = Name = type.Name;
			ObjectTypePrefix = "";

			object[] attributes = type.GetCustomAttributes(typeof(FbxClassAttribute), false);
			foreach (FbxClassAttribute attribute in attributes) {
				FbxFileTypeName = attribute.FbxFileTypeName;
				ObjectTypePrefix = attribute.ObjectTypePrefix;
			}

			if (type.BaseType != typeof(FbxObject))
				Parent = FromType(type.BaseType);
		}

		public static FbxClass FromType(Type type) {
			if (type == null)
				throw new ArgumentNullException("type");

			lock (list) {
				FbxClass result;

				if (list.TryGetValue(type, out result))
					return result;

				if(!type.IsSubclassOf(typeof(FbxObject)))
					throw new ArgumentException("Type is not a subclass of " + typeof(FbxObject).Name + ".");
				return list[type] = new FbxClass(type);
			}
		}
	}
}
