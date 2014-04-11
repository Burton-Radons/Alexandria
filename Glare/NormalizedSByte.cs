using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare
{
	/// <summary>
	/// A value between <c>-1.0f</c> and <c>1.0f</c> that is stored in a single <see cref="SByte"/>.
	/// </summary>
	public struct NormalizedSByte : IComparable<NormalizedSByte>, IEquatable<NormalizedSByte>, IFormattable
	{
		internal readonly SByte value;
		NormalizedSByte(sbyte value) { this.value = value; }

		public NormalizedSByte(float value) : this((SByte)(value < -1 ? SByte.MinValue : value > 1 ? SByte.MaxValue : ((SByte)value * SByte.MaxValue))) { }
		public NormalizedSByte(double value) : this((SByte)(value < -1 ? SByte.MinValue : value > 1 ? SByte.MaxValue : ((SByte)value * SByte.MaxValue))) { }

		public static readonly NormalizedSByte Zero = new NormalizedSByte(0);
		public static readonly NormalizedSByte One = new NormalizedSByte(1);
		public static readonly NormalizedSByte NegativeOne = new NormalizedSByte(-1);

		public int CompareTo(NormalizedByte other) { return ((float)this).CompareTo(other.value); }
		public int CompareTo(NormalizedSByte other) { return value.CompareTo(other.value); }
		public int CompareTo(NormalizedInt32 other) { return ((float)this).CompareTo(other); }

		public static NormalizedSByte CreateCoded(sbyte value) { return new NormalizedSByte(value); }

		public override bool Equals(object obj)
		{
			if (obj is NormalizedSByte) return Equals((NormalizedSByte)obj);
			if (obj is NormalizedByte) return Equals((NormalizedByte)obj);
			return base.Equals(obj);
		}

		public bool Equals(NormalizedByte obj) { return value == obj.value; }
		public bool Equals(NormalizedSByte obj) { return value == obj.value; }

		public override int GetHashCode() { return value.GetHashCode(); }

		public override string ToString() { return ToString(null, null); }
		public string ToString(string format, IFormatProvider provider) { return ((float)this).ToString(format, provider) + "nsb"; }

		public static implicit operator float(NormalizedSByte value) { return value.value == sbyte.MinValue ? -1 : value.value / (float)SByte.MaxValue; }
		public static implicit operator double(NormalizedSByte value) { return value.value == sbyte.MinValue ? -1 : value.value / (double)SByte.MaxValue; }
		public static explicit operator int(NormalizedSByte value) { return (int)(float)value; }
		public static explicit operator NormalizedSByte(float value) { return new NormalizedSByte(value); }
		public static explicit operator NormalizedSByte(double value) { return new NormalizedSByte(value); }
		public static explicit operator NormalizedSByte(int value) { return new NormalizedSByte(value); }
		public static explicit operator NormalizedSByte(NormalizedByte value) { return new NormalizedSByte((float)value); }

		public static bool operator ==(NormalizedSByte a, NormalizedSByte b) { return a.value == b.value; }
		public static bool operator ==(NormalizedSByte a, NormalizedByte b) { return a.value == b.value; }
		public static bool operator ==(NormalizedByte a, NormalizedSByte b) { return a.value == b.value; }
		public static bool operator !=(NormalizedSByte a, NormalizedSByte b) { return a.value != b.value; }
		public static bool operator !=(NormalizedByte a, NormalizedSByte b) { return a.value != b.value; }
		public static bool operator !=(NormalizedSByte a, NormalizedByte b) { return a.value != b.value; }

		public static bool operator >(NormalizedSByte a, NormalizedSByte b) { return a.CompareTo(b) > 0; }
		public static bool operator >=(NormalizedSByte a, NormalizedSByte b) { return a.CompareTo(b) > 0; }
		public static bool operator <(NormalizedSByte a, NormalizedSByte b) { return a.CompareTo(b) < 0; }
		public static bool operator <=(NormalizedSByte a, NormalizedSByte b) { return a.CompareTo(b) <= 0; }

		public static bool operator >(NormalizedSByte a, NormalizedByte b) { return a.CompareTo(b) > 0; }
		public static bool operator >=(NormalizedSByte a, NormalizedByte b) { return a.CompareTo(b) > 0; }
		public static bool operator <(NormalizedSByte a, NormalizedByte b) { return a.CompareTo(b) < 0; }
		public static bool operator <=(NormalizedSByte a, NormalizedByte b) { return a.CompareTo(b) <= 0; }

		public static bool operator >(NormalizedByte a, NormalizedSByte b) { return a.CompareTo(b) > 0; }
		public static bool operator >=(NormalizedByte a, NormalizedSByte b) { return a.CompareTo(b) > 0; }
		public static bool operator <(NormalizedByte a, NormalizedSByte b) { return a.CompareTo(b) < 0; }
		public static bool operator <=(NormalizedByte a, NormalizedSByte b) { return a.CompareTo(b) <= 0; }
	}
}
