using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics.Loaders.Fbx {
	public abstract class FbxDocument : FbxContainer {
		[FbxProperty("ActiveAnimStackName", 1)]
		string ActiveAnimationStackName { get; set; }

		[FbxProperty("SourceObject", 0)]
		int SourceObject { get; set; }

		/*
		Document: 1040233808, "", "Scene" {
		Properties70:  {
			P: "SourceObject", "object", "", ""
			P: "ActiveAnimStackName", "KString", "", "", ""
		}
		RootNode: 0
	}*/
	}
}
