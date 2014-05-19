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
		/// 
		/// </summary>
		/// <remarks>
		/// From "GameAreaParam.paramdef" (id 29h).
		/// </remarks>
		public class GameArea : ParameterTableRow {
			public const string TableName = "GAME_AREA_PARAM_ST";

			UInt32 bonusSoul_single, bonusSoul_multi;
			Int32 humanityPointCountFlagIdTop;
			Int16 humanityDropPoint1, humanityDropPoint2, humanityDropPoint3, humanityDropPoint4, humanityDropPoint5, humanityDropPoint6, humanityDropPoint7, humanityDropPoint8, humanityDropPoint9, humanityDropPoint10;

			public static readonly PropertyInfo
				BonusSoul_singleProperty = GetProperty<GameArea>("BonusSoul_single"),
				BonusSoul_multiProperty = GetProperty<GameArea>("BonusSoul_multi"),
				HumanityPointCountFlagIdTopProperty = GetProperty<GameArea>("HumanityPointCountFlagIdTop"),
				HumanityDropPoint1Property = GetProperty<GameArea>("HumanityDropPoint1"),
				HumanityDropPoint2Property = GetProperty<GameArea>("HumanityDropPoint2"),
				HumanityDropPoint3Property = GetProperty<GameArea>("HumanityDropPoint3"),
				HumanityDropPoint4Property = GetProperty<GameArea>("HumanityDropPoint4"),
				HumanityDropPoint5Property = GetProperty<GameArea>("HumanityDropPoint5"),
				HumanityDropPoint6Property = GetProperty<GameArea>("HumanityDropPoint6"),
				HumanityDropPoint7Property = GetProperty<GameArea>("HumanityDropPoint7"),
				HumanityDropPoint8Property = GetProperty<GameArea>("HumanityDropPoint8"),
				HumanityDropPoint9Property = GetProperty<GameArea>("HumanityDropPoint9"),
				HumanityDropPoint10Property = GetProperty<GameArea>("HumanityDropPoint10");

			/// <summary>Single at the time clear bonus amount Seoul</summary>
			/// <remarks>
			/// Japanese short name: "シングル時クリアボーナスソウル量", Google translated: "Single at the time clear bonus amount Seoul".
			/// Japanese description: "エリアボスを倒したときに取得できるソウル量(シングルプレイ時)", Google translated: "Seoul amount that can be obtained when defeated the area boss ( single player mode)".
			/// </remarks>
			[ParameterTableRowAttribute("bonusSoul_single", index: 0, minimum: 0, maximum: 1E+08, step: 1, order: 100, unknown2: 1)]
			[DisplayName("Single at the time clear bonus amount Seoul")]
			[Description("Seoul amount that can be obtained when defeated the area boss ( single player mode)")]
			[DefaultValue((UInt32)0)]
			public UInt32 BonusSoul_single {
				get { return bonusSoul_single; }
				set {
					if ((double)value < 0 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1E+08 for BonusSoul_single.");
					SetProperty(ref bonusSoul_single, ref value, BonusSoul_singleProperty);
				}
			}

			/// <summary>Multiplayer when clear bonus amount Seoul</summary>
			/// <remarks>
			/// Japanese short name: "マルチプレイ時クリアボーナスソウル量", Google translated: "Multiplayer when clear bonus amount Seoul".
			/// Japanese description: "エリアボスを倒したときに取得できるソウル量(マルチプレイ時)", Google translated: "Seoul amount that can be obtained when defeated the area boss ( Multiplayer mode)".
			/// </remarks>
			[ParameterTableRowAttribute("bonusSoul_multi", index: 1, minimum: 0, maximum: 1E+08, step: 1, order: 200, unknown2: 1)]
			[DisplayName("Multiplayer when clear bonus amount Seoul")]
			[Description("Seoul amount that can be obtained when defeated the area boss ( Multiplayer mode)")]
			[DefaultValue((UInt32)0)]
			public UInt32 BonusSoul_multi {
				get { return bonusSoul_multi; }
				set {
					if ((double)value < 0 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1E+08 for BonusSoul_multi.");
					SetProperty(ref bonusSoul_multi, ref value, BonusSoul_multiProperty);
				}
			}

			/// <summary>Humanity drop point count beginning flag ID</summary>
			/// <remarks>
			/// Japanese short name: "人間性ドロップポイントカウント先頭フラグID", Google translated: "Humanity drop point count beginning flag ID".
			/// Japanese description: "人間性ドロップポイントを管理する為の先頭フラグID(20Bit使用)", Google translated: "First flag ID for managing the humanity drop point (20Bit used)".
			/// </remarks>
			[ParameterTableRowAttribute("humanityPointCountFlagIdTop", index: 2, minimum: -1, maximum: 1E+08, step: 1, order: 300, unknown2: 1)]
			[DisplayName("Humanity drop point count beginning flag ID")]
			[Description("First flag ID for managing the humanity drop point (20Bit used)")]
			[DefaultValue((Int32)(-1))]
			public Int32 HumanityPointCountFlagIdTop {
				get { return humanityPointCountFlagIdTop; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for HumanityPointCountFlagIdTop.");
					SetProperty(ref humanityPointCountFlagIdTop, ref value, HumanityPointCountFlagIdTopProperty);
				}
			}

			/// <summary>Humanity drop in points 1</summary>
			/// <remarks>
			/// Japanese short name: "人間性ドロップ必要ポイント1", Google translated: "Humanity drop in points 1".
			/// Japanese description: "人間性を取得する為の閾値1", Google translated: "1 threshold for obtaining the humanity".
			/// </remarks>
			[ParameterTableRowAttribute("humanityDropPoint1", index: 3, minimum: -1, maximum: 32767, step: 1, order: 400, unknown2: 1)]
			[DisplayName("Humanity drop in points 1")]
			[Description("1 threshold for obtaining the humanity")]
			[DefaultValue((Int16)(-1))]
			public Int16 HumanityDropPoint1 {
				get { return humanityDropPoint1; }
				set {
					if ((double)value < -1 || (double)value > 32767)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 32767 for HumanityDropPoint1.");
					SetProperty(ref humanityDropPoint1, ref value, HumanityDropPoint1Property);
				}
			}

			/// <summary>Humanity drop in points 2</summary>
			/// <remarks>
			/// Japanese short name: "人間性ドロップ必要ポイント2", Google translated: "Humanity drop in points 2".
			/// Japanese description: "人間性を取得する為の閾値2", Google translated: "2 threshold for obtaining the humanity".
			/// </remarks>
			[ParameterTableRowAttribute("humanityDropPoint2", index: 4, minimum: -1, maximum: 32767, step: 1, order: 401, unknown2: 1)]
			[DisplayName("Humanity drop in points 2")]
			[Description("2 threshold for obtaining the humanity")]
			[DefaultValue((Int16)(-1))]
			public Int16 HumanityDropPoint2 {
				get { return humanityDropPoint2; }
				set {
					if ((double)value < -1 || (double)value > 32767)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 32767 for HumanityDropPoint2.");
					SetProperty(ref humanityDropPoint2, ref value, HumanityDropPoint2Property);
				}
			}

			/// <summary>Humanity drop in points 3</summary>
			/// <remarks>
			/// Japanese short name: "人間性ドロップ必要ポイント3", Google translated: "Humanity drop in points 3".
			/// Japanese description: "人間性を取得する為の閾値3", Google translated: "3 threshold for obtaining the humanity".
			/// </remarks>
			[ParameterTableRowAttribute("humanityDropPoint3", index: 5, minimum: -1, maximum: 32767, step: 1, order: 402, unknown2: 1)]
			[DisplayName("Humanity drop in points 3")]
			[Description("3 threshold for obtaining the humanity")]
			[DefaultValue((Int16)(-1))]
			public Int16 HumanityDropPoint3 {
				get { return humanityDropPoint3; }
				set {
					if ((double)value < -1 || (double)value > 32767)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 32767 for HumanityDropPoint3.");
					SetProperty(ref humanityDropPoint3, ref value, HumanityDropPoint3Property);
				}
			}

			/// <summary>Humanity drop in points 4</summary>
			/// <remarks>
			/// Japanese short name: "人間性ドロップ必要ポイント4", Google translated: "Humanity drop in points 4".
			/// Japanese description: "人間性を取得する為の閾値4", Google translated: "4 threshold for obtaining the humanity".
			/// </remarks>
			[ParameterTableRowAttribute("humanityDropPoint4", index: 6, minimum: -1, maximum: 32767, step: 1, order: 403, unknown2: 1)]
			[DisplayName("Humanity drop in points 4")]
			[Description("4 threshold for obtaining the humanity")]
			[DefaultValue((Int16)(-1))]
			public Int16 HumanityDropPoint4 {
				get { return humanityDropPoint4; }
				set {
					if ((double)value < -1 || (double)value > 32767)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 32767 for HumanityDropPoint4.");
					SetProperty(ref humanityDropPoint4, ref value, HumanityDropPoint4Property);
				}
			}

			/// <summary>Humanity drop in points 5</summary>
			/// <remarks>
			/// Japanese short name: "人間性ドロップ必要ポイント5", Google translated: "Humanity drop in points 5".
			/// Japanese description: "人間性を取得する為の閾値5", Google translated: "5 threshold for obtaining the humanity".
			/// </remarks>
			[ParameterTableRowAttribute("humanityDropPoint5", index: 7, minimum: -1, maximum: 32767, step: 1, order: 404, unknown2: 1)]
			[DisplayName("Humanity drop in points 5")]
			[Description("5 threshold for obtaining the humanity")]
			[DefaultValue((Int16)(-1))]
			public Int16 HumanityDropPoint5 {
				get { return humanityDropPoint5; }
				set {
					if ((double)value < -1 || (double)value > 32767)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 32767 for HumanityDropPoint5.");
					SetProperty(ref humanityDropPoint5, ref value, HumanityDropPoint5Property);
				}
			}

			/// <summary>Humanity must drop point 6</summary>
			/// <remarks>
			/// Japanese short name: "人間性ドロップ必要ポイント6", Google translated: "Humanity must drop point 6".
			/// Japanese description: "人間性を取得する為の閾値6", Google translated: "6 threshold for obtaining the humanity".
			/// </remarks>
			[ParameterTableRowAttribute("humanityDropPoint6", index: 8, minimum: -1, maximum: 32767, step: 1, order: 405, unknown2: 1)]
			[DisplayName("Humanity must drop point 6")]
			[Description("6 threshold for obtaining the humanity")]
			[DefaultValue((Int16)(-1))]
			public Int16 HumanityDropPoint6 {
				get { return humanityDropPoint6; }
				set {
					if ((double)value < -1 || (double)value > 32767)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 32767 for HumanityDropPoint6.");
					SetProperty(ref humanityDropPoint6, ref value, HumanityDropPoint6Property);
				}
			}

			/// <summary>Humanity drop in points 7</summary>
			/// <remarks>
			/// Japanese short name: "人間性ドロップ必要ポイント7", Google translated: "Humanity drop in points 7".
			/// Japanese description: "人間性を取得する為の閾値7", Google translated: "7 threshold for obtaining the humanity".
			/// </remarks>
			[ParameterTableRowAttribute("humanityDropPoint7", index: 9, minimum: -1, maximum: 32767, step: 1, order: 406, unknown2: 1)]
			[DisplayName("Humanity drop in points 7")]
			[Description("7 threshold for obtaining the humanity")]
			[DefaultValue((Int16)(-1))]
			public Int16 HumanityDropPoint7 {
				get { return humanityDropPoint7; }
				set {
					if ((double)value < -1 || (double)value > 32767)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 32767 for HumanityDropPoint7.");
					SetProperty(ref humanityDropPoint7, ref value, HumanityDropPoint7Property);
				}
			}

			/// <summary>Humanity must drop point 8</summary>
			/// <remarks>
			/// Japanese short name: "人間性ドロップ必要ポイント8", Google translated: "Humanity must drop point 8".
			/// Japanese description: "人間性を取得する為の閾値8", Google translated: "8 threshold for obtaining the humanity".
			/// </remarks>
			[ParameterTableRowAttribute("humanityDropPoint8", index: 10, minimum: -1, maximum: 32767, step: 1, order: 407, unknown2: 1)]
			[DisplayName("Humanity must drop point 8")]
			[Description("8 threshold for obtaining the humanity")]
			[DefaultValue((Int16)(-1))]
			public Int16 HumanityDropPoint8 {
				get { return humanityDropPoint8; }
				set {
					if ((double)value < -1 || (double)value > 32767)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 32767 for HumanityDropPoint8.");
					SetProperty(ref humanityDropPoint8, ref value, HumanityDropPoint8Property);
				}
			}

			/// <summary>Humanity drop in points 9</summary>
			/// <remarks>
			/// Japanese short name: "人間性ドロップ必要ポイント9", Google translated: "Humanity drop in points 9".
			/// Japanese description: "人間性を取得する為の閾値9", Google translated: "9 threshold for obtaining the humanity".
			/// </remarks>
			[ParameterTableRowAttribute("humanityDropPoint9", index: 11, minimum: -1, maximum: 32767, step: 1, order: 408, unknown2: 1)]
			[DisplayName("Humanity drop in points 9")]
			[Description("9 threshold for obtaining the humanity")]
			[DefaultValue((Int16)(-1))]
			public Int16 HumanityDropPoint9 {
				get { return humanityDropPoint9; }
				set {
					if ((double)value < -1 || (double)value > 32767)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 32767 for HumanityDropPoint9.");
					SetProperty(ref humanityDropPoint9, ref value, HumanityDropPoint9Property);
				}
			}

			/// <summary>Humanity drop in points 10</summary>
			/// <remarks>
			/// Japanese short name: "人間性ドロップ必要ポイント10", Google translated: "Humanity drop in points 10".
			/// Japanese description: "人間性を取得する為の閾値10", Google translated: "10 threshold for obtaining the humanity".
			/// </remarks>
			[ParameterTableRowAttribute("humanityDropPoint10", index: 12, minimum: -1, maximum: 32767, step: 1, order: 409, unknown2: 1)]
			[DisplayName("Humanity drop in points 10")]
			[Description("10 threshold for obtaining the humanity")]
			[DefaultValue((Int16)(-1))]
			public Int16 HumanityDropPoint10 {
				get { return humanityDropPoint10; }
				set {
					if ((double)value < -1 || (double)value > 32767)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 32767 for HumanityDropPoint10.");
					SetProperty(ref humanityDropPoint10, ref value, HumanityDropPoint10Property);
				}
			}

			internal GameArea(ParameterTable table, int index, AssetLoader loader, int next)
				: base(table, index, loader) {
				BinaryReader reader = loader.Reader;

				BonusSoul_single = reader.ReadUInt32();
				BonusSoul_multi = reader.ReadUInt32();
				HumanityPointCountFlagIdTop = reader.ReadInt32();
				HumanityDropPoint1 = reader.ReadInt16();
				HumanityDropPoint2 = reader.ReadInt16();
				HumanityDropPoint3 = reader.ReadInt16();
				HumanityDropPoint4 = reader.ReadInt16();
				HumanityDropPoint5 = reader.ReadInt16();
				HumanityDropPoint6 = reader.ReadInt16();
				HumanityDropPoint7 = reader.ReadInt16();
				HumanityDropPoint8 = reader.ReadInt16();
				HumanityDropPoint9 = reader.ReadInt16();
				HumanityDropPoint10 = reader.ReadInt16();
			}

			internal GameArea(ParameterTable table, int index)
				: base(table, index) {
				BonusSoul_single = (UInt32)0;
				BonusSoul_multi = (UInt32)0;
				HumanityPointCountFlagIdTop = (Int32)(-1);
				HumanityDropPoint1 = (Int16)(-1);
				HumanityDropPoint2 = (Int16)(-1);
				HumanityDropPoint3 = (Int16)(-1);
				HumanityDropPoint4 = (Int16)(-1);
				HumanityDropPoint5 = (Int16)(-1);
				HumanityDropPoint6 = (Int16)(-1);
				HumanityDropPoint7 = (Int16)(-1);
				HumanityDropPoint8 = (Int16)(-1);
				HumanityDropPoint9 = (Int16)(-1);
				HumanityDropPoint10 = (Int16)(-1);
			}

			public override void Write(BinaryWriter writer) {
				writer.Write(BonusSoul_single);
				writer.Write(BonusSoul_multi);
				writer.Write(HumanityPointCountFlagIdTop);
				writer.Write(HumanityDropPoint1);
				writer.Write(HumanityDropPoint2);
				writer.Write(HumanityDropPoint3);
				writer.Write(HumanityDropPoint4);
				writer.Write(HumanityDropPoint5);
				writer.Write(HumanityDropPoint6);
				writer.Write(HumanityDropPoint7);
				writer.Write(HumanityDropPoint8);
				writer.Write(HumanityDropPoint9);
				writer.Write(HumanityDropPoint10);
			}
		}
	}
}
