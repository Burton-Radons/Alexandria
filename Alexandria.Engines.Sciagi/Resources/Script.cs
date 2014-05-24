using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glare;
using Glare.Internal;
using Glare.Assets;
using Glare.Framework;
using System.Windows.Forms;

namespace Alexandria.Engines.Sciagi.Resources {
	public class Script : ResourceData {
		Codex<ScriptSection> blocks = new Codex<ScriptSection>();

		public Codex<ScriptSection> Blocks { get { return blocks; } }

		public Script(AssetLoader loader)
			: base(loader.Context as Resource) {
			ScriptSection block;

			while ((block = ScriptSection.Read(this, loader)) != null)
				blocks.Add(block);
		}

		public override Control Browse() {
			TreeView tree = new TreeView() {
			};

			foreach (ScriptSection block in blocks)
				tree.Nodes.Add(block.ToTreeNode());

			return tree;
		}
	}

	public struct ScriptBlock {
		public long ContentOffset { get { return HeaderOffset + (Type == ScriptBlockType.End ? 2 : 4); } }
		public int ContentSize { get { return TotalSize > 0 ? TotalSize - 4 : 0; } }
		public long EndOffset { get { return HeaderOffset + TotalSize; } }
		public readonly long HeaderOffset;
		public readonly int TotalSize;
		public readonly ScriptBlockType Type;

		public ScriptBlock(AssetLoader loader) {
			HeaderOffset = loader.Position;
			Type = (ScriptBlockType)loader.Reader.ReadUInt16();

			if (Type == ScriptBlockType.End)
				TotalSize = 2;
			else {
				TotalSize = loader.Reader.ReadUInt16();
				if (TotalSize < 4) {
					loader.AddError(HeaderOffset, "Invalid block size in script resource; terminating reading.");
					TotalSize = 2;
					Type = ScriptBlockType.End;
				}
			}
		}
	}

	public abstract class ScriptSection {
		public ScriptBlock Block { get; private set; }

		public Script Script { get; private set; }

		public ScriptSection(Script script, ScriptBlock block) {
			Block = block;
			Script = script;
		}

		public static ScriptSection Read(Script script, AssetLoader loader) {
			ScriptBlock block = new ScriptBlock(loader);

			switch (block.Type) {
				case ScriptBlockType.End: return null;
				case ScriptBlockType.Locals: return new Locals(script, block, loader);
				case ScriptBlockType.Object: return new Object(script, block, loader);
				case ScriptBlockType.Strings: return new Strings(script, block, loader);
				default: return new Unknown(script, block, loader);
			}
		}

		public override string ToString() {
			return string.Format("{0} ({1} byte(s))", Block.Type, Block.ContentSize);
		}

		public abstract TreeNode ToTreeNode();

		public class Locals : ScriptSection {
			public int Count { get { return Values.Count; } }
			public Codex<ushort> Values { get; private set; }
			public ushort this[int index] { get { return Values[index]; } }

			public Locals(Script script, ScriptBlock block, AssetLoader loader)
				: base(script, block) {
				Codex<ushort> values = new Codex<ushort>(block.ContentSize / 2);
				Values = values;
				for (int index = 0; index < block.ContentSize / 2; index++)
					values.Add(loader.Reader.ReadUInt16());
			}

			public override string ToString() {
				return string.Format("{0}({1} local(s))", GetType().Name, Count);
			}

			public override TreeNode ToTreeNode() {
				TreeNode node = new TreeNode(ToString());
				for (int index = 0; index < Count; index++)
					node.Nodes.Add(new TreeNode(index + ". " + this[index]));
				return node;
			}
		}

		public class Object : ScriptSection {
			public const int SpeciesSelectorIndex = 0;
			public const int SuperClassSelectorIndex = 1;
			public const int InfoSelectorIndex = 2;
			public const int NameSelectorIndex = 3;

			public Codex<FunctionSelector> Functions { get; private set; }

			public VariableSelector Info { get { return Variables[InfoSelectorIndex]; } }

			public int LocalVariableOffset { get; private set; }

			public VariableSelector Name { get { return Variables[NameSelectorIndex]; } }

			public VariableSelector Species { get { return Variables[SpeciesSelectorIndex]; } }

			public VariableSelector SuperClass { get { return Variables[SuperClassSelectorIndex]; } }

			public Codex<VariableSelector> Variables { get; private set; }

			public Object(Script script, ScriptBlock block, AssetLoader loader)
				: base(script, block) {
				var reader = loader.Reader;

				loader.Expect((ushort)0x1234);
				LocalVariableOffset = reader.ReadUInt16();
				int functionSelectorListOffset = reader.ReadUInt16();

				int variableSelectorCount = reader.ReadUInt16();
				Codex<VariableSelector> variableSelectors = new Codex<VariableSelector>(variableSelectorCount);
				Variables = variableSelectors;
				for (int index = 0; index < variableSelectorCount; index++)
					variableSelectors.Add(new VariableSelector(this, loader));

				int functionSelectorCount = reader.ReadUInt16();
				Codex<FunctionSelector> functionSelectors = new Codex<FunctionSelector>(functionSelectorCount);
				Functions = functionSelectors;
				for (int index = 0; index < functionSelectorCount; index++)
					functionSelectors.Add(new FunctionSelector(this, loader));

				loader.Expect((ushort)0);
				for (int index = 0; index < functionSelectorCount; index++)
					functionSelectors[index].ReadCodeOffset(loader);

				loader.ExpectPosition(block.EndOffset);
			}

			public override string ToString() {
				return string.Format("{0}({1} variable(s), {2} function(s))", GetType().Name, Variables.Count, Functions.Count);
			}

			public override TreeNode ToTreeNode() {
				var node = new TreeNode(ToString());
				return node;
			}

			public abstract class Selector {
				public int Index { get; private set; }
				public Object Object { get; private set; }

				internal Selector(Object @object, AssetLoader loader) {
					Object = @object;
					Index = loader.Reader.ReadUInt16();
				}

				public override string ToString() {
					return string.Format("{0}({1})", GetType().Name, Index);
				}
			}

			public class FunctionSelector : Selector {
				public int CodeOffset { get; private set; }

				internal FunctionSelector(Object @object, AssetLoader loader) : base(@object, loader) { }

				internal void ReadCodeOffset(AssetLoader loader) { CodeOffset = loader.Reader.ReadUInt16(); }

				public override string ToString() {
					return string.Format("{0}({1}, {2})", GetType().Name, Index, CodeOffset);
				}
			}

			public class VariableSelector : Selector {
				internal VariableSelector(Object @object, AssetLoader loader) : base(@object, loader) { }
			}
		}

		public class Strings : ScriptSection {
			public int Count { get { return Values.Count; } }
			public Codex<string> Values { get; private set; }

			public string this[int index] { get { return Values[index]; } }

			internal Strings(Script script, ScriptBlock block, AssetLoader loader)
				: base(script, block) {
				Codex<string> list = new Codex<string>();
				Values = list;
				while (loader.Position < block.EndOffset)
					list.Add(loader.Reader.ReadStringz(Encoding.ASCII));
			}

			public override string ToString() {
				return string.Format("{0}({1} string(s))", GetType().Name, Count);
			}

			public override TreeNode ToTreeNode() {
				TreeNode node = new TreeNode(ToString());
				for (int index = 0; index < Count; index++)
					node.Nodes.Add(new TreeNode(index + ". " + this[index].Trim()));
				return node;
			}
		}

		public class Unknown : ScriptSection {
			public Codex<byte> Content { get; private set; }

			internal Unknown(Script script, ScriptBlock block, AssetLoader loader)
				: base(script, block) {
				Content = new Codex<byte>(loader.Reader.ReadBytes(block.ContentSize));
			}

			public override TreeNode ToTreeNode() {
				var node = new TreeNode(ToString());
				return node;
			}
		}
	}

	public enum ScriptBlockType : ushort {
		End = 0,
		Object = 1,
		Code = 2,
		Synonyms = 3,
		Said = 4,
		Strings = 5,
		Class = 6,
		Exports = 7,
		Relocations = 8,
		Preload = 9,
		Locals = 10,
	}
}
