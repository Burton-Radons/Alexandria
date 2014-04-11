using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Collections.ObjectModel;
using Alexandria.Engines.Unreal.Core;

namespace Alexandria.Engines.Unreal {
	/// <summary>
	/// Used on a property to enable automatic reading/writing of the attribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class PackagePropertyAttribute : Attribute {
		#region Constructors

		/// <summary>
		/// Initialise a native property attribute.
		/// </summary>
		/// <param name="index">The zero-based index of this attribute in the class in which it resides.</param>
		/// <param name="exclusiveToGames">If empty, then all games have this property. If not empty, then only the games listed have this property.</param>
		public PackagePropertyAttribute(int index, params PackageGame[] exclusiveToGames) : this(index, null, exclusiveToGames) {
			Index = index;
		}

		/// <summary>
		/// Initialize a native property attribute.
		/// </summary>
		/// <param name="index">The zero-based index of this attribute in the class in which it resides.</param>
		/// <param name="readerType">The type of a <see cref="DataProcessor"/>-derived class for reading/writing the property.</param>
		/// <param name="exclusiveToGames">If empty, then all games have this property. If not empty, then only the games listed have this property.</param>
		public PackagePropertyAttribute(int index, Type readerType, params PackageGame[] exclusiveToGames) {
			Index = index;
			if(readerType != null) Processor = RootObject.GetReaderSingleton(readerType);
			ExclusiveToGames = new HashSet<PackageGame>(exclusiveToGames);
		}

		#endregion Constructors

		#region Internals

		static readonly Dictionary<Type, PackagePropertyCollection> NativePropertiesDictionary = new Dictionary<Type, PackagePropertyCollection>();
		static readonly Dictionary<Type, PackagePropertyCollection> LocalNativePropertiesDictionary = new Dictionary<Type, PackagePropertyCollection>();

		#endregion Internals

		#region Properties

		/// <summary>Get the zero-based index of this attribute in the class in which it resides. This index is necessary because there is no method to get a list of properties in declaration order, and because moving symbols around (such as in refactoring) would screw up the order.</summary>
		public int Index { get; protected set; }

		/// <summary>Get the collection of <see cref="PackageGame"/> objects that this property is present for. If this is empty, then it includes all games.</summary>
		protected readonly HashSet<PackageGame> ExclusiveToGames;

		/// <summary>
		/// Delegate to use to process a property value, or <c>null</c> to use a default. If this is <c>null</c>, it will be filled in when <see cref="GetLocalNativeProperties"/> or <see cref="GetNativeProperties"/> is called.
		/// </summary>
		protected DataProcessor Processor;

		/// <summary>The <see cref="PropertyInfo"/> this is attached to, or <c>null</c>. If this is <c>null</c>, it will be filled in when <see cref="GetDataProcessor"/> or <see cref="Read"/> is called.</summary>
		protected PropertyInfo AttachedProperty;
		
		#endregion Properties

		#region Methods

		/// <summary>
		/// Get all the <see cref="PackageProperty"/> values, including properties in base types. To get the properties declared only in this type, use <see cref="GetLocalNativeProperties"/>. Note that these <see cref="PackagePropertyAttribute"/> values will have overlapped indexes.
		/// </summary>
		/// <param name="type">The <see cref="Type"/> to search for properties within.</param>
		/// <returns>The collection of <see cref="PackageProperty"/> objects.</returns>
		public static PackagePropertyCollection GetNativeProperties(Type type) {
			PackagePropertyCollection collection;
			ObservableCollection<PackageProperty> list;

			if(NativePropertiesDictionary.TryGetValue(type, out collection))
				return collection;

			if(type.BaseType != null) {
				list = new ObservableCollection<PackageProperty>(GetNativeProperties(type.BaseType));
				var properties = GetLocalNativeProperties(type);
				foreach(var item in properties)
					list.Add(item);
			} else
				list = new ObservableCollection<PackageProperty>(GetLocalNativeProperties(type));

			collection = NativePropertiesDictionary[type] = new PackagePropertyCollection(list);
			return collection;
		}

		/// <summary>
		/// Get all the <see cref="PackageProperty"/> attributes that are defined within this <see cref="Type"/> (no inheritance).
		/// </summary>
		/// <param name="type">The <see cref="Type"/> to search for properties within.</param>
		/// <returns>The collection of <see cref="PackageProperty"/> objects.</returns>
		public static PackagePropertyCollection GetLocalNativeProperties(Type type) {
			PackagePropertyCollection collection;

			if(LocalNativePropertiesDictionary.TryGetValue(type, out collection))
				return collection;

			ObservableCollection<PackageProperty> list = new ObservableCollection<PackageProperty>();
			collection = LocalNativePropertiesDictionary[type] = new PackagePropertyCollection(list);

			var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);

			foreach(var property in properties) {
				var attributes = property.GetCustomAttributes(typeof(PackagePropertyAttribute), false);
				if(attributes == null || attributes.Length == 0)
					continue;
				var attribute = (PackagePropertyAttribute)attributes[0];
				while(list.Count <= attribute.Index)
					list.Add(null);
				if(list[attribute.Index] != null)
					throw new Exception(type.Name + " has invalid NativePropertyAttributes - multiple attributes (including " + list[attribute.Index].Property.Name + " and " + property.Name + ") have the same index.");
				var nativeProperty = list[attribute.Index] = new PackageProperty(property, attribute);

				attribute.AttachedProperty = property;
				if(attribute.Processor == null)
					attribute.Processor = TypeProcessorAttribute.MustGetTypeProcessor(property.PropertyType);
			}

			for(int index = 0; index < list.Count; index++)
				if(list[index] == null)
					throw new Exception(type.Name + " has invalid NativePropertyAttributes - there is no usage of " + index + ", and maybe others.");

			return collection;
		}

		/// <summary>
		/// Read a <see cref="PropertyInfo"/> this is attached to from the <see cref="BinaryReader"/>.
		/// </summary>
		/// <param name="property">The <see cref="PropertyInfo"/> this <see cref="PackagePropertyAttribute"/> is attached to.</param>
		/// <param name="target">The object to store the value of this property.</param>
		/// <param name="package">The <see cref="Package"/> that is reading the property.</param>
		/// <param name="reader">The <see cref="BinaryReader"/> to read the property from.</param>
		/// <param name="end">The absolute end of the property or object's data.</param>
		/// <returns>The value of the property.</returns>
		public object Read(PropertyInfo property, object target, Package package, BinaryReader reader, long end) {
			object value = Processor.Read(target, package, reader, end);
			return value;
		}

		/// <summary>
		/// Read the object's package properties by finding its <see cref="PackagePropertyAttribute"/>-assigned properties, and then reading them in order.
		/// </summary>
		/// <param name="target">The object to read.</param>
		/// <param name="package">The <see cref="Package"/> this object is in.</param>
		/// <param name="reader">The <see cref="BinaryReader"/> to read from.</param>
		/// <param name="end">The end position of the object in the stream.</param>
		public static void ReadObject(object target, Package package, BinaryReader reader, long end) {
			if(target == null) throw new ArgumentNullException("target");
			if(package == null) throw new ArgumentNullException("package");
			if(reader == null) throw new ArgumentNullException("reader");
			var targetType = target.GetType();
			var properties = GetNativeProperties(targetType);
			for(var index = 0; index < properties.Count; index++) {
				var native = properties[index];
				PropertyInfo property = native.Property;
				PackagePropertyAttribute attribute = native.Attribute;
				Type type = property.PropertyType;

				if(reader.BaseStream.Position >= end)
					throw new InvalidOperationException("The object has read past its end.");

				if(attribute.ExclusiveToGames.Count == 0 || attribute.ExclusiveToGames.Contains(package.Game)) {
					object value = attribute.Read(property, target, package, reader, end);
					property.SetValue(target, value, null);
				}
			}
		}
		
		#endregion Methods
	}

	/// <summary>
	/// A connection between a <see cref="PackagePropertyAttribute"/> and the <see cref="PropertyInfo"/> property it's attached to.
	/// </summary>
	public class PackageProperty {
		/// <summary>
		/// Initialize the <see cref="PackageProperty"/>.
		/// </summary>
		/// <param name="property">The value for the <see cref="Property"/> field.</param>
		/// <param name="attribute">The value for the <see cref="PackagePropertyAttribute"/> field.</param>
		public PackageProperty(PropertyInfo property, PackagePropertyAttribute attribute) {
			Property = property;
			Attribute = attribute;
		}

		/// <summary>The <see cref="PropertyInfo"/> property that the <see cref="Attribute"/> is attached to.</summary>
		public readonly PropertyInfo Property;

		/// <summary>The <see cref="PackagePropertyAttribute"/> that is attached to the <see cref="Property"/>.</summary>
		public readonly PackagePropertyAttribute Attribute;

		/// <summary>Convert to a string of the form "NativeProperty({TypeName} {DeclaringType}.{PropertyName})".</summary>
		/// <returns></returns>
		public override string ToString() {
			return string.Format("NativeProperty({2} {0}.{1})", Property.DeclaringType.Name, Property.Name, Property.PropertyType.Name);
		}
	}

	/// <summary>
	/// An observable read-only collection of <see cref="PackageProperty"/> objects.
	/// </summary>
	public class PackagePropertyCollection : ReadOnlyObservableCollection<PackageProperty> {
		/// <summary>
		/// Initialize the <see cref="PackagePropertyCollection"/>.
		/// </summary>
		/// <param name="list">The <see cref="ObservableCollection<PackageProperty>"/> to wrap.</param>
		public PackagePropertyCollection(ObservableCollection<PackageProperty> list) : base(list) { }
	}
}
