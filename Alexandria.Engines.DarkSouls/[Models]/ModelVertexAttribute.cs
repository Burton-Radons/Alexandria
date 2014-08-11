using Glare;
using Glare.Assets;
using Glare.Graphics;
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
	public class ModelVertexAttribute : ModelAsset {
		#region Internal

		internal const int DataSize = 4 * 5;

		#endregion Internal

		#region Properties

		/// <summary>
		/// Get the vertex declaration this is a part of.
		/// </summary>
		public ModelVertexDeclaration Declaration { get; private set; }

		/// <summary>
		/// Get the offset in bytes from the start of the vertex to the attribute.
		/// </summary>
		public int Offset { get; private set; }

		/// <summary>
		/// Get the format of the attribute.
		/// </summary>
		public ModelVertexFormat Format { get; private set; }

		/// <summary>
		/// Get the graphics format of the attribute.
		/// </summary>
		public Format GraphicsFormat {
			get {
				switch (Format) {
					case ModelVertexFormat.Vector2ns1024: return Formats.Vector2s;
					case ModelVertexFormat.Vector3f: return Formats.Vector3f;
					case ModelVertexFormat.Vector4abgr: return Formats.Vector4nb;
					case ModelVertexFormat.Vector4b: return Formats.Vector4b;
					case ModelVertexFormat.Vector4ns: return Formats.Vector4ns;
					case ModelVertexFormat.Vector4ns1024: return Formats.Vector4s;
					case ModelVertexFormat.Vector4nsb: return Formats.Vector4nsb;
					case ModelVertexFormat.Vector4nsb2: return Formats.Vector4nsb;
					default: throw new NotImplementedException();
				}
			}
		}

		/// <summary>
		/// Get how the attribute is used.
		/// </summary>
		public ModelVertexUsage Usage { get; private set; }

		/// <summary>
		/// Get the usage index.
		/// </summary>
		public int UsageIndex { get; private set; }

		/// <summary>Get the index of the first vertex axis</summary>
		public int VertexStartOffset { get { return Index > 0 ? Declaration.Attributes[Index - 1].ValueEndOffset : 0; } }

		/// <summary>Get the index past the last vertex axis.</summary>
		public int ValueEndOffset { get { return VertexStartOffset + VertexOrder; } }

		/// <summary>Get the usage identifier.</summary>
		public string VertexId { get { return Usage + (UsageIndex > 0 ? UsageIndex.ToString() : ""); } }

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

		internal ModelVertexAttribute(ModelVertexDeclaration declaration, AssetLoader loader, int index)
			: base(declaration, index, loader) {
			var reader = loader.Reader;

			Declaration = declaration;
			reader.RequireZeroes(4);
			Offset = reader.ReadInt32();
			Format = (ModelVertexFormat)reader.ReadInt32();
			Usage = (ModelVertexUsage)reader.ReadInt32();
			UsageIndex = reader.ReadInt32();
		}

		/// <summary>Get the <see cref="ProgramAttribute"/> this should bind to, or <c>null</c> if there is none.</summary>
		/// <param name="program"></param>
		/// <returns></returns>
		public ProgramAttribute MatchAttribute(ModelProgram program) {
			switch (Usage) {
				case ModelVertexUsage.BlendIndices: return program.BoneIndices;
				case ModelVertexUsage.BlendWeight: return program.BoneWeights;
				case ModelVertexUsage.Normal: return program.Normal;
				case ModelVertexUsage.Position: return program.Position;
				case ModelVertexUsage.TextureCoordinate: return program.Texel;
				default: return null;
			}
		}

		/// <summary>Read the attribute.</summary>
		/// <param name="reader"></param>
		/// <param name="offset"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Get a string representation of the object.
		/// </summary>
		/// <returns></returns>
		public override string ToString() {
			return string.Format("{0}(Offset {1}, Type {2}, Usage {3}{4}{5})", GetType().Name, Offset, Format, Usage, UsageIndex > 0 ? "+" + UsageIndex : "", Unknowns.ToCommaPrefixedList());
		}
	}
}
