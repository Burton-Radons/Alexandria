using Glare.Assets;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria {
	public abstract class State {
		public FileManager FileManager { get; private set; }

		public AlexandriaManager Manager { get; private set; }

		public State(AlexandriaManager manager, FileManager fileManager) {
			if (manager == null)
				throw new ArgumentNullException("manager");
			if (fileManager == null)
				throw new ArgumentNullException("fileManager");
			Manager = manager;
			FileManager = fileManager;
		}

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

		public PathState(AlexandriaManager manager, string rootPath, FileManager fileManager)
			: base(manager, fileManager) {
			RootPath = rootPath;
		}

		public PathState(AlexandriaManager manager, string rootPath)
			: this(manager, rootPath, FileManager.System) {
		}
	}
}
