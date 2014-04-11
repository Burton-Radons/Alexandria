using Glare.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Glare.Graphics.Collada {
	[Serializable]
	public class Node : IdElement, IAsset, IExtras, IName {
		internal const string XmlName = "node";

		Asset asset;
		ExtraCollection extras;
		GeometryInstanceCollection geometryInstances;
		NodeCollection nodes;
		TransformCollection transforms;

		/// <summary>Allows the node to express asset management information.</summary>
		[XmlElement(Asset.XmlName, Order = 0)]
		public Asset Asset {
			get { return asset; }
			set { SetElement(ref asset, value); }
		}

		/// <summary>Provides arbitrary additional information about or related to its parent element.</summary>
		[XmlElement(Glare.Graphics.Collada.Extra.XmlName, Order = 4)]
		public ExtraCollection Extras {
			get { return extras; }
			set { SetCollection<Extra, ExtraCollection>(ref extras, value); }
		}

		[XmlElement(InstanceGeometry.XmlName, Order = 2)]
		public GeometryInstanceCollection GeometryInstances {
			get { return geometryInstances; }
			set { SetCollection<InstanceGeometry, GeometryInstanceCollection>(ref geometryInstances, value); }
		}

		/*/// <summary>The names of the layers to which this node belongs. For example, a value of foreground glowing” indicates that this node belongs to both the layer named foregroundand the layer named glowing. The default is empty, indicating that the node doesn’t belong to any layer.</summary>
		[XmlAttribute("layers")]
		[DefaultValue("")]
		public List<string> Layers { get; set; }*/

		/// <summary>Get or set the name of the object; optional.</summary>
		[XmlAttribute(Element.XmlNameAttribute, DataType = "token")]
		public string Name { get; set; }

		[XmlElement(Node.XmlName, Order = 3)]
		public NodeCollection Nodes {
			get { return nodes; }
			set { SetCollection<Node, NodeCollection>(ref nodes, value); }
		}

		/// <summary>The type of the <see cref="Node"/> element. Valid types are <see cref="NodeType.Joint"/> and <see cref="NodeType.Node"/>. The default is <see cref="NodeType.Node"/>.</summary>
		[XmlAttribute("type")]
		[DefaultValue(NodeType.Node)]
		public NodeType NodeType { get; set; }

		/// <summary>A text string value containing the scoped identifier of this element. This value must be unique within the scope ofthe parent element. Optional. For details, see “Address Syntax” in Chapter 3: Schema Concepts.</summary>
		[XmlAttribute(XmlScopedIdAttribute)]
		public string ScopedId { get; set; }

		/// <summary>List of transforms applied to the <see cref="Node"/>.</summary>
		[XmlElement(RotateTransform.XmlName, typeof(RotateTransform), Order = 1)]
		[XmlElement(ScaleTransform.XmlName, typeof(ScaleTransform), Order = 1)]
		[XmlElement(TranslateTransform.XmlName, typeof(TranslateTransform), Order = 1)]
		public TransformCollection Transforms {
			get { return transforms; }
			set { SetCollection<Transform, TransformCollection>(ref transforms, value); }
		}

		public Node() {
			Extras = new ExtraCollection();
			Nodes = new NodeCollection();
			GeometryInstances = new GeometryInstanceCollection();
			//Layers = new List<string>();
			Transforms = new TransformCollection();
		}

		public Node(string id)
			: this() {
			Id = id;
		}
	}

	[Serializable]
	public class NodeCollection : ElementCollection<Node> {
		public NodeCollection() { }
		public NodeCollection(int capacity) : base(capacity) { }
		public NodeCollection(IEnumerable<Node> collection) : base(collection) { }
		public NodeCollection(params Node[] list) : base(list) { }
	}

	public enum NodeType {
		[XmlEnum("NODE")]
		Node,

		[XmlEnum("JOINT")]
		Joint,
	}
}
