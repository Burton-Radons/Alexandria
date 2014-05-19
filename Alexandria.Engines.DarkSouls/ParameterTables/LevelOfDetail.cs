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
		/// Defined as "LOD_BANK" in the file "LodBank.paramdef" (id 0Bh).
		/// </remarks>
		public class LevelOfDetail : ParameterTableRow {
			public const string TableName = "LOD_BANK";

			Single lv01_BorderDist, lv01_PlayDist, lv12_BorderDist, lv12_PlayDist;

			public static readonly PropertyInfo
				Lv01_BorderDistProperty = GetProperty<LevelOfDetail>("Lv01_BorderDist"),
				Lv01_PlayDistProperty = GetProperty<LevelOfDetail>("Lv01_PlayDist"),
				Lv12_BorderDistProperty = GetProperty<LevelOfDetail>("Lv12_BorderDist"),
				Lv12_PlayDistProperty = GetProperty<LevelOfDetail>("Lv12_PlayDist");

			/// <summary>LOD level 0-1 boundary distance [m]</summary>
			/// <remarks>
			/// Japanese short name: "LODレベル0-1境界距離[m]", Google translated: "LOD level 0-1 boundary distance [m]".
			/// Japanese description: "切り替わる中心", Google translated: "Center to switch".
			/// </remarks>
			[ParameterTableRowAttribute("lv01_BorderDist", index: 0, minimum: 0, maximum: 9999, step: 0.1, order: 1, unknown2: 0)]
			[DisplayName("LOD level 0-1 boundary distance [m]")]
			[Description("Center to switch")]
			[DefaultValue((Single)5)]
			public Single Lv01_BorderDist {
				get { return lv01_BorderDist; }
				set {
					if ((double)value < 0 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9999 for Lv01_BorderDist.");
					SetProperty(ref lv01_BorderDist, ref value, Lv01_BorderDistProperty);
				}
			}

			/// <summary>LOD level 0-1 play distance [m]</summary>
			/// <remarks>
			/// Japanese short name: "LODレベル0-1遊び距離[m]", Google translated: "LOD level 0-1 play distance [m]".
			/// Japanese description: "境界中心で±遊び", Google translated: "± play center at the border".
			/// </remarks>
			[ParameterTableRowAttribute("lv01_PlayDist", index: 1, minimum: 0, maximum: 10, step: 0.1, order: 2, unknown2: 0)]
			[DisplayName("LOD level 0-1 play distance [m]")]
			[Description("± play center at the border")]
			[DefaultValue((Single)1)]
			public Single Lv01_PlayDist {
				get { return lv01_PlayDist; }
				set {
					if ((double)value < 0 || (double)value > 10)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 10 for Lv01_PlayDist.");
					SetProperty(ref lv01_PlayDist, ref value, Lv01_PlayDistProperty);
				}
			}

			/// <summary>LOD Level 1-2 boundary distance [m]</summary>
			/// <remarks>
			/// Japanese short name: "LODレベル1-2境界距離[m]", Google translated: "LOD Level 1-2 boundary distance [m]".
			/// Japanese description: "切り替わる中心", Google translated: "Center to switch".
			/// </remarks>
			[ParameterTableRowAttribute("lv12_BorderDist", index: 2, minimum: 0, maximum: 9999, step: 0.1, order: 3, unknown2: 0)]
			[DisplayName("LOD Level 1-2 boundary distance [m]")]
			[Description("Center to switch")]
			[DefaultValue((Single)20)]
			public Single Lv12_BorderDist {
				get { return lv12_BorderDist; }
				set {
					if ((double)value < 0 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9999 for Lv12_BorderDist.");
					SetProperty(ref lv12_BorderDist, ref value, Lv12_BorderDistProperty);
				}
			}

			/// <summary>LOD level 1-2 play distance [m]</summary>
			/// <remarks>
			/// Japanese short name: "LODレベル1-2遊び距離[m]", Google translated: "LOD level 1-2 play distance [m]".
			/// Japanese description: "境界中心で±遊び", Google translated: "± play center at the border".
			/// </remarks>
			[ParameterTableRowAttribute("lv12_PlayDist", index: 3, minimum: 0, maximum: 10, step: 0.1, order: 4, unknown2: 0)]
			[DisplayName("LOD level 1-2 play distance [m]")]
			[Description("± play center at the border")]
			[DefaultValue((Single)2)]
			public Single Lv12_PlayDist {
				get { return lv12_PlayDist; }
				set {
					if ((double)value < 0 || (double)value > 10)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 10 for Lv12_PlayDist.");
					SetProperty(ref lv12_PlayDist, ref value, Lv12_PlayDistProperty);
				}
			}

			internal LevelOfDetail(ParameterTable table, int index, AssetLoader loader, int next)
				: base(table, index, loader) {
				BinaryReader reader = loader.Reader;

				Lv01_BorderDist = reader.ReadSingle();
				Lv01_PlayDist = reader.ReadSingle();
				Lv12_BorderDist = reader.ReadSingle();
				Lv12_PlayDist = reader.ReadSingle();
			}

			internal LevelOfDetail(ParameterTable table, int index)
				: base(table, index) {
				Lv01_BorderDist = (Single)5;
				Lv01_PlayDist = (Single)1;
				Lv12_BorderDist = (Single)20;
				Lv12_PlayDist = (Single)2;
			}

			public override void Write(BinaryWriter writer) {
				writer.Write(Lv01_BorderDist);
				writer.Write(Lv01_PlayDist);
				writer.Write(Lv12_BorderDist);
				writer.Write(Lv12_PlayDist);
			}
		}
	}
}
