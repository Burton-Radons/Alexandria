using Glare.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.Creation {
	/// <summary>
	/// A field in a <see cref="ModuleRecord"/>.
	/// </summary>
	public struct RecordField {
		public long EndOffset { get { return StartOffset + Size; } }

		public readonly ushort Size;

		public readonly long StartOffset;

		public readonly string Type;

		public RecordField(BinaryReader reader) {
			if (reader.BaseStream.Position + 8 >= reader.BaseStream.Length) {
			}
			Type = reader.ReadId4();
			Size = reader.ReadUInt16();
			StartOffset = reader.BaseStream.Position;
		}
	}
}
