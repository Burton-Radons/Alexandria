using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics
{
	/// <summary>
	/// This specifies the number of bits in an element index.
	/// </summary>
	public enum ElementType
	{
		/// <summary>There are 8 bits in an element index.</summary>
		Byte = DrawElementsType.UnsignedByte,

		/// <summary>There are 16 bits in an element index.</summary>
		UInt16 = DrawElementsType.UnsignedShort,

		/// <summary>There are 32 bits in an element index.</summary>
		UInt32 = DrawElementsType.UnsignedInt,
	}
}
