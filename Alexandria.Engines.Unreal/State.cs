using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alexandria.Engines.Unreal.Core;
using System.IO;
using Glare.Assets;

namespace Alexandria.Engines.Unreal {
	/// <summary>
	/// Shared engine state.
	/// </summary>
	public class State : PathState {
		/// <summary>
		/// List of paths that can be searched for packages.
		/// </summary>
		public List<string> PackagePaths { get; protected set; }

		/// <summary>
		/// Packages that have been loaded.
		/// </summary>
		public List<Package> Packages { get; protected set; }

		Dictionary<string, Dictionary<string, Dictionary<string, Type>>> FactoryTypes = new Dictionary<string, Dictionary<string, Dictionary<string, Type>>>();

		/// <summary>Initialise the state.</summary>
		/// <param name="manager"></param>
		/// <param name="rootPath"></param>
		/// <param name="fileManager"></param>
		public State(AlexandriaManager manager, string rootPath, FileManager fileManager)
			: base(manager, rootPath, fileManager) {
			PackagePaths = new List<string>();
			Packages = new List<Package>();

			string Core = "Core", Class = "Class";

			RegisterTypes(Core, Class,
			    typeof(BoolProperty), typeof(Brush), typeof(ByteProperty),
			    typeof(ClassProperty), typeof(Const),
			    typeof(Alexandria.Engines.Unreal.Core.Enum),
			    typeof(FloatProperty), typeof(Function),
			    typeof(IntProperty), typeof(InterpolationPoint),
			    typeof(Level), typeof(LevelInfo), typeof(LevelSummary),
			    typeof(Light),
			    typeof(Model),
			    typeof(NameProperty),
			    typeof(ObjectProperty),
			    typeof(Polys),
			    typeof(Struct), typeof(StructProperty), typeof(StrProperty),
			    typeof(TextBuffer),
			    typeof(ZoneInfo));

			RegisterStateFrames(Core, Class,
			    "AmbientSound",
			    "Camera",
			    "ElectricityEmitter",
			    "HidePoint",
			    "PathNode", "PatrolPoint", "PlayerStart",
			    "Teleporter");

			RegisterStateFrames(Core, Class,
			    "ATM",
			    "Candybar", "CeilingFan", "CeilingFanMotor", "Chair1", "CigaretteMachine", "Cigarettes", "ComputerPublic",
			    "DXLogo", "DXText", "DeusExLevelInfo", "DeusExMover",
			    "Flowers",
			    "EidosLogo",
			    "IonStormLogo",
			    "JordanShea",
			    "MedKit",
			    "NathanMadison",
			    "Plant2",
			    "SandraRenton", "Sodacan", "SoyFood",
			    "Trashcan2");
		}

		/// <summary>
		/// Add a path to the collection, if it exists. Returns whether the directory exists and was added.
		/// </summary>
		/// <param name="fullPath"></param>
		public bool AddPackagePath(string fullPath) {
			var directoryName = Path.GetDirectoryName(fullPath);
			if (!FileManager.Exists(directoryName))
				return false;
			fullPath = fullPath.Replace("\\", "/");
			PackagePaths.Add(fullPath);
			return true;
		}

		/// <summary>Add a list of sub-paths for the packages.</summary>
		/// <param name="subPaths"></param>
		public void AddPackageSubPaths(params string[] subPaths) {
			AddPackageSubPaths((IEnumerable<string>)subPaths);
		}

		/// <summary>Add a list of sub-paths for the packages.</summary>
		/// <param name="subPaths"></param>
		public void AddPackageSubPaths(IEnumerable<string> subPaths) {
			foreach (var path in subPaths)
				AddPackagePath(RootPath + path);
		}

		/// <summary>Open a given package.</summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public Package OpenPackage(string name) {
			foreach (var package in Packages)
				if (package.Export.Name == name)
					return package;
			foreach (var path in PackagePaths) {
				var fileName = path.Replace("*", name);
				if (FileManager.Exists(fileName)) {
					var package = new Package(this, fileName);
					Packages.Add(package);
					return package;
				}
			}
			throw new Exception("Package " + name + " could not be found.");
		}

		/// <summary>Get the list of package filenames.</summary>
		/// <returns></returns>
		public List<string> GetPackageFilenames() {
			List<string> list = new List<string>();

			foreach (string packagePath in PackagePaths) {
				int split = Math.Max(packagePath.LastIndexOf('/'), packagePath.LastIndexOf('\\'));
				string path = packagePath.Substring(0, split);
				string searchPattern = packagePath.Substring(split + 1);
				string[] files = FileManager.GetFiles(path, searchPattern, SearchOption.TopDirectoryOnly);
				foreach (string file in files)
					list.Add(Path.GetFileNameWithoutExtension(file));
			}

			return list;
		}

		/// <summary>Register a new type.</summary>
		/// <param name="packageName"></param>
		/// <param name="className"></param>
		/// <param name="objectName"></param>
		/// <param name="type"></param>
		public void RegisterType(string packageName, string className, string objectName, Type type) {
			if (!type.IsSubclassOf(typeof(RootObject)))
				throw new Exception();

			Dictionary<string, Dictionary<string, Type>> packages;
			if (!FactoryTypes.TryGetValue(packageName, out packages))
				packages = FactoryTypes[packageName] = new Dictionary<string, Dictionary<string, Type>>(1);

			Dictionary<string, Type> types;
			if (!packages.TryGetValue(className, out types))
				types = packages[className] = new Dictionary<string, Type>();

			// Note this can override a previously registered factory.
			types[objectName] = type;
		}

		public void RegisterType(string packageName, string className, Type type) {
			RegisterType(packageName, className, type.Name, type);
		}

		public void RegisterTypes(string packageName, string className, params Type[] types) {
			foreach (var type in types)
				RegisterType(packageName, className, type);
		}

		public void RegisterStateFrames(string packageName, string className, params string[] typeNames) {
			foreach (var type in typeNames)
				RegisterType(packageName, className, type, typeof(StateFrame));
		}

		public RootObject CallFactory(string packageName, string className, string objectName) {
			Dictionary<string, Dictionary<string, Type>> packages;
			Dictionary<string, Type> types;
			Type type;

			if (FactoryTypes.TryGetValue(packageName, out packages)) {
				if (packages.TryGetValue(className, out types)) {
					if (types.TryGetValue(objectName, out type)) {
						return (RootObject)type.GetConstructor(Type.EmptyTypes).Invoke(null);
					}
				}
			}

			throw new Exception("There is no factory defined for " + packageName + ":" + className + ":" + objectName + ".");
		}
	}

	/// <summary>
	/// Unreal 1 or 2 state.
	/// </summary>
	public class State2 : State {
		/// <summary>
		/// Initialization file for the entry point module.
		/// </summary>
		public IniFile IniFile { get; protected set; }

		public State2(AlexandriaManager manager, string rootPath, FileManager fileManager, string iniFileName)
			: base(manager, rootPath, fileManager) {
			IniFile = new IniFile(RootPath + iniFileName);

			IniFileSection section = IniFile["Core.System"];
			List<string> paths = section.GetMultiple("Paths");
			paths.AddRange(section.GetMultiple("Paths__t")); // Thief 3: Deadly Shadows
			foreach (var path in paths) {
				var fullPath = RootPath + "System/" + path;
				var simplePath = Path.GetFullPath(Path.GetDirectoryName(fullPath)) + "\\" + Path.GetFileName(fullPath);
				AddPackagePath(simplePath);
			}
		}
	}
}
