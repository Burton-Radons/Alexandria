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
		/// <summary>Get whether the file exists.</summary>
		/// <param name="path">The path to the file.</param>
		/// <returns></returns>
		public abstract bool Exists(string path);

		/// <summary>Open a file.</summary>
		/// <param name="path"></param>
		/// <param name="mode"></param>
		/// <param name="access"></param>
		/// <param name="share"></param>
		/// <returns></returns>
		public abstract Stream Open(string path, FileMode mode, FileAccess access, FileShare share);

		public Stream TryOpen(string path, FileMode mode, FileAccess access, FileShare share) {
			try {
				return Open(path, mode, access, share);
			} catch (IOException) {
				return null;
			}
		}

		/// <summary>Open an existing file for reading.</summary>
		/// <param name="path"></param>
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

		public BinaryReader OpenReader(string name, ByteOrder byteOrder = ByteOrder.LittleEndian) { return BigEndianBinaryReader.Create(byteOrder, OpenRead(name)); }

		public virtual Asset Load(AssetManager manager, string path, Asset context) {
			Stream stream = OpenRead(path);
			return manager.Load(stream, path, this, context);
		}

		public static readonly FileManager System = new FileSystemContextClass();

		class FileSystemContextClass : FileManager {
			public override bool Exists(string path) {
				return File.Exists(path);
			}

			public override Stream Open(string path, FileMode mode, FileAccess access, FileShare share) {
				return File.Open(path, mode, access, share);
			}
		}
	}
}
