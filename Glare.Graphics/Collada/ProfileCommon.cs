using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Glare.Graphics.Collada {
	/// <summary>Opens a block of platform-independent declarations for the common, fixed-function shader.</summary>
	[Serializable]
	public class ProfileCommon : Profile {
		internal const string XmlName = "profile_COMMON";

		NewParameterCollection parameters;
		ProfileCommonTechnique technique;

		[XmlElement(NewParameter.XmlName)]
		public NewParameterCollection Parameters {
			get { return parameters; }
			set { SetCollection<NewParameter, NewParameterCollection>(ref parameters, value); }
		}

		[XmlElement("technique")]
		public ProfileCommonTechnique Technique {
			get { return technique; }
			set { SetElement(ref technique, value); }
		}

		public ProfileCommon() {
			Parameters = new NewParameterCollection();
		}

		public ProfileCommon(ProfileCommonTechnique technique)
			: this() {
			Technique = technique;
		}
	}

	[Serializable]
	public class ProfileCommonTechnique : EffectTechnique {
		GenericShaderCollection shaders;

		[XmlElement(PhongShader.XmlName, typeof(PhongShader))]
		public GenericShaderCollection Shaders {
			get { return shaders; }
			set { SetCollection<ProfileCommonShader, GenericShaderCollection>(ref shaders, value); }
		}

		public ProfileCommonTechnique() {
			Shaders = new GenericShaderCollection();
		}

		public ProfileCommonTechnique(string scopedId, params ProfileCommonShader[] shaders)
			: this() {
			ScopedId = scopedId;
			Shaders = new GenericShaderCollection(shaders);
		}
	}
}
