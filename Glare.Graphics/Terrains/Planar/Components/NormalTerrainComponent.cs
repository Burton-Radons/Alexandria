using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glare.Graphics.Terrains.Planar.Components
{
	/// <summary>Provides a texture layer that has a surface normal.</summary>
	public class NormalTerrainComponent : LayerTerrainComponent
	{
		/// <summary>Initialize the normal layer.</summary>
		/// <param name="terrain"></param>
		public NormalTerrainComponent(PlanarTerrain terrain)
			: base(terrain, Formats.Vector4nb)
		{
			foreach (PlanarTerrainBlock block in terrain.Blocks)
			{
				RecreateNormalMap(block);
				block.NormalTexture = Get(block);
			}
		}

		protected override void OnDetach()
		{
			base.OnDetach();
			foreach (PlanarTerrainBlock block in Terrain.Blocks)
				block.NormalTexture = null;
		}

		public override void OnBlockAdded(PlanarTerrainBlock block)
		{
			base.OnBlockAdded(block);
			RecreateNormalMap(block);
			block.NormalTexture = Get(block);
		}

		public override void OnHeightModified(PlanarTerrainBlock block, ref Box2i area)
		{
			base.OnHeightModified(block, ref area);
			RecreateNormalMap(block);
		}

		void RecreateNormalMap(PlanarTerrainBlock block)
		{
			var effect = Terrain.Program; // NormalEffect
			effect.Uniforms["heightTexture"].Set(block.HeightTexture);
			Terrain.FrameBuffer.Colors[0].Attach(Get(block));
			Device.FrameBuffer = Terrain.FrameBuffer;
			Terrain.DrawUnitRectangle("normal");
			Device.FrameBuffer = null;

			effect.Uniforms["heightTexture"].Set((Texture2D)null);
		}

		public override void SetTexture(PlanarTerrainBlock block, Texture2D value)
		{
			base.SetTexture(block, value);
			block.NormalTexture = value;
		}
	}
}
