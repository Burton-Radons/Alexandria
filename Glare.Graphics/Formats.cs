using GL = OpenTK.Graphics.OpenGL4;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glare.Graphics {

	/// <summary>Formats that can be fed into shader program attributes.</summary>
	public static partial class VectorFormats {
		/*
		FloatMat2 = 35674,
		FloatMat3 = 35675,
		FloatMat4 = 35676,
		DoubleMat2 = 36678,
		DoubleMat3 = 36679,
		DoubleMat4 = 36680,
		DoubleMat2x3 = 36681,
		DoubleMat2x4 = 36682,
		DoubleMat3x2 = 36683,
		DoubleMat3x4 = 36684,
		DoubleMat4x2 = 36685,
		DoubleMat4x3 = 36686,
		 */

	}

	/// <summary>
	/// This contains all formats that can be used for textures.
	/// </summary>
	public static partial class TextureFormats {
	}

	/// <summary>This contains the list of all valid <see cref="Format"/> values that are common to both <see cref="VectorFormats"/> and <see cref="TextureFormats"/>.</summary>
	public static partial class CommonFormats {
	}

	/// <summary>This contains all of the valid <see cref="VectorFormats"/>, <see cref="TextureFormats"/>, and <see cref="CommonFormats"/> values.</summary>
	public static partial class Formats {
		#region Scalar

		#endregion Scalar

		#region Two-dimensional

		#endregion Two-dimensional

		#region Three-dimensional

		/// <summary>Three-dimensional normalized unsigned byte vector in SRGB color space. Texture format only.</summary>
		public static readonly Format Vector3srgb = new Format(typeof(NormalizedByte), null, rows: 3, isSrgb: true);

		#endregion Three-dimensional

		#region Four-dimensional

		/// <summary>Four-dimensional normalized unsigned byte vector corresponding to <see cref="Glare.Vector4nb"/>, in BGRA order. Texture format only.</summary>
		public static readonly Format Vector4nbBGRA = new Format(typeof(NormalizedByte), null, rows: 4, isReversed: true);

		/// <summary>Four-dimensional normalized unsigned byte vector in SRGB color space corresponding to <see cref="Glare.Vector4srgba"/>. Texture format only.</summary>
		public static readonly Format Vector4srgba = new Format(typeof(NormalizedByte), null, rows: 4, isSrgb: true);

		#endregion Four-dimensional

		#region Compressed texture formats

		/// <summary>DXT1 compressed format.</summary>
		public static readonly Format DXT1 = new Format(null, null, rows: 3, compression: FormatCompression.DXT1);

		/// <summary>DXT3 compressed format.</summary>
		public static readonly Format DXT3 = new Format(null, null, rows: 4, compression: FormatCompression.DXT3);

		/// <summary>DXT5 compressed format.</summary>
		public static readonly Format DXT5 = new Format(null, null, rows: 4, compression: FormatCompression.DXT5);

		#endregion Compressed texture formats

		const int GL_R8_SNORM = 36756;
	}
}
