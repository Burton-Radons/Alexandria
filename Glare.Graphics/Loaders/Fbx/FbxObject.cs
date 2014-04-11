using Glare.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Glare.Graphics.Loaders.Fbx {
	/// <summary>
	/// Root class of the FBX object hierarchy.
	/// </summary>
	public abstract class FbxObject {
		[ThreadStatic]
		static Random Random = new System.Random();

		/// <summary>Get the class of this <see cref="FbxObject"/>.</summary>
		public FbxClass ClassId { get; private set; }

		public FbxObjectDestinationCollection DestinationObjects { get; private set; }

		/// <summary>Get or set the initial name of this object, which is the name the object had when it was loaded from the file.</summary>
		public string InitialName { get; set; }

		/// <summary>Get or set the name of this object.</summary>
		public string Name { get; set; }

		/// <summary>Get or set the name space of this object.</summary>
		public string NameSpace { get; set; }

		public FbxObjectSourceCollection SourceObjects { get; private set; }

		/// <summary>Get a unique identifier of this object.</summary>
		public ulong UniqueID { get; private set; }

		public FbxObject() {
			UniqueID = AllocateUniqueID();
			ClassId = FbxClass.FromType(GetType());
			DestinationObjects = new FbxObjectDestinationCollection(this);
			SourceObjects = new FbxObjectSourceCollection(this);
		}

		static ulong AllocateUniqueID() { return Random.NextFullUInt64(); }

	}

	public abstract class FbxObjectConnectionCollection : IList<FbxObject> {
		public FbxObject Object { get; private set; }

		internal FbxObjectConnectionCollection(FbxObject @object) {
			Object = @object;
		}

		public int IndexOf(FbxObject item) {
			throw new NotImplementedException();
		}

		public void Insert(int index, FbxObject item) {
			throw new NotImplementedException();
		}

		public void RemoveAt(int index) {
			throw new NotImplementedException();
		}

		public FbxObject this[int index] {
			get {
				throw new NotImplementedException();
			}
			set {
				throw new NotImplementedException();
			}
		}

		public void Add(FbxObject item) {
			throw new NotImplementedException();
		}

		public void Clear() {
			throw new NotImplementedException();
		}

		public bool Contains(FbxObject item) {
			throw new NotImplementedException();
		}

		public void CopyTo(FbxObject[] array, int arrayIndex) {
			throw new NotImplementedException();
		}

		public int Count {
			get { throw new NotImplementedException(); }
		}

		public bool IsReadOnly {
			get { throw new NotImplementedException(); }
		}

		public bool Remove(FbxObject item) {
			throw new NotImplementedException();
		}

		public IEnumerator<FbxObject> GetEnumerator() {
			throw new NotImplementedException();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
			throw new NotImplementedException();
		}
	}

	public class FbxObjectDestinationCollection : FbxObjectConnectionCollection {
		internal FbxObjectDestinationCollection(FbxObject @object)
			: base(@object) {
		}
	}

	public class FbxObjectSourceCollection : FbxObjectConnectionCollection {
		internal FbxObjectSourceCollection(FbxObject @object)
			: base(@object) {
		}
	}
}
