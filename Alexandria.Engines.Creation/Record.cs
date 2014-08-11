using Alexandria.Engines.Creation.Records;
using Glare.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.Creation {
	/// <summary>Base class of all <see cref="Module"/> records.</summary>
	public abstract class Record : FolderAsset {
		public override string DisplayName { get { return Header.ToString(); } }

		public RecordHeader Header { get; private set; }

		internal Record(Module module, RecordReader reader)
			: base(module.Manager, "") {
			Header = reader.Record;
			Name = Header.Type;
		}

		internal static Record Read(Module module, RecordReader reader) {
			switch (reader.Record.Type) {
				default: return new UnknownRecord(module, reader);
			}
		}

		public static Type GetRecordType(string type) {
			switch (type) {
				default: return null;
			}
		}
	}
}
