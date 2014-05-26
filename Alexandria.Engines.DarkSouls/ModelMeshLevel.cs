using Glare.Graphics;
using Glare.Graphics.Rendering;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glare.Internal;
using Glare.Framework;

namespace Alexandria.Engines.DarkSouls {
	/// <summary>A detail level of a <see cref="ModelMesh"/>.</summary>
	public class ModelMeshLevel : ModelObject {
		#region Internal

		internal const int DataSize = 4 * 8;

		#endregion Internal

		public int Id { get; private set; }

		/// <summary>Get the number of indices in the triangle strip.</summary>
		public int IndexCount { get; private set; }

		public int IndexDataOffset { get; private set; }

		/// <summary>Get the offset in the <see cref="Model.Buffer"/> in which the index data (which are ushort values) are stored.</summary>
		public int IndexOffset { get; private set; }

		/// <summary>Get the <see cref="ModelMesh"/> that this is in.</summary>
		public ModelMesh Mesh { get; private set; }

		internal ModelMeshLevel(ModelMesh mesh, int index, BinaryReader reader, int dataOffset, ArrayBackedList<byte> bufferData)
			: base(mesh, index, reader) {
			Mesh = mesh;

			const int indexSize = 2;

			Id = reader.ReadInt32();

			Unknowns.ReadInt16s(reader, IsDS1 ? 1 : 2);
			if (IsDS1) reader.RequireZeroes(2);

			IndexCount = reader.ReadInt32();
			IndexDataOffset = reader.ReadInt32() + dataOffset;
			int indexDataSize = reader.ReadInt32();
			if (indexDataSize != IndexCount * indexSize)
				throw new InvalidDataException("Index data size doesn't match the expected value from the index count.");
			reader.RequireZeroes(4 * 3);

			long reset = reader.BaseStream.Position;
			reader.BaseStream.Position = IndexDataOffset;
			bufferData.AddRange(reader, IndexCount * indexSize);
			reader.BaseStream.Position = reset;
		}

		public override string ToString() {
			return string.Format("{0}(id {1:X}, IndexCount {2}, IndexData at {4:X}h{3})", GetType().Name, Id, IndexCount, Unknowns.ToCommaPrefixedList(), IndexDataOffset);
		}
	}
}
