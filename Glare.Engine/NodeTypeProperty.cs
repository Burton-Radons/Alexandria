using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Glare.Internal;

namespace Glare.Engine {
	/// <summary>
	/// 
	/// </summary>
	/// <remarks>
	/// Properties can use the <see cref="DisplayNameAttribute"/> to define
	/// Properties can use the <see cref="DefaultValueAttribute"/> to define the initial value for a property if none has been assigned, as well as to determine whether the property value has truly been changed for presentation purposes.
	/// </remarks>
	public class NodeTypeProperty : NodeMemberInfo {
		readonly object defaultValue;
		readonly NodeType nodeType;
		readonly PropertyInfo info;

		/// <summary>Get the <see cref="System.Type"/> that this is declared in.</summary>
		public Type DeclaringType { get { return info.DeclaringType; } }

		/// <summary>Get the default value of the property, or <c>null</c> if none is known.</summary>
		public object DefaultValue { get { return defaultValue; } }

		/// <summary>Get the <see cref="PropertyInfo"/> for the property.</summary>
		public PropertyInfo Info { get { return info; } }

		/// <summary>Get the declaring <see cref="Glare.Engine.NodeType"/>.</summary>
		public NodeType NodeType { get { return nodeType; } }

		/// <summary>Get the <see cref="System.Type"/> of the property.</summary>
		public Type PropertyType { get { return info.PropertyType; } }

		internal NodeTypeProperty(NodeType nodeType, PropertyInfo info) : base(info) {
			this.nodeType = nodeType;
			this.info = info;

			var defaultValueAttribute = info.GetCustomAttribute<DefaultValueAttribute>(true);
			if (defaultValueAttribute != null)
				defaultValue = defaultValueAttribute.Value;
		}
	}
}
