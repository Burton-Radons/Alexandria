using Glare.Assets;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Alexandria.Engines.DarkSouls {
	/// <summary>Contains game information like equipment parameters.</summary>
	public class ParameterTable : Table {
		public ParameterTable(AssetManager manager, AssetLoader loader)
			: base(manager, loader) {
			var reader = loader.Reader;
			int stringsOffset = reader.ReadInt32();
			int firstRowDataOffset = reader.ReadUInt16();
			Unknowns.ReadInt16s(reader, 2);
			int rowCount = reader.ReadUInt16();

			Name = reader.ReadStringz(0x20, Encoding.ASCII);
			Unknowns.ReadInt32s(reader, 1);

			// Read in the row headers.
			RowInfo[] rows = new RowInfo[rowCount];
			for (int index = 0; index < rowCount; index++)
				rows[index].Read(reader);

			// Read in the rows.
			for (int index = 0; index < rowCount; index++) {
				loader.Position = rows[index].DataOffset;
				int next = index < rows.Length - 1 ? rows[index + 1].DataOffset : stringsOffset;
				ParameterTableRow item = ParameterTableRow.ReadRow(this, index, loader, next);
				item.Id = rows[index].Id;

				if (loader.Position != next)
					loader.AddError(rows[index].DataOffset, "Row {0} of the table did not read the correct amount; position should be {1}, but is {2}.", index, next, loader.Position);
			}

			// Read in the row names.
			for (int index = 0; index < rowCount; index++) {
				loader.Position = rows[index].NameOffset;
				((ParameterTableRow)Children[index]).Name = reader.ReadStringz(EncodingShiftJis).Trim();//((index < rows.Length - 1 ? rows[index + 1].NameOffset : loader.ShortLength) - rows[index].NameOffset - 1, EncodingShiftJis));
			}
		}
		
		struct RowInfo {
			public int Id;
			public int DataOffset;
			public int NameOffset;

			public void Read(BinaryReader reader) {
				Id = reader.ReadInt32();
				DataOffset = reader.ReadInt32();
				NameOffset = reader.ReadInt32();
			}
		}
	}

	public class ParameterTableFormat : AssetFormat {
		public ParameterTableFormat(Engine engine)
			: base(engine, typeof(TableArchive), canLoad: true, extension: ".param") {
		}

		public override LoadMatchStrength LoadMatch(AssetLoader loader) {
			var reader = loader.Reader;

			if (loader.Length < 0x3C)
				return LoadMatchStrength.None;

			int stringsOffset = reader.ReadInt32(), firstRowDataOffset = reader.ReadUInt16(), unknown1 = reader.ReadUInt16(), unknown2 = reader.ReadUInt16(), rowCount = reader.ReadUInt16();

			if (firstRowDataOffset < 0x30 + rowCount * 12 || rowCount < 1 || stringsOffset <= firstRowDataOffset || stringsOffset > loader.Length)
				return LoadMatchStrength.None;

			// Name is "[ASCII...]\x00[\x20...]"
			{
				const int nameEnd = 0x2C;
				byte nameChar;
				while (true)
					if (loader.Position >= nameEnd)
						return LoadMatchStrength.None;
					else if ((nameChar = reader.ReadByte()) == 0)
						break;
					else if (nameChar > 127 || nameChar <= 32)
						return LoadMatchStrength.None;
				while (loader.Position < nameEnd)
					if (reader.ReadByte() != 0x20)
						return LoadMatchStrength.None;
			}

			reader.ReadInt32(); // Weird thing, 0x200, etc.

			return LoadMatchStrength.Medium;
		}

		public override Asset Load(AssetLoader loader) {
			return new ParameterTable(Manager, loader);
		}
	}

	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class ParameterTableRowAttribute : TableRowAttribute {
		public string OriginalName { get; private set; }
		public int Index { get; private set; }
		public int Unknown2 { get; private set; }

		public ParameterTableRowAttribute(string originalName, int index, double minimum, double maximum, double step, int order, int unknown2 = 1)
			: base(originalName, minimum, maximum, order) {
			Index = index;
			Unknown2 = unknown2;
			OriginalName = originalName;
		}
	}

	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
	public class ParameterTableRowOrderAttribute : PropertyTableRowOrderAttribute {
		public int Index { get; private set; }

		public ParameterTableRowOrderAttribute(string id, int index, int sortOrder)
			: base(id, sortOrder) {
			Index = index;
		}
	}

	public abstract class ParameterTableRow : TableRow {
		protected byte[] BitFields;

		public int Id { get; internal set; }

		[Browsable(false)]
		public new ParameterTable Parent { get { return (ParameterTable)base.Parent; } }

		public string TranslatedName {
			get { return Engine.GetTranslation(Name); }
		}

		public ParameterTableRow(ParameterTable table, int index, AssetLoader loader)
			: this(table, index) {
		}

		public ParameterTableRow(ParameterTable table, int index)
			: base(table, index) {
		}

		int GetBits(int shift) {
			int index = shift / 8;
			return BitFields[index] | (BitFields.TryGet(index + 1) << 8) | (BitFields.TryGet(index + 2) << 16) | (BitFields.TryGet(index + 3) << 24);
		}

		protected int GetBitProperty(int shift, int count, PropertyInfo property) {
			if (count > 24)
				throw new ArgumentOutOfRangeException("count");
			int bits = GetBits(shift);
			int mask = (1 << count) - 1;

			return (bits >> (shift & 7)) & mask;
		}

		protected bool GetBoolProperty(int shift, int count, PropertyInfo property) {
			return GetBitProperty(shift, count, property) != 0;
		}

		/// <summary>Get a localised string using the <see cref="Id"/> value as the index.</summary>
		/// <param name="archive"></param>
		/// <param name="language"></param>
		/// <returns></returns>
		protected string GetLocalisedString(Engine.ItemArchiveId archive, Language language = Language.English) { return Parent.GetLocalisedString(archive, Id, language); }

		public static ParameterTableRow ReadRow(ParameterTable table, int index, AssetLoader loader, int next) {
			switch (table.Name) {
				case TableRows.Protector.TableName: return new TableRows.Protector(table, index, loader, next);
				case TableRows.Good.TableName: return new TableRows.Good(table, index, loader, next);
				case TableRows.Weapon.TableName: return new TableRows.Weapon(table, index, loader, next);
				case TableRows.Accessory.TableName: return new TableRows.Accessory(table, index, loader, next);
				case TableRows.FaceGeneration.TableName: return new TableRows.FaceGeneration(table, index, loader, next);
				case TableRows.Npc.TableName: return new TableRows.Npc(table, index, loader, next);
				case TableRows.Throw.TableName: return new TableRows.Throw(table, index, loader, next);
				case TableRows.AiStandardInfo.TableName: return new TableRows.AiStandardInfo(table, index, loader, next);
				case TableRows.Behavior.TableName: return new TableRows.Behavior(table, index, loader, next);
				case TableRows.Bullet.TableName: return new TableRows.Bullet(table, index, loader, next);
				case TableRows.CalculationCorrection.TableName: return new TableRows.CalculationCorrection(table, index, loader, next);
				case TableRows.CharacterInitialiser.TableName: return new TableRows.CharacterInitialiser(table, index, loader, next);
				case TableRows.DepthOfField.TableName: return new TableRows.DepthOfField(table, index, loader, next);
				case TableRows.EnemyStandardInfo.TableName: return new TableRows.EnemyStandardInfo(table, index, loader, next);
				case TableRows.EnvironmentLight.TableName: return new TableRows.EnvironmentLight(table, index, loader, next);
				case TableRows.MaterialSet.TableName: return new TableRows.MaterialSet(table, index, loader, next);
				case TableRows.Fog.TableName: return new TableRows.Fog(table, index, loader, next);
				case TableRows.GameArea.TableName: return new TableRows.GameArea(table, index, loader, next);
				case TableRows.HitMaterial.TableName: return new TableRows.HitMaterial(table, index, loader, next);
				case TableRows.ItemLot.TableName: return new TableRows.ItemLot(table, index, loader, next);
				case TableRows.KnockBack.TableName: return new TableRows.KnockBack(table, index, loader, next);
				case TableRows.LensFlare.TableName: return new TableRows.LensFlare(table, index, loader, next);
				case TableRows.LensFlareEx.TableName: return new TableRows.LensFlareEx(table, index, loader, next);
				case TableRows.Light.TableName: return new TableRows.Light(table, index, loader, next);
				case TableRows.LightScattering.TableName: return new TableRows.LightScattering(table, index, loader, next);
				case TableRows.LockCamera.TableName: return new TableRows.LockCamera(table, index, loader, next);
				case TableRows.LevelOfDetail.TableName: return new TableRows.LevelOfDetail(table, index, loader, next);
				case TableRows.QwcJudge.TableName: return new TableRows.QwcJudge(table, index, loader, next);
				case TableRows.QwcChange.TableName: return new TableRows.QwcChange(table, index, loader, next);
				case TableRows.PointLight.TableName: return new TableRows.PointLight(table, index, loader, next);
				case TableRows.ObjectInfo.TableName: return new TableRows.ObjectInfo(table, index, loader, next);
				case TableRows.ObjectAction.TableName: return new TableRows.ObjectAction(table, index, loader, next);
				case TableRows.NpcThink.TableName: return new TableRows.NpcThink(table, index, loader, next);
				case TableRows.Move.TableName: return new TableRows.Move(table, index, loader, next);
				case TableRows.MenuColor.TableName: return new TableRows.MenuColor(table, index, loader, next);
				case TableRows.Magic.TableName: return new TableRows.Magic(table, index, loader, next);
				case TableRows.Skeleton.TableName: return new TableRows.Skeleton(table, index, loader, next);
				case TableRows.ShopLineup.TableName: return new TableRows.ShopLineup(table, index, loader, next);
				case TableRows.Shadow.TableName: return new TableRows.Shadow(table, index, loader, next);
				case TableRows.WeaponUpgrade.TableName: return new TableRows.WeaponUpgrade(table, index, loader, next);
				case TableRows.ProtectorReinforcement.TableName: return new TableRows.ProtectorReinforcement(table, index, loader, next);
				case TableRows.ToneMapping.TableName: return new TableRows.ToneMapping(table, index, loader, next);
				case TableRows.ToneCorrection.TableName: return new TableRows.ToneCorrection(table, index, loader, next);
				case TableRows.Talk.TableName: return new TableRows.Talk(table, index, loader, next);
				case TableRows.SpecialEffectVfx.TableName: return new TableRows.SpecialEffectVfx(table, index, loader, next);
				case TableRows.SpecialEffect.TableName: return new TableRows.SpecialEffect(table, index, loader, next);
				case TableRows.Ragdoll.TableName: return new TableRows.Ragdoll(table, index, loader, next);
				case TableRows.Attack.TableName: return new TableRows.Attack(table, index, loader, next);

				default:
					throw new NotImplementedException("Table row type " + table.Name + " is not implemented.");
			}
		}

		protected void SetBitProperty(int shift, int count, bool value, PropertyInfo property) { SetBitProperty(shift, count, value ? ~0 : 0, property); }

		protected void SetBitProperty(int shift, int count, int value, PropertyInfo property) {
			if (count > 24)
				throw new ArgumentOutOfRangeException("count");
			int index = shift / 8;
			int bits = GetBits(shift);
			int mask = (1 << count) - 1;
			int lowShift = shift & 7;

			bits &= ~(mask << lowShift);
			bits |= (value & mask) << lowShift;
			BitFields[index] = (byte)bits;
			BitFields.TrySet(index + 1, (byte)(bits >> 8));
			BitFields.TrySet(index + 2, (byte)(bits >> 16));
			BitFields.TrySet(index + 3, (byte)(bits >> 24));
		}

		public abstract void Write(BinaryWriter writer);
	}

	public enum ParameterTableIndex {
		/// <summary></summary>
		/// <remarks>"AtkParam_Npc.param" (id 0x0E), using <see cref="TableRows.Attack"/></remarks>
		NpcAttacks = 0x0E,

		/// <summary></summary>
		/// <remarks>"AtkParam_Pc.param" (id 0x0D), using <see cref="TableRows.Attack"/></remarks>
		PcAttacks = 0x0D,

		/// <summary></summary>
		/// <remarks>"BehaviorParam.param" (id 0x0A), using <see cref="TableRows.Behavior"/></remarks>
		Behaviors = 0x0A,

		/// <summary></summary>
		/// <remarks>"BehaviorParam_PC.param" (id 0x0B), using <see cref="TableRows.Behavior"/></remarks>
		PcBehaviors = 0x0B,

		/// <summary></summary>
		/// <remarks>"Bullet.param" (id 0x12), using <see cref="TableRows.Bullet"/></remarks>
		Bullets = 0x12,

		/// <summary></summary>
		/// <remarks>"CalCorrectGraph.param" (id 0x21), using <see cref="TableRows.CalculationCorrection"/></remarks>
		CalculationCorrections = 0x21,

		/// <summary></summary>
		/// <remarks>"CharaInitParam.param" (id 0x19), using <see cref="TableRows.CharacterInitialiser"/></remarks>
		CharacterInitialisers = 0x19,

		/// <summary></summary>
		/// <remarks>"default_AIStandardInfoBank.param" (id 0x08), using <see cref="TableRows.AiStandardInfo"/></remarks>
		DefaultAiStandardInfos = 0x08,

		/// <summary></summary>
		/// <remarks>"default_EnemyBehaviorBank.param" (id 0x07), using <see cref="TableRows.EnemyBehavior"/></remarks>
		DefaultEnemyBehaviors = 0x07,

		/// <summary></summary>
		/// <remarks>"EquipMtrlSetParam.param" (id 0x06), using <see cref="TableRows.MaterialSet"/></remarks>
		MaterialSets = 0x06,

		/// <summary></summary>
		/// <remarks>"EquipParamAccessory.param" (id 0x02), using <see cref="TableRows.Accessory"/></remarks>
		Accessories = 0x02,

		/// <summary></summary>
		/// <remarks>"EquipParamGoods.param" (id 0x03), using <see cref="TableRows.Good"/></remarks>
		Goods = 0x03,

		/// <summary></summary>
		/// <remarks>"EquipParamProtector.param" (id 0x01), using <see cref="TableRows.Protector"/></remarks>
		Protectors = 0x01,

		/// <summary></summary>
		/// <remarks>"EquipParamWeapon.param" (id 0x00), using <see cref="TableRows.Weapon"/></remarks>
		Weapons = 0x00,

		/// <summary></summary>
		/// <remarks>"FaceGenParam.param" (id 0x1A), using <see cref="TableRows.FaceGeneration"/></remarks>
		FaceGeneration = 0x1A,

		/// <summary></summary>
		/// <remarks>"GameAreaParam.param" (id 0x1F), using <see cref="TableRows.GameArea"/></remarks>
		GameAreas = 0x1F,

		/// <summary></summary>
		/// <remarks>"HitMtrlParam.param" (id 0x26), using <see cref="TableRows.HitMaterial"/></remarks>
		HitMaterials = 0x26,

		/// <summary></summary>
		/// <remarks>"ItemLotParam.param" (id 0x), using <see cref="TableRows.ItemLot"/></remarks>
		ItemLots = 0x17,

		/// <summary></summary>
		/// <remarks>"KnockBackParam.param" (id 0x27), using <see cref="TableRows.KnockBack"/></remarks>
		KnockBacks = 0x27,

		/// <summary></summary>
		/// <remarks>"LockCamParam.param" (id 0x24), using <see cref="TableRows.LockCamera"/></remarks>
		LockCameras = 0x24,

		/// <summary></summary>
		/// <remarks>"Magic.param" (id 0x0F), using <see cref="TableRows.Magic"/></remarks>
		Magics = 0x0F,

		/// <summary></summary>
		/// <remarks>"MenuColorTable.param" (id 0x16), using <see cref="TableRows.MenuColor"/></remarks>
		MenuColors = 0x16,

		/// <summary></summary>
		/// <remarks>"MoveParam.param" (id 0x18), using <see cref="TableRows.Move"/></remarks>
		Move = 0x18,

		/// <summary></summary>
		/// <remarks>"NpcParam.param" (id 0x0C), using <see cref="TableRows.Npc"/></remarks>
		Npcs = 0x0C,

		/// <summary></summary>
		/// <remarks>"NpcThinkParam.param" (id 0x10), using <see cref="TableRows.NpcThink"/></remarks>
		NpcThinks = 0x10,

		/// <summary></summary>
		/// <remarks>"ObjActParam.param" (id 0x25), using <see cref="TableRows.ObjectAction"/></remarks>
		ObjectActions = 0x25,

		/// <summary></summary>
		/// <remarks>"ObjectParam.param" (id 0x11), using <see cref="TableRows.ObjectInfo"/></remarks>
		Objects = 0x11,

		/// <summary></summary>
		/// <remarks>"QwcChange.param" (id 0x1D), using <see cref="TableRows.QwcChange"/></remarks>
		QwcChanges = 0x1D,

		/// <summary></summary>
		/// <remarks>"QwcJudge.param" (id 0x1E), using <see cref="TableRows.QwcJudge"/></remarks>
		QwcJudges = 0x1E,

		/// <summary></summary>
		/// <remarks>"RagdollParam.param" (id 0x1B), using <see cref="TableRows.Ragdoll"/></remarks>
		Ragdolls = 0x1B,

		/// <summary></summary>
		/// <remarks>"ReinforceParamProtector.param" (id 0x05), using <see cref="TableRows.ProtectorReinforcement"/></remarks>
		ProtectorReinforcements = 0x05,

		/// <summary></summary>
		/// <remarks>"ReinforceParamWeapon.param" (id 0x04), using <see cref="TableRows.WeaponReinforcement"/></remarks>
		WeaponReinforcements = 0x04,

		/// <summary></summary>
		/// <remarks>"ShopLineupParam.param" (id 0x1C), using <see cref="TableRows.ShopLineup"/></remarks>
		ShopLineup = 0x1C,

		/// <summary></summary>
		/// <remarks>"SkeletonParam.param" (id 0x20), using <see cref="TableRows.Skeleton"/></remarks>
		Skeleton = 0x20,

		/// <summary></summary>
		/// <remarks>"SpEffectParam.param" (id 0x13), using <see cref="TableRows.SpecialEffect"/></remarks>
		SpecialEffects = 0x13,

		/// <summary></summary>
		/// <remarks>"SpEffectVfxParam.param" (id 0x14), using <see cref="TableRows.SpecialEffectVfx"/></remarks>
		SpecialEffectVfxs = 0x14,

		/// <summary></summary>
		/// <remarks>"TalkParam.param" (id 0x15), using <see cref="TableRows.Talk"/></remarks>
		Talks = 0x15,

		/// <summary></summary>
		/// <remarks>"ThrowParam.param" (id 0x09), using <see cref="TableRows.Throw"/></remarks>
		Throws = 0x09,
	}

	public abstract class EquipmentTableRow : ParameterTableRow {
		int basicPrice = 0, sellValue = 0, sortId = 0, vagrantItemLotId = 0, vagrantBonusEneDropItemLotId = 0, vagrantItemEneDropItemLotId = 0;
		float weight = 1;

		public static readonly PropertyInfo
			BasicPriceProperty = GetProperty<EquipmentTableRow>("BasicPrice"),
			IsDepositProperty = GetProperty<EquipmentTableRow>("IsDeposit"),
			SellValueProperty = GetProperty<EquipmentTableRow>("SellValue"),
			WeightProperty = GetProperty<EquipmentTableRow>("Weight"),
			VagrantItemLotIdProperty = GetProperty<EquipmentTableRow>("VagrantItemLotId"),
			VagrantBonusEneDropItemLotIdProperty = GetProperty<EquipmentTableRow>("VagrantBonusEneDropItemLotId"),
			VagrantItemEneDropItemLotIdProperty = GetProperty<EquipmentTableRow>("VagrantItemEneDropItemLotId"),
			SortIdProperty = GetProperty<EquipmentTableRow>("SortId");

		/// <summary>Base price</summary>
		/// <remarks>
		/// Japanese short name: "基本価格", Google translated: "Base price".
		/// Japanese description: "基本価格", Google translated: "Base price".
		/// </remarks>
		[ParameterTableRowAttribute("basicPrice", index: -1, minimum: 0, maximum: 1E+08, step: 1, order: -1, unknown2: 1)]
		[DisplayName("Base price")]
		[Description("Base price")]
		[DefaultValue((Int32)0)]
		public Int32 BasicPrice {
			get { return basicPrice; }
			set {
				if ((double)value < 0 || (double)value > 1E+08)
					throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1E+08 for BasicPrice.");
				SetProperty(ref basicPrice, ref value, BasicPriceProperty);
			}
		}

		/// <summary>Either deposit</summary>
		/// <remarks>
		/// Japanese short name: "預けれるか", Google translated: "Either deposit".
		/// Japanese description: "倉庫に預けれるか", Google translated: "Is either deposited in the warehouse".
		/// </remarks>
		[ParameterTableRowAttribute("isDeposit:1", index: 73, minimum: 0, maximum: 1, step: 1, order: 4775, unknown2: 1)]
		[DisplayName("Either deposit")]
		[Description("Is either deposited in the warehouse")]
		[DefaultValue(true)]
		public Boolean IsDeposit {
			get { return GetBitProperty(0, 1, IsDepositProperty) != 0; }
			set { SetBitProperty(0, 1, value ? 1 : 0, IsDepositProperty); }
		}

		/// <summary>Selling price</summary>
		/// <remarks>
		/// Japanese short name: "販売価格", Google translated: "Selling price".
		/// Japanese description: "販売価格", Google translated: "Selling price".
		/// </remarks>
		[ParameterTableRowAttribute("sellValue", index: -1, minimum: -1, maximum: 1E+08, step: 1, order: -1, unknown2: 1)]
		[DisplayName("Selling price")]
		[Description("Selling price")]
		[DefaultValue((Int32)0)]
		public Int32 SellValue {
			get { return sellValue; }
			set {
				if ((double)value < -1 || (double)value > 1E+08)
					throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for SellValue.");
				SetProperty(ref sellValue, ref value, SellValueProperty);
			}
		}

		/// <summary>Sort ID</summary>
		/// <remarks>
		/// Japanese short name: "ソートID", Google translated: "Sort ID".
		/// Japanese description: "ソートID(-1:集めない)", Google translated: "Sort ID (-1: it is not collected )".
		/// </remarks>
		[ParameterTableRowAttribute("sortId", index: -1, minimum: -1, maximum: 999999, step: 1, order: -1, unknown2: 1)]
		[DisplayName("Sort ID")]
		[Description("Sort ID (-1: it is not collected )")]
		[DefaultValue((Int32)0)]
		public Int32 SortId {
			get { return sortId; }
			set {
				if ((double)value < -1 || (double)value > 999999)
					throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 999999 for SortId.");
				SetProperty(ref sortId, ref value, SortIdProperty);
			}
		}

		/// <summary>Vagrant when items lottery ID</summary>
		/// <remarks>
		/// Japanese short name: "ベイグラント時アイテム抽選ID", Google translated: "Vagrant when items lottery ID".
		/// Japanese description: "-1：ベイグラントなし 0：抽選なし 1～：抽選あり", Google translated: "Yes lottery : 1 to No lottery : 0 No Vagrant : -1".
		/// </remarks>
		[ParameterTableRowAttribute("vagrantItemLotId", index: -1, minimum: -1, maximum: 1E+08, step: 1, order: -1, unknown2: 0)]
		[DisplayName("Vagrant when items lottery ID")]
		[Description("Yes lottery : 1 to No lottery : 0 No Vagrant : -1")]
		[DefaultValue((Int32)0)]
		public Int32 VagrantItemLotId {
			get { return vagrantItemLotId; }
			set {
				if ((double)value < -1 || (double)value > 1E+08)
					throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for VagrantItemLotId.");
				SetProperty(ref vagrantItemLotId, ref value, VagrantItemLotIdProperty);
			}
		}

		/// <summary>Vagrant bonus enemy drop items lottery ID</summary>
		/// <remarks>
		/// Japanese short name: "ベイグラントボーナス敵ドロップアイテム抽選ID", Google translated: "Vagrant bonus enemy drop items lottery ID".
		/// Japanese description: "-1：ドロップなし 0：抽選なし 1～：抽選あり", Google translated: "Yes lottery : 1 to No lottery : 0 No Drop: -1".
		/// </remarks>
		[ParameterTableRowAttribute("vagrantBonusEneDropItemLotId", index: -1, minimum: -1, maximum: 1E+08, step: 1, order: -1, unknown2: 0)]
		[DisplayName("Vagrant bonus enemy drop items lottery ID")]
		[Description("Yes lottery : 1 to No lottery : 0 No Drop: -1")]
		[DefaultValue((Int32)0)]
		public Int32 VagrantBonusEneDropItemLotId {
			get { return vagrantBonusEneDropItemLotId; }
			set {
				if ((double)value < -1 || (double)value > 1E+08)
					throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for VagrantBonusEneDropItemLotId.");
				SetProperty(ref vagrantBonusEneDropItemLotId, ref value, VagrantBonusEneDropItemLotIdProperty);
			}
		}

		/// <summary>Vagrant enemy item drop item lottery ID</summary>
		/// <remarks>
		/// Japanese short name: "ベイグラントアイテム敵ドロップアイテム抽選ID", Google translated: "Vagrant enemy item drop item lottery ID".
		/// Japanese description: "-1：ドロップなし 0：抽選なし 1～：抽選あり", Google translated: "Yes lottery : 1 to No lottery : 0 No Drop: -1".
		/// </remarks>
		[ParameterTableRowAttribute("vagrantItemEneDropItemLotId", index: -1, minimum: -1, maximum: 1E+08, step: 1, order: -1, unknown2: 0)]
		[DisplayName("Vagrant enemy item drop item lottery ID")]
		[Description("Yes lottery : 1 to No lottery : 0 No Drop: -1")]
		[DefaultValue((Int32)0)]
		public Int32 VagrantItemEneDropItemLotId {
			get { return vagrantItemEneDropItemLotId; }
			set {
				if ((double)value < -1 || (double)value > 1E+08)
					throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range -1 to 1E+08 for VagrantItemEneDropItemLotId.");
				SetProperty(ref vagrantItemEneDropItemLotId, ref value, VagrantItemEneDropItemLotIdProperty);
			}
		}

		/// <summary>Weight [kg]</summary>
		/// <remarks>
		/// Japanese short name: "重量[kg]", Google translated: "Weight [kg]".
		/// Japanese description: "重量[kg].", Google translated: "Weight [kg].".
		/// </remarks>
		[ParameterTableRowAttribute("weight", index: -1, minimum: 0, maximum: 1000, step: 0.1, order: -1, unknown2: 1)]
		[DisplayName("Weight [kg]")]
		[Description("Weight [kg].")]
		[DefaultValue((Single)1)]
		public Single Weight {
			get { return weight; }
			set {
				if ((double)value < 0 || (double)value > 1000)
					throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1000 for Weight.");
				SetProperty(ref weight, ref value, WeightProperty);
			}
		}

		public EquipmentTableRow(ParameterTable table, int index, AssetLoader loader)
			: base(table, index, loader) {
		}

		public EquipmentTableRow(ParameterTable table, int index)
			: base(table, index) {
		}
	}

	/// <summary>Equipment that is worn - <see cref="Ring"/>s and through <see cref="ClothingTableRow"/>, <see cref="Weapon"/>s and <see cref="Armor"/>.</summary>
	public abstract class WornTableRow : EquipmentTableRow {
		EquipModelCategory equipModelCategory = (EquipModelCategory)0;
		EquipModelGender equipModelGender = (EquipModelGender)0;
		ushort equipModelId = 0;

		PropertyInfo
			EquipModelIdProperty = GetProperty<WornTableRow>("EquipModelId"),
			EquipModelCategoryProperty = GetProperty<WornTableRow>("EquipModelCategory"),
			EquipModelGenderProperty = GetProperty<WornTableRow>("EquipModelGender");

		/// <summary>Equipment model type</summary>
		/// <remarks>
		/// Japanese short name: "装備モデル種別", Google translated: "Equipment model type".
		/// Japanese description: "装備モデルの種別", Google translated: "Type of equipment model".
		/// </remarks>
		[ParameterTableRowAttribute("equipModelCategory", index: 13, minimum: 0, maximum: 99, step: 1, order: 100, unknown2: 1)]
		[DisplayName("Equipment model type")]
		[Description("Type of equipment model")]
		[DefaultValue((EquipModelCategory)0)]
		public EquipModelCategory EquipModelCategory {
			get { return equipModelCategory; }
			set { SetProperty(ref equipModelCategory, ref value, EquipModelCategoryProperty); }
		}

		/// <summary>Equipment model sex</summary>
		/// <remarks>
		/// Japanese short name: "装備モデル性別", Google translated: "Equipment model sex".
		/// Japanese description: "装備モデルの性別", Google translated: "Gender model of equipment".
		/// </remarks>
		[ParameterTableRowAttribute("equipModelGender", index: 14, minimum: 0, maximum: 99, step: 1, order: 200, unknown2: 1)]
		[DisplayName("Equipment model sex")]
		[Description("Gender model of equipment")]
		[DefaultValue((EquipModelGender)0)]
		public EquipModelGender EquipModelGender {
			get { return equipModelGender; }
			set { SetProperty(ref equipModelGender, ref value, EquipModelGenderProperty); }
		}

		/// <summary>Equipment model number</summary>
		/// <remarks>
		/// Japanese short name: "装備モデル番号", Google translated: "Equipment model number".
		/// Japanese description: "装備モデルの番号.", Google translated: "Number of equipment models .".
		/// </remarks>
		[ParameterTableRowAttribute("equipModelId", index: -1, minimum: 0, maximum: 9999, step: 1, order: -1, unknown2: 1)]
		[DisplayName("Equipment model number")]
		[Description("Number of equipment models .")]
		[DefaultValue((UInt16)0)]
		public UInt16 EquipModelId {
			get { return equipModelId; }
			set {
				if ((double)value < 0 || (double)value > 9999)
					throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 9999 for EquipModelId.");
				SetProperty(ref equipModelId, ref value, EquipModelIdProperty);
			}
		}

		public WornTableRow(ParameterTable table, int index, AssetLoader loader)
			: base(table, index, loader) {
		}

		public WornTableRow(ParameterTable table, int index)
			: base(table, index) {
		}
	}

	/// <summary><see cref="Armor"/>s and <see cref="Weapon"/>s.</summary>
	public abstract class ClothingTableRow : WornTableRow {
		int fixPrice = 0, wanderingEquipId = 0;
		short oldSortId = 0;
		ushort durability = 100, durabilityMax = 100;

		public static readonly PropertyInfo
			WanderingEquipIdProperty = GetProperty<ClothingTableRow>("WanderingEquipId"),
			OldSortIdProperty = GetProperty<ClothingTableRow>("OldSortId"),
			DurabilityProperty = GetProperty<ClothingTableRow>("Durability"),
			DurabilityMaxProperty = GetProperty<ClothingTableRow>("DurabilityMax"),
			FixPriceProperty = GetProperty<ClothingTableRow>("FixPrice");

		/// <summary>Durability</summary>
		/// <remarks>
		/// Japanese short name: "耐久度", Google translated: "Durability".
		/// Japanese description: "初期耐久度.", Google translated: "Initial durability .".
		/// </remarks>
		[ParameterTableRowAttribute("durability", index: -1, minimum: 0, maximum: 999, step: 1, order: -1, unknown2: 1)]
		[DisplayName("Durability")]
		[Description("Initial durability .")]
		[DefaultValue((UInt16)100)]
		public UInt16 Durability {
			get { return durability; }
			set {
				if ((double)value < 0 || (double)value > 999)
					throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for Durability.");
				SetProperty(ref durability, ref value, DurabilityProperty);
			}
		}

		/// <summary>Durability maximum value</summary>
		/// <remarks>
		/// Japanese short name: "耐久度最大値", Google translated: "Durability maximum value".
		/// Japanese description: "新品耐久度.", Google translated: "New durability .".
		/// </remarks>
		[ParameterTableRowAttribute("durabilityMax", index: -1, minimum: 0, maximum: 999, step: 1, order: -1, unknown2: 1)]
		[DisplayName("Durability maximum value")]
		[Description("New durability .")]
		[DefaultValue((UInt16)100)]
		public UInt16 DurabilityMax {
			get { return durabilityMax; }
			set {
				if ((double)value < 0 || (double)value > 999)
					throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 999 for DurabilityMax.");
				SetProperty(ref durabilityMax, ref value, DurabilityMaxProperty);
			}
		}

		/// <summary>Repair price</summary>
		/// <remarks>
		/// Japanese short name: "修理価格", Google translated: "Repair price".
		/// Japanese description: "修理基本価格", Google translated: "Repair base price".
		/// </remarks>
		[ParameterTableRowAttribute("fixPrice", index: -1, minimum: 0, maximum: 1E+08, step: 1, order: -1, unknown2: 1)]
		[DisplayName("Repair price")]
		[Description("Repair base price")]
		[DefaultValue((Int32)0)]
		public Int32 FixPrice {
			get { return fixPrice; }
			set {
				if ((double)value < 0 || (double)value > 1E+08)
					throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1E+08 for FixPrice.");
				SetProperty(ref fixPrice, ref value, FixPriceProperty);
			}
		}

		/// <summary>Old sort ID</summary>
		/// <remarks>
		/// Maximum was 10437; <see cref="Armor"/> needed the value.
		/// 
		/// Japanese short name: "旧ソートID", Google translated: "Old sort ID".
		/// Japanese description: "旧ソートID(-1:集めない)", Google translated: "Old sort ID (-1: it is not collected )".
		/// </remarks>
		[ParameterTableRowAttribute("oldSortId", index: -1, minimum: -32768, maximum: 32767, step: 1, order: -1, unknown2: 1)]
		[DisplayName("Old sort ID")]
		[Description("Old sort ID (-1: it is not collected )")]
		[DefaultValue((Int16)0)]
		public Int16 OldSortId {
			get { return oldSortId; }
			set { SetProperty(ref oldSortId, ref value, OldSortIdProperty); }
		}

		/// <summary>Loitering equipment ID</summary>
		/// <remarks>
		/// Japanese short name: "徘徊装備ID", Google translated: "Loitering equipment ID".
		/// Japanese description: "徘徊ゴースト用の差し替え装備ID.", Google translated: "Replacement equipment ID of the wandering ghost for .".
		/// </remarks>
		[ParameterTableRowAttribute("wanderingEquipId", index: -1, minimum: 0, maximum: 1E+09, step: 1, order: -1, unknown2: 1)]
		[DisplayName("Loitering equipment ID")]
		[Description("Replacement equipment ID of the wandering ghost for .")]
		[DefaultValue((Int32)0)]
		public Int32 WanderingEquipId {
			get { return wanderingEquipId; }
			set {
				if ((double)value < 0 || (double)value > 1E+09)
					throw new ArgumentOutOfRangeException("value", "value of " + value + " is out of range 0 to 1E+09 for WanderingEquipId.");
				SetProperty(ref wanderingEquipId, ref value, WanderingEquipIdProperty);
			}
		}

		public ClothingTableRow(ParameterTable table, int index, AssetLoader loader)
			: base(table, index, loader) {
		}

		public ClothingTableRow(ParameterTable table, int index)
			: base(table, index) {
		}
	}
}
