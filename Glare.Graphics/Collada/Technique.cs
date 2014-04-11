using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Glare.Graphics.Collada {
	/// <summary>Declares the information used to process some portion of the content.</summary>
	/// <remarks>
	/// A technique describes information needed by a specific platform or program. The platform or program is specified with the profileattribute. Each technique conforms to an associated profile. Two things define the context for a technique: its profile and its parent element in the instance document. 
	/// 
	/// Techniques generally act as a “switch”. If more than one is present for a particular portion of content on import, one or the other is picked, but usually not both. Selection should be based on which profile the importing application can support. 
	/// 
	/// Techniques contain application data and programs, making them assets that can be managed as a unit. 
	/// </remarks>
	public class Technique : Element {
		internal const string XmlName = "technique";

		ObservableCollection<XmlElement> elements;
		ParameterCollection parameters;

		[XmlElement(Parameter.XmlName)]
		public ParameterCollection Parameters {
			get { return GetCollection(ref parameters); }
			set { SetCollection<Parameter, ParameterCollection>(ref parameters, value); }
		}

		/// <summary>The type of profile. This is a vendor-defined character string that indicates the platform or capability target for the technique. Required. </summary>
		[XmlAttribute("profile", DataType = "NMTOKEN")]
		public string Profile { get; set; }

		/// <summary>This XML Schema namespace attribute identifies an additional schema to use for validating the content of thisinstance document. Optional.</summary>
		[XmlAttribute("xmlns", DataType = "anyURI")]
		public string XmlNamespace { get; set; }

		[XmlAnyElement]
		public ObservableCollection<XmlElement> Elements {
			get { return GetCollection(ref elements); }
			set { elements = value; }
		}

		public Technique() { }

		public Technique(string profile, params XmlElement[] elements) {
			Profile = profile;
			foreach(XmlElement element in elements)
				Elements.Add(element);
		}
	}

	[Serializable]
	public class TechniqueCollection : ElementCollection<Technique> {
		public TechniqueCollection() { }
		public TechniqueCollection(int capacity) : base(capacity) { }
		public TechniqueCollection(IEnumerable<Technique> collection) : base(collection) { }
		public TechniqueCollection(params Technique[] list) : base(list) { }
	}
}
