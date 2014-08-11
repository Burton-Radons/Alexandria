using Glare.Assets;
using Glare.Framework;
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
	/// <summary>
	/// An asset that can be loaded as binary data, which is then loaded as data.
	/// </summary>
	public abstract class DataAsset : Asset {
		WeakReference contents;

		/// <summary>Load the contents of the asset.</summary>
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

		/// <summary>Get the file manager to use...</summary>
		public virtual FileManager FileManager { get { return FileManager.System; } }
		
		/// <summary>
		/// Initialise the data asset.
		/// </summary>
		/// <param name="parent"></param>
		/// <param name="name"></param>
		/// <param name="description"></param>
		public DataAsset(FolderAsset parent, string name, string description = null)
			: base(parent, name, description) {
		}

		/// <summary>Browse the contents.</summary>
		/// <param name="progressUpdateCallback"></param>
		/// <returns></returns>
		public override Control Browse(Action<double> progressUpdateCallback = null) {
			return Contents.Browse(progressUpdateCallback);
		}

		/// <summary>Browse the contents.</summary>
		/// <param name="progressUpdateCallback"></param>
		/// <returns></returns>
		public override Control BrowseContents(Action<double> progressUpdateCallback = null) {
			return Contents.BrowseContents(progressUpdateCallback);
		}

		/// <summary>
		/// Fill out a context menu.
		/// </summary>
		/// <param name="strip"></param>
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

		/// <summary>
		/// Load the data.
		/// </summary>
		/// <returns></returns>
		protected virtual Asset Load() {
			return Manager.Load(OpenReader(), PathName, FileManager, this);
		}

		/// <summary>Open a stream to the data.</summary>
		/// <returns></returns>
		public abstract Stream Open();

		/// <summary>Open a binary reader for the stream.</summary>
		/// <returns></returns>
		public virtual BinaryReader OpenReader() { return new BinaryReader(Open()); }

		/// <summary>Open a binary reader for the stream.</summary>
		/// <param name="byteOrder"></param>
		/// <returns></returns>
		public virtual BinaryReader OpenReader(ByteOrder byteOrder) { return BigEndianBinaryReader.Create(byteOrder, Open()); }
	}
}
