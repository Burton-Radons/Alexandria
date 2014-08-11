using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare {
	/// <summary>
	/// Describes the parameters of a vector type.
	/// </summary>
	public class VectorTypeAttribute : Attribute {
		readonly Type elementType;
		readonly int elementCount;
		readonly bool reversed;

		/// <summary>
		/// Get the type of an element of the vector.
		/// </summary>
		public Type ElementType { get { return elementType; } }

		/// <summary>
		/// Get the number of axes in the vector.
		/// </summary>
		public int ElementCount { get { return elementCount; } }

		/// <summary>Get whether the elements are reversed.</summary>
		public bool Reversed { get { return reversed; } }

		/// <summary>Initialise the attribute.</summary>
		/// <param name="elementType"></param>
		/// <param name="elementCount"></param>
		/// <param name="reversed"></param>
		public VectorTypeAttribute(Type elementType, int elementCount, bool reversed) {
			if (elementType == null)
				throw new ArgumentNullException("elementType");
			if (elementCount < 2)
				throw new ArgumentOutOfRangeException("elementCount");
			if (reversed && elementCount != 4)
				throw new ArgumentOutOfRangeException("reversed");

			this.elementType = elementType;
			this.elementCount = elementCount;
			this.reversed = reversed;
		}
	}

	partial struct Vector4f {
	}

	partial struct Vector4d {
	}

	partial struct Vector4i {
	}

	partial struct Vector4nb {
	}

	partial struct Vector3rgb {
	}
}
