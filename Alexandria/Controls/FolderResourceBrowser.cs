using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Alexandria.Resources;

namespace Alexandria.Controls {
	public partial class FolderResourceBrowser : UserControl {
		public readonly Folder Resource;

		public FolderResourceBrowser(Folder resource) {
			Resource = resource;
			InitializeComponent();

			tree.BeginUpdate();
			tree.AfterExpand += (sender, args) => { AdjustSplitter(); };
			tree.AfterCollapse += (sender, args) => { AdjustSplitter(); };
			var nodes = tree.Nodes;

			foreach (Resource child in resource.Children)
				Add(child, nodes);

			tree.EndUpdate();
			AdjustSplitter();
		}

		void Add(Resource resource, TreeNodeCollection nodes) {
			TreeNode node = new ResourceTreeNode(resource);

			nodes.Add(node);

			if (resource is Folder) {
				if (resource.Children.Count == 1)
					node.Expand();
				foreach (Resource child in resource.Children)
					Add(child, node.Nodes);
			}
		}

		class ResourceTreeNode : TreeNode {
			readonly Resource Resource;

			public override ContextMenuStrip ContextMenuStrip {
				get {
					var strip = Resource.GetContextMenu();
					if (strip.Items.Count == 0)
						return null;
					return strip;
					//return base.ContextMenuStrip;
				}
				set {
					base.ContextMenuStrip = value;
				}
			}

			public ResourceTreeNode(Resource resource)
				: base(resource.DisplayName) {
				Tag = Resource = resource;
			}
		}

		private void OnSelected(object sender, TreeViewEventArgs e) {
			ControlCollection controls = splitter.Panel2.Controls;
			Resource resource = e.Node.Tag as Resource;
			Control control;

			controls.Clear();

			try {
				if (resource == null)
					return;
				
				control = resource.BrowseContents();
			} catch (Exception exception) {
				control = new TextBox() {
					Font = new Font(FontFamily.GenericMonospace, 8),
					Multiline = true,
					ScrollBars = System.Windows.Forms.ScrollBars.Both,
					Text = exception.ToString(),
					WordWrap = true,
				};
			}

			if (control != null) {
				control.Dock = DockStyle.Fill;
				controls.Add(control);
			} else
				controls.Add(new Label() { Text = resource.GetType().Name + "doesn't have a browser." });
		}

		private void OnSplitterDoubleClicked(object sender, EventArgs e) {
			AdjustSplitter();
		}

		void AdjustSplitter() {
			splitter.SplitterDistance = RightmostBound(tree.Nodes) + tree.Margin.Horizontal + 15;
		}

		int RightmostBound(TreeNodeCollection nodes) {
			int value = 0;

			foreach (TreeNode node in nodes) {
				value = Math.Max(node.Bounds.Right, value);
				if (node.IsExpanded)
					value = Math.Max(RightmostBound(node.Nodes), value);
			}

			return value;
		}

		class MyTreeView : TreeView {
			public MyTreeView() {
			}
		}
	}
}
