namespace Alexandria.Controls {
	partial class GameInstanceManager {
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.addManuallyButton = new System.Windows.Forms.Button();
			this.detectGames = new System.Windows.Forms.Button();
			this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.EngineColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Path = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.flowLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(284, 262);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NameColumn,
            this.EngineColumn,
            this.Path});
			this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridView1.Location = new System.Drawing.Point(3, 3);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.Size = new System.Drawing.Size(278, 221);
			this.dataGridView1.TabIndex = 0;
			this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.AutoSize = true;
			this.flowLayoutPanel1.Controls.Add(this.addManuallyButton);
			this.flowLayoutPanel1.Controls.Add(this.detectGames);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 230);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(278, 29);
			this.flowLayoutPanel1.TabIndex = 1;
			// 
			// addManuallyButton
			// 
			this.addManuallyButton.AutoSize = true;
			this.addManuallyButton.Location = new System.Drawing.Point(3, 3);
			this.addManuallyButton.Name = "addManuallyButton";
			this.addManuallyButton.Size = new System.Drawing.Size(90, 23);
			this.addManuallyButton.TabIndex = 0;
			this.addManuallyButton.Text = "Add Manually...";
			this.addManuallyButton.UseVisualStyleBackColor = true;
			// 
			// detectGames
			// 
			this.detectGames.AutoSize = true;
			this.detectGames.Location = new System.Drawing.Point(99, 3);
			this.detectGames.Name = "detectGames";
			this.detectGames.Size = new System.Drawing.Size(94, 23);
			this.detectGames.TabIndex = 1;
			this.detectGames.Text = "Detect Games...";
			this.detectGames.UseVisualStyleBackColor = true;
			this.detectGames.Click += new System.EventHandler(this.detectGames_Click);
			// 
			// NameColumn
			// 
			this.NameColumn.HeaderText = "Name";
			this.NameColumn.Name = "NameColumn";
			this.NameColumn.ReadOnly = true;
			// 
			// EngineColumn
			// 
			this.EngineColumn.HeaderText = "Engine";
			this.EngineColumn.Name = "EngineColumn";
			this.EngineColumn.ReadOnly = true;
			// 
			// Path
			// 
			this.Path.HeaderText = "Path";
			this.Path.Name = "Path";
			this.Path.ReadOnly = true;
			// 
			// GameInstanceManager
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 262);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "GameInstanceManager";
			this.Text = "Game Instance Manager";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Button addManuallyButton;
		private System.Windows.Forms.Button detectGames;
		private System.Windows.Forms.DataGridViewTextBoxColumn NameColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn EngineColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn Path;
	}
}