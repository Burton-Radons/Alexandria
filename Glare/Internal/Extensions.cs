using Glare.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Internal {
	public static partial class Extensions {
		/// <summary>Find a constructor with no arguments in the type and invoke it, returning the new object. It can be public or private.</summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException">The type does not have a constructor without arguments.</exception>
		public static T Construct<T>() {
			Type type = typeof(T);
			ConstructorInfo constructor = type.GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, Type.EmptyTypes, null);
			if (constructor == null)
				throw new InvalidOperationException("Type " + type.Name + " does not have a constructor with no arguments.");
			return (T)constructor.Invoke(null);
		}

		public static T[] CreateArray<T>(T value, int count) {
			T[] result = new T[count];
			for (int index = 0; index < count; index++)
				result[index] = value;
			return result;
		}

		public static int GetSmallerIndex(int a, int b) { return a < b ? (a < 0 ? b : a) : b < 0 ? a : b; }

		public static void Swap<T>(ref T a, ref T b) { T c = a; a = b; b = c; }
	}

	public static partial class ExtensionMethods {
		#region Shared by extensions

		struct ArrayBuffer<T> {
			T[] Value;

			public ArrayBuffer(int initial) { Value = new T[initial]; }

			static int Expand(int count) { return count * 3 / 2; }

			public T[] Get(int required) {
				if (Value == null || Value.Length < required)
					Value = new T[Expand(required)];
				return Value;
			}

			public T[] Ensure(int current, int count) {
				if (Value == null) {
					if(current != 0)
						throw new ArgumentException("current");
					Value = new T[Expand(current + count)];
				} else if (current + count > Value.Length) {
					T[] newArray = new T[Expand(current + count)];
					Array.Copy(Value, 0, newArray, 0, current);
					Value = newArray;
				}
				return Value;
			}
		}

		[ThreadStatic]
		static ArrayBuffer<byte> SharedBytes;

		[ThreadStatic]
		static ArrayBuffer<char> SharedChars;

		[ThreadStatic]
		static Dictionary<Encoding, Decoder> SharedDecoders;

		[ThreadStatic]
		static byte[] __SharedBytes4;

		static byte[] GetSharedBytes4() { return __SharedBytes4 ?? (__SharedBytes4 = new byte[4]); }

		static Decoder GetSharedDecoder(Encoding encoding) {
			Decoder decoder;

			if (SharedDecoders == null)
				SharedDecoders = new Dictionary<Encoding, Decoder>();
			if (SharedDecoders.TryGetValue(encoding, out decoder))
				decoder.Reset();
			else {
				decoder = encoding.GetDecoder();
				if (encoding.CodePage > 0)
					SharedDecoders[encoding] = decoder;
			}

			return decoder;
		}

		static byte[] ReadSharedBytes(this BinaryReader reader, int count, out int read) {
			byte[] bytes = SharedBytes.Get(count);
			read = reader.Read(bytes, 0, count);
			return bytes;
		}

		static byte[] ReadSharedBytes(this Stream stream, int count, out int read) {
			byte[] bytes = SharedBytes.Get(count);
			read = stream.Read(bytes, 0, count);
			return bytes;
		}

		static Encoding CheckEncodingEndian(BinaryReader reader, Encoding encoding) {
			if (reader is BigEndianBinaryReader) {
				if (encoding == Encoding.Unicode)
					return Encoding.BigEndianUnicode;
				else if (encoding == Encoding.BigEndianUnicode)
					return Encoding.Unicode;
				else if (encoding == Encoding.UTF32)
					throw new NotImplementedException();
			}
			return encoding;
		}

		#endregion Shared by extensions

		#region BinaryReader

		public static bool MatchMagic(this BinaryReader reader, string magic) {
			var compare = reader.ReadString(magic.Length, Encoding.ASCII);
			return compare == magic;
		}

		public static string ReadString(this BinaryReader reader, int byteCount, Encoding encoding) {
			byte[] bytes = ReadSharedBytes(reader, byteCount, out byteCount);
			encoding = CheckEncodingEndian(reader, encoding);
			return encoding.GetString(bytes, 0, byteCount);
		}

		/// <summary>Read a string terminated with a '\0' character.</summary>
		/// <param name="reader"></param>
		/// <param name="encoding"></param>
		/// <returns></returns>
		public static string ReadStringz(this BinaryReader reader, Encoding encoding) {
			Decoder decoder = GetSharedDecoder(encoding);
			int charCount = 0;
			byte[] bytes = SharedBytes.Get(16);
			char[] chars = SharedChars.Get(16);

			encoding = CheckEncodingEndian(reader, encoding);

			while (true) {
				int byteCount = 0;

				chars = SharedChars.Ensure(charCount, 16);

				do bytes[byteCount++] = reader.ReadByte();
				while (decoder.GetCharCount(bytes, 0, byteCount, false) == 0);

				int addCount = decoder.GetChars(bytes, 0, byteCount, chars, charCount, false);
				for (int count = 0; count < addCount; count++, charCount++)
					if (chars[charCount] == 0)
						return new String(chars, 0, charCount);
			}
		}

		/// <summary>Read a string that is always the same number of bytes, but may have a terminator in it that makes it shorter.</summary>
		/// <param name="reader">The <see cref="BinaryReader"/> to read the string from.</param>
		/// <param name="byteCount">The number of bytes in the string.</param>
		/// <param name="encoding">The encoding to read the string with.</param>
		/// <param name="stopValue1">The first character to stop the string with. The default is '\0' (the NUL terminator).</param>
		/// <param name="stopValue2">The second character to stop the string with. The default is '\0' (the NUL terminator).</param>
		/// <returns></returns>
		public static string ReadStringz(this BinaryReader reader, int byteCount, Encoding encoding, char stopValue1 = '\0', char stopValue2 = '\0') {
			Decoder decoder = GetSharedDecoder(encoding);
			byte[] bytes = reader.ReadSharedBytes(byteCount, out byteCount);
			char[] chars = SharedChars.Get(encoding.GetMaxCharCount(byteCount));
			int charCount = decoder.GetChars(bytes, 0, byteCount, chars, 0);
			int charLength;

			for (charLength = 0; charLength < charCount; charLength++)
				if (chars[charLength] == stopValue1 || chars[charLength] == stopValue2)
					break;
			return new string(chars, 0, charLength);
		}

		public static string ReadStringzAt(this BinaryReader reader, long offset, Encoding encoding) {
			reader.BaseStream.Position = offset;
			return reader.ReadStringz(encoding);
		}

		public static string ReadStringzAt(this BinaryReader reader, long offset, int maxByteLength, Encoding encoding) {
			reader.BaseStream.Position = offset;
			return reader.ReadStringz(maxByteLength, encoding);
		}

		public static string ReadStringzAtUInt32(this BinaryReader reader, Encoding encoding) { return reader.ReadStringzAtUInt32(ByteOrder.LittleEndian, encoding); }

		/// <summary>Read a <see cref="UInt32"/> offset, read the NUL-terminated string at the offset (or just return <c>null</c> if the offset is 0), and then return the stream position to just after the <see cref="UInt32"/>.</summary>
		/// <param name="reader"></param>
		/// <param name="byteOrder"></param>
		/// <param name="encoding"></param>
		/// <returns></returns>
		public static string ReadStringzAtUInt32(this BinaryReader reader, ByteOrder byteOrder, Encoding encoding) {
			uint offset = reader.ReadUInt32(byteOrder);
			if (offset == 0)
				return null;

			long reset = reader.BaseStream.Position;
			string result = ReadStringzAt(reader, offset, encoding);
			reader.BaseStream.Position = reset;
			return result;
		}

		public static void RequireBE(this BinaryReader reader, int value) {
			int read = reader.ReadInt32BE();
			if (read != value)
				throw new Exception();
		}

		public static void RequireMagic(this BinaryReader reader, string magic) {
			if (!reader.MatchMagic(magic))
				throw new Exception();
		}

		public static void RequireZeroes(this BinaryReader reader, int count) {
			for (int index = 0, value; index < count; index++)
				if ((value = reader.ReadByte()) != 0)
					throw new Exception();
		}

		public static int TryReadInt32(this BinaryReader reader) { return reader.BaseStream.TryReadInt32(0); }

		public static void WriteToFile(this BinaryReader reader, string path) {
			long reset = reader.BaseStream.Position;
			reader.BaseStream.Position = 0;
			using (var stream = File.Open(path, FileMode.Create))
				reader.BaseStream.CopyTo(stream);
			reader.BaseStream.Position = reset;
		}

		#endregion BinaryReader

		#region Arrays

		public static void AddRange<T>(this ICollection<T> list, IEnumerable<T> collection) {
			foreach (T item in collection)
				list.Add(item);
		}

		public static void CheckRange(this Array data, int first, int count) {
			if (data == null)
				throw new ArgumentNullException("data");
			if (first < 0 || first > data.Length)
				throw new ArgumentOutOfRangeException("first");
			if (count < 0 || first + count > data.Length)
				throw new ArgumentOutOfRangeException("count");
		}

		public static int CopyTo<T>(this T[] array, int first, int count, byte[] output, int outputOffset) {
			int typeSize = Marshal.SizeOf(typeof(T));

			if (array == null)
				throw new ArgumentNullException("array");
			if (first < 0 || first > array.Length)
				throw new ArgumentOutOfRangeException("first");
			if (count < 0 || first + count > array.Length)
				throw new ArgumentOutOfRangeException("count");
			if (output == null)
				throw new ArgumentNullException("output");
			if (outputOffset < 0 || outputOffset + typeSize * count > output.Length)
				throw new ArgumentOutOfRangeException("outputOffset");

			GCHandle handle = GCHandle.Alloc(array, GCHandleType.Pinned);

			try {
				IntPtr address = Marshal.UnsafeAddrOfPinnedArrayElement(array, first);
				Marshal.Copy(address, output, outputOffset, typeSize * count);
			} finally {
				handle.Free();
			}
			return typeSize * count;
		}

		/// <summary>Pin the array at the given index, returning the pin handle that can be disposed (and therefore this can be used in a <c>using</c> block), and outputting a pointer to the indexed item.</summary>
		/// <param name="array">The array to pin.</param>
		/// <param name="pointer">Receives the <see cref="IntPtr"/> to the array at the given index.</param>
		/// <param name="index">The index of the item in the array to pin.</param>
		/// <returns>A <see cref="GCPinnedHandle"/> for the pinned array, that must be disposed with <see cref="GCPinnedHandle.Dispose"/>() when done.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="array"/> is <c>null</c>.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is out of the range (0 &lt;= <paramref name="index"/> &lt; <paramref name="array"/>.<see cref="Array.Length"/>.</exception>
		public static GCPinnedHandle Pin(this Array array, out IntPtr pointer, int index = 0) {
			if (array == null)
				throw new ArgumentNullException("array");
			if (index < 0 || index > array.Length)
				throw new ArgumentOutOfRangeException("index");
			var handle = new GCPinnedHandle(array);
			pointer = Marshal.UnsafeAddrOfPinnedArrayElement(array, index);
			return handle;
		}

		/// <summary>Pin the array at the given index, returning the pin handle that can be disposed (and therefore this can be used in a <c>using</c> block), and outputting a pointer to the indexed item.</summary>
		/// <param name="array">The array to pin.</param>
		/// <param name="pointer">Receives the <see cref="IntPtr"/> to the array at the given index.</param>
		/// <param name="first">The index of the first item in the array to pin.</param>
		/// <returns>A <see cref="GCPinnedHandle"/> for the pinned array, that must be disposed with <see cref="GCPinnedHandle.Dispose"/>() when done.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="array"/> is <c>null</c>.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="first"/> is out of the range (0 &lt;= <paramref name="first"/> &lt;= <paramref name="array"/>.<see cref="Array.Length"/>.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is out of the range (0 &lt;= <paramref name="count"/> &lt;= <paramref name="array"/>.<see cref="Array.Length"/> - <paramref name="first"/>.</exception>
		public static GCPinnedHandle Pin(this Array value, out IntPtr pointer, int first, int count) {
			value.CheckRange(first, count);
			return value.Pin(out pointer, first);
		}

		/// <summary>Sort the list in place.</summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <param name="comparison"></param>
		public static IList<T> Sort<T>(this IList<T> list, Comparison<T> comparison) {
			return list.Sort(0, list.Count, comparison);
		}

		/// <summary>Sort a section of the list in place.</summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <param name="first"></param>
		/// <param name="count"></param>
		/// <param name="comparison"></param>
		public static IList<T> Sort<T>(this IList<T> list, int first, int count, Comparison<T> comparison) {
			if (list == null)
				throw new ArgumentNullException("list");
			if (comparison == null)
				throw new ArgumentNullException("comparison");
			if (first < 0 || first > list.Count)
				throw new ArgumentOutOfRangeException("first");
			if (count < 0 || first + count > list.Count)
				throw new ArgumentOutOfRangeException("count");

			list.SortQuick(first, first + count - 1, comparison);
			return list;
		}

		static void SortQuick<T>(this IList<T> list, int left, int right, Comparison<T> comparison) {
			if (left >= right)
				return;

			// Choose a pivot index such that left <= pivotIndex <= right
			int pivotIndex = left + (right - left) / 2; // Avoid integer overflow.

			// Get lists of bigger and smaller items and final position of pivot.
			int pivotNewIndex = list.SortPartition(left, right, pivotIndex, comparison);

			// Recursively sort elements smaller than the pivot (assuming pivotNewIndex - 1 does not underflow).
			list.SortQuick(left, pivotNewIndex - 1, comparison);

			// Recursively sort elements at least as big as the pivot (assume pivotNewIndex + 1 does not overflow).
			list.SortQuick(pivotNewIndex + 1, right, comparison);
		}

		/// <summary></summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <param name="left">Leftmost element of the subarray.</param>
		/// <param name="right">Rightmost element of the subarray, inclusive.</param>
		/// <param name="pivotIndex"></param>
		static int SortPartition<T>(this IList<T> list, int left, int right, int pivotIndex, Comparison<T> comparison) {
			T pivotValue = list[pivotIndex];

			// Move pivot to end by swapping list[pivotIndex] and list[right].
			list[pivotIndex] = list[right];
			list[right] = pivotValue;

			int storeIndex = left;
			for (int index = left; index < right; index++) {
				T indexValue = list[index];

				if (comparison(indexValue, pivotValue) <= 0) {
					// Swap list[index] and list[storeIndex]
					list[index] = list[storeIndex];
					list[storeIndex] = indexValue;
					storeIndex++;
				}
			}

			// Move pivot to its final place.
			T storeValue = list[storeIndex];
			list[storeIndex] = list[right];
			list[right] = storeValue;

			return storeIndex;
		}

		/// <summary>If the index is within the list, return it; otherwise return a default value.</summary>
		/// <typeparam name="T">The type of the elements of the list.</typeparam>
		/// <param name="list">The list to index.</param>
		/// <param name="index">The index to attempt.</param>
		/// <param name="defaultValue">The default value to return if the index is out of range.</param>
		/// <returns>The indexed list value or the default value.</returns>
		public static T TryGet<T>(this IList<T> list, int index, T defaultValue = default(T)) {
			return (index < 0 || index >= list.Count) ? defaultValue : list[index];
		}

		/// <summary>If the index is within the list, assign it; otherwise do nothing.</summary>
		/// <typeparam name="T">The type of the elements of the list.</typeparam>
		/// <param name="list">The list to assign.</param>
		/// <param name="index">The index to attempt to assign to.</param>
		/// <param name="value">The value to attempt to assign.</param>
		/// <returns>Whether the value was assigned.</returns>
		public static bool TrySet<T>(this IList<T> list, int index, T value) {
			if (index >= 0 && index < list.Count) {
				list[index] = value;
				return true;
			}
			return false;
		}

		#endregion Arrays

		#region BinaryReader

		public static byte[] ReadAllBytes(this BinaryReader reader) { return reader.BaseStream.ReadAllBytes(); }

		#endregion BinaryReader

		#region Byte lists

		public static byte ReadByte(this IList<byte> list, int offset, int endOffset) { return offset < endOffset ? list[offset] : (byte)0; }
		public static sbyte ReadSByte(this IList<byte> list, int offset, int endOffset) { return (SByte)list.ReadByte(offset, endOffset); }
		public static Int16 ReadInt16(this IList<byte> list, int offset, int endOffset) { return (Int16)(list.ReadByte(offset, endOffset) | (list.ReadByte(offset + 1, endOffset) << 8)); }
		public static Int16 ReadInt16BE(this IList<byte> list, int offset, int endOffset) { return (Int16)(list.ReadByte(offset + 1, endOffset) | (list.ReadByte(offset, endOffset) << 8)); }
		public static UInt16 ReadUInt16(this IList<byte> list, int offset, int endOffset) { return (UInt16)(list.ReadByte(offset, endOffset) | (list.ReadByte(offset + 1, endOffset) << 8)); }
		public static UInt16 ReadUInt16BE(this IList<byte> list, int offset, int endOffset) { return (UInt16)(list.ReadByte(offset + 1, endOffset) | (list.ReadByte(offset, endOffset) << 8)); }
		public static Int32 ReadInt32(this IList<byte> list, int offset, int endOffset) { return (Int32)(list.ReadByte(offset, endOffset) | (list.ReadByte(offset + 1, endOffset) << 8) | (list.ReadByte(offset + 2, endOffset) << 16) | (list.ReadByte(offset + 3, endOffset) << 24)); }
		public static Int32 ReadInt32BE(this IList<byte> list, int offset, int endOffset) { return (Int32)(list.ReadByte(offset + 3, endOffset) | (list.ReadByte(offset + 2, endOffset) << 8) | (list.ReadByte(offset + 1, endOffset) << 16) | (list.ReadByte(offset, endOffset) << 24)); }
		public static UInt32 ReadUInt32(this IList<byte> list, int offset, int endOffset) { return (UInt32)(list.ReadByte(offset, endOffset) | (list.ReadByte(offset + 1, endOffset) << 8) | (list.ReadByte(offset + 2, endOffset) << 16) | (list.ReadByte(offset + 3, endOffset) << 24)); }
		public static UInt32 ReadUInt32BE(this IList<byte> list, int offset, int endOffset) { return (UInt32)(list.ReadByte(offset + 3, endOffset) | (list.ReadByte(offset + 2, endOffset) << 8) | (list.ReadByte(offset + 1, endOffset) << 16) | (list.ReadByte(offset, endOffset) << 24)); }
		public static Int64 ReadInt64(this IList<byte> list, int offset, int endOffset) { return (Int64)((ulong)list.ReadByte(offset, endOffset) | ((ulong)list.ReadByte(offset + 1, endOffset) << 8) | ((ulong)list.ReadByte(offset + 2, endOffset) << 16) | ((ulong)list.ReadByte(offset + 3, endOffset) << 24) | ((ulong)list.ReadByte(offset + 4, endOffset) << 32) | ((ulong)list.ReadByte(offset + 5, endOffset) << 40) | ((ulong)list.ReadByte(offset + 6, endOffset) << 48) | ((ulong)list.ReadByte(offset + 7, endOffset) << 56)); }
		public static Int64 ReadInt64BE(this IList<byte> list, int offset, int endOffset) { return (Int64)((ulong)list.ReadByte(offset + 7, endOffset) | ((ulong)list.ReadByte(offset + 6, endOffset) << 8) | ((ulong)list.ReadByte(offset + 5, endOffset) << 16) | ((ulong)list.ReadByte(offset + 4, endOffset) << 24) | ((ulong)list.ReadByte(offset + 3, endOffset) << 32) | ((ulong)list.ReadByte(offset + 2, endOffset) << 40) | ((ulong)list.ReadByte(offset + 1, endOffset) << 48) | ((ulong)list.ReadByte(offset, endOffset) << 56)); }
		public static UInt64 ReadUInt64(this IList<byte> list, int offset, int endOffset) { return (UInt64)((ulong)list.ReadByte(offset, endOffset) | ((ulong)list.ReadByte(offset + 1, endOffset) << 8) | ((ulong)list.ReadByte(offset + 2, endOffset) << 16) | ((ulong)list.ReadByte(offset + 3, endOffset) << 24) | ((ulong)list.ReadByte(offset + 4, endOffset) << 32) | ((ulong)list.ReadByte(offset + 5, endOffset) << 40) | ((ulong)list.ReadByte(offset + 6, endOffset) << 48) | ((ulong)list.ReadByte(offset + 7, endOffset) << 56)); }
		public static UInt64 ReadUInt64BE(this IList<byte> list, int offset, int endOffset) { return (UInt64)((ulong)list.ReadByte(offset + 7, endOffset) | ((ulong)list.ReadByte(offset + 6, endOffset) << 8) | ((ulong)list.ReadByte(offset + 5, endOffset) << 16) | ((ulong)list.ReadByte(offset + 4, endOffset) << 24) | ((ulong)list.ReadByte(offset + 3, endOffset) << 32) | ((ulong)list.ReadByte(offset + 2, endOffset) << 40) | ((ulong)list.ReadByte(offset + 1, endOffset) << 48) | ((ulong)list.ReadByte(offset, endOffset) << 56)); }
		public static Single ReadSingle(this IList<byte> list, int offset, int endOffset) { return (Single)new PackedSingle(list.ReadInt32(offset, endOffset)); }
		public static Single ReadSingleBE(this IList<byte> list, int offset, int endOffset) { return (Single)new PackedSingle(list.ReadInt32BE(offset, endOffset)); }
		public static Double ReadDouble(this IList<byte> list, int offset, int endOffset) { return (Double)new PackedDouble(list.ReadInt64(offset, endOffset)); }
		public static Double ReadDoubleBE(this IList<byte> list, int offset, int endOffset) { return (Double)new PackedDouble(list.ReadInt64BE(offset, endOffset)); }

		#endregion Byte lists

		#region Dictionaries

		/// <summary>Try to get a value from the dictionary. If the value is not in the dictionary, assign <paramref name="defaultValue"/>.</summary>
		/// <typeparam name="TKey">The key type of the dictionary.</typeparam>
		/// <typeparam name="TValue">The value type of the dictionary.</typeparam>
		/// <param name="dictionary">The dictionary to query.</param>
		/// <param name="key">The key to search for.</param>
		/// <param name="value">Receives the value from the dictionary or the <paramref name="defaultValue"/> if not found.</param>
		/// <param name="defaultValue">The default value to use if the dictionary does not have the key.</param>
		/// <returns>Whether the key is in the dictionary.</returns>
		public static bool TryGetValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, out TValue value, TValue defaultValue = default(TValue)) {
			bool result = dictionary.TryGetValue(key, out value);
			if (!result)
				value = defaultValue;
			return result;
		}

		/// <summary>Try to get a value from the dictionary. If the value is not in the dictionary, return <paramref name="defaultValue"/>.</summary>
		/// <typeparam name="TKey">The key type of the dictionary.</typeparam>
		/// <typeparam name="TValue">The value type of the dictionary.</typeparam>
		/// <param name="dictionary">The dictionary to query.</param>
		/// <param name="key">The key to search for.</param>
		/// <param name="defaultValue">The default value to use if the dictionary does not have the key.</param>
		/// <returns>The key's value from the dictionary or <paramref name="defaultValue"/> if it did not have one.</returns>
		public static TValue TryGetValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue = default(TValue)) {
			TValue value;
			return dictionary.TryGetValue(key, out value) ? value : defaultValue;
		}

		/// <summary>Try to get a value from the dictionary. If the value is not in the dictionary, construct a new value and put it in the dictionary, then return the new value.</summary>
		/// <typeparam name="TKey">The key type of the dictionary.</typeparam>
		/// <typeparam name="TValue">The value type of the dictionary. It must have a public constructor that has no parameters.</typeparam>
		/// <param name="dictionary">The dictionary to query and possibly modify.</param>
		/// <param name="key">The key to search for.</param>
		/// <returns>The key's value from the dictionary or the new value if one was created.</returns>
		public static TValue GetValueOrCreate<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key) where TValue : new() {
			TValue result;

			if (dictionary.TryGetValue(key, out result))
				return result;
			dictionary[key] = result = new TValue();
			return result;
		}

		#endregion Dictionaries

		#region IntPtr

		/*protected static Type MakeDelegateType(CallingConventions convention, Type returnType, params Type[] parameterTypes)
		{
			ModuleBuilder dynamicMod = null; // supply this

			TypeBuilder tb = dynamicMod.DefineType("delegate-maker" + Guid.NewGuid(),
			    TypeAttributes.Public | TypeAttributes.Sealed, typeof(MulticastDelegate));

			tb.DefineConstructor(MethodAttributes.RTSpecialName |
			     MethodAttributes.SpecialName | MethodAttributes.Public |
			     MethodAttributes.HideBySig, CallingConventions.Standard,
			     new Type[] { typeof(object), typeof(IntPtr) }).
			     SetImplementationFlags(MethodImplAttributes.Runtime);

			var inv = tb.DefineMethod("Invoke", MethodAttributes.Public |
				MethodAttributes.Virtual | MethodAttributes.NewSlot |
				MethodAttributes.HideBySig,
				CallingConventions.Standard, returnType, null,
				new Type[] 
				{ 
					// this is the important bit
					typeof(System.Runtime.CompilerServices.CallConvCdecl)
				},
				parameterTypes, null, null);

			inv.SetImplementationFlags(MethodImplAttributes.Runtime);

			var t = tb.CreateType();
			return t;
		}*/

		public static T GetDelegateForFunctionPointer<T>(this IntPtr functionPointer) where T : class {
			return (T)(object)Marshal.GetDelegateForFunctionPointer(functionPointer, typeof(T));
		}

		public static T GetDelegateForFunctionPointer<T>(this IntPtr functionPointer, out T result) where T : class {
			return result = GetDelegateForFunctionPointer<T>(functionPointer);
		}

		#endregion IntPtr

		#region MemberInfo

		public static TAttribute GetCustomAttribute<TAttribute>(this MemberInfo member, bool inherit = false) where TAttribute : Attribute {
			var list = member.GetCustomAttributes(typeof(TAttribute), inherit);
			return list != null && list.Length > 0 ? (TAttribute)list[0] : null;
		}

        public static T[] GetCustomAttributes<T>(this MemberInfo memberInfo, bool inherit = false)
        {
            object[] objectList = memberInfo.GetCustomAttributes(typeof(T), inherit);
            T[] attributeList = new T[objectList.Length];

            for (int index = 0; index < objectList.Length; index++)
                attributeList[index] = (T)objectList[index];
            return attributeList;
        }

		public static bool TryGetCustomAttribute<TAttribute>(this MemberInfo member, out TAttribute result, bool inherit = false) where TAttribute : Attribute {
			result = member.GetCustomAttribute<TAttribute>(inherit);
			return result != null;
		}

		#endregion MemberInfo

		#region Numbers

		public static Int16 ReverseBytes(this Int16 value) { return (Int16)ReverseBytes((UInt16)value); }
		public static Int32 ReverseBytes(this Int32 value) { return (Int32)ReverseBytes((UInt32)value); }
		public static Int64 ReverseBytes(this Int64 value) { return (Int64)ReverseBytes((UInt64)value); }
		public static Single ReverseBytes(this Single value) { return new PackedSingle() { UIntValue = ReverseBytes(new PackedSingle() { SingleValue = value }.UIntValue) }.SingleValue; }
		public static Double ReverseBytes(this Double value) { return new PackedDouble() { UIntValue = ReverseBytes(new PackedDouble() { DoubleValue = value }.UIntValue) }.DoubleValue; }

		public static UInt16 ReverseBytes(this UInt16 value) { return (ushort)((value >> 8) | (value << 8)); }
		public static UInt32 ReverseBytes(this UInt32 value) { return (value >> 24) | ((value >> 8) & 0xFF00) | ((value & 0xFF00) << 8) | (value << 24); }
		public static UInt64 ReverseBytes(this UInt64 value) { return (value >> 56) | ((value >> 40) & 0xFF00) | ((value >> 24) & 0xFF0000) | ((value >> 8) & 0xFF000000) | ((value & 0xFF000000) << 8) | ((value & 0xFF0000) << 24) | ((value & 0xFF00) << 40) | (value << 56); }

		public static Area Squared(this Length value) { return value * value; }

		public static long ToBits(this double value) { unsafe { return new PackedDouble(value).IntValue; } }
		public static int ToBits(this float value) { unsafe { return new PackedSingle(value).IntValue; } }
		public static float BitsToSingle(this int value) { unsafe { return new PackedSingle(value).SingleValue; } }
		public static double BitsToDouble(this long value) { unsafe { return new PackedDouble(value).DoubleValue; } }

		#endregion Numbers

		#region ResourceManager

		

		#endregion ResourceManager

		#region Stream

		public static byte[] ReadAllBytes(this Stream stream) { return stream.ReadSharedBytes(checked((int)(stream.Length - stream.Position))); }

		public static byte[] ReadSharedBytes(this Stream stream, int count) {
			byte[] buffer = new byte[count];
			count = stream.Read(buffer, 0, count);

			if (count == buffer.Length)
				return buffer;

			byte[] copy = new byte[count];
			Array.Copy(buffer, 0, copy, 0, count);
			return copy;
		}

		public static int TryReadInt32(this Stream stream, int defaultValue) {
			int a = stream.ReadByte(), b = stream.ReadByte(), c = stream.ReadByte(), d = stream.ReadByte();
			if (a < 0 || b < 0 || c < 0 || d < 0)
				return defaultValue;
			return a | (b << 8) | (c << 16) | (d << 24);
		}

		#endregion Stream

		#region String

		public static int CompareNumeric(this string left, string right) {
			return CompareNumeric(left, 0, left.Length, right, 0, right.Length, CultureInfo.InvariantCulture, CompareOptions.None);
		}

		/// <summary>
		/// Compare the strings using numeric processing.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static int CompareNumeric(this string left, int leftIndex, int leftLength, string right, int rightIndex, int rightLength, CultureInfo culture, CompareOptions options) {
			int leftEnd = leftIndex + leftLength, rightEnd = rightIndex + rightLength;
			bool optionIgnoreCase = options.HasFlag(CompareOptions.IgnoreCase); // Ignore case.
			bool optionIgnoreNonSpace = options.HasFlag(CompareOptions.IgnoreNonSpace); // Ignore nonspacing combining characters such as diacritics.
			bool optionIgnoreSymbols = options.HasFlag(CompareOptions.IgnoreSymbols); // Ignore symbols such as white-space characters, punctuation, currency symbols, the percent sign, mathematical symbols, and so on.
			bool optionIgnoreKanaType = options.HasFlag(CompareOptions.IgnoreKanaType); // Ignore the Kana type, meaning that a hiragana character sound is deemed equivalent to a katakana character sound.
			bool optionIgnoreWidth = options.HasFlag(CompareOptions.IgnoreWidth); // The string comparison must ignore character width. For example, Japanese katakana characters can be full-width or half-width; these are deemed equivalent if this is set.
			bool optionOrdinalIgnoreCase = options.HasFlag(CompareOptions.OrdinalIgnoreCase); // The string comparison must ignore case and perform an ordinal comparison. This is equivalent to converting the string to uppercase using the invariant culture and then performing an ordinal comparison on the result.
			bool optionStringSort = options.HasFlag(CompareOptions.StringSort); // Indicates that the string comparison must use the string sort algorithm. In a string sort, the hyphen and apostrophe, as well as other nonalphanumeric symbols, come before alphanumeric characters.
			bool optionOrdinal = options.HasFlag(CompareOptions.Ordinal); // Indicates that the string comparison must use successive Unicode UTF-16 encoded values of the string (code unit by code unit comparison), leading to a fast comparison but one that is culture-insensitive. A string starting with a code unit XXXX16 comes before a string starting with YYYY16, if XXXX16 is less than YYYY16. This value cannot be combined with other System.Globalization.CompareOptions values and must be used alone.

			if (culture == null)
				throw new ArgumentNullException("culture");

			if (optionOrdinal || optionOrdinalIgnoreCase) {
				if (optionOrdinal && options != CompareOptions.Ordinal)
					throw new ArgumentException("Option " + typeof(CompareOptions).Name + "." + CompareOptions.Ordinal + " must be alone, not joined like " + options + ".");
				if (optionOrdinalIgnoreCase && options != CompareOptions.OrdinalIgnoreCase)
					throw new ArgumentException("Option " + typeof(CompareOptions).Name + "." + CompareOptions.OrdinalIgnoreCase + " must be alone, not joined like " + options + ".");
				if (culture != CultureInfo.InvariantCulture)
					throw new ArgumentException("With option " + options + ", culture must be the invariant culture.");
			}

			if (optionOrdinalIgnoreCase || optionOrdinal)
				throw new NotImplementedException();

			CompareInfo compare = culture.CompareInfo;

			while (true) {
				// Skip any ignored values.
				CompareNumeric_Skip(left, ref leftIndex, leftEnd, optionIgnoreNonSpace, optionIgnoreSymbols);
				CompareNumeric_Skip(right, ref rightIndex, rightEnd, optionIgnoreNonSpace, optionIgnoreSymbols);

				// Drop out if we're at the end, returning which string is longer.
				if (leftIndex >= leftEnd || rightIndex >= rightEnd)
					return leftLength.CompareTo(rightLength);

				char leftCharacter = left[leftIndex], rightCharacter = right[rightIndex];
				UnicodeCategory leftCategory = char.GetUnicodeCategory(leftCharacter);
				UnicodeCategory rightCategory = char.GetUnicodeCategory(rightCharacter);
				int comparison;

				if (leftCategory == UnicodeCategory.DecimalDigitNumber && rightCategory == UnicodeCategory.DecimalDigitNumber) {
					double leftNumber = CompareNumeric_GetNumber(left, ref leftIndex, leftEnd);
					double rightNumber = CompareNumeric_GetNumber(right, ref rightIndex, rightEnd);
					comparison = leftNumber.CompareTo(rightNumber);
				} else {
					int count = 1, max = Math.Min(leftEnd - leftIndex, rightEnd - rightIndex);

					for (count = 1; count < max; count++) {
						leftCategory = char.GetUnicodeCategory(left[leftIndex + count]);
						rightCategory = char.GetUnicodeCategory(right[rightIndex + count]);
						if (!CompareNumeric_UseDefault(leftCategory, optionIgnoreNonSpace, optionIgnoreSymbols) ||
							!CompareNumeric_UseDefault(rightCategory, optionIgnoreNonSpace, optionIgnoreSymbols))
							break;
					}

					comparison = compare.Compare(left, leftIndex, count, right, rightIndex, count, options);
					
					leftIndex += count;
					rightIndex += count;
				}

				if (comparison != 0)
					return comparison;
			}
		}

		static bool CompareNumeric_UseDefault(UnicodeCategory category, bool ignoreNonSpace, bool ignoreSymbols) {
			switch (category) {
				case UnicodeCategory.DecimalDigitNumber: return false;
				case UnicodeCategory.ModifierLetter:
				case UnicodeCategory.ModifierSymbol: return !ignoreNonSpace;
				case UnicodeCategory.CurrencySymbol:
				case UnicodeCategory.MathSymbol:
				case UnicodeCategory.OtherSymbol: return !ignoreSymbols;
				default: return true;
			}
		}

		static double CompareNumeric_GetNumber(string text, ref int index, int end) {
			double value = 0;

			while (index < end) {
				char ch = text[index];
				UnicodeCategory category = char.GetUnicodeCategory(ch);
				if (category != UnicodeCategory.DecimalDigitNumber)
					break;
				value = value * 10 + char.GetNumericValue(ch);
				index++;
			}

			return value;
		}

		static void CompareNumeric_Skip(string text, ref int index, int end, bool ignoreNonSpace, bool ignoreSymbols) {
			while (index < end) {
				switch (char.GetUnicodeCategory(text[index])) {
					case UnicodeCategory.ModifierLetter:
					case UnicodeCategory.ModifierSymbol:
						if (ignoreNonSpace)
							break;
						else
							return;

					case UnicodeCategory.CurrencySymbol:
					case UnicodeCategory.MathSymbol:
					case UnicodeCategory.OtherSymbol:
						if (ignoreSymbols)
							break;
						else
							return;

					default:
						return;
				}

				index++;
			}
		}

		#endregion String

		#region Type

		public static int Size(this Type value) { return Marshal.SizeOf(value); }

		#endregion Type
	}

	/// <summary>
	/// A wrapper for a <see cref="GCHandleType.Pinned"/> <see cref="GCHandle"/> that can be disposed, and therefore used in <c>using</c>.
	/// </summary>
	public struct GCPinnedHandle : IDisposable {
		GCHandle handle;

		public GCPinnedHandle(object value) {
			this.handle = GCHandle.Alloc(value, GCHandleType.Pinned);
		}

		public void Dispose() {
			handle.Free();
		}
	}


	[StructLayout(LayoutKind.Explicit)]
	public struct PackedDouble {
		[FieldOffset(0)]
		public double DoubleValue;

		[FieldOffset(0)]
		public long IntValue;

		[FieldOffset(0)]
		public ulong UIntValue;

		public PackedDouble(Double value) { IntValue = 0; UIntValue = 0; DoubleValue = value; }
		public PackedDouble(Int64 value) { DoubleValue = UIntValue = 0; IntValue = value; }
		public PackedDouble(UInt64 value) { DoubleValue = IntValue = 0; UIntValue = value; }

		public static explicit operator Double(PackedDouble value) { return value.DoubleValue; }
		public static explicit operator Int64(PackedDouble value) { return value.IntValue; }
		public static explicit operator UInt64(PackedDouble value) { return value.UIntValue; }
	}

	[StructLayout(LayoutKind.Explicit)]
	public struct PackedSingle {
		[FieldOffset(0)]
		public float SingleValue;

		[FieldOffset(0)]
		public int IntValue;

		[FieldOffset(0)]
		public uint UIntValue;

		public PackedSingle(float value) { IntValue = 0; UIntValue = 0; SingleValue = value; }
		public PackedSingle(int value) { SingleValue = UIntValue = 0; IntValue = value; }
		public PackedSingle(uint value) { SingleValue = IntValue = 0; UIntValue = value; }

		public static explicit operator Single(PackedSingle value) { return value.SingleValue; }
		public static explicit operator Int64(PackedSingle value) { return value.IntValue; }
		public static explicit operator UInt64(PackedSingle value) { return value.UIntValue; }
	}
}
