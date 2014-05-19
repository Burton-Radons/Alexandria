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
		/// <remarks>From "EnvLightTexBank.paramdef" (id 0Ah).</remarks>
		public class EnvironmentLight : ParameterTableRow {
			public const string TableName = "ENV_LIGHT_TEX_BANK";

			SByte isUse, autoUpdate;
			Byte[] pad_0, pad_Dif0, pad_Spc0, pad_Spc1, pad_Spc2, pad_Spc3, pad_00, pad_01, pad_02, pad_03, pad_04, pad_05, pad_06, pad_07, pad_08, pad_09;
			Int16 invMulCol, resNameId_Dif0, invMulCol_Dif0, resNameId_Spc0, invMulCol_Spc0, resNameId_Spc1, invMulCol_Spc1, resNameId_Spc2, invMulCol_Spc2, resNameId_Spc3, invMulCol_Spc3, degRotX_00, degRotY_00, colR_00, colG_00, colB_00, colA_00, degRotX_01, degRotY_01, colR_01, colG_01, colB_01, colA_01, degRotX_02, degRotY_02, colR_02, colG_02, colB_02, colA_02, degRotX_03, degRotY_03, colR_03, colG_03, colB_03, colA_03, degRotX_04, degRotY_04, colR_04, colG_04, colB_04, colA_04, degRotX_05, degRotY_05, colR_05, colG_05, colB_05, colA_05, degRotX_06, degRotY_06, colR_06, colG_06, colB_06, colA_06, degRotX_07, degRotY_07, colR_07, colG_07, colB_07, colA_07, degRotX_08, degRotY_08, colR_08, colG_08, colB_08, colA_08, degRotX_09, degRotY_09, colR_09, colG_09, colB_09, colA_09;
			Single sepcPow_Dif0, sepcPow_Spc0, sepcPow_Spc1, sepcPow_Spc2, sepcPow_Spc3;

			public static readonly PropertyInfo
				IsUseProperty = GetProperty<EnvironmentLight>("IsUse"),
				AutoUpdateProperty = GetProperty<EnvironmentLight>("AutoUpdate"),
				Pad_0Property = GetProperty<EnvironmentLight>("Pad_0"),
				InvMulColProperty = GetProperty<EnvironmentLight>("InvMulCol"),
				ResNameId_Dif0Property = GetProperty<EnvironmentLight>("ResNameId_Dif0"),
				InvMulCol_Dif0Property = GetProperty<EnvironmentLight>("InvMulCol_Dif0"),
				SepcPow_Dif0Property = GetProperty<EnvironmentLight>("SepcPow_Dif0"),
				Pad_Dif0Property = GetProperty<EnvironmentLight>("Pad_Dif0"),
				ResNameId_Spc0Property = GetProperty<EnvironmentLight>("ResNameId_Spc0"),
				InvMulCol_Spc0Property = GetProperty<EnvironmentLight>("InvMulCol_Spc0"),
				SepcPow_Spc0Property = GetProperty<EnvironmentLight>("SepcPow_Spc0"),
				Pad_Spc0Property = GetProperty<EnvironmentLight>("Pad_Spc0"),
				ResNameId_Spc1Property = GetProperty<EnvironmentLight>("ResNameId_Spc1"),
				InvMulCol_Spc1Property = GetProperty<EnvironmentLight>("InvMulCol_Spc1"),
				SepcPow_Spc1Property = GetProperty<EnvironmentLight>("SepcPow_Spc1"),
				Pad_Spc1Property = GetProperty<EnvironmentLight>("Pad_Spc1"),
				ResNameId_Spc2Property = GetProperty<EnvironmentLight>("ResNameId_Spc2"),
				InvMulCol_Spc2Property = GetProperty<EnvironmentLight>("InvMulCol_Spc2"),
				SepcPow_Spc2Property = GetProperty<EnvironmentLight>("SepcPow_Spc2"),
				Pad_Spc2Property = GetProperty<EnvironmentLight>("Pad_Spc2"),
				ResNameId_Spc3Property = GetProperty<EnvironmentLight>("ResNameId_Spc3"),
				InvMulCol_Spc3Property = GetProperty<EnvironmentLight>("InvMulCol_Spc3"),
				SepcPow_Spc3Property = GetProperty<EnvironmentLight>("SepcPow_Spc3"),
				Pad_Spc3Property = GetProperty<EnvironmentLight>("Pad_Spc3"),
				DegRotX_00Property = GetProperty<EnvironmentLight>("DegRotX_00"),
				DegRotY_00Property = GetProperty<EnvironmentLight>("DegRotY_00"),
				ColR_00Property = GetProperty<EnvironmentLight>("ColR_00"),
				ColG_00Property = GetProperty<EnvironmentLight>("ColG_00"),
				ColB_00Property = GetProperty<EnvironmentLight>("ColB_00"),
				ColA_00Property = GetProperty<EnvironmentLight>("ColA_00"),
				Pad_00Property = GetProperty<EnvironmentLight>("Pad_00"),
				DegRotX_01Property = GetProperty<EnvironmentLight>("DegRotX_01"),
				DegRotY_01Property = GetProperty<EnvironmentLight>("DegRotY_01"),
				ColR_01Property = GetProperty<EnvironmentLight>("ColR_01"),
				ColG_01Property = GetProperty<EnvironmentLight>("ColG_01"),
				ColB_01Property = GetProperty<EnvironmentLight>("ColB_01"),
				ColA_01Property = GetProperty<EnvironmentLight>("ColA_01"),
				Pad_01Property = GetProperty<EnvironmentLight>("Pad_01"),
				DegRotX_02Property = GetProperty<EnvironmentLight>("DegRotX_02"),
				DegRotY_02Property = GetProperty<EnvironmentLight>("DegRotY_02"),
				ColR_02Property = GetProperty<EnvironmentLight>("ColR_02"),
				ColG_02Property = GetProperty<EnvironmentLight>("ColG_02"),
				ColB_02Property = GetProperty<EnvironmentLight>("ColB_02"),
				ColA_02Property = GetProperty<EnvironmentLight>("ColA_02"),
				Pad_02Property = GetProperty<EnvironmentLight>("Pad_02"),
				DegRotX_03Property = GetProperty<EnvironmentLight>("DegRotX_03"),
				DegRotY_03Property = GetProperty<EnvironmentLight>("DegRotY_03"),
				ColR_03Property = GetProperty<EnvironmentLight>("ColR_03"),
				ColG_03Property = GetProperty<EnvironmentLight>("ColG_03"),
				ColB_03Property = GetProperty<EnvironmentLight>("ColB_03"),
				ColA_03Property = GetProperty<EnvironmentLight>("ColA_03"),
				Pad_03Property = GetProperty<EnvironmentLight>("Pad_03"),
				DegRotX_04Property = GetProperty<EnvironmentLight>("DegRotX_04"),
				DegRotY_04Property = GetProperty<EnvironmentLight>("DegRotY_04"),
				ColR_04Property = GetProperty<EnvironmentLight>("ColR_04"),
				ColG_04Property = GetProperty<EnvironmentLight>("ColG_04"),
				ColB_04Property = GetProperty<EnvironmentLight>("ColB_04"),
				ColA_04Property = GetProperty<EnvironmentLight>("ColA_04"),
				Pad_04Property = GetProperty<EnvironmentLight>("Pad_04"),
				DegRotX_05Property = GetProperty<EnvironmentLight>("DegRotX_05"),
				DegRotY_05Property = GetProperty<EnvironmentLight>("DegRotY_05"),
				ColR_05Property = GetProperty<EnvironmentLight>("ColR_05"),
				ColG_05Property = GetProperty<EnvironmentLight>("ColG_05"),
				ColB_05Property = GetProperty<EnvironmentLight>("ColB_05"),
				ColA_05Property = GetProperty<EnvironmentLight>("ColA_05"),
				Pad_05Property = GetProperty<EnvironmentLight>("Pad_05"),
				DegRotX_06Property = GetProperty<EnvironmentLight>("DegRotX_06"),
				DegRotY_06Property = GetProperty<EnvironmentLight>("DegRotY_06"),
				ColR_06Property = GetProperty<EnvironmentLight>("ColR_06"),
				ColG_06Property = GetProperty<EnvironmentLight>("ColG_06"),
				ColB_06Property = GetProperty<EnvironmentLight>("ColB_06"),
				ColA_06Property = GetProperty<EnvironmentLight>("ColA_06"),
				Pad_06Property = GetProperty<EnvironmentLight>("Pad_06"),
				DegRotX_07Property = GetProperty<EnvironmentLight>("DegRotX_07"),
				DegRotY_07Property = GetProperty<EnvironmentLight>("DegRotY_07"),
				ColR_07Property = GetProperty<EnvironmentLight>("ColR_07"),
				ColG_07Property = GetProperty<EnvironmentLight>("ColG_07"),
				ColB_07Property = GetProperty<EnvironmentLight>("ColB_07"),
				ColA_07Property = GetProperty<EnvironmentLight>("ColA_07"),
				Pad_07Property = GetProperty<EnvironmentLight>("Pad_07"),
				DegRotX_08Property = GetProperty<EnvironmentLight>("DegRotX_08"),
				DegRotY_08Property = GetProperty<EnvironmentLight>("DegRotY_08"),
				ColR_08Property = GetProperty<EnvironmentLight>("ColR_08"),
				ColG_08Property = GetProperty<EnvironmentLight>("ColG_08"),
				ColB_08Property = GetProperty<EnvironmentLight>("ColB_08"),
				ColA_08Property = GetProperty<EnvironmentLight>("ColA_08"),
				Pad_08Property = GetProperty<EnvironmentLight>("Pad_08"),
				DegRotX_09Property = GetProperty<EnvironmentLight>("DegRotX_09"),
				DegRotY_09Property = GetProperty<EnvironmentLight>("DegRotY_09"),
				ColR_09Property = GetProperty<EnvironmentLight>("ColR_09"),
				ColG_09Property = GetProperty<EnvironmentLight>("ColG_09"),
				ColB_09Property = GetProperty<EnvironmentLight>("ColB_09"),
				ColA_09Property = GetProperty<EnvironmentLight>("ColA_09"),
				Pad_09Property = GetProperty<EnvironmentLight>("Pad_09");

			/// <summary>Whether to use</summary>
			/// <remarks>
			/// Japanese short name: "使用するか", Google translated: "Whether to use".
			/// Japanese description: "0:しない, 1:する", Google translated: "And without it to 1 : 0:".
			/// </remarks>
			[ParameterTableRowAttribute("isUse", index: 0, minimum: 0, maximum: 1, step: 1, order: 1, unknown2: 0)]
			[DisplayName("Whether to use")]
			[Description("And without it to 1 : 0:")]
			[DefaultValue((SByte)0)]
			public SByte IsUse {
				get { return isUse; }
				set {
					if ((double)value < 0 || (double)value > 1)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1 for IsUse.");
					SetProperty(ref isUse, ref value, IsUseProperty);
				}
			}

			/// <summary>Or automatic update</summary>
			/// <remarks>
			/// Japanese short name: "自動更新するか", Google translated: "Or automatic update".
			/// Japanese description: "0:しない, 1:する", Google translated: "And without it to 1 : 0:".
			/// </remarks>
			[ParameterTableRowAttribute("autoUpdate", index: 1, minimum: 0, maximum: 1, step: 1, order: 2, unknown2: 0)]
			[DisplayName("Or automatic update")]
			[Description("And without it to 1 : 0:")]
			[DefaultValue((SByte)0)]
			public SByte AutoUpdate {
				get { return autoUpdate; }
				set {
					if ((double)value < 0 || (double)value > 1)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1 for AutoUpdate.");
					SetProperty(ref autoUpdate, ref value, AutoUpdateProperty);
				}
			}

			/// <summary>Padding</summary>
			/// <remarks>
			/// Japanese short name: "パディング", Google translated: "Padding".
			/// Japanese description: "ダミー", Google translated: "Dummy".
			/// </remarks>
			[ParameterTableRowAttribute("pad_0[12]", index: 2, minimum: 0, maximum: 0, step: 0, order: 95, unknown2: 0)]
			[DisplayName("Padding")]
			[Description("Dummy")]
			public Byte[] Pad_0 {
				get { return pad_0; }
				set { SetProperty(ref pad_0, ref value, Pad_0Property); }
			}

			/// <summary>RGB reverse magnification [ %]</summary>
			/// <remarks>
			/// Japanese short name: "RGB逆倍率[％]", Google translated: "RGB reverse magnification [ %]".
			/// Japanese description: "全ての光源色にこの値の逆数を掛けます", Google translated: "I multiplied by the reciprocal of this value to the light source color of all".
			/// </remarks>
			[ParameterTableRowAttribute("invMulCol", index: 3, minimum: 1, maximum: 10000, step: 1, order: 3, unknown2: 0)]
			[DisplayName("RGB reverse magnification [ %]")]
			[Description("I multiplied by the reciprocal of this value to the light source color of all")]
			[DefaultValue((Int16)100)]
			public Int16 InvMulCol {
				get { return invMulCol; }
				set {
					if ((double)value < 1 || (double)value > 10000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 1 to 10000 for InvMulCol.");
					SetProperty(ref invMulCol, ref value, InvMulColProperty);
				}
			}

			/// <summary>Resource name ID</summary>
			/// <remarks>
			/// Japanese short name: "リソース名ID", Google translated: "Resource name ID".
			/// Japanese description: "[DIF0] テクスチャファイル名のID(-1:なし)", Google translated: "ID of [DIF0] texture file name ( -1 : none)".
			/// </remarks>
			[ParameterTableRowAttribute("resNameId_Dif0", index: 4, minimum: -1, maximum: 999, step: 1, order: 4, unknown2: 4)]
			[DisplayName("Resource name ID")]
			[Description("ID of [DIF0] texture file name ( -1 : none)")]
			[DefaultValue((Int16)0)]
			public Int16 ResNameId_Dif0 {
				get { return resNameId_Dif0; }
				set {
					if ((double)value < -1 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999 for ResNameId_Dif0.");
					SetProperty(ref resNameId_Dif0, ref value, ResNameId_Dif0Property);
				}
			}

			/// <summary>RGB reverse magnification [ %]</summary>
			/// <remarks>
			/// Japanese short name: "RGB逆倍率[％]", Google translated: "RGB reverse magnification [ %]".
			/// Japanese description: "[DIF0] 光源色にこの値の逆数を掛けます", Google translated: "I multiplied by the reciprocal of this value to [DIF0] light source color".
			/// </remarks>
			[ParameterTableRowAttribute("invMulCol_Dif0", index: 5, minimum: 1, maximum: 10000, step: 1, order: 5, unknown2: 0)]
			[DisplayName("RGB reverse magnification [ %]")]
			[Description("I multiplied by the reciprocal of this value to [DIF0] light source color")]
			[DefaultValue((Int16)100)]
			public Int16 InvMulCol_Dif0 {
				get { return invMulCol_Dif0; }
				set {
					if ((double)value < 1 || (double)value > 10000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 1 to 10000 for InvMulCol_Dif0.");
					SetProperty(ref invMulCol_Dif0, ref value, InvMulCol_Dif0Property);
				}
			}

			/// <summary>Specular order</summary>
			/// <remarks>
			/// Japanese short name: "スペキュラ次数", Google translated: "Specular order".
			/// Japanese description: "[DIF0] スペキュラ次数", Google translated: "[DIF0] specular order".
			/// </remarks>
			[ParameterTableRowAttribute("sepcPow_Dif0", index: 6, minimum: 0, maximum: 100, step: 0.01, order: 6, unknown2: 0)]
			[DisplayName("Specular order")]
			[Description("[DIF0] specular order")]
			[DefaultValue((Single)1)]
			public Single SepcPow_Dif0 {
				get { return sepcPow_Dif0; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for SepcPow_Dif0.");
					SetProperty(ref sepcPow_Dif0, ref value, SepcPow_Dif0Property);
				}
			}

			/// <summary>Padding</summary>
			/// <remarks>
			/// Japanese short name: "パディング", Google translated: "Padding".
			/// Japanese description: "[DIF0]  ダミー", Google translated: "[DIF0] dummy".
			/// </remarks>
			[ParameterTableRowAttribute("pad_Dif0[8]", index: 7, minimum: 0, maximum: 0, step: 0, order: 96, unknown2: 0)]
			[DisplayName("Padding")]
			[Description("[DIF0] dummy")]
			public Byte[] Pad_Dif0 {
				get { return pad_Dif0; }
				set { SetProperty(ref pad_Dif0, ref value, Pad_Dif0Property); }
			}

			/// <summary>Resource name ID</summary>
			/// <remarks>
			/// Japanese short name: "リソース名ID", Google translated: "Resource name ID".
			/// Japanese description: "[SPC0] テクスチャファイル名のID(-1:なし)", Google translated: "ID of [SPC0] texture file name ( -1 : none)".
			/// </remarks>
			[ParameterTableRowAttribute("resNameId_Spc0", index: 8, minimum: -1, maximum: 999, step: 1, order: 7, unknown2: 4)]
			[DisplayName("Resource name ID")]
			[Description("ID of [SPC0] texture file name ( -1 : none)")]
			[DefaultValue((Int16)0)]
			public Int16 ResNameId_Spc0 {
				get { return resNameId_Spc0; }
				set {
					if ((double)value < -1 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999 for ResNameId_Spc0.");
					SetProperty(ref resNameId_Spc0, ref value, ResNameId_Spc0Property);
				}
			}

			/// <summary>RGB reverse magnification [ %]</summary>
			/// <remarks>
			/// Japanese short name: "RGB逆倍率[％]", Google translated: "RGB reverse magnification [ %]".
			/// Japanese description: "[SPC0] 光源色にこの値の逆数を掛けます", Google translated: "I multiplied by the reciprocal of this value to [SPC0] light source color".
			/// </remarks>
			[ParameterTableRowAttribute("invMulCol_Spc0", index: 9, minimum: 1, maximum: 10000, step: 1, order: 8, unknown2: 0)]
			[DisplayName("RGB reverse magnification [ %]")]
			[Description("I multiplied by the reciprocal of this value to [SPC0] light source color")]
			[DefaultValue((Int16)100)]
			public Int16 InvMulCol_Spc0 {
				get { return invMulCol_Spc0; }
				set {
					if ((double)value < 1 || (double)value > 10000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 1 to 10000 for InvMulCol_Spc0.");
					SetProperty(ref invMulCol_Spc0, ref value, InvMulCol_Spc0Property);
				}
			}

			/// <summary>Specular order</summary>
			/// <remarks>
			/// Japanese short name: "スペキュラ次数", Google translated: "Specular order".
			/// Japanese description: "[SPC0] スペキュラ次数", Google translated: "[SPC0] specular order".
			/// </remarks>
			[ParameterTableRowAttribute("sepcPow_Spc0", index: 10, minimum: 0, maximum: 100, step: 0.01, order: 9, unknown2: 0)]
			[DisplayName("Specular order")]
			[Description("[SPC0] specular order")]
			[DefaultValue((Single)1)]
			public Single SepcPow_Spc0 {
				get { return sepcPow_Spc0; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for SepcPow_Spc0.");
					SetProperty(ref sepcPow_Spc0, ref value, SepcPow_Spc0Property);
				}
			}

			/// <summary>Padding</summary>
			/// <remarks>
			/// Japanese short name: "パディング", Google translated: "Padding".
			/// Japanese description: "[SPC0]  ダミー", Google translated: "[SPC0] dummy".
			/// </remarks>
			[ParameterTableRowAttribute("pad_Spc0[8]", index: 11, minimum: 0, maximum: 0, step: 0, order: 97, unknown2: 0)]
			[DisplayName("Padding")]
			[Description("[SPC0] dummy")]
			public Byte[] Pad_Spc0 {
				get { return pad_Spc0; }
				set { SetProperty(ref pad_Spc0, ref value, Pad_Spc0Property); }
			}

			/// <summary>Resource name ID</summary>
			/// <remarks>
			/// Japanese short name: "リソース名ID", Google translated: "Resource name ID".
			/// Japanese description: "[SPC1] テクスチャファイル名のID(-1:なし)", Google translated: "ID of [SPC1] texture file name ( -1 : none)".
			/// </remarks>
			[ParameterTableRowAttribute("resNameId_Spc1", index: 12, minimum: -1, maximum: 999, step: 1, order: 10, unknown2: 4)]
			[DisplayName("Resource name ID")]
			[Description("ID of [SPC1] texture file name ( -1 : none)")]
			[DefaultValue((Int16)1)]
			public Int16 ResNameId_Spc1 {
				get { return resNameId_Spc1; }
				set {
					if ((double)value < -1 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999 for ResNameId_Spc1.");
					SetProperty(ref resNameId_Spc1, ref value, ResNameId_Spc1Property);
				}
			}

			/// <summary>RGB reverse magnification [ %]</summary>
			/// <remarks>
			/// Japanese short name: "RGB逆倍率[％]", Google translated: "RGB reverse magnification [ %]".
			/// Japanese description: "[SPC1] 光源色にこの値の逆数を掛けます", Google translated: "I multiplied by the reciprocal of this value to [SPC1] light source color".
			/// </remarks>
			[ParameterTableRowAttribute("invMulCol_Spc1", index: 13, minimum: 1, maximum: 10000, step: 1, order: 11, unknown2: 0)]
			[DisplayName("RGB reverse magnification [ %]")]
			[Description("I multiplied by the reciprocal of this value to [SPC1] light source color")]
			[DefaultValue((Int16)100)]
			public Int16 InvMulCol_Spc1 {
				get { return invMulCol_Spc1; }
				set {
					if ((double)value < 1 || (double)value > 10000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 1 to 10000 for InvMulCol_Spc1.");
					SetProperty(ref invMulCol_Spc1, ref value, InvMulCol_Spc1Property);
				}
			}

			/// <summary>Specular order</summary>
			/// <remarks>
			/// Japanese short name: "スペキュラ次数", Google translated: "Specular order".
			/// Japanese description: "[SPC1] スペキュラ次数", Google translated: "[SPC1] specular order".
			/// </remarks>
			[ParameterTableRowAttribute("sepcPow_Spc1", index: 14, minimum: 0, maximum: 100, step: 0.01, order: 12, unknown2: 0)]
			[DisplayName("Specular order")]
			[Description("[SPC1] specular order")]
			[DefaultValue((Single)1)]
			public Single SepcPow_Spc1 {
				get { return sepcPow_Spc1; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for SepcPow_Spc1.");
					SetProperty(ref sepcPow_Spc1, ref value, SepcPow_Spc1Property);
				}
			}

			/// <summary>Padding</summary>
			/// <remarks>
			/// Japanese short name: "パディング", Google translated: "Padding".
			/// Japanese description: "[SPC1]  ダミー", Google translated: "[SPC1] dummy".
			/// </remarks>
			[ParameterTableRowAttribute("pad_Spc1[8]", index: 15, minimum: 0, maximum: 0, step: 0, order: 98, unknown2: 0)]
			[DisplayName("Padding")]
			[Description("[SPC1] dummy")]
			public Byte[] Pad_Spc1 {
				get { return pad_Spc1; }
				set { SetProperty(ref pad_Spc1, ref value, Pad_Spc1Property); }
			}

			/// <summary>Resource name ID</summary>
			/// <remarks>
			/// Japanese short name: "リソース名ID", Google translated: "Resource name ID".
			/// Japanese description: "[SPC2] テクスチャファイル名のID(-1:なし)", Google translated: "ID of [SPC2] texture file name ( -1 : none)".
			/// </remarks>
			[ParameterTableRowAttribute("resNameId_Spc2", index: 16, minimum: -1, maximum: 999, step: 1, order: 13, unknown2: 4)]
			[DisplayName("Resource name ID")]
			[Description("ID of [SPC2] texture file name ( -1 : none)")]
			[DefaultValue((Int16)2)]
			public Int16 ResNameId_Spc2 {
				get { return resNameId_Spc2; }
				set {
					if ((double)value < -1 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999 for ResNameId_Spc2.");
					SetProperty(ref resNameId_Spc2, ref value, ResNameId_Spc2Property);
				}
			}

			/// <summary>RGB reverse magnification [ %]</summary>
			/// <remarks>
			/// Japanese short name: "RGB逆倍率[％]", Google translated: "RGB reverse magnification [ %]".
			/// Japanese description: "[SPC2] 光源色にこの値の逆数を掛けます", Google translated: "I multiplied by the reciprocal of this value to [SPC2] light source color".
			/// </remarks>
			[ParameterTableRowAttribute("invMulCol_Spc2", index: 17, minimum: 1, maximum: 10000, step: 1, order: 14, unknown2: 0)]
			[DisplayName("RGB reverse magnification [ %]")]
			[Description("I multiplied by the reciprocal of this value to [SPC2] light source color")]
			[DefaultValue((Int16)100)]
			public Int16 InvMulCol_Spc2 {
				get { return invMulCol_Spc2; }
				set {
					if ((double)value < 1 || (double)value > 10000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 1 to 10000 for InvMulCol_Spc2.");
					SetProperty(ref invMulCol_Spc2, ref value, InvMulCol_Spc2Property);
				}
			}

			/// <summary>Specular order</summary>
			/// <remarks>
			/// Japanese short name: "スペキュラ次数", Google translated: "Specular order".
			/// Japanese description: "[SPC2] スペキュラ次数", Google translated: "[SPC2] specular order".
			/// </remarks>
			[ParameterTableRowAttribute("sepcPow_Spc2", index: 18, minimum: 0, maximum: 100, step: 0.01, order: 15, unknown2: 0)]
			[DisplayName("Specular order")]
			[Description("[SPC2] specular order")]
			[DefaultValue((Single)1)]
			public Single SepcPow_Spc2 {
				get { return sepcPow_Spc2; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for SepcPow_Spc2.");
					SetProperty(ref sepcPow_Spc2, ref value, SepcPow_Spc2Property);
				}
			}

			/// <summary>Padding</summary>
			/// <remarks>
			/// Japanese short name: "パディング", Google translated: "Padding".
			/// Japanese description: "[SPC2]  ダミー", Google translated: "[SPC2] dummy".
			/// </remarks>
			[ParameterTableRowAttribute("pad_Spc2[8]", index: 19, minimum: 0, maximum: 0, step: 0, order: 99, unknown2: 0)]
			[DisplayName("Padding")]
			[Description("[SPC2] dummy")]
			public Byte[] Pad_Spc2 {
				get { return pad_Spc2; }
				set { SetProperty(ref pad_Spc2, ref value, Pad_Spc2Property); }
			}

			/// <summary>Resource name ID</summary>
			/// <remarks>
			/// Japanese short name: "リソース名ID", Google translated: "Resource name ID".
			/// Japanese description: "[SPC3] テクスチャファイル名のID(-1:なし)", Google translated: "ID of [SPC3] texture file name ( -1 : none)".
			/// </remarks>
			[ParameterTableRowAttribute("resNameId_Spc3", index: 20, minimum: -1, maximum: 999, step: 1, order: 16, unknown2: 4)]
			[DisplayName("Resource name ID")]
			[Description("ID of [SPC3] texture file name ( -1 : none)")]
			[DefaultValue((Int16)3)]
			public Int16 ResNameId_Spc3 {
				get { return resNameId_Spc3; }
				set {
					if ((double)value < -1 || (double)value > 999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999 for ResNameId_Spc3.");
					SetProperty(ref resNameId_Spc3, ref value, ResNameId_Spc3Property);
				}
			}

			/// <summary>RGB reverse magnification [ %]</summary>
			/// <remarks>
			/// Japanese short name: "RGB逆倍率[％]", Google translated: "RGB reverse magnification [ %]".
			/// Japanese description: "[SPC3] 光源色にこの値の逆数を掛けます", Google translated: "I multiplied by the reciprocal of this value in [SPC3] light source color".
			/// </remarks>
			[ParameterTableRowAttribute("invMulCol_Spc3", index: 21, minimum: 1, maximum: 10000, step: 1, order: 17, unknown2: 0)]
			[DisplayName("RGB reverse magnification [ %]")]
			[Description("I multiplied by the reciprocal of this value in [SPC3] light source color")]
			[DefaultValue((Int16)100)]
			public Int16 InvMulCol_Spc3 {
				get { return invMulCol_Spc3; }
				set {
					if ((double)value < 1 || (double)value > 10000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 1 to 10000 for InvMulCol_Spc3.");
					SetProperty(ref invMulCol_Spc3, ref value, InvMulCol_Spc3Property);
				}
			}

			/// <summary>Specular order</summary>
			/// <remarks>
			/// Japanese short name: "スペキュラ次数", Google translated: "Specular order".
			/// Japanese description: "[SPC3] スペキュラ次数", Google translated: "[SPC3] specular order".
			/// </remarks>
			[ParameterTableRowAttribute("sepcPow_Spc3", index: 22, minimum: 0, maximum: 100, step: 0.01, order: 18, unknown2: 0)]
			[DisplayName("Specular order")]
			[Description("[SPC3] specular order")]
			[DefaultValue((Single)1)]
			public Single SepcPow_Spc3 {
				get { return sepcPow_Spc3; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for SepcPow_Spc3.");
					SetProperty(ref sepcPow_Spc3, ref value, SepcPow_Spc3Property);
				}
			}

			/// <summary>Padding</summary>
			/// <remarks>
			/// Japanese short name: "パディング", Google translated: "Padding".
			/// Japanese description: "[SPC3]  ダミー", Google translated: "[SPC3] dummy".
			/// </remarks>
			[ParameterTableRowAttribute("pad_Spc3[8]", index: 23, minimum: 0, maximum: 0, step: 0, order: 100, unknown2: 0)]
			[DisplayName("Padding")]
			[Description("[SPC3] dummy")]
			public Byte[] Pad_Spc3 {
				get { return pad_Spc3; }
				set { SetProperty(ref pad_Spc3, ref value, Pad_Spc3Property); }
			}

			/// <summary>X angle</summary>
			/// <remarks>
			/// Japanese short name: "X角度", Google translated: "X angle".
			/// Japanese description: "平行光源：００", Google translated: "Collimated light source : 00".
			/// </remarks>
			[ParameterTableRowAttribute("degRotX_00", index: 24, minimum: -90, maximum: 90, step: 1, order: 19, unknown2: 0)]
			[DisplayName("X angle")]
			[Description("Collimated light source : 00")]
			[DefaultValue((Int16)0)]
			public Int16 DegRotX_00 {
				get { return degRotX_00; }
				set {
					if ((double)value < -90 || (double)value > 90)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -90 to 90 for DegRotX_00.");
					SetProperty(ref degRotX_00, ref value, DegRotX_00Property);
				}
			}

			/// <summary>Y angle</summary>
			/// <remarks>
			/// Japanese short name: "Ｙ角度", Google translated: "Y angle".
			/// Japanese description: "平行光源：００", Google translated: "Collimated light source : 00".
			/// </remarks>
			[ParameterTableRowAttribute("degRotY_00", index: 25, minimum: -180, maximum: 180, step: 1, order: 20, unknown2: 1)]
			[DisplayName("Y angle")]
			[Description("Collimated light source : 00")]
			[DefaultValue((Int16)0)]
			public Int16 DegRotY_00 {
				get { return degRotY_00; }
				set {
					if ((double)value < -180 || (double)value > 180)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -180 to 180 for DegRotY_00.");
					SetProperty(ref degRotY_00, ref value, DegRotY_00Property);
				}
			}

			/// <summary>R</summary>
			/// <remarks>
			/// Japanese short name: "Ｒ", Google translated: "R".
			/// Japanese description: "平行光源：００", Google translated: "Collimated light source : 00".
			/// </remarks>
			[ParameterTableRowAttribute("colR_00", index: 26, minimum: 0, maximum: 255, step: 1, order: 21, unknown2: 0)]
			[DisplayName("R")]
			[Description("Collimated light source : 00")]
			[DefaultValue((Int16)255)]
			public Int16 ColR_00 {
				get { return colR_00; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColR_00.");
					SetProperty(ref colR_00, ref value, ColR_00Property);
				}
			}

			/// <summary>G</summary>
			/// <remarks>
			/// Japanese short name: "Ｇ", Google translated: "G".
			/// Japanese description: "平行光源：００", Google translated: "Collimated light source : 00".
			/// </remarks>
			[ParameterTableRowAttribute("colG_00", index: 27, minimum: 0, maximum: 255, step: 1, order: 22, unknown2: 0)]
			[DisplayName("G")]
			[Description("Collimated light source : 00")]
			[DefaultValue((Int16)255)]
			public Int16 ColG_00 {
				get { return colG_00; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColG_00.");
					SetProperty(ref colG_00, ref value, ColG_00Property);
				}
			}

			/// <summary>B</summary>
			/// <remarks>
			/// Japanese short name: "Ｂ", Google translated: "B".
			/// Japanese description: "平行光源：００", Google translated: "Collimated light source : 00".
			/// </remarks>
			[ParameterTableRowAttribute("colB_00", index: 28, minimum: 0, maximum: 255, step: 1, order: 23, unknown2: 0)]
			[DisplayName("B")]
			[Description("Collimated light source : 00")]
			[DefaultValue((Int16)255)]
			public Int16 ColB_00 {
				get { return colB_00; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColB_00.");
					SetProperty(ref colB_00, ref value, ColB_00Property);
				}
			}

			/// <summary>RGB magnification [ %]</summary>
			/// <remarks>
			/// Japanese short name: "RGB倍率[％]", Google translated: "RGB magnification [ %]".
			/// Japanese description: "平行光源：００ (0:未使用)", Google translated: "Collimated light source : 00 ( 0 : not used)".
			/// </remarks>
			[ParameterTableRowAttribute("colA_00", index: 29, minimum: 0, maximum: 10000, step: 1, order: 24, unknown2: 0)]
			[DisplayName("RGB magnification [ %]")]
			[Description("Collimated light source : 00 ( 0 : not used)")]
			[DefaultValue((Int16)100)]
			public Int16 ColA_00 {
				get { return colA_00; }
				set {
					if ((double)value < 0 || (double)value > 10000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 10000 for ColA_00.");
					SetProperty(ref colA_00, ref value, ColA_00Property);
				}
			}

			/// <summary>Padding</summary>
			/// <remarks>
			/// Japanese short name: "パディング", Google translated: "Padding".
			/// Japanese description: "平行光源：００", Google translated: "Collimated light source : 00".
			/// </remarks>
			[ParameterTableRowAttribute("pad_00[4]", index: 30, minimum: 0, maximum: 0, step: 0, order: 101, unknown2: 0)]
			[DisplayName("Padding")]
			[Description("Collimated light source : 00")]
			public Byte[] Pad_00 {
				get { return pad_00; }
				set { SetProperty(ref pad_00, ref value, Pad_00Property); }
			}

			/// <summary>X angle</summary>
			/// <remarks>
			/// Japanese short name: "X角度", Google translated: "X angle".
			/// Japanese description: "平行光源：０１", Google translated: "Collimated light source : 01".
			/// </remarks>
			[ParameterTableRowAttribute("degRotX_01", index: 31, minimum: -90, maximum: 90, step: 1, order: 25, unknown2: 0)]
			[DisplayName("X angle")]
			[Description("Collimated light source : 01")]
			[DefaultValue((Int16)0)]
			public Int16 DegRotX_01 {
				get { return degRotX_01; }
				set {
					if ((double)value < -90 || (double)value > 90)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -90 to 90 for DegRotX_01.");
					SetProperty(ref degRotX_01, ref value, DegRotX_01Property);
				}
			}

			/// <summary>Y angle</summary>
			/// <remarks>
			/// Japanese short name: "Ｙ角度", Google translated: "Y angle".
			/// Japanese description: "平行光源：０１", Google translated: "Collimated light source : 01".
			/// </remarks>
			[ParameterTableRowAttribute("degRotY_01", index: 32, minimum: -180, maximum: 180, step: 1, order: 26, unknown2: 1)]
			[DisplayName("Y angle")]
			[Description("Collimated light source : 01")]
			[DefaultValue((Int16)0)]
			public Int16 DegRotY_01 {
				get { return degRotY_01; }
				set {
					if ((double)value < -180 || (double)value > 180)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -180 to 180 for DegRotY_01.");
					SetProperty(ref degRotY_01, ref value, DegRotY_01Property);
				}
			}

			/// <summary>R</summary>
			/// <remarks>
			/// Japanese short name: "Ｒ", Google translated: "R".
			/// Japanese description: "平行光源：０１", Google translated: "Collimated light source : 01".
			/// </remarks>
			[ParameterTableRowAttribute("colR_01", index: 33, minimum: 0, maximum: 255, step: 1, order: 27, unknown2: 0)]
			[DisplayName("R")]
			[Description("Collimated light source : 01")]
			[DefaultValue((Int16)255)]
			public Int16 ColR_01 {
				get { return colR_01; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColR_01.");
					SetProperty(ref colR_01, ref value, ColR_01Property);
				}
			}

			/// <summary>G</summary>
			/// <remarks>
			/// Japanese short name: "Ｇ", Google translated: "G".
			/// Japanese description: "平行光源：０１", Google translated: "Collimated light source : 01".
			/// </remarks>
			[ParameterTableRowAttribute("colG_01", index: 34, minimum: 0, maximum: 255, step: 1, order: 28, unknown2: 0)]
			[DisplayName("G")]
			[Description("Collimated light source : 01")]
			[DefaultValue((Int16)255)]
			public Int16 ColG_01 {
				get { return colG_01; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColG_01.");
					SetProperty(ref colG_01, ref value, ColG_01Property);
				}
			}

			/// <summary>B</summary>
			/// <remarks>
			/// Japanese short name: "Ｂ", Google translated: "B".
			/// Japanese description: "平行光源：０１", Google translated: "Collimated light source : 01".
			/// </remarks>
			[ParameterTableRowAttribute("colB_01", index: 35, minimum: 0, maximum: 255, step: 1, order: 29, unknown2: 0)]
			[DisplayName("B")]
			[Description("Collimated light source : 01")]
			[DefaultValue((Int16)255)]
			public Int16 ColB_01 {
				get { return colB_01; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColB_01.");
					SetProperty(ref colB_01, ref value, ColB_01Property);
				}
			}

			/// <summary>RGB magnification [ %]</summary>
			/// <remarks>
			/// Japanese short name: "RGB倍率[％]", Google translated: "RGB magnification [ %]".
			/// Japanese description: "平行光源：０１ (0:未使用)", Google translated: "Collimated light source : 01 ( 0 : not used)".
			/// </remarks>
			[ParameterTableRowAttribute("colA_01", index: 36, minimum: 0, maximum: 10000, step: 1, order: 30, unknown2: 0)]
			[DisplayName("RGB magnification [ %]")]
			[Description("Collimated light source : 01 ( 0 : not used)")]
			[DefaultValue((Int16)0)]
			public Int16 ColA_01 {
				get { return colA_01; }
				set {
					if ((double)value < 0 || (double)value > 10000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 10000 for ColA_01.");
					SetProperty(ref colA_01, ref value, ColA_01Property);
				}
			}

			/// <summary>Padding</summary>
			/// <remarks>
			/// Japanese short name: "パディング", Google translated: "Padding".
			/// Japanese description: "平行光源：０１", Google translated: "Collimated light source : 01".
			/// </remarks>
			[ParameterTableRowAttribute("pad_01[4]", index: 37, minimum: 0, maximum: 0, step: 0, order: 102, unknown2: 0)]
			[DisplayName("Padding")]
			[Description("Collimated light source : 01")]
			public Byte[] Pad_01 {
				get { return pad_01; }
				set { SetProperty(ref pad_01, ref value, Pad_01Property); }
			}

			/// <summary>X angle</summary>
			/// <remarks>
			/// Japanese short name: "X角度", Google translated: "X angle".
			/// Japanese description: "平行光源：０２", Google translated: "Collimated light source : 02".
			/// </remarks>
			[ParameterTableRowAttribute("degRotX_02", index: 38, minimum: -90, maximum: 90, step: 1, order: 31, unknown2: 0)]
			[DisplayName("X angle")]
			[Description("Collimated light source : 02")]
			[DefaultValue((Int16)0)]
			public Int16 DegRotX_02 {
				get { return degRotX_02; }
				set {
					if ((double)value < -90 || (double)value > 90)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -90 to 90 for DegRotX_02.");
					SetProperty(ref degRotX_02, ref value, DegRotX_02Property);
				}
			}

			/// <summary>Y angle</summary>
			/// <remarks>
			/// Japanese short name: "Ｙ角度", Google translated: "Y angle".
			/// Japanese description: "平行光源：０２", Google translated: "Collimated light source : 02".
			/// </remarks>
			[ParameterTableRowAttribute("degRotY_02", index: 39, minimum: -180, maximum: 180, step: 1, order: 32, unknown2: 1)]
			[DisplayName("Y angle")]
			[Description("Collimated light source : 02")]
			[DefaultValue((Int16)0)]
			public Int16 DegRotY_02 {
				get { return degRotY_02; }
				set {
					if ((double)value < -180 || (double)value > 180)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -180 to 180 for DegRotY_02.");
					SetProperty(ref degRotY_02, ref value, DegRotY_02Property);
				}
			}

			/// <summary>R</summary>
			/// <remarks>
			/// Japanese short name: "Ｒ", Google translated: "R".
			/// Japanese description: "平行光源：０２", Google translated: "Collimated light source : 02".
			/// </remarks>
			[ParameterTableRowAttribute("colR_02", index: 40, minimum: 0, maximum: 255, step: 1, order: 33, unknown2: 0)]
			[DisplayName("R")]
			[Description("Collimated light source : 02")]
			[DefaultValue((Int16)255)]
			public Int16 ColR_02 {
				get { return colR_02; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColR_02.");
					SetProperty(ref colR_02, ref value, ColR_02Property);
				}
			}

			/// <summary>G</summary>
			/// <remarks>
			/// Japanese short name: "Ｇ", Google translated: "G".
			/// Japanese description: "平行光源：０２", Google translated: "Collimated light source : 02".
			/// </remarks>
			[ParameterTableRowAttribute("colG_02", index: 41, minimum: 0, maximum: 255, step: 1, order: 34, unknown2: 0)]
			[DisplayName("G")]
			[Description("Collimated light source : 02")]
			[DefaultValue((Int16)255)]
			public Int16 ColG_02 {
				get { return colG_02; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColG_02.");
					SetProperty(ref colG_02, ref value, ColG_02Property);
				}
			}

			/// <summary>B</summary>
			/// <remarks>
			/// Japanese short name: "Ｂ", Google translated: "B".
			/// Japanese description: "平行光源：０２", Google translated: "Collimated light source : 02".
			/// </remarks>
			[ParameterTableRowAttribute("colB_02", index: 42, minimum: 0, maximum: 255, step: 1, order: 35, unknown2: 0)]
			[DisplayName("B")]
			[Description("Collimated light source : 02")]
			[DefaultValue((Int16)255)]
			public Int16 ColB_02 {
				get { return colB_02; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColB_02.");
					SetProperty(ref colB_02, ref value, ColB_02Property);
				}
			}

			/// <summary>RGB magnification [ %]</summary>
			/// <remarks>
			/// Japanese short name: "RGB倍率[％]", Google translated: "RGB magnification [ %]".
			/// Japanese description: "平行光源：０２ (0:未使用)", Google translated: "Collimated light source : 02 ( 0 : not used)".
			/// </remarks>
			[ParameterTableRowAttribute("colA_02", index: 43, minimum: 0, maximum: 10000, step: 1, order: 36, unknown2: 0)]
			[DisplayName("RGB magnification [ %]")]
			[Description("Collimated light source : 02 ( 0 : not used)")]
			[DefaultValue((Int16)0)]
			public Int16 ColA_02 {
				get { return colA_02; }
				set {
					if ((double)value < 0 || (double)value > 10000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 10000 for ColA_02.");
					SetProperty(ref colA_02, ref value, ColA_02Property);
				}
			}

			/// <summary>Padding</summary>
			/// <remarks>
			/// Japanese short name: "パディング", Google translated: "Padding".
			/// Japanese description: "平行光源：０２", Google translated: "Collimated light source : 02".
			/// </remarks>
			[ParameterTableRowAttribute("pad_02[4]", index: 44, minimum: 0, maximum: 0, step: 0, order: 103, unknown2: 0)]
			[DisplayName("Padding")]
			[Description("Collimated light source : 02")]
			public Byte[] Pad_02 {
				get { return pad_02; }
				set { SetProperty(ref pad_02, ref value, Pad_02Property); }
			}

			/// <summary>X angle</summary>
			/// <remarks>
			/// Japanese short name: "X角度", Google translated: "X angle".
			/// Japanese description: "平行光源：０３", Google translated: "Collimated light source : 03".
			/// </remarks>
			[ParameterTableRowAttribute("degRotX_03", index: 45, minimum: -90, maximum: 90, step: 1, order: 37, unknown2: 0)]
			[DisplayName("X angle")]
			[Description("Collimated light source : 03")]
			[DefaultValue((Int16)0)]
			public Int16 DegRotX_03 {
				get { return degRotX_03; }
				set {
					if ((double)value < -90 || (double)value > 90)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -90 to 90 for DegRotX_03.");
					SetProperty(ref degRotX_03, ref value, DegRotX_03Property);
				}
			}

			/// <summary>Y angle</summary>
			/// <remarks>
			/// Japanese short name: "Ｙ角度", Google translated: "Y angle".
			/// Japanese description: "平行光源：０３", Google translated: "Collimated light source : 03".
			/// </remarks>
			[ParameterTableRowAttribute("degRotY_03", index: 46, minimum: -180, maximum: 180, step: 1, order: 38, unknown2: 1)]
			[DisplayName("Y angle")]
			[Description("Collimated light source : 03")]
			[DefaultValue((Int16)0)]
			public Int16 DegRotY_03 {
				get { return degRotY_03; }
				set {
					if ((double)value < -180 || (double)value > 180)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -180 to 180 for DegRotY_03.");
					SetProperty(ref degRotY_03, ref value, DegRotY_03Property);
				}
			}

			/// <summary>R</summary>
			/// <remarks>
			/// Japanese short name: "Ｒ", Google translated: "R".
			/// Japanese description: "平行光源：０３", Google translated: "Collimated light source : 03".
			/// </remarks>
			[ParameterTableRowAttribute("colR_03", index: 47, minimum: 0, maximum: 255, step: 1, order: 39, unknown2: 0)]
			[DisplayName("R")]
			[Description("Collimated light source : 03")]
			[DefaultValue((Int16)255)]
			public Int16 ColR_03 {
				get { return colR_03; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColR_03.");
					SetProperty(ref colR_03, ref value, ColR_03Property);
				}
			}

			/// <summary>G</summary>
			/// <remarks>
			/// Japanese short name: "Ｇ", Google translated: "G".
			/// Japanese description: "平行光源：０３", Google translated: "Collimated light source : 03".
			/// </remarks>
			[ParameterTableRowAttribute("colG_03", index: 48, minimum: 0, maximum: 255, step: 1, order: 40, unknown2: 0)]
			[DisplayName("G")]
			[Description("Collimated light source : 03")]
			[DefaultValue((Int16)255)]
			public Int16 ColG_03 {
				get { return colG_03; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColG_03.");
					SetProperty(ref colG_03, ref value, ColG_03Property);
				}
			}

			/// <summary>B</summary>
			/// <remarks>
			/// Japanese short name: "Ｂ", Google translated: "B".
			/// Japanese description: "平行光源：０３", Google translated: "Collimated light source : 03".
			/// </remarks>
			[ParameterTableRowAttribute("colB_03", index: 49, minimum: 0, maximum: 255, step: 1, order: 41, unknown2: 0)]
			[DisplayName("B")]
			[Description("Collimated light source : 03")]
			[DefaultValue((Int16)255)]
			public Int16 ColB_03 {
				get { return colB_03; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColB_03.");
					SetProperty(ref colB_03, ref value, ColB_03Property);
				}
			}

			/// <summary>RGB magnification [ %]</summary>
			/// <remarks>
			/// Japanese short name: "RGB倍率[％]", Google translated: "RGB magnification [ %]".
			/// Japanese description: "平行光源：０３ (0:未使用)", Google translated: "Collimated light source : 03 ( 0 : not used)".
			/// </remarks>
			[ParameterTableRowAttribute("colA_03", index: 50, minimum: 0, maximum: 10000, step: 1, order: 42, unknown2: 0)]
			[DisplayName("RGB magnification [ %]")]
			[Description("Collimated light source : 03 ( 0 : not used)")]
			[DefaultValue((Int16)0)]
			public Int16 ColA_03 {
				get { return colA_03; }
				set {
					if ((double)value < 0 || (double)value > 10000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 10000 for ColA_03.");
					SetProperty(ref colA_03, ref value, ColA_03Property);
				}
			}

			/// <summary>Padding</summary>
			/// <remarks>
			/// Japanese short name: "パディング", Google translated: "Padding".
			/// Japanese description: "平行光源：０３", Google translated: "Collimated light source : 03".
			/// </remarks>
			[ParameterTableRowAttribute("pad_03[4]", index: 51, minimum: 0, maximum: 0, step: 0, order: 104, unknown2: 0)]
			[DisplayName("Padding")]
			[Description("Collimated light source : 03")]
			public Byte[] Pad_03 {
				get { return pad_03; }
				set { SetProperty(ref pad_03, ref value, Pad_03Property); }
			}

			/// <summary>X angle</summary>
			/// <remarks>
			/// Japanese short name: "X角度", Google translated: "X angle".
			/// Japanese description: "平行光源：０４", Google translated: "Collimated light source : 04".
			/// </remarks>
			[ParameterTableRowAttribute("degRotX_04", index: 52, minimum: -90, maximum: 90, step: 1, order: 43, unknown2: 0)]
			[DisplayName("X angle")]
			[Description("Collimated light source : 04")]
			[DefaultValue((Int16)0)]
			public Int16 DegRotX_04 {
				get { return degRotX_04; }
				set {
					if ((double)value < -90 || (double)value > 90)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -90 to 90 for DegRotX_04.");
					SetProperty(ref degRotX_04, ref value, DegRotX_04Property);
				}
			}

			/// <summary>Y angle</summary>
			/// <remarks>
			/// Japanese short name: "Ｙ角度", Google translated: "Y angle".
			/// Japanese description: "平行光源：０４", Google translated: "Collimated light source : 04".
			/// </remarks>
			[ParameterTableRowAttribute("degRotY_04", index: 53, minimum: -180, maximum: 180, step: 1, order: 44, unknown2: 1)]
			[DisplayName("Y angle")]
			[Description("Collimated light source : 04")]
			[DefaultValue((Int16)0)]
			public Int16 DegRotY_04 {
				get { return degRotY_04; }
				set {
					if ((double)value < -180 || (double)value > 180)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -180 to 180 for DegRotY_04.");
					SetProperty(ref degRotY_04, ref value, DegRotY_04Property);
				}
			}

			/// <summary>R</summary>
			/// <remarks>
			/// Japanese short name: "Ｒ", Google translated: "R".
			/// Japanese description: "平行光源：０４", Google translated: "Collimated light source : 04".
			/// </remarks>
			[ParameterTableRowAttribute("colR_04", index: 54, minimum: 0, maximum: 255, step: 1, order: 45, unknown2: 0)]
			[DisplayName("R")]
			[Description("Collimated light source : 04")]
			[DefaultValue((Int16)255)]
			public Int16 ColR_04 {
				get { return colR_04; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColR_04.");
					SetProperty(ref colR_04, ref value, ColR_04Property);
				}
			}

			/// <summary>G</summary>
			/// <remarks>
			/// Japanese short name: "Ｇ", Google translated: "G".
			/// Japanese description: "平行光源：０４", Google translated: "Collimated light source : 04".
			/// </remarks>
			[ParameterTableRowAttribute("colG_04", index: 55, minimum: 0, maximum: 255, step: 1, order: 46, unknown2: 0)]
			[DisplayName("G")]
			[Description("Collimated light source : 04")]
			[DefaultValue((Int16)255)]
			public Int16 ColG_04 {
				get { return colG_04; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColG_04.");
					SetProperty(ref colG_04, ref value, ColG_04Property);
				}
			}

			/// <summary>B</summary>
			/// <remarks>
			/// Japanese short name: "Ｂ", Google translated: "B".
			/// Japanese description: "平行光源：０４", Google translated: "Collimated light source : 04".
			/// </remarks>
			[ParameterTableRowAttribute("colB_04", index: 56, minimum: 0, maximum: 255, step: 1, order: 47, unknown2: 0)]
			[DisplayName("B")]
			[Description("Collimated light source : 04")]
			[DefaultValue((Int16)255)]
			public Int16 ColB_04 {
				get { return colB_04; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColB_04.");
					SetProperty(ref colB_04, ref value, ColB_04Property);
				}
			}

			/// <summary>RGB magnification [ %]</summary>
			/// <remarks>
			/// Japanese short name: "RGB倍率[％]", Google translated: "RGB magnification [ %]".
			/// Japanese description: "平行光源：０４ (0:未使用)", Google translated: "Collimated light source : 04 ( 0 : not used)".
			/// </remarks>
			[ParameterTableRowAttribute("colA_04", index: 57, minimum: 0, maximum: 10000, step: 1, order: 48, unknown2: 0)]
			[DisplayName("RGB magnification [ %]")]
			[Description("Collimated light source : 04 ( 0 : not used)")]
			[DefaultValue((Int16)0)]
			public Int16 ColA_04 {
				get { return colA_04; }
				set {
					if ((double)value < 0 || (double)value > 10000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 10000 for ColA_04.");
					SetProperty(ref colA_04, ref value, ColA_04Property);
				}
			}

			/// <summary>Padding</summary>
			/// <remarks>
			/// Japanese short name: "パディング", Google translated: "Padding".
			/// Japanese description: "平行光源：０４", Google translated: "Collimated light source : 04".
			/// </remarks>
			[ParameterTableRowAttribute("pad_04[4]", index: 58, minimum: 0, maximum: 0, step: 0, order: 105, unknown2: 0)]
			[DisplayName("Padding")]
			[Description("Collimated light source : 04")]
			public Byte[] Pad_04 {
				get { return pad_04; }
				set { SetProperty(ref pad_04, ref value, Pad_04Property); }
			}

			/// <summary>X angle</summary>
			/// <remarks>
			/// Japanese short name: "X角度", Google translated: "X angle".
			/// Japanese description: "平行光源：０５", Google translated: "Collimated light source : 05".
			/// </remarks>
			[ParameterTableRowAttribute("degRotX_05", index: 59, minimum: -90, maximum: 90, step: 1, order: 49, unknown2: 0)]
			[DisplayName("X angle")]
			[Description("Collimated light source : 05")]
			[DefaultValue((Int16)0)]
			public Int16 DegRotX_05 {
				get { return degRotX_05; }
				set {
					if ((double)value < -90 || (double)value > 90)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -90 to 90 for DegRotX_05.");
					SetProperty(ref degRotX_05, ref value, DegRotX_05Property);
				}
			}

			/// <summary>Y angle</summary>
			/// <remarks>
			/// Japanese short name: "Ｙ角度", Google translated: "Y angle".
			/// Japanese description: "平行光源：０５", Google translated: "Collimated light source : 05".
			/// </remarks>
			[ParameterTableRowAttribute("degRotY_05", index: 60, minimum: -180, maximum: 180, step: 1, order: 50, unknown2: 1)]
			[DisplayName("Y angle")]
			[Description("Collimated light source : 05")]
			[DefaultValue((Int16)0)]
			public Int16 DegRotY_05 {
				get { return degRotY_05; }
				set {
					if ((double)value < -180 || (double)value > 180)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -180 to 180 for DegRotY_05.");
					SetProperty(ref degRotY_05, ref value, DegRotY_05Property);
				}
			}

			/// <summary>R</summary>
			/// <remarks>
			/// Japanese short name: "Ｒ", Google translated: "R".
			/// Japanese description: "平行光源：０５", Google translated: "Collimated light source : 05".
			/// </remarks>
			[ParameterTableRowAttribute("colR_05", index: 61, minimum: 0, maximum: 255, step: 1, order: 51, unknown2: 0)]
			[DisplayName("R")]
			[Description("Collimated light source : 05")]
			[DefaultValue((Int16)255)]
			public Int16 ColR_05 {
				get { return colR_05; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColR_05.");
					SetProperty(ref colR_05, ref value, ColR_05Property);
				}
			}

			/// <summary>G</summary>
			/// <remarks>
			/// Japanese short name: "Ｇ", Google translated: "G".
			/// Japanese description: "平行光源：０５", Google translated: "Collimated light source : 05".
			/// </remarks>
			[ParameterTableRowAttribute("colG_05", index: 62, minimum: 0, maximum: 255, step: 1, order: 52, unknown2: 0)]
			[DisplayName("G")]
			[Description("Collimated light source : 05")]
			[DefaultValue((Int16)255)]
			public Int16 ColG_05 {
				get { return colG_05; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColG_05.");
					SetProperty(ref colG_05, ref value, ColG_05Property);
				}
			}

			/// <summary>B</summary>
			/// <remarks>
			/// Japanese short name: "Ｂ", Google translated: "B".
			/// Japanese description: "平行光源：０５", Google translated: "Collimated light source : 05".
			/// </remarks>
			[ParameterTableRowAttribute("colB_05", index: 63, minimum: 0, maximum: 255, step: 1, order: 53, unknown2: 0)]
			[DisplayName("B")]
			[Description("Collimated light source : 05")]
			[DefaultValue((Int16)255)]
			public Int16 ColB_05 {
				get { return colB_05; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColB_05.");
					SetProperty(ref colB_05, ref value, ColB_05Property);
				}
			}

			/// <summary>RGB magnification [ %]</summary>
			/// <remarks>
			/// Japanese short name: "RGB倍率[％]", Google translated: "RGB magnification [ %]".
			/// Japanese description: "平行光源：０５ (0:未使用)", Google translated: "Collimated light source : 05 ( 0 : not used)".
			/// </remarks>
			[ParameterTableRowAttribute("colA_05", index: 64, minimum: 0, maximum: 10000, step: 1, order: 54, unknown2: 0)]
			[DisplayName("RGB magnification [ %]")]
			[Description("Collimated light source : 05 ( 0 : not used)")]
			[DefaultValue((Int16)0)]
			public Int16 ColA_05 {
				get { return colA_05; }
				set {
					if ((double)value < 0 || (double)value > 10000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 10000 for ColA_05.");
					SetProperty(ref colA_05, ref value, ColA_05Property);
				}
			}

			/// <summary>Padding</summary>
			/// <remarks>
			/// Japanese short name: "パディング", Google translated: "Padding".
			/// Japanese description: "平行光源：０５", Google translated: "Collimated light source : 05".
			/// </remarks>
			[ParameterTableRowAttribute("pad_05[4]", index: 65, minimum: 0, maximum: 0, step: 0, order: 106, unknown2: 0)]
			[DisplayName("Padding")]
			[Description("Collimated light source : 05")]
			public Byte[] Pad_05 {
				get { return pad_05; }
				set { SetProperty(ref pad_05, ref value, Pad_05Property); }
			}

			/// <summary>X angle</summary>
			/// <remarks>
			/// Japanese short name: "X角度", Google translated: "X angle".
			/// Japanese description: "平行光源：０６", Google translated: "Collimated light source : 06".
			/// </remarks>
			[ParameterTableRowAttribute("degRotX_06", index: 66, minimum: -90, maximum: 90, step: 1, order: 55, unknown2: 0)]
			[DisplayName("X angle")]
			[Description("Collimated light source : 06")]
			[DefaultValue((Int16)0)]
			public Int16 DegRotX_06 {
				get { return degRotX_06; }
				set {
					if ((double)value < -90 || (double)value > 90)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -90 to 90 for DegRotX_06.");
					SetProperty(ref degRotX_06, ref value, DegRotX_06Property);
				}
			}

			/// <summary>Y angle</summary>
			/// <remarks>
			/// Japanese short name: "Ｙ角度", Google translated: "Y angle".
			/// Japanese description: "平行光源：０６", Google translated: "Collimated light source : 06".
			/// </remarks>
			[ParameterTableRowAttribute("degRotY_06", index: 67, minimum: -180, maximum: 180, step: 1, order: 56, unknown2: 1)]
			[DisplayName("Y angle")]
			[Description("Collimated light source : 06")]
			[DefaultValue((Int16)0)]
			public Int16 DegRotY_06 {
				get { return degRotY_06; }
				set {
					if ((double)value < -180 || (double)value > 180)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -180 to 180 for DegRotY_06.");
					SetProperty(ref degRotY_06, ref value, DegRotY_06Property);
				}
			}

			/// <summary>R</summary>
			/// <remarks>
			/// Japanese short name: "Ｒ", Google translated: "R".
			/// Japanese description: "平行光源：０６", Google translated: "Collimated light source : 06".
			/// </remarks>
			[ParameterTableRowAttribute("colR_06", index: 68, minimum: 0, maximum: 255, step: 1, order: 57, unknown2: 0)]
			[DisplayName("R")]
			[Description("Collimated light source : 06")]
			[DefaultValue((Int16)255)]
			public Int16 ColR_06 {
				get { return colR_06; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColR_06.");
					SetProperty(ref colR_06, ref value, ColR_06Property);
				}
			}

			/// <summary>G</summary>
			/// <remarks>
			/// Japanese short name: "Ｇ", Google translated: "G".
			/// Japanese description: "平行光源：０６", Google translated: "Collimated light source : 06".
			/// </remarks>
			[ParameterTableRowAttribute("colG_06", index: 69, minimum: 0, maximum: 255, step: 1, order: 58, unknown2: 0)]
			[DisplayName("G")]
			[Description("Collimated light source : 06")]
			[DefaultValue((Int16)255)]
			public Int16 ColG_06 {
				get { return colG_06; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColG_06.");
					SetProperty(ref colG_06, ref value, ColG_06Property);
				}
			}

			/// <summary>B</summary>
			/// <remarks>
			/// Japanese short name: "Ｂ", Google translated: "B".
			/// Japanese description: "平行光源：０６", Google translated: "Collimated light source : 06".
			/// </remarks>
			[ParameterTableRowAttribute("colB_06", index: 70, minimum: 0, maximum: 255, step: 1, order: 59, unknown2: 0)]
			[DisplayName("B")]
			[Description("Collimated light source : 06")]
			[DefaultValue((Int16)255)]
			public Int16 ColB_06 {
				get { return colB_06; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColB_06.");
					SetProperty(ref colB_06, ref value, ColB_06Property);
				}
			}

			/// <summary>RGB magnification [ %]</summary>
			/// <remarks>
			/// Japanese short name: "RGB倍率[％]", Google translated: "RGB magnification [ %]".
			/// Japanese description: "平行光源：０６ (0:未使用)", Google translated: "Collimated light source : 06 ( 0 : not used)".
			/// </remarks>
			[ParameterTableRowAttribute("colA_06", index: 71, minimum: 0, maximum: 10000, step: 1, order: 60, unknown2: 0)]
			[DisplayName("RGB magnification [ %]")]
			[Description("Collimated light source : 06 ( 0 : not used)")]
			[DefaultValue((Int16)0)]
			public Int16 ColA_06 {
				get { return colA_06; }
				set {
					if ((double)value < 0 || (double)value > 10000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 10000 for ColA_06.");
					SetProperty(ref colA_06, ref value, ColA_06Property);
				}
			}

			/// <summary>Padding</summary>
			/// <remarks>
			/// Japanese short name: "パディング", Google translated: "Padding".
			/// Japanese description: "平行光源：０６", Google translated: "Collimated light source : 06".
			/// </remarks>
			[ParameterTableRowAttribute("pad_06[4]", index: 72, minimum: 0, maximum: 0, step: 0, order: 107, unknown2: 0)]
			[DisplayName("Padding")]
			[Description("Collimated light source : 06")]
			public Byte[] Pad_06 {
				get { return pad_06; }
				set { SetProperty(ref pad_06, ref value, Pad_06Property); }
			}

			/// <summary>X angle</summary>
			/// <remarks>
			/// Japanese short name: "X角度", Google translated: "X angle".
			/// Japanese description: "平行光源：０７", Google translated: "Collimated light source : 07".
			/// </remarks>
			[ParameterTableRowAttribute("degRotX_07", index: 73, minimum: -90, maximum: 90, step: 1, order: 61, unknown2: 0)]
			[DisplayName("X angle")]
			[Description("Collimated light source : 07")]
			[DefaultValue((Int16)0)]
			public Int16 DegRotX_07 {
				get { return degRotX_07; }
				set {
					if ((double)value < -90 || (double)value > 90)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -90 to 90 for DegRotX_07.");
					SetProperty(ref degRotX_07, ref value, DegRotX_07Property);
				}
			}

			/// <summary>Y angle</summary>
			/// <remarks>
			/// Japanese short name: "Ｙ角度", Google translated: "Y angle".
			/// Japanese description: "平行光源：０７", Google translated: "Collimated light source : 07".
			/// </remarks>
			[ParameterTableRowAttribute("degRotY_07", index: 74, minimum: -180, maximum: 180, step: 1, order: 62, unknown2: 1)]
			[DisplayName("Y angle")]
			[Description("Collimated light source : 07")]
			[DefaultValue((Int16)0)]
			public Int16 DegRotY_07 {
				get { return degRotY_07; }
				set {
					if ((double)value < -180 || (double)value > 180)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -180 to 180 for DegRotY_07.");
					SetProperty(ref degRotY_07, ref value, DegRotY_07Property);
				}
			}

			/// <summary>R</summary>
			/// <remarks>
			/// Japanese short name: "Ｒ", Google translated: "R".
			/// Japanese description: "平行光源：０７", Google translated: "Collimated light source : 07".
			/// </remarks>
			[ParameterTableRowAttribute("colR_07", index: 75, minimum: 0, maximum: 255, step: 1, order: 63, unknown2: 0)]
			[DisplayName("R")]
			[Description("Collimated light source : 07")]
			[DefaultValue((Int16)255)]
			public Int16 ColR_07 {
				get { return colR_07; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColR_07.");
					SetProperty(ref colR_07, ref value, ColR_07Property);
				}
			}

			/// <summary>G</summary>
			/// <remarks>
			/// Japanese short name: "Ｇ", Google translated: "G".
			/// Japanese description: "平行光源：０７", Google translated: "Collimated light source : 07".
			/// </remarks>
			[ParameterTableRowAttribute("colG_07", index: 76, minimum: 0, maximum: 255, step: 1, order: 64, unknown2: 0)]
			[DisplayName("G")]
			[Description("Collimated light source : 07")]
			[DefaultValue((Int16)255)]
			public Int16 ColG_07 {
				get { return colG_07; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColG_07.");
					SetProperty(ref colG_07, ref value, ColG_07Property);
				}
			}

			/// <summary>B</summary>
			/// <remarks>
			/// Japanese short name: "Ｂ", Google translated: "B".
			/// Japanese description: "平行光源：０７", Google translated: "Collimated light source : 07".
			/// </remarks>
			[ParameterTableRowAttribute("colB_07", index: 77, minimum: 0, maximum: 255, step: 1, order: 65, unknown2: 0)]
			[DisplayName("B")]
			[Description("Collimated light source : 07")]
			[DefaultValue((Int16)255)]
			public Int16 ColB_07 {
				get { return colB_07; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColB_07.");
					SetProperty(ref colB_07, ref value, ColB_07Property);
				}
			}

			/// <summary>RGB magnification [ %]</summary>
			/// <remarks>
			/// Japanese short name: "RGB倍率[％]", Google translated: "RGB magnification [ %]".
			/// Japanese description: "平行光源：０７ (0:未使用)", Google translated: "Collimated light source : 07 ( 0 : not used)".
			/// </remarks>
			[ParameterTableRowAttribute("colA_07", index: 78, minimum: 0, maximum: 10000, step: 1, order: 66, unknown2: 0)]
			[DisplayName("RGB magnification [ %]")]
			[Description("Collimated light source : 07 ( 0 : not used)")]
			[DefaultValue((Int16)0)]
			public Int16 ColA_07 {
				get { return colA_07; }
				set {
					if ((double)value < 0 || (double)value > 10000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 10000 for ColA_07.");
					SetProperty(ref colA_07, ref value, ColA_07Property);
				}
			}

			/// <summary>Padding</summary>
			/// <remarks>
			/// Japanese short name: "パディング", Google translated: "Padding".
			/// Japanese description: "平行光源：０７", Google translated: "Collimated light source : 07".
			/// </remarks>
			[ParameterTableRowAttribute("pad_07[4]", index: 79, minimum: 0, maximum: 0, step: 0, order: 108, unknown2: 0)]
			[DisplayName("Padding")]
			[Description("Collimated light source : 07")]
			public Byte[] Pad_07 {
				get { return pad_07; }
				set { SetProperty(ref pad_07, ref value, Pad_07Property); }
			}

			/// <summary>X angle</summary>
			/// <remarks>
			/// Japanese short name: "X角度", Google translated: "X angle".
			/// Japanese description: "平行光源：０８", Google translated: "Collimated light source : 08".
			/// </remarks>
			[ParameterTableRowAttribute("degRotX_08", index: 80, minimum: -90, maximum: 90, step: 1, order: 67, unknown2: 0)]
			[DisplayName("X angle")]
			[Description("Collimated light source : 08")]
			[DefaultValue((Int16)0)]
			public Int16 DegRotX_08 {
				get { return degRotX_08; }
				set {
					if ((double)value < -90 || (double)value > 90)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -90 to 90 for DegRotX_08.");
					SetProperty(ref degRotX_08, ref value, DegRotX_08Property);
				}
			}

			/// <summary>Y angle</summary>
			/// <remarks>
			/// Japanese short name: "Ｙ角度", Google translated: "Y angle".
			/// Japanese description: "平行光源：０８", Google translated: "Collimated light source : 08".
			/// </remarks>
			[ParameterTableRowAttribute("degRotY_08", index: 81, minimum: -180, maximum: 180, step: 1, order: 68, unknown2: 1)]
			[DisplayName("Y angle")]
			[Description("Collimated light source : 08")]
			[DefaultValue((Int16)0)]
			public Int16 DegRotY_08 {
				get { return degRotY_08; }
				set {
					if ((double)value < -180 || (double)value > 180)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -180 to 180 for DegRotY_08.");
					SetProperty(ref degRotY_08, ref value, DegRotY_08Property);
				}
			}

			/// <summary>R</summary>
			/// <remarks>
			/// Japanese short name: "Ｒ", Google translated: "R".
			/// Japanese description: "平行光源：０８", Google translated: "Collimated light source : 08".
			/// </remarks>
			[ParameterTableRowAttribute("colR_08", index: 82, minimum: 0, maximum: 255, step: 1, order: 69, unknown2: 0)]
			[DisplayName("R")]
			[Description("Collimated light source : 08")]
			[DefaultValue((Int16)255)]
			public Int16 ColR_08 {
				get { return colR_08; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColR_08.");
					SetProperty(ref colR_08, ref value, ColR_08Property);
				}
			}

			/// <summary>G</summary>
			/// <remarks>
			/// Japanese short name: "Ｇ", Google translated: "G".
			/// Japanese description: "平行光源：０８", Google translated: "Collimated light source : 08".
			/// </remarks>
			[ParameterTableRowAttribute("colG_08", index: 83, minimum: 0, maximum: 255, step: 1, order: 70, unknown2: 0)]
			[DisplayName("G")]
			[Description("Collimated light source : 08")]
			[DefaultValue((Int16)255)]
			public Int16 ColG_08 {
				get { return colG_08; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColG_08.");
					SetProperty(ref colG_08, ref value, ColG_08Property);
				}
			}

			/// <summary>B</summary>
			/// <remarks>
			/// Japanese short name: "Ｂ", Google translated: "B".
			/// Japanese description: "平行光源：０８", Google translated: "Collimated light source : 08".
			/// </remarks>
			[ParameterTableRowAttribute("colB_08", index: 84, minimum: 0, maximum: 255, step: 1, order: 71, unknown2: 0)]
			[DisplayName("B")]
			[Description("Collimated light source : 08")]
			[DefaultValue((Int16)255)]
			public Int16 ColB_08 {
				get { return colB_08; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColB_08.");
					SetProperty(ref colB_08, ref value, ColB_08Property);
				}
			}

			/// <summary>RGB magnification [ %]</summary>
			/// <remarks>
			/// Japanese short name: "RGB倍率[％]", Google translated: "RGB magnification [ %]".
			/// Japanese description: "平行光源：０８ (0:未使用)", Google translated: "Collimated light source : 08 ( 0 : not used)".
			/// </remarks>
			[ParameterTableRowAttribute("colA_08", index: 85, minimum: 0, maximum: 10000, step: 1, order: 72, unknown2: 0)]
			[DisplayName("RGB magnification [ %]")]
			[Description("Collimated light source : 08 ( 0 : not used)")]
			[DefaultValue((Int16)0)]
			public Int16 ColA_08 {
				get { return colA_08; }
				set {
					if ((double)value < 0 || (double)value > 10000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 10000 for ColA_08.");
					SetProperty(ref colA_08, ref value, ColA_08Property);
				}
			}

			/// <summary>Padding</summary>
			/// <remarks>
			/// Japanese short name: "パディング", Google translated: "Padding".
			/// Japanese description: "平行光源：０８", Google translated: "Collimated light source : 08".
			/// </remarks>
			[ParameterTableRowAttribute("pad_08[4]", index: 86, minimum: 0, maximum: 0, step: 0, order: 109, unknown2: 0)]
			[DisplayName("Padding")]
			[Description("Collimated light source : 08")]
			public Byte[] Pad_08 {
				get { return pad_08; }
				set { SetProperty(ref pad_08, ref value, Pad_08Property); }
			}

			/// <summary>X angle</summary>
			/// <remarks>
			/// Japanese short name: "X角度", Google translated: "X angle".
			/// Japanese description: "平行光源：０９", Google translated: "Collimated light source : 09".
			/// </remarks>
			[ParameterTableRowAttribute("degRotX_09", index: 87, minimum: -90, maximum: 90, step: 1, order: 73, unknown2: 0)]
			[DisplayName("X angle")]
			[Description("Collimated light source : 09")]
			[DefaultValue((Int16)0)]
			public Int16 DegRotX_09 {
				get { return degRotX_09; }
				set {
					if ((double)value < -90 || (double)value > 90)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -90 to 90 for DegRotX_09.");
					SetProperty(ref degRotX_09, ref value, DegRotX_09Property);
				}
			}

			/// <summary>Y angle</summary>
			/// <remarks>
			/// Japanese short name: "Ｙ角度", Google translated: "Y angle".
			/// Japanese description: "平行光源：０９", Google translated: "Collimated light source : 09".
			/// </remarks>
			[ParameterTableRowAttribute("degRotY_09", index: 88, minimum: -180, maximum: 180, step: 1, order: 74, unknown2: 1)]
			[DisplayName("Y angle")]
			[Description("Collimated light source : 09")]
			[DefaultValue((Int16)0)]
			public Int16 DegRotY_09 {
				get { return degRotY_09; }
				set {
					if ((double)value < -180 || (double)value > 180)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -180 to 180 for DegRotY_09.");
					SetProperty(ref degRotY_09, ref value, DegRotY_09Property);
				}
			}

			/// <summary>R</summary>
			/// <remarks>
			/// Japanese short name: "Ｒ", Google translated: "R".
			/// Japanese description: "平行光源：０９", Google translated: "Collimated light source : 09".
			/// </remarks>
			[ParameterTableRowAttribute("colR_09", index: 89, minimum: 0, maximum: 255, step: 1, order: 75, unknown2: 0)]
			[DisplayName("R")]
			[Description("Collimated light source : 09")]
			[DefaultValue((Int16)255)]
			public Int16 ColR_09 {
				get { return colR_09; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColR_09.");
					SetProperty(ref colR_09, ref value, ColR_09Property);
				}
			}

			/// <summary>G</summary>
			/// <remarks>
			/// Japanese short name: "Ｇ", Google translated: "G".
			/// Japanese description: "平行光源：０９", Google translated: "Collimated light source : 09".
			/// </remarks>
			[ParameterTableRowAttribute("colG_09", index: 90, minimum: 0, maximum: 255, step: 1, order: 76, unknown2: 0)]
			[DisplayName("G")]
			[Description("Collimated light source : 09")]
			[DefaultValue((Int16)255)]
			public Int16 ColG_09 {
				get { return colG_09; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColG_09.");
					SetProperty(ref colG_09, ref value, ColG_09Property);
				}
			}

			/// <summary>B</summary>
			/// <remarks>
			/// Japanese short name: "Ｂ", Google translated: "B".
			/// Japanese description: "平行光源：０９", Google translated: "Collimated light source : 09".
			/// </remarks>
			[ParameterTableRowAttribute("colB_09", index: 91, minimum: 0, maximum: 255, step: 1, order: 77, unknown2: 0)]
			[DisplayName("B")]
			[Description("Collimated light source : 09")]
			[DefaultValue((Int16)255)]
			public Int16 ColB_09 {
				get { return colB_09; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColB_09.");
					SetProperty(ref colB_09, ref value, ColB_09Property);
				}
			}

			/// <summary>RGB magnification [ %]</summary>
			/// <remarks>
			/// Japanese short name: "RGB倍率[％]", Google translated: "RGB magnification [ %]".
			/// Japanese description: "平行光源：０９ (0:未使用)", Google translated: "Collimated light source : 09 ( 0 : not used)".
			/// </remarks>
			[ParameterTableRowAttribute("colA_09", index: 92, minimum: 0, maximum: 10000, step: 1, order: 78, unknown2: 0)]
			[DisplayName("RGB magnification [ %]")]
			[Description("Collimated light source : 09 ( 0 : not used)")]
			[DefaultValue((Int16)0)]
			public Int16 ColA_09 {
				get { return colA_09; }
				set {
					if ((double)value < 0 || (double)value > 10000)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 10000 for ColA_09.");
					SetProperty(ref colA_09, ref value, ColA_09Property);
				}
			}

			/// <summary>Padding</summary>
			/// <remarks>
			/// Japanese short name: "パディング", Google translated: "Padding".
			/// Japanese description: "平行光源：０９", Google translated: "Collimated light source : 09".
			/// </remarks>
			[ParameterTableRowAttribute("pad_09[4]", index: 93, minimum: 0, maximum: 0, step: 0, order: 110, unknown2: 0)]
			[DisplayName("Padding")]
			[Description("Collimated light source : 09")]
			public Byte[] Pad_09 {
				get { return pad_09; }
				set { SetProperty(ref pad_09, ref value, Pad_09Property); }
			}

			internal EnvironmentLight(ParameterTable table, int index, AssetLoader loader, int next)
				: base(table, index, loader) {
				BinaryReader reader = loader.Reader;

				IsUse = reader.ReadSByte();
				AutoUpdate = reader.ReadSByte();
				Pad_0 = reader.ReadBytes(12);
				InvMulCol = reader.ReadInt16();
				ResNameId_Dif0 = reader.ReadInt16();
				InvMulCol_Dif0 = reader.ReadInt16();
				SepcPow_Dif0 = reader.ReadSingle();
				Pad_Dif0 = reader.ReadBytes(8);
				ResNameId_Spc0 = reader.ReadInt16();
				InvMulCol_Spc0 = reader.ReadInt16();
				SepcPow_Spc0 = reader.ReadSingle();
				Pad_Spc0 = reader.ReadBytes(8);
				ResNameId_Spc1 = reader.ReadInt16();
				InvMulCol_Spc1 = reader.ReadInt16();
				SepcPow_Spc1 = reader.ReadSingle();
				Pad_Spc1 = reader.ReadBytes(8);
				ResNameId_Spc2 = reader.ReadInt16();
				InvMulCol_Spc2 = reader.ReadInt16();
				SepcPow_Spc2 = reader.ReadSingle();
				Pad_Spc2 = reader.ReadBytes(8);
				ResNameId_Spc3 = reader.ReadInt16();
				InvMulCol_Spc3 = reader.ReadInt16();
				SepcPow_Spc3 = reader.ReadSingle();
				Pad_Spc3 = reader.ReadBytes(8);
				DegRotX_00 = reader.ReadInt16();
				DegRotY_00 = reader.ReadInt16();
				ColR_00 = reader.ReadInt16();
				ColG_00 = reader.ReadInt16();
				ColB_00 = reader.ReadInt16();
				ColA_00 = reader.ReadInt16();
				Pad_00 = reader.ReadBytes(4);
				DegRotX_01 = reader.ReadInt16();
				DegRotY_01 = reader.ReadInt16();
				ColR_01 = reader.ReadInt16();
				ColG_01 = reader.ReadInt16();
				ColB_01 = reader.ReadInt16();
				ColA_01 = reader.ReadInt16();
				Pad_01 = reader.ReadBytes(4);
				DegRotX_02 = reader.ReadInt16();
				DegRotY_02 = reader.ReadInt16();
				ColR_02 = reader.ReadInt16();
				ColG_02 = reader.ReadInt16();
				ColB_02 = reader.ReadInt16();
				ColA_02 = reader.ReadInt16();
				Pad_02 = reader.ReadBytes(4);
				DegRotX_03 = reader.ReadInt16();
				DegRotY_03 = reader.ReadInt16();
				ColR_03 = reader.ReadInt16();
				ColG_03 = reader.ReadInt16();
				ColB_03 = reader.ReadInt16();
				ColA_03 = reader.ReadInt16();
				Pad_03 = reader.ReadBytes(4);
				DegRotX_04 = reader.ReadInt16();
				DegRotY_04 = reader.ReadInt16();
				ColR_04 = reader.ReadInt16();
				ColG_04 = reader.ReadInt16();
				ColB_04 = reader.ReadInt16();
				ColA_04 = reader.ReadInt16();
				Pad_04 = reader.ReadBytes(4);
				DegRotX_05 = reader.ReadInt16();
				DegRotY_05 = reader.ReadInt16();
				ColR_05 = reader.ReadInt16();
				ColG_05 = reader.ReadInt16();
				ColB_05 = reader.ReadInt16();
				ColA_05 = reader.ReadInt16();
				Pad_05 = reader.ReadBytes(4);
				DegRotX_06 = reader.ReadInt16();
				DegRotY_06 = reader.ReadInt16();
				ColR_06 = reader.ReadInt16();
				ColG_06 = reader.ReadInt16();
				ColB_06 = reader.ReadInt16();
				ColA_06 = reader.ReadInt16();
				Pad_06 = reader.ReadBytes(4);
				DegRotX_07 = reader.ReadInt16();
				DegRotY_07 = reader.ReadInt16();
				ColR_07 = reader.ReadInt16();
				ColG_07 = reader.ReadInt16();
				ColB_07 = reader.ReadInt16();
				ColA_07 = reader.ReadInt16();
				Pad_07 = reader.ReadBytes(4);
				DegRotX_08 = reader.ReadInt16();
				DegRotY_08 = reader.ReadInt16();
				ColR_08 = reader.ReadInt16();
				ColG_08 = reader.ReadInt16();
				ColB_08 = reader.ReadInt16();
				ColA_08 = reader.ReadInt16();
				Pad_08 = reader.ReadBytes(4);
				DegRotX_09 = reader.ReadInt16();
				DegRotY_09 = reader.ReadInt16();
				ColR_09 = reader.ReadInt16();
				ColG_09 = reader.ReadInt16();
				ColB_09 = reader.ReadInt16();
				ColA_09 = reader.ReadInt16();
				Pad_09 = reader.ReadBytes(4);
			}

			internal EnvironmentLight(ParameterTable table, int index)
				: base(table, index) {
				IsUse = (SByte)0;
				AutoUpdate = (SByte)0;
				Pad_0 = new Byte[12];
				InvMulCol = (Int16)100;
				ResNameId_Dif0 = (Int16)0;
				InvMulCol_Dif0 = (Int16)100;
				SepcPow_Dif0 = (Single)1;
				Pad_Dif0 = new Byte[8];
				ResNameId_Spc0 = (Int16)0;
				InvMulCol_Spc0 = (Int16)100;
				SepcPow_Spc0 = (Single)1;
				Pad_Spc0 = new Byte[8];
				ResNameId_Spc1 = (Int16)1;
				InvMulCol_Spc1 = (Int16)100;
				SepcPow_Spc1 = (Single)1;
				Pad_Spc1 = new Byte[8];
				ResNameId_Spc2 = (Int16)2;
				InvMulCol_Spc2 = (Int16)100;
				SepcPow_Spc2 = (Single)1;
				Pad_Spc2 = new Byte[8];
				ResNameId_Spc3 = (Int16)3;
				InvMulCol_Spc3 = (Int16)100;
				SepcPow_Spc3 = (Single)1;
				Pad_Spc3 = new Byte[8];
				DegRotX_00 = (Int16)0;
				DegRotY_00 = (Int16)0;
				ColR_00 = (Int16)255;
				ColG_00 = (Int16)255;
				ColB_00 = (Int16)255;
				ColA_00 = (Int16)100;
				Pad_00 = new Byte[4];
				DegRotX_01 = (Int16)0;
				DegRotY_01 = (Int16)0;
				ColR_01 = (Int16)255;
				ColG_01 = (Int16)255;
				ColB_01 = (Int16)255;
				ColA_01 = (Int16)0;
				Pad_01 = new Byte[4];
				DegRotX_02 = (Int16)0;
				DegRotY_02 = (Int16)0;
				ColR_02 = (Int16)255;
				ColG_02 = (Int16)255;
				ColB_02 = (Int16)255;
				ColA_02 = (Int16)0;
				Pad_02 = new Byte[4];
				DegRotX_03 = (Int16)0;
				DegRotY_03 = (Int16)0;
				ColR_03 = (Int16)255;
				ColG_03 = (Int16)255;
				ColB_03 = (Int16)255;
				ColA_03 = (Int16)0;
				Pad_03 = new Byte[4];
				DegRotX_04 = (Int16)0;
				DegRotY_04 = (Int16)0;
				ColR_04 = (Int16)255;
				ColG_04 = (Int16)255;
				ColB_04 = (Int16)255;
				ColA_04 = (Int16)0;
				Pad_04 = new Byte[4];
				DegRotX_05 = (Int16)0;
				DegRotY_05 = (Int16)0;
				ColR_05 = (Int16)255;
				ColG_05 = (Int16)255;
				ColB_05 = (Int16)255;
				ColA_05 = (Int16)0;
				Pad_05 = new Byte[4];
				DegRotX_06 = (Int16)0;
				DegRotY_06 = (Int16)0;
				ColR_06 = (Int16)255;
				ColG_06 = (Int16)255;
				ColB_06 = (Int16)255;
				ColA_06 = (Int16)0;
				Pad_06 = new Byte[4];
				DegRotX_07 = (Int16)0;
				DegRotY_07 = (Int16)0;
				ColR_07 = (Int16)255;
				ColG_07 = (Int16)255;
				ColB_07 = (Int16)255;
				ColA_07 = (Int16)0;
				Pad_07 = new Byte[4];
				DegRotX_08 = (Int16)0;
				DegRotY_08 = (Int16)0;
				ColR_08 = (Int16)255;
				ColG_08 = (Int16)255;
				ColB_08 = (Int16)255;
				ColA_08 = (Int16)0;
				Pad_08 = new Byte[4];
				DegRotX_09 = (Int16)0;
				DegRotY_09 = (Int16)0;
				ColR_09 = (Int16)255;
				ColG_09 = (Int16)255;
				ColB_09 = (Int16)255;
				ColA_09 = (Int16)0;
				Pad_09 = new Byte[4];
			}

			public override void Write(BinaryWriter writer) {
				writer.Write(IsUse);
				writer.Write(AutoUpdate);
				writer.Write(Pad_0);
				writer.Write(InvMulCol);
				writer.Write(ResNameId_Dif0);
				writer.Write(InvMulCol_Dif0);
				writer.Write(SepcPow_Dif0);
				writer.Write(Pad_Dif0);
				writer.Write(ResNameId_Spc0);
				writer.Write(InvMulCol_Spc0);
				writer.Write(SepcPow_Spc0);
				writer.Write(Pad_Spc0);
				writer.Write(ResNameId_Spc1);
				writer.Write(InvMulCol_Spc1);
				writer.Write(SepcPow_Spc1);
				writer.Write(Pad_Spc1);
				writer.Write(ResNameId_Spc2);
				writer.Write(InvMulCol_Spc2);
				writer.Write(SepcPow_Spc2);
				writer.Write(Pad_Spc2);
				writer.Write(ResNameId_Spc3);
				writer.Write(InvMulCol_Spc3);
				writer.Write(SepcPow_Spc3);
				writer.Write(Pad_Spc3);
				writer.Write(DegRotX_00);
				writer.Write(DegRotY_00);
				writer.Write(ColR_00);
				writer.Write(ColG_00);
				writer.Write(ColB_00);
				writer.Write(ColA_00);
				writer.Write(Pad_00);
				writer.Write(DegRotX_01);
				writer.Write(DegRotY_01);
				writer.Write(ColR_01);
				writer.Write(ColG_01);
				writer.Write(ColB_01);
				writer.Write(ColA_01);
				writer.Write(Pad_01);
				writer.Write(DegRotX_02);
				writer.Write(DegRotY_02);
				writer.Write(ColR_02);
				writer.Write(ColG_02);
				writer.Write(ColB_02);
				writer.Write(ColA_02);
				writer.Write(Pad_02);
				writer.Write(DegRotX_03);
				writer.Write(DegRotY_03);
				writer.Write(ColR_03);
				writer.Write(ColG_03);
				writer.Write(ColB_03);
				writer.Write(ColA_03);
				writer.Write(Pad_03);
				writer.Write(DegRotX_04);
				writer.Write(DegRotY_04);
				writer.Write(ColR_04);
				writer.Write(ColG_04);
				writer.Write(ColB_04);
				writer.Write(ColA_04);
				writer.Write(Pad_04);
				writer.Write(DegRotX_05);
				writer.Write(DegRotY_05);
				writer.Write(ColR_05);
				writer.Write(ColG_05);
				writer.Write(ColB_05);
				writer.Write(ColA_05);
				writer.Write(Pad_05);
				writer.Write(DegRotX_06);
				writer.Write(DegRotY_06);
				writer.Write(ColR_06);
				writer.Write(ColG_06);
				writer.Write(ColB_06);
				writer.Write(ColA_06);
				writer.Write(Pad_06);
				writer.Write(DegRotX_07);
				writer.Write(DegRotY_07);
				writer.Write(ColR_07);
				writer.Write(ColG_07);
				writer.Write(ColB_07);
				writer.Write(ColA_07);
				writer.Write(Pad_07);
				writer.Write(DegRotX_08);
				writer.Write(DegRotY_08);
				writer.Write(ColR_08);
				writer.Write(ColG_08);
				writer.Write(ColB_08);
				writer.Write(ColA_08);
				writer.Write(Pad_08);
				writer.Write(DegRotX_09);
				writer.Write(DegRotY_09);
				writer.Write(ColR_09);
				writer.Write(ColG_09);
				writer.Write(ColB_09);
				writer.Write(ColA_09);
				writer.Write(Pad_09);
			}
		}
	}
}
