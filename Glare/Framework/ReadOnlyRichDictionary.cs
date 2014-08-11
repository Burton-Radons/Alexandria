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
	/// <summary>
	/// A dictionary that can be observed as it changes.
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	/// <typeparam name="TValue"></typeparam>
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(DictionaryDebugView<,>))]
	public class ReadOnlyObservableDictionary<TKey, TValue> : IDictionary<TKey, TValue>, IDictionary, INotifyPropertyChanged, INotifyCollectionChanged {
		readonly RichDictionary<TKey, TValue> Dictionary;

		/// <summary>Notified when the dictionary is changed.</summary>
		public event NotifyCollectionChangedEventHandler CollectionChanged;

		/// <summary>Notified when a property (<see cref="Count"/>) has changed.</summary>
		public event PropertyChangedEventHandler PropertyChanged;

		internal ReadOnlyObservableDictionary(RichDictionary<TKey, TValue> dictionary) {
			Dictionary = dictionary;
		}

		/// <summary>Invoked when the dictionary has changed.</summary>
		/// <param name="action"></param>
		/// <param name="changedItem"></param>
		protected internal virtual void OnCollectionChanged(NotifyCollectionChangedAction action, KeyValuePair<TKey, TValue> changedItem) {
			if (CollectionChanged != null)
				OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, changedItem));
		}

		/// <summary>Invoked when the dictionary has changed.</summary>
		/// <param name="args"></param>
		protected internal virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs args) {
			if (CollectionChanged != null)
				CollectionChanged(this, args);
		}

		/// <summary>Invoked when a value has been replaced.</summary>
		/// <param name="key"></param>
		/// <param name="newValue"></param>
		/// <param name="oldValue"></param>
		protected internal virtual void OnCollectionValueReplaced(TKey key, TValue newValue, TValue oldValue) {
			if (CollectionChanged != null)
				OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, new KeyValuePair<TKey, TValue>(key, newValue), new KeyValuePair<TKey, TValue>(key, oldValue)));
		}

		/// <summary>Notified when a property (<see cref="Count"/>) has changed.</summary>
		/// <param name="args"></param>
		protected internal virtual void OnPropertyChanged(PropertyChangedEventArgs args) {
			if (PropertyChanged != null)
				PropertyChanged(this, args);
		}

		Exception ReadOnlyException() { return new NotSupportedException("This is a read-only view of a collection."); }

		void IDictionary<TKey, TValue>.Add(TKey key, TValue value) { throw ReadOnlyException(); }

		/// <summary>Get whether the dictionary contains a key.</summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public bool ContainsKey(TKey key) { return Dictionary.ContainsKey(key); }

		/// <summary>Get the keys of the dictionary.</summary>
		public RichDictionary<TKey, TValue>.KeyCollection Keys { get { return Dictionary.Keys; } }
		ICollection<TKey> IDictionary<TKey, TValue>.Keys { get { return Dictionary.Keys; } }

		bool IDictionary<TKey, TValue>.Remove(TKey key) { throw ReadOnlyException(); }

		/// <summary>Attempt to find a key in the dictionary.</summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public bool TryGetValue(TKey key, out TValue value) { return Dictionary.TryGetValue(key, out value); }

		/// <summary>Get the values of the dictionary.</summary>
		public RichDictionary<TKey, TValue>.ValueCollection Values { get { return Dictionary.Values; } }
		ICollection<TValue> IDictionary<TKey, TValue>.Values { get { return Dictionary.Values; } }

		/// <summary>Get a value of the dictionary.</summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public TValue this[TKey key] { get { return Dictionary[key]; } }

		TValue IDictionary<TKey, TValue>.this[TKey key] {
			get { return Dictionary[key]; }
			set { throw ReadOnlyException(); }
		}

		void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item) { throw ReadOnlyException(); }

		void ICollection<KeyValuePair<TKey, TValue>>.Clear() { throw ReadOnlyException(); }

		/// <summary>Get whether an item is in the dictionary.</summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public bool Contains(KeyValuePair<TKey, TValue> item) { return ((ICollection<KeyValuePair<TKey, TValue>>)Dictionary).Contains(item); }

		/// <summary>Copy the dictionary to an array.</summary>
		/// <param name="array"></param>
		/// <param name="arrayIndex"></param>
		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) { ((ICollection<KeyValuePair<TKey, TValue>>)Dictionary).CopyTo(array, arrayIndex); }

		/// <summary>Get the number of items in the dictionary.</summary>
		public int Count { get { return Dictionary.Count; } }

		bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly { get { return true; } }

		bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item) { throw ReadOnlyException(); }

		/// <summary>Get an enumerator over the dictionary items.</summary>
		/// <returns></returns>
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
