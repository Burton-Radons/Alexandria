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
		/// Defined as "SHOP_LINEUP_PARAM" in Dark Souls in the file "ShopLineupParam.paramdef" (id 13h).
		/// </remarks>
		public class ShopLineup : ParameterTableRow {
			public const string TableName = "SHOP_LINEUP_PARAM";

			Int32 equipId, _value, mtrlId, eventFlag, qwcId;
			Int16 sellQuantity;
			StoreInventoryType shopType;
			StoreInventoryEquipmentType equipType;
			Byte[] pad_0;

			public static readonly PropertyInfo
				EquipIdProperty = GetProperty<ShopLineup>("EquipId"),
				ValueProperty = GetProperty<ShopLineup>("Value"),
				MtrlIdProperty = GetProperty<ShopLineup>("MtrlId"),
				EventFlagProperty = GetProperty<ShopLineup>("EventFlag"),
				QwcIdProperty = GetProperty<ShopLineup>("QwcId"),
				SellQuantityProperty = GetProperty<ShopLineup>("SellQuantity"),
				ShopTypeProperty = GetProperty<ShopLineup>("ShopType"),
				EquipTypeProperty = GetProperty<ShopLineup>("EquipType"),
				Pad_0Property = GetProperty<ShopLineup>("Pad_0");

			/// <summary>Equipment ID of the commodity sold</summary>
			/// <remarks>
			/// Japanese short name: "販売品の装備ID", Google translated: "Equipment ID of the commodity sold".
			/// Japanese description: "販売している装備品のID", Google translated: "ID of the equipment it sells".
			/// </remarks>
			[ParameterTableRowAttribute("equipId", index: 0, minimum: 0, maximum: 1E+08, step: 1, order: 400, unknown2: 1)]
			[DisplayName("Equipment ID of the commodity sold")]
			[Description("ID of the equipment it sells")]
			[DefaultValue((Int32)0)]
			public Int32 EquipId {
				get { return equipId; }
				set {
					if ((double)value < 0 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1E+08 for " + EquipIdProperty.Name + ".");
					SetProperty(ref equipId, ref value, EquipIdProperty);
				}
			}

			/// <summary>Selling price</summary>
			/// <remarks>
			/// Japanese short name: "販売価格", Google translated: "Selling price".
			/// Japanese description: "販売価格", Google translated: "Selling price".
			/// </remarks>
			[ParameterTableRowAttribute("value", index: 1, minimum: -1, maximum: 1E+08, step: 1, order: 500, unknown2: 1)]
			[DisplayName("Selling price")]
			[Description("Selling price")]
			[DefaultValue((Int32)(-1))]
			public Int32 Value {
				get { return _value; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for " + ValueProperty.Name + ".");
					SetProperty(ref _value, ref value, ValueProperty);
				}
			}

			/// <summary>Material ID required to purchase</summary>
			/// <remarks>
			/// Japanese short name: "購入に必要な素材ID", Google translated: "Material ID required to purchase".
			/// Japanese description: "購入に必要な素材ID", Google translated: "Material ID required to purchase".
			/// </remarks>
			[ParameterTableRowAttribute("mtrlId", index: 2, minimum: -1, maximum: 1E+08, step: 1, order: 600, unknown2: 1)]
			[DisplayName("Material ID required to purchase")]
			[Description("Material ID required to purchase")]
			[DefaultValue((Int32)(-1))]
			public Int32 MtrlId {
				get { return mtrlId; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for " + MtrlIdProperty.Name + ".");
					SetProperty(ref mtrlId, ref value, MtrlIdProperty);
				}
			}

			/// <summary>Event flag</summary>
			/// <remarks>
			/// Japanese short name: "イベントフラグ", Google translated: "Event flag".
			/// Japanese description: "個数を保持してあるイベントフラグ値", Google translated: "Event flag value are holding the piece".
			/// </remarks>
			[ParameterTableRowAttribute("eventFlag", index: 3, minimum: -1, maximum: 1E+08, step: 1, order: 700, unknown2: 1)]
			[DisplayName("Event flag")]
			[Description("Event flag value are holding the piece")]
			[DefaultValue((Int32)(-1))]
			public Int32 EventFlag {
				get { return eventFlag; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for " + EventFlagProperty.Name + ".");
					SetProperty(ref eventFlag, ref value, EventFlagProperty);
				}
			}

			/// <summary>QWC parameter ID</summary>
			/// <remarks>
			/// Japanese short name: "QWCパラメタID", Google translated: "QWC parameter ID".
			/// Japanese description: "ＱＷＣパラメータのID", Google translated: "ID parameter of QWC".
			/// </remarks>
			[ParameterTableRowAttribute("qwcId", index: 4, minimum: -1, maximum: 1E+08, step: 1, order: 900, unknown2: 1)]
			[DisplayName("QWC parameter ID")]
			[Description("ID parameter of QWC")]
			[DefaultValue((Int32)(-1))]
			public Int32 QwcId {
				get { return qwcId; }
				set {
					if ((double)value < -1 || (double)value > 1E+08)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for " + QwcIdProperty.Name + ".");
					SetProperty(ref qwcId, ref value, QwcIdProperty);
				}
			}

			/// <summary>Unit sales</summary>
			/// <remarks>
			/// Japanese short name: "販売個数", Google translated: "Unit sales".
			/// Japanese description: "販売個数", Google translated: "Unit sales".
			/// </remarks>
			[ParameterTableRowAttribute("sellQuantity", index: 5, minimum: -1, maximum: 9999, step: 1, order: 800, unknown2: 1)]
			[DisplayName("Unit sales")]
			[Description("Unit sales")]
			[DefaultValue((Int16)(-1))]
			public Int16 SellQuantity {
				get { return sellQuantity; }
				set {
					if ((double)value < -1 || (double)value > 9999)
						throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 9999 for " + SellQuantityProperty.Name + ".");
					SetProperty(ref sellQuantity, ref value, SellQuantityProperty);
				}
			}

			/// <summary>Shop type</summary>
			/// <remarks>
			/// Japanese short name: "ショップタイプ", Google translated: "Shop type".
			/// Japanese description: "ショップの種類", Google translated: "Type of shop".
			/// </remarks>
			[ParameterTableRowAttribute("shopType", index: 6, minimum: 0, maximum: 4, step: 1, order: 100, unknown2: 1)]
			[DisplayName("Shop type")]
			[Description("Type of shop")]
			[DefaultValue((StoreInventoryType)0)]
			public StoreInventoryType ShopType {
				get { return shopType; }
				set { SetProperty(ref shopType, ref value, ShopTypeProperty); }
			}

			/// <summary>Equipment type of commodity sold</summary>
			/// <remarks>
			/// Japanese short name: "販売品の装備タイプ", Google translated: "Equipment type of commodity sold".
			/// Japanese description: "販売している装備品の種類", Google translated: "The type of equipment it sells".
			/// </remarks>
			[ParameterTableRowAttribute("equipType", index: 7, minimum: 0, maximum: 6, step: 1, order: 200, unknown2: 1)]
			[DisplayName("Equipment type of commodity sold")]
			[Description("The type of equipment it sells")]
			[DefaultValue((StoreInventoryEquipmentType)0)]
			public StoreInventoryEquipmentType EquipType {
				get { return equipType; }
				set { SetProperty(ref equipType, ref value, EquipTypeProperty); }
			}

			/// <summary>pad</summary>
			/// <remarks>
			/// Japanese short name: "pad", Google translated: "pad".
			/// Japanese description: "", Google translated: "".
			/// </remarks>
			[ParameterTableRowAttribute("pad_0[8]", index: 8, minimum: 0, maximum: 0, step: 0, order: 99999999, unknown2: 0)]
			[DisplayName("pad")]
			[Description("")]
			[Browsable(false)]
			public Byte[] Pad_0 {
				get { return pad_0; }
				set { SetProperty(ref pad_0, ref value, Pad_0Property); }
			}

			internal ShopLineup(ParameterTable table, int index, AssetLoader loader, int next)
				: base(table, index, loader) {
				BinaryReader reader = loader.Reader;

				EquipId = reader.ReadInt32();
				Value = reader.ReadInt32();
				MtrlId = reader.ReadInt32();
				EventFlag = reader.ReadInt32();
				QwcId = reader.ReadInt32();
				SellQuantity = reader.ReadInt16();
				ShopType = (StoreInventoryType)reader.ReadByte();
				EquipType = (StoreInventoryEquipmentType)reader.ReadByte();
				Pad_0 = reader.ReadBytes(8);
			}

			internal ShopLineup(ParameterTable table, int index)
				: base(table, index) {
				EquipId = (Int32)0;
				Value = (Int32)(-1);
				MtrlId = (Int32)(-1);
				EventFlag = (Int32)(-1);
				QwcId = (Int32)(-1);
				SellQuantity = (Int16)(-1);
				ShopType = (StoreInventoryType)0;
				EquipType = (StoreInventoryEquipmentType)0;
				Pad_0 = new Byte[8];
			}

			public override void Write(BinaryWriter writer) {
				writer.Write(EquipId);
				writer.Write(Value);
				writer.Write(MtrlId);
				writer.Write(EventFlag);
				writer.Write(QwcId);
				writer.Write(SellQuantity);
				writer.Write((Byte)ShopType);
				writer.Write((Byte)EquipType);
				writer.Write(Pad_0);
			}
		}
	}
}
