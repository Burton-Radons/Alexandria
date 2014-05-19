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
	/// A dictionary that can be observed for changes, is thread-safe, and has a read-only view.
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	/// <typeparam name="TValue"></typeparam>
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(DictionaryDebugView<,>))]
	public class RichDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IDictionary, INotifyCollectionChanged, INotifyPropertyChanged {
		readonly Dictionary<TKey, TValue> Dictionary;
		ICollection<KeyValuePair<TKey, TValue>> Collection { get { return Dictionary; } }

		ReadOnlyObservableDictionary<TKey, TValue> readOnlyView;

		public event NotifyCollectionChangedEventHandler CollectionChanged;

		public event PropertyChangedEventHandler PropertyChanged;

		PropertyChangedEventArgs CountChangedEventArgs = new PropertyChangedEventArgs("Count");

		public ReadOnlyObservableDictionary<TKey, TValue> ReadOnlyView { get { return readOnlyView ?? (readOnlyView = new ReadOnlyObservableDictionary<TKey, TValue>(this)); } }

		public static implicit operator ReadOnlyObservableDictionary<TKey, TValue>(RichDictionary<TKey, TValue> value) { return value.ReadOnlyView; }

		public RichDictionary() { Dictionary = new Dictionary<TKey, TValue>(); }
		public RichDictionary(IDictionary<TKey, TValue> dictionary) { Dictionary = new Dictionary<TKey, TValue>(dictionary); }
		public RichDictionary(IEqualityComparer<TKey> comparer) { Dictionary = new Dictionary<TKey, TValue>(comparer); }
		public RichDictionary(int capacity) { Dictionary = new Dictionary<TKey, TValue>(capacity); }
		public RichDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer) { Dictionary = new Dictionary<TKey, TValue>(dictionary, comparer); }
		public RichDictionary(int capacity, IEqualityComparer<TKey> comparer) { Dictionary = new Dictionary<TKey, TValue>(capacity, comparer); }

		#region Notification methods

		protected virtual void OnCollectionChanged(NotifyCollectionChangedAction action) {
			if (CollectionChanged != null)
				CollectionChanged(this, new NotifyCollectionChangedEventArgs(action));
		}

		protected virtual void OnCollectionChanged(NotifyCollectionChangedAction action, KeyValuePair<TKey, TValue> changedItem) {
			if (CollectionChanged != null) {
				var args = new NotifyCollectionChangedEventArgs(action, changedItem);
				CollectionChanged(this, args);
				if (readOnlyView != null)
					readOnlyView.OnCollectionChanged(args);
			} else if (readOnlyView != null)
				readOnlyView.OnCollectionChanged(action, changedItem);
		}

		protected virtual void OnCollectionValueReplaced(TKey key, TValue newValue, TValue oldValue) {
			if (CollectionChanged != null) {
				var args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, new KeyValuePair<TKey, TValue>(key, newValue), new KeyValuePair<TKey, TValue>(key, oldValue));
				CollectionChanged(this, args);
				if (readOnlyView != null)
					readOnlyView.OnCollectionChanged(args);
			} else if (readOnlyView != null)
				readOnlyView.OnCollectionValueReplaced(key, newValue, oldValue);
		}

		void OnCountChanged() { OnPropertyChanged(CountChangedEventArgs); }

		protected virtual void OnPropertyChanged(PropertyChangedEventArgs args) {
			if (PropertyChanged != null)
				PropertyChanged(this, args);
			if (readOnlyView != null)
				readOnlyView.OnPropertyChanged(args);
		}

		#endregion Notification methods

		#region Virtual methods

		public virtual void Add(TKey key, TValue value) {
			lock (this) {
				Dictionary.Add(key, value);
				OnCountChanged();
				OnCollectionChanged(NotifyCollectionChangedAction.Add, new KeyValuePair<TKey, TValue>(key, value));
			}
		}

		protected virtual void Set(TKey key, TValue value) {
			TValue oldValue;

			lock (this) {
				if (Dictionary.TryGetValue(key, out oldValue)) {
					Dictionary[key] = value;
					OnCollectionValueReplaced(key, value, oldValue);
				} else {
					Dictionary[key] = value;
					OnCountChanged();
					OnCollectionChanged(NotifyCollectionChangedAction.Add, new KeyValuePair<TKey, TValue>(key, value));
				}
			}
		}

		public virtual bool Remove(TKey key) {
			TValue value;

			lock (this) {
				if (Dictionary.TryGetValue(key, out value)) {
					Dictionary.Remove(key);
					OnCountChanged();
					OnCollectionChanged(NotifyCollectionChangedAction.Remove, new KeyValuePair<TKey, TValue>(key, value));
					return true;
				} else
					return false;
			}
		}

		protected virtual bool Remove(KeyValuePair<TKey, TValue> item) {
			lock (this) {
				if (!Collection.Remove(item))
					return false;
				OnCountChanged();
				OnCollectionChanged(NotifyCollectionChangedAction.Remove, item);
				return true;
			}
		}

		public virtual void Clear() {
			lock (this) {
				if (Dictionary.Count == 0)
					return;
				Dictionary.Clear();
				OnCountChanged();
				OnCollectionChanged(NotifyCollectionChangedAction.Reset);
			}
		}

		bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly { get { return false; } }

		#endregion Virtual methods

		#region IDictionary<TKey, TValue>

		public int Count { get { lock (this) return Dictionary.Count; } }

		ICollection<TKey> IDictionary<TKey, TValue>.Keys { get { return Dictionary.Keys; } }
		public KeyCollection Keys { get { return new KeyCollection(this); } }

		ICollection<TValue> IDictionary<TKey, TValue>.Values { get { return Values; } }
		public ValueCollection Values { get { return new ValueCollection(this); } }

		public TValue this[TKey key] {
			get { lock (this) return Dictionary[key]; }
			set { Set(key, value); }
		}

		public bool ContainsKey(TKey key) {
			lock (this)
				return Dictionary.ContainsKey(key);
		}

		public bool TryGetValue(TKey key, out TValue value) { return Dictionary.TryGetValue(key, out value); }

		#endregion IDictionary<TKey, TValue>

		#region ICollection<KeyValuePair<TKey, TValue>>

		void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item) { Add(item.Key, item.Value); }

		bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item) { lock (this) return Collection.Contains(item); }

		void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) { lock (this) Collection.CopyTo(array, arrayIndex); }

		bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item) { return Remove(item); }

		#endregion ICollection<KeyValuePair<TKey, TValue>>

		#region IEnumerable

		public Enumerator GetEnumerator() { return new Enumerator(this); }
		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() { return GetEnumerator(); }
		IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

		#endregion IEnumerable

		#region IDictionary

		void IDictionary.Add(object key, object value) { Add((TKey)key, (TValue)value); }
		bool IDictionary.Contains(object key) { return key is TKey && ContainsKey((TKey)key); }
		IDictionaryEnumerator IDictionary.GetEnumerator() { return GetEnumerator(); }

		bool IDictionary.IsFixedSize { get { return false; } }
		bool IDictionary.IsReadOnly { get { return false; } }

		ICollection IDictionary.Keys { get { return Keys; } }

		void IDictionary.Remove(object key) { Remove((TKey)key); }

		ICollection IDictionary.Values { get { return Values; } }

		object IDictionary.this[object key] {
			get { return this[(TKey)key]; }
			set { this[(TKey)key] = (TValue)value; }
		}

		#endregion IDictionary

		#region ICollection

		void ICollection.CopyTo(Array array, int index) { lock (this) ((ICollection)Dictionary).CopyTo(array, index); }

		bool ICollection.IsSynchronized { get { return true; } }

		object ICollection.SyncRoot { get { return this; } }

		#endregion ICollection

		#region Inner types

		static Exception ReadOnlyException() { return new NotSupportedException("This collection is read-only."); }

		public struct KeyCollection : ICollection<TKey>, ICollection {
			readonly RichDictionary<TKey, TValue> Dictionary;
			readonly Dictionary<TKey, TValue>.KeyCollection Source;

			internal KeyCollection(RichDictionary<TKey, TValue> dictionary) {
				Dictionary = dictionary;
				Source = dictionary.Dictionary.Keys;
			}

			void ICollection<TKey>.Add(TKey item) { throw ReadOnlyException(); }
			void ICollection<TKey>.Clear() { throw ReadOnlyException(); }

			public bool Contains(TKey item) { lock (Dictionary) return Source.Contains(item); }

			public void CopyTo(TKey[] array, int arrayIndex) { lock (Dictionary) Source.CopyTo(array, arrayIndex); }

			public int Count { get { lock (Dictionary) return Source.Count; } }

			bool ICollection<TKey>.IsReadOnly { get { return true; } }

			bool ICollection<TKey>.Remove(TKey item) { throw ReadOnlyException(); }

			public Enumerator GetEnumerator() { return new Enumerator(this); }
			IEnumerator<TKey> IEnumerable<TKey>.GetEnumerator() { return GetEnumerator(); }
			IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

			void ICollection.CopyTo(Array array, int index) { lock (Dictionary) ((ICollection)Source).CopyTo(array, index); }

			bool ICollection.IsSynchronized { get { return true; } }

			object ICollection.SyncRoot { get { return Dictionary; } }

			public struct Enumerator : IEnumerator<TKey> {
				readonly RichDictionary<TKey, TValue> Dictionary;
				readonly Dictionary<TKey, TValue>.KeyCollection.Enumerator Source;

				internal Enumerator(KeyCollection collection) {
					Dictionary = collection.Dictionary;
					Source = collection.Source.GetEnumerator();
				}

				public TKey Current { get { lock (Dictionary) return Source.Current; } }
				public void Dispose() { Source.Dispose(); }
				object IEnumerator.Current { get { lock (Dictionary) return Source.Current; } }
				public bool MoveNext() { lock (Dictionary) return Source.MoveNext(); }
				public void Reset() { lock (Dictionary) ((IEnumerator)Source).Reset(); }
			}
		}

		public struct ValueCollection : ICollection<TValue>, ICollection {
			readonly RichDictionary<TKey, TValue> Dictionary;
			readonly Dictionary<TKey, TValue>.ValueCollection Source;

			internal ValueCollection(RichDictionary<TKey, TValue> dictionary) {
				Dictionary = dictionary;
				Source = dictionary.Dictionary.Values;
			}

			void ICollection<TValue>.Add(TValue item) { throw ReadOnlyException(); }
			void ICollection<TValue>.Clear() { throw ReadOnlyException(); }

			public bool Contains(TValue item) { lock (Dictionary) return Source.Contains(item); }

			public void CopyTo(TValue[] array, int arrayIndex) { lock (Dictionary) Source.CopyTo(array, arrayIndex); }

			public int Count { get { lock (Dictionary) return Source.Count; } }

			bool ICollection<TValue>.IsReadOnly { get { return true; } }

			bool ICollection<TValue>.Remove(TValue item) { throw ReadOnlyException(); }

			public Enumerator GetEnumerator() { return new Enumerator(this); }
			IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator() { return GetEnumerator(); }
			IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

			void ICollection.CopyTo(Array array, int index) { lock (Dictionary) ((ICollection)Source).CopyTo(array, index); }

			bool ICollection.IsSynchronized { get { return true; } }

			object ICollection.SyncRoot { get { return Dictionary; } }

			public struct Enumerator : IEnumerator<TValue> {
				readonly RichDictionary<TKey, TValue> Dictionary;
				readonly Dictionary<TKey, TValue>.ValueCollection.Enumerator Source;

				internal Enumerator(ValueCollection collection) {
					Dictionary = collection.Dictionary;
					Source = collection.Source.GetEnumerator();
				}

				public TValue Current { get { lock (Dictionary) return Source.Current; } }
				public void Dispose() { Source.Dispose(); }
				object IEnumerator.Current { get { lock (Dictionary) return Source.Current; } }
				public bool MoveNext() { lock (Dictionary) return Source.MoveNext(); }
				public void Reset() { lock (Dictionary) ((IEnumerator)Source).Reset(); }
			}
		}

		public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDictionaryEnumerator {
			readonly RichDictionary<TKey, TValue> Dictionary;
			Dictionary<TKey, TValue>.Enumerator Source;

			internal Enumerator(RichDictionary<TKey, TValue> dictionary) {
				Dictionary = dictionary;
				Source = dictionary.Dictionary.GetEnumerator();
			}

			public KeyValuePair<TKey, TValue> Current { get { lock (Dictionary) return Source.Current; } }

			public void Dispose() { lock (Dictionary) Source.Dispose(); }

			object IEnumerator.Current { get { lock (Dictionary) return ((IEnumerator)Source).Current; } }

			public bool MoveNext() { lock (Dictionary) return Source.MoveNext(); }

			void IEnumerator.Reset() { lock (Dictionary) ((IEnumerator)Source).Reset(); }

			DictionaryEntry IDictionaryEnumerator.Entry { get { lock (Dictionary) return ((IDictionaryEnumerator)Source).Entry; } }

			object IDictionaryEnumerator.Key { get { lock (Dictionary) return ((IDictionaryEnumerator)Source).Key; } }

			object IDictionaryEnumerator.Value { get { lock (Dictionary) return ((IDictionaryEnumerator)Source).Value; } }
		}

		#endregion Inner types
	}
}
