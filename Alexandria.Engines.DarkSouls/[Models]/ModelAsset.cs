using Glare.Assets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Alexandria.Engines.DarkSouls {
	/// <summary>An object contained within a <see cref="Model"/>.</summary>
	public abstract class ModelAsset : FolderAsset {
		#region Internal

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal Encoding Encoding { get { return Model.Encoding; } }

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal bool IsDS1 { get { return Model.IsDS1; } }

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal bool IsDS2 { get { return Model.IsDS2; } }

		#endregion Internal

		/// <summary>Get a friendly display name for the object.</summary>
		public override string DisplayName { get { return ToString(); } }

		/// <summary>Get the zero-based index of this object in the model.</summary>
		public int Index { get; private set; }

		/// <summary>Get the model that this is part of.</summary>
		public Model Model {
			get {
				Model model;
				for (Asset parent = Parent; (model = parent as Model) == null; parent = parent.Parent) ;
				return model;
			}
		}

		/// <summary>
		/// Initialise the object.
		/// </summary>
		/// <param name="parent"></param>
		/// <param name="index"></param>
		/// <param name="loader"></param>
		public ModelAsset(FolderAsset parent, int index, AssetLoader loader)
			: base(parent, "") {
			Name = GetType().Name + " " + index;
			Index = index;
		}
	}
}
