using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics.Loaders.Fbx {
	public class FbxPropertyAttribute : Attribute {
		public bool IsContent { get; set; }

		public int Order { get; set; }

		public string SerializedName { get; set; }

		public FbxPropertyAttribute(int order) {
			Order = order;
		}

		public FbxPropertyAttribute(string serializedName, int order) : this(order) { }
	}
}
