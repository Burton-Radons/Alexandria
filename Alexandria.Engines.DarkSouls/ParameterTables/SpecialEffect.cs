/*
For ParameterTable.cs under ParameterTableRow.ReadRow:
For ParameterDefinition.cs under ParameterDefinitionRow.GetDotNetType():
For Enumerations.cs:
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
		/// <summary></summary>
		/// <remarks>
		/// Defined as "SP_EFFECT_PARAM_ST" in Dark Souls in the file "SpEffect.paramdef" (id 1Fh).
		/// </remarks>
		public class SpecialEffect : ParameterTableRow {
			public const string TableName = "SP_EFFECT_PARAM_ST";

			Int32 iconId, physicsAttackPower, magicAttackPower, fireAttackPower, thunderAttackPower, physicsDiffence, magicDiffence, fireDiffence, thunderDiffence, behaviorId, changeHpPoint, changeMpPoint, mpRecoverChangeSpeed, changeStaminaPoint, staminaRecoverChangeSpeed, insideDurability, maxDurability, poizonAttackPower, registIllness, registBlood, registCurse, soul, animIdOffset, sightSearchEnemyCut, hearingSearchEnemyCut, replaceSpEffectId, cycleOccurrenceSpEffectId, atkOccurrenceSpEffectId;
			Single conditionHp, effectEndurance, motionInterval, maxHpRate, maxMpRate, maxStaminaRate, slashDamageCutRate, blowDamageCutRate, thrustDamageCutRate, neutralDamageCutRate, magicDamageCutRate, fireDamageCutRate, thunderDamageCutRate, physicsAttackRate, magicAttackRate, fireAttackRate, thunderAttackRate, physicsAttackPowerRate, magicAttackPowerRate, fireAttackPowerRate, thunderAttackPowerRate, physicsDiffenceRate, magicDiffenceRate, fireDiffenceRate, thunderDiffenceRate, noGuardDamageRate, vitalSpotChangeRate, normalSpotChangeRate, maxHpChangeRate, changeHpRate, changeMpRate, changeStaminaRate, magicEffectTimeChange, staminaAttackRate, fallDamageRate, soulRate, equipWeightChangeRate, allItemWeightChangeRate, haveSoulRate, targetPriority, grabityRate, registPoizonChangeRate, registIllnessChangeRate, registBloodChangeRate, registCurseChangeRate, soulStealRate, lifeReductionRate, hpRecoverRate, guardDefFlickPowerRate, guardStaminaCutRate;
			Int16 rayCastPassedTime, changeSuperArmorPoint, bowDistRate;
			SpecialEffectSpCategory spCategory;
			Byte categoryPriority, changeMagicSlot, changeMiracleSlot, defFlickPower, flickDamageCutRate, bloodDamageRate, addBehaviorJudgeId_add;
			SpecialEffectSaveCategory saveCategory;
			SByte heroPointDamage, addBehaviorJudgeId_condition;
			ATKPARAM_REP_DMGTYPE dmgLv_None, dmgLv_S, dmgLv_M, dmgLv_L, dmgLv_BlowM, dmgLv_Push, dmgLv_Strike, dmgLv_BlowS, dmgLv_Min, dmgLv_Uppercut, dmgLv_BlowLL, dmgLv_Breath;
			AttackAttackAttributes atkAttribute;
			AttackParameterSpecialAttributes spAttribute;
			SpecialEffectType stateInfo, lifeReductionType;
			SpecialEffectWeaponChange wepParamChange;
			SpecialEffectMoveType moveType;
			SpecialEffectThrowCondition throwCondition;
			Byte[] pad1;

			public static readonly PropertyInfo
				IconIdProperty = GetProperty<SpecialEffect>("IconId"),
				ConditionHpProperty = GetProperty<SpecialEffect>("ConditionHp"),
				EffectEnduranceProperty = GetProperty<SpecialEffect>("EffectEndurance"),
				MotionIntervalProperty = GetProperty<SpecialEffect>("MotionInterval"),
				MaxHpRateProperty = GetProperty<SpecialEffect>("MaxHpRate"),
				MaxMpRateProperty = GetProperty<SpecialEffect>("MaxMpRate"),
				MaxStaminaRateProperty = GetProperty<SpecialEffect>("MaxStaminaRate"),
				SlashDamageCutRateProperty = GetProperty<SpecialEffect>("SlashDamageCutRate"),
				BlowDamageCutRateProperty = GetProperty<SpecialEffect>("BlowDamageCutRate"),
				ThrustDamageCutRateProperty = GetProperty<SpecialEffect>("ThrustDamageCutRate"),
				NeutralDamageCutRateProperty = GetProperty<SpecialEffect>("NeutralDamageCutRate"),
				MagicDamageCutRateProperty = GetProperty<SpecialEffect>("MagicDamageCutRate"),
				FireDamageCutRateProperty = GetProperty<SpecialEffect>("FireDamageCutRate"),
				ThunderDamageCutRateProperty = GetProperty<SpecialEffect>("ThunderDamageCutRate"),
				PhysicsAttackRateProperty = GetProperty<SpecialEffect>("PhysicsAttackRate"),
				MagicAttackRateProperty = GetProperty<SpecialEffect>("MagicAttackRate"),
				FireAttackRateProperty = GetProperty<SpecialEffect>("FireAttackRate"),
				ThunderAttackRateProperty = GetProperty<SpecialEffect>("ThunderAttackRate"),
				PhysicsAttackPowerRateProperty = GetProperty<SpecialEffect>("PhysicsAttackPowerRate"),
				MagicAttackPowerRateProperty = GetProperty<SpecialEffect>("MagicAttackPowerRate"),
				FireAttackPowerRateProperty = GetProperty<SpecialEffect>("FireAttackPowerRate"),
				ThunderAttackPowerRateProperty = GetProperty<SpecialEffect>("ThunderAttackPowerRate"),
				PhysicsAttackPowerProperty = GetProperty<SpecialEffect>("PhysicsAttackPower"),
				MagicAttackPowerProperty = GetProperty<SpecialEffect>("MagicAttackPower"),
				FireAttackPowerProperty = GetProperty<SpecialEffect>("FireAttackPower"),
				ThunderAttackPowerProperty = GetProperty<SpecialEffect>("ThunderAttackPower"),
				PhysicsDiffenceRateProperty = GetProperty<SpecialEffect>("PhysicsDiffenceRate"),
				MagicDiffenceRateProperty = GetProperty<SpecialEffect>("MagicDiffenceRate"),
				FireDiffenceRateProperty = GetProperty<SpecialEffect>("FireDiffenceRate"),
				ThunderDiffenceRateProperty = GetProperty<SpecialEffect>("ThunderDiffenceRate"),
				PhysicsDiffenceProperty = GetProperty<SpecialEffect>("PhysicsDiffence"),
				MagicDiffenceProperty = GetProperty<SpecialEffect>("MagicDiffence"),
				FireDiffenceProperty = GetProperty<SpecialEffect>("FireDiffence"),
				ThunderDiffenceProperty = GetProperty<SpecialEffect>("ThunderDiffence"),
				NoGuardDamageRateProperty = GetProperty<SpecialEffect>("NoGuardDamageRate"),
				VitalSpotChangeRateProperty = GetProperty<SpecialEffect>("VitalSpotChangeRate"),
				NormalSpotChangeRateProperty = GetProperty<SpecialEffect>("NormalSpotChangeRate"),
				MaxHpChangeRateProperty = GetProperty<SpecialEffect>("MaxHpChangeRate"),
				BehaviorIdProperty = GetProperty<SpecialEffect>("BehaviorId"),
				HasTargetProperty = GetProperty<SpecialEffect>("HasTarget"),
				ChangeHpRateProperty = GetProperty<SpecialEffect>("ChangeHpRate"),
				ChangeHpPointProperty = GetProperty<SpecialEffect>("ChangeHpPoint"),
				ChangeMpRateProperty = GetProperty<SpecialEffect>("ChangeMpRate"),
				ChangeMpPointProperty = GetProperty<SpecialEffect>("ChangeMpPoint"),
				MpRecoverChangeSpeedProperty = GetProperty<SpecialEffect>("MpRecoverChangeSpeed"),
				ChangeStaminaRateProperty = GetProperty<SpecialEffect>("ChangeStaminaRate"),
				ChangeStaminaPointProperty = GetProperty<SpecialEffect>("ChangeStaminaPoint"),
				StaminaRecoverChangeSpeedProperty = GetProperty<SpecialEffect>("StaminaRecoverChangeSpeed"),
				MagicEffectTimeChangeProperty = GetProperty<SpecialEffect>("MagicEffectTimeChange"),
				InsideDurabilityProperty = GetProperty<SpecialEffect>("InsideDurability"),
				MaxDurabilityProperty = GetProperty<SpecialEffect>("MaxDurability"),
				StaminaAttackRateProperty = GetProperty<SpecialEffect>("StaminaAttackRate"),
				PoizonAttackPowerProperty = GetProperty<SpecialEffect>("PoizonAttackPower"),
				RegistIllnessProperty = GetProperty<SpecialEffect>("RegistIllness"),
				RegistBloodProperty = GetProperty<SpecialEffect>("RegistBlood"),
				RegistCurseProperty = GetProperty<SpecialEffect>("RegistCurse"),
				FallDamageRateProperty = GetProperty<SpecialEffect>("FallDamageRate"),
				SoulRateProperty = GetProperty<SpecialEffect>("SoulRate"),
				EquipWeightChangeRateProperty = GetProperty<SpecialEffect>("EquipWeightChangeRate"),
				AllItemWeightChangeRateProperty = GetProperty<SpecialEffect>("AllItemWeightChangeRate"),
				SoulProperty = GetProperty<SpecialEffect>("Soul"),
				AnimIdOffsetProperty = GetProperty<SpecialEffect>("AnimIdOffset"),
				HaveSoulRateProperty = GetProperty<SpecialEffect>("HaveSoulRate"),
				TargetPriorityProperty = GetProperty<SpecialEffect>("TargetPriority"),
				SightSearchEnemyCutProperty = GetProperty<SpecialEffect>("SightSearchEnemyCut"),
				HearingSearchEnemyCutProperty = GetProperty<SpecialEffect>("HearingSearchEnemyCut"),
				GrabityRateProperty = GetProperty<SpecialEffect>("GrabityRate"),
				RegistPoizonChangeRateProperty = GetProperty<SpecialEffect>("RegistPoizonChangeRate"),
				RegistIllnessChangeRateProperty = GetProperty<SpecialEffect>("RegistIllnessChangeRate"),
				RegistBloodChangeRateProperty = GetProperty<SpecialEffect>("RegistBloodChangeRate"),
				RegistCurseChangeRateProperty = GetProperty<SpecialEffect>("RegistCurseChangeRate"),
				SoulStealRateProperty = GetProperty<SpecialEffect>("SoulStealRate"),
				LifeReductionRateProperty = GetProperty<SpecialEffect>("LifeReductionRate"),
				HpRecoverRateProperty = GetProperty<SpecialEffect>("HpRecoverRate"),
				ReplaceSpEffectIdProperty = GetProperty<SpecialEffect>("ReplaceSpEffectId"),
				CycleOccurrenceSpEffectIdProperty = GetProperty<SpecialEffect>("CycleOccurrenceSpEffectId"),
				AtkOccurrenceSpEffectIdProperty = GetProperty<SpecialEffect>("AtkOccurrenceSpEffectId"),
				GuardDefFlickPowerRateProperty = GetProperty<SpecialEffect>("GuardDefFlickPowerRate"),
				GuardStaminaCutRateProperty = GetProperty<SpecialEffect>("GuardStaminaCutRate"),
				RayCastPassedTimeProperty = GetProperty<SpecialEffect>("RayCastPassedTime"),
				ChangeSuperArmorPointProperty = GetProperty<SpecialEffect>("ChangeSuperArmorPoint"),
				BowDistRateProperty = GetProperty<SpecialEffect>("BowDistRate"),
				SpCategoryProperty = GetProperty<SpecialEffect>("SpCategory"),
				CategoryPriorityProperty = GetProperty<SpecialEffect>("CategoryPriority"),
				SaveCategoryProperty = GetProperty<SpecialEffect>("SaveCategory"),
				ChangeMagicSlotProperty = GetProperty<SpecialEffect>("ChangeMagicSlot"),
				ChangeMiracleSlotProperty = GetProperty<SpecialEffect>("ChangeMiracleSlot"),
				HeroPointDamageProperty = GetProperty<SpecialEffect>("HeroPointDamage"),
				DefFlickPowerProperty = GetProperty<SpecialEffect>("DefFlickPower"),
				FlickDamageCutRateProperty = GetProperty<SpecialEffect>("FlickDamageCutRate"),
				BloodDamageRateProperty = GetProperty<SpecialEffect>("BloodDamageRate"),
				DmgLv_NoneProperty = GetProperty<SpecialEffect>("DmgLv_None"),
				DmgLv_SProperty = GetProperty<SpecialEffect>("DmgLv_S"),
				DmgLv_MProperty = GetProperty<SpecialEffect>("DmgLv_M"),
				DmgLv_LProperty = GetProperty<SpecialEffect>("DmgLv_L"),
				DmgLv_BlowMProperty = GetProperty<SpecialEffect>("DmgLv_BlowM"),
				DmgLv_PushProperty = GetProperty<SpecialEffect>("DmgLv_Push"),
				DmgLv_StrikeProperty = GetProperty<SpecialEffect>("DmgLv_Strike"),
				DmgLv_BlowSProperty = GetProperty<SpecialEffect>("DmgLv_BlowS"),
				DmgLv_MinProperty = GetProperty<SpecialEffect>("DmgLv_Min"),
				DmgLv_UppercutProperty = GetProperty<SpecialEffect>("DmgLv_Uppercut"),
				DmgLv_BlowLLProperty = GetProperty<SpecialEffect>("DmgLv_BlowLL"),
				DmgLv_BreathProperty = GetProperty<SpecialEffect>("DmgLv_Breath"),
				AtkAttributeProperty = GetProperty<SpecialEffect>("AtkAttribute"),
				SpAttributeProperty = GetProperty<SpecialEffect>("SpAttribute"),
				StateInfoProperty = GetProperty<SpecialEffect>("StateInfo"),
				WepParamChangeProperty = GetProperty<SpecialEffect>("WepParamChange"),
				MoveTypeProperty = GetProperty<SpecialEffect>("MoveType"),
				LifeReductionTypeProperty = GetProperty<SpecialEffect>("LifeReductionType"),
				ThrowConditionProperty = GetProperty<SpecialEffect>("ThrowCondition"),
				AddBehaviorJudgeId_conditionProperty = GetProperty<SpecialEffect>("AddBehaviorJudgeId_condition"),
				AddBehaviorJudgeId_addProperty = GetProperty<SpecialEffect>("AddBehaviorJudgeId_add"),
				EffectTargetSelfProperty = GetProperty<SpecialEffect>("EffectTargetSelf"),
				EffectTargetFriendProperty = GetProperty<SpecialEffect>("EffectTargetFriend"),
				EffectTargetEnemyProperty = GetProperty<SpecialEffect>("EffectTargetEnemy"),
				EffectTargetPlayerProperty = GetProperty<SpecialEffect>("EffectTargetPlayer"),
				EffectTargetAIProperty = GetProperty<SpecialEffect>("EffectTargetAI"),
				EffectTargetLiveProperty = GetProperty<SpecialEffect>("EffectTargetLive"),
				EffectTargetGhostProperty = GetProperty<SpecialEffect>("EffectTargetGhost"),
				EffectTargetWhiteGhostProperty = GetProperty<SpecialEffect>("EffectTargetWhiteGhost"),
				EffectTargetBlackGhostProperty = GetProperty<SpecialEffect>("EffectTargetBlackGhost"),
				EffectTargetAttackerProperty = GetProperty<SpecialEffect>("EffectTargetAttacker"),
				DispIconNonactiveProperty = GetProperty<SpecialEffect>("DispIconNonactive"),
				UseSpEffectEffectProperty = GetProperty<SpecialEffect>("UseSpEffectEffect"),
				BAdjustMagicAblityProperty = GetProperty<SpecialEffect>("BAdjustMagicAblity"),
				BAdjustFaithAblityProperty = GetProperty<SpecialEffect>("BAdjustFaithAblity"),
				BGameClearBonusProperty = GetProperty<SpecialEffect>("BGameClearBonus"),
				MagParamChangeProperty = GetProperty<SpecialEffect>("MagParamChange"),
				MiracleParamChangeProperty = GetProperty<SpecialEffect>("MiracleParamChange"),
				ClearSoulProperty = GetProperty<SpecialEffect>("ClearSoul"),
				RequestSOSProperty = GetProperty<SpecialEffect>("RequestSOS"),
				RequestBlackSOSProperty = GetProperty<SpecialEffect>("RequestBlackSOS"),
				RequestForceJoinBlackSOSProperty = GetProperty<SpecialEffect>("RequestForceJoinBlackSOS"),
				RequestKickSessionProperty = GetProperty<SpecialEffect>("RequestKickSession"),
				RequestLeaveSessionProperty = GetProperty<SpecialEffect>("RequestLeaveSession"),
				RequestNpcInvedaProperty = GetProperty<SpecialEffect>("RequestNpcInveda"),
				NoDeadProperty = GetProperty<SpecialEffect>("NoDead"),
				BCurrHPIndependeMaxHPProperty = GetProperty<SpecialEffect>("BCurrHPIndependeMaxHP"),
				CorrosionIgnoreProperty = GetProperty<SpecialEffect>("CorrosionIgnore"),
				SightSearchCutIgnoreProperty = GetProperty<SpecialEffect>("SightSearchCutIgnore"),
				HearingSearchCutIgnoreProperty = GetProperty<SpecialEffect>("HearingSearchCutIgnore"),
				AntiMagicIgnoreProperty = GetProperty<SpecialEffect>("AntiMagicIgnore"),
				FakeTargetIgnoreProperty = GetProperty<SpecialEffect>("FakeTargetIgnore"),
				FakeTargetIgnoreUndeadProperty = GetProperty<SpecialEffect>("FakeTargetIgnoreUndead"),
				FakeTargetIgnoreAnimalProperty = GetProperty<SpecialEffect>("FakeTargetIgnoreAnimal"),
				GrabityIgnoreProperty = GetProperty<SpecialEffect>("GrabityIgnore"),
				DisablePoisonProperty = GetProperty<SpecialEffect>("DisablePoison"),
				DisableDiseaseProperty = GetProperty<SpecialEffect>("DisableDisease"),
				DisableBloodProperty = GetProperty<SpecialEffect>("DisableBlood"),
				DisableCurseProperty = GetProperty<SpecialEffect>("DisableCurse"),
				EnableCharmProperty = GetProperty<SpecialEffect>("EnableCharm"),
				EnableLifeTimeProperty = GetProperty<SpecialEffect>("EnableLifeTime"),
				IsFireDamageCancelProperty = GetProperty<SpecialEffect>("IsFireDamageCancel"),
				IsExtendSpEffectLifeProperty = GetProperty<SpecialEffect>("IsExtendSpEffectLife"),
				RequestLeaveColiseumSessionProperty = GetProperty<SpecialEffect>("RequestLeaveColiseumSession"),
				Pad_2Property = GetProperty<SpecialEffect>("Pad_2"),
				VowType0Property = GetProperty<SpecialEffect>("VowType0"),
				VowType1Property = GetProperty<SpecialEffect>("VowType1"),
				VowType2Property = GetProperty<SpecialEffect>("VowType2"),
				VowType3Property = GetProperty<SpecialEffect>("VowType3"),
				VowType4Property = GetProperty<SpecialEffect>("VowType4"),
				VowType5Property = GetProperty<SpecialEffect>("VowType5"),
				VowType6Property = GetProperty<SpecialEffect>("VowType6"),
				VowType7Property = GetProperty<SpecialEffect>("VowType7"),
				VowType8Property = GetProperty<SpecialEffect>("VowType8"),
				VowType9Property = GetProperty<SpecialEffect>("VowType9"),
				VowType10Property = GetProperty<SpecialEffect>("VowType10"),
				VowType11Property = GetProperty<SpecialEffect>("VowType11"),
				VowType12Property = GetProperty<SpecialEffect>("VowType12"),
				VowType13Property = GetProperty<SpecialEffect>("VowType13"),
				VowType14Property = GetProperty<SpecialEffect>("VowType14"),
				VowType15Property = GetProperty<SpecialEffect>("VowType15"),
				Pad1Property = GetProperty<SpecialEffect>("Pad1");

			/// <summary>Icon ID</summary>
			/// <remarks>
			/// Japanese short name: "アイコンID", Google translated: "Icon ID".
			/// Japanese description: "アイコンID(-1の時は、アイコン必要なし)", Google translated: "( If -1 , no icon necessary ) icon ID".
			/// </remarks>
			[ParameterTableRowAttribute("iconId", index: 0, minimum: -1, maximum: 999999, step: 1, order: 100, unknown2: 1)]
			[DisplayName("Icon ID")]
			[Description("( If -1 , no icon necessary ) icon ID")]
			[DefaultValue((Int32)(-1))]
			public Int32 IconId {
				get { return iconId; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for " + IconIdProperty.Name + ".");
					SetProperty(ref iconId, ref value, IconIdProperty);
				}
			}

			/// <summary>Triggering conditions remaining HP ratio [ %]</summary>
			/// <remarks>
			/// Japanese short name: "発動条件　残りHP比率[%]", Google translated: "Triggering conditions remaining HP ratio [ %]".
			/// Japanese description: "残りHPが、maxHPの何%になったら発動するかを設定", Google translated: "HP is remaining , set whether to activate the percentage of maxHP Once".
			/// </remarks>
			[ParameterTableRowAttribute("conditionHp", index: 1, minimum: -1, maximum: 100, step: 0.1, order: 1800, unknown2: 1)]
			[DisplayName("Triggering conditions remaining HP ratio [ %]")]
			[Description("HP is remaining , set whether to activate the percentage of maxHP Once")]
			[DefaultValue((Single)(-1))]
			public Single ConditionHp {
				get { return conditionHp; }
				set {
					if ((double)value < -1 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 100 for " + ConditionHpProperty.Name + ".");
					SetProperty(ref conditionHp, ref value, ConditionHpProperty);
				}
			}

			/// <summary>Time duration of effect [s]</summary>
			/// <remarks>
			/// Japanese short name: "効果持続時間　時間[s]", Google translated: "Time duration of effect [s]".
			/// Japanese description: "変化持続時間　/-1で永続 /0で瞬間1回限り", Google translated: "One-time moment in Persistent / 0 by the change duration / -1".
			/// </remarks>
			[ParameterTableRowAttribute("effectEndurance", index: 2, minimum: -1, maximum: 999, step: 0.1, order: 1900, unknown2: 1)]
			[DisplayName("Time duration of effect [s]")]
			[Description("One-time moment in Persistent / 0 by the change duration / -1")]
			[DefaultValue((Single)0)]
			public Single EffectEndurance {
				get { return effectEndurance; }
				set {
					if ((double)value < -1 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999 for " + EffectEnduranceProperty.Name + ".");
					SetProperty(ref effectEndurance, ref value, EffectEnduranceProperty);
				}
			}

			/// <summary>Trigger interval [s]</summary>
			/// <remarks>
			/// Japanese short name: "発動間隔[s]", Google translated: "Trigger interval [s]".
			/// Japanese description: "何秒間隔で発生するのかを設定", Google translated: "Set in what second intervals whether the occurrence of".
			/// </remarks>
			[ParameterTableRowAttribute("motionInterval", index: 3, minimum: -1, maximum: 999, step: 0.1, order: 2000, unknown2: 1)]
			[DisplayName("Trigger interval [s]")]
			[Description("Set in what second intervals whether the occurrence of")]
			[DefaultValue((Single)0)]
			public Single MotionInterval {
				get { return motionInterval; }
				set {
					if ((double)value < -1 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999 for " + MotionIntervalProperty.Name + ".");
					SetProperty(ref motionInterval, ref value, MotionIntervalProperty);
				}
			}

			/// <summary>Maximum HP magnification [ %]</summary>
			/// <remarks>
			/// Japanese short name: "最大HP倍率[%]", Google translated: "Maximum HP magnification [ %]".
			/// Japanese description: "最大HPに補正をかける", Google translated: "I make a correction to the maximum HP".
			/// </remarks>
			[ParameterTableRowAttribute("maxHpRate", index: 4, minimum: 0, maximum: 99, step: 0.001, order: 2100, unknown2: 1)]
			[DisplayName("Maximum HP magnification [ %]")]
			[Description("I make a correction to the maximum HP")]
			[DefaultValue((Single)1)]
			public Single MaxHpRate {
				get { return maxHpRate; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for " + MaxHpRateProperty.Name + ".");
					SetProperty(ref maxHpRate, ref value, MaxHpRateProperty);
				}
			}

			/// <summary>Maximum MP magnification [ %]</summary>
			/// <remarks>
			/// Japanese short name: "最大MP倍率[%]", Google translated: "Maximum MP magnification [ %]".
			/// Japanese description: "最大MPに補正をかける", Google translated: "I make a correction to the maximum MP".
			/// </remarks>
			[ParameterTableRowAttribute("maxMpRate", index: 5, minimum: 0, maximum: 99, step: 0.001, order: 2200, unknown2: 1)]
			[DisplayName("Maximum MP magnification [ %]")]
			[Description("I make a correction to the maximum MP")]
			[DefaultValue((Single)1)]
			public Single MaxMpRate {
				get { return maxMpRate; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for " + MaxMpRateProperty.Name + ".");
					SetProperty(ref maxMpRate, ref value, MaxMpRateProperty);
				}
			}

			/// <summary>Maximum magnification stamina [ %]</summary>
			/// <remarks>
			/// Japanese short name: "最大スタミナ倍率[%]", Google translated: "Maximum magnification stamina [ %]".
			/// Japanese description: "最大SPに補正をかける", Google translated: "I make a correction to the maximum SP".
			/// </remarks>
			[ParameterTableRowAttribute("maxStaminaRate", index: 6, minimum: 0, maximum: 99, step: 0.001, order: 2300, unknown2: 1)]
			[DisplayName("Maximum magnification stamina [ %]")]
			[Description("I make a correction to the maximum SP")]
			[DefaultValue((Single)1)]
			public Single MaxStaminaRate {
				get { return maxStaminaRate; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for " + MaxStaminaRateProperty.Name + ".");
					SetProperty(ref maxStaminaRate, ref value, MaxStaminaRateProperty);
				}
			}

			/// <summary>Defender : slashing Damage magnification</summary>
			/// <remarks>
			/// Japanese short name: "防御側：斬撃ダメージ倍率", Google translated: "Defender : slashing Damage magnification".
			/// Japanese description: "斬撃ダメージ倍率：算出したダメージに×○倍で補正をかける。１が通常。", Google translated: "To apply a correction in ○ × magnification to the damage was calculated : slashing damage magnification . 1 usually .".
			/// </remarks>
			[ParameterTableRowAttribute("slashDamageCutRate", index: 7, minimum: -99, maximum: 99, step: 0.001, order: 2910, unknown2: 1)]
			[DisplayName("Defender : slashing Damage magnification")]
			[Description("To apply a correction in ○ × magnification to the damage was calculated : slashing damage magnification . 1 usually .")]
			[DefaultValue((Single)1)]
			public Single SlashDamageCutRate {
				get { return slashDamageCutRate; }
				set {
					if ((double)value < -99 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -99 to 99 for " + SlashDamageCutRateProperty.Name + ".");
					SetProperty(ref slashDamageCutRate, ref value, SlashDamageCutRateProperty);
				}
			}

			/// <summary>Defender : blow damage magnification</summary>
			/// <remarks>
			/// Japanese short name: "防御側：打撃ダメージ倍率", Google translated: "Defender : blow damage magnification".
			/// Japanese description: "打撃ダメージ倍率：算出したダメージに×○倍で補正をかける。１が通常。", Google translated: "To apply a correction in ○ × magnification to the damage was calculated : blow damage magnification . 1 usually .".
			/// </remarks>
			[ParameterTableRowAttribute("blowDamageCutRate", index: 8, minimum: -99, maximum: 99, step: 0.001, order: 2920, unknown2: 1)]
			[DisplayName("Defender : blow damage magnification")]
			[Description("To apply a correction in ○ × magnification to the damage was calculated : blow damage magnification . 1 usually .")]
			[DefaultValue((Single)1)]
			public Single BlowDamageCutRate {
				get { return blowDamageCutRate; }
				set {
					if ((double)value < -99 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -99 to 99 for " + BlowDamageCutRateProperty.Name + ".");
					SetProperty(ref blowDamageCutRate, ref value, BlowDamageCutRateProperty);
				}
			}

			/// <summary>Defender : piercing damage magnification</summary>
			/// <remarks>
			/// Japanese short name: "防御側：刺突ダメージ倍率", Google translated: "Defender : piercing damage magnification".
			/// Japanese description: "刺突ダメージ倍率：算出したダメージに×○倍で補正をかける。１が通常。", Google translated: "To apply a correction in ○ × magnification to the damage was calculated : Piercing Damage magnification . 1 usually .".
			/// </remarks>
			[ParameterTableRowAttribute("thrustDamageCutRate", index: 9, minimum: -99, maximum: 99, step: 0.001, order: 2930, unknown2: 1)]
			[DisplayName("Defender : piercing damage magnification")]
			[Description("To apply a correction in ○ × magnification to the damage was calculated : Piercing Damage magnification . 1 usually .")]
			[DefaultValue((Single)1)]
			public Single ThrustDamageCutRate {
				get { return thrustDamageCutRate; }
				set {
					if ((double)value < -99 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -99 to 99 for " + ThrustDamageCutRateProperty.Name + ".");
					SetProperty(ref thrustDamageCutRate, ref value, ThrustDamageCutRateProperty);
				}
			}

			/// <summary>Defender : non-attribute damage magnification</summary>
			/// <remarks>
			/// Japanese short name: "防御側：無属性ダメージ倍率", Google translated: "Defender : non-attribute damage magnification".
			/// Japanese description: "無属性ダメージ倍率：算出したダメージに×○倍で補正をかける。１が通常。", Google translated: "To apply a correction in ○ × magnification to the damage was calculated : non-attribute damage magnification . 1 usually .".
			/// </remarks>
			[ParameterTableRowAttribute("neutralDamageCutRate", index: 10, minimum: -99, maximum: 99, step: 0.001, order: 2940, unknown2: 1)]
			[DisplayName("Defender : non-attribute damage magnification")]
			[Description("To apply a correction in ○ × magnification to the damage was calculated : non-attribute damage magnification . 1 usually .")]
			[DefaultValue((Single)1)]
			public Single NeutralDamageCutRate {
				get { return neutralDamageCutRate; }
				set {
					if ((double)value < -99 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -99 to 99 for " + NeutralDamageCutRateProperty.Name + ".");
					SetProperty(ref neutralDamageCutRate, ref value, NeutralDamageCutRateProperty);
				}
			}

			/// <summary>Defender : Magic Damage magnification</summary>
			/// <remarks>
			/// Japanese short name: "防御側：魔法ダメージ倍率", Google translated: "Defender : Magic Damage magnification".
			/// Japanese description: "魔法ダメージ倍率：算出したダメージに×○倍で補正をかける。１が通常。", Google translated: "To apply a correction in ○ × magnification to the damage was calculated : Magic Damage magnification . 1 usually .".
			/// </remarks>
			[ParameterTableRowAttribute("magicDamageCutRate", index: 11, minimum: -99, maximum: 99, step: 0.001, order: 3000, unknown2: 1)]
			[DisplayName("Defender : Magic Damage magnification")]
			[Description("To apply a correction in ○ × magnification to the damage was calculated : Magic Damage magnification . 1 usually .")]
			[DefaultValue((Single)1)]
			public Single MagicDamageCutRate {
				get { return magicDamageCutRate; }
				set {
					if ((double)value < -99 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -99 to 99 for " + MagicDamageCutRateProperty.Name + ".");
					SetProperty(ref magicDamageCutRate, ref value, MagicDamageCutRateProperty);
				}
			}

			/// <summary>Defender : Fire Damage magnification</summary>
			/// <remarks>
			/// Japanese short name: "防御側：炎ダメージ倍率", Google translated: "Defender : Fire Damage magnification".
			/// Japanese description: "炎ダメージ倍率：算出したダメージに×○倍で補正をかける。１が通常。", Google translated: "To apply a correction in ○ × magnification to the damage was calculated : fire damage magnification . 1 usually .".
			/// </remarks>
			[ParameterTableRowAttribute("fireDamageCutRate", index: 12, minimum: -99, maximum: 99, step: 0.001, order: 3100, unknown2: 1)]
			[DisplayName("Defender : Fire Damage magnification")]
			[Description("To apply a correction in ○ × magnification to the damage was calculated : fire damage magnification . 1 usually .")]
			[DefaultValue((Single)1)]
			public Single FireDamageCutRate {
				get { return fireDamageCutRate; }
				set {
					if ((double)value < -99 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -99 to 99 for " + FireDamageCutRateProperty.Name + ".");
					SetProperty(ref fireDamageCutRate, ref value, FireDamageCutRateProperty);
				}
			}

			/// <summary>Defender : lightning damage magnification</summary>
			/// <remarks>
			/// Japanese short name: "防御側：電撃ダメージ倍率", Google translated: "Defender : lightning damage magnification".
			/// Japanese description: "炎ダメージ倍率：算出したダメージに×○倍で補正をかける。１が通常。", Google translated: "To apply a correction in ○ × magnification to the damage was calculated : fire damage magnification . 1 usually .".
			/// </remarks>
			[ParameterTableRowAttribute("thunderDamageCutRate", index: 13, minimum: -99, maximum: 99, step: 0.001, order: 3110, unknown2: 1)]
			[DisplayName("Defender : lightning damage magnification")]
			[Description("To apply a correction in ○ × magnification to the damage was calculated : fire damage magnification . 1 usually .")]
			[DefaultValue((Single)1)]
			public Single ThunderDamageCutRate {
				get { return thunderDamageCutRate; }
				set {
					if ((double)value < -99 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -99 to 99 for " + ThunderDamageCutRateProperty.Name + ".");
					SetProperty(ref thunderDamageCutRate, ref value, ThunderDamageCutRateProperty);
				}
			}

			/// <summary>Attacker : Physical Damage magnification</summary>
			/// <remarks>
			/// Japanese short name: "攻撃側：物理ダメージ倍率", Google translated: "Attacker : Physical Damage magnification".
			/// Japanese description: "物理ダメージ倍率：算出したダメージに×○倍で補正をかける。１が通常。", Google translated: "To apply a correction in ○ × magnification to the damage was calculated : Physical Damage magnification . 1 usually .".
			/// </remarks>
			[ParameterTableRowAttribute("physicsAttackRate", index: 14, minimum: -99, maximum: 99, step: 0.001, order: 3200, unknown2: 1)]
			[DisplayName("Attacker : Physical Damage magnification")]
			[Description("To apply a correction in ○ × magnification to the damage was calculated : Physical Damage magnification . 1 usually .")]
			[DefaultValue((Single)1)]
			public Single PhysicsAttackRate {
				get { return physicsAttackRate; }
				set {
					if ((double)value < -99 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -99 to 99 for " + PhysicsAttackRateProperty.Name + ".");
					SetProperty(ref physicsAttackRate, ref value, PhysicsAttackRateProperty);
				}
			}

			/// <summary>Attacker : Magic Damage magnification</summary>
			/// <remarks>
			/// Japanese short name: "攻撃側：魔法ダメージ倍率", Google translated: "Attacker : Magic Damage magnification".
			/// Japanese description: "魔法ダメージ倍率：算出したダメージに×○倍で補正をかける。１が通常。", Google translated: "To apply a correction in ○ × magnification to the damage was calculated : Magic Damage magnification . 1 usually .".
			/// </remarks>
			[ParameterTableRowAttribute("magicAttackRate", index: 15, minimum: -99, maximum: 99, step: 0.001, order: 3300, unknown2: 1)]
			[DisplayName("Attacker : Magic Damage magnification")]
			[Description("To apply a correction in ○ × magnification to the damage was calculated : Magic Damage magnification . 1 usually .")]
			[DefaultValue((Single)1)]
			public Single MagicAttackRate {
				get { return magicAttackRate; }
				set {
					if ((double)value < -99 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -99 to 99 for " + MagicAttackRateProperty.Name + ".");
					SetProperty(ref magicAttackRate, ref value, MagicAttackRateProperty);
				}
			}

			/// <summary>Attacker : fire damage magnification</summary>
			/// <remarks>
			/// Japanese short name: "攻撃側：炎ダメージ倍率", Google translated: "Attacker : fire damage magnification".
			/// Japanese description: "炎ダメージ倍率：算出したダメージに×○倍で補正をかける。１が通常。", Google translated: "To apply a correction in ○ × magnification to the damage was calculated : fire damage magnification . 1 usually .".
			/// </remarks>
			[ParameterTableRowAttribute("fireAttackRate", index: 16, minimum: -99, maximum: 99, step: 0.001, order: 3400, unknown2: 1)]
			[DisplayName("Attacker : fire damage magnification")]
			[Description("To apply a correction in ○ × magnification to the damage was calculated : fire damage magnification . 1 usually .")]
			[DefaultValue((Single)1)]
			public Single FireAttackRate {
				get { return fireAttackRate; }
				set {
					if ((double)value < -99 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -99 to 99 for " + FireAttackRateProperty.Name + ".");
					SetProperty(ref fireAttackRate, ref value, FireAttackRateProperty);
				}
			}

			/// <summary>Attacker : shock damage magnification</summary>
			/// <remarks>
			/// Japanese short name: "攻撃側：電撃ダメージ倍率", Google translated: "Attacker : shock damage magnification".
			/// Japanese description: "電撃ダメージ倍率：算出したダメージに×○倍で補正をかける。１が通常。", Google translated: "To apply a correction in ○ × magnification to the damage was calculated : lightning damage magnification . 1 usually .".
			/// </remarks>
			[ParameterTableRowAttribute("thunderAttackRate", index: 17, minimum: -99, maximum: 99, step: 0.001, order: 3410, unknown2: 1)]
			[DisplayName("Attacker : shock damage magnification")]
			[Description("To apply a correction in ○ × magnification to the damage was calculated : lightning damage magnification . 1 usually .")]
			[DefaultValue((Single)1)]
			public Single ThunderAttackRate {
				get { return thunderAttackRate; }
				set {
					if ((double)value < -99 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -99 to 99 for " + ThunderAttackRateProperty.Name + ".");
					SetProperty(ref thunderAttackRate, ref value, ThunderAttackRateProperty);
				}
			}

			/// <summary>Physical Attack magnification</summary>
			/// <remarks>
			/// Japanese short name: "物理攻撃力倍率", Google translated: "Physical Attack magnification".
			/// Japanese description: "物理攻撃力に設定した数値をかけます", Google translated: "Multiply the number you set to Physical Attack".
			/// </remarks>
			[ParameterTableRowAttribute("physicsAttackPowerRate", index: 18, minimum: -99, maximum: 99, step: 0.001, order: 3500, unknown2: 1)]
			[DisplayName("Physical Attack magnification")]
			[Description("Multiply the number you set to Physical Attack")]
			[DefaultValue((Single)1)]
			public Single PhysicsAttackPowerRate {
				get { return physicsAttackPowerRate; }
				set {
					if ((double)value < -99 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -99 to 99 for " + PhysicsAttackPowerRateProperty.Name + ".");
					SetProperty(ref physicsAttackPowerRate, ref value, PhysicsAttackPowerRateProperty);
				}
			}

			/// <summary>Magic attack power magnification</summary>
			/// <remarks>
			/// Japanese short name: "魔法攻撃力倍率", Google translated: "Magic attack power magnification".
			/// Japanese description: "魔法攻撃力に設定した数値をかけます", Google translated: "Multiply the number you set in the Magic Attack".
			/// </remarks>
			[ParameterTableRowAttribute("magicAttackPowerRate", index: 19, minimum: -99, maximum: 99, step: 0.001, order: 3600, unknown2: 1)]
			[DisplayName("Magic attack power magnification")]
			[Description("Multiply the number you set in the Magic Attack")]
			[DefaultValue((Single)1)]
			public Single MagicAttackPowerRate {
				get { return magicAttackPowerRate; }
				set {
					if ((double)value < -99 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -99 to 99 for " + MagicAttackPowerRateProperty.Name + ".");
					SetProperty(ref magicAttackPowerRate, ref value, MagicAttackPowerRateProperty);
				}
			}

			/// <summary>Flame attack power magnification</summary>
			/// <remarks>
			/// Japanese short name: "炎攻撃力倍率", Google translated: "Flame attack power magnification".
			/// Japanese description: "炎攻撃力に設定した数値をかけます", Google translated: "Multiply the number you set fire to attack power".
			/// </remarks>
			[ParameterTableRowAttribute("fireAttackPowerRate", index: 20, minimum: -99, maximum: 99, step: 0.001, order: 3700, unknown2: 1)]
			[DisplayName("Flame attack power magnification")]
			[Description("Multiply the number you set fire to attack power")]
			[DefaultValue((Single)1)]
			public Single FireAttackPowerRate {
				get { return fireAttackPowerRate; }
				set {
					if ((double)value < -99 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -99 to 99 for " + FireAttackPowerRateProperty.Name + ".");
					SetProperty(ref fireAttackPowerRate, ref value, FireAttackPowerRateProperty);
				}
			}

			/// <summary>Blitz attack power magnification</summary>
			/// <remarks>
			/// Japanese short name: "電撃攻撃力倍率", Google translated: "Blitz attack power magnification".
			/// Japanese description: "電撃攻撃力に設定した数値をかけます", Google translated: "Multiply the number you set to blitz attack power".
			/// </remarks>
			[ParameterTableRowAttribute("thunderAttackPowerRate", index: 21, minimum: -99, maximum: 99, step: 0.001, order: 3710, unknown2: 1)]
			[DisplayName("Blitz attack power magnification")]
			[Description("Multiply the number you set to blitz attack power")]
			[DefaultValue((Single)1)]
			public Single ThunderAttackPowerRate {
				get { return thunderAttackPowerRate; }
				set {
					if ((double)value < -99 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -99 to 99 for " + ThunderAttackPowerRateProperty.Name + ".");
					SetProperty(ref thunderAttackPowerRate, ref value, ThunderAttackPowerRateProperty);
				}
			}

			/// <summary>Physical Attack [point]</summary>
			/// <remarks>
			/// Japanese short name: "物理攻撃力[point]", Google translated: "Physical Attack [point]".
			/// Japanese description: "物理攻撃力に設定した数値を加減算する", Google translated: "And adding or subtracting the value it is set to Physical Attack".
			/// </remarks>
			[ParameterTableRowAttribute("physicsAttackPower", index: 22, minimum: -9999, maximum: 9999, step: 1, order: 3800, unknown2: 1)]
			[DisplayName("Physical Attack [point]")]
			[Description("And adding or subtracting the value it is set to Physical Attack")]
			[DefaultValue((Int32)0)]
			public Int32 PhysicsAttackPower {
				get { return physicsAttackPower; }
				set {
					if ((double)value < -9999 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -9999 to 9999 for " + PhysicsAttackPowerProperty.Name + ".");
					SetProperty(ref physicsAttackPower, ref value, PhysicsAttackPowerProperty);
				}
			}

			/// <summary>Magic Attack [point]</summary>
			/// <remarks>
			/// Japanese short name: "魔法攻撃力[point]", Google translated: "Magic Attack [point]".
			/// Japanese description: "魔法攻撃力に設定した数値を加減算する", Google translated: "And adding or subtracting the number you set in the Magic Attack".
			/// </remarks>
			[ParameterTableRowAttribute("magicAttackPower", index: 23, minimum: -9999, maximum: 9999, step: 1, order: 3900, unknown2: 1)]
			[DisplayName("Magic Attack [point]")]
			[Description("And adding or subtracting the number you set in the Magic Attack")]
			[DefaultValue((Int32)0)]
			public Int32 MagicAttackPower {
				get { return magicAttackPower; }
				set {
					if ((double)value < -9999 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -9999 to 9999 for " + MagicAttackPowerProperty.Name + ".");
					SetProperty(ref magicAttackPower, ref value, MagicAttackPowerProperty);
				}
			}

			/// <summary>Flame attack power [point]</summary>
			/// <remarks>
			/// Japanese short name: "炎攻撃力[point]", Google translated: "Flame attack power [point]".
			/// Japanese description: "炎攻撃力に設定した数値を加減算する", Google translated: "And adding or subtracting the number you set fire to attack power".
			/// </remarks>
			[ParameterTableRowAttribute("fireAttackPower", index: 24, minimum: -9999, maximum: 9999, step: 1, order: 4000, unknown2: 1)]
			[DisplayName("Flame attack power [point]")]
			[Description("And adding or subtracting the number you set fire to attack power")]
			[DefaultValue((Int32)0)]
			public Int32 FireAttackPower {
				get { return fireAttackPower; }
				set {
					if ((double)value < -9999 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -9999 to 9999 for " + FireAttackPowerProperty.Name + ".");
					SetProperty(ref fireAttackPower, ref value, FireAttackPowerProperty);
				}
			}

			/// <summary>Blitz Attack Power [point]</summary>
			/// <remarks>
			/// Japanese short name: "電撃攻撃力[point]", Google translated: "Blitz Attack Power [point]".
			/// Japanese description: "電撃攻撃力に設定した数値を加減算する", Google translated: "And adding or subtracting the value it is set to blitz attack power".
			/// </remarks>
			[ParameterTableRowAttribute("thunderAttackPower", index: 25, minimum: -9999, maximum: 9999, step: 1, order: 4010, unknown2: 1)]
			[DisplayName("Blitz Attack Power [point]")]
			[Description("And adding or subtracting the value it is set to blitz attack power")]
			[DefaultValue((Int32)0)]
			public Int32 ThunderAttackPower {
				get { return thunderAttackPower; }
				set {
					if ((double)value < -9999 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -9999 to 9999 for " + ThunderAttackPowerProperty.Name + ".");
					SetProperty(ref thunderAttackPower, ref value, ThunderAttackPowerProperty);
				}
			}

			/// <summary>Physical Def magnification</summary>
			/// <remarks>
			/// Japanese short name: "物理防御力倍率", Google translated: "Physical Def magnification".
			/// Japanese description: "物理防御力に設定した数値をかけます", Google translated: "Multiply the number you set in Physical Defense".
			/// </remarks>
			[ParameterTableRowAttribute("physicsDiffenceRate", index: 26, minimum: -99, maximum: 99, step: 0.001, order: 4100, unknown2: 1)]
			[DisplayName("Physical Def magnification")]
			[Description("Multiply the number you set in Physical Defense")]
			[DefaultValue((Single)1)]
			public Single PhysicsDiffenceRate {
				get { return physicsDiffenceRate; }
				set {
					if ((double)value < -99 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -99 to 99 for " + PhysicsDiffenceRateProperty.Name + ".");
					SetProperty(ref physicsDiffenceRate, ref value, PhysicsDiffenceRateProperty);
				}
			}

			/// <summary>Magic defense magnification</summary>
			/// <remarks>
			/// Japanese short name: "魔法防御力倍率", Google translated: "Magic defense magnification".
			/// Japanese description: "魔法防御力に設定した数値をかけます", Google translated: "Multiply the number you set in the magic defense".
			/// </remarks>
			[ParameterTableRowAttribute("magicDiffenceRate", index: 27, minimum: -99, maximum: 99, step: 0.001, order: 4200, unknown2: 1)]
			[DisplayName("Magic defense magnification")]
			[Description("Multiply the number you set in the magic defense")]
			[DefaultValue((Single)1)]
			public Single MagicDiffenceRate {
				get { return magicDiffenceRate; }
				set {
					if ((double)value < -99 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -99 to 99 for " + MagicDiffenceRateProperty.Name + ".");
					SetProperty(ref magicDiffenceRate, ref value, MagicDiffenceRateProperty);
				}
			}

			/// <summary>Flame Defense magnification</summary>
			/// <remarks>
			/// Japanese short name: "炎防御力倍率", Google translated: "Flame Defense magnification".
			/// Japanese description: "炎防御力に設定した数値をかけます", Google translated: "Multiply the number you set on fire defense".
			/// </remarks>
			[ParameterTableRowAttribute("fireDiffenceRate", index: 28, minimum: -99, maximum: 99, step: 0.001, order: 4300, unknown2: 1)]
			[DisplayName("Flame Defense magnification")]
			[Description("Multiply the number you set on fire defense")]
			[DefaultValue((Single)1)]
			public Single FireDiffenceRate {
				get { return fireDiffenceRate; }
				set {
					if ((double)value < -99 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -99 to 99 for " + FireDiffenceRateProperty.Name + ".");
					SetProperty(ref fireDiffenceRate, ref value, FireDiffenceRateProperty);
				}
			}

			/// <summary>Blitz Defense magnification</summary>
			/// <remarks>
			/// Japanese short name: "電撃防御力倍率", Google translated: "Blitz Defense magnification".
			/// Japanese description: "電撃防御力に設定した数値をかけます", Google translated: "Multiply the number you set in the blitz defense".
			/// </remarks>
			[ParameterTableRowAttribute("thunderDiffenceRate", index: 29, minimum: -99, maximum: 99, step: 0.001, order: 4310, unknown2: 1)]
			[DisplayName("Blitz Defense magnification")]
			[Description("Multiply the number you set in the blitz defense")]
			[DefaultValue((Single)1)]
			public Single ThunderDiffenceRate {
				get { return thunderDiffenceRate; }
				set {
					if ((double)value < -99 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -99 to 99 for " + ThunderDiffenceRateProperty.Name + ".");
					SetProperty(ref thunderDiffenceRate, ref value, ThunderDiffenceRateProperty);
				}
			}

			/// <summary>Physical Defense [point]</summary>
			/// <remarks>
			/// Japanese short name: "物理防御力[point]", Google translated: "Physical Defense [point]".
			/// Japanese description: "物理防御力に設定した数値を加減算する", Google translated: "And adding or subtracting the number you set in Physical Defense".
			/// </remarks>
			[ParameterTableRowAttribute("physicsDiffence", index: 30, minimum: -9999, maximum: 9999, step: 1, order: 4400, unknown2: 1)]
			[DisplayName("Physical Defense [point]")]
			[Description("And adding or subtracting the number you set in Physical Defense")]
			[DefaultValue((Int32)0)]
			public Int32 PhysicsDiffence {
				get { return physicsDiffence; }
				set {
					if ((double)value < -9999 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -9999 to 9999 for " + PhysicsDiffenceProperty.Name + ".");
					SetProperty(ref physicsDiffence, ref value, PhysicsDiffenceProperty);
				}
			}

			/// <summary>Magic defense [point]</summary>
			/// <remarks>
			/// Japanese short name: "魔法防御力[point]", Google translated: "Magic defense [point]".
			/// Japanese description: "魔法防御力に設定した数値を加減算する", Google translated: "And adding or subtracting the number you set in the magic defense".
			/// </remarks>
			[ParameterTableRowAttribute("magicDiffence", index: 31, minimum: -9999, maximum: 9999, step: 1, order: 4500, unknown2: 1)]
			[DisplayName("Magic defense [point]")]
			[Description("And adding or subtracting the number you set in the magic defense")]
			[DefaultValue((Int32)0)]
			public Int32 MagicDiffence {
				get { return magicDiffence; }
				set {
					if ((double)value < -9999 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -9999 to 9999 for " + MagicDiffenceProperty.Name + ".");
					SetProperty(ref magicDiffence, ref value, MagicDiffenceProperty);
				}
			}

			/// <summary>Flame Defense [point]</summary>
			/// <remarks>
			/// Japanese short name: "炎防御力[point]", Google translated: "Flame Defense [point]".
			/// Japanese description: "炎防御力に設定した数値を加減算する", Google translated: "And adding or subtracting a number that has been set to fire Defense".
			/// </remarks>
			[ParameterTableRowAttribute("fireDiffence", index: 32, minimum: -9999, maximum: 9999, step: 1, order: 4600, unknown2: 1)]
			[DisplayName("Flame Defense [point]")]
			[Description("And adding or subtracting a number that has been set to fire Defense")]
			[DefaultValue((Int32)0)]
			public Int32 FireDiffence {
				get { return fireDiffence; }
				set {
					if ((double)value < -9999 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -9999 to 9999 for " + FireDiffenceProperty.Name + ".");
					SetProperty(ref fireDiffence, ref value, FireDiffenceProperty);
				}
			}

			/// <summary>Blitz Defense [point]</summary>
			/// <remarks>
			/// Japanese short name: "電撃防御力[point]", Google translated: "Blitz Defense [point]".
			/// Japanese description: "電撃防御力に設定した数値を加減算する", Google translated: "And adding or subtracting the value it is set to blitz defense".
			/// </remarks>
			[ParameterTableRowAttribute("thunderDiffence", index: 33, minimum: -9999, maximum: 9999, step: 1, order: 4610, unknown2: 1)]
			[DisplayName("Blitz Defense [point]")]
			[Description("And adding or subtracting the value it is set to blitz defense")]
			[DefaultValue((Int32)0)]
			public Int32 ThunderDiffence {
				get { return thunderDiffence; }
				set {
					if ((double)value < -9999 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -9999 to 9999 for " + ThunderDiffenceProperty.Name + ".");
					SetProperty(ref thunderDiffence, ref value, ThunderDiffenceProperty);
				}
			}

			/// <summary>Chance damage magnification</summary>
			/// <remarks>
			/// Japanese short name: "隙ダメージ倍率", Google translated: "Chance damage magnification".
			/// Japanese description: "隙のときのダメージ倍率を、設定した数値に置き換える（ダメージ側に設定）", Google translated: "( Set to damage the side ) to replace the values ​​set , the magnification of the damage when the gap".
			/// </remarks>
			[ParameterTableRowAttribute("NoGuardDamageRate", index: 34, minimum: -100, maximum: 100, step: 0.01, order: 4700, unknown2: 1)]
			[DisplayName("Chance damage magnification")]
			[Description("( Set to damage the side ) to replace the values ​​set , the magnification of the damage when the gap")]
			[DefaultValue((Single)1)]
			public Single NoGuardDamageRate {
				get { return noGuardDamageRate; }
				set {
					if ((double)value < -100 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -100 to 100 for " + NoGuardDamageRateProperty.Name + ".");
					SetProperty(ref noGuardDamageRate, ref value, NoGuardDamageRateProperty);
				}
			}

			/// <summary>Sweet spot magnification</summary>
			/// <remarks>
			/// Japanese short name: "スィートスポット倍率", Google translated: "Sweet spot magnification".
			/// Japanese description: "スィートスポットのダメージ計算を指定した数値に差し替える(急所ダメージ補正) -1.0で無効", Google translated: "Disable to replace the numerical value that you specify a damage calculation of the sweet spot in the ( Key points damage correction ) -1.0".
			/// </remarks>
			[ParameterTableRowAttribute("vitalSpotChangeRate", index: 35, minimum: -1, maximum: 99, step: 0.001, order: 4800, unknown2: 1)]
			[DisplayName("Sweet spot magnification")]
			[Description("Disable to replace the numerical value that you specify a damage calculation of the sweet spot in the ( Key points damage correction ) -1.0")]
			[DefaultValue((Single)(-1))]
			public Single VitalSpotChangeRate {
				get { return vitalSpotChangeRate; }
				set {
					if ((double)value < -1 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 99 for " + VitalSpotChangeRateProperty.Name + ".");
					SetProperty(ref vitalSpotChangeRate, ref value, VitalSpotChangeRateProperty);
				}
			}

			/// <summary>Normal hit magnification</summary>
			/// <remarks>
			/// Japanese short name: "ノーマルヒット倍率", Google translated: "Normal hit magnification".
			/// Japanese description: "ノーマルヒットのダメージ計算を指定した数値に差し替える  -1.0で無効", Google translated: "Disabled by -1.0 to plug in the number you specify the damage calculation of normal hit".
			/// </remarks>
			[ParameterTableRowAttribute("normalSpotChangeRate", index: 36, minimum: -1, maximum: 99, step: 0.001, order: 4900, unknown2: 1)]
			[DisplayName("Normal hit magnification")]
			[Description("Disabled by -1.0 to plug in the number you specify the damage calculation of normal hit")]
			[DefaultValue((Single)(-1))]
			public Single NormalSpotChangeRate {
				get { return normalSpotChangeRate; }
				set {
					if ((double)value < -1 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 99 for " + NormalSpotChangeRateProperty.Name + ".");
					SetProperty(ref normalSpotChangeRate, ref value, NormalSpotChangeRateProperty);
				}
			}

			/// <summary>Maximum HP fold change</summary>
			/// <remarks>
			/// Japanese short name: "最大HP変化倍率", Google translated: "Maximum HP fold change".
			/// Japanese description: "最大HPに対して、設定された倍率をかけて増減させる", Google translated: "To increase or decrease over the magnification of the maximum HP, the set".
			/// </remarks>
			[ParameterTableRowAttribute("maxHpChangeRate", index: 37, minimum: 0, maximum: 99, step: 0.001, order: 5000, unknown2: 1)]
			[DisplayName("Maximum HP fold change")]
			[Description("To increase or decrease over the magnification of the maximum HP, the set")]
			[DefaultValue((Single)0)]
			public Single MaxHpChangeRate {
				get { return maxHpChangeRate; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for " + MaxHpChangeRateProperty.Name + ".");
					SetProperty(ref maxHpChangeRate, ref value, MaxHpChangeRateProperty);
				}
			}

			/// <summary>Action ID specified frame</summary>
			/// <remarks>
			/// Japanese short name: "行動ID指定枠", Google translated: "Action ID specified frame".
			/// Japanese description: "特殊効果から行動IDを使ってダメージを与える場合に指定-1で無効", Google translated: "Invalid specify -1 in the case of damage by using the action ID from the special effects".
			/// </remarks>
			[ParameterTableRowAttribute("behaviorId", index: 38, minimum: -1, maximum: 1E+08, step: 1, order: 5010, unknown2: 1)]
			[DisplayName("Action ID specified frame")]
			[Description("Invalid specify -1 in the case of damage by using the action ID from the special effects")]
			[DefaultValue((Int32)(-1))]
			public Int32 BehaviorId {
				get { return behaviorId; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for " + BehaviorIdProperty.Name + ".");
					SetProperty(ref behaviorId, ref value, BehaviorIdProperty);
				}
			}

			/// <summary>HP amount of damage [% ]</summary>
			/// <remarks>
			/// Japanese short name: "HPダメージ量[%]", Google translated: "HP amount of damage [% ]".
			/// Japanese description: "最大HPの何%分を毎秒加算（または減算）するかを設定", Google translated: "Set what % of the maximum HP whether to ( or subtracted ) per second addition".
			/// </remarks>
			[ParameterTableRowAttribute("changeHpRate", index: 39, minimum: -100, maximum: 100, step: 0.1, order: 5100, unknown2: 1)]
			[DisplayName("HP amount of damage [% ]")]
			[Description("Set what % of the maximum HP whether to ( or subtracted ) per second addition")]
			[DefaultValue((Single)0)]
			public Single ChangeHpRate {
				get { return changeHpRate; }
				set {
					if ((double)value < -100 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -100 to 100 for " + ChangeHpRateProperty.Name + ".");
					SetProperty(ref changeHpRate, ref value, ChangeHpRateProperty);
				}
			}

			/// <summary>HP damage [point]</summary>
			/// <remarks>
			/// Japanese short name: "HPダメージ[point]", Google translated: "HP damage [point]".
			/// Japanese description: "1秒間に何ポイント加算（または減算）するかを設定", Google translated: "Set what point addition whether to ( or subtracted from ) per second".
			/// </remarks>
			[ParameterTableRowAttribute("changeHpPoint", index: 40, minimum: -9999, maximum: 9999, step: 1, order: 5200, unknown2: 1)]
			[DisplayName("HP damage [point]")]
			[Description("Set what point addition whether to ( or subtracted from ) per second")]
			[DefaultValue((Int32)0)]
			public Int32 ChangeHpPoint {
				get { return changeHpPoint; }
				set {
					if ((double)value < -9999 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -9999 to 9999 for " + ChangeHpPointProperty.Name + ".");
					SetProperty(ref changeHpPoint, ref value, ChangeHpPointProperty);
				}
			}

			/// <summary>MP amount of damage [% ]</summary>
			/// <remarks>
			/// Japanese short name: "MPダメージ量[%]", Google translated: "MP amount of damage [% ]".
			/// Japanese description: "最大MPの何%分を毎秒加算（または減算）するかを設定", Google translated: "Set what % of the maximum MP whether to ( or subtracted ) per second addition".
			/// </remarks>
			[ParameterTableRowAttribute("changeMpRate", index: 41, minimum: -100, maximum: 100, step: 0.1, order: 5300, unknown2: 1)]
			[DisplayName("MP amount of damage [% ]")]
			[Description("Set what % of the maximum MP whether to ( or subtracted ) per second addition")]
			[DefaultValue((Single)0)]
			public Single ChangeMpRate {
				get { return changeMpRate; }
				set {
					if ((double)value < -100 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -100 to 100 for " + ChangeMpRateProperty.Name + ".");
					SetProperty(ref changeMpRate, ref value, ChangeMpRateProperty);
				}
			}

			/// <summary>MP damage [point]</summary>
			/// <remarks>
			/// Japanese short name: "MPダメージ[point]", Google translated: "MP damage [point]".
			/// Japanese description: "1秒間に何ポイント加算（または減算）するかを設定", Google translated: "Set what point addition whether to ( or subtracted from ) per second".
			/// </remarks>
			[ParameterTableRowAttribute("changeMpPoint", index: 42, minimum: -9999, maximum: 9999, step: 1, order: 5400, unknown2: 1)]
			[DisplayName("MP damage [point]")]
			[Description("Set what point addition whether to ( or subtracted from ) per second")]
			[DefaultValue((Int32)0)]
			public Int32 ChangeMpPoint {
				get { return changeMpPoint; }
				set {
					if ((double)value < -9999 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -9999 to 9999 for " + ChangeMpPointProperty.Name + ".");
					SetProperty(ref changeMpPoint, ref value, ChangeMpPointProperty);
				}
			}

			/// <summary>MP recovery speed change [point]</summary>
			/// <remarks>
			/// Japanese short name: "MP回復速度変化[point]", Google translated: "MP recovery speed change [point]".
			/// Japanese description: "回復速度を変化させる。回復計算式の基準回復速度、初期回復速度に加減算する。", Google translated: "I vary the rate of recovery . I added to or subtracted from a reference rate of recovery of the recovery formula , the initial recovery rate .".
			/// </remarks>
			[ParameterTableRowAttribute("mpRecoverChangeSpeed", index: 43, minimum: -100, maximum: 100, step: 1, order: 5500, unknown2: 1)]
			[DisplayName("MP recovery speed change [point]")]
			[Description("I vary the rate of recovery . I added to or subtracted from a reference rate of recovery of the recovery formula , the initial recovery rate .")]
			[DefaultValue((Int32)0)]
			public Int32 MpRecoverChangeSpeed {
				get { return mpRecoverChangeSpeed; }
				set {
					if ((double)value < -100 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -100 to 100 for " + MpRecoverChangeSpeedProperty.Name + ".");
					SetProperty(ref mpRecoverChangeSpeed, ref value, MpRecoverChangeSpeedProperty);
				}
			}

			/// <summary>Stamina amount of damage [% ]</summary>
			/// <remarks>
			/// Japanese short name: "スタミナダメージ量[%]", Google translated: "Stamina amount of damage [% ]".
			/// Japanese description: "最大スタミナの何%分を毎秒加算（または減算）するかを設定", Google translated: "Set what % of the maximum stamina whether to ( or subtracted ) per second addition".
			/// </remarks>
			[ParameterTableRowAttribute("changeStaminaRate", index: 44, minimum: -100, maximum: 100, step: 1, order: 5600, unknown2: 1)]
			[DisplayName("Stamina amount of damage [% ]")]
			[Description("Set what % of the maximum stamina whether to ( or subtracted ) per second addition")]
			[DefaultValue((Single)0)]
			public Single ChangeStaminaRate {
				get { return changeStaminaRate; }
				set {
					if ((double)value < -100 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -100 to 100 for " + ChangeStaminaRateProperty.Name + ".");
					SetProperty(ref changeStaminaRate, ref value, ChangeStaminaRateProperty);
				}
			}

			/// <summary>Stamina damage [point]</summary>
			/// <remarks>
			/// Japanese short name: "スタミナダメージ[point]", Google translated: "Stamina damage [point]".
			/// Japanese description: "1秒間に何ポイント加算（または減算）するかを設定", Google translated: "Set what point addition whether to ( or subtracted from ) per second".
			/// </remarks>
			[ParameterTableRowAttribute("changeStaminaPoint", index: 45, minimum: -9999, maximum: 9999, step: 1, order: 5700, unknown2: 1)]
			[DisplayName("Stamina damage [point]")]
			[Description("Set what point addition whether to ( or subtracted from ) per second")]
			[DefaultValue((Int32)0)]
			public Int32 ChangeStaminaPoint {
				get { return changeStaminaPoint; }
				set {
					if ((double)value < -9999 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -9999 to 9999 for " + ChangeStaminaPointProperty.Name + ".");
					SetProperty(ref changeStaminaPoint, ref value, ChangeStaminaPointProperty);
				}
			}

			/// <summary>Stamina recovery speed change [point]</summary>
			/// <remarks>
			/// Japanese short name: "スタミナ回復速度変化[point]", Google translated: "Stamina recovery speed change [point]".
			/// Japanese description: "回復速度を変化させる。回復計算式の基準回復速度、初期回復速度に加減算する。", Google translated: "I vary the rate of recovery . I added to or subtracted from a reference rate of recovery of the recovery formula , the initial recovery rate .".
			/// </remarks>
			[ParameterTableRowAttribute("staminaRecoverChangeSpeed", index: 46, minimum: -100, maximum: 100, step: 1, order: 5800, unknown2: 1)]
			[DisplayName("Stamina recovery speed change [point]")]
			[Description("I vary the rate of recovery . I added to or subtracted from a reference rate of recovery of the recovery formula , the initial recovery rate .")]
			[DefaultValue((Int32)0)]
			public Int32 StaminaRecoverChangeSpeed {
				get { return staminaRecoverChangeSpeed; }
				set {
					if ((double)value < -100 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -100 to 100 for " + StaminaRecoverChangeSpeedProperty.Name + ".");
					SetProperty(ref staminaRecoverChangeSpeed, ref value, StaminaRecoverChangeSpeedProperty);
				}
			}

			/// <summary>Magic effect time change</summary>
			/// <remarks>
			/// Japanese short name: "魔法効果時間変化", Google translated: "Magic effect time change".
			/// Japanese description: "効果持続時間に0.1秒以上設定されている魔法のみ、効果持続時間に設定されている時間を加減算する", Google translated: "Magic is set at least 0.1 second in duration of effect only , adding or subtracting the time set in the duration of effect".
			/// </remarks>
			[ParameterTableRowAttribute("magicEffectTimeChange", index: 47, minimum: -999, maximum: 999, step: 0.1, order: 5900, unknown2: 1)]
			[DisplayName("Magic effect time change")]
			[Description("Magic is set at least 0.1 second in duration of effect only , adding or subtracting the time set in the duration of effect")]
			[DefaultValue((Single)0)]
			public Single MagicEffectTimeChange {
				get { return magicEffectTimeChange; }
				set {
					if ((double)value < -999 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -999 to 999 for " + MagicEffectTimeChangeProperty.Name + ".");
					SetProperty(ref magicEffectTimeChange, ref value, MagicEffectTimeChangeProperty);
				}
			}

			/// <summary>Durability change : internal wear degree [point]</summary>
			/// <remarks>
			/// Japanese short name: "耐久度変化：内部損耗度[point]", Google translated: "Durability change : internal wear degree [point]".
			/// Japanese description: "内部損耗度に数値分を加減算する", Google translated: "I added to or subtracted from the value worth of internal wear degree".
			/// </remarks>
			[ParameterTableRowAttribute("insideDurability", index: 48, minimum: -999, maximum: 999, step: 1, order: 6000, unknown2: 1)]
			[DisplayName("Durability change : internal wear degree [point]")]
			[Description("I added to or subtracted from the value worth of internal wear degree")]
			[DefaultValue((Int32)0)]
			public Int32 InsideDurability {
				get { return insideDurability; }
				set {
					if ((double)value < -999 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -999 to 999 for " + InsideDurabilityProperty.Name + ".");
					SetProperty(ref insideDurability, ref value, InsideDurabilityProperty);
				}
			}

			/// <summary>Durability change : maximum wear of change [point]</summary>
			/// <remarks>
			/// Japanese short name: "耐久度変化：最大損耗度変化[point]", Google translated: "Durability change : maximum wear of change [point]".
			/// Japanese description: "耐久度の内部損耗度の最大値に、設定された数値を加算する", Google translated: "The maximum value of the internal wear of the durability , I will add the number set".
			/// </remarks>
			[ParameterTableRowAttribute("maxDurability", index: 49, minimum: 0, maximum: 999, step: 1, order: 6100, unknown2: 1)]
			[DisplayName("Durability change : maximum wear of change [point]")]
			[Description("The maximum value of the internal wear of the durability , I will add the number set")]
			[DefaultValue((Int32)0)]
			public Int32 MaxDurability {
				get { return maxDurability; }
				set {
					if ((double)value < 0 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for " + MaxDurabilityProperty.Name + ".");
					SetProperty(ref maxDurability, ref value, MaxDurabilityProperty);
				}
			}

			/// <summary>Stamina attack power magnification</summary>
			/// <remarks>
			/// Japanese short name: "スタミナ攻撃力倍率", Google translated: "Stamina attack power magnification".
			/// Japanese description: "スタミナ攻撃力に、倍率をかける(1.0 1倍 0.5 半分）", Google translated: "Stamina to attack power , and multiplying by magnification (1.0 × 0.5 1 half )".
			/// </remarks>
			[ParameterTableRowAttribute("staminaAttackRate", index: 50, minimum: 0, maximum: 99, step: 0.1, order: 6200, unknown2: 1)]
			[DisplayName("Stamina attack power magnification")]
			[Description("Stamina to attack power , and multiplying by magnification (1.0 × 0.5 1 half )")]
			[DefaultValue((Single)1)]
			public Single StaminaAttackRate {
				get { return staminaAttackRate; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for " + StaminaAttackRateProperty.Name + ".");
					SetProperty(ref staminaAttackRate, ref value, StaminaAttackRateProperty);
				}
			}

			/// <summary>Poison Resistance attack power [point]</summary>
			/// <remarks>
			/// Japanese short name: "毒耐性攻撃力[point]", Google translated: "Poison Resistance attack power [point]".
			/// Japanese description: "ヒットした時に、対象の【毒耐性値】に加算する数値", Google translated: "When you hit, the numerical value to be added to the target of poison resistance value ]".
			/// </remarks>
			[ParameterTableRowAttribute("poizonAttackPower", index: 51, minimum: -99999, maximum: 99999, step: 1, order: 6400, unknown2: 1)]
			[DisplayName("Poison Resistance attack power [point]")]
			[Description("When you hit, the numerical value to be added to the target of poison resistance value ]")]
			[DefaultValue((Int32)0)]
			public Int32 PoizonAttackPower {
				get { return poizonAttackPower; }
				set {
					if ((double)value < -99999 || (double)value > 99999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -99999 to 99999 for " + PoizonAttackPowerProperty.Name + ".");
					SetProperty(ref poizonAttackPower, ref value, PoizonAttackPowerProperty);
				}
			}

			/// <summary>Resistant plague attack power [point]</summary>
			/// <remarks>
			/// Japanese short name: "疫病耐性攻撃力[point]", Google translated: "Resistant plague attack power [point]".
			/// Japanese description: "ヒットした時に、対象の【疫病耐性値】に加算する数値", Google translated: "When you hit, the numerical value to be added to the target resistance value [ plague ]".
			/// </remarks>
			[ParameterTableRowAttribute("registIllness", index: 52, minimum: -99999, maximum: 99999, step: 1, order: 6500, unknown2: 1)]
			[DisplayName("Resistant plague attack power [point]")]
			[Description("When you hit, the numerical value to be added to the target resistance value [ plague ]")]
			[DefaultValue((Int32)0)]
			public Int32 RegistIllness {
				get { return registIllness; }
				set {
					if ((double)value < -99999 || (double)value > 99999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -99999 to 99999 for " + RegistIllnessProperty.Name + ".");
					SetProperty(ref registIllness, ref value, RegistIllnessProperty);
				}
			}

			/// <summary>Bleeding resistance attack power [point]</summary>
			/// <remarks>
			/// Japanese short name: "出血耐性攻撃力[point]", Google translated: "Bleeding resistance attack power [point]".
			/// Japanese description: "ヒットした時に、対象の【出血耐性値】に加算する数値", Google translated: "When you hit, the numerical value to be added to the target resistance value [ bleeding ]".
			/// </remarks>
			[ParameterTableRowAttribute("registBlood", index: 53, minimum: -99999, maximum: 99999, step: 1, order: 6600, unknown2: 1)]
			[DisplayName("Bleeding resistance attack power [point]")]
			[Description("When you hit, the numerical value to be added to the target resistance value [ bleeding ]")]
			[DefaultValue((Int32)0)]
			public Int32 RegistBlood {
				get { return registBlood; }
				set {
					if ((double)value < -99999 || (double)value > 99999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -99999 to 99999 for " + RegistBloodProperty.Name + ".");
					SetProperty(ref registBlood, ref value, RegistBloodProperty);
				}
			}

			/// <summary>Curse resistance attack power [point]</summary>
			/// <remarks>
			/// Japanese short name: "呪耐性攻撃力[point]", Google translated: "Curse resistance attack power [point]".
			/// Japanese description: "ヒットした時に、対象の【呪耐性値】に加算する数値", Google translated: "When you hit, the numerical value to be added to the target resistance value [ curse ]".
			/// </remarks>
			[ParameterTableRowAttribute("registCurse", index: 54, minimum: -99999, maximum: 99999, step: 1, order: 6610, unknown2: 1)]
			[DisplayName("Curse resistance attack power [point]")]
			[Description("When you hit, the numerical value to be added to the target resistance value [ curse ]")]
			[DefaultValue((Int32)0)]
			public Int32 RegistCurse {
				get { return registCurse; }
				set {
					if ((double)value < -99999 || (double)value > 99999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -99999 to 99999 for " + RegistCurseProperty.Name + ".");
					SetProperty(ref registCurse, ref value, RegistCurseProperty);
				}
			}

			/// <summary>Falling damage magnification</summary>
			/// <remarks>
			/// Japanese short name: "落下ダメージ倍率", Google translated: "Falling damage magnification".
			/// Japanese description: "落下時のダメージ計算に倍率をかける", Google translated: "I put a magnification to damage calculation at the time of falling".
			/// </remarks>
			[ParameterTableRowAttribute("fallDamageRate", index: 55, minimum: 0, maximum: 99, step: 0.001, order: 6700, unknown2: 1)]
			[DisplayName("Falling damage magnification")]
			[Description("I put a magnification to damage calculation at the time of falling")]
			[DefaultValue((Single)0)]
			public Single FallDamageRate {
				get { return fallDamageRate; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for " + FallDamageRateProperty.Name + ".");
					SetProperty(ref fallDamageRate, ref value, FallDamageRateProperty);
				}
			}

			/// <summary>Get Seoul magnification</summary>
			/// <remarks>
			/// Japanese short name: "取得ソウル倍率", Google translated: "Get Seoul magnification".
			/// Japanese description: "敵を倒した時の取得ソウル量が、指定倍数分上乗せされる", Google translated: "Seoul acquisition amount at the time of defeat the enemy is designated multiple minute plus".
			/// </remarks>
			[ParameterTableRowAttribute("soulRate", index: 56, minimum: 0, maximum: 99, step: 0.001, order: 6800, unknown2: 1)]
			[DisplayName("Get Seoul magnification")]
			[Description("Seoul acquisition amount at the time of defeat the enemy is designated multiple minute plus")]
			[DefaultValue((Single)0)]
			public Single SoulRate {
				get { return soulRate; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for " + SoulRateProperty.Name + ".");
					SetProperty(ref soulRate, ref value, SoulRateProperty);
				}
			}

			/// <summary>Equipment weight change magnification</summary>
			/// <remarks>
			/// Japanese short name: "装備重量変化倍率", Google translated: "Equipment weight change magnification".
			/// Japanese description: "最大装備重量に、設定された倍率をかける", Google translated: "The maximum weight equipment , I put the magnification set".
			/// </remarks>
			[ParameterTableRowAttribute("equipWeightChangeRate", index: 57, minimum: 0, maximum: 99, step: 0.001, order: 6900, unknown2: 1)]
			[DisplayName("Equipment weight change magnification")]
			[Description("The maximum weight equipment , I put the magnification set")]
			[DefaultValue((Single)0)]
			public Single EquipWeightChangeRate {
				get { return equipWeightChangeRate; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for " + EquipWeightChangeRateProperty.Name + ".");
					SetProperty(ref equipWeightChangeRate, ref value, EquipWeightChangeRateProperty);
				}
			}

			/// <summary>Possession weight change magnification</summary>
			/// <remarks>
			/// Japanese short name: "所持重量変化倍率", Google translated: "Possession weight change magnification".
			/// Japanese description: "最大所持重量に、設定された倍率をかける", Google translated: "Maximum weight in possession , I bet the magnification set".
			/// </remarks>
			[ParameterTableRowAttribute("allItemWeightChangeRate", index: 58, minimum: 0, maximum: 99, step: 0.001, order: 7000, unknown2: 1)]
			[DisplayName("Possession weight change magnification")]
			[Description("Maximum weight in possession , I bet the magnification set")]
			[DefaultValue((Single)0)]
			public Single AllItemWeightChangeRate {
				get { return allItemWeightChangeRate; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for " + AllItemWeightChangeRateProperty.Name + ".");
					SetProperty(ref allItemWeightChangeRate, ref value, AllItemWeightChangeRateProperty);
				}
			}

			/// <summary>Seoul addition</summary>
			/// <remarks>
			/// Japanese short name: "ソウル加算", Google translated: "Seoul addition".
			/// Japanese description: "所持ソウルに、設定値を加算する", Google translated: "Possession in Seoul , I added a set value".
			/// </remarks>
			[ParameterTableRowAttribute("soul", index: 59, minimum: -1E+08, maximum: 1E+08, step: 1, order: 7100, unknown2: 1)]
			[DisplayName("Seoul addition")]
			[Description("Possession in Seoul , I added a set value")]
			[DefaultValue((Int32)0)]
			public Int32 Soul {
				get { return soul; }
				set {
					if ((double)value < -1E+08 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1E+08 to 1E+08 for " + SoulProperty.Name + ".");
					SetProperty(ref soul, ref value, SoulProperty);
				}
			}

			/// <summary>Anime ID offset</summary>
			/// <remarks>
			/// Japanese short name: "アニメIDオフセット", Google translated: "Anime ID offset".
			/// Japanese description: "アニメIDオフセット", Google translated: "Anime ID offset".
			/// </remarks>
			[ParameterTableRowAttribute("animIdOffset", index: 60, minimum: -1E+08, maximum: 1E+08, step: 1, order: 11150, unknown2: 1)]
			[DisplayName("Anime ID offset")]
			[Description("Anime ID offset")]
			[DefaultValue((Int32)0)]
			public Int32 AnimIdOffset {
				get { return animIdOffset; }
				set {
					if ((double)value < -1E+08 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1E+08 to 1E+08 for " + AnimIdOffsetProperty.Name + ".");
					SetProperty(ref animIdOffset, ref value, AnimIdOffsetProperty);
				}
			}

			/// <summary>Seoul possession rate</summary>
			/// <remarks>
			/// Japanese short name: "所持ソウル率", Google translated: "Seoul possession rate".
			/// Japanese description: "敵周回効果用。設定されているキャラから外にソウルが出て行く時に適用されます。", Google translated: "Enemy around for effect . It applies when the soul is going out of the character that has been set .".
			/// </remarks>
			[ParameterTableRowAttribute("haveSoulRate", index: 61, minimum: 0, maximum: 99, step: 0.01, order: 7200, unknown2: 1)]
			[DisplayName("Seoul possession rate")]
			[Description("Enemy around for effect . It applies when the soul is going out of the character that has been set .")]
			[DefaultValue((Single)1)]
			public Single HaveSoulRate {
				get { return haveSoulRate; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for " + HaveSoulRateProperty.Name + ".");
					SetProperty(ref haveSoulRate, ref value, HaveSoulRateProperty);
				}
			}

			/// <summary>Target priority adding minute</summary>
			/// <remarks>
			/// Japanese short name: "ターゲット優先度加算分", Google translated: "Target priority adding minute".
			/// Japanese description: "マルチプレイ時、敵から優先的にターゲットとして狙われるようになる。プライオリティの加算。０がデフォルト。プラス値でよく狙われるようになる。マイナスは、－１まで。", Google translated: "Multiplayer at the time , come to be targeted as a target on a priority basis from the enemy . Addition of priority . 0 is the default . I would be targeted as well with positive value . Negative , to -1 .".
			/// </remarks>
			[ParameterTableRowAttribute("targetPriority", index: 62, minimum: -1, maximum: 10, step: 0.01, order: 7300, unknown2: 1)]
			[DisplayName("Target priority adding minute")]
			[Description("Multiplayer at the time , come to be targeted as a target on a priority basis from the enemy . Addition of priority . 0 is the default . I would be targeted as well with positive value . Negative , to -1 .")]
			[DefaultValue((Single)0)]
			public Single TargetPriority {
				get { return targetPriority; }
				set {
					if ((double)value < -1 || (double)value > 10)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 10 for " + TargetPriorityProperty.Name + ".");
					SetProperty(ref targetPriority, ref value, TargetPriorityProperty);
				}
			}

			/// <summary>Visual search operation cut</summary>
			/// <remarks>
			/// Japanese short name: "視覚索敵カット", Google translated: "Visual search operation cut".
			/// Japanese description: "AIの視覚索敵対象から外れやすくなる。０がデフォルト。", Google translated: "Can be easily detached from the visual search operation target of AI. 0 is the default .".
			/// </remarks>
			[ParameterTableRowAttribute("sightSearchEnemyCut", index: 63, minimum: 0, maximum: 100, step: 1, order: 7400, unknown2: 1)]
			[DisplayName("Visual search operation cut")]
			[Description("Can be easily detached from the visual search operation target of AI. 0 is the default .")]
			[DefaultValue((Int32)0)]
			public Int32 SightSearchEnemyCut {
				get { return sightSearchEnemyCut; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for " + SightSearchEnemyCutProperty.Name + ".");
					SetProperty(ref sightSearchEnemyCut, ref value, SightSearchEnemyCutProperty);
				}
			}

			/// <summary>Hearing searching for the enemy cut</summary>
			/// <remarks>
			/// Japanese short name: "聴覚索敵カット", Google translated: "Hearing searching for the enemy cut".
			/// Japanese description: "AIの聴覚索敵対象から外れやすくなる。０がデフォルト。", Google translated: "Can be easily detached from the subject of the hearing searching for the enemy AI. 0 is the default .".
			/// </remarks>
			[ParameterTableRowAttribute("hearingSearchEnemyCut", index: 64, minimum: 0, maximum: 100, step: 1, order: 7500, unknown2: 1)]
			[DisplayName("Hearing searching for the enemy cut")]
			[Description("Can be easily detached from the subject of the hearing searching for the enemy AI. 0 is the default .")]
			[DefaultValue((Int32)0)]
			public Int32 HearingSearchEnemyCut {
				get { return hearingSearchEnemyCut; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for " + HearingSearchEnemyCutProperty.Name + ".");
					SetProperty(ref hearingSearchEnemyCut, ref value, HearingSearchEnemyCutProperty);
				}
			}

			/// <summary>Gravity rate</summary>
			/// <remarks>
			/// Japanese short name: "グラビティ率", Google translated: "Gravity rate".
			/// Japanese description: "グラビティ率", Google translated: "Gravity rate".
			/// </remarks>
			[ParameterTableRowAttribute("grabityRate", index: 65, minimum: 0, maximum: 1, step: 0.001, order: 8700, unknown2: 1)]
			[DisplayName("Gravity rate")]
			[Description("Gravity rate")]
			[DefaultValue((Single)1)]
			public Single GrabityRate {
				get { return grabityRate; }
				set {
					if ((double)value < 0 || (double)value > 1)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1 for " + GrabityRateProperty.Name + ".");
					SetProperty(ref grabityRate, ref value, GrabityRateProperty);
				}
			}

			/// <summary>Poison -resistant fold change</summary>
			/// <remarks>
			/// Japanese short name: "毒耐性変化倍率", Google translated: "Poison -resistant fold change".
			/// Japanese description: "毒耐性値に設定された倍率をかける", Google translated: "I bet rate set for poison resistance value".
			/// </remarks>
			[ParameterTableRowAttribute("registPoizonChangeRate", index: 66, minimum: -1, maximum: 99, step: 0.001, order: 8800, unknown2: 1)]
			[DisplayName("Poison -resistant fold change")]
			[Description("I bet rate set for poison resistance value")]
			[DefaultValue((Single)0)]
			public Single RegistPoizonChangeRate {
				get { return registPoizonChangeRate; }
				set {
					if ((double)value < -1 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 99 for " + RegistPoizonChangeRateProperty.Name + ".");
					SetProperty(ref registPoizonChangeRate, ref value, RegistPoizonChangeRateProperty);
				}
			}

			/// <summary>Plague -resistant fold change</summary>
			/// <remarks>
			/// Japanese short name: "疫病耐性変化倍率", Google translated: "Plague -resistant fold change".
			/// Japanese description: "疫病耐性値に設定された倍率をかける", Google translated: "I bet rate set for plague resistance value".
			/// </remarks>
			[ParameterTableRowAttribute("registIllnessChangeRate", index: 67, minimum: -1, maximum: 99, step: 0.001, order: 8900, unknown2: 1)]
			[DisplayName("Plague -resistant fold change")]
			[Description("I bet rate set for plague resistance value")]
			[DefaultValue((Single)0)]
			public Single RegistIllnessChangeRate {
				get { return registIllnessChangeRate; }
				set {
					if ((double)value < -1 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 99 for " + RegistIllnessChangeRateProperty.Name + ".");
					SetProperty(ref registIllnessChangeRate, ref value, RegistIllnessChangeRateProperty);
				}
			}

			/// <summary>Bleeding resistance fold change</summary>
			/// <remarks>
			/// Japanese short name: "出血耐性変化倍率", Google translated: "Bleeding resistance fold change".
			/// Japanese description: "出血耐性値に設定された倍率をかける", Google translated: "I bet rate set for bleeding resistance value".
			/// </remarks>
			[ParameterTableRowAttribute("registBloodChangeRate", index: 68, minimum: -1, maximum: 99, step: 0.001, order: 9000, unknown2: 1)]
			[DisplayName("Bleeding resistance fold change")]
			[Description("I bet rate set for bleeding resistance value")]
			[DefaultValue((Single)0)]
			public Single RegistBloodChangeRate {
				get { return registBloodChangeRate; }
				set {
					if ((double)value < -1 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 99 for " + RegistBloodChangeRateProperty.Name + ".");
					SetProperty(ref registBloodChangeRate, ref value, RegistBloodChangeRateProperty);
				}
			}

			/// <summary>Curse -resistant fold change</summary>
			/// <remarks>
			/// Japanese short name: "呪耐性変化倍率", Google translated: "Curse -resistant fold change".
			/// Japanese description: "呪耐性値に設定された倍率をかける", Google translated: "I bet rate set for spell resistance value".
			/// </remarks>
			[ParameterTableRowAttribute("registCurseChangeRate", index: 69, minimum: -1, maximum: 99, step: 0.001, order: 9010, unknown2: 1)]
			[DisplayName("Curse -resistant fold change")]
			[Description("I bet rate set for spell resistance value")]
			[DefaultValue((Single)0)]
			public Single RegistCurseChangeRate {
				get { return registCurseChangeRate; }
				set {
					if ((double)value < -1 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 99 for " + RegistCurseChangeRateProperty.Name + ".");
					SetProperty(ref registCurseChangeRate, ref value, RegistCurseChangeRateProperty);
				}
			}

			/// <summary>Seoul Steel factor</summary>
			/// <remarks>
			/// Japanese short name: "ソウルスティール係数", Google translated: "Seoul Steel factor".
			/// Japanese description: "NPCがソウルスティールで奪われるHPに対する防御力", Google translated: "Defense against HP deprived in Seoul Steele NPC".
			/// </remarks>
			[ParameterTableRowAttribute("soulStealRate", index: 70, minimum: 0, maximum: 99, step: 0.001, order: 9100, unknown2: 1)]
			[DisplayName("Seoul Steel factor")]
			[Description("Defense against HP deprived in Seoul Steele NPC")]
			[DefaultValue((Single)0)]
			public Single SoulStealRate {
				get { return soulStealRate; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for " + SoulStealRateProperty.Name + ".");
					SetProperty(ref soulStealRate, ref value, SoulStealRateProperty);
				}
			}

			/// <summary>Defense: life factor</summary>
			/// <remarks>
			/// Japanese short name: "防御：寿命係数", Google translated: "Defense: life factor".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("lifeReductionRate", index: 71, minimum: 0, maximum: 99, step: 0.001, order: 10000, unknown2: 1)]
			[DisplayName("Defense: life factor")]
			[Description("")]
			[DefaultValue((Single)0)]
			public Single LifeReductionRate {
				get { return lifeReductionRate; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for " + LifeReductionRateProperty.Name + ".");
					SetProperty(ref lifeReductionRate, ref value, LifeReductionRateProperty);
				}
			}

			/// <summary>HP recovery amount coefficient</summary>
			/// <remarks>
			/// Japanese short name: "HP回復量係数", Google translated: "HP recovery amount coefficient".
			/// Japanese description: "HPが減るときは、効かない。", Google translated: "When HP is reduced, it does not work .".
			/// </remarks>
			[ParameterTableRowAttribute("hpRecoverRate", index: 72, minimum: 0, maximum: 99, step: 0.001, order: 10100, unknown2: 1)]
			[DisplayName("HP recovery amount coefficient")]
			[Description("When HP is reduced, it does not work .")]
			[DefaultValue((Single)0)]
			public Single HpRecoverRate {
				get { return hpRecoverRate; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for " + HpRecoverRateProperty.Name + ".");
					SetProperty(ref hpRecoverRate, ref value, HpRecoverRateProperty);
				}
			}

			/// <summary>Special effects to replace</summary>
			/// <remarks>
			/// Japanese short name: "差し替える特殊効果", Google translated: "Special effects to replace".
			/// Japanese description: "寿命が尽きた時に追加される特殊効果ID(-1は無視)", Google translated: "(-1 Is ignored) special effects ID to be added when the end-of-life".
			/// </remarks>
			[ParameterTableRowAttribute("replaceSpEffectId", index: 73, minimum: -1, maximum: 1E+09, step: 1, order: 11100, unknown2: 1)]
			[DisplayName("Special effects to replace")]
			[Description("(-1 Is ignored) special effects ID to be added when the end-of-life")]
			[DefaultValue((Int32)(-1))]
			public Int32 ReplaceSpEffectId {
				get { return replaceSpEffectId; }
				set {
					if ((double)value < -1 || (double)value > 1E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+09 for " + ReplaceSpEffectIdProperty.Name + ".");
					SetProperty(ref replaceSpEffectId, ref value, ReplaceSpEffectIdProperty);
				}
			}

			/// <summary>Cycle generating special effects</summary>
			/// <remarks>
			/// Japanese short name: "周期発生特殊効果", Google translated: "Cycle generating special effects".
			/// Japanese description: "発動周期毎に発生する特殊効果ID(-1は無視)", Google translated: "(-1 Is ignored) ID special effects that occur to trigger cycle".
			/// </remarks>
			[ParameterTableRowAttribute("cycleOccurrenceSpEffectId", index: 74, minimum: -1, maximum: 1E+09, step: 1, order: 11101, unknown2: 1)]
			[DisplayName("Cycle generating special effects")]
			[Description("(-1 Is ignored) ID special effects that occur to trigger cycle")]
			[DefaultValue((Int32)(-1))]
			public Int32 CycleOccurrenceSpEffectId {
				get { return cycleOccurrenceSpEffectId; }
				set {
					if ((double)value < -1 || (double)value > 1E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+09 for " + CycleOccurrenceSpEffectIdProperty.Name + ".");
					SetProperty(ref cycleOccurrenceSpEffectId, ref value, CycleOccurrenceSpEffectIdProperty);
				}
			}

			/// <summary>Attack generation special effects</summary>
			/// <remarks>
			/// Japanese short name: "攻撃発生特殊効果", Google translated: "Attack generation special effects".
			/// Japanese description: "攻撃Hit時に発生する特殊効果ID(-1は無視)", Google translated: "(-1 Is ignored) ID special effects that occur during attack Hit".
			/// </remarks>
			[ParameterTableRowAttribute("atkOccurrenceSpEffectId", index: 75, minimum: -1, maximum: 1E+09, step: 1, order: 11102, unknown2: 1)]
			[DisplayName("Attack generation special effects")]
			[Description("(-1 Is ignored) ID special effects that occur during attack Hit")]
			[DefaultValue((Int32)(-1))]
			public Int32 AtkOccurrenceSpEffectId {
				get { return atkOccurrenceSpEffectId; }
				set {
					if ((double)value < -1 || (double)value > 1E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+09 for " + AtkOccurrenceSpEffectIdProperty.Name + ".");
					SetProperty(ref atkOccurrenceSpEffectId, ref value, AtkOccurrenceSpEffectIdProperty);
				}
			}

			/// <summary>Defense magnification up soon guard when</summary>
			/// <remarks>
			/// Japanese short name: "ガード時はじき防御力アップ倍率", Google translated: "Defense magnification up soon guard when".
			/// Japanese description: "ガード時のはじき防御力補正値", Google translated: "Value correction defense repelling force of the guard at the time".
			/// </remarks>
			[ParameterTableRowAttribute("guardDefFlickPowerRate", index: 76, minimum: -99, maximum: 99, step: 0.01, order: 2753, unknown2: 1)]
			[DisplayName("Defense magnification up soon guard when")]
			[Description("Value correction defense repelling force of the guard at the time")]
			[DefaultValue((Single)1)]
			public Single GuardDefFlickPowerRate {
				get { return guardDefFlickPowerRate; }
				set {
					if ((double)value < -99 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -99 to 99 for " + GuardDefFlickPowerRateProperty.Name + ".");
					SetProperty(ref guardDefFlickPowerRate, ref value, GuardDefFlickPowerRateProperty);
				}
			}

			/// <summary>Guard when stamina cut magnification</summary>
			/// <remarks>
			/// Japanese short name: "ガード時スタミナカット倍率", Google translated: "Guard when stamina cut magnification".
			/// Japanese description: "ガード時のスタミナカット率補正値", Google translated: "Stamina cut rate correction value of the guard at the time".
			/// </remarks>
			[ParameterTableRowAttribute("guardStaminaCutRate", index: 77, minimum: -99, maximum: 99, step: 0.01, order: 6210, unknown2: 1)]
			[DisplayName("Guard when stamina cut magnification")]
			[Description("Stamina cut rate correction value of the guard at the time")]
			[DefaultValue((Single)1)]
			public Single GuardStaminaCutRate {
				get { return guardStaminaCutRate; }
				set {
					if ((double)value < -99 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -99 to 99 for " + GuardStaminaCutRateProperty.Name + ".");
					SetProperty(ref guardStaminaCutRate, ref value, GuardStaminaCutRateProperty);
				}
			}

			/// <summary>Line of sight passing : casting time [ms]</summary>
			/// <remarks>
			/// Japanese short name: "視線通過：発動時間[ms]", Google translated: "Line of sight passing : casting time [ms]".
			/// Japanese description: "視線通過：発動時間[ms]（邪眼用）", Google translated: "Line of sight passing : Casting Time [ms] ( evil eye for )".
			/// </remarks>
			[ParameterTableRowAttribute("rayCastPassedTime", index: 78, minimum: -1, maximum: 30000, step: 1, order: 10800, unknown2: 1)]
			[DisplayName("Line of sight passing : casting time [ms]")]
			[Description("Line of sight passing : Casting Time [ms] ( evil eye for )")]
			[DefaultValue((Int16)(-1))]
			public Int16 RayCastPassedTime {
				get { return rayCastPassedTime; }
				set {
					if ((double)value < -1 || (double)value > 30000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 30000 for " + RayCastPassedTimeProperty.Name + ".");
					SetProperty(ref rayCastPassedTime, ref value, RayCastPassedTimeProperty);
				}
			}

			/// <summary>SA value [point]</summary>
			/// <remarks>
			/// Japanese short name: "SA値[point]", Google translated: "SA value [point]".
			/// Japanese description: "スーパーアーマー値に加算する値", Google translated: "Value to be added to the super armor".
			/// </remarks>
			[ParameterTableRowAttribute("changeSuperArmorPoint", index: 79, minimum: -9999, maximum: 9999, step: 1, order: 5810, unknown2: 1)]
			[DisplayName("SA value [point]")]
			[Description("Value to be added to the super armor")]
			[DefaultValue((Int16)0)]
			public Int16 ChangeSuperArmorPoint {
				get { return changeSuperArmorPoint; }
				set {
					if ((double)value < -9999 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -9999 to 9999 for " + ChangeSuperArmorPointProperty.Name + ".");
					SetProperty(ref changeSuperArmorPoint, ref value, ChangeSuperArmorPointProperty);
				}
			}

			/// <summary>Bow flying distance correction [ %]</summary>
			/// <remarks>
			/// Japanese short name: "弓飛距離補正[％]", Google translated: "Bow flying distance correction [ %]".
			/// Japanese description: "武器の飛距離補正に加算される補正値", Google translated: "Correction value to be added to the distance correction of weapons".
			/// </remarks>
			[ParameterTableRowAttribute("bowDistRate", index: 80, minimum: -100, maximum: 999, step: 1, order: 6220, unknown2: 1)]
			[DisplayName("Bow flying distance correction [ %]")]
			[Description("Correction value to be added to the distance correction of weapons")]
			[DefaultValue((Int16)0)]
			public Int16 BowDistRate {
				get { return bowDistRate; }
				set {
					if ((double)value < -100 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -100 to 999 for " + BowDistRateProperty.Name + ".");
					SetProperty(ref bowDistRate, ref value, BowDistRateProperty);
				}
			}

			/// <summary>Special effects category</summary>
			/// <remarks>
			/// Japanese short name: "特殊効果カテゴリ", Google translated: "Special effects category".
			/// Japanese description: "特殊効果の上書きなどの挙動を決めるカテゴリ", Google translated: "A category that determine the behavior , such as overwriting of special effects".
			/// </remarks>
			[ParameterTableRowAttribute("spCategory", index: 81, minimum: 0, maximum: 60000, step: 1, order: 210, unknown2: 1)]
			[DisplayName("Special effects category")]
			[Description("A category that determine the behavior , such as overwriting of special effects")]
			[DefaultValue((SpecialEffectSpCategory)0)]
			public SpecialEffectSpCategory SpCategory {
				get { return spCategory; }
				set { SetProperty(ref spCategory, ref value, SpCategoryProperty); }
			}

			/// <summary>Category priority</summary>
			/// <remarks>
			/// Japanese short name: "カテゴリ内優先度", Google translated: "Category priority".
			/// Japanese description: "同一カテゴリ内での優先度（低い方が優先）", Google translated: "( Lower priority ) priority in the same category".
			/// </remarks>
			[ParameterTableRowAttribute("categoryPriority", index: 82, minimum: 0, maximum: 255, step: 1, order: 220, unknown2: 1)]
			[DisplayName("Category priority")]
			[Description("( Lower priority ) priority in the same category")]
			[DefaultValue((Byte)0)]
			public Byte CategoryPriority {
				get { return categoryPriority; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for " + CategoryPriorityProperty.Name + ".");
					SetProperty(ref categoryPriority, ref value, CategoryPriorityProperty);
				}
			}

			/// <summary>Save category</summary>
			/// <remarks>
			/// Japanese short name: "保存カテゴリ", Google translated: "Save category".
			/// Japanese description: "特殊効果を保存するカテゴリ", Google translated: "Category in which to save the special effects".
			/// </remarks>
			[ParameterTableRowAttribute("saveCategory", index: 83, minimum: -1, maximum: 127, step: 1, order: 230, unknown2: 1)]
			[DisplayName("Save category")]
			[Description("Category in which to save the special effects")]
			[DefaultValue((SpecialEffectSaveCategory)(-1))]
			public SpecialEffectSaveCategory SaveCategory {
				get { return saveCategory; }
				set { SetProperty(ref saveCategory, ref value, SaveCategoryProperty); }
			}

			/// <summary>Magic registration frame change magic slot</summary>
			/// <remarks>
			/// Japanese short name: "魔法登録枠変化　魔法スロット", Google translated: "Magic registration frame change magic slot".
			/// Japanese description: "魔法登録枠を指定数増やすことが出来る", Google translated: "It is possible to increase the specified number of magic registration frame".
			/// </remarks>
			[ParameterTableRowAttribute("changeMagicSlot", index: 84, minimum: 0, maximum: 3, step: 1, order: 8300, unknown2: 1)]
			[DisplayName("Magic registration frame change magic slot")]
			[Description("It is possible to increase the specified number of magic registration frame")]
			[DefaultValue((Byte)0)]
			public Byte ChangeMagicSlot {
				get { return changeMagicSlot; }
				set {
					if ((double)value < 0 || (double)value > 3)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 3 for " + ChangeMagicSlotProperty.Name + ".");
					SetProperty(ref changeMagicSlot, ref value, ChangeMagicSlotProperty);
				}
			}

			/// <summary>Miracle miracle change registration frame slot</summary>
			/// <remarks>
			/// Japanese short name: "奇跡登録枠変化　奇跡スロット", Google translated: "Miracle miracle change registration frame slot".
			/// Japanese description: "軌跡登録枠を指定数増やすことが出来る", Google translated: "It is possible to increase the number of specified trajectory registration frame".
			/// </remarks>
			[ParameterTableRowAttribute("changeMiracleSlot", index: 85, minimum: 0, maximum: 3, step: 1, order: 8400, unknown2: 1)]
			[DisplayName("Miracle miracle change registration frame slot")]
			[Description("It is possible to increase the number of specified trajectory registration frame")]
			[DefaultValue((Byte)0)]
			public Byte ChangeMiracleSlot {
				get { return changeMiracleSlot; }
				set {
					if ((double)value < 0 || (double)value > 3)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 3 for " + ChangeMiracleSlotProperty.Name + ".");
					SetProperty(ref changeMiracleSlot, ref value, ChangeMiracleSlotProperty);
				}
			}

			/// <summary>Human nature damage value</summary>
			/// <remarks>
			/// Japanese short name: "人間性ダメージ値", Google translated: "Human nature damage value".
			/// Japanese description: "人間性値に与えるダメージ値", Google translated: "Damage value given to human nature value".
			/// </remarks>
			[ParameterTableRowAttribute("heroPointDamage", index: 86, minimum: -99, maximum: 99, step: 1, order: 11200, unknown2: 1)]
			[DisplayName("Human nature damage value")]
			[Description("Damage value given to human nature value")]
			[DefaultValue((SByte)0)]
			public SByte HeroPointDamage {
				get { return heroPointDamage; }
				set {
					if ((double)value < -99 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -99 to 99 for " + HeroPointDamageProperty.Name + ".");
					SetProperty(ref heroPointDamage, ref value, HeroPointDamageProperty);
				}
			}

			/// <summary>Repelling Defense _ overwrite</summary>
			/// <remarks>
			/// Japanese short name: "はじき防御力_上書き", Google translated: "Repelling Defense _ overwrite".
			/// Japanese description: "はじき防御力を上書きする値を設定", Google translated: "Set the value to override the flick Defense".
			/// </remarks>
			[ParameterTableRowAttribute("defFlickPower", index: 87, minimum: 0, maximum: 255, step: 1, order: 2751, unknown2: 1)]
			[DisplayName("Repelling Defense _ overwrite")]
			[Description("Set the value to override the flick Defense")]
			[DefaultValue((Byte)0)]
			public Byte DefFlickPower {
				get { return defFlickPower; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for " + DefFlickPowerProperty.Name + ".");
					SetProperty(ref defFlickPower, ref value, DefFlickPowerProperty);
				}
			}

			/// <summary>Repelling when damage attenuation rate [ %] _ overwrite</summary>
			/// <remarks>
			/// Japanese short name: "はじき時ダメージ減衰率[%]_上書き", Google translated: "Repelling when damage attenuation rate [ %] _ overwrite".
			/// Japanese description: "はじき時のダメージ減衰率を上書きする値を設定", Google translated: "Set the value to override the damage attenuation rate at the time of repelling".
			/// </remarks>
			[ParameterTableRowAttribute("flickDamageCutRate", index: 88, minimum: 0, maximum: 100, step: 1, order: 2752, unknown2: 1)]
			[DisplayName("Repelling when damage attenuation rate [ %] _ overwrite")]
			[Description("Set the value to override the damage attenuation rate at the time of repelling")]
			[DefaultValue((Byte)0)]
			public Byte FlickDamageCutRate {
				get { return flickDamageCutRate; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for " + FlickDamageCutRateProperty.Name + ".");
					SetProperty(ref flickDamageCutRate, ref value, FlickDamageCutRateProperty);
				}
			}

			/// <summary>Bleeding damage magnification correction</summary>
			/// <remarks>
			/// Japanese short name: "出血ダメージ補正倍率", Google translated: "Bleeding damage magnification correction".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("bloodDamageRate", index: 89, minimum: 0, maximum: 255, step: 1, order: 9020, unknown2: 1)]
			[DisplayName("Bleeding damage magnification correction")]
			[Description("")]
			[DefaultValue((Byte)100)]
			public Byte BloodDamageRate {
				get { return bloodDamageRate; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for " + BloodDamageRateProperty.Name + ".");
					SetProperty(ref bloodDamageRate, ref value, BloodDamageRateProperty);
				}
			}

			/// <summary>No damage DL_ ( 0)</summary>
			/// <remarks>
			/// Japanese short name: "DL_ダメージなし（0）", Google translated: "No damage DL_ ( 0)".
			/// Japanese description: "ダメージLv0を差し替えるタイプを指定", Google translated: "Specifies the type supersede damage Lv0".
			/// </remarks>
			[ParameterTableRowAttribute("dmgLv_None", index: 90, minimum: 0, maximum: 127, step: 1, order: 2711, unknown2: 1)]
			[DisplayName("No damage DL_ ( 0)")]
			[Description("Specifies the type supersede damage Lv0")]
			[DefaultValue((ATKPARAM_REP_DMGTYPE)0)]
			public ATKPARAM_REP_DMGTYPE DmgLv_None {
				get { return dmgLv_None; }
				set { SetProperty(ref dmgLv_None, ref value, DmgLv_NoneProperty); }
			}

			/// <summary>DL_ Small ( 1)</summary>
			/// <remarks>
			/// Japanese short name: "DL_小（1）", Google translated: "DL_ Small ( 1)".
			/// Japanese description: "ダメージLv1を差し替えるタイプを指定", Google translated: "Specifies the type supersede damage Lv1".
			/// </remarks>
			[ParameterTableRowAttribute("dmgLv_S", index: 91, minimum: 0, maximum: 127, step: 1, order: 2713, unknown2: 1)]
			[DisplayName("DL_ Small ( 1)")]
			[Description("Specifies the type supersede damage Lv1")]
			[DefaultValue((ATKPARAM_REP_DMGTYPE)0)]
			public ATKPARAM_REP_DMGTYPE DmgLv_S {
				get { return dmgLv_S; }
				set { SetProperty(ref dmgLv_S, ref value, DmgLv_SProperty); }
			}

			/// <summary>DL_ in ( 2 )</summary>
			/// <remarks>
			/// Japanese short name: "DL_中（2）", Google translated: "DL_ in ( 2 )".
			/// Japanese description: "ダメージLv2を差し替えるタイプを指定", Google translated: "Specifies the type supersede damage Lv2".
			/// </remarks>
			[ParameterTableRowAttribute("dmgLv_M", index: 92, minimum: 0, maximum: 127, step: 1, order: 2714, unknown2: 1)]
			[DisplayName("DL_ in ( 2 )")]
			[Description("Specifies the type supersede damage Lv2")]
			[DefaultValue((ATKPARAM_REP_DMGTYPE)0)]
			public ATKPARAM_REP_DMGTYPE DmgLv_M {
				get { return dmgLv_M; }
				set { SetProperty(ref dmgLv_M, ref value, DmgLv_MProperty); }
			}

			/// <summary>DL_ Large ( 3)</summary>
			/// <remarks>
			/// Japanese short name: "DL_大（3）", Google translated: "DL_ Large ( 3)".
			/// Japanese description: "ダメージLv3を差し替えるタイプを指定", Google translated: "Specifies the type supersede damage Lv3".
			/// </remarks>
			[ParameterTableRowAttribute("dmgLv_L", index: 93, minimum: 0, maximum: 127, step: 1, order: 2715, unknown2: 1)]
			[DisplayName("DL_ Large ( 3)")]
			[Description("Specifies the type supersede damage Lv3")]
			[DefaultValue((ATKPARAM_REP_DMGTYPE)0)]
			public ATKPARAM_REP_DMGTYPE DmgLv_L {
				get { return dmgLv_L; }
				set { SetProperty(ref dmgLv_L, ref value, DmgLv_LProperty); }
			}

			/// <summary>The Futtobi DL_ (4)</summary>
			/// <remarks>
			/// Japanese short name: "DL_吹っ飛び（4）", Google translated: "The Futtobi DL_ (4)".
			/// Japanese description: "ダメージLv4を差し替えるタイプを指定", Google translated: "Specifies the type supersede damage Lv4".
			/// </remarks>
			[ParameterTableRowAttribute("dmgLv_BlowM", index: 94, minimum: 0, maximum: 127, step: 1, order: 2717, unknown2: 1)]
			[DisplayName("The Futtobi DL_ (4)")]
			[Description("Specifies the type supersede damage Lv4")]
			[DefaultValue((ATKPARAM_REP_DMGTYPE)0)]
			public ATKPARAM_REP_DMGTYPE DmgLv_BlowM {
				get { return dmgLv_BlowM; }
				set { SetProperty(ref dmgLv_BlowM, ref value, DmgLv_BlowMProperty); }
			}

			/// <summary>DL_ push ( 5)</summary>
			/// <remarks>
			/// Japanese short name: "DL_プッシュ（5）", Google translated: "DL_ push ( 5)".
			/// Japanese description: "ダメージLv5を差し替えるタイプを指定", Google translated: "Specifies the type supersede damage Lv5".
			/// </remarks>
			[ParameterTableRowAttribute("dmgLv_Push", index: 95, minimum: 0, maximum: 127, step: 1, order: 2720, unknown2: 1)]
			[DisplayName("DL_ push ( 5)")]
			[Description("Specifies the type supersede damage Lv5")]
			[DefaultValue((ATKPARAM_REP_DMGTYPE)0)]
			public ATKPARAM_REP_DMGTYPE DmgLv_Push {
				get { return dmgLv_Push; }
				set { SetProperty(ref dmgLv_Push, ref value, DmgLv_PushProperty); }
			}

			/// <summary>Slams DL_ (6)</summary>
			/// <remarks>
			/// Japanese short name: "DL_叩きつけ（6）", Google translated: "Slams DL_ (6)".
			/// Japanese description: "ダメージLv6を差し替えるタイプを指定", Google translated: "Specifies the type supersede damage Lv6".
			/// </remarks>
			[ParameterTableRowAttribute("dmgLv_Strike", index: 96, minimum: 0, maximum: 127, step: 1, order: 2718, unknown2: 1)]
			[DisplayName("Slams DL_ (6)")]
			[Description("Specifies the type supersede damage Lv6")]
			[DefaultValue((ATKPARAM_REP_DMGTYPE)0)]
			public ATKPARAM_REP_DMGTYPE DmgLv_Strike {
				get { return dmgLv_Strike; }
				set { SetProperty(ref dmgLv_Strike, ref value, DmgLv_StrikeProperty); }
			}

			/// <summary>The Futtobi DL_ small ( 7 )</summary>
			/// <remarks>
			/// Japanese short name: "DL_小吹っ飛び（7）", Google translated: "The Futtobi DL_ small ( 7 )".
			/// Japanese description: "ダメージLv7を差し替えるタイプを指定", Google translated: "Specifies the type supersede damage Lv7".
			/// </remarks>
			[ParameterTableRowAttribute("dmgLv_BlowS", index: 97, minimum: 0, maximum: 127, step: 1, order: 2716, unknown2: 1)]
			[DisplayName("The Futtobi DL_ small ( 7 )")]
			[Description("Specifies the type supersede damage Lv7")]
			[DefaultValue((ATKPARAM_REP_DMGTYPE)0)]
			public ATKPARAM_REP_DMGTYPE DmgLv_BlowS {
				get { return dmgLv_BlowS; }
				set { SetProperty(ref dmgLv_BlowS, ref value, DmgLv_BlowSProperty); }
			}

			/// <summary>DL_ minimum ( 8)</summary>
			/// <remarks>
			/// Japanese short name: "DL_極小（8）", Google translated: "DL_ minimum ( 8)".
			/// Japanese description: "ダメージLv8を差し替えるタイプを指定", Google translated: "Specifies the type supersede damage Lv8".
			/// </remarks>
			[ParameterTableRowAttribute("dmgLv_Min", index: 98, minimum: 0, maximum: 127, step: 1, order: 2712, unknown2: 1)]
			[DisplayName("DL_ minimum ( 8)")]
			[Description("Specifies the type supersede damage Lv8")]
			[DefaultValue((ATKPARAM_REP_DMGTYPE)0)]
			public ATKPARAM_REP_DMGTYPE DmgLv_Min {
				get { return dmgLv_Min; }
				set { SetProperty(ref dmgLv_Min, ref value, DmgLv_MinProperty); }
			}

			/// <summary>The launch DL_ (9)</summary>
			/// <remarks>
			/// Japanese short name: "DL_打ち上げ（9）", Google translated: "The launch DL_ (9)".
			/// Japanese description: "ダメージLv9を差し替えるタイプを指定", Google translated: "Specifies the type supersede damage Lv9".
			/// </remarks>
			[ParameterTableRowAttribute("dmgLv_Uppercut", index: 99, minimum: 0, maximum: 127, step: 1, order: 2719, unknown2: 1)]
			[DisplayName("The launch DL_ (9)")]
			[Description("Specifies the type supersede damage Lv9")]
			[DefaultValue((ATKPARAM_REP_DMGTYPE)0)]
			public ATKPARAM_REP_DMGTYPE DmgLv_Uppercut {
				get { return dmgLv_Uppercut; }
				set { SetProperty(ref dmgLv_Uppercut, ref value, DmgLv_UppercutProperty); }
			}

			/// <summary>The Futtobi DL_ extra large ( 10)</summary>
			/// <remarks>
			/// Japanese short name: "DL_特大吹っ飛び(10)", Google translated: "The Futtobi DL_ extra large ( 10)".
			/// Japanese description: "ダメージLv10を差し替えるタイプを指定", Google translated: "Specifies the type supersede damage Lv10".
			/// </remarks>
			[ParameterTableRowAttribute("dmgLv_BlowLL", index: 100, minimum: 0, maximum: 127, step: 1, order: 2720, unknown2: 1)]
			[DisplayName("The Futtobi DL_ extra large ( 10)")]
			[Description("Specifies the type supersede damage Lv10")]
			[DefaultValue((ATKPARAM_REP_DMGTYPE)0)]
			public ATKPARAM_REP_DMGTYPE DmgLv_BlowLL {
				get { return dmgLv_BlowLL; }
				set { SetProperty(ref dmgLv_BlowLL, ref value, DmgLv_BlowLLProperty); }
			}

			/// <summary>DL_ breath ( 11 )</summary>
			/// <remarks>
			/// Japanese short name: "DL_ブレス(11)", Google translated: "DL_ breath ( 11 )".
			/// Japanese description: "ダメージLv11を差し替えるタイプを指定", Google translated: "Specifies the type supersede damage Lv11".
			/// </remarks>
			[ParameterTableRowAttribute("dmgLv_Breath", index: 101, minimum: 0, maximum: 127, step: 1, order: 2721, unknown2: 1)]
			[DisplayName("DL_ breath ( 11 )")]
			[Description("Specifies the type supersede damage Lv11")]
			[DefaultValue((ATKPARAM_REP_DMGTYPE)0)]
			public ATKPARAM_REP_DMGTYPE DmgLv_Breath {
				get { return dmgLv_Breath; }
				set { SetProperty(ref dmgLv_Breath, ref value, DmgLv_BreathProperty); }
			}

			/// <summary>Physical attributes</summary>
			/// <remarks>
			/// Japanese short name: "物理属性", Google translated: "Physical attributes".
			/// Japanese description: "特殊効果に設定する物理属性", Google translated: "Physical attributes to be set for the special effects".
			/// </remarks>
			[ParameterTableRowAttribute("atkAttribute", index: 102, minimum: 0, maximum: 255, step: 1, order: 2740, unknown2: 1)]
			[DisplayName("Physical attributes")]
			[Description("Physical attributes to be set for the special effects")]
			[DefaultValue((AttackAttackAttributes)0)]
			public AttackAttackAttributes AtkAttribute {
				get { return atkAttribute; }
				set { SetProperty(ref atkAttribute, ref value, AtkAttributeProperty); }
			}

			/// <summary>Special attributes</summary>
			/// <remarks>
			/// Japanese short name: "特殊属性", Google translated: "Special attributes".
			/// Japanese description: "特殊効果に設定する特殊属性", Google translated: "Special attributes to be set for the special effects".
			/// </remarks>
			[ParameterTableRowAttribute("spAttribute", index: 103, minimum: 0, maximum: 255, step: 1, order: 2750, unknown2: 1)]
			[DisplayName("Special attributes")]
			[Description("Special attributes to be set for the special effects")]
			[DefaultValue((AttackParameterSpecialAttributes)0)]
			public AttackParameterSpecialAttributes SpAttribute {
				get { return spAttribute; }
				set { SetProperty(ref spAttribute, ref value, SpAttributeProperty); }
			}

			/// <summary>State change type</summary>
			/// <remarks>
			/// Japanese short name: "状態変化タイプ", Google translated: "State change type".
			/// Japanese description: "状態変化の判定フラグ", Google translated: "Determination flag of the state change".
			/// </remarks>
			[ParameterTableRowAttribute("stateInfo", index: 104, minimum: 0, maximum: 255, step: 1, order: 200, unknown2: 1)]
			[DisplayName("State change type")]
			[Description("Determination flag of the state change")]
			[DefaultValue((SpecialEffectType)0)]
			public SpecialEffectType StateInfo {
				get { return stateInfo; }
				set { SetProperty(ref stateInfo, ref value, StateInfoProperty); }
			}

			/// <summary>Versus weapon parameter change</summary>
			/// <remarks>
			/// Japanese short name: "対武器パラメータ変化", Google translated: "Versus weapon parameter change".
			/// Japanese description: "どの武器に対して効果を発揮するかを指定する。制限無しの場合は敵も含めた全ての攻撃・防御が対象", Google translated: "Specify whether to show an effect on any weapon . Attack - defense of all enemies , including the target in the case of unlimited".
			/// </remarks>
			[ParameterTableRowAttribute("wepParamChange", index: 105, minimum: 0, maximum: 255, step: 1, order: 2400, unknown2: 1)]
			[DisplayName("Versus weapon parameter change")]
			[Description("Specify whether to show an effect on any weapon . Attack - defense of all enemies , including the target in the case of unlimited")]
			[DefaultValue((SpecialEffectWeaponChange)0)]
			public SpecialEffectWeaponChange WepParamChange {
				get { return wepParamChange; }
				set { SetProperty(ref wepParamChange, ref value, WepParamChangeProperty); }
			}

			/// <summary>Movement type</summary>
			/// <remarks>
			/// Japanese short name: "移動タイプ", Google translated: "Movement type".
			/// Japanese description: "移動タイプ。移動速度を変更する。", Google translated: "Movement type . I want to change the moving speed .".
			/// </remarks>
			[ParameterTableRowAttribute("moveType", index: 106, minimum: 0, maximum: 255, step: 1, order: 8500, unknown2: 1)]
			[DisplayName("Movement type")]
			[Description("Movement type . I want to change the moving speed .")]
			[DefaultValue((SpecialEffectMoveType)0)]
			public SpecialEffectMoveType MoveType {
				get { return moveType; }
				set { SetProperty(ref moveType, ref value, MoveTypeProperty); }
			}

			/// <summary>Defense: life reduction type</summary>
			/// <remarks>
			/// Japanese short name: "防御：寿命減少タイプ", Google translated: "Defense: life reduction type".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("lifeReductionType", index: 107, minimum: 0, maximum: 255, step: 1, order: 9900, unknown2: 1)]
			[DisplayName("Defense: life reduction type")]
			[Description("")]
			[DefaultValue((SpecialEffectType)0)]
			public SpecialEffectType LifeReductionType {
				get { return lifeReductionType; }
				set { SetProperty(ref lifeReductionType, ref value, LifeReductionTypeProperty); }
			}

			/// <summary>Conditions throw</summary>
			/// <remarks>
			/// Japanese short name: "投げ条件", Google translated: "Conditions throw".
			/// Japanese description: "投げ条件。投げマスクに影響する。", Google translated: "Conditions throw . Affecting the mask throwing .".
			/// </remarks>
			[ParameterTableRowAttribute("throwCondition", index: 108, minimum: 0, maximum: 255, step: 1, order: 11000, unknown2: 1)]
			[DisplayName("Conditions throw")]
			[Description("Conditions throw . Affecting the mask throwing .")]
			[DefaultValue((SpecialEffectThrowCondition)0)]
			public SpecialEffectThrowCondition ThrowCondition {
				get { return throwCondition; }
				set { SetProperty(ref throwCondition, ref value, ThrowConditionProperty); }
			}

			/// <summary>Condition value to be added to the behavior determining ID</summary>
			/// <remarks>
			/// Japanese short name: "行動判定IDに加算する条件値", Google translated: "Condition value to be added to the behavior determining ID".
			/// Japanese description: "行動判定ＩＤに値を加算する条件値(Def:-1)", Google translated: "Condition value that adds value to the behavior determining ID (Def: -1)".
			/// </remarks>
			[ParameterTableRowAttribute("addBehaviorJudgeId_condition", index: 109, minimum: -1, maximum: 9, step: 1, order: 11300, unknown2: 1)]
			[DisplayName("Condition value to be added to the behavior determining ID")]
			[Description("Condition value that adds value to the behavior determining ID (Def: -1)")]
			[DefaultValue((SByte)(-1))]
			public SByte AddBehaviorJudgeId_condition {
				get { return addBehaviorJudgeId_condition; }
				set {
					if ((double)value < -1 || (double)value > 9)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 9 for " + AddBehaviorJudgeId_conditionProperty.Name + ".");
					SetProperty(ref addBehaviorJudgeId_condition, ref value, AddBehaviorJudgeId_conditionProperty);
				}
			}

			/// <summary>Additional value to be added to the behavior determining ID</summary>
			/// <remarks>
			/// Japanese short name: "行動判定IDに加算する加算値", Google translated: "Additional value to be added to the behavior determining ID".
			/// Japanese description: "行動判定IDの加算値 ０の場合は行動を切り替えるのではなく、行動しなくなります。", Google translated: "Rather than switch the action , you will not act in the case of the added value of 0 behavior determining ID.".
			/// </remarks>
			[ParameterTableRowAttribute("addBehaviorJudgeId_add", index: 110, minimum: 0, maximum: 255, step: 1, order: 11400, unknown2: 1)]
			[DisplayName("Additional value to be added to the behavior determining ID")]
			[Description("Rather than switch the action , you will not act in the case of the added value of 0 behavior determining ID.")]
			[DefaultValue((Byte)0)]
			public Byte AddBehaviorJudgeId_add {
				get { return addBehaviorJudgeId_add; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for " + AddBehaviorJudgeId_addProperty.Name + ".");
					SetProperty(ref addBehaviorJudgeId_add, ref value, AddBehaviorJudgeId_addProperty);
				}
			}

			/// <summary>Target effect : their affiliation</summary>
			/// <remarks>
			/// Japanese short name: "効果対象：所属　自分", Google translated: "Target effect : their affiliation".
			/// Japanese description: "この判定にチェックが入っている対象のみ効果を発揮する、デフォルトは×", Google translated: "Object is checked in this judgment only be effective , the default ×".
			/// </remarks>
			[ParameterTableRowAttribute("effectTargetSelf:1", index: 111, minimum: 0, maximum: 1, step: 1, order: 300, unknown2: 1)]
			[DisplayName("Target effect : their affiliation")]
			[Description("Object is checked in this judgment only be effective , the default ×")]
			[DefaultValue(false)]
			public Boolean EffectTargetSelf {
				get { return GetBitProperty(0, 1, EffectTargetSelfProperty) != 0; }
				set { SetBitProperty(0, 1, value ? 1 : 0, EffectTargetSelfProperty); }
			}

			/// <summary>Effect for: affiliation friend</summary>
			/// <remarks>
			/// Japanese short name: "効果対象：所属　味方", Google translated: "Effect for: affiliation friend".
			/// Japanese description: "この判定にチェックが入っている対象のみ効果を発揮する、デフォルトは×", Google translated: "Object is checked in this judgment only be effective , the default ×".
			/// </remarks>
			[ParameterTableRowAttribute("effectTargetFriend:1", index: 112, minimum: 0, maximum: 1, step: 1, order: 400, unknown2: 1)]
			[DisplayName("Effect for: affiliation friend")]
			[Description("Object is checked in this judgment only be effective , the default ×")]
			[DefaultValue(false)]
			public Boolean EffectTargetFriend {
				get { return GetBitProperty(1, 1, EffectTargetFriendProperty) != 0; }
				set { SetBitProperty(1, 1, value ? 1 : 0, EffectTargetFriendProperty); }
			}

			/// <summary>Effect for: affiliation enemy</summary>
			/// <remarks>
			/// Japanese short name: "効果対象：所属　敵", Google translated: "Effect for: affiliation enemy".
			/// Japanese description: "この判定にチェックが入っている対象のみ効果を発揮する、デフォルトは×", Google translated: "Object is checked in this judgment only be effective , the default ×".
			/// </remarks>
			[ParameterTableRowAttribute("effectTargetEnemy:1", index: 113, minimum: 0, maximum: 1, step: 1, order: 500, unknown2: 1)]
			[DisplayName("Effect for: affiliation enemy")]
			[Description("Object is checked in this judgment only be effective , the default ×")]
			[DefaultValue(false)]
			public Boolean EffectTargetEnemy {
				get { return GetBitProperty(2, 1, EffectTargetEnemyProperty) != 0; }
				set { SetBitProperty(2, 1, value ? 1 : 0, EffectTargetEnemyProperty); }
			}

			/// <summary>Effect for: PC operation</summary>
			/// <remarks>
			/// Japanese short name: "効果対象：操作　PC", Google translated: "Effect for: PC operation".
			/// Japanese description: "この判定にチェックが入っている対象のみ効果を発揮する、デフォルトは×", Google translated: "Object is checked in this judgment only be effective , the default ×".
			/// </remarks>
			[ParameterTableRowAttribute("effectTargetPlayer:1", index: 114, minimum: 0, maximum: 1, step: 1, order: 600, unknown2: 1)]
			[DisplayName("Effect for: PC operation")]
			[Description("Object is checked in this judgment only be effective , the default ×")]
			[DefaultValue(false)]
			public Boolean EffectTargetPlayer {
				get { return GetBitProperty(3, 1, EffectTargetPlayerProperty) != 0; }
				set { SetBitProperty(3, 1, value ? 1 : 0, EffectTargetPlayerProperty); }
			}

			/// <summary>Effect for: Operation AI</summary>
			/// <remarks>
			/// Japanese short name: "効果対象：操作　AI", Google translated: "Effect for: Operation AI".
			/// Japanese description: "この判定にチェックが入っている対象のみ効果を発揮する、デフォルトは×", Google translated: "Object is checked in this judgment only be effective , the default ×".
			/// </remarks>
			[ParameterTableRowAttribute("effectTargetAI:1", index: 115, minimum: 0, maximum: 1, step: 1, order: 700, unknown2: 1)]
			[DisplayName("Effect for: Operation AI")]
			[Description("Object is checked in this judgment only be effective , the default ×")]
			[DefaultValue(false)]
			public Boolean EffectTargetAI {
				get { return GetBitProperty(4, 1, EffectTargetAIProperty) != 0; }
				set { SetBitProperty(4, 1, value ? 1 : 0, EffectTargetAIProperty); }
			}

			/// <summary>Effect for: state survival</summary>
			/// <remarks>
			/// Japanese short name: "効果対象：状態　生存", Google translated: "Effect for: state survival".
			/// Japanese description: "この判定にチェックが入っている対象のみ効果を発揮する、デフォルトは×", Google translated: "Object is checked in this judgment only be effective , the default ×".
			/// </remarks>
			[ParameterTableRowAttribute("effectTargetLive:1", index: 116, minimum: 0, maximum: 1, step: 1, order: 800, unknown2: 1)]
			[DisplayName("Effect for: state survival")]
			[Description("Object is checked in this judgment only be effective , the default ×")]
			[DefaultValue(false)]
			public Boolean EffectTargetLive {
				get { return GetBitProperty(5, 1, EffectTargetLiveProperty) != 0; }
				set { SetBitProperty(5, 1, value ? 1 : 0, EffectTargetLiveProperty); }
			}

			/// <summary>Effect for: state all ghost</summary>
			/// <remarks>
			/// Japanese short name: "効果対象：状態　全ゴースト", Google translated: "Effect for: state all ghost".
			/// Japanese description: "この判定にチェックが入っている対象のみ効果を発揮する、デフォルトは×", Google translated: "Object is checked in this judgment only be effective , the default ×".
			/// </remarks>
			[ParameterTableRowAttribute("effectTargetGhost:1", index: 117, minimum: 0, maximum: 1, step: 1, order: 900, unknown2: 1)]
			[DisplayName("Effect for: state all ghost")]
			[Description("Object is checked in this judgment only be effective , the default ×")]
			[DefaultValue(false)]
			public Boolean EffectTargetGhost {
				get { return GetBitProperty(6, 1, EffectTargetGhostProperty) != 0; }
				set { SetBitProperty(6, 1, value ? 1 : 0, EffectTargetGhostProperty); }
			}

			/// <summary>Effect for: state white ghost</summary>
			/// <remarks>
			/// Japanese short name: "効果対象：状態　白ゴースト", Google translated: "Effect for: state white ghost".
			/// Japanese description: "この判定にチェックが入っている対象のみ効果を発揮する、デフォルトは×", Google translated: "Object is checked in this judgment only be effective , the default ×".
			/// </remarks>
			[ParameterTableRowAttribute("effectTargetWhiteGhost:1", index: 118, minimum: 0, maximum: 1, step: 1, order: 1000, unknown2: 1)]
			[DisplayName("Effect for: state white ghost")]
			[Description("Object is checked in this judgment only be effective , the default ×")]
			[DefaultValue(false)]
			public Boolean EffectTargetWhiteGhost {
				get { return GetBitProperty(7, 1, EffectTargetWhiteGhostProperty) != 0; }
				set { SetBitProperty(7, 1, value ? 1 : 0, EffectTargetWhiteGhostProperty); }
			}

			/// <summary>Effect for: black ghost state</summary>
			/// <remarks>
			/// Japanese short name: "効果対象：状態　黒ゴースト", Google translated: "Effect for: black ghost state".
			/// Japanese description: "この判定にチェックが入っている対象のみ効果を発揮する、デフォルトは×", Google translated: "Object is checked in this judgment only be effective , the default ×".
			/// </remarks>
			[ParameterTableRowAttribute("effectTargetBlackGhost:1", index: 119, minimum: 0, maximum: 1, step: 1, order: 1100, unknown2: 1)]
			[DisplayName("Effect for: black ghost state")]
			[Description("Object is checked in this judgment only be effective , the default ×")]
			[DefaultValue(false)]
			public Boolean EffectTargetBlackGhost {
				get { return GetBitProperty(8, 1, EffectTargetBlackGhostProperty) != 0; }
				set { SetBitProperty(8, 1, value ? 1 : 0, EffectTargetBlackGhostProperty); }
			}

			/// <summary>The trigger to the attacker : target effect</summary>
			/// <remarks>
			/// Japanese short name: "効果対象：攻撃者に発動", Google translated: "The trigger to the attacker : target effect".
			/// Japanese description: "ダメージ後に攻撃者に特殊効果を適用（防御側には入れない）", Google translated: "( Do not put the defense side) and apply special effects to the attacker to damage after".
			/// </remarks>
			[ParameterTableRowAttribute("effectTargetAttacker:1", index: 120, minimum: 0, maximum: 1, step: 1, order: 1200, unknown2: 1)]
			[DisplayName("The trigger to the attacker : target effect")]
			[Description("( Do not put the defense side) and apply special effects to the attacker to damage after")]
			[DefaultValue(false)]
			public Boolean EffectTargetAttacker {
				get { return GetBitProperty(9, 1, EffectTargetAttackerProperty) != 0; }
				set { SetBitProperty(9, 1, value ? 1 : 0, EffectTargetAttackerProperty); }
			}

			/// <summary>The icon display without by fires</summary>
			/// <remarks>
			/// Japanese short name: "発動してなくてもアイコン表示", Google translated: "The icon display without by fires".
			/// Japanese description: "発動待ちの状態でもアイコンを表示する。", Google translated: "I want to display an icon in the state of waiting for activation .".
			/// </remarks>
			[ParameterTableRowAttribute("dispIconNonactive:1", index: 121, minimum: 0, maximum: 1, step: 1, order: 1300, unknown2: 1)]
			[DisplayName("The icon display without by fires")]
			[Description("I want to display an icon in the state of waiting for activation .")]
			[DefaultValue(false)]
			public Boolean DispIconNonactive {
				get { return GetBitProperty(10, 1, DispIconNonactiveProperty) != 0; }
				set { SetBitProperty(10, 1, value ? 1 : 0, DispIconNonactiveProperty); }
			}

			/// <summary>You can use the special effects effect</summary>
			/// <remarks>
			/// Japanese short name: "特殊効果エフェクトを使用するか", Google translated: "You can use the special effects effect".
			/// Japanese description: "特殊効果エフェクトを使用するか", Google translated: "You can use the special effects effect".
			/// </remarks>
			[ParameterTableRowAttribute("useSpEffectEffect:1", index: 122, minimum: 0, maximum: 1, step: 1, order: 1400, unknown2: 1)]
			[DisplayName("You can use the special effects effect")]
			[Description("You can use the special effects effect")]
			[DefaultValue(false)]
			public Boolean UseSpEffectEffect {
				get { return GetBitProperty(11, 1, UseSpEffectEffectProperty) != 0; }
				set { SetBitProperty(11, 1, value ? 1 : 0, UseSpEffectEffectProperty); }
			}

			/// <summary>Correction or magic ?</summary>
			/// <remarks>
			/// Japanese short name: "魔力補正するか？", Google translated: "Correction or magic ?".
			/// Japanese description: "魔力補正するか？", Google translated: "Correction or magic ?".
			/// </remarks>
			[ParameterTableRowAttribute("bAdjustMagicAblity:1", index: 123, minimum: 0, maximum: 1, step: 1, order: 1500, unknown2: 1)]
			[DisplayName("Correction or magic ?")]
			[Description("Correction or magic ?")]
			[DefaultValue(false)]
			public Boolean BAdjustMagicAblity {
				get { return GetBitProperty(12, 1, BAdjustMagicAblityProperty) != 0; }
				set { SetBitProperty(12, 1, value ? 1 : 0, BAdjustMagicAblityProperty); }
			}

			/// <summary>Faith or correction ?</summary>
			/// <remarks>
			/// Japanese short name: "信仰補正するか？", Google translated: "Faith or correction ?".
			/// Japanese description: "信仰補正するか？", Google translated: "Faith or correction ?".
			/// </remarks>
			[ParameterTableRowAttribute("bAdjustFaithAblity:1", index: 124, minimum: 0, maximum: 1, step: 1, order: 1600, unknown2: 1)]
			[DisplayName("Faith or correction ?")]
			[Description("Faith or correction ?")]
			[DefaultValue(false)]
			public Boolean BAdjustFaithAblity {
				get { return GetBitProperty(13, 1, BAdjustFaithAblityProperty) != 0; }
				set { SetBitProperty(13, 1, value ? 1 : 0, BAdjustFaithAblityProperty); }
			}

			/// <summary>Or bonus round for ?</summary>
			/// <remarks>
			/// Japanese short name: "周回ボーナス用か？", Google translated: "Or bonus round for ?".
			/// Japanese description: "ゲームクリア周回ボーナス用かどうか。", Google translated: "Whether game clear bonus round for .".
			/// </remarks>
			[ParameterTableRowAttribute("bGameClearBonus:1", index: 125, minimum: 0, maximum: 1, step: 1, order: 1700, unknown2: 1)]
			[DisplayName("Or bonus round for ?")]
			[Description("Whether game clear bonus round for .")]
			[DefaultValue(false)]
			public Boolean BGameClearBonus {
				get { return GetBitProperty(14, 1, BGameClearBonusProperty) != 0; }
				set { SetBitProperty(14, 1, value ? 1 : 0, BGameClearBonusProperty); }
			}

			/// <summary>Versus magic parameter change</summary>
			/// <remarks>
			/// Japanese short name: "対魔法パラメータ変化", Google translated: "Versus magic parameter change".
			/// Japanese description: "魔法に対して効果を発揮するかしないかを設定する", Google translated: "Set whether to exert an effect on magic".
			/// </remarks>
			[ParameterTableRowAttribute("magParamChange:1", index: 126, minimum: 0, maximum: 1, step: 1, order: 2500, unknown2: 1)]
			[DisplayName("Versus magic parameter change")]
			[Description("Set whether to exert an effect on magic")]
			[DefaultValue(false)]
			public Boolean MagParamChange {
				get { return GetBitProperty(15, 1, MagParamChangeProperty) != 0; }
				set { SetBitProperty(15, 1, value ? 1 : 0, MagParamChangeProperty); }
			}

			/// <summary>Versus miracle parameter change</summary>
			/// <remarks>
			/// Japanese short name: "対奇跡パラメータ変化", Google translated: "Versus miracle parameter change".
			/// Japanese description: "奇跡に対して効果を発揮するかしないかを設定する", Google translated: "To set whether beneficial for miracle".
			/// </remarks>
			[ParameterTableRowAttribute("miracleParamChange:1", index: 127, minimum: 0, maximum: 1, step: 1, order: 2600, unknown2: 1)]
			[DisplayName("Versus miracle parameter change")]
			[Description("To set whether beneficial for miracle")]
			[DefaultValue(false)]
			public Boolean MiracleParamChange {
				get { return GetBitProperty(16, 1, MiracleParamChangeProperty) != 0; }
				set { SetBitProperty(16, 1, value ? 1 : 0, MiracleParamChangeProperty); }
			}

			/// <summary>Or possession Seoul clear</summary>
			/// <remarks>
			/// Japanese short name: "所持ソウルクリアするか", Google translated: "Or possession Seoul clear".
			/// Japanese description: "所持ソウルが0になります。", Google translated: "Possession Seoul is 0 .".
			/// </remarks>
			[ParameterTableRowAttribute("clearSoul:1", index: 128, minimum: 0, maximum: 1, step: 1, order: 2700, unknown2: 1)]
			[DisplayName("Or possession Seoul clear")]
			[Description("Possession Seoul is 0 .")]
			[DefaultValue(false)]
			public Boolean ClearSoul {
				get { return GetBitProperty(17, 1, ClearSoulProperty) != 0; }
				set { SetBitProperty(17, 1, value ? 1 : 0, ClearSoulProperty); }
			}

			/// <summary>SOS sign determination flag</summary>
			/// <remarks>
			/// Japanese short name: "SOSサイン　判定フラグ", Google translated: "SOS sign determination flag".
			/// Japanese description: "チェックが付いている場合、発動時にSOSサイン要求を発行", Google translated: "If checked , it issues a request to the SOS sign upon activation".
			/// </remarks>
			[ParameterTableRowAttribute("requestSOS:1", index: 129, minimum: 0, maximum: 1, step: 1, order: 7600, unknown2: 1)]
			[DisplayName("SOS sign determination flag")]
			[Description("If checked , it issues a request to the SOS sign upon activation")]
			[DefaultValue(false)]
			public Boolean RequestSOS {
				get { return GetBitProperty(18, 1, RequestSOSProperty) != 0; }
				set { SetBitProperty(18, 1, value ? 1 : 0, RequestSOSProperty); }
			}

			/// <summary>Black SOS sign determination flag</summary>
			/// <remarks>
			/// Japanese short name: "ブラックSOSサイン　判定フラグ", Google translated: "Black SOS sign determination flag".
			/// Japanese description: "チェックが付いている場合、発動時にブラックSOSサイン要求を発行", Google translated: "If checked , issue the black SOS request to sign upon activation".
			/// </remarks>
			[ParameterTableRowAttribute("requestBlackSOS:1", index: 130, minimum: 0, maximum: 1, step: 1, order: 7700, unknown2: 1)]
			[DisplayName("Black SOS sign determination flag")]
			[Description("If checked , issue the black SOS request to sign upon activation")]
			[DefaultValue(false)]
			public Boolean RequestBlackSOS {
				get { return GetBitProperty(19, 1, RequestBlackSOSProperty) != 0; }
				set { SetBitProperty(19, 1, value ? 1 : 0, RequestBlackSOSProperty); }
			}

			/// <summary>Black Force participation SOS sign determination flag</summary>
			/// <remarks>
			/// Japanese short name: "ブラック強制参加SOSサイン　判定フラグ", Google translated: "Black Force participation SOS sign determination flag".
			/// Japanese description: "チェックが付いている場合、発動時にブラック強制参加SOSサイン要求を発行", Google translated: "If checked , issue the black force participation SOS request to sign upon activation".
			/// </remarks>
			[ParameterTableRowAttribute("requestForceJoinBlackSOS:1", index: 131, minimum: 0, maximum: 1, step: 1, order: 7800, unknown2: 1)]
			[DisplayName("Black Force participation SOS sign determination flag")]
			[Description("If checked , issue the black force participation SOS request to sign upon activation")]
			[DefaultValue(false)]
			public Boolean RequestForceJoinBlackSOS {
				get { return GetBitProperty(20, 1, RequestForceJoinBlackSOSProperty) != 0; }
				set { SetBitProperty(20, 1, value ? 1 : 0, RequestForceJoinBlackSOSProperty); }
			}

			/// <summary>Kick determination flag</summary>
			/// <remarks>
			/// Japanese short name: "キック　判定フラグ", Google translated: "Kick determination flag".
			/// Japanese description: "チェックが付いている場合、発動時にキック要求を発行", Google translated: "If checked , it issues a request to kick upon activation".
			/// </remarks>
			[ParameterTableRowAttribute("requestKickSession:1", index: 132, minimum: 0, maximum: 1, step: 1, order: 7900, unknown2: 1)]
			[DisplayName("Kick determination flag")]
			[Description("If checked , it issues a request to kick upon activation")]
			[DefaultValue(false)]
			public Boolean RequestKickSession {
				get { return GetBitProperty(21, 1, RequestKickSessionProperty) != 0; }
				set { SetBitProperty(21, 1, value ? 1 : 0, RequestKickSessionProperty); }
			}

			/// <summary>Exit determination flag</summary>
			/// <remarks>
			/// Japanese short name: "退出　判定フラグ", Google translated: "Exit determination flag".
			/// Japanese description: "チェックが付いている場合、発動時に退出要求を発行", Google translated: "If checked , it issues a request to exit upon activation".
			/// </remarks>
			[ParameterTableRowAttribute("requestLeaveSession:1", index: 133, minimum: 0, maximum: 1, step: 1, order: 8000, unknown2: 1)]
			[DisplayName("Exit determination flag")]
			[Description("If checked , it issues a request to exit upon activation")]
			[DefaultValue(false)]
			public Boolean RequestLeaveSession {
				get { return GetBitProperty(22, 1, RequestLeaveSessionProperty) != 0; }
				set { SetBitProperty(22, 1, value ? 1 : 0, RequestLeaveSessionProperty); }
			}

			/// <summary>Intrusion determination flag to the NPC</summary>
			/// <remarks>
			/// Japanese short name: "NPCへの侵入　判定フラグ", Google translated: "Intrusion determination flag to the NPC".
			/// Japanese description: "チェックが付いている場合、発動時にNPCへの侵入要求を発行", Google translated: "If checked , it issues a request to the invasion in NPC upon activation".
			/// </remarks>
			[ParameterTableRowAttribute("requestNpcInveda:1", index: 134, minimum: 0, maximum: 1, step: 1, order: 8010, unknown2: 1)]
			[DisplayName("Intrusion determination flag to the NPC")]
			[Description("If checked , it issues a request to the invasion in NPC upon activation")]
			[DefaultValue(false)]
			public Boolean RequestNpcInveda {
				get { return GetBitProperty(23, 1, RequestNpcInvedaProperty) != 0; }
				set { SetBitProperty(23, 1, value ? 1 : 0, RequestNpcInvedaProperty); }
			}

			/// <summary>Buddhahood not flag decision</summary>
			/// <remarks>
			/// Japanese short name: "成仏不可　判定フラグ", Google translated: "Buddhahood not flag decision".
			/// Japanese description: "死体状態になれるかどうか。このチェックが付いていると、死亡状態にならない", Google translated: "Whether you become a corpse state . This check is attached , it does not become the state death".
			/// </remarks>
			[ParameterTableRowAttribute("noDead:1", index: 135, minimum: 0, maximum: 1, step: 1, order: 8100, unknown2: 1)]
			[DisplayName("Buddhahood not flag decision")]
			[Description("Whether you become a corpse state . This check is attached , it does not become the state death")]
			[DefaultValue(false)]
			public Boolean NoDead {
				get { return GetBitProperty(24, 1, NoDeadProperty) != 0; }
				set { SetBitProperty(24, 1, value ? 1 : 0, NoDeadProperty); }
			}

			/// <summary>Maximum HP will be increased or decreased , the current HP or no effect ?</summary>
			/// <remarks>
			/// Japanese short name: "最大HPが増減しても、現在HPは影響しないか？", Google translated: "Maximum HP will be increased or decreased , the current HP or no effect ?".
			/// Japanese description: "最大HPが増減しても、現在HPは影響しないか？", Google translated: "Maximum HP will be increased or decreased , the current HP or no effect ?".
			/// </remarks>
			[ParameterTableRowAttribute("bCurrHPIndependeMaxHP:1", index: 136, minimum: 0, maximum: 1, step: 1, order: 8200, unknown2: 1)]
			[DisplayName("Maximum HP will be increased or decreased , the current HP or no effect ?")]
			[Description("Maximum HP will be increased or decreased , the current HP or no effect ?")]
			[DefaultValue(false)]
			public Boolean BCurrHPIndependeMaxHP {
				get { return GetBitProperty(25, 1, BCurrHPIndependeMaxHPProperty) != 0; }
				set { SetBitProperty(25, 1, value ? 1 : 0, BCurrHPIndependeMaxHPProperty); }
			}

			/// <summary>Corrosion ignored</summary>
			/// <remarks>
			/// Japanese short name: "腐食無視", Google translated: "Corrosion ignored".
			/// Japanese description: "【状態変化タイプ】が【腐食】による【耐久度】減少を無視する", Google translated: "I ignore state change [ type ] is due to the corrosion ] and [ durability ] decrease".
			/// </remarks>
			[ParameterTableRowAttribute("corrosionIgnore:1", index: 137, minimum: 0, maximum: 1, step: 1, order: 9200, unknown2: 1)]
			[DisplayName("Corrosion ignored")]
			[Description("I ignore state change [ type ] is due to the corrosion ] and [ durability ] decrease")]
			[DefaultValue(false)]
			public Boolean CorrosionIgnore {
				get { return GetBitProperty(26, 1, CorrosionIgnoreProperty) != 0; }
				set { SetBitProperty(26, 1, value ? 1 : 0, CorrosionIgnoreProperty); }
			}

			/// <summary>Visual search operation cut ignored</summary>
			/// <remarks>
			/// Japanese short name: "視覚索敵カット無視", Google translated: "Visual search operation cut ignored".
			/// Japanese description: "視覚索敵無効を無視する", Google translated: "I ignore the visual search operation invalid".
			/// </remarks>
			[ParameterTableRowAttribute("sightSearchCutIgnore:1", index: 138, minimum: 0, maximum: 1, step: 1, order: 9300, unknown2: 1)]
			[DisplayName("Visual search operation cut ignored")]
			[Description("I ignore the visual search operation invalid")]
			[DefaultValue(false)]
			public Boolean SightSearchCutIgnore {
				get { return GetBitProperty(27, 1, SightSearchCutIgnoreProperty) != 0; }
				set { SetBitProperty(27, 1, value ? 1 : 0, SightSearchCutIgnoreProperty); }
			}

			/// <summary>Hearing searching for the enemy cut ignored</summary>
			/// <remarks>
			/// Japanese short name: "聴覚索敵カット無視", Google translated: "Hearing searching for the enemy cut ignored".
			/// Japanese description: "聴覚索敵無効を無視する", Google translated: "I ignore the hearing invalid search operation".
			/// </remarks>
			[ParameterTableRowAttribute("hearingSearchCutIgnore:1", index: 139, minimum: 0, maximum: 1, step: 1, order: 9400, unknown2: 1)]
			[DisplayName("Hearing searching for the enemy cut ignored")]
			[Description("I ignore the hearing invalid search operation")]
			[DefaultValue(false)]
			public Boolean HearingSearchCutIgnore {
				get { return GetBitProperty(28, 1, HearingSearchCutIgnoreProperty) != 0; }
				set { SetBitProperty(28, 1, value ? 1 : 0, HearingSearchCutIgnoreProperty); }
			}

			/// <summary>Anti- Magic invalid</summary>
			/// <remarks>
			/// Japanese short name: "アンチマジック無効", Google translated: "Anti- Magic invalid".
			/// Japanese description: "アンチマジック範囲でも魔法を使用できる", Google translated: "I can use the magic in the Anti- Magic range".
			/// </remarks>
			[ParameterTableRowAttribute("antiMagicIgnore:1", index: 140, minimum: 0, maximum: 1, step: 1, order: 9500, unknown2: 1)]
			[DisplayName("Anti- Magic invalid")]
			[Description("I can use the magic in the Anti- Magic range")]
			[DefaultValue(false)]
			public Boolean AntiMagicIgnore {
				get { return GetBitProperty(29, 1, AntiMagicIgnoreProperty) != 0; }
				set { SetBitProperty(29, 1, value ? 1 : 0, AntiMagicIgnoreProperty); }
			}

			/// <summary>False target invalid</summary>
			/// <remarks>
			/// Japanese short name: "偽ターゲット無効", Google translated: "False target invalid".
			/// Japanese description: "発生した偽ターゲットに引っかからなくなる", Google translated: "It will not catch on the false target that occurred".
			/// </remarks>
			[ParameterTableRowAttribute("fakeTargetIgnore:1", index: 141, minimum: 0, maximum: 1, step: 1, order: 9600, unknown2: 1)]
			[DisplayName("False target invalid")]
			[Description("It will not catch on the false target that occurred")]
			[DefaultValue(false)]
			public Boolean FakeTargetIgnore {
				get { return GetBitProperty(30, 1, FakeTargetIgnoreProperty) != 0; }
				set { SetBitProperty(30, 1, value ? 1 : 0, FakeTargetIgnoreProperty); }
			}

			/// <summary>False target invalid _ immortality system</summary>
			/// <remarks>
			/// Japanese short name: "偽ターゲット無効_不死系", Google translated: "False target invalid _ immortality system".
			/// Japanese description: "発生した不死系の偽ターゲットに引っかからなくなる", Google translated: "I will not catch on the false target immortal system that occurred".
			/// </remarks>
			[ParameterTableRowAttribute("fakeTargetIgnoreUndead:1", index: 142, minimum: 0, maximum: 1, step: 1, order: 9650, unknown2: 1)]
			[DisplayName("False target invalid _ immortality system")]
			[Description("I will not catch on the false target immortal system that occurred")]
			[DefaultValue(false)]
			public Boolean FakeTargetIgnoreUndead {
				get { return GetBitProperty(31, 1, FakeTargetIgnoreUndeadProperty) != 0; }
				set { SetBitProperty(31, 1, value ? 1 : 0, FakeTargetIgnoreUndeadProperty); }
			}

			/// <summary>False target invalid _ beast system</summary>
			/// <remarks>
			/// Japanese short name: "偽ターゲット無効_獣系", Google translated: "False target invalid _ beast system".
			/// Japanese description: "発生した獣系の偽ターゲットに引っかからなくなる", Google translated: "I will not catch on the fake target of the beast system that occurred".
			/// </remarks>
			[ParameterTableRowAttribute("fakeTargetIgnoreAnimal:1", index: 143, minimum: 0, maximum: 1, step: 1, order: 9660, unknown2: 1)]
			[DisplayName("False target invalid _ beast system")]
			[Description("I will not catch on the fake target of the beast system that occurred")]
			[DefaultValue(false)]
			public Boolean FakeTargetIgnoreAnimal {
				get { return GetBitProperty(32, 1, FakeTargetIgnoreAnimalProperty) != 0; }
				set { SetBitProperty(32, 1, value ? 1 : 0, FakeTargetIgnoreAnimalProperty); }
			}

			/// <summary>Gravity invalid</summary>
			/// <remarks>
			/// Japanese short name: "グラビティ無効", Google translated: "Gravity invalid".
			/// Japanese description: "グラビティ効果無効", Google translated: "Gravity effect invalid".
			/// </remarks>
			[ParameterTableRowAttribute("grabityIgnore:1", index: 144, minimum: 0, maximum: 1, step: 1, order: 9700, unknown2: 1)]
			[DisplayName("Gravity invalid")]
			[Description("Gravity effect invalid")]
			[DefaultValue(false)]
			public Boolean GrabityIgnore {
				get { return GetBitProperty(33, 1, GrabityIgnoreProperty) != 0; }
				set { SetBitProperty(33, 1, value ? 1 : 0, GrabityIgnoreProperty); }
			}

			/// <summary>Poison invalid</summary>
			/// <remarks>
			/// Japanese short name: "毒無効", Google translated: "Poison invalid".
			/// Japanese description: "この効果がかかっていると毒にかからなくなる", Google translated: "It is not applied to poison this effect is at stake".
			/// </remarks>
			[ParameterTableRowAttribute("disablePoison:1", index: 145, minimum: 0, maximum: 1, step: 1, order: 10200, unknown2: 1)]
			[DisplayName("Poison invalid")]
			[Description("It is not applied to poison this effect is at stake")]
			[DefaultValue(false)]
			public Boolean DisablePoison {
				get { return GetBitProperty(34, 1, DisablePoisonProperty) != 0; }
				set { SetBitProperty(34, 1, value ? 1 : 0, DisablePoisonProperty); }
			}

			/// <summary>Plague invalid</summary>
			/// <remarks>
			/// Japanese short name: "疫病無効", Google translated: "Plague invalid".
			/// Japanese description: "この効果がかかっていると疫病にかからなくなる", Google translated: "I no longer applied to the plague this effect is at stake".
			/// </remarks>
			[ParameterTableRowAttribute("disableDisease:1", index: 146, minimum: 0, maximum: 1, step: 1, order: 10300, unknown2: 1)]
			[DisplayName("Plague invalid")]
			[Description("I no longer applied to the plague this effect is at stake")]
			[DefaultValue(false)]
			public Boolean DisableDisease {
				get { return GetBitProperty(35, 1, DisableDiseaseProperty) != 0; }
				set { SetBitProperty(35, 1, value ? 1 : 0, DisableDiseaseProperty); }
			}

			/// <summary>Bleeding invalid</summary>
			/// <remarks>
			/// Japanese short name: "出血無効", Google translated: "Bleeding invalid".
			/// Japanese description: "この効果がかかっていると出血にかからなくなる", Google translated: "I no longer applied to the bleeding this effect is at stake".
			/// </remarks>
			[ParameterTableRowAttribute("disableBlood:1", index: 147, minimum: 0, maximum: 1, step: 1, order: 10400, unknown2: 1)]
			[DisplayName("Bleeding invalid")]
			[Description("I no longer applied to the bleeding this effect is at stake")]
			[DefaultValue(false)]
			public Boolean DisableBlood {
				get { return GetBitProperty(36, 1, DisableBloodProperty) != 0; }
				set { SetBitProperty(36, 1, value ? 1 : 0, DisableBloodProperty); }
			}

			/// <summary>Curse invalid</summary>
			/// <remarks>
			/// Japanese short name: "呪無効", Google translated: "Curse invalid".
			/// Japanese description: "この効果がかかっていると呪いにかからなくなる", Google translated: "It is not applied to curse this effect is at stake".
			/// </remarks>
			[ParameterTableRowAttribute("disableCurse:1", index: 148, minimum: 0, maximum: 1, step: 1, order: 10400, unknown2: 1)]
			[DisplayName("Curse invalid")]
			[Description("It is not applied to curse this effect is at stake")]
			[DefaultValue(false)]
			public Boolean DisableCurse {
				get { return GetBitProperty(37, 1, DisableCurseProperty) != 0; }
				set { SetBitProperty(37, 1, value ? 1 : 0, DisableCurseProperty); }
			}

			/// <summary>Fascinated effective</summary>
			/// <remarks>
			/// Japanese short name: "魅了有効", Google translated: "Fascinated effective".
			/// Japanese description: "この効果がかかっていると魅了にかかるようになる", Google translated: "I would like to take this effect is fascinated rests".
			/// </remarks>
			[ParameterTableRowAttribute("enableCharm:1", index: 149, minimum: 0, maximum: 1, step: 1, order: 10410, unknown2: 1)]
			[DisplayName("Fascinated effective")]
			[Description("I would like to take this effect is fascinated rests")]
			[DefaultValue(false)]
			public Boolean EnableCharm {
				get { return GetBitProperty(38, 1, EnableCharmProperty) != 0; }
				set { SetBitProperty(38, 1, value ? 1 : 0, EnableCharmProperty); }
			}

			/// <summary>Or life extension ?</summary>
			/// <remarks>
			/// Japanese short name: "寿命延長するか？", Google translated: "Or life extension ?".
			/// Japanese description: "TAEによるフラグ設定時に寿命延長するか？", Google translated: "You can extend life to flag setting by TAE?".
			/// </remarks>
			[ParameterTableRowAttribute("enableLifeTime:1", index: 150, minimum: 0, maximum: 1, step: 1, order: 10500, unknown2: 1)]
			[DisplayName("Or life extension ?")]
			[Description("You can extend life to flag setting by TAE?")]
			[DefaultValue(false)]
			public Boolean EnableLifeTime {
				get { return GetBitProperty(39, 1, EnableLifeTimeProperty) != 0; }
				set { SetBitProperty(39, 1, value ? 1 : 0, EnableLifeTimeProperty); }
			}

			/// <summary></summary>
			/// <remarks>
			/// Japanese short name: "敵を把握しているか？", Google translated: "".
			/// Japanese description: "敵を把握しているか？：[発動条件](邪眼使用者用)", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("hasTarget : 1", index: 151, minimum: 0, maximum: 1, step: 1, order: 10900, unknown2: 1)]
			[DisplayName("")]
			[Description("")]
			[DefaultValue(false)]
			public Boolean HasTarget {
				get { return GetBitProperty(40, 1, HasTargetProperty) != 0; }
				set { SetBitProperty(40, 1, value ? 1 : 0, HasTargetProperty); }
			}

			/// <summary>Release conditions : fire damage</summary>
			/// <remarks>
			/// Japanese short name: "解除条件:炎ダメージ", Google translated: "Release conditions : fire damage".
			/// Japanese description: "炎ダメージによる特殊効果の解除を行うか？", Google translated: "You can do the release of the special effects of fire damage ?".
			/// </remarks>
			[ParameterTableRowAttribute("isFireDamageCancel:1", index: 152, minimum: 0, maximum: 1, step: 1, order: 10910, unknown2: 1)]
			[DisplayName("Release conditions : fire damage")]
			[Description("You can do the release of the special effects of fire damage ?")]
			[DefaultValue(false)]
			public Boolean IsFireDamageCancel {
				get { return GetBitProperty(41, 1, IsFireDamageCancelProperty) != 0; }
				set { SetBitProperty(41, 1, value ? 1 : 0, IsFireDamageCancelProperty); }
			}

			/// <summary>You can extend life span extension effect ?</summary>
			/// <remarks>
			/// Japanese short name: "寿命延長効果で延長するか？", Google translated: "You can extend life span extension effect ?".
			/// Japanese description: "寿命延長効果が掛かっている時に延長対象になるかどうか", Google translated: "Whether be an extension object when the life extension effect is applied".
			/// </remarks>
			[ParameterTableRowAttribute("isExtendSpEffectLife:1", index: 153, minimum: 0, maximum: 1, step: 1, order: 10510, unknown2: 1)]
			[DisplayName("You can extend life span extension effect ?")]
			[Description("Whether be an extension object when the life extension effect is applied")]
			[DefaultValue(false)]
			public Boolean IsExtendSpEffectLife {
				get { return GetBitProperty(42, 1, IsExtendSpEffectLifeProperty) != 0; }
				set { SetBitProperty(42, 1, value ? 1 : 0, IsExtendSpEffectLifeProperty); }
			}

			/// <summary>Arena exit determination flag</summary>
			/// <remarks>
			/// Japanese short name: "闘技場退出　判定フラグ", Google translated: "Arena exit determination flag".
			/// Japanese description: "チェックが付いている場合、発動時に闘技場退出要求を発行", Google translated: "If checked , issue the arena request to exit upon activation".
			/// </remarks>
			[ParameterTableRowAttribute("requestLeaveColiseumSession:1", index: 154, minimum: 0, maximum: 1, step: 1, order: 8000, unknown2: 1)]
			[DisplayName("Arena exit determination flag")]
			[Description("If checked , issue the arena request to exit upon activation")]
			[DefaultValue(false)]
			public Boolean RequestLeaveColiseumSession {
				get { return GetBitProperty(43, 1, RequestLeaveColiseumSessionProperty) != 0; }
				set { SetBitProperty(43, 1, value ? 1 : 0, RequestLeaveColiseumSessionProperty); }
			}

			/// <summary>pad</summary>
			/// <remarks>
			/// Japanese short name: "pad", Google translated: "pad".
			/// Japanese description: "pad", Google translated: "pad".
			/// </remarks>
			[ParameterTableRowAttribute("pad_2:4", index: 155, minimum: 0, maximum: 0, step: 0, order: 13001, unknown2: 0)]
			[DisplayName("pad")]
			[Description("pad")]
			[DefaultValue((Byte)0)]
			[Browsable(false)]
			public Byte Pad_2 {
				get { return (Byte)GetBitProperty(44, 4, Pad_2Property); }
				set {
					if ((double)value < 0 || (double)value > 0)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 0 for " + Pad_2Property.Name + ".");
					SetBitProperty(44, 4, (int)value, Pad_2Property);
				}
			}

			/// <summary>Pledge 0</summary>
			/// <remarks>
			/// Japanese short name: "誓約0", Google translated: "Pledge 0".
			/// Japanese description: "誓約0", Google translated: "Pledge 0".
			/// </remarks>
			[ParameterTableRowAttribute("vowType0:1", index: 156, minimum: 0, maximum: 1, step: 1, order: 11500, unknown2: 1)]
			[DisplayName("Pledge 0")]
			[Description("Pledge 0")]
			[DefaultValue(false)]
			public Boolean VowType0 {
				get { return GetBitProperty(48, 1, VowType0Property) != 0; }
				set { SetBitProperty(48, 1, value ? 1 : 0, VowType0Property); }
			}

			/// <summary>Pledge 1</summary>
			/// <remarks>
			/// Japanese short name: "誓約1", Google translated: "Pledge 1".
			/// Japanese description: "誓約1", Google translated: "Pledge 1".
			/// </remarks>
			[ParameterTableRowAttribute("vowType1:1", index: 157, minimum: 0, maximum: 1, step: 1, order: 11600, unknown2: 1)]
			[DisplayName("Pledge 1")]
			[Description("Pledge 1")]
			[DefaultValue(false)]
			public Boolean VowType1 {
				get { return GetBitProperty(49, 1, VowType1Property) != 0; }
				set { SetBitProperty(49, 1, value ? 1 : 0, VowType1Property); }
			}

			/// <summary>Pledge 2</summary>
			/// <remarks>
			/// Japanese short name: "誓約2", Google translated: "Pledge 2".
			/// Japanese description: "誓約2", Google translated: "Pledge 2".
			/// </remarks>
			[ParameterTableRowAttribute("vowType2:1", index: 158, minimum: 0, maximum: 1, step: 1, order: 11700, unknown2: 1)]
			[DisplayName("Pledge 2")]
			[Description("Pledge 2")]
			[DefaultValue(false)]
			public Boolean VowType2 {
				get { return GetBitProperty(50, 1, VowType2Property) != 0; }
				set { SetBitProperty(50, 1, value ? 1 : 0, VowType2Property); }
			}

			/// <summary>Pledge 3</summary>
			/// <remarks>
			/// Japanese short name: "誓約3", Google translated: "Pledge 3".
			/// Japanese description: "誓約3", Google translated: "Pledge 3".
			/// </remarks>
			[ParameterTableRowAttribute("vowType3:1", index: 159, minimum: 0, maximum: 1, step: 1, order: 11800, unknown2: 1)]
			[DisplayName("Pledge 3")]
			[Description("Pledge 3")]
			[DefaultValue(false)]
			public Boolean VowType3 {
				get { return GetBitProperty(51, 1, VowType3Property) != 0; }
				set { SetBitProperty(51, 1, value ? 1 : 0, VowType3Property); }
			}

			/// <summary>Pledge 4</summary>
			/// <remarks>
			/// Japanese short name: "誓約4", Google translated: "Pledge 4".
			/// Japanese description: "誓約4", Google translated: "Pledge 4".
			/// </remarks>
			[ParameterTableRowAttribute("vowType4:1", index: 160, minimum: 0, maximum: 1, step: 1, order: 11900, unknown2: 1)]
			[DisplayName("Pledge 4")]
			[Description("Pledge 4")]
			[DefaultValue(false)]
			public Boolean VowType4 {
				get { return GetBitProperty(52, 1, VowType4Property) != 0; }
				set { SetBitProperty(52, 1, value ? 1 : 0, VowType4Property); }
			}

			/// <summary>Pledge 5</summary>
			/// <remarks>
			/// Japanese short name: "誓約5", Google translated: "Pledge 5".
			/// Japanese description: "誓約5", Google translated: "Pledge 5".
			/// </remarks>
			[ParameterTableRowAttribute("vowType5:1", index: 161, minimum: 0, maximum: 1, step: 1, order: 12000, unknown2: 1)]
			[DisplayName("Pledge 5")]
			[Description("Pledge 5")]
			[DefaultValue(false)]
			public Boolean VowType5 {
				get { return GetBitProperty(53, 1, VowType5Property) != 0; }
				set { SetBitProperty(53, 1, value ? 1 : 0, VowType5Property); }
			}

			/// <summary>Pledge 6</summary>
			/// <remarks>
			/// Japanese short name: "誓約6", Google translated: "Pledge 6".
			/// Japanese description: "誓約6", Google translated: "Pledge 6".
			/// </remarks>
			[ParameterTableRowAttribute("vowType6:1", index: 162, minimum: 0, maximum: 1, step: 1, order: 12100, unknown2: 1)]
			[DisplayName("Pledge 6")]
			[Description("Pledge 6")]
			[DefaultValue(false)]
			public Boolean VowType6 {
				get { return GetBitProperty(54, 1, VowType6Property) != 0; }
				set { SetBitProperty(54, 1, value ? 1 : 0, VowType6Property); }
			}

			/// <summary>Pledge 7</summary>
			/// <remarks>
			/// Japanese short name: "誓約7", Google translated: "Pledge 7".
			/// Japanese description: "誓約7", Google translated: "Pledge 7".
			/// </remarks>
			[ParameterTableRowAttribute("vowType7:1", index: 163, minimum: 0, maximum: 1, step: 1, order: 12200, unknown2: 1)]
			[DisplayName("Pledge 7")]
			[Description("Pledge 7")]
			[DefaultValue(false)]
			public Boolean VowType7 {
				get { return GetBitProperty(55, 1, VowType7Property) != 0; }
				set { SetBitProperty(55, 1, value ? 1 : 0, VowType7Property); }
			}

			/// <summary>Pledge 8</summary>
			/// <remarks>
			/// Japanese short name: "誓約8", Google translated: "Pledge 8".
			/// Japanese description: "誓約8", Google translated: "Pledge 8".
			/// </remarks>
			[ParameterTableRowAttribute("vowType8:1", index: 164, minimum: 0, maximum: 1, step: 1, order: 12300, unknown2: 1)]
			[DisplayName("Pledge 8")]
			[Description("Pledge 8")]
			[DefaultValue(false)]
			public Boolean VowType8 {
				get { return GetBitProperty(56, 1, VowType8Property) != 0; }
				set { SetBitProperty(56, 1, value ? 1 : 0, VowType8Property); }
			}

			/// <summary>Pledge 9</summary>
			/// <remarks>
			/// Japanese short name: "誓約9", Google translated: "Pledge 9".
			/// Japanese description: "誓約9", Google translated: "Pledge 9".
			/// </remarks>
			[ParameterTableRowAttribute("vowType9:1", index: 165, minimum: 0, maximum: 1, step: 1, order: 12400, unknown2: 1)]
			[DisplayName("Pledge 9")]
			[Description("Pledge 9")]
			[DefaultValue(false)]
			public Boolean VowType9 {
				get { return GetBitProperty(57, 1, VowType9Property) != 0; }
				set { SetBitProperty(57, 1, value ? 1 : 0, VowType9Property); }
			}

			/// <summary>Pledge 10</summary>
			/// <remarks>
			/// Japanese short name: "誓約10", Google translated: "Pledge 10".
			/// Japanese description: "誓約10", Google translated: "Pledge 10".
			/// </remarks>
			[ParameterTableRowAttribute("vowType10:1", index: 166, minimum: 0, maximum: 1, step: 1, order: 12500, unknown2: 1)]
			[DisplayName("Pledge 10")]
			[Description("Pledge 10")]
			[DefaultValue(false)]
			public Boolean VowType10 {
				get { return GetBitProperty(58, 1, VowType10Property) != 0; }
				set { SetBitProperty(58, 1, value ? 1 : 0, VowType10Property); }
			}

			/// <summary>Pledge 11</summary>
			/// <remarks>
			/// Japanese short name: "誓約11", Google translated: "Pledge 11".
			/// Japanese description: "誓約11", Google translated: "Pledge 11".
			/// </remarks>
			[ParameterTableRowAttribute("vowType11:1", index: 167, minimum: 0, maximum: 1, step: 1, order: 12600, unknown2: 1)]
			[DisplayName("Pledge 11")]
			[Description("Pledge 11")]
			[DefaultValue(false)]
			public Boolean VowType11 {
				get { return GetBitProperty(59, 1, VowType11Property) != 0; }
				set { SetBitProperty(59, 1, value ? 1 : 0, VowType11Property); }
			}

			/// <summary>Pledge 12</summary>
			/// <remarks>
			/// Japanese short name: "誓約12", Google translated: "Pledge 12".
			/// Japanese description: "誓約12", Google translated: "Pledge 12".
			/// </remarks>
			[ParameterTableRowAttribute("vowType12:1", index: 168, minimum: 0, maximum: 1, step: 1, order: 12700, unknown2: 1)]
			[DisplayName("Pledge 12")]
			[Description("Pledge 12")]
			[DefaultValue(false)]
			public Boolean VowType12 {
				get { return GetBitProperty(60, 1, VowType12Property) != 0; }
				set { SetBitProperty(60, 1, value ? 1 : 0, VowType12Property); }
			}

			/// <summary>Pledge 13</summary>
			/// <remarks>
			/// Japanese short name: "誓約13", Google translated: "Pledge 13".
			/// Japanese description: "誓約13", Google translated: "Pledge 13".
			/// </remarks>
			[ParameterTableRowAttribute("vowType13:1", index: 169, minimum: 0, maximum: 1, step: 1, order: 12800, unknown2: 1)]
			[DisplayName("Pledge 13")]
			[Description("Pledge 13")]
			[DefaultValue(false)]
			public Boolean VowType13 {
				get { return GetBitProperty(61, 1, VowType13Property) != 0; }
				set { SetBitProperty(61, 1, value ? 1 : 0, VowType13Property); }
			}

			/// <summary>Pledge 14</summary>
			/// <remarks>
			/// Japanese short name: "誓約14", Google translated: "Pledge 14".
			/// Japanese description: "誓約14", Google translated: "Pledge 14".
			/// </remarks>
			[ParameterTableRowAttribute("vowType14:1", index: 170, minimum: 0, maximum: 1, step: 1, order: 12900, unknown2: 1)]
			[DisplayName("Pledge 14")]
			[Description("Pledge 14")]
			[DefaultValue(false)]
			public Boolean VowType14 {
				get { return GetBitProperty(62, 1, VowType14Property) != 0; }
				set { SetBitProperty(62, 1, value ? 1 : 0, VowType14Property); }
			}

			/// <summary>Pledge 15</summary>
			/// <remarks>
			/// Japanese short name: "誓約15", Google translated: "Pledge 15".
			/// Japanese description: "誓約15", Google translated: "Pledge 15".
			/// </remarks>
			[ParameterTableRowAttribute("vowType15:1", index: 171, minimum: 0, maximum: 1, step: 1, order: 13000, unknown2: 1)]
			[DisplayName("Pledge 15")]
			[Description("Pledge 15")]
			[DefaultValue(false)]
			public Boolean VowType15 {
				get { return GetBitProperty(63, 1, VowType15Property) != 0; }
				set { SetBitProperty(63, 1, value ? 1 : 0, VowType15Property); }
			}

			/// <summary>pad</summary>
			/// <remarks>
			/// Japanese short name: "pad", Google translated: "pad".
			/// Japanese description: "pad", Google translated: "pad".
			/// </remarks>
			[ParameterTableRowAttribute("pad1[11]", index: 172, minimum: 0, maximum: 0, step: 0, order: 13002, unknown2: 0)]
			[DisplayName("pad")]
			[Description("pad")]
			[Browsable(false)]
			public Byte[] Pad1 {
				get { return pad1; }
				set { SetProperty(ref pad1, ref value, Pad1Property); }
			}

			internal SpecialEffect(ParameterTable table, int index, AssetLoader loader, int next)
				: base(table, index, loader) {
				BinaryReader reader = loader.Reader;

				IconId = reader.ReadInt32();
				ConditionHp = reader.ReadSingle();
				EffectEndurance = reader.ReadSingle();
				MotionInterval = reader.ReadSingle();
				MaxHpRate = reader.ReadSingle();
				MaxMpRate = reader.ReadSingle();
				MaxStaminaRate = reader.ReadSingle();
				SlashDamageCutRate = reader.ReadSingle();
				BlowDamageCutRate = reader.ReadSingle();
				ThrustDamageCutRate = reader.ReadSingle();
				NeutralDamageCutRate = reader.ReadSingle();
				MagicDamageCutRate = reader.ReadSingle();
				FireDamageCutRate = reader.ReadSingle();
				ThunderDamageCutRate = reader.ReadSingle();
				PhysicsAttackRate = reader.ReadSingle();
				MagicAttackRate = reader.ReadSingle();
				FireAttackRate = reader.ReadSingle();
				ThunderAttackRate = reader.ReadSingle();
				PhysicsAttackPowerRate = reader.ReadSingle();
				MagicAttackPowerRate = reader.ReadSingle();
				FireAttackPowerRate = reader.ReadSingle();
				ThunderAttackPowerRate = reader.ReadSingle();
				PhysicsAttackPower = reader.ReadInt32();
				MagicAttackPower = reader.ReadInt32();
				FireAttackPower = reader.ReadInt32();
				ThunderAttackPower = reader.ReadInt32();
				PhysicsDiffenceRate = reader.ReadSingle();
				MagicDiffenceRate = reader.ReadSingle();
				FireDiffenceRate = reader.ReadSingle();
				ThunderDiffenceRate = reader.ReadSingle();
				PhysicsDiffence = reader.ReadInt32();
				MagicDiffence = reader.ReadInt32();
				FireDiffence = reader.ReadInt32();
				ThunderDiffence = reader.ReadInt32();
				NoGuardDamageRate = reader.ReadSingle();
				VitalSpotChangeRate = reader.ReadSingle();
				NormalSpotChangeRate = reader.ReadSingle();
				MaxHpChangeRate = reader.ReadSingle();
				BehaviorId = reader.ReadInt32();
				ChangeHpRate = reader.ReadSingle();
				ChangeHpPoint = reader.ReadInt32();
				ChangeMpRate = reader.ReadSingle();
				ChangeMpPoint = reader.ReadInt32();
				MpRecoverChangeSpeed = reader.ReadInt32();
				ChangeStaminaRate = reader.ReadSingle();
				ChangeStaminaPoint = reader.ReadInt32();
				StaminaRecoverChangeSpeed = reader.ReadInt32();
				MagicEffectTimeChange = reader.ReadSingle();
				InsideDurability = reader.ReadInt32();
				MaxDurability = reader.ReadInt32();
				StaminaAttackRate = reader.ReadSingle();
				PoizonAttackPower = reader.ReadInt32();
				RegistIllness = reader.ReadInt32();
				RegistBlood = reader.ReadInt32();
				RegistCurse = reader.ReadInt32();
				FallDamageRate = reader.ReadSingle();
				SoulRate = reader.ReadSingle();
				EquipWeightChangeRate = reader.ReadSingle();
				AllItemWeightChangeRate = reader.ReadSingle();
				Soul = reader.ReadInt32();
				AnimIdOffset = reader.ReadInt32();
				HaveSoulRate = reader.ReadSingle();
				TargetPriority = reader.ReadSingle();
				SightSearchEnemyCut = reader.ReadInt32();
				HearingSearchEnemyCut = reader.ReadInt32();
				GrabityRate = reader.ReadSingle();
				RegistPoizonChangeRate = reader.ReadSingle();
				RegistIllnessChangeRate = reader.ReadSingle();
				RegistBloodChangeRate = reader.ReadSingle();
				RegistCurseChangeRate = reader.ReadSingle();
				SoulStealRate = reader.ReadSingle();
				LifeReductionRate = reader.ReadSingle();
				HpRecoverRate = reader.ReadSingle();
				ReplaceSpEffectId = reader.ReadInt32();
				CycleOccurrenceSpEffectId = reader.ReadInt32();
				AtkOccurrenceSpEffectId = reader.ReadInt32();
				GuardDefFlickPowerRate = reader.ReadSingle();
				GuardStaminaCutRate = reader.ReadSingle();
				RayCastPassedTime = reader.ReadInt16();
				ChangeSuperArmorPoint = reader.ReadInt16();
				BowDistRate = reader.ReadInt16();
				SpCategory = (SpecialEffectSpCategory)reader.ReadUInt16();
				CategoryPriority = reader.ReadByte();
				SaveCategory = (SpecialEffectSaveCategory)reader.ReadSByte();
				ChangeMagicSlot = reader.ReadByte();
				ChangeMiracleSlot = reader.ReadByte();
				HeroPointDamage = reader.ReadSByte();
				DefFlickPower = reader.ReadByte();
				FlickDamageCutRate = reader.ReadByte();
				BloodDamageRate = reader.ReadByte();
				DmgLv_None = (ATKPARAM_REP_DMGTYPE)reader.ReadSByte();
				DmgLv_S = (ATKPARAM_REP_DMGTYPE)reader.ReadSByte();
				DmgLv_M = (ATKPARAM_REP_DMGTYPE)reader.ReadSByte();
				DmgLv_L = (ATKPARAM_REP_DMGTYPE)reader.ReadSByte();
				DmgLv_BlowM = (ATKPARAM_REP_DMGTYPE)reader.ReadSByte();
				DmgLv_Push = (ATKPARAM_REP_DMGTYPE)reader.ReadSByte();
				DmgLv_Strike = (ATKPARAM_REP_DMGTYPE)reader.ReadSByte();
				DmgLv_BlowS = (ATKPARAM_REP_DMGTYPE)reader.ReadSByte();
				DmgLv_Min = (ATKPARAM_REP_DMGTYPE)reader.ReadSByte();
				DmgLv_Uppercut = (ATKPARAM_REP_DMGTYPE)reader.ReadSByte();
				DmgLv_BlowLL = (ATKPARAM_REP_DMGTYPE)reader.ReadSByte();
				DmgLv_Breath = (ATKPARAM_REP_DMGTYPE)reader.ReadSByte();
				AtkAttribute = (AttackAttackAttributes)reader.ReadByte();
				SpAttribute = (AttackParameterSpecialAttributes)reader.ReadByte();
				StateInfo = (SpecialEffectType)reader.ReadByte();
				WepParamChange = (SpecialEffectWeaponChange)reader.ReadByte();
				MoveType = (SpecialEffectMoveType)reader.ReadByte();
				LifeReductionType = (SpecialEffectType)reader.ReadByte();
				ThrowCondition = (SpecialEffectThrowCondition)reader.ReadByte();
				AddBehaviorJudgeId_condition = reader.ReadSByte();
				AddBehaviorJudgeId_add = reader.ReadByte();
				BitFields = reader.ReadBytes(8);
				Pad1 = reader.ReadBytes(11);
			}

			internal SpecialEffect(ParameterTable table, int index)
				: base(table, index) {
				BitFields = new byte[8];
				IconId = (Int32)(-1);
				ConditionHp = (Single)(-1);
				EffectEndurance = (Single)0;
				MotionInterval = (Single)0;
				MaxHpRate = (Single)1;
				MaxMpRate = (Single)1;
				MaxStaminaRate = (Single)1;
				SlashDamageCutRate = (Single)1;
				BlowDamageCutRate = (Single)1;
				ThrustDamageCutRate = (Single)1;
				NeutralDamageCutRate = (Single)1;
				MagicDamageCutRate = (Single)1;
				FireDamageCutRate = (Single)1;
				ThunderDamageCutRate = (Single)1;
				PhysicsAttackRate = (Single)1;
				MagicAttackRate = (Single)1;
				FireAttackRate = (Single)1;
				ThunderAttackRate = (Single)1;
				PhysicsAttackPowerRate = (Single)1;
				MagicAttackPowerRate = (Single)1;
				FireAttackPowerRate = (Single)1;
				ThunderAttackPowerRate = (Single)1;
				PhysicsAttackPower = (Int32)0;
				MagicAttackPower = (Int32)0;
				FireAttackPower = (Int32)0;
				ThunderAttackPower = (Int32)0;
				PhysicsDiffenceRate = (Single)1;
				MagicDiffenceRate = (Single)1;
				FireDiffenceRate = (Single)1;
				ThunderDiffenceRate = (Single)1;
				PhysicsDiffence = (Int32)0;
				MagicDiffence = (Int32)0;
				FireDiffence = (Int32)0;
				ThunderDiffence = (Int32)0;
				NoGuardDamageRate = (Single)1;
				VitalSpotChangeRate = (Single)(-1);
				NormalSpotChangeRate = (Single)(-1);
				MaxHpChangeRate = (Single)0;
				BehaviorId = (Int32)(-1);
				ChangeHpRate = (Single)0;
				ChangeHpPoint = (Int32)0;
				ChangeMpRate = (Single)0;
				ChangeMpPoint = (Int32)0;
				MpRecoverChangeSpeed = (Int32)0;
				ChangeStaminaRate = (Single)0;
				ChangeStaminaPoint = (Int32)0;
				StaminaRecoverChangeSpeed = (Int32)0;
				MagicEffectTimeChange = (Single)0;
				InsideDurability = (Int32)0;
				MaxDurability = (Int32)0;
				StaminaAttackRate = (Single)1;
				PoizonAttackPower = (Int32)0;
				RegistIllness = (Int32)0;
				RegistBlood = (Int32)0;
				RegistCurse = (Int32)0;
				FallDamageRate = (Single)0;
				SoulRate = (Single)0;
				EquipWeightChangeRate = (Single)0;
				AllItemWeightChangeRate = (Single)0;
				Soul = (Int32)0;
				AnimIdOffset = (Int32)0;
				HaveSoulRate = (Single)1;
				TargetPriority = (Single)0;
				SightSearchEnemyCut = (Int32)0;
				HearingSearchEnemyCut = (Int32)0;
				GrabityRate = (Single)1;
				RegistPoizonChangeRate = (Single)0;
				RegistIllnessChangeRate = (Single)0;
				RegistBloodChangeRate = (Single)0;
				RegistCurseChangeRate = (Single)0;
				SoulStealRate = (Single)0;
				LifeReductionRate = (Single)0;
				HpRecoverRate = (Single)0;
				ReplaceSpEffectId = (Int32)(-1);
				CycleOccurrenceSpEffectId = (Int32)(-1);
				AtkOccurrenceSpEffectId = (Int32)(-1);
				GuardDefFlickPowerRate = (Single)1;
				GuardStaminaCutRate = (Single)1;
				RayCastPassedTime = (Int16)(-1);
				ChangeSuperArmorPoint = (Int16)0;
				BowDistRate = (Int16)0;
				SpCategory = (SpecialEffectSpCategory)0;
				CategoryPriority = (Byte)0;
				SaveCategory = (SpecialEffectSaveCategory)(-1);
				ChangeMagicSlot = (Byte)0;
				ChangeMiracleSlot = (Byte)0;
				HeroPointDamage = (SByte)0;
				DefFlickPower = (Byte)0;
				FlickDamageCutRate = (Byte)0;
				BloodDamageRate = (Byte)100;
				DmgLv_None = (ATKPARAM_REP_DMGTYPE)0;
				DmgLv_S = (ATKPARAM_REP_DMGTYPE)0;
				DmgLv_M = (ATKPARAM_REP_DMGTYPE)0;
				DmgLv_L = (ATKPARAM_REP_DMGTYPE)0;
				DmgLv_BlowM = (ATKPARAM_REP_DMGTYPE)0;
				DmgLv_Push = (ATKPARAM_REP_DMGTYPE)0;
				DmgLv_Strike = (ATKPARAM_REP_DMGTYPE)0;
				DmgLv_BlowS = (ATKPARAM_REP_DMGTYPE)0;
				DmgLv_Min = (ATKPARAM_REP_DMGTYPE)0;
				DmgLv_Uppercut = (ATKPARAM_REP_DMGTYPE)0;
				DmgLv_BlowLL = (ATKPARAM_REP_DMGTYPE)0;
				DmgLv_Breath = (ATKPARAM_REP_DMGTYPE)0;
				AtkAttribute = (AttackAttackAttributes)0;
				SpAttribute = (AttackParameterSpecialAttributes)0;
				StateInfo = (SpecialEffectType)0;
				WepParamChange = (SpecialEffectWeaponChange)0;
				MoveType = (SpecialEffectMoveType)0;
				LifeReductionType = (SpecialEffectType)0;
				ThrowCondition = (SpecialEffectThrowCondition)0;
				AddBehaviorJudgeId_condition = (SByte)(-1);
				AddBehaviorJudgeId_add = (Byte)0;
				EffectTargetSelf = false;
				EffectTargetFriend = false;
				EffectTargetEnemy = false;
				EffectTargetPlayer = false;
				EffectTargetAI = false;
				EffectTargetLive = false;
				EffectTargetGhost = false;
				EffectTargetWhiteGhost = false;
				EffectTargetBlackGhost = false;
				EffectTargetAttacker = false;
				DispIconNonactive = false;
				UseSpEffectEffect = false;
				BAdjustMagicAblity = false;
				BAdjustFaithAblity = false;
				BGameClearBonus = false;
				MagParamChange = false;
				MiracleParamChange = false;
				ClearSoul = false;
				RequestSOS = false;
				RequestBlackSOS = false;
				RequestForceJoinBlackSOS = false;
				RequestKickSession = false;
				RequestLeaveSession = false;
				RequestNpcInveda = false;
				NoDead = false;
				BCurrHPIndependeMaxHP = false;
				CorrosionIgnore = false;
				SightSearchCutIgnore = false;
				HearingSearchCutIgnore = false;
				AntiMagicIgnore = false;
				FakeTargetIgnore = false;
				FakeTargetIgnoreUndead = false;
				FakeTargetIgnoreAnimal = false;
				GrabityIgnore = false;
				DisablePoison = false;
				DisableDisease = false;
				DisableBlood = false;
				DisableCurse = false;
				EnableCharm = false;
				EnableLifeTime = false;
				HasTarget = false;
				IsFireDamageCancel = false;
				IsExtendSpEffectLife = false;
				RequestLeaveColiseumSession = false;
				Pad_2 = (Byte)0;
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
				Pad1 = new Byte[11];
			}

			public override void Write(BinaryWriter writer) {
				writer.Write(IconId);
				writer.Write(ConditionHp);
				writer.Write(EffectEndurance);
				writer.Write(MotionInterval);
				writer.Write(MaxHpRate);
				writer.Write(MaxMpRate);
				writer.Write(MaxStaminaRate);
				writer.Write(SlashDamageCutRate);
				writer.Write(BlowDamageCutRate);
				writer.Write(ThrustDamageCutRate);
				writer.Write(NeutralDamageCutRate);
				writer.Write(MagicDamageCutRate);
				writer.Write(FireDamageCutRate);
				writer.Write(ThunderDamageCutRate);
				writer.Write(PhysicsAttackRate);
				writer.Write(MagicAttackRate);
				writer.Write(FireAttackRate);
				writer.Write(ThunderAttackRate);
				writer.Write(PhysicsAttackPowerRate);
				writer.Write(MagicAttackPowerRate);
				writer.Write(FireAttackPowerRate);
				writer.Write(ThunderAttackPowerRate);
				writer.Write(PhysicsAttackPower);
				writer.Write(MagicAttackPower);
				writer.Write(FireAttackPower);
				writer.Write(ThunderAttackPower);
				writer.Write(PhysicsDiffenceRate);
				writer.Write(MagicDiffenceRate);
				writer.Write(FireDiffenceRate);
				writer.Write(ThunderDiffenceRate);
				writer.Write(PhysicsDiffence);
				writer.Write(MagicDiffence);
				writer.Write(FireDiffence);
				writer.Write(ThunderDiffence);
				writer.Write(NoGuardDamageRate);
				writer.Write(VitalSpotChangeRate);
				writer.Write(NormalSpotChangeRate);
				writer.Write(MaxHpChangeRate);
				writer.Write(BehaviorId);
				writer.Write(ChangeHpRate);
				writer.Write(ChangeHpPoint);
				writer.Write(ChangeMpRate);
				writer.Write(ChangeMpPoint);
				writer.Write(MpRecoverChangeSpeed);
				writer.Write(ChangeStaminaRate);
				writer.Write(ChangeStaminaPoint);
				writer.Write(StaminaRecoverChangeSpeed);
				writer.Write(MagicEffectTimeChange);
				writer.Write(InsideDurability);
				writer.Write(MaxDurability);
				writer.Write(StaminaAttackRate);
				writer.Write(PoizonAttackPower);
				writer.Write(RegistIllness);
				writer.Write(RegistBlood);
				writer.Write(RegistCurse);
				writer.Write(FallDamageRate);
				writer.Write(SoulRate);
				writer.Write(EquipWeightChangeRate);
				writer.Write(AllItemWeightChangeRate);
				writer.Write(Soul);
				writer.Write(AnimIdOffset);
				writer.Write(HaveSoulRate);
				writer.Write(TargetPriority);
				writer.Write(SightSearchEnemyCut);
				writer.Write(HearingSearchEnemyCut);
				writer.Write(GrabityRate);
				writer.Write(RegistPoizonChangeRate);
				writer.Write(RegistIllnessChangeRate);
				writer.Write(RegistBloodChangeRate);
				writer.Write(RegistCurseChangeRate);
				writer.Write(SoulStealRate);
				writer.Write(LifeReductionRate);
				writer.Write(HpRecoverRate);
				writer.Write(ReplaceSpEffectId);
				writer.Write(CycleOccurrenceSpEffectId);
				writer.Write(AtkOccurrenceSpEffectId);
				writer.Write(GuardDefFlickPowerRate);
				writer.Write(GuardStaminaCutRate);
				writer.Write(RayCastPassedTime);
				writer.Write(ChangeSuperArmorPoint);
				writer.Write(BowDistRate);
				writer.Write((UInt16)SpCategory);
				writer.Write(CategoryPriority);
				writer.Write((SByte)SaveCategory);
				writer.Write(ChangeMagicSlot);
				writer.Write(ChangeMiracleSlot);
				writer.Write(HeroPointDamage);
				writer.Write(DefFlickPower);
				writer.Write(FlickDamageCutRate);
				writer.Write(BloodDamageRate);
				writer.Write((SByte)DmgLv_None);
				writer.Write((SByte)DmgLv_S);
				writer.Write((SByte)DmgLv_M);
				writer.Write((SByte)DmgLv_L);
				writer.Write((SByte)DmgLv_BlowM);
				writer.Write((SByte)DmgLv_Push);
				writer.Write((SByte)DmgLv_Strike);
				writer.Write((SByte)DmgLv_BlowS);
				writer.Write((SByte)DmgLv_Min);
				writer.Write((SByte)DmgLv_Uppercut);
				writer.Write((SByte)DmgLv_BlowLL);
				writer.Write((SByte)DmgLv_Breath);
				writer.Write((Byte)AtkAttribute);
				writer.Write((Byte)SpAttribute);
				writer.Write((Byte)StateInfo);
				writer.Write((Byte)WepParamChange);
				writer.Write((Byte)MoveType);
				writer.Write((Byte)LifeReductionType);
				writer.Write((Byte)ThrowCondition);
				writer.Write(AddBehaviorJudgeId_condition);
				writer.Write(AddBehaviorJudgeId_add);
				writer.Write(BitFields);
				writer.Write(Pad1);
			}
		}
	}
}
