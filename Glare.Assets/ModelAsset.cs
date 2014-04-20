using Glare.Assets.Controls;
using Glare;
using Glare.Assets;
using Glare.Graphics;
using Glare.Graphics.Rendering;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Glare.Assets {
	public class ModelAsset : Asset {
		public override object GlareObject { get { return Content; } }

		public Glare.Graphics.Rendering.Model Content { get; protected set; }

		public ModelAsset(AssetManager manager, string name, string description = null)
			: base(manager, name, description) {
		}

		public override System.Windows.Forms.Control Browse() {
			return new ModelAssetBrowser(this);
			/*
*/
		}
	}
}
