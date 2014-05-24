using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Glare.Framework {
	/// <summary>
	/// A list implemented as an array that expands as needed, is observable, and has a read-only view.
	/// </summary>
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(ListDebugView<>))]
	public class Codex<T> : IList<T>, IList, INotifyCollectionChanged, INotifyPropertyChanged {
		/// <summary>Backing field for <see cref="Array"/>.</summary>
		T[] array;

		/// <summary>Backing field for <see cref="Count"/>.</summary>
		int count;

		/// <summary>
		/// Get the current backing array; <see cref="Count"/> values are in use.
		/// </summary>
		public T[] Array { get { return array; } }

		static readonly PropertyChangedEventArgs CountChangedEventArgs = new PropertyChangedEventArgs("Count");

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		ReadOnlyCodex<T> readOnlyView;

		ReadOnlyCodex<T> ReadOnlyView { get { return readOnlyView ?? (readOnlyView = new ReadOnlyCodex<T>(this)); } }

		public virtual T this[int index] {
			get {
				CheckIndex(index);
				return array[index];
			}

			set {
				CheckIndex(index);
				array[index] = value;
			}
		}

		public event NotifyCollectionChangedEventHandler CollectionChanged;

		public event PropertyChangedEventHandler PropertyChanged;

		public Codex()
			: this(0) {
		}

		public Codex(int capacity) {
			array = new T[Math.Max(capacity, 1)];
		}

		public Codex(params T[] array) : this((IEnumerable<T>)array) { }

		public Codex(T[] array, int count) {
			if (array == null && count > 0)
				throw new ArgumentNullException("array");
			if (count > array.Length)
				throw new ArgumentOutOfRangeException("count");
			this.array = array;
			this.count = count;
		}

		public Codex(IEnumerable<T> items) {
			if (items == null)
				throw new ArgumentNullException("items");
			ICollection<T> collection = items as ICollection<T>;
			array = new T[collection != null ? collection.Count : 16];
			AddRange(items);
		}

		public virtual void Add(T item) {
			Expect(1);
			int index = count++;
			array[index] = item;

			OnCountChanged();
			OnCollectionChanged(NotifyCollectionChangedAction.Add, item, index);
		}

		public void AddRange(IEnumerable<T> items) {
			ICollection<T> collection = items as ICollection<T>;

			if (collection != null)
				Expect(collection.Count);
			foreach (T item in items)
				Add(item);
		}

		void Expect(int count) {
			Capacity = this.count + count;
		}

		void CheckIndex(int index) {
			if (index < 0 || index >= count)
				throw new IndexOutOfRangeException("Index is not in range.");
		}

		void CheckInclusiveIndex(int index) {
			if (index < 0 || index > count)
				throw new IndexOutOfRangeException("Index is not in range.");
		}

		public int IndexOf(T item) {
			for (int index = 0; index < count; index++)
				if (Object.Equals(array[index], item))
					return index;
			return -1;
		}

		public virtual void Insert(int index, T item) {
			CheckInclusiveIndex(index);
			Expect(1);
			for (int copyIndex = count; copyIndex > index; copyIndex--)
				array[copyIndex] = array[copyIndex - 1];
			array[index] = item;
			count++;

			OnCountChanged();
			OnCollectionChanged(NotifyCollectionChangedAction.Add, item, index);
		}

		protected virtual void OnCollectionChanged(NotifyCollectionChangedAction action) {
			if (CollectionChanged != null)
				CollectionChanged(this, new NotifyCollectionChangedEventArgs(action));
		}

		protected virtual void OnCollectionChanged(NotifyCollectionChangedAction action, T changedItem, int index) {
			if (CollectionChanged != null)
				CollectionChanged(this, new NotifyCollectionChangedEventArgs(action, changedItem, index));
			if (readOnlyView != null)
				readOnlyView.OnCollectionChanged(action, changedItem, index);
		}

		void OnCountChanged() { OnPropertyChanged(CountChangedEventArgs); }

		protected virtual void OnPropertyChanged(PropertyChangedEventArgs args) {
			if (PropertyChanged != null)
				PropertyChanged(this, args);
			if (readOnlyView != null && args == CountChangedEventArgs)
				readOnlyView.OnCountChanged();
		}

		public virtual void RemoveAt(int index) {
			CheckIndex(index);
			T value = array[index];
			for (int copyIndex = index; copyIndex < count - 1; copyIndex++)
				array[copyIndex] = array[copyIndex + 1];
			count--;

			OnCountChanged();
			OnCollectionChanged(NotifyCollectionChangedAction.Remove, value, index);
		}

		/// <summary>
		/// Set the <see cref="Count"/> to zero without freeing the backing array, allowing it to be reused. This also clears all (now hidden) elements in the backing array, so that any references are released for garbage collection. If this speed hit is undesirable or unnecessary, call <see cref="ClearFast"/>.
		/// </summary>
		public virtual void Clear() {
			System.Array.Clear(array, 0, count);
			count = 0;

			OnCountChanged();
			OnCollectionChanged(NotifyCollectionChangedAction.Reset);
		}

		/// <summary>
		/// Set the <see cref="Count"/> to zero without freeing the backing array, allowing it to be reused. This does not clear all the (now hidden) elements in the backing array, so that any references will not be released for garbage collection, but it's faster. Use <see cref="Clear"/> to erase hidden elements.
		/// </summary>
		public virtual void ClearFast() {
			count = 0;

			OnCountChanged();
			OnCollectionChanged(NotifyCollectionChangedAction.Reset);
		}

		public bool Contains(T item) {
			return IndexOf(item) >= 0;
		}

		public void CopyTo(T[] array, int arrayIndex) {
			for (int index = 0; index < count; index++)
				array[arrayIndex + index] = this.array[index];
		}

		public int Count {
			get { return count; }
		}

		public bool IsReadOnly {
			get { return false; }
		}

		public bool Remove(T item) {
			var index = IndexOf(item);
			if (index < 0)
				return false;
			RemoveAt(index);
			return true;
		}

		public ListEnumerator<Codex<T>, T> GetEnumerator() { return new ListEnumerator<Codex<T>, T>(this); }

		IEnumerator<T> IEnumerable<T>.GetEnumerator() { return GetEnumerator(); }

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return GetEnumerator(); }

		public int Capacity {
			get { return array.Length; }

			set {
				if (value < count)
					throw new ArgumentOutOfRangeException("value", "Cannot set Capacity to a value less than Count.");
				if (value < array.Length)
					return;

				var newArray = new T[value * 3 / 2];
				System.Array.Copy(array, newArray, count);
				array = newArray;
			}
		}


		int IList.Add(object value) { int index = Count; Add((T)value); return index; }

		void IList.Clear() { Clear(); }

		bool IList.Contains(object value) { return Contains((T)value); }

		int IList.IndexOf(object value) { return IndexOf((T)value); }

		void IList.Insert(int index, object value) { Insert(index, (T)value); }

		bool IList.IsFixedSize { get { return false; } }

		bool IList.IsReadOnly { get { return false; } }

		void IList.Remove(object value) { Remove((T)value); }

		void IList.RemoveAt(int index) { RemoveAt(index); }

		object IList.this[int index] {
			get { return this[index]; }
			set { this[index] = (T)value; }
		}

		void ICollection.CopyTo(Array array, int index) { foreach (T item in this) array.SetValue(item, index++); }

		bool ICollection.IsSynchronized { get { return false; } }

		object ICollection.SyncRoot { get { return null; } }

		public static implicit operator ReadOnlyCodex<T>(Codex<T> value) { return value.ReadOnlyView; }
	}
}
