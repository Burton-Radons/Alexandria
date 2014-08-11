using Glare.Assets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Alexandria.Controls {
	/// <summary>
	/// A form to manage game instances.
	/// </summary>
	public partial class GameInstanceManager : Form {
		/// <summary>Get the controlling <see cref="AlexandriaManager"/>.</summary>
		public AlexandriaManager Manager { get; set; }

		/// <summary>Initialise the controls.</summary>
		/// <param name="manager"></param>
		public GameInstanceManager(AlexandriaManager manager) {
			InitializeComponent();
			Manager = manager;
		}

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) {

		}

		private void detectGames_Click(object sender, EventArgs e) {
			List<GameInstance> instances = new List<GameInstance>();

			foreach (AlexandriaPlugin plugin in Manager.Plugins) {
			}
		}
	}
}
