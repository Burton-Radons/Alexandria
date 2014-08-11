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
		/// Defined as "REINFORCE_PARAM_WEAPON_ST" in Dark Souls in the file "ReinforceParamWeapon.paramdef" (id 10h).
		/// </remarks>
		public class WeaponReinforcement : ParameterTableRow {
			/// <summary>The name of the table in the file.</summary>
			public const string TableName = "REINFORCE_PARAM_WEAPON_ST";

			Single physicsAtkRate, magicAtkRate, fireAtkRate, thunderAtkRate, staminaAtkRate, saWeaponAtkRate, saDurabilityRate, correctStrengthRate, correctAgilityRate, correctMagicRate, correctFaithRate, physicsGuardCutRate, magicGuardCutRate, fireGuardCutRate, thunderGuardCutRate, poisonGuardResistRate, diseaseGuardResistRate, bloodGuardResistRate, curseGuardResistRate, staminaGuardDefRate;
			Byte spEffectId1, spEffectId2, spEffectId3, residentSpEffectId1, residentSpEffectId2, residentSpEffectId3, materialSetId;
			Byte[] pad;

			/// <summary>A property in the class.</summary>
			public static readonly PropertyInfo
				PhysicsAtkRateProperty = GetProperty<WeaponReinforcement>("PhysicsAtkRate"),
				MagicAtkRateProperty = GetProperty<WeaponReinforcement>("MagicAtkRate"),
				FireAtkRateProperty = GetProperty<WeaponReinforcement>("FireAtkRate"),
				ThunderAtkRateProperty = GetProperty<WeaponReinforcement>("ThunderAtkRate"),
				StaminaAtkRateProperty = GetProperty<WeaponReinforcement>("StaminaAtkRate"),
				SaWeaponAtkRateProperty = GetProperty<WeaponReinforcement>("SaWeaponAtkRate"),
				SaDurabilityRateProperty = GetProperty<WeaponReinforcement>("SaDurabilityRate"),
				CorrectStrengthRateProperty = GetProperty<WeaponReinforcement>("CorrectStrengthRate"),
				CorrectAgilityRateProperty = GetProperty<WeaponReinforcement>("CorrectAgilityRate"),
				CorrectMagicRateProperty = GetProperty<WeaponReinforcement>("CorrectMagicRate"),
				CorrectFaithRateProperty = GetProperty<WeaponReinforcement>("CorrectFaithRate"),
				PhysicsGuardCutRateProperty = GetProperty<WeaponReinforcement>("PhysicsGuardCutRate"),
				MagicGuardCutRateProperty = GetProperty<WeaponReinforcement>("MagicGuardCutRate"),
				FireGuardCutRateProperty = GetProperty<WeaponReinforcement>("FireGuardCutRate"),
				ThunderGuardCutRateProperty = GetProperty<WeaponReinforcement>("ThunderGuardCutRate"),
				PoisonGuardResistRateProperty = GetProperty<WeaponReinforcement>("PoisonGuardResistRate"),
				DiseaseGuardResistRateProperty = GetProperty<WeaponReinforcement>("DiseaseGuardResistRate"),
				BloodGuardResistRateProperty = GetProperty<WeaponReinforcement>("BloodGuardResistRate"),
				CurseGuardResistRateProperty = GetProperty<WeaponReinforcement>("CurseGuardResistRate"),
				StaminaGuardDefRateProperty = GetProperty<WeaponReinforcement>("StaminaGuardDefRate"),
				SpEffectId1Property = GetProperty<WeaponReinforcement>("SpEffectId1"),
				SpEffectId2Property = GetProperty<WeaponReinforcement>("SpEffectId2"),
				SpEffectId3Property = GetProperty<WeaponReinforcement>("SpEffectId3"),
				ResidentSpEffectId1Property = GetProperty<WeaponReinforcement>("ResidentSpEffectId1"),
				ResidentSpEffectId2Property = GetProperty<WeaponReinforcement>("ResidentSpEffectId2"),
				ResidentSpEffectId3Property = GetProperty<WeaponReinforcement>("ResidentSpEffectId3"),
				MaterialSetIdProperty = GetProperty<WeaponReinforcement>("MaterialSetId"),
				PadProperty = GetProperty<WeaponReinforcement>("Pad");

			/// <summary>Physical Attack base value</summary>
			/// <remarks>
			/// Japanese short name: "物理攻撃力基本値", Google translated: "Physical Attack base value".
			/// Japanese description: "物理攻撃力の補正値", Google translated: "Correction value of Physical Attack".
			/// </remarks>
			[ParameterTableRowAttribute("physicsAtkRate", index: 0, minimum: 0, maximum: 99.99, step: 0.01, sortOrder: 100, unknown2: 1)]
			[DisplayName("Physical Attack base value")]
			[Description("Correction value of Physical Attack")]
			[DefaultValue((Single)1)]
			public Single PhysicsAtkRate {
				get { return physicsAtkRate; }
				set {
					if ((double)value < 0 || (double)value > 99.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99.99 for " + PhysicsAtkRateProperty.Name + ".");
					SetProperty(ref physicsAtkRate, ref value, PhysicsAtkRateProperty);
				}
			}

			/// <summary>Magic Attack base value</summary>
			/// <remarks>
			/// Japanese short name: "魔法攻撃力基本値", Google translated: "Magic Attack base value".
			/// Japanese description: "魔法攻撃力の補正値", Google translated: "Correction value of Magic Attack".
			/// </remarks>
			[ParameterTableRowAttribute("magicAtkRate", index: 1, minimum: 0, maximum: 99.99, step: 0.01, sortOrder: 200, unknown2: 1)]
			[DisplayName("Magic Attack base value")]
			[Description("Correction value of Magic Attack")]
			[DefaultValue((Single)1)]
			public Single MagicAtkRate {
				get { return magicAtkRate; }
				set {
					if ((double)value < 0 || (double)value > 99.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99.99 for " + MagicAtkRateProperty.Name + ".");
					SetProperty(ref magicAtkRate, ref value, MagicAtkRateProperty);
				}
			}

			/// <summary>Flame attack power base value</summary>
			/// <remarks>
			/// Japanese short name: "炎攻撃力基本値", Google translated: "Flame attack power base value".
			/// Japanese description: "炎攻撃力の補正値", Google translated: "Correction value of flame attack power".
			/// </remarks>
			[ParameterTableRowAttribute("fireAtkRate", index: 2, minimum: 0, maximum: 99.99, step: 0.01, sortOrder: 300, unknown2: 1)]
			[DisplayName("Flame attack power base value")]
			[Description("Correction value of flame attack power")]
			[DefaultValue((Single)1)]
			public Single FireAtkRate {
				get { return fireAtkRate; }
				set {
					if ((double)value < 0 || (double)value > 99.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99.99 for " + FireAtkRateProperty.Name + ".");
					SetProperty(ref fireAtkRate, ref value, FireAtkRateProperty);
				}
			}

			/// <summary>Blitz attack power base value</summary>
			/// <remarks>
			/// Japanese short name: "電撃攻撃力基本値", Google translated: "Blitz attack power base value".
			/// Japanese description: "電撃攻撃力の補正値", Google translated: "Correction value of blitz attack power".
			/// </remarks>
			[ParameterTableRowAttribute("thunderAtkRate", index: 3, minimum: 0, maximum: 99.99, step: 0.01, sortOrder: 400, unknown2: 1)]
			[DisplayName("Blitz attack power base value")]
			[Description("Correction value of blitz attack power")]
			[DefaultValue((Single)1)]
			public Single ThunderAtkRate {
				get { return thunderAtkRate; }
				set {
					if ((double)value < 0 || (double)value > 99.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99.99 for " + ThunderAtkRateProperty.Name + ".");
					SetProperty(ref thunderAtkRate, ref value, ThunderAtkRateProperty);
				}
			}

			/// <summary>Stamina attack power</summary>
			/// <remarks>
			/// Japanese short name: "スタミナ攻撃力", Google translated: "Stamina attack power".
			/// Japanese description: "スタミナ攻撃力の補正値", Google translated: "Correction value of stamina attack power".
			/// </remarks>
			[ParameterTableRowAttribute("staminaAtkRate", index: 4, minimum: 0, maximum: 99.99, step: 0.01, sortOrder: 500, unknown2: 1)]
			[DisplayName("Stamina attack power")]
			[Description("Correction value of stamina attack power")]
			[DefaultValue((Single)1)]
			public Single StaminaAtkRate {
				get { return staminaAtkRate; }
				set {
					if ((double)value < 0 || (double)value > 99.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99.99 for " + StaminaAtkRateProperty.Name + ".");
					SetProperty(ref staminaAtkRate, ref value, StaminaAtkRateProperty);
				}
			}

			/// <summary>SA weapon attack force</summary>
			/// <remarks>
			/// Japanese short name: "SA武器攻撃力", Google translated: "SA weapon attack force".
			/// Japanese description: "スーパーアーマー武器攻撃色の補正値", Google translated: "Correction value of super- armor weapon attack color".
			/// </remarks>
			[ParameterTableRowAttribute("saWeaponAtkRate", index: 5, minimum: 0, maximum: 99.99, step: 0.01, sortOrder: 600, unknown2: 1)]
			[DisplayName("SA weapon attack force")]
			[Description("Correction value of super- armor weapon attack color")]
			[DefaultValue((Single)1)]
			public Single SaWeaponAtkRate {
				get { return saWeaponAtkRate; }
				set {
					if ((double)value < 0 || (double)value > 99.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99.99 for " + SaWeaponAtkRateProperty.Name + ".");
					SetProperty(ref saWeaponAtkRate, ref value, SaWeaponAtkRateProperty);
				}
			}

			/// <summary>SA Durability</summary>
			/// <remarks>
			/// Japanese short name: "SA耐久値", Google translated: "SA Durability".
			/// Japanese description: "SA耐久力の補正値", Google translated: "Correction value of SA endurance".
			/// </remarks>
			[ParameterTableRowAttribute("saDurabilityRate", index: 6, minimum: 0, maximum: 99.99, step: 0.01, sortOrder: 700, unknown2: 1)]
			[DisplayName("SA Durability")]
			[Description("Correction value of SA endurance")]
			[DefaultValue((Single)1)]
			public Single SaDurabilityRate {
				get { return saDurabilityRate; }
				set {
					if ((double)value < 0 || (double)value > 99.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99.99 for " + SaDurabilityRateProperty.Name + ".");
					SetProperty(ref saDurabilityRate, ref value, SaDurabilityRateProperty);
				}
			}

			/// <summary>Strength correction</summary>
			/// <remarks>
			/// Japanese short name: "筋力補正", Google translated: "Strength correction".
			/// Japanese description: "筋力補正の補正値", Google translated: "Correction value of muscle strength correction".
			/// </remarks>
			[ParameterTableRowAttribute("correctStrengthRate", index: 7, minimum: 0, maximum: 99.99, step: 0.01, sortOrder: 800, unknown2: 1)]
			[DisplayName("Strength correction")]
			[Description("Correction value of muscle strength correction")]
			[DefaultValue((Single)1)]
			public Single CorrectStrengthRate {
				get { return correctStrengthRate; }
				set {
					if ((double)value < 0 || (double)value > 99.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99.99 for " + CorrectStrengthRateProperty.Name + ".");
					SetProperty(ref correctStrengthRate, ref value, CorrectStrengthRateProperty);
				}
			}

			/// <summary>Agile correction</summary>
			/// <remarks>
			/// Japanese short name: "俊敏補正", Google translated: "Agile correction".
			/// Japanese description: "俊敏補正の補正値", Google translated: "Correction value of agile correction".
			/// </remarks>
			[ParameterTableRowAttribute("correctAgilityRate", index: 8, minimum: 0, maximum: 99.99, step: 0.01, sortOrder: 900, unknown2: 1)]
			[DisplayName("Agile correction")]
			[Description("Correction value of agile correction")]
			[DefaultValue((Single)1)]
			public Single CorrectAgilityRate {
				get { return correctAgilityRate; }
				set {
					if ((double)value < 0 || (double)value > 99.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99.99 for " + CorrectAgilityRateProperty.Name + ".");
					SetProperty(ref correctAgilityRate, ref value, CorrectAgilityRateProperty);
				}
			}

			/// <summary>Magic correction</summary>
			/// <remarks>
			/// Japanese short name: "魔力補正", Google translated: "Magic correction".
			/// Japanese description: "魔力補正の補正値", Google translated: "Correction value of magic correction".
			/// </remarks>
			[ParameterTableRowAttribute("correctMagicRate", index: 9, minimum: 0, maximum: 99.99, step: 0.01, sortOrder: 1000, unknown2: 1)]
			[DisplayName("Magic correction")]
			[Description("Correction value of magic correction")]
			[DefaultValue((Single)1)]
			public Single CorrectMagicRate {
				get { return correctMagicRate; }
				set {
					if ((double)value < 0 || (double)value > 99.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99.99 for " + CorrectMagicRateProperty.Name + ".");
					SetProperty(ref correctMagicRate, ref value, CorrectMagicRateProperty);
				}
			}

			/// <summary>Faith correction</summary>
			/// <remarks>
			/// Japanese short name: "信仰補正", Google translated: "Faith correction".
			/// Japanese description: "信仰補正の補正値", Google translated: "Correction value of faith correction".
			/// </remarks>
			[ParameterTableRowAttribute("correctFaithRate", index: 10, minimum: 0, maximum: 99.99, step: 0.01, sortOrder: 1100, unknown2: 1)]
			[DisplayName("Faith correction")]
			[Description("Correction value of faith correction")]
			[DefaultValue((Single)1)]
			public Single CorrectFaithRate {
				get { return correctFaithRate; }
				set {
					if ((double)value < 0 || (double)value > 99.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99.99 for " + CorrectFaithRateProperty.Name + ".");
					SetProperty(ref correctFaithRate, ref value, CorrectFaithRateProperty);
				}
			}

			/// <summary>Guard during a physical attack rate cut</summary>
			/// <remarks>
			/// Japanese short name: "ガード時物理攻撃カット率", Google translated: "Guard during a physical attack rate cut".
			/// Japanese description: "ガード時物理攻撃カット率の補正値", Google translated: "Correction value of the guard during the physical attack rate cut".
			/// </remarks>
			[ParameterTableRowAttribute("physicsGuardCutRate", index: 11, minimum: 0, maximum: 99.99, step: 0.01, sortOrder: 1200, unknown2: 1)]
			[DisplayName("Guard during a physical attack rate cut")]
			[Description("Correction value of the guard during the physical attack rate cut")]
			[DefaultValue((Single)1)]
			public Single PhysicsGuardCutRate {
				get { return physicsGuardCutRate; }
				set {
					if ((double)value < 0 || (double)value > 99.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99.99 for " + PhysicsGuardCutRateProperty.Name + ".");
					SetProperty(ref physicsGuardCutRate, ref value, PhysicsGuardCutRateProperty);
				}
			}

			/// <summary>Guard when magic attack rate cut</summary>
			/// <remarks>
			/// Japanese short name: "ガード時魔法攻撃カット率", Google translated: "Guard when magic attack rate cut".
			/// Japanese description: "ガード時魔法攻撃カット率の補正値", Google translated: "Correction value of the guard at the time magic attack rate cut".
			/// </remarks>
			[ParameterTableRowAttribute("magicGuardCutRate", index: 12, minimum: 0, maximum: 99.99, step: 0.01, sortOrder: 1300, unknown2: 1)]
			[DisplayName("Guard when magic attack rate cut")]
			[Description("Correction value of the guard at the time magic attack rate cut")]
			[DefaultValue((Single)1)]
			public Single MagicGuardCutRate {
				get { return magicGuardCutRate; }
				set {
					if ((double)value < 0 || (double)value > 99.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99.99 for " + MagicGuardCutRateProperty.Name + ".");
					SetProperty(ref magicGuardCutRate, ref value, MagicGuardCutRateProperty);
				}
			}

			/// <summary>Guard when flame attack rate cut</summary>
			/// <remarks>
			/// Japanese short name: "ガード時炎攻撃カット率", Google translated: "Guard when flame attack rate cut".
			/// Japanese description: "ガード時炎攻撃カット率の補正値", Google translated: "Correction value of the guard at the time flame attack rate cut".
			/// </remarks>
			[ParameterTableRowAttribute("fireGuardCutRate", index: 13, minimum: 0, maximum: 99.99, step: 0.01, sortOrder: 1400, unknown2: 1)]
			[DisplayName("Guard when flame attack rate cut")]
			[Description("Correction value of the guard at the time flame attack rate cut")]
			[DefaultValue((Single)1)]
			public Single FireGuardCutRate {
				get { return fireGuardCutRate; }
				set {
					if ((double)value < 0 || (double)value > 99.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99.99 for " + FireGuardCutRateProperty.Name + ".");
					SetProperty(ref fireGuardCutRate, ref value, FireGuardCutRateProperty);
				}
			}

			/// <summary>Guard when lightning attack rate cut</summary>
			/// <remarks>
			/// Japanese short name: "ガード時電撃攻撃カット率", Google translated: "Guard when lightning attack rate cut".
			/// Japanese description: "ガード時電撃攻撃カット率の補正値", Google translated: "Correction value of the guard when lightning attack rate cut".
			/// </remarks>
			[ParameterTableRowAttribute("thunderGuardCutRate", index: 14, minimum: 0, maximum: 99.99, step: 0.01, sortOrder: 1500, unknown2: 1)]
			[DisplayName("Guard when lightning attack rate cut")]
			[Description("Correction value of the guard when lightning attack rate cut")]
			[DefaultValue((Single)1)]
			public Single ThunderGuardCutRate {
				get { return thunderGuardCutRate; }
				set {
					if ((double)value < 0 || (double)value > 99.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99.99 for " + ThunderGuardCutRateProperty.Name + ".");
					SetProperty(ref thunderGuardCutRate, ref value, ThunderGuardCutRateProperty);
				}
			}

			/// <summary>Guard when poison attack rate cut</summary>
			/// <remarks>
			/// Japanese short name: "ガード時毒攻撃カット率", Google translated: "Guard when poison attack rate cut".
			/// Japanese description: "ガード時毒攻撃カット率の補正値", Google translated: "Correction value of the guard at the time poison attack rate cut".
			/// </remarks>
			[ParameterTableRowAttribute("poisonGuardResistRate", index: 15, minimum: 0, maximum: 99.99, step: 0.01, sortOrder: 1600, unknown2: 1)]
			[DisplayName("Guard when poison attack rate cut")]
			[Description("Correction value of the guard at the time poison attack rate cut")]
			[DefaultValue((Single)1)]
			public Single PoisonGuardResistRate {
				get { return poisonGuardResistRate; }
				set {
					if ((double)value < 0 || (double)value > 99.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99.99 for " + PoisonGuardResistRateProperty.Name + ".");
					SetProperty(ref poisonGuardResistRate, ref value, PoisonGuardResistRateProperty);
				}
			}

			/// <summary>Guard when plague attack rate cut</summary>
			/// <remarks>
			/// Japanese short name: "ガード時疫病攻撃カット率", Google translated: "Guard when plague attack rate cut".
			/// Japanese description: "ガード時疫病攻撃カット率の補正値", Google translated: "Correction value of the guard during the plague attack rate cut".
			/// </remarks>
			[ParameterTableRowAttribute("diseaseGuardResistRate", index: 16, minimum: 0, maximum: 99.99, step: 0.01, sortOrder: 1700, unknown2: 1)]
			[DisplayName("Guard when plague attack rate cut")]
			[Description("Correction value of the guard during the plague attack rate cut")]
			[DefaultValue((Single)1)]
			public Single DiseaseGuardResistRate {
				get { return diseaseGuardResistRate; }
				set {
					if ((double)value < 0 || (double)value > 99.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99.99 for " + DiseaseGuardResistRateProperty.Name + ".");
					SetProperty(ref diseaseGuardResistRate, ref value, DiseaseGuardResistRateProperty);
				}
			}

			/// <summary>Bleeding attack rate cut guard when</summary>
			/// <remarks>
			/// Japanese short name: "ガード時出血攻撃カット率", Google translated: "Bleeding attack rate cut guard when".
			/// Japanese description: "ガード時出血攻撃カット率の補正値", Google translated: "Correction value of bleeding attack cut rates when guard".
			/// </remarks>
			[ParameterTableRowAttribute("bloodGuardResistRate", index: 17, minimum: 0, maximum: 99.99, step: 0.01, sortOrder: 1800, unknown2: 1)]
			[DisplayName("Bleeding attack rate cut guard when")]
			[Description("Correction value of bleeding attack cut rates when guard")]
			[DefaultValue((Single)1)]
			public Single BloodGuardResistRate {
				get { return bloodGuardResistRate; }
				set {
					if ((double)value < 0 || (double)value > 99.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99.99 for " + BloodGuardResistRateProperty.Name + ".");
					SetProperty(ref bloodGuardResistRate, ref value, BloodGuardResistRateProperty);
				}
			}

			/// <summary>Guard when curse attack rate cut</summary>
			/// <remarks>
			/// Japanese short name: "ガード時呪攻撃カット率", Google translated: "Guard when curse attack rate cut".
			/// Japanese description: "ガード時呪い攻撃カット率の補正値", Google translated: "Correction value of the guard at the time curse attack rate cut".
			/// </remarks>
			[ParameterTableRowAttribute("curseGuardResistRate", index: 18, minimum: 0, maximum: 99.99, step: 0.01, sortOrder: 1900, unknown2: 1)]
			[DisplayName("Guard when curse attack rate cut")]
			[Description("Correction value of the guard at the time curse attack rate cut")]
			[DefaultValue((Single)1)]
			public Single CurseGuardResistRate {
				get { return curseGuardResistRate; }
				set {
					if ((double)value < 0 || (double)value > 99.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99.99 for " + CurseGuardResistRateProperty.Name + ".");
					SetProperty(ref curseGuardResistRate, ref value, CurseGuardResistRateProperty);
				}
			}

			/// <summary>Guard when stamina Defense</summary>
			/// <remarks>
			/// Japanese short name: "ガード時スタミナ防御力", Google translated: "Guard when stamina Defense".
			/// Japanese description: "ガード時スタミナ防御力の補正値", Google translated: "Correction value of the guard at the time stamina Defense".
			/// </remarks>
			[ParameterTableRowAttribute("staminaGuardDefRate", index: 19, minimum: 0, maximum: 99.99, step: 0.01, sortOrder: 2000, unknown2: 1)]
			[DisplayName("Guard when stamina Defense")]
			[Description("Correction value of the guard at the time stamina Defense")]
			[DefaultValue((Single)1)]
			public Single StaminaGuardDefRate {
				get { return staminaGuardDefRate; }
				set {
					if ((double)value < 0 || (double)value > 99.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99.99 for " + StaminaGuardDefRateProperty.Name + ".");
					SetProperty(ref staminaGuardDefRate, ref value, StaminaGuardDefRateProperty);
				}
			}

			/// <summary>Special effects ID1</summary>
			/// <remarks>
			/// Japanese short name: "特殊効果ID1", Google translated: "Special effects ID1".
			/// Japanese description: "特殊効果ID1の加算補正値", Google translated: "Adding the correction value of the special effects ID1".
			/// </remarks>
			[ParameterTableRowAttribute("spEffectId1", index: 20, minimum: 0, maximum: 255, step: 1, sortOrder: 2100, unknown2: 1)]
			[DisplayName("Special effects ID1")]
			[Description("Adding the correction value of the special effects ID1")]
			[DefaultValue((Byte)0)]
			public Byte SpEffectId1 {
				get { return spEffectId1; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for " + SpEffectId1Property.Name + ".");
					SetProperty(ref spEffectId1, ref value, SpEffectId1Property);
				}
			}

			/// <summary>Special effects ID2</summary>
			/// <remarks>
			/// Japanese short name: "特殊効果ID2", Google translated: "Special effects ID2".
			/// Japanese description: "特殊効果ID2の加算補正値", Google translated: "Adding the correction value of the special effects ID2".
			/// </remarks>
			[ParameterTableRowAttribute("spEffectId2", index: 21, minimum: 0, maximum: 255, step: 1, sortOrder: 2200, unknown2: 1)]
			[DisplayName("Special effects ID2")]
			[Description("Adding the correction value of the special effects ID2")]
			[DefaultValue((Byte)0)]
			public Byte SpEffectId2 {
				get { return spEffectId2; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for " + SpEffectId2Property.Name + ".");
					SetProperty(ref spEffectId2, ref value, SpEffectId2Property);
				}
			}

			/// <summary>Special effects ID3</summary>
			/// <remarks>
			/// Japanese short name: "特殊効果ID3", Google translated: "Special effects ID3".
			/// Japanese description: "特殊効果ID3の加算補正値", Google translated: "Adding the correction value of the special effects ID3".
			/// </remarks>
			[ParameterTableRowAttribute("spEffectId3", index: 22, minimum: 0, maximum: 255, step: 1, sortOrder: 2300, unknown2: 1)]
			[DisplayName("Special effects ID3")]
			[Description("Adding the correction value of the special effects ID3")]
			[DefaultValue((Byte)0)]
			public Byte SpEffectId3 {
				get { return spEffectId3; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for " + SpEffectId3Property.Name + ".");
					SetProperty(ref spEffectId3, ref value, SpEffectId3Property);
				}
			}

			/// <summary>Resident special effects ID1</summary>
			/// <remarks>
			/// Japanese short name: "常駐特殊効果ID1", Google translated: "Resident special effects ID1".
			/// Japanese description: "常駐特殊効果ID1の加算補正値", Google translated: "Adding the correction value of the resident special effects ID1".
			/// </remarks>
			[ParameterTableRowAttribute("residentSpEffectId1", index: 23, minimum: 0, maximum: 255, step: 1, sortOrder: 2400, unknown2: 1)]
			[DisplayName("Resident special effects ID1")]
			[Description("Adding the correction value of the resident special effects ID1")]
			[DefaultValue((Byte)0)]
			public Byte ResidentSpEffectId1 {
				get { return residentSpEffectId1; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for " + ResidentSpEffectId1Property.Name + ".");
					SetProperty(ref residentSpEffectId1, ref value, ResidentSpEffectId1Property);
				}
			}

			/// <summary>Resident special effects ID2</summary>
			/// <remarks>
			/// Japanese short name: "常駐特殊効果ID2", Google translated: "Resident special effects ID2".
			/// Japanese description: "常駐特殊効果ID2の加算補正値", Google translated: "Adding the correction value of the resident special effects ID2".
			/// </remarks>
			[ParameterTableRowAttribute("residentSpEffectId2", index: 24, minimum: 0, maximum: 255, step: 1, sortOrder: 2500, unknown2: 1)]
			[DisplayName("Resident special effects ID2")]
			[Description("Adding the correction value of the resident special effects ID2")]
			[DefaultValue((Byte)0)]
			public Byte ResidentSpEffectId2 {
				get { return residentSpEffectId2; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for " + ResidentSpEffectId2Property.Name + ".");
					SetProperty(ref residentSpEffectId2, ref value, ResidentSpEffectId2Property);
				}
			}

			/// <summary>Resident special effects ID3</summary>
			/// <remarks>
			/// Japanese short name: "常駐特殊効果ID3", Google translated: "Resident special effects ID3".
			/// Japanese description: "常駐特殊効果ID3の加算補正値", Google translated: "Adding the correction value of the resident special effects ID3".
			/// </remarks>
			[ParameterTableRowAttribute("residentSpEffectId3", index: 25, minimum: 0, maximum: 255, step: 1, sortOrder: 2600, unknown2: 1)]
			[DisplayName("Resident special effects ID3")]
			[Description("Adding the correction value of the resident special effects ID3")]
			[DefaultValue((Byte)0)]
			public Byte ResidentSpEffectId3 {
				get { return residentSpEffectId3; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for " + ResidentSpEffectId3Property.Name + ".");
					SetProperty(ref residentSpEffectId3, ref value, ResidentSpEffectId3Property);
				}
			}

			/// <summary>Material ID added value</summary>
			/// <remarks>
			/// Japanese short name: "素材ID加算値", Google translated: "Material ID added value".
			/// Japanese description: "素材パラメータIDの加算補正値", Google translated: "Adding the correction value of the material parameter ID".
			/// </remarks>
			[ParameterTableRowAttribute("materialSetId", index: 26, minimum: 0, maximum: 255, step: 1, sortOrder: 2700, unknown2: 1)]
			[DisplayName("Material ID added value")]
			[Description("Adding the correction value of the material parameter ID")]
			[DefaultValue((Byte)0)]
			public Byte MaterialSetId {
				get { return materialSetId; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for " + MaterialSetIdProperty.Name + ".");
					SetProperty(ref materialSetId, ref value, MaterialSetIdProperty);
				}
			}

			/// <summary>pading</summary>
			/// <remarks>
			/// Japanese short name: "pading", Google translated: "pading".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("pad[9]", index: 27, minimum: 0, maximum: 0, step: 0, sortOrder: 2701, unknown2: 0)]
			[DisplayName("pading")]
			[Description("")]
			[Browsable(false)]
			public Byte[] Pad {
				get { return pad; }
				set { SetProperty(ref pad, ref value, PadProperty); }
			}

			internal WeaponReinforcement(ParameterTable table, int index, AssetLoader loader, int next)
				: base(table, index, loader) {
				BinaryReader reader = loader.Reader;

				PhysicsAtkRate = reader.ReadSingle();
				MagicAtkRate = reader.ReadSingle();
				FireAtkRate = reader.ReadSingle();
				ThunderAtkRate = reader.ReadSingle();
				StaminaAtkRate = reader.ReadSingle();
				SaWeaponAtkRate = reader.ReadSingle();
				SaDurabilityRate = reader.ReadSingle();
				CorrectStrengthRate = reader.ReadSingle();
				CorrectAgilityRate = reader.ReadSingle();
				CorrectMagicRate = reader.ReadSingle();
				CorrectFaithRate = reader.ReadSingle();
				PhysicsGuardCutRate = reader.ReadSingle();
				MagicGuardCutRate = reader.ReadSingle();
				FireGuardCutRate = reader.ReadSingle();
				ThunderGuardCutRate = reader.ReadSingle();
				PoisonGuardResistRate = reader.ReadSingle();
				DiseaseGuardResistRate = reader.ReadSingle();
				BloodGuardResistRate = reader.ReadSingle();
				CurseGuardResistRate = reader.ReadSingle();
				StaminaGuardDefRate = reader.ReadSingle();
				SpEffectId1 = reader.ReadByte();
				SpEffectId2 = reader.ReadByte();
				SpEffectId3 = reader.ReadByte();
				ResidentSpEffectId1 = reader.ReadByte();
				ResidentSpEffectId2 = reader.ReadByte();
				ResidentSpEffectId3 = reader.ReadByte();
				MaterialSetId = reader.ReadByte();
				Pad = reader.ReadBytes(9);
			}

			internal WeaponReinforcement(ParameterTable table, int index)
				: base(table, index) {
				PhysicsAtkRate = (Single)1;
				MagicAtkRate = (Single)1;
				FireAtkRate = (Single)1;
				ThunderAtkRate = (Single)1;
				StaminaAtkRate = (Single)1;
				SaWeaponAtkRate = (Single)1;
				SaDurabilityRate = (Single)1;
				CorrectStrengthRate = (Single)1;
				CorrectAgilityRate = (Single)1;
				CorrectMagicRate = (Single)1;
				CorrectFaithRate = (Single)1;
				PhysicsGuardCutRate = (Single)1;
				MagicGuardCutRate = (Single)1;
				FireGuardCutRate = (Single)1;
				ThunderGuardCutRate = (Single)1;
				PoisonGuardResistRate = (Single)1;
				DiseaseGuardResistRate = (Single)1;
				BloodGuardResistRate = (Single)1;
				CurseGuardResistRate = (Single)1;
				StaminaGuardDefRate = (Single)1;
				SpEffectId1 = (Byte)0;
				SpEffectId2 = (Byte)0;
				SpEffectId3 = (Byte)0;
				ResidentSpEffectId1 = (Byte)0;
				ResidentSpEffectId2 = (Byte)0;
				ResidentSpEffectId3 = (Byte)0;
				MaterialSetId = (Byte)0;
				Pad = new Byte[9];
			}

			/// <summary>
			/// Write the <see cref="WeaponReinforcement"/> row to the stream.
			/// </summary>
			/// <param name="writer"></param>
			public override void Write(BinaryWriter writer) {
				writer.Write(PhysicsAtkRate);
				writer.Write(MagicAtkRate);
				writer.Write(FireAtkRate);
				writer.Write(ThunderAtkRate);
				writer.Write(StaminaAtkRate);
				writer.Write(SaWeaponAtkRate);
				writer.Write(SaDurabilityRate);
				writer.Write(CorrectStrengthRate);
				writer.Write(CorrectAgilityRate);
				writer.Write(CorrectMagicRate);
				writer.Write(CorrectFaithRate);
				writer.Write(PhysicsGuardCutRate);
				writer.Write(MagicGuardCutRate);
				writer.Write(FireGuardCutRate);
				writer.Write(ThunderGuardCutRate);
				writer.Write(PoisonGuardResistRate);
				writer.Write(DiseaseGuardResistRate);
				writer.Write(BloodGuardResistRate);
				writer.Write(CurseGuardResistRate);
				writer.Write(StaminaGuardDefRate);
				writer.Write(SpEffectId1);
				writer.Write(SpEffectId2);
				writer.Write(SpEffectId3);
				writer.Write(ResidentSpEffectId1);
				writer.Write(ResidentSpEffectId2);
				writer.Write(ResidentSpEffectId3);
				writer.Write(MaterialSetId);
				writer.Write(Pad);
			}
		}
	}
}
