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
		/// Defined as "TONE_MAP_BANK" in Dark Souls in the file "ToneMapBank.paramdef" (id 06h).
		/// </remarks>
		public class ToneMapping : ParameterTableRow {
			public const string TableName = "TONE_MAP_BANK";

			SByte bloomBegin, bloomMul, bloomBeginFar, bloomMulFar, lightShaftBegin;
			Single bloomNearDist, bloomFarDist, grayKeyValue, minAdaptedLum, maxAdapredLum, adaptSpeed, lightShaftPower, lightShaftAttenRate;
			Byte[] pad_0;

			public static readonly PropertyInfo
				BloomBeginProperty = GetProperty<ToneMapping>("BloomBegin"),
				BloomMulProperty = GetProperty<ToneMapping>("BloomMul"),
				BloomBeginFarProperty = GetProperty<ToneMapping>("BloomBeginFar"),
				BloomMulFarProperty = GetProperty<ToneMapping>("BloomMulFar"),
				BloomNearDistProperty = GetProperty<ToneMapping>("BloomNearDist"),
				BloomFarDistProperty = GetProperty<ToneMapping>("BloomFarDist"),
				GrayKeyValueProperty = GetProperty<ToneMapping>("GrayKeyValue"),
				MinAdaptedLumProperty = GetProperty<ToneMapping>("MinAdaptedLum"),
				MaxAdapredLumProperty = GetProperty<ToneMapping>("MaxAdapredLum"),
				AdaptSpeedProperty = GetProperty<ToneMapping>("AdaptSpeed"),
				LightShaftBeginProperty = GetProperty<ToneMapping>("LightShaftBegin"),
				Pad_0Property = GetProperty<ToneMapping>("Pad_0"),
				LightShaftPowerProperty = GetProperty<ToneMapping>("LightShaftPower"),
				LightShaftAttenRateProperty = GetProperty<ToneMapping>("LightShaftAttenRate");

			/// <summary>Bloom near threshold [ %]</summary>
			/// <remarks>
			/// Japanese short name: "近傍ブルーム閾値[％]", Google translated: "Bloom near threshold [ %]".
			/// Japanese description: "輝度が閾値を越えるとにじみ始めます（近傍）", Google translated: "Begins bleeding brightness exceeds the threshold (in the vicinity )".
			/// </remarks>
			[ParameterTableRowAttribute("bloomBegin", index: 0, minimum: 0, maximum: 100, step: 1, order: 1, unknown2: 0)]
			[DisplayName("Bloom near threshold [ %]")]
			[Description("Begins bleeding brightness exceeds the threshold (in the vicinity )")]
			[DefaultValue((SByte)50)]
			public SByte BloomBegin {
				get { return bloomBegin; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for " + BloomBeginProperty.Name + ".");
					SetProperty(ref bloomBegin, ref value, BloomBeginProperty);
				}
			}

			/// <summary>Near Broome magnification [ %]</summary>
			/// <remarks>
			/// Japanese short name: "近傍ブルーム倍率[％]", Google translated: "Near Broome magnification [ %]".
			/// Japanese description: "閾値以上の値に掛ける値です(0でにじまなくなります)（近傍）", Google translated: "Is the value to be applied to the value equal to or more than the threshold value ( you will not bleed at 0) (in the vicinity )".
			/// </remarks>
			[ParameterTableRowAttribute("bloomMul", index: 1, minimum: 0, maximum: 100, step: 1, order: 2, unknown2: 0)]
			[DisplayName("Near Broome magnification [ %]")]
			[Description("Is the value to be applied to the value equal to or more than the threshold value ( you will not bleed at 0) (in the vicinity )")]
			[DefaultValue((SByte)50)]
			public SByte BloomMul {
				get { return bloomMul; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for " + BloomMulProperty.Name + ".");
					SetProperty(ref bloomMul, ref value, BloomMulProperty);
				}
			}

			/// <summary>Far Bloom threshold [ %]</summary>
			/// <remarks>
			/// Japanese short name: "遠方ブルーム閾値[％]", Google translated: "Far Bloom threshold [ %]".
			/// Japanese description: "輝度が閾値を越えるとにじみ始めます（遠方）", Google translated: "Begins bleeding brightness exceeds the threshold ( far )".
			/// </remarks>
			[ParameterTableRowAttribute("bloomBeginFar", index: 2, minimum: 0, maximum: 100, step: 1, order: 3, unknown2: 0)]
			[DisplayName("Far Bloom threshold [ %]")]
			[Description("Begins bleeding brightness exceeds the threshold ( far )")]
			[DefaultValue((SByte)50)]
			public SByte BloomBeginFar {
				get { return bloomBeginFar; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for " + BloomBeginFarProperty.Name + ".");
					SetProperty(ref bloomBeginFar, ref value, BloomBeginFarProperty);
				}
			}

			/// <summary>Far Bloom magnification [ %]</summary>
			/// <remarks>
			/// Japanese short name: "遠方ブルーム倍率[％]", Google translated: "Far Bloom magnification [ %]".
			/// Japanese description: "閾値以上の値に掛ける値です(0でにじまなくなります)（遠方）", Google translated: "Is the value to be applied to the value equal to or more than the threshold value ( you will not bleed at 0) ( far )".
			/// </remarks>
			[ParameterTableRowAttribute("bloomMulFar", index: 3, minimum: 0, maximum: 100, step: 1, order: 4, unknown2: 0)]
			[DisplayName("Far Bloom magnification [ %]")]
			[Description("Is the value to be applied to the value equal to or more than the threshold value ( you will not bleed at 0) ( far )")]
			[DefaultValue((SByte)50)]
			public SByte BloomMulFar {
				get { return bloomMulFar; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for " + BloomMulFarProperty.Name + ".");
					SetProperty(ref bloomMulFar, ref value, BloomMulFarProperty);
				}
			}

			/// <summary>Bloom neighborhood distance</summary>
			/// <remarks>
			/// Japanese short name: "ブルーム近傍距離", Google translated: "Bloom neighborhood distance".
			/// Japanese description: "この距離まで近傍のパラメータが適用されます。(m)", Google translated: "Parameters of the neighborhood will be applied to this distance . (m)".
			/// </remarks>
			[ParameterTableRowAttribute("bloomNearDist", index: 4, minimum: -10000, maximum: 999999, step: 0.1, order: 5, unknown2: 0)]
			[DisplayName("Bloom neighborhood distance")]
			[Description("Parameters of the neighborhood will be applied to this distance . (m)")]
			[DefaultValue((Single)20)]
			public Single BloomNearDist {
				get { return bloomNearDist; }
				set {
					if ((double)value < -10000 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -10000 to 999999 for " + BloomNearDistProperty.Name + ".");
					SetProperty(ref bloomNearDist, ref value, BloomNearDistProperty);
				}
			}

			/// <summary>Bloom far distance</summary>
			/// <remarks>
			/// Japanese short name: "ブルーム遠方距離", Google translated: "Bloom far distance".
			/// Japanese description: "この距離から遠方のパラメータが適用されます。(m)", Google translated: "Parameters far will be applied from this distance . (m)".
			/// </remarks>
			[ParameterTableRowAttribute("bloomFarDist", index: 5, minimum: -10000, maximum: 999999, step: 0.1, order: 6, unknown2: 0)]
			[DisplayName("Bloom far distance")]
			[Description("Parameters far will be applied from this distance . (m)")]
			[DefaultValue((Single)60)]
			public Single BloomFarDist {
				get { return bloomFarDist; }
				set {
					if ((double)value < -10000 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -10000 to 999999 for " + BloomFarDistProperty.Name + ".");
					SetProperty(ref bloomFarDist, ref value, BloomFarDistProperty);
				}
			}

			/// <summary>Reference value of the tone map</summary>
			/// <remarks>
			/// Japanese short name: "トーンマップの基準値", Google translated: "Reference value of the tone map".
			/// Japanese description: "この値が大きいと画面が明るくなり小さいと画面が暗くなります", Google translated: "Screen will darken and small screen is bright and this value is greater".
			/// </remarks>
			[ParameterTableRowAttribute("grayKeyValue", index: 6, minimum: 0, maximum: 10, step: 0.01, order: 7, unknown2: 0)]
			[DisplayName("Reference value of the tone map")]
			[Description("Screen will darken and small screen is bright and this value is greater")]
			[DefaultValue((Single)0.18)]
			public Single GrayKeyValue {
				get { return grayKeyValue; }
				set {
					if ((double)value < 0 || (double)value > 10)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 10 for " + GrayKeyValueProperty.Name + ".");
					SetProperty(ref grayKeyValue, ref value, GrayKeyValueProperty);
				}
			}

			/// <summary>Minimum brightness adaptation</summary>
			/// <remarks>
			/// Japanese short name: "最小適応輝度", Google translated: "Minimum brightness adaptation".
			/// Japanese description: "値を小さくすると暗い場所に適応して見えるようになります", Google translated: "It will be visible to adapt to a dark place to a smaller value".
			/// </remarks>
			[ParameterTableRowAttribute("minAdaptedLum", index: 7, minimum: 0, maximum: 10, step: 0.01, order: 8, unknown2: 0)]
			[DisplayName("Minimum brightness adaptation")]
			[Description("It will be visible to adapt to a dark place to a smaller value")]
			[DefaultValue((Single)0.1)]
			public Single MinAdaptedLum {
				get { return minAdaptedLum; }
				set {
					if ((double)value < 0 || (double)value > 10)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 10 for " + MinAdaptedLumProperty.Name + ".");
					SetProperty(ref minAdaptedLum, ref value, MinAdaptedLumProperty);
				}
			}

			/// <summary>Maximum fitness brightness</summary>
			/// <remarks>
			/// Japanese short name: "最大適応輝度", Google translated: "Maximum fitness brightness".
			/// Japanese description: "値を大きくすると明るい場所に適応して見えるようになります", Google translated: "It will be visible to adapt to bright place a higher value".
			/// </remarks>
			[ParameterTableRowAttribute("maxAdapredLum", index: 8, minimum: 0, maximum: 10, step: 0.01, order: 9, unknown2: 0)]
			[DisplayName("Maximum fitness brightness")]
			[Description("It will be visible to adapt to bright place a higher value")]
			[DefaultValue((Single)0.2)]
			public Single MaxAdapredLum {
				get { return maxAdapredLum; }
				set {
					if ((double)value < 0 || (double)value > 10)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 10 for " + MaxAdapredLumProperty.Name + ".");
					SetProperty(ref maxAdapredLum, ref value, MaxAdapredLumProperty);
				}
			}

			/// <summary>Adaptation speed</summary>
			/// <remarks>
			/// Japanese short name: "適応速度", Google translated: "Adaptation speed".
			/// Japanese description: "値を大きくする明順応、暗順応の速度が速くなります", Google translated: "Light adaptation to increase the value , speed of dark adaptation is faster".
			/// </remarks>
			[ParameterTableRowAttribute("adaptSpeed", index: 9, minimum: 0, maximum: 10, step: 0.01, order: 10, unknown2: 0)]
			[DisplayName("Adaptation speed")]
			[Description("Light adaptation to increase the value , speed of dark adaptation is faster")]
			[DefaultValue((Single)1)]
			public Single AdaptSpeed {
				get { return adaptSpeed; }
				set {
					if ((double)value < 0 || (double)value > 10)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 10 for " + AdaptSpeedProperty.Name + ".");
					SetProperty(ref adaptSpeed, ref value, AdaptSpeedProperty);
				}
			}

			/// <summary>Light shaft threshold [ %]</summary>
			/// <remarks>
			/// Japanese short name: "ライトシャフト閾値[％]", Google translated: "Light shaft threshold [ %]".
			/// Japanese description: "輝度が閾値を越えると光の筋がでます", Google translated: "Streaks of light will appear brightness exceeds a threshold".
			/// </remarks>
			[ParameterTableRowAttribute("lightShaftBegin", index: 10, minimum: 0, maximum: 100, step: 1, order: 11, unknown2: 0)]
			[DisplayName("Light shaft threshold [ %]")]
			[Description("Streaks of light will appear brightness exceeds a threshold")]
			[DefaultValue((SByte)50)]
			public SByte LightShaftBegin {
				get { return lightShaftBegin; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for " + LightShaftBeginProperty.Name + ".");
					SetProperty(ref lightShaftBegin, ref value, LightShaftBeginProperty);
				}
			}

			/// <summary>Padding</summary>
			/// <remarks>
			/// Japanese short name: "パディング", Google translated: "Padding".
			/// Japanese description: "ダミー", Google translated: "Dummy".
			/// </remarks>
			[ParameterTableRowAttribute("pad_0[3]", index: 11, minimum: 0, maximum: 0, step: 0, order: 15, unknown2: 0)]
			[DisplayName("Padding")]
			[Description("Dummy")]
			[Browsable(false)]
			public Byte[] Pad_0 {
				get { return pad_0; }
				set { SetProperty(ref pad_0, ref value, Pad_0Property); }
			}

			/// <summary>The strength of the light shaft</summary>
			/// <remarks>
			/// Japanese short name: "ライトシャフトの強さ", Google translated: "The strength of the light shaft".
			/// Japanese description: "ライトシャフトの強さです。(0で消えます)", Google translated: "Is the strength of the light shaft . ( Will close by 0)".
			/// </remarks>
			[ParameterTableRowAttribute("lightShaftPower", index: 12, minimum: 0, maximum: 10, step: 0.01, order: 12, unknown2: 0)]
			[DisplayName("The strength of the light shaft")]
			[Description("Is the strength of the light shaft . ( Will close by 0)")]
			[DefaultValue((Single)0)]
			public Single LightShaftPower {
				get { return lightShaftPower; }
				set {
					if ((double)value < 0 || (double)value > 10)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 10 for " + LightShaftPowerProperty.Name + ".");
					SetProperty(ref lightShaftPower, ref value, LightShaftPowerProperty);
				}
			}

			/// <summary>Attenuation factor of the light shaft</summary>
			/// <remarks>
			/// Japanese short name: "ライトシャフトの減衰率", Google translated: "Attenuation factor of the light shaft".
			/// Japanese description: "ライトシャフトの減衰率です。小さくすると短くなります", Google translated: "It is the attenuation factor of the light shaft . It is short and small".
			/// </remarks>
			[ParameterTableRowAttribute("lightShaftAttenRate", index: 13, minimum: 0, maximum: 1, step: 0.01, order: 13, unknown2: 0)]
			[DisplayName("Attenuation factor of the light shaft")]
			[Description("It is the attenuation factor of the light shaft . It is short and small")]
			[DefaultValue((Single)0.95)]
			public Single LightShaftAttenRate {
				get { return lightShaftAttenRate; }
				set {
					if ((double)value < 0 || (double)value > 1)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1 for " + LightShaftAttenRateProperty.Name + ".");
					SetProperty(ref lightShaftAttenRate, ref value, LightShaftAttenRateProperty);
				}
			}

			internal ToneMapping(ParameterTable table, int index, AssetLoader loader, int next)
				: base(table, index, loader) {
				BinaryReader reader = loader.Reader;

				BloomBegin = reader.ReadSByte();
				BloomMul = reader.ReadSByte();
				BloomBeginFar = reader.ReadSByte();
				BloomMulFar = reader.ReadSByte();
				BloomNearDist = reader.ReadSingle();
				BloomFarDist = reader.ReadSingle();
				GrayKeyValue = reader.ReadSingle();
				MinAdaptedLum = reader.ReadSingle();
				MaxAdapredLum = reader.ReadSingle();
				AdaptSpeed = reader.ReadSingle();
				LightShaftBegin = reader.ReadSByte();
				Pad_0 = reader.ReadBytes(3);
				LightShaftPower = reader.ReadSingle();
				LightShaftAttenRate = reader.ReadSingle();
			}

			internal ToneMapping(ParameterTable table, int index)
				: base(table, index) {
				BloomBegin = (SByte)50;
				BloomMul = (SByte)50;
				BloomBeginFar = (SByte)50;
				BloomMulFar = (SByte)50;
				BloomNearDist = (Single)20;
				BloomFarDist = (Single)60;
				GrayKeyValue = (Single)0.18;
				MinAdaptedLum = (Single)0.1;
				MaxAdapredLum = (Single)0.2;
				AdaptSpeed = (Single)1;
				LightShaftBegin = (SByte)50;
				Pad_0 = new Byte[3];
				LightShaftPower = (Single)0;
				LightShaftAttenRate = (Single)0.95;
			}

			public override void Write(BinaryWriter writer) {
				writer.Write(BloomBegin);
				writer.Write(BloomMul);
				writer.Write(BloomBeginFar);
				writer.Write(BloomMulFar);
				writer.Write(BloomNearDist);
				writer.Write(BloomFarDist);
				writer.Write(GrayKeyValue);
				writer.Write(MinAdaptedLum);
				writer.Write(MaxAdapredLum);
				writer.Write(AdaptSpeed);
				writer.Write(LightShaftBegin);
				writer.Write(Pad_0);
				writer.Write(LightShaftPower);
				writer.Write(LightShaftAttenRate);
			}
		}
	}
}
