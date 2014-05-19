using Glare.Assets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Alexandria.Engines.DarkSouls {
	/* Item
	 *		Int32 refId, sfxVariationId, behaviorId, replaceItemId, qwcId, yesNoDialogMessageId, magicId;
			UInt16 iconId, modelId;
			Int16 shopLv, compTrophySedId, trophySeqId, maxNum;
			Byte consumeHeroPoint, overDexterity;
			ItemType goodsType;
			BehaviorRefType refCategory;
			BehaviorCategory spEffectCategory;
			ItemCategory goodsCategory;
			ItemUseAnimation goodsUseAnim;
			ItemUseMenu opmeMenuType;
			SpecialEffectUseLimitCategory useLimitCategory;
			ReplacementCategory replaceCategory;
	 * 
	 * Ring
	 *		Int32 refId, sfxVariationId, behaviorId, qwcId;
			UInt16 iconId;
			Int16 shopLv, trophySGradeId, trophySeqId;
			AccessoryCategory accessoryCategory;
			BehaviorRefType refCategory;
			BehaviorCategory spEffectCategory;
	 * 
	 * Armor
	 *		Int32 residentSpEffectId, residentSpEffectId2, residentSpEffectId3, materialSetId, originEquipPro, originEquipPro1, originEquipPro2, originEquipPro3, originEquipPro4, originEquipPro5, originEquipPro6, originEquipPro7, originEquipPro8, originEquipPro9, originEquipPro10, originEquipPro11, originEquipPro12, originEquipPro13, originEquipPro14, originEquipPro15, qwcId;
			Single partsDamageRate, corectSARecover, faceScaleM_ScaleX, faceScaleM_ScaleZ, faceScaleM_MaxX, faceScaleM_MaxZ, faceScaleF_ScaleX, faceScaleF_ScaleZ, faceScaleF_MaxX, faceScaleF_MaxZ;
			UInt16 iconIdM, iconIdF, knockBack, knockbackBounceRate, defFlickPower, defensePhysics, defenseMagic, defenseFire, defenseThunder, resistPoison, resistDisease, resistBlood, resistCurse;
			Int16 saDurability, defenseSlash, defenseBlow, defenseThrust, reinforceTypeId, trophySGradeId, shopLv;
			Byte knockbackParamId, flickDamageCutRate;
			ArmorCategory protectorCategory;
			WeaponMaterialDefend defenseMaterial, defenseMaterial_Weak;
			WeaponMaterialDefendSound defenseMaterialSfx, defenseMaterialSfx_Weak;
			AttackParameterPartDamageType partsDmgType;
	 * 
	 * Weapon
	 *		Int32 behaviorVariationId, spEffectBehaviorId0, spEffectBehaviorId1, spEffectBehaviorId2, residentSpEffectId, residentSpEffectId1, residentSpEffectId2, materialSetId, originEquipWep, originEquipWep1, originEquipWep2, originEquipWep3, originEquipWep4, originEquipWep5, originEquipWep6, originEquipWep7, originEquipWep8, originEquipWep9, originEquipWep10, originEquipWep11, originEquipWep12, originEquipWep13, originEquipWep14, originEquipWep15;
			Single weaponWeightRate, correctStrength, correctAgility, correctMagic, correctFaith, physGuardCutRate, magGuardCutRate, fireGuardCutRate, thunGuardCutRate, antiDemonDamageRate, antSaintDamageRate, antWeakA_DamageRate, antWeakB_DamageRate;
			UInt16 iconId, attackThrowEscape, attackBasePhysics, attackBaseMagic, attackBaseFire, attackBaseThunder, attackBaseStamina, saWeaponDamage;
			Int16 parryDamageLife, saDurability, guardAngle, staminaGuardDef, reinforceTypeId, trophySGradeId, trophySeqId, throwAtkRate, bowDistRate;
			WeaponCategory weaponCategory;
			WeaponMotionCategory wepmotionCategory;
			GuardMotionCategory guardmotionCategory;
			WeaponMaterialAttack atkMaterial;
			WeaponMaterialDefend defMaterial;
			WeaponMaterialDefendSound defSfxMaterial;
			WeaponCorrectType correctType;
			AttackParameterSpecialAttributes spAttribute;
			Byte spAtkcategory, wepmotionOneHandId, wepmotionBothHandId, properStrength, properAgility, properMagic, properFaith, overStrength, attackBaseParry, defenseBaseParry, guardBaseRepel, attackBaseRepel;
			SByte guardCutCancelRate, guardLevel, slashGuardCutRate, blowGuardCutRate, thrustGuardCutRate, poisonGuardResist, diseaseGuardResist, bloodGuardResist, curseGuardResist;
			DurabilityDivergenceCategory isDurabilityDivergence;
	 */

	partial class TableRows {
		/// <summary>
		/// 
		/// </summary>
		/// <remarks>
		/// From "EquipParamWeapon.paramdef" (id 0Ch).
		/// </remarks>
		[ParameterTableRowOrder("sortId", 1, 2400)]
		[ParameterTableRowOrder("weight", 3, 3000)]
		[ParameterTableRowOrder("basicPrice", 6, 3500)]
		[ParameterTableRowOrder("sellValue", 7, 3510)]
		[ParameterTableRowOrder("isDeposit", 124, 1960)]
		[ParameterTableRowOrder("fixPrice", 5, 3400)]
		[ParameterTableRowOrder("wanderingEquipId", 2, 2500)]
		[ParameterTableRowOrder("vagrantItemLotId", 43, 2510)]
		[ParameterTableRowOrder("vagrantBonusEneDropItemLotId", 44, 2511)]
		[ParameterTableRowOrder("vagrantItemEneDropItemLotId", 43, 2512)]
		[ParameterTableRowOrder("equipModelCategory", 66, 100)]
		[ParameterTableRowOrder("equipModelGender", 67, 200)]
		[ParameterTableRowOrder("equipModelId", 46, 300)]
		[ParameterTableRowOrder("oldSortId", 127, 2400)]
		[ParameterTableRowOrder("durability", 48, 3200)]
		[ParameterTableRowOrder("durabilityMax", 49, 3300)]
		public class Weapon : ClothingTableRow {
			public const string TableName = "EQUIP_PARAM_WEAPON_ST";

			Int32 behaviorVariationId, spEffectBehaviorId0, spEffectBehaviorId1, spEffectBehaviorId2, residentSpEffectId, residentSpEffectId1, residentSpEffectId2, materialSetId, originEquipWep, originEquipWep1, originEquipWep2, originEquipWep3, originEquipWep4, originEquipWep5, originEquipWep6, originEquipWep7, originEquipWep8, originEquipWep9, originEquipWep10, originEquipWep11, originEquipWep12, originEquipWep13, originEquipWep14, originEquipWep15;
			Single weaponWeightRate, correctStrength, correctAgility, correctMagic, correctFaith, physGuardCutRate, magGuardCutRate, fireGuardCutRate, thunGuardCutRate, antiDemonDamageRate, antSaintDamageRate, antWeakA_DamageRate, antWeakB_DamageRate;
			UInt16 iconId, attackThrowEscape, attackBasePhysics, attackBaseMagic, attackBaseFire, attackBaseThunder, attackBaseStamina, saWeaponDamage;
			Int16 parryDamageLife, saDurability, guardAngle, staminaGuardDef, reinforceTypeId, trophySGradeId, trophySeqId, throwAtkRate, bowDistRate;
			WeaponCategory weaponCategory;
			WeaponMotionCategory motionCategory;
			GuardMotionCategory guardmotionCategory;
			WeaponMaterialAttack atkMaterial;
			WeaponMaterialDefend defMaterial;
			WeaponMaterialDefendSound defSfxMaterial;
			WeaponCorrectType correctType;
			AttackParameterSpecialAttributes spAttribute;
			Byte spAtkcategory, wepmotionOneHandId, wepmotionBothHandId, properStrength, properAgility, properMagic, properFaith, overStrength, attackBaseParry, defenseBaseParry, guardBaseRepel, attackBaseRepel;
			SByte guardCutCancelRate, guardLevel, slashGuardCutRate, blowGuardCutRate, thrustGuardCutRate, poisonGuardResist, diseaseGuardResist, bloodGuardResist, curseGuardResist;
			DurabilityDivergenceCategory isDurabilityDivergence;
			Byte[] pad_0, pad_1;

			public static readonly PropertyInfo
				BehaviorVariationIdProperty = GetProperty<Weapon>("BehaviorVariationId"),
				WeaponWeightRateProperty = GetProperty<Weapon>("WeaponWeightRate"),
				CorrectStrengthProperty = GetProperty<Weapon>("CorrectStrength"),
				CorrectAgilityProperty = GetProperty<Weapon>("CorrectAgility"),
				CorrectMagicProperty = GetProperty<Weapon>("CorrectMagic"),
				CorrectFaithProperty = GetProperty<Weapon>("CorrectFaith"),
				PhysGuardCutRateProperty = GetProperty<Weapon>("PhysGuardCutRate"),
				MagGuardCutRateProperty = GetProperty<Weapon>("MagGuardCutRate"),
				FireGuardCutRateProperty = GetProperty<Weapon>("FireGuardCutRate"),
				ThunGuardCutRateProperty = GetProperty<Weapon>("ThunGuardCutRate"),
				SpEffectBehaviorId0Property = GetProperty<Weapon>("SpEffectBehaviorId0"),
				SpEffectBehaviorId1Property = GetProperty<Weapon>("SpEffectBehaviorId1"),
				SpEffectBehaviorId2Property = GetProperty<Weapon>("SpEffectBehaviorId2"),
				ResidentSpEffectIdProperty = GetProperty<Weapon>("ResidentSpEffectId"),
				ResidentSpEffectId1Property = GetProperty<Weapon>("ResidentSpEffectId1"),
				ResidentSpEffectId2Property = GetProperty<Weapon>("ResidentSpEffectId2"),
				MaterialSetIdProperty = GetProperty<Weapon>("MaterialSetId"),
				OriginEquipWepProperty = GetProperty<Weapon>("OriginEquipWep"),
				OriginEquipWep1Property = GetProperty<Weapon>("OriginEquipWep1"),
				OriginEquipWep2Property = GetProperty<Weapon>("OriginEquipWep2"),
				OriginEquipWep3Property = GetProperty<Weapon>("OriginEquipWep3"),
				OriginEquipWep4Property = GetProperty<Weapon>("OriginEquipWep4"),
				OriginEquipWep5Property = GetProperty<Weapon>("OriginEquipWep5"),
				OriginEquipWep6Property = GetProperty<Weapon>("OriginEquipWep6"),
				OriginEquipWep7Property = GetProperty<Weapon>("OriginEquipWep7"),
				OriginEquipWep8Property = GetProperty<Weapon>("OriginEquipWep8"),
				OriginEquipWep9Property = GetProperty<Weapon>("OriginEquipWep9"),
				OriginEquipWep10Property = GetProperty<Weapon>("OriginEquipWep10"),
				OriginEquipWep11Property = GetProperty<Weapon>("OriginEquipWep11"),
				OriginEquipWep12Property = GetProperty<Weapon>("OriginEquipWep12"),
				OriginEquipWep13Property = GetProperty<Weapon>("OriginEquipWep13"),
				OriginEquipWep14Property = GetProperty<Weapon>("OriginEquipWep14"),
				OriginEquipWep15Property = GetProperty<Weapon>("OriginEquipWep15"),
				AntiDemonDamageRateProperty = GetProperty<Weapon>("AntiDemonDamageRate"),
				AntSaintDamageRateProperty = GetProperty<Weapon>("AntSaintDamageRate"),
				AntWeakA_DamageRateProperty = GetProperty<Weapon>("AntWeakA_DamageRate"),
				AntWeakB_DamageRateProperty = GetProperty<Weapon>("AntWeakB_DamageRate"),
				IconIdProperty = GetProperty<Weapon>("IconId"),
				AttackThrowEscapeProperty = GetProperty<Weapon>("AttackThrowEscape"),
				ParryDamageLifeProperty = GetProperty<Weapon>("ParryDamageLife"),
				AttackBasePhysicsProperty = GetProperty<Weapon>("AttackBasePhysics"),
				AttackBaseMagicProperty = GetProperty<Weapon>("AttackBaseMagic"),
				AttackBaseFireProperty = GetProperty<Weapon>("AttackBaseFire"),
				AttackBaseThunderProperty = GetProperty<Weapon>("AttackBaseThunder"),
				AttackBaseStaminaProperty = GetProperty<Weapon>("AttackBaseStamina"),
				SaWeaponDamageProperty = GetProperty<Weapon>("SaWeaponDamage"),
				SaDurabilityProperty = GetProperty<Weapon>("SaDurability"),
				GuardAngleProperty = GetProperty<Weapon>("GuardAngle"),
				StaminaGuardDefProperty = GetProperty<Weapon>("StaminaGuardDef"),
				ReinforceTypeIdProperty = GetProperty<Weapon>("ReinforceTypeId"),
				TrophySGradeIdProperty = GetProperty<Weapon>("TrophySGradeId"),
				TrophySeqIdProperty = GetProperty<Weapon>("TrophySeqId"),
				ThrowAtkRateProperty = GetProperty<Weapon>("ThrowAtkRate"),
				BowDistRateProperty = GetProperty<Weapon>("BowDistRate"),
				WeaponCategoryProperty = GetProperty<Weapon>("WeaponCategory"),
				MotionCategoryProperty = GetProperty<Weapon>("MotionCategory"),
				GuardmotionCategoryProperty = GetProperty<Weapon>("GuardmotionCategory"),
				AtkMaterialProperty = GetProperty<Weapon>("AtkMaterial"),
				DefMaterialProperty = GetProperty<Weapon>("DefMaterial"),
				DefSfxMaterialProperty = GetProperty<Weapon>("DefSfxMaterial"),
				CorrectTypeProperty = GetProperty<Weapon>("CorrectType"),
				SpAttributeProperty = GetProperty<Weapon>("SpAttribute"),
				SpAtkcategoryProperty = GetProperty<Weapon>("SpAtkcategory"),
				WepmotionOneHandIdProperty = GetProperty<Weapon>("WepmotionOneHandId"),
				WepmotionBothHandIdProperty = GetProperty<Weapon>("WepmotionBothHandId"),
				ProperStrengthProperty = GetProperty<Weapon>("ProperStrength"),
				ProperAgilityProperty = GetProperty<Weapon>("ProperAgility"),
				ProperMagicProperty = GetProperty<Weapon>("ProperMagic"),
				ProperFaithProperty = GetProperty<Weapon>("ProperFaith"),
				OverStrengthProperty = GetProperty<Weapon>("OverStrength"),
				AttackBaseParryProperty = GetProperty<Weapon>("AttackBaseParry"),
				DefenseBaseParryProperty = GetProperty<Weapon>("DefenseBaseParry"),
				GuardBaseRepelProperty = GetProperty<Weapon>("GuardBaseRepel"),
				AttackBaseRepelProperty = GetProperty<Weapon>("AttackBaseRepel"),
				GuardCutCancelRateProperty = GetProperty<Weapon>("GuardCutCancelRate"),
				GuardLevelProperty = GetProperty<Weapon>("GuardLevel"),
				SlashGuardCutRateProperty = GetProperty<Weapon>("SlashGuardCutRate"),
				BlowGuardCutRateProperty = GetProperty<Weapon>("BlowGuardCutRate"),
				ThrustGuardCutRateProperty = GetProperty<Weapon>("ThrustGuardCutRate"),
				PoisonGuardResistProperty = GetProperty<Weapon>("PoisonGuardResist"),
				DiseaseGuardResistProperty = GetProperty<Weapon>("DiseaseGuardResist"),
				BloodGuardResistProperty = GetProperty<Weapon>("BloodGuardResist"),
				CurseGuardResistProperty = GetProperty<Weapon>("CurseGuardResist"),
				IsDurabilityDivergenceProperty = GetProperty<Weapon>("IsDurabilityDivergence"),
				RightHandEquipableProperty = GetProperty<Weapon>("RightHandEquipable"),
				LeftHandEquipableProperty = GetProperty<Weapon>("LeftHandEquipable"),
				BothHandEquipableProperty = GetProperty<Weapon>("BothHandEquipable"),
				ArrowSlotEquipableProperty = GetProperty<Weapon>("ArrowSlotEquipable"),
				BoltSlotEquipableProperty = GetProperty<Weapon>("BoltSlotEquipable"),
				EnableGuardProperty = GetProperty<Weapon>("EnableGuard"),
				EnableParryProperty = GetProperty<Weapon>("EnableParry"),
				EnableMagicProperty = GetProperty<Weapon>("EnableMagic"),
				EnableSorceryProperty = GetProperty<Weapon>("EnableSorcery"),
				EnableMiracleProperty = GetProperty<Weapon>("EnableMiracle"),
				EnableVowMagicProperty = GetProperty<Weapon>("EnableVowMagic"),
				IsNormalAttackTypeProperty = GetProperty<Weapon>("IsNormalAttackType"),
				IsBlowAttackTypeProperty = GetProperty<Weapon>("IsBlowAttackType"),
				IsSlashAttackTypeProperty = GetProperty<Weapon>("IsSlashAttackType"),
				IsThrustAttackTypeProperty = GetProperty<Weapon>("IsThrustAttackType"),
				IsEnhanceProperty = GetProperty<Weapon>("IsEnhance"),
				IsLuckCorrectProperty = GetProperty<Weapon>("IsLuckCorrect"),
				IsCustomProperty = GetProperty<Weapon>("IsCustom"),
				DisableBaseChangeResetProperty = GetProperty<Weapon>("DisableBaseChangeReset"),
				DisableRepairProperty = GetProperty<Weapon>("DisableRepair"),
				IsDarkHandProperty = GetProperty<Weapon>("IsDarkHand"),
				SimpleModelForDlcProperty = GetProperty<Weapon>("SimpleModelForDlc"),
				LanternWepProperty = GetProperty<Weapon>("LanternWep"),
				IsVersusGhostWepProperty = GetProperty<Weapon>("IsVersusGhostWep"),
				BaseChangeCategoryProperty = GetProperty<Weapon>("BaseChangeCategory"),
				IsDragonSlayerProperty = GetProperty<Weapon>("IsDragonSlayer"),
				DisableMultiDropShareProperty = GetProperty<Weapon>("DisableMultiDropShare"),
				Pad_0Property = GetProperty<Weapon>("Pad_0"),
				Pad_1Property = GetProperty<Weapon>("Pad_1");

			/// <summary>Behavioral variation ID</summary>
			/// <remarks>
			/// Japanese short name: "行動バリエーションID", Google translated: "Behavioral variation ID".
			/// Japanese description: "攻撃時に参照する行動パラメータIDを決定するときに使う", Google translated: "To use when determining the action parameter ID that reference at the time of the attack".
			/// </remarks>
			[ParameterTableRowAttribute("behaviorVariationId", index: 0, minimum: -1, maximum: 1E+08, step: 1, order: 400, unknown2: 1)]
			[DisplayName("Behavioral variation ID")]
			[Description("To use when determining the action parameter ID that reference at the time of the attack")]
			[DefaultValue((Int32)0)]
			public Int32 BehaviorVariationId {
				get { return behaviorVariationId; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for BehaviorVariationId.");
					SetProperty(ref behaviorVariationId, ref value, BehaviorVariationIdProperty);
				}
			}

			/// <summary>Equipment weight ratio</summary>
			/// <remarks>
			/// Japanese short name: "装備重量比率", Google translated: "Equipment weight ratio".
			/// Japanese description: "装備重量比率", Google translated: "Equipment weight ratio".
			/// </remarks>
			[ParameterTableRowAttribute("weaponWeightRate", index: 4, minimum: 0, maximum: 1, step: 0.001, order: 3100, unknown2: 1)]
			[DisplayName("Equipment weight ratio")]
			[Description("Equipment weight ratio")]
			[DefaultValue((Single)0)]
			public Single WeaponWeightRate {
				get { return weaponWeightRate; }
				set {
					if ((double)value < 0 || (double)value > 1)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1 for WeaponWeightRate.");
					SetProperty(ref weaponWeightRate, ref value, WeaponWeightRateProperty);
				}
			}

			/// <summary>Strength correction</summary>
			/// <remarks>
			/// Japanese short name: "筋力補正", Google translated: "Strength correction".
			/// Japanese description: "キャラパラ補正値.", Google translated: "Kyarapara correction value .".
			/// </remarks>
			[ParameterTableRowAttribute("correctStrength", index: 8, minimum: 0, maximum: 999, step: 0.1, order: 6100, unknown2: 1)]
			[DisplayName("Strength correction")]
			[Description("Kyarapara correction value .")]
			[DefaultValue((Single)0)]
			public Single CorrectStrength {
				get { return correctStrength; }
				set {
					if ((double)value < 0 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for CorrectStrength.");
					SetProperty(ref correctStrength, ref value, CorrectStrengthProperty);
				}
			}

			/// <summary>Agile correction</summary>
			/// <remarks>
			/// Japanese short name: "俊敏補正", Google translated: "Agile correction".
			/// Japanese description: "キャラパラ補正値.", Google translated: "Kyarapara correction value .".
			/// </remarks>
			[ParameterTableRowAttribute("correctAgility", index: 9, minimum: 0, maximum: 999, step: 0.1, order: 6200, unknown2: 1)]
			[DisplayName("Agile correction")]
			[Description("Kyarapara correction value .")]
			[DefaultValue((Single)0)]
			public Single CorrectAgility {
				get { return correctAgility; }
				set {
					if ((double)value < 0 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for CorrectAgility.");
					SetProperty(ref correctAgility, ref value, CorrectAgilityProperty);
				}
			}

			/// <summary>Magic correction</summary>
			/// <remarks>
			/// Japanese short name: "魔力補正", Google translated: "Magic correction".
			/// Japanese description: "キャラパラ補正値.", Google translated: "Kyarapara correction value .".
			/// </remarks>
			[ParameterTableRowAttribute("correctMagic", index: 10, minimum: 0, maximum: 999, step: 0.1, order: 6300, unknown2: 1)]
			[DisplayName("Magic correction")]
			[Description("Kyarapara correction value .")]
			[DefaultValue((Single)0)]
			public Single CorrectMagic {
				get { return correctMagic; }
				set {
					if ((double)value < 0 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for CorrectMagic.");
					SetProperty(ref correctMagic, ref value, CorrectMagicProperty);
				}
			}

			/// <summary>Faith correction</summary>
			/// <remarks>
			/// Japanese short name: "信仰補正", Google translated: "Faith correction".
			/// Japanese description: "キャラパラ補正値.", Google translated: "Kyarapara correction value .".
			/// </remarks>
			[ParameterTableRowAttribute("correctFaith", index: 11, minimum: 0, maximum: 999, step: 0.1, order: 6400, unknown2: 1)]
			[DisplayName("Faith correction")]
			[Description("Kyarapara correction value .")]
			[DefaultValue((Single)0)]
			public Single CorrectFaith {
				get { return correctFaith; }
				set {
					if ((double)value < 0 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for CorrectFaith.");
					SetProperty(ref correctFaith, ref value, CorrectFaithProperty);
				}
			}

			/// <summary>Guard during a physical attack rate cut</summary>
			/// <remarks>
			/// Japanese short name: "ガード時物理攻撃カット率", Google translated: "Guard during a physical attack rate cut".
			/// Japanese description: "ガード時のダメージカット率を各攻撃ごとに設定", Google translated: "Set to each per attack damage cut rate of the guard at the time".
			/// </remarks>
			[ParameterTableRowAttribute("physGuardCutRate", index: 12, minimum: 0, maximum: 100, step: 0.1, order: 6500, unknown2: 1)]
			[DisplayName("Guard during a physical attack rate cut")]
			[Description("Set to each per attack damage cut rate of the guard at the time")]
			[DefaultValue((Single)0)]
			public Single PhysGuardCutRate {
				get { return physGuardCutRate; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for PhysGuardCutRate.");
					SetProperty(ref physGuardCutRate, ref value, PhysGuardCutRateProperty);
				}
			}

			/// <summary>Guard when magic attack rate cut</summary>
			/// <remarks>
			/// Japanese short name: "ガード時魔法攻撃カット率", Google translated: "Guard when magic attack rate cut".
			/// Japanese description: "ガード攻撃でない場合は、0を入れる", Google translated: "If not guard attacks , put a 0".
			/// </remarks>
			[ParameterTableRowAttribute("magGuardCutRate", index: 13, minimum: 0, maximum: 100, step: 0.1, order: 6600, unknown2: 1)]
			[DisplayName("Guard when magic attack rate cut")]
			[Description("If not guard attacks , put a 0")]
			[DefaultValue((Single)0)]
			public Single MagGuardCutRate {
				get { return magGuardCutRate; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for MagGuardCutRate.");
					SetProperty(ref magGuardCutRate, ref value, MagGuardCutRateProperty);
				}
			}

			/// <summary>Guard when flame attack power cut rate</summary>
			/// <remarks>
			/// Japanese short name: "ガード時炎攻撃力カット率", Google translated: "Guard when flame attack power cut rate".
			/// Japanese description: "炎攻撃をどれだけカットするか？", Google translated: "You can cut how much the fire attack ?".
			/// </remarks>
			[ParameterTableRowAttribute("fireGuardCutRate", index: 14, minimum: 0, maximum: 100, step: 0.1, order: 6700, unknown2: 1)]
			[DisplayName("Guard when flame attack power cut rate")]
			[Description("You can cut how much the fire attack ?")]
			[DefaultValue((Single)0)]
			public Single FireGuardCutRate {
				get { return fireGuardCutRate; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for FireGuardCutRate.");
					SetProperty(ref fireGuardCutRate, ref value, FireGuardCutRateProperty);
				}
			}

			/// <summary>Guard when lightning attack power cut rate</summary>
			/// <remarks>
			/// Japanese short name: "ガード時電撃攻撃力カット率", Google translated: "Guard when lightning attack power cut rate".
			/// Japanese description: "電撃攻撃をどれだけカットするか？", Google translated: "You can cut much a bolt out of the blue attack ?".
			/// </remarks>
			[ParameterTableRowAttribute("thunGuardCutRate", index: 15, minimum: 0, maximum: 100, step: 0.1, order: 6800, unknown2: 1)]
			[DisplayName("Guard when lightning attack power cut rate")]
			[Description("You can cut much a bolt out of the blue attack ?")]
			[DefaultValue((Single)0)]
			public Single ThunGuardCutRate {
				get { return thunGuardCutRate; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for ThunGuardCutRate.");
					SetProperty(ref thunGuardCutRate, ref value, ThunGuardCutRateProperty);
				}
			}

			/// <summary>Special effects action ID0</summary>
			/// <remarks>
			/// Japanese short name: "特殊効果行動ID0", Google translated: "Special effects action ID0".
			/// Japanese description: "武器に特殊効果を追加するときに登録する", Google translated: "To register when you add special effects to weapons".
			/// </remarks>
			[ParameterTableRowAttribute("spEffectBehaviorId0", index: 16, minimum: -1, maximum: 1E+08, step: 1, order: 7700, unknown2: 1)]
			[DisplayName("Special effects action ID0")]
			[Description("To register when you add special effects to weapons")]
			[DefaultValue((Int32)(-1))]
			public Int32 SpEffectBehaviorId0 {
				get { return spEffectBehaviorId0; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for SpEffectBehaviorId0.");
					SetProperty(ref spEffectBehaviorId0, ref value, SpEffectBehaviorId0Property);
				}
			}

			/// <summary>Special effects action ID1</summary>
			/// <remarks>
			/// Japanese short name: "特殊効果行動ID1", Google translated: "Special effects action ID1".
			/// Japanese description: "武器に特殊効果を追加するときに登録する", Google translated: "To register when you add special effects to weapons".
			/// </remarks>
			[ParameterTableRowAttribute("spEffectBehaviorId1", index: 17, minimum: -1, maximum: 1E+08, step: 1, order: 7800, unknown2: 1)]
			[DisplayName("Special effects action ID1")]
			[Description("To register when you add special effects to weapons")]
			[DefaultValue((Int32)(-1))]
			public Int32 SpEffectBehaviorId1 {
				get { return spEffectBehaviorId1; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for SpEffectBehaviorId1.");
					SetProperty(ref spEffectBehaviorId1, ref value, SpEffectBehaviorId1Property);
				}
			}

			/// <summary>Special effects action ID2</summary>
			/// <remarks>
			/// Japanese short name: "特殊効果行動ID2", Google translated: "Special effects action ID2".
			/// Japanese description: "武器に特殊効果を追加するときに登録する", Google translated: "To register when you add special effects to weapons".
			/// </remarks>
			[ParameterTableRowAttribute("spEffectBehaviorId2", index: 18, minimum: -1, maximum: 1E+08, step: 1, order: 7900, unknown2: 1)]
			[DisplayName("Special effects action ID2")]
			[Description("To register when you add special effects to weapons")]
			[DefaultValue((Int32)(-1))]
			public Int32 SpEffectBehaviorId2 {
				get { return spEffectBehaviorId2; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for SpEffectBehaviorId2.");
					SetProperty(ref spEffectBehaviorId2, ref value, SpEffectBehaviorId2Property);
				}
			}

			/// <summary>Resident special effects ID</summary>
			/// <remarks>
			/// Japanese short name: "常駐特殊効果ID", Google translated: "Resident special effects ID".
			/// Japanese description: "常駐特殊効果ID0", Google translated: "Resident special effects ID0".
			/// </remarks>
			[ParameterTableRowAttribute("residentSpEffectId", index: 19, minimum: -1, maximum: 1E+08, step: 1, order: 8000, unknown2: 1)]
			[DisplayName("Resident special effects ID")]
			[Description("Resident special effects ID0")]
			[DefaultValue((Int32)(-1))]
			public Int32 ResidentSpEffectId {
				get { return residentSpEffectId; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for ResidentSpEffectId.");
					SetProperty(ref residentSpEffectId, ref value, ResidentSpEffectIdProperty);
				}
			}

			/// <summary>Resident special effects ID1</summary>
			/// <remarks>
			/// Japanese short name: "常駐特殊効果ID1", Google translated: "Resident special effects ID1".
			/// Japanese description: "常駐特殊効果ID1", Google translated: "Resident special effects ID1".
			/// </remarks>
			[ParameterTableRowAttribute("residentSpEffectId1", index: 20, minimum: -1, maximum: 1E+08, step: 1, order: 8100, unknown2: 1)]
			[DisplayName("Resident special effects ID1")]
			[Description("Resident special effects ID1")]
			[DefaultValue((Int32)(-1))]
			public Int32 ResidentSpEffectId1 {
				get { return residentSpEffectId1; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for ResidentSpEffectId1.");
					SetProperty(ref residentSpEffectId1, ref value, ResidentSpEffectId1Property);
				}
			}

			/// <summary>Resident special effects ID2</summary>
			/// <remarks>
			/// Japanese short name: "常駐特殊効果ID2", Google translated: "Resident special effects ID2".
			/// Japanese description: "常駐特殊効果ID2", Google translated: "Resident special effects ID2".
			/// </remarks>
			[ParameterTableRowAttribute("residentSpEffectId2", index: 21, minimum: -1, maximum: 1E+08, step: 1, order: 8200, unknown2: 1)]
			[DisplayName("Resident special effects ID2")]
			[Description("Resident special effects ID2")]
			[DefaultValue((Int32)(-1))]
			public Int32 ResidentSpEffectId2 {
				get { return residentSpEffectId2; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for ResidentSpEffectId2.");
					SetProperty(ref residentSpEffectId2, ref value, ResidentSpEffectId2Property);
				}
			}

			/// <summary>Material ID</summary>
			/// <remarks>
			/// Japanese short name: "素材ID", Google translated: "Material ID".
			/// Japanese description: "武器強化に必要な素材パラメータID", Google translated: "Material parameters ID required weapon enhancement".
			/// </remarks>
			[ParameterTableRowAttribute("materialSetId", index: 22, minimum: -1, maximum: 999999, step: 1, order: 8500, unknown2: 1)]
			[DisplayName("Material ID")]
			[Description("Material parameters ID required weapon enhancement")]
			[DefaultValue((Int32)(-1))]
			public Int32 MaterialSetId {
				get { return materialSetId; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for MaterialSetId.");
					SetProperty(ref materialSetId, ref value, MaterialSetIdProperty);
				}
			}

			/// <summary>Derived</summary>
			/// <remarks>
			/// Japanese short name: "派生元", Google translated: "Derived".
			/// Japanese description: "この武器の強化元武器ID", Google translated: "Strengthening the original weapon ID of this weapon".
			/// </remarks>
			[ParameterTableRowAttribute("originEquipWep", index: 23, minimum: -1, maximum: 1E+08, step: 1, order: 8600, unknown2: 1)]
			[DisplayName("Derived")]
			[Description("Strengthening the original weapon ID of this weapon")]
			[DefaultValue((Int32)(-1))]
			public Int32 OriginEquipWep {
				get { return originEquipWep; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for OriginEquipWep.");
					SetProperty(ref originEquipWep, ref value, OriginEquipWepProperty);
				}
			}

			/// <summary>Derive enhancement +1</summary>
			/// <remarks>
			/// Japanese short name: "派生元 強化+1", Google translated: "Derive enhancement +1".
			/// Japanese description: "この武器の強化元武器ID1", Google translated: "Strengthening the original weapon ID1 of this weapon".
			/// </remarks>
			[ParameterTableRowAttribute("originEquipWep1", index: 24, minimum: -1, maximum: 1E+08, step: 1, order: 8600, unknown2: 1)]
			[DisplayName("Derive enhancement +1")]
			[Description("Strengthening the original weapon ID1 of this weapon")]
			[DefaultValue((Int32)(-1))]
			public Int32 OriginEquipWep1 {
				get { return originEquipWep1; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for OriginEquipWep1.");
					SetProperty(ref originEquipWep1, ref value, OriginEquipWep1Property);
				}
			}

			/// <summary>Derive enhancement +2</summary>
			/// <remarks>
			/// Japanese short name: "派生元 強化+2", Google translated: "Derive enhancement +2".
			/// Japanese description: "この武器の強化元武器ID2", Google translated: "Strengthening the original weapon ID2 of this weapon".
			/// </remarks>
			[ParameterTableRowAttribute("originEquipWep2", index: 25, minimum: -1, maximum: 1E+08, step: 1, order: 8600, unknown2: 1)]
			[DisplayName("Derive enhancement +2")]
			[Description("Strengthening the original weapon ID2 of this weapon")]
			[DefaultValue((Int32)(-1))]
			public Int32 OriginEquipWep2 {
				get { return originEquipWep2; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for OriginEquipWep2.");
					SetProperty(ref originEquipWep2, ref value, OriginEquipWep2Property);
				}
			}

			/// <summary>Derive enhancement +3</summary>
			/// <remarks>
			/// Japanese short name: "派生元 強化+3", Google translated: "Derive enhancement +3".
			/// Japanese description: "この武器の強化元武器ID3", Google translated: "Strengthening the original weapon of this weapon ID3".
			/// </remarks>
			[ParameterTableRowAttribute("originEquipWep3", index: 26, minimum: -1, maximum: 1E+08, step: 1, order: 8600, unknown2: 1)]
			[DisplayName("Derive enhancement +3")]
			[Description("Strengthening the original weapon of this weapon ID3")]
			[DefaultValue((Int32)(-1))]
			public Int32 OriginEquipWep3 {
				get { return originEquipWep3; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for OriginEquipWep3.");
					SetProperty(ref originEquipWep3, ref value, OriginEquipWep3Property);
				}
			}

			/// <summary>Derive enhancement +4</summary>
			/// <remarks>
			/// Japanese short name: "派生元 強化+4", Google translated: "Derive enhancement +4".
			/// Japanese description: "この武器の強化元武器ID4", Google translated: "Strengthening the original weapon ID4 of this weapon".
			/// </remarks>
			[ParameterTableRowAttribute("originEquipWep4", index: 27, minimum: -1, maximum: 1E+08, step: 1, order: 8600, unknown2: 1)]
			[DisplayName("Derive enhancement +4")]
			[Description("Strengthening the original weapon ID4 of this weapon")]
			[DefaultValue((Int32)(-1))]
			public Int32 OriginEquipWep4 {
				get { return originEquipWep4; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for OriginEquipWep4.");
					SetProperty(ref originEquipWep4, ref value, OriginEquipWep4Property);
				}
			}

			/// <summary>Derive enhancement +5</summary>
			/// <remarks>
			/// Japanese short name: "派生元 強化+5", Google translated: "Derive enhancement +5".
			/// Japanese description: "この武器の強化元武器ID5", Google translated: "Strengthening the original weapon ID5 of this weapon".
			/// </remarks>
			[ParameterTableRowAttribute("originEquipWep5", index: 28, minimum: -1, maximum: 1E+08, step: 1, order: 8600, unknown2: 1)]
			[DisplayName("Derive enhancement +5")]
			[Description("Strengthening the original weapon ID5 of this weapon")]
			[DefaultValue((Int32)(-1))]
			public Int32 OriginEquipWep5 {
				get { return originEquipWep5; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for OriginEquipWep5.");
					SetProperty(ref originEquipWep5, ref value, OriginEquipWep5Property);
				}
			}

			/// <summary>Derive enhancement +6</summary>
			/// <remarks>
			/// Japanese short name: "派生元 強化+6", Google translated: "Derive enhancement +6".
			/// Japanese description: "この武器の強化元武器ID6", Google translated: "Strengthening the original weapon ID6 of this weapon".
			/// </remarks>
			[ParameterTableRowAttribute("originEquipWep6", index: 29, minimum: -1, maximum: 1E+08, step: 1, order: 8600, unknown2: 1)]
			[DisplayName("Derive enhancement +6")]
			[Description("Strengthening the original weapon ID6 of this weapon")]
			[DefaultValue((Int32)(-1))]
			public Int32 OriginEquipWep6 {
				get { return originEquipWep6; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for OriginEquipWep6.");
					SetProperty(ref originEquipWep6, ref value, OriginEquipWep6Property);
				}
			}

			/// <summary>Derive enhancement +7</summary>
			/// <remarks>
			/// Japanese short name: "派生元 強化+7", Google translated: "Derive enhancement +7".
			/// Japanese description: "この武器の強化元武器ID7", Google translated: "Strengthening the original weapon ID7 of this weapon".
			/// </remarks>
			[ParameterTableRowAttribute("originEquipWep7", index: 30, minimum: -1, maximum: 1E+08, step: 1, order: 8600, unknown2: 1)]
			[DisplayName("Derive enhancement +7")]
			[Description("Strengthening the original weapon ID7 of this weapon")]
			[DefaultValue((Int32)(-1))]
			public Int32 OriginEquipWep7 {
				get { return originEquipWep7; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for OriginEquipWep7.");
					SetProperty(ref originEquipWep7, ref value, OriginEquipWep7Property);
				}
			}

			/// <summary>Derive enhancement +8</summary>
			/// <remarks>
			/// Japanese short name: "派生元 強化+8", Google translated: "Derive enhancement +8".
			/// Japanese description: "この武器の強化元武器ID8", Google translated: "Strengthening the original weapon ID8 of this weapon".
			/// </remarks>
			[ParameterTableRowAttribute("originEquipWep8", index: 31, minimum: -1, maximum: 1E+08, step: 1, order: 8600, unknown2: 1)]
			[DisplayName("Derive enhancement +8")]
			[Description("Strengthening the original weapon ID8 of this weapon")]
			[DefaultValue((Int32)(-1))]
			public Int32 OriginEquipWep8 {
				get { return originEquipWep8; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for OriginEquipWep8.");
					SetProperty(ref originEquipWep8, ref value, OriginEquipWep8Property);
				}
			}

			/// <summary>Derive enhancement +9</summary>
			/// <remarks>
			/// Japanese short name: "派生元 強化+9", Google translated: "Derive enhancement +9".
			/// Japanese description: "この武器の強化元武器ID9", Google translated: "Strengthening the original weapon ID9 of this weapon".
			/// </remarks>
			[ParameterTableRowAttribute("originEquipWep9", index: 32, minimum: -1, maximum: 1E+08, step: 1, order: 8600, unknown2: 1)]
			[DisplayName("Derive enhancement +9")]
			[Description("Strengthening the original weapon ID9 of this weapon")]
			[DefaultValue((Int32)(-1))]
			public Int32 OriginEquipWep9 {
				get { return originEquipWep9; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for OriginEquipWep9.");
					SetProperty(ref originEquipWep9, ref value, OriginEquipWep9Property);
				}
			}

			/// <summary>Derive enhancement +10</summary>
			/// <remarks>
			/// Japanese short name: "派生元 強化+10", Google translated: "Derive enhancement +10".
			/// Japanese description: "この武器の強化元武器ID10", Google translated: "Strengthening the original weapon ID10 of this weapon".
			/// </remarks>
			[ParameterTableRowAttribute("originEquipWep10", index: 33, minimum: -1, maximum: 1E+08, step: 1, order: 8600, unknown2: 1)]
			[DisplayName("Derive enhancement +10")]
			[Description("Strengthening the original weapon ID10 of this weapon")]
			[DefaultValue((Int32)(-1))]
			public Int32 OriginEquipWep10 {
				get { return originEquipWep10; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for OriginEquipWep10.");
					SetProperty(ref originEquipWep10, ref value, OriginEquipWep10Property);
				}
			}

			/// <summary>Derive enhancement +11</summary>
			/// <remarks>
			/// Japanese short name: "派生元 強化+11", Google translated: "Derive enhancement +11".
			/// Japanese description: "この武器の強化元武器ID11", Google translated: "Strengthening the original weapon ID11 of this weapon".
			/// </remarks>
			[ParameterTableRowAttribute("originEquipWep11", index: 34, minimum: -1, maximum: 1E+08, step: 1, order: 8600, unknown2: 1)]
			[DisplayName("Derive enhancement +11")]
			[Description("Strengthening the original weapon ID11 of this weapon")]
			[DefaultValue((Int32)(-1))]
			public Int32 OriginEquipWep11 {
				get { return originEquipWep11; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for OriginEquipWep11.");
					SetProperty(ref originEquipWep11, ref value, OriginEquipWep11Property);
				}
			}

			/// <summary>Derive enhancement +12</summary>
			/// <remarks>
			/// Japanese short name: "派生元 強化+12", Google translated: "Derive enhancement +12".
			/// Japanese description: "この武器の強化元武器ID12", Google translated: "Strengthening the original weapon ID12 of this weapon".
			/// </remarks>
			[ParameterTableRowAttribute("originEquipWep12", index: 35, minimum: -1, maximum: 1E+08, step: 1, order: 8600, unknown2: 1)]
			[DisplayName("Derive enhancement +12")]
			[Description("Strengthening the original weapon ID12 of this weapon")]
			[DefaultValue((Int32)(-1))]
			public Int32 OriginEquipWep12 {
				get { return originEquipWep12; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for OriginEquipWep12.");
					SetProperty(ref originEquipWep12, ref value, OriginEquipWep12Property);
				}
			}

			/// <summary>Derive enhancement +13</summary>
			/// <remarks>
			/// Japanese short name: "派生元 強化+13", Google translated: "Derive enhancement +13".
			/// Japanese description: "この武器の強化元武器ID13", Google translated: "Strengthening the original weapon ID13 of this weapon".
			/// </remarks>
			[ParameterTableRowAttribute("originEquipWep13", index: 36, minimum: -1, maximum: 1E+08, step: 1, order: 8600, unknown2: 1)]
			[DisplayName("Derive enhancement +13")]
			[Description("Strengthening the original weapon ID13 of this weapon")]
			[DefaultValue((Int32)(-1))]
			public Int32 OriginEquipWep13 {
				get { return originEquipWep13; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for OriginEquipWep13.");
					SetProperty(ref originEquipWep13, ref value, OriginEquipWep13Property);
				}
			}

			/// <summary>Derive enhancement +14</summary>
			/// <remarks>
			/// Japanese short name: "派生元 強化+14", Google translated: "Derive enhancement +14".
			/// Japanese description: "この武器の強化元武器ID14", Google translated: "Strengthening the original weapon ID14 of this weapon".
			/// </remarks>
			[ParameterTableRowAttribute("originEquipWep14", index: 37, minimum: -1, maximum: 1E+08, step: 1, order: 8600, unknown2: 1)]
			[DisplayName("Derive enhancement +14")]
			[Description("Strengthening the original weapon ID14 of this weapon")]
			[DefaultValue((Int32)(-1))]
			public Int32 OriginEquipWep14 {
				get { return originEquipWep14; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for OriginEquipWep14.");
					SetProperty(ref originEquipWep14, ref value, OriginEquipWep14Property);
				}
			}

			/// <summary>Derive enhancement +15</summary>
			/// <remarks>
			/// Japanese short name: "派生元 強化+15", Google translated: "Derive enhancement +15".
			/// Japanese description: "この武器の強化元武器ID15", Google translated: "Strengthening the original weapon ID15 of this weapon".
			/// </remarks>
			[ParameterTableRowAttribute("originEquipWep15", index: 38, minimum: -1, maximum: 1E+08, step: 1, order: 8600, unknown2: 1)]
			[DisplayName("Derive enhancement +15")]
			[Description("Strengthening the original weapon ID15 of this weapon")]
			[DefaultValue((Int32)(-1))]
			public Int32 OriginEquipWep15 {
				get { return originEquipWep15; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for OriginEquipWep15.");
					SetProperty(ref originEquipWep15, ref value, OriginEquipWep15Property);
				}
			}

			/// <summary>Versus daemon damage magnification</summary>
			/// <remarks>
			/// Japanese short name: "対デーモンダメージ倍率", Google translated: "Versus daemon damage magnification".
			/// Japanese description: "対デーモン用のダメージ倍率", Google translated: "Magnification of damage versus daemon".
			/// </remarks>
			[ParameterTableRowAttribute("antiDemonDamageRate", index: 39, minimum: 0, maximum: 99, step: 0.01, order: 8900, unknown2: 1)]
			[DisplayName("Versus daemon damage magnification")]
			[Description("Magnification of damage versus daemon")]
			[DefaultValue((Single)1)]
			public Single AntiDemonDamageRate {
				get { return antiDemonDamageRate; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for AntiDemonDamageRate.");
					SetProperty(ref antiDemonDamageRate, ref value, AntiDemonDamageRateProperty);
				}
			}

			/// <summary>Versus sacred damage magnification</summary>
			/// <remarks>
			/// Japanese short name: "対神聖ダメージ倍率", Google translated: "Versus sacred damage magnification".
			/// Japanese description: "対神聖弱点用ダメージ倍率", Google translated: "Versus sacred weakness for damage magnification".
			/// </remarks>
			[ParameterTableRowAttribute("antSaintDamageRate", index: 40, minimum: 0, maximum: 99, step: 0.01, order: 8910, unknown2: 1)]
			[DisplayName("Versus sacred damage magnification")]
			[Description("Versus sacred weakness for damage magnification")]
			[DefaultValue((Single)1)]
			public Single AntSaintDamageRate {
				get { return antSaintDamageRate; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for AntSaintDamageRate.");
					SetProperty(ref antSaintDamageRate, ref value, AntSaintDamageRateProperty);
				}
			}

			/// <summary>A weakness versus damage magnification</summary>
			/// <remarks>
			/// Japanese short name: "対弱点Aダメージ倍率", Google translated: "A weakness versus damage magnification".
			/// Japanese description: "対弱点A用ダメージ倍率", Google translated: "A weakness versus magnification for damage".
			/// </remarks>
			[ParameterTableRowAttribute("antWeakA_DamageRate", index: 41, minimum: 0, maximum: 99, step: 0.01, order: 8920, unknown2: 1)]
			[DisplayName("A weakness versus damage magnification")]
			[Description("A weakness versus magnification for damage")]
			[DefaultValue((Single)1)]
			public Single AntWeakA_DamageRate {
				get { return antWeakA_DamageRate; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for AntWeakA_DamageRate.");
					SetProperty(ref antWeakA_DamageRate, ref value, AntWeakA_DamageRateProperty);
				}
			}

			/// <summary>Weakness versus B damage magnification</summary>
			/// <remarks>
			/// Japanese short name: "対弱点Bダメージ倍率", Google translated: "Weakness versus B damage magnification".
			/// Japanese description: "対弱点B用ダメージ倍率", Google translated: "Weakness versus B for damage magnification".
			/// </remarks>
			[ParameterTableRowAttribute("antWeakB_DamageRate", index: 42, minimum: 0, maximum: 99, step: 0.01, order: 8930, unknown2: 1)]
			[DisplayName("Weakness versus B damage magnification")]
			[Description("Weakness versus B for damage magnification")]
			[DefaultValue((Single)1)]
			public Single AntWeakB_DamageRate {
				get { return antWeakB_DamageRate; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for AntWeakB_DamageRate.");
					SetProperty(ref antWeakB_DamageRate, ref value, AntWeakB_DamageRateProperty);
				}
			}

			/// <summary>Icon ID</summary>
			/// <remarks>
			/// Japanese short name: "アイコンID", Google translated: "Icon ID".
			/// Japanese description: "メニューアイコンID.", Google translated: "Menu icon ID.".
			/// </remarks>
			[ParameterTableRowAttribute("iconId", index: 47, minimum: 0, maximum: 9999, step: 1, order: 2300, unknown2: 1)]
			[DisplayName("Icon ID")]
			[Description("Menu icon ID.")]
			[DefaultValue((UInt16)0)]
			public UInt16 IconId {
				get { return iconId; }
				set {
					if ((double)value < 0 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9999 for IconId.");
					SetProperty(ref iconId, ref value, IconIdProperty);
				}
			}

			/// <summary>Attack power base value missing throw</summary>
			/// <remarks>
			/// Japanese short name: "投げ抜け攻撃力基本値", Google translated: "Attack power base value missing throw".
			/// Japanese description: "投げ抜け攻撃力の基本値", Google translated: "Base value of attack power missing throw".
			/// </remarks>
			[ParameterTableRowAttribute("attackThrowEscape", index: 50, minimum: 0, maximum: 9999, step: 1, order: 4200, unknown2: 1)]
			[DisplayName("Attack power base value missing throw")]
			[Description("Base value of attack power missing throw")]
			[DefaultValue((UInt16)0)]
			public UInt16 AttackThrowEscape {
				get { return attackThrowEscape; }
				set {
					if ((double)value < 0 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9999 for AttackThrowEscape.");
					SetProperty(ref attackThrowEscape, ref value, AttackThrowEscapeProperty);
				}
			}

			/// <summary>Parry generation time [frame]</summary>
			/// <remarks>
			/// Japanese short name: "パリィ発生時間[frame]", Google translated: "Parry generation time [frame]".
			/// Japanese description: "パリィダメージの寿命を制限する。TimeActで設定されている以上には持続しない。", Google translated: "I will limit the life of the Parry damage . It does not persist more than it is set in TimeAct.".
			/// </remarks>
			[ParameterTableRowAttribute("parryDamageLife", index: 51, minimum: -1, maximum: 9999, step: 1, order: 4300, unknown2: 1)]
			[DisplayName("Parry generation time [frame]")]
			[Description("I will limit the life of the Parry damage . It does not persist more than it is set in TimeAct.")]
			[DefaultValue((Int16)(-1))]
			public Int16 ParryDamageLife {
				get { return parryDamageLife; }
				set {
					if ((double)value < -1 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 9999 for ParryDamageLife.");
					SetProperty(ref parryDamageLife, ref value, ParryDamageLifeProperty);
				}
			}

			/// <summary>Physical Attack base value</summary>
			/// <remarks>
			/// Japanese short name: "物理攻撃力基本値", Google translated: "Physical Attack base value".
			/// Japanese description: "敵のＨＰにダメージを与える物理属性攻撃の基本値", Google translated: "Basic value of the physical attribute attack damage to HP of the enemy".
			/// </remarks>
			[ParameterTableRowAttribute("attackBasePhysics", index: 52, minimum: 0, maximum: 9999, step: 1, order: 5400, unknown2: 1)]
			[DisplayName("Physical Attack base value")]
			[Description("Basic value of the physical attribute attack damage to HP of the enemy")]
			[DefaultValue((UInt16)100)]
			public UInt16 AttackBasePhysics {
				get { return attackBasePhysics; }
				set {
					if ((double)value < 0 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9999 for AttackBasePhysics.");
					SetProperty(ref attackBasePhysics, ref value, AttackBasePhysicsProperty);
				}
			}

			/// <summary>Magic Attack base value</summary>
			/// <remarks>
			/// Japanese short name: "魔法攻撃力基本値", Google translated: "Magic Attack base value".
			/// Japanese description: "敵のＨＰにダメージを与える魔法属性攻撃の基本値", Google translated: "The base value of the attribute magic attack damage to HP of the enemy".
			/// </remarks>
			[ParameterTableRowAttribute("attackBaseMagic", index: 53, minimum: 0, maximum: 9999, step: 1, order: 5500, unknown2: 1)]
			[DisplayName("Magic Attack base value")]
			[Description("The base value of the attribute magic attack damage to HP of the enemy")]
			[DefaultValue((UInt16)100)]
			public UInt16 AttackBaseMagic {
				get { return attackBaseMagic; }
				set {
					if ((double)value < 0 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9999 for AttackBaseMagic.");
					SetProperty(ref attackBaseMagic, ref value, AttackBaseMagicProperty);
				}
			}

			/// <summary>Flame attack power base value</summary>
			/// <remarks>
			/// Japanese short name: "炎攻撃力基本値", Google translated: "Flame attack power base value".
			/// Japanese description: "敵のＨＰにダメージを与える炎属性攻撃の基本値", Google translated: "Base value of FIRE attack damage to HP of the enemy".
			/// </remarks>
			[ParameterTableRowAttribute("attackBaseFire", index: 54, minimum: 0, maximum: 9999, step: 1, order: 5600, unknown2: 1)]
			[DisplayName("Flame attack power base value")]
			[Description("Base value of FIRE attack damage to HP of the enemy")]
			[DefaultValue((UInt16)100)]
			public UInt16 AttackBaseFire {
				get { return attackBaseFire; }
				set {
					if ((double)value < 0 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9999 for AttackBaseFire.");
					SetProperty(ref attackBaseFire, ref value, AttackBaseFireProperty);
				}
			}

			/// <summary>Bolt out of the blue attack force base value</summary>
			/// <remarks>
			/// Japanese short name: "電撃攻撃力基本値", Google translated: "Bolt out of the blue attack force base value".
			/// Japanese description: "敵のＨＰにダメージを与える電撃属性攻撃の基本値", Google translated: "The base value of the attribute blitz attack damage to HP of the enemy".
			/// </remarks>
			[ParameterTableRowAttribute("attackBaseThunder", index: 55, minimum: 0, maximum: 9999, step: 1, order: 5700, unknown2: 1)]
			[DisplayName("Bolt out of the blue attack force base value")]
			[Description("The base value of the attribute blitz attack damage to HP of the enemy")]
			[DefaultValue((UInt16)100)]
			public UInt16 AttackBaseThunder {
				get { return attackBaseThunder; }
				set {
					if ((double)value < 0 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9999 for AttackBaseThunder.");
					SetProperty(ref attackBaseThunder, ref value, AttackBaseThunderProperty);
				}
			}

			/// <summary>Stamina attack power</summary>
			/// <remarks>
			/// Japanese short name: "スタミナ攻撃力", Google translated: "Stamina attack power".
			/// Japanese description: "敵へのスタミナ攻撃力", Google translated: "Stamina attack force on the enemy".
			/// </remarks>
			[ParameterTableRowAttribute("attackBaseStamina", index: 56, minimum: 0, maximum: 999, step: 1, order: 5800, unknown2: 1)]
			[DisplayName("Stamina attack power")]
			[Description("Stamina attack force on the enemy")]
			[DefaultValue((UInt16)100)]
			public UInt16 AttackBaseStamina {
				get { return attackBaseStamina; }
				set {
					if ((double)value < 0 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for AttackBaseStamina.");
					SetProperty(ref attackBaseStamina, ref value, AttackBaseStaminaProperty);
				}
			}

			/// <summary>SA weapon attack force</summary>
			/// <remarks>
			/// Japanese short name: "SA武器攻撃力", Google translated: "SA weapon attack force".
			/// Japanese description: "スーパーアーマー基本攻撃力", Google translated: "Super Armor basic attack power".
			/// </remarks>
			[ParameterTableRowAttribute("saWeaponDamage", index: 57, minimum: 0, maximum: 9999, step: 1, order: 5900, unknown2: 1)]
			[DisplayName("SA weapon attack force")]
			[Description("Super Armor basic attack power")]
			[DefaultValue((UInt16)0)]
			public UInt16 SaWeaponDamage {
				get { return saWeaponDamage; }
				set {
					if ((double)value < 0 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9999 for SaWeaponDamage.");
					SetProperty(ref saWeaponDamage, ref value, SaWeaponDamageProperty);
				}
			}

			/// <summary>SA Durability</summary>
			/// <remarks>
			/// Japanese short name: "SA耐久値", Google translated: "SA Durability".
			/// Japanese description: "攻撃モーション中に使われる追加SA耐久値", Google translated: "Add SA endurance value used to attack in motion".
			/// </remarks>
			[ParameterTableRowAttribute("saDurability", index: 58, minimum: -1, maximum: 999, step: 1, order: 6000, unknown2: 1)]
			[DisplayName("SA Durability")]
			[Description("Add SA endurance value used to attack in motion")]
			[DefaultValue((Int16)0)]
			public Int16 SaDurability {
				get { return saDurability; }
				set {
					if ((double)value < -1 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999 for SaDurability.");
					SetProperty(ref saDurability, ref value, SaDurabilityProperty);
				}
			}

			/// <summary>Guard range [deg]</summary>
			/// <remarks>
			/// Japanese short name: "ガード範囲[deg]", Google translated: "Guard range [deg]".
			/// Japanese description: "武器のガード時の防御発生範囲角度", Google translated: "Defense occurrence range angle of the guard when the weapon".
			/// </remarks>
			[ParameterTableRowAttribute("guardAngle", index: 59, minimum: 0, maximum: 180, step: 1, order: 5200, unknown2: 1)]
			[DisplayName("Guard range [deg]")]
			[Description("Defense occurrence range angle of the guard when the weapon")]
			[DefaultValue((Int16)0)]
			public Int16 GuardAngle {
				get { return guardAngle; }
				set {
					if ((double)value < 0 || (double)value > 180)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 180 for GuardAngle.");
					SetProperty(ref guardAngle, ref value, GuardAngleProperty);
				}
			}

			/// <summary>Guard when stamina Defense</summary>
			/// <remarks>
			/// Japanese short name: "ガード時スタミナ防御力", Google translated: "Guard when stamina Defense".
			/// Japanese description: "ガード成功時に、敵のスタミナ攻撃に対する防御力", Google translated: "To guard success , defense against enemy attack stamina".
			/// </remarks>
			[ParameterTableRowAttribute("staminaGuardDef", index: 60, minimum: 0, maximum: 100, step: 1, order: 7600, unknown2: 1)]
			[DisplayName("Guard when stamina Defense")]
			[Description("To guard success , defense against enemy attack stamina")]
			[DefaultValue((Int16)0)]
			public Int16 StaminaGuardDef {
				get { return staminaGuardDef; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for StaminaGuardDef.");
					SetProperty(ref staminaGuardDef, ref value, StaminaGuardDefProperty);
				}
			}

			/// <summary>Reinforced type ID</summary>
			/// <remarks>
			/// Japanese short name: "強化タイプID", Google translated: "Reinforced type ID".
			/// Japanese description: "強化タイプID", Google translated: "Reinforced type ID".
			/// </remarks>
			[ParameterTableRowAttribute("reinforceTypeId", index: 61, minimum: 0, maximum: 9999, step: 1, order: 8300, unknown2: 1)]
			[DisplayName("Reinforced type ID")]
			[Description("Reinforced type ID")]
			[DefaultValue((Int16)0)]
			public Int16 ReinforceTypeId {
				get { return reinforceTypeId; }
				set {
					if ((double)value < 0 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9999 for ReinforceTypeId.");
					SetProperty(ref reinforceTypeId, ref value, ReinforceTypeIdProperty);
				}
			}

			/// <summary>S grade trophy ID</summary>
			/// <remarks>
			/// Japanese short name: "トロフィーＳグレードID", Google translated: "S grade trophy ID".
			/// Japanese description: "トロフィーシステムに関係あるか？", Google translated: "Is there related to the trophy system ?".
			/// </remarks>
			[ParameterTableRowAttribute("trophySGradeId", index: 62, minimum: -1, maximum: 9999, step: 1, order: 8700, unknown2: 1)]
			[DisplayName("S grade trophy ID")]
			[Description("Is there related to the trophy system ?")]
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
			/// Japanese description: "トロフィーのSEQ番号（１３～２９）", Google translated: "SEQ number of trophy ( 13-29 )".
			/// </remarks>
			[ParameterTableRowAttribute("trophySeqId", index: 63, minimum: -1, maximum: 99, step: 1, order: 8800, unknown2: 1)]
			[DisplayName("Trophy SEQ number")]
			[Description("SEQ number of trophy ( 13-29 )")]
			[DefaultValue((Int16)(-1))]
			public Int16 TrophySeqId {
				get { return trophySeqId; }
				set {
					if ((double)value < -1 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 99 for TrophySeqId.");
					SetProperty(ref trophySeqId, ref value, TrophySeqIdProperty);
				}
			}

			/// <summary>Magnification force attack throw</summary>
			/// <remarks>
			/// Japanese short name: "投げ攻撃力倍率", Google translated: "Magnification force attack throw".
			/// Japanese description: "投げの攻撃力倍率", Google translated: "Attack power magnification of the throw".
			/// </remarks>
			[ParameterTableRowAttribute("throwAtkRate", index: 64, minimum: -100, maximum: 999, step: 1, order: 9000, unknown2: 1)]
			[DisplayName("Magnification force attack throw")]
			[Description("Attack power magnification of the throw")]
			[DefaultValue((Int16)0)]
			public Int16 ThrowAtkRate {
				get { return throwAtkRate; }
				set {
					if ((double)value < -100 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -100 to 999 for ThrowAtkRate.");
					SetProperty(ref throwAtkRate, ref value, ThrowAtkRateProperty);
				}
			}

			/// <summary>Bow flying distance correction [ %]</summary>
			/// <remarks>
			/// Japanese short name: "弓飛距離補正[％]", Google translated: "Bow flying distance correction [ %]".
			/// Japanese description: "飛距離を伸ばすアップ％", Google translated: "% Up to extend the distance".
			/// </remarks>
			[ParameterTableRowAttribute("bowDistRate", index: 65, minimum: -100, maximum: 999, step: 1, order: 9100, unknown2: 1)]
			[DisplayName("Bow flying distance correction [ %]")]
			[Description("% Up to extend the distance")]
			[DefaultValue((Int16)0)]
			public Int16 BowDistRate {
				get { return bowDistRate; }
				set {
					if ((double)value < -100 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -100 to 999 for BowDistRate.");
					SetProperty(ref bowDistRate, ref value, BowDistRateProperty);
				}
			}

			/// <summary>Weapon category</summary>
			/// <remarks>
			/// Japanese short name: "武器カテゴリ", Google translated: "Weapon category".
			/// Japanese description: "武器のカテゴリ.", Google translated: "Category of weapons .".
			/// </remarks>
			[ParameterTableRowAttribute("weaponCategory", index: 68, minimum: 0, maximum: 99, step: 1, order: 500, unknown2: 1)]
			[DisplayName("Weapon category")]
			[Description("Category of weapons .")]
			[DefaultValue((WeaponCategory)0)]
			public WeaponCategory WeaponCategory {
				get { return weaponCategory; }
				set { SetProperty(ref weaponCategory, ref value, WeaponCategoryProperty); }
			}

			/// <summary>Weapon motion category. This value times 100,000 indexes "AtkParam_Pc.param".</summary>
			/// <remarks>
			/// Japanese short name: "武器モーションカテゴリ", Google translated: "Weapon motion category".
			/// Japanese description: "武器モーションのカテゴリ.", Google translated: "Category of weapons motion .".
			/// </remarks>
			[ParameterTableRowAttribute("wepmotionCategory", index: 69, minimum: 0, maximum: 99, step: 1, order: 700, unknown2: 1)]
			[DisplayName("Weapon motion category")]
			[Description("Category of weapons motion .")]
			[DefaultValue((WeaponMotionCategory)0)]
			public WeaponMotionCategory MotionCategory {
				get { return motionCategory; }
				set { SetProperty(ref motionCategory, ref value, MotionCategoryProperty); }
			}

			/// <summary>Guard motion category</summary>
			/// <remarks>
			/// Japanese short name: "ガードモーションカテゴリ", Google translated: "Guard motion category".
			/// Japanese description: "ガードモーションのカテゴリ", Google translated: "Category of guard motion".
			/// </remarks>
			[ParameterTableRowAttribute("guardmotionCategory", index: 70, minimum: 0, maximum: 255, step: 1, order: 800, unknown2: 1)]
			[DisplayName("Guard motion category")]
			[Description("Category of guard motion")]
			[DefaultValue((GuardMotionCategory)0)]
			public GuardMotionCategory GuardmotionCategory {
				get { return guardmotionCategory; }
				set { SetProperty(ref guardmotionCategory, ref value, GuardmotionCategoryProperty); }
			}

			/// <summary>Attack Material</summary>
			/// <remarks>
			/// Japanese short name: "攻撃材質", Google translated: "Attack Material".
			/// Japanese description: "攻撃パラから使用される攻撃材質", Google translated: "Attack the material used from the attack para".
			/// </remarks>
			[ParameterTableRowAttribute("atkMaterial", index: 71, minimum: 0, maximum: 255, step: 1, order: 2000, unknown2: 1)]
			[DisplayName("Attack Material")]
			[Description("Attack the material used from the attack para")]
			[DefaultValue((WeaponMaterialAttack)0)]
			public WeaponMaterialAttack AtkMaterial {
				get { return atkMaterial; }
				set { SetProperty(ref atkMaterial, ref value, AtkMaterialProperty); }
			}

			/// <summary>Defense Material</summary>
			/// <remarks>
			/// Japanese short name: "防御材質", Google translated: "Defense Material".
			/// Japanese description: "攻撃パラから使用される防御材質", Google translated: "Defense material used from para attack".
			/// </remarks>
			[ParameterTableRowAttribute("defMaterial", index: 72, minimum: 0, maximum: 255, step: 1, order: 2100, unknown2: 1)]
			[DisplayName("Defense Material")]
			[Description("Defense material used from para attack")]
			[DefaultValue((WeaponMaterialDefend)0)]
			public WeaponMaterialDefend DefMaterial {
				get { return defMaterial; }
				set { SetProperty(ref defMaterial, ref value, DefMaterialProperty); }
			}

			/// <summary>SFX Defense Material</summary>
			/// <remarks>
			/// Japanese short name: "防御SFX材質", Google translated: "SFX Defense Material".
			/// Japanese description: "攻撃パラから使用される防御SFX材質", Google translated: "SFX defense material to be used from para attack".
			/// </remarks>
			[ParameterTableRowAttribute("defSfxMaterial", index: 73, minimum: 0, maximum: 255, step: 1, order: 2200, unknown2: 1)]
			[DisplayName("SFX Defense Material")]
			[Description("SFX defense material to be used from para attack")]
			[DefaultValue((WeaponMaterialDefendSound)0)]
			public WeaponMaterialDefendSound DefSfxMaterial {
				get { return defSfxMaterial; }
				set { SetProperty(ref defSfxMaterial, ref value, DefSfxMaterialProperty); }
			}

			/// <summary>Correction type</summary>
			/// <remarks>
			/// Japanese short name: "補正タイプ", Google translated: "Correction type".
			/// Japanese description: "一次パラメータによる補正グラフのタイプを決める", Google translated: "I decide the type of correction graph by the primary parameters".
			/// </remarks>
			[ParameterTableRowAttribute("correctType", index: 74, minimum: 0, maximum: 255, step: 1, order: 8400, unknown2: 1)]
			[DisplayName("Correction type")]
			[Description("I decide the type of correction graph by the primary parameters")]
			[DefaultValue((WeaponCorrectType)0)]
			public WeaponCorrectType CorrectType {
				get { return correctType; }
				set { SetProperty(ref correctType, ref value, CorrectTypeProperty); }
			}

			/// <summary>Special attributes</summary>
			/// <remarks>
			/// Japanese short name: "特殊属性", Google translated: "Special attributes".
			/// Japanese description: "武器の特殊属性値", Google translated: "Special attribute value of arms".
			/// </remarks>
			[ParameterTableRowAttribute("spAttribute", index: 75, minimum: 0, maximum: 255, step: 1, order: 1940, unknown2: 1)]
			[DisplayName("Special attributes")]
			[Description("Special attribute value of arms")]
			[DefaultValue((AttackParameterSpecialAttributes)0)]
			public AttackParameterSpecialAttributes SpAttribute {
				get { return spAttribute; }
				set { SetProperty(ref spAttribute, ref value, SpAttributeProperty); }
			}

			/// <summary>Special attack category</summary>
			/// <remarks>
			/// Japanese short name: "特殊攻撃カテゴリ", Google translated: "Special attack category".
			/// Japanese description: "特殊攻撃カテゴリ（50～199まで可能)", Google translated: "( Up to 50-199 ) special attack category".
			/// </remarks>
			[ParameterTableRowAttribute("spAtkcategory", index: 76, minimum: 0, maximum: 199, step: 1, order: 600, unknown2: 1)]
			[DisplayName("Special attack category")]
			[Description("( Up to 50-199 ) special attack category")]
			[DefaultValue((Byte)0)]
			public Byte SpAtkcategory {
				get { return spAtkcategory; }
				set {
					if ((double)value < 0 || (double)value > 199)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 199 for SpAtkcategory.");
					SetProperty(ref spAtkcategory, ref value, SpAtkcategoryProperty);
				}
			}

			/// <summary>Motion one hand weapon ID</summary>
			/// <remarks>
			/// Japanese short name: "武器モーション片手ID", Google translated: "Motion one hand weapon ID".
			/// Japanese description: "片手装備時の基本モーションID.", Google translated: "Basic Motion ID of one hand equipped.".
			/// </remarks>
			[ParameterTableRowAttribute("wepmotionOneHandId", index: 77, minimum: 0, maximum: 99, step: 1, order: 900, unknown2: 1)]
			[DisplayName("Motion one hand weapon ID")]
			[Description("Basic Motion ID of one hand equipped.")]
			[DefaultValue((Byte)0)]
			public Byte WepmotionOneHandId {
				get { return wepmotionOneHandId; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for WepmotionOneHandId.");
					SetProperty(ref wepmotionOneHandId, ref value, WepmotionOneHandIdProperty);
				}
			}

			/// <summary>Weapon motion both hands ID</summary>
			/// <remarks>
			/// Japanese short name: "武器モーション両手ID", Google translated: "Weapon motion both hands ID".
			/// Japanese description: "両手装備時の基本モーションID.", Google translated: "Basic Motion ID of both hands equipped.".
			/// </remarks>
			[ParameterTableRowAttribute("wepmotionBothHandId", index: 78, minimum: 0, maximum: 99, step: 1, order: 1000, unknown2: 1)]
			[DisplayName("Weapon motion both hands ID")]
			[Description("Basic Motion ID of both hands equipped.")]
			[DefaultValue((Byte)0)]
			public Byte WepmotionBothHandId {
				get { return wepmotionBothHandId; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for WepmotionBothHandId.");
					SetProperty(ref wepmotionBothHandId, ref value, WepmotionBothHandIdProperty);
				}
			}

			/// <summary>Equipped with the proper muscle strength</summary>
			/// <remarks>
			/// Japanese short name: "装備適正筋力", Google translated: "Equipped with the proper muscle strength".
			/// Japanese description: "装備適正値.", Google translated: "Equipped with the proper value .".
			/// </remarks>
			[ParameterTableRowAttribute("properStrength", index: 79, minimum: 0, maximum: 99, step: 1, order: 3700, unknown2: 1)]
			[DisplayName("Equipped with the proper muscle strength")]
			[Description("Equipped with the proper value .")]
			[DefaultValue((Byte)0)]
			public Byte ProperStrength {
				get { return properStrength; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for ProperStrength.");
					SetProperty(ref properStrength, ref value, ProperStrengthProperty);
				}
			}

			/// <summary>Equipped with proper agile</summary>
			/// <remarks>
			/// Japanese short name: "装備適正俊敏", Google translated: "Equipped with proper agile".
			/// Japanese description: "装備適正値.", Google translated: "Equipped with the proper value .".
			/// </remarks>
			[ParameterTableRowAttribute("properAgility", index: 80, minimum: 0, maximum: 99, step: 1, order: 3800, unknown2: 1)]
			[DisplayName("Equipped with proper agile")]
			[Description("Equipped with the proper value .")]
			[DefaultValue((Byte)0)]
			public Byte ProperAgility {
				get { return properAgility; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for ProperAgility.");
					SetProperty(ref properAgility, ref value, ProperAgilityProperty);
				}
			}

			/// <summary>Equipped with the proper magic</summary>
			/// <remarks>
			/// Japanese short name: "装備適正魔力", Google translated: "Equipped with the proper magic".
			/// Japanese description: "装備適正値.", Google translated: "Equipped with the proper value .".
			/// </remarks>
			[ParameterTableRowAttribute("properMagic", index: 81, minimum: 0, maximum: 99, step: 1, order: 3900, unknown2: 1)]
			[DisplayName("Equipped with the proper magic")]
			[Description("Equipped with the proper value .")]
			[DefaultValue((Byte)0)]
			public Byte ProperMagic {
				get { return properMagic; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for ProperMagic.");
					SetProperty(ref properMagic, ref value, ProperMagicProperty);
				}
			}

			/// <summary>Equipped with the proper faith</summary>
			/// <remarks>
			/// Japanese short name: "装備適正信仰", Google translated: "Equipped with the proper faith".
			/// Japanese description: "装備適正値.", Google translated: "Equipped with the proper value .".
			/// </remarks>
			[ParameterTableRowAttribute("properFaith", index: 82, minimum: 0, maximum: 99, step: 1, order: 4000, unknown2: 1)]
			[DisplayName("Equipped with the proper faith")]
			[Description("Equipped with the proper value .")]
			[DefaultValue((Byte)0)]
			public Byte ProperFaith {
				get { return properFaith; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for ProperFaith.");
					SetProperty(ref properFaith, ref value, ProperFaithProperty);
				}
			}

			/// <summary>Muscle strength over the starting value</summary>
			/// <remarks>
			/// Japanese short name: "筋力オーバー開始値", Google translated: "Muscle strength over the starting value".
			/// Japanese description: "筋力オーバー開始値", Google translated: "Muscle strength over the starting value".
			/// </remarks>
			[ParameterTableRowAttribute("overStrength", index: 83, minimum: 0, maximum: 99, step: 1, order: 4100, unknown2: 1)]
			[DisplayName("Muscle strength over the starting value")]
			[Description("Muscle strength over the starting value")]
			[DefaultValue((Byte)0)]
			public Byte OverStrength {
				get { return overStrength; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for OverStrength.");
					SetProperty(ref overStrength, ref value, OverStrengthProperty);
				}
			}

			/// <summary>Parry attack base value</summary>
			/// <remarks>
			/// Japanese short name: "パリィ攻撃基本値", Google translated: "Parry attack base value".
			/// Japanese description: "敵のパリィをやぶるための基本値", Google translated: "Base value to break the enemy Parry".
			/// </remarks>
			[ParameterTableRowAttribute("attackBaseParry", index: 84, minimum: 0, maximum: 99, step: 1, order: 4400, unknown2: 1)]
			[DisplayName("Parry attack base value")]
			[Description("Base value to break the enemy Parry")]
			[DefaultValue((Byte)0)]
			public Byte AttackBaseParry {
				get { return attackBaseParry; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for AttackBaseParry.");
					SetProperty(ref attackBaseParry, ref value, AttackBaseParryProperty);
				}
			}

			/// <summary>Parry Defense</summary>
			/// <remarks>
			/// Japanese short name: "パリィ防御値", Google translated: "Parry Defense".
			/// Japanese description: "パリィ判定時に、パリィになるかガードになるかの判定に利用", Google translated: "Parry in judgment time and used to determine whether a guard- or become Parry".
			/// </remarks>
			[ParameterTableRowAttribute("defenseBaseParry", index: 85, minimum: 0, maximum: 99, step: 1, order: 4500, unknown2: 1)]
			[DisplayName("Parry Defense")]
			[Description("Parry in judgment time and used to determine whether a guard- or become Parry")]
			[DefaultValue((Byte)0)]
			public Byte DefenseBaseParry {
				get { return defenseBaseParry; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for DefenseBaseParry.");
					SetProperty(ref defenseBaseParry, ref value, DefenseBaseParryProperty);
				}
			}

			/// <summary>Repelling defense force base value</summary>
			/// <remarks>
			/// Japanese short name: "はじき防御力基本値", Google translated: "Repelling defense force base value".
			/// Japanese description: "ガード敵を攻撃した時に、はじかれるかどうかの判定に利用", Google translated: "When attacked the enemy guard , used for determination as to whether repelled".
			/// </remarks>
			[ParameterTableRowAttribute("guardBaseRepel", index: 86, minimum: 0, maximum: 99, step: 1, order: 4600, unknown2: 1)]
			[DisplayName("Repelling defense force base value")]
			[Description("When attacked the enemy guard , used for determination as to whether repelled")]
			[DefaultValue((Byte)0)]
			public Byte GuardBaseRepel {
				get { return guardBaseRepel; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for GuardBaseRepel.");
					SetProperty(ref guardBaseRepel, ref value, GuardBaseRepelProperty);
				}
			}

			/// <summary>Repelling attack power base value</summary>
			/// <remarks>
			/// Japanese short name: "はじき攻撃力基本値", Google translated: "Repelling attack power base value".
			/// Japanese description: "敵の攻撃をガードしたときに、はじけるかどうかの判定に利用", Google translated: "When you guard the attack of the enemy , and used to determine as to whether or not split open".
			/// </remarks>
			[ParameterTableRowAttribute("attackBaseRepel", index: 87, minimum: 0, maximum: 99, step: 1, order: 4700, unknown2: 1)]
			[DisplayName("Repelling attack power base value")]
			[Description("When you guard the attack of the enemy , and used to determine as to whether or not split open")]
			[DefaultValue((Byte)0)]
			public Byte AttackBaseRepel {
				get { return attackBaseRepel; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for AttackBaseRepel.");
					SetProperty(ref attackBaseRepel, ref value, AttackBaseRepelProperty);
				}
			}

			/// <summary>Guard cut disabled magnification</summary>
			/// <remarks>
			/// Japanese short name: "ガードカット無効化倍率", Google translated: "Guard cut disabled magnification".
			/// Japanese description: "相手のガードカットを無効化させる倍率。-100で完全無効。100で相手の防御効果倍増。", Google translated: "Magnification to disable the guard cut the opponent . Completely disabled by -100 . Protective effect of double opponent at 100 .".
			/// </remarks>
			[ParameterTableRowAttribute("guardCutCancelRate", index: 88, minimum: -100, maximum: 100, step: 1, order: 5100, unknown2: 1)]
			[DisplayName("Guard cut disabled magnification")]
			[Description("Magnification to disable the guard cut the opponent . Completely disabled by -100 . Protective effect of double opponent at 100 .")]
			[DefaultValue((SByte)0)]
			public SByte GuardCutCancelRate {
				get { return guardCutCancelRate; }
				set {
					if ((double)value < -100 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -100 to 100 for GuardCutCancelRate.");
					SetProperty(ref guardCutCancelRate, ref value, GuardCutCancelRateProperty);
				}
			}

			/// <summary>Guard level</summary>
			/// <remarks>
			/// Japanese short name: "ガードレベル", Google translated: "Guard level".
			/// Japanese description: "ガードしたとき、敵の攻撃をどのガードモーションで受けるか？を決める", Google translated: "When you guard , you can either receive any motion guard enemy attacks ? I decide the".
			/// </remarks>
			[ParameterTableRowAttribute("guardLevel", index: 89, minimum: 0, maximum: 100, step: 1, order: 5300, unknown2: 1)]
			[DisplayName("Guard level")]
			[Description("When you guard , you can either receive any motion guard enemy attacks ? I decide the")]
			[DefaultValue((SByte)0)]
			public SByte GuardLevel {
				get { return guardLevel; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for GuardLevel.");
					SetProperty(ref guardLevel, ref value, GuardLevelProperty);
				}
			}

			/// <summary>Slashing attack rate cut</summary>
			/// <remarks>
			/// Japanese short name: "斬撃攻撃カット率", Google translated: "Slashing attack rate cut".
			/// Japanese description: "攻撃タイプを見て、斬撃属性のダメージを何％カットするか？を指定", Google translated: "Look at the type of attack , what percentage you can either cut the damage of撃属of Zan ? Specify".
			/// </remarks>
			[ParameterTableRowAttribute("slashGuardCutRate", index: 90, minimum: -100, maximum: 100, step: 1, order: 4910, unknown2: 1)]
			[DisplayName("Slashing attack rate cut")]
			[Description("Look at the type of attack , what percentage you can either cut the damage of撃属of Zan ? Specify")]
			[DefaultValue((SByte)0)]
			public SByte SlashGuardCutRate {
				get { return slashGuardCutRate; }
				set {
					if ((double)value < -100 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -100 to 100 for SlashGuardCutRate.");
					SetProperty(ref slashGuardCutRate, ref value, SlashGuardCutRateProperty);
				}
			}

			/// <summary>Blow attack rate cut</summary>
			/// <remarks>
			/// Japanese short name: "打撃攻撃カット率", Google translated: "Blow attack rate cut".
			/// Japanese description: "攻撃タイプを見て、打撃属性のダメージを何％カットするか？を指定", Google translated: "Look at the type of attack , what percentage you can either cut the damage of blow attribute ? Specify".
			/// </remarks>
			[ParameterTableRowAttribute("blowGuardCutRate", index: 91, minimum: -100, maximum: 100, step: 1, order: 4911, unknown2: 1)]
			[DisplayName("Blow attack rate cut")]
			[Description("Look at the type of attack , what percentage you can either cut the damage of blow attribute ? Specify")]
			[DefaultValue((SByte)0)]
			public SByte BlowGuardCutRate {
				get { return blowGuardCutRate; }
				set {
					if ((double)value < -100 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -100 to 100 for BlowGuardCutRate.");
					SetProperty(ref blowGuardCutRate, ref value, BlowGuardCutRateProperty);
				}
			}

			/// <summary>Piercing attack rate cut</summary>
			/// <remarks>
			/// Japanese short name: "刺突攻撃カット率", Google translated: "Piercing attack rate cut".
			/// Japanese description: "攻撃タイプを見て、刺突属性のダメージを何％カットするか？を指定", Google translated: "Look at the type of attack , what percentage you can either cut the damage of thorn突属efficient? Specify".
			/// </remarks>
			[ParameterTableRowAttribute("thrustGuardCutRate", index: 92, minimum: -100, maximum: 100, step: 1, order: 4912, unknown2: 1)]
			[DisplayName("Piercing attack rate cut")]
			[Description("Look at the type of attack , what percentage you can either cut the damage of thorn突属efficient? Specify")]
			[DefaultValue((SByte)0)]
			public SByte ThrustGuardCutRate {
				get { return thrustGuardCutRate; }
				set {
					if ((double)value < -100 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -100 to 100 for ThrustGuardCutRate.");
					SetProperty(ref thrustGuardCutRate, ref value, ThrustGuardCutRateProperty);
				}
			}

			/// <summary>Poison -resistant cut rate</summary>
			/// <remarks>
			/// Japanese short name: "毒耐性カット率", Google translated: "Poison -resistant cut rate".
			/// Japanese description: "毒にする攻撃力（特殊効果パラメータに設定）をどれだけカットするか", Google translated: "You can cut much (setting special effects parameters) attack force to poison".
			/// </remarks>
			[ParameterTableRowAttribute("poisonGuardResist", index: 93, minimum: 0, maximum: 100, step: 1, order: 7200, unknown2: 1)]
			[DisplayName("Poison -resistant cut rate")]
			[Description("You can cut much (setting special effects parameters) attack force to poison")]
			[DefaultValue((SByte)0)]
			public SByte PoisonGuardResist {
				get { return poisonGuardResist; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for PoisonGuardResist.");
					SetProperty(ref poisonGuardResist, ref value, PoisonGuardResistProperty);
				}
			}

			/// <summary>Epidemic attack rate cut</summary>
			/// <remarks>
			/// Japanese short name: "疫病攻撃カット率", Google translated: "Epidemic attack rate cut".
			/// Japanese description: "疫病にする攻撃力（特殊効果パラメータに設定）をどれだけカットするか", Google translated: "You can cut much (setting special effects parameters) attack force to plague".
			/// </remarks>
			[ParameterTableRowAttribute("diseaseGuardResist", index: 94, minimum: 0, maximum: 100, step: 1, order: 7300, unknown2: 1)]
			[DisplayName("Epidemic attack rate cut")]
			[Description("You can cut much (setting special effects parameters) attack force to plague")]
			[DefaultValue((SByte)0)]
			public SByte DiseaseGuardResist {
				get { return diseaseGuardResist; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for DiseaseGuardResist.");
					SetProperty(ref diseaseGuardResist, ref value, DiseaseGuardResistProperty);
				}
			}

			/// <summary>Bleeding attack rate cut</summary>
			/// <remarks>
			/// Japanese short name: "出血攻撃カット率", Google translated: "Bleeding attack rate cut".
			/// Japanese description: "出血にする攻撃力（特殊効果パラメータに設定）をどれだけカットするか", Google translated: "You can cut much (setting special effects parameters) attack force to bleeding".
			/// </remarks>
			[ParameterTableRowAttribute("bloodGuardResist", index: 95, minimum: 0, maximum: 100, step: 1, order: 7400, unknown2: 1)]
			[DisplayName("Bleeding attack rate cut")]
			[Description("You can cut much (setting special effects parameters) attack force to bleeding")]
			[DefaultValue((SByte)0)]
			public SByte BloodGuardResist {
				get { return bloodGuardResist; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for BloodGuardResist.");
					SetProperty(ref bloodGuardResist, ref value, BloodGuardResistProperty);
				}
			}

			/// <summary>Curse attack rate cut</summary>
			/// <remarks>
			/// Japanese short name: "呪攻撃カット率", Google translated: "Curse attack rate cut".
			/// Japanese description: "呪いにする攻撃力（特殊効果パラメータに設定）をどれだけカットするか", Google translated: "You can cut much (setting special effects parameters) attack force to curse".
			/// </remarks>
			[ParameterTableRowAttribute("curseGuardResist", index: 96, minimum: 0, maximum: 100, step: 1, order: 7500, unknown2: 1)]
			[DisplayName("Curse attack rate cut")]
			[Description("You can cut much (setting special effects parameters) attack force to curse")]
			[DefaultValue((SByte)0)]
			public SByte CurseGuardResist {
				get { return curseGuardResist; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for CurseGuardResist.");
					SetProperty(ref curseGuardResist, ref value, CurseGuardResistProperty);
				}
			}

			/// <summary>You can branch in durability</summary>
			/// <remarks>
			/// Japanese short name: "耐久度で分岐するか", Google translated: "You can branch in durability".
			/// Japanese description: "魔法使用武器対応：耐久度によるモーション分岐", Google translated: "The motion by branching Durability : use magic weapon support".
			/// </remarks>
			[ParameterTableRowAttribute("isDurabilityDivergence", index: 97, minimum: 0, maximum: 10, step: 1, order: 1010, unknown2: 1)]
			[DisplayName("You can branch in durability")]
			[Description("The motion by branching Durability : use magic weapon support")]
			[DefaultValue((DurabilityDivergenceCategory)0)]
			public DurabilityDivergenceCategory IsDurabilityDivergence {
				get { return isDurabilityDivergence; }
				set { SetProperty(ref isDurabilityDivergence, ref value, IsDurabilityDivergenceProperty); }
			}

			/// <summary>Right equipment</summary>
			/// <remarks>
			/// Japanese short name: "右手装備", Google translated: "Right equipment".
			/// Japanese description: "右手装備可能か.", Google translated: "Or right hand can be equipped .".
			/// </remarks>
			[ParameterTableRowAttribute("rightHandEquipable:1", index: 98, minimum: 0, maximum: 1, step: 1, order: 1100, unknown2: 1)]
			[DisplayName("Right equipment")]
			[Description("Or right hand can be equipped .")]
			[DefaultValue(true)]
			public Boolean RightHandEquipable {
				get { return GetBitProperty(0, 1, RightHandEquipableProperty) != 0; }
				set { SetBitProperty(0, 1, value ? 1 : 0, RightHandEquipableProperty); }
			}

			/// <summary>Left hand equipment</summary>
			/// <remarks>
			/// Japanese short name: "左手装備", Google translated: "Left hand equipment".
			/// Japanese description: "左手装備可能か.", Google translated: "Or left hand can be equipped .".
			/// </remarks>
			[ParameterTableRowAttribute("leftHandEquipable:1", index: 99, minimum: 0, maximum: 1, step: 1, order: 1200, unknown2: 1)]
			[DisplayName("Left hand equipment")]
			[Description("Or left hand can be equipped .")]
			[DefaultValue(true)]
			public Boolean LeftHandEquipable {
				get { return GetBitProperty(1, 1, LeftHandEquipableProperty) != 0; }
				set { SetBitProperty(1, 1, value ? 1 : 0, LeftHandEquipableProperty); }
			}

			/// <summary>Both hands equipment</summary>
			/// <remarks>
			/// Japanese short name: "両手装備", Google translated: "Both hands equipment".
			/// Japanese description: "両手装備可能か.", Google translated: "Or can be equipped with both hands .".
			/// </remarks>
			[ParameterTableRowAttribute("bothHandEquipable:1", index: 100, minimum: 0, maximum: 1, step: 1, order: 1300, unknown2: 1)]
			[DisplayName("Both hands equipment")]
			[Description("Or can be equipped with both hands .")]
			[DefaultValue(true)]
			public Boolean BothHandEquipable {
				get { return GetBitProperty(2, 1, BothHandEquipableProperty) != 0; }
				set { SetBitProperty(2, 1, value ? 1 : 0, BothHandEquipableProperty); }
			}

			/// <summary>Bow and arrow bullet equipment</summary>
			/// <remarks>
			/// Japanese short name: "弓矢弾装備", Google translated: "Bow and arrow bullet equipment".
			/// Japanese description: "弓用矢弾装備可能か.", Google translated: "Or bow for arrow bullets can be equipped .".
			/// </remarks>
			[ParameterTableRowAttribute("arrowSlotEquipable:1", index: 101, minimum: 0, maximum: 1, step: 1, order: 1400, unknown2: 1)]
			[DisplayName("Bow and arrow bullet equipment")]
			[Description("Or bow for arrow bullets can be equipped .")]
			[DefaultValue(false)]
			public Boolean ArrowSlotEquipable {
				get { return GetBitProperty(3, 1, ArrowSlotEquipableProperty) != 0; }
				set { SetBitProperty(3, 1, value ? 1 : 0, ArrowSlotEquipableProperty); }
			}

			/// <summary>Ishiyumiya bullets equipment</summary>
			/// <remarks>
			/// Japanese short name: "弩矢弾装備", Google translated: "Ishiyumiya bullets equipment".
			/// Japanese description: "弩用矢弾装備可能か.", Google translated: "Or for Crossbow arrow bullets can be equipped .".
			/// </remarks>
			[ParameterTableRowAttribute("boltSlotEquipable:1", index: 102, minimum: 0, maximum: 1, step: 1, order: 1500, unknown2: 1)]
			[DisplayName("Ishiyumiya bullets equipment")]
			[Description("Or for Crossbow arrow bullets can be equipped .")]
			[DefaultValue(false)]
			public Boolean BoltSlotEquipable {
				get { return GetBitProperty(4, 1, BoltSlotEquipableProperty) != 0; }
				set { SetBitProperty(4, 1, value ? 1 : 0, BoltSlotEquipableProperty); }
			}

			/// <summary>Guard possible</summary>
			/// <remarks>
			/// Japanese short name: "ガード可能", Google translated: "Guard possible".
			/// Japanese description: "左手装備時L1でガード", Google translated: "Guard by L1 left hand when equipped".
			/// </remarks>
			[ParameterTableRowAttribute("enableGuard:1", index: 103, minimum: 0, maximum: 1, step: 1, order: 1600, unknown2: 1)]
			[DisplayName("Guard possible")]
			[Description("Guard by L1 left hand when equipped")]
			[DefaultValue(false)]
			public Boolean EnableGuard {
				get { return GetBitProperty(5, 1, EnableGuardProperty) != 0; }
				set { SetBitProperty(5, 1, value ? 1 : 0, EnableGuardProperty); }
			}

			/// <summary>Parry possible</summary>
			/// <remarks>
			/// Japanese short name: "パリィ可能", Google translated: "Parry possible".
			/// Japanese description: "左手装備時L2でパリィ", Google translated: "Parry in L2 left hand when equipped".
			/// </remarks>
			[ParameterTableRowAttribute("enableParry:1", index: 104, minimum: 0, maximum: 1, step: 1, order: 1700, unknown2: 1)]
			[DisplayName("Parry possible")]
			[Description("Parry in L2 left hand when equipped")]
			[DefaultValue(false)]
			public Boolean EnableParry {
				get { return GetBitProperty(6, 1, EnableParryProperty) != 0; }
				set { SetBitProperty(6, 1, value ? 1 : 0, EnableParryProperty); }
			}

			/// <summary>Magic possible</summary>
			/// <remarks>
			/// Japanese short name: "魔法可能", Google translated: "Magic possible".
			/// Japanese description: "攻撃時に魔法発動", Google translated: "The magic is activated at the time of the attack".
			/// </remarks>
			[ParameterTableRowAttribute("enableMagic:1", index: 105, minimum: 0, maximum: 1, step: 1, order: 1800, unknown2: 1)]
			[DisplayName("Magic possible")]
			[Description("The magic is activated at the time of the attack")]
			[DefaultValue(false)]
			public Boolean EnableMagic {
				get { return GetBitProperty(7, 1, EnableMagicProperty) != 0; }
				set { SetBitProperty(7, 1, value ? 1 : 0, EnableMagicProperty); }
			}

			/// <summary>Magic possible</summary>
			/// <remarks>
			/// Japanese short name: "呪術可能", Google translated: "Magic possible".
			/// Japanese description: "攻撃時に呪術発動", Google translated: "The magic activated at the time of the attack".
			/// </remarks>
			[ParameterTableRowAttribute("enableSorcery:1", index: 106, minimum: 0, maximum: 1, step: 1, order: 1810, unknown2: 1)]
			[DisplayName("Magic possible")]
			[Description("The magic activated at the time of the attack")]
			[DefaultValue(false)]
			public Boolean EnableSorcery {
				get { return GetBitProperty(8, 1, EnableSorceryProperty) != 0; }
				set { SetBitProperty(8, 1, value ? 1 : 0, EnableSorceryProperty); }
			}

			/// <summary>Miracle possible</summary>
			/// <remarks>
			/// Japanese short name: "奇蹟可能", Google translated: "Miracle possible".
			/// Japanese description: "攻撃時に奇蹟発動", Google translated: "The miracle activated at the time of the attack".
			/// </remarks>
			[ParameterTableRowAttribute("enableMiracle:1", index: 107, minimum: 0, maximum: 1, step: 1, order: 1900, unknown2: 1)]
			[DisplayName("Miracle possible")]
			[Description("The miracle activated at the time of the attack")]
			[DefaultValue(false)]
			public Boolean EnableMiracle {
				get { return GetBitProperty(9, 1, EnableMiracleProperty) != 0; }
				set { SetBitProperty(9, 1, value ? 1 : 0, EnableMiracleProperty); }
			}

			/// <summary>Pledge magic possible</summary>
			/// <remarks>
			/// Japanese short name: "誓約魔法可能", Google translated: "Pledge magic possible".
			/// Japanese description: "攻撃時に誓約魔法発動", Google translated: "The pledge magic is activated at the time of the attack".
			/// </remarks>
			[ParameterTableRowAttribute("enableVowMagic:1", index: 108, minimum: 0, maximum: 1, step: 1, order: 1910, unknown2: 1)]
			[DisplayName("Pledge magic possible")]
			[Description("The pledge magic is activated at the time of the attack")]
			[DefaultValue(false)]
			public Boolean EnableVowMagic {
				get { return GetBitProperty(10, 1, EnableVowMagicProperty) != 0; }
				set { SetBitProperty(10, 1, value ? 1 : 0, EnableVowMagicProperty); }
			}

			/// <summary>Usually</summary>
			/// <remarks>
			/// Japanese short name: "通常", Google translated: "Usually".
			/// Japanese description: "メニュー表示用攻撃タイプ。通常か", Google translated: "Menu display type attack . Normally the".
			/// </remarks>
			[ParameterTableRowAttribute("isNormalAttackType:1", index: 109, minimum: 0, maximum: 1, step: 1, order: 2600, unknown2: 1)]
			[DisplayName("Usually")]
			[Description("Menu display type attack . Normally the")]
			[DefaultValue(false)]
			public Boolean IsNormalAttackType {
				get { return GetBitProperty(11, 1, IsNormalAttackTypeProperty) != 0; }
				set { SetBitProperty(11, 1, value ? 1 : 0, IsNormalAttackTypeProperty); }
			}

			/// <summary>Blow</summary>
			/// <remarks>
			/// Japanese short name: "打撃", Google translated: "Blow".
			/// Japanese description: "メニュー表示用攻撃タイプ。打撃か", Google translated: "Menu display type attack . Stroke or".
			/// </remarks>
			[ParameterTableRowAttribute("isBlowAttackType:1", index: 110, minimum: 0, maximum: 1, step: 1, order: 2700, unknown2: 1)]
			[DisplayName("Blow")]
			[Description("Menu display type attack . Stroke or")]
			[DefaultValue(false)]
			public Boolean IsBlowAttackType {
				get { return GetBitProperty(12, 1, IsBlowAttackTypeProperty) != 0; }
				set { SetBitProperty(12, 1, value ? 1 : 0, IsBlowAttackTypeProperty); }
			}

			/// <summary>Slashing</summary>
			/// <remarks>
			/// Japanese short name: "斬撃", Google translated: "Slashing".
			/// Japanese description: "メニュー表示用攻撃タイプ。斬撃か", Google translated: "Menu display type attack . Or slashing".
			/// </remarks>
			[ParameterTableRowAttribute("isSlashAttackType:1", index: 111, minimum: 0, maximum: 1, step: 1, order: 2800, unknown2: 1)]
			[DisplayName("Slashing")]
			[Description("Menu display type attack . Or slashing")]
			[DefaultValue(false)]
			public Boolean IsSlashAttackType {
				get { return GetBitProperty(13, 1, IsSlashAttackTypeProperty) != 0; }
				set { SetBitProperty(13, 1, value ? 1 : 0, IsSlashAttackTypeProperty); }
			}

			/// <summary>Piercing</summary>
			/// <remarks>
			/// Japanese short name: "刺突", Google translated: "Piercing".
			/// Japanese description: "メニュー表示用攻撃タイプ。刺突か", Google translated: "Menu display type attack . The thorn poked".
			/// </remarks>
			[ParameterTableRowAttribute("isThrustAttackType:1", index: 112, minimum: 0, maximum: 1, step: 1, order: 2900, unknown2: 1)]
			[DisplayName("Piercing")]
			[Description("Menu display type attack . The thorn poked")]
			[DefaultValue(false)]
			public Boolean IsThrustAttackType {
				get { return GetBitProperty(14, 1, IsThrustAttackTypeProperty) != 0; }
				set { SetBitProperty(14, 1, value ? 1 : 0, IsThrustAttackTypeProperty); }
			}

			/// <summary>Enchantment possible?</summary>
			/// <remarks>
			/// Japanese short name: "エンチャント可能か？", Google translated: "Enchantment possible?".
			/// Japanese description: "松脂などで、強化可能か？", Google translated: ", Such as pine resin , or strengthening possible?".
			/// </remarks>
			[ParameterTableRowAttribute("isEnhance:1", index: 113, minimum: 0, maximum: 1, step: 1, order: 3600, unknown2: 1)]
			[DisplayName("Enchantment possible?")]
			[Description(", Such as pine resin , or strengthening possible?")]
			[DefaultValue(true)]
			public Boolean IsEnhance {
				get { return GetBitProperty(15, 1, IsEnhanceProperty) != 0; }
				set { SetBitProperty(15, 1, value ? 1 : 0, IsEnhanceProperty); }
			}

			/// <summary>Is luck correction</summary>
			/// <remarks>
			/// Japanese short name: "運補正あるか", Google translated: "Is luck correction".
			/// Japanese description: "運による攻撃力補正があるか", Google translated: "Whether there is any offensive power correction by luck".
			/// </remarks>
			[ParameterTableRowAttribute("isLuckCorrect:1", index: 114, minimum: 0, maximum: 1, step: 1, order: 5000, unknown2: 1)]
			[DisplayName("Is luck correction")]
			[Description("Whether there is any offensive power correction by luck")]
			[DefaultValue(false)]
			public Boolean IsLuckCorrect {
				get { return GetBitProperty(16, 1, IsLuckCorrectProperty) != 0; }
				set { SetBitProperty(16, 1, value ? 1 : 0, IsLuckCorrectProperty); }
			}

			/// <summary>Or be enhanced ?</summary>
			/// <remarks>
			/// Japanese short name: "強化できるか？", Google translated: "Or be enhanced ?".
			/// Japanese description: "強化ショップで強化対象リストに並ぶ(仕様変更で削除するかも？)", Google translated: "( It might be deleted in the specification change ?) Line up to strengthen target list with the strengthening shop".
			/// </remarks>
			[ParameterTableRowAttribute("isCustom:1", index: 115, minimum: 0, maximum: 1, step: 1, order: 9200, unknown2: 1)]
			[DisplayName("Or be enhanced ?")]
			[Description("( It might be deleted in the specification change ?) Line up to strengthen target list with the strengthening shop")]
			[DefaultValue(true)]
			public Boolean IsCustom {
				get { return GetBitProperty(17, 1, IsCustomProperty) != 0; }
				set { SetBitProperty(17, 1, value ? 1 : 0, IsCustomProperty); }
			}

			/// <summary>Career change or reset disabled</summary>
			/// <remarks>
			/// Japanese short name: "転職リセット禁止か", Google translated: "Career change or reset disabled".
			/// Japanese description: "転職リセット禁止か", Google translated: "Career change or reset disabled".
			/// </remarks>
			[ParameterTableRowAttribute("disableBaseChangeReset:1", index: 116, minimum: 0, maximum: 1, step: 1, order: 9220, unknown2: 1)]
			[DisplayName("Career change or reset disabled")]
			[Description("Career change or reset disabled")]
			[DefaultValue(false)]
			public Boolean DisableBaseChangeReset {
				get { return GetBitProperty(18, 1, DisableBaseChangeResetProperty) != 0; }
				set { SetBitProperty(18, 1, value ? 1 : 0, DisableBaseChangeResetProperty); }
			}

			/// <summary>Repair or ban</summary>
			/// <remarks>
			/// Japanese short name: "修理禁止か", Google translated: "Repair or ban".
			/// Japanese description: "修理禁止か", Google translated: "Repair or ban".
			/// </remarks>
			[ParameterTableRowAttribute("disableRepair:1", index: 117, minimum: 0, maximum: 1, step: 1, order: 9240, unknown2: 1)]
			[DisplayName("Repair or ban")]
			[Description("Repair or ban")]
			[DefaultValue(false)]
			public Boolean DisableRepair {
				get { return GetBitProperty(19, 1, DisableRepairProperty) != 0; }
				set { SetBitProperty(19, 1, value ? 1 : 0, DisableRepairProperty); }
			}

			/// <summary>Or Dark Hand</summary>
			/// <remarks>
			/// Japanese short name: "ダークハンドか", Google translated: "Or Dark Hand".
			/// Japanese description: "ダークハンドか", Google translated: "Or Dark Hand".
			/// </remarks>
			[ParameterTableRowAttribute("isDarkHand:1", index: 118, minimum: 0, maximum: 1, step: 1, order: 1950, unknown2: 1)]
			[DisplayName("Or Dark Hand")]
			[Description("Or Dark Hand")]
			[DefaultValue(false)]
			public Boolean IsDarkHand {
				get { return GetBitProperty(20, 1, IsDarkHandProperty) != 0; }
				set { SetBitProperty(20, 1, value ? 1 : 0, IsDarkHandProperty); }
			}

			/// <summary>Or have DLC for simple model</summary>
			/// <remarks>
			/// Japanese short name: "DLC用シンプルモデルありか", Google translated: "Or have DLC for simple model".
			/// Japanese description: "ＤＬＣ用シンプルモデルが存在しているか", Google translated: "DLC for simple model is whether there".
			/// </remarks>
			[ParameterTableRowAttribute("simpleModelForDlc:1", index: 119, minimum: 0, maximum: 1, step: 1, order: 1970, unknown2: 1)]
			[DisplayName("Or have DLC for simple model")]
			[Description("DLC for simple model is whether there")]
			[DefaultValue(false)]
			public Boolean SimpleModelForDlc {
				get { return GetBitProperty(21, 1, SimpleModelForDlcProperty) != 0; }
				set { SetBitProperty(21, 1, value ? 1 : 0, SimpleModelForDlcProperty); }
			}

			/// <summary>Lantern weapon</summary>
			/// <remarks>
			/// Japanese short name: "ランタン武器", Google translated: "Lantern weapon".
			/// Japanese description: "ランタン武器か", Google translated: "Or lantern weapon".
			/// </remarks>
			[ParameterTableRowAttribute("lanternWep:1", index: 120, minimum: 0, maximum: 1, step: 1, order: 1920, unknown2: 1)]
			[DisplayName("Lantern weapon")]
			[Description("Or lantern weapon")]
			[DefaultValue(false)]
			public Boolean LanternWep {
				get { return GetBitProperty(22, 1, LanternWepProperty) != 0; }
				set { SetBitProperty(22, 1, value ? 1 : 0, LanternWepProperty); }
			}

			/// <summary>Tairei weapon</summary>
			/// <remarks>
			/// Japanese short name: "対霊武器", Google translated: "Tairei weapon".
			/// Japanese description: "対霊武器か", Google translated: "Or Tairei weapon".
			/// </remarks>
			[ParameterTableRowAttribute("isVersusGhostWep:1", index: 121, minimum: 0, maximum: 1, step: 1, order: 1930, unknown2: 1)]
			[DisplayName("Tairei weapon")]
			[Description("Or Tairei weapon")]
			[DefaultValue(false)]
			public Boolean IsVersusGhostWep {
				get { return GetBitProperty(23, 1, IsVersusGhostWepProperty) != 0; }
				set { SetBitProperty(23, 1, value ? 1 : 0, IsVersusGhostWepProperty); }
			}

			/// <summary>Career change weapon category</summary>
			/// <remarks>
			/// Japanese short name: "武器転職カテゴリ", Google translated: "Career change weapon category".
			/// Japanese description: "武器転職カテゴリ", Google translated: "Career change weapon category".
			/// </remarks>
			[ParameterTableRowAttribute("baseChangeCategory:6", index: 122, minimum: 0, maximum: 55, step: 1, order: 8550, unknown2: 1)]
			[DisplayName("Career change weapon category")]
			[Description("Career change weapon category")]
			[DefaultValue((WeaponBaseChangeCategory)0)]
			public WeaponBaseChangeCategory BaseChangeCategory {
				get { return (WeaponBaseChangeCategory)GetBitProperty(24, 6, BaseChangeCategoryProperty); }
				set { SetBitProperty(24, 6, (int)value, BaseChangeCategoryProperty); }
			}

			/// <summary>Or dragon hunting</summary>
			/// <remarks>
			/// Japanese short name: "竜狩りか", Google translated: "Or dragon hunting".
			/// Japanese description: "竜狩り武器か", Google translated: "Or dragon hunting weapon".
			/// </remarks>
			[ParameterTableRowAttribute("isDragonSlayer:1", index: 123, minimum: 0, maximum: 1, step: 1, order: 1940, unknown2: 1)]
			[DisplayName("Or dragon hunting")]
			[Description("Or dragon hunting weapon")]
			[DefaultValue(false)]
			public Boolean IsDragonSlayer {
				get { return GetBitProperty(30, 1, IsDragonSlayerProperty) != 0; }
				set { SetBitProperty(30, 1, value ? 1 : 0, IsDragonSlayerProperty); }
			}

			/// <summary>Or multi-drop sharing ban</summary>
			/// <remarks>
			/// Japanese short name: "マルチドロップ共有禁止か", Google translated: "Or multi-drop sharing ban".
			/// Japanese description: "マルチドロップ共有禁止か", Google translated: "Or multi-drop sharing ban".
			/// </remarks>
			[ParameterTableRowAttribute("disableMultiDropShare:1", index: 125, minimum: 0, maximum: 1, step: 1, order: 1965, unknown2: 1)]
			[DisplayName("Or multi-drop sharing ban")]
			[Description("Or multi-drop sharing ban")]
			[DefaultValue(false)]
			public Boolean DisableMultiDropShare {
				get { return GetBitProperty(32, 1, DisableMultiDropShareProperty) != 0; }
				set { SetBitProperty(32, 1, value ? 1 : 0, DisableMultiDropShareProperty); }
			}

			/// <summary>Padding</summary>
			/// <remarks>
			/// Japanese short name: "パディング", Google translated: "Padding".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("pad_0[1]", index: 126, minimum: 0, maximum: 1, step: 1, order: 9241, unknown2: 1)]
			[DisplayName("Padding")]
			[Description("")]
			[Browsable(false)]
			public Byte[] Pad_0 {
				get { return pad_0; }
				set { SetProperty(ref pad_0, ref value, Pad_0Property); }
			}

			/// <summary>Padding</summary>
			/// <remarks>
			/// Japanese short name: "パディング", Google translated: "Padding".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("pad_1[8]", index: 128, minimum: 0, maximum: 1, step: 1, order: 9242, unknown2: 1)]
			[DisplayName("Padding")]
			[Description("")]
			[Browsable(false)]
			public Byte[] Pad_1 {
				get { return pad_1; }
				set { SetProperty(ref pad_1, ref value, Pad_1Property); }
			}

			public string EnglishName { get { return GetLocalisedName(Language.English); } }

			public string EnglishDescription { get { return GetLocalisedDescription(Language.English).Trim(); } }

			public string EnglishType { get { return GetLocalisedType(Language.English); } }

			internal Weapon(ParameterTable table, int index, AssetLoader loader, int next)
				: base(table, index, loader) {
				BinaryReader reader = loader.Reader;

				BehaviorVariationId = reader.ReadInt32();
				SortId = reader.ReadInt32();
				WanderingEquipId = reader.ReadInt32();
				Weight = reader.ReadSingle();
				WeaponWeightRate = reader.ReadSingle();
				FixPrice = reader.ReadInt32();
				BasicPrice = reader.ReadInt32();
				SellValue = reader.ReadInt32();
				CorrectStrength = reader.ReadSingle();
				CorrectAgility = reader.ReadSingle();
				CorrectMagic = reader.ReadSingle();
				CorrectFaith = reader.ReadSingle();
				PhysGuardCutRate = reader.ReadSingle();
				MagGuardCutRate = reader.ReadSingle();
				FireGuardCutRate = reader.ReadSingle();
				ThunGuardCutRate = reader.ReadSingle();
				SpEffectBehaviorId0 = reader.ReadInt32();
				SpEffectBehaviorId1 = reader.ReadInt32();
				SpEffectBehaviorId2 = reader.ReadInt32();
				ResidentSpEffectId = reader.ReadInt32();
				ResidentSpEffectId1 = reader.ReadInt32();
				ResidentSpEffectId2 = reader.ReadInt32();
				MaterialSetId = reader.ReadInt32();
				OriginEquipWep = reader.ReadInt32();
				OriginEquipWep1 = reader.ReadInt32();
				OriginEquipWep2 = reader.ReadInt32();
				OriginEquipWep3 = reader.ReadInt32();
				OriginEquipWep4 = reader.ReadInt32();
				OriginEquipWep5 = reader.ReadInt32();
				OriginEquipWep6 = reader.ReadInt32();
				OriginEquipWep7 = reader.ReadInt32();
				OriginEquipWep8 = reader.ReadInt32();
				OriginEquipWep9 = reader.ReadInt32();
				OriginEquipWep10 = reader.ReadInt32();
				OriginEquipWep11 = reader.ReadInt32();
				OriginEquipWep12 = reader.ReadInt32();
				OriginEquipWep13 = reader.ReadInt32();
				OriginEquipWep14 = reader.ReadInt32();
				OriginEquipWep15 = reader.ReadInt32();
				AntiDemonDamageRate = reader.ReadSingle();
				AntSaintDamageRate = reader.ReadSingle();
				AntWeakA_DamageRate = reader.ReadSingle();
				AntWeakB_DamageRate = reader.ReadSingle();
				VagrantItemLotId = reader.ReadInt32();
				VagrantBonusEneDropItemLotId = reader.ReadInt32();
				VagrantItemEneDropItemLotId = reader.ReadInt32();
				EquipModelId = reader.ReadUInt16();
				IconId = reader.ReadUInt16();
				Durability = reader.ReadUInt16();
				DurabilityMax = reader.ReadUInt16();
				AttackThrowEscape = reader.ReadUInt16();
				ParryDamageLife = reader.ReadInt16();
				AttackBasePhysics = reader.ReadUInt16();
				AttackBaseMagic = reader.ReadUInt16();
				AttackBaseFire = reader.ReadUInt16();
				AttackBaseThunder = reader.ReadUInt16();
				AttackBaseStamina = reader.ReadUInt16();
				SaWeaponDamage = reader.ReadUInt16();
				SaDurability = reader.ReadInt16();
				GuardAngle = reader.ReadInt16();
				StaminaGuardDef = reader.ReadInt16();
				ReinforceTypeId = reader.ReadInt16();
				TrophySGradeId = reader.ReadInt16();
				TrophySeqId = reader.ReadInt16();
				ThrowAtkRate = reader.ReadInt16();
				BowDistRate = reader.ReadInt16();
				EquipModelCategory = (EquipModelCategory)reader.ReadByte();
				EquipModelGender = (EquipModelGender)reader.ReadByte();
				WeaponCategory = (WeaponCategory)reader.ReadByte();
				MotionCategory = (WeaponMotionCategory)reader.ReadByte();
				GuardmotionCategory = (GuardMotionCategory)reader.ReadByte();
				AtkMaterial = (WeaponMaterialAttack)reader.ReadByte();
				DefMaterial = (WeaponMaterialDefend)reader.ReadByte();
				DefSfxMaterial = (WeaponMaterialDefendSound)reader.ReadByte();
				CorrectType = (WeaponCorrectType)reader.ReadByte();
				SpAttribute = (AttackParameterSpecialAttributes)reader.ReadByte();
				SpAtkcategory = reader.ReadByte();
				WepmotionOneHandId = reader.ReadByte();
				WepmotionBothHandId = reader.ReadByte();
				ProperStrength = reader.ReadByte();
				ProperAgility = reader.ReadByte();
				ProperMagic = reader.ReadByte();
				ProperFaith = reader.ReadByte();
				OverStrength = reader.ReadByte();
				AttackBaseParry = reader.ReadByte();
				DefenseBaseParry = reader.ReadByte();
				GuardBaseRepel = reader.ReadByte();
				AttackBaseRepel = reader.ReadByte();
				GuardCutCancelRate = reader.ReadSByte();
				GuardLevel = reader.ReadSByte();
				SlashGuardCutRate = reader.ReadSByte();
				BlowGuardCutRate = reader.ReadSByte();
				ThrustGuardCutRate = reader.ReadSByte();
				PoisonGuardResist = reader.ReadSByte();
				DiseaseGuardResist = reader.ReadSByte();
				BloodGuardResist = reader.ReadSByte();
				CurseGuardResist = reader.ReadSByte();
				IsDurabilityDivergence = (DurabilityDivergenceCategory)reader.ReadByte();
				BitFields = reader.ReadBytes(5);
				Pad_0 = reader.ReadBytes(1);
				OldSortId = reader.ReadInt16();
				Pad_1 = reader.ReadBytes(8);
			}

			internal Weapon(ParameterTable table, int index)
				: base(table, index) {
				BitFields = new byte[5];
				BehaviorVariationId = (Int32)0;
				WeaponWeightRate = (Single)0;
				CorrectStrength = (Single)0;
				CorrectAgility = (Single)0;
				CorrectMagic = (Single)0;
				CorrectFaith = (Single)0;
				PhysGuardCutRate = (Single)0;
				MagGuardCutRate = (Single)0;
				FireGuardCutRate = (Single)0;
				ThunGuardCutRate = (Single)0;
				SpEffectBehaviorId0 = (Int32)(-1);
				SpEffectBehaviorId1 = (Int32)(-1);
				SpEffectBehaviorId2 = (Int32)(-1);
				ResidentSpEffectId = (Int32)(-1);
				ResidentSpEffectId1 = (Int32)(-1);
				ResidentSpEffectId2 = (Int32)(-1);
				MaterialSetId = (Int32)(-1);
				OriginEquipWep = (Int32)(-1);
				OriginEquipWep1 = (Int32)(-1);
				OriginEquipWep2 = (Int32)(-1);
				OriginEquipWep3 = (Int32)(-1);
				OriginEquipWep4 = (Int32)(-1);
				OriginEquipWep5 = (Int32)(-1);
				OriginEquipWep6 = (Int32)(-1);
				OriginEquipWep7 = (Int32)(-1);
				OriginEquipWep8 = (Int32)(-1);
				OriginEquipWep9 = (Int32)(-1);
				OriginEquipWep10 = (Int32)(-1);
				OriginEquipWep11 = (Int32)(-1);
				OriginEquipWep12 = (Int32)(-1);
				OriginEquipWep13 = (Int32)(-1);
				OriginEquipWep14 = (Int32)(-1);
				OriginEquipWep15 = (Int32)(-1);
				AntiDemonDamageRate = (Single)1;
				AntSaintDamageRate = (Single)1;
				AntWeakA_DamageRate = (Single)1;
				AntWeakB_DamageRate = (Single)1;
				IconId = (UInt16)0;
				AttackThrowEscape = (UInt16)0;
				ParryDamageLife = (Int16)(-1);
				AttackBasePhysics = (UInt16)100;
				AttackBaseMagic = (UInt16)100;
				AttackBaseFire = (UInt16)100;
				AttackBaseThunder = (UInt16)100;
				AttackBaseStamina = (UInt16)100;
				SaWeaponDamage = (UInt16)0;
				SaDurability = (Int16)0;
				GuardAngle = (Int16)0;
				StaminaGuardDef = (Int16)0;
				ReinforceTypeId = (Int16)0;
				TrophySGradeId = (Int16)(-1);
				TrophySeqId = (Int16)(-1);
				ThrowAtkRate = (Int16)0;
				BowDistRate = (Int16)0;
				EquipModelCategory = (EquipModelCategory)7;
				EquipModelGender = (EquipModelGender)0;
				WeaponCategory = (WeaponCategory)0;
				MotionCategory = (WeaponMotionCategory)0;
				GuardmotionCategory = (GuardMotionCategory)0;
				AtkMaterial = (WeaponMaterialAttack)0;
				DefMaterial = (WeaponMaterialDefend)0;
				DefSfxMaterial = (WeaponMaterialDefendSound)0;
				CorrectType = (WeaponCorrectType)0;
				SpAttribute = (AttackParameterSpecialAttributes)0;
				SpAtkcategory = (Byte)0;
				WepmotionOneHandId = (Byte)0;
				WepmotionBothHandId = (Byte)0;
				ProperStrength = (Byte)0;
				ProperAgility = (Byte)0;
				ProperMagic = (Byte)0;
				ProperFaith = (Byte)0;
				OverStrength = (Byte)0;
				AttackBaseParry = (Byte)0;
				DefenseBaseParry = (Byte)0;
				GuardBaseRepel = (Byte)0;
				AttackBaseRepel = (Byte)0;
				GuardCutCancelRate = (SByte)0;
				GuardLevel = (SByte)0;
				SlashGuardCutRate = (SByte)0;
				BlowGuardCutRate = (SByte)0;
				ThrustGuardCutRate = (SByte)0;
				PoisonGuardResist = (SByte)0;
				DiseaseGuardResist = (SByte)0;
				BloodGuardResist = (SByte)0;
				CurseGuardResist = (SByte)0;
				IsDurabilityDivergence = (DurabilityDivergenceCategory)0;
				RightHandEquipable = true;
				LeftHandEquipable = true;
				BothHandEquipable = true;
				ArrowSlotEquipable = false;
				BoltSlotEquipable = false;
				EnableGuard = false;
				EnableParry = false;
				EnableMagic = false;
				EnableSorcery = false;
				EnableMiracle = false;
				EnableVowMagic = false;
				IsNormalAttackType = false;
				IsBlowAttackType = false;
				IsSlashAttackType = false;
				IsThrustAttackType = false;
				IsEnhance = true;
				IsLuckCorrect = false;
				IsCustom = true;
				DisableBaseChangeReset = false;
				DisableRepair = false;
				IsDarkHand = false;
				SimpleModelForDlc = false;
				LanternWep = false;
				IsVersusGhostWep = false;
				BaseChangeCategory = (WeaponBaseChangeCategory)0;
				IsDragonSlayer = false;
				IsDeposit = true;
				DisableMultiDropShare = false;
				Pad_0 = new Byte[1];
				Pad_1 = new Byte[8];
			}

			public string GetLocalisedName(Language language = Language.English) { return GetLocalisedString(Engine.ItemArchiveId.WeaponNames, language); }
			public string GetLocalisedDescription(Language language = Language.English) { return GetLocalisedString(Engine.ItemArchiveId.WeaponDescriptions, language); }
			public string GetLocalisedType(Language language = Language.English) { return GetLocalisedString(Engine.ItemArchiveId.WeaponTypes, language); }

			public override void Write(BinaryWriter writer) {
				writer.Write(BehaviorVariationId);
				writer.Write(SortId);
				writer.Write(WanderingEquipId);
				writer.Write(Weight);
				writer.Write(WeaponWeightRate);
				writer.Write(FixPrice);
				writer.Write(BasicPrice);
				writer.Write(SellValue);
				writer.Write(CorrectStrength);
				writer.Write(CorrectAgility);
				writer.Write(CorrectMagic);
				writer.Write(CorrectFaith);
				writer.Write(PhysGuardCutRate);
				writer.Write(MagGuardCutRate);
				writer.Write(FireGuardCutRate);
				writer.Write(ThunGuardCutRate);
				writer.Write(SpEffectBehaviorId0);
				writer.Write(SpEffectBehaviorId1);
				writer.Write(SpEffectBehaviorId2);
				writer.Write(ResidentSpEffectId);
				writer.Write(ResidentSpEffectId1);
				writer.Write(ResidentSpEffectId2);
				writer.Write(MaterialSetId);
				writer.Write(OriginEquipWep);
				writer.Write(OriginEquipWep1);
				writer.Write(OriginEquipWep2);
				writer.Write(OriginEquipWep3);
				writer.Write(OriginEquipWep4);
				writer.Write(OriginEquipWep5);
				writer.Write(OriginEquipWep6);
				writer.Write(OriginEquipWep7);
				writer.Write(OriginEquipWep8);
				writer.Write(OriginEquipWep9);
				writer.Write(OriginEquipWep10);
				writer.Write(OriginEquipWep11);
				writer.Write(OriginEquipWep12);
				writer.Write(OriginEquipWep13);
				writer.Write(OriginEquipWep14);
				writer.Write(OriginEquipWep15);
				writer.Write(AntiDemonDamageRate);
				writer.Write(AntSaintDamageRate);
				writer.Write(AntWeakA_DamageRate);
				writer.Write(AntWeakB_DamageRate);
				writer.Write(VagrantItemLotId);
				writer.Write(VagrantBonusEneDropItemLotId);
				writer.Write(VagrantItemEneDropItemLotId);
				writer.Write(EquipModelId);
				writer.Write(IconId);
				writer.Write(Durability);
				writer.Write(DurabilityMax);
				writer.Write(AttackThrowEscape);
				writer.Write(ParryDamageLife);
				writer.Write(AttackBasePhysics);
				writer.Write(AttackBaseMagic);
				writer.Write(AttackBaseFire);
				writer.Write(AttackBaseThunder);
				writer.Write(AttackBaseStamina);
				writer.Write(SaWeaponDamage);
				writer.Write(SaDurability);
				writer.Write(GuardAngle);
				writer.Write(StaminaGuardDef);
				writer.Write(ReinforceTypeId);
				writer.Write(TrophySGradeId);
				writer.Write(TrophySeqId);
				writer.Write(ThrowAtkRate);
				writer.Write(BowDistRate);
				writer.Write((Byte)EquipModelCategory);
				writer.Write((Byte)EquipModelGender);
				writer.Write((Byte)WeaponCategory);
				writer.Write((Byte)MotionCategory);
				writer.Write((Byte)GuardmotionCategory);
				writer.Write((Byte)AtkMaterial);
				writer.Write((Byte)DefMaterial);
				writer.Write((Byte)DefSfxMaterial);
				writer.Write((Byte)CorrectType);
				writer.Write((Byte)SpAttribute);
				writer.Write(SpAtkcategory);
				writer.Write(WepmotionOneHandId);
				writer.Write(WepmotionBothHandId);
				writer.Write(ProperStrength);
				writer.Write(ProperAgility);
				writer.Write(ProperMagic);
				writer.Write(ProperFaith);
				writer.Write(OverStrength);
				writer.Write(AttackBaseParry);
				writer.Write(DefenseBaseParry);
				writer.Write(GuardBaseRepel);
				writer.Write(AttackBaseRepel);
				writer.Write(GuardCutCancelRate);
				writer.Write(GuardLevel);
				writer.Write(SlashGuardCutRate);
				writer.Write(BlowGuardCutRate);
				writer.Write(ThrustGuardCutRate);
				writer.Write(PoisonGuardResist);
				writer.Write(DiseaseGuardResist);
				writer.Write(BloodGuardResist);
				writer.Write(CurseGuardResist);
				writer.Write((Byte)IsDurabilityDivergence);
				writer.Write(BitFields);
				writer.Write(Pad_0);
				writer.Write(OldSortId);
				writer.Write(Pad_1);
			}
		}
	}
}
