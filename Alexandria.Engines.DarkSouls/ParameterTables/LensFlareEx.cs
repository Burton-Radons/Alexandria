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
		/// Named "LENS_FLARE_EX_BANK" in Dark Souls; defined in the file "ensFlareExBank.paramdef" (id 02h).
		/// </summary>
		public class LensFlareEx : ParameterTableRow {
			/// <summary>The name of the table in the file.</summary>
			public const string TableName = "LENS_FLARE_EX_BANK";

			Int16 lightDegRotX, lightDegRotY, colR, colG, colB, colA;
			Single lightDist;

			/// <summary>A property of the class.</summary>
			public static readonly PropertyInfo
				LightDegRotXProperty = GetProperty<LensFlareEx>("LightDegRotX"),
				LightDegRotYProperty = GetProperty<LensFlareEx>("LightDegRotY"),
				ColRProperty = GetProperty<LensFlareEx>("ColR"),
				ColGProperty = GetProperty<LensFlareEx>("ColG"),
				ColBProperty = GetProperty<LensFlareEx>("ColB"),
				ColAProperty = GetProperty<LensFlareEx>("ColA"),
				LightDistProperty = GetProperty<LensFlareEx>("LightDist");

			/// <summary>X light source angle [deg]</summary>
			/// <remarks>
			/// Japanese short name: "光源X角度[deg]", Google translated: "X light source angle [deg]".
			/// Japanese description: "光源のX角度", Google translated: "X angle of the light source".
			/// </remarks>
			[ParameterTableRowAttribute("lightDegRotX", index: 0, minimum: -90, maximum: 90, step: 1, sortOrder: 1, unknown2: 0)]
			[DisplayName("X light source angle [deg]")]
			[Description("X angle of the light source")]
			[DefaultValue((Int16)0)]
			public Int16 LightDegRotX {
				get { return lightDegRotX; }
				set {
					if ((double)value < -90 || (double)value > 90)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -90 to 90 for LightDegRotX.");
					SetProperty(ref lightDegRotX, ref value, LightDegRotXProperty);
				}
			}

			/// <summary>Light source Y angle [deg]</summary>
			/// <remarks>
			/// Japanese short name: "光源Y角度[deg]", Google translated: "Light source Y angle [deg]".
			/// Japanese description: "光源のY角度", Google translated: "Y angle of the light source".
			/// </remarks>
			[ParameterTableRowAttribute("lightDegRotY", index: 1, minimum: -180, maximum: 180, step: 1, sortOrder: 2, unknown2: 1)]
			[DisplayName("Light source Y angle [deg]")]
			[Description("Y angle of the light source")]
			[DefaultValue((Int16)0)]
			public Int16 LightDegRotY {
				get { return lightDegRotY; }
				set {
					if ((double)value < -180 || (double)value > 180)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -180 to 180 for LightDegRotY.");
					SetProperty(ref lightDegRotY, ref value, LightDegRotYProperty);
				}
			}

			/// <summary>Color R</summary>
			/// <remarks>
			/// Japanese short name: "色R", Google translated: "Color R".
			/// Japanese description: "レンズフレア乗算色", Google translated: "Lens flare multiply color".
			/// </remarks>
			[ParameterTableRowAttribute("colR", index: 2, minimum: 0, maximum: 255, step: 1, sortOrder: 3, unknown2: 0)]
			[DisplayName("Color R")]
			[Description("Lens flare multiply color")]
			[DefaultValue((Int16)255)]
			public Int16 ColR {
				get { return colR; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColR.");
					SetProperty(ref colR, ref value, ColRProperty);
				}
			}

			/// <summary>Color G</summary>
			/// <remarks>
			/// Japanese short name: "色G", Google translated: "Color G".
			/// Japanese description: "レンズフレア乗算色", Google translated: "Lens flare multiply color".
			/// </remarks>
			[ParameterTableRowAttribute("colG", index: 3, minimum: 0, maximum: 255, step: 1, sortOrder: 4, unknown2: 0)]
			[DisplayName("Color G")]
			[Description("Lens flare multiply color")]
			[DefaultValue((Int16)255)]
			public Int16 ColG {
				get { return colG; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColG.");
					SetProperty(ref colG, ref value, ColGProperty);
				}
			}

			/// <summary>Color B</summary>
			/// <remarks>
			/// Japanese short name: "色B", Google translated: "Color B".
			/// Japanese description: "レンズフレア乗算色", Google translated: "Lens flare multiply color".
			/// </remarks>
			[ParameterTableRowAttribute("colB", index: 4, minimum: 0, maximum: 255, step: 1, sortOrder: 5, unknown2: 0)]
			[DisplayName("Color B")]
			[Description("Lens flare multiply color")]
			[DefaultValue((Int16)255)]
			public Int16 ColB {
				get { return colB; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColB.");
					SetProperty(ref colB, ref value, ColBProperty);
				}
			}

			/// <summary>RGB color magnification [%]</summary>
			/// <remarks>
			/// Japanese short name: "色RGB倍率[％]", Google translated: "RGB color magnification [%]".
			/// Japanese description: "レンズフレア乗算色", Google translated: "Lens flare multiply color".
			/// </remarks>
			[ParameterTableRowAttribute("colA", index: 5, minimum: 0, maximum: 1000, step: 1, sortOrder: 6, unknown2: 0)]
			[DisplayName("RGB color magnification [%]")]
			[Description("Lens flare multiply color")]
			[DefaultValue((Int16)100)]
			public Int16 ColA {
				get { return colA; }
				set {
					if ((double)value < 0 || (double)value > 1000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1000 for ColA.");
					SetProperty(ref colA, ref value, ColAProperty);
				}
			}

			/// <summary>Light source distance [m]</summary>
			/// <remarks>
			/// Japanese short name: "光源距離[m]", Google translated: "Light source distance [m]".
			/// Japanese description: "光源距離", Google translated: "Light source distance".
			/// </remarks>
			[ParameterTableRowAttribute("lightDist", index: 6, minimum: 0, maximum: 999999, step: 0.1, sortOrder: 7, unknown2: 0)]
			[DisplayName("Light source distance [m]")]
			[Description("Light source distance")]
			[DefaultValue((Single)300)]
			public Single LightDist {
				get { return lightDist; }
				set {
					if ((double)value < 0 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999999 for LightDist.");
					SetProperty(ref lightDist, ref value, LightDistProperty);
				}
			}

			internal LensFlareEx(ParameterTable table, int index, AssetLoader loader, int next)
				: base(table, index, loader) {
				BinaryReader reader = loader.Reader;

				LightDegRotX = reader.ReadInt16();
				LightDegRotY = reader.ReadInt16();
				ColR = reader.ReadInt16();
				ColG = reader.ReadInt16();
				ColB = reader.ReadInt16();
				ColA = reader.ReadInt16();
				LightDist = reader.ReadSingle();
			}

			internal LensFlareEx(ParameterTable table, int index)
				: base(table, index) {
				LightDegRotX = (Int16)0;
				LightDegRotY = (Int16)0;
				ColR = (Int16)255;
				ColG = (Int16)255;
				ColB = (Int16)255;
				ColA = (Int16)100;
				LightDist = (Single)300;
			}

			/// <summary>Write the <see cref="LensFlareEx"/> row.</summary>
			/// <param name="writer"></param>
			public override void Write(BinaryWriter writer) {
				writer.Write(LightDegRotX);
				writer.Write(LightDegRotY);
				writer.Write(ColR);
				writer.Write(ColG);
				writer.Write(ColB);
				writer.Write(ColA);
				writer.Write(LightDist);
			}
		}
	}
}
