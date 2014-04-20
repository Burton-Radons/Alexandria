using Glare.Graphics.Internal;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics {
	/// <summary>
	/// Capabilities for a <see cref="ShaderStage"/>, which are subclasses of <see cref="Shader"/> (<see cref="ComputeShader"/>, <see cref="FragmentShader"/>, <see cref="GeometryShader"/>, <see cref="TessellationControlShader"/>, <see cref="TessellationEvaluationShader"/>, and <see cref="VertexShader"/>). Instances of subclasses of this class are returned by properties in <see cref="Capabilities"/> (<see cref="Capabilities.Compute"/>, <see cref="Capabilities.Fragment"/>, <see cref="Capabilities.Geometry"/>, <see cref="Capabilities.TesselationControl"/>, <see cref="Capabilities.TesselationEvaluation"/>, and <see cref="Capabilities.Vertex"/>). For the combined values, see OpenGL 4.4, table 23.63, page 584.
	/// </summary>
	public class ShaderStageCapabilities {
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly ShaderStage stage;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly GetPName maxAtomicCounterBuffers, maxAtomicCounters, maxImageUniforms, maxShaderStorageBlocks, maxTextureImageUnits, maxUniformBlocks;

		/// <summary>Get the maximum number of atomic counter buffers that may be accessed by a shader.</summary>
		public int MaxAtomicCounterBuffers { get { return Device.GetInt32(maxAtomicCounterBuffers); } }

		/// <summary>Get the maximum number of atomic counters available to shaders.</summary>
		public int MaxAtomicCounters { get { return Device.GetInt32(maxAtomicCounters); } }

		/// <summary>Get the maximum number of image uniforms available to the individual shader stage. If more than one shader stage accesses the same uniform, each such access counts separately. This must be at least 0, except for the fragment shader stage where it must be at least 8. The combined limit must be at least 8. See OpenGL 4.4, section 11.1.3.7, page 355.</summary>
		public int MaxImageUniforms { get { return Device.GetInt32(maxImageUniforms); } }

		/// <summary>Get the maximum number of active shader storage blocks that may be accessed by a shader. There is no minimum limit except in fragment shaders where the minimum is 8. The combined limit must be at least 8. See OpenGL 4.4, section 7.8, page 135; and tables 23.57-64, pages 578-585.</summary>
		public int MaxShaderStorageBlocks { get { return Device.GetInt32(maxShaderStorageBlocks); } }

		/// <summary>Get the maximum supported texture image units that can be used to access texture maps from the shader. The limit must be at least 16; the combined sum of all stages must have a limit of at least 96. See OpenGL 4.4, section 11.1.3.5, page 352.</summary>
		public int MaxTextureImageUnits { get { return Device.GetInt32(maxTextureImageUnits); } }

		/// <summary>Get the maximum number of uniform blocks per <see cref="Shader"/>. The limit must be at least 14; the combined sum of all stages must have a limit of at least 70.</summary>
		public int MaxUniformBlocks { get { return Device.GetInt32(maxUniformBlocks); } }

		public ShaderStage Stage { get { return stage; } }

		internal ShaderStageCapabilities(ShaderStage stage, GetPName maxAtomicCounterBuffers, GetPName maxAtomicCounters, GetPName maxImageUniforms, GetPName maxShaderStorageBlocks, GetPName maxTextureImageUnits, GetPName maxUniformBlocks) {
			this.stage = stage;

			this.maxAtomicCounterBuffers = maxAtomicCounterBuffers;
			this.maxAtomicCounters = maxAtomicCounters;
			this.maxImageUniforms = maxImageUniforms;
			this.maxShaderStorageBlocks = maxShaderStorageBlocks;
			this.maxTextureImageUnits = maxTextureImageUnits;
			this.maxUniformBlocks = maxUniformBlocks;
		}
	}

	/// <summary>This contains limitations that apply to the sum of all other stages, as well as providing other program-general capabilities. See OpenGL 4.4, table 23.63, page 584; through table 23.66, page 587.</summary>
	public class CombinedShaderStageCapabilities : ShaderStageCapabilities {
		/// <summary>The minimum value is -8. See OpenGL 4.4, section 11.1.3.5, pages 352-354.</summary>
		public int MaxTexelOffset { get { return Device.GetInt32(GetPName.MaxProgramTexelOffset); } }

		/// <summary>The minimum value is 7. See OpenGL 4.4, section 11.1.3.5, pages 352-354.</summary>
		public int MinTexelOffset { get { return Device.GetInt32(GetPName.MinProgramTexelOffset); } }

		/// <summary>The minimum value is 84.</summary>
		public int MaxUniformBufferBindings { get { return Device.GetInt32(GetPName.MaxUniformBufferBindings); } }

		/// <summary>When linking a <see cref="Program"/> containing only a <see cref="VertexShader"/> and <see cref="FragmentShader"/>, this is the limit on the total number of components used as vertex shader outputs or fragment shader inputs. The minimum value is 60. See OpenGL 4.4, section 11.1.2.1, pages 345.</summary>
		public int MaxVaryingComponents { get { return Device.GetInt32(GetPName.MaxVaryingComponents); } }

		/// <summary>Defined as <see cref="MaxVaryingComponents"/> divided by 4.</summary>
		public int MaxVaryingVectors { get { return Device.GetInt32(GetPName.MaxVaryingVectors); } }

		internal CombinedShaderStageCapabilities()
			: base(ShaderStage.None,
				GLExt.MaxCombinedAtomicCounterBuffers,
				GLExt.MaxCombinedAtomicCounters,
				GLExt.MaxCombinedImageUniforms,
				GLExt.MaxCombinedShaderStorageBlocks,
				GetPName.MaxCombinedTextureImageUnits,
				GetPName.MaxCombinedUniformBlocks) {
		}
	}

	/// <summary>
	/// Base abstract class of the pipeline stage capabilities classes.
	/// </summary>
	public abstract class PipelineShaderStageCapabilities : ShaderStageCapabilities {
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly GetPName maxCombinedUniformComponents, maxUniformComponents;

		/// <summary>Get the number of words for the <see cref="Shader"/> <see cref="Stage"/>'s uniform variables in all uniform blocks (including default). The value must be at least <see cref="MaxUniformBlocks"/> * <see cref="MaxUniformBlockSize"/> / 4 + <see cref="MaxUniformComponents"/>. See OpenGL 4.4, table 7.5, page 118; and table 23.66, page 587.</summary>
		public int MaxCombinedUniformComponents { get { return Device.GetInt32(maxCombinedUniformComponents); } }

		/// <summary>Get the maximum number of individual floating-point, integer, or boolean values that can be held in uniform variable storage for a shader. The limit must be at least 512 for compute and geometry shaders, and 1024 for all other shaders.</summary>
		public int MaxUniformComponents { get { return Device.GetInt32(maxUniformComponents); } }

		/// <summary>The number of vectors for vertex shader uniform variables. Defined to be <see cref="MaxUniformComponents"/> divided by 4. See OpenGL 4.4, section 7.6, page 117. For shader types other than Vertex and Fragment, these are extrapolated based on that definition to fill out the other shader types.</summary>
		public virtual int MaxUniformVectors { get { return MaxUniformComponents / 4; } }

		internal PipelineShaderStageCapabilities(ShaderStage stage, GetPName maxAtomicCounterBuffers, GetPName maxAtomicCounters, GetPName maxCombinedUniformComponents, GetPName maxImageUniforms, GetPName maxShaderStorageBlocks, GetPName maxTextureImageUnits, GetPName maxUniformBlocks, GetPName maxUniformComponents)
			: base(stage, maxAtomicCounterBuffers, maxAtomicCounters, maxImageUniforms, maxShaderStorageBlocks, maxTextureImageUnits, maxUniformBlocks) {
			this.maxCombinedUniformComponents = maxCombinedUniformComponents;
			this.maxUniformComponents = maxUniformComponents;
		}
	}

	/// <summary>See OpenGL 4.4, table 23.62, page 583; and Chapter 19.</summary>
	public class ComputeShaderStageCapabilities : PipelineShaderStageCapabilities {
		/// <summary>Get the maximum number of work groups that may be dispatched to a compute shader. The minimum value of the product of the components is 65535. See OpenGL 4.4, section 19, page 488.</summary>
		public Vector3i MaxWorkGroupCount { get { return Device.GetVector3i(GLExt.MaxComputeWorkGroupCount); } }

		/// <summary>Get the maximum number of invocations in a single local work group (the product of the three dimensions). The minimum value is 1024. See OpenGL 4.4, section 19, page 488.</summary>
		public int MaxWorkGroupInvocations { get { return Device.GetInt32(GLExt.MaxComputeWorkGroupInvocations); } }

		/// <summary>Get the maximum local size of a compute shader work group. The minimum values are 1024 for X and Y, and 64 for Z. See OpenGL 4.4, section 19, page 488.</summary>
		public Vector3i MaxWorkGroupSize { get { return Device.GetVector3i(GLExt.MaxComputeWorkGroupSize); } }

		internal ComputeShaderStageCapabilities()
			: base(ShaderStage.Compute,
				GLExt.MaxComputeAtomicCounterBuffers,
				GLExt.MaxComputeAtomicCounters,
				GLExt.MaxCombinedComputeUniformComponents,
				GLExt.MaxComputeImageUniforms,
				GLExt.MaxComputeShaderStorageBlocks,
				GLExt.MaxComputeTextureImageUnits,
				GLExt.MaxComputeUniformBlocks,
				GLExt.MaxComputeUniformComponents) { }
	}

	public class FragmentShaderStageCapabilities : PipelineShaderStageCapabilities {
		public int MaxInputComponents { get { return Device.GetInt32(GetPName.MaxFragmentInputComponents); } }

		/// <summary>The number of vectors for vertex shader uniform variables. Defined to be <see cref="MaxUniformComponents"/> divided by 4. See OpenGL 4.4, section 7.6, page 117.</summary>
		public override int MaxUniformVectors { get { return Device.GetInt32(GetPName.MaxFragmentUniformVectors); } }

		internal FragmentShaderStageCapabilities()
			: base(ShaderStage.Fragment,
				GLExt.MaxFragmentAtomicCounterBuffers,
				GLExt.MaxFragmentAtomicCounters,
				GetPName.MaxCombinedFragmentUniformComponents,
				GLExt.MaxFragmentImageUniforms,
				GLExt.MaxFragmentShaderStorageBlocks,
				GetPName.MaxTextureImageUnits,
				GetPName.MaxFragmentUniformBlocks,
				GetPName.MaxFragmentUniformComponents) { }
	}

	public class GeometryShaderStageCapabilities : PipelineShaderStageCapabilities {
		/// <summary>Get the maximum number of components of inputs read by a geometry shader, which must be at least 64.</summary>
		public int MaxInputComponents { get { return Device.GetInt32(GetPName.MaxGeometryInputComponents); } }

		/// <summary>Get the maximum number of components of outputs written by a <see cref="GeometryShader"/>, which must be at least 128.</summary>
		public int MaxOutputComponents { get { return Device.GetInt32(GetPName.MaxGeometryOutputComponents); } }

		internal GeometryShaderStageCapabilities()
			: base(ShaderStage.Geometry,
				GLExt.MaxGeometryAtomicCounterBuffers,
				GLExt.MaxGeometryAtomicCounters,
				GetPName.MaxCombinedGeometryUniformComponents,
				GLExt.MaxGeometryImageUniforms,
				GLExt.MaxGeometryShaderStorageBlocks,
				GetPName.MaxGeometryTextureImageUnits,
				GetPName.MaxGeometryUniformBlocks,
				GetPName.MaxGeometryUniformComponents) { }
	}

	public class TesselationControlShaderStageCapabilities : PipelineShaderStageCapabilities {
		internal TesselationControlShaderStageCapabilities()
			: base(ShaderStage.TessellationControl,
				GLExt.MaxTessControlAtomicCounterBuffers,
				GLExt.MaxTessControlAtomicCounters,
				GetPName.MaxCombinedTessControlUniformComponents,
				GLExt.MaxTessControlImageUniforms,
				GLExt.MaxTessControlShaderStorageBlocks,
				GetPName.MaxTessControlTextureImageUnits,
				GetPName.MaxTessControlUniformBlocks,
				GetPName.MaxTessControlUniformComponents) { }
	}

	public class TesselationEvaluationShaderStageCapabilities : PipelineShaderStageCapabilities {
		internal TesselationEvaluationShaderStageCapabilities()
			: base(ShaderStage.TessellationEvaluation,
				GLExt.MaxTessEvaluationAtomicCounterBuffers,
				GLExt.MaxTessEvaluationAtomicCounters,
				GetPName.MaxCombinedTessEvaluationUniformComponents,
				GLExt.MaxTessEvaluationImageUniforms,
				GLExt.MaxTessEvaluationShaderStorageBlocks,
				GetPName.MaxTessEvaluationTextureImageUnits,
				GetPName.MaxTessEvaluationUniformBlocks,
				GetPName.MaxTessEvaluationUniformComponents) { }
	}

	/// <summary>
	/// Capabilities that apply to the <see cref="VertexShader"/> stage (selected by <see cref="ShaderStage"/>'s <see cref="ShaderStage.Vertex"/> value). See OpenGL 4.4, table 23.57, page 578.
	/// </summary>
	public class VertexShaderStageCapabilities : PipelineShaderStageCapabilities {
		// vertex
		//	max_output_components 64
		//	max_uniform_blocks min 14
		//	max_uniform_components min 1024
		//	max_uniform_vectors min 256
		//	max_vertex_texture_image_units 16

		/// <summary>Get the maximum number of four-component generic vertex attributes. The minimum is 16. See OpenGL 4.4, section 10.2, pages 307-310.</summary>
		public int MaxAttributes { get { return Device.GetInt32(GetPName.MaxVertexAttribs); } }

		/// <summary>Get the number of components (individual scalar numeric values) of output variables that can be written by the vertex shader, whether or not a tessellation control, tessellation evaluation, or geometry shader is active. Outputs declared as vectors, matrices, and arrays will all consume multiple components. For the purposes of counting input and output components consumed by a shader, variables declared as vectors, matrices, and arrays will all consume multiple components. Each component of variables declared as double-precision floating-point scalars, vectors, or matrices may be counted as consuming two components. The minimum is 64. See OpenGL 4.4, section 11.1.2.1, page 344.</summary>
		public int MaxOutputComponents { get { return Device.GetInt32(GetPName.MaxVertexOutputComponents); } }

		/// <summary>The number of vectors for vertex shader uniform variables. Defined to be <see cref="MaxUniformComponents"/> divided by 4. See OpenGL 4.4, section 7.6, page 117.</summary>
		public override int MaxUniformVectors { get { return Device.GetInt32(GetPName.MaxVertexUniformVectors); } }

		internal VertexShaderStageCapabilities()
			: base(ShaderStage.Vertex,
				GLExt.MaxVertexAtomicCounterBuffers,
				GLExt.MaxVertexAtomicCounters,
				GetPName.MaxCombinedVertexUniformComponents,
				GLExt.MaxVertexImageUniforms,
				GLExt.MaxVertexShaderStorageBlocks,
				GetPName.MaxVertexTextureImageUnits,
				GetPName.MaxVertexUniformBlocks,
				GetPName.MaxVertexUniformComponents) { }
	}
}
