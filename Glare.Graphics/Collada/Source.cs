using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Glare.Graphics.Collada {
	[Serializable]
	[XmlRoot(XmlName)]
	public class Source : IdElement, IAsset, IName {
		internal const string XmlName = "source";

		Asset asset;
		SourceTechniqueCommon commonTechnique;
		ArrayElement elements;

		/// <summary>Defines asset-management information regarding its parent element.</summary>
		[XmlElement(Asset.XmlName, Order = 0)]
		public Asset Asset {
			get { return asset; }
			set { SetElement(ref asset, asset); }
		}

		[XmlElement(BooleanArrayElement.XmlName, typeof(BooleanArrayElement), Order = 1)]
		[XmlElement(IdArrayElement.XmlName, typeof(IdArrayElement), Order = 1)]
		[XmlElement(Int32ArrayElement.XmlName, typeof(Int32ArrayElement), Order = 1)]
		[XmlElement(NameArrayElement.XmlName, typeof(NameArrayElement), Order = 1)]
		[XmlElement(ScopedIdArrayElement.XmlName, typeof(ScopedIdArrayElement), Order = 1)]
		[XmlElement(SingleArrayElement.XmlName, typeof(SingleArrayElement), Order = 1)]
		public ArrayElement Elements {
			get { return elements; }
			set { SetElement(ref elements, value); }
		}

		/// <summary>Get or set the name of the object; optional.</summary>
		[XmlAttribute(Element.XmlNameAttribute, DataType = "token")]
		public string Name { get; set; }

		/// <summary>Specifies source information for the common profile that all COLLADA implementations must support. See “The Common Profile” section for usage information. Optional.</summary>
		[XmlElement(SourceTechniqueCommon.XmlName, Order = 2)]
		public SourceTechniqueCommon TechniqueCommon {
			get { return commonTechnique; }
			set { SetElement(ref commonTechnique, value); }
		}

		public Source() : base() { }

		public Source(string id, ArrayElement elements = null, SourceTechniqueCommon commonTechnique = null)
			: this() {
			Id = id;
			Elements = elements;
			TechniqueCommon = commonTechnique;
		}
	}

	[Serializable]
	public class SourceCollection : ElementCollection<Source> {
		public SourceCollection() { }
		public SourceCollection(int capacity) : base(capacity) { }
		public SourceCollection(IEnumerable<Source> collection) : base(collection) { }
		public SourceCollection(params Source[] list) : base(list) { }
	}

	[Serializable]
	public class SourceTechniqueCommon : TechniqueCommon {
		Accessor accessor;
		Asset asset;

		[XmlElement(Accessor.XmlName, typeof(Accessor))]
		public Accessor Accessor {
			get { return accessor; }
			set { SetElement(ref accessor, value); }
		}

		[XmlElement(Asset.XmlName, typeof(Asset))]
		public Asset Asset {
			get { return asset; }
			set { SetElement(ref asset, value); }
		}

		public SourceTechniqueCommon() { }

		public SourceTechniqueCommon(Accessor accessor) {
			Accessor = accessor;
		}
	}
}
