using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Glare.Graphics.Collada {
	/// <summary>Declares the storage for the graphical representation of an object. </summary>
	/// <remarks>Digital imagery comes in three main forms of data: raster, vector, and hybrid. Raster imagery comprises a sequence of brightness or color values, called picture elements (pixels), that together form the complete picture. Vector imagery uses mathematical formulae for curves, lines, and shapes to describe a picture or drawing. Hybrid imagery combines both raster and vectorinformation, leveraging their respective strengths, to describe the picture. 
	/// 
	/// The <see cref="Image"/> element best describes raster image data, but can conceivably handle other forms of imagery. Raster imagery datais typically organized in n-dimensional arrays. This array organization can be leveraged by texture look-up functions to access noncolor values such asdisplacement, normal, or height field values.
	/// 
	/// This is a contained in a <see cref="LibraryImages"/> library.
	/// </remarks>
	[Serializable]
	public class Image : IdScopedIdElement, IAsset, IExtras, IName {
		internal const string XmlName = "image";

		Asset asset;
		ExtraCollection extras;
		InitializeFrom initializeFrom;

		/// <summary>Defines asset-management information regarding its parent element.</summary>
		[XmlElement(Asset.XmlName, Order = 0)]
		public Asset Asset {
			get { return asset; }
			set { SetElement(ref asset, value); }
		}

		/// <summary>Provides arbitrary additional information about or related to its parent element.</summary>
		[XmlElement(Glare.Graphics.Collada.Extra.XmlName, Order = 2)]
		public ExtraCollection Extras {
			get { return GetCollection(ref extras); }
			set { SetCollection<Extra, ExtraCollection>(ref extras, value); }
		}

		[XmlElement(InitializeFrom.XmlName, typeof(InitializeFrom), Order = 1)]
		public InitializeFrom InitializeFrom {
			get { return initializeFrom; }
			set { SetElement(ref initializeFrom, value); }
		}

		/// <summary>Get or set the name of the object; optional.</summary>
		[XmlAttribute(Element.XmlNameAttribute, DataType = "token")]
		public string Name { get; set; }
	}

	[Serializable]
	public class ImageCollection : ElementCollection<Image> {
		public ImageCollection() { }
		public ImageCollection(int capacity) : base(capacity) { }
		public ImageCollection(IEnumerable<Image> collection) : base(collection) { }
		public ImageCollection(params Image[] list) : base(list) { }
	}
}
