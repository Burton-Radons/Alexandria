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
using Glare.Assets;

namespace Alexandria.Engines.DarkSouls {
	/// <summary>A detail level of a <see cref="ModelMesh"/>.</summary>
	public class ModelDetailLevel : ModelAsset {
		#region Internal

		internal const int DataSize = 4 * 8;

		/// <summary>Get the offset of the index data.</summary>
		int IndexDataOffset { get; set; }

		#endregion Internal

		/// <summary>Get the offset in the <see cref="Model.Buffer"/> in which the index data (which are ushort values) are stored.</summary>
		public int BufferIndexOffset { get; private set; }

		/// <summary>
		/// Get the threshold distance of the detail level.
		/// </summary>
		public int Threshold { get; private set; }

		/// <summary>Get the number of indices in the triangle strip.</summary>
		public int IndexCount { get; private set; }

		/// <summary>Get the <see cref="ModelMesh"/> that this is in.</summary>
		public ModelMesh Mesh { get; private set; }

		internal ModelDetailLevel(ModelMesh mesh, int index, AssetLoader loader, int dataOffset, ArrayBackedList<byte> bufferData)
			: base(mesh, index, loader) {
			var reader = loader.Reader;

			Mesh = mesh;

			const int indexSize = 2;

			Threshold = reader.ReadInt32();

			Unknowns.ReadInt16s(reader, IsDS1 ? 1 : 2);
			if (IsDS1) reader.RequireZeroes(2);

			IndexCount = reader.ReadInt32();
			IndexDataOffset = reader.ReadInt32() + dataOffset;
			int indexDataSize = reader.ReadInt32();
			if (indexDataSize != IndexCount * indexSize)
				throw new InvalidDataException("Index data size doesn't match the expected value from the index count.");
			reader.RequireZeroes(4 * 3);

			BufferIndexOffset = bufferData.Count;
			long reset = reader.BaseStream.Position;
			reader.BaseStream.Position = IndexDataOffset;
			bufferData.AddRange(reader, IndexCount * indexSize);
			reader.BaseStream.Position = reset;
		}

		/// <summary>
		/// Draw the detail level.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="program"></param>
		public void Draw(ModelDrawContext context, ModelProgram program) {
			program.Draw(Primitive.TriangleStrip, IndexCount, Model.Buffer, ElementType.UInt16, BufferIndexOffset);
		}

		/// <summary>Create a string representation of the object.</summary>
		/// <returns></returns>
		public override string ToString() {
			return string.Format("{0}(id {1:X}, IndexCount {2})", GetType().Name, Threshold, IndexCount, Unknowns.ToCommaPrefixedList());
		}
	}
}
