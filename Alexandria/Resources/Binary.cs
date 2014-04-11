using Alexandria.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alexandria.Resources {
	public class Binary : Resource {
		readonly int count;
		readonly IList<byte> data;
		readonly int displayOffset;
		readonly int start;

		public IList<byte> Data { get { return data; } }

		public int DataCount { get { return count; } }

		public int DataDisplayOffset { get { return displayOffset; } }

		public int DataStart { get { return start; } }

		public byte this[int index] {
			get {
				if (index < 0 || index >= count)
					throw new IndexOutOfRangeException();
				return data[start + index];
			}
		}

		public Binary(Manager manager, string name, IList<byte> data, int displayOffset = 0)
			: this(manager, name, data, 0, data.Count, displayOffset) { }

		public Binary(Manager manager, string name, IList<byte> data, int start, int count, int displayOffset = 0)
			: base(manager, name) {
			this.data = data;
			this.start = start;
			this.count = count;
			this.displayOffset = displayOffset;
		}

		public override Control Browse() {
			return new BinaryResourceBrowser(this);
		}
	}
}
