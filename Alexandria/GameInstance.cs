using Glare.Assets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria {
	/// <summary>
	/// This describes an instance of a game.
	/// </summary>
	public class GameInstance {
		/// <summary>Current version number used in the source.</summary>
		public const int CurrentSourceVersion = 0;

		/// <summary>Get the <see cref="Game"/> that this is an instance for. This may be <c>null</c>, if the <see cref="Game"/> cannot be found.</summary>
		public Game Game { get; private set; }

		/// <summary>Get the <see cref="Game"/>'s <see cref="Guid"/> from its <see cref="Alexandria.Game.Guid"/> property. This is stored so that if the game cannot be found temporarily, then its record is retained until the game is found.</summary>
		public Guid GameGuid { get; private set; }

		/// <summary>Get the <see cref="Game"/>'s <see cref="Asset.DisplayName"/> property. This is stored so that if the <see cref="Game"/> cannot be found, then we still know what its record is.</summary>
		public string GameName { get; private set; }

		/// <summary>Get the path to the <see cref="Game"/>.</summary>
		public string Path { get; private set; }

		/// <summary>Initialise the instance.</summary>
		/// <param name="game"></param>
		/// <param name="path"></param>
		public GameInstance(Game game, string path) {
			if (game == null)
				throw new ArgumentNullException("game");
			if (path == null)
				throw new ArgumentNullException("path");

			Game = game;
			Path = path;
			GameGuid = game.Guid;
			GameName = game.DisplayName;
		}

		/// <summary>Decode the instance from the source text.</summary>
		/// <param name="source"></param>
		public GameInstance(string source) {
			string[] parts = source.Split('\t');
			int version = int.Parse(parts[0]);

			if (version < 0 || version > CurrentSourceVersion)
				throw new NotSupportedException("Version " + version + " is out of range (0 to " + CurrentSourceVersion + ").");
			if (version == 0) {
				GameGuid = new Guid(parts[1]);
				GameName = parts[2].Replace("\\\t", "\t").Replace("\\\\", "\\");
			} else
				throw new NotImplementedException();
		}

		/// <summary>Convert to a string representation of the <see cref="GameInstance"/>, which is <c>"<see cref="CurrentSourceVersion"/>\t<see cref="GameGuid"/>\t<see cref="GameName"/>\t<see cref="Path"/>"</c>.</summary>
		/// <returns></returns>
		public string ToSource() { return "0\t" + GameGuid.ToString() + "\t" + GameName.Replace("\\", "\\\\").Replace("\t", "\\t") + "\t" + Path; }
	}
}
