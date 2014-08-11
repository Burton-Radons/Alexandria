using Glare.Assets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.UltimaUnderworld {
	/// <summary>
	/// Static game state.
	/// </summary>
	public class State : Alexandria.PathState {
		readonly PathStateAsset palettes;

		public PaletteArchive Palettes { get { return (PaletteArchive)palettes.Contents; } }

		public State(AlexandriaManager manager, string rootPath, FileManager fileManager)
			: base(manager, rootPath, fileManager) {
				palettes = new PathStateAsset(this, "data/pals.dat");
		}

		/// <summary>Get the state based on an <see cref="Asset"/> within the game.</summary>
		/// <param name="asset"></param>
		/// <returns></returns>
		public static State Get(Asset asset) {
			AlexandriaManager manager = (AlexandriaManager)asset.Manager;
			FileManager fileManager = asset.LoadFileManager;
			string path = Path.GetDirectoryName(Path.GetDirectoryName(asset.PathName));
			Type gameType;

			if (fileManager.Exists(path + "/uw.exe"))
				gameType = typeof(GameUltimaUnderworld);
			else if (fileManager.Exists(path + "/uw2.exe"))
				gameType = typeof(GameUltimaUnderworld2);
			else if (fileManager.Exists(path + "/sshock.exe"))
				gameType = typeof(GameSystemShock);
			else
				throw new Exception(string.Format("No game executable could be found in '{0}'.", path));

			Game game = (Game)manager.GetGame(gameType);

			return (State)((AlexandriaManager)asset.Manager).GetPathState(game, fileManager, path);
		}

		/// <summary>Get an indexed palette.</summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public PaletteAsset GetPalette(int index) { return (PaletteAsset)Palettes.Children[index]; }
	}
}
