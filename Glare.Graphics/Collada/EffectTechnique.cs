using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Glare.Graphics.Collada {
	[Serializable]
	public abstract class EffectTechnique : IdScopedIdElement,  IAsset, IExtras {
		Asset asset;
		ExtraCollection extras;

		/// <summary>Defines asset-management information regarding its parent element.</summary>
		[XmlElement(Asset.XmlName)]
		public Asset Asset {
			get { return asset; }
			set { SetElement(ref asset, value); }
		}

		/// <summary>Provides arbitrary additional information about or related to its parent element.</summary>
		[XmlElement(Glare.Graphics.Collada.Extra.XmlName)]
		public ExtraCollection Extras {
			get { return extras; }
			set { SetCollection<Extra, ExtraCollection>(ref extras, value); }
		}

		public EffectTechnique() {
			Extras = new ExtraCollection();
		}
	}
}
