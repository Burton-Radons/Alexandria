using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glare.Graphics.Terrains.Planar.Components
{
	public class AttachedTreeTerrainModule<TModule, TAttachedNode> : TerrainComponent
		where TModule : AttachedTreeTerrainModule<TModule, TAttachedNode>
		where TAttachedNode : AttachedTreeTerrainModule<TModule, TAttachedNode>.BaseAttachedTreeNode, new()
	{
		public AttachedTreeTerrainModule(PlanarTerrain terrain) : base(terrain) { }

		public class BaseAttachedTreeNode
		{
			public TModule Module { get; internal set; }
			public PlanarTerrain Terrain { get { return Node.Terrain; } }
			public TerrainTreeNode Node { get; internal set; }
		}

		protected Dictionary<TerrainTreeNode, TAttachedNode> Attachments = new Dictionary<TerrainTreeNode, TAttachedNode>();

		protected TAttachedNode GetAttachment(TerrainTreeNode node)
		{
			TAttachedNode result;

			if (!Attachments.TryGetValue(node, out result))
				result = Attachments[node] = new TAttachedNode() { Module = (TModule)this, Node = node };
			return result;
		}
	}
}
