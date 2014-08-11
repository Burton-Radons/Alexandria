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
	/// <summary>
	/// A script resource.
	/// </summary>
	public class Script : ResourceData {
		Codex<ScriptSection> blocks = new Codex<ScriptSection>();

		/// <summary>
		/// Get the sections of the script.
		/// </summary>
		public Codex<ScriptSection> Blocks { get { return blocks; } }

		internal Script(AssetLoader loader)
			: base(loader) {
			ScriptSection block;

			while ((block = ScriptSection.Read(this, loader)) != null)
				blocks.Add(block);
		}

		/// <summary>
		/// Create a control to browse the script.
		/// </summary>
		/// <returns></returns>
		public override Control Browse(Action<double> progressUpdateCallback = null) {
			TreeView tree = new TreeView() {
			};

			foreach (ScriptSection block in blocks)
				tree.Nodes.Add(block.ToTreeNode());

			return tree;
		}
	}

	/// <summary>A block in a <see cref="Script"/>.</summary>
	public struct ScriptBlock {
		/// <summary>Get the offset of the content of the block.</summary>
		public long ContentOffset { get { return HeaderOffset + (Type == ScriptBlockType.End ? 2 : 4); } }

		/// <summary>Get the size in bytes of the block.</summary>
		public int ContentSize { get { return TotalSize > 0 ? TotalSize - 4 : 0; } }

		/// <summary>Get the offset after the end of the block.</summary>
		public long EndOffset { get { return HeaderOffset + TotalSize; } }

		/// <summary>Get the offset of the header of the block.</summary>
		public readonly long HeaderOffset;

		/// <summary>Get the total size of the block including the header.</summary>
		public readonly int TotalSize;

		/// <summary>Get the type of the block.</summary>
		public readonly ScriptBlockType Type;

		/// <summary>Load the block.</summary>
		/// <param name="loader"></param>
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

	/// <summary>
	/// A section of a <see cref="Script"/>.
	/// </summary>
	public abstract class ScriptSection {
		/// <summary>
		/// Get the block data for this section.
		/// </summary>
		public ScriptBlock Block { get; private set; }

		/// <summary>
		/// Get the containing <see cref="Script"/>.
		/// </summary>
		public Script Script { get; private set; }

		internal ScriptSection(Script script, ScriptBlock block) {
			Block = block;
			Script = script;
		}

		internal static ScriptSection Read(Script script, AssetLoader loader) {
			ScriptBlock block = new ScriptBlock(loader);

			switch (block.Type) {
				case ScriptBlockType.End: return null;
				case ScriptBlockType.Locals: return new Locals(script, block, loader);
				case ScriptBlockType.Object: return new Object(script, block, loader);
				case ScriptBlockType.Strings: return new Strings(script, block, loader);
				default: return new Unknown(script, block, loader);
			}
		}

		/// <summary>Convert to a string representation of the section.</summary>
		/// <returns></returns>
		public override string ToString() {
			return string.Format("{0} ({1} byte(s))", Block.Type, Block.ContentSize);
		}

		/// <summary>Convert to a tree node control.</summary>
		/// <returns></returns>
		public abstract TreeNode ToTreeNode();

		/// <summary>A local variable section for a script.</summary>
		public class Locals : ScriptSection {
			/// <summary>Get the number of <see cref="Values"/>.</summary>
			public int Count { get { return Values.Count; } }

			/// <summary>Get the values of the locals.</summary>
			public Codex<ushort> Values { get; private set; }

			/// <summary>Get the value of a local.</summary>
			/// <param name="index"></param>
			/// <returns></returns>
			public ushort this[int index] { get { return Values[index]; } }

			internal Locals(Script script, ScriptBlock block, AssetLoader loader)
				: base(script, block) {
				Codex<ushort> values = new Codex<ushort>(block.ContentSize / 2);
				Values = values;
				for (int index = 0; index < block.ContentSize / 2; index++)
					values.Add(loader.Reader.ReadUInt16());
			}

			/// <summary>
			/// Convert to a string.
			/// </summary>
			/// <returns></returns>
			public override string ToString() {
				return string.Format("{0}({1} local(s))", GetType().Name, Count);
			}

			/// <summary>
			/// Convert to a tree node control.
			/// </summary>
			/// <returns></returns>
			public override TreeNode ToTreeNode() {
				TreeNode node = new TreeNode(ToString());
				for (int index = 0; index < Count; index++)
					node.Nodes.Add(new TreeNode(index + ". " + this[index]));
				return node;
			}
		}

		/// <summary>
		/// Describes an object.
		/// </summary>
		public class Object : ScriptSection {
			/// <summary>The species selector index.</summary>
			public const int SpeciesSelectorIndex = 0;

			/// <summary>The super class selector index.</summary>
			public const int SuperClassSelectorIndex = 1;

			/// <summary>The info selector index.</summary>
			public const int InfoSelectorIndex = 2;

			/// <summary>The name selector index.</summary>
			public const int NameSelectorIndex = 3;

			/// <summary>Get the functions defined on the object.</summary>
			public Codex<FunctionSelector> Functions { get; private set; }

			/// <summary>Get the info selector variable object.</summary>
			public VariableSelector Info { get { return Variables[InfoSelectorIndex]; } }

			/// <summary>Get the offset of the local variables.</summary>
			public int LocalVariableOffset { get; private set; }

			/// <summary>Get the name selector.</summary>
			public VariableSelector Name { get { return Variables[NameSelectorIndex]; } }

			/// <summary>Get the species selector variable.</summary>
			public VariableSelector Species { get { return Variables[SpeciesSelectorIndex]; } }

			/// <summary>Get the super class selector variable.</summary>
			public VariableSelector SuperClass { get { return Variables[SuperClassSelectorIndex]; } }

			/// <summary>Get the variable selectors of the object.</summary>
			public Codex<VariableSelector> Variables { get; private set; }

			internal Object(Script script, ScriptBlock block, AssetLoader loader)
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

			/// <summary>
			/// Create a string representation of the object.
			/// </summary>
			/// <returns></returns>
			public override string ToString() {
				return string.Format("{0}({1} variable(s), {2} function(s))", GetType().Name, Variables.Count, Functions.Count);
			}

			/// <summary>
			/// Convert this object to a tree node control.
			/// </summary>
			/// <returns></returns>
			public override TreeNode ToTreeNode() {
				var node = new TreeNode(ToString());
				return node;
			}

			/// <summary>
			/// A selector in the object.
			/// </summary>
			public abstract class Selector {
				/// <summary>
				/// Get the index of the selector.
				/// </summary>
				public int Index { get; private set; }

				/// <summary>
				/// Get the object this selector is in.
				/// </summary>
				public Object Object { get; private set; }

				internal Selector(Object @object, AssetLoader loader) {
					Object = @object;
					Index = loader.Reader.ReadUInt16();
				}

				public override string ToString() {
					return string.Format("{0}({1})", GetType().Name, Index);
				}
			}

			/// <summary>
			/// A function selector in the object.
			/// </summary>
			public class FunctionSelector : Selector {
				/// <summary>
				/// Get the offset of the code.
				/// </summary>
				public int CodeOffset { get; private set; }

				internal FunctionSelector(Object @object, AssetLoader loader) : base(@object, loader) { }

				internal void ReadCodeOffset(AssetLoader loader) { CodeOffset = loader.Reader.ReadUInt16(); }

				/// <summary>
				/// Create a string representation of the object.
				/// </summary>
				/// <returns></returns>
				public override string ToString() {
					return string.Format("{0}({1}, {2})", GetType().Name, Index, CodeOffset);
				}
			}

			/// <summary>
			/// A variable selector in the object.
			/// </summary>
			public class VariableSelector : Selector {
				internal VariableSelector(Object @object, AssetLoader loader) : base(@object, loader) { }
			}
		}

		/// <summary>
		/// A collection of strings.
		/// </summary>
		public class Strings : ScriptSection {
			/// <summary>
			/// Get the number of strings.
			/// </summary>
			public int Count { get { return Values.Count; } }

			/// <summary>
			/// Get the values of the strings.
			/// </summary>
			public Codex<string> Values { get; private set; }

			/// <summary>
			/// Get an indexed string.
			/// </summary>
			/// <param name="index"></param>
			/// <returns></returns>
			public string this[int index] { get { return Values[index]; } }

			internal Strings(Script script, ScriptBlock block, AssetLoader loader)
				: base(script, block) {
				Codex<string> list = new Codex<string>();
				Values = list;
				while (loader.Position < block.EndOffset)
					list.Add(loader.Reader.ReadStringz(Encoding.ASCII));
			}

			/// <summary>
			/// Get a string representation of the object.
			/// </summary>
			/// <returns></returns>
			public override string ToString() {
				return string.Format("{0}({1} string(s))", GetType().Name, Count);
			}

			/// <summary>
			/// Create a tree node control for this object.
			/// </summary>
			/// <returns></returns>
			public override TreeNode ToTreeNode() {
				TreeNode node = new TreeNode(ToString());
				for (int index = 0; index < Count; index++)
					node.Nodes.Add(new TreeNode(index + ". " + this[index].Trim()));
				return node;
			}
		}

		/// <summary>
		/// An unknown script section.
		/// </summary>
		public class Unknown : ScriptSection {
			/// <summary>
			/// Get the data content.
			/// </summary>
			public Codex<byte> Content { get; private set; }

			internal Unknown(Script script, ScriptBlock block, AssetLoader loader)
				: base(script, block) {
				Content = new Codex<byte>(loader.Reader.ReadBytes(block.ContentSize));
			}

			/// <summary>
			/// Get a string representation of the section.
			/// </summary>
			/// <returns></returns>
			public override TreeNode ToTreeNode() {
				var node = new TreeNode(ToString());
				return node;
			}
		}
	}

	/// <summary>
	/// The type of a script block.
	/// </summary>
	public enum ScriptBlockType : ushort {
		/// <summary>Marks the end of the script blocks.</summary>
		End = 0,

		/// <summary>An object.</summary>
		Object = 1,

		/// <summary></summary>
		Code = 2,

		/// <summary></summary>
		Synonyms = 3,

		/// <summary></summary>
		Said = 4,

		/// <summary></summary>
		Strings = 5,

		/// <summary></summary>
		Class = 6,

		/// <summary></summary>
		Exports = 7,

		/// <summary></summary>
		Relocations = 8,

		/// <summary></summary>
		Preload = 9,

		/// <summary></summary>
		Locals = 10,
	}
}
