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
		/// Defined as "QWC_JUDGE_PARAM_ST" in Dark Souls in the file "QwcJudgeParam.paramdef" (id 28h).
		/// </remarks>
		public class QwcJudge : ParameterTableRow {
			public const string TableName = "QWC_JUDGE_PARAM_ST";

			Int16 pcJudgeUnderWB, pcJudgeTopWB, pcJudgeUnderLR, pcJudgeTopLR, areaJudgeUnderWB, areaJudgeTopWB, areaJudgeUnderLR, areaJudgeTopLR;

			public static readonly PropertyInfo
				PcJudgeUnderWBProperty = GetProperty<QwcJudge>("PcJudgeUnderWB"),
				PcJudgeTopWBProperty = GetProperty<QwcJudge>("PcJudgeTopWB"),
				PcJudgeUnderLRProperty = GetProperty<QwcJudge>("PcJudgeUnderLR"),
				PcJudgeTopLRProperty = GetProperty<QwcJudge>("PcJudgeTopLR"),
				AreaJudgeUnderWBProperty = GetProperty<QwcJudge>("AreaJudgeUnderWB"),
				AreaJudgeTopWBProperty = GetProperty<QwcJudge>("AreaJudgeTopWB"),
				AreaJudgeUnderLRProperty = GetProperty<QwcJudge>("AreaJudgeUnderLR"),
				AreaJudgeTopLRProperty = GetProperty<QwcJudge>("AreaJudgeTopLR");

			/// <summary>PC-black conditions (lower limit)</summary>
			/// <remarks>
			/// Japanese short name: "PC-黒条件（下限）", Google translated: "PC-black conditions (lower limit)".
			/// Japanese description: "PC白黒属性条件（下限）", Google translated: "PC black-and-white attribute condition (lower limit)".
			/// </remarks>
			[ParameterTableRowAttribute("pcJudgeUnderWB", index: 0, minimum: -200, maximum: 200, step: 1, order: 100, unknown2: 1)]
			[DisplayName("PC-black conditions (lower limit)")]
			[Description("PC black-and-white attribute condition (lower limit)")]
			[DefaultValue((Int16)(-200))]
			public Int16 PcJudgeUnderWB {
				get { return pcJudgeUnderWB; }
				set {
					if ((double)value < -200 || (double)value > 200)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -200 to 200 for PcJudgeUnderWB.");
					SetProperty(ref pcJudgeUnderWB, ref value, PcJudgeUnderWBProperty);
				}
			}

			/// <summary>PC-white conditions (upper limit)</summary>
			/// <remarks>
			/// Japanese short name: "PC-白条件（上限）", Google translated: "PC-white conditions (upper limit)".
			/// Japanese description: "PC白黒属性条件（上限）", Google translated: "PC black-and-white attribute condition (upper limit)".
			/// </remarks>
			[ParameterTableRowAttribute("pcJudgeTopWB", index: 1, minimum: -200, maximum: 200, step: 1, order: 200, unknown2: 1)]
			[DisplayName("PC-white conditions (upper limit)")]
			[Description("PC black-and-white attribute condition (upper limit)")]
			[DefaultValue((Int16)200)]
			public Int16 PcJudgeTopWB {
				get { return pcJudgeTopWB; }
				set {
					if ((double)value < -200 || (double)value > 200)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -200 to 200 for PcJudgeTopWB.");
					SetProperty(ref pcJudgeTopWB, ref value, PcJudgeTopWBProperty);
				}
			}

			/// <summary>PC-left condition (lower limit)</summary>
			/// <remarks>
			/// Japanese short name: "PC-左条件（下限）", Google translated: "PC-left condition (lower limit)".
			/// Japanese description: "PC左右属性条件（下限）", Google translated: "PC left and right attribute condition (lower limit)".
			/// </remarks>
			[ParameterTableRowAttribute("pcJudgeUnderLR", index: 2, minimum: -200, maximum: 200, step: 1, order: 300, unknown2: 1)]
			[DisplayName("PC-left condition (lower limit)")]
			[Description("PC left and right attribute condition (lower limit)")]
			[DefaultValue((Int16)(-200))]
			public Int16 PcJudgeUnderLR {
				get { return pcJudgeUnderLR; }
				set {
					if ((double)value < -200 || (double)value > 200)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -200 to 200 for PcJudgeUnderLR.");
					SetProperty(ref pcJudgeUnderLR, ref value, PcJudgeUnderLRProperty);
				}
			}

			/// <summary>PC-right conditions (upper limit)</summary>
			/// <remarks>
			/// Japanese short name: "PC-右条件（上限）", Google translated: "PC-right conditions (upper limit)".
			/// Japanese description: "PC左右属性条件（上限）", Google translated: "PC left and right attribute condition (upper limit)".
			/// </remarks>
			[ParameterTableRowAttribute("pcJudgeTopLR", index: 3, minimum: -200, maximum: 200, step: 1, order: 400, unknown2: 1)]
			[DisplayName("PC-right conditions (upper limit)")]
			[Description("PC left and right attribute condition (upper limit)")]
			[DefaultValue((Int16)200)]
			public Int16 PcJudgeTopLR {
				get { return pcJudgeTopLR; }
				set {
					if ((double)value < -200 || (double)value > 200)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -200 to 200 for PcJudgeTopLR.");
					SetProperty(ref pcJudgeTopLR, ref value, PcJudgeTopLRProperty);
				}
			}

			/// <summary>Area - black conditions (lower limit)</summary>
			/// <remarks>
			/// Japanese short name: "エリア-黒条件（下限）", Google translated: "Area - black conditions (lower limit)".
			/// Japanese description: "エリア白黒属性条件（下限）", Google translated: "Black-and-white area attribute condition (lower limit)".
			/// </remarks>
			[ParameterTableRowAttribute("areaJudgeUnderWB", index: 4, minimum: -200, maximum: 200, step: 1, order: 500, unknown2: 1)]
			[DisplayName("Area - black conditions (lower limit)")]
			[Description("Black-and-white area attribute condition (lower limit)")]
			[DefaultValue((Int16)(-200))]
			public Int16 AreaJudgeUnderWB {
				get { return areaJudgeUnderWB; }
				set {
					if ((double)value < -200 || (double)value > 200)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -200 to 200 for AreaJudgeUnderWB.");
					SetProperty(ref areaJudgeUnderWB, ref value, AreaJudgeUnderWBProperty);
				}
			}

			/// <summary>Area - white conditions (upper limit)</summary>
			/// <remarks>
			/// Japanese short name: "エリア-白条件（上限）", Google translated: "Area - white conditions (upper limit)".
			/// Japanese description: "エリア白黒属性条件（上限）", Google translated: "Black-and-white area attribute condition (upper limit)".
			/// </remarks>
			[ParameterTableRowAttribute("areaJudgeTopWB", index: 5, minimum: -200, maximum: 200, step: 1, order: 600, unknown2: 1)]
			[DisplayName("Area - white conditions (upper limit)")]
			[Description("Black-and-white area attribute condition (upper limit)")]
			[DefaultValue((Int16)200)]
			public Int16 AreaJudgeTopWB {
				get { return areaJudgeTopWB; }
				set {
					if ((double)value < -200 || (double)value > 200)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -200 to 200 for AreaJudgeTopWB.");
					SetProperty(ref areaJudgeTopWB, ref value, AreaJudgeTopWBProperty);
				}
			}

			/// <summary>Area - left condition (lower limit)</summary>
			/// <remarks>
			/// Japanese short name: "エリア-左条件（下限）", Google translated: "Area - left condition (lower limit)".
			/// Japanese description: "エリア左右属性条件（下限）", Google translated: "Area left and right attribute condition (lower limit)".
			/// </remarks>
			[ParameterTableRowAttribute("areaJudgeUnderLR", index: 6, minimum: -200, maximum: 200, step: 1, order: 700, unknown2: 1)]
			[DisplayName("Area - left condition (lower limit)")]
			[Description("Area left and right attribute condition (lower limit)")]
			[DefaultValue((Int16)(-200))]
			public Int16 AreaJudgeUnderLR {
				get { return areaJudgeUnderLR; }
				set {
					if ((double)value < -200 || (double)value > 200)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -200 to 200 for AreaJudgeUnderLR.");
					SetProperty(ref areaJudgeUnderLR, ref value, AreaJudgeUnderLRProperty);
				}
			}

			/// <summary>Area - right conditions (upper limit)</summary>
			/// <remarks>
			/// Japanese short name: "エリア-右条件（上限）", Google translated: "Area - right conditions (upper limit)".
			/// Japanese description: "エリア左右属性条件（上限）", Google translated: "Area left and right attribute condition (upper limit)".
			/// </remarks>
			[ParameterTableRowAttribute("areaJudgeTopLR", index: 7, minimum: -200, maximum: 200, step: 1, order: 800, unknown2: 1)]
			[DisplayName("Area - right conditions (upper limit)")]
			[Description("Area left and right attribute condition (upper limit)")]
			[DefaultValue((Int16)200)]
			public Int16 AreaJudgeTopLR {
				get { return areaJudgeTopLR; }
				set {
					if ((double)value < -200 || (double)value > 200)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -200 to 200 for AreaJudgeTopLR.");
					SetProperty(ref areaJudgeTopLR, ref value, AreaJudgeTopLRProperty);
				}
			}

			internal QwcJudge(ParameterTable table, int index, AssetLoader loader, int next)
				: base(table, index, loader) {
				BinaryReader reader = loader.Reader;

				PcJudgeUnderWB = reader.ReadInt16();
				PcJudgeTopWB = reader.ReadInt16();
				PcJudgeUnderLR = reader.ReadInt16();
				PcJudgeTopLR = reader.ReadInt16();
				AreaJudgeUnderWB = reader.ReadInt16();
				AreaJudgeTopWB = reader.ReadInt16();
				AreaJudgeUnderLR = reader.ReadInt16();
				AreaJudgeTopLR = reader.ReadInt16();
			}

			internal QwcJudge(ParameterTable table, int index)
				: base(table, index) {
				PcJudgeUnderWB = (Int16)(-200);
				PcJudgeTopWB = (Int16)200;
				PcJudgeUnderLR = (Int16)(-200);
				PcJudgeTopLR = (Int16)200;
				AreaJudgeUnderWB = (Int16)(-200);
				AreaJudgeTopWB = (Int16)200;
				AreaJudgeUnderLR = (Int16)(-200);
				AreaJudgeTopLR = (Int16)200;
			}

			public override void Write(BinaryWriter writer) {
				writer.Write(PcJudgeUnderWB);
				writer.Write(PcJudgeTopWB);
				writer.Write(PcJudgeUnderLR);
				writer.Write(PcJudgeTopLR);
				writer.Write(AreaJudgeUnderWB);
				writer.Write(AreaJudgeTopWB);
				writer.Write(AreaJudgeUnderLR);
				writer.Write(AreaJudgeTopLR);
			}
		}
	}
}
