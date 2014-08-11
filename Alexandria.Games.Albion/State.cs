using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Games.Albion {
	/// <summary>Static game state.</summary>
	public class State {
	}

	/// <summary>This contains constants for use throughout the Albion library.</summary>
	public static class Constants {
		/// <summary>Subdirectory for the libraries.</summary>
		public const string Library = "xldlibs/";

		/// <summary>Subdirectory for the German text.</summary>
		public const string LanguageGerman = "german/";

		/// <summary>Subdirectory for the English text.</summary>
		public const string LanguageEnglish = "english/";

		/// <summary>Subdirectory for the French text.</summary>
		public const string LanguageFrench = "french/";

		/// <summary>Relative path to the initial game state.</summary>
		public const string Initial = "initial/";

		/// <summary>Relative path to the current game state.</summary>
		public const string Current = "current/";

		/// <summary>3D backgrounds library.</summary>
		public const string Backgrounds3dLibrary = "3dbckgr0.xld";

		/// <summary>3D floors library ({0} is 0-2).</summary>
		public const string Floors3dLibrary = "3dfloor{0}.xld";

		/// <summary>3D objects libraries ({0} is 0-3).</summary>
		public const string Objects3dLibrary = "3dobjec{0}.xld";

		/// <summary>3D overlays library ({0} is 0-2).</summary>
		public const string Overlays3dLibrary = "3doverl{0}.xld";

		/// <summary>3D walls library ({0} is 0-1).</summary>
		public const string Walls3dLibrary = "3dwalls{0}.xld";

		/// <summary>Automap graphics library, with a scroll map background in the first record and a technological map background in the second record. Each is a set of 16x16 tiles. </summary>
		public const string AutomapGraphicsLibrary = "autogfx0.xld";

		/// <summary>Perhaps black list. Seems related to the tilesets.</summary>
		public const string BlockListLibrary = "blklist0.xld";

		/// <summary>Combat background images.</summary>
		public const string CombatBackgroundLibrary = "comback0.xld";

		/// <summary>Combat graphics.</summary>
		public const string CombatGraphicsLibrary = "comgfx0.xld";

		/// <summary>({0} is 0-3 or 9).</summary>
		public const string EventSetLibrary = "evntset{0}.xld";

		/// <summary>Body pictures.</summary>
		public const string FullBodyPictureLibrary = "fbodpix0.xld";

		/// <summary>Fonts.</summary>
		public const string FontsLibrary = "fonts0.xld";

		/// <summary>Mappings and information for each tileset.</summary>
		public const string IconDataLibrary = "icondat0.xld";

		/// <summary>Tilesets.</summary>
		public const string IconGraphicsLibrary = "icongfx0.xld";

		/// <summary>Item graphics.</summary>
		public const string ItemGraphicsLibrary = "itemgfx";

		/// <summary>Item list.</summary>
		public const string ItemListLibrary = "itemlist.dat";

		/// <summary>Item names.</summary>
		public const string ItemNamesData = "itemname.dat";

		/// <summary>({0} is 0-2).</summary>
		public const string LabDataLibrary = "labdata{0}.xld";

		/// <summary>({0} is 1-3).</summary>
		public const string MapDataLibrary = "mapdata{0}.xld";

		/// <summary>Monster characters.</summary>
		public const string MonsterCharacterLibrary = "monchar0.xld";

		/// <summary>Monster graphics.</summary>
		public const string MonsterGraphicsLibrary = "mongfx0.xld";

		/// <summary>({0} is 0-2).</summary>
		public const string MonsterGroupLibrary = "mongrp{0}.xld";

		/// <summary>({0} is 0-1).</summary>
		public const string NPCGraphicsLibrary = "npcgr{0}.xld";

		/// <summary>Tiny images for some NPCs.</summary>
		public const string NPCTinyGraphicsLibrary = "npckl0.xld";

		/// <summary>Static portion of the palette; {byte r, g, b}[64], where each value is between 0 and 255.</summary>
		public const string PaletteStatic = "palette.000";

		/// <summary>Palette library. Each is {ubyte r, g, b}[192], where each value is between 0 and 255.</summary>
		public const string PaletteLibrary = "palette0.xld";

		/// <summary>Party graphics.</summary>
		public const string PartyGraphicsLibrary = "partgr0.xld";

		/// <summary>Tiny party graphics.</summary>
		public const string PartyTinyGraphicsLibrary = "partkl0.xld";

		/// <summary>A library of LBM files (45 in total) containing their own colour maps.</summary>
		/// <remarks>
		/// 0 - A gray-haired man with a beard wearing a corsair outfit reacts as if he's been shot.
		/// 1 - A view of a village on the side of a lake, pink sky, dark clouds above.
		/// 2 - The protagonist flying through space in the shuttle.
		/// 3 - A background of stars with "Bild des Planeten" written in the centre.
		/// 4 - A map of a maze drawn on a scroll.
		/// 5 - A misty view of a waterfall with a cactus in front.
		/// 6 - Three people casting magic on a giant seed pod.
		/// 7 - A view of triangular-roofed huts on a river.
		/// 8 - Two men struggling with a woman in green bearing a knife.
		/// 9 - Vikings facing down mutants with tails on a green background.
		/// 10 - An open, empty chest.
		/// 11 - The open chest containing booty.
		/// 12 - The closed chest.
		/// 13 - Booty on the ground.
		/// 14 - A spaceship with vines growing over it.
		/// 15 - Same scene with silhouetted people in front.
		/// 16 - A test image in the crashed Toronto, apparently showing an overlay.
		/// 17 - A closed arched door.
		/// 18 - Zero.
		/// 19 - Small "ALBION" top left.
		/// 20 - Small "ALBION" bottom left.
		/// 21 - Big "ALBION" top left.
		/// 22 - Small "ALBION" top right.
		/// 23 - Small "ALBION" bottom right.
		/// 24 - Big "ALBION" top right.
		/// 25 - Big "ALBION" middle top.
		/// 26 - Big "ALBION" bottom right.
		/// 27 - Parchment map showing rock walls broken by coloured circles.
		/// 28 - Parchment map showing X beside a waterfall.
		/// 29 - Jesus pocking a red-headed furry's nose.
		/// 30 - Camping in front of a fire.
		/// 31 - Test image exploring a small village with a bridge.
		/// 32 - Test image in a 3D dungeon facing down two jerks behind a red partition.
		/// 33 - Test image of a conga line in a dude's house with a big-ass fire.
		/// 34 - Test image of a conga line in a warrior's house.
		/// 35 - Test image of a conga line in a temple.
		/// 36 - Test image of a tree in the jungle with water behind.
		/// 37 - Test image in a 3D town looking at some boats.
		/// 38 - Test image of a long-range conga lin through desert with spirey rocks.
		/// 39 - Test image in the Toronto with two people.
		/// 40 - Test image in the 3D Toronto with two people.
		/// 41 - Test image of a conga line leading up to a cross/sword on a green curtain.
		/// 42 - Test image of a 3D dungeon facing floating hands with two glowing eyes in the palm.
		/// 43 - Test image of a 3D dungeon killing zombies with lightningfire.
		/// 44 - Test image of a 3D savannah killing kangaroos with waterlightning.
		/// </remarks>
		public const string PictureLibrary = "picture0.xld"; 

		/// <summary>({0} is 0-2).</summary>
		public const string SamplesLibrary = "samples{0}.xld";

		/// <summary>({0} is 0-2).</summary>
		public const string ScriptLibrary = "script{0}.xld";
		public const string SlabFile = "slab";

		/// <summary>({0} is 0-1).</summary>
		public const string SmallPortraitsLibrary = "smlport{0}.xld";

		public const string SongsLibrary = "songs0.xld";
		public const string SpellDataFile = "spelldat.dat";

		/// <summary>Party and enemy images for the combat screen.</summary>
		public const string TacticalObjectLibrary = "tactico0.xld";

		public const string TransparencyTableLibrary = "transtb0.xld";
		public const string WaveLibraryLibrary = "wavelib0.xld";

		/// <summary>Event text within a language path ({0} is 0-3 or 9).</summary>
		public const string LanguageEventText = "evnttxt{0}.xld";

		/// <summary>Animations within a language path.</summary>
		public const string LanguageFlics = "flics0.xld";

		/// <summary>Map text within a language path ({0} is 1-3).</summary>
		public const string LanguageMapText = "maptext{0}.xld";

		/// <summary>System text within a language path. Line-oriented. Any line not starting with '[' is ignored; otherwise the form is "[{0:D4}:{1}]", where {1} is the text assigned to this index.</summary>
		public const string LanguageSystemText = "systexts";

		/// <summary>Word list within a language path.</summary>
		public const string LanguageWordList = "wordlis0.xld";

		/// <summary>({0} is 1-3).</summary>
		public const string InitialAutomap = "automap{0}.xld";

		/// <summary>({0} is 0-2 or 5).</summary>
		public const string InitialChestData = "chestdt{0}.xld";

		/// <summary>({0} is 0-2).</summary>
		public const string InitialMerchantData = "merchdt{0}.xld";

		/// <summary>({0} is 0-2).</summary>
		public const string StateNPCCharacterLibrary = "npcchar{0}.xld";

		/// <summary>({0} is 0-2).</summary>
		public const string StatePartyCharacterLibrary = "prtchar{0}.xld";

		// Each is 0x03AC long
		// 0Eh - ushort - Protection level
		// 16h - ushort - Training points
		// 1Ah - ushort - Food
		// 2Ah/2Ch - ushort - Strength/capacity
		// 32h/34h - ushort - Intelligence/capacity
		// 3Ah/3Ch - ushort - Dexterity/capacity
		// 42h/44h - ushort - Speed/capacity
		// 4Ah/4Ch - ushort - Stamina/capacity
		// 52h/54h - ushort - Luck/capacity
		// 5Ah/5Ch - ushort - Magic resistance/capacity
		// 62h/64h - ushort - Magic talent/capacity
		// 6Ah - ushort - Age in years.
		// 6Ch - ushort - Age capacity?
		// 7Ah/7Ch - ushort - Close range combat/capacity
		// 82h/84h - ushort - Long range combat/capacity
		// 8Ah/8Ch - ushort - Critical hit/capacity
		// 92h/94h - ushort - Lockpicking/capacity
		// CAh/CCh - ushort - Life points/capacity
		// EAh - ushort - Level?
		// EEh - uint - Experience points
		// 112h+? - Name

	}
}
