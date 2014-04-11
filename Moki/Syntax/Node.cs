using Moki.Compiler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Moki.Syntax {
	/// <summary>
	/// Base class of the syntax nodes.
	/// </summary>
	public abstract class Node : INotifyPropertyChanged, INotifyPropertyChanging {
		/// <summary>A value that will be returned from <see cref="Marker"/> if it is not overloaded to return different.</summary>
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		Marker marker;

		/// <summary>Get where in the source code the <see cref="Node"/> is at.</summary>
		/// <remarks>The default returns <see cref="marker"/>.</remarks>
		public virtual Marker Marker { get { return marker; } }

		public event PropertyChangedEventHandler PropertyChanged;

		public event PropertyChangingEventHandler PropertyChanging;

		public Node(Marker marker) {
			this.marker = marker;
		}

		static protected PropertyInfo FindProperty(Type type, string name) {
			PropertyInfo property = type.GetProperty(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (property == null)
				throw new InvalidOperationException("Type " + type.FullName + " does not have a property named " + name + ".");

			FieldInfo propertyField = type.GetField(name + "Property", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			if (propertyField == null)
				throw new InvalidOperationException("Type " + type.FullName + " does not have a PropertyInfo entry for property " + name + ".");
			if (propertyField.FieldType != typeof(PropertyInfo) || !propertyField.IsInitOnly)
				throw new InvalidOperationException("Type " + type.FullName + " " + name + "Property entry is not 'public static readonly PropertyInfo " + name + " = FindProperty(typeof(" + type.Name + "), \"" + name + "\");'");

			return property;
		}

		protected T GetProperty<T>(PropertyInfo property, ref T slot) {
			return slot;
		}

		protected virtual void OnPropertyChanged<T>(PropertyInfo property, ref T slot, ref T value) {
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(property.Name));
		}

		protected virtual void OnPropertyChanging<T>(PropertyInfo property, ref T slot, ref T value) {
			if (PropertyChanging != null)
				PropertyChanging(this, new PropertyChangingEventArgs(property.Name));
		}

		protected void SetProperty<T>(PropertyInfo property, ref T slot, ref T value) {
			OnPropertyChanging(property, ref slot, ref value);
			slot = value;
			OnPropertyChanged(property, ref slot, ref value);
		}
	}
}
