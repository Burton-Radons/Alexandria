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

		/// <summary>
		/// Get or set the capacity of the list. If the value is less than the current capacity, assignment is ignored.
		/// </summary>
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

		/// <summary>Get or set an element of the list.</summary>
		/// <param name="index">The zero-based index of the element to read or assign.</param>
		/// <returns>The value at the given index.</returns>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is out of range.</exception>
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

		/// <summary>Create a list with a <see cref="Capacity"/> of 16.</summary>
		public ArrayBackedList() : this(16) { }

		/// <summary>Create a list with the given initial capacity.</summary>
		/// <param name="capacity">The capacity of the list.</param>
		public ArrayBackedList(int capacity) { pArray = new T[capacity]; }

		/// <summary>Copy the values into a new list.</summary>
		/// <param name="values">The values to copy into the new list.</param>
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

		/// <summary>Create a list by taking provided values directly.</summary>
		/// <param name="values">The initial <see cref="Array"/> value.</param>
		/// <param name="start">The start of the <paramref name="values"/> array to read from; if this is not zero, then <see cref="Array"/> will be a copy.</param>
		/// <param name="count">The initial <see cref="Count"/> value.</param>
		/// <exception cref="ArgumentNullException"><paramref name="values"/> is <c>null</c>.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="start"/> is less than 0 or greater than <paramref name="values"/>.length.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is less than 0 or <paramref name="start"/> + <paramref name="count"/> is greater than <paramref name="values"/>.length.</exception>
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

		/// <summary>Add an element to the end of the list.</summary>
		/// <param name="item">The element to add.</param>
		public void Add(T item) {
			if (pCount >= pArray.Length)
				pReallocate(1);
			pArray[pCount++] = item;
		}

		/// <summary>Add all elements in the enumerable to the list. There are optimizations for providing arrays, lists, and collections.</summary>
		/// <param name="values">The values to add to the list.</param>
		/// <exception cref="ArgumentNullException"><paramref name="values"/> is <c>null</c>.</exception>
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

		/// <summary>Add an array to the end of the list.</summary>
		/// <param name="values">The array of values to add.</param>
		/// <exception cref="ArgumentNullException"><paramref name="values"/> is <c>null</c>.</exception>
		public void AddRange(params T[] values) { AddRange((IEnumerable<T>)values); }

		/// <summary>Add elements of a list to the end of this list. There are optimizations for arrays and lists.</summary>
		/// <param name="values">The values to add to the list.</param>
		/// <param name="start">The first item in <paramref name="values"/> to add.</param>
		/// <param name="count">The number of items in <paramref name="values"/> to add.</param>
		/// <exception cref="ArgumentNullException"><paramref name="values"/> is <c>null</c>.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="start"/> is less than 0 or greater than <paramref name="values"/>' length.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is less than 0 or <paramref name="start"/> + <paramref name="count"/> is greater than <paramref name="values"/>' length.</exception>
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

		/// <summary>
		/// Set the <see cref="Count"/> to zero. This does not clear the entries of the list.
		/// </summary>
		public void Clear() { pCount = 0; }

		/// <summary>Get whether the list contains the item.</summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public bool Contains(T item) { return IndexOf(item) >= 0; }

		/// <summary>Copy the list to an array.</summary>
		/// <param name="array"></param>
		/// <param name="arrayIndex"></param>
		public void CopyTo(T[] array, int arrayIndex) {
			if (array == null)
				throw new ArgumentNullException("array");
			System.Array.Copy(this.pArray, 0, array, arrayIndex, pCount);
		}

		/// <summary>Copy the list to an array.</summary>
		/// <param name="array"></param>
		/// <param name="arrayIndex"></param>
		/// <param name="count"></param>
		public void CopyTo(T[] array, int arrayIndex, int count) {
			if (array == null)
				throw new ArgumentNullException("array");
			if (count > this.pCount)
				throw new ArgumentOutOfRangeException("count");
			System.Array.Copy(this.pArray, 0, array, arrayIndex, count);
		}

		/// <summary>Copy the list to an array.</summary>
		/// <param name="startIndex"></param>
		/// <param name="array"></param>
		/// <param name="arrayIndex"></param>
		/// <param name="count"></param>
		public void CopyTo(int startIndex, T[] array, int arrayIndex, int count) {
			if (array == null)
				throw new ArgumentNullException("array");
			if (startIndex < 0 || startIndex > count)
				throw new ArgumentOutOfRangeException("startIndex");
			if (count < 0 || startIndex + count > this.pCount)
				throw new ArgumentOutOfRangeException("count");
			System.Array.Copy(this.pArray, startIndex, array, arrayIndex, count);
		}

		/// <summary>Create a list by taking the <paramref name="values"/> array directly as <see cref="Array"/>.</summary>
		/// <param name="values"></param>
		/// <returns></returns>
		public static ArrayBackedList<T> CreateFromReference(params T[] values) { return new ArrayBackedList<T>(values, 0, values != null ? values.Length : 0); }

		/// <summary>Create a list by taking the <paramref name="values"/> array directly as <see cref="Array"/>.</summary>
		/// <param name="values"></param>
		/// <param name="start"></param>
		/// <param name="count"></param>
		/// <returns></returns>
		public static ArrayBackedList<T> CreateFromReference(T[] values, int start, int count) { return new ArrayBackedList<T>(values, start, count); }

		/// <summary>Create a list by taking the <paramref name="values"/> array directly as <see cref="Array"/>.</summary>
		public static ArrayBackedList<T> CreateFromReference(T[] values, int count) { return new ArrayBackedList<T>(values, 0, count); }

		/// <summary>Get an enumerator over the items of the list.</summary>
		/// <returns></returns>
		public Enumerator GetEnumerator() { return new Enumerator(this); }

		/// <summary>Get an enumerator over the items of the list.</summary>
		/// <returns></returns>
		IEnumerator<T> IEnumerable<T>.GetEnumerator() { return new Enumerator(this); }

		/// <summary>Get an enumerator over the items of the list.</summary>
		/// <returns></returns>
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return new Enumerator(this); }

		/// <summary>Find an item in the list and return its zero-based index, or return -1 if the item is not in the list.</summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public int IndexOf(T item) {
			var result = ((IList<T>)pArray).IndexOf(item);
			return result >= pCount ? -1 : result;
		}

		/// <summary>Insert an item into the list at the given index.</summary>
		/// <param name="index"></param>
		/// <param name="item"></param>
		public void Insert(int index, T item) {
			if (index < 0 || index > pCount)
				throw new ArgumentOutOfRangeException("index");

			if (pCount >= pArray.Length)
				pReallocate(1);
			for (int shift = pCount; shift > index; shift--)
				pArray[shift] = pArray[shift - 1];
		}

		/// <summary>Insert a range of items into the list at the given index.</summary>
		/// <param name="index"></param>
		/// <param name="collection"></param>
		public void InsertRange(int index, ICollection<T> collection) {
			if (collection == null)
				throw new ArgumentNullException("collection");

			int adding = collection.Count;
			pInsertSpace(index, adding);
			collection.CopyTo(pArray, index);
		}

		/// <summary>Insert a range of items into the list at the given index.</summary>
		/// <param name="index"></param>
		/// <param name="list"></param>
		/// <param name="start"></param>
		/// <param name="count"></param>
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

		/// <summary>Attempt to remove the first item found from the list, returning success.</summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public bool Remove(T item) {
			int index = IndexOf(item);
			if (index < 0)
				return false;
			RemoveAt(index);
			return true;
		}

		/// <summary>Remove an element from the list.</summary>
		/// <param name="index"></param>
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

		/// <summary>An enumerator for the list.</summary>
		public struct Enumerator : IEnumerator<T> {
			readonly ArrayBackedList<T> list;
			int index;

			/// <summary>Get the current value.</summary>
			public T Current { get { return list[index]; } }

			/// <summary>Get the current value.</summary>
			object IEnumerator.Current { get { return list[index]; } }

			internal Enumerator(ArrayBackedList<T> list) {
				this.list = list;
				this.index = -1;
			}

			void IDisposable.Dispose() { }

			/// <summary>Move to the next value in the list, returning whether the enumeration has not ended.</summary>
			/// <returns></returns>
			public bool MoveNext() { return ++index >= list.pCount; }

			/// <summary>Reset the enumerator.</summary>
			public void Reset() { index = -1; }
		}
	}
}
