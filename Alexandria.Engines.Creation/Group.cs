using Alexandria.Engines.Creation.Groups;
using Glare;
using Glare.Assets;
using Glare.Framework;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.Creation {
	/// <summary>A group in a <see cref="Module"/>.</summary>
	public class Group : FolderAsset {
		/// <summary>Get the end of the content in the module.</summary>
		public long ContentEnd { get { return ContentOffset + ContentSize; } }

		/// <summary>Get the offset of the content in the module.</summary>
		public long ContentOffset { get; private set; }

		/// <summary>Get the size of the group contents, excluding the header.</summary>
		public uint ContentSize { get; private set; }

		public override string DisplayName {
			get {
				return string.Format("{0} ({1}, Version {2}, Unknowns {3})", Name, TimeStamp.ToString("d"), Version, Unknowns.ToCommaSeparatedList());
			}
		}

		/// <summary>Get the type of the group.</summary>
		public ModuleGroupType GroupType { get; private set; }

		/// <summary>Get the label, whose interpretation changes depending upon usage.</summary>
		public uint LabelUInt32 { get; private set; }

		/// <summary>Get the <see cref="LabelUInt32"/> inte rpreted as four ASCII characters.</summary>
		public string LabelId { get { return Extensions.ToId4((int)LabelUInt32); } }

		/// <summary>Get the <see cref="LabelUInt32"/> interpreted as a <see cref="FormId"/>.</summary>
		public FormId LabelFormId { get { return new FormId(LabelUInt32); } }

		/// <summary>Get the <see cref="LabelUInt32"/> interpreted as coordinates.</summary>
		public Vector2i LabelPosition { get { return new Vector2i((short)(LabelUInt32 >> 16), (short)(LabelUInt32 & 0xFFFF)); } }

		/// <summary>Get the proper value of the label in the correct type for <see cref="GroupType"/>.</summary>
		public string LabelValue {
			get {
				switch (GroupType) {
					case ModuleGroupType.CellDistantChild:
					case ModuleGroupType.CellChild:
					case ModuleGroupType.CellPersistentChild:
					case ModuleGroupType.CellTemporaryChild:
					case ModuleGroupType.TopicChild:
						return LabelFormId.ToString();

					case ModuleGroupType.ExteriorCellBlock:
					case ModuleGroupType.ExteriorCellSubBlock:
						return string.Format("({0}, {1})", LabelPosition.X, LabelPosition.Y);

					case ModuleGroupType.InteriorCellBlock:
					case ModuleGroupType.InteriorCellSubBlock:
						return LabelUInt32.ToString();

					case ModuleGroupType.Top:
					case ModuleGroupType.WorldChildren:
						return LabelId;

					default:
						throw new NotSupportedException();
				}
			}
		}

		/// <summary>Get the <see cref="Alexandria.Engines.Creation.Module"/> this <see cref="Group"/> is in.</summary>
		public Module Module { get { return FindAncestor<Module>(); } }

		/// <summary>Get the <see cref="DateTime"/> for when this group was created. This is reduced to number of days since 2002-12-01.</summary>
		public DateTime TimeStamp { get; private set; }

		/// <summary>Get the version number.</summary>
		public int Version { get; private set; }

		static internal Group ReadGroup(Module module) {
			BinaryReader reader = module.Reader;

			string type = reader.ReadId4();
			if (type != "GRUP")
				throw new InvalidDataException(string.Format("Expected GRUP; received {0}.", type));
			uint contentSize = checked(reader.ReadUInt32() - 24);
			uint label = reader.ReadUInt32();
			string labelId = Extensions.ToId4((int)label);
			ModuleGroupType groupType = (ModuleGroupType)reader.ReadInt32();

			switch (groupType) {
				case ModuleGroupType.Top:
					switch(labelId) {
						case "CELL":
						case "WRLD":
						case "DIAL":
							return new GroupTopGroup(module, contentSize, label);
						default:
							return new RecordTopGroup(module, contentSize, label);
					}

				default:
					throw new NotImplementedException("Group type " + groupType + " is not implemented.");
			}
		}

		internal Group(Module module, uint contentSize, uint label, ModuleGroupType groupType)
			: base(module.Manager, "") {
			BinaryReader reader = module.Reader;

			ContentSize = contentSize;
			LabelUInt32 = label;
			GroupType = groupType;

			var timeStampDay = reader.ReadByte(); // One-based day in the month.
			var timeStampMonth = reader.ReadByte();
			TimeStamp = new DateTime(2002, 12, 1).AddMonths(timeStampMonth);
			int currentMonth = TimeStamp.Month;
			if (timeStampDay == 0)
				throw new InvalidDataException();
			TimeStamp = TimeStamp.AddDays(timeStampDay - 1);
			if (TimeStamp.Month != currentMonth)
				throw new InvalidDataException();
			//.AddDays(timeStampDay);


			Unknowns.ReadInt16s(reader, 1);
			Version = reader.ReadUInt16();
			Unknowns.ReadInt16s(reader, 1);

			ContentOffset = reader.BaseStream.Position;
			reader.BaseStream.Seek(ContentSize, SeekOrigin.Current);

			Name = GroupType + " " + LabelValue;
		}
	}

	/// <summary>Type of a <see cref="Group"/>.</summary>
	public enum ModuleGroupType {
		/// <summary>Top-level group. <see cref="Group.LabelId"/> is the record type.</summary>
		Top = 0,

		/// <summary>Child groups of a "WRLD". <see cref="Group.LabelFormId"/> identifies the <see cref="FormId"/>.</summary>
		WorldChildren = 1,

		/// <summary>An interior cell block. <see cref="Group.LabelUInt32"/> identifies the <see cref="UInt32"/> block number.</summary>
		InteriorCellBlock = 2,

		/// <summary>An interior cell sub-block. <see cref="Group.LabelUInt32"/> identifies the <see cref="UInt32"/> sub-block number.</summary>
		InteriorCellSubBlock = 3,

		/// <summary>An exterior cell block. <see cref="Group.LabelPosition"/> identifies the <see cref="Vector2i"/> location.</summary>
		ExteriorCellBlock = 4,

		/// <summary>An exterior cell sub-block. <see cref="Group.LabelPosition"/> identifies the <see cref="Vector2i"/> location.</summary>
		ExteriorCellSubBlock = 5,

		/// <summary>Children of a cell. <see cref="Group.LabelFormId"/> identifies the <see cref="FormId"/>. This group is a child of "CELL".</summary>
		CellChild = 6,

		/// <summary>Children of a topic. <see cref="Group.LabelFormId"/> identifies the <see cref="FormId"/>. This group is a child of a "DIAL".</summary>
		TopicChild = 7,

		/// <summary>Persistent children of a cell. <see cref="Group.LabelFormId"/> identifies the <see cref="FormId"/>. This group is a child of "CELL".</summary>
		CellPersistentChild = 8,

		/// <summary>Temporary children of a cell. <see cref="Group.LabelFormId"/> identifies the <see cref="FormId"/>. This group is a child of "CELL".</summary>
		CellTemporaryChild = 9,

		/// <summary>Distant visible children of a cell. <see cref="Group.LabelFormId"/> identifies the <see cref="FormId"/>. This group is a child of "CELL".</summary>
		CellDistantChild = 10,
	}
}
