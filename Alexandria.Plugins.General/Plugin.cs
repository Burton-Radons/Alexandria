using Alexandria.Resources;
using Glare;
using Glare.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Plugins.General {
	public class Plugin : Alexandria.Plugin {
		public Plugin(Manager manager)
			: base(manager, Properties.Resources.ResourceManager) {
			AddFormat(new LuaFormat(this));
			AddFormat(new GlareResourceFormat(this));
			AddFormat(new BinaryFormat(this));
			Graphics.SetupResourceManager();
		}

		class BinaryFormat : ResourceFormat {
			public BinaryFormat(Plugin plugin) : base(plugin, typeof(Binary), canLoad: true) { }

			public override LoadMatchStrength LoadMatch(LoadInfo context) {
				return LoadMatchStrength.Fallback - 2;
			}

			public override Resource Load(LoadInfo context) {
				return new Binary(Manager, context.Name, context.Reader.ReadBytes(checked((int)context.Length)), 0);
			}
		}

		class GlareResourceFormat : ResourceFormat {
			public GlareResourceFormat(Plugin plugin) : base(plugin, typeof(Resource), canLoad: true) { }

			public override LoadMatchStrength LoadMatch(LoadInfo context) {
				var name = context.Name;
				var stream = context.Reader.BaseStream;
				return ResourceLoader.Identify(name, stream) != null ? LoadMatchStrength.Weak : LoadMatchStrength.None;
			}

			public override Resource Load(LoadInfo context) {
				object result = ResourceLoader.Load(context.Name, context.Reader.BaseStream, true);

				if (result is Glare.Graphics.Texture)
					return new Resources.Texture(Manager, (Glare.Graphics.Texture)result, context.Name);
				throw new Exception("Cannot convert " + result.GetType().Name + " to a " + typeof(Resource).Name + ".");
			}
		}
	}
}
