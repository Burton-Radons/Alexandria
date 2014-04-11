using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Glare.Graphics.Collada {
	[Serializable]
	public class Mesh : GeometryElement, IExtras {
		internal const string XmlName = "mesh";

		MeshPrimitivesCollection elements;
		ExtraCollection extras;
		VertexCollection vertices;

		[XmlElement(MeshPolygons.XmlName, typeof(MeshPolygons), Order = 2)]
		[XmlElement(MeshTriangles.XmlName, typeof(MeshTriangles), Order = 2)]
		[XmlElement(MeshTriangleStrips.XmlName, typeof(MeshTriangleStrips), Order = 2)]
		public MeshPrimitivesCollection Elements {
			get { return elements; }
			set { SetCollection<MeshPrimitives, MeshPrimitivesCollection>(ref elements, value); }
		}

		/// <summary>Provides arbitrary additional information about or related to its parent element.</summary>
		[XmlElement(Glare.Graphics.Collada.Extra.XmlName, Order = 3)]
		public ExtraCollection Extras {
			get { return extras; }
			set { SetCollection<Extra, ExtraCollection>(ref extras, value); }
		}

		[XmlElement(VertexCollection.XmlName, typeof(VertexCollection), Order = 1)]
		public VertexCollection Vertices {
			get { return vertices; }
			set { SetElement(ref vertices, value); }
		}

		public Mesh() {
			Elements = new MeshPrimitivesCollection();
			Extras = new ExtraCollection();
			Vertices = new VertexCollection();
		}
	}
}
