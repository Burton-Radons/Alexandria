using Glare;
using Glare.Graphics;
using Glare.Graphics.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.DarkSouls {
	/// <summary>
	/// A builtin <see cref="Program"/> that offers basic rendering capabilities like lighting and texture support.
	/// </summary>
	public class ModelProgram : Program {
		const string ResourceName = "Alexandria.Engines.DarkSouls._Models_.ModelProgram.glsl";

		readonly VertexShader vertexShader;
		readonly FragmentShader fragmentShader;
		static readonly ShaderBuilder shaderBuilder;

		ProgramAttribute boneIndices, boneWeights, normal, position, texel;
		ProgramUniform ambientLight, diffuseColor, diffuseMap, displayMode, projection, view, world;

		Vector3d ambientLightValue = new Vector3d(0.5);
		Vector4d diffuseColorValue = Vector4d.One;
		Texture2D diffuseMapValue;
		BasicProgramDisplayMode displayModeValue;
		Matrix4d worldValue = Matrix4d.Identity, viewValue = Matrix4d.Identity, projectionValue = Matrix4d.Identity;

		/// <summary>Get or set the ambient lighting.</summary>
		public Vector3d AmbientLight { get { return ambientLightValue; } set { ambientLightValue = value; if (ambientLight != null) ambientLight.Set(ref value); } }

		/// <summary>Get the 4-vector blend indices attribute.</summary>
		public ProgramAttribute BoneIndices { get { return boneIndices; } }

		/// <summary>Get the 4-vector blend weights attribute.</summary>
		public ProgramAttribute BoneWeights { get { return boneWeights; } }

		/// <summary>Get or set the diffuse colour.</summary>
		public Vector4d DiffuseColor { get { return diffuseColorValue; } set { diffuseColorValue = value; if (diffuseColor != null) diffuseColor.Set(ref value); } }

		/// <summary>Get or set the diffuse colour map; if <c>null</c>, it will default to a white texture.</summary>
		public Texture2D DiffuseMap { get { return diffuseMapValue; } set { diffuseMapValue = value; if (diffuseMap != null) diffuseMap.Set(value ?? Device.WhiteTexture); } }

		/// <summary>Get or set the display mode.</summary>
		public BasicProgramDisplayMode DisplayMode { get { return displayModeValue; } set { displayModeValue = value; if (displayMode != null) displayMode.Set((int)value); } }

		/// <summary>Get the 3-vector normal attribute.</summary>
		public ProgramAttribute Normal { get { return normal; } }

		/// <summary>Get the 3-vector position attribute.</summary>
		public ProgramAttribute Position { get { return position; } }

		/// <summary>Get or set the projection matrix.</summary>
		public Matrix4d Projection { get { return projectionValue; } set { projectionValue = value; if (projection != null) projection.Set(ref value); } }

		/// <summary>Get the 2-to-4 vector texture coordinate attribute.</summary>
		public ProgramAttribute Texel { get { return texel; } }

		/// <summary>Get or set the view matrix.</summary>
		public Matrix4d View { get { return viewValue; } set { viewValue = value; if (view != null) view.Set(ref value); } }

		/// <summary>Get or set the world matrix.</summary>
		public Matrix4d World { get { return worldValue; } set { worldValue = value; if (world != null) world.Set(ref value); } }

		static readonly Dictionary<string, string> sections = new Dictionary<string, string>();

		/// <summary>Initialise the program.</summary>
		public ModelProgram() {
			vertexShader = shaderBuilder.VertexShader("Common", "Uniforms", "Vertex");
			fragmentShader = shaderBuilder.FragmentShader("Common", "Uniforms", "Fragment");

			Shaders.AddRange(vertexShader, fragmentShader);
			MustLink();
		}

		static ModelProgram() {
			shaderBuilder = ShaderBuilder.CreateFromAssemblyResource(ResourceName);
		}

		void DoLink() {
			if (view == null)
				MustLink();
		}

		/// <summary>Link the program, returning the result.</summary>
		/// <returns></returns>
		public override bool Link() {
			bool result = base.Link();
			Unlink();

			if (result) {
				boneIndices = Attributes["BoneIndices"];
				boneWeights = Attributes["BoneWeights"];
				normal = Attributes["Normal"];
				position = Attributes["Position"];
				texel = Attributes["Texel"];

				(ambientLight = Uniforms["AmbientLight"]).Set(ref ambientLightValue);
				(diffuseColor = Uniforms["DiffuseColor"]).Set(ref diffuseColorValue);
				(diffuseMap = Uniforms["DiffuseMap"]).Set(diffuseMapValue ?? Device.WhiteTexture);
				(displayMode = Uniforms["DisplayMode"]).Set((int)displayModeValue);
				(projection = Uniforms["Projection"]).Set(ref projectionValue);
				(view = Uniforms["View"]).Set(ref viewValue);
				(world = Uniforms["World"]).Set(ref worldValue);
			}

			return result;
		}

		void Unlink() {
			ambientLight = diffuseColor = diffuseMap = projection = view = world = null;
			boneIndices = boneWeights = normal = position = texel = null;
		}
	}
}
