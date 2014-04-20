using Glare.Assets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.Sciagi {
	public enum ResourceMapVersion {
		None,
		Sci0,
		Sci1,
	}

	public class ResourceMap : FolderAsset {
		FileManager FileManager;
		readonly Dictionary<int, BinaryReader> Pages = new Dictionary<int, BinaryReader>();
		readonly string Path;

		public EngineVersion EngineVersion { get { return EngineVersion.SCI0; } }

		public GameId GameId { get { return GameId.Unknown; } }

		public GraphicsMode GraphicsMode { get { return Sciagi.GraphicsMode.Ega; } }

		public ResourceMapVersion Version { get; private set; }

		public ResourceMap(AssetManager manager, AssetLoader loader)
			: base(manager, loader.Name) {
			var reader = loader.Reader;

			Path = loader.Name;
			FileManager = loader.FileManager;
			Version = DetectVersion(loader);

			Dictionary<ResourceType, FolderAsset> folders = new Dictionary<ResourceType, FolderAsset>();

			using (reader) {
				if (Version == ResourceMapVersion.Sci0) {
					while (true) {
						ResourceId id = new ResourceId(reader, Version);

						if (id.IsEnd)
							break;
						AddResource(id, folders);
					}
				} else if (Version == ResourceMapVersion.Sci1) {
					List<KeyValuePair<ResourceType, int>> types = new List<KeyValuePair<ResourceType, int>>();

					while (true) {
						ResourceType type = (ResourceType)reader.ReadByte();
						int offset = reader.ReadUInt16();

						types.Add(new KeyValuePair<ResourceType, int>(type == ResourceType.End ? type : (ResourceType)((int)type & 0x7F), offset));
						if (type == ResourceType.End)
							break;
					}

					for (int typeIndex = 0; typeIndex < types.Count - 1; typeIndex++) {
						ResourceType type = types[typeIndex].Key;
						int end = types[typeIndex + 1].Value;

						while (reader.BaseStream.Position < end) {
							ResourceId id = new ResourceId(reader, Version, type);
							AddResource(id, folders);
						}
					}
				} else
					throw new NotImplementedException();
			}

			SortChildrenRecursively();
		}

		void AddResource(ResourceId id, Dictionary<ResourceType, FolderAsset> folders) {
			foreach (FolderAsset childFolder in Children) {
				foreach (Resource resource in childFolder.Children) {
					if (resource.Id.Type == id.Type && resource.Id.Index == id.Index)
						return;
				}
			}

			FolderAsset folder;

			if (!folders.TryGetValue(id.Type, out folder))
				folder = folders[id.Type] = new FolderAsset(this, id.Type.ToString());

			new Resource(folder, this, id);
		}

		public BinaryReader OpenPage(int index) {
			BinaryReader reader;

			if (Pages.TryGetValue(index, out reader))
				return reader;

			string name = String.Format("resource.{0:d3}", index);
			string path = System.IO.Path.GetDirectoryName(Path) + "/" + name;
			reader = FileManager.OpenReader(path);
			Pages[index] = reader;
			return reader;
		}

		public static ResourceMapVersion DetectVersion(AssetLoader loader) {
			var reader = loader.Reader;
			long length = loader.Length;

			if (string.Compare(System.IO.Path.GetFileName(loader.Name), "resource.map", true) != 0)
				return ResourceMapVersion.None;

			// Try SCI0.
			bool isSci0 = DetectVersionSci0(loader);
			loader.Reset();
			if (isSci0)
				return ResourceMapVersion.Sci0;

			// Try SCI1.
			bool isSci1 = DetectVersionSci1(loader);
			loader.Reset();
			if (isSci1)
				return ResourceMapVersion.Sci1;

			return ResourceMapVersion.None;
		}

		static bool DetectVersionSci0(AssetLoader loader) {
			var version = ResourceMapVersion.Sci0;
			var reader = loader.Reader;
			var length = loader.Length;

			// File is a simple list of 6-byte resource ids.
			if (length % 6 != 0 || length / 6 > short.MaxValue)
				return false;
			int count = (int)(length / 6);
			for (int index = 0; index < count - 1; index++) {
				ResourceId id = new ResourceId(reader, version);
				if (id.IsEnd)
					return false;
			}

			ResourceId end = new ResourceId(reader, version);
			if (!end.IsEnd)
				return false;
			return true;
		}

		static bool DetectVersionSci1(AssetLoader loader) {
			var reader = loader.Reader;
			var length = loader.Length;

			// File starts with three-byte resource type headers; each (u1 type; u2 offset), where type ends in 255.
			int firstOffset = -1, lastOffset = 0;

			while (true) {
				if (reader.BaseStream.Position + 3 >= length)
					return false;
				ResourceType type = (ResourceType)reader.ReadByte();
				int offset = reader.ReadUInt16();
				if (firstOffset < 0)
					firstOffset = offset;
				else if (offset < lastOffset || (offset - lastOffset) % 6 != 0)
					return false;

				lastOffset = offset;
				if (type == ResourceType.End)
					break;
			}

			// The remainder are sorted resource lists for each type, but this is enough for positive detection.
			return reader.BaseStream.Position == firstOffset && lastOffset == length;
		}
	}

	class ResourceMapFormat : AssetFormat {
		public ResourceMapFormat(Plugin plugin)
			: base(plugin, typeof(ResourceMap), canLoad: true) {
		}

		public override LoadMatchStrength LoadMatch(AssetLoader loader) {
			return ResourceMap.DetectVersion(loader) == ResourceMapVersion.None ? LoadMatchStrength.None : LoadMatchStrength.Medium;
		}

		public override Asset Load(AssetLoader loader) {
			return new ResourceMap(Manager, loader);
		}
	}
}
