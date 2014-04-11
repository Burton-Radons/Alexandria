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
	/// An individual mesh in a <see cref="Model"/>.
	/// </summary>
	public class ModelMesh : ModelObject
	{
		internal ModelBone bone;
		internal readonly ModelPartCollection parts;
		Sphere3d bounds;

		/// <summary>Get or set the bone this mesh uses.</summary>
		public ModelBone Bone
		{
			get { return bone; }

			set
			{
				if (value != null && !object.ReferenceEquals(value.model, Model))
					throw new Exception("The given bone is not part of the same model as this mesh.");
				bone = value;
			}
		}

		/// <summary>Get or set a bounding <see cref="Sphere3d"/> that can be used for clipping. The default is empty.</summary>
		public Sphere3d Bounds { get { return bounds; } set { bounds = value; } }

		public ModelPartCollection Parts { get { return parts; } }

		public ModelMesh()
		{
			parts = new ModelPartCollection(this);
		}

		public ModelMesh(Sphere3d bounds, ModelBone bone, IEnumerable<ModelPart> parts) : this()
		{
			if (bone != null && bone.model != null)
				bone.model.Meshes.Add(this);
			Bounds = bounds;
			Bone = bone;
			foreach (var item in parts)
				Parts.Add(item);
		}

		public ModelMesh(Sphere3d bounds, ModelBone bone, params ModelPart[] parts) : this(bounds, bone, (IEnumerable<ModelPart>)parts) { }
	}

	public class ModelMeshCollection : IList<ModelMesh>, INotifyCollectionChanged, INotifyPropertyChanged
	{
		readonly Model model;
		readonly List<ModelMesh> list = new List<ModelMesh>();

		public event NotifyCollectionChangedEventHandler CollectionChanged;

		public event PropertyChangedEventHandler PropertyChanged;

		internal ModelMeshCollection(Model model)
		{
			this.model = model;
		}

		public int IndexOf(ModelMesh item) { return list.IndexOf(item); }

		public ModelMesh Insert(int index, ModelMesh item)
		{
			if (item == null)
				throw new ArgumentNullException("item");
			if (index < 0 || index > Count)
				throw new ArgumentOutOfRangeException("index");
			if (item.model != null)
				item.model.Meshes.Remove(item);
			list.Insert(index, item);
			item.model = model;
			if (CollectionChanged != null)
				CollectionChanged.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
			if (PropertyChanged != null)
				PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Count"));
			return item;
		}

		void IList<ModelMesh>.Insert(int index, ModelMesh item) { Insert(index, item); }

		public void RemoveAt(int index)
		{
			var item = list[index];
			list.RemoveAt(index);
			item.model = null;
			if (CollectionChanged != null)
				CollectionChanged.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index));
			if (PropertyChanged != null)
				PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Count"));
		}

		public ModelMesh this[int index]
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

		public void Add(ModelMesh item) { Insert(Count, item); }

		public void Clear() { while (Count > 0) RemoveAt(Count - 1); }

		public bool Contains(ModelMesh item) { return list.Contains(item); }

		public void CopyTo(ModelMesh[] array, int arrayIndex) { list.CopyTo(array, arrayIndex); }

		public int Count { get { return list.Count; } }

		bool ICollection<ModelMesh>.IsReadOnly { get { return false; } }

		public bool Remove(ModelMesh item)
		{
			int index = IndexOf(item);
			if (index < 0) return false;
			RemoveAt(index);
			return true;
		}

		public List<ModelMesh>.Enumerator GetEnumerator() { return list.GetEnumerator(); }

		IEnumerator<ModelMesh> IEnumerable<ModelMesh>.GetEnumerator() { return GetEnumerator(); }

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return GetEnumerator(); }
	}
}
