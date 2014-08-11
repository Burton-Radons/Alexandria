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
		/// Defined as "MENU_PARAM_COLOR_TABLE_ST" in Dark Souls in the file "MenuParamColorTable.paramdef" (id 22h).
		/// </remarks>
		public class MenuColor : ParameterTableRow {
			/// <summary>The name of the table in the file.</summary>
			public const string TableName = "MENU_PARAM_COLOR_TABLE_ST";

			Byte r, g, b, a;

			/// <summary>A property in the class.</summary>
			public static readonly PropertyInfo
				RProperty = GetProperty<MenuColor>("R"),
				GProperty = GetProperty<MenuColor>("G"),
				BProperty = GetProperty<MenuColor>("B"),
				AProperty = GetProperty<MenuColor>("A");

			/// <summary>Red</summary>
			/// <remarks>
			/// Japanese short name: "赤色", Google translated: "Red".
			/// Japanese description: "カラーテーブルの赤色", Google translated: "Red in the color table".
			/// </remarks>
			[ParameterTableRowAttribute("r", index: 0, minimum: 0, maximum: 255, step: 1, sortOrder: 1, unknown2: 1)]
			[DisplayName("Red")]
			[Description("Red in the color table")]
			[DefaultValue((Byte)255)]
			public Byte R {
				get { return r; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for R.");
					SetProperty(ref r, ref value, RProperty);
				}
			}

			/// <summary>Green</summary>
			/// <remarks>
			/// Japanese short name: "緑色", Google translated: "Green".
			/// Japanese description: "カラーテーブルの緑", Google translated: "Green the color table".
			/// </remarks>
			[ParameterTableRowAttribute("g", index: 1, minimum: 0, maximum: 255, step: 1, sortOrder: 2, unknown2: 1)]
			[DisplayName("Green")]
			[Description("Green the color table")]
			[DefaultValue((Byte)255)]
			public Byte G {
				get { return g; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for G.");
					SetProperty(ref g, ref value, GProperty);
				}
			}

			/// <summary>Blue</summary>
			/// <remarks>
			/// Japanese short name: "青色", Google translated: "Blue".
			/// Japanese description: "カラーテーブルの青", Google translated: "Blue color table".
			/// </remarks>
			[ParameterTableRowAttribute("b", index: 2, minimum: 0, maximum: 255, step: 1, sortOrder: 3, unknown2: 1)]
			[DisplayName("Blue")]
			[Description("Blue color table")]
			[DefaultValue((Byte)255)]
			public Byte B {
				get { return b; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for B.");
					SetProperty(ref b, ref value, BProperty);
				}
			}

			/// <summary>Alpha</summary>
			/// <remarks>
			/// Japanese short name: "アルファ", Google translated: "Alpha".
			/// Japanese description: "カラーテーブルのアルファ", Google translated: "Alpha in the color table".
			/// </remarks>
			[ParameterTableRowAttribute("a", index: 3, minimum: 0, maximum: 255, step: 1, sortOrder: 4, unknown2: 1)]
			[DisplayName("Alpha")]
			[Description("Alpha in the color table")]
			[DefaultValue((Byte)255)]
			public Byte A {
				get { return a; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for A.");
					SetProperty(ref a, ref value, AProperty);
				}
			}

			internal MenuColor(ParameterTable table, int index, AssetLoader loader, int next)
				: base(table, index, loader) {
				BinaryReader reader = loader.Reader;

				R = reader.ReadByte();
				G = reader.ReadByte();
				B = reader.ReadByte();
				A = reader.ReadByte();
			}

			internal MenuColor(ParameterTable table, int index)
				: base(table, index) {
				R = (Byte)255;
				G = (Byte)255;
				B = (Byte)255;
				A = (Byte)255;
			}

			/// <summary>
			/// Write the <see cref="MenuColor"/> row.
			/// </summary>
			/// <param name="writer"></param>
			public override void Write(BinaryWriter writer) {
				writer.Write(R);
				writer.Write(G);
				writer.Write(B);
				writer.Write(A);
			}
		}
	}
}
