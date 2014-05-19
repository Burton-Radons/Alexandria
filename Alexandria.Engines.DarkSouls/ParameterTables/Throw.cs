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
		/// Defined as "THROW_INFO_BANK" in Dark Souls in the file "ThrowParam.paramdef" (id 15h).
		/// </remarks>
		public class Throw : ParameterTableRow {
			public const string TableName = "THROW_INFO_BANK";

			Int32 atkChrId, defChrId, throwTypeId, atkAnimId, defAnimId;
			Single dist, diffAngMin, diffAngMax, upperYRange, lowerYRange, diffAngMyToDef;
			UInt16 escHp, selfEscCycleTime, sphereCastRadiusRateTop, sphereCastRadiusRateLow;
			ThrowPadType padType;
			ThrowEnableState atkEnableState;
			Byte atkSorbDmyId, defSorbDmyId, selfEscCycleCnt;
			ThrowType throwType;
			ThrowDmyCharacterDirectionType dmyHasChrDirType;
			Byte[] pad1;

			public static readonly PropertyInfo
				AtkChrIdProperty = GetProperty<Throw>("AtkChrId"),
				DefChrIdProperty = GetProperty<Throw>("DefChrId"),
				DistProperty = GetProperty<Throw>("Dist"),
				DiffAngMinProperty = GetProperty<Throw>("DiffAngMin"),
				DiffAngMaxProperty = GetProperty<Throw>("DiffAngMax"),
				UpperYRangeProperty = GetProperty<Throw>("UpperYRange"),
				LowerYRangeProperty = GetProperty<Throw>("LowerYRange"),
				DiffAngMyToDefProperty = GetProperty<Throw>("DiffAngMyToDef"),
				ThrowTypeIdProperty = GetProperty<Throw>("ThrowTypeId"),
				AtkAnimIdProperty = GetProperty<Throw>("AtkAnimId"),
				DefAnimIdProperty = GetProperty<Throw>("DefAnimId"),
				EscHpProperty = GetProperty<Throw>("EscHp"),
				SelfEscCycleTimeProperty = GetProperty<Throw>("SelfEscCycleTime"),
				SphereCastRadiusRateTopProperty = GetProperty<Throw>("SphereCastRadiusRateTop"),
				SphereCastRadiusRateLowProperty = GetProperty<Throw>("SphereCastRadiusRateLow"),
				PadTypeProperty = GetProperty<Throw>("PadType"),
				AtkEnableStateProperty = GetProperty<Throw>("AtkEnableState"),
				AtkSorbDmyIdProperty = GetProperty<Throw>("AtkSorbDmyId"),
				DefSorbDmyIdProperty = GetProperty<Throw>("DefSorbDmyId"),
				ThrowTypeProperty = GetProperty<Throw>("ThrowType"),
				SelfEscCycleCntProperty = GetProperty<Throw>("SelfEscCycleCnt"),
				DmyHasChrDirTypeProperty = GetProperty<Throw>("DmyHasChrDirType"),
				IsTurnAtkerProperty = GetProperty<Throw>("IsTurnAtker"),
				IsSkipWepCateProperty = GetProperty<Throw>("IsSkipWepCate"),
				IsSkipSphereCastProperty = GetProperty<Throw>("IsSkipSphereCast"),
				Pad0Property = GetProperty<Throw>("Pad0"),
				Pad1Property = GetProperty<Throw>("Pad1");

			/// <summary>Character ID side throw</summary>
			/// <remarks>
			/// Japanese short name: "投げ側キャラID", Google translated: "Character ID side throw".
			/// Japanese description: "投げ側キャラID", Google translated: "Character ID side throw".
			/// </remarks>
			[ParameterTableRowAttribute("AtkChrId", index: 0, minimum: 0, maximum: 10000, step: 1, order: 100, unknown2: 1)]
			[DisplayName("Character ID side throw")]
			[Description("Character ID side throw")]
			[DefaultValue((Int32)0)]
			public Int32 AtkChrId {
				get { return atkChrId; }
				set {
					if ((double)value < 0 || (double)value > 10000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 10000 for " + AtkChrIdProperty.Name + ".");
					SetProperty(ref atkChrId, ref value, AtkChrIdProperty);
				}
			}

			/// <summary>Character ID receiving side</summary>
			/// <remarks>
			/// Japanese short name: "受け側キャラID", Google translated: "Character ID receiving side".
			/// Japanese description: "受け側キャラID", Google translated: "Character ID receiving side".
			/// </remarks>
			[ParameterTableRowAttribute("DefChrId", index: 1, minimum: 0, maximum: 10000, step: 1, order: 200, unknown2: 0)]
			[DisplayName("Character ID receiving side")]
			[Description("Character ID receiving side")]
			[DefaultValue((Int32)0)]
			public Int32 DefChrId {
				get { return defChrId; }
				set {
					if ((double)value < 0 || (double)value > 10000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 10000 for " + DefChrIdProperty.Name + ".");
					SetProperty(ref defChrId, ref value, DefChrIdProperty);
				}
			}

			/// <summary>Effective distance [m]</summary>
			/// <remarks>
			/// Japanese short name: "有効距離[m]", Google translated: "Effective distance [m]".
			/// Japanese description: "この値より近い距離じゃないと投げない[m]", Google translated: "You do not throw 's not a distance smaller than this value [m]".
			/// </remarks>
			[ParameterTableRowAttribute("Dist", index: 2, minimum: 0, maximum: 10000, step: 0.1, order: 800, unknown2: 0)]
			[DisplayName("Effective distance [m]")]
			[Description("You do not throw 's not a distance smaller than this value [m]")]
			[DefaultValue((Single)0)]
			public Single Dist {
				get { return dist; }
				set {
					if ((double)value < 0 || (double)value > 10000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 10000 for " + DistProperty.Name + ".");
					SetProperty(ref dist, ref value, DistProperty);
				}
			}

			/// <summary>Angular difference range min in the direction of the other party to the direction of their own</summary>
			/// <remarks>
			/// Japanese short name: "自分の向きと相手の向きの角度差範囲min", Google translated: "Angular difference range min in the direction of the other party to the direction of their own".
			/// Japanese description: "投げ側と受け側の角度差(Y軸)がこの角度より大きくないと投げない", Google translated: "Not throw angle difference between the receiving -side throw (Y -axis ) is not greater than the angle".
			/// </remarks>
			[ParameterTableRowAttribute("DiffAngMin", index: 3, minimum: 0, maximum: 180, step: 0.01, order: 1100, unknown2: 0)]
			[DisplayName("Angular difference range min in the direction of the other party to the direction of their own")]
			[Description("Not throw angle difference between the receiving -side throw (Y -axis ) is not greater than the angle")]
			[DefaultValue((Single)0)]
			public Single DiffAngMin {
				get { return diffAngMin; }
				set {
					if ((double)value < 0 || (double)value > 180)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 180 for " + DiffAngMinProperty.Name + ".");
					SetProperty(ref diffAngMin, ref value, DiffAngMinProperty);
				}
			}

			/// <summary>Angular difference max range of the orientation of the other party to the direction of their own</summary>
			/// <remarks>
			/// Japanese short name: "自分の向きと相手の向きの角度差範囲max", Google translated: "Angular difference max range of the orientation of the other party to the direction of their own".
			/// Japanese description: "投げ側と受け側の角度差(Y軸)がこの角度より小さくないと投げない", Google translated: "Not throw angle difference between the receiving -side throw (Y -axis ) is not smaller than the angle".
			/// </remarks>
			[ParameterTableRowAttribute("DiffAngMax", index: 4, minimum: 0, maximum: 180, step: 0.01, order: 1200, unknown2: 0)]
			[DisplayName("Angular difference max range of the orientation of the other party to the direction of their own")]
			[Description("Not throw angle difference between the receiving -side throw (Y -axis ) is not smaller than the angle")]
			[DefaultValue((Single)0)]
			public Single DiffAngMax {
				get { return diffAngMax; }
				set {
					if ((double)value < 0 || (double)value > 180)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 180 for " + DiffAngMaxProperty.Name + ".");
					SetProperty(ref diffAngMax, ref value, DiffAngMaxProperty);
				}
			}

			/// <summary>Range on height [m]</summary>
			/// <remarks>
			/// Japanese short name: "高さ範囲上[m]", Google translated: "Range on height [m]".
			/// Japanese description: "投げ側から受け側のY軸の相対距離がこの値より小さくないと投げない", Google translated: "Do not throw the relative distance of the Y axis on the receiving side from the side throwing is not smaller than the value".
			/// </remarks>
			[ParameterTableRowAttribute("upperYRange", index: 5, minimum: 0, maximum: 10000, step: 0.01, order: 900, unknown2: 0)]
			[DisplayName("Range on height [m]")]
			[Description("Do not throw the relative distance of the Y axis on the receiving side from the side throwing is not smaller than the value")]
			[DefaultValue((Single)0.2)]
			public Single UpperYRange {
				get { return upperYRange; }
				set {
					if ((double)value < 0 || (double)value > 10000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 10000 for " + UpperYRangeProperty.Name + ".");
					SetProperty(ref upperYRange, ref value, UpperYRangeProperty);
				}
			}

			/// <summary>Range under height [m]</summary>
			/// <remarks>
			/// Japanese short name: "高さ範囲下[m]", Google translated: "Range under height [m]".
			/// Japanese description: "投げ側から受け側のY軸の相対距離がこの値より小さくないと投げない", Google translated: "Do not throw the relative distance of the Y axis on the receiving side from the side throwing is not smaller than the value".
			/// </remarks>
			[ParameterTableRowAttribute("lowerYRange", index: 6, minimum: 0, maximum: 10000, step: 0.01, order: 1000, unknown2: 0)]
			[DisplayName("Range under height [m]")]
			[Description("Do not throw the relative distance of the Y axis on the receiving side from the side throwing is not smaller than the value")]
			[DefaultValue((Single)0.2)]
			public Single LowerYRange {
				get { return lowerYRange; }
				set {
					if ((double)value < 0 || (double)value > 10000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 10000 for " + LowerYRangeProperty.Name + ".");
					SetProperty(ref lowerYRange, ref value, LowerYRangeProperty);
				}
			}

			/// <summary>Angular difference of direction to the other from their orientation and their</summary>
			/// <remarks>
			/// Japanese short name: "自分の向きと自分から相手への方向の角度差", Google translated: "Angular difference of direction to the other from their orientation and their".
			/// Japanese description: "自分の正面のベクトルと、自分から相手への方向のベクトルの角度差。この値より大きいと投げない", Google translated: "The vector of the front of their angular difference vector in the direction of to the other from their own . You do not throw to be greater than this value".
			/// </remarks>
			[ParameterTableRowAttribute("diffAngMyToDef", index: 7, minimum: 0, maximum: 180, step: 0.1, order: 1300, unknown2: 0)]
			[DisplayName("Angular difference of direction to the other from their orientation and their")]
			[Description("The vector of the front of their angular difference vector in the direction of to the other from their own . You do not throw to be greater than this value")]
			[DefaultValue((Single)60)]
			public Single DiffAngMyToDef {
				get { return diffAngMyToDef; }
				set {
					if ((double)value < 0 || (double)value > 180)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 180 for " + DiffAngMyToDefProperty.Name + ".");
					SetProperty(ref diffAngMyToDef, ref value, DiffAngMyToDefProperty);
				}
			}

			/// <summary>Type ID throw</summary>
			/// <remarks>
			/// Japanese short name: "投げタイプID", Google translated: "Type ID throw".
			/// Japanese description: "投げの種類を特定するID(攻撃パラメタと紐付け)", Google translated: "( Linking the attack parameters ) ID for identifying the type of throw".
			/// </remarks>
			[ParameterTableRowAttribute("throwTypeId", index: 8, minimum: 0, maximum: 1E+08, step: 1, order: 600, unknown2: 0)]
			[DisplayName("Type ID throw")]
			[Description("( Linking the attack parameters ) ID for identifying the type of throw")]
			[DefaultValue((Int32)0)]
			public Int32 ThrowTypeId {
				get { return throwTypeId; }
				set {
					if ((double)value < 0 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1E+08 for " + ThrowTypeIdProperty.Name + ".");
					SetProperty(ref throwTypeId, ref value, ThrowTypeIdProperty);
				}
			}

			/// <summary>Anime ID side throw</summary>
			/// <remarks>
			/// Japanese short name: "投げ側アニメID", Google translated: "Anime ID side throw".
			/// Japanese description: "攻撃アニメIDを設定(EzStateと紐付け)", Google translated: "( With string and EzState) set the attack animation ID".
			/// </remarks>
			[ParameterTableRowAttribute("atkAnimId", index: 9, minimum: 0, maximum: 1E+08, step: 1, order: 300, unknown2: 0)]
			[DisplayName("Anime ID side throw")]
			[Description("( With string and EzState) set the attack animation ID")]
			[DefaultValue((Int32)0)]
			public Int32 AtkAnimId {
				get { return atkAnimId; }
				set {
					if ((double)value < 0 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1E+08 for " + AtkAnimIdProperty.Name + ".");
					SetProperty(ref atkAnimId, ref value, AtkAnimIdProperty);
				}
			}

			/// <summary>Anime ID receiving side</summary>
			/// <remarks>
			/// Japanese short name: "受け側アニメID", Google translated: "Anime ID receiving side".
			/// Japanese description: "ダメージアニメIDを設定(EzStateと紐付け)", Google translated: "( With string and EzState) set the damage animation ID".
			/// </remarks>
			[ParameterTableRowAttribute("defAnimId", index: 10, minimum: 0, maximum: 1E+08, step: 1, order: 400, unknown2: 0)]
			[DisplayName("Anime ID receiving side")]
			[Description("( With string and EzState) set the damage animation ID")]
			[DefaultValue((Int32)0)]
			public Int32 DefAnimId {
				get { return defAnimId; }
				set {
					if ((double)value < 0 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1E+08 for " + DefAnimIdProperty.Name + ".");
					SetProperty(ref defAnimId, ref value, DefAnimIdProperty);
				}
			}

			/// <summary>HP missing throw</summary>
			/// <remarks>
			/// Japanese short name: "投げ抜けHP", Google translated: "HP missing throw".
			/// Japanese description: "投げ抜けに耐えられる値", Google translated: "Value to be bear to throw missing".
			/// </remarks>
			[ParameterTableRowAttribute("escHp", index: 11, minimum: 0, maximum: 9999, step: 1, order: 1800, unknown2: 0)]
			[DisplayName("HP missing throw")]
			[Description("Value to be bear to throw missing")]
			[DefaultValue((UInt16)0)]
			public UInt16 EscHp {
				get { return escHp; }
				set {
					if ((double)value < 0 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9999 for " + EscHpProperty.Name + ".");
					SetProperty(ref escHp, ref value, EscHpProperty);
				}
			}

			/// <summary>Missing throw their own cycle time [ms]</summary>
			/// <remarks>
			/// Japanese short name: "自力投げ抜けサイクル時間[ms]", Google translated: "Missing throw their own cycle time [ms]".
			/// Japanese description: "自力投げ抜けのサイクル時間[ms]", Google translated: "Cycle time of missing throw themselves [ms]".
			/// </remarks>
			[ParameterTableRowAttribute("selfEscCycleTime", index: 12, minimum: 0, maximum: 65535, step: 1, order: 1900, unknown2: 0)]
			[DisplayName("Missing throw their own cycle time [ms]")]
			[Description("Cycle time of missing throw themselves [ms]")]
			[DefaultValue((UInt16)0)]
			public UInt16 SelfEscCycleTime {
				get { return selfEscCycleTime; }
				set {
					if ((double)value < 0 || (double)value > 65535)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 65535 for " + SelfEscCycleTimeProperty.Name + ".");
					SetProperty(ref selfEscCycleTime, ref value, SelfEscCycleTimeProperty);
				}
			}

			/// <summary>Sphere cast radius ratio above _ [1/100Rate]</summary>
			/// <remarks>
			/// Japanese short name: "スフィアキャスト半径比率_上[1/100Rate]", Google translated: "Sphere cast radius ratio above _ [1/100Rate]".
			/// Japanese description: "スフィアキャストの上側半径の比率[80->0.8]", Google translated: "Ratio of the radius of the sphere upper cast [80 - > 0.8 ]".
			/// </remarks>
			[ParameterTableRowAttribute("sphereCastRadiusRateTop", index: 13, minimum: 0, maximum: 999, step: 1, order: 2500, unknown2: 1)]
			[DisplayName("Sphere cast radius ratio above _ [1/100Rate]")]
			[Description("Ratio of the radius of the sphere upper cast [80 - > 0.8 ]")]
			[DefaultValue((UInt16)80)]
			public UInt16 SphereCastRadiusRateTop {
				get { return sphereCastRadiusRateTop; }
				set {
					if ((double)value < 0 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for " + SphereCastRadiusRateTopProperty.Name + ".");
					SetProperty(ref sphereCastRadiusRateTop, ref value, SphereCastRadiusRateTopProperty);
				}
			}

			/// <summary>Sphere cast radius ratio _ under [1/100Rate]</summary>
			/// <remarks>
			/// Japanese short name: "スフィアキャスト半径比率_下[1/100Rate]", Google translated: "Sphere cast radius ratio _ under [1/100Rate]".
			/// Japanese description: "スフィアキャストの下側半径の比率[80->0.8]", Google translated: "The ratio of the lower radius of the sphere cast [80 - > 0.8 ]".
			/// </remarks>
			[ParameterTableRowAttribute("sphereCastRadiusRateLow", index: 14, minimum: 0, maximum: 999, step: 1, order: 2600, unknown2: 1)]
			[DisplayName("Sphere cast radius ratio _ under [1/100Rate]")]
			[Description("The ratio of the lower radius of the sphere cast [80 - > 0.8 ]")]
			[DefaultValue((UInt16)80)]
			public UInt16 SphereCastRadiusRateLow {
				get { return sphereCastRadiusRateLow; }
				set {
					if ((double)value < 0 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for " + SphereCastRadiusRateLowProperty.Name + ".");
					SetProperty(ref sphereCastRadiusRateLow, ref value, SphereCastRadiusRateLowProperty);
				}
			}

			/// <summary>Operation Type</summary>
			/// <remarks>
			/// Japanese short name: "操作タイプ", Google translated: "Operation Type".
			/// Japanese description: "操作タイプ", Google translated: "Operation Type".
			/// </remarks>
			[ParameterTableRowAttribute("PadType", index: 15, minimum: 0, maximum: 10, step: 1, order: 2100, unknown2: 0)]
			[DisplayName("Operation Type")]
			[Description("Operation Type")]
			[DefaultValue((ThrowPadType)1)]
			public ThrowPadType PadType {
				get { return padType; }
				set { SetProperty(ref padType, ref value, PadTypeProperty); }
			}

			/// <summary>The type of state can throw side throw</summary>
			/// <remarks>
			/// Japanese short name: "投げ側の投げ可能状態タイプ", Google translated: "The type of state can throw side throw".
			/// Japanese description: "投げ側の投げが可能な状態タイプを設定してください", Google translated: "Throw on the side of tossing Please set the possible state type".
			/// </remarks>
			[ParameterTableRowAttribute("AtkEnableState", index: 16, minimum: 0, maximum: 255, step: 1, order: 700, unknown2: 0)]
			[DisplayName("The type of state can throw side throw")]
			[Description("Throw on the side of tossing Please set the possible state type")]
			[DefaultValue((ThrowEnableState)0)]
			public ThrowEnableState AtkEnableState {
				get { return atkEnableState; }
				set { SetProperty(ref atkEnableState, ref value, AtkEnableStateProperty); }
			}

			/// <summary>Damipori ID adsorption side throw</summary>
			/// <remarks>
			/// Japanese short name: "投げ側 吸着ダミポリID", Google translated: "Damipori ID adsorption side throw".
			/// Japanese description: "投げ側のどこに受け側を吸着させるか？", Google translated: "Where in the side throw or adsorb the receiving side ?".
			/// </remarks>
			[ParameterTableRowAttribute("atkSorbDmyId", index: 17, minimum: 0, maximum: 255, step: 1, order: 1400, unknown2: 0)]
			[DisplayName("Damipori ID adsorption side throw")]
			[Description("Where in the side throw or adsorb the receiving side ?")]
			[DefaultValue((Byte)0)]
			public Byte AtkSorbDmyId {
				get { return atkSorbDmyId; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for " + AtkSorbDmyIdProperty.Name + ".");
					SetProperty(ref atkSorbDmyId, ref value, AtkSorbDmyIdProperty);
				}
			}

			/// <summary>Damipori ID adsorption side received</summary>
			/// <remarks>
			/// Japanese short name: "受け側 吸着ダミポリID", Google translated: "Damipori ID adsorption side received".
			/// Japanese description: "受け側のどこに投げ側を吸着させるか？", Google translated: "Where on the receiving side or adsorb the side throw ?".
			/// </remarks>
			[ParameterTableRowAttribute("defSorbDmyId", index: 18, minimum: 0, maximum: 255, step: 1, order: 1500, unknown2: 0)]
			[DisplayName("Damipori ID adsorption side received")]
			[Description("Where on the receiving side or adsorb the side throw ?")]
			[DefaultValue((Byte)0)]
			public Byte DefSorbDmyId {
				get { return defSorbDmyId; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for " + DefSorbDmyIdProperty.Name + ".");
					SetProperty(ref defSorbDmyId, ref value, DefSorbDmyIdProperty);
				}
			}

			/// <summary>Type throw</summary>
			/// <remarks>
			/// Japanese short name: "投げ種別", Google translated: "Type throw".
			/// Japanese description: "投げの種別", Google translated: "Type of throw".
			/// </remarks>
			[ParameterTableRowAttribute("throwType", index: 19, minimum: 0, maximum: 255, step: 1, order: 500, unknown2: 0)]
			[DisplayName("Type throw")]
			[Description("Type of throw")]
			[DefaultValue((ThrowType)0)]
			public ThrowType ThrowType {
				get { return throwType; }
				set { SetProperty(ref throwType, ref value, ThrowTypeProperty); }
			}

			/// <summary>Number of cycles missing throw themselves</summary>
			/// <remarks>
			/// Japanese short name: "自力投げ抜けサイクル回数", Google translated: "Number of cycles missing throw themselves".
			/// Japanese description: "自力投げ抜けのサイクル回数", Google translated: "Number of cycles of missing throw themselves".
			/// </remarks>
			[ParameterTableRowAttribute("selfEscCycleCnt", index: 20, minimum: 0, maximum: 255, step: 1, order: 2000, unknown2: 0)]
			[DisplayName("Number of cycles missing throw themselves")]
			[Description("Number of cycles of missing throw themselves")]
			[DefaultValue((Byte)0)]
			public Byte SelfEscCycleCnt {
				get { return selfEscCycleCnt; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for " + SelfEscCycleCntProperty.Name + ".");
					SetProperty(ref selfEscCycleCnt, ref value, SelfEscCycleCntProperty);
				}
			}

			/// <summary>Orientation of the character Damipori possession at the time of occurrence throw</summary>
			/// <remarks>
			/// Japanese short name: "投げ発生時のダミポリ所持キャラの向き", Google translated: "Orientation of the character Damipori possession at the time of occurrence throw".
			/// Japanese description: "投げ発生時のダミポリ所持キャラの向き", Google translated: "Orientation of the character Damipori possession at the time of occurrence throw".
			/// </remarks>
			[ParameterTableRowAttribute("dmyHasChrDirType", index: 21, minimum: 0, maximum: 255, step: 1, order: 1600, unknown2: 0)]
			[DisplayName("Orientation of the character Damipori possession at the time of occurrence throw")]
			[Description("Orientation of the character Damipori possession at the time of occurrence throw")]
			[DefaultValue((ThrowDmyCharacterDirectionType)0)]
			public ThrowDmyCharacterDirectionType DmyHasChrDirType {
				get { return dmyHasChrDirType; }
				set { SetProperty(ref dmyHasChrDirType, ref value, DmyHasChrDirTypeProperty); }
			}

			/// <summary>Side tossing or turning ?</summary>
			/// <remarks>
			/// Japanese short name: "投げ側が旋回するか？", Google translated: "Side tossing or turning ?".
			/// Japanese description: "投げ側が旋回するか？", Google translated: "Side tossing or turning ?".
			/// </remarks>
			[ParameterTableRowAttribute("isTurnAtker:1", index: 22, minimum: 0, maximum: 1, step: 1, order: 1700, unknown2: 0)]
			[DisplayName("Side tossing or turning ?")]
			[Description("Side tossing or turning ?")]
			[DefaultValue(false)]
			public Boolean IsTurnAtker {
				get { return GetBitProperty(0, 1, IsTurnAtkerProperty) != 0; }
				set { SetBitProperty(0, 1, value ? 1 : 0, IsTurnAtkerProperty); }
			}

			/// <summary>You can skip the weapon category check ?</summary>
			/// <remarks>
			/// Japanese short name: "武器カテゴリチェックをスキップするか？", Google translated: "You can skip the weapon category check ?".
			/// Japanese description: "攻撃側の武器カテゴリチェックをスキップするか？", Google translated: "You can skip the weapon category check the attacking ?".
			/// </remarks>
			[ParameterTableRowAttribute("isSkipWepCate:1", index: 23, minimum: 0, maximum: 1, step: 1, order: 2300, unknown2: 0)]
			[DisplayName("You can skip the weapon category check ?")]
			[Description("You can skip the weapon category check the attacking ?")]
			[DefaultValue(false)]
			public Boolean IsSkipWepCate {
				get { return GetBitProperty(1, 1, IsSkipWepCateProperty) != 0; }
				set { SetBitProperty(1, 1, value ? 1 : 0, IsSkipWepCateProperty); }
			}

			/// <summary>You can skip the sphere cast ?</summary>
			/// <remarks>
			/// Japanese short name: "スフィアキャストをスキップするか？", Google translated: "You can skip the sphere cast ?".
			/// Japanese description: "スフィアキャストをスキップするか？", Google translated: "You can skip the sphere cast ?".
			/// </remarks>
			[ParameterTableRowAttribute("isSkipSphereCast:1", index: 24, minimum: 0, maximum: 1, step: 1, order: 2400, unknown2: 0)]
			[DisplayName("You can skip the sphere cast ?")]
			[Description("You can skip the sphere cast ?")]
			[DefaultValue(false)]
			public Boolean IsSkipSphereCast {
				get { return GetBitProperty(2, 1, IsSkipSphereCastProperty) != 0; }
				set { SetBitProperty(2, 1, value ? 1 : 0, IsSkipSphereCastProperty); }
			}

			/// <summary>pad</summary>
			/// <remarks>
			/// Japanese short name: "pad", Google translated: "pad".
			/// Japanese description: "pad", Google translated: "pad".
			/// </remarks>
			[ParameterTableRowAttribute("pad0:5", index: 25, minimum: 0, maximum: 0, step: 0, order: 99999998, unknown2: 0)]
			[DisplayName("pad")]
			[Description("pad")]
			[DefaultValue((Byte)0)]
			[Browsable(false)]
			public Byte Pad0 {
				get { return (Byte)GetBitProperty(3, 5, Pad0Property); }
				set {
					if ((double)value < 0 || (double)value > 0)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 0 for " + Pad0Property.Name + ".");
					SetBitProperty(3, 5, (int)value, Pad0Property);
				}
			}

			/// <summary>pad</summary>
			/// <remarks>
			/// Japanese short name: "pad", Google translated: "pad".
			/// Japanese description: "pad", Google translated: "pad".
			/// </remarks>
			[ParameterTableRowAttribute("pad1[4]", index: 26, minimum: 0, maximum: 0, step: 0, order: 99999999, unknown2: 0)]
			[DisplayName("pad")]
			[Description("pad")]
			[Browsable(false)]
			public Byte[] Pad1 {
				get { return pad1; }
				set { SetProperty(ref pad1, ref value, Pad1Property); }
			}

			internal Throw(ParameterTable table, int index, AssetLoader loader, int next)
				: base(table, index, loader) {
				BinaryReader reader = loader.Reader;

				AtkChrId = reader.ReadInt32();
				DefChrId = reader.ReadInt32();
				Dist = reader.ReadSingle();
				DiffAngMin = reader.ReadSingle();
				DiffAngMax = reader.ReadSingle();
				UpperYRange = reader.ReadSingle();
				LowerYRange = reader.ReadSingle();
				DiffAngMyToDef = reader.ReadSingle();
				ThrowTypeId = reader.ReadInt32();
				AtkAnimId = reader.ReadInt32();
				DefAnimId = reader.ReadInt32();
				EscHp = reader.ReadUInt16();
				SelfEscCycleTime = reader.ReadUInt16();
				SphereCastRadiusRateTop = reader.ReadUInt16();
				SphereCastRadiusRateLow = reader.ReadUInt16();
				PadType = (ThrowPadType)reader.ReadByte();
				AtkEnableState = (ThrowEnableState)reader.ReadByte();
				AtkSorbDmyId = reader.ReadByte();
				DefSorbDmyId = reader.ReadByte();
				ThrowType = (ThrowType)reader.ReadByte();
				SelfEscCycleCnt = reader.ReadByte();
				DmyHasChrDirType = (ThrowDmyCharacterDirectionType)reader.ReadByte();
				BitFields = reader.ReadBytes(1);
				Pad1 = reader.ReadBytes(4);
			}

			internal Throw(ParameterTable table, int index)
				: base(table, index) {
				BitFields = new byte[1];
				AtkChrId = (Int32)0;
				DefChrId = (Int32)0;
				Dist = (Single)0;
				DiffAngMin = (Single)0;
				DiffAngMax = (Single)0;
				UpperYRange = (Single)0.2;
				LowerYRange = (Single)0.2;
				DiffAngMyToDef = (Single)60;
				ThrowTypeId = (Int32)0;
				AtkAnimId = (Int32)0;
				DefAnimId = (Int32)0;
				EscHp = (UInt16)0;
				SelfEscCycleTime = (UInt16)0;
				SphereCastRadiusRateTop = (UInt16)80;
				SphereCastRadiusRateLow = (UInt16)80;
				PadType = (ThrowPadType)1;
				AtkEnableState = (ThrowEnableState)0;
				AtkSorbDmyId = (Byte)0;
				DefSorbDmyId = (Byte)0;
				ThrowType = (ThrowType)0;
				SelfEscCycleCnt = (Byte)0;
				DmyHasChrDirType = (ThrowDmyCharacterDirectionType)0;
				IsTurnAtker = false;
				IsSkipWepCate = false;
				IsSkipSphereCast = false;
				Pad0 = (Byte)0;
				Pad1 = new Byte[4];
			}

			public override void Write(BinaryWriter writer) {
				writer.Write(AtkChrId);
				writer.Write(DefChrId);
				writer.Write(Dist);
				writer.Write(DiffAngMin);
				writer.Write(DiffAngMax);
				writer.Write(UpperYRange);
				writer.Write(LowerYRange);
				writer.Write(DiffAngMyToDef);
				writer.Write(ThrowTypeId);
				writer.Write(AtkAnimId);
				writer.Write(DefAnimId);
				writer.Write(EscHp);
				writer.Write(SelfEscCycleTime);
				writer.Write(SphereCastRadiusRateTop);
				writer.Write(SphereCastRadiusRateLow);
				writer.Write((Byte)PadType);
				writer.Write((Byte)AtkEnableState);
				writer.Write(AtkSorbDmyId);
				writer.Write(DefSorbDmyId);
				writer.Write((Byte)ThrowType);
				writer.Write(SelfEscCycleCnt);
				writer.Write((Byte)DmyHasChrDirType);
				writer.Write(BitFields);
				writer.Write(Pad1);
			}
		}
	}
}
