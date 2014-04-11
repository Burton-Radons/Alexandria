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

			internal const double ToHertz = (double)(1);

		public static Frequency Hertz(double value) { return new Frequency(value / ToHertz); }

		public double InHertz { get { return value * ToHertz; } }

		public static readonly Frequency OneHertz = Hertz(1);

		public Frequency ClampHertz(double min, double max)
		{
			min = min / ToHertz;
			max = max / ToHertz;
			return new Frequency(value > max ? max : value < min ? min : value);
		}

		public void ClampInPlaceHertz(double min, double max)
		{
			if(value > (max = max / ToHertz))
				value = max;
			else if(value < (min = min / ToHertz))
				value = min;
		}

				double value;
		Frequency(double value) { this.value = value; }

		public static readonly Frequency Zero = new Frequency(0);
		public static readonly Frequency PositiveInfinity = new Frequency(double.PositiveInfinity);
		public static readonly Frequency NegativeInfinity = new Frequency(double.NegativeInfinity);
		public static readonly Frequency NaN = new Frequency(double.NaN);

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
		public static Frequency operator *(Frequency a, double b) { return new Frequency(a.value * b); }
		public static Frequency operator *(double a, Frequency b) { return new Frequency(a * b.value); }
		public static Frequency operator /(Frequency a, double b) { return new Frequency(a.value / b); }
		public static double operator %(Frequency a, Frequency b) { return a.value % b.value; }
	
			public Frequency(double number, TimeSpan span) { value = (number / span.TotalSeconds) / ToHertz; }
		}
	}



