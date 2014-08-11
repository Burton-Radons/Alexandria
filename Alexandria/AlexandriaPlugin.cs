using Glare.Assets;
using Glare.Framework;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria {
	/// <summary>
	/// A plugin for Alexandria. This includes <see cref="Game"/> and <see cref="Engine"/> support.
	/// </summary>
	public abstract class AlexandriaPlugin : AssetPlugin {
		readonly Codex<Engine> EnginesMutable = new Codex<Engine>();
		readonly Codex<Game> GamesMutable = new Codex<Game>();

		/// <summary>Iterate over all of the enabled <see cref="AssetFormat"/>s of this <see cref="AlexandriaPlugin"/>, the <see cref="Engines"/>, and the <see cref="Games"/>.</summary>
		public override IEnumerable<AssetFormat> AllEnabledFormats {
			get {
				foreach (AssetFormat format in Formats)
					yield return format;
				foreach (Engine engine in Engines)
					foreach (AssetFormat format in engine.AllEnabledFormats)
						yield return format;
				foreach (Game game in Games)
					foreach (AssetFormat format in game.AllEnabledFormats)
						yield return format;
			}
		}

		/// <summary>Iterate over all of the <see cref="Game"/>s in this <see cref="AlexandriaPlugin"/>, including those within <see cref="Engine"/>s.</summary>
		public IEnumerable<Game> AllGames {
			get {
				foreach (Game game in Games)
					yield return game;
				foreach (Engine engine in Engines)
					foreach (Game game in Games)
						yield return game;
			}
		}

		/// <summary>
		/// Get the collection of game <see cref="Engine"/>s.
		/// </summary>
		public ReadOnlyCodex<Engine> Engines { get { return EnginesMutable; } }

		/// <summary>Get the collection of <see cref="Game"/>s that don't have an engine.</summary>
		public ReadOnlyCodex<Game> Games { get { return GamesMutable; } }

		/// <summary>Initialise the plugin.</summary>
		/// <param name="manager">The <see cref="AssetManager"/> to use. This is actually an <see cref="AlexandriaManager"/>.</param>
		/// <param name="resourceManager">The resource manager used to retrieve names and descriptions of <see cref="Engine"/>s and <see cref="Game"/>s.</param>
		public AlexandriaPlugin(AssetManager manager, ResourceManager resourceManager) : base(manager, resourceManager) { }

		/// <summary>Add an engine to this plugin.</summary>
		/// <param name="engine">The engine to add.</param>
		/// <exception cref="ArgumentNullException"><paramref name="engine"/> is <c>null</c>.</exception>
		/// <exception cref="ArgumentException"><paramref name="engine"/> is not part of this <see cref="AlexandriaPlugin"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="engine"/> is already in this <see cref="AlexandriaPlugin"/>.</exception>
		protected void AddEngine(Engine engine) {
			if (engine == null)
				throw new ArgumentNullException("engine");
			if (engine.Plugin != this)
				throw new ArgumentException(engine.Name + " is not part of this " + Name + ".");
			if (EnginesMutable.Contains(engine))
				throw new ArgumentException(engine.Name + " is already added to this " + Name + ".");
			EnginesMutable.Add(engine);
		}

		/// <summary>Add a game to this plugin.</summary>
		/// <param name="game">The game to add.</param>
		/// <exception cref="ArgumentNullException"><paramref name="game"/> is <c>null</c>.</exception>
		/// <exception cref="ArgumentException"><paramref name="game"/> is not part of this <see cref="AlexandriaPlugin"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="game"/> is already in this <see cref="AlexandriaPlugin"/>.</exception>
		protected void AddGame(Game game) {
			if (game == null)
				throw new ArgumentNullException("game");
			if (game.Plugin != this)
				throw new ArgumentException(game.Name + " is not part of this " + Name + ".");
			if (GamesMutable.Contains(game))
				throw new ArgumentException(game.Name + " is already added to this " + Name + ".");
			GamesMutable.Add(game);
		}
	}

}
