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
		/// Defined as "POINT_LIGHT_BANK" in Dark Souls in the file "PointLightBank.paramdef" (id 03h).
		/// </remarks>
		public class PointLight : ParameterTableRow {
			/// <summary>The name of the table in the file.</summary>
			public const string TableName = "POINT_LIGHT_BANK";

			Single dwindleBegin, dwindleEnd;
			Int16 colR, colG, colB, colA;

			/// <summary>A property in the class.</summary>
			public static readonly PropertyInfo
				DwindleBeginProperty = GetProperty<PointLight>("DwindleBegin"),
				DwindleEndProperty = GetProperty<PointLight>("DwindleEnd"),
				ColRProperty = GetProperty<PointLight>("ColR"),
				ColGProperty = GetProperty<PointLight>("ColG"),
				ColBProperty = GetProperty<PointLight>("ColB"),
				ColAProperty = GetProperty<PointLight>("ColA");

			/// <summary>Attenuation start distance [m]</summary>
			/// <remarks>
			/// Japanese short name: "減衰開始距離[m]", Google translated: "Attenuation start distance [m]".
			/// Japanese description: "点光源", Google translated: "Point light source".
			/// </remarks>
			[ParameterTableRowAttribute("dwindleBegin", index: 0, minimum: 0, maximum: 9999, step: 0.1, sortOrder: 1, unknown2: 0)]
			[DisplayName("Attenuation start distance [m]")]
			[Description("Point light source")]
			[DefaultValue((Single)0.5)]
			public Single DwindleBegin {
				get { return dwindleBegin; }
				set {
					if ((double)value < 0 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9999 for DwindleBegin.");
					SetProperty(ref dwindleBegin, ref value, DwindleBeginProperty);
				}
			}

			/// <summary>Attenuation end distance [m]</summary>
			/// <remarks>
			/// Japanese short name: "減衰終了距離[m]", Google translated: "Attenuation end distance [m]".
			/// Japanese description: "点光源", Google translated: "Point light source".
			/// </remarks>
			[ParameterTableRowAttribute("dwindleEnd", index: 1, minimum: 0, maximum: 9999, step: 0.1, sortOrder: 2, unknown2: 0)]
			[DisplayName("Attenuation end distance [m]")]
			[Description("Point light source")]
			[DefaultValue((Single)2)]
			public Single DwindleEnd {
				get { return dwindleEnd; }
				set {
					if ((double)value < 0 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9999 for DwindleEnd.");
					SetProperty(ref dwindleEnd, ref value, DwindleEndProperty);
				}
			}

			/// <summary>R</summary>
			/// <remarks>
			/// Japanese short name: "Ｒ", Google translated: "R".
			/// Japanese description: "点光源", Google translated: "Point light source".
			/// </remarks>
			[ParameterTableRowAttribute("colR", index: 2, minimum: 0, maximum: 255, step: 1, sortOrder: 3, unknown2: 0)]
			[DisplayName("R")]
			[Description("Point light source")]
			[DefaultValue((Int16)255)]
			public Int16 ColR {
				get { return colR; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColR.");
					SetProperty(ref colR, ref value, ColRProperty);
				}
			}

			/// <summary>G</summary>
			/// <remarks>
			/// Japanese short name: "Ｇ", Google translated: "G".
			/// Japanese description: "点光源", Google translated: "Point light source".
			/// </remarks>
			[ParameterTableRowAttribute("colG", index: 3, minimum: 0, maximum: 255, step: 1, sortOrder: 4, unknown2: 0)]
			[DisplayName("G")]
			[Description("Point light source")]
			[DefaultValue((Int16)255)]
			public Int16 ColG {
				get { return colG; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColG.");
					SetProperty(ref colG, ref value, ColGProperty);
				}
			}

			/// <summary>B</summary>
			/// <remarks>
			/// Japanese short name: "Ｂ", Google translated: "B".
			/// Japanese description: "点光源", Google translated: "Point light source".
			/// </remarks>
			[ParameterTableRowAttribute("colB", index: 4, minimum: 0, maximum: 255, step: 1, sortOrder: 5, unknown2: 0)]
			[DisplayName("B")]
			[Description("Point light source")]
			[DefaultValue((Int16)255)]
			public Int16 ColB {
				get { return colB; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColB.");
					SetProperty(ref colB, ref value, ColBProperty);
				}
			}

			/// <summary>RGB magnification [%]</summary>
			/// <remarks>
			/// Japanese short name: "RGB倍率[％]", Google translated: "RGB magnification [%]".
			/// Japanese description: "点光源", Google translated: "Point light source".
			/// </remarks>
			[ParameterTableRowAttribute("colA", index: 5, minimum: 0, maximum: 1000, step: 1, sortOrder: 6, unknown2: 0)]
			[DisplayName("RGB magnification [%]")]
			[Description("Point light source")]
			[DefaultValue((Int16)100)]
			public Int16 ColA {
				get { return colA; }
				set {
					if ((double)value < 0 || (double)value > 1000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1000 for ColA.");
					SetProperty(ref colA, ref value, ColAProperty);
				}
			}

			internal PointLight(ParameterTable table, int index, AssetLoader loader, int next)
				: base(table, index, loader) {
				BinaryReader reader = loader.Reader;

				DwindleBegin = reader.ReadSingle();
				DwindleEnd = reader.ReadSingle();
				ColR = reader.ReadInt16();
				ColG = reader.ReadInt16();
				ColB = reader.ReadInt16();
				ColA = reader.ReadInt16();
			}

			internal PointLight(ParameterTable table, int index)
				: base(table, index) {
				DwindleBegin = (Single)0.5;
				DwindleEnd = (Single)2;
				ColR = (Int16)255;
				ColG = (Int16)255;
				ColB = (Int16)255;
				ColA = (Int16)100;
			}

			/// <summary>
			/// Write the <see cref="PointLight"/> row.
			/// </summary>
			/// <param name="writer"></param>
			public override void Write(BinaryWriter writer) {
				writer.Write(DwindleBegin);
				writer.Write(DwindleEnd);
				writer.Write(ColR);
				writer.Write(ColG);
				writer.Write(ColB);
				writer.Write(ColA);
			}
		}
	}
}
