using Glare.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics.Scenes {
	/// <summary>
	/// A scene node.
	/// </summary>
	public class Node : SceneObject {
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		readonly RichList<Node> children = new RichList<Node>();

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		Node parent;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		Vector3d translation;

		public ReadOnlyList<Node> Children { get { return children; } }

		public NodeComponentCollection Components { get; private set; }

		/// <summary>Get or set the name of the <see cref="Node"/>.</summary>
		public string Name { get; set; }

		public Node Parent {
			get { return parent; }

			set {
				throw new NotImplementedException();
			}
		}

		/// <summary>
		/// Get or set the <see cref="Node"/>'s translation.
		/// </summary>
		public Vector3d Translation {
			get { return translation; }
			set { SetProperty(TranslationChangingArgs, TranslationChangedArgs, ref translation, ref value); }
		}

		const string
			TranslationName = "Translation";

		static readonly PropertyChangingEventArgs
			TranslationChangingArgs = new PropertyChangingEventArgs(TranslationName);

		static readonly PropertyChangedEventArgs
			TranslationChangedArgs = new PropertyChangedEventArgs(TranslationName);

		public Node() {
			Components = new NodeComponentCollection(this);
		}
	}

	public class NodeComponentCollection : RichList<Component> {
		readonly Node Node;

		internal NodeComponentCollection(Node node) {
			Node = node;
		}

		public override void Add(Component item) {
			Insert(Count, item);
		}

		public override void Clear() {
			while (Count > 0)
				RemoveAt(Count - 1);
		}

		public override void ClearFast() {
			Clear();
		}

		public override void Insert(int index, Component item) {
			if (item == null)
				throw new ArgumentNullException("item");
			if (item.Node != null)
				throw new ArgumentException(string.Format("The {0} has already been added to a {1}.", typeof(Component).Name, typeof(Node).Name));

			base.Insert(index, item);
			item.Node = Node;
		}

		public override void RemoveAt(int index) {
			base.RemoveAt(index);
		}
	}
}
