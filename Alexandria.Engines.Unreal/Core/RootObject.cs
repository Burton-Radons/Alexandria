using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Collections.ObjectModel;

namespace Alexandria.Engines.Unreal.Core {
	/// <summary>
	/// The absolute root of an Unreal object hierarchy.
	/// </summary>
    public partial class RootObject {
		#region Constructors

		/// <summary>
		/// Load this object's data from the <see cref="BinaryReader"/>.
		/// </summary>
		/// <param name="package">The <see cref="Alexandria.Engines.Unreal.Package"/> that this object is within.</param>
		/// <param name="reader">The <see cref="BinaryReader"/> to use.</param>
		/// <param name="end">The offset within the stream of the end of the object's data.</param>
		/// <returns><c>this</c> object.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="package"/> or <paramref name="reader"/> are <c>null</c>.</exception>
		public RootObject Load(Package package, BinaryReader reader, long end) {
			if(package == null)
				throw new ArgumentNullException("package");
            Package = package;
            return Load(reader, end);
        }

		/// <summary>Load this object's data from the <see cref="BinaryReader"/> using the current <see cref="Alexandria.Engines.Unreal.State"/> in <see cref="State"/>.</summary>
		/// <param name="reader"></param>
		/// <param name="end"></param>
		/// <returns></returns>
        public virtual RootObject Load(BinaryReader reader, long end) {
            PackagePropertyAttribute.ReadObject(this, Package, reader, end);
            return this;
		}

		#endregion Constructors

		#region Properties

		/// <summary>Get or set the linking <see cref="Alexandria.Engines.Unreal.Export"/>.</summary>
        [Browsable(false)]
        public Export Export {
            get { return pExport; }

            set {
                pExport = value;
                Package = value != null ? value.Package : null;
            }
        }

        /// <summary>Get or set the containing <see cref="Alexandria.Engines.Unreal.Package"/> or <c>null</c> for none. This is also assigned by assigning to <see cref="Alexandria.Engines.Unreal.Export"/>.</summary>
        [Browsable(false)]
        public Package Package { get; set; }

		/// <summary>
        /// Get the containing <see cref="Alexandria.Engines.Unreal.State"/> or <c>null</c> for none.
        /// </summary>
        [Browsable(false)]
        public virtual State State { get { var package = Package; return package != null ? package.State : null; } }

		/// <summary>
		/// Get a collection of all the native properties in this <see cref="RootObject"/>.
		/// </summary>
        [Browsable(false)]
        public ReadOnlyCollection<PackageProperty> NativeProperties { get { return PackagePropertyAttribute.GetNativeProperties(GetType()); } }

		#endregion Properties

		/// <summary>Get a compact one-line description of the object.</summary>
		public virtual string ShortDescription { get { return null; } }

		#region Internals

		Export pExport;

		static readonly Dictionary<Type, DataProcessor> ReaderSingletonDictionary = new Dictionary<Type, DataProcessor>();

		#endregion Internals

		#region Methods

		/// <summary>Call the initializer on a <see cref="DataProcessor"/>-derived <see cref="Type"/> and return it, cached.</summary>
		/// <param name="readerType">The <see cref="DataProcessor"/>-derived <see cref="Type"/> to construct.</param>
		/// <returns>The <see cref="DataProcessor"/> object.</returns>
        public static DataProcessor GetReaderSingleton(Type readerType) {
            DataProcessor result;

            if(!ReaderSingletonDictionary.TryGetValue(readerType, out result))
                result = ReaderSingletonDictionary[readerType] = (DataProcessor)readerType.GetConstructor(Type.EmptyTypes).Invoke(null);
            return result;
        }

		/// <summary>
		/// Use the <see cref="DataProcessor"/> of the given type to read properties into the object.
		/// </summary>
		/// <param name="readerType">The <see cref="Type"/> of the object to read.</param>
		/// <param name="target">The object to read into.</param>
		/// <param name="package">The <see cref="Alexandria.Engines.Unreal.Package"/> this <see cref="RootObject"/> should be placed in.</param>
		/// <param name="reader">The <see cref="BinaryReader"/> for reading the object.</param>
		/// <param name="end">An index just past the end of the <see cref="RootObject"/>'s data.</param>
		/// <returns><paramref name="target"/>.</returns>
        public static object CallReader(Type readerType, RootObject target, Package package, BinaryReader reader, long end) {
            return GetReaderSingleton(readerType).Read(target, package, reader, end);
        }

		#endregion Methods
	}
}
