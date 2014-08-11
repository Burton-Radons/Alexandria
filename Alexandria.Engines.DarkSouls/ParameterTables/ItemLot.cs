using Glare.Assets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Alexandria.Engines.DarkSouls {
	partial class TableRows {
		/// <summary>
		/// Item lots to determine rewards for chests, killing NPCs and monsters, etc.
		/// </summary>
		/// <remarks>
		/// Called "ITEMLOT_PARAM_ST" in Dark Souls; from "ItemLotParam.paramdef" (id 23h).
		/// </remarks>
		public class ItemLot : ParameterTableRow {
			/// <summary>The name of the table in the file.</summary>
			public const string TableName = "ITEMLOT_PARAM_ST";

			Int32 lotItemId01, lotItemId02, lotItemId03, lotItemId04, lotItemId05, lotItemId06, lotItemId07, lotItemId08, getItemFlagId01, getItemFlagId02, getItemFlagId03, getItemFlagId04, getItemFlagId05, getItemFlagId06, getItemFlagId07, getItemFlagId08, getItemFlagId, cumulateNumFlagId;
			RewardItemCategory lotItemCategory01, lotItemCategory02, lotItemCategory03, lotItemCategory04, lotItemCategory05, lotItemCategory06, lotItemCategory07, lotItemCategory08;
			UInt16 lotItemBasePoint01, lotItemBasePoint02, lotItemBasePoint03, lotItemBasePoint04, lotItemBasePoint05, lotItemBasePoint06, lotItemBasePoint07, lotItemBasePoint08, cumulateLotPoint01, cumulateLotPoint02, cumulateLotPoint03, cumulateLotPoint04, cumulateLotPoint05, cumulateLotPoint06, cumulateLotPoint07, cumulateLotPoint08;
			Byte cumulateNumMax, rarity, lotItemNum01, lotItemNum02, lotItemNum03, lotItemNum04, lotItemNum05, lotItemNum06, lotItemNum07, lotItemNum08;

			/// <summary>A property of the class.</summary>
			public static readonly PropertyInfo
				LotItemId01Property = GetProperty<ItemLot>("LotItemId01"),
				LotItemId02Property = GetProperty<ItemLot>("LotItemId02"),
				LotItemId03Property = GetProperty<ItemLot>("LotItemId03"),
				LotItemId04Property = GetProperty<ItemLot>("LotItemId04"),
				LotItemId05Property = GetProperty<ItemLot>("LotItemId05"),
				LotItemId06Property = GetProperty<ItemLot>("LotItemId06"),
				LotItemId07Property = GetProperty<ItemLot>("LotItemId07"),
				LotItemId08Property = GetProperty<ItemLot>("LotItemId08"),
				LotItemCategory01Property = GetProperty<ItemLot>("LotItemCategory01"),
				LotItemCategory02Property = GetProperty<ItemLot>("LotItemCategory02"),
				LotItemCategory03Property = GetProperty<ItemLot>("LotItemCategory03"),
				LotItemCategory04Property = GetProperty<ItemLot>("LotItemCategory04"),
				LotItemCategory05Property = GetProperty<ItemLot>("LotItemCategory05"),
				LotItemCategory06Property = GetProperty<ItemLot>("LotItemCategory06"),
				LotItemCategory07Property = GetProperty<ItemLot>("LotItemCategory07"),
				LotItemCategory08Property = GetProperty<ItemLot>("LotItemCategory08"),
				LotItemBasePoint01Property = GetProperty<ItemLot>("LotItemBasePoint01"),
				LotItemBasePoint02Property = GetProperty<ItemLot>("LotItemBasePoint02"),
				LotItemBasePoint03Property = GetProperty<ItemLot>("LotItemBasePoint03"),
				LotItemBasePoint04Property = GetProperty<ItemLot>("LotItemBasePoint04"),
				LotItemBasePoint05Property = GetProperty<ItemLot>("LotItemBasePoint05"),
				LotItemBasePoint06Property = GetProperty<ItemLot>("LotItemBasePoint06"),
				LotItemBasePoint07Property = GetProperty<ItemLot>("LotItemBasePoint07"),
				LotItemBasePoint08Property = GetProperty<ItemLot>("LotItemBasePoint08"),
				CumulateLotPoint01Property = GetProperty<ItemLot>("CumulateLotPoint01"),
				CumulateLotPoint02Property = GetProperty<ItemLot>("CumulateLotPoint02"),
				CumulateLotPoint03Property = GetProperty<ItemLot>("CumulateLotPoint03"),
				CumulateLotPoint04Property = GetProperty<ItemLot>("CumulateLotPoint04"),
				CumulateLotPoint05Property = GetProperty<ItemLot>("CumulateLotPoint05"),
				CumulateLotPoint06Property = GetProperty<ItemLot>("CumulateLotPoint06"),
				CumulateLotPoint07Property = GetProperty<ItemLot>("CumulateLotPoint07"),
				CumulateLotPoint08Property = GetProperty<ItemLot>("CumulateLotPoint08"),
				GetItemFlagId01Property = GetProperty<ItemLot>("GetItemFlagId01"),
				GetItemFlagId02Property = GetProperty<ItemLot>("GetItemFlagId02"),
				GetItemFlagId03Property = GetProperty<ItemLot>("GetItemFlagId03"),
				GetItemFlagId04Property = GetProperty<ItemLot>("GetItemFlagId04"),
				GetItemFlagId05Property = GetProperty<ItemLot>("GetItemFlagId05"),
				GetItemFlagId06Property = GetProperty<ItemLot>("GetItemFlagId06"),
				GetItemFlagId07Property = GetProperty<ItemLot>("GetItemFlagId07"),
				GetItemFlagId08Property = GetProperty<ItemLot>("GetItemFlagId08"),
				GetItemFlagIdProperty = GetProperty<ItemLot>("GetItemFlagId"),
				CumulateNumFlagIdProperty = GetProperty<ItemLot>("CumulateNumFlagId"),
				CumulateNumMaxProperty = GetProperty<ItemLot>("CumulateNumMax"),
				RarityProperty = GetProperty<ItemLot>("Rarity"),
				LotItemNum01Property = GetProperty<ItemLot>("LotItemNum01"),
				LotItemNum02Property = GetProperty<ItemLot>("LotItemNum02"),
				LotItemNum03Property = GetProperty<ItemLot>("LotItemNum03"),
				LotItemNum04Property = GetProperty<ItemLot>("LotItemNum04"),
				LotItemNum05Property = GetProperty<ItemLot>("LotItemNum05"),
				LotItemNum06Property = GetProperty<ItemLot>("LotItemNum06"),
				LotItemNum07Property = GetProperty<ItemLot>("LotItemNum07"),
				LotItemNum08Property = GetProperty<ItemLot>("LotItemNum08"),
				EnableLuck01Property = GetProperty<ItemLot>("EnableLuck01"),
				EnableLuck02Property = GetProperty<ItemLot>("EnableLuck02"),
				EnableLuck03Property = GetProperty<ItemLot>("EnableLuck03"),
				EnableLuck04Property = GetProperty<ItemLot>("EnableLuck04"),
				EnableLuck05Property = GetProperty<ItemLot>("EnableLuck05"),
				EnableLuck06Property = GetProperty<ItemLot>("EnableLuck06"),
				EnableLuck07Property = GetProperty<ItemLot>("EnableLuck07"),
				EnableLuck08Property = GetProperty<ItemLot>("EnableLuck08"),
				CumulateReset01Property = GetProperty<ItemLot>("CumulateReset01"),
				CumulateReset02Property = GetProperty<ItemLot>("CumulateReset02"),
				CumulateReset03Property = GetProperty<ItemLot>("CumulateReset03"),
				CumulateReset04Property = GetProperty<ItemLot>("CumulateReset04"),
				CumulateReset05Property = GetProperty<ItemLot>("CumulateReset05"),
				CumulateReset06Property = GetProperty<ItemLot>("CumulateReset06"),
				CumulateReset07Property = GetProperty<ItemLot>("CumulateReset07"),
				CumulateReset08Property = GetProperty<ItemLot>("CumulateReset08");

			Engine.ItemArchiveId GetNameArchiveId(RewardItemCategory category) {
				switch (category) {
					case RewardItemCategory.Protector: return Engine.ItemArchiveId.ProtectorNames;
					case RewardItemCategory.Goods: return Engine.ItemArchiveId.GoodsNames;
					case RewardItemCategory.Accessory: return Engine.ItemArchiveId.AccessoryNames;
					case RewardItemCategory.Weapon: return Engine.ItemArchiveId.WeaponNames;
					case RewardItemCategory.None: return Engine.ItemArchiveId.None;
					default: throw new ArgumentOutOfRangeException("category");
				}
			}

			string GetLotItemName(RewardItemCategory category, int id, Language language = Language.English, string defaultValue = "") {
				return Parent.GetLocalisedString(GetNameArchiveId(category), id, language, defaultValue);
			}

			string GetLotItemName01(Language language = Language.English, string defaultValue = "") { return GetLotItemName(LotItemCategory01, LotItemId01, language, defaultValue); }
			string GetLotItemName02(Language language = Language.English, string defaultValue = "") { return GetLotItemName(LotItemCategory02, LotItemId02, language, defaultValue); }
			string GetLotItemName03(Language language = Language.English, string defaultValue = "") { return GetLotItemName(LotItemCategory03, LotItemId03, language, defaultValue); }
			string GetLotItemName04(Language language = Language.English, string defaultValue = "") { return GetLotItemName(LotItemCategory04, LotItemId04, language, defaultValue); }
			string GetLotItemName05(Language language = Language.English, string defaultValue = "") { return GetLotItemName(LotItemCategory05, LotItemId05, language, defaultValue); }
			string GetLotItemName06(Language language = Language.English, string defaultValue = "") { return GetLotItemName(LotItemCategory06, LotItemId06, language, defaultValue); }
			string GetLotItemName07(Language language = Language.English, string defaultValue = "") { return GetLotItemName(LotItemCategory07, LotItemId07, language, defaultValue); }
			string GetLotItemName08(Language language = Language.English, string defaultValue = "") { return GetLotItemName(LotItemCategory08, LotItemId08, language, defaultValue); }

			/// <summary>Get the localised English name of the first item, or "" if there is no item for this entry.</summary>
			[ParameterTableRow("@lotItemEnglishName01", index: -1, minimum: 0, maximum: 0, step: 0, sortOrder: 401)]
			public string LotItemEnglishName01 { get { return GetLotItemName01(); } }

			/// <summary>Get the localised English name of the second item, or "" if there is no item for this entry.</summary>
			[ParameterTableRow("@lotItemEnglishName02", index: -1, minimum: 0, maximum: 0, step: 0, sortOrder: 1201)]
			public string LotItemEnglishName02 { get { return GetLotItemName02(); } }

			/// <summary>Get the localised English name of the third item, or "" if there is no item for this entry.</summary>
			[ParameterTableRow("@lotItemEnglishName03", index: -1, minimum: 0, maximum: 0, step: 0, sortOrder: 2001)]
			public string LotItemEnglishName03 { get { return GetLotItemName03(); } }

			/// <summary>Get the localised English name of the fourth item, or "" if there is no item for this entry.</summary>
			[ParameterTableRow("@lotItemEnglishName04", index: -1, minimum: 0, maximum: 0, step: 0, sortOrder: 2801)]
			public string LotItemEnglishName04 { get { return GetLotItemName04(); } }

			/// <summary>Get the localised English name of the fifth item, or "" if there is no item for this entry.</summary>
			[ParameterTableRow("@lotItemEnglishName05", index: -1, minimum: 0, maximum: 0, step: 0, sortOrder: 3601)]
			public string LotItemEnglishName05 { get { return GetLotItemName05(); } }

			/// <summary>Get the localised English name of the sixth item, or "" if there is no item for this entry.</summary>
			[ParameterTableRow("@lotItemEnglishName06", index: -1, minimum: 0, maximum: 0, step: 0, sortOrder: 4401)]
			public string LotItemEnglishName06 { get { return GetLotItemName06(); } }

			/// <summary>Get the localised English name of the seventh item, or "" if there is no item for this entry.</summary>
			[ParameterTableRow("@lotItemEnglishName07", index: -1, minimum: 0, maximum: 0, step: 0, sortOrder: 5201)]
			public string LotItemEnglishName07 { get { return GetLotItemName07(); } }

			/// <summary>Get the localised English name of the eighth item, or "" if there is no item for this entry.</summary>
			[ParameterTableRow("@lotItemEnglishName08", index: -1, minimum: 0, maximum: 0, step: 0, sortOrder: 6001)]
			public string LotItemEnglishName08 { get { return GetLotItemName08(); } }

			/// <summary>1 : item ID</summary>
			/// <remarks>
			/// Japanese short name: "１：アイテムID", Google translated: "1 : item ID".
			/// Japanese description: "取得できるアイテムのID", Google translated: "ID of the item that can be retrieved".
			/// </remarks>
			[ParameterTableRowAttribute("lotItemId01", index: 0, minimum: -1, maximum: 1E+09, step: 1, sortOrder: 400, unknown2: 1)]
			[DisplayName("1 : item ID")]
			[Description("ID of the item that can be retrieved")]
			[DefaultValue((Int32)0)]
			public Int32 LotItemId01 {
				get { return lotItemId01; }
				set {
					if ((double)value < -1 || (double)value > 1E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+09 for LotItemId01.");
					SetProperty(ref lotItemId01, ref value, LotItemId01Property);
				}
			}

			/// <summary>2 : item ID</summary>
			/// <remarks>
			/// Japanese short name: "２：アイテムID", Google translated: "2 : item ID".
			/// Japanese description: "取得できるアイテムのID", Google translated: "ID of the item that can be retrieved".
			/// </remarks>
			[ParameterTableRowAttribute("lotItemId02", index: 1, minimum: -1, maximum: 1E+09, step: 1, sortOrder: 1200, unknown2: 1)]
			[DisplayName("2 : item ID")]
			[Description("ID of the item that can be retrieved")]
			[DefaultValue((Int32)0)]
			public Int32 LotItemId02 {
				get { return lotItemId02; }
				set {
					if ((double)value < -1 || (double)value > 1E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+09 for LotItemId02.");
					SetProperty(ref lotItemId02, ref value, LotItemId02Property);
				}
			}

			/// <summary>3 : Item ID</summary>
			/// <remarks>
			/// Japanese short name: "３：アイテムID", Google translated: "3 : Item ID".
			/// Japanese description: "取得できるアイテムのID", Google translated: "ID of the item that can be retrieved".
			/// </remarks>
			[ParameterTableRowAttribute("lotItemId03", index: 2, minimum: -1, maximum: 1E+09, step: 1, sortOrder: 2000, unknown2: 1)]
			[DisplayName("3 : Item ID")]
			[Description("ID of the item that can be retrieved")]
			[DefaultValue((Int32)0)]
			public Int32 LotItemId03 {
				get { return lotItemId03; }
				set {
					if ((double)value < -1 || (double)value > 1E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+09 for LotItemId03.");
					SetProperty(ref lotItemId03, ref value, LotItemId03Property);
				}
			}

			/// <summary>4 : Item ID</summary>
			/// <remarks>
			/// Japanese short name: "４：アイテムID", Google translated: "4 : Item ID".
			/// Japanese description: "取得できるアイテムのID", Google translated: "ID of the item that can be retrieved".
			/// </remarks>
			[ParameterTableRowAttribute("lotItemId04", index: 3, minimum: -1, maximum: 1E+09, step: 1, sortOrder: 2800, unknown2: 1)]
			[DisplayName("4 : Item ID")]
			[Description("ID of the item that can be retrieved")]
			[DefaultValue((Int32)0)]
			public Int32 LotItemId04 {
				get { return lotItemId04; }
				set {
					if ((double)value < -1 || (double)value > 1E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+09 for LotItemId04.");
					SetProperty(ref lotItemId04, ref value, LotItemId04Property);
				}
			}

			/// <summary>5 : Item ID</summary>
			/// <remarks>
			/// Japanese short name: "５：アイテムID", Google translated: "5 : Item ID".
			/// Japanese description: "取得できるアイテムのID", Google translated: "ID of the item that can be retrieved".
			/// </remarks>
			[ParameterTableRowAttribute("lotItemId05", index: 4, minimum: -1, maximum: 1E+09, step: 1, sortOrder: 3600, unknown2: 1)]
			[DisplayName("5 : Item ID")]
			[Description("ID of the item that can be retrieved")]
			[DefaultValue((Int32)0)]
			public Int32 LotItemId05 {
				get { return lotItemId05; }
				set {
					if ((double)value < -1 || (double)value > 1E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+09 for LotItemId05.");
					SetProperty(ref lotItemId05, ref value, LotItemId05Property);
				}
			}

			/// <summary>6 : item ID</summary>
			/// <remarks>
			/// Japanese short name: "６：アイテムID", Google translated: "6 : item ID".
			/// Japanese description: "取得できるアイテムのID", Google translated: "ID of the item that can be retrieved".
			/// </remarks>
			[ParameterTableRowAttribute("lotItemId06", index: 5, minimum: -1, maximum: 1E+09, step: 1, sortOrder: 4400, unknown2: 1)]
			[DisplayName("6 : item ID")]
			[Description("ID of the item that can be retrieved")]
			[DefaultValue((Int32)0)]
			public Int32 LotItemId06 {
				get { return lotItemId06; }
				set {
					if ((double)value < -1 || (double)value > 1E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+09 for LotItemId06.");
					SetProperty(ref lotItemId06, ref value, LotItemId06Property);
				}
			}

			/// <summary>7 : item ID</summary>
			/// <remarks>
			/// Japanese short name: "７：アイテムID", Google translated: "7 : item ID".
			/// Japanese description: "取得できるアイテムのID", Google translated: "ID of the item that can be retrieved".
			/// </remarks>
			[ParameterTableRowAttribute("lotItemId07", index: 6, minimum: -1, maximum: 1E+09, step: 1, sortOrder: 5200, unknown2: 1)]
			[DisplayName("7 : item ID")]
			[Description("ID of the item that can be retrieved")]
			[DefaultValue((Int32)0)]
			public Int32 LotItemId07 {
				get { return lotItemId07; }
				set {
					if ((double)value < -1 || (double)value > 1E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+09 for LotItemId07.");
					SetProperty(ref lotItemId07, ref value, LotItemId07Property);
				}
			}

			/// <summary>8 : item ID</summary>
			/// <remarks>
			/// Japanese short name: "８：アイテムID", Google translated: "8 : item ID".
			/// Japanese description: "取得できるアイテムのID", Google translated: "ID of the item that can be retrieved".
			/// </remarks>
			[ParameterTableRowAttribute("lotItemId08", index: 7, minimum: -1, maximum: 1E+09, step: 1, sortOrder: 6000, unknown2: 1)]
			[DisplayName("8 : item ID")]
			[Description("ID of the item that can be retrieved")]
			[DefaultValue((Int32)0)]
			public Int32 LotItemId08 {
				get { return lotItemId08; }
				set {
					if ((double)value < -1 || (double)value > 1E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+09 for LotItemId08.");
					SetProperty(ref lotItemId08, ref value, LotItemId08Property);
				}
			}

			/// <summary>1 : Item category</summary>
			/// <remarks>
			/// Japanese short name: "１：アイテムカテゴリ", Google translated: "1 : Item category".
			/// Japanese description: "取得できるアイテムのカテゴリ", Google translated: "Category of items that can be retrieved".
			/// </remarks>
			[ParameterTableRowAttribute("lotItemCategory01", index: 8, minimum: -1, maximum: 2.147484E+09, step: 1, sortOrder: 300, unknown2: 5)]
			[DisplayName("1 : Item category")]
			[Description("Category of items that can be retrieved")]
			[DefaultValue((RewardItemCategory)0)]
			public RewardItemCategory LotItemCategory01 {
				get { return lotItemCategory01; }
				set { SetProperty(ref lotItemCategory01, ref value, LotItemCategory01Property); }
			}

			/// <summary>2 : Item category</summary>
			/// <remarks>
			/// Japanese short name: "２：アイテムカテゴリ", Google translated: "2 : Item category".
			/// Japanese description: "取得できるアイテムのカテゴリ", Google translated: "Category of items that can be retrieved".
			/// </remarks>
			[ParameterTableRowAttribute("lotItemCategory02", index: 9, minimum: -1, maximum: 2.147484E+09, step: 1, sortOrder: 1100, unknown2: 5)]
			[DisplayName("2 : Item category")]
			[Description("Category of items that can be retrieved")]
			[DefaultValue((RewardItemCategory)0)]
			public RewardItemCategory LotItemCategory02 {
				get { return lotItemCategory02; }
				set { SetProperty(ref lotItemCategory02, ref value, LotItemCategory02Property); }
			}

			/// <summary>3 : Item category</summary>
			/// <remarks>
			/// Japanese short name: "３：アイテムカテゴリ", Google translated: "3 : Item category".
			/// Japanese description: "取得できるアイテムのカテゴリ", Google translated: "Category of items that can be retrieved".
			/// </remarks>
			[ParameterTableRowAttribute("lotItemCategory03", index: 10, minimum: -1, maximum: 2.147484E+09, step: 1, sortOrder: 1900, unknown2: 5)]
			[DisplayName("3 : Item category")]
			[Description("Category of items that can be retrieved")]
			[DefaultValue((RewardItemCategory)0)]
			public RewardItemCategory LotItemCategory03 {
				get { return lotItemCategory03; }
				set { SetProperty(ref lotItemCategory03, ref value, LotItemCategory03Property); }
			}

			/// <summary>4 : Item category</summary>
			/// <remarks>
			/// Japanese short name: "４：アイテムカテゴリ", Google translated: "4 : Item category".
			/// Japanese description: "取得できるアイテムのカテゴリ", Google translated: "Category of items that can be retrieved".
			/// </remarks>
			[ParameterTableRowAttribute("lotItemCategory04", index: 11, minimum: -1, maximum: 2.147484E+09, step: 1, sortOrder: 2700, unknown2: 5)]
			[DisplayName("4 : Item category")]
			[Description("Category of items that can be retrieved")]
			[DefaultValue((RewardItemCategory)0)]
			public RewardItemCategory LotItemCategory04 {
				get { return lotItemCategory04; }
				set { SetProperty(ref lotItemCategory04, ref value, LotItemCategory04Property); }
			}

			/// <summary>5 : Item category</summary>
			/// <remarks>
			/// Japanese short name: "５：アイテムカテゴリ", Google translated: "5 : Item category".
			/// Japanese description: "取得できるアイテムのカテゴリ", Google translated: "Category of items that can be retrieved".
			/// </remarks>
			[ParameterTableRowAttribute("lotItemCategory05", index: 12, minimum: -1, maximum: 2.147484E+09, step: 1, sortOrder: 3500, unknown2: 5)]
			[DisplayName("5 : Item category")]
			[Description("Category of items that can be retrieved")]
			[DefaultValue((RewardItemCategory)0)]
			public RewardItemCategory LotItemCategory05 {
				get { return lotItemCategory05; }
				set { SetProperty(ref lotItemCategory05, ref value, LotItemCategory05Property); }
			}

			/// <summary>6 : Item category</summary>
			/// <remarks>
			/// Japanese short name: "６：アイテムカテゴリ", Google translated: "6 : Item category".
			/// Japanese description: "取得できるアイテムのカテゴリ", Google translated: "Category of items that can be retrieved".
			/// </remarks>
			[ParameterTableRowAttribute("lotItemCategory06", index: 13, minimum: -1, maximum: 2.147484E+09, step: 1, sortOrder: 4300, unknown2: 5)]
			[DisplayName("6 : Item category")]
			[Description("Category of items that can be retrieved")]
			[DefaultValue((RewardItemCategory)0)]
			public RewardItemCategory LotItemCategory06 {
				get { return lotItemCategory06; }
				set { SetProperty(ref lotItemCategory06, ref value, LotItemCategory06Property); }
			}

			/// <summary>7 : Items category</summary>
			/// <remarks>
			/// Japanese short name: "７：アイテムカテゴリ", Google translated: "7 : Items category".
			/// Japanese description: "取得できるアイテムのカテゴリ", Google translated: "Category of items that can be retrieved".
			/// </remarks>
			[ParameterTableRowAttribute("lotItemCategory07", index: 14, minimum: -1, maximum: 2.147484E+09, step: 1, sortOrder: 5100, unknown2: 5)]
			[DisplayName("7 : Items category")]
			[Description("Category of items that can be retrieved")]
			[DefaultValue((RewardItemCategory)0)]
			public RewardItemCategory LotItemCategory07 {
				get { return lotItemCategory07; }
				set { SetProperty(ref lotItemCategory07, ref value, LotItemCategory07Property); }
			}

			/// <summary>8 : Item category</summary>
			/// <remarks>
			/// Japanese short name: "８：アイテムカテゴリ", Google translated: "8 : Item category".
			/// Japanese description: "取得できるアイテムのカテゴリ", Google translated: "Category of items that can be retrieved".
			/// </remarks>
			[ParameterTableRowAttribute("lotItemCategory08", index: 15, minimum: -1, maximum: 2.147484E+09, step: 1, sortOrder: 5900, unknown2: 5)]
			[DisplayName("8 : Item category")]
			[Description("Category of items that can be retrieved")]
			[DefaultValue((RewardItemCategory)0)]
			public RewardItemCategory LotItemCategory08 {
				get { return lotItemCategory08; }
				set { SetProperty(ref lotItemCategory08, ref value, LotItemCategory08Property); }
			}

			/// <summary>Basic appearance point</summary>
			/// <remarks>
			/// Japanese short name: "基本出現ポイント", Google translated: "Basic appearance point".
			/// Japanese description: "通常時の出現ポイント", Google translated: "Emergence point at the normal time".
			/// </remarks>
			[ParameterTableRowAttribute("lotItemBasePoint01", index: 16, minimum: 0, maximum: 2000, step: 1, sortOrder: 600, unknown2: 1)]
			[DisplayName("Basic appearance point")]
			[Description("Emergence point at the normal time")]
			[DefaultValue((UInt16)0)]
			public UInt16 LotItemBasePoint01 {
				get { return lotItemBasePoint01; }
				set {
					if ((double)value < 0 || (double)value > 2000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 2000 for LotItemBasePoint01.");
					SetProperty(ref lotItemBasePoint01, ref value, LotItemBasePoint01Property);
				}
			}

			/// <summary>Basic appearance point</summary>
			/// <remarks>
			/// Japanese short name: "基本出現ポイント", Google translated: "Basic appearance point".
			/// Japanese description: "通常時の出現ポイント", Google translated: "Emergence point at the normal time".
			/// </remarks>
			[ParameterTableRowAttribute("lotItemBasePoint02", index: 17, minimum: 0, maximum: 2000, step: 1, sortOrder: 1400, unknown2: 1)]
			[DisplayName("Basic appearance point")]
			[Description("Emergence point at the normal time")]
			[DefaultValue((UInt16)0)]
			public UInt16 LotItemBasePoint02 {
				get { return lotItemBasePoint02; }
				set {
					if ((double)value < 0 || (double)value > 2000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 2000 for LotItemBasePoint02.");
					SetProperty(ref lotItemBasePoint02, ref value, LotItemBasePoint02Property);
				}
			}

			/// <summary>Basic appearance point</summary>
			/// <remarks>
			/// Japanese short name: "基本出現ポイント", Google translated: "Basic appearance point".
			/// Japanese description: "通常時の出現ポイント", Google translated: "Emergence point at the normal time".
			/// </remarks>
			[ParameterTableRowAttribute("lotItemBasePoint03", index: 18, minimum: 0, maximum: 2000, step: 1, sortOrder: 2200, unknown2: 1)]
			[DisplayName("Basic appearance point")]
			[Description("Emergence point at the normal time")]
			[DefaultValue((UInt16)0)]
			public UInt16 LotItemBasePoint03 {
				get { return lotItemBasePoint03; }
				set {
					if ((double)value < 0 || (double)value > 2000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 2000 for LotItemBasePoint03.");
					SetProperty(ref lotItemBasePoint03, ref value, LotItemBasePoint03Property);
				}
			}

			/// <summary>Basic appearance point</summary>
			/// <remarks>
			/// Japanese short name: "基本出現ポイント", Google translated: "Basic appearance point".
			/// Japanese description: "通常時の出現ポイント", Google translated: "Emergence point at the normal time".
			/// </remarks>
			[ParameterTableRowAttribute("lotItemBasePoint04", index: 19, minimum: 0, maximum: 2000, step: 1, sortOrder: 3000, unknown2: 1)]
			[DisplayName("Basic appearance point")]
			[Description("Emergence point at the normal time")]
			[DefaultValue((UInt16)0)]
			public UInt16 LotItemBasePoint04 {
				get { return lotItemBasePoint04; }
				set {
					if ((double)value < 0 || (double)value > 2000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 2000 for LotItemBasePoint04.");
					SetProperty(ref lotItemBasePoint04, ref value, LotItemBasePoint04Property);
				}
			}

			/// <summary>Basic appearance point</summary>
			/// <remarks>
			/// Japanese short name: "基本出現ポイント", Google translated: "Basic appearance point".
			/// Japanese description: "通常時の出現ポイント", Google translated: "Emergence point at the normal time".
			/// </remarks>
			[ParameterTableRowAttribute("lotItemBasePoint05", index: 20, minimum: 0, maximum: 2000, step: 1, sortOrder: 3800, unknown2: 1)]
			[DisplayName("Basic appearance point")]
			[Description("Emergence point at the normal time")]
			[DefaultValue((UInt16)0)]
			public UInt16 LotItemBasePoint05 {
				get { return lotItemBasePoint05; }
				set {
					if ((double)value < 0 || (double)value > 2000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 2000 for LotItemBasePoint05.");
					SetProperty(ref lotItemBasePoint05, ref value, LotItemBasePoint05Property);
				}
			}

			/// <summary>Basic appearance point</summary>
			/// <remarks>
			/// Japanese short name: "基本出現ポイント", Google translated: "Basic appearance point".
			/// Japanese description: "通常時の出現ポイント", Google translated: "Emergence point at the normal time".
			/// </remarks>
			[ParameterTableRowAttribute("lotItemBasePoint06", index: 21, minimum: 0, maximum: 2000, step: 1, sortOrder: 4600, unknown2: 1)]
			[DisplayName("Basic appearance point")]
			[Description("Emergence point at the normal time")]
			[DefaultValue((UInt16)0)]
			public UInt16 LotItemBasePoint06 {
				get { return lotItemBasePoint06; }
				set {
					if ((double)value < 0 || (double)value > 2000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 2000 for LotItemBasePoint06.");
					SetProperty(ref lotItemBasePoint06, ref value, LotItemBasePoint06Property);
				}
			}

			/// <summary>Basic appearance point</summary>
			/// <remarks>
			/// Japanese short name: "基本出現ポイント", Google translated: "Basic appearance point".
			/// Japanese description: "通常時の出現ポイント", Google translated: "Emergence point at the normal time".
			/// </remarks>
			[ParameterTableRowAttribute("lotItemBasePoint07", index: 22, minimum: 0, maximum: 2000, step: 1, sortOrder: 5400, unknown2: 1)]
			[DisplayName("Basic appearance point")]
			[Description("Emergence point at the normal time")]
			[DefaultValue((UInt16)0)]
			public UInt16 LotItemBasePoint07 {
				get { return lotItemBasePoint07; }
				set {
					if ((double)value < 0 || (double)value > 2000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 2000 for LotItemBasePoint07.");
					SetProperty(ref lotItemBasePoint07, ref value, LotItemBasePoint07Property);
				}
			}

			/// <summary>Basic appearance point</summary>
			/// <remarks>
			/// Japanese short name: "基本出現ポイント", Google translated: "Basic appearance point".
			/// Japanese description: "通常時の出現ポイント", Google translated: "Emergence point at the normal time".
			/// </remarks>
			[ParameterTableRowAttribute("lotItemBasePoint08", index: 23, minimum: 0, maximum: 2000, step: 1, sortOrder: 6200, unknown2: 1)]
			[DisplayName("Basic appearance point")]
			[Description("Emergence point at the normal time")]
			[DefaultValue((UInt16)0)]
			public UInt16 LotItemBasePoint08 {
				get { return lotItemBasePoint08; }
				set {
					if ((double)value < 0 || (double)value > 2000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 2000 for LotItemBasePoint08.");
					SetProperty(ref lotItemBasePoint08, ref value, LotItemBasePoint08Property);
				}
			}

			/// <summary>The cumulative post- emergence point</summary>
			/// <remarks>
			/// Japanese short name: "累積後出現ポイント", Google translated: "The cumulative post- emergence point".
			/// Japanese description: "最大累積時の出現ポイント", Google translated: "Appearance point of maximum cumulative time".
			/// </remarks>
			[ParameterTableRowAttribute("cumulateLotPoint01", index: 24, minimum: 0, maximum: 2000, step: 1, sortOrder: 700, unknown2: 1)]
			[DisplayName("The cumulative post- emergence point")]
			[Description("Appearance point of maximum cumulative time")]
			[DefaultValue((UInt16)0)]
			public UInt16 CumulateLotPoint01 {
				get { return cumulateLotPoint01; }
				set {
					if ((double)value < 0 || (double)value > 2000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 2000 for CumulateLotPoint01.");
					SetProperty(ref cumulateLotPoint01, ref value, CumulateLotPoint01Property);
				}
			}

			/// <summary>The cumulative post- emergence point</summary>
			/// <remarks>
			/// Japanese short name: "累積後出現ポイント", Google translated: "The cumulative post- emergence point".
			/// Japanese description: "最大累積時の出現ポイント", Google translated: "Appearance point of maximum cumulative time".
			/// </remarks>
			[ParameterTableRowAttribute("cumulateLotPoint02", index: 25, minimum: 0, maximum: 2000, step: 1, sortOrder: 1500, unknown2: 1)]
			[DisplayName("The cumulative post- emergence point")]
			[Description("Appearance point of maximum cumulative time")]
			[DefaultValue((UInt16)0)]
			public UInt16 CumulateLotPoint02 {
				get { return cumulateLotPoint02; }
				set {
					if ((double)value < 0 || (double)value > 2000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 2000 for CumulateLotPoint02.");
					SetProperty(ref cumulateLotPoint02, ref value, CumulateLotPoint02Property);
				}
			}

			/// <summary>The cumulative post- emergence point</summary>
			/// <remarks>
			/// Japanese short name: "累積後出現ポイント", Google translated: "The cumulative post- emergence point".
			/// Japanese description: "最大累積時の出現ポイント", Google translated: "Appearance point of maximum cumulative time".
			/// </remarks>
			[ParameterTableRowAttribute("cumulateLotPoint03", index: 26, minimum: 0, maximum: 2000, step: 1, sortOrder: 2300, unknown2: 1)]
			[DisplayName("The cumulative post- emergence point")]
			[Description("Appearance point of maximum cumulative time")]
			[DefaultValue((UInt16)0)]
			public UInt16 CumulateLotPoint03 {
				get { return cumulateLotPoint03; }
				set {
					if ((double)value < 0 || (double)value > 2000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 2000 for CumulateLotPoint03.");
					SetProperty(ref cumulateLotPoint03, ref value, CumulateLotPoint03Property);
				}
			}

			/// <summary>The cumulative post- emergence point</summary>
			/// <remarks>
			/// Japanese short name: "累積後出現ポイント", Google translated: "The cumulative post- emergence point".
			/// Japanese description: "最大累積時の出現ポイント", Google translated: "Appearance point of maximum cumulative time".
			/// </remarks>
			[ParameterTableRowAttribute("cumulateLotPoint04", index: 27, minimum: 0, maximum: 2000, step: 1, sortOrder: 3100, unknown2: 1)]
			[DisplayName("The cumulative post- emergence point")]
			[Description("Appearance point of maximum cumulative time")]
			[DefaultValue((UInt16)0)]
			public UInt16 CumulateLotPoint04 {
				get { return cumulateLotPoint04; }
				set {
					if ((double)value < 0 || (double)value > 2000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 2000 for CumulateLotPoint04.");
					SetProperty(ref cumulateLotPoint04, ref value, CumulateLotPoint04Property);
				}
			}

			/// <summary>The cumulative post- emergence point</summary>
			/// <remarks>
			/// Japanese short name: "累積後出現ポイント", Google translated: "The cumulative post- emergence point".
			/// Japanese description: "最大累積時の出現ポイント", Google translated: "Appearance point of maximum cumulative time".
			/// </remarks>
			[ParameterTableRowAttribute("cumulateLotPoint05", index: 28, minimum: 0, maximum: 2000, step: 1, sortOrder: 3900, unknown2: 1)]
			[DisplayName("The cumulative post- emergence point")]
			[Description("Appearance point of maximum cumulative time")]
			[DefaultValue((UInt16)0)]
			public UInt16 CumulateLotPoint05 {
				get { return cumulateLotPoint05; }
				set {
					if ((double)value < 0 || (double)value > 2000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 2000 for CumulateLotPoint05.");
					SetProperty(ref cumulateLotPoint05, ref value, CumulateLotPoint05Property);
				}
			}

			/// <summary>The cumulative post- emergence point</summary>
			/// <remarks>
			/// Japanese short name: "累積後出現ポイント", Google translated: "The cumulative post- emergence point".
			/// Japanese description: "最大累積時の出現ポイント", Google translated: "Appearance point of maximum cumulative time".
			/// </remarks>
			[ParameterTableRowAttribute("cumulateLotPoint06", index: 29, minimum: 0, maximum: 2000, step: 1, sortOrder: 4700, unknown2: 1)]
			[DisplayName("The cumulative post- emergence point")]
			[Description("Appearance point of maximum cumulative time")]
			[DefaultValue((UInt16)0)]
			public UInt16 CumulateLotPoint06 {
				get { return cumulateLotPoint06; }
				set {
					if ((double)value < 0 || (double)value > 2000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 2000 for CumulateLotPoint06.");
					SetProperty(ref cumulateLotPoint06, ref value, CumulateLotPoint06Property);
				}
			}

			/// <summary>The cumulative post- emergence point</summary>
			/// <remarks>
			/// Japanese short name: "累積後出現ポイント", Google translated: "The cumulative post- emergence point".
			/// Japanese description: "最大累積時の出現ポイント", Google translated: "Appearance point of maximum cumulative time".
			/// </remarks>
			[ParameterTableRowAttribute("cumulateLotPoint07", index: 30, minimum: 0, maximum: 2000, step: 1, sortOrder: 5500, unknown2: 1)]
			[DisplayName("The cumulative post- emergence point")]
			[Description("Appearance point of maximum cumulative time")]
			[DefaultValue((UInt16)0)]
			public UInt16 CumulateLotPoint07 {
				get { return cumulateLotPoint07; }
				set {
					if ((double)value < 0 || (double)value > 2000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 2000 for CumulateLotPoint07.");
					SetProperty(ref cumulateLotPoint07, ref value, CumulateLotPoint07Property);
				}
			}

			/// <summary>The cumulative post- emergence point</summary>
			/// <remarks>
			/// Japanese short name: "累積後出現ポイント", Google translated: "The cumulative post- emergence point".
			/// Japanese description: "最大累積時の出現ポイント", Google translated: "Appearance point of maximum cumulative time".
			/// </remarks>
			[ParameterTableRowAttribute("cumulateLotPoint08", index: 31, minimum: 0, maximum: 2000, step: 1, sortOrder: 6300, unknown2: 1)]
			[DisplayName("The cumulative post- emergence point")]
			[Description("Appearance point of maximum cumulative time")]
			[DefaultValue((UInt16)0)]
			public UInt16 CumulateLotPoint08 {
				get { return cumulateLotPoint08; }
				set {
					if ((double)value < 0 || (double)value > 2000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 2000 for CumulateLotPoint08.");
					SetProperty(ref cumulateLotPoint08, ref value, CumulateLotPoint08Property);
				}
			}

			/// <summary>Another crunch flag ID</summary>
			/// <remarks>
			/// Japanese short name: "別ザクザクフラグID", Google translated: "Another crunch flag ID".
			/// Japanese description: "取得済みフラグとザクザク枠兼用(0:共通使用)", Google translated: "Crunch frame shared with the acquired flag ( 0 : common usage )".
			/// </remarks>
			[ParameterTableRowAttribute("getItemFlagId01", index: 32, minimum: -1E+08, maximum: 1E+08, step: 1, sortOrder: 800, unknown2: 5)]
			[DisplayName("Another crunch flag ID")]
			[Description("Crunch frame shared with the acquired flag ( 0 : common usage )")]
			[DefaultValue((Int32)0)]
			public Int32 GetItemFlagId01 {
				get { return getItemFlagId01; }
				set {
					if ((double)value < -1E+08 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1E+08 to 1E+08 for GetItemFlagId01.");
					SetProperty(ref getItemFlagId01, ref value, GetItemFlagId01Property);
				}
			}

			/// <summary>Another crunch flag ID</summary>
			/// <remarks>
			/// Japanese short name: "別ザクザクフラグID", Google translated: "Another crunch flag ID".
			/// Japanese description: "取得済みフラグとザクザク枠兼用(0:共通使用)", Google translated: "Crunch frame shared with the acquired flag ( 0 : common usage )".
			/// </remarks>
			[ParameterTableRowAttribute("getItemFlagId02", index: 33, minimum: -1E+08, maximum: 1E+08, step: 1, sortOrder: 1600, unknown2: 5)]
			[DisplayName("Another crunch flag ID")]
			[Description("Crunch frame shared with the acquired flag ( 0 : common usage )")]
			[DefaultValue((Int32)0)]
			public Int32 GetItemFlagId02 {
				get { return getItemFlagId02; }
				set {
					if ((double)value < -1E+08 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1E+08 to 1E+08 for GetItemFlagId02.");
					SetProperty(ref getItemFlagId02, ref value, GetItemFlagId02Property);
				}
			}

			/// <summary>Another crunch flag ID</summary>
			/// <remarks>
			/// Japanese short name: "別ザクザクフラグID", Google translated: "Another crunch flag ID".
			/// Japanese description: "取得済みフラグとザクザク枠兼用(0:共通使用)", Google translated: "Crunch frame shared with the acquired flag ( 0 : common usage )".
			/// </remarks>
			[ParameterTableRowAttribute("getItemFlagId03", index: 34, minimum: -1E+08, maximum: 1E+08, step: 1, sortOrder: 2400, unknown2: 5)]
			[DisplayName("Another crunch flag ID")]
			[Description("Crunch frame shared with the acquired flag ( 0 : common usage )")]
			[DefaultValue((Int32)0)]
			public Int32 GetItemFlagId03 {
				get { return getItemFlagId03; }
				set {
					if ((double)value < -1E+08 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1E+08 to 1E+08 for GetItemFlagId03.");
					SetProperty(ref getItemFlagId03, ref value, GetItemFlagId03Property);
				}
			}

			/// <summary>Another crunch flag ID</summary>
			/// <remarks>
			/// Japanese short name: "別ザクザクフラグID", Google translated: "Another crunch flag ID".
			/// Japanese description: "取得済みフラグとザクザク枠兼用(0:共通使用)", Google translated: "Crunch frame shared with the acquired flag ( 0 : common usage )".
			/// </remarks>
			[ParameterTableRowAttribute("getItemFlagId04", index: 35, minimum: -1E+08, maximum: 1E+08, step: 1, sortOrder: 3200, unknown2: 5)]
			[DisplayName("Another crunch flag ID")]
			[Description("Crunch frame shared with the acquired flag ( 0 : common usage )")]
			[DefaultValue((Int32)0)]
			public Int32 GetItemFlagId04 {
				get { return getItemFlagId04; }
				set {
					if ((double)value < -1E+08 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1E+08 to 1E+08 for GetItemFlagId04.");
					SetProperty(ref getItemFlagId04, ref value, GetItemFlagId04Property);
				}
			}

			/// <summary>Another crunch flag ID</summary>
			/// <remarks>
			/// Japanese short name: "別ザクザクフラグID", Google translated: "Another crunch flag ID".
			/// Japanese description: "取得済みフラグとザクザク枠兼用(0:共通使用)", Google translated: "Crunch frame shared with the acquired flag ( 0 : common usage )".
			/// </remarks>
			[ParameterTableRowAttribute("getItemFlagId05", index: 36, minimum: -1E+08, maximum: 1E+08, step: 1, sortOrder: 4000, unknown2: 5)]
			[DisplayName("Another crunch flag ID")]
			[Description("Crunch frame shared with the acquired flag ( 0 : common usage )")]
			[DefaultValue((Int32)0)]
			public Int32 GetItemFlagId05 {
				get { return getItemFlagId05; }
				set {
					if ((double)value < -1E+08 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1E+08 to 1E+08 for GetItemFlagId05.");
					SetProperty(ref getItemFlagId05, ref value, GetItemFlagId05Property);
				}
			}

			/// <summary>Another crunch flag ID</summary>
			/// <remarks>
			/// Japanese short name: "別ザクザクフラグID", Google translated: "Another crunch flag ID".
			/// Japanese description: "取得済みフラグとザクザク枠兼用(0:共通使用)", Google translated: "Crunch frame shared with the acquired flag ( 0 : common usage )".
			/// </remarks>
			[ParameterTableRowAttribute("getItemFlagId06", index: 37, minimum: -1E+08, maximum: 1E+08, step: 1, sortOrder: 4800, unknown2: 5)]
			[DisplayName("Another crunch flag ID")]
			[Description("Crunch frame shared with the acquired flag ( 0 : common usage )")]
			[DefaultValue((Int32)0)]
			public Int32 GetItemFlagId06 {
				get { return getItemFlagId06; }
				set {
					if ((double)value < -1E+08 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1E+08 to 1E+08 for GetItemFlagId06.");
					SetProperty(ref getItemFlagId06, ref value, GetItemFlagId06Property);
				}
			}

			/// <summary>Another crunch flag ID</summary>
			/// <remarks>
			/// Japanese short name: "別ザクザクフラグID", Google translated: "Another crunch flag ID".
			/// Japanese description: "取得済みフラグとザクザク枠兼用(0:共通使用)", Google translated: "Crunch frame shared with the acquired flag ( 0 : common usage )".
			/// </remarks>
			[ParameterTableRowAttribute("getItemFlagId07", index: 38, minimum: -1E+08, maximum: 1E+08, step: 1, sortOrder: 5600, unknown2: 5)]
			[DisplayName("Another crunch flag ID")]
			[Description("Crunch frame shared with the acquired flag ( 0 : common usage )")]
			[DefaultValue((Int32)0)]
			public Int32 GetItemFlagId07 {
				get { return getItemFlagId07; }
				set {
					if ((double)value < -1E+08 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1E+08 to 1E+08 for GetItemFlagId07.");
					SetProperty(ref getItemFlagId07, ref value, GetItemFlagId07Property);
				}
			}

			/// <summary>Another crunch flag ID</summary>
			/// <remarks>
			/// Japanese short name: "別ザクザクフラグID", Google translated: "Another crunch flag ID".
			/// Japanese description: "取得済みフラグとザクザク枠兼用(0:共通使用)", Google translated: "Crunch frame shared with the acquired flag ( 0 : common usage )".
			/// </remarks>
			[ParameterTableRowAttribute("getItemFlagId08", index: 39, minimum: -1E+08, maximum: 1E+08, step: 1, sortOrder: 6400, unknown2: 5)]
			[DisplayName("Another crunch flag ID")]
			[Description("Crunch frame shared with the acquired flag ( 0 : common usage )")]
			[DefaultValue((Int32)0)]
			public Int32 GetItemFlagId08 {
				get { return getItemFlagId08; }
				set {
					if ((double)value < -1E+08 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1E+08 to 1E+08 for GetItemFlagId08.");
					SetProperty(ref getItemFlagId08, ref value, GetItemFlagId08Property);
				}
			}

			/// <summary>Crunch flag ID</summary>
			/// <remarks>
			/// Japanese short name: "ザクザクフラグID", Google translated: "Crunch flag ID".
			/// Japanese description: "取得済みフラグとザクザク枠兼用(&lt;0:フラグ無効)", Google translated: "Crunch frame shared with the acquired flag ( &lt; 0 : invalid flag )".
			/// </remarks>
			[ParameterTableRowAttribute("getItemFlagId", index: 40, minimum: -1E+08, maximum: 1E+08, step: 1, sortOrder: 150, unknown2: 5)]
			[DisplayName("Crunch flag ID")]
			[Description("Crunch frame shared with the acquired flag ( < 0 : invalid flag )")]
			[DefaultValue((Int32)(-1))]
			public Int32 GetItemFlagId {
				get { return getItemFlagId; }
				set {
					if ((double)value < -1E+08 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1E+08 to 1E+08 for GetItemFlagId.");
					SetProperty(ref getItemFlagId, ref value, GetItemFlagIdProperty);
				}
			}

			/// <summary>Lottery cumulative save flag ID</summary>
			/// <remarks>
			/// Japanese short name: "抽選累積保存フラグID", Google translated: "Lottery cumulative save flag ID".
			/// Japanese description: "抽選回数保存用(※8フラグ連番使用)", Google translated: "Lottery number storage (※ 8 flag sequence number used)".
			/// </remarks>
			[ParameterTableRowAttribute("cumulateNumFlagId", index: 41, minimum: -1, maximum: 1E+08, step: 1, sortOrder: 160, unknown2: 5)]
			[DisplayName("Lottery cumulative save flag ID")]
			[Description("Lottery number storage (※ 8 flag sequence number used)")]
			[DefaultValue((Int32)(-1))]
			public Int32 CumulateNumFlagId {
				get { return cumulateNumFlagId; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for CumulateNumFlagId.");
					SetProperty(ref cumulateNumFlagId, ref value, CumulateNumFlagIdProperty);
				}
			}

			/// <summary>Lottery cumulative maximum number</summary>
			/// <remarks>
			/// Japanese short name: "抽選累積最大数", Google translated: "Lottery cumulative maximum number".
			/// Japanese description: "抽選累積最大数(0:累積なし)", Google translated: "Lottery cumulative maximum number (0: non-accumulative )".
			/// </remarks>
			[ParameterTableRowAttribute("cumulateNumMax", index: 42, minimum: 0, maximum: 255, step: 1, sortOrder: 170, unknown2: 1)]
			[DisplayName("Lottery cumulative maximum number")]
			[Description("Lottery cumulative maximum number (0: non-accumulative )")]
			[DefaultValue((Byte)0)]
			public Byte CumulateNumMax {
				get { return cumulateNumMax; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for CumulateNumMax.");
					SetProperty(ref cumulateNumMax, ref value, CumulateNumMaxProperty);
				}
			}

			/// <summary>Rarity setting</summary>
			/// <remarks>
			/// Japanese short name: "レアリティ設定", Google translated: "Rarity setting".
			/// Japanese description: "宝箱などに、どれくらい貴重なアイテムが入っているかを指定する", Google translated: "I will be identified as such the treasure chest , valuable items whether on how much".
			/// </remarks>
			[ParameterTableRowAttribute("lotItem_Rarity", index: 43, minimum: 0, maximum: 10, step: 1, sortOrder: 100, unknown2: 1)]
			[DisplayName("Rarity setting")]
			[Description("I will be identified as such the treasure chest , valuable items whether on how much")]
			[DefaultValue((Byte)0)]
			public Byte Rarity {
				get { return rarity; }
				set {
					if ((double)value < 0 || (double)value > 10)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 10 for " + RarityProperty.Name + ".");
					SetProperty(ref rarity, ref value, RarityProperty);
				}
			}

			/// <summary>Number</summary>
			/// <remarks>
			/// Japanese short name: "個数", Google translated: "Number".
			/// Japanese description: "取得できるアイテムの個数", Google translated: "Number of items that can be retrieved".
			/// </remarks>
			[ParameterTableRowAttribute("lotItemNum01", index: 44, minimum: 0, maximum: 99, step: 1, sortOrder: 500, unknown2: 1)]
			[DisplayName("Number")]
			[Description("Number of items that can be retrieved")]
			[DefaultValue((Byte)0)]
			public Byte LotItemNum01 {
				get { return lotItemNum01; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for LotItemNum01.");
					SetProperty(ref lotItemNum01, ref value, LotItemNum01Property);
				}
			}

			/// <summary>Number</summary>
			/// <remarks>
			/// Japanese short name: "個数", Google translated: "Number".
			/// Japanese description: "取得できるアイテムの個数", Google translated: "Number of items that can be retrieved".
			/// </remarks>
			[ParameterTableRowAttribute("lotItemNum02", index: 45, minimum: 0, maximum: 99, step: 1, sortOrder: 1300, unknown2: 1)]
			[DisplayName("Number")]
			[Description("Number of items that can be retrieved")]
			[DefaultValue((Byte)0)]
			public Byte LotItemNum02 {
				get { return lotItemNum02; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for LotItemNum02.");
					SetProperty(ref lotItemNum02, ref value, LotItemNum02Property);
				}
			}

			/// <summary>Number</summary>
			/// <remarks>
			/// Japanese short name: "個数", Google translated: "Number".
			/// Japanese description: "取得できるアイテムの個数", Google translated: "Number of items that can be retrieved".
			/// </remarks>
			[ParameterTableRowAttribute("lotItemNum03", index: 46, minimum: 0, maximum: 99, step: 1, sortOrder: 2100, unknown2: 1)]
			[DisplayName("Number")]
			[Description("Number of items that can be retrieved")]
			[DefaultValue((Byte)0)]
			public Byte LotItemNum03 {
				get { return lotItemNum03; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for LotItemNum03.");
					SetProperty(ref lotItemNum03, ref value, LotItemNum03Property);
				}
			}

			/// <summary>Number</summary>
			/// <remarks>
			/// Japanese short name: "個数", Google translated: "Number".
			/// Japanese description: "取得できるアイテムの個数", Google translated: "Number of items that can be retrieved".
			/// </remarks>
			[ParameterTableRowAttribute("lotItemNum04", index: 47, minimum: 0, maximum: 99, step: 1, sortOrder: 2900, unknown2: 1)]
			[DisplayName("Number")]
			[Description("Number of items that can be retrieved")]
			[DefaultValue((Byte)0)]
			public Byte LotItemNum04 {
				get { return lotItemNum04; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for LotItemNum04.");
					SetProperty(ref lotItemNum04, ref value, LotItemNum04Property);
				}
			}

			/// <summary>Number</summary>
			/// <remarks>
			/// Japanese short name: "個数", Google translated: "Number".
			/// Japanese description: "取得できるアイテムの個数", Google translated: "Number of items that can be retrieved".
			/// </remarks>
			[ParameterTableRowAttribute("lotItemNum05", index: 48, minimum: 0, maximum: 99, step: 1, sortOrder: 3700, unknown2: 1)]
			[DisplayName("Number")]
			[Description("Number of items that can be retrieved")]
			[DefaultValue((Byte)0)]
			public Byte LotItemNum05 {
				get { return lotItemNum05; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for LotItemNum05.");
					SetProperty(ref lotItemNum05, ref value, LotItemNum05Property);
				}
			}

			/// <summary>Number</summary>
			/// <remarks>
			/// Japanese short name: "個数", Google translated: "Number".
			/// Japanese description: "取得できるアイテムの個数", Google translated: "Number of items that can be retrieved".
			/// </remarks>
			[ParameterTableRowAttribute("lotItemNum06", index: 49, minimum: 0, maximum: 99, step: 1, sortOrder: 4500, unknown2: 1)]
			[DisplayName("Number")]
			[Description("Number of items that can be retrieved")]
			[DefaultValue((Byte)0)]
			public Byte LotItemNum06 {
				get { return lotItemNum06; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for LotItemNum06.");
					SetProperty(ref lotItemNum06, ref value, LotItemNum06Property);
				}
			}

			/// <summary>Number</summary>
			/// <remarks>
			/// Japanese short name: "個数", Google translated: "Number".
			/// Japanese description: "取得できるアイテムの個数", Google translated: "Number of items that can be retrieved".
			/// </remarks>
			[ParameterTableRowAttribute("lotItemNum07", index: 50, minimum: 0, maximum: 99, step: 1, sortOrder: 5300, unknown2: 1)]
			[DisplayName("Number")]
			[Description("Number of items that can be retrieved")]
			[DefaultValue((Byte)0)]
			public Byte LotItemNum07 {
				get { return lotItemNum07; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for LotItemNum07.");
					SetProperty(ref lotItemNum07, ref value, LotItemNum07Property);
				}
			}

			/// <summary>Number</summary>
			/// <remarks>
			/// Japanese short name: "個数", Google translated: "Number".
			/// Japanese description: "取得できるアイテムの個数", Google translated: "Number of items that can be retrieved".
			/// </remarks>
			[ParameterTableRowAttribute("lotItemNum08", index: 51, minimum: 0, maximum: 99, step: 1, sortOrder: 6100, unknown2: 1)]
			[DisplayName("Number")]
			[Description("Number of items that can be retrieved")]
			[DefaultValue((Byte)0)]
			public Byte LotItemNum08 {
				get { return lotItemNum08; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for LotItemNum08.");
					SetProperty(ref lotItemNum08, ref value, LotItemNum08Property);
				}
			}

			/// <summary>Luck parameter validation</summary>
			/// <remarks>
			/// Japanese short name: "運パラメータ有効", Google translated: "Luck parameter validation".
			/// Japanese description: "抽選の確率をプレイヤーの運を反映させるか", Google translated: "Or to reflect the luck of the player the probability of lottery".
			/// </remarks>
			[ParameterTableRowAttribute("enableLuck01:1", index: 52, minimum: 0, maximum: 1, step: 1, sortOrder: 900, unknown2: 1)]
			[DisplayName("Luck parameter validation")]
			[Description("Or to reflect the luck of the player the probability of lottery")]
			[DefaultValue((RewardEnableLuck)0)]
			public RewardEnableLuck EnableLuck01 {
				get { return (RewardEnableLuck)GetBitProperty(0, 1, EnableLuck01Property); }
				set { SetBitProperty(0, 1, (int)value, EnableLuck01Property); }
			}

			/// <summary>Luck parameter validation</summary>
			/// <remarks>
			/// Japanese short name: "運パラメータ有効", Google translated: "Luck parameter validation".
			/// Japanese description: "抽選の確率をプレイヤーの運を反映させるか", Google translated: "Or to reflect the luck of the player the probability of lottery".
			/// </remarks>
			[ParameterTableRowAttribute("enableLuck02:1", index: 53, minimum: 0, maximum: 1, step: 1, sortOrder: 1700, unknown2: 1)]
			[DisplayName("Luck parameter validation")]
			[Description("Or to reflect the luck of the player the probability of lottery")]
			[DefaultValue((RewardEnableLuck)0)]
			public RewardEnableLuck EnableLuck02 {
				get { return (RewardEnableLuck)GetBitProperty(1, 1, EnableLuck02Property); }
				set { SetBitProperty(1, 1, (int)value, EnableLuck02Property); }
			}

			/// <summary>Luck parameter validation</summary>
			/// <remarks>
			/// Japanese short name: "運パラメータ有効", Google translated: "Luck parameter validation".
			/// Japanese description: "抽選の確率をプレイヤーの運を反映させるか", Google translated: "Or to reflect the luck of the player the probability of lottery".
			/// </remarks>
			[ParameterTableRowAttribute("enableLuck03:1", index: 54, minimum: 0, maximum: 1, step: 1, sortOrder: 2500, unknown2: 1)]
			[DisplayName("Luck parameter validation")]
			[Description("Or to reflect the luck of the player the probability of lottery")]
			[DefaultValue((RewardEnableLuck)0)]
			public RewardEnableLuck EnableLuck03 {
				get { return (RewardEnableLuck)GetBitProperty(2, 1, EnableLuck03Property); }
				set { SetBitProperty(2, 1, (int)value, EnableLuck03Property); }
			}

			/// <summary>Luck parameter validation</summary>
			/// <remarks>
			/// Japanese short name: "運パラメータ有効", Google translated: "Luck parameter validation".
			/// Japanese description: "抽選の確率をプレイヤーの運を反映させるか", Google translated: "Or to reflect the luck of the player the probability of lottery".
			/// </remarks>
			[ParameterTableRowAttribute("enableLuck04:1", index: 55, minimum: 0, maximum: 1, step: 1, sortOrder: 3300, unknown2: 1)]
			[DisplayName("Luck parameter validation")]
			[Description("Or to reflect the luck of the player the probability of lottery")]
			[DefaultValue((RewardEnableLuck)0)]
			public RewardEnableLuck EnableLuck04 {
				get { return (RewardEnableLuck)GetBitProperty(3, 1, EnableLuck04Property); }
				set { SetBitProperty(3, 1, (int)value, EnableLuck04Property); }
			}

			/// <summary>Luck parameter validation</summary>
			/// <remarks>
			/// Japanese short name: "運パラメータ有効", Google translated: "Luck parameter validation".
			/// Japanese description: "抽選の確率をプレイヤーの運を反映させるか", Google translated: "Or to reflect the luck of the player the probability of lottery".
			/// </remarks>
			[ParameterTableRowAttribute("enableLuck05:1", index: 56, minimum: 0, maximum: 1, step: 1, sortOrder: 4100, unknown2: 1)]
			[DisplayName("Luck parameter validation")]
			[Description("Or to reflect the luck of the player the probability of lottery")]
			[DefaultValue((RewardEnableLuck)0)]
			public RewardEnableLuck EnableLuck05 {
				get { return (RewardEnableLuck)GetBitProperty(4, 1, EnableLuck05Property); }
				set { SetBitProperty(4, 1, (int)value, EnableLuck05Property); }
			}

			/// <summary>Luck parameter validation</summary>
			/// <remarks>
			/// Japanese short name: "運パラメータ有効", Google translated: "Luck parameter validation".
			/// Japanese description: "抽選の確率をプレイヤーの運を反映させるか", Google translated: "Or to reflect the luck of the player the probability of lottery".
			/// </remarks>
			[ParameterTableRowAttribute("enableLuck06:1", index: 57, minimum: 0, maximum: 1, step: 1, sortOrder: 4900, unknown2: 1)]
			[DisplayName("Luck parameter validation")]
			[Description("Or to reflect the luck of the player the probability of lottery")]
			[DefaultValue((RewardEnableLuck)0)]
			public RewardEnableLuck EnableLuck06 {
				get { return (RewardEnableLuck)GetBitProperty(5, 1, EnableLuck06Property); }
				set { SetBitProperty(5, 1, (int)value, EnableLuck06Property); }
			}

			/// <summary>Luck parameter validation</summary>
			/// <remarks>
			/// Japanese short name: "運パラメータ有効", Google translated: "Luck parameter validation".
			/// Japanese description: "抽選の確率をプレイヤーの運を反映させるか", Google translated: "Or to reflect the luck of the player the probability of lottery".
			/// </remarks>
			[ParameterTableRowAttribute("enableLuck07:1", index: 58, minimum: 0, maximum: 1, step: 1, sortOrder: 5700, unknown2: 1)]
			[DisplayName("Luck parameter validation")]
			[Description("Or to reflect the luck of the player the probability of lottery")]
			[DefaultValue((RewardEnableLuck)0)]
			public RewardEnableLuck EnableLuck07 {
				get { return (RewardEnableLuck)GetBitProperty(6, 1, EnableLuck07Property); }
				set { SetBitProperty(6, 1, (int)value, EnableLuck07Property); }
			}

			/// <summary>Luck parameter validation</summary>
			/// <remarks>
			/// Japanese short name: "運パラメータ有効", Google translated: "Luck parameter validation".
			/// Japanese description: "抽選の確率をプレイヤーの運を反映させるか", Google translated: "Or to reflect the luck of the player the probability of lottery".
			/// </remarks>
			[ParameterTableRowAttribute("enableLuck08:1", index: 59, minimum: 0, maximum: 1, step: 1, sortOrder: 6500, unknown2: 1)]
			[DisplayName("Luck parameter validation")]
			[Description("Or to reflect the luck of the player the probability of lottery")]
			[DefaultValue((RewardEnableLuck)0)]
			public RewardEnableLuck EnableLuck08 {
				get { return (RewardEnableLuck)GetBitProperty(7, 1, EnableLuck08Property); }
				set { SetBitProperty(7, 1, (int)value, EnableLuck08Property); }
			}

			/// <summary>Cumulative reset</summary>
			/// <remarks>
			/// Japanese short name: "累積リセット", Google translated: "Cumulative reset".
			/// Japanese description: "累積リセットするか", Google translated: "Or reset the cumulative".
			/// </remarks>
			[ParameterTableRowAttribute("cumulateReset01:1", index: 60, minimum: 0, maximum: 1, step: 1, sortOrder: 950, unknown2: 1)]
			[DisplayName("Cumulative reset")]
			[Description("Or reset the cumulative")]
			[DefaultValue((RewardCumulateReset)0)]
			public RewardCumulateReset CumulateReset01 {
				get { return (RewardCumulateReset)GetBitProperty(8, 1, CumulateReset01Property); }
				set { SetBitProperty(8, 1, (int)value, CumulateReset01Property); }
			}

			/// <summary>Cumulative reset</summary>
			/// <remarks>
			/// Japanese short name: "累積リセット", Google translated: "Cumulative reset".
			/// Japanese description: "累積リセットするか", Google translated: "Or reset the cumulative".
			/// </remarks>
			[ParameterTableRowAttribute("cumulateReset02:1", index: 61, minimum: 0, maximum: 1, step: 1, sortOrder: 1750, unknown2: 1)]
			[DisplayName("Cumulative reset")]
			[Description("Or reset the cumulative")]
			[DefaultValue((RewardCumulateReset)0)]
			public RewardCumulateReset CumulateReset02 {
				get { return (RewardCumulateReset)GetBitProperty(9, 1, CumulateReset02Property); }
				set { SetBitProperty(9, 1, (int)value, CumulateReset02Property); }
			}

			/// <summary>Cumulative reset</summary>
			/// <remarks>
			/// Japanese short name: "累積リセット", Google translated: "Cumulative reset".
			/// Japanese description: "累積リセットするか", Google translated: "Or reset the cumulative".
			/// </remarks>
			[ParameterTableRowAttribute("cumulateReset03:1", index: 62, minimum: 0, maximum: 1, step: 1, sortOrder: 2550, unknown2: 1)]
			[DisplayName("Cumulative reset")]
			[Description("Or reset the cumulative")]
			[DefaultValue((RewardCumulateReset)0)]
			public RewardCumulateReset CumulateReset03 {
				get { return (RewardCumulateReset)GetBitProperty(10, 1, CumulateReset03Property); }
				set { SetBitProperty(10, 1, (int)value, CumulateReset03Property); }
			}

			/// <summary>Cumulative reset</summary>
			/// <remarks>
			/// Japanese short name: "累積リセット", Google translated: "Cumulative reset".
			/// Japanese description: "累積リセットするか", Google translated: "Or reset the cumulative".
			/// </remarks>
			[ParameterTableRowAttribute("cumulateReset04:1", index: 63, minimum: 0, maximum: 1, step: 1, sortOrder: 3350, unknown2: 1)]
			[DisplayName("Cumulative reset")]
			[Description("Or reset the cumulative")]
			[DefaultValue((RewardCumulateReset)0)]
			public RewardCumulateReset CumulateReset04 {
				get { return (RewardCumulateReset)GetBitProperty(11, 1, CumulateReset04Property); }
				set { SetBitProperty(11, 1, (int)value, CumulateReset04Property); }
			}

			/// <summary>Cumulative reset</summary>
			/// <remarks>
			/// Japanese short name: "累積リセット", Google translated: "Cumulative reset".
			/// Japanese description: "累積リセットするか", Google translated: "Or reset the cumulative".
			/// </remarks>
			[ParameterTableRowAttribute("cumulateReset05:1", index: 64, minimum: 0, maximum: 1, step: 1, sortOrder: 4150, unknown2: 1)]
			[DisplayName("Cumulative reset")]
			[Description("Or reset the cumulative")]
			[DefaultValue((RewardCumulateReset)0)]
			public RewardCumulateReset CumulateReset05 {
				get { return (RewardCumulateReset)GetBitProperty(12, 1, CumulateReset05Property); }
				set { SetBitProperty(12, 1, (int)value, CumulateReset05Property); }
			}

			/// <summary>Cumulative reset</summary>
			/// <remarks>
			/// Japanese short name: "累積リセット", Google translated: "Cumulative reset".
			/// Japanese description: "累積リセットするか", Google translated: "Or reset the cumulative".
			/// </remarks>
			[ParameterTableRowAttribute("cumulateReset06:1", index: 65, minimum: 0, maximum: 1, step: 1, sortOrder: 4950, unknown2: 1)]
			[DisplayName("Cumulative reset")]
			[Description("Or reset the cumulative")]
			[DefaultValue((RewardCumulateReset)0)]
			public RewardCumulateReset CumulateReset06 {
				get { return (RewardCumulateReset)GetBitProperty(13, 1, CumulateReset06Property); }
				set { SetBitProperty(13, 1, (int)value, CumulateReset06Property); }
			}

			/// <summary>Cumulative reset</summary>
			/// <remarks>
			/// Japanese short name: "累積リセット", Google translated: "Cumulative reset".
			/// Japanese description: "累積リセットするか", Google translated: "Or reset the cumulative".
			/// </remarks>
			[ParameterTableRowAttribute("cumulateReset07:1", index: 66, minimum: 0, maximum: 1, step: 1, sortOrder: 5750, unknown2: 1)]
			[DisplayName("Cumulative reset")]
			[Description("Or reset the cumulative")]
			[DefaultValue((RewardCumulateReset)0)]
			public RewardCumulateReset CumulateReset07 {
				get { return (RewardCumulateReset)GetBitProperty(14, 1, CumulateReset07Property); }
				set { SetBitProperty(14, 1, (int)value, CumulateReset07Property); }
			}

			/// <summary>Cumulative reset</summary>
			/// <remarks>
			/// Japanese short name: "累積リセット", Google translated: "Cumulative reset".
			/// Japanese description: "累積リセットするか", Google translated: "Or reset the cumulative".
			/// </remarks>
			[ParameterTableRowAttribute("cumulateReset08:1", index: 67, minimum: 0, maximum: 1, step: 1, sortOrder: 6550, unknown2: 1)]
			[DisplayName("Cumulative reset")]
			[Description("Or reset the cumulative")]
			[DefaultValue((RewardCumulateReset)0)]
			public RewardCumulateReset CumulateReset08 {
				get { return (RewardCumulateReset)GetBitProperty(15, 1, CumulateReset08Property); }
				set { SetBitProperty(15, 1, (int)value, CumulateReset08Property); }
			}

			internal ItemLot(ParameterTable table, int index, AssetLoader loader, int next)
				: base(table, index, loader) {
				BinaryReader reader = loader.Reader;

				LotItemId01 = reader.ReadInt32();
				LotItemId02 = reader.ReadInt32();
				LotItemId03 = reader.ReadInt32();
				LotItemId04 = reader.ReadInt32();
				LotItemId05 = reader.ReadInt32();
				LotItemId06 = reader.ReadInt32();
				LotItemId07 = reader.ReadInt32();
				LotItemId08 = reader.ReadInt32();
				LotItemCategory01 = (RewardItemCategory)reader.ReadInt32();
				LotItemCategory02 = (RewardItemCategory)reader.ReadInt32();
				LotItemCategory03 = (RewardItemCategory)reader.ReadInt32();
				LotItemCategory04 = (RewardItemCategory)reader.ReadInt32();
				LotItemCategory05 = (RewardItemCategory)reader.ReadInt32();
				LotItemCategory06 = (RewardItemCategory)reader.ReadInt32();
				LotItemCategory07 = (RewardItemCategory)reader.ReadInt32();
				LotItemCategory08 = (RewardItemCategory)reader.ReadInt32();
				LotItemBasePoint01 = reader.ReadUInt16();
				LotItemBasePoint02 = reader.ReadUInt16();
				LotItemBasePoint03 = reader.ReadUInt16();
				LotItemBasePoint04 = reader.ReadUInt16();
				LotItemBasePoint05 = reader.ReadUInt16();
				LotItemBasePoint06 = reader.ReadUInt16();
				LotItemBasePoint07 = reader.ReadUInt16();
				LotItemBasePoint08 = reader.ReadUInt16();
				CumulateLotPoint01 = reader.ReadUInt16();
				CumulateLotPoint02 = reader.ReadUInt16();
				CumulateLotPoint03 = reader.ReadUInt16();
				CumulateLotPoint04 = reader.ReadUInt16();
				CumulateLotPoint05 = reader.ReadUInt16();
				CumulateLotPoint06 = reader.ReadUInt16();
				CumulateLotPoint07 = reader.ReadUInt16();
				CumulateLotPoint08 = reader.ReadUInt16();
				GetItemFlagId01 = reader.ReadInt32();
				GetItemFlagId02 = reader.ReadInt32();
				GetItemFlagId03 = reader.ReadInt32();
				GetItemFlagId04 = reader.ReadInt32();
				GetItemFlagId05 = reader.ReadInt32();
				GetItemFlagId06 = reader.ReadInt32();
				GetItemFlagId07 = reader.ReadInt32();
				GetItemFlagId08 = reader.ReadInt32();
				GetItemFlagId = reader.ReadInt32();
				CumulateNumFlagId = reader.ReadInt32();
				CumulateNumMax = reader.ReadByte();
				Rarity = reader.ReadByte();
				LotItemNum01 = reader.ReadByte();
				LotItemNum02 = reader.ReadByte();
				LotItemNum03 = reader.ReadByte();
				LotItemNum04 = reader.ReadByte();
				LotItemNum05 = reader.ReadByte();
				LotItemNum06 = reader.ReadByte();
				LotItemNum07 = reader.ReadByte();
				LotItemNum08 = reader.ReadByte();
				BitFields = reader.ReadBytes(2);
			}

			internal ItemLot(ParameterTable table, int index)
				: base(table, index) {
				BitFields = new byte[2];
				LotItemId01 = (Int32)0;
				LotItemId02 = (Int32)0;
				LotItemId03 = (Int32)0;
				LotItemId04 = (Int32)0;
				LotItemId05 = (Int32)0;
				LotItemId06 = (Int32)0;
				LotItemId07 = (Int32)0;
				LotItemId08 = (Int32)0;
				LotItemCategory01 = (RewardItemCategory)0;
				LotItemCategory02 = (RewardItemCategory)0;
				LotItemCategory03 = (RewardItemCategory)0;
				LotItemCategory04 = (RewardItemCategory)0;
				LotItemCategory05 = (RewardItemCategory)0;
				LotItemCategory06 = (RewardItemCategory)0;
				LotItemCategory07 = (RewardItemCategory)0;
				LotItemCategory08 = (RewardItemCategory)0;
				LotItemBasePoint01 = (UInt16)0;
				LotItemBasePoint02 = (UInt16)0;
				LotItemBasePoint03 = (UInt16)0;
				LotItemBasePoint04 = (UInt16)0;
				LotItemBasePoint05 = (UInt16)0;
				LotItemBasePoint06 = (UInt16)0;
				LotItemBasePoint07 = (UInt16)0;
				LotItemBasePoint08 = (UInt16)0;
				CumulateLotPoint01 = (UInt16)0;
				CumulateLotPoint02 = (UInt16)0;
				CumulateLotPoint03 = (UInt16)0;
				CumulateLotPoint04 = (UInt16)0;
				CumulateLotPoint05 = (UInt16)0;
				CumulateLotPoint06 = (UInt16)0;
				CumulateLotPoint07 = (UInt16)0;
				CumulateLotPoint08 = (UInt16)0;
				GetItemFlagId01 = (Int32)0;
				GetItemFlagId02 = (Int32)0;
				GetItemFlagId03 = (Int32)0;
				GetItemFlagId04 = (Int32)0;
				GetItemFlagId05 = (Int32)0;
				GetItemFlagId06 = (Int32)0;
				GetItemFlagId07 = (Int32)0;
				GetItemFlagId08 = (Int32)0;
				GetItemFlagId = (Int32)(-1);
				CumulateNumFlagId = (Int32)(-1);
				CumulateNumMax = (Byte)0;
				Rarity = (Byte)0;
				LotItemNum01 = (Byte)0;
				LotItemNum02 = (Byte)0;
				LotItemNum03 = (Byte)0;
				LotItemNum04 = (Byte)0;
				LotItemNum05 = (Byte)0;
				LotItemNum06 = (Byte)0;
				LotItemNum07 = (Byte)0;
				LotItemNum08 = (Byte)0;
				EnableLuck01 = (RewardEnableLuck)0;
				EnableLuck02 = (RewardEnableLuck)0;
				EnableLuck03 = (RewardEnableLuck)0;
				EnableLuck04 = (RewardEnableLuck)0;
				EnableLuck05 = (RewardEnableLuck)0;
				EnableLuck06 = (RewardEnableLuck)0;
				EnableLuck07 = (RewardEnableLuck)0;
				EnableLuck08 = (RewardEnableLuck)0;
				CumulateReset01 = (RewardCumulateReset)0;
				CumulateReset02 = (RewardCumulateReset)0;
				CumulateReset03 = (RewardCumulateReset)0;
				CumulateReset04 = (RewardCumulateReset)0;
				CumulateReset05 = (RewardCumulateReset)0;
				CumulateReset06 = (RewardCumulateReset)0;
				CumulateReset07 = (RewardCumulateReset)0;
				CumulateReset08 = (RewardCumulateReset)0;
			}

			/// <summary>Write the <see cref="ItemLot"/> row.</summary>
			/// <param name="writer"></param>
			public override void Write(BinaryWriter writer) {
				writer.Write(LotItemId01);
				writer.Write(LotItemId02);
				writer.Write(LotItemId03);
				writer.Write(LotItemId04);
				writer.Write(LotItemId05);
				writer.Write(LotItemId06);
				writer.Write(LotItemId07);
				writer.Write(LotItemId08);
				writer.Write((Int32)LotItemCategory01);
				writer.Write((Int32)LotItemCategory02);
				writer.Write((Int32)LotItemCategory03);
				writer.Write((Int32)LotItemCategory04);
				writer.Write((Int32)LotItemCategory05);
				writer.Write((Int32)LotItemCategory06);
				writer.Write((Int32)LotItemCategory07);
				writer.Write((Int32)LotItemCategory08);
				writer.Write(LotItemBasePoint01);
				writer.Write(LotItemBasePoint02);
				writer.Write(LotItemBasePoint03);
				writer.Write(LotItemBasePoint04);
				writer.Write(LotItemBasePoint05);
				writer.Write(LotItemBasePoint06);
				writer.Write(LotItemBasePoint07);
				writer.Write(LotItemBasePoint08);
				writer.Write(CumulateLotPoint01);
				writer.Write(CumulateLotPoint02);
				writer.Write(CumulateLotPoint03);
				writer.Write(CumulateLotPoint04);
				writer.Write(CumulateLotPoint05);
				writer.Write(CumulateLotPoint06);
				writer.Write(CumulateLotPoint07);
				writer.Write(CumulateLotPoint08);
				writer.Write(GetItemFlagId01);
				writer.Write(GetItemFlagId02);
				writer.Write(GetItemFlagId03);
				writer.Write(GetItemFlagId04);
				writer.Write(GetItemFlagId05);
				writer.Write(GetItemFlagId06);
				writer.Write(GetItemFlagId07);
				writer.Write(GetItemFlagId08);
				writer.Write(GetItemFlagId);
				writer.Write(CumulateNumFlagId);
				writer.Write(CumulateNumMax);
				writer.Write(Rarity);
				writer.Write(LotItemNum01);
				writer.Write(LotItemNum02);
				writer.Write(LotItemNum03);
				writer.Write(LotItemNum04);
				writer.Write(LotItemNum05);
				writer.Write(LotItemNum06);
				writer.Write(LotItemNum07);
				writer.Write(LotItemNum08);
			}
		}
	}
}
