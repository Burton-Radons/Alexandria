using Glare;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics.Loaders.Fbx {
	public struct FbxValue {
		[StructLayout(LayoutKind.Explicit)]
		struct ComboField {
			[FieldOffset(0)]
			public double Double;

			[FieldOffset(0)]
			public int Int32;

			[FieldOffset(0)]
			public long Int64;
		}


		public readonly FbxValueType Type;

		readonly ComboField ValueCombo;
		readonly object ValueObject;

		public FbxValue(BinaryReader reader)
			: this() {
			Type = (FbxValueType)reader.ReadByte();
			switch (Type) {
				case FbxValueType.Double:
					ValueCombo.Double = reader.ReadDouble();
					break;

				case FbxValueType.DoubleArray:
					int doubleCount = reader.ReadInt32();
					reader.RequireZeroes(4);
					reader.Require(doubleCount * 8);
					ValueObject = reader.ReadArrayDouble(doubleCount);
					break;

				case FbxValueType.Int32:
					ValueCombo.Int32 = reader.ReadInt32();
					break;

				case FbxValueType.Int32Array:
					int int32Count = reader.ReadInt32();
					reader.RequireZeroes(4);
					reader.Require(int32Count * 4);
					ValueObject = reader.ReadArrayInt32(int32Count);
					break;

				case FbxValueType.String:
					int stringLength = reader.ReadInt32();
					ValueObject = reader.ReadString(stringLength, Encoding.UTF8);
					break;

				case FbxValueType.Data:
					int dataLength = reader.ReadInt32();
					ValueObject = reader.ReadBytes(dataLength);
					break;

				default:
					throw new NotImplementedException(string.Format("{0} {1:X2}h/'{2}' is not implemented.", typeof(FbxValueType).Name, (int)Type, (char)Type));
			}
		}

		Exception Required(FbxValueType type) {
			return new InvalidOperationException(typeof(FbxValueType).Name + " " + type + " is required, but the " + typeof(FbxValue).Name + " is " + Type + ".");
		}

		void Require(FbxValueType type) { if (Type != type) throw Required(type); }

		public double AsDouble {
			get {
				switch (Type) {
					case FbxValueType.Double: return ValueCombo.Double;
					case FbxValueType.Int32: return ValueCombo.Int32;
					case FbxValueType.Int64: return ValueCombo.Int64;
					default: throw Required(FbxValueType.Double);
				}
			}
		}

		public double[] AsDoubleArray { get { Require(FbxValueType.DoubleArray); return (Double[])ValueObject; } }

		public int AsInt32 {
			get {
				switch (Type) {
					case FbxValueType.Int32: return ValueCombo.Int32;
					default: throw Required(FbxValueType.Int32);
				}
			}
		}

		public int[] AsInt32Array { get { Require(FbxValueType.Int32Array); return (Int32[])ValueObject; } }

		public long AsInt64 {
			get {
				switch (Type) {
					case FbxValueType.Int32: return ValueCombo.Int32;
					case FbxValueType.Int64: return ValueCombo.Int64;
					default: throw Required(FbxValueType.Int64);
				}
			}
		}

		public string AsString { get { Require(FbxValueType.String); return (String)ValueObject; } }

		public static explicit operator double(FbxValue field) { return field.AsDouble; }
		public static explicit operator int(FbxValue field) { return field.AsInt32; }
		public static explicit operator long(FbxValue field) { return field.AsInt64; }
		public static explicit operator string(FbxValue field) { return field.AsString; }
	}

	public enum FbxValueType : byte {
		None = 0,

		/// <summary>Double array, stored as (int count, int zero, int dataLength, double[count] data).</summary>
		DoubleArray = (Byte)'d',

		/// <summary>32-bit signed integer array, stored as (int count, int zero, int dataLength, int[count] data).</summary>
		Int32Array = (Byte)'i',

		/// <summary>Double-precision floating point.</summary>
		Double = (Byte)'D',

		/// <summary>32-bit signed integer.</summary>
		Int32 = (Byte)'I',

		/// <summary>64-bit signed integer.</summary>
		Int64 = (Byte)'L',

		Data = (Byte)'R',

		/// <summary>Stored as (int length, byte[length] data), where data are UTF-8. Sometimes contains codes in them, like "GlobalInfo\x00\x01SceneInfo".</summary>
		String = (Byte)'S',
	}
}
