using Glare.Graphics.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics.Simulation {
	/// <summary>
	/// This is a fluid dynamic simulation texture.
	/// </summary>
	public class FluidTexture : DynamicTexture {
		/// <summary>Assumes square cells.</summary>
		public double GridScale = 1;

		public TimeSpan Timestep = TimeSpan.FromSeconds(1);

		public int PoissonStepCount = 50;

		public double InkLongevity = 0.9995;

		public double Viscosity = 0;

		public bool ArbitraryBC = false;

		public bool ClearPressureEachStep = true;

		public bool ComputeVorticity = true;

		public bool ApplyVCForce = true;

		public double VorticityConfinementScale = 0.035;

		public bool ImpulseToProcess = false;

		public bool InkToAdd = false;

		public Texture2D OffscreenBuffer, VelocityOffset, PressureOffset, Display, BC, BCDisplay, BCDetail;

		public Vector3d InkColor = Vector3d.One;

		public Texture2D[] Textures = new Texture2D[(int)Texture.MaxValue];

		public Vector4f[] Data;

		enum Texture {
			Velocity = 0,
			Density,
			Divergence,
			Pressure,
			Vorticity,
			VelocityOffsets,
			PressureOffsets,
			MaxValue
		};

		public FluidTexture(Context context, Vector2i dimensions)
			: base(context, Formats.Vector4f, dimensions) {
			Data = new Vector4f[dimensions.Product];

			Format format = Formats.Vector4f;
			OffscreenBuffer = new Texture2D(format, dimensions);

			for (int index = 0; index < Textures.Length; index++) {
				var texture = Textures[index] = new Texture2D(format, dimensions) {
					MinificationFilter = TextureFilter.Nearest,
					MipmapFilter = TextureFilter.Nearest,
					WrapX = TextureWrap.ClampToEdge,
					WrapY = TextureWrap.ClampToEdge,
				};
			}

			BC = new Texture2D(Formats.Vector4nb, dimensions) {
				MinificationFilter = TextureFilter.Nearest,
				MipmapFilter = TextureFilter.None,
				MagnificationFilter = TextureFilter.Nearest,
				WrapX = TextureWrap.ClampToEdge,
				WrapY = TextureWrap.ClampToEdge,
			};
			Clear(0, BC);

			BCDisplay = new Texture2D(Formats.Vector4nb, 64, 64) {
				MinificationFilter = TextureFilter.Linear,
				MipmapFilter = TextureFilter.None,
				MagnificationFilter = TextureFilter.Linear,
				WrapX = TextureWrap.ClampToEdge,
				WrapY = TextureWrap.ClampToEdge,
			};
			Clear(0, BCDisplay);

			Display = new Texture2D(Formats.Vector4nb, dimensions) {
				MinificationFilter = TextureFilter.Linear,
				MipmapFilter = TextureFilter.None,
				MagnificationFilter = TextureFilter.Linear,
				WrapX = TextureWrap.ClampToEdge,
				WrapY = TextureWrap.ClampToEdge,
			};

			BCDetail = new Texture2D(Formats.Vector4nb, 512, 512) // "bc.tga") // Grimy map
			{
				MinificationFilter = TextureFilter.Linear,
				MipmapFilter = TextureFilter.None,
				MagnificationFilter = TextureFilter.Linear,
				WrapX = TextureWrap.Repeat,
				WrapY = TextureWrap.Repeat,
			};

			float[] velocityData = new float[136]
			{
				// This cell is a fluid cell
				1,  0,  1,  0,   // Free (no neighboring boundaries)
				0,  0, -1,  1,   // East (a boundary to the east)
				1,  0,  1,  0,   // Unused
				1,  0,  0,  0,   // North
				0,  0,  0,  0,   // Northeast
				1,  0,  1,  0,   // South
				0,  0,  1,  0,   // Southeast
				1,  0,  1,  0,   // West
				1,  0,  1,  0,   // Unused
				0,  0,  0,  0,   // surrounded (3 neighbors)
				1,  0,  0,  0,   // Northwest
				0,  0,  0,  0,   // surrounded (3 neighbors)
				1,  0,  1,  0,   // Southwest 
				0,  0,  0,  0,   // surrounded (3 neighbors)
				0,  0,  0,  0,   // Unused
				0,  0,  0,  0,   // surrounded (3 neighbors)
				0,  0,  0,  0,   // surrounded (4 neighbors)
				// This cell is a boundary cell (the inverse of above!)
				1,  0,  1,  0,   // No neighboring boundaries (Error)
				0,  0,  0,  0,   // Unused
				0,  0,  0,  0,   // Unused
				0,  0,  0,  0,   // Unused
				-1, -1, -1, -1,   // Southwest 
				0,  0,  0,  0,   // Unused
				-1,  1,  0,  0,   // Northwest
				0,  0,  0,  0,   // Unused
				0,  0,  0,  0,   // Unused
				0,  0, -1, -1,   // West
				0,  0, -1,  1,   // Southeast
				-1, -1,  0,  0,   // South
				0,  0,  0,  0,   // Northeast
				-1,  1,  0,  0,   // North
				0,  0,  0,  0,   // Unused
				0,  0, -1,  1,   // East (a boundary to the east)
				0,  0,  0,  0    // Unused
			};

			VelocityOffset = new Texture2D() {
				MinificationFilter = TextureFilter.Nearest,
				MipmapFilter = TextureFilter.None,
				MagnificationFilter = TextureFilter.Nearest,
				WrapX = TextureWrap.ClampToEdge,
				WrapY = TextureWrap.ClampToEdge,
			}.Data(34, 1, format, velocityData, 0, Formats.Vector4f);

			float[] pressureData = new float[136] {
				// This cell is a fluid cell
				0,  0,  0,  0,   // Free (no neighboring boundaries)
				0,  0,  0,  0,   // East (a boundary to the east)
				0,  0,  0,  0,   // Unused
				0,  0,  0,  0,   // North
				0,  0,  0,  0,   // Northeast
				0,  0,  0,  0,   // South
				0,  0,  0,  0,   // Southeast
				0,  0,  0,  0,   // West
				0,  0,  0,  0,   // Unused
				0,  0,  0,  0,   // Landlocked (3 neighbors)
				0,  0,  0,  0,   // Northwest
				0,  0,  0,  0,   // Landlocked (3 neighbors)
				0,  0,  0,  0,   // Southwest 
				0,  0,  0,  0,   // Landlocked (3 neighbors)
				0,  0,  0,  0,   // Unused
				0,  0,  0,  0,   // Landlocked (3 neighbors)
				0,  0,  0,  0,   // Landlocked (4 neighbors)
				// This cell is a boundary cell (the inverse of above!)
				0,  0,  0,  0,   // no neighboring boundaries
				0,  0,  0,  0,   // unused
				0,  0,  0,  0,   // unused
				0,  0,  0,  0,   // unused
				-1,  0,  0, -1,   // Southwest 
				0,  0,  0,  0,   // unused
				-1,  0,  0,  1,   // Northwest
				0,  0,  0,  0,   // Unused
				0,  0,  0,  0,   // Unused
				-1,  0, -1,  0,   // West
				0, -1,  1,  0,   // Southeast
				0, -1,  0, -1,   // South
				0,  1,  1,  0,   // Northeast
				0,  1,  0,  1,   // North
				0,  0,  0,  0,   // Unused
				1,  0,  1,  0,   // East (a boundary to the east)
				0,  0,  0,  0   // Unused
			};

			PressureOffset = new Texture2D() {
				MinificationFilter = TextureFilter.Nearest,
				MipmapFilter = TextureFilter.None,
				MagnificationFilter = TextureFilter.Nearest,
				WrapX = TextureWrap.ClampToEdge,
				WrapY = TextureWrap.ClampToEdge,
			}.Data(34, 1, format, pressureData, 0, Formats.Vector4f);
		}
	}
}
