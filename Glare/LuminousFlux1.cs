using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare
{
			/// <summary>A measurement of the total "amount" of visible light emitted by a source. Luminous flux is equal to luminous intensity times a steradian.</summary>
		public struct LuminousFlux : IComparable<LuminousFlux>, IEquatable<LuminousFlux>
		{
			internal const double ToCandelas = (double)(1);

		public static LuminousFlux Candelas(double value) { return new LuminousFlux(value / ToCandelas); }

		public double InCandelas { get { return value * ToCandelas; } }

		public static readonly LuminousFlux Lumen = Candelas(1);
				double value;
		LuminousFlux(double value) { this.value = value; }

		public static readonly LuminousFlux Zero = new LuminousFlux(0);

		public int CompareTo(LuminousFlux other) { return value.CompareTo(other.value); }

		public bool Equals(LuminousFlux other) { return value == other.value; }
		public override bool Equals(object other) { if(other is LuminousFlux) return value == ((LuminousFlux)other).value; return base.Equals(other); }

		public override int GetHashCode() { return value.GetHashCode(); }

		public static bool operator ==(LuminousFlux a, LuminousFlux b) { return a.value == b.value; }
		public static bool operator !=(LuminousFlux a, LuminousFlux b) { return a.value != b.value; }
		public static bool operator >(LuminousFlux a, LuminousFlux b) { return a.value > b.value; }
		public static bool operator >=(LuminousFlux a, LuminousFlux b) { return a.value >= b.value; }
		public static bool operator <(LuminousFlux a, LuminousFlux b) { return a.value < b.value; }
		public static bool operator <=(LuminousFlux a, LuminousFlux b) { return a.value <= b.value; }

		public static LuminousFlux operator +(LuminousFlux value) { return new LuminousFlux(+value.value); }
		public static LuminousFlux operator -(LuminousFlux value) { return new LuminousFlux(-value.value); }

		public static LuminousFlux operator +(LuminousFlux a, LuminousFlux b) { return new LuminousFlux(a.value + b.value); }
		public static LuminousFlux operator -(LuminousFlux a, LuminousFlux b) { return new LuminousFlux(a.value - b.value); }
		public static LuminousFlux operator *(LuminousFlux a, double b) { return new LuminousFlux(a.value * b); }
		public static LuminousFlux operator *(double a, LuminousFlux b) { return new LuminousFlux(a * b.value); }
		public static LuminousFlux operator /(LuminousFlux a, double b) { return new LuminousFlux(a.value / b); }
		public static double operator %(LuminousFlux a, LuminousFlux b) { return a.value % b.value; }
			}
	}



