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
		/// Defined as "RAGDOLL_PARAM_ST" in Dark Souls in the file "RagdollParam.paramdef" (id 26h).
		/// </remarks>
		public class Ragdoll : ParameterTableRow {
			public const string TableName = "RAGDOLL_PARAM_ST";

			Single hierarchyGain, velocityDamping, accelGain, velocityGain, positionGain, maxLinerVelocity, maxAngularVelocity, snapGain;
			bool enable;
			SByte partsHitMaskNo;
			Byte[] pad;

			public static readonly PropertyInfo
				HierarchyGainProperty = GetProperty<Ragdoll>("HierarchyGain"),
				VelocityDampingProperty = GetProperty<Ragdoll>("VelocityDamping"),
				AccelGainProperty = GetProperty<Ragdoll>("AccelGain"),
				VelocityGainProperty = GetProperty<Ragdoll>("VelocityGain"),
				PositionGainProperty = GetProperty<Ragdoll>("PositionGain"),
				MaxLinerVelocityProperty = GetProperty<Ragdoll>("MaxLinerVelocity"),
				MaxAngularVelocityProperty = GetProperty<Ragdoll>("MaxAngularVelocity"),
				SnapGainProperty = GetProperty<Ragdoll>("SnapGain"),
				EnableProperty = GetProperty<Ragdoll>("Enable"),
				PartsHitMaskNoProperty = GetProperty<Ragdoll>("PartsHitMaskNo"),
				PadProperty = GetProperty<Ragdoll>("Pad");

			/// <summary>Hierarchy gain</summary>
			/// <remarks>
			/// Japanese short name: "ヒエラルキーゲイン", Google translated: "Hierarchy gain".
			/// Japanese description: "低くすると元のポーズに近づき、大きくするとぐにゃぐにゃになる。", Google translated: "The closer to the original pose , mush Increasing Decreasing .".
			/// </remarks>
			[ParameterTableRowAttribute("hierarchyGain", index: 0, minimum: 0, maximum: 1, step: 0.01, order: 400, unknown2: 1)]
			[DisplayName("Hierarchy gain")]
			[Description("The closer to the original pose , mush Increasing Decreasing .")]
			[DefaultValue((Single)0.17)]
			public Single HierarchyGain {
				get { return hierarchyGain; }
				set {
					if ((double)value < 0 || (double)value > 1)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1 for " + HierarchyGainProperty.Name + ".");
					SetProperty(ref hierarchyGain, ref value, HierarchyGainProperty);
				}
			}

			/// <summary>Velocity decay</summary>
			/// <remarks>
			/// Japanese short name: "速度減衰", Google translated: "Velocity decay".
			/// Japanese description: "ラグドールの移動スピードの減速率。0に近づくとゆっくり戻り、1に近づくとぱっと戻る", Google translated: "Deceleration rate of movement speed of ragdoll . Returning slowly approaches 0 , the process returns putt approaches 1".
			/// </remarks>
			[ParameterTableRowAttribute("velocityDamping", index: 1, minimum: 0, maximum: 1, step: 0.01, order: 500, unknown2: 1)]
			[DisplayName("Velocity decay")]
			[Description("Deceleration rate of movement speed of ragdoll . Returning slowly approaches 0 , the process returns putt approaches 1")]
			[DefaultValue((Single)0)]
			public Single VelocityDamping {
				get { return velocityDamping; }
				set {
					if ((double)value < 0 || (double)value > 1)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1 for " + VelocityDampingProperty.Name + ".");
					SetProperty(ref velocityDamping, ref value, VelocityDampingProperty);
				}
			}

			/// <summary>Acceleration gain</summary>
			/// <remarks>
			/// Japanese short name: "加速度ゲイン", Google translated: "Acceleration gain".
			/// Japanese description: "リジッドの加速度の調整パラメータ。低くすると動きが柔らかくなり、高くすると硬くなる。加速度ゲインよりも低い値にするらしい。", Google translated: "Adjustment parameters of the acceleration of the rigid . Movement is soft and low , it is hard and higher . It seems to be to a value lower than the acceleration gain .".
			/// </remarks>
			[ParameterTableRowAttribute("accelGain", index: 2, minimum: 0, maximum: 1, step: 0.01, order: 600, unknown2: 1)]
			[DisplayName("Acceleration gain")]
			[Description("Adjustment parameters of the acceleration of the rigid . Movement is soft and low , it is hard and higher . It seems to be to a value lower than the acceleration gain .")]
			[DefaultValue((Single)1)]
			public Single AccelGain {
				get { return accelGain; }
				set {
					if ((double)value < 0 || (double)value > 1)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1 for " + AccelGainProperty.Name + ".");
					SetProperty(ref accelGain, ref value, AccelGainProperty);
				}
			}

			/// <summary>Speed ​​gain</summary>
			/// <remarks>
			/// Japanese short name: "速度ゲイン", Google translated: "Speed ​​gain".
			/// Japanese description: "リジッドの速度の調整パラメータ。低くすると動きが柔らかくなり、高くすると硬くなる。速度ゲインよりも低い値にするらしい。", Google translated: "Adjustment parameters of the velocity of the rigid . Movement is soft and low , it is hard and higher . It seems to be to a value lower than the speed gain .".
			/// </remarks>
			[ParameterTableRowAttribute("velocityGain", index: 3, minimum: 0, maximum: 1, step: 0.01, order: 700, unknown2: 1)]
			[DisplayName("Speed ​​gain")]
			[Description("Adjustment parameters of the velocity of the rigid . Movement is soft and low , it is hard and higher . It seems to be to a value lower than the speed gain .")]
			[DefaultValue((Single)0.6)]
			public Single VelocityGain {
				get { return velocityGain; }
				set {
					if ((double)value < 0 || (double)value > 1)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1 for " + VelocityGainProperty.Name + ".");
					SetProperty(ref velocityGain, ref value, VelocityGainProperty);
				}
			}

			/// <summary>Position gain</summary>
			/// <remarks>
			/// Japanese short name: "位置ゲイン", Google translated: "Position gain".
			/// Japanese description: "リジッドの移動位置の調整パラメータ。低くすると動きが柔らかくなり、高くすると硬くなる。", Google translated: "Adjust parameters of movement of the rigid position . Movement is soft and low , it is hard and higher .".
			/// </remarks>
			[ParameterTableRowAttribute("positionGain", index: 4, minimum: 0, maximum: 1, step: 0.01, order: 800, unknown2: 1)]
			[DisplayName("Position gain")]
			[Description("Adjust parameters of movement of the rigid position . Movement is soft and low , it is hard and higher .")]
			[DefaultValue((Single)0.05)]
			public Single PositionGain {
				get { return positionGain; }
				set {
					if ((double)value < 0 || (double)value > 1)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1 for " + PositionGainProperty.Name + ".");
					SetProperty(ref positionGain, ref value, PositionGainProperty);
				}
			}

			/// <summary>Maximum velocity</summary>
			/// <remarks>
			/// Japanese short name: "最大速度", Google translated: "Maximum velocity".
			/// Japanese description: "リジッドの最大移動速度", Google translated: "Maximum moving speed of the rigid".
			/// </remarks>
			[ParameterTableRowAttribute("maxLinerVelocity", index: 5, minimum: 0, maximum: 10, step: 0.01, order: 900, unknown2: 1)]
			[DisplayName("Maximum velocity")]
			[Description("Maximum moving speed of the rigid")]
			[DefaultValue((Single)1.4)]
			public Single MaxLinerVelocity {
				get { return maxLinerVelocity; }
				set {
					if ((double)value < 0 || (double)value > 10)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 10 for " + MaxLinerVelocityProperty.Name + ".");
					SetProperty(ref maxLinerVelocity, ref value, MaxLinerVelocityProperty);
				}
			}

			/// <summary>Maximal angular velocity</summary>
			/// <remarks>
			/// Japanese short name: "最大角速度", Google translated: "Maximal angular velocity".
			/// Japanese description: "リジッドの最大角速度", Google translated: "Maximum angular velocity of the rigid".
			/// </remarks>
			[ParameterTableRowAttribute("maxAngularVelocity", index: 6, minimum: 0, maximum: 10, step: 0.01, order: 1000, unknown2: 1)]
			[DisplayName("Maximal angular velocity")]
			[Description("Maximum angular velocity of the rigid")]
			[DefaultValue((Single)1.8)]
			public Single MaxAngularVelocity {
				get { return maxAngularVelocity; }
				set {
					if ((double)value < 0 || (double)value > 10)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 10 for " + MaxAngularVelocityProperty.Name + ".");
					SetProperty(ref maxAngularVelocity, ref value, MaxAngularVelocityProperty);
				}
			}

			/// <summary>Snap gain</summary>
			/// <remarks>
			/// Japanese short name: "スナップゲイン", Google translated: "Snap gain".
			/// Japanese description: "元のポーズに近づけるための調整値。位置ゲインと似た効果", Google translated: "Adjustment value to approximate the original pose . Effect similar to the position gain".
			/// </remarks>
			[ParameterTableRowAttribute("snapGain", index: 7, minimum: 0, maximum: 1, step: 0.01, order: 1100, unknown2: 1)]
			[DisplayName("Snap gain")]
			[Description("Adjustment value to approximate the original pose . Effect similar to the position gain")]
			[DefaultValue((Single)0.1)]
			public Single SnapGain {
				get { return snapGain; }
				set {
					if ((double)value < 0 || (double)value > 1)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1 for " + SnapGainProperty.Name + ".");
					SetProperty(ref snapGain, ref value, SnapGainProperty);
				}
			}

			/// <summary>It works</summary>
			/// <remarks>
			/// Japanese short name: "動くか", Google translated: "It works".
			/// Japanese description: "ダメージラグドール時に動くか", Google translated: "Or move to damage during Ragdoll".
			/// </remarks>
			[ParameterTableRowAttribute("enable", index: 8, minimum: 0, maximum: 1, step: 1, order: 100, unknown2: 1)]
			[DisplayName("It works")]
			[Description("Or move to damage during Ragdoll")]
			[DefaultValue(false)]
			public bool Enable {
				get { return enable; }
				set { SetProperty(ref enable, ref value, EnableProperty); }
			}

			/// <summary>Mask number per site</summary>
			/// <remarks>
			/// Japanese short name: "部位あたりマスク番号", Google translated: "Mask number per site".
			/// Japanese description: "部位あたりマスク番号。-1:マスク無効", Google translated: "Mask number per site . -1 : Invalid mask".
			/// </remarks>
			[ParameterTableRowAttribute("partsHitMaskNo", index: 9, minimum: -1, maximum: 7, step: 1, order: 200, unknown2: 1)]
			[DisplayName("Mask number per site")]
			[Description("Mask number per site . -1 : Invalid mask")]
			[DefaultValue((SByte)(-1))]
			public SByte PartsHitMaskNo {
				get { return partsHitMaskNo; }
				set {
					if ((double)value < -1 || (double)value > 7)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 7 for " + PartsHitMaskNoProperty.Name + ".");
					SetProperty(ref partsHitMaskNo, ref value, PartsHitMaskNoProperty);
				}
			}

			/// <summary>Padding</summary>
			/// <remarks>
			/// Japanese short name: "パディング", Google translated: "Padding".
			/// Japanese description: "パディング", Google translated: "Padding".
			/// </remarks>
			[ParameterTableRowAttribute("pad[14]", index: 10, minimum: 0, maximum: 1, step: 1, order: 99999999, unknown2: 1)]
			[DisplayName("Padding")]
			[Description("Padding")]
			[Browsable(false)]
			public Byte[] Pad {
				get { return pad; }
				set { SetProperty(ref pad, ref value, PadProperty); }
			}

			internal Ragdoll(ParameterTable table, int index, AssetLoader loader, int next)
				: base(table, index, loader) {
				BinaryReader reader = loader.Reader;

				HierarchyGain = reader.ReadSingle();
				VelocityDamping = reader.ReadSingle();
				AccelGain = reader.ReadSingle();
				VelocityGain = reader.ReadSingle();
				PositionGain = reader.ReadSingle();
				MaxLinerVelocity = reader.ReadSingle();
				MaxAngularVelocity = reader.ReadSingle();
				SnapGain = reader.ReadSingle();
				Enable = reader.ReadByte() != 0;
				PartsHitMaskNo = reader.ReadSByte();
				Pad = reader.ReadBytes(14);
			}

			internal Ragdoll(ParameterTable table, int index)
				: base(table, index) {
				HierarchyGain = (Single)0.17;
				VelocityDamping = (Single)0;
				AccelGain = (Single)1;
				VelocityGain = (Single)0.6;
				PositionGain = (Single)0.05;
				MaxLinerVelocity = (Single)1.4;
				MaxAngularVelocity = (Single)1.8;
				SnapGain = (Single)0.1;
				Enable = false;
				PartsHitMaskNo = (SByte)(-1);
				Pad = new Byte[14];
			}

			public override void Write(BinaryWriter writer) {
				writer.Write(HierarchyGain);
				writer.Write(VelocityDamping);
				writer.Write(AccelGain);
				writer.Write(VelocityGain);
				writer.Write(PositionGain);
				writer.Write(MaxLinerVelocity);
				writer.Write(MaxAngularVelocity);
				writer.Write(SnapGain);
				writer.Write((Byte)(Enable ? 1 : 0));
				writer.Write(PartsHitMaskNo);
				writer.Write(Pad);
			}
		}
	}
}
