using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moki.Floop {
	/// <summary>
	/// An <see cref="Attribute"/> attached to <see cref="Value&ltT&gt;"/> properties that describe the value's parameters.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class StructurePropertyAttribute : Attribute {
		public ByteOrder ByteOrder { get; private set; }

		public StructurePropertyAttribute(ByteOrder byteOrder) {
			ByteOrder = byteOrder;
		}
	}
}
