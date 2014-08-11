using Glare.Assets;
using Glare.Internal;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria {
	/// <summary>
	/// This describes an individual game. This may be part of an <see cref="Alexandria.Engine"/>, or may directly be related to a <see cref="AlexandriaPlugin"/>.
	/// </summary>
	public abstract class Game : AlexandriaPluginFormatAsset {
		readonly Engine engine;

		/// <summary>
		/// Get the <see cref="Alexandria.Engine"/> that this <see cref="Game"/> is for, or <c>null</c> if it's not part of an engine.
		/// </summary>
		public Engine Engine { get { return engine; } }

		/// <summary>Get a globally-unique identifier to uniquely identify this game.</summary>
		public Guid Guid { get; protected set; }

		/// <summary>Get the type of <see cref="State"/> that this <see cref="Game"/> uses, or <c>null</c> if that is not implemented.</summary>
		public virtual Type StateType { get { return null; } }

		/// <summary>Get the application id of this <see cref="Game"/> on Valve's Steam service, or <c>null</c> if there is none.</summary>
		public int? SteamApplicationId { get; protected set; }

		/// <summary>
		/// The location of the registry keys for software products under the LocalMachine registry.
		/// </summary>
		public const string SoftwareRegistryBase = @"SOFTWARE\";

		/// <summary>
		/// The location of the registry keys for software products under the 32-bit abstraction layer, under the LocalMachine registry.
		/// </summary>
		public const string SoftwareRegistryBase32 = @"SOFTWARE\Wow6432Node\";

		/// <summary>
		/// The location of Steam application keys under <see cref="SoftwareRegistryBase"/> or <see cref="SoftwareRegistryBase32"/>.
		/// </summary>
		public const string SteamBase = @"Microsoft\Windows\CurrentVersion\Uninstall\Steam App ";

		/// <summary>Get the steam foler key name.</summary>
		public const string SteamFolderKeyName = "InstallLocation";

		/// <summary>
		/// Get the path within the LocalMachine registry for the game, relative to both <see cref="SoftwareRegistryBase32"/> and <see cref="SoftwareRegistryBase"/>, or <c>null</c> if it's not relevant for this game.
		/// </summary>
		public string RegistryPath { get; protected set; }

		/// <summary>
		/// Get the name of the folder registry key within the <see cref="RegistryPath"/> for this game.
		/// </summary>
		public string RegistryFolderName { get; protected set; }

		/// <summary>Find a value from the registry, returning <c>null</c>> if it's not found.</summary>
		/// <param name="baseKey"></param>
		/// <param name="path"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		protected string GetRegistryValue(RegistryKey baseKey, string path, string name) {
			var key = baseKey.OpenSubKey(path);
			if (key == null)
				return null;
			return key.GetValue(name, null) as string;
		}

		/// <summary>Get a registry value in the software registry directories.</summary>
		/// <param name="baseKey"></param>
		/// <param name="subpath"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		protected string GetRegistrySoftwareValue(RegistryKey baseKey, string subpath, string name) {
			return GetRegistryValue(baseKey, SoftwareRegistryBase + subpath, name) ??
				GetRegistryValue(baseKey, SoftwareRegistryBase32 + subpath, name);
		}

		/// <summary>Find a registry in the software registry directories and the Steam registry keys.</summary>
		/// <param name="subpath"></param>
		/// <param name="folderKeyName"></param>
		/// <returns></returns>
		protected string GetSoftwareRegistryValue(string subpath, string folderKeyName) {
			return (string.IsNullOrEmpty(subpath) ? null :
					GetRegistrySoftwareValue(Registry.LocalMachine, subpath, folderKeyName)
					?? GetRegistrySoftwareValue(Registry.CurrentUser, subpath, folderKeyName))
				?? (SteamApplicationId.HasValue ?
					GetRegistrySoftwareValue(Registry.LocalMachine, SteamBase + SteamApplicationId.Value, SteamFolderKeyName) : null);
		}

		/// <summary>Initialise the game.</summary>
		/// <param name="plugin"></param>
		/// <param name="engine"></param>
		/// <param name="guid">The <see cref="Guid"/> that uniquely identifies the <see cref="Game"/> or <c>null</c> to use the <see cref="Guid"/> of this <see cref="Type"/>. If a given <see cref="Game"/> class supports multiple games, the <see cref="Guid"/> must be unique.</param>
		Game(AlexandriaPlugin plugin, Engine engine, Guid? guid)
			: base(plugin) {
			this.engine = engine;
			Guid = guid.GetValueOrDefault(GetType().GUID);
		}

		/// <summary>Initialise the game.</summary>
		/// <param name="plugin"></param>
		/// <param name="guid">The <see cref="Guid"/> that uniquely identifies the <see cref="Game"/> or <c>null</c> to use the <see cref="Guid"/> of this <see cref="Type"/>. If a given <see cref="Game"/> class supports multiple games, the <see cref="Guid"/> must be unique.</param>
		public Game(AlexandriaPlugin plugin, Guid? guid) : this(plugin, null, guid) { }

		/// <summary>Initialise the game.</summary>
		/// <param name="engine"></param>
		/// <param name="guid">The <see cref="Guid"/> that uniquely identifies the <see cref="Game"/> or <c>null</c> to use the <see cref="Guid"/> of this <see cref="Type"/>. If a given <see cref="Game"/> class supports multiple games, the <see cref="Guid"/> must be unique.</param>
		public Game(Engine engine, Guid? guid) : this((AlexandriaPlugin)engine.Plugin, engine, guid) { }
	}
}
