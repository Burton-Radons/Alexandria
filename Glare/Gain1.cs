using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare
{
		/// <summary>The change in power or amplitude of a signal from the input to the output.</summary>
	public struct Gain : IComparable<Gain>, IEquatable<Gain>
	{
				double value;
		Gain(double value) { this.value = value; }

		public static readonly Gain Zero = new Gain(0);
		public static readonly Gain PositiveInfinity = new Gain(double.PositiveInfinity);
		public static readonly Gain NegativeInfinity = new Gain(double.NegativeInfinity);
		public static readonly Gain NaN = new Gain(double.NaN);

		public Gain Clamp(Gain min, Gain max) { return new Gain(value > max.value ? max.value : value < min.value ? min.value : value); }

		public void ClampInPlace(Gain min, Gain max) { if(value > max.value) value = min.value; else if(value < min.value) value = min.value; }

		public int CompareTo(Gain other) { return value.CompareTo(other.value); }

		public bool Equals(Gain other) { return value == other.value; }
		public override bool Equals(object other) { if(other is Gain) return value == ((Gain)other).value; return base.Equals(other); }

		public override int GetHashCode() { return value.GetHashCode(); }

		/// <summary>Return the maximum of this value or the passed value.</summary>
		public Gain Max(Gain other) { return new Gain(other.value > value ? other.value : value); }

		/// <summary>Assign this <see cref="Gain"/> to the maximum of this value or the other one.</summary>
		public void MaxInPlace(Gain other) { if(other.value > value) value = other.value; }

		/// <summary>Return the minimum of this value or the passed value.</summary>
		public Gain Min(Gain other) { return new Gain(other.value < value ? other.value : value); }

		/// <summary>Assign this <see cref="Gain"/> to the minimum of this value or the other one.</summary>
		public void MinInPlace(Gain other) { if(other.value < value) value = other.value; }

		public static bool operator ==(Gain a, Gain b) { return a.value == b.value; }
		public static bool operator !=(Gain a, Gain b) { return a.value != b.value; }
		public static bool operator >(Gain a, Gain b) { return a.value > b.value; }
		public static bool operator >=(Gain a, Gain b) { return a.value >= b.value; }
		public static bool operator <(Gain a, Gain b) { return a.value < b.value; }
		public static bool operator <=(Gain a, Gain b) { return a.value <= b.value; }

		public static Gain operator +(Gain value) { return new Gain(+value.value); }
		public static Gain operator -(Gain value) { return new Gain(-value.value); }

		public static Gain operator +(Gain a, Gain b) { return new Gain(a.value + b.value); }
		public static Gain operator -(Gain a, Gain b) { return new Gain(a.value - b.value); }
		public static Gain operator *(Gain a, double b) { return new Gain(a.value * b); }
		public static Gain operator *(double a, Gain b) { return new Gain(a * b.value); }
		public static Gain operator /(Gain a, double b) { return new Gain(a.value / b); }
		public static double operator %(Gain a, Gain b) { return a.value % b.value; }
	
		public static Gain Linear(double value) { return new Gain(value); }
		public double InLinear { get { return value; } }

		public static Gain AudioDecibels(double value) { return new Gain(Math.Pow(10, value / 20)); }
		public double InAudioDecibels { get { return 20 * Math.Log10(value); } }

		public static Attenuation operator /(Gain gain, Length length) { return Attenuation.PerMetre(gain.InLinear / length.InMetres); }
	}

	
		public struct Attenuation : IComparable<Attenuation>, IEquatable<Attenuation>
	{
				double value;
		Attenuation(double value) { this.value = value; }

		public static readonly Attenuation Zero = new Attenuation(0);
		public static readonly Attenuation PositiveInfinity = new Attenuation(double.PositiveInfinity);
		public static readonly Attenuation NegativeInfinity = new Attenuation(double.NegativeInfinity);
		public static readonly Attenuation NaN = new Attenuation(double.NaN);

		public Attenuation Clamp(Attenuation min, Attenuation max) { return new Attenuation(value > max.value ? max.value : value < min.value ? min.value : value); }

		public void ClampInPlace(Attenuation min, Attenuation max) { if(value > max.value) value = min.value; else if(value < min.value) value = min.value; }

		public int CompareTo(Attenuation other) { return value.CompareTo(other.value); }

		public bool Equals(Attenuation other) { return value == other.value; }
		public override bool Equals(object other) { if(other is Attenuation) return value == ((Attenuation)other).value; return base.Equals(other); }

		public override int GetHashCode() { return value.GetHashCode(); }

		/// <summary>Return the maximum of this value or the passed value.</summary>
		public Attenuation Max(Attenuation other) { return new Attenuation(other.value > value ? other.value : value); }

		/// <summary>Assign this <see cref="Attenuation"/> to the maximum of this value or the other one.</summary>
		public void MaxInPlace(Attenuation other) { if(other.value > value) value = other.value; }

		/// <summary>Return the minimum of this value or the passed value.</summary>
		public Attenuation Min(Attenuation other) { return new Attenuation(other.value < value ? other.value : value); }

		/// <summary>Assign this <see cref="Attenuation"/> to the minimum of this value or the other one.</summary>
		public void MinInPlace(Attenuation other) { if(other.value < value) value = other.value; }

		public static bool operator ==(Attenuation a, Attenuation b) { return a.value == b.value; }
		public static bool operator !=(Attenuation a, Attenuation b) { return a.value != b.value; }
		public static bool operator >(Attenuation a, Attenuation b) { return a.value > b.value; }
		public static bool operator >=(Attenuation a, Attenuation b) { return a.value >= b.value; }
		public static bool operator <(Attenuation a, Attenuation b) { return a.value < b.value; }
		public static bool operator <=(Attenuation a, Attenuation b) { return a.value <= b.value; }

		public static Attenuation operator +(Attenuation value) { return new Attenuation(+value.value); }
		public static Attenuation operator -(Attenuation value) { return new Attenuation(-value.value); }

		public static Attenuation operator +(Attenuation a, Attenuation b) { return new Attenuation(a.value + b.value); }
		public static Attenuation operator -(Attenuation a, Attenuation b) { return new Attenuation(a.value - b.value); }
		public static Attenuation operator *(Attenuation a, double b) { return new Attenuation(a.value * b); }
		public static Attenuation operator *(double a, Attenuation b) { return new Attenuation(a * b.value); }
		public static Attenuation operator /(Attenuation a, double b) { return new Attenuation(a.value / b); }
		public static double operator %(Attenuation a, Attenuation b) { return a.value % b.value; }
	
		public static Attenuation PerMetre(double value) { return new Attenuation(value); }
		public double InPerMetre { get { return value; } }

		public static Gain operator *(Attenuation a, Length b) { return Gain.Linear(a.InPerMetre * b.InMetres); }
		public static Gain operator *(Length a, Attenuation b) { return Gain.Linear(a.InMetres * b.InPerMetre); }
		public static Attenuation operator *(Attenuation a, Gain b) { return Attenuation.PerMetre(a.InPerMetre * b.InLinear); }
		public static Attenuation operator *(Gain a, Attenuation b) { return Attenuation.PerMetre(a.InLinear * b.InPerMetre); }
	}
	}


