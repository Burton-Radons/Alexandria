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
		/// 
		/// </summary>
		/// <remarks>
		/// From CharaInitParam.paramdef (id 24h).
		/// </remarks>
		public class CharacterInitialiser : ParameterTableRow {
			/// <summary>The name of the table in the file.</summary>
			public const string TableName = "CHARACTER_INIT_PARAM";

			Single baseRec_mp, baseRec_sp, red_Falldam;
			Int32 soul, equip_Wep_Right, equip_Subwep_Right, equip_Wep_Left, equip_Subwep_Left, equip_Helm, equip_Armer, equip_Gaunt, equip_Leg, equip_Arrow, equip_Bolt, equip_SubArrow, equip_SubBolt, equip_Accessory01, equip_Accessory02, equip_Accessory03, equip_Accessory04, equip_Accessory05, equip_Skill_01, equip_Skill_02, equip_Skill_03, equip_Spell_01, equip_Spell_02, equip_Spell_03, equip_Spell_04, equip_Spell_05, equip_Spell_06, equip_Spell_07, item_01, item_02, item_03, item_04, item_05, item_06, item_07, item_08, item_09, item_10, npcPlayerFaceGenId, npcPlayerThinkId;
			UInt16 baseHp, baseMp, baseSp, arrowNum, boltNum, subArrowNum, subBoltNum;
			Int16 qWC_sb, qWC_mw, qWC_cd, soulLv;
			Byte baseVit, baseWil, baseEnd, baseStr, baseDex, baseMag, baseFai, baseLuc, baseHeroPoint, baseDurability, itemNum_01, itemNum_02, itemNum_03, itemNum_04, itemNum_05, itemNum_06, itemNum_07, itemNum_08, itemNum_09, itemNum_10;
			SByte bodyScaleHead, bodyScaleBreast, bodyScaleAbdomen, bodyScaleArm, bodyScaleLeg, gestureId0, gestureId1, gestureId2, gestureId3, gestureId4, gestureId5, gestureId6;
			NpcType npcPlayerType;
			NpcDrawType npcPlayerDrawType;
			CharacterInitialSex npcPlayerSex;
			Byte[] pad0;

			/// <summary>A property of the class.</summary>
			public static readonly PropertyInfo
				BaseRec_mpProperty = GetProperty<CharacterInitialiser>("BaseRec_mp"),
				BaseRec_spProperty = GetProperty<CharacterInitialiser>("BaseRec_sp"),
				Red_FalldamProperty = GetProperty<CharacterInitialiser>("Red_Falldam"),
				SoulProperty = GetProperty<CharacterInitialiser>("Soul"),
				Equip_Wep_RightProperty = GetProperty<CharacterInitialiser>("Equip_Wep_Right"),
				Equip_Subwep_RightProperty = GetProperty<CharacterInitialiser>("Equip_Subwep_Right"),
				Equip_Wep_LeftProperty = GetProperty<CharacterInitialiser>("Equip_Wep_Left"),
				Equip_Subwep_LeftProperty = GetProperty<CharacterInitialiser>("Equip_Subwep_Left"),
				Equip_HelmProperty = GetProperty<CharacterInitialiser>("Equip_Helm"),
				Equip_ArmerProperty = GetProperty<CharacterInitialiser>("Equip_Armer"),
				Equip_GauntProperty = GetProperty<CharacterInitialiser>("Equip_Gaunt"),
				Equip_LegProperty = GetProperty<CharacterInitialiser>("Equip_Leg"),
				Equip_ArrowProperty = GetProperty<CharacterInitialiser>("Equip_Arrow"),
				Equip_BoltProperty = GetProperty<CharacterInitialiser>("Equip_Bolt"),
				Equip_SubArrowProperty = GetProperty<CharacterInitialiser>("Equip_SubArrow"),
				Equip_SubBoltProperty = GetProperty<CharacterInitialiser>("Equip_SubBolt"),
				Equip_Accessory01Property = GetProperty<CharacterInitialiser>("Equip_Accessory01"),
				Equip_Accessory02Property = GetProperty<CharacterInitialiser>("Equip_Accessory02"),
				Equip_Accessory03Property = GetProperty<CharacterInitialiser>("Equip_Accessory03"),
				Equip_Accessory04Property = GetProperty<CharacterInitialiser>("Equip_Accessory04"),
				Equip_Accessory05Property = GetProperty<CharacterInitialiser>("Equip_Accessory05"),
				Equip_Skill_01Property = GetProperty<CharacterInitialiser>("Equip_Skill_01"),
				Equip_Skill_02Property = GetProperty<CharacterInitialiser>("Equip_Skill_02"),
				Equip_Skill_03Property = GetProperty<CharacterInitialiser>("Equip_Skill_03"),
				Equip_Spell_01Property = GetProperty<CharacterInitialiser>("Equip_Spell_01"),
				Equip_Spell_02Property = GetProperty<CharacterInitialiser>("Equip_Spell_02"),
				Equip_Spell_03Property = GetProperty<CharacterInitialiser>("Equip_Spell_03"),
				Equip_Spell_04Property = GetProperty<CharacterInitialiser>("Equip_Spell_04"),
				Equip_Spell_05Property = GetProperty<CharacterInitialiser>("Equip_Spell_05"),
				Equip_Spell_06Property = GetProperty<CharacterInitialiser>("Equip_Spell_06"),
				Equip_Spell_07Property = GetProperty<CharacterInitialiser>("Equip_Spell_07"),
				Item_01Property = GetProperty<CharacterInitialiser>("Item_01"),
				Item_02Property = GetProperty<CharacterInitialiser>("Item_02"),
				Item_03Property = GetProperty<CharacterInitialiser>("Item_03"),
				Item_04Property = GetProperty<CharacterInitialiser>("Item_04"),
				Item_05Property = GetProperty<CharacterInitialiser>("Item_05"),
				Item_06Property = GetProperty<CharacterInitialiser>("Item_06"),
				Item_07Property = GetProperty<CharacterInitialiser>("Item_07"),
				Item_08Property = GetProperty<CharacterInitialiser>("Item_08"),
				Item_09Property = GetProperty<CharacterInitialiser>("Item_09"),
				Item_10Property = GetProperty<CharacterInitialiser>("Item_10"),
				NpcPlayerFaceGenIdProperty = GetProperty<CharacterInitialiser>("NpcPlayerFaceGenId"),
				NpcPlayerThinkIdProperty = GetProperty<CharacterInitialiser>("NpcPlayerThinkId"),
				BaseHpProperty = GetProperty<CharacterInitialiser>("BaseHp"),
				BaseMpProperty = GetProperty<CharacterInitialiser>("BaseMp"),
				BaseSpProperty = GetProperty<CharacterInitialiser>("BaseSp"),
				ArrowNumProperty = GetProperty<CharacterInitialiser>("ArrowNum"),
				BoltNumProperty = GetProperty<CharacterInitialiser>("BoltNum"),
				SubArrowNumProperty = GetProperty<CharacterInitialiser>("SubArrowNum"),
				SubBoltNumProperty = GetProperty<CharacterInitialiser>("SubBoltNum"),
				QWC_sbProperty = GetProperty<CharacterInitialiser>("QWC_sb"),
				QWC_mwProperty = GetProperty<CharacterInitialiser>("QWC_mw"),
				QWC_cdProperty = GetProperty<CharacterInitialiser>("QWC_cd"),
				SoulLvProperty = GetProperty<CharacterInitialiser>("SoulLv"),
				BaseVitProperty = GetProperty<CharacterInitialiser>("BaseVit"),
				BaseWilProperty = GetProperty<CharacterInitialiser>("BaseWil"),
				BaseEndProperty = GetProperty<CharacterInitialiser>("BaseEnd"),
				BaseStrProperty = GetProperty<CharacterInitialiser>("BaseStr"),
				BaseDexProperty = GetProperty<CharacterInitialiser>("BaseDex"),
				BaseMagProperty = GetProperty<CharacterInitialiser>("BaseMag"),
				BaseFaiProperty = GetProperty<CharacterInitialiser>("BaseFai"),
				BaseLucProperty = GetProperty<CharacterInitialiser>("BaseLuc"),
				BaseHeroPointProperty = GetProperty<CharacterInitialiser>("BaseHeroPoint"),
				BaseDurabilityProperty = GetProperty<CharacterInitialiser>("BaseDurability"),
				ItemNum_01Property = GetProperty<CharacterInitialiser>("ItemNum_01"),
				ItemNum_02Property = GetProperty<CharacterInitialiser>("ItemNum_02"),
				ItemNum_03Property = GetProperty<CharacterInitialiser>("ItemNum_03"),
				ItemNum_04Property = GetProperty<CharacterInitialiser>("ItemNum_04"),
				ItemNum_05Property = GetProperty<CharacterInitialiser>("ItemNum_05"),
				ItemNum_06Property = GetProperty<CharacterInitialiser>("ItemNum_06"),
				ItemNum_07Property = GetProperty<CharacterInitialiser>("ItemNum_07"),
				ItemNum_08Property = GetProperty<CharacterInitialiser>("ItemNum_08"),
				ItemNum_09Property = GetProperty<CharacterInitialiser>("ItemNum_09"),
				ItemNum_10Property = GetProperty<CharacterInitialiser>("ItemNum_10"),
				BodyScaleHeadProperty = GetProperty<CharacterInitialiser>("BodyScaleHead"),
				BodyScaleBreastProperty = GetProperty<CharacterInitialiser>("BodyScaleBreast"),
				BodyScaleAbdomenProperty = GetProperty<CharacterInitialiser>("BodyScaleAbdomen"),
				BodyScaleArmProperty = GetProperty<CharacterInitialiser>("BodyScaleArm"),
				BodyScaleLegProperty = GetProperty<CharacterInitialiser>("BodyScaleLeg"),
				GestureId0Property = GetProperty<CharacterInitialiser>("GestureId0"),
				GestureId1Property = GetProperty<CharacterInitialiser>("GestureId1"),
				GestureId2Property = GetProperty<CharacterInitialiser>("GestureId2"),
				GestureId3Property = GetProperty<CharacterInitialiser>("GestureId3"),
				GestureId4Property = GetProperty<CharacterInitialiser>("GestureId4"),
				GestureId5Property = GetProperty<CharacterInitialiser>("GestureId5"),
				GestureId6Property = GetProperty<CharacterInitialiser>("GestureId6"),
				NpcPlayerTypeProperty = GetProperty<CharacterInitialiser>("NpcPlayerType"),
				NpcPlayerDrawTypeProperty = GetProperty<CharacterInitialiser>("NpcPlayerDrawType"),
				NpcPlayerSexProperty = GetProperty<CharacterInitialiser>("NpcPlayerSex"),
				VowTypeProperty = GetProperty<CharacterInitialiser>("VowType"),
				PadProperty = GetProperty<CharacterInitialiser>("Pad"),
				Pad0Property = GetProperty<CharacterInitialiser>("Pad0");

			/// <summary>MP recovery speed base value [s]</summary>
			/// <remarks>
			/// Japanese short name: "ＭＰ回復速度基本値[s]", Google translated: "MP recovery speed base value [s]".
			/// Japanese description: "ＭＰが、1ポイント回復するまでの時間（小数点第一位）", Google translated: "The MP, time to recover one point ( one point )".
			/// </remarks>
			[ParameterTableRowAttribute("baseRec_mp", index: 0, minimum: 0, maximum: 999, step: 0.1, sortOrder: 1100, unknown2: 1)]
			[DisplayName("MP recovery speed base value [s]")]
			[Description("The MP, time to recover one point ( one point )")]
			[DefaultValue((Single)0)]
			public Single BaseRec_mp {
				get { return baseRec_mp; }
				set {
					if ((double)value < 0 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for BaseRec_mp.");
					SetProperty(ref baseRec_mp, ref value, BaseRec_mpProperty);
				}
			}

			/// <summary>Stamina recovery rate base value [s]</summary>
			/// <remarks>
			/// Japanese short name: "スタミナ回復速度基本値[s]", Google translated: "Stamina recovery rate base value [s]".
			/// Japanese description: "スタミナが、1ポイント回復するまでの時間（小数点第一位）", Google translated: "Stamina , time to recover one point ( one point )".
			/// </remarks>
			[ParameterTableRowAttribute("baseRec_sp", index: 1, minimum: 0, maximum: 999, step: 0.1, sortOrder: 1300, unknown2: 1)]
			[DisplayName("Stamina recovery rate base value [s]")]
			[Description("Stamina , time to recover one point ( one point )")]
			[DefaultValue((Single)0)]
			public Single BaseRec_sp {
				get { return baseRec_sp; }
				set {
					if ((double)value < 0 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for BaseRec_sp.");
					SetProperty(ref baseRec_sp, ref value, BaseRec_spProperty);
				}
			}

			/// <summary>Fall damage reduction correction [ %]</summary>
			/// <remarks>
			/// Japanese short name: "落下ダメージ軽減補正[%]", Google translated: "Fall damage reduction correction [ %]".
			/// Japanese description: "他のキャラクターに上からのしかかれたときに、クッションとなりえるダメージ軽減量（％）（小数点第一位）", Google translated: "When it is him only from the top to the other characters , reducing the amount of damage can be a cushion (% ) ( one point )".
			/// </remarks>
			[ParameterTableRowAttribute("red_Falldam", index: 2, minimum: 0, maximum: 100, step: 0.1, sortOrder: 1400, unknown2: 1)]
			[DisplayName("Fall damage reduction correction [ %]")]
			[Description("When it is him only from the top to the other characters , reducing the amount of damage can be a cushion (% ) ( one point )")]
			[DefaultValue((Single)0)]
			public Single Red_Falldam {
				get { return red_Falldam; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for Red_Falldam.");
					SetProperty(ref red_Falldam, ref value, Red_FalldamProperty);
				}
			}

			/// <summary>Initial Seoul</summary>
			/// <remarks>
			/// Japanese short name: "初期ソウル", Google translated: "Initial Seoul".
			/// Japanese description: "初期に所持しているソウル量", Google translated: "Seoul amount you have possession in the early".
			/// </remarks>
			[ParameterTableRowAttribute("soul", index: 3, minimum: 0, maximum: 1E+07, step: 1, sortOrder: 1500, unknown2: 1)]
			[DisplayName("Initial Seoul")]
			[Description("Seoul amount you have possession in the early")]
			[DefaultValue((Int32)0)]
			public Int32 Soul {
				get { return soul; }
				set {
					if ((double)value < 0 || (double)value > 1E+07)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1E+07 for Soul.");
					SetProperty(ref soul, ref value, SoulProperty);
				}
			}

			/// <summary>Right hand weapon</summary>
			/// <remarks>
			/// Japanese short name: "右手武器", Google translated: "Right hand weapon".
			/// Japanese description: "装備品パラメータの武器ＩＤ(右手)", Google translated: "ID of weapon equipment parameters ( right hand )".
			/// </remarks>
			[ParameterTableRowAttribute("equip_Wep_Right", index: 4, minimum: -1, maximum: 1E+09, step: 1, sortOrder: 1600, unknown2: 1)]
			[DisplayName("Right hand weapon")]
			[Description("ID of weapon equipment parameters ( right hand )")]
			[DefaultValue((Int32)(-1))]
			public Int32 Equip_Wep_Right {
				get { return equip_Wep_Right; }
				set {
					if ((double)value < -1 || (double)value > 1E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+09 for Equip_Wep_Right.");
					SetProperty(ref equip_Wep_Right, ref value, Equip_Wep_RightProperty);
				}
			}

			/// <summary>Right hand spare weapon</summary>
			/// <remarks>
			/// Japanese short name: "右手予備武器", Google translated: "Right hand spare weapon".
			/// Japanese description: "装備品パラメータの武器ＩＤ(右手予備)", Google translated: "ID of weapon equipment parameters ( right hand spare )".
			/// </remarks>
			[ParameterTableRowAttribute("equip_Subwep_Right", index: 5, minimum: -1, maximum: 1E+09, step: 1, sortOrder: 1700, unknown2: 1)]
			[DisplayName("Right hand spare weapon")]
			[Description("ID of weapon equipment parameters ( right hand spare )")]
			[DefaultValue((Int32)(-1))]
			public Int32 Equip_Subwep_Right {
				get { return equip_Subwep_Right; }
				set {
					if ((double)value < -1 || (double)value > 1E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+09 for Equip_Subwep_Right.");
					SetProperty(ref equip_Subwep_Right, ref value, Equip_Subwep_RightProperty);
				}
			}

			/// <summary>Left hand weapon</summary>
			/// <remarks>
			/// Japanese short name: "左手武器", Google translated: "Left hand weapon".
			/// Japanese description: "装備品パラメータの武器ＩＤ(左手)", Google translated: "ID of weapon equipment parameters ( left hand )".
			/// </remarks>
			[ParameterTableRowAttribute("equip_Wep_Left", index: 6, minimum: -1, maximum: 1E+09, step: 1, sortOrder: 1800, unknown2: 1)]
			[DisplayName("Left hand weapon")]
			[Description("ID of weapon equipment parameters ( left hand )")]
			[DefaultValue((Int32)(-1))]
			public Int32 Equip_Wep_Left {
				get { return equip_Wep_Left; }
				set {
					if ((double)value < -1 || (double)value > 1E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+09 for Equip_Wep_Left.");
					SetProperty(ref equip_Wep_Left, ref value, Equip_Wep_LeftProperty);
				}
			}

			/// <summary>Left hand spare weapon</summary>
			/// <remarks>
			/// Japanese short name: "左手予備武器", Google translated: "Left hand spare weapon".
			/// Japanese description: "装備品パラメータの武器ＩＤ(左手予備)", Google translated: "ID of weapon equipment parameters ( left hand spare )".
			/// </remarks>
			[ParameterTableRowAttribute("equip_Subwep_Left", index: 7, minimum: -1, maximum: 1E+09, step: 1, sortOrder: 1900, unknown2: 1)]
			[DisplayName("Left hand spare weapon")]
			[Description("ID of weapon equipment parameters ( left hand spare )")]
			[DefaultValue((Int32)(-1))]
			public Int32 Equip_Subwep_Left {
				get { return equip_Subwep_Left; }
				set {
					if ((double)value < -1 || (double)value > 1E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+09 for Equip_Subwep_Left.");
					SetProperty(ref equip_Subwep_Left, ref value, Equip_Subwep_LeftProperty);
				}
			}

			/// <summary>Head armor</summary>
			/// <remarks>
			/// Japanese short name: "頭防具", Google translated: "Head armor".
			/// Japanese description: "装備品パラメータの防具ＩＤ(頭防具)", Google translated: "Armor ID of equipment parameters ( head armor )".
			/// </remarks>
			[ParameterTableRowAttribute("equip_Helm", index: 8, minimum: -1, maximum: 1E+09, step: 1, sortOrder: 2000, unknown2: 1)]
			[DisplayName("Head armor")]
			[Description("Armor ID of equipment parameters ( head armor )")]
			[DefaultValue((Int32)(-1))]
			public Int32 Equip_Helm {
				get { return equip_Helm; }
				set {
					if ((double)value < -1 || (double)value > 1E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+09 for Equip_Helm.");
					SetProperty(ref equip_Helm, ref value, Equip_HelmProperty);
				}
			}

			/// <summary>Body armor</summary>
			/// <remarks>
			/// Japanese short name: "胴体防具", Google translated: "Body armor".
			/// Japanese description: "装備品パラメータの防具ＩＤ(胴体防具)", Google translated: "Armor ID of equipment parameters ( body armor )".
			/// </remarks>
			[ParameterTableRowAttribute("equip_Armer", index: 9, minimum: -1, maximum: 1E+09, step: 1, sortOrder: 2100, unknown2: 1)]
			[DisplayName("Body armor")]
			[Description("Armor ID of equipment parameters ( body armor )")]
			[DefaultValue((Int32)(-1))]
			public Int32 Equip_Armer {
				get { return equip_Armer; }
				set {
					if ((double)value < -1 || (double)value > 1E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+09 for Equip_Armer.");
					SetProperty(ref equip_Armer, ref value, Equip_ArmerProperty);
				}
			}

			/// <summary>Arm armor</summary>
			/// <remarks>
			/// Japanese short name: "腕防具", Google translated: "Arm armor".
			/// Japanese description: "装備品パラメータの防具ＩＤ(腕防具)", Google translated: "Armor ID of equipment parameters ( arm armor )".
			/// </remarks>
			[ParameterTableRowAttribute("equip_Gaunt", index: 10, minimum: -1, maximum: 1E+09, step: 1, sortOrder: 2200, unknown2: 1)]
			[DisplayName("Arm armor")]
			[Description("Armor ID of equipment parameters ( arm armor )")]
			[DefaultValue((Int32)(-1))]
			public Int32 Equip_Gaunt {
				get { return equip_Gaunt; }
				set {
					if ((double)value < -1 || (double)value > 1E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+09 for Equip_Gaunt.");
					SetProperty(ref equip_Gaunt, ref value, Equip_GauntProperty);
				}
			}

			/// <summary>Leg armor</summary>
			/// <remarks>
			/// Japanese short name: "脚防具", Google translated: "Leg armor".
			/// Japanese description: "装備品パラメータの防具ＩＤ(脚防具)", Google translated: "Armor ID of equipment parameters ( leg armor )".
			/// </remarks>
			[ParameterTableRowAttribute("equip_Leg", index: 11, minimum: -1, maximum: 1E+09, step: 1, sortOrder: 2300, unknown2: 1)]
			[DisplayName("Leg armor")]
			[Description("Armor ID of equipment parameters ( leg armor )")]
			[DefaultValue((Int32)(-1))]
			public Int32 Equip_Leg {
				get { return equip_Leg; }
				set {
					if ((double)value < -1 || (double)value > 1E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+09 for Equip_Leg.");
					SetProperty(ref equip_Leg, ref value, Equip_LegProperty);
				}
			}

			/// <summary>Arrow</summary>
			/// <remarks>
			/// Japanese short name: "矢", Google translated: "Arrow".
			/// Japanese description: "装備品パラメータの武器ＩＤ(矢)", Google translated: "ID of weapon equipment parameters ( arrow )".
			/// </remarks>
			[ParameterTableRowAttribute("equip_Arrow", index: 12, minimum: -1, maximum: 1E+09, step: 1, sortOrder: 2400, unknown2: 1)]
			[DisplayName("Arrow")]
			[Description("ID of weapon equipment parameters ( arrow )")]
			[DefaultValue((Int32)(-1))]
			public Int32 Equip_Arrow {
				get { return equip_Arrow; }
				set {
					if ((double)value < -1 || (double)value > 1E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+09 for Equip_Arrow.");
					SetProperty(ref equip_Arrow, ref value, Equip_ArrowProperty);
				}
			}

			/// <summary>Bolt</summary>
			/// <remarks>
			/// Japanese short name: "ボルト", Google translated: "Bolt".
			/// Japanese description: "装備品パラメータの武器ＩＤ(ボルト)", Google translated: "ID of weapon equipment parameters ( volts)".
			/// </remarks>
			[ParameterTableRowAttribute("equip_Bolt", index: 13, minimum: -1, maximum: 1E+09, step: 1, sortOrder: 2600, unknown2: 1)]
			[DisplayName("Bolt")]
			[Description("ID of weapon equipment parameters ( volts)")]
			[DefaultValue((Int32)(-1))]
			public Int32 Equip_Bolt {
				get { return equip_Bolt; }
				set {
					if ((double)value < -1 || (double)value > 1E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+09 for Equip_Bolt.");
					SetProperty(ref equip_Bolt, ref value, Equip_BoltProperty);
				}
			}

			/// <summary>Preliminary arrow</summary>
			/// <remarks>
			/// Japanese short name: "予備矢", Google translated: "Preliminary arrow".
			/// Japanese description: "装備品パラメータの武器ＩＤ(矢予備)", Google translated: "ID of weapon equipment parameters ( arrow preliminary )".
			/// </remarks>
			[ParameterTableRowAttribute("equip_SubArrow", index: 14, minimum: -1, maximum: 1E+09, step: 1, sortOrder: 2500, unknown2: 1)]
			[DisplayName("Preliminary arrow")]
			[Description("ID of weapon equipment parameters ( arrow preliminary )")]
			[DefaultValue((Int32)(-1))]
			public Int32 Equip_SubArrow {
				get { return equip_SubArrow; }
				set {
					if ((double)value < -1 || (double)value > 1E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+09 for Equip_SubArrow.");
					SetProperty(ref equip_SubArrow, ref value, Equip_SubArrowProperty);
				}
			}

			/// <summary>Spare bolt</summary>
			/// <remarks>
			/// Japanese short name: "予備ボルト", Google translated: "Spare bolt".
			/// Japanese description: "装備品パラメータの武器ＩＤ(ボルト予備)", Google translated: "ID of weapon equipment parameters (volt spare )".
			/// </remarks>
			[ParameterTableRowAttribute("equip_SubBolt", index: 15, minimum: -1, maximum: 1E+09, step: 1, sortOrder: 2700, unknown2: 1)]
			[DisplayName("Spare bolt")]
			[Description("ID of weapon equipment parameters (volt spare )")]
			[DefaultValue((Int32)(-1))]
			public Int32 Equip_SubBolt {
				get { return equip_SubBolt; }
				set {
					if ((double)value < -1 || (double)value > 1E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+09 for Equip_SubBolt.");
					SetProperty(ref equip_SubBolt, ref value, Equip_SubBoltProperty);
				}
			}

			/// <summary>Ornaments 1</summary>
			/// <remarks>
			/// Japanese short name: "装飾品1", Google translated: "Ornaments 1".
			/// Japanese description: "装備品パラメータの装飾品ＩＤ01", Google translated: "Ornaments ID01 of equipment parameters".
			/// </remarks>
			[ParameterTableRowAttribute("equip_Accessory01", index: 16, minimum: -1, maximum: 999999, step: 1, sortOrder: 2800, unknown2: 1)]
			[DisplayName("Ornaments 1")]
			[Description("Ornaments ID01 of equipment parameters")]
			[DefaultValue((Int32)(-1))]
			public Int32 Equip_Accessory01 {
				get { return equip_Accessory01; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for Equip_Accessory01.");
					SetProperty(ref equip_Accessory01, ref value, Equip_Accessory01Property);
				}
			}

			/// <summary>Ornaments 2</summary>
			/// <remarks>
			/// Japanese short name: "装飾品2", Google translated: "Ornaments 2".
			/// Japanese description: "装備品パラメータの装飾品ＩＤ02", Google translated: "Ornaments ID02 of equipment parameters".
			/// </remarks>
			[ParameterTableRowAttribute("equip_Accessory02", index: 17, minimum: -1, maximum: 999999, step: 1, sortOrder: 2900, unknown2: 1)]
			[DisplayName("Ornaments 2")]
			[Description("Ornaments ID02 of equipment parameters")]
			[DefaultValue((Int32)(-1))]
			public Int32 Equip_Accessory02 {
				get { return equip_Accessory02; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for Equip_Accessory02.");
					SetProperty(ref equip_Accessory02, ref value, Equip_Accessory02Property);
				}
			}

			/// <summary>Ornaments 3</summary>
			/// <remarks>
			/// Japanese short name: "装飾品3", Google translated: "Ornaments 3".
			/// Japanese description: "装備品パラメータの装飾品ＩＤ03", Google translated: "Ornaments ID03 of equipment parameters".
			/// </remarks>
			[ParameterTableRowAttribute("equip_Accessory03", index: 18, minimum: -1, maximum: 999999, step: 1, sortOrder: 3000, unknown2: 1)]
			[DisplayName("Ornaments 3")]
			[Description("Ornaments ID03 of equipment parameters")]
			[DefaultValue((Int32)(-1))]
			public Int32 Equip_Accessory03 {
				get { return equip_Accessory03; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for Equip_Accessory03.");
					SetProperty(ref equip_Accessory03, ref value, Equip_Accessory03Property);
				}
			}

			/// <summary>Ornaments 4</summary>
			/// <remarks>
			/// Japanese short name: "装飾品4", Google translated: "Ornaments 4".
			/// Japanese description: "装備品パラメータの装飾品ＩＤ04", Google translated: "Ornaments ID04 of equipment parameters".
			/// </remarks>
			[ParameterTableRowAttribute("equip_Accessory04", index: 19, minimum: -1, maximum: 999999, step: 1, sortOrder: 3100, unknown2: 1)]
			[DisplayName("Ornaments 4")]
			[Description("Ornaments ID04 of equipment parameters")]
			[DefaultValue((Int32)(-1))]
			public Int32 Equip_Accessory04 {
				get { return equip_Accessory04; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for Equip_Accessory04.");
					SetProperty(ref equip_Accessory04, ref value, Equip_Accessory04Property);
				}
			}

			/// <summary>Ornaments 5</summary>
			/// <remarks>
			/// Japanese short name: "装飾品5", Google translated: "Ornaments 5".
			/// Japanese description: "装備品パラメータの装飾品ＩＤ05", Google translated: "Ornaments ID05 of equipment parameters".
			/// </remarks>
			[ParameterTableRowAttribute("equip_Accessory05", index: 20, minimum: -1, maximum: 999999, step: 1, sortOrder: 3200, unknown2: 1)]
			[DisplayName("Ornaments 5")]
			[Description("Ornaments ID05 of equipment parameters")]
			[DefaultValue((Int32)(-1))]
			public Int32 Equip_Accessory05 {
				get { return equip_Accessory05; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for Equip_Accessory05.");
					SetProperty(ref equip_Accessory05, ref value, Equip_Accessory05Property);
				}
			}

			/// <summary>Skill 1</summary>
			/// <remarks>
			/// Japanese short name: "スキル1", Google translated: "Skill 1".
			/// Japanese description: "初期装備のスキルＩD01", Google translated: "ID01 skill of initial equipment".
			/// </remarks>
			[ParameterTableRowAttribute("equip_Skill_01", index: 21, minimum: -1, maximum: 999999, step: 1, sortOrder: 3300, unknown2: 1)]
			[DisplayName("Skill 1")]
			[Description("ID01 skill of initial equipment")]
			[DefaultValue((Int32)(-1))]
			public Int32 Equip_Skill_01 {
				get { return equip_Skill_01; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for Equip_Skill_01.");
					SetProperty(ref equip_Skill_01, ref value, Equip_Skill_01Property);
				}
			}

			/// <summary>Skill 2</summary>
			/// <remarks>
			/// Japanese short name: "スキル2", Google translated: "Skill 2".
			/// Japanese description: "初期装備のスキルＩD02", Google translated: "ID02 skill of initial equipment".
			/// </remarks>
			[ParameterTableRowAttribute("equip_Skill_02", index: 22, minimum: -1, maximum: 999999, step: 1, sortOrder: 3400, unknown2: 1)]
			[DisplayName("Skill 2")]
			[Description("ID02 skill of initial equipment")]
			[DefaultValue((Int32)(-1))]
			public Int32 Equip_Skill_02 {
				get { return equip_Skill_02; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for Equip_Skill_02.");
					SetProperty(ref equip_Skill_02, ref value, Equip_Skill_02Property);
				}
			}

			/// <summary>Skill 3</summary>
			/// <remarks>
			/// Japanese short name: "スキル3", Google translated: "Skill 3".
			/// Japanese description: "初期装備のスキルＩD03", Google translated: "ID03 skill of initial equipment".
			/// </remarks>
			[ParameterTableRowAttribute("equip_Skill_03", index: 23, minimum: -1, maximum: 999999, step: 1, sortOrder: 3500, unknown2: 1)]
			[DisplayName("Skill 3")]
			[Description("ID03 skill of initial equipment")]
			[DefaultValue((Int32)(-1))]
			public Int32 Equip_Skill_03 {
				get { return equip_Skill_03; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for Equip_Skill_03.");
					SetProperty(ref equip_Skill_03, ref value, Equip_Skill_03Property);
				}
			}

			/// <summary>Magic or miracle 1</summary>
			/// <remarks>
			/// Japanese short name: "魔法・奇跡1", Google translated: "Magic or miracle 1".
			/// Japanese description: "初期配置の魔法・奇跡ID01", Google translated: "Magic, miracle ID01 of initial placement".
			/// </remarks>
			[ParameterTableRowAttribute("equip_Spell_01", index: 24, minimum: -1, maximum: 999999, step: 1, sortOrder: 3600, unknown2: 1)]
			[DisplayName("Magic or miracle 1")]
			[Description("Magic, miracle ID01 of initial placement")]
			[DefaultValue((Int32)(-1))]
			public Int32 Equip_Spell_01 {
				get { return equip_Spell_01; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for Equip_Spell_01.");
					SetProperty(ref equip_Spell_01, ref value, Equip_Spell_01Property);
				}
			}

			/// <summary>Magic, miracle 2</summary>
			/// <remarks>
			/// Japanese short name: "魔法・奇跡2", Google translated: "Magic, miracle 2".
			/// Japanese description: "初期配置の魔法・奇跡ID02", Google translated: "Magic, miracle ID02 of initial placement".
			/// </remarks>
			[ParameterTableRowAttribute("equip_Spell_02", index: 25, minimum: -1, maximum: 999999, step: 1, sortOrder: 3700, unknown2: 1)]
			[DisplayName("Magic, miracle 2")]
			[Description("Magic, miracle ID02 of initial placement")]
			[DefaultValue((Int32)(-1))]
			public Int32 Equip_Spell_02 {
				get { return equip_Spell_02; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for Equip_Spell_02.");
					SetProperty(ref equip_Spell_02, ref value, Equip_Spell_02Property);
				}
			}

			/// <summary>Magic, miracle 3</summary>
			/// <remarks>
			/// Japanese short name: "魔法・奇跡3", Google translated: "Magic, miracle 3".
			/// Japanese description: "初期配置の魔法・奇跡ID03", Google translated: "Magic, miracle ID03 of initial placement".
			/// </remarks>
			[ParameterTableRowAttribute("equip_Spell_03", index: 26, minimum: -1, maximum: 999999, step: 1, sortOrder: 3800, unknown2: 1)]
			[DisplayName("Magic, miracle 3")]
			[Description("Magic, miracle ID03 of initial placement")]
			[DefaultValue((Int32)(-1))]
			public Int32 Equip_Spell_03 {
				get { return equip_Spell_03; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for Equip_Spell_03.");
					SetProperty(ref equip_Spell_03, ref value, Equip_Spell_03Property);
				}
			}

			/// <summary>Magic and miracles 4</summary>
			/// <remarks>
			/// Japanese short name: "魔法・奇跡4", Google translated: "Magic and miracles 4".
			/// Japanese description: "初期配置の魔法・奇跡ID04", Google translated: "Magic, miracle ID04 of initial placement".
			/// </remarks>
			[ParameterTableRowAttribute("equip_Spell_04", index: 27, minimum: -1, maximum: 999999, step: 1, sortOrder: 3900, unknown2: 1)]
			[DisplayName("Magic and miracles 4")]
			[Description("Magic, miracle ID04 of initial placement")]
			[DefaultValue((Int32)(-1))]
			public Int32 Equip_Spell_04 {
				get { return equip_Spell_04; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for Equip_Spell_04.");
					SetProperty(ref equip_Spell_04, ref value, Equip_Spell_04Property);
				}
			}

			/// <summary>Magic, miracle 5</summary>
			/// <remarks>
			/// Japanese short name: "魔法・奇跡5", Google translated: "Magic, miracle 5".
			/// Japanese description: "初期配置の魔法・奇跡ID05", Google translated: "Magic, miracle ID05 of initial placement".
			/// </remarks>
			[ParameterTableRowAttribute("equip_Spell_05", index: 28, minimum: -1, maximum: 999999, step: 1, sortOrder: 4000, unknown2: 1)]
			[DisplayName("Magic, miracle 5")]
			[Description("Magic, miracle ID05 of initial placement")]
			[DefaultValue((Int32)(-1))]
			public Int32 Equip_Spell_05 {
				get { return equip_Spell_05; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for Equip_Spell_05.");
					SetProperty(ref equip_Spell_05, ref value, Equip_Spell_05Property);
				}
			}

			/// <summary>Magic or miracle 6</summary>
			/// <remarks>
			/// Japanese short name: "魔法・奇跡6", Google translated: "Magic or miracle 6".
			/// Japanese description: "初期配置の魔法・奇跡ID06", Google translated: "Magic, miracle ID06 of initial placement".
			/// </remarks>
			[ParameterTableRowAttribute("equip_Spell_06", index: 29, minimum: -1, maximum: 999999, step: 1, sortOrder: 4100, unknown2: 1)]
			[DisplayName("Magic or miracle 6")]
			[Description("Magic, miracle ID06 of initial placement")]
			[DefaultValue((Int32)(-1))]
			public Int32 Equip_Spell_06 {
				get { return equip_Spell_06; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for Equip_Spell_06.");
					SetProperty(ref equip_Spell_06, ref value, Equip_Spell_06Property);
				}
			}

			/// <summary>Magic or miracle 7</summary>
			/// <remarks>
			/// Japanese short name: "魔法・奇跡7", Google translated: "Magic or miracle 7".
			/// Japanese description: "初期配置の魔法・奇跡ID07", Google translated: "Magic, miracle ID07 of initial placement".
			/// </remarks>
			[ParameterTableRowAttribute("equip_Spell_07", index: 30, minimum: -1, maximum: 999999, step: 1, sortOrder: 4200, unknown2: 1)]
			[DisplayName("Magic or miracle 7")]
			[Description("Magic, miracle ID07 of initial placement")]
			[DefaultValue((Int32)(-1))]
			public Int32 Equip_Spell_07 {
				get { return equip_Spell_07; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for Equip_Spell_07.");
					SetProperty(ref equip_Spell_07, ref value, Equip_Spell_07Property);
				}
			}

			/// <summary>Item 01</summary>
			/// <remarks>
			/// Japanese short name: "アイテム01", Google translated: "Item 01".
			/// Japanese description: "初期所持のアイテムID01", Google translated: "Item ID01 of initial possession".
			/// </remarks>
			[ParameterTableRowAttribute("item_01", index: 31, minimum: -1, maximum: 1E+09, step: 1, sortOrder: 4600, unknown2: 1)]
			[DisplayName("Item 01")]
			[Description("Item ID01 of initial possession")]
			[DefaultValue((Int32)(-1))]
			public Int32 Item_01 {
				get { return item_01; }
				set {
					if ((double)value < -1 || (double)value > 1E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+09 for Item_01.");
					SetProperty(ref item_01, ref value, Item_01Property);
				}
			}

			/// <summary>Item 02</summary>
			/// <remarks>
			/// Japanese short name: "アイテム02", Google translated: "Item 02".
			/// Japanese description: "初期所持のアイテムID02", Google translated: "Item ID02 of initial possession".
			/// </remarks>
			[ParameterTableRowAttribute("item_02", index: 32, minimum: -1, maximum: 1E+09, step: 1, sortOrder: 4800, unknown2: 1)]
			[DisplayName("Item 02")]
			[Description("Item ID02 of initial possession")]
			[DefaultValue((Int32)(-1))]
			public Int32 Item_02 {
				get { return item_02; }
				set {
					if ((double)value < -1 || (double)value > 1E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+09 for Item_02.");
					SetProperty(ref item_02, ref value, Item_02Property);
				}
			}

			/// <summary>Item 03</summary>
			/// <remarks>
			/// Japanese short name: "アイテム03", Google translated: "Item 03".
			/// Japanese description: "初期所持のアイテムID03", Google translated: "Item ID03 of initial possession".
			/// </remarks>
			[ParameterTableRowAttribute("item_03", index: 33, minimum: -1, maximum: 1E+09, step: 1, sortOrder: 5000, unknown2: 1)]
			[DisplayName("Item 03")]
			[Description("Item ID03 of initial possession")]
			[DefaultValue((Int32)(-1))]
			public Int32 Item_03 {
				get { return item_03; }
				set {
					if ((double)value < -1 || (double)value > 1E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+09 for Item_03.");
					SetProperty(ref item_03, ref value, Item_03Property);
				}
			}

			/// <summary>Item 04</summary>
			/// <remarks>
			/// Japanese short name: "アイテム04", Google translated: "Item 04".
			/// Japanese description: "初期所持のアイテムID04", Google translated: "Item ID04 of initial possession".
			/// </remarks>
			[ParameterTableRowAttribute("item_04", index: 34, minimum: -1, maximum: 1E+09, step: 1, sortOrder: 5200, unknown2: 1)]
			[DisplayName("Item 04")]
			[Description("Item ID04 of initial possession")]
			[DefaultValue((Int32)(-1))]
			public Int32 Item_04 {
				get { return item_04; }
				set {
					if ((double)value < -1 || (double)value > 1E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+09 for Item_04.");
					SetProperty(ref item_04, ref value, Item_04Property);
				}
			}

			/// <summary>Item 05</summary>
			/// <remarks>
			/// Japanese short name: "アイテム05", Google translated: "Item 05".
			/// Japanese description: "初期所持のアイテムID05", Google translated: "Item ID05 of initial possession".
			/// </remarks>
			[ParameterTableRowAttribute("item_05", index: 35, minimum: -1, maximum: 1E+09, step: 1, sortOrder: 5400, unknown2: 1)]
			[DisplayName("Item 05")]
			[Description("Item ID05 of initial possession")]
			[DefaultValue((Int32)(-1))]
			public Int32 Item_05 {
				get { return item_05; }
				set {
					if ((double)value < -1 || (double)value > 1E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+09 for Item_05.");
					SetProperty(ref item_05, ref value, Item_05Property);
				}
			}

			/// <summary>Item 06</summary>
			/// <remarks>
			/// Japanese short name: "アイテム06", Google translated: "Item 06".
			/// Japanese description: "初期所持のアイテムID06", Google translated: "Item ID06 of initial possession".
			/// </remarks>
			[ParameterTableRowAttribute("item_06", index: 36, minimum: -1, maximum: 1E+09, step: 1, sortOrder: 5600, unknown2: 1)]
			[DisplayName("Item 06")]
			[Description("Item ID06 of initial possession")]
			[DefaultValue((Int32)(-1))]
			public Int32 Item_06 {
				get { return item_06; }
				set {
					if ((double)value < -1 || (double)value > 1E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+09 for Item_06.");
					SetProperty(ref item_06, ref value, Item_06Property);
				}
			}

			/// <summary>Item 07</summary>
			/// <remarks>
			/// Japanese short name: "アイテム07", Google translated: "Item 07".
			/// Japanese description: "初期所持のアイテムID07", Google translated: "Item ID07 of initial possession".
			/// </remarks>
			[ParameterTableRowAttribute("item_07", index: 37, minimum: -1, maximum: 1E+09, step: 1, sortOrder: 5800, unknown2: 1)]
			[DisplayName("Item 07")]
			[Description("Item ID07 of initial possession")]
			[DefaultValue((Int32)(-1))]
			public Int32 Item_07 {
				get { return item_07; }
				set {
					if ((double)value < -1 || (double)value > 1E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+09 for Item_07.");
					SetProperty(ref item_07, ref value, Item_07Property);
				}
			}

			/// <summary>Item 08</summary>
			/// <remarks>
			/// Japanese short name: "アイテム08", Google translated: "Item 08".
			/// Japanese description: "初期所持のアイテムID08", Google translated: "Item ID08 of initial possession".
			/// </remarks>
			[ParameterTableRowAttribute("item_08", index: 38, minimum: -1, maximum: 1E+09, step: 1, sortOrder: 6000, unknown2: 1)]
			[DisplayName("Item 08")]
			[Description("Item ID08 of initial possession")]
			[DefaultValue((Int32)(-1))]
			public Int32 Item_08 {
				get { return item_08; }
				set {
					if ((double)value < -1 || (double)value > 1E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+09 for Item_08.");
					SetProperty(ref item_08, ref value, Item_08Property);
				}
			}

			/// <summary>Item 09</summary>
			/// <remarks>
			/// Japanese short name: "アイテム09", Google translated: "Item 09".
			/// Japanese description: "初期所持のアイテムID09", Google translated: "Item ID09 of initial possession".
			/// </remarks>
			[ParameterTableRowAttribute("item_09", index: 39, minimum: -1, maximum: 1E+09, step: 1, sortOrder: 6200, unknown2: 1)]
			[DisplayName("Item 09")]
			[Description("Item ID09 of initial possession")]
			[DefaultValue((Int32)(-1))]
			public Int32 Item_09 {
				get { return item_09; }
				set {
					if ((double)value < -1 || (double)value > 1E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+09 for Item_09.");
					SetProperty(ref item_09, ref value, Item_09Property);
				}
			}

			/// <summary>Item 10</summary>
			/// <remarks>
			/// Japanese short name: "アイテム10", Google translated: "Item 10".
			/// Japanese description: "初期所持のアイテムID10", Google translated: "Item ID10 of initial possession".
			/// </remarks>
			[ParameterTableRowAttribute("item_10", index: 40, minimum: -1, maximum: 1E+09, step: 1, sortOrder: 6400, unknown2: 1)]
			[DisplayName("Item 10")]
			[Description("Item ID10 of initial possession")]
			[DefaultValue((Int32)(-1))]
			public Int32 Item_10 {
				get { return item_10; }
				set {
					if ((double)value < -1 || (double)value > 1E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+09 for Item_10.");
					SetProperty(ref item_10, ref value, Item_10Property);
				}
			}

			/// <summary>Face Jen parameter ID</summary>
			/// <remarks>
			/// Japanese short name: "フェイスジェンパラメータID", Google translated: "Face Jen parameter ID".
			/// Japanese description: "NPCプレイヤーで使用するフェイスジェンパラメータID。通常プレイヤーでは使用しません。", Google translated: "Face Jen parameter ID to be used in the NPC player . It is not used in the normal player .".
			/// </remarks>
			[ParameterTableRowAttribute("npcPlayerFaceGenId", index: 41, minimum: -1, maximum: 999999, step: 1, sortOrder: 6600, unknown2: 1)]
			[DisplayName("Face Jen parameter ID")]
			[Description("Face Jen parameter ID to be used in the NPC player . It is not used in the normal player .")]
			[DefaultValue((Int32)0)]
			public Int32 NpcPlayerFaceGenId {
				get { return npcPlayerFaceGenId; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for NpcPlayerFaceGenId.");
					SetProperty(ref npcPlayerFaceGenId, ref value, NpcPlayerFaceGenIdProperty);
				}
			}

			/// <summary>Thinking ID of NPC player</summary>
			/// <remarks>
			/// Japanese short name: "NPCプレイヤーの思考ID", Google translated: "Thinking ID of NPC player".
			/// Japanese description: "NPCプレイヤーで使用するNPC思考パラメータID。通常プレイヤーでは使用しません。", Google translated: "NPC thinking parameter ID to be used in the NPC player . It is not used in the normal player .".
			/// </remarks>
			[ParameterTableRowAttribute("npcPlayerThinkId", index: 42, minimum: -1, maximum: 999999, step: 1, sortOrder: 6700, unknown2: 1)]
			[DisplayName("Thinking ID of NPC player")]
			[Description("NPC thinking parameter ID to be used in the NPC player . It is not used in the normal player .")]
			[DefaultValue((Int32)0)]
			public Int32 NpcPlayerThinkId {
				get { return npcPlayerThinkId; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for NpcPlayerThinkId.");
					SetProperty(ref npcPlayerThinkId, ref value, NpcPlayerThinkIdProperty);
				}
			}

			/// <summary>HP base value</summary>
			/// <remarks>
			/// Japanese short name: "ＨＰ基本値", Google translated: "HP base value".
			/// Japanese description: "ＨＰの基本値（実際は、計算式で補正される）", Google translated: "Basic value of HP ( in fact , it is corrected by the formula )".
			/// </remarks>
			[ParameterTableRowAttribute("baseHp", index: 43, minimum: 0, maximum: 999, step: 1, sortOrder: 900, unknown2: 1)]
			[DisplayName("HP base value")]
			[Description("Basic value of HP ( in fact , it is corrected by the formula )")]
			[DefaultValue((UInt16)0)]
			public UInt16 BaseHp {
				get { return baseHp; }
				set {
					if ((double)value < 0 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for BaseHp.");
					SetProperty(ref baseHp, ref value, BaseHpProperty);
				}
			}

			/// <summary>MP base value</summary>
			/// <remarks>
			/// Japanese short name: "ＭＰ基本値", Google translated: "MP base value".
			/// Japanese description: "ＭＰの基本値（実際は、計算式で補正される）", Google translated: "Base value of MP ( in fact , it is corrected by the formula )".
			/// </remarks>
			[ParameterTableRowAttribute("baseMp", index: 44, minimum: 0, maximum: 999, step: 1, sortOrder: 1000, unknown2: 1)]
			[DisplayName("MP base value")]
			[Description("Base value of MP ( in fact , it is corrected by the formula )")]
			[DefaultValue((UInt16)0)]
			public UInt16 BaseMp {
				get { return baseMp; }
				set {
					if ((double)value < 0 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for BaseMp.");
					SetProperty(ref baseMp, ref value, BaseMpProperty);
				}
			}

			/// <summary>Stamina base value</summary>
			/// <remarks>
			/// Japanese short name: "スタミナ基本値", Google translated: "Stamina base value".
			/// Japanese description: "スタミナの基本値（実際は、計算式で補正される）", Google translated: "Basic value stamina ( in fact , it is corrected by the formula )".
			/// </remarks>
			[ParameterTableRowAttribute("baseSp", index: 45, minimum: 0, maximum: 999, step: 1, sortOrder: 1200, unknown2: 1)]
			[DisplayName("Stamina base value")]
			[Description("Basic value stamina ( in fact , it is corrected by the formula )")]
			[DefaultValue((UInt16)0)]
			public UInt16 BaseSp {
				get { return baseSp; }
				set {
					if ((double)value < 0 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for BaseSp.");
					SetProperty(ref baseSp, ref value, BaseSpProperty);
				}
			}

			/// <summary>The number of possession arrow</summary>
			/// <remarks>
			/// Japanese short name: "矢の所持数", Google translated: "The number of possession arrow".
			/// Japanese description: "矢の初期所持数", Google translated: "Initial number of possession arrow".
			/// </remarks>
			[ParameterTableRowAttribute("arrowNum", index: 46, minimum: 0, maximum: 999, step: 1, sortOrder: 2450, unknown2: 1)]
			[DisplayName("The number of possession arrow")]
			[Description("Initial number of possession arrow")]
			[DefaultValue((UInt16)0)]
			public UInt16 ArrowNum {
				get { return arrowNum; }
				set {
					if ((double)value < 0 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for ArrowNum.");
					SetProperty(ref arrowNum, ref value, ArrowNumProperty);
				}
			}

			/// <summary>Possession number of bolt</summary>
			/// <remarks>
			/// Japanese short name: "ボルトの所持数", Google translated: "Possession number of bolt".
			/// Japanese description: "ボルトの初期所持数", Google translated: "Initial possession number of bolt".
			/// </remarks>
			[ParameterTableRowAttribute("boltNum", index: 47, minimum: 0, maximum: 999, step: 1, sortOrder: 2650, unknown2: 1)]
			[DisplayName("Possession number of bolt")]
			[Description("Initial possession number of bolt")]
			[DefaultValue((UInt16)0)]
			public UInt16 BoltNum {
				get { return boltNum; }
				set {
					if ((double)value < 0 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for BoltNum.");
					SetProperty(ref boltNum, ref value, BoltNumProperty);
				}
			}

			/// <summary>Number of preliminary possession arrow</summary>
			/// <remarks>
			/// Japanese short name: "予備矢の所持数", Google translated: "Number of preliminary possession arrow".
			/// Japanese description: "矢の初期所持数", Google translated: "Initial number of possession arrow".
			/// </remarks>
			[ParameterTableRowAttribute("subArrowNum", index: 48, minimum: 0, maximum: 999, step: 1, sortOrder: 2550, unknown2: 1)]
			[DisplayName("Number of preliminary possession arrow")]
			[Description("Initial number of possession arrow")]
			[DefaultValue((UInt16)0)]
			public UInt16 SubArrowNum {
				get { return subArrowNum; }
				set {
					if ((double)value < 0 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for SubArrowNum.");
					SetProperty(ref subArrowNum, ref value, SubArrowNumProperty);
				}
			}

			/// <summary>Possession number of spare bolt</summary>
			/// <remarks>
			/// Japanese short name: "予備ボルトの所持数", Google translated: "Possession number of spare bolt".
			/// Japanese description: "ボルトの初期所持数", Google translated: "Initial possession number of bolt".
			/// </remarks>
			[ParameterTableRowAttribute("subBoltNum", index: 49, minimum: 0, maximum: 999, step: 1, sortOrder: 2750, unknown2: 1)]
			[DisplayName("Possession number of spare bolt")]
			[Description("Initial possession number of bolt")]
			[DefaultValue((UInt16)0)]
			public UInt16 SubBoltNum {
				get { return subBoltNum; }
				set {
					if ((double)value < 0 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for SubBoltNum.");
					SetProperty(ref subBoltNum, ref value, SubBoltNumProperty);
				}
			}

			/// <summary>Trend coefficient S-B -axis</summary>
			/// <remarks>
			/// Japanese short name: "傾向係数　S-B軸", Google translated: "Trend coefficient S-B -axis".
			/// Japanese description: "基本QWC値(Sword-Bow)", Google translated: "QWC basic value (Sword-Bow)".
			/// </remarks>
			[ParameterTableRowAttribute("QWC_sb", index: 50, minimum: -1000, maximum: 1000, step: 1, sortOrder: 4300, unknown2: 1)]
			[DisplayName("Trend coefficient S-B -axis")]
			[Description("QWC basic value (Sword-Bow)")]
			[DefaultValue((Int16)0)]
			public Int16 QWC_sb {
				get { return qWC_sb; }
				set {
					if ((double)value < -1000 || (double)value > 1000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1000 to 1000 for QWC_sb.");
					SetProperty(ref qWC_sb, ref value, QWC_sbProperty);
				}
			}

			/// <summary>Trend coefficient M-W -axis</summary>
			/// <remarks>
			/// Japanese short name: "傾向係数　M-W軸", Google translated: "Trend coefficient M-W -axis".
			/// Japanese description: "基本QWC値(Mace-Wand)", Google translated: "QWC basic value (Mace-Wand)".
			/// </remarks>
			[ParameterTableRowAttribute("QWC_mw", index: 51, minimum: -1000, maximum: 1000, step: 1, sortOrder: 4400, unknown2: 1)]
			[DisplayName("Trend coefficient M-W -axis")]
			[Description("QWC basic value (Mace-Wand)")]
			[DefaultValue((Int16)0)]
			public Int16 QWC_mw {
				get { return qWC_mw; }
				set {
					if ((double)value < -1000 || (double)value > 1000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1000 to 1000 for QWC_mw.");
					SetProperty(ref qWC_mw, ref value, QWC_mwProperty);
				}
			}

			/// <summary>Trend coefficient C-D axis</summary>
			/// <remarks>
			/// Japanese short name: "傾向係数　C-D軸", Google translated: "Trend coefficient C-D axis".
			/// Japanese description: "基本QWC値(理解-拡散)", Google translated: "QWC basic value ( understanding - diffusion )".
			/// </remarks>
			[ParameterTableRowAttribute("QWC_cd", index: 52, minimum: -1000, maximum: 1000, step: 1, sortOrder: 4500, unknown2: 1)]
			[DisplayName("Trend coefficient C-D axis")]
			[Description("QWC basic value ( understanding - diffusion )")]
			[DefaultValue((Int16)0)]
			public Int16 QWC_cd {
				get { return qWC_cd; }
				set {
					if ((double)value < -1000 || (double)value > 1000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1000 to 1000 for QWC_cd.");
					SetProperty(ref qWC_cd, ref value, QWC_cdProperty);
				}
			}

			/// <summary>Seoul Lv</summary>
			/// <remarks>
			/// Japanese short name: "ソウルLv", Google translated: "Seoul Lv".
			/// Japanese description: "初期Lv", Google translated: "Initial Lv".
			/// </remarks>
			[ParameterTableRowAttribute("soulLv", index: 53, minimum: 0, maximum: 9999, step: 1, sortOrder: 10, unknown2: 1)]
			[DisplayName("Seoul Lv")]
			[Description("Initial Lv")]
			[DefaultValue((Int16)0)]
			public Int16 SoulLv {
				get { return soulLv; }
				set {
					if ((double)value < 0 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9999 for SoulLv.");
					SetProperty(ref soulLv, ref value, SoulLvProperty);
				}
			}

			/// <summary>Physical fitness</summary>
			/// <remarks>
			/// Japanese short name: "体力", Google translated: "Physical fitness".
			/// Japanese description: "体力の基本値", Google translated: "Base value of physical fitness".
			/// </remarks>
			[ParameterTableRowAttribute("baseVit", index: 54, minimum: 0, maximum: 99, step: 1, sortOrder: 100, unknown2: 1)]
			[DisplayName("Physical fitness")]
			[Description("Base value of physical fitness")]
			[DefaultValue((Byte)0)]
			public Byte BaseVit {
				get { return baseVit; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for BaseVit.");
					SetProperty(ref baseVit, ref value, BaseVitProperty);
				}
			}

			/// <summary>Spirit</summary>
			/// <remarks>
			/// Japanese short name: "精神", Google translated: "Spirit".
			/// Japanese description: "精神の基本値", Google translated: "The base value of the spirit".
			/// </remarks>
			[ParameterTableRowAttribute("baseWil", index: 55, minimum: 0, maximum: 99, step: 1, sortOrder: 200, unknown2: 1)]
			[DisplayName("Spirit")]
			[Description("The base value of the spirit")]
			[DefaultValue((Byte)0)]
			public Byte BaseWil {
				get { return baseWil; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for BaseWil.");
					SetProperty(ref baseWil, ref value, BaseWilProperty);
				}
			}

			/// <summary>Stubborn</summary>
			/// <remarks>
			/// Japanese short name: "頑強", Google translated: "Stubborn".
			/// Japanese description: "頑強の基本値", Google translated: "Base value of robust".
			/// </remarks>
			[ParameterTableRowAttribute("baseEnd", index: 56, minimum: 0, maximum: 99, step: 1, sortOrder: 300, unknown2: 1)]
			[DisplayName("Stubborn")]
			[Description("Base value of robust")]
			[DefaultValue((Byte)0)]
			public Byte BaseEnd {
				get { return baseEnd; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for BaseEnd.");
					SetProperty(ref baseEnd, ref value, BaseEndProperty);
				}
			}

			/// <summary>Muscle strength</summary>
			/// <remarks>
			/// Japanese short name: "筋力", Google translated: "Muscle strength".
			/// Japanese description: "筋力の基本値", Google translated: "Base value of muscle strength".
			/// </remarks>
			[ParameterTableRowAttribute("baseStr", index: 57, minimum: 0, maximum: 99, step: 1, sortOrder: 400, unknown2: 1)]
			[DisplayName("Muscle strength")]
			[Description("Base value of muscle strength")]
			[DefaultValue((Byte)0)]
			public Byte BaseStr {
				get { return baseStr; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for BaseStr.");
					SetProperty(ref baseStr, ref value, BaseStrProperty);
				}
			}

			/// <summary>Legerity</summary>
			/// <remarks>
			/// Japanese short name: "俊敏", Google translated: "Legerity".
			/// Japanese description: "俊敏の基本値", Google translated: "Base value of agile".
			/// </remarks>
			[ParameterTableRowAttribute("baseDex", index: 58, minimum: 0, maximum: 99, step: 1, sortOrder: 500, unknown2: 1)]
			[DisplayName("Legerity")]
			[Description("Base value of agile")]
			[DefaultValue((Byte)0)]
			public Byte BaseDex {
				get { return baseDex; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for BaseDex.");
					SetProperty(ref baseDex, ref value, BaseDexProperty);
				}
			}

			/// <summary>Witchcraft</summary>
			/// <remarks>
			/// Japanese short name: "魔力", Google translated: "Witchcraft".
			/// Japanese description: "魔力の基本値", Google translated: "Base value of magic".
			/// </remarks>
			[ParameterTableRowAttribute("baseMag", index: 59, minimum: 0, maximum: 99, step: 1, sortOrder: 600, unknown2: 1)]
			[DisplayName("Witchcraft")]
			[Description("Base value of magic")]
			[DefaultValue((Byte)0)]
			public Byte BaseMag {
				get { return baseMag; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for BaseMag.");
					SetProperty(ref baseMag, ref value, BaseMagProperty);
				}
			}

			/// <summary>Faith</summary>
			/// <remarks>
			/// Japanese short name: "信仰", Google translated: "Faith".
			/// Japanese description: "信仰の基本値", Google translated: "Basic values ​​of faith".
			/// </remarks>
			[ParameterTableRowAttribute("baseFai", index: 60, minimum: 0, maximum: 99, step: 1, sortOrder: 700, unknown2: 1)]
			[DisplayName("Faith")]
			[Description("Basic values ​​of faith")]
			[DefaultValue((Byte)0)]
			public Byte BaseFai {
				get { return baseFai; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for BaseFai.");
					SetProperty(ref baseFai, ref value, BaseFaiProperty);
				}
			}

			/// <summary>Luck</summary>
			/// <remarks>
			/// Japanese short name: "運", Google translated: "Luck".
			/// Japanese description: "運の基本値", Google translated: "Base value of luck".
			/// </remarks>
			[ParameterTableRowAttribute("baseLuc", index: 61, minimum: 0, maximum: 99, step: 1, sortOrder: 800, unknown2: 1)]
			[DisplayName("Luck")]
			[Description("Base value of luck")]
			[DefaultValue((Byte)0)]
			public Byte BaseLuc {
				get { return baseLuc; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for BaseLuc.");
					SetProperty(ref baseLuc, ref value, BaseLucProperty);
				}
			}

			/// <summary>Human nature</summary>
			/// <remarks>
			/// Japanese short name: "人間性", Google translated: "Human nature".
			/// Japanese description: "人間性の基本値", Google translated: "Basic values ​​of humanity".
			/// </remarks>
			[ParameterTableRowAttribute("baseHeroPoint", index: 62, minimum: 0, maximum: 99, step: 1, sortOrder: 810, unknown2: 1)]
			[DisplayName("Human nature")]
			[Description("Basic values ​​of humanity")]
			[DefaultValue((Byte)0)]
			public Byte BaseHeroPoint {
				get { return baseHeroPoint; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for BaseHeroPoint.");
					SetProperty(ref baseHeroPoint, ref value, BaseHeroPointProperty);
				}
			}

			/// <summary>Endurance</summary>
			/// <remarks>
			/// Japanese short name: "耐久力", Google translated: "Endurance".
			/// Japanese description: "耐久力の基本値", Google translated: "Base value of endurance".
			/// </remarks>
			[ParameterTableRowAttribute("baseDurability", index: 63, minimum: 0, maximum: 99, step: 1, sortOrder: 820, unknown2: 1)]
			[DisplayName("Endurance")]
			[Description("Base value of endurance")]
			[DefaultValue((Byte)0)]
			public Byte BaseDurability {
				get { return baseDurability; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for BaseDurability.");
					SetProperty(ref baseDurability, ref value, BaseDurabilityProperty);
				}
			}

			/// <summary>Possession number of items 01</summary>
			/// <remarks>
			/// Japanese short name: "アイテム01の所持数", Google translated: "Possession number of items 01".
			/// Japanese description: "初期所持のアイテム個数01", Google translated: "Item number 01 of the initial possession".
			/// </remarks>
			[ParameterTableRowAttribute("itemNum_01", index: 64, minimum: 0, maximum: 99, step: 1, sortOrder: 4700, unknown2: 1)]
			[DisplayName("Possession number of items 01")]
			[Description("Item number 01 of the initial possession")]
			[DefaultValue((Byte)0)]
			public Byte ItemNum_01 {
				get { return itemNum_01; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for ItemNum_01.");
					SetProperty(ref itemNum_01, ref value, ItemNum_01Property);
				}
			}

			/// <summary>Possession number of items 02</summary>
			/// <remarks>
			/// Japanese short name: "アイテム02の所持数", Google translated: "Possession number of items 02".
			/// Japanese description: "初期所持のアイテム個数02", Google translated: "Item number 02 of the initial possession".
			/// </remarks>
			[ParameterTableRowAttribute("itemNum_02", index: 65, minimum: 0, maximum: 99, step: 1, sortOrder: 4900, unknown2: 1)]
			[DisplayName("Possession number of items 02")]
			[Description("Item number 02 of the initial possession")]
			[DefaultValue((Byte)0)]
			public Byte ItemNum_02 {
				get { return itemNum_02; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for ItemNum_02.");
					SetProperty(ref itemNum_02, ref value, ItemNum_02Property);
				}
			}

			/// <summary>Possession number of items 03</summary>
			/// <remarks>
			/// Japanese short name: "アイテム03の所持数", Google translated: "Possession number of items 03".
			/// Japanese description: "初期所持のアイテム個数03", Google translated: "Item number 03 of the initial possession".
			/// </remarks>
			[ParameterTableRowAttribute("itemNum_03", index: 66, minimum: 0, maximum: 99, step: 1, sortOrder: 5100, unknown2: 1)]
			[DisplayName("Possession number of items 03")]
			[Description("Item number 03 of the initial possession")]
			[DefaultValue((Byte)0)]
			public Byte ItemNum_03 {
				get { return itemNum_03; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for ItemNum_03.");
					SetProperty(ref itemNum_03, ref value, ItemNum_03Property);
				}
			}

			/// <summary>Item number 04</summary>
			/// <remarks>
			/// Japanese short name: "アイテム個数04", Google translated: "Item number 04".
			/// Japanese description: "初期所持のアイテム個数04", Google translated: "Item number 04 of the initial possession".
			/// </remarks>
			[ParameterTableRowAttribute("itemNum_04", index: 67, minimum: 0, maximum: 99, step: 1, sortOrder: 5300, unknown2: 1)]
			[DisplayName("Item number 04")]
			[Description("Item number 04 of the initial possession")]
			[DefaultValue((Byte)0)]
			public Byte ItemNum_04 {
				get { return itemNum_04; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for ItemNum_04.");
					SetProperty(ref itemNum_04, ref value, ItemNum_04Property);
				}
			}

			/// <summary>Item number 05</summary>
			/// <remarks>
			/// Japanese short name: "アイテム個数05", Google translated: "Item number 05".
			/// Japanese description: "初期所持のアイテム個数05", Google translated: "Item number 05 of the initial possession".
			/// </remarks>
			[ParameterTableRowAttribute("itemNum_05", index: 68, minimum: 0, maximum: 99, step: 1, sortOrder: 5500, unknown2: 1)]
			[DisplayName("Item number 05")]
			[Description("Item number 05 of the initial possession")]
			[DefaultValue((Byte)0)]
			public Byte ItemNum_05 {
				get { return itemNum_05; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for ItemNum_05.");
					SetProperty(ref itemNum_05, ref value, ItemNum_05Property);
				}
			}

			/// <summary>Item number 06</summary>
			/// <remarks>
			/// Japanese short name: "アイテム個数06", Google translated: "Item number 06".
			/// Japanese description: "初期所持のアイテム個数06", Google translated: "Item number 06 of the initial possession".
			/// </remarks>
			[ParameterTableRowAttribute("itemNum_06", index: 69, minimum: 0, maximum: 99, step: 1, sortOrder: 5700, unknown2: 1)]
			[DisplayName("Item number 06")]
			[Description("Item number 06 of the initial possession")]
			[DefaultValue((Byte)0)]
			public Byte ItemNum_06 {
				get { return itemNum_06; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for ItemNum_06.");
					SetProperty(ref itemNum_06, ref value, ItemNum_06Property);
				}
			}

			/// <summary>Item number 07</summary>
			/// <remarks>
			/// Japanese short name: "アイテム個数07", Google translated: "Item number 07".
			/// Japanese description: "初期所持のアイテム個数07", Google translated: "Item number 07 of the initial possession".
			/// </remarks>
			[ParameterTableRowAttribute("itemNum_07", index: 70, minimum: 0, maximum: 99, step: 1, sortOrder: 5900, unknown2: 1)]
			[DisplayName("Item number 07")]
			[Description("Item number 07 of the initial possession")]
			[DefaultValue((Byte)0)]
			public Byte ItemNum_07 {
				get { return itemNum_07; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for ItemNum_07.");
					SetProperty(ref itemNum_07, ref value, ItemNum_07Property);
				}
			}

			/// <summary>Item number 08</summary>
			/// <remarks>
			/// Japanese short name: "アイテム個数08", Google translated: "Item number 08".
			/// Japanese description: "初期所持のアイテム個数08", Google translated: "Item number 08 of the initial possession".
			/// </remarks>
			[ParameterTableRowAttribute("itemNum_08", index: 71, minimum: 0, maximum: 99, step: 1, sortOrder: 6100, unknown2: 1)]
			[DisplayName("Item number 08")]
			[Description("Item number 08 of the initial possession")]
			[DefaultValue((Byte)0)]
			public Byte ItemNum_08 {
				get { return itemNum_08; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for ItemNum_08.");
					SetProperty(ref itemNum_08, ref value, ItemNum_08Property);
				}
			}

			/// <summary>Item number 09</summary>
			/// <remarks>
			/// Japanese short name: "アイテム個数09", Google translated: "Item number 09".
			/// Japanese description: "初期所持のアイテム個数09", Google translated: "Item number 09 of the initial possession".
			/// </remarks>
			[ParameterTableRowAttribute("itemNum_09", index: 72, minimum: 0, maximum: 99, step: 1, sortOrder: 6300, unknown2: 1)]
			[DisplayName("Item number 09")]
			[Description("Item number 09 of the initial possession")]
			[DefaultValue((Byte)0)]
			public Byte ItemNum_09 {
				get { return itemNum_09; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for ItemNum_09.");
					SetProperty(ref itemNum_09, ref value, ItemNum_09Property);
				}
			}

			/// <summary>Item number 10</summary>
			/// <remarks>
			/// Japanese short name: "アイテム個数10", Google translated: "Item number 10".
			/// Japanese description: "初期所持のアイテム個数10", Google translated: "Item number 10 of the initial possession".
			/// </remarks>
			[ParameterTableRowAttribute("itemNum_10", index: 73, minimum: 0, maximum: 99, step: 1, sortOrder: 6500, unknown2: 1)]
			[DisplayName("Item number 10")]
			[Description("Item number 10 of the initial possession")]
			[DefaultValue((Byte)0)]
			public Byte ItemNum_10 {
				get { return itemNum_10; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for ItemNum_10.");
					SetProperty(ref itemNum_10, ref value, ItemNum_10Property);
				}
			}

			/// <summary>Head</summary>
			/// <remarks>
			/// Japanese short name: "頭部", Google translated: "Head".
			/// Japanese description: "体型変化用スケール", Google translated: "Body Type change for scale".
			/// </remarks>
			[ParameterTableRowAttribute("bodyScaleHead", index: 74, minimum: -100, maximum: 100, step: 1, sortOrder: 8000, unknown2: 0)]
			[DisplayName("Head")]
			[Description("Body Type change for scale")]
			[DefaultValue((SByte)0)]
			public SByte BodyScaleHead {
				get { return bodyScaleHead; }
				set {
					if ((double)value < -100 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -100 to 100 for BodyScaleHead.");
					SetProperty(ref bodyScaleHead, ref value, BodyScaleHeadProperty);
				}
			}

			/// <summary>Breast</summary>
			/// <remarks>
			/// Japanese short name: "胸部", Google translated: "Breast".
			/// Japanese description: "体型変化用スケール", Google translated: "Body Type change for scale".
			/// </remarks>
			[ParameterTableRowAttribute("bodyScaleBreast", index: 75, minimum: -100, maximum: 100, step: 1, sortOrder: 8100, unknown2: 0)]
			[DisplayName("Breast")]
			[Description("Body Type change for scale")]
			[DefaultValue((SByte)0)]
			public SByte BodyScaleBreast {
				get { return bodyScaleBreast; }
				set {
					if ((double)value < -100 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -100 to 100 for BodyScaleBreast.");
					SetProperty(ref bodyScaleBreast, ref value, BodyScaleBreastProperty);
				}
			}

			/// <summary>Abdominal</summary>
			/// <remarks>
			/// Japanese short name: "腹部", Google translated: "Abdominal".
			/// Japanese description: "体型変化用スケール", Google translated: "Body Type change for scale".
			/// </remarks>
			[ParameterTableRowAttribute("bodyScaleAbdomen", index: 76, minimum: -100, maximum: 100, step: 1, sortOrder: 8200, unknown2: 0)]
			[DisplayName("Abdominal")]
			[Description("Body Type change for scale")]
			[DefaultValue((SByte)0)]
			public SByte BodyScaleAbdomen {
				get { return bodyScaleAbdomen; }
				set {
					if ((double)value < -100 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -100 to 100 for BodyScaleAbdomen.");
					SetProperty(ref bodyScaleAbdomen, ref value, BodyScaleAbdomenProperty);
				}
			}

			/// <summary>Arms</summary>
			/// <remarks>
			/// Japanese short name: "腕部", Google translated: "Arms".
			/// Japanese description: "体型変化用スケール", Google translated: "Body Type change for scale".
			/// </remarks>
			[ParameterTableRowAttribute("bodyScaleArm", index: 77, minimum: -100, maximum: 100, step: 1, sortOrder: 8300, unknown2: 0)]
			[DisplayName("Arms")]
			[Description("Body Type change for scale")]
			[DefaultValue((SByte)0)]
			public SByte BodyScaleArm {
				get { return bodyScaleArm; }
				set {
					if ((double)value < -100 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -100 to 100 for BodyScaleArm.");
					SetProperty(ref bodyScaleArm, ref value, BodyScaleArmProperty);
				}
			}

			/// <summary>Leg</summary>
			/// <remarks>
			/// Japanese short name: "脚部", Google translated: "Leg".
			/// Japanese description: "体型変化用スケール", Google translated: "Body Type change for scale".
			/// </remarks>
			[ParameterTableRowAttribute("bodyScaleLeg", index: 78, minimum: -100, maximum: 100, step: 1, sortOrder: 8400, unknown2: 0)]
			[DisplayName("Leg")]
			[Description("Body Type change for scale")]
			[DefaultValue((SByte)0)]
			public SByte BodyScaleLeg {
				get { return bodyScaleLeg; }
				set {
					if ((double)value < -100 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -100 to 100 for BodyScaleLeg.");
					SetProperty(ref bodyScaleLeg, ref value, BodyScaleLegProperty);
				}
			}

			/// <summary>Gesture ID0</summary>
			/// <remarks>
			/// Japanese short name: "ジェスチャーID0", Google translated: "Gesture ID0".
			/// Japanese description: "ジェスチャー0番目(EzStateのジェスチャー0を再生したいなら0)", Google translated: "( 0 If you want to play a gesture of 0 EzState) 0 -th gesture".
			/// </remarks>
			[ParameterTableRowAttribute("gestureId0", index: 79, minimum: -1, maximum: 127, step: 1, sortOrder: 8600, unknown2: 1)]
			[DisplayName("Gesture ID0")]
			[Description("( 0 If you want to play a gesture of 0 EzState) 0 -th gesture")]
			[DefaultValue((SByte)(-1))]
			public SByte GestureId0 {
				get { return gestureId0; }
				set {
					if ((double)value < -1 || (double)value > 127)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 127 for GestureId0.");
					SetProperty(ref gestureId0, ref value, GestureId0Property);
				}
			}

			/// <summary>Gesture ID1</summary>
			/// <remarks>
			/// Japanese short name: "ジェスチャーID1", Google translated: "Gesture ID1".
			/// Japanese description: "ジェスチャー1番目(EzStateのジェスチャー0を再生したいなら0)", Google translated: "( 0 If you want to play a gesture of 0 EzState) 1 -th gesture".
			/// </remarks>
			[ParameterTableRowAttribute("gestureId1", index: 80, minimum: -1, maximum: 127, step: 1, sortOrder: 8700, unknown2: 1)]
			[DisplayName("Gesture ID1")]
			[Description("( 0 If you want to play a gesture of 0 EzState) 1 -th gesture")]
			[DefaultValue((SByte)(-1))]
			public SByte GestureId1 {
				get { return gestureId1; }
				set {
					if ((double)value < -1 || (double)value > 127)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 127 for GestureId1.");
					SetProperty(ref gestureId1, ref value, GestureId1Property);
				}
			}

			/// <summary>Gesture ID2</summary>
			/// <remarks>
			/// Japanese short name: "ジェスチャーID2", Google translated: "Gesture ID2".
			/// Japanese description: "ジェスチャー2番目(EzStateのジェスチャー0を再生したいなら0)", Google translated: "( 0 If you want to play a gesture of 0 EzState) 2 -th gesture".
			/// </remarks>
			[ParameterTableRowAttribute("gestureId2", index: 81, minimum: -1, maximum: 127, step: 1, sortOrder: 8800, unknown2: 1)]
			[DisplayName("Gesture ID2")]
			[Description("( 0 If you want to play a gesture of 0 EzState) 2 -th gesture")]
			[DefaultValue((SByte)(-1))]
			public SByte GestureId2 {
				get { return gestureId2; }
				set {
					if ((double)value < -1 || (double)value > 127)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 127 for GestureId2.");
					SetProperty(ref gestureId2, ref value, GestureId2Property);
				}
			}

			/// <summary>Gesture ID3</summary>
			/// <remarks>
			/// Japanese short name: "ジェスチャーID3", Google translated: "Gesture ID3".
			/// Japanese description: "ジェスチャー3番目(EzStateのジェスチャー0を再生したいなら0)", Google translated: "( 0 If you want to play a gesture of 0 EzState) 3 -th gesture".
			/// </remarks>
			[ParameterTableRowAttribute("gestureId3", index: 82, minimum: -1, maximum: 127, step: 1, sortOrder: 8900, unknown2: 1)]
			[DisplayName("Gesture ID3")]
			[Description("( 0 If you want to play a gesture of 0 EzState) 3 -th gesture")]
			[DefaultValue((SByte)(-1))]
			public SByte GestureId3 {
				get { return gestureId3; }
				set {
					if ((double)value < -1 || (double)value > 127)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 127 for GestureId3.");
					SetProperty(ref gestureId3, ref value, GestureId3Property);
				}
			}

			/// <summary>Gesture ID4</summary>
			/// <remarks>
			/// Japanese short name: "ジェスチャーID4", Google translated: "Gesture ID4".
			/// Japanese description: "ジェスチャー4番目(EzStateのジェスチャー0を再生したいなら0)", Google translated: "( 0 If you want to play a gesture of 0 EzState) 4 th gesture".
			/// </remarks>
			[ParameterTableRowAttribute("gestureId4", index: 83, minimum: -1, maximum: 127, step: 1, sortOrder: 9000, unknown2: 1)]
			[DisplayName("Gesture ID4")]
			[Description("( 0 If you want to play a gesture of 0 EzState) 4 th gesture")]
			[DefaultValue((SByte)(-1))]
			public SByte GestureId4 {
				get { return gestureId4; }
				set {
					if ((double)value < -1 || (double)value > 127)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 127 for GestureId4.");
					SetProperty(ref gestureId4, ref value, GestureId4Property);
				}
			}

			/// <summary>Gesture ID5</summary>
			/// <remarks>
			/// Japanese short name: "ジェスチャーID5", Google translated: "Gesture ID5".
			/// Japanese description: "ジェスチャー5番目(EzStateのジェスチャー0を再生したいなら0)", Google translated: "( 0 If you want to play a gesture of 0 EzState) 5 th gesture".
			/// </remarks>
			[ParameterTableRowAttribute("gestureId5", index: 84, minimum: -1, maximum: 127, step: 1, sortOrder: 9100, unknown2: 1)]
			[DisplayName("Gesture ID5")]
			[Description("( 0 If you want to play a gesture of 0 EzState) 5 th gesture")]
			[DefaultValue((SByte)(-1))]
			public SByte GestureId5 {
				get { return gestureId5; }
				set {
					if ((double)value < -1 || (double)value > 127)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 127 for GestureId5.");
					SetProperty(ref gestureId5, ref value, GestureId5Property);
				}
			}

			/// <summary>Gesture ID6</summary>
			/// <remarks>
			/// Japanese short name: "ジェスチャーID6", Google translated: "Gesture ID6".
			/// Japanese description: "ジェスチャー6番目(EzStateのジェスチャー0を再生したいなら0)", Google translated: "( 0 If you want to play a gesture of 0 EzState) 6 th gesture".
			/// </remarks>
			[ParameterTableRowAttribute("gestureId6", index: 85, minimum: -1, maximum: 127, step: 1, sortOrder: 9200, unknown2: 1)]
			[DisplayName("Gesture ID6")]
			[Description("( 0 If you want to play a gesture of 0 EzState) 6 th gesture")]
			[DefaultValue((SByte)(-1))]
			public SByte GestureId6 {
				get { return gestureId6; }
				set {
					if ((double)value < -1 || (double)value > 127)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 127 for GestureId6.");
					SetProperty(ref gestureId6, ref value, GestureId6Property);
				}
			}

			/// <summary>NPC NPC type of player</summary>
			/// <remarks>
			/// Japanese short name: "NPCプレイヤーのNPCタイプ", Google translated: "NPC NPC type of player".
			/// Japanese description: "NPCプレイヤーで使用するNPCタイプ。通常プレイヤーでは使用しません。", Google translated: "NPC type used in the NPC player . It is not used in the normal player .".
			/// </remarks>
			[ParameterTableRowAttribute("npcPlayerType", index: 86, minimum: 0, maximum: 255, step: 1, sortOrder: 6800, unknown2: 1)]
			[DisplayName("NPC NPC type of player")]
			[Description("NPC type used in the NPC player . It is not used in the normal player .")]
			[DefaultValue((NpcType)0)]
			public NpcType NpcPlayerType {
				get { return npcPlayerType; }
				set { SetProperty(ref npcPlayerType, ref value, NpcPlayerTypeProperty); }
			}

			/// <summary>Drawing type of NPC player</summary>
			/// <remarks>
			/// Japanese short name: "NPCプレイヤーの描画タイプ", Google translated: "Drawing type of NPC player".
			/// Japanese description: "NPCプレイヤーで使用する描画タイプ。通常プレイヤーでは使用しません。", Google translated: "Drawing type to be used in the NPC player . It is not used in the normal player .".
			/// </remarks>
			[ParameterTableRowAttribute("npcPlayerDrawType", index: 87, minimum: 0, maximum: 255, step: 1, sortOrder: 6900, unknown2: 1)]
			[DisplayName("Drawing type of NPC player")]
			[Description("Drawing type to be used in the NPC player . It is not used in the normal player .")]
			[DefaultValue((NpcDrawType)0)]
			public NpcDrawType NpcPlayerDrawType {
				get { return npcPlayerDrawType; }
				set { SetProperty(ref npcPlayerDrawType, ref value, NpcPlayerDrawTypeProperty); }
			}

			/// <summary>Gender of NPC player</summary>
			/// <remarks>
			/// Japanese short name: "NPCプレイヤーの性別", Google translated: "Gender of NPC player".
			/// Japanese description: "NPCプレイヤーで使用する性別です。通常プレイヤーには反映しません。", Google translated: "It is a gender to be used in the NPC player . It does not reflect the normal player .".
			/// </remarks>
			[ParameterTableRowAttribute("npcPlayerSex", index: 88, minimum: 0, maximum: 1, step: 1, sortOrder: 7000, unknown2: 1)]
			[DisplayName("Gender of NPC player")]
			[Description("It is a gender to be used in the NPC player . It does not reflect the normal player .")]
			[DefaultValue((CharacterInitialSex)0)]
			public CharacterInitialSex NpcPlayerSex {
				get { return npcPlayerSex; }
				set { SetProperty(ref npcPlayerSex, ref value, NpcPlayerSexProperty); }
			}

			/// <summary>Pledge</summary>
			/// <remarks>
			/// Japanese short name: "誓約", Google translated: "Pledge".
			/// Japanese description: "誓約タイプ(なし：0)", Google translated: "Pledge type (no : 0)".
			/// </remarks>
			[ParameterTableRowAttribute("vowType:4", index: 89, minimum: 0, maximum: 15, step: 1, sortOrder: 8500, unknown2: 1)]
			[DisplayName("Pledge")]
			[Description("Pledge type (no : 0)")]
			[DefaultValue((CharacterInitialVow)0)]
			public CharacterInitialVow VowType {
				get { return (CharacterInitialVow)GetBitProperty(0, 4, VowTypeProperty); }
				set { SetBitProperty(0, 4, (int)value, VowTypeProperty); }
			}

			/// <summary>pad</summary>
			/// <remarks>
			/// Japanese short name: "pad", Google translated: "pad".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("pad:4", index: 90, minimum: 0, maximum: 0, step: 0, sortOrder: 100000000, unknown2: 0)]
			[DisplayName("pad")]
			[Description("")]
			[DefaultValue((Byte)0)]
			[Browsable(false)]
			public Byte Pad {
				get { return (Byte)GetBitProperty(4, 4, PadProperty); }
				set {
					if ((double)value < 0 || (double)value > 0)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 0 for Pad.");
					SetBitProperty(4, 4, (int)value, PadProperty);
				}
			}

			/// <summary>Padding</summary>
			/// <remarks>
			/// Japanese short name: "パディング", Google translated: "Padding".
			/// Japanese description: "パディング", Google translated: "Padding".
			/// </remarks>
			[ParameterTableRowAttribute("pad0[10]", index: 91, minimum: -1, maximum: 999999, step: 1, sortOrder: 99999999, unknown2: 1)]
			[DisplayName("Padding")]
			[Description("Padding")]
			[Browsable(false)]
			public Byte[] Pad0 {
				get { return pad0; }
				set { SetProperty(ref pad0, ref value, Pad0Property); }
			}

			internal CharacterInitialiser(ParameterTable table, int index, AssetLoader loader, int next)
				: base(table, index, loader) {
				BinaryReader reader = loader.Reader;

				BaseRec_mp = reader.ReadSingle();
				BaseRec_sp = reader.ReadSingle();
				Red_Falldam = reader.ReadSingle();
				Soul = reader.ReadInt32();
				Equip_Wep_Right = reader.ReadInt32();
				Equip_Subwep_Right = reader.ReadInt32();
				Equip_Wep_Left = reader.ReadInt32();
				Equip_Subwep_Left = reader.ReadInt32();
				Equip_Helm = reader.ReadInt32();
				Equip_Armer = reader.ReadInt32();
				Equip_Gaunt = reader.ReadInt32();
				Equip_Leg = reader.ReadInt32();
				Equip_Arrow = reader.ReadInt32();
				Equip_Bolt = reader.ReadInt32();
				Equip_SubArrow = reader.ReadInt32();
				Equip_SubBolt = reader.ReadInt32();
				Equip_Accessory01 = reader.ReadInt32();
				Equip_Accessory02 = reader.ReadInt32();
				Equip_Accessory03 = reader.ReadInt32();
				Equip_Accessory04 = reader.ReadInt32();
				Equip_Accessory05 = reader.ReadInt32();
				Equip_Skill_01 = reader.ReadInt32();
				Equip_Skill_02 = reader.ReadInt32();
				Equip_Skill_03 = reader.ReadInt32();
				Equip_Spell_01 = reader.ReadInt32();
				Equip_Spell_02 = reader.ReadInt32();
				Equip_Spell_03 = reader.ReadInt32();
				Equip_Spell_04 = reader.ReadInt32();
				Equip_Spell_05 = reader.ReadInt32();
				Equip_Spell_06 = reader.ReadInt32();
				Equip_Spell_07 = reader.ReadInt32();
				Item_01 = reader.ReadInt32();
				Item_02 = reader.ReadInt32();
				Item_03 = reader.ReadInt32();
				Item_04 = reader.ReadInt32();
				Item_05 = reader.ReadInt32();
				Item_06 = reader.ReadInt32();
				Item_07 = reader.ReadInt32();
				Item_08 = reader.ReadInt32();
				Item_09 = reader.ReadInt32();
				Item_10 = reader.ReadInt32();
				NpcPlayerFaceGenId = reader.ReadInt32();
				NpcPlayerThinkId = reader.ReadInt32();
				BaseHp = reader.ReadUInt16();
				BaseMp = reader.ReadUInt16();
				BaseSp = reader.ReadUInt16();
				ArrowNum = reader.ReadUInt16();
				BoltNum = reader.ReadUInt16();
				SubArrowNum = reader.ReadUInt16();
				SubBoltNum = reader.ReadUInt16();
				QWC_sb = reader.ReadInt16();
				QWC_mw = reader.ReadInt16();
				QWC_cd = reader.ReadInt16();
				SoulLv = reader.ReadInt16();
				BaseVit = reader.ReadByte();
				BaseWil = reader.ReadByte();
				BaseEnd = reader.ReadByte();
				BaseStr = reader.ReadByte();
				BaseDex = reader.ReadByte();
				BaseMag = reader.ReadByte();
				BaseFai = reader.ReadByte();
				BaseLuc = reader.ReadByte();
				BaseHeroPoint = reader.ReadByte();
				BaseDurability = reader.ReadByte();
				ItemNum_01 = reader.ReadByte();
				ItemNum_02 = reader.ReadByte();
				ItemNum_03 = reader.ReadByte();
				ItemNum_04 = reader.ReadByte();
				ItemNum_05 = reader.ReadByte();
				ItemNum_06 = reader.ReadByte();
				ItemNum_07 = reader.ReadByte();
				ItemNum_08 = reader.ReadByte();
				ItemNum_09 = reader.ReadByte();
				ItemNum_10 = reader.ReadByte();
				BodyScaleHead = reader.ReadSByte();
				BodyScaleBreast = reader.ReadSByte();
				BodyScaleAbdomen = reader.ReadSByte();
				BodyScaleArm = reader.ReadSByte();
				BodyScaleLeg = reader.ReadSByte();
				GestureId0 = reader.ReadSByte();
				GestureId1 = reader.ReadSByte();
				GestureId2 = reader.ReadSByte();
				GestureId3 = reader.ReadSByte();
				GestureId4 = reader.ReadSByte();
				GestureId5 = reader.ReadSByte();
				GestureId6 = reader.ReadSByte();
				NpcPlayerType = (NpcType)reader.ReadByte();
				NpcPlayerDrawType = (NpcDrawType)reader.ReadByte();
				NpcPlayerSex = (CharacterInitialSex)reader.ReadByte();
				BitFields = reader.ReadBytes(1);
				Pad0 = reader.ReadBytes(10);
			}

			internal CharacterInitialiser(ParameterTable table, int index)
				: base(table, index) {
				BitFields = new byte[1];
				BaseRec_mp = (Single)0;
				BaseRec_sp = (Single)0;
				Red_Falldam = (Single)0;
				Soul = (Int32)0;
				Equip_Wep_Right = (Int32)(-1);
				Equip_Subwep_Right = (Int32)(-1);
				Equip_Wep_Left = (Int32)(-1);
				Equip_Subwep_Left = (Int32)(-1);
				Equip_Helm = (Int32)(-1);
				Equip_Armer = (Int32)(-1);
				Equip_Gaunt = (Int32)(-1);
				Equip_Leg = (Int32)(-1);
				Equip_Arrow = (Int32)(-1);
				Equip_Bolt = (Int32)(-1);
				Equip_SubArrow = (Int32)(-1);
				Equip_SubBolt = (Int32)(-1);
				Equip_Accessory01 = (Int32)(-1);
				Equip_Accessory02 = (Int32)(-1);
				Equip_Accessory03 = (Int32)(-1);
				Equip_Accessory04 = (Int32)(-1);
				Equip_Accessory05 = (Int32)(-1);
				Equip_Skill_01 = (Int32)(-1);
				Equip_Skill_02 = (Int32)(-1);
				Equip_Skill_03 = (Int32)(-1);
				Equip_Spell_01 = (Int32)(-1);
				Equip_Spell_02 = (Int32)(-1);
				Equip_Spell_03 = (Int32)(-1);
				Equip_Spell_04 = (Int32)(-1);
				Equip_Spell_05 = (Int32)(-1);
				Equip_Spell_06 = (Int32)(-1);
				Equip_Spell_07 = (Int32)(-1);
				Item_01 = (Int32)(-1);
				Item_02 = (Int32)(-1);
				Item_03 = (Int32)(-1);
				Item_04 = (Int32)(-1);
				Item_05 = (Int32)(-1);
				Item_06 = (Int32)(-1);
				Item_07 = (Int32)(-1);
				Item_08 = (Int32)(-1);
				Item_09 = (Int32)(-1);
				Item_10 = (Int32)(-1);
				NpcPlayerFaceGenId = (Int32)0;
				NpcPlayerThinkId = (Int32)0;
				BaseHp = (UInt16)0;
				BaseMp = (UInt16)0;
				BaseSp = (UInt16)0;
				ArrowNum = (UInt16)0;
				BoltNum = (UInt16)0;
				SubArrowNum = (UInt16)0;
				SubBoltNum = (UInt16)0;
				QWC_sb = (Int16)0;
				QWC_mw = (Int16)0;
				QWC_cd = (Int16)0;
				SoulLv = (Int16)0;
				BaseVit = (Byte)0;
				BaseWil = (Byte)0;
				BaseEnd = (Byte)0;
				BaseStr = (Byte)0;
				BaseDex = (Byte)0;
				BaseMag = (Byte)0;
				BaseFai = (Byte)0;
				BaseLuc = (Byte)0;
				BaseHeroPoint = (Byte)0;
				BaseDurability = (Byte)0;
				ItemNum_01 = (Byte)0;
				ItemNum_02 = (Byte)0;
				ItemNum_03 = (Byte)0;
				ItemNum_04 = (Byte)0;
				ItemNum_05 = (Byte)0;
				ItemNum_06 = (Byte)0;
				ItemNum_07 = (Byte)0;
				ItemNum_08 = (Byte)0;
				ItemNum_09 = (Byte)0;
				ItemNum_10 = (Byte)0;
				BodyScaleHead = (SByte)0;
				BodyScaleBreast = (SByte)0;
				BodyScaleAbdomen = (SByte)0;
				BodyScaleArm = (SByte)0;
				BodyScaleLeg = (SByte)0;
				GestureId0 = (SByte)(-1);
				GestureId1 = (SByte)(-1);
				GestureId2 = (SByte)(-1);
				GestureId3 = (SByte)(-1);
				GestureId4 = (SByte)(-1);
				GestureId5 = (SByte)(-1);
				GestureId6 = (SByte)(-1);
				NpcPlayerType = (NpcType)0;
				NpcPlayerDrawType = (NpcDrawType)0;
				NpcPlayerSex = (CharacterInitialSex)0;
				VowType = (CharacterInitialVow)0;
				Pad = (Byte)0;
				Pad0 = new Byte[10];
			}

			/// <summary>Write the row to the writer.</summary>
			/// <param name="writer"></param>
			public override void Write(BinaryWriter writer) {
				writer.Write(BaseRec_mp);
				writer.Write(BaseRec_sp);
				writer.Write(Red_Falldam);
				writer.Write(Soul);
				writer.Write(Equip_Wep_Right);
				writer.Write(Equip_Subwep_Right);
				writer.Write(Equip_Wep_Left);
				writer.Write(Equip_Subwep_Left);
				writer.Write(Equip_Helm);
				writer.Write(Equip_Armer);
				writer.Write(Equip_Gaunt);
				writer.Write(Equip_Leg);
				writer.Write(Equip_Arrow);
				writer.Write(Equip_Bolt);
				writer.Write(Equip_SubArrow);
				writer.Write(Equip_SubBolt);
				writer.Write(Equip_Accessory01);
				writer.Write(Equip_Accessory02);
				writer.Write(Equip_Accessory03);
				writer.Write(Equip_Accessory04);
				writer.Write(Equip_Accessory05);
				writer.Write(Equip_Skill_01);
				writer.Write(Equip_Skill_02);
				writer.Write(Equip_Skill_03);
				writer.Write(Equip_Spell_01);
				writer.Write(Equip_Spell_02);
				writer.Write(Equip_Spell_03);
				writer.Write(Equip_Spell_04);
				writer.Write(Equip_Spell_05);
				writer.Write(Equip_Spell_06);
				writer.Write(Equip_Spell_07);
				writer.Write(Item_01);
				writer.Write(Item_02);
				writer.Write(Item_03);
				writer.Write(Item_04);
				writer.Write(Item_05);
				writer.Write(Item_06);
				writer.Write(Item_07);
				writer.Write(Item_08);
				writer.Write(Item_09);
				writer.Write(Item_10);
				writer.Write(NpcPlayerFaceGenId);
				writer.Write(NpcPlayerThinkId);
				writer.Write(BaseHp);
				writer.Write(BaseMp);
				writer.Write(BaseSp);
				writer.Write(ArrowNum);
				writer.Write(BoltNum);
				writer.Write(SubArrowNum);
				writer.Write(SubBoltNum);
				writer.Write(QWC_sb);
				writer.Write(QWC_mw);
				writer.Write(QWC_cd);
				writer.Write(SoulLv);
				writer.Write(BaseVit);
				writer.Write(BaseWil);
				writer.Write(BaseEnd);
				writer.Write(BaseStr);
				writer.Write(BaseDex);
				writer.Write(BaseMag);
				writer.Write(BaseFai);
				writer.Write(BaseLuc);
				writer.Write(BaseHeroPoint);
				writer.Write(BaseDurability);
				writer.Write(ItemNum_01);
				writer.Write(ItemNum_02);
				writer.Write(ItemNum_03);
				writer.Write(ItemNum_04);
				writer.Write(ItemNum_05);
				writer.Write(ItemNum_06);
				writer.Write(ItemNum_07);
				writer.Write(ItemNum_08);
				writer.Write(ItemNum_09);
				writer.Write(ItemNum_10);
				writer.Write(BodyScaleHead);
				writer.Write(BodyScaleBreast);
				writer.Write(BodyScaleAbdomen);
				writer.Write(BodyScaleArm);
				writer.Write(BodyScaleLeg);
				writer.Write(GestureId0);
				writer.Write(GestureId1);
				writer.Write(GestureId2);
				writer.Write(GestureId3);
				writer.Write(GestureId4);
				writer.Write(GestureId5);
				writer.Write(GestureId6);
				writer.Write((Byte)NpcPlayerType);
				writer.Write((Byte)NpcPlayerDrawType);
				writer.Write((Byte)NpcPlayerSex);
				writer.Write(BitFields);
				writer.Write(Pad0);
			}
		}
	}
}
