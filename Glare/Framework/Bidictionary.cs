using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Framework {
	/// <summary>A bidirectional dictionary, meaning that its keys can be treated as values and vice versa. This also has a read-only view.</summary>
	/// <typeparam name="A">The first matched type.</typeparam>
	/// <typeparam name="B">The second matched type.</typeparam>
	public class Bidictionary<A, B> : IDictionary<A, B> {
		/// <summary>The dictionary for converting <typeparamref name="A"/> values to <typeparamref name="B"/>.</summary>
		internal readonly Dictionary<A, B> AToB = new Dictionary<A, B>();

		/// <summary>The dictionary for converting <typeparamref name="B"/> values to <typeparamref name="A"/>.</summary>
		internal readonly Dictionary<B, A> BToA = new Dictionary<B, A>();

		/// <summary>Holds the read-only view of this bidictionary when necessary.</summary>
		ReadOnlyBidictionary<A, B> pReadOnlyView;

		/// <summary>Get the number of elements in the bidictionary.</summary>
		public int Count { get { return AToB.Count; } }

		bool ICollection<KeyValuePair<A, B>>.IsReadOnly { get { return false; } }

		ICollection<A> IDictionary<A, B>.Keys { get { return AToB.Keys; } }

		/// <summary>Get the keys of the A to B dictionary.</summary>
		public Dictionary<A, B>.KeyCollection Keys { get { return AToB.Keys; } }

		ICollection<B> IDictionary<A, B>.Values { get { return AToB.Values; } }

		/// <summary>Get the values of the A to B dictionary.</summary>
		public Dictionary<A, B>.ValueCollection Values { get { return AToB.Values; } }

		/// <summary>Get a read-only view of this bidictionary.</summary>
		public ReadOnlyBidictionary<A, B> ReadOnlyView { get { return pReadOnlyView ?? (pReadOnlyView = new ReadOnlyBidictionary<A, B>(this)); } }

		/// <summary>Get or set a <typeparamref name="B"/> value from an <typeparamref name="A"/> key.</summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public B this[A key] {
			get { return AToB[key]; }
			set {
				if (BToA.ContainsKey(value))
					throw new ArgumentException("Value " + value + " already exists.");
				Remove(key);
				Add(key, value);
			}
		}

		/// <summary>Get or set a <typeparamref name="A"/> value from a <typeparamref name="B"/> key.</summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public A this[B key] {
			get { return BToA[key]; }
			set {
				if (AToB.ContainsKey(value))
					throw new ArgumentException("Value " + value + " already exists.");
				Remove(key);
				Add(key, value);
			}
		}

		/// <summary>Add an item to the bidictionary, throwing an exception if either value has already been added.</summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		public void Add(A a, B b) {
			if (AToB.ContainsKey(a)) throw new ArgumentException();
			if (BToA.ContainsKey(b)) throw new ArgumentException();
			AToB.Add(a, b);
			BToA.Add(b, a);
		}

		/// <summary>Add an item to the bidictionary, throwing an exception if either value has already been added.</summary>
		/// <param name="b"></param>
		/// <param name="a"></param>
		public void Add(B b, A a) { Add(a, b); }

		/// <summary>Add a pair of values to the bidictionary, throwing an exception if either is already in the bidictionary.</summary>
		/// <param name="item"></param>
		public void Add(KeyValuePair<A, B> item) { Add(item.Key, item.Value); }

		/// <summary>Add a pair of values to the bidictionary, throwing an exception if either is already in the bidictionary.</summary>
		/// <param name="item"></param>
		public void Add(KeyValuePair<B, A> item) { Add(item.Key, item.Value); }

		/// <summary>Add a pair of values to the bidictionary, throwing an exception if either is already in the bidictionary.</summary>
		/// <param name="item"></param>
		public void Add(Aggregate<A, B> item) { Add(item.Item1, item.Item2); }

		/// <summary>Add a pair of values to the bidictionary, throwing an exception if either is already in the bidictionary.</summary>
		/// <param name="item"></param>
		public void Add(Tuple<A, B> item) { Add(item.Item1, item.Item2); }

		/// <summary>Clear all values from the bidictionary.</summary>
		public void Clear() {
			AToB.Clear();
			BToA.Clear();
		}

		/// <summary>Get whether the bidictionary contains this specific combination of values.</summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public bool Contains(A a, B b) { return AToB.Contains(new KeyValuePair<A, B>(a, b)); }

		/// <summary>Get whether the bidictionary contains this specific combination of values.</summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public bool Contains(KeyValuePair<A, B> item) { return AToB.Contains(item); }

		/// <summary>Get whether the bidictionary contains this specific combination of values.</summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public bool Contains(KeyValuePair<B, A> item) { return BToA.Contains(item); }

		/// <summary>Get whether the bidictionary contains this specific combination of values.</summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public bool Contains(Aggregate<A, B> item) { return Contains(item.Item1, item.Item2); }

		/// <summary>Get whether the bidictionary contains this specific combination of values.</summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public bool Contains(Tuple<A, B> item) { return Contains(item.Item1, item.Item2); }

		/// <summary>Get whether the bidictionary contains a value.</summary>
		/// <param name="a"></param>
		/// <returns></returns>
		public bool ContainsKey(A a) { return AToB.ContainsKey(a); }

		/// <summary>Get whether the bidictionary contains a value.</summary>
		/// <param name="b"></param>
		/// <returns></returns>
		public bool ContainsKey(B b) { return BToA.ContainsKey(b); }

		/// <summary>Copy the values to an array.</summary>
		/// <param name="array"></param>
		/// <param name="arrayIndex"></param>
		public void CopyTo(KeyValuePair<A, B>[] array, int arrayIndex) { ((IDictionary<A, B>)AToB).CopyTo(array, arrayIndex); }

		/// <summary>Copy the values to an array.</summary>
		/// <param name="array"></param>
		/// <param name="arrayIndex"></param>
		public void CopyTo(KeyValuePair<B, A>[] array, int arrayIndex) { ((IDictionary<B, A>)BToA).CopyTo(array, arrayIndex); }

		/// <summary>Copy the values to an array.</summary>
		/// <param name="array"></param>
		/// <param name="arrayIndex"></param>
		public void CopyTo(Aggregate<A, B>[] array, int arrayIndex) {
			foreach (KeyValuePair<A, B> value in AToB)
				array[arrayIndex++] = Aggregate.Create(value);
		}

		/// <summary>Copy the values to an array.</summary>
		/// <param name="array"></param>
		/// <param name="arrayIndex"></param>
		public void CopyTo(Aggregate<B, A>[] array, int arrayIndex) {
			foreach (KeyValuePair<B, A> value in BToA)
				array[arrayIndex++] = Aggregate.Create(value);
		}

		/// <summary>Copy the values to an array.</summary>
		/// <param name="array"></param>
		/// <param name="arrayIndex"></param>
		public void CopyTo(Tuple<A, B>[] array, int arrayIndex) {
			foreach (KeyValuePair<A, B> value in AToB)
				array[arrayIndex++] = Tuple.Create(value.Key, value.Value);
		}

		/// <summary>Copy the values to an array.</summary>
		/// <param name="array"></param>
		/// <param name="arrayIndex"></param>
		public void CopyTo(Tuple<B, A>[] array, int arrayIndex) {
			foreach (KeyValuePair<B, A> value in BToA)
				array[arrayIndex++] = Tuple.Create(value.Key, value.Value);
		}

		/// <summary>Get an enumerator over the pairs in the bidictionary.</summary>
		/// <returns></returns>
		public Dictionary<A, B>.Enumerator GetEnumerator() { return AToB.GetEnumerator(); }

		/// <summary>Get an enumerator over the values in the bidictionary.</summary>
		/// <returns></returns>
		IEnumerator<KeyValuePair<A, B>> IEnumerable<KeyValuePair<A, B>>.GetEnumerator() { return AToB.GetEnumerator(); }

		/// <summary>Get an enumerator over the pairs in the bidictionary.</summary>
		/// <returns></returns>
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return AToB.GetEnumerator(); }

		/// <summary>Remove a pair of values from the bidictionary, if present.</summary>
		/// <param name="a"></param>
		/// <returns></returns>
		public bool Remove(A a) {
			B b;

			if (!AToB.TryGetValue(a, out b))
				return false;
			AToB.Remove(a);
			BToA.Remove(b);
			return true;
		}

		/// <summary>Remove a pair of values from the bidictionary, if present.</summary>
		/// <param name="b"></param>
		/// <returns></returns>
		public bool Remove(B b) {
			A a;

			if (!BToA.TryGetValue(b, out a))
				return false;
			AToB.Remove(a);
			BToA.Remove(b);
			return true;
		}

		/// <summary>Attempt to remove an element from the bidictionary.</summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public bool Remove(KeyValuePair<A, B> item) {
			if (!((IDictionary<A, B>)AToB).Remove(item))
				return false;
			((IDictionary<B, A>)BToA).Remove(new KeyValuePair<B, A>(item.Value, item.Key));
			return true;
		}

		/// <summary>Attempt to find a <typeparamref name="B"/> value in the bidictionary based on the <typeparamref name="A"/> key.</summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public bool TryGetValue(A key, out B value) { return AToB.TryGetValue(key, out value); }

		/// <summary>Attempt to find a <typeparamref name="A"/> value in the bidictionary based on the <typeparamref name="B"/> key.</summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public bool TryGetValue(B key, out A value) { return BToA.TryGetValue(key, out value); }

		/// <summary>Implicitly return a read-only view of the bidictionary.</summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static implicit operator ReadOnlyBidictionary<A, B>(Bidictionary<A, B> value) { return value.ReadOnlyView; }
	}

	/// <summary>
	/// A read-only view of a <see cref="Bidictionary&lt;A,B&gt;"/>.
	/// </summary>
	/// <typeparam name="A"></typeparam>
	/// <typeparam name="B"></typeparam>
	public class ReadOnlyBidictionary<A, B> : IDictionary<A, B> {
		Dictionary<A, B> AToB { get { return Dictionary.AToB; } }
		Dictionary<B, A> BToA { get { return Dictionary.BToA; } }

		bool ICollection<KeyValuePair<A, B>>.IsReadOnly { get { return true; } }

		/// <summary>Get the number of elements in the bidictionary.</summary>
		public int Count { get { return AToB.Count; } }

		ICollection<A> IDictionary<A, B>.Keys { get { return AToB.Keys; } }

		/// <summary>Get the keys of the A to B dictionary.</summary>
		public Dictionary<A, B>.KeyCollection Keys { get { return AToB.Keys; } }

		ICollection<B> IDictionary<A, B>.Values { get { return AToB.Values; } }

		/// <summary>Get the values of the A to B dictionary.</summary>
		public Dictionary<A, B>.ValueCollection Values { get { return AToB.Values; } }

		B IDictionary<A, B>.this[A key] {
			get { return Dictionary[key]; }
			set { throw ReadOnlyException(); }
		}

		/// <summary>Get a key value from the bidictionary.</summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public B this[A key] { get { return Dictionary[key]; } }

		/// <summary>Get a key value from the bidictionary.</summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public A this[B key] { get { return Dictionary[key]; } }

		readonly Bidictionary<A, B> Dictionary;

		/// <summary>Initialise the bidictionary.</summary>
		/// <param name="dictionary"></param>
		public ReadOnlyBidictionary(Bidictionary<A, B> dictionary) {
			if (dictionary == null)
				throw new ArgumentNullException("dictionary");
			Dictionary = dictionary;
		}

		/// <summary>Get whether the bidictionary contains this specific combination of values.</summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public bool Contains(A a, B b) { return AToB.Contains(new KeyValuePair<A, B>(a, b)); }

		/// <summary>Get whether the bidictionary contains this specific combination of values.</summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public bool Contains(KeyValuePair<A, B> item) { return AToB.Contains(item); }

		/// <summary>Get whether the bidictionary contains this specific combination of values.</summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public bool Contains(KeyValuePair<B, A> item) { return BToA.Contains(item); }

		/// <summary>Get whether the bidictionary contains this specific combination of values.</summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public bool Contains(Aggregate<A, B> item) { return Contains(item.Item1, item.Item2); }

		/// <summary>Get whether the bidictionary contains this specific combination of values.</summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public bool Contains(Tuple<A, B> item) { return Contains(item.Item1, item.Item2); }

		/// <summary>Get whether the bidictionary contains a value.</summary>
		/// <param name="a"></param>
		/// <returns></returns>
		public bool ContainsKey(A a) { return AToB.ContainsKey(a); }

		/// <summary>Get whether the bidictionary contains a value.</summary>
		/// <param name="b"></param>
		/// <returns></returns>
		public bool ContainsKey(B b) { return BToA.ContainsKey(b); }

		/// <summary>Copy the values to an array.</summary>
		/// <param name="array"></param>
		/// <param name="arrayIndex"></param>
		public void CopyTo(KeyValuePair<A, B>[] array, int arrayIndex) { ((IDictionary<A, B>)AToB).CopyTo(array, arrayIndex); }

		/// <summary>Copy the values to an array.</summary>
		/// <param name="array"></param>
		/// <param name="arrayIndex"></param>
		public void CopyTo(KeyValuePair<B, A>[] array, int arrayIndex) { ((IDictionary<B, A>)BToA).CopyTo(array, arrayIndex); }

		/// <summary>Copy the values to an array.</summary>
		/// <param name="array"></param>
		/// <param name="arrayIndex"></param>
		public void CopyTo(Aggregate<A, B>[] array, int arrayIndex) {
			foreach (KeyValuePair<A, B> value in AToB)
				array[arrayIndex++] = Aggregate.Create(value);
		}

		/// <summary>Copy the values to an array.</summary>
		/// <param name="array"></param>
		/// <param name="arrayIndex"></param>
		public void CopyTo(Aggregate<B, A>[] array, int arrayIndex) {
			foreach (KeyValuePair<B, A> value in BToA)
				array[arrayIndex++] = Aggregate.Create(value);
		}

		/// <summary>Copy the values to an array.</summary>
		/// <param name="array"></param>
		/// <param name="arrayIndex"></param>
		public void CopyTo(Tuple<A, B>[] array, int arrayIndex) {
			foreach (KeyValuePair<A, B> value in AToB)
				array[arrayIndex++] = Tuple.Create(value.Key, value.Value);
		}

		/// <summary>Copy the values to an array.</summary>
		/// <param name="array"></param>
		/// <param name="arrayIndex"></param>
		public void CopyTo(Tuple<B, A>[] array, int arrayIndex) {
			foreach (KeyValuePair<B, A> value in BToA)
				array[arrayIndex++] = Tuple.Create(value.Key, value.Value);
		}

		/// <summary>Get an enumerator over the pairs in the bidictionary.</summary>
		/// <returns></returns>
		public Dictionary<A, B>.Enumerator GetEnumerator() { return AToB.GetEnumerator(); }

		/// <summary>Get an enumerator over the values in the bidictionary.</summary>
		/// <returns></returns>
		IEnumerator<KeyValuePair<A, B>> IEnumerable<KeyValuePair<A, B>>.GetEnumerator() { return AToB.GetEnumerator(); }

		/// <summary>Get an enumerator over the pairs in the bidictionary.</summary>
		/// <returns></returns>
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return AToB.GetEnumerator(); }

		/// <summary>Attempt to find a <typeparamref name="B"/> value in the bidictionary based on the <typeparamref name="A"/> key.</summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public bool TryGetValue(A key, out B value) { return AToB.TryGetValue(key, out value); }

		/// <summary>Attempt to find a <typeparamref name="A"/> value in the bidictionary based on the <typeparamref name="B"/> key.</summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public bool TryGetValue(B key, out A value) { return BToA.TryGetValue(key, out value); }

		Exception ReadOnlyException() { return new NotSupportedException("This is a read-only view of a " + typeof(Bidictionary<,>).Name + " type and cannot be modified."); }

		void ICollection<KeyValuePair<A, B>>.Add(KeyValuePair<A, B> item) { throw ReadOnlyException(); }
		void ICollection<KeyValuePair<A, B>>.Clear() { throw ReadOnlyException(); }
		bool ICollection<KeyValuePair<A, B>>.Remove(KeyValuePair<A, B> item) { throw ReadOnlyException(); }

		void IDictionary<A, B>.Add(A key, B value) { throw ReadOnlyException(); }
		bool IDictionary<A, B>.Remove(A key) { throw ReadOnlyException(); }
	}
}
