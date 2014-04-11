using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Glare.Graphics.Collada {
	/// <summary>Creates a new, named parameter object, and assigns it a type and an initial value.</summary>
	public class NewParameter : ScopedIdElement {
		internal const string XmlName = "newparam";

		Element value;

		[XmlElement(Sampler2D.XmlName, typeof(Sampler2D))]
		public Element Value {
			get { return value; }
			set { SetElement(ref this.value, value); }
		}

		public NewParameter() { }

		public NewParameter(string scopedId, Element value) {
			ScopedId = scopedId;
			Value = value;
		}
	}

	[Serializable]
	public class NewParameterCollection : ElementCollection<NewParameter> {
		public NewParameterCollection() { }
		public NewParameterCollection(int capacity) : base(capacity) { }
		public NewParameterCollection(IEnumerable<NewParameter> collection) : base(collection) { }
		public NewParameterCollection(params NewParameter[] list) : base(list) { }
	}
}
