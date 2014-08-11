using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glare.Graphics.Terrains.Planar.Components {
	/// <summary>
	/// Provides a heightmap layer to the terrain.
	/// </summary>
	public class HeightTerrainComponent : LayerTerrainComponent {
		public HeightTerrainComponent(PlanarTerrain terrain, Format format)
			: base(terrain, format) {
			foreach (PlanarTerrainBlock block in terrain.Blocks)
				block.HeightTexture = Get(block);
		}

		public HeightTerrainComponent(PlanarTerrain terrain) : this(terrain, Formats.Vector1f) { }

		public override void SetTexture(PlanarTerrainBlock block, Texture2D value) {
			base.SetTexture(block, value);
			block.HeightTexture = value;
		}

		public override void OnBlockAdded(PlanarTerrainBlock block) {
			base.OnBlockAdded(block);
			block.HeightTexture = Get(block);
		}
	}
}
