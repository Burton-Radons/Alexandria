using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics.Rendering
{
	public class ModelBone : ModelObject
	{
		internal int index = -1;
		ModelBoneChildrenCollection children;
		internal ModelBone parent;
		Matrix4d transform;

		/// <summary>Get the children of the bone.</summary>
		public ModelBoneChildrenCollection Children { get { return children; } }

		/// <summary>Get the zero-based index of this bone in the <see cref="Model.Bones"/> collection, or -1 if it's not part of a <see cref="Model"/>.</summary>
		public int Index { get { return index; } }

		/// <summary>Get or set the bone that this bone's transformation is based off of.</summary>
		public ModelBone Parent
		{
			get { return parent; }

			set
			{
				if (value != null)
					value.children.Add(this);
				else if (parent != null)
					parent.children.Remove(this);
			}
		}

		/// <summary>Get or set the matrix that transforms this bone relative to its parent. The initial value is the identity matrix.</summary>
		public Matrix4d Transform { get { return transform; } set { transform = value; } }

		/// <summary>Get the <see cref="Transform"/> applied to the <see cref="Parent"/>'s <see cref="WorldTransform"/>, if this <see cref="ModelBone"/> has a <see cref="Parent"/>.</summary>
		public Matrix4d WorldTransform { get { Matrix4d result; GetWorldTransform(out result); return result; } }

		public ModelBone()
		{
			children = new ModelBoneChildrenCollection(this);
			Transform = Matrix4d.Identity;
		}

		public void GetWorldTransform(out Matrix4d result)
		{
			result = Transform;
			for (ModelBone parent = Parent; parent != null; parent = parent.parent)
				result.Multiply(ref parent.transform, out result);
		}
	}

	/// <summary>
	/// A list of <see cref="ModelBone"/> objects that are children of another <see cref="ModelBone"/> in its <see cref="ModelBone.Children"/> property.
	/// </summary>
	public class ModelBoneChildrenCollection : IList<ModelBone>, INotifyCollectionChanged, INotifyPropertyChanged
	{
		readonly ModelBone bone;
		internal readonly List<ModelBone> list = new List<ModelBone>();

		public event NotifyCollectionChangedEventHandler CollectionChanged;

		public event PropertyChangedEventHandler PropertyChanged;

		internal ModelBoneChildrenCollection(ModelBone bone)
		{
			this.bone = bone;
		}

		public int IndexOf(ModelBone item) { return list.IndexOf(item); }

		public void Insert(int index, ModelBone item)
		{
			if (item == null)
				throw new ArgumentNullException("item");
			if (item.parent != null)
				throw new ArgumentException("item");

			for (ModelBone parent = bone; parent != null; parent = parent.parent)
				if (object.ReferenceEquals(parent, item))
					throw new ArgumentException("item");

			if (!object.ReferenceEquals(item.model, bone.model))
			{
				if (item.model != null)
					item.model.Bones.Remove(item);
				if (bone.model != null)
					bone.model.Bones.Add(item);
			}

			list.Insert(index, item);
			item.parent = bone;
		}

		public void RemoveAt(int index)
		{
			var item = list[index];
			list.RemoveAt(index);
			item.parent = null;

			if (CollectionChanged != null)
				CollectionChanged.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index));
			if (PropertyChanged != null)
				PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Count"));
		}

		public ModelBone this[int index]
		{
			get { return list[index]; }
			set { Insert(index, value); RemoveAt(index + 1); }
		}

		public ModelBone Add(ModelBone item) { Insert(Count, item); return item; }

		void ICollection<ModelBone>.Add(ModelBone item) { Add(item); }

		public void Clear() { while (Count > 0) RemoveAt(Count - 1); }

		public bool Contains(ModelBone item) { return list.Contains(item); }

		public void CopyTo(ModelBone[] array, int arrayIndex) { list.CopyTo(array, arrayIndex); }

		public int Count { get { return list.Count; } }

		bool ICollection<ModelBone>.IsReadOnly { get { return false; } }

		public bool Remove(ModelBone item)
		{
			int index = IndexOf(item);
			if (index < 0) return false;
			RemoveAt(index);
			return true;
		}

		public List<ModelBone>.Enumerator GetEnumerator() { return list.GetEnumerator(); }

		IEnumerator<ModelBone> IEnumerable<ModelBone>.GetEnumerator() { return GetEnumerator(); }

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return GetEnumerator(); }
	}

	/// <summary>
	/// A list of <see cref="ModelBone"/> objects that are contained by a <see cref="ModelMesh"/>.
	/// </summary>
	public class ModelBoneCollection : IList<ModelBone>, INotifyCollectionChanged, INotifyPropertyChanged
	{
		readonly Model model;
		internal readonly List<ModelBone> list = new List<ModelBone>();

		public event NotifyCollectionChangedEventHandler CollectionChanged;

		public event PropertyChangedEventHandler PropertyChanged;

		internal ModelBoneCollection(Model model)
		{
			this.model = model;
		}

		public int IndexOf(ModelBone item) { return list.IndexOf(item); }

		public void Insert(int index, ModelBone item)
		{
			int insertIndex = index;
			List<ModelBone> added = null;

			lock (this)
				lock (item)
				{
					if (item == null)
						throw new ArgumentNullException("item");
					if (item.model != null)
						throw new ArgumentException("This " + typeof(ModelBone).Name + " is already part of a " + typeof(Model).Name + ".", "item");
					InsertChildren(item, ref insertIndex);
					for (int update = insertIndex; update < list.Count; update++)
						list[update].index = update;

					if (CollectionChanged != null)
					{
						added = new List<ModelBone>();
						for (int update = index; update < insertIndex; update++)
							added.Add(list[update]);
					}
				}
			if (CollectionChanged != null)
				CollectionChanged.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, added, index));
			if (PropertyChanged != null)
				PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Count"));
		}

		void InsertChildren(ModelBone bone, ref int index)
		{
			list.Insert(index, bone);
			bone.model = model;
			bone.index = index;
			index++;

			foreach (ModelBone child in bone.Children)
				InsertChildren(child, ref index);
		}

		public void RemoveAt(int index)
		{
			var item = list[index];

			lock (this)
				lock (item)
				{
					RemoveBone(item, index);
					if (item.parent != null)
					{
						item.parent.Children.Remove(item);
						item.parent = null;
					}
				}
			if (PropertyChanged != null)
				PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Count"));
		}

		void RemoveBone(ModelBone bone, int index)
		{
			list.RemoveAt(index);
			bone.model = null;
			bone.index = -1;
			for (int update = index; update < list.Count; update++)
				list[update].index = update;
			if (CollectionChanged != null)
				CollectionChanged.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, bone, index));
			foreach (ModelBone child in bone.Children)
			{
				index = list.IndexOf(child);
				RemoveBone(child, index);
			}
		}

		public ModelBone this[int index]
		{
			get { return list[index]; }

			set { Insert(index, value); RemoveAt(index + 1); }
		}

		public ModelBone Add(ModelBone item) { Insert(Count, item); return item; }

		void ICollection<ModelBone>.Add(ModelBone item) { Add(item); }

		public void Clear() { while (Count > 0) RemoveAt(Count - 1); }

		public bool Contains(ModelBone item) { return list.Contains(item); }

		public void CopyTo(ModelBone[] array, int arrayIndex) { list.CopyTo(array, arrayIndex); }

		public int Count { get { return list.Count; } }

		bool ICollection<ModelBone>.IsReadOnly { get { return false; } }

		public bool Remove(ModelBone item)
		{
			int index = IndexOf(item);
			if (index < 0) return false;
			RemoveAt(index);
			return true;
		}

		public List<ModelBone>.Enumerator GetEnumerator() { return list.GetEnumerator(); }

		IEnumerator<ModelBone> IEnumerable<ModelBone>.GetEnumerator() { return GetEnumerator(); }
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return GetEnumerator(); }
	}
}
