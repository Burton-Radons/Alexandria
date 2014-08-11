using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare {
			/// <summary>An angle between two lines or a rotation, depending upon usage.</summary>
		public struct Angle : IComparable<Angle>, IEquatable<Angle>, IFormattable {
			/// <summary>This is the constant value for conversion from the Radian unit to the stored value when divided, or back to Radian when multiplied.</summary>
		internal const Double ToRadians = (Double)(1);

		/// <summary>Create a <see cref="Angle"/> by providing a Radian value.</summary>
		public static Angle Radians(Double value) { return new Angle(value / ToRadians); }

		/// <summary>Get the <see cref="Angle"/> value in the Radian unit.</summary>
		public Double InRadians { get { return value * ToRadians; } }

		/// <summary>Clamp this <see cref="Angle"/> value to the Radian range, returning the clamped value.</summary>
		public Angle ClampRadians(Double min, Double max) {
			min = min / ToRadians;
			max = max / ToRadians;
			return new Angle(value > max ? max : value < min ? min : value);
		}

		/// <summary>Clamp this <see cref="Angle"/> value to the Radian range, changing this value, then return it.</summary>
		public Angle ClampInPlaceRadians(Double min, Double max) {
			if(value > (max = max / ToRadians))
				value = max;
			else if(value < (min = min / ToRadians))
				value = min;
			return this;
		}

		/// <summary>This is the constant value for conversion from the Degree unit to the stored value when divided, or back to Degree when multiplied.</summary>
		internal const Double ToDegrees = (Double)(ToRadians * 180 / Math.PI);

		/// <summary>Create a <see cref="Angle"/> by providing a Degree value.</summary>
		public static Angle Degrees(Double value) { return new Angle(value / ToDegrees); }

		/// <summary>Get the <see cref="Angle"/> value in the Degree unit.</summary>
		public Double InDegrees { get { return value * ToDegrees; } }

		/// <summary>Clamp this <see cref="Angle"/> value to the Degree range, returning the clamped value.</summary>
		public Angle ClampDegrees(Double min, Double max) {
			min = min / ToDegrees;
			max = max / ToDegrees;
			return new Angle(value > max ? max : value < min ? min : value);
		}

		/// <summary>Clamp this <see cref="Angle"/> value to the Degree range, changing this value, then return it.</summary>
		public Angle ClampInPlaceDegrees(Double min, Double max) {
			if(value > (max = max / ToDegrees))
				value = max;
			else if(value < (min = min / ToDegrees))
				value = min;
			return this;
		}

		/// <summary>This is the constant value for conversion from the Turn unit to the stored value when divided, or back to Turn when multiplied.</summary>
		internal const Double ToTurns = (Double)(ToRadians * 2 * Math.PI);

		/// <summary>Create a <see cref="Angle"/> by providing a Turn value.</summary>
		public static Angle Turns(Double value) { return new Angle(value / ToTurns); }

		/// <summary>Get the <see cref="Angle"/> value in the Turn unit.</summary>
		public Double InTurns { get { return value * ToTurns; } }

		/// <summary>Clamp this <see cref="Angle"/> value to the Turn range, returning the clamped value.</summary>
		public Angle ClampTurns(Double min, Double max) {
			min = min / ToTurns;
			max = max / ToTurns;
			return new Angle(value > max ? max : value < min ? min : value);
		}

		/// <summary>Clamp this <see cref="Angle"/> value to the Turn range, changing this value, then return it.</summary>
		public Angle ClampInPlaceTurns(Double min, Double max) {
			if(value > (max = max / ToTurns))
				value = max;
			else if(value < (min = min / ToTurns))
				value = min;
			return this;
		}

		/// <summary>Get the absolute value of this <see cref="Angle"/>.</summary>
		public Angle AsUnsigned { get { return new Angle(Math.Abs(value)); }}

		/// <summary>Create a <see cref="Angle"/> by providing the universal unit, which is Degrees.</summary>
		public static Angle Universal(double value) { return Degrees(value); }

		/// <summary>Get this <see cref="Angle"/> in the universal unit, which is Degrees.</summary>
		public double InUniversal { get { return InDegrees; } }

		/// <summary>The coded value.</summary>
		Double value;

		/// <summary>Private constructor for the <see cref="Angle"/>; the real constructors are the static unit methods.</summary>
		Angle(Double value) { this.value = value; }

		/// <summary>Get the zero value for <see cref="Angle"/>.</summary>
		public static readonly Angle Zero = new Angle(0);

		/// <summary>Get the positive infinity value for <see cref="Angle"/>.</summary>
		public static readonly Angle PositiveInfinity = new Angle(Double.PositiveInfinity);

		/// <summary>Get the negative infinity value for <see cref="Angle"/>.</summary>
		public static readonly Angle NegativeInfinity = new Angle(Double.NegativeInfinity);

		/// <summary>Get the not-a-number value for <see cref="Angle"/>.</summary>
		public static readonly Angle NaN = new Angle(Double.NaN);

		/// <summary>Clamp this <see cref="Angle"/> to the provided range, returning the result.</summary>
		public Angle Clamp(Angle min, Angle max) { return new Angle(value > max.value ? max.value : value < min.value ? min.value : value); }

		/// <summary>Clamp this <see cref="Angle"/> to the provided range, storing the result in this value and returning it.</summary>
		public Angle ClampInPlace(Angle min, Angle max) {
			if(value > max.value)
				value = min.value;
			else if(value < min.value)
				value = min.value;
			return this;
		}

		/// <summary>Compare this <see cref="Angle"/> to the other value, returning the relative order.</summary>.
		public int CompareTo(Angle other) { return value.CompareTo(other.value); }

		/// <summary>Compare this <see cref="Angle"/> to the other value, returning whether they are equal.</summary>
		public bool Equals(Angle other) { return value == other.value; }

		/// <summary>If the other object is <see cref="Angle"/>, determine equality and return the result; otherwise return <c>null</c>.</summary>
		public override bool Equals(object other) {
			if(other is Angle)
				return value == ((Angle)other).value;
			return base.Equals(other);
		}

		/// <summary>Get a hash code from the value.</summary>
		public override int GetHashCode() { return value.GetHashCode(); }

		/// <summary>Return the maximum of this value or the passed value.</summary>
		public Angle Max(Angle other) { return new Angle(other.value > value ? other.value : value); }

		/// <summary>Assign this <see cref="Angle"/> to the maximum of this value or the other one.</summary>
		public void MaxInPlace(Angle other) { if(other.value > value) value = other.value; }

		/// <summary>Return the minimum of this value or the passed value.</summary>
		public Angle Min(Angle other) { return new Angle(other.value < value ? other.value : value); }

		/// <summary>Assign this <see cref="Angle"/> to the minimum of this value or the other one.</summary>
		public void MinInPlace(Angle other) { if(other.value < value) value = other.value; }

		/// <summary>Convert to a string of the form "&lt;value&gt;°".</summary>
		public override string ToString() { return ToString(null, null); }

		/// <summary>Convert to a string of the form "&lt;value&gt;°".</summary>
		public string ToString(string format, IFormatProvider provider) { return InDegrees.ToString(format, provider) + "°"; }

		/// <summary>Test for equality between the <see cref="Angle"/> values.</summary>
		public static bool operator ==(Angle a, Angle b) { return a.value == b.value; }

		/// <summary>Test for inequality between the <see cref="Angle"/> values.</summary>
		public static bool operator !=(Angle a, Angle b) { return a.value != b.value; }

		/// <summary>Compare the <see cref="Angle"/> values.</summary>
		public static bool operator >(Angle a, Angle b) { return a.value > b.value; }

		/// <summary>Compare the <see cref="Angle"/> values.</summary>
		public static bool operator >=(Angle a, Angle b) { return a.value >= b.value; }

		/// <summary>Compare the <see cref="Angle"/> values.</summary>
		public static bool operator <(Angle a, Angle b) { return a.value < b.value; }

		/// <summary>Compare the <see cref="Angle"/> values.</summary>
		public static bool operator <=(Angle a, Angle b) { return a.value <= b.value; }

		/// <summary>Return the positive of the <see cref="Angle"/> value, which is itself.</summary>
		public static Angle operator +(Angle value) { return new Angle(+value.value); }

		/// <summary>Return the negative of the <see cref="Angle"/> value.</summary>
		public static Angle operator -(Angle value) { return new Angle(-value.value); }

		/// <summary>Add the <see cref="Angle"/> values.</summary>
		public static Angle operator +(Angle a, Angle b) { return new Angle(a.value + b.value); }

		/// <summary>Subtract the <see cref="Angle"/> values.</summary>
		public static Angle operator -(Angle a, Angle b) { return new Angle(a.value - b.value); }

		/// <summary>Multiply the <see cref="Angle"/> value to a scalar.</summary>
		public static Angle operator *(Angle a, Double b) { return new Angle(a.value * b); }

		/// <summary>Multiply the <see cref="Angle"/> value to a scalar.</summary>
		public static Angle operator *(Double a, Angle b) { return new Angle(a * b.value); }

		/// <summary>Divide the <see cref="Angle"/> value by a scalar.</summary>
		public static Angle operator /(Angle a, Double b) { return new Angle(a.value / b); }

		/// <summary>Divide the <see cref="Angle"/> values, producing the scalar magnitude difference between them.</summary>
		public static Double operator /(Angle a, Angle b) { return a.value / b.value; }

		/// <summary>Modulo the <see cref="Angle"/> values, producing the scalar result.</summary>
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
							/// <summary>Create a <see cref="Angle3"/> from Radian values.</summary>
				public static Angle3 Radians( double x ,  double y ,  double z ) { 
					return new Angle3( Angle.Radians(x) ,  Angle.Radians(y) ,  Angle.Radians(z) );
				}
			
															/// <summary>Create a <see cref="Angle3"/> from Radian values.</summary>
						public static Angle3 Radians( Vector3d value ) { 
							Angle3 result;
															result.X = Angle.Radians(value.X);
															result.Y = Angle.Radians(value.Y);
															result.Z = Angle.Radians(value.Z);
														return result;
						}
											/// <summary>Create a <see cref="Angle3"/> from Radian values.</summary>
						public static Angle3 Radians( ref  Vector3d value ) { 
							Angle3 result;
															result.X = Angle.Radians(value.X);
															result.Y = Angle.Radians(value.Y);
															result.Z = Angle.Radians(value.Z);
														return result;
						}
																				/// <summary>Create a <see cref="Angle3"/> from Radian values.</summary>
						public static void Radians( Vector3d value , out Angle3 result) { 
							
															result.X = Angle.Radians(value.X);
															result.Y = Angle.Radians(value.Y);
															result.Z = Angle.Radians(value.Z);
														return;
						}
											/// <summary>Create a <see cref="Angle3"/> from Radian values.</summary>
						public static void Radians( ref  Vector3d value , out Angle3 result) { 
							
															result.X = Angle.Radians(value.X);
															result.Y = Angle.Radians(value.Y);
															result.Z = Angle.Radians(value.Z);
														return;
						}
									
				/// <summary>Get the vector values of this <see cref="Angle3"/> in the Radian unit.</summary>
				public Vector3d InRadians { get { return new Vector3d( X.InRadians ,  Y.InRadians ,  Z.InRadians ); } }
							/// <summary>Create a <see cref="Angle3"/> from Degree values.</summary>
				public static Angle3 Degrees( double x ,  double y ,  double z ) { 
					return new Angle3( Angle.Degrees(x) ,  Angle.Degrees(y) ,  Angle.Degrees(z) );
				}
			
															/// <summary>Create a <see cref="Angle3"/> from Degree values.</summary>
						public static Angle3 Degrees( Vector3d value ) { 
							Angle3 result;
															result.X = Angle.Degrees(value.X);
															result.Y = Angle.Degrees(value.Y);
															result.Z = Angle.Degrees(value.Z);
														return result;
						}
											/// <summary>Create a <see cref="Angle3"/> from Degree values.</summary>
						public static Angle3 Degrees( ref  Vector3d value ) { 
							Angle3 result;
															result.X = Angle.Degrees(value.X);
															result.Y = Angle.Degrees(value.Y);
															result.Z = Angle.Degrees(value.Z);
														return result;
						}
																				/// <summary>Create a <see cref="Angle3"/> from Degree values.</summary>
						public static void Degrees( Vector3d value , out Angle3 result) { 
							
															result.X = Angle.Degrees(value.X);
															result.Y = Angle.Degrees(value.Y);
															result.Z = Angle.Degrees(value.Z);
														return;
						}
											/// <summary>Create a <see cref="Angle3"/> from Degree values.</summary>
						public static void Degrees( ref  Vector3d value , out Angle3 result) { 
							
															result.X = Angle.Degrees(value.X);
															result.Y = Angle.Degrees(value.Y);
															result.Z = Angle.Degrees(value.Z);
														return;
						}
									
				/// <summary>Get the vector values of this <see cref="Angle3"/> in the Degree unit.</summary>
				public Vector3d InDegrees { get { return new Vector3d( X.InDegrees ,  Y.InDegrees ,  Z.InDegrees ); } }
							/// <summary>Create a <see cref="Angle3"/> from Turn values.</summary>
				public static Angle3 Turns( double x ,  double y ,  double z ) { 
					return new Angle3( Angle.Turns(x) ,  Angle.Turns(y) ,  Angle.Turns(z) );
				}
			
															/// <summary>Create a <see cref="Angle3"/> from Turn values.</summary>
						public static Angle3 Turns( Vector3d value ) { 
							Angle3 result;
															result.X = Angle.Turns(value.X);
															result.Y = Angle.Turns(value.Y);
															result.Z = Angle.Turns(value.Z);
														return result;
						}
											/// <summary>Create a <see cref="Angle3"/> from Turn values.</summary>
						public static Angle3 Turns( ref  Vector3d value ) { 
							Angle3 result;
															result.X = Angle.Turns(value.X);
															result.Y = Angle.Turns(value.Y);
															result.Z = Angle.Turns(value.Z);
														return result;
						}
																				/// <summary>Create a <see cref="Angle3"/> from Turn values.</summary>
						public static void Turns( Vector3d value , out Angle3 result) { 
							
															result.X = Angle.Turns(value.X);
															result.Y = Angle.Turns(value.Y);
															result.Z = Angle.Turns(value.Z);
														return;
						}
											/// <summary>Create a <see cref="Angle3"/> from Turn values.</summary>
						public static void Turns( ref  Vector3d value , out Angle3 result) { 
							
															result.X = Angle.Turns(value.X);
															result.Y = Angle.Turns(value.Y);
															result.Z = Angle.Turns(value.Z);
														return;
						}
									
				/// <summary>Get the vector values of this <see cref="Angle3"/> in the Turn unit.</summary>
				public Vector3d InTurns { get { return new Vector3d( X.InTurns ,  Y.InTurns ,  Z.InTurns ); } }
						
			/// <summary>Get the absolute sum of all of the axes.</summary>
			public Angle AbsoluteSum { get { return (Angle)( X.AsUnsigned + Y.AsUnsigned + Z.AsUnsigned ); } }
			
			/// <summary>Get the positive of a <see cref="Angle3"/>, which is the same value.</summary>
			public static Angle3 operator +(Angle3 a) { return new Angle3( +a.X ,  +a.Y ,  +a.Z ); }

			/// <summary>Get the negative of a <see cref="Angle3"/>.</summary>
			public static Angle3 operator -(Angle3 a) { return new Angle3( -a.X ,  -a.Y ,  -a.Z ); }

			/// <summary>Add the <see cref="Angle3"/> values.</summary>
			public static Angle3 operator +(Angle3 a, Angle3 b) { return new Angle3( a.X + b.X ,  a.Y + b.Y ,  a.Z + b.Z ); }

			/// <summary>Subtract the <see cref="Angle3"/> values.</summary>
			public static Angle3 operator -(Angle3 a, Angle3 b) { return new Angle3( a.X - b.X ,  a.Y - b.Y ,  a.Z - b.Z ); }

			/// <summary>Multiply the <see cref="Angle3"/> value with the scalar.</summary>
			public static Angle3 operator *(Angle3 a, double b) { return new Angle3( a.X * b ,  a.Y * b ,  a.Z * b ); }

			/// <summary>Multiply the <see cref="Angle3"/> value with the scalar.</summary>
			public static Angle3 operator *(double a, Angle3 b) { return new Angle3( a * b.X ,  a * b.Y ,  a * b.Z ); }

			/// <summary>Divide the <see cref="Angle3"/> value by a scalar.</summary>
			public static Angle3 operator /(Angle3 a, double b) { return new Angle3( a.X / b ,  a.Y / b ,  a.Z / b ); }

			/// <summary>Divide the <see cref="Angle3"/> values, producing the vector magnitude difference between them.</summary>
			public static Vector3d operator /(Angle3 a, Angle3 b) { return new Vector3d( a.X / b.X ,  a.Y / b.Y ,  a.Z / b.Z ); }

			/// <summary>Modulo the <see cref="Angle3"/> values, producing the vector result.</summary>
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

	/// <summary>Get the zero value of the <see cref="Angle3"/>.</summary>
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

	/// <summary>If the other object is a <see cref="Angle3"/> of the same type, get whether this <see cref="Angle3"/> has equal factors as it; otherwise return false.</summary>
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
	
	/// <summary>Get whether the <see cref="Angle3"/> values are equal.</summary>
	public static bool operator ==(Angle3 a, Angle3 b) { return a.X == b.X&&a.Y == b.Y&&a.Z == b.Z; }

	/// <summary>Get whether the <see cref="Angle3"/> values are unequal.</summary>
	public static bool operator !=(Angle3 a, Angle3 b) { return a.X != b.X||a.Y != b.Y||a.Z != b.Z; }
		}
	}





