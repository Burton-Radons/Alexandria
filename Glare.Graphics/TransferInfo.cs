using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics
{
	/// <summary>
	/// This describes the layout of client data for transferring pixels to and from the GPU.
	/// </summary>
	public struct TransferInfo
	{
		/// <summary>Get or set the number of pixels in a plane for 3D textures, or 0 to use the width times height. The default is 0.</summary>
		public int ImageHeight { get; set; }

		/// <summary>Get or set whether the least significant bit in a byte is first. Otherwise the most significant bit in a byte is first. The default is <c>false</c>.</summary>
		public bool LsbFirst { get; set; }

		/// <summary>Get or set the number of pixels in a row, or 0 to use the width of a row. The default is 0.</summary>
		public int RowLength { get; set; }

		/// <summary>Get or set the number of pixels to skip from the beginning of the image in the data.</summary>
		public int SkipPixels { get; set; }

		/// <summary>Get or set whether to swap the bytes of multibyte components. This has no effect on the order of components in a pixel, only the order of the bytes of their individual components. The default is <c>false</c>.</summary>
		public bool SwapBytes { get; set; }
	}
}
