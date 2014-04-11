using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare
{
	/// <summary>
	/// A value between <c>0.0f</c> and <c>1.0f</c> that is stored in a single <see cref="Byte"/>.
	/// </summary>
	public struct NormalizedByte : IComparable<NormalizedByte>, IEquatable<NormalizedByte>, IFormattable
	{
		internal readonly byte value;
		NormalizedByte(byte value) { this.value = value; }

		public NormalizedByte(float value) : this((byte)(value < 0 ? byte.MinValue : value > 1 ? byte.MaxValue : ((byte)value * byte.MaxValue))) { }
		public NormalizedByte(double value) : this((byte)(value < 0 ? byte.MinValue : value > 1 ? byte.MaxValue : ((byte)value * byte.MaxValue))) { }

		public static readonly NormalizedByte Zero = new NormalizedByte(0);
		public static readonly NormalizedByte One = new NormalizedByte(1);

		public int CompareTo(double other) { return ((double)this).CompareTo(other); }
		public int CompareTo(NormalizedByte other) { return value.CompareTo(other.value); }
		public int CompareTo(NormalizedSByte other) { return ((double)this).CompareTo(other); }
		public int CompareTo(NormalizedInt32 other) { return ((double)this).CompareTo(other); }

		public static NormalizedByte CreateCoded(byte value) { return new NormalizedByte(value); }

		public override bool Equals(object obj)
		{
			if (obj is Single) return Equals((Single)obj);
			if (obj is Double) return Equals((Double)obj);
			if (obj is NormalizedByte) return Equals((NormalizedByte)obj);
			if (obj is NormalizedSByte) return Equals((NormalizedSByte)obj);
			if (obj is NormalizedInt32) return Equals((NormalizedInt32)obj);
			return base.Equals(obj);
		}

		public bool Equals(double obj) { return (double)this == obj; }
		public bool Equals(NormalizedByte obj) { return value == obj.value; }
		public bool Equals(NormalizedSByte obj) { return (double)this == (double)obj; }
		public bool Equals(NormalizedInt32 obj) { return (double)this == (double)obj; }

		public override int GetHashCode() { return value.GetHashCode(); }

		public override string ToString() { return ToString(null, null); }
		public string ToString(string format, IFormatProvider provider) { return ((float)this).ToString(format, provider) + "nb"; }

		public static implicit operator float(NormalizedByte value) { return (float)(value.value / (double)byte.MaxValue); }
		public static implicit operator double(NormalizedByte value) { return value.value / (double)byte.MaxValue; }
		public static explicit operator int(NormalizedByte value) { return (int)(float)value; }
		public static explicit operator NormalizedByte(float value) { return new NormalizedByte(value); }
		public static explicit operator NormalizedByte(double value) { return new NormalizedByte(value); }
		public static explicit operator NormalizedByte(int value) { return new NormalizedByte(value); }
		public static explicit operator NormalizedByte(NormalizedSByte value) { return new NormalizedByte((float)value); }

		public static bool operator ==(NormalizedByte a, NormalizedByte b) { return a.value == b.value; }
		public static bool operator !=(NormalizedByte a, NormalizedByte b) { return a.value != b.value; }

		public static bool operator >(NormalizedByte a, NormalizedByte b) { return a.CompareTo(b) > 0; }
		public static bool operator >=(NormalizedByte a, NormalizedByte b) { return a.CompareTo(b) > 0; }
		public static bool operator <(NormalizedByte a, NormalizedByte b) { return a.CompareTo(b) < 0; }
		public static bool operator <=(NormalizedByte a, NormalizedByte b) { return a.CompareTo(b) <= 0; }
	}
}
