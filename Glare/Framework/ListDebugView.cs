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

		public ListDebugView(ICollection<T> collection) {
			if (collection == null)
				throw new ArgumentNullException("collection");
			this.collection = collection;
		}

		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items {
			get {
				T[] array = new T[this.collection.Count];
				this.collection.CopyTo(array, 0);
				return array;
			}
		}
	}

	public class DictionaryDebugView<TKey, TValue> {
		private ICollection<KeyValuePair<TKey, TValue>> collection;

		public DictionaryDebugView(ICollection<KeyValuePair<TKey, TValue>> collection) {
			if (collection == null)
				throw new ArgumentNullException("collection");
			this.collection = collection;
		}

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
