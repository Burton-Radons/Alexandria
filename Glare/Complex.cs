using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare
{
	/// <summary>
	/// A complex value using double factors.
	/// </summary>
	public struct Complex
	{
		/// <summary>Get or set the real factor.</summary>
		public double Real;

		/// <summary>Get or set the imaginary factor.</summary>
		public double Imaginary;

		public Complex(double real, double imaginary = 0) { Real = real; Imaginary = imaginary; }

		public double Abs() { return Math.Sqrt(AbsSquared()); }
		public double AbsSquared() { return Real * Real + Imaginary * Imaginary; }
		public double Atan() { return Math.Atan2(Imaginary, Real); }
		public Complex Cos() { return new Complex(Math.Cos(Real) * Math.Cosh(Imaginary), Math.Sin(Real) * Math.Sinh(Imaginary)); }
		public Complex Exp() { double m = Math.Exp(Real); return new Complex(Math.Cos(Imaginary) * m, Math.Sin(Imaginary) * m); }
		public Complex Flip() { return new Complex(Imaginary, Real); }
		public Complex Floor() { return new Complex(Math.Floor(Real), Math.Floor(Imaginary)); }
		public Complex Log() { return new Complex(0.5 * Math.Log(AbsSquared()), Atan()); }
		public Complex Pow(Complex b) { if (b.Real == 2 && b.Imaginary == 0) return Pow2(); return (b * Log()).Exp(); }
		public Complex Pow(double b) { return (b * Log()).Exp(); }
		public Complex Pow2() { return new Complex(Real * Real - Imaginary * Imaginary, 2 * Real * Imaginary); }
		public Complex Round() { return new Complex(Math.Round(Real), Math.Round(Imaginary)); }
		public Complex Sin() { return new Complex(Math.Sin(Real) * Math.Cosh(Imaginary), Math.Cos(Real) * Math.Sinh(Imaginary)); }
		public Complex Squared() { return new Complex(Real * Real - Imaginary * Imaginary, Imaginary * Real + Real * Imaginary); }
		public Complex Sqrt() { double abs = Abs(); return new Complex(Math.Sqrt(0.5 * (abs + Real)), (Imaginary / Math.Abs(Imaginary)) * Math.Sqrt(0.5 * (abs - Real))); }

		public static implicit operator Complex(double value) { return new Complex(value); }

		public static Complex operator +(Complex a, Complex b) { return new Complex(a.Real + b.Real, a.Imaginary + b.Imaginary); }
		public static Complex operator +(Complex a, double b) { return new Complex(a.Real + b, a.Imaginary); }
		public static Complex operator +(double a, Complex b) { return new Complex(a + b.Real, b.Imaginary); }
		public static Complex operator -(Complex a, Complex b) { return new Complex(a.Real - b.Real, a.Imaginary - b.Imaginary); }
		public static Complex operator -(Complex a, double b) { return new Complex(a.Real - b, a.Imaginary); }
		public static Complex operator -(double a, Complex b) { return new Complex(a - b.Real, -b.Imaginary); }
		public static Complex operator *(Complex a, Complex b) { return new Complex(a.Real * b.Real - a.Imaginary * b.Imaginary, a.Imaginary * b.Real + a.Real * b.Imaginary); }
		public static Complex operator *(double a, Complex b) { return new Complex(a * b.Real, a * b.Imaginary); }
		public static Complex operator *(Complex a, double b) { return new Complex(a.Real * b, a.Imaginary * b); }

		public static Complex operator /(Complex a, Complex b)
		{
			double m = 1.0 / (b.Real * b.Real + b.Imaginary * b.Imaginary);
			return new Complex((a.Real * b.Real + a.Imaginary * b.Imaginary) * m, (a.Imaginary * b.Real - a.Real * b.Imaginary) * m);
		}

		public static Complex operator /(Complex a, double b) { double m = 1.0 / (b * b); return new Complex(a.Real * b * m, a.Imaginary * b * m); }
		public static Complex operator /(double a, Complex b) { double m = 1.0 / (b.Real * b.Real + b.Imaginary * b.Imaginary); return new Complex(a * b.Real * m, -a * b.Imaginary * m); }
	}
}