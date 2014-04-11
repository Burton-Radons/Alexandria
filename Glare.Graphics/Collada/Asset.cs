using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using System.Diagnostics;

namespace Glare.Graphics.Collada {
	/// <summary>Defines asset-management information regarding its parent element. </summary>
	[Serializable]
	public class Asset : Element, IExtras {
		internal const string XmlName = "asset";

		AssetContributorCollection contributors;
		AssetCoverage coverage;
		ExtraCollection extras;
		AssetUnit unit;

		[XmlElement(AssetContributor.XmlName)]
		public AssetContributorCollection Contributors {
			get { return contributors; }
			set { SetCollection<AssetContributor, AssetContributorCollection>(ref contributors, value); }
		}

		/// <summary>Provides information about the location of the visual scene in physical space.</summary>
		[XmlElement(AssetCoverage.XmlName)]
		public AssetCoverage Coverage {
			get { return coverage; }
			set { SetElement(ref coverage, value); }
		}

		/// <summary>Contains the date and time that the parent element was created.</summary>
		[XmlElement("created")]
		public DateTime Created { get; set; }

		/// <summary>Provides arbitrary additional information about or related to its parent element.</summary>
		[XmlElement(Glare.Graphics.Collada.Extra.XmlName)]
		public ExtraCollection Extras {
			get { return extras; }
			set { SetCollection<Extra, ExtraCollection>(ref extras, value); }
		}

		[XmlElement("keywords", Type = typeof(string))]
		[DefaultValue("")]
		public List<string> Keywords { get; set; }

		[XmlElement("modified")]
		public DateTime Modified { get; set; }

		[XmlElement("revision")]
		public string Revision { get; set; }

		[XmlElement("subject")]
		public string Subject { get; set; }

		[XmlElement("title")]
		public string Title { get; set; }

		/// <summary>Defines unit of distance for COLLADA elements and objects. This unit of distance applies to all spatial measurements within the scope of the <see cref="Asset"/>’s parent element, unless overridden by a more local <see cref="Asset"/> and <see cref="AssetUnit"/>.</summary>
		[XmlElement(AssetUnit.XmlName)]
		[DefaultValue(typeof(AssetUnit), "meter 1.0")]
		public AssetUnit Unit {
			get { return unit; }
			set { SetElement(ref unit, value); }
		}

		[XmlElement("up_axis")]
		[DefaultValue(UpAxis.Y)]
		public UpAxis UpAxis { get; set; }

		public Asset() {
			Contributors = new AssetContributorCollection();
			Created = Modified = DateTime.UtcNow;
			Extras = new ExtraCollection();
			Keywords = new List<string>();
			Unit = new AssetUnit();
			UpAxis = UpAxis.Y;
		}
	}

	[Serializable]
	public class AssetCollection : ElementCollection<Asset> {
		public AssetCollection() { }
		public AssetCollection(int capacity) : base(capacity) { }
		public AssetCollection(IEnumerable<Asset> collection) : base(collection) { }
		public AssetCollection(params Asset[] list) : base(list) { }
	}

	[Serializable]
	public enum UpAxis {
		[XmlEnum("X_UP")]
		X,

		[XmlEnum("Y_UP")]
		Y,

		[XmlEnum("Z_UP")]
		Z,
	}

	[Serializable]
	public class AssetContributor : Element {
		internal const string XmlName = "contributor";

		/// <summary>Contains a string with the author’s name.</summary>
		[XmlElement("author")]
		public string Author { get; set; }

		/// <summary>Contains a string with the author’s full email address compliant with RFC 2822 section 3.4.</summary>
		[XmlElement("author_email")]
		public string AuthorEmail { get; set; }

		/// <summary>Contains a URL of this contributor’s website.</summary>
		[XmlElement("author_website", DataType = "anyURI")]
		public string AuthorWebsite { get; set; }

		/// <summary>Contains a string with the name of the authoring tool.</summary>
		[XmlElement("authoring_tool")]
		public string AuthoringTool { get; set; }

		/// <summary>Contains a string with comments from this contributor.</summary>
		[XmlElement("comments")]
		public string Comments { get; set; }

		/// <summary>Contains a string with copyright information.</summary>
		[XmlElement("copyright")]
		public string Copyright { get; set; }

		/// <summary>Contains a URI reference to the source data used for this asset.</summary>
		[XmlElement("source_data", DataType = "anyURI")]
		public string SourceData { get; set; }		
	}

	[Serializable]
	public class AssetContributorCollection : ElementCollection<AssetContributor> {
		public AssetContributorCollection() { }
		public AssetContributorCollection(int capacity) : base(capacity) { }
		public AssetContributorCollection(IEnumerable<AssetContributor> collection) : base(collection) { }
		public AssetContributorCollection(params AssetContributor[] list) : base(list) { }
	}

	[Serializable]
	[TypeConverter(typeof(AssetUnitTypeConverter))]
	public class AssetUnit : Element, IName {
		internal const string XmlName = "unit";

		/// <summary>How many real-world meters in one distance unit as a floating-point number. For example, 1.0 for the name "meter"; 1000 for the name "kilometer"; 0.3048 for the name "foot". For more information, see “About Physical Units” in “Chapter 6: Physics Reference”.</summary>
		[XmlAttribute("meter")]
		[DefaultValue(1)]
		public double Meter { get; set; }

		/// <summary>The name of the distance unit. For example, “meter”, “centimeter”, “inches”, or “parsec”. This can be the real name of a measurement, or an imaginary name.</summary>
		[XmlAttribute(Element.XmlNameAttribute, DataType = "token")]
		[DefaultValue("meter")]
		public string Name { get; set; }

		public AssetUnit() {
			Name = "meter";
			Meter = 1.0;
		}

		public AssetUnit(string name, double meter) {
			Name = name;
			Meter = meter;
		}

		public override bool Equals(object obj) {
			if (obj is AssetUnit) {
				AssetUnit other = (AssetUnit)obj;
				return Meter == other.Meter && Name == other.Name;
			}
			return base.Equals(obj);
		}

		public override int GetHashCode() {
			return Meter.GetHashCode() ^ (Name != null ? Name.GetHashCode() : -1);
		}

		public static bool operator ==(AssetUnit a, AssetUnit b) { return (a == null && b == null) || (a != null && a.Equals(b)); }
		public static bool operator !=(AssetUnit a, AssetUnit b) { return !(a == b); }
	}

	class AssetUnitTypeConverter : TypeConverter {
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) {
			if (sourceType == typeof(string))
				return true;
			return base.CanConvertFrom(context, sourceType);
		}

		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) {
			if (destinationType == typeof(AssetUnit))
				return true;
			return base.CanConvertTo(context, destinationType);
		}

		public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value) {
			if (value is string) {
				string[] split = ((string)value).Split(' ');
				return new AssetUnit(XmlConvert.DecodeName(split[0]), double.Parse(split[1]));
			}

			return base.ConvertFrom(context, culture, value);
		}

		public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType) {
			if (value is AssetUnit && destinationType == typeof(string)) {
				AssetUnit unit = (AssetUnit)value;
				return string.Format("{0} {1}", XmlConvert.EncodeName(unit.Name), unit.Meter);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}

	/// <summary>Provides information about the location of the visual scene in physical space.</summary>
	[Serializable]
	public class AssetCoverage : Element {
		internal const string XmlName = "coverage";

		AssetGeographicalLocation geographicalLocation;

		[XmlElement(AssetGeographicalLocation.XmlName)]
		public AssetGeographicalLocation GeographicalLocation {
			get { return geographicalLocation; }
			set { SetElement(ref geographicalLocation, value); }
		}
	}

	/// <summary>Defines geographic location information regarding the parent of the <see cref="Asset"/> element in which it resides.</summary>
	[Serializable]
	public class AssetGeographicalLocation : Element {
		internal const string XmlName = "geographical_location";

		AssetAltitude altitude;

		[XmlElement(AssetAltitude.XmlName)]
		public AssetAltitude Altitude {
			get { return altitude; }
			set { SetElement(ref altitude, value); }
		}

		/// <summary>Contains a floating-point number that specifies the latitude of the asset as defined by the WGS 84 world geodetic system. Valid values range from -90.0 to 90.0.</summary>
		[XmlAttribute("latitude")]
		public double Latitude { get; set; }

		/// <summary>Contains a floating-point number that specifies the longitude of the asset as defined by the WGS 84 world geodetic system. Valid values range from -180.0 to 180.0.</summary>
		[XmlAttribute("longitude")]
		public double Longitude { get; set; }

		public AssetGeographicalLocation() {
		}
	}

	/// <summary>Specifies the altitude of the asset as a floating-point number. Altitude follows the Keyhole Markup Language (KML) standard rather than the WGS 84 calculation of height. That is, it can be relative to terrain, or relative to sea level.</summary>
	public class AssetAltitude : Element {
		internal const string XmlName = "altitude";

		/// <summary>The altitude value in metres or units (the standard doesn't specify).</summary>
		[XmlIgnore]
		public double Altitude { get; set; }

		/// <summary>Indicates whether the altitude value should be interpreted as the distance in meters from sea level or from the altitude of the terrain at the latitude/longitude point.</summary>
		[XmlAttribute("mode")]
		public AssetAltitudeMode Mode{ get; set; }

		[XmlText]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public string XmlAltitudeText {
			get { return Altitude.ToString(); }
			set { Altitude = double.Parse(value); }
		}

		public AssetAltitude() {
			// Mode is required even though it has a default. Obviously a bug in the spec, but we use it.
			Mode = AssetAltitudeMode.RelativeToGround;
		}
	}

	/// <summary>Indicates whether the altitude value should be interpreted as the distance in meters from sea level or from the altitude of the terrain at the latitude/longitude point.</summary>
	[Serializable]
	public enum AssetAltitudeMode {
		[XmlEnum("absolute")]
		Absolute,

		[XmlEnum("relativeToGround")]
		RelativeToGround,
	}
}
