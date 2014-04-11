using Glare.Internal;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.Sciagi {
	public class Plugin : Alexandria.Plugin {
		internal static ResourceManager OurResourceManager { get { return Properties.Resources.ResourceManager; } }

		public Plugin(Manager manager)
			: base(manager, OurResourceManager) {
			AddFormat(new ResourceMapFormat(this));
		}

		class ResourceMapFormat : ResourceFormat {
			public ResourceMapFormat(Plugin plugin)
				: base(plugin, typeof(ResourceMap), canLoad: true) {
			}

			public override LoadMatchStrength LoadMatch(LoadInfo info) {
				var reader = info.Reader;
				long length = info.Length;

				if (string.Compare(Path.GetFileName(info.Name), "resource.map", true) != 0)
					return LoadMatchStrength.None;

				if (length % 6 != 0 || length / 6 > short.MaxValue)
					return LoadMatchStrength.None;
				int count = (int)(length / 6);
				for (int index = 0; index < count - 1; index++) {
					ResourceId id = new ResourceId(reader);
					if (id.IsEnd)
						return LoadMatchStrength.None;
				}

				ResourceId end = new ResourceId(reader);
				if (!end.IsEnd)
					return LoadMatchStrength.None;
				return LoadMatchStrength.Medium;
			}

			public override Alexandria.Resource Load(LoadInfo info) {
				return new ResourceMap(Manager, info.Reader, info.Name, info.Opener);
			}
		}
	}
}
