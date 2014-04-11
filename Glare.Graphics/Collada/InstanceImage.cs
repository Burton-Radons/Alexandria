using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Glare.Graphics.Collada {
	/// <summary>Instantiates an image to use in a shader. </summary>
	/// <remarks>Typically for use in an effect for shading a geometric surface. However, an image can also be used as a target for rendering. This way, the picture or datainside the image can be updated dynamically with advanced FX shading techniques. An image that is the target for rendering, however, must contain the <see cref="Renderable"/> element.</remarks>
	public class InstanceImage : ScopedIdElement, IExtras, IName {
		internal const string XmlName = "instance_image";

		ExtraCollection extras;

		/// <summary>Provides arbitrary additional information about or related to its parent element.</summary>
		[XmlElement(Glare.Graphics.Collada.Extra.XmlName)]
		public ExtraCollection Extras {
			get { return GetCollection(ref extras); }
			set { SetCollection<Extra, ExtraCollection>(ref extras, extras); }
		}

		/// <summary>Get or set the name of the object; optional.</summary>
		[XmlAttribute(Element.XmlNameAttribute, DataType = "token")]
		public string Name { get; set; }

		/// <summary>Required. The URI of the image asset.</summary>
		[XmlAttribute("url", DataType = "anyURI")]
		public string Url { get; set; }

		public InstanceImage() { }

		public InstanceImage(string url)
			: this() {
			Url = url;
		}
	}
}
