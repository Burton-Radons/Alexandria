using Glare.Assets;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Engines.DarkSouls {
	/// <summary>
	/// The asset format for processing <see cref="Model"/>s.
	/// </summary>
	public class ModelFormat : AssetFormat {
		/// <summary>
		/// Initialise the format.
		/// </summary>
		/// <param name="engine"></param>
		public ModelFormat(Engine engine) : base(engine, typeof(Model), canLoad: true, extension: ".flver") { }

		/// <summary>Attempt to match this as a model.</summary>
		/// <param name="loader"></param>
		/// <returns></returns>
		public override LoadMatchStrength LoadMatch(AssetLoader loader) { return LoadMatchMagic(loader, Model.Magic, 100); }

		/// <summary>Load the model.</summary>
		/// <param name="loader"></param>
		/// <returns></returns>
		public override Asset Load(AssetLoader loader) { return new Model(loader); }
	}
}
