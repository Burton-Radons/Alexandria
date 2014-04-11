using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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

		public Type DeclaringType { get { return info.DeclaringType; } }

		public object DefaultValue { get { return defaultValue; } }

		/// <summary>Get the <see cref="PropertyInfo"/> for the property.</summary>
		public PropertyInfo Info { get { return info; } }

		/// <summary>Get the declaring <see cref="Charana.NodeType"/>.</summary>
		public NodeType NodeType { get { return nodeType; } }

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
