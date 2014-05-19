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
		/// Defined as "OBJECT_PARAM_ST" in Dark Souls in the file "ObjectParam.paramdef" (id 1Dh).
		/// </remarks>
		public class ObjectInfo : ParameterTableRow {
			public const string TableName = "OBJECT_PARAM_ST";

			Int16 hp, extRefTexId, materialId;
			UInt16 defense;
			Byte animBreakIdMax;
			SByte defaultLodParamId;
			Int32 breakSfxId;

			public static readonly PropertyInfo
				HpProperty = GetProperty<ObjectInfo>("Hp"),
				DefenseProperty = GetProperty<ObjectInfo>("Defense"),
				ExtRefTexIdProperty = GetProperty<ObjectInfo>("ExtRefTexId"),
				MaterialIdProperty = GetProperty<ObjectInfo>("MaterialId"),
				AnimBreakIdMaxProperty = GetProperty<ObjectInfo>("AnimBreakIdMax"),
				IsCamHitProperty = GetProperty<ObjectInfo>("IsCamHit"),
				IsBreakByPlayerCollideProperty = GetProperty<ObjectInfo>("IsBreakByPlayerCollide"),
				IsAnimBreakProperty = GetProperty<ObjectInfo>("IsAnimBreak"),
				IsPenetrationBulletHitProperty = GetProperty<ObjectInfo>("IsPenetrationBulletHit"),
				IsChrHitProperty = GetProperty<ObjectInfo>("IsChrHit"),
				IsAttackBacklashProperty = GetProperty<ObjectInfo>("IsAttackBacklash"),
				IsDisableBreakForFirstAppearProperty = GetProperty<ObjectInfo>("IsDisableBreakForFirstAppear"),
				IsLadderProperty = GetProperty<ObjectInfo>("IsLadder"),
				IsAnimPauseOnRemoPlayProperty = GetProperty<ObjectInfo>("IsAnimPauseOnRemoPlay"),
				IsDamageNoHitProperty = GetProperty<ObjectInfo>("IsDamageNoHit"),
				IsMoveObjProperty = GetProperty<ObjectInfo>("IsMoveObj"),
				Pad_1Property = GetProperty<ObjectInfo>("Pad_1"),
				DefaultLodParamIdProperty = GetProperty<ObjectInfo>("DefaultLodParamId"),
				BreakSfxIdProperty = GetProperty<ObjectInfo>("BreakSfxId");

			/// <summary>HP</summary>
			/// <remarks>
			/// Japanese short name: "HP", Google translated: "HP".
			/// Japanese description: "破壊までの耐久力(-1:破壊不可)", Google translated: "Durability to failure (-1 : breaking impossibility)".
			/// </remarks>
			[ParameterTableRowAttribute("hp", index: 0, minimum: -1, maximum: 9999, step: 1, order: 100, unknown2: 0)]
			[DisplayName("HP")]
			[Description("Durability to failure (-1 : breaking impossibility)")]
			[DefaultValue((Int16)(-1))]
			public Int16 Hp {
				get { return hp; }
				set {
					if ((double)value < -1 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 9999 for Hp.");
					SetProperty(ref hp, ref value, HpProperty);
				}
			}

			/// <summary>Phylactic power</summary>
			/// <remarks>
			/// Japanese short name: "防御力", Google translated: "Phylactic power".
			/// Japanese description: "この値以下の攻撃力はダメージなし", Google translated: "No damage attack power less than or equal to this value".
			/// </remarks>
			[ParameterTableRowAttribute("defense", index: 1, minimum: 0, maximum: 9999, step: 1, order: 200, unknown2: 0)]
			[DisplayName("Phylactic power")]
			[Description("No damage attack power less than or equal to this value")]
			[DefaultValue((UInt16)0)]
			public UInt16 Defense {
				get { return defense; }
				set {
					if ((double)value < 0 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9999 for Defense.");
					SetProperty(ref defense, ref value, DefenseProperty);
				}
			}

			/// <summary>External reference texture ID</summary>
			/// <remarks>
			/// Japanese short name: "外部参照テクスチャID", Google translated: "External reference texture ID".
			/// Japanese description: "mAA/mAA_????.tpf(-1:なし)(AA:エリア番号)", Google translated: "????. mAA / mAA_ tpf (-1: no ) (AA: area number )".
			/// </remarks>
			[ParameterTableRowAttribute("extRefTexId", index: 2, minimum: -1, maximum: 9999, step: 1, order: 500, unknown2: 0)]
			[DisplayName("External reference texture ID")]
			[Description("????. mAA / mAA_ tpf (-1: no ) (AA: area number )")]
			[DefaultValue((Int16)(-1))]
			public Int16 ExtRefTexId {
				get { return extRefTexId; }
				set {
					if ((double)value < -1 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 9999 for ExtRefTexId.");
					SetProperty(ref extRefTexId, ref value, ExtRefTexIdProperty);
				}
			}

			/// <summary>Material ID</summary>
			/// <remarks>
			/// Japanese short name: "材質ID", Google translated: "Material ID".
			/// Japanese description: "マテリアルID。床材質と同じ扱い。-1のときは今までと同じ挙動", Google translated: "Material ID. Treated the same as a floor material . The same behavior as ever if -1".
			/// </remarks>
			[ParameterTableRowAttribute("materialId", index: 3, minimum: -1, maximum: 999, step: 1, order: 800, unknown2: 0)]
			[DisplayName("Material ID")]
			[Description("Material ID. Treated the same as a floor material . The same behavior as ever if -1")]
			[DefaultValue((Int16)(-1))]
			public Int16 MaterialId {
				get { return materialId; }
				set {
					if ((double)value < -1 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999 for MaterialId.");
					SetProperty(ref materialId, ref value, MaterialIdProperty);
				}
			}

			/// <summary>Anime destruction ID maximum value</summary>
			/// <remarks>
			/// Japanese short name: "アニメ破壊ID最大値", Google translated: "Anime destruction ID maximum value".
			/// Japanese description: "アニメ破壊IDが0番から何番までか", Google translated: "Anime destruction ID or to what Numbers from 0".
			/// </remarks>
			[ParameterTableRowAttribute("animBreakIdMax", index: 4, minimum: 0, maximum: 99, step: 1, order: 700, unknown2: 0)]
			[DisplayName("Anime destruction ID maximum value")]
			[Description("Anime destruction ID or to what Numbers from 0")]
			[DefaultValue((Byte)0)]
			public Byte AnimBreakIdMax {
				get { return animBreakIdMax; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for AnimBreakIdMax.");
					SetProperty(ref animBreakIdMax, ref value, AnimBreakIdMaxProperty);
				}
			}

			/// <summary>Camera is hit</summary>
			/// <remarks>
			/// Japanese short name: "カメラが当たるか", Google translated: "Camera is hit".
			/// Japanese description: "カメラが当たるか(0:当たらない, 1:当たる)", Google translated: "Camera is hit (0: do not hit , 1: hit )".
			/// </remarks>
			[ParameterTableRowAttribute("isCamHit:1", index: 5, minimum: 0, maximum: 1, step: 1, order: 300, unknown2: 1)]
			[DisplayName("Camera is hit")]
			[Description("Camera is hit (0: do not hit , 1: hit )")]
			[DefaultValue(false)]
			public Boolean IsCamHit {
				get { return GetBitProperty(0, 1, IsCamHitProperty) != 0; }
				set { SetBitProperty(0, 1, value ? 1 : 0, IsCamHitProperty); }
			}

			/// <summary>Or corruption on player collision</summary>
			/// <remarks>
			/// Japanese short name: "プレイヤ衝突で壊れるか", Google translated: "Or corruption on player collision".
			/// Japanese description: "プレイヤが接触したときに壊れ(0:ない, 1:る)", Google translated: "( : No , 1 : Ru 0 ) to break when the player is in contact".
			/// </remarks>
			[ParameterTableRowAttribute("isBreakByPlayerCollide:1", index: 6, minimum: 0, maximum: 1, step: 1, order: 400, unknown2: 1)]
			[DisplayName("Or corruption on player collision")]
			[Description("( : No , 1 : Ru 0 ) to break when the player is in contact")]
			[DefaultValue(false)]
			public Boolean IsBreakByPlayerCollide {
				get { return GetBitProperty(1, 1, IsBreakByPlayerCollideProperty) != 0; }
				set { SetBitProperty(1, 1, value ? 1 : 0, IsBreakByPlayerCollideProperty); }
			}

			/// <summary>Anime or destruction</summary>
			/// <remarks>
			/// Japanese short name: "アニメ破壊か", Google translated: "Anime or destruction".
			/// Japanese description: "アニメ破壊か(0:物理破壊, 1:アニメ破壊)", Google translated: "( : Physical destruction , 1 : Anime destruction 0) or anime destruction".
			/// </remarks>
			[ParameterTableRowAttribute("isAnimBreak:1", index: 7, minimum: 0, maximum: 1, step: 1, order: 600, unknown2: 1)]
			[DisplayName("Anime or destruction")]
			[Description("( : Physical destruction , 1 : Anime destruction 0) or anime destruction")]
			[DefaultValue(false)]
			public Boolean IsAnimBreak {
				get { return GetBitProperty(2, 1, IsAnimBreakProperty) != 0; }
				set { SetBitProperty(2, 1, value ? 1 : 0, IsAnimBreakProperty); }
			}

			/// <summary>Through bullet or hits</summary>
			/// <remarks>
			/// Japanese short name: "貫通弾丸が当たるか", Google translated: "Through bullet or hits".
			/// Japanese description: "貫通弾丸が当たるか(0:当たらない, 1:当たる)", Google translated: "Through bullet or hits (0: do not hit , 1: hit )".
			/// </remarks>
			[ParameterTableRowAttribute("isPenetrationBulletHit:1", index: 8, minimum: 0, maximum: 1, step: 1, order: 900, unknown2: 1)]
			[DisplayName("Through bullet or hits")]
			[Description("Through bullet or hits (0: do not hit , 1: hit )")]
			[DefaultValue(false)]
			public Boolean IsPenetrationBulletHit {
				get { return GetBitProperty(3, 1, IsPenetrationBulletHitProperty) != 0; }
				set { SetBitProperty(3, 1, value ? 1 : 0, IsPenetrationBulletHitProperty); }
			}

			/// <summary>Character or hit</summary>
			/// <remarks>
			/// Japanese short name: "キャラが当たるか", Google translated: "Character or hit".
			/// Japanese description: "キャラが当たるか(0:当たらない, 1:当たる)", Google translated: "Character or hit (0: do not hit , 1: hit )".
			/// </remarks>
			[ParameterTableRowAttribute("isChrHit:1", index: 9, minimum: 0, maximum: 1, step: 1, order: 350, unknown2: 1)]
			[DisplayName("Character or hit")]
			[Description("Character or hit (0: do not hit , 1: hit )")]
			[DefaultValue(true)]
			public Boolean IsChrHit {
				get { return GetBitProperty(4, 1, IsChrHitProperty) != 0; }
				set { SetBitProperty(4, 1, value ? 1 : 0, IsChrHitProperty); }
			}

			/// <summary>Do play the attack</summary>
			/// <remarks>
			/// Japanese short name: "攻撃を弾くか", Google translated: "Do play the attack".
			/// Japanese description: "攻撃を弾くか(0:弾かない, 1:弾く)", Google translated: "The ( : do not play , 1: 0 play ) or play the attack".
			/// </remarks>
			[ParameterTableRowAttribute("isAttackBacklash:1", index: 10, minimum: 0, maximum: 1, step: 1, order: 950, unknown2: 1)]
			[DisplayName("Do play the attack")]
			[Description("The ( : do not play , 1: 0 play ) or play the attack")]
			[DefaultValue(true)]
			public Boolean IsAttackBacklash {
				get { return GetBitProperty(5, 1, IsAttackBacklashProperty) != 0; }
				set { SetBitProperty(5, 1, value ? 1 : 0, IsAttackBacklashProperty); }
			}

			/// <summary>Initial appearance for breaking ban</summary>
			/// <remarks>
			/// Japanese short name: "初期出現用破壊禁止", Google translated: "Initial appearance for breaking ban".
			/// Japanese description: "プレイヤの初期出現で壊れ(0:る, 1:ない)", Google translated: "( : Ru , 1: 0 ) is broken in the early appearance of the player".
			/// </remarks>
			[ParameterTableRowAttribute("isDisableBreakForFirstAppear:1", index: 11, minimum: 0, maximum: 1, step: 1, order: 450, unknown2: 1)]
			[DisplayName("Initial appearance for breaking ban")]
			[Description("( : Ru , 1: 0 ) is broken in the early appearance of the player")]
			[DefaultValue(false)]
			public Boolean IsDisableBreakForFirstAppear {
				get { return GetBitProperty(6, 1, IsDisableBreakForFirstAppearProperty) != 0; }
				set { SetBitProperty(6, 1, value ? 1 : 0, IsDisableBreakForFirstAppearProperty); }
			}

			/// <summary>Or ladder</summary>
			/// <remarks>
			/// Japanese short name: "ハシゴか", Google translated: "Or ladder".
			/// Japanese description: "ハシゴか(0:ちがう, 1:そう)", Google translated: "Or ladder (0: no, 1 : yes )".
			/// </remarks>
			[ParameterTableRowAttribute("isLadder:1", index: 12, minimum: 0, maximum: 1, step: 1, order: 970, unknown2: 1)]
			[DisplayName("Or ladder")]
			[Description("Or ladder (0: no, 1 : yes )")]
			[DefaultValue(false)]
			public Boolean IsLadder {
				get { return GetBitProperty(7, 1, IsLadderProperty) != 0; }
				set { SetBitProperty(7, 1, value ? 1 : 0, IsLadderProperty); }
			}

			/// <summary>You can stop the poly play animation</summary>
			/// <remarks>
			/// Japanese short name: "ポリ劇中アニメを停止するか", Google translated: "You can stop the poly play animation".
			/// Japanese description: "ポリ劇中アニメを停止するか(0:しない, 1:する)", Google translated: "You can stop the poly drama Animation (0: no , 1: to )".
			/// </remarks>
			[ParameterTableRowAttribute("isAnimPauseOnRemoPlay:1", index: 13, minimum: 0, maximum: 1, step: 1, order: 980, unknown2: 1)]
			[DisplayName("You can stop the poly play animation")]
			[Description("You can stop the poly drama Animation (0: no , 1: to )")]
			[DefaultValue(false)]
			public Boolean IsAnimPauseOnRemoPlay {
				get { return GetBitProperty(8, 1, IsAnimPauseOnRemoPlayProperty) != 0; }
				set { SetBitProperty(8, 1, value ? 1 : 0, IsAnimPauseOnRemoPlayProperty); }
			}

			/// <summary>Damage or does not hit</summary>
			/// <remarks>
			/// Japanese short name: "ダメージが当たらないか", Google translated: "Damage or does not hit".
			/// Japanese description: "ダメージが当たらない(0:当たる, 1:当たらない)", Google translated: "The ( : hit , 1: not hit 0) damage does not hit".
			/// </remarks>
			[ParameterTableRowAttribute("isDamageNoHit:1", index: 14, minimum: 0, maximum: 1, step: 1, order: 375, unknown2: 1)]
			[DisplayName("Damage or does not hit")]
			[Description("The ( : hit , 1: not hit 0) damage does not hit")]
			[DefaultValue(false)]
			public Boolean IsDamageNoHit {
				get { return GetBitProperty(9, 1, IsDamageNoHitProperty) != 0; }
				set { SetBitProperty(9, 1, value ? 1 : 0, IsDamageNoHitProperty); }
			}

			/// <summary>Or moving objects</summary>
			/// <remarks>
			/// Japanese short name: "移動オブジェか", Google translated: "Or moving objects".
			/// Japanese description: "移動オブジェか(0:ちがう, 1:そう)", Google translated: "Or moving objects (0: no, 1 : yes )".
			/// </remarks>
			[ParameterTableRowAttribute("isMoveObj:1", index: 15, minimum: 0, maximum: 1, step: 1, order: 975, unknown2: 1)]
			[DisplayName("Or moving objects")]
			[Description("Or moving objects (0: no, 1 : yes )")]
			[DefaultValue(false)]
			public Boolean IsMoveObj {
				get { return GetBitProperty(10, 1, IsMoveObjProperty) != 0; }
				set { SetBitProperty(10, 1, value ? 1 : 0, IsMoveObjProperty); }
			}

			/// <summary>Padding</summary>
			/// <remarks>
			/// Japanese short name: "パディング", Google translated: "Padding".
			/// Japanese description: "pad", Google translated: "pad".
			/// </remarks>
			[ParameterTableRowAttribute("pad_1:5", index: 16, minimum: 0, maximum: 0, step: 0, order: 1101, unknown2: 0)]
			[DisplayName("Padding")]
			[Description("pad")]
			[DefaultValue((Byte)0)]
			[Browsable(false)]
			public Byte Pad_1 {
				get { return (Byte)GetBitProperty(11, 5, Pad_1Property); }
				set {
					if ((double)value < 0 || (double)value > 0)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 0 for Pad_1.");
					SetBitProperty(11, 5, (int)value, Pad_1Property);
				}
			}

			/// <summary>Default LOD Parham ID</summary>
			/// <remarks>
			/// Japanese short name: "デフォルトLODパラムID", Google translated: "Default LOD Parham ID".
			/// Japanese description: "デフォルトLODパラムID(-1：なし)", Google translated: "Default LOD Parham ID, (-1 : none)".
			/// </remarks>
			[ParameterTableRowAttribute("defaultLodParamId", index: 17, minimum: -1, maximum: 127, step: 1, order: 1100, unknown2: 0)]
			[DisplayName("Default LOD Parham ID")]
			[Description("Default LOD Parham ID, (-1 : none)")]
			[DefaultValue((SByte)(-1))]
			public SByte DefaultLodParamId {
				get { return defaultLodParamId; }
				set {
					if ((double)value < -1 || (double)value > 127)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 127 for DefaultLodParamId.");
					SetProperty(ref defaultLodParamId, ref value, DefaultLodParamIdProperty);
				}
			}

			/// <summary>SFXID at break</summary>
			/// <remarks>
			/// Japanese short name: "破壊時SFXID", Google translated: "SFXID at break".
			/// Japanese description: "オブジェ破壊時のSFXID(-1:デフォルト(80))", Google translated: "SFXID of objects at break ( Default: -1 ( 80) )".
			/// </remarks>
			[ParameterTableRowAttribute("breakSfxId", index: 18, minimum: -1, maximum: 1E+09, step: 1, order: 1000, unknown2: 0)]
			[DisplayName("SFXID at break")]
			[Description("SFXID of objects at break ( Default: -1 ( 80) )")]
			[DefaultValue((Int32)(-1))]
			public Int32 BreakSfxId {
				get { return breakSfxId; }
				set {
					if ((double)value < -1 || (double)value > 1E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+09 for BreakSfxId.");
					SetProperty(ref breakSfxId, ref value, BreakSfxIdProperty);
				}
			}

			internal ObjectInfo(ParameterTable table, int index, AssetLoader loader, int next)
				: base(table, index, loader) {
				BinaryReader reader = loader.Reader;

				Hp = reader.ReadInt16();
				Defense = reader.ReadUInt16();
				ExtRefTexId = reader.ReadInt16();
				MaterialId = reader.ReadInt16();
				AnimBreakIdMax = reader.ReadByte();
				BitFields = reader.ReadBytes(2);
				DefaultLodParamId = reader.ReadSByte();
				BreakSfxId = reader.ReadInt32();
			}

			internal ObjectInfo(ParameterTable table, int index)
				: base(table, index) {
				BitFields = new byte[2];
				Hp = (Int16)(-1);
				Defense = (UInt16)0;
				ExtRefTexId = (Int16)(-1);
				MaterialId = (Int16)(-1);
				AnimBreakIdMax = (Byte)0;
				IsCamHit = false;
				IsBreakByPlayerCollide = false;
				IsAnimBreak = false;
				IsPenetrationBulletHit = false;
				IsChrHit = true;
				IsAttackBacklash = true;
				IsDisableBreakForFirstAppear = false;
				IsLadder = false;
				IsAnimPauseOnRemoPlay = false;
				IsDamageNoHit = false;
				IsMoveObj = false;
				Pad_1 = (Byte)0;
				DefaultLodParamId = (SByte)(-1);
				BreakSfxId = (Int32)(-1);
			}

			public override void Write(BinaryWriter writer) {
				writer.Write(Hp);
				writer.Write(Defense);
				writer.Write(ExtRefTexId);
				writer.Write(MaterialId);
				writer.Write(AnimBreakIdMax);
				writer.Write(BitFields);
				writer.Write(DefaultLodParamId);
				writer.Write(BreakSfxId);
			}
		}
	}
}
