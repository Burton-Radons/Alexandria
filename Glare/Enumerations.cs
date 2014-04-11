using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare
{
	/// <summary>
	/// Specifies how two objects intersect.
	/// </summary>
	public enum Containment
	{
		/// <summary>The first object fully contains the second one.</summary>
		Contains,

		/// <summary>There is no overlap between the objects.</summary>
		Disjoint,

		/// <summary>The objects partially overlap.</summary>
		Intersects,
	}
}
