using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare {
	public class VectorTypeAttribute : Attribute {
		readonly Type elementType;
		readonly int elementCount;
		readonly bool reversed;

		public Type ElementType { get { return elementType; } }
		public int ElementCount { get { return elementCount; } }
		public bool Reversed { get { return reversed; } }

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
