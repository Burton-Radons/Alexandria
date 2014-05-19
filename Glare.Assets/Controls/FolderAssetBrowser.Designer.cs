namespace Glare.Assets.Controls {
	partial class FolderAssetBrowser {
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.splitter = new System.Windows.Forms.SplitContainer();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.tree = new System.Windows.Forms.TreeView();
			this.assetBar = new Glare.Assets.Controls.AssetBar();
			((System.ComponentModel.ISupportInitialize)(this.splitter)).BeginInit();
			this.splitter.Panel1.SuspendLayout();
			this.splitter.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitter
			// 
			this.splitter.BackColor = System.Drawing.SystemColors.ButtonShadow;
			this.splitter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitter.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitter.Location = new System.Drawing.Point(0, 0);
			this.splitter.Name = "splitter";
			// 
			// splitter.Panel1
			// 
			this.splitter.Panel1.Controls.Add(this.tableLayoutPanel1);
			this.splitter.Size = new System.Drawing.Size(501, 392);
			this.splitter.SplitterDistance = 167;
			this.splitter.TabIndex = 0;
			this.splitter.DoubleClick += new System.EventHandler(this.OnSplitterDoubleClicked);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.tree, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.assetBar, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(167, 392);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// tree
			// 
			this.tree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tree.Location = new System.Drawing.Point(0, 35);
			this.tree.Margin = new System.Windows.Forms.Padding(0);
			this.tree.Name = "tree";
			this.tree.Size = new System.Drawing.Size(167, 357);
			this.tree.TabIndex = 1;
			this.tree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.OnSelected);
			// 
			// assetBar
			// 
			this.assetBar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.assetBar.Asset = null;
			this.assetBar.Location = new System.Drawing.Point(0, 0);
			this.assetBar.Margin = new System.Windows.Forms.Padding(0);
			this.assetBar.Name = "assetBar";
			this.assetBar.Size = new System.Drawing.Size(167, 35);
			this.assetBar.TabIndex = 2;
			// 
			// FolderAssetBrowser
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitter);
			this.Name = "FolderAssetBrowser";
			this.Size = new System.Drawing.Size(501, 392);
			this.splitter.Panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitter)).EndInit();
			this.splitter.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitter;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TreeView tree;
		private AssetBar assetBar;
	}
}
