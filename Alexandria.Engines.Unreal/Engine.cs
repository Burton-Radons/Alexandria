using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Globalization;

namespace Alexandria.Engines.Unreal {
	public class Engine : Alexandria.Engine {
		public Engine(Plugin plugin)
			: base(plugin) {
			AddGame(AlphaProtocol = new UnrealGame(this, "Alpha Protocol", @"Obsidian\Alpha Protocol", "InstallPath", ue3IniFile, 34010));
			AddGame(BatmanArkhamAsylum = new UnrealGame(this, "Batman: Arkham Asylum", @"RocksteadyLtd\Batman Arkham Asylum GOTY", "Install Directory", ue3IniFile, 35140));
			AddGame(Borderlands = new UnrealGame(this, "Borderlands", @"Gearbox Software\Borderlands", "InstallFolder", "../" + ue3IniFile, 8980));
			AddGame(DeusEx = new UnrealGame(this, "Deus Ex", @"Unreal Technology\Installed Apps\Deus Ex", "Folder", "System/DeusEx.ini", 6910));
			AddGame(DeusExInvisibleWar = new UnrealGame(this, "Deus Ex: Invisible War", @"Ion Storm\Deus Ex - Invisible War", "ION_ROOT_PC", "System/Default.ini", 6920));
			AddGame(Dishonored = new UnrealGame(this, "Dishonored", null, null, ue3IniFile, 205100));
			AddGame(MassEffect1 = new UnrealGame(this, "Mass Effect", null, null, ue3IniFile, null));
			AddGame(MassEffect2 = new UnrealGame(this, "Mass Effect 2", null, null, ue3IniFile, null));
			AddGame(MassEffect3 = new UnrealGame(this, "Mass Effect 3", @"BioWare\Mass Effect 3", null, ue3IniFile, null));
			AddGame(TheLastRemnant = new UnrealGame(this, "The Last Remnant", null, null, ue3IniFile, 23310));
			AddGame(ThiefDeadlyShadows = new UnrealGame(this, "Thief: Deadly Shadows", @"Ion Storm\Thief - Deadly Shadows", "ION_ROOT", "System/DEFAULT.INI", 6980));
			AddGame(Unreal2TheAwakening = new UnrealGame(this, "Unreal II: The Awakening", null, null, ue2IniFile, 13200));
			AddGame(UnrealGold = new UnrealGame(this, "Unreal Gold", @"Unreal Technology\Installed Apps\Unreal Gold", "Folder", ue2IniFile, 13250));
			AddGame(UnrealTournament = new UnrealGame(this, "Unreal Tournament", @"Unreal Technology\Installed Apps\UnrealTournament", "Folder", ue2IniFile, 13240));
			AddGame(UnrealTournament2004 = new UnrealGame(this, "Unreal Tournament 2004", @"Unreal Technology\Installed Apps\UT2004", "Folder", ue2IniFile, 13230));
			AddGame(UnrealTournament3 = new UnrealGame(this, "Unreal Tournament 3", null, null, ue3IniFile, 13210));
		}

		const string ue2IniFile = "System/Default.ini";
		const string ue3IniFile = "Engine/Config/BaseEngine.ini";

		static readonly CultureInfo Czech = CultureInfo.GetCultureInfo("cs");
		static readonly CultureInfo English = CultureInfo.GetCultureInfo("en");
		static readonly CultureInfo French = CultureInfo.GetCultureInfo("fr");
		static readonly CultureInfo German = CultureInfo.GetCultureInfo("de");
		static readonly CultureInfo Hungarian = CultureInfo.GetCultureInfo("hu");
		static readonly CultureInfo Italian = CultureInfo.GetCultureInfo("it");
		static readonly CultureInfo Japanese = CultureInfo.GetCultureInfo("ja");
		static readonly CultureInfo Polish = CultureInfo.GetCultureInfo("pl");
		static readonly CultureInfo Russian = CultureInfo.GetCultureInfo("ru");
		static readonly CultureInfo SpanishSpain = CultureInfo.GetCultureInfo("es-ES");

		public readonly UnrealGame AlphaProtocol;
		public readonly UnrealGame BatmanArkhamAsylum;
		public readonly UnrealGame Borderlands;
		public readonly UnrealGame DeusEx;
		public UnrealGame DeusEx1 { get { return DeusEx; } }
		public UnrealGame DeusEx2 { get { return DeusExInvisibleWar; } }
		public readonly UnrealGame DeusExInvisibleWar;
		public readonly UnrealGame Dishonored;
		public readonly UnrealGame MassEffect1;
		public readonly UnrealGame MassEffect2;
		public readonly UnrealGame MassEffect3;
		public readonly UnrealGame TheLastRemnant;
		public readonly UnrealGame ThiefDeadlyShadows;
		public UnrealGame Thief3 { get { return ThiefDeadlyShadows; } }
		public readonly UnrealGame Unreal2TheAwakening;
		public UnrealGame Unreal2 { get { return Unreal2TheAwakening; } }
		public readonly UnrealGame UnrealGold;
		public readonly UnrealGame UnrealTournament;
		public readonly UnrealGame UnrealTournament2004;
		public readonly UnrealGame UnrealTournament3;
	}

	public abstract class UnrealBaseGame : Game {
		public UnrealBaseGame(Engine engine, string name, string iniFileName)
			: base(engine) {
			Name = name;
			IniFileName = iniFileName;
		}

		#region Properties

		/// <summary>
		/// The path within the installation directory to the .ini filename for the main game.
		/// </summary>
		public string IniFileName { get; protected set; }

		#endregion Properties

		/// <summary>
		/// Search the registry locations for this game, returning the path or null if not found.
		/// </summary>
		/// <returns></returns>
		protected abstract string SearchRegistry();

		public override void Detect(GameInstanceList instances) {
			base.Detect(instances);
			var path = SearchRegistry();
			if (path != null && Directory.Exists(path))
				instances.Add(new GameInstance(this, path));
		}
	}

	/// <summary>
	/// An Unreal 1 or 2 game.
	/// </summary>
	public class UnrealGame : UnrealBaseGame {
		public UnrealGame(Engine engine, string name, string registryPath, string registryFolderName, string iniFileName, int? steamApplicationId)
			: base(engine, name, iniFileName) {
			RegistryPath = registryPath;
			RegistryFolderName = registryFolderName;
			SteamApplicationId = steamApplicationId;
		}

		/// <summary>
		/// The location of the registry keys for Unreal products under the LocalMachine registry.
		/// </summary>
		public const string RegistryBase = @"SOFTWARE\";

		/// <summary>
		/// The location of the registry keys for Unreal products under the 32-bit abstraction layer, under the LocalMachine registry.
		/// </summary>
		public const string RegistryBase32 = @"SOFTWARE\Wow6432Node\";

		/// <summary>
		/// The location of Steam application keys under <see cref="RegistryBase"/> or <see cref="RegistryBase32"/>.
		/// </summary>
		public const string SteamBase = @"Microsoft\Windows\CurrentVersion\Uninstall\Steam App ";

		public const string SteamFolderKeyName = "InstallLocation";

		/// <summary>
		/// The path within the LocalMachine registry for the game, relative to both <see cref="RegistryBase32"/> and <see cref="RegistryBase"/>.
		/// </summary>
		public string RegistryPath { get; protected set; }

		/// <summary>
		/// Get the name of the folder registry key within the <see cref="RegistryPath"/> for this game.
		/// </summary>
		public string RegistryFolderName { get; protected set; }

		/// <summary>
		/// Get the identifier of this game on Steam, or <c>null</c> if it's not on Steam.
		/// </summary>
		public int? SteamApplicationId { get; protected set; }

		string GetRegistryValue(RegistryKey baseKey, string path, string name) {
			var key = baseKey.OpenSubKey(path);
			if (key == null)
				return null;
			return key.GetValue(name, null) as string;
		}

		string GetRegistrySoftwareValue(RegistryKey baseKey, string subpath, string name) {
			return GetRegistryValue(baseKey, RegistryBase + subpath, name) ??
				GetRegistryValue(baseKey, RegistryBase32 + subpath, name);
		}

		string GetSoftwareRegistryValue(string subpath, string folderKeyName) {
			return (string.IsNullOrEmpty(subpath) ? null :
					GetRegistrySoftwareValue(Registry.LocalMachine, subpath, folderKeyName)
					?? GetRegistrySoftwareValue(Registry.CurrentUser, subpath, folderKeyName))
				?? (SteamApplicationId.HasValue ?
					GetRegistrySoftwareValue(Registry.LocalMachine, SteamBase + SteamApplicationId.Value, SteamFolderKeyName) : null);
		}

		protected override string SearchRegistry() {
			return GetSoftwareRegistryValue(RegistryPath, RegistryFolderName);
		}

		/*public override TabItem Browse(GameInstance instance) {
			var state = new State2(instance.Path, IniFileName);
			var packages = state.GetPackageFilenames();
			var package = state.OpenPackage("Core");
			return new TabItem() { Header = package.ShortFileName, ToolTip = package.FileName, Content = package };
		}*/
	}

	/// <summary>
	/// An Unreal engine 3 game.
	/// </summary>
	public class Unreal3Game : UnrealBaseGame {
		#region Constructors

		public Unreal3Game(Engine engine, string name, string idPath, string iniFileName, string[] packagePaths, Dictionary<CultureInfo, string> languagePackagePaths)
			: base(engine, name, iniFileName) {
			if (idPath == null)
				throw new ArgumentNullException("idPath");
			if (packagePaths == null)
				throw new ArgumentNullException("packagePaths");
			if (languagePackagePaths == null)
				throw new ArgumentNullException("languagePackagePaths");

			IdPath = idPath;
			PackagePaths = new List<string>(packagePaths);
			LanguagePackagePaths = languagePackagePaths;
		}

		#endregion Constructors

		#region Properties

		/// <summary>
		/// Get a directory or file within the base path that uniquely identifies this game.
		/// </summary>
		public string IdPath { get; private set; }

		/// <summary>
		/// Get a list of package directories for this game.
		/// </summary>
		public List<string> PackagePaths { get; private set; }

		/// <summary>
		/// 
		/// </summary>
		public Dictionary<CultureInfo, string> LanguagePackagePaths { get; private set; }

		#endregion Properties

		#region Methods

		/*public override TabItem Browse(GameInstance instance) {
			var state = new State2(instance.Path, IniFileName);
			var package = state.OpenPackage("Core");
			return new TabItem() { Content = package };
		}*/

		protected override string SearchRegistry() {
			return null;
		}

		#endregion Methods
	}
}
