using Glare.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glare.Graphics.Terrains.Planar
{
	/// <summary>
	/// <see cref="Terrain"/> have a quadtree hierarchy for culling and LOD; this is a leaf or branch in that hierarchy.
	/// </summary>
	public class TerrainTreeNode
	{
		const int TerrainBlockSize = 16;

		/// <summary>
		/// Generate the tree node.
		/// </summary>
		/// <param name="terrainBlock">The terrain this is a node within.</param>
		/// <param name="corner">The integer coordinates of the top-left corner of the tree node.</param>
		/// <param name="size">The size in cells of this tree node.</param>
		/// <param name="lod">The level of detail of this node. This decreases for each step down into the terrain until it reaches zero.</param>
		public TerrainTreeNode(PlanarTerrainBlock terrainBlock, Vector2i corner, int size, int lod)
		{
			if (size < TerrainBlockSize)
				throw new Exception("Invalid terrain size - must be a power of 2 greater than " + TerrainBlockSize + ".");

			this.terrainBlock = terrainBlock;
			pLod = lod;
			pTexelCorner = new Vector2d(corner.X, corner.Y) / Terrain.BlockSize;
			pTexelSize = size / (double)Terrain.BlockSize;
			pBox.Min = new Vector3d(corner.X, 0, corner.Y);
			pBox.Max = new Vector3d(corner.X + size, 0, corner.Y + size);
			pVertexCorner = new Vector2i(corner.X / TerrainBlockSize, corner.Y / TerrainBlockSize);

			if (size > TerrainBlockSize)
			{
				var hsize = size / 2;
				pChildTopLeft = new TerrainTreeNode(terrainBlock, corner, hsize, lod - 1);
				pChildTopRight = new TerrainTreeNode(terrainBlock, new Vector2i(corner.X + hsize, corner.Y), hsize, lod - 1);
				pChildBottomLeft = new TerrainTreeNode(terrainBlock, new Vector2i(corner.X, corner.Y + hsize), hsize, lod - 1);
				pChildBottomRight = new TerrainTreeNode(terrainBlock, new Vector2i(corner.X + hsize, corner.Y + hsize), hsize, lod - 1);
			}
		}

		#region Backing fields

		/// <summary>Backing field for the <see cref="TerrainBlock"/> property.</summary>
		PlanarTerrainBlock terrainBlock;

		/// <summary>Backing field for the <see cref="TexelCorner"/> property.</summary>
		Vector2d pTexelCorner;

		/// <summary>Backing field for the <see cref="TexelSize"/> property.</summary>
		double pTexelSize;

		/// <summary>Backing field for the <see cref="VertexCorner"/> property.</summary>
		Vector2i pVertexCorner;

		/// <summary>Backing field for the <see cref="Lod"/> property.</summary>
		int pLod;

		/// <summary>Backing field for the <see cref="ChildTopLeft"/> property.</summary>
		TerrainTreeNode pChildTopLeft;

		/// <summary>Backing field for the <see cref="ChildTopRight"/> property.</summary>
		TerrainTreeNode pChildTopRight;

		/// <summary>Backing field for the <see cref="ChildBottomLeft"/> property.</summary>
		TerrainTreeNode pChildBottomLeft;

		/// <summary>Backing field for the <see cref="ChildBottomRight"/> property.</summary>
		TerrainTreeNode pChildBottomRight;

		/// <summary>Backing field for the <see cref="Box"/> property.</summary>
		internal Box3d pBox;

		#endregion // Backing fields

		/// <summary>
		/// Get the <see cref="PlanarTerrainBlock"/> this is a node within.
		/// </summary>
		public PlanarTerrainBlock TerrainBlock { get { return terrainBlock; } }

		/// <summary>Get the <see cref="PlanarTerrain"/> that this <see cref="TerrainBlock"/> is in.</summary>
		public PlanarTerrain Terrain { get { return terrainBlock.Terrain; } }

		/// <summary>Get the top-left corner of the first terrain vertex of the tree node in texel coordinates (in the range [0, 1]).</summary>
		public Vector2d TexelCorner { get { return pTexelCorner; } }

		/// <summary>Get the width and height of the tree node in texel coordinates (in the range [0, 1]).</summary>
		public double TexelSize { get { return pTexelSize; } }

		/// <summary>Get the top-left corner of the tree node in cells.</summary>
		public Vector2i VertexCorner { get { return pVertexCorner; } }

		/// <summary>Get the detail level of this node. This is 0 for a leaf node, positive for anything else.</summary>
		public int Lod { get { return pLod; } }

		/// <summary>Get the bounding box that encapsulates the terrain's points within this node's box of interest. If the terrain changes, this will be updated by the <see cref="Terrain.Update"/> method.</summary>
		public Box3d Box { get { return pBox; } }

		/// <summary>
		/// Get the sub-tree node for the top-left quadrant of this tree node, or <c>null</c> if this is a leaf node.
		/// </summary>
		public TerrainTreeNode ChildTopLeft { get { return pChildTopLeft; } }

		/// <summary>
		/// Get the sub-tree node for the top-right quadrant of this tree node, or <c>null</c> if this is a leaf node.
		/// </summary>
		public TerrainTreeNode ChildTopRight { get { return pChildTopRight; } }

		/// <summary>
		/// Get the sub-tree node for the bottom-left quadrant of this tree node, or <c>null</c> if this is a leaf node.
		/// </summary>
		public TerrainTreeNode ChildBottomLeft { get { return pChildBottomLeft; } }

		/// <summary>
		/// Get the sub-tree node for the bottom-right quadrant of this tree node, or <c>null</c> if this is a leaf node.
		/// </summary>
		public TerrainTreeNode ChildBottomRight { get { return pChildBottomRight; } }

		/// <summary>
		/// Get whether this is a leaf node or a tree node.
		/// </summary>
		public bool IsLeaf { get { return Lod == 0; } }

		class LeafEnumerator : IEnumerator<TerrainTreeNode>
		{
			readonly Stack<TerrainTreeNode> Stack = new Stack<TerrainTreeNode>();
			TerrainTreeNode Start;
			TerrainTreeNode pCurrent;

			public LeafEnumerator(TerrainTreeNode start)
			{
				Start = start;
				Stack.Push(start);
			}

			public TerrainTreeNode Current { get { return pCurrent; } }

			public void Dispose() { }

			object System.Collections.IEnumerator.Current { get { return pCurrent; } }

			public bool MoveNext()
			{
				pCurrent = null;

				while (Stack.Count > 0)
				{
					TerrainTreeNode current = Stack.Pop();

					if (current.IsLeaf)
					{
						pCurrent = current;
						return true;
					}
					else
					{
						if (!current.IsLeaf)
						{
							Stack.Push(current.ChildTopLeft);
							Stack.Push(current.ChildTopRight);
							Stack.Push(current.ChildBottomLeft);
							Stack.Push(current.ChildBottomRight);
						}
					}
				}

				return false;
			}

			public void Reset()
			{
				Stack.Clear();
				Stack.Push(Start);
				pCurrent = null;
			}
		}

		class LeafEnumerable : IEnumerable<TerrainTreeNode>
		{
			readonly TerrainTreeNode Start;

			public LeafEnumerable(TerrainTreeNode start) { Start = start; }

			public IEnumerator<TerrainTreeNode> GetEnumerator() { return new LeafEnumerator(Start); }
			System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return GetEnumerator(); }
		}

		/// <summary>
		/// Iterate over the leafs of the hierarchy within this tree node.
		/// </summary>
		IEnumerable<TerrainTreeNode> Leafs
		{
			get { return new LeafEnumerable(this); }
		}

		/// <summary>
		/// Update the <see cref="Box"/> value on the basis of data read from a shader.
		/// </summary>
		/// <param name="dirty">The boundaries of the modified area in cells.</param>
		/// <param name="smallHeightData"></param>
		public void Clean(ref Box2i dirty, float[] smallHeightData)
		{
			if (dirty.Min.X > pBox.Max.X || dirty.Min.Y > pBox.Max.Z || dirty.Max.X <= pBox.Min.X || dirty.Max.Y <= pBox.Min.Z)
				return;

			if (IsLeaf)
			{
				var offset = (VertexCorner.X + VertexCorner.Y * (Terrain.BlockSize / 16)) * 2;
				double min = smallHeightData[offset + 0], max = smallHeightData[offset + 1];

				pBox.Min.Y = min;
				pBox.Max.Y = max;
			}
			else
			{
				ChildTopLeft.Clean(ref dirty, smallHeightData);
				ChildTopRight.Clean(ref dirty, smallHeightData);
				ChildBottomLeft.Clean(ref dirty, smallHeightData);
				ChildBottomRight.Clean(ref dirty, smallHeightData);

				pBox.Min.Y = Math.Min(Math.Min(ChildTopLeft.pBox.Min.Y, ChildTopRight.pBox.Min.Y), Math.Min(ChildBottomLeft.pBox.Min.Y, ChildBottomRight.pBox.Min.Y));
				pBox.Max.Y = Math.Max(Math.Max(ChildTopLeft.pBox.Max.Y, ChildTopRight.pBox.Max.Y), Math.Max(ChildBottomLeft.pBox.Max.Y, ChildBottomRight.pBox.Max.Y));
			}
		}

		double DistanceTo(ref Vector3d point)
		{
			return pBox.Distance(ref point);
		}

		/// <summary>Sort the parameters so that smaller values are first. <paramref name="ad"/> is paired to <paramref name="a"/> and so on.</summary>
		static void SortByDepth(ref double ad, ref double bd, ref double cd, ref double dd, ref TerrainTreeNode a, ref TerrainTreeNode b, ref TerrainTreeNode c, ref TerrainTreeNode d)
		{
			if (bd < ad && bd < cd && bd < dd)
			{
				Extensions.Swap(ref ad, ref bd);
				Extensions.Swap(ref a, ref b);
			}
			else if (cd < ad && cd < bd && cd < dd)
			{
				Extensions.Swap(ref ad, ref cd);
				Extensions.Swap(ref a, ref c);
			}
			else if (dd < ad && dd < bd && dd < cd)
			{
				Extensions.Swap(ref ad, ref dd);
				Extensions.Swap(ref a, ref d);
			}

			if (cd < bd && cd < dd)
			{
				Extensions.Swap(ref bd, ref cd);
				Extensions.Swap(ref b, ref c);
			}
			else if (dd < bd && dd < cd)
			{
				Extensions.Swap(ref bd, ref dd);
				Extensions.Swap(ref b, ref d);
			}

			if (cd < dd)
			{
				Extensions.Swap(ref cd, ref dd);
				Extensions.Swap(ref c, ref d);
			}
		}

		bool CheckClip(ref bool fullyContained)
		{
			if (fullyContained)
				return true;
			bool contained = true;

			foreach (var item in Terrain.ComponentsNodeClip)
			{
				var containment = item.NodeClip(this);

				if (containment == Containment.Disjoint)
					return false;
				if (containment == Containment.Intersects)
					contained = false;
			}

			if (contained)
				fullyContained = true;
			return true;
		}

		double LodBlend(double distance)
		{
			double max = 0;

			foreach (var item in Terrain.ComponentsNodeLodBlend)
			{
				double result = item.NodeLodBlend(this, distance);
				if (result == 1)
					return 1;
				max = Math.Max(result, max);
			}

			return max;
			//return distance < 128 * Lod;
		}

		/// <summary>
		/// Add this node to the terrain's batch.
		/// </summary>
		/// <param name="viewPoint">The position that this node might be viewed from.</param>
		public void Batch(ref Vector3d viewPoint)
		{
			Batch(false, ref viewPoint, DistanceTo(ref viewPoint));
		}

		/// <summary>
		/// Add this node to the terrain's batch.
		/// </summary>
		/// <param name="fullyContained">Whether this node is known to be fully contained within the clipping frustums, in which case it and its children do not need to check clipping against it.</param>
		/// <param name="viewPoint">The position that this node is being viewed from.</param>
		/// <param name="distance">Equal to the distance from the <paramref name="viewPoint"/> to the nearest point on this node. This is needed by parent nodes for sorting, so it is calculated there and then passed down.</param>
		public void Batch(bool fullyContained, ref Vector3d viewPoint, double distance)
		{
			if (!CheckClip(ref fullyContained))
				return;

			double lodBlend = LodBlend(distance);
			if (lodBlend >= 1 && !IsLeaf)
			{
				TerrainTreeNode a = ChildTopLeft, b = ChildTopRight, c = ChildBottomLeft, d = ChildBottomRight;
				double ad = ChildTopLeft.DistanceTo(ref viewPoint);
				double bd = ChildTopRight.DistanceTo(ref viewPoint);
				double cd = ChildBottomLeft.DistanceTo(ref viewPoint);
				double dd = ChildBottomRight.DistanceTo(ref viewPoint);

				SortByDepth(ref ad, ref bd, ref cd, ref dd, ref a, ref b, ref c, ref d);

				a.Batch(fullyContained, ref viewPoint, ad);
				b.Batch(fullyContained, ref viewPoint, bd);
				c.Batch(fullyContained, ref viewPoint, cd);
				d.Batch(fullyContained, ref viewPoint, dd);
				return;
			}

			Terrain.AddInstanceArray(TexelCorner.X, TexelCorner.Y, Lod + lodBlend, TexelSize);
		}
	}
}
