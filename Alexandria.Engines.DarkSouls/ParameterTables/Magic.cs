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
		/// <summary></summary>
		/// <remarks>
		/// Defined as "MAGIC_PARAM_ST" in Dark Souls in the file "MagicParam.paramdef" (id 1Bh).
		/// </remarks>
		public class Magic : ParameterTableRow {
			public const string TableName = "MAGIC_PARAM_ST";

			Int32 yesNoDialogMessageId, limitCancelSpEffectId;
			Int16 sortId, refId, mp, stamina, iconId, behaviorId, mtrlItemId, replaceMagicId, maxQuantity;
			Byte humanity, overDexterity, slotLength, requirementIntellect, requirementFaith, analogDexiterityMin, analogDexiterityMax;
			SByte sfxVariationId;
			MagicCategory ezStateBehaviorType;
			BehaviorRefType refCategory;
			BehaviorCategory spEffectCategory;
			MagicMotion refType;
			ItemUseMenu opmeMenuType;
			SpecialEffectType hasSpEffectType;
			ReplacementCategory replaceCategory;
			SpecialEffectUseLimitCategory useLimitCategory;
			Byte[] pad;

			public static readonly PropertyInfo
				YesNoDialogMessageIdProperty = GetProperty<Magic>("YesNoDialogMessageId"),
				LimitCancelSpEffectIdProperty = GetProperty<Magic>("LimitCancelSpEffectId"),
				SortIdProperty = GetProperty<Magic>("SortId"),
				RefIdProperty = GetProperty<Magic>("RefId"),
				MpProperty = GetProperty<Magic>("Mp"),
				StaminaProperty = GetProperty<Magic>("Stamina"),
				IconIdProperty = GetProperty<Magic>("IconId"),
				BehaviorIdProperty = GetProperty<Magic>("BehaviorId"),
				MtrlItemIdProperty = GetProperty<Magic>("MtrlItemId"),
				ReplaceMagicIdProperty = GetProperty<Magic>("ReplaceMagicId"),
				MaxQuantityProperty = GetProperty<Magic>("MaxQuantity"),
				HumanityProperty = GetProperty<Magic>("Humanity"),
				OverDexterityProperty = GetProperty<Magic>("OverDexterity"),
				SfxVariationIdProperty = GetProperty<Magic>("SfxVariationId"),
				SlotLengthProperty = GetProperty<Magic>("SlotLength"),
				RequirementIntellectProperty = GetProperty<Magic>("RequirementIntellect"),
				RequirementFaithProperty = GetProperty<Magic>("RequirementFaith"),
				AnalogDexiterityMinProperty = GetProperty<Magic>("AnalogDexiterityMin"),
				AnalogDexiterityMaxProperty = GetProperty<Magic>("AnalogDexiterityMax"),
				EzStateBehaviorTypeProperty = GetProperty<Magic>("EzStateBehaviorType"),
				RefCategoryProperty = GetProperty<Magic>("RefCategory"),
				SpEffectCategoryProperty = GetProperty<Magic>("SpEffectCategory"),
				RefTypeProperty = GetProperty<Magic>("RefType"),
				OpmeMenuTypeProperty = GetProperty<Magic>("OpmeMenuType"),
				HasSpEffectTypeProperty = GetProperty<Magic>("HasSpEffectType"),
				ReplaceCategoryProperty = GetProperty<Magic>("ReplaceCategory"),
				UseLimitCategoryProperty = GetProperty<Magic>("UseLimitCategory"),
				VowType0Property = GetProperty<Magic>("VowType0"),
				VowType1Property = GetProperty<Magic>("VowType1"),
				VowType2Property = GetProperty<Magic>("VowType2"),
				VowType3Property = GetProperty<Magic>("VowType3"),
				VowType4Property = GetProperty<Magic>("VowType4"),
				VowType5Property = GetProperty<Magic>("VowType5"),
				VowType6Property = GetProperty<Magic>("VowType6"),
				VowType7Property = GetProperty<Magic>("VowType7"),
				Enable_multiProperty = GetProperty<Magic>("Enable_multi"),
				Enable_multi_onlyProperty = GetProperty<Magic>("Enable_multi_only"),
				IsEnchantProperty = GetProperty<Magic>("IsEnchant"),
				IsShieldEnchantProperty = GetProperty<Magic>("IsShieldEnchant"),
				Enable_liveProperty = GetProperty<Magic>("Enable_live"),
				Enable_grayProperty = GetProperty<Magic>("Enable_gray"),
				Enable_whiteProperty = GetProperty<Magic>("Enable_white"),
				Enable_blackProperty = GetProperty<Magic>("Enable_black"),
				DisableOfflineProperty = GetProperty<Magic>("DisableOffline"),
				CastResonanceMagicProperty = GetProperty<Magic>("CastResonanceMagic"),
				Pad_1Property = GetProperty<Magic>("Pad_1"),
				VowType8Property = GetProperty<Magic>("VowType8"),
				VowType9Property = GetProperty<Magic>("VowType9"),
				VowType10Property = GetProperty<Magic>("VowType10"),
				VowType11Property = GetProperty<Magic>("VowType11"),
				VowType12Property = GetProperty<Magic>("VowType12"),
				VowType13Property = GetProperty<Magic>("VowType13"),
				VowType14Property = GetProperty<Magic>("VowType14"),
				VowType15Property = GetProperty<Magic>("VowType15"),
				PadProperty = GetProperty<Magic>("Pad");

			/// <summary>Yes / No dialog message ID</summary>
			/// <remarks>
			/// Japanese short name: "Yes/NoダイアログメッセージID", Google translated: "Yes / No dialog message ID".
			/// Japanese description: "魔法使用時に出すYes/NoダイアログのメッセージID", Google translated: "Message ID of the Yes / No dialog that put magic when using".
			/// </remarks>
			[ParameterTableRowAttribute("yesNoDialogMessageId", index: 0, minimum: -1, maximum: 1E+08, step: 1, order: 2500, unknown2: 1)]
			[DisplayName("Yes / No dialog message ID")]
			[Description("Message ID of the Yes / No dialog that put magic when using")]
			[DefaultValue((Int32)0)]
			public Int32 YesNoDialogMessageId {
				get { return yesNoDialogMessageId; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for YesNoDialogMessageId.");
					SetProperty(ref yesNoDialogMessageId, ref value, YesNoDialogMessageIdProperty);
				}
			}

			/// <summary>Special effects that ID out of the use restriction</summary>
			/// <remarks>
			/// Japanese short name: "使用制限から外れる特殊効果ID", Google translated: "Special effects that ID out of the use restriction".
			/// Japanese description: "指定した特殊効果IDが発動している時は使用制限を無視できる", Google translated: "Negligible use restriction when special effects specified ID is activated".
			/// </remarks>
			[ParameterTableRowAttribute("limitCancelSpEffectId", index: 1, minimum: -1, maximum: 1E+09, step: 1, order: 1020, unknown2: 1)]
			[DisplayName("Special effects that ID out of the use restriction")]
			[Description("Negligible use restriction when special effects specified ID is activated")]
			[DefaultValue((Int32)(-1))]
			public Int32 LimitCancelSpEffectId {
				get { return limitCancelSpEffectId; }
				set {
					if ((double)value < -1 || (double)value > 1E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+09 for LimitCancelSpEffectId.");
					SetProperty(ref limitCancelSpEffectId, ref value, LimitCancelSpEffectIdProperty);
				}
			}

			/// <summary>SortID</summary>
			/// <remarks>
			/// Japanese short name: "SortID", Google translated: "SortID".
			/// Japanese description: "ソートID(-1:集めない)", Google translated: "Sort ID (-1: it is not collected )".
			/// </remarks>
			[ParameterTableRowAttribute("sortId", index: 2, minimum: -1, maximum: 30000, step: 1, order: 1800, unknown2: 1)]
			[DisplayName("SortID")]
			[Description("Sort ID (-1: it is not collected )")]
			[DefaultValue((Int16)0)]
			public Int16 SortId {
				get { return sortId; }
				set {
					if ((double)value < -1 || (double)value > 30000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 30000 for SortId.");
					SetProperty(ref sortId, ref value, SortIdProperty);
				}
			}

			/// <summary>Call ID</summary>
			/// <remarks>
			/// Japanese short name: "呼び出しID", Google translated: "Call ID".
			/// Japanese description: "魔法から呼び出すID", Google translated: "ID calling from magic".
			/// </remarks>
			[ParameterTableRowAttribute("refId", index: 3, minimum: -1, maximum: 30000, step: 1, order: 500, unknown2: 1)]
			[DisplayName("Call ID")]
			[Description("ID calling from magic")]
			[DefaultValue((Int16)(-1))]
			public Int16 RefId {
				get { return refId; }
				set {
					if ((double)value < -1 || (double)value > 30000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 30000 for RefId.");
					SetProperty(ref refId, ref value, RefIdProperty);
				}
			}

			/// <summary>MP consumption</summary>
			/// <remarks>
			/// Japanese short name: "消費MP", Google translated: "MP consumption".
			/// Japanese description: "消費MP", Google translated: "MP consumption".
			/// </remarks>
			[ParameterTableRowAttribute("mp", index: 4, minimum: 0, maximum: 9999, step: 1, order: 600, unknown2: 1)]
			[DisplayName("MP consumption")]
			[Description("MP consumption")]
			[DefaultValue((Int16)0)]
			public Int16 Mp {
				get { return mp; }
				set {
					if ((double)value < 0 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9999 for Mp.");
					SetProperty(ref mp, ref value, MpProperty);
				}
			}

			/// <summary>Consumption stamina</summary>
			/// <remarks>
			/// Japanese short name: "消費スタミナ", Google translated: "Consumption stamina".
			/// Japanese description: "消費スタミナ", Google translated: "Consumption stamina".
			/// </remarks>
			[ParameterTableRowAttribute("stamina", index: 5, minimum: 0, maximum: 9999, step: 1, order: 700, unknown2: 1)]
			[DisplayName("Consumption stamina")]
			[Description("Consumption stamina")]
			[DefaultValue((Int16)0)]
			public Int16 Stamina {
				get { return stamina; }
				set {
					if ((double)value < 0 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9999 for Stamina.");
					SetProperty(ref stamina, ref value, StaminaProperty);
				}
			}

			/// <summary>Icon ID</summary>
			/// <remarks>
			/// Japanese short name: "アイコンID", Google translated: "Icon ID".
			/// Japanese description: "アイコンを指定　＞メニュー用", Google translated: "Specified > menu for the icon".
			/// </remarks>
			[ParameterTableRowAttribute("iconId", index: 6, minimum: -1, maximum: 30000, step: 1, order: 1300, unknown2: 1)]
			[DisplayName("Icon ID")]
			[Description("Specified > menu for the icon")]
			[DefaultValue((Int16)0)]
			public Int16 IconId {
				get { return iconId; }
				set {
					if ((double)value < -1 || (double)value > 30000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 30000 for IconId.");
					SetProperty(ref iconId, ref value, IconIdProperty);
				}
			}

			/// <summary>Action ID</summary>
			/// <remarks>
			/// Japanese short name: "行動ID", Google translated: "Action ID".
			/// Japanese description: "行動IDを設定する", Google translated: "I set the action ID".
			/// </remarks>
			[ParameterTableRowAttribute("behaviorId", index: 7, minimum: -1, maximum: 30000, step: 1, order: 1400, unknown2: 1)]
			[DisplayName("Action ID")]
			[Description("I set the action ID")]
			[DefaultValue((Int16)0)]
			public Int16 BehaviorId {
				get { return behaviorId; }
				set {
					if ((double)value < -1 || (double)value > 30000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 30000 for BehaviorId.");
					SetProperty(ref behaviorId, ref value, BehaviorIdProperty);
				}
			}

			/// <summary>Necessary item ID</summary>
			/// <remarks>
			/// Japanese short name: "必要アイテムID", Google translated: "Necessary item ID".
			/// Japanese description: "購入に必要なアイテムID", Google translated: "Item ID required to purchase".
			/// </remarks>
			[ParameterTableRowAttribute("mtrlItemId", index: 8, minimum: -1, maximum: 30000, step: 1, order: 2000, unknown2: 1)]
			[DisplayName("Necessary item ID")]
			[Description("Item ID required to purchase")]
			[DefaultValue((Int16)(-1))]
			public Int16 MtrlItemId {
				get { return mtrlItemId; }
				set {
					if ((double)value < -1 || (double)value > 30000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 30000 for MtrlItemId.");
					SetProperty(ref mtrlItemId, ref value, MtrlItemIdProperty);
				}
			}

			/// <summary>Magic ID to replace</summary>
			/// <remarks>
			/// Japanese short name: "差し替える魔法ID", Google translated: "Magic ID to replace".
			/// Japanese description: "状態変化一致時に差し替えるID(-1:無効)", Google translated: "ID to replace the change in the state matched ( -1 : disabled)".
			/// </remarks>
			[ParameterTableRowAttribute("replaceMagicId", index: 9, minimum: -1, maximum: 30000, step: 1, order: 3000, unknown2: 1)]
			[DisplayName("Magic ID to replace")]
			[Description("ID to replace the change in the state matched ( -1 : disabled)")]
			[DefaultValue((Int16)(-1))]
			public Int16 ReplaceMagicId {
				get { return replaceMagicId; }
				set {
					if ((double)value < -1 || (double)value > 30000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 30000 for ReplaceMagicId.");
					SetProperty(ref replaceMagicId, ref value, ReplaceMagicIdProperty);
				}
			}

			/// <summary>The maximum number</summary>
			/// <remarks>
			/// Japanese short name: "最大個数", Google translated: "The maximum number".
			/// Japanese description: "１個当たりの個数(-1:無限)", Google translated: "The number of per (-1 : infinite )".
			/// </remarks>
			[ParameterTableRowAttribute("maxQuantity", index: 10, minimum: -1, maximum: 9999, step: 1, order: 650, unknown2: 1)]
			[DisplayName("The maximum number")]
			[Description("The number of per (-1 : infinite )")]
			[DefaultValue((Int16)0)]
			public Int16 MaxQuantity {
				get { return maxQuantity; }
				set {
					if ((double)value < -1 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 9999 for MaxQuantity.");
					SetProperty(ref maxQuantity, ref value, MaxQuantityProperty);
				}
			}

			/// <summary>Consumption humanity</summary>
			/// <remarks>
			/// Japanese short name: "消費人間性", Google translated: "Consumption humanity".
			/// Japanese description: "消費人間性", Google translated: "Consumption humanity".
			/// </remarks>
			[ParameterTableRowAttribute("heroPoint", index: 11, minimum: 0, maximum: 255, step: 1, order: 710, unknown2: 1)]
			[DisplayName("Consumption humanity")]
			[Description("Consumption humanity")]
			[DefaultValue((Byte)0)]
			public Byte Humanity {
				get { return humanity; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for " + HumanityProperty.Name + ".");
					SetProperty(ref humanity, ref value, HumanityProperty);
				}
			}

			/// <summary>Workmanship over the starting value</summary>
			/// <remarks>
			/// Japanese short name: "技量オーバー開始値", Google translated: "Workmanship over the starting value".
			/// Japanese description: "技量オーバー開始値", Google translated: "Workmanship over the starting value".
			/// </remarks>
			[ParameterTableRowAttribute("overDexterity", index: 12, minimum: 0, maximum: 99, step: 1, order: 200, unknown2: 1)]
			[DisplayName("Workmanship over the starting value")]
			[Description("Workmanship over the starting value")]
			[DefaultValue((Byte)0)]
			public Byte OverDexterity {
				get { return overDexterity; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for OverDexterity.");
					SetProperty(ref overDexterity, ref value, OverDexterityProperty);
				}
			}

			/// <summary>SFX variation ID</summary>
			/// <remarks>
			/// Japanese short name: "SFXバリエーションID", Google translated: "SFX variation ID".
			/// Japanese description: "ＳＦＸのバリエーションを指定（TimeActEditorのＩＤと組み合わせて、ＳＦＸを特定するのに使用する）", Google translated: "( In conjunction with the ID of TimeActEditor, is used to identify the SFX) specifies the variation SFX".
			/// </remarks>
			[ParameterTableRowAttribute("sfxVariationId", index: 13, minimum: -1, maximum: 127, step: 1, order: 800, unknown2: 1)]
			[DisplayName("SFX variation ID")]
			[Description("( In conjunction with the ID of TimeActEditor, is used to identify the SFX) specifies the variation SFX")]
			[DefaultValue((SByte)(-1))]
			public SByte SfxVariationId {
				get { return sfxVariationId; }
				set {
					if ((double)value < -1 || (double)value > 127)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 127 for SfxVariationId.");
					SetProperty(ref sfxVariationId, ref value, SfxVariationIdProperty);
				}
			}

			/// <summary>Required slot</summary>
			/// <remarks>
			/// Japanese short name: "必要スロット", Google translated: "Required slot".
			/// Japanese description: "装備に必要なスロット数 ＞メニュー用", Google translated: "Number of slots > menu for required equipment".
			/// </remarks>
			[ParameterTableRowAttribute("slotLength", index: 14, minimum: 0, maximum: 3, step: 1, order: 1600, unknown2: 1)]
			[DisplayName("Required slot")]
			[Description("Number of slots > menu for required equipment")]
			[DefaultValue((Byte)0)]
			public Byte SlotLength {
				get { return slotLength; }
				set {
					if ((double)value < 0 || (double)value > 3)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 3 for SlotLength.");
					SetProperty(ref slotLength, ref value, SlotLengthProperty);
				}
			}

			/// <summary>Equipment conditions Intelligence</summary>
			/// <remarks>
			/// Japanese short name: "装備条件【知力】", Google translated: "Equipment conditions Intelligence".
			/// Japanese description: "PCの知力がこれ以上無いと装備できない", Google translated: "Can not be equipped with no more intelligence of the PC".
			/// </remarks>
			[ParameterTableRowAttribute("requirementIntellect", index: 15, minimum: 0, maximum: 99, step: 1, order: 1601, unknown2: 1)]
			[DisplayName("Equipment conditions Intelligence")]
			[Description("Can not be equipped with no more intelligence of the PC")]
			[DefaultValue((Byte)0)]
			public Byte RequirementIntellect {
				get { return requirementIntellect; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for RequirementIntellect.");
					SetProperty(ref requirementIntellect, ref value, RequirementIntellectProperty);
				}
			}

			/// <summary>Equipment conditions [ physical force ]</summary>
			/// <remarks>
			/// Japanese short name: "装備条件【理力】", Google translated: "Equipment conditions [ physical force ]".
			/// Japanese description: "PCの理力がこれ以上無いと装備できない", Google translated: "Can not be equipped with no more sense of power PC".
			/// </remarks>
			[ParameterTableRowAttribute("requirementFaith", index: 16, minimum: 0, maximum: 99, step: 1, order: 1602, unknown2: 1)]
			[DisplayName("Equipment conditions [ physical force ]")]
			[Description("Can not be equipped with no more sense of power PC")]
			[DefaultValue((Byte)0)]
			public Byte RequirementFaith {
				get { return requirementFaith; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for RequirementFaith.");
					SetProperty(ref requirementFaith, ref value, RequirementFaithProperty);
				}
			}

			/// <summary>Analog lowest skill</summary>
			/// <remarks>
			/// Japanese short name: "アナログ最低技量", Google translated: "Analog lowest skill".
			/// Japanese description: "モーションキャンセルアナログ化：最低技量値", Google translated: "Motion cancellation of analog : the lowest value workmanship".
			/// </remarks>
			[ParameterTableRowAttribute("analogDexiterityMin", index: 17, minimum: 0, maximum: 99, step: 1, order: 1005, unknown2: 1)]
			[DisplayName("Analog lowest skill")]
			[Description("Motion cancellation of analog : the lowest value workmanship")]
			[DefaultValue((Byte)0)]
			public Byte AnalogDexiterityMin {
				get { return analogDexiterityMin; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for AnalogDexiterityMin.");
					SetProperty(ref analogDexiterityMin, ref value, AnalogDexiterityMinProperty);
				}
			}

			/// <summary>Analog maximum workmanship</summary>
			/// <remarks>
			/// Japanese short name: "アナログ最大技量", Google translated: "Analog maximum workmanship".
			/// Japanese description: "モーションキャンセルアナログ化：最高技量値", Google translated: "Motion cancellation of analog : highest workmanship value".
			/// </remarks>
			[ParameterTableRowAttribute("analogDexiterityMax", index: 18, minimum: 0, maximum: 99, step: 1, order: 1006, unknown2: 1)]
			[DisplayName("Analog maximum workmanship")]
			[Description("Motion cancellation of analog : highest workmanship value")]
			[DefaultValue((Byte)0)]
			public Byte AnalogDexiterityMax {
				get { return analogDexiterityMax; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for AnalogDexiterityMax.");
					SetProperty(ref analogDexiterityMax, ref value, AnalogDexiterityMaxProperty);
				}
			}

			/// <summary>Category</summary>
			/// <remarks>
			/// Japanese short name: "カテゴリ", Google translated: "Category".
			/// Japanese description: "並べ替えに使用　＞メニュー用", Google translated: "> Using menu for sorting".
			/// </remarks>
			[ParameterTableRowAttribute("ezStateBehaviorType", index: 19, minimum: 0, maximum: 255, step: 1, order: 100, unknown2: 1)]
			[DisplayName("Category")]
			[Description("> Using menu for sorting")]
			[DefaultValue((MagicCategory)0)]
			public MagicCategory EzStateBehaviorType {
				get { return ezStateBehaviorType; }
				set { SetProperty(ref ezStateBehaviorType, ref value, EzStateBehaviorTypeProperty); }
			}

			/// <summary>ID category</summary>
			/// <remarks>
			/// Japanese short name: "IDカテゴリ", Google translated: "ID category".
			/// Japanese description: "↓のIDのカテゴリ[攻撃、飛び道具、特殊]", Google translated: "↓ category of ID [ attack , missile , special ]".
			/// </remarks>
			[ParameterTableRowAttribute("refCategory", index: 20, minimum: 0, maximum: 255, step: 1, order: 400, unknown2: 1)]
			[DisplayName("ID category")]
			[Description("↓ category of ID [ attack , missile , special ]")]
			[DefaultValue((BehaviorRefType)0)]
			public BehaviorRefType RefCategory {
				get { return refCategory; }
				set { SetProperty(ref refCategory, ref value, RefCategoryProperty); }
			}

			/// <summary>Special effects category</summary>
			/// <remarks>
			/// Japanese short name: "特殊効果カテゴリ", Google translated: "Special effects category".
			/// Japanese description: "スキルや、魔法、アイテムなどで、パラメータが変動する効果（エンチャントウェポンなど）があるので、│定した効果が、「武器攻撃のみをパワーアップする」といった効果に対応できるように行動ごとに設定するバリスタなど、設定の必要のないものは「なし」を設定する", Google translated: "Skills , magic , or item , because the effect of variation in parameters (such as Enchant Weapon ) , and sets the action for each effect it was │ boss is , to cope with effects such as' power up the only weapon attack " things such as varistor , without the need for setting is set to " None"".
			/// </remarks>
			[ParameterTableRowAttribute("spEffectCategory", index: 21, minimum: 0, maximum: 255, step: 1, order: 900, unknown2: 1)]
			[DisplayName("Special effects category")]
			[Description("Skills , magic , or item , because the effect of variation in parameters (such as Enchant Weapon ) , and sets the action for each effect it was │ boss is , to cope with effects such as' power up the only weapon attack \" things such as varistor , without the need for setting is set to \" None\"")]
			[DefaultValue((BehaviorCategory)0)]
			public BehaviorCategory SpEffectCategory {
				get { return spEffectCategory; }
				set { SetProperty(ref spEffectCategory, ref value, SpEffectCategoryProperty); }
			}

			/// <summary>Motion category</summary>
			/// <remarks>
			/// Japanese short name: "モーションカテゴリ", Google translated: "Motion category".
			/// Japanese description: "モーションを指定　＞EzState用", Google translated: "Specified > EzState for the motion".
			/// </remarks>
			[ParameterTableRowAttribute("refType", index: 22, minimum: 0, maximum: 255, step: 1, order: 1000, unknown2: 1)]
			[DisplayName("Motion category")]
			[Description("Specified > EzState for the motion")]
			[DefaultValue((MagicMotion)0)]
			public MagicMotion RefType {
				get { return refType; }
				set { SetProperty(ref refType, ref value, RefTypeProperty); }
			}

			/// <summary>Menu when using a</summary>
			/// <remarks>
			/// Japanese short name: "使用時メニュータイプ", Google translated: "Menu when using a".
			/// Japanese description: "魔法使用時に出すメニュータイプ", Google translated: "Menu type to put magic in use".
			/// </remarks>
			[ParameterTableRowAttribute("opmeMenuType", index: 23, minimum: 0, maximum: 255, step: 1, order: 2600, unknown2: 1)]
			[DisplayName("Menu when using a")]
			[Description("Menu type to put magic in use")]
			[DefaultValue((ItemUseMenu)0)]
			public ItemUseMenu OpmeMenuType {
				get { return opmeMenuType; }
				set { SetProperty(ref opmeMenuType, ref value, OpmeMenuTypeProperty); }
			}

			/// <summary>Or what normal ?</summary>
			/// <remarks>
			/// Japanese short name: "どの常態か？", Google translated: "Or what normal ?".
			/// Japanese description: "魔法IDを差し替える必要がある状態変化を指定", Google translated: "Specifies the state changes that need to replace the magic ID".
			/// </remarks>
			[ParameterTableRowAttribute("hasSpEffectType", index: 24, minimum: 0, maximum: 255, step: 1, order: 2800, unknown2: 1)]
			[DisplayName("Or what normal ?")]
			[Description("Specifies the state changes that need to replace the magic ID")]
			[DefaultValue((SpecialEffectType)0)]
			public SpecialEffectType HasSpEffectType {
				get { return hasSpEffectType; }
				set { SetProperty(ref hasSpEffectType, ref value, HasSpEffectTypeProperty); }
			}

			/// <summary>Replacement category</summary>
			/// <remarks>
			/// Japanese short name: "差し替えカテゴリ", Google translated: "Replacement category".
			/// Japanese description: "魔法IDを差し替える時の追加条件", Google translated: "Additional conditions when you replace the magic ID".
			/// </remarks>
			[ParameterTableRowAttribute("replaceCategory", index: 25, minimum: 0, maximum: 255, step: 1, order: 3100, unknown2: 1)]
			[DisplayName("Replacement category")]
			[Description("Additional conditions when you replace the magic ID")]
			[DefaultValue((ReplacementCategory)0)]
			public ReplacementCategory ReplaceCategory {
				get { return replaceCategory; }
				set { SetProperty(ref replaceCategory, ref value, ReplaceCategoryProperty); }
			}

			/// <summary>Use limited by special effects category</summary>
			/// <remarks>
			/// Japanese short name: "特殊効果カテゴリによる使用制限", Google translated: "Use limited by special effects category".
			/// Japanese description: "特殊効果によって使用可能かどうかを制御する為に指定", Google translated: "Specify in order to control whether available by special effects".
			/// </remarks>
			[ParameterTableRowAttribute("useLimitCategory", index: 26, minimum: 0, maximum: 255, step: 1, order: 1010, unknown2: 1)]
			[DisplayName("Use limited by special effects category")]
			[Description("Specify in order to control whether available by special effects")]
			[DefaultValue((SpecialEffectUseLimitCategory)0)]
			public SpecialEffectUseLimitCategory UseLimitCategory {
				get { return useLimitCategory; }
				set { SetProperty(ref useLimitCategory, ref value, UseLimitCategoryProperty); }
			}

			/// <summary>Pledge 0</summary>
			/// <remarks>
			/// Japanese short name: "誓約0", Google translated: "Pledge 0".
			/// Japanese description: "誓約0", Google translated: "Pledge 0".
			/// </remarks>
			[ParameterTableRowAttribute("vowType0:1", index: 27, minimum: 0, maximum: 1, step: 1, order: 3300, unknown2: 1)]
			[DisplayName("Pledge 0")]
			[Description("Pledge 0")]
			[DefaultValue(false)]
			public Boolean VowType0 {
				get { return GetBitProperty(0, 1, VowType0Property) != 0; }
				set { SetBitProperty(0, 1, value ? 1 : 0, VowType0Property); }
			}

			/// <summary>Pledge 1</summary>
			/// <remarks>
			/// Japanese short name: "誓約1", Google translated: "Pledge 1".
			/// Japanese description: "誓約1", Google translated: "Pledge 1".
			/// </remarks>
			[ParameterTableRowAttribute("vowType1:1", index: 28, minimum: 0, maximum: 1, step: 1, order: 3400, unknown2: 1)]
			[DisplayName("Pledge 1")]
			[Description("Pledge 1")]
			[DefaultValue(false)]
			public Boolean VowType1 {
				get { return GetBitProperty(1, 1, VowType1Property) != 0; }
				set { SetBitProperty(1, 1, value ? 1 : 0, VowType1Property); }
			}

			/// <summary>Pledge 2</summary>
			/// <remarks>
			/// Japanese short name: "誓約2", Google translated: "Pledge 2".
			/// Japanese description: "誓約2", Google translated: "Pledge 2".
			/// </remarks>
			[ParameterTableRowAttribute("vowType2:1", index: 29, minimum: 0, maximum: 1, step: 1, order: 3500, unknown2: 1)]
			[DisplayName("Pledge 2")]
			[Description("Pledge 2")]
			[DefaultValue(false)]
			public Boolean VowType2 {
				get { return GetBitProperty(2, 1, VowType2Property) != 0; }
				set { SetBitProperty(2, 1, value ? 1 : 0, VowType2Property); }
			}

			/// <summary>Pledge 3</summary>
			/// <remarks>
			/// Japanese short name: "誓約3", Google translated: "Pledge 3".
			/// Japanese description: "誓約3", Google translated: "Pledge 3".
			/// </remarks>
			[ParameterTableRowAttribute("vowType3:1", index: 30, minimum: 0, maximum: 1, step: 1, order: 3600, unknown2: 1)]
			[DisplayName("Pledge 3")]
			[Description("Pledge 3")]
			[DefaultValue(false)]
			public Boolean VowType3 {
				get { return GetBitProperty(3, 1, VowType3Property) != 0; }
				set { SetBitProperty(3, 1, value ? 1 : 0, VowType3Property); }
			}

			/// <summary>Pledge 4</summary>
			/// <remarks>
			/// Japanese short name: "誓約4", Google translated: "Pledge 4".
			/// Japanese description: "誓約4", Google translated: "Pledge 4".
			/// </remarks>
			[ParameterTableRowAttribute("vowType4:1", index: 31, minimum: 0, maximum: 1, step: 1, order: 3700, unknown2: 1)]
			[DisplayName("Pledge 4")]
			[Description("Pledge 4")]
			[DefaultValue(false)]
			public Boolean VowType4 {
				get { return GetBitProperty(4, 1, VowType4Property) != 0; }
				set { SetBitProperty(4, 1, value ? 1 : 0, VowType4Property); }
			}

			/// <summary>Pledge 5</summary>
			/// <remarks>
			/// Japanese short name: "誓約5", Google translated: "Pledge 5".
			/// Japanese description: "誓約5", Google translated: "Pledge 5".
			/// </remarks>
			[ParameterTableRowAttribute("vowType5:1", index: 32, minimum: 0, maximum: 1, step: 1, order: 3800, unknown2: 1)]
			[DisplayName("Pledge 5")]
			[Description("Pledge 5")]
			[DefaultValue(false)]
			public Boolean VowType5 {
				get { return GetBitProperty(5, 1, VowType5Property) != 0; }
				set { SetBitProperty(5, 1, value ? 1 : 0, VowType5Property); }
			}

			/// <summary>Pledge 6</summary>
			/// <remarks>
			/// Japanese short name: "誓約6", Google translated: "Pledge 6".
			/// Japanese description: "誓約6", Google translated: "Pledge 6".
			/// </remarks>
			[ParameterTableRowAttribute("vowType6:1", index: 33, minimum: 0, maximum: 1, step: 1, order: 3900, unknown2: 1)]
			[DisplayName("Pledge 6")]
			[Description("Pledge 6")]
			[DefaultValue(false)]
			public Boolean VowType6 {
				get { return GetBitProperty(6, 1, VowType6Property) != 0; }
				set { SetBitProperty(6, 1, value ? 1 : 0, VowType6Property); }
			}

			/// <summary>Pledge 7</summary>
			/// <remarks>
			/// Japanese short name: "誓約7", Google translated: "Pledge 7".
			/// Japanese description: "誓約7", Google translated: "Pledge 7".
			/// </remarks>
			[ParameterTableRowAttribute("vowType7:1", index: 34, minimum: 0, maximum: 1, step: 1, order: 4000, unknown2: 1)]
			[DisplayName("Pledge 7")]
			[Description("Pledge 7")]
			[DefaultValue(false)]
			public Boolean VowType7 {
				get { return GetBitProperty(7, 1, VowType7Property) != 0; }
				set { SetBitProperty(7, 1, value ? 1 : 0, VowType7Property); }
			}

			/// <summary>Either can be used in a multi-</summary>
			/// <remarks>
			/// Japanese short name: "マルチでも使用可能か", Google translated: "Either can be used in a multi-".
			/// Japanese description: "マルチでも使用できるか。シングル、マルチ両方で使える", Google translated: "Or can also be used in multi . I can use a single , multi- both".
			/// </remarks>
			[ParameterTableRowAttribute("enable_multi:1", index: 35, minimum: 0, maximum: 1, step: 1, order: 1100, unknown2: 1)]
			[DisplayName("Either can be used in a multi-")]
			[Description("Or can also be used in multi . I can use a single , multi- both")]
			[DefaultValue(false)]
			public Boolean Enable_multi {
				get { return GetBitProperty(8, 1, Enable_multiProperty) != 0; }
				set { SetBitProperty(8, 1, value ? 1 : 0, Enable_multiProperty); }
			}

			/// <summary>Or multi -only</summary>
			/// <remarks>
			/// Japanese short name: "マルチ専用か", Google translated: "Or multi -only".
			/// Japanese description: "マルチ専用か。シングルのときには使えない。マルチのときは使える。", Google translated: "Or multi -only. Can not be used in single . I use the case of multi .".
			/// </remarks>
			[ParameterTableRowAttribute("enable_multi_only:1", index: 36, minimum: 0, maximum: 1, step: 1, order: 1200, unknown2: 1)]
			[DisplayName("Or multi -only")]
			[Description("Or multi -only. Can not be used in single . I use the case of multi .")]
			[DefaultValue(false)]
			public Boolean Enable_multi_only {
				get { return GetBitProperty(9, 1, Enable_multi_onlyProperty) != 0; }
				set { SetBitProperty(9, 1, value ? 1 : 0, Enable_multi_onlyProperty); }
			}

			/// <summary>Enchantment</summary>
			/// <remarks>
			/// Japanese short name: "エンチャントか", Google translated: "Enchantment".
			/// Japanese description: "エンチャントする魔法か", Google translated: "Or magic to enchant".
			/// </remarks>
			[ParameterTableRowAttribute("isEnchant:1", index: 37, minimum: 0, maximum: 1, step: 1, order: 1700, unknown2: 1)]
			[DisplayName("Enchantment")]
			[Description("Or magic to enchant")]
			[DefaultValue(false)]
			public Boolean IsEnchant {
				get { return GetBitProperty(10, 1, IsEnchantProperty) != 0; }
				set { SetBitProperty(10, 1, value ? 1 : 0, IsEnchantProperty); }
			}

			/// <summary>Shield or enchanted</summary>
			/// <remarks>
			/// Japanese short name: "盾エンチャントか", Google translated: "Shield or enchanted".
			/// Japanese description: "ガード・盾エンチャントする魔法か", Google translated: "Or magic to guard shield enchantment".
			/// </remarks>
			[ParameterTableRowAttribute("isShieldEnchant:1", index: 38, minimum: 0, maximum: 1, step: 1, order: 1710, unknown2: 1)]
			[DisplayName("Shield or enchanted")]
			[Description("Or magic to guard shield enchantment")]
			[DefaultValue(false)]
			public Boolean IsShieldEnchant {
				get { return GetBitProperty(11, 1, IsShieldEnchantProperty) != 0; }
				set { SetBitProperty(11, 1, value ? 1 : 0, IsShieldEnchantProperty); }
			}

			/// <summary>Survival usable</summary>
			/// <remarks>
			/// Japanese short name: "生存使用可", Google translated: "Survival usable".
			/// Japanese description: "生存キャラが使用可能か", Google translated: "The survival character available?".
			/// </remarks>
			[ParameterTableRowAttribute("enable_live:1", index: 39, minimum: 0, maximum: 1, step: 1, order: 2100, unknown2: 1)]
			[DisplayName("Survival usable")]
			[Description("The survival character available?")]
			[DefaultValue(false)]
			public Boolean Enable_live {
				get { return GetBitProperty(12, 1, Enable_liveProperty) != 0; }
				set { SetBitProperty(12, 1, value ? 1 : 0, Enable_liveProperty); }
			}

			/// <summary>Gray usable</summary>
			/// <remarks>
			/// Japanese short name: "グレイ使用可", Google translated: "Gray usable".
			/// Japanese description: "グレイキャラが使用可能か", Google translated: "Grey character available?".
			/// </remarks>
			[ParameterTableRowAttribute("enable_gray:1", index: 40, minimum: 0, maximum: 1, step: 1, order: 2200, unknown2: 1)]
			[DisplayName("Gray usable")]
			[Description("Grey character available?")]
			[DefaultValue(false)]
			public Boolean Enable_gray {
				get { return GetBitProperty(13, 1, Enable_grayProperty) != 0; }
				set { SetBitProperty(13, 1, value ? 1 : 0, Enable_grayProperty); }
			}

			/// <summary>White usable</summary>
			/// <remarks>
			/// Japanese short name: "白使用可", Google translated: "White usable".
			/// Japanese description: "白ゴーストキャラが使用可能か", Google translated: "White ghost characters available?".
			/// </remarks>
			[ParameterTableRowAttribute("enable_white:1", index: 41, minimum: 0, maximum: 1, step: 1, order: 2300, unknown2: 1)]
			[DisplayName("White usable")]
			[Description("White ghost characters available?")]
			[DefaultValue(false)]
			public Boolean Enable_white {
				get { return GetBitProperty(14, 1, Enable_whiteProperty) != 0; }
				set { SetBitProperty(14, 1, value ? 1 : 0, Enable_whiteProperty); }
			}

			/// <summary>Black usable</summary>
			/// <remarks>
			/// Japanese short name: "黒使用可", Google translated: "Black usable".
			/// Japanese description: "黒ゴーストキャラが使用可能か", Google translated: "Black ghost characters available?".
			/// </remarks>
			[ParameterTableRowAttribute("enable_black:1", index: 42, minimum: 0, maximum: 1, step: 1, order: 2400, unknown2: 1)]
			[DisplayName("Black usable")]
			[Description("Black ghost characters available?")]
			[DefaultValue(false)]
			public Boolean Enable_black {
				get { return GetBitProperty(15, 1, Enable_blackProperty) != 0; }
				set { SetBitProperty(15, 1, value ? 1 : 0, Enable_blackProperty); }
			}

			/// <summary>Or disabled in the offline</summary>
			/// <remarks>
			/// Japanese short name: "オフラインで使用不可か", Google translated: "Or disabled in the offline".
			/// Japanese description: "オフラインで使用不可か", Google translated: "Or disabled in the offline".
			/// </remarks>
			[ParameterTableRowAttribute("disableOffline:1", index: 43, minimum: 0, maximum: 1, step: 1, order: 2700, unknown2: 1)]
			[DisplayName("Or disabled in the offline")]
			[Description("Or disabled in the offline")]
			[DefaultValue(false)]
			public Boolean DisableOffline {
				get { return GetBitProperty(16, 1, DisableOfflineProperty) != 0; }
				set { SetBitProperty(16, 1, value ? 1 : 0, DisableOfflineProperty); }
			}

			/// <summary>Or resonance magic delivery</summary>
			/// <remarks>
			/// Japanese short name: "共鳴魔法配信するか", Google translated: "Or resonance magic delivery".
			/// Japanese description: "共鳴魔法配信するか", Google translated: "Or resonance magic delivery".
			/// </remarks>
			[ParameterTableRowAttribute("castResonanceMagic:1", index: 44, minimum: 0, maximum: 1, step: 1, order: 3200, unknown2: 1)]
			[DisplayName("Or resonance magic delivery")]
			[Description("Or resonance magic delivery")]
			[DefaultValue(false)]
			public Boolean CastResonanceMagic {
				get { return GetBitProperty(17, 1, CastResonanceMagicProperty) != 0; }
				set { SetBitProperty(17, 1, value ? 1 : 0, CastResonanceMagicProperty); }
			}

			/// <summary>pading</summary>
			/// <remarks>
			/// Japanese short name: "pading", Google translated: "pading".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("pad_1:6", index: 45, minimum: 0, maximum: 1, step: 1, order: 4801, unknown2: 1)]
			[DisplayName("pading")]
			[Description("")]
			[DefaultValue((Byte)0)]
			[Browsable(false)]
			public Byte Pad_1 {
				get { return (Byte)GetBitProperty(18, 6, Pad_1Property); }
				set {
					if ((double)value < 0 || (double)value > 1)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1 for Pad_1.");
					SetBitProperty(18, 6, (int)value, Pad_1Property);
				}
			}

			/// <summary>Pledge 8</summary>
			/// <remarks>
			/// Japanese short name: "誓約8", Google translated: "Pledge 8".
			/// Japanese description: "誓約8", Google translated: "Pledge 8".
			/// </remarks>
			[ParameterTableRowAttribute("vowType8:1", index: 46, minimum: 0, maximum: 1, step: 1, order: 4100, unknown2: 1)]
			[DisplayName("Pledge 8")]
			[Description("Pledge 8")]
			[DefaultValue(false)]
			public Boolean VowType8 {
				get { return GetBitProperty(24, 1, VowType8Property) != 0; }
				set { SetBitProperty(24, 1, value ? 1 : 0, VowType8Property); }
			}

			/// <summary>Pledge 9</summary>
			/// <remarks>
			/// Japanese short name: "誓約9", Google translated: "Pledge 9".
			/// Japanese description: "誓約9", Google translated: "Pledge 9".
			/// </remarks>
			[ParameterTableRowAttribute("vowType9:1", index: 47, minimum: 0, maximum: 1, step: 1, order: 4200, unknown2: 1)]
			[DisplayName("Pledge 9")]
			[Description("Pledge 9")]
			[DefaultValue(false)]
			public Boolean VowType9 {
				get { return GetBitProperty(25, 1, VowType9Property) != 0; }
				set { SetBitProperty(25, 1, value ? 1 : 0, VowType9Property); }
			}

			/// <summary>Pledge 10</summary>
			/// <remarks>
			/// Japanese short name: "誓約10", Google translated: "Pledge 10".
			/// Japanese description: "誓約10", Google translated: "Pledge 10".
			/// </remarks>
			[ParameterTableRowAttribute("vowType10:1", index: 48, minimum: 0, maximum: 1, step: 1, order: 4300, unknown2: 1)]
			[DisplayName("Pledge 10")]
			[Description("Pledge 10")]
			[DefaultValue(false)]
			public Boolean VowType10 {
				get { return GetBitProperty(26, 1, VowType10Property) != 0; }
				set { SetBitProperty(26, 1, value ? 1 : 0, VowType10Property); }
			}

			/// <summary>Pledge 11</summary>
			/// <remarks>
			/// Japanese short name: "誓約11", Google translated: "Pledge 11".
			/// Japanese description: "誓約11", Google translated: "Pledge 11".
			/// </remarks>
			[ParameterTableRowAttribute("vowType11:1", index: 49, minimum: 0, maximum: 1, step: 1, order: 4400, unknown2: 1)]
			[DisplayName("Pledge 11")]
			[Description("Pledge 11")]
			[DefaultValue(false)]
			public Boolean VowType11 {
				get { return GetBitProperty(27, 1, VowType11Property) != 0; }
				set { SetBitProperty(27, 1, value ? 1 : 0, VowType11Property); }
			}

			/// <summary>Pledge 12</summary>
			/// <remarks>
			/// Japanese short name: "誓約12", Google translated: "Pledge 12".
			/// Japanese description: "誓約12", Google translated: "Pledge 12".
			/// </remarks>
			[ParameterTableRowAttribute("vowType12:1", index: 50, minimum: 0, maximum: 1, step: 1, order: 4500, unknown2: 1)]
			[DisplayName("Pledge 12")]
			[Description("Pledge 12")]
			[DefaultValue(false)]
			public Boolean VowType12 {
				get { return GetBitProperty(28, 1, VowType12Property) != 0; }
				set { SetBitProperty(28, 1, value ? 1 : 0, VowType12Property); }
			}

			/// <summary>Pledge 13</summary>
			/// <remarks>
			/// Japanese short name: "誓約13", Google translated: "Pledge 13".
			/// Japanese description: "誓約13", Google translated: "Pledge 13".
			/// </remarks>
			[ParameterTableRowAttribute("vowType13:1", index: 51, minimum: 0, maximum: 1, step: 1, order: 4600, unknown2: 1)]
			[DisplayName("Pledge 13")]
			[Description("Pledge 13")]
			[DefaultValue(false)]
			public Boolean VowType13 {
				get { return GetBitProperty(29, 1, VowType13Property) != 0; }
				set { SetBitProperty(29, 1, value ? 1 : 0, VowType13Property); }
			}

			/// <summary>Pledge 14</summary>
			/// <remarks>
			/// Japanese short name: "誓約14", Google translated: "Pledge 14".
			/// Japanese description: "誓約14", Google translated: "Pledge 14".
			/// </remarks>
			[ParameterTableRowAttribute("vowType14:1", index: 52, minimum: 0, maximum: 1, step: 1, order: 4700, unknown2: 1)]
			[DisplayName("Pledge 14")]
			[Description("Pledge 14")]
			[DefaultValue(false)]
			public Boolean VowType14 {
				get { return GetBitProperty(30, 1, VowType14Property) != 0; }
				set { SetBitProperty(30, 1, value ? 1 : 0, VowType14Property); }
			}

			/// <summary>Pledge 15</summary>
			/// <remarks>
			/// Japanese short name: "誓約15", Google translated: "Pledge 15".
			/// Japanese description: "誓約15", Google translated: "Pledge 15".
			/// </remarks>
			[ParameterTableRowAttribute("vowType15:1", index: 53, minimum: 0, maximum: 1, step: 1, order: 4800, unknown2: 1)]
			[DisplayName("Pledge 15")]
			[Description("Pledge 15")]
			[DefaultValue(false)]
			public Boolean VowType15 {
				get { return GetBitProperty(31, 1, VowType15Property) != 0; }
				set { SetBitProperty(31, 1, value ? 1 : 0, VowType15Property); }
			}

			/// <summary>pading</summary>
			/// <remarks>
			/// Japanese short name: "pading", Google translated: "pading".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("pad[2]", index: 54, minimum: 0, maximum: 1, step: 1, order: 4802, unknown2: 1)]
			[DisplayName("pading")]
			[Description("")]
			[Browsable(false)]
			public Byte[] Pad {
				get { return pad; }
				set { SetProperty(ref pad, ref value, PadProperty); }
			}

			internal Magic(ParameterTable table, int index, AssetLoader loader, int next)
				: base(table, index, loader) {
				BinaryReader reader = loader.Reader;

				YesNoDialogMessageId = reader.ReadInt32();
				LimitCancelSpEffectId = reader.ReadInt32();
				SortId = reader.ReadInt16();
				RefId = reader.ReadInt16();
				Mp = reader.ReadInt16();
				Stamina = reader.ReadInt16();
				IconId = reader.ReadInt16();
				BehaviorId = reader.ReadInt16();
				MtrlItemId = reader.ReadInt16();
				ReplaceMagicId = reader.ReadInt16();
				MaxQuantity = reader.ReadInt16();
				Humanity = reader.ReadByte();
				OverDexterity = reader.ReadByte();
				SfxVariationId = reader.ReadSByte();
				SlotLength = reader.ReadByte();
				RequirementIntellect = reader.ReadByte();
				RequirementFaith = reader.ReadByte();
				AnalogDexiterityMin = reader.ReadByte();
				AnalogDexiterityMax = reader.ReadByte();
				EzStateBehaviorType = (MagicCategory)reader.ReadByte();
				RefCategory = (BehaviorRefType)reader.ReadByte();
				SpEffectCategory = (BehaviorCategory)reader.ReadByte();
				RefType = (MagicMotion)reader.ReadByte();
				OpmeMenuType = (ItemUseMenu)reader.ReadByte();
				HasSpEffectType = (SpecialEffectType)reader.ReadByte();
				ReplaceCategory = (ReplacementCategory)reader.ReadByte();
				UseLimitCategory = (SpecialEffectUseLimitCategory)reader.ReadByte();
				BitFields = reader.ReadBytes(4);
				Pad = reader.ReadBytes(2);
			}

			internal Magic(ParameterTable table, int index)
				: base(table, index) {
				BitFields = new byte[4];
				YesNoDialogMessageId = (Int32)0;
				LimitCancelSpEffectId = (Int32)(-1);
				SortId = (Int16)0;
				RefId = (Int16)(-1);
				Mp = (Int16)0;
				Stamina = (Int16)0;
				IconId = (Int16)0;
				BehaviorId = (Int16)0;
				MtrlItemId = (Int16)(-1);
				ReplaceMagicId = (Int16)(-1);
				MaxQuantity = (Int16)0;
				Humanity = (Byte)0;
				OverDexterity = (Byte)0;
				SfxVariationId = (SByte)(-1);
				SlotLength = (Byte)0;
				RequirementIntellect = (Byte)0;
				RequirementFaith = (Byte)0;
				AnalogDexiterityMin = (Byte)0;
				AnalogDexiterityMax = (Byte)0;
				EzStateBehaviorType = (MagicCategory)0;
				RefCategory = (BehaviorRefType)0;
				SpEffectCategory = (BehaviorCategory)0;
				RefType = (MagicMotion)0;
				OpmeMenuType = (ItemUseMenu)0;
				HasSpEffectType = (SpecialEffectType)0;
				ReplaceCategory = (ReplacementCategory)0;
				UseLimitCategory = (SpecialEffectUseLimitCategory)0;
				VowType0 = false;
				VowType1 = false;
				VowType2 = false;
				VowType3 = false;
				VowType4 = false;
				VowType5 = false;
				VowType6 = false;
				VowType7 = false;
				Enable_multi = false;
				Enable_multi_only = false;
				IsEnchant = false;
				IsShieldEnchant = false;
				Enable_live = false;
				Enable_gray = false;
				Enable_white = false;
				Enable_black = false;
				DisableOffline = false;
				CastResonanceMagic = false;
				Pad_1 = (Byte)0;
				VowType8 = false;
				VowType9 = false;
				VowType10 = false;
				VowType11 = false;
				VowType12 = false;
				VowType13 = false;
				VowType14 = false;
				VowType15 = false;
				Pad = new Byte[2];
			}

			public override void Write(BinaryWriter writer) {
				writer.Write(YesNoDialogMessageId);
				writer.Write(LimitCancelSpEffectId);
				writer.Write(SortId);
				writer.Write(RefId);
				writer.Write(Mp);
				writer.Write(Stamina);
				writer.Write(IconId);
				writer.Write(BehaviorId);
				writer.Write(MtrlItemId);
				writer.Write(ReplaceMagicId);
				writer.Write(MaxQuantity);
				writer.Write(Humanity);
				writer.Write(OverDexterity);
				writer.Write(SfxVariationId);
				writer.Write(SlotLength);
				writer.Write(RequirementIntellect);
				writer.Write(RequirementFaith);
				writer.Write(AnalogDexiterityMin);
				writer.Write(AnalogDexiterityMax);
				writer.Write((Byte)EzStateBehaviorType);
				writer.Write((Byte)RefCategory);
				writer.Write((Byte)SpEffectCategory);
				writer.Write((Byte)RefType);
				writer.Write((Byte)OpmeMenuType);
				writer.Write((Byte)HasSpEffectType);
				writer.Write((Byte)ReplaceCategory);
				writer.Write((Byte)UseLimitCategory);
				writer.Write(BitFields);
				writer.Write(Pad);
			}
		}
	}
}
