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
		/// Defined as "TALK_PARAM_ST" in Dark Souls in the file "TalkParam.paramdef" (id 21h).
		/// </remarks>
		public class Talk : ParameterTableRow {
			public const string TableName = "TALK_PARAM_ST";

			Int32 msgId, voiceId, motionId, returnPos, reactionId, eventId;
			Byte isMotionLoop;
			Byte[] pad0;

			public static readonly PropertyInfo
				MsgIdProperty = GetProperty<Talk>("MsgId"),
				VoiceIdProperty = GetProperty<Talk>("VoiceId"),
				MotionIdProperty = GetProperty<Talk>("MotionId"),
				ReturnPosProperty = GetProperty<Talk>("ReturnPos"),
				ReactionIdProperty = GetProperty<Talk>("ReactionId"),
				EventIdProperty = GetProperty<Talk>("EventId"),
				IsMotionLoopProperty = GetProperty<Talk>("IsMotionLoop"),
				Pad0Property = GetProperty<Talk>("Pad0");

			/// <summary>Message ID</summary>
			/// <remarks>
			/// Japanese short name: "メッセージID", Google translated: "Message ID".
			/// Japanese description: "メッセージを指定->メニュー", Google translated: "Specifies the message -> menu".
			/// </remarks>
			[ParameterTableRowAttribute("msgId", index: 0, minimum: -1, maximum: 4.294967E+09, step: 1, order: 1, unknown2: 1)]
			[DisplayName("Message ID")]
			[Description("Specifies the message -> menu")]
			[DefaultValue((Int32)(-1))]
			public Int32 MsgId {
				get { return msgId; }
				set {
					if ((double)value < -1 || (double)value > 4.294967E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 4.294967E+09 for " + MsgIdProperty.Name + ".");
					SetProperty(ref msgId, ref value, MsgIdProperty);
				}
			}

			/// <summary>Voice ID</summary>
			/// <remarks>
			/// Japanese short name: "ボイスID", Google translated: "Voice ID".
			/// Japanese description: "ボイスを指定->サウンド", Google translated: "Specifies the voice -> Sound".
			/// </remarks>
			[ParameterTableRowAttribute("voiceId", index: 1, minimum: -1, maximum: 4.294967E+09, step: 1, order: 2, unknown2: 1)]
			[DisplayName("Voice ID")]
			[Description("Specifies the voice -> Sound")]
			[DefaultValue((Int32)(-1))]
			public Int32 VoiceId {
				get { return voiceId; }
				set {
					if ((double)value < -1 || (double)value > 4.294967E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 4.294967E+09 for " + VoiceIdProperty.Name + ".");
					SetProperty(ref voiceId, ref value, VoiceIdProperty);
				}
			}

			/// <summary>Motion ID</summary>
			/// <remarks>
			/// Japanese short name: "モーションID", Google translated: "Motion ID".
			/// Japanese description: "モーションを指定->キャラ", Google translated: "Specifies the motion -> Character".
			/// </remarks>
			[ParameterTableRowAttribute("motionId", index: 2, minimum: -1, maximum: 4.294967E+09, step: 1, order: 3, unknown2: 1)]
			[DisplayName("Motion ID")]
			[Description("Specifies the motion -> Character")]
			[DefaultValue((Int32)(-1))]
			public Int32 MotionId {
				get { return motionId; }
				set {
					if ((double)value < -1 || (double)value > 4.294967E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 4.294967E+09 for " + MotionIdProperty.Name + ".");
					SetProperty(ref motionId, ref value, MotionIdProperty);
				}
			}

			/// <summary>Return position</summary>
			/// <remarks>
			/// Japanese short name: "復帰位置", Google translated: "Return position".
			/// Japanese description: "復帰する会話の相対位置->会話", Google translated: "> Conversation - the relative position of the conversation to return".
			/// </remarks>
			[ParameterTableRowAttribute("returnPos", index: 3, minimum: -1, maximum: 4.294967E+09, step: 1, order: 4, unknown2: 1)]
			[DisplayName("Return position")]
			[Description("> Conversation - the relative position of the conversation to return")]
			[DefaultValue((Int32)(-1))]
			public Int32 ReturnPos {
				get { return returnPos; }
				set {
					if ((double)value < -1 || (double)value > 4.294967E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 4.294967E+09 for " + ReturnPosProperty.Name + ".");
					SetProperty(ref returnPos, ref value, ReturnPosProperty);
				}
			}

			/// <summary>Notes ID</summary>
			/// <remarks>
			/// Japanese short name: "リアクションID", Google translated: "Notes ID".
			/// Japanese description: "復帰時の会話指定->会話", Google translated: "> Conversation - conversation specify when returning".
			/// </remarks>
			[ParameterTableRowAttribute("reactionId", index: 4, minimum: -1, maximum: 4.294967E+09, step: 1, order: 5, unknown2: 1)]
			[DisplayName("Notes ID")]
			[Description("> Conversation - conversation specify when returning")]
			[DefaultValue((Int32)(-1))]
			public Int32 ReactionId {
				get { return reactionId; }
				set {
					if ((double)value < -1 || (double)value > 4.294967E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 4.294967E+09 for " + ReactionIdProperty.Name + ".");
					SetProperty(ref reactionId, ref value, ReactionIdProperty);
				}
			}

			/// <summary>Event ID</summary>
			/// <remarks>
			/// Japanese short name: "イベントID", Google translated: "Event ID".
			/// Japanese description: "イベントID->イベント", Google translated: "Event ID-> events".
			/// </remarks>
			[ParameterTableRowAttribute("eventId", index: 5, minimum: -1, maximum: 4.294967E+09, step: 1, order: 6, unknown2: 1)]
			[DisplayName("Event ID")]
			[Description("Event ID-> events")]
			[DefaultValue((Int32)(-1))]
			public Int32 EventId {
				get { return eventId; }
				set {
					if ((double)value < -1 || (double)value > 4.294967E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 4.294967E+09 for " + EventIdProperty.Name + ".");
					SetProperty(ref eventId, ref value, EventIdProperty);
				}
			}

			/// <summary>Motion loop effective</summary>
			/// <remarks>
			/// Japanese short name: "モーションループ有効", Google translated: "Motion loop effective".
			/// Japanese description: "モーションループするか？", Google translated: "Or motion loop?".
			/// </remarks>
			[ParameterTableRowAttribute("isMotionLoop", index: 6, minimum: 0, maximum: 1, step: 1, order: 7, unknown2: 1)]
			[DisplayName("Motion loop effective")]
			[Description("Or motion loop?")]
			[DefaultValue((Byte)0)]
			public Byte IsMotionLoop {
				get { return isMotionLoop; }
				set {
					if ((double)value < 0 || (double)value > 1)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1 for " + IsMotionLoopProperty.Name + ".");
					SetProperty(ref isMotionLoop, ref value, IsMotionLoopProperty);
				}
			}

			/// <summary>dummy</summary>
			/// <remarks>
			/// Japanese short name: "dummy", Google translated: "dummy".
			/// Japanese description: "padding", Google translated: "padding".
			/// </remarks>
			[ParameterTableRowAttribute("pad0[7]", index: 7, minimum: 0, maximum: 0, step: 0, order: 9, unknown2: 0)]
			[DisplayName("dummy")]
			[Description("padding")]
			[Browsable(false)]
			public Byte[] Pad0 {
				get { return pad0; }
				set { SetProperty(ref pad0, ref value, Pad0Property); }
			}

			internal Talk(ParameterTable table, int index, AssetLoader loader, int next)
				: base(table, index, loader) {
				BinaryReader reader = loader.Reader;

				MsgId = reader.ReadInt32();
				VoiceId = reader.ReadInt32();
				MotionId = reader.ReadInt32();
				ReturnPos = reader.ReadInt32();
				ReactionId = reader.ReadInt32();
				EventId = reader.ReadInt32();
				IsMotionLoop = reader.ReadByte();
				Pad0 = reader.ReadBytes(7);
			}

			internal Talk(ParameterTable table, int index)
				: base(table, index) {
				MsgId = (Int32)(-1);
				VoiceId = (Int32)(-1);
				MotionId = (Int32)(-1);
				ReturnPos = (Int32)(-1);
				ReactionId = (Int32)(-1);
				EventId = (Int32)(-1);
				IsMotionLoop = (Byte)0;
				Pad0 = new Byte[7];
			}

			public override void Write(BinaryWriter writer) {
				writer.Write(MsgId);
				writer.Write(VoiceId);
				writer.Write(MotionId);
				writer.Write(ReturnPos);
				writer.Write(ReactionId);
				writer.Write(EventId);
				writer.Write(IsMotionLoop);
				writer.Write(Pad0);
			}
		}
	}
}
