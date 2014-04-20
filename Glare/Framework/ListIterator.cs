using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Framework {
	/// <summary>Provides a generic lightweight iterator for a list type.</summary>
	/// <typeparam name="TList">The type of the list; implements <see cref="IList&lt;&gt;"/>.</typeparam>
	/// <typeparam name="TElement">The type of the elements of the list.</typeparam>
	public struct ListEnumerator<TList, TElement> : IEnumerator<TElement>
		where TList : IList<TElement> {
		readonly TList List;
		int Index;

		public ListEnumerator(TList list) {
			if (list == null)
				throw new ArgumentNullException("list");
			List = list;
			Index = -1;
		}

		public TElement Current { get { return List[Index]; } }

		public void Dispose() { Reset(); }

		object IEnumerator.Current { get { return List[Index]; } }

		public bool MoveNext() { return ++Index < List.Count; }

		public void Reset() { Index = -1; }
	}
}
