using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Glare.Graphics.Collada {
	[Serializable]
	public class Material : IdElement, IName {
		internal const string XmlName = "material";

		InstanceEffect instanceEffect;

		[XmlElement(InstanceEffect.XmlName)]
		public InstanceEffect InstanceEffect {
			get { return instanceEffect; }
			set { SetElement(ref instanceEffect, value); }
		}

		/// <summary>Get or set the name of the object; optional.</summary>
		[XmlAttribute(Element.XmlNameAttribute, DataType = "token")]
		public string Name { get; set; }

		public Material() { }

		public Material(string id, InstanceEffect instanceEffect = null) {
			Id = id;
			InstanceEffect = instanceEffect;
		}
	}

	[Serializable]
	public class MaterialCollection : ElementCollection<Material> {
		public MaterialCollection() { }
		public MaterialCollection(int capacity) : base(capacity) { }
		public MaterialCollection(IEnumerable<Material> collection) : base(collection) { }
		public MaterialCollection(params Material[] list) : base(list) { }
	}
}
