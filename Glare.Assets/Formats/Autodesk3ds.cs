using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glare.Internal;
using Glare.Graphics.Rendering;
using Glare.Graphics.Scenes;
using Glare.Graphics.Scenes.Components;

namespace Glare.Assets.Formats {
	public class Autodesk3ds : ModelAsset {
		public const int MaxModelVersion = 3, MinModelVersion = 0;
		public const int MaxEditorVersion = 3, MinEditorVersion = 0;

		internal Autodesk3ds(AssetLoader loader, Autodesk3dsFormat format)
			: base(format.Manager, "3DS Model - " + loader.Name) {
			var reader = loader.Reader;

			AssetFormat = format;

			Chunk chunk, subchunk;
			if (!Chunk.ReadRequired(loader, ChunkId.Main, out chunk))
				return;

			while (chunk.ReadSubchunk(out subchunk)) {
				switch (subchunk.Id) {
					case ChunkId.ModelVersion:
						int modelVersion = subchunk.ReadContentInt(0);
						if (modelVersion > MaxModelVersion || modelVersion < MinModelVersion)
							loader.AddError(chunk.Offset, "3DS model version {0} is out of range ({1} to {2} supported).", modelVersion, MinModelVersion, MaxModelVersion);
						break;

					case ChunkId.Editor:
						ReadEditor(subchunk);
						break;

					default:
						chunk.ReportUnknownSubchunkError(subchunk);
						break;
				}

				subchunk.RequireAtEnd();
			}
		}

		bool ReadEditor(Chunk chunk) {
			var reader = chunk.Reader;
			Chunk subchunk;
			List<ModelMaterial> materials = new List<ModelMaterial>();
			List<Node> nodes = new List<Node>();
			float masterScale = 1;

			while (chunk.ReadSubchunk(out subchunk)) {
				switch (subchunk.Id) {
					case ChunkId.EditorVersion:
						int editorVersion = subchunk.ReadContentInt(0);
						if (editorVersion > MaxEditorVersion || editorVersion < MinEditorVersion)
							chunk.Loader.AddError(chunk.Offset, "3DS editor version {0} is out of range ({1} to {2} supported).", editorVersion, MinEditorVersion, MaxEditorVersion);
						break;

					case ChunkId.MasterScale: masterScale = reader.ReadSingle(); break;

					case ChunkId.Material:
						materials.Add(ReadMaterial(subchunk));
						break;

					case ChunkId.Node: nodes.Add(ReadNode(subchunk)); break;

					default: chunk.ReportUnknownSubchunkError(subchunk); break;
				}

				subchunk.RequireAtEnd();
			}

			return true;
		}

		ModelMaterial ReadMaterial(Chunk root) {
			var loader = root.Loader;
			var reader = root.Reader;
			Chunk chunk;
			ModelMaterial material = new ModelMaterial();

			int? shading = null;

			while (root.ReadSubchunk(out chunk)) {
				switch (chunk.Id) {
					case ChunkId.MaterialName: material.Name = chunk.ReadContentString(); break;
					case ChunkId.MaterialAmbient: material.AmbientColor = chunk.ReadContentColor(Vector3d.One); break;
					case ChunkId.MaterialDiffuse: material.DiffuseColor = chunk.ReadContentColor(Vector3d.One); break;
					case ChunkId.MaterialSpecular: material.SpecularColor = chunk.ReadContentColor(Vector3d.One); break;
					case ChunkId.MaterialSpecularSoften: material.SpecularSoftness = chunk.ReadContentPercentage(0.1); break;
					case ChunkId.MaterialSpecularLevel: material.SpecularLevel = chunk.ReadContentPercentage(1); break;
					case ChunkId.MaterialTransparency: material.Opacity = chunk.ReadContentPercentage(1); break;
					case ChunkId.MaterialTransparencyFalloff: material.OpacityFalloffLevel = 1 - chunk.ReadContentPercentage(0); break;
					case ChunkId.MaterialReflectionBlur:
						double reflectionBlur = chunk.ReadContentPercentage(0);
						if (reflectionBlur != 0)
							loader.AddError(chunk.Offset, "Expected a reflection blur value of 0 for material; value is ignored.");
						break;
					case ChunkId.MaterialShading: shading = chunk.ReadContentInt(-1); break;
					case ChunkId.MaterialSelfIlluminationPercentage: material.SelfIlluminationLevel = chunk.ReadContentPercentage(0); break;
					case ChunkId.MaterialTransparencyFalloffIn: material.OpacityFalloff = ModelMaterialOpacityFalloff.In; break;
					case ChunkId.MaterialWireSize: material.WireSize = reader.ReadSingle(); break;
					default: root.ReportUnknownSubchunkError(chunk); break;
				}

				chunk.RequireAtEnd();
			}

			return material;
		}

		Node ReadNode(Chunk root) {
			Node node = new Node();
			var reader = root.Reader;
			Chunk chunk;

			node.Name = root.ReadContentString();

			while(root.ReadSubchunk(out chunk)) {
				switch (chunk.Id) {
					case ChunkId.ComponentTriangles: node.Components.Add(ReadNodeComponentTriangles(chunk)); break;
					default: root.ReportUnknownSubchunkError(chunk); break;
				}
				chunk.RequireAtEnd();
			}

			return node;
		}

		Component ReadNodeComponentTriangles(Chunk root) {
			ModelBuilder builder = new ModelBuilder();
			AssetLoader loader = root.Loader;
			var reader = root.Reader;
			Chunk chunk;

			Vector3f[] points = null;
			Vector2f[] texels = null;
			int count;
			Matrix4f transform = Matrix4f.Identity;

			while (root.ReadSubchunk(out chunk)) {
				switch (chunk.Id) {
					case ChunkId.ComponentTrianglesPointArray:
						count = (chunk.DataLength - 2) / 12;
						loader.Expect((ushort)count);
						points = reader.ReadArrayVector3f(count);
						break;

					case ChunkId.ComponentTrianglesTexelArray:
						count = (chunk.DataLength - 2) / 8;
						loader.Expect((ushort)count);
						texels = reader.ReadArrayVector2f(count);
						break;

					case ChunkId.ComponentTrianglesTransform:
						for (int row = 0; row < 4; row++)
							for (int column = 0; column < 3; column++)
								transform[row, column] = reader.ReadSingle();
						break;

					case ChunkId.ComponentTrianglesFaceArray:
						count = reader.ReadUInt16();
						ushort[] faces = reader.ReadArrayUInt16(count * 4);
						string unknown1Name = chunk.ReadContentString(); // "0A-"
						loader.Expect((ushort)0);
						string materialName = chunk.ReadContentString();

						count = reader.ReadUInt16();
						ushort[] unknownIndices = reader.ReadArrayUInt16(count); // Seems to just be indices?
						string unknown2Name = chunk.ReadContentString(); // "PA6"
						loader.Expect((ushort)0);
						uint[] smoothingGroups = reader.ReadArrayUInt32(count); 

						break;

					default: root.ReportUnknownSubchunkError(chunk); break;
				}
				chunk.RequireAtEnd();
			}

			return new RenderModelComponent(builder.Finish());
		}

		#region Nested types

		public struct Chunk {
			public bool AtEnd { get { return Loader.Position >= End; } }
			public int DataLength { get { return Length >= 6 ? Length - 6 : 0; } }
			public long End { get { return Offset + Length; } }
			public readonly ChunkId Id;
			public string IdString { get { return IdToString(Id); } }
			public readonly AssetLoader Loader;
			public readonly long Offset;
			public readonly int Length;
			public BinaryReader Reader { get { return Loader.Reader; } }

			public Chunk(AssetLoader loader) {
				var reader = loader.Reader;
				Loader = loader;
				Offset = reader.BaseStream.Position;
				Id = (ChunkId)reader.ReadUInt16();
				Length = reader.ReadInt32();
			}

			public static string IdToString(ChunkId id) {
				if (!typeof(ChunkId).IsEnumDefined(id))
					return string.Format("{0:X4}h", (int)id);
				return string.Format("{0}/{1:X4}h", id, (int)id);
			}

			public int ReadContentInt(int defaultValue) {
				switch (DataLength) {
					case 1: return Reader.ReadByte();
					case 2: return Reader.ReadUInt16();
					case 4: return Reader.ReadInt32();
					default:
						Loader.AddError(Offset, "Expected chunk {0} to contain an integer; but its data length is {1}.", IdString, DataLength);
						return defaultValue;
				}
			}

			public Vector3d ReadContentColor(Vector3d defaultValue) {
				Vector3d? value3b = null, value3f = null, valueLinear3b = null, valueLinear3f = null;
				Chunk subchunk;

				if (DataLength == 0)
					Loader.AddError(Offset, "Expected chunk {0} to contain a color, but it is empty.", IdString);
				while (ReadSubchunk(out subchunk)) {
					switch (subchunk.Id) {
						case ChunkId.Color3b: value3b = new Vector3d(Reader.ReadByte() / 255.0, Reader.ReadByte() / 255.0, Reader.ReadByte() / 255.0); break;
						case ChunkId.Color3f: value3f = Reader.ReadVector3f(); break;
						case ChunkId.LinearColor3b: valueLinear3b = new Vector3d(Reader.ReadByte() / 255.0, Reader.ReadByte() / 255.0, Reader.ReadByte() / 255.0); break;
						case ChunkId.LinearColor3f: valueLinear3f = Reader.ReadVector3f(); break;
						default: ReportUnknownSubchunkError(subchunk); break;
					}
					subchunk.RequireAtEnd();
				}

				return valueLinear3f.HasValue ? valueLinear3f.Value : valueLinear3b.HasValue ? valueLinear3b.Value : value3f.HasValue ? value3f.Value : value3b.HasValue ? value3b.Value : defaultValue;
				
			}

			public double ReadContentPercentage(double defaultValue) {
				double? value1s = null, value1f = null;
				Chunk subchunk;

				if (DataLength == 0)
					Loader.AddError(Offset, "Expected chunk {0} to contain a percentage, but it is empty.", IdString);
				while (ReadSubchunk(out subchunk)) {
					switch (subchunk.Id) {
						case ChunkId.Percentage1s: value1s = Reader.ReadInt16() / 100.0; break;
						case ChunkId.Percentage1f: value1f = Reader.ReadSingle() / 100.0; break;
						default: ReportUnknownSubchunkError(subchunk); break;
					}
					subchunk.RequireAtEnd();
				}

				return value1f.HasValue ? value1f.Value : value1s.HasValue ? value1s.Value : defaultValue;
			}

			public string ReadContentString() {
				if (DataLength == 0)
					return null;
				return Reader.ReadStringz(Encoding.UTF8);
			}

			public static bool ReadRequired(AssetLoader loader, ChunkId requiredId, out Chunk chunk) {
				chunk = new Chunk(loader);
				return chunk.RequireId(requiredId);
			}

			public bool ReadRequiredSubchunk(out Chunk subchunk, ChunkId requiredId) {
				if (!ReadSubchunk(out subchunk)) {
					Loader.Errors.Add(new AssetLoadError(Loader, null, "Expected subchunk of type " + IdToString(requiredId) + " but chunk " + IdToString(Id) + " ended."));
					return false;
				}

				return subchunk.RequireId(requiredId);
			}

			public bool ReadSubchunk(out Chunk subchunk) {
				if (AtEnd) {
					subchunk = default(Chunk);
					return false;
				}

				subchunk = new Chunk(Loader);
				return true;
			}

			public void ReportUnknownSubchunkError(Chunk subchunk) {
				Loader.AddError(subchunk.Offset, "Unknown chunk type {0} (data length {2}) while reading chunk {1}; skipped", subchunk.IdString, IdString, subchunk.DataLength);
				subchunk.Skip();
			}

			/// <summary>Require the loader to be at the end of the chunk. If it's not, report an error and seek to the end.</summary>
			/// <returns></returns>
			public bool RequireAtEnd() {
				long position = Loader.Position;

				if (position == End)
					return false;

				if (position > End)
					Loader.AddError(position, "Read past the end of the {0} chunk at offset {1}/{1:X}h by {2} byte(s).", IdString, Offset, position - End);
				else
					Loader.AddError(position, "The {0} chunk at offset {1}/{1:X}h was not fully read; {2} byte(s) remain, which are either content or sub-chunks.", IdString, Offset, End - position);

				Skip();
				return false;
			}

			public bool RequireId(ChunkId requiredId) {
				if (requiredId == Id)
					return true;
				Loader.Errors.Add(new AssetLoadError.InvalidData(Loader, Offset, IdToString(requiredId), IdToString(Id)));
				return false;
			}

			public void Skip() {
				Loader.Position = End;
			}
		}

		public enum ChunkId : ushort {
			/// <summary>NULL_CHUNK.</summary>
			Null = 0x0000,

			/// <summary>M3D_VERSION (u2 version); child of <see cref="Main"/>.</summary>
			ModelVersion = 0x0002,

			/// <summary>M3D_KFVERSION</summary>
			KeyframeVersion = 0x0005,

			/// <summary>(f4 red, f4 green, f4 blue)</summary>
			Color3f = 0x0010,

			/// <summary>(u1 red, u1 green, u1 blue)</summary>
			Color3b = 0x0011,

			/// <summary>(u1 red, u1 green, u1 blue)</summary>
			LinearColor3b = 0x0012,

			/// <summary>(f4 red, f4 green, f4 blue)</summary>
			LinearColor3f = 0x0013,

			/// <summary>(s2 value)</summary>
			Percentage1s = 0x0030,

			/// <summary>(f1 value)</summary>
			Percentage1f = 0x0031,

			/// <summary>(f4 value)</summary>
			MasterScale = 0x0100,

			/// <summary>EDIT3DS; child of <see cref="Main"/>. Contains <see cref="EditorVersion"/>, <see cref="Material"/>, <see cref="Node"/>.</summary>
			Editor = 0x3d3d,

			EditorVersion = 0x3d3e,

			/// <summary>(stringz name); child of <see cref="Editor"/>. Contains <see cref="ComponentTriangles"/>.</summary>
			Node = 0x4000,

			/// <summary>Child of <see cref="Node"/>. Contains <see cref="ComponentTrianglesPointArray"/>, <see cref="ComponentTrianglesFaceArray"/>, <see cref="ComponentTrianglesTexelArray"/>, <see cref="ComponentTrianglesTransform"/>.</summary>
			ComponentTriangles = 0x4100,

			/// <summary>(ushort pointCount; Vector3f[pointCount] points); child of <see cref="ComponentTriangles"/>.</summary>
			ComponentTrianglesPointArray = 0x4110,

			/// <summary>FACE_ARRAY (ushort faceCount; { ushort[3] vertices, ushort flags }[faceCount] faces); child of <see cref="ComponentTriangles"/>.</summary>
			ComponentTrianglesFaceArray = 0x4120,

			/// <summary>(ushort pointCount; Vector2f[pointCount] points); child of <see cref="ComponentTriangles"/>.</summary>
			ComponentTrianglesTexelArray = 0x4140,

			/// <summary>MESH_MATRIX (float[4][3] matrix); child of <see cref="ComponentTriangles"/>.</summary>
			ComponentTrianglesTransform = 0x4160,

#if false
4111H	POINT_FLAG_ARRAY
short nflags;
short flags[nflags];
4120H	FACE_ARRAY may be followed by smooth_group
short nfaces;
struct {
short vertex1, vertex2, vertex3;
short flags;
} facearray[nfaces];
4130H	MSH_MAT_GROUP mesh_material_group
cstr material_name;
short nfaces;
short facenum[nfaces];
4131H	OLD_MAT_GROUP
4150H	SMOOTH_GROUP
short grouplist[n]; determined by length, seems to be 4 per face
4165H	MESH_COLOR
short color_index;
4170H	MESH_TEXTURE_INFO
short map_type;
float x_tiling, y_tiling;
float icon_x, icon_y, icon_z;
float matrix[4][3];
float scaling, plan_icon_w, plan_icon_h, cyl_icon_h;
#endif

			/// <summary>MAIN3DS. Main chunk magic; contains <see cref="ModelVersion"/>, <see cref="Editor"/>, and <see cref="Keyframe"/>.</summary>
			Main = 0x4d4d,

			/// <summary>(stringz) Material name; child of <see cref="Material"/>.</summary>
			MaterialName = 0xA000,

			/// <summary>[Color] Material ambient color; child of <see cref="Material"/>.</summary>
			MaterialAmbient = 0xA010,

			/// <summary>[Color] Material diffuse color; child of <see cref="Material"/>.</summary>
			MaterialDiffuse = 0xA020,

			/// <summary>[Color] Material specular color; child of <see cref="Material"/>.</summary>
			MaterialSpecular = 0xA030,

			/// <summary>[Percentage] Material specular softness; child of <see cref="Material"/>.</summary>
			MaterialSpecularSoften = 0xA040,

			/// <summary>[Percentage] Material specular level; child of <see cref="Material"/>.</summary>
			MaterialSpecularLevel = 0xA041,

			/// <summary>[Percentage] Material transparency; child of <see cref="Material"/>.</summary>
			MaterialTransparency = 0xA050,
			
			/// <summary>[Percentage]; child of <see cref="Material"/>.</summary>
			MaterialTransparencyFalloff = 0xA052,

			/// <summary>[Percentage]; child of <see cref="Material"/>.</summary>
			MaterialReflectionBlur = 0xA053,

			/// <summary>[Percentage]; child of <see cref="Material"/>.</summary>
			MaterialSelfIlluminationPercentage = 0xA084,

			/// <summary>(float); child of <see cref="Material"/>.</summary>
			MaterialWireSize = 0xA087,

			/// <summary>[Percentage]; child of <see cref="Material"/>.</summary>
			MaterialTransparencyFalloffIn = 0xA08A,

			/// <summary>(short value); child of <see cref="Material"/></summary>
			MaterialShading = 0xA100,

			/// <summary>Material definition. Child of <see cref="Editor"/>. Contains <see cref="MaterialName"/>, <see cref="MaterialAmbient"/>, <see cref="MaterialDiffuse"/>, <see cref="MaterialSpecular"/>, <see cref="MaterialSpecularSoften"/>, <see cref="MaterialSpecularLevel"/>, <see cref="MaterialTransparency"/>, <see cref="MaterialTransparencyFalloff"/>, <see cref="MaterialReflectionBlur"/>, <see cref="MaterialSelfIlluminationPercentage"/>, <see cref="MaterialWireSize"/>, <see cref="MaterialTransparencyFalloffIn"/>, <see cref="MaterialShading"/>.</summary>
			Material = 0xAFFF,

			/// <summary>KEYF3DS.</summary>
			Keyframe = 0xb000,
		}

		#endregion Nested types
	}

	/// <summary>
	/// Autodesk 3DS file format.
	/// </summary>
	public class Autodesk3dsFormat : AssetFormat {
		internal Autodesk3dsFormat(DefaultPlugin plugin)
			: base(plugin, typeof(ModelAsset), canLoad: true, extension: ".3ds") {
		}

		public override LoadMatchStrength LoadMatch(AssetLoader loader) {
			if (loader.Length < 32)
				return LoadMatchStrength.None;
			Autodesk3ds.Chunk chunk = new Autodesk3ds.Chunk(loader);
			if (chunk.Id != Autodesk3ds.ChunkId.Main || chunk.End != loader.End)
				return LoadMatchStrength.None;
			return LoadMatchStrength.Medium; // Stronger than a magic match, not as strong as a real header.
		}

		public override Asset Load(AssetLoader loader) {
			return new Autodesk3ds(loader, this);
		}
	}
}
