using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare
{
			/// <summary>A measurement of luminous intensity.</summary>
		public struct LuminousIntensity : IComparable<LuminousIntensity>, IEquatable<LuminousIntensity>
		{
			internal const double ToCandelas = (double)(1);

		public static LuminousIntensity Candelas(double value) { return new LuminousIntensity(value / ToCandelas); }

		public double InCandelas { get { return value * ToCandelas; } }

		public static readonly LuminousIntensity Candela = Candelas(1);

		public LuminousIntensity ClampCandelas(double min, double max)
		{
			min = min / ToCandelas;
			max = max / ToCandelas;
			return new LuminousIntensity(value > max ? max : value < min ? min : value);
		}

		public void ClampInPlaceCandelas(double min, double max)
		{
			if(value > (max = max / ToCandelas))
				value = max;
			else if(value < (min = min / ToCandelas))
				value = min;
		}

				double value;
		LuminousIntensity(double value) { this.value = value; }

		public static readonly LuminousIntensity Zero = new LuminousIntensity(0);
		public static readonly LuminousIntensity PositiveInfinity = new LuminousIntensity(double.PositiveInfinity);
		public static readonly LuminousIntensity NegativeInfinity = new LuminousIntensity(double.NegativeInfinity);
		public static readonly LuminousIntensity NaN = new LuminousIntensity(double.NaN);

		public LuminousIntensity Clamp(LuminousIntensity min, LuminousIntensity max) { return new LuminousIntensity(value > max.value ? max.value : value < min.value ? min.value : value); }

		public void ClampInPlace(LuminousIntensity min, LuminousIntensity max) { if(value > max.value) value = min.value; else if(value < min.value) value = min.value; }

		public int CompareTo(LuminousIntensity other) { return value.CompareTo(other.value); }

		public bool Equals(LuminousIntensity other) { return value == other.value; }
		public override bool Equals(object other) { if(other is LuminousIntensity) return value == ((LuminousIntensity)other).value; return base.Equals(other); }

		public override int GetHashCode() { return value.GetHashCode(); }

		/// <summary>Return the maximum of this value or the passed value.</summary>
		public LuminousIntensity Max(LuminousIntensity other) { return new LuminousIntensity(other.value > value ? other.value : value); }

		/// <summary>Assign this <see cref="LuminousIntensity"/> to the maximum of this value or the other one.</summary>
		public void MaxInPlace(LuminousIntensity other) { if(other.value > value) value = other.value; }

		/// <summary>Return the minimum of this value or the passed value.</summary>
		public LuminousIntensity Min(LuminousIntensity other) { return new LuminousIntensity(other.value < value ? other.value : value); }

		/// <summary>Assign this <see cref="LuminousIntensity"/> to the minimum of this value or the other one.</summary>
		public void MinInPlace(LuminousIntensity other) { if(other.value < value) value = other.value; }

		public static bool operator ==(LuminousIntensity a, LuminousIntensity b) { return a.value == b.value; }
		public static bool operator !=(LuminousIntensity a, LuminousIntensity b) { return a.value != b.value; }
		public static bool operator >(LuminousIntensity a, LuminousIntensity b) { return a.value > b.value; }
		public static bool operator >=(LuminousIntensity a, LuminousIntensity b) { return a.value >= b.value; }
		public static bool operator <(LuminousIntensity a, LuminousIntensity b) { return a.value < b.value; }
		public static bool operator <=(LuminousIntensity a, LuminousIntensity b) { return a.value <= b.value; }

		public static LuminousIntensity operator +(LuminousIntensity value) { return new LuminousIntensity(+value.value); }
		public static LuminousIntensity operator -(LuminousIntensity value) { return new LuminousIntensity(-value.value); }

		public static LuminousIntensity operator +(LuminousIntensity a, LuminousIntensity b) { return new LuminousIntensity(a.value + b.value); }
		public static LuminousIntensity operator -(LuminousIntensity a, LuminousIntensity b) { return new LuminousIntensity(a.value - b.value); }
		public static LuminousIntensity operator *(LuminousIntensity a, double b) { return new LuminousIntensity(a.value * b); }
		public static LuminousIntensity operator *(double a, LuminousIntensity b) { return new LuminousIntensity(a * b.value); }
		public static LuminousIntensity operator /(LuminousIntensity a, double b) { return new LuminousIntensity(a.value / b); }
		public static double operator %(LuminousIntensity a, LuminousIntensity b) { return a.value % b.value; }
			}

	
/*

*/
}



