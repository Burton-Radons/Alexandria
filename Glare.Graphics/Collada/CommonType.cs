using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Glare.Graphics.Collada {
	/// <summary>Used within <see cref="ProfileCommonShader"/> effects.</summary>
	[Serializable]
	public abstract class CommonType : Element {
	}

	/// <summary>A type that describes color attributes offixed-function shader elements inside <see cref="ProfileCommon"/> effects, under the <see cref="ProfileCommonShader"/> types.</summary>
	/// <remarks>There are implicit casts to <see cref="CommonType"/> from <see cref="Vector4d"/> to represent a color.</remarks>
	[Serializable]
	public class CommonColorOrTextureType : CommonType {
		internal const string XmlName = "fx_common_color_or_texture_type";

		CommonTexture texture;

		/// <summary>The value is a literal color, specified by four floatingpoint numbers in RGBA order.</summary>
		[XmlIgnore]
		public Vector4d Color { get; set; }

		/// <summary>Specifies from which channel to take transparency information. For additional information, see “Determining Transparency (Opacity)” in Chapter 7: Getting Started with FX.</summary>
		[XmlAttribute("opaque")]
		[DefaultValue(CommonColorOrTextureTypeOpaque.AlphaOne)]
		public CommonColorOrTextureTypeOpaque Opaque { get; set; }

		[XmlElement(CommonTexture.XmlName)]
		public CommonTexture Texture {
			get { return texture; }
			set { SetElement(ref texture, value); }
		}

		/// <summary>A string-based alias for <see cref="Color"/>, used exclusively for XML serialization.</summary>
		[XmlElement("color")]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public string XmlColor {
			get {
				if (texture != null)
					return null;
				return string.Format("{0} {1} {2} {3}", Color.X, Color.Y, Color.Z, Color.W);
			}

			set {
				List<double> list = new List<double>();
				DoubleArrayElement.StringToCollection(value, list);
				if (list.Count != 4)
					throw new ArgumentException("value");
				Color = new Vector4d(list[0], list[1], list[2], list[3]);
			}
		}

		public CommonColorOrTextureType() {
		}

		public CommonColorOrTextureType(Vector4d color) {
			Color = color;
		}

		public CommonColorOrTextureType(CommonTexture texture) {
			Texture = texture;
		}

		public static implicit operator CommonColorOrTextureType(Vector4d color) { return new CommonColorOrTextureType(color); }
		public static implicit operator CommonColorOrTextureType(CommonTexture texture) { return new CommonColorOrTextureType(texture); }
	}

	/// <summary>A <see cref="Sampler2D"/> reference that is used by a <see cref="CommonColorOrTextureType"/> in its <see cref="CommonColorOrTextureType.Texture"/> property.</summary>
	[Serializable]
	public class CommonTexture : Element, IExtras {
		internal const string XmlName = "texture";

		ExtraCollection extras;

		/// <summary>Provides arbitrary additional information about or related to its parent element.</summary>
		[XmlElement(Glare.Graphics.Collada.Extra.XmlName)]
		public ExtraCollection Extras {
			get { return GetCollection(ref extras); }
			set { SetCollection<Extra, ExtraCollection>(ref extras, value); }
		}

		/// <summary>An identifier for a previously defined <see cref="Sampler2D"/> object.</summary>
		[XmlAttribute("texture", DataType = "NCName")]
		public string Texture { get; set; }

		/// <summary>A semantic token which is referenced within a <see cref="BindMaterial"/> to bind an array of texture coordinates from a <see cref="InstanceGeometry"/> to the sampler.</summary>
		[XmlAttribute("texcoord", DataType = "NCName")]
		public string TextureCoordinate { get; set; }

		public CommonTexture() { }

		public CommonTexture(string texture, string textureCoordinate)
			: this() {
			Texture = texture;
			TextureCoordinate = textureCoordinate;
		}
	}

	/// <summary>Specifies from which channel to take transparency information. For additional information, see “Determining Transparency (Opacity)” in Chapter 7: Getting Started with FX.</summary>
	[Serializable]
	public enum CommonColorOrTextureTypeOpaque {
		/// <summary>Takes the transparency information from the color’s alpha channel, where the value 1.0 is opaque. This is the default.</summary>
		[XmlEnum("A_ONE")]
		AlphaOne,

		/// <summary>Takes the transparency information from the color’s red, green, and blue channels, where the value 0.0 is opaque, with each channel modulated independently.</summary>
		[XmlEnum("RGB_ZERO")]
		RgbZero,

		/// <summary>Takes the transparency information from the color’s alpha channel, where the value 0.0 is opaque. </summary>
		[XmlEnum("A_ZERO")]
		AlphaZero,

		/// <summary>Takes the transparency information from the color’s red, green, and blue channels, where the value 1.0 is opaque, with each channel modulated independently.</summary>
		[XmlEnum("RGB_ONE")]
		RgbOne,
	}

	/// <summary>A type that describes the scalar attributes of fixed-function shader elements inside <see cref="ProfileCommon"/> effects, used in the <see cref="ProfileCommonShader"/> types.</summary>
	/// <remarks>There are implicit casts to <see cref="CommonType"/> from <see cref="Double"/> to represent a value.</remarks>
	[Serializable]
	public class CommonFloatOrParamType : CommonType {
		internal const string XmlName = "fx_common_float_or_param_type";

		[XmlElement("float")]
		public Double Float { get; set; }

		public CommonFloatOrParamType() { }

		public CommonFloatOrParamType(double floatValue)
			: this() {
			Float = floatValue;
		}

		public static implicit operator CommonFloatOrParamType(double value) { return new CommonFloatOrParamType(value); }
	}
}
