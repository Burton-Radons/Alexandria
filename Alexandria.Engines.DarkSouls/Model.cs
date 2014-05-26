#define Marking // Enable to save the original to "D:\dump" and the marked copy to "d:\dump2".
using Glare;
using Glare.Framework;
using Glare.Graphics;
using Glare.Internal;
using Glare.Assets;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;

namespace Alexandria.Engines.DarkSouls {
	/// <summary>
	/// A ".flver" file.
	/// </summary>
	public class Model : FolderAsset {
		#region Internal

		internal const int HeaderSize = 4 * 32;

		internal const string Magic = "FLVER\0";

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly ListDictionary<string, ModelBone> bones = new ListDictionary<string, ModelBone>((bone) => bone.Name);

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly Codex<ModelBoneUnknown> boneUnknowns = new Codex<ModelBoneUnknown>();

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly ListDictionary<string, ModelMaterial> materials = new ListDictionary<string, ModelMaterial>((material) => material.Name);

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly Codex<DSModelMaterialParameter> materialParameters = new Codex<DSModelMaterialParameter>();

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly Codex<ModelMesh> meshes = new Codex<ModelMesh>();

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly Codex<ModelMeshLevel> detailLevels = new Codex<ModelMeshLevel>();

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly Codex<ModelVertexDeclaration> vertexDeclarations = new Codex<ModelVertexDeclaration>();

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal ByteOrder ByteOrder { get; private set; }

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal Encoding Encoding { get { return ByteOrder == ByteOrder.LittleEndian ? Encoding.Unicode : Encoding.BigEndianUnicode; } }
		
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal bool IsDS1 { get { return Version == DSModelVersion.DS1; } }

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal bool IsDS2 { get { return Version == DSModelVersion.DS2; } }

		#endregion Internal

		#region Properties

		public ReadOnlyCodexDictionary<string, ModelBone> Bones { get { return bones; } }

		public Codex<ModelBoneUnknown> BoneUnknowns { get { return boneUnknowns; } }

		public Box3f Bounds { get; private set; }

		public ReadOnlyCodexDictionary<string, ModelMaterial> Materials { get { return materials; } }

		public Codex<DSModelMaterialParameter> MaterialParameters { get { return materialParameters; } }

		public Codex<ModelMesh> Meshes { get { return meshes; } }

		public Codex<ModelMeshLevel> DetailLevels { get { return detailLevels; } }

		public DSModelVersion Version { get; private set; }

		public string VersionString { get { return (((int)Version) >> 16) + "." + ((int)Version & 0xFFFF); } }

		public Codex<ModelVertexDeclaration> VertexDeclarations { get { return vertexDeclarations; } }

		/// <summary>Get the buffer containing vertex and index information.</summary>
		public GraphicsBuffer Buffer { get; private set; }

		#endregion Properties

		public Model(AssetManager manager, AssetLoader loader)
			: base(manager, loader) {
			Asset context = loader.Context;
			string name = loader.Name;
			BinaryReader reader = loader.Reader;

			FolderAsset textureArchive = null;

			FolderAsset bonesFolder = new FolderAsset(this, "Bones");
			FolderAsset materialsFolder = new FolderAsset(this, "Materials");
			FolderAsset meshesFolder = new FolderAsset(this, "Meshes");
			FolderAsset vertexDeclarationsFolder = new FolderAsset(this, "Vertex declarations");

			ArrayBackedList<byte> bufferData = new ArrayBackedList<byte>();
			
			if (context != null && context.Parent is FolderAsset) {
				// @"/map/m##_##_##_##/m*.flver.dcx" for DS1; textures are in the @"/map/m##/" folder.
				if (name.StartsWith(@"map/m")) {
					string folderName = name.Substring(4, 3);
					FolderAsset maps = (FolderAsset)context.Parent.Parent;
					foreach (FolderAsset child in maps.Children) {
						if (child.Name == folderName) {
							textureArchive = child;
							break;
						}
					}
				} else
					textureArchive = (FolderAsset)context.Parent;				
			}

#if Marking
			MarkingReader markingReader = null;

			loader.Reader = reader = markingReader = new MarkingReader(reader.BaseStream);
#endif

			loader.ExpectMagic(Magic);
			char endian = (char)reader.ReadByte();
			
			switch (endian) {
				case 'L':
					ByteOrder = ByteOrder.LittleEndian;
					break;

				case 'B':
					reader = loader.Reader = new BigEndianBinaryReader(reader.BaseStream, Encoding);
					ByteOrder = ByteOrder.BigEndian;
					break;

				default:
					throw new Exception();
			}

			using (reader) {
				// Read header.
				loader.Expect((byte)0);
				Version = (DSModelVersion)reader.ReadInt32();
				if (Version != DSModelVersion.DS1 && Version != DSModelVersion.DS2)
					loader.AddError(loader.Position - 4, "Unknown model version " + VersionString + "; will try to load it anyway.");
				int dataOffset = reader.ReadInt32();
				int dataSize = reader.ReadInt32();
				if (((dataOffset + dataSize + 31) & ~31) != reader.BaseStream.Length)
					loader.AddError(loader.Position - 4, "Data size and offset aren't correct.");

				int boneUnknownCount = reader.ReadInt32();
				int materialCount = reader.ReadInt32();
				int boneCount = reader.ReadInt32();

				int meshCount = reader.ReadInt32();
				int meshCount2 = reader.ReadInt32();
				if (meshCount != meshCount2)
					loader.AddError(loader.Position - 4, "Mesh count 1 and 2 aren't the same.");

				Bounds = new Box3f(reader.ReadVector3f(), reader.ReadVector3f());
				Unknowns.ReadInt32s(reader, 1); // Possible the non-degenerate triangle count. Seems related.
				int triangleCount = reader.ReadInt32();

				loader.Expect(IsDS1 ? 272 : 0x10010100);
				loader.Expect(IsDS1 ? 0 : 0xFFFF);

				int partCount = reader.ReadInt32();
				int vertexDeclarationCount = reader.ReadInt32();
				int materialParameterCount = reader.ReadInt32();
				loader.Expect(IsDS1 ? 0 : 0x1000000);
				loader.ExpectZeroes(4, 8);

				// Calculate offsets.
				long boneUnknownsOffset = HeaderSize;
				long materialsOffset = boneUnknownsOffset + boneUnknownCount * ModelBoneUnknown.DataSize;
				long bonesOffset = materialsOffset + materialCount * ModelMaterial.DataSize;
				long meshesOffset = bonesOffset + boneCount * ModelBone.DataSize;
				long detailLevelsOffset = meshesOffset + meshCount * ModelMesh.DataSize;
				long meshVerticesOffset = detailLevelsOffset + partCount * ModelMeshLevel.DataSize;
				long vertexDeclarationsOffset = meshVerticesOffset + meshCount * ModelMesh.DataSizeVertexHeader;
				long materialParametersOffset = vertexDeclarationsOffset + vertexDeclarationCount * ModelVertexDeclaration.DataSize;
				long postHeaderOffset = materialParametersOffset + materialParameterCount * DSModelMaterialParameter.DataSize;

				// BoneUnknowns
				ExpectedOffset(loader, boneUnknownsOffset, typeof(ModelBoneUnknown).Name);
				for (int index = 0; index < boneUnknownCount; index++)
					boneUnknowns.Add(new ModelBoneUnknown(bonesFolder, index, reader));

				// Materials
				ExpectedOffset(loader, materialsOffset, typeof(ModelMaterial).Name);
				for (int index = 0; index < materialCount; index++)
					materials.Add(new ModelMaterial(materialsFolder, index, reader));
				int expectedMaterialParameterCount = materialCount > 0 ? materials[materialCount - 1].ParameterEndIndex : 0;
				if (expectedMaterialParameterCount != materialParameterCount)
					loader.AddError(null, "Expected material parameter count {0} doesn't match actual count {1}.", expectedMaterialParameterCount, materialParameterCount);

				// Bones
				ExpectedOffset(loader, bonesOffset, typeof(ModelBone).Name);
				for (int index = 0; index < boneCount; index++)
					bones.Add(new ModelBone(bonesFolder, index, reader));

				// Meshes
				ExpectedOffset(loader, meshesOffset, typeof(ModelMesh).Name);
				for (int index = 0; index < meshCount; index++)
					meshes.Add(new ModelMesh(meshesFolder, index, reader));
				int expectedPartCount = meshCount > 0 ? meshes[meshCount - 1].PartEndIndex : 0;
				if (expectedPartCount != partCount)
					throw new InvalidDataException("Expected part count doesn't match actual count.");

				// Detail levels
				ExpectedOffset(loader, detailLevelsOffset, typeof(ModelMeshLevel).Name);
				foreach (ModelMesh mesh in meshes) {
					mesh.ReadDetailLevels(reader, dataOffset, bufferData);
					detailLevels.AddRange(mesh.DetailLevels);
				}

				// Mesh vertices
				ExpectedOffset(loader, meshVerticesOffset, typeof(ModelMesh).Name + " vertex header");
				foreach (ModelMesh mesh in meshes)
					mesh.ReadVertexHeaders(reader, dataOffset, bufferData);

				// Vertex declarations
				ExpectedOffset(loader, vertexDeclarationsOffset, typeof(ModelVertexDeclaration).Name);
				for (int index = 0; index < vertexDeclarationCount; index++)
					vertexDeclarations.Add(new ModelVertexDeclaration(vertexDeclarationsFolder, index, reader));

				// Material parameters
				ExpectedOffset(loader, materialParametersOffset, typeof(DSModelMaterialParameter).Name);
				foreach (ModelMaterial material in materials) {
					material.ReadParameters(reader, textureArchive);
					materialParameters.AddRange(material.Parameters);
				}

				ExpectedOffset(loader, postHeaderOffset, "Post-header");

#if Marking
				if (markingReader != null) {
					markingReader.Report(loader);
				}
#endif // Marking

#if SkippedChecks
				/*int vertexDataSize = 0, indexCount = 0, indexSize = 0, vertexCount = 0, expectedTriangleCount = 0, nondegenerateTriangleCount = 0;
				foreach (var mesh in Meshes) {
					vertexDataSize += mesh.VertexCount * mesh.VertexSize;
					vertexCount += mesh.VertexCount;
					foreach (var part in mesh.Parts) {
						indexCount += part.Indices.Length;
						indexSize += part.Indices.Length * 2;
						expectedTriangleCount += part.Indices.Length - 2;
						for (int index = 0; index < part.Indices.Length - 2; index++) {
							if (part.Indices[index] != part.Indices[index + 1] && part.Indices[index + 1] != part.Indices[index + 2] && part.Indices[index] != part.Indices[index + 2])
								nondegenerateTriangleCount++;
						}
					}
				}
				if (Math.Abs(expectedTriangleCount - triangleCount) > partCount)
					throw new InvalidDataException("Expected triangle count doesn't match the read value.");*/
#endif
			}

			Buffer = new GraphicsBuffer(bufferData.Count == 0 ? 1 : bufferData.Count);
			Buffer.Write(0, bufferData.Array, 0, bufferData.Count);

		}

		void ExpectedOffset(AssetLoader loader, long offset, string section) {
			if (loader.Position != offset) {
				loader.AddError(loader.Position, "Expected to be at offset " + offset + " for section " + section + ".");
				loader.Position = offset;
			}
		}
	}

	/// <summary>An object contained within a <see cref="Model"/>.</summary>
	public abstract class ModelObject : FolderAsset {
		#region Internal

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal Encoding Encoding { get { return Model.Encoding; } }

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal bool IsDS1 { get { return Model.IsDS1; } }

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal bool IsDS2 { get { return Model.IsDS2; } }

		#endregion Internal

		public long DataOffset { get; private set; }

		public override string DisplayName { get { return ToString(); } }

		public int Index { get; private set; }

		public Model Model {
			get {
				Model model;
				for (Asset parent = Parent; (model = parent as Model) == null; parent = parent.Parent) ;
				return model;
			}
		}

		public ModelObject(FolderAsset parent, int index, BinaryReader reader)
			: base(parent, "") {
				DataOffset = reader.BaseStream.Position;
			Name = GetType().Name + " " + index;
			Index = index;
		}
	}

	public enum DSModelVersion {
		/// <summary>2.12, used for Dark Souls 1.</summary>
		DS1 = 0x2000C,

		/// <summary>2.16, used for Dark Souls 2.</summary>
		DS2 = 0x20010,
	}

	public class ModelFormat : AssetFormat {
		public ModelFormat(Engine engine)
			: base(engine, typeof(Model), canLoad: true, extension: ".flver") {
		}

		public override LoadMatchStrength LoadMatch(AssetLoader loader) {
			return loader.Reader.MatchMagic(Model.Magic) ? LoadMatchStrength.Medium : LoadMatchStrength.None;
		}

		public override Asset Load(AssetLoader loader) {
			return new Model(Manager, loader);
		}
	}
}
