/*
*/
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
		/// Describes an attack.
		/// </summary>
		public class Attack : ParameterTableRow {
			/// <summary>The name of the table in the file.</summary>
			public const string TableName = "ATK_PARAM_ST";

			Single hit0_Radius, hit1_Radius, hit2_Radius, hit3_Radius, knockbackDist, hitStopTime;
			Int32 spEffectId0, spEffectId1, spEffectId2, spEffectId3, spEffectId4;
			Int16 hit0_DmyPoly1, hit1_DmyPoly1, hit2_DmyPoly1, hit3_DmyPoly1, hit0_DmyPoly2, hit1_DmyPoly2, hit2_DmyPoly2, hit3_DmyPoly2, guardStaminaCutRate, guardRate;
			UInt16 blowingCorrection, atkPhysCorrection, atkMagCorrection, atkFireCorrection, atkThunCorrection, atkStamCorrection, guardAtkRateCorrection, guardBreakCorrection, atkThrowEscapeCorrection, atkSuperArmorCorrection, atkPhys, atkMag, atkFire, atkThun, atkStam, guardAtkRate, guardBreakRate, atkSuperArmor, atkThrowEscape, atkObj, throwTypeId;
			AttackHitType hit0_hitType, hit1_hitType, hit2_hitType, hit3_hitType;
			Byte hti0_Priority, hti1_Priority, hti2_Priority, hti3_Priority, damageLevel;
			AttackMapHit mapHitType;
			SByte guardCutCancelRate;
			AttackAttackAttributes atkAttribute;
			AttackParameterSpecialAttributes specialAttributes;
			BehaviorAttackType attackType;
			WeaponMaterialAttack atkMaterial;
			BehaviorAttackSize atkSize;
			WeaponMaterialDefend defMaterial;
			WeaponMaterialDefendSound defSfxMaterial;
			AttackHitSource hitSourceType;
			AttackThrowFlag throwFlag;
			Byte[] pad;

			/// <summary>A property of the class.</summary>
			public static readonly PropertyInfo
				Hit0_RadiusProperty = GetProperty<Attack>("Hit0_Radius"),
				Hit1_RadiusProperty = GetProperty<Attack>("Hit1_Radius"),
				Hit2_RadiusProperty = GetProperty<Attack>("Hit2_Radius"),
				Hit3_RadiusProperty = GetProperty<Attack>("Hit3_Radius"),
				KnockbackDistProperty = GetProperty<Attack>("KnockbackDist"),
				HitStopTimeProperty = GetProperty<Attack>("HitStopTime"),
				SpEffectId0Property = GetProperty<Attack>("SpEffectId0"),
				SpEffectId1Property = GetProperty<Attack>("SpEffectId1"),
				SpEffectId2Property = GetProperty<Attack>("SpEffectId2"),
				SpEffectId3Property = GetProperty<Attack>("SpEffectId3"),
				SpEffectId4Property = GetProperty<Attack>("SpEffectId4"),
				Hit0_DmyPoly1Property = GetProperty<Attack>("Hit0_DmyPoly1"),
				Hit1_DmyPoly1Property = GetProperty<Attack>("Hit1_DmyPoly1"),
				Hit2_DmyPoly1Property = GetProperty<Attack>("Hit2_DmyPoly1"),
				Hit3_DmyPoly1Property = GetProperty<Attack>("Hit3_DmyPoly1"),
				Hit0_DmyPoly2Property = GetProperty<Attack>("Hit0_DmyPoly2"),
				Hit1_DmyPoly2Property = GetProperty<Attack>("Hit1_DmyPoly2"),
				Hit2_DmyPoly2Property = GetProperty<Attack>("Hit2_DmyPoly2"),
				Hit3_DmyPoly2Property = GetProperty<Attack>("Hit3_DmyPoly2"),
				BlowingCorrectionProperty = GetProperty<Attack>("BlowingCorrection"),
				AtkPhysCorrectionProperty = GetProperty<Attack>("AtkPhysCorrection"),
				AtkMagCorrectionProperty = GetProperty<Attack>("AtkMagCorrection"),
				AtkFireCorrectionProperty = GetProperty<Attack>("AtkFireCorrection"),
				AtkThunCorrectionProperty = GetProperty<Attack>("AtkThunCorrection"),
				AtkStamCorrectionProperty = GetProperty<Attack>("AtkStamCorrection"),
				GuardAtkRateCorrectionProperty = GetProperty<Attack>("GuardAtkRateCorrection"),
				GuardBreakCorrectionProperty = GetProperty<Attack>("GuardBreakCorrection"),
				AtkThrowEscapeCorrectionProperty = GetProperty<Attack>("AtkThrowEscapeCorrection"),
				AtkSuperArmorCorrectionProperty = GetProperty<Attack>("AtkSuperArmorCorrection"),
				AtkPhysProperty = GetProperty<Attack>("AtkPhys"),
				AtkMagProperty = GetProperty<Attack>("AtkMag"),
				AtkFireProperty = GetProperty<Attack>("AtkFire"),
				AtkThunProperty = GetProperty<Attack>("AtkThun"),
				AtkStamProperty = GetProperty<Attack>("AtkStam"),
				GuardAtkRateProperty = GetProperty<Attack>("GuardAtkRate"),
				GuardBreakRateProperty = GetProperty<Attack>("GuardBreakRate"),
				AtkSuperArmorProperty = GetProperty<Attack>("AtkSuperArmor"),
				AtkThrowEscapeProperty = GetProperty<Attack>("AtkThrowEscape"),
				AtkObjProperty = GetProperty<Attack>("AtkObj"),
				GuardStaminaCutRateProperty = GetProperty<Attack>("GuardStaminaCutRate"),
				GuardRateProperty = GetProperty<Attack>("GuardRate"),
				ThrowTypeIdProperty = GetProperty<Attack>("ThrowTypeId"),
				Hit0_hitTypeProperty = GetProperty<Attack>("Hit0_hitType"),
				Hit1_hitTypeProperty = GetProperty<Attack>("Hit1_hitType"),
				Hit2_hitTypeProperty = GetProperty<Attack>("Hit2_hitType"),
				Hit3_hitTypeProperty = GetProperty<Attack>("Hit3_hitType"),
				Hti0_PriorityProperty = GetProperty<Attack>("Hti0_Priority"),
				Hti1_PriorityProperty = GetProperty<Attack>("Hti1_Priority"),
				Hti2_PriorityProperty = GetProperty<Attack>("Hti2_Priority"),
				Hti3_PriorityProperty = GetProperty<Attack>("Hti3_Priority"),
				DamageLevelProperty = GetProperty<Attack>("DamageLevel"),
				MapHitTypeProperty = GetProperty<Attack>("MapHitType"),
				GuardCutCancelRateProperty = GetProperty<Attack>("GuardCutCancelRate"),
				AtkAttributeProperty = GetProperty<Attack>("AtkAttribute"),
				SpecialAttributesProperty = GetProperty<Attack>("SpecialAttributes"),
				AttackTypeProperty = GetProperty<Attack>("AttackType"),
				AtkMaterialProperty = GetProperty<Attack>("AtkMaterial"),
				AtkSizeProperty = GetProperty<Attack>("AtkSize"),
				DefMaterialProperty = GetProperty<Attack>("DefMaterial"),
				DefSfxMaterialProperty = GetProperty<Attack>("DefSfxMaterial"),
				HitSourceTypeProperty = GetProperty<Attack>("HitSourceType"),
				ThrowFlagProperty = GetProperty<Attack>("ThrowFlag"),
				DisableGuardProperty = GetProperty<Attack>("DisableGuard"),
				DisableStaminaAttackProperty = GetProperty<Attack>("DisableStaminaAttack"),
				DisableHitSpEffectProperty = GetProperty<Attack>("DisableHitSpEffect"),
				IgnoreNotifyMissSwingForAIProperty = GetProperty<Attack>("IgnoreNotifyMissSwingForAI"),
				RepeatHitSfxProperty = GetProperty<Attack>("RepeatHitSfx"),
				IsArrowAtkProperty = GetProperty<Attack>("IsArrowAtk"),
				IsGhostAtkProperty = GetProperty<Attack>("IsGhostAtk"),
				IsDisableNoDamageProperty = GetProperty<Attack>("IsDisableNoDamage"),
				PadProperty = GetProperty<Attack>("Pad");

			/// <summary>0 radius per</summary>
			/// <remarks>
			/// Japanese short name: "あたり0 半径", Google translated: "0 radius per".
			/// Japanese description: "球、カプセルの半径", Google translated: "Radius sphere , the capsule".
			/// </remarks>
			[ParameterTableRowAttribute("hit0_Radius", index: 0, minimum: 0, maximum: 100, step: 0.01, sortOrder: 4000, unknown2: 1)]
			[DisplayName("0 radius per")]
			[Description("Radius sphere , the capsule")]
			[DefaultValue((Single)0)]
			public Single Hit0_Radius {
				get { return hit0_Radius; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for Hit0_Radius.");
					SetProperty(ref hit0_Radius, ref value, Hit0_RadiusProperty);
				}
			}

			/// <summary>1 radius per</summary>
			/// <remarks>
			/// Japanese short name: "あたり1 半径", Google translated: "1 radius per".
			/// Japanese description: "球、カプセルの半径", Google translated: "Radius sphere , the capsule".
			/// </remarks>
			[ParameterTableRowAttribute("hit1_Radius", index: 1, minimum: 0, maximum: 100, step: 0.01, sortOrder: 4500, unknown2: 1)]
			[DisplayName("1 radius per")]
			[Description("Radius sphere , the capsule")]
			[DefaultValue((Single)0)]
			public Single Hit1_Radius {
				get { return hit1_Radius; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for Hit1_Radius.");
					SetProperty(ref hit1_Radius, ref value, Hit1_RadiusProperty);
				}
			}

			/// <summary>2 radius per</summary>
			/// <remarks>
			/// Japanese short name: "あたり2 半径", Google translated: "2 radius per".
			/// Japanese description: "球、カプセルの半径", Google translated: "Radius sphere , the capsule".
			/// </remarks>
			[ParameterTableRowAttribute("hit2_Radius", index: 2, minimum: 0, maximum: 100, step: 0.01, sortOrder: 5000, unknown2: 1)]
			[DisplayName("2 radius per")]
			[Description("Radius sphere , the capsule")]
			[DefaultValue((Single)0)]
			public Single Hit2_Radius {
				get { return hit2_Radius; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for Hit2_Radius.");
					SetProperty(ref hit2_Radius, ref value, Hit2_RadiusProperty);
				}
			}

			/// <summary>3 radius per</summary>
			/// <remarks>
			/// Japanese short name: "あたり3 半径", Google translated: "3 radius per".
			/// Japanese description: "球、カプセルの半径", Google translated: "Radius sphere , the capsule".
			/// </remarks>
			[ParameterTableRowAttribute("hit3_Radius", index: 3, minimum: 0, maximum: 100, step: 0.01, sortOrder: 5500, unknown2: 1)]
			[DisplayName("3 radius per")]
			[Description("Radius sphere , the capsule")]
			[DefaultValue((Single)0)]
			public Single Hit3_Radius {
				get { return hit3_Radius; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for Hit3_Radius.");
					SetProperty(ref hit3_Radius, ref value, Hit3_RadiusProperty);
				}
			}

			/// <summary>Knock back distance [m]</summary>
			/// <remarks>
			/// Japanese short name: "ノックバック距離[m]", Google translated: "Knock back distance [m]".
			/// Japanese description: "ノックバック距離[m]", Google translated: "Knock back distance [m]".
			/// </remarks>
			[ParameterTableRowAttribute("knockbackDist", index: 4, minimum: 0, maximum: 99, step: 0.01, sortOrder: 5790, unknown2: 1)]
			[DisplayName("Knock back distance [m]")]
			[Description("Knock back distance [m]")]
			[DefaultValue((Single)0)]
			public Single KnockbackDist {
				get { return knockbackDist; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for KnockbackDist.");
					SetProperty(ref knockbackDist, ref value, KnockbackDistProperty);
				}
			}

			/// <summary>Hit stop time [s]</summary>
			/// <remarks>
			/// Japanese short name: "ヒットストップ時間[s]", Google translated: "Hit stop time [s]".
			/// Japanese description: "ヒットストップの停止時間[s]", Google translated: "Stop time of the hit stop [s]".
			/// </remarks>
			[ParameterTableRowAttribute("hitStopTime", index: 5, minimum: 0, maximum: 10, step: 0.01, sortOrder: 6110, unknown2: 1)]
			[DisplayName("Hit stop time [s]")]
			[Description("Stop time of the hit stop [s]")]
			[DefaultValue((Single)0)]
			public Single HitStopTime {
				get { return hitStopTime; }
				set {
					if ((double)value < 0 || (double)value > 10)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 10 for HitStopTime.");
					SetProperty(ref hitStopTime, ref value, HitStopTimeProperty);
				}
			}

			/// <summary>Special effects 0</summary>
			/// <remarks>
			/// Japanese short name: "特殊効果0", Google translated: "Special effects 0".
			/// Japanese description: "特殊効果パラメータで作成したＩＤを入れる", Google translated: "Insert the ID that you created in special effects parameters".
			/// </remarks>
			[ParameterTableRowAttribute("spEffectId0", index: 6, minimum: -1, maximum: 999999, step: 1, sortOrder: 6300, unknown2: 1)]
			[DisplayName("Special effects 0")]
			[Description("Insert the ID that you created in special effects parameters")]
			[DefaultValue((Int32)(-1))]
			public Int32 SpEffectId0 {
				get { return spEffectId0; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for SpEffectId0.");
					SetProperty(ref spEffectId0, ref value, SpEffectId0Property);
				}
			}

			/// <summary>Special effects 1</summary>
			/// <remarks>
			/// Japanese short name: "特殊効果1", Google translated: "Special effects 1".
			/// Japanese description: "特殊効果パラメータで作成したＩＤを入れる", Google translated: "Insert the ID that you created in special effects parameters".
			/// </remarks>
			[ParameterTableRowAttribute("spEffectId1", index: 7, minimum: -1, maximum: 999999, step: 1, sortOrder: 6400, unknown2: 1)]
			[DisplayName("Special effects 1")]
			[Description("Insert the ID that you created in special effects parameters")]
			[DefaultValue((Int32)(-1))]
			public Int32 SpEffectId1 {
				get { return spEffectId1; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for SpEffectId1.");
					SetProperty(ref spEffectId1, ref value, SpEffectId1Property);
				}
			}

			/// <summary>Special effects 2</summary>
			/// <remarks>
			/// Japanese short name: "特殊効果2", Google translated: "Special effects 2".
			/// Japanese description: "特殊効果パラメータで作成したＩＤを入れる", Google translated: "Insert the ID that you created in special effects parameters".
			/// </remarks>
			[ParameterTableRowAttribute("spEffectId2", index: 8, minimum: -1, maximum: 999999, step: 1, sortOrder: 6500, unknown2: 1)]
			[DisplayName("Special effects 2")]
			[Description("Insert the ID that you created in special effects parameters")]
			[DefaultValue((Int32)(-1))]
			public Int32 SpEffectId2 {
				get { return spEffectId2; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for SpEffectId2.");
					SetProperty(ref spEffectId2, ref value, SpEffectId2Property);
				}
			}

			/// <summary>Special effects 3</summary>
			/// <remarks>
			/// Japanese short name: "特殊効果3", Google translated: "Special effects 3".
			/// Japanese description: "特殊効果パラメータで作成したＩＤを入れる", Google translated: "Insert the ID that you created in special effects parameters".
			/// </remarks>
			[ParameterTableRowAttribute("spEffectId3", index: 9, minimum: -1, maximum: 999999, step: 1, sortOrder: 6600, unknown2: 1)]
			[DisplayName("Special effects 3")]
			[Description("Insert the ID that you created in special effects parameters")]
			[DefaultValue((Int32)(-1))]
			public Int32 SpEffectId3 {
				get { return spEffectId3; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for SpEffectId3.");
					SetProperty(ref spEffectId3, ref value, SpEffectId3Property);
				}
			}

			/// <summary>Special effects 4</summary>
			/// <remarks>
			/// Japanese short name: "特殊効果4", Google translated: "Special effects 4".
			/// Japanese description: "特殊効果パラメータで作成したＩＤを入れる", Google translated: "Insert the ID that you created in special effects parameters".
			/// </remarks>
			[ParameterTableRowAttribute("spEffectId4", index: 10, minimum: -1, maximum: 999999, step: 1, sortOrder: 6700, unknown2: 1)]
			[DisplayName("Special effects 4")]
			[Description("Insert the ID that you created in special effects parameters")]
			[DefaultValue((Int32)(-1))]
			public Int32 SpEffectId4 {
				get { return spEffectId4; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for SpEffectId4.");
					SetProperty(ref spEffectId4, ref value, SpEffectId4Property);
				}
			}

			/// <summary>0 Damipori 1 per</summary>
			/// <remarks>
			/// Japanese short name: "あたり0 ダミポリ1", Google translated: "0 Damipori 1 per".
			/// Japanese description: "球、カプセル位置のダミポリ", Google translated: "Damipori sphere , the capsule position".
			/// </remarks>
			[ParameterTableRowAttribute("hit0_DmyPoly1", index: 11, minimum: -1, maximum: 255, step: 1, sortOrder: 3800, unknown2: 1)]
			[DisplayName("0 Damipori 1 per")]
			[Description("Damipori sphere , the capsule position")]
			[DefaultValue((Int16)0)]
			public Int16 Hit0_DmyPoly1 {
				get { return hit0_DmyPoly1; }
				set {
					if ((double)value < -1 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 255 for Hit0_DmyPoly1.");
					SetProperty(ref hit0_DmyPoly1, ref value, Hit0_DmyPoly1Property);
				}
			}

			/// <summary>Per Damipori 1</summary>
			/// <remarks>
			/// Japanese short name: "あたり1 ダミポリ1", Google translated: "Per Damipori 1".
			/// Japanese description: "球、カプセル位置のダミポリ", Google translated: "Damipori sphere , the capsule position".
			/// </remarks>
			[ParameterTableRowAttribute("hit1_DmyPoly1", index: 12, minimum: -1, maximum: 255, step: 1, sortOrder: 4300, unknown2: 1)]
			[DisplayName("Per Damipori 1")]
			[Description("Damipori sphere , the capsule position")]
			[DefaultValue((Int16)0)]
			public Int16 Hit1_DmyPoly1 {
				get { return hit1_DmyPoly1; }
				set {
					if ((double)value < -1 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 255 for Hit1_DmyPoly1.");
					SetProperty(ref hit1_DmyPoly1, ref value, Hit1_DmyPoly1Property);
				}
			}

			/// <summary>1 per 2 Damipori</summary>
			/// <remarks>
			/// Japanese short name: "あたり2 ダミポリ1", Google translated: "1 per 2 Damipori".
			/// Japanese description: "球、カプセル位置のダミポリ", Google translated: "Damipori sphere , the capsule position".
			/// </remarks>
			[ParameterTableRowAttribute("hit2_DmyPoly1", index: 13, minimum: -1, maximum: 255, step: 1, sortOrder: 4800, unknown2: 1)]
			[DisplayName("1 per 2 Damipori")]
			[Description("Damipori sphere , the capsule position")]
			[DefaultValue((Int16)0)]
			public Int16 Hit2_DmyPoly1 {
				get { return hit2_DmyPoly1; }
				set {
					if ((double)value < -1 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 255 for Hit2_DmyPoly1.");
					SetProperty(ref hit2_DmyPoly1, ref value, Hit2_DmyPoly1Property);
				}
			}

			/// <summary>1 per 3 Damipori</summary>
			/// <remarks>
			/// Japanese short name: "あたり3 ダミポリ1", Google translated: "1 per 3 Damipori".
			/// Japanese description: "球、カプセル位置のダミポリ", Google translated: "Damipori sphere , the capsule position".
			/// </remarks>
			[ParameterTableRowAttribute("hit3_DmyPoly1", index: 14, minimum: -1, maximum: 255, step: 1, sortOrder: 5300, unknown2: 1)]
			[DisplayName("1 per 3 Damipori")]
			[Description("Damipori sphere , the capsule position")]
			[DefaultValue((Int16)0)]
			public Int16 Hit3_DmyPoly1 {
				get { return hit3_DmyPoly1; }
				set {
					if ((double)value < -1 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 255 for Hit3_DmyPoly1.");
					SetProperty(ref hit3_DmyPoly1, ref value, Hit3_DmyPoly1Property);
				}
			}

			/// <summary>0 Damipori 2 per</summary>
			/// <remarks>
			/// Japanese short name: "あたり0 ダミポリ2", Google translated: "0 Damipori 2 per".
			/// Japanese description: "カプセルのもうひとつの点の位置ダミポリ。-1だと球になる", Google translated: "Damipori position of the point of another capsule . And a sphere that it is -1".
			/// </remarks>
			[ParameterTableRowAttribute("hit0_DmyPoly2", index: 15, minimum: -1, maximum: 255, step: 1, sortOrder: 3900, unknown2: 1)]
			[DisplayName("0 Damipori 2 per")]
			[Description("Damipori position of the point of another capsule . And a sphere that it is -1")]
			[DefaultValue((Int16)0)]
			public Int16 Hit0_DmyPoly2 {
				get { return hit0_DmyPoly2; }
				set {
					if ((double)value < -1 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 255 for Hit0_DmyPoly2.");
					SetProperty(ref hit0_DmyPoly2, ref value, Hit0_DmyPoly2Property);
				}
			}

			/// <summary>1 Damipori 2 per</summary>
			/// <remarks>
			/// Japanese short name: "あたり1 ダミポリ2", Google translated: "1 Damipori 2 per".
			/// Japanese description: "カプセルのもうひとつの点の位置ダミポリ。-1だと球になる", Google translated: "Damipori position of the point of another capsule . And a sphere that it is -1".
			/// </remarks>
			[ParameterTableRowAttribute("hit1_DmyPoly2", index: 16, minimum: -1, maximum: 255, step: 1, sortOrder: 4400, unknown2: 1)]
			[DisplayName("1 Damipori 2 per")]
			[Description("Damipori position of the point of another capsule . And a sphere that it is -1")]
			[DefaultValue((Int16)0)]
			public Int16 Hit1_DmyPoly2 {
				get { return hit1_DmyPoly2; }
				set {
					if ((double)value < -1 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 255 for Hit1_DmyPoly2.");
					SetProperty(ref hit1_DmyPoly2, ref value, Hit1_DmyPoly2Property);
				}
			}

			/// <summary>2 Damipori 2 per</summary>
			/// <remarks>
			/// Japanese short name: "あたり2 ダミポリ2", Google translated: "2 Damipori 2 per".
			/// Japanese description: "カプセルのもうひとつの点の位置ダミポリ。-1だと球になる", Google translated: "Damipori position of the point of another capsule . And a sphere that it is -1".
			/// </remarks>
			[ParameterTableRowAttribute("hit2_DmyPoly2", index: 17, minimum: -1, maximum: 255, step: 1, sortOrder: 4900, unknown2: 1)]
			[DisplayName("2 Damipori 2 per")]
			[Description("Damipori position of the point of another capsule . And a sphere that it is -1")]
			[DefaultValue((Int16)0)]
			public Int16 Hit2_DmyPoly2 {
				get { return hit2_DmyPoly2; }
				set {
					if ((double)value < -1 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 255 for Hit2_DmyPoly2.");
					SetProperty(ref hit2_DmyPoly2, ref value, Hit2_DmyPoly2Property);
				}
			}

			/// <summary>3 Damipori 2 per</summary>
			/// <remarks>
			/// Japanese short name: "あたり3 ダミポリ2", Google translated: "3 Damipori 2 per".
			/// Japanese description: "カプセルのもうひとつの点の位置ダミポリ。-1だと球になる", Google translated: "Damipori position of the point of another capsule . And a sphere that it is -1".
			/// </remarks>
			[ParameterTableRowAttribute("hit3_DmyPoly2", index: 18, minimum: -1, maximum: 255, step: 1, sortOrder: 5400, unknown2: 1)]
			[DisplayName("3 Damipori 2 per")]
			[Description("Damipori position of the point of another capsule . And a sphere that it is -1")]
			[DefaultValue((Int16)0)]
			public Int16 Hit3_DmyPoly2 {
				get { return hit3_DmyPoly2; }
				set {
					if ((double)value < -1 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 255 for Hit3_DmyPoly2.");
					SetProperty(ref hit3_DmyPoly2, ref value, Hit3_DmyPoly2Property);
				}
			}

			/// <summary>Correction value blowing off</summary>
			/// <remarks>
			/// Japanese short name: "吹き飛ばし補正値", Google translated: "Correction value blowing off".
			/// Japanese description: "吹き飛ばす時の補正値", Google translated: "Correction value when the blow off".
			/// </remarks>
			[ParameterTableRowAttribute("blowingCorrection", index: 19, minimum: 0, maximum: 60000, step: 1, sortOrder: 500, unknown2: 1)]
			[DisplayName("Correction value blowing off")]
			[Description("Correction value when the blow off")]
			[DefaultValue((UInt16)0)]
			public UInt16 BlowingCorrection {
				get { return blowingCorrection; }
				set {
					if ((double)value < 0 || (double)value > 60000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 60000 for BlowingCorrection.");
					SetProperty(ref blowingCorrection, ref value, BlowingCorrectionProperty);
				}
			}

			/// <summary>Physical Attack correction value</summary>
			/// <remarks>
			/// Japanese short name: "物理攻撃力補正値", Google translated: "Physical Attack correction value".
			/// Japanese description: "PCのみ。物理攻撃力基本値に掛ける倍率", Google translated: "Only PC. Magnification to apply to the physical attack power base value".
			/// </remarks>
			[ParameterTableRowAttribute("atkPhysCorrection", index: 20, minimum: 0, maximum: 60000, step: 1, sortOrder: 2300, unknown2: 1)]
			[DisplayName("Physical Attack correction value")]
			[Description("Only PC. Magnification to apply to the physical attack power base value")]
			[DefaultValue((UInt16)0)]
			public UInt16 AtkPhysCorrection {
				get { return atkPhysCorrection; }
				set {
					if ((double)value < 0 || (double)value > 60000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 60000 for AtkPhysCorrection.");
					SetProperty(ref atkPhysCorrection, ref value, AtkPhysCorrectionProperty);
				}
			}

			/// <summary>Magic Attack correction value</summary>
			/// <remarks>
			/// Japanese short name: "魔法攻撃力補正値", Google translated: "Magic Attack correction value".
			/// Japanese description: "PCのみ。魔法攻撃力に掛ける倍率（弓の場合は、飛び道具を補正）", Google translated: "Only PC. (In the case of bow , correct the missile ) magnification to be applied to Magic Attack".
			/// </remarks>
			[ParameterTableRowAttribute("atkMagCorrection", index: 21, minimum: 0, maximum: 60000, step: 1, sortOrder: 2400, unknown2: 1)]
			[DisplayName("Magic Attack correction value")]
			[Description("Only PC. (In the case of bow , correct the missile ) magnification to be applied to Magic Attack")]
			[DefaultValue((UInt16)0)]
			public UInt16 AtkMagCorrection {
				get { return atkMagCorrection; }
				set {
					if ((double)value < 0 || (double)value > 60000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 60000 for AtkMagCorrection.");
					SetProperty(ref atkMagCorrection, ref value, AtkMagCorrectionProperty);
				}
			}

			/// <summary>Flame attack power correction value</summary>
			/// <remarks>
			/// Japanese short name: "炎攻撃力補正値", Google translated: "Flame attack power correction value".
			/// Japanese description: "PCのみ。炎攻撃力に掛ける倍率（弓の場合は、飛び道具を補正）", Google translated: "Only PC. (In the case of bow , correct the missile ) ratio, which is subjected to fire attack power".
			/// </remarks>
			[ParameterTableRowAttribute("atkFireCorrection", index: 22, minimum: 0, maximum: 60000, step: 1, sortOrder: 2500, unknown2: 1)]
			[DisplayName("Flame attack power correction value")]
			[Description("Only PC. (In the case of bow , correct the missile ) ratio, which is subjected to fire attack power")]
			[DefaultValue((UInt16)0)]
			public UInt16 AtkFireCorrection {
				get { return atkFireCorrection; }
				set {
					if ((double)value < 0 || (double)value > 60000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 60000 for AtkFireCorrection.");
					SetProperty(ref atkFireCorrection, ref value, AtkFireCorrectionProperty);
				}
			}

			/// <summary>Blitz attack power correction value</summary>
			/// <remarks>
			/// Japanese short name: "電撃攻撃力補正値", Google translated: "Blitz attack power correction value".
			/// Japanese description: "PCのみ。電撃攻撃力に掛ける倍率（弓の場合は、飛び道具を補正）", Google translated: "Only PC. (In the case of bow , correct the missile ) ratio, which is subjected to shock attack force".
			/// </remarks>
			[ParameterTableRowAttribute("atkThunCorrection", index: 23, minimum: 0, maximum: 60000, step: 1, sortOrder: 2510, unknown2: 1)]
			[DisplayName("Blitz attack power correction value")]
			[Description("Only PC. (In the case of bow , correct the missile ) ratio, which is subjected to shock attack force")]
			[DefaultValue((UInt16)0)]
			public UInt16 AtkThunCorrection {
				get { return atkThunCorrection; }
				set {
					if ((double)value < 0 || (double)value > 60000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 60000 for AtkThunCorrection.");
					SetProperty(ref atkThunCorrection, ref value, AtkThunCorrectionProperty);
				}
			}

			/// <summary>Stamina attack power correction value</summary>
			/// <remarks>
			/// Japanese short name: "スタミナ攻撃力補正値", Google translated: "Stamina attack power correction value".
			/// Japanese description: "PCのみ。スタミナ攻撃力に掛ける倍率", Google translated: "Only PC. Magnification to apply to stamina attack force".
			/// </remarks>
			[ParameterTableRowAttribute("atkStamCorrection", index: 24, minimum: 0, maximum: 60000, step: 1, sortOrder: 2600, unknown2: 1)]
			[DisplayName("Stamina attack power correction value")]
			[Description("Only PC. Magnification to apply to stamina attack force")]
			[DefaultValue((UInt16)0)]
			public UInt16 AtkStamCorrection {
				get { return atkStamCorrection; }
				set {
					if ((double)value < 0 || (double)value > 60000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 60000 for AtkStamCorrection.");
					SetProperty(ref atkStamCorrection, ref value, AtkStamCorrectionProperty);
				}
			}

			/// <summary>Repelling attack power correction value</summary>
			/// <remarks>
			/// Japanese short name: "はじき攻撃力補正値", Google translated: "Repelling attack power correction value".
			/// Japanese description: "PCのみ。1のみ", Google translated: "Only PC. Only 1".
			/// </remarks>
			[ParameterTableRowAttribute("guardAtkRateCorrection", index: 25, minimum: 0, maximum: 60000, step: 1, sortOrder: 2900, unknown2: 1)]
			[DisplayName("Repelling attack power correction value")]
			[Description("Only PC. Only 1")]
			[DefaultValue((UInt16)0)]
			public UInt16 GuardAtkRateCorrection {
				get { return guardAtkRateCorrection; }
				set {
					if ((double)value < 0 || (double)value > 60000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 60000 for GuardAtkRateCorrection.");
					SetProperty(ref guardAtkRateCorrection, ref value, GuardAtkRateCorrectionProperty);
				}
			}

			/// <summary>Repelling defense force correction value</summary>
			/// <remarks>
			/// Japanese short name: "はじき防御力補正値", Google translated: "Repelling defense force correction value".
			/// Japanese description: "PCのみ。攻撃のはじかれ基本値に掛ける倍率", Google translated: "Only PC. Magnification to be applied to the base value of the attack is repelled".
			/// </remarks>
			[ParameterTableRowAttribute("guardBreakCorrection", index: 26, minimum: 0, maximum: 60000, step: 1, sortOrder: 3000, unknown2: 1)]
			[DisplayName("Repelling defense force correction value")]
			[Description("Only PC. Magnification to be applied to the base value of the attack is repelled")]
			[DefaultValue((UInt16)0)]
			public UInt16 GuardBreakCorrection {
				get { return guardBreakCorrection; }
				set {
					if ((double)value < 0 || (double)value > 60000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 60000 for GuardBreakCorrection.");
					SetProperty(ref guardBreakCorrection, ref value, GuardBreakCorrectionProperty);
				}
			}

			/// <summary>Attack force correction value missing throw</summary>
			/// <remarks>
			/// Japanese short name: "投げ抜け攻撃力補正値", Google translated: "Attack force correction value missing throw".
			/// Japanese description: "投げ抜け攻撃に対する武器補正値", Google translated: "Correction value for the weapon attack missing throw".
			/// </remarks>
			[ParameterTableRowAttribute("atkThrowEscapeCorrection", index: 27, minimum: 0, maximum: 60000, step: 1, sortOrder: 3630, unknown2: 1)]
			[DisplayName("Attack force correction value missing throw")]
			[Description("Correction value for the weapon attack missing throw")]
			[DefaultValue((UInt16)0)]
			public UInt16 AtkThrowEscapeCorrection {
				get { return atkThrowEscapeCorrection; }
				set {
					if ((double)value < 0 || (double)value > 60000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 60000 for AtkThrowEscapeCorrection.");
					SetProperty(ref atkThrowEscapeCorrection, ref value, AtkThrowEscapeCorrectionProperty);
				}
			}

			/// <summary>SA attack power correction value</summary>
			/// <remarks>
			/// Japanese short name: "SA攻撃力補正値", Google translated: "SA attack power correction value".
			/// Japanese description: "PCのみ。武器に設定された【基本値】にかける補正値", Google translated: "Only PC. Correction value to be applied is set in the weapon to the base value ]".
			/// </remarks>
			[ParameterTableRowAttribute("atkSuperArmorCorrection", index: 28, minimum: 0, maximum: 60000, step: 1, sortOrder: 3110, unknown2: 1)]
			[DisplayName("SA attack power correction value")]
			[Description("Only PC. Correction value to be applied is set in the weapon to the base value ]")]
			[DefaultValue((UInt16)0)]
			public UInt16 AtkSuperArmorCorrection {
				get { return atkSuperArmorCorrection; }
				set {
					if ((double)value < 0 || (double)value > 60000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 60000 for AtkSuperArmorCorrection.");
					SetProperty(ref atkSuperArmorCorrection, ref value, AtkSuperArmorCorrectionProperty);
				}
			}

			/// <summary>Physical Attack</summary>
			/// <remarks>
			/// Japanese short name: "物理攻撃力", Google translated: "Physical Attack".
			/// Japanese description: "NPCのみ。物理攻撃の基本ダメージ", Google translated: "Only NPC. Base damage of a physical attack".
			/// </remarks>
			[ParameterTableRowAttribute("atkPhys", index: 29, minimum: 0, maximum: 9999, step: 1, sortOrder: 600, unknown2: 1)]
			[DisplayName("Physical Attack")]
			[Description("Only NPC. Base damage of a physical attack")]
			[DefaultValue((UInt16)0)]
			public UInt16 AtkPhys {
				get { return atkPhys; }
				set {
					if ((double)value < 0 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9999 for AtkPhys.");
					SetProperty(ref atkPhys, ref value, AtkPhysProperty);
				}
			}

			/// <summary>Magic Attack</summary>
			/// <remarks>
			/// Japanese short name: "魔法攻撃力", Google translated: "Magic Attack".
			/// Japanese description: "NPCのみ。魔法攻撃の追加ダメージ", Google translated: "Only NPC. Additional damage of magic attack".
			/// </remarks>
			[ParameterTableRowAttribute("atkMag", index: 30, minimum: 0, maximum: 9999, step: 1, sortOrder: 700, unknown2: 1)]
			[DisplayName("Magic Attack")]
			[Description("Only NPC. Additional damage of magic attack")]
			[DefaultValue((UInt16)0)]
			public UInt16 AtkMag {
				get { return atkMag; }
				set {
					if ((double)value < 0 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9999 for AtkMag.");
					SetProperty(ref atkMag, ref value, AtkMagProperty);
				}
			}

			/// <summary>Flame attack power</summary>
			/// <remarks>
			/// Japanese short name: "炎攻撃力", Google translated: "Flame attack power".
			/// Japanese description: "NPCのみ。炎攻撃の追加ダメージ", Google translated: "Only NPC. Additional damage of fire attack".
			/// </remarks>
			[ParameterTableRowAttribute("atkFire", index: 31, minimum: 0, maximum: 9999, step: 1, sortOrder: 800, unknown2: 1)]
			[DisplayName("Flame attack power")]
			[Description("Only NPC. Additional damage of fire attack")]
			[DefaultValue((UInt16)0)]
			public UInt16 AtkFire {
				get { return atkFire; }
				set {
					if ((double)value < 0 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9999 for AtkFire.");
					SetProperty(ref atkFire, ref value, AtkFireProperty);
				}
			}

			/// <summary>Blitz attack power</summary>
			/// <remarks>
			/// Japanese short name: "電撃攻撃力", Google translated: "Blitz attack power".
			/// Japanese description: "NPCのみ。電撃攻撃の追加ダメージ", Google translated: "Only NPC. Additional damage lightning attack".
			/// </remarks>
			[ParameterTableRowAttribute("atkThun", index: 32, minimum: 0, maximum: 9999, step: 1, sortOrder: 810, unknown2: 1)]
			[DisplayName("Blitz attack power")]
			[Description("Only NPC. Additional damage lightning attack")]
			[DefaultValue((UInt16)0)]
			public UInt16 AtkThun {
				get { return atkThun; }
				set {
					if ((double)value < 0 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9999 for AtkThun.");
					SetProperty(ref atkThun, ref value, AtkThunProperty);
				}
			}

			/// <summary>Stamina attack power</summary>
			/// <remarks>
			/// Japanese short name: "スタミナ攻撃力", Google translated: "Stamina attack power".
			/// Japanese description: "NPCのみ。敵（プレイヤー）のスタミナに対するダメージ量", Google translated: "Only NPC. The amount of damage to the enemy of stamina ( player )".
			/// </remarks>
			[ParameterTableRowAttribute("atkStam", index: 33, minimum: 0, maximum: 999, step: 1, sortOrder: 900, unknown2: 1)]
			[DisplayName("Stamina attack power")]
			[Description("Only NPC. The amount of damage to the enemy of stamina ( player )")]
			[DefaultValue((UInt16)0)]
			public UInt16 AtkStam {
				get { return atkStam; }
				set {
					if ((double)value < 0 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for AtkStam.");
					SetProperty(ref atkStam, ref value, AtkStamProperty);
				}
			}

			/// <summary>Repelling force attack</summary>
			/// <remarks>
			/// Japanese short name: "はじき攻撃力", Google translated: "Repelling force attack".
			/// Japanese description: "NPCのみ。はじき値", Google translated: "Only NPC. Value repelling".
			/// </remarks>
			[ParameterTableRowAttribute("guardAtkRate", index: 34, minimum: 0, maximum: 999, step: 1, sortOrder: 1200, unknown2: 1)]
			[DisplayName("Repelling force attack")]
			[Description("Only NPC. Value repelling")]
			[DefaultValue((UInt16)0)]
			public UInt16 GuardAtkRate {
				get { return guardAtkRate; }
				set {
					if ((double)value < 0 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for GuardAtkRate.");
					SetProperty(ref guardAtkRate, ref value, GuardAtkRateProperty);
				}
			}

			/// <summary>Repelling Defense</summary>
			/// <remarks>
			/// Japanese short name: "はじき防御力", Google translated: "Repelling Defense".
			/// Japanese description: "NPCのみ。攻撃がはじかれるかどうかの判定に利用する値", Google translated: "Only NPC. Value to be used to determine attack of whether being repelled".
			/// </remarks>
			[ParameterTableRowAttribute("guardBreakRate", index: 35, minimum: 0, maximum: 999, step: 1, sortOrder: 1300, unknown2: 1)]
			[DisplayName("Repelling Defense")]
			[Description("Only NPC. Value to be used to determine attack of whether being repelled")]
			[DefaultValue((UInt16)0)]
			public UInt16 GuardBreakRate {
				get { return guardBreakRate; }
				set {
					if ((double)value < 0 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for GuardBreakRate.");
					SetProperty(ref guardBreakRate, ref value, GuardBreakRateProperty);
				}
			}

			/// <summary>SA attack power</summary>
			/// <remarks>
			/// Japanese short name: "SA攻撃力", Google translated: "SA attack power".
			/// Japanese description: "NPCのみ。SAブレイク計算式に利用すする値", Google translated: "Only NPC. Values ​​that you use to sip SA break formula".
			/// </remarks>
			[ParameterTableRowAttribute("atkSuperArmor", index: 36, minimum: 0, maximum: 999, step: 1, sortOrder: 1310, unknown2: 1)]
			[DisplayName("SA attack power")]
			[Description("Only NPC. Values ​​that you use to sip SA break formula")]
			[DefaultValue((UInt16)0)]
			public UInt16 AtkSuperArmor {
				get { return atkSuperArmor; }
				set {
					if ((double)value < 0 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for AtkSuperArmor.");
					SetProperty(ref atkSuperArmor, ref value, AtkSuperArmorProperty);
				}
			}

			/// <summary>Attack extraction force throw</summary>
			/// <remarks>
			/// Japanese short name: "投げ抜け攻撃力", Google translated: "Attack extraction force throw".
			/// Japanese description: "投げ抜け攻撃力", Google translated: "Attack extraction force throw".
			/// </remarks>
			[ParameterTableRowAttribute("atkThrowEscape", index: 37, minimum: 0, maximum: 999, step: 1, sortOrder: 3640, unknown2: 1)]
			[DisplayName("Attack extraction force throw")]
			[Description("Attack extraction force throw")]
			[DefaultValue((UInt16)0)]
			public UInt16 AtkThrowEscape {
				get { return atkThrowEscape; }
				set {
					if ((double)value < 0 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for AtkThrowEscape.");
					SetProperty(ref atkThrowEscape, ref value, AtkThrowEscapeProperty);
				}
			}

			/// <summary>Object attack power</summary>
			/// <remarks>
			/// Japanese short name: "オブジェ攻撃力", Google translated: "Object attack power".
			/// Japanese description: "ＯＢＪに対する攻撃力", Google translated: "Attack force on OBJ".
			/// </remarks>
			[ParameterTableRowAttribute("atkObj", index: 38, minimum: 0, maximum: 999, step: 1, sortOrder: 2200, unknown2: 1)]
			[DisplayName("Object attack power")]
			[Description("Attack force on OBJ")]
			[DefaultValue((UInt16)0)]
			public UInt16 AtkObj {
				get { return atkObj; }
				set {
					if ((double)value < 0 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for AtkObj.");
					SetProperty(ref atkObj, ref value, AtkObjProperty);
				}
			}

			/// <summary>Guard when stamina cut rate correction</summary>
			/// <remarks>
			/// Japanese short name: "ガード時スタミナカット率補正", Google translated: "Guard when stamina cut rate correction".
			/// Japanese description: "武器パラメータ、ＮＰＣパラメータに設定されている【ガード時スタミナカット率】を補正する", Google translated: "I corrected is set weapon parameters , to NPC parameters [ guard when stamina cut rates ]".
			/// </remarks>
			[ParameterTableRowAttribute("guardStaminaCutRate", index: 39, minimum: -100, maximum: 9999, step: 1, sortOrder: 3200, unknown2: 1)]
			[DisplayName("Guard when stamina cut rate correction")]
			[Description("I corrected is set weapon parameters , to NPC parameters [ guard when stamina cut rates ]")]
			[DefaultValue((Int16)0)]
			public Int16 GuardStaminaCutRate {
				get { return guardStaminaCutRate; }
				set {
					if ((double)value < -100 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -100 to 9999 for GuardStaminaCutRate.");
					SetProperty(ref guardStaminaCutRate, ref value, GuardStaminaCutRateProperty);
				}
			}

			/// <summary>Guard magnification</summary>
			/// <remarks>
			/// Japanese short name: "ガード倍率", Google translated: "Guard magnification".
			/// Japanese description: "ＮＰＣ、武器パラメータで設定してあるガード性能を一律で補正を掛ける0で、1倍／100で、2倍／－100で、0　にパラメータが増減するようにするガード倍率　=　（ガード倍率/100　+　1）", Google translated: "0 of applying a correction in uniform , in 1x / 100 , 2 times / -100 , guard magnification parameters so as to increase or decrease to 0 = and ( guard magnification / 100 guard performance that is set NPC, weapons parameters + 1)".
			/// </remarks>
			[ParameterTableRowAttribute("guardRate", index: 40, minimum: -100, maximum: 999, step: 1, sortOrder: 3500, unknown2: 1)]
			[DisplayName("Guard magnification")]
			[Description("0 of applying a correction in uniform , in 1x / 100 , 2 times / -100 , guard magnification parameters so as to increase or decrease to 0 = and ( guard magnification / 100 guard performance that is set NPC, weapons parameters + 1)")]
			[DefaultValue((Int16)0)]
			public Int16 GuardRate {
				get { return guardRate; }
				set {
					if ((double)value < -100 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -100 to 999 for GuardRate.");
					SetProperty(ref guardRate, ref value, GuardRateProperty);
				}
			}

			/// <summary>Type ID throw</summary>
			/// <remarks>
			/// Japanese short name: "投げタイプID", Google translated: "Type ID throw".
			/// Japanese description: "投げパラメータと紐付けされているID", Google translated: "ID that is a string with the parameters and throw".
			/// </remarks>
			[ParameterTableRowAttribute("throwTypeId", index: 41, minimum: 0, maximum: 9999, step: 1, sortOrder: 3620, unknown2: 1)]
			[DisplayName("Type ID throw")]
			[Description("ID that is a string with the parameters and throw")]
			[DefaultValue((UInt16)0)]
			public UInt16 ThrowTypeId {
				get { return throwTypeId; }
				set {
					if ((double)value < 0 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9999 for ThrowTypeId.");
					SetProperty(ref throwTypeId, ref value, ThrowTypeIdProperty);
				}
			}

			/// <summary>0 sites per</summary>
			/// <remarks>
			/// Japanese short name: "あたり0 部位", Google translated: "0 sites per".
			/// Japanese description: "あたり部位", Google translated: "Per site".
			/// </remarks>
			[ParameterTableRowAttribute("hit0_hitType", index: 42, minimum: 0, maximum: 255, step: 1, sortOrder: 4100, unknown2: 1)]
			[DisplayName("0 sites per")]
			[Description("Per site")]
			[DefaultValue((AttackHitType)0)]
			public AttackHitType Hit0_hitType {
				get { return hit0_hitType; }
				set { SetProperty(ref hit0_hitType, ref value, Hit0_hitTypeProperty); }
			}

			/// <summary>Per site</summary>
			/// <remarks>
			/// Japanese short name: "あたり1 部位", Google translated: "Per site".
			/// Japanese description: "あたり部位", Google translated: "Per site".
			/// </remarks>
			[ParameterTableRowAttribute("hit1_hitType", index: 43, minimum: 0, maximum: 255, step: 1, sortOrder: 4600, unknown2: 1)]
			[DisplayName("Per site")]
			[Description("Per site")]
			[DefaultValue((AttackHitType)0)]
			public AttackHitType Hit1_hitType {
				get { return hit1_hitType; }
				set { SetProperty(ref hit1_hitType, ref value, Hit1_hitTypeProperty); }
			}

			/// <summary>Two sites per</summary>
			/// <remarks>
			/// Japanese short name: "あたり2 部位", Google translated: "Two sites per".
			/// Japanese description: "あたり部位", Google translated: "Per site".
			/// </remarks>
			[ParameterTableRowAttribute("hit2_hitType", index: 44, minimum: 0, maximum: 255, step: 1, sortOrder: 5100, unknown2: 1)]
			[DisplayName("Two sites per")]
			[Description("Per site")]
			[DefaultValue((AttackHitType)0)]
			public AttackHitType Hit2_hitType {
				get { return hit2_hitType; }
				set { SetProperty(ref hit2_hitType, ref value, Hit2_hitTypeProperty); }
			}

			/// <summary>3 sites per</summary>
			/// <remarks>
			/// Japanese short name: "あたり3 部位", Google translated: "3 sites per".
			/// Japanese description: "あたり部位", Google translated: "Per site".
			/// </remarks>
			[ParameterTableRowAttribute("hit3_hitType", index: 45, minimum: 0, maximum: 255, step: 1, sortOrder: 5600, unknown2: 1)]
			[DisplayName("3 sites per")]
			[Description("Per site")]
			[DefaultValue((AttackHitType)0)]
			public AttackHitType Hit3_hitType {
				get { return hit3_hitType; }
				set { SetProperty(ref hit3_hitType, ref value, Hit3_hitTypeProperty); }
			}

			/// <summary>0 Priority per</summary>
			/// <remarks>
			/// Japanese short name: "あたり0 優先順位", Google translated: "0 Priority per".
			/// Japanese description: "優先度。同時に2つ以上のあたりがあたった場合、優先度が高いほうを採用する。", Google translated: "Priority . If the per two or more hits at the same time , I want to adopt a more high priority .".
			/// </remarks>
			[ParameterTableRowAttribute("hti0_Priority", index: 46, minimum: 0, maximum: 255, step: 1, sortOrder: 4200, unknown2: 5)]
			[DisplayName("0 Priority per")]
			[Description("Priority . If the per two or more hits at the same time , I want to adopt a more high priority .")]
			[DefaultValue((Byte)0)]
			public Byte Hti0_Priority {
				get { return hti0_Priority; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for Hti0_Priority.");
					SetProperty(ref hti0_Priority, ref value, Hti0_PriorityProperty);
				}
			}

			/// <summary>Priority 1 per</summary>
			/// <remarks>
			/// Japanese short name: "あたり1 優先順位", Google translated: "Priority 1 per".
			/// Japanese description: "優先度。同時に2つ以上のあたりがあたった場合、優先度が高いほうを採用する。", Google translated: "Priority . If the per two or more hits at the same time , I want to adopt a more high priority .".
			/// </remarks>
			[ParameterTableRowAttribute("hti1_Priority", index: 47, minimum: 0, maximum: 255, step: 1, sortOrder: 4700, unknown2: 5)]
			[DisplayName("Priority 1 per")]
			[Description("Priority . If the per two or more hits at the same time , I want to adopt a more high priority .")]
			[DefaultValue((Byte)0)]
			public Byte Hti1_Priority {
				get { return hti1_Priority; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for Hti1_Priority.");
					SetProperty(ref hti1_Priority, ref value, Hti1_PriorityProperty);
				}
			}

			/// <summary>Priority 2 per</summary>
			/// <remarks>
			/// Japanese short name: "あたり2 優先順位", Google translated: "Priority 2 per".
			/// Japanese description: "優先度。同時に2つ以上のあたりがあたった場合、優先度が高いほうを採用する。", Google translated: "Priority . If the per two or more hits at the same time , I want to adopt a more high priority .".
			/// </remarks>
			[ParameterTableRowAttribute("hti2_Priority", index: 48, minimum: 0, maximum: 255, step: 1, sortOrder: 5200, unknown2: 5)]
			[DisplayName("Priority 2 per")]
			[Description("Priority . If the per two or more hits at the same time , I want to adopt a more high priority .")]
			[DefaultValue((Byte)0)]
			public Byte Hti2_Priority {
				get { return hti2_Priority; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for Hti2_Priority.");
					SetProperty(ref hti2_Priority, ref value, Hti2_PriorityProperty);
				}
			}

			/// <summary>Priority 3 per</summary>
			/// <remarks>
			/// Japanese short name: "あたり3 優先順位", Google translated: "Priority 3 per".
			/// Japanese description: "優先度。同時に2つ以上のあたりがあたった場合、優先度が高いほうを採用する。", Google translated: "Priority . If the per two or more hits at the same time , I want to adopt a more high priority .".
			/// </remarks>
			[ParameterTableRowAttribute("hti3_Priority", index: 49, minimum: 0, maximum: 255, step: 1, sortOrder: 5700, unknown2: 5)]
			[DisplayName("Priority 3 per")]
			[Description("Priority . If the per two or more hits at the same time , I want to adopt a more high priority .")]
			[DefaultValue((Byte)0)]
			public Byte Hti3_Priority {
				get { return hti3_Priority; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for Hti3_Priority.");
					SetProperty(ref hti3_Priority, ref value, Hti3_PriorityProperty);
				}
			}

			/// <summary>Damage Level</summary>
			/// <remarks>
			/// Japanese short name: "ダメージレベル", Google translated: "Damage Level".
			/// Japanese description: "攻撃したとき、敵にどのダメージモーションを再生するか？を決める.", Google translated: "When you attack , you can either play back the motion which damage to the enemy ? I decide .".
			/// </remarks>
			[ParameterTableRowAttribute("dmgLevel", index: 50, minimum: 0, maximum: 100, step: 1, sortOrder: 100, unknown2: 1)]
			[DisplayName("Damage Level")]
			[Description("When you attack , you can either play back the motion which damage to the enemy ? I decide .")]
			[DefaultValue((Byte)0)]
			public Byte DamageLevel {
				get { return damageLevel; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for " + DamageLevelProperty.Name + ".");
					SetProperty(ref damageLevel, ref value, DamageLevelProperty);
				}
			}

			/// <summary>Per map reference</summary>
			/// <remarks>
			/// Japanese short name: "マップあたり参照", Google translated: "Per map reference".
			/// Japanese description: "攻撃あたりが、どのマップあたりを見るか？を設定", Google translated: "Per attack , or see which per map ? Set".
			/// </remarks>
			[ParameterTableRowAttribute("mapHitType", index: 51, minimum: 0, maximum: 255, step: 1, sortOrder: 300, unknown2: 1)]
			[DisplayName("Per map reference")]
			[Description("Per attack , or see which per map ? Set")]
			[DefaultValue((AttackMapHit)0)]
			public AttackMapHit MapHitType {
				get { return mapHitType; }
				set { SetProperty(ref mapHitType, ref value, MapHitTypeProperty); }
			}

			/// <summary>Guard cut rate disabled magnification</summary>
			/// <remarks>
			/// Japanese short name: "ガードカット率無効化倍率", Google translated: "Guard cut rate disabled magnification".
			/// Japanese description: "ガードカット率無効化倍率（－100～100）　→0のとき通常／－100で完全無効化／100で相手の防御効果倍増 　→－50とすれば、100％カットの盾が、50％カットになります", Google translated: "If the protective effect of doubling → ​​-50 opponent completely disable / 100 / -100 when normal (-100 ~ 100) → 0 guard cut rates disabled magnification , shield cut of 100% , 50 % cut will".
			/// </remarks>
			[ParameterTableRowAttribute("guardCutCancelRate", index: 52, minimum: -100, maximum: 100, step: 1, sortOrder: 1400, unknown2: 1)]
			[DisplayName("Guard cut rate disabled magnification")]
			[Description("If the protective effect of doubling → ​​-50 opponent completely disable / 100 / -100 when normal (-100 ~ 100) → 0 guard cut rates disabled magnification , shield cut of 100% , 50 % cut will")]
			[DefaultValue((SByte)0)]
			public SByte GuardCutCancelRate {
				get { return guardCutCancelRate; }
				set {
					if ((double)value < -100 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -100 to 100 for GuardCutCancelRate.");
					SetProperty(ref guardCutCancelRate, ref value, GuardCutCancelRateProperty);
				}
			}

			/// <summary>Physical attributes</summary>
			/// <remarks>
			/// Japanese short name: "物理属性", Google translated: "Physical attributes".
			/// Japanese description: "攻撃に設定する物理属性", Google translated: "Physical attributes to be set for the attack".
			/// </remarks>
			[ParameterTableRowAttribute("atkAttribute", index: 53, minimum: 0, maximum: 255, step: 1, sortOrder: 1510, unknown2: 1)]
			[DisplayName("Physical attributes")]
			[Description("Physical attributes to be set for the attack")]
			[DefaultValue((AttackAttackAttributes)0)]
			public AttackAttackAttributes AtkAttribute {
				get { return atkAttribute; }
				set { SetProperty(ref atkAttribute, ref value, AtkAttributeProperty); }
			}

			/// <summary>Special attributes</summary>
			/// <remarks>
			/// Japanese short name: "特殊属性", Google translated: "Special attributes".
			/// Japanese description: "攻撃に設定する特殊属性", Google translated: "Special attributes to be set for the attack".
			/// </remarks>
			[ParameterTableRowAttribute("spAttribute", index: 54, minimum: 0, maximum: 255, step: 1, sortOrder: 1520, unknown2: 1)]
			[DisplayName("Special attributes")]
			[Description("Special attributes to be set for the attack")]
			[DefaultValue((AttackParameterSpecialAttributes)0)]
			public AttackParameterSpecialAttributes SpecialAttributes {
				get { return specialAttributes; }
				set { SetProperty(ref specialAttributes, ref value, SpecialAttributesProperty); }
			}

			/// <summary>Attack attribute [SFX / SE]</summary>
			/// <remarks>
			/// Japanese short name: "攻撃属性[SFX/SE]", Google translated: "Attack attribute [SFX / SE]".
			/// Japanese description: "攻撃時のSFX/SEを指定(属性、材質、サイズで1セット)", Google translated: "( One set of attributes , material , size ) specifies the SFX / SE of attack during".
			/// </remarks>
			[ParameterTableRowAttribute("atkType", index: 55, minimum: 0, maximum: 255, step: 1, sortOrder: 1600, unknown2: 1)]
			[DisplayName("Attack attribute [SFX / SE]")]
			[Description("( One set of attributes , material , size ) specifies the SFX / SE of attack during")]
			[DefaultValue((BehaviorAttackType)0)]
			public BehaviorAttackType AttackType {
				get { return attackType; }
				set { SetProperty(ref attackType, ref value, AttackTypeProperty); }
			}

			/// <summary>Attack Material [SFX / SE]</summary>
			/// <remarks>
			/// Japanese short name: "攻撃材質[SFX/SE]", Google translated: "Attack Material [SFX / SE]".
			/// Japanese description: "攻撃時のSFX/SEを指定(属性、材質、サイズで1セット)", Google translated: "( One set of attributes , material , size ) specifies the SFX / SE of attack during".
			/// </remarks>
			[ParameterTableRowAttribute("atkMaterial", index: 56, minimum: 0, maximum: 255, step: 1, sortOrder: 1700, unknown2: 1)]
			[DisplayName("Attack Material [SFX / SE]")]
			[Description("( One set of attributes , material , size ) specifies the SFX / SE of attack during")]
			[DefaultValue((WeaponMaterialAttack)0)]
			public WeaponMaterialAttack AtkMaterial {
				get { return atkMaterial; }
				set { SetProperty(ref atkMaterial, ref value, AtkMaterialProperty); }
			}

			/// <summary>Attack size [SFX / SE]</summary>
			/// <remarks>
			/// Japanese short name: "攻撃サイズ[SFX/SE]", Google translated: "Attack size [SFX / SE]".
			/// Japanese description: "攻撃時のSFX/SEを指定（予備だが使用されているので、必ず空欄or小を設定すること）", Google translated: "Specify the SFX / SE of attack when it ( is preliminary , but because it is used , it is possible to set a blank or small always )".
			/// </remarks>
			[ParameterTableRowAttribute("atkSize", index: 57, minimum: 0, maximum: 255, step: 1, sortOrder: 1750, unknown2: 1)]
			[DisplayName("Attack size [SFX / SE]")]
			[Description("Specify the SFX / SE of attack when it ( is preliminary , but because it is used , it is possible to set a blank or small always )")]
			[DefaultValue((BehaviorAttackSize)0)]
			public BehaviorAttackSize AtkSize {
				get { return atkSize; }
				set { SetProperty(ref atkSize, ref value, AtkSizeProperty); }
			}

			/// <summary>Defense material [SE]</summary>
			/// <remarks>
			/// Japanese short name: "防御材質[SE]", Google translated: "Defense material [SE]".
			/// Japanese description: "ガード時のSEに使用", Google translated: "Use the SE of the guard at the time".
			/// </remarks>
			[ParameterTableRowAttribute("defMaterial", index: 58, minimum: 0, maximum: 255, step: 1, sortOrder: 2000, unknown2: 1)]
			[DisplayName("Defense material [SE]")]
			[Description("Use the SE of the guard at the time")]
			[DefaultValue((WeaponMaterialDefend)0)]
			public WeaponMaterialDefend DefMaterial {
				get { return defMaterial; }
				set { SetProperty(ref defMaterial, ref value, DefMaterialProperty); }
			}

			/// <summary>Defense material [SFX]</summary>
			/// <remarks>
			/// Japanese short name: "防御材質[SFX]", Google translated: "Defense material [SFX]".
			/// Japanese description: "ガード時のSFXに使用.", Google translated: "The use of SFX on guard at the time .".
			/// </remarks>
			[ParameterTableRowAttribute("defSfxMaterial", index: 59, minimum: 0, maximum: 255, step: 1, sortOrder: 1800, unknown2: 1)]
			[DisplayName("Defense material [SFX]")]
			[Description("The use of SFX on guard at the time .")]
			[DefaultValue((WeaponMaterialDefendSound)0)]
			public WeaponMaterialDefendSound DefSfxMaterial {
				get { return defSfxMaterial; }
				set { SetProperty(ref defSfxMaterial, ref value, DefSfxMaterialProperty); }
			}

			/// <summary>Per source</summary>
			/// <remarks>
			/// Japanese short name: "あたり発生源", Google translated: "Per source".
			/// Japanese description: "攻撃あたりのダミポリＩＤをどこから取ってくるか？を指定する", Google translated: "Where can I fetch the ID of Damipori per attack ? I specify the".
			/// </remarks>
			[ParameterTableRowAttribute("hitSourceType", index: 60, minimum: 0, maximum: 255, step: 1, sortOrder: 3700, unknown2: 1)]
			[DisplayName("Per source")]
			[Description("Where can I fetch the ID of Damipori per attack ? I specify the")]
			[DefaultValue((AttackHitSource)0)]
			public AttackHitSource HitSourceType {
				get { return hitSourceType; }
				set { SetProperty(ref hitSourceType, ref value, HitSourceTypeProperty); }
			}

			/// <summary>Throw</summary>
			/// <remarks>
			/// Japanese short name: "投げ", Google translated: "Throw".
			/// Japanese description: "投げ情報に用いるフラグ", Google translated: "Flag to be used for information Throw".
			/// </remarks>
			[ParameterTableRowAttribute("throwFlag", index: 61, minimum: 0, maximum: 255, step: 1, sortOrder: 3610, unknown2: 1)]
			[DisplayName("Throw")]
			[Description("Flag to be used for information Throw")]
			[DefaultValue((AttackThrowFlag)0)]
			public AttackThrowFlag ThrowFlag {
				get { return throwFlag; }
				set { SetProperty(ref throwFlag, ref value, ThrowFlagProperty); }
			}

			/// <summary>Unblockable flag</summary>
			/// <remarks>
			/// Japanese short name: "ガード不可フラグ", Google translated: "Unblockable flag".
			/// Japanese description: "1の場合、ガード側のガードを無視して、ダメージレベルを入れる", Google translated: "If 1 , ignore the guard the guard side , put a damage level".
			/// </remarks>
			[ParameterTableRowAttribute("disableGuard:1", index: 62, minimum: 0, maximum: 1, step: 1, sortOrder: 3300, unknown2: 1)]
			[DisplayName("Unblockable flag")]
			[Description("If 1 , ignore the guard the guard side , put a damage level")]
			[DefaultValue(false)]
			public Boolean DisableGuard {
				get { return GetBitProperty(0, 1, DisableGuardProperty) != 0; }
				set { SetBitProperty(0, 1, value ? 1 : 0, DisableGuardProperty); }
			}

			/// <summary>Does not decrease stamina</summary>
			/// <remarks>
			/// Japanese short name: "スタミナ減らない", Google translated: "Does not decrease stamina".
			/// Japanese description: "スタミナ攻撃力による「崩され判定」は行うが、実際にスタミナは減らさない", Google translated: "It performs is "determination to be deformed " by stamina attack power , but it does not reduce stamina actually".
			/// </remarks>
			[ParameterTableRowAttribute("disableStaminaAttack:1", index: 63, minimum: 0, maximum: 1, step: 1, sortOrder: 3400, unknown2: 1)]
			[DisplayName("Does not decrease stamina")]
			[Description("It performs is \"determination to be deformed \" by stamina attack power , but it does not reduce stamina actually")]
			[DefaultValue(false)]
			public Boolean DisableStaminaAttack {
				get { return GetBitProperty(1, 1, DisableStaminaAttackProperty) != 0; }
				set { SetBitProperty(1, 1, value ? 1 : 0, DisableStaminaAttackProperty); }
			}

			/// <summary>Special effects disabled hit</summary>
			/// <remarks>
			/// Japanese short name: "ヒット時特殊効果無効", Google translated: "Special effects disabled hit".
			/// Japanese description: "攻撃ヒットしたときの特殊効果を無効にします。SCEバグ対策", Google translated: "You can disable the special effects at the time of the attack hit . SCE bug fix".
			/// </remarks>
			[ParameterTableRowAttribute("disableHitSpEffect:1", index: 64, minimum: 0, maximum: 1, step: 1, sortOrder: 3600, unknown2: 1)]
			[DisplayName("Special effects disabled hit")]
			[Description("You can disable the special effects at the time of the attack hit . SCE bug fix")]
			[DefaultValue(false)]
			public Boolean DisableHitSpEffect {
				get { return GetBitProperty(2, 1, DisableHitSpEffectProperty) != 0; }
				set { SetBitProperty(2, 1, value ? 1 : 0, DisableHitSpEffectProperty); }
			}

			/// <summary>Do not swing away notice to AI</summary>
			/// <remarks>
			/// Japanese short name: "AIに空振り通知しない", Google translated: "Do not swing away notice to AI".
			/// Japanese description: "AIに空振り通知しない", Google translated: "Do not swing away notice to AI".
			/// </remarks>
			[ParameterTableRowAttribute("IgnoreNotifyMissSwingForAI:1", index: 65, minimum: 0, maximum: 1, step: 1, sortOrder: 6200, unknown2: 1)]
			[DisplayName("Do not swing away notice to AI")]
			[Description("Do not swing away notice to AI")]
			[DefaultValue(false)]
			public Boolean IgnoreNotifyMissSwingForAI {
				get { return GetBitProperty(3, 1, IgnoreNotifyMissSwingForAIProperty) != 0; }
				set { SetBitProperty(3, 1, value ? 1 : 0, IgnoreNotifyMissSwingForAIProperty); }
			}

			/// <summary>Or issue a number of times at the SFX HIT</summary>
			/// <remarks>
			/// Japanese short name: "ＨＩＴ時にＳＦＸを何度も出すか", Google translated: "Or issue a number of times at the SFX HIT".
			/// Japanese description: "敵専用：壁Hit時のSFXが連続で発生するか", Google translated: "Enemy only : SFX wall Hit when occurs or in a continuous".
			/// </remarks>
			[ParameterTableRowAttribute("repeatHitSfx:1", index: 66, minimum: 0, maximum: 1, step: 1, sortOrder: 2010, unknown2: 1)]
			[DisplayName("Or issue a number of times at the SFX HIT")]
			[Description("Enemy only : SFX wall Hit when occurs or in a continuous")]
			[DefaultValue(false)]
			public Boolean RepeatHitSfx {
				get { return GetBitProperty(4, 1, RepeatHitSfxProperty) != 0; }
				set { SetBitProperty(4, 1, value ? 1 : 0, RepeatHitSfxProperty); }
			}

			/// <summary>Or arrow attack</summary>
			/// <remarks>
			/// Japanese short name: "矢攻撃か", Google translated: "Or arrow attack".
			/// Japanese description: "部位ダメージ判定に使用する。", Google translated: "I use the site to damage judgment .".
			/// </remarks>
			[ParameterTableRowAttribute("isArrowAtk:1", index: 67, minimum: 0, maximum: 1, step: 1, sortOrder: 310, unknown2: 1)]
			[DisplayName("Or arrow attack")]
			[Description("I use the site to damage judgment .")]
			[DefaultValue(false)]
			public Boolean IsArrowAtk {
				get { return GetBitProperty(5, 1, IsArrowAtkProperty) != 0; }
				set { SetBitProperty(5, 1, value ? 1 : 0, IsArrowAtkProperty); }
			}

			/// <summary>Or spirit body attack</summary>
			/// <remarks>
			/// Japanese short name: "霊体攻撃か", Google translated: "Or spirit body attack".
			/// Japanese description: "霊体ダメージ判定に使用。", Google translated: "Used Rei-tai damage judgment .".
			/// </remarks>
			[ParameterTableRowAttribute("isGhostAtk:1", index: 68, minimum: 0, maximum: 1, step: 1, sortOrder: 320, unknown2: 1)]
			[DisplayName("Or spirit body attack")]
			[Description("Used Rei-tai damage judgment .")]
			[DefaultValue(false)]
			public Boolean IsGhostAtk {
				get { return GetBitProperty(6, 1, IsGhostAtkProperty) != 0; }
				set { SetBitProperty(6, 1, value ? 1 : 0, IsGhostAtkProperty); }
			}

			/// <summary>Invincible or invalid</summary>
			/// <remarks>
			/// Japanese short name: "無敵無効か", Google translated: "Invincible or invalid".
			/// Japanese description: "ステップ等の無敵効果を無視します、TAEの完全無敵は無視できません。", Google translated: "Ignore the invincible effect of steps and the like , full of invincible TAE can not be ignored .".
			/// </remarks>
			[ParameterTableRowAttribute("isDisableNoDamage:1", index: 69, minimum: 0, maximum: 1, step: 1, sortOrder: 330, unknown2: 1)]
			[DisplayName("Invincible or invalid")]
			[Description("Ignore the invincible effect of steps and the like , full of invincible TAE can not be ignored .")]
			[DefaultValue(false)]
			public Boolean IsDisableNoDamage {
				get { return GetBitProperty(7, 1, IsDisableNoDamageProperty) != 0; }
				set { SetBitProperty(7, 1, value ? 1 : 0, IsDisableNoDamageProperty); }
			}

			/// <summary>pad</summary>
			/// <remarks>
			/// Japanese short name: "pad", Google translated: "pad".
			/// Japanese description: "pad", Google translated: "pad".
			/// </remarks>
			[ParameterTableRowAttribute("pad[1]", index: 70, minimum: 0, maximum: 1, step: 1, sortOrder: 6701, unknown2: 1)]
			[DisplayName("pad")]
			[Description("pad")]
			[Browsable(false)]
			public Byte[] Pad {
				get { return pad; }
				set { SetProperty(ref pad, ref value, PadProperty); }
			}

			internal Attack(ParameterTable table, int index, AssetLoader loader, int next)
				: base(table, index, loader) {
				BinaryReader reader = loader.Reader;

				Hit0_Radius = reader.ReadSingle();
				Hit1_Radius = reader.ReadSingle();
				Hit2_Radius = reader.ReadSingle();
				Hit3_Radius = reader.ReadSingle();
				KnockbackDist = reader.ReadSingle();
				HitStopTime = reader.ReadSingle();
				SpEffectId0 = reader.ReadInt32();
				SpEffectId1 = reader.ReadInt32();
				SpEffectId2 = reader.ReadInt32();
				SpEffectId3 = reader.ReadInt32();
				SpEffectId4 = reader.ReadInt32();
				Hit0_DmyPoly1 = reader.ReadInt16();
				Hit1_DmyPoly1 = reader.ReadInt16();
				Hit2_DmyPoly1 = reader.ReadInt16();
				Hit3_DmyPoly1 = reader.ReadInt16();
				Hit0_DmyPoly2 = reader.ReadInt16();
				Hit1_DmyPoly2 = reader.ReadInt16();
				Hit2_DmyPoly2 = reader.ReadInt16();
				Hit3_DmyPoly2 = reader.ReadInt16();
				BlowingCorrection = reader.ReadUInt16();
				AtkPhysCorrection = reader.ReadUInt16();
				AtkMagCorrection = reader.ReadUInt16();
				AtkFireCorrection = reader.ReadUInt16();
				AtkThunCorrection = reader.ReadUInt16();
				AtkStamCorrection = reader.ReadUInt16();
				GuardAtkRateCorrection = reader.ReadUInt16();
				GuardBreakCorrection = reader.ReadUInt16();
				AtkThrowEscapeCorrection = reader.ReadUInt16();
				AtkSuperArmorCorrection = reader.ReadUInt16();
				AtkPhys = reader.ReadUInt16();
				AtkMag = reader.ReadUInt16();
				AtkFire = reader.ReadUInt16();
				AtkThun = reader.ReadUInt16();
				AtkStam = reader.ReadUInt16();
				GuardAtkRate = reader.ReadUInt16();
				GuardBreakRate = reader.ReadUInt16();
				AtkSuperArmor = reader.ReadUInt16();
				AtkThrowEscape = reader.ReadUInt16();
				AtkObj = reader.ReadUInt16();
				GuardStaminaCutRate = reader.ReadInt16();
				GuardRate = reader.ReadInt16();
				ThrowTypeId = reader.ReadUInt16();
				Hit0_hitType = (AttackHitType)reader.ReadByte();
				Hit1_hitType = (AttackHitType)reader.ReadByte();
				Hit2_hitType = (AttackHitType)reader.ReadByte();
				Hit3_hitType = (AttackHitType)reader.ReadByte();
				Hti0_Priority = reader.ReadByte();
				Hti1_Priority = reader.ReadByte();
				Hti2_Priority = reader.ReadByte();
				Hti3_Priority = reader.ReadByte();
				DamageLevel = reader.ReadByte();
				MapHitType = (AttackMapHit)reader.ReadByte();
				GuardCutCancelRate = reader.ReadSByte();
				AtkAttribute = (AttackAttackAttributes)reader.ReadByte();
				SpecialAttributes = (AttackParameterSpecialAttributes)reader.ReadByte();
				AttackType = (BehaviorAttackType)reader.ReadByte();
				AtkMaterial = (WeaponMaterialAttack)reader.ReadByte();
				AtkSize = (BehaviorAttackSize)reader.ReadByte();
				DefMaterial = (WeaponMaterialDefend)reader.ReadByte();
				DefSfxMaterial = (WeaponMaterialDefendSound)reader.ReadByte();
				HitSourceType = (AttackHitSource)reader.ReadByte();
				ThrowFlag = (AttackThrowFlag)reader.ReadByte();
				BitFields = reader.ReadBytes(1);
				Pad = reader.ReadBytes(1);
			}

			internal Attack(ParameterTable table, int index)
				: base(table, index) {
				BitFields = new byte[1];
				Hit0_Radius = (Single)0;
				Hit1_Radius = (Single)0;
				Hit2_Radius = (Single)0;
				Hit3_Radius = (Single)0;
				KnockbackDist = (Single)0;
				HitStopTime = (Single)0;
				SpEffectId0 = (Int32)(-1);
				SpEffectId1 = (Int32)(-1);
				SpEffectId2 = (Int32)(-1);
				SpEffectId3 = (Int32)(-1);
				SpEffectId4 = (Int32)(-1);
				Hit0_DmyPoly1 = (Int16)0;
				Hit1_DmyPoly1 = (Int16)0;
				Hit2_DmyPoly1 = (Int16)0;
				Hit3_DmyPoly1 = (Int16)0;
				Hit0_DmyPoly2 = (Int16)0;
				Hit1_DmyPoly2 = (Int16)0;
				Hit2_DmyPoly2 = (Int16)0;
				Hit3_DmyPoly2 = (Int16)0;
				BlowingCorrection = (UInt16)0;
				AtkPhysCorrection = (UInt16)0;
				AtkMagCorrection = (UInt16)0;
				AtkFireCorrection = (UInt16)0;
				AtkThunCorrection = (UInt16)0;
				AtkStamCorrection = (UInt16)0;
				GuardAtkRateCorrection = (UInt16)0;
				GuardBreakCorrection = (UInt16)0;
				AtkThrowEscapeCorrection = (UInt16)0;
				AtkSuperArmorCorrection = (UInt16)0;
				AtkPhys = (UInt16)0;
				AtkMag = (UInt16)0;
				AtkFire = (UInt16)0;
				AtkThun = (UInt16)0;
				AtkStam = (UInt16)0;
				GuardAtkRate = (UInt16)0;
				GuardBreakRate = (UInt16)0;
				AtkSuperArmor = (UInt16)0;
				AtkThrowEscape = (UInt16)0;
				AtkObj = (UInt16)0;
				GuardStaminaCutRate = (Int16)0;
				GuardRate = (Int16)0;
				ThrowTypeId = (UInt16)0;
				Hit0_hitType = (AttackHitType)0;
				Hit1_hitType = (AttackHitType)0;
				Hit2_hitType = (AttackHitType)0;
				Hit3_hitType = (AttackHitType)0;
				Hti0_Priority = (Byte)0;
				Hti1_Priority = (Byte)0;
				Hti2_Priority = (Byte)0;
				Hti3_Priority = (Byte)0;
				DamageLevel = (Byte)0;
				MapHitType = (AttackMapHit)0;
				GuardCutCancelRate = (SByte)0;
				AtkAttribute = (AttackAttackAttributes)0;
				SpecialAttributes = (AttackParameterSpecialAttributes)0;
				AttackType = (BehaviorAttackType)0;
				AtkMaterial = (WeaponMaterialAttack)0;
				AtkSize = (BehaviorAttackSize)0;
				DefMaterial = (WeaponMaterialDefend)0;
				DefSfxMaterial = (WeaponMaterialDefendSound)0;
				HitSourceType = (AttackHitSource)0;
				ThrowFlag = (AttackThrowFlag)0;
				DisableGuard = false;
				DisableStaminaAttack = false;
				DisableHitSpEffect = false;
				IgnoreNotifyMissSwingForAI = false;
				RepeatHitSfx = false;
				IsArrowAtk = false;
				IsGhostAtk = false;
				IsDisableNoDamage = false;
				Pad = new Byte[1];
			}

			/// <summary>Write the row to the writer.</summary>
			/// <param name="writer"></param>
			public override void Write(BinaryWriter writer) {
				writer.Write(Hit0_Radius);
				writer.Write(Hit1_Radius);
				writer.Write(Hit2_Radius);
				writer.Write(Hit3_Radius);
				writer.Write(KnockbackDist);
				writer.Write(HitStopTime);
				writer.Write(SpEffectId0);
				writer.Write(SpEffectId1);
				writer.Write(SpEffectId2);
				writer.Write(SpEffectId3);
				writer.Write(SpEffectId4);
				writer.Write(Hit0_DmyPoly1);
				writer.Write(Hit1_DmyPoly1);
				writer.Write(Hit2_DmyPoly1);
				writer.Write(Hit3_DmyPoly1);
				writer.Write(Hit0_DmyPoly2);
				writer.Write(Hit1_DmyPoly2);
				writer.Write(Hit2_DmyPoly2);
				writer.Write(Hit3_DmyPoly2);
				writer.Write(BlowingCorrection);
				writer.Write(AtkPhysCorrection);
				writer.Write(AtkMagCorrection);
				writer.Write(AtkFireCorrection);
				writer.Write(AtkThunCorrection);
				writer.Write(AtkStamCorrection);
				writer.Write(GuardAtkRateCorrection);
				writer.Write(GuardBreakCorrection);
				writer.Write(AtkThrowEscapeCorrection);
				writer.Write(AtkSuperArmorCorrection);
				writer.Write(AtkPhys);
				writer.Write(AtkMag);
				writer.Write(AtkFire);
				writer.Write(AtkThun);
				writer.Write(AtkStam);
				writer.Write(GuardAtkRate);
				writer.Write(GuardBreakRate);
				writer.Write(AtkSuperArmor);
				writer.Write(AtkThrowEscape);
				writer.Write(AtkObj);
				writer.Write(GuardStaminaCutRate);
				writer.Write(GuardRate);
				writer.Write(ThrowTypeId);
				writer.Write((Byte)Hit0_hitType);
				writer.Write((Byte)Hit1_hitType);
				writer.Write((Byte)Hit2_hitType);
				writer.Write((Byte)Hit3_hitType);
				writer.Write(Hti0_Priority);
				writer.Write(Hti1_Priority);
				writer.Write(Hti2_Priority);
				writer.Write(Hti3_Priority);
				writer.Write(DamageLevel);
				writer.Write((Byte)MapHitType);
				writer.Write(GuardCutCancelRate);
				writer.Write((Byte)AtkAttribute);
				writer.Write((Byte)SpecialAttributes);
				writer.Write((Byte)AttackType);
				writer.Write((Byte)AtkMaterial);
				writer.Write((Byte)AtkSize);
				writer.Write((Byte)DefMaterial);
				writer.Write((Byte)DefSfxMaterial);
				writer.Write((Byte)HitSourceType);
				writer.Write((Byte)ThrowFlag);
				writer.Write(BitFields);
				writer.Write(Pad);
			}
		}
	}
}
