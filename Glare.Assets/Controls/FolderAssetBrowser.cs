using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Glare.Assets;

namespace Glare.Assets.Controls {
	/// <summary>
	/// A browser for a <see cref="FolderAsset"/>, or any other asset that wants to act like one.
	/// </summary>
	public partial class FolderAssetBrowser : UserControl {
		/// <summary>
		/// Get the asset this views.
		/// </summary>
		public readonly Asset Resource;

		/// <summary>
		/// Initialise the browser.
		/// </summary>
		/// <param name="progressUpdateCallback"></param>
		/// <param name="resource"></param>
		public FolderAssetBrowser(Asset resource, Action<double> progressUpdateCallback = null) {
			Resource = resource;
			InitializeComponent();

			assetBar.Asset = resource;

			FillTree();
			AdjustSplitter();
		}

		void Add(Asset resource, TreeNodeCollection nodes, Action<double> progressUpdateCallback, double percentOffset, double percentPart) {
			if (progressUpdateCallback != null)
				progressUpdateCallback(percentOffset);

			TreeNode node = new AssetTreeNode(resource);

			nodes.Add(node);

			if (resource is FolderAsset) {
				if (resource.Children.Count == 1)
					node.Expand();

				double childPercentOffset = 0, childPercentPart = percentPart / resource.Children.Count;
				foreach (Asset child in resource.Children) {
					Add(child, node.Nodes, progressUpdateCallback, percentOffset + childPercentOffset, percentPart);
					childPercentOffset += childPercentPart;
				}
			}
		}

		void FillTree(Action<double> progressUpdateCallback = null) {
			tree.BeginUpdate();
			tree.AfterExpand += (sender, args) => { AdjustSplitter(); };
			tree.AfterCollapse += (sender, args) => { AdjustSplitter(); };
			var nodes = tree.Nodes;

			double percentOffset = 0, percentPart = 100.0 / Resource.Children.Count;
			foreach (Asset child in Resource.Children) {
				Add(child, nodes, progressUpdateCallback, percentOffset, percentPart);
				percentOffset += percentPart;
			}

			tree.EndUpdate();
		}

		class AssetTreeNode : TreeNode {
			readonly Asset Resource;

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

			public AssetTreeNode(Asset resource)
				: base(resource.DisplayName) {
				Tag = Resource = resource;
			}
		}

		private void OnSelected(object sender, TreeViewEventArgs e) {
			ControlCollection controls = splitter.Panel2.Controls;
			Asset resource = e.Node.Tag as Asset;
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
