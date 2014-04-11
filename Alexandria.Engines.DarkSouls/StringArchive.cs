using Glare.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alexandria.Engines.DarkSouls {
	public class StringArchive : Resource {
		public const int Magic1 = 0x10000;
		public const int Magic2 = 1;

		readonly ArrayBackedList<StringGroup> groups = new ArrayBackedList<StringGroup>();
		readonly ArrayBackedList<string> strings = new ArrayBackedList<string>();
		readonly Dictionary<int, string> stringsById = new Dictionary<int, string>();
		readonly ReadOnlyDictionary<int, string> stringsByIdReadOnly;

		public ReadOnlyList<StringGroup> Groups { get { return groups; } }
		public ReadOnlyList<string> Strings { get { return strings; } }
		public ReadOnlyDictionary<int, string> StringsById { get { return stringsByIdReadOnly; } }

		public StringArchive(Manager manager, BinaryReader reader, string name)
			: base(manager, name) {
			stringsByIdReadOnly = new ReadOnlyDictionary<int, string>(stringsById);

			using (reader) {
				reader.Require(Magic1);
				int totalFileLength = reader.ReadInt32();
				reader.Require(Magic2);
				int groupCount = reader.ReadInt32();
				int stringCount = reader.ReadInt32();
				int stringOffset = reader.ReadInt32();
				reader.RequireZeroes(4 * 1);

				for (int index = 0; index < groupCount; index++)
					groups.Add(new StringGroup(reader));

				int[] stringOffsets = reader.ReadArrayInt32(stringCount);

				for (int index = 0; index < stringCount; index++) {
					int offset = stringOffsets[index];

					if (offset == 0)
						strings.Add(null);
					else {
						reader.BaseStream.Position = offset;
						string value = reader.ReadStringz(Encoding.Unicode);
						strings.Add(value);
						stringsById[index] = value;
					}
				}
			}
		}

		public override System.Windows.Forms.Control Browse() {
			var splitter = new SplitContainer() {
				Orientation = Orientation.Horizontal
			};

			DataGridView stringView;

			splitter.Panel1.Controls.Add(stringView = new DataGridView() {
				AutoGenerateColumns = false,
				AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells,
				DataSource = 
					(from i in StringsById
						select new KeyValuePair<int, string>(i.Key, i.Value.Trim())).ToArray(),
				Dock = DockStyle.Fill,
			});

			stringView.Columns.Add(new DataGridViewTextBoxColumn() {
				DataPropertyName = "Key",

				HeaderText = "Index",

			});

			stringView.Columns.Add(new DataGridViewTextBoxColumn() {
				AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
				DataPropertyName = "Value",
				DefaultCellStyle = new DataGridViewCellStyle() {
					WrapMode = DataGridViewTriState.True,
				},
				HeaderText = "Value",
			});

			splitter.Panel2.Controls.Add(new DataGridView() {
				AutoGenerateColumns = true,
				DataSource = Groups,
				Dock = DockStyle.Fill,
			});

			return splitter;
		}
	}

	public class StringGroup {
		public int Index1 { get; private set; }
		public int Index2 { get; private set; }
		public int Index3 { get; private set; }
		public int IndexSpan { get { return Index3 - Index2; } }

		public StringGroup(BinaryReader reader) {
			Index1 = reader.ReadInt32();
			Index2 = reader.ReadInt32();
			Index3 = reader.ReadInt32();
		}
	}

	public class StringArchiveLoader : Loader {
		public StringArchiveLoader(Engine engine)
			: base(engine) {
		}

		public override LoaderMatchLevel Match(System.IO.BinaryReader reader, string name, LoaderFileOpener opener, Resource context) {
			long length = reader.BaseStream.Length;

			if (length < 4 * 7)
				return LoaderMatchLevel.None;
			int magic1 = reader.ReadInt32();
			if (magic1 != StringArchive.Magic1)
				return LoaderMatchLevel.None;
			int totalFileLength = reader.ReadInt32();
			if (length != totalFileLength)
				return LoaderMatchLevel.None;
			int magic2 = reader.ReadInt32();
			if (magic2 != StringArchive.Magic2)
				return LoaderMatchLevel.None;
			int groupCount = reader.ReadInt32();
			int stringCount = reader.ReadInt32();
			int stringOffset = reader.ReadInt32();
			int zero1 = reader.ReadInt32(), zero2 = reader.ReadInt32();
			if (zero1 != 0 || zero2 != 0)
				return LoaderMatchLevel.None;

			if (stringOffset != 28 + groupCount * 12)
				return LoaderMatchLevel.None;
			return LoaderMatchLevel.Strong;
		}

		public override Resource Load(System.IO.BinaryReader reader, string name, LoaderFileOpener opener, Resource context) {
			return new StringArchive(Manager, reader, name);
		}
	}
}
