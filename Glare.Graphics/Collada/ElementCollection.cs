using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.Specialized;

namespace Glare.Graphics.Collada {
	/// <summary>An observable collection of <see cref="Element"/> objects that hooks into the <see cref="Element.Parent"/> and <see cref="Element.Children"/> properties. Element collections can only be assigned to one <see cref="Element"/>; the property must be cleared or changed to another collection before the collection can be assigned to another <see cref="Element"/>.</summary>
	/// <typeparam name="T">The type of the <see cref="Element"/>.</typeparam>
	public class ElementCollection<T> : IList<T>, INotifyPropertyChanged, INotifyCollectionChanged where T : Element {
		readonly List<T> List;

		public Element Parent { get; internal set; }

		public T this[int index] {
			get { return List[index]; }

			set {
				if (value == null)
					throw new ArgumentNullException("value");
				if (value.Parent != null)
					throw new ArgumentException("Value already has a parent element.", "value");

				if (index < Count)
					RemoveAt(index);
				Insert(index, value);
			}
		}

		public event NotifyCollectionChangedEventHandler CollectionChanged;

		public event PropertyChangedEventHandler PropertyChanged;

		public ElementCollection() { List = new List<T>(); }
		public ElementCollection(int capacity) { List = new List<T>(capacity); }
		public ElementCollection(IEnumerable<T> collection) { List = new List<T>(collection); }
		public ElementCollection(params T[] collection) : this((IEnumerable<T>)collection) { }

		public void Add(T item) { Insert(Count, item); }

		public void AddRange(IEnumerable<T> collection) { foreach (T item in collection) Add(item); }
		public void AddRange(params T[] collection) { foreach (T item in collection) Add(item); }

		public void Clear() { while (Count > 0) RemoveAt(Count - 1); }

		public bool Contains(T item) { return List.Contains(item); }

		public void CopyTo(T[] array, int arrayIndex) { List.CopyTo(array, arrayIndex); }

		public int Count { get { return List.Count; } }

		public List<T>.Enumerator GetEnumerator() { return List.GetEnumerator(); }

		IEnumerator<T> IEnumerable<T>.GetEnumerator() { return GetEnumerator(); }

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return GetEnumerator(); }

		public int IndexOf(T item) { return List.IndexOf(item); }

		public void Insert(int index, T item) {
			if (item == null)
				throw new ArgumentNullException("item");
			if (item.Parent != null)
				throw new ArgumentException("Item already has a parent element.", "item");

			Collada document = Parent != null ? Parent.OwnerDocument : null;

			if(Parent != null)
				item.Check(document, Parent);

			// Insert here to throw an exception if index is out of range or we're out of memory.
			List.Insert(index, item);

			if (Parent != null)
				item.Attach(document, Parent);

			OnCountChanged();
			OnCollectionChanged(NotifyCollectionChangedAction.Add, item, index);
		}

		bool ICollection<T>.IsReadOnly { get { return false; } }

		protected virtual void OnCollectionChanged(NotifyCollectionChangedAction action, T changedValue, int index) {
			if (CollectionChanged != null)
				CollectionChanged(this, new NotifyCollectionChangedEventArgs(action, changedValue, index));
		}

		void OnCountChanged() { OnPropertyChanged("Count"); }

		protected virtual void OnPropertyChanged(string propertyName) {
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

		public bool Remove(T item) {
			int index = IndexOf(item);

			if (index < 0)
				return false;
			RemoveAt(index);
			return true;
		}

		public void RemoveAt(int index) {
			T item = List[index];

			List.RemoveAt(index);
			if (Parent != null)
				item.Detach(Parent.OwnerDocument, Parent);

			OnCountChanged();
			OnCollectionChanged(NotifyCollectionChangedAction.Remove, item, index);
		}
	}
}
