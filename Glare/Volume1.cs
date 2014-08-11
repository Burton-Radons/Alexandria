using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare {
			/// <summary>A volume measurement.</summary>
		public struct Volume : IComparable<Volume>, IEquatable<Volume> {
			/// <summary>This is the constant value for conversion from the CubicMetre unit to the stored value when divided, or back to CubicMetre when multiplied.</summary>
		internal const Double ToCubicMetres = (Double)(Area.ToSquareMetres * Length.ToMetres);

		/// <summary>Create a <see cref="Volume"/> by providing a CubicMetre value.</summary>
		public static Volume CubicMetres(Double value) { return new Volume(value / ToCubicMetres); }

		/// <summary>Get the <see cref="Volume"/> value in the CubicMetre unit.</summary>
		public Double InCubicMetres { get { return value * ToCubicMetres; } }

		/// <summary>Clamp this <see cref="Volume"/> value to the CubicMetre range, returning the clamped value.</summary>
		public Volume ClampCubicMetres(Double min, Double max) {
			min = min / ToCubicMetres;
			max = max / ToCubicMetres;
			return new Volume(value > max ? max : value < min ? min : value);
		}

		/// <summary>Clamp this <see cref="Volume"/> value to the CubicMetre range, changing this value, then return it.</summary>
		public Volume ClampInPlaceCubicMetres(Double min, Double max) {
			if(value > (max = max / ToCubicMetres))
				value = max;
			else if(value < (min = min / ToCubicMetres))
				value = min;
			return this;
		}

		/// <summary>Get the absolute value of this <see cref="Volume"/>.</summary>
		public Volume AsUnsigned { get { return new Volume(Math.Abs(value)); }}

		/// <summary>Create a <see cref="Volume"/> by providing the universal unit, which is CubicMetres.</summary>
		public static Volume Universal(double value) { return CubicMetres(value); }

		/// <summary>Get this <see cref="Volume"/> in the universal unit, which is CubicMetres.</summary>
		public double InUniversal { get { return InCubicMetres; } }

		/// <summary>The coded value.</summary>
		Double value;

		/// <summary>Private constructor for the <see cref="Volume"/>; the real constructors are the static unit methods.</summary>
		Volume(Double value) { this.value = value; }

		/// <summary>Get the zero value for <see cref="Volume"/>.</summary>
		public static readonly Volume Zero = new Volume(0);

		/// <summary>Get the positive infinity value for <see cref="Volume"/>.</summary>
		public static readonly Volume PositiveInfinity = new Volume(Double.PositiveInfinity);

		/// <summary>Get the negative infinity value for <see cref="Volume"/>.</summary>
		public static readonly Volume NegativeInfinity = new Volume(Double.NegativeInfinity);

		/// <summary>Get the not-a-number value for <see cref="Volume"/>.</summary>
		public static readonly Volume NaN = new Volume(Double.NaN);

		/// <summary>Clamp this <see cref="Volume"/> to the provided range, returning the result.</summary>
		public Volume Clamp(Volume min, Volume max) { return new Volume(value > max.value ? max.value : value < min.value ? min.value : value); }

		/// <summary>Clamp this <see cref="Volume"/> to the provided range, storing the result in this value and returning it.</summary>
		public Volume ClampInPlace(Volume min, Volume max) {
			if(value > max.value)
				value = min.value;
			else if(value < min.value)
				value = min.value;
			return this;
		}

		/// <summary>Compare this <see cref="Volume"/> to the other value, returning the relative order.</summary>.
		public int CompareTo(Volume other) { return value.CompareTo(other.value); }

		/// <summary>Compare this <see cref="Volume"/> to the other value, returning whether they are equal.</summary>
		public bool Equals(Volume other) { return value == other.value; }

		/// <summary>If the other object is <see cref="Volume"/>, determine equality and return the result; otherwise return <c>null</c>.</summary>
		public override bool Equals(object other) {
			if(other is Volume)
				return value == ((Volume)other).value;
			return base.Equals(other);
		}

		/// <summary>Get a hash code from the value.</summary>
		public override int GetHashCode() { return value.GetHashCode(); }

		/// <summary>Return the maximum of this value or the passed value.</summary>
		public Volume Max(Volume other) { return new Volume(other.value > value ? other.value : value); }

		/// <summary>Assign this <see cref="Volume"/> to the maximum of this value or the other one.</summary>
		public void MaxInPlace(Volume other) { if(other.value > value) value = other.value; }

		/// <summary>Return the minimum of this value or the passed value.</summary>
		public Volume Min(Volume other) { return new Volume(other.value < value ? other.value : value); }

		/// <summary>Assign this <see cref="Volume"/> to the minimum of this value or the other one.</summary>
		public void MinInPlace(Volume other) { if(other.value < value) value = other.value; }

		/// <summary>Convert to a string of the form "&lt;value&gt;m³".</summary>
		public override string ToString() { return ToString(null, null); }

		/// <summary>Convert to a string of the form "&lt;value&gt;m³".</summary>
		public string ToString(string format, IFormatProvider provider) { return InCubicMetres.ToString(format, provider) + "m³"; }

		/// <summary>Test for equality between the <see cref="Volume"/> values.</summary>
		public static bool operator ==(Volume a, Volume b) { return a.value == b.value; }

		/// <summary>Test for inequality between the <see cref="Volume"/> values.</summary>
		public static bool operator !=(Volume a, Volume b) { return a.value != b.value; }

		/// <summary>Compare the <see cref="Volume"/> values.</summary>
		public static bool operator >(Volume a, Volume b) { return a.value > b.value; }

		/// <summary>Compare the <see cref="Volume"/> values.</summary>
		public static bool operator >=(Volume a, Volume b) { return a.value >= b.value; }

		/// <summary>Compare the <see cref="Volume"/> values.</summary>
		public static bool operator <(Volume a, Volume b) { return a.value < b.value; }

		/// <summary>Compare the <see cref="Volume"/> values.</summary>
		public static bool operator <=(Volume a, Volume b) { return a.value <= b.value; }

		/// <summary>Return the positive of the <see cref="Volume"/> value, which is itself.</summary>
		public static Volume operator +(Volume value) { return new Volume(+value.value); }

		/// <summary>Return the negative of the <see cref="Volume"/> value.</summary>
		public static Volume operator -(Volume value) { return new Volume(-value.value); }

		/// <summary>Add the <see cref="Volume"/> values.</summary>
		public static Volume operator +(Volume a, Volume b) { return new Volume(a.value + b.value); }

		/// <summary>Subtract the <see cref="Volume"/> values.</summary>
		public static Volume operator -(Volume a, Volume b) { return new Volume(a.value - b.value); }

		/// <summary>Multiply the <see cref="Volume"/> value to a scalar.</summary>
		public static Volume operator *(Volume a, Double b) { return new Volume(a.value * b); }

		/// <summary>Multiply the <see cref="Volume"/> value to a scalar.</summary>
		public static Volume operator *(Double a, Volume b) { return new Volume(a * b.value); }

		/// <summary>Divide the <see cref="Volume"/> value by a scalar.</summary>
		public static Volume operator /(Volume a, Double b) { return new Volume(a.value / b); }

		/// <summary>Divide the <see cref="Volume"/> values, producing the scalar magnitude difference between them.</summary>
		public static Double operator /(Volume a, Volume b) { return a.value / b.value; }

		/// <summary>Modulo the <see cref="Volume"/> values, producing the scalar result.</summary>
		public static Double operator %(Volume a, Volume b) { return a.value % b.value; }
	
			/// <summary>Divide a <see cref="Volume"/> value by a <see cref="Length"/>, producing an <see cref="Area"/>.</summary>
			public static Area operator /(Volume a, Length b) { return Area.SquareMetres(a.InCubicMetres / b.InMetres); }

			/// <summary>Divide a <see cref="Volume"/> value by a <see cref="Area"/>, producing a <see cref="Length"/>.</summary>
			public static Length operator /(Volume a, Area b) { return Length.Metres(a.InCubicMetres / b.InSquareMetres); }
		}
	}



