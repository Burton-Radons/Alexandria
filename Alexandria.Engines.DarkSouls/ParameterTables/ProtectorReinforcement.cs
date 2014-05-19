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
		/// <summary>Armor upgrade.</summary>
		/// <remarks>
		/// Defined as "REINFORCE_PARAM_PROTECTOR_ST" in Dark Souls in the file "ReinforceParamProtector.paramdef" (id 11h).
		/// </remarks>
		public class ProtectorReinforcement : ParameterTableRow {
			public const string TableName = "REINFORCE_PARAM_PROTECTOR_ST";

			Single physicsDefRate, magicDefRate, fireDefRate, thunderDefRate, slashDefRate, blowDefRate, thrustDefRate, resistPoisonRate, resistDiseaseRate, resistBloodRate, resistCurseRate;
			Byte residentSpEffectId1, residentSpEffectId2, residentSpEffectId3, materialSetId;

			public static readonly PropertyInfo
				PhysicsDefRateProperty = GetProperty<ProtectorReinforcement>("PhysicsDefRate"),
				MagicDefRateProperty = GetProperty<ProtectorReinforcement>("MagicDefRate"),
				FireDefRateProperty = GetProperty<ProtectorReinforcement>("FireDefRate"),
				ThunderDefRateProperty = GetProperty<ProtectorReinforcement>("ThunderDefRate"),
				SlashDefRateProperty = GetProperty<ProtectorReinforcement>("SlashDefRate"),
				BlowDefRateProperty = GetProperty<ProtectorReinforcement>("BlowDefRate"),
				ThrustDefRateProperty = GetProperty<ProtectorReinforcement>("ThrustDefRate"),
				ResistPoisonRateProperty = GetProperty<ProtectorReinforcement>("ResistPoisonRate"),
				ResistDiseaseRateProperty = GetProperty<ProtectorReinforcement>("ResistDiseaseRate"),
				ResistBloodRateProperty = GetProperty<ProtectorReinforcement>("ResistBloodRate"),
				ResistCurseRateProperty = GetProperty<ProtectorReinforcement>("ResistCurseRate"),
				ResidentSpEffectId1Property = GetProperty<ProtectorReinforcement>("ResidentSpEffectId1"),
				ResidentSpEffectId2Property = GetProperty<ProtectorReinforcement>("ResidentSpEffectId2"),
				ResidentSpEffectId3Property = GetProperty<ProtectorReinforcement>("ResidentSpEffectId3"),
				MaterialSetIdProperty = GetProperty<ProtectorReinforcement>("MaterialSetId");

			/// <summary>Physical Def</summary>
			/// <remarks>
			/// Japanese short name: "物理防御力", Google translated: "Physical Def".
			/// Japanese description: "物理防御力の補正値", Google translated: "Correction value of the physical defense".
			/// </remarks>
			[ParameterTableRowAttribute("physicsDefRate", index: 0, minimum: 0, maximum: 99.99, step: 0.01, order: 100, unknown2: 1)]
			[DisplayName("Physical Def")]
			[Description("Correction value of the physical defense")]
			[DefaultValue((Single)1)]
			public Single PhysicsDefRate {
				get { return physicsDefRate; }
				set {
					if ((double)value < 0 || (double)value > 99.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99.99 for " + PhysicsDefRateProperty.Name + ".");
					SetProperty(ref physicsDefRate, ref value, PhysicsDefRateProperty);
				}
			}

			/// <summary>Magic defense</summary>
			/// <remarks>
			/// Japanese short name: "魔法防御力", Google translated: "Magic defense".
			/// Japanese description: "魔法防御力の補正値", Google translated: "Correction value of magic defense".
			/// </remarks>
			[ParameterTableRowAttribute("magicDefRate", index: 1, minimum: 0, maximum: 99.99, step: 0.01, order: 200, unknown2: 1)]
			[DisplayName("Magic defense")]
			[Description("Correction value of magic defense")]
			[DefaultValue((Single)1)]
			public Single MagicDefRate {
				get { return magicDefRate; }
				set {
					if ((double)value < 0 || (double)value > 99.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99.99 for " + MagicDefRateProperty.Name + ".");
					SetProperty(ref magicDefRate, ref value, MagicDefRateProperty);
				}
			}

			/// <summary>Flame Defense</summary>
			/// <remarks>
			/// Japanese short name: "炎防御力", Google translated: "Flame Defense".
			/// Japanese description: "炎防御力の補正値", Google translated: "Correction value of flame Defense".
			/// </remarks>
			[ParameterTableRowAttribute("fireDefRate", index: 2, minimum: 0, maximum: 99.99, step: 0.01, order: 300, unknown2: 1)]
			[DisplayName("Flame Defense")]
			[Description("Correction value of flame Defense")]
			[DefaultValue((Single)1)]
			public Single FireDefRate {
				get { return fireDefRate; }
				set {
					if ((double)value < 0 || (double)value > 99.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99.99 for " + FireDefRateProperty.Name + ".");
					SetProperty(ref fireDefRate, ref value, FireDefRateProperty);
				}
			}

			/// <summary>Blitz Defense</summary>
			/// <remarks>
			/// Japanese short name: "電撃防御力", Google translated: "Blitz Defense".
			/// Japanese description: "電撃防御力の補正値", Google translated: "Correction value of blitz defense".
			/// </remarks>
			[ParameterTableRowAttribute("thunderDefRate", index: 3, minimum: 0, maximum: 99.99, step: 0.01, order: 400, unknown2: 1)]
			[DisplayName("Blitz Defense")]
			[Description("Correction value of blitz defense")]
			[DefaultValue((Single)1)]
			public Single ThunderDefRate {
				get { return thunderDefRate; }
				set {
					if ((double)value < 0 || (double)value > 99.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99.99 for " + ThunderDefRateProperty.Name + ".");
					SetProperty(ref thunderDefRate, ref value, ThunderDefRateProperty);
				}
			}

			/// <summary>Slashing Defense</summary>
			/// <remarks>
			/// Japanese short name: "斬撃防御力", Google translated: "Slashing Defense".
			/// Japanese description: "斬撃防御力の補正値", Google translated: "Correction value of slashing defense".
			/// </remarks>
			[ParameterTableRowAttribute("slashDefRate", index: 4, minimum: 0, maximum: 99.99, step: 0.01, order: 500, unknown2: 1)]
			[DisplayName("Slashing Defense")]
			[Description("Correction value of slashing defense")]
			[DefaultValue((Single)1)]
			public Single SlashDefRate {
				get { return slashDefRate; }
				set {
					if ((double)value < 0 || (double)value > 99.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99.99 for " + SlashDefRateProperty.Name + ".");
					SetProperty(ref slashDefRate, ref value, SlashDefRateProperty);
				}
			}

			/// <summary>Blow Defense</summary>
			/// <remarks>
			/// Japanese short name: "打撃防御力", Google translated: "Blow Defense".
			/// Japanese description: "打撃防御力の補正値", Google translated: "Correction value of the striking defense".
			/// </remarks>
			[ParameterTableRowAttribute("blowDefRate", index: 5, minimum: 0, maximum: 99.99, step: 0.01, order: 600, unknown2: 1)]
			[DisplayName("Blow Defense")]
			[Description("Correction value of the striking defense")]
			[DefaultValue((Single)1)]
			public Single BlowDefRate {
				get { return blowDefRate; }
				set {
					if ((double)value < 0 || (double)value > 99.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99.99 for " + BlowDefRateProperty.Name + ".");
					SetProperty(ref blowDefRate, ref value, BlowDefRateProperty);
				}
			}

			/// <summary>Piercing Defense</summary>
			/// <remarks>
			/// Japanese short name: "刺突防御力", Google translated: "Piercing Defense".
			/// Japanese description: "刺突防御力の補正値", Google translated: "Correction value of piercing defense".
			/// </remarks>
			[ParameterTableRowAttribute("thrustDefRate", index: 6, minimum: 0, maximum: 99.99, step: 0.01, order: 700, unknown2: 1)]
			[DisplayName("Piercing Defense")]
			[Description("Correction value of piercing defense")]
			[DefaultValue((Single)1)]
			public Single ThrustDefRate {
				get { return thrustDefRate; }
				set {
					if ((double)value < 0 || (double)value > 99.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99.99 for " + ThrustDefRateProperty.Name + ".");
					SetProperty(ref thrustDefRate, ref value, ThrustDefRateProperty);
				}
			}

			/// <summary>Poison Resistance</summary>
			/// <remarks>
			/// Japanese short name: "毒耐性", Google translated: "Poison Resistance".
			/// Japanese description: "毒耐性の補正値", Google translated: "Correction value of poison-resistant".
			/// </remarks>
			[ParameterTableRowAttribute("resistPoisonRate", index: 7, minimum: 0, maximum: 99.99, step: 0.01, order: 800, unknown2: 1)]
			[DisplayName("Poison Resistance")]
			[Description("Correction value of poison-resistant")]
			[DefaultValue((Single)1)]
			public Single ResistPoisonRate {
				get { return resistPoisonRate; }
				set {
					if ((double)value < 0 || (double)value > 99.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99.99 for " + ResistPoisonRateProperty.Name + ".");
					SetProperty(ref resistPoisonRate, ref value, ResistPoisonRateProperty);
				}
			}

			/// <summary>Plague-resistant</summary>
			/// <remarks>
			/// Japanese short name: "疫病耐性", Google translated: "Plague-resistant".
			/// Japanese description: "疫病耐性の補正値", Google translated: "Correction value of plague-resistant".
			/// </remarks>
			[ParameterTableRowAttribute("resistDiseaseRate", index: 8, minimum: 0, maximum: 99.99, step: 0.01, order: 900, unknown2: 1)]
			[DisplayName("Plague-resistant")]
			[Description("Correction value of plague-resistant")]
			[DefaultValue((Single)1)]
			public Single ResistDiseaseRate {
				get { return resistDiseaseRate; }
				set {
					if ((double)value < 0 || (double)value > 99.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99.99 for " + ResistDiseaseRateProperty.Name + ".");
					SetProperty(ref resistDiseaseRate, ref value, ResistDiseaseRateProperty);
				}
			}

			/// <summary>Bleeding-resistant</summary>
			/// <remarks>
			/// Japanese short name: "出血耐性", Google translated: "Bleeding-resistant".
			/// Japanese description: "出血耐性の補正値", Google translated: "Correction value of bleeding resistant".
			/// </remarks>
			[ParameterTableRowAttribute("resistBloodRate", index: 9, minimum: 0, maximum: 99.99, step: 0.01, order: 1000, unknown2: 1)]
			[DisplayName("Bleeding-resistant")]
			[Description("Correction value of bleeding resistant")]
			[DefaultValue((Single)1)]
			public Single ResistBloodRate {
				get { return resistBloodRate; }
				set {
					if ((double)value < 0 || (double)value > 99.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99.99 for " + ResistBloodRateProperty.Name + ".");
					SetProperty(ref resistBloodRate, ref value, ResistBloodRateProperty);
				}
			}

			/// <summary>Curse resistance</summary>
			/// <remarks>
			/// Japanese short name: "呪耐性", Google translated: "Curse resistance".
			/// Japanese description: "呪耐性の補正値", Google translated: "Correction value of curse resistance".
			/// </remarks>
			[ParameterTableRowAttribute("resistCurseRate", index: 10, minimum: 0, maximum: 99.99, step: 0.01, order: 1100, unknown2: 1)]
			[DisplayName("Curse resistance")]
			[Description("Correction value of curse resistance")]
			[DefaultValue((Single)1)]
			public Single ResistCurseRate {
				get { return resistCurseRate; }
				set {
					if ((double)value < 0 || (double)value > 99.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99.99 for " + ResistCurseRateProperty.Name + ".");
					SetProperty(ref resistCurseRate, ref value, ResistCurseRateProperty);
				}
			}

			/// <summary>Resident special effects ID1</summary>
			/// <remarks>
			/// Japanese short name: "常駐特殊効果ID1", Google translated: "Resident special effects ID1".
			/// Japanese description: "常駐特殊効果ID1の加算補正値", Google translated: "Adding the correction value of the resident special effects ID1".
			/// </remarks>
			[ParameterTableRowAttribute("residentSpEffectId1", index: 11, minimum: 0, maximum: 255, step: 1, order: 1200, unknown2: 1)]
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
			[ParameterTableRowAttribute("residentSpEffectId2", index: 12, minimum: 0, maximum: 255, step: 1, order: 1300, unknown2: 1)]
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
			[ParameterTableRowAttribute("residentSpEffectId3", index: 13, minimum: 0, maximum: 255, step: 1, order: 1400, unknown2: 1)]
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
			[ParameterTableRowAttribute("materialSetId", index: 14, minimum: 0, maximum: 255, step: 1, order: 1500, unknown2: 1)]
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

			internal ProtectorReinforcement(ParameterTable table, int index, AssetLoader loader, int next)
				: base(table, index, loader) {
				BinaryReader reader = loader.Reader;

				PhysicsDefRate = reader.ReadSingle();
				MagicDefRate = reader.ReadSingle();
				FireDefRate = reader.ReadSingle();
				ThunderDefRate = reader.ReadSingle();
				SlashDefRate = reader.ReadSingle();
				BlowDefRate = reader.ReadSingle();
				ThrustDefRate = reader.ReadSingle();
				ResistPoisonRate = reader.ReadSingle();
				ResistDiseaseRate = reader.ReadSingle();
				ResistBloodRate = reader.ReadSingle();
				ResistCurseRate = reader.ReadSingle();
				ResidentSpEffectId1 = reader.ReadByte();
				ResidentSpEffectId2 = reader.ReadByte();
				ResidentSpEffectId3 = reader.ReadByte();
				MaterialSetId = reader.ReadByte();
			}

			internal ProtectorReinforcement(ParameterTable table, int index)
				: base(table, index) {
				PhysicsDefRate = (Single)1;
				MagicDefRate = (Single)1;
				FireDefRate = (Single)1;
				ThunderDefRate = (Single)1;
				SlashDefRate = (Single)1;
				BlowDefRate = (Single)1;
				ThrustDefRate = (Single)1;
				ResistPoisonRate = (Single)1;
				ResistDiseaseRate = (Single)1;
				ResistBloodRate = (Single)1;
				ResistCurseRate = (Single)1;
				ResidentSpEffectId1 = (Byte)0;
				ResidentSpEffectId2 = (Byte)0;
				ResidentSpEffectId3 = (Byte)0;
				MaterialSetId = (Byte)0;
			}

			public override void Write(BinaryWriter writer) {
				writer.Write(PhysicsDefRate);
				writer.Write(MagicDefRate);
				writer.Write(FireDefRate);
				writer.Write(ThunderDefRate);
				writer.Write(SlashDefRate);
				writer.Write(BlowDefRate);
				writer.Write(ThrustDefRate);
				writer.Write(ResistPoisonRate);
				writer.Write(ResistDiseaseRate);
				writer.Write(ResistBloodRate);
				writer.Write(ResistCurseRate);
				writer.Write(ResidentSpEffectId1);
				writer.Write(ResidentSpEffectId2);
				writer.Write(ResidentSpEffectId3);
				writer.Write(MaterialSetId);
			}
		}
	}
}
