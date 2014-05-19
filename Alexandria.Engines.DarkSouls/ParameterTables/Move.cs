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
		/// Defined as "MOVE_PARAM_ST" in Dark Souls in the file "MoveParam.paramdef" (id 1Ch).
		/// </remarks>
		public class Move : ParameterTableRow {
			public const string TableName = "MOVE_PARAM_ST";

			Int32 stayId, walkF, walkB, walkL, walkR, dashF, dashB, dashL, dashR, superDash, escapeF, escapeB, escapeL, escapeR, turnL, trunR, largeTurnL, largeTurnR, stepMove, flyStay, flyWalkF, flyWalkFL, flyWalkFR, flyWalkFL2, flyWalkFR2, flyDashF, flyDashFL, flyDashFR, flyDashFL2, flyDashFR2, dashEscapeF, dashEscapeB, dashEscapeL, dashEscapeR, analogMoveParamId;
			Byte[] pad;

			public static readonly PropertyInfo
				StayIdProperty = GetProperty<Move>("StayId"),
				WalkFProperty = GetProperty<Move>("WalkF"),
				WalkBProperty = GetProperty<Move>("WalkB"),
				WalkLProperty = GetProperty<Move>("WalkL"),
				WalkRProperty = GetProperty<Move>("WalkR"),
				DashFProperty = GetProperty<Move>("DashF"),
				DashBProperty = GetProperty<Move>("DashB"),
				DashLProperty = GetProperty<Move>("DashL"),
				DashRProperty = GetProperty<Move>("DashR"),
				SuperDashProperty = GetProperty<Move>("SuperDash"),
				EscapeFProperty = GetProperty<Move>("EscapeF"),
				EscapeBProperty = GetProperty<Move>("EscapeB"),
				EscapeLProperty = GetProperty<Move>("EscapeL"),
				EscapeRProperty = GetProperty<Move>("EscapeR"),
				TurnLProperty = GetProperty<Move>("TurnL"),
				TrunRProperty = GetProperty<Move>("TrunR"),
				LargeTurnLProperty = GetProperty<Move>("LargeTurnL"),
				LargeTurnRProperty = GetProperty<Move>("LargeTurnR"),
				StepMoveProperty = GetProperty<Move>("StepMove"),
				FlyStayProperty = GetProperty<Move>("FlyStay"),
				FlyWalkFProperty = GetProperty<Move>("FlyWalkF"),
				FlyWalkFLProperty = GetProperty<Move>("FlyWalkFL"),
				FlyWalkFRProperty = GetProperty<Move>("FlyWalkFR"),
				FlyWalkFL2Property = GetProperty<Move>("FlyWalkFL2"),
				FlyWalkFR2Property = GetProperty<Move>("FlyWalkFR2"),
				FlyDashFProperty = GetProperty<Move>("FlyDashF"),
				FlyDashFLProperty = GetProperty<Move>("FlyDashFL"),
				FlyDashFRProperty = GetProperty<Move>("FlyDashFR"),
				FlyDashFL2Property = GetProperty<Move>("FlyDashFL2"),
				FlyDashFR2Property = GetProperty<Move>("FlyDashFR2"),
				DashEscapeFProperty = GetProperty<Move>("DashEscapeF"),
				DashEscapeBProperty = GetProperty<Move>("DashEscapeB"),
				DashEscapeLProperty = GetProperty<Move>("DashEscapeL"),
				DashEscapeRProperty = GetProperty<Move>("DashEscapeR"),
				AnalogMoveParamIdProperty = GetProperty<Move>("AnalogMoveParamId"),
				PadProperty = GetProperty<Move>("Pad");

			/// <summary>Wait</summary>
			/// <remarks>
			/// Japanese short name: "待機", Google translated: "Wait".
			/// Japanese description: "待機", Google translated: "Wait".
			/// </remarks>
			[ParameterTableRowAttribute("stayId", index: 0, minimum: -1, maximum: 999999, step: 1, order: 100, unknown2: 1)]
			[DisplayName("Wait")]
			[Description("Wait")]
			[DefaultValue((Int32)(-1))]
			public Int32 StayId {
				get { return stayId; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for StayId.");
					SetProperty(ref stayId, ref value, StayIdProperty);
				}
			}

			/// <summary>Walking before</summary>
			/// <remarks>
			/// Japanese short name: "歩行 前", Google translated: "Walking before".
			/// Japanese description: "歩行 前", Google translated: "Walking before".
			/// </remarks>
			[ParameterTableRowAttribute("walkF", index: 1, minimum: -1, maximum: 999999, step: 1, order: 200, unknown2: 1)]
			[DisplayName("Walking before")]
			[Description("Walking before")]
			[DefaultValue((Int32)(-1))]
			public Int32 WalkF {
				get { return walkF; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for WalkF.");
					SetProperty(ref walkF, ref value, WalkFProperty);
				}
			}

			/// <summary>After walking</summary>
			/// <remarks>
			/// Japanese short name: "歩行 後", Google translated: "After walking".
			/// Japanese description: "歩行 後", Google translated: "After walking".
			/// </remarks>
			[ParameterTableRowAttribute("walkB", index: 2, minimum: -1, maximum: 999999, step: 1, order: 300, unknown2: 1)]
			[DisplayName("After walking")]
			[Description("After walking")]
			[DefaultValue((Int32)(-1))]
			public Int32 WalkB {
				get { return walkB; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for WalkB.");
					SetProperty(ref walkB, ref value, WalkBProperty);
				}
			}

			/// <summary>Walking left</summary>
			/// <remarks>
			/// Japanese short name: "歩行 左", Google translated: "Walking left".
			/// Japanese description: "歩行 左", Google translated: "Walking left".
			/// </remarks>
			[ParameterTableRowAttribute("walkL", index: 3, minimum: -1, maximum: 999999, step: 1, order: 400, unknown2: 1)]
			[DisplayName("Walking left")]
			[Description("Walking left")]
			[DefaultValue((Int32)(-1))]
			public Int32 WalkL {
				get { return walkL; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for WalkL.");
					SetProperty(ref walkL, ref value, WalkLProperty);
				}
			}

			/// <summary>Walking right</summary>
			/// <remarks>
			/// Japanese short name: "歩行 右", Google translated: "Walking right".
			/// Japanese description: "歩行 右", Google translated: "Walking right".
			/// </remarks>
			[ParameterTableRowAttribute("walkR", index: 4, minimum: -1, maximum: 999999, step: 1, order: 500, unknown2: 1)]
			[DisplayName("Walking right")]
			[Description("Walking right")]
			[DefaultValue((Int32)(-1))]
			public Int32 WalkR {
				get { return walkR; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for WalkR.");
					SetProperty(ref walkR, ref value, WalkRProperty);
				}
			}

			/// <summary>Before traveling</summary>
			/// <remarks>
			/// Japanese short name: "走行 前", Google translated: "Before traveling".
			/// Japanese description: "走行 前", Google translated: "Before traveling".
			/// </remarks>
			[ParameterTableRowAttribute("dashF", index: 5, minimum: -1, maximum: 999999, step: 1, order: 600, unknown2: 1)]
			[DisplayName("Before traveling")]
			[Description("Before traveling")]
			[DefaultValue((Int32)(-1))]
			public Int32 DashF {
				get { return dashF; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for DashF.");
					SetProperty(ref dashF, ref value, DashFProperty);
				}
			}

			/// <summary>After traveling</summary>
			/// <remarks>
			/// Japanese short name: "走行 後", Google translated: "After traveling".
			/// Japanese description: "走行 後", Google translated: "After traveling".
			/// </remarks>
			[ParameterTableRowAttribute("dashB", index: 6, minimum: -1, maximum: 999999, step: 1, order: 700, unknown2: 1)]
			[DisplayName("After traveling")]
			[Description("After traveling")]
			[DefaultValue((Int32)(-1))]
			public Int32 DashB {
				get { return dashB; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for DashB.");
					SetProperty(ref dashB, ref value, DashBProperty);
				}
			}

			/// <summary>Traveling left</summary>
			/// <remarks>
			/// Japanese short name: "走行 左", Google translated: "Traveling left".
			/// Japanese description: "走行 左", Google translated: "Traveling left".
			/// </remarks>
			[ParameterTableRowAttribute("dashL", index: 7, minimum: -1, maximum: 999999, step: 1, order: 800, unknown2: 1)]
			[DisplayName("Traveling left")]
			[Description("Traveling left")]
			[DefaultValue((Int32)(-1))]
			public Int32 DashL {
				get { return dashL; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for DashL.");
					SetProperty(ref dashL, ref value, DashLProperty);
				}
			}

			/// <summary>Running right</summary>
			/// <remarks>
			/// Japanese short name: "走行 右", Google translated: "Running right".
			/// Japanese description: "走行 右", Google translated: "Running right".
			/// </remarks>
			[ParameterTableRowAttribute("dashR", index: 8, minimum: -1, maximum: 999999, step: 1, order: 900, unknown2: 1)]
			[DisplayName("Running right")]
			[Description("Running right")]
			[DefaultValue((Int32)(-1))]
			public Int32 DashR {
				get { return dashR; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for DashR.");
					SetProperty(ref dashR, ref value, DashRProperty);
				}
			}

			/// <summary>Dash move</summary>
			/// <remarks>
			/// Japanese short name: "ダッシュ移動", Google translated: "Dash move".
			/// Japanese description: "ダッシュ移動", Google translated: "Dash move".
			/// </remarks>
			[ParameterTableRowAttribute("superDash", index: 9, minimum: -1, maximum: 999999, step: 1, order: 1000, unknown2: 1)]
			[DisplayName("Dash move")]
			[Description("Dash move")]
			[DefaultValue((Int32)(-1))]
			public Int32 SuperDash {
				get { return superDash; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for SuperDash.");
					SetProperty(ref superDash, ref value, SuperDashProperty);
				}
			}

			/// <summary>Emergency avoidance before</summary>
			/// <remarks>
			/// Japanese short name: "緊急回避 前", Google translated: "Emergency avoidance before".
			/// Japanese description: "緊急回避 前", Google translated: "Emergency avoidance before".
			/// </remarks>
			[ParameterTableRowAttribute("escapeF", index: 10, minimum: -1, maximum: 999999, step: 1, order: 1100, unknown2: 1)]
			[DisplayName("Emergency avoidance before")]
			[Description("Emergency avoidance before")]
			[DefaultValue((Int32)(-1))]
			public Int32 EscapeF {
				get { return escapeF; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for EscapeF.");
					SetProperty(ref escapeF, ref value, EscapeFProperty);
				}
			}

			/// <summary>After emergency avoidance</summary>
			/// <remarks>
			/// Japanese short name: "緊急回避 後", Google translated: "After emergency avoidance".
			/// Japanese description: "緊急回避 後", Google translated: "After emergency avoidance".
			/// </remarks>
			[ParameterTableRowAttribute("escapeB", index: 11, minimum: -1, maximum: 999999, step: 1, order: 1200, unknown2: 1)]
			[DisplayName("After emergency avoidance")]
			[Description("After emergency avoidance")]
			[DefaultValue((Int32)(-1))]
			public Int32 EscapeB {
				get { return escapeB; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for EscapeB.");
					SetProperty(ref escapeB, ref value, EscapeBProperty);
				}
			}

			/// <summary>Emergency avoidance left</summary>
			/// <remarks>
			/// Japanese short name: "緊急回避 左", Google translated: "Emergency avoidance left".
			/// Japanese description: "緊急回避 左", Google translated: "Emergency avoidance left".
			/// </remarks>
			[ParameterTableRowAttribute("escapeL", index: 12, minimum: -1, maximum: 999999, step: 1, order: 1300, unknown2: 1)]
			[DisplayName("Emergency avoidance left")]
			[Description("Emergency avoidance left")]
			[DefaultValue((Int32)(-1))]
			public Int32 EscapeL {
				get { return escapeL; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for EscapeL.");
					SetProperty(ref escapeL, ref value, EscapeLProperty);
				}
			}

			/// <summary>Emergency avoidance right</summary>
			/// <remarks>
			/// Japanese short name: "緊急回避 右", Google translated: "Emergency avoidance right".
			/// Japanese description: "緊急回避 右", Google translated: "Emergency avoidance right".
			/// </remarks>
			[ParameterTableRowAttribute("escapeR", index: 13, minimum: -1, maximum: 999999, step: 1, order: 1400, unknown2: 1)]
			[DisplayName("Emergency avoidance right")]
			[Description("Emergency avoidance right")]
			[DefaultValue((Int32)(-1))]
			public Int32 EscapeR {
				get { return escapeR; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for EscapeR.");
					SetProperty(ref escapeR, ref value, EscapeRProperty);
				}
			}

			/// <summary>Turn left 90 degrees</summary>
			/// <remarks>
			/// Japanese short name: "90度旋回 左", Google translated: "Turn left 90 degrees".
			/// Japanese description: "90度旋回 左", Google translated: "Turn left 90 degrees".
			/// </remarks>
			[ParameterTableRowAttribute("turnL", index: 14, minimum: -1, maximum: 999999, step: 1, order: 1500, unknown2: 1)]
			[DisplayName("Turn left 90 degrees")]
			[Description("Turn left 90 degrees")]
			[DefaultValue((Int32)(-1))]
			public Int32 TurnL {
				get { return turnL; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for TurnL.");
					SetProperty(ref turnL, ref value, TurnLProperty);
				}
			}

			/// <summary>Turn right 90 degrees</summary>
			/// <remarks>
			/// Japanese short name: "90度旋回 右", Google translated: "Turn right 90 degrees".
			/// Japanese description: "90度旋回 右", Google translated: "Turn right 90 degrees".
			/// </remarks>
			[ParameterTableRowAttribute("trunR", index: 15, minimum: -1, maximum: 999999, step: 1, order: 1600, unknown2: 1)]
			[DisplayName("Turn right 90 degrees")]
			[Description("Turn right 90 degrees")]
			[DefaultValue((Int32)(-1))]
			public Int32 TrunR {
				get { return trunR; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for TrunR.");
					SetProperty(ref trunR, ref value, TrunRProperty);
				}
			}

			/// <summary>Turn left 180 degrees</summary>
			/// <remarks>
			/// Japanese short name: "180度旋回 左", Google translated: "Turn left 180 degrees".
			/// Japanese description: "180度旋回 左", Google translated: "Turn left 180 degrees".
			/// </remarks>
			[ParameterTableRowAttribute("largeTurnL", index: 16, minimum: -1, maximum: 999999, step: 1, order: 1700, unknown2: 1)]
			[DisplayName("Turn left 180 degrees")]
			[Description("Turn left 180 degrees")]
			[DefaultValue((Int32)(-1))]
			public Int32 LargeTurnL {
				get { return largeTurnL; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for LargeTurnL.");
					SetProperty(ref largeTurnL, ref value, LargeTurnLProperty);
				}
			}

			/// <summary>Turn right 180 degrees</summary>
			/// <remarks>
			/// Japanese short name: "180度旋回 右", Google translated: "Turn right 180 degrees".
			/// Japanese description: "180度旋回 右", Google translated: "Turn right 180 degrees".
			/// </remarks>
			[ParameterTableRowAttribute("largeTurnR", index: 17, minimum: -1, maximum: 999999, step: 1, order: 1800, unknown2: 1)]
			[DisplayName("Turn right 180 degrees")]
			[Description("Turn right 180 degrees")]
			[DefaultValue((Int32)(-1))]
			public Int32 LargeTurnR {
				get { return largeTurnR; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for LargeTurnR.");
					SetProperty(ref largeTurnR, ref value, LargeTurnRProperty);
				}
			}

			/// <summary>Step movement</summary>
			/// <remarks>
			/// Japanese short name: "ステップ移動", Google translated: "Step movement".
			/// Japanese description: "180度旋回 右", Google translated: "Turn right 180 degrees".
			/// </remarks>
			[ParameterTableRowAttribute("stepMove", index: 18, minimum: -1, maximum: 999999, step: 1, order: 1900, unknown2: 1)]
			[DisplayName("Step movement")]
			[Description("Turn right 180 degrees")]
			[DefaultValue((Int32)(-1))]
			public Int32 StepMove {
				get { return stepMove; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for StepMove.");
					SetProperty(ref stepMove, ref value, StepMoveProperty);
				}
			}

			/// <summary>Flight waiting</summary>
			/// <remarks>
			/// Japanese short name: "飛行待機", Google translated: "Flight waiting".
			/// Japanese description: "飛行待機", Google translated: "Flight waiting".
			/// </remarks>
			[ParameterTableRowAttribute("flyStay", index: 19, minimum: -1, maximum: 999999, step: 1, order: 2100, unknown2: 1)]
			[DisplayName("Flight waiting")]
			[Description("Flight waiting")]
			[DefaultValue((Int32)(-1))]
			public Int32 FlyStay {
				get { return flyStay; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for FlyStay.");
					SetProperty(ref flyStay, ref value, FlyStayProperty);
				}
			}

			/// <summary>Forward flight</summary>
			/// <remarks>
			/// Japanese short name: "飛行前進", Google translated: "Forward flight".
			/// Japanese description: "飛行前進", Google translated: "Forward flight".
			/// </remarks>
			[ParameterTableRowAttribute("flyWalkF", index: 20, minimum: -1, maximum: 999999, step: 1, order: 2200, unknown2: 1)]
			[DisplayName("Forward flight")]
			[Description("Forward flight")]
			[DefaultValue((Int32)(-1))]
			public Int32 FlyWalkF {
				get { return flyWalkF; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for FlyWalkF.");
					SetProperty(ref flyWalkF, ref value, FlyWalkFProperty);
				}
			}

			/// <summary>Flight left forward</summary>
			/// <remarks>
			/// Japanese short name: "飛行左前進", Google translated: "Flight left forward".
			/// Japanese description: "飛行左前進。低回転", Google translated: "Flight left forward . Low revolution".
			/// </remarks>
			[ParameterTableRowAttribute("flyWalkFL", index: 21, minimum: -1, maximum: 999999, step: 1, order: 2300, unknown2: 1)]
			[DisplayName("Flight left forward")]
			[Description("Flight left forward . Low revolution")]
			[DefaultValue((Int32)(-1))]
			public Int32 FlyWalkFL {
				get { return flyWalkFL; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for FlyWalkFL.");
					SetProperty(ref flyWalkFL, ref value, FlyWalkFLProperty);
				}
			}

			/// <summary>Flight right forward</summary>
			/// <remarks>
			/// Japanese short name: "飛行右前進", Google translated: "Flight right forward".
			/// Japanese description: "飛行右前進。低回転", Google translated: "Flight right forward . Low revolution".
			/// </remarks>
			[ParameterTableRowAttribute("flyWalkFR", index: 22, minimum: -1, maximum: 999999, step: 1, order: 2400, unknown2: 1)]
			[DisplayName("Flight right forward")]
			[Description("Flight right forward . Low revolution")]
			[DefaultValue((Int32)(-1))]
			public Int32 FlyWalkFR {
				get { return flyWalkFR; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for FlyWalkFR.");
					SetProperty(ref flyWalkFR, ref value, FlyWalkFRProperty);
				}
			}

			/// <summary>Flight left the second forward</summary>
			/// <remarks>
			/// Japanese short name: "飛行左前進2", Google translated: "Flight left the second forward".
			/// Japanese description: "飛行左前進2。高回転", Google translated: "Flight left the second forward . High revolution".
			/// </remarks>
			[ParameterTableRowAttribute("flyWalkFL2", index: 23, minimum: -1, maximum: 999999, step: 1, order: 2500, unknown2: 1)]
			[DisplayName("Flight left the second forward")]
			[Description("Flight left the second forward . High revolution")]
			[DefaultValue((Int32)(-1))]
			public Int32 FlyWalkFL2 {
				get { return flyWalkFL2; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for FlyWalkFL2.");
					SetProperty(ref flyWalkFL2, ref value, FlyWalkFL2Property);
				}
			}

			/// <summary>Flight right the second forward</summary>
			/// <remarks>
			/// Japanese short name: "飛行右前進2", Google translated: "Flight right the second forward".
			/// Japanese description: "飛行右前進2。高回転", Google translated: "Flying right forward . High revolution".
			/// </remarks>
			[ParameterTableRowAttribute("flyWalkFR2", index: 24, minimum: -1, maximum: 999999, step: 1, order: 2600, unknown2: 1)]
			[DisplayName("Flight right the second forward")]
			[Description("Flying right forward . High revolution")]
			[DefaultValue((Int32)(-1))]
			public Int32 FlyWalkFR2 {
				get { return flyWalkFR2; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for FlyWalkFR2.");
					SetProperty(ref flyWalkFR2, ref value, FlyWalkFR2Property);
				}
			}

			/// <summary>Fast forward flight</summary>
			/// <remarks>
			/// Japanese short name: "高速飛行前進", Google translated: "Fast forward flight".
			/// Japanese description: "高速飛行前進", Google translated: "Fast forward flight".
			/// </remarks>
			[ParameterTableRowAttribute("flyDashF", index: 25, minimum: -1, maximum: 999999, step: 1, order: 2700, unknown2: 1)]
			[DisplayName("Fast forward flight")]
			[Description("Fast forward flight")]
			[DefaultValue((Int32)(-1))]
			public Int32 FlyDashF {
				get { return flyDashF; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for FlyDashF.");
					SetProperty(ref flyDashF, ref value, FlyDashFProperty);
				}
			}

			/// <summary>High-speed flight left forward</summary>
			/// <remarks>
			/// Japanese short name: "高速飛行左前進", Google translated: "High-speed flight left forward".
			/// Japanese description: "高速飛行左前進。低回転", Google translated: "High-speed flight left forward . Low revolution".
			/// </remarks>
			[ParameterTableRowAttribute("flyDashFL", index: 26, minimum: -1, maximum: 999999, step: 1, order: 2800, unknown2: 1)]
			[DisplayName("High-speed flight left forward")]
			[Description("High-speed flight left forward . Low revolution")]
			[DefaultValue((Int32)(-1))]
			public Int32 FlyDashFL {
				get { return flyDashFL; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for FlyDashFL.");
					SetProperty(ref flyDashFL, ref value, FlyDashFLProperty);
				}
			}

			/// <summary>High-speed flight right forward</summary>
			/// <remarks>
			/// Japanese short name: "高速飛行右前進", Google translated: "High-speed flight right forward".
			/// Japanese description: "高速飛行右前進。低回転", Google translated: "High-speed flight right forward . Low revolution".
			/// </remarks>
			[ParameterTableRowAttribute("flyDashFR", index: 27, minimum: -1, maximum: 999999, step: 1, order: 2900, unknown2: 1)]
			[DisplayName("High-speed flight right forward")]
			[Description("High-speed flight right forward . Low revolution")]
			[DefaultValue((Int32)(-1))]
			public Int32 FlyDashFR {
				get { return flyDashFR; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for FlyDashFR.");
					SetProperty(ref flyDashFR, ref value, FlyDashFRProperty);
				}
			}

			/// <summary>High-speed flight left the second forward</summary>
			/// <remarks>
			/// Japanese short name: "高速飛行左前進2", Google translated: "High-speed flight left the second forward".
			/// Japanese description: "高速飛行左前進2。高回転", Google translated: "High-speed flight left the second forward . High revolution".
			/// </remarks>
			[ParameterTableRowAttribute("flyDashFL2", index: 28, minimum: -1, maximum: 999999, step: 1, order: 3000, unknown2: 1)]
			[DisplayName("High-speed flight left the second forward")]
			[Description("High-speed flight left the second forward . High revolution")]
			[DefaultValue((Int32)(-1))]
			public Int32 FlyDashFL2 {
				get { return flyDashFL2; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for FlyDashFL2.");
					SetProperty(ref flyDashFL2, ref value, FlyDashFL2Property);
				}
			}

			/// <summary>High-speed flight right the second forward</summary>
			/// <remarks>
			/// Japanese short name: "高速飛行右前進2", Google translated: "High-speed flight right the second forward".
			/// Japanese description: "高速飛行右前進2。高回転", Google translated: "2 high-speed flight right forward . High revolution".
			/// </remarks>
			[ParameterTableRowAttribute("flyDashFR2", index: 29, minimum: -1, maximum: 999999, step: 1, order: 3100, unknown2: 1)]
			[DisplayName("High-speed flight right the second forward")]
			[Description("2 high-speed flight right forward . High revolution")]
			[DefaultValue((Int32)(-1))]
			public Int32 FlyDashFR2 {
				get { return flyDashFR2; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for FlyDashFR2.");
					SetProperty(ref flyDashFR2, ref value, FlyDashFR2Property);
				}
			}

			/// <summary>Dash emergency avoidance before</summary>
			/// <remarks>
			/// Japanese short name: "ダッシュ緊急回避前", Google translated: "Dash emergency avoidance before".
			/// Japanese description: "ダッシュ緊急回避前", Google translated: "Dash emergency avoidance before".
			/// </remarks>
			[ParameterTableRowAttribute("dashEscapeF", index: 30, minimum: -1, maximum: 999999, step: 1, order: 3200, unknown2: 1)]
			[DisplayName("Dash emergency avoidance before")]
			[Description("Dash emergency avoidance before")]
			[DefaultValue((Int32)(-1))]
			public Int32 DashEscapeF {
				get { return dashEscapeF; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for DashEscapeF.");
					SetProperty(ref dashEscapeF, ref value, DashEscapeFProperty);
				}
			}

			/// <summary>Dash emergency avoidance after</summary>
			/// <remarks>
			/// Japanese short name: "ダッシュ緊急回避後", Google translated: "Dash emergency avoidance after".
			/// Japanese description: "ダッシュ緊急回避後", Google translated: "Dash emergency avoidance after".
			/// </remarks>
			[ParameterTableRowAttribute("dashEscapeB", index: 31, minimum: -1, maximum: 999999, step: 1, order: 3300, unknown2: 1)]
			[DisplayName("Dash emergency avoidance after")]
			[Description("Dash emergency avoidance after")]
			[DefaultValue((Int32)(-1))]
			public Int32 DashEscapeB {
				get { return dashEscapeB; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for DashEscapeB.");
					SetProperty(ref dashEscapeB, ref value, DashEscapeBProperty);
				}
			}

			/// <summary>Dash emergency avoidance left</summary>
			/// <remarks>
			/// Japanese short name: "ダッシュ緊急回避左", Google translated: "Dash emergency avoidance left".
			/// Japanese description: "ダッシュ緊急回避左", Google translated: "Dash emergency avoidance left".
			/// </remarks>
			[ParameterTableRowAttribute("dashEscapeL", index: 32, minimum: -1, maximum: 999999, step: 1, order: 3400, unknown2: 1)]
			[DisplayName("Dash emergency avoidance left")]
			[Description("Dash emergency avoidance left")]
			[DefaultValue((Int32)(-1))]
			public Int32 DashEscapeL {
				get { return dashEscapeL; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for DashEscapeL.");
					SetProperty(ref dashEscapeL, ref value, DashEscapeLProperty);
				}
			}

			/// <summary>Dash emergency avoidance right</summary>
			/// <remarks>
			/// Japanese short name: "ダッシュ緊急回避右", Google translated: "Dash emergency avoidance right".
			/// Japanese description: "ダッシュ緊急回避右", Google translated: "Dash emergency avoidance right".
			/// </remarks>
			[ParameterTableRowAttribute("dashEscapeR", index: 33, minimum: -1, maximum: 999999, step: 1, order: 3500, unknown2: 1)]
			[DisplayName("Dash emergency avoidance right")]
			[Description("Dash emergency avoidance right")]
			[DefaultValue((Int32)(-1))]
			public Int32 DashEscapeR {
				get { return dashEscapeR; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for DashEscapeR.");
					SetProperty(ref dashEscapeR, ref value, DashEscapeRProperty);
				}
			}

			/// <summary>Analog movement para ID</summary>
			/// <remarks>
			/// Japanese short name: "アナログ移動パラＩＤ", Google translated: "Analog movement para ID".
			/// Japanese description: "移動アニメブレンドで使用される移動アニメパラメータＩＤ", Google translated: "Move animation parameter ID for use in a mobile animation blend".
			/// </remarks>
			[ParameterTableRowAttribute("analogMoveParamId", index: 34, minimum: -1, maximum: 999999, step: 1, order: 3600, unknown2: 1)]
			[DisplayName("Analog movement para ID")]
			[Description("Move animation parameter ID for use in a mobile animation blend")]
			[DefaultValue((Int32)(-1))]
			public Int32 AnalogMoveParamId {
				get { return analogMoveParamId; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for AnalogMoveParamId.");
					SetProperty(ref analogMoveParamId, ref value, AnalogMoveParamIdProperty);
				}
			}

			/// <summary>Padding</summary>
			/// <remarks>
			/// Japanese short name: "パディング", Google translated: "Padding".
			/// Japanese description: "パディング", Google translated: "Padding".
			/// </remarks>
			[ParameterTableRowAttribute("pad[4]", index: 35, minimum: 0, maximum: 0, step: 0, order: 3601, unknown2: 0)]
			[DisplayName("Padding")]
			[Description("Padding")]
			[Browsable(false)]
			public Byte[] Pad {
				get { return pad; }
				set { SetProperty(ref pad, ref value, PadProperty); }
			}

			internal Move(ParameterTable table, int index, AssetLoader loader, int next)
				: base(table, index, loader) {
				BinaryReader reader = loader.Reader;

				StayId = reader.ReadInt32();
				WalkF = reader.ReadInt32();
				WalkB = reader.ReadInt32();
				WalkL = reader.ReadInt32();
				WalkR = reader.ReadInt32();
				DashF = reader.ReadInt32();
				DashB = reader.ReadInt32();
				DashL = reader.ReadInt32();
				DashR = reader.ReadInt32();
				SuperDash = reader.ReadInt32();
				EscapeF = reader.ReadInt32();
				EscapeB = reader.ReadInt32();
				EscapeL = reader.ReadInt32();
				EscapeR = reader.ReadInt32();
				TurnL = reader.ReadInt32();
				TrunR = reader.ReadInt32();
				LargeTurnL = reader.ReadInt32();
				LargeTurnR = reader.ReadInt32();
				StepMove = reader.ReadInt32();
				FlyStay = reader.ReadInt32();
				FlyWalkF = reader.ReadInt32();
				FlyWalkFL = reader.ReadInt32();
				FlyWalkFR = reader.ReadInt32();
				FlyWalkFL2 = reader.ReadInt32();
				FlyWalkFR2 = reader.ReadInt32();
				FlyDashF = reader.ReadInt32();
				FlyDashFL = reader.ReadInt32();
				FlyDashFR = reader.ReadInt32();
				FlyDashFL2 = reader.ReadInt32();
				FlyDashFR2 = reader.ReadInt32();
				DashEscapeF = reader.ReadInt32();
				DashEscapeB = reader.ReadInt32();
				DashEscapeL = reader.ReadInt32();
				DashEscapeR = reader.ReadInt32();
				AnalogMoveParamId = reader.ReadInt32();
				Pad = reader.ReadBytes(4);
			}

			internal Move(ParameterTable table, int index)
				: base(table, index) {
				StayId = (Int32)(-1);
				WalkF = (Int32)(-1);
				WalkB = (Int32)(-1);
				WalkL = (Int32)(-1);
				WalkR = (Int32)(-1);
				DashF = (Int32)(-1);
				DashB = (Int32)(-1);
				DashL = (Int32)(-1);
				DashR = (Int32)(-1);
				SuperDash = (Int32)(-1);
				EscapeF = (Int32)(-1);
				EscapeB = (Int32)(-1);
				EscapeL = (Int32)(-1);
				EscapeR = (Int32)(-1);
				TurnL = (Int32)(-1);
				TrunR = (Int32)(-1);
				LargeTurnL = (Int32)(-1);
				LargeTurnR = (Int32)(-1);
				StepMove = (Int32)(-1);
				FlyStay = (Int32)(-1);
				FlyWalkF = (Int32)(-1);
				FlyWalkFL = (Int32)(-1);
				FlyWalkFR = (Int32)(-1);
				FlyWalkFL2 = (Int32)(-1);
				FlyWalkFR2 = (Int32)(-1);
				FlyDashF = (Int32)(-1);
				FlyDashFL = (Int32)(-1);
				FlyDashFR = (Int32)(-1);
				FlyDashFL2 = (Int32)(-1);
				FlyDashFR2 = (Int32)(-1);
				DashEscapeF = (Int32)(-1);
				DashEscapeB = (Int32)(-1);
				DashEscapeL = (Int32)(-1);
				DashEscapeR = (Int32)(-1);
				AnalogMoveParamId = (Int32)(-1);
				Pad = new Byte[4];
			}

			public override void Write(BinaryWriter writer) {
				writer.Write(StayId);
				writer.Write(WalkF);
				writer.Write(WalkB);
				writer.Write(WalkL);
				writer.Write(WalkR);
				writer.Write(DashF);
				writer.Write(DashB);
				writer.Write(DashL);
				writer.Write(DashR);
				writer.Write(SuperDash);
				writer.Write(EscapeF);
				writer.Write(EscapeB);
				writer.Write(EscapeL);
				writer.Write(EscapeR);
				writer.Write(TurnL);
				writer.Write(TrunR);
				writer.Write(LargeTurnL);
				writer.Write(LargeTurnR);
				writer.Write(StepMove);
				writer.Write(FlyStay);
				writer.Write(FlyWalkF);
				writer.Write(FlyWalkFL);
				writer.Write(FlyWalkFR);
				writer.Write(FlyWalkFL2);
				writer.Write(FlyWalkFR2);
				writer.Write(FlyDashF);
				writer.Write(FlyDashFL);
				writer.Write(FlyDashFR);
				writer.Write(FlyDashFL2);
				writer.Write(FlyDashFR2);
				writer.Write(DashEscapeF);
				writer.Write(DashEscapeB);
				writer.Write(DashEscapeL);
				writer.Write(DashEscapeR);
				writer.Write(AnalogMoveParamId);
				writer.Write(Pad);
			}
		}
	}
}
