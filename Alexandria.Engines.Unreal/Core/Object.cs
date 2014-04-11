using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.ComponentModel;

namespace Alexandria.Engines.Unreal.Core {
    public class ObjectWithAttributes : RootObject {
        /// <summary>
        /// Fields defined in the object or null for none.
        /// </summary>
        [PackageProperty(0, typeof(FieldsReader))]
        public AttributeDictionary Fields { get; protected set; }

        class FieldsReader : DataProcessor {
            public override object Read(object target, Package package, BinaryReader reader, long end) {
                var rootObject = (RootObject)target;
                if(rootObject.Export != null && rootObject.Export.ObjectClassReference != null)
                    return AttributeDictionary.Load(package, reader);
                return null;
            }
        }
    }

    /// <summary>
    /// The root class of all Unreal objects. This is composed of a header and a data component that may not be loaded yet.
    /// </summary>
    public class Object : ObjectWithAttributes {
        [PackageProperty(0), Browsable(false)]
        public Reference SuperField { get; protected set; }

        [PackageProperty(1), Browsable(false)]
        public Reference NextReference { get; protected set; }

        [Browsable(false)]
        public Object Next { get { return NextReference == null ? null : (Object)NextReference.Object; } }

        /*public override URootObject Load(BinaryReader reader, int size) {
            if(Export != null && Export.ObjectClassReference != null)
                Fields = AttributeDictionary.Load(Package, reader);
            return base.Load(reader, size);
        }*/
    }

}
