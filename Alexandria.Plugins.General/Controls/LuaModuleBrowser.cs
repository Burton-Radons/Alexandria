using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alexandria.General.Controls {
	public partial class LuaModuleBrowser : UserControl {
		public LuaModule Module { get; private set; }

		public LuaModuleBrowser(LuaModule module) {
			Module = module;

			InitializeComponent();

			tree.Nodes.Add("Endianness: " + module.Endianness);
			tree.Nodes.Add("Version: " + module.VersionString);

			TreeNode root = FunctionToTree(module.Main);
			root.Expand();
			tree.Nodes.Add(root);
		}

		TreeNode FunctionToTree(LuaFunction function) {
			string text = function.SourceOrIndex;

			text += ", ParameterCount: " + function.ParameterCount;
			text += ", LineDefined: " + function.LineDefined;
			if(function.MaxStackSize > 0)
				text += ", MaxStackSize: " + function.MaxStackSize;
			if(function.UpValueCount > 0)
				text += ", UpValueCount: " + function.UpValueCount;
			text += function.HasVariableArguments ? ", HasVariableArguments" : "";

			TreeNode root = new TreeNode(text);

			if (function.LineInfo.Count > 0) {
				TreeNode node = CreateCountNode(root, "Line Info", function.LineInfo);

				foreach (int value in function.LineInfo)
					node.Nodes.Add(value.ToString());
			}

			if (function.Constants.Count > 0) {
				TreeNode node = CreateCountNode(root, "Constant", function.Constants);

				for (int index = 0; index < function.Constants.Count; index++) {
					object constant = function.Constants[index];
					string constantText = index + ": ";

					if (constant is string)
						constantText += "\"" + constant + "\"";
					else if (constant == null)
						constantText += "null";
					else
						constantText += constant.ToString();
					node.Nodes.Add(new TreeNode(constantText));
				}
			}

			if (function.LocalVariables.Count > 0) {
				TreeNode node = CreateCountNode(root, "Local Variable", function.LocalVariables);

				for (int index = 0; index < function.LocalVariables.Count; index++) {
					LuaLocalVariable value = function.LocalVariables[index];

					node.Nodes.Add(new TreeNode(value.Name + " (" + value.ProgramCounterStart + " to " + value.ProgramCounterEnd + ")"));
				}
			}

			if (function.UpValues.Count > 0) {
				var list = function.UpValues;
				TreeNode node = CreateCountNode(root, "Up Value", list);

				for (int index = 0; index < list.Count; index++)
					node.Nodes.Add(new TreeNode(index + ": " + list[index]));
				
			}

			if (function.Closures.Count > 0) {
				TreeNode node = CreateCountNode(root, "Closure", function.Closures);

				foreach (LuaFunction value in function.Closures)
					node.Nodes.Add(FunctionToTree(value));
			}

			if (function.Instructions.Count > 0) {
				IList<LuaInstruction> list = function.Instructions;
				TreeNode node = CreateCountNode(root, "Instruction", function.Instructions);

				foreach (LuaInstruction value in list)
					node.Nodes.Add(value.ToString());
			}

			return root;
		}

		TreeNode CreateCountNode<T>(TreeNode parent, string root, ICollection<T> collection) {
			return CreateCountNode(parent, root, root, collection);
		}

		TreeNode CreateCountNode<T>(TreeNode parent, string rootSingular, string rootPlural, ICollection<T> collection) {
			TreeNode node = new TreeNode((collection.Count == 1 ? rootSingular : rootPlural) + " (" + collection.Count + ")");

			parent.Nodes.Add(node);
			return node;
		}
	}
}
