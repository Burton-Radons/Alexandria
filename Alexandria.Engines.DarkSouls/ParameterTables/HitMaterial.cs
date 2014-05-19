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
		/// From "HitMtrlParam.paramdef" (id 30h).
		/// </remarks>
		public class HitMaterial : ParameterTableRow {
			public const string TableName = "HIT_MTRL_PARAM_ST";

			Single aiVolumeRate;
			Int32 spEffectIdOnHit0, spEffectIdOnHit1;
			Byte[] pad0;

			public static readonly PropertyInfo
				AiVolumeRateProperty = GetProperty<HitMaterial>("AiVolumeRate"),
				SpEffectIdOnHit0Property = GetProperty<HitMaterial>("SpEffectIdOnHit0"),
				SpEffectIdOnHit1Property = GetProperty<HitMaterial>("SpEffectIdOnHit1"),
				FootEffectHeightTypeProperty = GetProperty<HitMaterial>("FootEffectHeightType"),
				FootEffectDirTypeProperty = GetProperty<HitMaterial>("FootEffectDirType"),
				FloorHeightTypeProperty = GetProperty<HitMaterial>("FloorHeightType"),
				Pad0Property = GetProperty<HitMaterial>("Pad0");

			/// <summary>Sound radius magnification</summary>
			/// <remarks>
			/// Japanese short name: "音半径倍率", Google translated: "Sound radius magnification".
			/// Japanese description: "1倍のときは普通。0にすると音半径が0になる（SEとSFXは無関係のゲーム的なパラメータ）", Google translated: "And usually when one fold. Sound radius becomes 0 when the 0 (SFX SE and the parameters of the game unrelated)".
			/// </remarks>
			[ParameterTableRowAttribute("aiVolumeRate", index: 0, minimum: 0, maximum: 99, step: 0.01, order: 1000, unknown2: 1)]
			[DisplayName("Sound radius magnification")]
			[Description("And usually when one fold. Sound radius becomes 0 when the 0 (SFX SE and the parameters of the game unrelated)")]
			[DefaultValue((Single)1)]
			public Single AiVolumeRate {
				get { return aiVolumeRate; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for AiVolumeRate.");
					SetProperty(ref aiVolumeRate, ref value, AiVolumeRateProperty);
				}
			}

			/// <summary>Special effects applied to 0 when you step on the hit material</summary>
			/// <remarks>
			/// Japanese short name: "ヒットマテリアルを踏んだ時にかかる特殊効果0", Google translated: "Special effects applied to 0 when you step on the hit material".
			/// Japanese description: "キャラがヒットマテリアルを踏んだ時に、設定した特殊効果0が発揮される", Google translated: "When the characters trod the hit material, special effects 0 set is exhibited".
			/// </remarks>
			[ParameterTableRowAttribute("spEffectIdOnHit0", index: 1, minimum: -1, maximum: 1E+08, step: 1, order: 2000, unknown2: 1)]
			[DisplayName("Special effects applied to 0 when you step on the hit material")]
			[Description("When the characters trod the hit material, special effects 0 set is exhibited")]
			[DefaultValue((Int32)(-1))]
			public Int32 SpEffectIdOnHit0 {
				get { return spEffectIdOnHit0; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for SpEffectIdOnHit0.");
					SetProperty(ref spEffectIdOnHit0, ref value, SpEffectIdOnHit0Property);
				}
			}

			/// <summary>Special effects 1 according to when you step on the hit material</summary>
			/// <remarks>
			/// Japanese short name: "ヒットマテリアルを踏んだ時にかかる特殊効果1", Google translated: "Special effects 1 according to when you step on the hit material".
			/// Japanese description: "キャラがヒットマテリアルを踏んだ時に、設定した特殊効果1が発揮される", Google translated: "Special effects 1 when the characters trod the hit material, you set are exhibited".
			/// </remarks>
			[ParameterTableRowAttribute("spEffectIdOnHit1", index: 2, minimum: -1, maximum: 1E+08, step: 1, order: 2000, unknown2: 1)]
			[DisplayName("Special effects 1 according to when you step on the hit material")]
			[Description("Special effects 1 when the characters trod the hit material, you set are exhibited")]
			[DefaultValue((Int32)(-1))]
			public Int32 SpEffectIdOnHit1 {
				get { return spEffectIdOnHit1; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for SpEffectIdOnHit1.");
					SetProperty(ref spEffectIdOnHit1, ref value, SpEffectIdOnHit1Property);
				}
			}

			/// <summary>Type the height of the foot effect</summary>
			/// <remarks>
			/// Japanese short name: "フットエフェクトの高さタイプ", Google translated: "Type the height of the foot effect".
			/// Japanese description: "フットエフェクトを発生させる高さ", Google translated: "Height to generate the foot effect".
			/// </remarks>
			[ParameterTableRowAttribute("footEffectHeightType:2", index: 3, minimum: 0, maximum: 2, step: 1, order: 3000, unknown2: 1)]
			[DisplayName("Type the height of the foot effect")]
			[Description("Height to generate the foot effect")]
			[DefaultValue((HitMaterialFootEffectHeight)0)]
			public HitMaterialFootEffectHeight FootEffectHeightType {
				get { return (HitMaterialFootEffectHeight)GetBitProperty(0, 2, FootEffectHeightTypeProperty); }
				set { SetBitProperty(0, 2, (int)value, FootEffectHeightTypeProperty); }
			}

			/// <summary>Orientation type of foot effect</summary>
			/// <remarks>
			/// Japanese short name: "フットエフェクトの向きタイプ", Google translated: "Orientation type of foot effect".
			/// Japanese description: "フットエフェクトの発生向き", Google translated: "Occurrence of foot orientation effect".
			/// </remarks>
			[ParameterTableRowAttribute("footEffectDirType:2", index: 4, minimum: 0, maximum: 2, step: 1, order: 3000, unknown2: 1)]
			[DisplayName("Orientation type of foot effect")]
			[Description("Occurrence of foot orientation effect")]
			[DefaultValue((HitMaterialFootEffectDirection)0)]
			public HitMaterialFootEffectDirection FootEffectDirType {
				get { return (HitMaterialFootEffectDirection)GetBitProperty(2, 2, FootEffectDirTypeProperty); }
				set { SetBitProperty(2, 2, (int)value, FootEffectDirTypeProperty); }
			}

			/// <summary>Type the height of the ground</summary>
			/// <remarks>
			/// Japanese short name: "地面の高さタイプ", Google translated: "Type the height of the ground".
			/// Japanese description: "水面などアイテムを浮かせるとき用", Google translated: "When for you float items such as water".
			/// </remarks>
			[ParameterTableRowAttribute("floorHeightType:2", index: 5, minimum: 0, maximum: 1, step: 1, order: 4000, unknown2: 1)]
			[DisplayName("Type the height of the ground")]
			[Description("When for you float items such as water")]
			[DefaultValue((HitMaterialFloorHeight)0)]
			public HitMaterialFloorHeight FloorHeightType {
				get { return (HitMaterialFloorHeight)GetBitProperty(4, 2, FloorHeightTypeProperty); }
				set { SetBitProperty(4, 2, (int)value, FloorHeightTypeProperty); }
			}

			/// <summary>pad</summary>
			/// <remarks>
			/// Japanese short name: "pad", Google translated: "pad".
			/// Japanese description: "pad", Google translated: "pad".
			/// </remarks>
			[ParameterTableRowAttribute("pad0[3]", index: 6, minimum: 0, maximum: 0, step: 0, order: 99999999, unknown2: 0)]
			[DisplayName("pad")]
			[Description("pad")]
			[Browsable(false)]
			public Byte[] Pad0 {
				get { return pad0; }
				set { SetProperty(ref pad0, ref value, Pad0Property); }
			}

			internal HitMaterial(ParameterTable table, int index, AssetLoader loader, int next)
				: base(table, index, loader) {
				BinaryReader reader = loader.Reader;

				AiVolumeRate = reader.ReadSingle();
				SpEffectIdOnHit0 = reader.ReadInt32();
				SpEffectIdOnHit1 = reader.ReadInt32();
				BitFields = reader.ReadBytes(1);
				Pad0 = reader.ReadBytes(3);
			}

			internal HitMaterial(ParameterTable table, int index)
				: base(table, index) {
				BitFields = new byte[1];
				AiVolumeRate = (Single)1;
				SpEffectIdOnHit0 = (Int32)(-1);
				SpEffectIdOnHit1 = (Int32)(-1);
				FootEffectHeightType = (HitMaterialFootEffectHeight)0;
				FootEffectDirType = (HitMaterialFootEffectDirection)0;
				FloorHeightType = (HitMaterialFloorHeight)0;
				Pad0 = new Byte[3];
			}

			public override void Write(BinaryWriter writer) {
				writer.Write(AiVolumeRate);
				writer.Write(SpEffectIdOnHit0);
				writer.Write(SpEffectIdOnHit1);
				writer.Write(BitFields);
				writer.Write(Pad0);
			}
		}
	}
}
