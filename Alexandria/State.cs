using Glare.Assets;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Alexandria {
	/// <summary>
	/// State for a <see cref="Game"/>.
	/// </summary>
	public abstract class State : FolderAsset {
		/// <summary>
		/// Get the file manager for loading resources.
		/// </summary>
		public FileManager FileManager { get; private set; }

		/// <summary>Get the <see cref="AlexandriaManager"/> that is used by the <see cref="State"/>.</summary>
		public new AlexandriaManager Manager { get; private set; }

		/// <summary>Initialise the <see cref="State"/>.</summary>
		/// <param name="manager"></param>
		/// <param name="fileManager"></param>
		public State(AlexandriaManager manager, FileManager fileManager)
			: base(manager, "State") {
			if (manager == null)
				throw new ArgumentNullException("manager");
			if (fileManager == null)
				throw new ArgumentNullException("fileManager");
			Manager = manager;
			FileManager = fileManager;
		}

		/// <summary>Initialise the <see cref="State"/>.</summary>
		/// <param name="manager"></param>
		public State(AlexandriaManager manager)
			: this(manager, FileManager.System) {
		}
	}

	/// <summary>State that operates within a directory.</summary>
	public abstract class PathState : State {
		string rootPath;

		/// <summary>Get the path with terminating path separator.</summary>
		public string RootPath {
			get { return rootPath; }

			protected set {
				rootPath = value;
				if (!string.IsNullOrEmpty(value)) {
					if (!rootPath.EndsWith(System.IO.Path.DirectorySeparatorChar))
						rootPath += System.IO.Path.DirectorySeparatorChar;
				}
			}
		}

		/// <summary>Initialise the <see cref="PathState"/>.</summary>
		/// <param name="manager"></param>
		/// <param name="rootPath"></param>
		/// <param name="fileManager"></param>
		public PathState(AlexandriaManager manager, string rootPath, FileManager fileManager)
			: base(manager, fileManager) {
			RootPath = rootPath;
		}

		/// <summary>Load an object from a relative path.</summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public Asset LoadRelative(string path) {
			return Manager.Load(OpenRelative(path, FileMode.Open, FileAccess.Read, FileShare.Delete | FileShare.ReadWrite), path, FileManager, null);
		}

		/// <summary>Open a stream to a relative path.</summary>
		/// <param name="path"></param>
		/// <param name="mode"></param>
		/// <param name="access"></param>
		/// <param name="share"></param>
		/// <returns></returns>
		public Stream OpenRelative(string path, FileMode mode, FileAccess access, FileShare share) {
			if (string.IsNullOrEmpty(path))
				throw new ArgumentNullException("path");
			if (path[0] != Path.DirectorySeparatorChar && path[0] != Path.AltDirectorySeparatorChar)
				path = Path.DirectorySeparatorChar + path;
			path = rootPath + path;
			return FileManager.Open(path, mode, access, share);
		}
	}

	/// <summary>An asset within a <see cref="State"/>.</summary>
	public class PathStateAsset : DataAsset {
		/// <summary>Get the <see cref="PathState"/> that this is contained in.</summary>
		public new PathState Parent { get { return (PathState)base.Parent; } }

		/// <summary>Initialise the asset.</summary>
		/// <param name="state"></param>
		/// <param name="relativePath"></param>
		public PathStateAsset(State state, string relativePath)
			: base(state, relativePath) {
		}

		/// <summary>Open a read-only stream to the asset data.</summary>
		/// <returns></returns>
		public override Stream Open() {
			return Parent.OpenRelative(Name, FileMode.Open, FileAccess.Read, FileShare.Delete | FileShare.ReadWrite);
		}
	}
}
