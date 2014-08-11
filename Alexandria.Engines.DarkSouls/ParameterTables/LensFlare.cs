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
		/// Named "LENS_FLARE_BANK" in Dark Souls, defined in the file "LensFlareBank.paramdef" (id 04h).
		/// </remarks>
		public class LensFlare : ParameterTableRow {
			/// <summary>The name of the table in the file.</summary>
			public const string TableName = "LENS_FLARE_BANK";

			SByte texId;
			Byte isFlare, enableRoll, enableScale;
			Single locateDistRate, texScale;
			Int16 colR, colG, colB, colA;

			/// <summary>A property of the class.</summary>
			public static readonly PropertyInfo
				TexIdProperty = GetProperty<LensFlare>("TexId"),
				IsFlareProperty = GetProperty<LensFlare>("IsFlare"),
				EnableRollProperty = GetProperty<LensFlare>("EnableRoll"),
				EnableScaleProperty = GetProperty<LensFlare>("EnableScale"),
				LocateDistRateProperty = GetProperty<LensFlare>("LocateDistRate"),
				TexScaleProperty = GetProperty<LensFlare>("TexScale"),
				ColRProperty = GetProperty<LensFlare>("ColR"),
				ColGProperty = GetProperty<LensFlare>("ColG"),
				ColBProperty = GetProperty<LensFlare>("ColB"),
				ColAProperty = GetProperty<LensFlare>("ColA");

			/// <summary>Texture ID</summary>
			/// <remarks>
			/// Japanese short name: "テクスチャID", Google translated: "Texture ID".
			/// Japanese description: "テクスチャ(lensflare_??)", Google translated: "Texture (lensflare_??)".
			/// </remarks>
			[ParameterTableRowAttribute("texId", index: 0, minimum: -1, maximum: 99, step: 1, sortOrder: 1, unknown2: 0)]
			[DisplayName("Texture ID")]
			[Description("Texture (lensflare_??)")]
			[DefaultValue((SByte)(-1))]
			public SByte TexId {
				get { return texId; }
				set {
					if ((double)value < -1 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 99 for TexId.");
					SetProperty(ref texId, ref value, TexIdProperty);
				}
			}

			/// <summary>Or flare</summary>
			/// <remarks>
			/// Japanese short name: "フレアか", Google translated: "Or flare".
			/// Japanese description: "0:ゴースト, 1:フレア", Google translated: "0: ghost, 1: flare".
			/// </remarks>
			[ParameterTableRowAttribute("isFlare", index: 1, minimum: 0, maximum: 1, step: 1, sortOrder: 2, unknown2: 1)]
			[DisplayName("Or flare")]
			[Description("0: ghost, 1: flare")]
			[DefaultValue((Byte)0)]
			public Byte IsFlare {
				get { return isFlare; }
				set {
					if ((double)value < 0 || (double)value > 1)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1 for IsFlare.");
					SetProperty(ref isFlare, ref value, IsFlareProperty);
				}
			}

			/// <summary>Enable rotation</summary>
			/// <remarks>
			/// Japanese short name: "回転有効", Google translated: "Enable rotation".
			/// Japanese description: "0:無効, 1:有効", Google translated: "0: Disabled, 1: Enabled".
			/// </remarks>
			[ParameterTableRowAttribute("enableRoll", index: 2, minimum: 0, maximum: 1, step: 1, sortOrder: 3, unknown2: 1)]
			[DisplayName("Enable rotation")]
			[Description("0: Disabled, 1: Enabled")]
			[DefaultValue((Byte)0)]
			public Byte EnableRoll {
				get { return enableRoll; }
				set {
					if ((double)value < 0 || (double)value > 1)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1 for EnableRoll.");
					SetProperty(ref enableRoll, ref value, EnableRollProperty);
				}
			}

			/// <summary>Scale effective</summary>
			/// <remarks>
			/// Japanese short name: "スケール有効", Google translated: "Scale effective".
			/// Japanese description: "0:無効, 1:有効", Google translated: "0: Disabled, 1: Enabled".
			/// </remarks>
			[ParameterTableRowAttribute("enableScale", index: 3, minimum: 0, maximum: 1, step: 1, sortOrder: 4, unknown2: 1)]
			[DisplayName("Scale effective")]
			[Description("0: Disabled, 1: Enabled")]
			[DefaultValue((Byte)0)]
			public Byte EnableScale {
				get { return enableScale; }
				set {
					if ((double)value < 0 || (double)value > 1)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1 for EnableScale.");
					SetProperty(ref enableScale, ref value, EnableScaleProperty);
				}
			}

			/// <summary>Placement range rate</summary>
			/// <remarks>
			/// Japanese short name: "配置距離率", Google translated: "Placement range rate".
			/// Japanese description: "0.0:光源位置～1.0:画面中心", Google translated: "The center of the screen: 0.0: 1.0-source position".
			/// </remarks>
			[ParameterTableRowAttribute("locateDistRate", index: 4, minimum: -10, maximum: 10, step: 0.01, sortOrder: 5, unknown2: 0)]
			[DisplayName("Placement range rate")]
			[Description("The center of the screen: 0.0: 1.0-source position")]
			[DefaultValue((Single)0)]
			public Single LocateDistRate {
				get { return locateDistRate; }
				set {
					if ((double)value < -10 || (double)value > 10)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -10 to 10 for LocateDistRate.");
					SetProperty(ref locateDistRate, ref value, LocateDistRateProperty);
				}
			}

			/// <summary>Texture scale</summary>
			/// <remarks>
			/// Japanese short name: "テクスチャスケール", Google translated: "Texture scale".
			/// Japanese description: "テクスチャのスケール", Google translated: "Scale of the texture".
			/// </remarks>
			[ParameterTableRowAttribute("texScale", index: 5, minimum: 0, maximum: 100, step: 0.1, sortOrder: 6, unknown2: 0)]
			[DisplayName("Texture scale")]
			[Description("Scale of the texture")]
			[DefaultValue((Single)1)]
			public Single TexScale {
				get { return texScale; }
				set {
					if ((double)value < 0 || (double)value > 100)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 100 for TexScale.");
					SetProperty(ref texScale, ref value, TexScaleProperty);
				}
			}

			/// <summary>R</summary>
			/// <remarks>
			/// Japanese short name: "Ｒ", Google translated: "R".
			/// Japanese description: "テクスチャ乗算色", Google translated: "Texture multiply color".
			/// </remarks>
			[ParameterTableRowAttribute("colR", index: 6, minimum: 0, maximum: 255, step: 1, sortOrder: 7, unknown2: 0)]
			[DisplayName("R")]
			[Description("Texture multiply color")]
			[DefaultValue((Int16)255)]
			public Int16 ColR {
				get { return colR; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColR.");
					SetProperty(ref colR, ref value, ColRProperty);
				}
			}

			/// <summary>G</summary>
			/// <remarks>
			/// Japanese short name: "Ｇ", Google translated: "G".
			/// Japanese description: "テクスチャ乗算色", Google translated: "Texture multiply color".
			/// </remarks>
			[ParameterTableRowAttribute("colG", index: 7, minimum: 0, maximum: 255, step: 1, sortOrder: 8, unknown2: 0)]
			[DisplayName("G")]
			[Description("Texture multiply color")]
			[DefaultValue((Int16)255)]
			public Int16 ColG {
				get { return colG; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColG.");
					SetProperty(ref colG, ref value, ColGProperty);
				}
			}

			/// <summary>B</summary>
			/// <remarks>
			/// Japanese short name: "Ｂ", Google translated: "B".
			/// Japanese description: "テクスチャ乗算色", Google translated: "Texture multiply color".
			/// </remarks>
			[ParameterTableRowAttribute("colB", index: 8, minimum: 0, maximum: 255, step: 1, sortOrder: 9, unknown2: 0)]
			[DisplayName("B")]
			[Description("Texture multiply color")]
			[DefaultValue((Int16)255)]
			public Int16 ColB {
				get { return colB; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColB.");
					SetProperty(ref colB, ref value, ColBProperty);
				}
			}

			/// <summary>A</summary>
			/// <remarks>
			/// Japanese short name: "Ａ", Google translated: "A".
			/// Japanese description: "テクスチャ乗算色", Google translated: "Texture multiply color".
			/// </remarks>
			[ParameterTableRowAttribute("colA", index: 9, minimum: 0, maximum: 255, step: 1, sortOrder: 10, unknown2: 0)]
			[DisplayName("A")]
			[Description("Texture multiply color")]
			[DefaultValue((Int16)255)]
			public Int16 ColA {
				get { return colA; }
				set {
					if ((double)value < 0 || (double)value > 255)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 255 for ColA.");
					SetProperty(ref colA, ref value, ColAProperty);
				}
			}

			internal LensFlare(ParameterTable table, int index, AssetLoader loader, int next)
				: base(table, index, loader) {
				BinaryReader reader = loader.Reader;

				TexId = reader.ReadSByte();
				IsFlare = reader.ReadByte();
				EnableRoll = reader.ReadByte();
				EnableScale = reader.ReadByte();
				LocateDistRate = reader.ReadSingle();
				TexScale = reader.ReadSingle();
				ColR = reader.ReadInt16();
				ColG = reader.ReadInt16();
				ColB = reader.ReadInt16();
				ColA = reader.ReadInt16();
			}

			internal LensFlare(ParameterTable table, int index)
				: base(table, index) {
				TexId = (SByte)(-1);
				IsFlare = (Byte)0;
				EnableRoll = (Byte)0;
				EnableScale = (Byte)0;
				LocateDistRate = (Single)0;
				TexScale = (Single)1;
				ColR = (Int16)255;
				ColG = (Int16)255;
				ColB = (Int16)255;
				ColA = (Int16)255;
			}

			/// <summary>Write the <see cref="LensFlare"/> row.</summary>
			/// <param name="writer"></param>
			public override void Write(BinaryWriter writer) {
				writer.Write(TexId);
				writer.Write(IsFlare);
				writer.Write(EnableRoll);
				writer.Write(EnableScale);
				writer.Write(LocateDistRate);
				writer.Write(TexScale);
				writer.Write(ColR);
				writer.Write(ColG);
				writer.Write(ColB);
				writer.Write(ColA);
			}
		}
	}
}
