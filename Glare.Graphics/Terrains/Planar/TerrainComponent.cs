using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glare.Graphics.Terrains.Planar
{
	/// <summary>
	/// A module that is attached to a <see cref="Terrain"/> that adds functionality to it.
	/// </summary>
	public class TerrainComponent
	{
		public TerrainComponent(PlanarTerrain terrain)
		{
			if (terrain == null)
				throw new ArgumentNullException("terrain");
			Terrain = terrain;
			terrain.ComponentsMutable.Add(this);

			if (SupportsNodeLodBlend)
				terrain.ComponentsNodeLodBlend.Add(this);
			if (SupportsNodeClip)
				terrain.ComponentsNodeClip.Add(this);
		}

		/// <summary>Get the <see cref="Terrain"/> this is a <see cref="TerrainComponent"/> for.</summary>
		public PlanarTerrain Terrain { get; private set; }

		#region Detachment

		/// <summary>Detach the <see cref="TerrainComponent"/> from the <see cref="Terrain"/>.</summary>
		public void Detach()
		{
			if (Terrain != null)
				throw new InvalidOperationException("The module has already been detached from the terrain.");
			Terrain.ComponentsMutable.Remove(this);
			OnDetach();
			if (Detached != null)
				Detached(this, EventArgs.Empty);

			if (SupportsNodeLodBlend)
				Terrain.ComponentsNodeLodBlend.Remove(this);
			if (SupportsNodeClip)
				Terrain.ComponentsNodeClip.Remove(this);

			Terrain = null;
		}

		/// <summary>
		/// Called when the <see cref="TerrainComponent"/> is being detached from the <see cref="Terrain"/>.
		/// </summary>
		protected virtual void OnDetach() { }

		/// <summary>
		/// Invoked when the <see cref="TerrainComponent"/> is being detached from the <see cref="Terrain"/>.
		/// </summary>
		public event EventHandler Detached;

		#endregion

		#region Callbacks from the terrain.

		/// <summary>
		/// Return whether the node is contained in this module's clipping range.
		/// If this returns <see cref="Containment.Contains"/>, then any node with a smaller
		/// box must also be contained.
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public virtual Containment NodeClip(TerrainTreeNode node)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Return how much to blend between this LOD level and the next less detailed one.
		/// If this returns 1, then the node should be divided if possible.
		/// </summary>
		/// <param name="node"></param>
		/// <param name="distance"></param>
		/// <returns></returns>
		public virtual double NodeLodBlend(TerrainTreeNode node, double distance)
		{
			throw new NotImplementedException();
		}

		/// <summary>This is called when the <see cref="PlanarTerrain"/> has added a new <see cref="PlanarTerrainBlock"/>.</summary>
		/// <param name="block"></param>
		public virtual void OnBlockAdded(PlanarTerrainBlock block)
		{
		}

		public virtual void OnDrawing(PlanarTerrainBlock block, ref Matrix4d world)
		{
		}

		/// <summary>
		/// The terrain's heightmap has been modified and its changes have been accepted into the core.
		/// This is then called on each attached module to give it an opportunity to affect it.
		/// </summary>
		/// <param name="area"></param>
		public virtual void OnHeightModified(PlanarTerrainBlock block, ref Box2i area)
		{
		}

		/// <summary>
		/// This is called by <see cref="Terrain.PostOpaqueRender"/>. The opaque scene has been drawn, and if the
		/// module can extract information from it (like occlusion queries), this is the time.
		/// </summary>
		public virtual void PostOpaqueRender(ref Matrix4d world, ref Matrix4d view, ref Matrix4d projection)
		{
		}

		/// <summary>
		/// If this is true (the default is false), <see cref="NodeClip"/> is valid and should be called.
		/// </summary>
		public virtual bool SupportsNodeClip { get { return false; } }

		/// <summary>
		/// If this is true, <see cref="NodeLodBlend"/> is valid and should be called.
		/// </summary>
		public virtual bool SupportsNodeLodBlend { get { return false; } }

		/// <summary>
		/// This is called by <see cref="Terrain.ResetMetrics"/>. Reset any metrics that have been gathered to zero.
		/// </summary>
		public virtual void ResetMetrics() { }

		#endregion
	}
}
