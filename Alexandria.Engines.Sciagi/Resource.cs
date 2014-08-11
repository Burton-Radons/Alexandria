using Alexandria.Engines.Sciagi.Resources;
using Glare.Assets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.Sciagi {
	/// <summary>
	/// A resource in a <see cref="ResourceMap"/>.
	/// </summary>
	public class Resource : DataAsset {
		/// <summary>
		/// Get the <see cref="Id"/> <see cref="ResourceId"/>'s <see cref="ResourceId.CombinedIndex"/>, which is the <see cref="ResourceId.Type"/> combined with the <see cref="ResourceId.Id"/> for SCI0 and earlier, but just <see cref="ResourceId.Id"/> for any other version.
		/// </summary>
		public int CombinedIndex { get { return Id.CombinedIndex; } }

		/// <summary>Get the name of the resource, which is "<see cref="ResourceId.Type"/> <see cref="ResourceId.Id"/> (page <see cref="ResourceId.Page"/>)", with the page omitted if it's zero.</summary>
		public override string DisplayName {
			get {
				string text = base.DisplayName;
				if (Id.Page != 0)
					text += string.Format(" (page {0})", Id.Page);
				return text;
			}
		}

		/// <summary>
		/// Get the <see cref="ResourceMap"/>'s <see cref="ResourceMap.EngineVersion"/> <see cref="Alexandria.Engines.Sciagi.EngineVersion"/> of <see cref="Map"/>.
		/// </summary>
		public EngineVersion EngineVersion { get { return Map.EngineVersion; } }

		/// <summary>
		/// Get the game this is for.
		/// </summary>
		public GameId GameId { get { return Map.GameId; } }

		/// <summary>
		/// Get the graphics mode this is in.
		/// </summary>
		public GraphicsMode GraphicsMode { get { return Map.GraphicsMode; } }

		/// <summary>
		/// Get the resource identifier.
		/// </summary>
		public ResourceId Id { get; private set; }

		/// <summary>
		/// Get the resource map.
		/// </summary>
		public ResourceMap Map { get; private set; }

		/// <summary>
		/// Initialise the resource.
		/// </summary>
		/// <param name="parent"></param>
		/// <param name="map"></param>
		/// <param name="id"></param>
		public Resource(FolderAsset parent, ResourceMap map, ResourceId id)
			: base(parent, id.ToString()) {
			Map = map;
			Id = id;
		}

		/// <summary>Open the resource data as a stream, decompressing it as necessary.</summary>
		/// <returns></returns>
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

				case ResourceMapVersion.Sci2:
					type = (ResourceType)reader.ReadByte();
					id = reader.ReadUInt16();
					compressedSize = reader.ReadInt32();
					uncompressedSize = reader.ReadInt32();
					compressionCode = reader.ReadUInt16();

					if (id != CombinedIndex)
						throw new InvalidDataException("Non-matching index.");
					if (type != Id.FullType)
						throw new InvalidDataException(string.Format("Incorrect type found in the resource data; {0} was expected but {1} was found.", Id.FullType, type));

					switch (compressionCode) {
						case 32: compressionMethod = CompressionMethod.Lzs; break;
						default: throw new NotSupportedException(string.Format("Compression code " + compressionCode + " for engine " + Id.Version + " is not supported (Page file=\"{0}\", Offset={1:X}h).", Map.GetPagePath(Id.Page), reader.BaseStream.Position));
					}
					break;

				default:
					throw new NotImplementedException("Operation not implemented for version " + Id.Version);
			}

			byte[] data = ResourceDecompressor.Decompress(reader.BaseStream, compressedSize, uncompressedSize, compressionMethod);
			return new MemoryStream(data, false);
		}

		/// <summary>
		/// Provides a custom loader to load the files.
		/// </summary>
		/// <returns></returns>
		protected override Asset Load() {
			using (BinaryReader reader = OpenReader()) {
				var loader = new AssetLoader(Manager, reader, Name, FileManager, this);

				switch (Id.Type) {
					case ResourceType.Palette: return new Palette(loader);
					case ResourceType.Picture: return new Picture(loader);
					case ResourceType.Script: return new Script(loader);
					case ResourceType.Text: return new Text(loader);
					case ResourceType.View: return new View(loader);
					case ResourceType.Message: return new Message(loader);
					default: return new BinaryAsset(Manager, Name, reader.ReadBytes(checked((int)reader.BaseStream.Length)));
					//default: throw new NotSupportedException("Resource type " + Id.Type + " is not supported.");
				}
			}
		}
	}
}
