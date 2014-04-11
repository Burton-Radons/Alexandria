using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare
{
	/// <summary>
	/// A value between <c>-1.0f</c> and <c>1.0f</c> that is stored in a single <see cref="Int32"/>.
	/// </summary>
	public struct NormalizedInt32 : IComparable<NormalizedInt32>, IEquatable<NormalizedInt32>, IFormattable
	{
		internal readonly Int32 value;
		NormalizedInt32(Int32 value) { this.value = value; }

		public NormalizedInt32(double value) : this((Int32)(value < -1 ? Int32.MinValue : value > 1 ? Int32.MaxValue : ((Int32)value * Int32.MaxValue))) { }

		public static readonly NormalizedInt32 Zero = new NormalizedInt32(0);
		public static readonly NormalizedInt32 One = new NormalizedInt32(1);
		public static readonly NormalizedInt32 NegativeOne = new NormalizedInt32(-1);

		public int CompareTo(NormalizedByte other) { return ((double)this).CompareTo((double)other); }
		public int CompareTo(NormalizedSByte other) { return ((double)this).CompareTo((double)other); }
		public int CompareTo(NormalizedInt32 other) { return value.CompareTo(other.value); }

		public override bool Equals(object obj)
		{
			if (obj is NormalizedInt32) return value == ((NormalizedInt32)obj).value;
			if (obj is NormalizedByte) return Equals((NormalizedByte)obj);
			return base.Equals(obj);
		}

		public bool Equals(NormalizedByte obj) { return (double)this == (double)obj; }
		public bool Equals(NormalizedSByte obj) { return (double)this == (double)obj; }
		public bool Equals(NormalizedInt32 obj) { return value == obj.value; }

		public override int GetHashCode() { return value.GetHashCode(); }

		public override string ToString() { return ToString(null, null); }
		public string ToString(string format, IFormatProvider provider) { return ((float)this).ToString(format, provider) + "ni"; }

		public static implicit operator float(NormalizedInt32 value) { return value.value == Int32.MinValue ? -1 : (float)(value.value / (double)Int32.MaxValue); }
		public static implicit operator double(NormalizedInt32 value) { return value.value == Int32.MinValue ? -1 : value.value / (double)Int32.MaxValue; }
		public static explicit operator int(NormalizedInt32 value) { return (int)(float)value; }
		public static explicit operator NormalizedInt32(float value) { return new NormalizedInt32(value); }
		public static explicit operator NormalizedInt32(double value) { return new NormalizedInt32(value); }
		public static explicit operator NormalizedInt32(int value) { return new NormalizedInt32(value); }
		public static explicit operator NormalizedInt32(NormalizedByte value) { return new NormalizedInt32((float)value); }

		public static bool operator ==(NormalizedInt32 a, NormalizedInt32 b) { return a.value == b.value; }
		public static bool operator ==(NormalizedInt32 a, NormalizedByte b) { return a.value == b.value; }
		public static bool operator ==(NormalizedByte a, NormalizedInt32 b) { return a.value == b.value; }
		public static bool operator !=(NormalizedInt32 a, NormalizedInt32 b) { return a.value != b.value; }
		public static bool operator !=(NormalizedByte a, NormalizedInt32 b) { return a.value != b.value; }
		public static bool operator !=(NormalizedInt32 a, NormalizedByte b) { return a.value != b.value; }

		public static bool operator >(NormalizedInt32 a, NormalizedInt32 b) { return a.CompareTo(b) > 0; }
		public static bool operator >=(NormalizedInt32 a, NormalizedInt32 b) { return a.CompareTo(b) > 0; }
		public static bool operator <(NormalizedInt32 a, NormalizedInt32 b) { return a.CompareTo(b) < 0; }
		public static bool operator <=(NormalizedInt32 a, NormalizedInt32 b) { return a.CompareTo(b) <= 0; }

		public static bool operator >(NormalizedInt32 a, NormalizedByte b) { return a.CompareTo(b) > 0; }
		public static bool operator >=(NormalizedInt32 a, NormalizedByte b) { return a.CompareTo(b) > 0; }
		public static bool operator <(NormalizedInt32 a, NormalizedByte b) { return a.CompareTo(b) < 0; }
		public static bool operator <=(NormalizedInt32 a, NormalizedByte b) { return a.CompareTo(b) <= 0; }

		public static bool operator >(NormalizedByte a, NormalizedInt32 b) { return a.CompareTo(b) > 0; }
		public static bool operator >=(NormalizedByte a, NormalizedInt32 b) { return a.CompareTo(b) > 0; }
		public static bool operator <(NormalizedByte a, NormalizedInt32 b) { return a.CompareTo(b) < 0; }
		public static bool operator <=(NormalizedByte a, NormalizedInt32 b) { return a.CompareTo(b) <= 0; }

		public static bool operator >(NormalizedInt32 a, NormalizedSByte b) { return a.CompareTo(b) > 0; }
		public static bool operator >=(NormalizedInt32 a, NormalizedSByte b) { return a.CompareTo(b) > 0; }
		public static bool operator <(NormalizedInt32 a, NormalizedSByte b) { return a.CompareTo(b) < 0; }
		public static bool operator <=(NormalizedInt32 a, NormalizedSByte b) { return a.CompareTo(b) <= 0; }

		public static bool operator >(NormalizedSByte a, NormalizedInt32 b) { return a.CompareTo(b) > 0; }
		public static bool operator >=(NormalizedSByte a, NormalizedInt32 b) { return a.CompareTo(b) > 0; }
		public static bool operator <(NormalizedSByte a, NormalizedInt32 b) { return a.CompareTo(b) < 0; }
		public static bool operator <=(NormalizedSByte a, NormalizedInt32 b) { return a.CompareTo(b) <= 0; }

	}
}
