using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare {
			public struct Angle : IComparable<Angle>, IEquatable<Angle>, IFormattable {
			internal const Double ToRadians = (Double)(1);

		public static Angle Radians(Double value) { return new Angle(value / ToRadians); }

		public Double InRadians { get { return value * ToRadians; } }

		public static readonly Angle Radian = Radians(1);

		public Angle ClampRadians(Double min, Double max) {
			min = min / ToRadians;
			max = max / ToRadians;
			return new Angle(value > max ? max : value < min ? min : value);
		}

		public void ClampInPlaceRadians(Double min, Double max) {
			if(value > (max = max / ToRadians))
				value = max;
			else if(value < (min = min / ToRadians))
				value = min;
		}

		internal const Double ToDegrees = (Double)(ToRadians * 180 / Math.PI);

		public static Angle Degrees(Double value) { return new Angle(value / ToDegrees); }

		public Double InDegrees { get { return value * ToDegrees; } }

		public static readonly Angle Degree = Degrees(1);

		public Angle ClampDegrees(Double min, Double max) {
			min = min / ToDegrees;
			max = max / ToDegrees;
			return new Angle(value > max ? max : value < min ? min : value);
		}

		public void ClampInPlaceDegrees(Double min, Double max) {
			if(value > (max = max / ToDegrees))
				value = max;
			else if(value < (min = min / ToDegrees))
				value = min;
		}

		internal const Double ToTurns = (Double)(ToRadians * 2 * Math.PI);

		public static Angle Turns(Double value) { return new Angle(value / ToTurns); }

		public Double InTurns { get { return value * ToTurns; } }

		public static readonly Angle Turn = Turns(1);

		public Angle ClampTurns(Double min, Double max) {
			min = min / ToTurns;
			max = max / ToTurns;
			return new Angle(value > max ? max : value < min ? min : value);
		}

		public void ClampInPlaceTurns(Double min, Double max) {
			if(value > (max = max / ToTurns))
				value = max;
			else if(value < (min = min / ToTurns))
				value = min;
		}

				public Angle Absolute { get { return new Angle(Math.Abs(value)); }}

		public static Angle Universal(double value) { return Degrees(value); }
		public double InUniversal { get { return InDegrees; } }

		Double value;
		Angle(Double value) { this.value = value; }

		public static readonly Angle Zero = new Angle(0);
		public static readonly Angle PositiveInfinity = new Angle(Double.PositiveInfinity);
		public static readonly Angle NegativeInfinity = new Angle(Double.NegativeInfinity);
		public static readonly Angle NaN = new Angle(Double.NaN);

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
		public static Angle operator *(Angle a, Double b) { return new Angle(a.value * b); }
		public static Angle operator *(Double a, Angle b) { return new Angle(a * b.value); }
		public static Angle operator /(Angle a, Double b) { return new Angle(a.value / b); }
		public static Double operator /(Angle a, Angle b) { return a.value / b.value; }
		public static Double operator %(Angle a, Angle b) { return a.value % b.value; }
					
			/// <summary>Get a 90 degree angle.</summary>
			public static readonly Angle Corner = Degrees(90);

			/// <summary>Get a 180 degree angle.</summary>
			public static readonly Angle Flip = Degrees(180);

			/// <summary>Get a 270 degree angle.</summary>
			public static readonly Angle Degrees270 = Degrees(270);
		}
	
			/// <summary>An angle between two lines or a rotation, depending upon usage.</summary>
		public struct Angle3 : IEquatable<Angle3>, IFormattable {
							public static Angle3 Radians( double x ,  double y ,  double z ) { 
					return new Angle3( Angle.Radians(x) ,  Angle.Radians(y) ,  Angle.Radians(z) );
				}
			
															public static Angle3 Radians( Vector3d value ) { 
							Angle3 result;
															result.X = Angle.Radians(value.X);
															result.Y = Angle.Radians(value.Y);
															result.Z = Angle.Radians(value.Z);
														return result;
						}
											public static Angle3 Radians( ref  Vector3d value ) { 
							Angle3 result;
															result.X = Angle.Radians(value.X);
															result.Y = Angle.Radians(value.Y);
															result.Z = Angle.Radians(value.Z);
														return result;
						}
																				public static void Radians( Vector3d value , out Angle3 result) { 
							
															result.X = Angle.Radians(value.X);
															result.Y = Angle.Radians(value.Y);
															result.Z = Angle.Radians(value.Z);
														return;
						}
											public static void Radians( ref  Vector3d value , out Angle3 result) { 
							
															result.X = Angle.Radians(value.X);
															result.Y = Angle.Radians(value.Y);
															result.Z = Angle.Radians(value.Z);
														return;
						}
									
				public Vector3d InRadians { get { return new Vector3d( X.InRadians ,  Y.InRadians ,  Z.InRadians ); } }
							public static Angle3 Degrees( double x ,  double y ,  double z ) { 
					return new Angle3( Angle.Degrees(x) ,  Angle.Degrees(y) ,  Angle.Degrees(z) );
				}
			
															public static Angle3 Degrees( Vector3d value ) { 
							Angle3 result;
															result.X = Angle.Degrees(value.X);
															result.Y = Angle.Degrees(value.Y);
															result.Z = Angle.Degrees(value.Z);
														return result;
						}
											public static Angle3 Degrees( ref  Vector3d value ) { 
							Angle3 result;
															result.X = Angle.Degrees(value.X);
															result.Y = Angle.Degrees(value.Y);
															result.Z = Angle.Degrees(value.Z);
														return result;
						}
																				public static void Degrees( Vector3d value , out Angle3 result) { 
							
															result.X = Angle.Degrees(value.X);
															result.Y = Angle.Degrees(value.Y);
															result.Z = Angle.Degrees(value.Z);
														return;
						}
											public static void Degrees( ref  Vector3d value , out Angle3 result) { 
							
															result.X = Angle.Degrees(value.X);
															result.Y = Angle.Degrees(value.Y);
															result.Z = Angle.Degrees(value.Z);
														return;
						}
									
				public Vector3d InDegrees { get { return new Vector3d( X.InDegrees ,  Y.InDegrees ,  Z.InDegrees ); } }
							public static Angle3 Turns( double x ,  double y ,  double z ) { 
					return new Angle3( Angle.Turns(x) ,  Angle.Turns(y) ,  Angle.Turns(z) );
				}
			
															public static Angle3 Turns( Vector3d value ) { 
							Angle3 result;
															result.X = Angle.Turns(value.X);
															result.Y = Angle.Turns(value.Y);
															result.Z = Angle.Turns(value.Z);
														return result;
						}
											public static Angle3 Turns( ref  Vector3d value ) { 
							Angle3 result;
															result.X = Angle.Turns(value.X);
															result.Y = Angle.Turns(value.Y);
															result.Z = Angle.Turns(value.Z);
														return result;
						}
																				public static void Turns( Vector3d value , out Angle3 result) { 
							
															result.X = Angle.Turns(value.X);
															result.Y = Angle.Turns(value.Y);
															result.Z = Angle.Turns(value.Z);
														return;
						}
											public static void Turns( ref  Vector3d value , out Angle3 result) { 
							
															result.X = Angle.Turns(value.X);
															result.Y = Angle.Turns(value.Y);
															result.Z = Angle.Turns(value.Z);
														return;
						}
									
				public Vector3d InTurns { get { return new Vector3d( X.InTurns ,  Y.InTurns ,  Z.InTurns ); } }
			
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

	/// <summary>Initialise a <see cref="Angle3"/> from the provided values for each factor.</summary>
	public Angle3(Angle x, Angle y, Angle z)
	{
					X = x;
					Y = y;
					Z = z;
			}

	/// <summary>Initialise a <see cref="Angle3"/> from a list.</summary>
	public Angle3(IList<Angle> list, int index = 0) : this(list[index + 0], list[index + 1], list[index + 2]) { }

	/// <summary>Initialise a <see cref="Angle3"/> from a single scalar that is applied to all factors.</summary>
	public Angle3(Angle value) : this(value, value, value) { }

	#endregion Constructors

	#region Methods

	/// <summary>Get whether this <see cref="Angle3"/> has equal factors as the other <see cref="Angle3"/>.</summary>
	public bool Equals(Angle3 other) {
		return  X == other.X && Y == other.Y && Z == other.Z ;
	}

	/// <summary>If the other object is a <see cref="Angle3"/> of the same type, get whether this <see cref="Angle3"/> has equal factors as it; otherwise return false./summary>
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

	/// <summary>Convert this <see cref="Angle3"/> to a string of the form "X, Y, Z".</summary>
	public string ToCommaSeparatedString(string format = null, IFormatProvider provider = null) {
		return X.ToString(format, provider) + ", " + Y.ToString(format, provider) + ", " + Z.ToString(format, provider);
	}

	/// <summary>Convert this <see cref="Angle3"/> to a string of the form "Angle3(X, Y, Z)".</summary>
	public override string ToString()
	{
		return ToString(null, null);
	}

	/// <summary>Convert this <see cref="Angle3"/> to a string of the form "Angle3(X, Y, Z)".</summary>
	public string ToString(string format, IFormatProvider provider)
	{
		return "Angle3(" + ToCommaSeparatedString(format, provider) + ")";
	}

	/// <summary>Convert this <see cref="Angle3"/> to a string of the form "{X, Y, Z".</summary>
	public string ToShortString(string format = null, IFormatProvider provider = null) { return "{" + ToCommaSeparatedString(format, provider) + "}"; }	

	#endregion Methods
	
	public static bool operator ==(Angle3 a, Angle3 b) { return a.X == b.X&&a.Y == b.Y&&a.Z == b.Z; }

	public static bool operator !=(Angle3 a, Angle3 b) { return a.X != b.X||a.Y != b.Y||a.Z != b.Z; }
		}
	}







