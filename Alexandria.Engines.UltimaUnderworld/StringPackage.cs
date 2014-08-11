using Glare.Assets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.UltimaUnderworld {
	public class StringPackage : ArchiveAsset {
		internal struct Node {
			public byte Value;
			public byte Parent;
			public byte Left, Right;
			bool Checked;

			public Node(BinaryReader reader) {
				Value = reader.ReadByte();
				Parent = reader.ReadByte();
				Left = reader.ReadByte();
				Right = reader.ReadByte();
				Checked = false;
			}

			public bool CheckValidity(int index, Node[] nodes) {
				if (Checked)
					return false;
				Checked = true;
				return (Parent != 255 || index == nodes.Length - 1) && // Only the root node has no parent.
					(Parent > index) && // No self-parenting, parents come from later nodes.
					((Left != 0xFF && Right != 0xFF) || (Left == 0xFF && Right == 0xFF)) && // Both branches must be 0xFF if one is.
					(Left == 0xFF || (Left < index && nodes[Left].CheckValidity(Left, nodes))) && // Left branch if present is earlier and is also valid.
					(Right == 0xFF || (Right < index && nodes[Right].CheckValidity(Right, nodes))) // Right branch if present is earlier and is also valid.
					;
			}
		}

		struct Block {
			public ushort Id;
			public uint Offset;

			public Block(BinaryReader reader) { Id = reader.ReadUInt16(); Offset = reader.ReadUInt32(); }
		}

		internal StringPackage(AssetLoader loader)
			: base(loader) {
			BinaryReader reader = loader.Reader;
			Node[] nodes = ReadNodes(reader);
			Block[] blocks = new Block[reader.ReadUInt16()];

			for (int index = 0; index < blocks.Length; index++)
				blocks[index] = new Block(reader);
			for (int index = 0; index < blocks.Length; index++)
				new StringBlock(this, blocks[index].Id, reader, blocks[index].Offset, nodes);
		}

		internal static Node[] ReadNodes(BinaryReader reader) {
			long length = reader.BaseStream.Length;
			if (length < 2)
				return null;

			ushort nodeCount = reader.ReadUInt16();

			if (nodeCount > 256 || nodeCount < 16 || length < 2 + nodeCount * 4)
				return null;

			Node[] nodes = new Node[nodeCount];
			for (int index = 0; index < nodeCount; index++)
				nodes[index] = new Node(reader);
			return nodes;
		}
	}

	public class StringBlock : FolderAsset {
		public int Id { get; private set; }

		internal StringBlock(StringPackage package, int id, BinaryReader reader, uint offset, StringPackage.Node[] nodes)
			: base(package, id.ToString()) {
			Id = id;

			reader.BaseStream.Position = offset;
			int count = reader.ReadUInt16();
			uint[] offsets = new uint[count];
			char[] buffer = new char[256 * 256];

			for (int index = 0; index < count; index++)
				offsets[index] = (uint)(offset + 2 + 2 * count + reader.ReadUInt16());
			for (int index = 0; index < count; index++)
				new StringValue(this, index, offsets[index], reader, nodes, buffer);
		}
	}

	public class StringValue : Asset {
		public override string DisplayName { get { return Index + ": " + Name; } }

		public int Index { get; private set; }

		internal StringValue(StringBlock block, int index, uint offset, BinaryReader reader, StringPackage.Node[] nodes, char[] buffer)
			: base(block, "") {
			Index = index;

			reader.BaseStream.Position = offset;

			int bitCount = 0, bits = 0;
			int length = 0;

			while (true) {
				StringPackage.Node node = nodes[nodes.Length - 1];

				while (node.Left != 0xFF) {
					if (bitCount == 0) {
						bits = reader.ReadByte();
						bitCount += 8;
					}

					if ((bits & 0x80) != 0)
						node = nodes[node.Right];
					else
						node = nodes[node.Left];

					bits <<= 1;
					bitCount--;
				}

				char value = (char)node.Value;
				if (value == '|')
					break;
				buffer[length++] = value;
			}

			Name = new string(buffer, 0, length);
		}
	}

	public class StringPackageFormat : AssetFormat {
		public StringPackageFormat(Engine engine)
			: base(engine, typeof(StringPackage), canLoad: true, extension: ".pak") {
		}

		public override LoadMatchStrength LoadMatch(AssetLoader loader) {
			StringPackage.Node[] nodes = StringPackage.ReadNodes(loader.Reader);
			if (nodes == null || !nodes[nodes.Length - 1].CheckValidity(nodes.Length - 1, nodes))
				return LoadMatchStrength.None;

			return LoadMatchStrength.Medium;
		}

		public override Asset Load(AssetLoader loader) {
			return new StringPackage(loader);
		}
	}
}
