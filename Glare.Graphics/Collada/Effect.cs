using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Glare.Graphics.Collada {
	/// <summary>Provides a self-contained description of a COLLADA effect.</summary>
	/// <remarks>
	/// An effect defines the equations necessary for the visual appearance of geometry and screen-space image processing. 
	/// 
	/// Programmable pipelines allow stages of the 3D pipeline to be programmed using high-level languages. These shaders often require very specific data to be passed to them and require the rest of the 3D pipeline to be set up in a particular way in order to function. Shader Effects is a way of describing not only shaders, but also the environment in which they will execute. The environment requires description of images, samplers, shaders, input and output parameters, uniform parameters, and render-state settings. 
	/// 
	/// Additionally, some algorithms require several passes to render the effect. This is supported by breaking pipeline descriptions into an ordered collection of <see cref="Pass"/> objects. These are grouped into <see cref="Technique"/>s that describe one of several ways of generating an effect. 
	/// 
	/// Elements inside the <see cref="Effect"/> declaration assume the use of an underlying library of code that handles the creation, use, and management of shaders, source code, parameters, etc. We shall refer to this underlying library as the “FX Runtime”. 
	/// 
	/// Parameters declared inside the <see cref="Effect"/> element but outside of any <see cref="Profile"/> element are said to be in “<see cref="Effect"/> scope”. Parameters inside <see cref="Effect"/> scope can be drawn only from a constrained list of basic data types and, after declaration, are available to <see cref="Shader"/>s and declarations across all profiles. <see cref="Effect"/> scope provides a handy way to parameterize many profiles and techniques with a single parameter. 
	/// </remarks>
	[Serializable]
	public class Effect : IdElement, IExtras, IName {
		internal const string XmlName = "effect";

		ExtraCollection extras;
		NewParameterCollection parameters;
		ProfileCollection profiles;

		/// <summary>Provides arbitrary additional information about or related to its parent element.</summary>
		[XmlElement(Glare.Graphics.Collada.Extra.XmlName, Order = 2)]
		public ExtraCollection Extras {
			get { return GetCollection(ref extras); }
			set { SetCollection<Extra, ExtraCollection>(ref extras, value); }
		}

		/// <summary>Get or set the name of the object; optional.</summary>
		[XmlAttribute(Element.XmlNameAttribute, DataType = "token")]
		public string Name { get; set; }

		/// <summary>Creates a new, named parameter object, and assigns it a type and an initial value. </summary>
		[XmlElement(NewParameter.XmlName, Order = 0)]
		public NewParameterCollection Parameters {
			get { return GetCollection(ref parameters); }
			set { SetCollection<NewParameter, NewParameterCollection>(ref parameters, value); }
		}


		[XmlElement(ProfileCommon.XmlName, typeof(ProfileCommon), Order = 1)]
		public ProfileCollection Profiles {
			get { return GetCollection(ref profiles); }
			set { SetCollection<Profile, ProfileCollection>(ref profiles, value); }
		}

		public Effect() { }

		public Effect(string id) : this() {
			Id = id;
		}
	}

	[Serializable]
	public class EffectCollection : ElementCollection<Effect> {
		public EffectCollection() { }
		public EffectCollection(int capacity) : base(capacity) { }
		public EffectCollection(IEnumerable<Effect> collection) : base(collection) { }
		public EffectCollection(params Effect[] list) : base(list) { }
	}
}
