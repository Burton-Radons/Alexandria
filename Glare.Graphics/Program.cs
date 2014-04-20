using Glare.Graphics.Internal;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics {
	// TODO: Binary

	/// <summary>
	/// A program object is an object to which <see cref="Shader"/> objects can be attached. This provides a mechanism to specify the <see cref="Shader"/> objects that will be linked to create a <see cref="Program"/>. It also provides a means for checking the compatibility of the shaders that will be used to create a program (for instance, checking the compatibility between a <see cref="VertexShader"/> and a <see cref="FragmentShader"/>). When no longer needed as part of a program object, shader objects can be detached.
	/// 
	/// One or more executables are created in a <see cref="Program"/> object by successfully attaching <see cref="Shader"/> objects to it with <see cref="Attach"/>​, successfully compiling the <see cref="Shader"/>objects with <see cref="Shader.Compile"/>​, and successfully linking the program object with <see cref="Link"/>​. These executables are made part of current state when <see cref="Context.Program"/>​ is assigned. Program objects can be deleted by calling <see cref="Dispose"/>​. The memory associated with the <see cref="Program"/> object will be deleted when it is no longer part of current rendering state for any context.
	/// </summary>
	public class Program : GraphicsResource {
		readonly ProgramShaderCollection shaders;
		readonly ProgramObjectCollection<ProgramAttribute> attributes = new ProgramObjectCollection<ProgramAttribute>();
		readonly ProgramObjectCollection<ProgramUniform> uniforms = new ProgramObjectCollection<ProgramUniform>();
		readonly ReadOnlyCollection<ProgramStage> stages;
		readonly Dictionary<ShaderStage, ProgramStage> stagesByShaderStage = new Dictionary<ShaderStage, ProgramStage>();
		int[] resultInt3 = new int[3];

		public int ActiveAtomicCounterBuffers { get { return GetInt32(GetProgramParameterName.ActiveAtomicCounterBuffers); } }

		/// <summary>Get the <see cref="Shader"/> objects that are attached to the <see cref="Program"/>.</summary>
		public ProgramShaderCollection Shaders { get { return shaders; } }

		/// <summary>Get the collection of <see cref="ProgramAttribute"/> objects associated with the <see cref="Program"/>. These are cleared and recreated every time a <see cref="Link"/> occurs.</summary>
		public ProgramObjectCollection<ProgramAttribute> Attributes { get { return attributes; } }

		/*public ProgramBinary Binary
		{
			get
			{
				int binaryLength;
				BinaryFormat binaryFormat;
				byte[] binaryData;

				using (Context.Lock())
				{
					GL.GetProgram(Id, GetProgramParameterName.ProgramBinaryLength, out binaryLength);
					Context.CheckError();
					if (binaryLength == 0)
						return new ProgramBinary();
					binaryData = new byte[binaryLength];
					GL.GetProgramBinary(Id, binaryData.Length, out binaryLength, out binaryFormat, binaryData);
					return new ProgramBinary((int)binaryFormat, binaryData);
				}
			}

			set
			{
				using (Context.Lock())
					GL.ProgramBinary(Id, (BinaryFormat)value.BinaryFormat, value.BinaryData, value.BinaryData.Length);
			}
		}*/

		/// <summary>Get or set the <see cref="ComputeShader"/> in this <see cref="Program"/>.</summary>
		public ComputeShader ComputeShader {
			get { return GetShader<ComputeShader>(); }
			set { SetShader(value); }
		}

		/// <summary>Get the <see cref="ProgramStage"/> for the <see cref="ComputeShader"/>.</summary>
		public ProgramStage ComputeStage { get { return stagesByShaderStage[ShaderStage.Compute]; } }

		/// <summary>Get the local work group size of the compute program as specified by its input layout qualifier(s), or <c>(0, 0, 0)</c> if this does not contain a compute shader. This <see cref="Program"/> must have been previously <see cref="Link"/>ed successfully and contains a binary for the <see cref="ComputeShader"/> stage. This requires OpenGL 4.3 or greater.</summary>
		[GLMinimum(4, 3)]
		public Vector3i ComputeWorkGroupSize {
			get {
				if (!Context.CheckVersion(4, 3))
					return Vector3i.Zero;
				lock (resultInt3) {
					resultInt3[0] = resultInt3[1] = resultInt3[2] = 0;
					using (Context.Lock()) {
						GL.GetProgram(Id, GetProgramParameterName.MaxComputeWorkGroupSize, resultInt3);
						ErrorCode error = GL.GetError();
						if (error == ErrorCode.InvalidOperation) return Vector3i.Zero;
						Context.CheckError(error);
					}
					return new Vector3i(resultInt3);
				}
			}
		}

		/// <summary>Get or set the <see cref="FragmentShader"/> in this <see cref="Program"/>.</summary>
		public FragmentShader FragmentShader {
			get { return GetShader<FragmentShader>(); }
			set { SetShader(value); }
		}

		/// <summary>Get the <see cref="ProgramStage"/> for the <see cref="FragmentShader"/>.</summary>
		public ProgramStage FragmentStage { get { return stagesByShaderStage[ShaderStage.Fragment]; } }

		/// <summary>Get the primitive type accepted as input to the <see cref="GeometryShader"/> contained in the <see cref="Program"/>, or <c>0</c> if it does not contain one​.</summary>
		[GLMinimum(3, 2)]
		public Primitive GeometryInputType {
			get {
				using (Context.Lock()) {
					int result;
					GL.GetProgram(Id, GetProgramParameterName.GeometryInputType, out result);
					ErrorCode error = GL.GetError();
					if (error == ErrorCode.InvalidOperation) return 0;
					Context.CheckError(error);
					return (Primitive)result;
				}
			}
		}

		/// <summary>Get the primitive type accepted as input to the <see cref="GeometryShader"/> contained in the <see cref="Program"/>, or <see cref="Primitive.None"/>​ if it does not contain one.</summary>
		[GLMinimum(3, 2)]
		public Primitive GeometryOutputType {
			get {
				using (Context.Lock()) {
					int result;
					GL.GetProgram(Id, GetProgramParameterName.GeometryOutputType, out result);
					ErrorCode error = GL.GetError();
					if (error == ErrorCode.InvalidOperation) return Primitive.None;
					Context.CheckError(error);
					return (Primitive)result;
				}
			}
		}

		/// <summary>Get or set the <see cref="GeometryShader"/> in this <see cref="Program"/>.</summary>
		[GLMinimum(3, 2)]
		public GeometryShader GeometryShader {
			get { return GetShader<GeometryShader>(); }
			set { SetShader(value); }
		}

		/// <summary>Get the <see cref="ProgramStage"/> for the <see cref="GeometryShader"/>.</summary>
		public ProgramStage GeometryStage { get { return stagesByShaderStage[ShaderStage.Geometry]; } }

		/// <summary>Get the information log for a <see cref="Program"/> object. The information log for a program object is modified when the program object is linked or validated.</summary>
		public string InfoLog { get { using (Context.Lock()) return GL.GetProgramInfoLog(Id); } }

		/// <summary><c>true</c> if the last <see cref="Link"/> operation on this <see cref="Program"/>​ was successful, and <c>false</c>​ otherwise.</summary>
		public bool IsLinked { get { return GetInt32(GetProgramParameterName.LinkStatus) != 0; } }

		/// <summary><c>true</c> if the last <see cref="Validate"/> operation on the <see cref="Program"/> was successful, and <c>false</c> otherwise.</summary>
		public bool IsValidated { get { return GetInt32(GetProgramParameterName.ValidateStatus) != 0; } }

		/// <summary>Get the maximum number of vertices that the <see cref="GeometryShader"/> in the <see cref="Program"/>​ will output, or <c>0</c> if there is no <see cref="GeometryShader"/>.</summary>
		[GLMinimum(3, 2)]
		public int MaxGeometryVerticesOut {
			get {
				using (Context.Lock()) {
					int result;
					GL.GetProgram(Id, GetProgramParameterName.GeometryVerticesOut, out result);
					ErrorCode error = GL.GetError();
					if (error == ErrorCode.InvalidOperation) return 0;
					Context.CheckError(error);
					return result;
				}
			}
		}

		/// <summary>Get the collection <see cref="ProgramStage"/> for each <see cref="ShaderStage"/>.</summary>
		public ReadOnlyCollection<ProgramStage> Stages { get { return stages; } }

		/// <summary>Get or set the <see cref="TessellationControlShader"/> in this <see cref="Program"/>.</summary>
		public TessellationControlShader TessellationControlShader {
			get { return GetShader<TessellationControlShader>(); }
			set { SetShader(value); }
		}

		/// <summary>Get the <see cref="ProgramStage"/> for the <see cref="TessellationControlShader"/>.</summary>
		public ProgramStage TessellationControlStage { get { return stagesByShaderStage[ShaderStage.TessellationControl]; } }

		/// <summary>Get or set the <see cref="TessellationEvaluationShader"/> in this <see cref="Program"/>.</summary>
		public TessellationEvaluationShader TessellationEvaluationShader {
			get { return GetShader<TessellationEvaluationShader>(); }
			set { SetShader(value); }
		}

		/// <summary>Get the <see cref="ProgramStage"/> for the <see cref="TessellationEvaluationShader"/>.</summary>
		public ProgramStage TessellationEvaluationStage { get { return stagesByShaderStage[ShaderStage.TessellationEvaluation]; } }

		/// <summary>Get the buffer mode used when transform feedback is active. This may be <see cref="TransformFeedbackMode.SeparateAttributes"/>​ or <see cref="TransformFeedbackMode.InterleavedAttributes"/>​.
		/// </summary>
		public TransformFeedbackMode TransformFeedbackBufferMode { get { return (TransformFeedbackMode)GetInt32(GetProgramParameterName.TransformFeedbackBufferMode); } }

		/// <summary>Get the number of varying variables to capture in transform feedback mode for the <see cref="Program"/>.</summary>
		public int TransformFeedbackVaryingCount { get { return GetInt32(GetProgramParameterName.TransformFeedbackVaryings); } }

		/// <summary>Get the length of the longest variable name to be used for transform feedback, including the null-terminator.</summary>
		public int TransformFeedbackVaryingMaxLength { get { return GetInt32(GetProgramParameterName.TransformFeedbackVaryingMaxLength); } }

		/// <summary>Get the collection of <see cref="ProgramUniform"/> objects, that can be indexed by number or name. These are cleared and recreated whenever <see cref="Link"/> is called.</summary>
		public ProgramObjectCollection<ProgramUniform> Uniforms { get { return uniforms; } }

		/// <summary>Get or set the <see cref="VertexShader"/> in this <see cref="Program"/>.</summary>
		public VertexShader VertexShader {
			get { return GetShader<VertexShader>(); }
			set { SetShader(value); }
		}

		/// <summary>Get the <see cref="ProgramStage"/> for the <see cref="VertexShader"/>.</summary>
		public ProgramStage VertexStage { get { return stagesByShaderStage[ShaderStage.Vertex]; } }

		public Program()
			: base(AllocateId()) {
			shaders = new ProgramShaderCollection(this);

			ProgramStage[] stageList = new ProgramStage[]
			{
				new ProgramStage(this, ShaderStage.Compute),
				new ProgramStage(this, ShaderStage.Fragment),
				new ProgramStage(this, ShaderStage.Geometry),
				new ProgramStage(this, ShaderStage.TessellationControl),
				new ProgramStage(this, ShaderStage.TessellationEvaluation),
				new ProgramStage(this, ShaderStage.Vertex)
			};

			stages = new ReadOnlyCollection<ProgramStage>(stageList);
			foreach (ProgramStage stage in stageList)
				stagesByShaderStage[stage.Stage] = stage;
		}

		public Program(IEnumerable<Shader> shaders)
			: this() {
			this.shaders.AddRange(shaders);
			MustLink();
		}

		public Program(params Shader[] shaders) : this((IEnumerable<Shader>)shaders) { }

		static int AllocateId() {
			using (Context.Lock())
				return GL.CreateProgram();
		}

		internal ProgramBinding Bind() { return new ProgramBinding(this); }

		protected override void DisposeBase() {
			if (Context.Shared.MakeCurrent())
				GL.DeleteProgram(Id);
		}

		public void Draw(Primitive primitive, int vertexCount) { Draw(primitive, 0, vertexCount); }
		public void Draw(Primitive primitive, int firstVertex, int vertexCount) {
			Device.DrawStart(this);
			using (Device.Lock())
				GL.DrawArrays((PrimitiveType)primitive, firstVertex, vertexCount);
		}

		public void Draw(Primitive primitive, int vertexCount, GraphicsBuffer elementBuffer, ElementType elementType, int elementOffsetInBytes) {
			if (elementBuffer == null)
				throw new ArgumentNullException("elementBuffer");
			Device.DrawStart(this);
			using (Device.Lock()) {
				GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBuffer.Id);
				CheckError();
				GL.DrawElements((BeginMode)primitive, vertexCount, (DrawElementsType)elementType, elementOffsetInBytes);
				CheckError();
				GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
			}
		}

		public void Draw(Primitive primitive, int firstVertex, int vertexCount, GraphicsBuffer elementBuffer, ElementType elementType, int elementOffsetInBytes) {
			if (firstVertex < 0)
				throw new ArgumentOutOfRangeException("firstVertex");

			switch (elementType) {
				case ElementType.Byte: elementOffsetInBytes += firstVertex; break;
				case ElementType.UInt16: elementOffsetInBytes += firstVertex * 2; break;
				case ElementType.UInt32: elementOffsetInBytes += firstVertex * 4; break;
				default: throw new ArgumentException("elementType");
			}

			Draw(primitive, vertexCount, elementBuffer, elementType, elementOffsetInBytes);
		}

		int GetInt32(GetProgramParameterName pname) {
			int result;

			using (Context.Lock())
				GL.GetProgram(Id, pname, out result);
			return result;
		}

		TShader GetShader<TShader>() where TShader : Shader {
			foreach (Shader shader in shaders) {
				TShader tshader = shader as TShader;
				if (tshader != null)
					return tshader;
			}
			return null;
		}

		/// <summary>Attempt to link a <see cref="Program"/> object, returning success.</summary>
		/// <remarks>
		/// If any <see cref="Shader"/> objects of type <see cref="VertexShader"/>​ are attached to the <see cref="Program"/>​, they will be used to create an executable that will run on the programmable vertex processor. If any shader objects of type <see cref="GeometryShader"/>​ are attached to the <see cref="Program"/>​, they will be used to create an executable that will run on the programmable geometry processor. If any shader objects of type <see cref="FragmentShader"/> are attached to the <see cref="Program"/>​, they will be used to create an executable that will run on the programmable fragment processor.
		///  
		/// The status of the link operation will be stored as part of the program object's state. This value will be set to <c>true</c>​ if the program object was linked without errors and is ready for use, and <c>false</c>​ otherwise. It can be queried through <see cref="IsLinked"/>.
		/// 
		/// As a result of a successful link operation, all active user-defined uniform variables belonging to program​ will be initialized to 0, and each of the program object's active uniform variables can be accessed through <see cref="ActiveUniforms"/>. Also, any active user-defined attribute variables that have not been bound to a generic vertex attribute index will be bound to one at this time and accessed through <see cref="ActiveAttributes"/>.
		/// 
		/// Linking of a program object can fail for a number of reasons as specified in the OpenGL Shading Language Specification. The following lists some of the conditions that will cause a link error.
		/// <list type="bullet">
		/// <item><description>The number of active attribute variables supported by the implementation has been exceeded.</description></item>
		/// <item><description>The storage limit for uniform variables has been exceeded.</description></item>
		/// <item><description>The number of active uniform variables supported by the implementation has been exceeded.</description></item>
		/// <item><description>The main function is missing for the vertex, geometry or fragment shader.</description></item>
		/// <item><description>A varying variable actually used in the fragment shader is not declared in the same way (or is not declared at all) in the vertex shader, or geometry shader shader if present.</description></item>
		/// <item><description>A reference to a function or variable name is unresolved.</description></item>
		/// <item><description>A shared global is declared with two different types or two different initial values.</description></item>
		/// <item><description>One or more of the attached shader objects has not been successfully compiled.</description></item>
		/// <item><description>Binding a generic attribute matrix caused some rows of the matrix to fall outside the allowed maximum of <see cref="GraphicsProgram.MaxVertexAttributes"/>​.</description></item>
		/// <item><description>Not enough contiguous vertex attribute slots could be found to bind attribute matrices.</description></item>
		/// <item><description>The program object contains objects to form a fragment shader but does not contain objects to form a vertex shader.</description></item>
		/// <item><description>The program object contains objects to form a geometry shader but does not contain objects to form a vertex shader.</description></item>
		/// <item><description>The program object contains objects to form a geometry shader and the input primitive type, output primitive type, or maximum output vertex count is not specified in any compiled geometry shader object.</description></item>
		/// <item><description>The program object contains objects to form a geometry shader and the input primitive type, output primitive type, or maximum output vertex count is specified differently in multiple geometry shader objects.</description></item>
		/// <item><description>The number of active outputs in the fragment shader is greater than the value of <see cref="GraphicsProgram.MaxDrawBuffers"/>​.</description></item>
		/// <item><description>The program has an active output assigned to a location greater than or equal to the value of <see cref="GraphicsProgram.MaxDualSourceDrawBuffers"/> ​and has an active output assigned an index greater than or equal to one.</description></item>
		/// <item><description>More than one varying out variable is bound to the same number and index.</description></item>
		/// <item><description>The explicit binding assigments do not leave enough space for the linker to automatically assign a location for a varying out array, which requires multiple contiguous locations.</description></item>
		/// <item><description>The count​ specified by glTransformFeedbackVaryings​ is non-zero, but the program object has no vertex or geometry shader.</description></item>
		/// <item><description>Any variable name specified to glTransformFeedbackVaryings​ in the varyings​ array is not declared as an output in the vertex shader (or the geometry shader, if active).</description></item>
		/// <item><description>Any two entries in the varyings​ array given glTransformFeedbackVaryings​ specify the same varying variable.</description></item>
		/// <item><description>The total number of components to capture in any transform feedback varying variable is greater than the constant <see cref="Context.MaxTransformFeedbackSeparateComponents"/>​ and the buffer mode is <see cref="TransformFeedbackMode.SeparateAttributes"/>​.</description></item>
		/// </list>
		///
		/// When a program object has been successfully linked, the program object can be made part of current state by setting <see cref="Context.Program"/>​. Whether or not the link operation was successful, the program object's information log will be overwritten. The information log can be retrieved through <see cref="InfoLog"/>.
		/// 
		/// <see cref="Link"/> will also install the generated executables as part of the current rendering state if the link operation was successful and the specified program object is already currently in use as a result of a previous assignment to <see cref="Context.Program"/>. If the program object currently in use is relinked unsuccessfully, its link status will be set to <c>false</c>​ , but the executables and associated state will remain part of the current state until a subsequent assignment to <see cref="Context.Program"/> removes it from use. After it is removed from use, it cannot be made part of current state until it has been successfully relinked.
		/// 
		/// If program​ contains shader objects of type <see cref="VertexShader"/>​, and optionally of type <see cref="GeometryShader"/>​, but does not contain shader objects of type <see cref="FragmentShader"/>​, the vertex shader executable will be installed on the programmable vertex processor, the geometry shader executable, if present, will be installed on the programmable geometry processor, but no executable will be installed on the fragment processor. The results of rasterizing primitives with such a program will be undefined.
		/// 
		/// The program object's information log is updated and the program is generated at the time of the link operation. After the link operation, applications are free to modify attached shader objects, compile attached shader objects, detach shader objects, delete shader objects, and attach additional shader objects. None of these operations affects the information log or the program that is part of the program object.
		/// </remarks>
		/// <returns></returns>
		public virtual bool Link() {
			using (Context.Lock()) {
				foreach (ProgramStage stage in stagesByShaderStage.Values)
					stage.Unlink();

				GL.LinkProgram(Id);
				Context.CheckError();
				int result;
				GL.GetProgram(Id, GetProgramParameterName.LinkStatus, out result);

				attributes.Clear();

				if (result != 0) {
					int count, maxNameLength, length;
					StringBuilder name;

					// Gather the attributes
					GL.GetProgram(Id, GetProgramParameterName.ActiveAttributes, out count);
					GL.GetProgram(Id, GetProgramParameterName.ActiveAttributeMaxLength, out maxNameLength);
					name = new StringBuilder(maxNameLength);
					for (int index = 0; index < count; index++) {
						ActiveAttribType type;
						int size;

						name.Clear();
						GL.GetActiveAttrib(Id, index, maxNameLength, out length, out size, out type, name);
						attributes.Add(new ProgramAttribute(this, name.ToString(), index, size, type));
					}

					// Gather the uniforms
					GL.GetProgram(Id, GetProgramParameterName.ActiveUniforms, out count);
					GL.GetProgram(Id, GetProgramParameterName.ActiveUniformMaxLength, out maxNameLength);
					name = new StringBuilder(maxNameLength);
					for (int index = 0; index < count; index++) {
						int size;
						ActiveUniformType type;

						GL.GetActiveUniform(Id, index, maxNameLength, out length, out size, out type, name);
						Context.CheckError();

						string nameString = name.ToString();
						int location = GL.GetUniformLocation(Id, nameString);
						Context.CheckError();

						uniforms.Add(new ProgramUniform(this, nameString, index, location, type, size));
					}

					// Find slots for the textures.
					for (int index = 0, slot = 0; index < count; index++) {
						var uniform = Uniforms[index];
						if (uniform.IsTexture)
							uniform.Unit = TextureUnit.Texture0 + slot++;
					}

					// Have the stages gather data.
					foreach (ProgramStage stage in stagesByShaderStage.Values)
						stage.Link();
				}

				return result != 0;
			}
		}

		public void MustLink() {
			if (!Link())
				throw new Exception("Program could not be linked: " + InfoLog);
		}

		/// <summary>This is called immediately before a draw command is about to be executed while using this <see cref="Program"/>.</summary>
		protected internal virtual void OnDraw() {
		}

		void SetShader<TShader>(TShader value) where TShader : Shader {
			TShader shader = GetShader<TShader>();

			if (shader != null)
				shaders.Remove(shader);
			if (value != null)
				shaders.Add(value);
		}

		internal void Used() {
			foreach (ProgramStage stage in stagesByShaderStage.Values)
				stage.Used();
			foreach (ProgramUniform uniform in uniforms)
				uniform.Used();
		}
	}

	internal struct ProgramBinding : IDisposable {
		ContextLock contextLock;

		internal ProgramBinding(Program program) {
			contextLock = Context.Lock();
			GL.UseProgram(program.Id);
			Context.CheckError();
		}

		public void Dispose() {
			Context.CheckError();
			GL.UseProgram(Device.Program != null ? Device.Program.Id : 0);
			contextLock.Dispose();
		}
	}

	public struct ProgramBinary {
		readonly byte[] binaryData;
		readonly int binaryFormat;

		public byte[] BinaryData { get { return binaryData; } }

		public int BinaryFormat { get { return binaryFormat; } }

		public ProgramBinary(int binaryFormat, byte[] binaryData) {
			this.binaryFormat = binaryFormat;
			this.binaryData = binaryData;
		}

		public override string ToString() {
			if (binaryData == null || binaryData.Length == 0)
				return "ProgramBinary(null)";
			return "ProgramBinary(" + binaryFormat + ", " + binaryData.Length + " byte" + (binaryData.Length != 1 ? "s" : "") + ")";
		}
	}

	public class ProgramShaderCollection : ICollection<Shader> {
		readonly HashSet<Shader> set = new HashSet<Shader>();
		readonly Program program;

		/// <summary>Get or set the <see cref="Shader"/> for a given <see cref="ShaderStage"/>, or <c>null</c> if there is no shader at that stage.</summary>
		/// <param name="stage"></param>
		/// <returns></returns>
		public Shader this[ShaderStage stage] {
			get {
				foreach (Shader shader in this)
					if (shader.Stage == stage)
						return shader;
				return null;
			}

			set {
				var shader = this[stage];
				if (shader != null)
					Remove(shader);
				if (value != null)
					Add(value);
			}
		}

		internal ProgramShaderCollection(Program program) {
			this.program = program;
		}

		public void Add(Shader item) {
			if (item == null)
				throw new ArgumentNullException("item");
			using (Context.Lock())
				GL.AttachShader(program.Id, item.Id);
			set.Add(item);
		}

		public void AddRange(IEnumerable<Shader> items) { foreach (Shader shader in items) Add(shader); }
		public void AddRange(params Shader[] items) { AddRange((IEnumerable<Shader>)items); }

		public void Clear() {
			using (Context.Lock())
				foreach (var item in set)
					GL.DetachShader(program.Id, item.Id);
			set.Clear();
		}

		public bool Contains(Shader item) { return set.Contains(item); }
		public void CopyTo(Shader[] array, int arrayIndex) { set.CopyTo(array, arrayIndex); }
		public int Count { get { return set.Count; } }

		bool ICollection<Shader>.IsReadOnly { get { return false; } }

		public bool Remove(Shader item) {
			if (set.Remove(item)) {
				using (Context.Lock())
					GL.DetachShader(program.Id, item.Id);
				return true;
			}
			return false;
		}

		public HashSet<Shader>.Enumerator GetEnumerator() { return set.GetEnumerator(); }

		IEnumerator<Shader> IEnumerable<Shader>.GetEnumerator() { return GetEnumerator(); }
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return GetEnumerator(); }
	}

	public abstract class ProgramObject {
		protected readonly int index;
		readonly string name;
		readonly Program program;

		/// <summary>Get the zero-based index of the program object.</summary>
		public int Index { get { return index; } }

		public string Name { get { return name; } }

		public Program Program { get { return program; } }

		internal ProgramObject(Program program, string name, int index) {
			this.program = program;
			this.name = name;
			this.index = index;
		}

		public override string ToString() { return GetType().Name + "(" + Name + ", " + Index + ")"; }
	}

	public class ProgramObjectCollection<T> : IList<T> where T : ProgramObject {
		readonly List<T> List = new List<T>();
		readonly Dictionary<string, T> ByName = new Dictionary<string, T>();

		public int Count { get { return List.Count; } }

		public T this[int index] { get { return List[index]; } }
		public T this[string name] { get { return ByName[name]; } }

		/// <summary>
		/// Try to get an element with the given name. If it's found, return the element; otherwise return <c>null</c> .
		/// </summary>
		/// <param name="name">The name of the program object to return.</param>
		/// <returns>The program object or <c>null</c> if there is no such object.</returns>
		public T TryGetValue(string name) {
			T value;
			ByName.TryGetValue(name, out value);
			return value;
		}

		internal void Add(T item) {
			List.Add(item);
			ByName[item.Name] = item;
		}

		internal void Clear() { List.Clear(); ByName.Clear(); }

		public List<T>.Enumerator GetEnumerator() { return List.GetEnumerator(); }

		Exception ReadOnlyException() { return new InvalidOperationException("This is a read-only collection."); }

		public bool Contains(T item) { return List.Contains(item); }
		public void CopyTo(T[] array, int arrayIndex) { List.CopyTo(array, arrayIndex); }
		public int IndexOf(T item) { return List.IndexOf(item); }

		void IList<T>.Insert(int index, T item) { throw ReadOnlyException(); }
		void IList<T>.RemoveAt(int index) { throw ReadOnlyException(); }

		void ICollection<T>.Add(T item) { throw ReadOnlyException(); }
		void ICollection<T>.Clear() { throw ReadOnlyException(); }
		bool ICollection<T>.IsReadOnly { get { return true; } }
		bool ICollection<T>.Remove(T item) { throw ReadOnlyException(); }

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return GetEnumerator(); }
		IEnumerator<T> IEnumerable<T>.GetEnumerator() { return GetEnumerator(); }

		T IList<T>.this[int index] {
			get { return List[index]; }
			set { throw ReadOnlyException(); }
		}
	}
}
/*
GL_ACTIVE_ATTRIBUTES​ - params​ returns the number of active attribute variables for program​.
GL_ACTIVE_ATTRIBUTE_MAX_LENGTH​ - params​ returns the length of the longest active attribute name for program​, including the null termination character (i.e., the size of the character buffer required to store the longest attribute name). If no active attributes exist, 0 is returned.
GL_ACTIVE_UNIFORMS​ - params​ returns the number of active uniform variables for program​.
GL_ACTIVE_UNIFORM_MAX_LENGTH​ - params​ returns the length of the longest active uniform variable name for program​, including the null termination character (i.e., the size of the character buffer required to store the longest uniform variable name). If no active uniform variables exist, 0 is returned.
GL_PROGRAM_BINARY_LENGTH​ - params​ returns the length of the program binary, in bytes that will be returned by a call to glGetProgramBinary​. When a progam's GL_LINK_STATUS​ is GL_FALSE​, its program binary length is zero.

 */

/*		ProgramBinaryRetrievableHint = 33367,
		ProgramSeparable = 33368,
		ProgramBinaryLength = 34625,
		GeometryShaderInvocations = 34943,
		ActiveUniformBlockMaxNameLength = 35381,
		ActiveUniformBlocks = 35382,
		DeleteStatus = 35712,
		LinkStatus = 35714,
		ValidateStatus = 35715,
		InfoLogLength = 35716,
		AttachedShaders = 35717,
		ActiveUniforms = 35718,
		ActiveUniformMaxLength = 35719,
		ActiveAttributes = 35721,
		ActiveAttributeMaxLength = 35722,
		TransformFeedbackVaryingMaxLength = 35958,
		TransformFeedbackBufferMode = 35967,
		TransformFeedbackVaryings = 35971,
		GeometryVerticesOut = 36314,
		GeometryInputType = 36315,
		GeometryOutputType = 36316,
		TessControlOutputVertices = 36469,
		TessGenMode = 36470,
		TessGenSpacing = 36471,
		TessGenVertexOrder = 36472,
		TessGenPointMode = 36473,*/
