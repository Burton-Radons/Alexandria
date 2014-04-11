using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Glare;
using Glare.Internal;

namespace Alexandria.Engines.Unreal {
	public class Field {
		static object LoadArrayElement<T>(object current, Func<T> function) {
			var list = current as List<T>;

			if (current is T) {
				list = new List<T>(2);
				list.Add((T)current);
			} else if (current is List<T>) {
			} else if (current == null) {
				list = new List<T>(1);
			} else
				throw new Exception();

			list.Add(function());
			return current = list;
		}

		static object LoadSingle(Package package, BinaryReader reader, FieldInfo info, bool inArray = false) {
			if (info.Size < 1)
				throw new Exception();
			switch (info.Type) {
				case FieldTypeBase.Byte:
					if (!inArray && info.Size != 1)
						throw new Exception();
					return reader.ReadByte();

				case FieldTypeBase.Float:
					if (!inArray && info.Size != 4)
						throw new Exception();
					return reader.ReadSingle();

				case FieldTypeBase.Integer:
					if (!inArray && info.Size != 4)
						throw new Exception();
					return reader.ReadInt32();

				case FieldTypeBase.Name:
					return package.ReadNameIndex(reader).Value;

				case FieldTypeBase.Object:
					return package.ReadReference(reader);

				case FieldTypeBase.Str:
					var stringLength = reader.ReadByte();
					if (stringLength != info.Size - 1)
						throw new Exception("String length isn't the field size minus 1.");
					return reader.ReadStringz(stringLength, Encoding.ASCII);

				case FieldTypeBase.Struct:
					string name;

					if (info.IsLiteralSizeForm) {
						name = package.Names[info.Size].Value;
						info.Size = UIndex.Read(reader);
					} else
						name = package.ReadNameIndex(reader).Value;

					var end = reader.BaseStream.Position + info.Size;
					object result;

					switch (name) {
						case "InitialAllianceInfo":
							result = InitialAllianceInfo.Read(reader, package);
							break;

						case "PointRegion":
							if (!inArray && info.Size < 6)
								throw new Exception("PointRegion structure size must be at least 6 bytes.");
							result = PointRegion.Read(reader, package);
							break;

						case "Rotator":
							result = Rotator.Read(reader);
							break;

						case "Scale":
							if (!inArray && info.Size != 17)
								throw new Exception("Scale structure size is a constant 17 bytes.");
							result = Scale.Read(reader);
							break;

						case "Vector":
							if (!inArray && info.Size != 12)
								throw new Exception("Vector structure size is a constant 12 bytes.");
							result =  reader.ReadVector3f();
							break;

						default:
							throw new Exception("Unhandled or unknown struct type " + name + ".");
					}

					if (!inArray && reader.BaseStream.Position != end)
						throw new Exception("Structure wasn't fully devoured or was overconsumed.");
					return result;

				default:
					throw new Exception("Unknown or unhandled type " + info.Type + ".");
			}
		}

		public static object Load(Package package, BinaryReader reader, object current) {
			var info = new FieldInfo(reader);
			var u25 = new FieldInfo(0x25);

			if (info.Type == FieldTypeBase.Boolean)
				return info.IsArray;
			if (info.IsArray) {
				switch (info.Type) {
					case FieldTypeBase.Str:
						return LoadArrayElement(current, () => reader.ReadStringz(reader.ReadByte(), Encoding.ASCII));

					case FieldTypeBase.Integer:
						return LoadArrayElement(current, reader.ReadInt32);

					case FieldTypeBase.Object:
						return LoadArrayElement(current, () => package.ReadReference(reader));

					case FieldTypeBase.Struct:
						return LoadArrayElement(current, () => LoadSingle(package, reader, info));

					default:
						throw new NotImplementedException(info.Type + " does not have an implementation for loading arrays (index " + info.ArrayIndex + ", size " + info.Size + ")");
				}
			} else {
				return LoadSingle(package, reader, info);
			}
		}
	}

	/// <summary>
	/// A description of an attribute field.
	/// </summary>
	public struct FieldInfo {
		public FieldInfo(BinaryReader reader) {
			Value = reader.ReadByte();
			Size = 0;
			ArrayIndex = 0;
			switch (SizeForm) {
				case FieldTypeSize.Size1: Size = 1; break;
				case FieldTypeSize.Size2: Size = 2; break;
				case FieldTypeSize.Size4: Size = 4; break;
				case FieldTypeSize.Size12: Size = 12; break;
				case FieldTypeSize.Size16: Size = 16; break;
				case FieldTypeSize.SizeByte: Size = reader.ReadByte(); break;
				case FieldTypeSize.SizeShort: Size = reader.ReadUInt16(); break;
				case FieldTypeSize.SizeInt: Size = reader.ReadInt32(); break;
				default: throw new Exception();
			}
			if (IsArray && Type != FieldTypeBase.Boolean)
				ArrayIndex = UIndex.Read(reader);
		}

		public FieldInfo(byte firstByte) {
			Value = firstByte;
			Size = -1;
			ArrayIndex = -1;
		}

		/// <summary>
		/// Mask to isolate the <see cref="FieldTypeBase"/> component.
		/// </summary>
		public const int BaseMask = 15;

		/// <summary>
		/// Mask to isolate the <see cref="FieldTypeSize"/> component.
		/// </summary>
		public const int SizeMask = 7 << 4;

		/// <summary>
		/// Mask to indicate this is an array.
		/// </summary>
		public const int ArrayMask = 128;

		public byte Value;

		/// <summary>
		/// Size of the value.
		/// </summary>
		public int Size;

		public int ArrayIndex;

		/// <summary>
		/// Get the base type component.
		/// </summary>
		public FieldTypeBase Type { get { return (FieldTypeBase)(Value & BaseMask); } }

		/// <summary>
		/// Get the size component.
		/// </summary>
		public FieldTypeSize SizeForm { get { return (FieldTypeSize)(Value & SizeMask); } }

		public bool IsLiteralSizeForm { get { return SizeForm == FieldTypeSize.SizeByte || SizeForm == FieldTypeSize.SizeShort || SizeForm == FieldTypeSize.SizeInt; } }

		/// <summary>
		/// Get whether this is an array.
		/// </summary>
		public bool IsArray { get { return (Value & ArrayMask) != 0; } }
	}

	public enum FieldTypeBase : byte {
		Byte = 1,
		Integer = 2,
		Boolean = 3,
		Float = 4,
		Object = 5,
		Name = 6,
		String = 7,
		Class = 8,
		Array = 9,
		Struct = 10, // 0x0A
		Vector = 11, // 0x0B
		Rotator = 12, // 0x0C
		Str = 13, // 0x0D
		Map = 14, // 0x0E
		FixedArray = 15, // 0x0F
	}

	public enum FieldTypeSize : byte {
		Size1 = 0 << 4, // Size of 1.
		Size2 = 1 << 4, // Size of 2.
		Size4 = 2 << 4, // Size of 4.
		Size12 = 3 << 4, // Size of 12.
		Size16 = 4 << 4, // Size of 15.
		SizeByte = 5 << 4, // Size that uses a byte.
		SizeShort = 6 << 4, // Size that uses a short.
		SizeInt = 7 << 4, // Size that uses an int.
	}
}
