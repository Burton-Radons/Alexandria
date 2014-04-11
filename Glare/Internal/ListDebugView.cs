using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Internal {
	class ListDebugView<T> {
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
}
