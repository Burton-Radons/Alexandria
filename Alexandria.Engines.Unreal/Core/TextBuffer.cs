using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Engines.Unreal.Core {
	public class TextBuffer : RootObject {
		[PackageProperty(0)]
		public AttributeDictionary Attributes { get; protected set; }

		[PackageProperty(1)]
		public int Position { get; protected set; }

		[PackageProperty(2)]
		public int Top { get; protected set; }

		[PackageProperty(3, typeof(DataProcessors.AsciizIndexLength))]
		public string Data { get; protected set; }
	}
}
