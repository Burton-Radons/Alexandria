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

namespace Alexandria.Engines.DarkSouls {
	public class DSModelPart : DSModelObject {
		#region Internal

		internal const int DataSize = 4 * 8;

		#endregion Internal

		public int Id { get; private set; }

		public ushort[] Indices { get; private set; }

		public DSModelMesh Mesh { get; private set; }

		internal DSModelPart(DSModelMesh mesh, int index, BinaryReader reader, int dataOffset)
			: base(mesh.Model, index) {
			Mesh = mesh;

			Id = reader.ReadInt32();

			Unknowns.ReadInt16s(reader, IsDS1 ? 1 : 2);
			if (IsDS1) reader.RequireZeroes(2);

			int indexCount = reader.ReadInt32();
			int indexDataOffset = reader.ReadInt32() + dataOffset;
			int indexDataSize = reader.ReadInt32();
			if (indexDataSize != indexCount * 2)
				throw new InvalidDataException("Index data size doesn't match the expected value from the index count.");
			reader.RequireZeroes(4 * 3);

			long reset = reader.BaseStream.Position;
			reader.BaseStream.Position = indexDataOffset;
			Indices = reader.ReadArrayUInt16(indexCount);
			reader.BaseStream.Position = reset;
		}

		internal void BuildCollada(List<int> list) {
			for (int index = 0; index < Indices.Length - 2; index++) {
				int a = Indices[index], b = Indices[index + 1], c = Indices[index + 2];

				// Detect degenerate triangles.
				if (a == b || b == c || a == c || a == 65535 || b == 65535 || c == 65535)
					continue;

				if (index % 2 == 0) {
					list.Add(a);
					list.Add(b);
					list.Add(c);
				} else {
					list.Add(c);
					list.Add(b);
					list.Add(a);
				}
			}
		}

		public void BuildModel(ModelBuilder builder) {
			int vertexIndex = Mesh.VertexStartIndex;
			foreach (ushort index in Indices)
				builder.AddIndex(index == 65535 ? -1 : index + vertexIndex);
			builder.FinishPart(Primitive.TriangleStrip, Mesh.Material.ModelMaterial);
		}

		public override string ToString() {
			return string.Format("{0}(id {1:X}, index count {2}, {3})", GetType().Name, Id, Indices.Length, Unknowns[0].JoinedValues);
		}
	}
}
