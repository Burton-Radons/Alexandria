using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glare.Graphics.Terrains.Planar
{
	/// <summary>
	/// A section of a <see cref="PlanarTerrain"/>.
	/// </summary>
	public class PlanarTerrainBlock
	{
		readonly Vector2i blockIndex;

		/// <summary>Backing field for the <see cref="ColorTexture"/> property.</summary>
		Texture2D colorTexture;

		/// <summary>Backing field for the <see cref="NormalTexture"/> property</summary>
		Texture2D normalTexture;

		/// <summary>Backing field for the <see cref="HeightTexture"/> property.</summary>
		Texture2D heightTexture;

		readonly PlanarTerrain terrain;

		/// <summary>The root of the terrain tree, backing field for the <see cref="TreeRoot"/> property.</summary>
		TerrainTreeNode treeRoot;

		public Vector2i BlockIndex { get { return blockIndex; } }

		/// <summary>
		/// Get or set the color texture. The color texture is not provided by the <see cref="Terrain"/> itself,
		/// but could be provided by the user or maintained by the <see cref="ColorLayerTerrainModule"/>. If
		/// assigned, it will be used by the default terrain effect for per-pixel colouring.</summary>
		/// </summary>
		public Texture2D ColorTexture
		{
			get { return colorTexture; }

			set
			{
				colorTexture = value;
				terrain.Program.Uniforms["ColorTexture"].Set(value);
				terrain.Program.Uniforms["ColorTextureStrength"].Set(value != null ? 1 : 0);
			}
		}

		/// <summary>The area of the terrain that is dirty, or <c>null</c> for none.</summary>
		Box2i? DirtyTreeArea;

		/// <summary>Get the height texture, which has a single float channel. This can be provided by the <see cref="HeightTerrainComponent"/>.</summary>
		public Texture2D HeightTexture { get { return heightTexture; } set { heightTexture = value; } }

		/// <summary>
		/// Get or set the normal texture. The normal texture is not provided by the <see cref="PlanarTerrain"/> itself,
		/// but could be provided by the user or maintained by the <see cref="NormalLayerTerrainModule"/>. If
		/// assigned, it will be used by the default terrain effect for per-pixel normals.</summary>
		public Texture2D NormalTexture
		{
			get { return normalTexture; }

			set
			{
				normalTexture = value;
				terrain.Program.Uniforms["normalTexture"].Set(value);
			}
		}

		public PlanarTerrain Terrain { get { return terrain; } }

		/// <summary>Get the root of the terrain's quadtree.</summary>
		public TerrainTreeNode TreeRoot { get { return treeRoot; } }

		public Vector3d WorldOffset { get { return new Vector3d(blockIndex.X * Terrain.BlockSize, 0, blockIndex.Y * Terrain.BlockSize); } }

		internal PlanarTerrainBlock(PlanarTerrain terrain, Vector2i blockIndex)
		{
			this.terrain = terrain;
			this.blockIndex = blockIndex;

			int size = terrain.BlockSize;

			//heightTexture = new Texture2D(device, size, size, true, Format.Single, DepthFormat.None, 0, RenderTargetUsage.PlatformContents);
			//heightBackBuffer = new Texture2D(device, size, size, true, Format.Single, DepthFormat.None, 0, RenderTargetUsage.PlatformContents);

			treeRoot = new TerrainTreeNode(this, new Vector2i(0, 0), size, terrain.DeepestLod);
			DirtyTreeArea = new Box2i(0, 0, size, size);
		}

		public Texture2D AllocateHeightBackBuffer() { return Terrain.GetCachedRenderTarget(Formats.Vector1f); }

		internal void CleanTree()
		{
			if (DirtyTreeArea == null)
				return;
			var area = DirtyTreeArea.Value;
			DirtyTreeArea = null;

			Program program = terrain.Program;

			Graphics.FrameBuffer = terrain.FrameBuffer;

			// Perform the horizontal reduction.
			program.Uniforms["heightTexture"].Set(HeightTexture);
			program.Uniforms["world"].Set(Matrix4f.Scale(16f / terrain.BlockSize, 0, 0));
			program.Uniforms["view"].Set(new Matrix4f(1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1));
			terrain.FrameBuffer.Colors[0].Attach(terrain.NarrowHeightTexture);
			Graphics.Blend = BlendState.Opaque;
			terrain.DrawUnitRectangle("clean");

			// Perform the vertical reduction.
			program.Uniforms["HeightTexture"].Set(terrain.NarrowHeightTexture);
			program.Uniforms["World"].Set(Matrix4d.Scale(0, 16.0 / terrain.BlockSize, 0));
			program.Uniforms["View"].Set(Matrix4d.Identity);
			terrain.FrameBuffer.Colors[0].Attach(terrain.SmallHeightTexture);
			terrain.DrawUnitRectangle("clean");
			Graphics.FrameBuffer = null;

			terrain.SmallHeightTexture.Levels[0].Read(terrain.SmallHeightData);
			treeRoot.Clean(ref area, terrain.SmallHeightData);

			foreach (var module in terrain.Components)
				module.OnHeightModified(this, ref area);
		}

		public void DirtyBackBufferHeightTexture(Texture2D newHeightTexture, Box2i modified)
		{
			if (newHeightTexture == null)
				throw new ArgumentNullException("newHeightTexture");

			throw new NotImplementedException();
			/*if (modified.IsEmpty)
			{
				Terrain.RenderTargetCache.ReturnToPool(newHeightTexture);
				return;
			}

			var heightLayerComponent = Terrain.GetComponent<HeightTerrainComponent>();
			if (heightLayerComponent != null)
				heightLayerComponent.SetTexture(this, newHeightTexture);
			else
			{
				Terrain.RenderTargetCache.ReturnToPool(heightTexture);
				heightTexture = newHeightTexture;
				//Terrain.RenderTargetCache.ReturnToPool(newHeightTexture);
			}

			var fullArea = new Rectangle(0, 0, terrain.BlockSize, terrain.BlockSize);
			Rectangle.Intersect(ref modified, ref fullArea, out modified);
			if (DirtyTreeArea == null)
				DirtyTreeArea = modified;
			else
				DirtyTreeArea = Rectangle.Union(DirtyTreeArea.Value, modified);*/
		}

		public void DirtyBackBufferHeightTexture(Texture2D newHeightTexture, int x, int y, int width, int height)
		{
			DirtyBackBufferHeightTexture(newHeightTexture, new Box2i(x, y, width, height));
		}

		public void DirtyAllHeightBackBuffer(Texture2D newHeightTexture)
		{
			DirtyBackBufferHeightTexture(newHeightTexture, new Box2i(0, 0, terrain.BlockSize, terrain.BlockSize));
		}

		internal void Draw(ref Vector3 viewPoint, ref Matrix4d world, ref Matrix4d view, ref Matrix4d projection)
		{
			Vector3d worldOffset = WorldOffset;
			Matrix4d blockWorld;

			Matrix4d.Translate(ref worldOffset, out blockWorld);
			blockWorld.Multiply(ref world, out blockWorld);

			foreach (TerrainComponent component in Terrain.Components)
				component.OnDrawing(this, ref blockWorld);

			DrawSetParameters("draw", ref blockWorld, ref view, ref projection);
			Draw(terrain.Program, viewPoint, ref blockWorld);
		}

		void Draw(Program program, Vector3 viewPoint, ref Matrix4d world)
		{
			if (program == null)
				throw new ArgumentNullException();
			CleanTree();
			terrain.ClearInstanceArray();

			throw new NotImplementedException();
			/*viewPoint = Vector3.Transform(viewPoint, Matrix.Invert(world));
			treeRoot.Batch(ref viewPoint);

			DrawInstances(program);*/
		}

		internal void DrawInstances(Program program)
		{
			int instanceCount = terrain.FlushInstanceArray();

			if (instanceCount == 0)
				return;

			throw new NotImplementedException();
			/*
			terrain.metrics.TrianglesDrawn += program.CurrentTechnique.Passes.Count * (terrain.BlockIndexBuffer.IndexCount - 2) * instanceCount;
			terrain.metrics.BlocksDrawn += instanceCount;

			context.SetVertexBuffers(
				new VertexBufferBinding(terrain.BlockVertexBuffer, 0, 0),
				new VertexBufferBinding(terrain.InstanceVertexBuffer, 0, 1));
			context.Indices = terrain.BlockIndexBuffer;

			foreach (var pass in program.CurrentTechnique.Passes)
			{
				pass.Apply();
				var primitive = PrimitiveType.TriangleStrip;

				//primitive = PrimitiveType.LineStrip;

				context.DrawInstancedPrimitives(primitive, 0, 0, terrain.BlockVertexBuffer.VertexCount, 0, terrain.BlockIndexBuffer.IndexCount - 2, instanceCount);
			}
			context.SetVertexBuffer(null);
			context.Indices = null;*/
		}

		void DrawSetParameters(string shaderName, ref Matrix4d world, ref Matrix4d view, ref Matrix4d projection)
		{
			var effect = terrain.Program;
			terrain.SetShader(shaderName);
			effect.Uniforms["world"].Set(world);
			effect.Uniforms["view"].Set(view);
			effect.Uniforms["projection"].Set(projection);
			effect.Uniforms["heightTexture"].Set(HeightTexture);
		}

		internal void DrawWater(Texture2D waterOffsetHeightTexture, ref Vector3d waterColor, ref Vector3d viewPoint, ref Matrix4d world, ref Matrix4d view, ref Matrix4d projection)
		{
			throw new NotImplementedException();
			/*var effect = terrain.Effect;
			DrawSetParameters("DrawWater", ref world, ref view, ref projection);
			effect.Parameters["WaterHeightTexture"].SetValue(waterOffsetHeightTexture);
			effect.Parameters["WaterColor"].SetValue(waterColor);
			Draw(effect, viewPoint, ref world);*/
		}

	}
}
