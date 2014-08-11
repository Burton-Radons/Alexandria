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
		/// <summary>An item.</summary>
		/// <remarks>
		/// From "EquipParamGoods.paramdef" (id 0Fh)
		/// </remarks>
		[ParameterTableRowOrder("sortId", 7, 3600)]
		[ParameterTableRowOrder("weight", 2, 900)]
		[ParameterTableRowOrder("basicPrice", 3, 1200)]
		[ParameterTableRowOrder("sellValue", 4, 1210)]
		[ParameterTableRowOrder("isDeposit", 55, 3150)]
		[ParameterTableRowOrder("vagrantItemLotId", 66, 10000)]
		[ParameterTableRowOrder("vagrantBonusEneDropItemLotId", 67, 10000)]
		[ParameterTableRowOrder("vagrantItemEneDropItemLotId", 68, 10000)]
		public class Good : EquipmentTableRow {
			/// <summary>The name of the table in the file.</summary>
			public const string TableName = "EQUIP_PARAM_GOODS_ST";

			Int32 refId, sfxVariationId, behaviorId, replaceItemId, qwcId, yesNoDialogMessageId, magicId;
			UInt16 iconId, modelId;
			Int16 shopLevel, compTrophySedId, trophySeqId, maxStack;
			Byte consumeHumanity, overDexterity;
			ItemType itemType;
			BehaviorRefType refCategory;
			BehaviorCategory spEffectCategory;
			ItemCategory goodsCategory;
			ItemUseAnimation goodsUseAnim;
			ItemUseMenu useMenu;
			SpecialEffectUseLimitCategory useLimitCategory;
			ReplacementCategory replaceCategory;
			Byte[] pad;

			/// <summary>A property of the class.</summary>
			public static readonly PropertyInfo
				RefIdProperty = GetProperty<Good>("RefId"),
				SfxVariationIdProperty = GetProperty<Good>("SfxVariationId"),
				BehaviorIdProperty = GetProperty<Good>("BehaviorId"),
				ReplaceItemIdProperty = GetProperty<Good>("ReplaceItemId"),
				QwcIdProperty = GetProperty<Good>("QwcId"),
				YesNoDialogMessageIdProperty = GetProperty<Good>("YesNoDialogMessageId"),
				MagicIdProperty = GetProperty<Good>("MagicId"),
				IconIdProperty = GetProperty<Good>("IconId"),
				ModelIdProperty = GetProperty<Good>("ModelId"),
				ShopLevelProperty = GetProperty<Good>("ShopLevel"),
				CompTrophySedIdProperty = GetProperty<Good>("CompTrophySedId"),
				TrophySeqIdProperty = GetProperty<Good>("TrophySeqId"),
				MaxStackProperty = GetProperty<Good>("MaxStack"),
				ConsumeHumanityProperty = GetProperty<Good>("ConsumeHumanity"),
				OverDexterityProperty = GetProperty<Good>("OverDexterity"),
				ItemTypeProperty = GetProperty<Good>("ItemType"),
				RefCategoryProperty = GetProperty<Good>("RefCategory"),
				SpEffectCategoryProperty = GetProperty<Good>("SpEffectCategory"),
				GoodsCategoryProperty = GetProperty<Good>("GoodsCategory"),
				GoodsUseAnimProperty = GetProperty<Good>("GoodsUseAnim"),
				UseMenuProperty = GetProperty<Good>("UseMenu"),
				UseLimitCategoryProperty = GetProperty<Good>("UseLimitCategory"),
				ReplaceCategoryProperty = GetProperty<Good>("ReplaceCategory"),
				VowType0Property = GetProperty<Good>("VowType0"),
				VowType1Property = GetProperty<Good>("VowType1"),
				VowType2Property = GetProperty<Good>("VowType2"),
				VowType3Property = GetProperty<Good>("VowType3"),
				VowType4Property = GetProperty<Good>("VowType4"),
				VowType5Property = GetProperty<Good>("VowType5"),
				VowType6Property = GetProperty<Good>("VowType6"),
				VowType7Property = GetProperty<Good>("VowType7"),
				VowType8Property = GetProperty<Good>("VowType8"),
				VowType9Property = GetProperty<Good>("VowType9"),
				VowType10Property = GetProperty<Good>("VowType10"),
				VowType11Property = GetProperty<Good>("VowType11"),
				VowType12Property = GetProperty<Good>("VowType12"),
				VowType13Property = GetProperty<Good>("VowType13"),
				VowType14Property = GetProperty<Good>("VowType14"),
				VowType15Property = GetProperty<Good>("VowType15"),
				Enable_liveProperty = GetProperty<Good>("Enable_live"),
				Enable_grayProperty = GetProperty<Good>("Enable_gray"),
				Enable_whiteProperty = GetProperty<Good>("Enable_white"),
				Enable_blackProperty = GetProperty<Good>("Enable_black"),
				Enable_multiProperty = GetProperty<Good>("Enable_multi"),
				Disable_offlineProperty = GetProperty<Good>("Disable_offline"),
				IsEquipProperty = GetProperty<Good>("IsEquip"),
				IsConsumeProperty = GetProperty<Good>("IsConsume"),
				IsAutoEquipProperty = GetProperty<Good>("IsAutoEquip"),
				IsEstablishmentProperty = GetProperty<Good>("IsEstablishment"),
				IsOnlyOneProperty = GetProperty<Good>("IsOnlyOne"),
				IsDropProperty = GetProperty<Good>("IsDrop"),
				IsDisableHandProperty = GetProperty<Good>("IsDisableHand"),
				IsTravelItemProperty = GetProperty<Good>("IsTravelItem"),
				IsSuppleItemProperty = GetProperty<Good>("IsSuppleItem"),
				IsFullSuppleItemProperty = GetProperty<Good>("IsFullSuppleItem"),
				IsEnhanceProperty = GetProperty<Good>("IsEnhance"),
				IsFixItemProperty = GetProperty<Good>("IsFixItem"),
				DisableMultiDropShareProperty = GetProperty<Good>("DisableMultiDropShare"),
				DisableUseAtColiseumProperty = GetProperty<Good>("DisableUseAtColiseum"),
				DisableUseAtOutOfColiseumProperty = GetProperty<Good>("DisableUseAtOutOfColiseum"),
				PadProperty = GetProperty<Good>("Pad");

			/// <summary>Call ID</summary>
			/// <remarks>
			/// Japanese short name: "呼び出しID", Google translated: "Call ID".
			/// Japanese description: "アイテムから呼び出されるID", Google translated: "ID to be called from the item".
			/// </remarks>
			[ParameterTableRowAttribute("refId", index: 0, minimum: -1, maximum: 1E+08, step: 1, sortOrder: 400, unknown2: 1)]
			[DisplayName("Call ID")]
			[Description("ID to be called from the item")]
			[DefaultValue((Int32)(-1))]
			public Int32 RefId {
				get { return refId; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for RefId.");
					SetProperty(ref refId, ref value, RefIdProperty);
				}
			}

			/// <summary>SFX variation ID</summary>
			/// <remarks>
			/// Japanese short name: "SFXバリエーションID", Google translated: "SFX variation ID".
			/// Japanese description: "ＳＦＸのバリエーションを指定（TimeActEditorのＩＤと組み合わせて、ＳＦＸを特定するのに使用する）", Google translated: "( In conjunction with the ID of TimeActEditor, is used to identify the SFX) specifies the variation SFX".
			/// </remarks>
			[ParameterTableRowAttribute("sfxVariationId", index: 1, minimum: -1, maximum: 1E+08, step: 1, sortOrder: 500, unknown2: 1)]
			[DisplayName("SFX variation ID")]
			[Description("( In conjunction with the ID of TimeActEditor, is used to identify the SFX) specifies the variation SFX")]
			[DefaultValue((Int32)(-1))]
			public Int32 SfxVariationId {
				get { return sfxVariationId; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for SfxVariationId.");
					SetProperty(ref sfxVariationId, ref value, SfxVariationIdProperty);
				}
			}

			/// <summary>Action ID</summary>
			/// <remarks>
			/// Japanese short name: "行動ID", Google translated: "Action ID".
			/// Japanese description: "道具を使ったときに発生する効果を設定します", Google translated: "You can set the effect that occurs when you use the tool".
			/// </remarks>
			[ParameterTableRowAttribute("behaviorId", index: 5, minimum: -1, maximum: 1E+08, step: 1, sortOrder: 1600, unknown2: 1)]
			[DisplayName("Action ID")]
			[Description("You can set the effect that occurs when you use the tool")]
			[DefaultValue((Int32)0)]
			public Int32 BehaviorId {
				get { return behaviorId; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for BehaviorId.");
					SetProperty(ref behaviorId, ref value, BehaviorIdProperty);
				}
			}

			/// <summary>Replacement item ID</summary>
			/// <remarks>
			/// Japanese short name: "差し替えアイテムID", Google translated: "Replacement item ID".
			/// Japanese description: "差し替えるときのアイテムID", Google translated: "Item ID when you replace".
			/// </remarks>
			[ParameterTableRowAttribute("replaceItemId", index: 6, minimum: -1, maximum: 1E+09, step: 1, sortOrder: 3500, unknown2: 1)]
			[DisplayName("Replacement item ID")]
			[Description("Item ID when you replace")]
			[DefaultValue((Int32)(-1))]
			public Int32 ReplaceItemId {
				get { return replaceItemId; }
				set {
					if ((double)value < -1 || (double)value > 1E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+09 for ReplaceItemId.");
					SetProperty(ref replaceItemId, ref value, ReplaceItemIdProperty);
				}
			}

			/// <summary>QWCID (always -1 in Dark Souls).</summary>
			/// <remarks>
			/// Japanese short name: "QWCID", Google translated: "QWCID".
			/// Japanese description: "QWCのパラメタiD", Google translated: "ID parameters of QWC".
			/// </remarks>
			[ParameterTableRowAttribute("qwcId", index: 8, minimum: -1, maximum: 1E+08, step: 1, sortOrder: 3900, unknown2: 1)]
			[DisplayName("QWCID")]
			[Description("ID parameters of QWC")]
			[DefaultValue((Int32)(-1))]
			[Browsable(false)]
			public Int32 QwcId {
				get { return qwcId; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for QwcId.");
					SetProperty(ref qwcId, ref value, QwcIdProperty);
				}
			}

			/// <summary>YES / NO message ID</summary>
			/// <remarks>
			/// Japanese short name: "YES/NOメッセージID", Google translated: "YES / NO message ID".
			/// Japanese description: "YesNoダイアログ表示時に使用するメッセージID", Google translated: "Message ID that is used to display the time YesNo dialog".
			/// </remarks>
			[ParameterTableRowAttribute("yesNoDialogMessageId", index: 9, minimum: -1, maximum: 1E+08, step: 1, sortOrder: 4300, unknown2: 1)]
			[DisplayName("YES / NO message ID")]
			[Description("Message ID that is used to display the time YesNo dialog")]
			[DefaultValue((Int32)(-1))]
			public Int32 YesNoDialogMessageId {
				get { return yesNoDialogMessageId; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for YesNoDialogMessageId.");
					SetProperty(ref yesNoDialogMessageId, ref value, YesNoDialogMessageIdProperty);
				}
			}

			/// <summary>Magic ID</summary>
			/// <remarks>
			/// Japanese short name: "魔法ID", Google translated: "Magic ID".
			/// Japanese description: "巻物と紐づいた魔法ID", Google translated: "Magic ID that Zui string and scroll".
			/// </remarks>
			[ParameterTableRowAttribute("magicId", index: 10, minimum: -1, maximum: 1E+08, step: 1, sortOrder: 15000, unknown2: 1)]
			[DisplayName("Magic ID")]
			[Description("Magic ID that Zui string and scroll")]
			[DefaultValue((Int32)(-1))]
			public Int32 MagicId {
				get { return magicId; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for MagicId.");
					SetProperty(ref magicId, ref value, MagicIdProperty);
				}
			}

			/// <summary>Icon ID</summary>
			/// <remarks>
			/// Japanese short name: "アイコンID", Google translated: "Icon ID".
			/// Japanese description: "メニュー用アイコンID", Google translated: "Menu icon ID".
			/// </remarks>
			[ParameterTableRowAttribute("iconId", index: 11, minimum: 0, maximum: 9999, step: 1, sortOrder: 100, unknown2: 1)]
			[DisplayName("Icon ID")]
			[Description("Menu icon ID")]
			[DefaultValue((UInt16)0)]
			public UInt16 IconId {
				get { return iconId; }
				set {
					if ((double)value < 0 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9999 for IconId.");
					SetProperty(ref iconId, ref value, IconIdProperty);
				}
			}

			/// <summary>Model ID (Always 0 in Dark Souls)</summary>
			/// <remarks>
			/// Japanese short name: "モデルID", Google translated: "Model ID".
			/// Japanese description: "モデルID", Google translated: "Model ID".
			/// </remarks>
			[ParameterTableRowAttribute("modelId", index: 12, minimum: 0, maximum: 9999, step: 1, sortOrder: 1000, unknown2: 1)]
			[DisplayName("Model ID")]
			[Description("Model ID")]
			[DefaultValue((UInt16)0)]
			[Browsable(false)]
			public UInt16 ModelId {
				get { return modelId; }
				set {
					if ((double)value < 0 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9999 for ModelId.");
					SetProperty(ref modelId, ref value, ModelIdProperty);
				}
			}

			/// <summary>Shop level (always -1 in Dark Souls).</summary>
			/// <remarks>
			/// Japanese short name: "ショップレベル", Google translated: "Shop level".
			/// Japanese description: "お店で販売できるレベル", Google translated: "Level that can be sold in the shop".
			/// </remarks>
			[ParameterTableRowAttribute("shopLv", index: 13, minimum: -1, maximum: 9999, step: 1, sortOrder: 2800, unknown2: 1)]
			[DisplayName("Shop level")]
			[Description("Level that can be sold in the shop")]
			[DefaultValue((Int16)0)]
			[Browsable(false)]
			public Int16 ShopLevel {
				get { return shopLevel; }
				set {
					if ((double)value < -1 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 9999 for " + ShopLevelProperty.Name + ".");
					SetProperty(ref shopLevel, ref value, ShopLevelProperty);
				}
			}

			/// <summary>Comp trophy SEQ number</summary>
			/// <remarks>
			/// Japanese short name: "コンプトロフィーSEQ番号", Google translated: "Comp trophy SEQ number".
			/// Japanese description: "コンプリート系トロフィのSEQ番号", Google translated: "SEQ number of complete system trophy".
			/// </remarks>
			[ParameterTableRowAttribute("compTrophySedId", index: 14, minimum: -1, maximum: 99, step: 1, sortOrder: 3700, unknown2: 1)]
			[DisplayName("Comp trophy SEQ number")]
			[Description("SEQ number of complete system trophy")]
			[DefaultValue((Int16)(-1))]
			public Int16 CompTrophySedId {
				get { return compTrophySedId; }
				set {
					if ((double)value < -1 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 99 for CompTrophySedId.");
					SetProperty(ref compTrophySedId, ref value, CompTrophySedIdProperty);
				}
			}

			/// <summary>Trophy SEQ number</summary>
			/// <remarks>
			/// Japanese short name: "トロフィーSEQ番号", Google translated: "Trophy SEQ number".
			/// Japanese description: "トロフィーのSEQ番号", Google translated: "SEQ number of trophy".
			/// </remarks>
			[ParameterTableRowAttribute("trophySeqId", index: 15, minimum: -1, maximum: 99, step: 1, sortOrder: 3800, unknown2: 1)]
			[DisplayName("Trophy SEQ number")]
			[Description("SEQ number of trophy")]
			[DefaultValue((Int16)(-1))]
			public Int16 TrophySeqId {
				get { return trophySeqId; }
				set {
					if ((double)value < -1 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 99 for TrophySeqId.");
					SetProperty(ref trophySeqId, ref value, TrophySeqIdProperty);
				}
			}

			/// <summary>The maximum number of possession</summary>
			/// <remarks>
			/// Japanese short name: "最大所持数", Google translated: "The maximum number of possession".
			/// Japanese description: "最大所持数", Google translated: "The maximum number of possession".
			/// </remarks>
			[ParameterTableRowAttribute("maxNum", index: 16, minimum: 0, maximum: 9999, step: 1, sortOrder: 610, unknown2: 1)]
			[DisplayName("The maximum number of possession")]
			[Description("The maximum number of possession")]
			[DefaultValue((Int16)0)]
			public Int16 MaxStack {
				get { return maxStack; }
				set {
					if ((double)value < 0 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9999 for " + MaxStackProperty.Name + ".");
					SetProperty(ref maxStack, ref value, MaxStackProperty);
				}
			}

			/// <summary>Consumption humanity (always 0 in Dark Souls).</summary>
			/// <remarks>
			/// Japanese short name: "消費人間性", Google translated: "Consumption humanity".
			/// Japanese description: "消費人間性", Google translated: "Consumption humanity".
			/// </remarks>
			[ParameterTableRowAttribute("consumeHeroPoint", index: 17, minimum: 0, maximum: 255, step: 1, sortOrder: 611, unknown2: 1)]
			[DisplayName("Consumption humanity")]
			[Description("Consumption humanity")]
			[DefaultValue((Byte)0)]
			[Browsable(false)]
			public Byte ConsumeHumanity {
				get { return consumeHumanity; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for " + ConsumeHumanityProperty.Name + ".");
					SetProperty(ref consumeHumanity, ref value, ConsumeHumanityProperty);
				}
			}

			/// <summary>Workmanship over the starting value</summary>
			/// <remarks>
			/// Japanese short name: "技量オーバー開始値", Google translated: "Workmanship over the starting value".
			/// Japanese description: "技量オーバー開始値", Google translated: "Workmanship over the starting value".
			/// </remarks>
			[ParameterTableRowAttribute("overDexterity", index: 18, minimum: 0, maximum: 99, step: 1, sortOrder: 700, unknown2: 1)]
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

			/// <summary>Type of tool</summary>
			/// <remarks>
			/// Japanese short name: "道具のタイプ", Google translated: "Type of tool".
			/// Japanese description: "道具の種類", Google translated: "Type of tool".
			/// </remarks>
			[ParameterTableRowAttribute("goodsType", index: 19, minimum: 0, maximum: 99, step: 1, sortOrder: 200, unknown2: 1)]
			[DisplayName("Type of tool")]
			[Description("Type of tool")]
			[DefaultValue((ItemType)0)]
			public ItemType ItemType {
				get { return itemType; }
				set { SetProperty(ref itemType, ref value, ItemTypeProperty); }
			}

			/// <summary>ID category</summary>
			/// <remarks>
			/// Japanese short name: "IDカテゴリ", Google translated: "ID category".
			/// Japanese description: "↓のIDのカテゴリ[攻撃、飛び道具、特殊]", Google translated: "↓ category of ID [ attack , missile , special ]".
			/// </remarks>
			[ParameterTableRowAttribute("refCategory", index: 20, minimum: 0, maximum: 255, step: 1, sortOrder: 300, unknown2: 1)]
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
			[ParameterTableRowAttribute("spEffectCategory", index: 21, minimum: 0, maximum: 255, step: 1, sortOrder: 600, unknown2: 1)]
			[DisplayName("Special effects category")]
			[Description("Skills , magic , or item , because the effect of variation in parameters (such as Enchant Weapon ) , and sets the action for each effect it was │ boss is , to cope with effects such as' power up the only weapon attack \" things such as varistor , without the need for setting is set to \" None\"")]
			[DefaultValue((BehaviorCategory)0)]
			public BehaviorCategory SpEffectCategory {
				get { return spEffectCategory; }
				set { SetProperty(ref spEffectCategory, ref value, SpEffectCategoryProperty); }
			}

			/// <summary>Tool category</summary>
			/// <remarks>
			/// Japanese short name: "道具カテゴリ", Google translated: "Tool category".
			/// Japanese description: "道具カテゴリ", Google translated: "Tool category".
			/// </remarks>
			[ParameterTableRowAttribute("goodsCategory", index: 22, minimum: 0, maximum: 99, step: 1, sortOrder: 1300, unknown2: 1)]
			[DisplayName("Tool category")]
			[Description("Tool category")]
			[DefaultValue((ItemCategory)0)]
			public ItemCategory GoodsCategory {
				get { return goodsCategory; }
				set { SetProperty(ref goodsCategory, ref value, GoodsCategoryProperty); }
			}

			/// <summary>Animation tool use when</summary>
			/// <remarks>
			/// Japanese short name: "道具使用時アニメ", Google translated: "Animation tool use when".
			/// Japanese description: "道具を使ったときに再生するアニメを設定します", Google translated: "You can set the animation that plays when you use the tool".
			/// </remarks>
			[ParameterTableRowAttribute("goodsUseAnim", index: 23, minimum: 0, maximum: 99, step: 1, sortOrder: 1500, unknown2: 1)]
			[DisplayName("Animation tool use when")]
			[Description("You can set the animation that plays when you use the tool")]
			[DefaultValue((ItemUseAnimation)0)]
			public ItemUseAnimation GoodsUseAnim {
				get { return goodsUseAnim; }
				set { SetProperty(ref goodsUseAnim, ref value, GoodsUseAnimProperty); }
			}

			/// <summary>Open or menu</summary>
			/// <remarks>
			/// Japanese short name: "メニュー開くか", Google translated: "Open or menu".
			/// Japanese description: "アイテム使用時に開くメニュータイプ", Google translated: "Menu type you want to open an item when using".
			/// </remarks>
			[ParameterTableRowAttribute("opmeMenuType", index: 24, minimum: 0, maximum: 255, step: 1, sortOrder: 4100, unknown2: 1)]
			[DisplayName("Open or menu")]
			[Description("Menu type you want to open an item when using")]
			[DefaultValue((ItemUseMenu)0)]
			public ItemUseMenu UseMenu {
				get { return useMenu; }
				set { SetProperty(ref useMenu, ref value, UseMenuProperty); }
			}

			/// <summary>Use limited by special effects category</summary>
			/// <remarks>
			/// Japanese short name: "特殊効果カテゴリによる使用制限", Google translated: "Use limited by special effects category".
			/// Japanese description: "かかっている特殊効果によって使用可能かを制御する為に指定", Google translated: "The specify to control the availability of the special effects are applied".
			/// </remarks>
			[ParameterTableRowAttribute("useLimitCategory", index: 25, minimum: 0, maximum: 255, step: 1, sortOrder: 1610, unknown2: 1)]
			[DisplayName("Use limited by special effects category")]
			[Description("The specify to control the availability of the special effects are applied")]
			[DefaultValue((SpecialEffectUseLimitCategory)0)]
			public SpecialEffectUseLimitCategory UseLimitCategory {
				get { return useLimitCategory; }
				set { SetProperty(ref useLimitCategory, ref value, UseLimitCategoryProperty); }
			}

			/// <summary>Replacement category</summary>
			/// <remarks>
			/// Japanese short name: "差し替えカテゴリ", Google translated: "Replacement category".
			/// Japanese description: "呼び出しIDに加算しる条件カテゴリ", Google translated: "In addition sill condition category to call ID".
			/// </remarks>
			[ParameterTableRowAttribute("replaceCategory", index: 26, minimum: 0, maximum: 255, step: 1, sortOrder: 15001, unknown2: 1)]
			[DisplayName("Replacement category")]
			[Description("In addition sill condition category to call ID")]
			[DefaultValue((ReplacementCategory)0)]
			public ReplacementCategory ReplaceCategory {
				get { return replaceCategory; }
				set { SetProperty(ref replaceCategory, ref value, ReplaceCategoryProperty); }
			}

			/// <summary>Pledge 0</summary>
			/// <remarks>
			/// Japanese short name: "誓約0", Google translated: "Pledge 0".
			/// Japanese description: "誓約0で使用可能か", Google translated: "Compatible with the available commitment 0".
			/// </remarks>
			[ParameterTableRowAttribute("vowType0:1", index: 27, minimum: 0, maximum: 1, step: 1, sortOrder: 20001, unknown2: 1)]
			[DisplayName("Pledge 0")]
			[Description("Compatible with the available commitment 0")]
			[DefaultValue(false)]
			public Boolean VowType0 {
				get { return GetBitProperty(0, 1, VowType0Property) != 0; }
				set { SetBitProperty(0, 1, value ? 1 : 0, VowType0Property); }
			}

			/// <summary>Pledge 1</summary>
			/// <remarks>
			/// Japanese short name: "誓約1", Google translated: "Pledge 1".
			/// Japanese description: "誓約1で使用可能か", Google translated: "Or can be used in one pledge".
			/// </remarks>
			[ParameterTableRowAttribute("vowType1:1", index: 28, minimum: 0, maximum: 1, step: 1, sortOrder: 20002, unknown2: 1)]
			[DisplayName("Pledge 1")]
			[Description("Or can be used in one pledge")]
			[DefaultValue(false)]
			public Boolean VowType1 {
				get { return GetBitProperty(1, 1, VowType1Property) != 0; }
				set { SetBitProperty(1, 1, value ? 1 : 0, VowType1Property); }
			}

			/// <summary>Pledge 2</summary>
			/// <remarks>
			/// Japanese short name: "誓約2", Google translated: "Pledge 2".
			/// Japanese description: "誓約2で使用可能か", Google translated: "Or available on the pledge 2".
			/// </remarks>
			[ParameterTableRowAttribute("vowType2:1", index: 29, minimum: 0, maximum: 1, step: 1, sortOrder: 20003, unknown2: 1)]
			[DisplayName("Pledge 2")]
			[Description("Or available on the pledge 2")]
			[DefaultValue(false)]
			public Boolean VowType2 {
				get { return GetBitProperty(2, 1, VowType2Property) != 0; }
				set { SetBitProperty(2, 1, value ? 1 : 0, VowType2Property); }
			}

			/// <summary>Pledge 3</summary>
			/// <remarks>
			/// Japanese short name: "誓約3", Google translated: "Pledge 3".
			/// Japanese description: "誓約3で使用可能か", Google translated: "Or available on the pledge 3".
			/// </remarks>
			[ParameterTableRowAttribute("vowType3:1", index: 30, minimum: 0, maximum: 1, step: 1, sortOrder: 20004, unknown2: 1)]
			[DisplayName("Pledge 3")]
			[Description("Or available on the pledge 3")]
			[DefaultValue(false)]
			public Boolean VowType3 {
				get { return GetBitProperty(3, 1, VowType3Property) != 0; }
				set { SetBitProperty(3, 1, value ? 1 : 0, VowType3Property); }
			}

			/// <summary>Pledge 4</summary>
			/// <remarks>
			/// Japanese short name: "誓約4", Google translated: "Pledge 4".
			/// Japanese description: "誓約4で使用可能か", Google translated: "Or available on the pledge 4".
			/// </remarks>
			[ParameterTableRowAttribute("vowType4:1", index: 31, minimum: 0, maximum: 1, step: 1, sortOrder: 20005, unknown2: 1)]
			[DisplayName("Pledge 4")]
			[Description("Or available on the pledge 4")]
			[DefaultValue(false)]
			public Boolean VowType4 {
				get { return GetBitProperty(4, 1, VowType4Property) != 0; }
				set { SetBitProperty(4, 1, value ? 1 : 0, VowType4Property); }
			}

			/// <summary>Pledge 5</summary>
			/// <remarks>
			/// Japanese short name: "誓約5", Google translated: "Pledge 5".
			/// Japanese description: "誓約5で使用可能か", Google translated: "Compatible with the available commitment 5".
			/// </remarks>
			[ParameterTableRowAttribute("vowType5:1", index: 32, minimum: 0, maximum: 1, step: 1, sortOrder: 20006, unknown2: 1)]
			[DisplayName("Pledge 5")]
			[Description("Compatible with the available commitment 5")]
			[DefaultValue(false)]
			public Boolean VowType5 {
				get { return GetBitProperty(5, 1, VowType5Property) != 0; }
				set { SetBitProperty(5, 1, value ? 1 : 0, VowType5Property); }
			}

			/// <summary>Pledge 6</summary>
			/// <remarks>
			/// Japanese short name: "誓約6", Google translated: "Pledge 6".
			/// Japanese description: "誓約6で使用可能か", Google translated: "Compatible with the available six pledge".
			/// </remarks>
			[ParameterTableRowAttribute("vowType6:1", index: 33, minimum: 0, maximum: 1, step: 1, sortOrder: 20007, unknown2: 1)]
			[DisplayName("Pledge 6")]
			[Description("Compatible with the available six pledge")]
			[DefaultValue(false)]
			public Boolean VowType6 {
				get { return GetBitProperty(6, 1, VowType6Property) != 0; }
				set { SetBitProperty(6, 1, value ? 1 : 0, VowType6Property); }
			}

			/// <summary>Pledge 7</summary>
			/// <remarks>
			/// Japanese short name: "誓約7", Google translated: "Pledge 7".
			/// Japanese description: "誓約7で使用可能か", Google translated: "Or available on the pledge 7".
			/// </remarks>
			[ParameterTableRowAttribute("vowType7:1", index: 34, minimum: 0, maximum: 1, step: 1, sortOrder: 20008, unknown2: 1)]
			[DisplayName("Pledge 7")]
			[Description("Or available on the pledge 7")]
			[DefaultValue(false)]
			public Boolean VowType7 {
				get { return GetBitProperty(7, 1, VowType7Property) != 0; }
				set { SetBitProperty(7, 1, value ? 1 : 0, VowType7Property); }
			}

			/// <summary>Pledge 8</summary>
			/// <remarks>
			/// Japanese short name: "誓約8", Google translated: "Pledge 8".
			/// Japanese description: "誓約8で使用可能か", Google translated: "Or available on the pledge 8".
			/// </remarks>
			[ParameterTableRowAttribute("vowType8:1", index: 35, minimum: 0, maximum: 1, step: 1, sortOrder: 20009, unknown2: 1)]
			[DisplayName("Pledge 8")]
			[Description("Or available on the pledge 8")]
			[DefaultValue(false)]
			public Boolean VowType8 {
				get { return GetBitProperty(8, 1, VowType8Property) != 0; }
				set { SetBitProperty(8, 1, value ? 1 : 0, VowType8Property); }
			}

			/// <summary>Pledge 9</summary>
			/// <remarks>
			/// Japanese short name: "誓約9", Google translated: "Pledge 9".
			/// Japanese description: "誓約9で使用可能か", Google translated: "Compatible with the available commitment 9".
			/// </remarks>
			[ParameterTableRowAttribute("vowType9:1", index: 36, minimum: 0, maximum: 1, step: 1, sortOrder: 20010, unknown2: 1)]
			[DisplayName("Pledge 9")]
			[Description("Compatible with the available commitment 9")]
			[DefaultValue(false)]
			public Boolean VowType9 {
				get { return GetBitProperty(9, 1, VowType9Property) != 0; }
				set { SetBitProperty(9, 1, value ? 1 : 0, VowType9Property); }
			}

			/// <summary>Pledge 10</summary>
			/// <remarks>
			/// Japanese short name: "誓約10", Google translated: "Pledge 10".
			/// Japanese description: "誓約10で使用可能か", Google translated: "Or available in 10 pledge".
			/// </remarks>
			[ParameterTableRowAttribute("vowType10:1", index: 37, minimum: 0, maximum: 1, step: 1, sortOrder: 20011, unknown2: 1)]
			[DisplayName("Pledge 10")]
			[Description("Or available in 10 pledge")]
			[DefaultValue(false)]
			public Boolean VowType10 {
				get { return GetBitProperty(10, 1, VowType10Property) != 0; }
				set { SetBitProperty(10, 1, value ? 1 : 0, VowType10Property); }
			}

			/// <summary>Pledge 11</summary>
			/// <remarks>
			/// Japanese short name: "誓約11", Google translated: "Pledge 11".
			/// Japanese description: "誓約11で使用可能か", Google translated: "Or available in 11 pledge".
			/// </remarks>
			[ParameterTableRowAttribute("vowType11:1", index: 38, minimum: 0, maximum: 1, step: 1, sortOrder: 20012, unknown2: 1)]
			[DisplayName("Pledge 11")]
			[Description("Or available in 11 pledge")]
			[DefaultValue(false)]
			public Boolean VowType11 {
				get { return GetBitProperty(11, 1, VowType11Property) != 0; }
				set { SetBitProperty(11, 1, value ? 1 : 0, VowType11Property); }
			}

			/// <summary>Pledge 12</summary>
			/// <remarks>
			/// Japanese short name: "誓約12", Google translated: "Pledge 12".
			/// Japanese description: "誓約12で使用可能か", Google translated: "Or available in 12 pledge".
			/// </remarks>
			[ParameterTableRowAttribute("vowType12:1", index: 39, minimum: 0, maximum: 1, step: 1, sortOrder: 20013, unknown2: 1)]
			[DisplayName("Pledge 12")]
			[Description("Or available in 12 pledge")]
			[DefaultValue(false)]
			public Boolean VowType12 {
				get { return GetBitProperty(12, 1, VowType12Property) != 0; }
				set { SetBitProperty(12, 1, value ? 1 : 0, VowType12Property); }
			}

			/// <summary>Pledge 13</summary>
			/// <remarks>
			/// Japanese short name: "誓約13", Google translated: "Pledge 13".
			/// Japanese description: "誓約13で使用可能か", Google translated: "Or available in 13 pledge".
			/// </remarks>
			[ParameterTableRowAttribute("vowType13:1", index: 40, minimum: 0, maximum: 1, step: 1, sortOrder: 20014, unknown2: 1)]
			[DisplayName("Pledge 13")]
			[Description("Or available in 13 pledge")]
			[DefaultValue(false)]
			public Boolean VowType13 {
				get { return GetBitProperty(13, 1, VowType13Property) != 0; }
				set { SetBitProperty(13, 1, value ? 1 : 0, VowType13Property); }
			}

			/// <summary>Pledge 14</summary>
			/// <remarks>
			/// Japanese short name: "誓約14", Google translated: "Pledge 14".
			/// Japanese description: "誓約14で使用可能か", Google translated: "Or available in 14 pledge".
			/// </remarks>
			[ParameterTableRowAttribute("vowType14:1", index: 41, minimum: 0, maximum: 1, step: 1, sortOrder: 20015, unknown2: 1)]
			[DisplayName("Pledge 14")]
			[Description("Or available in 14 pledge")]
			[DefaultValue(false)]
			public Boolean VowType14 {
				get { return GetBitProperty(14, 1, VowType14Property) != 0; }
				set { SetBitProperty(14, 1, value ? 1 : 0, VowType14Property); }
			}

			/// <summary>Pledge 15</summary>
			/// <remarks>
			/// Japanese short name: "誓約15", Google translated: "Pledge 15".
			/// Japanese description: "誓約15で使用可能か", Google translated: "Or available in 15 pledge".
			/// </remarks>
			[ParameterTableRowAttribute("vowType15:1", index: 42, minimum: 0, maximum: 1, step: 1, sortOrder: 20016, unknown2: 1)]
			[DisplayName("Pledge 15")]
			[Description("Or available in 15 pledge")]
			[DefaultValue(false)]
			public Boolean VowType15 {
				get { return GetBitProperty(15, 1, VowType15Property) != 0; }
				set { SetBitProperty(15, 1, value ? 1 : 0, VowType15Property); }
			}

			/// <summary>Survival usable</summary>
			/// <remarks>
			/// Japanese short name: "生存使用可", Google translated: "Survival usable".
			/// Japanese description: "生存プレイヤー使用可能か", Google translated: "Or survival player available".
			/// </remarks>
			[ParameterTableRowAttribute("enable_live:1", index: 43, minimum: 0, maximum: 1, step: 1, sortOrder: 1700, unknown2: 1)]
			[DisplayName("Survival usable")]
			[Description("Or survival player available")]
			[DefaultValue(false)]
			public Boolean Enable_live {
				get { return GetBitProperty(16, 1, Enable_liveProperty) != 0; }
				set { SetBitProperty(16, 1, value ? 1 : 0, Enable_liveProperty); }
			}

			/// <summary>Gray usable</summary>
			/// <remarks>
			/// Japanese short name: "グレイ使用可", Google translated: "Gray usable".
			/// Japanese description: "グレイゴースト使用可能か", Google translated: "Or gray ghost available".
			/// </remarks>
			[ParameterTableRowAttribute("enable_gray:1", index: 44, minimum: 0, maximum: 1, step: 1, sortOrder: 1800, unknown2: 1)]
			[DisplayName("Gray usable")]
			[Description("Or gray ghost available")]
			[DefaultValue(false)]
			public Boolean Enable_gray {
				get { return GetBitProperty(17, 1, Enable_grayProperty) != 0; }
				set { SetBitProperty(17, 1, value ? 1 : 0, Enable_grayProperty); }
			}

			/// <summary>White usable</summary>
			/// <remarks>
			/// Japanese short name: "白使用可", Google translated: "White usable".
			/// Japanese description: "ホワイトゴースト使用可能か", Google translated: "White or ghost available".
			/// </remarks>
			[ParameterTableRowAttribute("enable_white:1", index: 45, minimum: 0, maximum: 1, step: 1, sortOrder: 1900, unknown2: 1)]
			[DisplayName("White usable")]
			[Description("White or ghost available")]
			[DefaultValue(false)]
			public Boolean Enable_white {
				get { return GetBitProperty(18, 1, Enable_whiteProperty) != 0; }
				set { SetBitProperty(18, 1, value ? 1 : 0, Enable_whiteProperty); }
			}

			/// <summary>Black usable</summary>
			/// <remarks>
			/// Japanese short name: "黒使用可", Google translated: "Black usable".
			/// Japanese description: "ブラックゴーストしよう可能か", Google translated: "Is it possible you are trying to black ghost".
			/// </remarks>
			[ParameterTableRowAttribute("enable_black:1", index: 46, minimum: 0, maximum: 1, step: 1, sortOrder: 2000, unknown2: 1)]
			[DisplayName("Black usable")]
			[Description("Is it possible you are trying to black ghost")]
			[DefaultValue(false)]
			public Boolean Enable_black {
				get { return GetBitProperty(19, 1, Enable_blackProperty) != 0; }
				set { SetBitProperty(19, 1, value ? 1 : 0, Enable_blackProperty); }
			}

			/// <summary>Multiplayer Allowed</summary>
			/// <remarks>
			/// Japanese short name: "マルチプレイ可", Google translated: "Multiplayer Allowed".
			/// Japanese description: "マルチプレイ中に使用可能か？", Google translated: "Or available in multiplayer ?".
			/// </remarks>
			[ParameterTableRowAttribute("enable_multi:1", index: 47, minimum: 0, maximum: 1, step: 1, sortOrder: 2100, unknown2: 1)]
			[DisplayName("Multiplayer Allowed")]
			[Description("Or available in multiplayer ?")]
			[DefaultValue(false)]
			public Boolean Enable_multi {
				get { return GetBitProperty(20, 1, Enable_multiProperty) != 0; }
				set { SetBitProperty(20, 1, value ? 1 : 0, Enable_multiProperty); }
			}

			/// <summary>Not available offline</summary>
			/// <remarks>
			/// Japanese short name: "オフラインで使用不可", Google translated: "Not available offline".
			/// Japanese description: "オフライン中に使用不可か？", Google translated: "Do not use while offline ?".
			/// </remarks>
			[ParameterTableRowAttribute("disable_offline:1", index: 48, minimum: 0, maximum: 1, step: 1, sortOrder: 2200, unknown2: 1)]
			[DisplayName("Not available offline")]
			[Description("Do not use while offline ?")]
			[DefaultValue(false)]
			public Boolean Disable_offline {
				get { return GetBitProperty(21, 1, Disable_offlineProperty) != 0; }
				set { SetBitProperty(21, 1, value ? 1 : 0, Disable_offlineProperty); }
			}

			/// <summary>Can be equipped</summary>
			/// <remarks>
			/// Japanese short name: "装備可能", Google translated: "Can be equipped".
			/// Japanese description: "装備できるかどうか", Google translated: "Whether equipment".
			/// </remarks>
			[ParameterTableRowAttribute("isEquip:1", index: 49, minimum: 0, maximum: 1, step: 1, sortOrder: 2400, unknown2: 1)]
			[DisplayName("Can be equipped")]
			[Description("Whether equipment")]
			[DefaultValue(true)]
			public Boolean IsEquip {
				get { return GetBitProperty(22, 1, IsEquipProperty) != 0; }
				set { SetBitProperty(22, 1, value ? 1 : 0, IsEquipProperty); }
			}

			/// <summary>Or consumables</summary>
			/// <remarks>
			/// Japanese short name: "消耗品か", Google translated: "Or consumables".
			/// Japanese description: "使用時に消耗するか(所持数が減るか)", Google translated: "( Possession or decrease the number ) or consumed at the time of use".
			/// </remarks>
			[ParameterTableRowAttribute("isConsume:1", index: 50, minimum: 0, maximum: 1, step: 1, sortOrder: 2500, unknown2: 1)]
			[DisplayName("Or consumables")]
			[Description("( Possession or decrease the number ) or consumed at the time of use")]
			[DefaultValue(true)]
			public Boolean IsConsume {
				get { return GetBitProperty(23, 1, IsConsumeProperty) != 0; }
				set { SetBitProperty(23, 1, value ? 1 : 0, IsConsumeProperty); }
			}

			/// <summary>Or automatic equipment ?</summary>
			/// <remarks>
			/// Japanese short name: "自動装備するか？", Google translated: "Or automatic equipment ?".
			/// Japanese description: "拾った時に自動で装備するか？", Google translated: "You can equip automatically when you picked up ?".
			/// </remarks>
			[ParameterTableRowAttribute("isAutoEquip:1", index: 51, minimum: 0, maximum: 1, step: 1, sortOrder: 2600, unknown2: 1)]
			[DisplayName("Or automatic equipment ?")]
			[Description("You can equip automatically when you picked up ?")]
			[DefaultValue(true)]
			public Boolean IsAutoEquip {
				get { return GetBitProperty(24, 1, IsAutoEquipProperty) != 0; }
				set { SetBitProperty(24, 1, value ? 1 : 0, IsAutoEquipProperty); }
			}

			/// <summary>Or installation type item?</summary>
			/// <remarks>
			/// Japanese short name: "設置型アイテムか？", Google translated: "Or installation type item?".
			/// Japanese description: "設置型アイテムか？", Google translated: "Or installation type item?".
			/// </remarks>
			[ParameterTableRowAttribute("isEstablishment:1", index: 52, minimum: 0, maximum: 1, step: 1, sortOrder: 2700, unknown2: 1)]
			[DisplayName("Or installation type item?")]
			[Description("Or installation type item?")]
			[DefaultValue(false)]
			public Boolean IsEstablishment {
				get { return GetBitProperty(25, 1, IsEstablishmentProperty) != 0; }
				set { SetBitProperty(25, 1, value ? 1 : 0, IsEstablishmentProperty); }
			}

			/// <summary>Do not you have only one</summary>
			/// <remarks>
			/// Japanese short name: "1個しか持てないか", Google translated: "Do not you have only one".
			/// Japanese description: "1個しか持てないアイテムか", Google translated: "Or items that you can only have one".
			/// </remarks>
			[ParameterTableRowAttribute("isOnlyOne:1", index: 53, minimum: 0, maximum: 1, step: 1, sortOrder: 2900, unknown2: 1)]
			[DisplayName("Do not you have only one")]
			[Description("Or items that you can only have one")]
			[DefaultValue(false)]
			public Boolean IsOnlyOne {
				get { return GetBitProperty(26, 1, IsOnlyOneProperty) != 0; }
				set { SetBitProperty(26, 1, value ? 1 : 0, IsOnlyOneProperty); }
			}

			/// <summary>Either discarded</summary>
			/// <remarks>
			/// Japanese short name: "捨てれるか", Google translated: "Either discarded".
			/// Japanese description: "アイテムを捨てれるか？TRUE=捨てれる", Google translated: "Is either discard the item? Is discarded TRUE =".
			/// </remarks>
			[ParameterTableRowAttribute("isDrop:1", index: 54, minimum: 0, maximum: 1, step: 1, sortOrder: 3100, unknown2: 1)]
			[DisplayName("Either discarded")]
			[Description("Is either discard the item? Is discarded TRUE =")]
			[DefaultValue(true)]
			public Boolean IsDrop {
				get { return GetBitProperty(27, 1, IsDropProperty) != 0; }
				set { SetBitProperty(27, 1, value ? 1 : 0, IsDropProperty); }
			}

			/// <summary>Do not use bare hands to the right</summary>
			/// <remarks>
			/// Japanese short name: "右素手に使えないか", Google translated: "Do not use bare hands to the right".
			/// Japanese description: "右手武器が素手の場合に使用不可か", Google translated: "Right hand weapon or disabled in the case of bare hands".
			/// </remarks>
			[ParameterTableRowAttribute("isDisableHand:1", index: 56, minimum: 0, maximum: 1, step: 1, sortOrder: 3200, unknown2: 1)]
			[DisplayName("Do not use bare hands to the right")]
			[Description("Right hand weapon or disabled in the case of bare hands")]
			[DefaultValue(false)]
			public Boolean IsDisableHand {
				get { return GetBitProperty(29, 1, IsDisableHandProperty) != 0; }
				set { SetBitProperty(29, 1, value ? 1 : 0, IsDisableHandProperty); }
			}

			/// <summary>Or items to travel</summary>
			/// <remarks>
			/// Japanese short name: "旅するアイテムか", Google translated: "Or items to travel".
			/// Japanese description: "旅するアイテム判別に使用します", Google translated: "It is used to determine items to travel".
			/// </remarks>
			[ParameterTableRowAttribute("IsTravelItem:1", index: 57, minimum: 0, maximum: 1, step: 1, sortOrder: 3300, unknown2: 1)]
			[DisplayName("Or items to travel")]
			[Description("It is used to determine items to travel")]
			[DefaultValue(false)]
			public Boolean IsTravelItem {
				get { return GetBitProperty(30, 1, IsTravelItemProperty) != 0; }
				set { SetBitProperty(30, 1, value ? 1 : 0, IsTravelItemProperty); }
			}

			/// <summary>Or replenishment items</summary>
			/// <remarks>
			/// Japanese short name: "補充アイテムか", Google translated: "Or replenishment items".
			/// Japanese description: "補充可能アイテムを判別するのに使用します", Google translated: "It is used to determine the refillable items".
			/// </remarks>
			[ParameterTableRowAttribute("isSuppleItem:1", index: 58, minimum: 0, maximum: 1, step: 1, sortOrder: 3310, unknown2: 1)]
			[DisplayName("Or replenishment items")]
			[Description("It is used to determine the refillable items")]
			[DefaultValue(false)]
			public Boolean IsSuppleItem {
				get { return GetBitProperty(31, 1, IsSuppleItemProperty) != 0; }
				set { SetBitProperty(31, 1, value ? 1 : 0, IsSuppleItemProperty); }
			}

			/// <summary>Or supplementation Items</summary>
			/// <remarks>
			/// Japanese short name: "補充済みアイテムか", Google translated: "Or supplementation Items".
			/// Japanese description: "補充済みアイテムを判別するのに使用します", Google translated: "It is used to determine the replenishment Items".
			/// </remarks>
			[ParameterTableRowAttribute("isFullSuppleItem:1", index: 59, minimum: 0, maximum: 1, step: 1, sortOrder: 3311, unknown2: 1)]
			[DisplayName("Or supplementation Items")]
			[Description("It is used to determine the replenishment Items")]
			[DefaultValue(false)]
			public Boolean IsFullSuppleItem {
				get { return GetBitProperty(32, 1, IsFullSuppleItemProperty) != 0; }
				set { SetBitProperty(32, 1, value ? 1 : 0, IsFullSuppleItemProperty); }
			}

			/// <summary>Or enchant ?</summary>
			/// <remarks>
			/// Japanese short name: "エンチャントするか？", Google translated: "Or enchant ?".
			/// Japanese description: "武器にエンチャントするか？", Google translated: "You can enchant weapons ?".
			/// </remarks>
			[ParameterTableRowAttribute("isEnhance:1", index: 60, minimum: 0, maximum: 1, step: 1, sortOrder: 4000, unknown2: 1)]
			[DisplayName("Or enchant ?")]
			[Description("You can enchant weapons ?")]
			[DefaultValue(false)]
			public Boolean IsEnhance {
				get { return GetBitProperty(33, 1, IsEnhanceProperty) != 0; }
				set { SetBitProperty(33, 1, value ? 1 : 0, IsEnhanceProperty); }
			}

			/// <summary>Or repair items</summary>
			/// <remarks>
			/// Japanese short name: "修理アイテムか", Google translated: "Or repair items".
			/// Japanese description: "修理するアイテムか？", Google translated: "The item to be repaired ?".
			/// </remarks>
			[ParameterTableRowAttribute("isFixItem:1", index: 61, minimum: 0, maximum: 1, step: 1, sortOrder: 3250, unknown2: 1)]
			[DisplayName("Or repair items")]
			[Description("The item to be repaired ?")]
			[DefaultValue(false)]
			public Boolean IsFixItem {
				get { return GetBitProperty(34, 1, IsFixItemProperty) != 0; }
				set { SetBitProperty(34, 1, value ? 1 : 0, IsFixItemProperty); }
			}

			/// <summary>Or multi-drop sharing ban</summary>
			/// <remarks>
			/// Japanese short name: "マルチドロップ共有禁止か", Google translated: "Or multi-drop sharing ban".
			/// Japanese description: "マルチドロップ共有禁止か", Google translated: "Or multi-drop sharing ban".
			/// </remarks>
			[ParameterTableRowAttribute("disableMultiDropShare:1", index: 62, minimum: 0, maximum: 1, step: 1, sortOrder: 3155, unknown2: 1)]
			[DisplayName("Or multi-drop sharing ban")]
			[Description("Or multi-drop sharing ban")]
			[DefaultValue(false)]
			public Boolean DisableMultiDropShare {
				get { return GetBitProperty(35, 1, DisableMultiDropShareProperty) != 0; }
				set { SetBitProperty(35, 1, value ? 1 : 0, DisableMultiDropShareProperty); }
			}

			/// <summary>Or disabled in the arena</summary>
			/// <remarks>
			/// Japanese short name: "闘技場で使用禁止か", Google translated: "Or disabled in the arena".
			/// Japanese description: "闘技場で使用禁止か", Google translated: "Or disabled in the arena".
			/// </remarks>
			[ParameterTableRowAttribute("disableUseAtColiseum:1", index: 63, minimum: 0, maximum: 1, step: 1, sortOrder: 3160, unknown2: 1)]
			[DisplayName("Or disabled in the arena")]
			[Description("Or disabled in the arena")]
			[DefaultValue(false)]
			public Boolean DisableUseAtColiseum {
				get { return GetBitProperty(36, 1, DisableUseAtColiseumProperty) != 0; }
				set { SetBitProperty(36, 1, value ? 1 : 0, DisableUseAtColiseumProperty); }
			}

			/// <summary>Or disabled in the arena other than</summary>
			/// <remarks>
			/// Japanese short name: "闘技場以外で使用禁止か", Google translated: "Or disabled in the arena other than".
			/// Japanese description: "闘技場以外で使用禁止か", Google translated: "Or disabled in the arena other than".
			/// </remarks>
			[ParameterTableRowAttribute("disableUseAtOutOfColiseum:1", index: 64, minimum: 0, maximum: 1, step: 1, sortOrder: 3160, unknown2: 1)]
			[DisplayName("Or disabled in the arena other than")]
			[Description("Or disabled in the arena other than")]
			[DefaultValue(false)]
			public Boolean DisableUseAtOutOfColiseum {
				get { return GetBitProperty(37, 1, DisableUseAtOutOfColiseumProperty) != 0; }
				set { SetBitProperty(37, 1, value ? 1 : 0, DisableUseAtOutOfColiseumProperty); }
			}

			/// <summary>Padding</summary>
			/// <remarks>
			/// Japanese short name: "パディング", Google translated: "Padding".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("pad[9]", index: 65, minimum: 0, maximum: 0, step: 0, sortOrder: 99999, unknown2: 0)]
			[DisplayName("Padding")]
			[Description("")]
			public Byte[] Pad {
				get { return pad; }
				set { SetProperty(ref pad, ref value, PadProperty); }
			}

			/// <summary>Get the localised English name of this <see cref="Good"/>.</summary>
			public string EnglishName { get { return GetLocalisedName(Language.English); } }

			/// <summary>Get the localised English description of this <see cref="Good"/>.</summary>
			public string EnglishDescription { get { return GetLocalisedDescription(Language.English).Trim(); } }

			/// <summary>Get the localised English effect of this <see cref="Good"/>.</summary>
			public string EnglishEffects { get { return GetLocalisedEffects(Language.English); } }

			internal Good(ParameterTable table, int index, AssetLoader loader, int next)
				: base(table, index, loader) {
				BinaryReader reader = loader.Reader;

				RefId = reader.ReadInt32();
				SfxVariationId = reader.ReadInt32();
				Weight = reader.ReadSingle();
				BasicPrice = reader.ReadInt32();
				SellValue = reader.ReadInt32();
				BehaviorId = reader.ReadInt32();
				ReplaceItemId = reader.ReadInt32();
				SortId = reader.ReadInt32();
				QwcId = reader.ReadInt32();
				YesNoDialogMessageId = reader.ReadInt32();
				MagicId = reader.ReadInt32();
				IconId = reader.ReadUInt16();
				ModelId = reader.ReadUInt16();
				ShopLevel = reader.ReadInt16();
				CompTrophySedId = reader.ReadInt16();
				TrophySeqId = reader.ReadInt16();
				MaxStack = reader.ReadInt16();
				ConsumeHumanity = reader.ReadByte();
				OverDexterity = reader.ReadByte();
				ItemType = (ItemType)reader.ReadByte();
				RefCategory = (BehaviorRefType)reader.ReadByte();
				SpEffectCategory = (BehaviorCategory)reader.ReadByte();
				GoodsCategory = (ItemCategory)reader.ReadByte();
				GoodsUseAnim = (ItemUseAnimation)reader.ReadByte();
				UseMenu = (ItemUseMenu)reader.ReadByte();
				UseLimitCategory = (SpecialEffectUseLimitCategory)reader.ReadByte();
				ReplaceCategory = (ReplacementCategory)reader.ReadByte();
				BitFields = reader.ReadBytes(5);
				Pad = reader.ReadBytes(9);
				VagrantItemLotId = reader.ReadInt32();
				VagrantBonusEneDropItemLotId = reader.ReadInt32();
				VagrantItemEneDropItemLotId = reader.ReadInt32();
			}

			internal Good(ParameterTable table, int index)
				: base(table, index) {
				BitFields = new byte[5];
				RefId = (Int32)(-1);
				SfxVariationId = (Int32)(-1);
				Weight = (Single)1;
				BasicPrice = (Int32)0;
				SellValue = (Int32)0;
				BehaviorId = (Int32)0;
				ReplaceItemId = (Int32)(-1);
				SortId = (Int32)0;
				QwcId = (Int32)(-1);
				YesNoDialogMessageId = (Int32)(-1);
				MagicId = (Int32)(-1);
				IconId = (UInt16)0;
				ModelId = (UInt16)0;
				ShopLevel = (Int16)0;
				CompTrophySedId = (Int16)(-1);
				TrophySeqId = (Int16)(-1);
				MaxStack = (Int16)0;
				ConsumeHumanity = (Byte)0;
				OverDexterity = (Byte)0;
				ItemType = (ItemType)0;
				RefCategory = (BehaviorRefType)0;
				SpEffectCategory = (BehaviorCategory)0;
				GoodsCategory = (ItemCategory)0;
				GoodsUseAnim = (ItemUseAnimation)0;
				UseMenu = (ItemUseMenu)0;
				UseLimitCategory = (SpecialEffectUseLimitCategory)0;
				ReplaceCategory = (ReplacementCategory)0;
				VowType0 = false;
				VowType1 = false;
				VowType2 = false;
				VowType3 = false;
				VowType4 = false;
				VowType5 = false;
				VowType6 = false;
				VowType7 = false;
				VowType8 = false;
				VowType9 = false;
				VowType10 = false;
				VowType11 = false;
				VowType12 = false;
				VowType13 = false;
				VowType14 = false;
				VowType15 = false;
				Enable_live = false;
				Enable_gray = false;
				Enable_white = false;
				Enable_black = false;
				Enable_multi = false;
				Disable_offline = false;
				IsEquip = true;
				IsConsume = true;
				IsAutoEquip = true;
				IsEstablishment = false;
				IsOnlyOne = false;
				IsDrop = true;
				IsDeposit = true;
				IsDisableHand = false;
				IsTravelItem = false;
				IsSuppleItem = false;
				IsFullSuppleItem = false;
				IsEnhance = false;
				IsFixItem = false;
				DisableMultiDropShare = false;
				DisableUseAtColiseum = false;
				DisableUseAtOutOfColiseum = false;
				Pad = new Byte[9];
			}

			/// <summary>Get the localised name of this <see cref="Good"/>.</summary>
			/// <param name="language"></param>
			/// <returns></returns>
			public string GetLocalisedName(Language language = Language.English) { return GetLocalisedString(Engine.ItemArchiveId.GoodsNames, language); }

			/// <summary>Get the localised description of this <see cref="Good"/>.</summary>
			/// <param name="language"></param>
			/// <returns></returns>
			public string GetLocalisedDescription(Language language = Language.English) { return GetLocalisedString(Engine.ItemArchiveId.GoodsDescriptions, language); }

			/// <summary>Get the localised effect of this <see cref="Good"/>.</summary>
			/// <param name="language"></param>
			/// <returns></returns>
			public string GetLocalisedEffects(Language language = Language.English) { return GetLocalisedString(Engine.ItemArchiveId.ItemEffects, language); }

			/// <summary>Write the <see cref="Good"/>.</summary>
			/// <param name="writer"></param>
			public override void Write(BinaryWriter writer) {
				writer.Write(RefId);
				writer.Write(SfxVariationId);
				writer.Write(Weight);
				writer.Write(BasicPrice);
				writer.Write(SellValue);
				writer.Write(BehaviorId);
				writer.Write(ReplaceItemId);
				writer.Write(SortId);
				writer.Write(QwcId);
				writer.Write(YesNoDialogMessageId);
				writer.Write(MagicId);
				writer.Write(IconId);
				writer.Write(ModelId);
				writer.Write(ShopLevel);
				writer.Write(CompTrophySedId);
				writer.Write(TrophySeqId);
				writer.Write(MaxStack);
				writer.Write(ConsumeHumanity);
				writer.Write(OverDexterity);
				writer.Write((Byte)ItemType);
				writer.Write((Byte)RefCategory);
				writer.Write((Byte)SpEffectCategory);
				writer.Write((Byte)GoodsCategory);
				writer.Write((Byte)GoodsUseAnim);
				writer.Write((Byte)UseMenu);
				writer.Write((Byte)UseLimitCategory);
				writer.Write((Byte)ReplaceCategory);
				writer.Write(BitFields);
				writer.Write(Pad);
				writer.Write(VagrantItemLotId);
				writer.Write(VagrantBonusEneDropItemLotId);
				writer.Write(VagrantItemEneDropItemLotId);
			}
		}
	}
}
