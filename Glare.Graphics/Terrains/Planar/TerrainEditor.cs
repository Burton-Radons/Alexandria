using Glare.Graphics.Rendering;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glare.Graphics.Terrains.Planar
{
	/// <summary>
	/// Provides editing of a <see cref="Terrain"/>, using the GPU to accelerate all operations.
	/// </summary>
	public class TerrainEditor
	{
		public const string EffectName = "Terracotta/TerrainEditorEffect";

		PlanarTerrainBlock terrainBlock;

		Program Program;

		public Matrix4d PerlinTransform
		{
			get { return Program.Uniforms["PerlinTransform"].GetMatrix4d(); }
			set { Program.Uniforms["PerlinTransform"].Set(value); }
		}

		public PlanarTerrain Terrain { get { return TerrainBlock.Terrain; } }

		public PlanarTerrainBlock TerrainBlock
		{
			get { return terrainBlock; }

			set
			{
				if (value == null)
					throw new ArgumentNullException("value");
				if (terrainBlock.Terrain.BlockSize != Terrain.BlockSize)
					throw new ArgumentException("Terrains must match in the size of their blocks.");
				terrainBlock = value;
			}
		}

		Random Rng;
		Texture2D TemporaryTexture;
		Vector4nb[] TemporaryData;

		const int PerlinSize = 256;

		/// <summary>
		/// Initialise the editor.
		/// </summary>
		/// <param name="terrainEditorProgram">The terrain editor effect to clone. In the default content, this is stored as "Terracotta/TerrainEditorEffect".</param>
		/// <param name="terrain">The terrain to edit.</param>
		public TerrainEditor(PlanarTerrainBlock block)
		{
			this.terrainBlock = block;

			var builder = ShaderBuilder.CreateFromAssemblyResource("Glare.Graphics.Shaders.TerrainEditor.glsl");
			Program = new Program(
				builder.VertexShader("Common", "Vertex"),
				builder.FragmentShader("Common", "Fragment"));

			Program.Uniforms["TerrainSize"].Set(Terrain.BlockSize);
			Program.Uniforms["InverseTerrainSize"].Set(1.0 / Terrain.BlockSize);
			Rng = new Random();

			byte[] permutations = new byte[PerlinSize];
			for (int i = 0; i < permutations.Length; i++)
				permutations[i] = (byte)i;
			for (int i = 0; i < permutations.Length; i++)
				Extensions.Swap(ref permutations[i], ref permutations[Rng.Next(permutations.Length)]);

			CreatePerlinPermutationTexture(permutations);
			CreatePerlinGradientTexture(permutations);
			CreateTemporaryTexture();
			//LoadRandomPerlinTransform();
			PerlinTransform = Matrix4d.Identity;
		}

		void CreateTemporaryTexture()
		{
			var texture = TemporaryTexture = new Texture2D(Formats.Vector4nb, 256, 256);
			Program.Uniforms["TemporaryTexture"].Set(texture);
			Program.Uniforms["TemporarySize"].Set(TemporaryTexture.Dimensions);
			Program.Uniforms["TemporaryInverseSize"].Set(Vector2d.One / (Vector2d)TemporaryTexture.Dimensions);
			TemporaryData = new Vector4nb[TemporaryTexture.Dimensions.Product];
		}

		void CreatePerlinPermutationTexture(byte[] permutations)
		{
			Vector4b[] data = new Vector4b[256 * 256];
			for (int y = 0; y < 256; y++)
				for (int x = 0; x < 256; x++)
				{
					int a = permutations[x] + y;
					int AA = permutations[a % 256];
					int AB = permutations[(a + 1) % 256];
					int B = permutations[(x + 1) % 256] + y;
					int BA = permutations[B % 256];
					int BB = permutations[(B + 1) % 256];
					data[x + y * 256] = new Vector4b((byte)AA, (byte)AB, (byte)BA, (byte)BB);
				}

			var texture = new Texture2D();
			texture.Data(256, 256, Formats.Vector4nb, data);
			Program.Uniforms["PerlinPermutationTexture"].Set(texture);
		}

		void CreatePerlinGradientTexture(byte[] permutations)
		{
			Vector4nsb[] gradients = new Vector4nsb[] {
				new Vector4nsb(1, 1, 0, 0), new Vector4nsb(-1, 1, 0, 0), new Vector4nsb(1, -1, 0, 0), new Vector4nsb(-1, -1, 0, 0),
				new Vector4nsb(1, 0, 1, 0), new Vector4nsb(-1, 0, 1, 0), new Vector4nsb(1, 0, -1, 0), new Vector4nsb(-1, 0, -1, 0),
				new Vector4nsb(0, 1, 1, 0), new Vector4nsb(0, -1, 1, 0), new Vector4nsb(0, 1, -1, 0), new Vector4nsb(0, -1, -1, 0),
				new Vector4nsb(1, 1, 0, 0), new Vector4nsb(0, -1, 1, 0), new Vector4nsb(-1, 1, 0, 0), new Vector4nsb(0, -1, -1, 0)
			};

			Vector4nsb[] data = new Vector4nsb[256];

#if false
	new NormalizedColor(0, -1, -1, -1),
	new NormalizedColor(0, -1, -1, 1),
	new NormalizedColor(0, -1, 1, -1),
	new NormalizedColor(0, -1, 1, 1),
	new NormalizedColor(0, 1, -1, -1),
	new NormalizedColor(0, 1, -1, 1),
	new NormalizedColor(0, 1, 1, -1),
	new NormalizedColor(0, 1, 1, 1),
	new NormalizedColor(-1, -1, 0, -1),
	new NormalizedColor(-1, 1, 0, -1),
	new NormalizedColor(1, -1, 0, -1),
	new NormalizedColor(1, 1, 0, -1),
	new NormalizedColor(-1, -1, 0, 1),
	new NormalizedColor(-1, 1, 0, 1),
	new NormalizedColor(1, -1, 0, 1),
	new NormalizedColor(1, 1, 0, 1),
	
	new NormalizedColor(-1, 0, -1, -1),
	new NormalizedColor(1, 0, -1, -1),
	new NormalizedColor(-1, 0, -1, 1),
	new NormalizedColor(1, 0, -1, 1),
	new NormalizedColor(-1, 0, 1, -1),
	new NormalizedColor(1, 0, 1, -1),
	new NormalizedColor(-1, 0, 1, 1),
	new NormalizedColor(1, 0, 1, 1),
	new NormalizedColor(0, -1, -1, 0),
	new NormalizedColor(0, -1, -1, 0),
	new NormalizedColor(0, -1, 1, 0),
	new NormalizedColor(0, -1, 1, 0),
	new NormalizedColor(0, 1, -1, 0),
	new NormalizedColor(0, 1, -1, 0),
	new NormalizedColor(0, 1, 1, 0),
	new NormalizedColor(0, 1, 1, 0),
#endif
			for (int i = 0; i < 256; i++)
				data[i] = gradients[permutations[i] % gradients.Length];

			var texture = new Texture2D();
			texture.Data(data.Length, 1, Formats.Vector4nsb, data);
			Program.Uniforms["PerlinGradientTexture"].Set(texture);
		}

		void LoadRandomPerlinTransform()
		{
			PerlinTransform = Rng.NextTranslationMatrix4d(0, 10000);
		}

		void ProcessHeight(string techniqueName, Box2i modified)
		{
			Texture2D heightTexture = TerrainBlock.AllocateHeightBackBuffer();
			ProcessTarget(heightTexture, techniqueName);
			TerrainBlock.DirtyBackBufferHeightTexture(heightTexture, modified);
		}

		void ProcessHeight(string techniqueName, int x, int y, int width, int height) { ProcessHeight(techniqueName, new Box2i(x, y, width, height)); }
		void ProcessHeightAll(string techniqueName) { ProcessHeight(techniqueName, 0, 0, Terrain.BlockSize, Terrain.BlockSize); }
		void ProcessHeightNone(string techniqueName) { ProcessHeight(techniqueName, Box2i.Zero); }

		void ProcessTarget(Texture2D target, string techniqueName)
		{
			throw new NotImplementedException();
			/*Program.CurrentTechnique = Program.Techniques[techniqueName];
			Program.Parameters["HeightTexture"].SetValue(TerrainBlock.HeightTexture);
			GraphicsContext.SetRenderTarget(target);
			Terrain.DrawUnitRectangle(Program);
			GraphicsContext.SetRenderTarget(null);*/
		}

		/// <summary>Clear the terrain to a specific value.</summary>
		/// <param name="value"></param>
		public void Clear(float value)
		{
			Program.Uniforms["ClearValue"].Set(new Vector4f(value));
			ProcessHeightAll("Clear");
		}

		int TemporaryTexturePixels { get { return TemporaryTexture.Dimensions.Product; } }

		void WriteTemporaryTexture(Vector4nb[] data, int count)
		{
			if (data == null)
				throw new ArgumentNullException("lines");
			if (count > data.Length || count < 0)
				throw new ArgumentOutOfRangeException("count");

			throw new NotImplementedException();
			/*int width = TemporaryTexture.Width;
			int rows = data.Length / width, remainder = data.Length % width;

			Program.Parameters["TemporaryTexture"].SetValue((Texture2D)null);
			Program.CurrentTechnique.Passes[0].Apply();

			if (rows > 0)
				TemporaryTexture.SetData(0, new Rectangle(0, 0, width, rows), data, 0, rows * width);
			if (remainder > 0)
				TemporaryTexture.SetData(0, new Rectangle(0, rows, remainder, 1), data, rows * width, remainder);

			Program.Parameters["TemporaryTexture"].SetValue(TemporaryTexture);*/
		}

		/// <summary>
		/// Perform a set of fault line increments on the height texture.
		/// </summary>
		/// <param name="lines">Each pair of values is a line to separate the texture. Any point on the left of the line is pushed down, any point on the right of the line is pulled up.</param>
		/// <param name="count">The number of lines to push.</param>
		/// <param name="push">How much to push the height texture up or down.</param>
		public void Fault(Vector4nb[] lines, int count, float push)
		{
			WriteTemporaryTexture(lines, count);
			Program.Uniforms["FaultCount"].Set(count);
			Program.Uniforms["FaultPush"].Set(push);
			ProcessHeightAll("Fault");
		}

		/// <summary>
		/// Perform a set of random fault line increments on the height texture.
		/// </summary>
		/// <param name="count">The number of lines to push.</param>
		/// <param name="push">How much to push the height texture up or down per line.</param>
		public void Fault(int count, float push)
		{
			Vector4nb[] lines = TemporaryData;
			int maxPass = 4000;

			while (count > 0)
			{
				int passCount = Math.Min(count, maxPass);
				for (int i = 0; i < passCount; i++)
					lines[i] = new Vector4nb(Rng.NextVector4d());
				Fault(lines, passCount, push);
				count -= passCount;
			}
		}

		/// <summary>
		/// Add fractal brownian motion to the height texture.
		/// </summary>
		/// <param name="strength"></param>
		/// <param name="scale"></param>
		/// <param name="offset"></param>
		/// <param name="octaves"></param>
		/// <param name="lacunarity"></param>
		/// <param name="gain"></param>
		public void FractalBrownianMotion(float strength, float scale, float offset, int octaves, float lacunarity, float gain)
		{
			Program.Uniforms["FractalBrownianMotionLacunarity"].Set(lacunarity);
			Program.Uniforms["FractalBrownianMotionGain"].Set(gain);
			Program.Uniforms["FractalBrownianMotionOctaves"].Set(octaves);
			Program.Uniforms["FractalBrownianMotionOffset"].Set(offset);
			Program.Uniforms["FractalBrownianMotionScale"].Set(scale);
			Program.Uniforms["FractalBrownianMotionStrength"].Set(strength);

			Matrix4d perlinTransform = PerlinTransform;
			PerlinTransform = Matrix4d.Translate(new Vector3d(TerrainBlock.BlockIndex.X * scale, TerrainBlock.BlockIndex.Y * scale, 0)) * PerlinTransform;
			//LoadRandomPerlinTransform();
			ProcessHeightAll("FractalBrownianMotionTechnique");
			PerlinTransform = perlinTransform;
		}

		public void FractalBrownianMotion(float strength, float scale, float offset, int octaves)
		{
			FractalBrownianMotion(strength, scale, offset, octaves, 2, 0.5f);
		}

		/// <summary>
		/// Create a <see cref="Texture2D"/> that matches the terrain's dimensions.
		/// </summary>
		/// <param name="mipMap">Whether to mipmap the render target automatically.</param>
		/// <param name="format">What <see cref="Format"/> to use.</param>
		/// <returns>The render target.</returns>
		public Texture2D CreateMatchedRenderTarget(bool mipMap, Format format)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Set the terrain's color texture according to the elevation and the tilt of the terrain, where higher elevation increases x and higher tilt increases y.
		/// </summary>
		/// <param name="texture">The texture to colour the terrain by.</param>
		/// <param name="transform">A transform to apply when mapping the terrain color.</param>
		public void ColorByElevationAndTilt(Texture2D texture, Matrix4d transform)
		{
			Program.Uniforms["ColorByElevationAndTilt_Texture"].Set(texture);
			Program.Uniforms["ColorByElevationAndTilt_Transform"].Set(transform);
			ProcessTarget(TerrainBlock.ColorTexture, "ColorByElevationAndTilt");
		}

		public void ColorByElevationAndTilt(Texture2D texture, Length minimumElevation, Length maximumElevation, Angle maximumTilt)
		{
			Matrix4d transform = Matrix4d.Translate(0, -minimumElevation.InUniversal, 0) * Matrix4d.Scale(1.0 / maximumTilt.InRadians, 1.0 / (maximumElevation - minimumElevation).InUniversal, 1);
			ColorByElevationAndTilt(texture, transform);
		}

		public void ColorByElevationAndTilt(Texture2D texture, Angle maximumTilt)
		{
			ColorByElevationAndTilt(texture, Length.Universal(TerrainBlock.TreeRoot.pBox.Min.Y), Length.Universal(TerrainBlock.TreeRoot.pBox.Max.Y), maximumTilt);
		}

		public PlanarTerrainBlock SwitchToBlock(int blockIndexX, int blockIndexY)
		{
			return SwitchToBlock(new Vector2i(blockIndexX, blockIndexY));
		}

		public PlanarTerrainBlock SwitchToBlock(Vector2i blockIndex)
		{
			return TerrainBlock = Terrain.GenerateBlock(blockIndex);
		}

		public class ErosionState : IDisposable
		{
			public TerrainEditor Editor { get; private set; }
			public PlanarTerrain Terrain { get { return Editor.Terrain; } }
			public PlanarTerrainBlock TerrainBlock { get { return Editor.TerrainBlock; } }
			public Texture2D Height { get { return (Texture2D)TerrainBlock.HeightTexture; } }
			public Texture2D WaterSediment { get; private set; }
			public Texture2D WaterSedimentBackBuffer { get; private set; }
			public Texture2D Velocity { get; private set; }
			public Texture2D VelocityBackBuffer { get; private set; }
			public Texture2D OutflowFlux { get; private set; }
			public Texture2D OutflowFluxBackBuffer { get; private set; }
			public Texture2D Color { get; private set; }
			public float Time { get; private set; }

			// (half water, half sediment, half2 velocity)
			static readonly Format WaterSedimentFormat = Formats.Vector2f;
			
			static readonly Format VelocityFormat = Formats.Vector2f;

			// (float fluxLeft, float fluxRight, float fluxUp, float fluxDown)
			static readonly Format OutflowFluxFormat = Formats.Vector4f;

			public ErosionState(TerrainEditor editor)
			{
				Editor = editor;

				throw new NotImplementedException();
				/*WaterSedimentBackBuffer = (WaterSediment = Editor.CreateMatchedRenderTarget(false, WaterSedimentFormat)).CloneSettings();
				VelocityBackBuffer = (Velocity = Editor.CreateMatchedRenderTarget(false, VelocityFormat)).CloneSettings();
				OutflowFluxBackBuffer = (OutflowFlux = Editor.CreateMatchedRenderTarget(false, OutflowFluxFormat)).CloneSettings();
				Color = Editor.CreateMatchedRenderTarget(false, Formats.Vector4nb);
				SetParameters();*/
			}

			~ErosionState()
			{
				Dispose();
			}

			void SwapWaterSediment()
			{
				var old = WaterSediment;
				WaterSediment = WaterSedimentBackBuffer;
				WaterSedimentBackBuffer = old;
				SetParameters();
			}

			void SwapVelocity()
			{
				var old = Velocity;
				Velocity = VelocityBackBuffer;
				VelocityBackBuffer = old;
				SetParameters();
			}

			void SwapOutflowFlux()
			{
				var old = OutflowFlux;
				OutflowFlux = OutflowFluxBackBuffer;
				OutflowFluxBackBuffer = old;
				SetParameters();
			}

			void SetParameters()
			{
				Editor.Program.Uniforms["ErosionWaterSedimentTexture"].Set(WaterSediment);
				Editor.Program.Uniforms["ErosionVelocityTexture"].Set(Velocity);
				Editor.Program.Uniforms["ErosionOutflowFluxTexture"].Set(OutflowFlux);
			}

			public void Dispose()
			{
				if (WaterSediment != null)
					WaterSediment.Dispose();
				WaterSediment = null;
				if (Velocity != null)
					Velocity.Dispose();
				Velocity = null;
				if (VelocityBackBuffer != null)
					VelocityBackBuffer.Dispose();
				VelocityBackBuffer = null;
				if (WaterSedimentBackBuffer != null)
					WaterSedimentBackBuffer.Dispose();
				WaterSedimentBackBuffer = null;
				if (OutflowFlux != null)
					OutflowFlux.Dispose();
				OutflowFlux = null;
				if (OutflowFluxBackBuffer != null)
					OutflowFluxBackBuffer.Dispose();
				OutflowFluxBackBuffer = null;
			}

			void Process(string techniqueName, Texture2D target)
			{
				throw new NotImplementedException();
				/*Graphics.SetRenderTarget(target);
				Editor.Program.CurrentTechnique = Editor.Program.Techniques[techniqueName];
				Editor.Program.Parameters["HeightTexture"].SetValue(TerrainBlock.HeightTexture);
				Terrain.DrawUnitRectangle(Editor.Program);
				Graphics.SetRenderTarget(null);*/
			}

			void ProcessWaterSediment(string techniqueName)
			{
				Process(techniqueName, WaterSedimentBackBuffer);
				SwapWaterSediment();
			}

			void ProcessVelocity(string techniqueName)
			{
				Process(techniqueName, VelocityBackBuffer);
				SwapVelocity();
			}

			void ProcessOutflow(string techniqueName)
			{
				Process(techniqueName, OutflowFluxBackBuffer);
				SwapOutflowFlux();
			}

			public void Elapse(float time)
			{
				Editor.Program.Uniforms["ErosionTime"].Set(Time);
				Editor.Program.Uniforms["ErosionDeltaTime"].Set(time);
				Time += time;
			}

			public void WaterIncrement()
			{
				ProcessWaterSediment("ErosionWaterFallTechnique");
			}

			public void FlowSimulation()
			{
				ProcessOutflow("ErosionOutflowFluxTechnique");

				ProcessMulti("ErosionWaterSurfaceVelocityFieldUpdateTechnique", WaterSedimentBackBuffer, VelocityBackBuffer);
				SwapWaterSediment();
				SwapVelocity();
			}

			void ProcessMulti(string techniqueName, Texture2D a, Texture2D b)
			{
				throw new NotImplementedException();/*
				Graphics.SetRenderTargets(new RenderTargetBinding(a), new RenderTargetBinding(b));
				Editor.Program.CurrentTechnique = Editor.Program.Techniques[techniqueName];
				Editor.Program.Parameters["HeightTexture"].SetValue(TerrainBlock.HeightTexture);
				Terrain.DrawUnitRectangle(Editor.Program);
				Graphics.SetRenderTarget(null);*/
			}

			public void ErosionDeposition()
			{
				Texture2D heightTexture = TerrainBlock.AllocateHeightBackBuffer();
				ProcessMulti("ErosionDepositionTechnique", WaterSedimentBackBuffer, heightTexture);
				SwapWaterSediment();
				TerrainBlock.DirtyAllHeightBackBuffer(heightTexture);
			}

			public void SedimentTransport()
			{
				ProcessWaterSediment("ErosionSedimentTransportTechnique");
			}

			public void Visualization()
			{
				Process("ErosionVisualizationTechnique", Color);
			}

			public bool DepositWater = true;

			public void Erode(float time)
			{
				Elapse(time);
				if (DepositWater)
					WaterIncrement();
				FlowSimulation();
				ErosionDeposition();
				SedimentTransport();
				Visualization();
			}
		}
	}
}
