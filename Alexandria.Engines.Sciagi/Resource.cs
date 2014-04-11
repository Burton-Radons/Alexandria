using Alexandria.Engines.Sciagi.Resources;
using Alexandria.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.Sciagi {
	public class Resource : Asset {
		public int CombinedIndex { get { return Id.GetCombinedIndex(Map.EngineVersion); } }

		public override string DisplayName {
			get {
				return base.DisplayName + " (page " + Id.Page + ")";
			}
		}

		public EngineVersion EngineVersion { get { return Map.EngineVersion; } }

		public GameId GameId { get { return Map.GameId; } }

		public GraphicsMode GraphicsMode { get { return Map.GraphicsMode; } }

		public ResourceId Id { get; private set; }

		public ResourceMap Map { get; private set; }

		public Resource(Folder parent, ResourceMap map, ResourceId id)
			: base(parent, id.ToString()) {
			Map = map;
			Id = id;
		}

		public override System.IO.Stream Open() {
			var reader = Map.OpenPage(Id.Page);
			reader.BaseStream.Position = Id.Offset;

			int id = reader.ReadUInt16();
			int compressedSize = reader.ReadUInt16() - 4;
			int uncompressedSize = reader.ReadUInt16();
			CompressionMethod compressionMethod;
			int compressionCode = reader.ReadUInt16();

			if (id != CombinedIndex)
				throw new Exception("Non-matching index.");

			if (compressionCode == 0)
				compressionMethod = CompressionMethod.None;
			else
				switch (Map.EngineVersion) {
					case EngineVersion.SCI0:
						switch (compressionCode) {
							case 0: compressionMethod = CompressionMethod.None; break;
							case 1: compressionMethod = CompressionMethod.LZW; break;
							case 2: compressionMethod = CompressionMethod.Huffman; break;
							default: throw new NotSupportedException("Compression mode " + compressionCode + " for engine " + Map.EngineVersion + " is not supported.");

						}
						break;

					default:
						throw new NotImplementedException();
				}

			byte[] data = ResourceDecompressor.Decompress(reader.BaseStream, compressedSize, uncompressedSize, compressionMethod);
			return new MemoryStream(data, false);
		}

		protected override Alexandria.Resource Load() {
			using (BinaryReader reader = OpenReader()) {
				switch (Id.Type) {
					case ResourceType.Picture: return new Picture(reader, this);
					case ResourceType.Text: return new Text(reader, this);
					case ResourceType.View: return new View(reader, this);
					default: throw new NotSupportedException("Resource type " + Id.Type + " is not supported.");
				}
			}
		}
	}
}
