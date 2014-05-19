using Glare.Assets;
using Glare.Assets.Controls;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Alexandria.Engines.DarkSouls {
	public class ParameterDefinition : FolderAsset {
		public override string DisplayName {
			get {
				return Name + " (" + Unknowns.ToCommaSeparatedList() + ")";
			}
		}

		internal ParameterDefinition(AssetManager manager, AssetLoader loader)
			: base(manager, loader) {
			var reader = loader.Reader;

			loader.Expect(loader.ShortLength);
			loader.Expect((ushort)0x30); // Offset of rows
			Unknowns.ReadInt16s(reader, 1); // 1-4
			int rowCount = reader.ReadUInt16();
			loader.Expect((ushort)0xB0); // Size in bytes of a row
			Name = reader.ReadStringz(32, EncodingShiftJis);
			loader.Expect((ushort)0);
			loader.Expect((ushort)0x68);

			for (int index = 0; index < rowCount; index++)
				new ParameterDefinitionRow(this, index, loader);
		}

		public override void FillContextMenu(ContextMenuStrip strip) {
			base.FillContextMenu(strip);

			strip.Items.Add(new ToolStripButton("Copy Japanese text lines to clipboard", null, CopyJapaneseTextLinesToClipboard));
			strip.Items.Add(new ToolStripButton("Copy C# code to clipboard (integrating translated Japanese text lines if on clipboard)", null, CopyCSharpCodeToClipboard));

		}

		const string JapaneseTranslationMarker = "Japanese to English translation lines";

		void CopyJapaneseTextLinesToClipboard(object sender, EventArgs args) {
			StringBuilder builder = new StringBuilder();

			builder.Append(JapaneseTranslationMarker + "\n");
			foreach (ParameterDefinitionRow row in Children)
				builder.AppendFormat("{0}\n{1}\n{2}\n\n", row.DotNetName, (row.JapaneseShortName ?? "").Replace("\n", "\\n"), (row.JapaneseDescription ?? "").Replace("\n", "\\n"));

			Clipboard.Clear();
			Clipboard.SetText(builder.ToString());
		}

		void CopyCSharpCodeToClipboard(object sender, EventArgs args) {
			StringBuilder builder = new StringBuilder();
			int bitCount = 0;

			Dictionary<string, string> translatedShortNames = new Dictionary<string,string>();
			Dictionary<string, string> translatedDescriptions = new Dictionary<string,string>();

			#region Detect an unknown "_BOOL" type and report it.
			foreach (ParameterDefinitionRow row in Children) {
				if (row.DotNetType.EndsWith("_BOOL") || row.RealDotNetType.EndsWith("_BOOL")) {
					MessageBox.Show("This has an unhandled '_BOOL' type, the code will be wrong.");
					break;
				}
			}
			#endregion

			#region Read the translated names from the clipboard.
			if (Clipboard.ContainsText()) {
				string[] lines = Clipboard.GetText().TrimStart().Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

				if(lines.Length > 0 && lines[0] == JapaneseTranslationMarker) {
					for (int offset = 1; offset < lines.Length; offset += 4) {
						string id = lines[offset];
						translatedShortNames[id] = lines.TryGet(offset + 1, "");
						translatedDescriptions[id] = lines.TryGet(offset + 2, "");
					}
				}
			}
			#endregion

			#region Print information to paste into various locations.
			builder.Append("/*\n");
			builder.AppendFormat(
				"For ParameterTable.cs under {1}.ReadRow:\n" +
				"	case TableRows.{0}.TableName: return new TableRows.{0}(table, index, loader, next);\n", Name, typeof(ParameterTableRow).Name);
			builder.AppendFormat(
				"For ParameterDefinition.cs under {0}.GetDotNetType():\n", typeof(ParameterDefinitionRow).Name);
			HashSet<string> unknownTypes = new HashSet<string>();
			string enumerations = "";
			
			foreach (ParameterDefinitionRow row in Children)
				if (ParameterDefinitionRow.GetDotNetType(row.Type) == null && unknownTypes.Add(row.Type)) {
					builder.AppendFormat("\tcase \"{0}\": return typeof({0}).Name;\n", row.Type);
					enumerations += string.Format(
						"\n\t/// <summary></summary>\n" +
						"\t/// <remarks>\"{0}\" in Dark Souls.</remarks>\n" +
						"\tpublic enum {0} : {1} {{\n" +
						"\t}}\n", row.Type, row.RealDotNetType.ToLower());
				}

			if(enumerations != null)
				builder.AppendFormat("For Enumerations.cs:{0}", enumerations);

			builder.Append("*/\n");
			#endregion

			builder.AppendFormat(
				"using Glare.Assets;\nusing System;\nusing System.Collections.Generic;\nusing System.ComponentModel;\nusing System.IO;\nusing System.Linq;\nusing System.Reflection;\nusing System.Text;\n\n" +
				"namespace Alexandria.Engines.DarkSouls {{\n" +
				"\tpartial class TableRows {{\n");

			if (LoadContext is ArchiveRecord) {
				ArchiveRecord record = (ArchiveRecord)LoadContext;
				builder.AppendFormat(
					"\t\t/// <summary></summary>\n" +
					"\t\t/// <remarks>\n" +
					"\t\t/// Defined as \"{0}\" in Dark Souls in the file \"{1}\" (id {2:X2}h).\n" +
					"\t\t/// </remarks>\n", Name, record.Name, record.Id);
			}

			builder.AppendFormat(
				"\t\tpublic class {0} : {1} {{\n" +
				"\t\t\tpublic const string TableName = \"{0}\";\n\n", Name, typeof(ParameterTableRow).Name);

			#region Print field definitions, grouping them up to make them easier to deal with.
			Dictionary<string, string> fieldDefinitions = new Dictionary<string, string>();

			// Define the fields.
			foreach (ParameterDefinitionRow row in Children) {
				if (row.IsBitField)
					continue;

				string type = row.DotNetType;
				string name = row.DotNetFieldName;
				string current;

				fieldDefinitions[type] = fieldDefinitions.TryGetValue(type, out current) ?
					current + ", " + name :
					name;
			}

			// Print the field definitions.
			foreach (var item in fieldDefinitions)
				builder.AppendFormat("\t\t\t{0} {1};\n", item.Key, item.Value);
			builder.Append("\n");
			#endregion

			#region Print the PropertyInfo declarations.
			builder.AppendFormat("\t\t\tpublic static readonly {0}\n", typeof(PropertyInfo).Name);
			foreach (ParameterDefinitionRow row in Children)
				builder.AppendFormat("\t\t\t\t{0}Property = GetProperty<{1}>(\"{0}\"){2}\n", row.DotNetName, Name, row == Children[Children.Count - 1] ? ";" : ",");
			builder.Append("\n");
			#endregion

			#region Print the property definitions
			bitCount = 0;
			foreach (ParameterDefinitionRow row in Children) {
				string getProperty = "return " + row.DotNetFieldName + ";";
				string setProperty = string.Format("SetProperty(ref {0}, ref value, {1}Property);", row.DotNetFieldName, row.DotNetName);

				if (row.IsBitField) {
					getProperty = string.Format("return {3}GetBitProperty({0}, {1}, {2}Property){4};",
						bitCount,
						row.BitFieldBits,
						row.DotNetName,
						row.IsBoolean ? "" : "(" + row.DotNetType + ")",
						row.IsBoolean ? " != 0" : "");
					setProperty = string.Format("SetBitProperty({0}, {1}, {3}, {2}Property);", bitCount, row.BitFieldBits, row.DotNetName, row.IsBoolean ? "value ? 1 : 0" : "(int)value");
					bitCount += row.BitFieldBits;
				}

				string translatedShortName = translatedShortNames.TryGetValue(row.DotNetName, "");
				string translatedDescription = translatedDescriptions.TryGetValue(row.DotNetName, "");

				builder.AppendFormat(
					"\t\t\t/// <summary>{0}</summary>\n" +
					"\t\t\t/// <remarks>\n" +
					"\t\t\t/// Japanese short name: \"{2}\", Google translated: \"{0}\".\n" +
					"\t\t\t/// Japanese description: \"{3}\", Google translated: \"{1}\".\n" +
					"\t\t\t/// </remarks>\n",
					translatedShortName, translatedDescription, // {0} and {1}
					row.JapaneseShortName, row.JapaneseDescription); // {2} and {3}

				builder.AppendFormat("\t\t\t[{0}(\"{1}\", index: {2}, @default: {3}, minimum: {4}, maximum: {5}, step: {6}, order: {7}, unknown2: {8})]\n",
					typeof(ParameterTableRowAttribute).Name, row.Name, row.Index, row.ValueDefault, row.ValueMinimum, row.ValueMaximum, row.ValueStep, row.UnknownValue1, row.UnknownValue2);

				builder.AppendFormat("\t\t\t[DisplayName(\"{0}\")]\n", translatedShortName.Replace("\"", "\\\""));
				builder.AppendFormat("\t\t\t[Description(\"{0}\")]\n", translatedDescription.Replace("\"", "\\\""));

				if(!row.IsArray)
					builder.AppendFormat("\t\t\t[DefaultValue({0})]\n", row.CSharpValueDefault);


				if (row.Type == "dummy8")
					builder.AppendFormat("\t\t\t[Browsable(false)]\n");

				builder.AppendFormat("\t\t\tpublic {0} {1} {{\n", row.DotNetType, row.DotNetName);
				builder.AppendFormat("\t\t\t\tget {{ {0} }}\n", getProperty);

				if (!row.IsEnum && !row.IsBoolean && !row.IsArray)
					builder.AppendFormat(
						"\t\t\t\tset {{\n" +
						"\t\t\t\t\tif((double)value < {1} || (double)value > {2})\n" +
						"\t\t\t\t\t\tthrow new ArgumentOutOfRangeException(\"value\", \"value of \" + value + \" is out of range {1} to {2} for \" + {0}Property.Name + \".\");\n" +
						"\t\t\t\t\t{3}\n" +
						"\t\t\t\t}}\n", row.DotNetName, row.ValueMinimum, row.ValueMaximum, setProperty);
				else
					builder.AppendFormat("\t\t\t\tset {{ {0} }}\n", setProperty);

				builder.Append("\t\t\t}\n");
				builder.Append("\n");
			}
			#endregion

			#region Print the constructor
			builder.AppendFormat(
				"\t\t\tinternal {0}({1} table, int index, {2} loader, int next)\n" +
				"\t\t\t\t: base(table, index, loader) {{\n" +
				"\t\t\t\tBinaryReader reader = loader.Reader;\n\n", Name, typeof(ParameterTable).Name, typeof(AssetLoader).Name);

			bool bitsMode = false, hadBits = false;

			foreach (ParameterDefinitionRow row in Children) {
				if (row.IsBitField) {
					if (bitsMode == false) {
						if (hadBits)
							MessageBox.Show("Multiple bit fields counted; code will not be correct.");
						hadBits = true;
						bitsMode = true;

						bitCount = 0;
						for (int bitCounter = row.Index; bitCounter < Children.Count; bitCounter++) {
							ParameterDefinitionRow bitRow = (ParameterDefinitionRow)Children[bitCounter];
							if (!bitRow.IsBitField)
								continue;
							bitCount += bitRow.BitFieldBits;
						}

						builder.AppendFormat("\t\t\t\tBitFields = reader.ReadBytes({0});\n", (bitCount + 7) / 8);
					}
				} else {
					bitsMode = false;
					if (row.DataType == "dummy8")
						builder.AppendFormat("\t\t\t\t{0} = reader.ReadBytes({1});\n", row.DotNetName, row.Size);
					else
						builder.AppendFormat("\t\t\t\t{0} = {1}reader.Read{2}();\n", row.DotNetName, row.IsEnum ? "(" + row.DotNetType + ")" : "", row.RealDotNetType);
				}
			}
			builder.Append("\t\t\t}\n\n");
			#endregion

			#region Print the initialiser constructor.
			builder.AppendFormat(
				"\t\t\tinternal {0}({1} table, int index)\n" +
				"\t\t\t\t: base(table, index) {{\n", Name, typeof(ParameterTable).Name);

			if (bitCount > 0)
				builder.AppendFormat("\t\t\t\tBitFields = new byte[{0}];\n", (bitCount + 7) / 8);
			foreach (ParameterDefinitionRow row in Children) {
				builder.AppendFormat("\t\t\t\t{0} = {1};\n", row.DotNetName, row.CSharpValueDefault);
			}

			builder.Append("\t\t\t}\n\n");
			#endregion

			#region Print the write method.
			builder.Append("\t\t\tpublic override void Write(BinaryWriter writer) {\n");
			foreach (ParameterDefinitionRow row in Children) {
				if (row.IsBitField) {
					if (bitsMode == false) {
						bitsMode = true;
						builder.Append("\t\t\t\twriter.Write(BitFields);\n");
					}
				} else {
					if (row.DataType == "dummy8")
						builder.AppendFormat("\t\t\t\twriter.Write({0});\n", row.DotNetName);
					else
						builder.AppendFormat("\t\t\t\twriter.Write({0}{1});\n", row.IsEnum ? "(" + row.RealDotNetType + ")" : "", row.DotNetName);
				}
			}
			builder.Append("\t\t\t}\n");
			#endregion

			builder.Append("\t\t}\n");
			builder.Append("\t}\n");
			builder.Append("}\n");

			Clipboard.Clear();
			Clipboard.SetText(builder.ToString());
		}

		public override System.Windows.Forms.Control Browse() {
			List<ParameterDefinitionRow> children = new List<ParameterDefinitionRow>();
			foreach (var child in Children)
				children.Add((ParameterDefinitionRow)child);

			DataGridView view = new DoubleBufferedDataGridView() {
				AutoGenerateColumns = false,
				DataSource = children,
				ReadOnly = true,

			};

			foreach (string column in new string[] { "Index", "Name", "JapaneseShortName", "DataType", "Type", "PrintFormat", "ValueDefault", "ValueMinimum", "ValueMaximum", "ValueStep", "Size", "JapaneseDescription", "UnknownValue1", "UnknownValue2" })
				view.Columns.Add(new DataGridViewTextBoxColumn() {
					//AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
					DataPropertyName = column,
					HeaderText = column,
					ReadOnly = true,
					Resizable = DataGridViewTriState.True,
					SortMode = DataGridViewColumnSortMode.Automatic,
				});

			return view;
		}

		public override Control BrowseContents() {
			return CreateBarPanel(Browse());
		}
	}

	public class ParameterDefinitionRow : Asset {
		string DotNetBaseName {
			get {
				string name = Name.Trim();
				if (name.Contains(':'))
					name = name.Substring(0, name.IndexOf(':'));
				if (name.Contains('['))
					name = name.Substring(0, name.IndexOf('['));
				return name;
			}
		}

		/// <summary>The name of the field in .NET.</summary>
		public string DotNetFieldName {
			get {
				string name = DotNetBaseName;
				return name.Substring(0, 1).ToLowerInvariant() + name.Substring(1);
			}
		}

		/// <summary>The name of the property in .NET.</summary>
		public string DotNetName {
			get {
				var name = DotNetBaseName;
				return name.Substring(0, 1).ToUpperInvariant() + name.Substring(1);
			}
		}

		public bool IsEnum { get { return DataType != Type; } }

		public bool IsArray { get { return Name.Contains('['); } }
		public bool IsBitField { get { return Name.Contains(':'); } }
		public bool IsBoolean { get { return DotNetType == "Boolean" || BitFieldBits == 1; } }

		public int BitFieldBits {
			get {
				int index = Name.IndexOf(':');
				if (index < 0)
					return -1;
				return int.Parse(Name.Substring(index + 1));
			}
		}

		/// <summary>The name of the type in .NET. This may be an enumeration.</summary>
		public string DotNetType {
			get {
				string result = GetBitFieldType() ?? GetDotNetType(Type);
				if (result == null)
					return Type;
				return result;
			}
		}

		/// <summary>The name of the storage field in .NET. For an enumeration, this may be the raw type.</summary>
		public string RealDotNetType {
			get {
				string result = GetBitFieldType() ?? GetDotNetType(DataType);
				if (result == null)
					throw new NotSupportedException("Parameter type " + DataType + " is not supported.");
				return result;
			}
		}

		public string JapaneseDescription { get; private set; }

		public string JapaneseShortName { get; private set; }

		public string PrintFormat { get; private set; }

		/// <summary>Size in bytes.</summary>
		public int Size { get; private set; }

		public string DataType { get; private set; }

		public string Type { get; private set; }

		public float ValueDefault { get; private set; }

		public float ValueMinimum { get; private set; }

		public float ValueMaximum { get; private set; }

		public float ValueStep { get; private set; }

		public int UnknownValue1 { get; private set; }

		public int UnknownValue2 { get; private set; }

		public int Index { get; private set; }

		public string CSharpValueDefault {
			get {
				if(IsBoolean)
					return ValueDefault != 0 ? "true" : "false";
				if (IsArray)
					return "new Byte[" + Size + "]";
				string text = "(" + DotNetType + ")";
				if (ValueDefault < 0)
					return text + "(" + ValueDefault + ")";
				return text + ValueDefault;
			}
		}

		internal ParameterDefinitionRow(ParameterDefinition definition, int index, AssetLoader loader)
			: base(definition, "") {
			var reader = loader.Reader;

			Index = index;
			JapaneseShortName = reader.ReadStringz(64, EncodingShiftJis).Trim();
			DataType = reader.ReadStringz(8, EncodingShiftJis);
			PrintFormat = reader.ReadStringz(8, EncodingShiftJis);
			ValueDefault = reader.ReadSingle();
			ValueMinimum = reader.ReadSingle();
			ValueMaximum = reader.ReadSingle();
			ValueStep = reader.ReadSingle();
			UnknownValue2 = reader.ReadInt32();
			Size = reader.ReadInt32();
			JapaneseDescription = (reader.ReadStringzAtUInt32(EncodingShiftJis) ?? "").Trim();
			Type = reader.ReadStringz(32, EncodingShiftJis).Trim();
			Name = reader.ReadStringz(32, EncodingShiftJis).Trim();
			UnknownValue1 = reader.ReadInt32();

			var name = Name;
		}

		string GetBitFieldType() {
			var bits = BitFieldBits;
			if (bits >= 0 && !IsEnum) {
				if (bits == 1)
					return "Boolean";
				return "Byte";
			}
			return null;
		}

		public static string GetDotNetType(string type) {
			switch (type) {
				case "f32": return "Single";
				case "s8": return "SByte";
				case "s16": return "Int16";
				case "s32": return "Int32";
				case "u8": return "Byte";
				case "u16": return "UInt16";
				case "u32": return "UInt32";
				case "dummy8": return "Byte[]";

				case "ATK_PARAM_BOOL":
				case "EQUIP_BOOL":
				case "MAGIC_BOOL":
				case "NPC_BOOL":
				case "SP_EFFECT_BOOL": return "Boolean";

				case "EQUIP_MODEL_CATEGORY": return typeof(EquipModelCategory).Name;
				case "EQUIP_MODEL_GENDER": return typeof(EquipModelGender).Name;
				case "WEAPON_CATEGORY": return typeof(WeaponCategory).Name;
				case "WEPMOTION_CATEGORY": return typeof(WeaponMotionCategory).Name;
				case "GUARDMOTION_CATEGORY": return typeof(GuardMotionCategory).Name;
				case "WEP_MATERIAL_ATK": return typeof(WeaponMaterialAttack).Name;
				case "WEP_MATERIAL_DEF": return typeof(WeaponMaterialDefend).Name;
				case "WEP_MATERIAL_DEF_SFX": return typeof(WeaponMaterialDefendSound).Name;
				case "WEP_CORRECT_TYPE": return typeof(WeaponCorrectType).Name;
				case "ATKPARAM_SPATTR_TYPE": return typeof(AttackParameterSpecialAttributes).Name;
				case "DURABILITY_DIVERGENCE_CATEGORY": return typeof(DurabilityDivergenceCategory).Name;
				case "WEP_BASE_CHANGE_CATEGORY": return typeof(WeaponBaseChangeCategory).Name;
				case "PROTECTOR_CATEGORY": return typeof(ArmorCategory).Name;
				case "ATK_PARAM_PARTSDMGTYPE": return typeof(AttackParameterPartDamageType).Name;
				case "GOODS_TYPE": return typeof(ItemType).Name;
				case "BEHAVIOR_REF_TYPE": return typeof(BehaviorRefType).Name;
				case "BEHAVIOR_CATEGORY": return typeof(BehaviorCategory).Name;
				case "GOODS_CATEGORY": return typeof(ItemCategory).Name;
				case "GOODS_USE_ANIM": return typeof(ItemUseAnimation).Name;
				case "GOODS_OPEN_MENU": return typeof(ItemUseMenu).Name;
				case "SP_EFFECT_USELIMIT_CATEGORY": return typeof(SpecialEffectUseLimitCategory).Name;
				case "REPLACE_CATEGORY": return typeof(ReplacementCategory).Name;
				case "ACCESSORY_CATEGORY": return typeof(AccessoryCategory).Name;
				case "FACE_PARAM_HAIRSTYLE_TYPE": return typeof(FaceHairStyle).Name;
				case "FACE_PARAM_HAIRCOLOR_TYPE": return typeof(FaceHairColor).Name;
				case "NPC_ITEMDROP_TYPE": return typeof(NpcItemDropType).Name;
				case "NPC_DRAW_TYPE": return typeof(NpcDrawType).Name;
				case "NPC_TYPE": return typeof(NpcType).Name;
				case "NPC_TEMA_TYPE": return typeof(NpcTemaType).Name;
				case "NPC_MOVE_TYPE": return typeof(NpcMoveType).Name;
				case "NPC_BURN_TYPE": return typeof(NpcBurnType).Name;
				case "NPC_SFX_SIZE": return typeof(NpcSfxSize).Name;
				case "NPC_HITSTOP_TYPE": return typeof(NpcHitStopType).Name;
				case "THROW_PAD_TYPE": return typeof(ThrowPadType).Name;
				case "THROW_ENABLE_STATE": return typeof(ThrowEnableState).Name;
				case "THROW_TYPE": return typeof(ThrowType).Name;
				case "THROW_DMY_CHR_DIR_TYPE": return typeof(ThrowDmyCharacterDirectionType).Name;
				case "ACTION_PATTERN": return typeof(ActionPattern).Name;
				case "ATKPARAM_ATKATTR_TYPE": return typeof(AttackAttackAttributes).Name;
				case "ATK_TYPE": return typeof(AttackType).Name;
				case "ATK_SIZE": return typeof(AttackSize).Name;
				case "BULLET_LAUNCH_CONDITION_TYPE": return typeof(BulletLaunchConditionType).Name;
				case "BULLET_FOLLOW_TYPE": return typeof(BulletFollowType).Name;
				case "BULLET_EMITTE_POS_TYPE": return typeof(BulletEmitterPosition).Name;
				case "BULLET_ATTACH_EFFECT_TYPE": return typeof(BulletAttachEffect).Name;
				case "CHARACTER_INIT_SEX": return typeof(CharacterInitialSex).Name;
				case "CHRINIT_VOW_TYPE": return typeof(CharacterInitialVow).Name;
				case "ENEMY_BEHAVIOR_ID": return typeof(EnemyBehaviorId).Name;
				case "ChrType": return typeof(CharacterTyep).Name;
				case "HMP_FOOT_EFFECT_HEIGHT_TYPE": return typeof(HitMaterialFootEffectHeight).Name;
				case "HMP_FOOT_EFFECT_DIR_TYPE": return typeof(HitMaterialFootEffectDirection).Name;
				case "HMP_FLOOR_HEIGHT_TYPE": return typeof(HitMaterialFloorHeight).Name;
				case "ITEMLOT_ITEMCATEGORY": return typeof(RewardItemCategory).Name;
				case "ITEMLOT_ENABLE_LUCK": return typeof(RewardEnableLuck).Name;
				case "ITEMLOT_CUMULATE_RESET": return typeof(RewardCumulateReset).Name;
				case "MAGIC_CATEGORY": return typeof(MagicCategory).Name;
				case "MAGIC_MOTION_TYPE": return typeof(MagicMotion).Name;
				case "SP_EFFECT_TYPE": return typeof(SpecialEffectType).Name;
				case "OBJACT_SP_QUALIFIED_TYPE": return typeof(ObjectActionSpecialQualifiedType).Name;
				case "OBJACT_CHR_SORB_TYPE": return typeof(ObjectActionCharacterSOrbType).Name;
				case "OBJACT_EVENT_KICK_TIMING": return typeof(ObjectActionEventKickTiming).Name;
				case "NPC_THINK_GOAL_ACTION": return typeof(NpcThoughtGoalAction).Name;
				case "NPC_THINK_REPLY_BEHAVIOR_TYPE": return typeof(NpcThoughtReplyBehavior).Name;
				case "SKELETON_PARAM_KNEE_AXIS_DIR": return typeof(SkeletonKneeAxisDirection).Name;
				case "SHOP_LINEUP_SHOPTYPE": return typeof(StoreInventoryType).Name;
				case "SHOP_LINEUP_EQUIPTYPE": return typeof(StoreInventoryEquipmentType).Name;
				case "SP_EFFECT_VFX_EFFECT_TYPE": return typeof(SpecialEffectVfxEffectType).Name;
				case "SP_EFFECT_VFX_SOUL_PARAM_TYPE": return typeof(SpecialEffectVfxSoulParameterType).Name;
				case "SP_EFFECT_VFX_PLAYCATEGORY": return typeof(SpecialEffectVfxPlayCategory).Name;
				case "SP_EFFECT_SPCATEGORY": return typeof(SpecialEffectSpCategory).Name;
				case "SP_EFFECT_SAVE_CATEGORY": return typeof(SpecialEffectSaveCategory).Name;
				case "ATKPARAM_REP_DMGTYPE": return typeof(ATKPARAM_REP_DMGTYPE).Name;
				case "SP_EFE_WEP_CHANGE_PARAM": return typeof(SpecialEffectWeaponChange).Name;
				case "SP_EFFECT_MOVE_TYPE": return typeof(SpecialEffectMoveType).Name;
				case "SP_EFFECT_THROW_CONDITION_TYPE": return typeof(SpecialEffectThrowCondition).Name;
				default: return null;
			}
		}
	}

	public class ParameterDefinitionFormat : AssetFormat {
		public ParameterDefinitionFormat(Engine engine)
			: base(engine, typeof(ParameterDefinition), canLoad: true, extension: ".paramdef") {
		}

		public override LoadMatchStrength LoadMatch(AssetLoader loader) {
			var reader = loader.Reader;

			if (loader.Length < 0x30)
				return LoadMatchStrength.None;
			if (!reader.ReadMatch(loader.ShortLength) || !reader.ReadMatch((ushort)0x30))
				return LoadMatchStrength.None;
			ushort value = reader.ReadUInt16();
			int rowCount = reader.ReadUInt16();
			int rowSize = reader.ReadUInt16();
			if (rowSize != 0xB0)
				return LoadMatchStrength.None;
			if (loader.Length < 0x30 + rowCount * rowSize)
				return LoadMatchStrength.None;

			return LoadMatchStrength.Medium;
		}

		public override Asset Load(AssetLoader loader) {
			return new ParameterDefinition(Manager, loader);
		}
	}
}
