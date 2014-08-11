using Glare.Assets;
using Glare.Assets.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Glare.Assets {
	/// <summary>
	/// A fallback for loading <see cref="Asset"/>s that just contains binary data.
	/// </summary>
	public class BinaryAsset : Asset {
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly int count;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly IList<byte> data;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly int displayOffset;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly int start;

		/// <summary>Get the binary data for the asset.</summary>
		public IList<byte> Data { get { return data; } }

		/// <summary>Get the number of bytes in the data.</summary>
		public int DataCount { get { return count; } }

		/// <summary>Get the offset to show as the first byte in display.</summary>
		public int DataDisplayOffset { get { return displayOffset; } }

		/// <summary>Get the offset in <see cref="Data"/> for the first byte of the data.</summary>
		public int DataStart { get { return start; } }

		/// <summary>Get a byte of the data.</summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public byte this[int index] {
			get {
				if (index < 0 || index >= count)
					throw new IndexOutOfRangeException();
				return data[start + index];
			}
		}

		/// <summary>Initialise the asset.</summary>
		/// <param name="manager"></param>
		/// <param name="name"></param>
		/// <param name="data"></param>
		/// <param name="displayOffset"></param>
		public BinaryAsset(AssetManager manager, string name, IList<byte> data, int displayOffset = 0)
			: this(manager, name, data, 0, data.Count, displayOffset) { }

		/// <summary>Initialise the asset.</summary>
		/// <param name="manager"></param>
		/// <param name="name"></param>
		/// <param name="data"></param>
		/// <param name="start"></param>
		/// <param name="count"></param>
		/// <param name="displayOffset"></param>
		public BinaryAsset(AssetManager manager, string name, IList<byte> data, int start, int count, int displayOffset = 0)
			: base(manager, name) {
			this.data = data;
			this.start = start;
			this.count = count;
			this.displayOffset = displayOffset;
		}

		/// <summary>Make a hex browser for the data.</summary>
		/// <returns></returns>
		public override Control Browse(Action<double> progressUpdateCallback = null) {
			return new BinaryAssetBrowser(this);
		}
	}
}
