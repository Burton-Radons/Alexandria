using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Glare.Graphics.Collada {
	[Serializable]
	public abstract class Profile : IdElement, IAsset {
		Asset asset;

		/// <summary>Defines asset-management information regarding its parent element.</summary>
		[XmlElement(Asset.XmlName)]
		public Asset Asset {
			get { return asset; }
			set { SetElement(ref asset, value); }
		}
	}

	[Serializable]
	public class ProfileCollection : ElementCollection<Profile> {
		public ProfileCollection() { }
		public ProfileCollection(int capacity) : base(capacity) { }
		public ProfileCollection(IEnumerable<Profile> collection) : base(collection) { }
		public ProfileCollection(params Profile[] list) : base(list) { }
	}
}
