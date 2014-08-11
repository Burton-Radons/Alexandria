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
		/// <remarks>From CalcCorrectGraph.paramdef (id 2Bh).</remarks>
		public class CalculationCorrection : ParameterTableRow {
			/// <summary>The name of the table in the file.</summary>
			public const string TableName = "CACL_CORRECT_GRAPH_ST";

			Single stageMaxVal0, stageMaxVal1, stageMaxVal2, stageMaxVal3, stageMaxVal4, stageMaxGrowVal0, stageMaxGrowVal1, stageMaxGrowVal2, stageMaxGrowVal3, stageMaxGrowVal4, adjPt_maxGrowVal0, adjPt_maxGrowVal1, adjPt_maxGrowVal2, adjPt_maxGrowVal3, adjPt_maxGrowVal4, init_inclination_soul, adjustment_value, boundry_inclination_soul, boundry_value;
			Byte[] pad;

			/// <summary>A property of the class.</summary>
			public static readonly PropertyInfo
				StageMaxVal0Property = GetProperty<CalculationCorrection>("StageMaxVal0"),
				StageMaxVal1Property = GetProperty<CalculationCorrection>("StageMaxVal1"),
				StageMaxVal2Property = GetProperty<CalculationCorrection>("StageMaxVal2"),
				StageMaxVal3Property = GetProperty<CalculationCorrection>("StageMaxVal3"),
				StageMaxVal4Property = GetProperty<CalculationCorrection>("StageMaxVal4"),
				StageMaxGrowVal0Property = GetProperty<CalculationCorrection>("StageMaxGrowVal0"),
				StageMaxGrowVal1Property = GetProperty<CalculationCorrection>("StageMaxGrowVal1"),
				StageMaxGrowVal2Property = GetProperty<CalculationCorrection>("StageMaxGrowVal2"),
				StageMaxGrowVal3Property = GetProperty<CalculationCorrection>("StageMaxGrowVal3"),
				StageMaxGrowVal4Property = GetProperty<CalculationCorrection>("StageMaxGrowVal4"),
				AdjPt_maxGrowVal0Property = GetProperty<CalculationCorrection>("AdjPt_maxGrowVal0"),
				AdjPt_maxGrowVal1Property = GetProperty<CalculationCorrection>("AdjPt_maxGrowVal1"),
				AdjPt_maxGrowVal2Property = GetProperty<CalculationCorrection>("AdjPt_maxGrowVal2"),
				AdjPt_maxGrowVal3Property = GetProperty<CalculationCorrection>("AdjPt_maxGrowVal3"),
				AdjPt_maxGrowVal4Property = GetProperty<CalculationCorrection>("AdjPt_maxGrowVal4"),
				Init_inclination_soulProperty = GetProperty<CalculationCorrection>("Init_inclination_soul"),
				Adjustment_valueProperty = GetProperty<CalculationCorrection>("Adjustment_value"),
				Boundry_inclination_soulProperty = GetProperty<CalculationCorrection>("Boundry_inclination_soul"),
				Boundry_valueProperty = GetProperty<CalculationCorrection>("Boundry_value"),
				PadProperty = GetProperty<CalculationCorrection>("Pad");

			/// <summary>Threshold point 0</summary>
			/// <remarks>
			/// Japanese short name: "閾値ポイント0", Google translated: "Threshold point 0".
			/// Japanese description: "仕様書に「n次閾値[point]」と書いてあるもの", Google translated: "Which it is written, " n-th threshold [point] " and in the specifications".
			/// </remarks>
			[ParameterTableRowAttribute("stageMaxVal0", index: 0, minimum: 0, maximum: 999, step: 0.1, sortOrder: 100, unknown2: 1)]
			[DisplayName("Threshold point 0")]
			[Description("Which it is written, \" n-th threshold [point] \" and in the specifications")]
			[DefaultValue((Single)0)]
			public Single StageMaxVal0 {
				get { return stageMaxVal0; }
				set {
					if ((double)value < 0 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for StageMaxVal0.");
					SetProperty(ref stageMaxVal0, ref value, StageMaxVal0Property);
				}
			}

			/// <summary>Threshold point 1</summary>
			/// <remarks>
			/// Japanese short name: "閾値ポイント1", Google translated: "Threshold point 1".
			/// Japanese description: "仕様書に「n次閾値[point]」と書いてあるもの", Google translated: "Which it is written, " n-th threshold [point] " and in the specifications".
			/// </remarks>
			[ParameterTableRowAttribute("stageMaxVal1", index: 1, minimum: 0, maximum: 999, step: 0.1, sortOrder: 200, unknown2: 1)]
			[DisplayName("Threshold point 1")]
			[Description("Which it is written, \" n-th threshold [point] \" and in the specifications")]
			[DefaultValue((Single)0)]
			public Single StageMaxVal1 {
				get { return stageMaxVal1; }
				set {
					if ((double)value < 0 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for StageMaxVal1.");
					SetProperty(ref stageMaxVal1, ref value, StageMaxVal1Property);
				}
			}

			/// <summary>Threshold point 2</summary>
			/// <remarks>
			/// Japanese short name: "閾値ポイント2", Google translated: "Threshold point 2".
			/// Japanese description: "仕様書に「n次閾値[point]」と書いてあるもの", Google translated: "Which it is written, " n-th threshold [point] " and in the specifications".
			/// </remarks>
			[ParameterTableRowAttribute("stageMaxVal2", index: 2, minimum: 0, maximum: 999, step: 0.1, sortOrder: 300, unknown2: 1)]
			[DisplayName("Threshold point 2")]
			[Description("Which it is written, \" n-th threshold [point] \" and in the specifications")]
			[DefaultValue((Single)0)]
			public Single StageMaxVal2 {
				get { return stageMaxVal2; }
				set {
					if ((double)value < 0 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for StageMaxVal2.");
					SetProperty(ref stageMaxVal2, ref value, StageMaxVal2Property);
				}
			}

			/// <summary>Threshold point 3</summary>
			/// <remarks>
			/// Japanese short name: "閾値ポイント3", Google translated: "Threshold point 3".
			/// Japanese description: "仕様書に「n次閾値[point]」と書いてあるもの", Google translated: "Which it is written, " n-th threshold [point] " and in the specifications".
			/// </remarks>
			[ParameterTableRowAttribute("stageMaxVal3", index: 3, minimum: 0, maximum: 999, step: 0.1, sortOrder: 400, unknown2: 1)]
			[DisplayName("Threshold point 3")]
			[Description("Which it is written, \" n-th threshold [point] \" and in the specifications")]
			[DefaultValue((Single)0)]
			public Single StageMaxVal3 {
				get { return stageMaxVal3; }
				set {
					if ((double)value < 0 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for StageMaxVal3.");
					SetProperty(ref stageMaxVal3, ref value, StageMaxVal3Property);
				}
			}

			/// <summary>Threshold point 4</summary>
			/// <remarks>
			/// Japanese short name: "閾値ポイント4", Google translated: "Threshold point 4".
			/// Japanese description: "仕様書に「n次閾値[point]」と書いてあるもの", Google translated: "Which it is written, " n-th threshold [point] " and in the specifications".
			/// </remarks>
			[ParameterTableRowAttribute("stageMaxVal4", index: 4, minimum: 0, maximum: 999, step: 0.1, sortOrder: 500, unknown2: 1)]
			[DisplayName("Threshold point 4")]
			[Description("Which it is written, \" n-th threshold [point] \" and in the specifications")]
			[DefaultValue((Single)0)]
			public Single StageMaxVal4 {
				get { return stageMaxVal4; }
				set {
					if ((double)value < 0 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for StageMaxVal4.");
					SetProperty(ref stageMaxVal4, ref value, StageMaxVal4Property);
				}
			}

			/// <summary>Threshold coefficient 0</summary>
			/// <remarks>
			/// Japanese short name: "閾値係数0", Google translated: "Threshold coefficient 0".
			/// Japanese description: "仕様書に「n次閾値[係数]」と書いてあるもの", Google translated: "Which it is written, " n-order threshold [ factor ] " and in the specifications".
			/// </remarks>
			[ParameterTableRowAttribute("stageMaxGrowVal0", index: 5, minimum: 0, maximum: 9999, step: 0.1, sortOrder: 700, unknown2: 1)]
			[DisplayName("Threshold coefficient 0")]
			[Description("Which it is written, \" n-order threshold [ factor ] \" and in the specifications")]
			[DefaultValue((Single)0)]
			public Single StageMaxGrowVal0 {
				get { return stageMaxGrowVal0; }
				set {
					if ((double)value < 0 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9999 for StageMaxGrowVal0.");
					SetProperty(ref stageMaxGrowVal0, ref value, StageMaxGrowVal0Property);
				}
			}

			/// <summary>Threshold coefficient 1</summary>
			/// <remarks>
			/// Japanese short name: "閾値係数1", Google translated: "Threshold coefficient 1".
			/// Japanese description: "仕様書に「n次閾値[係数]」と書いてあるもの", Google translated: "Which it is written, " n-order threshold [ factor ] " and in the specifications".
			/// </remarks>
			[ParameterTableRowAttribute("stageMaxGrowVal1", index: 6, minimum: 0, maximum: 9999, step: 0.1, sortOrder: 800, unknown2: 1)]
			[DisplayName("Threshold coefficient 1")]
			[Description("Which it is written, \" n-order threshold [ factor ] \" and in the specifications")]
			[DefaultValue((Single)0)]
			public Single StageMaxGrowVal1 {
				get { return stageMaxGrowVal1; }
				set {
					if ((double)value < 0 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9999 for StageMaxGrowVal1.");
					SetProperty(ref stageMaxGrowVal1, ref value, StageMaxGrowVal1Property);
				}
			}

			/// <summary>Threshold factor 2</summary>
			/// <remarks>
			/// Japanese short name: "閾値係数2", Google translated: "Threshold factor 2".
			/// Japanese description: "仕様書に「n次閾値[係数]」と書いてあるもの", Google translated: "Which it is written, " n-order threshold [ factor ] " and in the specifications".
			/// </remarks>
			[ParameterTableRowAttribute("stageMaxGrowVal2", index: 7, minimum: 0, maximum: 9999, step: 0.1, sortOrder: 900, unknown2: 1)]
			[DisplayName("Threshold factor 2")]
			[Description("Which it is written, \" n-order threshold [ factor ] \" and in the specifications")]
			[DefaultValue((Single)0)]
			public Single StageMaxGrowVal2 {
				get { return stageMaxGrowVal2; }
				set {
					if ((double)value < 0 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9999 for StageMaxGrowVal2.");
					SetProperty(ref stageMaxGrowVal2, ref value, StageMaxGrowVal2Property);
				}
			}

			/// <summary>Threshold coefficient 3</summary>
			/// <remarks>
			/// Japanese short name: "閾値係数3", Google translated: "Threshold coefficient 3".
			/// Japanese description: "仕様書に「n次閾値[係数]」と書いてあるもの", Google translated: "Which it is written, " n-order threshold [ factor ] " and in the specifications".
			/// </remarks>
			[ParameterTableRowAttribute("stageMaxGrowVal3", index: 8, minimum: 0, maximum: 9999, step: 0.1, sortOrder: 1000, unknown2: 1)]
			[DisplayName("Threshold coefficient 3")]
			[Description("Which it is written, \" n-order threshold [ factor ] \" and in the specifications")]
			[DefaultValue((Single)0)]
			public Single StageMaxGrowVal3 {
				get { return stageMaxGrowVal3; }
				set {
					if ((double)value < 0 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9999 for StageMaxGrowVal3.");
					SetProperty(ref stageMaxGrowVal3, ref value, StageMaxGrowVal3Property);
				}
			}

			/// <summary>Threshold factor of 4</summary>
			/// <remarks>
			/// Japanese short name: "閾値係数4", Google translated: "Threshold factor of 4".
			/// Japanese description: "仕様書に「n次閾値[係数]」と書いてあるもの", Google translated: "Which it is written, " n-order threshold [ factor ] " and in the specifications".
			/// </remarks>
			[ParameterTableRowAttribute("stageMaxGrowVal4", index: 9, minimum: 0, maximum: 9999, step: 0.1, sortOrder: 1100, unknown2: 1)]
			[DisplayName("Threshold factor of 4")]
			[Description("Which it is written, \" n-order threshold [ factor ] \" and in the specifications")]
			[DefaultValue((Single)0)]
			public Single StageMaxGrowVal4 {
				get { return stageMaxGrowVal4; }
				set {
					if ((double)value < 0 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9999 for StageMaxGrowVal4.");
					SetProperty(ref stageMaxGrowVal4, ref value, StageMaxGrowVal4Property);
				}
			}

			/// <summary>Adjustment factor 0</summary>
			/// <remarks>
			/// Japanese short name: "調整係数0", Google translated: "Adjustment factor 0".
			/// Japanese description: "調整係数", Google translated: "Adjustment factor".
			/// </remarks>
			[ParameterTableRowAttribute("adjPt_maxGrowVal0", index: 10, minimum: -99, maximum: 99, step: 0.01, sortOrder: 1300, unknown2: 1)]
			[DisplayName("Adjustment factor 0")]
			[Description("Adjustment factor")]
			[DefaultValue((Single)0)]
			public Single AdjPt_maxGrowVal0 {
				get { return adjPt_maxGrowVal0; }
				set {
					if ((double)value < -99 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -99 to 99 for AdjPt_maxGrowVal0.");
					SetProperty(ref adjPt_maxGrowVal0, ref value, AdjPt_maxGrowVal0Property);
				}
			}

			/// <summary>Adjustment factor 1</summary>
			/// <remarks>
			/// Japanese short name: "調整係数1", Google translated: "Adjustment factor 1".
			/// Japanese description: "調整係数", Google translated: "Adjustment factor".
			/// </remarks>
			[ParameterTableRowAttribute("adjPt_maxGrowVal1", index: 11, minimum: -99, maximum: 99, step: 0.01, sortOrder: 1400, unknown2: 1)]
			[DisplayName("Adjustment factor 1")]
			[Description("Adjustment factor")]
			[DefaultValue((Single)0)]
			public Single AdjPt_maxGrowVal1 {
				get { return adjPt_maxGrowVal1; }
				set {
					if ((double)value < -99 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -99 to 99 for AdjPt_maxGrowVal1.");
					SetProperty(ref adjPt_maxGrowVal1, ref value, AdjPt_maxGrowVal1Property);
				}
			}

			/// <summary>Adjustment factor 2</summary>
			/// <remarks>
			/// Japanese short name: "調整係数2", Google translated: "Adjustment factor 2".
			/// Japanese description: "調整係数", Google translated: "Adjustment factor".
			/// </remarks>
			[ParameterTableRowAttribute("adjPt_maxGrowVal2", index: 12, minimum: -99, maximum: 99, step: 0.01, sortOrder: 1500, unknown2: 1)]
			[DisplayName("Adjustment factor 2")]
			[Description("Adjustment factor")]
			[DefaultValue((Single)0)]
			public Single AdjPt_maxGrowVal2 {
				get { return adjPt_maxGrowVal2; }
				set {
					if ((double)value < -99 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -99 to 99 for AdjPt_maxGrowVal2.");
					SetProperty(ref adjPt_maxGrowVal2, ref value, AdjPt_maxGrowVal2Property);
				}
			}

			/// <summary>Adjustment factor 3</summary>
			/// <remarks>
			/// Japanese short name: "調整係数3", Google translated: "Adjustment factor 3".
			/// Japanese description: "調整係数", Google translated: "Adjustment factor".
			/// </remarks>
			[ParameterTableRowAttribute("adjPt_maxGrowVal3", index: 13, minimum: -99, maximum: 99, step: 0.01, sortOrder: 1600, unknown2: 1)]
			[DisplayName("Adjustment factor 3")]
			[Description("Adjustment factor")]
			[DefaultValue((Single)0)]
			public Single AdjPt_maxGrowVal3 {
				get { return adjPt_maxGrowVal3; }
				set {
					if ((double)value < -99 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -99 to 99 for AdjPt_maxGrowVal3.");
					SetProperty(ref adjPt_maxGrowVal3, ref value, AdjPt_maxGrowVal3Property);
				}
			}

			/// <summary>Adjustment factor 4</summary>
			/// <remarks>
			/// Japanese short name: "調整係数4", Google translated: "Adjustment factor 4".
			/// Japanese description: "調整係数", Google translated: "Adjustment factor".
			/// </remarks>
			[ParameterTableRowAttribute("adjPt_maxGrowVal4", index: 14, minimum: -99, maximum: 99, step: 0.01, sortOrder: 1700, unknown2: 1)]
			[DisplayName("Adjustment factor 4")]
			[Description("Adjustment factor")]
			[DefaultValue((Single)0)]
			public Single AdjPt_maxGrowVal4 {
				get { return adjPt_maxGrowVal4; }
				set {
					if ((double)value < -99 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -99 to 99 for AdjPt_maxGrowVal4.");
					SetProperty(ref adjPt_maxGrowVal4, ref value, AdjPt_maxGrowVal4Property);
				}
			}

			/// <summary>Α1 slope of the graph of growth Seoul initial</summary>
			/// <remarks>
			/// Japanese short name: "成長ソウル 初期のグラフの傾きα1", Google translated: "Α1 slope of the graph of growth Seoul initial".
			/// Japanese description: "成長ソウル 初期のグラフの傾きα1", Google translated: "Α1 slope of the graph of growth Seoul initial".
			/// </remarks>
			[ParameterTableRowAttribute("init_inclination_soul", index: 15, minimum: -99, maximum: 99, step: 0.01, sortOrder: 1800, unknown2: 1)]
			[DisplayName("Α1 slope of the graph of growth Seoul initial")]
			[Description("Α1 slope of the graph of growth Seoul initial")]
			[DefaultValue((Single)0)]
			public Single Init_inclination_soul {
				get { return init_inclination_soul; }
				set {
					if ((double)value < -99 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -99 to 99 for Init_inclination_soul.");
					SetProperty(ref init_inclination_soul, ref value, Init_inclination_soulProperty);
				}
			}

			/// <summary>Adjust α2 soul of Seoul initial growth</summary>
			/// <remarks>
			/// Japanese short name: "成長ソウル 初期のsoul調整α2", Google translated: "Adjust α2 soul of Seoul initial growth".
			/// Japanese description: "成長ソウル 初期のsoul調整α2", Google translated: "Adjust α2 soul of Seoul initial growth".
			/// </remarks>
			[ParameterTableRowAttribute("adjustment_value", index: 16, minimum: -99, maximum: 99, step: 0.01, sortOrder: 1900, unknown2: 1)]
			[DisplayName("Adjust α2 soul of Seoul initial growth")]
			[Description("Adjust α2 soul of Seoul initial growth")]
			[DefaultValue((Single)0)]
			public Single Adjustment_value {
				get { return adjustment_value; }
				set {
					if ((double)value < -99 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -99 to 99 for Adjustment_value.");
					SetProperty(ref adjustment_value, ref value, Adjustment_valueProperty);
				}
			}

			/// <summary>Effect α3 to the slope of the graph of growth Seoul threshold after</summary>
			/// <remarks>
			/// Japanese short name: "成長ソウル 閾値後のグラフの傾きに影響α3", Google translated: "Effect α3 to the slope of the graph of growth Seoul threshold after".
			/// Japanese description: "成長ソウル 閾値後のグラフの傾きに影響α3", Google translated: "Effect α3 to the slope of the graph of growth Seoul threshold after".
			/// </remarks>
			[ParameterTableRowAttribute("boundry_inclination_soul", index: 17, minimum: -99, maximum: 99, step: 0.01, sortOrder: 2000, unknown2: 1)]
			[DisplayName("Effect α3 to the slope of the graph of growth Seoul threshold after")]
			[Description("Effect α3 to the slope of the graph of growth Seoul threshold after")]
			[DefaultValue((Single)0)]
			public Single Boundry_inclination_soul {
				get { return boundry_inclination_soul; }
				set {
					if ((double)value < -99 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -99 to 99 for Boundry_inclination_soul.");
					SetProperty(ref boundry_inclination_soul, ref value, Boundry_inclination_soulProperty);
				}
			}

			/// <summary>Seoul growth threshold t</summary>
			/// <remarks>
			/// Japanese short name: "成長ソウル 閾値 t", Google translated: "Seoul growth threshold t".
			/// Japanese description: "成長ソウル 閾値 t", Google translated: "Seoul growth threshold t".
			/// </remarks>
			[ParameterTableRowAttribute("boundry_value", index: 18, minimum: 0, maximum: 1000000, step: 1, sortOrder: 2100, unknown2: 1)]
			[DisplayName("Seoul growth threshold t")]
			[Description("Seoul growth threshold t")]
			[DefaultValue((Single)0)]
			public Single Boundry_value {
				get { return boundry_value; }
				set {
					if ((double)value < 0 || (double)value > 1000000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1000000 for Boundry_value.");
					SetProperty(ref boundry_value, ref value, Boundry_valueProperty);
				}
			}

			/// <summary>Padding</summary>
			/// <remarks>
			/// Japanese short name: "パディング", Google translated: "Padding".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("pad[4]", index: 19, minimum: 0, maximum: 0, step: 0, sortOrder: 2101, unknown2: 0)]
			[DisplayName("Padding")]
			[Description("")]
			[Browsable(false)]
			public Byte[] Pad {
				get { return pad; }
				set { SetProperty(ref pad, ref value, PadProperty); }
			}

			internal CalculationCorrection(ParameterTable table, int index, AssetLoader loader, int next)
				: base(table, index, loader) {
				BinaryReader reader = loader.Reader;

				StageMaxVal0 = reader.ReadSingle();
				StageMaxVal1 = reader.ReadSingle();
				StageMaxVal2 = reader.ReadSingle();
				StageMaxVal3 = reader.ReadSingle();
				StageMaxVal4 = reader.ReadSingle();
				StageMaxGrowVal0 = reader.ReadSingle();
				StageMaxGrowVal1 = reader.ReadSingle();
				StageMaxGrowVal2 = reader.ReadSingle();
				StageMaxGrowVal3 = reader.ReadSingle();
				StageMaxGrowVal4 = reader.ReadSingle();
				AdjPt_maxGrowVal0 = reader.ReadSingle();
				AdjPt_maxGrowVal1 = reader.ReadSingle();
				AdjPt_maxGrowVal2 = reader.ReadSingle();
				AdjPt_maxGrowVal3 = reader.ReadSingle();
				AdjPt_maxGrowVal4 = reader.ReadSingle();
				Init_inclination_soul = reader.ReadSingle();
				Adjustment_value = reader.ReadSingle();
				Boundry_inclination_soul = reader.ReadSingle();
				Boundry_value = reader.ReadSingle();
				Pad = reader.ReadBytes(4);
			}

			internal CalculationCorrection(ParameterTable table, int index)
				: base(table, index) {
				StageMaxVal0 = (Single)0;
				StageMaxVal1 = (Single)0;
				StageMaxVal2 = (Single)0;
				StageMaxVal3 = (Single)0;
				StageMaxVal4 = (Single)0;
				StageMaxGrowVal0 = (Single)0;
				StageMaxGrowVal1 = (Single)0;
				StageMaxGrowVal2 = (Single)0;
				StageMaxGrowVal3 = (Single)0;
				StageMaxGrowVal4 = (Single)0;
				AdjPt_maxGrowVal0 = (Single)0;
				AdjPt_maxGrowVal1 = (Single)0;
				AdjPt_maxGrowVal2 = (Single)0;
				AdjPt_maxGrowVal3 = (Single)0;
				AdjPt_maxGrowVal4 = (Single)0;
				Init_inclination_soul = (Single)0;
				Adjustment_value = (Single)0;
				Boundry_inclination_soul = (Single)0;
				Boundry_value = (Single)0;
				Pad = new Byte[4];
			}

			/// <summary>Write the row to the writer.</summary>
			/// <param name="writer"></param>
			public override void Write(BinaryWriter writer) {
				writer.Write(StageMaxVal0);
				writer.Write(StageMaxVal1);
				writer.Write(StageMaxVal2);
				writer.Write(StageMaxVal3);
				writer.Write(StageMaxVal4);
				writer.Write(StageMaxGrowVal0);
				writer.Write(StageMaxGrowVal1);
				writer.Write(StageMaxGrowVal2);
				writer.Write(StageMaxGrowVal3);
				writer.Write(StageMaxGrowVal4);
				writer.Write(AdjPt_maxGrowVal0);
				writer.Write(AdjPt_maxGrowVal1);
				writer.Write(AdjPt_maxGrowVal2);
				writer.Write(AdjPt_maxGrowVal3);
				writer.Write(AdjPt_maxGrowVal4);
				writer.Write(Init_inclination_soul);
				writer.Write(Adjustment_value);
				writer.Write(Boundry_inclination_soul);
				writer.Write(Boundry_value);
				writer.Write(Pad);
			}
		}
	}
}
