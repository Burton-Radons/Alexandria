using Glare;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using Glare.Assets;
using System.Windows.Forms;
using System.ComponentModel;
using System.Reflection;
using System.Collections;

namespace Alexandria.Engines.DarkSouls {
	public class TableArchive : FolderAsset {
		internal TableArchive(AssetManager manager, AssetLoader loader)
			: base(manager, loader.Name) {
			while (loader.Position < loader.Length)
				new Table(this, loader);
		}
	}

	public partial class Table : TableAsset<TableRow> {
		Archive RootArchive {
			get {
				var archiveRecord = LoadContext as ArchiveRecord;
				if (archiveRecord == null)
					return null;
				Archive parent = archiveRecord.Archive;
				ArchiveRecord parentContext = parent.Context as ArchiveRecord;
				if (parentContext == null)
					return null;
				return parentContext.Archive;
			}
		}

		internal Table(TableArchive archive, AssetLoader loader)
			: base(archive, loader) {
			var reader = loader.Reader;

			var tables = archive.Children;
			var children = Children;
			var errors = loader.Errors;

			reader.RequireZeroes(4);
			var name = Name = reader.ReadStringzAtUInt32(Encoding.ASCII);
			int count = reader.ReadInt32();
			int[] offsets = reader.ReadArrayInt32(count);

			if (offsets[count - 1] == 0)
				offsets[count - 1] = loader.ShortLength;

			for (int index = 0; index < count - 1; index++) {
				reader.BaseStream.Position = offsets[index];
				TableRow.ReadRow(this, index, loader, offsets[index + 1]);
			}

			reader.BaseStream.Position = offsets[count - 1];
		}

		protected Table(AssetManager manager, AssetLoader loader) : base(manager, loader) { }

		Dictionary<Engine.ItemArchiveId, Dictionary<Language, StringArchive>> StringArchives = new Dictionary<Engine.ItemArchiveId, Dictionary<Language, StringArchive>>();
		Dictionary<ParameterTableIndex, ParameterTable> ParameterTables = new Dictionary<ParameterTableIndex, ParameterTable>();
		Archive GameParametersArchive;

		public Archive GetGameParametersArchive() {
			if (GameParametersArchive != null)
				return GameParametersArchive;

			Archive root = RootArchive;
			if (root == null)
				return null;

			var archive = root.FindRecordByPath("param/GameParam/GameParam.parambnd.dcx");
			if (archive == null)
				return null;

			return GameParametersArchive = (Archive)archive.Contents;
		}

		public ParameterTable GetGameParameters(ParameterTableIndex index) {
			ParameterTable table;

			if (ParameterTables.TryGetValue(index, out table))
				return table;

			var archive = GetGameParametersArchive();
			if (archive == null)
				return null;

			var record = archive.FindRecordById((int)index);
			table = (ParameterTable)record.Contents;
			return table;
		}

		public StringArchive GetLocalisedStringArchive(Engine.ItemArchiveId archive, Language language = Language.English) {
			if (archive == Engine.ItemArchiveId.None)
				return null;

			Dictionary<Language, StringArchive> dictionary;
			Archive root = RootArchive;

			if (root == null)
				return null;

			dictionary = StringArchives.GetValueOrCreate(archive);
			StringArchive stringArchive = dictionary.TryGetValue(language);

			if (stringArchive == null)
				dictionary[language] = stringArchive = Engine.GetStringArchive(root, archive, language);
			return stringArchive;
		}

		public string GetLocalisedString(Engine.ItemArchiveId archive, int id, Language language = Language.English, string defaultValue = "") {
			StringArchive strings = GetLocalisedStringArchive(archive, language);
			if (strings == null)
				return defaultValue;
			string value;
			if (strings.StringsById.TryGetValue(id, out value))
				return value;
			return defaultValue;
		}
	}

	public abstract class TableRow : TableRowAsset {
		[Browsable(false)]
		public new Table Parent { get { return (Table)base.Parent; } }

		public TableRow(Table table, int index) : base(table, index) { }

		public static TableRow ReadRow(Table table, int index, AssetLoader loader, int next) {
			switch (table.Name) {
				case TableRows.Event.TableName: return new TableRows.Event(table, index, loader, next);
				case TableRows.Model.TableName: return new TableRows.Model(table, index, loader);
				case TableRows.Parts.TableName: return new TableRows.Parts(table, index, loader, next);
				case TableRows.Point.TableName: return new TableRows.Point(table, index, loader);
				default:
					throw new NotImplementedException("Table row type " + table.Name + " is not implemented.");
			}
		}

		public override string ToString() {
			return GetType().Name + "(" + DisplayName + ")";
		}
	}

	/// <summary>This contains the list of table row classes.</summary>
	public abstract partial class TableRows {
		/// <summary>"EVENT_PARAM_ST" in a MapStudio ".msb" file.</summary>
		public class Event : TableRow {
			public const string TableName = "EVENT_PARAM_ST";
			const int HeaderLength = 0x1C;

			public override string DisplayName {
				get {
					return string.Format("'{0}', Type = {2}, Index = {3}{1}", Name, Unknowns.ToCommaPrefixedList(), Type, Index);
				}
			}

			public int Type { get; private set; }
			public int Index { get; private set; }

			internal Event(Table table, int index, AssetLoader loader, int next)
				: base(table, index) {
				BinaryReader reader = loader.Reader;
				long start = loader.Position;

				loader.Expect(HeaderLength);
				Unknowns.ReadInt32s(reader, 1);
				Type = reader.ReadInt32();
				Index = reader.ReadInt32();
				int offseta = reader.ReadInt32();
				int offsetb = reader.ReadInt32();
				if (offsetb != offseta + 0x10)
					loader.AddError(start, "{0} index {1} does not have a normal offset A/B", GetType().Name, index);
				loader.ExpectZeroes(4, 1);
				Name = reader.ReadStringz(EncodingShiftJis);

				loader.Position = start + offseta;
				Unknowns.ReadInt32s(reader, 4);
				Unknowns.ReadInt32s(reader, (next - (int)loader.Position) / 4);
			}
		}

		/// <summary>
		/// "MODEL_PARAM_ST" in a MapStudio ".msb" file.
		/// </summary>
		public class Model : TableRow {
			public const string TableName = "MODEL_PARAM_ST";
			const int HeaderLength = 0x20;

			public override string DisplayName {
				get {
					return string.Format("'{0}', Type = {1}, Index = {2}, Path = '{4}'{3}", Name, Type, Index, Unknowns.ToCommaPrefixedList(), Path);
				}
			}

			public int Type { get; private set; }
			public int Index { get; private set; }
			public string Path { get; private set; }

			internal Model(Table table, int index, AssetLoader loader)
				: base(table, index) {
				BinaryReader reader = loader.Reader;

				loader.Expect(HeaderLength);
				Type = reader.ReadInt32();
				Index = reader.ReadInt32();

				int contentSize = reader.ReadInt32(); // Size from start to end of Name
				Unknowns.ReadInt32s(reader, 1);
				loader.ExpectZeroes(4, 3);

				Name = reader.ReadStringz(EncodingShiftJis);
				Path = reader.ReadStringz(EncodingShiftJis);
			}
		}

		/// <summary>
		/// "PARTS_PARAM_ST" in a MapStudio ".msb" file.
		/// </summary>
		public class Parts : TableRow {
			public const string TableName = "PARTS_PARAM_ST";
			const int HeaderLength = 0x64;

			public override string DisplayName {
				get {
					return string.Format("'{0}', Type = {1}, Index = {2}{3}{4}{7}, Path = '{5}'{6}", Name, Type, Index, Position != Vector3f.Zero ? ", Position = " + Position.ToShortString() : "", Scale != Vector3f.One ? ", Scale = " + Scale.ToShortString() : "", Path, Unknowns.ToCommaPrefixedList(), Rotation != Angle3.Zero ? ", Rotation = " + Rotation.ToShortString() : "");
				}
			}

			public int Index { get; private set; }
			public string Path { get; private set; }
			public Vector3f Position { get; private set; }
			public Angle3 Rotation { get; private set; }
			public Vector3f Scale { get; private set; }
			public int Type { get; private set; }

			internal Parts(Table table, int index, AssetLoader loader, int next)
				: base(table, index) {
				BinaryReader reader = loader.Reader;
				long start = reader.BaseStream.Position;

				loader.Expect(HeaderLength);
				Type = reader.ReadInt32();
				Unknowns.ReadInt32s(reader, 1);
				Index = reader.ReadInt32();
				int pathOffset = reader.ReadInt32();
				Position = reader.ReadVector3f();
				Rotation = Angle3.Degrees(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
				Scale = reader.ReadVector3f();
				Unknowns.ReadInt32s(reader, 8);
				int offset1 = reader.ReadInt32();
				int offset2 = reader.ReadInt32();
				if (offset1 + 0x18 != offset2)
					loader.AddError(start, "Offset2 is not correct.");
				loader.ExpectZeroes(4, 1);
				Name = reader.ReadStringz(EncodingShiftJis);
				Path = reader.ReadStringz(EncodingShiftJis);

				loader.Position = start + offset1;
				Unknowns.ReadInt32s(reader, 6, "Offset1s");

				if (loader.Position > next || (next - loader.Position) / 4 > 64) {
				}

				Unknowns.ReadInt32s(reader, (int)(next - loader.Position) / 4, "Offset2s");
			}
		}

		/// <summary>
		/// "POINT_PARAM_ST" in a MapStudio ".msb" file.
		/// </summary>
		public class Point : TableRow {
			public const string TableName = "POINT_PARAM_ST";
			const int HeaderLength = 0x3C;

			public override string DisplayName {
				get {
					return string.Format("{0}, Subtype = {1}, Position = {3}, Rotation = {4}, {2}", Name, Subtype, Unknowns.ToCommaPrefixedList(), Position, Rotation);
				}
			}

			public int Subtype { get; private set; }
			public Vector3f Position { get; private set; }
			public Angle3 Rotation { get; private set; }

			internal Point(Table table, int index, AssetLoader loader)
				: base(table, index) {
				BinaryReader reader = loader.Reader;
				long start = reader.BaseStream.Position;

				loader.Expect(HeaderLength);
				loader.ExpectZeroes(4, 1); // Probably type, always zero.
				loader.Expect(index); // Zero-based index of the row with this type.
				Subtype = reader.ReadInt32();
				Position = new Vector3f(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
				Rotation = Angle3.Degrees(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
				int unknown1Offset = reader.ReadInt32(); // Offset to Unknown1 from start of row.
				int unknown2Offset = reader.ReadInt32(); // Offset to Unknown2 from start of row (Unknown1Offset + 4).
				int unknown3Offset = reader.ReadInt32(); // Offset to Unknown3 from start of row, or 0 if none (Unknown2Offset + 4).
				int unknown4Offset = reader.ReadInt32(); // Offset to Unknown3 from start of row (Unknown2Offset + 4 or Unknown3Offset + 12).
				loader.ExpectZeroes(4, 1);
				Name = reader.ReadStringz(EncodingShiftJis);

				reader.BaseStream.Position = start + unknown1Offset;
				loader.ExpectZeroes(4, 2); // Unknown1 and Unknown2
				if (unknown3Offset != 0)
					Unknowns.ReadSingles(reader, 3);
				Unknowns.ReadInt32s(reader, 1); // -1 or values like 1002000 for events.

				var errors = loader.Errors;
			}
		}
	}

	class TableArchiveFormat : AssetFormat {
		public TableArchiveFormat(Engine engine)
			: base(engine, typeof(TableArchive), canLoad: true) {
		}

		public override LoadMatchStrength LoadMatch(AssetLoader loader) {
			var reader = loader.Reader;

			if (loader.Length < 256)
				return LoadMatchStrength.None;

			int zero = reader.ReadInt32();
			int nameOffset = reader.ReadInt32();
			int rowCount = reader.ReadInt32();

			if (zero == 0 && nameOffset == 12 + rowCount * 4 && rowCount >= 16)
				return LoadMatchStrength.Medium;

			return LoadMatchStrength.None;
		}

		public override Asset Load(AssetLoader loader) {
			return new TableArchive(Manager, loader);
		}
	}
}
