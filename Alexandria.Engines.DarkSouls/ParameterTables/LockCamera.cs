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
		/// Defined as "LOCK_CAM_PARAM_ST" in the file "LockCaramParam.paramdef" (id 2Eh).
		/// </remarks>
		public class LockCamera : ParameterTableRow {
			/// <summary>The name of the table in the file.</summary>
			public const string TableName = "LOCK_CAM_PARAM_ST";

			Single camDistTarget, rotRangeMinX, lockRotXShiftRatio, chrOrgOffset_Y, chrLockRangeMaxRadius, camFovY;
			Byte[] pad;

			/// <summary>A property of the class.</summary>
			public static readonly PropertyInfo
				CamDistTargetProperty = GetProperty<LockCamera>("CamDistTarget"),
				RotRangeMinXProperty = GetProperty<LockCamera>("RotRangeMinX"),
				LockRotXShiftRatioProperty = GetProperty<LockCamera>("LockRotXShiftRatio"),
				ChrOrgOffset_YProperty = GetProperty<LockCamera>("ChrOrgOffset_Y"),
				ChrLockRangeMaxRadiusProperty = GetProperty<LockCamera>("ChrLockRangeMaxRadius"),
				CamFovYProperty = GetProperty<LockCamera>("CamFovY"),
				PadProperty = GetProperty<LockCamera>("Pad");

			/// <summary>Camera distance goal [m]</summary>
			/// <remarks>
			/// Japanese short name: "カメラ距離目標[m]", Google translated: "Camera distance goal [m]".
			/// Japanese description: "カメラ用", Google translated: "Camera".
			/// </remarks>
			[ParameterTableRowAttribute("camDistTarget", index: 0, minimum: 0.1, maximum: 100, step: 0.01, sortOrder: 1, unknown2: 0)]
			[DisplayName("Camera distance goal [m]")]
			[Description("Camera")]
			[DefaultValue((Single)4)]
			public Single CamDistTarget {
				get { return camDistTarget; }
				set {
					if ((double)value < 0.1 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0.1 to 100 for CamDistTarget.");
					SetProperty(ref camDistTarget, ref value, CamDistTargetProperty);
				}
			}

			/// <summary>X-axis rotation minimum value [deg]</summary>
			/// <remarks>
			/// Japanese short name: "X軸回転最小値[deg]", Google translated: "X-axis rotation minimum value [deg]".
			/// Japanese description: "カメラ用", Google translated: "Camera".
			/// </remarks>
			[ParameterTableRowAttribute("rotRangeMinX", index: 1, minimum: -80, maximum: 80, step: 0.1, sortOrder: 2, unknown2: 0)]
			[DisplayName("X-axis rotation minimum value [deg]")]
			[Description("Camera")]
			[DefaultValue((Single)(-40))]
			public Single RotRangeMinX {
				get { return rotRangeMinX; }
				set {
					if ((double)value < -80 || (double)value > 80)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -80 to 80 for RotRangeMinX.");
					SetProperty(ref rotRangeMinX, ref value, RotRangeMinXProperty);
				}
			}

			/// <summary>X lock rotation shift ratio (0.0 and 1.0)</summary>
			/// <remarks>
			/// Japanese short name: "ロックX回転シフト率(0.0～1.0)", Google translated: "X lock rotation shift ratio (0.0 and 1.0)".
			/// Japanese description: "カメラ用", Google translated: "Camera".
			/// </remarks>
			[ParameterTableRowAttribute("lockRotXShiftRatio", index: 2, minimum: 0, maximum: 1, step: 0.01, sortOrder: 3, unknown2: 0)]
			[DisplayName("X lock rotation shift ratio (0.0 and 1.0)")]
			[Description("Camera")]
			[DefaultValue((Single)0.6)]
			public Single LockRotXShiftRatio {
				get { return lockRotXShiftRatio; }
				set {
					if ((double)value < 0 || (double)value > 1)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1 for LockRotXShiftRatio.");
					SetProperty(ref lockRotXShiftRatio, ref value, LockRotXShiftRatioProperty);
				}
			}

			/// <summary>Character origin offset (character space)</summary>
			/// <remarks>
			/// Japanese short name: "キャラ基点オフセット(キャラ空間)", Google translated: "Character origin offset (character space)".
			/// Japanese description: "カメラ用", Google translated: "Camera".
			/// </remarks>
			[ParameterTableRowAttribute("chrOrgOffset_Y", index: 3, minimum: -10, maximum: 10, step: 0.01, sortOrder: 4, unknown2: 0)]
			[DisplayName("Character origin offset (character space)")]
			[Description("Camera")]
			[DefaultValue((Single)1.42)]
			public Single ChrOrgOffset_Y {
				get { return chrOrgOffset_Y; }
				set {
					if ((double)value < -10 || (double)value > 10)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -10 to 10 for ChrOrgOffset_Y.");
					SetProperty(ref chrOrgOffset_Y, ref value, ChrOrgOffset_YProperty);
				}
			}

			/// <summary>Character range maximum radius [m]</summary>
			/// <remarks>
			/// Japanese short name: "キャラ範囲最大半径[m]", Google translated: "Character range maximum radius [m]".
			/// Japanese description: "ロック用", Google translated: "Locking".
			/// </remarks>
			[ParameterTableRowAttribute("chrLockRangeMaxRadius", index: 4, minimum: 0, maximum: 30, step: 0.01, sortOrder: 5, unknown2: 0)]
			[DisplayName("Character range maximum radius [m]")]
			[Description("Locking")]
			[DefaultValue((Single)15)]
			public Single ChrLockRangeMaxRadius {
				get { return chrLockRangeMaxRadius; }
				set {
					if ((double)value < 0 || (double)value > 30)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 30 for ChrLockRangeMaxRadius.");
					SetProperty(ref chrLockRangeMaxRadius, ref value, ChrLockRangeMaxRadiusProperty);
				}
			}

			/// <summary>Tate-ga angle [deg]</summary>
			/// <remarks>
			/// Japanese short name: "縦画角[deg]", Google translated: "Tate-ga angle [deg]".
			/// Japanese description: "カメラ用", Google translated: "Camera".
			/// </remarks>
			[ParameterTableRowAttribute("camFovY", index: 5, minimum: 38, maximum: 48, step: 0.1, sortOrder: 6, unknown2: 0)]
			[DisplayName("Tate-ga angle [deg]")]
			[Description("Camera")]
			[DefaultValue((Single)43)]
			public Single CamFovY {
				get { return camFovY; }
				set {
					if ((double)value < 38 || (double)value > 48)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 38 to 48 for CamFovY.");
					SetProperty(ref camFovY, ref value, CamFovYProperty);
				}
			}

			/// <summary>Padding</summary>
			/// <remarks>
			/// Japanese short name: "パディング", Google translated: "Padding".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("pad[8]", index: 6, minimum: 0, maximum: 30, step: 0.01, sortOrder: 8, unknown2: 0)]
			[DisplayName("Padding")]
			[Description("")]
			[Browsable(false)]
			public Byte[] Pad {
				get { return pad; }
				set { SetProperty(ref pad, ref value, PadProperty); }
			}

			internal LockCamera(ParameterTable table, int index, AssetLoader loader, int next)
				: base(table, index, loader) {
				BinaryReader reader = loader.Reader;

				CamDistTarget = reader.ReadSingle();
				RotRangeMinX = reader.ReadSingle();
				LockRotXShiftRatio = reader.ReadSingle();
				ChrOrgOffset_Y = reader.ReadSingle();
				ChrLockRangeMaxRadius = reader.ReadSingle();
				CamFovY = reader.ReadSingle();
				Pad = reader.ReadBytes(8);
			}

			internal LockCamera(ParameterTable table, int index)
				: base(table, index) {
				CamDistTarget = (Single)4;
				RotRangeMinX = (Single)(-40);
				LockRotXShiftRatio = (Single)0.6;
				ChrOrgOffset_Y = (Single)1.42;
				ChrLockRangeMaxRadius = (Single)15;
				CamFovY = (Single)43;
				Pad = new Byte[8];
			}

			/// <summary>Write the <see cref="LockCamera"/> row.</summary>
			/// <param name="writer"></param>
			public override void Write(BinaryWriter writer) {
				writer.Write(CamDistTarget);
				writer.Write(RotRangeMinX);
				writer.Write(LockRotXShiftRatio);
				writer.Write(ChrOrgOffset_Y);
				writer.Write(ChrLockRangeMaxRadius);
				writer.Write(CamFovY);
				writer.Write(Pad);
			}
		}
	}
}
