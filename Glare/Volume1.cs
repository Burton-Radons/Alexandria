using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare
{
			/// <summary>A volume measurement.</summary>
		public struct Volume : IComparable<Volume>, IEquatable<Volume>
		{
			internal const double ToCubicMetres = (double)(Area.ToSquareMetres * Length.ToMetres);

		public static Volume CubicMetres(double value) { return new Volume(value / ToCubicMetres); }

		public double InCubicMetres { get { return value * ToCubicMetres; } }

		public static readonly Volume CubicMetre = CubicMetres(1);
		
	double value;
	Volume(double value) { this.value = value; }

	public static readonly Volume Zero = new Volume(0);

	public int CompareTo(Volume other) { return value.CompareTo(other.value); }

	public bool Equals(Volume other) { return value == other.value; }
	public override bool Equals(object other) { if(other is Volume) return value == ((Volume)other).value; return base.Equals(other); }

	public override int GetHashCode() { return value.GetHashCode(); }

	public static bool operator ==(Volume a, Volume b) { return a.value == b.value; }
	public static bool operator !=(Volume a, Volume b) { return a.value != b.value; }
	public static bool operator >(Volume a, Volume b) { return a.value > b.value; }
	public static bool operator >=(Volume a, Volume b) { return a.value >= b.value; }
	public static bool operator <(Volume a, Volume b) { return a.value < b.value; }
	public static bool operator <=(Volume a, Volume b) { return a.value <= b.value; }

	public static Volume operator +(Volume value) { return new Volume(+value.value); }
	public static Volume operator -(Volume value) { return new Volume(-value.value); }

	public static Volume operator +(Volume a, Volume b) { return new Volume(a.value + b.value); }
	public static Volume operator -(Volume a, Volume b) { return new Volume(a.value - b.value); }
	public static Volume operator *(Volume a, double b) { return new Volume(a.value * b); }
	public static Volume operator *(double a, Volume b) { return new Volume(a * b.value); }
	public static Volume operator /(Volume a, double b) { return new Volume(a.value / b); }
	public static double operator %(Volume a, Volume b) { return a.value % b.value; }

	
			public static Area operator /(Volume a, Length b) { return Area.SquareMetres(a.InCubicMetres / b.InMetres); }
			public static Length operator /(Volume a, Area b) { return Length.Metres(a.InCubicMetres / b.InSquareMetres); }
		}
	}



