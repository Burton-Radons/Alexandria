using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics.Rendering {
	/// <summary>
	/// A detail level of a <see cref="ModelMesh"/>. This is composed of a collection of <see cref="ModelPart"/>s, which are the actual rendering calls.
	/// </summary>
	public class ModelDetailLevel {
		/// <summary>Get the <see cref="ModelMesh"/> this is for, or <c>null</c> if the <see cref="ModelDetailLevel"/> has not been added to one.</summary>
		public ModelMesh Mesh { get; internal set; }

		/// <summary>
		/// Get the maximum distance at which this detail level should be chosen.
		/// </summary>
		public double MaximumDistance { get; private set; }
	}
}
