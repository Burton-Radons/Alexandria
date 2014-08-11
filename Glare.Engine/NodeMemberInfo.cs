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
	/// <summary>Breaks apart and presents component model attributes for a <see cref="MemberInfo"/>.</summary>
	public abstract class NodeMemberInfo {
		const string DefaultGroupName = "General";

		readonly CategoryAttribute categoryAttribute;
		readonly DescriptionAttribute descriptionAttribute;
		readonly DisplayAttribute displayAttribute;
		readonly DisplayNameAttribute displayNameAttribute;
		MemberInfo member;

		/// <summary>Get the name of the group of members that this belongs to from the <see cref="DisplayAttribute"/> if any, the <see cref="CategoryAttribute"/> if any, or <see cref="DefaultGroupName"/>.</summary>
		public string Category {
			get {
				return (displayAttribute != null ? displayAttribute.GetGroupName() : null) ??
					(categoryAttribute != null ? categoryAttribute.Category : null) ??
					DefaultGroupName;
			}
		}

		/// <summary>Get the description of the member from the <see cref="DisplayAttribute"/> if present, from the <see cref="DescriptionAttribute"/> if present, or else <c>null</c>.</summary>
		public string Description {
			get {
				return (displayAttribute != null ? displayAttribute.GetDescription() : null) ??
					(descriptionAttribute != null ? descriptionAttribute.Description : null) ??
					null;
			}
		}

		/// <summary>Get the name of the property to display in the UI. This is based on the <see cref="DisplayAttribute"/> if found; otherwise the <see cref="DisplayNameAttribute"/> if found; otherwise the <see cref="Name"/>.</summary>
		public string DisplayName {
			get {
				return (displayAttribute != null ? displayAttribute.GetName() : null) ??
					(displayNameAttribute != null ? displayNameAttribute.DisplayName : null) ??
					Name;
			}
		}

		/// <summary>Get the name of the declaration.</summary>
		public string Name { get { return member.Name; } }

		/// <summary>Get the <see cref="MemberInfo"/> object for this member.</summary>
		protected MemberInfo Member { get { return member; } }

		/// <summary>Get a short name for the property, using a <see cref="DisplayAttribute"/> if possible. If there is no short name or there is no attribute, this returns <see cref="DisplayName"/>.</summary>
		public string ShortName { get { return displayAttribute != null ? displayAttribute.GetShortName() ?? DisplayName : DisplayName; } }

		/// <summary>Initialise the object.</summary>
		/// <param name="member"></param>
		protected NodeMemberInfo(MemberInfo member) {
			if (member == null)
				throw new ArgumentNullException("member");
			this.member = member;

			member.TryGetCustomAttribute(out categoryAttribute, true);
			member.TryGetCustomAttribute(out descriptionAttribute, true);
			member.TryGetCustomAttribute(out displayAttribute, true);
			member.TryGetCustomAttribute(out displayNameAttribute, true);
		}
	}
}
