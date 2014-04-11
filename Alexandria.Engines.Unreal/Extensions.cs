using Glare;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Alexandria.Engines.Unreal {
	public static class Extensions {
		public static T[] ReadIndexCountArray<T>(this BinaryReader reader, Func<T> function, bool nullIfEmpty = false) {
			int count = UIndex.Read(reader);
			if(count == 0 && nullIfEmpty)
				return null;
			var array = new T[count];
			for(int index = 0; index < count; index++)
				array[index] = function();
			return array;
		}

		public static List<T> ReadIndexCountList<T>(this BinaryReader reader, Func<T> function, bool nullIfEmpty = false) {
			int count = UIndex.Read(reader);
			if(count == 0 && nullIfEmpty)
				return null;
			var list = new List<T>(count);
			for(int index = 0; index < count; index++)
				list.Add(function());
			return list;
		}
	}
}
