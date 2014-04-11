using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics.Loaders.Fbx {
	public class FbxGeometryBase : FbxLayerContainer {
		[FbxProperty("Vertices", 0, IsContent = true)]
		Vector3d[] ControlPoints { get; set; }

		int GeometryVersion { get; set; }
	}
}
