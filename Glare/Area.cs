using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare
{
			/// <summary>An area measurement.</summary>
		public struct Area : IComparable<Area>, IEquatable<Area>
		{
			internal const double ToSquareMetres = (double)(Length.ToMetres * Length.ToMetres);

		public static Area SquareMetres(double value) { return new Area(value / ToSquareMetres); }

		public double InSquareMetres { get { return value * ToSquareMetres; } }

		public static readonly Area SquareMetre = SquareMetres(1);

		public Area ClampSquareMetres(double min, double max)
		{
			min = min / ToSquareMetres;
			max = max / ToSquareMetres;
			return new Area(value > max ? max : value < min ? min : value);
		}

		public void ClampInPlaceSquareMetres(double min, double max)
		{
			if(value > (max = max / ToSquareMetres))
				value = max;
			else if(value < (min = min / ToSquareMetres))
				value = min;
		}

				public static Area Universal(double value) { return SquareMetres(value); }
		public double InUniversal { get { return InSquareMetres; } }

		double value;
		Area(double value) { this.value = value; }

		public static readonly Area Zero = new Area(0);
		public static readonly Area PositiveInfinity = new Area(double.PositiveInfinity);
		public static readonly Area NegativeInfinity = new Area(double.NegativeInfinity);
		public static readonly Area NaN = new Area(double.NaN);

		public Area Clamp(Area min, Area max) { return new Area(value > max.value ? max.value : value < min.value ? min.value : value); }

		public void ClampInPlace(Area min, Area max) { if(value > max.value) value = min.value; else if(value < min.value) value = min.value; }

		public int CompareTo(Area other) { return value.CompareTo(other.value); }

		public bool Equals(Area other) { return value == other.value; }
		public override bool Equals(object other) { if(other is Area) return value == ((Area)other).value; return base.Equals(other); }

		public override int GetHashCode() { return value.GetHashCode(); }

		/// <summary>Return the maximum of this value or the passed value.</summary>
		public Area Max(Area other) { return new Area(other.value > value ? other.value : value); }

		/// <summary>Assign this <see cref="Area"/> to the maximum of this value or the other one.</summary>
		public void MaxInPlace(Area other) { if(other.value > value) value = other.value; }

		/// <summary>Return the minimum of this value or the passed value.</summary>
		public Area Min(Area other) { return new Area(other.value < value ? other.value : value); }

		/// <summary>Assign this <see cref="Area"/> to the minimum of this value or the other one.</summary>
		public void MinInPlace(Area other) { if(other.value < value) value = other.value; }

		/// <summary>Convert to a string of the form "<value>m²".</summary>
		public override string ToString() { return ToString(null, null); }

		/// <summary>Convert to a string of the form "<value>m²".</summary>
		public string ToString(string format, IFormatProvider provider) { return value.ToString(format, provider) + "m²"; }

		public static bool operator ==(Area a, Area b) { return a.value == b.value; }
		public static bool operator !=(Area a, Area b) { return a.value != b.value; }
		public static bool operator >(Area a, Area b) { return a.value > b.value; }
		public static bool operator >=(Area a, Area b) { return a.value >= b.value; }
		public static bool operator <(Area a, Area b) { return a.value < b.value; }
		public static bool operator <=(Area a, Area b) { return a.value <= b.value; }

		public static Area operator +(Area value) { return new Area(+value.value); }
		public static Area operator -(Area value) { return new Area(-value.value); }

		public static Area operator +(Area a, Area b) { return new Area(a.value + b.value); }
		public static Area operator -(Area a, Area b) { return new Area(a.value - b.value); }
		public static Area operator *(Area a, double b) { return new Area(a.value * b); }
		public static Area operator *(double a, Area b) { return new Area(a * b.value); }
		public static Area operator /(Area a, double b) { return new Area(a.value / b); }
		public static double operator /(Area a, Area b) { return a.value / b.value; }
		public static double operator %(Area a, Area b) { return a.value % b.value; }
	
			public static Volume operator *(Area a, Length b) { return Volume.CubicMetres(a.InSquareMetres * b.InMetres); }
			public static Volume operator *(Length a, Area b) { return Volume.CubicMetres(a.InMetres * b.InSquareMetres); }
			public static Length operator /(Area a, Length b) { return Length.Metres(a.InSquareMetres / b.InMetres); }
		}
	}


