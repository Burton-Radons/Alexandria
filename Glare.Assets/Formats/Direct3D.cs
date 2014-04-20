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
		Position = 0,
		BlendWeight = 1,
		BlendIndices = 2,
		Normal = 3,
		PointSize = 4,
		TextureCoordinate = 5,
		Tangent = 6,
		Binormal = 7,
		TesselationFactor = 8,
		TransformedPosition = 9,
		Color = 10,
		Fog = 11,
		Depth = 12,
		Sample = 13,
	}

	/// <summary>
	/// Direct3D declarations and functionality.
	/// </summary>
	public static class D3D {
	}
}
