using Glare.Assets;
using Glare.Framework;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria {
	/// <summary>
	/// A common engine that has been used for a number of different games. The Unreal Engine is a good example of that.
	/// </summary>
	public abstract class Engine : AlexandriaPluginFormatAsset {
		readonly Codex<Game> GamesMutable = new Codex<Game>();

		/// <summary>
		/// Get all the of formats in all of the games in this <see cref="Engine"/>.
		/// </summary>
		public override IEnumerable<AssetFormat> AllEnabledFormats {
			get {
				foreach (var format in base.AllEnabledFormats)
					yield return format;
				foreach (var game in Games)
					foreach (var format in game.AllEnabledFormats)
						yield return format;
			}
		}

		/// <summary>Get all of the known games used by this engine.</summary>
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public ReadOnlyCodex<Game> Games { get { return GamesMutable; } }

		/// <summary>
		/// Initialise the engine.
		/// </summary>
		/// <param name="plugin">The plugin this is part of.</param>
		public Engine(AlexandriaPlugin plugin) : base(plugin) { }

		/// <summary>
		/// Add a game to the engine.
		/// </summary>
		/// <param name="game">The game to add.</param>
		protected void AddGame(Game game) {
			if (game == null)
				throw new ArgumentNullException("game");
			if (GamesMutable.Contains(game))
				throw new ArgumentException(game.Name + " is already added to this " + Name + ".");
			if (game.Engine != this)
				throw new ArgumentException(game.Name + " does not have this " + Name + " as its Engine.");
			GamesMutable.Add(game);
		}
	}
}
