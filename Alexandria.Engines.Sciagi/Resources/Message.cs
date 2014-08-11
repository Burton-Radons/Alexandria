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
using Glare.Assets;
using System.Diagnostics;

namespace Alexandria.Engines.Sciagi.Resources {
	/// <summary>
	/// This describes a vocabulary word.
	/// </summary>
	public class Message : ResourceData {
		/// <summary>
		/// Get the collection of <see cref="MessageItem"/>s.
		/// </summary>
		public ReadOnlyCodex<MessageItem> Items { get; private set; }

		/// <summary>Get the major version number based on <see cref="Version"/>, such as 5.</summary>
		public int MajorVersion { get { return (int)Version / 1000; } }

		/// <summary>Get the minor version number based on <see cref="Version"/>, such as 220.</summary>
		public int MinorVersion { get { return (int)Version % 1000; } }

		/// <summary>Get the version number.</summary>
		public MessageVersion Version { get; private set; }

		internal Message(AssetLoader loader)
			: base(loader) {
			using (BinaryReader reader = loader.Reader) {
				Version = (MessageVersion)reader.ReadInt32();

				int fileLength, addonOffset = -1;
				int count;
				Codex<MessageItem> items = new Codex<MessageItem>();
				Items = items;

				switch (MajorVersion) {
					case 3:
						fileLength = reader.ReadUInt16() + 6;
						count = reader.ReadUInt16();
						break;

					case 4:
						addonOffset = reader.ReadUInt16() + 6;
						Unknowns.ReadInt16s(reader, 1);
						count = reader.ReadUInt16();
						break;

					case 5:
						addonOffset = reader.ReadInt32() + 6;
						count = reader.ReadUInt16();
						break;

					default:
						throw new NotImplementedException("Unimplemented version " + Version);
				}

				for (int index = 0; index < count; index++) {
					MessageItem item = new MessageItem(this, index, loader);
					items.Add(item);
					AddChild(item);
				}

				foreach (MessageItem item in items)
					item.ReadText(reader);

				if (addonOffset >= 0) {
					reader.BaseStream.Position = addonOffset;
					foreach (MessageItem item in items)
						item.ReadAddOn(reader);
				}
			}
		}

		DataGridViewTextBoxColumn CreateColumn(DataGridView view, string id, int width = 60, DataGridViewCellStyle cellStyle = null) {
			DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn() {
				DataPropertyName = id,
				HeaderText = id,
				SortMode = DataGridViewColumnSortMode.Automatic,
				Resizable = DataGridViewTriState.True,
				Width = width,
				DefaultCellStyle = cellStyle,
			};

			view.Columns.Add(column);
			return column;
		}

		/// <summary>
		/// Create a control to browse the message.
		/// </summary>
		/// <returns></returns>
		public override Control Browse(Action<double> progressUpdateCallback = null) {
			DataGridView view = new DoubleBufferedDataGridView() {
				AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells,
				AutoGenerateColumns = false,
				ReadOnly = true,
			};

			view.DataSource = Items;

			DataGridViewCellStyle wrapStyle = new DataGridViewCellStyle() { WrapMode = DataGridViewTriState.True };

			CreateColumn(view, "Index");
			CreateColumn(view, "Noun");
			CreateColumn(view, "Verb");
			CreateColumn(view, "Condition");
			CreateColumn(view, "Sequence");
			CreateColumn(view, "Talker");
			CreateColumn(view, "UnknownsString", width: 100);
			CreateColumn(view, "Text", width: 400, cellStyle: wrapStyle);
			CreateColumn(view, "Comment", width: 200);

			return view;
		}
	}

	/// <summary>
	/// An item in a <see cref="Message"/>.
	/// </summary>
	public class MessageItem : Asset {
		/// <summary>An optional developer comment about the message item.</summary>
		public string Comment { get; private set; }

		/// <summary>
		/// Get the containing message.
		/// </summary>
		public Message Message { get; private set; }

		/// <summary>
		/// Get the zero-based index of this message item.
		/// </summary>
		public int Index { get; private set; }

		/// <summary>
		/// Get the noun index.
		/// </summary>
		public byte Noun { get; private set; }

		/// <summary>
		/// Get the verb index.
		/// </summary>
		public byte Verb { get; private set; }

		/// <summary>
		/// Get the condition index.
		/// </summary>
		public byte Condition { get; private set; }

		/// <summary>Get the sequence index.</summary>
		public byte Sequence { get; private set; }

		/// <summary>Get the talker index.</summary>
		public byte Talker { get; private set; }

		/// <summary>Get the text of the message.</summary>
		public string Text { get; private set; }

		/// <summary>Get a comma-separated string for the <see cref="Unknowns"/>.</summary>
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public string UnknownsString { get { return Unknowns.ToCommaSeparatedList(); } }

		readonly int Offset;

		internal MessageItem(Message message, int index, AssetLoader loader)
			: base(loader) {
			BinaryReader reader = loader.Reader;

			Message = message;
			Index = index;

			switch (message.MajorVersion) {
				case 3:
					Noun = reader.ReadByte();
					Verb = reader.ReadByte();
					Condition = reader.ReadByte();
					Sequence = reader.ReadByte();
					Talker = reader.ReadByte();
					Offset = reader.ReadUInt16();
					Unknowns.ReadBytes(reader, 3);
					break;

				case 4:
				case 5:
					Noun = reader.ReadByte();
					Verb = reader.ReadByte();
					Condition = reader.ReadByte();
					Sequence = reader.ReadByte();
					Talker = reader.ReadByte();
					Offset = reader.ReadUInt16();
					Unknowns.ReadBytes(reader, 4);
					break;

				default:
					throw new NotImplementedException();
			}
		}

		internal void ReadAddOn(BinaryReader reader) {
			switch (Message.MajorVersion) {
				case 4:
					string value = reader.ReadStringz(Encoding.ASCII);
					if (!string.IsNullOrEmpty(value))
						Comment = value;
					Unknowns.ReadBytes(reader, 6);
					break;
					
				case 5:
					Unknowns.ReadBytes(reader, 6);
					break;

				default:
					throw new NotImplementedException();
			}
		}

		internal void ReadText(BinaryReader reader) {
			Text = reader.ReadStringzAt(Offset, Encoding.ASCII);
		}

		/// <summary>Convert to a string representation of the message item.</summary>
		/// <returns></returns>
		public override string ToString() {
			return string.Format("{0}(Noun {1}, Verb {2}, Condition {3}, Sequence {4}, Talker {5}, Unknown {6}, Text \"{7}\")", GetType().Name, Noun, Verb, Condition, Sequence, Talker, Unknowns.ToCommaSeparatedList(), Text);
		}
	}

	/// <summary>
	/// Identifies a <see cref="Message"/> version number.
	/// </summary>
	public enum MessageVersion {
		/// <summary>Version 3220 (<c>0x0C94</c>).</summary>
		Version3220 = 0x0C94,

		/// <summary>Version 3340 (<c>0x0D0C</c>).</summary>
		Version3340 = 0x0D0C,

		/// <summary>Version 4321 (<c>0x10E1</c>); used in Quest for Glory IV.</summary>
		Version4321 = 0x10E1,

		/// <summary>Version 5000 (<c>0x1388</c>); used in Space Quest VI.</summary>
		Version5000 = 0x1388,
	}
}
