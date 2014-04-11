using Alexandria.Controls;
using Glare.Graphics.Loaders.Fbx;
using Moki.Compiler;
using Moki.Compilers.CSharp;
using Moki.Syntax;
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
		
			/*MessageHandler messages = new MessageHandler();
			Source source = new Source("string", "int Floop;");
			Lexer lexer = new CSharpLexer(source, messages);
			Parser parser = new CSharpParser(lexer);

			Statement statement = parser.ParseStatement();
			Console.WriteLine(statement != null ? statement.ToString() : "Null returned");

			Console.Write("Press any key...");
			Console.ReadKey();*/

			Manager manager = new Manager();
			manager.LoadPlugins();

			var window = new MainWindow(manager);
			window.Show();
			window.FormClosed += (sender, e) => { System.Environment.Exit(0); };
			System.Windows.Forms.Application.Run();
		}
	}
}
