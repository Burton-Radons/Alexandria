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
	public abstract class Engine : PluginFormatAsset {
		readonly Codex<Game> GamesMutable = new Codex<Game>();

		public override IEnumerable<AssetFormat> AllFormats {
			get {
				foreach (var format in base.AllFormats)
					yield return format;
				foreach (var game in Games)
					foreach (var format in game.AllFormats)
						yield return format;
			}
		}

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public ReadOnlyCodex<Game> Games { get { return GamesMutable; } }

		public Engine(AssetPlugin plugin)
			: base(plugin) {
		}

		protected void AddGame(Game game) {
			if (game == null)
				throw new ArgumentNullException("game");
			if (GamesMutable.Contains(game))
				throw new ArgumentException(game.Name + " is already added to this " + Name + ".");
			if (game.Engine != this)
				throw new ArgumentException(game.Name + " does not have this " + Name + " as its Engine.");
			GamesMutable.Add(game);
		}

		public virtual void Detect(GameInstanceList instances) {
		}
	}
}
