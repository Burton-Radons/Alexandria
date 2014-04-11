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
		internal const double ToCentimetres = (double)(ToMetres * 100);

		public static Length Centimetres(double value) { return new Length(value / ToCentimetres); }

		public double InCentimetres { get { return value * ToCentimetres; } }

		public static readonly Length Centimetre = Centimetres(1);

		public Length ClampCentimetres(double min, double max)
		{
			min = min / ToCentimetres;
			max = max / ToCentimetres;
			return new Length(value > max ? max : value < min ? min : value);
		}

		public void ClampInPlaceCentimetres(double min, double max)
		{
			if(value > (max = max / ToCentimetres))
				value = max;
			else if(value < (min = min / ToCentimetres))
				value = min;
		}

		internal const double ToFeet = (double)(ToYards * 3);

		public static Length Feet(double value) { return new Length(value / ToFeet); }

		public double InFeet { get { return value * ToFeet; } }

		public static readonly Length Foot = Feet(1);

		public Length ClampFeet(double min, double max)
		{
			min = min / ToFeet;
			max = max / ToFeet;
			return new Length(value > max ? max : value < min ? min : value);
		}

		public void ClampInPlaceFeet(double min, double max)
		{
			if(value > (max = max / ToFeet))
				value = max;
			else if(value < (min = min / ToFeet))
				value = min;
		}

		internal const double ToInches = (double)(ToFeet * 12);

		public static Length Inches(double value) { return new Length(value / ToInches); }

		public double InInches { get { return value * ToInches; } }

		public static readonly Length Inch = Inches(1);

		public Length ClampInches(double min, double max)
		{
			min = min / ToInches;
			max = max / ToInches;
			return new Length(value > max ? max : value < min ? min : value);
		}

		public void ClampInPlaceInches(double min, double max)
		{
			if(value > (max = max / ToInches))
				value = max;
			else if(value < (min = min / ToInches))
				value = min;
		}

		internal const double ToKilometres = (double)(ToMetres / 10e3);

		public static Length Kilometres(double value) { return new Length(value / ToKilometres); }

		public double InKilometres { get { return value * ToKilometres; } }

		public static readonly Length Kilometre = Kilometres(1);

		public Length ClampKilometres(double min, double max)
		{
			min = min / ToKilometres;
			max = max / ToKilometres;
			return new Length(value > max ? max : value < min ? min : value);
		}

		public void ClampInPlaceKilometres(double min, double max)
		{
			if(value > (max = max / ToKilometres))
				value = max;
			else if(value < (min = min / ToKilometres))
				value = min;
		}

		internal const double ToMegametres = (double)(ToMetres / 10e6);

		public static Length Megametres(double value) { return new Length(value / ToMegametres); }

		public double InMegametres { get { return value * ToMegametres; } }

		public static readonly Length Megametre = Megametres(1);

		public Length ClampMegametres(double min, double max)
		{
			min = min / ToMegametres;
			max = max / ToMegametres;
			return new Length(value > max ? max : value < min ? min : value);
		}

		public void ClampInPlaceMegametres(double min, double max)
		{
			if(value > (max = max / ToMegametres))
				value = max;
			else if(value < (min = min / ToMegametres))
				value = min;
		}

		internal const double ToMetres = (double)(1);

		public static Length Metres(double value) { return new Length(value / ToMetres); }

		public double InMetres { get { return value * ToMetres; } }

		public static readonly Length Metre = Metres(1);

		public Length ClampMetres(double min, double max)
		{
			min = min / ToMetres;
			max = max / ToMetres;
			return new Length(value > max ? max : value < min ? min : value);
		}

		public void ClampInPlaceMetres(double min, double max)
		{
			if(value > (max = max / ToMetres))
				value = max;
			else if(value < (min = min / ToMetres))
				value = min;
		}

		internal const double ToMicrometres = (double)(ToMetres * 10e6);

		public static Length Micrometres(double value) { return new Length(value / ToMicrometres); }

		public double InMicrometres { get { return value * ToMicrometres; } }

		public static readonly Length Micrometre = Micrometres(1);

		public Length ClampMicrometres(double min, double max)
		{
			min = min / ToMicrometres;
			max = max / ToMicrometres;
			return new Length(value > max ? max : value < min ? min : value);
		}

		public void ClampInPlaceMicrometres(double min, double max)
		{
			if(value > (max = max / ToMicrometres))
				value = max;
			else if(value < (min = min / ToMicrometres))
				value = min;
		}

		internal const double ToMillimetres = (double)(ToMetres * 1000);

		public static Length Millimetres(double value) { return new Length(value / ToMillimetres); }

		public double InMillimetres { get { return value * ToMillimetres; } }

		public static readonly Length Millimetre = Millimetres(1);

		public Length ClampMillimetres(double min, double max)
		{
			min = min / ToMillimetres;
			max = max / ToMillimetres;
			return new Length(value > max ? max : value < min ? min : value);
		}

		public void ClampInPlaceMillimetres(double min, double max)
		{
			if(value > (max = max / ToMillimetres))
				value = max;
			else if(value < (min = min / ToMillimetres))
				value = min;
		}

		internal const double ToNanometres = (double)(ToMetres * 10e9);

		public static Length Nanometres(double value) { return new Length(value / ToNanometres); }

		public double InNanometres { get { return value * ToNanometres; } }

		public static readonly Length Nanometre = Nanometres(1);

		public Length ClampNanometres(double min, double max)
		{
			min = min / ToNanometres;
			max = max / ToNanometres;
			return new Length(value > max ? max : value < min ? min : value);
		}

		public void ClampInPlaceNanometres(double min, double max)
		{
			if(value > (max = max / ToNanometres))
				value = max;
			else if(value < (min = min / ToNanometres))
				value = min;
		}

		internal const double ToYards = (double)(ToMetres / 0.9144);

		public static Length Yards(double value) { return new Length(value / ToYards); }

		public double InYards { get { return value * ToYards; } }

		public static readonly Length Yard = Yards(1);

		public Length ClampYards(double min, double max)
		{
			min = min / ToYards;
			max = max / ToYards;
			return new Length(value > max ? max : value < min ? min : value);
		}

		public void ClampInPlaceYards(double min, double max)
		{
			if(value > (max = max / ToYards))
				value = max;
			else if(value < (min = min / ToYards))
				value = min;
		}

				public static Length Universal(double value) { return Metres(value); }
		public double InUniversal { get { return InMetres; } }

		double value;
		Length(double value) { this.value = value; }

		public static readonly Length Zero = new Length(0);
		public static readonly Length PositiveInfinity = new Length(double.PositiveInfinity);
		public static readonly Length NegativeInfinity = new Length(double.NegativeInfinity);
		public static readonly Length NaN = new Length(double.NaN);

		public Length Clamp(Length min, Length max) { return new Length(value > max.value ? max.value : value < min.value ? min.value : value); }

		public void ClampInPlace(Length min, Length max) { if(value > max.value) value = min.value; else if(value < min.value) value = min.value; }

		public int CompareTo(Length other) { return value.CompareTo(other.value); }

		public bool Equals(Length other) { return value == other.value; }
		public override bool Equals(object other) { if(other is Length) return value == ((Length)other).value; return base.Equals(other); }

		public override int GetHashCode() { return value.GetHashCode(); }

		/// <summary>Return the maximum of this value or the passed value.</summary>
		public Length Max(Length other) { return new Length(other.value > value ? other.value : value); }

		/// <summary>Assign this <see cref="Length"/> to the maximum of this value or the other one.</summary>
		public void MaxInPlace(Length other) { if(other.value > value) value = other.value; }

		/// <summary>Return the minimum of this value or the passed value.</summary>
		public Length Min(Length other) { return new Length(other.value < value ? other.value : value); }

		/// <summary>Assign this <see cref="Length"/> to the minimum of this value or the other one.</summary>
		public void MinInPlace(Length other) { if(other.value < value) value = other.value; }

		/// <summary>Convert to a string of the form "<value>m".</summary>
		public override string ToString() { return ToString(null, null); }

		/// <summary>Convert to a string of the form "<value>m".</summary>
		public string ToString(string format, IFormatProvider provider) { return value.ToString(format, provider) + "m"; }

		public static bool operator ==(Length a, Length b) { return a.value == b.value; }
		public static bool operator !=(Length a, Length b) { return a.value != b.value; }
		public static bool operator >(Length a, Length b) { return a.value > b.value; }
		public static bool operator >=(Length a, Length b) { return a.value >= b.value; }
		public static bool operator <(Length a, Length b) { return a.value < b.value; }
		public static bool operator <=(Length a, Length b) { return a.value <= b.value; }

		public static Length operator +(Length value) { return new Length(+value.value); }
		public static Length operator -(Length value) { return new Length(-value.value); }

		public static Length operator +(Length a, Length b) { return new Length(a.value + b.value); }
		public static Length operator -(Length a, Length b) { return new Length(a.value - b.value); }
		public static Length operator *(Length a, double b) { return new Length(a.value * b); }
		public static Length operator *(double a, Length b) { return new Length(a * b.value); }
		public static Length operator /(Length a, double b) { return new Length(a.value / b); }
		public static double operator /(Length a, Length b) { return a.value / b.value; }
		public static double operator %(Length a, Length b) { return a.value % b.value; }
	
		/// <summary>Get this value multiplied by itself.</summary>
		public Area Squared { get { return Area.SquareMetres(InMetres * InMetres); } }
		
		public static Area operator *(Length a, Length b) { return Area.SquareMetres(a.InMetres * b.InMetres); }
		public static Vector3 operator *(Length a, Vector3d b) { return new Vector3(a * b.X, a * b.Y, a * b.Z); }
		public static Vector3 operator *(Vector3d a, Length b) { return new Vector3(a.X * b, a.Y * b, a.Z * b); }
		public static Velocity operator /(Length a, TimeSpan b) { return Velocity.MetresPerSecond(a.InMetres / b.TotalSeconds); }
	}
	}


