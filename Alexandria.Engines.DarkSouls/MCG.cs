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
	/// <summary>An MCG object.</summary>
	public class MCG : Asset {
		internal const int HeaderDataSize = 32;

		int Table1Offset, Table2Offset, Table3Offset, EndOffset;

		/// <summary>Get the first table.</summary>
		public Codex<MCGTable1> Table1 { get; private set; }

		/// <summary>Get the second table.</summary>
		public Codex<MCGTable2> Table2 { get; private set; }

		/// <summary>Get the third table.</summary>
		public Codex<MCGTable3> Table3 { get; private set; }

		internal MCG(AssetManager manager, AssetLoader loader)
			: base(manager, loader.Name) {
			var reader = loader.Reader;

			EndOffset = checked((int)loader.Length);
			loader.Expect(1);
			loader.Expect(0);
			int table3Count = reader.ReadInt32();
			int table3Offset = Table3Offset = reader.ReadInt32();
			int table2Count = reader.ReadInt32();
			int table2Offset = Table2Offset = reader.ReadInt32();
			loader.Expect(0);
			loader.Expect(0);

			int table1Offset = Table1Offset = HeaderDataSize;
			int table1Count = (table2Offset - table1Offset) / MCGTable1.DataSize;

			reader.BaseStream.Position = table1Offset;
			var table1 = new Codex<MCGTable1>(table1Count);
			Table1 = table1;
			for (int index = 0; index < table1Count; index++)
				table1.Add(new MCGTable1(this, index, loader));

			reader.BaseStream.Position = table2Offset;
			var table2 = new Codex<MCGTable2>(table2Count);
			Table2 = table2;
			for (int index = 0; index < table2Count; index++)
				table2.Add(new MCGTable2(this, index, loader));

			reader.BaseStream.Position = table3Offset;
			var table3 = new Codex<MCGTable3>(table3Count);
			Table3 = table3;
			for (int index = 0; index < table3Count; index++)
				table3.Add(new MCGTable3(this, index, loader));
		}

		/// <summary>Create a control to browse the <see cref="MCG"/>.</summary>
		/// <returns></returns>
		public override System.Windows.Forms.Control Browse(Action<double> progressUpdateCallback = null) {
			var tree = new TreeView();

			var table1 = CreateTreeNode(tree, Table1, Table1Offset, Table2Offset);
			foreach (var item in Table1)
				table1.Nodes.Add(item.CreateTreeNode());

			var table2 = CreateTreeNode(tree, Table2, Table2Offset, Table3Offset);
			foreach (var item in Table2)
				table2.Nodes.Add(item.CreateTreeNode());

			var table3 = CreateTreeNode(tree, Table3, Table3Offset, EndOffset);
			foreach (var item in Table3)
				table3.Nodes.Add(item.CreateTreeNode());

			return tree;
		}

		TreeNode CreateTreeNode<T>(TreeView tree, IList<T> list, int offset, int endOffset) {
			if(list.Count == 0)
				return null;

			var node = new TreeNode(string.Format("{0} ({1} item{2} at {3:X}h to {4:X}h)", typeof(T).Name, list.Count, list.Count == 1 ? "" : "s", offset, endOffset));
			tree.Nodes.Add(node);
			return node;
		}

		internal Codex<MCGTable1> GetTable1Slice(int count, int offset) {
			if (offset < Table1Offset || offset + count * MCGTable1.DataSize > Table2Offset || (offset - Table1Offset) % MCGTable1.DataSize != 0)
				throw new InvalidDataException();
			int start = (offset - Table1Offset) / MCGTable1.DataSize;
			Codex<MCGTable1> table = new Codex<MCGTable1>(count);
			for (int index = 0; index < count; index++)
				table.Add(Table1[start + index]);
			return table;
		}

		internal static string Table1ToString(ICollection<MCGTable1> table) {
			return "(" + string.Join(", ", (from a in table select a.ToShortString())) + ")";
		}
	}

	/// <summary>A table within an <see cref="MCG"/>.</summary>
	public abstract class MCGTable {
		/// <summary>Get the zero-based index of the <see cref="MCGTable"/>.</summary>
		public int Index { get; private set; }

		/// <summary>Get the containing <see cref="MCG"/>.</summary>
		public MCG MCG { get; private set; }

		/// <summary>Get the list of unknown values.</summary>
		public UnknownList Unknowns { get; private set; }

		internal MCGTable(MCG mcg, int index) {
			MCG = mcg;
			Index = index;
			Unknowns = new UnknownList();
		}

		/// <summary>Convert to a short string.</summary>
		/// <returns></returns>
		public string ToShortString() {
			return Unknowns.ToCommaSeparatedList();
		}

		/// <summary>Convert to a full string.</summary>
		/// <returns></returns>
		public override string ToString() {
			var content = ToStringContent();
			return string.Format("{0}({1}{2})", GetType().Name, content, string.IsNullOrEmpty(content) ? Unknowns.ToCommaSeparatedList() : Unknowns.ToCommaPrefixedList());
		}

		/// <summary>Convert to a small string.</summary>
		/// <returns></returns>
		protected virtual string ToStringContent() { return ""; }

		/// <summary>Create a tree node.</summary>
		/// <returns></returns>
		public virtual TreeNode CreateTreeNode() {
			return new TreeNode(ToString());
		}
	}

	/// <summary>A table row in an <see cref="MCG"/>.</summary>
	public class MCGTable1 : MCGTable {
		internal const int DataSize = 4;

		/// <summary>Get the value of the row.</summary>
		public int Value { get; private set; }

		internal MCGTable1(MCG mcg, int index, AssetLoader loader)
			: base(mcg, index) {
			Unknowns.ReadInt32s(loader.Reader, 1);
		}
	}

	/// <summary>A table row in an <see cref="MCG"/>.</summary>
	public class MCGTable2 : MCGTable {
		internal const int DataSize = 36;

		/// <summary>Get the list of references to <see cref="MCGTable1"/> elements.</summary>
		public Codex<MCGTable1> Table1U1 { get; private set; }

		/// <summary>Get the second list of references to <see cref="MCGTable1"/> elements.</summary>
		public Codex<MCGTable1> Table1U2 { get; private set; }

		internal MCGTable2(MCG mcg, int index, AssetLoader loader)
			: base(mcg, index) {
			var reader = loader.Reader;

			Unknowns.ReadInt32s(reader, 1);

			int count1 = reader.ReadInt32();
			int offset1 = reader.ReadInt32();
			Table1U1 = mcg.GetTable1Slice(count1, offset1);

			Unknowns.ReadInt32s(reader, 1);

			int count2 = reader.ReadInt32();
			int offset2 = reader.ReadInt32();
			Table1U2 = mcg.GetTable1Slice(count2, offset2);

			Unknowns.ReadInt32s(reader, 2);

			Unknowns.ReadSingles(reader, 1);
		}

		/// <summary>Get a string representation of the row's contents.</summary>
		/// <returns></returns>
		protected override string ToStringContent() {
			return string.Format("{0}, {1}", MCG.Table1ToString(Table1U1), MCG.Table1ToString(Table1U2));
		}
	}

	/// <summary>A table in an <see cref="MCG"/>.</summary>
	public class MCGTable3 : MCGTable {
		internal const int DataSize = 32;

		/// <summary>Get a table referencing <see cref="MCGTable1"/> values.</summary>
		public Codex<MCGTable1> Table1U1 { get; private set; }

		/// <summary>Get a table referencing <see cref="MCGTable1"/> values.</summary>
		public Codex<MCGTable1> Table1U2 { get; private set; }

		/// <summary>Get a position vector.</summary>
		public Vector3f Position { get; private set; }

		internal MCGTable3(MCG mcg, int index, AssetLoader loader)
			: base(mcg, index) {
			var reader = loader.Reader;

			int count = reader.ReadInt32();
			Position = reader.ReadVector3f();
			Table1U1 = mcg.GetTable1Slice(count, reader.ReadInt32());
			Table1U2 = mcg.GetTable1Slice(count, reader.ReadInt32());
			Unknowns.ReadInt32s(reader, 2);
		}

		/// <summary>Get a string representation of the row's values.</summary>
		/// <returns></returns>
		protected override string ToStringContent() {
			return string.Format("{0}, {1}, {2}", MCG.Table1ToString(Table1U1), MCG.Table1ToString(Table1U2), Position.ToShortString());
		}
	}

	/// <summary>The asset format for the <see cref="MCG"/>.</summary>
	public class MCGFormat : AssetFormat {
		internal MCGFormat(Engine engine)
			: base(engine, typeof(MCG), canLoad: true, extension: ".mcg") {
		}

		/// <summary>Attempt to match the stream as an <see cref="MCG"/> file.</summary>
		/// <param name="loader"></param>
		/// <returns></returns>
		public override LoadMatchStrength LoadMatch(AssetLoader loader) {
			var reader = loader.Reader;

			if (loader.Length < MCG.HeaderDataSize)
				return LoadMatchStrength.None;
			if ((reader.ReadInt32() != 1) || (reader.ReadInt32() != 0))
				return LoadMatchStrength.None;
			var table3Count = reader.ReadInt32();
			var table3Offset = reader.ReadInt32();
			var table2Count = reader.ReadInt32();
			var table2Offset = reader.ReadInt32();
			if ((reader.ReadInt32() != 0) || (reader.ReadInt32() != 0))
				return LoadMatchStrength.None;

			long table3Size = loader.Length - table3Offset;
			if (table3Size % MCGTable3.DataSize != 0 || table3Size / MCGTable3.DataSize != table3Count)
				return LoadMatchStrength.None;

			int table2Size = table3Offset - table2Offset;
			if (table2Size % MCGTable2.DataSize != 0 || table2Size / MCGTable2.DataSize != table2Count)
				return LoadMatchStrength.None;
			if (table2Offset < MCG.HeaderDataSize)
				return LoadMatchStrength.None;

			return LoadMatchStrength.Medium;
		}

		/// <summary>Load the MCG.</summary>
		/// <param name="loader"></param>
		/// <returns></returns>
		public override Asset Load(AssetLoader loader) {
			return new MCG(Manager, loader);
		}
	}
}
