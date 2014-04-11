using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Alexandria.Engines.Unreal.Core;
using SCG = System.Collections.Generic;
using Glare.Internal;

namespace Alexandria.Engines.Unreal {
	/// <summary>
	/// A function for loading and saving data. This is used by the <see cref="PackagePropertyAttribute"/>.
	/// </summary>
	public abstract class DataProcessor {
		public abstract object Read(object target, Package package, BinaryReader reader, long end);
	}

	/// <summary>
	/// Contains a set of alternate <see cref="DataProcessor"/> types.
	/// </summary>
	public static class DataProcessors {
		/// <summary>
		/// Reads a <c>string</c> as a <see cref="UIndex"/> followed by the given number of ASCII characters, terminated with a NUL.
		/// </summary>
		public class AsciizIndexLength : DataProcessor {
			public override object Read(object target, Package package, BinaryReader reader, long end) {
				int length = UIndex.Read(reader);
				return reader.ReadStringz(length, Encoding.ASCII);
			}
		}

		/// <summary>
		/// Reads a <c>string</c> as a <c>byte</c> followed by the given number of ASCII characters, terminated with a NUL.
		/// </summary>
		public class AsciizByteLength : DataProcessor {
			public override object Read(object target, Package package, BinaryReader reader, long end) {
				var length = reader.ReadByte();
				return reader.ReadStringz(length, Encoding.ASCII);
			}
		}

		/// <summary>
		/// Reads a list of objects where the count is encoded as an index.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		public class List<T> : DataProcessor where T : RootObject, new() {
			public override object Read(object target, Package package, BinaryReader reader, long end) {
				int count = UIndex.Read(reader);

				if (count == 0)
					return null;
				SCG.List<T> result = new SCG.List<T>(count);

				for (var index = 0; index < count; index++) {
					var value = new T();
					value.Package = package;
					value.Load(reader, end);
					result.Add(value);
				}

				return result;
			}
		}

		/// <summary>
		/// Reads a list of objects where the count is encoded as a 32-bit integer.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		public class ListInt32Count<T> : DataProcessor where T : RootObject, new() {
			public override object Read(object target, Package package, BinaryReader reader, long end) {
				int count = reader.ReadInt32();

				if (count == 0)
					return null;
				var result = new SCG.List<T>(count);

				for (var index = 0; index < count; index++) {
					var value = new T();
					value.Package = package;
					value.Load(reader, end);
					result.Add(value);
				}

				return result;
			}
		}

		/// <summary>
		/// Reads a 32-bit int as an index into the package's name table, returning a string.
		/// </summary>
		public class IntNameIndex : DataProcessor {
			public override object Read(object target, Package package, BinaryReader reader, long end) {
				return package.Names[reader.ReadInt32()].Value;
			}
		}

		/// <summary>
		/// Reads an int as a <see cref="UIndex"/>.
		/// </summary>
		public class IndexInt : DataProcessor {
			public override object Read(object target, Package package, BinaryReader reader, long end) {
				return (int)UIndex.Read(reader);
			}
		}

		/// <summary>
		/// A <c>List&lt;Reference&gt;</c> reader with a <c>int</c> count.
		/// </summary>
		public class IntCountReferenceList : DataProcessor {
			public override object Read(object target, Package package, System.IO.BinaryReader reader, long end) {
				var count = reader.ReadInt32();
				var list = new SCG.List<Reference>(count);

				while (count-- > 0)
					list.Add(package.ReadReference(reader));
				return list;
			}
		}

		/// <summary>
		/// Reads an object embedded in another object.
		/// </summary>
		public class LiteralObject<T> : DataProcessor where T : RootObject, new() {
			public override object Read(object target, Package package, System.IO.BinaryReader reader, long end) {
				var result = new T();
				result.Load(package, reader, end);
				return result;
			}
		}

		/// <summary>
		/// A null-terminated ascii <c>string</c>.
		/// </summary>
		public class NulTerminatedAscii : DataProcessor {
			public override object Read(object target, Package package, System.IO.BinaryReader reader, long end) {
				return reader.ReadStringz(Encoding.ASCII);
			}
		}

		/// <summary>
		/// Reads a <see cref="UIndex"/>, returning it as <c>int</c>. It must be zero, or else there will be an exception.
		/// This is for properties that are known to be arrays, but have always been empty.
		/// </summary>
		public class UnknownIndex : DataProcessor {
			public override object Read(object target, Package package, System.IO.BinaryReader reader, long end) {
				int count = UIndex.Read(reader);

				if (count != 0)
					throw new Exception();
				return count;
			}
		}

		public class ZeroInt32 : DataProcessor {
			public override object Read(object target, Package package, BinaryReader reader, long end) {
				int value = reader.ReadInt32();

				if (value != 0)
					throw new Exception();
				return value;
			}
		}
	}
}
