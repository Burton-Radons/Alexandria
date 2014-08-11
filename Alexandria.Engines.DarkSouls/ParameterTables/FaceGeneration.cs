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
		/// From "FaceGenParam.paramdef" (id 25h)
		/// </remarks>
		public class FaceGeneration : ParameterTableRow {
			/// <summary>The name of the table in the file.</summary>
			public const string TableName = "FACE_PARAM_ST";

			Byte faceGeoData00, faceGeoData01, faceGeoData02, faceGeoData03, faceGeoData04, faceGeoData05, faceGeoData06, faceGeoData07, faceGeoData08, faceGeoData09, faceGeoData10, faceGeoData11, faceGeoData12, faceGeoData13, faceGeoData14, faceGeoData15, faceGeoData16, faceGeoData17, faceGeoData18, faceGeoData19, faceGeoData20, faceGeoData21, faceGeoData22, faceGeoData23, faceGeoData24, faceGeoData25, faceGeoData26, faceGeoData27, faceGeoData28, faceGeoData29, faceGeoData30, faceGeoData31, faceGeoData32, faceGeoData33, faceGeoData34, faceGeoData35, faceGeoData36, faceGeoData37, faceGeoData38, faceGeoData39, faceGeoData40, faceGeoData41, faceGeoData42, faceGeoData43, faceGeoData44, faceGeoData45, faceGeoData46, faceGeoData47, faceGeoData48, faceGeoData49, faceTexData00, faceTexData01, faceTexData02, faceTexData03, faceTexData04, faceTexData05, faceTexData06, faceTexData07, faceTexData08, faceTexData09, faceTexData10, faceTexData11, faceTexData12, faceTexData13, faceTexData14, faceTexData15, faceTexData16, faceTexData17, faceTexData18, faceTexData19, faceTexData20, faceTexData21, faceTexData22, faceTexData23, faceTexData24, faceTexData25, faceTexData26, faceTexData27, faceTexData28, faceTexData29, faceTexData30, faceTexData31, faceTexData32, faceTexData33, faceTexData34, faceTexData35, faceTexData36, faceTexData37, faceTexData38, faceTexData39, faceTexData40, faceTexData41, faceTexData42, faceTexData43, faceTexData44, faceTexData45, faceTexData46, faceTexData47, faceTexData48, faceTexData49, hairColor_R, hairColor_G, hairColor_B, eyeColor_R, eyeColor_G, eyeColor_B;
			FaceHairStyle hairStyle;
			FaceHairColor hairColor_Base;
			Byte[] pad;

			/// <summary>A property of the class.</summary>
			public static readonly PropertyInfo
				FaceGeoData00Property = GetProperty<FaceGeneration>("FaceGeoData00"),
				FaceGeoData01Property = GetProperty<FaceGeneration>("FaceGeoData01"),
				FaceGeoData02Property = GetProperty<FaceGeneration>("FaceGeoData02"),
				FaceGeoData03Property = GetProperty<FaceGeneration>("FaceGeoData03"),
				FaceGeoData04Property = GetProperty<FaceGeneration>("FaceGeoData04"),
				FaceGeoData05Property = GetProperty<FaceGeneration>("FaceGeoData05"),
				FaceGeoData06Property = GetProperty<FaceGeneration>("FaceGeoData06"),
				FaceGeoData07Property = GetProperty<FaceGeneration>("FaceGeoData07"),
				FaceGeoData08Property = GetProperty<FaceGeneration>("FaceGeoData08"),
				FaceGeoData09Property = GetProperty<FaceGeneration>("FaceGeoData09"),
				FaceGeoData10Property = GetProperty<FaceGeneration>("FaceGeoData10"),
				FaceGeoData11Property = GetProperty<FaceGeneration>("FaceGeoData11"),
				FaceGeoData12Property = GetProperty<FaceGeneration>("FaceGeoData12"),
				FaceGeoData13Property = GetProperty<FaceGeneration>("FaceGeoData13"),
				FaceGeoData14Property = GetProperty<FaceGeneration>("FaceGeoData14"),
				FaceGeoData15Property = GetProperty<FaceGeneration>("FaceGeoData15"),
				FaceGeoData16Property = GetProperty<FaceGeneration>("FaceGeoData16"),
				FaceGeoData17Property = GetProperty<FaceGeneration>("FaceGeoData17"),
				FaceGeoData18Property = GetProperty<FaceGeneration>("FaceGeoData18"),
				FaceGeoData19Property = GetProperty<FaceGeneration>("FaceGeoData19"),
				FaceGeoData20Property = GetProperty<FaceGeneration>("FaceGeoData20"),
				FaceGeoData21Property = GetProperty<FaceGeneration>("FaceGeoData21"),
				FaceGeoData22Property = GetProperty<FaceGeneration>("FaceGeoData22"),
				FaceGeoData23Property = GetProperty<FaceGeneration>("FaceGeoData23"),
				FaceGeoData24Property = GetProperty<FaceGeneration>("FaceGeoData24"),
				FaceGeoData25Property = GetProperty<FaceGeneration>("FaceGeoData25"),
				FaceGeoData26Property = GetProperty<FaceGeneration>("FaceGeoData26"),
				FaceGeoData27Property = GetProperty<FaceGeneration>("FaceGeoData27"),
				FaceGeoData28Property = GetProperty<FaceGeneration>("FaceGeoData28"),
				FaceGeoData29Property = GetProperty<FaceGeneration>("FaceGeoData29"),
				FaceGeoData30Property = GetProperty<FaceGeneration>("FaceGeoData30"),
				FaceGeoData31Property = GetProperty<FaceGeneration>("FaceGeoData31"),
				FaceGeoData32Property = GetProperty<FaceGeneration>("FaceGeoData32"),
				FaceGeoData33Property = GetProperty<FaceGeneration>("FaceGeoData33"),
				FaceGeoData34Property = GetProperty<FaceGeneration>("FaceGeoData34"),
				FaceGeoData35Property = GetProperty<FaceGeneration>("FaceGeoData35"),
				FaceGeoData36Property = GetProperty<FaceGeneration>("FaceGeoData36"),
				FaceGeoData37Property = GetProperty<FaceGeneration>("FaceGeoData37"),
				FaceGeoData38Property = GetProperty<FaceGeneration>("FaceGeoData38"),
				FaceGeoData39Property = GetProperty<FaceGeneration>("FaceGeoData39"),
				FaceGeoData40Property = GetProperty<FaceGeneration>("FaceGeoData40"),
				FaceGeoData41Property = GetProperty<FaceGeneration>("FaceGeoData41"),
				FaceGeoData42Property = GetProperty<FaceGeneration>("FaceGeoData42"),
				FaceGeoData43Property = GetProperty<FaceGeneration>("FaceGeoData43"),
				FaceGeoData44Property = GetProperty<FaceGeneration>("FaceGeoData44"),
				FaceGeoData45Property = GetProperty<FaceGeneration>("FaceGeoData45"),
				FaceGeoData46Property = GetProperty<FaceGeneration>("FaceGeoData46"),
				FaceGeoData47Property = GetProperty<FaceGeneration>("FaceGeoData47"),
				FaceGeoData48Property = GetProperty<FaceGeneration>("FaceGeoData48"),
				FaceGeoData49Property = GetProperty<FaceGeneration>("FaceGeoData49"),
				FaceTexData00Property = GetProperty<FaceGeneration>("FaceTexData00"),
				FaceTexData01Property = GetProperty<FaceGeneration>("FaceTexData01"),
				FaceTexData02Property = GetProperty<FaceGeneration>("FaceTexData02"),
				FaceTexData03Property = GetProperty<FaceGeneration>("FaceTexData03"),
				FaceTexData04Property = GetProperty<FaceGeneration>("FaceTexData04"),
				FaceTexData05Property = GetProperty<FaceGeneration>("FaceTexData05"),
				FaceTexData06Property = GetProperty<FaceGeneration>("FaceTexData06"),
				FaceTexData07Property = GetProperty<FaceGeneration>("FaceTexData07"),
				FaceTexData08Property = GetProperty<FaceGeneration>("FaceTexData08"),
				FaceTexData09Property = GetProperty<FaceGeneration>("FaceTexData09"),
				FaceTexData10Property = GetProperty<FaceGeneration>("FaceTexData10"),
				FaceTexData11Property = GetProperty<FaceGeneration>("FaceTexData11"),
				FaceTexData12Property = GetProperty<FaceGeneration>("FaceTexData12"),
				FaceTexData13Property = GetProperty<FaceGeneration>("FaceTexData13"),
				FaceTexData14Property = GetProperty<FaceGeneration>("FaceTexData14"),
				FaceTexData15Property = GetProperty<FaceGeneration>("FaceTexData15"),
				FaceTexData16Property = GetProperty<FaceGeneration>("FaceTexData16"),
				FaceTexData17Property = GetProperty<FaceGeneration>("FaceTexData17"),
				FaceTexData18Property = GetProperty<FaceGeneration>("FaceTexData18"),
				FaceTexData19Property = GetProperty<FaceGeneration>("FaceTexData19"),
				FaceTexData20Property = GetProperty<FaceGeneration>("FaceTexData20"),
				FaceTexData21Property = GetProperty<FaceGeneration>("FaceTexData21"),
				FaceTexData22Property = GetProperty<FaceGeneration>("FaceTexData22"),
				FaceTexData23Property = GetProperty<FaceGeneration>("FaceTexData23"),
				FaceTexData24Property = GetProperty<FaceGeneration>("FaceTexData24"),
				FaceTexData25Property = GetProperty<FaceGeneration>("FaceTexData25"),
				FaceTexData26Property = GetProperty<FaceGeneration>("FaceTexData26"),
				FaceTexData27Property = GetProperty<FaceGeneration>("FaceTexData27"),
				FaceTexData28Property = GetProperty<FaceGeneration>("FaceTexData28"),
				FaceTexData29Property = GetProperty<FaceGeneration>("FaceTexData29"),
				FaceTexData30Property = GetProperty<FaceGeneration>("FaceTexData30"),
				FaceTexData31Property = GetProperty<FaceGeneration>("FaceTexData31"),
				FaceTexData32Property = GetProperty<FaceGeneration>("FaceTexData32"),
				FaceTexData33Property = GetProperty<FaceGeneration>("FaceTexData33"),
				FaceTexData34Property = GetProperty<FaceGeneration>("FaceTexData34"),
				FaceTexData35Property = GetProperty<FaceGeneration>("FaceTexData35"),
				FaceTexData36Property = GetProperty<FaceGeneration>("FaceTexData36"),
				FaceTexData37Property = GetProperty<FaceGeneration>("FaceTexData37"),
				FaceTexData38Property = GetProperty<FaceGeneration>("FaceTexData38"),
				FaceTexData39Property = GetProperty<FaceGeneration>("FaceTexData39"),
				FaceTexData40Property = GetProperty<FaceGeneration>("FaceTexData40"),
				FaceTexData41Property = GetProperty<FaceGeneration>("FaceTexData41"),
				FaceTexData42Property = GetProperty<FaceGeneration>("FaceTexData42"),
				FaceTexData43Property = GetProperty<FaceGeneration>("FaceTexData43"),
				FaceTexData44Property = GetProperty<FaceGeneration>("FaceTexData44"),
				FaceTexData45Property = GetProperty<FaceGeneration>("FaceTexData45"),
				FaceTexData46Property = GetProperty<FaceGeneration>("FaceTexData46"),
				FaceTexData47Property = GetProperty<FaceGeneration>("FaceTexData47"),
				FaceTexData48Property = GetProperty<FaceGeneration>("FaceTexData48"),
				FaceTexData49Property = GetProperty<FaceGeneration>("FaceTexData49"),
				HairStyleProperty = GetProperty<FaceGeneration>("HairStyle"),
				HairColor_BaseProperty = GetProperty<FaceGeneration>("HairColor_Base"),
				HairColor_RProperty = GetProperty<FaceGeneration>("HairColor_R"),
				HairColor_GProperty = GetProperty<FaceGeneration>("HairColor_G"),
				HairColor_BProperty = GetProperty<FaceGeneration>("HairColor_B"),
				EyeColor_RProperty = GetProperty<FaceGeneration>("EyeColor_R"),
				EyeColor_GProperty = GetProperty<FaceGeneration>("EyeColor_G"),
				EyeColor_BProperty = GetProperty<FaceGeneration>("EyeColor_B"),
				PadProperty = GetProperty<FaceGeneration>("Pad");

			/// <summary>Face create geometry data 00</summary>
			/// <remarks>
			/// Japanese short name: "顔作成ジオメトリデータ00", Google translated: "Face create geometry data 00".
			/// Japanese description: "顔作成ジオメトリデータ00", Google translated: "Face create geometry data 00".
			/// </remarks>
			[ParameterTableRowAttribute("faceGeoData00", index: 0, minimum: 0, maximum: 255, step: 1, sortOrder: 1, unknown2: 1)]
			[DisplayName("Face create geometry data 00")]
			[Description("Face create geometry data 00")]
			[DefaultValue((Byte)0)]
			public Byte FaceGeoData00 {
				get { return faceGeoData00; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceGeoData00.");
					SetProperty(ref faceGeoData00, ref value, FaceGeoData00Property);
				}
			}

			/// <summary>Face create geometry data 01</summary>
			/// <remarks>
			/// Japanese short name: "顔作成ジオメトリデータ01", Google translated: "Face create geometry data 01".
			/// Japanese description: "顔作成ジオメトリデータ01", Google translated: "Face create geometry data 01".
			/// </remarks>
			[ParameterTableRowAttribute("faceGeoData01", index: 1, minimum: 0, maximum: 255, step: 1, sortOrder: 2, unknown2: 1)]
			[DisplayName("Face create geometry data 01")]
			[Description("Face create geometry data 01")]
			[DefaultValue((Byte)0)]
			public Byte FaceGeoData01 {
				get { return faceGeoData01; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceGeoData01.");
					SetProperty(ref faceGeoData01, ref value, FaceGeoData01Property);
				}
			}

			/// <summary>Face create geometry data 02</summary>
			/// <remarks>
			/// Japanese short name: "顔作成ジオメトリデータ02", Google translated: "Face create geometry data 02".
			/// Japanese description: "顔作成ジオメトリデータ02", Google translated: "Face create geometry data 02".
			/// </remarks>
			[ParameterTableRowAttribute("faceGeoData02", index: 2, minimum: 0, maximum: 255, step: 1, sortOrder: 3, unknown2: 1)]
			[DisplayName("Face create geometry data 02")]
			[Description("Face create geometry data 02")]
			[DefaultValue((Byte)0)]
			public Byte FaceGeoData02 {
				get { return faceGeoData02; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceGeoData02.");
					SetProperty(ref faceGeoData02, ref value, FaceGeoData02Property);
				}
			}

			/// <summary>Face create geometry data 03</summary>
			/// <remarks>
			/// Japanese short name: "顔作成ジオメトリデータ03", Google translated: "Face create geometry data 03".
			/// Japanese description: "顔作成ジオメトリデータ03", Google translated: "Face create geometry data 03".
			/// </remarks>
			[ParameterTableRowAttribute("faceGeoData03", index: 3, minimum: 0, maximum: 255, step: 1, sortOrder: 4, unknown2: 1)]
			[DisplayName("Face create geometry data 03")]
			[Description("Face create geometry data 03")]
			[DefaultValue((Byte)0)]
			public Byte FaceGeoData03 {
				get { return faceGeoData03; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceGeoData03.");
					SetProperty(ref faceGeoData03, ref value, FaceGeoData03Property);
				}
			}

			/// <summary>Face create geometry data 04</summary>
			/// <remarks>
			/// Japanese short name: "顔作成ジオメトリデータ04", Google translated: "Face create geometry data 04".
			/// Japanese description: "顔作成ジオメトリデータ04", Google translated: "Face create geometry data 04".
			/// </remarks>
			[ParameterTableRowAttribute("faceGeoData04", index: 4, minimum: 0, maximum: 255, step: 1, sortOrder: 5, unknown2: 1)]
			[DisplayName("Face create geometry data 04")]
			[Description("Face create geometry data 04")]
			[DefaultValue((Byte)0)]
			public Byte FaceGeoData04 {
				get { return faceGeoData04; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceGeoData04.");
					SetProperty(ref faceGeoData04, ref value, FaceGeoData04Property);
				}
			}

			/// <summary>Face create geometry data 05</summary>
			/// <remarks>
			/// Japanese short name: "顔作成ジオメトリデータ05", Google translated: "Face create geometry data 05".
			/// Japanese description: "顔作成ジオメトリデータ05", Google translated: "Face create geometry data 05".
			/// </remarks>
			[ParameterTableRowAttribute("faceGeoData05", index: 5, minimum: 0, maximum: 255, step: 1, sortOrder: 6, unknown2: 1)]
			[DisplayName("Face create geometry data 05")]
			[Description("Face create geometry data 05")]
			[DefaultValue((Byte)0)]
			public Byte FaceGeoData05 {
				get { return faceGeoData05; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceGeoData05.");
					SetProperty(ref faceGeoData05, ref value, FaceGeoData05Property);
				}
			}

			/// <summary>Face create geometry data 06</summary>
			/// <remarks>
			/// Japanese short name: "顔作成ジオメトリデータ06", Google translated: "Face create geometry data 06".
			/// Japanese description: "顔作成ジオメトリデータ06", Google translated: "Face create geometry data 06".
			/// </remarks>
			[ParameterTableRowAttribute("faceGeoData06", index: 6, minimum: 0, maximum: 255, step: 1, sortOrder: 7, unknown2: 1)]
			[DisplayName("Face create geometry data 06")]
			[Description("Face create geometry data 06")]
			[DefaultValue((Byte)0)]
			public Byte FaceGeoData06 {
				get { return faceGeoData06; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceGeoData06.");
					SetProperty(ref faceGeoData06, ref value, FaceGeoData06Property);
				}
			}

			/// <summary>Face create geometry data 07</summary>
			/// <remarks>
			/// Japanese short name: "顔作成ジオメトリデータ07", Google translated: "Face create geometry data 07".
			/// Japanese description: "顔作成ジオメトリデータ07", Google translated: "Face create geometry data 07".
			/// </remarks>
			[ParameterTableRowAttribute("faceGeoData07", index: 7, minimum: 0, maximum: 255, step: 1, sortOrder: 8, unknown2: 1)]
			[DisplayName("Face create geometry data 07")]
			[Description("Face create geometry data 07")]
			[DefaultValue((Byte)0)]
			public Byte FaceGeoData07 {
				get { return faceGeoData07; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceGeoData07.");
					SetProperty(ref faceGeoData07, ref value, FaceGeoData07Property);
				}
			}

			/// <summary>Face create geometry data 08</summary>
			/// <remarks>
			/// Japanese short name: "顔作成ジオメトリデータ08", Google translated: "Face create geometry data 08".
			/// Japanese description: "顔作成ジオメトリデータ08", Google translated: "Face create geometry data 08".
			/// </remarks>
			[ParameterTableRowAttribute("faceGeoData08", index: 8, minimum: 0, maximum: 255, step: 1, sortOrder: 9, unknown2: 1)]
			[DisplayName("Face create geometry data 08")]
			[Description("Face create geometry data 08")]
			[DefaultValue((Byte)0)]
			public Byte FaceGeoData08 {
				get { return faceGeoData08; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceGeoData08.");
					SetProperty(ref faceGeoData08, ref value, FaceGeoData08Property);
				}
			}

			/// <summary>Face create geometry data 09</summary>
			/// <remarks>
			/// Japanese short name: "顔作成ジオメトリデータ09", Google translated: "Face create geometry data 09".
			/// Japanese description: "顔作成ジオメトリデータ09", Google translated: "Face create geometry data 09".
			/// </remarks>
			[ParameterTableRowAttribute("faceGeoData09", index: 9, minimum: 0, maximum: 255, step: 1, sortOrder: 10, unknown2: 1)]
			[DisplayName("Face create geometry data 09")]
			[Description("Face create geometry data 09")]
			[DefaultValue((Byte)0)]
			public Byte FaceGeoData09 {
				get { return faceGeoData09; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceGeoData09.");
					SetProperty(ref faceGeoData09, ref value, FaceGeoData09Property);
				}
			}

			/// <summary>Face create geometry data 10</summary>
			/// <remarks>
			/// Japanese short name: "顔作成ジオメトリデータ10", Google translated: "Face create geometry data 10".
			/// Japanese description: "顔作成ジオメトリデータ10", Google translated: "Face create geometry data 10".
			/// </remarks>
			[ParameterTableRowAttribute("faceGeoData10", index: 10, minimum: 0, maximum: 255, step: 1, sortOrder: 11, unknown2: 1)]
			[DisplayName("Face create geometry data 10")]
			[Description("Face create geometry data 10")]
			[DefaultValue((Byte)0)]
			public Byte FaceGeoData10 {
				get { return faceGeoData10; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceGeoData10.");
					SetProperty(ref faceGeoData10, ref value, FaceGeoData10Property);
				}
			}

			/// <summary>Face create geometry data 11</summary>
			/// <remarks>
			/// Japanese short name: "顔作成ジオメトリデータ11", Google translated: "Face create geometry data 11".
			/// Japanese description: "顔作成ジオメトリデータ11", Google translated: "Face create geometry data 11".
			/// </remarks>
			[ParameterTableRowAttribute("faceGeoData11", index: 11, minimum: 0, maximum: 255, step: 1, sortOrder: 12, unknown2: 1)]
			[DisplayName("Face create geometry data 11")]
			[Description("Face create geometry data 11")]
			[DefaultValue((Byte)0)]
			public Byte FaceGeoData11 {
				get { return faceGeoData11; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceGeoData11.");
					SetProperty(ref faceGeoData11, ref value, FaceGeoData11Property);
				}
			}

			/// <summary>Face create geometry data 12</summary>
			/// <remarks>
			/// Japanese short name: "顔作成ジオメトリデータ12", Google translated: "Face create geometry data 12".
			/// Japanese description: "顔作成ジオメトリデータ12", Google translated: "Face create geometry data 12".
			/// </remarks>
			[ParameterTableRowAttribute("faceGeoData12", index: 12, minimum: 0, maximum: 255, step: 1, sortOrder: 13, unknown2: 1)]
			[DisplayName("Face create geometry data 12")]
			[Description("Face create geometry data 12")]
			[DefaultValue((Byte)0)]
			public Byte FaceGeoData12 {
				get { return faceGeoData12; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceGeoData12.");
					SetProperty(ref faceGeoData12, ref value, FaceGeoData12Property);
				}
			}

			/// <summary>Face create geometry data 13</summary>
			/// <remarks>
			/// Japanese short name: "顔作成ジオメトリデータ13", Google translated: "Face create geometry data 13".
			/// Japanese description: "顔作成ジオメトリデータ13", Google translated: "Face create geometry data 13".
			/// </remarks>
			[ParameterTableRowAttribute("faceGeoData13", index: 13, minimum: 0, maximum: 255, step: 1, sortOrder: 14, unknown2: 1)]
			[DisplayName("Face create geometry data 13")]
			[Description("Face create geometry data 13")]
			[DefaultValue((Byte)0)]
			public Byte FaceGeoData13 {
				get { return faceGeoData13; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceGeoData13.");
					SetProperty(ref faceGeoData13, ref value, FaceGeoData13Property);
				}
			}

			/// <summary>Face create geometry data 14</summary>
			/// <remarks>
			/// Japanese short name: "顔作成ジオメトリデータ14", Google translated: "Face create geometry data 14".
			/// Japanese description: "顔作成ジオメトリデータ14", Google translated: "Face create geometry data 14".
			/// </remarks>
			[ParameterTableRowAttribute("faceGeoData14", index: 14, minimum: 0, maximum: 255, step: 1, sortOrder: 15, unknown2: 1)]
			[DisplayName("Face create geometry data 14")]
			[Description("Face create geometry data 14")]
			[DefaultValue((Byte)0)]
			public Byte FaceGeoData14 {
				get { return faceGeoData14; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceGeoData14.");
					SetProperty(ref faceGeoData14, ref value, FaceGeoData14Property);
				}
			}

			/// <summary>Face create geometry data 15</summary>
			/// <remarks>
			/// Japanese short name: "顔作成ジオメトリデータ15", Google translated: "Face create geometry data 15".
			/// Japanese description: "顔作成ジオメトリデータ15", Google translated: "Face create geometry data 15".
			/// </remarks>
			[ParameterTableRowAttribute("faceGeoData15", index: 15, minimum: 0, maximum: 255, step: 1, sortOrder: 16, unknown2: 1)]
			[DisplayName("Face create geometry data 15")]
			[Description("Face create geometry data 15")]
			[DefaultValue((Byte)0)]
			public Byte FaceGeoData15 {
				get { return faceGeoData15; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceGeoData15.");
					SetProperty(ref faceGeoData15, ref value, FaceGeoData15Property);
				}
			}

			/// <summary>Face create geometry data 16</summary>
			/// <remarks>
			/// Japanese short name: "顔作成ジオメトリデータ16", Google translated: "Face create geometry data 16".
			/// Japanese description: "顔作成ジオメトリデータ16", Google translated: "Face create geometry data 16".
			/// </remarks>
			[ParameterTableRowAttribute("faceGeoData16", index: 16, minimum: 0, maximum: 255, step: 1, sortOrder: 17, unknown2: 1)]
			[DisplayName("Face create geometry data 16")]
			[Description("Face create geometry data 16")]
			[DefaultValue((Byte)0)]
			public Byte FaceGeoData16 {
				get { return faceGeoData16; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceGeoData16.");
					SetProperty(ref faceGeoData16, ref value, FaceGeoData16Property);
				}
			}

			/// <summary>Face create geometry data 17</summary>
			/// <remarks>
			/// Japanese short name: "顔作成ジオメトリデータ17", Google translated: "Face create geometry data 17".
			/// Japanese description: "顔作成ジオメトリデータ17", Google translated: "Face create geometry data 17".
			/// </remarks>
			[ParameterTableRowAttribute("faceGeoData17", index: 17, minimum: 0, maximum: 255, step: 1, sortOrder: 18, unknown2: 1)]
			[DisplayName("Face create geometry data 17")]
			[Description("Face create geometry data 17")]
			[DefaultValue((Byte)0)]
			public Byte FaceGeoData17 {
				get { return faceGeoData17; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceGeoData17.");
					SetProperty(ref faceGeoData17, ref value, FaceGeoData17Property);
				}
			}

			/// <summary>Face create geometry data 18</summary>
			/// <remarks>
			/// Japanese short name: "顔作成ジオメトリデータ18", Google translated: "Face create geometry data 18".
			/// Japanese description: "顔作成ジオメトリデータ18", Google translated: "Face create geometry data 18".
			/// </remarks>
			[ParameterTableRowAttribute("faceGeoData18", index: 18, minimum: 0, maximum: 255, step: 1, sortOrder: 19, unknown2: 1)]
			[DisplayName("Face create geometry data 18")]
			[Description("Face create geometry data 18")]
			[DefaultValue((Byte)0)]
			public Byte FaceGeoData18 {
				get { return faceGeoData18; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceGeoData18.");
					SetProperty(ref faceGeoData18, ref value, FaceGeoData18Property);
				}
			}

			/// <summary>Face create geometry data 19</summary>
			/// <remarks>
			/// Japanese short name: "顔作成ジオメトリデータ19", Google translated: "Face create geometry data 19".
			/// Japanese description: "顔作成ジオメトリデータ19", Google translated: "Face create geometry data 19".
			/// </remarks>
			[ParameterTableRowAttribute("faceGeoData19", index: 19, minimum: 0, maximum: 255, step: 1, sortOrder: 20, unknown2: 1)]
			[DisplayName("Face create geometry data 19")]
			[Description("Face create geometry data 19")]
			[DefaultValue((Byte)0)]
			public Byte FaceGeoData19 {
				get { return faceGeoData19; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceGeoData19.");
					SetProperty(ref faceGeoData19, ref value, FaceGeoData19Property);
				}
			}

			/// <summary>Face create geometry data 20</summary>
			/// <remarks>
			/// Japanese short name: "顔作成ジオメトリデータ20", Google translated: "Face create geometry data 20".
			/// Japanese description: "顔作成ジオメトリデータ20", Google translated: "Face create geometry data 20".
			/// </remarks>
			[ParameterTableRowAttribute("faceGeoData20", index: 20, minimum: 0, maximum: 255, step: 1, sortOrder: 21, unknown2: 1)]
			[DisplayName("Face create geometry data 20")]
			[Description("Face create geometry data 20")]
			[DefaultValue((Byte)0)]
			public Byte FaceGeoData20 {
				get { return faceGeoData20; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceGeoData20.");
					SetProperty(ref faceGeoData20, ref value, FaceGeoData20Property);
				}
			}

			/// <summary>Face create geometry data 21</summary>
			/// <remarks>
			/// Japanese short name: "顔作成ジオメトリデータ21", Google translated: "Face create geometry data 21".
			/// Japanese description: "顔作成ジオメトリデータ21", Google translated: "Face create geometry data 21".
			/// </remarks>
			[ParameterTableRowAttribute("faceGeoData21", index: 21, minimum: 0, maximum: 255, step: 1, sortOrder: 22, unknown2: 1)]
			[DisplayName("Face create geometry data 21")]
			[Description("Face create geometry data 21")]
			[DefaultValue((Byte)0)]
			public Byte FaceGeoData21 {
				get { return faceGeoData21; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceGeoData21.");
					SetProperty(ref faceGeoData21, ref value, FaceGeoData21Property);
				}
			}

			/// <summary>Face create geometry data 22</summary>
			/// <remarks>
			/// Japanese short name: "顔作成ジオメトリデータ22", Google translated: "Face create geometry data 22".
			/// Japanese description: "顔作成ジオメトリデータ22", Google translated: "Face create geometry data 22".
			/// </remarks>
			[ParameterTableRowAttribute("faceGeoData22", index: 22, minimum: 0, maximum: 255, step: 1, sortOrder: 23, unknown2: 1)]
			[DisplayName("Face create geometry data 22")]
			[Description("Face create geometry data 22")]
			[DefaultValue((Byte)0)]
			public Byte FaceGeoData22 {
				get { return faceGeoData22; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceGeoData22.");
					SetProperty(ref faceGeoData22, ref value, FaceGeoData22Property);
				}
			}

			/// <summary>Face create geometry data 23</summary>
			/// <remarks>
			/// Japanese short name: "顔作成ジオメトリデータ23", Google translated: "Face create geometry data 23".
			/// Japanese description: "顔作成ジオメトリデータ23", Google translated: "Face create geometry data 23".
			/// </remarks>
			[ParameterTableRowAttribute("faceGeoData23", index: 23, minimum: 0, maximum: 255, step: 1, sortOrder: 24, unknown2: 1)]
			[DisplayName("Face create geometry data 23")]
			[Description("Face create geometry data 23")]
			[DefaultValue((Byte)0)]
			public Byte FaceGeoData23 {
				get { return faceGeoData23; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceGeoData23.");
					SetProperty(ref faceGeoData23, ref value, FaceGeoData23Property);
				}
			}

			/// <summary>Face create geometry data 24</summary>
			/// <remarks>
			/// Japanese short name: "顔作成ジオメトリデータ24", Google translated: "Face create geometry data 24".
			/// Japanese description: "顔作成ジオメトリデータ24", Google translated: "Face create geometry data 24".
			/// </remarks>
			[ParameterTableRowAttribute("faceGeoData24", index: 24, minimum: 0, maximum: 255, step: 1, sortOrder: 25, unknown2: 1)]
			[DisplayName("Face create geometry data 24")]
			[Description("Face create geometry data 24")]
			[DefaultValue((Byte)0)]
			public Byte FaceGeoData24 {
				get { return faceGeoData24; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceGeoData24.");
					SetProperty(ref faceGeoData24, ref value, FaceGeoData24Property);
				}
			}

			/// <summary>Face create geometry data 25</summary>
			/// <remarks>
			/// Japanese short name: "顔作成ジオメトリデータ25", Google translated: "Face create geometry data 25".
			/// Japanese description: "顔作成ジオメトリデータ25", Google translated: "Face create geometry data 25".
			/// </remarks>
			[ParameterTableRowAttribute("faceGeoData25", index: 25, minimum: 0, maximum: 255, step: 1, sortOrder: 26, unknown2: 1)]
			[DisplayName("Face create geometry data 25")]
			[Description("Face create geometry data 25")]
			[DefaultValue((Byte)0)]
			public Byte FaceGeoData25 {
				get { return faceGeoData25; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceGeoData25.");
					SetProperty(ref faceGeoData25, ref value, FaceGeoData25Property);
				}
			}

			/// <summary>Face create geometry data 26</summary>
			/// <remarks>
			/// Japanese short name: "顔作成ジオメトリデータ26", Google translated: "Face create geometry data 26".
			/// Japanese description: "顔作成ジオメトリデータ26", Google translated: "Face create geometry data 26".
			/// </remarks>
			[ParameterTableRowAttribute("faceGeoData26", index: 26, minimum: 0, maximum: 255, step: 1, sortOrder: 27, unknown2: 1)]
			[DisplayName("Face create geometry data 26")]
			[Description("Face create geometry data 26")]
			[DefaultValue((Byte)0)]
			public Byte FaceGeoData26 {
				get { return faceGeoData26; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceGeoData26.");
					SetProperty(ref faceGeoData26, ref value, FaceGeoData26Property);
				}
			}

			/// <summary>Face create geometry data 27</summary>
			/// <remarks>
			/// Japanese short name: "顔作成ジオメトリデータ27", Google translated: "Face create geometry data 27".
			/// Japanese description: "顔作成ジオメトリデータ27", Google translated: "Face create geometry data 27".
			/// </remarks>
			[ParameterTableRowAttribute("faceGeoData27", index: 27, minimum: 0, maximum: 255, step: 1, sortOrder: 28, unknown2: 1)]
			[DisplayName("Face create geometry data 27")]
			[Description("Face create geometry data 27")]
			[DefaultValue((Byte)0)]
			public Byte FaceGeoData27 {
				get { return faceGeoData27; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceGeoData27.");
					SetProperty(ref faceGeoData27, ref value, FaceGeoData27Property);
				}
			}

			/// <summary>Face create geometry data 28</summary>
			/// <remarks>
			/// Japanese short name: "顔作成ジオメトリデータ28", Google translated: "Face create geometry data 28".
			/// Japanese description: "顔作成ジオメトリデータ28", Google translated: "Face create geometry data 28".
			/// </remarks>
			[ParameterTableRowAttribute("faceGeoData28", index: 28, minimum: 0, maximum: 255, step: 1, sortOrder: 29, unknown2: 1)]
			[DisplayName("Face create geometry data 28")]
			[Description("Face create geometry data 28")]
			[DefaultValue((Byte)0)]
			public Byte FaceGeoData28 {
				get { return faceGeoData28; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceGeoData28.");
					SetProperty(ref faceGeoData28, ref value, FaceGeoData28Property);
				}
			}

			/// <summary>Face create geometry data 29</summary>
			/// <remarks>
			/// Japanese short name: "顔作成ジオメトリデータ29", Google translated: "Face create geometry data 29".
			/// Japanese description: "顔作成ジオメトリデータ29", Google translated: "Face create geometry data 29".
			/// </remarks>
			[ParameterTableRowAttribute("faceGeoData29", index: 29, minimum: 0, maximum: 255, step: 1, sortOrder: 30, unknown2: 1)]
			[DisplayName("Face create geometry data 29")]
			[Description("Face create geometry data 29")]
			[DefaultValue((Byte)0)]
			public Byte FaceGeoData29 {
				get { return faceGeoData29; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceGeoData29.");
					SetProperty(ref faceGeoData29, ref value, FaceGeoData29Property);
				}
			}

			/// <summary>Face create geometry data 30</summary>
			/// <remarks>
			/// Japanese short name: "顔作成ジオメトリデータ30", Google translated: "Face create geometry data 30".
			/// Japanese description: "顔作成ジオメトリデータ30", Google translated: "Face create geometry data 30".
			/// </remarks>
			[ParameterTableRowAttribute("faceGeoData30", index: 30, minimum: 0, maximum: 255, step: 1, sortOrder: 31, unknown2: 1)]
			[DisplayName("Face create geometry data 30")]
			[Description("Face create geometry data 30")]
			[DefaultValue((Byte)0)]
			public Byte FaceGeoData30 {
				get { return faceGeoData30; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceGeoData30.");
					SetProperty(ref faceGeoData30, ref value, FaceGeoData30Property);
				}
			}

			/// <summary>Face create geometry data 31</summary>
			/// <remarks>
			/// Japanese short name: "顔作成ジオメトリデータ31", Google translated: "Face create geometry data 31".
			/// Japanese description: "顔作成ジオメトリデータ31", Google translated: "Face create geometry data 31".
			/// </remarks>
			[ParameterTableRowAttribute("faceGeoData31", index: 31, minimum: 0, maximum: 255, step: 1, sortOrder: 32, unknown2: 1)]
			[DisplayName("Face create geometry data 31")]
			[Description("Face create geometry data 31")]
			[DefaultValue((Byte)0)]
			public Byte FaceGeoData31 {
				get { return faceGeoData31; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceGeoData31.");
					SetProperty(ref faceGeoData31, ref value, FaceGeoData31Property);
				}
			}

			/// <summary>Face create geometry data 32</summary>
			/// <remarks>
			/// Japanese short name: "顔作成ジオメトリデータ32", Google translated: "Face create geometry data 32".
			/// Japanese description: "顔作成ジオメトリデータ32", Google translated: "Face create geometry data 32".
			/// </remarks>
			[ParameterTableRowAttribute("faceGeoData32", index: 32, minimum: 0, maximum: 255, step: 1, sortOrder: 33, unknown2: 1)]
			[DisplayName("Face create geometry data 32")]
			[Description("Face create geometry data 32")]
			[DefaultValue((Byte)0)]
			public Byte FaceGeoData32 {
				get { return faceGeoData32; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceGeoData32.");
					SetProperty(ref faceGeoData32, ref value, FaceGeoData32Property);
				}
			}

			/// <summary>Face create geometry data 33</summary>
			/// <remarks>
			/// Japanese short name: "顔作成ジオメトリデータ33", Google translated: "Face create geometry data 33".
			/// Japanese description: "顔作成ジオメトリデータ33", Google translated: "Face create geometry data 33".
			/// </remarks>
			[ParameterTableRowAttribute("faceGeoData33", index: 33, minimum: 0, maximum: 255, step: 1, sortOrder: 34, unknown2: 1)]
			[DisplayName("Face create geometry data 33")]
			[Description("Face create geometry data 33")]
			[DefaultValue((Byte)0)]
			public Byte FaceGeoData33 {
				get { return faceGeoData33; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceGeoData33.");
					SetProperty(ref faceGeoData33, ref value, FaceGeoData33Property);
				}
			}

			/// <summary>Face create geometry data 34</summary>
			/// <remarks>
			/// Japanese short name: "顔作成ジオメトリデータ34", Google translated: "Face create geometry data 34".
			/// Japanese description: "顔作成ジオメトリデータ34", Google translated: "Face create geometry data 34".
			/// </remarks>
			[ParameterTableRowAttribute("faceGeoData34", index: 34, minimum: 0, maximum: 255, step: 1, sortOrder: 35, unknown2: 1)]
			[DisplayName("Face create geometry data 34")]
			[Description("Face create geometry data 34")]
			[DefaultValue((Byte)0)]
			public Byte FaceGeoData34 {
				get { return faceGeoData34; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceGeoData34.");
					SetProperty(ref faceGeoData34, ref value, FaceGeoData34Property);
				}
			}

			/// <summary>Face create geometry data 35</summary>
			/// <remarks>
			/// Japanese short name: "顔作成ジオメトリデータ35", Google translated: "Face create geometry data 35".
			/// Japanese description: "顔作成ジオメトリデータ35", Google translated: "Face create geometry data 35".
			/// </remarks>
			[ParameterTableRowAttribute("faceGeoData35", index: 35, minimum: 0, maximum: 255, step: 1, sortOrder: 36, unknown2: 1)]
			[DisplayName("Face create geometry data 35")]
			[Description("Face create geometry data 35")]
			[DefaultValue((Byte)0)]
			public Byte FaceGeoData35 {
				get { return faceGeoData35; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceGeoData35.");
					SetProperty(ref faceGeoData35, ref value, FaceGeoData35Property);
				}
			}

			/// <summary>Face create geometry data 36</summary>
			/// <remarks>
			/// Japanese short name: "顔作成ジオメトリデータ36", Google translated: "Face create geometry data 36".
			/// Japanese description: "顔作成ジオメトリデータ36", Google translated: "Face create geometry data 36".
			/// </remarks>
			[ParameterTableRowAttribute("faceGeoData36", index: 36, minimum: 0, maximum: 255, step: 1, sortOrder: 37, unknown2: 1)]
			[DisplayName("Face create geometry data 36")]
			[Description("Face create geometry data 36")]
			[DefaultValue((Byte)0)]
			public Byte FaceGeoData36 {
				get { return faceGeoData36; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceGeoData36.");
					SetProperty(ref faceGeoData36, ref value, FaceGeoData36Property);
				}
			}

			/// <summary>Face create geometry data 37</summary>
			/// <remarks>
			/// Japanese short name: "顔作成ジオメトリデータ37", Google translated: "Face create geometry data 37".
			/// Japanese description: "顔作成ジオメトリデータ37", Google translated: "Face create geometry data 37".
			/// </remarks>
			[ParameterTableRowAttribute("faceGeoData37", index: 37, minimum: 0, maximum: 255, step: 1, sortOrder: 38, unknown2: 1)]
			[DisplayName("Face create geometry data 37")]
			[Description("Face create geometry data 37")]
			[DefaultValue((Byte)0)]
			public Byte FaceGeoData37 {
				get { return faceGeoData37; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceGeoData37.");
					SetProperty(ref faceGeoData37, ref value, FaceGeoData37Property);
				}
			}

			/// <summary>Face create geometry data 38</summary>
			/// <remarks>
			/// Japanese short name: "顔作成ジオメトリデータ38", Google translated: "Face create geometry data 38".
			/// Japanese description: "顔作成ジオメトリデータ38", Google translated: "Face create geometry data 38".
			/// </remarks>
			[ParameterTableRowAttribute("faceGeoData38", index: 38, minimum: 0, maximum: 255, step: 1, sortOrder: 39, unknown2: 1)]
			[DisplayName("Face create geometry data 38")]
			[Description("Face create geometry data 38")]
			[DefaultValue((Byte)0)]
			public Byte FaceGeoData38 {
				get { return faceGeoData38; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceGeoData38.");
					SetProperty(ref faceGeoData38, ref value, FaceGeoData38Property);
				}
			}

			/// <summary>Face create geometry data 39</summary>
			/// <remarks>
			/// Japanese short name: "顔作成ジオメトリデータ39", Google translated: "Face create geometry data 39".
			/// Japanese description: "顔作成ジオメトリデータ39", Google translated: "Face create geometry data 39".
			/// </remarks>
			[ParameterTableRowAttribute("faceGeoData39", index: 39, minimum: 0, maximum: 255, step: 1, sortOrder: 40, unknown2: 1)]
			[DisplayName("Face create geometry data 39")]
			[Description("Face create geometry data 39")]
			[DefaultValue((Byte)0)]
			public Byte FaceGeoData39 {
				get { return faceGeoData39; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceGeoData39.");
					SetProperty(ref faceGeoData39, ref value, FaceGeoData39Property);
				}
			}

			/// <summary>Face create geometry data 40</summary>
			/// <remarks>
			/// Japanese short name: "顔作成ジオメトリデータ40", Google translated: "Face create geometry data 40".
			/// Japanese description: "顔作成ジオメトリデータ40", Google translated: "Face create geometry data 40".
			/// </remarks>
			[ParameterTableRowAttribute("faceGeoData40", index: 40, minimum: 0, maximum: 255, step: 1, sortOrder: 41, unknown2: 1)]
			[DisplayName("Face create geometry data 40")]
			[Description("Face create geometry data 40")]
			[DefaultValue((Byte)0)]
			public Byte FaceGeoData40 {
				get { return faceGeoData40; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceGeoData40.");
					SetProperty(ref faceGeoData40, ref value, FaceGeoData40Property);
				}
			}

			/// <summary>Face create geometry data 41</summary>
			/// <remarks>
			/// Japanese short name: "顔作成ジオメトリデータ41", Google translated: "Face create geometry data 41".
			/// Japanese description: "顔作成ジオメトリデータ41", Google translated: "Face create geometry data 41".
			/// </remarks>
			[ParameterTableRowAttribute("faceGeoData41", index: 41, minimum: 0, maximum: 255, step: 1, sortOrder: 42, unknown2: 1)]
			[DisplayName("Face create geometry data 41")]
			[Description("Face create geometry data 41")]
			[DefaultValue((Byte)0)]
			public Byte FaceGeoData41 {
				get { return faceGeoData41; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceGeoData41.");
					SetProperty(ref faceGeoData41, ref value, FaceGeoData41Property);
				}
			}

			/// <summary>Face create geometry data 42</summary>
			/// <remarks>
			/// Japanese short name: "顔作成ジオメトリデータ42", Google translated: "Face create geometry data 42".
			/// Japanese description: "顔作成ジオメトリデータ42", Google translated: "Face create geometry data 42".
			/// </remarks>
			[ParameterTableRowAttribute("faceGeoData42", index: 42, minimum: 0, maximum: 255, step: 1, sortOrder: 43, unknown2: 1)]
			[DisplayName("Face create geometry data 42")]
			[Description("Face create geometry data 42")]
			[DefaultValue((Byte)0)]
			public Byte FaceGeoData42 {
				get { return faceGeoData42; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceGeoData42.");
					SetProperty(ref faceGeoData42, ref value, FaceGeoData42Property);
				}
			}

			/// <summary>Face create geometry data 43</summary>
			/// <remarks>
			/// Japanese short name: "顔作成ジオメトリデータ43", Google translated: "Face create geometry data 43".
			/// Japanese description: "顔作成ジオメトリデータ43", Google translated: "Face create geometry data 43".
			/// </remarks>
			[ParameterTableRowAttribute("faceGeoData43", index: 43, minimum: 0, maximum: 255, step: 1, sortOrder: 44, unknown2: 1)]
			[DisplayName("Face create geometry data 43")]
			[Description("Face create geometry data 43")]
			[DefaultValue((Byte)0)]
			public Byte FaceGeoData43 {
				get { return faceGeoData43; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceGeoData43.");
					SetProperty(ref faceGeoData43, ref value, FaceGeoData43Property);
				}
			}

			/// <summary>Face create geometry data 44</summary>
			/// <remarks>
			/// Japanese short name: "顔作成ジオメトリデータ44", Google translated: "Face create geometry data 44".
			/// Japanese description: "顔作成ジオメトリデータ44", Google translated: "Face create geometry data 44".
			/// </remarks>
			[ParameterTableRowAttribute("faceGeoData44", index: 44, minimum: 0, maximum: 255, step: 1, sortOrder: 45, unknown2: 1)]
			[DisplayName("Face create geometry data 44")]
			[Description("Face create geometry data 44")]
			[DefaultValue((Byte)0)]
			public Byte FaceGeoData44 {
				get { return faceGeoData44; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceGeoData44.");
					SetProperty(ref faceGeoData44, ref value, FaceGeoData44Property);
				}
			}

			/// <summary>Face create geometry data 45</summary>
			/// <remarks>
			/// Japanese short name: "顔作成ジオメトリデータ45", Google translated: "Face create geometry data 45".
			/// Japanese description: "顔作成ジオメトリデータ45", Google translated: "Face create geometry data 45".
			/// </remarks>
			[ParameterTableRowAttribute("faceGeoData45", index: 45, minimum: 0, maximum: 255, step: 1, sortOrder: 46, unknown2: 1)]
			[DisplayName("Face create geometry data 45")]
			[Description("Face create geometry data 45")]
			[DefaultValue((Byte)0)]
			public Byte FaceGeoData45 {
				get { return faceGeoData45; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceGeoData45.");
					SetProperty(ref faceGeoData45, ref value, FaceGeoData45Property);
				}
			}

			/// <summary>Face create geometry data 46</summary>
			/// <remarks>
			/// Japanese short name: "顔作成ジオメトリデータ46", Google translated: "Face create geometry data 46".
			/// Japanese description: "顔作成ジオメトリデータ46", Google translated: "Face create geometry data 46".
			/// </remarks>
			[ParameterTableRowAttribute("faceGeoData46", index: 46, minimum: 0, maximum: 255, step: 1, sortOrder: 47, unknown2: 1)]
			[DisplayName("Face create geometry data 46")]
			[Description("Face create geometry data 46")]
			[DefaultValue((Byte)0)]
			public Byte FaceGeoData46 {
				get { return faceGeoData46; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceGeoData46.");
					SetProperty(ref faceGeoData46, ref value, FaceGeoData46Property);
				}
			}

			/// <summary>Face create geometry data 47</summary>
			/// <remarks>
			/// Japanese short name: "顔作成ジオメトリデータ47", Google translated: "Face create geometry data 47".
			/// Japanese description: "顔作成ジオメトリデータ47", Google translated: "Face create geometry data 47".
			/// </remarks>
			[ParameterTableRowAttribute("faceGeoData47", index: 47, minimum: 0, maximum: 255, step: 1, sortOrder: 48, unknown2: 1)]
			[DisplayName("Face create geometry data 47")]
			[Description("Face create geometry data 47")]
			[DefaultValue((Byte)0)]
			public Byte FaceGeoData47 {
				get { return faceGeoData47; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceGeoData47.");
					SetProperty(ref faceGeoData47, ref value, FaceGeoData47Property);
				}
			}

			/// <summary>Face create geometry data 48</summary>
			/// <remarks>
			/// Japanese short name: "顔作成ジオメトリデータ48", Google translated: "Face create geometry data 48".
			/// Japanese description: "顔作成ジオメトリデータ48", Google translated: "Face create geometry data 48".
			/// </remarks>
			[ParameterTableRowAttribute("faceGeoData48", index: 48, minimum: 0, maximum: 255, step: 1, sortOrder: 49, unknown2: 1)]
			[DisplayName("Face create geometry data 48")]
			[Description("Face create geometry data 48")]
			[DefaultValue((Byte)0)]
			public Byte FaceGeoData48 {
				get { return faceGeoData48; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceGeoData48.");
					SetProperty(ref faceGeoData48, ref value, FaceGeoData48Property);
				}
			}

			/// <summary>Face create geometry data 49</summary>
			/// <remarks>
			/// Japanese short name: "顔作成ジオメトリデータ49", Google translated: "Face create geometry data 49".
			/// Japanese description: "顔作成ジオメトリデータ49", Google translated: "Face create geometry data 49".
			/// </remarks>
			[ParameterTableRowAttribute("faceGeoData49", index: 49, minimum: 0, maximum: 255, step: 1, sortOrder: 50, unknown2: 1)]
			[DisplayName("Face create geometry data 49")]
			[Description("Face create geometry data 49")]
			[DefaultValue((Byte)0)]
			public Byte FaceGeoData49 {
				get { return faceGeoData49; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceGeoData49.");
					SetProperty(ref faceGeoData49, ref value, FaceGeoData49Property);
				}
			}

			/// <summary>Face create texture data 00</summary>
			/// <remarks>
			/// Japanese short name: "顔作成テクスチャデータ00", Google translated: "Face create texture data 00".
			/// Japanese description: "顔作成テクスチャデータ00", Google translated: "Face create texture data 00".
			/// </remarks>
			[ParameterTableRowAttribute("faceTexData00", index: 50, minimum: 0, maximum: 255, step: 1, sortOrder: 51, unknown2: 1)]
			[DisplayName("Face create texture data 00")]
			[Description("Face create texture data 00")]
			[DefaultValue((Byte)0)]
			public Byte FaceTexData00 {
				get { return faceTexData00; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceTexData00.");
					SetProperty(ref faceTexData00, ref value, FaceTexData00Property);
				}
			}

			/// <summary>Face create texture data 01</summary>
			/// <remarks>
			/// Japanese short name: "顔作成テクスチャデータ01", Google translated: "Face create texture data 01".
			/// Japanese description: "顔作成テクスチャデータ01", Google translated: "Face create texture data 01".
			/// </remarks>
			[ParameterTableRowAttribute("faceTexData01", index: 51, minimum: 0, maximum: 255, step: 1, sortOrder: 52, unknown2: 1)]
			[DisplayName("Face create texture data 01")]
			[Description("Face create texture data 01")]
			[DefaultValue((Byte)0)]
			public Byte FaceTexData01 {
				get { return faceTexData01; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceTexData01.");
					SetProperty(ref faceTexData01, ref value, FaceTexData01Property);
				}
			}

			/// <summary>Face create texture data 02</summary>
			/// <remarks>
			/// Japanese short name: "顔作成テクスチャデータ02", Google translated: "Face create texture data 02".
			/// Japanese description: "顔作成テクスチャデータ02", Google translated: "Face create texture data 02".
			/// </remarks>
			[ParameterTableRowAttribute("faceTexData02", index: 52, minimum: 0, maximum: 255, step: 1, sortOrder: 53, unknown2: 1)]
			[DisplayName("Face create texture data 02")]
			[Description("Face create texture data 02")]
			[DefaultValue((Byte)0)]
			public Byte FaceTexData02 {
				get { return faceTexData02; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceTexData02.");
					SetProperty(ref faceTexData02, ref value, FaceTexData02Property);
				}
			}

			/// <summary>Face create texture data 03</summary>
			/// <remarks>
			/// Japanese short name: "顔作成テクスチャデータ03", Google translated: "Face create texture data 03".
			/// Japanese description: "顔作成テクスチャデータ03", Google translated: "Face create texture data 03".
			/// </remarks>
			[ParameterTableRowAttribute("faceTexData03", index: 53, minimum: 0, maximum: 255, step: 1, sortOrder: 54, unknown2: 1)]
			[DisplayName("Face create texture data 03")]
			[Description("Face create texture data 03")]
			[DefaultValue((Byte)0)]
			public Byte FaceTexData03 {
				get { return faceTexData03; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceTexData03.");
					SetProperty(ref faceTexData03, ref value, FaceTexData03Property);
				}
			}

			/// <summary>Face create texture data 04</summary>
			/// <remarks>
			/// Japanese short name: "顔作成テクスチャデータ04", Google translated: "Face create texture data 04".
			/// Japanese description: "顔作成テクスチャデータ04", Google translated: "Face create texture data 04".
			/// </remarks>
			[ParameterTableRowAttribute("faceTexData04", index: 54, minimum: 0, maximum: 255, step: 1, sortOrder: 55, unknown2: 1)]
			[DisplayName("Face create texture data 04")]
			[Description("Face create texture data 04")]
			[DefaultValue((Byte)0)]
			public Byte FaceTexData04 {
				get { return faceTexData04; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceTexData04.");
					SetProperty(ref faceTexData04, ref value, FaceTexData04Property);
				}
			}

			/// <summary>Face create texture data 05</summary>
			/// <remarks>
			/// Japanese short name: "顔作成テクスチャデータ05", Google translated: "Face create texture data 05".
			/// Japanese description: "顔作成テクスチャデータ05", Google translated: "Face create texture data 05".
			/// </remarks>
			[ParameterTableRowAttribute("faceTexData05", index: 55, minimum: 0, maximum: 255, step: 1, sortOrder: 56, unknown2: 1)]
			[DisplayName("Face create texture data 05")]
			[Description("Face create texture data 05")]
			[DefaultValue((Byte)0)]
			public Byte FaceTexData05 {
				get { return faceTexData05; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceTexData05.");
					SetProperty(ref faceTexData05, ref value, FaceTexData05Property);
				}
			}

			/// <summary>Face create texture data 06</summary>
			/// <remarks>
			/// Japanese short name: "顔作成テクスチャデータ06", Google translated: "Face create texture data 06".
			/// Japanese description: "顔作成テクスチャデータ06", Google translated: "Face create texture data 06".
			/// </remarks>
			[ParameterTableRowAttribute("faceTexData06", index: 56, minimum: 0, maximum: 255, step: 1, sortOrder: 57, unknown2: 1)]
			[DisplayName("Face create texture data 06")]
			[Description("Face create texture data 06")]
			[DefaultValue((Byte)0)]
			public Byte FaceTexData06 {
				get { return faceTexData06; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceTexData06.");
					SetProperty(ref faceTexData06, ref value, FaceTexData06Property);
				}
			}

			/// <summary>Face create texture data 07</summary>
			/// <remarks>
			/// Japanese short name: "顔作成テクスチャデータ07", Google translated: "Face create texture data 07".
			/// Japanese description: "顔作成テクスチャデータ07", Google translated: "Face create texture data 07".
			/// </remarks>
			[ParameterTableRowAttribute("faceTexData07", index: 57, minimum: 0, maximum: 255, step: 1, sortOrder: 58, unknown2: 1)]
			[DisplayName("Face create texture data 07")]
			[Description("Face create texture data 07")]
			[DefaultValue((Byte)0)]
			public Byte FaceTexData07 {
				get { return faceTexData07; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceTexData07.");
					SetProperty(ref faceTexData07, ref value, FaceTexData07Property);
				}
			}

			/// <summary>Face create texture data 08</summary>
			/// <remarks>
			/// Japanese short name: "顔作成テクスチャデータ08", Google translated: "Face create texture data 08".
			/// Japanese description: "顔作成テクスチャデータ08", Google translated: "Face create texture data 08".
			/// </remarks>
			[ParameterTableRowAttribute("faceTexData08", index: 58, minimum: 0, maximum: 255, step: 1, sortOrder: 59, unknown2: 1)]
			[DisplayName("Face create texture data 08")]
			[Description("Face create texture data 08")]
			[DefaultValue((Byte)0)]
			public Byte FaceTexData08 {
				get { return faceTexData08; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceTexData08.");
					SetProperty(ref faceTexData08, ref value, FaceTexData08Property);
				}
			}

			/// <summary>Face create texture data 09</summary>
			/// <remarks>
			/// Japanese short name: "顔作成テクスチャデータ09", Google translated: "Face create texture data 09".
			/// Japanese description: "顔作成テクスチャデータ09", Google translated: "Face create texture data 09".
			/// </remarks>
			[ParameterTableRowAttribute("faceTexData09", index: 59, minimum: 0, maximum: 255, step: 1, sortOrder: 60, unknown2: 1)]
			[DisplayName("Face create texture data 09")]
			[Description("Face create texture data 09")]
			[DefaultValue((Byte)0)]
			public Byte FaceTexData09 {
				get { return faceTexData09; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceTexData09.");
					SetProperty(ref faceTexData09, ref value, FaceTexData09Property);
				}
			}

			/// <summary>Face create texture data 10</summary>
			/// <remarks>
			/// Japanese short name: "顔作成テクスチャデータ10", Google translated: "Face create texture data 10".
			/// Japanese description: "顔作成テクスチャデータ10", Google translated: "Face create texture data 10".
			/// </remarks>
			[ParameterTableRowAttribute("faceTexData10", index: 60, minimum: 0, maximum: 255, step: 1, sortOrder: 61, unknown2: 1)]
			[DisplayName("Face create texture data 10")]
			[Description("Face create texture data 10")]
			[DefaultValue((Byte)0)]
			public Byte FaceTexData10 {
				get { return faceTexData10; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceTexData10.");
					SetProperty(ref faceTexData10, ref value, FaceTexData10Property);
				}
			}

			/// <summary>Face create texture data 11</summary>
			/// <remarks>
			/// Japanese short name: "顔作成テクスチャデータ11", Google translated: "Face create texture data 11".
			/// Japanese description: "顔作成テクスチャデータ11", Google translated: "Face create texture data 11".
			/// </remarks>
			[ParameterTableRowAttribute("faceTexData11", index: 61, minimum: 0, maximum: 255, step: 1, sortOrder: 62, unknown2: 1)]
			[DisplayName("Face create texture data 11")]
			[Description("Face create texture data 11")]
			[DefaultValue((Byte)0)]
			public Byte FaceTexData11 {
				get { return faceTexData11; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceTexData11.");
					SetProperty(ref faceTexData11, ref value, FaceTexData11Property);
				}
			}

			/// <summary>Face create texture data 12</summary>
			/// <remarks>
			/// Japanese short name: "顔作成テクスチャデータ12", Google translated: "Face create texture data 12".
			/// Japanese description: "顔作成テクスチャデータ12", Google translated: "Face create texture data 12".
			/// </remarks>
			[ParameterTableRowAttribute("faceTexData12", index: 62, minimum: 0, maximum: 255, step: 1, sortOrder: 63, unknown2: 1)]
			[DisplayName("Face create texture data 12")]
			[Description("Face create texture data 12")]
			[DefaultValue((Byte)0)]
			public Byte FaceTexData12 {
				get { return faceTexData12; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceTexData12.");
					SetProperty(ref faceTexData12, ref value, FaceTexData12Property);
				}
			}

			/// <summary>Face create texture data 13</summary>
			/// <remarks>
			/// Japanese short name: "顔作成テクスチャデータ13", Google translated: "Face create texture data 13".
			/// Japanese description: "顔作成テクスチャデータ13", Google translated: "Face create texture data 13".
			/// </remarks>
			[ParameterTableRowAttribute("faceTexData13", index: 63, minimum: 0, maximum: 255, step: 1, sortOrder: 64, unknown2: 1)]
			[DisplayName("Face create texture data 13")]
			[Description("Face create texture data 13")]
			[DefaultValue((Byte)0)]
			public Byte FaceTexData13 {
				get { return faceTexData13; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceTexData13.");
					SetProperty(ref faceTexData13, ref value, FaceTexData13Property);
				}
			}

			/// <summary>Face create texture data 14</summary>
			/// <remarks>
			/// Japanese short name: "顔作成テクスチャデータ14", Google translated: "Face create texture data 14".
			/// Japanese description: "顔作成テクスチャデータ14", Google translated: "Face create texture data 14".
			/// </remarks>
			[ParameterTableRowAttribute("faceTexData14", index: 64, minimum: 0, maximum: 255, step: 1, sortOrder: 65, unknown2: 1)]
			[DisplayName("Face create texture data 14")]
			[Description("Face create texture data 14")]
			[DefaultValue((Byte)0)]
			public Byte FaceTexData14 {
				get { return faceTexData14; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceTexData14.");
					SetProperty(ref faceTexData14, ref value, FaceTexData14Property);
				}
			}

			/// <summary>Face create texture data 15</summary>
			/// <remarks>
			/// Japanese short name: "顔作成テクスチャデータ15", Google translated: "Face create texture data 15".
			/// Japanese description: "顔作成テクスチャデータ15", Google translated: "Face create texture data 15".
			/// </remarks>
			[ParameterTableRowAttribute("faceTexData15", index: 65, minimum: 0, maximum: 255, step: 1, sortOrder: 66, unknown2: 1)]
			[DisplayName("Face create texture data 15")]
			[Description("Face create texture data 15")]
			[DefaultValue((Byte)0)]
			public Byte FaceTexData15 {
				get { return faceTexData15; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceTexData15.");
					SetProperty(ref faceTexData15, ref value, FaceTexData15Property);
				}
			}

			/// <summary>Face create texture data 16</summary>
			/// <remarks>
			/// Japanese short name: "顔作成テクスチャデータ16", Google translated: "Face create texture data 16".
			/// Japanese description: "顔作成テクスチャデータ16", Google translated: "Face create texture data 16".
			/// </remarks>
			[ParameterTableRowAttribute("faceTexData16", index: 66, minimum: 0, maximum: 255, step: 1, sortOrder: 67, unknown2: 1)]
			[DisplayName("Face create texture data 16")]
			[Description("Face create texture data 16")]
			[DefaultValue((Byte)0)]
			public Byte FaceTexData16 {
				get { return faceTexData16; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceTexData16.");
					SetProperty(ref faceTexData16, ref value, FaceTexData16Property);
				}
			}

			/// <summary>Face create texture data 17</summary>
			/// <remarks>
			/// Japanese short name: "顔作成テクスチャデータ17", Google translated: "Face create texture data 17".
			/// Japanese description: "顔作成テクスチャデータ17", Google translated: "Face create texture data 17".
			/// </remarks>
			[ParameterTableRowAttribute("faceTexData17", index: 67, minimum: 0, maximum: 255, step: 1, sortOrder: 68, unknown2: 1)]
			[DisplayName("Face create texture data 17")]
			[Description("Face create texture data 17")]
			[DefaultValue((Byte)0)]
			public Byte FaceTexData17 {
				get { return faceTexData17; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceTexData17.");
					SetProperty(ref faceTexData17, ref value, FaceTexData17Property);
				}
			}

			/// <summary>Face create texture data 18</summary>
			/// <remarks>
			/// Japanese short name: "顔作成テクスチャデータ18", Google translated: "Face create texture data 18".
			/// Japanese description: "顔作成テクスチャデータ18", Google translated: "Face create texture data 18".
			/// </remarks>
			[ParameterTableRowAttribute("faceTexData18", index: 68, minimum: 0, maximum: 255, step: 1, sortOrder: 69, unknown2: 1)]
			[DisplayName("Face create texture data 18")]
			[Description("Face create texture data 18")]
			[DefaultValue((Byte)0)]
			public Byte FaceTexData18 {
				get { return faceTexData18; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceTexData18.");
					SetProperty(ref faceTexData18, ref value, FaceTexData18Property);
				}
			}

			/// <summary>Face create texture data 19</summary>
			/// <remarks>
			/// Japanese short name: "顔作成テクスチャデータ19", Google translated: "Face create texture data 19".
			/// Japanese description: "顔作成テクスチャデータ19", Google translated: "Face create texture data 19".
			/// </remarks>
			[ParameterTableRowAttribute("faceTexData19", index: 69, minimum: 0, maximum: 255, step: 1, sortOrder: 70, unknown2: 1)]
			[DisplayName("Face create texture data 19")]
			[Description("Face create texture data 19")]
			[DefaultValue((Byte)0)]
			public Byte FaceTexData19 {
				get { return faceTexData19; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceTexData19.");
					SetProperty(ref faceTexData19, ref value, FaceTexData19Property);
				}
			}

			/// <summary>Face create texture data 20</summary>
			/// <remarks>
			/// Japanese short name: "顔作成テクスチャデータ20", Google translated: "Face create texture data 20".
			/// Japanese description: "顔作成テクスチャデータ20", Google translated: "Face create texture data 20".
			/// </remarks>
			[ParameterTableRowAttribute("faceTexData20", index: 70, minimum: 0, maximum: 255, step: 1, sortOrder: 71, unknown2: 1)]
			[DisplayName("Face create texture data 20")]
			[Description("Face create texture data 20")]
			[DefaultValue((Byte)0)]
			public Byte FaceTexData20 {
				get { return faceTexData20; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceTexData20.");
					SetProperty(ref faceTexData20, ref value, FaceTexData20Property);
				}
			}

			/// <summary>Face create texture data 21</summary>
			/// <remarks>
			/// Japanese short name: "顔作成テクスチャデータ21", Google translated: "Face create texture data 21".
			/// Japanese description: "顔作成テクスチャデータ21", Google translated: "Face create texture data 21".
			/// </remarks>
			[ParameterTableRowAttribute("faceTexData21", index: 71, minimum: 0, maximum: 255, step: 1, sortOrder: 72, unknown2: 1)]
			[DisplayName("Face create texture data 21")]
			[Description("Face create texture data 21")]
			[DefaultValue((Byte)0)]
			public Byte FaceTexData21 {
				get { return faceTexData21; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceTexData21.");
					SetProperty(ref faceTexData21, ref value, FaceTexData21Property);
				}
			}

			/// <summary>Face create texture data 22</summary>
			/// <remarks>
			/// Japanese short name: "顔作成テクスチャデータ22", Google translated: "Face create texture data 22".
			/// Japanese description: "顔作成テクスチャデータ22", Google translated: "Face create texture data 22".
			/// </remarks>
			[ParameterTableRowAttribute("faceTexData22", index: 72, minimum: 0, maximum: 255, step: 1, sortOrder: 73, unknown2: 1)]
			[DisplayName("Face create texture data 22")]
			[Description("Face create texture data 22")]
			[DefaultValue((Byte)0)]
			public Byte FaceTexData22 {
				get { return faceTexData22; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceTexData22.");
					SetProperty(ref faceTexData22, ref value, FaceTexData22Property);
				}
			}

			/// <summary>Face create texture data 23</summary>
			/// <remarks>
			/// Japanese short name: "顔作成テクスチャデータ23", Google translated: "Face create texture data 23".
			/// Japanese description: "顔作成テクスチャデータ23", Google translated: "Face create texture data 23".
			/// </remarks>
			[ParameterTableRowAttribute("faceTexData23", index: 73, minimum: 0, maximum: 255, step: 1, sortOrder: 74, unknown2: 1)]
			[DisplayName("Face create texture data 23")]
			[Description("Face create texture data 23")]
			[DefaultValue((Byte)0)]
			public Byte FaceTexData23 {
				get { return faceTexData23; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceTexData23.");
					SetProperty(ref faceTexData23, ref value, FaceTexData23Property);
				}
			}

			/// <summary>Face create texture data 24</summary>
			/// <remarks>
			/// Japanese short name: "顔作成テクスチャデータ24", Google translated: "Face create texture data 24".
			/// Japanese description: "顔作成テクスチャデータ24", Google translated: "Face create texture data 24".
			/// </remarks>
			[ParameterTableRowAttribute("faceTexData24", index: 74, minimum: 0, maximum: 255, step: 1, sortOrder: 75, unknown2: 1)]
			[DisplayName("Face create texture data 24")]
			[Description("Face create texture data 24")]
			[DefaultValue((Byte)0)]
			public Byte FaceTexData24 {
				get { return faceTexData24; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceTexData24.");
					SetProperty(ref faceTexData24, ref value, FaceTexData24Property);
				}
			}

			/// <summary>Face create texture data 25</summary>
			/// <remarks>
			/// Japanese short name: "顔作成テクスチャデータ25", Google translated: "Face create texture data 25".
			/// Japanese description: "顔作成テクスチャデータ25", Google translated: "Face create texture data 25".
			/// </remarks>
			[ParameterTableRowAttribute("faceTexData25", index: 75, minimum: 0, maximum: 255, step: 1, sortOrder: 76, unknown2: 1)]
			[DisplayName("Face create texture data 25")]
			[Description("Face create texture data 25")]
			[DefaultValue((Byte)0)]
			public Byte FaceTexData25 {
				get { return faceTexData25; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceTexData25.");
					SetProperty(ref faceTexData25, ref value, FaceTexData25Property);
				}
			}

			/// <summary>Face create texture data 26</summary>
			/// <remarks>
			/// Japanese short name: "顔作成テクスチャデータ26", Google translated: "Face create texture data 26".
			/// Japanese description: "顔作成テクスチャデータ26", Google translated: "Face create texture data 26".
			/// </remarks>
			[ParameterTableRowAttribute("faceTexData26", index: 76, minimum: 0, maximum: 255, step: 1, sortOrder: 77, unknown2: 1)]
			[DisplayName("Face create texture data 26")]
			[Description("Face create texture data 26")]
			[DefaultValue((Byte)0)]
			public Byte FaceTexData26 {
				get { return faceTexData26; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceTexData26.");
					SetProperty(ref faceTexData26, ref value, FaceTexData26Property);
				}
			}

			/// <summary>Face create texture data 27</summary>
			/// <remarks>
			/// Japanese short name: "顔作成テクスチャデータ27", Google translated: "Face create texture data 27".
			/// Japanese description: "顔作成テクスチャデータ27", Google translated: "Face create texture data 27".
			/// </remarks>
			[ParameterTableRowAttribute("faceTexData27", index: 77, minimum: 0, maximum: 255, step: 1, sortOrder: 78, unknown2: 1)]
			[DisplayName("Face create texture data 27")]
			[Description("Face create texture data 27")]
			[DefaultValue((Byte)0)]
			public Byte FaceTexData27 {
				get { return faceTexData27; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceTexData27.");
					SetProperty(ref faceTexData27, ref value, FaceTexData27Property);
				}
			}

			/// <summary>Face create texture data 28</summary>
			/// <remarks>
			/// Japanese short name: "顔作成テクスチャデータ28", Google translated: "Face create texture data 28".
			/// Japanese description: "顔作成テクスチャデータ28", Google translated: "Face create texture data 28".
			/// </remarks>
			[ParameterTableRowAttribute("faceTexData28", index: 78, minimum: 0, maximum: 255, step: 1, sortOrder: 79, unknown2: 1)]
			[DisplayName("Face create texture data 28")]
			[Description("Face create texture data 28")]
			[DefaultValue((Byte)0)]
			public Byte FaceTexData28 {
				get { return faceTexData28; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceTexData28.");
					SetProperty(ref faceTexData28, ref value, FaceTexData28Property);
				}
			}

			/// <summary>Face create texture data 29</summary>
			/// <remarks>
			/// Japanese short name: "顔作成テクスチャデータ29", Google translated: "Face create texture data 29".
			/// Japanese description: "顔作成テクスチャデータ29", Google translated: "Face create texture data 29".
			/// </remarks>
			[ParameterTableRowAttribute("faceTexData29", index: 79, minimum: 0, maximum: 255, step: 1, sortOrder: 80, unknown2: 1)]
			[DisplayName("Face create texture data 29")]
			[Description("Face create texture data 29")]
			[DefaultValue((Byte)0)]
			public Byte FaceTexData29 {
				get { return faceTexData29; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceTexData29.");
					SetProperty(ref faceTexData29, ref value, FaceTexData29Property);
				}
			}

			/// <summary>Face create texture data 30</summary>
			/// <remarks>
			/// Japanese short name: "顔作成テクスチャデータ30", Google translated: "Face create texture data 30".
			/// Japanese description: "顔作成テクスチャデータ30", Google translated: "Face create texture data 30".
			/// </remarks>
			[ParameterTableRowAttribute("faceTexData30", index: 80, minimum: 0, maximum: 255, step: 1, sortOrder: 81, unknown2: 1)]
			[DisplayName("Face create texture data 30")]
			[Description("Face create texture data 30")]
			[DefaultValue((Byte)0)]
			public Byte FaceTexData30 {
				get { return faceTexData30; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceTexData30.");
					SetProperty(ref faceTexData30, ref value, FaceTexData30Property);
				}
			}

			/// <summary>Face create texture data 31</summary>
			/// <remarks>
			/// Japanese short name: "顔作成テクスチャデータ31", Google translated: "Face create texture data 31".
			/// Japanese description: "顔作成テクスチャデータ31", Google translated: "Face create texture data 31".
			/// </remarks>
			[ParameterTableRowAttribute("faceTexData31", index: 81, minimum: 0, maximum: 255, step: 1, sortOrder: 82, unknown2: 1)]
			[DisplayName("Face create texture data 31")]
			[Description("Face create texture data 31")]
			[DefaultValue((Byte)0)]
			public Byte FaceTexData31 {
				get { return faceTexData31; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceTexData31.");
					SetProperty(ref faceTexData31, ref value, FaceTexData31Property);
				}
			}

			/// <summary>Face create texture data 32</summary>
			/// <remarks>
			/// Japanese short name: "顔作成テクスチャデータ32", Google translated: "Face create texture data 32".
			/// Japanese description: "顔作成テクスチャデータ32", Google translated: "Face create texture data 32".
			/// </remarks>
			[ParameterTableRowAttribute("faceTexData32", index: 82, minimum: 0, maximum: 255, step: 1, sortOrder: 83, unknown2: 1)]
			[DisplayName("Face create texture data 32")]
			[Description("Face create texture data 32")]
			[DefaultValue((Byte)0)]
			public Byte FaceTexData32 {
				get { return faceTexData32; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceTexData32.");
					SetProperty(ref faceTexData32, ref value, FaceTexData32Property);
				}
			}

			/// <summary>Face create texture data 33</summary>
			/// <remarks>
			/// Japanese short name: "顔作成テクスチャデータ33", Google translated: "Face create texture data 33".
			/// Japanese description: "顔作成テクスチャデータ33", Google translated: "Face create texture data 33".
			/// </remarks>
			[ParameterTableRowAttribute("faceTexData33", index: 83, minimum: 0, maximum: 255, step: 1, sortOrder: 84, unknown2: 1)]
			[DisplayName("Face create texture data 33")]
			[Description("Face create texture data 33")]
			[DefaultValue((Byte)0)]
			public Byte FaceTexData33 {
				get { return faceTexData33; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceTexData33.");
					SetProperty(ref faceTexData33, ref value, FaceTexData33Property);
				}
			}

			/// <summary>Face create texture data 34</summary>
			/// <remarks>
			/// Japanese short name: "顔作成テクスチャデータ34", Google translated: "Face create texture data 34".
			/// Japanese description: "顔作成テクスチャデータ34", Google translated: "Face create texture data 34".
			/// </remarks>
			[ParameterTableRowAttribute("faceTexData34", index: 84, minimum: 0, maximum: 255, step: 1, sortOrder: 85, unknown2: 1)]
			[DisplayName("Face create texture data 34")]
			[Description("Face create texture data 34")]
			[DefaultValue((Byte)0)]
			public Byte FaceTexData34 {
				get { return faceTexData34; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceTexData34.");
					SetProperty(ref faceTexData34, ref value, FaceTexData34Property);
				}
			}

			/// <summary>Face create texture data 35</summary>
			/// <remarks>
			/// Japanese short name: "顔作成テクスチャデータ35", Google translated: "Face create texture data 35".
			/// Japanese description: "顔作成テクスチャデータ35", Google translated: "Face create texture data 35".
			/// </remarks>
			[ParameterTableRowAttribute("faceTexData35", index: 85, minimum: 0, maximum: 255, step: 1, sortOrder: 86, unknown2: 1)]
			[DisplayName("Face create texture data 35")]
			[Description("Face create texture data 35")]
			[DefaultValue((Byte)0)]
			public Byte FaceTexData35 {
				get { return faceTexData35; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceTexData35.");
					SetProperty(ref faceTexData35, ref value, FaceTexData35Property);
				}
			}

			/// <summary>Face create texture data 36</summary>
			/// <remarks>
			/// Japanese short name: "顔作成テクスチャデータ36", Google translated: "Face create texture data 36".
			/// Japanese description: "顔作成テクスチャデータ36", Google translated: "Face create texture data 36".
			/// </remarks>
			[ParameterTableRowAttribute("faceTexData36", index: 86, minimum: 0, maximum: 255, step: 1, sortOrder: 87, unknown2: 1)]
			[DisplayName("Face create texture data 36")]
			[Description("Face create texture data 36")]
			[DefaultValue((Byte)0)]
			public Byte FaceTexData36 {
				get { return faceTexData36; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceTexData36.");
					SetProperty(ref faceTexData36, ref value, FaceTexData36Property);
				}
			}

			/// <summary>Face create texture data 37</summary>
			/// <remarks>
			/// Japanese short name: "顔作成テクスチャデータ37", Google translated: "Face create texture data 37".
			/// Japanese description: "顔作成テクスチャデータ37", Google translated: "Face create texture data 37".
			/// </remarks>
			[ParameterTableRowAttribute("faceTexData37", index: 87, minimum: 0, maximum: 255, step: 1, sortOrder: 88, unknown2: 1)]
			[DisplayName("Face create texture data 37")]
			[Description("Face create texture data 37")]
			[DefaultValue((Byte)0)]
			public Byte FaceTexData37 {
				get { return faceTexData37; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceTexData37.");
					SetProperty(ref faceTexData37, ref value, FaceTexData37Property);
				}
			}

			/// <summary>Face create texture data 38</summary>
			/// <remarks>
			/// Japanese short name: "顔作成テクスチャデータ38", Google translated: "Face create texture data 38".
			/// Japanese description: "顔作成テクスチャデータ38", Google translated: "Face create texture data 38".
			/// </remarks>
			[ParameterTableRowAttribute("faceTexData38", index: 88, minimum: 0, maximum: 255, step: 1, sortOrder: 89, unknown2: 1)]
			[DisplayName("Face create texture data 38")]
			[Description("Face create texture data 38")]
			[DefaultValue((Byte)0)]
			public Byte FaceTexData38 {
				get { return faceTexData38; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceTexData38.");
					SetProperty(ref faceTexData38, ref value, FaceTexData38Property);
				}
			}

			/// <summary>Face create texture data 39</summary>
			/// <remarks>
			/// Japanese short name: "顔作成テクスチャデータ39", Google translated: "Face create texture data 39".
			/// Japanese description: "顔作成テクスチャデータ39", Google translated: "Face create texture data 39".
			/// </remarks>
			[ParameterTableRowAttribute("faceTexData39", index: 89, minimum: 0, maximum: 255, step: 1, sortOrder: 90, unknown2: 1)]
			[DisplayName("Face create texture data 39")]
			[Description("Face create texture data 39")]
			[DefaultValue((Byte)0)]
			public Byte FaceTexData39 {
				get { return faceTexData39; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceTexData39.");
					SetProperty(ref faceTexData39, ref value, FaceTexData39Property);
				}
			}

			/// <summary>Face create texture data 40</summary>
			/// <remarks>
			/// Japanese short name: "顔作成テクスチャデータ40", Google translated: "Face create texture data 40".
			/// Japanese description: "顔作成テクスチャデータ40", Google translated: "Face create texture data 40".
			/// </remarks>
			[ParameterTableRowAttribute("faceTexData40", index: 90, minimum: 0, maximum: 255, step: 1, sortOrder: 91, unknown2: 1)]
			[DisplayName("Face create texture data 40")]
			[Description("Face create texture data 40")]
			[DefaultValue((Byte)0)]
			public Byte FaceTexData40 {
				get { return faceTexData40; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceTexData40.");
					SetProperty(ref faceTexData40, ref value, FaceTexData40Property);
				}
			}

			/// <summary>Face create texture data 41</summary>
			/// <remarks>
			/// Japanese short name: "顔作成テクスチャデータ41", Google translated: "Face create texture data 41".
			/// Japanese description: "顔作成テクスチャデータ41", Google translated: "Face create texture data 41".
			/// </remarks>
			[ParameterTableRowAttribute("faceTexData41", index: 91, minimum: 0, maximum: 255, step: 1, sortOrder: 92, unknown2: 1)]
			[DisplayName("Face create texture data 41")]
			[Description("Face create texture data 41")]
			[DefaultValue((Byte)0)]
			public Byte FaceTexData41 {
				get { return faceTexData41; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceTexData41.");
					SetProperty(ref faceTexData41, ref value, FaceTexData41Property);
				}
			}

			/// <summary>Face create texture data 42</summary>
			/// <remarks>
			/// Japanese short name: "顔作成テクスチャデータ42", Google translated: "Face create texture data 42".
			/// Japanese description: "顔作成テクスチャデータ42", Google translated: "Face create texture data 42".
			/// </remarks>
			[ParameterTableRowAttribute("faceTexData42", index: 92, minimum: 0, maximum: 255, step: 1, sortOrder: 93, unknown2: 1)]
			[DisplayName("Face create texture data 42")]
			[Description("Face create texture data 42")]
			[DefaultValue((Byte)0)]
			public Byte FaceTexData42 {
				get { return faceTexData42; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceTexData42.");
					SetProperty(ref faceTexData42, ref value, FaceTexData42Property);
				}
			}

			/// <summary>Face create texture data 43</summary>
			/// <remarks>
			/// Japanese short name: "顔作成テクスチャデータ43", Google translated: "Face create texture data 43".
			/// Japanese description: "顔作成テクスチャデータ43", Google translated: "Face create texture data 43".
			/// </remarks>
			[ParameterTableRowAttribute("faceTexData43", index: 93, minimum: 0, maximum: 255, step: 1, sortOrder: 94, unknown2: 1)]
			[DisplayName("Face create texture data 43")]
			[Description("Face create texture data 43")]
			[DefaultValue((Byte)0)]
			public Byte FaceTexData43 {
				get { return faceTexData43; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceTexData43.");
					SetProperty(ref faceTexData43, ref value, FaceTexData43Property);
				}
			}

			/// <summary>Face create texture data 44</summary>
			/// <remarks>
			/// Japanese short name: "顔作成テクスチャデータ44", Google translated: "Face create texture data 44".
			/// Japanese description: "顔作成テクスチャデータ44", Google translated: "Face create texture data 44".
			/// </remarks>
			[ParameterTableRowAttribute("faceTexData44", index: 94, minimum: 0, maximum: 255, step: 1, sortOrder: 95, unknown2: 1)]
			[DisplayName("Face create texture data 44")]
			[Description("Face create texture data 44")]
			[DefaultValue((Byte)0)]
			public Byte FaceTexData44 {
				get { return faceTexData44; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceTexData44.");
					SetProperty(ref faceTexData44, ref value, FaceTexData44Property);
				}
			}

			/// <summary>Face create texture data 45</summary>
			/// <remarks>
			/// Japanese short name: "顔作成テクスチャデータ45", Google translated: "Face create texture data 45".
			/// Japanese description: "顔作成テクスチャデータ45", Google translated: "Face create texture data 45".
			/// </remarks>
			[ParameterTableRowAttribute("faceTexData45", index: 95, minimum: 0, maximum: 255, step: 1, sortOrder: 96, unknown2: 1)]
			[DisplayName("Face create texture data 45")]
			[Description("Face create texture data 45")]
			[DefaultValue((Byte)0)]
			public Byte FaceTexData45 {
				get { return faceTexData45; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceTexData45.");
					SetProperty(ref faceTexData45, ref value, FaceTexData45Property);
				}
			}

			/// <summary>Face create texture data 46</summary>
			/// <remarks>
			/// Japanese short name: "顔作成テクスチャデータ46", Google translated: "Face create texture data 46".
			/// Japanese description: "顔作成テクスチャデータ46", Google translated: "Face create texture data 46".
			/// </remarks>
			[ParameterTableRowAttribute("faceTexData46", index: 96, minimum: 0, maximum: 255, step: 1, sortOrder: 97, unknown2: 1)]
			[DisplayName("Face create texture data 46")]
			[Description("Face create texture data 46")]
			[DefaultValue((Byte)0)]
			public Byte FaceTexData46 {
				get { return faceTexData46; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceTexData46.");
					SetProperty(ref faceTexData46, ref value, FaceTexData46Property);
				}
			}

			/// <summary>Face create texture data 47</summary>
			/// <remarks>
			/// Japanese short name: "顔作成テクスチャデータ47", Google translated: "Face create texture data 47".
			/// Japanese description: "顔作成テクスチャデータ47", Google translated: "Face create texture data 47".
			/// </remarks>
			[ParameterTableRowAttribute("faceTexData47", index: 97, minimum: 0, maximum: 255, step: 1, sortOrder: 98, unknown2: 1)]
			[DisplayName("Face create texture data 47")]
			[Description("Face create texture data 47")]
			[DefaultValue((Byte)0)]
			public Byte FaceTexData47 {
				get { return faceTexData47; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceTexData47.");
					SetProperty(ref faceTexData47, ref value, FaceTexData47Property);
				}
			}

			/// <summary>Face create texture data 48</summary>
			/// <remarks>
			/// Japanese short name: "顔作成テクスチャデータ48", Google translated: "Face create texture data 48".
			/// Japanese description: "顔作成テクスチャデータ48", Google translated: "Face create texture data 48".
			/// </remarks>
			[ParameterTableRowAttribute("faceTexData48", index: 98, minimum: 0, maximum: 255, step: 1, sortOrder: 99, unknown2: 1)]
			[DisplayName("Face create texture data 48")]
			[Description("Face create texture data 48")]
			[DefaultValue((Byte)0)]
			public Byte FaceTexData48 {
				get { return faceTexData48; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceTexData48.");
					SetProperty(ref faceTexData48, ref value, FaceTexData48Property);
				}
			}

			/// <summary>Face create texture data 49</summary>
			/// <remarks>
			/// Japanese short name: "顔作成テクスチャデータ49", Google translated: "Face create texture data 49".
			/// Japanese description: "顔作成テクスチャデータ49", Google translated: "Face create texture data 49".
			/// </remarks>
			[ParameterTableRowAttribute("faceTexData49", index: 99, minimum: 0, maximum: 255, step: 1, sortOrder: 100, unknown2: 1)]
			[DisplayName("Face create texture data 49")]
			[Description("Face create texture data 49")]
			[DefaultValue((Byte)0)]
			public Byte FaceTexData49 {
				get { return faceTexData49; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for FaceTexData49.");
					SetProperty(ref faceTexData49, ref value, FaceTexData49Property);
				}
			}

			/// <summary>Hairdo</summary>
			/// <remarks>
			/// Japanese short name: "髪型", Google translated: "Hairdo".
			/// Japanese description: "髪モデルを選択させる", Google translated: "I to select a hair model".
			/// </remarks>
			[ParameterTableRowAttribute("hairStyle", index: 100, minimum: 0, maximum: 255, step: 1, sortOrder: 101, unknown2: 1)]
			[DisplayName("Hairdo")]
			[Description("I to select a hair model")]
			[DefaultValue((FaceHairStyle)0)]
			public FaceHairStyle HairStyle {
				get { return hairStyle; }
				set { SetProperty(ref hairStyle, ref value, HairStyleProperty); }
			}

			/// <summary>Hair color</summary>
			/// <remarks>
			/// Japanese short name: "髪の色", Google translated: "Hair color".
			/// Japanese description: "デフォルトカラーを色名で選択", Google translated: "Can be selected by color name the default color".
			/// </remarks>
			[ParameterTableRowAttribute("hairColor_Base", index: 101, minimum: 0, maximum: 255, step: 1, sortOrder: 102, unknown2: 1)]
			[DisplayName("Hair color")]
			[Description("Can be selected by color name the default color")]
			[DefaultValue((FaceHairColor)0)]
			public FaceHairColor HairColor_Base {
				get { return hairColor_Base; }
				set { SetProperty(ref hairColor_Base, ref value, HairColor_BaseProperty); }
			}

			/// <summary>Hair Color (R)</summary>
			/// <remarks>
			/// Japanese short name: "髪の色(Ｒ)", Google translated: "Hair Color (R)".
			/// Japanese description: "デフォルトカラー状態からR値を変更可能", Google translated: "You can change the R-value from the default color state".
			/// </remarks>
			[ParameterTableRowAttribute("hairColor_R", index: 102, minimum: 0, maximum: 255, step: 1, sortOrder: 103, unknown2: 1)]
			[DisplayName("Hair Color (R)")]
			[Description("You can change the R-value from the default color state")]
			[DefaultValue((Byte)0)]
			public Byte HairColor_R {
				get { return hairColor_R; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for HairColor_R.");
					SetProperty(ref hairColor_R, ref value, HairColor_RProperty);
				}
			}

			/// <summary>Hair color (G)</summary>
			/// <remarks>
			/// Japanese short name: "髪の色(Ｇ)", Google translated: "Hair color (G)".
			/// Japanese description: "デフォルトカラー状態からG値を変更可能", Google translated: "You can change the G value from the default color state".
			/// </remarks>
			[ParameterTableRowAttribute("hairColor_G", index: 103, minimum: 0, maximum: 255, step: 1, sortOrder: 104, unknown2: 1)]
			[DisplayName("Hair color (G)")]
			[Description("You can change the G value from the default color state")]
			[DefaultValue((Byte)0)]
			public Byte HairColor_G {
				get { return hairColor_G; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for HairColor_G.");
					SetProperty(ref hairColor_G, ref value, HairColor_GProperty);
				}
			}

			/// <summary>Hair color (B)</summary>
			/// <remarks>
			/// Japanese short name: "髪の色(Ｂ)", Google translated: "Hair color (B)".
			/// Japanese description: "デフォルトカラー状態からB値を変更可能", Google translated: "Can be changed B values ​​from the default color state".
			/// </remarks>
			[ParameterTableRowAttribute("hairColor_B", index: 104, minimum: 0, maximum: 255, step: 1, sortOrder: 105, unknown2: 1)]
			[DisplayName("Hair color (B)")]
			[Description("Can be changed B values ​​from the default color state")]
			[DefaultValue((Byte)0)]
			public Byte HairColor_B {
				get { return hairColor_B; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for HairColor_B.");
					SetProperty(ref hairColor_B, ref value, HairColor_BProperty);
				}
			}

			/// <summary>Eye Color (R)</summary>
			/// <remarks>
			/// Japanese short name: "瞳の色(Ｒ)", Google translated: "Eye Color (R)".
			/// Japanese description: "デフォルトカラー状態からR値を変更可能", Google translated: "You can change the R-value from the default color state".
			/// </remarks>
			[ParameterTableRowAttribute("eyeColor_R", index: 105, minimum: 0, maximum: 255, step: 1, sortOrder: 106, unknown2: 1)]
			[DisplayName("Eye Color (R)")]
			[Description("You can change the R-value from the default color state")]
			[DefaultValue((Byte)127)]
			public Byte EyeColor_R {
				get { return eyeColor_R; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for EyeColor_R.");
					SetProperty(ref eyeColor_R, ref value, EyeColor_RProperty);
				}
			}

			/// <summary>Eye Color (G)</summary>
			/// <remarks>
			/// Japanese short name: "瞳の色(Ｇ)", Google translated: "Eye Color (G)".
			/// Japanese description: "デフォルトカラー状態からG値を変更可能", Google translated: "You can change the G value from the default color state".
			/// </remarks>
			[ParameterTableRowAttribute("eyeColor_G", index: 106, minimum: 0, maximum: 255, step: 1, sortOrder: 107, unknown2: 1)]
			[DisplayName("Eye Color (G)")]
			[Description("You can change the G value from the default color state")]
			[DefaultValue((Byte)165)]
			public Byte EyeColor_G {
				get { return eyeColor_G; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for EyeColor_G.");
					SetProperty(ref eyeColor_G, ref value, EyeColor_GProperty);
				}
			}

			/// <summary>Eye Color (B)</summary>
			/// <remarks>
			/// Japanese short name: "瞳の色(Ｂ)", Google translated: "Eye Color (B)".
			/// Japanese description: "デフォルトカラー状態からB値を変更可能", Google translated: "Can be changed B values ​​from the default color state".
			/// </remarks>
			[ParameterTableRowAttribute("eyeColor_B", index: 107, minimum: 0, maximum: 255, step: 1, sortOrder: 108, unknown2: 1)]
			[DisplayName("Eye Color (B)")]
			[Description("Can be changed B values ​​from the default color state")]
			[DefaultValue((Byte)178)]
			public Byte EyeColor_B {
				get { return eyeColor_B; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for EyeColor_B.");
					SetProperty(ref eyeColor_B, ref value, EyeColor_BProperty);
				}
			}

			/// <summary>Padding</summary>
			/// <remarks>
			/// Japanese short name: "パディング", Google translated: "Padding".
			/// Japanese description: "パディング", Google translated: "Padding".
			/// </remarks>
			[ParameterTableRowAttribute("pad[20]", index: 108, minimum: 0, maximum: 0, step: 0, sortOrder: 110, unknown2: 0)]
			[DisplayName("Padding")]
			[Description("Padding")]
			public Byte[] Pad {
				get { return pad; }
				set { SetProperty(ref pad, ref value, PadProperty); }
			}

			internal FaceGeneration(ParameterTable table, int index, AssetLoader loader, int next)
				: base(table, index, loader) {
				BinaryReader reader = loader.Reader;

				FaceGeoData00 = reader.ReadByte();
				FaceGeoData01 = reader.ReadByte();
				FaceGeoData02 = reader.ReadByte();
				FaceGeoData03 = reader.ReadByte();
				FaceGeoData04 = reader.ReadByte();
				FaceGeoData05 = reader.ReadByte();
				FaceGeoData06 = reader.ReadByte();
				FaceGeoData07 = reader.ReadByte();
				FaceGeoData08 = reader.ReadByte();
				FaceGeoData09 = reader.ReadByte();
				FaceGeoData10 = reader.ReadByte();
				FaceGeoData11 = reader.ReadByte();
				FaceGeoData12 = reader.ReadByte();
				FaceGeoData13 = reader.ReadByte();
				FaceGeoData14 = reader.ReadByte();
				FaceGeoData15 = reader.ReadByte();
				FaceGeoData16 = reader.ReadByte();
				FaceGeoData17 = reader.ReadByte();
				FaceGeoData18 = reader.ReadByte();
				FaceGeoData19 = reader.ReadByte();
				FaceGeoData20 = reader.ReadByte();
				FaceGeoData21 = reader.ReadByte();
				FaceGeoData22 = reader.ReadByte();
				FaceGeoData23 = reader.ReadByte();
				FaceGeoData24 = reader.ReadByte();
				FaceGeoData25 = reader.ReadByte();
				FaceGeoData26 = reader.ReadByte();
				FaceGeoData27 = reader.ReadByte();
				FaceGeoData28 = reader.ReadByte();
				FaceGeoData29 = reader.ReadByte();
				FaceGeoData30 = reader.ReadByte();
				FaceGeoData31 = reader.ReadByte();
				FaceGeoData32 = reader.ReadByte();
				FaceGeoData33 = reader.ReadByte();
				FaceGeoData34 = reader.ReadByte();
				FaceGeoData35 = reader.ReadByte();
				FaceGeoData36 = reader.ReadByte();
				FaceGeoData37 = reader.ReadByte();
				FaceGeoData38 = reader.ReadByte();
				FaceGeoData39 = reader.ReadByte();
				FaceGeoData40 = reader.ReadByte();
				FaceGeoData41 = reader.ReadByte();
				FaceGeoData42 = reader.ReadByte();
				FaceGeoData43 = reader.ReadByte();
				FaceGeoData44 = reader.ReadByte();
				FaceGeoData45 = reader.ReadByte();
				FaceGeoData46 = reader.ReadByte();
				FaceGeoData47 = reader.ReadByte();
				FaceGeoData48 = reader.ReadByte();
				FaceGeoData49 = reader.ReadByte();
				FaceTexData00 = reader.ReadByte();
				FaceTexData01 = reader.ReadByte();
				FaceTexData02 = reader.ReadByte();
				FaceTexData03 = reader.ReadByte();
				FaceTexData04 = reader.ReadByte();
				FaceTexData05 = reader.ReadByte();
				FaceTexData06 = reader.ReadByte();
				FaceTexData07 = reader.ReadByte();
				FaceTexData08 = reader.ReadByte();
				FaceTexData09 = reader.ReadByte();
				FaceTexData10 = reader.ReadByte();
				FaceTexData11 = reader.ReadByte();
				FaceTexData12 = reader.ReadByte();
				FaceTexData13 = reader.ReadByte();
				FaceTexData14 = reader.ReadByte();
				FaceTexData15 = reader.ReadByte();
				FaceTexData16 = reader.ReadByte();
				FaceTexData17 = reader.ReadByte();
				FaceTexData18 = reader.ReadByte();
				FaceTexData19 = reader.ReadByte();
				FaceTexData20 = reader.ReadByte();
				FaceTexData21 = reader.ReadByte();
				FaceTexData22 = reader.ReadByte();
				FaceTexData23 = reader.ReadByte();
				FaceTexData24 = reader.ReadByte();
				FaceTexData25 = reader.ReadByte();
				FaceTexData26 = reader.ReadByte();
				FaceTexData27 = reader.ReadByte();
				FaceTexData28 = reader.ReadByte();
				FaceTexData29 = reader.ReadByte();
				FaceTexData30 = reader.ReadByte();
				FaceTexData31 = reader.ReadByte();
				FaceTexData32 = reader.ReadByte();
				FaceTexData33 = reader.ReadByte();
				FaceTexData34 = reader.ReadByte();
				FaceTexData35 = reader.ReadByte();
				FaceTexData36 = reader.ReadByte();
				FaceTexData37 = reader.ReadByte();
				FaceTexData38 = reader.ReadByte();
				FaceTexData39 = reader.ReadByte();
				FaceTexData40 = reader.ReadByte();
				FaceTexData41 = reader.ReadByte();
				FaceTexData42 = reader.ReadByte();
				FaceTexData43 = reader.ReadByte();
				FaceTexData44 = reader.ReadByte();
				FaceTexData45 = reader.ReadByte();
				FaceTexData46 = reader.ReadByte();
				FaceTexData47 = reader.ReadByte();
				FaceTexData48 = reader.ReadByte();
				FaceTexData49 = reader.ReadByte();
				HairStyle = (FaceHairStyle)reader.ReadByte();
				HairColor_Base = (FaceHairColor)reader.ReadByte();
				HairColor_R = reader.ReadByte();
				HairColor_G = reader.ReadByte();
				HairColor_B = reader.ReadByte();
				EyeColor_R = reader.ReadByte();
				EyeColor_G = reader.ReadByte();
				EyeColor_B = reader.ReadByte();
				Pad = reader.ReadBytes(20);
			}

			internal FaceGeneration(ParameterTable table, int index)
				: base(table, index) {
				FaceGeoData00 = (Byte)0;
				FaceGeoData01 = (Byte)0;
				FaceGeoData02 = (Byte)0;
				FaceGeoData03 = (Byte)0;
				FaceGeoData04 = (Byte)0;
				FaceGeoData05 = (Byte)0;
				FaceGeoData06 = (Byte)0;
				FaceGeoData07 = (Byte)0;
				FaceGeoData08 = (Byte)0;
				FaceGeoData09 = (Byte)0;
				FaceGeoData10 = (Byte)0;
				FaceGeoData11 = (Byte)0;
				FaceGeoData12 = (Byte)0;
				FaceGeoData13 = (Byte)0;
				FaceGeoData14 = (Byte)0;
				FaceGeoData15 = (Byte)0;
				FaceGeoData16 = (Byte)0;
				FaceGeoData17 = (Byte)0;
				FaceGeoData18 = (Byte)0;
				FaceGeoData19 = (Byte)0;
				FaceGeoData20 = (Byte)0;
				FaceGeoData21 = (Byte)0;
				FaceGeoData22 = (Byte)0;
				FaceGeoData23 = (Byte)0;
				FaceGeoData24 = (Byte)0;
				FaceGeoData25 = (Byte)0;
				FaceGeoData26 = (Byte)0;
				FaceGeoData27 = (Byte)0;
				FaceGeoData28 = (Byte)0;
				FaceGeoData29 = (Byte)0;
				FaceGeoData30 = (Byte)0;
				FaceGeoData31 = (Byte)0;
				FaceGeoData32 = (Byte)0;
				FaceGeoData33 = (Byte)0;
				FaceGeoData34 = (Byte)0;
				FaceGeoData35 = (Byte)0;
				FaceGeoData36 = (Byte)0;
				FaceGeoData37 = (Byte)0;
				FaceGeoData38 = (Byte)0;
				FaceGeoData39 = (Byte)0;
				FaceGeoData40 = (Byte)0;
				FaceGeoData41 = (Byte)0;
				FaceGeoData42 = (Byte)0;
				FaceGeoData43 = (Byte)0;
				FaceGeoData44 = (Byte)0;
				FaceGeoData45 = (Byte)0;
				FaceGeoData46 = (Byte)0;
				FaceGeoData47 = (Byte)0;
				FaceGeoData48 = (Byte)0;
				FaceGeoData49 = (Byte)0;
				FaceTexData00 = (Byte)0;
				FaceTexData01 = (Byte)0;
				FaceTexData02 = (Byte)0;
				FaceTexData03 = (Byte)0;
				FaceTexData04 = (Byte)0;
				FaceTexData05 = (Byte)0;
				FaceTexData06 = (Byte)0;
				FaceTexData07 = (Byte)0;
				FaceTexData08 = (Byte)0;
				FaceTexData09 = (Byte)0;
				FaceTexData10 = (Byte)0;
				FaceTexData11 = (Byte)0;
				FaceTexData12 = (Byte)0;
				FaceTexData13 = (Byte)0;
				FaceTexData14 = (Byte)0;
				FaceTexData15 = (Byte)0;
				FaceTexData16 = (Byte)0;
				FaceTexData17 = (Byte)0;
				FaceTexData18 = (Byte)0;
				FaceTexData19 = (Byte)0;
				FaceTexData20 = (Byte)0;
				FaceTexData21 = (Byte)0;
				FaceTexData22 = (Byte)0;
				FaceTexData23 = (Byte)0;
				FaceTexData24 = (Byte)0;
				FaceTexData25 = (Byte)0;
				FaceTexData26 = (Byte)0;
				FaceTexData27 = (Byte)0;
				FaceTexData28 = (Byte)0;
				FaceTexData29 = (Byte)0;
				FaceTexData30 = (Byte)0;
				FaceTexData31 = (Byte)0;
				FaceTexData32 = (Byte)0;
				FaceTexData33 = (Byte)0;
				FaceTexData34 = (Byte)0;
				FaceTexData35 = (Byte)0;
				FaceTexData36 = (Byte)0;
				FaceTexData37 = (Byte)0;
				FaceTexData38 = (Byte)0;
				FaceTexData39 = (Byte)0;
				FaceTexData40 = (Byte)0;
				FaceTexData41 = (Byte)0;
				FaceTexData42 = (Byte)0;
				FaceTexData43 = (Byte)0;
				FaceTexData44 = (Byte)0;
				FaceTexData45 = (Byte)0;
				FaceTexData46 = (Byte)0;
				FaceTexData47 = (Byte)0;
				FaceTexData48 = (Byte)0;
				FaceTexData49 = (Byte)0;
				HairStyle = (FaceHairStyle)0;
				HairColor_Base = (FaceHairColor)0;
				HairColor_R = (Byte)0;
				HairColor_G = (Byte)0;
				HairColor_B = (Byte)0;
				EyeColor_R = (Byte)127;
				EyeColor_G = (Byte)165;
				EyeColor_B = (Byte)178;
				Pad = new Byte[20];
			}

			/// <summary>Write the row to the writer.</summary>
			/// <param name="writer"></param>
			public override void Write(BinaryWriter writer) {
				writer.Write(FaceGeoData00);
				writer.Write(FaceGeoData01);
				writer.Write(FaceGeoData02);
				writer.Write(FaceGeoData03);
				writer.Write(FaceGeoData04);
				writer.Write(FaceGeoData05);
				writer.Write(FaceGeoData06);
				writer.Write(FaceGeoData07);
				writer.Write(FaceGeoData08);
				writer.Write(FaceGeoData09);
				writer.Write(FaceGeoData10);
				writer.Write(FaceGeoData11);
				writer.Write(FaceGeoData12);
				writer.Write(FaceGeoData13);
				writer.Write(FaceGeoData14);
				writer.Write(FaceGeoData15);
				writer.Write(FaceGeoData16);
				writer.Write(FaceGeoData17);
				writer.Write(FaceGeoData18);
				writer.Write(FaceGeoData19);
				writer.Write(FaceGeoData20);
				writer.Write(FaceGeoData21);
				writer.Write(FaceGeoData22);
				writer.Write(FaceGeoData23);
				writer.Write(FaceGeoData24);
				writer.Write(FaceGeoData25);
				writer.Write(FaceGeoData26);
				writer.Write(FaceGeoData27);
				writer.Write(FaceGeoData28);
				writer.Write(FaceGeoData29);
				writer.Write(FaceGeoData30);
				writer.Write(FaceGeoData31);
				writer.Write(FaceGeoData32);
				writer.Write(FaceGeoData33);
				writer.Write(FaceGeoData34);
				writer.Write(FaceGeoData35);
				writer.Write(FaceGeoData36);
				writer.Write(FaceGeoData37);
				writer.Write(FaceGeoData38);
				writer.Write(FaceGeoData39);
				writer.Write(FaceGeoData40);
				writer.Write(FaceGeoData41);
				writer.Write(FaceGeoData42);
				writer.Write(FaceGeoData43);
				writer.Write(FaceGeoData44);
				writer.Write(FaceGeoData45);
				writer.Write(FaceGeoData46);
				writer.Write(FaceGeoData47);
				writer.Write(FaceGeoData48);
				writer.Write(FaceGeoData49);
				writer.Write(FaceTexData00);
				writer.Write(FaceTexData01);
				writer.Write(FaceTexData02);
				writer.Write(FaceTexData03);
				writer.Write(FaceTexData04);
				writer.Write(FaceTexData05);
				writer.Write(FaceTexData06);
				writer.Write(FaceTexData07);
				writer.Write(FaceTexData08);
				writer.Write(FaceTexData09);
				writer.Write(FaceTexData10);
				writer.Write(FaceTexData11);
				writer.Write(FaceTexData12);
				writer.Write(FaceTexData13);
				writer.Write(FaceTexData14);
				writer.Write(FaceTexData15);
				writer.Write(FaceTexData16);
				writer.Write(FaceTexData17);
				writer.Write(FaceTexData18);
				writer.Write(FaceTexData19);
				writer.Write(FaceTexData20);
				writer.Write(FaceTexData21);
				writer.Write(FaceTexData22);
				writer.Write(FaceTexData23);
				writer.Write(FaceTexData24);
				writer.Write(FaceTexData25);
				writer.Write(FaceTexData26);
				writer.Write(FaceTexData27);
				writer.Write(FaceTexData28);
				writer.Write(FaceTexData29);
				writer.Write(FaceTexData30);
				writer.Write(FaceTexData31);
				writer.Write(FaceTexData32);
				writer.Write(FaceTexData33);
				writer.Write(FaceTexData34);
				writer.Write(FaceTexData35);
				writer.Write(FaceTexData36);
				writer.Write(FaceTexData37);
				writer.Write(FaceTexData38);
				writer.Write(FaceTexData39);
				writer.Write(FaceTexData40);
				writer.Write(FaceTexData41);
				writer.Write(FaceTexData42);
				writer.Write(FaceTexData43);
				writer.Write(FaceTexData44);
				writer.Write(FaceTexData45);
				writer.Write(FaceTexData46);
				writer.Write(FaceTexData47);
				writer.Write(FaceTexData48);
				writer.Write(FaceTexData49);
				writer.Write((Byte)HairStyle);
				writer.Write((Byte)HairColor_Base);
				writer.Write(HairColor_R);
				writer.Write(HairColor_G);
				writer.Write(HairColor_B);
				writer.Write(EyeColor_R);
				writer.Write(EyeColor_G);
				writer.Write(EyeColor_B);
				writer.Write(Pad);
			}
		}
	}
}
