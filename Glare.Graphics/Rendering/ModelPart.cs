using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics.Rendering
{
	/// <summary>
	/// A part of a <see cref="ModelMesh"/>. This is an individual draw call.
	/// </summary>
	public class ModelPart : ModelObject
	{
		internal ModelMesh mesh;

		readonly ModelMaterial material;
		readonly Primitive primitive;
		readonly int offset;
		readonly int count;

		/// <summary>Get the number of vertices to render.</summary>
		public int Count { get { return count; } }

		/// <summary>Get the <see cref="ModelMaterial"/> to use for this <see cref="ModelPart"/>.</summary>
		public ModelMaterial Material { get { return material; } }

		/// <summary>Get the offset in bytes from the beginning of the element buffer to the first vertex index.</summary>
		public int Offset { get { return offset; } }

		/// <summary>Get the primitive type to draw.</summary>
		public Primitive Primitive { get { return primitive; } }

		public ModelPart(Primitive primitive, int offset, int count, ModelMaterial material)
		{
			this.primitive = primitive;
			this.offset = offset;
			this.count = count;
			this.material = material;
		}
	}
	
	public class ModelPartCollection : IList<ModelPart>, INotifyCollectionChanged, INotifyPropertyChanged
	{
		readonly ModelMesh mesh;
		readonly List<ModelPart> list = new List<ModelPart>();

		public event NotifyCollectionChangedEventHandler CollectionChanged;

		public event PropertyChangedEventHandler PropertyChanged;

		internal ModelPartCollection(ModelMesh mesh)
		{
			this.mesh = mesh;
		}

		public int IndexOf(ModelPart item) { return list.IndexOf(item); }

		public ModelPart Insert(int index, ModelPart item)
		{
			if (item == null)
				throw new ArgumentNullException("item");

			if (item.mesh != null)
				item.mesh.parts.Remove(item);
			list.Insert(index, item);
			item.mesh = mesh;
			item.model = mesh.model;
			if (CollectionChanged != null)
				CollectionChanged.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
			if (PropertyChanged != null)
				PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Count"));
			return item;
		}

		void IList<ModelPart>.Insert(int index, ModelPart item) { Insert(index, item); }

		public void RemoveAt(int index)
		{
			var item = list[index];
			list.RemoveAt(index);
			item.mesh = null;
			item.model = null;
			if (CollectionChanged != null)
				CollectionChanged.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index));
			if (PropertyChanged != null)
				PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Count"));
		}

		public ModelPart this[int index]
		{
			get { return list[index]; }

			set
			{
				if (index < 0 || index >= Count)
					throw new ArgumentOutOfRangeException("index");
				Insert(index, value);
				RemoveAt(index + 1);
			}
		}

		public ModelPart Add(ModelPart item) { return Insert(Count, item); }
		void ICollection<ModelPart>.Add(ModelPart item) { Add(item); }

		public void Clear() { while (Count > 0) RemoveAt(Count - 1); }

		public bool Contains(ModelPart item) { return list.Contains(item); }

		public void CopyTo(ModelPart[] array, int arrayIndex) { list.CopyTo(array, arrayIndex); }

		public int Count { get { return list.Count; } }

		bool ICollection<ModelPart>.IsReadOnly { get { return false; } }

		public bool Remove(ModelPart item)
		{
			int index = IndexOf(item);
			if (index < 0) return false;
			RemoveAt(index);
			return true;
		}

		public List<ModelPart>.Enumerator GetEnumerator() { return list.GetEnumerator(); }
		IEnumerator<ModelPart> IEnumerable<ModelPart>.GetEnumerator() { return GetEnumerator(); }
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return GetEnumerator(); }
	}
}
