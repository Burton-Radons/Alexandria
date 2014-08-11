using Glare.Assets;
using Glare.Assets.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alexandria.Engines.Creation.Records {
	public class UnknownRecord : Record {
		internal UnknownRecord(Module module, RecordReader reader)
			: base(module, reader) {
			while (reader.ReadField()) {
				new UnknownRecordField(this, ref reader);
			}
		}
	}

	public class UnknownRecordField : Asset {
		public byte[] Data { get; private set; }

		public override string DisplayName { get { return string.Format("{0} ({1} byte(s))", Name, Data); } }

		internal UnknownRecordField(UnknownRecord record, ref RecordReader reader)
			: base(record, reader.Field.Type) {
			RecordField field = reader.Field;

			Data = reader.Reader.ReadBytes(field.Size);
		}

		public override Control Browse(Action<double> progressUpdateCallback = null) {
			return new BinaryAssetBrowser(Data, 0, Data.Length);
		}
	}
}
