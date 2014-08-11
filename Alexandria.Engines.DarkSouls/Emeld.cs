using Glare.Assets;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.DarkSouls {
	/// <summary>
	/// A ".emeld" file.
	/// </summary>
	public class Emeld : TableAsset<EmeldRow> {
		internal const string Magic = "ELD\0";

		internal Emeld(AssetLoader loader)
			: base(loader) {
			BinaryReader reader = loader.Reader;

			reader.MatchMagic(Magic);
			loader.ExpectZeroes(4, 1);
			loader.Expect((ushort)0x65);
			loader.Expect((ushort)0xCC);
			int totalFileSize = reader.ReadInt32();
			int tableCount = reader.ReadInt32();
			int tableOffset = reader.ReadInt32(); // Always 0x38
			loader.ExpectZeroes(4, 1); // Always empty table
			int unknown1Offset = reader.ReadInt32();
			loader.ExpectZeroes(4, 1); // Always empty table
			int unknown2Offset = reader.ReadInt32();
			int stringsLength = reader.ReadInt32();
			int stringsOffset = reader.ReadInt32();
			loader.ExpectZeroes(4, 2);

			loader.Position = tableOffset;
			for (int index = 0; index < tableCount; index++)
				new EmeldRow(this, index, loader, stringsOffset);
		}
	}

	/// <summary>A row in a ".emeld" file.</summary>
	public class EmeldRow : TableRowAsset {
		/// <summary>First unknown value.</summary>
		public ushort U1 { get; private set; }

		/// <summary>Second unknown value.</summary>
		public ushort U2 { get; private set; }

		/// <summary>A Japanese string value.</summary>
		public string String { get; private set; }

		/// <summary>A translation of the Japanese <see cref="String"/> value.</summary>
		public string TranslatedString { get { return Engine.GetTranslation(String); } }

		internal EmeldRow(Emeld emeld, int index, AssetLoader loader, int stringsOffset)
			: base(emeld, index) {
			BinaryReader reader = loader.Reader;

			U1 = reader.ReadUInt16();
			U2 = reader.ReadUInt16();
			int stringOffset = reader.ReadInt32();
			loader.ExpectZeroes(4, 1);

			long reset = loader.Position;
			String = reader.ReadStringzAt(stringsOffset + stringOffset, Encoding.Unicode);
			loader.Position = reset;
		}
	}

	/// <summary>The file format for a <see cref="Emeld"/> file, with a ".emeld" extension.</summary>
	public class EmeldFormat : AssetFormat {
		internal EmeldFormat(Engine engine) : base(engine, typeof(Emeld), canLoad: true, extension: ".emeld") { }

		/// <summary>Attempt to match the loader as a ".emeld" file.</summary>
		/// <param name="loader"></param>
		/// <returns></returns>
		public override LoadMatchStrength LoadMatch(AssetLoader loader) { return loader.Length > 4 && loader.Reader.MatchMagic(Emeld.Magic) ? LoadMatchStrength.Medium : LoadMatchStrength.None; }

		/// <summary>Load the <see cref="Emeld"/> file.</summary>
		/// <param name="loader"></param>
		/// <returns></returns>
		public override Asset Load(AssetLoader loader) { return new Emeld(loader); }
	}
}
