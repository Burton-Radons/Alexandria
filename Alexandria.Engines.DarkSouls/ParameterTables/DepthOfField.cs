/* f
For ParameterTable.cs under ParameterTableRow.ReadRow:
For ParameterDefinition.cs under ParameterDefinitionRow.GetDotNetType():
For Enumerations.cs:*/
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
		/// <remarks>From DofBank.paramdef (id 05h).</remarks>
		public class DepthOfField : ParameterTableRow {
			public const string TableName = "DOF_BANK";

			Single farDofBegin, farDofEnd, nearDofBegin, nearDofEnd, dispersionSq;
			Byte farDofMul, nearDofMul;
			Byte[] pad_0, pad_1;

			public static readonly PropertyInfo
				FarDofBeginProperty = GetProperty<DepthOfField>("FarDofBegin"),
				FarDofEndProperty = GetProperty<DepthOfField>("FarDofEnd"),
				FarDofMulProperty = GetProperty<DepthOfField>("FarDofMul"),
				Pad_0Property = GetProperty<DepthOfField>("Pad_0"),
				NearDofBeginProperty = GetProperty<DepthOfField>("NearDofBegin"),
				NearDofEndProperty = GetProperty<DepthOfField>("NearDofEnd"),
				NearDofMulProperty = GetProperty<DepthOfField>("NearDofMul"),
				Pad_1Property = GetProperty<DepthOfField>("Pad_1"),
				DispersionSqProperty = GetProperty<DepthOfField>("DispersionSq");

			/// <summary>Far start distance [m]</summary>
			/// <remarks>
			/// Japanese short name: "遠方開始距離[m]", Google translated: "Far start distance [m]".
			/// Japanese description: "被写界深度で遠くがぼけ始める距離", Google translated: "Distance far starts blurring in depth of field".
			/// </remarks>
			[ParameterTableRowAttribute("farDofBegin", index: 0, minimum: 0, maximum: 999999, step: 0.1, order: 1, unknown2: 0)]
			[DisplayName("Far start distance [m]")]
			[Description("Distance far starts blurring in depth of field")]
			[DefaultValue((Single)60)]
			public Single FarDofBegin {
				get { return farDofBegin; }
				set {
					if ((double)value < 0 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999999 for FarDofBegin.");
					SetProperty(ref farDofBegin, ref value, FarDofBeginProperty);
				}
			}

			/// <summary>Far end distance [m]</summary>
			/// <remarks>
			/// Japanese short name: "遠方終了距離[m]", Google translated: "Far end distance [m]".
			/// Japanese description: "被写界深度で遠くがぼけ終わる距離", Google translated: "Distance far has finished in the depth of field blurring".
			/// </remarks>
			[ParameterTableRowAttribute("farDofEnd", index: 1, minimum: 0, maximum: 999999, step: 0.1, order: 2, unknown2: 0)]
			[DisplayName("Far end distance [m]")]
			[Description("Distance far has finished in the depth of field blurring")]
			[DefaultValue((Single)360)]
			public Single FarDofEnd {
				get { return farDofEnd; }
				set {
					if ((double)value < 0 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999999 for FarDofEnd.");
					SetProperty(ref farDofEnd, ref value, FarDofEndProperty);
				}
			}

			/// <summary>Far strength [%]</summary>
			/// <remarks>
			/// Japanese short name: "遠方強度[％]", Google translated: "Far strength [%]".
			/// Japanese description: "被写界深度のぼけ具合(0でぼけなくなります)", Google translated: "(You will not be blurred by 0) blurriness of depth of field".
			/// </remarks>
			[ParameterTableRowAttribute("farDofMul", index: 2, minimum: 0, maximum: 100, step: 1, order: 3, unknown2: 0)]
			[DisplayName("Far strength [%]")]
			[Description("(You will not be blurred by 0) blurriness of depth of field")]
			[DefaultValue((Byte)100)]
			public Byte FarDofMul {
				get { return farDofMul; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for FarDofMul.");
					SetProperty(ref farDofMul, ref value, FarDofMulProperty);
				}
			}

			/// <summary>Padding</summary>
			/// <remarks>
			/// Japanese short name: "パディング", Google translated: "Padding".
			/// Japanese description: "ダミー", Google translated: "Dummy".
			/// </remarks>
			[ParameterTableRowAttribute("pad_0[3]", index: 3, minimum: 0, maximum: 0, step: 0, order: 10, unknown2: 0)]
			[DisplayName("Padding")]
			[Description("Dummy")]
			public Byte[] Pad_0 {
				get { return pad_0; }
				set { SetProperty(ref pad_0, ref value, Pad_0Property); }
			}

			/// <summary>Near start distance [m]</summary>
			/// <remarks>
			/// Japanese short name: "近傍開始距離[m]", Google translated: "Near start distance [m]".
			/// Japanese description: "被写界深度で近くがぼけ始める距離(終了距離より遠くします)", Google translated: "(I will be far away from the end distance) close starts blurring in the depth of field".
			/// </remarks>
			[ParameterTableRowAttribute("nearDofBegin", index: 4, minimum: 0, maximum: 999999, step: 0.1, order: 4, unknown2: 0)]
			[DisplayName("Near start distance [m]")]
			[Description("(I will be far away from the end distance) close starts blurring in the depth of field")]
			[DefaultValue((Single)3)]
			public Single NearDofBegin {
				get { return nearDofBegin; }
				set {
					if ((double)value < 0 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999999 for NearDofBegin.");
					SetProperty(ref nearDofBegin, ref value, NearDofBeginProperty);
				}
			}

			/// <summary>Near the end distance [m]</summary>
			/// <remarks>
			/// Japanese short name: "近傍終了距離[m]", Google translated: "Near the end distance [m]".
			/// Japanese description: "被写界深度で近くがぼけ終わる距離(開始距離より近くします)", Google translated: "(I'll start closer than distance) close finishes in the depth of field blurring".
			/// </remarks>
			[ParameterTableRowAttribute("nearDofEnd", index: 5, minimum: 0, maximum: 999999, step: 0.1, order: 5, unknown2: 0)]
			[DisplayName("Near the end distance [m]")]
			[Description("(I'll start closer than distance) close finishes in the depth of field blurring")]
			[DefaultValue((Single)0)]
			public Single NearDofEnd {
				get { return nearDofEnd; }
				set {
					if ((double)value < 0 || (double)value > 999999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999999 for NearDofEnd.");
					SetProperty(ref nearDofEnd, ref value, NearDofEndProperty);
				}
			}

			/// <summary>Neighborhood strength [%]</summary>
			/// <remarks>
			/// Japanese short name: "近傍強度[％]", Google translated: "Neighborhood strength [%]".
			/// Japanese description: "被写界深度のぼけ具合(0でぼけなくなります)", Google translated: "(You will not be blurred by 0) blurriness of depth of field".
			/// </remarks>
			[ParameterTableRowAttribute("nearDofMul", index: 6, minimum: 0, maximum: 100, step: 1, order: 6, unknown2: 0)]
			[DisplayName("Neighborhood strength [%]")]
			[Description("(You will not be blurred by 0) blurriness of depth of field")]
			[DefaultValue((Byte)100)]
			public Byte NearDofMul {
				get { return nearDofMul; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for NearDofMul.");
					SetProperty(ref nearDofMul, ref value, NearDofMulProperty);
				}
			}

			/// <summary>Padding</summary>
			/// <remarks>
			/// Japanese short name: "パディング", Google translated: "Padding".
			/// Japanese description: "ダミー", Google translated: "Dummy".
			/// </remarks>
			[ParameterTableRowAttribute("pad_1[3]", index: 7, minimum: 0, maximum: 0, step: 0, order: 11, unknown2: 0)]
			[DisplayName("Padding")]
			[Description("Dummy")]
			public Byte[] Pad_1 {
				get { return pad_1; }
				set { SetProperty(ref pad_1, ref value, Pad_1Property); }
			}

			/// <summary>Size of the blur</summary>
			/// <remarks>
			/// Japanese short name: "ボケの大きさ", Google translated: "Size of the blur".
			/// Japanese description: "値を大きくすると被写界深度のボケが強くなります", Google translated: "Blurring of the depth of field will be stronger and to increase the value".
			/// </remarks>
			[ParameterTableRowAttribute("dispersionSq", index: 8, minimum: 0, maximum: 30, step: 0.01, order: 7, unknown2: 0)]
			[DisplayName("Size of the blur")]
			[Description("Blurring of the depth of field will be stronger and to increase the value")]
			[DefaultValue((Single)5)]
			public Single DispersionSq {
				get { return dispersionSq; }
				set {
					if ((double)value < 0 || (double)value > 30)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 30 for DispersionSq.");
					SetProperty(ref dispersionSq, ref value, DispersionSqProperty);
				}
			}

			internal DepthOfField(ParameterTable table, int index, AssetLoader loader, int next)
				: base(table, index, loader) {
				BinaryReader reader = loader.Reader;

				FarDofBegin = reader.ReadSingle();
				FarDofEnd = reader.ReadSingle();
				FarDofMul = reader.ReadByte();
				Pad_0 = reader.ReadBytes(3);
				NearDofBegin = reader.ReadSingle();
				NearDofEnd = reader.ReadSingle();
				NearDofMul = reader.ReadByte();
				Pad_1 = reader.ReadBytes(3);
				DispersionSq = reader.ReadSingle();
			}

			internal DepthOfField(ParameterTable table, int index)
				: base(table, index) {
				FarDofBegin = (Single)60;
				FarDofEnd = (Single)360;
				FarDofMul = (Byte)100;
				Pad_0 = new Byte[3];
				NearDofBegin = (Single)3;
				NearDofEnd = (Single)0;
				NearDofMul = (Byte)100;
				Pad_1 = new Byte[3];
				DispersionSq = (Single)5;
			}

			public override void Write(BinaryWriter writer) {
				writer.Write(FarDofBegin);
				writer.Write(FarDofEnd);
				writer.Write(FarDofMul);
				writer.Write(Pad_0);
				writer.Write(NearDofBegin);
				writer.Write(NearDofEnd);
				writer.Write(NearDofMul);
				writer.Write(Pad_1);
				writer.Write(DispersionSq);
			}
		}
	}
}
