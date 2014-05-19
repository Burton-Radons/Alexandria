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
		/// Defined as "LIGHT_BANK" in the file "LightBank.paramdef" (id 01h).
		/// </remarks>
		public class Light : ParameterTableRow {
			public const string TableName = "LIGHT_BANK";

			Int16 degRotX_0, degRotY_0, colR_0, colG_0, colB_0, colA_0, degRotX_1, degRotY_1, colR_1, colG_1, colB_1, colA_1, degRotX_2, degRotY_2, colR_2, colG_2, colB_2, colA_2, colR_u, colG_u, colB_u, colA_u, colR_d, colG_d, colB_d, colA_d, degRotX_s, degRotY_s, colR_s, colG_s, colB_s, colA_s, envDif_colR, envDif_colG, envDif_colB, envDif_colA, envSpc_colR, envSpc_colG, envSpc_colB, envSpc_colA, envDif, envSpc_0, envSpc_1, envSpc_2, envSpc_3;
			Byte[] pad;

			public static readonly PropertyInfo
				DegRotX_0Property = GetProperty<Light>("DegRotX_0"),
				DegRotY_0Property = GetProperty<Light>("DegRotY_0"),
				ColR_0Property = GetProperty<Light>("ColR_0"),
				ColG_0Property = GetProperty<Light>("ColG_0"),
				ColB_0Property = GetProperty<Light>("ColB_0"),
				ColA_0Property = GetProperty<Light>("ColA_0"),
				DegRotX_1Property = GetProperty<Light>("DegRotX_1"),
				DegRotY_1Property = GetProperty<Light>("DegRotY_1"),
				ColR_1Property = GetProperty<Light>("ColR_1"),
				ColG_1Property = GetProperty<Light>("ColG_1"),
				ColB_1Property = GetProperty<Light>("ColB_1"),
				ColA_1Property = GetProperty<Light>("ColA_1"),
				DegRotX_2Property = GetProperty<Light>("DegRotX_2"),
				DegRotY_2Property = GetProperty<Light>("DegRotY_2"),
				ColR_2Property = GetProperty<Light>("ColR_2"),
				ColG_2Property = GetProperty<Light>("ColG_2"),
				ColB_2Property = GetProperty<Light>("ColB_2"),
				ColA_2Property = GetProperty<Light>("ColA_2"),
				ColR_uProperty = GetProperty<Light>("ColR_u"),
				ColG_uProperty = GetProperty<Light>("ColG_u"),
				ColB_uProperty = GetProperty<Light>("ColB_u"),
				ColA_uProperty = GetProperty<Light>("ColA_u"),
				ColR_dProperty = GetProperty<Light>("ColR_d"),
				ColG_dProperty = GetProperty<Light>("ColG_d"),
				ColB_dProperty = GetProperty<Light>("ColB_d"),
				ColA_dProperty = GetProperty<Light>("ColA_d"),
				DegRotX_sProperty = GetProperty<Light>("DegRotX_s"),
				DegRotY_sProperty = GetProperty<Light>("DegRotY_s"),
				ColR_sProperty = GetProperty<Light>("ColR_s"),
				ColG_sProperty = GetProperty<Light>("ColG_s"),
				ColB_sProperty = GetProperty<Light>("ColB_s"),
				ColA_sProperty = GetProperty<Light>("ColA_s"),
				EnvDif_colRProperty = GetProperty<Light>("EnvDif_colR"),
				EnvDif_colGProperty = GetProperty<Light>("EnvDif_colG"),
				EnvDif_colBProperty = GetProperty<Light>("EnvDif_colB"),
				EnvDif_colAProperty = GetProperty<Light>("EnvDif_colA"),
				EnvSpc_colRProperty = GetProperty<Light>("EnvSpc_colR"),
				EnvSpc_colGProperty = GetProperty<Light>("EnvSpc_colG"),
				EnvSpc_colBProperty = GetProperty<Light>("EnvSpc_colB"),
				EnvSpc_colAProperty = GetProperty<Light>("EnvSpc_colA"),
				EnvDifProperty = GetProperty<Light>("EnvDif"),
				EnvSpc_0Property = GetProperty<Light>("EnvSpc_0"),
				EnvSpc_1Property = GetProperty<Light>("EnvSpc_1"),
				EnvSpc_2Property = GetProperty<Light>("EnvSpc_2"),
				EnvSpc_3Property = GetProperty<Light>("EnvSpc_3"),
				PadProperty = GetProperty<Light>("Pad");

			/// <summary>X angle</summary>
			/// <remarks>
			/// Japanese short name: "X角度", Google translated: "X angle".
			/// Japanese description: "平行光源：０", Google translated: "Collimated light source : 0".
			/// </remarks>
			[ParameterTableRowAttribute("degRotX_0", index: 0, minimum: -90, maximum: 90, step: 1, order: 1, unknown2: 0)]
			[DisplayName("X angle")]
			[Description("Collimated light source : 0")]
			[DefaultValue((Int16)0)]
			public Int16 DegRotX_0 {
				get { return degRotX_0; }
				set {
					if ((double)value < -90 || (double)value > 90)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -90 to 90 for DegRotX_0.");
					SetProperty(ref degRotX_0, ref value, DegRotX_0Property);
				}
			}

			/// <summary>Y angle</summary>
			/// <remarks>
			/// Japanese short name: "Ｙ角度", Google translated: "Y angle".
			/// Japanese description: "平行光源：０", Google translated: "Collimated light source : 0".
			/// </remarks>
			[ParameterTableRowAttribute("degRotY_0", index: 1, minimum: -180, maximum: 180, step: 1, order: 2, unknown2: 0)]
			[DisplayName("Y angle")]
			[Description("Collimated light source : 0")]
			[DefaultValue((Int16)0)]
			public Int16 DegRotY_0 {
				get { return degRotY_0; }
				set {
					if ((double)value < -180 || (double)value > 180)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -180 to 180 for DegRotY_0.");
					SetProperty(ref degRotY_0, ref value, DegRotY_0Property);
				}
			}

			/// <summary>R</summary>
			/// <remarks>
			/// Japanese short name: "Ｒ", Google translated: "R".
			/// Japanese description: "平行光源：０", Google translated: "Collimated light source : 0".
			/// </remarks>
			[ParameterTableRowAttribute("colR_0", index: 2, minimum: 0, maximum: 255, step: 1, order: 3, unknown2: 0)]
			[DisplayName("R")]
			[Description("Collimated light source : 0")]
			[DefaultValue((Int16)255)]
			public Int16 ColR_0 {
				get { return colR_0; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColR_0.");
					SetProperty(ref colR_0, ref value, ColR_0Property);
				}
			}

			/// <summary>G</summary>
			/// <remarks>
			/// Japanese short name: "Ｇ", Google translated: "G".
			/// Japanese description: "平行光源：０", Google translated: "Collimated light source : 0".
			/// </remarks>
			[ParameterTableRowAttribute("colG_0", index: 3, minimum: 0, maximum: 255, step: 1, order: 4, unknown2: 0)]
			[DisplayName("G")]
			[Description("Collimated light source : 0")]
			[DefaultValue((Int16)255)]
			public Int16 ColG_0 {
				get { return colG_0; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColG_0.");
					SetProperty(ref colG_0, ref value, ColG_0Property);
				}
			}

			/// <summary>B</summary>
			/// <remarks>
			/// Japanese short name: "Ｂ", Google translated: "B".
			/// Japanese description: "平行光源：０", Google translated: "Collimated light source : 0".
			/// </remarks>
			[ParameterTableRowAttribute("colB_0", index: 4, minimum: 0, maximum: 255, step: 1, order: 5, unknown2: 0)]
			[DisplayName("B")]
			[Description("Collimated light source : 0")]
			[DefaultValue((Int16)255)]
			public Int16 ColB_0 {
				get { return colB_0; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColB_0.");
					SetProperty(ref colB_0, ref value, ColB_0Property);
				}
			}

			/// <summary>RGB magnification [ %]</summary>
			/// <remarks>
			/// Japanese short name: "RGB倍率[％]", Google translated: "RGB magnification [ %]".
			/// Japanese description: "平行光源：０", Google translated: "Collimated light source : 0".
			/// </remarks>
			[ParameterTableRowAttribute("colA_0", index: 5, minimum: 0, maximum: 1000, step: 1, order: 6, unknown2: 0)]
			[DisplayName("RGB magnification [ %]")]
			[Description("Collimated light source : 0")]
			[DefaultValue((Int16)100)]
			public Int16 ColA_0 {
				get { return colA_0; }
				set {
					if ((double)value < 0 || (double)value > 1000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1000 for ColA_0.");
					SetProperty(ref colA_0, ref value, ColA_0Property);
				}
			}

			/// <summary>X angle</summary>
			/// <remarks>
			/// Japanese short name: "X角度", Google translated: "X angle".
			/// Japanese description: "平行光源：１", Google translated: "Collimated light source : 1".
			/// </remarks>
			[ParameterTableRowAttribute("degRotX_1", index: 6, minimum: -90, maximum: 90, step: 1, order: 7, unknown2: 0)]
			[DisplayName("X angle")]
			[Description("Collimated light source : 1")]
			[DefaultValue((Int16)0)]
			public Int16 DegRotX_1 {
				get { return degRotX_1; }
				set {
					if ((double)value < -90 || (double)value > 90)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -90 to 90 for DegRotX_1.");
					SetProperty(ref degRotX_1, ref value, DegRotX_1Property);
				}
			}

			/// <summary>Y angle</summary>
			/// <remarks>
			/// Japanese short name: "Ｙ角度", Google translated: "Y angle".
			/// Japanese description: "平行光源：１", Google translated: "Collimated light source : 1".
			/// </remarks>
			[ParameterTableRowAttribute("degRotY_1", index: 7, minimum: -180, maximum: 180, step: 1, order: 8, unknown2: 0)]
			[DisplayName("Y angle")]
			[Description("Collimated light source : 1")]
			[DefaultValue((Int16)0)]
			public Int16 DegRotY_1 {
				get { return degRotY_1; }
				set {
					if ((double)value < -180 || (double)value > 180)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -180 to 180 for DegRotY_1.");
					SetProperty(ref degRotY_1, ref value, DegRotY_1Property);
				}
			}

			/// <summary>R</summary>
			/// <remarks>
			/// Japanese short name: "Ｒ", Google translated: "R".
			/// Japanese description: "平行光源：１", Google translated: "Collimated light source : 1".
			/// </remarks>
			[ParameterTableRowAttribute("colR_1", index: 8, minimum: 0, maximum: 255, step: 1, order: 9, unknown2: 0)]
			[DisplayName("R")]
			[Description("Collimated light source : 1")]
			[DefaultValue((Int16)255)]
			public Int16 ColR_1 {
				get { return colR_1; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColR_1.");
					SetProperty(ref colR_1, ref value, ColR_1Property);
				}
			}

			/// <summary>G</summary>
			/// <remarks>
			/// Japanese short name: "Ｇ", Google translated: "G".
			/// Japanese description: "平行光源：１", Google translated: "Collimated light source : 1".
			/// </remarks>
			[ParameterTableRowAttribute("colG_1", index: 9, minimum: 0, maximum: 255, step: 1, order: 10, unknown2: 0)]
			[DisplayName("G")]
			[Description("Collimated light source : 1")]
			[DefaultValue((Int16)255)]
			public Int16 ColG_1 {
				get { return colG_1; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColG_1.");
					SetProperty(ref colG_1, ref value, ColG_1Property);
				}
			}

			/// <summary>B</summary>
			/// <remarks>
			/// Japanese short name: "Ｂ", Google translated: "B".
			/// Japanese description: "平行光源：１", Google translated: "Collimated light source : 1".
			/// </remarks>
			[ParameterTableRowAttribute("colB_1", index: 10, minimum: 0, maximum: 255, step: 1, order: 11, unknown2: 0)]
			[DisplayName("B")]
			[Description("Collimated light source : 1")]
			[DefaultValue((Int16)255)]
			public Int16 ColB_1 {
				get { return colB_1; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColB_1.");
					SetProperty(ref colB_1, ref value, ColB_1Property);
				}
			}

			/// <summary>RGB magnification [ %]</summary>
			/// <remarks>
			/// Japanese short name: "RGB倍率[％]", Google translated: "RGB magnification [ %]".
			/// Japanese description: "平行光源：１", Google translated: "Collimated light source : 1".
			/// </remarks>
			[ParameterTableRowAttribute("colA_1", index: 11, minimum: 0, maximum: 1000, step: 1, order: 12, unknown2: 0)]
			[DisplayName("RGB magnification [ %]")]
			[Description("Collimated light source : 1")]
			[DefaultValue((Int16)100)]
			public Int16 ColA_1 {
				get { return colA_1; }
				set {
					if ((double)value < 0 || (double)value > 1000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1000 for ColA_1.");
					SetProperty(ref colA_1, ref value, ColA_1Property);
				}
			}

			/// <summary>X angle</summary>
			/// <remarks>
			/// Japanese short name: "X角度", Google translated: "X angle".
			/// Japanese description: "平行光源：２", Google translated: "Collimated light source : 2".
			/// </remarks>
			[ParameterTableRowAttribute("degRotX_2", index: 12, minimum: -90, maximum: 90, step: 1, order: 13, unknown2: 0)]
			[DisplayName("X angle")]
			[Description("Collimated light source : 2")]
			[DefaultValue((Int16)0)]
			public Int16 DegRotX_2 {
				get { return degRotX_2; }
				set {
					if ((double)value < -90 || (double)value > 90)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -90 to 90 for DegRotX_2.");
					SetProperty(ref degRotX_2, ref value, DegRotX_2Property);
				}
			}

			/// <summary>Y angle</summary>
			/// <remarks>
			/// Japanese short name: "Ｙ角度", Google translated: "Y angle".
			/// Japanese description: "平行光源：２", Google translated: "Collimated light source : 2".
			/// </remarks>
			[ParameterTableRowAttribute("degRotY_2", index: 13, minimum: -180, maximum: 180, step: 1, order: 14, unknown2: 1)]
			[DisplayName("Y angle")]
			[Description("Collimated light source : 2")]
			[DefaultValue((Int16)0)]
			public Int16 DegRotY_2 {
				get { return degRotY_2; }
				set {
					if ((double)value < -180 || (double)value > 180)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -180 to 180 for DegRotY_2.");
					SetProperty(ref degRotY_2, ref value, DegRotY_2Property);
				}
			}

			/// <summary>R</summary>
			/// <remarks>
			/// Japanese short name: "Ｒ", Google translated: "R".
			/// Japanese description: "平行光源：２", Google translated: "Collimated light source : 2".
			/// </remarks>
			[ParameterTableRowAttribute("colR_2", index: 14, minimum: 0, maximum: 255, step: 1, order: 15, unknown2: 0)]
			[DisplayName("R")]
			[Description("Collimated light source : 2")]
			[DefaultValue((Int16)255)]
			public Int16 ColR_2 {
				get { return colR_2; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColR_2.");
					SetProperty(ref colR_2, ref value, ColR_2Property);
				}
			}

			/// <summary>G</summary>
			/// <remarks>
			/// Japanese short name: "Ｇ", Google translated: "G".
			/// Japanese description: "平行光源：２", Google translated: "Collimated light source : 2".
			/// </remarks>
			[ParameterTableRowAttribute("colG_2", index: 15, minimum: 0, maximum: 255, step: 1, order: 16, unknown2: 0)]
			[DisplayName("G")]
			[Description("Collimated light source : 2")]
			[DefaultValue((Int16)255)]
			public Int16 ColG_2 {
				get { return colG_2; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColG_2.");
					SetProperty(ref colG_2, ref value, ColG_2Property);
				}
			}

			/// <summary>B</summary>
			/// <remarks>
			/// Japanese short name: "Ｂ", Google translated: "B".
			/// Japanese description: "平行光源：２", Google translated: "Collimated light source : 2".
			/// </remarks>
			[ParameterTableRowAttribute("colB_2", index: 16, minimum: 0, maximum: 255, step: 1, order: 17, unknown2: 0)]
			[DisplayName("B")]
			[Description("Collimated light source : 2")]
			[DefaultValue((Int16)255)]
			public Int16 ColB_2 {
				get { return colB_2; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColB_2.");
					SetProperty(ref colB_2, ref value, ColB_2Property);
				}
			}

			/// <summary>RGB magnification [ %]</summary>
			/// <remarks>
			/// Japanese short name: "RGB倍率[％]", Google translated: "RGB magnification [ %]".
			/// Japanese description: "平行光源：２", Google translated: "Collimated light source : 2".
			/// </remarks>
			[ParameterTableRowAttribute("colA_2", index: 17, minimum: 0, maximum: 1000, step: 1, order: 18, unknown2: 0)]
			[DisplayName("RGB magnification [ %]")]
			[Description("Collimated light source : 2")]
			[DefaultValue((Int16)100)]
			public Int16 ColA_2 {
				get { return colA_2; }
				set {
					if ((double)value < 0 || (double)value > 1000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1000 for ColA_2.");
					SetProperty(ref colA_2, ref value, ColA_2Property);
				}
			}

			/// <summary>R</summary>
			/// <remarks>
			/// Japanese short name: "Ｒ", Google translated: "R".
			/// Japanese description: "アンビエント上半球", Google translated: "Ambient on hemisphere".
			/// </remarks>
			[ParameterTableRowAttribute("colR_u", index: 18, minimum: 0, maximum: 255, step: 1, order: 19, unknown2: 0)]
			[DisplayName("R")]
			[Description("Ambient on hemisphere")]
			[DefaultValue((Int16)255)]
			public Int16 ColR_u {
				get { return colR_u; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColR_u.");
					SetProperty(ref colR_u, ref value, ColR_uProperty);
				}
			}

			/// <summary>G</summary>
			/// <remarks>
			/// Japanese short name: "Ｇ", Google translated: "G".
			/// Japanese description: "アンビエント上半球", Google translated: "Ambient on hemisphere".
			/// </remarks>
			[ParameterTableRowAttribute("colG_u", index: 19, minimum: 0, maximum: 255, step: 1, order: 20, unknown2: 0)]
			[DisplayName("G")]
			[Description("Ambient on hemisphere")]
			[DefaultValue((Int16)255)]
			public Int16 ColG_u {
				get { return colG_u; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColG_u.");
					SetProperty(ref colG_u, ref value, ColG_uProperty);
				}
			}

			/// <summary>B</summary>
			/// <remarks>
			/// Japanese short name: "Ｂ", Google translated: "B".
			/// Japanese description: "アンビエント上半球", Google translated: "Ambient on hemisphere".
			/// </remarks>
			[ParameterTableRowAttribute("colB_u", index: 20, minimum: 0, maximum: 255, step: 1, order: 21, unknown2: 0)]
			[DisplayName("B")]
			[Description("Ambient on hemisphere")]
			[DefaultValue((Int16)255)]
			public Int16 ColB_u {
				get { return colB_u; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColB_u.");
					SetProperty(ref colB_u, ref value, ColB_uProperty);
				}
			}

			/// <summary>RGB magnification [ %]</summary>
			/// <remarks>
			/// Japanese short name: "RGB倍率[％]", Google translated: "RGB magnification [ %]".
			/// Japanese description: "アンビエント上半球", Google translated: "Ambient on hemisphere".
			/// </remarks>
			[ParameterTableRowAttribute("colA_u", index: 21, minimum: 0, maximum: 1000, step: 1, order: 22, unknown2: 0)]
			[DisplayName("RGB magnification [ %]")]
			[Description("Ambient on hemisphere")]
			[DefaultValue((Int16)100)]
			public Int16 ColA_u {
				get { return colA_u; }
				set {
					if ((double)value < 0 || (double)value > 1000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1000 for ColA_u.");
					SetProperty(ref colA_u, ref value, ColA_uProperty);
				}
			}

			/// <summary>R</summary>
			/// <remarks>
			/// Japanese short name: "Ｒ", Google translated: "R".
			/// Japanese description: "アンビエント下半球", Google translated: "Under ambient hemisphere".
			/// </remarks>
			[ParameterTableRowAttribute("colR_d", index: 22, minimum: 0, maximum: 255, step: 1, order: 23, unknown2: 0)]
			[DisplayName("R")]
			[Description("Under ambient hemisphere")]
			[DefaultValue((Int16)255)]
			public Int16 ColR_d {
				get { return colR_d; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColR_d.");
					SetProperty(ref colR_d, ref value, ColR_dProperty);
				}
			}

			/// <summary>G</summary>
			/// <remarks>
			/// Japanese short name: "Ｇ", Google translated: "G".
			/// Japanese description: "アンビエント下半球", Google translated: "Under ambient hemisphere".
			/// </remarks>
			[ParameterTableRowAttribute("colG_d", index: 23, minimum: 0, maximum: 255, step: 1, order: 24, unknown2: 0)]
			[DisplayName("G")]
			[Description("Under ambient hemisphere")]
			[DefaultValue((Int16)255)]
			public Int16 ColG_d {
				get { return colG_d; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColG_d.");
					SetProperty(ref colG_d, ref value, ColG_dProperty);
				}
			}

			/// <summary>B</summary>
			/// <remarks>
			/// Japanese short name: "Ｂ", Google translated: "B".
			/// Japanese description: "アンビエント下半球", Google translated: "Under ambient hemisphere".
			/// </remarks>
			[ParameterTableRowAttribute("colB_d", index: 24, minimum: 0, maximum: 255, step: 1, order: 25, unknown2: 0)]
			[DisplayName("B")]
			[Description("Under ambient hemisphere")]
			[DefaultValue((Int16)255)]
			public Int16 ColB_d {
				get { return colB_d; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColB_d.");
					SetProperty(ref colB_d, ref value, ColB_dProperty);
				}
			}

			/// <summary>RGB magnification [ %]</summary>
			/// <remarks>
			/// Japanese short name: "RGB倍率[％]", Google translated: "RGB magnification [ %]".
			/// Japanese description: "アンビエント下半球", Google translated: "Under ambient hemisphere".
			/// </remarks>
			[ParameterTableRowAttribute("colA_d", index: 25, minimum: 0, maximum: 1000, step: 1, order: 26, unknown2: 0)]
			[DisplayName("RGB magnification [ %]")]
			[Description("Under ambient hemisphere")]
			[DefaultValue((Int16)100)]
			public Int16 ColA_d {
				get { return colA_d; }
				set {
					if ((double)value < 0 || (double)value > 1000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1000 for ColA_d.");
					SetProperty(ref colA_d, ref value, ColA_dProperty);
				}
			}

			/// <summary>X angle</summary>
			/// <remarks>
			/// Japanese short name: "X角度", Google translated: "X angle".
			/// Japanese description: "平行光源：スペキュラ", Google translated: "Collimated light source : specular".
			/// </remarks>
			[ParameterTableRowAttribute("degRotX_s", index: 26, minimum: -90, maximum: 90, step: 1, order: 27, unknown2: 0)]
			[DisplayName("X angle")]
			[Description("Collimated light source : specular")]
			[DefaultValue((Int16)0)]
			public Int16 DegRotX_s {
				get { return degRotX_s; }
				set {
					if ((double)value < -90 || (double)value > 90)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -90 to 90 for DegRotX_s.");
					SetProperty(ref degRotX_s, ref value, DegRotX_sProperty);
				}
			}

			/// <summary>Y angle</summary>
			/// <remarks>
			/// Japanese short name: "Ｙ角度", Google translated: "Y angle".
			/// Japanese description: "平行光源：スペキュラ", Google translated: "Collimated light source : specular".
			/// </remarks>
			[ParameterTableRowAttribute("degRotY_s", index: 27, minimum: -180, maximum: 180, step: 1, order: 28, unknown2: 1)]
			[DisplayName("Y angle")]
			[Description("Collimated light source : specular")]
			[DefaultValue((Int16)0)]
			public Int16 DegRotY_s {
				get { return degRotY_s; }
				set {
					if ((double)value < -180 || (double)value > 180)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -180 to 180 for DegRotY_s.");
					SetProperty(ref degRotY_s, ref value, DegRotY_sProperty);
				}
			}

			/// <summary>R</summary>
			/// <remarks>
			/// Japanese short name: "Ｒ", Google translated: "R".
			/// Japanese description: "平行光源：スペキュラ", Google translated: "Collimated light source : specular".
			/// </remarks>
			[ParameterTableRowAttribute("colR_s", index: 28, minimum: 0, maximum: 255, step: 1, order: 29, unknown2: 0)]
			[DisplayName("R")]
			[Description("Collimated light source : specular")]
			[DefaultValue((Int16)255)]
			public Int16 ColR_s {
				get { return colR_s; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColR_s.");
					SetProperty(ref colR_s, ref value, ColR_sProperty);
				}
			}

			/// <summary>G</summary>
			/// <remarks>
			/// Japanese short name: "Ｇ", Google translated: "G".
			/// Japanese description: "平行光源：スペキュラ", Google translated: "Collimated light source : specular".
			/// </remarks>
			[ParameterTableRowAttribute("colG_s", index: 29, minimum: 0, maximum: 255, step: 1, order: 30, unknown2: 0)]
			[DisplayName("G")]
			[Description("Collimated light source : specular")]
			[DefaultValue((Int16)255)]
			public Int16 ColG_s {
				get { return colG_s; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColG_s.");
					SetProperty(ref colG_s, ref value, ColG_sProperty);
				}
			}

			/// <summary>B</summary>
			/// <remarks>
			/// Japanese short name: "Ｂ", Google translated: "B".
			/// Japanese description: "平行光源：スペキュラ", Google translated: "Collimated light source : specular".
			/// </remarks>
			[ParameterTableRowAttribute("colB_s", index: 30, minimum: 0, maximum: 255, step: 1, order: 31, unknown2: 0)]
			[DisplayName("B")]
			[Description("Collimated light source : specular")]
			[DefaultValue((Int16)255)]
			public Int16 ColB_s {
				get { return colB_s; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColB_s.");
					SetProperty(ref colB_s, ref value, ColB_sProperty);
				}
			}

			/// <summary>RGB magnification [ %]</summary>
			/// <remarks>
			/// Japanese short name: "RGB倍率[％]", Google translated: "RGB magnification [ %]".
			/// Japanese description: "平行光源：スペキュラ", Google translated: "Collimated light source : specular".
			/// </remarks>
			[ParameterTableRowAttribute("colA_s", index: 31, minimum: 0, maximum: 1000, step: 1, order: 32, unknown2: 0)]
			[DisplayName("RGB magnification [ %]")]
			[Description("Collimated light source : specular")]
			[DefaultValue((Int16)0)]
			public Int16 ColA_s {
				get { return colA_s; }
				set {
					if ((double)value < 0 || (double)value > 1000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1000 for ColA_s.");
					SetProperty(ref colA_s, ref value, ColA_sProperty);
				}
			}

			/// <summary>R</summary>
			/// <remarks>
			/// Japanese short name: "Ｒ", Google translated: "R".
			/// Japanese description: "環境光源：ディフューズ乗算色", Google translated: "Ambient light source : diffuse multiply color".
			/// </remarks>
			[ParameterTableRowAttribute("envDif_colR", index: 32, minimum: 0, maximum: 255, step: 1, order: 33, unknown2: 0)]
			[DisplayName("R")]
			[Description("Ambient light source : diffuse multiply color")]
			[DefaultValue((Int16)255)]
			public Int16 EnvDif_colR {
				get { return envDif_colR; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for EnvDif_colR.");
					SetProperty(ref envDif_colR, ref value, EnvDif_colRProperty);
				}
			}

			/// <summary>G</summary>
			/// <remarks>
			/// Japanese short name: "Ｇ", Google translated: "G".
			/// Japanese description: "環境光源：ディフューズ乗算色", Google translated: "Ambient light source : diffuse multiply color".
			/// </remarks>
			[ParameterTableRowAttribute("envDif_colG", index: 33, minimum: 0, maximum: 255, step: 1, order: 34, unknown2: 0)]
			[DisplayName("G")]
			[Description("Ambient light source : diffuse multiply color")]
			[DefaultValue((Int16)255)]
			public Int16 EnvDif_colG {
				get { return envDif_colG; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for EnvDif_colG.");
					SetProperty(ref envDif_colG, ref value, EnvDif_colGProperty);
				}
			}

			/// <summary>B</summary>
			/// <remarks>
			/// Japanese short name: "Ｂ", Google translated: "B".
			/// Japanese description: "環境光源：ディフューズ乗算色", Google translated: "Ambient light source : diffuse multiply color".
			/// </remarks>
			[ParameterTableRowAttribute("envDif_colB", index: 34, minimum: 0, maximum: 255, step: 1, order: 35, unknown2: 0)]
			[DisplayName("B")]
			[Description("Ambient light source : diffuse multiply color")]
			[DefaultValue((Int16)255)]
			public Int16 EnvDif_colB {
				get { return envDif_colB; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for EnvDif_colB.");
					SetProperty(ref envDif_colB, ref value, EnvDif_colBProperty);
				}
			}

			/// <summary>RGB magnification [ %]</summary>
			/// <remarks>
			/// Japanese short name: "RGB倍率[％]", Google translated: "RGB magnification [ %]".
			/// Japanese description: "環境光源：ディフューズ乗算色", Google translated: "Ambient light source : diffuse multiply color".
			/// </remarks>
			[ParameterTableRowAttribute("envDif_colA", index: 35, minimum: 0, maximum: 1000, step: 1, order: 36, unknown2: 0)]
			[DisplayName("RGB magnification [ %]")]
			[Description("Ambient light source : diffuse multiply color")]
			[DefaultValue((Int16)100)]
			public Int16 EnvDif_colA {
				get { return envDif_colA; }
				set {
					if ((double)value < 0 || (double)value > 1000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1000 for EnvDif_colA.");
					SetProperty(ref envDif_colA, ref value, EnvDif_colAProperty);
				}
			}

			/// <summary>R</summary>
			/// <remarks>
			/// Japanese short name: "Ｒ", Google translated: "R".
			/// Japanese description: "環境光源：スペキュラ乗算色", Google translated: "Ambient light source : specular multiply color".
			/// </remarks>
			[ParameterTableRowAttribute("envSpc_colR", index: 36, minimum: 0, maximum: 255, step: 1, order: 37, unknown2: 0)]
			[DisplayName("R")]
			[Description("Ambient light source : specular multiply color")]
			[DefaultValue((Int16)255)]
			public Int16 EnvSpc_colR {
				get { return envSpc_colR; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for EnvSpc_colR.");
					SetProperty(ref envSpc_colR, ref value, EnvSpc_colRProperty);
				}
			}

			/// <summary>G</summary>
			/// <remarks>
			/// Japanese short name: "Ｇ", Google translated: "G".
			/// Japanese description: "環境光源：スペキュラ乗算色", Google translated: "Ambient light source : specular multiply color".
			/// </remarks>
			[ParameterTableRowAttribute("envSpc_colG", index: 37, minimum: 0, maximum: 255, step: 1, order: 38, unknown2: 0)]
			[DisplayName("G")]
			[Description("Ambient light source : specular multiply color")]
			[DefaultValue((Int16)255)]
			public Int16 EnvSpc_colG {
				get { return envSpc_colG; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for EnvSpc_colG.");
					SetProperty(ref envSpc_colG, ref value, EnvSpc_colGProperty);
				}
			}

			/// <summary>B</summary>
			/// <remarks>
			/// Japanese short name: "Ｂ", Google translated: "B".
			/// Japanese description: "環境光源：スペキュラ乗算色", Google translated: "Ambient light source : specular multiply color".
			/// </remarks>
			[ParameterTableRowAttribute("envSpc_colB", index: 38, minimum: 0, maximum: 255, step: 1, order: 39, unknown2: 0)]
			[DisplayName("B")]
			[Description("Ambient light source : specular multiply color")]
			[DefaultValue((Int16)255)]
			public Int16 EnvSpc_colB {
				get { return envSpc_colB; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for EnvSpc_colB.");
					SetProperty(ref envSpc_colB, ref value, EnvSpc_colBProperty);
				}
			}

			/// <summary>RGB magnification [ %]</summary>
			/// <remarks>
			/// Japanese short name: "RGB倍率[％]", Google translated: "RGB magnification [ %]".
			/// Japanese description: "環境光源：スペキュラ乗算色", Google translated: "Ambient light source : specular multiply color".
			/// </remarks>
			[ParameterTableRowAttribute("envSpc_colA", index: 39, minimum: 0, maximum: 1000, step: 1, order: 40, unknown2: 0)]
			[DisplayName("RGB magnification [ %]")]
			[Description("Ambient light source : specular multiply color")]
			[DefaultValue((Int16)100)]
			public Int16 EnvSpc_colA {
				get { return envSpc_colA; }
				set {
					if ((double)value < 0 || (double)value > 1000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1000 for EnvSpc_colA.");
					SetProperty(ref envSpc_colA, ref value, EnvSpc_colAProperty);
				}
			}

			/// <summary>Environment diffuse</summary>
			/// <remarks>
			/// Japanese short name: "環境ディフューズ", Google translated: "Environment diffuse".
			/// Japanese description: "環境光源：ディフューズテクスチャID", Google translated: "Ambient light source : the diffuse texture ID".
			/// </remarks>
			[ParameterTableRowAttribute("envDif", index: 40, minimum: 0, maximum: 999, step: 1, order: 41, unknown2: 0)]
			[DisplayName("Environment diffuse")]
			[Description("Ambient light source : the diffuse texture ID")]
			[DefaultValue((Int16)0)]
			public Int16 EnvDif {
				get { return envDif; }
				set {
					if ((double)value < 0 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for EnvDif.");
					SetProperty(ref envDif, ref value, EnvDifProperty);
				}
			}

			/// <summary>Environment specular 0</summary>
			/// <remarks>
			/// Japanese short name: "環境スペキュラ０", Google translated: "Environment specular 0".
			/// Japanese description: "環境光源：スペキュラ０テクスチャID", Google translated: "Ambient light source : 0 specular texture ID".
			/// </remarks>
			[ParameterTableRowAttribute("envSpc_0", index: 41, minimum: 0, maximum: 999, step: 1, order: 42, unknown2: 0)]
			[DisplayName("Environment specular 0")]
			[Description("Ambient light source : 0 specular texture ID")]
			[DefaultValue((Int16)0)]
			public Int16 EnvSpc_0 {
				get { return envSpc_0; }
				set {
					if ((double)value < 0 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for EnvSpc_0.");
					SetProperty(ref envSpc_0, ref value, EnvSpc_0Property);
				}
			}

			/// <summary>Environment specular 1</summary>
			/// <remarks>
			/// Japanese short name: "環境スペキュラ１", Google translated: "Environment specular 1".
			/// Japanese description: "環境光源：スペキュラ１テクスチャID", Google translated: "Ambient light source : 1 specular texture ID".
			/// </remarks>
			[ParameterTableRowAttribute("envSpc_1", index: 42, minimum: 0, maximum: 999, step: 1, order: 43, unknown2: 0)]
			[DisplayName("Environment specular 1")]
			[Description("Ambient light source : 1 specular texture ID")]
			[DefaultValue((Int16)0)]
			public Int16 EnvSpc_1 {
				get { return envSpc_1; }
				set {
					if ((double)value < 0 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for EnvSpc_1.");
					SetProperty(ref envSpc_1, ref value, EnvSpc_1Property);
				}
			}

			/// <summary>Environment specular 2</summary>
			/// <remarks>
			/// Japanese short name: "環境スペキュラ２", Google translated: "Environment specular 2".
			/// Japanese description: "環境光源：スペキュラ２テクスチャID", Google translated: "Ambient light source : 2 specular texture ID".
			/// </remarks>
			[ParameterTableRowAttribute("envSpc_2", index: 43, minimum: 0, maximum: 999, step: 1, order: 44, unknown2: 0)]
			[DisplayName("Environment specular 2")]
			[Description("Ambient light source : 2 specular texture ID")]
			[DefaultValue((Int16)0)]
			public Int16 EnvSpc_2 {
				get { return envSpc_2; }
				set {
					if ((double)value < 0 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for EnvSpc_2.");
					SetProperty(ref envSpc_2, ref value, EnvSpc_2Property);
				}
			}

			/// <summary>Specular environment 3</summary>
			/// <remarks>
			/// Japanese short name: "環境スペキュラ３", Google translated: "Specular environment 3".
			/// Japanese description: "環境光源：スペキュラ３テクスチャID", Google translated: "Ambient light source : 3 specular texture ID".
			/// </remarks>
			[ParameterTableRowAttribute("envSpc_3", index: 44, minimum: 0, maximum: 999, step: 1, order: 45, unknown2: 0)]
			[DisplayName("Specular environment 3")]
			[Description("Ambient light source : 3 specular texture ID")]
			[DefaultValue((Int16)0)]
			public Int16 EnvSpc_3 {
				get { return envSpc_3; }
				set {
					if ((double)value < 0 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for EnvSpc_3.");
					SetProperty(ref envSpc_3, ref value, EnvSpc_3Property);
				}
			}

			/// <summary>Padding</summary>
			/// <remarks>
			/// Japanese short name: "パディング", Google translated: "Padding".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("pad[2]", index: 45, minimum: 0, maximum: 999, step: 1, order: 47, unknown2: 0)]
			[DisplayName("Padding")]
			[Description("")]
			public Byte[] Pad {
				get { return pad; }
				set { SetProperty(ref pad, ref value, PadProperty); }
			}

			internal Light(ParameterTable table, int index, AssetLoader loader, int next)
				: base(table, index, loader) {
				BinaryReader reader = loader.Reader;

				DegRotX_0 = reader.ReadInt16();
				DegRotY_0 = reader.ReadInt16();
				ColR_0 = reader.ReadInt16();
				ColG_0 = reader.ReadInt16();
				ColB_0 = reader.ReadInt16();
				ColA_0 = reader.ReadInt16();
				DegRotX_1 = reader.ReadInt16();
				DegRotY_1 = reader.ReadInt16();
				ColR_1 = reader.ReadInt16();
				ColG_1 = reader.ReadInt16();
				ColB_1 = reader.ReadInt16();
				ColA_1 = reader.ReadInt16();
				DegRotX_2 = reader.ReadInt16();
				DegRotY_2 = reader.ReadInt16();
				ColR_2 = reader.ReadInt16();
				ColG_2 = reader.ReadInt16();
				ColB_2 = reader.ReadInt16();
				ColA_2 = reader.ReadInt16();
				ColR_u = reader.ReadInt16();
				ColG_u = reader.ReadInt16();
				ColB_u = reader.ReadInt16();
				ColA_u = reader.ReadInt16();
				ColR_d = reader.ReadInt16();
				ColG_d = reader.ReadInt16();
				ColB_d = reader.ReadInt16();
				ColA_d = reader.ReadInt16();
				DegRotX_s = reader.ReadInt16();
				DegRotY_s = reader.ReadInt16();
				ColR_s = reader.ReadInt16();
				ColG_s = reader.ReadInt16();
				ColB_s = reader.ReadInt16();
				ColA_s = reader.ReadInt16();
				EnvDif_colR = reader.ReadInt16();
				EnvDif_colG = reader.ReadInt16();
				EnvDif_colB = reader.ReadInt16();
				EnvDif_colA = reader.ReadInt16();
				EnvSpc_colR = reader.ReadInt16();
				EnvSpc_colG = reader.ReadInt16();
				EnvSpc_colB = reader.ReadInt16();
				EnvSpc_colA = reader.ReadInt16();
				EnvDif = reader.ReadInt16();
				EnvSpc_0 = reader.ReadInt16();
				EnvSpc_1 = reader.ReadInt16();
				EnvSpc_2 = reader.ReadInt16();
				EnvSpc_3 = reader.ReadInt16();
				Pad = reader.ReadBytes(2);
			}

			internal Light(ParameterTable table, int index)
				: base(table, index) {
				DegRotX_0 = (Int16)0;
				DegRotY_0 = (Int16)0;
				ColR_0 = (Int16)255;
				ColG_0 = (Int16)255;
				ColB_0 = (Int16)255;
				ColA_0 = (Int16)100;
				DegRotX_1 = (Int16)0;
				DegRotY_1 = (Int16)0;
				ColR_1 = (Int16)255;
				ColG_1 = (Int16)255;
				ColB_1 = (Int16)255;
				ColA_1 = (Int16)100;
				DegRotX_2 = (Int16)0;
				DegRotY_2 = (Int16)0;
				ColR_2 = (Int16)255;
				ColG_2 = (Int16)255;
				ColB_2 = (Int16)255;
				ColA_2 = (Int16)100;
				ColR_u = (Int16)255;
				ColG_u = (Int16)255;
				ColB_u = (Int16)255;
				ColA_u = (Int16)100;
				ColR_d = (Int16)255;
				ColG_d = (Int16)255;
				ColB_d = (Int16)255;
				ColA_d = (Int16)100;
				DegRotX_s = (Int16)0;
				DegRotY_s = (Int16)0;
				ColR_s = (Int16)255;
				ColG_s = (Int16)255;
				ColB_s = (Int16)255;
				ColA_s = (Int16)0;
				EnvDif_colR = (Int16)255;
				EnvDif_colG = (Int16)255;
				EnvDif_colB = (Int16)255;
				EnvDif_colA = (Int16)100;
				EnvSpc_colR = (Int16)255;
				EnvSpc_colG = (Int16)255;
				EnvSpc_colB = (Int16)255;
				EnvSpc_colA = (Int16)100;
				EnvDif = (Int16)0;
				EnvSpc_0 = (Int16)0;
				EnvSpc_1 = (Int16)0;
				EnvSpc_2 = (Int16)0;
				EnvSpc_3 = (Int16)0;
				Pad = new Byte[2];
			}

			public override void Write(BinaryWriter writer) {
				writer.Write(DegRotX_0);
				writer.Write(DegRotY_0);
				writer.Write(ColR_0);
				writer.Write(ColG_0);
				writer.Write(ColB_0);
				writer.Write(ColA_0);
				writer.Write(DegRotX_1);
				writer.Write(DegRotY_1);
				writer.Write(ColR_1);
				writer.Write(ColG_1);
				writer.Write(ColB_1);
				writer.Write(ColA_1);
				writer.Write(DegRotX_2);
				writer.Write(DegRotY_2);
				writer.Write(ColR_2);
				writer.Write(ColG_2);
				writer.Write(ColB_2);
				writer.Write(ColA_2);
				writer.Write(ColR_u);
				writer.Write(ColG_u);
				writer.Write(ColB_u);
				writer.Write(ColA_u);
				writer.Write(ColR_d);
				writer.Write(ColG_d);
				writer.Write(ColB_d);
				writer.Write(ColA_d);
				writer.Write(DegRotX_s);
				writer.Write(DegRotY_s);
				writer.Write(ColR_s);
				writer.Write(ColG_s);
				writer.Write(ColB_s);
				writer.Write(ColA_s);
				writer.Write(EnvDif_colR);
				writer.Write(EnvDif_colG);
				writer.Write(EnvDif_colB);
				writer.Write(EnvDif_colA);
				writer.Write(EnvSpc_colR);
				writer.Write(EnvSpc_colG);
				writer.Write(EnvSpc_colB);
				writer.Write(EnvSpc_colA);
				writer.Write(EnvDif);
				writer.Write(EnvSpc_0);
				writer.Write(EnvSpc_1);
				writer.Write(EnvSpc_2);
				writer.Write(EnvSpc_3);
				writer.Write(Pad);
			}
		}
	}
}
