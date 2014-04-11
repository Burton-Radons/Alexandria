using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare
{
	/// <summary>
	/// Constant <see cref="Vector4d"/> values for RGBA colors.
	/// </summary>
	public struct Colours
	{
		/// <summary>Get a black color (0, 0, 0, 1).</summary>
		public static readonly Vector4d Black = new Vector4d(0, 0, 0, 1);

		/// <summary>Get a blue color (0, 0, 1, 1).</summary>
		public static readonly Vector4d Blue = new Vector4d(0, 0, 1, 1);

		/// <summary>Get a green color (0, 1, 0, 1).</summary>
		public static readonly Vector4d Green = new Vector4d(0, 1, 0, 1);

		/// <summary>Get a red color (1, 0, 0, 1).</summary>
		public static readonly Vector4d Red = new Vector4d(0, 0, 0, 1);

		/// <summary>Get a transparent color (0, 0, 0, 0).</summary>
		public static readonly Vector4d Transparent = new Vector4d(0, 0, 0, 0);

		/// <summary>Get a white color (1, 1, 1, 1).</summary>
		public static readonly Vector4d White = new Vector4d(1, 1, 1, 1);
	}
}
