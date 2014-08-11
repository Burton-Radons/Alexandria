using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Framework {
	/// <summary>This is a base class of an object that notifies when its properties are changing.</summary>
	public abstract class NotifyingObject : INotifyRichPropertyChanged, INotifyRichPropertyChanging {
		/// <summary>Find a property of the type. If not found, this throws an exception. This is used in static constructors (see remarks).</summary>
		/// <remarks>
		/// This is primarily used in static fields for the properties, in the form:
		/// 
		/// <code>static readonly PropertyInfo MarcusProperty = GetProperty&lt;Bacon&gt;("Marcus");
		/// 
		/// int MarcusField;
		/// 
		/// public int Marcus {
		///	get { return MarcusField; }
		///	set { SetProperty(ref MarcusField, ref value); }
		/// }</code>
		/// </remarks>
		/// <typeparam name="T">The type that contains the property.</typeparam>
		/// <param name="name">The name of the property to find.</param>
		/// <returns>The <see cref="PropertyInfo"/> for the matching property.</returns>
		protected static PropertyInfo GetProperty<T>(string name) {
			if (name == null)
				throw new ArgumentNullException("name");

			Type type = typeof(T);
			PropertyInfo property = type.GetProperty(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

			if (property == null)
				throw new InvalidOperationException(string.Format("The type {0} (formally {1}) does not have a property named {2}.", type.Name, type.AssemblyQualifiedName, name));
			return property;
		}

		/// <summary>Invoked on the changing of a property value through <see cref="SetProperty"/>. The default sends the correct messages through the <see cref="PropertyChanged"/> and <see cref="INotifyPropertyChanged.PropertyChanged"/> events.</summary>
		/// <typeparam name="T">The type of the property value.</typeparam>
		/// <param name="property">The <see cref="PropertyInfo"/> of the property that's changing.</param>
		/// <param name="oldValue">The old value of the property.</param>
		/// <param name="newValue">The new value of the property.</param>
		/// <param name="oldValueObject">A boxed version of the old value (in <paramref name="oldValue"/>) of the property, if <paramref name="objectsSet"/> is <c>true</c>. The default implementation will set this if any events need to be raised.</param>
		/// <param name="newValueObject">A boxed version of the new value (in <paramref name="newValue"/>) of the property, if <paramref name="objectsSet"/> is <c>true</c>. The default implementation will set this if any events need to be raised.</param>
		/// <param name="objectsSet">Whether <paramref name="oldValueObject"/> and <paramref name="newValueObject"/> are actual boxed versions of <paramref name="oldValue"/> and <paramref name="newValue"/>. The default implementation sets this to <c>true</c> if it has raised any events, and boxed the values.</param>
		/// <exception cref="ArgumentNullException"><paramref name="property"/> is <c>null</c>.</exception>
		protected virtual void OnPropertyChanged<T>(PropertyInfo property, ref T oldValue, ref T newValue, ref object oldValueObject, ref object newValueObject, ref bool objectsSet) {
			PropertyChangedEventArgs baseEventArgs = null;
			RichPropertyChangedEventArgs eventArgs = null;

			if(PropertyChanged != null && !objectsSet) {
				oldValueObject = oldValue;
				newValueObject = newValue;
				objectsSet = true;
			}

			if (PropertyChanged != null)
				baseEventArgs = eventArgs = new RichPropertyChangedEventArgs(property, oldValueObject, newValueObject);
			else if (BasePropertyChanged != null)
				baseEventArgs = new PropertyChangedEventArgs(property.Name);

			if (PropertyChanged != null)
				PropertyChanged.Invoke(this, eventArgs);
			if (BasePropertyChanged != null)
				BasePropertyChanged.Invoke(this, baseEventArgs);
		}

		/// <summary>Invoked on the changing of a property value through <see cref="SetProperty"/>. The default sends the correct messages through the <see cref="PropertyChanging"/> and <see cref="INotifyPropertyChanging.PropertyChanging"/> events.</summary>
		/// <typeparam name="T">The type of the property value.</typeparam>
		/// <param name="property">The <see cref="PropertyInfo"/> of the property that's changing.</param>
		/// <param name="oldValue">The old value of the property.</param>
		/// <param name="newValue">The new value of the property.</param>
		/// <param name="oldValueObject">A boxed version of the old value (in <paramref name="oldValue"/>) of the property, if <paramref name="objectsSet"/> is <c>true</c>. The default implementation will set this if any events need to be raised.</param>
		/// <param name="newValueObject">A boxed version of the new value (in <paramref name="newValue"/>) of the property, if <paramref name="objectsSet"/> is <c>true</c>. The default implementation will set this if any events need to be raised.</param>
		/// <param name="objectsSet">Whether <paramref name="oldValueObject"/> and <paramref name="newValueObject"/> are actual boxed versions of <paramref name="oldValue"/> and <paramref name="newValue"/>. The default implementation sets this to <c>true</c> if it has raised any events, and boxed the values.</param>
		/// <exception cref="ArgumentNullException"><paramref name="property"/> is <c>null</c>.</exception>
		protected virtual void OnPropertyChanging<T>(PropertyInfo property, ref T oldValue, ref T newValue, ref object oldValueObject, ref object newValueObject, ref bool objectsSet) {
			PropertyChangingEventArgs baseEventArgs = null;
			RichPropertyChangingEventArgs eventArgs = null;

			if (PropertyChanging != null && !objectsSet) {
				oldValueObject = oldValue;
				newValueObject = newValue;
				objectsSet = true;
			}

			if (PropertyChanging != null)
				baseEventArgs = eventArgs = new RichPropertyChangingEventArgs(property, oldValueObject, newValueObject);
			else if (BasePropertyChanging != null)
				baseEventArgs = new PropertyChangingEventArgs(property.Name);

			if (PropertyChanging != null)
				PropertyChanging.Invoke(this, eventArgs);
			if (BasePropertyChanging != null)
				BasePropertyChanging.Invoke(this, baseEventArgs);
		}

		/// <summary>Change the value of the property, raising the <see cref="PropertyChanging"/> (and <see cref="INotifyPropertyChanging.PropertyChanging"/>) and <see cref="PropertyChanged"/> (and <see cref="INotifyPropertyChanged.PropertyChanged"/> events using <see cref="OnPropertyChanged"/> and <see cref="OnPropertyChanging"/> if necessary. Before assignment, this does a test for equality using <see cref="EqualityComparer{T}"/>.</summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="slot"></param>
		/// <param name="value"></param>
		protected void SetProperty<T>(PropertyInfo property, ref T slot, ref T value) {
			if (EqualityComparer<T>.Default.Equals(slot, value))
				return;
			T oldValue = slot;
			object oldValueObject = null, newValueObject = null;
			bool objectsSet = false;

			OnPropertyChanging(property, ref oldValue, ref value, ref oldValueObject, ref newValueObject, ref objectsSet);
			slot = value;
			OnPropertyChanged(property, ref oldValue, ref value, ref oldValueObject, ref newValueObject, ref objectsSet);
		}

		event PropertyChangedEventHandler BasePropertyChanged;
		event PropertyChangingEventHandler BasePropertyChanging;

		/// <summary>Raised when a property of the object has been changed.</summary>
		public event RichPropertyChangedEventHandler PropertyChanged;

		event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged {
			add { BasePropertyChanged += value; }
			remove { BasePropertyChanged -= value; }
		}

		/// <summary>Raised when a property of the object is changing.</summary>
		public event RichPropertyChangingEventHandler PropertyChanging;

		event PropertyChangingEventHandler INotifyPropertyChanging.PropertyChanging {
			add { BasePropertyChanging += value; }
			remove {  BasePropertyChanging -= value; }
		}
	}

	/// <summary>An extension of <see cref="INotifyPropertyChanged"/> that also supports <see cref="RichPropertyChangedEventHandler"/>.</summary>
	public interface INotifyRichPropertyChanged : INotifyPropertyChanged {
		/// <summary>Occurs when a property value changes.</summary>
		new event RichPropertyChangedEventHandler PropertyChanged;
	}

	/// <summary>An extension of <see cref="INotifyPropertyChanging"/> that also supports <see cref="RichPropertyChangingEventHandler"/>.</summary>
	public interface INotifyRichPropertyChanging : INotifyPropertyChanging {
		/// <summary>Occurs when a property value is changing.</summary>
		new event RichPropertyChangingEventHandler PropertyChanging;
	}

	/// <summary>Represents the method that will handle the <see cref="INotifyRichPropertyChanged"/> event raised when a property is changed on a component.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">A <see cref="RichPropertyChangedEventArgs"/> that contains the event data.</param>
	public delegate void RichPropertyChangedEventHandler(object sender, RichPropertyChangedEventArgs e);

	/// <summary>Represents the method that will handle the <see cref="INotifyRichPropertyChanging"/> event raised when a property is changing on a component.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">A <see cref="RichPropertyChangingEventArgs"/> that contains the event data.</param>
	public delegate void RichPropertyChangingEventHandler(object sender, RichPropertyChangingEventArgs e);

	public class RichPropertyChangedEventArgs : PropertyChangedEventArgs {
		/// <summary>Get the <see cref="PropertyInfo"/> that was changed.</summary>
		public PropertyInfo Property { get; private set; }

		/// <summary>Get the old value of the property.</summary>
		public object OldValue { get; private set; }

		/// <summary>Get the new value of the property.</summary>
		public object NewValue { get; private set; }

		/// <summary>Initialise the object.</summary>
		/// <param name="property">The <see cref="PropertyInfo"/> of the property being changed.</param>
		/// <param name="oldValue">The old value of the <paramref name="property"/>.</param>
		/// <param name="newValue">The new value of the <paramref name="property"/>.</param>
		public RichPropertyChangedEventArgs(PropertyInfo property, object oldValue, object newValue)
			: base(property.Name) {
			Property = property;
			OldValue = oldValue;
			NewValue = newValue;
		}
	}

	public class RichPropertyChangingEventArgs : PropertyChangingEventArgs {
		/// <summary>Get the <see cref="PropertyInfo"/> that is changing.</summary>
		public PropertyInfo Property { get; private set; }

		/// <summary>Get the old value of the property.</summary>
		public object OldValue { get; private set; }

		/// <summary>Get the new value of the property.</summary>
		public object NewValue { get; private set; }

		/// <summary>Initialise the object.</summary>
		/// <param name="property">The <see cref="PropertyInfo"/> of the property that is changing.</param>
		/// <param name="oldValue">The old value of the <paramref name="property"/>.</param>
		/// <param name="newValue">The new value of the <paramref name="property"/>.</param>
		public RichPropertyChangingEventArgs(PropertyInfo property, object oldValue, object newValue)
			: base(property.Name) {
			Property = property;
			OldValue = oldValue;
			NewValue = newValue;
		}
	}
}
