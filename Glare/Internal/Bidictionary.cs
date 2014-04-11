using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Internal {
	public class Bidictionary<A, B> : IDictionary<A, B> {
		readonly Dictionary<A, B> AToB = new Dictionary<A, B>();
		readonly Dictionary<B, A> BToA = new Dictionary<B, A>();
		ReadOnlyBidictionary<A, B> readOnlyView;

		public int Count { get { return AToB.Count; } }

		bool ICollection<KeyValuePair<A, B>>.IsReadOnly { get { return false; } }

		ICollection<A> IDictionary<A, B>.Keys { get { return AToB.Keys; } }
		public Dictionary<A, B>.KeyCollection Keys { get { return AToB.Keys; } }

		public ReadOnlyBidictionary<A, B> ReadOnlyView { get { return readOnlyView ?? (readOnlyView = new ReadOnlyBidictionary<A, B>(this)); } }

		ICollection<B> IDictionary<A, B>.Values { get { return AToB.Values; } }
		public Dictionary<A, B>.ValueCollection Values { get { return AToB.Values; } }

		public B this[A key] {
			get { return AToB[key]; }
			set {
				if (BToA.ContainsKey(value))
					throw new ArgumentException("Value " + value + " already exists.");
				Remove(key);
				Add(key, value);
			}
		}

		public A this[B key] {
			get { return BToA[key]; }
			set {
				if (AToB.ContainsKey(value))
					throw new ArgumentException("Value " + value + " already exists.");
				Remove(key);
				Add(key, value);
			}
		}

		public void Add(A a, B b) {
			if (AToB.ContainsKey(a)) throw new ArgumentException();
			if (BToA.ContainsKey(b)) throw new ArgumentException();
			AToB.Add(a, b);
			BToA.Add(b, a);
		}

		public void Add(B b, A a) { Add(a, b); }

		public bool ContainsKey(A a) { return AToB.ContainsKey(a); }
		public bool ContainsKey(B b) { return BToA.ContainsKey(b); }

		public bool Remove(A a) {
			B b;

			if (!AToB.TryGetValue(a, out b))
				return false;
			AToB.Remove(a);
			BToA.Remove(b);
			return true;
		}

		public bool Remove(B b) {
			A a;

			if (!BToA.TryGetValue(b, out a))
				return false;
			AToB.Remove(a);
			BToA.Remove(b);
			return true;
		}

		public bool TryGetValue(A key, out B value) { return AToB.TryGetValue(key, out value); }
		public bool TryGetValue(B key, out A value) { return BToA.TryGetValue(key, out value); }

		public void Add(KeyValuePair<A, B> item) { Add(item.Key, item.Value); }
		public void Add(KeyValuePair<B, A> item) { Add(item.Key, item.Value); }

		public void Clear() {
			AToB.Clear();
			BToA.Clear();
		}

		public bool Contains(KeyValuePair<A, B> item) { return AToB.Contains(item); }
		public bool Contains(KeyValuePair<B, A> item) { return BToA.Contains(item); }

		public void CopyTo(KeyValuePair<A, B>[] array, int arrayIndex) { ((IDictionary<A, B>)AToB).CopyTo(array, arrayIndex); }
		public void CopyTo(KeyValuePair<B, A>[] array, int arrayIndex) { ((IDictionary<B, A>)BToA).CopyTo(array, arrayIndex); }

		public bool Remove(KeyValuePair<A, B> item) {
			if (!((IDictionary<A, B>)AToB).Remove(item))
				return false;
			((IDictionary<B, A>)BToA).Remove(new KeyValuePair<B, A>(item.Value, item.Key));
			return true;
		}

		public Dictionary<A, B>.Enumerator GetEnumerator() { return AToB.GetEnumerator(); }
		IEnumerator<KeyValuePair<A, B>> IEnumerable<KeyValuePair<A, B>>.GetEnumerator() { return AToB.GetEnumerator(); }
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return AToB.GetEnumerator(); }

		public static implicit operator ReadOnlyBidictionary<A, B>(Bidictionary<A, B> value) { return value.ReadOnlyView; }
	}

	public class ReadOnlyBidictionary<A, B> : IDictionary<A, B> {
		public int Count { get { return Dictionary.Count; } }

		bool ICollection<KeyValuePair<A, B>>.IsReadOnly { get { return true; } }

		public Dictionary<A, B>.KeyCollection Keys { get { return Dictionary.Keys; } }
		ICollection<A> IDictionary<A, B>.Keys { get { return Dictionary.Keys; } }

		public Dictionary<A, B>.ValueCollection Values { get { return Dictionary.Values; } }
		ICollection<B> IDictionary<A, B>.Values { get { return Dictionary.Values; } }

		B IDictionary<A, B>.this[A key] {
			get { return Dictionary[key]; }
			set { throw ReadOnlyException(); }
		}

		public B this[A key] { get { return Dictionary[key]; } }
		public A this[B key] { get { return Dictionary[key]; } }

		readonly Bidictionary<A, B> Dictionary;

		public ReadOnlyBidictionary(Bidictionary<A, B> dictionary) {
			if (dictionary == null)
				throw new ArgumentNullException("dictionary");
			Dictionary = dictionary;
		}

		Exception ReadOnlyException() { return new NotSupportedException("This is a read-only view of a " + typeof(Bidictionary<,>).Name + " type and cannot be modified."); }

		void ICollection<KeyValuePair<A, B>>.Add(KeyValuePair<A, B> item) { throw ReadOnlyException(); }
		void ICollection<KeyValuePair<A, B>>.Clear() { throw ReadOnlyException(); }
		bool ICollection<KeyValuePair<A, B>>.Remove(KeyValuePair<A, B> item) { throw ReadOnlyException(); }

		void IDictionary<A, B>.Add(A key, B value) { throw ReadOnlyException(); }
		bool IDictionary<A, B>.Remove(A key) { throw ReadOnlyException(); }

		public bool Contains(KeyValuePair<A, B> item) { return Dictionary.Contains(item); }
		public bool Contains(KeyValuePair<B, A> item) { return Dictionary.Contains(item); }

		public bool ContainsKey(A key) { return Dictionary.ContainsKey(key); }
		public bool ContainsKey(B key) { return Dictionary.ContainsKey(key); }

		public void CopyTo(KeyValuePair<A, B>[] array, int arrayIndex) { Dictionary.CopyTo(array, arrayIndex); }
		public void CopyTo(KeyValuePair<B, A>[] array, int arrayIndex) { Dictionary.CopyTo(array, arrayIndex); }

		public bool TryGetValue(A key, out B value) { return Dictionary.TryGetValue(key, out value); }
		public bool TryGetValue(B key, out A value) { return Dictionary.TryGetValue(key, out value); }

		public Dictionary<A, B>.Enumerator GetEnumerator() { return Dictionary.GetEnumerator(); }

		IEnumerator<KeyValuePair<A, B>> IEnumerable<KeyValuePair<A, B>>.GetEnumerator() { return Dictionary.GetEnumerator(); }

		IEnumerator IEnumerable.GetEnumerator() { return Dictionary.GetEnumerator(); }
	}
}
