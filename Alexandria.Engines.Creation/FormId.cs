using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.Creation {
	public struct FormId : IEquatable<FormId> {
		readonly uint Id;

		public FormId(BinaryReader reader) : this(reader.ReadUInt32()) { }
		public FormId(int id) : this((uint)id) { }
		public FormId(uint id) { Id = id; }

		public bool Equals(FormId other) { return Id == other.Id; }

		public override bool Equals(object obj) {
			if (obj is FormId)
				return ((FormId)obj).Id == Id;
			return base.Equals(obj);
		}

		public override int GetHashCode() { return Id.GetHashCode(); }

		public override string ToString() { return string.Format("Id#{0:X08}", Id); }

		public static bool operator ==(FormId a, FormId b) { return a.Id == b.Id; }
		public static bool operator !=(FormId a, FormId b) { return a.Id != b.Id; }
	}
}
