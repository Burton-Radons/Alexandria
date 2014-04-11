using Alexandria.Resources;
using Glare;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glare.Internal;
using System.Resources;

namespace Alexandria.Engines.DarkSouls {
	public class TableArchive : Folder {
		public TableArchive(Manager manager, BinaryReader reader, string name)
			: base(manager, name) {
			long length = reader.BaseStream.Position;

			while (reader.BaseStream.Position < length) {
				new Table(this, reader);
			}
		}
	}

	public class Table : Folder {
		public Table(TableArchive archive, BinaryReader reader)
			: base(archive, "Dark Souls Table") {
			reader.RequireZeroes(4);
			Name = reader.ReadStringzAtUInt32(Encoding.ASCII);
			int count = reader.ReadInt32();
			int[] offsets = new int[count];

			for (int index = 0; index < count; index++)
				offsets[index] = reader.ReadInt32();

			for (int index = 0; index < count - 1; index++) {
				reader.BaseStream.Position = offsets[index];
				TableRow.ReadRow(this, index, reader);
			}

			reader.BaseStream.Position = offsets[count - 1];
		}
	}

	public abstract class TableRow : Resource {
		public TableRow(Table table, int index)
			: base(table, table.Name + " row " + index) {
		}

		public static TableRow ReadRow(Table table, int index, BinaryReader reader) {
			switch (table.Name) {
				case ModelParameterRow.TableName: return new ModelParameterRow(table, index, reader);
				default:
					throw new NotImplementedException("Table row type " + table.Name + " is not implemented.");
			}
		}

		/// <summary>
		/// "MODEL_PARAM_ST" in a MapStudio ".msb" file.
		/// </summary>
		public class ModelParameterRow : TableRow {
			public const string TableName = "MODEL_PARAM_ST";
			const int HeaderLength = 0x20;

			public int Type { get; private set; }
			public int Index { get; private set; }

			public ModelParameterRow(Table table, int index, BinaryReader reader)
				: base(table, index) {
				reader.Require(HeaderLength);
				Unknowns.ReadInt32s(reader, 1); // Seems to be unique indices, perhaps to some reference point or object.
				Type = reader.ReadInt32();
				Index = reader.ReadInt32();

				int contentSize = reader.ReadInt32();
				int rowSize = reader.ReadInt32();
				reader.RequireZeroes(4 * 1);

				Name = reader.ReadStringz(EncodingShiftJis);
				reader.RequireZeroes(2);

				Unknowns.ReadInt32s(reader, 1); // Some kind of index as well, increasing and sometimes shared with one another.
				Unknowns.ReadInt32s(reader, 1); // Another index, increasing, rarely shared, doesn't start at 0.
				reader.RequireZeroes(4 * 2);

				Unknowns.ReadInt32s(reader, 1); // 7s and 8s.
				// Padding to multiple of 8?
			}
		}

		/// <summary>
		/// "POINT_PARAM_ST" in a MapStudio ".msb" file.
		/// </summary>
		public class PointParameterRow : TableRow {
			public const string TableName = "POINT_PARAM_ST";
			const int HeaderLength = 0x3C;

			public int Subtype { get; private set; }
			public Vector3f Position { get; private set; }
			public Angle RotationX { get; private set; }
			public Angle RotationY { get; private set; }

			public PointParameterRow(Table table, int index, BinaryReader reader)
				: base(table, index) {
				long start = reader.BaseStream.Position;

				reader.Require(HeaderLength);
				reader.RequireZeroes(4); // Probably type, always zero.
				reader.Require(index); // Zero-based index of the row with this type.
				Subtype = reader.ReadInt32();
				Position = new Vector3f(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
				RotationX = Angle.Degrees(reader.ReadSingle());
				RotationY = Angle.Degrees(reader.ReadSingle());
				reader.RequireZeroes(4 * 1);
				int unknown1Offset = reader.ReadInt32(); // Offset to Unknown1 from start of row.
				int unknown2Offset = reader.ReadInt32(); // Offset to Unknown2 from start of row.
				reader.RequireZeroes(4 * 1);
				int unknown3Offset = reader.ReadInt32(); // Offset to Unknown3 from start of row.
				reader.RequireZeroes(4 * 1);
				Name = reader.ReadStringz(EncodingShiftJis);

				reader.BaseStream.Position = start + unknown1Offset;
				reader.RequireZeroes(4 * 2); // Unknown1 and Unknown2
				Unknowns.ReadInt32s(reader, 1); // -1 or values like 1002000 for events.
			}
		}
	}

	class TableArchiveFormat : ResourceFormat {
		public TableArchiveFormat(Engine engine)
			: base(engine, typeof(TableArchive), canLoad: true) {
		}

		public override LoadMatchStrength LoadMatch(LoadInfo context) {
			return LoadMatchStrength.None;
			/*if (reader.BaseStream.Length < 16)
				return LoaderMatchLevel.None;

			int zero = reader.ReadInt32();
			int nameOffset = reader.ReadInt32();
			int rowCount = reader.ReadInt32();

			if (zero != 0 || nameOffset < 16 || rowCount < 1)
				return LoaderMatchLevel.None;
			return LoaderMatchLevel.Medium;*/
		}

		public override Resource Load(LoadInfo context) {
			return new TableArchive(Manager, context.Reader, context.Name);
		}
	}
}
