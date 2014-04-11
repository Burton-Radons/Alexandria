namespace Alexandria.Engines.Sciagi.Controls {
	partial class ViewBrowserItem {
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.label = new System.Windows.Forms.Label();
			this.picture = new System.Windows.Forms.PictureBox();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picture)).BeginInit();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.label, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.picture, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(232, 264);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// label
			// 
			this.label.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label.AutoSize = true;
			this.label.Location = new System.Drawing.Point(98, 251);
			this.label.Name = "label";
			this.label.Size = new System.Drawing.Size(35, 13);
			this.label.TabIndex = 0;
			this.label.Text = "label1";
			// 
			// picture
			// 
			this.picture.Dock = System.Windows.Forms.DockStyle.Fill;
			this.picture.Location = new System.Drawing.Point(3, 3);
			this.picture.Name = "picture";
			this.picture.Size = new System.Drawing.Size(226, 245);
			this.picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.picture.TabIndex = 1;
			this.picture.TabStop = false;
			// 
			// ViewBrowserItem
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "ViewBrowserItem";
			this.Size = new System.Drawing.Size(232, 264);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picture)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label label;
		private System.Windows.Forms.PictureBox picture;
	}
}
