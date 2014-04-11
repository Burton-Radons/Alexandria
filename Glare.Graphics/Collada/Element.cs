using Glare.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Glare.Graphics.Collada {
	public abstract class Element {
		internal const string XmlIdAttribute = "id";
		internal const string XmlNameAttribute = "name";
		internal const string XmlScopedIdAttribute = "sid";

		Element parent;

		/// <summary>The mutable list of children of the <see cref="Element"/>.</summary>
		internal readonly ArrayBackedList<Element> ChildrenMutable = new ArrayBackedList<Element>();

		/// <summary>Get the collection of direction children to this <see cref="Element"/>.</summary>
		[XmlIgnore]
		public ReadOnlyList<Element> Children { get { return ChildrenMutable; } }

		/// <summary>Get the <see cref="OwnerDocument"/> that this <see cref="Element"/> is currently in, or <c>null</c> if it's not attached to any.</summary>
		[XmlIgnore]
		public Collada OwnerDocument { get { Collada collada = this as Collada; return collada != null ? collada : Parent != null ? Parent.OwnerDocument : null; } }

		/// <summary>Get the parent <see cref="Element"/>, or <c>null</c> if this <see cref="Element"/> is not attached to any.</summary>
		[XmlIgnore]
		public Element Parent { get { return parent; } }

		internal void Attach(Collada newDocument, Element newParent) {
			AttachDocument(newDocument);
			AttachParent(newParent);
		}

		protected virtual void AttachDocument(Collada newDocument) {
			foreach (Element child in ChildrenMutable)
				child.AttachDocument(newDocument);
		}

		protected virtual void AttachParent(Element newParent) {
			parent = newParent;
			if (parent != null)
				parent.ChildrenMutable.Add(this);
		}

		internal void Check(Collada newDocument, Element newParent) {
			if (newDocument != null)
				CheckDocument(newDocument);
			if (newParent != null)
				CheckParent(newParent);
		}

		/// <summary>Recursively check that this element tree can be inserted into the new document, throwing an exception if it's invalid.</summary>
		/// <param name="newDocument"></param>
		protected virtual void CheckDocument(Collada newDocument) {
			foreach (Element child in ChildrenMutable)
				child.CheckDocument(newDocument);
		}

		/// <summary>Check that this element can be inserted into the new parent.</summary>
		/// <param name="newParent"></param>
		protected virtual void CheckParent(Element newParent) {
		}

		internal void Detach(Collada oldDocument, Element oldParent) {
			DetachDocument(oldDocument);
			DetachParent(oldParent);
		}

		/// <summary>Recursively detach this element tree from the document.</summary>
		/// <param name="oldDocument"></param>
		protected virtual void DetachDocument(Collada oldDocument) {
			foreach (Element child in ChildrenMutable)
				child.DetachDocument(oldDocument);
		}

		/// <summary>Detach this element from its parent.</summary>
		/// <param name="oldParent"></param>
		protected virtual void DetachParent(Element oldParent) {
			parent = null;
			if (parent != null)
				parent.ChildrenMutable.Remove(this);
		}

		protected TCollection GetCollection<TCollection>(ref TCollection slot)
			where TCollection : new() {
			if (slot == null)
				slot = new TCollection();
			return slot;
		}

		/// <summary>Set a collection property that's derived from <see cref="ElementCollection&lt;&gt;"/>. This is necessary to properly maintain the <see cref="Parent"/>/<see cref="Children"/> hierarchy.</summary>
		/// <typeparam name="TElement">The type of the <see cref="Element"/>.</typeparam>
		/// <typeparam name="TElementList">The type of the <see cref="ElementCollection&lt;&gt;"/>.</typeparam>
		/// <param name="slot">The field slot to change. It must hold the old value, if any, so that it can be detached properly.</param>
		/// <param name="value">The new value to assign to the field.</param>
		protected void SetCollection<TElement, TElementList>(ref TElementList slot, TElementList value)
			where TElementList : ElementCollection<TElement>
			where TElement : Element {
			if (object.ReferenceEquals(slot, value))
				return;
			if (value != null && value.Parent != null)
				throw new ArgumentException("This element list already has a parent; it cannot be added to another element.", "value");

			Collada document = OwnerDocument;

			if (value != null) {
				foreach (TElement element in value)
					element.Check(document, this);
			}

			if (slot != null) {
				foreach (TElement element in slot)
					element.Attach(document, this);
			}

			slot = value;

			if (value != null) {
				value.Parent = this;
				foreach (TElement element in value)
					element.Detach(document, this);
			}
		}

		/// <summary>Change an <see cref="Element"/> field while properly maintaining the <see cref="Parent"/>/<see cref="Children"/> hierarchy.</summary>
		/// <typeparam name="T">The type of the <see cref="Element"/>.</typeparam>
		/// <param name="slot">The field slot to change. It must hold the old value, if any, so that it can be detached properly.</param>
		/// <param name="value">The new value to assign to the field.</param>
		protected void SetElement<T>(ref T slot, T value) where T : Element {
			if (object.ReferenceEquals(slot, value))
				return;
			if (value != null && value.Parent != null)
				throw new ArgumentException("This element already has a parent; it cannot be added to another element.", "value");

			Collada document = OwnerDocument;

			if (value != null)
				value.Check(document, this);

			if (slot != null)
				slot.Detach(document, this);

			slot = value;
			if (value != null)
				value.Attach(document, this);
		}

		protected void ScopedIdAttachParent(string scopedId, Element newParent) {
		}

		protected void ScopedIdCheckParent(string scopedId, Element newParent) {
			if (!object.ReferenceEquals(newParent, null) && !object.ReferenceEquals(scopedId, null)) {
				foreach (Element child in newParent.ChildrenMutable) {
					IScopedId childScopedId = child as IScopedId;
					if (childScopedId != null && childScopedId.ScopedId == scopedId)
						throw new ArgumentException("The parent element already has a child with this scoped id.");
				}
			}
		}

		protected void ScopedIdDetachParent(string scopedId, Element oldParent) {
		}

		protected void ScopedIdSet(ref string slot, string value) {
			slot = value;
		}
	}

	/// <summary>An <see cref="Element"/> that has an <see cref="Id"/> property, which uniquely identifies the <see cref="Element"/> within the <see cref="Collada"/> document.</summary>
	public abstract class IdElement : Element, IId {
		string id;

		/// <summary>Get or set the unique identifier of this element; required.</summary>
		[XmlAttribute(Element.XmlIdAttribute, DataType = "NCName")]
		public string Id {
			get { return id; }

			set {
				Collada collada = OwnerDocument;

				if (id == value)
					return;

				if (collada != null) {
					// Check that new value is unique.
					if (value != null) {
						CheckKeyIsUnique(collada, value);
					}

					// Remove old value.
					if (id != null)
						collada.ElementsById.Remove(id);

					// Add new value.
					collada.ElementsById[id] = this;
				}

				id = value;
			}
		}

		void CheckKeyIsUnique(Collada collada, string id) {
			if (collada.ElementsById.ContainsKey(id))
				throw new ArgumentException("Cannot assign Id to '" + id + "' because there is already an " + typeof(Element).Name + " with this name in the " + typeof(Collada).Name + " document.");
		}

		protected override void AttachDocument(Collada newDocument) {
			base.AttachDocument(newDocument);
			if (!object.ReferenceEquals(newDocument, null) && !object.ReferenceEquals(id, null))
				newDocument.ElementsById[id] = this;
		}

		protected override void CheckDocument(Collada newDocument) {
			base.CheckDocument(newDocument);
			if (!object.ReferenceEquals(newDocument, null) && !object.ReferenceEquals(id, null))
				CheckKeyIsUnique(newDocument, id);
		}

		protected override void DetachDocument(Collada oldDocument) {
			base.DetachDocument(oldDocument);
			if (!object.ReferenceEquals(oldDocument, null) && !object.ReferenceEquals(id, null))
				oldDocument.ElementsById[id] = this;
		}
	}

	/// <summary>An <see cref="Element"/> that has a <see cref="ScopedId"/> property, which uniquely identifies the <see cref="Element"/> amongst its siblings.</summary>
	public abstract class ScopedIdElement : Element, IScopedId {
		string scopedId;

		/// <summary>A text string value containing the scoped identifier of this element. This value must be unique within the scope of the parent element. Optional. For details, see “Address Syntax” in Chapter 3: Schema Concepts.</summary>
		[XmlAttribute(Element.XmlScopedIdAttribute, DataType = "NCName")]
		public string ScopedId {
			get { return scopedId; }
			set { ScopedIdSet(ref scopedId, value); }
		}

		protected override void AttachParent(Element newParent) {
			base.AttachParent(newParent);
			ScopedIdAttachParent(scopedId, newParent);
		}

		protected override void CheckParent(Element newParent) {
			base.CheckParent(newParent);
			ScopedIdCheckParent(scopedId, newParent);
		}

		protected override void DetachParent(Element oldParent) {
			base.DetachParent(oldParent);
			ScopedIdDetachParent(scopedId, oldParent);
		}
	}

	public abstract class IdScopedIdElement : IdElement, IScopedId {
		string scopedId;

		/// <summary>A text string value containing the scoped identifier of this element. This value must be unique within the scope of the parent element. Optional. For details, see “Address Syntax” in Chapter 3: Schema Concepts.</summary>
		[XmlAttribute(Element.XmlScopedIdAttribute, DataType = "NCName")]
		public string ScopedId {
			get { return scopedId; }
			set { ScopedIdSet(ref scopedId, value); }
		}

		protected override void AttachParent(Element newParent) {
			base.AttachParent(newParent);
			ScopedIdAttachParent(scopedId, newParent);
		}

		protected override void CheckParent(Element newParent) {
			base.CheckParent(newParent);
			ScopedIdCheckParent(scopedId, newParent);
		}

		protected override void DetachParent(Element oldParent) {
			base.DetachParent(oldParent);
			ScopedIdDetachParent(scopedId, oldParent);
		}
	}
}
