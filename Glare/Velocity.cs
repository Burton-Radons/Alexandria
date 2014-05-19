using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare
{
			/// <summary>A measurement of length by time, created as <see cref="Length"/> divided by <see cref="TimeSpan"/>.</summary>
		public struct Velocity : IComparable<Velocity>, IEquatable<Velocity>
		{
			internal const double ToSeconds = 1;

			internal const Double ToMetresPerSecond = (Double)(Length.ToMetres / ToSeconds);

		public static Velocity MetresPerSecond(Double value) { return new Velocity(value / ToMetresPerSecond); }

		public Double InMetresPerSecond { get { return value * ToMetresPerSecond; } }

		public static readonly Velocity MetrePerSecond = MetresPerSecond(1);

		public Velocity ClampMetresPerSecond(Double min, Double max) {
			min = min / ToMetresPerSecond;
			max = max / ToMetresPerSecond;
			return new Velocity(value > max ? max : value < min ? min : value);
		}

		public void ClampInPlaceMetresPerSecond(Double min, Double max) {
			if(value > (max = max / ToMetresPerSecond))
				value = max;
			else if(value < (min = min / ToMetresPerSecond))
				value = min;
		}

				public Velocity Absolute { get { return new Velocity(Math.Abs(value)); }}

		public static Velocity Universal(double value) { return MetresPerSecond(value); }
		public double InUniversal { get { return InMetresPerSecond; } }

		Double value;
		Velocity(Double value) { this.value = value; }

		public static readonly Velocity Zero = new Velocity(0);
		public static readonly Velocity PositiveInfinity = new Velocity(Double.PositiveInfinity);
		public static readonly Velocity NegativeInfinity = new Velocity(Double.NegativeInfinity);
		public static readonly Velocity NaN = new Velocity(Double.NaN);

		public Velocity Clamp(Velocity min, Velocity max) { return new Velocity(value > max.value ? max.value : value < min.value ? min.value : value); }

		public void ClampInPlace(Velocity min, Velocity max) { if(value > max.value) value = min.value; else if(value < min.value) value = min.value; }

		public int CompareTo(Velocity other) { return value.CompareTo(other.value); }

		public bool Equals(Velocity other) { return value == other.value; }
		public override bool Equals(object other) { if(other is Velocity) return value == ((Velocity)other).value; return base.Equals(other); }

		public override int GetHashCode() { return value.GetHashCode(); }

		/// <summary>Return the maximum of this value or the passed value.</summary>
		public Velocity Max(Velocity other) { return new Velocity(other.value > value ? other.value : value); }

		/// <summary>Assign this <see cref="Velocity"/> to the maximum of this value or the other one.</summary>
		public void MaxInPlace(Velocity other) { if(other.value > value) value = other.value; }

		/// <summary>Return the minimum of this value or the passed value.</summary>
		public Velocity Min(Velocity other) { return new Velocity(other.value < value ? other.value : value); }

		/// <summary>Assign this <see cref="Velocity"/> to the minimum of this value or the other one.</summary>
		public void MinInPlace(Velocity other) { if(other.value < value) value = other.value; }

		/// <summary>Convert to a string of the form "<value>m·s⁻¹".</summary>
		public override string ToString() { return ToString(null, null); }

		/// <summary>Convert to a string of the form "<value>m·s⁻¹".</summary>
		public string ToString(string format, IFormatProvider provider) { return InMetresPerSecond.ToString(format, provider) + "m·s⁻¹"; }

		public static bool operator ==(Velocity a, Velocity b) { return a.value == b.value; }
		public static bool operator !=(Velocity a, Velocity b) { return a.value != b.value; }
		public static bool operator >(Velocity a, Velocity b) { return a.value > b.value; }
		public static bool operator >=(Velocity a, Velocity b) { return a.value >= b.value; }
		public static bool operator <(Velocity a, Velocity b) { return a.value < b.value; }
		public static bool operator <=(Velocity a, Velocity b) { return a.value <= b.value; }

		public static Velocity operator +(Velocity value) { return new Velocity(+value.value); }
		public static Velocity operator -(Velocity value) { return new Velocity(-value.value); }

		public static Velocity operator +(Velocity a, Velocity b) { return new Velocity(a.value + b.value); }
		public static Velocity operator -(Velocity a, Velocity b) { return new Velocity(a.value - b.value); }
		public static Velocity operator *(Velocity a, Double b) { return new Velocity(a.value * b); }
		public static Velocity operator *(Double a, Velocity b) { return new Velocity(a * b.value); }
		public static Velocity operator /(Velocity a, Double b) { return new Velocity(a.value / b); }
		public static Double operator /(Velocity a, Velocity b) { return a.value / b.value; }
		public static Double operator %(Velocity a, Velocity b) { return a.value % b.value; }
			}

		public struct Velocity3 : IEquatable<Velocity3>
		{
			public Velocity X;
			public Velocity Y;
			public Velocity Z;

			public Velocity3(Velocity x, Velocity y, Velocity z) { X = x; Y = y; Z = z; }

			public static readonly Velocity3 Zero = MetresPerSecond(0, 0, 0);

			public bool Equals(Velocity3 other) { return X == other.X && Y == other.Y && Z == other.Z; }

			public override bool Equals(object other) { if(other is Velocity3) return Equals((Velocity3)other); return base.Equals(other); }

			public override int GetHashCode() { return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode(); }

			public override string ToString() { return "Velocity3(" + X + ", " + Y + ", " + Z + ")"; }

			public static bool operator ==(Velocity3 a, Velocity3 b) { return a.X == b.X && a.Y == b.Y && a.Z == b.Z; }
			public static bool operator !=(Velocity3 a, Velocity3 b) { return a.X != b.X || a.Y != b.Y || a.Z != b.Z; }

							public static Velocity3 MetresPerSecond(double x, double y, double z) { return new Velocity3(Velocity.MetresPerSecond(x), Velocity.MetresPerSecond(y), Velocity.MetresPerSecond(z)); }
				public static Velocity3 MetresPerSecond(Vector3d value) { return new Velocity3(Velocity.MetresPerSecond(value.X), Velocity.MetresPerSecond(value.Y), Velocity.MetresPerSecond(value.Z)); }
				public static Velocity3 MetresPerSecond(ref Vector3d value) { return new Velocity3(Velocity.MetresPerSecond(value.X), Velocity.MetresPerSecond(value.Y), Velocity.MetresPerSecond(value.Z)); }

				public Vector3d InMetresPerSecond { get { return new Vector3d(X.InMetresPerSecond, Y.InMetresPerSecond, Z.InMetresPerSecond); } }
					}
	}






