using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare
{
			/// <summary>A measurement of length by time, created as <see cref="Length"/> divided by <see cref="TimeSpan"/>.</summary>
		public struct Frequency : IComparable<Frequency>, IEquatable<Frequency>
		{
			internal const double ToSeconds = Velocity.ToSeconds;

			internal const Double ToHertz = (Double)(1);

		public static Frequency Hertz(Double value) { return new Frequency(value / ToHertz); }

		public Double InHertz { get { return value * ToHertz; } }

		public static readonly Frequency OneHertz = Hertz(1);

		public Frequency ClampHertz(Double min, Double max) {
			min = min / ToHertz;
			max = max / ToHertz;
			return new Frequency(value > max ? max : value < min ? min : value);
		}

		public void ClampInPlaceHertz(Double min, Double max) {
			if(value > (max = max / ToHertz))
				value = max;
			else if(value < (min = min / ToHertz))
				value = min;
		}

				public Frequency Absolute { get { return new Frequency(Math.Abs(value)); }}

		public static Frequency Universal(double value) { return Hertz(value); }
		public double InUniversal { get { return InHertz; } }

		Double value;
		Frequency(Double value) { this.value = value; }

		public static readonly Frequency Zero = new Frequency(0);
		public static readonly Frequency PositiveInfinity = new Frequency(Double.PositiveInfinity);
		public static readonly Frequency NegativeInfinity = new Frequency(Double.NegativeInfinity);
		public static readonly Frequency NaN = new Frequency(Double.NaN);

		public Frequency Clamp(Frequency min, Frequency max) { return new Frequency(value > max.value ? max.value : value < min.value ? min.value : value); }

		public void ClampInPlace(Frequency min, Frequency max) { if(value > max.value) value = min.value; else if(value < min.value) value = min.value; }

		public int CompareTo(Frequency other) { return value.CompareTo(other.value); }

		public bool Equals(Frequency other) { return value == other.value; }
		public override bool Equals(object other) { if(other is Frequency) return value == ((Frequency)other).value; return base.Equals(other); }

		public override int GetHashCode() { return value.GetHashCode(); }

		/// <summary>Return the maximum of this value or the passed value.</summary>
		public Frequency Max(Frequency other) { return new Frequency(other.value > value ? other.value : value); }

		/// <summary>Assign this <see cref="Frequency"/> to the maximum of this value or the other one.</summary>
		public void MaxInPlace(Frequency other) { if(other.value > value) value = other.value; }

		/// <summary>Return the minimum of this value or the passed value.</summary>
		public Frequency Min(Frequency other) { return new Frequency(other.value < value ? other.value : value); }

		/// <summary>Assign this <see cref="Frequency"/> to the minimum of this value or the other one.</summary>
		public void MinInPlace(Frequency other) { if(other.value < value) value = other.value; }

		/// <summary>Convert to a string of the form "<value>Hz".</summary>
		public override string ToString() { return ToString(null, null); }

		/// <summary>Convert to a string of the form "<value>Hz".</summary>
		public string ToString(string format, IFormatProvider provider) { return InHertz.ToString(format, provider) + "Hz"; }

		public static bool operator ==(Frequency a, Frequency b) { return a.value == b.value; }
		public static bool operator !=(Frequency a, Frequency b) { return a.value != b.value; }
		public static bool operator >(Frequency a, Frequency b) { return a.value > b.value; }
		public static bool operator >=(Frequency a, Frequency b) { return a.value >= b.value; }
		public static bool operator <(Frequency a, Frequency b) { return a.value < b.value; }
		public static bool operator <=(Frequency a, Frequency b) { return a.value <= b.value; }

		public static Frequency operator +(Frequency value) { return new Frequency(+value.value); }
		public static Frequency operator -(Frequency value) { return new Frequency(-value.value); }

		public static Frequency operator +(Frequency a, Frequency b) { return new Frequency(a.value + b.value); }
		public static Frequency operator -(Frequency a, Frequency b) { return new Frequency(a.value - b.value); }
		public static Frequency operator *(Frequency a, Double b) { return new Frequency(a.value * b); }
		public static Frequency operator *(Double a, Frequency b) { return new Frequency(a * b.value); }
		public static Frequency operator /(Frequency a, Double b) { return new Frequency(a.value / b); }
		public static Double operator /(Frequency a, Frequency b) { return a.value / b.value; }
		public static Double operator %(Frequency a, Frequency b) { return a.value % b.value; }
	
			public Frequency(double number, TimeSpan span) { value = (number / span.TotalSeconds) / ToHertz; }
		}
	}





