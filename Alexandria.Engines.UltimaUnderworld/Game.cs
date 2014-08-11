using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.UltimaUnderworld {
	public abstract class Game : Alexandria.Game {
		/// <summary>Get the name of the executable file, such as "uw.exe", "uw2.exe", or "sshock.exe".</summary>
		public string ExecutableName { get; private set; }

		public override Type StateType { get { return typeof(State); } }

		public GameType Type { get; private set; }

		internal Game(Engine engine, GameType type, string executableName)
			: base(engine, null) {
			Type = type;
			ExecutableName = executableName;
		}
	}

	public class GameUltimaUnderworld : Game {
		internal GameUltimaUnderworld(Engine engine) : base(engine, GameType.UltimaUnderworld, "uw.exe") { }
	}

	public class GameUltimaUnderworld2 : Game {
		internal GameUltimaUnderworld2(Engine engine) : base(engine, GameType.UltimaUnderworld2, "uw2.exe") { }
	}

	public class GameSystemShock : Game {
		internal GameSystemShock(Engine engine) : base(engine, GameType.SystemShock, "sshock.exe") { }
	}

	public enum GameType {
		UltimaUnderworld,
		UltimaUnderworld2,
		SystemShock,
	}
}
