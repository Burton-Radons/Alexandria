using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Glare.Graphics.Collada {
	public abstract class Sampler : Element, IExtras {
		ExtraCollection extras;
		InstanceImage instanceImage;

		[XmlElement(InstanceImage.XmlName, Order = 0)]
		public InstanceImage InstanceImage {
			get { return instanceImage; }
			set { SetElement(ref instanceImage, value); }
		}

		/// <summary>Provides arbitrary additional information about or related to its parent element.</summary>
		[XmlElement(Glare.Graphics.Collada.Extra.XmlName, Order = 1)]
		public ExtraCollection Extras {
			get { return GetCollection(ref extras); }
			set { SetCollection<Extra, ExtraCollection>(ref extras, value); }
		}

		public Sampler() {
		}
	}

	public class Sampler2D : Sampler {
		internal const string XmlName = "sampler2D";

		public Sampler2D() { }
	}
}
