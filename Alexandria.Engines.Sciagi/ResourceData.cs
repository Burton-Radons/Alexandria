using Glare.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.Sciagi {
	public abstract class ResourceData : Asset {
		public EngineVersion EngineVersion { get { return Resource.EngineVersion; } }

		public GameId GameId { get { return Map.GameId; } }

		public GraphicsMode GraphicsMode { get { return Map.GraphicsMode; } }

		public ResourceMap Map { get { return Resource.Map; } }

		public Resource Resource { get; private set; }

		public ResourceData(Resource resource) : base(resource.Manager, Plugin.OurResourceManager) {
			Resource = resource;
		}
	}
}
