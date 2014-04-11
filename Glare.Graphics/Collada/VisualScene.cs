using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Glare.Graphics.Collada {
	[Serializable]
	public class VisualScene : IdElement, IName {
		internal const string XmlName = "visual_scene";

		NodeCollection nodes;

		/// <summary>Get or set the name of the object; optional.</summary>
		[XmlAttribute(Element.XmlNameAttribute, DataType = "token")]
		public string Name { get; set; }

		[XmlElement(Node.XmlName)]
		public NodeCollection Nodes {
			get { return nodes; }
			set { SetCollection<Node, NodeCollection>(ref nodes, value); }
		}

		public VisualScene() {
			Nodes = new NodeCollection();
		}

		public VisualScene(string id, params Node[] nodes) {
			Id = id;
			Nodes = new NodeCollection(nodes);
		}
	}

	[Serializable]
	public class VisualSceneCollection : ElementCollection<VisualScene> {
		public VisualSceneCollection() { }
		public VisualSceneCollection(int capacity) : base(capacity) { }
		public VisualSceneCollection(IEnumerable<VisualScene> collection) : base(collection) { }
		public VisualSceneCollection(params VisualScene[] list) : base(list) { }
	}
}
