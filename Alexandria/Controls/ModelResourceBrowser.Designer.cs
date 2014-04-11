namespace Alexandria.Controls {
	partial class ModelResourceBrowser {
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
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.DisplayModeComboBox = new System.Windows.Forms.ToolStripComboBox();
			this.Panel = new System.Windows.Forms.Panel();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DisplayModeComboBox});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(389, 25);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// DisplayModeComboBox
			// 
			this.DisplayModeComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.DisplayModeComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.DisplayModeComboBox.Name = "DisplayModeComboBox";
			this.DisplayModeComboBox.Size = new System.Drawing.Size(121, 25);
			this.DisplayModeComboBox.SelectedIndexChanged += new System.EventHandler(this.OnDisplayModeChanged);
			this.DisplayModeComboBox.Click += new System.EventHandler(this.DisplayModeComboBox_Click);
			// 
			// Panel
			// 
			this.Panel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Panel.Location = new System.Drawing.Point(0, 25);
			this.Panel.Name = "Panel";
			this.Panel.Size = new System.Drawing.Size(389, 364);
			this.Panel.TabIndex = 1;
			// 
			// ModelResourceBrowser
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.Panel);
			this.Controls.Add(this.toolStrip1);
			this.Name = "ModelResourceBrowser";
			this.Size = new System.Drawing.Size(389, 389);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripComboBox DisplayModeComboBox;
		private System.Windows.Forms.Panel Panel;
	}
}
