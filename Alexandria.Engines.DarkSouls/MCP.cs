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
	/// <summary>An MCP file.</summary>
	public class MCP : Asset {
		internal const int HeaderDataSize = 0x10;
		internal const int Magic1 = 2;
		internal const int Magic2 = 0x4085D1;

		/// <summary>Get the first table of <see cref="MCPTable1"/> rows.</summary>
		public Codex<MCPTable1> Table1 { get; private set; }

		int Table1Offset, Table2Offset, EndOffset;

		/// <summary>Get the second list of <see cref="MCPTable2"/> rows.</summary>
		public Codex<MCPTable2> Table2 { get; private set; }

		internal MCP(AssetManager manager, AssetLoader loader)
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
			var table1 = new Codex<MCPTable1>(table1Count);
			Table1 = table1;
			for (int index = 0; index < table1Count; index++)
				table1.Add(new MCPTable1(this, index, loader));

			reader.BaseStream.Position = table2Offset;
			var table2 = new Codex<MCPTable2>(table2Count);
			Table2 = table2;
			for (int index = 0; index < table2Count; index++)
				table2.Add(new MCPTable2(this, index, loader));
		}

		/// <summary>Create a control to browse the <see cref="MCP"/>.</summary>
		/// <returns></returns>
		public override System.Windows.Forms.Control Browse(Action<double> progressUpdateCallback = null) {
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

		internal Codex<MCPTable1> GetTable1Slice(int count, int offset) {
			if (offset < Table1Offset || offset + count * MCPTable1.DataSize > Table2Offset || (offset - Table1Offset) % MCPTable1.DataSize != 0)
				throw new InvalidDataException();
			int start = (offset - Table1Offset) / MCPTable1.DataSize;
			Codex<MCPTable1> table = new Codex<MCPTable1>(count);
			for (int index = 0; index < count; index++)
				table.Add(Table1[start + index]);
			return table;
		}

		internal static string Table1ToString(ICollection<MCPTable1> table) {
			return "(" + string.Join(", ", (from a in table select a.ToShortString())) + ")";
		}
	}

	/// <summary>
	/// A table row in an <see cref="MCP"/>.
	/// </summary>
	public abstract class MCPTable {
		/// <summary>
		/// Zero-based index of this row.
		/// </summary>
		public int Index { get; private set; }

		/// <summary>
		/// Get the <see cref="MCP"/> this is contained in.
		/// </summary>
		public MCP MCP { get; private set; }

		/// <summary>
		/// Get the collection of unknown values.
		/// </summary>
		public UnknownList Unknowns { get; private set; }

		internal MCPTable(MCP mcp, int index) {
			MCP = mcp;
			Index = index;
			Unknowns = new UnknownList();
		}

		/// <summary>Get a short string representation of the object.</summary>
		/// <returns></returns>
		public string ToShortString() {
			return Unknowns.ToCommaSeparatedList();
		}

		/// <summary>
		/// Get a string representation of the object.
		/// </summary>
		/// <returns></returns>
		public override string ToString() {
			var content = ToStringContent();
			return string.Format("{0}({1}{2})", GetType().Name, content, string.IsNullOrEmpty(content) ? Unknowns.ToCommaSeparatedList() : Unknowns.ToCommaPrefixedList());
		}

		/// <summary>
		/// Get a string representation of the content of the object.
		/// </summary>
		/// <returns></returns>
		protected virtual string ToStringContent() { return ""; }

		/// <summary>
		/// Create a tree node for this object.
		/// </summary>
		/// <returns></returns>
		public virtual TreeNode CreateTreeNode() {
			return new TreeNode(ToString());
		}
	}

	/// <summary>
	/// A table row in an <see cref="MCP"/>.
	/// </summary>
	public class MCPTable1 : MCPTable {
		internal const int DataSize = 4;

		/// <summary>
		/// The value of the row.
		/// </summary>
		public int Value { get; private set; }

		internal MCPTable1(MCP mcp, int index, AssetLoader loader)
			: base(mcp, index) {
			Unknowns.ReadInt32s(loader.Reader, 1);
		}
	}

	/// <summary>
	/// A table row in an <see cref="MCP"/>.
	/// </summary>
	public class MCPTable2 : MCPTable {
		internal const int DataSize = 40;

		/// <summary>
		/// Get a list of <see cref="MCPTable1"/> indices.
		/// </summary>
		public Codex<MCPTable1> Table1U1 { get; private set; }

		/// <summary>
		/// Get the bounding box.
		/// </summary>
		public Box3f Box { get; private set; }

		internal MCPTable2(MCP mcp, int index, AssetLoader loader)
			: base(mcp, index) {
			var reader = loader.Reader;

			Unknowns.ReadInt32s(reader, 1);
			loader.Expect(index);

			int count1 = reader.ReadInt32();
			int offset1 = reader.ReadInt32();
			Table1U1 = mcp.GetTable1Slice(count1, offset1);

			Box = loader.ReadCheckedAbsoluteBox3f();
		}

		/// <summary>
		/// Get a string representation of the content.
		/// </summary>
		/// <returns></returns>
		protected override string ToStringContent() {
			return string.Format("{0}, {1}", MCP.Table1ToString(Table1U1), Box);
		}
	}

	/// <summary>
	/// <see cref="AssetFormat"/> for the <see cref="MCP"/> type.
	/// </summary>
	public class MCPFormat : AssetFormat {
		internal MCPFormat(Engine engine)
			: base(engine, typeof(MCP), canLoad: true, extension: ".mcp") {
		}

		/// <summary>
		/// Attempt to match the loader as a <see cref="MCP"/>.
		/// </summary>
		/// <param name="loader"></param>
		/// <returns></returns>
		public override LoadMatchStrength LoadMatch(AssetLoader loader) {
			var reader = loader.Reader;

			if (loader.Length < MCP.HeaderDataSize || loader.Length > int.MaxValue)
				return LoadMatchStrength.None;
			if ((reader.ReadInt32() != MCP.Magic1) || (reader.ReadInt32() != MCP.Magic2))
				return LoadMatchStrength.None;
			var table2Count = reader.ReadInt32();
			var table2Offset = reader.ReadInt32();

			int table2Size = loader.CheckedShortLength - table2Offset;
			if (table2Size % MCPTable2.DataSize != 0 || table2Size / MCPTable2.DataSize != table2Count)
				return LoadMatchStrength.None;
			if (table2Offset < MCP.HeaderDataSize)
				return LoadMatchStrength.None;

			return LoadMatchStrength.Medium;
		}

		/// <summary>
		/// Load the <see cref="MCP"/>.
		/// </summary>
		/// <param name="loader"></param>
		/// <returns></returns>
		public override Asset Load(AssetLoader loader) {
			return new MCP(Manager, loader);
		}
	}
}
