using Glare.Assets;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Games.Albion {
	public class Library : ArchiveAsset {
		public const string Magic = "XLD0I\0";

		internal BinaryReader Reader { get; private set; }

		internal Library(AssetManager manager, AssetLoader loader)
			: base(manager, loader.Name) {
			var reader = Reader = loader.Reader;

			loader.ExpectMagic(Magic);
			var count = reader.ReadUInt16();
			int offset = 8 + count * 4;
			for (int index = 0; index < count; index++) {
				int size = reader.ReadInt32();
				new LibraryRecord(this, index, offset, size);
				offset += size;
			}
		}
	}

	public class LibraryRecord : DataAsset {
		public override string DisplayName {
			get {
				return string.Format("{0} ({1} byte{2})", Name, Size, Size == 1 ? "" : "s");
			}
		}

		public int Index { get; private set; }

		public Library Library { get; private set; }

		public int Offset { get; private set; }

		public int Size { get; private set; }

		internal LibraryRecord(Library library, int index, int offset, int size)
			: base(library, index.ToString()) {
			Library = library;
			Offset = offset;
			Size = size;
		}

		public override Stream Open() {
			var reader = Library.Reader;
			reader.BaseStream.Position = Offset;
			return new MemoryStream(reader.ReadBytes(Size), false);
		}
	}

	class LibraryFormat : AssetFormat {
		public LibraryFormat(Game game)
			: base(game, typeof(Library), extension: ".xld", canLoad: true) {
		}

		public override LoadMatchStrength LoadMatch(AssetLoader loader) {
			return loader.Reader.MatchMagic(Library.Magic) ? LoadMatchStrength.Medium : LoadMatchStrength.None;
		}

		public override Asset Load(AssetLoader loader) {
			return new Library(Manager, loader);
		}
	}
}
