using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Glare.Graphics.Collada {
	/// <summary>Instantiates a COLLADA effect.</summary>
	/// <remarks>
	/// An effect defines the equations necessary for the visual appearance of geometry and screen-space image processing. 
	/// 
	/// For details about instance elements in COLLADA, see “Instantiation and External Referencing” in Chapter 3: Schema Concepts. 
	/// 
	/// <see cref="InstanceEffect"/> instantiates an effect definition from the <see cref="Collada.Effects"/> and customizes its parameters. 
	/// 
	/// The <see cref="Url"/> attribute references the effect. 
	/// 
	/// <see cref="SetParameter"/>s assign values to specific effect and profile parameters that are unique to the instance.  <see cref="TechniqueHint"/>s indicate the desired or last-used technique inside an effect profile. This allows the user to maintain the same look-and-feel of the effect instance as the last time that the user used it. Some runtime render engines may choose new techniques on the fly, but it is important for some effects and for digital-content-creation consistency to maintain the same visual appearance during authoring.</remarks>
	public class InstanceEffect : ScopedIdElement, IName {
		internal const string XmlName = "instance_effect";

		/// <summary>Get or set the name of the object; optional.</summary>
		[XmlAttribute(Element.XmlNameAttribute, DataType = "token")]
		public string Name { get; set; }

		/// <summary>The URI of the location of the <see cref="Effect"/> element to instantiate. Required. Can refer to a local instance or external reference. For a local instance, this is a relative URI fragment identifier that begins with the “#” character. The fragment identifier is an XPointer shorthand pointer that consists of the ID of the element to instantiate. For an external reference, this is an absolute or relative URL.</summary>
		[XmlAttribute("url", DataType = "anyURI")]
		public string Url { get; set; }

		public InstanceEffect() { }

		public InstanceEffect(string url) {
			Url = url;
		}
	}
}
