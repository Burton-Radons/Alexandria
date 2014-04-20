using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.Sciagi {
	public struct ResourceId : IEquatable<ResourceId> {
		public readonly ResourceType Type;
		public readonly int Index;
		public readonly int Offset;
		public readonly int Page;
		public ResourceMapVersion Version;

		public int CombinedIndex {
			get {
				if (Version <= ResourceMapVersion.Sci0)
					return Index + ((int)Type << 11);
				return Index;
			}
		}

		public bool IsEnd { get { return Type == ResourceType.End; } }

		public ResourceId(BinaryReader reader, ResourceMapVersion version, ResourceType? type = null) {
			Version = version;
			if (version == ResourceMapVersion.Sci0) {
				short a = reader.ReadInt16();
				int b = reader.ReadInt32();

				if (type.HasValue)
					throw new InvalidDataException();
				Type = (ResourceType)(a >> 11);
				Index = a & 2047;
				Offset = b & ~(~0 << 26);
				Page = (b >> 26) & 63;
				if (a == -1 && b == -1)
					Type = ResourceType.End;
			} else if(version == ResourceMapVersion.Sci1) {
				Type = type.Value;
				Index = reader.ReadUInt16();
				int b = reader.ReadInt32();
				Offset = b & ~(~0 << 28);
				Page = (b >> 28) & 15;
			} else
				throw new NotImplementedException();
		}

		public bool Equals(ResourceId other) {
			return Index == other.Index && Type == other.Type;
		}

		public override bool Equals(object obj) {
			if (obj is ResourceId)
				return Equals((ResourceId)obj);
			return base.Equals(obj);
		}

		public override int GetHashCode() {
			return Type.GetHashCode() ^ Index.GetHashCode();
		}

		public override string ToString() {
			return Type + " " + Index;
		}

		public static bool operator ==(ResourceId a, ResourceId b) { return a.Index == b.Index && a.Type == b.Type; }
		public static bool operator !=(ResourceId a, ResourceId b) { return a.Index != b.Index || a.Type != b.Type; }
	}
}
