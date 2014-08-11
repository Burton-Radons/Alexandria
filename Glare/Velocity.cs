using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare {
			/// <summary>A measurement of length by time, created as <see cref="Length"/> divided by <see cref="TimeSpan"/>.</summary>
		public struct Velocity : IComparable<Velocity>, IEquatable<Velocity> {
			internal const double ToSeconds = 1;

			/// <summary>This is the constant value for conversion from the MetrePerSecond unit to the stored value when divided, or back to MetrePerSecond when multiplied.</summary>
		internal const Double ToMetresPerSecond = (Double)(Length.ToMetres / ToSeconds);

		/// <summary>Create a <see cref="Velocity"/> by providing a MetrePerSecond value.</summary>
		public static Velocity MetresPerSecond(Double value) { return new Velocity(value / ToMetresPerSecond); }

		/// <summary>Get the <see cref="Velocity"/> value in the MetrePerSecond unit.</summary>
		public Double InMetresPerSecond { get { return value * ToMetresPerSecond; } }

		/// <summary>Clamp this <see cref="Velocity"/> value to the MetrePerSecond range, returning the clamped value.</summary>
		public Velocity ClampMetresPerSecond(Double min, Double max) {
			min = min / ToMetresPerSecond;
			max = max / ToMetresPerSecond;
			return new Velocity(value > max ? max : value < min ? min : value);
		}

		/// <summary>Clamp this <see cref="Velocity"/> value to the MetrePerSecond range, changing this value, then return it.</summary>
		public Velocity ClampInPlaceMetresPerSecond(Double min, Double max) {
			if(value > (max = max / ToMetresPerSecond))
				value = max;
			else if(value < (min = min / ToMetresPerSecond))
				value = min;
			return this;
		}

		/// <summary>Get the absolute value of this <see cref="Velocity"/>.</summary>
		public Velocity AsUnsigned { get { return new Velocity(Math.Abs(value)); }}

		/// <summary>Create a <see cref="Velocity"/> by providing the universal unit, which is MetresPerSecond.</summary>
		public static Velocity Universal(double value) { return MetresPerSecond(value); }

		/// <summary>Get this <see cref="Velocity"/> in the universal unit, which is MetresPerSecond.</summary>
		public double InUniversal { get { return InMetresPerSecond; } }

		/// <summary>The coded value.</summary>
		Double value;

		/// <summary>Private constructor for the <see cref="Velocity"/>; the real constructors are the static unit methods.</summary>
		Velocity(Double value) { this.value = value; }

		/// <summary>Get the zero value for <see cref="Velocity"/>.</summary>
		public static readonly Velocity Zero = new Velocity(0);

		/// <summary>Get the positive infinity value for <see cref="Velocity"/>.</summary>
		public static readonly Velocity PositiveInfinity = new Velocity(Double.PositiveInfinity);

		/// <summary>Get the negative infinity value for <see cref="Velocity"/>.</summary>
		public static readonly Velocity NegativeInfinity = new Velocity(Double.NegativeInfinity);

		/// <summary>Get the not-a-number value for <see cref="Velocity"/>.</summary>
		public static readonly Velocity NaN = new Velocity(Double.NaN);

		/// <summary>Clamp this <see cref="Velocity"/> to the provided range, returning the result.</summary>
		public Velocity Clamp(Velocity min, Velocity max) { return new Velocity(value > max.value ? max.value : value < min.value ? min.value : value); }

		/// <summary>Clamp this <see cref="Velocity"/> to the provided range, storing the result in this value and returning it.</summary>
		public Velocity ClampInPlace(Velocity min, Velocity max) {
			if(value > max.value)
				value = min.value;
			else if(value < min.value)
				value = min.value;
			return this;
		}

		/// <summary>Compare this <see cref="Velocity"/> to the other value, returning the relative order.</summary>.
		public int CompareTo(Velocity other) { return value.CompareTo(other.value); }

		/// <summary>Compare this <see cref="Velocity"/> to the other value, returning whether they are equal.</summary>
		public bool Equals(Velocity other) { return value == other.value; }

		/// <summary>If the other object is <see cref="Velocity"/>, determine equality and return the result; otherwise return <c>null</c>.</summary>
		public override bool Equals(object other) {
			if(other is Velocity)
				return value == ((Velocity)other).value;
			return base.Equals(other);
		}

		/// <summary>Get a hash code from the value.</summary>
		public override int GetHashCode() { return value.GetHashCode(); }

		/// <summary>Return the maximum of this value or the passed value.</summary>
		public Velocity Max(Velocity other) { return new Velocity(other.value > value ? other.value : value); }

		/// <summary>Assign this <see cref="Velocity"/> to the maximum of this value or the other one.</summary>
		public void MaxInPlace(Velocity other) { if(other.value > value) value = other.value; }

		/// <summary>Return the minimum of this value or the passed value.</summary>
		public Velocity Min(Velocity other) { return new Velocity(other.value < value ? other.value : value); }

		/// <summary>Assign this <see cref="Velocity"/> to the minimum of this value or the other one.</summary>
		public void MinInPlace(Velocity other) { if(other.value < value) value = other.value; }

		/// <summary>Convert to a string of the form "&lt;value&gt;m·s⁻¹".</summary>
		public override string ToString() { return ToString(null, null); }

		/// <summary>Convert to a string of the form "&lt;value&gt;m·s⁻¹".</summary>
		public string ToString(string format, IFormatProvider provider) { return InMetresPerSecond.ToString(format, provider) + "m·s⁻¹"; }

		/// <summary>Test for equality between the <see cref="Velocity"/> values.</summary>
		public static bool operator ==(Velocity a, Velocity b) { return a.value == b.value; }

		/// <summary>Test for inequality between the <see cref="Velocity"/> values.</summary>
		public static bool operator !=(Velocity a, Velocity b) { return a.value != b.value; }

		/// <summary>Compare the <see cref="Velocity"/> values.</summary>
		public static bool operator >(Velocity a, Velocity b) { return a.value > b.value; }

		/// <summary>Compare the <see cref="Velocity"/> values.</summary>
		public static bool operator >=(Velocity a, Velocity b) { return a.value >= b.value; }

		/// <summary>Compare the <see cref="Velocity"/> values.</summary>
		public static bool operator <(Velocity a, Velocity b) { return a.value < b.value; }

		/// <summary>Compare the <see cref="Velocity"/> values.</summary>
		public static bool operator <=(Velocity a, Velocity b) { return a.value <= b.value; }

		/// <summary>Return the positive of the <see cref="Velocity"/> value, which is itself.</summary>
		public static Velocity operator +(Velocity value) { return new Velocity(+value.value); }

		/// <summary>Return the negative of the <see cref="Velocity"/> value.</summary>
		public static Velocity operator -(Velocity value) { return new Velocity(-value.value); }

		/// <summary>Add the <see cref="Velocity"/> values.</summary>
		public static Velocity operator +(Velocity a, Velocity b) { return new Velocity(a.value + b.value); }

		/// <summary>Subtract the <see cref="Velocity"/> values.</summary>
		public static Velocity operator -(Velocity a, Velocity b) { return new Velocity(a.value - b.value); }

		/// <summary>Multiply the <see cref="Velocity"/> value to a scalar.</summary>
		public static Velocity operator *(Velocity a, Double b) { return new Velocity(a.value * b); }

		/// <summary>Multiply the <see cref="Velocity"/> value to a scalar.</summary>
		public static Velocity operator *(Double a, Velocity b) { return new Velocity(a * b.value); }

		/// <summary>Divide the <see cref="Velocity"/> value by a scalar.</summary>
		public static Velocity operator /(Velocity a, Double b) { return new Velocity(a.value / b); }

		/// <summary>Divide the <see cref="Velocity"/> values, producing the scalar magnitude difference between them.</summary>
		public static Double operator /(Velocity a, Velocity b) { return a.value / b.value; }

		/// <summary>Modulo the <see cref="Velocity"/> values, producing the scalar result.</summary>
		public static Double operator %(Velocity a, Velocity b) { return a.value % b.value; }
			}

		/// <summary>A 3-dimensional <see cref="Velocity"/> value.</summary>
		public struct Velocity3 : IEquatable<Velocity3> {
			/// <summary>Get or set the first axis.</summary>
			public Velocity X;

			/// <summary>Get or set the second axis.</summary>
			public Velocity Y;

			/// <summary>Get or set the third axis.</summary>
			public Velocity Z;

			/// <summary>Initialise the velocity vector.</summary>
			public Velocity3(Velocity x, Velocity y, Velocity z) { X = x; Y = y; Z = z; }

			/// <summary>Get a zero velocity vector.</summary>
			public static readonly Velocity3 Zero = MetresPerSecond(0, 0, 0);

			/// <summary>Get whether this velocity vector equals the other.</summary>
			public bool Equals(Velocity3 other) { return X == other.X && Y == other.Y && Z == other.Z; }

			/// <summary>Get whether this velocity vector equals another velocity vector.</summary>
			public override bool Equals(object other) { if(other is Velocity3) return Equals((Velocity3)other); return base.Equals(other); }

			/// <summary>Get a hash code.</summary>
			public override int GetHashCode() { return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode(); }

			/// <summary>Get a string representation of the vector.</summary>
			public override string ToString() { return "Velocity3(" + X + ", " + Y + ", " + Z + ")"; }

			/// <summary>Test for equality.</summary>
			public static bool operator ==(Velocity3 a, Velocity3 b) { return a.X == b.X && a.Y == b.Y && a.Z == b.Z; }

			/// <summary>Test for inequality.</summary>
			public static bool operator !=(Velocity3 a, Velocity3 b) { return a.X != b.X || a.Y != b.Y || a.Z != b.Z; }

							/// <summary>Create a velocity vector from MetrePerSecond values.</summary>
				public static Velocity3 MetresPerSecond(double x, double y, double z) { return new Velocity3(Velocity.MetresPerSecond(x), Velocity.MetresPerSecond(y), Velocity.MetresPerSecond(z)); }

				/// <summary>Create a velocity vector from MetrePerSecond values.</summary>
				public static Velocity3 MetresPerSecond(Vector3d value) { return new Velocity3(Velocity.MetresPerSecond(value.X), Velocity.MetresPerSecond(value.Y), Velocity.MetresPerSecond(value.Z)); }

				/// <summary>Create a velocity vector from MetrePerSecond values.</summary>
				public static Velocity3 MetresPerSecond(ref Vector3d value) { return new Velocity3(Velocity.MetresPerSecond(value.X), Velocity.MetresPerSecond(value.Y), Velocity.MetresPerSecond(value.Z)); }

				/// <summary>Get a velocity vector in MetrePerSecond values.</summary>
				public Vector3d InMetresPerSecond { get { return new Vector3d(X.InMetresPerSecond, Y.InMetresPerSecond, Z.InMetresPerSecond); } }
					}
	}




