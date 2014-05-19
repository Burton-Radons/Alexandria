using Glare.Assets.Controls;
using Glare.Framework;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alexandria.Engines.Sciagi.Resources {
	public class Text : ResourceData {
		readonly RichList<string> StringsMutable = new RichList<string>();

		public ReadOnlyList<string> Strings { get { return StringsMutable; } }

		List<KeyValuePair<int, string>> StringsWithId = new List<KeyValuePair<int, string>>();

		public Text(BinaryReader reader, Resource resource) : base(resource) {
			long end = reader.BaseStream.Length;

			while (reader.BaseStream.Position < end) {
				string value = reader.ReadStringz(Encoding.ASCII);

				StringsMutable.Add(value);
				StringsWithId.Add(new KeyValuePair<int, string>(StringsMutable.Count - 1, value));
			}
		}

		public override Control Browse() {
			DataGridView view = new DoubleBufferedDataGridView() {
				AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells,
				ReadOnly = true,
			};

			view.DataSource = StringsWithId;

			view.Columns.Add(new DataGridViewTextBoxColumn() {
				DataPropertyName = "Key",
				HeaderText = "Index",
			});

			view.Columns.Add(new DataGridViewTextBoxColumn() {
				AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
				DataPropertyName = "Value",
				HeaderText = "Value",
				Resizable = DataGridViewTriState.True,

				DefaultCellStyle = new DataGridViewCellStyle() {
					WrapMode = DataGridViewTriState.True,
				},
			});

			return view;
		}
	}
}
