using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glare.Graphics.Terrains.Planar.Components
{
	/// <summary>
	/// A module for a <see cref="PlanarTerrain"/> that uses occlusion queries to update.
	/// </summary>
	public class OcclusionQueryTerrainComponent : AttachedTreeTerrainModule<OcclusionQueryTerrainComponent, OcclusionQueryTerrainComponent.AttachedTreeNode>
	{
		#region Constructors

		/// <summary>Initialize the <see cref="OcclusionQueryTerrainComponent"/>.</summary>
		/// <param name="terrain">The <see cref="PlanarTerrain"/> this is to be a <see cref="Module"/> for.</param>
		public OcclusionQueryTerrainComponent(PlanarTerrain terrain)
			: base(terrain)
		{
		}

		#endregion Constructors

		#region Internals

		static GraphicsBuffer UnitCubeVertexBuffer;
		static GraphicsBuffer UnitCubeIndexBuffer;
		internal static readonly ushort[] UnitCubeIndices = new ushort[] { 2, 1, 3, 5, 7, 4, 6, 0, 2, 1, 1, 0, 5, 4, 4, 6, 6, 2, 7, 3 };

		#endregion Internals

		#region Interior classes

		/// <summary>The <see cref="BaseAttachedTreeNode"/> that will be attached to every <see cref="TerrainTreeNode"/> in the <see cref="PlanarTerrain"/>.</summary>
		public class AttachedTreeNode : BaseAttachedTreeNode
		{
			/// <summary>Get the level-of-detail blend state for the given distance.</summary>
			/// <param name="distance">The distance to the node.</param>
			/// <returns>The LOD blend state.</returns>
			public double LodBlend(double distance)
			{
				return 0;
			}
		}

		#endregion Interior classes

		#region Properties

		/// <summary>Get the number of subdivisions that have been prevented due to this metric.</summary>
		public int CullMetric { get; protected internal set; }

		#endregion Properties

		#region Methods

		static void BuildUnitCube(Context graphics, out GraphicsBuffer vertexBuffer, out GraphicsBuffer indexBuffer)
		{
			/*Vector4nb[] vertices = new Vector4nb[8];
			var vertexDeclaration = new VertexDeclaration(new VertexElement(0, VertexElementFormat.Color, VertexElementUsage.Position, 0));
			vertexBuffer = new VertexBuffer(graphics, vertexDeclaration, vertices.Length, BufferUsage.WriteOnly);

			for (int i = 0; i < 8; i++)
				vertices[i] = new Vector4nb((i & 1) != 0 ? 1 : 0, (i & 2) != 0 ? 1 : 0, (i & 4) != 0 ? 1 : 0, 1);
			vertexBuffer.SetData(vertices);

			ushort[] indices = UnitCubeIndices;
			indexBuffer = new IndexBuffer(graphics, IndexElementSize.SixteenBits, indices.Length, BufferUsage.WriteOnly);
			indexBuffer.SetData(indices);*/
			throw new NotImplementedException();
		}

		/// <summary>Get the level-of-detail blending for the given <see cref="TerrainTreeNode"/>.</summary>
		/// <param name="node">The <see cref="TerrainTreeNode"/> to test.</param>
		/// <param name="distance">The distance to the <see cref="TerrainTreeNode"/>.</param>
		/// <returns>The level-of-detail blending.</returns>
		public override double NodeLodBlend(TerrainTreeNode node, double distance)
		{
			return GetAttachment(node).LodBlend(distance);
		}

		static void PrepareUnitCube(Context graphics)
		{
			if (UnitCubeVertexBuffer == null)
				BuildUnitCube(graphics, out UnitCubeVertexBuffer, out UnitCubeIndexBuffer);
			throw new NotImplementedException();
			//graphics.SetVertexBuffer(UnitCubeVertexBuffer);
			//graphics.Indices = UnitCubeIndexBuffer;
		}

		public override void PostOpaqueRender(ref Matrix4d world, ref Matrix4d view, ref Matrix4d projection)
		{
			base.PostOpaqueRender(ref world, ref view, ref projection);
			var oldBlendState = Graphics.DrawBuffers[0].Blend;
			//var oldDepthStencilState = context.DepthStencilState;
			var effect = Terrain.Program; // OcclusionEffect

			/*context.DrawBuffers[0].Blend = new BlendState() { ColorWriteChannels = ColorWriteChannels.None };
			context.DepthStencilState = DepthStencilState.DepthRead;

			PrepareUnitCube(context);
			context.SetVertexBuffer(UnitCubeVertexBuffer);
			context.Indices = UnitCubeIndexBuffer;

			effect.Parameters["World"].SetValue(world * view * projection);

			context.BlendState = oldBlendState;
			context.DepthStencilState = oldDepthStencilState;*/
			throw new NotImplementedException();
		}

		/// <summary>Reset all metrics that are being recorded.</summary>
		public override void ResetMetrics()
		{
			CullMetric = 0;
			base.ResetMetrics();
		}

		public override bool SupportsNodeLodBlend { get { return true; } }

		#endregion Methods

#if false
        OcclusionQuery Query;

                if(Query != null && distance != 0 && Terrain.OcclusionQueryEnabled) {
                    discard = Query.IsComplete && Query.PixelCount < 1000;
                    if(discard)
                        Terrain.OcclusionQueryCullMetric++;
                }


            OcclusionQueryCullMetric = 0;

         // post-render
            if(OcclusionQueryEnabled)
                Tree.Occlude(frustum, ref viewPoint);



        public void Occlude(BoundingBoxContainmentFunction frustum, ref Vector3 viewPoint) {
            float distance = DistanceTo(ref viewPoint);
            if(!CheckFrustum(ref frustum))
                return;
            if(ShouldDivide(distance)) {
                if(distance != 0) {
                    var device = Terrain.GraphicsContext;

                    Terrain.OcclusionEffect.Parameters["View"].SetValue(Matrix.CreateScale(pBox.Max - pBox.Min) * Matrix.CreateTranslation(pBox.Min));

                    if(Query == null)
                        Query = new OcclusionQuery(Terrain.GraphicsContext);
                    var complete = Query.IsComplete;
                    Query.Begin();
                    foreach(var pass in Terrain.OcclusionEffect.CurrentTechnique.Passes) {
                        pass.Apply();
                        device.DrawIndexedPrimitives(PrimitiveType.TriangleStrip, 0, 0, 8, 0, Terrain.UnitCubeIndices.Length - 2);
                    }
                    Query.End();
                }
            }

            if(!IsLeaf) {
                TopLeft.Occlude(frustum, ref viewPoint);
                TopRight.Occlude(frustum, ref viewPoint);
                BottomLeft.Occlude(frustum, ref viewPoint);
                BottomRight.Occlude(frustum, ref viewPoint);
            }
        }
#endif
	}
}
