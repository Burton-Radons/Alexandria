using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Framework {
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(ListDebugView<>))]
	public class ReadOnlyObservableDictionary<TKey, TValue> : IDictionary<TKey, TValue>, IDictionary, INotifyPropertyChanged, INotifyCollectionChanged {
		readonly RichDictionary<TKey, TValue> Dictionary;

		public event NotifyCollectionChangedEventHandler CollectionChanged;

		public event PropertyChangedEventHandler PropertyChanged;

		internal ReadOnlyObservableDictionary(RichDictionary<TKey, TValue> dictionary) {
			Dictionary = dictionary;
		}

		protected internal virtual void OnCollectionChanged(NotifyCollectionChangedAction action, KeyValuePair<TKey, TValue> changedItem) {
			if (CollectionChanged != null)
				OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, changedItem));
		}

		protected internal virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs args) {
			if (CollectionChanged != null)
				CollectionChanged(this, args);
		}

		protected internal virtual void OnCollectionValueReplaced(TKey key, TValue newValue, TValue oldValue) {
			if (CollectionChanged != null)
				OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, new KeyValuePair<TKey, TValue>(key, newValue), new KeyValuePair<TKey, TValue>(key, oldValue)));
		}

		protected internal virtual void OnPropertyChanged(PropertyChangedEventArgs args) {
			if (PropertyChanged != null)
				PropertyChanged(this, args);
		}

		Exception ReadOnlyException() { return new NotSupportedException("This is a read-only view of a collection."); }

		void IDictionary<TKey, TValue>.Add(TKey key, TValue value) { throw ReadOnlyException(); }

		public bool ContainsKey(TKey key) { return Dictionary.ContainsKey(key); }

		public RichDictionary<TKey, TValue>.KeyCollection Keys { get { return Dictionary.Keys; } }
		ICollection<TKey> IDictionary<TKey, TValue>.Keys { get { return Dictionary.Keys; } }

		bool IDictionary<TKey, TValue>.Remove(TKey key) { throw ReadOnlyException(); }

		public bool TryGetValue(TKey key, out TValue value) { return Dictionary.TryGetValue(key, out value); }

		public RichDictionary<TKey, TValue>.ValueCollection Values { get { return Dictionary.Values; } }
		ICollection<TValue> IDictionary<TKey, TValue>.Values { get { return Dictionary.Values; } }

		public TValue this[TKey key] { get { return Dictionary[key]; } }

		TValue IDictionary<TKey, TValue>.this[TKey key] {
			get { return Dictionary[key]; }
			set { throw ReadOnlyException(); }
		}

		void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item) { throw ReadOnlyException(); }

		void ICollection<KeyValuePair<TKey, TValue>>.Clear() { throw ReadOnlyException(); }

		public bool Contains(KeyValuePair<TKey, TValue> item) { return ((ICollection<KeyValuePair<TKey, TValue>>)Dictionary).Contains(item); }

		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) { ((ICollection<KeyValuePair<TKey, TValue>>)Dictionary).CopyTo(array, arrayIndex); }

		public int Count { get { return Dictionary.Count; } }

		bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly { get { return true; } }

		bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item) { throw ReadOnlyException(); }

		public RichDictionary<TKey, TValue>.Enumerator GetEnumerator() { return Dictionary.GetEnumerator(); }
		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() { return GetEnumerator(); }
		IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

		void IDictionary.Add(object key, object value) { throw ReadOnlyException(); }

		void IDictionary.Clear() { throw ReadOnlyException(); }

		bool IDictionary.Contains(object key) { return ((IDictionary)Dictionary).Contains(key); }

		IDictionaryEnumerator IDictionary.GetEnumerator() { return ((IDictionary)Dictionary).GetEnumerator(); }

		bool IDictionary.IsFixedSize { get { return false; } }

		bool IDictionary.IsReadOnly { get { return true; } }

		ICollection IDictionary.Keys { get { return Keys; } }

		void IDictionary.Remove(object key) { throw ReadOnlyException(); }

		ICollection IDictionary.Values { get { return Values; } }

		object IDictionary.this[object key] {
			get { return ((IDictionary)Dictionary)[key]; }
			set { throw ReadOnlyException(); }
		}

		void ICollection.CopyTo(Array array, int index) { ((ICollection)Dictionary).CopyTo(array, index); }

		bool ICollection.IsSynchronized { get { return true; } }

		object ICollection.SyncRoot { get { return ((ICollection)Dictionary).SyncRoot; } }
	}
}
