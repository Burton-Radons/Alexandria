using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Engines.Unreal.Core {
	/// <summary>
	/// 
	/// </summary>
	public class LevelSummary : RootObject {
		/// <summary>
		/// 
		/// </summary>
		[PackageProperty(0)]
		public AttributeDictionary Attributes { get; protected set; }
	}
}
