using Glare.Internal;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Glare.Assets.Formats {
	/// <summary>Manager for a PK-Zip archive file.</summary>
	public class ZipArchive : ArchiveAsset {
		/// <summary>Magic number at the start of any zip file ("PK\x03\x04").</summary>
		public const int Magic = 0x04034B50;

		/// <summary>Get the zip library for this file.</summary>
		public ZipFile ZipFile { get; private set; }

		internal ZipArchive(AssetLoader loader)
			: base(loader) {
			ZipFile = new ZipFile(loader.Reader.BaseStream);
			foreach (ZipEntry entry in ZipFile) {
				if (entry.IsDirectory)
					continue;
				new ZipRecord(this, entry);
			}
		}

		internal Stream Open(ZipEntry entry) { return ZipFile.GetInputStream(entry); }
	}

	/// <summary>An entry in a PK-Zip archive file (held with <see cref="ZipArchive"/>).</summary>
	public class ZipRecord : DataAsset {
		/// <summary>Get the containing archive.</summary>
		public ZipArchive ZipArchive {
			get {
				for (Asset asset = Parent; ; asset = asset.Parent)
					if (asset is ZipArchive)
						return (ZipArchive)asset;
			}
		}

		/// <summary>Get the library entry for this zip file entry.</summary>
		public ZipEntry ZipEntry { get; private set; }

		internal ZipRecord(ZipArchive archive, ZipEntry entry)
			: base(archive, entry.Name) {
			ZipEntry = entry;
			MoveIntoPath(Name);
		}

		/// <summary>Open the zip archive entry.</summary>
		/// <returns></returns>
		public override System.IO.Stream Open() {
			Stream stream = ZipArchive.ZipFile.GetInputStream(ZipEntry);
			return new MemoryStream(stream.ReadBytes(checked((int)ZipEntry.Size)), false);
		}
	}

	/// <summary>Provides file format support for the <see cref="ZipArchive"/>.</summary>
	public class ZipFormat : AssetFormat {
		internal ZipFormat(DefaultPlugin plugin)
			: base(plugin, typeof(ZipArchive), canLoad: true, extension: ".zip") {
			IsGeneralUseArchiveFormat = true;
		}

		/// <summary>Matches if the magic number matches ("PK\x03\x04").</summary>
		/// <param name="loader"></param>
		/// <returns></returns>
		public override LoadMatchStrength LoadMatch(AssetLoader loader) {
			return loader.Length > 4 && loader.Reader.ReadInt32() == ZipArchive.Magic ? LoadMatchStrength.Medium : LoadMatchStrength.None;
		}

		/// <summary>
		/// Load the zip archive.
		/// </summary>
		/// <param name="loader"></param>
		/// <returns></returns>
		public override Asset Load(AssetLoader loader) {
			return new ZipArchive(loader);
		}
	}
}
