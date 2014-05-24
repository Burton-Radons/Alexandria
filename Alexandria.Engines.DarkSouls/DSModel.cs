//#define Marking // Enable to save the original to "D:\dump" and the marked copy to "d:\dump2".
using Glare;
using Glare.Framework;
using Glare.Graphics;
using Glare.Graphics.Rendering;
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

namespace Alexandria.Engines.DarkSouls {
	public class DSModel : ModelAsset {
		#region Internal

		internal const int HeaderSize = 4 * 32;

		internal const string Magic = "FLVER\0";

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly ListDictionary<string, DSModelBone> bones = new ListDictionary<string, DSModelBone>((bone) => bone.Name);

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly Codex<DSModelBoneUnknown> boneUnknowns = new Codex<DSModelBoneUnknown>();

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly ListDictionary<string, DSModelMaterial> materials = new ListDictionary<string, DSModelMaterial>((material) => material.Name);

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly Codex<DSModelMaterialParameter> materialParameters = new Codex<DSModelMaterialParameter>();

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly Codex<DSModelMesh> meshes = new Codex<DSModelMesh>();

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly Codex<DSModelPart> parts = new Codex<DSModelPart>();

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly Codex<DSModelVertexDeclaration> vertexDeclarations = new Codex<DSModelVertexDeclaration>();

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

		public ReadOnlyCodexDictionary<string, DSModelBone> Bones { get { return bones; } }

		public Codex<DSModelBoneUnknown> BoneUnknowns { get { return boneUnknowns; } }

		public Box3f Bounds { get; private set; }

		public ReadOnlyCodexDictionary<string, DSModelMaterial> Materials { get { return materials; } }

		public Codex<DSModelMaterialParameter> MaterialParameters { get { return materialParameters; } }

		public Codex<DSModelMesh> Meshes { get { return meshes; } }

		public Codex<DSModelPart> Parts { get { return parts; } }

		public DSModelVersion Version { get; private set; }

		public string VersionString { get { return (((int)Version) >> 16) + "." + ((int)Version & 0xFFFF); } }

		public Codex<DSModelVertexDeclaration> VertexDeclarations { get { return vertexDeclarations; } }

		#endregion Properties

		public DSModel(AssetManager manager, AssetLoader loader)
			: base(manager, loader.Name) {
			Asset context = loader.Context;
			string name = loader.Name;
			BinaryReader reader = loader.Reader;

			ModelBuilder builder = new ModelBuilder();
			FolderAsset textureArchive = null;

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
			MarkingStream markingStream = null;

			reader.WriteToFile(@"D:\dump2");
			markingStream = new MarkingStream(reader.BaseStream);
			reader = new BinaryReader(markingStream);
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
				long materialsOffset = boneUnknownsOffset + boneUnknownCount * DSModelBoneUnknown.DataSize;
				long bonesOffset = materialsOffset + materialCount * DSModelMaterial.DataSize;
				long meshesOffset = bonesOffset + boneCount * DSModelBone.DataSize;
				long partsOffset = meshesOffset + meshCount * DSModelMesh.DataSize;
				long meshVerticesOffset = partsOffset + partCount * DSModelPart.DataSize;
				long vertexDeclarationsOffset = meshVerticesOffset + meshCount * DSModelMesh.DataSizeVertexHeader;
				long materialParametersOffset = vertexDeclarationsOffset + vertexDeclarationCount * DSModelVertexDeclaration.DataSize;
				long postHeaderOffset = materialParametersOffset + materialParameterCount * DSModelMaterialParameter.DataSize;

				// BoneUnknowns
				ExpectedOffset(loader, boneUnknownsOffset, typeof(DSModelBoneUnknown).Name);
				for (int index = 0; index < boneUnknownCount; index++)
					boneUnknowns.Add(new DSModelBoneUnknown(this, index, reader));

				// Materials
				ExpectedOffset(loader, materialsOffset, typeof(DSModelMaterial).Name);
				for (int index = 0; index < materialCount; index++)
					materials.Add(new DSModelMaterial(this, index, reader));
				int expectedMaterialParameterCount = materialCount > 0 ? materials[materialCount - 1].ParameterEndIndex : 0;
				if (expectedMaterialParameterCount != materialParameterCount)
					loader.AddError(null, "Expected material parameter count {0} doesn't match actual count {1}.", expectedMaterialParameterCount, materialParameterCount);

				// Bones
				ExpectedOffset(loader, bonesOffset, typeof(DSModelBone).Name);
				for (int index = 0; index < boneCount; index++)
					bones.Add(new DSModelBone(this, index, reader));

				// Meshes
				ExpectedOffset(loader, meshesOffset, typeof(DSModelMesh).Name);
				for (int index = 0; index < meshCount; index++)
					meshes.Add(new DSModelMesh(this, index, reader));
				int expectedPartCount = meshCount > 0 ? meshes[meshCount - 1].PartEndIndex : 0;
				if (expectedPartCount != partCount)
					throw new InvalidDataException("Expected part count doesn't match actual count.");

				// Parts
				ExpectedOffset(loader, partsOffset, typeof(DSModelPart).Name);
				foreach (DSModelMesh mesh in meshes) {
					mesh.ReadParts(reader, dataOffset);
					parts.AddRange(mesh.Parts);
				}

				// Mesh vertices
				ExpectedOffset(loader, meshVerticesOffset, typeof(DSModelMesh).Name + " vertex header");
				foreach (DSModelMesh mesh in meshes)
					mesh.ReadVertexHeaders(reader, dataOffset);

				// Vertex declarations
				ExpectedOffset(loader, vertexDeclarationsOffset, typeof(DSModelVertexDeclaration).Name);
				for (int index = 0; index < vertexDeclarationCount; index++)
					vertexDeclarations.Add(new DSModelVertexDeclaration(this, index, reader));

				// Material parameters
				ExpectedOffset(loader, materialParametersOffset, typeof(DSModelMaterialParameter).Name);
				foreach (DSModelMaterial material in materials) {
					material.ReadParameters(reader, textureArchive);
					materialParameters.AddRange(material.Parameters);
				}

				ExpectedOffset(loader, postHeaderOffset, "Post-header");

#if Marking
				if (markingStream != null)
					markingStream.WriteUnusedBytes(@"D:\dump");
#endif // Marking

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

				foreach (DSModelMesh mesh in Meshes)
					mesh.BuildModel(builder);
				Content = builder.Finish();
			}
		}

		void ExpectedOffset(AssetLoader loader, long offset, string section) {
			if (loader.Position != offset) {
				loader.AddError(loader.Position, "Expected to be at offset " + offset + " for section " + section + ".");
				loader.Position = offset;
			}
		}
	}

	public abstract class DSModelObject {
		#region Internal

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal Encoding Encoding { get { return Model.Encoding; } }

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal bool IsDS1 { get { return Model.IsDS1; } }

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal bool IsDS2 { get { return Model.IsDS2; } }

		#endregion Internal

		public int Index { get; private set; }

		public DSModel Model { get; private set; }

		public UnknownList Unknowns { get; private set; }

		public DSModelObject(DSModel model, int index) {
			Model = model;
			Index = index;
			Unknowns = new UnknownList();
		}
	}

	public enum DSModelVersion {
		/// <summary>2.12, used for Dark Souls 1.</summary>
		DS1 = 0x2000C,

		/// <summary>2.16, used for Dark Souls 2.</summary>
		DS2 = 0x20010,
	}

	public class DSModelFormat : AssetFormat {
		public DSModelFormat(Engine engine)
			: base(engine, typeof(DSModel), canLoad: true) {
		}

		public override LoadMatchStrength LoadMatch(AssetLoader loader) {
			return loader.Reader.MatchMagic(DSModel.Magic) ? LoadMatchStrength.Medium : LoadMatchStrength.None;
		}

		public override Asset Load(AssetLoader loader) {
			return new DSModel(Manager, loader);
		}
	}
}
