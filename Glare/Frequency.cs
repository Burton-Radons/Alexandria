using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare {
			/// <summary>A measurement of length by time, created as <see cref="Length"/> divided by <see cref="TimeSpan"/>.</summary>
		public struct Frequency : IComparable<Frequency>, IEquatable<Frequency> {
			internal const double ToSeconds = Velocity.ToSeconds;

			/// <summary>This is the constant value for conversion from the OneHertz unit to the stored value when divided, or back to OneHertz when multiplied.</summary>
		internal const Double ToHertz = (Double)(1);

		/// <summary>Create a <see cref="Frequency"/> by providing a OneHertz value.</summary>
		public static Frequency Hertz(Double value) { return new Frequency(value / ToHertz); }

		/// <summary>Get the <see cref="Frequency"/> value in the OneHertz unit.</summary>
		public Double InHertz { get { return value * ToHertz; } }

		/// <summary>Clamp this <see cref="Frequency"/> value to the OneHertz range, returning the clamped value.</summary>
		public Frequency ClampHertz(Double min, Double max) {
			min = min / ToHertz;
			max = max / ToHertz;
			return new Frequency(value > max ? max : value < min ? min : value);
		}

		/// <summary>Clamp this <see cref="Frequency"/> value to the OneHertz range, changing this value, then return it.</summary>
		public Frequency ClampInPlaceHertz(Double min, Double max) {
			if(value > (max = max / ToHertz))
				value = max;
			else if(value < (min = min / ToHertz))
				value = min;
			return this;
		}

		/// <summary>Get the absolute value of this <see cref="Frequency"/>.</summary>
		public Frequency AsUnsigned { get { return new Frequency(Math.Abs(value)); }}

		/// <summary>Create a <see cref="Frequency"/> by providing the universal unit, which is Hertz.</summary>
		public static Frequency Universal(double value) { return Hertz(value); }

		/// <summary>Get this <see cref="Frequency"/> in the universal unit, which is Hertz.</summary>
		public double InUniversal { get { return InHertz; } }

		/// <summary>The coded value.</summary>
		Double value;

		/// <summary>Private constructor for the <see cref="Frequency"/>; the real constructors are the static unit methods.</summary>
		Frequency(Double value) { this.value = value; }

		/// <summary>Get the zero value for <see cref="Frequency"/>.</summary>
		public static readonly Frequency Zero = new Frequency(0);

		/// <summary>Get the positive infinity value for <see cref="Frequency"/>.</summary>
		public static readonly Frequency PositiveInfinity = new Frequency(Double.PositiveInfinity);

		/// <summary>Get the negative infinity value for <see cref="Frequency"/>.</summary>
		public static readonly Frequency NegativeInfinity = new Frequency(Double.NegativeInfinity);

		/// <summary>Get the not-a-number value for <see cref="Frequency"/>.</summary>
		public static readonly Frequency NaN = new Frequency(Double.NaN);

		/// <summary>Clamp this <see cref="Frequency"/> to the provided range, returning the result.</summary>
		public Frequency Clamp(Frequency min, Frequency max) { return new Frequency(value > max.value ? max.value : value < min.value ? min.value : value); }

		/// <summary>Clamp this <see cref="Frequency"/> to the provided range, storing the result in this value and returning it.</summary>
		public Frequency ClampInPlace(Frequency min, Frequency max) {
			if(value > max.value)
				value = min.value;
			else if(value < min.value)
				value = min.value;
			return this;
		}

		/// <summary>Compare this <see cref="Frequency"/> to the other value, returning the relative order.</summary>.
		public int CompareTo(Frequency other) { return value.CompareTo(other.value); }

		/// <summary>Compare this <see cref="Frequency"/> to the other value, returning whether they are equal.</summary>
		public bool Equals(Frequency other) { return value == other.value; }

		/// <summary>If the other object is <see cref="Frequency"/>, determine equality and return the result; otherwise return <c>null</c>.</summary>
		public override bool Equals(object other) {
			if(other is Frequency)
				return value == ((Frequency)other).value;
			return base.Equals(other);
		}

		/// <summary>Get a hash code from the value.</summary>
		public override int GetHashCode() { return value.GetHashCode(); }

		/// <summary>Return the maximum of this value or the passed value.</summary>
		public Frequency Max(Frequency other) { return new Frequency(other.value > value ? other.value : value); }

		/// <summary>Assign this <see cref="Frequency"/> to the maximum of this value or the other one.</summary>
		public void MaxInPlace(Frequency other) { if(other.value > value) value = other.value; }

		/// <summary>Return the minimum of this value or the passed value.</summary>
		public Frequency Min(Frequency other) { return new Frequency(other.value < value ? other.value : value); }

		/// <summary>Assign this <see cref="Frequency"/> to the minimum of this value or the other one.</summary>
		public void MinInPlace(Frequency other) { if(other.value < value) value = other.value; }

		/// <summary>Convert to a string of the form "&lt;value&gt;Hz".</summary>
		public override string ToString() { return ToString(null, null); }

		/// <summary>Convert to a string of the form "&lt;value&gt;Hz".</summary>
		public string ToString(string format, IFormatProvider provider) { return InHertz.ToString(format, provider) + "Hz"; }

		/// <summary>Test for equality between the <see cref="Frequency"/> values.</summary>
		public static bool operator ==(Frequency a, Frequency b) { return a.value == b.value; }

		/// <summary>Test for inequality between the <see cref="Frequency"/> values.</summary>
		public static bool operator !=(Frequency a, Frequency b) { return a.value != b.value; }

		/// <summary>Compare the <see cref="Frequency"/> values.</summary>
		public static bool operator >(Frequency a, Frequency b) { return a.value > b.value; }

		/// <summary>Compare the <see cref="Frequency"/> values.</summary>
		public static bool operator >=(Frequency a, Frequency b) { return a.value >= b.value; }

		/// <summary>Compare the <see cref="Frequency"/> values.</summary>
		public static bool operator <(Frequency a, Frequency b) { return a.value < b.value; }

		/// <summary>Compare the <see cref="Frequency"/> values.</summary>
		public static bool operator <=(Frequency a, Frequency b) { return a.value <= b.value; }

		/// <summary>Return the positive of the <see cref="Frequency"/> value, which is itself.</summary>
		public static Frequency operator +(Frequency value) { return new Frequency(+value.value); }

		/// <summary>Return the negative of the <see cref="Frequency"/> value.</summary>
		public static Frequency operator -(Frequency value) { return new Frequency(-value.value); }

		/// <summary>Add the <see cref="Frequency"/> values.</summary>
		public static Frequency operator +(Frequency a, Frequency b) { return new Frequency(a.value + b.value); }

		/// <summary>Subtract the <see cref="Frequency"/> values.</summary>
		public static Frequency operator -(Frequency a, Frequency b) { return new Frequency(a.value - b.value); }

		/// <summary>Multiply the <see cref="Frequency"/> value to a scalar.</summary>
		public static Frequency operator *(Frequency a, Double b) { return new Frequency(a.value * b); }

		/// <summary>Multiply the <see cref="Frequency"/> value to a scalar.</summary>
		public static Frequency operator *(Double a, Frequency b) { return new Frequency(a * b.value); }

		/// <summary>Divide the <see cref="Frequency"/> value by a scalar.</summary>
		public static Frequency operator /(Frequency a, Double b) { return new Frequency(a.value / b); }

		/// <summary>Divide the <see cref="Frequency"/> values, producing the scalar magnitude difference between them.</summary>
		public static Double operator /(Frequency a, Frequency b) { return a.value / b.value; }

		/// <summary>Modulo the <see cref="Frequency"/> values, producing the scalar result.</summary>
		public static Double operator %(Frequency a, Frequency b) { return a.value % b.value; }
	
			/// <summary>Initialise the frequency based on the number of oscillations over some time.</summary>
			public Frequency(double number, TimeSpan span) { value = (number / span.TotalSeconds) / ToHertz; }
		}
	}



