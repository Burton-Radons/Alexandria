using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Internal {
	/// <summary>A <see cref="List&lt;&gt;"/> that also acts as a <see cref="Dictionary&lt;,&gt;"/> of <typeparamref name="TValue"/> elements, from which it derives a <typeparamref name="TKey"/>. One use of this is for a list of objects that also have unique name properties. It's also observable and <see cref="ReadOnlyListDictionary&lt;TKey,TValue&gt;"/> can be implicitly casted to.</summary>
	/// <typeparam name="TKey"></typeparam>
	/// <typeparam name="TValue"></typeparam>
	public class ListDictionary<TKey, TValue> : ObservableCollection<TValue> {
		ReadOnlyListDictionary<TKey, TValue> readOnlyView;

		readonly Dictionary<TKey, TValue> Dictionary = new Dictionary<TKey, TValue>();

		public ReadOnlyListDictionary<TKey, TValue> ReadOnlyView { get { return readOnlyView ?? (readOnlyView = new ReadOnlyListDictionary<TKey, TValue>(this)); } }

		public delegate TKey GetKeyCallback(TValue value);

		GetKeyCallback GetKeyValue;

		protected ListDictionary() : base() { }

		public TValue this[TKey key] { get { return Dictionary[key]; } }

		public ListDictionary(GetKeyCallback getKey) {
			if (getKey == null)
				throw new ArgumentNullException("getKey");
			GetKeyValue = getKey;
		}

		public ListDictionary(IEnumerable<TValue> collection, GetKeyCallback getKey)
			: this(getKey) {
			AddRange(collection);
		}

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

		protected override void ClearItems() {
			base.ClearItems();
			Dictionary.Clear();
		}

		protected override void InsertItem(int index, TValue item) {
			base.InsertItem(index, item);
			Dictionary[GetKey(item)] = item;
		}

		protected override void RemoveItem(int index) {
			Dictionary.Remove(GetKey(this[index]));
			base.RemoveItem(index);
		}

		protected override void SetItem(int index, TValue item) {
			Dictionary.Remove(GetKey(this[index]));
			Dictionary[GetKey(item)] = item;
			base.SetItem(index, item);
		}

		public static implicit operator ReadOnlyListDictionary<TKey, TValue>(ListDictionary<TKey, TValue> value) { return value.ReadOnlyView; }
	}

	/// <summary>A read-only view of a <see cref="ListDictionary&lt;TKey, TValue&gt;"/>.</summary>
	/// <typeparam name="TKey"></typeparam>
	/// <typeparam name="TValue"></typeparam>
	public class ReadOnlyListDictionary<TKey, TValue> : ReadOnlyObservableCollection<TValue> {
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		protected ListDictionary<TKey, TValue> List { get; private set; }

		public TValue this[TKey key] { get { return List[key]; } }

		public ReadOnlyListDictionary(ListDictionary<TKey, TValue> list) : base(list) {
			List = list;
		}
	}
}
