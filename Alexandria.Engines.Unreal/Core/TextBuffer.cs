using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Engines.Unreal.Core {
	/// <summary>A text buffer object.</summary>
	public class TextBuffer : RootObject {
		/// <summary>Get the attributes.</summary>
		[PackageProperty(0)]
		public AttributeDictionary Attributes { get; protected set; }

		/// <summary>Get the position.</summary>
		[PackageProperty(1)]
		public int Position { get; protected set; }

		/// <summary>Get the top index.</summary>
		[PackageProperty(2)]
		public int Top { get; protected set; }

		/// <summary>Get the text data.</summary>
		[PackageProperty(3, typeof(DataProcessors.AsciizIndexLength))]
		public string Data { get; protected set; }
	}
}
