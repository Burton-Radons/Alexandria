using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using GLsizei = System.Int32;
using GLuint = System.UInt32;
using GLint = System.Int32;
using GLboolean = System.Boolean;

namespace Glare.Graphics.Internal
{
	internal static class GLExt
	{
		const string dllName = "opengl32.dll";

		public const GetPName MaxComputeAtomicCounterBuffers = (GetPName)0x8264;
		public const GetPName MaxComputeAtomicCounters = (GetPName)0x8265;
		public const GetPName MaxCombinedComputeUniformComponents = (GetPName)0x8266;
		public const int ComputeWorkGroupSize = (int)0x8267;

		public const GetPName MaxComputeUniformComponents = (GetPName)0x8263;

		public const GetPName MaxVertexImageUniforms = (GetPName)0x90CA;
		public const GetPName MaxTessControlImageUniforms = (GetPName)0x90CB;
		public const GetPName MaxTessEvaluationImageUniforms = (GetPName)0x90CC;
		public const GetPName MaxGeometryImageUniforms = (GetPName)0x90CD;
		public const GetPName MaxFragmentImageUniforms = (GetPName)0x90CE;
		public const GetPName MaxCombinedImageUniforms = (GetPName)0x90CF;

		public const GetPName MaxVertexShaderStorageBlocks = (GetPName)0x90D6;
		public const GetPName MaxGeometryShaderStorageBlocks = (GetPName)0x90D7;
		public const GetPName MaxTessControlShaderStorageBlocks = (GetPName)0x90D8;
		public const GetPName MaxTessEvaluationShaderStorageBlocks = (GetPName)0x90D9;
		public const GetPName MaxFragmentShaderStorageBlocks = (GetPName)0x90DA;
		public const GetPName MaxComputeShaderStorageBlocks = (GetPName)0x90DB;
		public const GetPName MaxCombinedShaderStorageBlocks = (GetPName)0x90DC;

		public const GetPName MaxComputeWorkGroupInvocations = (GetPName)0x90EB;
		public const int UniformBlockReferencedByComputeShader = (int)0x90EC; //UNIFORM_BLOCK_REFERENCED_BY_COMPUTE_SHADER 0x90EC
		public const int AtomicCounterBufferReferencedByComputeShader = (int)0x90ED; // ATOMIC_COUNTER_BUFFER_REFERENCED_BY_COMPUTE_SHADER 0x90ED

		public const GetPName MaxComputeUniformBlocks = (GetPName)0x91BB;
		public const GetPName MaxComputeTextureImageUnits = (GetPName)0x91BC;
		public const GetPName MaxComputeImageUniforms = (GetPName)0x91BD;
		public const GetIndexedPName MaxComputeWorkGroupCount = (GetIndexedPName)0x91BE;
		public const GetIndexedPName MaxComputeWorkGroupSize = (GetIndexedPName)0x91BF;

		public const GetPName MaxVertexAtomicCounterBuffers = (GetPName)0x92CC;
		public const GetPName MaxTessControlAtomicCounterBuffers = (GetPName)0x92CD;
		public const GetPName MaxTessEvaluationAtomicCounterBuffers = (GetPName)0x92CE;
		public const GetPName MaxGeometryAtomicCounterBuffers = (GetPName)0x92CF;
		public const GetPName MaxFragmentAtomicCounterBuffers = (GetPName)0x92C0;
		public const GetPName MaxCombinedAtomicCounterBuffers = (GetPName)0x92C1;

		public const GetPName MaxVertexAtomicCounters = (GetPName)0x92D2;
		public const GetPName MaxTessControlAtomicCounters = (GetPName)0x92D3;
		public const GetPName MaxTessEvaluationAtomicCounters = (GetPName)0x92D4;
		public const GetPName MaxGeometryAtomicCounters = (GetPName)0x92D5;
		public const GetPName MaxFragmentAtomicCounters = (GetPName)0x92D6;
		public const GetPName MaxCombinedAtomicCounters = (GetPName)0x92D7;

		public const ShaderType ComputeShader = (ShaderType)0x91B9;

		public delegate void glDispatchCompute(int num_groups_x, int num_groups_y, int num_groups_z);
		public delegate void glTexStorage2D(TextureTarget target, GLsizei levels, PixelInternalFormat internalformat, GLsizei width, GLsizei height);
		public delegate void glVertexAttribFormat(GLuint attribindex, GLint size, VertexAttribPointerType type, GLboolean normalized, GLuint relativeoffset);

		public static glDispatchCompute DispatchCompute;
		public static glTexStorage2D TexStorage2D;
		public static glVertexAttribFormat VertexAttribFormat;

		public static void Setup(IGraphicsContextInternal context)
		{
			MustGet(context, "glDispatchCompute", out DispatchCompute);
			MustGet(context, "glTexStorage2D", out TexStorage2D);
			MustGet(context, "glVertexAttribFormat", out VertexAttribFormat);
		}

		static void MustGet<T>(IGraphicsContextInternal context, string name, out T result) where T : class
		{
			IntPtr pointer = context.GetAddress(name);
			result = (T)(object)Marshal.GetDelegateForFunctionPointer(pointer, typeof(T));
		}
	}
}
