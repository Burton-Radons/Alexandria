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
		/// <summary>Armor.</summary>
		/// <remarks>
		/// From "EquipParamProtector.paramdef" (id 0Dh).
		/// </remarks>
		[ParameterTableRowOrder("sortId", 0, 1300)]
		[ParameterTableRowOrder("weight", 8, 2700)]
		[ParameterTableRowOrder("basicPrice", 6, 2600)]
		[ParameterTableRowOrder("sellValue", 7, 2610)]
		[ParameterTableRowOrder("isDeposit", 73, 4775)]
		[ParameterTableRowOrder("fixPrice", 5, 2500)]
		[ParameterTableRowOrder("wanderingEquipId", 1, 1400)]
		[ParameterTableRowOrder("vagrantItemLotId", 2, 1500)]
		[ParameterTableRowOrder("vagrantBonusEneDropItemLotId", 3, 1600)]
		[ParameterTableRowOrder("vagrantItemEneDropItemLotId", 4, 1700)]
		[ParameterTableRowOrder("equipModelCategory", 65, 100)]
		[ParameterTableRowOrder("equipModelGender", 66, 200)]
		[ParameterTableRowOrder("equipModelId", 40, 300)]
		[ParameterTableRowOrder("oldSortId", 130, 1300)]
		[ParameterTableRowOrder("durability", 48, 3200)]
		[ParameterTableRowOrder("durabilityMax", 49, 3300)]
		public class Protector : ClothingTableRow {
			/// <summary>The name of the table in the file.</summary>
			public const string TableName = "EQUIP_PARAM_PROTECTOR_ST";

			Int32 residentSpEffectId, residentSpEffectId2, residentSpEffectId3, materialSetId, originEquipPro, originEquipPro1, originEquipPro2, originEquipPro3, originEquipPro4, originEquipPro5, originEquipPro6, originEquipPro7, originEquipPro8, originEquipPro9, originEquipPro10, originEquipPro11, originEquipPro12, originEquipPro13, originEquipPro14, originEquipPro15, qwcId;
			Single partsDamageRate, corectSARecover, faceScaleM_ScaleX, faceScaleM_ScaleZ, faceScaleM_MaxX, faceScaleM_MaxZ, faceScaleF_ScaleX, faceScaleF_ScaleZ, faceScaleF_MaxX, faceScaleF_MaxZ;
			UInt16 iconIdM, iconIdF, knockBack, knockbackBounceRate, defFlickPower, defensePhysics, defenseMagic, defenseFire, defenseThunder, resistPoison, resistDisease, resistBlood, resistCurse;
			Int16 saDurability, defenseSlash, defenseBlow, defenseThrust, reinforceTypeId, trophySGradeId, shopLv;
			Byte knockbackParamId, flickDamageCutRate;
			ArmorCategory protectorCategory;
			WeaponMaterialDefend defenseMaterial, defenseMaterial_Weak;
			WeaponMaterialDefendSound defenseMaterialSfx, defenseMaterialSfx_Weak;
			AttackParameterPartDamageType partsDmgType;
			Byte[] pad_0, pad_1;

			/// <summary>A property in the class.</summary>
			public static readonly PropertyInfo
				ResidentSpEffectIdProperty = GetProperty<Protector>("ResidentSpEffectId"),
				ResidentSpEffectId2Property = GetProperty<Protector>("ResidentSpEffectId2"),
				ResidentSpEffectId3Property = GetProperty<Protector>("ResidentSpEffectId3"),
				MaterialSetIdProperty = GetProperty<Protector>("MaterialSetId"),
				PartsDamageRateProperty = GetProperty<Protector>("PartsDamageRate"),
				CorectSARecoverProperty = GetProperty<Protector>("CorectSARecover"),
				OriginEquipProProperty = GetProperty<Protector>("OriginEquipPro"),
				OriginEquipPro1Property = GetProperty<Protector>("OriginEquipPro1"),
				OriginEquipPro2Property = GetProperty<Protector>("OriginEquipPro2"),
				OriginEquipPro3Property = GetProperty<Protector>("OriginEquipPro3"),
				OriginEquipPro4Property = GetProperty<Protector>("OriginEquipPro4"),
				OriginEquipPro5Property = GetProperty<Protector>("OriginEquipPro5"),
				OriginEquipPro6Property = GetProperty<Protector>("OriginEquipPro6"),
				OriginEquipPro7Property = GetProperty<Protector>("OriginEquipPro7"),
				OriginEquipPro8Property = GetProperty<Protector>("OriginEquipPro8"),
				OriginEquipPro9Property = GetProperty<Protector>("OriginEquipPro9"),
				OriginEquipPro10Property = GetProperty<Protector>("OriginEquipPro10"),
				OriginEquipPro11Property = GetProperty<Protector>("OriginEquipPro11"),
				OriginEquipPro12Property = GetProperty<Protector>("OriginEquipPro12"),
				OriginEquipPro13Property = GetProperty<Protector>("OriginEquipPro13"),
				OriginEquipPro14Property = GetProperty<Protector>("OriginEquipPro14"),
				OriginEquipPro15Property = GetProperty<Protector>("OriginEquipPro15"),
				FaceScaleM_ScaleXProperty = GetProperty<Protector>("FaceScaleM_ScaleX"),
				FaceScaleM_ScaleZProperty = GetProperty<Protector>("FaceScaleM_ScaleZ"),
				FaceScaleM_MaxXProperty = GetProperty<Protector>("FaceScaleM_MaxX"),
				FaceScaleM_MaxZProperty = GetProperty<Protector>("FaceScaleM_MaxZ"),
				FaceScaleF_ScaleXProperty = GetProperty<Protector>("FaceScaleF_ScaleX"),
				FaceScaleF_ScaleZProperty = GetProperty<Protector>("FaceScaleF_ScaleZ"),
				FaceScaleF_MaxXProperty = GetProperty<Protector>("FaceScaleF_MaxX"),
				FaceScaleF_MaxZProperty = GetProperty<Protector>("FaceScaleF_MaxZ"),
				QwcIdProperty = GetProperty<Protector>("QwcId"),
				IconIdMProperty = GetProperty<Protector>("IconIdM"),
				IconIdFProperty = GetProperty<Protector>("IconIdF"),
				KnockBackProperty = GetProperty<Protector>("KnockBack"),
				KnockbackBounceRateProperty = GetProperty<Protector>("KnockbackBounceRate"),
				SaDurabilityProperty = GetProperty<Protector>("SaDurability"),
				DefFlickPowerProperty = GetProperty<Protector>("DefFlickPower"),
				DefensePhysicsProperty = GetProperty<Protector>("DefensePhysics"),
				DefenseMagicProperty = GetProperty<Protector>("DefenseMagic"),
				DefenseFireProperty = GetProperty<Protector>("DefenseFire"),
				DefenseThunderProperty = GetProperty<Protector>("DefenseThunder"),
				DefenseSlashProperty = GetProperty<Protector>("DefenseSlash"),
				DefenseBlowProperty = GetProperty<Protector>("DefenseBlow"),
				DefenseThrustProperty = GetProperty<Protector>("DefenseThrust"),
				ResistPoisonProperty = GetProperty<Protector>("ResistPoison"),
				ResistDiseaseProperty = GetProperty<Protector>("ResistDisease"),
				ResistBloodProperty = GetProperty<Protector>("ResistBlood"),
				ResistCurseProperty = GetProperty<Protector>("ResistCurse"),
				ReinforceTypeIdProperty = GetProperty<Protector>("ReinforceTypeId"),
				TrophySGradeIdProperty = GetProperty<Protector>("TrophySGradeId"),
				ShopLvProperty = GetProperty<Protector>("ShopLv"),
				KnockbackParamIdProperty = GetProperty<Protector>("KnockbackParamId"),
				FlickDamageCutRateProperty = GetProperty<Protector>("FlickDamageCutRate"),
				ProtectorCategoryProperty = GetProperty<Protector>("ProtectorCategory"),
				DefenseMaterialProperty = GetProperty<Protector>("DefenseMaterial"),
				DefenseMaterialSfxProperty = GetProperty<Protector>("DefenseMaterialSfx"),
				PartsDmgTypeProperty = GetProperty<Protector>("PartsDmgType"),
				DefenseMaterial_WeakProperty = GetProperty<Protector>("DefenseMaterial_Weak"),
				DefenseMaterialSfx_WeakProperty = GetProperty<Protector>("DefenseMaterialSfx_Weak"),
				HeadEquipProperty = GetProperty<Protector>("HeadEquip"),
				BodyEquipProperty = GetProperty<Protector>("BodyEquip"),
				ArmEquipProperty = GetProperty<Protector>("ArmEquip"),
				LegEquipProperty = GetProperty<Protector>("LegEquip"),
				UseFaceScaleProperty = GetProperty<Protector>("UseFaceScale"),
				InvisibleFlag00Property = GetProperty<Protector>("InvisibleFlag00"),
				InvisibleFlag01Property = GetProperty<Protector>("InvisibleFlag01"),
				InvisibleFlag02Property = GetProperty<Protector>("InvisibleFlag02"),
				InvisibleFlag03Property = GetProperty<Protector>("InvisibleFlag03"),
				InvisibleFlag04Property = GetProperty<Protector>("InvisibleFlag04"),
				InvisibleFlag05Property = GetProperty<Protector>("InvisibleFlag05"),
				InvisibleFlag06Property = GetProperty<Protector>("InvisibleFlag06"),
				InvisibleFlag07Property = GetProperty<Protector>("InvisibleFlag07"),
				InvisibleFlag08Property = GetProperty<Protector>("InvisibleFlag08"),
				InvisibleFlag09Property = GetProperty<Protector>("InvisibleFlag09"),
				InvisibleFlag10Property = GetProperty<Protector>("InvisibleFlag10"),
				InvisibleFlag11Property = GetProperty<Protector>("InvisibleFlag11"),
				InvisibleFlag12Property = GetProperty<Protector>("InvisibleFlag12"),
				InvisibleFlag13Property = GetProperty<Protector>("InvisibleFlag13"),
				InvisibleFlag14Property = GetProperty<Protector>("InvisibleFlag14"),
				InvisibleFlag15Property = GetProperty<Protector>("InvisibleFlag15"),
				InvisibleFlag16Property = GetProperty<Protector>("InvisibleFlag16"),
				InvisibleFlag17Property = GetProperty<Protector>("InvisibleFlag17"),
				InvisibleFlag18Property = GetProperty<Protector>("InvisibleFlag18"),
				InvisibleFlag19Property = GetProperty<Protector>("InvisibleFlag19"),
				InvisibleFlag20Property = GetProperty<Protector>("InvisibleFlag20"),
				InvisibleFlag21Property = GetProperty<Protector>("InvisibleFlag21"),
				InvisibleFlag22Property = GetProperty<Protector>("InvisibleFlag22"),
				InvisibleFlag23Property = GetProperty<Protector>("InvisibleFlag23"),
				InvisibleFlag24Property = GetProperty<Protector>("InvisibleFlag24"),
				InvisibleFlag25Property = GetProperty<Protector>("InvisibleFlag25"),
				InvisibleFlag26Property = GetProperty<Protector>("InvisibleFlag26"),
				InvisibleFlag27Property = GetProperty<Protector>("InvisibleFlag27"),
				InvisibleFlag28Property = GetProperty<Protector>("InvisibleFlag28"),
				InvisibleFlag29Property = GetProperty<Protector>("InvisibleFlag29"),
				InvisibleFlag30Property = GetProperty<Protector>("InvisibleFlag30"),
				InvisibleFlag31Property = GetProperty<Protector>("InvisibleFlag31"),
				InvisibleFlag32Property = GetProperty<Protector>("InvisibleFlag32"),
				InvisibleFlag33Property = GetProperty<Protector>("InvisibleFlag33"),
				InvisibleFlag34Property = GetProperty<Protector>("InvisibleFlag34"),
				InvisibleFlag35Property = GetProperty<Protector>("InvisibleFlag35"),
				InvisibleFlag36Property = GetProperty<Protector>("InvisibleFlag36"),
				InvisibleFlag37Property = GetProperty<Protector>("InvisibleFlag37"),
				InvisibleFlag38Property = GetProperty<Protector>("InvisibleFlag38"),
				InvisibleFlag39Property = GetProperty<Protector>("InvisibleFlag39"),
				InvisibleFlag40Property = GetProperty<Protector>("InvisibleFlag40"),
				InvisibleFlag41Property = GetProperty<Protector>("InvisibleFlag41"),
				InvisibleFlag42Property = GetProperty<Protector>("InvisibleFlag42"),
				InvisibleFlag43Property = GetProperty<Protector>("InvisibleFlag43"),
				InvisibleFlag44Property = GetProperty<Protector>("InvisibleFlag44"),
				InvisibleFlag45Property = GetProperty<Protector>("InvisibleFlag45"),
				InvisibleFlag46Property = GetProperty<Protector>("InvisibleFlag46"),
				InvisibleFlag47Property = GetProperty<Protector>("InvisibleFlag47"),
				DisableMultiDropShareProperty = GetProperty<Protector>("DisableMultiDropShare"),
				SimpleModelForDlcProperty = GetProperty<Protector>("SimpleModelForDlc"),
				Pad_0Property = GetProperty<Protector>("Pad_0"),
				Pad_1Property = GetProperty<Protector>("Pad_1");

			/// <summary>Resident special effects ID1</summary>
			/// <remarks>
			/// Japanese short name: "常駐特殊効果ID1", Google translated: "Resident special effects ID1".
			/// Japanese description: "常駐特殊効果ID1", Google translated: "Resident special effects ID1".
			/// </remarks>
			[ParameterTableRowAttribute("residentSpEffectId", index: 9, minimum: -1, maximum: 999999, step: 1, sortOrder: 4500, unknown2: 1)]
			[DisplayName("Resident special effects ID1")]
			[Description("Resident special effects ID1")]
			[DefaultValue((Int32)0)]
			public Int32 ResidentSpEffectId {
				get { return residentSpEffectId; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for ResidentSpEffectId.");
					SetProperty(ref residentSpEffectId, ref value, ResidentSpEffectIdProperty);
				}
			}

			/// <summary>Resident special effects ID2</summary>
			/// <remarks>
			/// Japanese short name: "常駐特殊効果ID2", Google translated: "Resident special effects ID2".
			/// Japanese description: "常駐特殊効果ID2", Google translated: "Resident special effects ID2".
			/// </remarks>
			[ParameterTableRowAttribute("residentSpEffectId2", index: 10, minimum: -1, maximum: 999999, step: 1, sortOrder: 4600, unknown2: 1)]
			[DisplayName("Resident special effects ID2")]
			[Description("Resident special effects ID2")]
			[DefaultValue((Int32)0)]
			public Int32 ResidentSpEffectId2 {
				get { return residentSpEffectId2; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for ResidentSpEffectId2.");
					SetProperty(ref residentSpEffectId2, ref value, ResidentSpEffectId2Property);
				}
			}

			/// <summary>Resident special effects ID3</summary>
			/// <remarks>
			/// Japanese short name: "常駐特殊効果ID3", Google translated: "Resident special effects ID3".
			/// Japanese description: "常駐特殊効果ID3", Google translated: "Resident special effects ID3".
			/// </remarks>
			[ParameterTableRowAttribute("residentSpEffectId3", index: 11, minimum: -1, maximum: 999999, step: 1, sortOrder: 4700, unknown2: 1)]
			[DisplayName("Resident special effects ID3")]
			[Description("Resident special effects ID3")]
			[DefaultValue((Int32)0)]
			public Int32 ResidentSpEffectId3 {
				get { return residentSpEffectId3; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for ResidentSpEffectId3.");
					SetProperty(ref residentSpEffectId3, ref value, ResidentSpEffectId3Property);
				}
			}

			/// <summary>Material ID</summary>
			/// <remarks>
			/// Japanese short name: "素材ID", Google translated: "Material ID".
			/// Japanese description: "武器強化に必要な素材パラメータID", Google translated: "Material parameters ID required weapon enhancement".
			/// </remarks>
			[ParameterTableRowAttribute("materialSetId", index: 12, minimum: -1, maximum: 999999, step: 1, sortOrder: 4800, unknown2: 1)]
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

			/// <summary>Site damage rate</summary>
			/// <remarks>
			/// Japanese short name: "部位ダメージ率", Google translated: "Site damage rate".
			/// Japanese description: "部位ダメージ率", Google translated: "Site damage rate".
			/// </remarks>
			[ParameterTableRowAttribute("partsDamageRate", index: 13, minimum: 0, maximum: 99, step: 0.01, sortOrder: 1900, unknown2: 1)]
			[DisplayName("Site damage rate")]
			[Description("Site damage rate")]
			[DefaultValue((Single)1)]
			public Single PartsDamageRate {
				get { return partsDamageRate; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for PartsDamageRate.");
					SetProperty(ref partsDamageRate, ref value, PartsDamageRateProperty);
				}
			}

			/// <summary>SA recovery time correction value</summary>
			/// <remarks>
			/// Japanese short name: "SA回復時間補正値", Google translated: "SA recovery time correction value".
			/// Japanese description: "スーパーアーマー回復時間の補正値", Google translated: "Correction value of super armor recovery time".
			/// </remarks>
			[ParameterTableRowAttribute("corectSARecover", index: 14, minimum: -1, maximum: 99.99, step: 0.01, sortOrder: 3100, unknown2: 1)]
			[DisplayName("SA recovery time correction value")]
			[Description("Correction value of super armor recovery time")]
			[DefaultValue((Single)0)]
			public Single CorectSARecover {
				get { return corectSARecover; }
				set {
					if ((double)value < -1 || (double)value > 99.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 99.99 for CorectSARecover.");
					SetProperty(ref corectSARecover, ref value, CorectSARecoverProperty);
				}
			}

			/// <summary>Derived</summary>
			/// <remarks>
			/// Japanese short name: "派生元", Google translated: "Derived".
			/// Japanese description: "この防具の強化元防具ID", Google translated: "Strengthening the original armor ID of this armor".
			/// </remarks>
			[ParameterTableRowAttribute("originEquipPro", index: 15, minimum: -1, maximum: 1E+08, step: 1, sortOrder: 5000, unknown2: 1)]
			[DisplayName("Derived")]
			[Description("Strengthening the original armor ID of this armor")]
			[DefaultValue((Int32)(-1))]
			public Int32 OriginEquipPro {
				get { return originEquipPro; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for OriginEquipPro.");
					SetProperty(ref originEquipPro, ref value, OriginEquipProProperty);
				}
			}

			/// <summary>Derive enhancement +1</summary>
			/// <remarks>
			/// Japanese short name: "派生元 強化+1", Google translated: "Derive enhancement +1".
			/// Japanese description: "この防具の強化元防具ID1", Google translated: "Strengthening the original armor ID1 of this armor".
			/// </remarks>
			[ParameterTableRowAttribute("originEquipPro1", index: 16, minimum: -1, maximum: 1E+08, step: 1, sortOrder: 5100, unknown2: 1)]
			[DisplayName("Derive enhancement +1")]
			[Description("Strengthening the original armor ID1 of this armor")]
			[DefaultValue((Int32)(-1))]
			public Int32 OriginEquipPro1 {
				get { return originEquipPro1; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for OriginEquipPro1.");
					SetProperty(ref originEquipPro1, ref value, OriginEquipPro1Property);
				}
			}

			/// <summary>Derive enhancement +2</summary>
			/// <remarks>
			/// Japanese short name: "派生元 強化+2", Google translated: "Derive enhancement +2".
			/// Japanese description: "この防具の強化元防具ID2", Google translated: "Strengthening the original armor ID2 of this armor".
			/// </remarks>
			[ParameterTableRowAttribute("originEquipPro2", index: 17, minimum: -1, maximum: 1E+08, step: 1, sortOrder: 5200, unknown2: 1)]
			[DisplayName("Derive enhancement +2")]
			[Description("Strengthening the original armor ID2 of this armor")]
			[DefaultValue((Int32)(-1))]
			public Int32 OriginEquipPro2 {
				get { return originEquipPro2; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for OriginEquipPro2.");
					SetProperty(ref originEquipPro2, ref value, OriginEquipPro2Property);
				}
			}

			/// <summary>Derive enhancement +3</summary>
			/// <remarks>
			/// Japanese short name: "派生元 強化+3", Google translated: "Derive enhancement +3".
			/// Japanese description: "この防具の強化元防具ID3", Google translated: "Strengthening the original ID3 armor of this armor".
			/// </remarks>
			[ParameterTableRowAttribute("originEquipPro3", index: 18, minimum: -1, maximum: 1E+08, step: 1, sortOrder: 5300, unknown2: 1)]
			[DisplayName("Derive enhancement +3")]
			[Description("Strengthening the original ID3 armor of this armor")]
			[DefaultValue((Int32)(-1))]
			public Int32 OriginEquipPro3 {
				get { return originEquipPro3; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for OriginEquipPro3.");
					SetProperty(ref originEquipPro3, ref value, OriginEquipPro3Property);
				}
			}

			/// <summary>Derive enhancement +4</summary>
			/// <remarks>
			/// Japanese short name: "派生元 強化+4", Google translated: "Derive enhancement +4".
			/// Japanese description: "この防具の強化元防具ID4", Google translated: "Strengthening the original armor ID4 of this armor".
			/// </remarks>
			[ParameterTableRowAttribute("originEquipPro4", index: 19, minimum: -1, maximum: 1E+08, step: 1, sortOrder: 5400, unknown2: 1)]
			[DisplayName("Derive enhancement +4")]
			[Description("Strengthening the original armor ID4 of this armor")]
			[DefaultValue((Int32)(-1))]
			public Int32 OriginEquipPro4 {
				get { return originEquipPro4; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for OriginEquipPro4.");
					SetProperty(ref originEquipPro4, ref value, OriginEquipPro4Property);
				}
			}

			/// <summary>Derive enhancement +5</summary>
			/// <remarks>
			/// Japanese short name: "派生元 強化+5", Google translated: "Derive enhancement +5".
			/// Japanese description: "この防具の強化元防具ID5", Google translated: "Strengthening the original armor ID5 of this armor".
			/// </remarks>
			[ParameterTableRowAttribute("originEquipPro5", index: 20, minimum: -1, maximum: 1E+08, step: 1, sortOrder: 5500, unknown2: 1)]
			[DisplayName("Derive enhancement +5")]
			[Description("Strengthening the original armor ID5 of this armor")]
			[DefaultValue((Int32)(-1))]
			public Int32 OriginEquipPro5 {
				get { return originEquipPro5; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for OriginEquipPro5.");
					SetProperty(ref originEquipPro5, ref value, OriginEquipPro5Property);
				}
			}

			/// <summary>Derive enhancement +6</summary>
			/// <remarks>
			/// Japanese short name: "派生元 強化+6", Google translated: "Derive enhancement +6".
			/// Japanese description: "この防具の強化元防具ID6", Google translated: "Strengthening the original armor ID6 of this armor".
			/// </remarks>
			[ParameterTableRowAttribute("originEquipPro6", index: 21, minimum: -1, maximum: 1E+08, step: 1, sortOrder: 5600, unknown2: 1)]
			[DisplayName("Derive enhancement +6")]
			[Description("Strengthening the original armor ID6 of this armor")]
			[DefaultValue((Int32)(-1))]
			public Int32 OriginEquipPro6 {
				get { return originEquipPro6; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for OriginEquipPro6.");
					SetProperty(ref originEquipPro6, ref value, OriginEquipPro6Property);
				}
			}

			/// <summary>Derive enhancement +7</summary>
			/// <remarks>
			/// Japanese short name: "派生元 強化+7", Google translated: "Derive enhancement +7".
			/// Japanese description: "この防具の強化元防具ID7", Google translated: "Strengthening the original armor ID7 of this armor".
			/// </remarks>
			[ParameterTableRowAttribute("originEquipPro7", index: 22, minimum: -1, maximum: 1E+08, step: 1, sortOrder: 5700, unknown2: 1)]
			[DisplayName("Derive enhancement +7")]
			[Description("Strengthening the original armor ID7 of this armor")]
			[DefaultValue((Int32)(-1))]
			public Int32 OriginEquipPro7 {
				get { return originEquipPro7; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for OriginEquipPro7.");
					SetProperty(ref originEquipPro7, ref value, OriginEquipPro7Property);
				}
			}

			/// <summary>Derive enhancement +8</summary>
			/// <remarks>
			/// Japanese short name: "派生元 強化+8", Google translated: "Derive enhancement +8".
			/// Japanese description: "この防具の強化元防具ID8", Google translated: "Strengthening the original armor ID8 of this armor".
			/// </remarks>
			[ParameterTableRowAttribute("originEquipPro8", index: 23, minimum: -1, maximum: 1E+08, step: 1, sortOrder: 5800, unknown2: 1)]
			[DisplayName("Derive enhancement +8")]
			[Description("Strengthening the original armor ID8 of this armor")]
			[DefaultValue((Int32)(-1))]
			public Int32 OriginEquipPro8 {
				get { return originEquipPro8; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for OriginEquipPro8.");
					SetProperty(ref originEquipPro8, ref value, OriginEquipPro8Property);
				}
			}

			/// <summary>Derive enhancement +9</summary>
			/// <remarks>
			/// Japanese short name: "派生元 強化+9", Google translated: "Derive enhancement +9".
			/// Japanese description: "この防具の強化元防具ID9", Google translated: "Strengthening the original armor ID9 of this armor".
			/// </remarks>
			[ParameterTableRowAttribute("originEquipPro9", index: 24, minimum: -1, maximum: 1E+08, step: 1, sortOrder: 5900, unknown2: 1)]
			[DisplayName("Derive enhancement +9")]
			[Description("Strengthening the original armor ID9 of this armor")]
			[DefaultValue((Int32)(-1))]
			public Int32 OriginEquipPro9 {
				get { return originEquipPro9; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for OriginEquipPro9.");
					SetProperty(ref originEquipPro9, ref value, OriginEquipPro9Property);
				}
			}

			/// <summary>Derive enhancement +10</summary>
			/// <remarks>
			/// Japanese short name: "派生元 強化+10", Google translated: "Derive enhancement +10".
			/// Japanese description: "この防具の強化元防具ID10", Google translated: "Strengthening the original armor ID10 of this armor".
			/// </remarks>
			[ParameterTableRowAttribute("originEquipPro10", index: 25, minimum: -1, maximum: 1E+08, step: 1, sortOrder: 6000, unknown2: 1)]
			[DisplayName("Derive enhancement +10")]
			[Description("Strengthening the original armor ID10 of this armor")]
			[DefaultValue((Int32)(-1))]
			public Int32 OriginEquipPro10 {
				get { return originEquipPro10; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for OriginEquipPro10.");
					SetProperty(ref originEquipPro10, ref value, OriginEquipPro10Property);
				}
			}

			/// <summary>Derive enhancement +11</summary>
			/// <remarks>
			/// Japanese short name: "派生元 強化+11", Google translated: "Derive enhancement +11".
			/// Japanese description: "この防具の強化元防具ID11", Google translated: "Strengthening the original armor ID11 of this armor".
			/// </remarks>
			[ParameterTableRowAttribute("originEquipPro11", index: 26, minimum: -1, maximum: 1E+08, step: 1, sortOrder: 6100, unknown2: 1)]
			[DisplayName("Derive enhancement +11")]
			[Description("Strengthening the original armor ID11 of this armor")]
			[DefaultValue((Int32)(-1))]
			public Int32 OriginEquipPro11 {
				get { return originEquipPro11; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for OriginEquipPro11.");
					SetProperty(ref originEquipPro11, ref value, OriginEquipPro11Property);
				}
			}

			/// <summary>Derive enhancement +12</summary>
			/// <remarks>
			/// Japanese short name: "派生元 強化+12", Google translated: "Derive enhancement +12".
			/// Japanese description: "この防具の強化元防具ID12", Google translated: "Strengthening the original armor ID12 of this armor".
			/// </remarks>
			[ParameterTableRowAttribute("originEquipPro12", index: 27, minimum: -1, maximum: 1E+08, step: 1, sortOrder: 6200, unknown2: 1)]
			[DisplayName("Derive enhancement +12")]
			[Description("Strengthening the original armor ID12 of this armor")]
			[DefaultValue((Int32)(-1))]
			public Int32 OriginEquipPro12 {
				get { return originEquipPro12; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for OriginEquipPro12.");
					SetProperty(ref originEquipPro12, ref value, OriginEquipPro12Property);
				}
			}

			/// <summary>Derive enhancement +13</summary>
			/// <remarks>
			/// Japanese short name: "派生元 強化+13", Google translated: "Derive enhancement +13".
			/// Japanese description: "この防具の強化元防具ID13", Google translated: "Strengthening the original armor ID13 of this armor".
			/// </remarks>
			[ParameterTableRowAttribute("originEquipPro13", index: 28, minimum: -1, maximum: 1E+08, step: 1, sortOrder: 6300, unknown2: 1)]
			[DisplayName("Derive enhancement +13")]
			[Description("Strengthening the original armor ID13 of this armor")]
			[DefaultValue((Int32)(-1))]
			public Int32 OriginEquipPro13 {
				get { return originEquipPro13; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for OriginEquipPro13.");
					SetProperty(ref originEquipPro13, ref value, OriginEquipPro13Property);
				}
			}

			/// <summary>Derive enhancement +14</summary>
			/// <remarks>
			/// Japanese short name: "派生元 強化+14", Google translated: "Derive enhancement +14".
			/// Japanese description: "この防具の強化元防具ID14", Google translated: "Strengthening the original armor ID14 of this armor".
			/// </remarks>
			[ParameterTableRowAttribute("originEquipPro14", index: 29, minimum: -1, maximum: 1E+08, step: 1, sortOrder: 6400, unknown2: 1)]
			[DisplayName("Derive enhancement +14")]
			[Description("Strengthening the original armor ID14 of this armor")]
			[DefaultValue((Int32)(-1))]
			public Int32 OriginEquipPro14 {
				get { return originEquipPro14; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for OriginEquipPro14.");
					SetProperty(ref originEquipPro14, ref value, OriginEquipPro14Property);
				}
			}

			/// <summary>Derive enhancement +15</summary>
			/// <remarks>
			/// Japanese short name: "派生元 強化+15", Google translated: "Derive enhancement +15".
			/// Japanese description: "この防具の強化元防具ID15", Google translated: "Strengthening the original armor ID15 of this armor".
			/// </remarks>
			[ParameterTableRowAttribute("originEquipPro15", index: 30, minimum: -1, maximum: 1E+08, step: 1, sortOrder: 6500, unknown2: 1)]
			[DisplayName("Derive enhancement +15")]
			[Description("Strengthening the original armor ID15 of this armor")]
			[DefaultValue((Int32)(-1))]
			public Int32 OriginEquipPro15 {
				get { return originEquipPro15; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for OriginEquipPro15.");
					SetProperty(ref originEquipPro15, ref value, OriginEquipPro15Property);
				}
			}

			/// <summary>Man profile scale expansion</summary>
			/// <remarks>
			/// Japanese short name: "男横顔拡大スケール", Google translated: "Man profile scale expansion".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("faceScaleM_ScaleX", index: 31, minimum: 0, maximum: 5, step: 0.01, sortOrder: 9900, unknown2: 1)]
			[DisplayName("Man profile scale expansion")]
			[Description("")]
			[DefaultValue((Single)1)]
			public Single FaceScaleM_ScaleX {
				get { return faceScaleM_ScaleX; }
				set {
					if ((double)value < 0 || (double)value > 5)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 5 for FaceScaleM_ScaleX.");
					SetProperty(ref faceScaleM_ScaleX, ref value, FaceScaleM_ScaleXProperty);
				}
			}

			/// <summary>Handsome face larger scale</summary>
			/// <remarks>
			/// Japanese short name: "男前顔拡大スケール", Google translated: "Handsome face larger scale".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("faceScaleM_ScaleZ", index: 32, minimum: 0, maximum: 5, step: 0.01, sortOrder: 10000, unknown2: 1)]
			[DisplayName("Handsome face larger scale")]
			[Description("")]
			[DefaultValue((Single)1)]
			public Single FaceScaleM_ScaleZ {
				get { return faceScaleM_ScaleZ; }
				set {
					if ((double)value < 0 || (double)value > 5)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 5 for FaceScaleM_ScaleZ.");
					SetProperty(ref faceScaleM_ScaleZ, ref value, FaceScaleM_ScaleZProperty);
				}
			}

			/// <summary>Man profile expansion maximum magnification</summary>
			/// <remarks>
			/// Japanese short name: "男横顔拡大最大倍率", Google translated: "Man profile expansion maximum magnification".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("faceScaleM_MaxX", index: 33, minimum: 1, maximum: 2, step: 0.01, sortOrder: 10100, unknown2: 1)]
			[DisplayName("Man profile expansion maximum magnification")]
			[Description("")]
			[DefaultValue((Single)1)]
			public Single FaceScaleM_MaxX {
				get { return faceScaleM_MaxX; }
				set {
					if ((double)value < 1 || (double)value > 2)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 1 to 2 for FaceScaleM_MaxX.");
					SetProperty(ref faceScaleM_MaxX, ref value, FaceScaleM_MaxXProperty);
				}
			}

			/// <summary>Handsome face expansion maximum magnification</summary>
			/// <remarks>
			/// Japanese short name: "男前顔拡大最大倍率", Google translated: "Handsome face expansion maximum magnification".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("faceScaleM_MaxZ", index: 34, minimum: 1, maximum: 2, step: 0.01, sortOrder: 10200, unknown2: 1)]
			[DisplayName("Handsome face expansion maximum magnification")]
			[Description("")]
			[DefaultValue((Single)1)]
			public Single FaceScaleM_MaxZ {
				get { return faceScaleM_MaxZ; }
				set {
					if ((double)value < 1 || (double)value > 2)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 1 to 2 for FaceScaleM_MaxZ.");
					SetProperty(ref faceScaleM_MaxZ, ref value, FaceScaleM_MaxZProperty);
				}
			}

			/// <summary>Woman profile scale expansion</summary>
			/// <remarks>
			/// Japanese short name: "女横顔拡大スケール", Google translated: "Woman profile scale expansion".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("faceScaleF_ScaleX", index: 35, minimum: 0, maximum: 5, step: 0.01, sortOrder: 10300, unknown2: 1)]
			[DisplayName("Woman profile scale expansion")]
			[Description("")]
			[DefaultValue((Single)1)]
			public Single FaceScaleF_ScaleX {
				get { return faceScaleF_ScaleX; }
				set {
					if ((double)value < 0 || (double)value > 5)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 5 for FaceScaleF_ScaleX.");
					SetProperty(ref faceScaleF_ScaleX, ref value, FaceScaleF_ScaleXProperty);
				}
			}

			/// <summary>Woman before the enlarged face scale</summary>
			/// <remarks>
			/// Japanese short name: "女前顔拡大スケール", Google translated: "Woman before the enlarged face scale".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("faceScaleF_ScaleZ", index: 36, minimum: 0, maximum: 5, step: 0.01, sortOrder: 10400, unknown2: 1)]
			[DisplayName("Woman before the enlarged face scale")]
			[Description("")]
			[DefaultValue((Single)1)]
			public Single FaceScaleF_ScaleZ {
				get { return faceScaleF_ScaleZ; }
				set {
					if ((double)value < 0 || (double)value > 5)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 5 for FaceScaleF_ScaleZ.");
					SetProperty(ref faceScaleF_ScaleZ, ref value, FaceScaleF_ScaleZProperty);
				}
			}

			/// <summary>Woman profile expansion maximum magnification</summary>
			/// <remarks>
			/// Japanese short name: "女横顔拡大最大倍率", Google translated: "Woman profile expansion maximum magnification".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("faceScaleF_MaxX", index: 37, minimum: 1, maximum: 2, step: 0.01, sortOrder: 10500, unknown2: 1)]
			[DisplayName("Woman profile expansion maximum magnification")]
			[Description("")]
			[DefaultValue((Single)1)]
			public Single FaceScaleF_MaxX {
				get { return faceScaleF_MaxX; }
				set {
					if ((double)value < 1 || (double)value > 2)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 1 to 2 for FaceScaleF_MaxX.");
					SetProperty(ref faceScaleF_MaxX, ref value, FaceScaleF_MaxXProperty);
				}
			}

			/// <summary>Woman before the enlarged face maximum magnification</summary>
			/// <remarks>
			/// Japanese short name: "女前顔拡大最大倍率", Google translated: "Woman before the enlarged face maximum magnification".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("faceScaleF_MaxZ", index: 38, minimum: 1, maximum: 2, step: 0.01, sortOrder: 10600, unknown2: 1)]
			[DisplayName("Woman before the enlarged face maximum magnification")]
			[Description("")]
			[DefaultValue((Single)1)]
			public Single FaceScaleF_MaxZ {
				get { return faceScaleF_MaxZ; }
				set {
					if ((double)value < 1 || (double)value > 2)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 1 to 2 for FaceScaleF_MaxZ.");
					SetProperty(ref faceScaleF_MaxZ, ref value, FaceScaleF_MaxZProperty);
				}
			}

			/// <summary>QWCID</summary>
			/// <remarks>
			/// Japanese short name: "QWCID", Google translated: "QWCID".
			/// Japanese description: "QWCのパラメタID", Google translated: "ID parameter of QWC".
			/// </remarks>
			[ParameterTableRowAttribute("qwcId", index: 39, minimum: -1, maximum: 1E+08, step: 1, sortOrder: 10800, unknown2: 1)]
			[DisplayName("QWCID")]
			[Description("ID parameter of QWC")]
			[DefaultValue((Int32)(-1))]
			public Int32 QwcId {
				get { return qwcId; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for QwcId.");
					SetProperty(ref qwcId, ref value, QwcIdProperty);
				}
			}

			/// <summary>Man icon ID</summary>
			/// <remarks>
			/// Japanese short name: "男用アイコンID", Google translated: "Man icon ID".
			/// Japanese description: "男用メニューアイコンID.", Google translated: "Man 's menu icon ID.".
			/// </remarks>
			[ParameterTableRowAttribute("iconIdM", index: 41, minimum: 0, maximum: 9999, step: 1, sortOrder: 1100, unknown2: 1)]
			[DisplayName("Man icon ID")]
			[Description("Man 's menu icon ID.")]
			[DefaultValue((UInt16)0)]
			public UInt16 IconIdM {
				get { return iconIdM; }
				set {
					if ((double)value < 0 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9999 for IconIdM.");
					SetProperty(ref iconIdM, ref value, IconIdMProperty);
				}
			}

			/// <summary>Woman icon ID</summary>
			/// <remarks>
			/// Japanese short name: "女用アイコンID", Google translated: "Woman icon ID".
			/// Japanese description: "女用メニューアイコンID.", Google translated: "Woman 's menu icon ID.".
			/// </remarks>
			[ParameterTableRowAttribute("iconIdF", index: 42, minimum: 0, maximum: 9999, step: 1, sortOrder: 1200, unknown2: 1)]
			[DisplayName("Woman icon ID")]
			[Description("Woman 's menu icon ID.")]
			[DefaultValue((UInt16)0)]
			public UInt16 IconIdF {
				get { return iconIdF; }
				set {
					if ((double)value < 0 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9999 for IconIdF.");
					SetProperty(ref iconIdF, ref value, IconIdFProperty);
				}
			}

			/// <summary>Knock back rate cut</summary>
			/// <remarks>
			/// Japanese short name: "ノックバックカット率", Google translated: "Knock back rate cut".
			/// Japanese description: "ノックバックの減少値.", Google translated: "Decrease in value of knock back .".
			/// </remarks>
			[ParameterTableRowAttribute("knockBack", index: 43, minimum: 0, maximum: 100, step: 1, sortOrder: 2200, unknown2: 1)]
			[DisplayName("Knock back rate cut")]
			[Description("Decrease in value of knock back .")]
			[DefaultValue((UInt16)0)]
			public UInt16 KnockBack {
				get { return knockBack; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for KnockBack.");
					SetProperty(ref knockBack, ref value, KnockBackProperty);
				}
			}

			/// <summary>Knock back rebound rate</summary>
			/// <remarks>
			/// Japanese short name: "ノックバック反発率", Google translated: "Knock back rebound rate".
			/// Japanese description: "ノックバックの反発率.", Google translated: "Rebound rate of knock back .".
			/// </remarks>
			[ParameterTableRowAttribute("knockbackBounceRate", index: 44, minimum: 0, maximum: 100, step: 1, sortOrder: 2300, unknown2: 1)]
			[DisplayName("Knock back rebound rate")]
			[Description("Rebound rate of knock back .")]
			[DefaultValue((UInt16)0)]
			public UInt16 KnockbackBounceRate {
				get { return knockbackBounceRate; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for KnockbackBounceRate.");
					SetProperty(ref knockbackBounceRate, ref value, KnockbackBounceRateProperty);
				}
			}

			/// <summary>SA Durability</summary>
			/// <remarks>
			/// Japanese short name: "SA耐久値", Google translated: "SA Durability".
			/// Japanese description: "スーパーアーマー耐久力", Google translated: "Super armor durability".
			/// </remarks>
			[ParameterTableRowAttribute("saDurability", index: 47, minimum: -1, maximum: 9999, step: 1, sortOrder: 3000, unknown2: 1)]
			[DisplayName("SA Durability")]
			[Description("Super armor durability")]
			[DefaultValue((Int16)0)]
			public Int16 SaDurability {
				get { return saDurability; }
				set {
					if ((double)value < -1 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 9999 for SaDurability.");
					SetProperty(ref saDurability, ref value, SaDurabilityProperty);
				}
			}

			/// <summary>Repelling Defense</summary>
			/// <remarks>
			/// Maximum was originally 99.
			/// 
			/// Japanese short name: "はじき防御力", Google translated: "Repelling Defense".
			/// Japanese description: "敵の攻撃のはじき返し判定に利用.", Google translated: "It used to determine deflected the attack of the enemy .".
			/// </remarks>
			[ParameterTableRowAttribute("defFlickPower", index: 48, minimum: DefFlickPowerMinimum, maximum: DefFlickPowerMaximum, step: 1, sortOrder: 3200, unknown2: 1)]
			[DisplayName("Repelling Defense")]
			[Description("It used to determine deflected the attack of the enemy .")]
			[DefaultValue((UInt16)0)]
			public UInt16 DefFlickPower {
				get { return defFlickPower; }
				set {
					if ((double)value < DefFlickPowerMinimum || (double)value > DefFlickPowerMaximum)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range " + DefFlickPowerMinimum + " to " + DefFlickPowerMaximum + " for " + DefFlickPowerProperty.Name + ".");
					SetProperty(ref defFlickPower, ref value, DefFlickPowerProperty);
				}
			}
			const double DefFlickPowerDefault = 0, DefFlickPowerMinimum = 0, DefFlickPowerMaximum = 900;

			/// <summary>Physical Def</summary>
			/// <remarks>
			/// Japanese short name: "物理防御力", Google translated: "Physical Def".
			/// Japanese description: "物理攻撃のダメージ防御.", Google translated: "Damage defense of physical attack .".
			/// </remarks>
			[ParameterTableRowAttribute("defensePhysics", index: 49, minimum: 0, maximum: 9999, step: 1, sortOrder: 3400, unknown2: 1)]
			[DisplayName("Physical Def")]
			[Description("Damage defense of physical attack .")]
			[DefaultValue((UInt16)100)]
			public UInt16 DefensePhysics {
				get { return defensePhysics; }
				set {
					if ((double)value < 0 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9999 for DefensePhysics.");
					SetProperty(ref defensePhysics, ref value, DefensePhysicsProperty);
				}
			}

			/// <summary>Magic defense</summary>
			/// <remarks>
			/// Japanese short name: "魔法防御力", Google translated: "Magic defense".
			/// Japanese description: "魔法攻撃のダメージ防御.", Google translated: "Damage defense magic attack .".
			/// </remarks>
			[ParameterTableRowAttribute("defenseMagic", index: 50, minimum: 0, maximum: 9999, step: 1, sortOrder: 3500, unknown2: 1)]
			[DisplayName("Magic defense")]
			[Description("Damage defense magic attack .")]
			[DefaultValue((UInt16)100)]
			public UInt16 DefenseMagic {
				get { return defenseMagic; }
				set {
					if ((double)value < 0 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9999 for DefenseMagic.");
					SetProperty(ref defenseMagic, ref value, DefenseMagicProperty);
				}
			}

			/// <summary>Flame Defense</summary>
			/// <remarks>
			/// Japanese short name: "炎防御力", Google translated: "Flame Defense".
			/// Japanese description: "炎攻撃のダメージ防御.", Google translated: "Damage defense of fire attack .".
			/// </remarks>
			[ParameterTableRowAttribute("defenseFire", index: 51, minimum: 0, maximum: 9999, step: 1, sortOrder: 3600, unknown2: 1)]
			[DisplayName("Flame Defense")]
			[Description("Damage defense of fire attack .")]
			[DefaultValue((UInt16)100)]
			public UInt16 DefenseFire {
				get { return defenseFire; }
				set {
					if ((double)value < 0 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9999 for DefenseFire.");
					SetProperty(ref defenseFire, ref value, DefenseFireProperty);
				}
			}

			/// <summary>Blitz Defense</summary>
			/// <remarks>
			/// Japanese short name: "電撃防御力", Google translated: "Blitz Defense".
			/// Japanese description: "電撃攻撃のダメージ防御.", Google translated: "Damage defense of blitz attack .".
			/// </remarks>
			[ParameterTableRowAttribute("defenseThunder", index: 52, minimum: 0, maximum: 9999, step: 1, sortOrder: 3700, unknown2: 1)]
			[DisplayName("Blitz Defense")]
			[Description("Damage defense of blitz attack .")]
			[DefaultValue((UInt16)100)]
			public UInt16 DefenseThunder {
				get { return defenseThunder; }
				set {
					if ((double)value < 0 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9999 for DefenseThunder.");
					SetProperty(ref defenseThunder, ref value, DefenseThunderProperty);
				}
			}

			/// <summary>Slashing Defense</summary>
			/// <remarks>
			/// Japanese short name: "斬撃防御力", Google translated: "Slashing Defense".
			/// Japanese description: "攻撃タイプを見て、斬撃属性のときは、防御力を減少させる", Google translated: "Look at the type of attack , when the撃属of Zan , to reduce the defense force".
			/// </remarks>
			[ParameterTableRowAttribute("defenseSlash", index: 53, minimum: -100, maximum: 9999, step: 1, sortOrder: 3800, unknown2: 1)]
			[DisplayName("Slashing Defense")]
			[Description("Look at the type of attack , when the撃属of Zan , to reduce the defense force")]
			[DefaultValue((Int16)0)]
			public Int16 DefenseSlash {
				get { return defenseSlash; }
				set {
					if ((double)value < -100 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -100 to 9999 for DefenseSlash.");
					SetProperty(ref defenseSlash, ref value, DefenseSlashProperty);
				}
			}

			/// <summary>Blow Defense</summary>
			/// <remarks>
			/// Japanese short name: "打撃防御力", Google translated: "Blow Defense".
			/// Japanese description: "攻撃属性を見て、打撃属性のときは、防御力を減少させる.", Google translated: "Looking at the attack attribute , when the stroke attribute , to reduce the defense .".
			/// </remarks>
			[ParameterTableRowAttribute("defenseBlow", index: 54, minimum: -100, maximum: 9999, step: 1, sortOrder: 3900, unknown2: 1)]
			[DisplayName("Blow Defense")]
			[Description("Looking at the attack attribute , when the stroke attribute , to reduce the defense .")]
			[DefaultValue((Int16)0)]
			public Int16 DefenseBlow {
				get { return defenseBlow; }
				set {
					if ((double)value < -100 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -100 to 9999 for DefenseBlow.");
					SetProperty(ref defenseBlow, ref value, DefenseBlowProperty);
				}
			}

			/// <summary>Piercing Defense</summary>
			/// <remarks>
			/// Japanese short name: "刺突防御力", Google translated: "Piercing Defense".
			/// Japanese description: "攻撃属性を見て、打撃属性のときは、防御力を減少させる.", Google translated: "Looking at the attack attribute , when the stroke attribute , to reduce the defense .".
			/// </remarks>
			[ParameterTableRowAttribute("defenseThrust", index: 55, minimum: -100, maximum: 9999, step: 1, sortOrder: 4000, unknown2: 1)]
			[DisplayName("Piercing Defense")]
			[Description("Looking at the attack attribute , when the stroke attribute , to reduce the defense .")]
			[DefaultValue((Int16)0)]
			public Int16 DefenseThrust {
				get { return defenseThrust; }
				set {
					if ((double)value < -100 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -100 to 9999 for DefenseThrust.");
					SetProperty(ref defenseThrust, ref value, DefenseThrustProperty);
				}
			}

			/// <summary>Poison Resistance</summary>
			/// <remarks>
			/// Maximum was 999.
			/// 
			/// Japanese short name: "毒耐性", Google translated: "Poison Resistance".
			/// Japanese description: "毒状態異常へのかかりにくさ", Google translated: "The difficulty takes to poison the abnormal state".
			/// </remarks>
			[ParameterTableRowAttribute("resistPoison", index: 56, minimum: 0, maximum: 65535, step: 1, sortOrder: 4100, unknown2: 1)]
			[DisplayName("Poison Resistance")]
			[Description("The difficulty takes to poison the abnormal state")]
			[DefaultValue((UInt16)100)]
			public UInt16 ResistPoison {
				get { return resistPoison; }
				set { SetProperty(ref resistPoison, ref value, ResistPoisonProperty); }
			}

			/// <summary>Plague -resistant</summary>
			/// <remarks>
			/// Maximum was originally 999.
			/// 
			/// Japanese short name: "疫病耐性", Google translated: "Plague -resistant".
			/// Japanese description: "疫病状態異常へのかかりにくさ", Google translated: "The difficulty to plague much of the abnormal state".
			/// </remarks>
			[ParameterTableRowAttribute("resistDisease", index: 57, minimum: 0, maximum: 65535, step: 1, sortOrder: 4200, unknown2: 1)]
			[DisplayName("Plague -resistant")]
			[Description("The difficulty to plague much of the abnormal state")]
			[DefaultValue((UInt16)100)]
			public UInt16 ResistDisease {
				get { return resistDisease; }
				set { SetProperty(ref resistDisease, ref value, ResistDiseaseProperty); }
			}

			/// <summary>Bleeding -resistant</summary>
			/// <remarks>
			/// Japanese short name: "出血耐性", Google translated: "Bleeding -resistant".
			/// Japanese description: "出血状態異常へのかかりにくさ", Google translated: "The difficulty takes to abnormal bleeding".
			/// </remarks>
			[ParameterTableRowAttribute("resistBlood", index: 58, minimum: 0, maximum: 999, step: 1, sortOrder: 4300, unknown2: 1)]
			[DisplayName("Bleeding -resistant")]
			[Description("The difficulty takes to abnormal bleeding")]
			[DefaultValue((UInt16)100)]
			public UInt16 ResistBlood {
				get { return resistBlood; }
				set {
					if ((double)value < 0 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for ResistBlood.");
					SetProperty(ref resistBlood, ref value, ResistBloodProperty);
				}
			}

			/// <summary>Curse resistance</summary>
			/// <remarks>
			/// Japanese short name: "呪耐性", Google translated: "Curse resistance".
			/// Japanese description: "呪い状態異常へのかかりにくさ", Google translated: "The difficulty takes to curse abnormal state".
			/// </remarks>
			[ParameterTableRowAttribute("resistCurse", index: 59, minimum: 0, maximum: 999, step: 1, sortOrder: 4400, unknown2: 1)]
			[DisplayName("Curse resistance")]
			[Description("The difficulty takes to curse abnormal state")]
			[DefaultValue((UInt16)100)]
			public UInt16 ResistCurse {
				get { return resistCurse; }
				set {
					if ((double)value < 0 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for ResistCurse.");
					SetProperty(ref resistCurse, ref value, ResistCurseProperty);
				}
			}

			/// <summary>Reinforced type ID</summary>
			/// <remarks>
			/// Japanese short name: "強化タイプID", Google translated: "Reinforced type ID".
			/// Japanese description: "強化タイプID", Google translated: "Reinforced type ID".
			/// </remarks>
			[ParameterTableRowAttribute("reinforceTypeId", index: 60, minimum: 0, maximum: 9999, step: 1, sortOrder: 4900, unknown2: 1)]
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

			/// <summary>Trophy</summary>
			/// <remarks>
			/// Japanese short name: "トロフィー", Google translated: "Trophy".
			/// Japanese description: "トロフィーシステムに関係あるか？", Google translated: "Is there related to the trophy system ?".
			/// </remarks>
			[ParameterTableRowAttribute("trophySGradeId", index: 61, minimum: -1, maximum: 9999, step: 1, sortOrder: 4750, unknown2: 1)]
			[DisplayName("Trophy")]
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

			/// <summary>Shop level</summary>
			/// <remarks>
			/// Japanese short name: "ショップレベル", Google translated: "Shop level".
			/// Japanese description: "お店で販売できるレベル", Google translated: "Level that can be sold in the shop".
			/// </remarks>
			[ParameterTableRowAttribute("shopLv", index: 62, minimum: -1, maximum: 9999, step: 1, sortOrder: 10900, unknown2: 1)]
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

			/// <summary>Knock back parameter ID</summary>
			/// <remarks>
			/// Japanese short name: "ノックバックパラメータID", Google translated: "Knock back parameter ID".
			/// Japanese description: "ノックバックで使用するパラメータのID", Google translated: "ID of the parameter used in the knock- back".
			/// </remarks>
			[ParameterTableRowAttribute("knockbackParamId", index: 63, minimum: 0, maximum: 255, step: 1, sortOrder: 2400, unknown2: 1)]
			[DisplayName("Knock back parameter ID")]
			[Description("ID of the parameter used in the knock- back")]
			[DefaultValue((Byte)0)]
			public Byte KnockbackParamId {
				get { return knockbackParamId; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for KnockbackParamId.");
					SetProperty(ref knockbackParamId, ref value, KnockbackParamIdProperty);
				}
			}

			/// <summary>Repelling when damage attenuation rate [ %]</summary>
			/// <remarks>
			/// Japanese short name: "はじき時ダメージ減衰率[%]", Google translated: "Repelling when damage attenuation rate [ %]".
			/// Japanese description: "はじき時のダメージ減衰率に使用", Google translated: "Used for damage attenuation rate at the time of repelling".
			/// </remarks>
			[ParameterTableRowAttribute("flickDamageCutRate", index: 64, minimum: 0, maximum: 255, step: 1, sortOrder: 3300, unknown2: 1)]
			[DisplayName("Repelling when damage attenuation rate [ %]")]
			[Description("Used for damage attenuation rate at the time of repelling")]
			[DefaultValue((Byte)0)]
			public Byte FlickDamageCutRate {
				get { return flickDamageCutRate; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for " + FlickDamageCutRateProperty + ".");
					SetProperty(ref flickDamageCutRate, ref value, FlickDamageCutRateProperty);
				}
			}

			/// <summary>Armor category</summary>
			/// <remarks>
			/// Japanese short name: "防具カテゴリ", Google translated: "Armor category".
			/// Japanese description: "防具のカテゴリ.", Google translated: "Category of armor .".
			/// </remarks>
			[ParameterTableRowAttribute("protectorCategory", index: 67, minimum: 0, maximum: 99, step: 1, sortOrder: 400, unknown2: 1)]
			[DisplayName("Armor category")]
			[Description("Category of armor .")]
			[DefaultValue((ArmorCategory)0)]
			public ArmorCategory ProtectorCategory {
				get { return protectorCategory; }
				set { SetProperty(ref protectorCategory, ref value, ProtectorCategoryProperty); }
			}

			/// <summary>Defense material [ SE ]</summary>
			/// <remarks>
			/// Japanese short name: "防御材質【SE】", Google translated: "Defense material [ SE ]".
			/// Japanese description: "移動/防御時のSE用.", Google translated: "SE for move / defending .".
			/// </remarks>
			[ParameterTableRowAttribute("defenseMaterial", index: 68, minimum: 0, maximum: 99, step: 1, sortOrder: 900, unknown2: 1)]
			[DisplayName("Defense material [ SE ]")]
			[Description("SE for move / defending .")]
			[DefaultValue((WeaponMaterialDefend)50)]
			public WeaponMaterialDefend DefenseMaterial {
				get { return defenseMaterial; }
				set { SetProperty(ref defenseMaterial, ref value, DefenseMaterialProperty); }
			}

			/// <summary>Defense material [ SFX ]</summary>
			/// <remarks>
			/// Japanese short name: "防御材質【SFX】", Google translated: "Defense material [ SFX ]".
			/// Japanese description: "移動/防御時のSFX用.", Google translated: "SFX for move / defending .".
			/// </remarks>
			[ParameterTableRowAttribute("defenseMaterialSfx", index: 69, minimum: 0, maximum: 99, step: 1, sortOrder: 1000, unknown2: 1)]
			[DisplayName("Defense material [ SFX ]")]
			[Description("SFX for move / defending .")]
			[DefaultValue((WeaponMaterialDefendSound)50)]
			public WeaponMaterialDefendSound DefenseMaterialSfx {
				get { return defenseMaterialSfx; }
				set { SetProperty(ref defenseMaterialSfx, ref value, DefenseMaterialSfxProperty); }
			}

			/// <summary>Application site damage attack</summary>
			/// <remarks>
			/// Japanese short name: "部位ダメージ適用攻撃", Google translated: "Application site damage attack".
			/// Japanese description: "部位ダメージ判定を行う攻撃タイプを設定", Google translated: "Set the type of attack to do damage site decision".
			/// </remarks>
			[ParameterTableRowAttribute("partsDmgType", index: 70, minimum: 0, maximum: 2, step: 1, sortOrder: 1800, unknown2: 1)]
			[DisplayName("Application site damage attack")]
			[Description("Set the type of attack to do damage site decision")]
			[DefaultValue((AttackParameterPartDamageType)0)]
			public AttackParameterPartDamageType PartsDmgType {
				get { return partsDmgType; }
				set { SetProperty(ref partsDmgType, ref value, PartsDmgTypeProperty); }
			}

			/// <summary>Weakness defense material [ SE ]</summary>
			/// <remarks>
			/// Japanese short name: "弱点防御材質【SE】", Google translated: "Weakness defense material [ SE ]".
			/// Japanese description: "弱点部位ダメージ時のSE用", Google translated: "SE for weakness site damage during".
			/// </remarks>
			[ParameterTableRowAttribute("defenseMaterial_Weak", index: 71, minimum: 0, maximum: 99, step: 1, sortOrder: 2000, unknown2: 1)]
			[DisplayName("Weakness defense material [ SE ]")]
			[Description("SE for weakness site damage during")]
			[DefaultValue((WeaponMaterialDefend)50)]
			public WeaponMaterialDefend DefenseMaterial_Weak {
				get { return defenseMaterial_Weak; }
				set { SetProperty(ref defenseMaterial_Weak, ref value, DefenseMaterial_WeakProperty); }
			}

			/// <summary>Weakness defense material [ SFX ]</summary>
			/// <remarks>
			/// Japanese short name: "弱点防御材質【SFX】", Google translated: "Weakness defense material [ SFX ]".
			/// Japanese description: "弱点部位ダメージ時のSFX用", Google translated: "SFX for weakness site damage during".
			/// </remarks>
			[ParameterTableRowAttribute("defenseMaterialSfx_Weak", index: 72, minimum: 0, maximum: 99, step: 1, sortOrder: 2100, unknown2: 1)]
			[DisplayName("Weakness defense material [ SFX ]")]
			[Description("SFX for weakness site damage during")]
			[DefaultValue((WeaponMaterialDefendSound)50)]
			public WeaponMaterialDefendSound DefenseMaterialSfx_Weak {
				get { return defenseMaterialSfx_Weak; }
				set { SetProperty(ref defenseMaterialSfx_Weak, ref value, DefenseMaterialSfx_WeakProperty); }
			}

			/// <summary>Headgears</summary>
			/// <remarks>
			/// Japanese short name: "頭装備", Google translated: "Headgears".
			/// Japanese description: "頭装備か.", Google translated: "Head or equipment .".
			/// </remarks>
			[ParameterTableRowAttribute("headEquip:1", index: 74, minimum: 0, maximum: 1, step: 1, sortOrder: 500, unknown2: 1)]
			[DisplayName("Headgears")]
			[Description("Head or equipment .")]
			[DefaultValue(true)]
			public Boolean HeadEquip {
				get { return GetBitProperty(1, 1, HeadEquipProperty) != 0; }
				set { SetBitProperty(1, 1, value ? 1 : 0, HeadEquipProperty); }
			}

			/// <summary>Trunk equipment</summary>
			/// <remarks>
			/// Japanese short name: "胴装備", Google translated: "Trunk equipment".
			/// Japanese description: "胴装備か.", Google translated: "Whether equipment .".
			/// </remarks>
			[ParameterTableRowAttribute("bodyEquip:1", index: 75, minimum: 0, maximum: 1, step: 1, sortOrder: 600, unknown2: 1)]
			[DisplayName("Trunk equipment")]
			[Description("Whether equipment .")]
			[DefaultValue(false)]
			public Boolean BodyEquip {
				get { return GetBitProperty(2, 1, BodyEquipProperty) != 0; }
				set { SetBitProperty(2, 1, value ? 1 : 0, BodyEquipProperty); }
			}

			/// <summary>Arm equipment</summary>
			/// <remarks>
			/// Japanese short name: "腕装備", Google translated: "Arm equipment".
			/// Japanese description: "腕装備か.", Google translated: "Arm or equipment .".
			/// </remarks>
			[ParameterTableRowAttribute("armEquip:1", index: 76, minimum: 0, maximum: 1, step: 1, sortOrder: 700, unknown2: 1)]
			[DisplayName("Arm equipment")]
			[Description("Arm or equipment .")]
			[DefaultValue(false)]
			public Boolean ArmEquip {
				get { return GetBitProperty(3, 1, ArmEquipProperty) != 0; }
				set { SetBitProperty(3, 1, value ? 1 : 0, ArmEquipProperty); }
			}

			/// <summary>Leg equipment</summary>
			/// <remarks>
			/// Japanese short name: "脚装備", Google translated: "Leg equipment".
			/// Japanese description: "脚装備か.", Google translated: "Equipment or legs .".
			/// </remarks>
			[ParameterTableRowAttribute("legEquip:1", index: 77, minimum: 0, maximum: 1, step: 1, sortOrder: 800, unknown2: 1)]
			[DisplayName("Leg equipment")]
			[Description("Equipment or legs .")]
			[DefaultValue(false)]
			public Boolean LegEquip {
				get { return GetBitProperty(4, 1, LegEquipProperty) != 0; }
				set { SetBitProperty(4, 1, value ? 1 : 0, LegEquipProperty); }
			}

			/// <summary>You can use the face scale</summary>
			/// <remarks>
			/// Japanese short name: "顔スケールを使用するか", Google translated: "You can use the face scale".
			/// Japanese description: "顔スケールを使用するか", Google translated: "You can use the face scale".
			/// </remarks>
			[ParameterTableRowAttribute("useFaceScale:1", index: 78, minimum: 0, maximum: 1, step: 1, sortOrder: 6600, unknown2: 1)]
			[DisplayName("You can use the face scale")]
			[Description("You can use the face scale")]
			[DefaultValue(false)]
			public Boolean UseFaceScale {
				get { return GetBitProperty(5, 1, UseFaceScaleProperty) != 0; }
				set { SetBitProperty(5, 1, value ? 1 : 0, UseFaceScaleProperty); }
			}

			/// <summary># 00 # hide</summary>
			/// <remarks>
			/// Japanese short name: "#00#非表示", Google translated: "# 00 # hide".
			/// Japanese description: "前髪の先", Google translated: "Tip of bangs".
			/// </remarks>
			[ParameterTableRowAttribute("invisibleFlag00:1", index: 79, minimum: 0, maximum: 1, step: 1, sortOrder: 8000, unknown2: 1)]
			[DisplayName("# 00 # hide")]
			[Description("Tip of bangs")]
			[DefaultValue(false)]
			public Boolean InvisibleFlag00 {
				get { return GetBitProperty(6, 1, InvisibleFlag00Property) != 0; }
				set { SetBitProperty(6, 1, value ? 1 : 0, InvisibleFlag00Property); }
			}

			/// <summary># 01 # hide</summary>
			/// <remarks>
			/// Japanese short name: "#01#非表示", Google translated: "# 01 # hide".
			/// Japanese description: "前髪の根元", Google translated: "The base of the bangs".
			/// </remarks>
			[ParameterTableRowAttribute("invisibleFlag01:1", index: 80, minimum: 0, maximum: 1, step: 1, sortOrder: 8010, unknown2: 1)]
			[DisplayName("# 01 # hide")]
			[Description("The base of the bangs")]
			[DefaultValue(false)]
			public Boolean InvisibleFlag01 {
				get { return GetBitProperty(7, 1, InvisibleFlag01Property) != 0; }
				set { SetBitProperty(7, 1, value ? 1 : 0, InvisibleFlag01Property); }
			}

			/// <summary># 02 # hide</summary>
			/// <remarks>
			/// Japanese short name: "#02#非表示", Google translated: "# 02 # hide".
			/// Japanese description: "もみあげ", Google translated: "Sideburn".
			/// </remarks>
			[ParameterTableRowAttribute("invisibleFlag02:1", index: 81, minimum: 0, maximum: 1, step: 1, sortOrder: 8020, unknown2: 1)]
			[DisplayName("# 02 # hide")]
			[Description("Sideburn")]
			[DefaultValue(false)]
			public Boolean InvisibleFlag02 {
				get { return GetBitProperty(8, 1, InvisibleFlag02Property) != 0; }
				set { SetBitProperty(8, 1, value ? 1 : 0, InvisibleFlag02Property); }
			}

			/// <summary># 03 # hide</summary>
			/// <remarks>
			/// Japanese short name: "#03#非表示", Google translated: "# 03 # hide".
			/// Japanese description: "頭頂部", Google translated: "Parietal".
			/// </remarks>
			[ParameterTableRowAttribute("invisibleFlag03:1", index: 82, minimum: 0, maximum: 1, step: 1, sortOrder: 8030, unknown2: 1)]
			[DisplayName("# 03 # hide")]
			[Description("Parietal")]
			[DefaultValue(false)]
			public Boolean InvisibleFlag03 {
				get { return GetBitProperty(9, 1, InvisibleFlag03Property) != 0; }
				set { SetBitProperty(9, 1, value ? 1 : 0, InvisibleFlag03Property); }
			}

			/// <summary># 04 # hide</summary>
			/// <remarks>
			/// Japanese short name: "#04#非表示", Google translated: "# 04 # hide".
			/// Japanese description: "頭頂部", Google translated: "Parietal".
			/// </remarks>
			[ParameterTableRowAttribute("invisibleFlag04:1", index: 83, minimum: 0, maximum: 1, step: 1, sortOrder: 8040, unknown2: 1)]
			[DisplayName("# 04 # hide")]
			[Description("Parietal")]
			[DefaultValue(false)]
			public Boolean InvisibleFlag04 {
				get { return GetBitProperty(10, 1, InvisibleFlag04Property) != 0; }
				set { SetBitProperty(10, 1, value ? 1 : 0, InvisibleFlag04Property); }
			}

			/// <summary># 05 # hide</summary>
			/// <remarks>
			/// Japanese short name: "#05#非表示", Google translated: "# 05 # hide".
			/// Japanese description: "後ろ髪", Google translated: "Back hair".
			/// </remarks>
			[ParameterTableRowAttribute("invisibleFlag05:1", index: 84, minimum: 0, maximum: 1, step: 1, sortOrder: 8050, unknown2: 1)]
			[DisplayName("# 05 # hide")]
			[Description("Back hair")]
			[DefaultValue(false)]
			public Boolean InvisibleFlag05 {
				get { return GetBitProperty(11, 1, InvisibleFlag05Property) != 0; }
				set { SetBitProperty(11, 1, value ? 1 : 0, InvisibleFlag05Property); }
			}

			/// <summary># 06 # hide</summary>
			/// <remarks>
			/// Japanese short name: "#06#非表示", Google translated: "# 06 # hide".
			/// Japanese description: "後ろ髪の先", Google translated: "Tip of back hair".
			/// </remarks>
			[ParameterTableRowAttribute("invisibleFlag06:1", index: 85, minimum: 0, maximum: 1, step: 1, sortOrder: 8060, unknown2: 1)]
			[DisplayName("# 06 # hide")]
			[Description("Tip of back hair")]
			[DefaultValue(false)]
			public Boolean InvisibleFlag06 {
				get { return GetBitProperty(12, 1, InvisibleFlag06Property) != 0; }
				set { SetBitProperty(12, 1, value ? 1 : 0, InvisibleFlag06Property); }
			}

			/// <summary># 07 # hide</summary>
			/// <remarks>
			/// Japanese short name: "#07#非表示", Google translated: "# 07 # hide".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("invisibleFlag07:1", index: 86, minimum: 0, maximum: 1, step: 1, sortOrder: 8070, unknown2: 1)]
			[DisplayName("# 07 # hide")]
			[Description("")]
			[DefaultValue(false)]
			public Boolean InvisibleFlag07 {
				get { return GetBitProperty(13, 1, InvisibleFlag07Property) != 0; }
				set { SetBitProperty(13, 1, value ? 1 : 0, InvisibleFlag07Property); }
			}

			/// <summary># 08 # hide</summary>
			/// <remarks>
			/// Japanese short name: "#08#非表示", Google translated: "# 08 # hide".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("invisibleFlag08:1", index: 87, minimum: 0, maximum: 1, step: 1, sortOrder: 8080, unknown2: 1)]
			[DisplayName("# 08 # hide")]
			[Description("")]
			[DefaultValue(false)]
			public Boolean InvisibleFlag08 {
				get { return GetBitProperty(14, 1, InvisibleFlag08Property) != 0; }
				set { SetBitProperty(14, 1, value ? 1 : 0, InvisibleFlag08Property); }
			}

			/// <summary># 09 # hide</summary>
			/// <remarks>
			/// Japanese short name: "#09#非表示", Google translated: "# 09 # hide".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("invisibleFlag09:1", index: 88, minimum: 0, maximum: 1, step: 1, sortOrder: 8090, unknown2: 1)]
			[DisplayName("# 09 # hide")]
			[Description("")]
			[DefaultValue(false)]
			public Boolean InvisibleFlag09 {
				get { return GetBitProperty(15, 1, InvisibleFlag09Property) != 0; }
				set { SetBitProperty(15, 1, value ? 1 : 0, InvisibleFlag09Property); }
			}

			/// <summary># 10 # hide</summary>
			/// <remarks>
			/// Japanese short name: "#10#非表示", Google translated: "# 10 # hide".
			/// Japanese description: "襟", Google translated: "Collar".
			/// </remarks>
			[ParameterTableRowAttribute("invisibleFlag10:1", index: 89, minimum: 0, maximum: 1, step: 1, sortOrder: 8100, unknown2: 1)]
			[DisplayName("# 10 # hide")]
			[Description("Collar")]
			[DefaultValue(false)]
			public Boolean InvisibleFlag10 {
				get { return GetBitProperty(16, 1, InvisibleFlag10Property) != 0; }
				set { SetBitProperty(16, 1, value ? 1 : 0, InvisibleFlag10Property); }
			}

			/// <summary># 11 # hide</summary>
			/// <remarks>
			/// Japanese short name: "#11#非表示", Google translated: "# 11 # hide".
			/// Japanese description: "襟回り", Google translated: "Collar around".
			/// </remarks>
			[ParameterTableRowAttribute("invisibleFlag11:1", index: 90, minimum: 0, maximum: 1, step: 1, sortOrder: 8110, unknown2: 1)]
			[DisplayName("# 11 # hide")]
			[Description("Collar around")]
			[DefaultValue(false)]
			public Boolean InvisibleFlag11 {
				get { return GetBitProperty(17, 1, InvisibleFlag11Property) != 0; }
				set { SetBitProperty(17, 1, value ? 1 : 0, InvisibleFlag11Property); }
			}

			/// <summary># 12 # hide</summary>
			/// <remarks>
			/// Japanese short name: "#12#非表示", Google translated: "# 12 # hide".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("invisibleFlag12:1", index: 91, minimum: 0, maximum: 1, step: 1, sortOrder: 8120, unknown2: 1)]
			[DisplayName("# 12 # hide")]
			[Description("")]
			[DefaultValue(false)]
			public Boolean InvisibleFlag12 {
				get { return GetBitProperty(18, 1, InvisibleFlag12Property) != 0; }
				set { SetBitProperty(18, 1, value ? 1 : 0, InvisibleFlag12Property); }
			}

			/// <summary># 13 # hide</summary>
			/// <remarks>
			/// Japanese short name: "#13#非表示", Google translated: "# 13 # hide".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("invisibleFlag13:1", index: 92, minimum: 0, maximum: 1, step: 1, sortOrder: 8130, unknown2: 1)]
			[DisplayName("# 13 # hide")]
			[Description("")]
			[DefaultValue(false)]
			public Boolean InvisibleFlag13 {
				get { return GetBitProperty(19, 1, InvisibleFlag13Property) != 0; }
				set { SetBitProperty(19, 1, value ? 1 : 0, InvisibleFlag13Property); }
			}

			/// <summary># 14 # hide</summary>
			/// <remarks>
			/// Japanese short name: "#14#非表示", Google translated: "# 14 # hide".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("invisibleFlag14:1", index: 93, minimum: 0, maximum: 1, step: 1, sortOrder: 8140, unknown2: 1)]
			[DisplayName("# 14 # hide")]
			[Description("")]
			[DefaultValue(false)]
			public Boolean InvisibleFlag14 {
				get { return GetBitProperty(20, 1, InvisibleFlag14Property) != 0; }
				set { SetBitProperty(20, 1, value ? 1 : 0, InvisibleFlag14Property); }
			}

			/// <summary># 15 # hide</summary>
			/// <remarks>
			/// Japanese short name: "#15#非表示", Google translated: "# 15 # hide".
			/// Japanese description: "頭巾の裾", Google translated: "Hem of the hood".
			/// </remarks>
			[ParameterTableRowAttribute("invisibleFlag15:1", index: 94, minimum: 0, maximum: 1, step: 1, sortOrder: 8150, unknown2: 1)]
			[DisplayName("# 15 # hide")]
			[Description("Hem of the hood")]
			[DefaultValue(false)]
			public Boolean InvisibleFlag15 {
				get { return GetBitProperty(21, 1, InvisibleFlag15Property) != 0; }
				set { SetBitProperty(21, 1, value ? 1 : 0, InvisibleFlag15Property); }
			}

			/// <summary># 16 # hide</summary>
			/// <remarks>
			/// Japanese short name: "#16#非表示", Google translated: "# 16 # hide".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("invisibleFlag16:1", index: 95, minimum: 0, maximum: 1, step: 1, sortOrder: 8160, unknown2: 1)]
			[DisplayName("# 16 # hide")]
			[Description("")]
			[DefaultValue(false)]
			public Boolean InvisibleFlag16 {
				get { return GetBitProperty(22, 1, InvisibleFlag16Property) != 0; }
				set { SetBitProperty(22, 1, value ? 1 : 0, InvisibleFlag16Property); }
			}

			/// <summary># 17 # hide</summary>
			/// <remarks>
			/// Japanese short name: "#17#非表示", Google translated: "# 17 # hide".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("invisibleFlag17:1", index: 96, minimum: 0, maximum: 1, step: 1, sortOrder: 8170, unknown2: 1)]
			[DisplayName("# 17 # hide")]
			[Description("")]
			[DefaultValue(false)]
			public Boolean InvisibleFlag17 {
				get { return GetBitProperty(23, 1, InvisibleFlag17Property) != 0; }
				set { SetBitProperty(23, 1, value ? 1 : 0, InvisibleFlag17Property); }
			}

			/// <summary># 18 # hide</summary>
			/// <remarks>
			/// Japanese short name: "#18#非表示", Google translated: "# 18 # hide".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("invisibleFlag18:1", index: 97, minimum: 0, maximum: 1, step: 1, sortOrder: 8180, unknown2: 1)]
			[DisplayName("# 18 # hide")]
			[Description("")]
			[DefaultValue(false)]
			public Boolean InvisibleFlag18 {
				get { return GetBitProperty(24, 1, InvisibleFlag18Property) != 0; }
				set { SetBitProperty(24, 1, value ? 1 : 0, InvisibleFlag18Property); }
			}

			/// <summary># 19 # hide</summary>
			/// <remarks>
			/// Japanese short name: "#19#非表示", Google translated: "# 19 # hide".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("invisibleFlag19:1", index: 98, minimum: 0, maximum: 1, step: 1, sortOrder: 8190, unknown2: 1)]
			[DisplayName("# 19 # hide")]
			[Description("")]
			[DefaultValue(false)]
			public Boolean InvisibleFlag19 {
				get { return GetBitProperty(25, 1, InvisibleFlag19Property) != 0; }
				set { SetBitProperty(25, 1, value ? 1 : 0, InvisibleFlag19Property); }
			}

			/// <summary># 20 # hide</summary>
			/// <remarks>
			/// Japanese short name: "#20#非表示", Google translated: "# 20 # hide".
			/// Japanese description: "袖A", Google translated: "A sleeve".
			/// </remarks>
			[ParameterTableRowAttribute("invisibleFlag20:1", index: 99, minimum: 0, maximum: 1, step: 1, sortOrder: 8200, unknown2: 1)]
			[DisplayName("# 20 # hide")]
			[Description("A sleeve")]
			[DefaultValue(false)]
			public Boolean InvisibleFlag20 {
				get { return GetBitProperty(26, 1, InvisibleFlag20Property) != 0; }
				set { SetBitProperty(26, 1, value ? 1 : 0, InvisibleFlag20Property); }
			}

			/// <summary># 21 # hide</summary>
			/// <remarks>
			/// Japanese short name: "#21#非表示", Google translated: "# 21 # hide".
			/// Japanese description: "袖B", Google translated: "Sleeve B".
			/// </remarks>
			[ParameterTableRowAttribute("invisibleFlag21:1", index: 100, minimum: 0, maximum: 1, step: 1, sortOrder: 8210, unknown2: 1)]
			[DisplayName("# 21 # hide")]
			[Description("Sleeve B")]
			[DefaultValue(false)]
			public Boolean InvisibleFlag21 {
				get { return GetBitProperty(27, 1, InvisibleFlag21Property) != 0; }
				set { SetBitProperty(27, 1, value ? 1 : 0, InvisibleFlag21Property); }
			}

			/// <summary># 22 # hide</summary>
			/// <remarks>
			/// Japanese short name: "#22#非表示", Google translated: "# 22 # hide".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("invisibleFlag22:1", index: 101, minimum: 0, maximum: 1, step: 1, sortOrder: 8220, unknown2: 1)]
			[DisplayName("# 22 # hide")]
			[Description("")]
			[DefaultValue(false)]
			public Boolean InvisibleFlag22 {
				get { return GetBitProperty(28, 1, InvisibleFlag22Property) != 0; }
				set { SetBitProperty(28, 1, value ? 1 : 0, InvisibleFlag22Property); }
			}

			/// <summary># 23 # hide</summary>
			/// <remarks>
			/// Japanese short name: "#23#非表示", Google translated: "# 23 # hide".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("invisibleFlag23:1", index: 102, minimum: 0, maximum: 1, step: 1, sortOrder: 8230, unknown2: 1)]
			[DisplayName("# 23 # hide")]
			[Description("")]
			[DefaultValue(false)]
			public Boolean InvisibleFlag23 {
				get { return GetBitProperty(29, 1, InvisibleFlag23Property) != 0; }
				set { SetBitProperty(29, 1, value ? 1 : 0, InvisibleFlag23Property); }
			}

			/// <summary># 24 # hide</summary>
			/// <remarks>
			/// Japanese short name: "#24#非表示", Google translated: "# 24 # hide".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("invisibleFlag24:1", index: 103, minimum: 0, maximum: 1, step: 1, sortOrder: 8240, unknown2: 1)]
			[DisplayName("# 24 # hide")]
			[Description("")]
			[DefaultValue(false)]
			public Boolean InvisibleFlag24 {
				get { return GetBitProperty(30, 1, InvisibleFlag24Property) != 0; }
				set { SetBitProperty(30, 1, value ? 1 : 0, InvisibleFlag24Property); }
			}

			/// <summary># 25 Hide</summary>
			/// <remarks>
			/// Japanese short name: "#25#非表示", Google translated: "# 25 Hide".
			/// Japanese description: "腕", Google translated: "Arm".
			/// </remarks>
			[ParameterTableRowAttribute("invisibleFlag25:1", index: 104, minimum: 0, maximum: 1, step: 1, sortOrder: 8250, unknown2: 1)]
			[DisplayName("# 25 Hide")]
			[Description("Arm")]
			[DefaultValue(false)]
			public Boolean InvisibleFlag25 {
				get { return GetBitProperty(31, 1, InvisibleFlag25Property) != 0; }
				set { SetBitProperty(31, 1, value ? 1 : 0, InvisibleFlag25Property); }
			}

			/// <summary># 26 # hide</summary>
			/// <remarks>
			/// Japanese short name: "#26#非表示", Google translated: "# 26 # hide".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("invisibleFlag26:1", index: 105, minimum: 0, maximum: 1, step: 1, sortOrder: 8260, unknown2: 1)]
			[DisplayName("# 26 # hide")]
			[Description("")]
			[DefaultValue(false)]
			public Boolean InvisibleFlag26 {
				get { return GetBitProperty(32, 1, InvisibleFlag26Property) != 0; }
				set { SetBitProperty(32, 1, value ? 1 : 0, InvisibleFlag26Property); }
			}

			/// <summary># 27 # hide</summary>
			/// <remarks>
			/// Japanese short name: "#27#非表示", Google translated: "# 27 # hide".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("invisibleFlag27:1", index: 106, minimum: 0, maximum: 1, step: 1, sortOrder: 8270, unknown2: 1)]
			[DisplayName("# 27 # hide")]
			[Description("")]
			[DefaultValue(false)]
			public Boolean InvisibleFlag27 {
				get { return GetBitProperty(33, 1, InvisibleFlag27Property) != 0; }
				set { SetBitProperty(33, 1, value ? 1 : 0, InvisibleFlag27Property); }
			}

			/// <summary># 28 # hide</summary>
			/// <remarks>
			/// Japanese short name: "#28#非表示", Google translated: "# 28 # hide".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("invisibleFlag28:1", index: 107, minimum: 0, maximum: 1, step: 1, sortOrder: 8280, unknown2: 1)]
			[DisplayName("# 28 # hide")]
			[Description("")]
			[DefaultValue(false)]
			public Boolean InvisibleFlag28 {
				get { return GetBitProperty(34, 1, InvisibleFlag28Property) != 0; }
				set { SetBitProperty(34, 1, value ? 1 : 0, InvisibleFlag28Property); }
			}

			/// <summary># 29 # hide</summary>
			/// <remarks>
			/// Japanese short name: "#29#非表示", Google translated: "# 29 # hide".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("invisibleFlag29:1", index: 108, minimum: 0, maximum: 1, step: 1, sortOrder: 8290, unknown2: 1)]
			[DisplayName("# 29 # hide")]
			[Description("")]
			[DefaultValue(false)]
			public Boolean InvisibleFlag29 {
				get { return GetBitProperty(35, 1, InvisibleFlag29Property) != 0; }
				set { SetBitProperty(35, 1, value ? 1 : 0, InvisibleFlag29Property); }
			}

			/// <summary># 30 # hide</summary>
			/// <remarks>
			/// Japanese short name: "#30#非表示", Google translated: "# 30 # hide".
			/// Japanese description: "ベルト", Google translated: "Belt".
			/// </remarks>
			[ParameterTableRowAttribute("invisibleFlag30:1", index: 109, minimum: 0, maximum: 1, step: 1, sortOrder: 8300, unknown2: 1)]
			[DisplayName("# 30 # hide")]
			[Description("Belt")]
			[DefaultValue(false)]
			public Boolean InvisibleFlag30 {
				get { return GetBitProperty(36, 1, InvisibleFlag30Property) != 0; }
				set { SetBitProperty(36, 1, value ? 1 : 0, InvisibleFlag30Property); }
			}

			/// <summary># 31 # hide</summary>
			/// <remarks>
			/// Japanese short name: "#31#非表示", Google translated: "# 31 # hide".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("invisibleFlag31:1", index: 110, minimum: 0, maximum: 1, step: 1, sortOrder: 8310, unknown2: 1)]
			[DisplayName("# 31 # hide")]
			[Description("")]
			[DefaultValue(false)]
			public Boolean InvisibleFlag31 {
				get { return GetBitProperty(37, 1, InvisibleFlag31Property) != 0; }
				set { SetBitProperty(37, 1, value ? 1 : 0, InvisibleFlag31Property); }
			}

			/// <summary># 32 # hide</summary>
			/// <remarks>
			/// Japanese short name: "#32#非表示", Google translated: "# 32 # hide".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("invisibleFlag32:1", index: 111, minimum: 0, maximum: 1, step: 1, sortOrder: 8320, unknown2: 1)]
			[DisplayName("# 32 # hide")]
			[Description("")]
			[DefaultValue(false)]
			public Boolean InvisibleFlag32 {
				get { return GetBitProperty(38, 1, InvisibleFlag32Property) != 0; }
				set { SetBitProperty(38, 1, value ? 1 : 0, InvisibleFlag32Property); }
			}

			/// <summary># 33 # hide</summary>
			/// <remarks>
			/// Japanese short name: "#33#非表示", Google translated: "# 33 # hide".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("invisibleFlag33:1", index: 112, minimum: 0, maximum: 1, step: 1, sortOrder: 8330, unknown2: 1)]
			[DisplayName("# 33 # hide")]
			[Description("")]
			[DefaultValue(false)]
			public Boolean InvisibleFlag33 {
				get { return GetBitProperty(39, 1, InvisibleFlag33Property) != 0; }
				set { SetBitProperty(39, 1, value ? 1 : 0, InvisibleFlag33Property); }
			}

			/// <summary># 34 # hide</summary>
			/// <remarks>
			/// Japanese short name: "#34#非表示", Google translated: "# 34 # hide".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("invisibleFlag34:1", index: 113, minimum: 0, maximum: 1, step: 1, sortOrder: 8340, unknown2: 1)]
			[DisplayName("# 34 # hide")]
			[Description("")]
			[DefaultValue(false)]
			public Boolean InvisibleFlag34 {
				get { return GetBitProperty(40, 1, InvisibleFlag34Property) != 0; }
				set { SetBitProperty(40, 1, value ? 1 : 0, InvisibleFlag34Property); }
			}

			/// <summary># 35 # hide</summary>
			/// <remarks>
			/// Japanese short name: "#35#非表示", Google translated: "# 35 # hide".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("invisibleFlag35:1", index: 114, minimum: 0, maximum: 1, step: 1, sortOrder: 8350, unknown2: 1)]
			[DisplayName("# 35 # hide")]
			[Description("")]
			[DefaultValue(false)]
			public Boolean InvisibleFlag35 {
				get { return GetBitProperty(41, 1, InvisibleFlag35Property) != 0; }
				set { SetBitProperty(41, 1, value ? 1 : 0, InvisibleFlag35Property); }
			}

			/// <summary># 36 # hide</summary>
			/// <remarks>
			/// Japanese short name: "#36#非表示", Google translated: "# 36 # hide".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("invisibleFlag36:1", index: 115, minimum: 0, maximum: 1, step: 1, sortOrder: 8360, unknown2: 1)]
			[DisplayName("# 36 # hide")]
			[Description("")]
			[DefaultValue(false)]
			public Boolean InvisibleFlag36 {
				get { return GetBitProperty(42, 1, InvisibleFlag36Property) != 0; }
				set { SetBitProperty(42, 1, value ? 1 : 0, InvisibleFlag36Property); }
			}

			/// <summary># 37 # hide</summary>
			/// <remarks>
			/// Japanese short name: "#37#非表示", Google translated: "# 37 # hide".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("invisibleFlag37:1", index: 116, minimum: 0, maximum: 1, step: 1, sortOrder: 8370, unknown2: 1)]
			[DisplayName("# 37 # hide")]
			[Description("")]
			[DefaultValue(false)]
			public Boolean InvisibleFlag37 {
				get { return GetBitProperty(43, 1, InvisibleFlag37Property) != 0; }
				set { SetBitProperty(43, 1, value ? 1 : 0, InvisibleFlag37Property); }
			}

			/// <summary># 38 # hide</summary>
			/// <remarks>
			/// Japanese short name: "#38#非表示", Google translated: "# 38 # hide".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("invisibleFlag38:1", index: 117, minimum: 0, maximum: 1, step: 1, sortOrder: 8380, unknown2: 1)]
			[DisplayName("# 38 # hide")]
			[Description("")]
			[DefaultValue(false)]
			public Boolean InvisibleFlag38 {
				get { return GetBitProperty(44, 1, InvisibleFlag38Property) != 0; }
				set { SetBitProperty(44, 1, value ? 1 : 0, InvisibleFlag38Property); }
			}

			/// <summary># 39 # hide</summary>
			/// <remarks>
			/// Japanese short name: "#39#非表示", Google translated: "# 39 # hide".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("invisibleFlag39:1", index: 118, minimum: 0, maximum: 1, step: 1, sortOrder: 8390, unknown2: 1)]
			[DisplayName("# 39 # hide")]
			[Description("")]
			[DefaultValue(false)]
			public Boolean InvisibleFlag39 {
				get { return GetBitProperty(45, 1, InvisibleFlag39Property) != 0; }
				set { SetBitProperty(45, 1, value ? 1 : 0, InvisibleFlag39Property); }
			}

			/// <summary># 40 # hide</summary>
			/// <remarks>
			/// Japanese short name: "#40#非表示", Google translated: "# 40 # hide".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("invisibleFlag40:1", index: 119, minimum: 0, maximum: 1, step: 1, sortOrder: 8400, unknown2: 1)]
			[DisplayName("# 40 # hide")]
			[Description("")]
			[DefaultValue(false)]
			public Boolean InvisibleFlag40 {
				get { return GetBitProperty(46, 1, InvisibleFlag40Property) != 0; }
				set { SetBitProperty(46, 1, value ? 1 : 0, InvisibleFlag40Property); }
			}

			/// <summary># 41 # hide</summary>
			/// <remarks>
			/// Japanese short name: "#41#非表示", Google translated: "# 41 # hide".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("invisibleFlag41:1", index: 120, minimum: 0, maximum: 1, step: 1, sortOrder: 8410, unknown2: 1)]
			[DisplayName("# 41 # hide")]
			[Description("")]
			[DefaultValue(false)]
			public Boolean InvisibleFlag41 {
				get { return GetBitProperty(47, 1, InvisibleFlag41Property) != 0; }
				set { SetBitProperty(47, 1, value ? 1 : 0, InvisibleFlag41Property); }
			}

			/// <summary># 42 # hide</summary>
			/// <remarks>
			/// Japanese short name: "#42#非表示", Google translated: "# 42 # hide".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("invisibleFlag42:1", index: 121, minimum: 0, maximum: 1, step: 1, sortOrder: 8420, unknown2: 1)]
			[DisplayName("# 42 # hide")]
			[Description("")]
			[DefaultValue(false)]
			public Boolean InvisibleFlag42 {
				get { return GetBitProperty(48, 1, InvisibleFlag42Property) != 0; }
				set { SetBitProperty(48, 1, value ? 1 : 0, InvisibleFlag42Property); }
			}

			/// <summary># 43 # hide</summary>
			/// <remarks>
			/// Japanese short name: "#43#非表示", Google translated: "# 43 # hide".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("invisibleFlag43:1", index: 122, minimum: 0, maximum: 1, step: 1, sortOrder: 8430, unknown2: 1)]
			[DisplayName("# 43 # hide")]
			[Description("")]
			[DefaultValue(false)]
			public Boolean InvisibleFlag43 {
				get { return GetBitProperty(49, 1, InvisibleFlag43Property) != 0; }
				set { SetBitProperty(49, 1, value ? 1 : 0, InvisibleFlag43Property); }
			}

			/// <summary># 44 # hide</summary>
			/// <remarks>
			/// Japanese short name: "#44#非表示", Google translated: "# 44 # hide".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("invisibleFlag44:1", index: 123, minimum: 0, maximum: 1, step: 1, sortOrder: 8440, unknown2: 1)]
			[DisplayName("# 44 # hide")]
			[Description("")]
			[DefaultValue(false)]
			public Boolean InvisibleFlag44 {
				get { return GetBitProperty(50, 1, InvisibleFlag44Property) != 0; }
				set { SetBitProperty(50, 1, value ? 1 : 0, InvisibleFlag44Property); }
			}

			/// <summary># 45 # hide</summary>
			/// <remarks>
			/// Japanese short name: "#45#非表示", Google translated: "# 45 # hide".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("invisibleFlag45:1", index: 124, minimum: 0, maximum: 1, step: 1, sortOrder: 8450, unknown2: 1)]
			[DisplayName("# 45 # hide")]
			[Description("")]
			[DefaultValue(false)]
			public Boolean InvisibleFlag45 {
				get { return GetBitProperty(51, 1, InvisibleFlag45Property) != 0; }
				set { SetBitProperty(51, 1, value ? 1 : 0, InvisibleFlag45Property); }
			}

			/// <summary># 46 # hide</summary>
			/// <remarks>
			/// Japanese short name: "#46#非表示", Google translated: "# 46 # hide".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("invisibleFlag46:1", index: 125, minimum: 0, maximum: 1, step: 1, sortOrder: 8460, unknown2: 1)]
			[DisplayName("# 46 # hide")]
			[Description("")]
			[DefaultValue(false)]
			public Boolean InvisibleFlag46 {
				get { return GetBitProperty(52, 1, InvisibleFlag46Property) != 0; }
				set { SetBitProperty(52, 1, value ? 1 : 0, InvisibleFlag46Property); }
			}

			/// <summary># 47 # hide</summary>
			/// <remarks>
			/// Japanese short name: "#47#非表示", Google translated: "# 47 # hide".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("invisibleFlag47:1", index: 126, minimum: 0, maximum: 1, step: 1, sortOrder: 8470, unknown2: 1)]
			[DisplayName("# 47 # hide")]
			[Description("")]
			[DefaultValue(false)]
			public Boolean InvisibleFlag47 {
				get { return GetBitProperty(53, 1, InvisibleFlag47Property) != 0; }
				set { SetBitProperty(53, 1, value ? 1 : 0, InvisibleFlag47Property); }
			}

			/// <summary>Or multi-drop sharing ban</summary>
			/// <remarks>
			/// Japanese short name: "マルチドロップ共有禁止か", Google translated: "Or multi-drop sharing ban".
			/// Japanese description: "マルチドロップ共有禁止か", Google translated: "Or multi-drop sharing ban".
			/// </remarks>
			[ParameterTableRowAttribute("disableMultiDropShare:1", index: 127, minimum: 0, maximum: 1, step: 1, sortOrder: 4780, unknown2: 1)]
			[DisplayName("Or multi-drop sharing ban")]
			[Description("Or multi-drop sharing ban")]
			[DefaultValue(false)]
			public Boolean DisableMultiDropShare {
				get { return GetBitProperty(54, 1, DisableMultiDropShareProperty) != 0; }
				set { SetBitProperty(54, 1, value ? 1 : 0, DisableMultiDropShareProperty); }
			}

			/// <summary>Or have DLC for simple model</summary>
			/// <remarks>
			/// Japanese short name: "DLC用シンプルモデルありか", Google translated: "Or have DLC for simple model".
			/// Japanese description: "ＤＬＣ用シンプルモデルが存在しているか", Google translated: "DLC for simple model is whether there".
			/// </remarks>
			[ParameterTableRowAttribute("simpleModelForDlc:1", index: 128, minimum: 0, maximum: 1, step: 1, sortOrder: 4785, unknown2: 1)]
			[DisplayName("Or have DLC for simple model")]
			[Description("DLC for simple model is whether there")]
			[DefaultValue(false)]
			public Boolean SimpleModelForDlc {
				get { return GetBitProperty(55, 1, SimpleModelForDlcProperty) != 0; }
				set { SetBitProperty(55, 1, value ? 1 : 0, SimpleModelForDlcProperty); }
			}

			/// <summary>Padding</summary>
			/// <remarks>
			/// Japanese short name: "パディング", Google translated: "Padding".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("pad_0[1]", index: 129, minimum: 0, maximum: 0, step: 0, sortOrder: 10901, unknown2: 0)]
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
			[ParameterTableRowAttribute("pad_1[6]", index: 131, minimum: 0, maximum: 1, step: 1, sortOrder: 10902, unknown2: 1)]
			[DisplayName("Padding")]
			[Description("")]
			[Browsable(false)]
			public Byte[] Pad_1 {
				get { return pad_1; }
				set { SetProperty(ref pad_1, ref value, Pad_1Property); }
			}

			/// <summary>Get the localised English name of the <see cref="Protector"/>.</summary>
			public string EnglishName { get { return GetLocalisedName(Language.English); } }

			/// <summary>Get the localised English description of the <see cref="Protector"/>.</summary>
			public string EnglishDescription { get { return GetLocalisedDescription(Language.English).Trim(); } }

			internal Protector(ParameterTable table, int index, AssetLoader loader, int next)
				: base(table, index, loader) {
				BinaryReader reader = loader.Reader;

				SortId = reader.ReadInt32();
				WanderingEquipId = reader.ReadInt32();
				VagrantItemLotId = reader.ReadInt32();
				VagrantBonusEneDropItemLotId = reader.ReadInt32();
				VagrantItemEneDropItemLotId = reader.ReadInt32();
				FixPrice = reader.ReadInt32();
				BasicPrice = reader.ReadInt32();
				SellValue = reader.ReadInt32();
				Weight = reader.ReadSingle();
				ResidentSpEffectId = reader.ReadInt32();
				ResidentSpEffectId2 = reader.ReadInt32();
				ResidentSpEffectId3 = reader.ReadInt32();
				MaterialSetId = reader.ReadInt32();
				PartsDamageRate = reader.ReadSingle();
				CorectSARecover = reader.ReadSingle();
				OriginEquipPro = reader.ReadInt32();
				OriginEquipPro1 = reader.ReadInt32();
				OriginEquipPro2 = reader.ReadInt32();
				OriginEquipPro3 = reader.ReadInt32();
				OriginEquipPro4 = reader.ReadInt32();
				OriginEquipPro5 = reader.ReadInt32();
				OriginEquipPro6 = reader.ReadInt32();
				OriginEquipPro7 = reader.ReadInt32();
				OriginEquipPro8 = reader.ReadInt32();
				OriginEquipPro9 = reader.ReadInt32();
				OriginEquipPro10 = reader.ReadInt32();
				OriginEquipPro11 = reader.ReadInt32();
				OriginEquipPro12 = reader.ReadInt32();
				OriginEquipPro13 = reader.ReadInt32();
				OriginEquipPro14 = reader.ReadInt32();
				OriginEquipPro15 = reader.ReadInt32();
				FaceScaleM_ScaleX = reader.ReadSingle();
				FaceScaleM_ScaleZ = reader.ReadSingle();
				FaceScaleM_MaxX = reader.ReadSingle();
				FaceScaleM_MaxZ = reader.ReadSingle();
				FaceScaleF_ScaleX = reader.ReadSingle();
				FaceScaleF_ScaleZ = reader.ReadSingle();
				FaceScaleF_MaxX = reader.ReadSingle();
				FaceScaleF_MaxZ = reader.ReadSingle();
				QwcId = reader.ReadInt32();
				EquipModelId = reader.ReadUInt16();
				IconIdM = reader.ReadUInt16();
				IconIdF = reader.ReadUInt16();
				KnockBack = reader.ReadUInt16();
				KnockbackBounceRate = reader.ReadUInt16();
				SaDurability = reader.ReadInt16();
				DefFlickPower = reader.ReadUInt16();
				DefensePhysics = reader.ReadUInt16();
				DefenseMagic = reader.ReadUInt16();
				DefenseFire = reader.ReadUInt16();
				DefenseThunder = reader.ReadUInt16();
				DefenseSlash = reader.ReadInt16();
				DefenseBlow = reader.ReadInt16();
				DefenseThrust = reader.ReadInt16();
				ResistPoison = reader.ReadUInt16();
				ResistDisease = reader.ReadUInt16();
				ResistBlood = reader.ReadUInt16();
				ResistCurse = reader.ReadUInt16();
				ReinforceTypeId = reader.ReadInt16();
				TrophySGradeId = reader.ReadInt16();
				ShopLv = reader.ReadInt16();
				KnockbackParamId = reader.ReadByte();
				FlickDamageCutRate = reader.ReadByte();
				EquipModelCategory = (EquipModelCategory)reader.ReadByte();
				EquipModelGender = (EquipModelGender)reader.ReadByte();
				ProtectorCategory = (ArmorCategory)reader.ReadByte();
				DefenseMaterial = (WeaponMaterialDefend)reader.ReadByte();
				DefenseMaterialSfx = (WeaponMaterialDefendSound)reader.ReadByte();
				PartsDmgType = (AttackParameterPartDamageType)reader.ReadByte();
				DefenseMaterial_Weak = (WeaponMaterialDefend)reader.ReadByte();
				DefenseMaterialSfx_Weak = (WeaponMaterialDefendSound)reader.ReadByte();
				BitFields = reader.ReadBytes(7);
				Pad_0 = reader.ReadBytes(1);
				OldSortId = reader.ReadInt16();
				Pad_1 = reader.ReadBytes(6);
			}

			internal Protector(ParameterTable table, int index)
				: base(table, index) {
				BitFields = new byte[7];
				ResidentSpEffectId = (Int32)0;
				ResidentSpEffectId2 = (Int32)0;
				ResidentSpEffectId3 = (Int32)0;
				MaterialSetId = (Int32)(-1);
				PartsDamageRate = (Single)1;
				CorectSARecover = (Single)0;
				OriginEquipPro = (Int32)(-1);
				OriginEquipPro1 = (Int32)(-1);
				OriginEquipPro2 = (Int32)(-1);
				OriginEquipPro3 = (Int32)(-1);
				OriginEquipPro4 = (Int32)(-1);
				OriginEquipPro5 = (Int32)(-1);
				OriginEquipPro6 = (Int32)(-1);
				OriginEquipPro7 = (Int32)(-1);
				OriginEquipPro8 = (Int32)(-1);
				OriginEquipPro9 = (Int32)(-1);
				OriginEquipPro10 = (Int32)(-1);
				OriginEquipPro11 = (Int32)(-1);
				OriginEquipPro12 = (Int32)(-1);
				OriginEquipPro13 = (Int32)(-1);
				OriginEquipPro14 = (Int32)(-1);
				OriginEquipPro15 = (Int32)(-1);
				FaceScaleM_ScaleX = (Single)1;
				FaceScaleM_ScaleZ = (Single)1;
				FaceScaleM_MaxX = (Single)1;
				FaceScaleM_MaxZ = (Single)1;
				FaceScaleF_ScaleX = (Single)1;
				FaceScaleF_ScaleZ = (Single)1;
				FaceScaleF_MaxX = (Single)1;
				FaceScaleF_MaxZ = (Single)1;
				QwcId = (Int32)(-1);
				IconIdM = (UInt16)0;
				IconIdF = (UInt16)0;
				KnockBack = (UInt16)0;
				KnockbackBounceRate = (UInt16)0;
				Durability = (UInt16)100;
				DurabilityMax = (UInt16)100;
				SaDurability = (Int16)0;
				DefFlickPower = (UInt16)0;
				DefensePhysics = (UInt16)100;
				DefenseMagic = (UInt16)100;
				DefenseFire = (UInt16)100;
				DefenseThunder = (UInt16)100;
				DefenseSlash = (Int16)0;
				DefenseBlow = (Int16)0;
				DefenseThrust = (Int16)0;
				ResistPoison = (UInt16)100;
				ResistDisease = (UInt16)100;
				ResistBlood = (UInt16)100;
				ResistCurse = (UInt16)100;
				ReinforceTypeId = (Int16)0;
				TrophySGradeId = (Int16)(-1);
				ShopLv = (Int16)0;
				KnockbackParamId = (Byte)0;
				FlickDamageCutRate = (Byte)0;
				EquipModelCategory = (EquipModelCategory)1;
				EquipModelGender = (EquipModelGender)0;
				ProtectorCategory = (ArmorCategory)0;
				DefenseMaterial = (WeaponMaterialDefend)50;
				DefenseMaterialSfx = (WeaponMaterialDefendSound)50;
				PartsDmgType = (AttackParameterPartDamageType)0;
				DefenseMaterial_Weak = (WeaponMaterialDefend)50;
				DefenseMaterialSfx_Weak = (WeaponMaterialDefendSound)50;
				IsDeposit = true;
				HeadEquip = true;
				BodyEquip = false;
				ArmEquip = false;
				LegEquip = false;
				UseFaceScale = false;
				InvisibleFlag00 = false;
				InvisibleFlag01 = false;
				InvisibleFlag02 = false;
				InvisibleFlag03 = false;
				InvisibleFlag04 = false;
				InvisibleFlag05 = false;
				InvisibleFlag06 = false;
				InvisibleFlag07 = false;
				InvisibleFlag08 = false;
				InvisibleFlag09 = false;
				InvisibleFlag10 = false;
				InvisibleFlag11 = false;
				InvisibleFlag12 = false;
				InvisibleFlag13 = false;
				InvisibleFlag14 = false;
				InvisibleFlag15 = false;
				InvisibleFlag16 = false;
				InvisibleFlag17 = false;
				InvisibleFlag18 = false;
				InvisibleFlag19 = false;
				InvisibleFlag20 = false;
				InvisibleFlag21 = false;
				InvisibleFlag22 = false;
				InvisibleFlag23 = false;
				InvisibleFlag24 = false;
				InvisibleFlag25 = false;
				InvisibleFlag26 = false;
				InvisibleFlag27 = false;
				InvisibleFlag28 = false;
				InvisibleFlag29 = false;
				InvisibleFlag30 = false;
				InvisibleFlag31 = false;
				InvisibleFlag32 = false;
				InvisibleFlag33 = false;
				InvisibleFlag34 = false;
				InvisibleFlag35 = false;
				InvisibleFlag36 = false;
				InvisibleFlag37 = false;
				InvisibleFlag38 = false;
				InvisibleFlag39 = false;
				InvisibleFlag40 = false;
				InvisibleFlag41 = false;
				InvisibleFlag42 = false;
				InvisibleFlag43 = false;
				InvisibleFlag44 = false;
				InvisibleFlag45 = false;
				InvisibleFlag46 = false;
				InvisibleFlag47 = false;
				DisableMultiDropShare = false;
				SimpleModelForDlc = false;
				Pad_0 = new Byte[1];
				Pad_1 = new Byte[6];
			}

			/// <summary>Get the localised name of the <see cref="Protector"/>.</summary>
			/// <param name="language"></param>
			/// <returns></returns>
			public string GetLocalisedName(Language language = Language.English) { return GetLocalisedString(Engine.ItemArchiveId.ProtectorNames, language); }

			/// <summary>Get the localised description of the <see cref="Protector"/>.</summary>
			/// <param name="language"></param>
			/// <returns></returns>
			public string GetLocalisedDescription(Language language = Language.English) { return GetLocalisedString(Engine.ItemArchiveId.ProtectorDescriptions, language); }

			/// <summary>Write the <see cref="Protector"/> row.</summary>
			/// <param name="writer"></param>
			public override void Write(BinaryWriter writer) {
				writer.Write(SortId);
				writer.Write(WanderingEquipId);
				writer.Write(VagrantItemLotId);
				writer.Write(VagrantBonusEneDropItemLotId);
				writer.Write(VagrantItemEneDropItemLotId);
				writer.Write(FixPrice);
				writer.Write(BasicPrice);
				writer.Write(SellValue);
				writer.Write(Weight);
				writer.Write(ResidentSpEffectId);
				writer.Write(ResidentSpEffectId2);
				writer.Write(ResidentSpEffectId3);
				writer.Write(MaterialSetId);
				writer.Write(PartsDamageRate);
				writer.Write(CorectSARecover);
				writer.Write(OriginEquipPro);
				writer.Write(OriginEquipPro1);
				writer.Write(OriginEquipPro2);
				writer.Write(OriginEquipPro3);
				writer.Write(OriginEquipPro4);
				writer.Write(OriginEquipPro5);
				writer.Write(OriginEquipPro6);
				writer.Write(OriginEquipPro7);
				writer.Write(OriginEquipPro8);
				writer.Write(OriginEquipPro9);
				writer.Write(OriginEquipPro10);
				writer.Write(OriginEquipPro11);
				writer.Write(OriginEquipPro12);
				writer.Write(OriginEquipPro13);
				writer.Write(OriginEquipPro14);
				writer.Write(OriginEquipPro15);
				writer.Write(FaceScaleM_ScaleX);
				writer.Write(FaceScaleM_ScaleZ);
				writer.Write(FaceScaleM_MaxX);
				writer.Write(FaceScaleM_MaxZ);
				writer.Write(FaceScaleF_ScaleX);
				writer.Write(FaceScaleF_ScaleZ);
				writer.Write(FaceScaleF_MaxX);
				writer.Write(FaceScaleF_MaxZ);
				writer.Write(QwcId);
				writer.Write(EquipModelId);
				writer.Write(IconIdM);
				writer.Write(IconIdF);
				writer.Write(KnockBack);
				writer.Write(KnockbackBounceRate);
				writer.Write(Durability);
				writer.Write(DurabilityMax);
				writer.Write(SaDurability);
				writer.Write(DefFlickPower);
				writer.Write(DefensePhysics);
				writer.Write(DefenseMagic);
				writer.Write(DefenseFire);
				writer.Write(DefenseThunder);
				writer.Write(DefenseSlash);
				writer.Write(DefenseBlow);
				writer.Write(DefenseThrust);
				writer.Write(ResistPoison);
				writer.Write(ResistDisease);
				writer.Write(ResistBlood);
				writer.Write(ResistCurse);
				writer.Write(ReinforceTypeId);
				writer.Write(TrophySGradeId);
				writer.Write(ShopLv);
				writer.Write(KnockbackParamId);
				writer.Write(FlickDamageCutRate);
				writer.Write((Byte)EquipModelCategory);
				writer.Write((Byte)EquipModelGender);
				writer.Write((Byte)ProtectorCategory);
				writer.Write((Byte)DefenseMaterial);
				writer.Write((Byte)DefenseMaterialSfx);
				writer.Write((Byte)PartsDmgType);
				writer.Write((Byte)DefenseMaterial_Weak);
				writer.Write((Byte)DefenseMaterialSfx_Weak);
				writer.Write(BitFields);
				writer.Write(Pad_0);
				writer.Write(OldSortId);
				writer.Write(Pad_1);
			}
		}
	}
}
