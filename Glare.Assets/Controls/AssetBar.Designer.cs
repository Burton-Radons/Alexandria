namespace Glare.Assets.Controls {
	partial class AssetBar {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AssetBar));
			this.panel1 = new System.Windows.Forms.Panel();
			this.bar = new System.Windows.Forms.TableLayoutPanel();
			this.label = new System.Windows.Forms.Label();
			this.button = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			this.bar.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			resources.ApplyResources(this.panel1, "panel1");
			this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.bar);
			this.panel1.Name = "panel1";
			// 
			// bar
			// 
			resources.ApplyResources(this.bar, "bar");
			this.bar.Controls.Add(this.label, 0, 0);
			this.bar.Controls.Add(this.button, 1, 0);
			this.bar.Name = "bar";
			// 
			// label
			// 
			resources.ApplyResources(this.label, "label");
			this.label.Name = "label";
			// 
			// button
			// 
			resources.ApplyResources(this.button, "button");
			this.button.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.button.Image = global::Glare.Assets.Properties.Resources.DownArrowImage;
			this.button.Name = "button";
			this.button.UseVisualStyleBackColor = false;
			this.button.Click += new System.EventHandler(this.button_Click);
			// 
			// AssetBar
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel1);
			this.Name = "AssetBar";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.bar.ResumeLayout(false);
			this.bar.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TableLayoutPanel bar;
		private System.Windows.Forms.Label label;
		private System.Windows.Forms.Button button;

	}
}
