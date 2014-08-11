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
		/// Defined as "LIGHT_SCATTERING_BANK" in the file "LightScatteringBank.paramdef" (id 02h).
		/// </remarks>
		public class LightScattering : ParameterTableRow {
			/// <summary>The name of the table in the file.</summary>
			public const string TableName = "LIGHT_SCATTERING_BANK";

			Int16 sunRotX, sunRotY, distanceMul, sunR, sunG, sunB, sunA, blendCoef, reflectanceR, reflectanceG, reflectanceB, reflectanceA;
			Byte[] pad_0, pad_1;
			Single lsHGg, lsBetaRay, lsBetaMie;

			/// <summary>A property of the class.</summary>
			public static readonly PropertyInfo
				SunRotXProperty = GetProperty<LightScattering>("SunRotX"),
				SunRotYProperty = GetProperty<LightScattering>("SunRotY"),
				DistanceMulProperty = GetProperty<LightScattering>("DistanceMul"),
				SunRProperty = GetProperty<LightScattering>("SunR"),
				SunGProperty = GetProperty<LightScattering>("SunG"),
				SunBProperty = GetProperty<LightScattering>("SunB"),
				SunAProperty = GetProperty<LightScattering>("SunA"),
				Pad_0Property = GetProperty<LightScattering>("Pad_0"),
				LsHGgProperty = GetProperty<LightScattering>("LsHGg"),
				LsBetaRayProperty = GetProperty<LightScattering>("LsBetaRay"),
				LsBetaMieProperty = GetProperty<LightScattering>("LsBetaMie"),
				BlendCoefProperty = GetProperty<LightScattering>("BlendCoef"),
				ReflectanceRProperty = GetProperty<LightScattering>("ReflectanceR"),
				ReflectanceGProperty = GetProperty<LightScattering>("ReflectanceG"),
				ReflectanceBProperty = GetProperty<LightScattering>("ReflectanceB"),
				ReflectanceAProperty = GetProperty<LightScattering>("ReflectanceA"),
				Pad_1Property = GetProperty<LightScattering>("Pad_1");

			/// <summary>X angle</summary>
			/// <remarks>
			/// Japanese short name: "X角度", Google translated: "X angle".
			/// Japanese description: "光源", Google translated: "Light source".
			/// </remarks>
			[ParameterTableRowAttribute("sunRotX", index: 0, minimum: -90, maximum: 90, step: 1, sortOrder: 1, unknown2: 0)]
			[DisplayName("X angle")]
			[Description("Light source")]
			[DefaultValue((Int16)0)]
			public Int16 SunRotX {
				get { return sunRotX; }
				set {
					if ((double)value < -90 || (double)value > 90)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -90 to 90 for SunRotX.");
					SetProperty(ref sunRotX, ref value, SunRotXProperty);
				}
			}

			/// <summary>Y angle</summary>
			/// <remarks>
			/// Japanese short name: "Ｙ角度", Google translated: "Y angle".
			/// Japanese description: "光源", Google translated: "Light source".
			/// </remarks>
			[ParameterTableRowAttribute("sunRotY", index: 1, minimum: -180, maximum: 180, step: 1, sortOrder: 2, unknown2: 1)]
			[DisplayName("Y angle")]
			[Description("Light source")]
			[DefaultValue((Int16)0)]
			public Int16 SunRotY {
				get { return sunRotY; }
				set {
					if ((double)value < -180 || (double)value > 180)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -180 to 180 for SunRotY.");
					SetProperty(ref sunRotY, ref value, SunRotYProperty);
				}
			}

			/// <summary>Distance magnification [%]</summary>
			/// <remarks>
			/// Japanese short name: "距離倍率[％]", Google translated: "Distance magnification [%]".
			/// Japanese description: "距離倍率[%](0～1000)", Google translated: "Distance magnification [%] (0-1000)".
			/// </remarks>
			[ParameterTableRowAttribute("distanceMul", index: 2, minimum: 0, maximum: 1000, step: 1, sortOrder: 3, unknown2: 0)]
			[DisplayName("Distance magnification [%]")]
			[Description("Distance magnification [%] (0-1000)")]
			[DefaultValue((Int16)100)]
			public Int16 DistanceMul {
				get { return distanceMul; }
				set {
					if ((double)value < 0 || (double)value > 1000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1000 for DistanceMul.");
					SetProperty(ref distanceMul, ref value, DistanceMulProperty);
				}
			}

			/// <summary>R</summary>
			/// <remarks>
			/// Japanese short name: "Ｒ", Google translated: "R".
			/// Japanese description: "光源", Google translated: "Light source".
			/// </remarks>
			[ParameterTableRowAttribute("sunR", index: 3, minimum: 0, maximum: 255, step: 1, sortOrder: 4, unknown2: 0)]
			[DisplayName("R")]
			[Description("Light source")]
			[DefaultValue((Int16)255)]
			public Int16 SunR {
				get { return sunR; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for SunR.");
					SetProperty(ref sunR, ref value, SunRProperty);
				}
			}

			/// <summary>G</summary>
			/// <remarks>
			/// Japanese short name: "Ｇ", Google translated: "G".
			/// Japanese description: "光源", Google translated: "Light source".
			/// </remarks>
			[ParameterTableRowAttribute("sunG", index: 4, minimum: 0, maximum: 255, step: 1, sortOrder: 5, unknown2: 0)]
			[DisplayName("G")]
			[Description("Light source")]
			[DefaultValue((Int16)255)]
			public Int16 SunG {
				get { return sunG; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for SunG.");
					SetProperty(ref sunG, ref value, SunGProperty);
				}
			}

			/// <summary>B</summary>
			/// <remarks>
			/// Japanese short name: "Ｂ", Google translated: "B".
			/// Japanese description: "光源", Google translated: "Light source".
			/// </remarks>
			[ParameterTableRowAttribute("sunB", index: 5, minimum: 0, maximum: 255, step: 1, sortOrder: 6, unknown2: 0)]
			[DisplayName("B")]
			[Description("Light source")]
			[DefaultValue((Int16)255)]
			public Int16 SunB {
				get { return sunB; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for SunB.");
					SetProperty(ref sunB, ref value, SunBProperty);
				}
			}

			/// <summary>RGB magnification [%]</summary>
			/// <remarks>
			/// Japanese short name: "RGB倍率[％]", Google translated: "RGB magnification [%]".
			/// Japanese description: "光源", Google translated: "Light source".
			/// </remarks>
			[ParameterTableRowAttribute("sunA", index: 6, minimum: 0, maximum: 1000, step: 1, sortOrder: 7, unknown2: 0)]
			[DisplayName("RGB magnification [%]")]
			[Description("Light source")]
			[DefaultValue((Int16)600)]
			public Int16 SunA {
				get { return sunA; }
				set {
					if ((double)value < 0 || (double)value > 1000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1000 for SunA.");
					SetProperty(ref sunA, ref value, SunAProperty);
				}
			}

			/// <summary>Padding</summary>
			/// <remarks>
			/// Japanese short name: "パディング", Google translated: "Padding".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("pad_0[2]", index: 7, minimum: 0, maximum: 999, step: 1, sortOrder: 18, unknown2: 0)]
			[DisplayName("Padding")]
			[Description("")]
			[Browsable(false)]
			public Byte[] Pad_0 {
				get { return pad_0; }
				set { SetProperty(ref pad_0, ref value, Pad_0Property); }
			}

			/// <summary>Scattering coefficient direction</summary>
			/// <remarks>
			/// Japanese short name: "散乱方向係数", Google translated: "Scattering coefficient direction".
			/// Japanese description: "散乱方向係数（-1:後方 1:前方）", Google translated: "(Forward: 1 -1 backward) direction scattering coefficient".
			/// </remarks>
			[ParameterTableRowAttribute("lsHGg", index: 8, minimum: -0.99, maximum: 0.99, step: 0.01, sortOrder: 8, unknown2: 0)]
			[DisplayName("Scattering coefficient direction")]
			[Description("(Forward: 1 -1 backward) direction scattering coefficient")]
			[DefaultValue((Single)0.8)]
			public Single LsHGg {
				get { return lsHGg; }
				set {
					if ((double)value < -0.99 || (double)value > 0.99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -0.99 to 0.99 for LsHGg.");
					SetProperty(ref lsHGg, ref value, LsHGgProperty);
				}
			}

			/// <summary>Rayleigh scattering coefficient</summary>
			/// <remarks>
			/// Japanese short name: "レイリー散乱係数", Google translated: "Rayleigh scattering coefficient".
			/// Japanese description: "レイリー散乱係数（空気の分子）", Google translated: "(Air molecules) Rayleigh scattering coefficient".
			/// </remarks>
			[ParameterTableRowAttribute("lsBetaRay", index: 9, minimum: 0, maximum: 10, step: 0.1, sortOrder: 9, unknown2: 0)]
			[DisplayName("Rayleigh scattering coefficient")]
			[Description("(Air molecules) Rayleigh scattering coefficient")]
			[DefaultValue((Single)0.2)]
			public Single LsBetaRay {
				get { return lsBetaRay; }
				set {
					if ((double)value < 0 || (double)value > 10)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 10 for LsBetaRay.");
					SetProperty(ref lsBetaRay, ref value, LsBetaRayProperty);
				}
			}

			/// <summary>Mie scattering coefficient</summary>
			/// <remarks>
			/// Japanese short name: "ミー散乱係数", Google translated: "Mie scattering coefficient".
			/// Japanese description: "ミー散乱係数（微粒子）", Google translated: "Mie scattering coefficient (fine particles)".
			/// </remarks>
			[ParameterTableRowAttribute("lsBetaMie", index: 10, minimum: 0, maximum: 1, step: 0.01, sortOrder: 10, unknown2: 0)]
			[DisplayName("Mie scattering coefficient")]
			[Description("Mie scattering coefficient (fine particles)")]
			[DefaultValue((Single)0.01)]
			public Single LsBetaMie {
				get { return lsBetaMie; }
				set {
					if ((double)value < 0 || (double)value > 1)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1 for LsBetaMie.");
					SetProperty(ref lsBetaMie, ref value, LsBetaMieProperty);
				}
			}

			/// <summary>Blending coefficient [%]</summary>
			/// <remarks>
			/// Japanese short name: "ブレンド係数[％]", Google translated: "Blending coefficient [%]".
			/// Japanese description: "スキャッタリングの係具合(100で最大)", Google translated: "(Up to 100) engagement condition of the scattering".
			/// </remarks>
			[ParameterTableRowAttribute("blendCoef", index: 11, minimum: 0, maximum: 100, step: 1, sortOrder: 11, unknown2: 0)]
			[DisplayName("Blending coefficient [%]")]
			[Description("(Up to 100) engagement condition of the scattering")]
			[DefaultValue((Int16)100)]
			public Int16 BlendCoef {
				get { return blendCoef; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for BlendCoef.");
					SetProperty(ref blendCoef, ref value, BlendCoefProperty);
				}
			}

			/// <summary>R</summary>
			/// <remarks>
			/// Japanese short name: "Ｒ", Google translated: "R".
			/// Japanese description: "地上の乱反射光色", Google translated: "Diffuse reflection light color on the ground".
			/// </remarks>
			[ParameterTableRowAttribute("reflectanceR", index: 12, minimum: 0, maximum: 255, step: 1, sortOrder: 12, unknown2: 0)]
			[DisplayName("R")]
			[Description("Diffuse reflection light color on the ground")]
			[DefaultValue((Int16)255)]
			public Int16 ReflectanceR {
				get { return reflectanceR; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ReflectanceR.");
					SetProperty(ref reflectanceR, ref value, ReflectanceRProperty);
				}
			}

			/// <summary>G</summary>
			/// <remarks>
			/// Japanese short name: "Ｇ", Google translated: "G".
			/// Japanese description: "地上の乱反射光色", Google translated: "Diffuse reflection light color on the ground".
			/// </remarks>
			[ParameterTableRowAttribute("reflectanceG", index: 13, minimum: 0, maximum: 255, step: 1, sortOrder: 13, unknown2: 0)]
			[DisplayName("G")]
			[Description("Diffuse reflection light color on the ground")]
			[DefaultValue((Int16)255)]
			public Int16 ReflectanceG {
				get { return reflectanceG; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ReflectanceG.");
					SetProperty(ref reflectanceG, ref value, ReflectanceGProperty);
				}
			}

			/// <summary>B</summary>
			/// <remarks>
			/// Japanese short name: "Ｂ", Google translated: "B".
			/// Japanese description: "地上の乱反射光色", Google translated: "Diffuse reflection light color on the ground".
			/// </remarks>
			[ParameterTableRowAttribute("reflectanceB", index: 14, minimum: 0, maximum: 255, step: 1, sortOrder: 14, unknown2: 0)]
			[DisplayName("B")]
			[Description("Diffuse reflection light color on the ground")]
			[DefaultValue((Int16)255)]
			public Int16 ReflectanceB {
				get { return reflectanceB; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ReflectanceB.");
					SetProperty(ref reflectanceB, ref value, ReflectanceBProperty);
				}
			}

			/// <summary>RGB magnification [%]</summary>
			/// <remarks>
			/// Japanese short name: "RGB倍率[％]", Google translated: "RGB magnification [%]".
			/// Japanese description: "地上の乱反射光色", Google translated: "Diffuse reflection light color on the ground".
			/// </remarks>
			[ParameterTableRowAttribute("reflectanceA", index: 15, minimum: 0, maximum: 1000, step: 1, sortOrder: 15, unknown2: 0)]
			[DisplayName("RGB magnification [%]")]
			[Description("Diffuse reflection light color on the ground")]
			[DefaultValue((Int16)100)]
			public Int16 ReflectanceA {
				get { return reflectanceA; }
				set {
					if ((double)value < 0 || (double)value > 1000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1000 for ReflectanceA.");
					SetProperty(ref reflectanceA, ref value, ReflectanceAProperty);
				}
			}

			/// <summary>Padding</summary>
			/// <remarks>
			/// Japanese short name: "パディング", Google translated: "Padding".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("pad_1[2]", index: 16, minimum: 0, maximum: 999, step: 1, sortOrder: 19, unknown2: 0)]
			[DisplayName("Padding")]
			[Description("")]
			[Browsable(false)]
			public Byte[] Pad_1 {
				get { return pad_1; }
				set { SetProperty(ref pad_1, ref value, Pad_1Property); }
			}

			internal LightScattering(ParameterTable table, int index, AssetLoader loader, int next)
				: base(table, index, loader) {
				BinaryReader reader = loader.Reader;

				SunRotX = reader.ReadInt16();
				SunRotY = reader.ReadInt16();
				DistanceMul = reader.ReadInt16();
				SunR = reader.ReadInt16();
				SunG = reader.ReadInt16();
				SunB = reader.ReadInt16();
				SunA = reader.ReadInt16();
				Pad_0 = reader.ReadBytes(2);
				LsHGg = reader.ReadSingle();
				LsBetaRay = reader.ReadSingle();
				LsBetaMie = reader.ReadSingle();
				BlendCoef = reader.ReadInt16();
				ReflectanceR = reader.ReadInt16();
				ReflectanceG = reader.ReadInt16();
				ReflectanceB = reader.ReadInt16();
				ReflectanceA = reader.ReadInt16();
				Pad_1 = reader.ReadBytes(2);
			}

			internal LightScattering(ParameterTable table, int index)
				: base(table, index) {
				SunRotX = (Int16)0;
				SunRotY = (Int16)0;
				DistanceMul = (Int16)100;
				SunR = (Int16)255;
				SunG = (Int16)255;
				SunB = (Int16)255;
				SunA = (Int16)600;
				Pad_0 = new Byte[2];
				LsHGg = (Single)0.8;
				LsBetaRay = (Single)0.2;
				LsBetaMie = (Single)0.01;
				BlendCoef = (Int16)100;
				ReflectanceR = (Int16)255;
				ReflectanceG = (Int16)255;
				ReflectanceB = (Int16)255;
				ReflectanceA = (Int16)100;
				Pad_1 = new Byte[2];
			}

			/// <summary>Write the <see cref="LightScattering"/> row.</summary>
			/// <param name="writer"></param>
			public override void Write(BinaryWriter writer) {
				writer.Write(SunRotX);
				writer.Write(SunRotY);
				writer.Write(DistanceMul);
				writer.Write(SunR);
				writer.Write(SunG);
				writer.Write(SunB);
				writer.Write(SunA);
				writer.Write(Pad_0);
				writer.Write(LsHGg);
				writer.Write(LsBetaRay);
				writer.Write(LsBetaMie);
				writer.Write(BlendCoef);
				writer.Write(ReflectanceR);
				writer.Write(ReflectanceG);
				writer.Write(ReflectanceB);
				writer.Write(ReflectanceA);
				writer.Write(Pad_1);
			}
		}
	}
}
