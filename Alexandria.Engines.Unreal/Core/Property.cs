using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Engines.Unreal.Core {
	/// <summary>
	/// Flags for <see cref="Property"/> objects in <see cref="Property.PropertyFlags"/>
	/// </summary>
	[Flags]
	public enum PropertyFlag : uint {
		/// <summary>This property can be visible in the editor (?).</summary>
		Editable = 0x1,

		/// <summary>This property's value cannot be changed.</summary>
		Const = 0x2,

		/// <summary>This property's value is optional.</summary>
		Optional = 0x10,

		/// <summary>Unknown</summary>
		Out = 0x100,

		/// <summary>Unknown</summary>
		Skip = 0x200,

		/// <summary>This is a return property.</summary>
		Return = 0x400,

		/// <summary>Unknown</summary>
		Coerce = 0x800,

		/// <summary>Unknown</summary>
		Native = 0x1000,

		/// <summary>Unknown</summary>
		Config = 0x4000,

		/// <summary>Unknown</summary>
		Localized = 0x8000,

		/// <summary>Unknown</summary>
		EditConst = 0x20000,

		// 0x400 - static final native function (all ReturnValue)
		// 0x1000 - const native
		// 0x400000 - nothing!
		//Test = 0x400,
	}

	/// <summary>
	/// A property object, which can be either a field of a type, a function argument, or a function return type.
	/// </summary>
    public abstract class Property : Object {
		/// <summary>
		/// 1 for scalar.
		/// </summary>
        [PackageProperty(0)]
        public ushort ArrayDimension { get; protected set; }

		/// <summary>
		/// The number of bytes in an element of the property.
		/// </summary>
        [PackageProperty(1)]
        public ushort ElementSize { get; protected set; }

		// 0x400000 = array?
		/// <summary>
		/// The flags of the property.
		/// </summary>
        [PackageProperty(2, typeof(PropertyFlagProcessor))]
        public PropertyFlag PropertyFlags { get; protected set; }

		/// <summary>
		/// The category that this <see cref="Property"/> is in.
		/// </summary>
        [PackageProperty(3)]
        public string Category { get; protected set; }

		/// <summary>Unknown, always zero so far.</summary>
		[PackageProperty(4, typeof(DataProcessors.UnknownIndex), PackageGame.ThiefDeadlyShadows)]
		public int Thief3Unknown1 { get; protected set; }

		/// <summary>Unknown, always zero so far.</summary>
		[PackageProperty(5, typeof(DataProcessors.UnknownIndex), PackageGame.ThiefDeadlyShadows)]
		public int Thief3Unknown2 { get; protected set; }

		/// <summary>Unknown, always zero so far.</summary>
		[PackageProperty(6, typeof(DataProcessors.UnknownIndex), PackageGame.ThiefDeadlyShadows)]
		public int Thief3Unknown3 { get; protected set; }

		/// <summary>The name of the member that is declaring this property.</summary>
		[PackageProperty(7, PackageGame.ThiefDeadlyShadows)]
		public string Thief3DeclaringMember { get; protected set; }

		/// <summary>Completely not understood.</summary>
		[PackageProperty(8, PackageGame.ThiefDeadlyShadows)]
		public uint Thief3Unknown4 { get; protected set; }

		/// <summary>
		/// Converts the various property fields into a comma-delimited list.
		/// </summary>
		/// <returns></returns>
		protected virtual string PropertiesToString() {
			string text = "";
			if(ArrayDimension != 1)
				text += ", ArrayDimension: " + ArrayDimension;
			if(ElementSize != 0)
				text += ", ElementSize: " + ElementSize;
			if(PropertyFlags != 0)
				text += ", PropertyFlags: " + PropertyFlags;
			return text;
		}
		
		/// <summary>
		/// Converts the <see cref="Property"/> into a <c>string</c>.
		/// </summary>
		/// <returns></returns>
		public override string ToString() {
			return GetType().Name + "(" + Export.Name + PropertiesToString() + ")";
		}

		/// <summary>
		/// Get the name of the type of the script this <see cref="Property"/> uses.
		/// </summary>
		protected abstract string ScriptTypeName { get; }

		/// <summary>
		/// Get a short description of the <see cref="Property"/> of the form "{TypeDescription} {Export.Name}".
		/// </summary>
		public override string ShortDescription {
			get {
				return TypeDescription + " " + Export.Name;
			}
		}

		/// <summary>
		/// Get a description of the type of this <see cref="Property"/>.
		/// </summary>
		public string TypeDescription {
			get {
				string text = "";

				if(PropertyFlags.HasFlag(PropertyFlag.Editable))
					text += "() ";
				if(PropertyFlags.HasFlag(PropertyFlag.Const))
					text += "const ";
				if(PropertyFlags.HasFlag(PropertyFlag.Optional))
					text += "optional ";
				if(PropertyFlags.HasFlag(PropertyFlag.Out))
					text += "out ";
				if(PropertyFlags.HasFlag(PropertyFlag.Skip))
					text += "skip ";
				if(PropertyFlags.HasFlag(PropertyFlag.Coerce))
					text += "coerce ";
				if(PropertyFlags.HasFlag(PropertyFlag.Native))
					text += "native ";
				if(PropertyFlags.HasFlag(PropertyFlag.Config))
					text += "config ";
				if(PropertyFlags.HasFlag(PropertyFlag.Localized))
					text += "localized ";
				if(PropertyFlags.HasFlag(PropertyFlag.EditConst))
					text += "editconst ";
				if(ElementSize != 0)
					throw new Exception();
				return text + ScriptTypeName + (ArrayDimension != 1 ? "[" + ArrayDimension + "]" : "");
			}
		}

		class PropertyFlagProcessor : DataProcessor {
			public override object Read(object target, Package package, System.IO.BinaryReader reader, long end) {
				return (PropertyFlag)reader.ReadUInt32();
			}
		}
    }

	/// <summary>A <see cref="Boolean"/> <see cref="Property"/>.</summary>
    public class BoolProperty : Property {
		/// <summary>Returns 'bool'.</summary>
		protected override string ScriptTypeName { get { return "bool"; } }
    }

	/// <summary>A <see cref="Byte"/> <see cref="Property"/>.</summary>
    public class ByteProperty : Property {
        /// <summary>Get the enumeration type that this uses, or <c>null</c> if there is none.</summary>
		[PackageProperty(0)]
        public Reference Enum { get; protected set; }

		/// <summary>Adds the <see cref="Enum"/> to the list of properties if it is not <c>null</c>.</summary>
		protected override string PropertiesToString() {
			string text = base.PropertiesToString();
			if(Enum != null)
				text += ", Enum: " + Enum.ToString();
			return text;
		}

		/// <summary>Returns 'byte' or the <see cref="Enum"/>'s <see cref="Reference.Name"/> if <see cref="Enum"/> is not <c>null</c>.</summary>
		protected override string ScriptTypeName { get { return Enum != null ? Enum.Name : "byte"; } }
    }

	/// <summary>A class <see cref="Property"/> through a <see cref="Reference"/>.</summary>
    public class ClassProperty : ObjectProperty {
        /// <summary>The <see cref="Reference"/> to the class that this <see cref="Property"/> refers to.</summary>
		[PackageProperty(0)]
        public Reference Class { get; protected set; }

		/// <summary>Adds the <see cref="Class"/> name reference to the list of properties.</summary>
		protected override string PropertiesToString() {
			string text = base.PropertiesToString();
			if(Class != null)
				text += ", Class: " + Class.ToString();
			return text;
		}

		/// <summary>Returns <see cref="Class"/>'s <see cref="Reference.Name"/>.</summary>
		protected override string ScriptTypeName { get { return Class.Name; } }
    }

	/// <summary>A <see cref="Single"/> <see cref="Property"/>.</summary>
    public class FloatProperty : Property {
		/// <summary>Returns 'float'.</summary>
		protected override string ScriptTypeName { get { return "float"; } }
    }

	/// <summary>An <see cref="Int32"/> <see cref="Property"/>.</summary>
    public class IntProperty : Property {
		/// <summary>Returns 'int'.</summary>
		protected override string ScriptTypeName { get { return "int"; } }
    }

	/// <summary>A name <see cref="Property"/>.</summary>
    public class NameProperty : Property {
		/// <summary>Returns 'name'.</summary>
		protected override string ScriptTypeName { get { return "name"; } }
    }

	/// <summary>An object <see cref="Reference"/> <see cref="Property"/>.</summary>
    public class ObjectProperty : Property {
        /// <summary>The <see cref="Reference"/> to the <see cref="Object"/> that this property refers to, or <c>null</c> if there is none.</summary>
		[PackageProperty(0)]
        public Reference Object { get; protected set; }

		/// <summary>Adds the <see cref="Object"/> to the list of properties, if it is not <c>null</c>.</summary>
		protected override string PropertiesToString() {
			string text = base.PropertiesToString();
			if(Object != null)
				text += ", Object: " + Object.ToString();
			return text;
		}

		/// <summary>Returns "object: {Object.Name}", using <see cref="Object"/>'s <see cref="Reference.Name"/> property. If <see cref="Object"/> is <c>null</c>, "object: null" is returned instead.</summary>
		protected override string ScriptTypeName { get { return "object:" + (Object != null ? Object.Name : "null"); } }
    }

	/// <summary>A <see cref="String"/> <see cref="Property"/>.</summary>
    public class StrProperty : Property {
		/// <summary>Returns "string".</summary>
		protected override string ScriptTypeName { get { return "string"; } }
    }

	/// <summary>A structure <see cref="Reference"/> <see cref="Property"/>.</summary>
    public class StructProperty : Property {
        /// <summary>Returns the <see cref="Reference"/> to the struct type.</summary>
		[PackageProperty(0)]
        public Reference Type { get; protected set; }

		/// <summary>Adds the name of the <see cref="Type"/> to the properties.</summary>
		protected override string PropertiesToString() {
			string text = base.PropertiesToString();
			if(Type != null)
				text += ", TypeName: " + Type.ToString();
			return text;
		}

		/// <summary>Returns <see cref="Type"/>'s <see cref="Reference.Name"/>.</summary>
		protected override string ScriptTypeName { get { return Type.Name; } }
    }
}
