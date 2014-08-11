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
		/// A possibly unused row for providing default AI information.
		/// </summary>
		public class AiStandardInfo : ParameterTableRow {
			/// <summary>The name of the table in the file.</summary>
			public const string TableName = "AI_STANDARD_INFO_BANK";

			UInt16 radarRange, territorySize, attack1_Distance, attack1_Margin, attack2_Distance, attack2_Margin, attack3_Distance, attack3_Margin, attack4_Distance, attack4_Margin;
			Byte radarAngleX, radarAngleY, threatBeforeAttackRate, attack1_Rate, attack1_DelayMin, attack1_DelayMax, attack1_ConeAngle, attack2_Rate, attack2_DelayMin, attack2_DelayMax, attack2_ConeAngle, attack3_Rate, attack3_DelayMin, attack3_DelayMax, attack3_ConeAngle, attack4_Rate, attack4_DelayMin, attack4_DelayMax, attack4_ConeAngle;
			bool forceThreatOnFirstLocked;
			Byte[] reserve0, reserve10, reserve11, reserve12, reserve13, reserve_last;
			ActionPattern attack1_ActionID, attack2_ActionID, attack3_ActionID, attack4_ActionID;

			/// <summary>A property of the class.</summary>
			public static readonly PropertyInfo
				RadarRangeProperty = GetProperty<AiStandardInfo>("RadarRange"),
				RadarAngleXProperty = GetProperty<AiStandardInfo>("RadarAngleX"),
				RadarAngleYProperty = GetProperty<AiStandardInfo>("RadarAngleY"),
				TerritorySizeProperty = GetProperty<AiStandardInfo>("TerritorySize"),
				ThreatBeforeAttackRateProperty = GetProperty<AiStandardInfo>("ThreatBeforeAttackRate"),
				ForceThreatOnFirstLockedProperty = GetProperty<AiStandardInfo>("ForceThreatOnFirstLocked"),
				Reserve0Property = GetProperty<AiStandardInfo>("Reserve0"),
				Attack1_DistanceProperty = GetProperty<AiStandardInfo>("Attack1_Distance"),
				Attack1_MarginProperty = GetProperty<AiStandardInfo>("Attack1_Margin"),
				Attack1_RateProperty = GetProperty<AiStandardInfo>("Attack1_Rate"),
				Attack1_ActionIDProperty = GetProperty<AiStandardInfo>("Attack1_ActionID"),
				Attack1_DelayMinProperty = GetProperty<AiStandardInfo>("Attack1_DelayMin"),
				Attack1_DelayMaxProperty = GetProperty<AiStandardInfo>("Attack1_DelayMax"),
				Attack1_ConeAngleProperty = GetProperty<AiStandardInfo>("Attack1_ConeAngle"),
				Reserve10Property = GetProperty<AiStandardInfo>("Reserve10"),
				Attack2_DistanceProperty = GetProperty<AiStandardInfo>("Attack2_Distance"),
				Attack2_MarginProperty = GetProperty<AiStandardInfo>("Attack2_Margin"),
				Attack2_RateProperty = GetProperty<AiStandardInfo>("Attack2_Rate"),
				Attack2_ActionIDProperty = GetProperty<AiStandardInfo>("Attack2_ActionID"),
				Attack2_DelayMinProperty = GetProperty<AiStandardInfo>("Attack2_DelayMin"),
				Attack2_DelayMaxProperty = GetProperty<AiStandardInfo>("Attack2_DelayMax"),
				Attack2_ConeAngleProperty = GetProperty<AiStandardInfo>("Attack2_ConeAngle"),
				Reserve11Property = GetProperty<AiStandardInfo>("Reserve11"),
				Attack3_DistanceProperty = GetProperty<AiStandardInfo>("Attack3_Distance"),
				Attack3_MarginProperty = GetProperty<AiStandardInfo>("Attack3_Margin"),
				Attack3_RateProperty = GetProperty<AiStandardInfo>("Attack3_Rate"),
				Attack3_ActionIDProperty = GetProperty<AiStandardInfo>("Attack3_ActionID"),
				Attack3_DelayMinProperty = GetProperty<AiStandardInfo>("Attack3_DelayMin"),
				Attack3_DelayMaxProperty = GetProperty<AiStandardInfo>("Attack3_DelayMax"),
				Attack3_ConeAngleProperty = GetProperty<AiStandardInfo>("Attack3_ConeAngle"),
				Reserve12Property = GetProperty<AiStandardInfo>("Reserve12"),
				Attack4_DistanceProperty = GetProperty<AiStandardInfo>("Attack4_Distance"),
				Attack4_MarginProperty = GetProperty<AiStandardInfo>("Attack4_Margin"),
				Attack4_RateProperty = GetProperty<AiStandardInfo>("Attack4_Rate"),
				Attack4_ActionIDProperty = GetProperty<AiStandardInfo>("Attack4_ActionID"),
				Attack4_DelayMinProperty = GetProperty<AiStandardInfo>("Attack4_DelayMin"),
				Attack4_DelayMaxProperty = GetProperty<AiStandardInfo>("Attack4_DelayMax"),
				Attack4_ConeAngleProperty = GetProperty<AiStandardInfo>("Attack4_ConeAngle"),
				Reserve13Property = GetProperty<AiStandardInfo>("Reserve13"),
				Reserve_lastProperty = GetProperty<AiStandardInfo>("Reserve_last");

			/// <summary>Recognition distance [m]</summary>
			/// <remarks>
			/// Japanese short name: "認識距離[m]", Google translated: "Recognition distance [m]".
			/// Japanese description: "敵性キャラクタを認識する距離", Google translated: "And recognizes the distance enemy character".
			/// </remarks>
			[ParameterTableRowAttribute("RadarRange", index: 0, minimum: 0, maximum: 30000, step: 1, sortOrder: 1, unknown2: 1)]
			[DisplayName("Recognition distance [m]")]
			[Description("And recognizes the distance enemy character")]
			[DefaultValue((UInt16)20)]
			public UInt16 RadarRange {
				get { return radarRange; }
				set {
					if ((double)value < 0 || (double)value > 30000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 30000 for RadarRange.");
					SetProperty(ref radarRange, ref value, RadarRangeProperty);
				}
			}

			/// <summary>Recognition angle X [deg]</summary>
			/// <remarks>
			/// Japanese short name: "認識角度Ｘ[deg]", Google translated: "Recognition angle X [deg]".
			/// Japanese description: "敵性キャラクタを認識するX角度　現在の視線方向を０度として、上が＋。", Google translated: "0 degrees as the line-of-sight direction of the current angle X recognize , above + the enemy character .".
			/// </remarks>
			[ParameterTableRowAttribute("RadarAngleX", index: 1, minimum: 0, maximum: 90, step: 1, sortOrder: 2, unknown2: 1)]
			[DisplayName("Recognition angle X [deg]")]
			[Description("0 degrees as the line-of-sight direction of the current angle X recognize , above + the enemy character .")]
			[DefaultValue((Byte)30)]
			public Byte RadarAngleX {
				get { return radarAngleX; }
				set {
					if ((double)value < 0 || (double)value > 90)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 90 for RadarAngleX.");
					SetProperty(ref radarAngleX, ref value, RadarAngleXProperty);
				}
			}

			/// <summary>Recognition angle Y [deg]</summary>
			/// <remarks>
			/// Japanese short name: "認識角度Y[deg]", Google translated: "Recognition angle Y [deg]".
			/// Japanese description: "敵性キャラクタを認識するY角度　現在の視線方向を０度として、右が＋。", Google translated: "0 degrees as the line-of-sight direction of the Y angle currently recognize , right + the enemy character .".
			/// </remarks>
			[ParameterTableRowAttribute("RadarAngleY", index: 2, minimum: 0, maximum: 180, step: 1, sortOrder: 3, unknown2: 1)]
			[DisplayName("Recognition angle Y [deg]")]
			[Description("0 degrees as the line-of-sight direction of the Y angle currently recognize , right + the enemy character .")]
			[DefaultValue((Byte)60)]
			public Byte RadarAngleY {
				get { return radarAngleY; }
				set {
					if ((double)value < 0 || (double)value > 180)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 180 for RadarAngleY.");
					SetProperty(ref radarAngleY, ref value, RadarAngleYProperty);
				}
			}

			/// <summary>Turf distance [m]</summary>
			/// <remarks>
			/// Japanese short name: "縄張り距離[m]", Google translated: "Turf distance [m]".
			/// Japanese description: "自分の縄張りの距離。認識しているプレイヤーがこの距離から外れると初期位置に戻ります。", Google translated: "Distance of their own turf . To return to the initial position player you are aware of is out of this distance .".
			/// </remarks>
			[ParameterTableRowAttribute("TerritorySize", index: 3, minimum: 0, maximum: 30000, step: 1, sortOrder: 4, unknown2: 1)]
			[DisplayName("Turf distance [m]")]
			[Description("Distance of their own turf . To return to the initial position player you are aware of is out of this distance .")]
			[DefaultValue((UInt16)20)]
			public UInt16 TerritorySize {
				get { return territorySize; }
				set {
					if ((double)value < 0 || (double)value > 30000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 30000 for TerritorySize.");
					SetProperty(ref territorySize, ref value, TerritorySizeProperty);
				}
			}

			/// <summary>Pre-attack threat rate [ 0-100 ]</summary>
			/// <remarks>
			/// Japanese short name: "攻撃前威嚇率[0～100]", Google translated: "Pre-attack threat rate [ 0-100 ]".
			/// Japanese description: "攻撃前に威嚇する確率", Google translated: "Probability of threat to pre-attack".
			/// </remarks>
			[ParameterTableRowAttribute("ThreatBeforeAttackRate", index: 4, minimum: 0, maximum: 100, step: 1, sortOrder: 5, unknown2: 1)]
			[DisplayName("Pre-attack threat rate [ 0-100 ]")]
			[Description("Probability of threat to pre-attack")]
			[DefaultValue((Byte)50)]
			public Byte ThreatBeforeAttackRate {
				get { return threatBeforeAttackRate; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for ThreatBeforeAttackRate.");
					SetProperty(ref threatBeforeAttackRate, ref value, ThreatBeforeAttackRateProperty);
				}
			}

			/// <summary>First recognized threat</summary>
			/// <remarks>
			/// Japanese short name: "初回認識威嚇", Google translated: "First recognized threat".
			/// Japanese description: "初回プレイヤー認識時に必ず威嚇するかどうか", Google translated: "Whether menacing always the first player recognition".
			/// </remarks>
			[ParameterTableRowAttribute("ForceThreatOnFirstLocked", index: 5, minimum: 0, maximum: 0, step: 1, sortOrder: 6, unknown2: 1)]
			[DisplayName("First recognized threat")]
			[Description("Whether menacing always the first player recognition")]
			[DefaultValue(false)]
			public bool ForceThreatOnFirstLocked {
				get { return forceThreatOnFirstLocked; }
				set { SetProperty(ref forceThreatOnFirstLocked, ref value, ForceThreatOnFirstLockedProperty); }
			}

			/// <summary>Reservation</summary>
			/// <remarks>
			/// Japanese short name: "予約", Google translated: "Reservation".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("reserve0[24]", index: 6, minimum: 0, maximum: 0, step: 0, sortOrder: 41, unknown2: 0)]
			[DisplayName("Reservation")]
			[Description("")]
			[Browsable(false)]
			public Byte[] Reserve0 {
				get { return reserve0; }
				set { SetProperty(ref reserve0, ref value, Reserve0Property); }
			}

			/// <summary>1 Reach attack [m]</summary>
			/// <remarks>
			/// Japanese short name: "攻撃１　間合い[m]", Google translated: "1 Reach attack [m]".
			/// Japanese description: "攻撃するときの間合い[m]", Google translated: "Reach when you attack [m]".
			/// </remarks>
			[ParameterTableRowAttribute("Attack1_Distance", index: 7, minimum: 0, maximum: 30000, step: 1, sortOrder: 7, unknown2: 1)]
			[DisplayName("1 Reach attack [m]")]
			[Description("Reach when you attack [m]")]
			[DefaultValue((UInt16)0)]
			public UInt16 Attack1_Distance {
				get { return attack1_Distance; }
				set {
					if ((double)value < 0 || (double)value > 30000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 30000 for Attack1_Distance.");
					SetProperty(ref attack1_Distance, ref value, Attack1_DistanceProperty);
				}
			}

			/// <summary>1 Reach play attack [m]</summary>
			/// <remarks>
			/// Japanese short name: "攻撃１　間合い遊び[m]", Google translated: "1 Reach play attack [m]".
			/// Japanese description: "攻撃間合いの遊び。間合い距離近辺で、振動しないように", Google translated: "Play of attack Reach . Reach distance in the vicinity , so as not to vibrate".
			/// </remarks>
			[ParameterTableRowAttribute("Attack1_Margin", index: 8, minimum: 0, maximum: 100, step: 1, sortOrder: 8, unknown2: 1)]
			[DisplayName("1 Reach play attack [m]")]
			[Description("Play of attack Reach . Reach distance in the vicinity , so as not to vibrate")]
			[DefaultValue((UInt16)0)]
			public UInt16 Attack1_Margin {
				get { return attack1_Margin; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for Attack1_Margin.");
					SetProperty(ref attack1_Margin, ref value, Attack1_MarginProperty);
				}
			}

			/// <summary>1 percentage attack [ 0-100 ]</summary>
			/// <remarks>
			/// Japanese short name: "攻撃１　割合[0～100]", Google translated: "1 percentage attack [ 0-100 ]".
			/// Japanese description: "攻撃の頻度", Google translated: "Frequency of attacks".
			/// </remarks>
			[ParameterTableRowAttribute("Attack1_Rate", index: 9, minimum: 0, maximum: 100, step: 1, sortOrder: 9, unknown2: 1)]
			[DisplayName("1 percentage attack [ 0-100 ]")]
			[Description("Frequency of attacks")]
			[DefaultValue((Byte)50)]
			public Byte Attack1_Rate {
				get { return attack1_Rate; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for Attack1_Rate.");
					SetProperty(ref attack1_Rate, ref value, Attack1_RateProperty);
				}
			}

			/// <summary>One type of attack</summary>
			/// <remarks>
			/// Japanese short name: "攻撃１　種類", Google translated: "One type of attack".
			/// Japanese description: "攻撃の種類", Google translated: "Type of attack".
			/// </remarks>
			[ParameterTableRowAttribute("Attack1_ActionID", index: 10, minimum: 0, maximum: 0, step: 1, sortOrder: 10, unknown2: 1)]
			[DisplayName("One type of attack")]
			[Description("Type of attack")]
			[DefaultValue((ActionPattern)0)]
			public ActionPattern Attack1_ActionID {
				get { return attack1_ActionID; }
				set { SetProperty(ref attack1_ActionID, ref value, Attack1_ActionIDProperty); }
			}

			/// <summary>1 minimum delay time attack [frame]</summary>
			/// <remarks>
			/// Japanese short name: "攻撃１　最小遅延時間[frame]", Google translated: "1 minimum delay time attack [frame]".
			/// Japanese description: "攻撃可能になった時点から、攻撃するまでの遅延時間の最小。", Google translated: "From when it becomes possible attack , the minimum of delay time to attack .".
			/// </remarks>
			[ParameterTableRowAttribute("Attack1_DelayMin", index: 11, minimum: 0, maximum: 255, step: 1, sortOrder: 11, unknown2: 1)]
			[DisplayName("1 minimum delay time attack [frame]")]
			[Description("From when it becomes possible attack , the minimum of delay time to attack .")]
			[DefaultValue((Byte)0)]
			public Byte Attack1_DelayMin {
				get { return attack1_DelayMin; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for Attack1_DelayMin.");
					SetProperty(ref attack1_DelayMin, ref value, Attack1_DelayMinProperty);
				}
			}

			/// <summary>1 longest delay time attack [frame]</summary>
			/// <remarks>
			/// Japanese short name: "攻撃１　最長遅延時間[frame]", Google translated: "1 longest delay time attack [frame]".
			/// Japanese description: "攻撃可能になった時点から、攻撃するまでの遅延時間の最長。", Google translated: "From when it becomes possible attack , longest delay time to attack .".
			/// </remarks>
			[ParameterTableRowAttribute("Attack1_DelayMax", index: 12, minimum: 0, maximum: 255, step: 1, sortOrder: 12, unknown2: 1)]
			[DisplayName("1 longest delay time attack [frame]")]
			[Description("From when it becomes possible attack , longest delay time to attack .")]
			[DefaultValue((Byte)0)]
			public Byte Attack1_DelayMax {
				get { return attack1_DelayMax; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for Attack1_DelayMax.");
					SetProperty(ref attack1_DelayMax, ref value, Attack1_DelayMaxProperty);
				}
			}

			/// <summary>Angle of attack allowed one cone attack [deg]</summary>
			/// <remarks>
			/// Japanese short name: "攻撃１　攻撃許可円錐の角度[deg]", Google translated: "Angle of attack allowed one cone attack [deg]".
			/// Japanese description: "視線方向とターゲットへの方向ベクトルのなす角が、この角度以内の場合、攻撃ＯＫ。", Google translated: "In the case of angle within this , the angle of the direction vector to the target and the direction of the line of sight , attack OK.".
			/// </remarks>
			[ParameterTableRowAttribute("Attack1_ConeAngle", index: 13, minimum: 0, maximum: 180, step: 1, sortOrder: 13, unknown2: 1)]
			[DisplayName("Angle of attack allowed one cone attack [deg]")]
			[Description("In the case of angle within this , the angle of the direction vector to the target and the direction of the line of sight , attack OK.")]
			[DefaultValue((Byte)30)]
			public Byte Attack1_ConeAngle {
				get { return attack1_ConeAngle; }
				set {
					if ((double)value < 0 || (double)value > 180)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 180 for Attack1_ConeAngle.");
					SetProperty(ref attack1_ConeAngle, ref value, Attack1_ConeAngleProperty);
				}
			}

			/// <summary>Reservation</summary>
			/// <remarks>
			/// Japanese short name: "予約", Google translated: "Reservation".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("reserve10[7]", index: 14, minimum: 0, maximum: 0, step: 0, sortOrder: 42, unknown2: 0)]
			[DisplayName("Reservation")]
			[Description("")]
			[Browsable(false)]
			public Byte[] Reserve10 {
				get { return reserve10; }
				set { SetProperty(ref reserve10, ref value, Reserve10Property); }
			}

			/// <summary>2 Reach attack [m]</summary>
			/// <remarks>
			/// Japanese short name: "攻撃２　間合い[m]", Google translated: "2 Reach attack [m]".
			/// Japanese description: "攻撃するときの間合い[m]", Google translated: "Reach when you attack [m]".
			/// </remarks>
			[ParameterTableRowAttribute("Attack2_Distance", index: 15, minimum: 0, maximum: 30000, step: 1, sortOrder: 14, unknown2: 1)]
			[DisplayName("2 Reach attack [m]")]
			[Description("Reach when you attack [m]")]
			[DefaultValue((UInt16)0)]
			public UInt16 Attack2_Distance {
				get { return attack2_Distance; }
				set {
					if ((double)value < 0 || (double)value > 30000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 30000 for Attack2_Distance.");
					SetProperty(ref attack2_Distance, ref value, Attack2_DistanceProperty);
				}
			}

			/// <summary>2 Reach play attack [m]</summary>
			/// <remarks>
			/// Japanese short name: "攻撃２　間合い遊び[m]", Google translated: "2 Reach play attack [m]".
			/// Japanese description: "攻撃間合いの遊び。間合い距離近辺で、振動しないように", Google translated: "Play of attack Reach . Reach distance in the vicinity , so as not to vibrate".
			/// </remarks>
			[ParameterTableRowAttribute("Attack2_Margin", index: 16, minimum: 0, maximum: 100, step: 1, sortOrder: 15, unknown2: 1)]
			[DisplayName("2 Reach play attack [m]")]
			[Description("Play of attack Reach . Reach distance in the vicinity , so as not to vibrate")]
			[DefaultValue((UInt16)0)]
			public UInt16 Attack2_Margin {
				get { return attack2_Margin; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for Attack2_Margin.");
					SetProperty(ref attack2_Margin, ref value, Attack2_MarginProperty);
				}
			}

			/// <summary>1 percentage attack [ 0-100 ]</summary>
			/// <remarks>
			/// Japanese short name: "攻撃１　割合[0～100]", Google translated: "1 percentage attack [ 0-100 ]".
			/// Japanese description: "攻撃の頻度", Google translated: "Frequency of attacks".
			/// </remarks>
			[ParameterTableRowAttribute("Attack2_Rate", index: 17, minimum: 0, maximum: 100, step: 1, sortOrder: 16, unknown2: 1)]
			[DisplayName("1 percentage attack [ 0-100 ]")]
			[Description("Frequency of attacks")]
			[DefaultValue((Byte)50)]
			public Byte Attack2_Rate {
				get { return attack2_Rate; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for Attack2_Rate.");
					SetProperty(ref attack2_Rate, ref value, Attack2_RateProperty);
				}
			}

			/// <summary>Two types of attack</summary>
			/// <remarks>
			/// Japanese short name: "攻撃２　種類", Google translated: "Two types of attack".
			/// Japanese description: "攻撃の種類", Google translated: "Type of attack".
			/// </remarks>
			[ParameterTableRowAttribute("Attack2_ActionID", index: 18, minimum: 0, maximum: 0, step: 1, sortOrder: 17, unknown2: 1)]
			[DisplayName("Two types of attack")]
			[Description("Type of attack")]
			[DefaultValue((ActionPattern)0)]
			public ActionPattern Attack2_ActionID {
				get { return attack2_ActionID; }
				set { SetProperty(ref attack2_ActionID, ref value, Attack2_ActionIDProperty); }
			}

			/// <summary>2 minimum delay time attack [frame]</summary>
			/// <remarks>
			/// Japanese short name: "攻撃2　最小遅延時間[frame]", Google translated: "2 minimum delay time attack [frame]".
			/// Japanese description: "攻撃可能になった時点から、攻撃するまでの遅延時間の最小。", Google translated: "From when it becomes possible attack , the minimum of delay time to attack .".
			/// </remarks>
			[ParameterTableRowAttribute("Attack2_DelayMin", index: 19, minimum: 0, maximum: 255, step: 1, sortOrder: 18, unknown2: 1)]
			[DisplayName("2 minimum delay time attack [frame]")]
			[Description("From when it becomes possible attack , the minimum of delay time to attack .")]
			[DefaultValue((Byte)0)]
			public Byte Attack2_DelayMin {
				get { return attack2_DelayMin; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for Attack2_DelayMin.");
					SetProperty(ref attack2_DelayMin, ref value, Attack2_DelayMinProperty);
				}
			}

			/// <summary>Two longest delay time attack [frame]</summary>
			/// <remarks>
			/// Japanese short name: "攻撃2　最長遅延時間[frame]", Google translated: "Two longest delay time attack [frame]".
			/// Japanese description: "攻撃可能になった時点から、攻撃するまでの遅延時間の最長。", Google translated: "From when it becomes possible attack , longest delay time to attack .".
			/// </remarks>
			[ParameterTableRowAttribute("Attack2_DelayMax", index: 20, minimum: 0, maximum: 255, step: 1, sortOrder: 19, unknown2: 1)]
			[DisplayName("Two longest delay time attack [frame]")]
			[Description("From when it becomes possible attack , longest delay time to attack .")]
			[DefaultValue((Byte)0)]
			public Byte Attack2_DelayMax {
				get { return attack2_DelayMax; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for Attack2_DelayMax.");
					SetProperty(ref attack2_DelayMax, ref value, Attack2_DelayMaxProperty);
				}
			}

			/// <summary>Angle of attack allowed two cone attack [deg]</summary>
			/// <remarks>
			/// Japanese short name: "攻撃2　攻撃許可円錐の角度[deg]", Google translated: "Angle of attack allowed two cone attack [deg]".
			/// Japanese description: "視線方向とターゲットへの方向ベクトルのなす角が、この角度以内の場合、攻撃ＯＫ。", Google translated: "In the case of angle within this , the angle of the direction vector to the target and the direction of the line of sight , attack OK.".
			/// </remarks>
			[ParameterTableRowAttribute("Attack2_ConeAngle", index: 21, minimum: 0, maximum: 180, step: 1, sortOrder: 20, unknown2: 1)]
			[DisplayName("Angle of attack allowed two cone attack [deg]")]
			[Description("In the case of angle within this , the angle of the direction vector to the target and the direction of the line of sight , attack OK.")]
			[DefaultValue((Byte)30)]
			public Byte Attack2_ConeAngle {
				get { return attack2_ConeAngle; }
				set {
					if ((double)value < 0 || (double)value > 180)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 180 for Attack2_ConeAngle.");
					SetProperty(ref attack2_ConeAngle, ref value, Attack2_ConeAngleProperty);
				}
			}

			/// <summary>Reservation</summary>
			/// <remarks>
			/// Japanese short name: "予約", Google translated: "Reservation".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("reserve11[7]", index: 22, minimum: 0, maximum: 0, step: 0, sortOrder: 43, unknown2: 0)]
			[DisplayName("Reservation")]
			[Description("")]
			[Browsable(false)]
			public Byte[] Reserve11 {
				get { return reserve11; }
				set { SetProperty(ref reserve11, ref value, Reserve11Property); }
			}

			/// <summary>3 Reach attack [m]</summary>
			/// <remarks>
			/// Japanese short name: "攻撃３　間合い[m]", Google translated: "3 Reach attack [m]".
			/// Japanese description: "攻撃するときの間合い[m]", Google translated: "Reach when you attack [m]".
			/// </remarks>
			[ParameterTableRowAttribute("Attack3_Distance", index: 23, minimum: 0, maximum: 30000, step: 1, sortOrder: 21, unknown2: 1)]
			[DisplayName("3 Reach attack [m]")]
			[Description("Reach when you attack [m]")]
			[DefaultValue((UInt16)0)]
			public UInt16 Attack3_Distance {
				get { return attack3_Distance; }
				set {
					if ((double)value < 0 || (double)value > 30000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 30000 for Attack3_Distance.");
					SetProperty(ref attack3_Distance, ref value, Attack3_DistanceProperty);
				}
			}

			/// <summary>3 Reach play attack [m]</summary>
			/// <remarks>
			/// Japanese short name: "攻撃３　間合い遊び[m]", Google translated: "3 Reach play attack [m]".
			/// Japanese description: "攻撃間合いの遊び。間合い距離近辺で、振動しないように", Google translated: "Play of attack Reach . Reach distance in the vicinity , so as not to vibrate".
			/// </remarks>
			[ParameterTableRowAttribute("Attack3_Margin", index: 24, minimum: 0, maximum: 100, step: 1, sortOrder: 22, unknown2: 1)]
			[DisplayName("3 Reach play attack [m]")]
			[Description("Play of attack Reach . Reach distance in the vicinity , so as not to vibrate")]
			[DefaultValue((UInt16)0)]
			public UInt16 Attack3_Margin {
				get { return attack3_Margin; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for Attack3_Margin.");
					SetProperty(ref attack3_Margin, ref value, Attack3_MarginProperty);
				}
			}

			/// <summary>1 percentage attack [ 0-100 ]</summary>
			/// <remarks>
			/// Japanese short name: "攻撃１　割合[0～100]", Google translated: "1 percentage attack [ 0-100 ]".
			/// Japanese description: "攻撃の頻度", Google translated: "Frequency of attacks".
			/// </remarks>
			[ParameterTableRowAttribute("Attack3_Rate", index: 25, minimum: 0, maximum: 100, step: 1, sortOrder: 23, unknown2: 1)]
			[DisplayName("1 percentage attack [ 0-100 ]")]
			[Description("Frequency of attacks")]
			[DefaultValue((Byte)50)]
			public Byte Attack3_Rate {
				get { return attack3_Rate; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for Attack3_Rate.");
					SetProperty(ref attack3_Rate, ref value, Attack3_RateProperty);
				}
			}

			/// <summary>Three types of attack</summary>
			/// <remarks>
			/// Japanese short name: "攻撃３　種類", Google translated: "Three types of attack".
			/// Japanese description: "攻撃の種類", Google translated: "Type of attack".
			/// </remarks>
			[ParameterTableRowAttribute("Attack3_ActionID", index: 26, minimum: 0, maximum: 0, step: 1, sortOrder: 24, unknown2: 1)]
			[DisplayName("Three types of attack")]
			[Description("Type of attack")]
			[DefaultValue((ActionPattern)0)]
			public ActionPattern Attack3_ActionID {
				get { return attack3_ActionID; }
				set { SetProperty(ref attack3_ActionID, ref value, Attack3_ActionIDProperty); }
			}

			/// <summary>3 minimum delay time attack [frame]</summary>
			/// <remarks>
			/// Japanese short name: "攻撃3　最小遅延時間[frame]", Google translated: "3 minimum delay time attack [frame]".
			/// Japanese description: "攻撃可能になった時点から、攻撃するまでの遅延時間の最小。", Google translated: "From when it becomes possible attack , the minimum of delay time to attack .".
			/// </remarks>
			[ParameterTableRowAttribute("Attack3_DelayMin", index: 27, minimum: 0, maximum: 255, step: 1, sortOrder: 25, unknown2: 1)]
			[DisplayName("3 minimum delay time attack [frame]")]
			[Description("From when it becomes possible attack , the minimum of delay time to attack .")]
			[DefaultValue((Byte)0)]
			public Byte Attack3_DelayMin {
				get { return attack3_DelayMin; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for Attack3_DelayMin.");
					SetProperty(ref attack3_DelayMin, ref value, Attack3_DelayMinProperty);
				}
			}

			/// <summary>3 longest delay time attack [frame]</summary>
			/// <remarks>
			/// Japanese short name: "攻撃3　最長遅延時間[frame]", Google translated: "3 longest delay time attack [frame]".
			/// Japanese description: "攻撃可能になった時点から、攻撃するまでの遅延時間の最長。", Google translated: "From when it becomes possible attack , longest delay time to attack .".
			/// </remarks>
			[ParameterTableRowAttribute("Attack3_DelayMax", index: 28, minimum: 0, maximum: 255, step: 1, sortOrder: 26, unknown2: 1)]
			[DisplayName("3 longest delay time attack [frame]")]
			[Description("From when it becomes possible attack , longest delay time to attack .")]
			[DefaultValue((Byte)0)]
			public Byte Attack3_DelayMax {
				get { return attack3_DelayMax; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for Attack3_DelayMax.");
					SetProperty(ref attack3_DelayMax, ref value, Attack3_DelayMaxProperty);
				}
			}

			/// <summary>Angle of attack allowed three cone attack [deg]</summary>
			/// <remarks>
			/// Japanese short name: "攻撃3　攻撃許可円錐の角度[deg]", Google translated: "Angle of attack allowed three cone attack [deg]".
			/// Japanese description: "視線方向とターゲットへの方向ベクトルのなす角が、この角度以内の場合、攻撃ＯＫ。", Google translated: "In the case of angle within this , the angle of the direction vector to the target and the direction of the line of sight , attack OK.".
			/// </remarks>
			[ParameterTableRowAttribute("Attack3_ConeAngle", index: 29, minimum: 0, maximum: 180, step: 1, sortOrder: 27, unknown2: 1)]
			[DisplayName("Angle of attack allowed three cone attack [deg]")]
			[Description("In the case of angle within this , the angle of the direction vector to the target and the direction of the line of sight , attack OK.")]
			[DefaultValue((Byte)30)]
			public Byte Attack3_ConeAngle {
				get { return attack3_ConeAngle; }
				set {
					if ((double)value < 0 || (double)value > 180)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 180 for Attack3_ConeAngle.");
					SetProperty(ref attack3_ConeAngle, ref value, Attack3_ConeAngleProperty);
				}
			}

			/// <summary>Reservation</summary>
			/// <remarks>
			/// Japanese short name: "予約", Google translated: "Reservation".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("reserve12[7]", index: 30, minimum: 0, maximum: 0, step: 0, sortOrder: 44, unknown2: 0)]
			[DisplayName("Reservation")]
			[Description("")]
			[Browsable(false)]
			public Byte[] Reserve12 {
				get { return reserve12; }
				set { SetProperty(ref reserve12, ref value, Reserve12Property); }
			}

			/// <summary>4 Reach attack [m]</summary>
			/// <remarks>
			/// Japanese short name: "攻撃４　間合い[m]", Google translated: "4 Reach attack [m]".
			/// Japanese description: "攻撃するときの間合い[m]", Google translated: "Reach when you attack [m]".
			/// </remarks>
			[ParameterTableRowAttribute("Attack4_Distance", index: 31, minimum: 0, maximum: 30000, step: 1, sortOrder: 28, unknown2: 1)]
			[DisplayName("4 Reach attack [m]")]
			[Description("Reach when you attack [m]")]
			[DefaultValue((UInt16)0)]
			public UInt16 Attack4_Distance {
				get { return attack4_Distance; }
				set {
					if ((double)value < 0 || (double)value > 30000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 30000 for Attack4_Distance.");
					SetProperty(ref attack4_Distance, ref value, Attack4_DistanceProperty);
				}
			}

			/// <summary>4 Reach play attack [m]</summary>
			/// <remarks>
			/// Japanese short name: "攻撃４　間合い遊び[m]", Google translated: "4 Reach play attack [m]".
			/// Japanese description: "攻撃間合いの遊び。間合い距離近辺で、振動しないように", Google translated: "Play of attack Reach . Reach distance in the vicinity , so as not to vibrate".
			/// </remarks>
			[ParameterTableRowAttribute("Attack4_Margin", index: 32, minimum: 0, maximum: 100, step: 1, sortOrder: 29, unknown2: 1)]
			[DisplayName("4 Reach play attack [m]")]
			[Description("Play of attack Reach . Reach distance in the vicinity , so as not to vibrate")]
			[DefaultValue((UInt16)0)]
			public UInt16 Attack4_Margin {
				get { return attack4_Margin; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for Attack4_Margin.");
					SetProperty(ref attack4_Margin, ref value, Attack4_MarginProperty);
				}
			}

			/// <summary>1 percentage attack [ 0-100 ]</summary>
			/// <remarks>
			/// Japanese short name: "攻撃１　割合[0～100]", Google translated: "1 percentage attack [ 0-100 ]".
			/// Japanese description: "攻撃の頻度", Google translated: "Frequency of attacks".
			/// </remarks>
			[ParameterTableRowAttribute("Attack4_Rate", index: 33, minimum: 0, maximum: 100, step: 1, sortOrder: 30, unknown2: 1)]
			[DisplayName("1 percentage attack [ 0-100 ]")]
			[Description("Frequency of attacks")]
			[DefaultValue((Byte)50)]
			public Byte Attack4_Rate {
				get { return attack4_Rate; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for Attack4_Rate.");
					SetProperty(ref attack4_Rate, ref value, Attack4_RateProperty);
				}
			}

			/// <summary>Four types of attack</summary>
			/// <remarks>
			/// Japanese short name: "攻撃４　種類", Google translated: "Four types of attack".
			/// Japanese description: "攻撃の種類", Google translated: "Type of attack".
			/// </remarks>
			[ParameterTableRowAttribute("Attack4_ActionID", index: 34, minimum: 0, maximum: 0, step: 1, sortOrder: 31, unknown2: 1)]
			[DisplayName("Four types of attack")]
			[Description("Type of attack")]
			[DefaultValue((ActionPattern)0)]
			public ActionPattern Attack4_ActionID {
				get { return attack4_ActionID; }
				set { SetProperty(ref attack4_ActionID, ref value, Attack4_ActionIDProperty); }
			}

			/// <summary>4 minimum delay time attack [frame]</summary>
			/// <remarks>
			/// Japanese short name: "攻撃4　最小遅延時間[frame]", Google translated: "4 minimum delay time attack [frame]".
			/// Japanese description: "攻撃可能になった時点から、攻撃するまでの遅延時間の最小。", Google translated: "From when it becomes possible attack , the minimum of delay time to attack .".
			/// </remarks>
			[ParameterTableRowAttribute("Attack4_DelayMin", index: 35, minimum: 0, maximum: 255, step: 1, sortOrder: 32, unknown2: 1)]
			[DisplayName("4 minimum delay time attack [frame]")]
			[Description("From when it becomes possible attack , the minimum of delay time to attack .")]
			[DefaultValue((Byte)0)]
			public Byte Attack4_DelayMin {
				get { return attack4_DelayMin; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for Attack4_DelayMin.");
					SetProperty(ref attack4_DelayMin, ref value, Attack4_DelayMinProperty);
				}
			}

			/// <summary>4 longest delay time attack [frame]</summary>
			/// <remarks>
			/// Japanese short name: "攻撃4　最長遅延時間[frame]", Google translated: "4 longest delay time attack [frame]".
			/// Japanese description: "攻撃可能になった時点から、攻撃するまでの遅延時間の最長。", Google translated: "From when it becomes possible attack , longest delay time to attack .".
			/// </remarks>
			[ParameterTableRowAttribute("Attack4_DelayMax", index: 36, minimum: 0, maximum: 255, step: 1, sortOrder: 33, unknown2: 1)]
			[DisplayName("4 longest delay time attack [frame]")]
			[Description("From when it becomes possible attack , longest delay time to attack .")]
			[DefaultValue((Byte)0)]
			public Byte Attack4_DelayMax {
				get { return attack4_DelayMax; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for Attack4_DelayMax.");
					SetProperty(ref attack4_DelayMax, ref value, Attack4_DelayMaxProperty);
				}
			}

			/// <summary>Angle of attack 4 permission cone attack [deg]</summary>
			/// <remarks>
			/// Japanese short name: "攻撃4　攻撃許可円錐の角度[deg]", Google translated: "Angle of attack 4 permission cone attack [deg]".
			/// Japanese description: "視線方向とターゲットへの方向ベクトルのなす角が、この角度以内の場合、攻撃ＯＫ。", Google translated: "In the case of angle within this , the angle of the direction vector to the target and the direction of the line of sight , attack OK.".
			/// </remarks>
			[ParameterTableRowAttribute("Attack4_ConeAngle", index: 37, minimum: 0, maximum: 180, step: 1, sortOrder: 34, unknown2: 1)]
			[DisplayName("Angle of attack 4 permission cone attack [deg]")]
			[Description("In the case of angle within this , the angle of the direction vector to the target and the direction of the line of sight , attack OK.")]
			[DefaultValue((Byte)30)]
			public Byte Attack4_ConeAngle {
				get { return attack4_ConeAngle; }
				set {
					if ((double)value < 0 || (double)value > 180)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 180 for Attack4_ConeAngle.");
					SetProperty(ref attack4_ConeAngle, ref value, Attack4_ConeAngleProperty);
				}
			}

			/// <summary>Reservation</summary>
			/// <remarks>
			/// Japanese short name: "予約", Google translated: "Reservation".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("reserve13[7]", index: 38, minimum: 0, maximum: 0, step: 0, sortOrder: 45, unknown2: 0)]
			[DisplayName("Reservation")]
			[Description("")]
			[Browsable(false)]
			public Byte[] Reserve13 {
				get { return reserve13; }
				set { SetProperty(ref reserve13, ref value, Reserve13Property); }
			}

			/// <summary>Reservation</summary>
			/// <remarks>
			/// Japanese short name: "予約", Google translated: "Reservation".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("reserve_last[32]", index: 39, minimum: 0, maximum: 0, step: 0, sortOrder: 46, unknown2: 0)]
			[DisplayName("Reservation")]
			[Description("")]
			[Browsable(false)]
			public Byte[] Reserve_last {
				get { return reserve_last; }
				set { SetProperty(ref reserve_last, ref value, Reserve_lastProperty); }
			}

			internal AiStandardInfo(ParameterTable table, int index, AssetLoader loader, int next)
				: base(table, index, loader) {
				BinaryReader reader = loader.Reader;

				RadarRange = reader.ReadUInt16();
				RadarAngleX = reader.ReadByte();
				RadarAngleY = reader.ReadByte();
				TerritorySize = reader.ReadUInt16();
				ThreatBeforeAttackRate = reader.ReadByte();
				ForceThreatOnFirstLocked = reader.ReadByte() != 0;
				Reserve0 = reader.ReadBytes(24);
				Attack1_Distance = reader.ReadUInt16();
				Attack1_Margin = reader.ReadUInt16();
				Attack1_Rate = reader.ReadByte();
				Attack1_ActionID = (ActionPattern)reader.ReadByte();
				Attack1_DelayMin = reader.ReadByte();
				Attack1_DelayMax = reader.ReadByte();
				Attack1_ConeAngle = reader.ReadByte();
				Reserve10 = reader.ReadBytes(7);
				Attack2_Distance = reader.ReadUInt16();
				Attack2_Margin = reader.ReadUInt16();
				Attack2_Rate = reader.ReadByte();
				Attack2_ActionID = (ActionPattern)reader.ReadByte();
				Attack2_DelayMin = reader.ReadByte();
				Attack2_DelayMax = reader.ReadByte();
				Attack2_ConeAngle = reader.ReadByte();
				Reserve11 = reader.ReadBytes(7);
				Attack3_Distance = reader.ReadUInt16();
				Attack3_Margin = reader.ReadUInt16();
				Attack3_Rate = reader.ReadByte();
				Attack3_ActionID = (ActionPattern)reader.ReadByte();
				Attack3_DelayMin = reader.ReadByte();
				Attack3_DelayMax = reader.ReadByte();
				Attack3_ConeAngle = reader.ReadByte();
				Reserve12 = reader.ReadBytes(7);
				Attack4_Distance = reader.ReadUInt16();
				Attack4_Margin = reader.ReadUInt16();
				Attack4_Rate = reader.ReadByte();
				Attack4_ActionID = (ActionPattern)reader.ReadByte();
				Attack4_DelayMin = reader.ReadByte();
				Attack4_DelayMax = reader.ReadByte();
				Attack4_ConeAngle = reader.ReadByte();
				Reserve13 = reader.ReadBytes(7);
				Reserve_last = reader.ReadBytes(32);
			}

			internal AiStandardInfo(ParameterTable table, int index)
				: base(table, index) {
				RadarRange = (UInt16)20;
				RadarAngleX = (Byte)30;
				RadarAngleY = (Byte)60;
				TerritorySize = (UInt16)20;
				ThreatBeforeAttackRate = (Byte)50;
				ForceThreatOnFirstLocked = false;
				Reserve0 = new Byte[24];
				Attack1_Distance = (UInt16)0;
				Attack1_Margin = (UInt16)0;
				Attack1_Rate = (Byte)50;
				Attack1_ActionID = (ActionPattern)0;
				Attack1_DelayMin = (Byte)0;
				Attack1_DelayMax = (Byte)0;
				Attack1_ConeAngle = (Byte)30;
				Reserve10 = new Byte[7];
				Attack2_Distance = (UInt16)0;
				Attack2_Margin = (UInt16)0;
				Attack2_Rate = (Byte)50;
				Attack2_ActionID = (ActionPattern)0;
				Attack2_DelayMin = (Byte)0;
				Attack2_DelayMax = (Byte)0;
				Attack2_ConeAngle = (Byte)30;
				Reserve11 = new Byte[7];
				Attack3_Distance = (UInt16)0;
				Attack3_Margin = (UInt16)0;
				Attack3_Rate = (Byte)50;
				Attack3_ActionID = (ActionPattern)0;
				Attack3_DelayMin = (Byte)0;
				Attack3_DelayMax = (Byte)0;
				Attack3_ConeAngle = (Byte)30;
				Reserve12 = new Byte[7];
				Attack4_Distance = (UInt16)0;
				Attack4_Margin = (UInt16)0;
				Attack4_Rate = (Byte)50;
				Attack4_ActionID = (ActionPattern)0;
				Attack4_DelayMin = (Byte)0;
				Attack4_DelayMax = (Byte)0;
				Attack4_ConeAngle = (Byte)30;
				Reserve13 = new Byte[7];
				Reserve_last = new Byte[32];
			}

			/// <summary>Write the row to the writer.</summary>
			/// <param name="writer"></param>
			public override void Write(BinaryWriter writer) {
				writer.Write(RadarRange);
				writer.Write(RadarAngleX);
				writer.Write(RadarAngleY);
				writer.Write(TerritorySize);
				writer.Write(ThreatBeforeAttackRate);
				writer.Write((byte)(ForceThreatOnFirstLocked ? 1 : 0));
				writer.Write(Reserve0);
				writer.Write(Attack1_Distance);
				writer.Write(Attack1_Margin);
				writer.Write(Attack1_Rate);
				writer.Write((Byte)Attack1_ActionID);
				writer.Write(Attack1_DelayMin);
				writer.Write(Attack1_DelayMax);
				writer.Write(Attack1_ConeAngle);
				writer.Write(Reserve10);
				writer.Write(Attack2_Distance);
				writer.Write(Attack2_Margin);
				writer.Write(Attack2_Rate);
				writer.Write((Byte)Attack2_ActionID);
				writer.Write(Attack2_DelayMin);
				writer.Write(Attack2_DelayMax);
				writer.Write(Attack2_ConeAngle);
				writer.Write(Reserve11);
				writer.Write(Attack3_Distance);
				writer.Write(Attack3_Margin);
				writer.Write(Attack3_Rate);
				writer.Write((Byte)Attack3_ActionID);
				writer.Write(Attack3_DelayMin);
				writer.Write(Attack3_DelayMax);
				writer.Write(Attack3_ConeAngle);
				writer.Write(Reserve12);
				writer.Write(Attack4_Distance);
				writer.Write(Attack4_Margin);
				writer.Write(Attack4_Rate);
				writer.Write((Byte)Attack4_ActionID);
				writer.Write(Attack4_DelayMin);
				writer.Write(Attack4_DelayMax);
				writer.Write(Attack4_ConeAngle);
				writer.Write(Reserve13);
				writer.Write(Reserve_last);
			}
		}
	}
}
