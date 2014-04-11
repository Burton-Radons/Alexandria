using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Glare.Graphics.Collada {
	/// <summary>Defines generic control information for dynamic content. </summary>
	/// <remarks>
	/// A controller is a device or mechanism that manages and directs the operations of another object. A <see cref="Controller"/> element is a general, generic mechanism for describing active or dynamic content. It contains elements that describe the manipulation of the data. The actual type and complexity of the data is represented in detail by the child elements. 
	/// 
	/// COLLADA describes two types of controllers for active mesh geometry in the visual scene: vertex skinning and mesh morphing. The controller concept is not limited to geometry and visualization, however, and other types of controllers may be introduced in future versions of the specification, which describe animation blending, physical simulation, dynamics, or user interaction.
	/// </remarks>
	[Serializable]
	public class Controller : IdElement, IAsset, IExtras, IName {
		internal const string XmlName = "controller";

		Asset asset;
		ControlElement element;
		ExtraCollection extras;

		/// <summary>Defines asset-management information regarding its parent element.</summary>
		[XmlElement(Asset.XmlName, Order = 0)]
		public Asset Asset {
			get { return asset; }
			set { SetElement(ref asset, value); }
		}

		/// <summary>The element that contains control data. Must be either a <see cref="Skin"/> or a <see cref="Morph"/>.</summary>
		[XmlElement(Skin.XmlName, typeof(Skin), Order = 1)]
		public ControlElement Element {
			get { return element; }
			set { SetElement(ref element, value); }
		}
		/// <summary>Provides arbitrary additional information about or related to its parent element.</summary>
		[XmlElement(Glare.Graphics.Collada.Extra.XmlName, Order = 2)]
		public ExtraCollection Extras {
			get { return GetCollection(ref extras); }
			set { SetCollection<Extra, ExtraCollection>(ref extras, value); }
		}

		/// <summary>Get or set the name of the object; optional.</summary>
		[XmlAttribute(Glare.Graphics.Collada.Element.XmlNameAttribute, DataType = "token")]
		public string Name { get; set; }
	}

	/// <summary>A control element for a <see cref="Controller"/>'s <see cref="Controller.Element"/> property. This may be a <see cref="Skin"/> or a <see cref="Morph"/>.</summary>
	[Serializable]
	public abstract class ControlElement : Element {
	}
}
