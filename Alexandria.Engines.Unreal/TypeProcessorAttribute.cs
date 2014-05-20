using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;
using Alexandria.Engines.Unreal.Core;
using Glare;

namespace Alexandria.Engines.Unreal {
	internal class TypeProcessorAttribute : Attribute {
		public TypeProcessorAttribute(Type type) {
			Type = type;
		}

		static TypeProcessorAttribute() {
			foreach(var item in typeof(BaseTypeProcessors).GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic)) {
				var attribute = (TypeProcessorAttribute)Attribute.GetCustomAttribute(item, typeof(TypeProcessorAttribute));
				var processor = RootObject.GetReaderSingleton(item);
				TypeProcessors.Add(attribute.Type, processor);
			}
		}

		public Type Type { get; private set; }
		public DataProcessor Processor { get; private set; }

		static readonly Dictionary<Type, DataProcessor> TypeProcessors = new Dictionary<Type, DataProcessor>();

		/// <summary>
		/// Get the <see cref="DataProcessor"/> for the given <see cref="System.Type"/>, returning the <see cref="DataProcessor"/> or throwing a <see cref="NotSupportedException"/> if there is none registered.
		/// </summary>
		/// <param name="type">The <see cref="System.Type"/> to search for a custom processor.</param>
		/// <returns>The <see cref="DataProcessor"/> for the <paramref name="type"/>.</returns>
		public static DataProcessor MustGetTypeProcessor(Type type) {
			if(type == null) throw new ArgumentNullException("type");
			DataProcessor processor;
			if(!TypeProcessors.TryGetValue(type, out processor))
				throw new NotSupportedException("There is no type processor defined for " + type.FullName);
			return processor;
		}
	}

	internal static class BaseTypeProcessors {
		[TypeProcessor(typeof(AttributeDictionary))]
		class AttributeDictionaryProcessor : DataProcessor {
			public override object Read(object target, Package package, BinaryReader reader, long end) { return AttributeDictionary.Load(package, reader); }
		}

		[TypeProcessor(typeof(Bounds))]
		class BoundsProcessor : DataProcessor {
			public override object Read(object target, Package package, BinaryReader reader, long end) { return Bounds.Read(reader); }
		}

		[TypeProcessor(typeof(byte))]
		class ByteProcessor : DataProcessor {
			public override object Read(object target, Package package, BinaryReader reader, long end) { return reader.ReadByte(); }
		}

		[TypeProcessor(typeof(byte[]))]
		class ByteArrayProcessor : DataProcessor {
			public override object Read(object target, Package package, BinaryReader reader, long end) { int count = UIndex.Read(reader); return reader.ReadBytes(count); }
		}

		[TypeProcessor(typeof(Export))]
		class ExportProcessor : DataProcessor {
			public override object Read(object target, Package package, BinaryReader reader, long end) { return package.ReadExport(reader); }
		}

		[TypeProcessor(typeof(float))]
		class FloatProcessor : DataProcessor {
			public override object Read(object target, Package package, BinaryReader reader, long end) { return reader.ReadSingle(); }
		}

		[TypeProcessor(typeof(Plane3f))]
		class FloatPlane3Processor : DataProcessor {
			public override object Read(object target, Package package, BinaryReader reader, long end) { return reader.ReadPlane3f(); }
		}

		[TypeProcessor(typeof(Sphere3f))]
		class Sphere3fProcessor : DataProcessor {
			public override object Read(object target, Package package, BinaryReader reader, long end) { return reader.ReadSphere3f(); }
		}

		[TypeProcessor(typeof(Vector2f))]
		class Vector2fProcessor : DataProcessor {
			public override object Read(object target, Package package, BinaryReader reader, long end) { return reader.ReadVector2f(); }
		}

		[TypeProcessor(typeof(Vector3f))]
		class Vector3fProcessor : DataProcessor {
			public override object Read(object target, Package package, BinaryReader reader, long end) { return reader.ReadVector3f(); }
		}

		[TypeProcessor(typeof(Vector3f[]))]
		class Vector3fArrayProcessor : DataProcessor {
			public override object Read(object target, Package package, BinaryReader reader, long end) { return reader.ReadIndexCountArray<Vector3f>(reader.ReadVector3f); }
		}

		[TypeProcessor(typeof(Vector4f))]
		class FloatVector4Processor : DataProcessor {
			public override object Read(object target, Package package, BinaryReader reader, long end) { return reader.ReadVector4f(); }
		}

		[TypeProcessor(typeof(Guid))]
		class GuidProcessor : DataProcessor {
			public override object Read(object target, Package package, BinaryReader reader, long end) { return new Guid(reader.ReadBytes(16)); }
		}

		[TypeProcessor(typeof(Int16))]
		class Int16Processor : DataProcessor {
			public override object Read(object target, Package package, BinaryReader reader, long end) { return reader.ReadInt16(); }
		}

		[TypeProcessor(typeof(Int32))]
		class Int32Processor : DataProcessor {
			public override object Read(object target, Package package, BinaryReader reader, long end) { return reader.ReadInt32(); }
		}

		[TypeProcessor(typeof(Int32[]))]
		class Int32ArrayProcessor : DataProcessor {
			public override object Read(object target, Package package, BinaryReader reader, long end) { return reader.ReadIndexCountArray(reader.ReadInt32); }
		}

		[TypeProcessor(typeof(Int64))]
		class Int64Processor : DataProcessor {
			public override object Read(object target, Package package, BinaryReader reader, long end) { return reader.ReadInt64(); }
		}

		[TypeProcessor(typeof(Reference))]
		class ReferenceProcessor : DataProcessor {
			public override object Read(object target, Package package, BinaryReader reader, long end) { return package.ReadReference(reader); }
		}

		[TypeProcessor(typeof(List<Reference>))]
		class ReferenceListProcessor : DataProcessor {
			public override object Read(object target, Package package, BinaryReader reader, long end) { return reader.ReadIndexCountList(() => package.ReadReference(reader)); }
		}

		[TypeProcessor(typeof(Statement))]
		class StatementProcessor : DataProcessor {
			public override object Read(object target, Package package, BinaryReader reader, long end) { int offset = -1; return Statement.Load(package, reader, ref offset, end); }
		}

		[TypeProcessor(typeof(string))]
		class StringProcessor : DataProcessor {
			public override object Read(object target, Package package, BinaryReader reader, long end) { return package.ReadNameValue(reader); }
		}

		[TypeProcessor(typeof(List<string>))]
		class StringListProcessor : DataProcessor {
			public override object Read(object target, Package package, BinaryReader reader, long end) { return reader.ReadIndexCountList(() => package.ReadNameValue(reader)); }
		}

		[TypeProcessor(typeof(UInt16))]
		class UInt16Processor : DataProcessor {
			public override object Read(object target, Package package, BinaryReader reader, long end) { return reader.ReadUInt16(); }
		}

		[TypeProcessor(typeof(UInt32))]
		class UInt32Processor : DataProcessor {
			public override object Read(object target, Package package, BinaryReader reader, long end) { return reader.ReadUInt32(); }
		}

		[TypeProcessor(typeof(UInt64))]
		class UInt64Processor : DataProcessor {
			public override object Read(object target, Package package, BinaryReader reader, long end) { return reader.ReadUInt64(); }
		}
	}
}
