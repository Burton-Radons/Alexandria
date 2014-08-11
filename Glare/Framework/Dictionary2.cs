using Glare.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glare.Framework {
	/// <summary>A dictionary with two keys.</summary>
	/// <typeparam name="TKey1"></typeparam>
	/// <typeparam name="TKey2"></typeparam>
	/// <typeparam name="TValue"></typeparam>
	public class Dictionary<TKey1, TKey2, TValue> : IDictionary<Aggregate<TKey1, TKey2>, TValue> {
		Dictionary<TKey1, Dictionary<TKey2, TValue>> Dictionary1 = new Dictionary<TKey1, Dictionary<TKey2, TValue>>();

		Dictionary<TKey2, TValue> GetDictionary2(TKey1 key1) { return Dictionary1.GetValueOrCreate(key1); }
		
		/// <summary>Add an element to the dictionary.</summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		public void Add(Aggregate<TKey1, TKey2> key, TValue value) { GetDictionary2(key.Item1).Add(key.Item2, value); }

		/// <summary>Add an element to the dictionary.</summary>
		/// <param name="key1"></param>
		/// <param name="key2"></param>
		/// <param name="value"></param>
		public void Add(TKey1 key1, TKey2 key2, TValue value) { GetDictionary2(key1).Add(key2, value); }

		/// <summary>Get whether the dictionary contains an item.</summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public bool ContainsKey(Aggregate<TKey1, TKey2> key) {
			Dictionary<TKey2, TValue> dictionary2;
			if (!Dictionary1.TryGetValue(key.Item1, out dictionary2))
				return false;
			return dictionary2.ContainsKey(key.Item2);
		}

		/// <summary>Get whehter the dictionary contains a key.</summary>
		/// <param name="key1"></param>
		/// <param name="key2"></param>
		/// <returns></returns>
		public bool ContainsKey(TKey1 key1, TKey2 key2) {
			Dictionary<TKey2, TValue> dictionary2;
			if (!Dictionary1.TryGetValue(key1, out dictionary2))
				return false;
			return dictionary2.ContainsKey(key2);
		}

		/// <summary>Get the keys of the dictionary.</summary>
		public ICollection<Aggregate<TKey1, TKey2>> Keys {
			get {
				var list = new List<Aggregate<TKey1, TKey2>>();
				foreach (var pair1 in Dictionary1)
					foreach (var pair2 in pair1.Value)
						list.Add(new Aggregate<TKey1, TKey2>(pair1.Key, pair2.Key));
				return list;
			}
		}

		/// <summary>Remove an element from the dictionary.</summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public bool Remove(Aggregate<TKey1, TKey2> key) {
			var dictionary2 = Dictionary1.TryGetValue(key.Item1);
			if (dictionary2 == null)
				return false;
			return dictionary2.Remove(key.Item2);
		}

		/// <summary>Remove an element from the dictionary.</summary>
		/// <param name="key1"></param>
		/// <param name="key2"></param>
		/// <returns></returns>
		public bool Remove(TKey1 key1, TKey2 key2) {
			var dictionary2 = Dictionary1.TryGetValue(key1);
			if (dictionary2 == null)
				return false;
			return dictionary2.Remove(key2);
		}

		/// <summary>Attempt to get a value from the dictionary.</summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public bool TryGetValue(Aggregate<TKey1, TKey2> key, out TValue value) {
			Dictionary<TKey2, TValue> dictionary2;
			if (Dictionary1.TryGetValue(key.Item1, out dictionary2))
				return dictionary2.TryGetValue(key.Item2, out value);
			value = default(TValue);
			return false;
		}

		/// <summary>Attempt to get a value from the dictionary.</summary>
		/// <param name="key1"></param>
		/// <param name="key2"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public bool TryGetValue(TKey1 key1, TKey2 key2, out TValue value) {
			Dictionary<TKey2, TValue> dictionary2;
			if (Dictionary1.TryGetValue(key1, out dictionary2))
				return dictionary2.TryGetValue(key2, out value);
			value = default(TValue);
			return false;
		}

		/// <summary>Get the collection of values of the dictionary.</summary>
		public ICollection<TValue> Values {
			get {
				List<TValue> list = new List<TValue>();
				foreach (var pair1 in Dictionary1)
					foreach (var pair2 in pair1.Value)
						list.Add(pair2.Value);
				return list;
			}
		}

		/// <summary>Get or set an element of the dictionary.</summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public TValue this[Aggregate<TKey1, TKey2> key] {
			get { return Dictionary1[key.Item1][key.Item2]; }
			set { Dictionary1.GetValueOrCreate(key.Item1)[key.Item2] = value; }
		}

		/// <summary>Get or set an element of the dictionary.</summary>
		/// <param name="key1"></param>
		/// <param name="key2"></param>
		/// <returns></returns>
		public TValue this[TKey1 key1, TKey2 key2] {
			get { return Dictionary1[key1][key2]; }
			set { Dictionary1.GetValueOrCreate(key1)[key2] = value; }
		}

		/// <summary>Add an element to the dictionary.</summary>
		/// <param name="item"></param>
		public void Add(KeyValuePair<Aggregate<TKey1, TKey2>, TValue> item) { Add(item.Key, item.Value); }

		/// <summary>Remove all items from the dictionary.</summary>
		public void Clear() { Dictionary1.Clear(); }

		/// <summary>Get whether an element is contained in the dictionary.</summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public bool Contains(KeyValuePair<Aggregate<TKey1, TKey2>, TValue> item) {
			var dictionary2 = Dictionary1.TryGetValue(item.Key.Item1);
			if (dictionary2 == null)
				return false;
			return dictionary2.Contains(new KeyValuePair<TKey2, TValue>(item.Key.Item2, item.Value));
		}

		/// <summary>Copy the dictionary's values into an array.</summary>
		/// <param name="array"></param>
		/// <param name="arrayIndex"></param>
		public void CopyTo(KeyValuePair<Aggregate<TKey1, TKey2>, TValue>[] array, int arrayIndex) {
			foreach(var pair1 in Dictionary1)
				foreach(var pair2 in pair1.Value)
					array[arrayIndex++] = new KeyValuePair<Aggregate<TKey1,TKey2>,TValue>(new Aggregate<TKey1,TKey2>(pair1.Key, pair2.Key), pair2.Value);
		}

		/// <summary>Get the number of elements in the dictionary.</summary>
		public int Count {
			get {
				int count = 0;
				foreach (var pair1 in Dictionary1)
					count += pair1.Value.Count;
				return count;
			}
		}

		/// <summary>Returns <c>false</c>.</summary>
		public bool IsReadOnly { get { return false; } }

		/// <summary>Attempt to remove a row from the dictionary.</summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public bool Remove(KeyValuePair<Aggregate<TKey1, TKey2>, TValue> item) {
			var dictionary2 = Dictionary1.TryGetValue(item.Key.Item1);
			if (dictionary2 == null)
				return false;
			return ((IDictionary<TKey2, TValue>)dictionary2).Remove(new KeyValuePair<TKey2, TValue>(item.Key.Item2, item.Value));
		}

		/// <summary>Get an enumerator over the elements of the dictionary.</summary>
		/// <returns></returns>
		public IEnumerator<KeyValuePair<Aggregate<TKey1, TKey2>, TValue>> GetEnumerator() {
			foreach (var pair1 in Dictionary1)
				foreach (var pair2 in pair1.Value)
					yield return new KeyValuePair<Aggregate<TKey1, TKey2>, TValue>(new Aggregate<TKey1, TKey2>(pair1.Key, pair2.Key), pair2.Value);
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
	}
}
