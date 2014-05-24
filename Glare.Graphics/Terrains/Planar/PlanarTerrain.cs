using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Glare.Internal;
using Glare.Graphics.Rendering;
using Glare.Framework;

namespace Glare.Graphics.Terrains.Planar {
	/// <summary>
	/// A two-dimensional terrain made up of a height map.
	/// Additional capabilities come from <see cref="TerrainComponent"/>s.
	/// 
	/// The <see cref="PlanarTerrain"/> is rendered as a set of blocks that are scaled to
	/// different levels based upon input from <see cref="TerrainComponent"/>s that provide
	/// that information, like the <see cref="DistanceLodTerrainModule"/> and
	/// <see cref="OcclusionQueryTerrainModule"/>.
	/// </summary>
	public class PlanarTerrain : Terrain {
		#region Static Fields

		/// <summary>The number of points across that the top of a block is.</summary>
		const int BlockEdgeCount = 17;

		/// <summary>Get the total number of indices in a block.</summary>
		const int BlockIndexCount = BlockVertexRow * (BlockVertexRow - 1) * 2 + (BlockVertexRow - 1) * 2;

		/// <summary>The number of points across that a block is, including the skirt.</summary>
		const int BlockVertexRow = BlockEdgeCount + 2;

		/// <summary>The total number of vertices in a block.</summary>
		const int BlockVertexCount = BlockVertexRow * BlockVertexRow;

		const int InstanceVertexElementCount = 2;

		static readonly Vector4nb[] UnitVertices = new Vector4nb[] { new Vector4nb(0, 0, 0, 1), new Vector4nb(0, 1, 0, 1), new Vector4nb(1, 0, 0, 1), new Vector4nb(1, 1, 0, 1) };

		readonly GraphicsBuffer UnitBuffer;

		/*static readonly VertexDeclaration InstanceVertexDeclaration = new VertexDeclaration(
			new VertexElement(0, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 0),
			new VertexElement(16, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 1));*/

		/// <summary>The vertex declaration for a block's vertices.</summary>
		//static readonly VertexDeclaration Byte4VertexDeclaration = new VertexDeclaration(new VertexElement(0, VertexElementFormat.Byte4, VertexElementUsage.Position, 0));

		#endregion Static Fields

		#region Fields

		/// <summary>This contains the indices and vertices for the blocks. First are <see cref="BlockIndexCount"/> <c>ushort</c> indices for a <see cref="Primitive.TriangleStrip"/>, followed by the <see cref="BlockVertexCount"/> <see cref="Vector4b"/> vertices. The shape they produce is a table, with a flat top that is <see cref="BlockEdgeCount"/> wide and a skirt running below the edge that hides inter-LOD seams.</summary>
		readonly GraphicsBuffer BlockBuffer;

		readonly Dictionary<Vector2i, PlanarTerrainBlock> BlockDictionary = new Dictionary<Vector2i, PlanarTerrainBlock>();

		/// <summary>Mutable backing field of <see cref="Components"/>.</summary>
		readonly internal List<TerrainComponent> ComponentsMutable = new List<TerrainComponent>();

		/// <summary>
		/// The list of attached modules which have indicated through returning <c>true</c> from <see cref="TerrainComponent.SupportsNodeClip"/> that they support the <see cref="TerrainComponent.NodeClip"/> method.
		/// </summary>
		readonly internal List<TerrainComponent> ComponentsNodeClip = new List<TerrainComponent>();

		/// <summary>
		/// The list of attached modules which have indicated through returning <c>true</c> from <see cref="TerrainComponent.SupportsNodeLodBlend"/> that they support the <see cref="TerrainComponent.NodeLodBlend"/> method.
		/// </summary>
		readonly internal List<TerrainComponent> ComponentsNodeLodBlend = new List<TerrainComponent>();

		Codex<Vector4f> InstanceArray = new Codex<Vector4f>();

		internal GraphicsBuffer InstanceVertexBuffer;

		/// <summary>The terrain effect and technique-specific copies for other purposes.</summary>
		internal readonly Program Program;

		internal TerrainMetrics MetricsField;

		/// <summary>The <see cref="HeightTexture"/> with a 16th the width, and two float channels. This is the render target for the first pass of the min/max boundary box function.</summary>
		internal readonly Texture2D NarrowHeightTexture;

		/// <summary>Data retrieved from the <see cref="SmallHeightTexture"/>.</summary>
		internal readonly float[] SmallHeightData;

		/// <summary>
		/// The <see cref="HeightTexture"/> with a 16th the width and height, and two float channels.
		/// This is the render target for the second pass of the min/max boundary box function.
		/// </summary>
		internal readonly Texture2D SmallHeightTexture;

		#endregion Fields

		#region Properties

		public Dictionary<Vector2i, PlanarTerrainBlock>.ValueCollection Blocks { get { return BlockDictionary.Values; } }

		/// <summary>Get the size of an edge of the terrain.</summary>
		public int BlockSize { get; private set; }

		/// <summary>Get the list of <see cref="TerrainComponent"/> attached to the terrain.
		/// This list is not affected directly, but by calling the <see cref="TerrainComponent.Attach"/> or
		/// <see cref="TerrainComponent.Detach"/> methods, or by using a constructor in the <see cref="TerrainComponent"/>
		/// implementation that takes a function and says it will perform the same action.</summary>
		public ReadOnlyCollection<TerrainComponent> Components { get { return new ReadOnlyCollection<TerrainComponent>(ComponentsMutable); } }

		public TerrainMetrics Metrics { get { return MetricsField; } }

		#endregion Properties

		public PlanarTerrain(int blockSize)
			: base() {
			//if (terrainEffect == null) throw new ArgumentNullException("terrainEffect");

			BlockSize = blockSize;

			NarrowHeightTexture = new Texture2D(Formats.Vector2f, blockSize / 16, blockSize);
			SmallHeightTexture = new Texture2D(Formats.Vector2f, blockSize / 16, blockSize / 16);
			SmallHeightData = new float[(blockSize / 16) * (blockSize / 16) * 2];

			BlockBuffer = CreateBlockBuffer();

			Texture2D randomTexture = Texture.CreateRandom4nb(256, 256);

			var builder = ShaderBuilder.CreateFromAssemblyResource("Glare.Graphics.Shaders.Terrain.glsl");
			Program = new Program(
				builder.VertexShader("Common", "Vertex"),
				builder.FragmentShader("Common", "Fragment"));
			Program.Uniforms["terrainSize"].Set(blockSize);
			Program.Uniforms["randomTexture"].Set(randomTexture);

			UnitBuffer = GraphicsBuffer.Create(UnitVertices);
		}

		internal void AddInstanceArray(double x, double y, double lodBlend, double scale) {
			InstanceArray.Add(new Vector4f((float)x, (float)y, (float)lodBlend, (float)scale));
			InstanceArray.Add(new Vector4f(0, 0, 0, 0));
		}

		/// <summary>
		/// Recreate the bounding box hierarchy in the <see cref="DirtyTreeArea"/> region.
		/// If that is null, nothing is done. This also calls <see cref="TerrainComponent.OnHeightModified"/> on each of the <see cref="Components"/>.
		/// </summary>
		void CleanTree() {
			foreach (PlanarTerrainBlock block in BlockDictionary.Values)
				block.CleanTree();
		}

		internal void ClearInstanceArray() {
			InstanceArray.Clear();
		}

		GraphicsBuffer CreateBlockBuffer() {
			GraphicsBuffer buffer = new GraphicsBuffer(BlockIndexCount * 2 + BlockVertexCount * 4);
			CreateBlockIndexBuffer(buffer, 0);
			CreateBlockVertexBuffer(buffer, BlockIndexCount * 2);
			return buffer;
		}

		/// <summary>Create the <see cref="BlockIndexBuffer"/>.</summary>
		void CreateBlockIndexBuffer(GraphicsBuffer buffer, int offset) {
			ushort[] indices = new ushort[BlockIndexCount];
			for (int z = 0, i = 0, l = 0; z < BlockVertexRow - 1; z++) {
				for (int x = 0; x < BlockVertexRow; x++) {
					l = indices[i++] = (ushort)(x + (z + 1) * BlockVertexRow);
					if (x == 0 && z > 0) indices[i++] = (ushort)l;
					l = indices[i++] = (ushort)(x + z * BlockVertexRow);
				}
				if (z < BlockVertexRow - 2) indices[i++] = (ushort)l;
			}
			buffer.Write(offset, indices);
		}

		/// <summary>
		/// Create the block <see cref="BlockVertexBuffer"/>.
		/// </summary>
		void CreateBlockVertexBuffer(GraphicsBuffer buffer, int offset) {
			Vector4b[] vertices = new Vector4b[BlockVertexCount];

			for (int z = 0, i = 0; z < BlockVertexRow; z++) {
				int pz = Math.Max(0, Math.Min(BlockEdgeCount - 1, z - 1));

				for (int x = 0; x < BlockVertexRow; x++) {
					int px = Math.Max(0, Math.Min(BlockEdgeCount - 1, x - 1));
					int py = (x == 0 || z == 0 || x == BlockVertexRow - 1 || z == BlockVertexRow - 1) ? 0 : 1;
					vertices[i++] = new Vector4b((byte)px, (byte)py, (byte)pz, 0);
				}
			}

			buffer.Write(offset, vertices);
		}

		public int DeepestLod { get { return Math.Max(0, (int)Math.Log(BlockSize, 2) - 4); } }

		public void Draw(Vector3 viewPoint, Matrix4d world, Matrix4d view, Matrix4d projection) {
			foreach (PlanarTerrainBlock block in BlockDictionary.Values)
				block.Draw(ref viewPoint, ref world, ref view, ref projection);
		}

		public void DrawWater(Texture2D waterOffsetHeightTexture, Vector3d waterColor, Vector3d viewPoint, Matrix4d world, Matrix4d view, Matrix4d projection) {
			foreach (PlanarTerrainBlock block in BlockDictionary.Values)
				block.DrawWater(waterOffsetHeightTexture, ref waterColor, ref viewPoint, ref world, ref view, ref projection);
		}

		/// <summary>Draw the unit rectangle (0, 0, 0) to (1, 1, 0), using the specified <see cref="Program"/>.</summary>
		internal void DrawUnitRectangle(string shader) {
			SetShader(shader);
			Program.Attributes["vertex"].Bind(UnitBuffer, 0, Formats.Vector4nb, 0);
			Program.Draw(Primitive.TriangleStrip, 4);
			//GraphicsContext.SetVertexBuffer(UnitVertexBuffer);
			/*foreach (var pass in effect.CurrentTechnique.Passes)
			{
				pass.Apply();
				Context.DrawUserPrimitives(PrimitiveType.TriangleStrip, UnitVertices, 0, 2, Byte4VertexDeclaration);
			}*/
		}

		internal int FlushInstanceArray() {
			throw new NotImplementedException();
			/*int count = InstanceArray.Count;
			if (count > 0)
			{
				if (InstanceVertexBuffer == null || InstanceVertexBuffer.VertexCount < InstanceArray.Count)
				{
					if (InstanceVertexBuffer != null)
						InstanceVertexBuffer.Dispose();
					InstanceVertexBuffer = new GraphicsBuffer(Context, InstanceVertexDeclaration, count * 3 / 2, BufferUsage.WriteOnly);
				}

				InstanceVertexBuffer.SetData(InstanceArray.Array, 0, count, SetDataOptions.Discard);
				ClearInstanceArray();
			}
			return count / InstanceVertexElementCount;*/
		}

		/// <summary>Get the <see cref="PlanarTerrainBlock"/> at the given block indices, creating it if necessary.</summary>
		/// <param name="blockIndexX">The horizontal block index.</param>
		/// <param name="blockIndexY">The vertical block index.</param>
		/// <returns>The <see cref="PlanarTerrainBlock"/>.</returns>
		public PlanarTerrainBlock GenerateBlock(int blockIndexX, int blockIndexY) {
			return GenerateBlock(new Vector2i(blockIndexX, blockIndexY));
		}

		/// <summary>Get the <see cref="PlanarTerrainBlock"/> at the given block indices, creating it if necessary.</summary>
		/// <param name="blockIndex">The block index.</param>
		/// <returns>The <see cref="PlanarTerrainBlock"/>.</returns>
		public PlanarTerrainBlock GenerateBlock(Vector2i blockIndex) {
			PlanarTerrainBlock block;

			if (!BlockDictionary.TryGetValue(blockIndex, out block)) {
				block = BlockDictionary[blockIndex] = new PlanarTerrainBlock(this, blockIndex);
				foreach (TerrainComponent component in ComponentsMutable)
					component.OnBlockAdded(block);
			}
			return block;
		}

		public Texture2D GetCachedRenderTarget() {
			return GetCachedRenderTarget(Formats.Vector4nb);
		}

		public Texture2D GetCachedRenderTarget(Format format) {
			throw new NotImplementedException();
			//return RenderTargetCache.GetRenderTarget(BlockSize, BlockSize, format);
		}

		public TComponent GetComponent<TComponent>() where TComponent : TerrainComponent {
			foreach (TerrainComponent component in Components)
				if (component is TComponent)
					return (TComponent)component;
			return null;
		}

		/// <summary>
		/// This should be called after scene rendering of opaque geometry has been completed.
		/// The terrain itself does nothing with this, but terrain modules can add functionality
		/// like updating occlusion culling data.
		/// </summary>
		public void PostOpaqueRender(Matrix4d world, Matrix4d view, Matrix4d projection) {
			throw new NotImplementedException();
			/*RenderTargetCache.Cleanup();
			foreach (var module in MutableComponents)
				module.PostOpaqueRender(ref world, ref view, ref projection);*/
		}

		/// <summary>Zero all metrics in the terrain and in the <see cref="Components"/>.</summary>
		public void ResetMetrics() {
			MetricsField.Reset();
			foreach (var module in ComponentsMutable)
				module.ResetMetrics();
		}

		internal void SetShader(string shader) {
			Program.VertexStage.Uniforms["vertexShader"].Set(shader + "VertexShader");
			Program.FragmentStage.Uniforms["fragmentShader"].Set(shader + "FragmentShader");
		}

		/// <summary>
		/// Update any data structures that have become out-of-date by modifications to the
		/// terrain. If the terrain is static, this is pointless (but harmless). This may
		/// cause the render target to change, which will clear the main buffer when reset.
		/// So don't call this in the middle of rendering.
		/// </summary>
		public void Update() {
			CleanTree();
		}
	}
}
