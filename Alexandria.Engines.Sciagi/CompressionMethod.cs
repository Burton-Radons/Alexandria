using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.Sciagi {
	/// <summary>
	/// This specifies one of the compression methods.
	/// </summary>
	public enum CompressionMethod {
		/// <summary>No compression or invalid value.</summary>
		None,

		/// <summary>Lempel-Ziv-Welch compression.</summary>
		Lzw,

		/// <summary>Huffman table compression.</summary>
		Huffman,

		/// <summary>"Comp3" compression.</summary>
		Comp3,

		/// <summary>DCL-Implode compression.</summary>
		DclImplode,

		/// <summary>STACpack/LZS compression.</summary>
		Lzs,
	}
}
