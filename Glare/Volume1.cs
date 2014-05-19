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
			internal const Double ToCubicMetres = (Double)(Area.ToSquareMetres * Length.ToMetres);

		public static Volume CubicMetres(Double value) { return new Volume(value / ToCubicMetres); }

		public Double InCubicMetres { get { return value * ToCubicMetres; } }

		public static readonly Volume CubicMetre = CubicMetres(1);

		public Volume ClampCubicMetres(Double min, Double max) {
			min = min / ToCubicMetres;
			max = max / ToCubicMetres;
			return new Volume(value > max ? max : value < min ? min : value);
		}

		public void ClampInPlaceCubicMetres(Double min, Double max) {
			if(value > (max = max / ToCubicMetres))
				value = max;
			else if(value < (min = min / ToCubicMetres))
				value = min;
		}

				public Volume Absolute { get { return new Volume(Math.Abs(value)); }}

		public static Volume Universal(double value) { return CubicMetres(value); }
		public double InUniversal { get { return InCubicMetres; } }

		Double value;
		Volume(Double value) { this.value = value; }

		public static readonly Volume Zero = new Volume(0);
		public static readonly Volume PositiveInfinity = new Volume(Double.PositiveInfinity);
		public static readonly Volume NegativeInfinity = new Volume(Double.NegativeInfinity);
		public static readonly Volume NaN = new Volume(Double.NaN);

		public Volume Clamp(Volume min, Volume max) { return new Volume(value > max.value ? max.value : value < min.value ? min.value : value); }

		public void ClampInPlace(Volume min, Volume max) { if(value > max.value) value = min.value; else if(value < min.value) value = min.value; }

		public int CompareTo(Volume other) { return value.CompareTo(other.value); }

		public bool Equals(Volume other) { return value == other.value; }
		public override bool Equals(object other) { if(other is Volume) return value == ((Volume)other).value; return base.Equals(other); }

		public override int GetHashCode() { return value.GetHashCode(); }

		/// <summary>Return the maximum of this value or the passed value.</summary>
		public Volume Max(Volume other) { return new Volume(other.value > value ? other.value : value); }

		/// <summary>Assign this <see cref="Volume"/> to the maximum of this value or the other one.</summary>
		public void MaxInPlace(Volume other) { if(other.value > value) value = other.value; }

		/// <summary>Return the minimum of this value or the passed value.</summary>
		public Volume Min(Volume other) { return new Volume(other.value < value ? other.value : value); }

		/// <summary>Assign this <see cref="Volume"/> to the minimum of this value or the other one.</summary>
		public void MinInPlace(Volume other) { if(other.value < value) value = other.value; }

		/// <summary>Convert to a string of the form "<value>m³".</summary>
		public override string ToString() { return ToString(null, null); }

		/// <summary>Convert to a string of the form "<value>m³".</summary>
		public string ToString(string format, IFormatProvider provider) { return InCubicMetres.ToString(format, provider) + "m³"; }

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
		public static Volume operator *(Volume a, Double b) { return new Volume(a.value * b); }
		public static Volume operator *(Double a, Volume b) { return new Volume(a * b.value); }
		public static Volume operator /(Volume a, Double b) { return new Volume(a.value / b); }
		public static Double operator /(Volume a, Volume b) { return a.value / b.value; }
		public static Double operator %(Volume a, Volume b) { return a.value % b.value; }
	
			public static Area operator /(Volume a, Length b) { return Area.SquareMetres(a.InCubicMetres / b.InMetres); }
			public static Length operator /(Volume a, Area b) { return Length.Metres(a.InCubicMetres / b.InSquareMetres); }
		}
	}





