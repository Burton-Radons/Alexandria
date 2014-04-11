using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Glare.Graphics.Collada {
	/// <summary>Provides arbitrary additional information about or related to its parent element.</summary>
	/// <remarks>
	/// An extensible schema requires a means for users to specify arbitrary information. This extra information can represent additional real data or semantic (meta) data tothe application. 
	/// 
	/// COLLADA represents extra information as techniques containing arbitrary XML elements and data. 
	/// </remarks>
	[Serializable]
	public class Extra : IdElement, IAsset, IName {
		internal const string XmlName = "extra";

		Asset asset;
		TechniqueCollection techniques;

		/// <summary>Defines asset-management information regarding its parent element.</summary>
		[XmlElement(Asset.XmlName, Order = 0)]
		public Asset Asset {
			get { return asset; }
			set { SetElement(ref asset, value); }
		}

		/// <summary>Get or set the name of the object; optional.</summary>
		[XmlAttribute(Element.XmlNameAttribute, DataType = "token")]
		public string Name { get; set; }

		[XmlElement(Technique.XmlName, Order = 1)]
		public TechniqueCollection Techniques {
			get { return GetCollection(ref techniques); }
			set { SetCollection<Technique, TechniqueCollection>(ref techniques, value); }
		}

		/// <summary>A hint as to the type of information that the particular <see cref="Extra"/> element represents. This text string must be understood by the application. Optional. </summary>
		[XmlAttribute("type", DataType = "NMTOKEN")]
		public string Type { get; set; }

		public Extra() { }

		public Extra(params Technique[] techniques) : this() {
			Techniques.AddRange(techniques);
		}
	}

	[Serializable]
	public class ExtraCollection : ElementCollection<Extra> {
		public ExtraCollection() { }
		public ExtraCollection(int capacity) : base(capacity) { }
		public ExtraCollection(IEnumerable<Extra> collection) : base(collection) { }
		public ExtraCollection(params Extra[] list) : base(list) { }
	}	
}
