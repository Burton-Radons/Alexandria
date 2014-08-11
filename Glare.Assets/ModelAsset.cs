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
using System.ComponentModel;

namespace Glare.Assets {
	/// <summary>A model asset.</summary>
	public class ModelAsset : Asset {
		/// <summary>Get the glare model.</summary>
		public override object GlareObject { get { return Content; } }

		/// <summary>Get the model.</summary>
		public Glare.Graphics.Rendering.Model Content { get; protected set; }

		/// <summary>Initialise the asset.</summary>
		/// <param name="manager"></param>
		/// <param name="name"></param>
		/// <param name="description"></param>
		public ModelAsset(AssetManager manager, string name, string description = null)
			: base(manager, name, description) {
		}

		/// <summary>Produce a browser for the asset.</summary>
		/// <param name="progressUpdateCallback"></param>
		/// <returns></returns>
		public override System.Windows.Forms.Control Browse(Action<double> progressUpdateCallback = null) {
			return new GlareModelAssetBrowser(this);
		}
	}
}
