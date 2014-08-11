using Alexandria.Controls;
using Glare.Assets;
using Glare.Graphics.Loaders.Fbx;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alexandria.Browser {
	class Program {
		[STAThread]
		static void Main(string[] args) {
			//new FbxReader(new BinaryReader(File.OpenRead(@"D:\fuck.fbx")));
		
			AlexandriaManager manager = new AlexandriaManager();
			manager.LoadPlugins();

			var window = new MainWindow(manager);
			window.Show();
			window.FormClosed += (sender, e) => { System.Environment.Exit(0); };
			System.Windows.Forms.Application.Run();
		}
	}
}
