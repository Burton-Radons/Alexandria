using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare
{
		/// <summary>A distance value.</summary>
	public struct Length : IComparable<Length>, IEquatable<Length>, IFormattable
	{
		/// <summary>This is the constant value for conversion from the Centimetre unit to the stored value when divided, or back to Centimetre when multiplied.</summary>
		internal const Double ToCentimetres = (Double)(ToMetres * 100);

		/// <summary>Create a <see cref="Length"/> by providing a Centimetre value.</summary>
		public static Length Centimetres(Double value) { return new Length(value / ToCentimetres); }

		/// <summary>Get the <see cref="Length"/> value in the Centimetre unit.</summary>
		public Double InCentimetres { get { return value * ToCentimetres; } }

		/// <summary>Clamp this <see cref="Length"/> value to the Centimetre range, returning the clamped value.</summary>
		public Length ClampCentimetres(Double min, Double max) {
			min = min / ToCentimetres;
			max = max / ToCentimetres;
			return new Length(value > max ? max : value < min ? min : value);
		}

		/// <summary>Clamp this <see cref="Length"/> value to the Centimetre range, changing this value, then return it.</summary>
		public Length ClampInPlaceCentimetres(Double min, Double max) {
			if(value > (max = max / ToCentimetres))
				value = max;
			else if(value < (min = min / ToCentimetres))
				value = min;
			return this;
		}

		/// <summary>This is the constant value for conversion from the Foot unit to the stored value when divided, or back to Foot when multiplied.</summary>
		internal const Double ToFeet = (Double)(ToYards * 3);

		/// <summary>Create a <see cref="Length"/> by providing a Foot value.</summary>
		public static Length Feet(Double value) { return new Length(value / ToFeet); }

		/// <summary>Get the <see cref="Length"/> value in the Foot unit.</summary>
		public Double InFeet { get { return value * ToFeet; } }

		/// <summary>Clamp this <see cref="Length"/> value to the Foot range, returning the clamped value.</summary>
		public Length ClampFeet(Double min, Double max) {
			min = min / ToFeet;
			max = max / ToFeet;
			return new Length(value > max ? max : value < min ? min : value);
		}

		/// <summary>Clamp this <see cref="Length"/> value to the Foot range, changing this value, then return it.</summary>
		public Length ClampInPlaceFeet(Double min, Double max) {
			if(value > (max = max / ToFeet))
				value = max;
			else if(value < (min = min / ToFeet))
				value = min;
			return this;
		}

		/// <summary>This is the constant value for conversion from the Inch unit to the stored value when divided, or back to Inch when multiplied.</summary>
		internal const Double ToInches = (Double)(ToFeet * 12);

		/// <summary>Create a <see cref="Length"/> by providing a Inch value.</summary>
		public static Length Inches(Double value) { return new Length(value / ToInches); }

		/// <summary>Get the <see cref="Length"/> value in the Inch unit.</summary>
		public Double InInches { get { return value * ToInches; } }

		/// <summary>Clamp this <see cref="Length"/> value to the Inch range, returning the clamped value.</summary>
		public Length ClampInches(Double min, Double max) {
			min = min / ToInches;
			max = max / ToInches;
			return new Length(value > max ? max : value < min ? min : value);
		}

		/// <summary>Clamp this <see cref="Length"/> value to the Inch range, changing this value, then return it.</summary>
		public Length ClampInPlaceInches(Double min, Double max) {
			if(value > (max = max / ToInches))
				value = max;
			else if(value < (min = min / ToInches))
				value = min;
			return this;
		}

		/// <summary>This is the constant value for conversion from the Kilometre unit to the stored value when divided, or back to Kilometre when multiplied.</summary>
		internal const Double ToKilometres = (Double)(ToMetres / 10e3);

		/// <summary>Create a <see cref="Length"/> by providing a Kilometre value.</summary>
		public static Length Kilometres(Double value) { return new Length(value / ToKilometres); }

		/// <summary>Get the <see cref="Length"/> value in the Kilometre unit.</summary>
		public Double InKilometres { get { return value * ToKilometres; } }

		/// <summary>Clamp this <see cref="Length"/> value to the Kilometre range, returning the clamped value.</summary>
		public Length ClampKilometres(Double min, Double max) {
			min = min / ToKilometres;
			max = max / ToKilometres;
			return new Length(value > max ? max : value < min ? min : value);
		}

		/// <summary>Clamp this <see cref="Length"/> value to the Kilometre range, changing this value, then return it.</summary>
		public Length ClampInPlaceKilometres(Double min, Double max) {
			if(value > (max = max / ToKilometres))
				value = max;
			else if(value < (min = min / ToKilometres))
				value = min;
			return this;
		}

		/// <summary>This is the constant value for conversion from the Megametre unit to the stored value when divided, or back to Megametre when multiplied.</summary>
		internal const Double ToMegametres = (Double)(ToMetres / 10e6);

		/// <summary>Create a <see cref="Length"/> by providing a Megametre value.</summary>
		public static Length Megametres(Double value) { return new Length(value / ToMegametres); }

		/// <summary>Get the <see cref="Length"/> value in the Megametre unit.</summary>
		public Double InMegametres { get { return value * ToMegametres; } }

		/// <summary>Clamp this <see cref="Length"/> value to the Megametre range, returning the clamped value.</summary>
		public Length ClampMegametres(Double min, Double max) {
			min = min / ToMegametres;
			max = max / ToMegametres;
			return new Length(value > max ? max : value < min ? min : value);
		}

		/// <summary>Clamp this <see cref="Length"/> value to the Megametre range, changing this value, then return it.</summary>
		public Length ClampInPlaceMegametres(Double min, Double max) {
			if(value > (max = max / ToMegametres))
				value = max;
			else if(value < (min = min / ToMegametres))
				value = min;
			return this;
		}

		/// <summary>This is the constant value for conversion from the Metre unit to the stored value when divided, or back to Metre when multiplied.</summary>
		internal const Double ToMetres = (Double)(1);

		/// <summary>Create a <see cref="Length"/> by providing a Metre value.</summary>
		public static Length Metres(Double value) { return new Length(value / ToMetres); }

		/// <summary>Get the <see cref="Length"/> value in the Metre unit.</summary>
		public Double InMetres { get { return value * ToMetres; } }

		/// <summary>Clamp this <see cref="Length"/> value to the Metre range, returning the clamped value.</summary>
		public Length ClampMetres(Double min, Double max) {
			min = min / ToMetres;
			max = max / ToMetres;
			return new Length(value > max ? max : value < min ? min : value);
		}

		/// <summary>Clamp this <see cref="Length"/> value to the Metre range, changing this value, then return it.</summary>
		public Length ClampInPlaceMetres(Double min, Double max) {
			if(value > (max = max / ToMetres))
				value = max;
			else if(value < (min = min / ToMetres))
				value = min;
			return this;
		}

		/// <summary>This is the constant value for conversion from the Micrometre unit to the stored value when divided, or back to Micrometre when multiplied.</summary>
		internal const Double ToMicrometres = (Double)(ToMetres * 10e6);

		/// <summary>Create a <see cref="Length"/> by providing a Micrometre value.</summary>
		public static Length Micrometres(Double value) { return new Length(value / ToMicrometres); }

		/// <summary>Get the <see cref="Length"/> value in the Micrometre unit.</summary>
		public Double InMicrometres { get { return value * ToMicrometres; } }

		/// <summary>Clamp this <see cref="Length"/> value to the Micrometre range, returning the clamped value.</summary>
		public Length ClampMicrometres(Double min, Double max) {
			min = min / ToMicrometres;
			max = max / ToMicrometres;
			return new Length(value > max ? max : value < min ? min : value);
		}

		/// <summary>Clamp this <see cref="Length"/> value to the Micrometre range, changing this value, then return it.</summary>
		public Length ClampInPlaceMicrometres(Double min, Double max) {
			if(value > (max = max / ToMicrometres))
				value = max;
			else if(value < (min = min / ToMicrometres))
				value = min;
			return this;
		}

		/// <summary>This is the constant value for conversion from the Millimetre unit to the stored value when divided, or back to Millimetre when multiplied.</summary>
		internal const Double ToMillimetres = (Double)(ToMetres * 1000);

		/// <summary>Create a <see cref="Length"/> by providing a Millimetre value.</summary>
		public static Length Millimetres(Double value) { return new Length(value / ToMillimetres); }

		/// <summary>Get the <see cref="Length"/> value in the Millimetre unit.</summary>
		public Double InMillimetres { get { return value * ToMillimetres; } }

		/// <summary>Clamp this <see cref="Length"/> value to the Millimetre range, returning the clamped value.</summary>
		public Length ClampMillimetres(Double min, Double max) {
			min = min / ToMillimetres;
			max = max / ToMillimetres;
			return new Length(value > max ? max : value < min ? min : value);
		}

		/// <summary>Clamp this <see cref="Length"/> value to the Millimetre range, changing this value, then return it.</summary>
		public Length ClampInPlaceMillimetres(Double min, Double max) {
			if(value > (max = max / ToMillimetres))
				value = max;
			else if(value < (min = min / ToMillimetres))
				value = min;
			return this;
		}

		/// <summary>This is the constant value for conversion from the Nanometre unit to the stored value when divided, or back to Nanometre when multiplied.</summary>
		internal const Double ToNanometres = (Double)(ToMetres * 10e9);

		/// <summary>Create a <see cref="Length"/> by providing a Nanometre value.</summary>
		public static Length Nanometres(Double value) { return new Length(value / ToNanometres); }

		/// <summary>Get the <see cref="Length"/> value in the Nanometre unit.</summary>
		public Double InNanometres { get { return value * ToNanometres; } }

		/// <summary>Clamp this <see cref="Length"/> value to the Nanometre range, returning the clamped value.</summary>
		public Length ClampNanometres(Double min, Double max) {
			min = min / ToNanometres;
			max = max / ToNanometres;
			return new Length(value > max ? max : value < min ? min : value);
		}

		/// <summary>Clamp this <see cref="Length"/> value to the Nanometre range, changing this value, then return it.</summary>
		public Length ClampInPlaceNanometres(Double min, Double max) {
			if(value > (max = max / ToNanometres))
				value = max;
			else if(value < (min = min / ToNanometres))
				value = min;
			return this;
		}

		/// <summary>This is the constant value for conversion from the Yard unit to the stored value when divided, or back to Yard when multiplied.</summary>
		internal const Double ToYards = (Double)(ToMetres / 0.9144);

		/// <summary>Create a <see cref="Length"/> by providing a Yard value.</summary>
		public static Length Yards(Double value) { return new Length(value / ToYards); }

		/// <summary>Get the <see cref="Length"/> value in the Yard unit.</summary>
		public Double InYards { get { return value * ToYards; } }

		/// <summary>Clamp this <see cref="Length"/> value to the Yard range, returning the clamped value.</summary>
		public Length ClampYards(Double min, Double max) {
			min = min / ToYards;
			max = max / ToYards;
			return new Length(value > max ? max : value < min ? min : value);
		}

		/// <summary>Clamp this <see cref="Length"/> value to the Yard range, changing this value, then return it.</summary>
		public Length ClampInPlaceYards(Double min, Double max) {
			if(value > (max = max / ToYards))
				value = max;
			else if(value < (min = min / ToYards))
				value = min;
			return this;
		}

		/// <summary>Get the absolute value of this <see cref="Length"/>.</summary>
		public Length AsUnsigned { get { return new Length(Math.Abs(value)); }}

		/// <summary>Create a <see cref="Length"/> by providing the universal unit, which is Metres.</summary>
		public static Length Universal(double value) { return Metres(value); }

		/// <summary>Get this <see cref="Length"/> in the universal unit, which is Metres.</summary>
		public double InUniversal { get { return InMetres; } }

		/// <summary>The coded value.</summary>
		Double value;

		/// <summary>Private constructor for the <see cref="Length"/>; the real constructors are the static unit methods.</summary>
		Length(Double value) { this.value = value; }

		/// <summary>Get the zero value for <see cref="Length"/>.</summary>
		public static readonly Length Zero = new Length(0);

		/// <summary>Get the positive infinity value for <see cref="Length"/>.</summary>
		public static readonly Length PositiveInfinity = new Length(Double.PositiveInfinity);

		/// <summary>Get the negative infinity value for <see cref="Length"/>.</summary>
		public static readonly Length NegativeInfinity = new Length(Double.NegativeInfinity);

		/// <summary>Get the not-a-number value for <see cref="Length"/>.</summary>
		public static readonly Length NaN = new Length(Double.NaN);

		/// <summary>Clamp this <see cref="Length"/> to the provided range, returning the result.</summary>
		public Length Clamp(Length min, Length max) { return new Length(value > max.value ? max.value : value < min.value ? min.value : value); }

		/// <summary>Clamp this <see cref="Length"/> to the provided range, storing the result in this value and returning it.</summary>
		public Length ClampInPlace(Length min, Length max) {
			if(value > max.value)
				value = min.value;
			else if(value < min.value)
				value = min.value;
			return this;
		}

		/// <summary>Compare this <see cref="Length"/> to the other value, returning the relative order.</summary>.
		public int CompareTo(Length other) { return value.CompareTo(other.value); }

		/// <summary>Compare this <see cref="Length"/> to the other value, returning whether they are equal.</summary>
		public bool Equals(Length other) { return value == other.value; }

		/// <summary>If the other object is <see cref="Length"/>, determine equality and return the result; otherwise return <c>null</c>.</summary>
		public override bool Equals(object other) {
			if(other is Length)
				return value == ((Length)other).value;
			return base.Equals(other);
		}

		/// <summary>Get a hash code from the value.</summary>
		public override int GetHashCode() { return value.GetHashCode(); }

		/// <summary>Return the maximum of this value or the passed value.</summary>
		public Length Max(Length other) { return new Length(other.value > value ? other.value : value); }

		/// <summary>Assign this <see cref="Length"/> to the maximum of this value or the other one.</summary>
		public void MaxInPlace(Length other) { if(other.value > value) value = other.value; }

		/// <summary>Return the minimum of this value or the passed value.</summary>
		public Length Min(Length other) { return new Length(other.value < value ? other.value : value); }

		/// <summary>Assign this <see cref="Length"/> to the minimum of this value or the other one.</summary>
		public void MinInPlace(Length other) { if(other.value < value) value = other.value; }

		/// <summary>Convert to a string of the form "&lt;value&gt;m".</summary>
		public override string ToString() { return ToString(null, null); }

		/// <summary>Convert to a string of the form "&lt;value&gt;m".</summary>
		public string ToString(string format, IFormatProvider provider) { return InMetres.ToString(format, provider) + "m"; }

		/// <summary>Test for equality between the <see cref="Length"/> values.</summary>
		public static bool operator ==(Length a, Length b) { return a.value == b.value; }

		/// <summary>Test for inequality between the <see cref="Length"/> values.</summary>
		public static bool operator !=(Length a, Length b) { return a.value != b.value; }

		/// <summary>Compare the <see cref="Length"/> values.</summary>
		public static bool operator >(Length a, Length b) { return a.value > b.value; }

		/// <summary>Compare the <see cref="Length"/> values.</summary>
		public static bool operator >=(Length a, Length b) { return a.value >= b.value; }

		/// <summary>Compare the <see cref="Length"/> values.</summary>
		public static bool operator <(Length a, Length b) { return a.value < b.value; }

		/// <summary>Compare the <see cref="Length"/> values.</summary>
		public static bool operator <=(Length a, Length b) { return a.value <= b.value; }

		/// <summary>Return the positive of the <see cref="Length"/> value, which is itself.</summary>
		public static Length operator +(Length value) { return new Length(+value.value); }

		/// <summary>Return the negative of the <see cref="Length"/> value.</summary>
		public static Length operator -(Length value) { return new Length(-value.value); }

		/// <summary>Add the <see cref="Length"/> values.</summary>
		public static Length operator +(Length a, Length b) { return new Length(a.value + b.value); }

		/// <summary>Subtract the <see cref="Length"/> values.</summary>
		public static Length operator -(Length a, Length b) { return new Length(a.value - b.value); }

		/// <summary>Multiply the <see cref="Length"/> value to a scalar.</summary>
		public static Length operator *(Length a, Double b) { return new Length(a.value * b); }

		/// <summary>Multiply the <see cref="Length"/> value to a scalar.</summary>
		public static Length operator *(Double a, Length b) { return new Length(a * b.value); }

		/// <summary>Divide the <see cref="Length"/> value by a scalar.</summary>
		public static Length operator /(Length a, Double b) { return new Length(a.value / b); }

		/// <summary>Divide the <see cref="Length"/> values, producing the scalar magnitude difference between them.</summary>
		public static Double operator /(Length a, Length b) { return a.value / b.value; }

		/// <summary>Modulo the <see cref="Length"/> values, producing the scalar result.</summary>
		public static Double operator %(Length a, Length b) { return a.value % b.value; }
	
		/// <summary>Get this value multiplied by itself.</summary>
		public Area Squared { get { return Area.SquareMetres(InMetres * InMetres); } }
		
		/// <summary>Multiply the <see cref="Length"/>s together, producing an <see cref="Area"/>.</summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static Area operator *(Length a, Length b) { return Area.SquareMetres(a.InMetres * b.InMetres); }
		
		/// <summary>Scale the <see cref="Length"/> by a scalar vector.</summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static Vector3 operator *(Length a, Vector3d b) { return new Vector3(a * b.X, a * b.Y, a * b.Z); }

		/// <summary>Scale a <see cref="Length"/> by a scalar vector.</summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static Vector3 operator *(Vector3d a, Length b) { return new Vector3(a.X * b, a.Y * b, a.Z * b); }

		/// <summary>Divide a <see cref="Length"/> by a <see cref="TimeSpan"/>, producing a <see cref="Velocity"/>.</summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static Velocity operator /(Length a, TimeSpan b) { return Velocity.MetresPerSecond(a.InMetres / b.TotalSeconds); }
	}
	}


