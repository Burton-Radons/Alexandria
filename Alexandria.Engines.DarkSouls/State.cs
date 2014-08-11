using Glare.Assets;
using Glare.Framework;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Alexandria.Engines.DarkSouls {
	/// <summary>
	/// State for a Dark Souls installation.
	/// </summary>
	public class State : PathState {
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly Codex<Archive> ArchivesMutable = new Codex<Archive>();

		/// <summary>
		/// Get the archives.
		/// </summary>
		public ReadOnlyCodex<Archive> Archives { get { return ArchivesMutable; } }

		/// <summary>
		/// Initialise the state.
		/// </summary>
		/// <param name="manager"></param>
		/// <param name="rootPath"></param>
		/// <param name="fileManager"></param>
		/// <param name="archive"></param>
		public State(AlexandriaManager manager, string rootPath, FileManager fileManager, Archive archive = null)
			: base(manager, rootPath, fileManager) {
			if (archive != null)
				ArchivesMutable.Add(archive);
			foreach(var path in Directory.EnumerateFiles(RootPath, "*.bhd5")) {

			}
		}
	}
}