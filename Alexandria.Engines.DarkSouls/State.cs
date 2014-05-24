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
	public class State : PathState {
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly Codex<Archive> ArchivesMutable = new Codex<Archive>();

		public ReadOnlyCodex<Archive> Archives { get { return ArchivesMutable; } }

		public State(AlexandriaManager manager, string rootPath, FileManager fileManager, Archive archive = null)
			: base(manager, rootPath, fileManager) {
			if (archive != null)
				ArchivesMutable.Add(archive);
			foreach(var path in Directory.EnumerateFiles(RootPath, "*.bhd5")) {

			}
		}
	}
}