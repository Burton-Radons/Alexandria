using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Globalization;

namespace Alexandria.Engines.Unreal {
	/// <summary>
	/// The Unreal Engine.
	/// </summary>
	public class Engine : Alexandria.Engine {
		internal Engine(Plugin plugin)
			: base(plugin) {
			AddGame(AlphaProtocol = new UnrealGame(this, "Alpha Protocol", @"Obsidian\Alpha Protocol", "InstallPath", ue3IniFile, 34010,
				guid: new Guid("5E7A67E6-0BF4-47C7-A31F-56BBFEF7B053")));
			AddGame(BatmanArkhamAsylum = new UnrealGame(this, "Batman: Arkham Asylum", @"RocksteadyLtd\Batman Arkham Asylum GOTY", "Install Directory", ue3IniFile, 35140,
				guid: new Guid("9117ED86-ED8F-4D2C-9EA2-DE63BCA67AF3")));
			AddGame(Borderlands = new UnrealGame(this, "Borderlands", @"Gearbox Software\Borderlands", "InstallFolder", "../" + ue3IniFile, 8980,
				guid: new Guid("3EAC3608-1C4D-4549-9299-209C0EFF068E")));
			AddGame(DeusEx = new UnrealGame(this, "Deus Ex", @"Unreal Technology\Installed Apps\Deus Ex", "Folder", "System/DeusEx.ini", 6910,
				guid: new Guid("B329275B-55A3-4A01-A7A6-C9585F49082D")));
			AddGame(DeusExInvisibleWar = new UnrealGame(this, "Deus Ex: Invisible War", @"Ion Storm\Deus Ex - Invisible War", "ION_ROOT_PC", "System/Default.ini", 6920,
				guid: new Guid("32A4BAA7-4AA3-425D-B0E1-BB747CA9709B")));
			AddGame(Dishonored = new UnrealGame(this, "Dishonored", registryPath: null, registryFolderName: null, iniFileName: ue3IniFile, steamApplicationId: 205100,
				guid: new Guid("9C143AC5-7551-48F6-9AD3-353AA4250A07")));
			AddGame(MassEffect = new UnrealGame(this, "Mass Effect", null, null, ue3IniFile, null,
				guid: new Guid("A762255E-0BA9-41DF-B81B-CB7C5FE91BEF")));
			AddGame(MassEffect2 = new UnrealGame(this, "Mass Effect 2", null, null, ue3IniFile, null,
				guid: new Guid("3D877FAC-7346-499A-8F7E-80468F795862")));
			AddGame(MassEffect3 = new UnrealGame(this, "Mass Effect 3", @"BioWare\Mass Effect 3", null, ue3IniFile, null,
				guid: new Guid("7C03A8A4-8D3D-46C9-BEA4-962E7CFF9CB6")));
			AddGame(TheLastRemnant = new UnrealGame(this, "The Last Remnant", null, null, ue3IniFile, steamApplicationId: 23310,
				guid: new Guid("EDAEBDAD-6FB4-41A1-B954-660B5CEDBCB4")));
			AddGame(ThiefDeadlyShadows = new UnrealGame(this, "Thief: Deadly Shadows", @"Ion Storm\Thief - Deadly Shadows", "ION_ROOT", "System/DEFAULT.INI", 
				steamApplicationId: 6980,
				guid: new Guid("400BD744-F163-4D23-B801-FE9A1B8A385C")));
			AddGame(Unreal2TheAwakening = new UnrealGame(this, "Unreal II: The Awakening", null, null, ue2IniFile, 
				steamApplicationId: 13200,
				guid: new Guid("6CB43728-3E79-4B7A-BC4D-AFD17EDB38BC")));
			AddGame(UnrealGold = new UnrealGame(this, "Unreal Gold", @"Unreal Technology\Installed Apps\Unreal Gold", "Folder", ue2IniFile, 
				steamApplicationId: 13250,
				guid: new Guid("9A418801-8D65-4020-B697-E0CF8A9D1047")));
			AddGame(UnrealTournament = new UnrealGame(this, "Unreal Tournament", @"Unreal Technology\Installed Apps\UnrealTournament", "Folder", ue2IniFile,
				steamApplicationId: 13240,
				guid: new Guid("F8570C6A-442E-40BA-8183-B171FD69E2E2")));
			AddGame(UnrealTournament2004 = new UnrealGame(this, "Unreal Tournament 2004", @"Unreal Technology\Installed Apps\UT2004", "Folder", ue2IniFile,
				steamApplicationId: 13230,
				guid: new Guid("8023FA95-7B0B-43E3-9A2C-8CC8526C3367")));
			AddGame(UnrealTournament3 = new UnrealGame(this, "Unreal Tournament 3", null, null, ue3IniFile,
				steamApplicationId: 13210,
				guid: new Guid("CF9D07EE-93CE-4E82-8232-30B578BEA915")));
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

		/// <summary>Alpha Protocol.</summary>
		public readonly UnrealGame AlphaProtocol;

		/// <summary>Batman: Arkham Asylum</summary>
		public readonly UnrealGame BatmanArkhamAsylum;

		/// <summary>Borderlands</summary>
		public readonly UnrealGame Borderlands;

		/// <summary>Deus Ex (Deus Ex 1)</summary>
		public readonly UnrealGame DeusEx;
		
		/// <summary>Deus Ex: Invisible War (Deus Ex 2)</summary>
		public readonly UnrealGame DeusExInvisibleWar;

		/// <summary>Dishonored</summary>
		public readonly UnrealGame Dishonored;

		/// <summary>Mass Effect</summary>
		public readonly UnrealGame MassEffect;

		/// <summary>Mass Effect 2</summary>
		public readonly UnrealGame MassEffect2;

		/// <summary>Mass Effect 3</summary>
		public readonly UnrealGame MassEffect3;

		/// <summary>The Last Remnant</summary>
		public readonly UnrealGame TheLastRemnant;

		/// <summary>Thief: Deadly Shadows (Thief 3)</summary>
		public readonly UnrealGame ThiefDeadlyShadows;

		/// <summary>Unreal 2: The Awakening (Unreal 2).</summary>
		public readonly UnrealGame Unreal2TheAwakening;

		/// <summary>Unreal Gold</summary>
		public readonly UnrealGame UnrealGold;

		/// <summary>Unreal Tournament.</summary>
		public readonly UnrealGame UnrealTournament;

		/// <summary>Unreal Tournament 2004.</summary>
		public readonly UnrealGame UnrealTournament2004;

		/// <summary>Unreal Tournament 3.</summary>
		public readonly UnrealGame UnrealTournament3;
	}

	/// <summary>Base class of the Unreal <see cref="Game"/>s.</summary>
	public abstract class UnrealBaseGame : Game {
		public UnrealBaseGame(Engine engine, string name, string iniFileName, Guid guid)
			: base(engine, guid) {
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

		public override void Detect(ICollection<GameInstance> instances) {
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
		public UnrealGame(Engine engine, string name, string registryPath, string registryFolderName, string iniFileName, int? steamApplicationId, Guid guid)
			: base(engine, name, iniFileName, guid) {
			RegistryPath = registryPath;
			RegistryFolderName = registryFolderName;
			SteamApplicationId = steamApplicationId;
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

		public Unreal3Game(Engine engine, string name, string idPath, string iniFileName, string[] packagePaths, Dictionary<CultureInfo, string> languagePackagePaths, Guid guid)
			: base(engine, name, iniFileName, guid) {
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
