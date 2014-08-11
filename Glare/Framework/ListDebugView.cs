using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Framework {
	/*
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(ListDebugView<>))]
	 
	 */

	/// <summary>
	/// A debug view on a <see cref="ICollection&lt;T&gt;"/>-supporting object can be used with this.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <remarks>
	/// To use, add these custom attributes:
	/// 
	/// <code>
	///	[DebuggerDisplay("Count = {Count}")]
	///	[DebuggerTypeProxy(typeof(ListDebugView&lt;&gt;)}]
	///	</code>
	/// </remarks>
	public class ListDebugView<T> {
		private ICollection<T> collection;

		/// <summary>
		/// Initialise the debug view.
		/// </summary>
		/// <param name="collection"></param>
		public ListDebugView(ICollection<T> collection) {
			if (collection == null)
				throw new ArgumentNullException("collection");
			this.collection = collection;
		}

		/// <summary>Present the items of the collection.</summary>
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items {
			get {
				T[] array = new T[this.collection.Count];
				this.collection.CopyTo(array, 0);
				return array;
			}
		}
	}

	/// <summary>
	/// A debug view for a dictionary.
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	/// <typeparam name="TValue"></typeparam>
	public class DictionaryDebugView<TKey, TValue> {
		private ICollection<KeyValuePair<TKey, TValue>> collection;

		/// <summary>
		/// Initialise the debug view.
		/// </summary>
		/// <param name="collection"></param>
		public DictionaryDebugView(ICollection<KeyValuePair<TKey, TValue>> collection) {
			if (collection == null)
				throw new ArgumentNullException("collection");
			this.collection = collection;
		}

		/// <summary>
		/// Get the array of items for browsing the dictionary.
		/// </summary>
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public KeyValuePair<TKey, TValue>[] Items {
			get {
				KeyValuePair<TKey, TValue>[] array = new KeyValuePair<TKey, TValue>[this.collection.Count];
				this.collection.CopyTo(array, 0);
				return array;
			}
		}
	}
}
