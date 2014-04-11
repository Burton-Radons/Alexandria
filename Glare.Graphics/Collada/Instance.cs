using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Glare.Graphics.Collada {
	[Serializable]
	public abstract class Instance : ScopedIdElement, IExtras, IName {
		const string XmlUrlAttribute = "url";

		ExtraCollection extras;

		/// <summary>Provides arbitrary additional information about or related to its parent element.</summary>
		[XmlElement(Glare.Graphics.Collada.Extra.XmlName)]
		public ExtraCollection Extras {
			get { return extras; }
			set { SetCollection<Extra, ExtraCollection>(ref extras, value); }
		}

		/// <summary>Get or set the name of the object; optional.</summary>
		[XmlAttribute(XmlNameAttribute, DataType = "token")]
		public string Name { get; set; }

		public Instance() {
			Extras = new ExtraCollection();
		}
	}

	[Serializable]
	public abstract class Instance<T> : Instance {
	}

	[Serializable]
	public class InstanceGeometry : Instance<Geometry> {
		internal const string XmlName = "instance_geometry";

		BindMaterial bindMaterial;

		/// <summary>Binds material symbols to material instances.</summary>
		[XmlElement(BindMaterial.XmlName)]
		public BindMaterial BindMaterial {
			get { return bindMaterial; }
			set { SetElement(ref bindMaterial, value); }
		}

		/// <summary>The URL of the location of the element to instantiate. Required. Can refer to a local instance or external reference. For a a local instance, this is a relative URI fragment identifier that begins with the “#” character. The fragment identifier is an XPointer shorthand pointer that consists of the ID of the element to instantiate. For an external reference, this is an absolute or relative URL. </summary>
		[XmlAttribute("url", DataType = "anyURI")]
		public string Url { get; set; }

		public InstanceGeometry() {
		}

		public InstanceGeometry(string url, BindMaterial bindMaterial = null) {
			Url = url;
			BindMaterial = bindMaterial;
		}
	}

	[Serializable]
	public class GeometryInstanceCollection : ElementCollection<InstanceGeometry> {
		public GeometryInstanceCollection() { }
		public GeometryInstanceCollection(int capacity) : base(capacity) { }
		public GeometryInstanceCollection(IEnumerable<InstanceGeometry> collection) : base(collection) { }
		public GeometryInstanceCollection(params InstanceGeometry[] list) : base(list) { }
	}

	[Serializable]
	public class InstanceMaterial : Instance<Material>, IName {
		internal const string XmlName = "instance_material";

		/// <summary>Which symbol defined from within the geometry this material binds to. Required.</summary>
		[XmlAttribute("symbol", DataType = "NCName")]
		public string Symbol { get; set; }

		/// <summary>The URI of the location of the <see cref="Material"/> element to instantiate. Required. Can refer to a local instance or external reference. For a local instance, this is a relative URI fragment identifier that begins with the “#” character. The fragment identifier is an XPointer shorthand pointer that consists of the ID of the element to instantiate. For an external reference, this is an absolute or relative URL.</summary>
		[XmlAttribute("target", DataType = "anyURI")]
		public string Target { get; set; }

		public InstanceMaterial() {
		}

		public InstanceMaterial(string symbol, string target) {
			Symbol = symbol;
			Target = target;
		}
	}

	[Serializable]
	public abstract class SceneInstance : Instance {
	}

	[Serializable]
	public abstract class SceneInstance<T> : SceneInstance {
	}

	[Serializable]
	public class VisualSceneInstance : SceneInstance<VisualScene>, IExtras, IName {
		internal const string XmlName = "instance_visual_scene";

		/// <summary>The URL of the location of the <see cref="VisualScene"/> element to instantiate. Required. Can refer to a local instance or external reference. For a local instance, this is a relative URI fragment identifier that begins with the “#” character. The fragment identifier is an XPointer shorthand pointer that consists of the ID of the element to instantiate. For an external reference, this is an absolute or relative URL.</summary>
		[XmlAttribute("url", DataType = "anyURI")]
		public string Url { get; set; }

		public VisualSceneInstance() { }

		public VisualSceneInstance(string url) {
			Url = url;
		}
	}
}
