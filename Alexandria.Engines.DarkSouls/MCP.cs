using Glare;
using Glare.Assets;
using Glare.Framework;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Alexandria.Engines.DarkSouls {
	public class MCP : Asset {
		internal const int HeaderDataSize = 0x10;
		internal const int Magic1 = 2;
		internal const int Magic2 = 0x4085D1;

		public ReadOnlyList<MCPTable1> Table1 { get; private set; }

		int Table1Offset, Table2Offset, EndOffset;

		public ReadOnlyList<MCPTable2> Table2 { get; private set; }

		public MCP(AssetManager manager, AssetLoader loader)
			: base(manager, loader.Name) {
			var reader = loader.Reader;

			EndOffset = checked((int)loader.Length);
			loader.Expect(Magic1 + 1);
			loader.Expect(Magic2);
			int table2Count = reader.ReadInt32();
			int table2Offset = Table2Offset = reader.ReadInt32();

			int table1Offset = Table1Offset = HeaderDataSize;
			int table1Count = (table2Offset - table1Offset) / MCPTable1.DataSize;

			reader.BaseStream.Position = table1Offset;
			var table1 = new RichList<MCPTable1>(table1Count);
			Table1 = table1;
			for (int index = 0; index < table1Count; index++)
				table1.Add(new MCPTable1(this, index, loader));

			reader.BaseStream.Position = table2Offset;
			var table2 = new RichList<MCPTable2>(table2Count);
			Table2 = table2;
			for (int index = 0; index < table2Count; index++)
				table2.Add(new MCPTable2(this, index, loader));
		}

		public override System.Windows.Forms.Control Browse() {
			var tree = new TreeView();

			var table1 = CreateTreeNode(tree, Table1, Table1Offset, Table2Offset);
			foreach (var item in Table1)
				table1.Nodes.Add(item.CreateTreeNode());

			var table2 = CreateTreeNode(tree, Table2, Table2Offset, EndOffset);
			foreach (var item in Table2)
				table2.Nodes.Add(item.CreateTreeNode());

			return tree;
		}

		TreeNode CreateTreeNode<T>(TreeView tree, IList<T> list, int offset, int endOffset) {
			if (list.Count == 0)
				return null;

			var node = new TreeNode(string.Format("{0} ({1} item{2} at {3:X}h to {4:X}h)", typeof(T).Name, list.Count, list.Count == 1 ? "" : "s", offset, endOffset));
			tree.Nodes.Add(node);
			return node;
		}

		internal RichList<MCPTable1> GetTable1Slice(int count, int offset) {
			if (offset < Table1Offset || offset + count * MCPTable1.DataSize > Table2Offset || (offset - Table1Offset) % MCPTable1.DataSize != 0)
				throw new InvalidDataException();
			int start = (offset - Table1Offset) / MCPTable1.DataSize;
			RichList<MCPTable1> table = new RichList<MCPTable1>(count);
			for (int index = 0; index < count; index++)
				table.Add(Table1[start + index]);
			return table;
		}

		internal static string Table1ToString(ICollection<MCPTable1> table) {
			return "(" + string.Join(", ", (from a in table select a.ToShortString())) + ")";
		}
	}

	public abstract class MCPTable {
		public int Index { get; private set; }

		public MCP MCP { get; private set; }

		public UnknownList Unknowns { get; private set; }

		public MCPTable(MCP mcp, int index) {
			MCP = mcp;
			Index = index;
			Unknowns = new UnknownList();
		}

		public string ToShortString() {
			return Unknowns.ToCommaSeparatedList();
		}

		public override string ToString() {
			var content = ToStringContent();
			return string.Format("{0}({1}{2})", GetType().Name, content, string.IsNullOrEmpty(content) ? Unknowns.ToCommaSeparatedList() : Unknowns.ToCommaPrefixedList());
		}

		protected virtual string ToStringContent() { return ""; }

		public virtual TreeNode CreateTreeNode() {
			return new TreeNode(ToString());
		}
	}

	public class MCPTable1 : MCPTable {
		internal const int DataSize = 4;

		public int Value { get; private set; }

		public MCPTable1(MCP mcp, int index, AssetLoader loader)
			: base(mcp, index) {
			Unknowns.ReadInt32s(loader.Reader, 1);
		}
	}

	public class MCPTable2 : MCPTable {
		internal const int DataSize = 40;

		public ReadOnlyList<MCPTable1> Table1U1 { get; private set; }

		public Box3f Box { get; private set; }

		public MCPTable2(MCP mcp, int index, AssetLoader loader)
			: base(mcp, index) {
			var reader = loader.Reader;

			Unknowns.ReadInt32s(reader, 1);
			loader.Expect(index);

			int count1 = reader.ReadInt32();
			int offset1 = reader.ReadInt32();
			Table1U1 = mcp.GetTable1Slice(count1, offset1);

			Box = loader.ReadCheckedAbsoluteBox3f();
		}

		protected override string ToStringContent() {
			return string.Format("{0}, {1}", MCP.Table1ToString(Table1U1), Box);
		}
	}

	public class MCPFormat : AssetFormat {
		public MCPFormat(Engine engine)
			: base(engine, typeof(MCP), canLoad: true, extension: ".mcp") {
		}

		public override LoadMatchStrength LoadMatch(AssetLoader loader) {
			var reader = loader.Reader;

			if (loader.Length < MCP.HeaderDataSize)
				return LoadMatchStrength.None;
			if ((reader.ReadInt32() != MCP.Magic1) || (reader.ReadInt32() != MCP.Magic2))
				return LoadMatchStrength.None;
			var table2Count = reader.ReadInt32();
			var table2Offset = reader.ReadInt32();

			int table2Size = loader.ShortLength - table2Offset;
			if (table2Size % MCPTable2.DataSize != 0 || table2Size / MCPTable2.DataSize != table2Count)
				return LoadMatchStrength.None;
			if (table2Offset < MCP.HeaderDataSize)
				return LoadMatchStrength.None;

			return LoadMatchStrength.Medium;
		}

		public override Asset Load(AssetLoader loader) {
			return new MCP(Manager, loader);
		}
	}
}
