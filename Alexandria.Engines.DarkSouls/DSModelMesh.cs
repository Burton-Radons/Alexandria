using Glare;
using Glare.Framework;
using Glare.Graphics;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.DarkSouls {
	public class DSModelMesh : DSModelObject {
		#region Internal

		internal const int DataSize = 4 * 12;
		internal const int DataSizeVertexHeader = 4 * 8;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal int PartCount;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal int PartStartIndex { get { return Index > 0 ? Model.Meshes[Index - 1].PartEndIndex : 0; } }

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal int PartEndIndex { get { return PartStartIndex + PartCount; } }

		#endregion Internal

		#region Properties

		public Codex<DSModelBone> Bones { get; private set; }

		public DSModelMaterial Material { get { return Model.Materials[MaterialIndex]; } }

		public int MaterialIndex { get; private set; }

		public Codex<DSModelPart> Parts { get; private set; }

		public int VertexCount { get; private set; }

		public byte[] VertexData { get; private set; }

		public int VertexDeclarationIndex { get; private set; }

		public DSModelVertexDeclaration VertexDeclaration { get { return Model.VertexDeclarations[VertexDeclarationIndex]; } }

		public int VertexSize { get; private set; }

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public int VertexStartIndex { get { return Index > 0 ? Model.Meshes[Index - 1].VertexEndIndex : 0; } }

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public int VertexEndIndex { get { return VertexStartIndex + VertexCount; } }

		#endregion Properties

		internal DSModelMesh(DSModel model, int index, BinaryReader reader)
			: base(model, index) {
			Unknowns.ReadInt32s(reader, 1); // 1?
			MaterialIndex = reader.ReadInt32();
			reader.RequireZeroes(4 * 2);
			Unknowns.ReadInt32s(reader, 1); // 0 or 1, seems to be material-related but is not transparency; second seems bones-related
			int boneCount = reader.ReadInt32();
			reader.RequireZeroes(4 * 1);
			int boneIndicesOffset = reader.ReadInt32();
			PartCount = reader.ReadInt32();
			int partIndicesOffset = reader.ReadInt32();
			reader.Require(1);
			int indexOffset = reader.ReadInt32();

			long reset = reader.BaseStream.Position;

			reader.BaseStream.Position = boneIndicesOffset;
			var bones = new Codex<DSModelBone>();
			Bones = bones;
			for (int i = 0; i < boneCount; i++)
				bones.Add(model.Bones[reader.ReadInt32()]);

			// Read the part indices.
			reader.BaseStream.Position = partIndicesOffset;
			int partStart = PartStartIndex;
			for (int i = 0; i < PartCount; i++)
				reader.Require(i + partStart);

			reader.BaseStream.Position = indexOffset;
			reader.Require(Index);

			reader.BaseStream.Position = reset;
		}

		/// <summary>Create a <see cref="BinaryReader"/> for the <see cref="VertexData"/>.</summary>
		/// <returns></returns>
		BinaryReader CreateVertexDataReader() { return BigEndianBinaryReader.Create(Model.ByteOrder, new MemoryStream(VertexData, false)); }

		internal void BuildModel(Glare.Graphics.Rendering.ModelBuilder builder) {
			Codex<DSModelVertexChannel> channels = Model.VertexDeclarations[VertexDeclarationIndex].Channels;
			int vertexSize = VertexSize;

			using (BinaryReader reader = CreateVertexDataReader()) {
				for (int index = 0; index < VertexCount; index++) {
					reader.BaseStream.Position = index * vertexSize;
					Vector3f position = Vector3f.Zero;
					Vector4i boneIndices = Vector4i.UnitX;
					Vector4f boneWeights = Vector4f.UnitX;
					Vector3f normal = Vector3f.Zero;
					Vector4f? tangent = null;
					Vector4f binormal = Vector4f.Zero;
					Vector4rgba color = Vector4rgba.One;
					Vector2f textureCoordinate = Vector2f.Zero;
					int offset = 0;

					foreach (DSModelVertexChannel channel in channels) {
						Vector4f value = channel.Read(reader, ref offset);

						switch (channel.Usage) {
							case DSModelVertexUsage.Position: position = new Vector3f(value.X, value.Y, value.Z); break;
							case DSModelVertexUsage.Normal: normal = new Vector3f(value.X, value.Y, value.Z); break;
							case DSModelVertexUsage.BlendIndices: boneIndices = (Vector4i)value; break;
							case DSModelVertexUsage.BlendWeight: boneWeights = (Vector4f)value; break;
							case DSModelVertexUsage.Tangent: tangent = value; break;
							case DSModelVertexUsage.Color: color = (Vector4rgba)value; break;
							case DSModelVertexUsage.TextureCoordinate: textureCoordinate = new Vector2f(value.X, value.Y); break;
							case DSModelVertexUsage.Binormal: binormal = value; break;
							default: throw new NotImplementedException("Usage " + channel.Usage + " is not implemented.");
						}
					}

					if (offset != vertexSize)
						throw new InvalidDataException();

					builder.AddVertex(position);
					builder.SetNormal(normal);
					builder.SetColor(color);
					builder.SetTexel(textureCoordinate);
					builder.SetBoneIndices(boneIndices);
					builder.SetBoneWeights(boneWeights);
				}
			}

			foreach (DSModelPart part in Parts) {
				part.BuildModel(builder);
				break;
			}
			builder.FinishMesh();
		}

		internal void ReadParts(BinaryReader reader, int dataOffset) {
			var parts = new Codex<DSModelPart>();
			Parts = parts;
			for (int index = 0; index < PartCount; index++)
				parts.Add(new DSModelPart(this, index, reader, dataOffset));
		}

		internal void ReadVertexHeaders(BinaryReader reader, int dataOffset) {
			reader.RequireZeroes(4 * 1);
			VertexDeclarationIndex = reader.ReadInt32();
			VertexSize = reader.ReadInt32();
			VertexCount = reader.ReadInt32();
			reader.RequireZeroes(4 * 2);
			int vertexDataSize = reader.ReadInt32();
			if (VertexSize * VertexCount != vertexDataSize)
				throw new Exception();
			int vertexDataOffset = reader.ReadInt32() + dataOffset;

			// Read vertex data.
			long reset = reader.BaseStream.Position;
			reader.BaseStream.Position = vertexDataOffset;
			VertexData = reader.ReadBytes(vertexDataSize);
			reader.BaseStream.Position = reset;
		}

		public override string ToString() {
			string text = string.Format("{0}(Material '{1}'", GetType().Name, Path.GetFileNameWithoutExtension(Material.Parameters[0].Value));

			text += ", BoneCount " + Bones.Count;
			text += ", VertexCount " + VertexCount + ", VertexSize " + VertexSize;
			text += Unknowns.ToCommaPrefixedList();
			return text + ")";
		}
	}
}
