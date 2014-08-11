using Glare.Assets;
using Glare.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Alexandria {
	/// <summary>
	/// A derived class from <see cref="PluginFormatAsset"/> that can detect <see cref="GameInstance"/>s.
	/// </summary>
	public abstract class AlexandriaPluginFormatAsset : PluginFormatAsset {
		/// <summary>Get whether this can be used to search for <see cref="Game"/> files. The default is <c>false</c>.</summary>
		public virtual bool CanSearchForGameFiles { get { return false; } }

		/// <summary>Get the file patterns that this should use when searching for game files.</summary>
		public virtual ReadOnlyCodex<string> GameFilePatterns { get { return new ReadOnlyCodex<string>(new string[] { }); } }

		/// <summary>
		/// Initialise the object.
		/// </summary>
		/// <param name="plugin"></param>
		public AlexandriaPluginFormatAsset(AlexandriaPlugin plugin) : base(plugin) { }

		/// <summary>Attempt to detect <see cref="GameInstance"/>s.</summary>
		/// <param name="collection"></param>
		public virtual void Detect(ICollection<GameInstance> collection) {
		}

		/// <summary>Attempt to detect a <see cref="GameInstance"/> from this file.</summary>
		/// <param name="collection"></param>
		/// <param name="path">The path to the file.</param>
		/// <param name="reader">The opened file reader. You do not need to reset the position afterwards.</param>
		/// <param name="manager">The <see cref="FileManager"/> to open any other files, if necessary.</param>
		public virtual void DetectFile(ICollection<GameInstance> collection, string path, BinaryReader reader, FileManager manager) {
		}

		/// <summary>Attempt to match a game instance from a given path or a given file, returning the <see cref="GameInstance"/> or <c>null</c> if none could be found.</summary>
		/// <param name="path"></param>
		/// <param name="manager">The file manager to use for opening files.</param>
		/// <returns></returns>
		public virtual GameInstance MatchGame(string path, FileManager manager) { return null; }

		/// <summary>
		/// Return whether the game file exists within the path or 
		/// </summary>
		/// <param name="path"></param>
		/// <param name="filename"></param>
		/// <param name="manager">The file manager to use for opening files.</param>
		/// <returns></returns>
		protected static bool MatchGameFile(string path, string filename, FileManager manager) {
			return false;
		}
	}
}
