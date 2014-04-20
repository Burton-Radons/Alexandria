using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics.Scenes {
	public abstract class Component : SceneObject {
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		Node node;

		public Node Node {
			get { return node; }
			internal set { SetProperty(NodeChangingArgs, NodeChangedArgs, ref node, ref value); }
		}

		const string
			NodeName = "Node";

		static readonly PropertyChangingEventArgs
			NodeChangingArgs = new PropertyChangingEventArgs(NodeName);

		static readonly PropertyChangedEventArgs
			NodeChangedArgs = new PropertyChangedEventArgs(NodeName);
	}
}
