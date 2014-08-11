using Glare.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.Creation {
	public struct RecordHeader {
		public readonly RecordFlags Flags;

		public readonly FormId Id;

		public bool IsCompressed { get { return (Flags & RecordFlags.Compressed) != 0; } }

		public long EndOffset { get { return StartOffset + Size; } }

		public readonly uint Revision;

		/// <summary>Size of the data in bytes.</summary>
		public readonly uint Size;

		public readonly long StartOffset;

		/// <summary>Type of the record.</summary>
		public readonly string Type;

		public readonly ushort Unknown;

		public readonly ushort Version;

		public RecordHeader(BinaryReader reader) {
			Type = reader.ReadId4();
			Size = reader.ReadUInt32();
			Flags = (RecordFlags)reader.ReadUInt32();
			Id = new FormId(reader.ReadUInt32());
			Revision = reader.ReadUInt32();
			Version = reader.ReadUInt16();
			Unknown = reader.ReadUInt16();
			StartOffset = reader.BaseStream.Position;
		}

		public void RequireType(string type) {
			if (Type != type)
				throw new InvalidDataException("Required a '" + type + "' record type, but received '" + Type + "'.");
		}

		public RecordReader OpenReader(BinaryReader reader) {
			return new RecordReader(this, reader);
		}

		public override string ToString() {
			return string.Format("{0} ({1}, Flags {2}, Revision {3}, Version {4}, Size {5}, Unknown {6})", Type, Id, Flags, Revision, Version, Size, Unknown);
		}

		public Exception UnknownRecordException() {
			return new InvalidDataException(string.Format("Record type '{0}' is not known or invalid.", Type));
		}
	}

}
