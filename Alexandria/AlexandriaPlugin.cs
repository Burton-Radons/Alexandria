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
	public abstract class AlexandriaPlugin : AssetPlugin {
		readonly RichList<Engine> EnginesMutable = new RichList<Engine>();
		readonly RichList<Game> GamesMutable = new RichList<Game>();

		public override IEnumerable<AssetFormat> AllFormats {
			get {
				foreach (AssetFormat format in Formats)
					yield return format;
				foreach (Engine engine in Engines)
					foreach (AssetFormat format in engine.AllFormats)
						yield return format;
				foreach (Game game in Games)
					foreach (AssetFormat format in game.AllFormats)
						yield return format;
			}
		}

		public ReadOnlyList<Engine> Engines { get { return EnginesMutable; } }

		public ReadOnlyList<Game> Games { get { return GamesMutable; } }

		public AlexandriaPlugin(AssetManager manager, ResourceManager resourceManager) : base(manager, resourceManager) { }

		protected void AddEngine(Engine engine) {
			if (engine == null)
				throw new ArgumentNullException("engine");
			if (engine.Plugin != this)
				throw new ArgumentException(engine.Name + " is not part of this " + Name + ".");
			if (EnginesMutable.Contains(engine))
				throw new ArgumentException(engine.Name + " is already added to this " + Name + ".");
			EnginesMutable.Add(engine);
		}

		protected void AddGame(Game game) {
			if (game == null)
				throw new ArgumentNullException("game");
			if (GamesMutable.Contains(game))
				throw new ArgumentException(game.Name + " is already added to this " + Name + ".");
			GamesMutable.Add(game);
		}
	}

}
