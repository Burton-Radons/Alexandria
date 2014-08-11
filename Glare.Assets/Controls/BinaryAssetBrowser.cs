using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Glare.Assets;
using System.Collections;
using Glare.Internal;

namespace Glare.Assets.Controls {
	/// <summary>
	/// A hex browser for a <see cref="BinaryAsset"/>.
	/// </summary>
	public partial class BinaryAssetBrowser : UserControl {
		readonly IList<byte> SourceData;
		readonly int SourceStart;
		readonly int SourceCount;
		readonly int SourceDisplayOffset;

		byte this[int index] {
			get {
				if (index < 0 || index > SourceCount)
					throw new ArgumentOutOfRangeException("index");
				return SourceData[SourceStart + index];
			}
		}

		DataGridView grid;

		public BinaryAssetBrowser(BinaryAsset asset) : this(asset.Data, asset.DataStart, asset.DataCount, asset.DataDisplayOffset) { }

		/// <summary>Initialise the control.</summary>
		/// <param name="data"></param>
		/// <param name="start"></param>
		/// <param name="count"></param>
		/// <param name="displayOffset"></param>
		public BinaryAssetBrowser(IList<byte> data, int start, int count, int displayOffset = 0) {
			if (data == null)
				throw new ArgumentNullException("data");
			if (start < 0 || start > data.Count)
				throw new ArgumentOutOfRangeException("start");
			if (count < 0 || start + count > data.Count)
				throw new ArgumentOutOfRangeException("count");

			SourceData = data;
			SourceStart = start;
			SourceCount = count;
			SourceDisplayOffset = displayOffset;

			InitializeComponent();

			grid = new RowView() {
				Dock = DockStyle.Fill,
			};

			splitter.Panel1.Controls.Add(grid);

			grid.CellEnter += OnGridCellEnter;

			DataGridViewCellStyle cellStyle = new DataGridViewCellStyle() {
				Font = new Font(FontFamily.GenericMonospace, 8),
			};

			DataGridViewCellStyle altCellStyle = new DataGridViewCellStyle(cellStyle) {
				BackColor = Color.Wheat,
			};

			var offsetColumn = new DataGridViewTextBoxColumn() {
				DataPropertyName = "OffsetHex",
				DefaultCellStyle = cellStyle,
				HeaderText = "Offset",
				ReadOnly = true,
				Resizable = DataGridViewTriState.False,
				Width = (int)(cellStyle.Font.Size * 9),
			};
			grid.Columns.Add(offsetColumn);

			for (int index = 0; index < 16; index++) {
				var column = new DataGridViewTextBoxColumn() {
					DataPropertyName = "Column" + index,
					DefaultCellStyle = index % 2 == 0 ? cellStyle : altCellStyle,
					HeaderText = string.Format("{0:X2}", (SourceStart + index) % 16),
					ReadOnly = true,
					Resizable = DataGridViewTriState.False,
				};

				column.Width = (int)(column.DefaultCellStyle.Font.Size * 3);
				grid.Columns.Add(column);
			}

			var textColumn = new DataGridViewTextBoxColumn() {
				DataPropertyName = "Text",
				DefaultCellStyle = cellStyle,
				HeaderText = "Text",
				ReadOnly = true,
				Resizable = DataGridViewTriState.False,
				Width = (int)(cellStyle.Font.Size * 16),
			};
			grid.Columns.Add(textColumn);

			grid.DataSource = new RowSource(this);
		}

		void OnGridCellEnter(object sender, DataGridViewCellEventArgs e) {
			if (e.ColumnIndex < 0 || e.ColumnIndex > 15)
				return;
			int offset = e.ColumnIndex + e.RowIndex * 16;
			DataOffset.Text = Format(offset + SourceDisplayOffset) + " of " + Format(SourceDisplayOffset + SourceCount);
			offset += SourceStart;

			int end = SourceStart + SourceCount;
			IList<byte> data = SourceData;

			DataByte.Text = Format(data.ReadByte(offset, end));
			DataSByte.Text = Format(data.ReadSByte(offset, end));

			DataInt16.Text = Format(data.ReadInt16(offset, end));
			DataInt16BE.Text = Format(data.ReadInt16BE(offset, end));
			DataUInt16.Text = Format(data.ReadUInt16(offset, end));
			DataUInt16BE.Text = Format(data.ReadUInt16BE(offset, end));

			DataInt32.Text = Format(data.ReadInt32(offset, end));
			DataInt32BE.Text = Format(data.ReadInt32BE(offset, end));
			DataUInt32.Text = Format(data.ReadUInt32(offset, end));
			DataUInt32BE.Text = Format(data.ReadUInt32BE(offset, end));

			DataInt64.Text = Format(data.ReadInt64(offset, end));
			DataInt64BE.Text = Format(data.ReadInt64BE(offset, end));
			DataUInt64.Text = Format(data.ReadUInt64(offset, end));
			DataUInt64BE.Text = Format(data.ReadUInt64BE(offset, end));

			DataSingle.Text = data.ReadSingle(offset, end).ToString();
			DataSingleBE.Text = data.ReadSingleBE(offset, end).ToString();

			DataDouble.Text = data.ReadDouble(offset, end).ToString();
			DataDoubleBE.Text = data.ReadDoubleBE(offset, end).ToString();
		}

		static string Format(object value) { return string.Format("{0} / {0:X}h", value); }

		class RowView : DataGridView {
			public RowView() {
				DoubleBuffered = true;
			}

			protected override void OnKeyDown(KeyEventArgs e) {
				Keys key = e.KeyCode;

				switch (key) {
					case Keys.Left:
						if (CurrentCellAddress.X > 0 || CurrentCellAddress.Y == 0)
							break;
						CurrentCell = this[15, CurrentCellAddress.Y - 1];
						e.Handled = e.SuppressKeyPress = true;
						return;

					case Keys.Right:
						if (CurrentCellAddress.X != 15 || CurrentCellAddress.Y >= RowCount - 1)
							break;
						CurrentCell = this[0, CurrentCellAddress.Y + 1];
						e.Handled = e.SuppressKeyPress = true;
						return;

					default:
						break;
				}
				base.OnKeyDown(e);
			}
		}

		class Row {
			readonly string[] Columns = new string[16];
			readonly int Offset;
			const int Pitch = 16;
			readonly BinaryAssetBrowser Resource;

			public string Column0 { get { return Columns[0]; } }
			public string Column1 { get { return Columns[1]; } }
			public string Column2 { get { return Columns[2]; } }
			public string Column3 { get { return Columns[3]; } }
			public string Column4 { get { return Columns[4]; } }
			public string Column5 { get { return Columns[5]; } }
			public string Column6 { get { return Columns[6]; } }
			public string Column7 { get { return Columns[7]; } }
			public string Column8 { get { return Columns[8]; } }
			public string Column9 { get { return Columns[9]; } }
			public string Column10 { get { return Columns[10]; } }
			public string Column11 { get { return Columns[11]; } }
			public string Column12 { get { return Columns[12]; } }
			public string Column13 { get { return Columns[13]; } }
			public string Column14 { get { return Columns[14]; } }
			public string Column15 { get { return Columns[15]; } }
			public string OffsetHex { get { return string.Format("{0:X4}:{1:X4}", ((Offset + Resource.SourceDisplayOffset) >> 16) & 0xFFFF, (Offset + Resource.SourceDisplayOffset) & 0xFFFF); } }

			public string Text {
				get {
					string result = "";
					for (int index = 0; index < Pitch && Offset + index < Resource.SourceCount; index++) {
						byte value = Resource[Offset + index];

						if (value < 32 || (value > 0x7E && value < 0xA0))
							result += ".";
						else
							result += (char)value;
					}
					return result;
				}
			}

			public Row(BinaryAssetBrowser resource, int offset) {
				Resource = resource;
				Offset = offset;

				for (int index = 0; index < Pitch; index++) {
					if (Offset + index >= Resource.SourceCount)
						Columns[index] = "  ";
					else
						Columns[index] = string.Format("{0:X2}", Resource[Offset + index]);
				}
			}
		}

		class RowSource : IList {
			const int Pitch = 16;
			readonly BinaryAssetBrowser Browser;
			readonly Row[] Rows;

			public RowSource(BinaryAssetBrowser browser) {
				Browser = browser;
				Rows = new Row[(browser.SourceCount + 15) / Pitch];
			}

			public int Add(object value) {
				throw new NotImplementedException();
			}

			public void Clear() {
				throw new NotImplementedException();
			}

			public bool Contains(object value) {
				throw new NotImplementedException();
			}

			public int IndexOf(object value) {
				throw new NotImplementedException();
			}

			public void Insert(int index, object value) {
				throw new NotImplementedException();
			}

			public bool IsFixedSize { get { return true; } }

			public bool IsReadOnly { get { return true; } }

			public void Remove(object value) {
				throw new NotImplementedException();
			}

			public void RemoveAt(int index) {
				throw new NotImplementedException();
			}

			public object this[int index] {
				get { return Rows[index] ?? (Rows[index] = new Row(Browser, index * Pitch)); }
				set { throw new NotImplementedException(); }
			}

			public void CopyTo(Array array, int index) {
				throw new NotImplementedException();
			}

			public int Count { get { return (Browser.SourceCount + 15) / 16; } }

			public bool IsSynchronized {
				get { throw new NotImplementedException(); }
			}

			public object SyncRoot {
				get { throw new NotImplementedException(); }
			}

			public IEnumerator GetEnumerator() {
				throw new NotImplementedException();
			}
		}

		private void label4_Click(object sender, EventArgs e) {

		}
	}
}
