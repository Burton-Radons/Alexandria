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
		/// Defined as "SHADOW_BANK" in Dark Souls in the file "ShadowBank.paramdef" (id 08h).
		/// </remarks>
		public class Shadow : ParameterTableRow {
			/// <summary>The name of the table in the file.</summary>
			public const string TableName = "SHADOW_BANK";

			Int16 lightDegRotX, lightDegRotY, densityRatio, colR, colG, colB;
			Single beginDist, endDist, calibulateFar, fadeBeginDist, fadeDist, persedDepthOffset, ｇradFactor, shadowVolumeDepth;

			/// <summary>A property in the class.</summary>
			public static readonly PropertyInfo
				LightDegRotXProperty = GetProperty<Shadow>("LightDegRotX"),
				LightDegRotYProperty = GetProperty<Shadow>("LightDegRotY"),
				DensityRatioProperty = GetProperty<Shadow>("DensityRatio"),
				ColRProperty = GetProperty<Shadow>("ColR"),
				ColGProperty = GetProperty<Shadow>("ColG"),
				ColBProperty = GetProperty<Shadow>("ColB"),
				BeginDistProperty = GetProperty<Shadow>("BeginDist"),
				EndDistProperty = GetProperty<Shadow>("EndDist"),
				CalibulateFarProperty = GetProperty<Shadow>("CalibulateFar"),
				FadeBeginDistProperty = GetProperty<Shadow>("FadeBeginDist"),
				FadeDistProperty = GetProperty<Shadow>("FadeDist"),
				PersedDepthOffsetProperty = GetProperty<Shadow>("PersedDepthOffset"),
				ＧradFactorProperty = GetProperty<Shadow>("ＧradFactor"),
				ShadowVolumeDepthProperty = GetProperty<Shadow>("ShadowVolumeDepth");

			/// <summary>X light source angle [deg]</summary>
			/// <remarks>
			/// Japanese short name: "光源X角度[deg]", Google translated: "X light source angle [deg]".
			/// Japanese description: "マップに影を落とす光源のX角度", Google translated: "X angle of the light source that cast a shadow on the map".
			/// </remarks>
			[ParameterTableRowAttribute("lightDegRotX", index: 0, minimum: -90, maximum: 90, step: 1, sortOrder: 1, unknown2: 0)]
			[DisplayName("X light source angle [deg]")]
			[Description("X angle of the light source that cast a shadow on the map")]
			[DefaultValue((Int16)0)]
			public Int16 LightDegRotX {
				get { return lightDegRotX; }
				set {
					if ((double)value < -90 || (double)value > 90)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -90 to 90 for " + LightDegRotXProperty.Name + ".");
					SetProperty(ref lightDegRotX, ref value, LightDegRotXProperty);
				}
			}

			/// <summary>Light source Y angle [deg]</summary>
			/// <remarks>
			/// Japanese short name: "光源Y角度[deg]", Google translated: "Light source Y angle [deg]".
			/// Japanese description: "マップに影を落とす光源のY角度", Google translated: "Y angle of the light source that cast a shadow on the map".
			/// </remarks>
			[ParameterTableRowAttribute("lightDegRotY", index: 1, minimum: -180, maximum: 180, step: 1, sortOrder: 2, unknown2: 1)]
			[DisplayName("Light source Y angle [deg]")]
			[Description("Y angle of the light source that cast a shadow on the map")]
			[DefaultValue((Int16)0)]
			public Int16 LightDegRotY {
				get { return lightDegRotY; }
				set {
					if ((double)value < -180 || (double)value > 180)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -180 to 180 for " + LightDegRotYProperty.Name + ".");
					SetProperty(ref lightDegRotY, ref value, LightDegRotYProperty);
				}
			}

			/// <summary>Saturation [ %]</summary>
			/// <remarks>
			/// Japanese short name: "濃さ[％]", Google translated: "Saturation [ %]".
			/// Japanese description: "100％で最も濃くなります", Google translated: "The darker the most 100%".
			/// </remarks>
			[ParameterTableRowAttribute("densityRatio", index: 2, minimum: 0, maximum: 100, step: 1, sortOrder: 3, unknown2: 0)]
			[DisplayName("Saturation [ %]")]
			[Description("The darker the most 100%")]
			[DefaultValue((Int16)100)]
			public Int16 DensityRatio {
				get { return densityRatio; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for " + DensityRatioProperty.Name + ".");
					SetProperty(ref densityRatio, ref value, DensityRatioProperty);
				}
			}

			/// <summary>Color R</summary>
			/// <remarks>
			/// Japanese short name: "色R", Google translated: "Color R".
			/// Japanese description: "0～255", Google translated: "0-255".
			/// </remarks>
			[ParameterTableRowAttribute("colR", index: 3, minimum: 0, maximum: 255, step: 1, sortOrder: 4, unknown2: 0)]
			[DisplayName("Color R")]
			[Description("0-255")]
			[DefaultValue((Int16)0)]
			public Int16 ColR {
				get { return colR; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for " + ColRProperty.Name + ".");
					SetProperty(ref colR, ref value, ColRProperty);
				}
			}

			/// <summary>Color G</summary>
			/// <remarks>
			/// Japanese short name: "色G", Google translated: "Color G".
			/// Japanese description: "0～255", Google translated: "0-255".
			/// </remarks>
			[ParameterTableRowAttribute("colG", index: 4, minimum: 0, maximum: 255, step: 1, sortOrder: 5, unknown2: 0)]
			[DisplayName("Color G")]
			[Description("0-255")]
			[DefaultValue((Int16)0)]
			public Int16 ColG {
				get { return colG; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for " + ColGProperty.Name + ".");
					SetProperty(ref colG, ref value, ColGProperty);
				}
			}

			/// <summary>Color B</summary>
			/// <remarks>
			/// Japanese short name: "色B", Google translated: "Color B".
			/// Japanese description: "0～255", Google translated: "0-255".
			/// </remarks>
			[ParameterTableRowAttribute("colB", index: 5, minimum: 0, maximum: 255, step: 1, sortOrder: 6, unknown2: 0)]
			[DisplayName("Color B")]
			[Description("0-255")]
			[DefaultValue((Int16)0)]
			public Int16 ColB {
				get { return colB; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for " + ColBProperty.Name + ".");
					SetProperty(ref colB, ref value, ColBProperty);
				}
			}

			/// <summary>Start distance [m]</summary>
			/// <remarks>
			/// Japanese short name: "開始距離[m]", Google translated: "Start distance [m]".
			/// Japanese description: "0にしておくとカメラのニアクリップを使用", Google translated: "Use of the camera near clip After you have a 0".
			/// </remarks>
			[ParameterTableRowAttribute("beginDist", index: 6, minimum: 0, maximum: 99999, step: 0.1, sortOrder: 7, unknown2: 0)]
			[DisplayName("Start distance [m]")]
			[Description("Use of the camera near clip After you have a 0")]
			[DefaultValue((Single)0)]
			public Single BeginDist {
				get { return beginDist; }
				set {
					if ((double)value < 0 || (double)value > 99999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99999 for " + BeginDistProperty.Name + ".");
					SetProperty(ref beginDist, ref value, BeginDistProperty);
				}
			}

			/// <summary>End distance [m]</summary>
			/// <remarks>
			/// Japanese short name: "終了距離[m]", Google translated: "End distance [m]".
			/// Japanese description: "終了距離[m]", Google translated: "End distance [m]".
			/// </remarks>
			[ParameterTableRowAttribute("endDist", index: 7, minimum: 0, maximum: 99999, step: 0.1, sortOrder: 8, unknown2: 0)]
			[DisplayName("End distance [m]")]
			[Description("End distance [m]")]
			[DefaultValue((Single)300)]
			public Single EndDist {
				get { return endDist; }
				set {
					if ((double)value < 0 || (double)value > 99999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99999 for " + EndDistProperty.Name + ".");
					SetProperty(ref endDist, ref value, EndDistProperty);
				}
			}

			/// <summary>Automatic adjustment far clip distance (PSM for )</summary>
			/// <remarks>
			/// Japanese short name: "ファークリップ自動調整距離(PSM用)", Google translated: "Automatic adjustment far clip distance (PSM for )".
			/// Japanese description: "光源方向を向いたときに終了距離を指定した距離分短くします", Google translated: "Shorten distance minutes that specifies the end distance when facing the light source direction".
			/// </remarks>
			[ParameterTableRowAttribute("calibulateFar", index: 8, minimum: 0, maximum: 99999, step: 0.1, sortOrder: 9, unknown2: 0)]
			[DisplayName("Automatic adjustment far clip distance (PSM for )")]
			[Description("Shorten distance minutes that specifies the end distance when facing the light source direction")]
			[DefaultValue((Single)0)]
			public Single CalibulateFar {
				get { return calibulateFar; }
				set {
					if ((double)value < 0 || (double)value > 99999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99999 for " + CalibulateFarProperty.Name + ".");
					SetProperty(ref calibulateFar, ref value, CalibulateFarProperty);
				}
			}

			/// <summary>Fade start distance [m]</summary>
			/// <remarks>
			/// Japanese short name: "フェード開始距離[m]", Google translated: "Fade start distance [m]".
			/// Japanese description: "フェード開始距離[m]", Google translated: "Fade start distance [m]".
			/// </remarks>
			[ParameterTableRowAttribute("fadeBeginDist", index: 9, minimum: 0, maximum: 99999, step: 0.1, sortOrder: 10, unknown2: 0)]
			[DisplayName("Fade start distance [m]")]
			[Description("Fade start distance [m]")]
			[DefaultValue((Single)9999)]
			public Single FadeBeginDist {
				get { return fadeBeginDist; }
				set {
					if ((double)value < 0 || (double)value > 99999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 99999 for " + FadeBeginDistProperty.Name + ".");
					SetProperty(ref fadeBeginDist, ref value, FadeBeginDistProperty);
				}
			}

			/// <summary>Fade distance [m]</summary>
			/// <remarks>
			/// Japanese short name: "フェード距離[m]", Google translated: "Fade distance [m]".
			/// Japanese description: "フェード距離[m]", Google translated: "Fade distance [m]".
			/// </remarks>
			[ParameterTableRowAttribute("fadeDist", index: 10, minimum: -1, maximum: 99999, step: 0.1, sortOrder: 11, unknown2: 0)]
			[DisplayName("Fade distance [m]")]
			[Description("Fade distance [m]")]
			[DefaultValue((Single)(-1))]
			public Single FadeDist {
				get { return fadeDist; }
				set {
					if ((double)value < -1 || (double)value > 99999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 99999 for " + FadeDistProperty.Name + ".");
					SetProperty(ref fadeDist, ref value, FadeDistProperty);
				}
			}

			/// <summary>Z offset (PSM for )</summary>
			/// <remarks>
			/// Japanese short name: "Zオフセット(PSM用)", Google translated: "Z offset (PSM for )".
			/// Japanese description: "Zオフセット [m] （－）にするほどセルフシャードウが出難くなります(PSM用)", Google translated: "Z offset [m] (-) will be difficult to come up with self Shah dough enough to to (PSM for )".
			/// </remarks>
			[ParameterTableRowAttribute("persedDepthOffset", index: 11, minimum: -1, maximum: 1, step: 0.0001, sortOrder: 12, unknown2: 0)]
			[DisplayName("Z offset (PSM for )")]
			[Description("Z offset [m] (-) will be difficult to come up with self Shah dough enough to to (PSM for )")]
			[DefaultValue((Single)(-0.0001))]
			public Single PersedDepthOffset {
				get { return persedDepthOffset; }
				set {
					if ((double)value < -1 || (double)value > 1)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1 for " + PersedDepthOffsetProperty.Name + ".");
					SetProperty(ref persedDepthOffset, ref value, PersedDepthOffsetProperty);
				}
			}

			/// <summary></summary>
			/// <remarks>
			/// Japanese short name: "パース調整パラメータ(PSM用)", Google translated: "".
			/// Japanese description: "－にするとシャドウマップのパースが弱まり,＋にするとパースが強まります", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("ｇradFactor", index: 12, minimum: -3, maximum: 5, step: 0.01, sortOrder: 13, unknown2: 0)]
			[DisplayName("")]
			[Description("")]
			[DefaultValue((Single)0)]
			public Single ＧradFactor {
				get { return ｇradFactor; }
				set {
					if ((double)value < -3 || (double)value > 5)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -3 to 5 for " + ＧradFactorProperty.Name + ".");
					SetProperty(ref ｇradFactor, ref value, ＧradFactorProperty);
				}
			}

			/// <summary>The length of the shadow volume [m]</summary>
			/// <remarks>
			/// Japanese short name: "シャドウボリュームの長さ[m]", Google translated: "The length of the shadow volume [m]".
			/// Japanese description: "ビルなど高いオブジェクトの影を落としたいときは値を大きくします", Google translated: "Increase the value if you want to cast a shadow of the object , such as building high".
			/// </remarks>
			[ParameterTableRowAttribute("shadowVolumeDepth", index: 13, minimum: 1, maximum: 9999, step: 0.1, sortOrder: 14, unknown2: 0)]
			[DisplayName("The length of the shadow volume [m]")]
			[Description("Increase the value if you want to cast a shadow of the object , such as building high")]
			[DefaultValue((Single)10)]
			public Single ShadowVolumeDepth {
				get { return shadowVolumeDepth; }
				set {
					if ((double)value < 1 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 1 to 9999 for " + ShadowVolumeDepthProperty.Name + ".");
					SetProperty(ref shadowVolumeDepth, ref value, ShadowVolumeDepthProperty);
				}
			}

			internal Shadow(ParameterTable table, int index, AssetLoader loader, int next)
				: base(table, index, loader) {
				BinaryReader reader = loader.Reader;

				LightDegRotX = reader.ReadInt16();
				LightDegRotY = reader.ReadInt16();
				DensityRatio = reader.ReadInt16();
				ColR = reader.ReadInt16();
				ColG = reader.ReadInt16();
				ColB = reader.ReadInt16();
				BeginDist = reader.ReadSingle();
				EndDist = reader.ReadSingle();
				CalibulateFar = reader.ReadSingle();
				FadeBeginDist = reader.ReadSingle();
				FadeDist = reader.ReadSingle();
				PersedDepthOffset = reader.ReadSingle();
				ＧradFactor = reader.ReadSingle();
				ShadowVolumeDepth = reader.ReadSingle();
			}

			internal Shadow(ParameterTable table, int index)
				: base(table, index) {
				LightDegRotX = (Int16)0;
				LightDegRotY = (Int16)0;
				DensityRatio = (Int16)100;
				ColR = (Int16)0;
				ColG = (Int16)0;
				ColB = (Int16)0;
				BeginDist = (Single)0;
				EndDist = (Single)300;
				CalibulateFar = (Single)0;
				FadeBeginDist = (Single)9999;
				FadeDist = (Single)(-1);
				PersedDepthOffset = (Single)(-0.0001);
				ＧradFactor = (Single)0;
				ShadowVolumeDepth = (Single)10;
			}

			/// <summary>
			/// Write the <see cref="Shadow"/> row.
			/// </summary>
			/// <param name="writer"></param>
			public override void Write(BinaryWriter writer) {
				writer.Write(LightDegRotX);
				writer.Write(LightDegRotY);
				writer.Write(DensityRatio);
				writer.Write(ColR);
				writer.Write(ColG);
				writer.Write(ColB);
				writer.Write(BeginDist);
				writer.Write(EndDist);
				writer.Write(CalibulateFar);
				writer.Write(FadeBeginDist);
				writer.Write(FadeDist);
				writer.Write(PersedDepthOffset);
				writer.Write(ＧradFactor);
				writer.Write(ShadowVolumeDepth);
			}
		}
	}
}
