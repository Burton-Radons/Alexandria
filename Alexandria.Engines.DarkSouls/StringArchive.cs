using Glare.Assets;
using Glare.Framework;
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
	public class StringArchive : Asset {
		public const int Magic1 = 0x10000;
		public const int Magic2 = 1, Magic2BE = 0x01FF0000;

		readonly RichList<StringGroup> groups = new RichList<StringGroup>();
		readonly RichList<string> strings = new RichList<string>();
		readonly RichDictionary<int, string> stringsById = new RichDictionary<int, string>();

		public ReadOnlyList<StringGroup> Groups { get { return groups; } }
		public ReadOnlyList<string> Strings { get { return strings; } }
		public ReadOnlyObservableDictionary<int, string> StringsById { get { return stringsById; } }

		public ByteOrder ByteOrder { get; private set; }
		public Encoding Encoding { get; private set; }

		public KeyValuePair<int, string>[] SortedStrings {
			get {
				var list = stringsById.ToArray();
				list.Sort((a, b) => a.Key.CompareTo(b.Key));
				return list;
			}
		}

		public string CommaSeparatedValues {
			get {
				StringBuilder builder = new StringBuilder();

				builder.Append("\"Id\",\"Value\"\r\n");
				foreach (var item in SortedStrings)
					builder.AppendFormat("\"{0}\",\"{1}\"\r\n", item.Key, item.Value.Replace("\"", "\"\""));

				return builder.ToString();
			}
		}

		internal StringArchive(AssetManager manager, BinaryReader reader, string name, long length)
			: base(manager, name) {
			ByteOrder = ByteOrder.LittleEndian;

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
					}
				}

				foreach (StringGroup group in groups) {
					for (int index = 0; index < group.StringCount; index++) {
						int stringIndex = group.StringsIndex + index;
						string stringValue = strings[stringIndex];
						int realIndex = group.IndexStart + index;

						if (stringValue != null)
							stringsById[realIndex] = stringValue;
					}
				}
			}
		}

		public override System.Windows.Forms.Control Browse() {
			DataGridView stringView = new DataGridView() {
				AutoGenerateColumns = false,
				AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells,
				DataSource =
					(from i in StringsById
					 select new KeyValuePair<int, string>(i.Key, (i.Value ?? "").Trim())).ToArray(),
				Dock = DockStyle.Fill,
			};

			stringView.Columns.Add(new DataGridViewTextBoxColumn() {
				DataPropertyName = "Key",
				HeaderText = "Index",
				ReadOnly = true,
			});

			stringView.Columns.Add(new DataGridViewTextBoxColumn() {
				AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
				DataPropertyName = "Value",
				DefaultCellStyle = new DataGridViewCellStyle() {
					WrapMode = DataGridViewTriState.True,
				},
				HeaderText = "Value",
			});

			return stringView;
		}

		public override void FillContextMenu(ContextMenuStrip strip) {
			base.FillContextMenu(strip);

			strip.Items.Add(new ToolStripButton("Copy CSV to Clipboard", null, (sender, args) => {
				Clipboard.SetText(CommaSeparatedValues);
			}));
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

	public class StringArchiveFormat : AssetFormat {
		public StringArchiveFormat(Engine engine)
			: base(engine, typeof(StringArchive), canLoad: true) {
		}

		public override LoadMatchStrength LoadMatch(AssetLoader context) {
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

		public override Asset Load(AssetLoader context) {
			return new StringArchive(Manager, context.Reader, context.Name, context.Length);
		}
	}
}
