using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glare.Graphics.Terrains.Planar.Components
{
	/// <summary>Provides a color layer to the terrain.</summary>
	public class ColorTerrainComponent : LayerTerrainComponent
	{
		public ColorTerrainComponent(PlanarTerrain terrain, Format format) : base(terrain, format) { }

		public override void OnBlockAdded(PlanarTerrainBlock block)
		{
			base.OnBlockAdded(block);
			block.ColorTexture = Get(block);
		}

		protected override void OnDetach()
		{
			base.OnDetach();
			foreach (PlanarTerrainBlock block in Terrain.Blocks)
				block.ColorTexture = null;
		}
	}
}
