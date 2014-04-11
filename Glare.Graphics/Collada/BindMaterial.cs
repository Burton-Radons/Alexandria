using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Glare.Graphics.Collada {
	[Serializable]
	public class BindMaterial : Element {
		internal const string XmlName = "bind_material";

		BindMaterialTechniqueCommon bindMaterialCommonTechnique;
		ParameterCollection parameters;

		/// <summary>Specifies material binding information for the common profile that all COLLADA implementations must support. See “The Common Profile” section for usage information.</summary>
		[XmlElement(BindMaterialTechniqueCommon.XmlName)]
		public BindMaterialTechniqueCommon CommonTechnique {
			get { return bindMaterialCommonTechnique; }
			set { SetElement(ref bindMaterialCommonTechnique, value); }
		}

		/// <summary>In <see cref="BindMaterial"/> these are added to be targets for  animation. These objects can then be bound to input  parameters in the normal manner without requiring the  animation targeting system to parse the internal layout of an <see cref="Effect"/>. See main entry in Core.</summary>
		[XmlElement(Parameter.XmlName)]
		public ParameterCollection Parameters {
			get { return parameters; }
			set { SetCollection<Parameter, ParameterCollection>(ref parameters, value); }
		}

		public BindMaterial() {
			Parameters = new ParameterCollection();
		}

		public BindMaterial(BindMaterialTechniqueCommon commonTechnique) : this() {
			CommonTechnique = commonTechnique;
		}
	}

	[Serializable]
	public class BindMaterialTechniqueCommon : TechniqueCommon {
		[XmlElement(InstanceMaterial.XmlName)]
		public List<InstanceMaterial> MaterialInstances { get; set; }

		public BindMaterialTechniqueCommon() {
			MaterialInstances = new List<InstanceMaterial>();
		}

		public BindMaterialTechniqueCommon(params InstanceMaterial[] materialInstances) {
			MaterialInstances = new List<InstanceMaterial>(materialInstances);
		}
	}
}
