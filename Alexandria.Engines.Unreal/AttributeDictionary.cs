using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Alexandria.Engines.Unreal {
	/// <summary>
	/// Contains a map of <c>string</c>s to <c>object</c>s.
	/// </summary>
	public class AttributeDictionary : Dictionary<string, object> {
		/// <summary>
		/// Load an <see cref="AttributeDictionary"/> from the <see cref="Package"/> and the <see cref="BinaryReader"/>.
		/// </summary>
		/// <param name="package">The <see cref="Package"/> the dictionary is in.</param>
		/// <param name="reader">The <see cref="BinaryReader"/> that holds the dictionary's data.</param>
		/// <returns>The <see cref="AttributeDictionary"/> or <c>null</c> if there was no entries.</returns>
		public static AttributeDictionary Load(Package package, BinaryReader reader) {
			AttributeDictionary result = null;

			while (true) {
				var name = package.ReadNameIndex(reader).Value;
				if (name == "None")
					break;
				if (result == null)
					result = new AttributeDictionary();
				object current;
				result.TryGetValue(name, out current);
				result[name] = Field.Load(package, reader, current);
			}

			return result;
		}
	}

}
