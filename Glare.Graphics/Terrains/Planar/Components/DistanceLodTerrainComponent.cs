using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glare.Graphics.Terrains.Planar.Components
{
	/// <summary>
	/// A <see cref="TerrainComponent"/> that blends between LOD levels based on the distance of the viewer from the terrain node.
	/// </summary>
	public class DistanceLodTerrainComponent : TerrainComponent
	{
		public DistanceLodTerrainComponent(PlanarTerrain terrain)
			: base(terrain)
		{
		}

		double TriggerDistance(int lod)
		{
			return lod * 256;
		}

		public override double NodeLodBlend(TerrainTreeNode node, double distance)
		{
			double next = TriggerDistance(node.Lod);

			if (distance < next)
				return 1;
			double previous = TriggerDistance(node.Lod + 1);

			return 1 - (distance - next) / (previous - next);
		}

		public override bool SupportsNodeLodBlend { get { return true; } }
	}
}
