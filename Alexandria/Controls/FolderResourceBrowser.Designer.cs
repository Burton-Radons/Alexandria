namespace Alexandria.Controls {
	partial class FolderResourceBrowser {
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
			this.tree = new System.Windows.Forms.TreeView();
			((System.ComponentModel.ISupportInitialize)(this.splitter)).BeginInit();
			this.splitter.Panel1.SuspendLayout();
			this.splitter.SuspendLayout();
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
			this.splitter.Panel1.Controls.Add(this.tree);
			this.splitter.Size = new System.Drawing.Size(501, 392);
			this.splitter.SplitterDistance = 167;
			this.splitter.TabIndex = 0;
			this.splitter.DoubleClick += new System.EventHandler(this.OnSplitterDoubleClicked);
			// 
			// tree
			// 
			this.tree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tree.Location = new System.Drawing.Point(0, 0);
			this.tree.Name = "tree";
			this.tree.Size = new System.Drawing.Size(167, 392);
			this.tree.TabIndex = 0;
			this.tree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.OnSelected);
			// 
			// FolderResourceBrowser
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitter);
			this.Name = "FolderResourceBrowser";
			this.Size = new System.Drawing.Size(501, 392);
			this.splitter.Panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitter)).EndInit();
			this.splitter.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitter;
		private System.Windows.Forms.TreeView tree;
	}
}
