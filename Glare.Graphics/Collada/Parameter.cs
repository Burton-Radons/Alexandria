using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Glare.Graphics.Collada {
	[Serializable]
	public class Parameter : Element, IName {
		internal const string XmlName = "param";

		internal const string XmlTypeAttribute = "type";

		/// <summary>Get or set the name of the object; optional.</summary>
		[XmlAttribute("name")]
		public string Name { get; set; }

		/// <summary>A text string value containing the scoped identifier of this element. This value must be unique within the scope of the parent element. Optional. For details, see “Address Syntax” in Chapter 3: Schema Concepts.</summary>
		[XmlAttribute("sid", DataType = "NCName")]
		public string ScopedId { get; set; }

		/// <summary>The type of the value data.</summary>
		[XmlAttribute("type")]
		public ParameterType Type { get; set; }

		/// <summary>Get or set the value of the parameter.</summary>
		[XmlIgnore]
		public object Value { get; set; }

		[XmlText(Type = typeof(string))]
		public string XmlValue {
			get { return Value != null ? Value.ToString() : null; }

			set {
				switch (Type) {
					case ParameterType.Int32: Value = int.Parse(value); break;
					case ParameterType.Double: Value = double.Parse(value); break;
				}
			}
		}

		public Parameter() {
		}

		public Parameter(string name, ParameterType type, object value = null)
			: this() {
			Name = name;
			Type = type;
			Value = value;
		}

		public Parameter(string name, double value) : this(name, ParameterType.Double, value) { }
		public Parameter(string name, int value) : this(name, ParameterType.Int32, value) { }
		public Parameter(string name, string value) : this(name, ParameterType.String, value) { }
	}

	[Serializable]
	public class ParameterCollection : ElementCollection<Parameter> {
		public ParameterCollection() { }
		public ParameterCollection(int capacity) : base(capacity) { }
		public ParameterCollection(IEnumerable<Parameter> collection) : base(collection) { }
		public ParameterCollection(params Parameter[] list) : base(list) { }
	}

	[Serializable]
	public enum ParameterType {
		[XmlEnum("int")]
		Int32,

		[XmlEnum("float")]
		Double,

		[XmlEnum("string")]
		String,
	}
}
