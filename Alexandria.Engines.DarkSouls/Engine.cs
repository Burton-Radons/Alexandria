using Glare.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.DarkSouls {
	public class Engine : Alexandria.Engine {
		public const string LanguageIdEnglish = "ENGLISH";
		public const string LanguageIdFrench = "FRENCH";
		public const string LanguageIdGerman = "GERMAN";
		public const string LanguageIdItalian = "ITALIAN";
		public const string LanguageIdJapanese = "JAPANESE";
		public const string LanguageIdKorean = "KOREAN";
		public const string LanguageIdPolish = "POLISH";
		public const string LanguageIdRussian = "RUSSIAN";
		public const string LanguageIdSpanish = "SPANISH";
		public const string LanguageIdChinese = "TCHINESE";

		public static string GetLanguageId(Language language) {
			switch (language) {
				case Language.English: return LanguageIdEnglish;
				case Language.French: return LanguageIdFrench;
				case Language.German: return LanguageIdGerman;
				case Language.Italian: return LanguageIdItalian;
				case Language.Japanese: return LanguageIdJapanese;
				case Language.Korean: return LanguageIdKorean;
				case Language.Polish: return LanguageIdPolish;
				case Language.Russian: return LanguageIdRussian;
				case Language.Spanish: return LanguageIdSpanish;
				case Language.Chinese: return LanguageIdChinese;
				default: throw new ArgumentException("Invalid language id " + language + ".", "language");
			}
		}

		/// <summary>Path to an archive containing item information ({0} is a LanguageId)</summary>
		public const string LanguageItemArchivePath = "msg/{0}/item.msgbnd.dcx";

		/// <summary>Base path for any content in an item archive (from <see cref="LanguageItemArchivePath"/>) where {0} is a LanguageId.</summary>
		public const string ItemArchive_ContentPath = "N:/FRPG/data/Msg/Data_{0}/win32/";

		public enum ItemArchiveId {
			/// <summary>No/invalid value.</summary>
			None = -1,

			/// <summary>Usable item names, i.e. 100 = "White Sign Soapstone". File name is "アイテム名.fmg".</summary>
			ItemNames = 0x0A,

			/// <summary>Weapon names with enchantments, i.e. 100000 = "Dagger", 100001 = "Dagger+1". File name is "武器名.fmg".</summary>
			WeaponNames = 0x0B,

			/// <summary>Armor and hairstyle names with enchantments, i.e. 1000 = "Saved", 10000 = "Catarina Helm", 40001 = "Dark Mask +1". File name is "防具名.fmg".</summary>
			ArmorNames = 0x0C,

			/// <summary>Ring names, i.e. 100 = "Havel's Ring". File name is "アクセサリ名.fmg".</summary>
			RingNames = 0x0D,

			/// <summary>Sorcery names; i.e. 3000 = "Soul Arrow". File name is "魔法名.fmg".</summary>
			SorceryNames = 0x0E,

			/// <summary>NPC names, i.e. 600 = "Knight Solaire". File name is "NPC名.fmg".</summary>
			NpcNames = 0x12,

			/// <summary>Area names, i.e. 1000 = "Depths", 1400 = "Blighttown". File name is "地名.fmg".</summary>
			AreaNames = 0x13,

			/// <summary>Usable item actions, i.e. 100 = "Leave summon sign". File name is "アイテム説明.fmg".</summary>
			ItemEffects = 0x14,

			/// <summary>Weapon types (no enchantments); i.e. 100000 = "Dagger", 406200 = "Curved Sword". File name is "武器説明.fmg".</summary>
			WeaponTypes = 0x15,

			/// <summary>Ring effects, i.e. 100 = "Boosts maximum equipment load". File name is "アクセサリ説明.fmg".</summary>
			RingEffects = 0x17,

			/// <summary>Usable item descriptions, i.e. 100 = "Online play item. Leave summon sign. Be summoned to another world...". File name is "アイテムうんちく.fmg".</summary>
			ItemDescriptions = 0x18,

			/// <summary>Weapon descriptions, i.e. 100000 = "Weapon type: Dagger, Attack type: Slash/Thrust, This standard small dagger has only a modest attack...". File name is "武器うんちく.fmg".</summary>
			WeaponDescriptions = 0x19,

			/// <summary>Armor descriptions, i.e. 10000 = "Distinctively shaped helm worn by the Knights of Catarina..." File name is "防具うんちく.fmg".</summary>
			ArmorDescriptions = 0x1A,

			/// <summary>Ring descriptions, i.e. 100 = "This ring was named after Havel the Rock...". File name is "アクセサリうんちく.fmg".</summary>
			RingDescriptions = 0x1B,

			/// <summary>Sorcery actions; i.e. 3000 = "Fire soul arrow". File name is "魔法説明.fmg".</summary>
			SorceryActions = 0x1C,

			/// <summary>Sorcery descriptions; i.e. 3000 = "Elemental sorcery. Fire soul arrow...". File name is "魔法うんちく.fmg".</summary>
			SorceryDescriptions = 0x1D,
		}

		public static Archive GetItemLanguageArchive(Archive baseArchive, Language language = Language.English) {
			string path = string.Format(LanguageItemArchivePath, GetLanguageId(language));
			ArchiveRecord record = baseArchive.FindRecordByPath(path);
			if (record == null)
				return null;
			Asset contents = record.Contents;
			return contents as Archive;
		}

		public static StringArchive GetStringArchive(Archive baseArchive, ItemArchiveId id, Language language = Language.English) {
			Archive itemArchive = GetItemLanguageArchive(baseArchive, language);
			if (itemArchive == null)
				return null;
			ArchiveRecord record = itemArchive.FindRecordById((int)id);
			if (record == null)
				return null;
			Asset contents = record.Contents;
			return contents as StringArchive;
		}
		

		public Engine(Plugin plugin)
			: base(plugin) {
			AddFormat(new ArchiveFormat(this));
			AddFormat(new DSModelFormat(this));
			AddFormat(new FsslFormat(this));
			AddFormat(new MCGFormat(this));
			AddFormat(new MCPFormat(this));
			AddFormat(new ParameterDefinitionFormat(this));
			AddFormat(new ParameterTableFormat(this));
			AddFormat(new StringArchiveFormat(this));
			AddFormat(new TableArchiveFormat(this));
			//AddFormat(new TextureFormat(this));
			AddFormat(new TextureArchiveFormat(this));
		}
	}
}
