using Alexandria.Resources;
using Glare;
using Glare.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.General {
	public class Plugin : Alexandria.Plugin {
		public Plugin(Manager manager)
			: base(manager, Properties.Resources.ResourceManager) {
			new LuaLoader(this);
			new GlareResourceLoader(this);
			Graphics.SetupResourceManager();
		}

		class GlareResourceLoader : Loader {
			public GlareResourceLoader(Plugin plugin) : base(plugin) { }

			public override LoaderMatchLevel Match(BinaryReader reader, string name, LoaderFileOpener opener, Resource context) {
				return ResourceLoader.Identify(name, reader.BaseStream) != null ? LoaderMatchLevel.Weak : LoaderMatchLevel.None;
			}

			public override Resource Load(BinaryReader reader, string name, LoaderFileOpener opener, Resource context) {
				object result = ResourceLoader.Load(name, reader.BaseStream, true);

				if (result is Glare.Graphics.Texture)
					return new Resources.Texture(Manager, (Glare.Graphics.Texture)result, name);
				throw new Exception("Cannot convert " + result.GetType().Name + " to a " + typeof(Resource).Name + ".");
			}
		}
	}
}
