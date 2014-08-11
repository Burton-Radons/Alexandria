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
		/// Defined as "TONE_CORRECT_BANK" in Dark Souls in the file "ToneCorrectBank.paramdef" (id 07h).
		/// </remarks>
		public class ToneCorrection : ParameterTableRow {
			/// <summary>The name of the table in the file.</summary>
			public const string TableName = "TONE_CORRECT_BANK";

			Single brightnessR, brightnessG, brightnessB, contrastR, contrastG, contrastB, saturation, hue;

			/// <summary>A property in the class.</summary>
			public static readonly PropertyInfo
				BrightnessRProperty = GetProperty<ToneCorrection>("BrightnessR"),
				BrightnessGProperty = GetProperty<ToneCorrection>("BrightnessG"),
				BrightnessBProperty = GetProperty<ToneCorrection>("BrightnessB"),
				ContrastRProperty = GetProperty<ToneCorrection>("ContrastR"),
				ContrastGProperty = GetProperty<ToneCorrection>("ContrastG"),
				ContrastBProperty = GetProperty<ToneCorrection>("ContrastB"),
				SaturationProperty = GetProperty<ToneCorrection>("Saturation"),
				HueProperty = GetProperty<ToneCorrection>("Hue");

			/// <summary>Brightness R</summary>
			/// <remarks>
			/// Japanese short name: "輝度R", Google translated: "Brightness R".
			/// Japanese description: "輝度", Google translated: "Brightness".
			/// </remarks>
			[ParameterTableRowAttribute("brightnessR", index: 0, minimum: 0, maximum: 5, step: 0.01, sortOrder: 1, unknown2: 0)]
			[DisplayName("Brightness R")]
			[Description("Brightness")]
			[DefaultValue((Single)1)]
			public Single BrightnessR {
				get { return brightnessR; }
				set {
					if ((double)value < 0 || (double)value > 5)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 5 for " + BrightnessRProperty.Name + ".");
					SetProperty(ref brightnessR, ref value, BrightnessRProperty);
				}
			}

			/// <summary>Brightness G</summary>
			/// <remarks>
			/// Japanese short name: "輝度G", Google translated: "Brightness G".
			/// Japanese description: "輝度", Google translated: "Brightness".
			/// </remarks>
			[ParameterTableRowAttribute("brightnessG", index: 1, minimum: 0, maximum: 5, step: 0.01, sortOrder: 2, unknown2: 0)]
			[DisplayName("Brightness G")]
			[Description("Brightness")]
			[DefaultValue((Single)1)]
			public Single BrightnessG {
				get { return brightnessG; }
				set {
					if ((double)value < 0 || (double)value > 5)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 5 for " + BrightnessGProperty.Name + ".");
					SetProperty(ref brightnessG, ref value, BrightnessGProperty);
				}
			}

			/// <summary>Brightness B</summary>
			/// <remarks>
			/// Japanese short name: "輝度B", Google translated: "Brightness B".
			/// Japanese description: "輝度", Google translated: "Brightness".
			/// </remarks>
			[ParameterTableRowAttribute("brightnessB", index: 2, minimum: 0, maximum: 5, step: 0.01, sortOrder: 3, unknown2: 0)]
			[DisplayName("Brightness B")]
			[Description("Brightness")]
			[DefaultValue((Single)1)]
			public Single BrightnessB {
				get { return brightnessB; }
				set {
					if ((double)value < 0 || (double)value > 5)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 5 for " + BrightnessBProperty.Name + ".");
					SetProperty(ref brightnessB, ref value, BrightnessBProperty);
				}
			}

			/// <summary>Contrast R</summary>
			/// <remarks>
			/// Japanese short name: "コントラストR", Google translated: "Contrast R".
			/// Japanese description: "コントラスト", Google translated: "Contrast".
			/// </remarks>
			[ParameterTableRowAttribute("contrastR", index: 3, minimum: -5, maximum: 5, step: 0.01, sortOrder: 4, unknown2: 0)]
			[DisplayName("Contrast R")]
			[Description("Contrast")]
			[DefaultValue((Single)1)]
			public Single ContrastR {
				get { return contrastR; }
				set {
					if ((double)value < -5 || (double)value > 5)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -5 to 5 for " + ContrastRProperty.Name + ".");
					SetProperty(ref contrastR, ref value, ContrastRProperty);
				}
			}

			/// <summary>Contrast G</summary>
			/// <remarks>
			/// Japanese short name: "コントラストG", Google translated: "Contrast G".
			/// Japanese description: "コントラスト", Google translated: "Contrast".
			/// </remarks>
			[ParameterTableRowAttribute("contrastG", index: 4, minimum: -5, maximum: 5, step: 0.01, sortOrder: 5, unknown2: 0)]
			[DisplayName("Contrast G")]
			[Description("Contrast")]
			[DefaultValue((Single)1)]
			public Single ContrastG {
				get { return contrastG; }
				set {
					if ((double)value < -5 || (double)value > 5)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -5 to 5 for " + ContrastGProperty.Name + ".");
					SetProperty(ref contrastG, ref value, ContrastGProperty);
				}
			}

			/// <summary>Contrast B</summary>
			/// <remarks>
			/// Japanese short name: "コントラストB", Google translated: "Contrast B".
			/// Japanese description: "コントラスト", Google translated: "Contrast".
			/// </remarks>
			[ParameterTableRowAttribute("contrastB", index: 5, minimum: -5, maximum: 5, step: 0.01, sortOrder: 6, unknown2: 0)]
			[DisplayName("Contrast B")]
			[Description("Contrast")]
			[DefaultValue((Single)1)]
			public Single ContrastB {
				get { return contrastB; }
				set {
					if ((double)value < -5 || (double)value > 5)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -5 to 5 for " + ContrastBProperty.Name + ".");
					SetProperty(ref contrastB, ref value, ContrastBProperty);
				}
			}

			/// <summary>Saturation</summary>
			/// <remarks>
			/// Japanese short name: "彩度", Google translated: "Saturation".
			/// Japanese description: "彩度", Google translated: "Saturation".
			/// </remarks>
			[ParameterTableRowAttribute("saturation", index: 6, minimum: -5, maximum: 5, step: 0.01, sortOrder: 7, unknown2: 0)]
			[DisplayName("Saturation")]
			[Description("Saturation")]
			[DefaultValue((Single)1)]
			public Single Saturation {
				get { return saturation; }
				set {
					if ((double)value < -5 || (double)value > 5)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -5 to 5 for " + SaturationProperty.Name + ".");
					SetProperty(ref saturation, ref value, SaturationProperty);
				}
			}

			/// <summary>Hue</summary>
			/// <remarks>
			/// Japanese short name: "色相", Google translated: "Hue".
			/// Japanese description: "色相", Google translated: "Hue".
			/// </remarks>
			[ParameterTableRowAttribute("hue", index: 7, minimum: 0, maximum: 360, step: 1, sortOrder: 8, unknown2: 1)]
			[DisplayName("Hue")]
			[Description("Hue")]
			[DefaultValue((Single)0)]
			public Single Hue {
				get { return hue; }
				set {
					if ((double)value < 0 || (double)value > 360)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 360 for " + HueProperty.Name + ".");
					SetProperty(ref hue, ref value, HueProperty);
				}
			}

			internal ToneCorrection(ParameterTable table, int index, AssetLoader loader, int next)
				: base(table, index, loader) {
				BinaryReader reader = loader.Reader;

				BrightnessR = reader.ReadSingle();
				BrightnessG = reader.ReadSingle();
				BrightnessB = reader.ReadSingle();
				ContrastR = reader.ReadSingle();
				ContrastG = reader.ReadSingle();
				ContrastB = reader.ReadSingle();
				Saturation = reader.ReadSingle();
				Hue = reader.ReadSingle();
			}

			internal ToneCorrection(ParameterTable table, int index)
				: base(table, index) {
				BrightnessR = (Single)1;
				BrightnessG = (Single)1;
				BrightnessB = (Single)1;
				ContrastR = (Single)1;
				ContrastG = (Single)1;
				ContrastB = (Single)1;
				Saturation = (Single)1;
				Hue = (Single)0;
			}

			/// <summary>
			/// Write the <see cref="ToneCorrection"/> row to the stream.
			/// </summary>
			/// <param name="writer"></param>
			public override void Write(BinaryWriter writer) {
				writer.Write(BrightnessR);
				writer.Write(BrightnessG);
				writer.Write(BrightnessB);
				writer.Write(ContrastR);
				writer.Write(ContrastG);
				writer.Write(ContrastB);
				writer.Write(Saturation);
				writer.Write(Hue);
			}
		}
	}
}
