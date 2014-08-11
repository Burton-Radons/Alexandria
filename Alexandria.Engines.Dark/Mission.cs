using Glare.Assets;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Alexandria.Engines.Dark {
	public class Mission : FolderAsset {
		internal Mission(AssetLoader loader)
			: base(loader) {
		}
	}

	public class MissionTable : Asset {
		readonly int Offset, DataLength;

		byte[] RawData;

		public MissionTableFormat Format { get; private set; }

		public MissionTableHeader Header { get; private set; }

		public int Index { get; private set; }
		
		public new Mission Parent { get { return (Mission)Parent; } }

		internal MissionTable(Mission mission, int index, BinaryReader reader) : base(mission, "") {
			Index = index;
			Name = reader.ReadStringz(12, Encoding.ASCII);
			Offset = reader.ReadInt32();
			DataLength = reader.ReadInt32();
			Format = MissionTableFormat.KnownFormats.TryGetValue(Name, null);
		}

		internal void Read(BinaryReader reader) {
			reader.BaseStream.Position = Offset + 12;
			Unknowns.ReadInt32s(reader, 1);
			Unknowns.ReadInt16s(reader, 2);
			Unknowns.ReadInt32s(reader, 1);

			if (Format != null) {
				int length = DataLength;

				if (Format.Header != null) {
					Header = new MissionTableHeader(this, Format.Header, reader);
					length -= Format.Header.Size;
				}

				if (length % Format.Row.Size != 0)
					throw new InvalidDataException();
				int count = length / Format.Row.Size;
				for (int index = 0; index < count; index++)
					new MissionRow(this, Format.Row, reader);
			}
		}
	}

	public class MissionTableFormat {
		internal static readonly Dictionary<string, MissionTableFormat> KnownFormats = new Dictionary<string, MissionTableFormat>() {
			{ "L$MetaProp", "short;short;int;int;short" },
			{ "LD$StimSens", "int/int;int;int;int;int;int" },
			{ "P$PhysType", "short;short;int" },
		};

		public MissionTableType Header { get; private set; }
		public MissionTableType Row { get; private set; }

		internal MissionTableFormat(string code) {
			string[] parts = code.Split('/');

			if (parts.Length == 2) {
				Header = new MissionTableType(parts[0]);
				Row = new MissionTableType(parts[1]);
			} else
				Row = new MissionTableType(parts[0]);
		}

		public static implicit operator MissionTableFormat(string code) { return new MissionTableFormat(code); }
	}

	public class MissionTableType {
		public List<MissionTableColumn> Columns { get; private set; }

		public int Size { get; private set; }

		internal MissionTableType(string code) {
			string[] parts = code.Split(';');

			Columns = new List<MissionTableColumn>();
			for (int index = 0; index < parts.Length; index++) {
				MissionTableColumn element = new MissionTableColumn(index, parts[index]);
				Size += element.Size;
			}
		}
	}

	public class MissionTableColumn {
		static readonly Dictionary<string, Type> Types = new Dictionary<string, Type>() {
				{ "short", typeof(short) },
				{ "int", typeof(int) },
				{ "ushort", typeof(ushort) },
				{ "uint", typeof(uint) },
			};

		public int Index { get; private set; }

		public Type Type { get; private set; }

		public int Size { get { return Marshal.SizeOf(Type); } }

		internal MissionTableColumn(int index, string code) {
			string[] parts = code.Split(',');

			Type = Types[parts[0]];
			if (parts.Length > 1)
				throw new NotImplementedException();
		}
	}

	public abstract class MissionStructure : Asset {
		public Dictionary<MissionTableColumn, object> Elements { get; private set; }

		public MissionTableType Structure { get; private set; }

		public MissionTable Table { get; private set; }

		internal MissionStructure(MissionTable table, MissionTableType structure, BinaryReader reader)
			: base(table.Manager, "") {
			Elements = new Dictionary<MissionTableColumn, object>();
			Structure = structure;
			Table = table;

			foreach (MissionTableColumn column in structure.Columns) {
				object value;

				switch (column.Type.Name) {
					case "Int16": value = reader.ReadInt16(); break;
					case "Int32": value = reader.ReadInt32(); break;
					case "Single": value = reader.ReadSingle(); break;
					case "UInt16": value = reader.ReadUInt16(); break;
					case "UInt32": value = reader.ReadUInt32(); break;
					default: throw new NotImplementedException();
				}

				Elements[column] = value;
			}
		}
	}

	public class MissionTableHeader : MissionStructure {
		internal MissionTableHeader(MissionTable table, MissionTableType structure, BinaryReader reader) : base(table, structure, reader) { }
	}

	public class MissionRow : MissionStructure {
		internal MissionRow(MissionTable table, MissionTableType structure, BinaryReader reader)
			: base(table, structure, reader) {
			Parent = table;
		}
	}
}
