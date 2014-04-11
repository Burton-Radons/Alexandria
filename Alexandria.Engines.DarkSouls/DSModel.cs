//#define Marking // Enable to save the original to "D:\dump" and the marked copy to "d:\dump2".
using Alexandria.Resources;
using Glare;
using Glare.Graphics;
using Glare.Graphics.Collada;
using Glare.Graphics.Rendering;
using Glare.Internal;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace Alexandria.Engines.DarkSouls {
	public class DSModel : Resources.Model {
		#region Internal

		internal const int HeaderSize = 4 * 32;

		internal const string Magic = "FLVER\0";

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly ListDictionary<string, DSModelBone> bones = new ListDictionary<string, DSModelBone>((bone) => bone.Name);

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly ArrayBackedList<DSModelBoneUnknown> boneUnknowns = new ArrayBackedList<DSModelBoneUnknown>();

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly ListDictionary<string, DSModelMaterial> materials = new ListDictionary<string, DSModelMaterial>((material) => material.Effect.Name);

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly ArrayBackedList<DSModelMaterialParameter> materialParameters = new ArrayBackedList<DSModelMaterialParameter>();

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly ArrayBackedList<DSModelMesh> meshes = new ArrayBackedList<DSModelMesh>();

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly ArrayBackedList<DSModelPart> parts = new ArrayBackedList<DSModelPart>();

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly ArrayBackedList<DSModelVertexDeclaration> vertexDeclarations = new ArrayBackedList<DSModelVertexDeclaration>();

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal ByteOrder ByteOrder { get; private set; }

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal Encoding Encoding { get { return ByteOrder == ByteOrder.LittleEndian ? Encoding.Unicode : Encoding.BigEndianUnicode; } }
		
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal bool IsDS1 { get { return Version == DSModelVersion.DS1; } }

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal bool IsDS2 { get { return Version == DSModelVersion.DS2; } }

		internal Collada Collada { get; private set; }

		#endregion Internal

		#region Properties

		public ReadOnlyListDictionary<string, DSModelBone> Bones { get { return bones; } }

		public ReadOnlyList<DSModelBoneUnknown> BoneUnknowns { get { return boneUnknowns; } }

		public Box3f Bounds { get; private set; }

		public ReadOnlyListDictionary<string, DSModelMaterial> Materials { get { return materials; } }

		public ReadOnlyList<DSModelMaterialParameter> MaterialParameters { get { return materialParameters; } }

		public ReadOnlyList<DSModelMesh> Meshes { get { return meshes; } }

		public ReadOnlyList<DSModelPart> Parts { get { return parts; } }

		public DSModelVersion Version { get; private set; }

		public string VersionString { get { return (((int)Version) >> 16) + "." + ((int)Version & 0xFFFF); } }

		public ReadOnlyList<DSModelVertexDeclaration> VertexDeclarations { get { return vertexDeclarations; } }

		#endregion Properties

		public DSModel(Manager manager, BinaryReader reader, string name, LoaderFileOpener opener, Resource context)
			: base(manager, name) {
			ModelBuilder builder = new ModelBuilder();
			Folder textureArchive = null;

			if (context != null && context.Parent is Folder) {
				// @"/map/m##_##_##_##/m*.flver.dcx" for DS1; textures are in the @"/map/m##/" folder.
				if (name.StartsWith(@"/map/m")) {
					string folderName = name.Substring(5, 3);
					Folder maps = (Folder)context.Parent.Parent;
					foreach (Folder child in maps.Children) {
						if (child.Name == folderName) {
							textureArchive = child;
							break;
						}
					}
				} else
					textureArchive = (Folder)context.Parent;
				
			}

#if Marking
			MarkingStream markingStream = null;

			reader.WriteToFile(@"D:\dump2");
			markingStream = new MarkingStream(reader.BaseStream);
			reader = new BinaryReader(markingStream);
#endif

			reader.RequireMagic(Magic);
			char endian = (char)reader.ReadByte();
			
			switch (endian) {
				case 'L':
					ByteOrder = ByteOrder.LittleEndian;
					break;

				case 'B':
					reader = new BigEndianBinaryReader(reader.BaseStream, Encoding, true);
					ByteOrder = ByteOrder.BigEndian;
					break;

				default:
					throw new Exception();
			}

			using (reader) {
				// Read header.
				reader.RequireZeroes(1);
				Version = (DSModelVersion)reader.ReadInt32();
				if (Version != DSModelVersion.DS1 && Version != DSModelVersion.DS2)
					throw new InvalidDataException("Unknown model version " + VersionString + ".");
				int dataOffset = reader.ReadInt32();
				int dataSize = reader.ReadInt32();
				if (((dataOffset + dataSize + 31) & ~31) != reader.BaseStream.Length)
					throw new InvalidDataException("Data size and offset aren't correct.");

				int boneUnknownCount = reader.ReadInt32();
				int materialCount = reader.ReadInt32();
				int boneCount = reader.ReadInt32();

				int meshCount = reader.ReadInt32();
				int meshCount2 = reader.ReadInt32();
				if (meshCount != meshCount2)
					throw new InvalidDataException();

				Bounds = new Box3f(reader.ReadVector3f(), reader.ReadVector3f());
				Unknowns.ReadInt32s(reader, 1); // Possible the non-degenerate triangle count. Seems related.
				int triangleCount = reader.ReadInt32();

				reader.Require(IsDS1 ? 272 : 0x10010100);
				reader.Require(IsDS1 ? 0 : 0xFFFF);

				int partCount = reader.ReadInt32();
				int vertexDeclarationCount = reader.ReadInt32();
				int materialParameterCount = reader.ReadInt32();
				reader.Require(IsDS1 ? 0 : 0x1000000);
				reader.RequireZeroes(4 * 8);

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
				ExpectedOffset(reader, boneUnknownsOffset, typeof(DSModelBoneUnknown).Name);
				for (int index = 0; index < boneUnknownCount; index++)
					boneUnknowns.Add(new DSModelBoneUnknown(this, index, reader));

				// Materials
				ExpectedOffset(reader, materialsOffset, typeof(DSModelMaterial).Name);
				for (int index = 0; index < materialCount; index++)
					materials.Add(new DSModelMaterial(this, index, reader));
				int expectedMaterialParameterCount = materialCount > 0 ? materials[materialCount - 1].ParameterEndIndex : 0;
				if (expectedMaterialParameterCount != materialParameterCount)
					throw new InvalidDataException("Expected material parameter count doesn't match actual count.");

				// Bones
				ExpectedOffset(reader, bonesOffset, typeof(DSModelBone).Name);
				for (int index = 0; index < boneCount; index++)
					bones.Add(new DSModelBone(this, index, reader));

				// Meshes
				ExpectedOffset(reader, meshesOffset, typeof(DSModelMesh).Name);
				for (int index = 0; index < meshCount; index++)
					meshes.Add(new DSModelMesh(this, index, reader));
				int expectedPartCount = meshCount > 0 ? meshes[meshCount - 1].PartEndIndex : 0;
				if (expectedPartCount != partCount)
					throw new InvalidDataException("Expected part count doesn't match actual count.");

				// Parts
				ExpectedOffset(reader, partsOffset, typeof(DSModelPart).Name);
				foreach (DSModelMesh mesh in meshes) {
					mesh.ReadParts(reader, dataOffset);
					parts.AddRange(mesh.Parts);
				}

				// Mesh vertices
				ExpectedOffset(reader, meshVerticesOffset, typeof(DSModelMesh).Name + " vertex header");
				foreach (DSModelMesh mesh in meshes)
					mesh.ReadVertexHeaders(reader, dataOffset);

				// Vertex declarations
				ExpectedOffset(reader, vertexDeclarationsOffset, typeof(DSModelVertexDeclaration).Name);
				for (int index = 0; index < vertexDeclarationCount; index++)
					vertexDeclarations.Add(new DSModelVertexDeclaration(this, index, reader));

				// Material parameters
				ExpectedOffset(reader, materialParametersOffset, typeof(DSModelMaterialParameter).Name);
				foreach (DSModelMaterial material in materials) {
					material.ReadParameters(reader, textureArchive);
					materialParameters.AddRange(material.Parameters);
				}

				ExpectedOffset(reader, postHeaderOffset, "Post-header");

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

				//var collada = GetCollada();
			}
		}

		/// <summary>Create the <see cref="Collada"/> document representation of this mesh if necessary and return it.</summary>
		/// <returns></returns>
		Collada GetCollada() {
			if (Collada != null)
				return Collada;

			Collada = new Glare.Graphics.Collada.Collada();
			Node[] nodes = new Node[Meshes.Count];

			string visualSceneId = "visualScene";

			foreach (DSModelBone bone in Bones)
				bone.BuildCollada();
			foreach (DSModelMaterial material in Materials)
				material.BuildCollada();

			foreach (DSModelMesh mesh in Meshes){
				mesh.BuildCollada();
				nodes[mesh.Index] = mesh.ColladaNode;
			}

			// Build and insert the VisualScene.
			VisualScene visualScene = new VisualScene(visualSceneId, nodes);
			Collada.VisualScenes.Add(visualScene);

			// Build and insert the Scene.
			Scene scene = Collada.Scene = new Scene() {
				VisualSceneInstance = new VisualSceneInstance("#" + visualScene.Id),
			};

			/*StringBuilder builder = new StringBuilder();
			var writer = XmlWriter.Create(builder, new XmlWriterSettings() { Indent = true, IndentChars = "    " });
			var serializer = Collada.GetSerializer();
			serializer.Serialize(writer, Collada);
			var text = builder.ToString();*/

			return Collada;
		}

		void ExpectedOffset(BinaryReader reader, long offset, string section) { if (reader.BaseStream.Position != offset) throw new InvalidDataException("Expected to be at offset " + offset + " for section " + section + "."); }

		public override void FillContextMenu(System.Windows.Forms.ContextMenuStrip strip) {
			base.FillContextMenu(strip);

			ToolStripMenuItem saveColladaDocumentMenuItem = new ToolStripMenuItem("Save &Collada document and images");

			saveColladaDocumentMenuItem.Click += (sender, args) => {
				var dialog = new SaveFileDialog() {
					AddExtension = true,
					DefaultExt = "dae",
					FileName = Name,
					Title = "Select filename for " + Name,
					OverwritePrompt = true,
				};

				if (dialog.ShowDialog() == DialogResult.OK) {
					using (Stream file = File.Open(dialog.FileName, FileMode.Create)) {
						using (XmlWriter writer = XmlWriter.Create(file, new XmlWriterSettings() { Indent = true, IndentChars = "    " })) {
							XmlSerializer serializer = Collada.GetSerializer();
							Collada document = GetCollada();

							serializer.Serialize(writer, document);

							foreach (DSModelMaterial material in Materials)
								material.SaveTextures(Path.GetDirectoryName(dialog.FileName));
						}
					}
				}
			};

			strip.Items.Add(saveColladaDocumentMenuItem);
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

	public class DSModelLoader : Loader {
		public DSModelLoader(Engine engine)
			: base(engine) {
		}

		public override LoaderMatchLevel Match(BinaryReader reader, string name, LoaderFileOpener opener, Resource context) {
			if (!reader.MatchMagic(DSModel.Magic))
				return LoaderMatchLevel.None;
			return LoaderMatchLevel.Strong;
		}

		public override Resource Load(BinaryReader reader, string name, LoaderFileOpener opener, Resource context) {
			return new DSModel(Manager, reader, name, opener, context);
		}
	}
}
