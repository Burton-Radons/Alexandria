using Glare;
using Glare.Assets;
using Glare.Framework;
using Glare.Graphics;
using Glare.Graphics.Rendering;
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
	/// <summary>
	/// A mesh in a <see cref="Model"/>. This is composed of a set of <see cref="ModelDetailLevel"/>s as detail levels.
	/// </summary>
	public class ModelMesh : ModelAsset {
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

		/// <summary>Get the <see cref="ModelBone"/>s that are indexed by this <see cref="ModelMesh"/>.</summary>
		public Codex<ModelBone> Bones { get; private set; }

		/// <summary>Get the offset into the <see cref="Model.Buffer"/> for the vertex data.</summary>
		public int BufferDataOffset { get; private set; }

		/// <summary>Get an appropriate display name.</summary>
		public override string DisplayName { get { return ToString(); } }

		/// <summary>Get the <see cref="ModelMaterial"/> used by this <see cref="ModelMesh"/>.</summary>
		public ModelMaterial Material { get { return Model.Materials[MaterialIndex]; } }

		/// <summary>Get the index of the material used by this <see cref="ModelMesh"/>.</summary>
		public int MaterialIndex { get; private set; }

		/// <summary>Get the <see cref="ModelDetailLevel"/> detail levels.</summary>
		public Codex<ModelDetailLevel> DetailLevels { get; private set; }

		/// <summary>Get the number of vertices.</summary>
		public int VertexCount { get; private set; }

		/// <summary>Get the position in the file of the vertex data.</summary>
		public int VertexDataOffset { get; private set; }

		/// <summary>Get the index of the vertex declaration to use.</summary>
		public int VertexDeclarationIndex { get; private set; }

		/// <summary>Get the <see cref="ModelVertexDeclaration"/> this uses.</summary>
		public ModelVertexDeclaration VertexDeclaration { get { return Model.VertexDeclarations[VertexDeclarationIndex]; } }

		/// <summary>Get the size in bytes of a vertex.</summary>
		public int VertexSize { get; private set; }

		/// <summary>If the vertices in the <see cref="ModelMesh"/>es were counted, this would be the first index of the first vertex of this mesh.</summary>
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public int VertexStartIndex { get { return Index > 0 ? Model.Meshes[Index - 1].VertexEndIndex : 0; } }

		/// <summary>If the vertices in the <see cref="ModelMesh"/>es were counted, this would be after the last index of the last vertex of this mesh.</summary>
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public int VertexEndIndex { get { return VertexStartIndex + VertexCount; } }

		#endregion Properties

		internal ModelMesh(FolderAsset folder, int index, AssetLoader loader)
			: base(folder, index, loader) {
			var reader = loader.Reader;

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

		/*/// <summary>Create a <see cref="BinaryReader"/> for the <see cref="VertexData"/>.</summary>
		/// <returns></returns>
		//BinaryReader CreateVertexDataReader() { return BigEndianBinaryReader.Create(Model.ByteOrder, new MemoryStream(VertexData, false)); }*/

		ModelProgram Program;

		/// <summary>
		/// Draw the <see cref="ModelMesh"/>.
		/// </summary>
		/// <param name="context"></param>
		public void Draw(ModelDrawContext context) {
			try {
				if (Program == null) {
					Program = new ModelProgram();
					VertexDeclaration.Bind(Program, this, VertexSize);
					Material.Bind(context, Program);
				}

				Program.Projection = context.Projection;
				Program.View = context.View;
				Program.World = context.World;
				Program.DisplayMode = context.DisplayMode;
				DetailLevels[0].Draw(context, Program);
			} catch (Exception exception) {
				throw exception;
			}
		}

		internal void ReadDetailLevels(AssetLoader loader, int dataOffset, ArrayBackedList<byte> bufferData) {
			var detailLevels = new Codex<ModelDetailLevel>();
			DetailLevels = detailLevels;
			for (int index = 0; index < PartCount; index++)
				detailLevels.Add(new ModelDetailLevel(this, index, loader, dataOffset, bufferData));
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
			VertexDataOffset = reader.ReadInt32() + dataOffset;

			// Read vertex data.
			long reset = reader.BaseStream.Position;
			reader.BaseStream.Position = VertexDataOffset;
			BufferDataOffset = bufferData.Count;
			bufferData.AddRange(reader, vertexDataSize);
			reader.BaseStream.Position = reset;
		}

		/// <summary>Convert to a string representation.</summary>
		/// <returns></returns>
		public override string ToString() {
			string text = string.Format("{0}(Material '{1}'", GetType().Name, Path.GetFileNameWithoutExtension(Material.Parameters[0].Value));

			text += ", BoneCount " + Bones.Count;
			text += ", VertexCount " + VertexCount + ", VertexSize " + VertexSize;
			text += Unknowns.ToCommaPrefixedList();
			return text + ")";
		}
	}
}
