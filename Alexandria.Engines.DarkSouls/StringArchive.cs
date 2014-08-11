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
	/// <summary>
	/// A collection of strings indexed by ids.
	/// </summary>
	public class StringArchive : Asset {
		/// <summary>The first magic value.</summary>
		public const int Magic1 = 0x10000;

		/// <summary>The second magic value, and in big-endian form.</summary>
		public const int Magic2 = 1, Magic2BE = 0x01FF0000;

		readonly Codex<StringGroup> groups = new Codex<StringGroup>();
		readonly Codex<string> strings = new Codex<string>();
		readonly RichDictionary<int, string> stringsById = new RichDictionary<int, string>();

		/// <summary>Get the groups of strings.</summary>
		public ReadOnlyCodex<StringGroup> Groups { get { return groups; } }

		/// <summary>Get the raw collection of strings.</summary>
		public ReadOnlyCodex<string> Strings { get { return strings; } }

		/// <summary>Get the strings by their actual ids.</summary>
		public ReadOnlyObservableDictionary<int, string> StringsById { get { return stringsById; } }

		/// <summary>Get the byte order of the string archive.</summary>
		public ByteOrder ByteOrder { get; private set; }

		/// <summary>Get the encoding to use with the string archive.</summary>
		public Encoding Encoding { get; private set; }

		/// <summary>Get the strings sorted by ids.</summary>
		public KeyValuePair<int, string>[] SortedStrings {
			get {
				var list = stringsById.ToArray();
				list.Sort((a, b) => a.Key.CompareTo(b.Key));
				return list;
			}
		}

		/// <summary>Get the string archive as a comma-separated value table.</summary>
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

		/// <summary>
		/// Browse the string archive as a data grid.
		/// </summary>
		/// <returns>A data grid control.</returns>
		public override System.Windows.Forms.Control Browse(Action<double> progressUpdateCallback = null) {
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

		/// <summary>
		/// Add an item to save the string archive to disk.
		/// </summary>
		/// <param name="strip"></param>
		public override void FillContextMenu(ContextMenuStrip strip) {
			base.FillContextMenu(strip);

			strip.Items.Add(new ToolStripButton("Copy CSV to Clipboard", null, (sender, args) => {
				Clipboard.SetText(CommaSeparatedValues);
			}));
		}
	}

	/// <summary>
	/// A collection of strings with consecutive ids.
	/// </summary>
	public class StringGroup {
		/// <summary>The Id of the first string.</summary>
		public int StringsIndex { get; private set; }

		/// <summary>The first string in the list of strings in the group.</summary>
		public int IndexStart { get; private set; }

		/// <summary>The index of the last string in the list of strings in this group, inclusive.</summary>
		public int IndexEnd { get; private set; }

		/// <summary>Get the number of strings in this group.</summary>
		public int StringCount { get { return IndexEnd - IndexStart + 1; } }

		internal StringGroup(BinaryReader reader, ByteOrder byteOrder) {
			StringsIndex = reader.ReadInt32(byteOrder);
			IndexStart = reader.ReadInt32(byteOrder);
			IndexEnd = reader.ReadInt32(byteOrder);
		}
	}

	/// <summary>
	/// The <see cref="AssetFormat"/> for managing a <see cref="StringArchive"/>.
	/// </summary>
	public class StringArchiveFormat : AssetFormat {
		/// <summary>
		/// Initialise the format.
		/// </summary>
		/// <param name="engine">The engine to associate with.</param>
		public StringArchiveFormat(Engine engine)
			: base(engine, typeof(StringArchive), canLoad: true) {
		}

		/// <summary>
		/// Attempt to identifier the loader as a string archive.
		/// </summary>
		/// <param name="loader">The loader to match.</param>
		/// <returns>The strength of the match.</returns>
		public override LoadMatchStrength LoadMatch(AssetLoader loader) {
			var reader = loader.Reader;
			ByteOrder byteOrder = ByteOrder.LittleEndian;

			if (loader.Length < 4 * 7)
				return LoadMatchStrength.None;
			int magic1 = reader.ReadInt32();
			if (magic1 != StringArchive.Magic1)
				return LoadMatchStrength.None;
			int totalFileLength = reader.ReadInt32();
			if (loader.Length != totalFileLength) {
				if (totalFileLength.ReverseBytes() == loader.Length)
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

		/// <summary>
		/// Load the string archive.
		/// </summary>
		/// <param name="loader">The loader to use.</param>
		/// <returns>The string archive.</returns>
		public override Asset Load(AssetLoader loader) {
			return new StringArchive(Manager, loader.Reader, loader.Name, loader.Length);
		}
	}
}
