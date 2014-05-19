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
		/// <remarks>From EquipMtrlSetParam.paramdef (id 12h).</remarks>
		public class MaterialSet : ParameterTableRow {
			public const string TableName = "EQUIP_MTRL_SET_PARAM_ST";

			Int32 materialId01, materialId02, materialId03, materialId04, materialId05;
			SByte itemNum01, itemNum02, itemNum03, itemNum04, itemNum05;
			Byte[] pad;

			public static readonly PropertyInfo
				MaterialId01Property = GetProperty<MaterialSet>("MaterialId01"),
				MaterialId02Property = GetProperty<MaterialSet>("MaterialId02"),
				MaterialId03Property = GetProperty<MaterialSet>("MaterialId03"),
				MaterialId04Property = GetProperty<MaterialSet>("MaterialId04"),
				MaterialId05Property = GetProperty<MaterialSet>("MaterialId05"),
				ItemNum01Property = GetProperty<MaterialSet>("ItemNum01"),
				ItemNum02Property = GetProperty<MaterialSet>("ItemNum02"),
				ItemNum03Property = GetProperty<MaterialSet>("ItemNum03"),
				ItemNum04Property = GetProperty<MaterialSet>("ItemNum04"),
				ItemNum05Property = GetProperty<MaterialSet>("ItemNum05"),
				IsDisableDispNum01Property = GetProperty<MaterialSet>("IsDisableDispNum01"),
				IsDisableDispNum02Property = GetProperty<MaterialSet>("IsDisableDispNum02"),
				IsDisableDispNum03Property = GetProperty<MaterialSet>("IsDisableDispNum03"),
				IsDisableDispNum04Property = GetProperty<MaterialSet>("IsDisableDispNum04"),
				IsDisableDispNum05Property = GetProperty<MaterialSet>("IsDisableDispNum05"),
				PadProperty = GetProperty<MaterialSet>("Pad");

			/// <summary>Need material items ID01</summary>
			/// <remarks>
			/// Japanese short name: "必要素材アイテムID01", Google translated: "Need material items ID01".
			/// Japanese description: "武具強化に必要な素材アイテムIDです。", Google translated: "It is the material item ID required to strengthen armor .".
			/// </remarks>
			[ParameterTableRowAttribute("materialId01", index: 0, minimum: -1, maximum: 1E+07, step: 1, order: 100, unknown2: 1)]
			[DisplayName("Need material items ID01")]
			[Description("It is the material item ID required to strengthen armor .")]
			[DefaultValue((Int32)(-1))]
			public Int32 MaterialId01 {
				get { return materialId01; }
				set {
					if ((double)value < -1 || (double)value > 1E+07)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+07 for MaterialId01.");
					SetProperty(ref materialId01, ref value, MaterialId01Property);
				}
			}

			/// <summary>Need material items ID02</summary>
			/// <remarks>
			/// Japanese short name: "必要素材アイテムID02", Google translated: "Need material items ID02".
			/// Japanese description: "武具強化に必要な素材アイテムIDです。", Google translated: "It is the material item ID required to strengthen armor .".
			/// </remarks>
			[ParameterTableRowAttribute("materialId02", index: 1, minimum: -1, maximum: 1E+07, step: 1, order: 300, unknown2: 1)]
			[DisplayName("Need material items ID02")]
			[Description("It is the material item ID required to strengthen armor .")]
			[DefaultValue((Int32)(-1))]
			public Int32 MaterialId02 {
				get { return materialId02; }
				set {
					if ((double)value < -1 || (double)value > 1E+07)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+07 for MaterialId02.");
					SetProperty(ref materialId02, ref value, MaterialId02Property);
				}
			}

			/// <summary>Need material items ID03</summary>
			/// <remarks>
			/// Japanese short name: "必要素材アイテムID03", Google translated: "Need material items ID03".
			/// Japanese description: "武具強化に必要な素材アイテムIDです。", Google translated: "It is the material item ID required to strengthen armor .".
			/// </remarks>
			[ParameterTableRowAttribute("materialId03", index: 2, minimum: -1, maximum: 1E+07, step: 1, order: 500, unknown2: 1)]
			[DisplayName("Need material items ID03")]
			[Description("It is the material item ID required to strengthen armor .")]
			[DefaultValue((Int32)(-1))]
			public Int32 MaterialId03 {
				get { return materialId03; }
				set {
					if ((double)value < -1 || (double)value > 1E+07)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+07 for MaterialId03.");
					SetProperty(ref materialId03, ref value, MaterialId03Property);
				}
			}

			/// <summary>Need material items ID04</summary>
			/// <remarks>
			/// Japanese short name: "必要素材アイテムID04", Google translated: "Need material items ID04".
			/// Japanese description: "武具強化に必要な素材アイテムIDです。", Google translated: "It is the material item ID required to strengthen armor .".
			/// </remarks>
			[ParameterTableRowAttribute("materialId04", index: 3, minimum: -1, maximum: 1E+07, step: 1, order: 700, unknown2: 1)]
			[DisplayName("Need material items ID04")]
			[Description("It is the material item ID required to strengthen armor .")]
			[DefaultValue((Int32)(-1))]
			public Int32 MaterialId04 {
				get { return materialId04; }
				set {
					if ((double)value < -1 || (double)value > 1E+07)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+07 for MaterialId04.");
					SetProperty(ref materialId04, ref value, MaterialId04Property);
				}
			}

			/// <summary>Need material items ID05</summary>
			/// <remarks>
			/// Japanese short name: "必要素材アイテムID05", Google translated: "Need material items ID05".
			/// Japanese description: "武具強化に必要な素材アイテムIDです。", Google translated: "It is the material item ID required to strengthen armor .".
			/// </remarks>
			[ParameterTableRowAttribute("materialId05", index: 4, minimum: -1, maximum: 1E+07, step: 1, order: 900, unknown2: 1)]
			[DisplayName("Need material items ID05")]
			[Description("It is the material item ID required to strengthen armor .")]
			[DefaultValue((Int32)(-1))]
			public Int32 MaterialId05 {
				get { return materialId05; }
				set {
					if ((double)value < -1 || (double)value > 1E+07)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+07 for MaterialId05.");
					SetProperty(ref materialId05, ref value, MaterialId05Property);
				}
			}

			/// <summary>Required number of 01</summary>
			/// <remarks>
			/// Japanese short name: "必要個数01", Google translated: "Required number of 01".
			/// Japanese description: "武具強化に必要な素材アイテムの個数です。", Google translated: "It is the number of material items necessary to strengthen armor .".
			/// </remarks>
			[ParameterTableRowAttribute("itemNum01", index: 5, minimum: -1, maximum: 99, step: 1, order: 200, unknown2: 1)]
			[DisplayName("Required number of 01")]
			[Description("It is the number of material items necessary to strengthen armor .")]
			[DefaultValue((SByte)(-1))]
			public SByte ItemNum01 {
				get { return itemNum01; }
				set {
					if ((double)value < -1 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 99 for ItemNum01.");
					SetProperty(ref itemNum01, ref value, ItemNum01Property);
				}
			}

			/// <summary>Required number of 02</summary>
			/// <remarks>
			/// Japanese short name: "必要個数02", Google translated: "Required number of 02".
			/// Japanese description: "武具強化に必要な素材アイテムの個数です。", Google translated: "It is the number of material items necessary to strengthen armor .".
			/// </remarks>
			[ParameterTableRowAttribute("itemNum02", index: 6, minimum: -1, maximum: 99, step: 1, order: 400, unknown2: 1)]
			[DisplayName("Required number of 02")]
			[Description("It is the number of material items necessary to strengthen armor .")]
			[DefaultValue((SByte)(-1))]
			public SByte ItemNum02 {
				get { return itemNum02; }
				set {
					if ((double)value < -1 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 99 for ItemNum02.");
					SetProperty(ref itemNum02, ref value, ItemNum02Property);
				}
			}

			/// <summary>Required number of 03</summary>
			/// <remarks>
			/// Japanese short name: "必要個数03", Google translated: "Required number of 03".
			/// Japanese description: "武具強化に必要な素材アイテムの個数です。", Google translated: "It is the number of material items necessary to strengthen armor .".
			/// </remarks>
			[ParameterTableRowAttribute("itemNum03", index: 7, minimum: -1, maximum: 99, step: 1, order: 600, unknown2: 1)]
			[DisplayName("Required number of 03")]
			[Description("It is the number of material items necessary to strengthen armor .")]
			[DefaultValue((SByte)(-1))]
			public SByte ItemNum03 {
				get { return itemNum03; }
				set {
					if ((double)value < -1 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 99 for ItemNum03.");
					SetProperty(ref itemNum03, ref value, ItemNum03Property);
				}
			}

			/// <summary>Required number of 04</summary>
			/// <remarks>
			/// Japanese short name: "必要個数04", Google translated: "Required number of 04".
			/// Japanese description: "武具強化に必要な素材アイテムの個数です。", Google translated: "It is the number of material items necessary to strengthen armor .".
			/// </remarks>
			[ParameterTableRowAttribute("itemNum04", index: 8, minimum: -1, maximum: 99, step: 1, order: 800, unknown2: 1)]
			[DisplayName("Required number of 04")]
			[Description("It is the number of material items necessary to strengthen armor .")]
			[DefaultValue((SByte)(-1))]
			public SByte ItemNum04 {
				get { return itemNum04; }
				set {
					if ((double)value < -1 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 99 for ItemNum04.");
					SetProperty(ref itemNum04, ref value, ItemNum04Property);
				}
			}

			/// <summary>Required number of 05</summary>
			/// <remarks>
			/// Japanese short name: "必要個数05", Google translated: "Required number of 05".
			/// Japanese description: "武具強化に必要な素材アイテムの個数です。", Google translated: "It is the number of material items necessary to strengthen armor .".
			/// </remarks>
			[ParameterTableRowAttribute("itemNum05", index: 9, minimum: -1, maximum: 99, step: 1, order: 1000, unknown2: 1)]
			[DisplayName("Required number of 05")]
			[Description("It is the number of material items necessary to strengthen armor .")]
			[DefaultValue((SByte)(-1))]
			public SByte ItemNum05 {
				get { return itemNum05; }
				set {
					if ((double)value < -1 || (double)value > 99)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 99 for ItemNum05.");
					SetProperty(ref itemNum05, ref value, ItemNum05Property);
				}
			}

			/// <summary>Disable 01 the number display</summary>
			/// <remarks>
			/// Japanese short name: "個数表示を無効化01", Google translated: "Disable 01 the number display".
			/// Japanese description: "個数表示を無効化するか(強化ショップ用)", Google translated: "Or disable the number display ( enhanced for shops )".
			/// </remarks>
			[ParameterTableRowAttribute("isDisableDispNum01:1", index: 10, minimum: 0, maximum: 1, step: 1, order: 250, unknown2: 1)]
			[DisplayName("Disable 01 the number display")]
			[Description("Or disable the number display ( enhanced for shops )")]
			[DefaultValue(false)]
			public Boolean IsDisableDispNum01 {
				get { return GetBitProperty(0, 1, IsDisableDispNum01Property) != 0; }
				set { SetBitProperty(0, 1, value ? 1 : 0, IsDisableDispNum01Property); }
			}

			/// <summary>Disable 02 the number display</summary>
			/// <remarks>
			/// Japanese short name: "個数表示を無効化02", Google translated: "Disable 02 the number display".
			/// Japanese description: "個数表示を無効化するか", Google translated: "You can disable the display number".
			/// </remarks>
			[ParameterTableRowAttribute("isDisableDispNum02:1", index: 11, minimum: 0, maximum: 1, step: 1, order: 450, unknown2: 1)]
			[DisplayName("Disable 02 the number display")]
			[Description("You can disable the display number")]
			[DefaultValue(false)]
			public Boolean IsDisableDispNum02 {
				get { return GetBitProperty(1, 1, IsDisableDispNum02Property) != 0; }
				set { SetBitProperty(1, 1, value ? 1 : 0, IsDisableDispNum02Property); }
			}

			/// <summary>Disable 03 the number display</summary>
			/// <remarks>
			/// Japanese short name: "個数表示を無効化03", Google translated: "Disable 03 the number display".
			/// Japanese description: "個数表示を無効化するか", Google translated: "You can disable the display number".
			/// </remarks>
			[ParameterTableRowAttribute("isDisableDispNum03:1", index: 12, minimum: 0, maximum: 1, step: 1, order: 650, unknown2: 1)]
			[DisplayName("Disable 03 the number display")]
			[Description("You can disable the display number")]
			[DefaultValue(false)]
			public Boolean IsDisableDispNum03 {
				get { return GetBitProperty(2, 1, IsDisableDispNum03Property) != 0; }
				set { SetBitProperty(2, 1, value ? 1 : 0, IsDisableDispNum03Property); }
			}

			/// <summary>Disable 04 the number display</summary>
			/// <remarks>
			/// Japanese short name: "個数表示を無効化04", Google translated: "Disable 04 the number display".
			/// Japanese description: "個数表示を無効化するか", Google translated: "You can disable the display number".
			/// </remarks>
			[ParameterTableRowAttribute("isDisableDispNum04:1", index: 13, minimum: 0, maximum: 1, step: 1, order: 850, unknown2: 1)]
			[DisplayName("Disable 04 the number display")]
			[Description("You can disable the display number")]
			[DefaultValue(false)]
			public Boolean IsDisableDispNum04 {
				get { return GetBitProperty(3, 1, IsDisableDispNum04Property) != 0; }
				set { SetBitProperty(3, 1, value ? 1 : 0, IsDisableDispNum04Property); }
			}

			/// <summary>Disable 05 the number display</summary>
			/// <remarks>
			/// Japanese short name: "個数表示を無効化05", Google translated: "Disable 05 the number display".
			/// Japanese description: "個数表示を無効化するか", Google translated: "You can disable the display number".
			/// </remarks>
			[ParameterTableRowAttribute("isDisableDispNum05:1", index: 14, minimum: 0, maximum: 1, step: 1, order: 1050, unknown2: 1)]
			[DisplayName("Disable 05 the number display")]
			[Description("You can disable the display number")]
			[DefaultValue(false)]
			public Boolean IsDisableDispNum05 {
				get { return GetBitProperty(4, 1, IsDisableDispNum05Property) != 0; }
				set { SetBitProperty(4, 1, value ? 1 : 0, IsDisableDispNum05Property); }
			}

			/// <summary>Padding</summary>
			/// <remarks>
			/// Japanese short name: "パディング", Google translated: "Padding".
			/// Japanese description: "パディングです。", Google translated: "The padding .".
			/// </remarks>
			[ParameterTableRowAttribute("pad[6]", index: 15, minimum: 0, maximum: 0, step: 0, order: 99999999, unknown2: 0)]
			[DisplayName("Padding")]
			[Description("The padding .")]
			[Browsable(false)]
			public Byte[] Pad {
				get { return pad; }
				set { SetProperty(ref pad, ref value, PadProperty); }
			}

			internal MaterialSet(ParameterTable table, int index, AssetLoader loader, int next)
				: base(table, index, loader) {
				BinaryReader reader = loader.Reader;

				MaterialId01 = reader.ReadInt32();
				MaterialId02 = reader.ReadInt32();
				MaterialId03 = reader.ReadInt32();
				MaterialId04 = reader.ReadInt32();
				MaterialId05 = reader.ReadInt32();
				ItemNum01 = reader.ReadSByte();
				ItemNum02 = reader.ReadSByte();
				ItemNum03 = reader.ReadSByte();
				ItemNum04 = reader.ReadSByte();
				ItemNum05 = reader.ReadSByte();
				BitFields = reader.ReadBytes(1);
				Pad = reader.ReadBytes(6);
			}

			internal MaterialSet(ParameterTable table, int index)
				: base(table, index) {
				BitFields = new byte[1];
				MaterialId01 = (Int32)(-1);
				MaterialId02 = (Int32)(-1);
				MaterialId03 = (Int32)(-1);
				MaterialId04 = (Int32)(-1);
				MaterialId05 = (Int32)(-1);
				ItemNum01 = (SByte)(-1);
				ItemNum02 = (SByte)(-1);
				ItemNum03 = (SByte)(-1);
				ItemNum04 = (SByte)(-1);
				ItemNum05 = (SByte)(-1);
				IsDisableDispNum01 = false;
				IsDisableDispNum02 = false;
				IsDisableDispNum03 = false;
				IsDisableDispNum04 = false;
				IsDisableDispNum05 = false;
				Pad = new Byte[6];
			}

			public override void Write(BinaryWriter writer) {
				writer.Write(MaterialId01);
				writer.Write(MaterialId02);
				writer.Write(MaterialId03);
				writer.Write(MaterialId04);
				writer.Write(MaterialId05);
				writer.Write(ItemNum01);
				writer.Write(ItemNum02);
				writer.Write(ItemNum03);
				writer.Write(ItemNum04);
				writer.Write(ItemNum05);
				writer.Write(BitFields);
				writer.Write(Pad);
			}
		}
	}
}
