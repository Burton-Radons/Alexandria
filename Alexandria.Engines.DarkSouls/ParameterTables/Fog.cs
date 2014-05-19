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
		/// From "FogBank.paramdef" (id 00h).
		/// </remarks>
		public class Fog : ParameterTableRow {
			public const string TableName = "FOG_BANK";

			Int16 fogBeginZ, fogEndZ, degRotZ, degRotW, colR, colG, colB, colA;

			public static readonly PropertyInfo
				FogBeginZProperty = GetProperty<Fog>("FogBeginZ"),
				FogEndZProperty = GetProperty<Fog>("FogEndZ"),
				DegRotZProperty = GetProperty<Fog>("DegRotZ"),
				DegRotWProperty = GetProperty<Fog>("DegRotW"),
				ColRProperty = GetProperty<Fog>("ColR"),
				ColGProperty = GetProperty<Fog>("ColG"),
				ColBProperty = GetProperty<Fog>("ColB"),
				ColAProperty = GetProperty<Fog>("ColA");

			/// <summary>Start distance [m]</summary>
			/// <remarks>
			/// Japanese short name: "開始距離[m]", Google translated: "Start distance [m]".
			/// Japanese description: "フォグの開始距離です", Google translated: "The starting distance of fog".
			/// </remarks>
			[ParameterTableRowAttribute("fogBeginZ", index: 0, minimum: -10000, maximum: 10000, step: 1, order: 1, unknown2: 0)]
			[DisplayName("Start distance [m]")]
			[Description("The starting distance of fog")]
			[DefaultValue((Int16)0)]
			public Int16 FogBeginZ {
				get { return fogBeginZ; }
				set {
					if ((double)value < -10000 || (double)value > 10000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -10000 to 10000 for FogBeginZ.");
					SetProperty(ref fogBeginZ, ref value, FogBeginZProperty);
				}
			}

			/// <summary>End distance [m]</summary>
			/// <remarks>
			/// Japanese short name: "終了距離[m]", Google translated: "End distance [m]".
			/// Japanese description: "フォグの終了距離です", Google translated: "The end distance of fog".
			/// </remarks>
			[ParameterTableRowAttribute("fogEndZ", index: 1, minimum: -10000, maximum: 10000, step: 1, order: 2, unknown2: 0)]
			[DisplayName("End distance [m]")]
			[Description("The end distance of fog")]
			[DefaultValue((Int16)100)]
			public Int16 FogEndZ {
				get { return fogEndZ; }
				set {
					if ((double)value < -10000 || (double)value > 10000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -10000 to 10000 for FogEndZ.");
					SetProperty(ref fogEndZ, ref value, FogEndZProperty);
				}
			}

			/// <summary>Dummy</summary>
			/// <remarks>
			/// Japanese short name: "ダミー", Google translated: "Dummy".
			/// Japanese description: "ダミー", Google translated: "Dummy".
			/// </remarks>
			[ParameterTableRowAttribute("degRotZ", index: 2, minimum: 0, maximum: 0, step: 0, order: 3, unknown2: 0)]
			[DisplayName("Dummy")]
			[Description("Dummy")]
			[DefaultValue((Int16)0)]
			[Browsable(false)]
			public Int16 DegRotZ {
				get { return degRotZ; }
				set {
					if ((double)value < 0 || (double)value > 0)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 0 for DegRotZ.");
					SetProperty(ref degRotZ, ref value, DegRotZProperty);
				}
			}

			/// <summary>Strength</summary>
			/// <remarks>
			/// Japanese short name: "強度", Google translated: "Strength".
			/// Japanese description: "通常100にしてください(0だとフォグが掛かりません)", Google translated: "And (fog does not take were zero), please to the normal 100".
			/// </remarks>
			[ParameterTableRowAttribute("degRotW", index: 3, minimum: 0, maximum: 1000, step: 1, order: 4, unknown2: 0)]
			[DisplayName("Strength")]
			[Description("And (fog does not take were zero), please to the normal 100")]
			[DefaultValue((Int16)100)]
			public Int16 DegRotW {
				get { return degRotW; }
				set {
					if ((double)value < 0 || (double)value > 1000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1000 for DegRotW.");
					SetProperty(ref degRotW, ref value, DegRotWProperty);
				}
			}

			/// <summary>R</summary>
			/// <remarks>
			/// Japanese short name: "Ｒ", Google translated: "R".
			/// Japanese description: "フォグカラー", Google translated: "Fog color".
			/// </remarks>
			[ParameterTableRowAttribute("colR", index: 4, minimum: 0, maximum: 255, step: 1, order: 5, unknown2: 0)]
			[DisplayName("R")]
			[Description("Fog color")]
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
			/// Japanese description: "フォグカラー", Google translated: "Fog color".
			/// </remarks>
			[ParameterTableRowAttribute("colG", index: 5, minimum: 0, maximum: 255, step: 1, order: 6, unknown2: 0)]
			[DisplayName("G")]
			[Description("Fog color")]
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
			/// Japanese description: "フォグカラー", Google translated: "Fog color".
			/// </remarks>
			[ParameterTableRowAttribute("colB", index: 6, minimum: 0, maximum: 255, step: 1, order: 7, unknown2: 0)]
			[DisplayName("B")]
			[Description("Fog color")]
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
			/// Japanese description: "フォグカラーの倍率(100が標準)", Google translated: "(100 is standard) magnification of the fog color".
			/// </remarks>
			[ParameterTableRowAttribute("colA", index: 7, minimum: 0, maximum: 1000, step: 1, order: 8, unknown2: 0)]
			[DisplayName("RGB magnification [%]")]
			[Description("(100 is standard) magnification of the fog color")]
			[DefaultValue((Int16)100)]
			public Int16 ColA {
				get { return colA; }
				set {
					if ((double)value < 0 || (double)value > 1000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1000 for ColA.");
					SetProperty(ref colA, ref value, ColAProperty);
				}
			}

			internal Fog(ParameterTable table, int index, AssetLoader loader, int next)
				: base(table, index, loader) {
				BinaryReader reader = loader.Reader;

				FogBeginZ = reader.ReadInt16();
				FogEndZ = reader.ReadInt16();
				DegRotZ = reader.ReadInt16();
				DegRotW = reader.ReadInt16();
				ColR = reader.ReadInt16();
				ColG = reader.ReadInt16();
				ColB = reader.ReadInt16();
				ColA = reader.ReadInt16();
			}

			internal Fog(ParameterTable table, int index)
				: base(table, index) {
				FogBeginZ = (Int16)0;
				FogEndZ = (Int16)100;
				DegRotZ = (Int16)0;
				DegRotW = (Int16)100;
				ColR = (Int16)255;
				ColG = (Int16)255;
				ColB = (Int16)255;
				ColA = (Int16)100;
			}

			public override void Write(BinaryWriter writer) {
				writer.Write(FogBeginZ);
				writer.Write(FogEndZ);
				writer.Write(DegRotZ);
				writer.Write(DegRotW);
				writer.Write(ColR);
				writer.Write(ColG);
				writer.Write(ColB);
				writer.Write(ColA);
			}
		}
	}
}
