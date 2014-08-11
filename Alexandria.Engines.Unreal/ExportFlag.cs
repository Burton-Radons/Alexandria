using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Engines.Unreal {
	/// <summary>
	/// Flags that can be applied to an <see cref="Export"/>.
	/// </summary>
	[Flags]
	public enum ExportFlag {
		/// <summary></summary>
		Transactional = 0x00000001,

		/// <summary>
		/// Not reachable on the object graph.
		/// </summary>
		Unreachable = 0x00000002,

		/// <summary>
		/// Visible outside the package.
		/// </summary>
		Public = 0x00000004,

		/// <summary>
		/// Temporary import tag for loading and saving.
		/// </summary>
		TagImp = 0x00000008,

		/// <summary>
		/// Temporary export tag for loading and saving.
		/// </summary>
		TagExp = 0x00000010,

		/// <summary>
		/// Modified relative to the source files.
		/// </summary>
		SourceModified = 0x00000020,

		/// <summary>
		/// Used during garbage collection.
		/// </summary>
		TagGarbage = 0x00000040,

		/// <summary>
		/// Private to its package.
		/// </summary>
		Private = 0x00000080,

		/// <summary>
		/// Used in UT2003, perhaps for array referenced objects.
		/// </summary>
		Unknown00000100 = 0x00000100,

		/// <summary>
		/// Indicates that the object needs loading.
		/// </summary>
		NeedLoad = 0x00000200,

		/// <summary>
		/// The name should be syntax-highlighted.
		/// </summary>
		HighlightedName = 0x00000400,

		/// <summary>
		/// In a singular function.
		/// </summary>
		InSingularFunction = 0x00000800,

		/// <summary>
		/// Suppressed log name.
		/// </summary>
		Suppressed = 0x00001000,

		/// <summary>
		/// Within an EndState call.
		/// </summary>
		InEndState = 0x00002000,

		/// <summary>
		/// Don't save the object.
		/// </summary>
		Transient = 0x00004000,

		/// <summary>
		/// Data is being preloaded from file.
		/// </summary>
		PreLoading = 0x00008000,

		/// <summary>
		/// Load for client.
		/// </summary>
		LoadForClient = 0x00010000,

		/// <summary>
		/// Load for server.
		/// </summary>
		LoadForServer = 0x00020000,

		/// <summary>
		/// Load for editor.
		/// </summary>
		LoadForEditor = 0x00040000,

		/// <summary>
		/// Keep object around for editing even if it's unreferenced.
		/// </summary>
		Standalone = 0x00080000,

		/// <summary>
		/// Don't load this object for the game client.
		/// </summary>
		NotForClient = 0x00100000,

		/// <summary>
		/// Don't load this object for the game server.
		/// </summary>
		NotForServer = 0x00200000,

		/// <summary>
		/// Don't load this object for the editor.
		/// </summary>
		NotForEditor = 0x00400000,

		/// <summary>
		/// The object has already been destroyed.
		/// </summary>
		Destroyed = 0x00800000,

		/// <summary>
		/// Object needs to be postloaded.
		/// </summary>
		NeedPostLoad = 0x01000000,

		/// <summary>
		/// Has an execution stack.
		/// </summary>
		HasStack = 0x02000000,

		/// <summary>
		/// Fully native (UClass only).
		/// </summary>
		Native = 0x04000000,

		/// <summary>
		/// Marked (for debugging).
		/// </summary>
		Marked = 0x08000000,

		/// <summary>
		/// ShutdownAfterError called.
		/// </summary>
		ErrorShutdown = 0x10000000,

		/// <summary>
		/// Serialize call debugging.
		/// </summary>
		DebugPostLoad = 0x20000000,

		/// <summary>
		/// Serialize call debugging.
		/// </summary>
		DebugSerialize = 0x40000000,

		/// <summary>
		/// Destroy call debugging.
		/// </summary>
		DebugDestroy = unchecked((int)0x80000000),
	}
}
