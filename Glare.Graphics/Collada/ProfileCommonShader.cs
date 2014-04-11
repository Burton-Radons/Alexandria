using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Glare.Graphics.Collada {
	public class ProfileCommonShader : Element {

	}

	[Serializable]
	public class GenericShaderCollection : ElementCollection<ProfileCommonShader> {
		public GenericShaderCollection() { }
		public GenericShaderCollection(int capacity) : base(capacity) { }
		public GenericShaderCollection(IEnumerable<ProfileCommonShader> collection) : base(collection) { }
		public GenericShaderCollection(params ProfileCommonShader[] list) : base(list) { }
	}

	/// <summary>Produces a shaded surface where the specular reflection is shaded according the Phong BRDF approximation.</summary>
	/// <remarks>
	/// Used inside a <see cref="ProfileCommon"/> effect, declares a fixed-function pipeline that produces a specularly shaded surface that reflects ambient, diffuse, and specular reflection, where the specular reflection is shaded according the Phong BRDF approximation. 
	/// The <see cref="PhongShader"/> uses the common Phong shading equation, that is: color = <see cref="Emission"/> + <see cref="Ambient"/> * ambientLight + <see cref="Diffuse"/> * max(dot(normal, light), 0) + <see cref="Specular"/> * pow(max(dot(reflection, eye), 0), <see cref="Shininess"/>)
	/// where: 
	/// • ambientLight – A constant amount of ambient light contribution coming from the scene. In the COMMON profile, this is the sum of all the <light><technique_common><ambient><color>values in the <visual_scene>. 
	/// • normal – Normal vector 
	/// • light – Light vector 
	/// • eye – Eye vector 
	/// • reflection – Perfect reflection vector (reflect (light around normal)) 
	/// </remarks>
	[Serializable]
	public class PhongShader : ProfileCommonShader {
		internal const string XmlName = "phong";

		CommonColorOrTextureType emission, ambient, diffuse, specular, reflective, transparent, indexOfRefraction;
		CommonFloatOrParamType shininess, reflectivity, transparency;

		/// <summary>Declares the amount of light emitted from the surface of this object. </summary>
		[XmlElement("emission", Order = 0)]
		public CommonColorOrTextureType Emission {
			get { return emission; }
			set { SetElement(ref emission, value); }
		}

		/// <summary>Declares the amount of ambient light emitted from the surface of this object.</summary>
		[XmlElement("ambient", Order = 1)]
		public CommonColorOrTextureType Ambient {
			get { return ambient; }
			set { SetElement(ref ambient, value); }
		}

		/// <summary>Declares the amount of light diffusely reflected from the surface of this object. </summary>
		[XmlElement("diffuse", Order = 2)]
		public CommonColorOrTextureType Diffuse {
			get { return diffuse; }
			set { SetElement(ref diffuse, value); }
		}

		/// <summary>Declares the color of light specularly reflected from the surface of this object.</summary>
		[XmlElement("specular", Order = 3)]
		public CommonColorOrTextureType Specular {
			get { return specular; }
			set { SetElement(ref specular, value); }
		}

		/// <summary>Declares the specularity or roughness of the specular reflection lobe. </summary>
		[XmlElement("shininess", Order = 4)]
		public CommonFloatOrParamType Shininess {
			get { return shininess; }
			set { SetElement(ref shininess, value); }
		}

		/// <summary>Declares the color of a perfect mirror reflection.</summary>
		[XmlElement("reflective", Order = 5)]
		public CommonColorOrTextureType Reflective {
			get { return reflective; }
			set { SetElement(ref reflective, value); }
		}

		/// <summary>Declares the amount of perfect mirror reflection to be added to the reflected light as a value between 0.0 and 1.0.</summary>
		[XmlElement("reflectivity", Order = 6)]
		public CommonFloatOrParamType Reflectivity {
			get { return reflectivity; }
			set { SetElement(ref reflectivity, value); }
		}

		/// <summary>Declares the color of perfectly refracted light. See “Determining Transparency (Opacity)” in Chapter 7: Getting Started with FX.</summary>
		[XmlElement("transparent", Order = 7)]
		public CommonColorOrTextureType Transparent {
			get { return transparent; }
			set { SetElement(ref transparent, value); }
		}

		/// <summary>Declares the amount of perfectly refracted light added to the reflected color as a scalar value between 0.0 and 1.0.</summary>
		[XmlElement("transparency", Order = 8)]
		public CommonFloatOrParamType Transparency {
			get { return transparency; }
			set { SetElement(ref transparency, value); }
		}

		/// <summary>Declares the index of refraction for perfectly refracted light as a single scalar index.</summary>
		[XmlElement("indexOfRefraction", Order = 9)]
		public CommonColorOrTextureType IndexOfRefraction {
			get { return indexOfRefraction; }
			set { SetElement(ref indexOfRefraction, value); }
		}
	}
}
