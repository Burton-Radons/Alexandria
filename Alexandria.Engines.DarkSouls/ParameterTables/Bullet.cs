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
		/// From BulletParam.paramdef (id 1Eh).
		/// </remarks>
		public class Bullet : ParameterTableRow {
			public const string TableName = "BULLET_PARAM_ST";

			Int32 atkId_Bullet, sfxId_Bullet, sfxId_Hit, sfxId_Flick, spEffectIDForShooter, autoSearchNPCThinkID, hitBulletID, spEffectId0, spEffectId1, spEffectId2, spEffectId3, spEffectId4;
			Single life, dist, shootInterval, gravityInRange, gravityOutRange, hormingStopRange, initVellocity, accelInRange, accelOutRange, maxVellocity, minVellocity, accelTime, homingBeginDist, hitRadius, hitRadiusMax, spreadTime, expDelay, hormingOffsetRange, dmgHitRecordLifeTime, externalForce;
			UInt16 numShoot;
			Int16 homingAngle, shootAngle, shootAngleInterval, shootAngleXInterval;
			SByte damageDamp, spelDamageDamp, fireDamageDamp, thunderDamageDamp, staminaDamp, knockbackDamp, shootAngleXZ;
			Byte lockShootLimitAng, isPenetrate, prevVelocityDirRate;
			AttackAttackAttributes atkAttribute;
			AttackParameterSpecialAttributes spAttribute;
			AttackType material_AttackType;
			WeaponMaterialAttack material_AttackMaterial;
			AttackSize material_Size;
			BulletLaunchConditionType launchConditionType;
			Byte[] pad;

			public static readonly PropertyInfo
				AtkId_BulletProperty = GetProperty<Bullet>("AtkId_Bullet"),
				SfxId_BulletProperty = GetProperty<Bullet>("SfxId_Bullet"),
				SfxId_HitProperty = GetProperty<Bullet>("SfxId_Hit"),
				SfxId_FlickProperty = GetProperty<Bullet>("SfxId_Flick"),
				LifeProperty = GetProperty<Bullet>("Life"),
				DistProperty = GetProperty<Bullet>("Dist"),
				ShootIntervalProperty = GetProperty<Bullet>("ShootInterval"),
				GravityInRangeProperty = GetProperty<Bullet>("GravityInRange"),
				GravityOutRangeProperty = GetProperty<Bullet>("GravityOutRange"),
				HormingStopRangeProperty = GetProperty<Bullet>("HormingStopRange"),
				InitVellocityProperty = GetProperty<Bullet>("InitVellocity"),
				AccelInRangeProperty = GetProperty<Bullet>("AccelInRange"),
				AccelOutRangeProperty = GetProperty<Bullet>("AccelOutRange"),
				MaxVellocityProperty = GetProperty<Bullet>("MaxVellocity"),
				MinVellocityProperty = GetProperty<Bullet>("MinVellocity"),
				AccelTimeProperty = GetProperty<Bullet>("AccelTime"),
				HomingBeginDistProperty = GetProperty<Bullet>("HomingBeginDist"),
				HitRadiusProperty = GetProperty<Bullet>("HitRadius"),
				HitRadiusMaxProperty = GetProperty<Bullet>("HitRadiusMax"),
				SpreadTimeProperty = GetProperty<Bullet>("SpreadTime"),
				ExpDelayProperty = GetProperty<Bullet>("ExpDelay"),
				HormingOffsetRangeProperty = GetProperty<Bullet>("HormingOffsetRange"),
				DmgHitRecordLifeTimeProperty = GetProperty<Bullet>("DmgHitRecordLifeTime"),
				ExternalForceProperty = GetProperty<Bullet>("ExternalForce"),
				SpEffectIDForShooterProperty = GetProperty<Bullet>("SpEffectIDForShooter"),
				AutoSearchNPCThinkIDProperty = GetProperty<Bullet>("AutoSearchNPCThinkID"),
				HitBulletIDProperty = GetProperty<Bullet>("HitBulletID"),
				SpEffectId0Property = GetProperty<Bullet>("SpEffectId0"),
				SpEffectId1Property = GetProperty<Bullet>("SpEffectId1"),
				SpEffectId2Property = GetProperty<Bullet>("SpEffectId2"),
				SpEffectId3Property = GetProperty<Bullet>("SpEffectId3"),
				SpEffectId4Property = GetProperty<Bullet>("SpEffectId4"),
				NumShootProperty = GetProperty<Bullet>("NumShoot"),
				HomingAngleProperty = GetProperty<Bullet>("HomingAngle"),
				ShootAngleProperty = GetProperty<Bullet>("ShootAngle"),
				ShootAngleIntervalProperty = GetProperty<Bullet>("ShootAngleInterval"),
				ShootAngleXIntervalProperty = GetProperty<Bullet>("ShootAngleXInterval"),
				DamageDampProperty = GetProperty<Bullet>("DamageDamp"),
				SpelDamageDampProperty = GetProperty<Bullet>("SpelDamageDamp"),
				FireDamageDampProperty = GetProperty<Bullet>("FireDamageDamp"),
				ThunderDamageDampProperty = GetProperty<Bullet>("ThunderDamageDamp"),
				StaminaDampProperty = GetProperty<Bullet>("StaminaDamp"),
				KnockbackDampProperty = GetProperty<Bullet>("KnockbackDamp"),
				ShootAngleXZProperty = GetProperty<Bullet>("ShootAngleXZ"),
				LockShootLimitAngProperty = GetProperty<Bullet>("LockShootLimitAng"),
				IsPenetrateProperty = GetProperty<Bullet>("IsPenetrate"),
				PrevVelocityDirRateProperty = GetProperty<Bullet>("PrevVelocityDirRate"),
				AtkAttributeProperty = GetProperty<Bullet>("AtkAttribute"),
				SpAttributeProperty = GetProperty<Bullet>("SpAttribute"),
				Material_AttackTypeProperty = GetProperty<Bullet>("Material_AttackType"),
				Material_AttackMaterialProperty = GetProperty<Bullet>("Material_AttackMaterial"),
				Material_SizeProperty = GetProperty<Bullet>("Material_Size"),
				LaunchConditionTypeProperty = GetProperty<Bullet>("LaunchConditionType"),
				FollowTypeProperty = GetProperty<Bullet>("FollowType"),
				EmittePosTypeProperty = GetProperty<Bullet>("EmittePosType"),
				IsAttackSFXProperty = GetProperty<Bullet>("IsAttackSFX"),
				IsEndlessHitProperty = GetProperty<Bullet>("IsEndlessHit"),
				IsPenetrateMapProperty = GetProperty<Bullet>("IsPenetrateMap"),
				IsHitBothTeamProperty = GetProperty<Bullet>("IsHitBothTeam"),
				IsUseSharedHitListProperty = GetProperty<Bullet>("IsUseSharedHitList"),
				IsUseMultiDmyPolyIfPlaceProperty = GetProperty<Bullet>("IsUseMultiDmyPolyIfPlace"),
				AttachEffectTypeProperty = GetProperty<Bullet>("AttachEffectType"),
				IsHitForceMagicProperty = GetProperty<Bullet>("IsHitForceMagic"),
				IsIgnoreSfxIfHitWaterProperty = GetProperty<Bullet>("IsIgnoreSfxIfHitWater"),
				IsIgnoreMoveStateIfHitWaterProperty = GetProperty<Bullet>("IsIgnoreMoveStateIfHitWater"),
				IsHitDarkForceMagicProperty = GetProperty<Bullet>("IsHitDarkForceMagic"),
				PadProperty = GetProperty<Bullet>("Pad");

			/// <summary>Attack ID</summary>
			/// <remarks>
			/// Japanese short name: "攻撃ID", Google translated: "Attack ID".
			/// Japanese description: "攻撃パラメータのＩＤをそれぞれ登録する.→攻撃タイプ／攻撃材質／物理攻撃力／魔法攻撃力／スタミナ攻撃力／ノックバック距離.", Google translated: ". → attack type / attack Material / Physical Attack / Magic Attack / Stamina Attack Power / knock back distance to register the ID of each attack parameters .".
			/// </remarks>
			[ParameterTableRowAttribute("atkId_Bullet", index: 0, minimum: -1, maximum: 1E+08, step: 1, order: 1000, unknown2: 1)]
			[DisplayName("Attack ID")]
			[Description(". → attack type / attack Material / Physical Attack / Magic Attack / Stamina Attack Power / knock back distance to register the ID of each attack parameters .")]
			[DefaultValue((Int32)(-1))]
			public Int32 AtkId_Bullet {
				get { return atkId_Bullet; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for AtkId_Bullet.");
					SetProperty(ref atkId_Bullet, ref value, AtkId_BulletProperty);
				}
			}

			/// <summary>SFXID [ bullet ]</summary>
			/// <remarks>
			/// Japanese short name: "SFXID【弾】", Google translated: "SFXID [ bullet ]".
			/// Japanese description: "SFX IDを入れる。", Google translated: "I put SFX ID.".
			/// </remarks>
			[ParameterTableRowAttribute("sfxId_Bullet", index: 1, minimum: -1, maximum: 1E+08, step: 1, order: 2000, unknown2: 1)]
			[DisplayName("SFXID [ bullet ]")]
			[Description("I put SFX ID.")]
			[DefaultValue((Int32)(-1))]
			public Int32 SfxId_Bullet {
				get { return sfxId_Bullet; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for SfxId_Bullet.");
					SetProperty(ref sfxId_Bullet, ref value, SfxId_BulletProperty);
				}
			}

			/// <summary>SFXID [ landing ]</summary>
			/// <remarks>
			/// Japanese short name: "SFXID【着弾】", Google translated: "SFXID [ landing ]".
			/// Japanese description: "着弾SFXID。-1は発生しない。", Google translated: "Landing SFXID. -1 Does not occur .".
			/// </remarks>
			[ParameterTableRowAttribute("sfxId_Hit", index: 2, minimum: -1, maximum: 1E+08, step: 1, order: 3000, unknown2: 1)]
			[DisplayName("SFXID [ landing ]")]
			[Description("Landing SFXID. -1 Does not occur .")]
			[DefaultValue((Int32)(-1))]
			public Int32 SfxId_Hit {
				get { return sfxId_Hit; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for SfxId_Hit.");
					SetProperty(ref sfxId_Hit, ref value, SfxId_HitProperty);
				}
			}

			/// <summary>SFXID [ when ] repelling</summary>
			/// <remarks>
			/// Japanese short name: "SFXID【はじき時】", Google translated: "SFXID [ when ] repelling".
			/// Japanese description: "はじき時SFXID。-1は発生しない。", Google translated: "SFXID repelling time . -1 Does not occur .".
			/// </remarks>
			[ParameterTableRowAttribute("sfxId_Flick", index: 3, minimum: -1, maximum: 1E+08, step: 1, order: 4000, unknown2: 1)]
			[DisplayName("SFXID [ when ] repelling")]
			[Description("SFXID repelling time . -1 Does not occur .")]
			[DefaultValue((Int32)(-1))]
			public Int32 SfxId_Flick {
				get { return sfxId_Flick; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for SfxId_Flick.");
					SetProperty(ref sfxId_Flick, ref value, SfxId_FlickProperty);
				}
			}

			/// <summary>Life [s]</summary>
			/// <remarks>
			/// Japanese short name: "寿命[s]", Google translated: "Life [s]".
			/// Japanese description: "飛び道具が存在し続けられる時間（-1なら無限）.", Google translated: "The ( infinite if -1) time that missile is continue to exist .".
			/// </remarks>
			[ParameterTableRowAttribute("life", index: 4, minimum: -1, maximum: 99999, step: 0.01, order: 29000, unknown2: 1)]
			[DisplayName("Life [s]")]
			[Description("The ( infinite if -1) time that missile is continue to exist .")]
			[DefaultValue((Single)(-1))]
			public Single Life {
				get { return life; }
				set {
					if ((double)value < -1 || (double)value > 99999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 99999 for Life.");
					SetProperty(ref life, ref value, LifeProperty);
				}
			}

			/// <summary>Range distance [m]</summary>
			/// <remarks>
			/// Japanese short name: "射程距離[m]", Google translated: "Range distance [m]".
			/// Japanese description: "減衰が開始される距離（実際の飛距離ではない）.", Google translated: "( It is not the distance actual ) distance attenuation is started .".
			/// </remarks>
			[ParameterTableRowAttribute("dist", index: 5, minimum: 0, maximum: 99999, step: 0.1, order: 33000, unknown2: 1)]
			[DisplayName("Range distance [m]")]
			[Description("( It is not the distance actual ) distance attenuation is started .")]
			[DefaultValue((Single)0)]
			public Single Dist {
				get { return dist; }
				set {
					if ((double)value < 0 || (double)value > 99999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99999 for Dist.");
					SetProperty(ref dist, ref value, DistProperty);
				}
			}

			/// <summary>Firing interval [s]</summary>
			/// <remarks>
			/// Japanese short name: "発射間隔[s]", Google translated: "Firing interval [s]".
			/// Japanese description: "飛び道具を何秒間隔で発射するかを指定.", Google translated: "Specify how many seconds interval whether to fire the missile .".
			/// </remarks>
			[ParameterTableRowAttribute("shootInterval", index: 6, minimum: 0, maximum: 99, step: 0.01, order: 28000, unknown2: 1)]
			[DisplayName("Firing interval [s]")]
			[Description("Specify how many seconds interval whether to fire the missile .")]
			[DefaultValue((Single)0)]
			public Single ShootInterval {
				get { return shootInterval; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for ShootInterval.");
					SetProperty(ref shootInterval, ref value, ShootIntervalProperty);
				}
			}

			/// <summary>Firing range in gravity [m / s ^ 2]</summary>
			/// <remarks>
			/// Japanese short name: "射程距離内重力[m/s^2]", Google translated: "Firing range in gravity [m / s ^ 2]".
			/// Japanese description: "射程距離内での下向きにかかる重力.", Google translated: "Gravity exerted downward in the range of distance within .".
			/// </remarks>
			[ParameterTableRowAttribute("gravityInRange", index: 7, minimum: -999, maximum: 999, step: 0.1, order: 34000, unknown2: 1)]
			[DisplayName("Firing range in gravity [m / s ^ 2]")]
			[Description("Gravity exerted downward in the range of distance within .")]
			[DefaultValue((Single)0)]
			public Single GravityInRange {
				get { return gravityInRange; }
				set {
					if ((double)value < -999 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -999 to 999 for GravityInRange.");
					SetProperty(ref gravityInRange, ref value, GravityInRangeProperty);
				}
			}

			/// <summary>Range out of range gravity [m / s ^ 2]</summary>
			/// <remarks>
			/// Japanese short name: "射程距離外重力[m/s^2]", Google translated: "Range out of range gravity [m / s ^ 2]".
			/// Japanese description: "減衰がはじまったときの下向きにかかる重力（ポトンと落ちる感じを表現.", Google translated: "Express the feeling that the fall ( plop gravity exerted downward when the decay began .".
			/// </remarks>
			[ParameterTableRowAttribute("gravityOutRange", index: 8, minimum: -999, maximum: 999, step: 0.1, order: 35000, unknown2: 1)]
			[DisplayName("Range out of range gravity [m / s ^ 2]")]
			[Description("Express the feeling that the fall ( plop gravity exerted downward when the decay began .")]
			[DefaultValue((Single)0)]
			public Single GravityOutRange {
				get { return gravityOutRange; }
				set {
					if ((double)value < -999 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -999 to 999 for GravityOutRange.");
					SetProperty(ref gravityOutRange, ref value, GravityOutRangeProperty);
				}
			}

			/// <summary>Induction stopping distance [m]</summary>
			/// <remarks>
			/// Japanese short name: "誘導停止距離[m]", Google translated: "Induction stopping distance [m]".
			/// Japanese description: "誘導を停止するターゲットとの距離。誘導弾で当たり過ぎないようにするパラメータ。", Google translated: "Distance from the target to stop the induction . The parameters that you do not too much per missile .".
			/// </remarks>
			[ParameterTableRowAttribute("hormingStopRange", index: 9, minimum: 0, maximum: 999999, step: 0.1, order: 45000, unknown2: 1)]
			[DisplayName("Induction stopping distance [m]")]
			[Description("Distance from the target to stop the induction . The parameters that you do not too much per missile .")]
			[DefaultValue((Single)0)]
			public Single HormingStopRange {
				get { return hormingStopRange; }
				set {
					if ((double)value < 0 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999999 for HormingStopRange.");
					SetProperty(ref hormingStopRange, ref value, HormingStopRangeProperty);
				}
			}

			/// <summary>Initial velocity [m / s]</summary>
			/// <remarks>
			/// Japanese short name: "初速[m/s]", Google translated: "Initial velocity [m / s]".
			/// Japanese description: "ＳＦＸの初速度.", Google translated: "Initial rate of SFX.".
			/// </remarks>
			[ParameterTableRowAttribute("initVellocity", index: 10, minimum: 0, maximum: 999, step: 0.1, order: 36000, unknown2: 1)]
			[DisplayName("Initial velocity [m / s]")]
			[Description("Initial rate of SFX.")]
			[DefaultValue((Single)0)]
			public Single InitVellocity {
				get { return initVellocity; }
				set {
					if ((double)value < 0 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for InitVellocity.");
					SetProperty(ref initVellocity, ref value, InitVellocityProperty);
				}
			}

			/// <summary>Firing range in acceleration [m / s ^ 2]</summary>
			/// <remarks>
			/// Japanese short name: "射程距離内加速度[m/s^2]", Google translated: "Firing range in acceleration [m / s ^ 2]".
			/// Japanese description: "ＳＦＸの射程内の加速度.", Google translated: "Acceleration within range of SFX.".
			/// </remarks>
			[ParameterTableRowAttribute("accelInRange", index: 11, minimum: -999, maximum: 999, step: 0.1, order: 40000, unknown2: 1)]
			[DisplayName("Firing range in acceleration [m / s ^ 2]")]
			[Description("Acceleration within range of SFX.")]
			[DefaultValue((Single)0)]
			public Single AccelInRange {
				get { return accelInRange; }
				set {
					if ((double)value < -999 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -999 to 999 for AccelInRange.");
					SetProperty(ref accelInRange, ref value, AccelInRangeProperty);
				}
			}

			/// <summary>Range out of range acceleration [m / s ^ 2]</summary>
			/// <remarks>
			/// Japanese short name: "射程距離外加速度[m/s^2]", Google translated: "Range out of range acceleration [m / s ^ 2]".
			/// Japanese description: "ＳＦＸが射程距離外に出たときの加速度.", Google translated: "Acceleration when the SFX comes into firing range outside .".
			/// </remarks>
			[ParameterTableRowAttribute("accelOutRange", index: 12, minimum: -999, maximum: 999, step: 0.1, order: 41000, unknown2: 1)]
			[DisplayName("Range out of range acceleration [m / s ^ 2]")]
			[Description("Acceleration when the SFX comes into firing range outside .")]
			[DefaultValue((Single)0)]
			public Single AccelOutRange {
				get { return accelOutRange; }
				set {
					if ((double)value < -999 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -999 to 999 for AccelOutRange.");
					SetProperty(ref accelOutRange, ref value, AccelOutRangeProperty);
				}
			}

			/// <summary>Maximum speed [m / s]</summary>
			/// <remarks>
			/// Japanese short name: "最高速度[m/s]", Google translated: "Maximum speed [m / s]".
			/// Japanese description: "最高速度.", Google translated: "Maximum speed .".
			/// </remarks>
			[ParameterTableRowAttribute("maxVellocity", index: 13, minimum: 0, maximum: 999, step: 0.1, order: 38000, unknown2: 1)]
			[DisplayName("Maximum speed [m / s]")]
			[Description("Maximum speed .")]
			[DefaultValue((Single)0)]
			public Single MaxVellocity {
				get { return maxVellocity; }
				set {
					if ((double)value < 0 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for MaxVellocity.");
					SetProperty(ref maxVellocity, ref value, MaxVellocityProperty);
				}
			}

			/// <summary>Minimum speed [m / s]</summary>
			/// <remarks>
			/// Japanese short name: "最低速度[m/s]", Google translated: "Minimum speed [m / s]".
			/// Japanese description: "最低保証速度.", Google translated: "Guaranteed rate .".
			/// </remarks>
			[ParameterTableRowAttribute("minVellocity", index: 14, minimum: 0, maximum: 999, step: 0.1, order: 37000, unknown2: 1)]
			[DisplayName("Minimum speed [m / s]")]
			[Description("Guaranteed rate .")]
			[DefaultValue((Single)0)]
			public Single MinVellocity {
				get { return minVellocity; }
				set {
					if ((double)value < 0 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for MinVellocity.");
					SetProperty(ref minVellocity, ref value, MinVellocityProperty);
				}
			}

			/// <summary>Acceleration start time [s]</summary>
			/// <remarks>
			/// Japanese short name: "加速開始時間[s]", Google translated: "Acceleration start time [s]".
			/// Japanese description: "この時間までは、加速しない（ロケット弾みたいな魔法を撃つことができるようにしておく）.", Google translated: "Up to this time , ( it should be to be able to shoot a magic rocket like ) you do not want to accelerate .".
			/// </remarks>
			[ParameterTableRowAttribute("accelTime", index: 15, minimum: 0, maximum: 99, step: 0.01, order: 39000, unknown2: 1)]
			[DisplayName("Acceleration start time [s]")]
			[Description("Up to this time , ( it should be to be able to shoot a magic rocket like ) you do not want to accelerate .")]
			[DefaultValue((Single)0)]
			public Single AccelTime {
				get { return accelTime; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for AccelTime.");
					SetProperty(ref accelTime, ref value, AccelTimeProperty);
				}
			}

			/// <summary>Induction start distance [m]</summary>
			/// <remarks>
			/// Japanese short name: "誘導開始距離[m]", Google translated: "Induction start distance [m]".
			/// Japanese description: "何ｍ進んだ地点から誘導を開始するか.", Google translated: "You can start the induction from the point where what m advanced .".
			/// </remarks>
			[ParameterTableRowAttribute("homingBeginDist", index: 16, minimum: 0, maximum: 999, step: 0.1, order: 44000, unknown2: 1)]
			[DisplayName("Induction start distance [m]")]
			[Description("You can start the induction from the point where what m advanced .")]
			[DefaultValue((Single)0)]
			public Single HomingBeginDist {
				get { return homingBeginDist; }
				set {
					if ((double)value < 0 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for HomingBeginDist.");
					SetProperty(ref homingBeginDist, ref value, HomingBeginDistProperty);
				}
			}

			/// <summary>Initial bullet radius [m]</summary>
			/// <remarks>
			/// Japanese short name: "初期弾半径[m]", Google translated: "Initial bullet radius [m]".
			/// Japanese description: "当たり球の半径を設定する.", Google translated: "I set the radius of the sphere hit .".
			/// </remarks>
			[ParameterTableRowAttribute("hitRadius", index: 17, minimum: -1, maximum: 999, step: 0.01, order: 30000, unknown2: 1)]
			[DisplayName("Initial bullet radius [m]")]
			[Description("I set the radius of the sphere hit .")]
			[DefaultValue((Single)(-1))]
			public Single HitRadius {
				get { return hitRadius; }
				set {
					if ((double)value < -1 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999 for HitRadius.");
					SetProperty(ref hitRadius, ref value, HitRadiusProperty);
				}
			}

			/// <summary>Maximum bullet radius [m]</summary>
			/// <remarks>
			/// Japanese short name: "最大弾半径[m]", Google translated: "Maximum bullet radius [m]".
			/// Japanese description: "あたり球の最大半径（－1の場合、初期半径と同じにする／デフォルト）", Google translated: "Maximum radius of the sphere around (in the case of -1 , the same as the initial radius / default )".
			/// </remarks>
			[ParameterTableRowAttribute("hitRadiusMax", index: 18, minimum: -1, maximum: 999, step: 0.01, order: 31000, unknown2: 1)]
			[DisplayName("Maximum bullet radius [m]")]
			[Description("Maximum radius of the sphere around (in the case of -1 , the same as the initial radius / default )")]
			[DefaultValue((Single)(-1))]
			public Single HitRadiusMax {
				get { return hitRadiusMax; }
				set {
					if ((double)value < -1 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999 for HitRadiusMax.");
					SetProperty(ref hitRadiusMax, ref value, HitRadiusMaxProperty);
				}
			}

			/// <summary>Range diffusion time [s]</summary>
			/// <remarks>
			/// Japanese short name: "範囲拡散時間[s]", Google translated: "Range diffusion time [s]".
			/// Japanese description: "範囲半径が細大にまで広がる時間.", Google translated: "Time range radius spread to Saidai .".
			/// </remarks>
			[ParameterTableRowAttribute("spreadTime", index: 19, minimum: 0, maximum: 99, step: 0.01, order: 32000, unknown2: 1)]
			[DisplayName("Range diffusion time [s]")]
			[Description("Time range radius spread to Saidai .")]
			[DefaultValue((Single)0)]
			public Single SpreadTime {
				get { return spreadTime; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for SpreadTime.");
					SetProperty(ref spreadTime, ref value, SpreadTimeProperty);
				}
			}

			/// <summary>Trigger delay [s]</summary>
			/// <remarks>
			/// Japanese short name: "発動遅延[s]", Google translated: "Trigger delay [s]".
			/// Japanese description: "着弾後、爆発までの時間（０の場合はすぐに爆発）.", Google translated: "After landing , ( the explosion immediately in the case of 0) time to explosion .".
			/// </remarks>
			[ParameterTableRowAttribute("expDelay", index: 20, minimum: 0, maximum: 99, step: 0.01, order: 59000, unknown2: 1)]
			[DisplayName("Trigger delay [s]")]
			[Description("After landing , ( the explosion immediately in the case of 0) time to explosion .")]
			[DefaultValue((Single)0)]
			public Single ExpDelay {
				get { return expDelay; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for ExpDelay.");
					SetProperty(ref expDelay, ref value, ExpDelayProperty);
				}
			}

			/// <summary>The amount of shift -induced [m]</summary>
			/// <remarks>
			/// Japanese short name: "誘導ずらし量[m]", Google translated: "The amount of shift -induced [m]".
			/// Japanese description: "０だと正確。射撃時にXYZ各成分を、この量だけずらして狙うようにする。", Google translated: "Accurate that it is 0 . And to make aim to be shifted by this amount , the XYZ components to shooting time.".
			/// </remarks>
			[ParameterTableRowAttribute("hormingOffsetRange", index: 21, minimum: 0, maximum: 999, step: 0.1, order: 46000, unknown2: 1)]
			[DisplayName("The amount of shift -induced [m]")]
			[Description("Accurate that it is 0 . And to make aim to be shifted by this amount , the XYZ components to shooting time.")]
			[DefaultValue((Single)0)]
			public Single HormingOffsetRange {
				get { return hormingOffsetRange; }
				set {
					if ((double)value < 0 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for HormingOffsetRange.");
					SetProperty(ref hormingOffsetRange, ref value, HormingOffsetRangeProperty);
				}
			}

			/// <summary>Survival time of the damage hit history [s]</summary>
			/// <remarks>
			/// Japanese short name: "ダメージヒット履歴の生存時間[s]", Google translated: "Survival time of the damage hit history [s]".
			/// Japanese description: "ダメージヒット履歴の生存時間[sec](<=0.0f：無期限)", Google translated: "Survival time of the damage hit history [sec] (<= 0.0f: indefinitely )".
			/// </remarks>
			[ParameterTableRowAttribute("dmgHitRecordLifeTime", index: 22, minimum: 0, maximum: 9999, step: 0.1, order: 17000, unknown2: 1)]
			[DisplayName("Survival time of the damage hit history [s]")]
			[Description("Survival time of the damage hit history [sec] (<= 0.0f: indefinitely )")]
			[DefaultValue((Single)0)]
			public Single DmgHitRecordLifeTime {
				get { return dmgHitRecordLifeTime; }
				set {
					if ((double)value < 0 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9999 for DmgHitRecordLifeTime.");
					SetProperty(ref dmgHitRecordLifeTime, ref value, DmgHitRecordLifeTimeProperty);
				}
			}

			/// <summary>External force [m / s ^ 2]</summary>
			/// <remarks>
			/// Japanese short name: "外力[m/s^2]", Google translated: "External force [m / s ^ 2]".
			/// Japanese description: "射撃時の方向にかかる外力.(Y軸は抜いている)", Google translated: "( I have to unplug the Y -axis) external force . Applied in the direction of the shooting at the time".
			/// </remarks>
			[ParameterTableRowAttribute("externalForce", index: 23, minimum: 0, maximum: 10, step: 0.001, order: 35500, unknown2: 1)]
			[DisplayName("External force [m / s ^ 2]")]
			[Description("( I have to unplug the Y -axis) external force . Applied in the direction of the shooting at the time")]
			[DefaultValue((Single)0)]
			public Single ExternalForce {
				get { return externalForce; }
				set {
					if ((double)value < 0 || (double)value > 10)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 10 for ExternalForce.");
					SetProperty(ref externalForce, ref value, ExternalForceProperty);
				}
			}

			/// <summary>Special effects to be applied to people who were shooting</summary>
			/// <remarks>
			/// Japanese short name: "射撃した人にかける特殊効果", Google translated: "Special effects to be applied to people who were shooting".
			/// Japanese description: "射撃した人にかける特殊効果", Google translated: "Special effects to be applied to people who were shooting".
			/// </remarks>
			[ParameterTableRowAttribute("spEffectIDForShooter", index: 24, minimum: -1, maximum: 1E+08, step: 1, order: 47000, unknown2: 1)]
			[DisplayName("Special effects to be applied to people who were shooting")]
			[Description("Special effects to be applied to people who were shooting")]
			[DefaultValue((Int32)(-1))]
			public Int32 SpEffectIDForShooter {
				get { return spEffectIDForShooter; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for SpEffectIDForShooter.");
					SetProperty(ref spEffectIDForShooter, ref value, SpEffectIDForShooterProperty);
				}
			}

			/// <summary>Funnel thinking NPC ID</summary>
			/// <remarks>
			/// Japanese short name: "ファンネルNPC思考ID", Google translated: "Funnel thinking NPC ID".
			/// Japanese description: "ファンネルがターゲットの検索使用するパラメータ", Google translated: "Parameters funnel searches use of target".
			/// </remarks>
			[ParameterTableRowAttribute("autoSearchNPCThinkID", index: 25, minimum: -1, maximum: 999999, step: 1, order: 60000, unknown2: 1)]
			[DisplayName("Funnel thinking NPC ID")]
			[Description("Parameters funnel searches use of target")]
			[DefaultValue((Int32)0)]
			public Int32 AutoSearchNPCThinkID {
				get { return autoSearchNPCThinkID; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for AutoSearchNPCThinkID.");
					SetProperty(ref autoSearchNPCThinkID, ref value, AutoSearchNPCThinkIDProperty);
				}
			}

			/// <summary>Generation bullet ID</summary>
			/// <remarks>
			/// Japanese short name: "発生弾丸ID", Google translated: "Generation bullet ID".
			/// Japanese description: "弾丸パラメータから、新しく弾丸パラメータを発生させるときにＩＤを指定する", Google translated: "From bullet parameter to specify the ID when generating the new bullet parameters".
			/// </remarks>
			[ParameterTableRowAttribute("HitBulletID", index: 26, minimum: -1, maximum: 1E+08, step: 1, order: 11000, unknown2: 1)]
			[DisplayName("Generation bullet ID")]
			[Description("From bullet parameter to specify the ID when generating the new bullet parameters")]
			[DefaultValue((Int32)(-1))]
			public Int32 HitBulletID {
				get { return hitBulletID; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for HitBulletID.");
					SetProperty(ref hitBulletID, ref value, HitBulletIDProperty);
				}
			}

			/// <summary>Special effects ID0</summary>
			/// <remarks>
			/// Japanese short name: "特殊効果ID0", Google translated: "Special effects ID0".
			/// Japanese description: "特殊効果パラメータのＩＤをそれぞれ登録する.→特殊効果全般.", Google translated: ". → special effects in general to register the ID of each special effect parameters .".
			/// </remarks>
			[ParameterTableRowAttribute("spEffectId0", index: 27, minimum: -1, maximum: 1E+08, step: 1, order: 48000, unknown2: 1)]
			[DisplayName("Special effects ID0")]
			[Description(". → special effects in general to register the ID of each special effect parameters .")]
			[DefaultValue((Int32)(-1))]
			public Int32 SpEffectId0 {
				get { return spEffectId0; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for SpEffectId0.");
					SetProperty(ref spEffectId0, ref value, SpEffectId0Property);
				}
			}

			/// <summary>Special effects ID1</summary>
			/// <remarks>
			/// Japanese short name: "特殊効果ID1", Google translated: "Special effects ID1".
			/// Japanese description: "特殊効果パラメータのＩＤをそれぞれ登録する.→特殊効果全般.", Google translated: ". → special effects in general to register the ID of each special effect parameters .".
			/// </remarks>
			[ParameterTableRowAttribute("spEffectId1", index: 28, minimum: -1, maximum: 1E+08, step: 1, order: 49000, unknown2: 1)]
			[DisplayName("Special effects ID1")]
			[Description(". → special effects in general to register the ID of each special effect parameters .")]
			[DefaultValue((Int32)(-1))]
			public Int32 SpEffectId1 {
				get { return spEffectId1; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for SpEffectId1.");
					SetProperty(ref spEffectId1, ref value, SpEffectId1Property);
				}
			}

			/// <summary>Special effects ID2</summary>
			/// <remarks>
			/// Japanese short name: "特殊効果ID2", Google translated: "Special effects ID2".
			/// Japanese description: "特殊効果パラメータのＩＤをそれぞれ登録する.→特殊効果全般.", Google translated: ". → special effects in general to register the ID of each special effect parameters .".
			/// </remarks>
			[ParameterTableRowAttribute("spEffectId2", index: 29, minimum: -1, maximum: 1E+08, step: 1, order: 50000, unknown2: 1)]
			[DisplayName("Special effects ID2")]
			[Description(". → special effects in general to register the ID of each special effect parameters .")]
			[DefaultValue((Int32)(-1))]
			public Int32 SpEffectId2 {
				get { return spEffectId2; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for SpEffectId2.");
					SetProperty(ref spEffectId2, ref value, SpEffectId2Property);
				}
			}

			/// <summary>Special effects ID3</summary>
			/// <remarks>
			/// Japanese short name: "特殊効果ID3", Google translated: "Special effects ID3".
			/// Japanese description: "特殊効果パラメータのＩＤをそれぞれ登録する.→特殊効果全般.", Google translated: ". → special effects in general to register the ID of each special effect parameters .".
			/// </remarks>
			[ParameterTableRowAttribute("spEffectId3", index: 30, minimum: -1, maximum: 1E+08, step: 1, order: 51000, unknown2: 1)]
			[DisplayName("Special effects ID3")]
			[Description(". → special effects in general to register the ID of each special effect parameters .")]
			[DefaultValue((Int32)(-1))]
			public Int32 SpEffectId3 {
				get { return spEffectId3; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for SpEffectId3.");
					SetProperty(ref spEffectId3, ref value, SpEffectId3Property);
				}
			}

			/// <summary>Special effects ID4</summary>
			/// <remarks>
			/// Japanese short name: "特殊効果ID4", Google translated: "Special effects ID4".
			/// Japanese description: "特殊効果パラメータのＩＤをそれぞれ登録する.→特殊効果全般.", Google translated: ". → special effects in general to register the ID of each special effect parameters .".
			/// </remarks>
			[ParameterTableRowAttribute("spEffectId4", index: 31, minimum: -1, maximum: 1E+08, step: 1, order: 52000, unknown2: 1)]
			[DisplayName("Special effects ID4")]
			[Description(". → special effects in general to register the ID of each special effect parameters .")]
			[DefaultValue((Int32)(-1))]
			public Int32 SpEffectId4 {
				get { return spEffectId4; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for SpEffectId4.");
					SetProperty(ref spEffectId4, ref value, SpEffectId4Property);
				}
			}

			/// <summary>Number of fire</summary>
			/// <remarks>
			/// Japanese short name: "発射数", Google translated: "Number of fire".
			/// Japanese description: "一度に発射する飛び道具の数.", Google translated: "Number of missile to fire at a time .".
			/// </remarks>
			[ParameterTableRowAttribute("numShoot", index: 32, minimum: 0, maximum: 99, step: 1, order: 23000, unknown2: 1)]
			[DisplayName("Number of fire")]
			[Description("Number of missile to fire at a time .")]
			[DefaultValue((UInt16)0)]
			public UInt16 NumShoot {
				get { return numShoot; }
				set {
					if ((double)value < 0 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99 for NumShoot.");
					SetProperty(ref numShoot, ref value, NumShootProperty);
				}
			}

			/// <summary>Induction performance [deg / s]</summary>
			/// <remarks>
			/// Japanese short name: "誘導性能[deg/s]", Google translated: "Induction performance [deg / s]".
			/// Japanese description: "1秒間に何度まで補正するか？.", Google translated: "How many times per second you can either correct ? .".
			/// </remarks>
			[ParameterTableRowAttribute("homingAngle", index: 33, minimum: -360, maximum: 360, step: 1, order: 43000, unknown2: 1)]
			[DisplayName("Induction performance [deg / s]")]
			[Description("How many times per second you can either correct ? .")]
			[DefaultValue((Int16)0)]
			public Int16 HomingAngle {
				get { return homingAngle; }
				set {
					if ((double)value < -360 || (double)value > 360)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -360 to 360 for HomingAngle.");
					SetProperty(ref homingAngle, ref value, HomingAngleProperty);
				}
			}

			/// <summary>Firing angle [deg]</summary>
			/// <remarks>
			/// Japanese short name: "発射角度[deg]", Google translated: "Firing angle [deg]".
			/// Japanese description: "飛び道具を前方何度に向かって発射するかを指定.", Google translated: "Specifies whether to launch in times forward toward the missile .".
			/// </remarks>
			[ParameterTableRowAttribute("shootAngle", index: 34, minimum: -360, maximum: 360, step: 1, order: 24000, unknown2: 1)]
			[DisplayName("Firing angle [deg]")]
			[Description("Specifies whether to launch in times forward toward the missile .")]
			[DefaultValue((Int16)0)]
			public Int16 ShootAngle {
				get { return shootAngle; }
				set {
					if ((double)value < -360 || (double)value > 360)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -360 to 360 for ShootAngle.");
					SetProperty(ref shootAngle, ref value, ShootAngleProperty);
				}
			}

			/// <summary>Firing angle interval [deg]</summary>
			/// <remarks>
			/// Japanese short name: "発射角度間隔[deg]", Google translated: "Firing angle interval [deg]".
			/// Japanese description: "飛び道具を複数発射する場合、何度間隔で発射するかを指定.(Y軸)", Google translated: "If you want to multi-fire the missile , designated . (Y -axis) many times whether to fire at intervals".
			/// </remarks>
			[ParameterTableRowAttribute("shootAngleInterval", index: 35, minimum: -360, maximum: 360, step: 1, order: 25000, unknown2: 1)]
			[DisplayName("Firing angle interval [deg]")]
			[Description("If you want to multi-fire the missile , designated . (Y -axis) many times whether to fire at intervals")]
			[DefaultValue((Int16)0)]
			public Int16 ShootAngleInterval {
				get { return shootAngleInterval; }
				set {
					if ((double)value < -360 || (double)value > 360)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -360 to 360 for ShootAngleInterval.");
					SetProperty(ref shootAngleInterval, ref value, ShootAngleIntervalProperty);
				}
			}

			/// <summary>Launch elevation angle interval [deg]</summary>
			/// <remarks>
			/// Japanese short name: "発射仰角間隔[deg]", Google translated: "Launch elevation angle interval [deg]".
			/// Japanese description: "飛び道具を複数発射する場合、何度間隔で発射するかを指定.(X軸)", Google translated: "If you want to multi-fire the missile , designated . (X -axis) many times whether to fire at intervals".
			/// </remarks>
			[ParameterTableRowAttribute("shootAngleXInterval", index: 36, minimum: -360, maximum: 360, step: 1, order: 27000, unknown2: 1)]
			[DisplayName("Launch elevation angle interval [deg]")]
			[Description("If you want to multi-fire the missile , designated . (X -axis) many times whether to fire at intervals")]
			[DefaultValue((Int16)0)]
			public Int16 ShootAngleXInterval {
				get { return shootAngleXInterval; }
				set {
					if ((double)value < -360 || (double)value > 360)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -360 to 360 for ShootAngleXInterval.");
					SetProperty(ref shootAngleXInterval, ref value, ShootAngleXIntervalProperty);
				}
			}

			/// <summary>Physical Attack attenuation rate [% / s]</summary>
			/// <remarks>
			/// Japanese short name: "物理攻撃力減衰率[%/s]", Google translated: "Physical Attack attenuation rate [% / s]".
			/// Japanese description: "減衰距離以降、1秒間に減少する補正値.", Google translated: "Attenuation distance later , the correction value is reduced to 1 second .".
			/// </remarks>
			[ParameterTableRowAttribute("damageDamp", index: 37, minimum: 0, maximum: 100, step: 1, order: 53000, unknown2: 1)]
			[DisplayName("Physical Attack attenuation rate [% / s]")]
			[Description("Attenuation distance later , the correction value is reduced to 1 second .")]
			[DefaultValue((SByte)0)]
			public SByte DamageDamp {
				get { return damageDamp; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for DamageDamp.");
					SetProperty(ref damageDamp, ref value, DamageDampProperty);
				}
			}

			/// <summary>Magic Attack attenuation rate [% / s]</summary>
			/// <remarks>
			/// Japanese short name: "魔法攻撃力減衰率[%/s]", Google translated: "Magic Attack attenuation rate [% / s]".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("spelDamageDamp", index: 38, minimum: 0, maximum: 100, step: 1, order: 54000, unknown2: 1)]
			[DisplayName("Magic Attack attenuation rate [% / s]")]
			[Description("")]
			[DefaultValue((SByte)0)]
			public SByte SpelDamageDamp {
				get { return spelDamageDamp; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for SpelDamageDamp.");
					SetProperty(ref spelDamageDamp, ref value, SpelDamageDampProperty);
				}
			}

			/// <summary>Flame attack power attenuation rate [% / s]</summary>
			/// <remarks>
			/// Japanese short name: "炎攻撃力減衰率[%/s]", Google translated: "Flame attack power attenuation rate [% / s]".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("fireDamageDamp", index: 39, minimum: 0, maximum: 100, step: 1, order: 55000, unknown2: 1)]
			[DisplayName("Flame attack power attenuation rate [% / s]")]
			[Description("")]
			[DefaultValue((SByte)0)]
			public SByte FireDamageDamp {
				get { return fireDamageDamp; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for FireDamageDamp.");
					SetProperty(ref fireDamageDamp, ref value, FireDamageDampProperty);
				}
			}

			/// <summary>Blitz attack power attenuation rate [% / s]</summary>
			/// <remarks>
			/// Japanese short name: "電撃攻撃力減衰率[%/s]", Google translated: "Blitz attack power attenuation rate [% / s]".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("thunderDamageDamp", index: 40, minimum: 0, maximum: 100, step: 1, order: 56000, unknown2: 1)]
			[DisplayName("Blitz attack power attenuation rate [% / s]")]
			[Description("")]
			[DefaultValue((SByte)0)]
			public SByte ThunderDamageDamp {
				get { return thunderDamageDamp; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for ThunderDamageDamp.");
					SetProperty(ref thunderDamageDamp, ref value, ThunderDamageDampProperty);
				}
			}

			/// <summary>Stamina damage attenuation rate [% / s]</summary>
			/// <remarks>
			/// Japanese short name: "スタミナダメージ減衰率[%/s]", Google translated: "Stamina damage attenuation rate [% / s]".
			/// Japanese description: "減衰距離以降、1秒間に減少する補正値.", Google translated: "Attenuation distance later , the correction value is reduced to 1 second .".
			/// </remarks>
			[ParameterTableRowAttribute("staminaDamp", index: 41, minimum: 0, maximum: 100, step: 1, order: 57000, unknown2: 1)]
			[DisplayName("Stamina damage attenuation rate [% / s]")]
			[Description("Attenuation distance later , the correction value is reduced to 1 second .")]
			[DefaultValue((SByte)0)]
			public SByte StaminaDamp {
				get { return staminaDamp; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for StaminaDamp.");
					SetProperty(ref staminaDamp, ref value, StaminaDampProperty);
				}
			}

			/// <summary>Knock back attenuation rate [% / s]</summary>
			/// <remarks>
			/// Japanese short name: "ノックバック減衰率[%/s]", Google translated: "Knock back attenuation rate [% / s]".
			/// Japanese description: "減衰距離以降、1秒間に減少する補正値.", Google translated: "Attenuation distance later , the correction value is reduced to 1 second .".
			/// </remarks>
			[ParameterTableRowAttribute("knockbackDamp", index: 42, minimum: 0, maximum: 100, step: 1, order: 58000, unknown2: 1)]
			[DisplayName("Knock back attenuation rate [% / s]")]
			[Description("Attenuation distance later , the correction value is reduced to 1 second .")]
			[DefaultValue((SByte)0)]
			public SByte KnockbackDamp {
				get { return knockbackDamp; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for KnockbackDamp.");
					SetProperty(ref knockbackDamp, ref value, KnockbackDampProperty);
				}
			}

			/// <summary>Launch elevation angle [deg]</summary>
			/// <remarks>
			/// Japanese short name: "発射仰角[deg]", Google translated: "Launch elevation angle [deg]".
			/// Japanese description: "水平方向からの追加仰角。", Google translated: "Add elevation angle from the horizontal direction .".
			/// </remarks>
			[ParameterTableRowAttribute("shootAngleXZ", index: 43, minimum: -90, maximum: 90, step: 1, order: 26000, unknown2: 1)]
			[DisplayName("Launch elevation angle [deg]")]
			[Description("Add elevation angle from the horizontal direction .")]
			[DefaultValue((SByte)0)]
			public SByte ShootAngleXZ {
				get { return shootAngleXZ; }
				set {
					if ((double)value < -90 || (double)value > 90)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -90 to 90 for ShootAngleXZ.");
					SetProperty(ref shootAngleXZ, ref value, ShootAngleXZProperty);
				}
			}

			/// <summary>Lock direction angle limit</summary>
			/// <remarks>
			/// Japanese short name: "ロック方向制限角度", Google translated: "Lock direction angle limit".
			/// Japanese description: "ロック方向を向かせるときの制限角度", Google translated: "Limit angle when suited to the lock direction".
			/// </remarks>
			[ParameterTableRowAttribute("lockShootLimitAng", index: 44, minimum: 0, maximum: 180, step: 1, order: 42000, unknown2: 1)]
			[DisplayName("Lock direction angle limit")]
			[Description("Limit angle when suited to the lock direction")]
			[DefaultValue((Byte)0)]
			public Byte LockShootLimitAng {
				get { return lockShootLimitAng; }
				set {
					if ((double)value < 0 || (double)value > 180)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 180 for LockShootLimitAng.");
					SetProperty(ref lockShootLimitAng, ref value, LockShootLimitAngProperty);
				}
			}

			/// <summary>And through the character · OBJ?</summary>
			/// <remarks>
			/// Japanese short name: "キャラ・OBJを貫通？", Google translated: "And through the character · OBJ?".
			/// Japanese description: "ＰＣ、ＮＰＣ、ＯＢＪに当たったときに、寿命まで消滅しないかどうか？を設定", Google translated: "When it hits PC, NPC, the OBJ, and whether or not disappear the lifetime ? Set".
			/// </remarks>
			[ParameterTableRowAttribute("isPenetrate", index: 45, minimum: 0, maximum: 100, step: 1, order: 19000, unknown2: 1)]
			[DisplayName("And through the character · OBJ?")]
			[Description("When it hits PC, NPC, the OBJ, and whether or not disappear the lifetime ? Set")]
			[DefaultValue((Byte)0)]
			public Byte IsPenetrate {
				get { return isPenetrate; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for IsPenetrate.");
					SetProperty(ref isPenetrate, ref value, IsPenetrateProperty);
				}
			}

			/// <summary>Moving direction of the previous addition rate [% ]</summary>
			/// <remarks>
			/// Japanese short name: "前回の移動方向加算率[%]", Google translated: "Moving direction of the previous addition rate [% ]".
			/// Japanese description: "滑る弾が壁にヒット時に前回の移動方向を今の方向へ加算する比率", Google translated: "Ratio of bullets slip to add in the direction of the now moving direction of the last to hit the wall".
			/// </remarks>
			[ParameterTableRowAttribute("prevVelocityDirRate", index: 46, minimum: 0, maximum: 100, step: 1, order: 46500, unknown2: 1)]
			[DisplayName("Moving direction of the previous addition rate [% ]")]
			[Description("Ratio of bullets slip to add in the direction of the now moving direction of the last to hit the wall")]
			[DefaultValue((Byte)0)]
			public Byte PrevVelocityDirRate {
				get { return prevVelocityDirRate; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for PrevVelocityDirRate.");
					SetProperty(ref prevVelocityDirRate, ref value, PrevVelocityDirRateProperty);
				}
			}

			/// <summary>Physical attributes</summary>
			/// <remarks>
			/// Japanese short name: "物理属性", Google translated: "Physical attributes".
			/// Japanese description: "弾丸に設定する物理属性を設定", Google translated: "Set the physical attributes to set for the bullet".
			/// </remarks>
			[ParameterTableRowAttribute("atkAttribute", index: 47, minimum: 0, maximum: 255, step: 1, order: 6000, unknown2: 1)]
			[DisplayName("Physical attributes")]
			[Description("Set the physical attributes to set for the bullet")]
			[DefaultValue((AttackAttackAttributes)0)]
			public AttackAttackAttributes AtkAttribute {
				get { return atkAttribute; }
				set { SetProperty(ref atkAttribute, ref value, AtkAttributeProperty); }
			}

			/// <summary>Special attributes</summary>
			/// <remarks>
			/// Japanese short name: "特殊属性", Google translated: "Special attributes".
			/// Japanese description: "弾丸に設定する特殊属性を設定", Google translated: "Set the special attributes to be set in the bullet".
			/// </remarks>
			[ParameterTableRowAttribute("spAttribute", index: 48, minimum: 0, maximum: 255, step: 1, order: 7000, unknown2: 1)]
			[DisplayName("Special attributes")]
			[Description("Set the special attributes to be set in the bullet")]
			[DefaultValue((AttackParameterSpecialAttributes)0)]
			public AttackParameterSpecialAttributes SpAttribute {
				get { return spAttribute; }
				set { SetProperty(ref spAttribute, ref value, SpAttributeProperty); }
			}

			/// <summary>SFX Material type attack</summary>
			/// <remarks>
			/// Japanese short name: "SFX材質 攻撃タイプ", Google translated: "SFX Material type attack".
			/// Japanese description: "攻撃属性が何かを指定する", Google translated: "Attack attribute to specify something".
			/// </remarks>
			[ParameterTableRowAttribute("Material_AttackType", index: 49, minimum: 0, maximum: 255, step: 1, order: 8000, unknown2: 1)]
			[DisplayName("SFX Material type attack")]
			[Description("Attack attribute to specify something")]
			[DefaultValue((AttackType)0)]
			public AttackType Material_AttackType {
				get { return material_AttackType; }
				set { SetProperty(ref material_AttackType, ref value, Material_AttackTypeProperty); }
			}

			/// <summary>SFX attack Material Material</summary>
			/// <remarks>
			/// Japanese short name: "SFX材質 攻撃材質", Google translated: "SFX attack Material Material".
			/// Japanese description: "攻撃時のSFX/ＳＥに使用", Google translated: "Used for SFX / SE of attack during".
			/// </remarks>
			[ParameterTableRowAttribute("Material_AttackMaterial", index: 50, minimum: 0, maximum: 255, step: 1, order: 9000, unknown2: 1)]
			[DisplayName("SFX attack Material Material")]
			[Description("Used for SFX / SE of attack during")]
			[DefaultValue((WeaponMaterialAttack)0)]
			public WeaponMaterialAttack Material_AttackMaterial {
				get { return material_AttackMaterial; }
				set { SetProperty(ref material_AttackMaterial, ref value, Material_AttackMaterialProperty); }
			}

			/// <summary>SFX Material size</summary>
			/// <remarks>
			/// Japanese short name: "SFX材質 サイズ", Google translated: "SFX Material size".
			/// Japanese description: "攻撃時のSFX/ＳＥに使用（予備／デフォルト0）", Google translated: "To SFX / SE of attack during use ( preliminary / default 0)".
			/// </remarks>
			[ParameterTableRowAttribute("Material_Size", index: 51, minimum: 0, maximum: 255, step: 1, order: 10000, unknown2: 1)]
			[DisplayName("SFX Material size")]
			[Description("To SFX / SE of attack during use ( preliminary / default 0)")]
			[DefaultValue((AttackSize)0)]
			public AttackSize Material_Size {
				get { return material_Size; }
				set { SetProperty(ref material_Size, ref value, Material_SizeProperty); }
			}

			/// <summary>Conditions</summary>
			/// <remarks>
			/// Japanese short name: "発生条件", Google translated: "Conditions".
			/// Japanese description: "着弾・寿命消滅時に弾を発生させるか判定する条件を指定", Google translated: "Specify conditions to determine whether to generate a bullet to the landing - life extinction".
			/// </remarks>
			[ParameterTableRowAttribute("launchConditionType", index: 52, minimum: 0, maximum: 255, step: 1, order: 12000, unknown2: 1)]
			[DisplayName("Conditions")]
			[Description("Specify conditions to determine whether to generate a bullet to the landing - life extinction")]
			[DefaultValue((BulletLaunchConditionType)0)]
			public BulletLaunchConditionType LaunchConditionType {
				get { return launchConditionType; }
				set { SetProperty(ref launchConditionType, ref value, LaunchConditionTypeProperty); }
			}

			/// <summary>Follow-up type</summary>
			/// <remarks>
			/// Japanese short name: "追従タイプ", Google translated: "Follow-up type".
			/// Japanese description: "追従タイプ。「追従しない」がデフォルト。", Google translated: "Follow-up type . " Does not follow " the default .".
			/// </remarks>
			[ParameterTableRowAttribute("FollowType:3", index: 53, minimum: 0, maximum: 255, step: 1, order: 14000, unknown2: 1)]
			[DisplayName("Follow-up type")]
			[Description("Follow-up type . \" Does not follow \" the default .")]
			[DefaultValue((BulletFollowType)0)]
			public BulletFollowType FollowType {
				get { return (BulletFollowType)GetBitProperty(0, 3, FollowTypeProperty); }
				set { SetBitProperty(0, 3, (int)value, FollowTypeProperty); }
			}

			/// <summary>Source type</summary>
			/// <remarks>
			/// Japanese short name: "発生源タイプ", Google translated: "Source type".
			/// Japanese description: "発生源タイプ。ダミポリからが通常。（メテオを判定するために導入）", Google translated: "Source type . From Damipori usually . ( Introduced to determine Meteor )".
			/// </remarks>
			[ParameterTableRowAttribute("EmittePosType:3", index: 54, minimum: 0, maximum: 7, step: 1, order: 13000, unknown2: 1)]
			[DisplayName("Source type")]
			[Description("Source type . From Damipori usually . ( Introduced to determine Meteor )")]
			[DefaultValue((BulletEmitterPosition)0)]
			public BulletEmitterPosition EmittePosType {
				get { return (BulletEmitterPosition)GetBitProperty(3, 3, EmittePosTypeProperty); }
				set { SetBitProperty(3, 3, (int)value, EmittePosTypeProperty); }
			}

			/// <summary>Become remain stuck</summary>
			/// <remarks>
			/// Japanese short name: "刺さったままになるか", Google translated: "Become remain stuck".
			/// Japanese description: "矢などの弾丸が、キャラクターに刺さったままになるかどうかを設定する", Google translated: "Set of bullets and arrows , whether made ​​to remain stuck in character".
			/// </remarks>
			[ParameterTableRowAttribute("isAttackSFX:1", index: 55, minimum: 0, maximum: 1, step: 1, order: 5000, unknown2: 1)]
			[DisplayName("Become remain stuck")]
			[Description("Set of bullets and arrows , whether made ​​to remain stuck in character")]
			[DefaultValue(false)]
			public bool IsAttackSFX {
				get { return GetBoolProperty(6, 1, IsAttackSFXProperty); }
				set { SetBitProperty(6, 1, value, IsAttackSFXProperty); }
			}

			/// <summary>Or continue around ?</summary>
			/// <remarks>
			/// Japanese short name: "あたり続けるか？", Google translated: "Or continue around ?".
			/// Japanese description: "あたり続けるか？", Google translated: "Or continue around ?".
			/// </remarks>
			[ParameterTableRowAttribute("isEndlessHit:1", index: 56, minimum: 0, maximum: 1, step: 1, order: 15000, unknown2: 1)]
			[DisplayName("Or continue around ?")]
			[Description("Or continue around ?")]
			[DefaultValue(false)]
			public bool IsEndlessHit {
				get { return GetBoolProperty(7, 1, IsEndlessHitProperty); }
				set { SetBitProperty(7, 1, value, IsEndlessHitProperty); }
			}

			/// <summary>And through the map ?</summary>
			/// <remarks>
			/// Japanese short name: "マップを貫通？", Google translated: "And through the map ?".
			/// Japanese description: "マップを貫通するか？", Google translated: "Either through a map ?".
			/// </remarks>
			[ParameterTableRowAttribute("isPenetrateMap:1", index: 57, minimum: 0, maximum: 1, step: 1, order: 20000, unknown2: 1)]
			[DisplayName("And through the map ?")]
			[Description("Either through a map ?")]
			[DefaultValue(false)]
			public bool IsPenetrateMap {
				get { return GetBoolProperty(8, 1, IsPenetrateMapProperty); }
				set { SetBitProperty(8, 1, value, IsPenetrateMapProperty); }
			}

			/// <summary>The hit to both friend or foe ?</summary>
			/// <remarks>
			/// Japanese short name: "敵味方共にあたる？", Google translated: "The hit to both friend or foe ?".
			/// Japanese description: "敵味方共にあたるか？（徘徊ゴーストにはあたらない）", Google translated: "Or falls on both friend and foe ? ( It is not exposed to the wandering ghost )".
			/// </remarks>
			[ParameterTableRowAttribute("isHitBothTeam:1", index: 58, minimum: 0, maximum: 1, step: 1, order: 21000, unknown2: 1)]
			[DisplayName("The hit to both friend or foe ?")]
			[Description("Or falls on both friend and foe ? ( It is not exposed to the wandering ghost )")]
			[DefaultValue(false)]
			public bool IsHitBothTeam {
				get { return GetBoolProperty(9, 1, IsHitBothTeamProperty); }
				set { SetBitProperty(9, 1, value, IsHitBothTeamProperty); }
			}

			/// <summary>You can share the hit list ?</summary>
			/// <remarks>
			/// Japanese short name: "ヒットリストを共有するか？", Google translated: "You can share the hit list ?".
			/// Japanese description: "ヒットリストを共有するかを指定", Google translated: "Specify whether to share the hit list".
			/// </remarks>
			[ParameterTableRowAttribute("isUseSharedHitList:1", index: 59, minimum: 0, maximum: 1, step: 1, order: 16000, unknown2: 1)]
			[DisplayName("You can share the hit list ?")]
			[Description("Specify whether to share the hit list")]
			[DefaultValue(false)]
			public bool IsUseSharedHitList {
				get { return GetBoolProperty(10, 1, IsUseSharedHitListProperty); }
				set { SetBitProperty(10, 1, value, IsUseSharedHitListProperty); }
			}

			/// <summary>Do use more than one Damipori ?</summary>
			/// <remarks>
			/// Japanese short name: "複数のダミポリを使うか？", Google translated: "Do use more than one Damipori ?".
			/// Japanese description: "弾配置時に同一ダミポリIDを複数使うか？", Google translated: "Do use more than one ID in the same Damipori bullet during deployment ?".
			/// </remarks>
			[ParameterTableRowAttribute("isUseMultiDmyPolyIfPlace:1", index: 60, minimum: 0, maximum: 1, step: 1, order: 22000, unknown2: 1)]
			[DisplayName("Do use more than one Damipori ?")]
			[Description("Do use more than one ID in the same Damipori bullet during deployment ?")]
			[DefaultValue(false)]
			public bool IsUseMultiDmyPolyIfPlace {
				get { return GetBoolProperty(11, 1, IsUseMultiDmyPolyIfPlaceProperty); }
				set { SetBitProperty(11, 1, value, IsUseMultiDmyPolyIfPlaceProperty); }
			}

			/// <summary>Attach effect type</summary>
			/// <remarks>
			/// Japanese short name: "アタッチ効果タイプ", Google translated: "Attach effect type".
			/// Japanese description: "アタッチする効果タイプ", Google translated: "Effect type you want to attach".
			/// </remarks>
			[ParameterTableRowAttribute("attachEffectType:2", index: 61, minimum: 0, maximum: 2, step: 1, order: 61000, unknown2: 1)]
			[DisplayName("Attach effect type")]
			[Description("Effect type you want to attach")]
			[DefaultValue((BulletAttachEffect)0)]
			public BulletAttachEffect AttachEffectType {
				get { return (BulletAttachEffect)GetBitProperty(12, 2, AttachEffectTypeProperty); }
				set { SetBitProperty(12, 2, (int)value, AttachEffectTypeProperty); }
			}

			/// <summary>Or hit the magic force</summary>
			/// <remarks>
			/// Japanese short name: "フォース魔法に当たるか", Google translated: "Or hit the magic force".
			/// Japanese description: "フォース魔法に当たるか", Google translated: "Or hit the magic force".
			/// </remarks>
			[ParameterTableRowAttribute("isHitForceMagic:1", index: 62, minimum: 0, maximum: 1, step: 1, order: 62000, unknown2: 1)]
			[DisplayName("Or hit the magic force")]
			[Description("Or hit the magic force")]
			[DefaultValue(false)]
			public Boolean IsHitForceMagic {
				get { return GetBitProperty(14, 1, IsHitForceMagicProperty) != 0; }
				set { SetBitProperty(14, 1, value ? 1 : 0, IsHitForceMagicProperty); }
			}

			/// <summary>You can ignore the effect of the water surface collision</summary>
			/// <remarks>
			/// Japanese short name: "水面衝突時のエフェクト無視するか", Google translated: "You can ignore the effect of the water surface collision".
			/// Japanese description: "水面に当たった場合はエフェクト無視するか", Google translated: "Either effect ignored when it hits the surface of the water".
			/// </remarks>
			[ParameterTableRowAttribute("isIgnoreSfxIfHitWater:1", index: 63, minimum: 0, maximum: 1, step: 1, order: 63000, unknown2: 1)]
			[DisplayName("You can ignore the effect of the water surface collision")]
			[Description("Either effect ignored when it hits the surface of the water")]
			[DefaultValue(false)]
			public Boolean IsIgnoreSfxIfHitWater {
				get { return GetBitProperty(15, 1, IsIgnoreSfxIfHitWaterProperty) != 0; }
				set { SetBitProperty(15, 1, value ? 1 : 0, IsIgnoreSfxIfHitWaterProperty); }
			}

			/// <summary>You can ignore the state transition of water collision</summary>
			/// <remarks>
			/// Japanese short name: "水面衝突時の状態遷移を無視するか", Google translated: "You can ignore the state transition of water collision".
			/// Japanese description: "水に当たっても状態遷移を無視するか", Google translated: "You can ignore the state transition even hit the water".
			/// </remarks>
			[ParameterTableRowAttribute("isIgnoreMoveStateIfHitWater:1", index: 64, minimum: 0, maximum: 1, step: 1, order: 64000, unknown2: 1)]
			[DisplayName("You can ignore the state transition of water collision")]
			[Description("You can ignore the state transition even hit the water")]
			[DefaultValue(false)]
			public Boolean IsIgnoreMoveStateIfHitWater {
				get { return GetBitProperty(16, 1, IsIgnoreMoveStateIfHitWaterProperty) != 0; }
				set { SetBitProperty(16, 1, value ? 1 : 0, IsIgnoreMoveStateIfHitWaterProperty); }
			}

			/// <summary>Or hit the darkness Force magic</summary>
			/// <remarks>
			/// Japanese short name: "闇フォース魔法に当たるか", Google translated: "Or hit the darkness Force magic".
			/// Japanese description: "闇フォース魔法に当たるか", Google translated: "Or hit the darkness Force magic".
			/// </remarks>
			[ParameterTableRowAttribute("isHitDarkForceMagic:1", index: 65, minimum: 0, maximum: 1, step: 1, order: 62000, unknown2: 1)]
			[DisplayName("Or hit the darkness Force magic")]
			[Description("Or hit the darkness Force magic")]
			[DefaultValue(false)]
			public Boolean IsHitDarkForceMagic {
				get { return GetBitProperty(17, 1, IsHitDarkForceMagicProperty) != 0; }
				set { SetBitProperty(17, 1, value ? 1 : 0, IsHitDarkForceMagicProperty); }
			}

			/// <summary>Padding</summary>
			/// <remarks>
			/// Japanese short name: "パディング", Google translated: "Padding".
			/// Japanese description: "pad", Google translated: "pad".
			/// </remarks>
			[ParameterTableRowAttribute("pad[3]", index: 66, minimum: 0, maximum: 0, step: 0, order: 99999999, unknown2: 0)]
			[DisplayName("Padding")]
			[Description("pad")]
			public Byte[] Pad {
				get { return pad; }
				set { SetProperty(ref pad, ref value, PadProperty); }
			}

			internal Bullet(ParameterTable table, int index, AssetLoader loader, int next)
				: base(table, index, loader) {
				BinaryReader reader = loader.Reader;

				AtkId_Bullet = reader.ReadInt32();
				SfxId_Bullet = reader.ReadInt32();
				SfxId_Hit = reader.ReadInt32();
				SfxId_Flick = reader.ReadInt32();
				Life = reader.ReadSingle();
				Dist = reader.ReadSingle();
				ShootInterval = reader.ReadSingle();
				GravityInRange = reader.ReadSingle();
				GravityOutRange = reader.ReadSingle();
				HormingStopRange = reader.ReadSingle();
				InitVellocity = reader.ReadSingle();
				AccelInRange = reader.ReadSingle();
				AccelOutRange = reader.ReadSingle();
				MaxVellocity = reader.ReadSingle();
				MinVellocity = reader.ReadSingle();
				AccelTime = reader.ReadSingle();
				HomingBeginDist = reader.ReadSingle();
				HitRadius = reader.ReadSingle();
				HitRadiusMax = reader.ReadSingle();
				SpreadTime = reader.ReadSingle();
				ExpDelay = reader.ReadSingle();
				HormingOffsetRange = reader.ReadSingle();
				DmgHitRecordLifeTime = reader.ReadSingle();
				ExternalForce = reader.ReadSingle();
				SpEffectIDForShooter = reader.ReadInt32();
				AutoSearchNPCThinkID = reader.ReadInt32();
				HitBulletID = reader.ReadInt32();
				SpEffectId0 = reader.ReadInt32();
				SpEffectId1 = reader.ReadInt32();
				SpEffectId2 = reader.ReadInt32();
				SpEffectId3 = reader.ReadInt32();
				SpEffectId4 = reader.ReadInt32();
				NumShoot = reader.ReadUInt16();
				HomingAngle = reader.ReadInt16();
				ShootAngle = reader.ReadInt16();
				ShootAngleInterval = reader.ReadInt16();
				ShootAngleXInterval = reader.ReadInt16();
				DamageDamp = reader.ReadSByte();
				SpelDamageDamp = reader.ReadSByte();
				FireDamageDamp = reader.ReadSByte();
				ThunderDamageDamp = reader.ReadSByte();
				StaminaDamp = reader.ReadSByte();
				KnockbackDamp = reader.ReadSByte();
				ShootAngleXZ = reader.ReadSByte();
				LockShootLimitAng = reader.ReadByte();
				IsPenetrate = reader.ReadByte();
				PrevVelocityDirRate = reader.ReadByte();
				AtkAttribute = (AttackAttackAttributes)reader.ReadByte();
				SpAttribute = (AttackParameterSpecialAttributes)reader.ReadByte();
				Material_AttackType = (AttackType)reader.ReadByte();
				Material_AttackMaterial = (WeaponMaterialAttack)reader.ReadByte();
				Material_Size = (AttackSize)reader.ReadByte();
				LaunchConditionType = (BulletLaunchConditionType)reader.ReadByte();
				BitFields = reader.ReadBytes(3);
				Pad = reader.ReadBytes(3);
			}

			internal Bullet(ParameterTable table, int index)
				: base(table, index) {
				BitFields = new byte[3];
				AtkId_Bullet = (Int32)(-1);
				SfxId_Bullet = (Int32)(-1);
				SfxId_Hit = (Int32)(-1);
				SfxId_Flick = (Int32)(-1);
				Life = (Single)(-1);
				Dist = (Single)0;
				ShootInterval = (Single)0;
				GravityInRange = (Single)0;
				GravityOutRange = (Single)0;
				HormingStopRange = (Single)0;
				InitVellocity = (Single)0;
				AccelInRange = (Single)0;
				AccelOutRange = (Single)0;
				MaxVellocity = (Single)0;
				MinVellocity = (Single)0;
				AccelTime = (Single)0;
				HomingBeginDist = (Single)0;
				HitRadius = (Single)(-1);
				HitRadiusMax = (Single)(-1);
				SpreadTime = (Single)0;
				ExpDelay = (Single)0;
				HormingOffsetRange = (Single)0;
				DmgHitRecordLifeTime = (Single)0;
				ExternalForce = (Single)0;
				SpEffectIDForShooter = (Int32)(-1);
				AutoSearchNPCThinkID = (Int32)0;
				HitBulletID = (Int32)(-1);
				SpEffectId0 = (Int32)(-1);
				SpEffectId1 = (Int32)(-1);
				SpEffectId2 = (Int32)(-1);
				SpEffectId3 = (Int32)(-1);
				SpEffectId4 = (Int32)(-1);
				NumShoot = (UInt16)0;
				HomingAngle = (Int16)0;
				ShootAngle = (Int16)0;
				ShootAngleInterval = (Int16)0;
				ShootAngleXInterval = (Int16)0;
				DamageDamp = (SByte)0;
				SpelDamageDamp = (SByte)0;
				FireDamageDamp = (SByte)0;
				ThunderDamageDamp = (SByte)0;
				StaminaDamp = (SByte)0;
				KnockbackDamp = (SByte)0;
				ShootAngleXZ = (SByte)0;
				LockShootLimitAng = (Byte)0;
				IsPenetrate = (Byte)0;
				PrevVelocityDirRate = (Byte)0;
				AtkAttribute = (AttackAttackAttributes)0;
				SpAttribute = (AttackParameterSpecialAttributes)0;
				Material_AttackType = (AttackType)0;
				Material_AttackMaterial = (WeaponMaterialAttack)0;
				Material_Size = (AttackSize)0;
				LaunchConditionType = (BulletLaunchConditionType)0;
				FollowType = (BulletFollowType)0;
				EmittePosType = (BulletEmitterPosition)0;
				IsAttackSFX = false;
				IsEndlessHit = false;
				IsPenetrateMap = false;
				IsHitBothTeam = false;
				IsUseSharedHitList = false;
				IsUseMultiDmyPolyIfPlace = false;
				AttachEffectType = (BulletAttachEffect)0;
				IsHitForceMagic = false;
				IsIgnoreSfxIfHitWater = false;
				IsIgnoreMoveStateIfHitWater = false;
				IsHitDarkForceMagic = false;
				Pad = new Byte[3];
			}

			public override void Write(BinaryWriter writer) {
				writer.Write(AtkId_Bullet);
				writer.Write(SfxId_Bullet);
				writer.Write(SfxId_Hit);
				writer.Write(SfxId_Flick);
				writer.Write(Life);
				writer.Write(Dist);
				writer.Write(ShootInterval);
				writer.Write(GravityInRange);
				writer.Write(GravityOutRange);
				writer.Write(HormingStopRange);
				writer.Write(InitVellocity);
				writer.Write(AccelInRange);
				writer.Write(AccelOutRange);
				writer.Write(MaxVellocity);
				writer.Write(MinVellocity);
				writer.Write(AccelTime);
				writer.Write(HomingBeginDist);
				writer.Write(HitRadius);
				writer.Write(HitRadiusMax);
				writer.Write(SpreadTime);
				writer.Write(ExpDelay);
				writer.Write(HormingOffsetRange);
				writer.Write(DmgHitRecordLifeTime);
				writer.Write(ExternalForce);
				writer.Write(SpEffectIDForShooter);
				writer.Write(AutoSearchNPCThinkID);
				writer.Write(HitBulletID);
				writer.Write(SpEffectId0);
				writer.Write(SpEffectId1);
				writer.Write(SpEffectId2);
				writer.Write(SpEffectId3);
				writer.Write(SpEffectId4);
				writer.Write(NumShoot);
				writer.Write(HomingAngle);
				writer.Write(ShootAngle);
				writer.Write(ShootAngleInterval);
				writer.Write(ShootAngleXInterval);
				writer.Write(DamageDamp);
				writer.Write(SpelDamageDamp);
				writer.Write(FireDamageDamp);
				writer.Write(ThunderDamageDamp);
				writer.Write(StaminaDamp);
				writer.Write(KnockbackDamp);
				writer.Write(ShootAngleXZ);
				writer.Write(LockShootLimitAng);
				writer.Write(IsPenetrate);
				writer.Write(PrevVelocityDirRate);
				writer.Write((Byte)AtkAttribute);
				writer.Write((Byte)SpAttribute);
				writer.Write((Byte)Material_AttackType);
				writer.Write((Byte)Material_AttackMaterial);
				writer.Write((Byte)Material_Size);
				writer.Write((Byte)LaunchConditionType);
				writer.Write(BitFields);
				writer.Write(Pad);
			}
		}
	}
}
