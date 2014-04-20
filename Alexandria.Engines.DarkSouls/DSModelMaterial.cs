using Glare;
using Glare.Assets;
using Glare.Assets.Formats;
using Glare.Framework;
using Glare.Graphics;
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

		public ModelMaterial ModelMaterial { get; private set; }

		public string Name { get; private set; }

		public string ShaderName { get; private set; }

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
			Name = reader.ReadStringzAtUInt32(Encoding);
			ShaderName = reader.ReadStringzAtUInt32(Encoding);
			ParameterCount = reader.ReadInt32();
			int parameterStartIndex = reader.ReadInt32();
			Unknowns.ReadInt32s(reader, 1);
			if(IsDS2)
				Unknowns.ReadInt32s(reader, 1);
			reader.RequireZeroes(4 * (IsDS1 ? 3 : 2));

			if (parameterStartIndex != ParameterStartIndex)
				throw new InvalidDataException("Parameter start index is not valid.");
		}

		static Texture2D GetTexture(IResourceSource<Texture2D> source) { return source != null ? source.GetResourceValue() : null; }

		internal void ReadParameters(BinaryReader reader, FolderAsset textureFolder) {
			int startIndex = ParameterStartIndex;

			for (int index = 0; index < ParameterCount; index++)
				ParametersList.Add(new DSModelMaterialParameter(this, index, startIndex + index, reader));

			ModelMaterial = new ModelMaterial() {
				AmbientColor = Vector3d.One,
			};

			if (textureFolder != null) {
				foreach (DSModelMaterialParameter parameter in Parameters) {
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

					if (parameter.Name == "g_Diffuse")
						ModelMaterial.DiffuseMap = MakeResourceSource(resource);
					else if (parameter.Name == "g_Specular")
						ModelMaterial.SpecularMap = MakeResourceSource(resource);
					else if (parameter.Name == "g_Bumpmap")
						ModelMaterial.NormalMap = MakeResourceSource(resource);
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

		public void SaveTextures(string path) {
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
				Model.Manager.GetFormat<DdsFormat>().Save(texture, writer);
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
