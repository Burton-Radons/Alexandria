using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics {
	public partial class ProgramUniform : ProgramObject {
		const bool Transpose = true;

		internal TextureUnit Unit;
		Texture Texture;

		readonly ActiveUniformType type;
		readonly int location;
		readonly int count;

		static object locker = new object();
		static IntPtr list = Marshal.AllocHGlobal(1024);
		static int listCount;

		static float[] floatList = new float[16];

		public int ComponentCount { get { return ComponentDimensions.Product; } }

		public Vector2i ComponentDimensions {
			get {
				switch (type) {
					case ActiveUniformType.Bool:
					case ActiveUniformType.Double:
					case ActiveUniformType.Float:
					case ActiveUniformType.Int:
					case ActiveUniformType.UnsignedInt:
						return new Vector2i(1, 1);
					case ActiveUniformType.BoolVec2:
					case ActiveUniformType.DoubleVec2:
					case ActiveUniformType.FloatVec2:
					case ActiveUniformType.IntVec2:
					case ActiveUniformType.UnsignedIntVec2:
						return new Vector2i(1, 2);
					case ActiveUniformType.BoolVec3:
					case ActiveUniformType.DoubleVec3:
					case ActiveUniformType.FloatVec3:
					case ActiveUniformType.IntVec3:
					case ActiveUniformType.UnsignedIntVec3:
						return new Vector2i(1, 3);
					case ActiveUniformType.BoolVec4:
					case ActiveUniformType.DoubleVec4:
					case ActiveUniformType.FloatVec4:
					case ActiveUniformType.IntVec4:
					case ActiveUniformType.UnsignedIntVec4:
						return new Vector2i(1, 4);
					//case ActiveUniformType.DoubleMat2:
					case ActiveUniformType.FloatMat2:
						return new Vector2i(2, 2);
					//case ActiveUniformType.DoubleMat2x3:
					case ActiveUniformType.FloatMat2x3:
						return new Vector2i(2, 3);
					//case ActiveUniformType.DoubleMat2x4:
					case ActiveUniformType.FloatMat2x4:
						return new Vector2i(2, 4);
					//case ActiveUniformType.DoubleMat3:
					case ActiveUniformType.FloatMat3:
						return new Vector2i(3, 3);
					//case ActiveUniformType.DoubleMat3x2:
					case ActiveUniformType.FloatMat3x2:
						return new Vector2i(3, 2);
					//case ActiveUniformType.DoubleMat3x4:
					case ActiveUniformType.FloatMat3x4:
						return new Vector2i(3, 4);
					//case ActiveUniformType.DoubleMat4:
					case ActiveUniformType.FloatMat4:
						return new Vector2i(4, 4);
					//case ActiveUniformType.DoubleMat4x2:
					case ActiveUniformType.FloatMat4x2:
						return new Vector2i(4, 2);
					//case ActiveUniformType.DoubleMat4x3:
					case ActiveUniformType.FloatMat4x3:
						return new Vector2i(4, 3);
					case ActiveUniformType.IntSampler1D:
					case ActiveUniformType.IntSampler1DArray:
					case ActiveUniformType.IntSampler2D:
					case ActiveUniformType.IntSampler2DArray:
					case ActiveUniformType.IntSampler2DMultisample:
					case ActiveUniformType.IntSampler2DMultisampleArray:
					case ActiveUniformType.IntSampler2DRect:
					case ActiveUniformType.IntSampler3D:
					case ActiveUniformType.IntSamplerBuffer:
					case ActiveUniformType.IntSamplerCube:
					case ActiveUniformType.IntSamplerCubeMapArray:
					case ActiveUniformType.Sampler1D:
					case ActiveUniformType.Sampler1DArray:
					case ActiveUniformType.Sampler1DArrayShadow:
					case ActiveUniformType.Sampler1DShadow:
					case ActiveUniformType.Sampler2D:
					case ActiveUniformType.Sampler2DArray:
					case ActiveUniformType.Sampler2DArrayShadow:
					case ActiveUniformType.Sampler2DMultisample:
					case ActiveUniformType.Sampler2DMultisampleArray:
					case ActiveUniformType.Sampler2DRect:
					case ActiveUniformType.Sampler2DRectShadow:
					case ActiveUniformType.Sampler2DShadow:
					case ActiveUniformType.Sampler3D:
					case ActiveUniformType.SamplerBuffer:
					case ActiveUniformType.SamplerCube:
					case ActiveUniformType.SamplerCubeMapArray:
					case ActiveUniformType.SamplerCubeMapArrayShadow:
					case ActiveUniformType.SamplerCubeShadow:
					case ActiveUniformType.UnsignedIntSampler1D:
					case ActiveUniformType.UnsignedIntSampler1DArray:
					case ActiveUniformType.UnsignedIntSampler2D:
					case ActiveUniformType.UnsignedIntSampler2DArray:
					case ActiveUniformType.UnsignedIntSampler2DMultisample:
					case ActiveUniformType.UnsignedIntSampler2DMultisampleArray:
					case ActiveUniformType.UnsignedIntSampler2DRect:
					case ActiveUniformType.UnsignedIntSampler3D:
					case ActiveUniformType.UnsignedIntSamplerBuffer:
					case ActiveUniformType.UnsignedIntSamplerCube:
					case ActiveUniformType.UnsignedIntSamplerCubeMapArray:
						return new Vector2i(1, 1);
					default: throw new NotImplementedException();
				}
			}
		}

		public Type ComponentType {
			get {
				switch (type) {
					case ActiveUniformType.Bool:
					case ActiveUniformType.BoolVec2:
					case ActiveUniformType.BoolVec3:
					case ActiveUniformType.BoolVec4:
						return typeof(bool);
					case ActiveUniformType.Double:
					/*case ActiveUniformType.DoubleMat2:
					case ActiveUniformType.DoubleMat2x3:
					case ActiveUniformType.DoubleMat2x4:
					case ActiveUniformType.DoubleMat3:
					case ActiveUniformType.DoubleMat3x2:
					case ActiveUniformType.DoubleMat3x4:
					case ActiveUniformType.DoubleMat4:
					case ActiveUniformType.DoubleMat4x2:
					case ActiveUniformType.DoubleMat4x3:*/
					case ActiveUniformType.DoubleVec2:
					case ActiveUniformType.DoubleVec3:
					case ActiveUniformType.DoubleVec4:
						return typeof(double);
					case ActiveUniformType.Float:
					case ActiveUniformType.FloatMat2:
					case ActiveUniformType.FloatMat2x3:
					case ActiveUniformType.FloatMat2x4:
					case ActiveUniformType.FloatMat3:
					case ActiveUniformType.FloatMat3x2:
					case ActiveUniformType.FloatMat3x4:
					case ActiveUniformType.FloatMat4:
					case ActiveUniformType.FloatMat4x2:
					case ActiveUniformType.FloatMat4x3:
					case ActiveUniformType.FloatVec2:
					case ActiveUniformType.FloatVec3:
					case ActiveUniformType.FloatVec4:
						return typeof(float);
					case ActiveUniformType.Int:
					case ActiveUniformType.IntVec2:
					case ActiveUniformType.IntVec3:
					case ActiveUniformType.IntVec4:
						return typeof(int);
					case ActiveUniformType.IntSampler1D:
					case ActiveUniformType.IntSampler1DArray:
					case ActiveUniformType.IntSampler2D:
					case ActiveUniformType.IntSampler2DArray:
					case ActiveUniformType.IntSampler2DMultisample:
					case ActiveUniformType.IntSampler2DMultisampleArray:
					case ActiveUniformType.IntSampler2DRect:
					case ActiveUniformType.IntSampler3D:
					case ActiveUniformType.IntSamplerBuffer:
					case ActiveUniformType.IntSamplerCube:
					case ActiveUniformType.IntSamplerCubeMapArray: throw new NotImplementedException();
					case ActiveUniformType.Sampler1D: return typeof(Texture1D);
					case ActiveUniformType.Sampler1DArray: return typeof(Texture1DArray);
					case ActiveUniformType.Sampler1DArrayShadow: throw new NotImplementedException();
					case ActiveUniformType.Sampler1DShadow: throw new NotImplementedException();
					case ActiveUniformType.Sampler2D: return typeof(Texture2D);
					case ActiveUniformType.Sampler2DArray: return typeof(Texture2DArray);
					case ActiveUniformType.Sampler2DArrayShadow:
					case ActiveUniformType.Sampler2DMultisample:
					case ActiveUniformType.Sampler2DMultisampleArray:
					case ActiveUniformType.Sampler2DRect:
					case ActiveUniformType.Sampler2DRectShadow:
					case ActiveUniformType.Sampler2DShadow: throw new NotImplementedException();
					case ActiveUniformType.Sampler3D: return typeof(Texture3D);
					case ActiveUniformType.SamplerBuffer: throw new NotImplementedException();
					case ActiveUniformType.SamplerCube: return typeof(TextureCube);
					case ActiveUniformType.SamplerCubeMapArray: return typeof(TextureCubeArray);
					case ActiveUniformType.SamplerCubeMapArrayShadow: throw new NotImplementedException();
					case ActiveUniformType.SamplerCubeShadow: throw new NotImplementedException();
					case ActiveUniformType.UnsignedInt:
					case ActiveUniformType.UnsignedIntVec2:
					case ActiveUniformType.UnsignedIntVec3:
					case ActiveUniformType.UnsignedIntVec4:
						return typeof(uint);
					case ActiveUniformType.UnsignedIntSampler1D:
					case ActiveUniformType.UnsignedIntSampler1DArray:
					case ActiveUniformType.UnsignedIntSampler2D:
					case ActiveUniformType.UnsignedIntSampler2DArray:
					case ActiveUniformType.UnsignedIntSampler2DMultisample:
					case ActiveUniformType.UnsignedIntSampler2DMultisampleArray:
					case ActiveUniformType.UnsignedIntSampler2DRect:
					case ActiveUniformType.UnsignedIntSampler3D:
					case ActiveUniformType.UnsignedIntSamplerBuffer:
					case ActiveUniformType.UnsignedIntSamplerCube:
					case ActiveUniformType.UnsignedIntSamplerCubeMapArray: throw new NotImplementedException();
					default: throw new NotImplementedException();
				}
			}
		}

		public int Count { get { return count; } }

		public bool IsTexture { get { return Type.IsSubclassOf(typeof(Texture)); } }

		TextureTarget TextureTarget {
			get {
				switch (type) {
					case ActiveUniformType.IntSampler1D:
					case ActiveUniformType.IntSampler1DArray:
					case ActiveUniformType.IntSampler2D:
					case ActiveUniformType.IntSampler2DArray:
					case ActiveUniformType.IntSampler2DMultisample:
					case ActiveUniformType.IntSampler2DMultisampleArray:
					case ActiveUniformType.IntSampler2DRect:
					case ActiveUniformType.IntSampler3D:
					case ActiveUniformType.IntSamplerBuffer:
					case ActiveUniformType.IntSamplerCube:
					case ActiveUniformType.IntSamplerCubeMapArray: throw new NotImplementedException();
					case ActiveUniformType.Sampler1D: return TextureTarget.Texture1D;
					case ActiveUniformType.Sampler1DArray: return TextureTarget.Texture1DArray;
					case ActiveUniformType.Sampler1DArrayShadow: throw new NotImplementedException();
					case ActiveUniformType.Sampler1DShadow: throw new NotImplementedException();
					case ActiveUniformType.Sampler2D: return TextureTarget.Texture2D;
					case ActiveUniformType.Sampler2DArray: return TextureTarget.Texture2DArray;
					case ActiveUniformType.Sampler2DArrayShadow: throw new NotImplementedException();
					case ActiveUniformType.Sampler2DMultisample: throw new NotImplementedException();
					case ActiveUniformType.Sampler2DMultisampleArray: throw new NotImplementedException();
					case ActiveUniformType.Sampler2DRect:
					case ActiveUniformType.Sampler2DRectShadow:
					case ActiveUniformType.Sampler2DShadow: throw new NotImplementedException();
					case ActiveUniformType.Sampler3D: return TextureTarget.Texture3D;
					case ActiveUniformType.SamplerBuffer: throw new NotImplementedException();
					case ActiveUniformType.SamplerCube: return TextureTarget.TextureCubeMap;
					case ActiveUniformType.SamplerCubeMapArray: return TextureTarget.TextureCubeMapArray;
					case ActiveUniformType.SamplerCubeMapArrayShadow:
					case ActiveUniformType.SamplerCubeShadow: throw new NotImplementedException();
					case ActiveUniformType.UnsignedIntSampler1D:
					case ActiveUniformType.UnsignedIntSampler1DArray:
					case ActiveUniformType.UnsignedIntSampler2D:
					case ActiveUniformType.UnsignedIntSampler2DArray:
					case ActiveUniformType.UnsignedIntSampler2DMultisample:
					case ActiveUniformType.UnsignedIntSampler2DMultisampleArray:
					case ActiveUniformType.UnsignedIntSampler2DRect:
					case ActiveUniformType.UnsignedIntSampler3D:
					case ActiveUniformType.UnsignedIntSamplerBuffer:
					case ActiveUniformType.UnsignedIntSamplerCube:
					case ActiveUniformType.UnsignedIntSamplerCubeMapArray: throw new NotImplementedException();
					default: throw new InvalidOperationException();
				}
			}
		}

		public Type Type {
			get {
				switch (type) {
					case ActiveUniformType.Bool: return typeof(bool);
					case ActiveUniformType.BoolVec2:
					case ActiveUniformType.BoolVec3:
					case ActiveUniformType.BoolVec4: throw new NotImplementedException();
					case ActiveUniformType.Double: return typeof(double);
					/*case ActiveUniformType.DoubleMat2: return typeof(Matrix2d);
					case ActiveUniformType.DoubleMat2x3: throw new NotImplementedException();
					case ActiveUniformType.DoubleMat2x4: throw new NotImplementedException();
					case ActiveUniformType.DoubleMat3: return typeof(Matrix3d);
					case ActiveUniformType.DoubleMat3x2: throw new NotImplementedException();
					case ActiveUniformType.DoubleMat3x4: throw new NotImplementedException();
					case ActiveUniformType.DoubleMat4: return typeof(Matrix4d);
					case ActiveUniformType.DoubleMat4x2: throw new NotImplementedException();
					case ActiveUniformType.DoubleMat4x3: throw new NotImplementedException();*/
					case ActiveUniformType.DoubleVec2: return typeof(Vector2d);
					case ActiveUniformType.DoubleVec3: return typeof(Vector3d);
					case ActiveUniformType.DoubleVec4: return typeof(Vector4d);
					case ActiveUniformType.Float: return typeof(float);
					case ActiveUniformType.FloatMat2: return typeof(Matrix2f);
					case ActiveUniformType.FloatMat2x3: throw new NotImplementedException();
					case ActiveUniformType.FloatMat2x4: throw new NotImplementedException();
					case ActiveUniformType.FloatMat3: return typeof(Matrix3f);
					case ActiveUniformType.FloatMat3x2: throw new NotImplementedException();
					case ActiveUniformType.FloatMat3x4: throw new NotImplementedException();
					case ActiveUniformType.FloatMat4: return typeof(Matrix4f);
					case ActiveUniformType.FloatMat4x2: throw new NotImplementedException();
					case ActiveUniformType.FloatMat4x3: throw new NotImplementedException();
					case ActiveUniformType.FloatVec2: return typeof(Vector2f);
					case ActiveUniformType.FloatVec3: return typeof(Vector3f);
					case ActiveUniformType.FloatVec4: return typeof(Vector4f);
					case ActiveUniformType.Int: return typeof(int);
					case ActiveUniformType.IntSampler1D:
					case ActiveUniformType.IntSampler1DArray:
					case ActiveUniformType.IntSampler2D:
					case ActiveUniformType.IntSampler2DArray:
					case ActiveUniformType.IntSampler2DMultisample:
					case ActiveUniformType.IntSampler2DMultisampleArray:
					case ActiveUniformType.IntSampler2DRect:
					case ActiveUniformType.IntSampler3D:
					case ActiveUniformType.IntSamplerBuffer:
					case ActiveUniformType.IntSamplerCube:
					case ActiveUniformType.IntSamplerCubeMapArray: throw new NotImplementedException();
					case ActiveUniformType.IntVec2: return typeof(Vector2i);
					case ActiveUniformType.IntVec3: return typeof(Vector3i);
					case ActiveUniformType.IntVec4: return typeof(Vector4i);
					case ActiveUniformType.Sampler1D: return typeof(Texture1D);
					case ActiveUniformType.Sampler1DArray: return typeof(Texture1DArray);
					case ActiveUniformType.Sampler1DArrayShadow: throw new NotImplementedException();
					case ActiveUniformType.Sampler1DShadow: throw new NotImplementedException();
					case ActiveUniformType.Sampler2D: return typeof(Texture2D);
					case ActiveUniformType.Sampler2DArray: return typeof(Texture2DArray);
					case ActiveUniformType.Sampler2DArrayShadow: throw new NotImplementedException();
					case ActiveUniformType.Sampler2DMultisample: throw new NotImplementedException();
					case ActiveUniformType.Sampler2DMultisampleArray: throw new NotImplementedException();
					case ActiveUniformType.Sampler2DRect:
					case ActiveUniformType.Sampler2DRectShadow:
					case ActiveUniformType.Sampler2DShadow: throw new NotImplementedException();
					case ActiveUniformType.Sampler3D: return typeof(Texture3D);
					case ActiveUniformType.SamplerBuffer: throw new NotImplementedException();
					case ActiveUniformType.SamplerCube: return typeof(TextureCube);
					case ActiveUniformType.SamplerCubeMapArray: return typeof(TextureCubeArray);
					case ActiveUniformType.SamplerCubeMapArrayShadow:
					case ActiveUniformType.SamplerCubeShadow: throw new NotImplementedException();
					case ActiveUniformType.UnsignedInt: return typeof(uint);
					case ActiveUniformType.UnsignedIntSampler1D:
					case ActiveUniformType.UnsignedIntSampler1DArray:
					case ActiveUniformType.UnsignedIntSampler2D:
					case ActiveUniformType.UnsignedIntSampler2DArray:
					case ActiveUniformType.UnsignedIntSampler2DMultisample:
					case ActiveUniformType.UnsignedIntSampler2DMultisampleArray:
					case ActiveUniformType.UnsignedIntSampler2DRect:
					case ActiveUniformType.UnsignedIntSampler3D:
					case ActiveUniformType.UnsignedIntSamplerBuffer:
					case ActiveUniformType.UnsignedIntSamplerCube:
					case ActiveUniformType.UnsignedIntSamplerCubeMapArray: throw new NotImplementedException();
					case ActiveUniformType.UnsignedIntVec2: return typeof(Vector2ui);
					case ActiveUniformType.UnsignedIntVec3: return typeof(Vector3ui);
					case ActiveUniformType.UnsignedIntVec4: return typeof(Vector4ui);
					default: throw new NotImplementedException();
				}
			}
		}

		internal ProgramUniform(Program program, string name, int index, int location, ActiveUniformType type, int count)
			: base(program, name, index) {
			this.type = type;
			this.location = location;
			this.count = count;
		}

		/*static ~ProgramUniform()
		{
			Marshal.FreeHGlobal(list);
		}*/

		Exception ConversionException(Type from) { return new InvalidOperationException("This " + typeof(ProgramUniform).Name + " requires some form of " + type + ", which is incompatible with " + from.Name + "."); }

		unsafe static void GetList(int count) {
			if (count > listCount) {
				list = Marshal.ReAllocHGlobal(list, new IntPtr(count * 4));
				listCount = count;
			}
		}

		Exception InvalidTypeException(Type source) {
			return new Exception("This " + typeof(ProgramUniform).Name + " type is not convertable from " + source.Name + ".");
		}

		public void Set(double value) {
			using (Context.Lock())
				switch (type) {
					case ActiveUniformType.Bool: GL.ProgramUniform1(Program.Id, location, value != 0 ? 1 : 0); break;
					case ActiveUniformType.Double: GL.ProgramUniform1(Program.Id, location, (double)value); break;
					case ActiveUniformType.Int: GL.ProgramUniform1(Program.Id, location, (int)value); break;
					case ActiveUniformType.UnsignedInt: GL.ProgramUniform1(Program.Id, location, (uint)value); break;
					default: throw InvalidTypeException(typeof(int));
				}
		}

		public void Set(float value) {
			using (Context.Lock())
				switch (type) {
					case ActiveUniformType.Bool: GL.ProgramUniform1(Program.Id, location, value != 0 ? 1 : 0); break;
					case ActiveUniformType.Double: GL.ProgramUniform1(Program.Id, location, (double)value); break;
					case ActiveUniformType.Int: GL.ProgramUniform1(Program.Id, location, (int)value); break;
					case ActiveUniformType.UnsignedInt: GL.ProgramUniform1(Program.Id, location, (uint)value); break;
					default: throw InvalidTypeException(typeof(int));
				}
		}

		public void Set(int value) {
			using (Context.Lock())
				switch (type) {
					case ActiveUniformType.Bool: GL.ProgramUniform1(Program.Id, location, value != 0 ? 1 : 0); break;
					case ActiveUniformType.Double: GL.ProgramUniform1(Program.Id, location, (double)value); break;
					case ActiveUniformType.Int: GL.ProgramUniform1(Program.Id, location, (int)value); break;
					case ActiveUniformType.UnsignedInt: GL.ProgramUniform1(Program.Id, location, (uint)value); break;
					default: throw InvalidTypeException(typeof(int));
				}
		}

		public void Set(uint value) {
			using (Context.Lock())
				switch (type) {
					case ActiveUniformType.Bool: GL.ProgramUniform1(Program.Id, location, value != 0 ? 1 : 0); break;
					case ActiveUniformType.Double: GL.ProgramUniform1(Program.Id, location, (double)value); break;
					case ActiveUniformType.Int: GL.ProgramUniform1(Program.Id, location, (int)value); break;
					case ActiveUniformType.UnsignedInt: GL.ProgramUniform1(Program.Id, location, (uint)value); break;
				}
		}

		public void Set(Matrix4d value) { Set(ref value); }
		public void Set(Matrix4f value) { Set(ref value); }

		public void Set(ref Matrix4f value) {
			using (Context.Lock())
				switch (type) {
					case ActiveUniformType.FloatMat4: GL.ProgramUniformMatrix4(Program.Id, location, 1, Transpose, ref value.XX); break;
					//case ActiveUniformType.DoubleMat4:
					//Matrix4d as4d = value;
					//GL.ProgramUniformMatrix4(Program.Id, location, 1, Transpose, ref as4d.XX); break;
					default: throw new NotImplementedException();
				}
		}

		public void Set(ref Matrix4d value) {
			using (Context.Lock())
				switch (type) {
					case ActiveUniformType.FloatMat4:
						Matrix4f as4f = (Matrix4f)value;
						GL.ProgramUniformMatrix4(Program.Id, location, 1, Transpose, ref as4f.XX); break;
					//case ActiveUniformType.DoubleMat4: GL.ProgramUniformMatrix4(Program.Id, location, 1, Transpose, ref value.XX); break;
					default: throw new NotImplementedException();
				}
		}

		public void Set(Texture2D texture) {
			Set(texture, ActiveUniformType.Sampler2D, typeof(Texture2D));
		}

		void Set(Texture texture, ActiveUniformType required, Type givenType) {
			if (type != required)
				throw ConversionException(givenType);

			if (object.ReferenceEquals(Context.Current.program, Program)) {
				using (Context.Lock()) {
					GL.ActiveTexture(Unit);
					Context.CheckError();
					GL.BindTexture(TextureTarget, texture != null ? texture.Id : 0);
				}
			}

			Texture = texture;
		}

		static void SetupFloatList(int count) { GetList(count); SetupList(ref floatList, count); }

		static void SetupList<T>(ref T[] list, int count) { if (count > list.Length) list = new T[(count + 1) * 3 / 2]; }

		internal void Used() {
			if (IsTexture) {
				GL.ActiveTexture(Unit);
				Context.CheckError();
				GL.BindTexture(TextureTarget, Texture != null ? Texture.Id : 0);
				Context.CheckError();
			}
		}
	}
}
