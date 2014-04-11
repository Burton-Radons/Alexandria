using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glare.Graphics.Terrains.Planar.Components
{
	/// <summary>
	/// A <see cref="TerrainComponent"/> that provides a texture layer.
	/// </summary>
	public class LayerTerrainComponent : TerrainComponent
	{
		readonly Format Format;
		readonly Dictionary<Vector2i, Texture2D> Textures = new Dictionary<Vector2i, Texture2D>();

		public LayerTerrainComponent(PlanarTerrain terrain, Format format)
			: base(terrain)
		{
			if (format == null)
				throw new ArgumentNullException("format");
			Format = format;
		}

		public Texture2D Get(PlanarTerrainBlock block)
		{
			Texture2D pair = null;

			if (!Textures.TryGetValue(block.BlockIndex, out pair) || pair == null)
			{
				pair = new Texture2D(Format, Terrain.BlockSize, Terrain.BlockSize);
				Textures[block.BlockIndex] = pair;
			}

			return pair;
		}

		protected override void OnDetach()
		{
			base.OnDetach();
			foreach (KeyValuePair<Vector2i, Texture2D> pair in Textures)
				SetTexture(Terrain.GenerateBlock(pair.Key), null);
		}

		public virtual void SetTexture(PlanarTerrainBlock block, Texture2D value)
		{
			//Texture2D texture;

			throw new NotImplementedException();
			/*Textures.TryGetValue(block.BlockIndex, out texture);
			Terrain.RenderTargetCache.ReturnToPool(texture);
			Textures[block.BlockIndex] = value;*/
		}
	}
}
