using Glare;
using Glare.Assets.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Engines.DarkSouls {
	/// <summary>
	/// An asset browser for a <see cref="Model"/>.
	/// </summary>
	public class ModelBrowser : ModelAssetBrowser {
		readonly Model Model;

		/// <summary>
		/// Initialise the model browser.
		/// </summary>
		/// <param name="model"></param>
		public ModelBrowser(Model model) {
			Model = model;
			WorldTranslation = -model.Bounds.Center;
			ViewLookAtDistance = model.Bounds.Radius * 3;
		}

		/// <summary>
		/// Render the model.
		/// </summary>
		protected override void Render() {
			base.Render();

			var context = new ModelDrawContext() {
				Projection = Projection,
				View = View,
				World =World,
				DisplayMode = Program.DisplayMode,
			};

			Model.Draw(context);
		}
	}
}
