using Glare.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Glare.Graphics.Collada {
	/// <summary>Describes a stream of values from an array data source.</summary>
	[Serializable]
	public class Accessor : Element {
		internal const string XmlName = "accessor";

		internal const string XmlCountAttribute = "count";
		internal const string XmlOffsetAttribute = "offset";
		internal const string XmlSourceAttribute = "source";
		internal const string XmlStrideAttribute = "stride";

		ParameterCollection parameters;

		/// <summary>The number of times the array is accessed. Required</summary>
		[XmlAttribute("count")]
		public int Count { get; set; }

		/// <summary>The index of the first value to be read from the array. The default is 0. Optional.</summary>
		[XmlAttribute("offset")]
		public int Offset { get; set; }

		[XmlElement("param")]
		public ParameterCollection Parameters {
			get { return parameters; }
			set { SetCollection<Parameter, ParameterCollection>(ref parameters, value); }
		}

		/// <summary>The location of the array to access using a URI expression. Required. This element may refer to a COLLADA array element or to an array data source outside the scope of the instance document; the source does not need to be a COLLADA document.</summary>
		[XmlAttribute("source", DataType = "anyURI")]
		public string Source { get; set; }

		/// <summary>The number of values that are to be considered a unit during each access to the array. The default is 1, indicating that a single value is accessed. Optional.</summary>
		[XmlAttribute("stride")]
		[DefaultValue(1)]
		public int Stride { get; set; }

		public Accessor() {
			Stride = 1;
			Parameters = new ParameterCollection();
		}

		public Accessor(string source, int count, int stride, int offset, params Parameter[] parameters) {
			Source = source;
			Count = count;
			Stride = stride;
			Offset = offset;
			Parameters = new ParameterCollection(parameters);
		}
	}
}
