using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics {
	public class ProgramStage {
		readonly Program program;
		readonly ShaderStage stage;
		readonly ProgramObjectCollection<ProgramSubroutine> subroutines = new ProgramObjectCollection<ProgramSubroutine>();
		readonly ProgramObjectCollection<ProgramSubroutineUniform> uniforms = new ProgramObjectCollection<ProgramSubroutineUniform>();

		internal int[] ids;

		/// <summary>Get the <see cref="Glare.Graphics.Program"/> this is a stage for.</summary>
		public Program Program { get { return program; } }

		/// <summary>
		/// Get or set the <see cref="Shader"/> at this <see cref="Stage"/>, or <c>null</c> if there is none.
		/// </summary>
		public Shader Shader {
			get { return program.Shaders[Stage]; }
			set { program.Shaders[Stage] = value; }
		}

		internal ShaderType ShaderType { get { return (ShaderType)stage; } }

		/// <summary>Get the <see cref="ShaderStage"/> this applies to.</summary>
		public ShaderStage Stage { get { return stage; } }

		/// <summary>Get the collection of <see cref="ProgramSubroutine"/>s that are defined by this <see cref="ProgramStage"/>.</summary>
		public ProgramObjectCollection<ProgramSubroutine> Subroutines { get { return subroutines; } }

		/// <summary>Get the collection of <see cref="ProgramSubroutineUniform"/>s that are defined in this <see cref="ProgramStage"/>.</summary>
		public ProgramObjectCollection<ProgramSubroutineUniform> Uniforms { get { return uniforms; } }

		internal ProgramStage(Program program, ShaderStage stage) {
			this.program = program;
			this.stage = stage;
		}

		int Get1i(ProgramStageParameter pname) {
			int value;

			using (Context.Lock())
				GL.GetProgramStage(program.Id, (ShaderType)stage, pname, out value);
			return value;
		}

		internal void Link() {
			Unlink();

			using (Context.Lock()) {
				int activeSubroutineMaxLength = Get1i(ProgramStageParameter.ActiveSubroutineMaxLength); // Longest subroutine name for the stage.
				int activeSubroutineUniformMaxLength = Get1i(ProgramStageParameter.ActiveSubroutineUniformMaxLength); // Longest subroutine uniform name for the stage.
				int activeSubroutines = Get1i(ProgramStageParameter.ActiveSubroutines); // The number of active subroutines in the stage.
				int activeSubroutineUniformLocations = Get1i(ProgramStageParameter.ActiveSubroutineUniformLocations); // The number of active subroutine variable locations in the stage.
				int activeSubroutineUniforms = Get1i(ProgramStageParameter.ActiveSubroutineUniforms); // The number of active subroutine variables in the stage.

				if (activeSubroutines != 0 || activeSubroutineUniforms != 0) {
					StringBuilder builder = new StringBuilder(Math.Max(activeSubroutineUniformMaxLength, activeSubroutineMaxLength));
					int length;

					ids = new int[activeSubroutineUniforms];

					// Gather the subroutines
					for (int index = 0; index < activeSubroutines; index++) {
						GL.GetActiveSubroutineName(Program.Id, ShaderType, index, activeSubroutineMaxLength, out length, builder);
						subroutines.Add(new ProgramSubroutine(this, builder.ToString(), index));
						builder.Clear();
					}

					// Gather the subroutine uniforms
					for (int index = 0; index < activeSubroutineUniforms; index++) {
						GL.GetActiveSubroutineUniformName(Program.Id, ShaderType, index, activeSubroutineUniformMaxLength, out length, builder);
						uniforms.Add(new ProgramSubroutineUniform(this, builder.ToString(), index));
						builder.Clear();
					}
				}
			}
		}

		internal void Unlink() {
			subroutines.Clear();
			uniforms.Clear();
		}

		internal void Used() {
			if (object.ReferenceEquals(Program, Graphics.Program)) {
				using (Context.Lock())
					GL.UniformSubroutines(ShaderType, ids.Length, ids);
			}
		}
	}

	public abstract class ProgramStageObject : ProgramObject {
		readonly ProgramStage stage;

		public ShaderStage ShaderStage { get { return stage.Stage; } }

		internal ShaderType ShaderType { get { return (ShaderType)ShaderStage; } }

		public ProgramStage Stage { get { return stage; } }

		internal ProgramStageObject(ProgramStage stage, string name, int index)
			: base(stage.Program, name, index) {
			this.stage = stage;
		}
	}

	public class ProgramSubroutine : ProgramStageObject {
		internal ProgramSubroutine(ProgramStage stage, string name, int index)
			: base(stage, name, index) {
		}
	}

	public class ProgramSubroutineUniform : ProgramStageObject {
		readonly ProgramObjectCollection<ProgramSubroutine> compatible = new ProgramObjectCollection<ProgramSubroutine>();
		readonly int location;

		public ProgramObjectCollection<ProgramSubroutine> Compatible { get { return compatible; } }

		public int Count { get { return Get1i(ActiveSubroutineUniformParameter.UniformSize); } }

		public int Location { get { return location; } }

		public ProgramSubroutine Subroutine {
			get { return Stage.Subroutines[Stage.ids[location]]; }

			set {
				if (value == null)
					throw new ArgumentNullException("value");
				if (!object.ReferenceEquals(Stage, value.Stage))
					throw new ArgumentException(typeof(ProgramSubroutine).Name + "'s Stage is from a different Stage than this " + typeof(ProgramSubroutineUniform).Name + ".");
				if (!compatible.Contains(value))
					throw new ArgumentException("This is not a valid subroutine for this uniform.");
				Stage.ids[location] = value.Index;
				Stage.Used();
			}
		}

		internal ProgramSubroutineUniform(ProgramStage stage, string name, int index)
			: base(stage, name, index) {
			int[] idList = new int[Get1i(ActiveSubroutineUniformParameter.NumCompatibleSubroutines)];
			GL.GetActiveSubroutineUniform(Program.Id, ShaderType, index, ActiveSubroutineUniformParameter.CompatibleSubroutines, idList);
			Context.CheckError();
			for (int idIndex = 0; idIndex < idList.Length; idIndex++)
				compatible.Add(stage.Subroutines[idList[idIndex]]);

			this.location = GL.GetSubroutineUniformLocation(Program.Id, ShaderType, Name);
			Context.CheckError();

			stage.ids[location] = idList[0];
		}

		int Get1i(ActiveSubroutineUniformParameter pname) { int value; using (Context.Lock()) GL.GetActiveSubroutineUniform(Program.Id, ShaderType, Index, pname, out value); return value; }

		public void Set(string subroutineName) {
			Subroutine = Compatible[subroutineName];
		}
	}
}
