using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.Engines.Creation.Groups {
	/// <summary>A <see cref="TopGroup"/> that contains <see cref="Record"/>s.</summary>
	public class RecordTopGroup : TopGroup {
		internal RecordTopGroup(Module module, uint contentSize, uint label) : base(module, contentSize, label) { }

		protected override void LoadChildrenBase() {
			Module module = Module;
			lock (module) {
				BinaryReader reader = module.Reader;
				reader.BaseStream.Position = ContentOffset;
				while (reader.BaseStream.Position < ContentEnd) {
					var recordHeader = new RecordHeader(reader);
					var recordReader = new RecordReader(recordHeader, reader);
					var record = Record.Read(module, recordReader);
					AddChild(record);
				}
			}
		}
	}
}
