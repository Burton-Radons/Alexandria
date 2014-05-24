using Glare;
using Glare.Framework;
using Glare.Graphics.Loaders;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.DarkSouls {
	public class DSModelVertexDeclaration : DSModelObject {
		#region Internal

		internal const int DataSize = 4 * 4;

		#endregion Internal

		public Codex<DSModelVertexChannel> Channels { get; private set; }

		public DSModelVertexChannel PositionChannel {
			get {
				foreach (DSModelVertexChannel channel in Channels)
					if (channel.Usage == DSModelVertexUsage.Position && channel.Set == 0)
						return channel;
				throw new InvalidDataException("There is no position channel somehow.");
			}
		}

		/// <summary>Count the number of values in all of the channels.</summary>
		public int ValueCount {
			get {
				int count = 0;
				foreach (DSModelVertexChannel channel in Channels)
					count += channel.VertexOrder;
				return count;
			}
		}

		internal DSModelVertexDeclaration(DSModel model, int index, BinaryReader reader)
			: base(model, index) {
			int count = reader.ReadInt32();
			reader.RequireZeroes(4 * 2);
			int offset = reader.ReadInt32();
			long reset = reader.BaseStream.Position;

			reader.BaseStream.Position = offset;
			var blocks = new Codex<DSModelVertexChannel>();
			Channels = blocks;
			for (int blockIndex = 0; blockIndex < count; blockIndex++)
				blocks.Add(new DSModelVertexChannel(this, reader, blockIndex));

			reader.BaseStream.Position = reset;
		}
	}

	public class DSModelVertexChannel : DSModelObject {
		#region Internal

		internal const int DataSize = 4 * 5;

		#endregion Internal

		#region Properties

		public DSModelVertexDeclaration Declaration { get; private set; }

		public int Offset { get; private set; }

		public DSModelVertexFormat Format { get; private set; }

		public DSModelVertexUsage Usage { get; private set; }

		public int Set { get; private set; }

		/// <summary>Get the index of the first vertex axis</summary>
		public int VertexStartOffset { get { return Index > 0 ? Declaration.Channels[Index - 1].ValueEndOffset : 0; } }

		public int ValueEndOffset { get { return VertexStartOffset + VertexOrder; } }

		public string VertexId { get { return Usage + (Set > 0 ? Set.ToString() : ""); } }

		/// <summary>Get the number of vertex axes in the channel.</summary>
		public int VertexOrder {
			get {
				switch (Format) {
					case DSModelVertexFormat.Vector2ns1024:
						return 2;

					case DSModelVertexFormat.Vector3f:
						return 3;

					case DSModelVertexFormat.Vector4abgr:
					case DSModelVertexFormat.Vector4b:
					case DSModelVertexFormat.Vector4ns:
					case DSModelVertexFormat.Vector4ns1024:
					case DSModelVertexFormat.Vector4nsb:
					case DSModelVertexFormat.Vector4nsb2:
						return 4;

					default:
						throw new NotImplementedException();
				}
			}
		}

		#endregion Properties

		internal DSModelVertexChannel(DSModelVertexDeclaration declaration, BinaryReader reader, int index)
			: base(declaration.Model, index) {
			Declaration = declaration;
			reader.RequireZeroes(4);
			Offset = reader.ReadInt32();
			Format = (DSModelVertexFormat)reader.ReadInt32();
			Usage = (DSModelVertexUsage)reader.ReadInt32();
			Set = reader.ReadInt32();
		}

		public Vector4f Read(BinaryReader reader, ref int offset) {
			Vector4f value;
			int size;

			if (Offset != offset)
				throw new Exception();

			switch (Format) {
				case DSModelVertexFormat.Vector3f:
					size = 12;
					value.X = reader.ReadSingle();
					value.Y = reader.ReadSingle();
					value.Z = reader.ReadSingle();
					value.W = 1;
					break;

				case DSModelVertexFormat.Vector4abgr:
					size = 4;
					value.W = reader.ReadByte() / 255f;
					value.Z = reader.ReadByte() / 255f;
					value.Y = reader.ReadByte() / 255f;
					value.X = reader.ReadByte() / 255f;
					break;

				case DSModelVertexFormat.Vector4b:
					size = 4;
					value.X = reader.ReadByte();
					value.Y = reader.ReadByte();
					value.Z = reader.ReadByte();
					value.W = reader.ReadByte();
					break;

				case DSModelVertexFormat.Vector4nsb:
				case DSModelVertexFormat.Vector4nsb2:
					size = 4;
					value.X = reader.ReadSByte() / 127f;
					value.Y = reader.ReadSByte() / 127f;
					value.Z = reader.ReadSByte() / 127f;
					value.W = reader.ReadSByte() / 127f;
					break;

				case DSModelVertexFormat.Vector2ns1024:
					size = 4;
					value.X = reader.ReadInt16() / 1024f;
					value.Y = reader.ReadInt16() / 1024f;
					value.Z = value.W = 0;
					break;

				case DSModelVertexFormat.Vector4ns1024:
					size = 8;
					value.X = reader.ReadInt16() / 1024f;
					value.Y = reader.ReadInt16() / 1024f;
					value.Z = reader.ReadInt16() / 1024f;
					value.W = reader.ReadInt16() / 1024f;
					break;

				case DSModelVertexFormat.Vector4ns:
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
			return string.Format("{0}(Offset {1}, Type {2}, Usage {3}{4})", GetType().Name, Offset, Format, Usage, Set > 0 ? "+" + Set : "");
		}
	}

	/// <summary>Partially from D3DDECLTYPE, but extended and changed.</summary>
	public enum DSModelVertexFormat : int {
		//Single = 0,
		//Vector2f = 1,
		Vector3f = 2,
		/*Vector4f = 3,
		Vector4rgba = 4,
		Vector4b = 5,
		Vector2s = 6,
		Vector4s = 7,
		Vector4nb = 8,
		Vector2ns = 9,
		Vector4ns = 10,
		Vector2nus = 11,
		Vector4nus = 12,
		Vector3u10 = 13,
		Vector3ns10 = 14,
		Vector2h = 15,
		Vector4h = 16,
		Unused = 17,*/

		/// <summary>DS2; 4-byte used as color. Guessing at the layout.</summary>
		Vector4abgr = 16,

		/// <summary>Used for blend indices.</summary>
		Vector4b = 17,

		/// <summary>Used for normals, tangents, colors (which always seem to be transparent).</summary>
		Vector4nsb = 19,

		/// <summary>4-byte stored as 6.10 fixed point, used for texture coordinates.</summary>
		Vector2ns1024 = 21,

		/// <summary>8-byte stored as 6.10 fixed point, used for texture coordinates in world geometry. Probably X and Y are used for diffuse/normal/specular textures and Z and W are used for light maps.</summary>
		Vector4ns1024 = 22,

		/// <summary>Used for blend weights.</summary>
		Vector4ns = 26,

		/// <summary>DS2; used by normal, blend indices, tangent.</summary>
		Vector4nsb2 = 47,
	}

	/// <summary>From D3DDECLUSAGE.</summary>
	public enum DSModelVertexUsage : int {
		Position = 0,
		BlendWeight = 1,
		BlendIndices = 2,
		Normal = 3,
		PointSize = 4,
		TextureCoordinate = 5,
		Tangent = 6,
		Binormal = 7,
		TesselationFactor = 8,
		TransformedPosition = 9,
		Color = 10,
		Fog = 11,
		Depth = 12,
		Sample = 13,
	}
}
