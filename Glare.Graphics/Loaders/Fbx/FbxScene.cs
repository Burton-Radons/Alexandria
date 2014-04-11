using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics.Loaders.Fbx {
	public class FbxScene : FbxDocument {
		[FbxProperty(0, IsContent = true)]
		public FbxNode RootNode { get; set; }

		public FbxScene() {
			RootNode = new FbxNode();
		}
	}
}
