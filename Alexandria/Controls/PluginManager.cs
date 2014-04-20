using Glare.Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alexandria.Controls {
	public partial class PluginManager : Form {
		public PluginManager(AssetManager manager) {
			InitializeComponent();

			tree.BeginUpdate();
			foreach (AssetPlugin plugin in manager.Plugins)
				tree.Nodes.Add(new PluginTreeNode(plugin));
			tree.ExpandAll();
			tree.EndUpdate();
		}

		static TreeNode CreateRoot<T>(TreeNode parent, string name, ICollection<T> collection) {
			TreeNode node = new TreeNode(collection.Count + " " + name + (collection.Count == 1 ? "" : "s"));

			parent.Nodes.Add(node);
			return node;
		}

		static void CreateSubnode(TreeNode parent, ICollection<Engine> collection) {
			if (collection.Count == 0)
				return;
			TreeNode node = CreateRoot(parent, "engine", collection);
			foreach (Engine engine in collection)
				node.Nodes.Add(new EngineTreeNode(engine));
		}

		static void CreateSubnode(TreeNode parent, ICollection<Game> collection, Engine engine) {
			if (collection.Count == 0)
				return;
			TreeNode node = CreateRoot(parent, "game", collection);
			foreach (Game game in collection)
				if (game.Engine == engine)
					node.Nodes.Add(new GameTreeNode(game));
		}

		static void CreateSubnode(TreeNode parent, ICollection<AssetFormat> collection) {
			if (collection.Count == 0)
				return;
			TreeNode node = CreateRoot(parent, "loader", collection);
			foreach (AssetFormat format in collection)
				node.Nodes.Add(new FormatTreeNode(format));
		}

		class EngineTreeNode : PluginAssetTreeNode {
			public EngineTreeNode(Engine engine)
				: base(engine) {
				ToolTipText = engine.Description;

				CreateSubnode(this, engine.Games, engine);
				CreateSubnode(this, engine.Formats);
			}
		}

		class GameTreeNode : PluginAssetTreeNode {
			public GameTreeNode(Game game)
				: base(game) {
				CreateSubnode(this, game.Formats);
			}
		}

		class FormatTreeNode : PluginAssetTreeNode {
			public FormatTreeNode(AssetFormat format)
				: base(format) {
			}
		}

		class PluginTreeNode : PluginAssetTreeNode {
			public PluginTreeNode(AssetPlugin plugin)
				: base(plugin) {
				AlexandriaPlugin aplugin = plugin as AlexandriaPlugin;

				if (aplugin != null) {
					CreateSubnode(this, aplugin.Engines);
					CreateSubnode(this, aplugin.Games, null);
				}

				CreateSubnode(this, plugin.Formats);
			}
		}

		abstract class PluginAssetTreeNode : TreeNode {
			public PluginAssetTreeNode(PluginAsset resource)
				: base(resource.DisplayName) {
				ToolTipText = resource.Description;
				Checked = resource.IsEnabled;
			}
		}
	}
}
