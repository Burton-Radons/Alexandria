using Alexandria.Properties;
using Glare.Assets;
using Glare.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alexandria.Controls {
	/// <summary>
	/// This provides the main window for Alexandria.
	/// </summary>
	public partial class MainWindow : Form {
		/// <summary>
		/// Get the asset manager to use.
		/// </summary>
		public readonly AlexandriaManager Manager;

		BackgroundWorker BackgroundOperationWorker;

		readonly Codex<Action> BackgroundOperationStack = new Codex<Action>();

		void BackgroundOperationProgressChanged(object sender, ProgressChangedEventArgs args) {
			progressBar.Value = args.ProgressPercentage;
		}

		void BackgroundOperationWork(object sender, DoWorkEventArgs args) {
			while (true) {
				BackgroundOperation current = Manager.CurrentBackgroundOperation;
				double progress = Manager.BackgroundOperationProgress;

				if (double.IsNaN(progress))
					progress = 0;

				int percent = (int)Math.Round(Math.Max(0, Math.Min(100, Manager.BackgroundOperationProgress)));
				int stackCount;

				if (current != null) {
					if (backgroundActionLabel.Text != current.Name)
						backgroundActionLabel.Text = current.Name;
					if (backgroundActionLabel.ToolTipText != (current.Description ?? ""))
						backgroundActionLabel.ToolTipText = (current.Description ?? "");
				}

				lock (BackgroundOperationStack)
					stackCount = BackgroundOperationStack.Count;

				if (progress != progressBar.Value || stackCount > 0)
					BackgroundOperationWorker.ReportProgress(percent);
				Thread.Sleep(100);
			}
		}

		/// <summary>
		/// Initialise the window with the asset manager.
		/// </summary>
		/// <param name="manager">The asset manager to use.</param>
		public MainWindow(AlexandriaManager manager) {
			Manager = manager;
			InitializeComponent();
			debuggModeToolStripMenuItem.Checked = manager.DebuggingEnabled;

			BackgroundOperationWorker = new BackgroundWorker() {
				WorkerReportsProgress = true,
			};

			BackgroundOperationWorker.ProgressChanged += BackgroundOperationProgressChanged;
			BackgroundOperationWorker.DoWork += BackgroundOperationWork;
			BackgroundOperationWorker.RunWorkerAsync();
		}

		private void MainWindow_Load(object sender, EventArgs e) {
			BuildRecentFilesList();
		}

		private void fileToolStripMenuItem_Click(object sender, EventArgs e) {

		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
			System.Windows.Forms.Application.Exit();
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e) {
			var dialog = new OpenFileDialog() {
				Title = "Select file to open.",
				CheckFileExists = true,
			};

			DialogResult result = dialog.ShowDialog();
			if (result == DialogResult.OK) {
				Open(dialog.FileName);
			}
		}

		private void OnControlAdded(object sender, ControlEventArgs e) {
		}

		private void CloseMenuItem_Click(object sender, EventArgs e) {
			if (ActiveMdiChild != null)
				ActiveMdiChild.Close();
		}

		void BuildRecentFilesList() {
			RecentFilesMenuItem.DropDownItems.Clear();

			var list = Settings.Default.RecentFiles;
			if (list == null)
				list = Settings.Default.RecentFiles = new System.Collections.Specialized.StringCollection();
			for(int listIndex = list.Count - 1, index = 1; listIndex >= 0; listIndex--, index++) {
				string path = list[listIndex];
				string text = (index < 10 ? "&" : "") + index + " " + path;

				RecentFilesMenuItem.DropDownItems.Add(text, null, (clickSender, args) => {
					Open(path);
				});
			}
		}

		void OpenProgressBar() {
		}

		static int MakePercentValue(double percent) {
			return (int)Math.Round(Math.Min(Math.Max(percent, 0.0), 100.0));
		}

		void Open(string path) {
			FileManager fileManager = FileManager.System;
			Stream stream = fileManager.OpenRead(path);
			BinaryReader reader = new BinaryReader(stream);
			AssetLoader loader = new AssetLoader(Manager, reader, path, fileManager);

			Manager.AddBackgroundOperation("Loading '" + path + "'", null, (updateProgress) => {
				loader.PropertyChanged += (sender, args) => {
					if (args.Property != AssetLoader.ProgressProperty)
						return;
					if(updateProgress != null)
						updateProgress(loader.Progress);
				};

				Asset asset = null;

				try {
					asset = AssetFormat.LoadAsset(loader, Manager.AllEnabledFormats);
				} catch (Exception exception) {
					MessageBox.Show("Load failed: " + exception, "Load failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}

				if (asset == null)
					return;

				Manager.AddBackgroundOperation("Browsing '" + path + "'.", null, (subUpdateProgress) => {
					Control control = asset.Browse(subUpdateProgress);


					var window = new Form() {
						MdiParent = this,
						Text = Path.GetFileName(path),
					};

					control.Dock = DockStyle.Fill;
					window.Controls.Add(control);
					window.BringToFront();
					window.Show();

					if (MdiChildren.Length == 1)
						window.WindowState = FormWindowState.Maximized;

					if (Settings.Default.RecentFiles == null)
						Settings.Default.RecentFiles = new System.Collections.Specialized.StringCollection();
					Settings.Default.RecentFiles.Remove(path);
					Settings.Default.RecentFiles.Add(path);
					if (Settings.Default.RecentFiles.Count > 20)
						Settings.Default.RecentFiles.RemoveAt(0);
					Settings.Default.Save();
					BuildRecentFilesList();
				});
			});
		}

		private void stackhorizontallyToolStripMenuItem_Click(object sender, EventArgs e) {
			LayoutMdi(MdiLayout.TileHorizontal);
		}

		private void stackverticallyToolStripMenuItem_Click(object sender, EventArgs e) {
			LayoutMdi(MdiLayout.TileVertical);
		}

		private void cascadeToolStripMenuItem_Click(object sender, EventArgs e) {
			LayoutMdi(MdiLayout.Cascade);
		}

		private void pluginsToolStripMenuItem_Click(object sender, EventArgs e) {
			var window = new PluginManager(Manager) {
				ShowInTaskbar = false,
			};

			window.Show(this);
		}

		private void detectGamesToolStripMenuItem_Click(object sender, EventArgs e) {
			var window = new GameInstanceManager((AlexandriaManager)Manager) {
			};

			window.BringToFront();
			window.Show();
		}

		private void OnDebugCheckStateChanged(object sender, EventArgs e) {
			Manager.DebuggingEnabled = debuggModeToolStripMenuItem.Checked;
		}

		private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e) {

		}
	}
}
