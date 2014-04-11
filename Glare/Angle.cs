using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare
{
			/// <summary>An angle between two lines or a rotation, depending upon usage.</summary>
		public struct Angle : IComparable<Angle>, IEquatable<Angle>, IFormattable {
			internal const double ToRadians = (double)(1);

		public static Angle Radians(double value) { return new Angle(value / ToRadians); }

		public double InRadians { get { return value * ToRadians; } }

		public static readonly Angle Radian = Radians(1);

		public Angle ClampRadians(double min, double max) {
			min = min / ToRadians;
			max = max / ToRadians;
			return new Angle(value > max ? max : value < min ? min : value);
		}

		public void ClampInPlaceRadians(double min, double max) {
			if(value > (max = max / ToRadians))
				value = max;
			else if(value < (min = min / ToRadians))
				value = min;
		}

		internal const double ToDegrees = (double)(ToRadians * 180 / Math.PI);

		public static Angle Degrees(double value) { return new Angle(value / ToDegrees); }

		public double InDegrees { get { return value * ToDegrees; } }

		public static readonly Angle Degree = Degrees(1);

		public Angle ClampDegrees(double min, double max) {
			min = min / ToDegrees;
			max = max / ToDegrees;
			return new Angle(value > max ? max : value < min ? min : value);
		}

		public void ClampInPlaceDegrees(double min, double max) {
			if(value > (max = max / ToDegrees))
				value = max;
			else if(value < (min = min / ToDegrees))
				value = min;
		}

		internal const double ToTurns = (double)(ToRadians * 2 * Math.PI);

		public static Angle Turns(double value) { return new Angle(value / ToTurns); }

		public double InTurns { get { return value * ToTurns; } }

		public static readonly Angle Turn = Turns(1);

		public Angle ClampTurns(double min, double max) {
			min = min / ToTurns;
			max = max / ToTurns;
			return new Angle(value > max ? max : value < min ? min : value);
		}

		public void ClampInPlaceTurns(double min, double max) {
			if(value > (max = max / ToTurns))
				value = max;
			else if(value < (min = min / ToTurns))
				value = min;
		}

				public Angle Absolute { get { return new Angle(Math.Abs(value)); }}

		public static Angle Universal(double value) { return Degrees(value); }
		public double InUniversal { get { return InDegrees; } }

		double value;
		Angle(double value) { this.value = value; }

		public static readonly Angle Zero = new Angle(0);
		public static readonly Angle PositiveInfinity = new Angle(double.PositiveInfinity);
		public static readonly Angle NegativeInfinity = new Angle(double.NegativeInfinity);
		public static readonly Angle NaN = new Angle(double.NaN);

		public Angle Clamp(Angle min, Angle max) { return new Angle(value > max.value ? max.value : value < min.value ? min.value : value); }

		public void ClampInPlace(Angle min, Angle max) { if(value > max.value) value = min.value; else if(value < min.value) value = min.value; }

		public int CompareTo(Angle other) { return value.CompareTo(other.value); }

		public bool Equals(Angle other) { return value == other.value; }
		public override bool Equals(object other) { if(other is Angle) return value == ((Angle)other).value; return base.Equals(other); }

		public override int GetHashCode() { return value.GetHashCode(); }

		/// <summary>Return the maximum of this value or the passed value.</summary>
		public Angle Max(Angle other) { return new Angle(other.value > value ? other.value : value); }

		/// <summary>Assign this <see cref="Angle"/> to the maximum of this value or the other one.</summary>
		public void MaxInPlace(Angle other) { if(other.value > value) value = other.value; }

		/// <summary>Return the minimum of this value or the passed value.</summary>
		public Angle Min(Angle other) { return new Angle(other.value < value ? other.value : value); }

		/// <summary>Assign this <see cref="Angle"/> to the minimum of this value or the other one.</summary>
		public void MinInPlace(Angle other) { if(other.value < value) value = other.value; }

		/// <summary>Convert to a string of the form "<value>°".</summary>
		public override string ToString() { return ToString(null, null); }

		/// <summary>Convert to a string of the form "<value>°".</summary>
		public string ToString(string format, IFormatProvider provider) { return InDegrees.ToString(format, provider) + "°"; }

		public static bool operator ==(Angle a, Angle b) { return a.value == b.value; }
		public static bool operator !=(Angle a, Angle b) { return a.value != b.value; }
		public static bool operator >(Angle a, Angle b) { return a.value > b.value; }
		public static bool operator >=(Angle a, Angle b) { return a.value >= b.value; }
		public static bool operator <(Angle a, Angle b) { return a.value < b.value; }
		public static bool operator <=(Angle a, Angle b) { return a.value <= b.value; }

		public static Angle operator +(Angle value) { return new Angle(+value.value); }
		public static Angle operator -(Angle value) { return new Angle(-value.value); }

		public static Angle operator +(Angle a, Angle b) { return new Angle(a.value + b.value); }
		public static Angle operator -(Angle a, Angle b) { return new Angle(a.value - b.value); }
		public static Angle operator *(Angle a, double b) { return new Angle(a.value * b); }
		public static Angle operator *(double a, Angle b) { return new Angle(a * b.value); }
		public static Angle operator /(Angle a, double b) { return new Angle(a.value / b); }
		public static double operator /(Angle a, Angle b) { return a.value / b.value; }
		public static double operator %(Angle a, Angle b) { return a.value % b.value; }
					
			/// <summary>Get a 90 degree angle.</summary>
			public static readonly Angle Corner = Degrees(90);

			/// <summary>Get a 180 degree angle.</summary>
			public static readonly Angle Flip = Degrees(180);

			/// <summary>Get a 270 degree angle.</summary>
			public static readonly Angle Degrees270 = Degrees(270);
		}
	
			public struct Angle3 : IEquatable<Angle3>, IFormattable {
												public static Angle3 Radians(Vector3d value ) {
						Angle3 result;
													result.X = Angle.Radians(value.X);
													result.Y = Angle.Radians(value.Y);
													result.Z = Angle.Radians(value.Z);
												return result;
					}
									public static void Radians(Vector3d value , out Angle3 result) {
						
													result.X = Angle.Radians(value.X);
													result.Y = Angle.Radians(value.Y);
													result.Z = Angle.Radians(value.Z);
												return;
					}
				
				public Vector3d InRadians { get { return new Vector3d( X.InRadians ,  Y.InRadians ,  Z.InRadians ); } }

				public void ToRadians(out Vector3d result) {
											result.X = X.InRadians;
											result.Y = Y.InRadians;
											result.Z = Z.InRadians;
									}
// < #GenerateReferenceReturn("boop", (r) => {# >
//		< #=r.ReturnType# > Booper(int x, int y < #=r.Argument# >)
//		{
//			< #=r.Begin# >
//			result.Booger = x + y;
//			< #=r.Return# >
//		}
// < #});# >
												public static Angle3 Degrees(Vector3d value ) {
						Angle3 result;
													result.X = Angle.Degrees(value.X);
													result.Y = Angle.Degrees(value.Y);
													result.Z = Angle.Degrees(value.Z);
												return result;
					}
									public static void Degrees(Vector3d value , out Angle3 result) {
						
													result.X = Angle.Degrees(value.X);
													result.Y = Angle.Degrees(value.Y);
													result.Z = Angle.Degrees(value.Z);
												return;
					}
				
				public Vector3d InDegrees { get { return new Vector3d( X.InDegrees ,  Y.InDegrees ,  Z.InDegrees ); } }

				public void ToDegrees(out Vector3d result) {
											result.X = X.InDegrees;
											result.Y = Y.InDegrees;
											result.Z = Z.InDegrees;
									}
// < #GenerateReferenceReturn("boop", (r) => {# >
//		< #=r.ReturnType# > Booper(int x, int y < #=r.Argument# >)
//		{
//			< #=r.Begin# >
//			result.Booger = x + y;
//			< #=r.Return# >
//		}
// < #});# >
												public static Angle3 Turns(Vector3d value ) {
						Angle3 result;
													result.X = Angle.Turns(value.X);
													result.Y = Angle.Turns(value.Y);
													result.Z = Angle.Turns(value.Z);
												return result;
					}
									public static void Turns(Vector3d value , out Angle3 result) {
						
													result.X = Angle.Turns(value.X);
													result.Y = Angle.Turns(value.Y);
													result.Z = Angle.Turns(value.Z);
												return;
					}
				
				public Vector3d InTurns { get { return new Vector3d( X.InTurns ,  Y.InTurns ,  Z.InTurns ); } }

				public void ToTurns(out Vector3d result) {
											result.X = X.InTurns;
											result.Y = Y.InTurns;
											result.Z = Z.InTurns;
									}
// < #GenerateReferenceReturn("boop", (r) => {# >
//		< #=r.ReturnType# > Booper(int x, int y < #=r.Argument# >)
//		{
//			< #=r.Begin# >
//			result.Booger = x + y;
//			< #=r.Return# >
//		}
// < #});# >
						
			/// <summary>Get the absolute sum of all of the axes.</summary>
			public Angle AbsoluteSum { get { return (Angle)( X.Absolute + Y.Absolute + Z.Absolute ); } }
			
			public static Angle3 operator +(Angle3 a) { return new Angle3( +a.X ,  +a.Y ,  +a.Z ); }
			public static Angle3 operator -(Angle3 a) { return new Angle3( -a.X ,  -a.Y ,  -a.Z ); }

			public static Angle3 operator +(Angle3 a, Angle3 b) { return new Angle3( a.X + b.X ,  a.Y + b.Y ,  a.Z + b.Z ); }
			public static Angle3 operator -(Angle3 a, Angle3 b) { return new Angle3( a.X - b.X ,  a.Y - b.Y ,  a.Z - b.Z ); }
			public static Angle3 operator *(Angle3 a, double b) { return new Angle3( a.X * b ,  a.Y * b ,  a.Z * b ); }
			public static Angle3 operator *(double a, Angle3 b) { return new Angle3( a * b.X ,  a * b.Y ,  a * b.Z ); }
			public static Angle3 operator /(Angle3 a, double b) { return new Angle3( a.X / b ,  a.Y / b ,  a.Z / b ); }
			public static Vector3d operator /(Angle3 a, Angle3 b) { return new Vector3d( a.X / b.X ,  a.Y / b.Y ,  a.Z / b.Z ); }
			public static Vector3d operator %(Angle3 a, Angle3 b) { return new Vector3d( a.X % b.X ,  a.Y % b.Y ,  a.Z % b.Z ); }

				#region Fields

			/// <summary>The first axis of the <see cref="Angle3"/>.</summary>
		public Angle X;
			/// <summary>The second axis of the <see cref="Angle3"/>.</summary>
		public Angle Y;
			/// <summary>The third axis of the <see cref="Angle3"/>.</summary>
		public Angle Z;
	
	#endregion Fields

	#region Properties

	public static readonly Angle3 Zero = new Angle3(Angle.Zero, Angle.Zero, Angle.Zero);

	#endregion Properties

	#region Constructors

	/// <summary>Initialise a vector from the provided values for each axis.</summary>
	public Angle3(Angle x, Angle y, Angle z)
	{
					X = x;
					Y = y;
					Z = z;
			}

	/// <summary>Initialise a vector from a list.</summary>
	public Angle3(IList<Angle> list, int index = 0) : this(list[index + 0], list[index + 1], list[index + 2]) { }

	/// <summary>Initialise a vector from a single scalar that is applied to all axes.</summary>
	public Angle3(Angle value) : this(value, value, value) { }

	#endregion Constructors

	#region Methods

	/// <summary>Get whether this vector has equal axes values as the other vector.</summary>
	public bool Equals(Angle3 other) {
		return  X == other.X && Y == other.Y && Z == other.Z ;
	}

	/// <summary>If the other object is a vector of the same type, get whether this vector has equal axes values as it; otherwise return false./summary>
	public override bool Equals(object other)
	{
		if(other is Angle3)
			return Equals((Angle3)other);
		return base.Equals(other);
	}

	/// <summary>Compute a hash code from combining the axes.</summary>
	public override int GetHashCode()
	{
		return  X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode() ;
	}

	/// <summary>Convert this vector to a string of the form "X, Y, Z".</summary>
	public string ToCommaSeparatedString(string format = null, IFormatProvider provider = null) {
		return X.ToString(format, provider) + ", " + Y.ToString(format, provider) + ", " + Z.ToString(format, provider);
	}

	/// <summary>Convert this vector to a string of the form "Angle3(X, Y, Z)".</summary>
	public override string ToString()
	{
		return ToString(null, null);
	}

	/// <summary>Convert this vector to a string of the form "Angle3(X, Y, Z)".</summary>
	public string ToString(string format, IFormatProvider provider)
	{
		return "Angle3(" + ToCommaSeparatedString(format, provider) + ")";
	}

	/// <summary>Convert this vector to a string of the form "{X, Y, Z".</summary>
	public string ToShortString(string format = null, IFormatProvider provider = null) { return "{" + ToCommaSeparatedString(format, provider) + "}"; }	

	#endregion Methods
	
	public static bool operator ==(Angle3 a, Angle3 b) { return a.X == b.X&&a.Y == b.Y&&a.Z == b.Z; }

	public static bool operator !=(Angle3 a, Angle3 b) { return a.X != b.X||a.Y != b.Y||a.Z != b.Z; }
		}
	}







