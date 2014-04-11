using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Glare.Graphics.Collada {
	[Serializable]
	public abstract class Library : IdElement, IAsset, IExtras, IName {
		Asset asset;
		ExtraCollection extras;

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

		/// <summary>Get or set the name of the object; optional.</summary>
		[XmlAttribute(Element.XmlNameAttribute, DataType = "token")]
		public string Name { get; set; }
	}

	[Serializable]
	public abstract class Library<TElement, TElementCollection> : Library
		where TElement : Element
		where TElementCollection : ElementCollection<TElement>, new() {
		[XmlIgnore]
		protected abstract TElementCollection Collection { get; set; }

		public void Add(TElement element) { Collection.Add(element); }
		public void AddRange(params TElement[] items) { Collection.AddRange(items); }
		public void AddRange(IEnumerable<TElement> items) { Collection.AddRange(items); }
	}

	/// <summary>Provides a library for the storage of <see cref="Image"/> assets. This is used as the <see cref="Collada.Images"/> property of <see cref="Collada"/>.</summary>
	[Serializable]
	public class LibraryImages : Library<Image, ImageCollection> {
		internal const string XmlName = "library_images";

		ImageCollection items;

		[XmlIgnore]
		protected override ImageCollection Collection { get { return Items; } set { Items = value; } }

		[XmlElement(Image.XmlName, Order = 1)]
		public ImageCollection Items {
			get { return GetCollection(ref items); }
			set { SetCollection<Image, ImageCollection>(ref items, value); }
		}

		public LibraryImages() { }
		public LibraryImages(params Image[] items) : this((IEnumerable<Image>)items) { }
		public LibraryImages(IEnumerable<Image> items) : this() { Items.AddRange(items); }
	}
}
