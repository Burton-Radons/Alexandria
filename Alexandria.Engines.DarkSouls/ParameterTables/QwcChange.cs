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
		/// Defined as "QWC_CHANGE_PARAM_ST" in Dark Souls in the file "QwcChangeParam.paramdef" (id 27h).
		/// </remarks>
		public class QwcChange : ParameterTableRow {
			/// <summary>The name of the table in the file.</summary>
			public const string TableName = "QWC_CHANGE_PARAM_ST";

			Int16 pcAttrB, pcAttrW, pcAttrL, pcAttrR, areaAttrB, areaAttrW, areaAttrL, areaAttrR;

			/// <summary>A property in the class.</summary>
			public static readonly PropertyInfo
				PcAttrBProperty = GetProperty<QwcChange>("PcAttrB"),
				PcAttrWProperty = GetProperty<QwcChange>("PcAttrW"),
				PcAttrLProperty = GetProperty<QwcChange>("PcAttrL"),
				PcAttrRProperty = GetProperty<QwcChange>("PcAttrR"),
				AreaAttrBProperty = GetProperty<QwcChange>("AreaAttrB"),
				AreaAttrWProperty = GetProperty<QwcChange>("AreaAttrW"),
				AreaAttrLProperty = GetProperty<QwcChange>("AreaAttrL"),
				AreaAttrRProperty = GetProperty<QwcChange>("AreaAttrR");

			/// <summary>PC-Black</summary>
			/// <remarks>
			/// Japanese short name: "PC-黒", Google translated: "PC-Black".
			/// Japanese description: "PC黒属性変化値", Google translated: "PC black attribute change value".
			/// </remarks>
			[ParameterTableRowAttribute("pcAttrB", index: 0, minimum: 0, maximum: 200, step: 1, sortOrder: 100, unknown2: 1)]
			[DisplayName("PC-Black")]
			[Description("PC black attribute change value")]
			[DefaultValue((Int16)0)]
			public Int16 PcAttrB {
				get { return pcAttrB; }
				set {
					if ((double)value < 0 || (double)value > 200)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 200 for PcAttrB.");
					SetProperty(ref pcAttrB, ref value, PcAttrBProperty);
				}
			}

			/// <summary>PC-White</summary>
			/// <remarks>
			/// Japanese short name: "PC-白", Google translated: "PC-White".
			/// Japanese description: "PC白属性変化値", Google translated: "PC white attribute change value".
			/// </remarks>
			[ParameterTableRowAttribute("pcAttrW", index: 1, minimum: 0, maximum: 200, step: 1, sortOrder: 200, unknown2: 1)]
			[DisplayName("PC-White")]
			[Description("PC white attribute change value")]
			[DefaultValue((Int16)0)]
			public Int16 PcAttrW {
				get { return pcAttrW; }
				set {
					if ((double)value < 0 || (double)value > 200)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 200 for PcAttrW.");
					SetProperty(ref pcAttrW, ref value, PcAttrWProperty);
				}
			}

			/// <summary>PC-left</summary>
			/// <remarks>
			/// Japanese short name: "PC-左", Google translated: "PC-left".
			/// Japanese description: "PC左属性変化値", Google translated: "PC left attribute change value".
			/// </remarks>
			[ParameterTableRowAttribute("pcAttrL", index: 2, minimum: 0, maximum: 200, step: 1, sortOrder: 300, unknown2: 1)]
			[DisplayName("PC-left")]
			[Description("PC left attribute change value")]
			[DefaultValue((Int16)0)]
			public Int16 PcAttrL {
				get { return pcAttrL; }
				set {
					if ((double)value < 0 || (double)value > 200)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 200 for PcAttrL.");
					SetProperty(ref pcAttrL, ref value, PcAttrLProperty);
				}
			}

			/// <summary>PC-right</summary>
			/// <remarks>
			/// Japanese short name: "PC-右", Google translated: "PC-right".
			/// Japanese description: "PC右属性変化値", Google translated: "PC right attribute value change".
			/// </remarks>
			[ParameterTableRowAttribute("pcAttrR", index: 3, minimum: 0, maximum: 200, step: 1, sortOrder: 400, unknown2: 1)]
			[DisplayName("PC-right")]
			[Description("PC right attribute value change")]
			[DefaultValue((Int16)0)]
			public Int16 PcAttrR {
				get { return pcAttrR; }
				set {
					if ((double)value < 0 || (double)value > 200)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 200 for PcAttrR.");
					SetProperty(ref pcAttrR, ref value, PcAttrRProperty);
				}
			}

			/// <summary>Area - black</summary>
			/// <remarks>
			/// Japanese short name: "エリア-黒", Google translated: "Area - black".
			/// Japanese description: "エリア黒属性変化値", Google translated: "Black area attribute change value".
			/// </remarks>
			[ParameterTableRowAttribute("areaAttrB", index: 4, minimum: 0, maximum: 200, step: 1, sortOrder: 500, unknown2: 1)]
			[DisplayName("Area - black")]
			[Description("Black area attribute change value")]
			[DefaultValue((Int16)0)]
			public Int16 AreaAttrB {
				get { return areaAttrB; }
				set {
					if ((double)value < 0 || (double)value > 200)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 200 for AreaAttrB.");
					SetProperty(ref areaAttrB, ref value, AreaAttrBProperty);
				}
			}

			/// <summary>Area - White</summary>
			/// <remarks>
			/// Japanese short name: "エリア-白", Google translated: "Area - White".
			/// Japanese description: "エリア白属性変化値", Google translated: "White area attribute change value".
			/// </remarks>
			[ParameterTableRowAttribute("areaAttrW", index: 5, minimum: 0, maximum: 200, step: 1, sortOrder: 600, unknown2: 1)]
			[DisplayName("Area - White")]
			[Description("White area attribute change value")]
			[DefaultValue((Int16)0)]
			public Int16 AreaAttrW {
				get { return areaAttrW; }
				set {
					if ((double)value < 0 || (double)value > 200)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 200 for AreaAttrW.");
					SetProperty(ref areaAttrW, ref value, AreaAttrWProperty);
				}
			}

			/// <summary>Area - left</summary>
			/// <remarks>
			/// Japanese short name: "エリア-左", Google translated: "Area - left".
			/// Japanese description: "エリア左属性変化値", Google translated: "Area left attribute change value".
			/// </remarks>
			[ParameterTableRowAttribute("areaAttrL", index: 6, minimum: 0, maximum: 200, step: 1, sortOrder: 700, unknown2: 1)]
			[DisplayName("Area - left")]
			[Description("Area left attribute change value")]
			[DefaultValue((Int16)0)]
			public Int16 AreaAttrL {
				get { return areaAttrL; }
				set {
					if ((double)value < 0 || (double)value > 200)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 200 for AreaAttrL.");
					SetProperty(ref areaAttrL, ref value, AreaAttrLProperty);
				}
			}

			/// <summary>Area - right</summary>
			/// <remarks>
			/// Japanese short name: "エリア-右", Google translated: "Area - right".
			/// Japanese description: "エリア右属性変化値", Google translated: "Area right attribute value change".
			/// </remarks>
			[ParameterTableRowAttribute("areaAttrR", index: 7, minimum: 0, maximum: 200, step: 1, sortOrder: 800, unknown2: 1)]
			[DisplayName("Area - right")]
			[Description("Area right attribute value change")]
			[DefaultValue((Int16)0)]
			public Int16 AreaAttrR {
				get { return areaAttrR; }
				set {
					if ((double)value < 0 || (double)value > 200)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 200 for AreaAttrR.");
					SetProperty(ref areaAttrR, ref value, AreaAttrRProperty);
				}
			}

			internal QwcChange(ParameterTable table, int index, AssetLoader loader, int next)
				: base(table, index, loader) {
				BinaryReader reader = loader.Reader;

				PcAttrB = reader.ReadInt16();
				PcAttrW = reader.ReadInt16();
				PcAttrL = reader.ReadInt16();
				PcAttrR = reader.ReadInt16();
				AreaAttrB = reader.ReadInt16();
				AreaAttrW = reader.ReadInt16();
				AreaAttrL = reader.ReadInt16();
				AreaAttrR = reader.ReadInt16();
			}

			internal QwcChange(ParameterTable table, int index)
				: base(table, index) {
				PcAttrB = (Int16)0;
				PcAttrW = (Int16)0;
				PcAttrL = (Int16)0;
				PcAttrR = (Int16)0;
				AreaAttrB = (Int16)0;
				AreaAttrW = (Int16)0;
				AreaAttrL = (Int16)0;
				AreaAttrR = (Int16)0;
			}

			/// <summary>
			/// Write the <see cref="QwcChange"/> row.
			/// </summary>
			/// <param name="writer"></param>
			public override void Write(BinaryWriter writer) {
				writer.Write(PcAttrB);
				writer.Write(PcAttrW);
				writer.Write(PcAttrL);
				writer.Write(PcAttrR);
				writer.Write(AreaAttrB);
				writer.Write(AreaAttrW);
				writer.Write(AreaAttrL);
				writer.Write(AreaAttrR);
			}
		}
	}
}
