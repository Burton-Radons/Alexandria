using Glare.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Engine {
	/// <summary>
	/// An object stored in a <see cref="Module"/>.
	/// </summary>
	[Serializable]
	public abstract class Node {
		readonly RichDictionary<NodeTypeProperty, object> assignedProperties = new RichDictionary<NodeTypeProperty, object>();
		readonly Guid id = Guid.NewGuid();
		readonly Node prototype;

		/// <summary>Get the collection of properties that have been assigned to this node.</summary>
		public ReadOnlyObservableDictionary<NodeTypeProperty, object> AssignedProperties { get { return assignedProperties; } }

		/// <summary>Get the unique identifier of this <see cref="Node"/> and overrides in inheriting <see cref="Module"/>s.</summary>
		public Guid Id { get { return id; } }

		/// <summary>Get the prototype node that this node uses. The prototype is used as a source of property values if it has not been assigned in this node.</summary>
		public Node Prototype { get { return prototype; } }

		/// <summary>Initialise the node.</summary>
		/// <param name="prototype"></param>
		public Node(Node prototype) {
			if (prototype != null && prototype.GetType() != GetType() && !GetType().IsSubclassOf(prototype.GetType()))
				throw new Exception("A prototype must have the same class as this type.");
			this.prototype = prototype;
		}

		/// <summary>Find a property of the type. If not found, this throws an exception.</summary>
		/// <param name="type"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		protected static NodeTypeProperty GetProperty(Type type, string name) {
			return NodeType.GetNodeType(type).GetProperty(name);
		}

		/// <summary>Get the value of a property.</summary>
		/// <param name="property"></param>
		/// <returns></returns>
		protected object GetValue(NodeTypeProperty property) {
			object value;

			if(assignedProperties.TryGetValue(property, out value))
				return value;

			if (prototype != null)
				return prototype.GetValue(property);

			return property.DefaultValue;
		}

		/// <summary>Get the value of a property.</summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="property"></param>
		/// <returns></returns>
		protected T GetValue<T>(NodeTypeProperty property) {
			return (T)GetValue(property);
		}

		/// <summary>Set the value of a property.</summary>
		/// <param name="property"></param>
		/// <param name="value"></param>
		protected void SetValue(NodeTypeProperty property, object value) {
			assignedProperties[property] = value;
		}
	}
}
