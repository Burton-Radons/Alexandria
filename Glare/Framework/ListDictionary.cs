using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Framework {
	/// <summary>A <see cref="List&lt;T&gt;"/> that also acts as a <see cref="Dictionary{TKey,TValue}"/> of <typeparamref name="TValue"/> elements, from which it derives a <typeparamref name="TKey"/>. One use of this is for a list of objects that also have unique name properties. It's also observable and <see cref="ReadOnlyCodexDictionary&lt;TKey,TValue&gt;"/> can be implicitly casted to.</summary>
	/// <typeparam name="TKey"></typeparam>
	/// <typeparam name="TValue"></typeparam>
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(ListDebugView<>))]
	public class ListDictionary<TKey, TValue> : ObservableCollection<TValue> {
		ReadOnlyCodexDictionary<TKey, TValue> readOnlyView;

		readonly Dictionary<TKey, TValue> Source = new Dictionary<TKey, TValue>();

		/// <summary>
		/// Get a read-only view of the dictionary.
		/// </summary>
		public ReadOnlyCodexDictionary<TKey, TValue> ReadOnlyView { get { return readOnlyView ?? (readOnlyView = new ReadOnlyCodexDictionary<TKey, TValue>(this)); } }

		/// <summary>Callback for retrieving the key from a value.</summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public delegate TKey GetKeyCallback(TValue value);

		GetKeyCallback GetKeyValue;

		/// <summary>Get an element of the list dictionary based on a key.</summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public TValue this[TKey key] { get { return Source[key]; } }

		/// <summary>
		/// Initialise the dictionary.
		/// </summary>
		protected ListDictionary() : base() { }

		/// <summary>Initialise the list dictionary.</summary>
		/// <param name="getKey"></param>
		public ListDictionary(GetKeyCallback getKey) {
			if (getKey == null)
				throw new ArgumentNullException("getKey");
			GetKeyValue = getKey;
		}

		/// <summary>Initialise the list dictionary.</summary>
		/// <param name="collection"></param>
		/// <param name="getKey"></param>
		public ListDictionary(IEnumerable<TValue> collection, GetKeyCallback getKey)
			: this(getKey) {
			AddRange(collection);
		}

		/// <summary>Add a number of elements to the list dictionary.</summary>
		/// <param name="collection"></param>
		public void AddRange(IEnumerable<TValue> collection) {
			foreach (TValue value in collection)
				Add(value);
		}

		/// <summary>Get the key for a value.</summary>
		/// <param name="value"></param>
		/// <returns></returns>
		protected virtual TKey GetKey(TValue value) {
			return GetKeyValue(value);
		}

		/// <summary>Remove all items of the list dictionary.</summary>
		protected override void ClearItems() {
			base.ClearItems();
			Source.Clear();
		}

		/// <summary>Insert an item into the list dictionary.</summary>
		/// <param name="index"></param>
		/// <param name="item"></param>
		protected override void InsertItem(int index, TValue item) {
			base.InsertItem(index, item);
			Source[GetKey(item)] = item;
		}

		/// <summary>Remove an item from the list dictionary.</summary>
		/// <param name="index"></param>
		protected override void RemoveItem(int index) {
			Source.Remove(GetKey(this[index]));
			base.RemoveItem(index);
		}

		/// <summary>Assign an item in the list dictionary.</summary>
		/// <param name="index"></param>
		/// <param name="item"></param>
		protected override void SetItem(int index, TValue item) {
			Source.Remove(GetKey(this[index]));
			Source[GetKey(item)] = item;
			base.SetItem(index, item);
		}

		/// <summary>Implicitly cast to a read-only view of this list dictionary.</summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static implicit operator ReadOnlyCodexDictionary<TKey, TValue>(ListDictionary<TKey, TValue> value) { return value.ReadOnlyView; }
	}

	/// <summary>A read-only view of a <see cref="ListDictionary&lt;TKey, TValue&gt;"/>.</summary>
	/// <typeparam name="TKey"></typeparam>
	/// <typeparam name="TValue"></typeparam>
	public class ReadOnlyCodexDictionary<TKey, TValue> : ReadOnlyObservableCollection<TValue> {
		/// <summary>Get the containing list.</summary>
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		protected ListDictionary<TKey, TValue> List { get; private set; }

		/// <summary>Get an element in the dictionary based on a key.</summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public TValue this[TKey key] { get { return List[key]; } }

		/// <summary>Initialise the view of the list dictionary.</summary>
		/// <param name="list"></param>
		public ReadOnlyCodexDictionary(ListDictionary<TKey, TValue> list) : base(list) {
			List = list;
		}
	}
}
