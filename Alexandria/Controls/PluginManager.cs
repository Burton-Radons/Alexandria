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
		public PluginManager(Manager manager) {
			InitializeComponent();

			foreach (Plugin plugin in manager.Plugins)
				tree.Nodes.Add(new PluginTreeNode(plugin));
			tree.ExpandAll();
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

		static void CreateSubnode(TreeNode parent, ICollection<Loader> collection) {
			if (collection.Count == 0)
				return;
			TreeNode node = CreateRoot(parent, "loader", collection);
			foreach (Loader loader in collection)
				node.Nodes.Add(new LoaderTreeNode(loader));
		}

		class EngineTreeNode : ResourceTreeNode {
			public EngineTreeNode(Engine engine)
				: base(engine) {
				ToolTipText = engine.Description;

				CreateSubnode(this, engine.Games, engine);
				CreateSubnode(this, engine.Loaders);
			}
		}

		class GameTreeNode : ResourceTreeNode {
			public GameTreeNode(Game game)
				: base(game) {
				CreateSubnode(this, game.Loaders);
			}
		}

		class LoaderTreeNode : ResourceTreeNode {
			public LoaderTreeNode(Loader loader)
				: base(loader) {
			}
		}

		class PluginTreeNode : ResourceTreeNode {
			public PluginTreeNode(Plugin plugin)
				: base(plugin) {
				CreateSubnode(this, plugin.Engines);
				CreateSubnode(this, plugin.Games, null);
				CreateSubnode(this, plugin.Loaders);
			}
		}

		abstract class ResourceTreeNode : TreeNode {
			public ResourceTreeNode(Resource resource)
				: base(resource.DisplayName) {
				ToolTipText = resource.Description;
			}
		}
	}
}
