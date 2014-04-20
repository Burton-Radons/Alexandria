using Alexandria.Engines.Sciagi.Resources;
using Glare.Assets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.Sciagi {
	public class Resource : DataAsset {
		public int CombinedIndex { get { return Id.CombinedIndex; } }

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

		public Resource(FolderAsset parent, ResourceMap map, ResourceId id)
			: base(parent, id.ToString()) {
			Map = map;
			Id = id;
		}

		public override System.IO.Stream Open() {
			var reader = Map.OpenPage(Id.Page);
			reader.BaseStream.Position = Id.Offset;

			ResourceType type;
			int id, compressedSize, uncompressedSize;
			CompressionMethod compressionMethod;
			int compressionCode;

			switch (Id.Version) {
				case ResourceMapVersion.Sci0:
					id = reader.ReadUInt16();
					compressedSize = reader.ReadUInt16() - 4;
					uncompressedSize = reader.ReadUInt16();
					compressionCode = reader.ReadUInt16();

					if (id != CombinedIndex)
						throw new Exception("Non-matching index.");

					switch (compressionCode) {
						case 0: compressionMethod = CompressionMethod.None; break;
						case 1: compressionMethod = CompressionMethod.Lzw; break;
						case 2: compressionMethod = CompressionMethod.Huffman; break;
						default: throw new NotSupportedException("Compression mode " + compressionCode + " for engine " + Map.EngineVersion + " is not supported.");

					}
					break;

				case ResourceMapVersion.Sci1:
					type = (ResourceType)reader.ReadByte();
					id = reader.ReadUInt16();
					compressedSize = reader.ReadUInt16();
					uncompressedSize = reader.ReadUInt16();
					compressionCode = reader.ReadUInt16();

					if(id != CombinedIndex)
						throw new Exception("Non-matching index.");
					if (type != (Id.Type | ResourceType.Sci1Mask))
						throw new InvalidDataException("Incorrect type.");

					switch (compressionCode) {
						case 0: compressionMethod = CompressionMethod.None; break;
						case 1: compressionMethod = CompressionMethod.Lzw; break;
						case 2: compressionMethod = CompressionMethod.Comp3; break;
						case 18:
						case 19:
						case 20:
							compressionMethod = CompressionMethod.DclImplode;
							break;
						default: throw new NotSupportedException("Compression code " + compressionCode + " for engine " + Id.Version + " is not supported.");
					}
					break;

				default:
					throw new NotImplementedException();
			}

			byte[] data = ResourceDecompressor.Decompress(reader.BaseStream, compressedSize, uncompressedSize, compressionMethod);
			return new MemoryStream(data, false);
		}

		protected override Asset Load() {
			using (BinaryReader reader = OpenReader()) {
				var loader = new AssetLoader(reader, Name, FileManager, this);

				switch (Id.Type) {
					case ResourceType.Picture: return new Picture(reader, this);
					case ResourceType.Script: return new Script(loader);
					case ResourceType.Text: return new Text(reader, this);
					case ResourceType.View: return new View(reader, this);
					case ResourceType.Message: return new Message(reader, this);
					default: return new BinaryAsset(Manager, Name, reader.ReadBytes(checked((int)reader.BaseStream.Length)));
					//default: throw new NotSupportedException("Resource type " + Id.Type + " is not supported.");
				}
			}
		}
	}
}
