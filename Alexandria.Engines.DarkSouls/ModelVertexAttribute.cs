using Glare;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Alexandria.Engines.DarkSouls {
	/// <summary>
	/// A channel in a <see cref="ModelVertexDeclaration"/>.
	/// </summary>
	public class ModelVertexAttribute : ModelObject {
		#region Internal

		internal const int DataSize = 4 * 5;

		#endregion Internal

		#region Properties

		public ModelVertexDeclaration Declaration { get; private set; }

		public int Offset { get; private set; }

		public ModelVertexFormat Format { get; private set; }

		public ModelVertexUsage Usage { get; private set; }

		public int Set { get; private set; }

		/// <summary>Get the index of the first vertex axis</summary>
		public int VertexStartOffset { get { return Index > 0 ? Declaration.Attributes[Index - 1].ValueEndOffset : 0; } }

		public int ValueEndOffset { get { return VertexStartOffset + VertexOrder; } }

		public string VertexId { get { return Usage + (Set > 0 ? Set.ToString() : ""); } }

		/// <summary>Get the number of vertex axes in the channel.</summary>
		public int VertexOrder {
			get {
				switch (Format) {
					case ModelVertexFormat.Vector2ns1024:
						return 2;

					case ModelVertexFormat.Vector3f:
						return 3;

					case ModelVertexFormat.Vector4abgr:
					case ModelVertexFormat.Vector4b:
					case ModelVertexFormat.Vector4ns:
					case ModelVertexFormat.Vector4ns1024:
					case ModelVertexFormat.Vector4nsb:
					case ModelVertexFormat.Vector4nsb2:
						return 4;

					default:
						throw new NotImplementedException();
				}
			}
		}

		#endregion Properties

		internal ModelVertexAttribute(ModelVertexDeclaration declaration, BinaryReader reader, int index)
			: base(declaration, index, reader) {
			Declaration = declaration;
			reader.RequireZeroes(4);
			Offset = reader.ReadInt32();
			Format = (ModelVertexFormat)reader.ReadInt32();
			Usage = (ModelVertexUsage)reader.ReadInt32();
			Set = reader.ReadInt32();
		}

		public Vector4f Read(BinaryReader reader, ref int offset) {
			Vector4f value;
			int size;

			if (Offset != offset)
				throw new Exception();

			switch (Format) {
				case ModelVertexFormat.Vector3f:
					size = 12;
					value.X = reader.ReadSingle();
					value.Y = reader.ReadSingle();
					value.Z = reader.ReadSingle();
					value.W = 1;
					break;

				case ModelVertexFormat.Vector4abgr:
					size = 4;
					value.W = reader.ReadByte() / 255f;
					value.Z = reader.ReadByte() / 255f;
					value.Y = reader.ReadByte() / 255f;
					value.X = reader.ReadByte() / 255f;
					break;

				case ModelVertexFormat.Vector4b:
					size = 4;
					value.X = reader.ReadByte();
					value.Y = reader.ReadByte();
					value.Z = reader.ReadByte();
					value.W = reader.ReadByte();
					break;

				case ModelVertexFormat.Vector4nsb:
				case ModelVertexFormat.Vector4nsb2:
					size = 4;
					value.X = reader.ReadSByte() / 127f;
					value.Y = reader.ReadSByte() / 127f;
					value.Z = reader.ReadSByte() / 127f;
					value.W = reader.ReadSByte() / 127f;
					break;

				case ModelVertexFormat.Vector2ns1024:
					size = 4;
					value.X = reader.ReadInt16() / 1024f;
					value.Y = reader.ReadInt16() / 1024f;
					value.Z = value.W = 0;
					break;

				case ModelVertexFormat.Vector4ns1024:
					size = 8;
					value.X = reader.ReadInt16() / 1024f;
					value.Y = reader.ReadInt16() / 1024f;
					value.Z = reader.ReadInt16() / 1024f;
					value.W = reader.ReadInt16() / 1024f;
					break;

				case ModelVertexFormat.Vector4ns:
					size = 8;
					value.X = reader.ReadInt16() / 32767f;
					value.Y = reader.ReadInt16() / 32767f;
					value.Z = reader.ReadInt16() / 32767f;
					value.W = reader.ReadInt16() / 32767f;
					break;

				default:
					throw new NotImplementedException("Format " + Format + " is not implemented.");
			}

			offset += size;
			return value;
		}

		public override string ToString() {
			return string.Format("{0}(at {5:X}h, Offset {1}, Type {2}, Usage {3}{4}{6})", GetType().Name, Offset, Format, Usage, Set > 0 ? "+" + Set : "", DataOffset, Unknowns.ToCommaPrefixedList());
		}
	}
}
