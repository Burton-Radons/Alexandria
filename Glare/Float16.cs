using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Glare {
	/// <summary>
	/// 16-bit floating-point value.
	/// </summary>
	public struct Float16 : IEquatable<Float16>, IFormattable {
		[StructLayout(LayoutKind.Explicit)]
		struct FloatUnion {
			[FieldOffset(0)]
			public float Float;
			[FieldOffset(0)]
			public int Int;
			[FieldOffset(0)]
			public uint UInt;
		}

		public ushort PackedValue;

		public double Value { get { return ToFloat(PackedValue); } }

		public Float16(double value) { PackedValue = ToUInt16(checked((float)value)); }

		public static Float16 CreateCoded(ushort packedValue) { Float16 result; result.PackedValue = packedValue; return result; }

		public bool Equals(Float16 other) { return PackedValue == other.PackedValue; }

		public override bool Equals(object obj) {
			if (obj is Double) return Value == (float)obj;
			if (obj is Float16) return PackedValue == ((Float16)obj).PackedValue;
			if (obj is Single) return Value == (float)obj;
			return base.Equals(obj);
		}

		public override int GetHashCode() {
			return PackedValue.GetHashCode();
		}

		public static Float16 Read(BinaryReader reader) { Float16 result; result.PackedValue = reader.ReadUInt16(); return result; }

		public static void Read(BinaryReader reader, out Float16 result) { result.PackedValue = reader.ReadUInt16(); }

		public override string ToString() {
			return Value.ToString();
		}

		public string ToString(string format, IFormatProvider formatProvider) {
			return Value.ToString(format, formatProvider);
		}

		static UInt16 ToUInt16(float value) {
			int intValue = new FloatUnion() { Float = value }.Int; 
			int sign = (intValue >> 16) & 0x00008000;
			int exponent = ((intValue >> 23) & 0x000000ff) - (127 - 15);
			int mantissa = intValue & 0x007fffff;

			if (exponent <= 0) {
				if (exponent < -10)
					return (UInt16)sign;

				mantissa = mantissa | 0x00800000;

				int t = 14 - exponent;
				int a = (1 << (t - 1)) - 1;
				int b = (mantissa >> t) & 1;

				mantissa = (mantissa + a + b) >> t;

				return (UInt16)(sign | mantissa);
			} else if (exponent == 0xff - (127 - 15)) {
				if (mantissa == 0)
					return (UInt16)(sign | 0x7c00);
				else {
					mantissa >>= 13;
					return (UInt16)(sign | 0x7c00 | mantissa | ((mantissa == 0) ? 1 : 0));
				}
			} else {
				mantissa = mantissa + 0x00000fff + ((mantissa >> 13) & 1);

				if ((mantissa & 0x00800000) != 0) {
					mantissa = 0;
					exponent += 1;
				}

				if (exponent > 30)
					return (UInt16)(sign | 0x7c00);

				return (UInt16)(sign | (exponent << 10) | (mantissa >> 13));
			}
		}

		static float ToFloat(ushort value) {
			uint result;
			uint mantissa = (uint)(value & 1023);
			uint exponent = 0xfffffff2;

			if ((value & -33792) == 0) {
				if (mantissa != 0) {
					while ((mantissa & 1024) == 0) {
						exponent--;
						mantissa = mantissa << 1;
					}
					mantissa &= 0xfffffbff;
					result = ((uint)((((uint)value & 0x8000) << 16) | ((exponent + 127) << 23))) | (mantissa << 13);
				} else {
					result = (uint)((value & 0x8000) << 16);
				}
			} else {
				result = (uint)(((((uint)value & 0x8000) << 16) | ((((((uint)value >> 10) & 0x1f) - 15) + 127) << 23)) | (mantissa << 13));
			}

			return new FloatUnion() { UInt = result }.Float;
		}

		public static Float16 operator +(Float16 value) { return new Float16(+(double)value); }
		public static Float16 operator -(Float16 value) { return new Float16(-(double)value); }
		public static Float16 operator +(Float16 a, Float16 b) { return new Float16(a.Value + b.Value); }
		public static Float16 operator -(Float16 a, Float16 b) { return new Float16(a.Value - b.Value); }
		public static Float16 operator *(Float16 a, Float16 b) { return new Float16(a.Value * b.Value); }
		public static Float16 operator /(Float16 a, Float16 b) { return new Float16(a.Value / b.Value); }
		public static Float16 operator %(Float16 a, Float16 b) { return new Float16(a.Value % b.Value); }

		public static bool operator ==(Float16 a, Float16 b) { return a.PackedValue == b.PackedValue; }
		public static bool operator !=(Float16 a, Float16 b) { return a.PackedValue != b.PackedValue; }

		public static explicit operator Float16(Double value) { return new Float16(value); }
		public static explicit operator Float16(NormalizedByte value) { return new Float16(value); }
		public static explicit operator Float16(NormalizedInt32 value) { return new Float16(value); }
		public static explicit operator Float16(NormalizedSByte value) { return new Float16(value); }

		public static explicit operator NormalizedByte(Float16 value) { return (NormalizedByte)value.Value; }
		public static explicit operator NormalizedInt32(Float16 value) { return (NormalizedInt32)value.Value; }
		public static explicit operator NormalizedSByte(Float16 value) { return (NormalizedSByte)value.Value; }
		public static implicit operator Single(Float16 value) { return (Single)value.Value; }
	}
}
