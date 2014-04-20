using Glare.Graphics.Internal;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics {
	/// <summary>
	/// The capabilities and implementation information of a <see cref="Device"/> provider. A singleton of this is returned from <see cref="Device.Capabilities"/>.
	/// </summary>
	public class Capabilities {
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly ComputeShaderStageCapabilities computeStage;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly ReadOnlyCollection<string> extensions;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly HashSet<string> extensionsSet;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly FragmentShaderStageCapabilities fragmentStage;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly GeometryShaderStageCapabilities geometryStage;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		ReadOnlyCollection<int> programBinaryFormats;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		ReadOnlyCollection<int> shaderBinaryFormats;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly GraphicsVersion shadingLanguage;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly TesselationControlShaderStageCapabilities tesselationControlStage;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly TesselationEvaluationShaderStageCapabilities tesselationEvaluationStage;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly GraphicsVersion version;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly VertexShaderStageCapabilities vertexStage;

		/// <summary>Get the minimum aliased line width in X and the maximum aliased line with in Y. <see cref="MinAliasedLineWidth"/> and <see cref="MaxAliasedLineWidth"/> separate the two.</summary>
		public Vector2d AliasedLineWidthRange { get { return Get2d(GetPName.AliasedLineWidthRange); } }

		/// <summary>Get <see cref="ComputeShaderStageCapabilities"/> for the <see cref="ComputeShader"/> <see cref="ShaderStage"/> (<see cref="ShaderStage.Compute"/>).</summary>
		public ComputeShaderStageCapabilities ComputeStage { get { return computeStage; } }

		/// <summary>Get the collection of extensions supported by the <see cref="Context"/>.</summary>
		public ReadOnlyCollection<string> Extensions { get { return extensions; } }

		/// <summary>Get <see cref="FragmentShaderStageCapabilities"/> for the <see cref="FragmentShader"/> <see cref="ShaderStage"/> (<see cref="ShaderStage.Fragment"/>).</summary>
		public FragmentShaderStageCapabilities FragmentStage { get { return fragmentStage; } }

		/// <summary>Get <see cref="GeometryShaderStageCapabilities"/> for the <see cref="GeometryShader"/> <see cref="ShaderStage"/> (<see cref="ShaderStage.Geometry"/>).</summary>
		public GeometryShaderStageCapabilities GeometryStage { get { return geometryStage; } }

		/// <summary>Get the maximum aliased <see cref="LineWidth"/>. This is separated from <see cref="AliasedLineWidthRange"/>.</summary>
		public double MaxAliasedLineWidth { get { return AliasedLineWidthRange.Y; } }

		/// <summary>Get the maximum number of uniform blocks per <see cref="Program"/>. The value must be at least 70.</summary>
		public int MaxCombinedUniformBlocks { get { return Get1i(GetPName.MaxCombinedUniformBlocks); } }

		public int MaxTextureDimension2D { get { return Device.GetInt32(GetPName.MaxTextureSize); } }
		public int MaxTextureDimension3D { get { return Device.GetInt32(GetPName.Max3DTextureSize); } }
		public int MaxTextureDimensionCube { get { return Device.GetInt32(GetPName.MaxCubeMapTextureSize); } }
		public int MaxTextureArrayLayers { get { return Device.GetInt32(GetPName.MaxArrayTextureLayers); } }
		public double MaxTextureLodBias { get { return Device.GetDouble(GetPName.MaxTextureLodBias); } }

		/// <summary>Get the maximum size in basic machine units of a uniform block, which must be at least 16384.</summary>
		public int MaxUniformBlockSize { get { return Get1i(GetPName.MaxUniformBlockSize); } }

		/// <summary>Get the maximum number of uniform buffer binding points on the context, which must be at least 36.</summary>
		public int MaxUniformBufferBindings { get { return Get1i(GetPName.MaxUniformBufferBindings); } }

		/// <summary>Get the minimum aliased <see cref="LineWidth"/>. This is separated from <see cref="AliasedLineWidthRange"/>.</summary>
		public double MinAliasedLineWidth { get { return AliasedLineWidthRange.X; } }

		public ReadOnlyCollection<int> ProgramBinaryFormats {
			get {
				if (programBinaryFormats == null) {
					using (Context.Lock()) {
						var list = new int[Device.GetInt32(GetPName.NumProgramBinaryFormats)];
						if (list.Length > 0)
							GL.GetInteger(GetPName.ProgramBinaryFormats, list);
						programBinaryFormats = new ReadOnlyCollection<int>(list);
					}
				}
				return programBinaryFormats;
			}
		}

		/// <summary>Get the renderer version string.</summary>
		public string Renderer { get { return GL.GetString(StringName.Renderer); } }

		/// <summary>Get the list of binary shader formats supported by this GL.</summary>
		public ReadOnlyCollection<int> ShaderBinaryFormats {
			get {
				if (shaderBinaryFormats == null) {
					using (Context.Lock()) {
						var list = new int[Device.GetInt32(GetPName.NumShaderBinaryFormats)];
						if (list.Length > 0)
							GL.GetInteger(GetPName.ShaderBinaryFormats, list);
						shaderBinaryFormats = new ReadOnlyCollection<int>(list);
					}
				}
				return shaderBinaryFormats;
			}
		}

		/// <summary>Get version information for the supported GLSL.</summary>
		public GraphicsVersion ShadingLanguage { get { return shadingLanguage; } }

		/// <summary>Get <see cref="TesselationControlShaderStageCapabilities"/> for the <see cref="TessellationControlShader"/> <see cref="ShaderStage"/> (<see cref="ShaderStage.TessellationControl"/>).</summary>
		public TesselationControlShaderStageCapabilities TesselationControlStage { get { return tesselationControlStage; } }

		/// <summary>Get <see cref="TesselationEvaluationShaderStageCapabilities"/> for the <see cref="TessellationEvaluationShader"/> <see cref="ShaderStage"/> (<see cref="ShaderStage.TessellationEvaluation"/>).</summary>
		public TesselationEvaluationShaderStageCapabilities TesselationEvaluationStage { get { return tesselationEvaluationStage; } }

		/// <summary>Get the company responsible for the GL release.</summary>
		public string Vendor { get { return GL.GetString(StringName.Vendor); } }

		/// <summary>Get a version or release number.</summary>
		public GraphicsVersion Version { get { return version; } }

		/// <summary>Get <see cref="VertexShaderStageCapabilities"/> for the <see cref="VertexShader"/> <see cref="ShaderStage"/> (<see cref="ShaderStage.Vertex"/>).</summary>
		public VertexShaderStageCapabilities VertexStage { get { return vertexStage; } }

		internal Capabilities() {
			computeStage = new ComputeShaderStageCapabilities();
			fragmentStage = new FragmentShaderStageCapabilities();
			geometryStage = new GeometryShaderStageCapabilities();
			tesselationControlStage = new TesselationControlShaderStageCapabilities();
			tesselationEvaluationStage = new TesselationEvaluationShaderStageCapabilities();
			vertexStage = new VertexShaderStageCapabilities();

			Context.CheckError();
			extensions = new ReadOnlyCollection<string>(GL.GetString(StringName.Extensions).Split(' '));
			GL.GetError(); // InvalidEnum set even though the call goes fine.
			extensionsSet = new HashSet<string>(extensions);
			Context.CheckError();
			shadingLanguage = new GraphicsVersion(GL.GetString(StringName.ShadingLanguageVersion));
			Context.CheckError();
			version = new GraphicsVersion(GL.GetString(StringName.Version));
			Context.CheckError();
		}

		Vector2d Get2d(GetPName pname) { return Device.GetVector2d(pname); }

		int Get1i(GetPName pname) { return Device.GetInt32(pname); }

		/// <summary>Get the <see cref="ShaderStageCapabilities"/> that describe the type, which must be a subclass of <see cref="Shader"/>.</summary>
		/// <typeparam name="TShader">The <see cref="Type"/> of the <see cref="Shader"/> stage to return. It cannot be <see cref="Shader"/> itself.</typeparam>
		/// <returns>The <see cref="ShaderStageCapabilities"/> of the given stage.</returns>
		public ShaderStageCapabilities GetShaderStage<TShader>() where TShader : Shader { return GetShaderStage(typeof(TShader)); }

		/// <summary>Get the <see cref="ShaderStageCapabilities"/> that describe the type, which must be a subclass of <see cref="Shader"/>.</summary>
		/// <param name="type">The <see cref="Type"/> of the <see cref="Shader"/> stage to return.</param>
		/// <returns>The <see cref="ShaderStageCapabilities"/> of the given stage.</returns>
		public ShaderStageCapabilities GetShaderStage(Type type) {
			if (type == null)
				throw new ArgumentNullException("type");
			switch (type.Name) {
				case "ComputeShader": return ComputeStage;
				case "FragmentShader": return FragmentStage;
				case "GeometryShader": return GeometryStage;
				case "TesselationControlShader": return TesselationControlStage;
				case "TesselationEvaluationShader": return TesselationEvaluationStage;
				case "VertexShader": return VertexStage;
				default: throw new ArgumentException("Type " + type.Name + " is not a subclass of " + typeof(Shader).Name + ".");
			}
		}

		/// <summary>Get the <see cref="ShaderStageCapabilities"/> of the corresponding <see cref="ShaderStage"/>.</summary>
		/// <param name="stage">The <see cref="ShaderStage"/> to match.</param>
		/// <returns>The corresponding <see cref="ShaderStageCapabilities"/>.</returns>
		public ShaderStageCapabilities GetShaderStage(ShaderStage stage) {
			switch (stage) {
				case ShaderStage.Compute: return ComputeStage;
				case ShaderStage.Fragment: return FragmentStage;
				case ShaderStage.Geometry: return GeometryStage;
				case ShaderStage.TessellationControl: return TesselationControlStage;
				case ShaderStage.TessellationEvaluation: return TesselationEvaluationStage;
				case ShaderStage.Vertex: return VertexStage;
				default: throw new ArgumentOutOfRangeException("stage");
			}
		}

		/// <summary>Get whether the given extension is supported by this implementation.</summary>
		/// <param name="name">The name of the extension.</param>
		/// <returns>Whether the extension is supported.</returns>
		public bool HasExtension(string name) { return extensionsSet.Contains(name); }
	}
}
