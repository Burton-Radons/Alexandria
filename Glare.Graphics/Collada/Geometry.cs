using Glare.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Glare.Graphics.Collada {
	[Serializable]
	[XmlInclude(typeof(GeometryElement))]
	[XmlInclude(typeof(Mesh))]
	public class Geometry : IdElement, IName {
		internal const string XmlName = "geometry";

		GeometryElement element;

		[XmlElement(Type = typeof(GeometryElement))]
		[XmlElement(Mesh.XmlName, Type = typeof(Mesh))]
		public GeometryElement Element {
			get { return element; }
			set { SetElement(ref element, value); }
		}

		/// <summary>Get or set the name of the object; optional.</summary>
		[XmlAttribute(XmlNameAttribute, DataType = "token")]
		public string Name { get; set; }

		public Geometry() { }

		public Geometry(string id, GeometryElement element = null) {
			Id = id;
			Element = element;
		}
	}

	[Serializable]
	public class GeometryCollection : ElementCollection<Geometry> {
		public GeometryCollection() { }
		public GeometryCollection(int capacity) : base(capacity) { }
		public GeometryCollection(IEnumerable<Geometry> collection) : base(collection) { }
		public GeometryCollection(params Geometry[] list) : base(list) { }
	}

	[Serializable]
	[XmlInclude(typeof(Mesh))]
	public abstract class GeometryElement : Element {
		SourceCollection sources;

		[XmlElement(Source.XmlName, typeof(Source), Order = 0)]
		public SourceCollection Sources {
			get { return sources; }
			set { SetCollection<Source, SourceCollection>(ref sources, value); }
		}

		public GeometryElement() {
			Sources = new SourceCollection();
		}
	}
}
