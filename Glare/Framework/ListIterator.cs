using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Framework {
	/// <summary>Provides a generic lightweight iterator for a list type.</summary>
	/// <typeparam name="TList">The type of the list; implements <see cref="IList{T}"/>.</typeparam>
	/// <typeparam name="TElement">The type of the elements of the list.</typeparam>
	public struct ListEnumerator<TList, TElement> : IEnumerator<TElement>
		where TList : IList<TElement> {
		readonly TList List;
		int Index;

		/// <summary>
		/// Initialise the list iterator.
		/// </summary>
		/// <param name="list"></param>
		public ListEnumerator(TList list) {
			if (list == null)
				throw new ArgumentNullException("list");
			List = list;
			Index = -1;
		}

		/// <summary>Get the current element.</summary>
		public TElement Current { get { return List[Index]; } }

		/// <summary>Does nothing.</summary>
		public void Dispose() { Reset(); }

		object IEnumerator.Current { get { return List[Index]; } }

		/// <summary>Move to the next element, returning whether the end has been reached.</summary>
		/// <returns></returns>
		public bool MoveNext() { return ++Index < List.Count; }

		/// <summary>Reset the enumerator to the start of the list.</summary>
		public void Reset() { Index = -1; }
	}
}
