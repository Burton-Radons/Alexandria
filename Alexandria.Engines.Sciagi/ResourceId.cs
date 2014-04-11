using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.Sciagi {
	public struct ResourceId {
		short A;
		int B;

		public int Index { get { return A & 2047; } }

		public bool IsEnd { get { return A == -1 && B == -1; } }

		public int Offset { get { return B & ~(~0 << 26); } }

		public int Page { get { return (B >> 26) & 63; } }

		public ResourceType Type { get { return (ResourceType)(A >> 11); } }

		public ResourceId(BinaryReader reader) {
			A = reader.ReadInt16();
			B = reader.ReadInt32();
		}

		public int GetCombinedIndex(EngineVersion version) {
			if (version <= EngineVersion.SCI0)
				return Index + ((int)Type << 11);
			return Index;
		}

		public override string ToString() {
			return Type + " " + Index;
		}
	}
}
