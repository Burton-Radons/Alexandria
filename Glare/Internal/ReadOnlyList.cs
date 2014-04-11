using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Internal {
	public class ReadOnlyList<T> : IList<T>, IList, INotifyCollectionChanged, INotifyPropertyChanged {
		readonly ArrayBackedList<T> List;

		static readonly PropertyChangedEventArgs CountChangedEventArgs = new PropertyChangedEventArgs("Count");

		public T this[int index] { get { return List[index]; } }

		internal ReadOnlyList(ArrayBackedList<T> list) {
			List = list;
		}

		public ListEnumerator<ReadOnlyList<T>, T> GetEnumerator() { return new ListEnumerator<ReadOnlyList<T>, T>(this); }

		public event NotifyCollectionChangedEventHandler CollectionChanged;

		public event PropertyChangedEventHandler PropertyChanged;

		internal void OnCountChanged() { OnPropertyChanged(CountChangedEventArgs); }

		protected internal virtual void OnCollectionChanged(NotifyCollectionChangedAction action, T changedItem, int index) {
			if (CollectionChanged != null)
				CollectionChanged(this, new NotifyCollectionChangedEventArgs(action, changedItem, index));
		}

		protected virtual void OnPropertyChanged(PropertyChangedEventArgs args) {
			if (PropertyChanged != null)
				PropertyChanged(this, args);
		}

		Exception ReadOnlyException() { return new NotSupportedException("This is a read-only view of a collection."); }

		public int IndexOf(T item) { return List.IndexOf(item); }

		void IList<T>.Insert(int index, T item) { throw ReadOnlyException(); }

		void IList<T>.RemoveAt(int index) { throw ReadOnlyException(); }

		T IList<T>.this[int index] {
			get { return List[index]; }
			set { throw ReadOnlyException(); }
		}

		void ICollection<T>.Add(T item) { throw ReadOnlyException(); }

		void ICollection<T>.Clear() { throw ReadOnlyException(); }

		public bool Contains(T item) { return List.Contains(item); }

		public void CopyTo(T[] array, int arrayIndex) { List.CopyTo(array, arrayIndex); }

		public int Count { get { return List.Count; } }

		public bool IsReadOnly { get { return true; } }

		bool ICollection<T>.Remove(T item) { throw ReadOnlyException(); }

		IEnumerator<T> IEnumerable<T>.GetEnumerator() { return GetEnumerator(); }

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return GetEnumerator(); }

		int IList.Add(object value) { throw ReadOnlyException(); }

		void IList.Clear() { throw ReadOnlyException(); }

		bool IList.Contains(object value) { return ((IList)List).Contains(value); }

		int IList.IndexOf(object value) { return ((IList)List).IndexOf(value); }

		void IList.Insert(int index, object value) { throw ReadOnlyException(); }

		bool IList.IsFixedSize { get { return false; } }

		bool IList.IsReadOnly { get { return true; } }

		void IList.Remove(object value) { throw ReadOnlyException(); }

		void IList.RemoveAt(int index) { throw ReadOnlyException(); }

		object IList.this[int index] {
			get { return List[index]; }
			set { throw ReadOnlyException(); }
		}

		void ICollection.CopyTo(Array array, int index) { ((ICollection)List).CopyTo(array, index); }
		bool ICollection.IsSynchronized { get { return ((IList)List).IsSynchronized; } }
		object ICollection.SyncRoot { get { return ((IList)List).SyncRoot; } }
	}
}
