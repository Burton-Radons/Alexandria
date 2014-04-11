using Glare.Graphics.Internal;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics
{
	public enum Axis {
		X,
		Y,
		Z,
		W,
	}

	/// <summary>
	/// A hint about how a <see cref="GraphicsBuffer"/> is used. This is composed of two parts (from <see cref="BufferUsagePart"/>).
	/// </summary>
	public enum BufferUsage
	{
		/// <summary>The data store contents will be modified once and used at most a few times. The data store contents are modified by the application, and used as the source for GL drawing and image specification commands.</summary>
		StreamDraw = 35040,

		/// <summary>The data store contents will be modified once and used at most a few times. The data store contents are modified by reading data from the GL, and used to return that data when queried by the application.</summary>
		StreamRead = 35041,

		/// <summary>The data store contents will be modified once and used at most a few times. The data store contents are modified by reading data from the GL, and used as the source for GL drawing and image specification commands.</summary>
		StreamCopy = 35042,

		/// <summary>The data store contents will be modified once and used many times. The data store contents are modified by the application, and used as the source for GL drawing and image specification commands.</summary>
		StaticDraw = 35044,

		/// <summary>The data store contents will be modified once and used many times. The data store contents are modified by reading data from the GL, and used to return that data when queried by the application.</summary>
		StaticRead = 35045,

		/// <summary>The data store contents will be modified once and used many times. The data store contents are modified by reading data from the GL, and used as the source for GL drawing and image specification commands.</summary>
		StaticCopy = 35046,

		/// <summary>The data store contents will be modified repeatedly and used many times. The data store contents are modified by the application, and used as the source for GL drawing and image specification commands.</summary>
		DynamicDraw = 35048,

		/// <summary>The data store contents will be modified repeatedly and used many times. The data store contents are modified by reading data from the GL, and used to return that data when queried by the application.</summary>
		DynamicRead = 35049,

		/// <summary>The data store contents will be modified repeatedly and used many times. The data store contents are modified by reading data from the GL, and used as the source for GL drawing and image specification commands.</summary>
		DynamicCopy = 35050,
	}

	public enum BufferUsagePart
	{
		/// <summary>The data store contents will be modified once and used at most a few times.</summary>
		FrequencyStream = 35040,

		/// <summary>The data store contents will be modified once and used many times.</summary>
		FrequencyStatic = 35044,

		/// <summary>The data store contents will be modified repeatedly and used many times.</summary>
		FrequencyDynamic = 35048,

		/// <summary>The data store contents are modified by the application, and used as the source for GL drawing and image specification commands.</summary>
		AccessDraw = 0,

		/// <summary>The data store contents are modified by reading data from the GL, and used to return that data when queried by the application.</summary>
		AccessRead = 1,

		/// <summary>The data store contents are modified by reading data from the GL, and used as the source for GL drawing and image specification commands.</summary>
		AccessCopy = 2,
	}

	/// <summary>Specifies how to compare two values (a and b) to get a boolean result.</summary>
	public enum ComparisonFunction
	{
		/// <summary>The result is <c>false</c>.</summary>
		False = DepthFunction.Never,

		/// <summary>The result is <c>true</c> if <i>a</i> &lt; <i>b</i>.</summary>
		Less = DepthFunction.Less,

		/// <summary>The result is <c>true</c> if <i>a</i> = <i>b</i>.</summary>
		Equal = DepthFunction.Equal,

		/// <summary>The result is <c>true</c> if <i>a</i> ≤ <i>b</i>.</summary>
		LessOrEqual = DepthFunction.Lequal,

		/// <summary>The result is <c>true</c> if <i>a</i> &gt; <i>b</i>.</summary>
		Greater = DepthFunction.Greater,

		/// <summary>The result is <c>true</c> if <i>a</i> ≠ <i>b</i>.</summary>
		Different = DepthFunction.Notequal,

		/// <summary>The result is <c>true</c> if <i>a</i> ≥ <i>b</i>.</summary>
		GreaterOrEqual = DepthFunction.Gequal,

		/// <summary>The result is <c>true</c>.</summary>
		True = DepthFunction.Always,
	}

	public enum Primitive
	{
		/// <summary>No or invalid value.</summary>
		None = 0,

		LineLoop = BeginMode.LineLoop,
		Lines = BeginMode.Lines,
		LinesAdjacency = BeginMode.LinesAdjacency,
		LineStrip = BeginMode.LineStrip,
		LineStripAdjacency = BeginMode.LineStripAdjacency,
		Patches = BeginMode.Patches,
		Points = BeginMode.Points,
		Polygon = BeginMode.Polygon,
		Quads = BeginMode.Quads,
		QuadStrip = BeginMode.QuadStrip,
		TriangleFan = BeginMode.TriangleFan,
		Triangles = BeginMode.Triangles,
		TrianglesAdjacency = BeginMode.TrianglesAdjacency,
		TriangleStrip = BeginMode.TriangleStrip,
		TriangleStripAdjacency = BeginMode.TriangleStripAdjacency,
	}

	/// <summary>
	/// This defines the stage that a <see cref="Shader"/> runs in.
	/// </summary>
	public enum ShaderStage
	{
		/// <summary>No or invalid value.</summary>
		None = 0,
		Compute = GLExt.ComputeShader,
		Fragment = ShaderType.FragmentShader,
		Geometry = ShaderType.GeometryShader,
		TessellationControl = ShaderType.TessControlShader,
		TessellationEvaluation = ShaderType.TessEvaluationShader,
		Vertex = ShaderType.VertexShader,
	}

	/// <summary>This specifies how texture values should be compared to depth values.</summary>
	public enum TextureComparisonMode
	{
		/// <summary>Compare the x channel of the texture to the current depth value.</summary>
		Compare = TextureCompareMode.CompareRefToTexture,

		/// <summary>Do not make a comparison; assign the x channel directly from the texture.</summary>
		Sample = TextureCompareMode.None,
	}

	/// <summary>This describes how mid-texel or mid-level coordinates should be interpreted in a shader.</summary>
	public enum TextureFilter
	{
		/// <summary>Perform no filtering. This is only valid for a mipmap filter, to disable it.</summary>
		None = 0,
		Nearest = TextureMagFilter.Nearest,
		Linear = TextureMagFilter.Linear,
	}

	/// <summary>This describes how out-of-range coordinates in texture sampling should be interpreted in a shader.</summary>
	public enum TextureWrap
	{
		/// <summary>Clamp to half a texel outside of the texture, the range [-1/(2N), 1+1/(2N)], where N is the size of the texture in the direction of clamping.</summary>
		ClampToBorder = TextureWrapMode.ClampToBorder,

		/// <summary>Clamp to half a texel within the texture, the range [1/(2N), 1+1/(2N)], where N is the size of the texture in the direction of clamping.</summary>
		ClampToEdge = TextureWrapMode.ClampToEdge,

		/// <summary>If the integer part of the coordinate is even, use the fraction. If the integer part of the coordinate is odd, then use 1 - frac(s).</summary>
		MirroredRepeat = TextureWrapMode.MirroredRepeat,

		/// <summary>Ignore the integer part of the coordinate, using only the fraction.</summary>
		Repeat = TextureWrapMode.Repeat,
	}

	public enum TransformFeedbackMode
	{
		InterleavedAttributes = 0x8C8C,
		SeparateAttributes = 0x8C8D,
	}
}
