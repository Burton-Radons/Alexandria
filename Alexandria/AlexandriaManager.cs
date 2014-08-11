using Glare;
using Glare.Assets;
using Glare.Framework;
using Glare.Graphics;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alexandria {
	/// <summary>
	/// Asset manager for Alexandria.
	/// </summary>
	public class AlexandriaManager : AssetManager {
		bool BaseConstructorComplete = false;

		/// <summary>Get an enumerator over all of the <see cref="Game"/>s.</summary>
		public IEnumerable<Game> AllGames {
			get {
				foreach (PluginAsset basePlugin in Plugins) {
					AlexandriaPlugin plugin = basePlugin as AlexandriaPlugin;

					if (!basePlugin.IsEnabled || plugin == null)
						continue;
					foreach (Game game in plugin.Games)
						if (game.IsEnabled)
							yield return game;
					foreach (Engine engine in plugin.Engines)
						foreach (Game game in engine.Games)
							if (game.IsEnabled)
								yield return game;
				}
			}
		}

		/// <summary>Get or set whether to use a debugger instead of attempting to catch exceptions. This also stores the setting.</summary>
		public override bool DebuggingEnabled {
			get { return base.DebuggingEnabled; }

			set {
				if (value == base.DebuggingEnabled)
					return;
				base.DebuggingEnabled = value;

				if (BaseConstructorComplete) {
					Settings.DebuggingEnabled = value;
					Settings.Save();
				}
			}
		}

		/// <summary>Get the string collection of the names of all disabled plugin resources.</summary>
		static StringCollection DisabledPluginResources {
			get {
				return Settings.DisabledPluginResources ??
					(Settings.DisabledPluginResources = new System.Collections.Specialized.StringCollection());
			}
		}

		static Properties.Settings Settings { get { return Properties.Settings.Default; } }

		Dictionary<Aggregate<string, Game, string>, PathState> PathStates = new Dictionary<Aggregate<string, Game, string>, PathState>();

		double BackgroundOperationProgressField;
		BackgroundOperation CurrentBackgroundOperationField;

		static readonly PropertyInfo BackgroundOperationProgressProperty = GetProperty<AlexandriaManager>("BackgroundOperationProgress");
		static readonly PropertyInfo CurrentBackgroundOperationProperty = GetProperty<AlexandriaManager>("CurrentBackgroundOperation");

		/// <summary>Get the current background operation progress level, from 0 to 100.</summary>
		public double BackgroundOperationProgress {
			get { return BackgroundOperationProgressField; }
			private set { SetProperty(BackgroundOperationProgressProperty, ref BackgroundOperationProgressField, ref value); }
		}

		/// <summary>Get the current background operation being executed.</summary>
		public BackgroundOperation CurrentBackgroundOperation {
			get { return CurrentBackgroundOperationField; }
			private set { SetProperty(CurrentBackgroundOperationProperty, ref CurrentBackgroundOperationField, ref value); }
		}

		readonly Codex<BackgroundOperation> BackgroundOperations = new Codex<BackgroundOperation>();

		Thread BackgroundOperationThread;

		void BackgroundOperationUpdateProgress(double value) {
			BackgroundOperationProgress = value;
		}

		void BackgroundOperationThreadStart() {
			while (true) {
				lock (BackgroundOperations) {
					if (BackgroundOperations.Count > 0) {
						CurrentBackgroundOperation = BackgroundOperations[0];
						BackgroundOperations.RemoveAt(0);

						try {
							CurrentBackgroundOperation.Run(BackgroundOperationUpdateProgress);
						} catch (Exception exception) {
							Debugger.Log(0, "Exception", exception.ToString());
							if (DebuggingEnabled)
								Debugger.Break();
						}

						BackgroundOperationProgress = 0;
						CurrentBackgroundOperation = null;
					}
				}

				Thread.Sleep(100);
			}
		}

		/// <summary>Add a background operation.</summary>
		/// <param name="name"></param>
		/// <param name="description"></param>
		/// <param name="callbacks"></param>
		/// <returns></returns>
		public BackgroundOperation AddBackgroundOperation(string name, string description, params BackgroundOperationCallback[] callbacks) {
			BackgroundOperation operation = new BackgroundOperation(this, name, description, callbacks);
			lock(BackgroundOperations)
				BackgroundOperations.Add(operation);
			return operation;
		}

		/// <summary>Initialise the manager, loading settings.</summary>
		public AlexandriaManager() {
			BackgroundOperationThread = new Thread(BackgroundOperationThreadStart);
			BackgroundOperationThread.Start();

			BaseConstructorComplete = true;
			if (Settings.FirstRun) {
				Settings.FirstRun = false;
				Settings.DebuggingEnabled = DebuggingEnabled;
				Settings.Save();
			} else {
				base.DebuggingEnabled = Settings.DebuggingEnabled;
			}
		}

		/// <summary>Stop the background operation thread monitor.</summary>
		~AlexandriaManager() {
			BackgroundOperationThread.Abort();
		}

		/// <summary>Search for the first <see cref="Game"/> with the given <see cref="Type"/>, or return <c>null</c> if not found. An exact match is attempted first, and then the first <see cref="Game"/> to inherit from the <see cref="Type"/> is matched.</summary>
		/// <param name="gameType"></param>
		/// <returns></returns>
		public Game GetGame(Type gameType) {
			foreach (Game game in AllGames)
				if (game.GetType() == gameType)
					return game;
			foreach (Game game in AllGames)
				if (game.GetType().IsSubclassOf(gameType))
					return game;
			return null;
		}

		/// <summary>Get whether the plugin asset is enabled, using the application settings.</summary>
		/// <param name="resource"></param>
		/// <returns></returns>
		protected override bool GetIsPluginAssetEnabled(PluginAsset resource) {
			return !DisabledPluginResources.Contains(resource.GetType().FullName);
		}

		/// <summary>Get a <see cref="PathState"/> for the given combination of parameters, creating one if necessary.</summary>
		/// <param name="game"></param>
		/// <param name="fileManager"></param>
		/// <param name="path"></param>
		/// <returns></returns>
		public PathState GetPathState(Game game, FileManager fileManager, string path) {
			if (game == null)
				throw new ArgumentNullException("game");
			if (fileManager == null)
				throw new ArgumentNullException("fileManager");

			var id = Aggregate.Create(fileManager.Id, game, path);
			PathState state;

			path = path.Replace('\\', '/');
			if (!PathStates.TryGetValue(id, out state)) {
				Type stateType = game.StateType;
				if (stateType == null)
					throw new ArgumentException(string.Format("The game {0} does not have an accepted {1} state type.", game, typeof(State).Name));

				ConstructorInfo constructor = stateType.GetConstructor(new Type[] { typeof(AlexandriaManager), typeof(string), typeof(FileManager) });

				if (constructor == null)
					throw new Exception(string.Format("State type {0} does not have an appropriate constructor, like the one for {1}.", stateType.FullName, typeof(State).FullName));

				state = (PathState)constructor.Invoke(new object[] { this, path, fileManager });
				PathStates[id] = state;
			}

			return state;
		}

		/// <summary>
		/// Load plugin DLLs.
		/// </summary>
		public void LoadPlugins() {
			LoadPlugins(Application.StartupPath, "Alexandria*.dll");
		}

		/// <summary>Store whether the plugin asset is enabled using the application settings.</summary>
		/// <param name="resource"></param>
		/// <param name="value"></param>
		protected override void SetIsPluginAssetEnabled(PluginAsset resource, bool value) {
			if (value != GetIsPluginAssetEnabled(resource)) {
				if (value)
					DisabledPluginResources.Remove(GetType().FullName);
				else
					DisabledPluginResources.Add(GetType().FullName);
				Properties.Settings.Default.Save();
			}
		}
	}
}
