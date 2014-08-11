using Glare.Assets;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.Infinity {
	/// <summary>
	/// Archive file format, likely with some meaning like "Black Isle File Format".
	/// </summary>
	public class BiffArchive : ArchiveAsset {
		public const string Magic = "BIFFV1  ";

		internal BiffArchive(AssetLoader loader)
			: base(loader) {
			BinaryReader reader = loader.Reader;

			loader.ExpectMagic(Magic);
			int recordCount = reader.ReadInt32();
			int tilesetCount = reader.ReadInt32();
			uint recordOffset = reader.ReadUInt32();

			reader.BaseStream.Position = recordOffset;
			for (int index = 0; index < recordCount; index++)
				new BiffArchiveRecord(this, index, loader);

		}
	}

	public abstract class BiffArchiveObject : DataAsset {
		public int Id { get; protected set; }

		public int Index { get; private set; }

		public uint Offset { get; protected set; }

		public BiffArchiveRecordType Type { get; protected set; }

		internal BiffArchiveObject(BiffArchive archive, int index)
			: base(archive, "") {
			Index = index;
		}
	}

	public class BiffArchiveRecord : BiffArchiveObject {
		public int Size { get; private set; }

		internal BiffArchiveRecord(BiffArchive archive, int index, AssetLoader loader)
			: base(archive, index) {
			BinaryReader reader = loader.Reader;

			Id = reader.ReadInt32();
			Offset = reader.ReadUInt32();
			Size = reader.ReadInt32();
			Type = (BiffArchiveRecordType)reader.ReadUInt16();
			Unknowns.ReadInt16s(reader, 1);
		}

		public override Stream Open() {
			throw new NotImplementedException();
		}
	}

	public class BiffArchiveTileset : BiffArchiveObject {
		internal BiffArchiveTileset(BiffArchive archive, int index, AssetLoader loader)
			: base(archive, index) {
			BinaryReader reader = loader.Reader;

			Id = reader.ReadInt32();
			Offset = reader.ReadUInt32();
			int tileCount = reader.ReadInt32();
			int tileSize = reader.ReadInt32();
			Type = (BiffArchiveRecordType)reader.ReadUInt16();
		}

		public override Stream Open() {
			throw new NotImplementedException();
		}
	}

	public enum BiffArchiveRecordType : ushort {
		/// <summary>Microsoft bitmap file format (".bmp" extension). Often used for storing palettes, in which case it's a 1x1 pixel image.</summary>
		BMP = 0x0001,

		MVE = 0x0002,

		/// <summary>RIFF wave sound files (".wav" extension).</summary>
		WAV = 0x0004,

		WFX = 0x0005,

		/// <summary>Paper doll bitmap files.</summary>
		PLT = 0x0006,

		/// <summary>Animations.</summary>
		BAM = 0x03E8,

		/// <summary>Region graphics.</summary>
		WED = 0x03E9,

		/// <summary>GUI elements.</summary>
		CHU = 0x03EA,

		/// <summary>Tileset information.</summary>
		TIS = 0x03EB,

		/// <summary>GUI backgrounds and region maps.</summary>
		MOS = 0x03EC,

		/// <summary>Inventory objects.</summary>
		ITM = 0x03ED,

		/// <summary>Spells.</summary>
		SPL = 0x03EE,

		/// <summary>Compiled script files.</summary>
		BCS = 0x03EF,

		/// <summary>Enumeration mappings.</summary>
		IDS = 0x03F0,

		/// <summary>Creatures.</summary>
		CRE = 0x03F1,

		/// <summary>Area descriptions.</summary>
		ARE = 0x03F2,

		/// <summary>Dialogues.</summary>
		DLG = 0x03F3,

		/// <summary>Two-dimensional information arrays.</summary>
		_2DA = 0x03F4,

		/// <summary>Save game files.</summary>
		GAM = 0x03F5,

		/// <summary>Store information.</summary>
		STO = 0x03F6,

		/// <summary>World map.</summary>
		WMP = 0x03F7,

		/// <summary>Exported player characters. This is replaced with <see cref="EFF"/> in Tales of the Sword Coast, Icewind Dale 1 and 2, and Baldur's Gate 2.</summary>
		CHR = 0x03F8,

		/// <summary>Effect descriptions. This replaces <see cref="CHR"/> in Tales of the Sword Coast, Icewind Dale 1 and 2, and Baldur's Gate 2.</summary>
		EFF = 0x03F8,

		/// <summary>Character control scripts.</summary>
		BS = 0x03F9,

		/// <summary>Exported player characters in Tales of the Sword Coast, Icewind Dale 1 and 2, and Baldur's Gate 2.</summary>
		CHR2 = 0x03FA,

		/// <summary>Visual spell effects.</summary>
		VVC = 0x03FB,

		/// <summary>Visual effects (Baldur's Gate 2 only).</summary>
		VEF = 0x03FC,

		/// <summary>Projectiles.</summary>
		PRO = 0x03FD,

		/// <summary>Character biographies (Baldur's Gate 2 only).</summary>
		BIO = 0x03FE,

		/// <summary>Unknown (Baldur's Gate 2 only).</summary>
		BAH = 0x044C,

		/// <summary>Initialisation files.</summary>
		INI = 0x0802,

		/// <summary>Text over people's heads (Planescape: Torment only).</summary>
		SRC = 0x0803,
	}

	public class BiffArchiveFormat : AssetFormat {
		public BiffArchiveFormat(Engine engine)
			: base(engine, typeof(BiffArchive), canLoad: true, extension: ".bif") {
		}

		public override LoadMatchStrength LoadMatch(AssetLoader loader) {
			if (loader.Reader.MatchMagic(BiffArchive.Magic))
				return LoadMatchStrength.Medium;
			return LoadMatchStrength.None;
		}

		public override Asset Load(AssetLoader loader) {
			return new BiffArchive(loader);
		}
	}
}
