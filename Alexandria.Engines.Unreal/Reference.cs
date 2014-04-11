using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alexandria.Engines.Unreal.Core;

namespace Alexandria.Engines.Unreal {
    /// <summary>
    /// The base class of exports and imports.
    /// </summary>
    public abstract class Reference {
        public Reference(Package package, int index) {
            Package = package;
            Index = index;
        }

        public State State { get { return Package.State; } }

        /// <summary>
        /// Package this is in.
        /// </summary>
        public Package Package { get; protected set; }

        /// <summary>
        /// Zero-based index of this reference in the package.
        /// </summary>
        public int Index { get; protected set; }

        /// <summary>
        /// Name of the object.
        /// </summary>
        public string Name { get; protected internal set; }

        /// <summary>
        /// Get the object this refers to, loading it if necessary.
        /// </summary>
        public abstract RootObject Object { get; }

        public virtual RootObject ResolveNewObject() {
            throw new Exception();
        }

        /// <summary>
        /// Return the export this is linked to, loading packages as needed.
        /// </summary>
        public abstract Export AsExport { get; }

		public static int IndexStorageSizeBytes(Reference value) {
			if(value == null)
				return new UIndex(0).StorageSizeBytes;
			return new UIndex(value.Index + 1).StorageSizeBytes;
		}
    }
}
