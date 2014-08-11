using Glare;
using Glare.Assets;
using Glare.Graphics;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Alexandria.Engines.DarkSouls {
	/// <summary>
	/// A parameter to a <see cref="ModelMaterial"/>. So far this seems to be restricted to textures. <see cref="Asset.Name"/> holds the name of the parameter.
	/// </summary>
	public class ModelMaterialParameter : ModelAsset {
		#region Internal

		internal const int DataSize = 4 * 8;

		#endregion Internal

		#region Properties

		/// <summary>Get the name of a diffuse map parameter.</summary>
		public const string DiffuseMapName = "g_Diffuse";

		/// <summary>Get the name of a specular map parameter.</summary>
		public const string SpecularMapName = "g_Specular";

		/// <summary>Get the name of a normal map parameter.</summary>
		public const string NormalMapName = "g_Bumpmap";

		/// <summary>Get the name of a light map parameter.</summary>
		public const string LightMapName = "g_Lightmap";

		/// <summary>Get the name of a detail normal map parameter.</summary>
		public const string DetailNormalMapName = "g_DetailBumpmap";

		/// <summary>Get the <see cref="ModelMaterial"/> that uses this <see cref="Material"/>.</summary>
		public ModelMaterial Material { get; private set; }

		/// <summary>Get the zero-based index of this parameter in the <see cref="Material"/>.</summary>
		public int MaterialIndex { get; private set; }

		/// <summary>Get the filename of the texture or "" for none.</summary>
		public string Value { get; private set; }

		/// <summary>Get the texture source.</summary>
		public IResourceSource<Texture2D> TextureSource { get; internal set; }

		#endregion Properties

		internal ModelMaterialParameter(ModelMaterial material, int materialIndex, int index, AssetLoader loader)
			: base(material, index, loader) {
			var reader = loader.Reader;

			Material = material;
			MaterialIndex = materialIndex;

			Value = reader.ReadStringzAtUInt32(Encoding);
			Name = reader.ReadStringzAtUInt32(Encoding);
			Unknowns.ReadSingles(reader, 2);
			Unknowns.ReadInt16s(reader, 1); // Always 257?
			reader.RequireZeroes(2);

			reader.RequireZeroes(12);
		}

		/// <summary>
		/// Bind the material parameter to the program.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="program"></param>
		public void Bind(ModelDrawContext context, ModelProgram program) {
			Texture2D texture = TextureSource != null ? TextureSource.GetResourceValue() : null;

			switch (Name) {
				case DiffuseMapName:
					program.DiffuseMap = texture;
					break;

				case SpecularMapName:
				case NormalMapName:
				case LightMapName:
				case DetailNormalMapName:
					break;

				default:
					break;
			}
		}

		/// <summary>Convert to a readable string format.</summary>
		/// <returns></returns>
		public override string ToString() {
			return string.Format("{0}({1} = \"{2}\", {3}, {4})", GetType().Name, Name, Value, Unknowns[0].JoinedValues, Unknowns[1].JoinedValues);
		}
	}
}
