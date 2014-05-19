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
		/// <summary>Rings.</summary>
		/// <remarks>
		/// From "EquipParamAccessory.paramdef" (id 0Eh)
		/// </remarks>
		[ParameterTableRowOrder("sortId", 6, 1600)]
		[ParameterTableRowOrder("weight", 2, 1100)]
		[ParameterTableRowOrder("basicPrice", 4, 1300)]
		[ParameterTableRowOrder("sellValue", 5, 1310)]
		[ParameterTableRowOrder("isDeposit", 22, 1800)]
		[ParameterTableRowOrder("vagrantItemLotId", 19, 10000)]
		[ParameterTableRowOrder("vagrantBonusEneDropItemLotId", 20, 10000)]
		[ParameterTableRowOrder("vagrantItemEneDropItemLotId", 21, 10000)]
		[ParameterTableRowOrder("equipModelCategory", 13, 100)]
		[ParameterTableRowOrder("equipModelGender", 14, 200)]
		[ParameterTableRowOrder("equipModelId", 46, 300)]
		public class Accessory : WornTableRow {
			public const string TableName = "EQUIP_PARAM_ACCESSORY_ST";

			Int32 refId, sfxVariationId, behaviorId, qwcId;
			UInt16 iconId;
			Int16 shopLv, trophySGradeId, trophySeqId;
			AccessoryCategory accessoryCategory;
			BehaviorRefType refCategory;
			BehaviorCategory spEffectCategory;
			Byte[] pad, pad1;

			public static readonly PropertyInfo
				RefIdProperty = GetProperty<Accessory>("RefId"),
				SfxVariationIdProperty = GetProperty<Accessory>("SfxVariationId"),
				BehaviorIdProperty = GetProperty<Accessory>("BehaviorId"),
				QwcIdProperty = GetProperty<Accessory>("QwcId"),
				IconIdProperty = GetProperty<Accessory>("IconId"),
				ShopLvProperty = GetProperty<Accessory>("ShopLv"),
				TrophySGradeIdProperty = GetProperty<Accessory>("TrophySGradeId"),
				TrophySeqIdProperty = GetProperty<Accessory>("TrophySeqId"),
				AccessoryCategoryProperty = GetProperty<Accessory>("AccessoryCategory"),
				RefCategoryProperty = GetProperty<Accessory>("RefCategory"),
				SpEffectCategoryProperty = GetProperty<Accessory>("SpEffectCategory"),
				PadProperty = GetProperty<Accessory>("Pad"),
				IsEquipOutBrakeProperty = GetProperty<Accessory>("IsEquipOutBrake"),
				DisableMultiDropShareProperty = GetProperty<Accessory>("DisableMultiDropShare"),
				Pad1Property = GetProperty<Accessory>("Pad1");

			/// <summary>Call ID</summary>
			/// <remarks>
			/// Japanese short name: "呼び出しID", Google translated: "Call ID".
			/// Japanese description: "装飾品から呼び出すID", Google translated: "ID calling from ornaments".
			/// </remarks>
			[ParameterTableRowAttribute("refId", index: 0, minimum: -1, maximum: 1E+08, step: 1, order: 700, unknown2: 1)]
			[DisplayName("Call ID")]
			[Description("ID calling from ornaments")]
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
			[ParameterTableRowAttribute("sfxVariationId", index: 1, minimum: -1, maximum: 1E+08, step: 1, order: 800, unknown2: 1)]
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
			/// Japanese description: "行動ID(=Skill)", Google translated: "Action ID (= Skill)".
			/// </remarks>
			[ParameterTableRowAttribute("behaviorId", index: 3, minimum: 0, maximum: 1E+08, step: 1, order: 1200, unknown2: 1)]
			[DisplayName("Action ID")]
			[Description("Action ID (= Skill)")]
			[DefaultValue((Int32)0)]
			public Int32 BehaviorId {
				get { return behaviorId; }
				set {
					if ((double)value < 0 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1E+08 for BehaviorId.");
					SetProperty(ref behaviorId, ref value, BehaviorIdProperty);
				}
			}

			/// <summary>QWCID</summary>
			/// <remarks>
			/// Japanese short name: "QWCID", Google translated: "QWCID".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("qwcId", index: 7, minimum: -1, maximum: 1E+08, step: 1, order: 1900, unknown2: 1)]
			[DisplayName("QWCID")]
			[Description("")]
			[DefaultValue((Int32)(-1))]
			public Int32 QwcId {
				get { return qwcId; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for QwcId.");
					SetProperty(ref qwcId, ref value, QwcIdProperty);
				}
			}

			/// <summary>Icon ID</summary>
			/// <remarks>
			/// Japanese short name: "アイコンID", Google translated: "Icon ID".
			/// Japanese description: "メニューアイコンID", Google translated: "Menu icon ID".
			/// </remarks>
			[ParameterTableRowAttribute("iconId", index: 9, minimum: 0, maximum: 9999, step: 1, order: 400, unknown2: 1)]
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

			/// <summary>Shop level</summary>
			/// <remarks>
			/// Japanese short name: "ショップレベル", Google translated: "Shop level".
			/// Japanese description: "お店で販売できるレベル", Google translated: "Level that can be sold in the shop".
			/// </remarks>
			[ParameterTableRowAttribute("shopLv", index: 10, minimum: -1, maximum: 9999, step: 1, order: 1400, unknown2: 1)]
			[DisplayName("Shop level")]
			[Description("Level that can be sold in the shop")]
			[DefaultValue((Int16)0)]
			public Int16 ShopLv {
				get { return shopLv; }
				set {
					if ((double)value < -1 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 9999 for ShopLv.");
					SetProperty(ref shopLv, ref value, ShopLvProperty);
				}
			}

			/// <summary>Trophy</summary>
			/// <remarks>
			/// Japanese short name: "トロフィー", Google translated: "Trophy".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("trophySGradeId", index: 11, minimum: -1, maximum: 9999, step: 1, order: 1700, unknown2: 1)]
			[DisplayName("Trophy")]
			[Description("")]
			[DefaultValue((Int16)(-1))]
			public Int16 TrophySGradeId {
				get { return trophySGradeId; }
				set {
					if ((double)value < -1 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 9999 for TrophySGradeId.");
					SetProperty(ref trophySGradeId, ref value, TrophySGradeIdProperty);
				}
			}

			/// <summary>Trophy SEQ number</summary>
			/// <remarks>
			/// Japanese short name: "トロフィーSEQ番号", Google translated: "Trophy SEQ number".
			/// Japanese description: "トロフィーのSEQ番号", Google translated: "SEQ number of trophy".
			/// </remarks>
			[ParameterTableRowAttribute("trophySeqId", index: 12, minimum: -1, maximum: 9999, step: 1, order: 1750, unknown2: 1)]
			[DisplayName("Trophy SEQ number")]
			[Description("SEQ number of trophy")]
			[DefaultValue((Int16)(-1))]
			public Int16 TrophySeqId {
				get { return trophySeqId; }
				set {
					if ((double)value < -1 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 9999 for TrophySeqId.");
					SetProperty(ref trophySeqId, ref value, TrophySeqIdProperty);
				}
			}

			/// <summary>Decoration category</summary>
			/// <remarks>
			/// Japanese short name: "装飾カテゴリ", Google translated: "Decoration category".
			/// Japanese description: "防具のカテゴリ", Google translated: "Category of Armor".
			/// </remarks>
			[ParameterTableRowAttribute("accessoryCategory", index: 15, minimum: 0, maximum: 99, step: 1, order: 500, unknown2: 1)]
			[DisplayName("Decoration category")]
			[Description("Category of Armor")]
			[DefaultValue((AccessoryCategory)0)]
			public AccessoryCategory AccessoryCategory {
				get { return accessoryCategory; }
				set { SetProperty(ref accessoryCategory, ref value, AccessoryCategoryProperty); }
			}

			/// <summary>ID category</summary>
			/// <remarks>
			/// Japanese short name: "IDカテゴリ", Google translated: "ID category".
			/// Japanese description: "↓のIDのカテゴリ[攻撃、飛び道具、特殊]", Google translated: "↓ category of ID [ attack , missile , special ]".
			/// </remarks>
			[ParameterTableRowAttribute("refCategory", index: 16, minimum: 0, maximum: 255, step: 1, order: 600, unknown2: 1)]
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
			[ParameterTableRowAttribute("spEffectCategory", index: 17, minimum: 0, maximum: 255, step: 1, order: 900, unknown2: 1)]
			[DisplayName("Special effects category")]
			[Description("Skills , magic , or item , because the effect of variation in parameters (such as Enchant Weapon ) , and sets the action for each effect it was │ boss is , to cope with effects such as' power up the only weapon attack \" things such as varistor , without the need for setting is set to \" None\"")]
			[DefaultValue((BehaviorCategory)0)]
			public BehaviorCategory SpEffectCategory {
				get { return spEffectCategory; }
				set { SetProperty(ref spEffectCategory, ref value, SpEffectCategoryProperty); }
			}

			/// <summary>Padding</summary>
			/// <remarks>
			/// Japanese short name: "パディング", Google translated: "Padding".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("pad[1]", index: 18, minimum: 0, maximum: 0, step: 0, order: 99999, unknown2: 0)]
			[DisplayName("Padding")]
			[Description("")]
			[Browsable(false)]
			public Byte[] Pad {
				get { return pad; }
				set { SetProperty(ref pad, ref value, PadProperty); }
			}

			/// <summary>Or break and remove</summary>
			/// <remarks>
			/// Japanese short name: "外すと壊れるか", Google translated: "Or break and remove".
			/// Japanese description: "装備して外す時に壊れるか", Google translated: "Or broken when removing the equipped".
			/// </remarks>
			[ParameterTableRowAttribute("isEquipOutBrake:1", index: 23, minimum: 0, maximum: 1, step: 1, order: 1810, unknown2: 1)]
			[DisplayName("Or break and remove")]
			[Description("Or broken when removing the equipped")]
			[DefaultValue(false)]
			public Boolean IsEquipOutBrake {
				get { return GetBitProperty(1, 1, IsEquipOutBrakeProperty) != 0; }
				set { SetBitProperty(1, 1, value ? 1 : 0, IsEquipOutBrakeProperty); }
			}

			/// <summary>Or multi-drop sharing ban</summary>
			/// <remarks>
			/// Japanese short name: "マルチドロップ共有禁止か", Google translated: "Or multi-drop sharing ban".
			/// Japanese description: "マルチドロップ共有禁止か", Google translated: "Or multi-drop sharing ban".
			/// </remarks>
			[ParameterTableRowAttribute("disableMultiDropShare:1", index: 24, minimum: 0, maximum: 1, step: 1, order: 1805, unknown2: 1)]
			[DisplayName("Or multi-drop sharing ban")]
			[Description("Or multi-drop sharing ban")]
			[DefaultValue(false)]
			public Boolean DisableMultiDropShare {
				get { return GetBitProperty(2, 1, DisableMultiDropShareProperty) != 0; }
				set { SetBitProperty(2, 1, value ? 1 : 0, DisableMultiDropShareProperty); }
			}

			/// <summary>pad</summary>
			/// <remarks>
			/// Japanese short name: "pad", Google translated: "pad".
			/// Japanese description: "pad", Google translated: "pad".
			/// </remarks>
			[ParameterTableRowAttribute("pad1[3]", index: 25, minimum: 0, maximum: 0, step: 0, order: 100000, unknown2: 0)]
			[DisplayName("pad")]
			[Description("pad")]
			[Browsable(false)]
			public Byte[] Pad1 {
				get { return pad1; }
				set { SetProperty(ref pad1, ref value, Pad1Property); }
			}

			public string EnglishName { get { return GetLocalisedName(Language.English); } }

			public string EnglishDescription { get { return GetLocalisedDescription(Language.English).Trim(); } }

			public string EnglishEffects { get { return GetLocalisedEffects(Language.English); } }

			internal Accessory(ParameterTable table, int index, AssetLoader loader, int next)
				: base(table, index, loader) {
				BinaryReader reader = loader.Reader;

				RefId = reader.ReadInt32();
				SfxVariationId = reader.ReadInt32();
				Weight = reader.ReadSingle();
				BehaviorId = reader.ReadInt32();
				BasicPrice = reader.ReadInt32();
				SellValue = reader.ReadInt32();
				SortId = reader.ReadInt32();
				QwcId = reader.ReadInt32();
				EquipModelId = reader.ReadUInt16();
				IconId = reader.ReadUInt16();
				ShopLv = reader.ReadInt16();
				TrophySGradeId = reader.ReadInt16();
				TrophySeqId = reader.ReadInt16();
				EquipModelCategory = (EquipModelCategory)reader.ReadByte();
				EquipModelGender = (EquipModelGender)reader.ReadByte();
				AccessoryCategory = (AccessoryCategory)reader.ReadByte();
				RefCategory = (BehaviorRefType)reader.ReadByte();
				SpEffectCategory = (BehaviorCategory)reader.ReadByte();
				Pad = reader.ReadBytes(1);
				VagrantItemLotId = reader.ReadInt32();
				VagrantBonusEneDropItemLotId = reader.ReadInt32();
				VagrantItemEneDropItemLotId = reader.ReadInt32();
				BitFields = reader.ReadBytes(1);
				Pad1 = reader.ReadBytes(3);
			}

			internal Accessory(ParameterTable table, int index)
				: base(table, index) {
				BitFields = new byte[1];
				RefId = (Int32)(-1);
				SfxVariationId = (Int32)(-1);
				Weight = (Single)1;
				BehaviorId = (Int32)0;
				BasicPrice = (Int32)0;
				SellValue = (Int32)0;
				SortId = (Int32)0;
				QwcId = (Int32)(-1);
				IconId = (UInt16)0;
				ShopLv = (Int16)0;
				TrophySGradeId = (Int16)(-1);
				TrophySeqId = (Int16)(-1);;
				AccessoryCategory = (AccessoryCategory)0;
				RefCategory = (BehaviorRefType)0;
				SpEffectCategory = (BehaviorCategory)0;
				Pad = new Byte[1];
				IsDeposit = true;
				IsEquipOutBrake = false;
				DisableMultiDropShare = false;
				Pad1 = new Byte[3];
			}

			public string GetLocalisedName(Language language = Language.English) { return GetLocalisedString(Engine.ItemArchiveId.RingNames, language); }
			public string GetLocalisedDescription(Language language = Language.English) { return GetLocalisedString(Engine.ItemArchiveId.RingDescriptions, language); }
			public string GetLocalisedEffects(Language language = Language.English) { return GetLocalisedString(Engine.ItemArchiveId.RingEffects, language); }

			public override void Write(BinaryWriter writer) {
				writer.Write(RefId);
				writer.Write(SfxVariationId);
				writer.Write(Weight);
				writer.Write(BehaviorId);
				writer.Write(BasicPrice);
				writer.Write(SellValue);
				writer.Write(SortId);
				writer.Write(QwcId);
				writer.Write(EquipModelId);
				writer.Write(IconId);
				writer.Write(ShopLv);
				writer.Write(TrophySGradeId);
				writer.Write(TrophySeqId);
				writer.Write((Byte)EquipModelCategory);
				writer.Write((Byte)EquipModelGender);
				writer.Write((Byte)AccessoryCategory);
				writer.Write((Byte)RefCategory);
				writer.Write((Byte)SpEffectCategory);
				writer.Write(Pad);
				writer.Write(VagrantItemLotId);
				writer.Write(VagrantBonusEneDropItemLotId);
				writer.Write(VagrantItemEneDropItemLotId);
				writer.Write(BitFields);
				writer.Write(Pad1);
			}
		}
	}
}
