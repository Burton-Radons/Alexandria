using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glare.Graphics.Terrains.Planar.Components {
	/// <summary>
	/// Perform node clipping on the terrain. Each <see cref="TerrainTreeNode"/> is sent to a clipping function which returns <see cref="Containment"/>; if it returns <see cref="Containment.Contains"/>, then the node is in view. If it returns <see cref="Containment.Intersects"/>, then the node intersects with an edge (and therefore any deeper nodes need to be tested). If it returns <see cref="Containment.Disjoint"/>, then the node is completely out of view.
	/// </summary>
	public abstract class ClipTerrainComponent : TerrainComponent {
		/// <summary>
		/// Initialise the clip module using the given clip function.
		/// </summary>
		/// <param name="terrain">The terrain to add this module to.</param>
		public ClipTerrainComponent(PlanarTerrain terrain)
			: base(terrain) {
		}

		/// <summary>
		/// Indicate that the <see cref="NodeClip"/> method is active.
		/// </summary>
		public override bool SupportsNodeClip { get { return true; } }

		/// <summary>
		/// Clip the node using the clipping function.
		/// </summary>
		/// <param name="node">The node to clip.</param>
		/// <returns>Whether the node is outside, intersects with, or is inside the clipping range.</returns>
		public abstract override Containment NodeClip(TerrainTreeNode node);
	}

	public class FrustumClipTerrainModule : ClipTerrainComponent {
		readonly Frustum Frustum;

		Matrix4d view, projection;

		public Matrix4d Projection {
			get { return projection; }
			set { projection = value; }
		}

		public Matrix4d View {
			get { return view; }
			set { view = value; }
		}

		public FrustumClipTerrainModule(PlanarTerrain terrain)
			: base(terrain) {
			Frustum = new Frustum(Matrix4d.Identity);
		}

		public override Containment NodeClip(TerrainTreeNode node) {
			return node.pBox.Intersect(Frustum);
		}

		public override void OnDrawing(PlanarTerrainBlock block, ref Matrix4d world) {
			base.OnDrawing(block, ref world);
			Frustum.Transform = world * view * projection;
		}
	}
}
