using Glare;
using Glare.Assets;
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
	public class ModelMesh : ModelObject {
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

		public Codex<ModelBone> Bones { get; private set; }

		public override string DisplayName { get { return ToString(); } }

		public ModelMaterial Material { get { return Model.Materials[MaterialIndex]; } }

		public int MaterialIndex { get; private set; }

		public Codex<ModelMeshLevel> DetailLevels { get; private set; }

		public int VertexCount { get; private set; }

		/// <summary>Get the offset into the <see cref="Model.Buffer"/> for the vertex data.</summary>
		public int VertexDataOffset { get; private set; }

		public int VertexDeclarationIndex { get; private set; }

		public ModelVertexDeclaration VertexDeclaration { get { return Model.VertexDeclarations[VertexDeclarationIndex]; } }

		public int VertexSize { get; private set; }

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public int VertexStartIndex { get { return Index > 0 ? Model.Meshes[Index - 1].VertexEndIndex : 0; } }

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public int VertexEndIndex { get { return VertexStartIndex + VertexCount; } }

		#endregion Properties

		internal ModelMesh(FolderAsset folder, int index, BinaryReader reader)
			: base(folder, index, reader) {
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
			var bones = new Codex<ModelBone>();
			Bones = bones;
			for (int i = 0; i < boneCount; i++)
				bones.Add(Model.Bones[reader.ReadInt32()]);

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
		//BinaryReader CreateVertexDataReader() { return BigEndianBinaryReader.Create(Model.ByteOrder, new MemoryStream(VertexData, false)); }

		internal void ReadDetailLevels(BinaryReader reader, int dataOffset, ArrayBackedList<byte> bufferData) {
			var detailLevels = new Codex<ModelMeshLevel>();
			DetailLevels = detailLevels;
			for (int index = 0; index < PartCount; index++)
				detailLevels.Add(new ModelMeshLevel(this, index, reader, dataOffset, bufferData));
		}

		internal void ReadVertexHeaders(BinaryReader reader, int dataOffset, ArrayBackedList<byte> bufferData) {
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
			VertexDataOffset = bufferData.Count;
			bufferData.AddRange(reader, vertexDataSize);
			reader.BaseStream.Position = reset;
		}

		public override string ToString() {
			string text = string.Format("{0}(at {2:X}h, Material '{1}'", GetType().Name, Path.GetFileNameWithoutExtension(Material.Parameters[0].Value), DataOffset);

			text += ", BoneCount " + Bones.Count;
			text += ", VertexCount " + VertexCount + ", VertexSize " + VertexSize;
			text += string.Format(", VertexData at {0:X}h", DataOffset);
			text += Unknowns.ToCommaPrefixedList();
			return text + ")";
		}
	}
}
