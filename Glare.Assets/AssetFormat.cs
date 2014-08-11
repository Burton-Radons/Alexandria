using Glare.Framework;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Glare.Assets {
	/// <summary>
	/// This describes a resource format.
	/// </summary>
	/// <remarks>
	/// To implement creating, <see cref="CanCreate"/> and <see cref="Create"/> need to be overloaded.
	/// 
	/// To implement loading, <see cref="CanLoad"/>, <see cref="LoadMatch"/>, and <see cref="Load"/> need to be overloaded; <see cref="LoadTypesMutable"/> should be filled or provided in the constructor (if <see cref="CanLoad"/> is <c>true</c> and no types are provided in the constructor, the <see cref="PrimaryType"/> is automatically added).
	/// 
	/// To implement saving, <see cref="CanSave"/>, <see cref="Save"/>, and <see cref="SaveCheck"/> need to be overloaded; <see cref="SaveTypesMutable"/> should be filled or provided in the constructor (if <see cref="CanSave"/> is <c>true</c> and no types are provided in the constructor, the <see cref="PrimaryType"/> is automatically added).
	/// </remarks>
	public abstract class AssetFormat : PluginAsset {
		/// <summary>Get the types that this can create.</summary>
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		protected readonly Codex<Type> CreateTypesMutable = new Codex<Type>();

		/// <summary>Get the extensions of the files for this format.</summary>
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		protected readonly Codex<string> ExtensionsMutable = new Codex<string>();

		/// <summary>Get the types that this can load.</summary>
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		protected readonly Codex<Type> LoadTypesMutable = new Codex<Type>();

		/// <summary>Get the types that this can save.</summary>
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		protected readonly Codex<Type> SaveTypesMutable = new Codex<Type>();

		/// <summary>Get whether this <see cref="AssetFormat"/> can create a new <see cref="Asset"/> of the <see cref="PrimaryType"/>.</summary>
		public bool CanCreate { get; private set; }

		/// <summary>Get whether this <see cref="AssetFormat"/> can load files using <see cref="LoadMatch"/> and <see cref="Load"/>, as well as enumerating the types of <see cref="Asset"/>s it will load in <see cref="LoadTypes"/>.</summary>
		public bool CanLoad { get; private set; }

		/// <summary>Get whether this can save files using <see cref="SaveCheck"/> and <see cref="Save"/>.</summary>
		public bool CanSave { get; private set; }

		/// <summary>Get the types that this can create with <see cref="Create"/>.</summary>
		public ReadOnlyCodex<Type> CreateTypes { get { return CreateTypesMutable; } }

		/// <summary>Get the file extensions, including preceding ".", that this <see cref="AssetFormat"/> saves files as.</summary>
		/// <remarks>Implementors: The mutable form of this property is <see cref="ExtensionsMutable"/>.</remarks>
		public ReadOnlyCodex<string> Extensions { get { return ExtensionsMutable; } }

		/// <summary>Get whether this is format is for a general-use <see cref="ArchiveAsset"/> file, and not just a specialised one. For example, a ".zip" file format would be <c>true</c>, but an archive file for a specific game would be <c>false</c>.</summary>
		public bool IsGeneralUseArchiveFormat { get; protected set; }

		/// <summary>Get the types of <see cref="Asset"/>s that can be loaded by this format. This can be empty, if this <see cref="AssetFormat"/> doesn't implement loading.</summary>
		/// <remarks>Implementors: The mutable form of this property is <see cref="LoadTypesMutable"/>.</remarks>
		public ReadOnlyCodex<Type> LoadTypes { get { return LoadTypesMutable; } }

		/// <summary>Get the primary type of <see cref="Asset"/> that this <see cref="AssetFormat"/> deals with.</summary>
		public Type PrimaryType { get; private set; }

		/// <summary>Get the types of <see cref="Asset"/>s that can be saved by this format. This can be empty, if this <see cref="AssetFormat"/> doesn't implement saving.</summary>
		/// <remarks>Implementors: The mutable form of this property is <see cref="SaveTypesMutable"/>.</remarks>
		public ReadOnlyCodex<Type> SaveTypes { get { return SaveTypesMutable; } }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pluginResource">The context for the <see cref="AssetFormat"/>, used to find the <see cref="AssetPlugin"/>.</param>
		/// <param name="primaryType">The primary <see cref="Asset"/> type that this <see cref="AssetFormat"/> operates with.</param>
		/// <param name="canLoad">Whether this can load <see cref="Asset"/>s; if <c>true</c>, then <see cref="Load"/> and <see cref="LoadMatch"/> must be implemented.</param>
		/// <param name="loadTypes">Any additional <see cref="Asset"/> types that could be loaded beyond the <paramref name="primaryType"/>; this can only be provided if <paramref name="canLoad"/> is <c>true</c>.</param>
		/// <param name="canSave">Whether this can save <see cref="Asset"/>s; if <c>true</c>, then <see cref="Save"/> and <see cref="SaveCheck"/> must be implemented, and <paramref name="saveTypes"/> can be provided for additional types this can save beyond the <paramref name="primaryType"/>.</param>
		/// <param name="saveTypes">Any additional <see cref="Asset"/> types that this can save beyond the <paramref name="primaryType"/>; this can only be provided if <paramref name="canSave"/> is <c>true</c>.</param>
		/// <param name="canCreate">Whether this can create an <see cref="Asset"/>. If so, <see cref="Create"/> must be implemented, and <paramref name="createTypes"/> can be provided for additional types this can create beyond the <paramref name="primaryType"/>.</param>
		/// <param name="createTypes">Any additional <see cref="Asset"/> types that this can create beyond the <paramref name="primaryType"/>; this can only be provided if <paramref name="canCreate"/> is <c>true</c>.</param>
		/// <param name="extension">File extension that this format uses in the form of ".ext".</param>
		/// <param name="extensions">File extensions that this format uses in the form of <c>new string[] { ".ext", ".ext2" }</c></param>
		public AssetFormat(PluginAsset pluginResource, Type primaryType, bool canLoad = false, IList<Type> loadTypes = null, bool canSave = false, IList<Type> saveTypes = null, bool canCreate = false, IList<Type> createTypes = null, string extension = null, IList<string> extensions = null)
			: base(pluginResource.Plugin) {
			if (primaryType == null)
				throw new ArgumentNullException("primaryType");
			PrimaryType = primaryType;

			CanLoad = canLoad;
			CanSave = canSave;
			CanCreate = canCreate;

			if (loadTypes != null) {
				if (!CanLoad && loadTypes.Count > 0)
					throw new ArgumentException("CanLoad is false, so no loadTypes should be added.", "loadTypes");
				LoadTypesMutable.AddRange(loadTypes);
			} else if (CanLoad)
				LoadTypesMutable.Add(PrimaryType);

			if (saveTypes != null) {
				if (!CanSave && saveTypes.Count > 0)
					throw new ArgumentException("CanSave is false, so no saveTypes should be added.", "saveTypes");
				SaveTypesMutable.AddRange(saveTypes);
			} else if (CanSave)
				SaveTypesMutable.Add(PrimaryType);

			if (createTypes != null) {
				if (!CanCreate && CreateTypes.Count > 0)
					throw new ArgumentException("CanCreate is false, so no createTypes should be added.", "createTypes");
				CreateTypesMutable.AddRange(createTypes);
			} else if (CanCreate)
				CreateTypesMutable.Add(PrimaryType);

			if (extension != null)
				ExtensionsMutable.Add(extension);
			if (extensions != null)
				ExtensionsMutable.AddRange(extensions);
		}

		/// <summary>Create a new <see cref="Asset"/> of the <see cref="PrimaryType"/>.</summary>
		/// <param name="manager"></param>
		/// <param name="resourceType"></param>
		/// <returns></returns>
		public virtual Asset Create(AssetManager manager, Type resourceType) {
			if (CanCreate)
				throw new InvalidOperationException("This " + typeof(AssetFormat).Name + " " + GetType().Name + " says it can create resources, but it doesn't work. Bug!");
			throw new InvalidOperationException("This " + typeof(AssetFormat).Name + " " + GetType().Name + " cannot create resources.");
		}

		/// <summary>Load an <see cref="Asset"/>. This should have previously matched using <see cref="LoadMatch"/>.</summary>
		/// <param name="loader"></param>
		/// <returns></returns>
		public virtual Asset Load(AssetLoader loader) {
			if (CanLoad)
				throw new InvalidOperationException("This " + typeof(AssetFormat).Name + " " + GetType().Name + " says it can load resources, but it doesn't work. Bug!");
			throw new InvalidOperationException("This " + typeof(AssetFormat).Name + " " + GetType().Name + " cannot load resources.");
		}

		/// <summary>Attempt to match this <see cref="AssetFormat"/> against the loader.</summary>
		/// <param name="loader"></param>
		/// <returns></returns>
		public virtual LoadMatchStrength LoadMatch(AssetLoader loader) {
			return LoadMatchStrength.None;
		}

		/// <summary>Perform a quick match based on a magic string and a minimum length.</summary>
		/// <param name="loader"></param>
		/// <param name="magic"></param>
		/// <param name="minimumLength"></param>
		/// <returns></returns>
		protected LoadMatchStrength LoadMatchMagic(AssetLoader loader, string magic, int minimumLength = 0) {
			if (loader.Length < Math.Max(minimumLength, magic.Length))
				return LoadMatchStrength.None;
			return loader.Reader.MatchMagic(magic) ? LoadMatchStrength.Medium : LoadMatchStrength.None;
		}

		/// <summary>Perform a quick match based on a magic <see cref="Int32"/> value.</summary>
		/// <param name="loader"></param>
		/// <param name="magic"></param>
		/// <param name="minimumLength"></param>
		/// <returns></returns>
		protected LoadMatchStrength LoadMatchMagic(AssetLoader loader, int magic, int minimumLength = 4) {
			if (loader.Length < Math.Max(minimumLength, 4))
				return LoadMatchStrength.None;
			return loader.Reader.ReadInt32() != magic ? LoadMatchStrength.Medium : LoadMatchStrength.None;
		}

		/// <summary>Save the asset to the file.</summary>
		/// <param name="asset"></param>
		/// <param name="writer"></param>
		/// <param name="fileManager">The <see cref="FileManager"/> that might be necessary to save additional files. This can be <c>null</c> if it can't be supported.</param>
		public virtual void Save(Asset asset, BinaryWriter writer, FileManager fileManager) {
			if (CanSave)
				throw new InvalidOperationException("This " + typeof(AssetFormat).Name + " " + GetType().Name + " says it can save resources, but it doesn't work. Bug!");
			throw new InvalidOperationException("This " + typeof(AssetFormat).Name + " " + GetType().Name + " cannot save resources.");
		}

		/// <summary>Check whether the <see cref="Asset"/> can be saved with this <see cref="AssetFormat"/>.</summary>
		/// <param name="resource"></param>
		/// <returns></returns>
		public virtual SaveCheckResult SaveCheck(Asset resource) {
			if (CanSave)
				throw new InvalidOperationException("This " + typeof(AssetFormat).Name + " " + GetType().Name + " says it can save resources, but it doesn't implement SaveCheck. A bug!");
			return new SaveCheckResult(false, "This " + typeof(AssetFormat).Name + " " + GetType().Name + " cannot save resources.");
		}

		/// <summary>Load an <see cref="Asset"/>.</summary>
		/// <param name="loader"></param>
		/// <param name="formats"></param>
		/// <param name="resolveConflict"></param>
		/// <returns></returns>
		public static Asset LoadAsset(AssetLoader loader, IEnumerable<AssetFormat> formats, ResolveLoadConflictCallback resolveConflict = null) {
			LoadMatchStrength matchStrength;
			AssetFormat format = LoadMatchAsset(out matchStrength, loader, formats, resolveConflict);
			if (loader.Context != null)
				loader.Context.LoadErrors = loader.Errors;
			return format.Load(loader);
		}

		/// <summary>Load an <see cref="Asset"/>.</summary>
		/// <param name="loader"></param>
		/// <param name="formats"></param>
		/// <param name="resolveConflict"></param>
		/// <param name="progress"></param>
		/// <param name="progressUpdateRate"></param>
		/// <returns></returns>
		public static Task<Asset> LoadAssetAsync(AssetLoader loader, IEnumerable<AssetFormat> formats, ResolveLoadConflictCallback resolveConflict = null, AssetLoaderProgressCallback progress = null, TimeSpan? progressUpdateRate = null) {
			TimeSpan progressUpdateRateValue = progressUpdateRate.GetValueOrDefault(TimeSpan.FromSeconds(0.1));
			LoadMatchStrength matchStrength;
			AssetFormat format = LoadMatchAsset(out matchStrength, loader, formats, resolveConflict);
			if (loader.Context != null)
				loader.Context.LoadErrors = loader.Errors;

			return new Task<Asset>(() => {
				Asset asset = null;
				bool complete = false;

				Thread loadThread = new Thread(() => {
					asset = format.Load(loader);
					complete = true;
				});

				loadThread.Start();

				while (!complete) {
					if (!loadThread.IsAlive)
						throw new InvalidOperationException("The load operation failed.");
					Thread.Sleep(progressUpdateRateValue);
					if (progress != null)
						progress.Invoke(loader);
				}

				return asset;
			});
		}

		/// <summary>Find the best <see cref="AssetFormat"/> for loading an <see cref="Asset"/>.</summary>
		/// <param name="matchStrength"></param>
		/// <param name="loader"></param>
		/// <param name="formats"></param>
		/// <param name="resolveConflict"></param>
		/// <returns></returns>
		public static AssetFormat LoadMatchAsset(out LoadMatchStrength matchStrength, AssetLoader loader, IEnumerable<AssetFormat> formats, ResolveLoadConflictCallback resolveConflict = null) {
			if (loader == null)
				throw new ArgumentNullException("context");
			if (formats == null)
				throw new ArgumentNullException("formats");

			AssetFormat bestFormat = null;
			LoadMatchStrength bestMatch = LoadMatchStrength.None;
			bool bestConflict = false;

			// Attempt to find the one best loader.
			foreach (AssetFormat format in formats) {
				if (!format.CanLoad || !format.IsEnabled)
					continue;

				LoadMatchStrength match = format.LoadMatch(loader);
				loader.Reset();
				if (match <= LoadMatchStrength.None || match < bestMatch)
					continue;

				if (match == bestMatch)
					bestConflict = true;
				else {
					bestFormat = format;
					bestMatch = match;
					bestConflict = false;
				}
			}

			matchStrength = bestMatch;

			// If a single best loader is found, return it.
			if (!bestConflict) {
				if (bestFormat == null)
					throw new InvalidDataException("No loader could be found for " + loader.Name + ".");

				return bestFormat;
			}

			// Otherwise there are multiple formats with equal match strengths; gather those together.
			List<AssetFormat> conflicts = new List<AssetFormat>();

			foreach (AssetFormat format in formats) {
				if (!format.CanLoad || !format.IsEnabled)
					continue;

				loader.Reset();
				LoadMatchStrength match = format.LoadMatch(loader);
				if (match == bestMatch)
					conflicts.Add(format);
			}

			// Attempt to resolve the conflict.
			bestFormat = null;
			if (resolveConflict != null)
				bestFormat = resolveConflict(loader, conflicts, matchStrength);

			// If no resolution is found, throw an exception.
			if (bestFormat == null)
				throw CreateConflictException(loader, matchStrength, conflicts);

			// A resolution was found, so return the best format.
			return bestFormat;
		}

		static InvalidDataException CreateConflictException(AssetLoader loader, LoadMatchStrength matchStrength, IList<AssetFormat> formats) {
			string text = string.Format("{3} {0} loaders matched {1} with strength {2}: {4}",
				typeof(AssetFormat).Name,
				loader.Name,
				MatchStrengthToString(matchStrength),
				formats.Count, string.Join(", ", (from i in formats select i.Name)));
			return new InvalidDataException(text);
		}

		static string MatchStrengthToString(LoadMatchStrength reference, LoadMatchStrength value) {
			if (value < reference)
				return reference.ToString() + (int)(value - reference);
			else
				return reference + "+" + (int)(value - reference);
		}

		/// <summary>Get a string describing a match strength, which handles values in between like "Perfect-2" or "Strong+3".</summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string MatchStrengthToString(LoadMatchStrength value) {
			if (value >= LoadMatchStrength.Perfect)
				return MatchStrengthToString(LoadMatchStrength.Perfect, value);
			if (value >= LoadMatchStrength.Comprehensive)
				return MatchStrengthToString(LoadMatchStrength.Comprehensive, value);
			if (value >= LoadMatchStrength.Strong)
				return MatchStrengthToString(LoadMatchStrength.Strong, value);
			if (value >= LoadMatchStrength.Medium)
				return MatchStrengthToString(LoadMatchStrength.Medium, value);
			if (value >= LoadMatchStrength.Weak)
				return MatchStrengthToString(LoadMatchStrength.Weak, value);
			if (value >= LoadMatchStrength.Generic)
				return MatchStrengthToString(LoadMatchStrength.Generic, value);
			if (value > LoadMatchStrength.None)
				return MatchStrengthToString(LoadMatchStrength.Fallback, value);
			return MatchStrengthToString(LoadMatchStrength.None, value);
		}
	}

	/// <summary>
	/// This is used to resolve a conflict in loading formats.
	/// </summary>
	/// <param name="context"></param>
	/// <param name="formats"></param>
	/// <param name="matchStrength"></param>
	/// <returns></returns>
	public delegate AssetFormat ResolveLoadConflictCallback(AssetLoader context, IList<AssetFormat> formats, LoadMatchStrength matchStrength);

	/// <summary>
	/// Results from a save check operation.
	/// </summary>
	public class SaveCheckResult {
		/// <summary>Get a collection of the problems with saving the resource, or effects on the resource that saving to this format will have.</summary>
		public ReadOnlyCodex<string> Problems { get; private set; }

		/// <summary>Whether saving can continue.</summary>
		public bool Result { get; private set; }

		/// <summary>Get a <see cref="SaveCheckResult"/> that simply can't save the <see cref="Asset"/>.</summary>
		public static readonly SaveCheckResult False = new SaveCheckResult(false);

		/// <summary>Get a <see cref="SaveCheckResult"/> that simply can save the <see cref="Asset"/>.</summary>
		public static readonly SaveCheckResult True = new SaveCheckResult(true);

		/// <summary>Initialise the results.</summary>
		/// <param name="result"></param>
		/// <param name="problems"></param>
		public SaveCheckResult(bool result, params string[] problems) {
			Result = result;
			Problems = new Codex<string>(problems);
		}
	}

	/// <summary>
	/// Match level returned by <see cref="AssetFormat.LoadMatch"/>. Each is an integer value, and so there can be values in between if necessary.
	/// 
	/// The general guidelines are these: If you don't match at all, return <see cref="None"/>. If you're a fallback (such as binary or text), return <see cref="Fallback"/>. If you're a fallback overload, return <see cref="Generic"/>. If the match is a poor quality default like file size for a palette or file name, return <see cref="Weak"/>. If you match just a magic number, return <see cref="Medium"/>. If you match a whole header and you're pretty sure you can understand the format, return <see cref="Strong"/>. If you matched the whole file and you know it both can't be anything else and is fully comprehensible, return <see cref="Comprehensive"/>. If you need more than that, return something between <see cref="Comprehensive"/> and <see cref="Perfect"/>. Don't return <see cref="Perfect"/>. Values in between are necessarily based on context; you're trying to prevent some other loader from matching, because if two loaders match at the same level, neither is used.
	/// </summary>
	public enum LoadMatchStrength {
		/// <summary>(0) The loader does not match the stream.</summary>
		None = 0,

		/// <summary>(10/100) The loader doesn't match, but can be used as a default. This is used by the binary/text loader.</summary>
		Fallback = 10,

		/// <summary>(20/100) The match is a poor quality default, such as a text file loader.</summary>
		Generic = 20,

		/// <summary>(30/100) The match is weak. Weaker than <see cref="Medium"/> (40/100) but stronger than <see cref="Generic"/> (20/100).</summary>
		Weak = 30,

		/// <summary>(40/100) The match is medium-strong. This is generally used if the file matches a magic number or part of the header. Not as strong as <see cref="Strong"/> (40/100) but stronger than <see cref="Weak"/> (30/100).</summary>
		Medium = 40,

		/// <summary>(50/100) The match is strong but not comprehensive. If you don't load the entire file but just match a header, you should return this. Weaker than <see cref="Comprehensive"/> (75/100) but stronger than <see cref="Medium"/> (40/100).</summary>
		Strong = 50,

		/// <summary>(75) The match is strong and comprehensive. You've matched the header extensively.</summary>
		Comprehensive = 75,

		/// <summary>(100) The object cannot be anything else.</summary>
		Perfect = 100,
	}

	/// <summary>When an <see cref="Asset"/> is being loaded asynchronously, this can optionally be called periodically to receive updates on the actions.</summary>
	/// <param name="loader"></param>
	public delegate void AssetLoaderProgressCallback(AssetLoader loader);
}
