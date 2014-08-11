using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glare.Internal;
using Glare.Assets;
using System.Drawing;
using System.Drawing.Imaging;
using Glare;
using Glare.Framework;

/* Game, GameTitle, GameCode, MakerCode, Title
 * "Peter Jackson's King Kong", "KING KONG", "AKQE", 0x3134/"41", "Peter Jackson's\nKing Kong\nUbisoft"
 * "Phoenix Wright: Ace Attorney", "PHOENIX WRIG", "AGYP", 0x3830/"08", "Phoenix Wright\nAce Attorney\nCAPCOM"
 * "Ping Pals", "PING PALS", "APPP", 0x3837/"78", "Ping Pals\nTHQ"
 * "Pokemon Pearl", "POKEMON P", "APAE", "01", "Pokémon Pearl\nNintendo"
 * "Polarium", "POLARIUM", "ASNE", "01", "□■ POLARIUM ■□\nNintendo"
 * "Professor Layton and the Curious Village", "LAYTON1", "A5FP",0x3130 ("01"), "Professor Layton\nand the Curious Village\nNintendo"
 * "Professor Layton and the Diabolical Box", "LAYTON2", "YLTE", 0x3130/"01", "Professor Layton\nand the Diabolical Box\nNintendo"
 * "Rayman DS", "RAYMAN DS", "ARYP", 0x3134 ("41"), "Rayman DS\nUbisoft"
 * "Real Time Conflict - Shogun Empires", "SHOGUN WAR.", "ASWE", "AF", "REAL TIME CONFLICT:\nSHOGUN EMPIRES™\nNAMCO HOMETEK INC."
 * "Retro Atari Classics", "ATARICLASSIC", "ATAE", "70", "Retro Atari Classics™\nAtari, Inc."
 * "Robots", "ROBOTS", "ARBP", "7D", "Robots™\nVivendi Universal Games, Inc."
 * "Sega Casino", "SEGA CASINO", "ACAE", "8P", "SEGA CASINO\nSEGA"
 * "Scribblenauts 2", "SCRIBBLENAUT", "BSLE", "WR", "ScribbleNauts\nWB Games, Inc."
 * 
 */

namespace Alexandria.Platforms.NintendoDS {
	/// <summary>
	/// A ROM maker code.
	/// </summary>
	public enum RomMaker : ushort {
		/// <summary>Nintendo ("01").</summary>
		Nintendo = 0x3130,

		/// <summary>Capcom ("08").</summary>
		Capcom = 0x3830,

		/// <summary>Ubisoft ("41").</summary>
		Ubisoft = 0x3134,

		/// <summary>Atari, Inc. ("70").</summary>
		Atari = 0x3730,

		/// <summary>THQ ("78").</summary>
		THQ = 0x3837,

		/// <summary>Vivendi Universal Games, Inc. ("7D").</summary>
		Vivendi = 0x4437,

		/// <summary>Sega ("8P").</summary>
		Sega = 0x5038,

		/// <summary>Namco Hometek Inc. ("AF").</summary>
		Namco = 0x4641,

		/// <summary>WB Games, Inc. ("WR").</summary>
		WBGames = 0x5257,
	}

	/// <summary>A folder within a <see cref="Rom"/>.</summary>
	public class RomFolder : FolderAsset {
		int FileNameOffset;

		ushort FileIndex;

		byte Unknown2;

		internal bool Used;

		/// <summary>Get a display name for the folder.</summary>
		public override string DisplayName {
			get {
				return string.Format("{0} ({1})", Name, Unknown2);
			}
		}

		/// <summary>Get the zero-based index of this folder within the folder array.</summary>
		public int Index { get; private set; }

		/// <summary>Get the containing ROM.</summary>
		public Rom Rom { get; private set; }

		internal RomFolder(Rom rom, int index, BinaryReader reader)
			: base(rom, "Folder " + index) {
			Rom = rom;
			Index = index;

			FileNameOffset = reader.ReadInt32();
			FileIndex = reader.ReadUInt16();

			// For the root folder this is the total number of folders.
			// Sub-folders are masked 0xF000, and the value masked by 0xFFF is the index of the parent folder.
			reader.ReadUInt16();
				
			Used = false;
		}

		internal void Sort(FolderAsset parent, string name, int fileNameBaseOffset, byte[] fileNames) {
			int fileNameOffset = FileNameOffset - fileNameBaseOffset;
			int fileIndex = FileIndex;

			if (Used)
				throw new InvalidDataException();

			Used = true;
			Parent = parent;
			Name = name;

			while (true) {
				byte nameCode = fileNames[fileNameOffset++];
				if (nameCode == 0)
					break;
				byte nameLength = (byte)(nameCode & 0x7F);
				bool isFolder = (nameCode & 0x80) != 0;

				string childName = Encoding.ASCII.GetString(fileNames, fileNameOffset, nameLength);
				fileNameOffset += nameLength;

				if (isFolder) {
					int folderIndex = fileNames[fileNameOffset++] + (fileNames[fileNameOffset++] << 8);
					RomFolder folder = Rom.Folders[folderIndex & 0xFFF];
					folder.Unknown2 = (byte)(folderIndex >> 12);
					if (folder.Unknown2 != 15) {
					}
					folder.Sort(this, childName, fileNameBaseOffset, fileNames);
				} else {
					RomFile file = Rom.Files[fileIndex++];
					file.Sort(this, childName);
				}
			}
		}
	}

	/// <summary>A file within a <see cref="Rom"/>.</summary>
	public class RomFile : DataAsset {
		/// <summary>Get the starting offset.</summary>
		int StartOffset;

		/// <summary>Get the ending offset.</summary>
		int EndOffset;

		/// <summary>Get whether this record is used.</summary>
		internal bool Used;

		/// <summary>Get a display name for the file.</summary>
		public override string DisplayName {
			get {
				return string.Format("{0} (Offset {1:X}h, Size {2})", Name, StartOffset, EndOffset - StartOffset);
			}
		}

		/// <summary>Get the zero-based index of this file within the file array.</summary>
		public int Index { get; private set; }

		/// <summary>Get the containing Rom.</summary>
		public Rom Rom { get; private set; }

		internal RomFile(Rom rom, int index, BinaryReader reader)
			: base(rom, "File " + index) {
			Rom = rom;
			Index = index;

			StartOffset = reader.ReadInt32();
			EndOffset = reader.ReadInt32();
			Used = false;
		}

		/// <summary>
		/// Open the ROM file.
		/// </summary>
		/// <returns></returns>
		public override Stream Open() {
			int length = EndOffset - StartOffset;

			if (Name.EndsWith(".lzc")) {
				BinaryReader reader = Rom.Reader;
				reader.BaseStream.Position = StartOffset;
				return new MemoryStream(reader.DecompressData(EndOffset), false);
			}

			return new SliceStream(Rom.Reader.BaseStream, StartOffset, EndOffset - StartOffset, closeStream: false);
		}

		internal void Sort(FolderAsset parent, string name) {
			if (Used)
				throw new InvalidDataException();
			Parent = parent;
			Name = name;
			Used = true;
		}
	}

	/// <summary>
	/// A Nintendo DS ROM file.
	/// </summary>
	public class Rom : FolderAsset {
		internal readonly BinaryReader Reader;

		/// <summary>Get the title of the game in all caps, found at the beginning of the ROM.</summary>
		public String GameTitle { get; private set; }

		/// <summary>Get the game code, which is a four-character string.</summary>
		public String GameCode { get; private set; }

		/// <summary>Get the maker code, which is a two-character string.</summary>
		public String MakerCode { get; private set; }

		/// <summary>Get the unit code.</summary>
		public byte UnitCode { get; private set; }

		/// <summary>Get the device code.</summary>
		public byte DeviceCode { get; private set; }

		/// <summary>Get the card size base value, which is derived by <see cref="CardSize"/>.</summary>
		public byte CardSizeBase { get; private set; }

		/// <summary>Get the card size.</summary>
		public int CardSize { get { return 2 << (20 + CardSizeBase); } }

		/// <summary>Size in bytes of a page; seems to always be 0x4000 (16384 bytes).</summary>
		public int PageSize { get; private set; }

		/// <summary>Get the actual end of the data of the ROM; everything past this byte is zero or 0xFF. Though, some might be liars...</summary>
		public int DataLength { get; private set; }

		/// <summary>Get the title of the game in English.</summary>
		public string TitleEnglish { get; private set; }

		/// <summary>Get the title of the game in French.</summary>
		public string TitleFrench { get; private set; }

		/// <summary>Get the title of the game in German.</summary>
		public string TitleGerman { get; private set; }

		/// <summary>Get the title of the game in Italian.</summary>
		public string TitleItalian { get; private set; }

		/// <summary>Get the title of the game in Spanish.</summary>
		public string TitleSpanish { get; private set; }

		/// <summary>Get an icon for the ROM.</summary>
		public IndexedTextureAsset Icon { get; private set; }

		/// <summary>Get the flat collection of the files in the ROM.</summary>
		public ReadOnlyCodex<RomFile> Files { get; private set; }

		/// <summary>Get the flat collection of folders in the ROM.</summary>
		public ReadOnlyCodex<RomFolder> Folders { get; private set; }

		internal Rom(AssetLoader loader)
			: base(loader) {
			BinaryReader reader = Reader = loader.Reader;

			Codex<RomFile> files = new Codex<RomFile>();
			Codex<RomFolder> folders = new Codex<RomFolder>();
			Files = files;
			Folders = folders;

			GameTitle = Reader.ReadStringz(12, Encoding.ASCII);
			GameCode = Reader.ReadStringz(4, Encoding.ASCII);

			// Offset 0x10
			MakerCode = Reader.ReadStringz(2, Encoding.ASCII);
			UnitCode = reader.ReadByte();
			DeviceCode = reader.ReadByte();
			CardSizeBase = reader.ReadByte();
			loader.ExpectZeroes(1, 7);
			Unknowns.ReadInt32s(reader, 1); // Usually 0, Alice in Wonderland: 3

			// Offset 0x20
			PageSize = reader.ReadInt32(); // Usually (always?) 0x4000
			Unknowns.ReadInt16s(reader, 2); // The Dark Spire: 0x800; Trauma Center: 0xF780 (-2176)
			loader.Expect(0x2000000);
			Unknowns.ReadInt32s(reader, 1); // The Dark Spire: 0x9BEF8; Trauma Center: 0x1DC518

			// Offset 0x30
			Unknowns.ReadInt32s(reader, 1); // Entry points? The Dark Spire: 0x152200; Trauma Center: 0x1E0600
			loader.Expect(0x2380000);
			loader.Expect(0x2380000);
			Unknowns.ReadInt16s(reader, 1); // The Dark Spire: 0x6F28; Trauma Center: 0x6F24
			loader.Expect((short)0x2);

			// Offset 0x40
			int fileNameTableOffset = reader.ReadInt32();
			int fileNameTableSize = reader.ReadInt32();
			int fileSizeTableOffset = reader.ReadInt32();
			int fileSizeTableSize = reader.ReadInt32();

			// Offset 0x50
			Unknowns.ReadInt32s(reader, 2, "Offset and size"); // Used in The Dark Spire, zeroes in Trauma Center.
			loader.ExpectZeroes(4, 2);

			// Offset 0x60
			Unknowns.ReadInt32s(reader, 2);
			int iconAndTitleOffset = reader.ReadInt32();
			Unknowns.ReadInt32s(reader, 1);

			// Offset 0x70
			Unknowns.ReadInt32s(reader, 2);
			loader.ExpectZeroes(4, 2);

			// Offset 0x80
			DataLength = reader.ReadInt32();
			loader.Expect(PageSize);
			Unknowns.ReadInt32s(reader, 2); // The Dark Spire: 0x4B68; 0 in Trauma Center.

			ReadIconAndTitle(iconAndTitleOffset);

			if (fileNameTableOffset > 0) {
				int folderNamesOffset = reader.ReadInt32At(fileNameTableOffset);
				reader.BaseStream.Position = fileNameTableOffset;
				int folderCount = folderNamesOffset / 8;
				for (int index = 0; index < folderCount; index++)
					folders.Add(new RomFolder(this, index, reader));
				byte[] folderNames = reader.ReadBytes(fileNameTableSize - folderNamesOffset);

				reader.BaseStream.Position = fileSizeTableOffset;
				int fileCount = fileSizeTableSize / 8;
				for (int index = 0; index < fileCount; index++)
					files.Add(new RomFile(this, index, reader));

				folders[0].Sort(this, "Root", folderNamesOffset, folderNames);

				FolderAsset orphanedFolders = null, orphanedFiles = null;

				foreach (RomFolder folder in folders) {
					if (folder.Used)
						continue;
					if (orphanedFolders == null)
						orphanedFolders = new FolderAsset(this, "Orphaned folder[s]");
					folder.Sort(this, folder.Name, folderNamesOffset, folderNames);
				}

				foreach (RomFile file in files) {
					if (file.Used)
						continue;
					if (orphanedFiles == null)
						orphanedFiles = new FolderAsset(this, "Orphaned file[s]");
					file.Sort(orphanedFiles, file.Name);
				}
			}
		}

		void ReadIconAndTitle(int iconAndTitleOffset) {
			if (iconAndTitleOffset == 0)
				return;

			// Read the palette.
			Reader.BaseStream.Position = iconAndTitleOffset + 0x220;
			Color[] colors = new Color[16];
			for (int index = 0; index < 16; index++)
				Reader.ReadNDSColor(forceTransparent: index == 0);
			PaletteAsset palette = new PaletteAsset(Manager, "Icon palette", colors);

			// Read the icon.
			Reader.BaseStream.Position = iconAndTitleOffset + 0x20;
			Vector2i iconSize = new Vector2i(32, 32);
			Vector2i tileSize = iconSize / 8;
			int[] indices = new int[iconSize.X * iconSize.Y];

			for (Vector2i tile = Vector2i.Zero; tile.Y < tileSize.Y; tile.Y++) {
				for (tile.X = 0; tile.X < tileSize.X; tile.X++) {
					for (Vector2i pixel = Vector2i.Zero; pixel.Y < 8; pixel.Y++) {
						for (pixel.X = 0; pixel.X < 4; pixel.X++) {
							int x = pixel.X + tile.X * 4;
							int y = pixel.Y + tile.Y * 8;
							int offset = x + y * iconSize.X;
							byte pair = Reader.ReadByte();

							indices[offset + 0] = pair & 0x0F;
							indices[offset + 1] = pair >> 4;
						}
					}
				}
			}

			Icon = new IndexedTextureAsset(Manager, "Icon", palette, iconSize.X, iconSize.Y, indices);

			// Read the title.
			Name = Reader.ReadStringzAt(iconAndTitleOffset + 0x240, Encoding.Unicode);
			TitleEnglish = Reader.ReadStringzAt(iconAndTitleOffset + 0x340, Encoding.Unicode);
			TitleFrench = Reader.ReadStringzAt(iconAndTitleOffset + 0x440, Encoding.Unicode);
			TitleGerman = Reader.ReadStringzAt(iconAndTitleOffset + 0x540, Encoding.Unicode);
			TitleItalian = Reader.ReadStringzAt(iconAndTitleOffset + 0x640, Encoding.Unicode);
			TitleSpanish = Reader.ReadStringzAt(iconAndTitleOffset + 0x740, Encoding.Unicode);
		}
	}

	/// <summary>Format for a Nintendo DS ROM.</summary>
	public class RomFormat : AssetFormat {
		internal RomFormat(Platform platform)
			: base(platform, typeof(Rom), canLoad: true, extension: ".nds") {
		}

		/// <summary>
		/// Attempt to match the source as an NDS ROM.
		/// </summary>
		/// <param name="loader"></param>
		/// <returns></returns>
		public override LoadMatchStrength LoadMatch(AssetLoader loader) {
			BinaryReader reader = loader.Reader;

			if (loader.Length < 0x4000)
				return LoadMatchStrength.None;

			// Short game name
			for (int i = 0; i < 12; i++)
				if (!IsAsciiOrNul(reader.ReadByte()))
					return LoadMatchStrength.None;

			// Game Id and Maker Id
			for (int i = 0; i < 6; i++)
				if (!IsAscii(reader.ReadByte()))
					return LoadMatchStrength.None;

			reader.BaseStream.Seek(14, SeekOrigin.Current);

			// Offset 0x20
			if (reader.ReadInt32() != 0x4000) // Page size - always 0x4000?
				return LoadMatchStrength.None;
			reader.BaseStream.Seek(12, SeekOrigin.Current);

			// Offset 0x30
			if (reader.ReadInt32() > loader.Length || reader.ReadInt32() != 0x2380000 || reader.ReadInt32() != 0x2380000)
				return LoadMatchStrength.None;

			return LoadMatchStrength.Medium;
		}

		static bool IsAscii(byte value) { return value >= 0x20 && value <= 0x7E; }
		static bool IsAsciiOrNul(byte value) { return value == 0 || IsAscii(value); }

		/// <summary>
		/// Load the ROM.
		/// </summary>
		/// <param name="loader"></param>
		/// <returns></returns>
		public override Asset Load(AssetLoader loader) {
			return new Rom(loader);
		}
	}
}
