using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glare.Internal;

namespace Alexandria.Platforms.NintendoDS {
	public class Rom : Resource {
		BinaryReader Reader;

		/// <summary>
		/// The title of the game, found at the beginning of the ROM.
		/// </summary>
		public String GameTitle { get { return Reader.ReadStringzAt(0, 12, Encoding.ASCII); } }

		public String GameCode { get { return Reader.ReadStringzAt(0x0C, 4, Encoding.ASCII); } }

		public String MakerCode { get { return Reader.ReadStringzAt(0x010, 2, Encoding.ASCII); } }

		public int UnitCode { get { return Reader.ReadByteAt(0x012); } }
		public int DeviceCode { get { return Reader.ReadByteAt(0x013); } }

		public byte CardSizeBase { get { return Reader.ReadByteAt(0x014); } }
		public int CardSize { get { return 2 << (20 + CardSizeBase); } }

		public int FileNameTableOffset { get { return Reader.ReadInt32At(0x040); } }
		public int FileNameTableSize { get { return Reader.ReadInt32At(0x044); } }
		public int FileSizeTableOffset { get { return Reader.ReadInt32At(0x048); } }
		public int FileSizeTableSize { get { return Reader.ReadInt32At(0x04C); } }

		public Rom(Manager manager, string path) : this(manager, path, File.OpenRead(path)) { }

		public Rom(Manager manager, string name, Stream stream) : this(manager, name, new BinaryReader(stream)) { }

		public Rom(Manager manager, string name, BinaryReader reader) : base(manager, name) {
			Reader = reader;
		}
	}
}
