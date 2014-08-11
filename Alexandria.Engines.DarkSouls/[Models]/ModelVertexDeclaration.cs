using Glare;
using Glare.Assets;
using Glare.Framework;
using Glare.Graphics;
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
	/// <summary>
	/// Describes the layout of a vertex.
	/// </summary>
	public class ModelVertexDeclaration : ModelAsset {
		#region Internal

		internal const int DataSize = 4 * 4;

		#endregion Internal

		/// <summary>
		/// Get the list of attributes in the vertex.
		/// </summary>
		public Codex<ModelVertexAttribute> Attributes { get; private set; }

		/// <summary>
		/// Find the position attribute and return it.
		/// </summary>
		public ModelVertexAttribute PositionAttribute {
			get {
				foreach (ModelVertexAttribute channel in Attributes)
					if (channel.Usage == ModelVertexUsage.Position && channel.UsageIndex == 0)
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

		internal ModelVertexDeclaration(FolderAsset folder, int index, AssetLoader loader)
			: base(folder, index, loader) {
			var reader = loader.Reader;

			int count = reader.ReadInt32();
			reader.RequireZeroes(4 * 2);
			int offset = reader.ReadInt32();
			long reset = reader.BaseStream.Position;

			reader.BaseStream.Position = offset;
			var blocks = new Codex<ModelVertexAttribute>();
			Attributes = blocks;
			for (int blockIndex = 0; blockIndex < count; blockIndex++)
				blocks.Add(new ModelVertexAttribute(this, loader, blockIndex));

			reader.BaseStream.Position = reset;
		}

		/// <summary>
		/// Bind the vertex declaration to the program.
		/// </summary>
		/// <param name="program"></param>
		/// <param name="mesh"></param>
		/// <param name="stride"></param>
		public void Bind(ModelProgram program, ModelMesh mesh, int stride) {
			GraphicsBuffer buffer = Model.Buffer;

			foreach (ModelVertexAttribute attribute in Attributes) {
				int offset = attribute.Offset + mesh.BufferDataOffset;
				ProgramAttribute programAttribute = attribute.MatchAttribute(program);

				if (programAttribute != null)
					programAttribute.Bind(buffer, offset, attribute.GraphicsFormat, stride);
			}
		}

		/// <summary>
		/// Get a string representation of the object.
		/// </summary>
		/// <returns></returns>
		public override string ToString() {
			return string.Format("{0}(AttributeCount {1}{2})", GetType().Name, Attributes.Count, Unknowns.ToCommaPrefixedList());
		}
	}

	/// <summary>Partially from D3DDECLTYPE, but extended and changed.</summary>
	public enum ModelVertexFormat : int {
		//Single = 0,
		//Vector2f = 1,
		/// <summary>Three-dimensional single-precision floating point vector.</summary>
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
		/// <summary>Used as the vertex's position.</summary>
		Position = 0,

		/// <summary>Gives the weight of each bone in the transform.</summary>
		BlendWeight = 1,

		/// <summary>Gives the indices of the bones in the transform.</summary>
		BlendIndices = 2,

		/// <summary>Provides the normal.</summary>
		Normal = 3,

		/// <summary>Provides the size of a point.</summary>
		PointSize = 4,

		/// <summary>Provides a texture coordinate.</summary>
		TextureCoordinate = 5,

		/// <summary>Provides a normal tangent.</summary>
		Tangent = 6,

		/// <summary>Provides a binormal.</summary>
		Binormal = 7,

		/// <summary>Provides a tesselation factor.</summary>
		TesselationFactor = 8,

		/// <summary>Provides the transformed position.</summary>
		TransformedPosition = 9,

		/// <summary>Provides the colour.</summary>
		Color = 10,

		/// <summary>Provides a fog value.</summary>
		Fog = 11,

		/// <summary>Provides a depth value.</summary>
		Depth = 12,

		/// <summary>Provides a value.</summary>
		Sample = 13,
	}
}
