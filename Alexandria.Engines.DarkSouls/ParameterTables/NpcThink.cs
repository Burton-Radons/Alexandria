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
		/// Defined as "NPC_THINK_PARAM_ST" in Dark Souls in the file "NpcThinkParam.paramdef" (id 1Ah).
		/// </remarks>
		public class NpcThink : ParameterTableRow {
			/// <summary>The name of the table in the file.</summary>
			public const string TableName = "NPC_THINK_PARAM_ST";

			Int32 logicId, battleGoalID, goalID_ToCaution, idAttackCannotMove, goalID_ToFind, callHelp_ActionAnimId, callHelp_CallActionId;
			Single nearDist, midDist, farDist, outDist, backHomeLife_OnHitEneWal;
			UInt16 eye_dist, ear_dist, ear_soundcut_dist, nose_dist, maxBackhomeDist, backhomeDist, backhomeBattleDist, nonBattleActLife, backHome_LookTargetTime, backHome_LookTargetDist, sightTargetForgetTime, soundTargetForgetTime, battleStartDist, callHelp_MyPeerId, callHelp_CallPeerId, targetSys_DmgEffectRate;
			Byte teamAttackEffectivity, eye_angX, eye_angY, ear_angX, ear_angY, callHelp_CallValidMinDistTarget, callHelp_CallValidRange, callHelp_ForgetTimeByArrival, callHelp_MinWaitTime, callHelp_MaxWaitTime, disablePathMove;
			NpcThoughtGoalAction goalAction_ToCaution, goalAction_ToFind;
			NpcThoughtReplyBehavior callHelp_ReplyBehaviorType;
			bool skipArrivalVisibleCheck, thinkAttr_doAdmirer;
			Byte[] enableNaviFlg_reserve1, pad0;

			/// <summary>A property in the class.</summary>
			public static readonly PropertyInfo
				LogicIdProperty = GetProperty<NpcThink>("LogicId"),
				BattleGoalIDProperty = GetProperty<NpcThink>("BattleGoalID"),
				NearDistProperty = GetProperty<NpcThink>("NearDist"),
				MidDistProperty = GetProperty<NpcThink>("MidDist"),
				FarDistProperty = GetProperty<NpcThink>("FarDist"),
				OutDistProperty = GetProperty<NpcThink>("OutDist"),
				BackHomeLife_OnHitEneWalProperty = GetProperty<NpcThink>("BackHomeLife_OnHitEneWal"),
				GoalID_ToCautionProperty = GetProperty<NpcThink>("GoalID_ToCaution"),
				IdAttackCannotMoveProperty = GetProperty<NpcThink>("IdAttackCannotMove"),
				GoalID_ToFindProperty = GetProperty<NpcThink>("GoalID_ToFind"),
				CallHelp_ActionAnimIdProperty = GetProperty<NpcThink>("CallHelp_ActionAnimId"),
				CallHelp_CallActionIdProperty = GetProperty<NpcThink>("CallHelp_CallActionId"),
				Eye_distProperty = GetProperty<NpcThink>("Eye_dist"),
				Ear_distProperty = GetProperty<NpcThink>("Ear_dist"),
				Ear_soundcut_distProperty = GetProperty<NpcThink>("Ear_soundcut_dist"),
				Nose_distProperty = GetProperty<NpcThink>("Nose_dist"),
				MaxBackhomeDistProperty = GetProperty<NpcThink>("MaxBackhomeDist"),
				BackhomeDistProperty = GetProperty<NpcThink>("BackhomeDist"),
				BackhomeBattleDistProperty = GetProperty<NpcThink>("BackhomeBattleDist"),
				NonBattleActLifeProperty = GetProperty<NpcThink>("NonBattleActLife"),
				BackHome_LookTargetTimeProperty = GetProperty<NpcThink>("BackHome_LookTargetTime"),
				BackHome_LookTargetDistProperty = GetProperty<NpcThink>("BackHome_LookTargetDist"),
				SightTargetForgetTimeProperty = GetProperty<NpcThink>("SightTargetForgetTime"),
				SoundTargetForgetTimeProperty = GetProperty<NpcThink>("SoundTargetForgetTime"),
				BattleStartDistProperty = GetProperty<NpcThink>("BattleStartDist"),
				CallHelp_MyPeerIdProperty = GetProperty<NpcThink>("CallHelp_MyPeerId"),
				CallHelp_CallPeerIdProperty = GetProperty<NpcThink>("CallHelp_CallPeerId"),
				TargetSys_DmgEffectRateProperty = GetProperty<NpcThink>("TargetSys_DmgEffectRate"),
				TeamAttackEffectivityProperty = GetProperty<NpcThink>("TeamAttackEffectivity"),
				Eye_angXProperty = GetProperty<NpcThink>("Eye_angX"),
				Eye_angYProperty = GetProperty<NpcThink>("Eye_angY"),
				Ear_angXProperty = GetProperty<NpcThink>("Ear_angX"),
				Ear_angYProperty = GetProperty<NpcThink>("Ear_angY"),
				CallHelp_CallValidMinDistTargetProperty = GetProperty<NpcThink>("CallHelp_CallValidMinDistTarget"),
				CallHelp_CallValidRangeProperty = GetProperty<NpcThink>("CallHelp_CallValidRange"),
				CallHelp_ForgetTimeByArrivalProperty = GetProperty<NpcThink>("CallHelp_ForgetTimeByArrival"),
				CallHelp_MinWaitTimeProperty = GetProperty<NpcThink>("CallHelp_MinWaitTime"),
				CallHelp_MaxWaitTimeProperty = GetProperty<NpcThink>("CallHelp_MaxWaitTime"),
				GoalAction_ToCautionProperty = GetProperty<NpcThink>("GoalAction_ToCaution"),
				GoalAction_ToFindProperty = GetProperty<NpcThink>("GoalAction_ToFind"),
				CallHelp_ReplyBehaviorTypeProperty = GetProperty<NpcThink>("CallHelp_ReplyBehaviorType"),
				DisablePathMoveProperty = GetProperty<NpcThink>("DisablePathMove"),
				SkipArrivalVisibleCheckProperty = GetProperty<NpcThink>("SkipArrivalVisibleCheck"),
				ThinkAttr_doAdmirerProperty = GetProperty<NpcThink>("ThinkAttr_doAdmirer"),
				EnableNaviFlg_EdgeProperty = GetProperty<NpcThink>("EnableNaviFlg_Edge"),
				EnableNaviFlg_LargeSpaceProperty = GetProperty<NpcThink>("EnableNaviFlg_LargeSpace"),
				EnableNaviFlg_LadderProperty = GetProperty<NpcThink>("EnableNaviFlg_Ladder"),
				EnableNaviFlg_HoleProperty = GetProperty<NpcThink>("EnableNaviFlg_Hole"),
				EnableNaviFlg_DoorProperty = GetProperty<NpcThink>("EnableNaviFlg_Door"),
				EnableNaviFlg_InSideWallProperty = GetProperty<NpcThink>("EnableNaviFlg_InSideWall"),
				EnableNaviFlg_reserve0Property = GetProperty<NpcThink>("EnableNaviFlg_reserve0"),
				EnableNaviFlg_reserve1Property = GetProperty<NpcThink>("EnableNaviFlg_reserve1"),
				Pad0Property = GetProperty<NpcThink>("Pad0");

			/// <summary>Logic script ID</summary>
			/// <remarks>
			/// Japanese short name: "ロジックスクリプトID", Google translated: "Logic script ID".
			/// Japanese description: "スクリプトで作成したロジックのIDを設定します。", Google translated: "You can set the ID of the logic that you create in the Script .".
			/// </remarks>
			[ParameterTableRowAttribute("logicId", index: 0, minimum: -1, maximum: 999999, step: 1, sortOrder: 100, unknown2: 1)]
			[DisplayName("Logic script ID")]
			[Description("You can set the ID of the logic that you create in the Script .")]
			[DefaultValue((Int32)(-1))]
			public Int32 LogicId {
				get { return logicId; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for LogicId.");
					SetProperty(ref logicId, ref value, LogicIdProperty);
				}
			}

			/// <summary>Goal combat ID</summary>
			/// <remarks>
			/// Japanese short name: "戦闘ゴールID", Google translated: "Goal combat ID".
			/// Japanese description: "戦闘ゴールID", Google translated: "Goal combat ID".
			/// </remarks>
			[ParameterTableRowAttribute("battleGoalID", index: 1, minimum: -1, maximum: 999999, step: 1, sortOrder: 200, unknown2: 1)]
			[DisplayName("Goal combat ID")]
			[Description("Goal combat ID")]
			[DefaultValue((Int32)(-1))]
			public Int32 BattleGoalID {
				get { return battleGoalID; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for BattleGoalID.");
					SetProperty(ref battleGoalID, ref value, BattleGoalIDProperty);
				}
			}

			/// <summary>Short-range distance [m]</summary>
			/// <remarks>
			/// Japanese short name: "近距離 距離[m]", Google translated: "Short-range distance [m]".
			/// Japanese description: "近距離とみなす範囲です。近接格闘の判定に使用します。", Google translated: "The range to be considered a short distance . I used to determine the proximity of fighting .".
			/// </remarks>
			[ParameterTableRowAttribute("nearDist", index: 2, minimum: 0, maximum: 99999, step: 0.1, sortOrder: 800, unknown2: 1)]
			[DisplayName("Short-range distance [m]")]
			[Description("The range to be considered a short distance . I used to determine the proximity of fighting .")]
			[DefaultValue((Single)0)]
			public Single NearDist {
				get { return nearDist; }
				set {
					if ((double)value < 0 || (double)value > 99999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99999 for NearDist.");
					SetProperty(ref nearDist, ref value, NearDistProperty);
				}
			}

			/// <summary>Medium-range distance [m]</summary>
			/// <remarks>
			/// Japanese short name: "中距離 距離[m]", Google translated: "Medium-range distance [m]".
			/// Japanese description: "中距離とみなす範囲です。中距離格闘の判定に使用します。", Google translated: "The range to be considered a middle distance . I use to determine the medium-range fighting .".
			/// </remarks>
			[ParameterTableRowAttribute("midDist", index: 3, minimum: 0, maximum: 99999, step: 0.1, sortOrder: 900, unknown2: 1)]
			[DisplayName("Medium-range distance [m]")]
			[Description("The range to be considered a middle distance . I use to determine the medium-range fighting .")]
			[DefaultValue((Single)0)]
			public Single MidDist {
				get { return midDist; }
				set {
					if ((double)value < 0 || (double)value > 99999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99999 for MidDist.");
					SetProperty(ref midDist, ref value, MidDistProperty);
				}
			}

			/// <summary>Long distance distance [m]</summary>
			/// <remarks>
			/// Japanese short name: "遠距離 距離[m]", Google translated: "Long distance distance [m]".
			/// Japanese description: "遠距離とみなす範囲です。飛び道具の判定に使用します。", Google translated: "The range to be considered a long distance . I use to determine the missile .".
			/// </remarks>
			[ParameterTableRowAttribute("farDist", index: 4, minimum: 0, maximum: 99999, step: 0.1, sortOrder: 1000, unknown2: 1)]
			[DisplayName("Long distance distance [m]")]
			[Description("The range to be considered a long distance . I use to determine the missile .")]
			[DefaultValue((Single)0)]
			public Single FarDist {
				get { return farDist; }
				set {
					if ((double)value < 0 || (double)value > 99999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99999 for FarDist.");
					SetProperty(ref farDist, ref value, FarDistProperty);
				}
			}

			/// <summary>Out-of-range distance [m]</summary>
			/// <remarks>
			/// Japanese short name: "範囲外 距離[m]", Google translated: "Out-of-range distance [m]".
			/// Japanese description: "戦闘範囲外とみなす距離です。範囲外の敵には戦闘行動を行おうとはしなくなります。", Google translated: "The distance to be considered a combat range. So that it no longer is it attempts to combat the enemy is out of range .".
			/// </remarks>
			[ParameterTableRowAttribute("outDist", index: 5, minimum: 0, maximum: 99999, step: 0.1, sortOrder: 1100, unknown2: 1)]
			[DisplayName("Out-of-range distance [m]")]
			[Description("The distance to be considered a combat range. So that it no longer is it attempts to combat the enemy is out of range .")]
			[DefaultValue((Single)0)]
			public Single OutDist {
				get { return outDist; }
				set {
					if ((double)value < 0 || (double)value > 99999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99999 for OutDist.");
					SetProperty(ref outDist, ref value, OutDistProperty);
				}
			}

			/// <summary>BackHome time enemy of wall contact time [sec]</summary>
			/// <remarks>
			/// Japanese short name: "敵壁接触時のBackHome時間[sec]", Google translated: "BackHome time enemy of wall contact time [sec]".
			/// Japanese description: "ブロックをさえぎる敵壁に接触したとき、BackToHomeゴールの寿命", Google translated: "When in contact with the enemy wall blocking the line of block , the life of the goal BackToHome".
			/// </remarks>
			[ParameterTableRowAttribute("BackHomeLife_OnHitEneWal", index: 6, minimum: 0, maximum: 999, step: 0.1, sortOrder: 3000, unknown2: 1)]
			[DisplayName("BackHome time enemy of wall contact time [sec]")]
			[Description("When in contact with the enemy wall blocking the line of block , the life of the goal BackToHome")]
			[DefaultValue((Single)5)]
			public Single BackHomeLife_OnHitEneWal {
				get { return backHomeLife_OnHitEneWal; }
				set {
					if ((double)value < 0 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for BackHomeLife_OnHitEneWal.");
					SetProperty(ref backHomeLife_OnHitEneWal, ref value, BackHomeLife_OnHitEneWalProperty);
				}
			}

			/// <summary>Goal : alert status</summary>
			/// <remarks>
			/// Japanese short name: "ゴール：警戒状態", Google translated: "Goal : alert status".
			/// Japanese description: "ゴール：警戒状態", Google translated: "Goal : alert status".
			/// </remarks>
			[ParameterTableRowAttribute("goalID_ToCaution", index: 7, minimum: -1, maximum: 999999, step: 1, sortOrder: 3400, unknown2: 1)]
			[DisplayName("Goal : alert status")]
			[Description("Goal : alert status")]
			[DefaultValue((Int32)0)]
			public Int32 GoalID_ToCaution {
				get { return goalID_ToCaution; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for GoalID_ToCaution.");
					SetProperty(ref goalID_ToCaution, ref value, GoalID_ToCautionProperty);
				}
			}

			/// <summary>EzState number to be performed when it is stuck</summary>
			/// <remarks>
			/// Japanese short name: "動けなくなったときに行うEzState番号", Google translated: "EzState number to be performed when it is stuck".
			/// Japanese description: "破壊可能なオブジェクトによって動きが止まっている場合、自動的に実行する行動。", Google translated: "When the motion is stopped by a breakable objects , actions to be executed automatically .".
			/// </remarks>
			[ParameterTableRowAttribute("idAttackCannotMove", index: 8, minimum: -1, maximum: 999999, step: 1, sortOrder: 2900, unknown2: 1)]
			[DisplayName("EzState number to be performed when it is stuck")]
			[Description("When the motion is stopped by a breakable objects , actions to be executed automatically .")]
			[DefaultValue((Int32)0)]
			public Int32 IdAttackCannotMove {
				get { return idAttackCannotMove; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for IdAttackCannotMove.");
					SetProperty(ref idAttackCannotMove, ref value, IdAttackCannotMoveProperty);
				}
			}

			/// <summary>Goal : finding state</summary>
			/// <remarks>
			/// Japanese short name: "ゴール：発見状態", Google translated: "Goal : finding state".
			/// Japanese description: "ゴール：発見状態", Google translated: "Goal : finding state".
			/// </remarks>
			[ParameterTableRowAttribute("goalID_ToFind", index: 9, minimum: -1, maximum: 999999, step: 1, sortOrder: 3800, unknown2: 1)]
			[DisplayName("Goal : finding state")]
			[Description("Goal : finding state")]
			[DefaultValue((Int32)0)]
			public Int32 GoalID_ToFind {
				get { return goalID_ToFind; }
				set {
					if ((double)value < -1 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for GoalID_ToFind.");
					SetProperty(ref goalID_ToFind, ref value, GoalID_ToFindProperty);
				}
			}

			/// <summary>Response action anime and ID fellow</summary>
			/// <remarks>
			/// Japanese short name: "仲間呼び 応答アクションアニメID", Google translated: "Response action anime and ID fellow".
			/// Japanese description: "応答する時のアニメID(EzStateAnimID)", Google translated: "Anime of ID when responding (EzStateAnimID)".
			/// </remarks>
			[ParameterTableRowAttribute("callHelp_ActionAnimId", index: 10, minimum: -1, maximum: 1E+08, step: 1, sortOrder: 10100, unknown2: 1)]
			[DisplayName("Response action anime and ID fellow")]
			[Description("Anime of ID when responding (EzStateAnimID)")]
			[DefaultValue((Int32)(-1))]
			public Int32 CallHelp_ActionAnimId {
				get { return callHelp_ActionAnimId; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for CallHelp_ActionAnimId.");
					SetProperty(ref callHelp_ActionAnimId, ref value, CallHelp_ActionAnimIdProperty);
				}
			}

			/// <summary>Action and ID fellow _ called fellow</summary>
			/// <remarks>
			/// Japanese short name: "仲間呼び_仲間呼びアクションID", Google translated: "Action and ID fellow _ called fellow".
			/// Japanese description: "仲間呼ぶときのアクションID(EzStateAnimID)", Google translated: "ID of the action when calling fellow (EzStateAnimID)".
			/// </remarks>
			[ParameterTableRowAttribute("callHelp_CallActionId", index: 11, minimum: -1, maximum: 1E+08, step: 1, sortOrder: 10110, unknown2: 1)]
			[DisplayName("Action and ID fellow _ called fellow")]
			[Description("ID of the action when calling fellow (EzStateAnimID)")]
			[DefaultValue((Int32)(-1))]
			public Int32 CallHelp_CallActionId {
				get { return callHelp_CallActionId; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for CallHelp_CallActionId.");
					SetProperty(ref callHelp_CallActionId, ref value, CallHelp_CallActionIdProperty);
				}
			}

			/// <summary>_ Visual distance [m]</summary>
			/// <remarks>
			/// Japanese short name: "視覚_距離[m]", Google translated: "_ Visual distance [m]".
			/// Japanese description: "視覚による索敵範囲.", Google translated: "The search operation visual range .".
			/// </remarks>
			[ParameterTableRowAttribute("eye_dist", index: 12, minimum: 0, maximum: 65535, step: 1, sortOrder: 1200, unknown2: 1)]
			[DisplayName("_ Visual distance [m]")]
			[Description("The search operation visual range .")]
			[DefaultValue((UInt16)0)]
			public UInt16 Eye_dist {
				get { return eye_dist; }
				set {
					if ((double)value < 0 || (double)value > 65535)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 65535 for Eye_dist.");
					SetProperty(ref eye_dist, ref value, Eye_distProperty);
				}
			}

			/// <summary>_ Hearing distance [cm]</summary>
			/// <remarks>
			/// Japanese short name: "聴覚_距離[cm]", Google translated: "_ Hearing distance [cm]".
			/// Japanese description: "聴覚による索敵範囲.。", Google translated: "The search operation range . Auditory .".
			/// </remarks>
			[ParameterTableRowAttribute("ear_dist", index: 13, minimum: 0, maximum: 65535, step: 1, sortOrder: 1500, unknown2: 1)]
			[DisplayName("_ Hearing distance [cm]")]
			[Description("The search operation range . Auditory .")]
			[DefaultValue((UInt16)0)]
			public UInt16 Ear_dist {
				get { return ear_dist; }
				set {
					if ((double)value < 0 || (double)value > 65535)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 65535 for Ear_dist.");
					SetProperty(ref ear_dist, ref value, Ear_distProperty);
				}
			}

			/// <summary>Hearing impact cut distance [m]</summary>
			/// <remarks>
			/// Japanese short name: "聴覚　影響カット距離[m]", Google translated: "Hearing impact cut distance [m]".
			/// Japanese description: "音源のサイズを小さくする距離。この距離未満の音が聞こえなくなります。", Google translated: "Distance to reduce the size of the sound source . Sound of this distance is less than will not be heard .".
			/// </remarks>
			[ParameterTableRowAttribute("ear_soundcut_dist", index: 14, minimum: 0, maximum: 65535, step: 1, sortOrder: 1600, unknown2: 1)]
			[DisplayName("Hearing impact cut distance [m]")]
			[Description("Distance to reduce the size of the sound source . Sound of this distance is less than will not be heard .")]
			[DefaultValue((UInt16)0)]
			public UInt16 Ear_soundcut_dist {
				get { return ear_soundcut_dist; }
				set {
					if ((double)value < 0 || (double)value > 65535)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 65535 for Ear_soundcut_dist.");
					SetProperty(ref ear_soundcut_dist, ref value, Ear_soundcut_distProperty);
				}
			}

			/// <summary>_ Olfactory distance [m]</summary>
			/// <remarks>
			/// Japanese short name: "嗅覚_距離[m]", Google translated: "_ Olfactory distance [m]".
			/// Japanese description: "嗅覚による索敵範囲.", Google translated: "The search operation range by the sense of smell .".
			/// </remarks>
			[ParameterTableRowAttribute("nose_dist", index: 15, minimum: 0, maximum: 65535, step: 1, sortOrder: 2000, unknown2: 1)]
			[DisplayName("_ Olfactory distance [m]")]
			[Description("The search operation range by the sense of smell .")]
			[DefaultValue((UInt16)0)]
			public UInt16 Nose_dist {
				get { return nose_dist; }
				set {
					if ((double)value < 0 || (double)value > 65535)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 65535 for Nose_dist.");
					SetProperty(ref nose_dist, ref value, Nose_distProperty);
				}
			}

			/// <summary>Distance to go home no matter what [m]</summary>
			/// <remarks>
			/// Japanese short name: "何があっても帰宅する距離[m]", Google translated: "Distance to go home no matter what [m]".
			/// Japanese description: "COMMON_SetBattleActLogicの引き数", Google translated: "Argument of COMMON_SetBattleActLogic".
			/// </remarks>
			[ParameterTableRowAttribute("maxBackhomeDist", index: 16, minimum: 0, maximum: 65535, step: 1, sortOrder: 2100, unknown2: 1)]
			[DisplayName("Distance to go home no matter what [m]")]
			[Description("Argument of COMMON_SetBattleActLogic")]
			[DefaultValue((UInt16)0)]
			public UInt16 MaxBackhomeDist {
				get { return maxBackhomeDist; }
				set {
					if ((double)value < 0 || (double)value > 65535)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 65535 for MaxBackhomeDist.");
					SetProperty(ref maxBackhomeDist, ref value, MaxBackhomeDistProperty);
				}
			}

			/// <summary>Distance to go home while fighting [m]</summary>
			/// <remarks>
			/// Japanese short name: "戦闘しつつ帰宅する距離[m]", Google translated: "Distance to go home while fighting [m]".
			/// Japanese description: "COMMON_SetBattleActLogicの引き数", Google translated: "Argument of COMMON_SetBattleActLogic".
			/// </remarks>
			[ParameterTableRowAttribute("backhomeDist", index: 17, minimum: 0, maximum: 65535, step: 1, sortOrder: 2200, unknown2: 1)]
			[DisplayName("Distance to go home while fighting [m]")]
			[Description("Argument of COMMON_SetBattleActLogic")]
			[DefaultValue((UInt16)0)]
			public UInt16 BackhomeDist {
				get { return backhomeDist; }
				set {
					if ((double)value < 0 || (double)value > 65535)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 65535 for BackhomeDist.");
					SetProperty(ref backhomeDist, ref value, BackhomeDistProperty);
				}
			}

			/// <summary>Distance to battle to give up and go home to the nest [m]</summary>
			/// <remarks>
			/// Japanese short name: "巣に帰るのをあきらめて戦闘する距離[m]", Google translated: "Distance to battle to give up and go home to the nest [m]".
			/// Japanese description: "COMMON_SetBattleActLogicの引き数", Google translated: "Argument of COMMON_SetBattleActLogic".
			/// </remarks>
			[ParameterTableRowAttribute("backhomeBattleDist", index: 18, minimum: 0, maximum: 65535, step: 1, sortOrder: 2300, unknown2: 1)]
			[DisplayName("Distance to battle to give up and go home to the nest [m]")]
			[Description("Argument of COMMON_SetBattleActLogic")]
			[DefaultValue((UInt16)0)]
			public UInt16 BackhomeBattleDist {
				get { return backhomeBattleDist; }
				set {
					if ((double)value < 0 || (double)value > 65535)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 65535 for BackhomeBattleDist.");
					SetProperty(ref backhomeBattleDist, ref value, BackhomeBattleDistProperty);
				}
			}

			/// <summary>Non- combat time when you are aware of the enemy [sec]</summary>
			/// <remarks>
			/// Japanese short name: "敵を意識しているときの非戦闘行動時間[sec]", Google translated: "Non- combat time when you are aware of the enemy [sec]".
			/// Japanese description: "COMMON_SetBattleActLogicの引き数", Google translated: "Argument of COMMON_SetBattleActLogic".
			/// </remarks>
			[ParameterTableRowAttribute("nonBattleActLife", index: 19, minimum: 0, maximum: 65534, step: 1, sortOrder: 2400, unknown2: 1)]
			[DisplayName("Non- combat time when you are aware of the enemy [sec]")]
			[Description("Argument of COMMON_SetBattleActLogic")]
			[DefaultValue((UInt16)0)]
			public UInt16 NonBattleActLife {
				get { return nonBattleActLife; }
				set {
					if ((double)value < 0 || (double)value > 65534)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 65534 for NonBattleActLife.");
					SetProperty(ref nonBattleActLife, ref value, NonBattleActLifeProperty);
				}
			}

			/// <summary>Returning home : you are looking at the target time [sec]</summary>
			/// <remarks>
			/// Japanese short name: "帰宅時：ターゲットを見ている時間[sec]", Google translated: "Returning home : you are looking at the target time [sec]".
			/// Japanese description: "帰宅時：ターゲットを見ている時間[sec]", Google translated: "Returning home : you are looking at the target time [sec]".
			/// </remarks>
			[ParameterTableRowAttribute("BackHome_LookTargetTime", index: 20, minimum: 0, maximum: 65534, step: 1, sortOrder: 2500, unknown2: 1)]
			[DisplayName("Returning home : you are looking at the target time [sec]")]
			[Description("Returning home : you are looking at the target time [sec]")]
			[DefaultValue((UInt16)3)]
			public UInt16 BackHome_LookTargetTime {
				get { return backHome_LookTargetTime; }
				set {
					if ((double)value < 0 || (double)value > 65534)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 65534 for BackHome_LookTargetTime.");
					SetProperty(ref backHome_LookTargetTime, ref value, BackHome_LookTargetTimeProperty);
				}
			}

			/// <summary>Returning home : distance looking at the target [m]</summary>
			/// <remarks>
			/// Japanese short name: "帰宅時：ターゲットを見ている距離[m]", Google translated: "Returning home : distance looking at the target [m]".
			/// Japanese description: "帰宅時：ターゲットを見ている距離[m]", Google translated: "Returning home : distance looking at the target [m]".
			/// </remarks>
			[ParameterTableRowAttribute("BackHome_LookTargetDist", index: 21, minimum: 0, maximum: 65534, step: 1, sortOrder: 2600, unknown2: 1)]
			[DisplayName("Returning home : distance looking at the target [m]")]
			[Description("Returning home : distance looking at the target [m]")]
			[DefaultValue((UInt16)10)]
			public UInt16 BackHome_LookTargetDist {
				get { return backHome_LookTargetDist; }
				set {
					if ((double)value < 0 || (double)value > 65534)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 65534 for BackHome_LookTargetDist.");
					SetProperty(ref backHome_LookTargetDist, ref value, BackHome_LookTargetDistProperty);
				}
			}

			/// <summary>Time to forget the visual target [frame]</summary>
			/// <remarks>
			/// Japanese short name: "視覚ターゲット忘れる時間[frame]", Google translated: "Time to forget the visual target [frame]".
			/// Japanese description: "視覚ターゲット忘れる時間。フレームで入力です。", Google translated: "Time to forget the visual target . It is entered in the frame .".
			/// </remarks>
			[ParameterTableRowAttribute("SightTargetForgetTime", index: 22, minimum: 0, maximum: 65534, step: 1, sortOrder: 2700, unknown2: 1)]
			[DisplayName("Time to forget the visual target [frame]")]
			[Description("Time to forget the visual target . It is entered in the frame .")]
			[DefaultValue((UInt16)600)]
			public UInt16 SightTargetForgetTime {
				get { return sightTargetForgetTime; }
				set {
					if ((double)value < 0 || (double)value > 65534)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 65534 for SightTargetForgetTime.");
					SetProperty(ref sightTargetForgetTime, ref value, SightTargetForgetTimeProperty);
				}
			}

			/// <summary>Time to forget the target sound [frame]</summary>
			/// <remarks>
			/// Japanese short name: "音ターゲット忘れる時間[frame]", Google translated: "Time to forget the target sound [frame]".
			/// Japanese description: "音ターゲット忘れる時間。フレームで入力です。", Google translated: "Time to forget the target sound . It is entered in the frame .".
			/// </remarks>
			[ParameterTableRowAttribute("SoundTargetForgetTime", index: 23, minimum: 0, maximum: 65534, step: 1, sortOrder: 2800, unknown2: 1)]
			[DisplayName("Time to forget the target sound [frame]")]
			[Description("Time to forget the target sound . It is entered in the frame .")]
			[DefaultValue((UInt16)150)]
			public UInt16 SoundTargetForgetTime {
				get { return soundTargetForgetTime; }
				set {
					if ((double)value < 0 || (double)value > 65534)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 65534 for SoundTargetForgetTime.");
					SetProperty(ref soundTargetForgetTime, ref value, SoundTargetForgetTimeProperty);
				}
			}

			/// <summary>Distance to start the battle after you discover the enemy [m]</summary>
			/// <remarks>
			/// Japanese short name: "敵を発見してから戦闘を開始する距離[m]", Google translated: "Distance to start the battle after you discover the enemy [m]".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("BattleStartDist", index: 24, minimum: 0, maximum: 65534, step: 1, sortOrder: 3500, unknown2: 1)]
			[DisplayName("Distance to start the battle after you discover the enemy [m]")]
			[Description("")]
			[DefaultValue((UInt16)0)]
			public UInt16 BattleStartDist {
				get { return battleStartDist; }
				set {
					if ((double)value < 0 || (double)value > 65534)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 65534 for BattleStartDist.");
					SetProperty(ref battleStartDist, ref value, BattleStartDistProperty);
				}
			}

			/// <summary>Fellow group ID of their fellow called</summary>
			/// <remarks>
			/// Japanese short name: "仲間呼び 自分の仲間グループID", Google translated: "Fellow group ID of their fellow called".
			/// Japanese description: "自分の仲間グループID", Google translated: "Fellow group ID of their".
			/// </remarks>
			[ParameterTableRowAttribute("callHelp_MyPeerId", index: 25, minimum: 0, maximum: 65534, step: 1, sortOrder: 9800, unknown2: 1)]
			[DisplayName("Fellow group ID of their fellow called")]
			[Description("Fellow group ID of their")]
			[DefaultValue((UInt16)0)]
			public UInt16 CallHelp_MyPeerId {
				get { return callHelp_MyPeerId; }
				set {
					if ((double)value < 0 || (double)value > 65534)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 65534 for CallHelp_MyPeerId.");
					SetProperty(ref callHelp_MyPeerId, ref value, CallHelp_MyPeerIdProperty);
				}
			}

			/// <summary>Fellow group ID is referred to as call fellow</summary>
			/// <remarks>
			/// Japanese short name: "仲間呼び 呼ぶ仲間グループID", Google translated: "Fellow group ID is referred to as call fellow".
			/// Japanese description: "仲間を呼ぶ対象となる仲間グループID", Google translated: "Fellow group ID to be called the fellow".
			/// </remarks>
			[ParameterTableRowAttribute("callHelp_CallPeerId", index: 26, minimum: 0, maximum: 65534, step: 1, sortOrder: 9900, unknown2: 1)]
			[DisplayName("Fellow group ID is referred to as call fellow")]
			[Description("Fellow group ID to be called the fellow")]
			[DefaultValue((UInt16)0)]
			public UInt16 CallHelp_CallPeerId {
				get { return callHelp_CallPeerId; }
				set {
					if ((double)value < 0 || (double)value > 65534)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 65534 for CallHelp_CallPeerId.");
					SetProperty(ref callHelp_CallPeerId, ref value, CallHelp_CallPeerIdProperty);
				}
			}

			/// <summary>Damage impact rate [ %]</summary>
			/// <remarks>
			/// Japanese short name: "ダメージ影響率[％]", Google translated: "Damage impact rate [ %]".
			/// Japanese description: "ダメージ影響率取得(ターゲットシステム評価情報)", Google translated: "Impact damage rate acquisition ( target system evaluation information )".
			/// </remarks>
			[ParameterTableRowAttribute("targetSys_DmgEffectRate", index: 27, minimum: 0, maximum: 1000, step: 1, sortOrder: 550, unknown2: 1)]
			[DisplayName("Damage impact rate [ %]")]
			[Description("Impact damage rate acquisition ( target system evaluation information )")]
			[DefaultValue((UInt16)100)]
			public UInt16 TargetSys_DmgEffectRate {
				get { return targetSys_DmgEffectRate; }
				set {
					if ((double)value < 0 || (double)value > 1000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1000 for TargetSys_DmgEffectRate.");
					SetProperty(ref targetSys_DmgEffectRate, ref value, TargetSys_DmgEffectRateProperty);
				}
			}

			/// <summary>Team attack influence [ 0-100 ]</summary>
			/// <remarks>
			/// Japanese short name: "チーム攻撃影響力[0-100]", Google translated: "Team attack influence [ 0-100 ]".
			/// Japanese description: "チーム内の同時攻撃人数を決めるための値。値を大きくすると、同時に攻撃参加できる人数が少なくなる。", Google translated: "Value for determining the number of simultaneous attack team . If you increase the value , many people can participate at the same time attack is reduced.".
			/// </remarks>
			[ParameterTableRowAttribute("TeamAttackEffectivity", index: 28, minimum: 0, maximum: 100, step: 1, sortOrder: 500, unknown2: 1)]
			[DisplayName("Team attack influence [ 0-100 ]")]
			[Description("Value for determining the number of simultaneous attack team . If you increase the value , many people can participate at the same time attack is reduced.")]
			[DefaultValue((Byte)25)]
			public Byte TeamAttackEffectivity {
				get { return teamAttackEffectivity; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for TeamAttackEffectivity.");
					SetProperty(ref teamAttackEffectivity, ref value, TeamAttackEffectivityProperty);
				}
			}

			/// <summary>_ Visual angle ( height ) [deg]</summary>
			/// <remarks>
			/// Japanese short name: "視覚_角度（高さ）[deg]", Google translated: "_ Visual angle ( height ) [deg]".
			/// Japanese description: "視覚による索敵範囲.", Google translated: "The search operation visual range .".
			/// </remarks>
			[ParameterTableRowAttribute("eye_angX", index: 29, minimum: 0, maximum: 180, step: 1, sortOrder: 1300, unknown2: 1)]
			[DisplayName("_ Visual angle ( height ) [deg]")]
			[Description("The search operation visual range .")]
			[DefaultValue((Byte)0)]
			public Byte Eye_angX {
				get { return eye_angX; }
				set {
					if ((double)value < 0 || (double)value > 180)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 180 for Eye_angX.");
					SetProperty(ref eye_angX, ref value, Eye_angXProperty);
				}
			}

			/// <summary>_ Visual angle ( width ) [deg]</summary>
			/// <remarks>
			/// Japanese short name: "視覚_角度（幅）[deg]", Google translated: "_ Visual angle ( width ) [deg]".
			/// Japanese description: "視覚による索敵範囲.", Google translated: "The search operation visual range .".
			/// </remarks>
			[ParameterTableRowAttribute("eye_angY", index: 30, minimum: 0, maximum: 180, step: 1, sortOrder: 1400, unknown2: 1)]
			[DisplayName("_ Visual angle ( width ) [deg]")]
			[Description("The search operation visual range .")]
			[DefaultValue((Byte)0)]
			public Byte Eye_angY {
				get { return eye_angY; }
				set {
					if ((double)value < 0 || (double)value > 180)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 180 for Eye_angY.");
					SetProperty(ref eye_angY, ref value, Eye_angYProperty);
				}
			}

			/// <summary>Hearing angle ( height ) [deg]</summary>
			/// <remarks>
			/// Japanese short name: "聴覚　角度（高さ）[deg]", Google translated: "Hearing angle ( height ) [deg]".
			/// Japanese description: "聴覚による索敵範囲。", Google translated: "The search operation range of the hearing .".
			/// </remarks>
			[ParameterTableRowAttribute("ear_angX", index: 31, minimum: 0, maximum: 90, step: 1, sortOrder: 1700, unknown2: 1)]
			[DisplayName("Hearing angle ( height ) [deg]")]
			[Description("The search operation range of the hearing .")]
			[DefaultValue((Byte)90)]
			public Byte Ear_angX {
				get { return ear_angX; }
				set {
					if ((double)value < 0 || (double)value > 90)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 90 for Ear_angX.");
					SetProperty(ref ear_angX, ref value, Ear_angXProperty);
				}
			}

			/// <summary>Hearing angle ( width ) [deg]</summary>
			/// <remarks>
			/// Japanese short name: "聴覚　角度（幅）[deg]", Google translated: "Hearing angle ( width ) [deg]".
			/// Japanese description: "聴覚による索敵範囲。", Google translated: "The search operation range of the hearing .".
			/// </remarks>
			[ParameterTableRowAttribute("ear_angY", index: 32, minimum: 0, maximum: 180, step: 1, sortOrder: 1800, unknown2: 1)]
			[DisplayName("Hearing angle ( width ) [deg]")]
			[Description("The search operation range of the hearing .")]
			[DefaultValue((Byte)180)]
			public Byte Ear_angY {
				get { return ear_angY; }
				set {
					if ((double)value < 0 || (double)value > 180)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 180 for Ear_angY.");
					SetProperty(ref ear_angY, ref value, Ear_angYProperty);
				}
			}

			/// <summary>Minimum distance to the target is called _ fellow [m]</summary>
			/// <remarks>
			/// Japanese short name: "仲間呼び_ターゲットとの最低距離[m]", Google translated: "Minimum distance to the target is called _ fellow [m]".
			/// Japanese description: "この値より近い場合は仲間呼びできない.", Google translated: "I can not call fellow if closer than this value .".
			/// </remarks>
			[ParameterTableRowAttribute("callHelp_CallValidMinDistTarget", index: 33, minimum: 0, maximum: 255, step: 1, sortOrder: 10400, unknown2: 1)]
			[DisplayName("Minimum distance to the target is called _ fellow [m]")]
			[Description("I can not call fellow if closer than this value .")]
			[DefaultValue((Byte)5)]
			public Byte CallHelp_CallValidMinDistTarget {
				get { return callHelp_CallValidMinDistTarget; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for CallHelp_CallValidMinDistTarget.");
					SetProperty(ref callHelp_CallValidMinDistTarget, ref value, CallHelp_CallValidMinDistTargetProperty);
				}
			}

			/// <summary>Effective distance calling fellow _ called fellow [m]</summary>
			/// <remarks>
			/// Japanese short name: "仲間呼び_仲間を呼ぶ有効距離[m]", Google translated: "Effective distance calling fellow _ called fellow [m]".
			/// Japanese description: "この値より仲間が遠い場合は呼ばない。", Google translated: "It is not known if fellow is far from this value .".
			/// </remarks>
			[ParameterTableRowAttribute("callHelp_CallValidRange", index: 34, minimum: 0, maximum: 255, step: 1, sortOrder: 10350, unknown2: 1)]
			[DisplayName("Effective distance calling fellow _ called fellow [m]")]
			[Description("It is not known if fellow is far from this value .")]
			[DefaultValue((Byte)15)]
			public Byte CallHelp_CallValidRange {
				get { return callHelp_CallValidRange; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for CallHelp_CallValidRange.");
					SetProperty(ref callHelp_CallValidRange, ref value, CallHelp_CallValidRangeProperty);
				}
			}

			/// <summary>Time to forget it after response called fellow [sec]</summary>
			/// <remarks>
			/// Japanese short name: "仲間呼び 応答してから忘れる時間[sec]", Google translated: "Time to forget it after response called fellow [sec]".
			/// Japanese description: "応答する時間", Google translated: "Time to respond".
			/// </remarks>
			[ParameterTableRowAttribute("callHelp_ForgetTimeByArrival", index: 35, minimum: 0, maximum: 255, step: 1, sortOrder: 10300, unknown2: 1)]
			[DisplayName("Time to forget it after response called fellow [sec]")]
			[Description("Time to respond")]
			[DefaultValue((Byte)0)]
			public Byte CallHelp_ForgetTimeByArrival {
				get { return callHelp_ForgetTimeByArrival; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for CallHelp_ForgetTimeByArrival.");
					SetProperty(ref callHelp_ForgetTimeByArrival, ref value, CallHelp_ForgetTimeByArrivalProperty);
				}
			}

			/// <summary>[Ssm => ss wait minimum time of response. mSec]</summary>
			/// <remarks>
			/// Japanese short name: "応答時の待機最小時間[ssm=>ss．mSec]", Google translated: "[Ssm => ss wait minimum time of response. mSec]".
			/// Japanese description: "応答ゴールの最初の待機ゴールでの最小時間[101=>10．1sec]", Google translated: "Minimum waiting time in the first goal of the response goal [101 => 10.1sec]".
			/// </remarks>
			[ParameterTableRowAttribute("callHelp_MinWaitTime", index: 36, minimum: 0, maximum: 255, step: 1, sortOrder: 10450, unknown2: 1)]
			[DisplayName("[Ssm => ss wait minimum time of response. mSec]")]
			[Description("Minimum waiting time in the first goal of the response goal [101 => 10.1sec]")]
			[DefaultValue((Byte)0)]
			public Byte CallHelp_MinWaitTime {
				get { return callHelp_MinWaitTime; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for CallHelp_MinWaitTime.");
					SetProperty(ref callHelp_MinWaitTime, ref value, CallHelp_MinWaitTimeProperty);
				}
			}

			/// <summary>[Ssm => ss waiting time of maximum response. mSec]</summary>
			/// <remarks>
			/// Japanese short name: "応答時の待機最大時間[ssm=>ss．mSec]", Google translated: "[Ssm => ss waiting time of maximum response. mSec]".
			/// Japanese description: "応答ゴールの最初の待機ゴールでの最大時間[101=>10．1sec]", Google translated: "The maximum waiting time at the first goal of the response goal [101 => 10.1sec]".
			/// </remarks>
			[ParameterTableRowAttribute("callHelp_MaxWaitTime", index: 37, minimum: 0, maximum: 255, step: 1, sortOrder: 10460, unknown2: 1)]
			[DisplayName("[Ssm => ss waiting time of maximum response. mSec]")]
			[Description("The maximum waiting time at the first goal of the response goal [101 => 10.1sec]")]
			[DefaultValue((Byte)0)]
			public Byte CallHelp_MaxWaitTime {
				get { return callHelp_MaxWaitTime; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for CallHelp_MaxWaitTime.");
					SetProperty(ref callHelp_MaxWaitTime, ref value, CallHelp_MaxWaitTimeProperty);
				}
			}

			/// <summary>Action goal : alert status</summary>
			/// <remarks>
			/// Japanese short name: "ゴールアクション：警戒状態", Google translated: "Action goal : alert status".
			/// Japanese description: "ゴールアクション：ターゲットが警戒状態になった", Google translated: "Goal action : target becomes alert state".
			/// </remarks>
			[ParameterTableRowAttribute("goalAction_ToCaution", index: 38, minimum: 0, maximum: 5, step: 1, sortOrder: 3200, unknown2: 1)]
			[DisplayName("Action goal : alert status")]
			[Description("Goal action : target becomes alert state")]
			[DefaultValue((NpcThoughtGoalAction)0)]
			public NpcThoughtGoalAction GoalAction_ToCaution {
				get { return goalAction_ToCaution; }
				set { SetProperty(ref goalAction_ToCaution, ref value, GoalAction_ToCautionProperty); }
			}

			/// <summary>Action goal : finding state</summary>
			/// <remarks>
			/// Japanese short name: "ゴールアクション：発見状態", Google translated: "Action goal : finding state".
			/// Japanese description: "ゴールアクション：ターゲットが発見状態になった", Google translated: "Goal action : target becomes the discovery state".
			/// </remarks>
			[ParameterTableRowAttribute("goalAction_ToFind", index: 39, minimum: 0, maximum: 5, step: 1, sortOrder: 3600, unknown2: 1)]
			[DisplayName("Action goal : finding state")]
			[Description("Goal action : target becomes the discovery state")]
			[DefaultValue((NpcThoughtGoalAction)0)]
			public NpcThoughtGoalAction GoalAction_ToFind {
				get { return goalAction_ToFind; }
				set { SetProperty(ref goalAction_ToFind, ref value, GoalAction_ToFindProperty); }
			}

			/// <summary>Action type of response after call fellow</summary>
			/// <remarks>
			/// Japanese short name: "仲間呼び 応答後の行動タイプ", Google translated: "Action type of response after call fellow".
			/// Japanese description: "応答後、目標位置までの行動タイプ", Google translated: "After the response , type of behavior to the target position".
			/// </remarks>
			[ParameterTableRowAttribute("callHelp_ReplyBehaviorType", index: 40, minimum: 0, maximum: 3, step: 1, sortOrder: 10200, unknown2: 1)]
			[DisplayName("Action type of response after call fellow")]
			[Description("After the response , type of behavior to the target position")]
			[DefaultValue((NpcThoughtReplyBehavior)0)]
			public NpcThoughtReplyBehavior CallHelp_ReplyBehaviorType {
				get { return callHelp_ReplyBehaviorType; }
				set { SetProperty(ref callHelp_ReplyBehaviorType, ref value, CallHelp_ReplyBehaviorTypeProperty); }
			}

			/// <summary>Do not pass move</summary>
			/// <remarks>
			/// Japanese short name: "パス移動しない", Google translated: "Do not pass move".
			/// Japanese description: "パス移動命令が来てもパスを辿らずに直接移動するか", Google translated: "You can go directly to not follow the path path movement command is also coming".
			/// </remarks>
			[ParameterTableRowAttribute("disablePathMove", index: 41, minimum: 0, maximum: 1, step: 1, sortOrder: 300, unknown2: 1)]
			[DisplayName("Do not pass move")]
			[Description("You can go directly to not follow the path path movement command is also coming")]
			[DefaultValue((Byte)0)]
			public Byte DisablePathMove {
				get { return disablePathMove; }
				set {
					if ((double)value < 0 || (double)value > 1)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1 for DisablePathMove.");
					SetProperty(ref disablePathMove, ref value, DisablePathMoveProperty);
				}
			}

			/// <summary>You can skip the arrival decision by line-of-sight ?</summary>
			/// <remarks>
			/// Japanese short name: "視線による到着判定をスキップするか？", Google translated: "You can skip the arrival decision by line-of-sight ?".
			/// Japanese description: "視線による到着判定をスキップするか？Onにすると、視線が通っていなくても、到着判定を行う。", Google translated: "You can skip the arrival decision by line-of-sight ? If this is On, the line of sight may not be through , to perform the arrival determination .".
			/// </remarks>
			[ParameterTableRowAttribute("skipArrivalVisibleCheck", index: 42, minimum: 0, maximum: 1, step: 1, sortOrder: 600, unknown2: 1)]
			[DisplayName("You can skip the arrival decision by line-of-sight ?")]
			[Description("You can skip the arrival decision by line-of-sight ? If this is On, the line of sight may not be through , to perform the arrival determination .")]
			[DefaultValue(false)]
			public bool SkipArrivalVisibleCheck {
				get { return skipArrivalVisibleCheck; }
				set { SetProperty(ref skipArrivalVisibleCheck, ref value, SkipArrivalVisibleCheckProperty); }
			}

			/// <summary>Become surrounds the role ?</summary>
			/// <remarks>
			/// Japanese short name: "取巻き役になるか？", Google translated: "Become surrounds the role ?".
			/// Japanese description: "思考属性：ＯＮにすると取巻き役を演じます。", Google translated: "It plays a role surrounds If you ON: thinking attribute .".
			/// </remarks>
			[ParameterTableRowAttribute("thinkAttr_doAdmirer", index: 43, minimum: 0, maximum: 1, step: 1, sortOrder: 5200, unknown2: 1)]
			[DisplayName("Become surrounds the role ?")]
			[Description("It plays a role surrounds If you ON: thinking attribute .")]
			[DefaultValue(false)]
			public bool ThinkAttr_doAdmirer {
				get { return thinkAttr_doAdmirer; }
				set { SetProperty(ref thinkAttr_doAdmirer, ref value, ThinkAttr_doAdmirerProperty); }
			}

			/// <summary>Flag " cliff " or pass along ?</summary>
			/// <remarks>
			/// Japanese short name: "フラグ「崖」通れるか？", Google translated: "Flag " cliff " or pass along ?".
			/// Japanese description: "ノード「崖」を通過できるか？(def:1)", Google translated: "Is it possible to pass through the node " cliff " ? (def: 1)".
			/// </remarks>
			[ParameterTableRowAttribute("enableNaviFlg_Edge:1", index: 44, minimum: 0, maximum: 1, step: 1, sortOrder: 10600, unknown2: 1)]
			[DisplayName("Flag \" cliff \" or pass along ?")]
			[Description("Is it possible to pass through the node \" cliff \" ? (def: 1)")]
			[DefaultValue(true)]
			public Boolean EnableNaviFlg_Edge {
				get { return GetBitProperty(0, 1, EnableNaviFlg_EdgeProperty) != 0; }
				set { SetBitProperty(0, 1, value ? 1 : 0, EnableNaviFlg_EdgeProperty); }
			}

			/// <summary>Flag " wide " or pass along ?</summary>
			/// <remarks>
			/// Japanese short name: "フラグ「広い」通れるか？", Google translated: "Flag " wide " or pass along ?".
			/// Japanese description: "ノード「広い」を通過できるか？(def:1)", Google translated: "Is it possible to pass through the node " wide " ? (def: 1)".
			/// </remarks>
			[ParameterTableRowAttribute("enableNaviFlg_LargeSpace:1", index: 45, minimum: 0, maximum: 1, step: 1, sortOrder: 10700, unknown2: 1)]
			[DisplayName("Flag \" wide \" or pass along ?")]
			[Description("Is it possible to pass through the node \" wide \" ? (def: 1)")]
			[DefaultValue(true)]
			public Boolean EnableNaviFlg_LargeSpace {
				get { return GetBitProperty(1, 1, EnableNaviFlg_LargeSpaceProperty) != 0; }
				set { SetBitProperty(1, 1, value ? 1 : 0, EnableNaviFlg_LargeSpaceProperty); }
			}

			/// <summary>Flag " ladder " or pass along ?</summary>
			/// <remarks>
			/// Japanese short name: "フラグ「梯子」通れるか？", Google translated: "Flag " ladder " or pass along ?".
			/// Japanese description: "ノード「梯子」を通過できるか？(def:0)", Google translated: "Is it possible to pass through the node " ladder " ? (def: 0)".
			/// </remarks>
			[ParameterTableRowAttribute("enableNaviFlg_Ladder:1", index: 46, minimum: 0, maximum: 1, step: 1, sortOrder: 10800, unknown2: 1)]
			[DisplayName("Flag \" ladder \" or pass along ?")]
			[Description("Is it possible to pass through the node \" ladder \" ? (def: 0)")]
			[DefaultValue(false)]
			public Boolean EnableNaviFlg_Ladder {
				get { return GetBitProperty(2, 1, EnableNaviFlg_LadderProperty) != 0; }
				set { SetBitProperty(2, 1, value ? 1 : 0, EnableNaviFlg_LadderProperty); }
			}

			/// <summary>Flag or pass along "hole" ?</summary>
			/// <remarks>
			/// Japanese short name: "フラグ「穴」通れるか？", Google translated: "Flag or pass along "hole" ?".
			/// Japanese description: "ノード「穴」を通過できるか？(def:0)", Google translated: "Is it possible to pass through the node "hole" ? (def: 0)".
			/// </remarks>
			[ParameterTableRowAttribute("enableNaviFlg_Hole:1", index: 47, minimum: 0, maximum: 1, step: 1, sortOrder: 10900, unknown2: 1)]
			[DisplayName("Flag or pass along \"hole\" ?")]
			[Description("Is it possible to pass through the node \"hole\" ? (def: 0)")]
			[DefaultValue(false)]
			public Boolean EnableNaviFlg_Hole {
				get { return GetBitProperty(3, 1, EnableNaviFlg_HoleProperty) != 0; }
				set { SetBitProperty(3, 1, value ? 1 : 0, EnableNaviFlg_HoleProperty); }
			}

			/// <summary>Flag " door " or pass along ?</summary>
			/// <remarks>
			/// Japanese short name: "フラグ「扉」通れるか？", Google translated: "Flag " door " or pass along ?".
			/// Japanese description: "ノード「扉」を通過できるか？(def:0)", Google translated: "Is it possible to pass through the node " door " ? (def: 0)".
			/// </remarks>
			[ParameterTableRowAttribute("enableNaviFlg_Door:1", index: 48, minimum: 0, maximum: 1, step: 1, sortOrder: 11000, unknown2: 1)]
			[DisplayName("Flag \" door \" or pass along ?")]
			[Description("Is it possible to pass through the node \" door \" ? (def: 0)")]
			[DefaultValue(false)]
			public Boolean EnableNaviFlg_Door {
				get { return GetBitProperty(4, 1, EnableNaviFlg_DoorProperty) != 0; }
				set { SetBitProperty(4, 1, value ? 1 : 0, EnableNaviFlg_DoorProperty); }
			}

			/// <summary>Flag " wall " or pass along ?</summary>
			/// <remarks>
			/// Japanese short name: "フラグ「壁中」通れるか？", Google translated: "Flag " wall " or pass along ?".
			/// Japanese description: "ノード「壁中」を通過できるか？(def:0)", Google translated: "Is it possible to pass through the node " wall " ? (def: 0)".
			/// </remarks>
			[ParameterTableRowAttribute("enableNaviFlg_InSideWall:1", index: 49, minimum: 0, maximum: 1, step: 1, sortOrder: 12000, unknown2: 1)]
			[DisplayName("Flag \" wall \" or pass along ?")]
			[Description("Is it possible to pass through the node \" wall \" ? (def: 0)")]
			[DefaultValue(false)]
			public Boolean EnableNaviFlg_InSideWall {
				get { return GetBitProperty(5, 1, EnableNaviFlg_InSideWallProperty) != 0; }
				set { SetBitProperty(5, 1, value ? 1 : 0, EnableNaviFlg_InSideWallProperty); }
			}

			/// <summary>Reservations really</summary>
			/// <remarks>
			/// Japanese short name: "ほんとに予約", Google translated: "Reservations really".
			/// Japanese description: "フラグが新しく必要になったらここにいれます（NotPadding)", Google translated: "Flag when you need a new one you put in here (NotPadding)".
			/// </remarks>
			[ParameterTableRowAttribute("enableNaviFlg_reserve0:2", index: 50, minimum: 0, maximum: 0, step: 0, sortOrder: 13000, unknown2: 1)]
			[DisplayName("Reservations really")]
			[Description("Flag when you need a new one you put in here (NotPadding)")]
			[DefaultValue((Byte)0)]
			[Browsable(false)]
			public Byte EnableNaviFlg_reserve0 {
				get { return (Byte)GetBitProperty(6, 2, EnableNaviFlg_reserve0Property); }
				set {
					if ((double)value < 0 || (double)value > 0)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 0 for EnableNaviFlg_reserve0.");
					SetBitProperty(6, 2, (int)value, EnableNaviFlg_reserve0Property);
				}
			}

			/// <summary>Reservations really</summary>
			/// <remarks>
			/// Japanese short name: "ほんとに予約", Google translated: "Reservations really".
			/// Japanese description: "フラグが新しく必要になったらここにいれます（NotPadding)", Google translated: "Flag when you need a new one you put in here (NotPadding)".
			/// </remarks>
			[ParameterTableRowAttribute("enableNaviFlg_reserve1[3]", index: 51, minimum: 0, maximum: 0, step: 0, sortOrder: 14000, unknown2: 0)]
			[DisplayName("Reservations really")]
			[Description("Flag when you need a new one you put in here (NotPadding)")]
			[Browsable(false)]
			public Byte[] EnableNaviFlg_reserve1 {
				get { return enableNaviFlg_reserve1; }
				set { SetProperty(ref enableNaviFlg_reserve1, ref value, EnableNaviFlg_reserve1Property); }
			}

			/// <summary>Pad</summary>
			/// <remarks>
			/// Japanese short name: "パッド", Google translated: "Pad".
			/// Japanese description: "pad", Google translated: "pad".
			/// </remarks>
			[ParameterTableRowAttribute("pad0[12]", index: 52, minimum: 0, maximum: 0, step: 0, sortOrder: 14001, unknown2: 0)]
			[DisplayName("Pad")]
			[Description("pad")]
			[Browsable(false)]
			public Byte[] Pad0 {
				get { return pad0; }
				set { SetProperty(ref pad0, ref value, Pad0Property); }
			}

			internal NpcThink(ParameterTable table, int index, AssetLoader loader, int next)
				: base(table, index, loader) {
				BinaryReader reader = loader.Reader;

				LogicId = reader.ReadInt32();
				BattleGoalID = reader.ReadInt32();
				NearDist = reader.ReadSingle();
				MidDist = reader.ReadSingle();
				FarDist = reader.ReadSingle();
				OutDist = reader.ReadSingle();
				BackHomeLife_OnHitEneWal = reader.ReadSingle();
				GoalID_ToCaution = reader.ReadInt32();
				IdAttackCannotMove = reader.ReadInt32();
				GoalID_ToFind = reader.ReadInt32();
				CallHelp_ActionAnimId = reader.ReadInt32();
				CallHelp_CallActionId = reader.ReadInt32();
				Eye_dist = reader.ReadUInt16();
				Ear_dist = reader.ReadUInt16();
				Ear_soundcut_dist = reader.ReadUInt16();
				Nose_dist = reader.ReadUInt16();
				MaxBackhomeDist = reader.ReadUInt16();
				BackhomeDist = reader.ReadUInt16();
				BackhomeBattleDist = reader.ReadUInt16();
				NonBattleActLife = reader.ReadUInt16();
				BackHome_LookTargetTime = reader.ReadUInt16();
				BackHome_LookTargetDist = reader.ReadUInt16();
				SightTargetForgetTime = reader.ReadUInt16();
				SoundTargetForgetTime = reader.ReadUInt16();
				BattleStartDist = reader.ReadUInt16();
				CallHelp_MyPeerId = reader.ReadUInt16();
				CallHelp_CallPeerId = reader.ReadUInt16();
				TargetSys_DmgEffectRate = reader.ReadUInt16();
				TeamAttackEffectivity = reader.ReadByte();
				Eye_angX = reader.ReadByte();
				Eye_angY = reader.ReadByte();
				Ear_angX = reader.ReadByte();
				Ear_angY = reader.ReadByte();
				CallHelp_CallValidMinDistTarget = reader.ReadByte();
				CallHelp_CallValidRange = reader.ReadByte();
				CallHelp_ForgetTimeByArrival = reader.ReadByte();
				CallHelp_MinWaitTime = reader.ReadByte();
				CallHelp_MaxWaitTime = reader.ReadByte();
				GoalAction_ToCaution = (NpcThoughtGoalAction)reader.ReadByte();
				GoalAction_ToFind = (NpcThoughtGoalAction)reader.ReadByte();
				CallHelp_ReplyBehaviorType = (NpcThoughtReplyBehavior)reader.ReadByte();
				DisablePathMove = reader.ReadByte();
				SkipArrivalVisibleCheck = reader.ReadByte() != 0;
				ThinkAttr_doAdmirer = reader.ReadByte() != 0;
				BitFields = reader.ReadBytes(1);
				EnableNaviFlg_reserve1 = reader.ReadBytes(3);
				Pad0 = reader.ReadBytes(12);
			}

			internal NpcThink(ParameterTable table, int index)
				: base(table, index) {
				BitFields = new byte[1];
				LogicId = (Int32)(-1);
				BattleGoalID = (Int32)(-1);
				NearDist = (Single)0;
				MidDist = (Single)0;
				FarDist = (Single)0;
				OutDist = (Single)0;
				BackHomeLife_OnHitEneWal = (Single)5;
				GoalID_ToCaution = (Int32)0;
				IdAttackCannotMove = (Int32)0;
				GoalID_ToFind = (Int32)0;
				CallHelp_ActionAnimId = (Int32)(-1);
				CallHelp_CallActionId = (Int32)(-1);
				Eye_dist = (UInt16)0;
				Ear_dist = (UInt16)0;
				Ear_soundcut_dist = (UInt16)0;
				Nose_dist = (UInt16)0;
				MaxBackhomeDist = (UInt16)0;
				BackhomeDist = (UInt16)0;
				BackhomeBattleDist = (UInt16)0;
				NonBattleActLife = (UInt16)0;
				BackHome_LookTargetTime = (UInt16)3;
				BackHome_LookTargetDist = (UInt16)10;
				SightTargetForgetTime = (UInt16)600;
				SoundTargetForgetTime = (UInt16)150;
				BattleStartDist = (UInt16)0;
				CallHelp_MyPeerId = (UInt16)0;
				CallHelp_CallPeerId = (UInt16)0;
				TargetSys_DmgEffectRate = (UInt16)100;
				TeamAttackEffectivity = (Byte)25;
				Eye_angX = (Byte)0;
				Eye_angY = (Byte)0;
				Ear_angX = (Byte)90;
				Ear_angY = (Byte)180;
				CallHelp_CallValidMinDistTarget = (Byte)5;
				CallHelp_CallValidRange = (Byte)15;
				CallHelp_ForgetTimeByArrival = (Byte)0;
				CallHelp_MinWaitTime = (Byte)0;
				CallHelp_MaxWaitTime = (Byte)0;
				GoalAction_ToCaution = (NpcThoughtGoalAction)0;
				GoalAction_ToFind = (NpcThoughtGoalAction)0;
				CallHelp_ReplyBehaviorType = (NpcThoughtReplyBehavior)0;
				DisablePathMove = (Byte)0;
				SkipArrivalVisibleCheck = false;
				ThinkAttr_doAdmirer = false;
				EnableNaviFlg_Edge = true;
				EnableNaviFlg_LargeSpace = true;
				EnableNaviFlg_Ladder = false;
				EnableNaviFlg_Hole = false;
				EnableNaviFlg_Door = false;
				EnableNaviFlg_InSideWall = false;
				EnableNaviFlg_reserve0 = (Byte)0;
				EnableNaviFlg_reserve1 = new Byte[3];
				Pad0 = new Byte[12];
			}

			/// <summary>
			/// Write the <see cref="NpcThink"/> row to the stream.
			/// </summary>
			/// <param name="writer"></param>
			public override void Write(BinaryWriter writer) {
				writer.Write(LogicId);
				writer.Write(BattleGoalID);
				writer.Write(NearDist);
				writer.Write(MidDist);
				writer.Write(FarDist);
				writer.Write(OutDist);
				writer.Write(BackHomeLife_OnHitEneWal);
				writer.Write(GoalID_ToCaution);
				writer.Write(IdAttackCannotMove);
				writer.Write(GoalID_ToFind);
				writer.Write(CallHelp_ActionAnimId);
				writer.Write(CallHelp_CallActionId);
				writer.Write(Eye_dist);
				writer.Write(Ear_dist);
				writer.Write(Ear_soundcut_dist);
				writer.Write(Nose_dist);
				writer.Write(MaxBackhomeDist);
				writer.Write(BackhomeDist);
				writer.Write(BackhomeBattleDist);
				writer.Write(NonBattleActLife);
				writer.Write(BackHome_LookTargetTime);
				writer.Write(BackHome_LookTargetDist);
				writer.Write(SightTargetForgetTime);
				writer.Write(SoundTargetForgetTime);
				writer.Write(BattleStartDist);
				writer.Write(CallHelp_MyPeerId);
				writer.Write(CallHelp_CallPeerId);
				writer.Write(TargetSys_DmgEffectRate);
				writer.Write(TeamAttackEffectivity);
				writer.Write(Eye_angX);
				writer.Write(Eye_angY);
				writer.Write(Ear_angX);
				writer.Write(Ear_angY);
				writer.Write(CallHelp_CallValidMinDistTarget);
				writer.Write(CallHelp_CallValidRange);
				writer.Write(CallHelp_ForgetTimeByArrival);
				writer.Write(CallHelp_MinWaitTime);
				writer.Write(CallHelp_MaxWaitTime);
				writer.Write((Byte)GoalAction_ToCaution);
				writer.Write((Byte)GoalAction_ToFind);
				writer.Write((Byte)CallHelp_ReplyBehaviorType);
				writer.Write(DisablePathMove);
				writer.Write((Byte)(SkipArrivalVisibleCheck ? 1 : 0));
				writer.Write((Byte)(ThinkAttr_doAdmirer ? 1 : 0));
				writer.Write(BitFields);
				writer.Write(EnableNaviFlg_reserve1);
				writer.Write(Pad0);
			}
		}
	}
}
