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
			new ResourceMapLoader(this);
		}

		class ResourceMapLoader : Loader {
			public ResourceMapLoader(Plugin plugin)
				: base(plugin) {
			}

			public override LoaderMatchLevel Match(BinaryReader reader, string name, LoaderFileOpener opener, Alexandria.Resource context) {
				long length = reader.BaseStream.Length;

				if (string.Compare(Path.GetFileName(name), "resource.map", true) != 0)
					return LoaderMatchLevel.None;

				if (length % 6 != 0 || length / 6 > short.MaxValue)
					return LoaderMatchLevel.None;
				int count = (int)(length / 6);
				for (int index = 0; index < count - 1; index++) {
					ResourceId id = new ResourceId(reader);
					if (id.IsEnd)
						return LoaderMatchLevel.None;
				}

				ResourceId end = new ResourceId(reader);
				if (!end.IsEnd)
					return LoaderMatchLevel.None;
				return LoaderMatchLevel.Medium;
			}

			public override Alexandria.Resource Load(BinaryReader reader, string name, LoaderFileOpener opener, Alexandria.Resource context) {
				return new ResourceMap(Manager, reader, name, opener);
			}
		}
	}
}
