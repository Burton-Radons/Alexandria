/*
For ParameterTable.cs under ParameterTableRow.ReadRow:
	case TableRows.NPC_PARAM_ST.TableName: return new TableRows.NPC_PARAM_ST(table, index, loader, next);
For ParameterDefinition.cs under ParameterDefinitionRow.GetDotNetType():
For Enumerations.cs:*/
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
		/// Defined as "NPC_PARAM_ST" in Dark Souls in the file "NpcParam.paramdef" (id 18h).
		/// </remarks>
		public class Npc : ParameterTableRow {
			/// <summary>The name of the table in the file.</summary>
			public const string TableName = "NPC_PARAM_ST";

			Int32 behaviorVariationId, aiThinkId, nameId, itemLotId_1, itemLotId_2, itemLotId_3, itemLotId_4, itemLotId_5, itemLotId_6, humanityLotId, spEffectID0, spEffectID1, spEffectID2, spEffectID3, spEffectID4, spEffectID5, spEffectID6, spEffectID7, gameClearSpEffectID, animIdOffset, moveAnimId, spMoveAnimId1, spMoveAnimId2, dbgBehaviorR1, dbgBehaviorL1, dbgBehaviorR2, dbgBehaviorL2, dbgBehaviorRL, dbgBehaviorRR, dbgBehaviorRD, dbgBehaviorRU, dbgBehaviorLL, dbgBehaviorLR, dbgBehaviorLD, dbgBehaviorLU, animIdOffset2;
			Single turnVellocity, hitHeight, hitRadius, hitYOffset, physGuardCutRate, magGuardCutRate, fireGuardCutRate, thunGuardCutRate, networkWarpDist, partsDamageRate1, partsDamageRate2, partsDamageRate3, partsDamageRate4, partsDamageRate5, partsDamageRate6, partsDamageRate7, partsDamageRate8, weakPartsDamageRate, superArmorRecoverCorrection, superArmorBrakeKnockbackDist;
			UInt32 weight, hp, mp, getSoul;
			UInt16 stamina, staminaRecoverBaseVel, def_phys, def_mag, def_fire, def_thunder, defFlickPower, resistPoison, resistDisease, resistBleed, resistCurse;
			Int16 def_slash, def_blow, def_thrust, ghostModelId, normalChangeResouceId, guardAngle, slashGuardCutRate, blowGuardCutRate, thrustGuardCutRate, superArmorDurability, normalChangeTexChrId;
			NpcItemDropType dropType;
			Byte knockbackRate, knockbackParamId, fallDamageDump, staminaGuardDef, pcAttrB, pcAttrW, pcAttrL, pcAttrR, areaAttrB, areaAttrW, areaAttrL, areaAttrR, mpRecoverBaseVel, flickDamageCutRate, lockDist, maxUndurationAng, parryAttack, parryDefence, pushOutCamRegionRadius, ladderEndChkOffsetTop, ladderEndChkOffsetLow;
			SByte defaultLodParamId, guardLevel, poisonGuardResist, diseaseGuardResist, bloodGuardResist, curseGuardResist;
			NpcDrawType drawType;
			NpcType npcType;
			NpcTemaType teamType;
			NpcMoveType moveType;
			WeaponMaterialDefend material, material_Weak;
			WeaponMaterialDefendSound materialSfx, materialSfx_Weak;
			AttackParameterPartDamageType partsDamageType;
			NpcBurnType burnSfxType;
			NpcSfxSize sfxSize;
			NpcHitStopType hitStopType;
			Byte[] pad2;

			/// <summary>A property in the class.</summary>
			public static readonly PropertyInfo
				BehaviorVariationIdProperty = GetProperty<Npc>("BehaviorVariationId"),
				AiThinkIdProperty = GetProperty<Npc>("AiThinkId"),
				NameIdProperty = GetProperty<Npc>("NameId"),
				TurnVellocityProperty = GetProperty<Npc>("TurnVellocity"),
				HitHeightProperty = GetProperty<Npc>("HitHeight"),
				HitRadiusProperty = GetProperty<Npc>("HitRadius"),
				WeightProperty = GetProperty<Npc>("Weight"),
				HitYOffsetProperty = GetProperty<Npc>("HitYOffset"),
				HpProperty = GetProperty<Npc>("Hp"),
				MpProperty = GetProperty<Npc>("Mp"),
				GetSoulProperty = GetProperty<Npc>("GetSoul"),
				ItemLotId_1Property = GetProperty<Npc>("ItemLotId_1"),
				ItemLotId_2Property = GetProperty<Npc>("ItemLotId_2"),
				ItemLotId_3Property = GetProperty<Npc>("ItemLotId_3"),
				ItemLotId_4Property = GetProperty<Npc>("ItemLotId_4"),
				ItemLotId_5Property = GetProperty<Npc>("ItemLotId_5"),
				ItemLotId_6Property = GetProperty<Npc>("ItemLotId_6"),
				HumanityLotIdProperty = GetProperty<Npc>("HumanityLotId"),
				SpEffectID0Property = GetProperty<Npc>("SpEffectID0"),
				SpEffectID1Property = GetProperty<Npc>("SpEffectID1"),
				SpEffectID2Property = GetProperty<Npc>("SpEffectID2"),
				SpEffectID3Property = GetProperty<Npc>("SpEffectID3"),
				SpEffectID4Property = GetProperty<Npc>("SpEffectID4"),
				SpEffectID5Property = GetProperty<Npc>("SpEffectID5"),
				SpEffectID6Property = GetProperty<Npc>("SpEffectID6"),
				SpEffectID7Property = GetProperty<Npc>("SpEffectID7"),
				GameClearSpEffectIDProperty = GetProperty<Npc>("GameClearSpEffectID"),
				PhysGuardCutRateProperty = GetProperty<Npc>("PhysGuardCutRate"),
				MagGuardCutRateProperty = GetProperty<Npc>("MagGuardCutRate"),
				FireGuardCutRateProperty = GetProperty<Npc>("FireGuardCutRate"),
				ThunGuardCutRateProperty = GetProperty<Npc>("ThunGuardCutRate"),
				AnimIdOffsetProperty = GetProperty<Npc>("AnimIdOffset"),
				MoveAnimIdProperty = GetProperty<Npc>("MoveAnimId"),
				SpMoveAnimId1Property = GetProperty<Npc>("SpMoveAnimId1"),
				SpMoveAnimId2Property = GetProperty<Npc>("SpMoveAnimId2"),
				NetworkWarpDistProperty = GetProperty<Npc>("NetworkWarpDist"),
				DbgBehaviorR1Property = GetProperty<Npc>("DbgBehaviorR1"),
				DbgBehaviorL1Property = GetProperty<Npc>("DbgBehaviorL1"),
				DbgBehaviorR2Property = GetProperty<Npc>("DbgBehaviorR2"),
				DbgBehaviorL2Property = GetProperty<Npc>("DbgBehaviorL2"),
				DbgBehaviorRLProperty = GetProperty<Npc>("DbgBehaviorRL"),
				DbgBehaviorRRProperty = GetProperty<Npc>("DbgBehaviorRR"),
				DbgBehaviorRDProperty = GetProperty<Npc>("DbgBehaviorRD"),
				DbgBehaviorRUProperty = GetProperty<Npc>("DbgBehaviorRU"),
				DbgBehaviorLLProperty = GetProperty<Npc>("DbgBehaviorLL"),
				DbgBehaviorLRProperty = GetProperty<Npc>("DbgBehaviorLR"),
				DbgBehaviorLDProperty = GetProperty<Npc>("DbgBehaviorLD"),
				DbgBehaviorLUProperty = GetProperty<Npc>("DbgBehaviorLU"),
				AnimIdOffset2Property = GetProperty<Npc>("AnimIdOffset2"),
				PartsDamageRate1Property = GetProperty<Npc>("PartsDamageRate1"),
				PartsDamageRate2Property = GetProperty<Npc>("PartsDamageRate2"),
				PartsDamageRate3Property = GetProperty<Npc>("PartsDamageRate3"),
				PartsDamageRate4Property = GetProperty<Npc>("PartsDamageRate4"),
				PartsDamageRate5Property = GetProperty<Npc>("PartsDamageRate5"),
				PartsDamageRate6Property = GetProperty<Npc>("PartsDamageRate6"),
				PartsDamageRate7Property = GetProperty<Npc>("PartsDamageRate7"),
				PartsDamageRate8Property = GetProperty<Npc>("PartsDamageRate8"),
				WeakPartsDamageRateProperty = GetProperty<Npc>("WeakPartsDamageRate"),
				SuperArmorRecoverCorrectionProperty = GetProperty<Npc>("SuperArmorRecoverCorrection"),
				SuperArmorBrakeKnockbackDistProperty = GetProperty<Npc>("SuperArmorBrakeKnockbackDist"),
				StaminaProperty = GetProperty<Npc>("Stamina"),
				StaminaRecoverBaseVelProperty = GetProperty<Npc>("StaminaRecoverBaseVel"),
				Def_physProperty = GetProperty<Npc>("Def_phys"),
				Def_slashProperty = GetProperty<Npc>("Def_slash"),
				Def_blowProperty = GetProperty<Npc>("Def_blow"),
				Def_thrustProperty = GetProperty<Npc>("Def_thrust"),
				Def_magProperty = GetProperty<Npc>("Def_mag"),
				Def_fireProperty = GetProperty<Npc>("Def_fire"),
				Def_thunderProperty = GetProperty<Npc>("Def_thunder"),
				DefFlickPowerProperty = GetProperty<Npc>("DefFlickPower"),
				ResistPoisonProperty = GetProperty<Npc>("ResistPoison"),
				ResistDiseaseProperty = GetProperty<Npc>("ResistDisease"),
				resistBleedProperty = GetProperty<Npc>("ResistBleed"),
				ResistCurseProperty = GetProperty<Npc>("ResistCurse"),
				GhostModelIdProperty = GetProperty<Npc>("GhostModelId"),
				NormalChangeResouceIdProperty = GetProperty<Npc>("NormalChangeResouceId"),
				GuardAngleProperty = GetProperty<Npc>("GuardAngle"),
				SlashGuardCutRateProperty = GetProperty<Npc>("SlashGuardCutRate"),
				BlowGuardCutRateProperty = GetProperty<Npc>("BlowGuardCutRate"),
				ThrustGuardCutRateProperty = GetProperty<Npc>("ThrustGuardCutRate"),
				SuperArmorDurabilityProperty = GetProperty<Npc>("SuperArmorDurability"),
				NormalChangeTexChrIdProperty = GetProperty<Npc>("NormalChangeTexChrId"),
				DropTypeProperty = GetProperty<Npc>("DropType"),
				KnockbackRateProperty = GetProperty<Npc>("KnockbackRate"),
				KnockbackParamIdProperty = GetProperty<Npc>("KnockbackParamId"),
				FallDamageDumpProperty = GetProperty<Npc>("FallDamageDump"),
				StaminaGuardDefProperty = GetProperty<Npc>("StaminaGuardDef"),
				PcAttrBProperty = GetProperty<Npc>("PcAttrB"),
				PcAttrWProperty = GetProperty<Npc>("PcAttrW"),
				PcAttrLProperty = GetProperty<Npc>("PcAttrL"),
				PcAttrRProperty = GetProperty<Npc>("PcAttrR"),
				AreaAttrBProperty = GetProperty<Npc>("AreaAttrB"),
				AreaAttrWProperty = GetProperty<Npc>("AreaAttrW"),
				AreaAttrLProperty = GetProperty<Npc>("AreaAttrL"),
				AreaAttrRProperty = GetProperty<Npc>("AreaAttrR"),
				MpRecoverBaseVelProperty = GetProperty<Npc>("MpRecoverBaseVel"),
				FlickDamageCutRateProperty = GetProperty<Npc>("FlickDamageCutRate"),
				DefaultLodParamIdProperty = GetProperty<Npc>("DefaultLodParamId"),
				DrawTypeProperty = GetProperty<Npc>("DrawType"),
				NpcTypeProperty = GetProperty<Npc>("NpcType"),
				TeamTypeProperty = GetProperty<Npc>("TeamType"),
				MoveTypeProperty = GetProperty<Npc>("MoveType"),
				LockDistProperty = GetProperty<Npc>("LockDist"),
				MaterialProperty = GetProperty<Npc>("Material"),
				MaterialSfxProperty = GetProperty<Npc>("MaterialSfx"),
				Material_WeakProperty = GetProperty<Npc>("Material_Weak"),
				MaterialSfx_WeakProperty = GetProperty<Npc>("MaterialSfx_Weak"),
				PartsDamageTypeProperty = GetProperty<Npc>("PartsDamageType"),
				MaxUndurationAngProperty = GetProperty<Npc>("MaxUndurationAng"),
				GuardLevelProperty = GetProperty<Npc>("GuardLevel"),
				BurnSfxTypeProperty = GetProperty<Npc>("BurnSfxType"),
				PoisonGuardResistProperty = GetProperty<Npc>("PoisonGuardResist"),
				DiseaseGuardResistProperty = GetProperty<Npc>("DiseaseGuardResist"),
				BloodGuardResistProperty = GetProperty<Npc>("BloodGuardResist"),
				CurseGuardResistProperty = GetProperty<Npc>("CurseGuardResist"),
				ParryAttackProperty = GetProperty<Npc>("ParryAttack"),
				ParryDefenceProperty = GetProperty<Npc>("ParryDefence"),
				SfxSizeProperty = GetProperty<Npc>("SfxSize"),
				PushOutCamRegionRadiusProperty = GetProperty<Npc>("PushOutCamRegionRadius"),
				HitStopTypeProperty = GetProperty<Npc>("HitStopType"),
				LadderEndChkOffsetTopProperty = GetProperty<Npc>("LadderEndChkOffsetTop"),
				LadderEndChkOffsetLowProperty = GetProperty<Npc>("LadderEndChkOffsetLow"),
				UseRagdollCamHitProperty = GetProperty<Npc>("UseRagdollCamHit"),
				DisableClothRigidHitProperty = GetProperty<Npc>("DisableClothRigidHit"),
				UseRagdollProperty = GetProperty<Npc>("UseRagdoll"),
				IsDemonProperty = GetProperty<Npc>("IsDemon"),
				IsGhostProperty = GetProperty<Npc>("IsGhost"),
				IsNoDamageMotionProperty = GetProperty<Npc>("IsNoDamageMotion"),
				IsUndurationProperty = GetProperty<Npc>("IsUnduration"),
				IsChangeWanderGhostProperty = GetProperty<Npc>("IsChangeWanderGhost"),
				ModelDispMask0Property = GetProperty<Npc>("ModelDispMask0"),
				ModelDispMask1Property = GetProperty<Npc>("ModelDispMask1"),
				ModelDispMask2Property = GetProperty<Npc>("ModelDispMask2"),
				ModelDispMask3Property = GetProperty<Npc>("ModelDispMask3"),
				ModelDispMask4Property = GetProperty<Npc>("ModelDispMask4"),
				ModelDispMask5Property = GetProperty<Npc>("ModelDispMask5"),
				ModelDispMask6Property = GetProperty<Npc>("ModelDispMask6"),
				ModelDispMask7Property = GetProperty<Npc>("ModelDispMask7"),
				ModelDispMask8Property = GetProperty<Npc>("ModelDispMask8"),
				ModelDispMask9Property = GetProperty<Npc>("ModelDispMask9"),
				ModelDispMask10Property = GetProperty<Npc>("ModelDispMask10"),
				ModelDispMask11Property = GetProperty<Npc>("ModelDispMask11"),
				ModelDispMask12Property = GetProperty<Npc>("ModelDispMask12"),
				ModelDispMask13Property = GetProperty<Npc>("ModelDispMask13"),
				ModelDispMask14Property = GetProperty<Npc>("ModelDispMask14"),
				ModelDispMask15Property = GetProperty<Npc>("ModelDispMask15"),
				IsEnableNeckTurnProperty = GetProperty<Npc>("IsEnableNeckTurn"),
				DisableRespawnProperty = GetProperty<Npc>("DisableRespawn"),
				IsMoveAnimWaitProperty = GetProperty<Npc>("IsMoveAnimWait"),
				IsCrowdProperty = GetProperty<Npc>("IsCrowd"),
				IsWeakSaintProperty = GetProperty<Npc>("IsWeakSaint"),
				IsWeakAProperty = GetProperty<Npc>("IsWeakA"),
				IsWeakBProperty = GetProperty<Npc>("IsWeakB"),
				Pad1Property = GetProperty<Npc>("Pad1"),
				VowTypeProperty = GetProperty<Npc>("VowType"),
				DisableInitializeDeadProperty = GetProperty<Npc>("DisableInitializeDead"),
				Pad3Property = GetProperty<Npc>("Pad3"),
				Pad2Property = GetProperty<Npc>("Pad2");

			/// <summary>Behavioral variation ID</summary>
			/// <remarks>
			/// Japanese short name: "行動バリエーションID", Google translated: "Behavioral variation ID".
			/// Japanese description: "行動IDを算出するときに使用するバリエーションID.", Google translated: "Variation ID to be used when calculating the action ID.".
			/// </remarks>
			[ParameterTableRowAttribute("behaviorVariationId", index: 0, minimum: 0, maximum: 99999, step: 1, sortOrder: 1000, unknown2: 1)]
			[DisplayName("Behavioral variation ID")]
			[Description("Variation ID to be used when calculating the action ID.")]
			[DefaultValue((Int32)0)]
			public Int32 BehaviorVariationId {
				get { return behaviorVariationId; }
				set {
					if ((double)value < 0 || (double)value > 99999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99999 for " + BehaviorVariationIdProperty.Name + ".");
					SetProperty(ref behaviorVariationId, ref value, BehaviorVariationIdProperty);
				}
			}

			/// <summary>AI thought ID</summary>
			/// <remarks>
			/// Japanese short name: "AI思考ID", Google translated: "AI thought ID".
			/// Japanese description: "使用するAI思考のID.", Google translated: "ID of AI thought to be used .".
			/// </remarks>
			[ParameterTableRowAttribute("aiThinkId", index: 1, minimum: -1, maximum: 999999, step: 1, sortOrder: 2000, unknown2: 1)]
			[DisplayName("AI thought ID")]
			[Description("ID of AI thought to be used .")]
			[DefaultValue((Int32)0)]
			public Int32 AiThinkId {
				get { return aiThinkId; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for " + AiThinkIdProperty.Name + ".");
					SetProperty(ref aiThinkId, ref value, AiThinkIdProperty);
				}
			}

			/// <summary>NPC name ID</summary>
			/// <remarks>
			/// Japanese short name: "NPC名ID", Google translated: "NPC name ID".
			/// Japanese description: "NPC名メッセージパラメータ用ID", Google translated: "NPC name message parameters for ID".
			/// </remarks>
			[ParameterTableRowAttribute("nameId", index: 2, minimum: -1, maximum: 999999, step: 1, sortOrder: 3000, unknown2: 1)]
			[DisplayName("NPC name ID")]
			[Description("NPC name message parameters for ID")]
			[DefaultValue((Int32)(-1))]
			public Int32 NameId {
				get { return nameId; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for " + NameIdProperty.Name + ".");
					SetProperty(ref nameId, ref value, NameIdProperty);
				}
			}

			/// <summary>Swing speed [deg / sec]</summary>
			/// <remarks>
			/// Japanese short name: "旋回速度[deg/sec]", Google translated: "Swing speed [deg / sec]".
			/// Japanese description: "1秒間に旋回できる回転速度[度/秒].", Google translated: "Rotational speed that can be swiveled one second [degrees / sec ] .".
			/// </remarks>
			[ParameterTableRowAttribute("turnVellocity", index: 3, minimum: 0, maximum: 1000000, step: 0.1, sortOrder: 8000, unknown2: 1)]
			[DisplayName("Swing speed [deg / sec]")]
			[Description("Rotational speed that can be swiveled one second [degrees / sec ] .")]
			[DefaultValue((Single)0)]
			public Single TurnVellocity {
				get { return turnVellocity; }
				set {
					if ((double)value < 0 || (double)value > 1000000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1000000 for " + TurnVellocityProperty.Name + ".");
					SetProperty(ref turnVellocity, ref value, TurnVellocityProperty);
				}
			}

			/// <summary>Per height [m]</summary>
			/// <remarks>
			/// Japanese short name: "あたりの高さ[m]", Google translated: "Per height [m]".
			/// Japanese description: "当たりカプセルの高さ.", Google translated: "Height per capsule .".
			/// </remarks>
			[ParameterTableRowAttribute("hitHeight", index: 4, minimum: 0, maximum: 999999, step: 0.1, sortOrder: 13000, unknown2: 1)]
			[DisplayName("Per height [m]")]
			[Description("Height per capsule .")]
			[DefaultValue((Single)0)]
			public Single HitHeight {
				get { return hitHeight; }
				set {
					if ((double)value < 0 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999999 for " + HitHeightProperty.Name + ".");
					SetProperty(ref hitHeight, ref value, HitHeightProperty);
				}
			}

			/// <summary>Radius of around [m]</summary>
			/// <remarks>
			/// Japanese short name: "あたりの半径[m]", Google translated: "Radius of around [m]".
			/// Japanese description: "当たりカプセルの半径.", Google translated: "Radius per capsule .".
			/// </remarks>
			[ParameterTableRowAttribute("hitRadius", index: 5, minimum: 0, maximum: 999999, step: 0.1, sortOrder: 14000, unknown2: 1)]
			[DisplayName("Radius of around [m]")]
			[Description("Radius per capsule .")]
			[DefaultValue((Single)0)]
			public Single HitRadius {
				get { return hitRadius; }
				set {
					if ((double)value < 0 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999999 for " + HitRadiusProperty.Name + ".");
					SetProperty(ref hitRadius, ref value, HitRadiusProperty);
				}
			}

			/// <summary>Weight [kg]</summary>
			/// <remarks>
			/// Japanese short name: "重量[kg]", Google translated: "Weight [kg]".
			/// Japanese description: "重量.", Google translated: "Weight .".
			/// </remarks>
			[ParameterTableRowAttribute("weight", index: 6, minimum: 0, maximum: 999999, step: 1, sortOrder: 15000, unknown2: 1)]
			[DisplayName("Weight [kg]")]
			[Description("Weight .")]
			[DefaultValue((UInt32)0)]
			public UInt32 Weight {
				get { return weight; }
				set {
					if ((double)value < 0 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999999 for " + WeightProperty.Name + ".");
					SetProperty(ref weight, ref value, WeightProperty);
				}
			}

			/// <summary>Y offset display position [m]</summary>
			/// <remarks>
			/// Japanese short name: "表示位置Yオフセット[m]", Google translated: "Y offset display position [m]".
			/// Japanese description: "モデル表示位置のY（高さ）方向のオフセット。あたり位置より浮かせることができる。", Google translated: "Y of model display position offset direction (height) . It is possible to float from the contact position .".
			/// </remarks>
			[ParameterTableRowAttribute("hitYOffset", index: 7, minimum: -100, maximum: 100, step: 0.1, sortOrder: 16000, unknown2: 1)]
			[DisplayName("Y offset display position [m]")]
			[Description("Y of model display position offset direction (height) . It is possible to float from the contact position .")]
			[DefaultValue((Single)0)]
			public Single HitYOffset {
				get { return hitYOffset; }
				set {
					if ((double)value < -100 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -100 to 100 for " + HitYOffsetProperty.Name + ".");
					SetProperty(ref hitYOffset, ref value, HitYOffsetProperty);
				}
			}

			/// <summary>HP</summary>
			/// <remarks>
			/// Japanese short name: "ＨＰ", Google translated: "HP".
			/// Japanese description: "死亡猶予.", Google translated: "Death grace .".
			/// </remarks>
			[ParameterTableRowAttribute("hp", index: 8, minimum: 0, maximum: 99999, step: 1, sortOrder: 21000, unknown2: 1)]
			[DisplayName("HP")]
			[Description("Death grace .")]
			[DefaultValue((UInt32)0)]
			public UInt32 Hp {
				get { return hp; }
				set {
					if ((double)value < 0 || (double)value > 99999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99999 for " + HpProperty.Name + ".");
					SetProperty(ref hp, ref value, HpProperty);
				}
			}

			/// <summary>MP</summary>
			/// <remarks>
			/// Japanese short name: "ＭＰ", Google translated: "MP".
			/// Japanese description: "魔法使用量.", Google translated: "Magic usage .".
			/// </remarks>
			[ParameterTableRowAttribute("mp", index: 9, minimum: 0, maximum: 99999, step: 1, sortOrder: 22000, unknown2: 1)]
			[DisplayName("MP")]
			[Description("Magic usage .")]
			[DefaultValue((UInt32)0)]
			public UInt32 Mp {
				get { return mp; }
				set {
					if ((double)value < 0 || (double)value > 99999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99999 for " + MpProperty.Name + ".");
					SetProperty(ref mp, ref value, MpProperty);
				}
			}

			/// <summary>Seoul</summary>
			/// <remarks>
			/// Japanese short name: "ソウル", Google translated: "Seoul".
			/// Japanese description: "死亡時に、キャラクターが取得できるソウル量.", Google translated: "At the time of death , the amount of Seoul can obtain character .".
			/// </remarks>
			[ParameterTableRowAttribute("getSoul", index: 10, minimum: 0, maximum: 999999, step: 1, sortOrder: 27000, unknown2: 1)]
			[DisplayName("Seoul")]
			[Description("At the time of death , the amount of Seoul can obtain character .")]
			[DefaultValue((UInt32)0)]
			public UInt32 GetSoul {
				get { return getSoul; }
				set {
					if ((double)value < 0 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999999 for " + GetSoulProperty.Name + ".");
					SetProperty(ref getSoul, ref value, GetSoulProperty);
				}
			}

			/// <summary>Items lottery ID_1</summary>
			/// <remarks>
			/// Japanese short name: "アイテム抽選ID_1", Google translated: "Items lottery ID_1".
			/// Japanese description: "死亡時に取得するアイテムの抽選IDを指定", Google translated: "Specifies the lottery ID of the item to get at the time of death".
			/// </remarks>
			[ParameterTableRowAttribute("itemLotId_1", index: 11, minimum: -1, maximum: 1E+09, step: 1, sortOrder: 28000, unknown2: 1)]
			[DisplayName("Items lottery ID_1")]
			[Description("Specifies the lottery ID of the item to get at the time of death")]
			[DefaultValue((Int32)(-1))]
			public Int32 ItemLotId_1 {
				get { return itemLotId_1; }
				set {
					if ((double)value < -1 || (double)value > 1E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+09 for " + ItemLotId_1Property.Name + ".");
					SetProperty(ref itemLotId_1, ref value, ItemLotId_1Property);
				}
			}

			/// <summary>Items lottery ID_2</summary>
			/// <remarks>
			/// Japanese short name: "アイテム抽選ID_2", Google translated: "Items lottery ID_2".
			/// Japanese description: "死亡時に取得するアイテムの抽選IDを指定", Google translated: "Specifies the lottery ID of the item to get at the time of death".
			/// </remarks>
			[ParameterTableRowAttribute("itemLotId_2", index: 12, minimum: -1, maximum: 1E+09, step: 1, sortOrder: 29000, unknown2: 1)]
			[DisplayName("Items lottery ID_2")]
			[Description("Specifies the lottery ID of the item to get at the time of death")]
			[DefaultValue((Int32)(-1))]
			public Int32 ItemLotId_2 {
				get { return itemLotId_2; }
				set {
					if ((double)value < -1 || (double)value > 1E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+09 for " + ItemLotId_2Property.Name + ".");
					SetProperty(ref itemLotId_2, ref value, ItemLotId_2Property);
				}
			}

			/// <summary>Items lottery ID_3</summary>
			/// <remarks>
			/// Japanese short name: "アイテム抽選ID_3", Google translated: "Items lottery ID_3".
			/// Japanese description: "死亡時に取得するアイテムの抽選IDを指定", Google translated: "Specifies the lottery ID of the item to get at the time of death".
			/// </remarks>
			[ParameterTableRowAttribute("itemLotId_3", index: 13, minimum: -1, maximum: 1E+09, step: 1, sortOrder: 30000, unknown2: 1)]
			[DisplayName("Items lottery ID_3")]
			[Description("Specifies the lottery ID of the item to get at the time of death")]
			[DefaultValue((Int32)(-1))]
			public Int32 ItemLotId_3 {
				get { return itemLotId_3; }
				set {
					if ((double)value < -1 || (double)value > 1E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+09 for " + ItemLotId_3Property.Name + ".");
					SetProperty(ref itemLotId_3, ref value, ItemLotId_3Property);
				}
			}

			/// <summary>Items lottery ID_4</summary>
			/// <remarks>
			/// Japanese short name: "アイテム抽選ID_4", Google translated: "Items lottery ID_4".
			/// Japanese description: "死亡時に取得するアイテムの抽選IDを指定", Google translated: "Specifies the lottery ID of the item to get at the time of death".
			/// </remarks>
			[ParameterTableRowAttribute("itemLotId_4", index: 14, minimum: -1, maximum: 1E+09, step: 1, sortOrder: 31000, unknown2: 1)]
			[DisplayName("Items lottery ID_4")]
			[Description("Specifies the lottery ID of the item to get at the time of death")]
			[DefaultValue((Int32)(-1))]
			public Int32 ItemLotId_4 {
				get { return itemLotId_4; }
				set {
					if ((double)value < -1 || (double)value > 1E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+09 for " + ItemLotId_4Property.Name + ".");
					SetProperty(ref itemLotId_4, ref value, ItemLotId_4Property);
				}
			}

			/// <summary>Items lottery ID_5</summary>
			/// <remarks>
			/// Japanese short name: "アイテム抽選ID_5", Google translated: "Items lottery ID_5".
			/// Japanese description: "死亡時に取得するアイテムの抽選IDを指定", Google translated: "Specifies the lottery ID of the item to get at the time of death".
			/// </remarks>
			[ParameterTableRowAttribute("itemLotId_5", index: 15, minimum: -1, maximum: 1E+09, step: 1, sortOrder: 32000, unknown2: 1)]
			[DisplayName("Items lottery ID_5")]
			[Description("Specifies the lottery ID of the item to get at the time of death")]
			[DefaultValue((Int32)(-1))]
			public Int32 ItemLotId_5 {
				get { return itemLotId_5; }
				set {
					if ((double)value < -1 || (double)value > 1E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+09 for " + ItemLotId_5Property.Name + ".");
					SetProperty(ref itemLotId_5, ref value, ItemLotId_5Property);
				}
			}

			/// <summary>Items lottery ID_6</summary>
			/// <remarks>
			/// Japanese short name: "アイテム抽選ID_6", Google translated: "Items lottery ID_6".
			/// Japanese description: "死亡時に取得するアイテムの抽選IDを指定", Google translated: "Specifies the lottery ID of the item to get at the time of death".
			/// </remarks>
			[ParameterTableRowAttribute("itemLotId_6", index: 16, minimum: -1, maximum: 1E+09, step: 1, sortOrder: 33000, unknown2: 1)]
			[DisplayName("Items lottery ID_6")]
			[Description("Specifies the lottery ID of the item to get at the time of death")]
			[DefaultValue((Int32)(-1))]
			public Int32 ItemLotId_6 {
				get { return itemLotId_6; }
				set {
					if ((double)value < -1 || (double)value > 1E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+09 for " + ItemLotId_6Property.Name + ".");
					SetProperty(ref itemLotId_6, ref value, ItemLotId_6Property);
				}
			}

			/// <summary>Humanity lottery ID</summary>
			/// <remarks>
			/// Japanese short name: "人間性抽選ID", Google translated: "Humanity lottery ID".
			/// Japanese description: "死亡時に取得する人間性の抽選IDを指定", Google translated: "Specifies the ID lottery of human nature to get at the time of death".
			/// </remarks>
			[ParameterTableRowAttribute("humanityLotId", index: 17, minimum: -1, maximum: 1E+09, step: 1, sortOrder: 33010, unknown2: 1)]
			[DisplayName("Humanity lottery ID")]
			[Description("Specifies the ID lottery of human nature to get at the time of death")]
			[DefaultValue((Int32)(-1))]
			public Int32 HumanityLotId {
				get { return humanityLotId; }
				set {
					if ((double)value < -1 || (double)value > 1E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+09 for " + HumanityLotIdProperty.Name + ".");
					SetProperty(ref humanityLotId, ref value, HumanityLotIdProperty);
				}
			}

			/// <summary>Resident special effects 0</summary>
			/// <remarks>
			/// Japanese short name: "常駐特殊効果0", Google translated: "Resident special effects 0".
			/// Japanese description: "常駐特殊効果0", Google translated: "Resident special effects 0".
			/// </remarks>
			[ParameterTableRowAttribute("spEffectID0", index: 18, minimum: -1, maximum: 999999, step: 1, sortOrder: 46000, unknown2: 1)]
			[DisplayName("Resident special effects 0")]
			[Description("Resident special effects 0")]
			[DefaultValue((Int32)(-1))]
			public Int32 SpEffectID0 {
				get { return spEffectID0; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for " + SpEffectID0Property.Name + ".");
					SetProperty(ref spEffectID0, ref value, SpEffectID0Property);
				}
			}

			/// <summary>Resident special effects 1</summary>
			/// <remarks>
			/// Japanese short name: "常駐特殊効果1", Google translated: "Resident special effects 1".
			/// Japanese description: "常駐特殊効果1", Google translated: "Resident special effects 1".
			/// </remarks>
			[ParameterTableRowAttribute("spEffectID1", index: 19, minimum: -1, maximum: 999999, step: 1, sortOrder: 47000, unknown2: 1)]
			[DisplayName("Resident special effects 1")]
			[Description("Resident special effects 1")]
			[DefaultValue((Int32)(-1))]
			public Int32 SpEffectID1 {
				get { return spEffectID1; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for " + SpEffectID1Property.Name + ".");
					SetProperty(ref spEffectID1, ref value, SpEffectID1Property);
				}
			}

			/// <summary>Resident special effects 2</summary>
			/// <remarks>
			/// Japanese short name: "常駐特殊効果2", Google translated: "Resident special effects 2".
			/// Japanese description: "常駐特殊効果2", Google translated: "Resident special effects 2".
			/// </remarks>
			[ParameterTableRowAttribute("spEffectID2", index: 20, minimum: -1, maximum: 999999, step: 1, sortOrder: 48000, unknown2: 1)]
			[DisplayName("Resident special effects 2")]
			[Description("Resident special effects 2")]
			[DefaultValue((Int32)(-1))]
			public Int32 SpEffectID2 {
				get { return spEffectID2; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for " + SpEffectID2Property.Name + ".");
					SetProperty(ref spEffectID2, ref value, SpEffectID2Property);
				}
			}

			/// <summary>Resident special effects 3</summary>
			/// <remarks>
			/// Japanese short name: "常駐特殊効果3", Google translated: "Resident special effects 3".
			/// Japanese description: "常駐特殊効果3", Google translated: "Resident special effects 3".
			/// </remarks>
			[ParameterTableRowAttribute("spEffectID3", index: 21, minimum: -1, maximum: 999999, step: 1, sortOrder: 49000, unknown2: 1)]
			[DisplayName("Resident special effects 3")]
			[Description("Resident special effects 3")]
			[DefaultValue((Int32)(-1))]
			public Int32 SpEffectID3 {
				get { return spEffectID3; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for " + SpEffectID3Property.Name + ".");
					SetProperty(ref spEffectID3, ref value, SpEffectID3Property);
				}
			}

			/// <summary>Resident special effects 4</summary>
			/// <remarks>
			/// Japanese short name: "常駐特殊効果4", Google translated: "Resident special effects 4".
			/// Japanese description: "常駐特殊効果4", Google translated: "Resident special effects 4".
			/// </remarks>
			[ParameterTableRowAttribute("spEffectID4", index: 22, minimum: -1, maximum: 999999, step: 1, sortOrder: 50000, unknown2: 1)]
			[DisplayName("Resident special effects 4")]
			[Description("Resident special effects 4")]
			[DefaultValue((Int32)(-1))]
			public Int32 SpEffectID4 {
				get { return spEffectID4; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for " + SpEffectID4Property.Name + ".");
					SetProperty(ref spEffectID4, ref value, SpEffectID4Property);
				}
			}

			/// <summary>Resident special effects 5</summary>
			/// <remarks>
			/// Japanese short name: "常駐特殊効果5", Google translated: "Resident special effects 5".
			/// Japanese description: "常駐特殊効果5", Google translated: "Resident special effects 5".
			/// </remarks>
			[ParameterTableRowAttribute("spEffectID5", index: 23, minimum: -1, maximum: 999999, step: 1, sortOrder: 50010, unknown2: 1)]
			[DisplayName("Resident special effects 5")]
			[Description("Resident special effects 5")]
			[DefaultValue((Int32)(-1))]
			public Int32 SpEffectID5 {
				get { return spEffectID5; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for " + SpEffectID5Property.Name + ".");
					SetProperty(ref spEffectID5, ref value, SpEffectID5Property);
				}
			}

			/// <summary>Resident special effects 6</summary>
			/// <remarks>
			/// Japanese short name: "常駐特殊効果6", Google translated: "Resident special effects 6".
			/// Japanese description: "常駐特殊効果6", Google translated: "Resident special effects 6".
			/// </remarks>
			[ParameterTableRowAttribute("spEffectID6", index: 24, minimum: -1, maximum: 999999, step: 1, sortOrder: 50020, unknown2: 1)]
			[DisplayName("Resident special effects 6")]
			[Description("Resident special effects 6")]
			[DefaultValue((Int32)(-1))]
			public Int32 SpEffectID6 {
				get { return spEffectID6; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for " + SpEffectID6Property.Name + ".");
					SetProperty(ref spEffectID6, ref value, SpEffectID6Property);
				}
			}

			/// <summary>Resident special effects 7</summary>
			/// <remarks>
			/// Japanese short name: "常駐特殊効果7", Google translated: "Resident special effects 7".
			/// Japanese description: "常駐特殊効果7", Google translated: "Resident special effects 7".
			/// </remarks>
			[ParameterTableRowAttribute("spEffectID7", index: 25, minimum: -1, maximum: 999999, step: 1, sortOrder: 50030, unknown2: 1)]
			[DisplayName("Resident special effects 7")]
			[Description("Resident special effects 7")]
			[DefaultValue((Int32)(-1))]
			public Int32 SpEffectID7 {
				get { return spEffectID7; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for " + SpEffectID7Property.Name + ".");
					SetProperty(ref spEffectID7, ref value, SpEffectID7Property);
				}
			}

			/// <summary>Bonus round for special effects ID</summary>
			/// <remarks>
			/// Japanese short name: "周回ボーナス用特殊効果ＩＤ", Google translated: "Bonus round for special effects ID".
			/// Japanese description: "周回ボーナス用特殊効果ＩＤ", Google translated: "Bonus round for special effects ID".
			/// </remarks>
			[ParameterTableRowAttribute("GameClearSpEffectID", index: 26, minimum: -1, maximum: 999999, step: 1, sortOrder: 51000, unknown2: 1)]
			[DisplayName("Bonus round for special effects ID")]
			[Description("Bonus round for special effects ID")]
			[DefaultValue((Int32)(-1))]
			public Int32 GameClearSpEffectID {
				get { return gameClearSpEffectID; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for " + GameClearSpEffectIDProperty.Name + ".");
					SetProperty(ref gameClearSpEffectID, ref value, GameClearSpEffectIDProperty);
				}
			}

			/// <summary>Physical attack cut rates [ %]</summary>
			/// <remarks>
			/// Japanese short name: "物理攻撃カット率[％]", Google translated: "Physical attack cut rates [ %]".
			/// Japanese description: "ガード時のダメージカット率を各攻撃ごとに設定", Google translated: "Set to each per attack damage cut rate of the guard at the time".
			/// </remarks>
			[ParameterTableRowAttribute("physGuardCutRate", index: 27, minimum: 0, maximum: 100, step: 0.1, sortOrder: 55000, unknown2: 1)]
			[DisplayName("Physical attack cut rates [ %]")]
			[Description("Set to each per attack damage cut rate of the guard at the time")]
			[DefaultValue((Single)0)]
			public Single PhysGuardCutRate {
				get { return physGuardCutRate; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for " + PhysGuardCutRateProperty.Name + ".");
					SetProperty(ref physGuardCutRate, ref value, PhysGuardCutRateProperty);
				}
			}

			/// <summary>Magic attack cut rates [ %]</summary>
			/// <remarks>
			/// Japanese short name: "魔法攻撃カット率[％]", Google translated: "Magic attack cut rates [ %]".
			/// Japanese description: "ガード攻撃でない場合は、0を入れる", Google translated: "If not guard attacks , put a 0".
			/// </remarks>
			[ParameterTableRowAttribute("magGuardCutRate", index: 28, minimum: 0, maximum: 100, step: 0.1, sortOrder: 60000, unknown2: 1)]
			[DisplayName("Magic attack cut rates [ %]")]
			[Description("If not guard attacks , put a 0")]
			[DefaultValue((Single)0)]
			public Single MagGuardCutRate {
				get { return magGuardCutRate; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for " + MagGuardCutRateProperty.Name + ".");
					SetProperty(ref magGuardCutRate, ref value, MagGuardCutRateProperty);
				}
			}

			/// <summary>Flame attack power cut rates [ %]</summary>
			/// <remarks>
			/// Japanese short name: "炎攻撃力カット率[％]", Google translated: "Flame attack power cut rates [ %]".
			/// Japanese description: "炎攻撃をどれだけカットするか？", Google translated: "You can cut how much the fire attack ?".
			/// </remarks>
			[ParameterTableRowAttribute("fireGuardCutRate", index: 29, minimum: 0, maximum: 100, step: 0.1, sortOrder: 61000, unknown2: 1)]
			[DisplayName("Flame attack power cut rates [ %]")]
			[Description("You can cut how much the fire attack ?")]
			[DefaultValue((Single)0)]
			public Single FireGuardCutRate {
				get { return fireGuardCutRate; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for " + FireGuardCutRateProperty.Name + ".");
					SetProperty(ref fireGuardCutRate, ref value, FireGuardCutRateProperty);
				}
			}

			/// <summary>Blitz attack power cut rates [ %]</summary>
			/// <remarks>
			/// Japanese short name: "電撃攻撃力カット率[％]", Google translated: "Blitz attack power cut rates [ %]".
			/// Japanese description: "電撃攻撃をどれだけカットするか？", Google translated: "You can cut much a bolt out of the blue attack ?".
			/// </remarks>
			[ParameterTableRowAttribute("thunGuardCutRate", index: 30, minimum: 0, maximum: 100, step: 0.1, sortOrder: 61100, unknown2: 1)]
			[DisplayName("Blitz attack power cut rates [ %]")]
			[Description("You can cut much a bolt out of the blue attack ?")]
			[DefaultValue((Single)0)]
			public Single ThunGuardCutRate {
				get { return thunGuardCutRate; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for " + ThunGuardCutRateProperty.Name + ".");
					SetProperty(ref thunGuardCutRate, ref value, ThunGuardCutRateProperty);
				}
			}

			/// <summary>Anime ID offset 1</summary>
			/// <remarks>
			/// Japanese short name: "アニメIDオフセット1", Google translated: "Anime ID offset 1".
			/// Japanese description: "すべてのアニメをこの数だけずらしたIDで再生します。なければ元のアニメIDを参照します。", Google translated: "I will play in the ID that was shifted by this number the animation of all . It refers to the ID of the anime original otherwise .".
			/// </remarks>
			[ParameterTableRowAttribute("animIdOffset", index: 31, minimum: 0, maximum: 1E+08, step: 1, sortOrder: 83000, unknown2: 1)]
			[DisplayName("Anime ID offset 1")]
			[Description("I will play in the ID that was shifted by this number the animation of all . It refers to the ID of the anime original otherwise .")]
			[DefaultValue((Int32)0)]
			public Int32 AnimIdOffset {
				get { return animIdOffset; }
				set {
					if ((double)value < 0 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1E+08 for " + AnimIdOffsetProperty.Name + ".");
					SetProperty(ref animIdOffset, ref value, AnimIdOffsetProperty);
				}
			}

			/// <summary>Move animation parameter ID</summary>
			/// <remarks>
			/// Japanese short name: "移動アニメパラメータID", Google translated: "Move animation parameter ID".
			/// Japanese description: "移動アニメパラメータ参照ID", Google translated: "Move animation parameter reference ID".
			/// </remarks>
			[ParameterTableRowAttribute("moveAnimId", index: 32, minimum: 0, maximum: 999999, step: 1, sortOrder: 92000, unknown2: 1)]
			[DisplayName("Move animation parameter ID")]
			[Description("Move animation parameter reference ID")]
			[DefaultValue((Int32)0)]
			public Int32 MoveAnimId {
				get { return moveAnimId; }
				set {
					if ((double)value < 0 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999999 for " + MoveAnimIdProperty.Name + ".");
					SetProperty(ref moveAnimId, ref value, MoveAnimIdProperty);
				}
			}

			/// <summary>Special move animation parameters ID0</summary>
			/// <remarks>
			/// Japanese short name: "特殊移動アニメパラメータID0", Google translated: "Special move animation parameters ID0".
			/// Japanese description: "特殊移動アニメパラメータ参照ID", Google translated: "Special move animation parameters reference ID".
			/// </remarks>
			[ParameterTableRowAttribute("spMoveAnimId1", index: 33, minimum: 0, maximum: 999999, step: 1, sortOrder: 93000, unknown2: 1)]
			[DisplayName("Special move animation parameters ID0")]
			[Description("Special move animation parameters reference ID")]
			[DefaultValue((Int32)0)]
			public Int32 SpMoveAnimId1 {
				get { return spMoveAnimId1; }
				set {
					if ((double)value < 0 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999999 for " + SpMoveAnimId1Property.Name + ".");
					SetProperty(ref spMoveAnimId1, ref value, SpMoveAnimId1Property);
				}
			}

			/// <summary>Special move animation parameters ID1</summary>
			/// <remarks>
			/// Japanese short name: "特殊移動アニメパラメータID1", Google translated: "Special move animation parameters ID1".
			/// Japanese description: "特殊移動アニメパラメータ参照ID", Google translated: "Special move animation parameters reference ID".
			/// </remarks>
			[ParameterTableRowAttribute("spMoveAnimId2", index: 34, minimum: 0, maximum: 999999, step: 1, sortOrder: 94000, unknown2: 1)]
			[DisplayName("Special move animation parameters ID1")]
			[Description("Special move animation parameters reference ID")]
			[DefaultValue((Int32)0)]
			public Int32 SpMoveAnimId2 {
				get { return spMoveAnimId2; }
				set {
					if ((double)value < 0 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999999 for " + SpMoveAnimId2Property.Name + ".");
					SetProperty(ref spMoveAnimId2, ref value, SpMoveAnimId2Property);
				}
			}

			/// <summary>Network warp judgment distance [m / s ]</summary>
			/// <remarks>
			/// Japanese short name: "ネットワークワープ判定距離[m/秒]", Google translated: "Network warp judgment distance [m / s ]".
			/// Japanese description: "ネットワークの同期で、補完移動でなくワープさせる距離。スピードの速い人（exドラゴン)は長めにしてあげる必要がある。", Google translated: "Synchronization of the network , the distance to be warped and not complementary movement . There is a need to Giving longish (ex Dragon ) people fast speed .".
			/// </remarks>
			[ParameterTableRowAttribute("networkWarpDist", index: 35, minimum: 0, maximum: 1000, step: 0.1, sortOrder: 101000, unknown2: 1)]
			[DisplayName("Network warp judgment distance [m / s ]")]
			[Description("Synchronization of the network , the distance to be warped and not complementary movement . There is a need to Giving longish (ex Dragon ) people fast speed .")]
			[DefaultValue((Single)0)]
			public Single NetworkWarpDist {
				get { return networkWarpDist; }
				set {
					if ((double)value < 0 || (double)value > 1000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1000 for " + NetworkWarpDistProperty.Name + ".");
					SetProperty(ref networkWarpDist, ref value, NetworkWarpDistProperty);
				}
			}

			/// <summary>R1</summary>
			/// <remarks>
			/// Japanese short name: "R1", Google translated: "R1".
			/// Japanese description: "行動パラメータツールからIDを登録し、行動を指定する.", Google translated: "Register an ID from the action parameter tool , specify the action .".
			/// </remarks>
			[ParameterTableRowAttribute("dbgBehaviorR1", index: 36, minimum: -1, maximum: 999999, step: 1, sortOrder: 102000, unknown2: 1)]
			[DisplayName("R1")]
			[Description("Register an ID from the action parameter tool , specify the action .")]
			[DefaultValue((Int32)(-1))]
			public Int32 DbgBehaviorR1 {
				get { return dbgBehaviorR1; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for " + DbgBehaviorR1Property.Name + ".");
					SetProperty(ref dbgBehaviorR1, ref value, DbgBehaviorR1Property);
				}
			}

			/// <summary>L1</summary>
			/// <remarks>
			/// Japanese short name: "L1", Google translated: "L1".
			/// Japanese description: "行動パラメータツールからIDを登録し、行動を指定する.", Google translated: "Register an ID from the action parameter tool , specify the action .".
			/// </remarks>
			[ParameterTableRowAttribute("dbgBehaviorL1", index: 37, minimum: -1, maximum: 999999, step: 1, sortOrder: 103000, unknown2: 1)]
			[DisplayName("L1")]
			[Description("Register an ID from the action parameter tool , specify the action .")]
			[DefaultValue((Int32)(-1))]
			public Int32 DbgBehaviorL1 {
				get { return dbgBehaviorL1; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for " + DbgBehaviorL1Property.Name + ".");
					SetProperty(ref dbgBehaviorL1, ref value, DbgBehaviorL1Property);
				}
			}

			/// <summary>R2</summary>
			/// <remarks>
			/// Japanese short name: "R2", Google translated: "R2".
			/// Japanese description: "行動パラメータツールからIDを登録し、行動を指定する.", Google translated: "Register an ID from the action parameter tool , specify the action .".
			/// </remarks>
			[ParameterTableRowAttribute("dbgBehaviorR2", index: 38, minimum: -1, maximum: 999999, step: 1, sortOrder: 104000, unknown2: 1)]
			[DisplayName("R2")]
			[Description("Register an ID from the action parameter tool , specify the action .")]
			[DefaultValue((Int32)(-1))]
			public Int32 DbgBehaviorR2 {
				get { return dbgBehaviorR2; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for " + DbgBehaviorR2Property.Name + ".");
					SetProperty(ref dbgBehaviorR2, ref value, DbgBehaviorR2Property);
				}
			}

			/// <summary>L2</summary>
			/// <remarks>
			/// Japanese short name: "L2", Google translated: "L2".
			/// Japanese description: "行動パラメータツールからIDを登録し、行動を指定する.", Google translated: "Register an ID from the action parameter tool , specify the action .".
			/// </remarks>
			[ParameterTableRowAttribute("dbgBehaviorL2", index: 39, minimum: -1, maximum: 999999, step: 1, sortOrder: 105000, unknown2: 1)]
			[DisplayName("L2")]
			[Description("Register an ID from the action parameter tool , specify the action .")]
			[DefaultValue((Int32)(-1))]
			public Int32 DbgBehaviorL2 {
				get { return dbgBehaviorL2; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for " + DbgBehaviorL2Property.Name + ".");
					SetProperty(ref dbgBehaviorL2, ref value, DbgBehaviorL2Property);
				}
			}

			/// <summary>□</summary>
			/// <remarks>
			/// Japanese short name: "□", Google translated: "□".
			/// Japanese description: "行動パラメータツールからIDを登録し、行動を指定する.", Google translated: "Register an ID from the action parameter tool , specify the action .".
			/// </remarks>
			[ParameterTableRowAttribute("dbgBehaviorRL", index: 40, minimum: -1, maximum: 999999, step: 1, sortOrder: 106000, unknown2: 1)]
			[DisplayName("□")]
			[Description("Register an ID from the action parameter tool , specify the action .")]
			[DefaultValue((Int32)(-1))]
			public Int32 DbgBehaviorRL {
				get { return dbgBehaviorRL; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for " + DbgBehaviorRLProperty.Name + ".");
					SetProperty(ref dbgBehaviorRL, ref value, DbgBehaviorRLProperty);
				}
			}

			/// <summary>○</summary>
			/// <remarks>
			/// Japanese short name: "○", Google translated: "○".
			/// Japanese description: "行動パラメータツールからIDを登録し、行動を指定する.", Google translated: "Register an ID from the action parameter tool , specify the action .".
			/// </remarks>
			[ParameterTableRowAttribute("dbgBehaviorRR", index: 41, minimum: -1, maximum: 999999, step: 1, sortOrder: 107000, unknown2: 1)]
			[DisplayName("○")]
			[Description("Register an ID from the action parameter tool , specify the action .")]
			[DefaultValue((Int32)(-1))]
			public Int32 DbgBehaviorRR {
				get { return dbgBehaviorRR; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for " + DbgBehaviorRRProperty.Name + ".");
					SetProperty(ref dbgBehaviorRR, ref value, DbgBehaviorRRProperty);
				}
			}

			/// <summary>×</summary>
			/// <remarks>
			/// Japanese short name: "×", Google translated: "×".
			/// Japanese description: "行動パラメータツールからIDを登録し、行動を指定する.", Google translated: "Register an ID from the action parameter tool , specify the action .".
			/// </remarks>
			[ParameterTableRowAttribute("dbgBehaviorRD", index: 42, minimum: -1, maximum: 999999, step: 1, sortOrder: 108000, unknown2: 1)]
			[DisplayName("×")]
			[Description("Register an ID from the action parameter tool , specify the action .")]
			[DefaultValue((Int32)(-1))]
			public Int32 DbgBehaviorRD {
				get { return dbgBehaviorRD; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for " + DbgBehaviorRDProperty.Name + ".");
					SetProperty(ref dbgBehaviorRD, ref value, DbgBehaviorRDProperty);
				}
			}

			/// <summary>△</summary>
			/// <remarks>
			/// Japanese short name: "△", Google translated: "△".
			/// Japanese description: "行動パラメータツールからIDを登録し、行動を指定する.", Google translated: "Register an ID from the action parameter tool , specify the action .".
			/// </remarks>
			[ParameterTableRowAttribute("dbgBehaviorRU", index: 43, minimum: -1, maximum: 999999, step: 1, sortOrder: 109000, unknown2: 1)]
			[DisplayName("△")]
			[Description("Register an ID from the action parameter tool , specify the action .")]
			[DefaultValue((Int32)(-1))]
			public Int32 DbgBehaviorRU {
				get { return dbgBehaviorRU; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for " + DbgBehaviorRUProperty.Name + ".");
					SetProperty(ref dbgBehaviorRU, ref value, DbgBehaviorRUProperty);
				}
			}

			/// <summary>←</summary>
			/// <remarks>
			/// Japanese short name: "←", Google translated: "←".
			/// Japanese description: "行動パラメータツールからIDを登録し、行動を指定する.", Google translated: "Register an ID from the action parameter tool , specify the action .".
			/// </remarks>
			[ParameterTableRowAttribute("dbgBehaviorLL", index: 44, minimum: -1, maximum: 999999, step: 1, sortOrder: 110000, unknown2: 1)]
			[DisplayName("←")]
			[Description("Register an ID from the action parameter tool , specify the action .")]
			[DefaultValue((Int32)(-1))]
			public Int32 DbgBehaviorLL {
				get { return dbgBehaviorLL; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for " + DbgBehaviorLLProperty.Name + ".");
					SetProperty(ref dbgBehaviorLL, ref value, DbgBehaviorLLProperty);
				}
			}

			/// <summary>→</summary>
			/// <remarks>
			/// Japanese short name: "→", Google translated: "→".
			/// Japanese description: "行動パラメータツールからIDを登録し、行動を指定する.", Google translated: "Register an ID from the action parameter tool , specify the action .".
			/// </remarks>
			[ParameterTableRowAttribute("dbgBehaviorLR", index: 45, minimum: -1, maximum: 999999, step: 1, sortOrder: 111000, unknown2: 1)]
			[DisplayName("→")]
			[Description("Register an ID from the action parameter tool , specify the action .")]
			[DefaultValue((Int32)(-1))]
			public Int32 DbgBehaviorLR {
				get { return dbgBehaviorLR; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for " + DbgBehaviorLRProperty.Name + ".");
					SetProperty(ref dbgBehaviorLR, ref value, DbgBehaviorLRProperty);
				}
			}

			/// <summary>↓</summary>
			/// <remarks>
			/// Japanese short name: "↓", Google translated: "↓".
			/// Japanese description: "行動パラメータツールからIDを登録し、行動を指定する.", Google translated: "Register an ID from the action parameter tool , specify the action .".
			/// </remarks>
			[ParameterTableRowAttribute("dbgBehaviorLD", index: 46, minimum: -1, maximum: 999999, step: 1, sortOrder: 112000, unknown2: 1)]
			[DisplayName("↓")]
			[Description("Register an ID from the action parameter tool , specify the action .")]
			[DefaultValue((Int32)(-1))]
			public Int32 DbgBehaviorLD {
				get { return dbgBehaviorLD; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for " + DbgBehaviorLDProperty.Name + ".");
					SetProperty(ref dbgBehaviorLD, ref value, DbgBehaviorLDProperty);
				}
			}

			/// <summary>↑</summary>
			/// <remarks>
			/// Japanese short name: "↑", Google translated: "↑".
			/// Japanese description: "行動パラメータツールからIDを登録し、行動を指定する.", Google translated: "Register an ID from the action parameter tool , specify the action .".
			/// </remarks>
			[ParameterTableRowAttribute("dbgBehaviorLU", index: 47, minimum: -1, maximum: 999999, step: 1, sortOrder: 113000, unknown2: 1)]
			[DisplayName("↑")]
			[Description("Register an ID from the action parameter tool , specify the action .")]
			[DefaultValue((Int32)(-1))]
			public Int32 DbgBehaviorLU {
				get { return dbgBehaviorLU; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for " + DbgBehaviorLUProperty.Name + ".");
					SetProperty(ref dbgBehaviorLU, ref value, DbgBehaviorLUProperty);
				}
			}

			/// <summary>Anime ID offset 2</summary>
			/// <remarks>
			/// Japanese short name: "アニメIDオフセット2", Google translated: "Anime ID offset 2".
			/// Japanese description: "すべてのアニメをこの数だけずらしたIDで再生します。なければアニメIDオフセット1のアニメIDを参照します。", Google translated: "I will play in the ID that was shifted by this number the animation of all . It refers to the ID of the anime anime ID offset 1 if not .".
			/// </remarks>
			[ParameterTableRowAttribute("animIdOffset2", index: 48, minimum: -1, maximum: 1E+08, step: 1, sortOrder: 122000, unknown2: 1)]
			[DisplayName("Anime ID offset 2")]
			[Description("I will play in the ID that was shifted by this number the animation of all . It refers to the ID of the anime anime ID offset 1 if not .")]
			[DefaultValue((Int32)0)]
			public Int32 AnimIdOffset2 {
				get { return animIdOffset2; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for " + AnimIdOffset2Property.Name + ".");
					SetProperty(ref animIdOffset2, ref value, AnimIdOffset2Property);
				}
			}

			/// <summary>Site 1 damage magnification</summary>
			/// <remarks>
			/// Japanese short name: "部位1ダメージ倍率", Google translated: "Site 1 damage magnification".
			/// Japanese description: "部位1に対するダメージ処理に適応する倍率", Google translated: "Magnification to adapt to the damage process for the one-site".
			/// </remarks>
			[ParameterTableRowAttribute("partsDamageRate1", index: 49, minimum: 0, maximum: 99, step: 0.01, sortOrder: 51110, unknown2: 1)]
			[DisplayName("Site 1 damage magnification")]
			[Description("Magnification to adapt to the damage process for the one-site")]
			[DefaultValue((Single)1)]
			public Single PartsDamageRate1 {
				get { return partsDamageRate1; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for " + PartsDamageRate1Property.Name + ".");
					SetProperty(ref partsDamageRate1, ref value, PartsDamageRate1Property);
				}
			}

			/// <summary>2 Damage magnification site</summary>
			/// <remarks>
			/// Japanese short name: "部位2ダメージ倍率", Google translated: "2 Damage magnification site".
			/// Japanese description: "部位2に対するダメージ処理に適応する倍率", Google translated: "Magnification to adapt to the damage process for the two-site".
			/// </remarks>
			[ParameterTableRowAttribute("partsDamageRate2", index: 50, minimum: 0, maximum: 99, step: 0.01, sortOrder: 51120, unknown2: 1)]
			[DisplayName("2 Damage magnification site")]
			[Description("Magnification to adapt to the damage process for the two-site")]
			[DefaultValue((Single)1)]
			public Single PartsDamageRate2 {
				get { return partsDamageRate2; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for " + PartsDamageRate2Property.Name + ".");
					SetProperty(ref partsDamageRate2, ref value, PartsDamageRate2Property);
				}
			}

			/// <summary>Site 3 damage magnification</summary>
			/// <remarks>
			/// Japanese short name: "部位3ダメージ倍率", Google translated: "Site 3 damage magnification".
			/// Japanese description: "部位3に対するダメージ処理に適応する倍率", Google translated: "Magnification to adapt to the damage process for the three sites".
			/// </remarks>
			[ParameterTableRowAttribute("partsDamageRate3", index: 51, minimum: 0, maximum: 99, step: 0.01, sortOrder: 51130, unknown2: 1)]
			[DisplayName("Site 3 damage magnification")]
			[Description("Magnification to adapt to the damage process for the three sites")]
			[DefaultValue((Single)1)]
			public Single PartsDamageRate3 {
				get { return partsDamageRate3; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for " + PartsDamageRate3Property.Name + ".");
					SetProperty(ref partsDamageRate3, ref value, PartsDamageRate3Property);
				}
			}

			/// <summary>Site 4 Damage magnification</summary>
			/// <remarks>
			/// Japanese short name: "部位4ダメージ倍率", Google translated: "Site 4 Damage magnification".
			/// Japanese description: "部位4に対するダメージ処理に適応する倍率", Google translated: "Magnification to adapt to the damage process for the four sites".
			/// </remarks>
			[ParameterTableRowAttribute("partsDamageRate4", index: 52, minimum: 0, maximum: 99, step: 0.01, sortOrder: 51140, unknown2: 1)]
			[DisplayName("Site 4 Damage magnification")]
			[Description("Magnification to adapt to the damage process for the four sites")]
			[DefaultValue((Single)1)]
			public Single PartsDamageRate4 {
				get { return partsDamageRate4; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for " + PartsDamageRate4Property.Name + ".");
					SetProperty(ref partsDamageRate4, ref value, PartsDamageRate4Property);
				}
			}

			/// <summary>5 Damage magnification site</summary>
			/// <remarks>
			/// Japanese short name: "部位5ダメージ倍率", Google translated: "5 Damage magnification site".
			/// Japanese description: "部位5に対するダメージ処理に適応する倍率", Google translated: "Magnification to adapt to the damage process for the five sites".
			/// </remarks>
			[ParameterTableRowAttribute("partsDamageRate5", index: 53, minimum: 0, maximum: 99, step: 0.01, sortOrder: 51150, unknown2: 1)]
			[DisplayName("5 Damage magnification site")]
			[Description("Magnification to adapt to the damage process for the five sites")]
			[DefaultValue((Single)1)]
			public Single PartsDamageRate5 {
				get { return partsDamageRate5; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for " + PartsDamageRate5Property.Name + ".");
					SetProperty(ref partsDamageRate5, ref value, PartsDamageRate5Property);
				}
			}

			/// <summary>6 Damage magnification site</summary>
			/// <remarks>
			/// Japanese short name: "部位6ダメージ倍率", Google translated: "6 Damage magnification site".
			/// Japanese description: "部位6に対するダメージ処理に適応する倍率", Google translated: "Magnification to adapt to the damage process for the six sites".
			/// </remarks>
			[ParameterTableRowAttribute("partsDamageRate6", index: 54, minimum: 0, maximum: 99, step: 0.01, sortOrder: 51160, unknown2: 1)]
			[DisplayName("6 Damage magnification site")]
			[Description("Magnification to adapt to the damage process for the six sites")]
			[DefaultValue((Single)1)]
			public Single PartsDamageRate6 {
				get { return partsDamageRate6; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for " + PartsDamageRate6Property.Name + ".");
					SetProperty(ref partsDamageRate6, ref value, PartsDamageRate6Property);
				}
			}

			/// <summary>Site 7 Damage magnification</summary>
			/// <remarks>
			/// Japanese short name: "部位7ダメージ倍率", Google translated: "Site 7 Damage magnification".
			/// Japanese description: "部位7に対するダメージ処理に適応する倍率", Google translated: "Magnification to adapt to the damage process for the seven sites".
			/// </remarks>
			[ParameterTableRowAttribute("partsDamageRate7", index: 55, minimum: 0, maximum: 99, step: 0.01, sortOrder: 51165, unknown2: 1)]
			[DisplayName("Site 7 Damage magnification")]
			[Description("Magnification to adapt to the damage process for the seven sites")]
			[DefaultValue((Single)1)]
			public Single PartsDamageRate7 {
				get { return partsDamageRate7; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for " + PartsDamageRate7Property.Name + ".");
					SetProperty(ref partsDamageRate7, ref value, PartsDamageRate7Property);
				}
			}

			/// <summary>Site 8 Damage magnification</summary>
			/// <remarks>
			/// Japanese short name: "部位8ダメージ倍率", Google translated: "Site 8 Damage magnification".
			/// Japanese description: "部位8に対するダメージ処理に適応する倍率", Google translated: "Magnification to adapt to the damage process for the eight sites".
			/// </remarks>
			[ParameterTableRowAttribute("partsDamageRate8", index: 56, minimum: 0, maximum: 99, step: 0.01, sortOrder: 51166, unknown2: 1)]
			[DisplayName("Site 8 Damage magnification")]
			[Description("Magnification to adapt to the damage process for the eight sites")]
			[DefaultValue((Single)1)]
			public Single PartsDamageRate8 {
				get { return partsDamageRate8; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for " + PartsDamageRate8Property.Name + ".");
					SetProperty(ref partsDamageRate8, ref value, PartsDamageRate8Property);
				}
			}

			/// <summary>Weakness site damage magnification</summary>
			/// <remarks>
			/// Japanese short name: "弱点部位ダメージ倍率", Google translated: "Weakness site damage magnification".
			/// Japanese description: "弱点部位に対するダメージ処理に適応する倍率", Google translated: "Magnification to adapt to the damage process for the weakness site".
			/// </remarks>
			[ParameterTableRowAttribute("weakPartsDamageRate", index: 57, minimum: 0, maximum: 99, step: 0.01, sortOrder: 51170, unknown2: 1)]
			[DisplayName("Weakness site damage magnification")]
			[Description("Magnification to adapt to the damage process for the weakness site")]
			[DefaultValue((Single)1)]
			public Single WeakPartsDamageRate {
				get { return weakPartsDamageRate; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for " + WeakPartsDamageRateProperty.Name + ".");
					SetProperty(ref weakPartsDamageRate, ref value, WeakPartsDamageRateProperty);
				}
			}

			/// <summary>SA recovery time correction value</summary>
			/// <remarks>
			/// Japanese short name: "SA回復時間補正値", Google translated: "SA recovery time correction value".
			/// Japanese description: "スーパーアーマー回復時間用の補正値", Google translated: "Correction value of super armor recovery time for".
			/// </remarks>
			[ParameterTableRowAttribute("superArmorRecoverCorrection", index: 58, minimum: -1, maximum: 99.99, step: 0.01, sortOrder: 39200, unknown2: 1)]
			[DisplayName("SA recovery time correction value")]
			[Description("Correction value of super armor recovery time for")]
			[DefaultValue((Single)0)]
			public Single SuperArmorRecoverCorrection {
				get { return superArmorRecoverCorrection; }
				set {
					if ((double)value < -1 || (double)value > 99.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 99.99 for " + SuperArmorRecoverCorrectionProperty.Name + ".");
					SetProperty(ref superArmorRecoverCorrection, ref value, SuperArmorRecoverCorrectionProperty);
				}
			}

			/// <summary>SA break when knock back distance</summary>
			/// <remarks>
			/// Japanese short name: "SAブレイク時ノックバック距離", Google translated: "SA break when knock back distance".
			/// Japanese description: "SAブレイクの時だけに使えるノックバック距離", Google translated: "Knock back distance that can be used only when the SA break".
			/// </remarks>
			[ParameterTableRowAttribute("superArmorBrakeKnockbackDist", index: 59, minimum: 0, maximum: 99.99, step: 0.01, sortOrder: 41110, unknown2: 1)]
			[DisplayName("SA break when knock back distance")]
			[Description("Knock back distance that can be used only when the SA break")]
			[DefaultValue((Single)0)]
			public Single SuperArmorBrakeKnockbackDist {
				get { return superArmorBrakeKnockbackDist; }
				set {
					if ((double)value < 0 || (double)value > 99.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99.99 for " + SuperArmorBrakeKnockbackDistProperty.Name + ".");
					SetProperty(ref superArmorBrakeKnockbackDist, ref value, SuperArmorBrakeKnockbackDistProperty);
				}
			}

			/// <summary>Stamina</summary>
			/// <remarks>
			/// Japanese short name: "スタミナ", Google translated: "Stamina".
			/// Japanese description: "スタミナ総量.", Google translated: "Stamina total .".
			/// </remarks>
			[ParameterTableRowAttribute("stamina", index: 60, minimum: 0, maximum: 999, step: 1, sortOrder: 24000, unknown2: 1)]
			[DisplayName("Stamina")]
			[Description("Stamina total .")]
			[DefaultValue((UInt16)0)]
			public UInt16 Stamina {
				get { return stamina; }
				set {
					if ((double)value < 0 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for " + StaminaProperty.Name + ".");
					SetProperty(ref stamina, ref value, StaminaProperty);
				}
			}

			/// <summary>Stamina recovery base speed [point / s]</summary>
			/// <remarks>
			/// Japanese short name: "スタミナ回復基本速度[point/s]", Google translated: "Stamina recovery base speed [point / s]".
			/// Japanese description: "スタミナ回復基本速度[point/s]", Google translated: "Stamina recovery base speed [point / s]".
			/// </remarks>
			[ParameterTableRowAttribute("staminaRecoverBaseVel", index: 61, minimum: 0, maximum: 1000, step: 1, sortOrder: 25000, unknown2: 1)]
			[DisplayName("Stamina recovery base speed [point / s]")]
			[Description("Stamina recovery base speed [point / s]")]
			[DefaultValue((UInt16)0)]
			public UInt16 StaminaRecoverBaseVel {
				get { return staminaRecoverBaseVel; }
				set {
					if ((double)value < 0 || (double)value > 1000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1000 for " + StaminaRecoverBaseVelProperty.Name + ".");
					SetProperty(ref staminaRecoverBaseVel, ref value, StaminaRecoverBaseVelProperty);
				}
			}

			/// <summary>Physical Def</summary>
			/// <remarks>
			/// Japanese short name: "物理防御力", Google translated: "Physical Def".
			/// Japanese description: "物理攻撃に対するダメージ減少基本値.", Google translated: "Damage Reduction base value for a physical attack .".
			/// </remarks>
			[ParameterTableRowAttribute("def_phys", index: 62, minimum: 0, maximum: 9999, step: 1, sortOrder: 34000, unknown2: 1)]
			[DisplayName("Physical Def")]
			[Description("Damage Reduction base value for a physical attack .")]
			[DefaultValue((UInt16)0)]
			public UInt16 Def_phys {
				get { return def_phys; }
				set {
					if ((double)value < 0 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9999 for " + Def_physProperty.Name + ".");
					SetProperty(ref def_phys, ref value, Def_physProperty);
				}
			}

			/// <summary>Slashing defense force [ %]</summary>
			/// <remarks>
			/// Japanese short name: "斬撃防御力[％]", Google translated: "Slashing defense force [ %]".
			/// Japanese description: "攻撃属性を見て、斬撃属性のときは、防御力を減少させる.", Google translated: "Look at the attack attribute , when the撃属of Zan , to reduce the defense force .".
			/// </remarks>
			[ParameterTableRowAttribute("def_slash", index: 63, minimum: -100, maximum: 999, step: 1, sortOrder: 35000, unknown2: 1)]
			[DisplayName("Slashing defense force [ %]")]
			[Description("Look at the attack attribute , when the撃属of Zan , to reduce the defense force .")]
			[DefaultValue((Int16)0)]
			public Int16 Def_slash {
				get { return def_slash; }
				set {
					if ((double)value < -100 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -100 to 999 for " + Def_slashProperty.Name + ".");
					SetProperty(ref def_slash, ref value, Def_slashProperty);
				}
			}

			/// <summary>Blow Defense [ %]</summary>
			/// <remarks>
			/// Japanese short name: "打撃防御力[％]", Google translated: "Blow Defense [ %]".
			/// Japanese description: "攻撃属性を見て、打撃属性のときは、防御力を減少させる.", Google translated: "Looking at the attack attribute , when the stroke attribute , to reduce the defense .".
			/// </remarks>
			[ParameterTableRowAttribute("def_blow", index: 64, minimum: -100, maximum: 999, step: 1, sortOrder: 36000, unknown2: 1)]
			[DisplayName("Blow Defense [ %]")]
			[Description("Looking at the attack attribute , when the stroke attribute , to reduce the defense .")]
			[DefaultValue((Int16)0)]
			public Int16 Def_blow {
				get { return def_blow; }
				set {
					if ((double)value < -100 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -100 to 999 for " + Def_blowProperty.Name + ".");
					SetProperty(ref def_blow, ref value, Def_blowProperty);
				}
			}

			/// <summary>Piercing defense force [ %]</summary>
			/// <remarks>
			/// Japanese short name: "刺突防御力[％]", Google translated: "Piercing defense force [ %]".
			/// Japanese description: "攻撃属性を見て、刺突属性のときは、防御力を減少させる.", Google translated: "Look at the attack attribute , when the thorn of突属, to reduce the defense force .".
			/// </remarks>
			[ParameterTableRowAttribute("def_thrust", index: 65, minimum: -100, maximum: 999, step: 1, sortOrder: 37000, unknown2: 1)]
			[DisplayName("Piercing defense force [ %]")]
			[Description("Look at the attack attribute , when the thorn of突属, to reduce the defense force .")]
			[DefaultValue((Int16)0)]
			public Int16 Def_thrust {
				get { return def_thrust; }
				set {
					if ((double)value < -100 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -100 to 999 for " + Def_thrustProperty.Name + ".");
					SetProperty(ref def_thrust, ref value, Def_thrustProperty);
				}
			}

			/// <summary>Magic defense</summary>
			/// <remarks>
			/// Japanese short name: "魔法防御力", Google translated: "Magic defense".
			/// Japanese description: "魔法攻撃に対するダメージ減少基本値.", Google translated: "Damage Reduction basic value for the magic attack .".
			/// </remarks>
			[ParameterTableRowAttribute("def_mag", index: 66, minimum: 0, maximum: 9999, step: 1, sortOrder: 38000, unknown2: 1)]
			[DisplayName("Magic defense")]
			[Description("Damage Reduction basic value for the magic attack .")]
			[DefaultValue((UInt16)0)]
			public UInt16 Def_mag {
				get { return def_mag; }
				set {
					if ((double)value < 0 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9999 for " + Def_magProperty.Name + ".");
					SetProperty(ref def_mag, ref value, Def_magProperty);
				}
			}

			/// <summary>Flame Defense</summary>
			/// <remarks>
			/// Japanese short name: "炎防御力", Google translated: "Flame Defense".
			/// Japanese description: "炎攻撃に対するダメージ減少基本値.", Google translated: "Damage Reduction basic value for flame attack .".
			/// </remarks>
			[ParameterTableRowAttribute("def_fire", index: 67, minimum: 0, maximum: 9999, step: 1, sortOrder: 39000, unknown2: 1)]
			[DisplayName("Flame Defense")]
			[Description("Damage Reduction basic value for flame attack .")]
			[DefaultValue((UInt16)0)]
			public UInt16 Def_fire {
				get { return def_fire; }
				set {
					if ((double)value < 0 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9999 for " + Def_fireProperty.Name + ".");
					SetProperty(ref def_fire, ref value, Def_fireProperty);
				}
			}

			/// <summary>Blitz Defense</summary>
			/// <remarks>
			/// Japanese short name: "電撃防御力", Google translated: "Blitz Defense".
			/// Japanese description: "電撃攻撃に対するダメージ減少基本値.", Google translated: "Damage Reduction basic value for the blitz attack .".
			/// </remarks>
			[ParameterTableRowAttribute("def_thunder", index: 68, minimum: 0, maximum: 9999, step: 1, sortOrder: 39100, unknown2: 1)]
			[DisplayName("Blitz Defense")]
			[Description("Damage Reduction basic value for the blitz attack .")]
			[DefaultValue((UInt16)0)]
			public UInt16 Def_thunder {
				get { return def_thunder; }
				set {
					if ((double)value < 0 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9999 for " + Def_thunderProperty.Name + ".");
					SetProperty(ref def_thunder, ref value, Def_thunderProperty);
				}
			}

			/// <summary>Repelling Defense</summary>
			/// <remarks>
			/// Japanese short name: "はじき防御力", Google translated: "Repelling Defense".
			/// Japanese description: "敵の攻撃のはじき判定に使用。//ガード以外の通常攻撃でもはじけるようにするためのものです.//硬い表皮の敵は、何もしなくてもはじかれることがある…みたいな感じ通常の敵なら関係ないです.", Google translated: "It used to determine repelling of the enemy attack . / / This is intended to popping usually also attack the guard outside . / / The enemy of hard skin , does not matter if enemies usually felt such ... like that may be repelled or may not be anything .".
			/// </remarks>
			[ParameterTableRowAttribute("defFlickPower", index: 69, minimum: 0, maximum: 999, step: 1, sortOrder: 40000, unknown2: 1)]
			[DisplayName("Repelling Defense")]
			[Description("It used to determine repelling of the enemy attack . / / This is intended to popping usually also attack the guard outside . / / The enemy of hard skin , does not matter if enemies usually felt such ... like that may be repelled or may not be anything .")]
			[DefaultValue((UInt16)0)]
			public UInt16 DefFlickPower {
				get { return defFlickPower; }
				set {
					if ((double)value < 0 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for " + DefFlickPowerProperty.Name + ".");
					SetProperty(ref defFlickPower, ref value, DefFlickPowerProperty);
				}
			}

			/// <summary>Poison Resistance</summary>
			/// <remarks>
			/// Japanese short name: "毒耐性", Google translated: "Poison Resistance".
			/// Japanese description: "毒状態異常へのかかりにくさ", Google translated: "The difficulty takes to poison the abnormal state".
			/// </remarks>
			[ParameterTableRowAttribute("resist_poison", index: 70, minimum: 0, maximum: 999, step: 1, sortOrder: 43000, unknown2: 1)]
			[DisplayName("Poison Resistance")]
			[Description("The difficulty takes to poison the abnormal state")]
			[DefaultValue((UInt16)0)]
			public UInt16 ResistPoison {
				get { return resistPoison; }
				set {
					if ((double)value < 0 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for " + ResistPoisonProperty.Name + ".");
					SetProperty(ref resistPoison, ref value, ResistPoisonProperty);
				}
			}

			/// <summary>Plague -resistant</summary>
			/// <remarks>
			/// Japanese short name: "疫病耐性", Google translated: "Plague -resistant".
			/// Japanese description: "疫病状態異常へのかかりにくさ", Google translated: "The difficulty to plague much of the abnormal state".
			/// </remarks>
			[ParameterTableRowAttribute("resist_desease", index: 71, minimum: 0, maximum: 999, step: 1, sortOrder: 44000, unknown2: 1)]
			[DisplayName("Plague -resistant")]
			[Description("The difficulty to plague much of the abnormal state")]
			[DefaultValue((UInt16)0)]
			public UInt16 ResistDisease {
				get { return resistDisease; }
				set {
					if ((double)value < 0 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for " + ResistDiseaseProperty.Name + ".");
					SetProperty(ref resistDisease, ref value, ResistDiseaseProperty);
				}
			}

			/// <summary>Bleeding -resistant</summary>
			/// <remarks>
			/// Japanese short name: "出血耐性", Google translated: "Bleeding -resistant".
			/// Japanese description: "出血状態異常へのかかりにくさ", Google translated: "The difficulty takes to abnormal bleeding".
			/// </remarks>
			[ParameterTableRowAttribute("resist_blood", index: 72, minimum: 0, maximum: 999, step: 1, sortOrder: 45000, unknown2: 1)]
			[DisplayName("Bleeding -resistant")]
			[Description("The difficulty takes to abnormal bleeding")]
			[DefaultValue((UInt16)0)]
			public UInt16 ResistBleed {
				get { return resistBleed; }
				set {
					if ((double)value < 0 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for " + resistBleedProperty.Name + ".");
					SetProperty(ref resistBleed, ref value, resistBleedProperty);
				}
			}

			/// <summary>Curse resistance</summary>
			/// <remarks>
			/// Japanese short name: "呪耐性", Google translated: "Curse resistance".
			/// Japanese description: "呪状態異常へのかかりにくさ", Google translated: "The difficulty takes to curse abnormal state".
			/// </remarks>
			[ParameterTableRowAttribute("resist_curse", index: 73, minimum: 0, maximum: 999, step: 1, sortOrder: 45100, unknown2: 1)]
			[DisplayName("Curse resistance")]
			[Description("The difficulty takes to curse abnormal state")]
			[DefaultValue((UInt16)0)]
			public UInt16 ResistCurse {
				get { return resistCurse; }
				set {
					if ((double)value < 0 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for " + ResistCurseProperty.Name + ".");
					SetProperty(ref resistCurse, ref value, ResistCurseProperty);
				}
			}

			/// <summary>Wandering ghost when replacement model ID</summary>
			/// <remarks>
			/// Japanese short name: "徘徊ゴースト時差し替えモデルID", Google translated: "Wandering ghost when replacement model ID".
			/// Japanese description: "徘徊ゴースト化したときの差し替えモデル、テクスチャID", Google translated: "Replacement model at the time of the wandering ghost , texture ID".
			/// </remarks>
			[ParameterTableRowAttribute("ghostModelId", index: 74, minimum: -1, maximum: 9999, step: 1, sortOrder: 99000, unknown2: 1)]
			[DisplayName("Wandering ghost when replacement model ID")]
			[Description("Replacement model at the time of the wandering ghost , texture ID")]
			[DefaultValue((Int16)(-1))]
			public Int16 GhostModelId {
				get { return ghostModelId; }
				set {
					if ((double)value < -1 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 9999 for " + GhostModelIdProperty.Name + ".");
					SetProperty(ref ghostModelId, ref value, GhostModelIdProperty);
				}
			}

			/// <summary>Normal time replacement resource ID</summary>
			/// <remarks>
			/// Japanese short name: "通常時差し替えリソースID", Google translated: "Normal time replacement resource ID".
			/// Japanese description: "通常時のリソースID差し替え（むやみに使わないこと）", Google translated: "( Not to use it unnecessarily ) resource ID replacement of normal time".
			/// </remarks>
			[ParameterTableRowAttribute("normalChangeResouceId", index: 75, minimum: -1, maximum: 9999, step: 1, sortOrder: 100000, unknown2: 1)]
			[DisplayName("Normal time replacement resource ID")]
			[Description("( Not to use it unnecessarily ) resource ID replacement of normal time")]
			[DefaultValue((Int16)(-1))]
			public Int16 NormalChangeResouceId {
				get { return normalChangeResouceId; }
				set {
					if ((double)value < -1 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 9999 for " + NormalChangeResouceIdProperty.Name + ".");
					SetProperty(ref normalChangeResouceId, ref value, NormalChangeResouceIdProperty);
				}
			}

			/// <summary>Guard range [deg]</summary>
			/// <remarks>
			/// Japanese short name: "ガード範囲[deg]", Google translated: "Guard range [deg]".
			/// Japanese description: "武器のガード時の防御発生範囲角度.保留中", Google translated: ". Pending defense occurrence range angle of the guard when the weapon".
			/// </remarks>
			[ParameterTableRowAttribute("guardAngle", index: 76, minimum: 0, maximum: 180, step: 1, sortOrder: 54000, unknown2: 1)]
			[DisplayName("Guard range [deg]")]
			[Description(". Pending defense occurrence range angle of the guard when the weapon")]
			[DefaultValue((Int16)0)]
			public Int16 GuardAngle {
				get { return guardAngle; }
				set {
					if ((double)value < 0 || (double)value > 180)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 180 for " + GuardAngleProperty.Name + ".");
					SetProperty(ref guardAngle, ref value, GuardAngleProperty);
				}
			}

			/// <summary>Slashing attack cut rates [ %]</summary>
			/// <remarks>
			/// Japanese short name: "斬撃攻撃カット率[％]", Google translated: "Slashing attack cut rates [ %]".
			/// Japanese description: "攻撃タイプを見て、斬撃属性のダメージを何％カットするか？を指定", Google translated: "Look at the type of attack , what percentage you can either cut the damage of撃属of Zan ? Specify".
			/// </remarks>
			[ParameterTableRowAttribute("slashGuardCutRate", index: 77, minimum: -100, maximum: 100, step: 1, sortOrder: 56000, unknown2: 1)]
			[DisplayName("Slashing attack cut rates [ %]")]
			[Description("Look at the type of attack , what percentage you can either cut the damage of撃属of Zan ? Specify")]
			[DefaultValue((Int16)0)]
			public Int16 SlashGuardCutRate {
				get { return slashGuardCutRate; }
				set {
					if ((double)value < -100 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -100 to 100 for " + SlashGuardCutRateProperty.Name + ".");
					SetProperty(ref slashGuardCutRate, ref value, SlashGuardCutRateProperty);
				}
			}

			/// <summary>Blow attack cut rates [ %]</summary>
			/// <remarks>
			/// Japanese short name: "打撃攻撃カット率[％]", Google translated: "Blow attack cut rates [ %]".
			/// Japanese description: "攻撃タイプを見て、打撃属性のダメージを何％カットするか？を指定", Google translated: "Look at the type of attack , what percentage you can either cut the damage of blow attribute ? Specify".
			/// </remarks>
			[ParameterTableRowAttribute("blowGuardCutRate", index: 78, minimum: -100, maximum: 100, step: 1, sortOrder: 57000, unknown2: 1)]
			[DisplayName("Blow attack cut rates [ %]")]
			[Description("Look at the type of attack , what percentage you can either cut the damage of blow attribute ? Specify")]
			[DefaultValue((Int16)0)]
			public Int16 BlowGuardCutRate {
				get { return blowGuardCutRate; }
				set {
					if ((double)value < -100 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -100 to 100 for " + BlowGuardCutRateProperty.Name + ".");
					SetProperty(ref blowGuardCutRate, ref value, BlowGuardCutRateProperty);
				}
			}

			/// <summary>Piercing attack cut rates [ %]</summary>
			/// <remarks>
			/// Japanese short name: "刺突攻撃カット率[％]", Google translated: "Piercing attack cut rates [ %]".
			/// Japanese description: "攻撃タイプを見て、刺突属性のダメージを何％カットするか？を指定", Google translated: "Look at the type of attack , what percentage you can either cut the damage of thorn突属efficient? Specify".
			/// </remarks>
			[ParameterTableRowAttribute("thrustGuardCutRate", index: 79, minimum: -100, maximum: 100, step: 1, sortOrder: 58000, unknown2: 1)]
			[DisplayName("Piercing attack cut rates [ %]")]
			[Description("Look at the type of attack , what percentage you can either cut the damage of thorn突属efficient? Specify")]
			[DefaultValue((Int16)0)]
			public Int16 ThrustGuardCutRate {
				get { return thrustGuardCutRate; }
				set {
					if ((double)value < -100 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -100 to 100 for " + ThrustGuardCutRateProperty.Name + ".");
					SetProperty(ref thrustGuardCutRate, ref value, ThrustGuardCutRateProperty);
				}
			}

			/// <summary>SA endurance</summary>
			/// <remarks>
			/// Japanese short name: "SA耐久力", Google translated: "SA endurance".
			/// Japanese description: "スーパーアーマー耐久値", Google translated: "Super Armor Durability".
			/// </remarks>
			[ParameterTableRowAttribute("superArmorDurability", index: 80, minimum: -1, maximum: 9999, step: 1, sortOrder: 39100, unknown2: 1)]
			[DisplayName("SA endurance")]
			[Description("Super Armor Durability")]
			[DefaultValue((Int16)0)]
			public Int16 SuperArmorDurability {
				get { return superArmorDurability; }
				set {
					if ((double)value < -1 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 9999 for " + SuperArmorDurabilityProperty.Name + ".");
					SetProperty(ref superArmorDurability, ref value, SuperArmorDurabilityProperty);
				}
			}

			/// <summary>Normal time replacement texture character ID</summary>
			/// <remarks>
			/// Japanese short name: "通常時差し替えテクスチャキャラID", Google translated: "Normal time replacement texture character ID".
			/// Japanese description: "通常時差し替えテクスチャキャラID（むやみに使わないこと）", Google translated: "( Not to use it unnecessarily ) normal time replacement texture character ID".
			/// </remarks>
			[ParameterTableRowAttribute("normalChangeTexChrId", index: 81, minimum: -1, maximum: 9999, step: 1, sortOrder: 100100, unknown2: 1)]
			[DisplayName("Normal time replacement texture character ID")]
			[Description("( Not to use it unnecessarily ) normal time replacement texture character ID")]
			[DefaultValue((Int16)(-1))]
			public Int16 NormalChangeTexChrId {
				get { return normalChangeTexChrId; }
				set {
					if ((double)value < -1 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 9999 for " + NormalChangeTexChrIdProperty.Name + ".");
					SetProperty(ref normalChangeTexChrId, ref value, NormalChangeTexChrIdProperty);
				}
			}

			/// <summary>Display format of drop items</summary>
			/// <remarks>
			/// Japanese short name: "ドロップアイテムの表示形式", Google translated: "Display format of drop items".
			/// Japanese description: "アイテムドロップ時の表示方法(死体発光orアイテム表示)", Google translated: "Display how the item -drop ( dead body emission or item display )".
			/// </remarks>
			[ParameterTableRowAttribute("dropType", index: 82, minimum: 0, maximum: 1, step: 1, sortOrder: 26000, unknown2: 1)]
			[DisplayName("Display format of drop items")]
			[Description("Display how the item -drop ( dead body emission or item display )")]
			[DefaultValue((NpcItemDropType)0)]
			public NpcItemDropType DropType {
				get { return dropType; }
				set { SetProperty(ref dropType, ref value, DropTypeProperty); }
			}

			/// <summary>Knock back cut rates [ %]</summary>
			/// <remarks>
			/// Japanese short name: "ノックバックカット率[％]", Google translated: "Knock back cut rates [ %]".
			/// Japanese description: "ノックバックダメージを受けたときの減少値／具体的には、攻撃側のノックバック初速度をカットする", Google translated: "The decrease in value / concrete when subjected to knock back damage , to cut the knock back initial velocity of the attacking".
			/// </remarks>
			[ParameterTableRowAttribute("knockbackRate", index: 83, minimum: 0, maximum: 100, step: 1, sortOrder: 41000, unknown2: 1)]
			[DisplayName("Knock back cut rates [ %]")]
			[Description("The decrease in value / concrete when subjected to knock back damage , to cut the knock back initial velocity of the attacking")]
			[DefaultValue((Byte)0)]
			public Byte KnockbackRate {
				get { return knockbackRate; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for " + KnockbackRateProperty.Name + ".");
					SetProperty(ref knockbackRate, ref value, KnockbackRateProperty);
				}
			}

			/// <summary>Knock back parameter ID</summary>
			/// <remarks>
			/// Japanese short name: "ノックバックパラメータID", Google translated: "Knock back parameter ID".
			/// Japanese description: "ノックバック時に使用するパラメータIDを設定", Google translated: "To set the parameters ID that is used to knock -back".
			/// </remarks>
			[ParameterTableRowAttribute("knockbackParamId", index: 84, minimum: 0, maximum: 255, step: 1, sortOrder: 41100, unknown2: 1)]
			[DisplayName("Knock back parameter ID")]
			[Description("To set the parameters ID that is used to knock -back")]
			[DefaultValue((Byte)0)]
			public Byte KnockbackParamId {
				get { return knockbackParamId; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for " + KnockbackParamIdProperty.Name + ".");
					SetProperty(ref knockbackParamId, ref value, KnockbackParamIdProperty);
				}
			}

			/// <summary>Fall damage reduction correction [ %]</summary>
			/// <remarks>
			/// Japanese short name: "落下ダメージ軽減補正[％]", Google translated: "Fall damage reduction correction [ %]".
			/// Japanese description: "落下ダメージ軽減補正[％]", Google translated: "Fall damage reduction correction [ %]".
			/// </remarks>
			[ParameterTableRowAttribute("fallDamageDump", index: 85, minimum: 0, maximum: 100, step: 1, sortOrder: 42000, unknown2: 1)]
			[DisplayName("Fall damage reduction correction [ %]")]
			[Description("Fall damage reduction correction [ %]")]
			[DefaultValue((Byte)0)]
			public Byte FallDamageDump {
				get { return fallDamageDump; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for " + FallDamageDumpProperty.Name + ".");
					SetProperty(ref fallDamageDump, ref value, FallDamageDumpProperty);
				}
			}

			/// <summary>Stamina attack cut rates [ %]</summary>
			/// <remarks>
			/// Japanese short name: "スタミナ攻撃カット率[％]", Google translated: "Stamina attack cut rates [ %]".
			/// Japanese description: "ガード成功時に、敵のスタミナ攻撃に対する防御力", Google translated: "To guard success , defense against enemy attack stamina".
			/// </remarks>
			[ParameterTableRowAttribute("staminaGuardDef", index: 86, minimum: 0, maximum: 100, step: 1, sortOrder: 66000, unknown2: 1)]
			[DisplayName("Stamina attack cut rates [ %]")]
			[Description("To guard success , defense against enemy attack stamina")]
			[DefaultValue((Byte)0)]
			public Byte StaminaGuardDef {
				get { return staminaGuardDef; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for " + StaminaGuardDefProperty.Name + ".");
					SetProperty(ref staminaGuardDef, ref value, StaminaGuardDefProperty);
				}
			}

			/// <summary>PC- Black</summary>
			/// <remarks>
			/// Japanese short name: "PC-黒", Google translated: "PC- Black".
			/// Japanese description: "QWC変化量　PC属性値黒", Google translated: "QWC amount of change attribute value PC black".
			/// </remarks>
			[ParameterTableRowAttribute("pcAttrB", index: 87, minimum: 0, maximum: 200, step: 1, sortOrder: 75000, unknown2: 1)]
			[DisplayName("PC- Black")]
			[Description("QWC amount of change attribute value PC black")]
			[DefaultValue((Byte)0)]
			public Byte PcAttrB {
				get { return pcAttrB; }
				set {
					if ((double)value < 0 || (double)value > 200)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 200 for " + PcAttrBProperty.Name + ".");
					SetProperty(ref pcAttrB, ref value, PcAttrBProperty);
				}
			}

			/// <summary>PC- White</summary>
			/// <remarks>
			/// Japanese short name: "PC-白", Google translated: "PC- White".
			/// Japanese description: "QWC変化量　PC属性値白", Google translated: "QWC variation PC attribute value white".
			/// </remarks>
			[ParameterTableRowAttribute("pcAttrW", index: 88, minimum: 0, maximum: 200, step: 1, sortOrder: 76000, unknown2: 1)]
			[DisplayName("PC- White")]
			[Description("QWC variation PC attribute value white")]
			[DefaultValue((Byte)0)]
			public Byte PcAttrW {
				get { return pcAttrW; }
				set {
					if ((double)value < 0 || (double)value > 200)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 200 for " + PcAttrWProperty.Name + ".");
					SetProperty(ref pcAttrW, ref value, PcAttrWProperty);
				}
			}

			/// <summary>PC- left</summary>
			/// <remarks>
			/// Japanese short name: "PC-左", Google translated: "PC- left".
			/// Japanese description: "QWC変化量　PC属性値左", Google translated: "QWC variation PC attribute value left".
			/// </remarks>
			[ParameterTableRowAttribute("pcAttrL", index: 89, minimum: 0, maximum: 200, step: 1, sortOrder: 77000, unknown2: 1)]
			[DisplayName("PC- left")]
			[Description("QWC variation PC attribute value left")]
			[DefaultValue((Byte)0)]
			public Byte PcAttrL {
				get { return pcAttrL; }
				set {
					if ((double)value < 0 || (double)value > 200)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 200 for " + PcAttrLProperty.Name + ".");
					SetProperty(ref pcAttrL, ref value, PcAttrLProperty);
				}
			}

			/// <summary>PC- right</summary>
			/// <remarks>
			/// Japanese short name: "PC-右", Google translated: "PC- right".
			/// Japanese description: "QWC変化量　PC属性値右", Google translated: "QWC variation PC attribute value right".
			/// </remarks>
			[ParameterTableRowAttribute("pcAttrR", index: 90, minimum: 0, maximum: 200, step: 1, sortOrder: 78000, unknown2: 1)]
			[DisplayName("PC- right")]
			[Description("QWC variation PC attribute value right")]
			[DefaultValue((Byte)0)]
			public Byte PcAttrR {
				get { return pcAttrR; }
				set {
					if ((double)value < 0 || (double)value > 200)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 200 for " + PcAttrRProperty.Name + ".");
					SetProperty(ref pcAttrR, ref value, PcAttrRProperty);
				}
			}

			/// <summary>Area - black</summary>
			/// <remarks>
			/// Japanese short name: "エリア-黒", Google translated: "Area - black".
			/// Japanese description: "QWC変化量　エリア属性値黒", Google translated: "QWC variation area attribute value black".
			/// </remarks>
			[ParameterTableRowAttribute("areaAttrB", index: 91, minimum: 0, maximum: 200, step: 1, sortOrder: 79000, unknown2: 1)]
			[DisplayName("Area - black")]
			[Description("QWC variation area attribute value black")]
			[DefaultValue((Byte)0)]
			public Byte AreaAttrB {
				get { return areaAttrB; }
				set {
					if ((double)value < 0 || (double)value > 200)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 200 for " + AreaAttrBProperty.Name + ".");
					SetProperty(ref areaAttrB, ref value, AreaAttrBProperty);
				}
			}

			/// <summary>Area - White</summary>
			/// <remarks>
			/// Japanese short name: "エリア-白", Google translated: "Area - White".
			/// Japanese description: "QWC変化量　エリア属性値白", Google translated: "QWC variation area attribute value white".
			/// </remarks>
			[ParameterTableRowAttribute("areaAttrW", index: 92, minimum: 0, maximum: 200, step: 1, sortOrder: 80000, unknown2: 1)]
			[DisplayName("Area - White")]
			[Description("QWC variation area attribute value white")]
			[DefaultValue((Byte)0)]
			public Byte AreaAttrW {
				get { return areaAttrW; }
				set {
					if ((double)value < 0 || (double)value > 200)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 200 for " + AreaAttrWProperty.Name + ".");
					SetProperty(ref areaAttrW, ref value, AreaAttrWProperty);
				}
			}

			/// <summary>Area - left</summary>
			/// <remarks>
			/// Japanese short name: "エリア-左", Google translated: "Area - left".
			/// Japanese description: "QWC変化量　エリア属性値左", Google translated: "QWC variation area attribute value left".
			/// </remarks>
			[ParameterTableRowAttribute("areaAttrL", index: 93, minimum: 0, maximum: 200, step: 1, sortOrder: 81000, unknown2: 1)]
			[DisplayName("Area - left")]
			[Description("QWC variation area attribute value left")]
			[DefaultValue((Byte)0)]
			public Byte AreaAttrL {
				get { return areaAttrL; }
				set {
					if ((double)value < 0 || (double)value > 200)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 200 for " + AreaAttrLProperty.Name + ".");
					SetProperty(ref areaAttrL, ref value, AreaAttrLProperty);
				}
			}

			/// <summary>Area - right</summary>
			/// <remarks>
			/// Japanese short name: "エリア-右", Google translated: "Area - right".
			/// Japanese description: "QWC変化量　エリア属性値右", Google translated: "QWC variation area attribute value right".
			/// </remarks>
			[ParameterTableRowAttribute("areaAttrR", index: 94, minimum: 0, maximum: 200, step: 1, sortOrder: 82000, unknown2: 1)]
			[DisplayName("Area - right")]
			[Description("QWC variation area attribute value right")]
			[DefaultValue((Byte)0)]
			public Byte AreaAttrR {
				get { return areaAttrR; }
				set {
					if ((double)value < 0 || (double)value > 200)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 200 for " + AreaAttrRProperty.Name + ".");
					SetProperty(ref areaAttrR, ref value, AreaAttrRProperty);
				}
			}

			/// <summary>MP recovery basic rate [% / s]</summary>
			/// <remarks>
			/// Japanese short name: "MP回復基本速度[％/s]", Google translated: "MP recovery basic rate [% / s]".
			/// Japanese description: "MP回復基本速度[％/s]", Google translated: "MP recovery basic rate [% / s]".
			/// </remarks>
			[ParameterTableRowAttribute("mpRecoverBaseVel", index: 95, minimum: 0, maximum: 100, step: 1, sortOrder: 23000, unknown2: 1)]
			[DisplayName("MP recovery basic rate [% / s]")]
			[Description("MP recovery basic rate [% / s]")]
			[DefaultValue((Byte)0)]
			public Byte MpRecoverBaseVel {
				get { return mpRecoverBaseVel; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for " + MpRecoverBaseVelProperty.Name + ".");
					SetProperty(ref mpRecoverBaseVel, ref value, MpRecoverBaseVelProperty);
				}
			}

			/// <summary>Repelling when damage attenuation rate [ %]</summary>
			/// <remarks>
			/// Japanese short name: "はじき時ダメージ減衰率[%]", Google translated: "Repelling when damage attenuation rate [ %]".
			/// Japanese description: "攻撃をはじいた時にダメージを減衰する値を設定", Google translated: "The set value for attenuating the damage when flicked attack".
			/// </remarks>
			[ParameterTableRowAttribute("flickDamageCutRate", index: 96, minimum: 0, maximum: 100, step: 1, sortOrder: 40100, unknown2: 1)]
			[DisplayName("Repelling when damage attenuation rate [ %]")]
			[Description("The set value for attenuating the damage when flicked attack")]
			[DefaultValue((Byte)0)]
			public Byte FlickDamageCutRate {
				get { return flickDamageCutRate; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for " + FlickDamageCutRateProperty.Name + ".");
					SetProperty(ref flickDamageCutRate, ref value, FlickDamageCutRateProperty);
				}
			}

			/// <summary>Default LOD Parham ID</summary>
			/// <remarks>
			/// Japanese short name: "デフォルトLODパラムID", Google translated: "Default LOD Parham ID".
			/// Japanese description: "デフォルトLODパラムID(-1：なし)", Google translated: "Default LOD Parham ID, (-1 : none)".
			/// </remarks>
			[ParameterTableRowAttribute("defaultLodParamId", index: 97, minimum: -1, maximum: 127, step: 1, sortOrder: 200000, unknown2: 0)]
			[DisplayName("Default LOD Parham ID")]
			[Description("Default LOD Parham ID, (-1 : none)")]
			[DefaultValue((SByte)(-1))]
			public SByte DefaultLodParamId {
				get { return defaultLodParamId; }
				set {
					if ((double)value < -1 || (double)value > 127)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 127 for " + DefaultLodParamIdProperty.Name + ".");
					SetProperty(ref defaultLodParamId, ref value, DefaultLodParamIdProperty);
				}
			}

			/// <summary>Drawing type</summary>
			/// <remarks>
			/// Japanese short name: "描画タイプ", Google translated: "Drawing type".
			/// Japanese description: "描画タイプ", Google translated: "Drawing type".
			/// </remarks>
			[ParameterTableRowAttribute("drawType", index: 98, minimum: 0, maximum: 255, step: 1, sortOrder: 4000, unknown2: 1)]
			[DisplayName("Drawing type")]
			[Description("Drawing type")]
			[DefaultValue((NpcDrawType)0)]
			public NpcDrawType DrawType {
				get { return drawType; }
				set { SetProperty(ref drawType, ref value, DrawTypeProperty); }
			}

			/// <summary>NPC type</summary>
			/// <remarks>
			/// Japanese short name: "NPCタイプ", Google translated: "NPC type".
			/// Japanese description: "NPCの種類.ザコ敵/ボス敵が区別されていればOK", Google translated: "OK. Zako enemy / boss enemy type of NPC , if it is distinguished".
			/// </remarks>
			[ParameterTableRowAttribute("npcType", index: 99, minimum: 0, maximum: 255, step: 1, sortOrder: 5000, unknown2: 1)]
			[DisplayName("NPC type")]
			[Description("OK. Zako enemy / boss enemy type of NPC , if it is distinguished")]
			[DefaultValue((NpcType)0)]
			public NpcType NpcType {
				get { return npcType; }
				set { SetProperty(ref npcType, ref value, NpcTypeProperty); }
			}

			/// <summary>Team Type</summary>
			/// <remarks>
			/// Japanese short name: "チームタイプ", Google translated: "Team Type".
			/// Japanese description: "NPCの攻撃が当たる/当たらない、狙う/狙わない設定", Google translated: "Attack the NPC hit / no hit , Aiming / settings that are not targeted".
			/// </remarks>
			[ParameterTableRowAttribute("teamType", index: 100, minimum: 0, maximum: 255, step: 1, sortOrder: 6000, unknown2: 1)]
			[DisplayName("Team Type")]
			[Description("Attack the NPC hit / no hit , Aiming / settings that are not targeted")]
			[DefaultValue((NpcTemaType)0)]
			public NpcTemaType TeamType {
				get { return teamType; }
				set { SetProperty(ref teamType, ref value, TeamTypeProperty); }
			}

			/// <summary>Movement type</summary>
			/// <remarks>
			/// Japanese short name: "移動タイプ", Google translated: "Movement type".
			/// Japanese description: "移動方法。これにより制御が変更される.", Google translated: "How to move . Control is changed by this .".
			/// </remarks>
			[ParameterTableRowAttribute("moveType", index: 101, minimum: 0, maximum: 255, step: 1, sortOrder: 7000, unknown2: 1)]
			[DisplayName("Movement type")]
			[Description("How to move . Control is changed by this .")]
			[DefaultValue((NpcMoveType)0)]
			public NpcMoveType MoveType {
				get { return moveType; }
				set { SetProperty(ref moveType, ref value, MoveTypeProperty); }
			}

			/// <summary>Lock distance</summary>
			/// <remarks>
			/// Japanese short name: "ロック距離", Google translated: "Lock distance".
			/// Japanese description: "ロックオンできる距離[m]", Google translated: "The operating distance of the lock-on [m]".
			/// </remarks>
			[ParameterTableRowAttribute("lockDist", index: 102, minimum: 0, maximum: 100, step: 1, sortOrder: 10000, unknown2: 1)]
			[DisplayName("Lock distance")]
			[Description("The operating distance of the lock-on [m]")]
			[DefaultValue((Byte)0)]
			public Byte LockDist {
				get { return lockDist; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for " + LockDistProperty.Name + ".");
					SetProperty(ref lockDist, ref value, LockDistProperty);
				}
			}

			/// <summary>Defense material [ SE ]</summary>
			/// <remarks>
			/// Japanese short name: "防御材質【SE】", Google translated: "Defense material [ SE ]".
			/// Japanese description: "ダメージを受けたときに鳴らすＳＥを判定する。見た目で設定してＯＫ.", Google translated: "To determine the SE to sound when it is damaged. OK set in appearance .".
			/// </remarks>
			[ParameterTableRowAttribute("material", index: 103, minimum: 0, maximum: 255, step: 1, sortOrder: 17000, unknown2: 1)]
			[DisplayName("Defense material [ SE ]")]
			[Description("To determine the SE to sound when it is damaged. OK set in appearance .")]
			[DefaultValue((WeaponMaterialDefend)0)]
			public WeaponMaterialDefend Material {
				get { return material; }
				set { SetProperty(ref material, ref value, MaterialProperty); }
			}

			/// <summary>Defense material [ SFX ]</summary>
			/// <remarks>
			/// Japanese short name: "防御材質【SFX】", Google translated: "Defense material [ SFX ]".
			/// Japanese description: "ダメージを受けたときに発生するSFXを判定する。見た目で設定してＯＫ.", Google translated: "To determine the SFX that occurs when you take damage . OK set in appearance .".
			/// </remarks>
			[ParameterTableRowAttribute("materialSfx", index: 104, minimum: 0, maximum: 255, step: 1, sortOrder: 18000, unknown2: 1)]
			[DisplayName("Defense material [ SFX ]")]
			[Description("To determine the SFX that occurs when you take damage . OK set in appearance .")]
			[DefaultValue((WeaponMaterialDefendSound)0)]
			public WeaponMaterialDefendSound MaterialSfx {
				get { return materialSfx; }
				set { SetProperty(ref materialSfx, ref value, MaterialSfxProperty); }
			}

			/// <summary>Defense material [ SE ]</summary>
			/// <remarks>
			/// Japanese short name: "防御材質【SE】", Google translated: "Defense material [ SE ]".
			/// Japanese description: "弱点部位ダメージを受けた時に鳴らすSEを判定する。", Google translated: "To determine the SE to sound when subjected to weakness site damage .".
			/// </remarks>
			[ParameterTableRowAttribute("material_Weak", index: 105, minimum: 0, maximum: 255, step: 1, sortOrder: 51200, unknown2: 1)]
			[DisplayName("Defense material [ SE ]")]
			[Description("To determine the SE to sound when subjected to weakness site damage .")]
			[DefaultValue((WeaponMaterialDefend)0)]
			public WeaponMaterialDefend Material_Weak {
				get { return material_Weak; }
				set { SetProperty(ref material_Weak, ref value, Material_WeakProperty); }
			}

			/// <summary>Defense material [ SFX ]</summary>
			/// <remarks>
			/// Japanese short name: "防御材質【SFX】", Google translated: "Defense material [ SFX ]".
			/// Japanese description: "弱点部位ダメージを受けた時に発生するSFXを判定する。", Google translated: "To determine the SFX which occurs when it is received weaknesses site damage .".
			/// </remarks>
			[ParameterTableRowAttribute("materialSfx_Weak", index: 106, minimum: 0, maximum: 255, step: 1, sortOrder: 51300, unknown2: 1)]
			[DisplayName("Defense material [ SFX ]")]
			[Description("To determine the SFX which occurs when it is received weaknesses site damage .")]
			[DefaultValue((WeaponMaterialDefendSound)0)]
			public WeaponMaterialDefendSound MaterialSfx_Weak {
				get { return materialSfx_Weak; }
				set { SetProperty(ref materialSfx_Weak, ref value, MaterialSfx_WeakProperty); }
			}

			/// <summary>Application site damage attack</summary>
			/// <remarks>
			/// Japanese short name: "部位ダメージ適用攻撃", Google translated: "Application site damage attack".
			/// Japanese description: "部位ダメージを適用する攻撃タイプを設定する", Google translated: "I set the attack type to apply the damage site".
			/// </remarks>
			[ParameterTableRowAttribute("partsDamageType", index: 107, minimum: 0, maximum: 2, step: 1, sortOrder: 51100, unknown2: 1)]
			[DisplayName("Application site damage attack")]
			[Description("I set the attack type to apply the damage site")]
			[DefaultValue((AttackParameterPartDamageType)0)]
			public AttackParameterPartDamageType PartsDamageType {
				get { return partsDamageType; }
				set { SetProperty(ref partsDamageType, ref value, PartsDamageTypeProperty); }
			}

			/// <summary>Maximum angle to fit the ups and downs</summary>
			/// <remarks>
			/// Japanese short name: "起伏にあわせる最大角度", Google translated: "Maximum angle to fit the ups and downs".
			/// Japanese description: "起伏に角度を合わせる場合の上限角度。全長が長い場合には低めに設定したほうがよいです。", Google translated: "Maximum angle of the case to match the angle in relief . Should I set lower is good if the total length is long.".
			/// </remarks>
			[ParameterTableRowAttribute("maxUndurationAng", index: 108, minimum: 0, maximum: 70, step: 1, sortOrder: 20000, unknown2: 1)]
			[DisplayName("Maximum angle to fit the ups and downs")]
			[Description("Maximum angle of the case to match the angle in relief . Should I set lower is good if the total length is long.")]
			[DefaultValue((Byte)0)]
			public Byte MaxUndurationAng {
				get { return maxUndurationAng; }
				set {
					if ((double)value < 0 || (double)value > 70)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 70 for " + MaxUndurationAngProperty.Name + ".");
					SetProperty(ref maxUndurationAng, ref value, MaxUndurationAngProperty);
				}
			}

			/// <summary>Guard level</summary>
			/// <remarks>
			/// Japanese short name: "ガードレベル", Google translated: "Guard level".
			/// Japanese description: "ガードしたとき、敵の攻撃をどのガードモーションで受けるか？を決める", Google translated: "When you guard , you can either receive any motion guard enemy attacks ? I decide the".
			/// </remarks>
			[ParameterTableRowAttribute("guardLevel", index: 109, minimum: 0, maximum: 100, step: 1, sortOrder: 52000, unknown2: 1)]
			[DisplayName("Guard level")]
			[Description("When you guard , you can either receive any motion guard enemy attacks ? I decide the")]
			[DefaultValue((SByte)0)]
			public SByte GuardLevel {
				get { return guardLevel; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for " + GuardLevelProperty.Name + ".");
					SetProperty(ref guardLevel, ref value, GuardLevelProperty);
				}
			}

			/// <summary>SFX combustion type</summary>
			/// <remarks>
			/// Japanese short name: "燃焼SFXタイプ", Google translated: "SFX combustion type".
			/// Japanese description: "燃焼時のSFXタイプ", Google translated: "SFX type of combustion".
			/// </remarks>
			[ParameterTableRowAttribute("burnSfxType", index: 110, minimum: 0, maximum: 255, step: 1, sortOrder: 53000, unknown2: 1)]
			[DisplayName("SFX combustion type")]
			[Description("SFX type of combustion")]
			[DefaultValue((NpcBurnType)0)]
			public NpcBurnType BurnSfxType {
				get { return burnSfxType; }
				set { SetProperty(ref burnSfxType, ref value, BurnSfxTypeProperty); }
			}

			/// <summary>Poison -resistant cut rates [ %]</summary>
			/// <remarks>
			/// Japanese short name: "毒耐性カット率[％]", Google translated: "Poison -resistant cut rates [ %]".
			/// Japanese description: "毒にする攻撃力（特殊効果パラメータに設定）をどれだけカットするか", Google translated: "You can cut much (setting special effects parameters) attack force to poison".
			/// </remarks>
			[ParameterTableRowAttribute("poisonGuardResist", index: 111, minimum: 0, maximum: 100, step: 1, sortOrder: 63000, unknown2: 1)]
			[DisplayName("Poison -resistant cut rates [ %]")]
			[Description("You can cut much (setting special effects parameters) attack force to poison")]
			[DefaultValue((SByte)0)]
			public SByte PoisonGuardResist {
				get { return poisonGuardResist; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for " + PoisonGuardResistProperty.Name + ".");
					SetProperty(ref poisonGuardResist, ref value, PoisonGuardResistProperty);
				}
			}

			/// <summary>Plague attack cut rates [ %]</summary>
			/// <remarks>
			/// Japanese short name: "疫病攻撃カット率[％]", Google translated: "Plague attack cut rates [ %]".
			/// Japanese description: "疫病にする攻撃力（特殊効果パラメータに設定）をどれだけカットするか", Google translated: "You can cut much (setting special effects parameters) attack force to plague".
			/// </remarks>
			[ParameterTableRowAttribute("diseaseGuardResist", index: 112, minimum: 0, maximum: 100, step: 1, sortOrder: 64000, unknown2: 1)]
			[DisplayName("Plague attack cut rates [ %]")]
			[Description("You can cut much (setting special effects parameters) attack force to plague")]
			[DefaultValue((SByte)0)]
			public SByte DiseaseGuardResist {
				get { return diseaseGuardResist; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for " + DiseaseGuardResistProperty.Name + ".");
					SetProperty(ref diseaseGuardResist, ref value, DiseaseGuardResistProperty);
				}
			}

			/// <summary>Bleeding attack cut rates [ %]</summary>
			/// <remarks>
			/// Japanese short name: "出血攻撃カット率[％]", Google translated: "Bleeding attack cut rates [ %]".
			/// Japanese description: "出血にする攻撃力（特殊効果パラメータに設定）をどれだけカットするか", Google translated: "You can cut much (setting special effects parameters) attack force to bleeding".
			/// </remarks>
			[ParameterTableRowAttribute("bloodGuardResist", index: 113, minimum: 0, maximum: 100, step: 1, sortOrder: 65000, unknown2: 1)]
			[DisplayName("Bleeding attack cut rates [ %]")]
			[Description("You can cut much (setting special effects parameters) attack force to bleeding")]
			[DefaultValue((SByte)0)]
			public SByte BloodGuardResist {
				get { return bloodGuardResist; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for " + BloodGuardResistProperty.Name + ".");
					SetProperty(ref bloodGuardResist, ref value, BloodGuardResistProperty);
				}
			}

			/// <summary>Curse attack cut rates [ %]</summary>
			/// <remarks>
			/// Japanese short name: "呪攻撃カット率[％]", Google translated: "Curse attack cut rates [ %]".
			/// Japanese description: "呪にする攻撃力（特殊効果パラメータに設定）をどれだけカットするか", Google translated: "You can cut much (setting special effects parameters) attack force to curse".
			/// </remarks>
			[ParameterTableRowAttribute("curseGuardResist", index: 114, minimum: 0, maximum: 100, step: 1, sortOrder: 65100, unknown2: 1)]
			[DisplayName("Curse attack cut rates [ %]")]
			[Description("You can cut much (setting special effects parameters) attack force to curse")]
			[DefaultValue((SByte)0)]
			public SByte CurseGuardResist {
				get { return curseGuardResist; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for " + CurseGuardResistProperty.Name + ".");
					SetProperty(ref curseGuardResist, ref value, CurseGuardResistProperty);
				}
			}

			/// <summary>Parry attack power</summary>
			/// <remarks>
			/// Japanese short name: "パリィ攻撃力", Google translated: "Parry attack power".
			/// Japanese description: "パリィ攻撃力。パリィする側が使用", Google translated: "Parry attack power . Side to use Parry".
			/// </remarks>
			[ParameterTableRowAttribute("parryAttack", index: 115, minimum: 0, maximum: 255, step: 1, sortOrder: 72000, unknown2: 1)]
			[DisplayName("Parry attack power")]
			[Description("Parry attack power . Side to use Parry")]
			[DefaultValue((Byte)0)]
			public Byte ParryAttack {
				get { return parryAttack; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for " + ParryAttackProperty.Name + ".");
					SetProperty(ref parryAttack, ref value, ParryAttackProperty);
				}
			}

			/// <summary>Parry Defense</summary>
			/// <remarks>
			/// Japanese short name: "パリィ防御力", Google translated: "Parry Defense".
			/// Japanese description: "パリィ防御力。パリィされる側が使用。", Google translated: "Parry Defense . The items that can be so Parry is used .".
			/// </remarks>
			[ParameterTableRowAttribute("parryDefence", index: 116, minimum: 0, maximum: 255, step: 1, sortOrder: 73000, unknown2: 1)]
			[DisplayName("Parry Defense")]
			[Description("Parry Defense . The items that can be so Parry is used .")]
			[DefaultValue((Byte)0)]
			public Byte ParryDefence {
				get { return parryDefence; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for " + ParryDefenceProperty.Name + ".");
					SetProperty(ref parryDefence, ref value, ParryDefenceProperty);
				}
			}

			/// <summary>SFX size</summary>
			/// <remarks>
			/// Japanese short name: "SFXサイズ", Google translated: "SFX size".
			/// Japanese description: "SFXサイズ", Google translated: "SFX size".
			/// </remarks>
			[ParameterTableRowAttribute("sfxSize", index: 117, minimum: 0, maximum: 255, step: 1, sortOrder: 97000, unknown2: 1)]
			[DisplayName("SFX size")]
			[Description("SFX size")]
			[DefaultValue((NpcSfxSize)0)]
			public NpcSfxSize SfxSize {
				get { return sfxSize; }
				set { SetProperty(ref sfxSize, ref value, SfxSizeProperty); }
			}

			/// <summary>Camera extrusion area radius [m]</summary>
			/// <remarks>
			/// Japanese short name: "カメラ押し出し領域半径[m]", Google translated: "Camera extrusion area radius [m]".
			/// Japanese description: "カメラ押し出し領域半径[m]", Google translated: "Camera extrusion area radius [m]".
			/// </remarks>
			[ParameterTableRowAttribute("pushOutCamRegionRadius", index: 118, minimum: 0, maximum: 255, step: 1, sortOrder: 98000, unknown2: 0)]
			[DisplayName("Camera extrusion area radius [m]")]
			[Description("Camera extrusion area radius [m]")]
			[DefaultValue((Byte)12)]
			public Byte PushOutCamRegionRadius {
				get { return pushOutCamRegionRadius; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for " + PushOutCamRegionRadiusProperty.Name + ".");
					SetProperty(ref pushOutCamRegionRadius, ref value, PushOutCamRegionRadiusProperty);
				}
			}

			/// <summary>Hit or stop</summary>
			/// <remarks>
			/// Japanese short name: "ヒットストップするか", Google translated: "Hit or stop".
			/// Japanese description: "ヒットストップ処理を行うかどうかの設定", Google translated: "Sets whether or not to hit stop processing".
			/// </remarks>
			[ParameterTableRowAttribute("hitStopType", index: 119, minimum: 0, maximum: 255, step: 1, sortOrder: 9510, unknown2: 1)]
			[DisplayName("Hit or stop")]
			[Description("Sets whether or not to hit stop processing")]
			[DefaultValue((NpcHitStopType)0)]
			public NpcHitStopType HitStopType {
				get { return hitStopType; }
				set { SetProperty(ref hitStopType, ref value, HitStopTypeProperty); }
			}

			/// <summary>Ladder on the end offset [1/10m]</summary>
			/// <remarks>
			/// Japanese short name: "はしご上終端オフセット[1/10m]", Google translated: "Ladder on the end offset [1/10m]".
			/// Japanese description: "はしご終端判定用オフセット上側", Google translated: "Ladder termination determination offset upper".
			/// </remarks>
			[ParameterTableRowAttribute("ladderEndChkOffsetTop", index: 120, minimum: 0, maximum: 255, step: 1, sortOrder: 16500, unknown2: 1)]
			[DisplayName("Ladder on the end offset [1/10m]")]
			[Description("Ladder termination determination offset upper")]
			[DefaultValue((Byte)15)]
			public Byte LadderEndChkOffsetTop {
				get { return ladderEndChkOffsetTop; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for " + LadderEndChkOffsetTopProperty.Name + ".");
					SetProperty(ref ladderEndChkOffsetTop, ref value, LadderEndChkOffsetTopProperty);
				}
			}

			/// <summary>Ladder under termination offset [1/10m]</summary>
			/// <remarks>
			/// Japanese short name: "はしご下終端オフセット[1/10m]", Google translated: "Ladder under termination offset [1/10m]".
			/// Japanese description: "はしご終端判定用オフセット下側", Google translated: "Ladder termination determination offset lower".
			/// </remarks>
			[ParameterTableRowAttribute("ladderEndChkOffsetLow", index: 121, minimum: 0, maximum: 255, step: 1, sortOrder: 16600, unknown2: 1)]
			[DisplayName("Ladder under termination offset [1/10m]")]
			[Description("Ladder termination determination offset lower")]
			[DefaultValue((Byte)8)]
			public Byte LadderEndChkOffsetLow {
				get { return ladderEndChkOffsetLow; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for " + LadderEndChkOffsetLowProperty.Name + ".");
					SetProperty(ref ladderEndChkOffsetLow, ref value, LadderEndChkOffsetLowProperty);
				}
			}

			/// <summary>Camera per hit ragdoll</summary>
			/// <remarks>
			/// Japanese short name: "カメラヒットあたりラグドール", Google translated: "Camera per hit ragdoll".
			/// Japanese description: "敵のラグドールにカメラがあたるか。(プレイヤにも当たるときのみ有効)", Google translated: "Camera or hit the enemy ragdoll . (Valid only when you hit even the player )".
			/// </remarks>
			[ParameterTableRowAttribute("useRagdollCamHit:1", index: 122, minimum: 0, maximum: 1, step: 1, sortOrder: 9100, unknown2: 1)]
			[DisplayName("Camera per hit ragdoll")]
			[Description("Camera or hit the enemy ragdoll . (Valid only when you hit even the player )")]
			[DefaultValue(false)]
			public Boolean UseRagdollCamHit {
				get { return GetBitProperty(0, 1, UseRagdollCamHitProperty) != 0; }
				set { SetBitProperty(0, 1, value ? 1 : 0, UseRagdollCamHitProperty); }
			}

			/// <summary>Disable the rigid cross hit</summary>
			/// <remarks>
			/// Japanese short name: "クロスリジッドヒットを無効", Google translated: "Disable the rigid cross hit".
			/// Japanese description: "クロスリジッドが自分に当たらないようにしたければ○", Google translated: "Rigid cross is if you want to avoid exposure to yourself ○".
			/// </remarks>
			[ParameterTableRowAttribute("disableClothRigidHit:1", index: 123, minimum: 0, maximum: 1, step: 1, sortOrder: 9500, unknown2: 1)]
			[DisplayName("Disable the rigid cross hit")]
			[Description("Rigid cross is if you want to avoid exposure to yourself ○")]
			[DefaultValue(false)]
			public Boolean DisableClothRigidHit {
				get { return GetBitProperty(1, 1, DisableClothRigidHitProperty) != 0; }
				set { SetBitProperty(1, 1, value ? 1 : 0, DisableClothRigidHitProperty); }
			}

			/// <summary>Per ragdoll</summary>
			/// <remarks>
			/// Japanese short name: "あたりラグドール", Google translated: "Per ragdoll".
			/// Japanese description: "敵のラグドールにプレイヤーがあたるか。デカキャラだけはラグドールにもプレイヤーがあたるようにしたいので、この設定でオンオフ.", Google translated: "Player or hit the ragdoll enemy . Player so want to hit even Ragdoll only Dekakyara , on-off in this setting .".
			/// </remarks>
			[ParameterTableRowAttribute("useRagdoll:1", index: 124, minimum: 0, maximum: 1, step: 1, sortOrder: 9000, unknown2: 1)]
			[DisplayName("Per ragdoll")]
			[Description("Player or hit the ragdoll enemy . Player so want to hit even Ragdoll only Dekakyara , on-off in this setting .")]
			[DefaultValue(false)]
			public Boolean UseRagdoll {
				get { return GetBitProperty(2, 1, UseRagdollProperty) != 0; }
				set { SetBitProperty(2, 1, value ? 1 : 0, UseRagdollProperty); }
			}

			/// <summary>Or daemon</summary>
			/// <remarks>
			/// Japanese short name: "デーモンか", Google translated: "Or daemon".
			/// Japanese description: "デーモンか。いまのところデモンブランドの効果があるかどうかだけ。", Google translated: "Or daemon . Only whether there is an effect of the Demon brand for now .".
			/// </remarks>
			[ParameterTableRowAttribute("isDemon:1", index: 125, minimum: 0, maximum: 1, step: 1, sortOrder: 11000, unknown2: 1)]
			[DisplayName("Or daemon")]
			[Description("Or daemon . Only whether there is an effect of the Demon brand for now .")]
			[DefaultValue(false)]
			public Boolean IsDemon {
				get { return GetBitProperty(3, 1, IsDemonProperty) != 0; }
				set { SetBitProperty(3, 1, value ? 1 : 0, IsDemonProperty); }
			}

			/// <summary>Body or spirit</summary>
			/// <remarks>
			/// Japanese short name: "霊体か", Google translated: "Body or spirit".
			/// Japanese description: "霊体か。ダメージ計算等が専用になります徘徊ゴーストと混同しないように注意", Google translated: "Body or spirit . Careful not to be confused with wandering ghost damage calculation or the like will be dedicated".
			/// </remarks>
			[ParameterTableRowAttribute("isGhost:1", index: 126, minimum: 0, maximum: 1, step: 1, sortOrder: 11010, unknown2: 1)]
			[DisplayName("Body or spirit")]
			[Description("Body or spirit . Careful not to be confused with wandering ghost damage calculation or the like will be dedicated")]
			[DefaultValue(false)]
			public Boolean IsGhost {
				get { return GetBitProperty(4, 1, IsGhostProperty) != 0; }
				set { SetBitProperty(4, 1, value ? 1 : 0, IsGhostProperty); }
			}

			/// <summary>Do no damage motion when the damage 0</summary>
			/// <remarks>
			/// Japanese short name: "ダメージ0のときにダメージモーションなしか", Google translated: "Do no damage motion when the damage 0".
			/// Japanese description: "ダメージ0のときにダメージモーションを再生しないか。", Google translated: "Do not play the damage motion when the damage 0 .".
			/// </remarks>
			[ParameterTableRowAttribute("isNoDamageMotion:1", index: 127, minimum: 0, maximum: 1, step: 1, sortOrder: 12000, unknown2: 1)]
			[DisplayName("Do no damage motion when the damage 0")]
			[Description("Do not play the damage motion when the damage 0 .")]
			[DefaultValue(false)]
			public Boolean IsNoDamageMotion {
				get { return GetBitProperty(5, 1, IsNoDamageMotionProperty) != 0; }
				set { SetBitProperty(5, 1, value ? 1 : 0, IsNoDamageMotionProperty); }
			}

			/// <summary>Or adjust the angle to relief</summary>
			/// <remarks>
			/// Japanese short name: "起伏に角度をあわせるか", Google translated: "Or adjust the angle to relief".
			/// Japanese description: "キャラの前後回転を地面の起伏に合わせるか。飛行キャラの場合は使用不可", Google translated: "Or match the relief of the ground before and after the rotation of the characters . In the case of flight character can not be used".
			/// </remarks>
			[ParameterTableRowAttribute("isUnduration:1", index: 128, minimum: 0, maximum: 1, step: 1, sortOrder: 19000, unknown2: 1)]
			[DisplayName("Or adjust the angle to relief")]
			[Description("Or match the relief of the ground before and after the rotation of the characters . In the case of flight character can not be used")]
			[DefaultValue(false)]
			public Boolean IsUnduration {
				get { return GetBitProperty(6, 1, IsUndurationProperty) != 0; }
				set { SetBitProperty(6, 1, value ? 1 : 0, IsUndurationProperty); }
			}

			/// <summary>Become wandering ghost</summary>
			/// <remarks>
			/// Japanese short name: "徘徊ゴーストになるか", Google translated: "Become wandering ghost".
			/// Japanese description: "プレイヤーがクライアントのときに徘徊ゴーストになるか", Google translated: "Player or become a wandering ghost when the client".
			/// </remarks>
			[ParameterTableRowAttribute("isChangeWanderGhost:1", index: 129, minimum: 0, maximum: 1, step: 1, sortOrder: 95000, unknown2: 1)]
			[DisplayName("Become wandering ghost")]
			[Description("Player or become a wandering ghost when the client")]
			[DefaultValue(false)]
			public Boolean IsChangeWanderGhost {
				get { return GetBitProperty(7, 1, IsChangeWanderGhostProperty) != 0; }
				set { SetBitProperty(7, 1, value ? 1 : 0, IsChangeWanderGhostProperty); }
			}

			/// <summary>Model display mask 0</summary>
			/// <remarks>
			/// Japanese short name: "モデル表示マスク0", Google translated: "Model display mask 0".
			/// Japanese description: "表示マスクに対応するモデルを表示します。", Google translated: "Show me the model corresponding to the display mask .".
			/// </remarks>
			[ParameterTableRowAttribute("modelDispMask0:1", index: 130, minimum: 0, maximum: 1, step: 1, sortOrder: 84000, unknown2: 1)]
			[DisplayName("Model display mask 0")]
			[Description("Show me the model corresponding to the display mask .")]
			[DefaultValue(false)]
			public Boolean ModelDispMask0 {
				get { return GetBitProperty(8, 1, ModelDispMask0Property) != 0; }
				set { SetBitProperty(8, 1, value ? 1 : 0, ModelDispMask0Property); }
			}

			/// <summary>Model display mask 1</summary>
			/// <remarks>
			/// Japanese short name: "モデル表示マスク1", Google translated: "Model display mask 1".
			/// Japanese description: "表示マスクに対応するモデルを表示します。", Google translated: "Show me the model corresponding to the display mask .".
			/// </remarks>
			[ParameterTableRowAttribute("modelDispMask1:1", index: 131, minimum: 0, maximum: 1, step: 1, sortOrder: 85000, unknown2: 1)]
			[DisplayName("Model display mask 1")]
			[Description("Show me the model corresponding to the display mask .")]
			[DefaultValue(false)]
			public Boolean ModelDispMask1 {
				get { return GetBitProperty(9, 1, ModelDispMask1Property) != 0; }
				set { SetBitProperty(9, 1, value ? 1 : 0, ModelDispMask1Property); }
			}

			/// <summary>Model display mask 2</summary>
			/// <remarks>
			/// Japanese short name: "モデル表示マスク2", Google translated: "Model display mask 2".
			/// Japanese description: "表示マスクに対応するモデルを表示します。", Google translated: "Show me the model corresponding to the display mask .".
			/// </remarks>
			[ParameterTableRowAttribute("modelDispMask2:1", index: 132, minimum: 0, maximum: 1, step: 1, sortOrder: 86000, unknown2: 1)]
			[DisplayName("Model display mask 2")]
			[Description("Show me the model corresponding to the display mask .")]
			[DefaultValue(false)]
			public Boolean ModelDispMask2 {
				get { return GetBitProperty(10, 1, ModelDispMask2Property) != 0; }
				set { SetBitProperty(10, 1, value ? 1 : 0, ModelDispMask2Property); }
			}

			/// <summary>Model display mask 3</summary>
			/// <remarks>
			/// Japanese short name: "モデル表示マスク3", Google translated: "Model display mask 3".
			/// Japanese description: "表示マスクに対応するモデルを表示します。", Google translated: "Show me the model corresponding to the display mask .".
			/// </remarks>
			[ParameterTableRowAttribute("modelDispMask3:1", index: 133, minimum: 0, maximum: 1, step: 1, sortOrder: 87000, unknown2: 1)]
			[DisplayName("Model display mask 3")]
			[Description("Show me the model corresponding to the display mask .")]
			[DefaultValue(false)]
			public Boolean ModelDispMask3 {
				get { return GetBitProperty(11, 1, ModelDispMask3Property) != 0; }
				set { SetBitProperty(11, 1, value ? 1 : 0, ModelDispMask3Property); }
			}

			/// <summary>Model display mask 4</summary>
			/// <remarks>
			/// Japanese short name: "モデル表示マスク4", Google translated: "Model display mask 4".
			/// Japanese description: "表示マスクに対応するモデルを表示します。", Google translated: "Show me the model corresponding to the display mask .".
			/// </remarks>
			[ParameterTableRowAttribute("modelDispMask4:1", index: 134, minimum: 0, maximum: 1, step: 1, sortOrder: 88000, unknown2: 1)]
			[DisplayName("Model display mask 4")]
			[Description("Show me the model corresponding to the display mask .")]
			[DefaultValue(false)]
			public Boolean ModelDispMask4 {
				get { return GetBitProperty(12, 1, ModelDispMask4Property) != 0; }
				set { SetBitProperty(12, 1, value ? 1 : 0, ModelDispMask4Property); }
			}

			/// <summary>Model display mask 5</summary>
			/// <remarks>
			/// Japanese short name: "モデル表示マスク5", Google translated: "Model display mask 5".
			/// Japanese description: "表示マスクに対応するモデルを表示します。", Google translated: "Show me the model corresponding to the display mask .".
			/// </remarks>
			[ParameterTableRowAttribute("modelDispMask5:1", index: 135, minimum: 0, maximum: 1, step: 1, sortOrder: 89000, unknown2: 1)]
			[DisplayName("Model display mask 5")]
			[Description("Show me the model corresponding to the display mask .")]
			[DefaultValue(false)]
			public Boolean ModelDispMask5 {
				get { return GetBitProperty(13, 1, ModelDispMask5Property) != 0; }
				set { SetBitProperty(13, 1, value ? 1 : 0, ModelDispMask5Property); }
			}

			/// <summary>Model display mask 6</summary>
			/// <remarks>
			/// Japanese short name: "モデル表示マスク6", Google translated: "Model display mask 6".
			/// Japanese description: "表示マスクに対応するモデルを表示します。", Google translated: "Show me the model corresponding to the display mask .".
			/// </remarks>
			[ParameterTableRowAttribute("modelDispMask6:1", index: 136, minimum: 0, maximum: 1, step: 1, sortOrder: 90000, unknown2: 1)]
			[DisplayName("Model display mask 6")]
			[Description("Show me the model corresponding to the display mask .")]
			[DefaultValue(false)]
			public Boolean ModelDispMask6 {
				get { return GetBitProperty(14, 1, ModelDispMask6Property) != 0; }
				set { SetBitProperty(14, 1, value ? 1 : 0, ModelDispMask6Property); }
			}

			/// <summary>Model display mask 7</summary>
			/// <remarks>
			/// Japanese short name: "モデル表示マスク7", Google translated: "Model display mask 7".
			/// Japanese description: "表示マスクに対応するモデルを表示します。", Google translated: "Show me the model corresponding to the display mask .".
			/// </remarks>
			[ParameterTableRowAttribute("modelDispMask7:1", index: 137, minimum: 0, maximum: 1, step: 1, sortOrder: 91000, unknown2: 1)]
			[DisplayName("Model display mask 7")]
			[Description("Show me the model corresponding to the display mask .")]
			[DefaultValue(false)]
			public Boolean ModelDispMask7 {
				get { return GetBitProperty(15, 1, ModelDispMask7Property) != 0; }
				set { SetBitProperty(15, 1, value ? 1 : 0, ModelDispMask7Property); }
			}

			/// <summary>Model display mask 8</summary>
			/// <remarks>
			/// Japanese short name: "モデル表示マスク8", Google translated: "Model display mask 8".
			/// Japanese description: "表示マスクに対応するモデルを表示します。", Google translated: "Show me the model corresponding to the display mask .".
			/// </remarks>
			[ParameterTableRowAttribute("modelDispMask8:1", index: 138, minimum: 0, maximum: 1, step: 1, sortOrder: 114000, unknown2: 1)]
			[DisplayName("Model display mask 8")]
			[Description("Show me the model corresponding to the display mask .")]
			[DefaultValue(false)]
			public Boolean ModelDispMask8 {
				get { return GetBitProperty(16, 1, ModelDispMask8Property) != 0; }
				set { SetBitProperty(16, 1, value ? 1 : 0, ModelDispMask8Property); }
			}

			/// <summary>Model display mask 9</summary>
			/// <remarks>
			/// Japanese short name: "モデル表示マスク9", Google translated: "Model display mask 9".
			/// Japanese description: "表示マスクに対応するモデルを表示します。", Google translated: "Show me the model corresponding to the display mask .".
			/// </remarks>
			[ParameterTableRowAttribute("modelDispMask9:1", index: 139, minimum: 0, maximum: 1, step: 1, sortOrder: 115000, unknown2: 1)]
			[DisplayName("Model display mask 9")]
			[Description("Show me the model corresponding to the display mask .")]
			[DefaultValue(false)]
			public Boolean ModelDispMask9 {
				get { return GetBitProperty(17, 1, ModelDispMask9Property) != 0; }
				set { SetBitProperty(17, 1, value ? 1 : 0, ModelDispMask9Property); }
			}

			/// <summary>Model display mask 10</summary>
			/// <remarks>
			/// Japanese short name: "モデル表示マスク10", Google translated: "Model display mask 10".
			/// Japanese description: "表示マスクに対応するモデルを表示します。", Google translated: "Show me the model corresponding to the display mask .".
			/// </remarks>
			[ParameterTableRowAttribute("modelDispMask10:1", index: 140, minimum: 0, maximum: 1, step: 1, sortOrder: 116000, unknown2: 1)]
			[DisplayName("Model display mask 10")]
			[Description("Show me the model corresponding to the display mask .")]
			[DefaultValue(false)]
			public Boolean ModelDispMask10 {
				get { return GetBitProperty(18, 1, ModelDispMask10Property) != 0; }
				set { SetBitProperty(18, 1, value ? 1 : 0, ModelDispMask10Property); }
			}

			/// <summary>Model display mask 11</summary>
			/// <remarks>
			/// Japanese short name: "モデル表示マスク11", Google translated: "Model display mask 11".
			/// Japanese description: "表示マスクに対応するモデルを表示します。", Google translated: "Show me the model corresponding to the display mask .".
			/// </remarks>
			[ParameterTableRowAttribute("modelDispMask11:1", index: 141, minimum: 0, maximum: 1, step: 1, sortOrder: 117000, unknown2: 1)]
			[DisplayName("Model display mask 11")]
			[Description("Show me the model corresponding to the display mask .")]
			[DefaultValue(false)]
			public Boolean ModelDispMask11 {
				get { return GetBitProperty(19, 1, ModelDispMask11Property) != 0; }
				set { SetBitProperty(19, 1, value ? 1 : 0, ModelDispMask11Property); }
			}

			/// <summary>Model display mask 12</summary>
			/// <remarks>
			/// Japanese short name: "モデル表示マスク12", Google translated: "Model display mask 12".
			/// Japanese description: "表示マスクに対応するモデルを表示します。", Google translated: "Show me the model corresponding to the display mask .".
			/// </remarks>
			[ParameterTableRowAttribute("modelDispMask12:1", index: 142, minimum: 0, maximum: 1, step: 1, sortOrder: 118000, unknown2: 1)]
			[DisplayName("Model display mask 12")]
			[Description("Show me the model corresponding to the display mask .")]
			[DefaultValue(false)]
			public Boolean ModelDispMask12 {
				get { return GetBitProperty(20, 1, ModelDispMask12Property) != 0; }
				set { SetBitProperty(20, 1, value ? 1 : 0, ModelDispMask12Property); }
			}

			/// <summary>Model display mask 13</summary>
			/// <remarks>
			/// Japanese short name: "モデル表示マスク13", Google translated: "Model display mask 13".
			/// Japanese description: "表示マスクに対応するモデルを表示します。", Google translated: "Show me the model corresponding to the display mask .".
			/// </remarks>
			[ParameterTableRowAttribute("modelDispMask13:1", index: 143, minimum: 0, maximum: 1, step: 1, sortOrder: 119000, unknown2: 1)]
			[DisplayName("Model display mask 13")]
			[Description("Show me the model corresponding to the display mask .")]
			[DefaultValue(false)]
			public Boolean ModelDispMask13 {
				get { return GetBitProperty(21, 1, ModelDispMask13Property) != 0; }
				set { SetBitProperty(21, 1, value ? 1 : 0, ModelDispMask13Property); }
			}

			/// <summary>Model display mask 14</summary>
			/// <remarks>
			/// Japanese short name: "モデル表示マスク14", Google translated: "Model display mask 14".
			/// Japanese description: "表示マスクに対応するモデルを表示します。", Google translated: "Show me the model corresponding to the display mask .".
			/// </remarks>
			[ParameterTableRowAttribute("modelDispMask14:1", index: 144, minimum: 0, maximum: 1, step: 1, sortOrder: 120000, unknown2: 1)]
			[DisplayName("Model display mask 14")]
			[Description("Show me the model corresponding to the display mask .")]
			[DefaultValue(false)]
			public Boolean ModelDispMask14 {
				get { return GetBitProperty(22, 1, ModelDispMask14Property) != 0; }
				set { SetBitProperty(22, 1, value ? 1 : 0, ModelDispMask14Property); }
			}

			/// <summary>Model display mask 15</summary>
			/// <remarks>
			/// Japanese short name: "モデル表示マスク15", Google translated: "Model display mask 15".
			/// Japanese description: "表示マスクに対応するモデルを表示します。", Google translated: "Show me the model corresponding to the display mask .".
			/// </remarks>
			[ParameterTableRowAttribute("modelDispMask15:1", index: 145, minimum: 0, maximum: 1, step: 1, sortOrder: 121000, unknown2: 1)]
			[DisplayName("Model display mask 15")]
			[Description("Show me the model corresponding to the display mask .")]
			[DefaultValue(false)]
			public Boolean ModelDispMask15 {
				get { return GetBitProperty(23, 1, ModelDispMask15Property) != 0; }
				set { SetBitProperty(23, 1, value ? 1 : 0, ModelDispMask15Property); }
			}

			/// <summary>Whether to swing valid</summary>
			/// <remarks>
			/// Japanese short name: "首振り有効にするか", Google translated: "Whether to swing valid".
			/// Japanese description: "パラムウィーバで設定された首振りを有効にするか。", Google translated: "How to enable a swing that is set in Paramuu~iba .".
			/// </remarks>
			[ParameterTableRowAttribute("isEnableNeckTurn:1", index: 146, minimum: 0, maximum: 1, step: 1, sortOrder: 123000, unknown2: 1)]
			[DisplayName("Whether to swing valid")]
			[Description("How to enable a swing that is set in Paramuu~iba .")]
			[DefaultValue(false)]
			public Boolean IsEnableNeckTurn {
				get { return GetBitProperty(24, 1, IsEnableNeckTurnProperty) != 0; }
				set { SetBitProperty(24, 1, value ? 1 : 0, IsEnableNeckTurnProperty); }
			}

			/// <summary>Or Risupon ban</summary>
			/// <remarks>
			/// Japanese short name: "リスポン禁止か", Google translated: "Or Risupon ban".
			/// Japanese description: "リスポンを禁止するか", Google translated: "And prohibit or Risupon".
			/// </remarks>
			[ParameterTableRowAttribute("disableRespawn:1", index: 147, minimum: 0, maximum: 1, step: 1, sortOrder: 124000, unknown2: 1)]
			[DisplayName("Or Risupon ban")]
			[Description("And prohibit or Risupon")]
			[DefaultValue(false)]
			public Boolean DisableRespawn {
				get { return GetBitProperty(25, 1, DisableRespawnProperty) != 0; }
				set { SetBitProperty(25, 1, value ? 1 : 0, DisableRespawnProperty); }
			}

			/// <summary>Wait for the moving animation</summary>
			/// <remarks>
			/// Japanese short name: "移動アニメを待つか", Google translated: "Wait for the moving animation".
			/// Japanese description: "移動アニメをアニメが終わるまで再生するか。（カゲロウ龍の様に。）", Google translated: "How to play animation until the end of the movement animation . ( . Ephemera like a dragon )".
			/// </remarks>
			[ParameterTableRowAttribute("isMoveAnimWait:1", index: 148, minimum: 0, maximum: 1, step: 1, sortOrder: 7500, unknown2: 1)]
			[DisplayName("Wait for the moving animation")]
			[Description("How to play animation until the end of the movement animation . ( . Ephemera like a dragon )")]
			[DefaultValue(false)]
			public Boolean IsMoveAnimWait {
				get { return GetBitProperty(26, 1, IsMoveAnimWaitProperty) != 0; }
				set { SetBitProperty(26, 1, value ? 1 : 0, IsMoveAnimWaitProperty); }
			}

			/// <summary>Or crowd for processing relief</summary>
			/// <remarks>
			/// Japanese short name: "群集用処理軽減するか", Google translated: "Or crowd for processing relief".
			/// Japanese description: "群集時の処理負荷軽減を行なうか。赤子用（できればファランクスも）", Google translated: "You can do the processing load reduction of the crowd at the time . (Still, Phalanx if possible) for baby".
			/// </remarks>
			[ParameterTableRowAttribute("isCrowd:1", index: 149, minimum: 0, maximum: 1, step: 1, sortOrder: 96000, unknown2: 1)]
			[DisplayName("Or crowd for processing relief")]
			[Description("You can do the processing load reduction of the crowd at the time . (Still, Phalanx if possible) for baby")]
			[DefaultValue(false)]
			public Boolean IsCrowd {
				get { return GetBitProperty(27, 1, IsCrowdProperty) != 0; }
				set { SetBitProperty(27, 1, value ? 1 : 0, IsCrowdProperty); }
			}

			/// <summary>Or sacred weakness</summary>
			/// <remarks>
			/// Japanese short name: "神聖弱点か", Google translated: "Or sacred weakness".
			/// Japanese description: "神聖弱点か？神聖ダメージ倍率が計算に含まれるようになります", Google translated: "Sacred or weakness ? Holy damage magnification will be included in the calculation".
			/// </remarks>
			[ParameterTableRowAttribute("isWeakSaint:1", index: 150, minimum: 0, maximum: 1, step: 1, sortOrder: 11001, unknown2: 1)]
			[DisplayName("Or sacred weakness")]
			[Description("Sacred or weakness ? Holy damage magnification will be included in the calculation")]
			[DefaultValue(false)]
			public Boolean IsWeakSaint {
				get { return GetBitProperty(28, 1, IsWeakSaintProperty) != 0; }
				set { SetBitProperty(28, 1, value ? 1 : 0, IsWeakSaintProperty); }
			}

			/// <summary>A weakness or</summary>
			/// <remarks>
			/// Japanese short name: "弱点Aか", Google translated: "A weakness or".
			/// Japanese description: "弱点Aか？弱点Aダメージ倍率が計算に含まれるようになります", Google translated: "Weakness or A? A weakness damage magnification will be included in the calculation".
			/// </remarks>
			[ParameterTableRowAttribute("isWeakA:1", index: 151, minimum: 0, maximum: 1, step: 1, sortOrder: 11002, unknown2: 1)]
			[DisplayName("A weakness or")]
			[Description("Weakness or A? A weakness damage magnification will be included in the calculation")]
			[DefaultValue(false)]
			public Boolean IsWeakA {
				get { return GetBitProperty(29, 1, IsWeakAProperty) != 0; }
				set { SetBitProperty(29, 1, value ? 1 : 0, IsWeakAProperty); }
			}

			/// <summary>Or weakness B</summary>
			/// <remarks>
			/// Japanese short name: "弱点Bか", Google translated: "Or weakness B".
			/// Japanese description: "弱点Bか？弱点Bダメージ倍率が計算に含まれるようになります", Google translated: "Weakness or B? Weakness B damage magnification will be included in the calculation".
			/// </remarks>
			[ParameterTableRowAttribute("isWeakB:1", index: 152, minimum: 0, maximum: 1, step: 1, sortOrder: 11003, unknown2: 1)]
			[DisplayName("Or weakness B")]
			[Description("Weakness or B? Weakness B damage magnification will be included in the calculation")]
			[DefaultValue(false)]
			public Boolean IsWeakB {
				get { return GetBitProperty(30, 1, IsWeakBProperty) != 0; }
				set { SetBitProperty(30, 1, value ? 1 : 0, IsWeakBProperty); }
			}

			/// <summary>pad</summary>
			/// <remarks>
			/// Japanese short name: "pad", Google translated: "pad".
			/// Japanese description: "pad", Google translated: "pad".
			/// </remarks>
			[ParameterTableRowAttribute("pad1:1", index: 153, minimum: 0, maximum: 0, step: 0, sortOrder: 200001, unknown2: 0)]
			[DisplayName("pad")]
			[Description("pad")]
			[DefaultValue(false)]
			[Browsable(false)]
			public Boolean Pad1 {
				get { return GetBitProperty(31, 1, Pad1Property) != 0; }
				set { SetBitProperty(31, 1, value ? 1 : 0, Pad1Property); }
			}

			/// <summary>Pledge</summary>
			/// <remarks>
			/// Japanese short name: "誓約", Google translated: "Pledge".
			/// Japanese description: "誓約タイプ(なし：０)", Google translated: "Pledge type (no : 0)".
			/// </remarks>
			[ParameterTableRowAttribute("vowType:3", index: 154, minimum: 0, maximum: 7, step: 1, sortOrder: 125000, unknown2: 1)]
			[DisplayName("Pledge")]
			[Description("Pledge type (no : 0)")]
			[DefaultValue((Byte)0)]
			public Byte VowType {
				get { return (Byte)GetBitProperty(32, 3, VowTypeProperty); }
				set {
					if ((double)value < 0 || (double)value > 7)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 7 for " + VowTypeProperty.Name + ".");
					SetBitProperty(32, 3, (int)value, VowTypeProperty);
				}
			}

			/// <summary>Not initial death</summary>
			/// <remarks>
			/// Japanese short name: "初期死亡しない", Google translated: "Not initial death".
			/// Japanese description: "初期死亡をしない場合にTRUE、殺してセーブしても死体再現されません。", Google translated: "Will not be reproduced corpse when you save TRUE, the kill if you do not want the initial death .".
			/// </remarks>
			[ParameterTableRowAttribute("disableInitializeDead:1", index: 155, minimum: 0, maximum: 1, step: 1, sortOrder: 124001, unknown2: 1)]
			[DisplayName("Not initial death")]
			[Description("Will not be reproduced corpse when you save TRUE, the kill if you do not want the initial death .")]
			[DefaultValue(false)]
			public Boolean DisableInitializeDead {
				get { return GetBitProperty(35, 1, DisableInitializeDeadProperty) != 0; }
				set { SetBitProperty(35, 1, value ? 1 : 0, DisableInitializeDeadProperty); }
			}

			/// <summary>pad</summary>
			/// <remarks>
			/// Japanese short name: "pad", Google translated: "pad".
			/// Japanese description: "pad", Google translated: "pad".
			/// </remarks>
			[ParameterTableRowAttribute("pad3:4", index: 156, minimum: 0, maximum: 0, step: 0, sortOrder: 200002, unknown2: 0)]
			[DisplayName("pad")]
			[Description("pad")]
			[DefaultValue((Byte)0)]
			[Browsable(false)]
			public Byte Pad3 {
				get { return (Byte)GetBitProperty(36, 4, Pad3Property); }
				set {
					if ((double)value < 0 || (double)value > 0)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 0 for " + Pad3Property.Name + ".");
					SetBitProperty(36, 4, (int)value, Pad3Property);
				}
			}

			/// <summary>pad</summary>
			/// <remarks>
			/// Japanese short name: "pad", Google translated: "pad".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("pad2[6]", index: 157, minimum: 0, maximum: 0, step: 0, sortOrder: 200003, unknown2: 0)]
			[DisplayName("pad")]
			[Description("")]
			[Browsable(false)]
			public Byte[] Pad2 {
				get { return pad2; }
				set { SetProperty(ref pad2, ref value, Pad2Property); }
			}

			/// <summary>Get the localized English name of the <see cref="Npc"/>.</summary>
			public string EnglishName { get { return GetLocalisedName(Language.English); } }

			internal Npc(ParameterTable table, int index, AssetLoader loader, int next)
				: base(table, index, loader) {
				BinaryReader reader = loader.Reader;

				BehaviorVariationId = reader.ReadInt32();
				AiThinkId = reader.ReadInt32();
				NameId = reader.ReadInt32();
				TurnVellocity = reader.ReadSingle();
				HitHeight = reader.ReadSingle();
				HitRadius = reader.ReadSingle();
				Weight = reader.ReadUInt32();
				HitYOffset = reader.ReadSingle();
				Hp = reader.ReadUInt32();
				Mp = reader.ReadUInt32();
				GetSoul = reader.ReadUInt32();
				ItemLotId_1 = reader.ReadInt32();
				ItemLotId_2 = reader.ReadInt32();
				ItemLotId_3 = reader.ReadInt32();
				ItemLotId_4 = reader.ReadInt32();
				ItemLotId_5 = reader.ReadInt32();
				ItemLotId_6 = reader.ReadInt32();
				HumanityLotId = reader.ReadInt32();
				SpEffectID0 = reader.ReadInt32();
				SpEffectID1 = reader.ReadInt32();
				SpEffectID2 = reader.ReadInt32();
				SpEffectID3 = reader.ReadInt32();
				SpEffectID4 = reader.ReadInt32();
				SpEffectID5 = reader.ReadInt32();
				SpEffectID6 = reader.ReadInt32();
				SpEffectID7 = reader.ReadInt32();
				GameClearSpEffectID = reader.ReadInt32();
				PhysGuardCutRate = reader.ReadSingle();
				MagGuardCutRate = reader.ReadSingle();
				FireGuardCutRate = reader.ReadSingle();
				ThunGuardCutRate = reader.ReadSingle();
				AnimIdOffset = reader.ReadInt32();
				MoveAnimId = reader.ReadInt32();
				SpMoveAnimId1 = reader.ReadInt32();
				SpMoveAnimId2 = reader.ReadInt32();
				NetworkWarpDist = reader.ReadSingle();
				DbgBehaviorR1 = reader.ReadInt32();
				DbgBehaviorL1 = reader.ReadInt32();
				DbgBehaviorR2 = reader.ReadInt32();
				DbgBehaviorL2 = reader.ReadInt32();
				DbgBehaviorRL = reader.ReadInt32();
				DbgBehaviorRR = reader.ReadInt32();
				DbgBehaviorRD = reader.ReadInt32();
				DbgBehaviorRU = reader.ReadInt32();
				DbgBehaviorLL = reader.ReadInt32();
				DbgBehaviorLR = reader.ReadInt32();
				DbgBehaviorLD = reader.ReadInt32();
				DbgBehaviorLU = reader.ReadInt32();
				AnimIdOffset2 = reader.ReadInt32();
				PartsDamageRate1 = reader.ReadSingle();
				PartsDamageRate2 = reader.ReadSingle();
				PartsDamageRate3 = reader.ReadSingle();
				PartsDamageRate4 = reader.ReadSingle();
				PartsDamageRate5 = reader.ReadSingle();
				PartsDamageRate6 = reader.ReadSingle();
				PartsDamageRate7 = reader.ReadSingle();
				PartsDamageRate8 = reader.ReadSingle();
				WeakPartsDamageRate = reader.ReadSingle();
				SuperArmorRecoverCorrection = reader.ReadSingle();
				SuperArmorBrakeKnockbackDist = reader.ReadSingle();
				Stamina = reader.ReadUInt16();
				StaminaRecoverBaseVel = reader.ReadUInt16();
				Def_phys = reader.ReadUInt16();
				Def_slash = reader.ReadInt16();
				Def_blow = reader.ReadInt16();
				Def_thrust = reader.ReadInt16();
				Def_mag = reader.ReadUInt16();
				Def_fire = reader.ReadUInt16();
				Def_thunder = reader.ReadUInt16();
				DefFlickPower = reader.ReadUInt16();
				ResistPoison = reader.ReadUInt16();
				ResistDisease = reader.ReadUInt16();
				ResistBleed = reader.ReadUInt16();
				ResistCurse = reader.ReadUInt16();
				GhostModelId = reader.ReadInt16();
				NormalChangeResouceId = reader.ReadInt16();
				GuardAngle = reader.ReadInt16();
				SlashGuardCutRate = reader.ReadInt16();
				BlowGuardCutRate = reader.ReadInt16();
				ThrustGuardCutRate = reader.ReadInt16();
				SuperArmorDurability = reader.ReadInt16();
				NormalChangeTexChrId = reader.ReadInt16();
				DropType = (NpcItemDropType)reader.ReadUInt16();
				KnockbackRate = reader.ReadByte();
				KnockbackParamId = reader.ReadByte();
				FallDamageDump = reader.ReadByte();
				StaminaGuardDef = reader.ReadByte();
				PcAttrB = reader.ReadByte();
				PcAttrW = reader.ReadByte();
				PcAttrL = reader.ReadByte();
				PcAttrR = reader.ReadByte();
				AreaAttrB = reader.ReadByte();
				AreaAttrW = reader.ReadByte();
				AreaAttrL = reader.ReadByte();
				AreaAttrR = reader.ReadByte();
				MpRecoverBaseVel = reader.ReadByte();
				FlickDamageCutRate = reader.ReadByte();
				DefaultLodParamId = reader.ReadSByte();
				DrawType = (NpcDrawType)reader.ReadByte();
				NpcType = (NpcType)reader.ReadByte();
				TeamType = (NpcTemaType)reader.ReadByte();
				MoveType = (NpcMoveType)reader.ReadByte();
				LockDist = reader.ReadByte();
				Material = (WeaponMaterialDefend)reader.ReadByte();
				MaterialSfx = (WeaponMaterialDefendSound)reader.ReadByte();
				Material_Weak = (WeaponMaterialDefend)reader.ReadByte();
				MaterialSfx_Weak = (WeaponMaterialDefendSound)reader.ReadByte();
				PartsDamageType = (AttackParameterPartDamageType)reader.ReadByte();
				MaxUndurationAng = reader.ReadByte();
				GuardLevel = reader.ReadSByte();
				BurnSfxType = (NpcBurnType)reader.ReadByte();
				PoisonGuardResist = reader.ReadSByte();
				DiseaseGuardResist = reader.ReadSByte();
				BloodGuardResist = reader.ReadSByte();
				CurseGuardResist = reader.ReadSByte();
				ParryAttack = reader.ReadByte();
				ParryDefence = reader.ReadByte();
				SfxSize = (NpcSfxSize)reader.ReadByte();
				PushOutCamRegionRadius = reader.ReadByte();
				HitStopType = (NpcHitStopType)reader.ReadByte();
				LadderEndChkOffsetTop = reader.ReadByte();
				LadderEndChkOffsetLow = reader.ReadByte();
				BitFields = reader.ReadBytes(5);
				Pad2 = reader.ReadBytes(6);
			}

			internal Npc(ParameterTable table, int index)
				: base(table, index) {
				BitFields = new byte[5];
				BehaviorVariationId = (Int32)0;
				AiThinkId = (Int32)0;
				NameId = (Int32)(-1);
				TurnVellocity = (Single)0;
				HitHeight = (Single)0;
				HitRadius = (Single)0;
				Weight = (UInt32)0;
				HitYOffset = (Single)0;
				Hp = (UInt32)0;
				Mp = (UInt32)0;
				GetSoul = (UInt32)0;
				ItemLotId_1 = (Int32)(-1);
				ItemLotId_2 = (Int32)(-1);
				ItemLotId_3 = (Int32)(-1);
				ItemLotId_4 = (Int32)(-1);
				ItemLotId_5 = (Int32)(-1);
				ItemLotId_6 = (Int32)(-1);
				HumanityLotId = (Int32)(-1);
				SpEffectID0 = (Int32)(-1);
				SpEffectID1 = (Int32)(-1);
				SpEffectID2 = (Int32)(-1);
				SpEffectID3 = (Int32)(-1);
				SpEffectID4 = (Int32)(-1);
				SpEffectID5 = (Int32)(-1);
				SpEffectID6 = (Int32)(-1);
				SpEffectID7 = (Int32)(-1);
				GameClearSpEffectID = (Int32)(-1);
				PhysGuardCutRate = (Single)0;
				MagGuardCutRate = (Single)0;
				FireGuardCutRate = (Single)0;
				ThunGuardCutRate = (Single)0;
				AnimIdOffset = (Int32)0;
				MoveAnimId = (Int32)0;
				SpMoveAnimId1 = (Int32)0;
				SpMoveAnimId2 = (Int32)0;
				NetworkWarpDist = (Single)0;
				DbgBehaviorR1 = (Int32)(-1);
				DbgBehaviorL1 = (Int32)(-1);
				DbgBehaviorR2 = (Int32)(-1);
				DbgBehaviorL2 = (Int32)(-1);
				DbgBehaviorRL = (Int32)(-1);
				DbgBehaviorRR = (Int32)(-1);
				DbgBehaviorRD = (Int32)(-1);
				DbgBehaviorRU = (Int32)(-1);
				DbgBehaviorLL = (Int32)(-1);
				DbgBehaviorLR = (Int32)(-1);
				DbgBehaviorLD = (Int32)(-1);
				DbgBehaviorLU = (Int32)(-1);
				AnimIdOffset2 = (Int32)0;
				PartsDamageRate1 = (Single)1;
				PartsDamageRate2 = (Single)1;
				PartsDamageRate3 = (Single)1;
				PartsDamageRate4 = (Single)1;
				PartsDamageRate5 = (Single)1;
				PartsDamageRate6 = (Single)1;
				PartsDamageRate7 = (Single)1;
				PartsDamageRate8 = (Single)1;
				WeakPartsDamageRate = (Single)1;
				SuperArmorRecoverCorrection = (Single)0;
				SuperArmorBrakeKnockbackDist = (Single)0;
				Stamina = (UInt16)0;
				StaminaRecoverBaseVel = (UInt16)0;
				Def_phys = (UInt16)0;
				Def_slash = (Int16)0;
				Def_blow = (Int16)0;
				Def_thrust = (Int16)0;
				Def_mag = (UInt16)0;
				Def_fire = (UInt16)0;
				Def_thunder = (UInt16)0;
				DefFlickPower = (UInt16)0;
				ResistPoison = (UInt16)0;
				ResistDisease = (UInt16)0;
				ResistBleed = (UInt16)0;
				ResistCurse = (UInt16)0;
				GhostModelId = (Int16)(-1);
				NormalChangeResouceId = (Int16)(-1);
				GuardAngle = (Int16)0;
				SlashGuardCutRate = (Int16)0;
				BlowGuardCutRate = (Int16)0;
				ThrustGuardCutRate = (Int16)0;
				SuperArmorDurability = (Int16)0;
				NormalChangeTexChrId = (Int16)(-1);
				DropType = (NpcItemDropType)0;
				KnockbackRate = (Byte)0;
				KnockbackParamId = (Byte)0;
				FallDamageDump = (Byte)0;
				StaminaGuardDef = (Byte)0;
				PcAttrB = (Byte)0;
				PcAttrW = (Byte)0;
				PcAttrL = (Byte)0;
				PcAttrR = (Byte)0;
				AreaAttrB = (Byte)0;
				AreaAttrW = (Byte)0;
				AreaAttrL = (Byte)0;
				AreaAttrR = (Byte)0;
				MpRecoverBaseVel = (Byte)0;
				FlickDamageCutRate = (Byte)0;
				DefaultLodParamId = (SByte)(-1);
				DrawType = (NpcDrawType)0;
				NpcType = (NpcType)0;
				TeamType = (NpcTemaType)0;
				MoveType = (NpcMoveType)0;
				LockDist = (Byte)0;
				Material = (WeaponMaterialDefend)0;
				MaterialSfx = (WeaponMaterialDefendSound)0;
				Material_Weak = (WeaponMaterialDefend)0;
				MaterialSfx_Weak = (WeaponMaterialDefendSound)0;
				PartsDamageType = (AttackParameterPartDamageType)0;
				MaxUndurationAng = (Byte)0;
				GuardLevel = (SByte)0;
				BurnSfxType = (NpcBurnType)0;
				PoisonGuardResist = (SByte)0;
				DiseaseGuardResist = (SByte)0;
				BloodGuardResist = (SByte)0;
				CurseGuardResist = (SByte)0;
				ParryAttack = (Byte)0;
				ParryDefence = (Byte)0;
				SfxSize = (NpcSfxSize)0;
				PushOutCamRegionRadius = (Byte)12;
				HitStopType = (NpcHitStopType)0;
				LadderEndChkOffsetTop = (Byte)15;
				LadderEndChkOffsetLow = (Byte)8;
				UseRagdollCamHit = false;
				DisableClothRigidHit = false;
				UseRagdoll = false;
				IsDemon = false;
				IsGhost = false;
				IsNoDamageMotion = false;
				IsUnduration = false;
				IsChangeWanderGhost = false;
				ModelDispMask0 = false;
				ModelDispMask1 = false;
				ModelDispMask2 = false;
				ModelDispMask3 = false;
				ModelDispMask4 = false;
				ModelDispMask5 = false;
				ModelDispMask6 = false;
				ModelDispMask7 = false;
				ModelDispMask8 = false;
				ModelDispMask9 = false;
				ModelDispMask10 = false;
				ModelDispMask11 = false;
				ModelDispMask12 = false;
				ModelDispMask13 = false;
				ModelDispMask14 = false;
				ModelDispMask15 = false;
				IsEnableNeckTurn = false;
				DisableRespawn = false;
				IsMoveAnimWait = false;
				IsCrowd = false;
				IsWeakSaint = false;
				IsWeakA = false;
				IsWeakB = false;
				Pad1 = false;
				VowType = (Byte)0;
				DisableInitializeDead = false;
				Pad3 = (Byte)0;
				Pad2 = new Byte[6];
			}

			/// <summary>Get the localised name of the <see cref="Npc"/>.</summary>
			/// <param name="language"></param>
			/// <returns></returns>
			public string GetLocalisedName(Language language) { return Parent.GetLocalisedString(Engine.ItemArchiveId.NpcNames, NameId); }

			/// <summary>Write the <see cref="Npc"/> data.</summary>
			/// <param name="writer"></param>
			public override void Write(BinaryWriter writer) {
				writer.Write(BehaviorVariationId);
				writer.Write(AiThinkId);
				writer.Write(NameId);
				writer.Write(TurnVellocity);
				writer.Write(HitHeight);
				writer.Write(HitRadius);
				writer.Write(Weight);
				writer.Write(HitYOffset);
				writer.Write(Hp);
				writer.Write(Mp);
				writer.Write(GetSoul);
				writer.Write(ItemLotId_1);
				writer.Write(ItemLotId_2);
				writer.Write(ItemLotId_3);
				writer.Write(ItemLotId_4);
				writer.Write(ItemLotId_5);
				writer.Write(ItemLotId_6);
				writer.Write(HumanityLotId);
				writer.Write(SpEffectID0);
				writer.Write(SpEffectID1);
				writer.Write(SpEffectID2);
				writer.Write(SpEffectID3);
				writer.Write(SpEffectID4);
				writer.Write(SpEffectID5);
				writer.Write(SpEffectID6);
				writer.Write(SpEffectID7);
				writer.Write(GameClearSpEffectID);
				writer.Write(PhysGuardCutRate);
				writer.Write(MagGuardCutRate);
				writer.Write(FireGuardCutRate);
				writer.Write(ThunGuardCutRate);
				writer.Write(AnimIdOffset);
				writer.Write(MoveAnimId);
				writer.Write(SpMoveAnimId1);
				writer.Write(SpMoveAnimId2);
				writer.Write(NetworkWarpDist);
				writer.Write(DbgBehaviorR1);
				writer.Write(DbgBehaviorL1);
				writer.Write(DbgBehaviorR2);
				writer.Write(DbgBehaviorL2);
				writer.Write(DbgBehaviorRL);
				writer.Write(DbgBehaviorRR);
				writer.Write(DbgBehaviorRD);
				writer.Write(DbgBehaviorRU);
				writer.Write(DbgBehaviorLL);
				writer.Write(DbgBehaviorLR);
				writer.Write(DbgBehaviorLD);
				writer.Write(DbgBehaviorLU);
				writer.Write(AnimIdOffset2);
				writer.Write(PartsDamageRate1);
				writer.Write(PartsDamageRate2);
				writer.Write(PartsDamageRate3);
				writer.Write(PartsDamageRate4);
				writer.Write(PartsDamageRate5);
				writer.Write(PartsDamageRate6);
				writer.Write(PartsDamageRate7);
				writer.Write(PartsDamageRate8);
				writer.Write(WeakPartsDamageRate);
				writer.Write(SuperArmorRecoverCorrection);
				writer.Write(SuperArmorBrakeKnockbackDist);
				writer.Write(Stamina);
				writer.Write(StaminaRecoverBaseVel);
				writer.Write(Def_phys);
				writer.Write(Def_slash);
				writer.Write(Def_blow);
				writer.Write(Def_thrust);
				writer.Write(Def_mag);
				writer.Write(Def_fire);
				writer.Write(Def_thunder);
				writer.Write(DefFlickPower);
				writer.Write(ResistPoison);
				writer.Write(ResistDisease);
				writer.Write(ResistBleed);
				writer.Write(ResistCurse);
				writer.Write(GhostModelId);
				writer.Write(NormalChangeResouceId);
				writer.Write(GuardAngle);
				writer.Write(SlashGuardCutRate);
				writer.Write(BlowGuardCutRate);
				writer.Write(ThrustGuardCutRate);
				writer.Write(SuperArmorDurability);
				writer.Write(NormalChangeTexChrId);
				writer.Write((UInt16)DropType);
				writer.Write(KnockbackRate);
				writer.Write(KnockbackParamId);
				writer.Write(FallDamageDump);
				writer.Write(StaminaGuardDef);
				writer.Write(PcAttrB);
				writer.Write(PcAttrW);
				writer.Write(PcAttrL);
				writer.Write(PcAttrR);
				writer.Write(AreaAttrB);
				writer.Write(AreaAttrW);
				writer.Write(AreaAttrL);
				writer.Write(AreaAttrR);
				writer.Write(MpRecoverBaseVel);
				writer.Write(FlickDamageCutRate);
				writer.Write(DefaultLodParamId);
				writer.Write((Byte)DrawType);
				writer.Write((Byte)NpcType);
				writer.Write((Byte)TeamType);
				writer.Write((Byte)MoveType);
				writer.Write(LockDist);
				writer.Write((Byte)Material);
				writer.Write((Byte)MaterialSfx);
				writer.Write((Byte)Material_Weak);
				writer.Write((Byte)MaterialSfx_Weak);
				writer.Write((Byte)PartsDamageType);
				writer.Write(MaxUndurationAng);
				writer.Write(GuardLevel);
				writer.Write((Byte)BurnSfxType);
				writer.Write(PoisonGuardResist);
				writer.Write(DiseaseGuardResist);
				writer.Write(BloodGuardResist);
				writer.Write(CurseGuardResist);
				writer.Write(ParryAttack);
				writer.Write(ParryDefence);
				writer.Write((Byte)SfxSize);
				writer.Write(PushOutCamRegionRadius);
				writer.Write((Byte)HitStopType);
				writer.Write(LadderEndChkOffsetTop);
				writer.Write(LadderEndChkOffsetLow);
				writer.Write(BitFields);
				writer.Write(Pad2);
			}
		}
	}
}
