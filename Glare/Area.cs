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
			/// <summary>This is the constant value for conversion from the SquareMetre unit to the stored value when divided, or back to SquareMetre when multiplied.</summary>
		internal const Double ToSquareMetres = (Double)(Length.ToMetres * Length.ToMetres);

		/// <summary>Create a <see cref="Area"/> by providing a SquareMetre value.</summary>
		public static Area SquareMetres(Double value) { return new Area(value / ToSquareMetres); }

		/// <summary>Get the <see cref="Area"/> value in the SquareMetre unit.</summary>
		public Double InSquareMetres { get { return value * ToSquareMetres; } }

		/// <summary>Clamp this <see cref="Area"/> value to the SquareMetre range, returning the clamped value.</summary>
		public Area ClampSquareMetres(Double min, Double max) {
			min = min / ToSquareMetres;
			max = max / ToSquareMetres;
			return new Area(value > max ? max : value < min ? min : value);
		}

		/// <summary>Clamp this <see cref="Area"/> value to the SquareMetre range, changing this value, then return it.</summary>
		public Area ClampInPlaceSquareMetres(Double min, Double max) {
			if(value > (max = max / ToSquareMetres))
				value = max;
			else if(value < (min = min / ToSquareMetres))
				value = min;
			return this;
		}

		/// <summary>Get the absolute value of this <see cref="Area"/>.</summary>
		public Area AsUnsigned { get { return new Area(Math.Abs(value)); }}

		/// <summary>Create a <see cref="Area"/> by providing the universal unit, which is SquareMetres.</summary>
		public static Area Universal(double value) { return SquareMetres(value); }

		/// <summary>Get this <see cref="Area"/> in the universal unit, which is SquareMetres.</summary>
		public double InUniversal { get { return InSquareMetres; } }

		/// <summary>The coded value.</summary>
		Double value;

		/// <summary>Private constructor for the <see cref="Area"/>; the real constructors are the static unit methods.</summary>
		Area(Double value) { this.value = value; }

		/// <summary>Get the zero value for <see cref="Area"/>.</summary>
		public static readonly Area Zero = new Area(0);

		/// <summary>Get the positive infinity value for <see cref="Area"/>.</summary>
		public static readonly Area PositiveInfinity = new Area(Double.PositiveInfinity);

		/// <summary>Get the negative infinity value for <see cref="Area"/>.</summary>
		public static readonly Area NegativeInfinity = new Area(Double.NegativeInfinity);

		/// <summary>Get the not-a-number value for <see cref="Area"/>.</summary>
		public static readonly Area NaN = new Area(Double.NaN);

		/// <summary>Clamp this <see cref="Area"/> to the provided range, returning the result.</summary>
		public Area Clamp(Area min, Area max) { return new Area(value > max.value ? max.value : value < min.value ? min.value : value); }

		/// <summary>Clamp this <see cref="Area"/> to the provided range, storing the result in this value and returning it.</summary>
		public Area ClampInPlace(Area min, Area max) {
			if(value > max.value)
				value = min.value;
			else if(value < min.value)
				value = min.value;
			return this;
		}

		/// <summary>Compare this <see cref="Area"/> to the other value, returning the relative order.</summary>.
		public int CompareTo(Area other) { return value.CompareTo(other.value); }

		/// <summary>Compare this <see cref="Area"/> to the other value, returning whether they are equal.</summary>
		public bool Equals(Area other) { return value == other.value; }

		/// <summary>If the other object is <see cref="Area"/>, determine equality and return the result; otherwise return <c>null</c>.</summary>
		public override bool Equals(object other) {
			if(other is Area)
				return value == ((Area)other).value;
			return base.Equals(other);
		}

		/// <summary>Get a hash code from the value.</summary>
		public override int GetHashCode() { return value.GetHashCode(); }

		/// <summary>Return the maximum of this value or the passed value.</summary>
		public Area Max(Area other) { return new Area(other.value > value ? other.value : value); }

		/// <summary>Assign this <see cref="Area"/> to the maximum of this value or the other one.</summary>
		public void MaxInPlace(Area other) { if(other.value > value) value = other.value; }

		/// <summary>Return the minimum of this value or the passed value.</summary>
		public Area Min(Area other) { return new Area(other.value < value ? other.value : value); }

		/// <summary>Assign this <see cref="Area"/> to the minimum of this value or the other one.</summary>
		public void MinInPlace(Area other) { if(other.value < value) value = other.value; }

		/// <summary>Convert to a string of the form "&lt;value&gt;m²".</summary>
		public override string ToString() { return ToString(null, null); }

		/// <summary>Convert to a string of the form "&lt;value&gt;m²".</summary>
		public string ToString(string format, IFormatProvider provider) { return InSquareMetres.ToString(format, provider) + "m²"; }

		/// <summary>Test for equality between the <see cref="Area"/> values.</summary>
		public static bool operator ==(Area a, Area b) { return a.value == b.value; }

		/// <summary>Test for inequality between the <see cref="Area"/> values.</summary>
		public static bool operator !=(Area a, Area b) { return a.value != b.value; }

		/// <summary>Compare the <see cref="Area"/> values.</summary>
		public static bool operator >(Area a, Area b) { return a.value > b.value; }

		/// <summary>Compare the <see cref="Area"/> values.</summary>
		public static bool operator >=(Area a, Area b) { return a.value >= b.value; }

		/// <summary>Compare the <see cref="Area"/> values.</summary>
		public static bool operator <(Area a, Area b) { return a.value < b.value; }

		/// <summary>Compare the <see cref="Area"/> values.</summary>
		public static bool operator <=(Area a, Area b) { return a.value <= b.value; }

		/// <summary>Return the positive of the <see cref="Area"/> value, which is itself.</summary>
		public static Area operator +(Area value) { return new Area(+value.value); }

		/// <summary>Return the negative of the <see cref="Area"/> value.</summary>
		public static Area operator -(Area value) { return new Area(-value.value); }

		/// <summary>Add the <see cref="Area"/> values.</summary>
		public static Area operator +(Area a, Area b) { return new Area(a.value + b.value); }

		/// <summary>Subtract the <see cref="Area"/> values.</summary>
		public static Area operator -(Area a, Area b) { return new Area(a.value - b.value); }

		/// <summary>Multiply the <see cref="Area"/> value to a scalar.</summary>
		public static Area operator *(Area a, Double b) { return new Area(a.value * b); }

		/// <summary>Multiply the <see cref="Area"/> value to a scalar.</summary>
		public static Area operator *(Double a, Area b) { return new Area(a * b.value); }

		/// <summary>Divide the <see cref="Area"/> value by a scalar.</summary>
		public static Area operator /(Area a, Double b) { return new Area(a.value / b); }

		/// <summary>Divide the <see cref="Area"/> values, producing the scalar magnitude difference between them.</summary>
		public static Double operator /(Area a, Area b) { return a.value / b.value; }

		/// <summary>Modulo the <see cref="Area"/> values, producing the scalar result.</summary>
		public static Double operator %(Area a, Area b) { return a.value % b.value; }
	
			/// <summary>Multiply the <see cref="Area"/> value with the <see cref="Length"/>, producing a <see cref="Volume"/> value.</summary>
			public static Volume operator *(Area a, Length b) { return Volume.CubicMetres(a.InSquareMetres * b.InMetres); }

			/// <summary>Multiply the <see cref="Area"/> value with the <see cref="Length"/>, producing a <see cref="Volume"/> value.</summary>
			public static Volume operator *(Length a, Area b) { return Volume.CubicMetres(a.InMetres * b.InSquareMetres); }

			/// <summary>Divide the <see cref="Area"/> value with the <see cref="Length"/>, producing a <see cref="Area"/> result.</summary>
			public static Length operator /(Area a, Length b) { return Length.Metres(a.InSquareMetres / b.InMetres); }
		}
	}


