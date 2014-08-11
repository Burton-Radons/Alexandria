using Glare.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.Sciagi {
	/// <summary>
	/// Base class of all of the resource types.
	/// </summary>
	public abstract class ResourceData : Asset {
		/// <summary>Get the engine version.</summary>
		public EngineVersion EngineVersion { get { return Resource.EngineVersion; } }

		/// <summary>Get the game identifier.</summary>
		public GameId GameId { get { return Map.GameId; } }

		/// <summary>Get the graphics mode.</summary>
		public GraphicsMode GraphicsMode { get { return Map.GraphicsMode; } }

		/// <summary>Get the <see cref="ResourceMap"/>.</summary>
		public ResourceMap Map { get { return Resource.Map; } }

		/// <summary>Get the source <see cref="Resource"/>.</summary>
		public Resource Resource { get; private set; }

		/// <summary>Initialise the resource data.</summary>
		/// <param name="loader"></param>
		public ResourceData(AssetLoader loader) : base(loader.Context.Manager, Plugin.OurResourceManager) {
			Resource = (Resource)loader.Context;
		}
	}
}
