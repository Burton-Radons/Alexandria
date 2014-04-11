using Glare.Graphics.Collada;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics.Loaders {
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
		public static readonly ReadOnlyBidictionary<D3DDeclarationUsage, InputSemantic> InputSemanticConverter = new Bidictionary<D3DDeclarationUsage, InputSemantic>() {
			{ D3DDeclarationUsage.Position, InputSemantic.Vertex },
			{ D3DDeclarationUsage.BlendWeight, InputSemantic.Weight },
			{ D3DDeclarationUsage.BlendIndices, InputSemantic.Joint },
			{ D3DDeclarationUsage.Normal, InputSemantic.Normal },
			{ D3DDeclarationUsage.PointSize, InputSemantic.PointSize },
			{ D3DDeclarationUsage.TextureCoordinate, InputSemantic.TextureCoordinate },
			{ D3DDeclarationUsage.Tangent, InputSemantic.Tangent },
			{ D3DDeclarationUsage.Binormal, InputSemantic.Binormal },
			{ D3DDeclarationUsage.TesselationFactor, InputSemantic.TesselationFactor },
			{ D3DDeclarationUsage.TransformedPosition, InputSemantic.TransformedPosition },
			{ D3DDeclarationUsage.Color, InputSemantic.Color },
			{ D3DDeclarationUsage.Fog, InputSemantic.Fog },
			{ D3DDeclarationUsage.Depth, InputSemantic.Depth },
			{ D3DDeclarationUsage.Sample, InputSemantic.Sample },
		};
	}
}
