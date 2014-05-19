using Glare.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Engine {
	[Serializable]
	public abstract class Node {
		readonly RichDictionary<NodeTypeProperty, object> assignedProperties = new RichDictionary<NodeTypeProperty, object>();
		readonly Guid id = Guid.NewGuid();
		readonly Node prototype;

		public ReadOnlyObservableDictionary<NodeTypeProperty, object> AssignedProperties { get { return assignedProperties; } }

		/// <summary>Get the unique identifier of this <see cref="Node"/> and overrides in inheriting <see cref="Module"/>s.</summary>
		public Guid Id { get { return id; } }

		public Node Prototype { get { return prototype; } }

		public Node(Node prototype) {
			if (prototype != null && prototype.GetType() != GetType() && !GetType().IsSubclassOf(prototype.GetType()))
				throw new Exception("A prototype must have the same class as this type.");
			this.prototype = prototype;
		}

		protected static NodeTypeProperty GetProperty(Type type, string name) {
			return NodeType.GetNodeType(type).GetProperty(name);
		}

		protected object GetValue(NodeTypeProperty property) {
			object value;

			if(assignedProperties.TryGetValue(property, out value))
				return value;

			if (prototype != null)
				return prototype.GetValue(property);

			return property.DefaultValue;
		}

		protected T GetValue<T>(NodeTypeProperty property) {
			return (T)GetValue(property);
		}

		protected void SetValue(NodeTypeProperty property, object value) {
			assignedProperties[property] = value;
		}
	}
}
