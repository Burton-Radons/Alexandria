using Glare.Assets;
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
	/// <summary>A collection of strings.</summary>
	public class Text : ResourceData {
		readonly Codex<string> StringsMutable = new Codex<string>();

		/// <summary>Get the list of strings.</summary>
		public ReadOnlyCodex<string> Strings { get { return StringsMutable; } }

		List<KeyValuePair<int, string>> StringsWithId = new List<KeyValuePair<int, string>>();

		/// <summary>Read the text resource.</summary>
		/// <param name="loader"></param>
		internal Text(AssetLoader loader)
			: base(loader) {
			BinaryReader reader = loader.Reader;
			long end = reader.BaseStream.Length;

			while (reader.BaseStream.Position < end) {
				string value = reader.ReadStringz(Encoding.ASCII);

				StringsMutable.Add(value);
				StringsWithId.Add(new KeyValuePair<int, string>(StringsMutable.Count - 1, value));
			}
		}

		/// <summary>Create a control to browse the object.</summary>
		/// <returns></returns>
		public override Control Browse(Action<double> progressUpdateCallback = null) {
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
