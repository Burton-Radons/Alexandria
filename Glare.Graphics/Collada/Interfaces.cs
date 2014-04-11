using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Glare.Graphics.Collada {
	public interface IAsset {
		/// <summary>Defines asset-management information regarding its parent element.</summary>
		[XmlElement(Asset.XmlName)]
		Asset Asset { get; set; }
	}

	public interface IId {
		/// <summary>Get or set the unique identifier of this element.</summary>
		[XmlAttribute(Element.XmlIdAttribute, DataType = "NCName")]
		string Id { get; set; }
	}

	public interface IName {
		/// <summary>Get or set the name of the object; optional.</summary>
		[XmlAttribute(Element.XmlNameAttribute, DataType = "token")]
		string Name { get; set; }
	}

	public interface IScopedId {
		/// <summary>A text string value containing the scoped identifier of this element. This value must be unique within the scope of the parent element. Optional. For details, see “Address Syntax” in Chapter 3: Schema Concepts.</summary>
		[XmlAttribute(Element.XmlScopedIdAttribute, DataType = "NCName")]
		string ScopedId { get; set; }
	}

	public interface IExtras {
		/// <summary>Provides arbitrary additional information about or related to its parent element.</summary>
		[XmlElement(Glare.Graphics.Collada.Extra.XmlName)]
		ExtraCollection Extras { get; set; }
	}
}
