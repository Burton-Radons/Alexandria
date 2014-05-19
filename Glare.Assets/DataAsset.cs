using Glare.Assets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Glare.Assets {
	public abstract class DataAsset : Asset {
		WeakReference contents;

		public Asset Contents {
			get {
				Asset target;

				if (contents == null)
					contents = new WeakReference(target = Load());
				else if ((target = (Asset)contents.Target) == null)
					contents.Target = target = Load();

				return target;
			}
		}

		public virtual FileManager FileManager { get { return FileManager.System; } }

		public DataAsset(FolderAsset parent, string name, string description = null)
			: base(parent, name, description) {
		}

		public override Control Browse() {
			return Contents.Browse();
		}

		public override Control BrowseContents() {
			return Contents.BrowseContents();
		}

		public override void FillContextMenu(ContextMenuStrip strip) {
			ToolStripMenuItem saveBinary;

			strip.Items.Add(saveBinary = new ToolStripMenuItem("Save binary to file...") {
			});

			saveBinary.Click += (sender, args) => {
				var dialog = new SaveFileDialog() {
					Title = "Select filename for " + Name,
					FileName = Name,
					OverwritePrompt = true,
				};

				if (dialog.ShowDialog() == DialogResult.OK) {
					using (Stream file = File.Open(dialog.FileName, FileMode.Create))
					using (Stream source = Open())
						source.CopyTo(file);
				}
			};

			try {
				Contents.FillContextMenu(strip);
				Contents.BrowseContents();
			} catch (Exception) {
				ToolStripMenuItem debugException = new ToolStripMenuItem("Debug exception thrown when reading...") {
				};

				debugException.Click += (sender, args) => {
					try {
						var thread = new Thread(() => Contents.FillContextMenu(strip));
						thread.Start();
						while (thread.ThreadState == System.Threading.ThreadState.Running)
							Thread.SpinWait(1);
						
					} catch(Exception exception) {
						exception.ToString();
						Debugger.Break();
					}
				};

				strip.Items.Add(debugException);
			}
		}

		protected virtual Asset Load() {
			return Manager.Load(OpenReader(), PathName, FileManager, this);
		}

		public abstract Stream Open();

		public virtual BinaryReader OpenReader() { return new BinaryReader(Open()); }
	}
}
