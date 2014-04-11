using Glare.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Glare.Graphics.Collada {
	[Serializable]
	public class Input : Element {
		internal const string XmlName = "input";

		/// <summary>The user-defined meaning of the input connection. Required.</summary>
		[XmlAttribute("semantic")]
		public InputSemantic Semantic { get; set; }

		[XmlAttribute("source", DataType = "anyURI")]
		public string Source { get; set; }

		public Input() { }

		public Input(InputSemantic semantic, string source) {
			Semantic = semantic;
			Source = source;
		}
	}

	[Serializable]
	public class InputCollection : ElementCollection<Input> {
		public InputCollection() { }
		public InputCollection(int capacity) : base(capacity) { }
		public InputCollection(IEnumerable<Input> collection) : base(collection) { }
		public InputCollection(params Input[] list) : base(list) { }
	}

	[Serializable]
	public class SharedInput : Input {
		/// <summary>The offset into the list of indices defined by the parent element’s "p" or "v" subelement. If two <see cref="SharedInput"/> elements share the same offset, they are indexed the same. This is a simple form of compression for the list of indices and also defines the order in which the inputs are used. Required.</summary>
		[XmlAttribute("offset", DataType = "int")]
		public int Offset { get; set; }

		/// <summary>Get or set which inputs to group as a single set. This is helpful when multiple inputs share the same semantics. Optional.</summary>
		[XmlAttribute("set", DataType = "int")]
		public int Set { get; set; }
	}

	[Serializable]
	public class SharedInputCollection : ElementCollection<SharedInput> {
		public SharedInputCollection() { }
		public SharedInputCollection(int capacity) : base(capacity) { }
		public SharedInputCollection(IEnumerable<SharedInput> collection) : base(collection) { }
		public SharedInputCollection(params SharedInput[] list) : base(list) { }
	}

	[Serializable]
	public enum InputSemantic {
		/// <summary>Geometric binormal (bitangent) vector.</summary>
		[XmlEnum("BINORMAL")]
		Binormal,

		/// <summary>Color coordinate vector. Color inputs are RGB (float3_type).</summary>
		[XmlEnum("COLOR")]
		Color,

		/// <summary>Continuity constraint at the control vertex (CV). See also “Curve Interpolation” in Chapter 4: Programming Guide. </summary>
		[XmlEnum("CONTINUITY")]
		Continuity,

		/// <summary>Raster or MIP-level input. </summary>
		[XmlEnum("IMAGE")]
		Image,

		/// <summary>Sampler input. See also “Curve Interpolation” in Chapter 4: Programming Guide. </summary>
		[XmlEnum("INPUT")]
		Input,

		/// <summary>Tangent vector for preceding control point. See also “Curve Interpolation” in Chapter 4: Programming Guide.</summary>
		[XmlEnum("IN_TANGENT")]
		InTangent,

		/// <summary>Sampler interpolation type. See also “Curve Interpolation” in Chapter 4: Programming Guide.</summary>
		[XmlEnum("INTERPOLATION")]
		Interpolation,

		/// <summary>Inverse of local-to-world matrix.</summary>
		[XmlEnum("INV_BIND_MATRIX")]
		InverseBindMatrix,

		/// <summary>Skin influence identifier.</summary>
		[XmlEnum("JOINT")]
		Joint,

		/// <summary>Number of piece-wise linear approximation steps to use for the spline segment that follows this CV. See also “Curve Interpolation” in Chapter 4: Programming Guide.</summary>
		[XmlEnum("LINEAR_STEPS")]
		LinearSteps,

		/// <summary>Morph targets for mesh morphing.</summary>
		[XmlEnum("MORPH_TARGET")]
		MorphTarget,

		/// <summary>Weights for mesh morphing.</summary>
		[XmlEnum("MORPH_WEIGHT")]
		MorphWeight,

		/// <summary>Normal vector.</summary>
		[XmlEnum("NORMAL")]
		Normal,

		/// <summary>Sampler output. See also “Curve Interpolation” in Chapter 4: Programming Guide.</summary>
		[XmlEnum("OUTPUT")]
		Output,

		/// <summary>Tangent vector for succeeding control point. See also “Curve Interpolation” in Chapter 4: Programming Guide.</summary>
		[XmlEnum("OUT_TANGENT")]
		OutTangent,

		/// <summary>Geometric coordinate vector. See also “Curve Interpolation” in Chapter 4: Programming Guide.</summary>
		[XmlEnum("POSITION")]
		Position,

		/// <summary>Geometric tangent vector.</summary>
		[XmlEnum("TANGENT")]
		Tangent,

		/// <summary>Texture binormal (bitangent) vector.</summary>
		[XmlEnum("TEXBINORMAL")]
		TextureBinormal,

		/// <summary>Texture coordinate vector.</summary>
		[XmlEnum("TEXCOORD")]
		TextureCoordinate,

		/// <summary>Texture tangent vector.</summary>
		[XmlEnum("TEXTANGENT")]
		TextureTangent,

		/// <summary>Generic parameter vector.</summary>
		[XmlEnum("UV")]
		Generic,

		/// <summary>Mesh vertex.</summary>
		[XmlEnum("VERTEX")]
		Vertex,

		/// <summary>Skin influence weighting value.</summary>
		[XmlEnum("WEIGHT")]
		Weight,

		/// <summary>Extension; size of a point (created for D3DDECLUSAGE).</summary>
		[XmlEnum("PSIZE")]
		PointSize,

		/// <summary>Extension; created for D3DDECLUSAGE.</summary>
		[XmlEnum("TESSFACTOR")]
		TesselationFactor,

		/// <summary>Extension; created for D3DDECLUSAGE.</summary>
		[XmlEnum("POSITIONT")]
		TransformedPosition,

		/// <summary>Extension; created for D3DDECLUSAGE.</summary>
		[XmlEnum("FOG")]
		Fog,

		/// <summary>Extension; created for D3DDECLUSAGE.</summary>
		[XmlEnum("DEPTH")]
		Depth,

		/// <summary>Extension; created for D3DDECLUSAGE.</summary>
		[XmlEnum("SAMPLE")]
		Sample,
	}
}
