using Glare.Assets;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.DarkSouls {
	/// <summary>
	/// The engine for Dark Souls.
	/// </summary>
	public class Engine : Alexandria.Engine {
		/// <summary>The language id for <see cref="Language.English"/>, which is "ENGLISH".</summary>
		public const string LanguageIdEnglish = "ENGLISH";

		/// <summary>The language id for <see cref="Language.French"/>, which is "FRENCH".</summary>
		public const string LanguageIdFrench = "FRENCH";

		/// <summary>The language id for <see cref="Language.German"/>, which is "GERMAN".</summary>
		public const string LanguageIdGerman = "GERMAN";

		/// <summary>The language id for <see cref="Language.Italian"/>, which is "ITALIAN".</summary>
		public const string LanguageIdItalian = "ITALIAN";

		/// <summary>The language id for <see cref="Language.Japanese"/>, which is "JAPANESE".</summary>
		public const string LanguageIdJapanese = "JAPANESE";

		/// <summary>The language id for <see cref="Language.Korean"/>, which is "KOREAN".</summary>
		public const string LanguageIdKorean = "KOREAN";

		/// <summary>The language id for <see cref="Language.Polish"/>, which is "POLISH".</summary>
		public const string LanguageIdPolish = "POLISH";

		/// <summary>The language id for <see cref="Language.Russian"/>, which is "RUSSIAN".</summary>
		public const string LanguageIdRussian = "RUSSIAN";

		/// <summary>The language id for <see cref="Language.Spanish"/>, which is "SPANISH".</summary>
		public const string LanguageIdSpanish = "SPANISH";

		/// <summary>The language id for <see cref="Language.Chinese"/>, which is "TCHINESE".</summary>
		public const string LanguageIdChinese = "TCHINESE";

		static Dictionary<string, string> TranslationDictionary;

		/// <summary>Attempt to translate a Japanese phrase in the data files into English.</summary>
		/// <param name="text"></param>
		/// <param name="defaultValue"></param>
		/// <returns></returns>
		public static string GetTranslation(string text, string defaultValue = null) {
			if (TranslationDictionary == null) {
				TranslationDictionary = new Dictionary<string, string>();

				using(var reader = new StreamReader(new GZipStream(new MemoryStream(Properties.Resources.Translations, false), CompressionMode.Decompress), Encoding.UTF8)) {
					string line;

					reader.ReadLine();
					while ((line = reader.ReadLine()) != null) {
						var split = line.IndexOf('\t');

						if (split < 0)
							continue;
						TranslationDictionary[line.Substring(0, split)] = line.Substring(split + 1);
					}
				}
			}

			return TranslationDictionary.TryGetValue(text, defaultValue);
		}

		/// <summary>Get a language id value.</summary>
		/// <param name="language"></param>
		/// <returns></returns>
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

		/// <summary>Identifiers (actually matching <see cref="ArchiveRecord"/>.<see cref="ArchiveRecord.Id"/> values) for the item string carchives.</summary>
		public enum ItemArchiveId {
			/// <summary>No/invalid value.</summary>
			None = -1,

			/// <summary>Usable item names, i.e. 100 = "White Sign Soapstone". File name is "アイテム名.fmg".</summary>
			GoodsNames = 0x0A,

			/// <summary>Weapon names with enchantments, i.e. 100000 = "Dagger", 100001 = "Dagger+1". File name is "武器名.fmg".</summary>
			WeaponNames = 0x0B,

			/// <summary>Armor and hairstyle names with enchantments, i.e. 1000 = "Saved", 10000 = "Catarina Helm", 40001 = "Dark Mask +1". File name is "防具名.fmg".</summary>
			ProtectorNames = 0x0C,

			/// <summary>Ring names, i.e. 100 = "Havel's Ring". File name is "アクセサリ名.fmg".</summary>
			AccessoryNames = 0x0D,

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
			AccessoryEffects = 0x17,

			/// <summary>Usable item descriptions, i.e. 100 = "Online play item. Leave summon sign. Be summoned to another world...". File name is "アイテムうんちく.fmg".</summary>
			GoodsDescriptions = 0x18,

			/// <summary>Weapon descriptions, i.e. 100000 = "Weapon type: Dagger, Attack type: Slash/Thrust, This standard small dagger has only a modest attack...". File name is "武器うんちく.fmg".</summary>
			WeaponDescriptions = 0x19,

			/// <summary>Armor descriptions, i.e. 10000 = "Distinctively shaped helm worn by the Knights of Catarina..." File name is "防具うんちく.fmg".</summary>
			ProtectorDescriptions = 0x1A,

			/// <summary>Ring descriptions, i.e. 100 = "This ring was named after Havel the Rock...". File name is "アクセサリうんちく.fmg".</summary>
			AccessoryDescriptions = 0x1B,

			/// <summary>Sorcery actions; i.e. 3000 = "Fire soul arrow". File name is "魔法説明.fmg".</summary>
			SorceryActions = 0x1C,

			/// <summary>Sorcery descriptions; i.e. 3000 = "Elemental sorcery. Fire soul arrow...". File name is "魔法うんちく.fmg".</summary>
			SorceryDescriptions = 0x1D,
		}

		/// <summary>Get the item language archive, which contains the <see cref="ItemArchiveId"/> <see cref="StringArchive"/>s.</summary>
		/// <param name="baseArchive"></param>
		/// <param name="language"></param>
		/// <returns></returns>
		public static Archive GetItemLanguageArchive(Archive baseArchive, Language language = Language.English) {
			string path = string.Format(LanguageItemArchivePath, GetLanguageId(language));
			ArchiveRecord record = baseArchive.FindRecordByPath(path);
			if (record == null)
				return null;
			Asset contents = record.Contents;
			return contents as Archive;
		}

		/// <summary>Get the string archive from the <see cref="ItemArchiveId"/>.</summary>
		/// <param name="baseArchive"></param>
		/// <param name="id"></param>
		/// <param name="language"></param>
		/// <returns></returns>
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
		
		/// <summary>Initialise the engine by adding all the formats.</summary>
		/// <param name="plugin"></param>
		public Engine(Plugin plugin)
			: base(plugin) {
			AddFormat(new ArchiveFormat(this));
			AddFormat(new ModelFormat(this));
			AddFormat(new EffectFormat(this));
			AddFormat(new EmeldFormat(this));
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
