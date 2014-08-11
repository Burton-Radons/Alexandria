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
		/// Defined as "OBJ_ACT_PARAM_ST" in Dark Souls in the file "ObjActParam.paramdef" (id 2Fh).
		/// </remarks>
		public class ObjectAction : ParameterTableRow {
			/// <summary>The name of the table in the file.</summary>
			public const string TableName = "OBJ_ACT_PARAM_ST";

			Int32 actionEnableMsgId, actionFailedMsgId, spQualifiedPassEventFlag;
			UInt16 validDist, playerAnimId, chrAnimId, spQualifiedId, spQualifiedId2;
			Byte objDummyId, objAnimId, validPlayerAngle, validObjAngle;
			ObjectActionSpecialQualifiedType spQualifiedType, spQualifiedType2;
			ObjectActionCharacterSOrbType chrSorbType;
			ObjectActionEventKickTiming eventKickTiming;
			Byte[] pad1;

			/// <summary>A property in the class.</summary>
			public static readonly PropertyInfo
				ActionEnableMsgIdProperty = GetProperty<ObjectAction>("ActionEnableMsgId"),
				ActionFailedMsgIdProperty = GetProperty<ObjectAction>("ActionFailedMsgId"),
				SpQualifiedPassEventFlagProperty = GetProperty<ObjectAction>("SpQualifiedPassEventFlag"),
				ValidDistProperty = GetProperty<ObjectAction>("ValidDist"),
				PlayerAnimIdProperty = GetProperty<ObjectAction>("PlayerAnimId"),
				ChrAnimIdProperty = GetProperty<ObjectAction>("ChrAnimId"),
				SpQualifiedIdProperty = GetProperty<ObjectAction>("SpQualifiedId"),
				SpQualifiedId2Property = GetProperty<ObjectAction>("SpQualifiedId2"),
				ObjDummyIdProperty = GetProperty<ObjectAction>("ObjDummyId"),
				ObjAnimIdProperty = GetProperty<ObjectAction>("ObjAnimId"),
				ValidPlayerAngleProperty = GetProperty<ObjectAction>("ValidPlayerAngle"),
				SpQualifiedTypeProperty = GetProperty<ObjectAction>("SpQualifiedType"),
				SpQualifiedType2Property = GetProperty<ObjectAction>("SpQualifiedType2"),
				ValidObjAngleProperty = GetProperty<ObjectAction>("ValidObjAngle"),
				ChrSorbTypeProperty = GetProperty<ObjectAction>("ChrSorbType"),
				EventKickTimingProperty = GetProperty<ObjectAction>("EventKickTiming"),
				Pad1Property = GetProperty<ObjectAction>("Pad1");

			/// <summary>MsgID of action is enabled</summary>
			/// <remarks>
			/// Japanese short name: "アクション有効時のMsgID", Google translated: "MsgID of action is enabled".
			/// Japanese description: "アクションが有効時に表示するメニューのMsgIDです。", Google translated: "It is MsgID of menu where the action is to display when enabled .".
			/// </remarks>
			[ParameterTableRowAttribute("actionEnableMsgId", index: 0, minimum: -1, maximum: 1E+08, step: 1, sortOrder: 9000, unknown2: 1)]
			[DisplayName("MsgID of action is enabled")]
			[Description("It is MsgID of menu where the action is to display when enabled .")]
			[DefaultValue((Int32)(-1))]
			public Int32 ActionEnableMsgId {
				get { return actionEnableMsgId; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for ActionEnableMsgId.");
					SetProperty(ref actionEnableMsgId, ref value, ActionEnableMsgIdProperty);
				}
			}

			/// <summary>MsgID of action failure</summary>
			/// <remarks>
			/// Japanese short name: "アクション失敗時のMsgID", Google translated: "MsgID of action failure".
			/// Japanese description: "アクションが失敗時に表示するメニューのMsgIDです。", Google translated: "It is MsgID of menu where the action is to show on failure .".
			/// </remarks>
			[ParameterTableRowAttribute("actionFailedMsgId", index: 1, minimum: -1, maximum: 1E+08, step: 1, sortOrder: 10000, unknown2: 1)]
			[DisplayName("MsgID of action failure")]
			[Description("It is MsgID of menu where the action is to show on failure .")]
			[DefaultValue((Int32)(-1))]
			public Int32 ActionFailedMsgId {
				get { return actionFailedMsgId; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for ActionFailedMsgId.");
					SetProperty(ref actionFailedMsgId, ref value, ActionFailedMsgIdProperty);
				}
			}

			/// <summary>Special conditions path for event flag</summary>
			/// <remarks>
			/// Japanese short name: "特殊条件パス用イベントフラグ", Google translated: "Special conditions path for event flag".
			/// Japanese description: "特殊条件を無条件パスするためのイベントフラグ.", Google translated: "Event flag to unconditionally pass the special conditions .".
			/// </remarks>
			[ParameterTableRowAttribute("spQualifiedPassEventFlag", index: 2, minimum: -1, maximum: 1E+08, step: 1, sortOrder: 8500, unknown2: 1)]
			[DisplayName("Special conditions path for event flag")]
			[Description("Event flag to unconditionally pass the special conditions .")]
			[DefaultValue((Int32)(-1))]
			public Int32 SpQualifiedPassEventFlag {
				get { return spQualifiedPassEventFlag; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for SpQualifiedPassEventFlag.");
					SetProperty(ref spQualifiedPassEventFlag, ref value, SpQualifiedPassEventFlagProperty);
				}
			}

			/// <summary>The effective range of action [cm]</summary>
			/// <remarks>
			/// Japanese short name: "アクションの有効距離[cm]", Google translated: "The effective range of action [cm]".
			/// Japanese description: "アクションの有効距離です。", Google translated: "It is the effective distance of the action .".
			/// </remarks>
			[ParameterTableRowAttribute("validDist", index: 3, minimum: 0, maximum: 60000, step: 1, sortOrder: 3000, unknown2: 1)]
			[DisplayName("The effective range of action [cm]")]
			[Description("It is the effective distance of the action .")]
			[DefaultValue((UInt16)150)]
			public UInt16 ValidDist {
				get { return validDist; }
				set {
					if ((double)value < 0 || (double)value > 60000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 60000 for ValidDist.");
					SetProperty(ref validDist, ref value, ValidDistProperty);
				}
			}

			/// <summary>Anime ID0 of the player</summary>
			/// <remarks>
			/// Japanese short name: "プレイヤのアニメID0", Google translated: "Anime ID0 of the player".
			/// Japanese description: "プレイヤーキャラのアクション時のアニメIDです。", Google translated: "This animation ID of the action when the player character .".
			/// </remarks>
			[ParameterTableRowAttribute("playerAnimId", index: 4, minimum: 0, maximum: 60000, step: 1, sortOrder: 5000, unknown2: 1)]
			[DisplayName("Anime ID0 of the player")]
			[Description("This animation ID of the action when the player character .")]
			[DefaultValue((UInt16)0)]
			public UInt16 PlayerAnimId {
				get { return playerAnimId; }
				set {
					if ((double)value < 0 || (double)value > 60000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 60000 for PlayerAnimId.");
					SetProperty(ref playerAnimId, ref value, PlayerAnimIdProperty);
				}
			}

			/// <summary>ID0 of anime characters</summary>
			/// <remarks>
			/// Japanese short name: "キャラのアニメID0", Google translated: "ID0 of anime characters".
			/// Japanese description: "敵などのアクション時のアニメID", Google translated: "Anime ID of the action , such as when enemy".
			/// </remarks>
			[ParameterTableRowAttribute("chrAnimId", index: 5, minimum: 0, maximum: 60000, step: 1, sortOrder: 6000, unknown2: 1)]
			[DisplayName("ID0 of anime characters")]
			[Description("Anime ID of the action , such as when enemy")]
			[DefaultValue((UInt16)0)]
			public UInt16 ChrAnimId {
				get { return chrAnimId; }
				set {
					if ((double)value < 0 || (double)value > 60000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 60000 for ChrAnimId.");
					SetProperty(ref chrAnimId, ref value, ChrAnimIdProperty);
				}
			}

			/// <summary>ID of the special conditions</summary>
			/// <remarks>
			/// Japanese short name: "特殊条件のID", Google translated: "ID of the special conditions".
			/// Japanese description: "特殊条件のID", Google translated: "ID of the special conditions".
			/// </remarks>
			[ParameterTableRowAttribute("spQualifiedId", index: 6, minimum: 0, maximum: 9999, step: 1, sortOrder: 8000, unknown2: 1)]
			[DisplayName("ID of the special conditions")]
			[Description("ID of the special conditions")]
			[DefaultValue((UInt16)0)]
			public UInt16 SpQualifiedId {
				get { return spQualifiedId; }
				set {
					if ((double)value < 0 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9999 for SpQualifiedId.");
					SetProperty(ref spQualifiedId, ref value, SpQualifiedIdProperty);
				}
			}

			/// <summary>ID 2 of the special conditions</summary>
			/// <remarks>
			/// Japanese short name: "特殊条件のID 2", Google translated: "ID 2 of the special conditions".
			/// Japanese description: "特殊条件のIDその2", Google translated: "2 the ID of the special conditions".
			/// </remarks>
			[ParameterTableRowAttribute("spQualifiedId2", index: 7, minimum: 0, maximum: 9999, step: 1, sortOrder: 8011, unknown2: 1)]
			[DisplayName("ID 2 of the special conditions")]
			[Description("2 the ID of the special conditions")]
			[DefaultValue((UInt16)0)]
			public UInt16 SpQualifiedId2 {
				get { return spQualifiedId2; }
				set {
					if ((double)value < 0 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9999 for SpQualifiedId2.");
					SetProperty(ref spQualifiedId2, ref value, SpQualifiedId2Property);
				}
			}

			/// <summary>Damipori ID0 of objects</summary>
			/// <remarks>
			/// Japanese short name: "オブジェのダミポリID0", Google translated: "Damipori ID0 of objects".
			/// Japanese description: "オブジェクトのアクション位置となるダミポリIDです", Google translated: "It is Damipori ID that is action position of the object".
			/// </remarks>
			[ParameterTableRowAttribute("objDummyId", index: 8, minimum: 0, maximum: 255, step: 1, sortOrder: 1000, unknown2: 1)]
			[DisplayName("Damipori ID0 of objects")]
			[Description("It is Damipori ID that is action position of the object")]
			[DefaultValue((Byte)0)]
			public Byte ObjDummyId {
				get { return objDummyId; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ObjDummyId.");
					SetProperty(ref objDummyId, ref value, ObjDummyIdProperty);
				}
			}

			/// <summary>Anime ID0 of objects</summary>
			/// <remarks>
			/// Japanese short name: "オブジェのアニメID0", Google translated: "Anime ID0 of objects".
			/// Japanese description: "オブジェクトのアクション時のアニメＩＤです。", Google translated: "This animation ID of the action when the object .".
			/// </remarks>
			[ParameterTableRowAttribute("objAnimId", index: 9, minimum: 0, maximum: 255, step: 1, sortOrder: 2000, unknown2: 1)]
			[DisplayName("Anime ID0 of objects")]
			[Description("This animation ID of the action when the object .")]
			[DefaultValue((Byte)0)]
			public Byte ObjAnimId {
				get { return objAnimId; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ObjAnimId.");
					SetProperty(ref objAnimId, ref value, ObjAnimIdProperty);
				}
			}

			/// <summary>Action effective angle of the player</summary>
			/// <remarks>
			/// Japanese short name: "プレイヤのアクション有効角度", Google translated: "Action effective angle of the player".
			/// Japanese description: "プレイヤのアクションの有効角度です。プレイヤの向きベクトルとオブジェへの方向ベクトルの有効角度差", Google translated: "It is effective angle of action of the player . Effective angular difference of direction vector to the object and the direction vector of the player".
			/// </remarks>
			[ParameterTableRowAttribute("validPlayerAngle", index: 10, minimum: 0, maximum: 180, step: 1, sortOrder: 4000, unknown2: 1)]
			[DisplayName("Action effective angle of the player")]
			[Description("It is effective angle of action of the player . Effective angular difference of direction vector to the object and the direction vector of the player")]
			[DefaultValue((Byte)30)]
			public Byte ValidPlayerAngle {
				get { return validPlayerAngle; }
				set {
					if ((double)value < 0 || (double)value > 180)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 180 for ValidPlayerAngle.");
					SetProperty(ref validPlayerAngle, ref value, ValidPlayerAngleProperty);
				}
			}

			/// <summary>Type of special conditions</summary>
			/// <remarks>
			/// Japanese short name: "特殊条件のタイプ", Google translated: "Type of special conditions".
			/// Japanese description: "特殊条件の種類", Google translated: "Kind of special conditions".
			/// </remarks>
			[ParameterTableRowAttribute("spQualifiedType", index: 11, minimum: 0, maximum: 255, step: 1, sortOrder: 7000, unknown2: 1)]
			[DisplayName("Type of special conditions")]
			[Description("Kind of special conditions")]
			[DefaultValue((ObjectActionSpecialQualifiedType)0)]
			public ObjectActionSpecialQualifiedType SpQualifiedType {
				get { return spQualifiedType; }
				set { SetProperty(ref spQualifiedType, ref value, SpQualifiedTypeProperty); }
			}

			/// <summary>Type 2 of the special conditions</summary>
			/// <remarks>
			/// Japanese short name: "特殊条件のタイプ2", Google translated: "Type 2 of the special conditions".
			/// Japanese description: "特殊条件の種類2", Google translated: "Two types of special conditions".
			/// </remarks>
			[ParameterTableRowAttribute("spQualifiedType2", index: 12, minimum: 0, maximum: 255, step: 1, sortOrder: 8010, unknown2: 1)]
			[DisplayName("Type 2 of the special conditions")]
			[Description("Two types of special conditions")]
			[DefaultValue((ObjectActionSpecialQualifiedType)0)]
			public ObjectActionSpecialQualifiedType SpQualifiedType2 {
				get { return spQualifiedType2; }
				set { SetProperty(ref spQualifiedType2, ref value, SpQualifiedType2Property); }
			}

			/// <summary>Action effective angle of object</summary>
			/// <remarks>
			/// Japanese short name: "オブジェのアクション有効角度", Google translated: "Action effective angle of object".
			/// Japanese description: "オブジェのアクション有効角度です。オブジェのアクションベクトルとキャラベクトルの有効角度差", Google translated: "The action effective angle of objects . Effective angular difference of character and action vector vector of objects".
			/// </remarks>
			[ParameterTableRowAttribute("validObjAngle", index: 13, minimum: 0, maximum: 180, step: 1, sortOrder: 4500, unknown2: 1)]
			[DisplayName("Action effective angle of object")]
			[Description("The action effective angle of objects . Effective angular difference of character and action vector vector of objects")]
			[DefaultValue((Byte)30)]
			public Byte ValidObjAngle {
				get { return validObjAngle; }
				set {
					if ((double)value < 0 || (double)value > 180)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 180 for ValidObjAngle.");
					SetProperty(ref validObjAngle, ref value, ValidObjAngleProperty);
				}
			}

			/// <summary>Adsorption type of character</summary>
			/// <remarks>
			/// Japanese short name: "キャラの吸着タイプ", Google translated: "Adsorption type of character".
			/// Japanese description: "オブジェアクション時のキャラの吸着方法です", Google translated: "It is a method of adsorption character of object action during".
			/// </remarks>
			[ParameterTableRowAttribute("chrSorbType", index: 14, minimum: 0, maximum: 255, step: 1, sortOrder: 1500, unknown2: 1)]
			[DisplayName("Adsorption type of character")]
			[Description("It is a method of adsorption character of object action during")]
			[DefaultValue((ObjectActionCharacterSOrbType)0)]
			public ObjectActionCharacterSOrbType ChrSorbType {
				get { return chrSorbType; }
				set { SetProperty(ref chrSorbType, ref value, ChrSorbTypeProperty); }
			}

			/// <summary>Event trigger timing</summary>
			/// <remarks>
			/// Japanese short name: "イベント発動タイミング", Google translated: "Event trigger timing".
			/// Japanese description: "イベントの実行タイミング", Google translated: "Execution timing of events".
			/// </remarks>
			[ParameterTableRowAttribute("eventKickTiming", index: 15, minimum: 0, maximum: 255, step: 1, sortOrder: 900, unknown2: 1)]
			[DisplayName("Event trigger timing")]
			[Description("Execution timing of events")]
			[DefaultValue((ObjectActionEventKickTiming)0)]
			public ObjectActionEventKickTiming EventKickTiming {
				get { return eventKickTiming; }
				set { SetProperty(ref eventKickTiming, ref value, EventKickTimingProperty); }
			}

			/// <summary>pad1</summary>
			/// <remarks>
			/// Japanese short name: "pad1", Google translated: "pad1".
			/// Japanese description: "pad1", Google translated: "pad1".
			/// </remarks>
			[ParameterTableRowAttribute("pad1[2]", index: 16, minimum: 0, maximum: 0, step: 0, sortOrder: 99999999, unknown2: 0)]
			[DisplayName("pad1")]
			[Description("pad1")]
			[Browsable(false)]
			public Byte[] Pad1 {
				get { return pad1; }
				set { SetProperty(ref pad1, ref value, Pad1Property); }
			}

			internal ObjectAction(ParameterTable table, int index, AssetLoader loader, int next)
				: base(table, index, loader) {
				BinaryReader reader = loader.Reader;

				ActionEnableMsgId = reader.ReadInt32();
				ActionFailedMsgId = reader.ReadInt32();
				SpQualifiedPassEventFlag = reader.ReadInt32();
				ValidDist = reader.ReadUInt16();
				PlayerAnimId = reader.ReadUInt16();
				ChrAnimId = reader.ReadUInt16();
				SpQualifiedId = reader.ReadUInt16();
				SpQualifiedId2 = reader.ReadUInt16();
				ObjDummyId = reader.ReadByte();
				ObjAnimId = reader.ReadByte();
				ValidPlayerAngle = reader.ReadByte();
				SpQualifiedType = (ObjectActionSpecialQualifiedType)reader.ReadByte();
				SpQualifiedType2 = (ObjectActionSpecialQualifiedType)reader.ReadByte();
				ValidObjAngle = reader.ReadByte();
				ChrSorbType = (ObjectActionCharacterSOrbType)reader.ReadByte();
				EventKickTiming = (ObjectActionEventKickTiming)reader.ReadByte();
				Pad1 = reader.ReadBytes(2);
			}

			internal ObjectAction(ParameterTable table, int index)
				: base(table, index) {
				ActionEnableMsgId = (Int32)(-1);
				ActionFailedMsgId = (Int32)(-1);
				SpQualifiedPassEventFlag = (Int32)(-1);
				ValidDist = (UInt16)150;
				PlayerAnimId = (UInt16)0;
				ChrAnimId = (UInt16)0;
				SpQualifiedId = (UInt16)0;
				SpQualifiedId2 = (UInt16)0;
				ObjDummyId = (Byte)0;
				ObjAnimId = (Byte)0;
				ValidPlayerAngle = (Byte)30;
				SpQualifiedType = (ObjectActionSpecialQualifiedType)0;
				SpQualifiedType2 = (ObjectActionSpecialQualifiedType)0;
				ValidObjAngle = (Byte)30;
				ChrSorbType = (ObjectActionCharacterSOrbType)0;
				EventKickTiming = (ObjectActionEventKickTiming)0;
				Pad1 = new Byte[2];
			}

			/// <summary>
			/// Write the <see cref="ObjectAction"/> row.
			/// </summary>
			/// <param name="writer"></param>
			public override void Write(BinaryWriter writer) {
				writer.Write(ActionEnableMsgId);
				writer.Write(ActionFailedMsgId);
				writer.Write(SpQualifiedPassEventFlag);
				writer.Write(ValidDist);
				writer.Write(PlayerAnimId);
				writer.Write(ChrAnimId);
				writer.Write(SpQualifiedId);
				writer.Write(SpQualifiedId2);
				writer.Write(ObjDummyId);
				writer.Write(ObjAnimId);
				writer.Write(ValidPlayerAngle);
				writer.Write((Byte)SpQualifiedType);
				writer.Write((Byte)SpQualifiedType2);
				writer.Write(ValidObjAngle);
				writer.Write((Byte)ChrSorbType);
				writer.Write((Byte)EventKickTiming);
				writer.Write(Pad1);
			}
		}
	}
}
