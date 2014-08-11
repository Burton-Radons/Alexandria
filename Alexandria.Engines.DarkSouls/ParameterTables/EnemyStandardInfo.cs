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
		/// <remarks>From "EnemyStandardInfo.paramdef".</remarks>
		public class EnemyStandardInfo : ParameterTableRow {
			/// <summary>The name of the table in the file.</summary>
			public const string TableName = "ENEMY_STANDARD_INFO_BANK";

			EnemyBehaviorId enemyBehaviorID;
			UInt16 hP, attackPower, stamina, staminaRecover, staminaConsumption, deffenct_Phys;
			CharacterTyep chrType;
			Single hitHeight, hitRadius, weight, dynamicFriction, staticFriction, rotY_per_Second;
			Int32 upperDefState, actionDefState;
			Byte[] reserve0, reserve_last, reserve_last2;
			Byte rotY_per_Second_old, enableSideStep, useRagdollHit;

			/// <summary>A property of the class.</summary>
			public static readonly PropertyInfo
				EnemyBehaviorIDProperty = GetProperty<EnemyStandardInfo>("EnemyBehaviorID"),
				HPProperty = GetProperty<EnemyStandardInfo>("HP"),
				AttackPowerProperty = GetProperty<EnemyStandardInfo>("AttackPower"),
				ChrTypeProperty = GetProperty<EnemyStandardInfo>("ChrType"),
				HitHeightProperty = GetProperty<EnemyStandardInfo>("HitHeight"),
				HitRadiusProperty = GetProperty<EnemyStandardInfo>("HitRadius"),
				WeightProperty = GetProperty<EnemyStandardInfo>("Weight"),
				DynamicFrictionProperty = GetProperty<EnemyStandardInfo>("DynamicFriction"),
				StaticFrictionProperty = GetProperty<EnemyStandardInfo>("StaticFriction"),
				UpperDefStateProperty = GetProperty<EnemyStandardInfo>("UpperDefState"),
				ActionDefStateProperty = GetProperty<EnemyStandardInfo>("ActionDefState"),
				RotY_per_SecondProperty = GetProperty<EnemyStandardInfo>("RotY_per_Second"),
				Reserve0Property = GetProperty<EnemyStandardInfo>("Reserve0"),
				RotY_per_Second_oldProperty = GetProperty<EnemyStandardInfo>("RotY_per_Second_old"),
				EnableSideStepProperty = GetProperty<EnemyStandardInfo>("EnableSideStep"),
				UseRagdollHitProperty = GetProperty<EnemyStandardInfo>("UseRagdollHit"),
				Reserve_lastProperty = GetProperty<EnemyStandardInfo>("Reserve_last"),
				StaminaProperty = GetProperty<EnemyStandardInfo>("Stamina"),
				StaminaRecoverProperty = GetProperty<EnemyStandardInfo>("StaminaRecover"),
				StaminaConsumptionProperty = GetProperty<EnemyStandardInfo>("StaminaConsumption"),
				Deffenct_PhysProperty = GetProperty<EnemyStandardInfo>("Deffenct_Phys"),
				Reserve_last2Property = GetProperty<EnemyStandardInfo>("Reserve_last2");

			/// <summary>Behavior id</summary>
			/// <remarks>
			/// Japanese short name: "挙動ｉｄ", Google translated: "Behavior id".
			/// Japanese description: "敵の挙動ＩＤ", Google translated: "Behavior ID of the enemy".
			/// </remarks>
			[ParameterTableRowAttribute("EnemyBehaviorID", index: 0, minimum: 0, maximum: 0, step: 1, sortOrder: 1, unknown2: 1)]
			[DisplayName("Behavior id")]
			[Description("Behavior ID of the enemy")]
			[DefaultValue((EnemyBehaviorId)0)]
			public EnemyBehaviorId EnemyBehaviorID {
				get { return enemyBehaviorID; }
				set { SetProperty(ref enemyBehaviorID, ref value, EnemyBehaviorIDProperty); }
			}

			/// <summary>Hit point</summary>
			/// <remarks>
			/// Japanese short name: "ヒットポイント", Google translated: "Hit point".
			/// Japanese description: "ヒットポイント", Google translated: "Hit point".
			/// </remarks>
			[ParameterTableRowAttribute("HP", index: 1, minimum: 0, maximum: 100, step: 1, sortOrder: 2, unknown2: 1)]
			[DisplayName("Hit point")]
			[Description("Hit point")]
			[DefaultValue((UInt16)1)]
			public UInt16 HP {
				get { return hP; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for HP.");
					SetProperty(ref hP, ref value, HPProperty);
				}
			}

			/// <summary>Offensive power</summary>
			/// <remarks>
			/// Japanese short name: "攻撃力", Google translated: "Offensive power".
			/// Japanese description: "攻撃力（プロト専用）", Google translated: "Attack power (prototype only)".
			/// </remarks>
			[ParameterTableRowAttribute("AttackPower", index: 2, minimum: 0, maximum: 100, step: 1, sortOrder: 3, unknown2: 0)]
			[DisplayName("Offensive power")]
			[Description("Attack power (prototype only)")]
			[DefaultValue((UInt16)1)]
			public UInt16 AttackPower {
				get { return attackPower; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for AttackPower.");
					SetProperty(ref attackPower, ref value, AttackPowerProperty);
				}
			}

			/// <summary>Character type</summary>
			/// <remarks>
			/// Japanese short name: "キャラタイプ", Google translated: "Character type".
			/// Japanese description: "キャラタイプ", Google translated: "Character type".
			/// </remarks>
			[ParameterTableRowAttribute("ChrType", index: 3, minimum: 0, maximum: 7, step: 1, sortOrder: 4, unknown2: 0)]
			[DisplayName("Character type")]
			[Description("Character type")]
			[DefaultValue((CharacterTyep)5)]
			public CharacterTyep ChrType {
				get { return chrType; }
				set { SetProperty(ref chrType, ref value, ChrTypeProperty); }
			}

			/// <summary>Per height [m]</summary>
			/// <remarks>
			/// Japanese short name: "あたりの高さ[m]", Google translated: "Per height [m]".
			/// Japanese description: "あたりの高さ（直径以上のサイズを指定してください）", Google translated: "( Please specify the size of the diameter or more ) of height per".
			/// </remarks>
			[ParameterTableRowAttribute("HitHeight", index: 4, minimum: 0, maximum: 100, step: 0.1, sortOrder: 5, unknown2: 0)]
			[DisplayName("Per height [m]")]
			[Description("( Please specify the size of the diameter or more ) of height per")]
			[DefaultValue((Single)2)]
			public Single HitHeight {
				get { return hitHeight; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for HitHeight.");
					SetProperty(ref hitHeight, ref value, HitHeightProperty);
				}
			}

			/// <summary>Radius of around [m]</summary>
			/// <remarks>
			/// Japanese short name: "あたりの半径[m]", Google translated: "Radius of around [m]".
			/// Japanese description: "あたりの半径", Google translated: "Radius per".
			/// </remarks>
			[ParameterTableRowAttribute("HitRadius", index: 5, minimum: 0, maximum: 50, step: 0.1, sortOrder: 6, unknown2: 0)]
			[DisplayName("Radius of around [m]")]
			[Description("Radius per")]
			[DefaultValue((Single)0.4)]
			public Single HitRadius {
				get { return hitRadius; }
				set {
					if ((double)value < 0 || (double)value > 50)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 50 for HitRadius.");
					SetProperty(ref hitRadius, ref value, HitRadiusProperty);
				}
			}

			/// <summary>Weight [kg]</summary>
			/// <remarks>
			/// Japanese short name: "重さ[kg]", Google translated: "Weight [kg]".
			/// Japanese description: "キャラの重さ", Google translated: "The weight of the character".
			/// </remarks>
			[ParameterTableRowAttribute("Weight", index: 6, minimum: 0, maximum: 1000000, step: 1, sortOrder: 7, unknown2: 0)]
			[DisplayName("Weight [kg]")]
			[Description("The weight of the character")]
			[DefaultValue((Single)60)]
			public Single Weight {
				get { return weight; }
				set {
					if ((double)value < 0 || (double)value > 1000000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1000000 for Weight.");
					SetProperty(ref weight, ref value, WeightProperty);
				}
			}

			/// <summary>Kinetic friction force</summary>
			/// <remarks>
			/// Japanese short name: "動摩擦力", Google translated: "Kinetic friction force".
			/// Japanese description: "動摩擦力", Google translated: "Kinetic friction force".
			/// </remarks>
			[ParameterTableRowAttribute("DynamicFriction", index: 7, minimum: 0, maximum: 10, step: 0.01, sortOrder: 8, unknown2: 0)]
			[DisplayName("Kinetic friction force")]
			[Description("Kinetic friction force")]
			[DefaultValue((Single)0)]
			public Single DynamicFriction {
				get { return dynamicFriction; }
				set {
					if ((double)value < 0 || (double)value > 10)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 10 for DynamicFriction.");
					SetProperty(ref dynamicFriction, ref value, DynamicFrictionProperty);
				}
			}

			/// <summary>Static friction force</summary>
			/// <remarks>
			/// Japanese short name: "静摩擦力", Google translated: "Static friction force".
			/// Japanese description: "静止摩擦力", Google translated: "Static frictional force".
			/// </remarks>
			[ParameterTableRowAttribute("StaticFriction", index: 8, minimum: 0, maximum: 10, step: 0.01, sortOrder: 9, unknown2: 0)]
			[DisplayName("Static friction force")]
			[Description("Static frictional force")]
			[DefaultValue((Single)0)]
			public Single StaticFriction {
				get { return staticFriction; }
				set {
					if ((double)value < 0 || (double)value > 10)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 10 for StaticFriction.");
					SetProperty(ref staticFriction, ref value, StaticFrictionProperty);
				}
			}

			/// <summary>Upper body initial state</summary>
			/// <remarks>
			/// Japanese short name: "上半身初期状態", Google translated: "Upper body initial state".
			/// Japanese description: "上半身初期状態（PG入力）", Google translated: "Upper body initial state (PG input )".
			/// </remarks>
			[ParameterTableRowAttribute("UpperDefState", index: 9, minimum: 0, maximum: 100000, step: 1, sortOrder: 10, unknown2: 0)]
			[DisplayName("Upper body initial state")]
			[Description("Upper body initial state (PG input )")]
			[DefaultValue((Int32)0)]
			public Int32 UpperDefState {
				get { return upperDefState; }
				set {
					if ((double)value < 0 || (double)value > 100000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100000 for UpperDefState.");
					SetProperty(ref upperDefState, ref value, UpperDefStateProperty);
				}
			}

			/// <summary>Action initial state</summary>
			/// <remarks>
			/// Japanese short name: "アクション初期状態", Google translated: "Action initial state".
			/// Japanese description: "アクション初期状態（PG入力）", Google translated: "Action initial state (PG input )".
			/// </remarks>
			[ParameterTableRowAttribute("ActionDefState", index: 10, minimum: 0, maximum: 100000, step: 1, sortOrder: 11, unknown2: 0)]
			[DisplayName("Action initial state")]
			[Description("Action initial state (PG input )")]
			[DefaultValue((Int32)0)]
			public Int32 ActionDefState {
				get { return actionDefState; }
				set {
					if ((double)value < 0 || (double)value > 100000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100000 for ActionDefState.");
					SetProperty(ref actionDefState, ref value, ActionDefStateProperty);
				}
			}

			/// <summary>Angle can pivot unit time [deg / s]</summary>
			/// <remarks>
			/// Japanese short name: "単位時間当たり旋回できる角度[deg/s]", Google translated: "Angle can pivot unit time [deg / s]".
			/// Japanese description: "単位時間当たりのＹ軸旋回角度[deg/s]", Google translated: "Y-axis turning angle per unit time [deg / s]".
			/// </remarks>
			[ParameterTableRowAttribute("RotY_per_Second", index: 11, minimum: 0, maximum: 10000, step: 1, sortOrder: 12, unknown2: 1)]
			[DisplayName("Angle can pivot unit time [deg / s]")]
			[Description("Y-axis turning angle per unit time [deg / s]")]
			[DefaultValue((Single)10)]
			public Single RotY_per_Second {
				get { return rotY_per_Second; }
				set {
					if ((double)value < 0 || (double)value > 10000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 10000 for RotY_per_Second.");
					SetProperty(ref rotY_per_Second, ref value, RotY_per_SecondProperty);
				}
			}

			/// <summary>Reservation</summary>
			/// <remarks>
			/// Japanese short name: "予約", Google translated: "Reservation".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("reserve0[20]", index: 12, minimum: 0, maximum: 0, step: 0, sortOrder: 23, unknown2: 0)]
			[DisplayName("Reservation")]
			[Description("")]
			public Byte[] Reserve0 {
				get { return reserve0; }
				set { SetProperty(ref reserve0, ref value, Reserve0Property); }
			}

			/// <summary>Unused</summary>
			/// <remarks>
			/// Japanese short name: "未使用", Google translated: "Unused".
			/// Japanese description: "未使用", Google translated: "Unused".
			/// </remarks>
			[ParameterTableRowAttribute("RotY_per_Second_old", index: 13, minimum: 0, maximum: 180, step: 1, sortOrder: 13, unknown2: 1)]
			[DisplayName("Unused")]
			[Description("Unused")]
			[DefaultValue((Byte)0)]
			public Byte RotY_per_Second_old {
				get { return rotY_per_Second_old; }
				set {
					if ((double)value < 0 || (double)value > 180)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 180 for RotY_per_Second_old.");
					SetProperty(ref rotY_per_Second_old, ref value, RotY_per_Second_oldProperty);
				}
			}

			/// <summary>You can move left and right</summary>
			/// <remarks>
			/// Japanese short name: "左右移動できるか", Google translated: "You can move left and right".
			/// Japanese description: "左右移動できるか", Google translated: "You can move left and right".
			/// </remarks>
			[ParameterTableRowAttribute("EnableSideStep", index: 14, minimum: 0, maximum: 1, step: 1, sortOrder: 14, unknown2: 1)]
			[DisplayName("You can move left and right")]
			[Description("You can move left and right")]
			[DefaultValue((Byte)0)]
			public Byte EnableSideStep {
				get { return enableSideStep; }
				set {
					if ((double)value < 0 || (double)value > 1)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1 for EnableSideStep.");
					SetProperty(ref enableSideStep, ref value, EnableSideStepProperty);
				}
			}

			/// <summary>You can use the ragdoll to character per</summary>
			/// <remarks>
			/// Japanese short name: "キャラあたりにラグドールを使用するか", Google translated: "You can use the ragdoll to character per".
			/// Japanese description: "キャラあたりにラグドールを使用するか", Google translated: "You can use the ragdoll to character per".
			/// </remarks>
			[ParameterTableRowAttribute("UseRagdollHit", index: 15, minimum: 0, maximum: 1, step: 1, sortOrder: 15, unknown2: 1)]
			[DisplayName("You can use the ragdoll to character per")]
			[Description("You can use the ragdoll to character per")]
			[DefaultValue((Byte)0)]
			public Byte UseRagdollHit {
				get { return useRagdollHit; }
				set {
					if ((double)value < 0 || (double)value > 1)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1 for UseRagdollHit.");
					SetProperty(ref useRagdollHit, ref value, UseRagdollHitProperty);
				}
			}

			/// <summary>Reservation</summary>
			/// <remarks>
			/// Japanese short name: "予約", Google translated: "Reservation".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("reserve_last[5]", index: 16, minimum: 0, maximum: 0, step: 0, sortOrder: 24, unknown2: 0)]
			[DisplayName("Reservation")]
			[Description("")]
			public Byte[] Reserve_last {
				get { return reserve_last; }
				set { SetProperty(ref reserve_last, ref value, Reserve_lastProperty); }
			}

			/// <summary>Stamina amount</summary>
			/// <remarks>
			/// Japanese short name: "スタミナ量", Google translated: "Stamina amount".
			/// Japanese description: "スタミナ総量", Google translated: "Stamina total".
			/// </remarks>
			[ParameterTableRowAttribute("stamina", index: 17, minimum: 0, maximum: 999, step: 1, sortOrder: 16, unknown2: 1)]
			[DisplayName("Stamina amount")]
			[Description("Stamina total")]
			[DefaultValue((UInt16)0)]
			public UInt16 Stamina {
				get { return stamina; }
				set {
					if ((double)value < 0 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for Stamina.");
					SetProperty(ref stamina, ref value, StaminaProperty);
				}
			}

			/// <summary>Stamina recovery</summary>
			/// <remarks>
			/// Japanese short name: "スタミナ回復", Google translated: "Stamina recovery".
			/// Japanese description: "1秒間あたりのスタミナ回復量", Google translated: "Stamina recovery amount per second".
			/// </remarks>
			[ParameterTableRowAttribute("staminaRecover", index: 18, minimum: 0, maximum: 999, step: 1, sortOrder: 17, unknown2: 1)]
			[DisplayName("Stamina recovery")]
			[Description("Stamina recovery amount per second")]
			[DefaultValue((UInt16)0)]
			public UInt16 StaminaRecover {
				get { return staminaRecover; }
				set {
					if ((double)value < 0 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for StaminaRecover.");
					SetProperty(ref staminaRecover, ref value, StaminaRecoverProperty);
				}
			}

			/// <summary>Stamina basic consumption</summary>
			/// <remarks>
			/// Japanese short name: "スタミナ基本消費", Google translated: "Stamina basic consumption".
			/// Japanese description: "攻撃、ガード時に使用するスタミナ消費の基本値", Google translated: "Attack , the basic value of stamina consumption to be used to guard when".
			/// </remarks>
			[ParameterTableRowAttribute("staminaConsumption", index: 19, minimum: 0, maximum: 999, step: 1, sortOrder: 18, unknown2: 1)]
			[DisplayName("Stamina basic consumption")]
			[Description("Attack , the basic value of stamina consumption to be used to guard when")]
			[DefaultValue((UInt16)0)]
			public UInt16 StaminaConsumption {
				get { return staminaConsumption; }
				set {
					if ((double)value < 0 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for StaminaConsumption.");
					SetProperty(ref staminaConsumption, ref value, StaminaConsumptionProperty);
				}
			}

			/// <summary>Physical Def</summary>
			/// <remarks>
			/// Japanese short name: "物理防御力", Google translated: "Physical Def".
			/// Japanese description: "物理攻撃に対するダメージ減少基本値", Google translated: "Damage Reduction base value for a physical attack".
			/// </remarks>
			[ParameterTableRowAttribute("deffenct_Phys", index: 20, minimum: 0, maximum: 999, step: 1, sortOrder: 19, unknown2: 1)]
			[DisplayName("Physical Def")]
			[Description("Damage Reduction base value for a physical attack")]
			[DefaultValue((UInt16)0)]
			public UInt16 Deffenct_Phys {
				get { return deffenct_Phys; }
				set {
					if ((double)value < 0 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for Deffenct_Phys.");
					SetProperty(ref deffenct_Phys, ref value, Deffenct_PhysProperty);
				}
			}

			/// <summary>Reserved 1</summary>
			/// <remarks>
			/// Japanese short name: "予約1", Google translated: "Reserved 1".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("reserve_last2[48]", index: 21, minimum: 0, maximum: 0, step: 0, sortOrder: 25, unknown2: 0)]
			[DisplayName("Reserved 1")]
			[Description("")]
			public Byte[] Reserve_last2 {
				get { return reserve_last2; }
				set { SetProperty(ref reserve_last2, ref value, Reserve_last2Property); }
			}

			internal EnemyStandardInfo(ParameterTable table, int index, AssetLoader loader, int next)
				: base(table, index, loader) {
				BinaryReader reader = loader.Reader;

				EnemyBehaviorID = (EnemyBehaviorId)reader.ReadInt32();
				HP = reader.ReadUInt16();
				AttackPower = reader.ReadUInt16();
				ChrType = (CharacterTyep)reader.ReadInt32();
				HitHeight = reader.ReadSingle();
				HitRadius = reader.ReadSingle();
				Weight = reader.ReadSingle();
				DynamicFriction = reader.ReadSingle();
				StaticFriction = reader.ReadSingle();
				UpperDefState = reader.ReadInt32();
				ActionDefState = reader.ReadInt32();
				RotY_per_Second = reader.ReadSingle();
				Reserve0 = reader.ReadBytes(20);
				RotY_per_Second_old = reader.ReadByte();
				EnableSideStep = reader.ReadByte();
				UseRagdollHit = reader.ReadByte();
				Reserve_last = reader.ReadBytes(5);
				Stamina = reader.ReadUInt16();
				StaminaRecover = reader.ReadUInt16();
				StaminaConsumption = reader.ReadUInt16();
				Deffenct_Phys = reader.ReadUInt16();
				Reserve_last2 = reader.ReadBytes(48);
			}

			internal EnemyStandardInfo(ParameterTable table, int index)
				: base(table, index) {
				EnemyBehaviorID = (EnemyBehaviorId)0;
				HP = (UInt16)1;
				AttackPower = (UInt16)1;
				ChrType = (CharacterTyep)5;
				HitHeight = (Single)2;
				HitRadius = (Single)0.4;
				Weight = (Single)60;
				DynamicFriction = (Single)0;
				StaticFriction = (Single)0;
				UpperDefState = (Int32)0;
				ActionDefState = (Int32)0;
				RotY_per_Second = (Single)10;
				Reserve0 = new Byte[20];
				RotY_per_Second_old = (Byte)0;
				EnableSideStep = (Byte)0;
				UseRagdollHit = (Byte)0;
				Reserve_last = new Byte[5];
				Stamina = (UInt16)0;
				StaminaRecover = (UInt16)0;
				StaminaConsumption = (UInt16)0;
				Deffenct_Phys = (UInt16)0;
				Reserve_last2 = new Byte[48];
			}

			/// <summary>Write the row to the writer.</summary>
			/// <param name="writer"></param>
			public override void Write(BinaryWriter writer) {
				writer.Write((Int32)EnemyBehaviorID);
				writer.Write(HP);
				writer.Write(AttackPower);
				writer.Write((Int32)ChrType);
				writer.Write(HitHeight);
				writer.Write(HitRadius);
				writer.Write(Weight);
				writer.Write(DynamicFriction);
				writer.Write(StaticFriction);
				writer.Write(UpperDefState);
				writer.Write(ActionDefState);
				writer.Write(RotY_per_Second);
				writer.Write(Reserve0);
				writer.Write(RotY_per_Second_old);
				writer.Write(EnableSideStep);
				writer.Write(UseRagdollHit);
				writer.Write(Reserve_last);
				writer.Write(Stamina);
				writer.Write(StaminaRecover);
				writer.Write(StaminaConsumption);
				writer.Write(Deffenct_Phys);
				writer.Write(Reserve_last2);
			}
		}
	}
}
