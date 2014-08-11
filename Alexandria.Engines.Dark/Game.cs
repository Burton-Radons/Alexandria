using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.Dark {
	/// <summary>Base class for a Dark Engine game.</summary>
	public abstract class Game : Alexandria.Game {
		internal Game(Engine engine) : base(engine, null) { }
	}

	/// <summary>Game for Thief: The Dark Project.</summary>
	[Guid("9C28C151-C3C9-4E97-B436-83721714FE4D")]
	public class Thief1Game : Game {
		internal Thief1Game(Engine engine) : base(engine) { }

		public override GameInstance MatchGame(string path, Glare.Assets.FileManager manager) {
			return base.MatchGame(path, manager);
		}
	}

	/// <summary>Game for Thief II: The Metal Age</summary>
	[Guid("3742E84C-BA51-4666-8389-066844E88476")]
	public class Thief2Game : Game {
		internal Thief2Game(Engine engine) : base(engine) { }
		//HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\thief2.exe\Path = "D:\Games\Thief 2"
		//HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\App Paths\thief2.exe\Path = "D:\Games\Thief 2"
	}

	/// <summary>Game for System Shock 2</summary>
	[Guid("A2C18CAE-D97D-47FD-AE5C-B38BB74FDBB7")]
	public class SystemShock2Game : Game {
		internal SystemShock2Game(Engine engine) : base(engine) { }
	}
}
