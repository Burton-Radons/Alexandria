using Alexandria.Resources;
using Glare;
using Glare.Graphics;
using Glare.Graphics.Collada;
using Glare.Graphics.Loaders;
using Glare.Graphics.Rendering;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Alexandria.Engines.DarkSouls {
	public class DSModelMaterial : DSModelObject {
		#region Internal

		internal const int DataSize = 4 * 8;

		internal Effect Effect { get; private set; }

		internal Material Material { get; private set; }

		PhongShader Shader;

		public ModelMaterial ModelMaterial { get; private set; }

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly ListDictionary<string, DSModelMaterialParameter> ParametersList = new ListDictionary<string, DSModelMaterialParameter>((parameter) => parameter.Name);

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal readonly int ParameterCount;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal int ParameterEndIndex { get { return ParameterStartIndex + ParameterCount; } }

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal int ParameterStartIndex { get { return Index > 0 ? Model.Materials[Index - 1].ParameterEndIndex : 0; } }

		#endregion Internal

		#region Properties

		/// <summary>Get the material's parameters.</summary>
		public ReadOnlyListDictionary<string, DSModelMaterialParameter> Parameters { get { return ParametersList; } }

		#endregion Properties

		public DSModelMaterial(DSModel model, int index, BinaryReader reader)
			: base(model, index) {
			string effectId = "effect" + Index;
			string materialId = "material" + Index;
			string techniqueScopedId = "technique" + Index;

			InstanceEffect instanceEffect;
			Technique effectExtraTechnique;

			Effect = new Effect(effectId) {
				Profiles = new ProfileCollection(
					new ProfileCommon(
						new ProfileCommonTechnique(techniqueScopedId,
							Shader = new PhongShader() {
								Emission = Vector4d.Zero,
								Ambient = Vector4d.One,
								Diffuse = Vector4d.One,
								Specular = Vector4d.One,
								Shininess = 20.0,
								Reflective = Vector4d.One,
								Reflectivity = 0.5,
								Transparent = Vector4d.One,
								Transparency = 1.0,

							}))), // PhongShader
						// ProfileCommonTechnique
					// ProfileCommon
				// Profiles

				Extras = new ExtraCollection(new Extra(effectExtraTechnique = new Technique("Dark Souls Material"))),
			}; // Effect

			Material = new Material() {
				Id = materialId,
				InstanceEffect = instanceEffect = new InstanceEffect("#" + effectId)
			}; // Material

			Effect.Name = reader.ReadStringzAtUInt32(Encoding);
			effectExtraTechnique.Parameters.Add(new Parameter("shader-name",
				reader.ReadStringzAtUInt32(Encoding)));
			ParameterCount = reader.ReadInt32();
			int parameterStartIndex = reader.ReadInt32();
			effectExtraTechnique.Parameters.Add(new Parameter("unknown",
				reader.ReadInt32()));
			if(IsDS2)
				effectExtraTechnique.Parameters.Add(new Parameter("unknown",
					reader.ReadInt32()));
			reader.RequireZeroes(4 * (IsDS1 ? 3 : 2));

			if (parameterStartIndex != ParameterStartIndex)
				throw new InvalidDataException("Parameter start index is not valid.");
		}

		internal void BuildCollada() {
			Model.Collada.Effects.Add(Effect);
			Model.Collada.Materials.Add(Material);

			BuildColladaTexture(Shader.Diffuse, ModelMaterial.DiffuseMap);
			BuildColladaTexture(Shader.Specular, ModelMaterial.SpecularMap);
		}

		void BuildColladaTexture(CommonColorOrTextureType currentValue, IResourceSource<Texture2D> source) {
			Texture2D texture;

			if (source == null || (texture = source.GetResourceValue()) == null)
				return;

			string textureName = Path.GetFileNameWithoutExtension(texture.Name);
			string parameterName = "material" + Index + textureName;
			string imageId = "image" + Index + textureName;

			Image image = new Image() {
				Id = imageId,
				InitializeFrom = new InitializeFrom(textureName + ".dds"),
			};
			Model.Collada.Images.Add(image);

			/*ColladaEffect.Parameters.Add(
				new NewParameter(parameterName,
					new Sampler2D() {
						InstanceImage = new InstanceImage(textureName + ".dds"),
					}
				) // NewParameter
			); */// Parameters

			currentValue.Texture = new CommonTexture(imageId, "textureCoordinate");
		}

		static Texture2D GetTexture(IResourceSource<Texture2D> source) { return source != null ? source.GetResourceValue() : null; }

		internal void ReadParameters(BinaryReader reader, Folder textureFolder) {
			int startIndex = ParameterStartIndex;

			for (int index = 0; index < ParameterCount; index++)
				ParametersList.Add(new DSModelMaterialParameter(this, index, startIndex + index, reader));

			ModelMaterial = new ModelMaterial() {
				Ambient = Vector3d.One,
			};

			if (textureFolder != null) {
				foreach (DSModelMaterialParameter parameter in Parameters) {
					if (string.IsNullOrEmpty(parameter.Value))
						continue;
					string name = Path.GetFileNameWithoutExtension(parameter.Value);
					Resources.Asset resource = null;

					foreach (Resources.Asset archiveAsset in textureFolder.Children) {
						var extension = Path.GetExtension(archiveAsset.Name);

						if (extension != ".tpf" && extension != ".tpfbhd")
							continue;

						Folder archive = archiveAsset.Contents as Folder;

						if (archive == null)
							continue;

						foreach (Resources.Asset compare in archive.Children)
							if (compare.Name.StartsWith(name) && (compare.Name.Length == name.Length || compare.Name[name.Length] == '.')) {
								if (compare.Name.EndsWith(".tpf.dcx")) {
									Folder textureArchive = (Folder)compare.Contents;
									resource = (Resources.Asset)textureArchive.Children[0];
								} else
									resource = compare;

								break;
							}

						if (resource != null)
							break;
					}

					if (parameter.Name == "g_Diffuse")
						ModelMaterial.DiffuseMap = MakeResourceSource(resource);
					else if (parameter.Name == "g_Specular")
						ModelMaterial.SpecularMap = MakeResourceSource(resource);
					else if (parameter.Name == "g_Bumpmap")
						ModelMaterial.NormalMap = MakeResourceSource(resource);
				}
			}
		}

		IResourceSource<Texture2D> MakeResourceSource(Resources.Asset resource) {
			if (resource == null)
				return null;
			return new AssetResourceSource<Texture2D>(resource);
		}

		class AssetResourceSource<T> : IResourceSource<T> {
			public T GetResourceValue() {
				Resource contents = AssetResource.Contents;
				return (T)contents.GlareObject;
			}

			object IResourceSource.GetResourceValue() { return GetResourceValue(); }

			readonly Resources.Asset AssetResource;

			public AssetResourceSource(Resources.Asset resource) {
				AssetResource = resource;
			}
		}

		public void SaveTextures(string path) {
			SaveTexture(path, ModelMaterial.DiffuseMap);
			SaveTexture(path, ModelMaterial.NormalMap);
			SaveTexture(path, ModelMaterial.SpecularMap);
		}

		static void SaveTexture(string path, IResourceSource<Texture2D> source) {
			Texture2D texture;

			if (source == null || (texture = source.GetResourceValue()) == null)
				return;
			string texturePath = TexturePath(source, path);
			if(File.Exists(texturePath))
				return;
			DDSSaver.Save(texturePath, texture);
		}

		static string TexturePath(IResourceSource<Texture2D> source, string path) {
			return Path.Combine(path, TexturePath(source));
		}

		static string TexturePath(IResourceSource<Texture2D> source) {
			Texture2D texture;

			if (source == null || (texture = source.GetResourceValue()) == null)
				return null;
			return Path.GetFileNameWithoutExtension(texture.Name) + ".dds";
		}

		public override string ToString() {
			return string.Format("{0}({3})", GetType().Name, Unknowns.ToCommaSeparatedList());
		}
	}

	public class DSModelMaterialParameter : DSModelObject {
		#region Internal

		internal const int DataSize = 4 * 8;

		#endregion Internal

		#region Properties

		/// <summary>Get the <see cref="DSModelMaterial"/> that uses this <see cref="Material"/>.</summary>
		public DSModelMaterial Material { get; private set; }

		/// <summary>Get the zero-based index of this parameter in the <see cref="Material"/>.</summary>
		public int MaterialIndex { get; private set; }

		/// <summary>Get the name of this parameter, such as "g_Diffuse".</summary>
		public string Name { get; private set; }

		/// <summary>Get the filename of the texture or "" for none.</summary>
		public string Value { get; private set; }

		#endregion Properties

		internal DSModelMaterialParameter(DSModelMaterial material, int materialIndex, int index, BinaryReader reader)
			: base(material.Model, index) {
			Material = material;
			MaterialIndex = materialIndex;

			Value = reader.ReadStringzAtUInt32(Encoding);
			Name = reader.ReadStringzAtUInt32(Encoding);
			Unknowns.ReadSingles(reader, 2);
			Unknowns.ReadInt16s(reader, 1); // Always 257?
			reader.RequireZeroes(2);

			reader.RequireZeroes(12);
		}

		public override string ToString() {
			return string.Format("{0}({1} = \"{2}\", {3}, {4})", GetType().Name, Name, Value, Unknowns[0].JoinedValues, Unknowns[1].JoinedValues);
		}
	}
}
