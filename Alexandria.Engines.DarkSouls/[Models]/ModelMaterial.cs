using Glare;
using Glare.Assets;
using Glare.Assets.Formats;
using Glare.Framework;
using Glare.Graphics;
using Glare.Graphics.Loaders;
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
	/// <summary>
	/// A material for a <see cref="Model"/>.
	/// </summary>
	public class ModelMaterial : ModelAsset {
		#region Internal

		internal const int DataSize = 4 * 8;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly ListDictionary<string, ModelMaterialParameter> ParametersList = new ListDictionary<string, ModelMaterialParameter>((parameter) => parameter.Name);

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal readonly int ParameterCount;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal int ParameterEndIndex { get { return ParameterStartIndex + ParameterCount; } }

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal int ParameterStartIndex { get { return Index > 0 ? Model.Materials[Index - 1].ParameterEndIndex : 0; } }

		#endregion Internal

		#region Properties

		/// <summary>Get the material's parameters.</summary>
		public ReadOnlyCodexDictionary<string, ModelMaterialParameter> Parameters { get { return ParametersList; } }

		/// <summary>Get the name of the shader to use.</summary>
		public string ShaderName { get; private set; }

		#endregion Properties

		internal ModelMaterial(FolderAsset parent, int index, AssetLoader loader)
			: base(parent, index, loader) {
			var reader = loader.Reader;

			Name = reader.ReadStringzAtUInt32(Encoding);
			ShaderName = reader.ReadStringzAtUInt32(Encoding);
			ParameterCount = reader.ReadInt32();
			int parameterStartIndex = reader.ReadInt32();
			Unknowns.ReadInt32s(reader, 1);
			if (IsDS2)
				Unknowns.ReadInt32s(reader, 1);
			reader.RequireZeroes(4 * (IsDS1 ? 3 : 2));

			if (parameterStartIndex != ParameterStartIndex)
				throw new InvalidDataException("Parameter start index is not valid.");
		}

		/// <summary>
		/// Bind the material to the program.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="program"></param>
		public void Bind(ModelDrawContext context, ModelProgram program) {
			foreach (ModelMaterialParameter parameter in Parameters)
				parameter.Bind(context, program);
		}

		static Texture2D GetTexture(IResourceSource<Texture2D> source) { return source != null ? source.GetResourceValue() : null; }

		internal void ReadParameters(AssetLoader loader, FolderAsset textureFolder) {
			var reader = loader.Reader;
			int startIndex = ParameterStartIndex;

			for (int index = 0; index < ParameterCount; index++)
				ParametersList.Add(new ModelMaterialParameter(this, index, startIndex + index, loader));

			if (textureFolder != null) {
				foreach (ModelMaterialParameter parameter in Parameters) {
					if (string.IsNullOrEmpty(parameter.Value))
						continue;
					string name = Path.GetFileNameWithoutExtension(parameter.Value);
					DataAsset resource = null;

					foreach (DataAsset archiveAsset in textureFolder.Children) {
						var extension = Path.GetExtension(archiveAsset.Name);

						if (extension != ".tpf" && extension != ".tpfbhd")
							continue;

						FolderAsset archive = archiveAsset.Contents as FolderAsset;

						if (archive == null)
							continue;

						foreach (DataAsset compare in archive.Children)
							if (compare.Name.StartsWith(name) && (compare.Name.Length == name.Length || compare.Name[name.Length] == '.')) {
								if (compare.Name.EndsWith(".tpf.dcx")) {
									FolderAsset textureArchive = (FolderAsset)compare.Contents;
									resource = (DataAsset)textureArchive.Children[0];
								} else
									resource = compare;

								break;
							}

						if (resource != null)
							break;
					}

					parameter.TextureSource = MakeResourceSource(resource);

					/*if (parameter.Name == "g_Diffuse")
						ModelMaterial.DiffuseMap = MakeResourceSource(resource);
					else if (parameter.Name == "g_Specular")
						ModelMaterial.SpecularMap = MakeResourceSource(resource);
					else if (parameter.Name == "g_Bumpmap")
						ModelMaterial.NormalMap = MakeResourceSource(resource);*/
				}
			}
		}

		IResourceSource<Texture2D> MakeResourceSource(DataAsset resource) {
			if (resource == null)
				return null;
			return new AssetResourceSource<Texture2D>(resource);
		}

		class AssetResourceSource<T> : IResourceSource<T> {
			public T GetResourceValue() {
				Asset contents = AssetResource.Contents;
				return (T)contents.GlareObject;
			}

			object IResourceSource.GetResourceValue() { return GetResourceValue(); }

			readonly DataAsset AssetResource;

			public AssetResourceSource(DataAsset resource) {
				AssetResource = resource;
			}
		}

		/*public void SaveTextures(string path) {
			SaveTexture(path, ModelMaterial.DiffuseMap);
			SaveTexture(path, ModelMaterial.NormalMap);
			SaveTexture(path, ModelMaterial.SpecularMap);
		}

		void SaveTexture(string path, IResourceSource<Texture2D> source) {
			Texture2D texture;

			if (source == null || (texture = source.GetResourceValue()) == null)
				return;
			string texturePath = TexturePath(source, path);
			if(File.Exists(texturePath))
				return;
			using (Stream stream = File.Open(texturePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
			using (BinaryWriter writer = new BinaryWriter(stream))
				Parent.Manager.GetFormat<DdsFormat>().Save(texture, writer);
		}

		static string TexturePath(IResourceSource<Texture2D> source, string path) {
			return Path.Combine(path, TexturePath(source));
		}

		static string TexturePath(IResourceSource<Texture2D> source) {
			Texture2D texture;

			if (source == null || (texture = source.GetResourceValue()) == null)
				return null;
			return Path.GetFileNameWithoutExtension(texture.Name) + ".dds";
		}*/

		/// <summary>Create a string representation of the object.</summary>
		/// <returns></returns>
		public override string ToString() {
			return string.Format("{0}('{1}', '{2}'{3})", GetType().Name, Name, ShaderName, Unknowns.ToCommaPrefixedList());
		}
	}
}
