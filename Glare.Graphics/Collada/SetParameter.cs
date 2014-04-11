using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics.Collada {
	/// <summary>Assign a new value to a previously defined parameter.</summary>
	public class SetParameter : ScopedIdElement {
		internal const string XmlName = "setparam";
	}

	[Serializable]
	public class SetParameterCollection : ElementCollection<SetParameter> {
		public SetParameterCollection() { }
		public SetParameterCollection(int capacity) : base(capacity) { }
		public SetParameterCollection(IEnumerable<SetParameter> collection) : base(collection) { }
		public SetParameterCollection(params SetParameter[] list) : base(list) { }
	}
}
