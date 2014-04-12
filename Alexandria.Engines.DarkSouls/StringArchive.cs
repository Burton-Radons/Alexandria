using Glare.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alexandria.Engines.DarkSouls {
	public class StringArchive : Resource {
		public const int Magic1 = 0x10000;
		public const int Magic2 = 1, Magic2BE = 0x01FF0000;

		readonly ArrayBackedList<StringGroup> groups = new ArrayBackedList<StringGroup>();
		readonly ArrayBackedList<string> strings = new ArrayBackedList<string>();
		readonly Dictionary<int, string> stringsById = new Dictionary<int, string>();
		readonly ReadOnlyDictionary<int, string> stringsByIdReadOnly;

		public ReadOnlyList<StringGroup> Groups { get { return groups; } }
		public ReadOnlyList<string> Strings { get { return strings; } }
		public ReadOnlyDictionary<int, string> StringsById { get { return stringsByIdReadOnly; } }

		public ByteOrder ByteOrder { get; private set; }
		public Encoding Encoding { get; private set; }

		internal StringArchive(Manager manager, BinaryReader reader, string name, long length)
			: base(manager, name) {
			ByteOrder = ByteOrder.LittleEndian;
			stringsByIdReadOnly = new ReadOnlyDictionary<int, string>(stringsById);

			Encoding = Encoding.Unicode;
			if (reader is BigEndianBinaryReader)
				Encoding = Encoding.BigEndianUnicode;

			using (reader) {
				reader.Require(Magic1);

				int totalFileLength = reader.ReadInt32();
				if (totalFileLength != length) {
					if (totalFileLength.ReverseBytes() == length) {
						ByteOrder = ByteOrder.BigEndian;
						Encoding = Encoding.BigEndianUnicode;
					} else
						throw new InvalidDataException();
				}

				int magic2 = reader.ReadInt32();
				if (magic2 != Magic2 && magic2 != Magic2BE)
					throw new InvalidDataException();

				int groupCount = reader.ReadInt32(ByteOrder);
				int stringCount = reader.ReadInt32(ByteOrder);
				int stringOffset = reader.ReadInt32(ByteOrder);
				reader.RequireZeroes(4 * 1);

				for (int index = 0; index < groupCount; index++)
					groups.Add(new StringGroup(reader, ByteOrder));

				int[] stringOffsets = reader.ReadArrayInt32(stringCount, ByteOrder);

				for (int index = 0; index < stringCount; index++) {
					int offset = stringOffsets[index];

					if (offset == 0)
						strings.Add(null);
					else {
						reader.BaseStream.Position = offset;
						string value = reader.ReadStringz(Encoding);
						strings.Add(value);
						//stringsById[index] = value;
					}
				}

				foreach (StringGroup group in groups) {
					for (int index = 0; index < group.StringCount; index++) {
						int stringIndex = group.StringsIndex + index;
						string stringValue = strings[stringIndex];

						if (stringValue != null)
							stringsById[group.IndexStart + index] = stringValue;
					}
				}
			}
		}

		public override System.Windows.Forms.Control Browse() {
			/*var splitter = new SplitContainer() {
				Orientation = Orientation.Horizontal
			};*/

			DataGridView stringView = new DataGridView() {
				AutoGenerateColumns = false,
				AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells,
				DataSource =
					(from i in StringsById
					 select new KeyValuePair<int, string>(i.Key, i.Value.Trim())).ToArray(),
				Dock = DockStyle.Fill,
			};

			/*splitter.Panel1.Controls.Add();*/

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

			/*splitter.Panel2.Controls.Add(new DataGridView() {
				AutoGenerateColumns = true,
				DataSource = Groups,
				Dock = DockStyle.Fill,
			});*/

			return stringView;
		}
	}

	public class StringGroup {
		public int StringsIndex { get; private set; }
		public int IndexStart { get; private set; }
		public int IndexEnd { get; private set; }
		public int StringCount { get { return IndexEnd - IndexStart + 1; } }

		public StringGroup(BinaryReader reader, ByteOrder byteOrder) {
			StringsIndex = reader.ReadInt32(byteOrder);
			IndexStart = reader.ReadInt32(byteOrder);
			IndexEnd = reader.ReadInt32(byteOrder);
		}
	}

	public class StringArchiveFormat : ResourceFormat {
		public StringArchiveFormat(Engine engine)
			: base(engine, typeof(StringArchive), canLoad: true) {
		}

		public override LoadMatchStrength LoadMatch(LoadInfo context) {
			var reader = context.Reader;
			ByteOrder byteOrder = ByteOrder.LittleEndian;

			if (context.Length < 4 * 7)
				return LoadMatchStrength.None;
			int magic1 = reader.ReadInt32();
			if (magic1 != StringArchive.Magic1)
				return LoadMatchStrength.None;
			int totalFileLength = reader.ReadInt32();
			if (context.Length != totalFileLength) {
				if (totalFileLength.ReverseBytes() == context.Length)
					byteOrder = ByteOrder.BigEndian;
				else
					return LoadMatchStrength.None;
			}

			int magic2 = reader.ReadInt32(byteOrder);
			if (magic2 != StringArchive.Magic2 && magic2 != StringArchive.Magic2BE)
				return LoadMatchStrength.None;
			int groupCount = reader.ReadInt32(byteOrder);
			int stringCount = reader.ReadInt32(byteOrder);
			int stringOffset = reader.ReadInt32(byteOrder);
			int zero1 = reader.ReadInt32(byteOrder);
			if (zero1 != 0)
				return LoadMatchStrength.None;

			if (stringOffset != 28 + groupCount * 12)
				return LoadMatchStrength.None;
			return LoadMatchStrength.Strong;
		}

		public override Resource Load(LoadInfo context) {
			return new StringArchive(Manager, context.Reader, context.Name, context.Length);
		}
	}
}
