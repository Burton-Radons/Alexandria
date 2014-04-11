using Alexandria.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.Sciagi {
	public enum ResourceMapVersion {
		None,
		Sci0Sci1Early,
		Sci1Middle,
		KingsQuest5FmTowns,
		Sci1Late,
		Sci11,
		Sci11Mac,
		Sci2,
		Sci3
	}

	public class ResourceMap : Folder {
		LoaderFileOpener Opener;
		readonly Dictionary<int, BinaryReader> Pages = new Dictionary<int, BinaryReader>();
		readonly string Path;

		public EngineVersion EngineVersion { get { return EngineVersion.SCI0; } }

		public GameId GameId { get { return GameId.Unknown; } }

		public GraphicsMode GraphicsMode { get { return Sciagi.GraphicsMode.Ega; } }

		public ResourceMap(Manager manager, BinaryReader reader, string name, LoaderFileOpener opener)
			: base(manager, name) {
			Path = name;
			Opener = opener;

			Dictionary<ResourceType, Folder> folders = new Dictionary<ResourceType, Folder>();

			using (reader)
				while (true) {
					ResourceId id = new ResourceId(reader);

					if (id.IsEnd)
						break;

					bool isDuplicate = false;

					foreach (Folder childFolder in Children) {
						foreach (Resource resource in childFolder.Children) {
							if (resource.Id.Type == id.Type && resource.Id.Index == id.Index) {
								isDuplicate = true;
								break;
							}
						}

						if (isDuplicate)
							break;
					}

					if (isDuplicate)
						continue;

					Folder folder;

					if (!folders.TryGetValue(id.Type, out folder))
						folder = folders[id.Type] = new Folder(this, id.Type.ToString());

					new Resource(folder, this, id);
				}

			SortChildrenRecursively();
		}

		public BinaryReader OpenPage(int index) {
			BinaryReader reader;

			if (Pages.TryGetValue(index, out reader))
				return reader;

			string name = String.Format("resource.{0:d3}", index);
			string path = System.IO.Path.GetDirectoryName(Path) + "/" + name;
			Stream stream = Opener(path);
			reader = new BinaryReader(stream);
			Pages[index] = reader;
			return reader;
		}
	}
}
