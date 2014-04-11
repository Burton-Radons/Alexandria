using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Alexandria.Engines.Unreal {
	/// <summary>
	/// A collection of exported and imported objects.
	/// </summary>
	public class Package : Core.Object {
		/// <summary>
		/// Initialize the <see cref="Package"/> by reading from the given filename.
		/// </summary>
		/// <param name="state">The <see cref="State"/> this <see cref="Package"/> is to exist within.</param>
		/// <param name="fileName">The name of the file to open that contains the <see cref="Package"/>.</param>
		public Package(State state, string fileName) : this(state, fileName, File.OpenRead(fileName)) { }

		/// <summary>
		/// Initialize the <see cref="Package"/> by reading from the given filename.
		/// </summary>
		/// <param name="state">The <see cref="State"/> this <see cref="Package"/> is to exist within.</param>
		/// <param name="fileName">The name of the file to open that contains the <see cref="Package"/>.</param>
		/// <param name="stream">The <see cref="Stream"/> to read from.</param>
		public Package(State state, string fileName, Stream stream) : this(state, fileName, new BinaryReader(stream)) { }

		/// <summary>
		/// Initialize the <see cref="Package"/> by reading from the given filename.
		/// </summary>
		/// <param name="state">The <see cref="State"/> this <see cref="Package"/> is to exist within.</param>
		/// <param name="fileName">The name of the file to open that contains the <see cref="Package"/>.</param>
		/// <param name="reader">The <see cref="BinaryReader"/> to read from.</param>
		public Package(State state, string fileName, BinaryReader reader) {
			if (state == null)
				throw new ArgumentNullException("state");
			if (reader == null)
				throw new ArgumentNullException("reader");

			if (reader.ReadInt32() != Magic)
				throw new Exception("Unreal package magic test failed.");
			Reader = reader;

			FileName = fileName;
			Export = new Unreal.Export(this, -1);
			Export.Name = Path.GetFileNameWithoutExtension(fileName);
			Export.LoadedObject = this;

			StateValue = state;
			FileVersion = reader.ReadUInt16();
			LicenseMode = reader.ReadUInt16();
			Flags = (PackageFlag)reader.ReadUInt32();
			var nameCount = reader.ReadInt32();
			var nameOffset = reader.ReadUInt32();
			var exportCount = reader.ReadInt32();
			var exportOffset = reader.ReadUInt32();
			var importCount = reader.ReadInt32();
			var importOffset = reader.ReadUInt32();
			if (FileVersion >= 68)
				Guid = new Guid(reader.ReadBytes(16));

			Imports = new ImportList(importCount);
			for (var index = 0; index < importCount; index++)
				Imports.Add(new Import(this, index));

			Exports = new ExportList(exportCount);
			for (var index = 0; index < exportCount; index++)
				Exports.Add(new Export(this, index));

			Names = new NameList(nameCount);
			reader.BaseStream.Position = nameOffset;
			for (var index = 0; index < nameCount; index++)
				Names.Add(new Name(this, reader));

			reader.BaseStream.Position = exportOffset;
			for (var index = 0; index < exportCount; index++)
				Exports[index].Load(reader);

			reader.BaseStream.Position = importOffset;
			for (var index = 0; index < importCount; index++)
				Imports[index].Load(reader);

			FilteredExports = new ExportList(exportCount);
			foreach (var export in Exports) {
				/*if(export.ObjectClassReference != null && export.ObjectClassReference.Name.EndsWith("Property")) {
					if(((((Alexandria.Engines.Unreal.Core.Property)export.Object).PropertyFlags) & Alexandria.Engines.Unreal.Core.PropertyFlag.Test) != 0)
						FilteredExports.Add(export);
				}*/
				if (export.ObjectClassReference == null || !export.ObjectClassReference.Name.EndsWith("Property"))
					FilteredExports.Add(export);
			}

			Game = DetermineGame(FileVersion, LicenseMode, IsEncrypted);
		}

		static PackageGame DetermineGame(int fileVersion, int licenseMode, bool isEncrypted) {
			int v = fileVersion, m = licenseMode;

			if (m == 0) {
				if (v == 76) return PackageGame.HarryPotterSorcerersStone;
				if (v == 79) return PackageGame.HarryPotterChamberOfSecrets;
				if (v >= 83 || isEncrypted) return PackageGame.Undying;
				if (v >= 68) return PackageGame.UnrealTournament;
				return PackageGame.Unreal;
			} else if (m == 25 /* build 927 */ || m == 28 /* demo and retail */ || m == 29 /* patch 1 */) return PackageGame.UnrealTournament2003;
			else if (m == 2481 /* v110 */ || (m == 635 || m == 763) /* v83 */) return PackageGame.Unreal2;
			else if (v == 118 && (m == 8 || m == 9 || (m >= 16 && m <= 19))) return PackageGame.ArmyOperations;
			else if (m == 8 && v == 120) return PackageGame.Devastation;
			else if (m == 30 && v == 119) return PackageGame.UnrealChampionship;
			else if (m == 58) return PackageGame.Borderlands;
			else if (m == 133) return PackageGame.ThiefDeadlyShadows;
			return PackageGame.Unknown;
		}

		/// <summary>A magic number that starts all Unreal packages.</summary>
		public const int Magic = unchecked((int)0x9E2A83C1);

		State StateValue;

		/// <summary>Get the filename of this package.</summary>
		public string FileName { get; private set; }

		/// <summary>Get the <see cref="FileName"/> without any path.</summary>
		public string ShortFileName {
			get {
				int index = Math.Max(FileName.LastIndexOf('/'), FileName.LastIndexOf('\\'));
				return ".../" + FileName.Substring(index + 1);
			}
		}

		/// <summary>Get the containing state.</summary>
		public override State State { get { return StateValue; } }

		/// <summary>Flags applied to this package.</summary>
		public PackageFlag Flags { get; protected set; }

		/// <summary>Get whether to allow downloading the package; the same as <c>((Flags &amp; PackageFlag.AllowDownload) != 0)</c>.</summary>
		public bool IsAllowDownload { get { return (Flags & PackageFlag.AllowDownload) != 0; } }

		/// <summary>Get whether the package is optional for the client; the same as <c>((Flags &amp; PackageFlag.ClientOptional) != 0)</c>.</summary>
		public bool IsClientOptional { get { return (Flags & PackageFlag.ClientOptional) != 0; } }

		/// <summary>
		/// Get whether this package only needs to exist or be loaded on the server; the same as <c>((Flags &amp; PackageFlag.ServerSideOnly) != 0)</c>.
		/// </summary>
		public bool IsServerSideOnly { get { return (Flags & PackageFlag.ServerSideOnly) != 0; } }

		/// <summary>
		/// Get whether there were broken links in the linker; the same as <c>((Flags &amp; PackageFlag.BrokenLinks) != 0)</c>.
		/// </summary>
		public bool IsBrokenLinks { get { return (Flags & PackageFlag.BrokenLinks) != 0; } }

		/// <summary>
		/// Get whether to not trust the package; the same as <c>((Flags &amp; PackageFlag.Unsecure) != 0)</c>.
		/// </summary>
		public bool IsUnsecure { get { return (Flags & PackageFlag.Unsecure) != 0; } }

		/// <summary>
		/// Get whether this package might be encrypted (used in Undying); the same as <c>((Flags &amp; PackageFlag.Encrypted) != 0)</c>.
		/// </summary>
		public bool IsEncrypted { get { return (Flags & PackageFlag.Encrypted) != 0; } }

		/// <summary>
		/// Get whether the client must download this package; the same as <c>((Flags &amp; PackageFlag.Required) != 0)</c>.
		/// </summary>
		public bool IsRequired { get { return (Flags & PackageFlag.Required) != 0; } }

		/// <summary>
		/// Access to the source file, if this is not synthetic or in-memory.
		/// </summary>
		public BinaryReader Reader { get; protected set; }

		/// <summary>Get the file version. Combined with the <see cref="LicenseMode"/>, this sometimes uniquely identifies a game.</summary>
		public ushort FileVersion { get; protected set; }

		/// <summary>Get the license mode.</summary>
		public ushort LicenseMode { get; protected set; }

		/// <summary>Get the collection of names used throughout the package.</summary>
		public NameList Names { get; protected set; }

		/// <summary>
		/// Exports from the package.
		/// </summary>
		public ExportList Exports { get; protected set; }

		/// <summary>
		/// <see cref="Exports"/> with the <see cref="Core.Property"/> exports filtered out (they're always children of another export).
		/// </summary>
		public ExportList FilteredExports { get; protected set; }

		/// <summary>
		/// Get the <see cref="Import"/> objects that this <see cref="Package"/> requires.
		/// </summary>
		public ImportList Imports { get; protected set; }

		/// <summary>
		/// Get the globally-unique identifier of the package; only present if <see cref="FileVersion"/> is at least 68.
		/// </summary>
		public Guid Guid { get; protected set; }

		/// <summary>
		/// Get the game this package is for, detected as a combination of the file version and license mode.
		/// </summary>
		public PackageGame Game { get; protected set; }

		/// <summary>Read a reference from the <see cref="BinaryReader"/>.</summary>
		/// <param name="reader">The <see cref="BinaryReader"/> to read from.</param>
		/// <returns>The resolved reference or <c>null</c>.</returns>
		public Reference ReadReference(BinaryReader reader) {
			int index = UIndex.Read(reader);
			return ResolveReference(index);
		}

		/// <summary>Read an <see cref="Export"/> reference from the <see cref="BinaryReader"/>.</summary>
		/// <param name="reader">The <see cref="BinaryReader"/> to read from.</param>
		/// <returns>The referenced <see cref="Export"/> or <c>null</c>.</returns>
		public Export ReadExport(BinaryReader reader) {
			var reference = ReadReference(reader);
			return reference != null ? reference.AsExport : null;
		}

		/// <summary>
		/// Convert an index into a <see cref="Reference"/>.
		/// </summary>
		/// <param name="index">The index to convert.</param>
		/// <returns>The <see cref="Reference"/> or <c>null</c>.</returns>
		public Reference ResolveReference(int index) {
			if (index > 0)
				return Exports[index - 1];
			if (index < 0)
				return Imports[-index - 1];
			return null;
		}

		/// <summary>
		/// Read a <see cref="Name"/> reference from the <see cref="BinaryReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="BinaryReader"/> to read from.</param>
		/// <returns>The <see cref="Name"/> object.</returns>
		public Name ReadNameIndex(BinaryReader reader) {
			int index = UIndex.Read(reader);
			return Names[index];
		}

		/// <summary>
		/// Read a <see cref="Name"/> reference from the <see cref="BinaryReader"/>, returning the <see cref="Name"/>'s string value.
		/// </summary>
		/// <param name="reader">The <see cref="BinaryReader"/> to read from.</param>
		/// <returns>The referenced <c>string</c>.</returns>
		public string ReadNameValue(BinaryReader reader) {
			return ReadNameIndex(reader).Value;
		}

		/// <summary>
		/// Search for an export.
		/// </summary>
		/// <param name="classPackageName"></param>
		/// <param name="className"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		public Export FindExport(string classPackageName, string className, string name) {
			foreach (var export in Exports) {
				if (export.Name != name)
					continue;
				if (export.ObjectClassReference == null) {
					if (classPackageName == "Core" && className == "Class")
						return export;
				} else {
					var objectClass = export.ObjectClassReference.AsExport;
				}
			}

			return null;
		}
	}

	/// <summary>
	/// Flags that can be applied to a <see cref="Package"/>.
	/// </summary>
	[Flags]
	public enum PackageFlag : uint {
		/// <summary>
		/// No flag value.
		/// </summary>
		None = 0,

		/// <summary>
		/// Allow downloading the package.
		/// </summary>
		AllowDownload = 0x0001,

		/// <summary>
		/// The package is optional for clients.
		/// </summary>
		ClientOptional = 0x0002,

		/// <summary>
		/// The package is only needed on the server side.
		/// </summary>
		ServerSideOnly = 0x0004,

		/// <summary>
		/// Loaded from linker with broken import links.
		/// </summary>
		BrokenLinks = 0x0008,

		/// <summary>
		/// The package is not to be trusted.
		/// </summary>
		Unsecure = 0x0010,

		/// <summary>
		/// Possibly encrypted; used in Undying.
		/// </summary>
		Encrypted = 0x0020,

		/// <summary>
		/// The client must download this package.
		/// </summary>
		Required = 0x8000,
	}

	/// <summary>
	/// Game that a <see cref="Package"/> is for.
	/// </summary>
	public enum PackageGame {
		/// <summary>
		/// No game could be identified.
		/// </summary>
		Unknown,

		/// <summary>
		/// Army Operations or Rainbow Six.
		/// </summary>
		ArmyOperations,

		/// <summary>Borderlands.</summary>
		Borderlands,

		/// <summary>Devastation.</summary>
		Devastation,

		/// <summary>Harry Potter and the Chamber of Secrets.</summary>
		HarryPotterChamberOfSecrets,

		/// <summary>Harry Potter and the Sorcerer's Stone.</summary>
		HarryPotterSorcerersStone,

		/// <summary>Lineage 2.</summary>
		Lineage2,

		/// <summary>Thief: Deadly Shadows, aka Thief 3.</summary>
		ThiefDeadlyShadows,

		/// <summary>Clive Barker Presents: Undying</summary>
		Undying,

		/// <summary>Unreal, aka Unreal 1.</summary>
		Unreal,

		/// <summary>Unreal 2</summary>
		Unreal2,

		/// <summary>Unreal Championship</summary>
		UnrealChampionship,

		/// <summary>Unreal Tournament</summary>
		UnrealTournament,

		/// <summary>Unreal Tournament 2003</summary>
		UnrealTournament2003,
	}
}
