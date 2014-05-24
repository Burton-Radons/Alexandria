using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glare.Internal;
using Glare.Framework;
using System.Windows.Forms;
using Glare.Assets.Controls;

namespace Alexandria.Engines.Sciagi.Resources {
	public class Message : ResourceData {
		public ReadOnlyCodex<MessageItem> Items { get; private set; }

		public int Version { get; private set; }

		public Message(BinaryReader reader, Resource resource)
			: base(resource) {
			using (reader) {
				Version = reader.ReadInt32();

				int fileLength;
				int count;
				Codex<MessageItem> items = new Codex<MessageItem>();
				Items = items;

				switch (Version) {
					case 0xC94:
					case 0xD0C:
						fileLength = reader.ReadUInt16() + 6;
						count = reader.ReadUInt16();
						break;

					default:
						throw new NotImplementedException("Unimplemented version " + Version);
				}

				for (int index = 0; index < count; index++)
					items.Add(new MessageItem(this, index, reader));
				foreach (MessageItem item in items)
					item.ReadText(reader);
			}
		}

		DataGridViewTextBoxColumn CreateColumn(DataGridView view, string id) {
			DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn() {
				DataPropertyName = id,
				HeaderText = id,
				SortMode = DataGridViewColumnSortMode.Automatic,
				Resizable = DataGridViewTriState.True,
				Width = 40,
			};

			view.Columns.Add(column);
			return column;
		}

		public override Control Browse() {
			DataGridView view = new DoubleBufferedDataGridView() {
				AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells,
				AutoGenerateColumns = false,
				ReadOnly = true,
			};

			view.DataSource = Items;

			CreateColumn(view, "Index");
			CreateColumn(view, "Noun");
			CreateColumn(view, "Verb");
			CreateColumn(view, "Condition");
			CreateColumn(view, "Sequence");
			CreateColumn(view, "Talker");
			CreateColumn(view, "Unknown1");
			CreateColumn(view, "Unknown2");
			CreateColumn(view, "Unknown3");
			var textColumn = CreateColumn(view, "Text");
			textColumn.DefaultCellStyle = new DataGridViewCellStyle() { WrapMode = DataGridViewTriState.True };
			textColumn.Width = 400;

			return view;
		}
	}

	public class MessageItem {
		public Message Message { get; private set; }

		public int Index { get; private set; }

		public byte Noun { get; private set; }
		public byte Verb { get; private set; }
		public byte Condition { get; private set; }
		public byte Sequence { get; private set; }
		public byte Talker { get; private set; }
		public byte Unknown1 { get; private set; }
		public byte Unknown2 { get; private set; }
		public byte Unknown3 { get; private set; }

		public string Text { get; private set; }

		readonly int Offset;

		public MessageItem(Message message, int index, BinaryReader reader) {
			Message = message;
			Index = index;

			switch (message.Version) {
				case 0xC94:
				case 0xD0C:
					Noun = reader.ReadByte();
					Verb = reader.ReadByte();
					Condition = reader.ReadByte();
					Sequence = reader.ReadByte();
					Talker = reader.ReadByte();
					Offset = reader.ReadUInt16();
					Unknown1 = reader.ReadByte();
					Unknown2 = reader.ReadByte();
					Unknown3 = reader.ReadByte();
					break;

				default:
					throw new NotImplementedException();
			}
		}

		internal void ReadText(BinaryReader reader) {
			Text = reader.ReadStringzAt(Offset, Encoding.ASCII);
		}

		public override string ToString() {
			return string.Format("{0}(Noun {1}, Verb {2}, Condition {3}, Sequence {4}, Talker {5}, Unknown {6}/{7}/{8}, Text \"{9}\")", GetType().Name, Noun, Verb, Condition, Sequence, Talker, Unknown1, Unknown2, Unknown3, Text);
		}
	}
}
