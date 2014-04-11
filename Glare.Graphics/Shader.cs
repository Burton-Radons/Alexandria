using Glare.Graphics.Internal;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics
{
	public abstract class Shader : GraphicsResource
	{
		protected const string VersionString = "#version 430\n";

		/// <summary>Get whether the <see cref="Shader"/> has been successfully compiled.</summary>
		public bool CompileStatus { get { return GetInt(ShaderParameter.CompileStatus) != 0; } }

		/// <summary>Get the information log on the <see cref="Shader"/> object. The information log for a shader object is the OpenGL implementer's primary mechanism for conveying information about the compilation process. Therefore, the information log can be helpful to application developers during the development process, even when compilation is successful. Application developers should not expect different OpenGL implementations to produce identical information logs.</summary>
		public string InfoLog { get { using (Context.Lock()) return GL.GetShaderInfoLog(Id); } }

		/// <summary>Get or set the <see cref="Shader"/>'s source code. On assignment, any source code previously stored in the shader object is completely replaced. Assignment only replaces the source code; it does not attempt to compile the shader.</summary>
		public string Source
		{
			get
			{
				using (Context.Lock())
				{
					StringBuilder builder;
					int length;
					GL.GetShader(Id, ShaderParameter.ShaderSourceLength, out length);
					builder = new StringBuilder(length);
					GL.GetShaderSource(Id, length, out length, builder);
					return builder.ToString();
				}
			}

			set
			{
				if (value == null)
					throw new ArgumentNullException("value");
				using (Context.Lock())
					GL.ShaderSource(Id, value);
			}
		}

		public abstract ShaderStage Stage { get; }

		internal Shader(ShaderType shaderType)
			: base(AllocateId(shaderType))
		{
		}

		static int AllocateId(ShaderType shaderType) {
			using (Context.Lock())
				return GL.CreateShader(shaderType);
		}

		/// <summary>
		/// Compiles the source code strings that have been stored in the shader object through <see cref="Source"/>, and return whether it compiles (which can also be checked through <see cref="CompileStatus"/>).
		/// </summary>
		/// <returns></returns>
		public bool Compile()
		{
			using (Context.Lock())
			{
				GL.CompileShader(Id);
				Context.CheckError();
				int result;
				GL.GetShader(Id, ShaderParameter.CompileStatus, out result);
				return result != 0;
			}
		}

		/// <summary>Set the <see cref="Source"/>, then attempt to <see cref="Compile()"/> it.</summary>
		/// <param name="source"></param>
		/// <returns></returns>
		public bool Compile(string source)
		{
			Source = source;
			return Compile();
		}

		protected override void DisposeBase()
		{
			if(Context.Shared.MakeCurrent())
				GL.DeleteShader(Id);
		}

		internal int GetInt(ShaderParameter pname) { int result; using (Context.Lock()) GL.GetShader(Id, pname, out result); return result; }

		public void MustCompile()
		{
			if (!Compile())
				throw new Exception("Couldn't compile " + GetType().Name + ": " + InfoLog);
		}

		public void MustCompile(string source)
		{
			Source = source;
			MustCompile();
		}
	}

	/// <summary>
	/// A shader stage that is used entirely for computing arbitrary information. While it can do rendering, it is generally used for tasks not directly related to drawing triangles and pixels.
	/// </summary>
	/// <remarks>
	/// Compute shaders operate differently from other shader stages. All of the other shader stages have a well-defined set of input values, some built-in and some user-defined. They have a well-defined set of output values, some built-in and some user-defined. The frequency at which a shader stage executes is specified by the nature of that stage; vertex shaders execute once per input vertex, for example (though some executions can be skipped via caching). Fragment shader execution is defined by the fragments generated from the rasterization process.
	/// 
	/// Compute shaders work very differently. The "space" that a compute shader operates on is largely abstract; it is up to each compute shader to decide what the space means. The number of compute shader executions is defined by the function used to execute the compute operation. Most important of all, compute shaders have no user-defined inputs and no outputs at all. The built-in inputs only define where in the "space" of execution a particular compute shader invocation is.
	/// 
	/// Therefore, if a compute shader wants to take some values as input, it is up to the shader itself to fetch that data, via texture access, arbitrary image load, shader storage blocks, or other forms of interface. Similarly, if a compute shader is to actually compute anything, it must explicitly write to an image or shader storage block.
	/// 
	/// <h1>Compute space</h1>
	/// The space that compute shaders operate within is abstract. There is the concept of a work group; this is the smallest amount of compute operations that the user can execute. Or to put it another way, the user can execute some number of work groups.
	/// 
	/// The number of work groups that a compute operation is executed with is defined by the user when they invoke the compute operation. The space of these groups is three dimensional, so it has a number of "X", "Y", and "Z" groups. Any of these can be 1, so you can perform a two-dimensional or one-dimensional compute operation instead of a 3D one. This is useful for processing image data or linear arrays of a particle system or whatever.
	/// 
	/// When the system actually computes the work groups, it can do so in any order. So if it is given a work group set of (3, 1, 2), it could execute group (0, 0, 0) first, then skip to group (1, 0, 1), then jump to (2, 0, 0), etc. So your compute shader should not rely on the order in which individual groups are processed.
	/// 
	/// Do not think that a single work group is the same thing as a single compute shader invocation; there's a reason why it is called a "group". Within a single work group, there may be many compute shader invocations. How many is defined by the compute shader itself, not by the call that executes it. This is known as the local size of the work group.
	/// 
	/// Every compute shader has a three-dimensional local size (again, sizes can be 1 to allow 2D or 1D local processing). This defines the number of invocations of the shader that will take place within each work group.
	/// 
	/// Therefore, if the local size of a compute shader is (128, 1, 1), and you execute it with a work group count of (16, 8, 64), then you will get 1,048,576 separate shader invocations. Each invocation will have a set of inputs that uniquely identifies that specific invocation.
	/// 
	/// This distinction is useful for doing various forms of image compression or decompression; the local size would be the size of a block of image data (8x8, for example), while the group count will be the image size divided by the block size. Each block is processed as a single work group.
	/// 
	/// The individual invocations within a work group will be executed "in parallel". The main purpose of the distinction between work group count and local size is that the different compute shader invocations within a work group can communicate through a set of shared​ variables and special functions. Invocations in different work groups (within the same compute shader dispatch) cannot effectively communicate. Not without potentially deadlocking the system.
	/// 
	/// <h1>Dispatch</h1>
	/// Compute shaders are not part of the regular rendering pipeline. So the usual vertex rendering functions do not work on them.
	/// 
	/// A <see cref="Program"/> object can have a <see cref="ComputeShader"/> in it. The <see cref="ComputeShader"/> linked with other Shader Stages (whether in a single program object or in a program pipeline) is effectively inert to rendering functions.
	/// 
	/// There are two functions to initiate compute operations. They will use whichever compute shader is currently active (via glBindProgramPipeline​ or glUseProgram​, following the usual rules for determining the active program for a stage). For <see cref="Context.DispatchCompute(int, int, int)"/> or <see cref="Context.DispatchCompute(Vector3i)"/>, the numGroups*​ parameter[s] define the work group count, in three dimensions. These numbers cannot be zero. There are limitations on the number of work groups that can be dispatched.
	/// 
	/// It is possible to execute dispatch operations where the work group count comes from information stored in a <see cref="GraphicsBuffer"/> object with <see cref="Context.DispatchComputeIndirect"/>. The indirect​ parameter is the byte-offset to the buffer currently assigned to the <see cref="DispatchIndirectBuffer"/>​​ target. Note that the same limitations on work group counts (see below) still apply; however, indirect dispatch bypasses OpenGL's usual error checking. As such, attempting to dispatch with out-of-bounds work group sizes can cause a crash or even a GPU hard-lock.
	/// 
	///  <h1>Inputs</h1>
	///  
	/// Compute shaders cannot have any user-defined input variables. Compute Shaders have the following built-in output variables.
	/// 
	/// <code lang="glsl">
	/// in uvec3 gl_NumWorkGroups;
	/// in uvec3 gl_WorkGroupID;
	/// in uvec3 gl_LocalInvocationID;
	/// in uvec3 gl_GlobalInvocationID;
	/// in uint  gl_LocalInvocationIndex;
	/// </code>
	/// 
	/// <list type="table">
	/// <item><term>gl_NumWorkGroups​</term><description>This variable contains the number of work groups passed to the dispatch function.</description></item>
	/// <item><term>gl_WorkGroupID​</term><description>This is the current work group for this shader invocation. Each of the XYZ components will be on the half-open range [0, gl_NumWorkGroups.xyz).</description></item>
	/// <item><term>gl_LocalInvocationID​</term><description>This is the current invocation of the shader within the work group. Each of the XYZ components will be on the half-open range [0, gl_WorkGroupSize.xyz​).</description></item>
	/// <item><term>gl_GlobalInvocationID​</term><description>This value uniquely identifies this particular invocation of the compute shader among all invocations of this compute dispatch call. It's a short-hand for the math computation: <c>gl_WorkGroupID * gl_WorkGroupSize + gl_LocalInvocationID</c>.</description></item>
	/// 
	/// <item><term>gl_LocalInvocationIndex​</term><description>This is a 1D version of gl_LocalInvocationID​. It identifies this invocation's index within the work group. It is short-hand for this math computation: <c>gl_LocalInvocationID.z * gl_WorkGroupSize.x * gl_WorkGroupSize.y + gl_LocalInvocationID.y * gl_WorkGroupSize.x + gl_LocalInvocationID.x</c>.</description></item>
	/// </list>
	/// 
	///  <h1>Local size</h1>
	///  
	/// The local size of a compute shader is defined within the shader source, using a special layout input declaration: <c>layout(local_size_x = X​, local_size_y = Y​, local_size_z = Z​) in;</c>
	/// 
	/// By default, the local sizes are 1, so if you only want a 1D or 2D work group space, you can specify just the X​ or the X​ and Y​ components. They must be integral constant expressions of value greater than 0. Their values must abide by the limitations imposed below; if they do not, a compiler or linker error occurs. The local size is available to the shader as a compile-time constant variable, so you don't need to define it yourself: <c>const uvec3 gl_WorkGroupSize;</c>
	/// 
	///  <h1>Shared variables</h1>
	///  
	/// Global variables in compute shaders can be declared with the shared​ storage qualifier. The value of such variables are shared between all invocations within a work group. You cannot declare any opaque types as shared, but aggregates (arrays and structs) are fine. At the beginning of a work group, these values are uninitialized, and the variable declaration cannot have initializers.
	/// 
	///  <h1>Shared memory coherency</h1>
	///  Shared variable access uses the rules for incoherent memory access. This means that the user must perform certain synchronization in order to ensure that shared variables are visible.
	///  
	/// Shared variables are all implicitly declared coherent​, so you don't need to (and can't use) that qualifier. However, you still need to provide an appropriate memory barrier.
	/// 
	/// The usual set of memory barriers is available to compute shaders, but they also have access to memoryBarrierShared()​; this barrier is specifically for shared variable ordering. groupMemoryBarrier()​ acts like memoryBarrier()​, ordering memory writes for all kinds of variables, but it only orders read/writes for the current work group.
	/// 
	/// While all invocations within a work group are said to execute "in parallel", that doesn't mean that you can assume that all of them are executing in lock-step. If you need to ensure that an invocation has written to some variable so that you can read it, you need to synchronize execution with the invocations, not just issue a memory barrier (you still need the memory barrier though).
	/// 
	/// To synchronize reads and writes between invocations within a work group, you must employ the barrier()​ function. This forces an explicit synchronization between all invocations in the work group. Execution within the work group will not proceed until all other invocations have reach this barrier. Once past the barrier()​, all shared variables previously written across all invocations in the group will be visible.
	/// 
	/// There are limitations on how you can call barrier()​. However, compute shaders are not as limited as Tessellation Control Shaders in their use of this function. barrier()​ can be called from flow-control, but it can only be called from uniform flow control. All expressions that lead to the evaluation of a barrier()​ must be dynamically uniform.
	/// 
	/// In short, if you execute the same compute shader, no matter how different the data they fetch is, every execution must hit the exact same set of barrier()​ calls in the exact same order. Otherwise badness happens.
	/// 
	///  <h1>Atomic operations</h1>
	///  A number of atomic operations can be performed on shared variables of integral type (and vectors/arrays/structs of them). These functions are shared with Shader Storage Buffer Object atomics.
	///  All of the atomic functions return the original value. The term "nint" can be int​ or uint​.
	/// 
	/// <list type="table">
	/// <item><term>nint atomicAdd(inout nint mem​, nint data​)</term><description>Adds data​ to mem​.</description></item>
	/// <item><term>nint atomicMin(inout nint mem​, nint data​)</term><description>The mem​'s value is no lower than data​.</description></item>
	/// <item><term>nint atomicMax(inout nint mem​, nint data​)</term><description>The mem​'s value is no greater than data​.</description></item>
	/// <item><term>nint atomicAnd (inout nint mem​, nint data​)</term><description>mem​ becomes the bitwise-and between mem​ and data​.</description></item>
	/// <item><term>nint atomicOr(inout nint mem​, nint data​)</term><description>mem​ becomes the bitwise-or between mem​ and data​.</description></item>
	/// <item><term>nint atomicXor(inout nint mem​, nint data​)</term><description>mem​ becomes the bitwise-xor between mem​ and data​.</description></item>
	/// <item><term>nint atomicExchange(inout nint mem​, nint data​)</term><description>Sets mem​'s value to data​.</description></item>
	/// <item><term>nint atomicCompSwap(inout nint mem​, nint compare​, nint data​)</term><description>If the current value of mem​ is equal to compare​, then mem​ is set to data​. Otherwise it is left unchanged.</description></item>
	/// </list>
	/// 
	///  <h1>Limitations</h1>
	/// The number of work groups that can be dispatched in a single dispatch call is defined by <see cref="Context.MaxComputeWorkGroupCount"/>​. Attempting to call <see cref="Context.DispatchCompute"/>​ with values that exceed this range is an error. Attempting to call <see cref="Context.DispatchComputeIndirect"/>​ is much worse; it may result in program termination or other badness.
	/// 
	/// Note that the minimum these values must be is 65535 in all three axes. So you've probably got a lot of room to work with.
	/// 
	/// There are limits on the local size as well; indeed, there are two sets of limitations. There is a general limitation on the local size dimensions, queried with <see cref="Context.MaxComputeWorkGroupSize"/>. Note that the minimum requirements here are much smaller: 1024 for X and Y, and a mere 64 for Z.
	/// 
	/// There is another limitation: the total number of invocations within a work group. That is, the product of the X, Y and Z components of the local size must be less than <see cref="Context.MaxComputeWorkGroupInvocations"/>​. The minimum value here is 1024.
	/// 
	/// There is also a limit on the total storage size for all shared variables in a compute shader. This is <see cref="MaxComputeSharedMemorySize"/>​, which is in bytes. The OpenGL-required minimum is 32KB. OpenGL does not specify the exact mapping between GL types and shared variable storage, though you could use the std140 layout rules and UBO/SSBO sizes as a general guideline.
	/// </remarks>
	/// <seealso href="http://www.opengl.org/wiki/Compute_Shader">OpenGL Wiki</seealso>
	[GLMinimum(4, 3)]
	public class ComputeShader : Shader
	{
		public override ShaderStage Stage { get { return ShaderStage.Compute; } }
		public ComputeShader() : base(GLExt.ComputeShader) { }
		public ComputeShader(string source) : this() { MustCompile(source); }
	}

	public class FragmentShader : Shader
	{
		public override ShaderStage Stage { get { return ShaderStage.Fragment; } }
		public FragmentShader() : base(ShaderType.FragmentShader) { }
		public FragmentShader(string source) : this() { MustCompile(source); }
	}

	public class GeometryShader : Shader
	{
		public override ShaderStage Stage { get { return ShaderStage.Geometry; } }
		public GeometryShader() : base(ShaderType.GeometryShader) { }
		public GeometryShader(string source) : this() { MustCompile(source); }

		public static GeometryShader Procedural(string positionGenerator, string normalGenerator)
		{
			string source = VersionString +
				"vec3 PositionGenerator(vec2 input) { " + positionGenerator + " }\n" +
				"vec3 NormalGenerator(vec2 input) { " + normalGenerator + " }\n" +
				"layout(points) in;\n" +
				"layout(triangle_strip max_vertices = 3) out;\n" +
				"uniform ivec2 Resolution = ivec2(16, 16);\n";

			return new GeometryShader(source);
		}
		/*
		 * 
			var geometryShader = new GeometryShader(platform, glslVersion +
				"layout(points) in;\n" +
				"layout(triangle_strip, max_vertices = 3) out;\n" +
				"void main() { }\n");
		 */
	}

	public class TessellationControlShader : Shader
	{
		public override ShaderStage Stage { get { return ShaderStage.TessellationControl; } }
		public TessellationControlShader() : base(ShaderType.TessControlShader) { }
		public TessellationControlShader(string source) : this() { MustCompile(source); }
	}

	public class TessellationEvaluationShader : Shader
	{
		public override ShaderStage Stage { get { return ShaderStage.TessellationEvaluation; } }
		public TessellationEvaluationShader() : base(ShaderType.TessEvaluationShader) { }
		public TessellationEvaluationShader(string source) : this() { MustCompile(source); }
	}

	public class VertexShader : Shader
	{
		public override ShaderStage Stage { get { return ShaderStage.Vertex; } }
		public VertexShader() : base(ShaderType.VertexShader) { }
		public VertexShader(string source) : this() { MustCompile(source); }
	}
}
