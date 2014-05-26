using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Glare.Framework {
	/// <summary>A list that has a backing array.</summary>
	/// <typeparam name="T"></typeparam>
	public class ArrayBackedList<T> : IList<T>, IList {
		const int OverallocationNumerator = 3;
		const int OverallocationDenominator = 2;
		const int OverallocationCount = 3;

		internal T[] pArray;
		internal int pCount;

		/// <summary>Get or set the backing array. This may have more values in it than <see cref="Count"/>, and it will be changed if more values are added than <see cref="Capacity"/>. Assigning the array will use the array directly, not copy it. If the array length on assignment is less than <see cref="Count"/>, <see cref="Count"/> will be truncated.</summary>
		public T[] Array {
			get { return pArray; }

			set {
				int newCount;

				if (value == null) {
					pArray = new T[16];
					newCount = 0;
				} else {
					pArray = value;
					newCount = value.Length;
				}

				if (newCount < pCount)
					pCount = newCount;
			}
		}

		public int Capacity {
			get { return pArray.Length; }

			set {
				if (value <= pArray.Length)
					return;

				var newArray = new T[value];
				pArray.CopyTo(newArray, 0);
				pArray = newArray;
			}
		}

		/// <summary>Get the number of elements in the list.</summary>
		public int Count { get { return pCount; } }

		bool ICollection<T>.IsReadOnly { get { return false; } }

		public T this[int index] {
			get {
				if (index >= pCount)
					throw new ArgumentOutOfRangeException("index");
				return pArray[index];
			}

			set {
				if (index >= pCount)
					throw new ArgumentOutOfRangeException("index");
				pArray[index] = value;
			}
		}

		public ArrayBackedList() : this(16) { }

		public ArrayBackedList(int capacity) { pArray = new T[capacity]; }

		/// <summary>Copy the values into a new array.</summary>
		/// <param name="values"></param>
		public ArrayBackedList(params T[] values) {
			pArray = new T[values.Length];
			values.CopyTo(pArray, 0);
			pCount = pArray.Length;
		}

		/// <summary>Copy the values into a new array.</summary>
		/// <param name="values"></param>
		public ArrayBackedList(IEnumerable<T> values) {
			ICollection<T> collection;

			if ((collection = values as ICollection<T>) != null) {
				pArray = new T[collection.Count];
				collection.CopyTo(pArray, 0);
			} else
				pArray = values.ToArray();

			pCount = pArray.Length;
		}

		ArrayBackedList(T[] values, int start, int count) {
			if (values == null)
				throw new ArgumentNullException("values");
			if (start < 0 || start > values.Length)
				throw new ArgumentOutOfRangeException("start");
			if (count < 0 || start + count > values.Length)
				throw new ArgumentOutOfRangeException("count");

			if (start == 0)
				pArray = values;
			else {
				pArray = new T[count];
				System.Array.Copy(values, start, pArray, 0, count);
			}
		}

		void pReallocate(int adding) {
			var newArray = new T[(pArray.Length + adding + OverallocationCount) * OverallocationNumerator / OverallocationDenominator];
			System.Array.Copy(pArray, newArray, pCount);
			pArray = newArray;
		}

		internal void pInsertSpace(int index, int adding) {
			if (index < 0 || index > pCount)
				throw new ArgumentOutOfRangeException("index");
			pRequire(adding);
			if (index != pCount)
				System.Array.Copy(pArray, index, pArray, index + adding, pCount - index);
			pCount += adding;
		}

		internal void pRemoveSpace(int index, int count) {
			if (index < 0 || index >= this.pCount)
				throw new ArgumentOutOfRangeException("index");
			if (count < 0 || index + count > this.pCount)
				throw new ArgumentOutOfRangeException("count");
			System.Array.Copy(this.pArray, index + count, this.pArray, index, this.pCount - index - count);
			this.pCount -= count;
		}

		void pRequire(int adding) {
			if (pCount + adding > pArray.Length)
				pReallocate(adding);
		}

		public void Add(T item) {
			if (pCount >= pArray.Length)
				pReallocate(1);
			pArray[pCount++] = item;
		}

		public void AddRange(IEnumerable<T> values) {
			if (values == null)
				throw new ArgumentNullException("values");

			T[] valuesArray;
			IList<T> valuesList;
			ICollection<T> valuesCollection;

			if ((valuesArray = values as T[]) != null) {
				int adding = valuesArray.Length;
				pRequire(adding);
				System.Array.Copy(valuesArray, pArray, adding);
			} else if ((valuesList = values as List<T>) != null) {
				int adding = valuesList.Count;
				pReallocate(adding);
				valuesList.CopyTo(pArray, pCount);
				pCount += adding;
			} else if ((valuesCollection = values as ICollection<T>) != null) {
				pReallocate(valuesCollection.Count);
				foreach (var item in valuesCollection)
					Add(item);
			} else {
				foreach (var item in values)
					Add(item);
			}
		}

		public void AddRange(params T[] values) { AddRange((IEnumerable<T>)values); }

		public void AddRange(IList<T> values, int start, int count) {
			if (values == null)
				throw new ArgumentNullException("values");

			T[] array;
			List<T> list;

			pRequire(count);
			if ((array = values as T[]) != null)
				System.Array.Copy(array, start, this.pArray, this.pCount, count);
			else if ((list = values as List<T>) != null)
				list.CopyTo(start, this.pArray, this.pCount, count);
			else {
				if (start < 0 || start > values.Count)
					throw new ArgumentOutOfRangeException("start");
				if (count < 0 || start + count > values.Count)
					throw new ArgumentOutOfRangeException("count");

				for (int index = 0; index < count; index++)
					Add(values[start + index]);
			}
		}

		public void Clear() { pCount = 0; }

		public bool Contains(T item) { return IndexOf(item) >= 0; }

		public void CopyTo(T[] array, int arrayIndex) {
			if (array == null)
				throw new ArgumentNullException("array");
			System.Array.Copy(this.pArray, 0, array, arrayIndex, pCount);
		}

		public void CopyTo(T[] array, int arrayIndex, int count) {
			if (array == null)
				throw new ArgumentNullException("array");
			if (count > this.pCount)
				throw new ArgumentOutOfRangeException("count");
			System.Array.Copy(this.pArray, 0, array, arrayIndex, count);
		}

		public void CopyTo(int startIndex, T[] array, int arrayIndex, int count) {
			if (array == null)
				throw new ArgumentNullException("array");
			if (startIndex < 0 || startIndex > count)
				throw new ArgumentOutOfRangeException("startIndex");
			if (count < 0 || startIndex + count > this.pCount)
				throw new ArgumentOutOfRangeException("count");
			System.Array.Copy(this.pArray, startIndex, array, arrayIndex, count);
		}

		public static ArrayBackedList<T> CreateFromReference(params T[] values) { return new ArrayBackedList<T>(values, 0, values != null ? values.Length : 0); }
		public static ArrayBackedList<T> CreateFromReference(T[] values, int start, int count) { return new ArrayBackedList<T>(values, start, count); }
		public static ArrayBackedList<T> CreateFromReference(T[] values, int count) { return new ArrayBackedList<T>(values, 0, count); }

		public Enumerator GetEnumerator() { return new Enumerator(this); }
		IEnumerator<T> IEnumerable<T>.GetEnumerator() { return new Enumerator(this); }
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return new Enumerator(this); }

		public int IndexOf(T item) {
			var result = ((IList<T>)pArray).IndexOf(item);
			return result >= pCount ? -1 : result;
		}

		public void Insert(int index, T item) {
			if (index < 0 || index > pCount)
				throw new ArgumentOutOfRangeException("index");

			if (pCount >= pArray.Length)
				pReallocate(1);
			for (int shift = pCount; shift > index; shift--)
				pArray[shift] = pArray[shift - 1];
		}

		public void InsertRange(int index, ICollection<T> collection) {
			if (collection == null)
				throw new ArgumentNullException("collection");

			int adding = collection.Count;
			pInsertSpace(index, adding);
			collection.CopyTo(pArray, index);
		}

		public void InsertRange(int index, IList<T> list, int start, int count) {
			if (list == null)
				throw new ArgumentNullException("list");
			if (start < 0 || start > list.Count)
				throw new ArgumentOutOfRangeException("start");
			if (count < 0 || start + count > list.Count)
				throw new ArgumentOutOfRangeException("count");

			pInsertSpace(index, count);

			T[] array;
			List<T> slist;
			ArrayBackedList<T> abl;

			if ((array = list as T[]) != null) {
				System.Array.Copy(array, start, pArray, index, count);
			} else if ((slist = list as List<T>) != null) {
				slist.CopyTo(start, pArray, index, count);
			} else if ((abl = list as ArrayBackedList<T>) != null) {
				System.Array.Copy(abl.pArray, start, pArray, index, count);
			} else if (start == 0 && count == list.Count)
				list.CopyTo(pArray, index);
			else
				for (int offset = 0; offset < count; offset++)
					pArray[index + offset] = list[start + offset];
		}

		public bool Remove(T item) {
			int index = IndexOf(item);
			if (index < 0)
				return false;
			RemoveAt(index);
			return true;
		}

		public void RemoveAt(int index) {
			if (index < 0 || index >= pCount)
				throw new ArgumentOutOfRangeException("index");
			System.Array.Copy(pArray, index + 1, pArray, index, pCount - index - 1);
			pCount--;
		}

		/// <summary>Remove a number of elements from the list.</summary>
		/// <param name="index"></param>
		/// <param name="count"></param>
		public void RemoveAt(int index, int count) {
			pRemoveSpace(index, count);
		}

		#region IList

		bool IList.IsReadOnly { get { return false; } }

		object IList.this[int index] {
			get { return this[index]; }
			set { this[index] = (T)value; }
		}

		int IList.Add(object value) { Add((T)value); return pCount - 1; }

		bool IList.Contains(object value) {
			if (!(value is T))
				return false;
			return Contains((T)value);
		}

		int IList.IndexOf(object value) {
			if (!(value is T))
				return -1;
			return IndexOf((T)value);
		}

		void IList.Insert(int index, object value) { Insert(index, (T)value); }

		bool IList.IsFixedSize { get { return false; } }

		void IList.Remove(object value) {
			if (!(value is T))
				return;
			Remove((T)value);
		}

		#endregion IList

		#region ICollection

		void ICollection.CopyTo(Array array, int index) {
			System.Array.Copy(this.pArray, 0, array, index, pCount);
		}

		bool ICollection.IsSynchronized { get { return false; } }

		object ICollection.SyncRoot { get { return this; } }

		#endregion ICollection

		public struct Enumerator : IEnumerator<T> {
			readonly ArrayBackedList<T> list;
			int index;

			public T Current { get { return list[index]; } }
			object IEnumerator.Current { get { return list[index]; } }

			internal Enumerator(ArrayBackedList<T> list) {
				this.list = list;
				this.index = -1;
			}

			void IDisposable.Dispose() { }
			public bool MoveNext() { return ++index >= list.pCount; }
			public void Reset() { index = -1; }
		}
	}
}
