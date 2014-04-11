namespace Alexandria.Controls {
	partial class MainWindow {
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
			this.MenuStrip = new System.Windows.Forms.MenuStrip();
			this.FileMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.OpenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.CloseMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.RecentFilesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.WindowMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.stackhorizontallyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.stackverticallyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.cascadeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pluginsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// MenuStrip
			// 
			this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenu,
            this.toolsToolStripMenuItem,
            this.WindowMenu});
			this.MenuStrip.Location = new System.Drawing.Point(0, 0);
			this.MenuStrip.MdiWindowListItem = this.WindowMenu;
			this.MenuStrip.Name = "MenuStrip";
			this.MenuStrip.Size = new System.Drawing.Size(284, 24);
			this.MenuStrip.TabIndex = 0;
			this.MenuStrip.Text = "menuStrip1";
			// 
			// FileMenu
			// 
			this.FileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenMenuItem,
            this.CloseMenuItem,
            this.RecentFilesMenuItem,
            this.ExitMenuItem});
			this.FileMenu.Name = "FileMenu";
			this.FileMenu.Size = new System.Drawing.Size(37, 20);
			this.FileMenu.Text = "&File";
			this.FileMenu.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
			// 
			// OpenMenuItem
			// 
			this.OpenMenuItem.Name = "OpenMenuItem";
			this.OpenMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.OpenMenuItem.Size = new System.Drawing.Size(152, 22);
			this.OpenMenuItem.Text = "&Open";
			this.OpenMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			// 
			// CloseMenuItem
			// 
			this.CloseMenuItem.Name = "CloseMenuItem";
			this.CloseMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
			this.CloseMenuItem.Size = new System.Drawing.Size(152, 22);
			this.CloseMenuItem.Text = "&Close";
			this.CloseMenuItem.Click += new System.EventHandler(this.CloseMenuItem_Click);
			// 
			// RecentFilesMenuItem
			// 
			this.RecentFilesMenuItem.Name = "RecentFilesMenuItem";
			this.RecentFilesMenuItem.Size = new System.Drawing.Size(152, 22);
			this.RecentFilesMenuItem.Text = "Recent &files";
			// 
			// ExitMenuItem
			// 
			this.ExitMenuItem.Name = "ExitMenuItem";
			this.ExitMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.ExitMenuItem.Size = new System.Drawing.Size(152, 22);
			this.ExitMenuItem.Text = "E&xit";
			this.ExitMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// WindowMenu
			// 
			this.WindowMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripSeparator1});
			this.WindowMenu.Name = "WindowMenu";
			this.WindowMenu.Size = new System.Drawing.Size(63, 20);
			this.WindowMenu.Text = "&Window";
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stackhorizontallyToolStripMenuItem,
            this.stackverticallyToolStripMenuItem,
            this.cascadeToolStripMenuItem});
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
			this.toolStripMenuItem1.Text = "&Organize...";
			// 
			// stackhorizontallyToolStripMenuItem
			// 
			this.stackhorizontallyToolStripMenuItem.Name = "stackhorizontallyToolStripMenuItem";
			this.stackhorizontallyToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
			this.stackhorizontallyToolStripMenuItem.Text = "Stack &horizontally";
			this.stackhorizontallyToolStripMenuItem.ToolTipText = "Stack all of the windows horizontally, one on top of the other.";
			this.stackhorizontallyToolStripMenuItem.Click += new System.EventHandler(this.stackhorizontallyToolStripMenuItem_Click);
			// 
			// stackverticallyToolStripMenuItem
			// 
			this.stackverticallyToolStripMenuItem.Name = "stackverticallyToolStripMenuItem";
			this.stackverticallyToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
			this.stackverticallyToolStripMenuItem.Text = "Stack &vertically";
			this.stackverticallyToolStripMenuItem.ToolTipText = "Stack all of the windows vertically beside each other.";
			this.stackverticallyToolStripMenuItem.Click += new System.EventHandler(this.stackverticallyToolStripMenuItem_Click);
			// 
			// cascadeToolStripMenuItem
			// 
			this.cascadeToolStripMenuItem.Name = "cascadeToolStripMenuItem";
			this.cascadeToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
			this.cascadeToolStripMenuItem.Text = "&Cascade";
			this.cascadeToolStripMenuItem.Click += new System.EventHandler(this.cascadeToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
			// 
			// toolsToolStripMenuItem
			// 
			this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pluginsToolStripMenuItem});
			this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
			this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
			this.toolsToolStripMenuItem.Text = "&Tools";
			// 
			// pluginsToolStripMenuItem
			// 
			this.pluginsToolStripMenuItem.Name = "pluginsToolStripMenuItem";
			this.pluginsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.pluginsToolStripMenuItem.Text = "Plugins...";
			this.pluginsToolStripMenuItem.ToolTipText = "Display and manage the plugins.";
			this.pluginsToolStripMenuItem.Click += new System.EventHandler(this.pluginsToolStripMenuItem_Click);
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 262);
			this.Controls.Add(this.MenuStrip);
			this.IsMdiContainer = true;
			this.MainMenuStrip = this.MenuStrip;
			this.Name = "MainWindow";
			this.Text = "Alexandria";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.MainWindow_Load);
			this.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.OnControlAdded);
			this.MenuStrip.ResumeLayout(false);
			this.MenuStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip MenuStrip;
		private System.Windows.Forms.ToolStripMenuItem FileMenu;
		private System.Windows.Forms.ToolStripMenuItem OpenMenuItem;
		private System.Windows.Forms.ToolStripMenuItem CloseMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ExitMenuItem;
		private System.Windows.Forms.ToolStripMenuItem RecentFilesMenuItem;
		private System.Windows.Forms.ToolStripMenuItem WindowMenu;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem stackhorizontallyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem stackverticallyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem cascadeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pluginsToolStripMenuItem;
	}
}