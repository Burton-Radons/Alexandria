using Glare;
using Glare.Assets;
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
	public class ModelVertexDeclaration : ModelObject {
		#region Internal

		internal const int DataSize = 4 * 4;

		#endregion Internal

		public Codex<ModelVertexAttribute> Attributes { get; private set; }

		public ModelVertexAttribute PositionChannel {
			get {
				foreach (ModelVertexAttribute channel in Attributes)
					if (channel.Usage == ModelVertexUsage.Position && channel.Set == 0)
						return channel;
				throw new InvalidDataException("There is no position channel somehow.");
			}
		}

		/// <summary>Count the number of values in all of the channels.</summary>
		public int ValueCount {
			get {
				int count = 0;
				foreach (ModelVertexAttribute channel in Attributes)
					count += channel.VertexOrder;
				return count;
			}
		}

		internal ModelVertexDeclaration(FolderAsset folder, int index, BinaryReader reader)
			: base(folder, index, reader) {
			int count = reader.ReadInt32();
			reader.RequireZeroes(4 * 2);
			int offset = reader.ReadInt32();
			long reset = reader.BaseStream.Position;

			reader.BaseStream.Position = offset;
			var blocks = new Codex<ModelVertexAttribute>();
			Attributes = blocks;
			for (int blockIndex = 0; blockIndex < count; blockIndex++)
				blocks.Add(new ModelVertexAttribute(this, reader, blockIndex));

			reader.BaseStream.Position = reset;
		}

		public override string ToString() {
			return string.Format("{0}(at {1:X}h, AttributeCount {2}{3})", GetType().Name, DataOffset, Attributes.Count, Unknowns.ToCommaPrefixedList());
		}
	}

	/// <summary>Partially from D3DDECLTYPE, but extended and changed.</summary>
	public enum ModelVertexFormat : int {
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
	public enum ModelVertexUsage : int {
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
