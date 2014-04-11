using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare
{
	/// <summary>
	/// An object that can be named with <see cref="Name"/> and has a dictionary of tags with <see cref="Tags"/>.
	/// </summary>
	public abstract class NamedObject
	{
		readonly Dictionary<object, object> tags = new Dictionary<object, object>();
		string debugName;

		/// <summary>Get or set the name to use when debugging. If this is <c>null</c>, <see cref="Name"/> is returned on get.</summary>
		public string DebugName { get { return debugName ?? Name; } set { debugName = value; } }

		/// <summary>Get or set the name of the object.</summary>
		public string Name { get; set; }

		/// <summary>Get a dictionary of user-defined tags.</summary>
		public Dictionary<object, object> Tags { get { return tags; } }

		public override string ToString()
		{
			return "{" + GetType().Name + (DebugName != null ? " '" + DebugName + "'" : "") + "}";
		}
	}
}
