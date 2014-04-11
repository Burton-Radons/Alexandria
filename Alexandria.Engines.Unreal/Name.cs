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
        public Name(Package package, BinaryReader reader) {
            Package = package;
            if(Package.FileVersion < 64)
                Value = reader.ReadStringz(Encoding.ASCII);
            else {
                var length = reader.ReadByte();
                Value = reader.ReadString(length, Encoding.ASCII);
                if(Value.Length > 0 && Value[Value.Length - 1] == '\0')
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

        public bool Tokey { get { return Flags.HasFlag(NameFlags.Unknown27); } }

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

        Unknown1 = 1,
        Unknown2 = 2,
        Unknown3 = 0x4,
        Unknown4 = 0x8,
        Unknown5 = 0x10,
        Unknown6 = 0x20,
        Unknown7 = 0x40,
        Unknown8 = 0x80,
        Unknown9 = 0x100,
        Unknown10 = 0x200,
        Unknown11 = 0x400,
        Unknown12 = 0x800,
        Unknown13 = 0x1000,
        Unknown14 = 0x2000,
        Unknown15 = 0x4000,
        Unknown16 = 0x8000,
        Unknown17 = 0x10000,
        Unknown18 = 0x20000,
        Unknown19 = 0x40000,
        Unknown20 = 0x80000,
        Unknown21 = 0x100000,
        Unknown22 = 0x200000,
        Unknown23 = 0x400000,
        Unknown24 = 0x800000,
        Unknown25 = 0x1000000,
        Unknown26 = 0x2000000,
        Unknown27 = 0x4000000,
        Unknown28 = 0x8000000,
        Unknown29 = 0x10000000,
        Unknown30 = 0x20000000,
        Unknown31 = 0x40000000,
        Unknown32 = 0x80000000
    }

    /// <summary>
    /// A <see cref="List"/> of <see cref="Name"/>.
    /// </summary>
    public class NameList : List<Name> {
        public NameList() : base() { }
        public NameList(int capacity) : base(capacity) { }
    }
}
