using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Framework {
	/// <summary>
	/// A read-only view of a <see cref="Codex&lt;T&gt;"/>.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(ListDebugView<>))]
	public class ReadOnlyCodex<T> : IList<T>, IList, INotifyCollectionChanged, INotifyPropertyChanged {
		readonly Codex<T> List;

		static readonly PropertyChangedEventArgs CountChangedEventArgs = new PropertyChangedEventArgs("Count");

		/// <summary>Get an element of the collection.</summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public T this[int index] { get { return List[index]; } }

		internal ReadOnlyCodex(Codex<T> list) {
			List = list;
		}

		/// <summary>Initialise the collection.</summary>
		/// <param name="collection"></param>
		public ReadOnlyCodex(IEnumerable<T> collection) : this(new Codex<T>(collection)) { }

		/// <summary>Get an enumerator over the elements of the collection.</summary>
		/// <returns></returns>
		public ListEnumerator<Codex<T>, T> GetEnumerator() { return new ListEnumerator<Codex<T>, T>(List); }

		/// <summary>Notified whenever the collection is changed.</summary>
		public event NotifyCollectionChangedEventHandler CollectionChanged;

		/// <summary>Notified whenever a property (<see cref="Count"/>) is changed.</summary>
		public event PropertyChangedEventHandler PropertyChanged;

		internal void OnCountChanged() { OnPropertyChanged(CountChangedEventArgs); }

		/// <summary>Invoked whenever the collection is changed.</summary>
		/// <param name="args"></param>
		/// <param name="action"></param>
		protected internal virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs args, NotifyCollectionChangedAction action) {
			if (CollectionChanged != null)
				CollectionChanged(this, args ?? new NotifyCollectionChangedEventArgs(action));
		}

		/// <summary>Invoked when the collection is changed.</summary>
		/// <param name="args"></param>
		/// <param name="action"></param>
		/// <param name="changedItem"></param>
		/// <param name="index"></param>
		protected internal virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs args, NotifyCollectionChangedAction action, T changedItem, int index) {
			if (CollectionChanged != null)
				CollectionChanged(this, args ?? new NotifyCollectionChangedEventArgs(action, changedItem, index));
		}

		/// <summary>Invoked whenever a property (<see cref="Count"/>) has changed</summary>
		/// <param name="args"></param>
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs args) {
			if (PropertyChanged != null)
				PropertyChanged(this, args);
		}

		Exception ReadOnlyException() { return new NotSupportedException("This is a read-only view of a collection."); }

		/// <summary>Get the first index of an element in the collection that matches the item.</summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public int IndexOf(T item) { return List.IndexOf(item); }

		void IList<T>.Insert(int index, T item) { throw ReadOnlyException(); }

		void IList<T>.RemoveAt(int index) { throw ReadOnlyException(); }

		T IList<T>.this[int index] {
			get { return List[index]; }
			set { throw ReadOnlyException(); }
		}

		void ICollection<T>.Add(T item) { throw ReadOnlyException(); }

		void ICollection<T>.Clear() { throw ReadOnlyException(); }

		/// <summary>Get whether the item is found in the collection.</summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public bool Contains(T item) { return List.Contains(item); }

		/// <summary>Copy the collection to an array.</summary>
		/// <param name="array"></param>
		/// <param name="arrayIndex"></param>
		public void CopyTo(T[] array, int arrayIndex) { List.CopyTo(array, arrayIndex); }

		/// <summary>Get the number of elements in the collection.</summary>
		public int Count { get { return List.Count; } }

		/// <summary>Returns <c>true</c>.</summary>
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
