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
		/// <remarks>From BehaviorParam.paramdef (id 17h).</remarks>
		public class Behavior : ParameterTableRow {
			public const string TableName = "BEHAVIOR_PARAM_ST";

			Int32 variationId, behaviorJudgeId, refId, sfxVariationId, stamina, mp;
			Byte ezStateBehaviorType_old, heroPoint;
			BehaviorRefType refType;
			Byte[] pad0, pad1;
			BehaviorCategory category;

			public static readonly PropertyInfo
				VariationIdProperty = GetProperty<Behavior>("VariationId"),
				BehaviorJudgeIdProperty = GetProperty<Behavior>("BehaviorJudgeId"),
				EzStateBehaviorType_oldProperty = GetProperty<Behavior>("EzStateBehaviorType_old"),
				RefTypeProperty = GetProperty<Behavior>("RefType"),
				Pad0Property = GetProperty<Behavior>("Pad0"),
				RefIdProperty = GetProperty<Behavior>("RefId"),
				SfxVariationIdProperty = GetProperty<Behavior>("SfxVariationId"),
				StaminaProperty = GetProperty<Behavior>("Stamina"),
				MpProperty = GetProperty<Behavior>("Mp"),
				CategoryProperty = GetProperty<Behavior>("Category"),
				HeroPointProperty = GetProperty<Behavior>("HeroPoint"),
				Pad1Property = GetProperty<Behavior>("Pad1");

			/// <summary>Behavioral variation ID</summary>
			/// <remarks>
			/// Japanese short name: "行動バリエーションID", Google translated: "Behavioral variation ID".
			/// Japanese description: "攻撃パラメータ用のIDを算出する際に使用します。実機上では直接使用しません。", Google translated: "It is used when calculating the ID of the attack parameter. It is not used directly in the actual machine .".
			/// </remarks>
			[ParameterTableRowAttribute("variationId", index: 0, minimum: 0, maximum: 1E+09, step: 1, order: 1, unknown2: 1)]
			[DisplayName("Behavioral variation ID")]
			[Description("It is used when calculating the ID of the attack parameter. It is not used directly in the actual machine .")]
			[DefaultValue((Int32)0)]
			public Int32 VariationId {
				get { return variationId; }
				set {
					if ((double)value < 0 || (double)value > 1E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1E+09 for VariationId.");
					SetProperty(ref variationId, ref value, VariationIdProperty);
				}
			}

			/// <summary>Behavior determining ID</summary>
			/// <remarks>
			/// Japanese short name: "行動判定ID", Google translated: "Behavior determining ID".
			/// Japanese description: "攻撃パラメータ用のIDを算出する際に使用します。このIDはTimeActEditorで入力される行動判定IDと一致させます。実機上では直接使用しません。", Google translated: "It is used when calculating the ID of the attack parameter. This ID must match the behavior determining ID that is entered in the TimeActEditor. It is not used directly in the actual machine .".
			/// </remarks>
			[ParameterTableRowAttribute("behaviorJudgeId", index: 1, minimum: 0, maximum: 999, step: 1, order: 2, unknown2: 1)]
			[DisplayName("Behavior determining ID")]
			[Description("It is used when calculating the ID of the attack parameter. This ID must match the behavior determining ID that is entered in the TimeActEditor. It is not used directly in the actual machine .")]
			[DefaultValue((Int32)0)]
			public Int32 BehaviorJudgeId {
				get { return behaviorJudgeId; }
				set {
					if ((double)value < 0 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for BehaviorJudgeId.");
					SetProperty(ref behaviorJudgeId, ref value, BehaviorJudgeIdProperty);
				}
			}

			/// <summary>ID rules for</summary>
			/// <remarks>
			/// Japanese short name: "IDルール用", Google translated: "ID rules for".
			/// Japanese description: "ID算出ルール用", Google translated: "ID calculation rule for".
			/// </remarks>
			[ParameterTableRowAttribute("ezStateBehaviorType_old", index: 2, minimum: 0, maximum: 255, step: 1, order: 3, unknown2: 1)]
			[DisplayName("ID rules for")]
			[Description("ID calculation rule for")]
			[DefaultValue((Byte)0)]
			public Byte EzStateBehaviorType_old {
				get { return ezStateBehaviorType_old; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for EzStateBehaviorType_old.");
					SetProperty(ref ezStateBehaviorType_old, ref value, EzStateBehaviorType_oldProperty);
				}
			}

			/// <summary>Reference ID type</summary>
			/// <remarks>
			/// Japanese short name: "参照IDタイプ", Google translated: "Reference ID type".
			/// Japanese description: "参照IDを間違わないように指定.", Google translated: "Specifies not mistaken reference ID.".
			/// </remarks>
			[ParameterTableRowAttribute("refType", index: 3, minimum: 0, maximum: 255, step: 1, order: 4, unknown2: 1)]
			[DisplayName("Reference ID type")]
			[Description("Specifies not mistaken reference ID.")]
			[DefaultValue((BehaviorRefType)0)]
			public BehaviorRefType RefType {
				get { return refType; }
				set { SetProperty(ref refType, ref value, RefTypeProperty); }
			}

			/// <summary>Padding 0</summary>
			/// <remarks>
			/// Japanese short name: "パディング0", Google translated: "Padding 0".
			/// Japanese description: "パディング0.", Google translated: "Padding 0 .".
			/// </remarks>
			[ParameterTableRowAttribute("pad0[2]", index: 4, minimum: 0, maximum: 0, step: 0, order: 301, unknown2: 0)]
			[DisplayName("Padding 0")]
			[Description("Padding 0 .")]
			[Browsable(false)]
			public Byte[] Pad0 {
				get { return pad0; }
				set { SetProperty(ref pad0, ref value, Pad0Property); }
			}

			/// <summary>Reference ID</summary>
			/// <remarks>
			/// Japanese short name: "参照ID", Google translated: "Reference ID".
			/// Japanese description: "攻撃力、飛び道具、特殊効果パラメータのID、refTypeによって使い分けられる。", Google translated: "ID attack power , missile , special effects parameters , can be used for different refType.".
			/// </remarks>
			[ParameterTableRowAttribute("refId", index: 5, minimum: -1, maximum: 1E+09, step: 1, order: 5, unknown2: 1)]
			[DisplayName("Reference ID")]
			[Description("ID attack power , missile , special effects parameters , can be used for different refType.")]
			[DefaultValue((Int32)(-1))]
			public Int32 RefId {
				get { return refId; }
				set {
					if ((double)value < -1 || (double)value > 1E+09)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+09 for RefId.");
					SetProperty(ref refId, ref value, RefIdProperty);
				}
			}

			/// <summary>SFX variation ID</summary>
			/// <remarks>
			/// Japanese short name: "SFX バリエーションID", Google translated: "SFX variation ID".
			/// Japanese description: "ＳＦＸのバリエーションを指定（TimeActEditorのＩＤと組み合わせて、ＳＦＸを特定するのに使用する）", Google translated: "( In conjunction with the ID of TimeActEditor, is used to identify the SFX) specifies the variation SFX".
			/// </remarks>
			[ParameterTableRowAttribute("sfxVariationId", index: 6, minimum: -1, maximum: 1E+10, step: 1, order: 6, unknown2: 1)]
			[DisplayName("SFX variation ID")]
			[Description("( In conjunction with the ID of TimeActEditor, is used to identify the SFX) specifies the variation SFX")]
			[DefaultValue((Int32)0)]
			public Int32 SfxVariationId {
				get { return sfxVariationId; }
				set {
					if ((double)value < -1 || (double)value > 1E+10)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+10 for SfxVariationId.");
					SetProperty(ref sfxVariationId, ref value, SfxVariationIdProperty);
				}
			}

			/// <summary>Consumption stamina</summary>
			/// <remarks>
			/// Japanese short name: "消費スタミナ", Google translated: "Consumption stamina".
			/// Japanese description: "行動時の消費スタミナ量を設定.", Google translated: "Set the amount of stamina consumption behavior at the time .".
			/// </remarks>
			[ParameterTableRowAttribute("stamina", index: 7, minimum: 0, maximum: 9999, step: 1, order: 100, unknown2: 1)]
			[DisplayName("Consumption stamina")]
			[Description("Set the amount of stamina consumption behavior at the time .")]
			[DefaultValue((Int32)0)]
			public Int32 Stamina {
				get { return stamina; }
				set {
					if ((double)value < 0 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9999 for Stamina.");
					SetProperty(ref stamina, ref value, StaminaProperty);
				}
			}

			/// <summary>MP consumption</summary>
			/// <remarks>
			/// Japanese short name: "消費MP", Google translated: "MP consumption".
			/// Japanese description: "行動時の消費MP量を設定.", Google translated: "Set the MP consumption amount of action at the time .".
			/// </remarks>
			[ParameterTableRowAttribute("mp", index: 8, minimum: 0, maximum: 9999, step: 1, order: 200, unknown2: 1)]
			[DisplayName("MP consumption")]
			[Description("Set the MP consumption amount of action at the time .")]
			[DefaultValue((Int32)0)]
			public Int32 Mp {
				get { return mp; }
				set {
					if ((double)value < 0 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9999 for Mp.");
					SetProperty(ref mp, ref value, MpProperty);
				}
			}

			/// <summary>Category</summary>
			/// <remarks>
			/// Japanese short name: "カテゴリ", Google translated: "Category".
			/// Japanese description: "スキルや、魔法、アイテムなどで、パラメータが変動する効果（エンチャントウェポンなど）があるので、│定した効果が、「武器攻撃のみをパワーアップする」といった効果に対応できるように行動ごとに設定するバリスタなど、設定の必要のないものは「なし」を設定する", Google translated: "Skills , magic , or item , because the effect of variation in parameters (such as Enchant Weapon ) , and sets the action for each effect it was │ boss is , to cope with effects such as' power up the only weapon attack " things such as varistor , without the need for setting is set to " None"".
			/// </remarks>
			[ParameterTableRowAttribute("category", index: 9, minimum: 0, maximum: 255, step: 1, order: 7, unknown2: 1)]
			[DisplayName("Category")]
			[Description("Skills , magic , or item , because the effect of variation in parameters (such as Enchant Weapon ) , and sets the action for each effect it was │ boss is , to cope with effects such as' power up the only weapon attack \" things such as varistor , without the need for setting is set to \" None\"")]
			[DefaultValue((BehaviorCategory)0)]
			public BehaviorCategory Category {
				get { return category; }
				set { SetProperty(ref category, ref value, CategoryProperty); }
			}

			/// <summary>Consumption humanity</summary>
			/// <remarks>
			/// Japanese short name: "消費人間性", Google translated: "Consumption humanity".
			/// Japanese description: "行動時の消費人間性量を設定", Google translated: "Set the consumption humanity amount of action at the time".
			/// </remarks>
			[ParameterTableRowAttribute("heroPoint", index: 10, minimum: 0, maximum: 255, step: 1, order: 300, unknown2: 1)]
			[DisplayName("Consumption humanity")]
			[Description("Set the consumption humanity amount of action at the time")]
			[DefaultValue((Byte)0)]
			public Byte HeroPoint {
				get { return heroPoint; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for HeroPoint.");
					SetProperty(ref heroPoint, ref value, HeroPointProperty);
				}
			}

			/// <summary>Padding 1</summary>
			/// <remarks>
			/// Japanese short name: "パディング1", Google translated: "Padding 1".
			/// Japanese description: "パディング1.", Google translated: "Padding 1 .".
			/// </remarks>
			[ParameterTableRowAttribute("pad1[2]", index: 11, minimum: 0, maximum: 0, step: 0, order: 302, unknown2: 0)]
			[DisplayName("Padding 1")]
			[Description("Padding 1 .")]
			[Browsable(false)]
			public Byte[] Pad1 {
				get { return pad1; }
				set { SetProperty(ref pad1, ref value, Pad1Property); }
			}

			internal Behavior(ParameterTable table, int index, AssetLoader loader, int next)
				: base(table, index, loader) {
				BinaryReader reader = loader.Reader;

				VariationId = reader.ReadInt32();
				BehaviorJudgeId = reader.ReadInt32();
				EzStateBehaviorType_old = reader.ReadByte();
				RefType = (BehaviorRefType)reader.ReadByte();
				Pad0 = reader.ReadBytes(2);
				RefId = reader.ReadInt32();
				SfxVariationId = reader.ReadInt32();
				Stamina = reader.ReadInt32();
				Mp = reader.ReadInt32();
				Category = (BehaviorCategory)reader.ReadByte();
				HeroPoint = reader.ReadByte();
				Pad1 = reader.ReadBytes(2);
			}

			internal Behavior(ParameterTable table, int index)
				: base(table, index) {
				VariationId = (Int32)0;
				BehaviorJudgeId = (Int32)0;
				EzStateBehaviorType_old = (Byte)0;
				RefType = (BehaviorRefType)0;
				Pad0 = new Byte[2];
				RefId = (Int32)(-1);
				SfxVariationId = (Int32)0;
				Stamina = (Int32)0;
				Mp = (Int32)0;
				Category = (BehaviorCategory)0;
				HeroPoint = (Byte)0;
				Pad1 = new Byte[2];
			}

			public override void Write(BinaryWriter writer) {
				writer.Write(VariationId);
				writer.Write(BehaviorJudgeId);
				writer.Write(EzStateBehaviorType_old);
				writer.Write((Byte)RefType);
				writer.Write(Pad0);
				writer.Write(RefId);
				writer.Write(SfxVariationId);
				writer.Write(Stamina);
				writer.Write(Mp);
				writer.Write((Byte)Category);
				writer.Write(HeroPoint);
				writer.Write(Pad1);
			}
		}
	}
}
