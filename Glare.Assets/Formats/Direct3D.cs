using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Assets.Formats {
	/// <summary>
	/// D3DDECLUSAGE
	/// </summary>
	public enum D3DDeclarationUsage : int {
		/// <summary>Position of the vertex.</summary>
		Position = 0,

		/// <summary>Blending weight for bones.</summary>
		BlendWeight = 1,

		/// <summary>Blending bone indices.</summary>
		BlendIndices = 2,

		/// <summary>Surface normal.</summary>
		Normal = 3,

		/// <summary>Size of the point.</summary>
		PointSize = 4,

		/// <summary>Texture coordinate.</summary>
		TextureCoordinate = 5,

		/// <summary>Surface tangent.</summary>
		Tangent = 6,

		/// <summary>Surface binormal.</summary>
		Binormal = 7,

		/// <summary>Tesselation factor.</summary>
		TesselationFactor = 8,

		/// <summary>Transformed vertex position.</summary>
		TransformedPosition = 9,

		/// <summary>Color.</summary>
		Color = 10,

		/// <summary>Fog parameter.</summary>
		Fog = 11,

		/// <summary>Vertex depth.</summary>
		Depth = 12,

		/// <summary>Vertex sample.</summary>
		Sample = 13,
	}

	/// <summary>
	/// Direct3D declarations and functionality.
	/// </summary>
	public static class D3D {
	}
}
