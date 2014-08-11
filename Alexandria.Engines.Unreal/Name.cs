using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Glare.Internal;

namespace Alexandria.Engines.Unreal {
	/// <summary>
	/// A string in a <see cref="Package"/> that can be applied to various purposes.
	/// </summary>
	public class Name {
		/// <summary></summary>
		public Name(Package package, BinaryReader reader) {
			Package = package;
			if (Package.FileVersion < 64)
				Value = reader.ReadStringz(Encoding.ASCII);
			else {
				var length = reader.ReadByte();
				Value = reader.ReadString(length, Encoding.ASCII);
				if (Value.Length > 0 && Value[Value.Length - 1] == '\0')
					Value = Value.Substring(0, Value.Length - 1);
			}
			Flags = (NameFlags)reader.ReadUInt32();
			Index = Package.Names.Count;
		}

		/// <summary>
		/// Package this is in.
		/// </summary>
		public Package Package { get; protected set; }

		/// <summary>
		/// Zero-based index of the name.
		/// </summary>
		public int Index { get; protected set; }

		/// <summary>
		/// Name this contains.
		/// </summary>
		public string Value { get; protected set; }

		/// <summary>
		/// Flags applied to the name.
		/// </summary>
		public NameFlags Flags { get; protected set; }

		/// <summary></summary>
		public bool Tokey { get { return Flags.HasFlag(NameFlags.Unknown27); } }

		/// <summary></summary>
		public override string ToString() {
			return "Name(\"" + Value + "\", Flags: " + Flags + ")";
		}
	}

	/// <summary>
	/// Flags that can be applied to a <see cref="Name"/>.
	/// </summary>
	[Flags]
	public enum NameFlags : uint {
		/// <summary>
		/// No flag value.
		/// </summary>
		None = 0,

		/// <summary></summary>
		Unknown1 = 1,
		/// <summary></summary>
		Unknown2 = 2,
		/// <summary></summary>
		Unknown3 = 0x4,
		/// <summary></summary>
		Unknown4 = 0x8,
		/// <summary></summary>
		Unknown5 = 0x10,
		/// <summary></summary>
		Unknown6 = 0x20,
		/// <summary></summary>
		Unknown7 = 0x40,
		/// <summary></summary>
		Unknown8 = 0x80,
		/// <summary></summary>
		Unknown9 = 0x100,
		/// <summary></summary>
		Unknown10 = 0x200,
		/// <summary></summary>
		Unknown11 = 0x400,
		/// <summary></summary>
		Unknown12 = 0x800,
		/// <summary></summary>
		Unknown13 = 0x1000,
		/// <summary></summary>
		Unknown14 = 0x2000,
		/// <summary></summary>
		Unknown15 = 0x4000,
		/// <summary></summary>
		Unknown16 = 0x8000,
		/// <summary></summary>
		Unknown17 = 0x10000,
		/// <summary></summary>
		Unknown18 = 0x20000,
		/// <summary></summary>
		Unknown19 = 0x40000,
		/// <summary></summary>
		Unknown20 = 0x80000,
		/// <summary></summary>
		Unknown21 = 0x100000,
		/// <summary></summary>
		Unknown22 = 0x200000,
		/// <summary></summary>
		Unknown23 = 0x400000,
		/// <summary></summary>
		Unknown24 = 0x800000,
		/// <summary></summary>
		Unknown25 = 0x1000000,
		/// <summary></summary>
		Unknown26 = 0x2000000,
		/// <summary></summary>
		Unknown27 = 0x4000000,
		/// <summary></summary>
		Unknown28 = 0x8000000,
		/// <summary></summary>
		Unknown29 = 0x10000000,
		/// <summary></summary>
		Unknown30 = 0x20000000,
		/// <summary></summary>
		Unknown31 = 0x40000000,
		/// <summary></summary>
		Unknown32 = 0x80000000
	}

	/// <summary>
	/// A <see cref="List{T}"/> of <see cref="Name"/>.
	/// </summary>
	public class NameList : List<Name> {
		/// <summary></summary>
		public NameList() : base() { }
		/// <summary></summary>
		public NameList(int capacity) : base(capacity) { }
	}
}
