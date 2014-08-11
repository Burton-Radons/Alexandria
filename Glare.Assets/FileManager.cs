using Glare.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Assets {
	/// <summary>
	/// A file context allows saving, opening, and creating files.
	/// </summary>
	public abstract class FileManager {
		/// <summary>Get an identifier that uniquely identifies this file manager's file space. For example, if a zip file is in the file system, it could be a combination of the file name of the zip file and the file system identifier. Compound identifiers like that should be separated by newlines.</summary>
		public abstract string Id { get; }

		/// <summary>Get whether the file exists.</summary>
		/// <param name="path">The path to the file.</param>
		/// <returns></returns>
		public abstract bool Exists(string path);

		/// <summary>List the files within the path.</summary>
		/// <param name="path"></param>
		/// <param name="pattern"></param>
		/// <param name="option"></param>
		/// <returns></returns>
		public virtual string[] GetFiles(string path, string pattern, SearchOption option = SearchOption.TopDirectoryOnly) {
			throw new NotImplementedException();
		}

		/// <summary>Open a file.</summary>
		/// <param name="path"></param>
		/// <param name="mode"></param>
		/// <param name="access"></param>
		/// <param name="share"></param>
		/// <returns></returns>
		public abstract Stream Open(string path, FileMode mode, FileAccess access, FileShare share);

		/// <summary>Attempt to open a file.</summary>
		/// <param name="path"></param>
		/// <param name="mode"></param>
		/// <param name="access"></param>
		/// <param name="share"></param>
		/// <returns></returns>
		public Stream TryOpen(string path, FileMode mode, FileAccess access, FileShare share) {
			try {
				return Open(path, mode, access, share);
			} catch (IOException) {
				return null;
			}
		}

		/// <summary>Open an existing file for reading.</summary>
		/// <param name="path"></param>
		/// <param name="share"></param>
		/// <returns></returns>
		public Stream OpenRead(string path, FileShare share = FileShare.Delete | FileShare.ReadWrite) { return Open(path, FileMode.Open, FileAccess.Read, share); }

		/// <summary>Attempt to open a file for reading.</summary>
		/// <param name="path"></param>
		/// <param name="share"></param>
		/// <returns>The <see cref="Stream"/> or <c>null</c> if it could not be opened.</returns>
		public Stream TryOpenRead(string path, FileShare share = FileShare.Delete | FileShare.ReadWrite) { return TryOpen(path, FileMode.Open, FileAccess.Read, share); }


		/// <summary>Open an existing or new file for writing. <see cref="OpenCreate"/> can be used to only create files.</summary>
		/// <param name="path"></param>
		/// <param name="share"></param>
		/// <returns></returns>
		public Stream OpenWrite(string path, FileShare share = FileShare.ReadWrite) { return Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, share); }

		/// <summary>Open a new file for writing; if the file already exists, delete it first. <see cref="OpenWrite"/> can be used to open the file if it already exists but create it otherwise.</summary>
		/// <param name="path"></param>
		/// <param name="share"></param>
		/// <returns></returns>
		public Stream OpenCreate(string path, FileShare share = FileShare.ReadWrite) { return Open(path, FileMode.Create, FileAccess.ReadWrite, share); }

		/// <summary>Open an existing file.</summary>
		/// <param name="name"></param>
		/// <param name="byteOrder"></param>
		/// <param name="share"></param>
		/// <returns></returns>
		public BinaryReader OpenReader(string name, ByteOrder byteOrder = ByteOrder.LittleEndian, FileShare share = FileShare.Delete | FileShare.ReadWrite) { return BigEndianBinaryReader.Create(byteOrder, OpenRead(name, share)); }

		/// <summary>Load an <see cref="Asset"/> using this <see cref="FileManager"/>.</summary>
		/// <param name="manager"></param>
		/// <param name="path"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual Asset Load(AssetManager manager, string path, Asset context = null) {
			Stream stream = OpenRead(path);
			return manager.Load(stream, path, this, context);
		}

		/// <summary>Get the system file manager for opening files using standard IO.</summary>
		public static readonly FileManager System = new FileSystemContextClass();

		/// <summary>Get a <see cref="FileManager"/> that does not support any files. This is necessary, as passing <c>null</c> to loading operations normally causes a default selection of <see cref="System"/>.</summary>
		public static readonly FileManager Null = new NullFileManager();

		class NullFileManager : FileManager {
			public override string Id { get { return "Null"; } }

			public override bool Exists(string path) { return false; }

			public override string[] GetFiles(string path, string pattern, SearchOption option = SearchOption.TopDirectoryOnly) {
				throw new NotSupportedException("There is no such path; this is a null file manager.");
			}

			public override Stream Open(string path, FileMode mode, FileAccess access, FileShare share) {
				throw new NotSupportedException("This is a null file manager.");
			}
		}

		class FileSystemContextClass : FileManager {
			public override string Id { get { return GetType().Name; } }

			public override bool Exists(string path) {
				return File.Exists(path);
			}

			public override string[] GetFiles(string path, string pattern, SearchOption option = SearchOption.TopDirectoryOnly) {
				return Directory.GetFiles(path, pattern, option);
			}

			public override Stream Open(string path, FileMode mode, FileAccess access, FileShare share) {
				return File.Open(path, mode, access, share);
			}
		}
	}
}
