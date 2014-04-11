using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics.Rendering
{
	/// <summary>
	/// A material for a <see cref="ModelMesh"/>. This defines visual characteristics of the mesh.
	/// </summary>
	public class ModelMaterial
	{
		/// <summary>
		/// Get or set the ambient color of the material, where X is red, Y is green, Z is blue, and 1.0 for each is "brightest". The default is (0, 0, 0).
		/// </summary>
		public Vector3d Ambient { get; set; }

		/// <summary>Get or set the diffuse color of the material, where X is red, Y is green, Z is blue, and 1.0 for each is "brightest". The default is (1, 1, 1).</summary>
		public Vector3d DiffuseColor { get; set; }

		/// <summary>Get or set the diffuse map, or <c>null</c> if there is none.</summary>
		public IResourceSource<Texture2D> DiffuseMap { get; set; }

		public Texture2D DiffuseTexture {
			get { return DiffuseMap != null ? DiffuseMap.GetResourceValue() : null; }
			set { DiffuseMap = value; }
		}

		/// <summary>Get or set the shininess of the material, which is the exponent used for specular reflections. Higher values produce a sharper reflection. The default is 10.</summary>
		public double Glossiness { get; set; }

		/// <summary>Get or set the normal map, or <c>null</c> if there is none.</summary>
		public IResourceSource<Texture2D> NormalMap { get; set; }

		/// <summary>Get or set the opacity of the material from 0.0 for transparent to 1.0 for opaque. The default is 1.</summary>
		public double Opacity { get; set; }

		/// <summary>Get or set the specular color of the material, where X is red, Y is green, Z is blue, and 1.0 for each is "brightest". The default is (1, 1, 1).</summary>
		public Vector3d Specular { get; set; }

		/// <summary>Get or set the specular map, or <c>null</c> if there is none.</summary>
		public IResourceSource<Texture2D> SpecularMap { get; set; }

		/// <summary>Get or set a multiplier of the specular level. Values above 1 amplify the specular reflections, and below 1 darken them. The default is 1.</summary>
		public double SpecularLevel { get; set; }

		public ModelMaterial()
		{
			DiffuseColor = Vector3d.One;
			Opacity = 1;
			Glossiness = 10;
			Specular = Vector3d.One;
			SpecularLevel = 1;
		}
	}

	public interface IModelMaterialBinder
	{
		/// <summary>
		/// Setup the program to use this <see cref="ModelMaterial"/>.
		/// </summary>
		/// <param name="material">The <see cref="ModelMaterial"/> to use.</param>
		void BindMaterial(ModelMaterial material);
	}
}
