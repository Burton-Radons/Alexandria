using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Glare.Graphics.Collada {
	[Serializable]
	public class VertexCollection : IdElement, IName {
		internal const string XmlName = "vertices";

		InputCollection inputs;

		[XmlElement(Input.XmlName, typeof(Input))]
		public InputCollection Inputs {
			get { return inputs; }
			set { SetCollection<Input, InputCollection>(ref inputs, value); }
		}

		/// <summary>Get or set the name of the object; optional.</summary>
		[XmlAttribute(Element.XmlNameAttribute, DataType = "token")]
		public string Name { get; set; }

		public VertexCollection() {
			Inputs = new InputCollection();
		}

		public VertexCollection(string id, params Input[] inputs) {
			Id = id;
			Inputs = new InputCollection(inputs);
		}
	}
}
