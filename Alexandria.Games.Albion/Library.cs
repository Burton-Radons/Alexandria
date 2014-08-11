using Glare.Assets;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Games.Albion {
	/// <summary>A library asset.</summary>
	public class Library : ArchiveAsset {
		internal const string Magic = "XLD0I\0";

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

	/// <summary>
	/// A record in a <see cref="Library"/>.
	/// </summary>
	public class LibraryRecord : DataAsset {
		/// <summary>Get the display name, of the form "<see cref="Index"/> (<see cref="Size"/> byte(s))".</summary>
		public override string DisplayName {
			get {
				return string.Format("{0} ({1} byte{2})", Name, Size, Size == 1 ? "" : "s");
			}
		}

		/// <summary>Get the zero-based index of this record in the library.</summary>
		public int Index { get; private set; }

		/// <summary>Get the containing library.</summary>
		public Library Library { get; private set; }

		/// <summary>Get the offset of this record in the library file.</summary>
		public int Offset { get; private set; }

		/// <summary>Get the size in bytes of the record.</summary>
		public int Size { get; private set; }

		internal LibraryRecord(Library library, int index, int offset, int size)
			: base(library, index.ToString()) {
			Library = library;
			Offset = offset;
			Size = size;
		}

		/// <summary>Open the record data in a stream.</summary>
		/// <returns></returns>
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
