using System;
using System.Collections.Generic;
using Glare.Internal;

namespace Glare
{
	
	/// <summary>A two-dimensional vector type using <see cref="float"/> elements.</summary>
	[VectorTypeAttribute(typeof(float), 2, false)]
	public partial struct Vector2f : IEquatable<Vector2f>, IFormattable
	{
		#region Properties
		
							/// <summary>Compute the squared magnitude of the vector, which is the distance from the origin squared. Use <see cref="Magnitude"/> for the unsquared version, which is slower to compute.</summary>
				public float MagnitudeSquared { get { return  X * X + Y * Y ; } }
			
				/// <summary>Compute the magnitude of the vector, which is the distance from the origin.</summary>
				public float Magnitude
				{
					get
					{
						return (float)(							Math.Sqrt(
																	 X.Squared() 
								+									 Y.Squared() 
															));
					}
				}

									/// <summary>Get the normalized form of this vector, with a magnitude of one.</summary>
					public Vector2f Normalized { get { Vector2f result; Normalize(out result); return result; } }
							
							/// <summary>Get the product of multiplying all of the axes together.</summary>
				public float Product { get { return (float)( X * Y ); } }
			
			/// <summary>Get the sum of all of the axes.</summary>
			public float Sum { get { return (float)( X + Y ); } }

		#endregion Properties

		#region Constructors
		
			
			
							public static readonly Vector2f One = new Vector2f((float)1, (float)1);

									/// <summary>Get a normalized vector where X is 1 and all other axes are 0.</summary>
					public static readonly Vector2f UnitX = new Vector2f((float)1, (float)0);
									/// <summary>Get a normalized vector where Y is 1 and all other axes are 0.</summary>
					public static readonly Vector2f UnitY = new Vector2f((float)0, (float)1);
							

			// Constructors built up of smaller vectors.
			
			// Constructors from much smaller vectors.
			
		#endregion Constructors

		#region Methods

			// Methods that have joint ref forms.
							/// <summary>Compute the dot product of this and other vector, which is the sum of the product of each axis of each vector.</summary>
				/// <param name="other">The other vector to calculate a dot product with. </param>
				public float Dot( Vector2f other) { return  X * other.X + Y * other.Y ; }
							/// <summary>Compute the dot product of this and other vector, which is the sum of the product of each axis of each vector.</summary>
				/// <param name="other">The other vector to calculate a dot product with. The value of the parameter will not be changed; <c>ref</c> is used for optimisation only.</param>
				public float Dot( ref  Vector2f other) { return  X * other.X + Y * other.Y ; }
			
							public Vector2f Clamp( Vector2f min,  Vector2f max )
				{
					Vector2f result;
											result.X = X < min.X ? min.X : X > max.X ? max.X : X;
											result.Y = Y < min.Y ? min.Y : Y > max.Y ? max.Y : Y;
										return result;
				}
							public void Clamp( ref  Vector2f min,  ref  Vector2f max , out Vector2f result)
				{
					
											result.X = X < min.X ? min.X : X > max.X ? max.X : X;
											result.Y = Y < min.Y ? min.Y : Y > max.Y ? max.Y : Y;
										return;
				}
			
			// Floating-point methods
												/// <summary>Get the distance between the vectors.</summary>
					public float Distance( Vector2f other)
					{
						return (float)(							Math.Sqrt(
																	 X - other.X .Squared() 
								+									 Y - other.Y .Squared() 
															));
					}

					/// <summary>Get the squared distance between the vectors.</summary>
					public float DistanceSquared( Vector2f other)
					{
						return 
															(X - other.X).Squared()
							+								(Y - other.Y).Squared()
							;
					}
									/// <summary>Get the distance between the vectors.</summary>
					public float Distance( ref  Vector2f other)
					{
						return (float)(							Math.Sqrt(
																	 X - other.X .Squared() 
								+									 Y - other.Y .Squared() 
															));
					}

					/// <summary>Get the squared distance between the vectors.</summary>
					public float DistanceSquared( ref  Vector2f other)
					{
						return 
															(X - other.X).Squared()
							+								(Y - other.Y).Squared()
							;
					}
				
									/// <summary>Get the normalized form of this vector, which has a magnitude of one.</summary>
					public void Normalize(out Vector2f result)
					{
						double m = 1.0 / Math.Sqrt( X * X + Y * Y );
						 result.X = (float)(X * m);  result.Y = (float)(Y * m); 					}

					/// <summary>Normalize this vector in place, giving it a magnitude of one. An identity vector will become NaN.</summary>
					public void NormalizeInPlace()
					{
						float m = (float)(1.0 / Math.Sqrt( X * X + Y * Y ));
						 X *= m;  Y *= m; 					}
				
												
		#endregion Methods

			#region Fields

			/// <summary>The first axis of the <see cref="Vector2f"/>.</summary>
		public float X;
			/// <summary>The second axis of the <see cref="Vector2f"/>.</summary>
		public float Y;
	
	#endregion Fields

	#region Properties

	public static readonly Vector2f Zero = new Vector2f((float)0, (float)0);

	#endregion Properties

	#region Constructors

	/// <summary>Initialise a <see cref="Vector2f"/> from the provided values for each factor.</summary>
	public Vector2f(float x, float y)
	{
					X = x;
					Y = y;
			}

	/// <summary>Initialise a <see cref="Vector2f"/> from a list.</summary>
	public Vector2f(IList<float> list, int index = 0) : this(list[index + 0], list[index + 1]) { }

	/// <summary>Initialise a <see cref="Vector2f"/> from a single scalar that is applied to all factors.</summary>
	public Vector2f(float value) : this(value, value) { }

	#endregion Constructors

	#region Methods

	/// <summary>Get whether this <see cref="Vector2f"/> has equal factors as the other <see cref="Vector2f"/>.</summary>
	public bool Equals(Vector2f other) {
		return  X == other.X && Y == other.Y ;
	}

	/// <summary>If the other object is a <see cref="Vector2f"/> of the same type, get whether this <see cref="Vector2f"/> has equal factors as it; otherwise return false./summary>
	public override bool Equals(object other)
	{
		if(other is Vector2f)
			return Equals((Vector2f)other);
		return base.Equals(other);
	}

	/// <summary>Compute a hash code from combining the axes.</summary>
	public override int GetHashCode()
	{
		return  X.GetHashCode() ^ Y.GetHashCode() ;
	}

	/// <summary>Convert this <see cref="Vector2f"/> to a string of the form "X, Y".</summary>
	public string ToCommaSeparatedString(string format = null, IFormatProvider provider = null) {
		return X.ToString(format, provider) + ", " + Y.ToString(format, provider);
	}

	/// <summary>Convert this <see cref="Vector2f"/> to a string of the form "Vector2f(X, Y)".</summary>
	public override string ToString()
	{
		return ToString(null, null);
	}

	/// <summary>Convert this <see cref="Vector2f"/> to a string of the form "Vector2f(X, Y)".</summary>
	public string ToString(string format, IFormatProvider provider)
	{
		return "Vector2f(" + ToCommaSeparatedString(format, provider) + ")";
	}

	/// <summary>Convert this <see cref="Vector2f"/> to a string of the form "{X, Y".</summary>
	public string ToShortString(string format = null, IFormatProvider provider = null) { return "{" + ToCommaSeparatedString(format, provider) + "}"; }	

	#endregion Methods
	
	public static bool operator ==(Vector2f a, Vector2f b) { return a.X == b.X&&a.Y == b.Y; }

	public static bool operator !=(Vector2f a, Vector2f b) { return a.X != b.X||a.Y != b.Y; }

		#region Operators
		

			// Casting
							public static implicit operator Vector2d(Vector2f a) { return new Vector2d((double)a.X, (double)a.Y); }
					public static explicit operator Vector2i(Vector2f a) { return new Vector2i((int)a.X, (int)a.Y); }
					public static explicit operator Vector2ui(Vector2f a) { return new Vector2ui((uint)a.X, (uint)a.Y); }
					
				// Explicit casting to vectors with a lower order, trimming off axes.
							
			public static Vector2f operator +(Vector2f a) { return new Vector2f((float)(+a.X), (float)(+a.Y)); }
			public static Vector2f operator -(Vector2f a) { return new Vector2f((float)(-a.X), (float)(-a.Y)); }
								public static Vector2f operator +(Vector2f a, Vector2f b) { return new Vector2f((float)(a.X + b.X), (float)(a.Y + b.Y)); }
					public static Vector2f operator +(Vector2f a, float b) { return new Vector2f((float)(a.X + b), (float)(a.Y + b)); }
					public static Vector2f operator +(float a, Vector2f b) { return new Vector2f((float)(a + b.X), (float)(a + b.Y)); }
									public static Vector2f operator -(Vector2f a, Vector2f b) { return new Vector2f((float)(a.X - b.X), (float)(a.Y - b.Y)); }
					public static Vector2f operator -(Vector2f a, float b) { return new Vector2f((float)(a.X - b), (float)(a.Y - b)); }
					public static Vector2f operator -(float a, Vector2f b) { return new Vector2f((float)(a - b.X), (float)(a - b.Y)); }
									public static Vector2f operator *(Vector2f a, Vector2f b) { return new Vector2f((float)(a.X * b.X), (float)(a.Y * b.Y)); }
					public static Vector2f operator *(Vector2f a, float b) { return new Vector2f((float)(a.X * b), (float)(a.Y * b)); }
					public static Vector2f operator *(float a, Vector2f b) { return new Vector2f((float)(a * b.X), (float)(a * b.Y)); }
									public static Vector2f operator /(Vector2f a, Vector2f b) { return new Vector2f((float)(a.X / b.X), (float)(a.Y / b.Y)); }
					public static Vector2f operator /(Vector2f a, float b) { return new Vector2f((float)(a.X / b), (float)(a.Y / b)); }
					public static Vector2f operator /(float a, Vector2f b) { return new Vector2f((float)(a / b.X), (float)(a / b.Y)); }
									public static Vector2f operator %(Vector2f a, Vector2f b) { return new Vector2f((float)(a.X % b.X), (float)(a.Y % b.Y)); }
					public static Vector2f operator %(Vector2f a, float b) { return new Vector2f((float)(a.X % b), (float)(a.Y % b)); }
					public static Vector2f operator %(float a, Vector2f b) { return new Vector2f((float)(a % b.X), (float)(a % b.Y)); }
				
		#endregion Operators
	}
	
	
	/// <summary>A two-dimensional vector type using <see cref="double"/> elements.</summary>
	[VectorTypeAttribute(typeof(double), 2, false)]
	public partial struct Vector2d : IEquatable<Vector2d>, IFormattable
	{
		#region Properties
		
							/// <summary>Compute the squared magnitude of the vector, which is the distance from the origin squared. Use <see cref="Magnitude"/> for the unsquared version, which is slower to compute.</summary>
				public double MagnitudeSquared { get { return  X * X + Y * Y ; } }
			
				/// <summary>Compute the magnitude of the vector, which is the distance from the origin.</summary>
				public double Magnitude
				{
					get
					{
						return (double)(							Math.Sqrt(
																	 X.Squared() 
								+									 Y.Squared() 
															));
					}
				}

									/// <summary>Get the normalized form of this vector, with a magnitude of one.</summary>
					public Vector2d Normalized { get { Vector2d result; Normalize(out result); return result; } }
							
							/// <summary>Get the product of multiplying all of the axes together.</summary>
				public double Product { get { return (double)( X * Y ); } }
			
			/// <summary>Get the sum of all of the axes.</summary>
			public double Sum { get { return (double)( X + Y ); } }

		#endregion Properties

		#region Constructors
		
			
			
							public static readonly Vector2d One = new Vector2d((double)1, (double)1);

									/// <summary>Get a normalized vector where X is 1 and all other axes are 0.</summary>
					public static readonly Vector2d UnitX = new Vector2d((double)1, (double)0);
									/// <summary>Get a normalized vector where Y is 1 and all other axes are 0.</summary>
					public static readonly Vector2d UnitY = new Vector2d((double)0, (double)1);
							

			// Constructors built up of smaller vectors.
			
			// Constructors from much smaller vectors.
			
		#endregion Constructors

		#region Methods

			// Methods that have joint ref forms.
							/// <summary>Compute the dot product of this and other vector, which is the sum of the product of each axis of each vector.</summary>
				/// <param name="other">The other vector to calculate a dot product with. </param>
				public double Dot( Vector2d other) { return  X * other.X + Y * other.Y ; }
							/// <summary>Compute the dot product of this and other vector, which is the sum of the product of each axis of each vector.</summary>
				/// <param name="other">The other vector to calculate a dot product with. The value of the parameter will not be changed; <c>ref</c> is used for optimisation only.</param>
				public double Dot( ref  Vector2d other) { return  X * other.X + Y * other.Y ; }
			
							public Vector2d Clamp( Vector2d min,  Vector2d max )
				{
					Vector2d result;
											result.X = X < min.X ? min.X : X > max.X ? max.X : X;
											result.Y = Y < min.Y ? min.Y : Y > max.Y ? max.Y : Y;
										return result;
				}
							public void Clamp( ref  Vector2d min,  ref  Vector2d max , out Vector2d result)
				{
					
											result.X = X < min.X ? min.X : X > max.X ? max.X : X;
											result.Y = Y < min.Y ? min.Y : Y > max.Y ? max.Y : Y;
										return;
				}
			
			// Floating-point methods
												/// <summary>Get the distance between the vectors.</summary>
					public double Distance( Vector2d other)
					{
						return (double)(							Math.Sqrt(
																	 X - other.X .Squared() 
								+									 Y - other.Y .Squared() 
															));
					}

					/// <summary>Get the squared distance between the vectors.</summary>
					public double DistanceSquared( Vector2d other)
					{
						return 
															(X - other.X).Squared()
							+								(Y - other.Y).Squared()
							;
					}
									/// <summary>Get the distance between the vectors.</summary>
					public double Distance( ref  Vector2d other)
					{
						return (double)(							Math.Sqrt(
																	 X - other.X .Squared() 
								+									 Y - other.Y .Squared() 
															));
					}

					/// <summary>Get the squared distance between the vectors.</summary>
					public double DistanceSquared( ref  Vector2d other)
					{
						return 
															(X - other.X).Squared()
							+								(Y - other.Y).Squared()
							;
					}
				
									/// <summary>Get the normalized form of this vector, which has a magnitude of one.</summary>
					public void Normalize(out Vector2d result)
					{
						double m = 1.0 / Math.Sqrt( X * X + Y * Y );
						 result.X = (double)(X * m);  result.Y = (double)(Y * m); 					}

					/// <summary>Normalize this vector in place, giving it a magnitude of one. An identity vector will become NaN.</summary>
					public void NormalizeInPlace()
					{
						double m = (double)(1.0 / Math.Sqrt( X * X + Y * Y ));
						 X *= m;  Y *= m; 					}
				
												
		#endregion Methods

			#region Fields

			/// <summary>The first axis of the <see cref="Vector2d"/>.</summary>
		public double X;
			/// <summary>The second axis of the <see cref="Vector2d"/>.</summary>
		public double Y;
	
	#endregion Fields

	#region Properties

	public static readonly Vector2d Zero = new Vector2d((double)0, (double)0);

	#endregion Properties

	#region Constructors

	/// <summary>Initialise a <see cref="Vector2d"/> from the provided values for each factor.</summary>
	public Vector2d(double x, double y)
	{
					X = x;
					Y = y;
			}

	/// <summary>Initialise a <see cref="Vector2d"/> from a list.</summary>
	public Vector2d(IList<double> list, int index = 0) : this(list[index + 0], list[index + 1]) { }

	/// <summary>Initialise a <see cref="Vector2d"/> from a single scalar that is applied to all factors.</summary>
	public Vector2d(double value) : this(value, value) { }

	#endregion Constructors

	#region Methods

	/// <summary>Get whether this <see cref="Vector2d"/> has equal factors as the other <see cref="Vector2d"/>.</summary>
	public bool Equals(Vector2d other) {
		return  X == other.X && Y == other.Y ;
	}

	/// <summary>If the other object is a <see cref="Vector2d"/> of the same type, get whether this <see cref="Vector2d"/> has equal factors as it; otherwise return false./summary>
	public override bool Equals(object other)
	{
		if(other is Vector2d)
			return Equals((Vector2d)other);
		return base.Equals(other);
	}

	/// <summary>Compute a hash code from combining the axes.</summary>
	public override int GetHashCode()
	{
		return  X.GetHashCode() ^ Y.GetHashCode() ;
	}

	/// <summary>Convert this <see cref="Vector2d"/> to a string of the form "X, Y".</summary>
	public string ToCommaSeparatedString(string format = null, IFormatProvider provider = null) {
		return X.ToString(format, provider) + ", " + Y.ToString(format, provider);
	}

	/// <summary>Convert this <see cref="Vector2d"/> to a string of the form "Vector2d(X, Y)".</summary>
	public override string ToString()
	{
		return ToString(null, null);
	}

	/// <summary>Convert this <see cref="Vector2d"/> to a string of the form "Vector2d(X, Y)".</summary>
	public string ToString(string format, IFormatProvider provider)
	{
		return "Vector2d(" + ToCommaSeparatedString(format, provider) + ")";
	}

	/// <summary>Convert this <see cref="Vector2d"/> to a string of the form "{X, Y".</summary>
	public string ToShortString(string format = null, IFormatProvider provider = null) { return "{" + ToCommaSeparatedString(format, provider) + "}"; }	

	#endregion Methods
	
	public static bool operator ==(Vector2d a, Vector2d b) { return a.X == b.X&&a.Y == b.Y; }

	public static bool operator !=(Vector2d a, Vector2d b) { return a.X != b.X||a.Y != b.Y; }

		#region Operators
		

			// Casting
							public static explicit operator Vector2f(Vector2d a) { return new Vector2f((float)a.X, (float)a.Y); }
					public static explicit operator Vector2i(Vector2d a) { return new Vector2i((int)a.X, (int)a.Y); }
					public static explicit operator Vector2ui(Vector2d a) { return new Vector2ui((uint)a.X, (uint)a.Y); }
					
				// Explicit casting to vectors with a lower order, trimming off axes.
							
			public static Vector2d operator +(Vector2d a) { return new Vector2d((double)(+a.X), (double)(+a.Y)); }
			public static Vector2d operator -(Vector2d a) { return new Vector2d((double)(-a.X), (double)(-a.Y)); }
								public static Vector2d operator +(Vector2d a, Vector2d b) { return new Vector2d((double)(a.X + b.X), (double)(a.Y + b.Y)); }
					public static Vector2d operator +(Vector2d a, double b) { return new Vector2d((double)(a.X + b), (double)(a.Y + b)); }
					public static Vector2d operator +(double a, Vector2d b) { return new Vector2d((double)(a + b.X), (double)(a + b.Y)); }
									public static Vector2d operator -(Vector2d a, Vector2d b) { return new Vector2d((double)(a.X - b.X), (double)(a.Y - b.Y)); }
					public static Vector2d operator -(Vector2d a, double b) { return new Vector2d((double)(a.X - b), (double)(a.Y - b)); }
					public static Vector2d operator -(double a, Vector2d b) { return new Vector2d((double)(a - b.X), (double)(a - b.Y)); }
									public static Vector2d operator *(Vector2d a, Vector2d b) { return new Vector2d((double)(a.X * b.X), (double)(a.Y * b.Y)); }
					public static Vector2d operator *(Vector2d a, double b) { return new Vector2d((double)(a.X * b), (double)(a.Y * b)); }
					public static Vector2d operator *(double a, Vector2d b) { return new Vector2d((double)(a * b.X), (double)(a * b.Y)); }
									public static Vector2d operator /(Vector2d a, Vector2d b) { return new Vector2d((double)(a.X / b.X), (double)(a.Y / b.Y)); }
					public static Vector2d operator /(Vector2d a, double b) { return new Vector2d((double)(a.X / b), (double)(a.Y / b)); }
					public static Vector2d operator /(double a, Vector2d b) { return new Vector2d((double)(a / b.X), (double)(a / b.Y)); }
									public static Vector2d operator %(Vector2d a, Vector2d b) { return new Vector2d((double)(a.X % b.X), (double)(a.Y % b.Y)); }
					public static Vector2d operator %(Vector2d a, double b) { return new Vector2d((double)(a.X % b), (double)(a.Y % b)); }
					public static Vector2d operator %(double a, Vector2d b) { return new Vector2d((double)(a % b.X), (double)(a % b.Y)); }
				
		#endregion Operators
	}
	
	
	/// <summary>A two-dimensional vector type using <see cref="int"/> elements.</summary>
	[VectorTypeAttribute(typeof(int), 2, false)]
	public partial struct Vector2i : IEquatable<Vector2i>, IFormattable
	{
		#region Properties
		
							/// <summary>Compute the squared magnitude of the vector, which is the distance from the origin squared. Use <see cref="Magnitude"/> for the unsquared version, which is slower to compute.</summary>
				public int MagnitudeSquared { get { return  X * X + Y * Y ; } }
			
				/// <summary>Compute the magnitude of the vector, which is the distance from the origin.</summary>
				public int Magnitude
				{
					get
					{
						return (int)(							Math.Sqrt(
																	 X.Squared() 
								+									 Y.Squared() 
															));
					}
				}

									/// <summary>Get the normalized form of this vector, with a magnitude of one.</summary>
					public Vector2i Normalized { get { Vector2i result; Normalize(out result); return result; } }
							
							/// <summary>Get the product of multiplying all of the axes together.</summary>
				public int Product { get { return (int)( X * Y ); } }
			
			/// <summary>Get the sum of all of the axes.</summary>
			public int Sum { get { return (int)( X + Y ); } }

		#endregion Properties

		#region Constructors
		
			
			
							public static readonly Vector2i One = new Vector2i((int)1, (int)1);

									/// <summary>Get a normalized vector where X is 1 and all other axes are 0.</summary>
					public static readonly Vector2i UnitX = new Vector2i((int)1, (int)0);
									/// <summary>Get a normalized vector where Y is 1 and all other axes are 0.</summary>
					public static readonly Vector2i UnitY = new Vector2i((int)0, (int)1);
							

			// Constructors built up of smaller vectors.
			
			// Constructors from much smaller vectors.
			
		#endregion Constructors

		#region Methods

			// Methods that have joint ref forms.
							/// <summary>Compute the dot product of this and other vector, which is the sum of the product of each axis of each vector.</summary>
				/// <param name="other">The other vector to calculate a dot product with. </param>
				public int Dot( Vector2i other) { return  X * other.X + Y * other.Y ; }
							/// <summary>Compute the dot product of this and other vector, which is the sum of the product of each axis of each vector.</summary>
				/// <param name="other">The other vector to calculate a dot product with. The value of the parameter will not be changed; <c>ref</c> is used for optimisation only.</param>
				public int Dot( ref  Vector2i other) { return  X * other.X + Y * other.Y ; }
			
							public Vector2i Clamp( Vector2i min,  Vector2i max )
				{
					Vector2i result;
											result.X = X < min.X ? min.X : X > max.X ? max.X : X;
											result.Y = Y < min.Y ? min.Y : Y > max.Y ? max.Y : Y;
										return result;
				}
							public void Clamp( ref  Vector2i min,  ref  Vector2i max , out Vector2i result)
				{
					
											result.X = X < min.X ? min.X : X > max.X ? max.X : X;
											result.Y = Y < min.Y ? min.Y : Y > max.Y ? max.Y : Y;
										return;
				}
			
			// Floating-point methods
												/// <summary>Get the distance between the vectors.</summary>
					public int Distance( Vector2i other)
					{
						return (int)(							Math.Sqrt(
																	 X - other.X .Squared() 
								+									 Y - other.Y .Squared() 
															));
					}

					/// <summary>Get the squared distance between the vectors.</summary>
					public int DistanceSquared( Vector2i other)
					{
						return 
															(X - other.X).Squared()
							+								(Y - other.Y).Squared()
							;
					}
									/// <summary>Get the distance between the vectors.</summary>
					public int Distance( ref  Vector2i other)
					{
						return (int)(							Math.Sqrt(
																	 X - other.X .Squared() 
								+									 Y - other.Y .Squared() 
															));
					}

					/// <summary>Get the squared distance between the vectors.</summary>
					public int DistanceSquared( ref  Vector2i other)
					{
						return 
															(X - other.X).Squared()
							+								(Y - other.Y).Squared()
							;
					}
				
									/// <summary>Get the normalized form of this vector, which has a magnitude of one.</summary>
					public void Normalize(out Vector2i result)
					{
						double m = 1.0 / Math.Sqrt( X * X + Y * Y );
						 result.X = (int)(X * m);  result.Y = (int)(Y * m); 					}

					/// <summary>Normalize this vector in place, giving it a magnitude of one. An identity vector will become NaN.</summary>
					public void NormalizeInPlace()
					{
						int m = (int)(1.0 / Math.Sqrt( X * X + Y * Y ));
						 X *= m;  Y *= m; 					}
				
												
		#endregion Methods

			#region Fields

			/// <summary>The first axis of the <see cref="Vector2i"/>.</summary>
		public int X;
			/// <summary>The second axis of the <see cref="Vector2i"/>.</summary>
		public int Y;
	
	#endregion Fields

	#region Properties

	public static readonly Vector2i Zero = new Vector2i((int)0, (int)0);

	#endregion Properties

	#region Constructors

	/// <summary>Initialise a <see cref="Vector2i"/> from the provided values for each factor.</summary>
	public Vector2i(int x, int y)
	{
					X = x;
					Y = y;
			}

	/// <summary>Initialise a <see cref="Vector2i"/> from a list.</summary>
	public Vector2i(IList<int> list, int index = 0) : this(list[index + 0], list[index + 1]) { }

	/// <summary>Initialise a <see cref="Vector2i"/> from a single scalar that is applied to all factors.</summary>
	public Vector2i(int value) : this(value, value) { }

	#endregion Constructors

	#region Methods

	/// <summary>Get whether this <see cref="Vector2i"/> has equal factors as the other <see cref="Vector2i"/>.</summary>
	public bool Equals(Vector2i other) {
		return  X == other.X && Y == other.Y ;
	}

	/// <summary>If the other object is a <see cref="Vector2i"/> of the same type, get whether this <see cref="Vector2i"/> has equal factors as it; otherwise return false./summary>
	public override bool Equals(object other)
	{
		if(other is Vector2i)
			return Equals((Vector2i)other);
		return base.Equals(other);
	}

	/// <summary>Compute a hash code from combining the axes.</summary>
	public override int GetHashCode()
	{
		return  X.GetHashCode() ^ Y.GetHashCode() ;
	}

	/// <summary>Convert this <see cref="Vector2i"/> to a string of the form "X, Y".</summary>
	public string ToCommaSeparatedString(string format = null, IFormatProvider provider = null) {
		return X.ToString(format, provider) + ", " + Y.ToString(format, provider);
	}

	/// <summary>Convert this <see cref="Vector2i"/> to a string of the form "Vector2i(X, Y)".</summary>
	public override string ToString()
	{
		return ToString(null, null);
	}

	/// <summary>Convert this <see cref="Vector2i"/> to a string of the form "Vector2i(X, Y)".</summary>
	public string ToString(string format, IFormatProvider provider)
	{
		return "Vector2i(" + ToCommaSeparatedString(format, provider) + ")";
	}

	/// <summary>Convert this <see cref="Vector2i"/> to a string of the form "{X, Y".</summary>
	public string ToShortString(string format = null, IFormatProvider provider = null) { return "{" + ToCommaSeparatedString(format, provider) + "}"; }	

	#endregion Methods
	
	public static bool operator ==(Vector2i a, Vector2i b) { return a.X == b.X&&a.Y == b.Y; }

	public static bool operator !=(Vector2i a, Vector2i b) { return a.X != b.X||a.Y != b.Y; }

		#region Operators
		

			// Casting
							public static implicit operator Vector2f(Vector2i a) { return new Vector2f((float)a.X, (float)a.Y); }
					public static implicit operator Vector2d(Vector2i a) { return new Vector2d((double)a.X, (double)a.Y); }
					public static explicit operator Vector2ui(Vector2i a) { return new Vector2ui((uint)a.X, (uint)a.Y); }
					
				// Explicit casting to vectors with a lower order, trimming off axes.
							
			public static Vector2i operator +(Vector2i a) { return new Vector2i((int)(+a.X), (int)(+a.Y)); }
			public static Vector2i operator -(Vector2i a) { return new Vector2i((int)(-a.X), (int)(-a.Y)); }
								public static Vector2i operator +(Vector2i a, Vector2i b) { return new Vector2i((int)(a.X + b.X), (int)(a.Y + b.Y)); }
					public static Vector2i operator +(Vector2i a, int b) { return new Vector2i((int)(a.X + b), (int)(a.Y + b)); }
					public static Vector2i operator +(int a, Vector2i b) { return new Vector2i((int)(a + b.X), (int)(a + b.Y)); }
									public static Vector2i operator -(Vector2i a, Vector2i b) { return new Vector2i((int)(a.X - b.X), (int)(a.Y - b.Y)); }
					public static Vector2i operator -(Vector2i a, int b) { return new Vector2i((int)(a.X - b), (int)(a.Y - b)); }
					public static Vector2i operator -(int a, Vector2i b) { return new Vector2i((int)(a - b.X), (int)(a - b.Y)); }
									public static Vector2i operator *(Vector2i a, Vector2i b) { return new Vector2i((int)(a.X * b.X), (int)(a.Y * b.Y)); }
					public static Vector2i operator *(Vector2i a, int b) { return new Vector2i((int)(a.X * b), (int)(a.Y * b)); }
					public static Vector2i operator *(int a, Vector2i b) { return new Vector2i((int)(a * b.X), (int)(a * b.Y)); }
									public static Vector2i operator /(Vector2i a, Vector2i b) { return new Vector2i((int)(a.X / b.X), (int)(a.Y / b.Y)); }
					public static Vector2i operator /(Vector2i a, int b) { return new Vector2i((int)(a.X / b), (int)(a.Y / b)); }
					public static Vector2i operator /(int a, Vector2i b) { return new Vector2i((int)(a / b.X), (int)(a / b.Y)); }
									public static Vector2i operator %(Vector2i a, Vector2i b) { return new Vector2i((int)(a.X % b.X), (int)(a.Y % b.Y)); }
					public static Vector2i operator %(Vector2i a, int b) { return new Vector2i((int)(a.X % b), (int)(a.Y % b)); }
					public static Vector2i operator %(int a, Vector2i b) { return new Vector2i((int)(a % b.X), (int)(a % b.Y)); }
				
		#endregion Operators
	}
	
	
	/// <summary>A two-dimensional vector type using <see cref="uint"/> elements.</summary>
	[VectorTypeAttribute(typeof(uint), 2, false)]
	public partial struct Vector2ui : IEquatable<Vector2ui>, IFormattable
	{
		#region Properties
		
			
							/// <summary>Get the product of multiplying all of the axes together.</summary>
				public uint Product { get { return (uint)( X * Y ); } }
			
			/// <summary>Get the sum of all of the axes.</summary>
			public uint Sum { get { return (uint)( X + Y ); } }

		#endregion Properties

		#region Constructors
		
			
			
							public static readonly Vector2ui One = new Vector2ui((uint)1, (uint)1);

									/// <summary>Get a normalized vector where X is 1 and all other axes are 0.</summary>
					public static readonly Vector2ui UnitX = new Vector2ui((uint)1, (uint)0);
									/// <summary>Get a normalized vector where Y is 1 and all other axes are 0.</summary>
					public static readonly Vector2ui UnitY = new Vector2ui((uint)0, (uint)1);
							

			// Constructors built up of smaller vectors.
			
			// Constructors from much smaller vectors.
			
		#endregion Constructors

		#region Methods

			// Methods that have joint ref forms.
							/// <summary>Compute the dot product of this and other vector, which is the sum of the product of each axis of each vector.</summary>
				/// <param name="other">The other vector to calculate a dot product with. </param>
				public uint Dot( Vector2ui other) { return  X * other.X + Y * other.Y ; }
							/// <summary>Compute the dot product of this and other vector, which is the sum of the product of each axis of each vector.</summary>
				/// <param name="other">The other vector to calculate a dot product with. The value of the parameter will not be changed; <c>ref</c> is used for optimisation only.</param>
				public uint Dot( ref  Vector2ui other) { return  X * other.X + Y * other.Y ; }
			
							public Vector2ui Clamp( Vector2ui min,  Vector2ui max )
				{
					Vector2ui result;
											result.X = X < min.X ? min.X : X > max.X ? max.X : X;
											result.Y = Y < min.Y ? min.Y : Y > max.Y ? max.Y : Y;
										return result;
				}
							public void Clamp( ref  Vector2ui min,  ref  Vector2ui max , out Vector2ui result)
				{
					
											result.X = X < min.X ? min.X : X > max.X ? max.X : X;
											result.Y = Y < min.Y ? min.Y : Y > max.Y ? max.Y : Y;
										return;
				}
			
			// Floating-point methods
			
		#endregion Methods

			#region Fields

			/// <summary>The first axis of the <see cref="Vector2ui"/>.</summary>
		public uint X;
			/// <summary>The second axis of the <see cref="Vector2ui"/>.</summary>
		public uint Y;
	
	#endregion Fields

	#region Properties

	public static readonly Vector2ui Zero = new Vector2ui((uint)0, (uint)0);

	#endregion Properties

	#region Constructors

	/// <summary>Initialise a <see cref="Vector2ui"/> from the provided values for each factor.</summary>
	public Vector2ui(uint x, uint y)
	{
					X = x;
					Y = y;
			}

	/// <summary>Initialise a <see cref="Vector2ui"/> from a list.</summary>
	public Vector2ui(IList<uint> list, int index = 0) : this(list[index + 0], list[index + 1]) { }

	/// <summary>Initialise a <see cref="Vector2ui"/> from a single scalar that is applied to all factors.</summary>
	public Vector2ui(uint value) : this(value, value) { }

	#endregion Constructors

	#region Methods

	/// <summary>Get whether this <see cref="Vector2ui"/> has equal factors as the other <see cref="Vector2ui"/>.</summary>
	public bool Equals(Vector2ui other) {
		return  X == other.X && Y == other.Y ;
	}

	/// <summary>If the other object is a <see cref="Vector2ui"/> of the same type, get whether this <see cref="Vector2ui"/> has equal factors as it; otherwise return false./summary>
	public override bool Equals(object other)
	{
		if(other is Vector2ui)
			return Equals((Vector2ui)other);
		return base.Equals(other);
	}

	/// <summary>Compute a hash code from combining the axes.</summary>
	public override int GetHashCode()
	{
		return  X.GetHashCode() ^ Y.GetHashCode() ;
	}

	/// <summary>Convert this <see cref="Vector2ui"/> to a string of the form "X, Y".</summary>
	public string ToCommaSeparatedString(string format = null, IFormatProvider provider = null) {
		return X.ToString(format, provider) + ", " + Y.ToString(format, provider);
	}

	/// <summary>Convert this <see cref="Vector2ui"/> to a string of the form "Vector2ui(X, Y)".</summary>
	public override string ToString()
	{
		return ToString(null, null);
	}

	/// <summary>Convert this <see cref="Vector2ui"/> to a string of the form "Vector2ui(X, Y)".</summary>
	public string ToString(string format, IFormatProvider provider)
	{
		return "Vector2ui(" + ToCommaSeparatedString(format, provider) + ")";
	}

	/// <summary>Convert this <see cref="Vector2ui"/> to a string of the form "{X, Y".</summary>
	public string ToShortString(string format = null, IFormatProvider provider = null) { return "{" + ToCommaSeparatedString(format, provider) + "}"; }	

	#endregion Methods
	
	public static bool operator ==(Vector2ui a, Vector2ui b) { return a.X == b.X&&a.Y == b.Y; }

	public static bool operator !=(Vector2ui a, Vector2ui b) { return a.X != b.X||a.Y != b.Y; }

		#region Operators
		

			// Casting
							public static implicit operator Vector2f(Vector2ui a) { return new Vector2f((float)a.X, (float)a.Y); }
					public static implicit operator Vector2d(Vector2ui a) { return new Vector2d((double)a.X, (double)a.Y); }
					public static explicit operator Vector2i(Vector2ui a) { return new Vector2i((int)a.X, (int)a.Y); }
					
				// Explicit casting to vectors with a lower order, trimming off axes.
							
			public static Vector2ui operator +(Vector2ui a) { return new Vector2ui((uint)(+a.X), (uint)(+a.Y)); }
			public static Vector2ui operator -(Vector2ui a) { return new Vector2ui((uint)(-a.X), (uint)(-a.Y)); }
								public static Vector2ui operator +(Vector2ui a, Vector2ui b) { return new Vector2ui((uint)(a.X + b.X), (uint)(a.Y + b.Y)); }
					public static Vector2ui operator +(Vector2ui a, uint b) { return new Vector2ui((uint)(a.X + b), (uint)(a.Y + b)); }
					public static Vector2ui operator +(uint a, Vector2ui b) { return new Vector2ui((uint)(a + b.X), (uint)(a + b.Y)); }
									public static Vector2ui operator -(Vector2ui a, Vector2ui b) { return new Vector2ui((uint)(a.X - b.X), (uint)(a.Y - b.Y)); }
					public static Vector2ui operator -(Vector2ui a, uint b) { return new Vector2ui((uint)(a.X - b), (uint)(a.Y - b)); }
					public static Vector2ui operator -(uint a, Vector2ui b) { return new Vector2ui((uint)(a - b.X), (uint)(a - b.Y)); }
									public static Vector2ui operator *(Vector2ui a, Vector2ui b) { return new Vector2ui((uint)(a.X * b.X), (uint)(a.Y * b.Y)); }
					public static Vector2ui operator *(Vector2ui a, uint b) { return new Vector2ui((uint)(a.X * b), (uint)(a.Y * b)); }
					public static Vector2ui operator *(uint a, Vector2ui b) { return new Vector2ui((uint)(a * b.X), (uint)(a * b.Y)); }
									public static Vector2ui operator /(Vector2ui a, Vector2ui b) { return new Vector2ui((uint)(a.X / b.X), (uint)(a.Y / b.Y)); }
					public static Vector2ui operator /(Vector2ui a, uint b) { return new Vector2ui((uint)(a.X / b), (uint)(a.Y / b)); }
					public static Vector2ui operator /(uint a, Vector2ui b) { return new Vector2ui((uint)(a / b.X), (uint)(a / b.Y)); }
									public static Vector2ui operator %(Vector2ui a, Vector2ui b) { return new Vector2ui((uint)(a.X % b.X), (uint)(a.Y % b.Y)); }
					public static Vector2ui operator %(Vector2ui a, uint b) { return new Vector2ui((uint)(a.X % b), (uint)(a.Y % b)); }
					public static Vector2ui operator %(uint a, Vector2ui b) { return new Vector2ui((uint)(a % b.X), (uint)(a % b.Y)); }
									public static Vector2ui operator &(Vector2ui a, Vector2ui b) { return new Vector2ui((uint)(a.X & b.X), (uint)(a.Y & b.Y)); }
					public static Vector2ui operator &(Vector2ui a, uint b) { return new Vector2ui((uint)(a.X & b), (uint)(a.Y & b)); }
					public static Vector2ui operator &(uint a, Vector2ui b) { return new Vector2ui((uint)(a & b.X), (uint)(a & b.Y)); }
									public static Vector2ui operator |(Vector2ui a, Vector2ui b) { return new Vector2ui((uint)(a.X | b.X), (uint)(a.Y | b.Y)); }
					public static Vector2ui operator |(Vector2ui a, uint b) { return new Vector2ui((uint)(a.X | b), (uint)(a.Y | b)); }
					public static Vector2ui operator |(uint a, Vector2ui b) { return new Vector2ui((uint)(a | b.X), (uint)(a | b.Y)); }
									public static Vector2ui operator ^(Vector2ui a, Vector2ui b) { return new Vector2ui((uint)(a.X ^ b.X), (uint)(a.Y ^ b.Y)); }
					public static Vector2ui operator ^(Vector2ui a, uint b) { return new Vector2ui((uint)(a.X ^ b), (uint)(a.Y ^ b)); }
					public static Vector2ui operator ^(uint a, Vector2ui b) { return new Vector2ui((uint)(a ^ b.X), (uint)(a ^ b.Y)); }
										public static Vector2ui operator <<(Vector2ui a, int b) { return new Vector2ui((uint)(a.X << b), (uint)(a.Y << b)); }
											public static Vector2ui operator >>(Vector2ui a, int b) { return new Vector2ui((uint)(a.X >> b), (uint)(a.Y >> b)); }
					
		#endregion Operators
	}
	
	
	/// <summary>A three-dimensional vector type using <see cref="Length"/> elements.</summary>
	[VectorTypeAttribute(typeof(Length), 3, false)]
	public partial struct Vector3 : IEquatable<Vector3>, IFormattable
	{
		#region Properties
		
							/// <summary>Compute the squared magnitude of the vector, which is the distance from the origin squared. Use <see cref="Magnitude"/> for the unsquared version, which is slower to compute.</summary>
				public Area MagnitudeSquared { get { return  X * X + Y * Y + Z * Z ; } }
			
				/// <summary>Compute the magnitude of the vector, which is the distance from the origin.</summary>
				public Length Magnitude
				{
					get
					{
						return Length.Universal(							Math.Sqrt(
																	( X).InUniversal.Squared() 
								+									( Y).InUniversal.Squared() 
								+									( Z).InUniversal.Squared() 
															));
					}
				}

							
			
			/// <summary>Get the sum of all of the axes.</summary>
			public Length Sum { get { return (Length)( X + Y + Z ); } }

		#endregion Properties

		#region Constructors
		
			
												public static Vector3 Centimetres( double x ,  double y ,  double z )
					{
						return new Vector3( Length.Centimetres(x) ,  Length.Centimetres(y) ,  Length.Centimetres(z) );
					}
				
					public static Vector3 Centimetres(double value)
					{
						return new Vector3(Length.Centimetres(value));
					}

											public static Vector3 Centimetres( Vector3d value)
						{
							return new Vector3(								Length.Centimetres(value.X)
							, 								Length.Centimetres(value.Y)
							, 								Length.Centimetres(value.Z)
							);
						}
											public static Vector3 Centimetres( ref  Vector3d value)
						{
							return new Vector3(								Length.Centimetres(value.X)
							, 								Length.Centimetres(value.Y)
							, 								Length.Centimetres(value.Z)
							);
						}
									

					public Vector3d InCentimetres
					{
						get
						{
							return new Vector3d(								X.InCentimetres
							, 								Y.InCentimetres
							, 								Z.InCentimetres
							);
						}
					}
									public static Vector3 Feet( double x ,  double y ,  double z )
					{
						return new Vector3( Length.Feet(x) ,  Length.Feet(y) ,  Length.Feet(z) );
					}
				
					public static Vector3 Feet(double value)
					{
						return new Vector3(Length.Feet(value));
					}

											public static Vector3 Feet( Vector3d value)
						{
							return new Vector3(								Length.Feet(value.X)
							, 								Length.Feet(value.Y)
							, 								Length.Feet(value.Z)
							);
						}
											public static Vector3 Feet( ref  Vector3d value)
						{
							return new Vector3(								Length.Feet(value.X)
							, 								Length.Feet(value.Y)
							, 								Length.Feet(value.Z)
							);
						}
									

					public Vector3d InFeet
					{
						get
						{
							return new Vector3d(								X.InFeet
							, 								Y.InFeet
							, 								Z.InFeet
							);
						}
					}
									public static Vector3 Inches( double x ,  double y ,  double z )
					{
						return new Vector3( Length.Inches(x) ,  Length.Inches(y) ,  Length.Inches(z) );
					}
				
					public static Vector3 Inches(double value)
					{
						return new Vector3(Length.Inches(value));
					}

											public static Vector3 Inches( Vector3d value)
						{
							return new Vector3(								Length.Inches(value.X)
							, 								Length.Inches(value.Y)
							, 								Length.Inches(value.Z)
							);
						}
											public static Vector3 Inches( ref  Vector3d value)
						{
							return new Vector3(								Length.Inches(value.X)
							, 								Length.Inches(value.Y)
							, 								Length.Inches(value.Z)
							);
						}
									

					public Vector3d InInches
					{
						get
						{
							return new Vector3d(								X.InInches
							, 								Y.InInches
							, 								Z.InInches
							);
						}
					}
									public static Vector3 Kilometres( double x ,  double y ,  double z )
					{
						return new Vector3( Length.Kilometres(x) ,  Length.Kilometres(y) ,  Length.Kilometres(z) );
					}
				
					public static Vector3 Kilometres(double value)
					{
						return new Vector3(Length.Kilometres(value));
					}

											public static Vector3 Kilometres( Vector3d value)
						{
							return new Vector3(								Length.Kilometres(value.X)
							, 								Length.Kilometres(value.Y)
							, 								Length.Kilometres(value.Z)
							);
						}
											public static Vector3 Kilometres( ref  Vector3d value)
						{
							return new Vector3(								Length.Kilometres(value.X)
							, 								Length.Kilometres(value.Y)
							, 								Length.Kilometres(value.Z)
							);
						}
									

					public Vector3d InKilometres
					{
						get
						{
							return new Vector3d(								X.InKilometres
							, 								Y.InKilometres
							, 								Z.InKilometres
							);
						}
					}
									public static Vector3 Megametres( double x ,  double y ,  double z )
					{
						return new Vector3( Length.Megametres(x) ,  Length.Megametres(y) ,  Length.Megametres(z) );
					}
				
					public static Vector3 Megametres(double value)
					{
						return new Vector3(Length.Megametres(value));
					}

											public static Vector3 Megametres( Vector3d value)
						{
							return new Vector3(								Length.Megametres(value.X)
							, 								Length.Megametres(value.Y)
							, 								Length.Megametres(value.Z)
							);
						}
											public static Vector3 Megametres( ref  Vector3d value)
						{
							return new Vector3(								Length.Megametres(value.X)
							, 								Length.Megametres(value.Y)
							, 								Length.Megametres(value.Z)
							);
						}
									

					public Vector3d InMegametres
					{
						get
						{
							return new Vector3d(								X.InMegametres
							, 								Y.InMegametres
							, 								Z.InMegametres
							);
						}
					}
									public static Vector3 Metres( double x ,  double y ,  double z )
					{
						return new Vector3( Length.Metres(x) ,  Length.Metres(y) ,  Length.Metres(z) );
					}
				
					public static Vector3 Metres(double value)
					{
						return new Vector3(Length.Metres(value));
					}

											public static Vector3 Metres( Vector3d value)
						{
							return new Vector3(								Length.Metres(value.X)
							, 								Length.Metres(value.Y)
							, 								Length.Metres(value.Z)
							);
						}
											public static Vector3 Metres( ref  Vector3d value)
						{
							return new Vector3(								Length.Metres(value.X)
							, 								Length.Metres(value.Y)
							, 								Length.Metres(value.Z)
							);
						}
									

					public Vector3d InMetres
					{
						get
						{
							return new Vector3d(								X.InMetres
							, 								Y.InMetres
							, 								Z.InMetres
							);
						}
					}
									public static Vector3 Micrometres( double x ,  double y ,  double z )
					{
						return new Vector3( Length.Micrometres(x) ,  Length.Micrometres(y) ,  Length.Micrometres(z) );
					}
				
					public static Vector3 Micrometres(double value)
					{
						return new Vector3(Length.Micrometres(value));
					}

											public static Vector3 Micrometres( Vector3d value)
						{
							return new Vector3(								Length.Micrometres(value.X)
							, 								Length.Micrometres(value.Y)
							, 								Length.Micrometres(value.Z)
							);
						}
											public static Vector3 Micrometres( ref  Vector3d value)
						{
							return new Vector3(								Length.Micrometres(value.X)
							, 								Length.Micrometres(value.Y)
							, 								Length.Micrometres(value.Z)
							);
						}
									

					public Vector3d InMicrometres
					{
						get
						{
							return new Vector3d(								X.InMicrometres
							, 								Y.InMicrometres
							, 								Z.InMicrometres
							);
						}
					}
									public static Vector3 Millimetres( double x ,  double y ,  double z )
					{
						return new Vector3( Length.Millimetres(x) ,  Length.Millimetres(y) ,  Length.Millimetres(z) );
					}
				
					public static Vector3 Millimetres(double value)
					{
						return new Vector3(Length.Millimetres(value));
					}

											public static Vector3 Millimetres( Vector3d value)
						{
							return new Vector3(								Length.Millimetres(value.X)
							, 								Length.Millimetres(value.Y)
							, 								Length.Millimetres(value.Z)
							);
						}
											public static Vector3 Millimetres( ref  Vector3d value)
						{
							return new Vector3(								Length.Millimetres(value.X)
							, 								Length.Millimetres(value.Y)
							, 								Length.Millimetres(value.Z)
							);
						}
									

					public Vector3d InMillimetres
					{
						get
						{
							return new Vector3d(								X.InMillimetres
							, 								Y.InMillimetres
							, 								Z.InMillimetres
							);
						}
					}
									public static Vector3 Nanometres( double x ,  double y ,  double z )
					{
						return new Vector3( Length.Nanometres(x) ,  Length.Nanometres(y) ,  Length.Nanometres(z) );
					}
				
					public static Vector3 Nanometres(double value)
					{
						return new Vector3(Length.Nanometres(value));
					}

											public static Vector3 Nanometres( Vector3d value)
						{
							return new Vector3(								Length.Nanometres(value.X)
							, 								Length.Nanometres(value.Y)
							, 								Length.Nanometres(value.Z)
							);
						}
											public static Vector3 Nanometres( ref  Vector3d value)
						{
							return new Vector3(								Length.Nanometres(value.X)
							, 								Length.Nanometres(value.Y)
							, 								Length.Nanometres(value.Z)
							);
						}
									

					public Vector3d InNanometres
					{
						get
						{
							return new Vector3d(								X.InNanometres
							, 								Y.InNanometres
							, 								Z.InNanometres
							);
						}
					}
									public static Vector3 Yards( double x ,  double y ,  double z )
					{
						return new Vector3( Length.Yards(x) ,  Length.Yards(y) ,  Length.Yards(z) );
					}
				
					public static Vector3 Yards(double value)
					{
						return new Vector3(Length.Yards(value));
					}

											public static Vector3 Yards( Vector3d value)
						{
							return new Vector3(								Length.Yards(value.X)
							, 								Length.Yards(value.Y)
							, 								Length.Yards(value.Z)
							);
						}
											public static Vector3 Yards( ref  Vector3d value)
						{
							return new Vector3(								Length.Yards(value.X)
							, 								Length.Yards(value.Y)
							, 								Length.Yards(value.Z)
							);
						}
									

					public Vector3d InYards
					{
						get
						{
							return new Vector3d(								X.InYards
							, 								Y.InYards
							, 								Z.InYards
							);
						}
					}
									public static Vector3 Universal( double x ,  double y ,  double z )
					{
						return new Vector3( Length.Universal(x) ,  Length.Universal(y) ,  Length.Universal(z) );
					}
				
					public static Vector3 Universal(double value)
					{
						return new Vector3(Length.Universal(value));
					}

											public static Vector3 Universal( Vector3d value)
						{
							return new Vector3(								Length.Universal(value.X)
							, 								Length.Universal(value.Y)
							, 								Length.Universal(value.Z)
							);
						}
											public static Vector3 Universal( ref  Vector3d value)
						{
							return new Vector3(								Length.Universal(value.X)
							, 								Length.Universal(value.Y)
							, 								Length.Universal(value.Z)
							);
						}
									

					public Vector3d InUniversal
					{
						get
						{
							return new Vector3d(								X.InUniversal
							, 								Y.InUniversal
							, 								Z.InUniversal
							);
						}
					}
				
			
			

			// Constructors built up of smaller vectors.
			
			// Constructors from much smaller vectors.
			
		#endregion Constructors

		#region Methods

			// Methods that have joint ref forms.
							/// <summary>Compute the dot product of this and other vector, which is the sum of the product of each axis of each vector.</summary>
				/// <param name="other">The other vector to calculate a dot product with. </param>
				public Area Dot( Vector3 other) { return  X * other.X + Y * other.Y + Z * other.Z ; }
							/// <summary>Compute the dot product of this and other vector, which is the sum of the product of each axis of each vector.</summary>
				/// <param name="other">The other vector to calculate a dot product with. The value of the parameter will not be changed; <c>ref</c> is used for optimisation only.</param>
				public Area Dot( ref  Vector3 other) { return  X * other.X + Y * other.Y + Z * other.Z ; }
			
							public Vector3 Clamp( Vector3 min,  Vector3 max )
				{
					Vector3 result;
											result.X = X < min.X ? min.X : X > max.X ? max.X : X;
											result.Y = Y < min.Y ? min.Y : Y > max.Y ? max.Y : Y;
											result.Z = Z < min.Z ? min.Z : Z > max.Z ? max.Z : Z;
										return result;
				}
							public void Clamp( ref  Vector3 min,  ref  Vector3 max , out Vector3 result)
				{
					
											result.X = X < min.X ? min.X : X > max.X ? max.X : X;
											result.Y = Y < min.Y ? min.Y : Y > max.Y ? max.Y : Y;
											result.Z = Z < min.Z ? min.Z : Z > max.Z ? max.Z : Z;
										return;
				}
			
			// Floating-point methods
												/// <summary>Get the distance between the vectors.</summary>
					public Length Distance( Vector3 other)
					{
						return Length.Universal(							Math.Sqrt(
																	( X - other.X ).InUniversal.Squared() 
								+									( Y - other.Y ).InUniversal.Squared() 
								+									( Z - other.Z ).InUniversal.Squared() 
															));
					}

					/// <summary>Get the squared distance between the vectors.</summary>
					public Area DistanceSquared( Vector3 other)
					{
						return 
															(X - other.X).Squared()
							+								(Y - other.Y).Squared()
							+								(Z - other.Z).Squared()
							;
					}
									/// <summary>Get the distance between the vectors.</summary>
					public Length Distance( ref  Vector3 other)
					{
						return Length.Universal(							Math.Sqrt(
																	( X - other.X ).InUniversal.Squared() 
								+									( Y - other.Y ).InUniversal.Squared() 
								+									( Z - other.Z ).InUniversal.Squared() 
															));
					}

					/// <summary>Get the squared distance between the vectors.</summary>
					public Area DistanceSquared( ref  Vector3 other)
					{
						return 
															(X - other.X).Squared()
							+								(Y - other.Y).Squared()
							+								(Z - other.Z).Squared()
							;
					}
				
							
		#endregion Methods

			#region Fields

			/// <summary>The first axis of the <see cref="Vector3"/>.</summary>
		public Length X;
			/// <summary>The second axis of the <see cref="Vector3"/>.</summary>
		public Length Y;
			/// <summary>The third axis of the <see cref="Vector3"/>.</summary>
		public Length Z;
	
	#endregion Fields

	#region Properties

	public static readonly Vector3 Zero = new Vector3(Length.Zero, Length.Zero, Length.Zero);

	#endregion Properties

	#region Constructors

	/// <summary>Initialise a <see cref="Vector3"/> from the provided values for each factor.</summary>
	public Vector3(Length x, Length y, Length z)
	{
					X = x;
					Y = y;
					Z = z;
			}

	/// <summary>Initialise a <see cref="Vector3"/> from a list.</summary>
	public Vector3(IList<Length> list, int index = 0) : this(list[index + 0], list[index + 1], list[index + 2]) { }

	/// <summary>Initialise a <see cref="Vector3"/> from a single scalar that is applied to all factors.</summary>
	public Vector3(Length value) : this(value, value, value) { }

	#endregion Constructors

	#region Methods

	/// <summary>Get whether this <see cref="Vector3"/> has equal factors as the other <see cref="Vector3"/>.</summary>
	public bool Equals(Vector3 other) {
		return  X == other.X && Y == other.Y && Z == other.Z ;
	}

	/// <summary>If the other object is a <see cref="Vector3"/> of the same type, get whether this <see cref="Vector3"/> has equal factors as it; otherwise return false./summary>
	public override bool Equals(object other)
	{
		if(other is Vector3)
			return Equals((Vector3)other);
		return base.Equals(other);
	}

	/// <summary>Compute a hash code from combining the axes.</summary>
	public override int GetHashCode()
	{
		return  X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode() ;
	}

	/// <summary>Convert this <see cref="Vector3"/> to a string of the form "X, Y, Z".</summary>
	public string ToCommaSeparatedString(string format = null, IFormatProvider provider = null) {
		return X.ToString(format, provider) + ", " + Y.ToString(format, provider) + ", " + Z.ToString(format, provider);
	}

	/// <summary>Convert this <see cref="Vector3"/> to a string of the form "Vector3(X, Y, Z)".</summary>
	public override string ToString()
	{
		return ToString(null, null);
	}

	/// <summary>Convert this <see cref="Vector3"/> to a string of the form "Vector3(X, Y, Z)".</summary>
	public string ToString(string format, IFormatProvider provider)
	{
		return "Vector3(" + ToCommaSeparatedString(format, provider) + ")";
	}

	/// <summary>Convert this <see cref="Vector3"/> to a string of the form "{X, Y, Z".</summary>
	public string ToShortString(string format = null, IFormatProvider provider = null) { return "{" + ToCommaSeparatedString(format, provider) + "}"; }	

	#endregion Methods
	
	public static bool operator ==(Vector3 a, Vector3 b) { return a.X == b.X&&a.Y == b.Y&&a.Z == b.Z; }

	public static bool operator !=(Vector3 a, Vector3 b) { return a.X != b.X||a.Y != b.Y||a.Z != b.Z; }

		#region Operators
		

			// Casting
			
			public static Vector3 operator +(Vector3 a) { return new Vector3((Length)(+a.X), (Length)(+a.Y), (Length)(+a.Z)); }
			public static Vector3 operator -(Vector3 a) { return new Vector3((Length)(-a.X), (Length)(-a.Y), (Length)(-a.Z)); }
								public static Vector3 operator +(Vector3 a, Vector3 b) { return new Vector3((Length)(a.X + b.X), (Length)(a.Y + b.Y), (Length)(a.Z + b.Z)); }
					public static Vector3 operator +(Vector3 a, Length b) { return new Vector3((Length)(a.X + b), (Length)(a.Y + b), (Length)(a.Z + b)); }
					public static Vector3 operator +(Length a, Vector3 b) { return new Vector3((Length)(a + b.X), (Length)(a + b.Y), (Length)(a + b.Z)); }
									public static Vector3 operator -(Vector3 a, Vector3 b) { return new Vector3((Length)(a.X - b.X), (Length)(a.Y - b.Y), (Length)(a.Z - b.Z)); }
					public static Vector3 operator -(Vector3 a, Length b) { return new Vector3((Length)(a.X - b), (Length)(a.Y - b), (Length)(a.Z - b)); }
					public static Vector3 operator -(Length a, Vector3 b) { return new Vector3((Length)(a - b.X), (Length)(a - b.Y), (Length)(a - b.Z)); }
				
		#endregion Operators
	}
	
	
	/// <summary>A three-dimensional vector type using <see cref="float"/> elements.</summary>
	[VectorTypeAttribute(typeof(float), 3, false)]
	public partial struct Vector3f : IEquatable<Vector3f>, IFormattable
	{
		#region Properties
		
							/// <summary>Compute the squared magnitude of the vector, which is the distance from the origin squared. Use <see cref="Magnitude"/> for the unsquared version, which is slower to compute.</summary>
				public float MagnitudeSquared { get { return  X * X + Y * Y + Z * Z ; } }
			
				/// <summary>Compute the magnitude of the vector, which is the distance from the origin.</summary>
				public float Magnitude
				{
					get
					{
						return (float)(							Math.Sqrt(
																	 X.Squared() 
								+									 Y.Squared() 
								+									 Z.Squared() 
															));
					}
				}

									/// <summary>Get the normalized form of this vector, with a magnitude of one.</summary>
					public Vector3f Normalized { get { Vector3f result; Normalize(out result); return result; } }
							
							/// <summary>Get the product of multiplying all of the axes together.</summary>
				public float Product { get { return (float)( X * Y * Z ); } }
			
			/// <summary>Get the sum of all of the axes.</summary>
			public float Sum { get { return (float)( X + Y + Z ); } }

		#endregion Properties

		#region Constructors
		
			
			
							public static readonly Vector3f One = new Vector3f((float)1, (float)1, (float)1);

									/// <summary>Get a normalized vector where X is 1 and all other axes are 0.</summary>
					public static readonly Vector3f UnitX = new Vector3f((float)1, (float)0, (float)0);
									/// <summary>Get a normalized vector where Y is 1 and all other axes are 0.</summary>
					public static readonly Vector3f UnitY = new Vector3f((float)0, (float)1, (float)0);
									/// <summary>Get a normalized vector where Z is 1 and all other axes are 0.</summary>
					public static readonly Vector3f UnitZ = new Vector3f((float)0, (float)0, (float)1);
							

			// Constructors built up of smaller vectors.
												public Vector3f( Vector2f xy, float z)
					{
													X = xy.X;
													Y = xy.Y;
										
						Z = z;
					}
				
					public Vector3f(float x,  Vector2f y)
					{
						X = x;

													Y = y.X;
													Z = y.Y;
											}
									public Vector3f( ref  Vector2f xy, float z)
					{
													X = xy.X;
													Y = xy.Y;
										
						Z = z;
					}
				
					public Vector3f(float x,  ref  Vector2f y)
					{
						X = x;

													Y = y.X;
													Z = y.Y;
											}
							
			// Constructors from much smaller vectors.
			
		#endregion Constructors

		#region Methods

			// Methods that have joint ref forms.
							/// <summary>Compute the dot product of this and other vector, which is the sum of the product of each axis of each vector.</summary>
				/// <param name="other">The other vector to calculate a dot product with. </param>
				public float Dot( Vector3f other) { return  X * other.X + Y * other.Y + Z * other.Z ; }
							/// <summary>Compute the dot product of this and other vector, which is the sum of the product of each axis of each vector.</summary>
				/// <param name="other">The other vector to calculate a dot product with. The value of the parameter will not be changed; <c>ref</c> is used for optimisation only.</param>
				public float Dot( ref  Vector3f other) { return  X * other.X + Y * other.Y + Z * other.Z ; }
			
							public Vector3f Clamp( Vector3f min,  Vector3f max )
				{
					Vector3f result;
											result.X = X < min.X ? min.X : X > max.X ? max.X : X;
											result.Y = Y < min.Y ? min.Y : Y > max.Y ? max.Y : Y;
											result.Z = Z < min.Z ? min.Z : Z > max.Z ? max.Z : Z;
										return result;
				}
							public void Clamp( ref  Vector3f min,  ref  Vector3f max , out Vector3f result)
				{
					
											result.X = X < min.X ? min.X : X > max.X ? max.X : X;
											result.Y = Y < min.Y ? min.Y : Y > max.Y ? max.Y : Y;
											result.Z = Z < min.Z ? min.Z : Z > max.Z ? max.Z : Z;
										return;
				}
			
			// Floating-point methods
												/// <summary>Get the distance between the vectors.</summary>
					public float Distance( Vector3f other)
					{
						return (float)(							Math.Sqrt(
																	 X - other.X .Squared() 
								+									 Y - other.Y .Squared() 
								+									 Z - other.Z .Squared() 
															));
					}

					/// <summary>Get the squared distance between the vectors.</summary>
					public float DistanceSquared( Vector3f other)
					{
						return 
															(X - other.X).Squared()
							+								(Y - other.Y).Squared()
							+								(Z - other.Z).Squared()
							;
					}
									/// <summary>Get the distance between the vectors.</summary>
					public float Distance( ref  Vector3f other)
					{
						return (float)(							Math.Sqrt(
																	 X - other.X .Squared() 
								+									 Y - other.Y .Squared() 
								+									 Z - other.Z .Squared() 
															));
					}

					/// <summary>Get the squared distance between the vectors.</summary>
					public float DistanceSquared( ref  Vector3f other)
					{
						return 
															(X - other.X).Squared()
							+								(Y - other.Y).Squared()
							+								(Z - other.Z).Squared()
							;
					}
				
									/// <summary>Get the normalized form of this vector, which has a magnitude of one.</summary>
					public void Normalize(out Vector3f result)
					{
						double m = 1.0 / Math.Sqrt( X * X + Y * Y + Z * Z );
						 result.X = (float)(X * m);  result.Y = (float)(Y * m);  result.Z = (float)(Z * m); 					}

					/// <summary>Normalize this vector in place, giving it a magnitude of one. An identity vector will become NaN.</summary>
					public void NormalizeInPlace()
					{
						float m = (float)(1.0 / Math.Sqrt( X * X + Y * Y + Z * Z ));
						 X *= m;  Y *= m;  Z *= m; 					}
				
											/// <summary>Calculates the cross product of the vectors.</summary>
						/// <param name="other">The other vector to perform a cross product with.</param>
						/// <returns>The cross product.</returns>
						public Vector3f Cross(Vector3f other) { Cross(ref other, out other); return other; }

						/// <summary>Calculates the cross product of the vectors.</summary>
						/// <param name="other">The other vector to perform a cross product with.</param>
						/// <param name="result">Receives the cross product.</param>
						public void Cross(Vector3f other, out Vector3f result) { Cross(ref other, out result); }

						/// <summary>Calculates the cross product of the vectors.</summary>
						/// <param name="other">The other vector to perform a cross product with.</param>
						/// <param name="result">Receives the cross product.</param>
						public void Cross(ref Vector3f other, out Vector3f result)
						{
							float x = (float)(Y * other.Z - Z * other.Y), y = (float)(Z * other.X - X * other.Z), z = (float)(X * other.Y - Y * other.X);
							result.X = x;
							result.Y = y;
							result.Z = z;
						}

												
		#endregion Methods

			#region Fields

			/// <summary>The first axis of the <see cref="Vector3f"/>.</summary>
		public float X;
			/// <summary>The second axis of the <see cref="Vector3f"/>.</summary>
		public float Y;
			/// <summary>The third axis of the <see cref="Vector3f"/>.</summary>
		public float Z;
	
	#endregion Fields

	#region Properties

	public static readonly Vector3f Zero = new Vector3f((float)0, (float)0, (float)0);

	#endregion Properties

	#region Constructors

	/// <summary>Initialise a <see cref="Vector3f"/> from the provided values for each factor.</summary>
	public Vector3f(float x, float y, float z)
	{
					X = x;
					Y = y;
					Z = z;
			}

	/// <summary>Initialise a <see cref="Vector3f"/> from a list.</summary>
	public Vector3f(IList<float> list, int index = 0) : this(list[index + 0], list[index + 1], list[index + 2]) { }

	/// <summary>Initialise a <see cref="Vector3f"/> from a single scalar that is applied to all factors.</summary>
	public Vector3f(float value) : this(value, value, value) { }

	#endregion Constructors

	#region Methods

	/// <summary>Get whether this <see cref="Vector3f"/> has equal factors as the other <see cref="Vector3f"/>.</summary>
	public bool Equals(Vector3f other) {
		return  X == other.X && Y == other.Y && Z == other.Z ;
	}

	/// <summary>If the other object is a <see cref="Vector3f"/> of the same type, get whether this <see cref="Vector3f"/> has equal factors as it; otherwise return false./summary>
	public override bool Equals(object other)
	{
		if(other is Vector3f)
			return Equals((Vector3f)other);
		return base.Equals(other);
	}

	/// <summary>Compute a hash code from combining the axes.</summary>
	public override int GetHashCode()
	{
		return  X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode() ;
	}

	/// <summary>Convert this <see cref="Vector3f"/> to a string of the form "X, Y, Z".</summary>
	public string ToCommaSeparatedString(string format = null, IFormatProvider provider = null) {
		return X.ToString(format, provider) + ", " + Y.ToString(format, provider) + ", " + Z.ToString(format, provider);
	}

	/// <summary>Convert this <see cref="Vector3f"/> to a string of the form "Vector3f(X, Y, Z)".</summary>
	public override string ToString()
	{
		return ToString(null, null);
	}

	/// <summary>Convert this <see cref="Vector3f"/> to a string of the form "Vector3f(X, Y, Z)".</summary>
	public string ToString(string format, IFormatProvider provider)
	{
		return "Vector3f(" + ToCommaSeparatedString(format, provider) + ")";
	}

	/// <summary>Convert this <see cref="Vector3f"/> to a string of the form "{X, Y, Z".</summary>
	public string ToShortString(string format = null, IFormatProvider provider = null) { return "{" + ToCommaSeparatedString(format, provider) + "}"; }	

	#endregion Methods
	
	public static bool operator ==(Vector3f a, Vector3f b) { return a.X == b.X&&a.Y == b.Y&&a.Z == b.Z; }

	public static bool operator !=(Vector3f a, Vector3f b) { return a.X != b.X||a.Y != b.Y||a.Z != b.Z; }

		#region Operators
		

			// Casting
							public static implicit operator Vector3d(Vector3f a) { return new Vector3d((double)a.X, (double)a.Y, (double)a.Z); }
					public static explicit operator Vector3i(Vector3f a) { return new Vector3i((int)a.X, (int)a.Y, (int)a.Z); }
					public static explicit operator Vector3ui(Vector3f a) { return new Vector3ui((uint)a.X, (uint)a.Y, (uint)a.Z); }
					public static explicit operator Vector3rgb(Vector3f a) { return new Vector3rgb((NormalizedByte)a.X, (NormalizedByte)a.Y, (NormalizedByte)a.Z); }
					
				// Explicit casting to vectors with a lower order, trimming off axes.
									public static explicit operator Vector2f(Vector3f a)
					{
						return new Vector2f(a.X, a.Y);
					}
							
			public static Vector3f operator +(Vector3f a) { return new Vector3f((float)(+a.X), (float)(+a.Y), (float)(+a.Z)); }
			public static Vector3f operator -(Vector3f a) { return new Vector3f((float)(-a.X), (float)(-a.Y), (float)(-a.Z)); }
								public static Vector3f operator +(Vector3f a, Vector3f b) { return new Vector3f((float)(a.X + b.X), (float)(a.Y + b.Y), (float)(a.Z + b.Z)); }
					public static Vector3f operator +(Vector3f a, float b) { return new Vector3f((float)(a.X + b), (float)(a.Y + b), (float)(a.Z + b)); }
					public static Vector3f operator +(float a, Vector3f b) { return new Vector3f((float)(a + b.X), (float)(a + b.Y), (float)(a + b.Z)); }
									public static Vector3f operator -(Vector3f a, Vector3f b) { return new Vector3f((float)(a.X - b.X), (float)(a.Y - b.Y), (float)(a.Z - b.Z)); }
					public static Vector3f operator -(Vector3f a, float b) { return new Vector3f((float)(a.X - b), (float)(a.Y - b), (float)(a.Z - b)); }
					public static Vector3f operator -(float a, Vector3f b) { return new Vector3f((float)(a - b.X), (float)(a - b.Y), (float)(a - b.Z)); }
									public static Vector3f operator *(Vector3f a, Vector3f b) { return new Vector3f((float)(a.X * b.X), (float)(a.Y * b.Y), (float)(a.Z * b.Z)); }
					public static Vector3f operator *(Vector3f a, float b) { return new Vector3f((float)(a.X * b), (float)(a.Y * b), (float)(a.Z * b)); }
					public static Vector3f operator *(float a, Vector3f b) { return new Vector3f((float)(a * b.X), (float)(a * b.Y), (float)(a * b.Z)); }
									public static Vector3f operator /(Vector3f a, Vector3f b) { return new Vector3f((float)(a.X / b.X), (float)(a.Y / b.Y), (float)(a.Z / b.Z)); }
					public static Vector3f operator /(Vector3f a, float b) { return new Vector3f((float)(a.X / b), (float)(a.Y / b), (float)(a.Z / b)); }
					public static Vector3f operator /(float a, Vector3f b) { return new Vector3f((float)(a / b.X), (float)(a / b.Y), (float)(a / b.Z)); }
									public static Vector3f operator %(Vector3f a, Vector3f b) { return new Vector3f((float)(a.X % b.X), (float)(a.Y % b.Y), (float)(a.Z % b.Z)); }
					public static Vector3f operator %(Vector3f a, float b) { return new Vector3f((float)(a.X % b), (float)(a.Y % b), (float)(a.Z % b)); }
					public static Vector3f operator %(float a, Vector3f b) { return new Vector3f((float)(a % b.X), (float)(a % b.Y), (float)(a % b.Z)); }
				
		#endregion Operators
	}
	
	
	/// <summary>A three-dimensional vector type using <see cref="double"/> elements.</summary>
	[VectorTypeAttribute(typeof(double), 3, false)]
	public partial struct Vector3d : IEquatable<Vector3d>, IFormattable
	{
		#region Properties
		
							/// <summary>Compute the squared magnitude of the vector, which is the distance from the origin squared. Use <see cref="Magnitude"/> for the unsquared version, which is slower to compute.</summary>
				public double MagnitudeSquared { get { return  X * X + Y * Y + Z * Z ; } }
			
				/// <summary>Compute the magnitude of the vector, which is the distance from the origin.</summary>
				public double Magnitude
				{
					get
					{
						return (double)(							Math.Sqrt(
																	 X.Squared() 
								+									 Y.Squared() 
								+									 Z.Squared() 
															));
					}
				}

									/// <summary>Get the normalized form of this vector, with a magnitude of one.</summary>
					public Vector3d Normalized { get { Vector3d result; Normalize(out result); return result; } }
							
							/// <summary>Get the product of multiplying all of the axes together.</summary>
				public double Product { get { return (double)( X * Y * Z ); } }
			
			/// <summary>Get the sum of all of the axes.</summary>
			public double Sum { get { return (double)( X + Y + Z ); } }

		#endregion Properties

		#region Constructors
		
			
			
							public static readonly Vector3d One = new Vector3d((double)1, (double)1, (double)1);

									/// <summary>Get a normalized vector where X is 1 and all other axes are 0.</summary>
					public static readonly Vector3d UnitX = new Vector3d((double)1, (double)0, (double)0);
									/// <summary>Get a normalized vector where Y is 1 and all other axes are 0.</summary>
					public static readonly Vector3d UnitY = new Vector3d((double)0, (double)1, (double)0);
									/// <summary>Get a normalized vector where Z is 1 and all other axes are 0.</summary>
					public static readonly Vector3d UnitZ = new Vector3d((double)0, (double)0, (double)1);
							

			// Constructors built up of smaller vectors.
												public Vector3d( Vector2d xy, double z)
					{
													X = xy.X;
													Y = xy.Y;
										
						Z = z;
					}
				
					public Vector3d(double x,  Vector2d y)
					{
						X = x;

													Y = y.X;
													Z = y.Y;
											}
									public Vector3d( ref  Vector2d xy, double z)
					{
													X = xy.X;
													Y = xy.Y;
										
						Z = z;
					}
				
					public Vector3d(double x,  ref  Vector2d y)
					{
						X = x;

													Y = y.X;
													Z = y.Y;
											}
							
			// Constructors from much smaller vectors.
			
		#endregion Constructors

		#region Methods

			// Methods that have joint ref forms.
							/// <summary>Compute the dot product of this and other vector, which is the sum of the product of each axis of each vector.</summary>
				/// <param name="other">The other vector to calculate a dot product with. </param>
				public double Dot( Vector3d other) { return  X * other.X + Y * other.Y + Z * other.Z ; }
							/// <summary>Compute the dot product of this and other vector, which is the sum of the product of each axis of each vector.</summary>
				/// <param name="other">The other vector to calculate a dot product with. The value of the parameter will not be changed; <c>ref</c> is used for optimisation only.</param>
				public double Dot( ref  Vector3d other) { return  X * other.X + Y * other.Y + Z * other.Z ; }
			
							public Vector3d Clamp( Vector3d min,  Vector3d max )
				{
					Vector3d result;
											result.X = X < min.X ? min.X : X > max.X ? max.X : X;
											result.Y = Y < min.Y ? min.Y : Y > max.Y ? max.Y : Y;
											result.Z = Z < min.Z ? min.Z : Z > max.Z ? max.Z : Z;
										return result;
				}
							public void Clamp( ref  Vector3d min,  ref  Vector3d max , out Vector3d result)
				{
					
											result.X = X < min.X ? min.X : X > max.X ? max.X : X;
											result.Y = Y < min.Y ? min.Y : Y > max.Y ? max.Y : Y;
											result.Z = Z < min.Z ? min.Z : Z > max.Z ? max.Z : Z;
										return;
				}
			
			// Floating-point methods
												/// <summary>Get the distance between the vectors.</summary>
					public double Distance( Vector3d other)
					{
						return (double)(							Math.Sqrt(
																	 X - other.X .Squared() 
								+									 Y - other.Y .Squared() 
								+									 Z - other.Z .Squared() 
															));
					}

					/// <summary>Get the squared distance between the vectors.</summary>
					public double DistanceSquared( Vector3d other)
					{
						return 
															(X - other.X).Squared()
							+								(Y - other.Y).Squared()
							+								(Z - other.Z).Squared()
							;
					}
									/// <summary>Get the distance between the vectors.</summary>
					public double Distance( ref  Vector3d other)
					{
						return (double)(							Math.Sqrt(
																	 X - other.X .Squared() 
								+									 Y - other.Y .Squared() 
								+									 Z - other.Z .Squared() 
															));
					}

					/// <summary>Get the squared distance between the vectors.</summary>
					public double DistanceSquared( ref  Vector3d other)
					{
						return 
															(X - other.X).Squared()
							+								(Y - other.Y).Squared()
							+								(Z - other.Z).Squared()
							;
					}
				
									/// <summary>Get the normalized form of this vector, which has a magnitude of one.</summary>
					public void Normalize(out Vector3d result)
					{
						double m = 1.0 / Math.Sqrt( X * X + Y * Y + Z * Z );
						 result.X = (double)(X * m);  result.Y = (double)(Y * m);  result.Z = (double)(Z * m); 					}

					/// <summary>Normalize this vector in place, giving it a magnitude of one. An identity vector will become NaN.</summary>
					public void NormalizeInPlace()
					{
						double m = (double)(1.0 / Math.Sqrt( X * X + Y * Y + Z * Z ));
						 X *= m;  Y *= m;  Z *= m; 					}
				
											/// <summary>Calculates the cross product of the vectors.</summary>
						/// <param name="other">The other vector to perform a cross product with.</param>
						/// <returns>The cross product.</returns>
						public Vector3d Cross(Vector3d other) { Cross(ref other, out other); return other; }

						/// <summary>Calculates the cross product of the vectors.</summary>
						/// <param name="other">The other vector to perform a cross product with.</param>
						/// <param name="result">Receives the cross product.</param>
						public void Cross(Vector3d other, out Vector3d result) { Cross(ref other, out result); }

						/// <summary>Calculates the cross product of the vectors.</summary>
						/// <param name="other">The other vector to perform a cross product with.</param>
						/// <param name="result">Receives the cross product.</param>
						public void Cross(ref Vector3d other, out Vector3d result)
						{
							double x = (double)(Y * other.Z - Z * other.Y), y = (double)(Z * other.X - X * other.Z), z = (double)(X * other.Y - Y * other.X);
							result.X = x;
							result.Y = y;
							result.Z = z;
						}

												
		#endregion Methods

			#region Fields

			/// <summary>The first axis of the <see cref="Vector3d"/>.</summary>
		public double X;
			/// <summary>The second axis of the <see cref="Vector3d"/>.</summary>
		public double Y;
			/// <summary>The third axis of the <see cref="Vector3d"/>.</summary>
		public double Z;
	
	#endregion Fields

	#region Properties

	public static readonly Vector3d Zero = new Vector3d((double)0, (double)0, (double)0);

	#endregion Properties

	#region Constructors

	/// <summary>Initialise a <see cref="Vector3d"/> from the provided values for each factor.</summary>
	public Vector3d(double x, double y, double z)
	{
					X = x;
					Y = y;
					Z = z;
			}

	/// <summary>Initialise a <see cref="Vector3d"/> from a list.</summary>
	public Vector3d(IList<double> list, int index = 0) : this(list[index + 0], list[index + 1], list[index + 2]) { }

	/// <summary>Initialise a <see cref="Vector3d"/> from a single scalar that is applied to all factors.</summary>
	public Vector3d(double value) : this(value, value, value) { }

	#endregion Constructors

	#region Methods

	/// <summary>Get whether this <see cref="Vector3d"/> has equal factors as the other <see cref="Vector3d"/>.</summary>
	public bool Equals(Vector3d other) {
		return  X == other.X && Y == other.Y && Z == other.Z ;
	}

	/// <summary>If the other object is a <see cref="Vector3d"/> of the same type, get whether this <see cref="Vector3d"/> has equal factors as it; otherwise return false./summary>
	public override bool Equals(object other)
	{
		if(other is Vector3d)
			return Equals((Vector3d)other);
		return base.Equals(other);
	}

	/// <summary>Compute a hash code from combining the axes.</summary>
	public override int GetHashCode()
	{
		return  X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode() ;
	}

	/// <summary>Convert this <see cref="Vector3d"/> to a string of the form "X, Y, Z".</summary>
	public string ToCommaSeparatedString(string format = null, IFormatProvider provider = null) {
		return X.ToString(format, provider) + ", " + Y.ToString(format, provider) + ", " + Z.ToString(format, provider);
	}

	/// <summary>Convert this <see cref="Vector3d"/> to a string of the form "Vector3d(X, Y, Z)".</summary>
	public override string ToString()
	{
		return ToString(null, null);
	}

	/// <summary>Convert this <see cref="Vector3d"/> to a string of the form "Vector3d(X, Y, Z)".</summary>
	public string ToString(string format, IFormatProvider provider)
	{
		return "Vector3d(" + ToCommaSeparatedString(format, provider) + ")";
	}

	/// <summary>Convert this <see cref="Vector3d"/> to a string of the form "{X, Y, Z".</summary>
	public string ToShortString(string format = null, IFormatProvider provider = null) { return "{" + ToCommaSeparatedString(format, provider) + "}"; }	

	#endregion Methods
	
	public static bool operator ==(Vector3d a, Vector3d b) { return a.X == b.X&&a.Y == b.Y&&a.Z == b.Z; }

	public static bool operator !=(Vector3d a, Vector3d b) { return a.X != b.X||a.Y != b.Y||a.Z != b.Z; }

		#region Operators
		

			// Casting
							public static explicit operator Vector3f(Vector3d a) { return new Vector3f((float)a.X, (float)a.Y, (float)a.Z); }
					public static explicit operator Vector3i(Vector3d a) { return new Vector3i((int)a.X, (int)a.Y, (int)a.Z); }
					public static explicit operator Vector3ui(Vector3d a) { return new Vector3ui((uint)a.X, (uint)a.Y, (uint)a.Z); }
					public static explicit operator Vector3rgb(Vector3d a) { return new Vector3rgb((NormalizedByte)a.X, (NormalizedByte)a.Y, (NormalizedByte)a.Z); }
					
				// Explicit casting to vectors with a lower order, trimming off axes.
									public static explicit operator Vector2d(Vector3d a)
					{
						return new Vector2d(a.X, a.Y);
					}
							
			public static Vector3d operator +(Vector3d a) { return new Vector3d((double)(+a.X), (double)(+a.Y), (double)(+a.Z)); }
			public static Vector3d operator -(Vector3d a) { return new Vector3d((double)(-a.X), (double)(-a.Y), (double)(-a.Z)); }
								public static Vector3d operator +(Vector3d a, Vector3d b) { return new Vector3d((double)(a.X + b.X), (double)(a.Y + b.Y), (double)(a.Z + b.Z)); }
					public static Vector3d operator +(Vector3d a, double b) { return new Vector3d((double)(a.X + b), (double)(a.Y + b), (double)(a.Z + b)); }
					public static Vector3d operator +(double a, Vector3d b) { return new Vector3d((double)(a + b.X), (double)(a + b.Y), (double)(a + b.Z)); }
									public static Vector3d operator -(Vector3d a, Vector3d b) { return new Vector3d((double)(a.X - b.X), (double)(a.Y - b.Y), (double)(a.Z - b.Z)); }
					public static Vector3d operator -(Vector3d a, double b) { return new Vector3d((double)(a.X - b), (double)(a.Y - b), (double)(a.Z - b)); }
					public static Vector3d operator -(double a, Vector3d b) { return new Vector3d((double)(a - b.X), (double)(a - b.Y), (double)(a - b.Z)); }
									public static Vector3d operator *(Vector3d a, Vector3d b) { return new Vector3d((double)(a.X * b.X), (double)(a.Y * b.Y), (double)(a.Z * b.Z)); }
					public static Vector3d operator *(Vector3d a, double b) { return new Vector3d((double)(a.X * b), (double)(a.Y * b), (double)(a.Z * b)); }
					public static Vector3d operator *(double a, Vector3d b) { return new Vector3d((double)(a * b.X), (double)(a * b.Y), (double)(a * b.Z)); }
									public static Vector3d operator /(Vector3d a, Vector3d b) { return new Vector3d((double)(a.X / b.X), (double)(a.Y / b.Y), (double)(a.Z / b.Z)); }
					public static Vector3d operator /(Vector3d a, double b) { return new Vector3d((double)(a.X / b), (double)(a.Y / b), (double)(a.Z / b)); }
					public static Vector3d operator /(double a, Vector3d b) { return new Vector3d((double)(a / b.X), (double)(a / b.Y), (double)(a / b.Z)); }
									public static Vector3d operator %(Vector3d a, Vector3d b) { return new Vector3d((double)(a.X % b.X), (double)(a.Y % b.Y), (double)(a.Z % b.Z)); }
					public static Vector3d operator %(Vector3d a, double b) { return new Vector3d((double)(a.X % b), (double)(a.Y % b), (double)(a.Z % b)); }
					public static Vector3d operator %(double a, Vector3d b) { return new Vector3d((double)(a % b.X), (double)(a % b.Y), (double)(a % b.Z)); }
				
		#endregion Operators
	}
	
	
	/// <summary>A three-dimensional vector type using <see cref="int"/> elements.</summary>
	[VectorTypeAttribute(typeof(int), 3, false)]
	public partial struct Vector3i : IEquatable<Vector3i>, IFormattable
	{
		#region Properties
		
							/// <summary>Compute the squared magnitude of the vector, which is the distance from the origin squared. Use <see cref="Magnitude"/> for the unsquared version, which is slower to compute.</summary>
				public int MagnitudeSquared { get { return  X * X + Y * Y + Z * Z ; } }
			
				/// <summary>Compute the magnitude of the vector, which is the distance from the origin.</summary>
				public int Magnitude
				{
					get
					{
						return (int)(							Math.Sqrt(
																	 X.Squared() 
								+									 Y.Squared() 
								+									 Z.Squared() 
															));
					}
				}

									/// <summary>Get the normalized form of this vector, with a magnitude of one.</summary>
					public Vector3i Normalized { get { Vector3i result; Normalize(out result); return result; } }
							
							/// <summary>Get the product of multiplying all of the axes together.</summary>
				public int Product { get { return (int)( X * Y * Z ); } }
			
			/// <summary>Get the sum of all of the axes.</summary>
			public int Sum { get { return (int)( X + Y + Z ); } }

		#endregion Properties

		#region Constructors
		
			
			
							public static readonly Vector3i One = new Vector3i((int)1, (int)1, (int)1);

									/// <summary>Get a normalized vector where X is 1 and all other axes are 0.</summary>
					public static readonly Vector3i UnitX = new Vector3i((int)1, (int)0, (int)0);
									/// <summary>Get a normalized vector where Y is 1 and all other axes are 0.</summary>
					public static readonly Vector3i UnitY = new Vector3i((int)0, (int)1, (int)0);
									/// <summary>Get a normalized vector where Z is 1 and all other axes are 0.</summary>
					public static readonly Vector3i UnitZ = new Vector3i((int)0, (int)0, (int)1);
							

			// Constructors built up of smaller vectors.
												public Vector3i( Vector2i xy, int z)
					{
													X = xy.X;
													Y = xy.Y;
										
						Z = z;
					}
				
					public Vector3i(int x,  Vector2i y)
					{
						X = x;

													Y = y.X;
													Z = y.Y;
											}
									public Vector3i( ref  Vector2i xy, int z)
					{
													X = xy.X;
													Y = xy.Y;
										
						Z = z;
					}
				
					public Vector3i(int x,  ref  Vector2i y)
					{
						X = x;

													Y = y.X;
													Z = y.Y;
											}
							
			// Constructors from much smaller vectors.
			
		#endregion Constructors

		#region Methods

			// Methods that have joint ref forms.
							/// <summary>Compute the dot product of this and other vector, which is the sum of the product of each axis of each vector.</summary>
				/// <param name="other">The other vector to calculate a dot product with. </param>
				public int Dot( Vector3i other) { return  X * other.X + Y * other.Y + Z * other.Z ; }
							/// <summary>Compute the dot product of this and other vector, which is the sum of the product of each axis of each vector.</summary>
				/// <param name="other">The other vector to calculate a dot product with. The value of the parameter will not be changed; <c>ref</c> is used for optimisation only.</param>
				public int Dot( ref  Vector3i other) { return  X * other.X + Y * other.Y + Z * other.Z ; }
			
							public Vector3i Clamp( Vector3i min,  Vector3i max )
				{
					Vector3i result;
											result.X = X < min.X ? min.X : X > max.X ? max.X : X;
											result.Y = Y < min.Y ? min.Y : Y > max.Y ? max.Y : Y;
											result.Z = Z < min.Z ? min.Z : Z > max.Z ? max.Z : Z;
										return result;
				}
							public void Clamp( ref  Vector3i min,  ref  Vector3i max , out Vector3i result)
				{
					
											result.X = X < min.X ? min.X : X > max.X ? max.X : X;
											result.Y = Y < min.Y ? min.Y : Y > max.Y ? max.Y : Y;
											result.Z = Z < min.Z ? min.Z : Z > max.Z ? max.Z : Z;
										return;
				}
			
			// Floating-point methods
												/// <summary>Get the distance between the vectors.</summary>
					public int Distance( Vector3i other)
					{
						return (int)(							Math.Sqrt(
																	 X - other.X .Squared() 
								+									 Y - other.Y .Squared() 
								+									 Z - other.Z .Squared() 
															));
					}

					/// <summary>Get the squared distance between the vectors.</summary>
					public int DistanceSquared( Vector3i other)
					{
						return 
															(X - other.X).Squared()
							+								(Y - other.Y).Squared()
							+								(Z - other.Z).Squared()
							;
					}
									/// <summary>Get the distance between the vectors.</summary>
					public int Distance( ref  Vector3i other)
					{
						return (int)(							Math.Sqrt(
																	 X - other.X .Squared() 
								+									 Y - other.Y .Squared() 
								+									 Z - other.Z .Squared() 
															));
					}

					/// <summary>Get the squared distance between the vectors.</summary>
					public int DistanceSquared( ref  Vector3i other)
					{
						return 
															(X - other.X).Squared()
							+								(Y - other.Y).Squared()
							+								(Z - other.Z).Squared()
							;
					}
				
									/// <summary>Get the normalized form of this vector, which has a magnitude of one.</summary>
					public void Normalize(out Vector3i result)
					{
						double m = 1.0 / Math.Sqrt( X * X + Y * Y + Z * Z );
						 result.X = (int)(X * m);  result.Y = (int)(Y * m);  result.Z = (int)(Z * m); 					}

					/// <summary>Normalize this vector in place, giving it a magnitude of one. An identity vector will become NaN.</summary>
					public void NormalizeInPlace()
					{
						int m = (int)(1.0 / Math.Sqrt( X * X + Y * Y + Z * Z ));
						 X *= m;  Y *= m;  Z *= m; 					}
				
											/// <summary>Calculates the cross product of the vectors.</summary>
						/// <param name="other">The other vector to perform a cross product with.</param>
						/// <returns>The cross product.</returns>
						public Vector3i Cross(Vector3i other) { Cross(ref other, out other); return other; }

						/// <summary>Calculates the cross product of the vectors.</summary>
						/// <param name="other">The other vector to perform a cross product with.</param>
						/// <param name="result">Receives the cross product.</param>
						public void Cross(Vector3i other, out Vector3i result) { Cross(ref other, out result); }

						/// <summary>Calculates the cross product of the vectors.</summary>
						/// <param name="other">The other vector to perform a cross product with.</param>
						/// <param name="result">Receives the cross product.</param>
						public void Cross(ref Vector3i other, out Vector3i result)
						{
							int x = (int)(Y * other.Z - Z * other.Y), y = (int)(Z * other.X - X * other.Z), z = (int)(X * other.Y - Y * other.X);
							result.X = x;
							result.Y = y;
							result.Z = z;
						}

												
		#endregion Methods

			#region Fields

			/// <summary>The first axis of the <see cref="Vector3i"/>.</summary>
		public int X;
			/// <summary>The second axis of the <see cref="Vector3i"/>.</summary>
		public int Y;
			/// <summary>The third axis of the <see cref="Vector3i"/>.</summary>
		public int Z;
	
	#endregion Fields

	#region Properties

	public static readonly Vector3i Zero = new Vector3i((int)0, (int)0, (int)0);

	#endregion Properties

	#region Constructors

	/// <summary>Initialise a <see cref="Vector3i"/> from the provided values for each factor.</summary>
	public Vector3i(int x, int y, int z)
	{
					X = x;
					Y = y;
					Z = z;
			}

	/// <summary>Initialise a <see cref="Vector3i"/> from a list.</summary>
	public Vector3i(IList<int> list, int index = 0) : this(list[index + 0], list[index + 1], list[index + 2]) { }

	/// <summary>Initialise a <see cref="Vector3i"/> from a single scalar that is applied to all factors.</summary>
	public Vector3i(int value) : this(value, value, value) { }

	#endregion Constructors

	#region Methods

	/// <summary>Get whether this <see cref="Vector3i"/> has equal factors as the other <see cref="Vector3i"/>.</summary>
	public bool Equals(Vector3i other) {
		return  X == other.X && Y == other.Y && Z == other.Z ;
	}

	/// <summary>If the other object is a <see cref="Vector3i"/> of the same type, get whether this <see cref="Vector3i"/> has equal factors as it; otherwise return false./summary>
	public override bool Equals(object other)
	{
		if(other is Vector3i)
			return Equals((Vector3i)other);
		return base.Equals(other);
	}

	/// <summary>Compute a hash code from combining the axes.</summary>
	public override int GetHashCode()
	{
		return  X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode() ;
	}

	/// <summary>Convert this <see cref="Vector3i"/> to a string of the form "X, Y, Z".</summary>
	public string ToCommaSeparatedString(string format = null, IFormatProvider provider = null) {
		return X.ToString(format, provider) + ", " + Y.ToString(format, provider) + ", " + Z.ToString(format, provider);
	}

	/// <summary>Convert this <see cref="Vector3i"/> to a string of the form "Vector3i(X, Y, Z)".</summary>
	public override string ToString()
	{
		return ToString(null, null);
	}

	/// <summary>Convert this <see cref="Vector3i"/> to a string of the form "Vector3i(X, Y, Z)".</summary>
	public string ToString(string format, IFormatProvider provider)
	{
		return "Vector3i(" + ToCommaSeparatedString(format, provider) + ")";
	}

	/// <summary>Convert this <see cref="Vector3i"/> to a string of the form "{X, Y, Z".</summary>
	public string ToShortString(string format = null, IFormatProvider provider = null) { return "{" + ToCommaSeparatedString(format, provider) + "}"; }	

	#endregion Methods
	
	public static bool operator ==(Vector3i a, Vector3i b) { return a.X == b.X&&a.Y == b.Y&&a.Z == b.Z; }

	public static bool operator !=(Vector3i a, Vector3i b) { return a.X != b.X||a.Y != b.Y||a.Z != b.Z; }

		#region Operators
		

			// Casting
							public static implicit operator Vector3f(Vector3i a) { return new Vector3f((float)a.X, (float)a.Y, (float)a.Z); }
					public static implicit operator Vector3d(Vector3i a) { return new Vector3d((double)a.X, (double)a.Y, (double)a.Z); }
					public static explicit operator Vector3ui(Vector3i a) { return new Vector3ui((uint)a.X, (uint)a.Y, (uint)a.Z); }
					public static explicit operator Vector3rgb(Vector3i a) { return new Vector3rgb((NormalizedByte)a.X, (NormalizedByte)a.Y, (NormalizedByte)a.Z); }
					
				// Explicit casting to vectors with a lower order, trimming off axes.
									public static explicit operator Vector2i(Vector3i a)
					{
						return new Vector2i(a.X, a.Y);
					}
							
			public static Vector3i operator +(Vector3i a) { return new Vector3i((int)(+a.X), (int)(+a.Y), (int)(+a.Z)); }
			public static Vector3i operator -(Vector3i a) { return new Vector3i((int)(-a.X), (int)(-a.Y), (int)(-a.Z)); }
								public static Vector3i operator +(Vector3i a, Vector3i b) { return new Vector3i((int)(a.X + b.X), (int)(a.Y + b.Y), (int)(a.Z + b.Z)); }
					public static Vector3i operator +(Vector3i a, int b) { return new Vector3i((int)(a.X + b), (int)(a.Y + b), (int)(a.Z + b)); }
					public static Vector3i operator +(int a, Vector3i b) { return new Vector3i((int)(a + b.X), (int)(a + b.Y), (int)(a + b.Z)); }
									public static Vector3i operator -(Vector3i a, Vector3i b) { return new Vector3i((int)(a.X - b.X), (int)(a.Y - b.Y), (int)(a.Z - b.Z)); }
					public static Vector3i operator -(Vector3i a, int b) { return new Vector3i((int)(a.X - b), (int)(a.Y - b), (int)(a.Z - b)); }
					public static Vector3i operator -(int a, Vector3i b) { return new Vector3i((int)(a - b.X), (int)(a - b.Y), (int)(a - b.Z)); }
									public static Vector3i operator *(Vector3i a, Vector3i b) { return new Vector3i((int)(a.X * b.X), (int)(a.Y * b.Y), (int)(a.Z * b.Z)); }
					public static Vector3i operator *(Vector3i a, int b) { return new Vector3i((int)(a.X * b), (int)(a.Y * b), (int)(a.Z * b)); }
					public static Vector3i operator *(int a, Vector3i b) { return new Vector3i((int)(a * b.X), (int)(a * b.Y), (int)(a * b.Z)); }
									public static Vector3i operator /(Vector3i a, Vector3i b) { return new Vector3i((int)(a.X / b.X), (int)(a.Y / b.Y), (int)(a.Z / b.Z)); }
					public static Vector3i operator /(Vector3i a, int b) { return new Vector3i((int)(a.X / b), (int)(a.Y / b), (int)(a.Z / b)); }
					public static Vector3i operator /(int a, Vector3i b) { return new Vector3i((int)(a / b.X), (int)(a / b.Y), (int)(a / b.Z)); }
									public static Vector3i operator %(Vector3i a, Vector3i b) { return new Vector3i((int)(a.X % b.X), (int)(a.Y % b.Y), (int)(a.Z % b.Z)); }
					public static Vector3i operator %(Vector3i a, int b) { return new Vector3i((int)(a.X % b), (int)(a.Y % b), (int)(a.Z % b)); }
					public static Vector3i operator %(int a, Vector3i b) { return new Vector3i((int)(a % b.X), (int)(a % b.Y), (int)(a % b.Z)); }
				
		#endregion Operators
	}
	
	
	/// <summary>A three-dimensional vector type using <see cref="uint"/> elements.</summary>
	[VectorTypeAttribute(typeof(uint), 3, false)]
	public partial struct Vector3ui : IEquatable<Vector3ui>, IFormattable
	{
		#region Properties
		
			
							/// <summary>Get the product of multiplying all of the axes together.</summary>
				public uint Product { get { return (uint)( X * Y * Z ); } }
			
			/// <summary>Get the sum of all of the axes.</summary>
			public uint Sum { get { return (uint)( X + Y + Z ); } }

		#endregion Properties

		#region Constructors
		
			
			
							public static readonly Vector3ui One = new Vector3ui((uint)1, (uint)1, (uint)1);

									/// <summary>Get a normalized vector where X is 1 and all other axes are 0.</summary>
					public static readonly Vector3ui UnitX = new Vector3ui((uint)1, (uint)0, (uint)0);
									/// <summary>Get a normalized vector where Y is 1 and all other axes are 0.</summary>
					public static readonly Vector3ui UnitY = new Vector3ui((uint)0, (uint)1, (uint)0);
									/// <summary>Get a normalized vector where Z is 1 and all other axes are 0.</summary>
					public static readonly Vector3ui UnitZ = new Vector3ui((uint)0, (uint)0, (uint)1);
							

			// Constructors built up of smaller vectors.
												public Vector3ui( Vector2ui xy, uint z)
					{
													X = xy.X;
													Y = xy.Y;
										
						Z = z;
					}
				
					public Vector3ui(uint x,  Vector2ui y)
					{
						X = x;

													Y = y.X;
													Z = y.Y;
											}
									public Vector3ui( ref  Vector2ui xy, uint z)
					{
													X = xy.X;
													Y = xy.Y;
										
						Z = z;
					}
				
					public Vector3ui(uint x,  ref  Vector2ui y)
					{
						X = x;

													Y = y.X;
													Z = y.Y;
											}
							
			// Constructors from much smaller vectors.
			
		#endregion Constructors

		#region Methods

			// Methods that have joint ref forms.
							/// <summary>Compute the dot product of this and other vector, which is the sum of the product of each axis of each vector.</summary>
				/// <param name="other">The other vector to calculate a dot product with. </param>
				public uint Dot( Vector3ui other) { return  X * other.X + Y * other.Y + Z * other.Z ; }
							/// <summary>Compute the dot product of this and other vector, which is the sum of the product of each axis of each vector.</summary>
				/// <param name="other">The other vector to calculate a dot product with. The value of the parameter will not be changed; <c>ref</c> is used for optimisation only.</param>
				public uint Dot( ref  Vector3ui other) { return  X * other.X + Y * other.Y + Z * other.Z ; }
			
							public Vector3ui Clamp( Vector3ui min,  Vector3ui max )
				{
					Vector3ui result;
											result.X = X < min.X ? min.X : X > max.X ? max.X : X;
											result.Y = Y < min.Y ? min.Y : Y > max.Y ? max.Y : Y;
											result.Z = Z < min.Z ? min.Z : Z > max.Z ? max.Z : Z;
										return result;
				}
							public void Clamp( ref  Vector3ui min,  ref  Vector3ui max , out Vector3ui result)
				{
					
											result.X = X < min.X ? min.X : X > max.X ? max.X : X;
											result.Y = Y < min.Y ? min.Y : Y > max.Y ? max.Y : Y;
											result.Z = Z < min.Z ? min.Z : Z > max.Z ? max.Z : Z;
										return;
				}
			
			// Floating-point methods
			
		#endregion Methods

			#region Fields

			/// <summary>The first axis of the <see cref="Vector3ui"/>.</summary>
		public uint X;
			/// <summary>The second axis of the <see cref="Vector3ui"/>.</summary>
		public uint Y;
			/// <summary>The third axis of the <see cref="Vector3ui"/>.</summary>
		public uint Z;
	
	#endregion Fields

	#region Properties

	public static readonly Vector3ui Zero = new Vector3ui((uint)0, (uint)0, (uint)0);

	#endregion Properties

	#region Constructors

	/// <summary>Initialise a <see cref="Vector3ui"/> from the provided values for each factor.</summary>
	public Vector3ui(uint x, uint y, uint z)
	{
					X = x;
					Y = y;
					Z = z;
			}

	/// <summary>Initialise a <see cref="Vector3ui"/> from a list.</summary>
	public Vector3ui(IList<uint> list, int index = 0) : this(list[index + 0], list[index + 1], list[index + 2]) { }

	/// <summary>Initialise a <see cref="Vector3ui"/> from a single scalar that is applied to all factors.</summary>
	public Vector3ui(uint value) : this(value, value, value) { }

	#endregion Constructors

	#region Methods

	/// <summary>Get whether this <see cref="Vector3ui"/> has equal factors as the other <see cref="Vector3ui"/>.</summary>
	public bool Equals(Vector3ui other) {
		return  X == other.X && Y == other.Y && Z == other.Z ;
	}

	/// <summary>If the other object is a <see cref="Vector3ui"/> of the same type, get whether this <see cref="Vector3ui"/> has equal factors as it; otherwise return false./summary>
	public override bool Equals(object other)
	{
		if(other is Vector3ui)
			return Equals((Vector3ui)other);
		return base.Equals(other);
	}

	/// <summary>Compute a hash code from combining the axes.</summary>
	public override int GetHashCode()
	{
		return  X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode() ;
	}

	/// <summary>Convert this <see cref="Vector3ui"/> to a string of the form "X, Y, Z".</summary>
	public string ToCommaSeparatedString(string format = null, IFormatProvider provider = null) {
		return X.ToString(format, provider) + ", " + Y.ToString(format, provider) + ", " + Z.ToString(format, provider);
	}

	/// <summary>Convert this <see cref="Vector3ui"/> to a string of the form "Vector3ui(X, Y, Z)".</summary>
	public override string ToString()
	{
		return ToString(null, null);
	}

	/// <summary>Convert this <see cref="Vector3ui"/> to a string of the form "Vector3ui(X, Y, Z)".</summary>
	public string ToString(string format, IFormatProvider provider)
	{
		return "Vector3ui(" + ToCommaSeparatedString(format, provider) + ")";
	}

	/// <summary>Convert this <see cref="Vector3ui"/> to a string of the form "{X, Y, Z".</summary>
	public string ToShortString(string format = null, IFormatProvider provider = null) { return "{" + ToCommaSeparatedString(format, provider) + "}"; }	

	#endregion Methods
	
	public static bool operator ==(Vector3ui a, Vector3ui b) { return a.X == b.X&&a.Y == b.Y&&a.Z == b.Z; }

	public static bool operator !=(Vector3ui a, Vector3ui b) { return a.X != b.X||a.Y != b.Y||a.Z != b.Z; }

		#region Operators
		

			// Casting
							public static implicit operator Vector3f(Vector3ui a) { return new Vector3f((float)a.X, (float)a.Y, (float)a.Z); }
					public static implicit operator Vector3d(Vector3ui a) { return new Vector3d((double)a.X, (double)a.Y, (double)a.Z); }
					public static explicit operator Vector3i(Vector3ui a) { return new Vector3i((int)a.X, (int)a.Y, (int)a.Z); }
					public static explicit operator Vector3rgb(Vector3ui a) { return new Vector3rgb((NormalizedByte)a.X, (NormalizedByte)a.Y, (NormalizedByte)a.Z); }
					
				// Explicit casting to vectors with a lower order, trimming off axes.
									public static explicit operator Vector2ui(Vector3ui a)
					{
						return new Vector2ui(a.X, a.Y);
					}
							
			public static Vector3ui operator +(Vector3ui a) { return new Vector3ui((uint)(+a.X), (uint)(+a.Y), (uint)(+a.Z)); }
			public static Vector3ui operator -(Vector3ui a) { return new Vector3ui((uint)(-a.X), (uint)(-a.Y), (uint)(-a.Z)); }
								public static Vector3ui operator +(Vector3ui a, Vector3ui b) { return new Vector3ui((uint)(a.X + b.X), (uint)(a.Y + b.Y), (uint)(a.Z + b.Z)); }
					public static Vector3ui operator +(Vector3ui a, uint b) { return new Vector3ui((uint)(a.X + b), (uint)(a.Y + b), (uint)(a.Z + b)); }
					public static Vector3ui operator +(uint a, Vector3ui b) { return new Vector3ui((uint)(a + b.X), (uint)(a + b.Y), (uint)(a + b.Z)); }
									public static Vector3ui operator -(Vector3ui a, Vector3ui b) { return new Vector3ui((uint)(a.X - b.X), (uint)(a.Y - b.Y), (uint)(a.Z - b.Z)); }
					public static Vector3ui operator -(Vector3ui a, uint b) { return new Vector3ui((uint)(a.X - b), (uint)(a.Y - b), (uint)(a.Z - b)); }
					public static Vector3ui operator -(uint a, Vector3ui b) { return new Vector3ui((uint)(a - b.X), (uint)(a - b.Y), (uint)(a - b.Z)); }
									public static Vector3ui operator *(Vector3ui a, Vector3ui b) { return new Vector3ui((uint)(a.X * b.X), (uint)(a.Y * b.Y), (uint)(a.Z * b.Z)); }
					public static Vector3ui operator *(Vector3ui a, uint b) { return new Vector3ui((uint)(a.X * b), (uint)(a.Y * b), (uint)(a.Z * b)); }
					public static Vector3ui operator *(uint a, Vector3ui b) { return new Vector3ui((uint)(a * b.X), (uint)(a * b.Y), (uint)(a * b.Z)); }
									public static Vector3ui operator /(Vector3ui a, Vector3ui b) { return new Vector3ui((uint)(a.X / b.X), (uint)(a.Y / b.Y), (uint)(a.Z / b.Z)); }
					public static Vector3ui operator /(Vector3ui a, uint b) { return new Vector3ui((uint)(a.X / b), (uint)(a.Y / b), (uint)(a.Z / b)); }
					public static Vector3ui operator /(uint a, Vector3ui b) { return new Vector3ui((uint)(a / b.X), (uint)(a / b.Y), (uint)(a / b.Z)); }
									public static Vector3ui operator %(Vector3ui a, Vector3ui b) { return new Vector3ui((uint)(a.X % b.X), (uint)(a.Y % b.Y), (uint)(a.Z % b.Z)); }
					public static Vector3ui operator %(Vector3ui a, uint b) { return new Vector3ui((uint)(a.X % b), (uint)(a.Y % b), (uint)(a.Z % b)); }
					public static Vector3ui operator %(uint a, Vector3ui b) { return new Vector3ui((uint)(a % b.X), (uint)(a % b.Y), (uint)(a % b.Z)); }
									public static Vector3ui operator &(Vector3ui a, Vector3ui b) { return new Vector3ui((uint)(a.X & b.X), (uint)(a.Y & b.Y), (uint)(a.Z & b.Z)); }
					public static Vector3ui operator &(Vector3ui a, uint b) { return new Vector3ui((uint)(a.X & b), (uint)(a.Y & b), (uint)(a.Z & b)); }
					public static Vector3ui operator &(uint a, Vector3ui b) { return new Vector3ui((uint)(a & b.X), (uint)(a & b.Y), (uint)(a & b.Z)); }
									public static Vector3ui operator |(Vector3ui a, Vector3ui b) { return new Vector3ui((uint)(a.X | b.X), (uint)(a.Y | b.Y), (uint)(a.Z | b.Z)); }
					public static Vector3ui operator |(Vector3ui a, uint b) { return new Vector3ui((uint)(a.X | b), (uint)(a.Y | b), (uint)(a.Z | b)); }
					public static Vector3ui operator |(uint a, Vector3ui b) { return new Vector3ui((uint)(a | b.X), (uint)(a | b.Y), (uint)(a | b.Z)); }
									public static Vector3ui operator ^(Vector3ui a, Vector3ui b) { return new Vector3ui((uint)(a.X ^ b.X), (uint)(a.Y ^ b.Y), (uint)(a.Z ^ b.Z)); }
					public static Vector3ui operator ^(Vector3ui a, uint b) { return new Vector3ui((uint)(a.X ^ b), (uint)(a.Y ^ b), (uint)(a.Z ^ b)); }
					public static Vector3ui operator ^(uint a, Vector3ui b) { return new Vector3ui((uint)(a ^ b.X), (uint)(a ^ b.Y), (uint)(a ^ b.Z)); }
										public static Vector3ui operator <<(Vector3ui a, int b) { return new Vector3ui((uint)(a.X << b), (uint)(a.Y << b), (uint)(a.Z << b)); }
											public static Vector3ui operator >>(Vector3ui a, int b) { return new Vector3ui((uint)(a.X >> b), (uint)(a.Y >> b), (uint)(a.Z >> b)); }
					
		#endregion Operators
	}
	
	
	/// <summary>A three-dimensional vector type using <see cref="NormalizedByte"/> elements.</summary>
	[VectorTypeAttribute(typeof(NormalizedByte), 3, false)]
	public partial struct Vector3rgb : IEquatable<Vector3rgb>, IFormattable
	{
		#region Properties
		
							/// <summary>Compute the squared magnitude of the vector, which is the distance from the origin squared. Use <see cref="Magnitude"/> for the unsquared version, which is slower to compute.</summary>
				public double MagnitudeSquared { get { return  X * X + Y * Y + Z * Z ; } }
			
				/// <summary>Compute the magnitude of the vector, which is the distance from the origin.</summary>
				public double Magnitude
				{
					get
					{
						return (double)(							Math.Sqrt(
																	 X.Squared() 
								+									 Y.Squared() 
								+									 Z.Squared() 
															));
					}
				}

									/// <summary>Get the normalized form of this vector, with a magnitude of one.</summary>
					public Vector3rgb Normalized { get { Vector3rgb result; Normalize(out result); return result; } }
							
							/// <summary>Get the product of multiplying all of the axes together.</summary>
				public NormalizedByte Product { get { return (NormalizedByte)( X * Y * Z ); } }
			
			/// <summary>Get the sum of all of the axes.</summary>
			public NormalizedByte Sum { get { return (NormalizedByte)( X + Y + Z ); } }

		#endregion Properties

		#region Constructors
		
							/// <summary>Initialise a vector from double values that are converted into normalised form.</summary>
				public Vector3rgb(double x, double y, double z) : this((NormalizedByte)x, (NormalizedByte)y, (NormalizedByte)z) { }

									public Vector3rgb( Vector3d value) : this((NormalizedByte)value.X, (NormalizedByte)value.Y, (NormalizedByte)value.Z) { }
									public Vector3rgb( ref  Vector3d value) : this((NormalizedByte)value.X, (NormalizedByte)value.Y, (NormalizedByte)value.Z) { }
									
			
			
							public static readonly Vector3rgb One = new Vector3rgb((NormalizedByte)1, (NormalizedByte)1, (NormalizedByte)1);

									/// <summary>Get a normalized vector where X is 1 and all other axes are 0.</summary>
					public static readonly Vector3rgb UnitX = new Vector3rgb((NormalizedByte)1, (NormalizedByte)0, (NormalizedByte)0);
									/// <summary>Get a normalized vector where Y is 1 and all other axes are 0.</summary>
					public static readonly Vector3rgb UnitY = new Vector3rgb((NormalizedByte)0, (NormalizedByte)1, (NormalizedByte)0);
									/// <summary>Get a normalized vector where Z is 1 and all other axes are 0.</summary>
					public static readonly Vector3rgb UnitZ = new Vector3rgb((NormalizedByte)0, (NormalizedByte)0, (NormalizedByte)1);
							

			// Constructors built up of smaller vectors.
			
			// Constructors from much smaller vectors.
			
		#endregion Constructors

		#region Methods

			// Methods that have joint ref forms.
							/// <summary>Compute the dot product of this and other vector, which is the sum of the product of each axis of each vector.</summary>
				/// <param name="other">The other vector to calculate a dot product with. </param>
				public double Dot( Vector3rgb other) { return  X * other.X + Y * other.Y + Z * other.Z ; }
							/// <summary>Compute the dot product of this and other vector, which is the sum of the product of each axis of each vector.</summary>
				/// <param name="other">The other vector to calculate a dot product with. The value of the parameter will not be changed; <c>ref</c> is used for optimisation only.</param>
				public double Dot( ref  Vector3rgb other) { return  X * other.X + Y * other.Y + Z * other.Z ; }
			
							public Vector3rgb Clamp( Vector3rgb min,  Vector3rgb max )
				{
					Vector3rgb result;
											result.X = X < min.X ? min.X : X > max.X ? max.X : X;
											result.Y = Y < min.Y ? min.Y : Y > max.Y ? max.Y : Y;
											result.Z = Z < min.Z ? min.Z : Z > max.Z ? max.Z : Z;
										return result;
				}
							public void Clamp( ref  Vector3rgb min,  ref  Vector3rgb max , out Vector3rgb result)
				{
					
											result.X = X < min.X ? min.X : X > max.X ? max.X : X;
											result.Y = Y < min.Y ? min.Y : Y > max.Y ? max.Y : Y;
											result.Z = Z < min.Z ? min.Z : Z > max.Z ? max.Z : Z;
										return;
				}
			
			// Floating-point methods
												/// <summary>Get the distance between the vectors.</summary>
					public double Distance( Vector3rgb other)
					{
						return (double)(							Math.Sqrt(
																	 X - other.X .Squared() 
								+									 Y - other.Y .Squared() 
								+									 Z - other.Z .Squared() 
															));
					}

					/// <summary>Get the squared distance between the vectors.</summary>
					public double DistanceSquared( Vector3rgb other)
					{
						return 
															(X - other.X).Squared()
							+								(Y - other.Y).Squared()
							+								(Z - other.Z).Squared()
							;
					}
									/// <summary>Get the distance between the vectors.</summary>
					public double Distance( ref  Vector3rgb other)
					{
						return (double)(							Math.Sqrt(
																	 X - other.X .Squared() 
								+									 Y - other.Y .Squared() 
								+									 Z - other.Z .Squared() 
															));
					}

					/// <summary>Get the squared distance between the vectors.</summary>
					public double DistanceSquared( ref  Vector3rgb other)
					{
						return 
															(X - other.X).Squared()
							+								(Y - other.Y).Squared()
							+								(Z - other.Z).Squared()
							;
					}
				
									/// <summary>Get the normalized form of this vector, which has a magnitude of one.</summary>
					public void Normalize(out Vector3rgb result)
					{
						double m = 1.0 / Math.Sqrt( X * X + Y * Y + Z * Z );
						 result.X = (NormalizedByte)(X * m);  result.Y = (NormalizedByte)(Y * m);  result.Z = (NormalizedByte)(Z * m); 					}

					/// <summary>Normalize this vector in place, giving it a magnitude of one. An identity vector will become NaN.</summary>
					public void NormalizeInPlace()
					{
						NormalizedByte m = (NormalizedByte)(1.0 / Math.Sqrt( X * X + Y * Y + Z * Z ));
						 X *= m;  Y *= m;  Z *= m; 					}
				
											/// <summary>Calculates the cross product of the vectors.</summary>
						/// <param name="other">The other vector to perform a cross product with.</param>
						/// <returns>The cross product.</returns>
						public Vector3rgb Cross(Vector3rgb other) { Cross(ref other, out other); return other; }

						/// <summary>Calculates the cross product of the vectors.</summary>
						/// <param name="other">The other vector to perform a cross product with.</param>
						/// <param name="result">Receives the cross product.</param>
						public void Cross(Vector3rgb other, out Vector3rgb result) { Cross(ref other, out result); }

						/// <summary>Calculates the cross product of the vectors.</summary>
						/// <param name="other">The other vector to perform a cross product with.</param>
						/// <param name="result">Receives the cross product.</param>
						public void Cross(ref Vector3rgb other, out Vector3rgb result)
						{
							NormalizedByte x = (NormalizedByte)(Y * other.Z - Z * other.Y), y = (NormalizedByte)(Z * other.X - X * other.Z), z = (NormalizedByte)(X * other.Y - Y * other.X);
							result.X = x;
							result.Y = y;
							result.Z = z;
						}

												
		#endregion Methods

			#region Fields

			/// <summary>The first axis of the <see cref="Vector3rgb"/>.</summary>
		public NormalizedByte X;
			/// <summary>The second axis of the <see cref="Vector3rgb"/>.</summary>
		public NormalizedByte Y;
			/// <summary>The third axis of the <see cref="Vector3rgb"/>.</summary>
		public NormalizedByte Z;
	
	#endregion Fields

	#region Properties

	public static readonly Vector3rgb Zero = new Vector3rgb((NormalizedByte)0, (NormalizedByte)0, (NormalizedByte)0);

	#endregion Properties

	#region Constructors

	/// <summary>Initialise a <see cref="Vector3rgb"/> from the provided values for each factor.</summary>
	public Vector3rgb(NormalizedByte x, NormalizedByte y, NormalizedByte z)
	{
					X = x;
					Y = y;
					Z = z;
			}

	/// <summary>Initialise a <see cref="Vector3rgb"/> from a list.</summary>
	public Vector3rgb(IList<NormalizedByte> list, int index = 0) : this(list[index + 0], list[index + 1], list[index + 2]) { }

	/// <summary>Initialise a <see cref="Vector3rgb"/> from a single scalar that is applied to all factors.</summary>
	public Vector3rgb(NormalizedByte value) : this(value, value, value) { }

	#endregion Constructors

	#region Methods

	/// <summary>Get whether this <see cref="Vector3rgb"/> has equal factors as the other <see cref="Vector3rgb"/>.</summary>
	public bool Equals(Vector3rgb other) {
		return  X == other.X && Y == other.Y && Z == other.Z ;
	}

	/// <summary>If the other object is a <see cref="Vector3rgb"/> of the same type, get whether this <see cref="Vector3rgb"/> has equal factors as it; otherwise return false./summary>
	public override bool Equals(object other)
	{
		if(other is Vector3rgb)
			return Equals((Vector3rgb)other);
		return base.Equals(other);
	}

	/// <summary>Compute a hash code from combining the axes.</summary>
	public override int GetHashCode()
	{
		return  X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode() ;
	}

	/// <summary>Convert this <see cref="Vector3rgb"/> to a string of the form "X, Y, Z".</summary>
	public string ToCommaSeparatedString(string format = null, IFormatProvider provider = null) {
		return X.ToString(format, provider) + ", " + Y.ToString(format, provider) + ", " + Z.ToString(format, provider);
	}

	/// <summary>Convert this <see cref="Vector3rgb"/> to a string of the form "Vector3rgb(X, Y, Z)".</summary>
	public override string ToString()
	{
		return ToString(null, null);
	}

	/// <summary>Convert this <see cref="Vector3rgb"/> to a string of the form "Vector3rgb(X, Y, Z)".</summary>
	public string ToString(string format, IFormatProvider provider)
	{
		return "Vector3rgb(" + ToCommaSeparatedString(format, provider) + ")";
	}

	/// <summary>Convert this <see cref="Vector3rgb"/> to a string of the form "{X, Y, Z".</summary>
	public string ToShortString(string format = null, IFormatProvider provider = null) { return "{" + ToCommaSeparatedString(format, provider) + "}"; }	

	#endregion Methods
	
	public static bool operator ==(Vector3rgb a, Vector3rgb b) { return a.X == b.X&&a.Y == b.Y&&a.Z == b.Z; }

	public static bool operator !=(Vector3rgb a, Vector3rgb b) { return a.X != b.X||a.Y != b.Y||a.Z != b.Z; }

		#region Operators
		

			// Casting
							public static implicit operator Vector3f(Vector3rgb a) { return new Vector3f((float)a.X, (float)a.Y, (float)a.Z); }
					public static implicit operator Vector3d(Vector3rgb a) { return new Vector3d((double)a.X, (double)a.Y, (double)a.Z); }
					public static explicit operator Vector3i(Vector3rgb a) { return new Vector3i((int)a.X, (int)a.Y, (int)a.Z); }
					public static explicit operator Vector3ui(Vector3rgb a) { return new Vector3ui((uint)a.X, (uint)a.Y, (uint)a.Z); }
					
				// Explicit casting to vectors with a lower order, trimming off axes.
							
			public static Vector3rgb operator +(Vector3rgb a) { return new Vector3rgb((NormalizedByte)(+a.X), (NormalizedByte)(+a.Y), (NormalizedByte)(+a.Z)); }
			public static Vector3rgb operator -(Vector3rgb a) { return new Vector3rgb((NormalizedByte)(-a.X), (NormalizedByte)(-a.Y), (NormalizedByte)(-a.Z)); }
								public static Vector3rgb operator +(Vector3rgb a, Vector3rgb b) { return new Vector3rgb((NormalizedByte)(a.X + b.X), (NormalizedByte)(a.Y + b.Y), (NormalizedByte)(a.Z + b.Z)); }
					public static Vector3rgb operator +(Vector3rgb a, NormalizedByte b) { return new Vector3rgb((NormalizedByte)(a.X + b), (NormalizedByte)(a.Y + b), (NormalizedByte)(a.Z + b)); }
					public static Vector3rgb operator +(NormalizedByte a, Vector3rgb b) { return new Vector3rgb((NormalizedByte)(a + b.X), (NormalizedByte)(a + b.Y), (NormalizedByte)(a + b.Z)); }
									public static Vector3rgb operator -(Vector3rgb a, Vector3rgb b) { return new Vector3rgb((NormalizedByte)(a.X - b.X), (NormalizedByte)(a.Y - b.Y), (NormalizedByte)(a.Z - b.Z)); }
					public static Vector3rgb operator -(Vector3rgb a, NormalizedByte b) { return new Vector3rgb((NormalizedByte)(a.X - b), (NormalizedByte)(a.Y - b), (NormalizedByte)(a.Z - b)); }
					public static Vector3rgb operator -(NormalizedByte a, Vector3rgb b) { return new Vector3rgb((NormalizedByte)(a - b.X), (NormalizedByte)(a - b.Y), (NormalizedByte)(a - b.Z)); }
									public static Vector3rgb operator *(Vector3rgb a, Vector3rgb b) { return new Vector3rgb((NormalizedByte)(a.X * b.X), (NormalizedByte)(a.Y * b.Y), (NormalizedByte)(a.Z * b.Z)); }
					public static Vector3rgb operator *(Vector3rgb a, NormalizedByte b) { return new Vector3rgb((NormalizedByte)(a.X * b), (NormalizedByte)(a.Y * b), (NormalizedByte)(a.Z * b)); }
					public static Vector3rgb operator *(NormalizedByte a, Vector3rgb b) { return new Vector3rgb((NormalizedByte)(a * b.X), (NormalizedByte)(a * b.Y), (NormalizedByte)(a * b.Z)); }
									public static Vector3rgb operator /(Vector3rgb a, Vector3rgb b) { return new Vector3rgb((NormalizedByte)(a.X / b.X), (NormalizedByte)(a.Y / b.Y), (NormalizedByte)(a.Z / b.Z)); }
					public static Vector3rgb operator /(Vector3rgb a, NormalizedByte b) { return new Vector3rgb((NormalizedByte)(a.X / b), (NormalizedByte)(a.Y / b), (NormalizedByte)(a.Z / b)); }
					public static Vector3rgb operator /(NormalizedByte a, Vector3rgb b) { return new Vector3rgb((NormalizedByte)(a / b.X), (NormalizedByte)(a / b.Y), (NormalizedByte)(a / b.Z)); }
									public static Vector3rgb operator %(Vector3rgb a, Vector3rgb b) { return new Vector3rgb((NormalizedByte)(a.X % b.X), (NormalizedByte)(a.Y % b.Y), (NormalizedByte)(a.Z % b.Z)); }
					public static Vector3rgb operator %(Vector3rgb a, NormalizedByte b) { return new Vector3rgb((NormalizedByte)(a.X % b), (NormalizedByte)(a.Y % b), (NormalizedByte)(a.Z % b)); }
					public static Vector3rgb operator %(NormalizedByte a, Vector3rgb b) { return new Vector3rgb((NormalizedByte)(a % b.X), (NormalizedByte)(a % b.Y), (NormalizedByte)(a % b.Z)); }
				
		#endregion Operators
	}
	
	
	/// <summary>A four-dimensional vector type using <see cref="float"/> elements.</summary>
	[VectorTypeAttribute(typeof(float), 4, false)]
	public partial struct Vector4f : IEquatable<Vector4f>, IFormattable
	{
		#region Properties
		
							/// <summary>Compute the squared magnitude of the vector, which is the distance from the origin squared. Use <see cref="Magnitude"/> for the unsquared version, which is slower to compute.</summary>
				public float MagnitudeSquared { get { return  X * X + Y * Y + Z * Z + W * W ; } }
			
				/// <summary>Compute the magnitude of the vector, which is the distance from the origin.</summary>
				public float Magnitude
				{
					get
					{
						return (float)(							Math.Sqrt(
																	 X.Squared() 
								+									 Y.Squared() 
								+									 Z.Squared() 
								+									 W.Squared() 
															));
					}
				}

									/// <summary>Get the normalized form of this vector, with a magnitude of one.</summary>
					public Vector4f Normalized { get { Vector4f result; Normalize(out result); return result; } }
							
							/// <summary>Get the product of multiplying all of the axes together.</summary>
				public float Product { get { return (float)( X * Y * Z * W ); } }
			
			/// <summary>Get the sum of all of the axes.</summary>
			public float Sum { get { return (float)( X + Y + Z + W ); } }

		#endregion Properties

		#region Constructors
		
			
			
							public static readonly Vector4f One = new Vector4f((float)1, (float)1, (float)1, (float)1);

									/// <summary>Get a normalized vector where X is 1 and all other axes are 0.</summary>
					public static readonly Vector4f UnitX = new Vector4f((float)1, (float)0, (float)0, (float)0);
									/// <summary>Get a normalized vector where Y is 1 and all other axes are 0.</summary>
					public static readonly Vector4f UnitY = new Vector4f((float)0, (float)1, (float)0, (float)0);
									/// <summary>Get a normalized vector where Z is 1 and all other axes are 0.</summary>
					public static readonly Vector4f UnitZ = new Vector4f((float)0, (float)0, (float)1, (float)0);
									/// <summary>Get a normalized vector where W is 1 and all other axes are 0.</summary>
					public static readonly Vector4f UnitW = new Vector4f((float)0, (float)0, (float)0, (float)1);
							

			// Constructors built up of smaller vectors.
												public Vector4f( Vector3f xyz, float w)
					{
													X = xyz.X;
													Y = xyz.Y;
													Z = xyz.Z;
										
						W = w;
					}
				
					public Vector4f(float x,  Vector3f yz)
					{
						X = x;

													Y = yz.X;
													Z = yz.Y;
													W = yz.Z;
											}
									public Vector4f( ref  Vector3f xyz, float w)
					{
													X = xyz.X;
													Y = xyz.Y;
													Z = xyz.Z;
										
						W = w;
					}
				
					public Vector4f(float x,  ref  Vector3f yz)
					{
						X = x;

													Y = yz.X;
													Z = yz.Y;
													W = yz.Z;
											}
							
			// Constructors from much smaller vectors.
												public Vector4f( Vector2f xy, float z, float w) { X = xy.X; Y = xy.Y; Z = z; W = w; }
					public Vector4f(float x,  Vector2f yz, float w) { X = x; Y = yz.X; Z = yz.Y; W = w; }
					public Vector4f(float x, float y,  Vector2f zw) { X = x; Y = y; Z = zw.X; W = zw.Y; }
					public Vector4f( Vector2f xy,  Vector2f zw) { X = xy.X; Y = xy.Y; Z = zw.X; W = zw.Y; }
									public Vector4f( ref  Vector2f xy, float z, float w) { X = xy.X; Y = xy.Y; Z = z; W = w; }
					public Vector4f(float x,  ref  Vector2f yz, float w) { X = x; Y = yz.X; Z = yz.Y; W = w; }
					public Vector4f(float x, float y,  ref  Vector2f zw) { X = x; Y = y; Z = zw.X; W = zw.Y; }
					public Vector4f( ref  Vector2f xy,  ref  Vector2f zw) { X = xy.X; Y = xy.Y; Z = zw.X; W = zw.Y; }
							
		#endregion Constructors

		#region Methods

			// Methods that have joint ref forms.
							/// <summary>Compute the dot product of this and other vector, which is the sum of the product of each axis of each vector.</summary>
				/// <param name="other">The other vector to calculate a dot product with. </param>
				public float Dot( Vector4f other) { return  X * other.X + Y * other.Y + Z * other.Z + W * other.W ; }
							/// <summary>Compute the dot product of this and other vector, which is the sum of the product of each axis of each vector.</summary>
				/// <param name="other">The other vector to calculate a dot product with. The value of the parameter will not be changed; <c>ref</c> is used for optimisation only.</param>
				public float Dot( ref  Vector4f other) { return  X * other.X + Y * other.Y + Z * other.Z + W * other.W ; }
			
							public Vector4f Clamp( Vector4f min,  Vector4f max )
				{
					Vector4f result;
											result.X = X < min.X ? min.X : X > max.X ? max.X : X;
											result.Y = Y < min.Y ? min.Y : Y > max.Y ? max.Y : Y;
											result.Z = Z < min.Z ? min.Z : Z > max.Z ? max.Z : Z;
											result.W = W < min.W ? min.W : W > max.W ? max.W : W;
										return result;
				}
							public void Clamp( ref  Vector4f min,  ref  Vector4f max , out Vector4f result)
				{
					
											result.X = X < min.X ? min.X : X > max.X ? max.X : X;
											result.Y = Y < min.Y ? min.Y : Y > max.Y ? max.Y : Y;
											result.Z = Z < min.Z ? min.Z : Z > max.Z ? max.Z : Z;
											result.W = W < min.W ? min.W : W > max.W ? max.W : W;
										return;
				}
			
			// Floating-point methods
												/// <summary>Get the distance between the vectors.</summary>
					public float Distance( Vector4f other)
					{
						return (float)(							Math.Sqrt(
																	 X - other.X .Squared() 
								+									 Y - other.Y .Squared() 
								+									 Z - other.Z .Squared() 
								+									 W - other.W .Squared() 
															));
					}

					/// <summary>Get the squared distance between the vectors.</summary>
					public float DistanceSquared( Vector4f other)
					{
						return 
															(X - other.X).Squared()
							+								(Y - other.Y).Squared()
							+								(Z - other.Z).Squared()
							+								(W - other.W).Squared()
							;
					}
									/// <summary>Get the distance between the vectors.</summary>
					public float Distance( ref  Vector4f other)
					{
						return (float)(							Math.Sqrt(
																	 X - other.X .Squared() 
								+									 Y - other.Y .Squared() 
								+									 Z - other.Z .Squared() 
								+									 W - other.W .Squared() 
															));
					}

					/// <summary>Get the squared distance between the vectors.</summary>
					public float DistanceSquared( ref  Vector4f other)
					{
						return 
															(X - other.X).Squared()
							+								(Y - other.Y).Squared()
							+								(Z - other.Z).Squared()
							+								(W - other.W).Squared()
							;
					}
				
									/// <summary>Get the normalized form of this vector, which has a magnitude of one.</summary>
					public void Normalize(out Vector4f result)
					{
						double m = 1.0 / Math.Sqrt( X * X + Y * Y + Z * Z + W * W );
						 result.X = (float)(X * m);  result.Y = (float)(Y * m);  result.Z = (float)(Z * m);  result.W = (float)(W * m); 					}

					/// <summary>Normalize this vector in place, giving it a magnitude of one. An identity vector will become NaN.</summary>
					public void NormalizeInPlace()
					{
						float m = (float)(1.0 / Math.Sqrt( X * X + Y * Y + Z * Z + W * W ));
						 X *= m;  Y *= m;  Z *= m;  W *= m; 					}
				
												
		#endregion Methods

			#region Fields

			/// <summary>The first axis of the <see cref="Vector4f"/>.</summary>
		public float X;
			/// <summary>The second axis of the <see cref="Vector4f"/>.</summary>
		public float Y;
			/// <summary>The third axis of the <see cref="Vector4f"/>.</summary>
		public float Z;
			/// <summary>The fourth axis of the <see cref="Vector4f"/>.</summary>
		public float W;
	
	#endregion Fields

	#region Properties

	public static readonly Vector4f Zero = new Vector4f((float)0, (float)0, (float)0, (float)0);

	#endregion Properties

	#region Constructors

	/// <summary>Initialise a <see cref="Vector4f"/> from the provided values for each factor.</summary>
	public Vector4f(float x, float y, float z, float w)
	{
					X = x;
					Y = y;
					Z = z;
					W = w;
			}

	/// <summary>Initialise a <see cref="Vector4f"/> from a list.</summary>
	public Vector4f(IList<float> list, int index = 0) : this(list[index + 0], list[index + 1], list[index + 2], list[index + 3]) { }

	/// <summary>Initialise a <see cref="Vector4f"/> from a single scalar that is applied to all factors.</summary>
	public Vector4f(float value) : this(value, value, value, value) { }

	#endregion Constructors

	#region Methods

	/// <summary>Get whether this <see cref="Vector4f"/> has equal factors as the other <see cref="Vector4f"/>.</summary>
	public bool Equals(Vector4f other) {
		return  X == other.X && Y == other.Y && Z == other.Z && W == other.W ;
	}

	/// <summary>If the other object is a <see cref="Vector4f"/> of the same type, get whether this <see cref="Vector4f"/> has equal factors as it; otherwise return false./summary>
	public override bool Equals(object other)
	{
		if(other is Vector4f)
			return Equals((Vector4f)other);
		return base.Equals(other);
	}

	/// <summary>Compute a hash code from combining the axes.</summary>
	public override int GetHashCode()
	{
		return  X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode() ^ W.GetHashCode() ;
	}

	/// <summary>Convert this <see cref="Vector4f"/> to a string of the form "X, Y, Z, W".</summary>
	public string ToCommaSeparatedString(string format = null, IFormatProvider provider = null) {
		return X.ToString(format, provider) + ", " + Y.ToString(format, provider) + ", " + Z.ToString(format, provider) + ", " + W.ToString(format, provider);
	}

	/// <summary>Convert this <see cref="Vector4f"/> to a string of the form "Vector4f(X, Y, Z, W)".</summary>
	public override string ToString()
	{
		return ToString(null, null);
	}

	/// <summary>Convert this <see cref="Vector4f"/> to a string of the form "Vector4f(X, Y, Z, W)".</summary>
	public string ToString(string format, IFormatProvider provider)
	{
		return "Vector4f(" + ToCommaSeparatedString(format, provider) + ")";
	}

	/// <summary>Convert this <see cref="Vector4f"/> to a string of the form "{X, Y, Z, W".</summary>
	public string ToShortString(string format = null, IFormatProvider provider = null) { return "{" + ToCommaSeparatedString(format, provider) + "}"; }	

	#endregion Methods
	
	public static bool operator ==(Vector4f a, Vector4f b) { return a.X == b.X&&a.Y == b.Y&&a.Z == b.Z&&a.W == b.W; }

	public static bool operator !=(Vector4f a, Vector4f b) { return a.X != b.X||a.Y != b.Y||a.Z != b.Z||a.W != b.W; }

		#region Operators
		

			// Casting
							public static implicit operator Vector4d(Vector4f a) { return new Vector4d((double)a.X, (double)a.Y, (double)a.Z, (double)a.W); }
					public static explicit operator Vector4i(Vector4f a) { return new Vector4i((int)a.X, (int)a.Y, (int)a.Z, (int)a.W); }
					public static explicit operator Vector4ui(Vector4f a) { return new Vector4ui((uint)a.X, (uint)a.Y, (uint)a.Z, (uint)a.W); }
					public static explicit operator Vector4b(Vector4f a) { return new Vector4b((byte)a.X, (byte)a.Y, (byte)a.Z, (byte)a.W); }
					public static explicit operator Vector4nb(Vector4f a) { return new Vector4nb((NormalizedByte)a.X, (NormalizedByte)a.Y, (NormalizedByte)a.Z, (NormalizedByte)a.W); }
					public static explicit operator Vector4nsb(Vector4f a) { return new Vector4nsb((NormalizedSByte)a.X, (NormalizedSByte)a.Y, (NormalizedSByte)a.Z, (NormalizedSByte)a.W); }
					public static explicit operator Vector4rgba(Vector4f a) { return new Vector4rgba((NormalizedByte)a.X, (NormalizedByte)a.Y, (NormalizedByte)a.Z, (NormalizedByte)a.W); }
					public static explicit operator Vector4h(Vector4f a) { return new Vector4h((Float16)a.X, (Float16)a.Y, (Float16)a.Z, (Float16)a.W); }
					
				// Explicit casting to vectors with a lower order, trimming off axes.
									public static explicit operator Vector3f(Vector4f a)
					{
						return new Vector3f(a.X, a.Y, a.Z);
					}
									public static explicit operator Vector2f(Vector4f a)
					{
						return new Vector2f(a.X, a.Y);
					}
							
			public static Vector4f operator +(Vector4f a) { return new Vector4f((float)(+a.X), (float)(+a.Y), (float)(+a.Z), (float)(+a.W)); }
			public static Vector4f operator -(Vector4f a) { return new Vector4f((float)(-a.X), (float)(-a.Y), (float)(-a.Z), (float)(-a.W)); }
								public static Vector4f operator +(Vector4f a, Vector4f b) { return new Vector4f((float)(a.X + b.X), (float)(a.Y + b.Y), (float)(a.Z + b.Z), (float)(a.W + b.W)); }
					public static Vector4f operator +(Vector4f a, float b) { return new Vector4f((float)(a.X + b), (float)(a.Y + b), (float)(a.Z + b), (float)(a.W + b)); }
					public static Vector4f operator +(float a, Vector4f b) { return new Vector4f((float)(a + b.X), (float)(a + b.Y), (float)(a + b.Z), (float)(a + b.W)); }
									public static Vector4f operator -(Vector4f a, Vector4f b) { return new Vector4f((float)(a.X - b.X), (float)(a.Y - b.Y), (float)(a.Z - b.Z), (float)(a.W - b.W)); }
					public static Vector4f operator -(Vector4f a, float b) { return new Vector4f((float)(a.X - b), (float)(a.Y - b), (float)(a.Z - b), (float)(a.W - b)); }
					public static Vector4f operator -(float a, Vector4f b) { return new Vector4f((float)(a - b.X), (float)(a - b.Y), (float)(a - b.Z), (float)(a - b.W)); }
									public static Vector4f operator *(Vector4f a, Vector4f b) { return new Vector4f((float)(a.X * b.X), (float)(a.Y * b.Y), (float)(a.Z * b.Z), (float)(a.W * b.W)); }
					public static Vector4f operator *(Vector4f a, float b) { return new Vector4f((float)(a.X * b), (float)(a.Y * b), (float)(a.Z * b), (float)(a.W * b)); }
					public static Vector4f operator *(float a, Vector4f b) { return new Vector4f((float)(a * b.X), (float)(a * b.Y), (float)(a * b.Z), (float)(a * b.W)); }
									public static Vector4f operator /(Vector4f a, Vector4f b) { return new Vector4f((float)(a.X / b.X), (float)(a.Y / b.Y), (float)(a.Z / b.Z), (float)(a.W / b.W)); }
					public static Vector4f operator /(Vector4f a, float b) { return new Vector4f((float)(a.X / b), (float)(a.Y / b), (float)(a.Z / b), (float)(a.W / b)); }
					public static Vector4f operator /(float a, Vector4f b) { return new Vector4f((float)(a / b.X), (float)(a / b.Y), (float)(a / b.Z), (float)(a / b.W)); }
									public static Vector4f operator %(Vector4f a, Vector4f b) { return new Vector4f((float)(a.X % b.X), (float)(a.Y % b.Y), (float)(a.Z % b.Z), (float)(a.W % b.W)); }
					public static Vector4f operator %(Vector4f a, float b) { return new Vector4f((float)(a.X % b), (float)(a.Y % b), (float)(a.Z % b), (float)(a.W % b)); }
					public static Vector4f operator %(float a, Vector4f b) { return new Vector4f((float)(a % b.X), (float)(a % b.Y), (float)(a % b.Z), (float)(a % b.W)); }
				
		#endregion Operators
	}
	
	
	/// <summary>A four-dimensional vector type using <see cref="double"/> elements.</summary>
	[VectorTypeAttribute(typeof(double), 4, false)]
	public partial struct Vector4d : IEquatable<Vector4d>, IFormattable
	{
		#region Properties
		
							/// <summary>Compute the squared magnitude of the vector, which is the distance from the origin squared. Use <see cref="Magnitude"/> for the unsquared version, which is slower to compute.</summary>
				public double MagnitudeSquared { get { return  X * X + Y * Y + Z * Z + W * W ; } }
			
				/// <summary>Compute the magnitude of the vector, which is the distance from the origin.</summary>
				public double Magnitude
				{
					get
					{
						return (double)(							Math.Sqrt(
																	 X.Squared() 
								+									 Y.Squared() 
								+									 Z.Squared() 
								+									 W.Squared() 
															));
					}
				}

									/// <summary>Get the normalized form of this vector, with a magnitude of one.</summary>
					public Vector4d Normalized { get { Vector4d result; Normalize(out result); return result; } }
							
							/// <summary>Get the product of multiplying all of the axes together.</summary>
				public double Product { get { return (double)( X * Y * Z * W ); } }
			
			/// <summary>Get the sum of all of the axes.</summary>
			public double Sum { get { return (double)( X + Y + Z + W ); } }

		#endregion Properties

		#region Constructors
		
			
			
							public static readonly Vector4d One = new Vector4d((double)1, (double)1, (double)1, (double)1);

									/// <summary>Get a normalized vector where X is 1 and all other axes are 0.</summary>
					public static readonly Vector4d UnitX = new Vector4d((double)1, (double)0, (double)0, (double)0);
									/// <summary>Get a normalized vector where Y is 1 and all other axes are 0.</summary>
					public static readonly Vector4d UnitY = new Vector4d((double)0, (double)1, (double)0, (double)0);
									/// <summary>Get a normalized vector where Z is 1 and all other axes are 0.</summary>
					public static readonly Vector4d UnitZ = new Vector4d((double)0, (double)0, (double)1, (double)0);
									/// <summary>Get a normalized vector where W is 1 and all other axes are 0.</summary>
					public static readonly Vector4d UnitW = new Vector4d((double)0, (double)0, (double)0, (double)1);
							

			// Constructors built up of smaller vectors.
												public Vector4d( Vector3d xyz, double w)
					{
													X = xyz.X;
													Y = xyz.Y;
													Z = xyz.Z;
										
						W = w;
					}
				
					public Vector4d(double x,  Vector3d yz)
					{
						X = x;

													Y = yz.X;
													Z = yz.Y;
													W = yz.Z;
											}
									public Vector4d( ref  Vector3d xyz, double w)
					{
													X = xyz.X;
													Y = xyz.Y;
													Z = xyz.Z;
										
						W = w;
					}
				
					public Vector4d(double x,  ref  Vector3d yz)
					{
						X = x;

													Y = yz.X;
													Z = yz.Y;
													W = yz.Z;
											}
							
			// Constructors from much smaller vectors.
												public Vector4d( Vector2d xy, double z, double w) { X = xy.X; Y = xy.Y; Z = z; W = w; }
					public Vector4d(double x,  Vector2d yz, double w) { X = x; Y = yz.X; Z = yz.Y; W = w; }
					public Vector4d(double x, double y,  Vector2d zw) { X = x; Y = y; Z = zw.X; W = zw.Y; }
					public Vector4d( Vector2d xy,  Vector2d zw) { X = xy.X; Y = xy.Y; Z = zw.X; W = zw.Y; }
									public Vector4d( ref  Vector2d xy, double z, double w) { X = xy.X; Y = xy.Y; Z = z; W = w; }
					public Vector4d(double x,  ref  Vector2d yz, double w) { X = x; Y = yz.X; Z = yz.Y; W = w; }
					public Vector4d(double x, double y,  ref  Vector2d zw) { X = x; Y = y; Z = zw.X; W = zw.Y; }
					public Vector4d( ref  Vector2d xy,  ref  Vector2d zw) { X = xy.X; Y = xy.Y; Z = zw.X; W = zw.Y; }
							
		#endregion Constructors

		#region Methods

			// Methods that have joint ref forms.
							/// <summary>Compute the dot product of this and other vector, which is the sum of the product of each axis of each vector.</summary>
				/// <param name="other">The other vector to calculate a dot product with. </param>
				public double Dot( Vector4d other) { return  X * other.X + Y * other.Y + Z * other.Z + W * other.W ; }
							/// <summary>Compute the dot product of this and other vector, which is the sum of the product of each axis of each vector.</summary>
				/// <param name="other">The other vector to calculate a dot product with. The value of the parameter will not be changed; <c>ref</c> is used for optimisation only.</param>
				public double Dot( ref  Vector4d other) { return  X * other.X + Y * other.Y + Z * other.Z + W * other.W ; }
			
							public Vector4d Clamp( Vector4d min,  Vector4d max )
				{
					Vector4d result;
											result.X = X < min.X ? min.X : X > max.X ? max.X : X;
											result.Y = Y < min.Y ? min.Y : Y > max.Y ? max.Y : Y;
											result.Z = Z < min.Z ? min.Z : Z > max.Z ? max.Z : Z;
											result.W = W < min.W ? min.W : W > max.W ? max.W : W;
										return result;
				}
							public void Clamp( ref  Vector4d min,  ref  Vector4d max , out Vector4d result)
				{
					
											result.X = X < min.X ? min.X : X > max.X ? max.X : X;
											result.Y = Y < min.Y ? min.Y : Y > max.Y ? max.Y : Y;
											result.Z = Z < min.Z ? min.Z : Z > max.Z ? max.Z : Z;
											result.W = W < min.W ? min.W : W > max.W ? max.W : W;
										return;
				}
			
			// Floating-point methods
												/// <summary>Get the distance between the vectors.</summary>
					public double Distance( Vector4d other)
					{
						return (double)(							Math.Sqrt(
																	 X - other.X .Squared() 
								+									 Y - other.Y .Squared() 
								+									 Z - other.Z .Squared() 
								+									 W - other.W .Squared() 
															));
					}

					/// <summary>Get the squared distance between the vectors.</summary>
					public double DistanceSquared( Vector4d other)
					{
						return 
															(X - other.X).Squared()
							+								(Y - other.Y).Squared()
							+								(Z - other.Z).Squared()
							+								(W - other.W).Squared()
							;
					}
									/// <summary>Get the distance between the vectors.</summary>
					public double Distance( ref  Vector4d other)
					{
						return (double)(							Math.Sqrt(
																	 X - other.X .Squared() 
								+									 Y - other.Y .Squared() 
								+									 Z - other.Z .Squared() 
								+									 W - other.W .Squared() 
															));
					}

					/// <summary>Get the squared distance between the vectors.</summary>
					public double DistanceSquared( ref  Vector4d other)
					{
						return 
															(X - other.X).Squared()
							+								(Y - other.Y).Squared()
							+								(Z - other.Z).Squared()
							+								(W - other.W).Squared()
							;
					}
				
									/// <summary>Get the normalized form of this vector, which has a magnitude of one.</summary>
					public void Normalize(out Vector4d result)
					{
						double m = 1.0 / Math.Sqrt( X * X + Y * Y + Z * Z + W * W );
						 result.X = (double)(X * m);  result.Y = (double)(Y * m);  result.Z = (double)(Z * m);  result.W = (double)(W * m); 					}

					/// <summary>Normalize this vector in place, giving it a magnitude of one. An identity vector will become NaN.</summary>
					public void NormalizeInPlace()
					{
						double m = (double)(1.0 / Math.Sqrt( X * X + Y * Y + Z * Z + W * W ));
						 X *= m;  Y *= m;  Z *= m;  W *= m; 					}
				
												
		#endregion Methods

			#region Fields

			/// <summary>The first axis of the <see cref="Vector4d"/>.</summary>
		public double X;
			/// <summary>The second axis of the <see cref="Vector4d"/>.</summary>
		public double Y;
			/// <summary>The third axis of the <see cref="Vector4d"/>.</summary>
		public double Z;
			/// <summary>The fourth axis of the <see cref="Vector4d"/>.</summary>
		public double W;
	
	#endregion Fields

	#region Properties

	public static readonly Vector4d Zero = new Vector4d((double)0, (double)0, (double)0, (double)0);

	#endregion Properties

	#region Constructors

	/// <summary>Initialise a <see cref="Vector4d"/> from the provided values for each factor.</summary>
	public Vector4d(double x, double y, double z, double w)
	{
					X = x;
					Y = y;
					Z = z;
					W = w;
			}

	/// <summary>Initialise a <see cref="Vector4d"/> from a list.</summary>
	public Vector4d(IList<double> list, int index = 0) : this(list[index + 0], list[index + 1], list[index + 2], list[index + 3]) { }

	/// <summary>Initialise a <see cref="Vector4d"/> from a single scalar that is applied to all factors.</summary>
	public Vector4d(double value) : this(value, value, value, value) { }

	#endregion Constructors

	#region Methods

	/// <summary>Get whether this <see cref="Vector4d"/> has equal factors as the other <see cref="Vector4d"/>.</summary>
	public bool Equals(Vector4d other) {
		return  X == other.X && Y == other.Y && Z == other.Z && W == other.W ;
	}

	/// <summary>If the other object is a <see cref="Vector4d"/> of the same type, get whether this <see cref="Vector4d"/> has equal factors as it; otherwise return false./summary>
	public override bool Equals(object other)
	{
		if(other is Vector4d)
			return Equals((Vector4d)other);
		return base.Equals(other);
	}

	/// <summary>Compute a hash code from combining the axes.</summary>
	public override int GetHashCode()
	{
		return  X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode() ^ W.GetHashCode() ;
	}

	/// <summary>Convert this <see cref="Vector4d"/> to a string of the form "X, Y, Z, W".</summary>
	public string ToCommaSeparatedString(string format = null, IFormatProvider provider = null) {
		return X.ToString(format, provider) + ", " + Y.ToString(format, provider) + ", " + Z.ToString(format, provider) + ", " + W.ToString(format, provider);
	}

	/// <summary>Convert this <see cref="Vector4d"/> to a string of the form "Vector4d(X, Y, Z, W)".</summary>
	public override string ToString()
	{
		return ToString(null, null);
	}

	/// <summary>Convert this <see cref="Vector4d"/> to a string of the form "Vector4d(X, Y, Z, W)".</summary>
	public string ToString(string format, IFormatProvider provider)
	{
		return "Vector4d(" + ToCommaSeparatedString(format, provider) + ")";
	}

	/// <summary>Convert this <see cref="Vector4d"/> to a string of the form "{X, Y, Z, W".</summary>
	public string ToShortString(string format = null, IFormatProvider provider = null) { return "{" + ToCommaSeparatedString(format, provider) + "}"; }	

	#endregion Methods
	
	public static bool operator ==(Vector4d a, Vector4d b) { return a.X == b.X&&a.Y == b.Y&&a.Z == b.Z&&a.W == b.W; }

	public static bool operator !=(Vector4d a, Vector4d b) { return a.X != b.X||a.Y != b.Y||a.Z != b.Z||a.W != b.W; }

		#region Operators
		

			// Casting
							public static explicit operator Vector4f(Vector4d a) { return new Vector4f((float)a.X, (float)a.Y, (float)a.Z, (float)a.W); }
					public static explicit operator Vector4i(Vector4d a) { return new Vector4i((int)a.X, (int)a.Y, (int)a.Z, (int)a.W); }
					public static explicit operator Vector4ui(Vector4d a) { return new Vector4ui((uint)a.X, (uint)a.Y, (uint)a.Z, (uint)a.W); }
					public static explicit operator Vector4b(Vector4d a) { return new Vector4b((byte)a.X, (byte)a.Y, (byte)a.Z, (byte)a.W); }
					public static explicit operator Vector4nb(Vector4d a) { return new Vector4nb((NormalizedByte)a.X, (NormalizedByte)a.Y, (NormalizedByte)a.Z, (NormalizedByte)a.W); }
					public static explicit operator Vector4nsb(Vector4d a) { return new Vector4nsb((NormalizedSByte)a.X, (NormalizedSByte)a.Y, (NormalizedSByte)a.Z, (NormalizedSByte)a.W); }
					public static explicit operator Vector4rgba(Vector4d a) { return new Vector4rgba((NormalizedByte)a.X, (NormalizedByte)a.Y, (NormalizedByte)a.Z, (NormalizedByte)a.W); }
					public static explicit operator Vector4h(Vector4d a) { return new Vector4h((Float16)a.X, (Float16)a.Y, (Float16)a.Z, (Float16)a.W); }
					
				// Explicit casting to vectors with a lower order, trimming off axes.
									public static explicit operator Vector3d(Vector4d a)
					{
						return new Vector3d(a.X, a.Y, a.Z);
					}
									public static explicit operator Vector2d(Vector4d a)
					{
						return new Vector2d(a.X, a.Y);
					}
							
			public static Vector4d operator +(Vector4d a) { return new Vector4d((double)(+a.X), (double)(+a.Y), (double)(+a.Z), (double)(+a.W)); }
			public static Vector4d operator -(Vector4d a) { return new Vector4d((double)(-a.X), (double)(-a.Y), (double)(-a.Z), (double)(-a.W)); }
								public static Vector4d operator +(Vector4d a, Vector4d b) { return new Vector4d((double)(a.X + b.X), (double)(a.Y + b.Y), (double)(a.Z + b.Z), (double)(a.W + b.W)); }
					public static Vector4d operator +(Vector4d a, double b) { return new Vector4d((double)(a.X + b), (double)(a.Y + b), (double)(a.Z + b), (double)(a.W + b)); }
					public static Vector4d operator +(double a, Vector4d b) { return new Vector4d((double)(a + b.X), (double)(a + b.Y), (double)(a + b.Z), (double)(a + b.W)); }
									public static Vector4d operator -(Vector4d a, Vector4d b) { return new Vector4d((double)(a.X - b.X), (double)(a.Y - b.Y), (double)(a.Z - b.Z), (double)(a.W - b.W)); }
					public static Vector4d operator -(Vector4d a, double b) { return new Vector4d((double)(a.X - b), (double)(a.Y - b), (double)(a.Z - b), (double)(a.W - b)); }
					public static Vector4d operator -(double a, Vector4d b) { return new Vector4d((double)(a - b.X), (double)(a - b.Y), (double)(a - b.Z), (double)(a - b.W)); }
									public static Vector4d operator *(Vector4d a, Vector4d b) { return new Vector4d((double)(a.X * b.X), (double)(a.Y * b.Y), (double)(a.Z * b.Z), (double)(a.W * b.W)); }
					public static Vector4d operator *(Vector4d a, double b) { return new Vector4d((double)(a.X * b), (double)(a.Y * b), (double)(a.Z * b), (double)(a.W * b)); }
					public static Vector4d operator *(double a, Vector4d b) { return new Vector4d((double)(a * b.X), (double)(a * b.Y), (double)(a * b.Z), (double)(a * b.W)); }
									public static Vector4d operator /(Vector4d a, Vector4d b) { return new Vector4d((double)(a.X / b.X), (double)(a.Y / b.Y), (double)(a.Z / b.Z), (double)(a.W / b.W)); }
					public static Vector4d operator /(Vector4d a, double b) { return new Vector4d((double)(a.X / b), (double)(a.Y / b), (double)(a.Z / b), (double)(a.W / b)); }
					public static Vector4d operator /(double a, Vector4d b) { return new Vector4d((double)(a / b.X), (double)(a / b.Y), (double)(a / b.Z), (double)(a / b.W)); }
									public static Vector4d operator %(Vector4d a, Vector4d b) { return new Vector4d((double)(a.X % b.X), (double)(a.Y % b.Y), (double)(a.Z % b.Z), (double)(a.W % b.W)); }
					public static Vector4d operator %(Vector4d a, double b) { return new Vector4d((double)(a.X % b), (double)(a.Y % b), (double)(a.Z % b), (double)(a.W % b)); }
					public static Vector4d operator %(double a, Vector4d b) { return new Vector4d((double)(a % b.X), (double)(a % b.Y), (double)(a % b.Z), (double)(a % b.W)); }
				
		#endregion Operators
	}
	
	
	/// <summary>A four-dimensional vector type using <see cref="int"/> elements.</summary>
	[VectorTypeAttribute(typeof(int), 4, false)]
	public partial struct Vector4i : IEquatable<Vector4i>, IFormattable
	{
		#region Properties
		
							/// <summary>Compute the squared magnitude of the vector, which is the distance from the origin squared. Use <see cref="Magnitude"/> for the unsquared version, which is slower to compute.</summary>
				public int MagnitudeSquared { get { return  X * X + Y * Y + Z * Z + W * W ; } }
			
				/// <summary>Compute the magnitude of the vector, which is the distance from the origin.</summary>
				public int Magnitude
				{
					get
					{
						return (int)(							Math.Sqrt(
																	 X.Squared() 
								+									 Y.Squared() 
								+									 Z.Squared() 
								+									 W.Squared() 
															));
					}
				}

									/// <summary>Get the normalized form of this vector, with a magnitude of one.</summary>
					public Vector4i Normalized { get { Vector4i result; Normalize(out result); return result; } }
							
							/// <summary>Get the product of multiplying all of the axes together.</summary>
				public int Product { get { return (int)( X * Y * Z * W ); } }
			
			/// <summary>Get the sum of all of the axes.</summary>
			public int Sum { get { return (int)( X + Y + Z + W ); } }

		#endregion Properties

		#region Constructors
		
			
			
							public static readonly Vector4i One = new Vector4i((int)1, (int)1, (int)1, (int)1);

									/// <summary>Get a normalized vector where X is 1 and all other axes are 0.</summary>
					public static readonly Vector4i UnitX = new Vector4i((int)1, (int)0, (int)0, (int)0);
									/// <summary>Get a normalized vector where Y is 1 and all other axes are 0.</summary>
					public static readonly Vector4i UnitY = new Vector4i((int)0, (int)1, (int)0, (int)0);
									/// <summary>Get a normalized vector where Z is 1 and all other axes are 0.</summary>
					public static readonly Vector4i UnitZ = new Vector4i((int)0, (int)0, (int)1, (int)0);
									/// <summary>Get a normalized vector where W is 1 and all other axes are 0.</summary>
					public static readonly Vector4i UnitW = new Vector4i((int)0, (int)0, (int)0, (int)1);
							

			// Constructors built up of smaller vectors.
												public Vector4i( Vector3i xyz, int w)
					{
													X = xyz.X;
													Y = xyz.Y;
													Z = xyz.Z;
										
						W = w;
					}
				
					public Vector4i(int x,  Vector3i yz)
					{
						X = x;

													Y = yz.X;
													Z = yz.Y;
													W = yz.Z;
											}
									public Vector4i( ref  Vector3i xyz, int w)
					{
													X = xyz.X;
													Y = xyz.Y;
													Z = xyz.Z;
										
						W = w;
					}
				
					public Vector4i(int x,  ref  Vector3i yz)
					{
						X = x;

													Y = yz.X;
													Z = yz.Y;
													W = yz.Z;
											}
							
			// Constructors from much smaller vectors.
												public Vector4i( Vector2i xy, int z, int w) { X = xy.X; Y = xy.Y; Z = z; W = w; }
					public Vector4i(int x,  Vector2i yz, int w) { X = x; Y = yz.X; Z = yz.Y; W = w; }
					public Vector4i(int x, int y,  Vector2i zw) { X = x; Y = y; Z = zw.X; W = zw.Y; }
					public Vector4i( Vector2i xy,  Vector2i zw) { X = xy.X; Y = xy.Y; Z = zw.X; W = zw.Y; }
									public Vector4i( ref  Vector2i xy, int z, int w) { X = xy.X; Y = xy.Y; Z = z; W = w; }
					public Vector4i(int x,  ref  Vector2i yz, int w) { X = x; Y = yz.X; Z = yz.Y; W = w; }
					public Vector4i(int x, int y,  ref  Vector2i zw) { X = x; Y = y; Z = zw.X; W = zw.Y; }
					public Vector4i( ref  Vector2i xy,  ref  Vector2i zw) { X = xy.X; Y = xy.Y; Z = zw.X; W = zw.Y; }
							
		#endregion Constructors

		#region Methods

			// Methods that have joint ref forms.
							/// <summary>Compute the dot product of this and other vector, which is the sum of the product of each axis of each vector.</summary>
				/// <param name="other">The other vector to calculate a dot product with. </param>
				public int Dot( Vector4i other) { return  X * other.X + Y * other.Y + Z * other.Z + W * other.W ; }
							/// <summary>Compute the dot product of this and other vector, which is the sum of the product of each axis of each vector.</summary>
				/// <param name="other">The other vector to calculate a dot product with. The value of the parameter will not be changed; <c>ref</c> is used for optimisation only.</param>
				public int Dot( ref  Vector4i other) { return  X * other.X + Y * other.Y + Z * other.Z + W * other.W ; }
			
							public Vector4i Clamp( Vector4i min,  Vector4i max )
				{
					Vector4i result;
											result.X = X < min.X ? min.X : X > max.X ? max.X : X;
											result.Y = Y < min.Y ? min.Y : Y > max.Y ? max.Y : Y;
											result.Z = Z < min.Z ? min.Z : Z > max.Z ? max.Z : Z;
											result.W = W < min.W ? min.W : W > max.W ? max.W : W;
										return result;
				}
							public void Clamp( ref  Vector4i min,  ref  Vector4i max , out Vector4i result)
				{
					
											result.X = X < min.X ? min.X : X > max.X ? max.X : X;
											result.Y = Y < min.Y ? min.Y : Y > max.Y ? max.Y : Y;
											result.Z = Z < min.Z ? min.Z : Z > max.Z ? max.Z : Z;
											result.W = W < min.W ? min.W : W > max.W ? max.W : W;
										return;
				}
			
			// Floating-point methods
												/// <summary>Get the distance between the vectors.</summary>
					public int Distance( Vector4i other)
					{
						return (int)(							Math.Sqrt(
																	 X - other.X .Squared() 
								+									 Y - other.Y .Squared() 
								+									 Z - other.Z .Squared() 
								+									 W - other.W .Squared() 
															));
					}

					/// <summary>Get the squared distance between the vectors.</summary>
					public int DistanceSquared( Vector4i other)
					{
						return 
															(X - other.X).Squared()
							+								(Y - other.Y).Squared()
							+								(Z - other.Z).Squared()
							+								(W - other.W).Squared()
							;
					}
									/// <summary>Get the distance between the vectors.</summary>
					public int Distance( ref  Vector4i other)
					{
						return (int)(							Math.Sqrt(
																	 X - other.X .Squared() 
								+									 Y - other.Y .Squared() 
								+									 Z - other.Z .Squared() 
								+									 W - other.W .Squared() 
															));
					}

					/// <summary>Get the squared distance between the vectors.</summary>
					public int DistanceSquared( ref  Vector4i other)
					{
						return 
															(X - other.X).Squared()
							+								(Y - other.Y).Squared()
							+								(Z - other.Z).Squared()
							+								(W - other.W).Squared()
							;
					}
				
									/// <summary>Get the normalized form of this vector, which has a magnitude of one.</summary>
					public void Normalize(out Vector4i result)
					{
						double m = 1.0 / Math.Sqrt( X * X + Y * Y + Z * Z + W * W );
						 result.X = (int)(X * m);  result.Y = (int)(Y * m);  result.Z = (int)(Z * m);  result.W = (int)(W * m); 					}

					/// <summary>Normalize this vector in place, giving it a magnitude of one. An identity vector will become NaN.</summary>
					public void NormalizeInPlace()
					{
						int m = (int)(1.0 / Math.Sqrt( X * X + Y * Y + Z * Z + W * W ));
						 X *= m;  Y *= m;  Z *= m;  W *= m; 					}
				
												
		#endregion Methods

			#region Fields

			/// <summary>The first axis of the <see cref="Vector4i"/>.</summary>
		public int X;
			/// <summary>The second axis of the <see cref="Vector4i"/>.</summary>
		public int Y;
			/// <summary>The third axis of the <see cref="Vector4i"/>.</summary>
		public int Z;
			/// <summary>The fourth axis of the <see cref="Vector4i"/>.</summary>
		public int W;
	
	#endregion Fields

	#region Properties

	public static readonly Vector4i Zero = new Vector4i((int)0, (int)0, (int)0, (int)0);

	#endregion Properties

	#region Constructors

	/// <summary>Initialise a <see cref="Vector4i"/> from the provided values for each factor.</summary>
	public Vector4i(int x, int y, int z, int w)
	{
					X = x;
					Y = y;
					Z = z;
					W = w;
			}

	/// <summary>Initialise a <see cref="Vector4i"/> from a list.</summary>
	public Vector4i(IList<int> list, int index = 0) : this(list[index + 0], list[index + 1], list[index + 2], list[index + 3]) { }

	/// <summary>Initialise a <see cref="Vector4i"/> from a single scalar that is applied to all factors.</summary>
	public Vector4i(int value) : this(value, value, value, value) { }

	#endregion Constructors

	#region Methods

	/// <summary>Get whether this <see cref="Vector4i"/> has equal factors as the other <see cref="Vector4i"/>.</summary>
	public bool Equals(Vector4i other) {
		return  X == other.X && Y == other.Y && Z == other.Z && W == other.W ;
	}

	/// <summary>If the other object is a <see cref="Vector4i"/> of the same type, get whether this <see cref="Vector4i"/> has equal factors as it; otherwise return false./summary>
	public override bool Equals(object other)
	{
		if(other is Vector4i)
			return Equals((Vector4i)other);
		return base.Equals(other);
	}

	/// <summary>Compute a hash code from combining the axes.</summary>
	public override int GetHashCode()
	{
		return  X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode() ^ W.GetHashCode() ;
	}

	/// <summary>Convert this <see cref="Vector4i"/> to a string of the form "X, Y, Z, W".</summary>
	public string ToCommaSeparatedString(string format = null, IFormatProvider provider = null) {
		return X.ToString(format, provider) + ", " + Y.ToString(format, provider) + ", " + Z.ToString(format, provider) + ", " + W.ToString(format, provider);
	}

	/// <summary>Convert this <see cref="Vector4i"/> to a string of the form "Vector4i(X, Y, Z, W)".</summary>
	public override string ToString()
	{
		return ToString(null, null);
	}

	/// <summary>Convert this <see cref="Vector4i"/> to a string of the form "Vector4i(X, Y, Z, W)".</summary>
	public string ToString(string format, IFormatProvider provider)
	{
		return "Vector4i(" + ToCommaSeparatedString(format, provider) + ")";
	}

	/// <summary>Convert this <see cref="Vector4i"/> to a string of the form "{X, Y, Z, W".</summary>
	public string ToShortString(string format = null, IFormatProvider provider = null) { return "{" + ToCommaSeparatedString(format, provider) + "}"; }	

	#endregion Methods
	
	public static bool operator ==(Vector4i a, Vector4i b) { return a.X == b.X&&a.Y == b.Y&&a.Z == b.Z&&a.W == b.W; }

	public static bool operator !=(Vector4i a, Vector4i b) { return a.X != b.X||a.Y != b.Y||a.Z != b.Z||a.W != b.W; }

		#region Operators
		

			// Casting
							public static implicit operator Vector4f(Vector4i a) { return new Vector4f((float)a.X, (float)a.Y, (float)a.Z, (float)a.W); }
					public static implicit operator Vector4d(Vector4i a) { return new Vector4d((double)a.X, (double)a.Y, (double)a.Z, (double)a.W); }
					public static explicit operator Vector4ui(Vector4i a) { return new Vector4ui((uint)a.X, (uint)a.Y, (uint)a.Z, (uint)a.W); }
					public static explicit operator Vector4b(Vector4i a) { return new Vector4b((byte)a.X, (byte)a.Y, (byte)a.Z, (byte)a.W); }
					public static explicit operator Vector4nb(Vector4i a) { return new Vector4nb((NormalizedByte)a.X, (NormalizedByte)a.Y, (NormalizedByte)a.Z, (NormalizedByte)a.W); }
					public static explicit operator Vector4nsb(Vector4i a) { return new Vector4nsb((NormalizedSByte)a.X, (NormalizedSByte)a.Y, (NormalizedSByte)a.Z, (NormalizedSByte)a.W); }
					public static explicit operator Vector4rgba(Vector4i a) { return new Vector4rgba((NormalizedByte)a.X, (NormalizedByte)a.Y, (NormalizedByte)a.Z, (NormalizedByte)a.W); }
					public static explicit operator Vector4h(Vector4i a) { return new Vector4h((Float16)a.X, (Float16)a.Y, (Float16)a.Z, (Float16)a.W); }
					
				// Explicit casting to vectors with a lower order, trimming off axes.
									public static explicit operator Vector3i(Vector4i a)
					{
						return new Vector3i(a.X, a.Y, a.Z);
					}
									public static explicit operator Vector2i(Vector4i a)
					{
						return new Vector2i(a.X, a.Y);
					}
							
			public static Vector4i operator +(Vector4i a) { return new Vector4i((int)(+a.X), (int)(+a.Y), (int)(+a.Z), (int)(+a.W)); }
			public static Vector4i operator -(Vector4i a) { return new Vector4i((int)(-a.X), (int)(-a.Y), (int)(-a.Z), (int)(-a.W)); }
								public static Vector4i operator +(Vector4i a, Vector4i b) { return new Vector4i((int)(a.X + b.X), (int)(a.Y + b.Y), (int)(a.Z + b.Z), (int)(a.W + b.W)); }
					public static Vector4i operator +(Vector4i a, int b) { return new Vector4i((int)(a.X + b), (int)(a.Y + b), (int)(a.Z + b), (int)(a.W + b)); }
					public static Vector4i operator +(int a, Vector4i b) { return new Vector4i((int)(a + b.X), (int)(a + b.Y), (int)(a + b.Z), (int)(a + b.W)); }
									public static Vector4i operator -(Vector4i a, Vector4i b) { return new Vector4i((int)(a.X - b.X), (int)(a.Y - b.Y), (int)(a.Z - b.Z), (int)(a.W - b.W)); }
					public static Vector4i operator -(Vector4i a, int b) { return new Vector4i((int)(a.X - b), (int)(a.Y - b), (int)(a.Z - b), (int)(a.W - b)); }
					public static Vector4i operator -(int a, Vector4i b) { return new Vector4i((int)(a - b.X), (int)(a - b.Y), (int)(a - b.Z), (int)(a - b.W)); }
									public static Vector4i operator *(Vector4i a, Vector4i b) { return new Vector4i((int)(a.X * b.X), (int)(a.Y * b.Y), (int)(a.Z * b.Z), (int)(a.W * b.W)); }
					public static Vector4i operator *(Vector4i a, int b) { return new Vector4i((int)(a.X * b), (int)(a.Y * b), (int)(a.Z * b), (int)(a.W * b)); }
					public static Vector4i operator *(int a, Vector4i b) { return new Vector4i((int)(a * b.X), (int)(a * b.Y), (int)(a * b.Z), (int)(a * b.W)); }
									public static Vector4i operator /(Vector4i a, Vector4i b) { return new Vector4i((int)(a.X / b.X), (int)(a.Y / b.Y), (int)(a.Z / b.Z), (int)(a.W / b.W)); }
					public static Vector4i operator /(Vector4i a, int b) { return new Vector4i((int)(a.X / b), (int)(a.Y / b), (int)(a.Z / b), (int)(a.W / b)); }
					public static Vector4i operator /(int a, Vector4i b) { return new Vector4i((int)(a / b.X), (int)(a / b.Y), (int)(a / b.Z), (int)(a / b.W)); }
									public static Vector4i operator %(Vector4i a, Vector4i b) { return new Vector4i((int)(a.X % b.X), (int)(a.Y % b.Y), (int)(a.Z % b.Z), (int)(a.W % b.W)); }
					public static Vector4i operator %(Vector4i a, int b) { return new Vector4i((int)(a.X % b), (int)(a.Y % b), (int)(a.Z % b), (int)(a.W % b)); }
					public static Vector4i operator %(int a, Vector4i b) { return new Vector4i((int)(a % b.X), (int)(a % b.Y), (int)(a % b.Z), (int)(a % b.W)); }
				
		#endregion Operators
	}
	
	
	/// <summary>A four-dimensional vector type using <see cref="uint"/> elements.</summary>
	[VectorTypeAttribute(typeof(uint), 4, false)]
	public partial struct Vector4ui : IEquatable<Vector4ui>, IFormattable
	{
		#region Properties
		
			
							/// <summary>Get the product of multiplying all of the axes together.</summary>
				public uint Product { get { return (uint)( X * Y * Z * W ); } }
			
			/// <summary>Get the sum of all of the axes.</summary>
			public uint Sum { get { return (uint)( X + Y + Z + W ); } }

		#endregion Properties

		#region Constructors
		
			
			
							public static readonly Vector4ui One = new Vector4ui((uint)1, (uint)1, (uint)1, (uint)1);

									/// <summary>Get a normalized vector where X is 1 and all other axes are 0.</summary>
					public static readonly Vector4ui UnitX = new Vector4ui((uint)1, (uint)0, (uint)0, (uint)0);
									/// <summary>Get a normalized vector where Y is 1 and all other axes are 0.</summary>
					public static readonly Vector4ui UnitY = new Vector4ui((uint)0, (uint)1, (uint)0, (uint)0);
									/// <summary>Get a normalized vector where Z is 1 and all other axes are 0.</summary>
					public static readonly Vector4ui UnitZ = new Vector4ui((uint)0, (uint)0, (uint)1, (uint)0);
									/// <summary>Get a normalized vector where W is 1 and all other axes are 0.</summary>
					public static readonly Vector4ui UnitW = new Vector4ui((uint)0, (uint)0, (uint)0, (uint)1);
							

			// Constructors built up of smaller vectors.
												public Vector4ui( Vector3ui xyz, uint w)
					{
													X = xyz.X;
													Y = xyz.Y;
													Z = xyz.Z;
										
						W = w;
					}
				
					public Vector4ui(uint x,  Vector3ui yz)
					{
						X = x;

													Y = yz.X;
													Z = yz.Y;
													W = yz.Z;
											}
									public Vector4ui( ref  Vector3ui xyz, uint w)
					{
													X = xyz.X;
													Y = xyz.Y;
													Z = xyz.Z;
										
						W = w;
					}
				
					public Vector4ui(uint x,  ref  Vector3ui yz)
					{
						X = x;

													Y = yz.X;
													Z = yz.Y;
													W = yz.Z;
											}
							
			// Constructors from much smaller vectors.
												public Vector4ui( Vector2ui xy, uint z, uint w) { X = xy.X; Y = xy.Y; Z = z; W = w; }
					public Vector4ui(uint x,  Vector2ui yz, uint w) { X = x; Y = yz.X; Z = yz.Y; W = w; }
					public Vector4ui(uint x, uint y,  Vector2ui zw) { X = x; Y = y; Z = zw.X; W = zw.Y; }
					public Vector4ui( Vector2ui xy,  Vector2ui zw) { X = xy.X; Y = xy.Y; Z = zw.X; W = zw.Y; }
									public Vector4ui( ref  Vector2ui xy, uint z, uint w) { X = xy.X; Y = xy.Y; Z = z; W = w; }
					public Vector4ui(uint x,  ref  Vector2ui yz, uint w) { X = x; Y = yz.X; Z = yz.Y; W = w; }
					public Vector4ui(uint x, uint y,  ref  Vector2ui zw) { X = x; Y = y; Z = zw.X; W = zw.Y; }
					public Vector4ui( ref  Vector2ui xy,  ref  Vector2ui zw) { X = xy.X; Y = xy.Y; Z = zw.X; W = zw.Y; }
							
		#endregion Constructors

		#region Methods

			// Methods that have joint ref forms.
							/// <summary>Compute the dot product of this and other vector, which is the sum of the product of each axis of each vector.</summary>
				/// <param name="other">The other vector to calculate a dot product with. </param>
				public uint Dot( Vector4ui other) { return  X * other.X + Y * other.Y + Z * other.Z + W * other.W ; }
							/// <summary>Compute the dot product of this and other vector, which is the sum of the product of each axis of each vector.</summary>
				/// <param name="other">The other vector to calculate a dot product with. The value of the parameter will not be changed; <c>ref</c> is used for optimisation only.</param>
				public uint Dot( ref  Vector4ui other) { return  X * other.X + Y * other.Y + Z * other.Z + W * other.W ; }
			
							public Vector4ui Clamp( Vector4ui min,  Vector4ui max )
				{
					Vector4ui result;
											result.X = X < min.X ? min.X : X > max.X ? max.X : X;
											result.Y = Y < min.Y ? min.Y : Y > max.Y ? max.Y : Y;
											result.Z = Z < min.Z ? min.Z : Z > max.Z ? max.Z : Z;
											result.W = W < min.W ? min.W : W > max.W ? max.W : W;
										return result;
				}
							public void Clamp( ref  Vector4ui min,  ref  Vector4ui max , out Vector4ui result)
				{
					
											result.X = X < min.X ? min.X : X > max.X ? max.X : X;
											result.Y = Y < min.Y ? min.Y : Y > max.Y ? max.Y : Y;
											result.Z = Z < min.Z ? min.Z : Z > max.Z ? max.Z : Z;
											result.W = W < min.W ? min.W : W > max.W ? max.W : W;
										return;
				}
			
			// Floating-point methods
			
		#endregion Methods

			#region Fields

			/// <summary>The first axis of the <see cref="Vector4ui"/>.</summary>
		public uint X;
			/// <summary>The second axis of the <see cref="Vector4ui"/>.</summary>
		public uint Y;
			/// <summary>The third axis of the <see cref="Vector4ui"/>.</summary>
		public uint Z;
			/// <summary>The fourth axis of the <see cref="Vector4ui"/>.</summary>
		public uint W;
	
	#endregion Fields

	#region Properties

	public static readonly Vector4ui Zero = new Vector4ui((uint)0, (uint)0, (uint)0, (uint)0);

	#endregion Properties

	#region Constructors

	/// <summary>Initialise a <see cref="Vector4ui"/> from the provided values for each factor.</summary>
	public Vector4ui(uint x, uint y, uint z, uint w)
	{
					X = x;
					Y = y;
					Z = z;
					W = w;
			}

	/// <summary>Initialise a <see cref="Vector4ui"/> from a list.</summary>
	public Vector4ui(IList<uint> list, int index = 0) : this(list[index + 0], list[index + 1], list[index + 2], list[index + 3]) { }

	/// <summary>Initialise a <see cref="Vector4ui"/> from a single scalar that is applied to all factors.</summary>
	public Vector4ui(uint value) : this(value, value, value, value) { }

	#endregion Constructors

	#region Methods

	/// <summary>Get whether this <see cref="Vector4ui"/> has equal factors as the other <see cref="Vector4ui"/>.</summary>
	public bool Equals(Vector4ui other) {
		return  X == other.X && Y == other.Y && Z == other.Z && W == other.W ;
	}

	/// <summary>If the other object is a <see cref="Vector4ui"/> of the same type, get whether this <see cref="Vector4ui"/> has equal factors as it; otherwise return false./summary>
	public override bool Equals(object other)
	{
		if(other is Vector4ui)
			return Equals((Vector4ui)other);
		return base.Equals(other);
	}

	/// <summary>Compute a hash code from combining the axes.</summary>
	public override int GetHashCode()
	{
		return  X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode() ^ W.GetHashCode() ;
	}

	/// <summary>Convert this <see cref="Vector4ui"/> to a string of the form "X, Y, Z, W".</summary>
	public string ToCommaSeparatedString(string format = null, IFormatProvider provider = null) {
		return X.ToString(format, provider) + ", " + Y.ToString(format, provider) + ", " + Z.ToString(format, provider) + ", " + W.ToString(format, provider);
	}

	/// <summary>Convert this <see cref="Vector4ui"/> to a string of the form "Vector4ui(X, Y, Z, W)".</summary>
	public override string ToString()
	{
		return ToString(null, null);
	}

	/// <summary>Convert this <see cref="Vector4ui"/> to a string of the form "Vector4ui(X, Y, Z, W)".</summary>
	public string ToString(string format, IFormatProvider provider)
	{
		return "Vector4ui(" + ToCommaSeparatedString(format, provider) + ")";
	}

	/// <summary>Convert this <see cref="Vector4ui"/> to a string of the form "{X, Y, Z, W".</summary>
	public string ToShortString(string format = null, IFormatProvider provider = null) { return "{" + ToCommaSeparatedString(format, provider) + "}"; }	

	#endregion Methods
	
	public static bool operator ==(Vector4ui a, Vector4ui b) { return a.X == b.X&&a.Y == b.Y&&a.Z == b.Z&&a.W == b.W; }

	public static bool operator !=(Vector4ui a, Vector4ui b) { return a.X != b.X||a.Y != b.Y||a.Z != b.Z||a.W != b.W; }

		#region Operators
		

			// Casting
							public static implicit operator Vector4f(Vector4ui a) { return new Vector4f((float)a.X, (float)a.Y, (float)a.Z, (float)a.W); }
					public static implicit operator Vector4d(Vector4ui a) { return new Vector4d((double)a.X, (double)a.Y, (double)a.Z, (double)a.W); }
					public static explicit operator Vector4i(Vector4ui a) { return new Vector4i((int)a.X, (int)a.Y, (int)a.Z, (int)a.W); }
					public static explicit operator Vector4b(Vector4ui a) { return new Vector4b((byte)a.X, (byte)a.Y, (byte)a.Z, (byte)a.W); }
					public static explicit operator Vector4nb(Vector4ui a) { return new Vector4nb((NormalizedByte)a.X, (NormalizedByte)a.Y, (NormalizedByte)a.Z, (NormalizedByte)a.W); }
					public static explicit operator Vector4nsb(Vector4ui a) { return new Vector4nsb((NormalizedSByte)a.X, (NormalizedSByte)a.Y, (NormalizedSByte)a.Z, (NormalizedSByte)a.W); }
					public static explicit operator Vector4rgba(Vector4ui a) { return new Vector4rgba((NormalizedByte)a.X, (NormalizedByte)a.Y, (NormalizedByte)a.Z, (NormalizedByte)a.W); }
					public static explicit operator Vector4h(Vector4ui a) { return new Vector4h((Float16)a.X, (Float16)a.Y, (Float16)a.Z, (Float16)a.W); }
					
				// Explicit casting to vectors with a lower order, trimming off axes.
									public static explicit operator Vector3ui(Vector4ui a)
					{
						return new Vector3ui(a.X, a.Y, a.Z);
					}
									public static explicit operator Vector2ui(Vector4ui a)
					{
						return new Vector2ui(a.X, a.Y);
					}
							
			public static Vector4ui operator +(Vector4ui a) { return new Vector4ui((uint)(+a.X), (uint)(+a.Y), (uint)(+a.Z), (uint)(+a.W)); }
			public static Vector4ui operator -(Vector4ui a) { return new Vector4ui((uint)(-a.X), (uint)(-a.Y), (uint)(-a.Z), (uint)(-a.W)); }
								public static Vector4ui operator +(Vector4ui a, Vector4ui b) { return new Vector4ui((uint)(a.X + b.X), (uint)(a.Y + b.Y), (uint)(a.Z + b.Z), (uint)(a.W + b.W)); }
					public static Vector4ui operator +(Vector4ui a, uint b) { return new Vector4ui((uint)(a.X + b), (uint)(a.Y + b), (uint)(a.Z + b), (uint)(a.W + b)); }
					public static Vector4ui operator +(uint a, Vector4ui b) { return new Vector4ui((uint)(a + b.X), (uint)(a + b.Y), (uint)(a + b.Z), (uint)(a + b.W)); }
									public static Vector4ui operator -(Vector4ui a, Vector4ui b) { return new Vector4ui((uint)(a.X - b.X), (uint)(a.Y - b.Y), (uint)(a.Z - b.Z), (uint)(a.W - b.W)); }
					public static Vector4ui operator -(Vector4ui a, uint b) { return new Vector4ui((uint)(a.X - b), (uint)(a.Y - b), (uint)(a.Z - b), (uint)(a.W - b)); }
					public static Vector4ui operator -(uint a, Vector4ui b) { return new Vector4ui((uint)(a - b.X), (uint)(a - b.Y), (uint)(a - b.Z), (uint)(a - b.W)); }
									public static Vector4ui operator *(Vector4ui a, Vector4ui b) { return new Vector4ui((uint)(a.X * b.X), (uint)(a.Y * b.Y), (uint)(a.Z * b.Z), (uint)(a.W * b.W)); }
					public static Vector4ui operator *(Vector4ui a, uint b) { return new Vector4ui((uint)(a.X * b), (uint)(a.Y * b), (uint)(a.Z * b), (uint)(a.W * b)); }
					public static Vector4ui operator *(uint a, Vector4ui b) { return new Vector4ui((uint)(a * b.X), (uint)(a * b.Y), (uint)(a * b.Z), (uint)(a * b.W)); }
									public static Vector4ui operator /(Vector4ui a, Vector4ui b) { return new Vector4ui((uint)(a.X / b.X), (uint)(a.Y / b.Y), (uint)(a.Z / b.Z), (uint)(a.W / b.W)); }
					public static Vector4ui operator /(Vector4ui a, uint b) { return new Vector4ui((uint)(a.X / b), (uint)(a.Y / b), (uint)(a.Z / b), (uint)(a.W / b)); }
					public static Vector4ui operator /(uint a, Vector4ui b) { return new Vector4ui((uint)(a / b.X), (uint)(a / b.Y), (uint)(a / b.Z), (uint)(a / b.W)); }
									public static Vector4ui operator %(Vector4ui a, Vector4ui b) { return new Vector4ui((uint)(a.X % b.X), (uint)(a.Y % b.Y), (uint)(a.Z % b.Z), (uint)(a.W % b.W)); }
					public static Vector4ui operator %(Vector4ui a, uint b) { return new Vector4ui((uint)(a.X % b), (uint)(a.Y % b), (uint)(a.Z % b), (uint)(a.W % b)); }
					public static Vector4ui operator %(uint a, Vector4ui b) { return new Vector4ui((uint)(a % b.X), (uint)(a % b.Y), (uint)(a % b.Z), (uint)(a % b.W)); }
									public static Vector4ui operator &(Vector4ui a, Vector4ui b) { return new Vector4ui((uint)(a.X & b.X), (uint)(a.Y & b.Y), (uint)(a.Z & b.Z), (uint)(a.W & b.W)); }
					public static Vector4ui operator &(Vector4ui a, uint b) { return new Vector4ui((uint)(a.X & b), (uint)(a.Y & b), (uint)(a.Z & b), (uint)(a.W & b)); }
					public static Vector4ui operator &(uint a, Vector4ui b) { return new Vector4ui((uint)(a & b.X), (uint)(a & b.Y), (uint)(a & b.Z), (uint)(a & b.W)); }
									public static Vector4ui operator |(Vector4ui a, Vector4ui b) { return new Vector4ui((uint)(a.X | b.X), (uint)(a.Y | b.Y), (uint)(a.Z | b.Z), (uint)(a.W | b.W)); }
					public static Vector4ui operator |(Vector4ui a, uint b) { return new Vector4ui((uint)(a.X | b), (uint)(a.Y | b), (uint)(a.Z | b), (uint)(a.W | b)); }
					public static Vector4ui operator |(uint a, Vector4ui b) { return new Vector4ui((uint)(a | b.X), (uint)(a | b.Y), (uint)(a | b.Z), (uint)(a | b.W)); }
									public static Vector4ui operator ^(Vector4ui a, Vector4ui b) { return new Vector4ui((uint)(a.X ^ b.X), (uint)(a.Y ^ b.Y), (uint)(a.Z ^ b.Z), (uint)(a.W ^ b.W)); }
					public static Vector4ui operator ^(Vector4ui a, uint b) { return new Vector4ui((uint)(a.X ^ b), (uint)(a.Y ^ b), (uint)(a.Z ^ b), (uint)(a.W ^ b)); }
					public static Vector4ui operator ^(uint a, Vector4ui b) { return new Vector4ui((uint)(a ^ b.X), (uint)(a ^ b.Y), (uint)(a ^ b.Z), (uint)(a ^ b.W)); }
										public static Vector4ui operator <<(Vector4ui a, int b) { return new Vector4ui((uint)(a.X << b), (uint)(a.Y << b), (uint)(a.Z << b), (uint)(a.W << b)); }
											public static Vector4ui operator >>(Vector4ui a, int b) { return new Vector4ui((uint)(a.X >> b), (uint)(a.Y >> b), (uint)(a.Z >> b), (uint)(a.W >> b)); }
					
		#endregion Operators
	}
	
	
	/// <summary>A four-dimensional vector type using <see cref="byte"/> elements.</summary>
	[VectorTypeAttribute(typeof(byte), 4, false)]
	public partial struct Vector4b : IEquatable<Vector4b>, IFormattable
	{
		#region Properties
		
			
							/// <summary>Get the product of multiplying all of the axes together.</summary>
				public byte Product { get { return (byte)( X * Y * Z * W ); } }
			
			/// <summary>Get the sum of all of the axes.</summary>
			public byte Sum { get { return (byte)( X + Y + Z + W ); } }

		#endregion Properties

		#region Constructors
		
			
			
							public static readonly Vector4b One = new Vector4b((byte)1, (byte)1, (byte)1, (byte)1);

									/// <summary>Get a normalized vector where X is 1 and all other axes are 0.</summary>
					public static readonly Vector4b UnitX = new Vector4b((byte)1, (byte)0, (byte)0, (byte)0);
									/// <summary>Get a normalized vector where Y is 1 and all other axes are 0.</summary>
					public static readonly Vector4b UnitY = new Vector4b((byte)0, (byte)1, (byte)0, (byte)0);
									/// <summary>Get a normalized vector where Z is 1 and all other axes are 0.</summary>
					public static readonly Vector4b UnitZ = new Vector4b((byte)0, (byte)0, (byte)1, (byte)0);
									/// <summary>Get a normalized vector where W is 1 and all other axes are 0.</summary>
					public static readonly Vector4b UnitW = new Vector4b((byte)0, (byte)0, (byte)0, (byte)1);
							

			// Constructors built up of smaller vectors.
			
			// Constructors from much smaller vectors.
			
		#endregion Constructors

		#region Methods

			// Methods that have joint ref forms.
							/// <summary>Compute the dot product of this and other vector, which is the sum of the product of each axis of each vector.</summary>
				/// <param name="other">The other vector to calculate a dot product with. </param>
				public int Dot( Vector4b other) { return  X * other.X + Y * other.Y + Z * other.Z + W * other.W ; }
							/// <summary>Compute the dot product of this and other vector, which is the sum of the product of each axis of each vector.</summary>
				/// <param name="other">The other vector to calculate a dot product with. The value of the parameter will not be changed; <c>ref</c> is used for optimisation only.</param>
				public int Dot( ref  Vector4b other) { return  X * other.X + Y * other.Y + Z * other.Z + W * other.W ; }
			
							public Vector4b Clamp( Vector4b min,  Vector4b max )
				{
					Vector4b result;
											result.X = X < min.X ? min.X : X > max.X ? max.X : X;
											result.Y = Y < min.Y ? min.Y : Y > max.Y ? max.Y : Y;
											result.Z = Z < min.Z ? min.Z : Z > max.Z ? max.Z : Z;
											result.W = W < min.W ? min.W : W > max.W ? max.W : W;
										return result;
				}
							public void Clamp( ref  Vector4b min,  ref  Vector4b max , out Vector4b result)
				{
					
											result.X = X < min.X ? min.X : X > max.X ? max.X : X;
											result.Y = Y < min.Y ? min.Y : Y > max.Y ? max.Y : Y;
											result.Z = Z < min.Z ? min.Z : Z > max.Z ? max.Z : Z;
											result.W = W < min.W ? min.W : W > max.W ? max.W : W;
										return;
				}
			
			// Floating-point methods
			
		#endregion Methods

			#region Fields

			/// <summary>The first axis of the <see cref="Vector4b"/>.</summary>
		public byte X;
			/// <summary>The second axis of the <see cref="Vector4b"/>.</summary>
		public byte Y;
			/// <summary>The third axis of the <see cref="Vector4b"/>.</summary>
		public byte Z;
			/// <summary>The fourth axis of the <see cref="Vector4b"/>.</summary>
		public byte W;
	
	#endregion Fields

	#region Properties

	public static readonly Vector4b Zero = new Vector4b((byte)0, (byte)0, (byte)0, (byte)0);

	#endregion Properties

	#region Constructors

	/// <summary>Initialise a <see cref="Vector4b"/> from the provided values for each factor.</summary>
	public Vector4b(byte x, byte y, byte z, byte w)
	{
					X = x;
					Y = y;
					Z = z;
					W = w;
			}

	/// <summary>Initialise a <see cref="Vector4b"/> from a list.</summary>
	public Vector4b(IList<byte> list, int index = 0) : this(list[index + 0], list[index + 1], list[index + 2], list[index + 3]) { }

	/// <summary>Initialise a <see cref="Vector4b"/> from a single scalar that is applied to all factors.</summary>
	public Vector4b(byte value) : this(value, value, value, value) { }

	#endregion Constructors

	#region Methods

	/// <summary>Get whether this <see cref="Vector4b"/> has equal factors as the other <see cref="Vector4b"/>.</summary>
	public bool Equals(Vector4b other) {
		return  X == other.X && Y == other.Y && Z == other.Z && W == other.W ;
	}

	/// <summary>If the other object is a <see cref="Vector4b"/> of the same type, get whether this <see cref="Vector4b"/> has equal factors as it; otherwise return false./summary>
	public override bool Equals(object other)
	{
		if(other is Vector4b)
			return Equals((Vector4b)other);
		return base.Equals(other);
	}

	/// <summary>Compute a hash code from combining the axes.</summary>
	public override int GetHashCode()
	{
		return  X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode() ^ W.GetHashCode() ;
	}

	/// <summary>Convert this <see cref="Vector4b"/> to a string of the form "X, Y, Z, W".</summary>
	public string ToCommaSeparatedString(string format = null, IFormatProvider provider = null) {
		return X.ToString(format, provider) + ", " + Y.ToString(format, provider) + ", " + Z.ToString(format, provider) + ", " + W.ToString(format, provider);
	}

	/// <summary>Convert this <see cref="Vector4b"/> to a string of the form "Vector4b(X, Y, Z, W)".</summary>
	public override string ToString()
	{
		return ToString(null, null);
	}

	/// <summary>Convert this <see cref="Vector4b"/> to a string of the form "Vector4b(X, Y, Z, W)".</summary>
	public string ToString(string format, IFormatProvider provider)
	{
		return "Vector4b(" + ToCommaSeparatedString(format, provider) + ")";
	}

	/// <summary>Convert this <see cref="Vector4b"/> to a string of the form "{X, Y, Z, W".</summary>
	public string ToShortString(string format = null, IFormatProvider provider = null) { return "{" + ToCommaSeparatedString(format, provider) + "}"; }	

	#endregion Methods
	
	public static bool operator ==(Vector4b a, Vector4b b) { return a.X == b.X&&a.Y == b.Y&&a.Z == b.Z&&a.W == b.W; }

	public static bool operator !=(Vector4b a, Vector4b b) { return a.X != b.X||a.Y != b.Y||a.Z != b.Z||a.W != b.W; }

		#region Operators
		

			// Casting
							public static implicit operator Vector4f(Vector4b a) { return new Vector4f((float)a.X, (float)a.Y, (float)a.Z, (float)a.W); }
					public static implicit operator Vector4d(Vector4b a) { return new Vector4d((double)a.X, (double)a.Y, (double)a.Z, (double)a.W); }
					public static explicit operator Vector4i(Vector4b a) { return new Vector4i((int)a.X, (int)a.Y, (int)a.Z, (int)a.W); }
					public static explicit operator Vector4ui(Vector4b a) { return new Vector4ui((uint)a.X, (uint)a.Y, (uint)a.Z, (uint)a.W); }
					public static explicit operator Vector4nb(Vector4b a) { return new Vector4nb((NormalizedByte)a.X, (NormalizedByte)a.Y, (NormalizedByte)a.Z, (NormalizedByte)a.W); }
					public static explicit operator Vector4nsb(Vector4b a) { return new Vector4nsb((NormalizedSByte)a.X, (NormalizedSByte)a.Y, (NormalizedSByte)a.Z, (NormalizedSByte)a.W); }
					public static explicit operator Vector4rgba(Vector4b a) { return new Vector4rgba((NormalizedByte)a.X, (NormalizedByte)a.Y, (NormalizedByte)a.Z, (NormalizedByte)a.W); }
					public static explicit operator Vector4h(Vector4b a) { return new Vector4h((Float16)a.X, (Float16)a.Y, (Float16)a.Z, (Float16)a.W); }
					
				// Explicit casting to vectors with a lower order, trimming off axes.
							
			public static Vector4b operator +(Vector4b a) { return new Vector4b((byte)(+a.X), (byte)(+a.Y), (byte)(+a.Z), (byte)(+a.W)); }
			public static Vector4b operator -(Vector4b a) { return new Vector4b((byte)(-a.X), (byte)(-a.Y), (byte)(-a.Z), (byte)(-a.W)); }
								public static Vector4b operator +(Vector4b a, Vector4b b) { return new Vector4b((byte)(a.X + b.X), (byte)(a.Y + b.Y), (byte)(a.Z + b.Z), (byte)(a.W + b.W)); }
					public static Vector4b operator +(Vector4b a, byte b) { return new Vector4b((byte)(a.X + b), (byte)(a.Y + b), (byte)(a.Z + b), (byte)(a.W + b)); }
					public static Vector4b operator +(byte a, Vector4b b) { return new Vector4b((byte)(a + b.X), (byte)(a + b.Y), (byte)(a + b.Z), (byte)(a + b.W)); }
									public static Vector4b operator -(Vector4b a, Vector4b b) { return new Vector4b((byte)(a.X - b.X), (byte)(a.Y - b.Y), (byte)(a.Z - b.Z), (byte)(a.W - b.W)); }
					public static Vector4b operator -(Vector4b a, byte b) { return new Vector4b((byte)(a.X - b), (byte)(a.Y - b), (byte)(a.Z - b), (byte)(a.W - b)); }
					public static Vector4b operator -(byte a, Vector4b b) { return new Vector4b((byte)(a - b.X), (byte)(a - b.Y), (byte)(a - b.Z), (byte)(a - b.W)); }
									public static Vector4b operator *(Vector4b a, Vector4b b) { return new Vector4b((byte)(a.X * b.X), (byte)(a.Y * b.Y), (byte)(a.Z * b.Z), (byte)(a.W * b.W)); }
					public static Vector4b operator *(Vector4b a, byte b) { return new Vector4b((byte)(a.X * b), (byte)(a.Y * b), (byte)(a.Z * b), (byte)(a.W * b)); }
					public static Vector4b operator *(byte a, Vector4b b) { return new Vector4b((byte)(a * b.X), (byte)(a * b.Y), (byte)(a * b.Z), (byte)(a * b.W)); }
									public static Vector4b operator /(Vector4b a, Vector4b b) { return new Vector4b((byte)(a.X / b.X), (byte)(a.Y / b.Y), (byte)(a.Z / b.Z), (byte)(a.W / b.W)); }
					public static Vector4b operator /(Vector4b a, byte b) { return new Vector4b((byte)(a.X / b), (byte)(a.Y / b), (byte)(a.Z / b), (byte)(a.W / b)); }
					public static Vector4b operator /(byte a, Vector4b b) { return new Vector4b((byte)(a / b.X), (byte)(a / b.Y), (byte)(a / b.Z), (byte)(a / b.W)); }
									public static Vector4b operator %(Vector4b a, Vector4b b) { return new Vector4b((byte)(a.X % b.X), (byte)(a.Y % b.Y), (byte)(a.Z % b.Z), (byte)(a.W % b.W)); }
					public static Vector4b operator %(Vector4b a, byte b) { return new Vector4b((byte)(a.X % b), (byte)(a.Y % b), (byte)(a.Z % b), (byte)(a.W % b)); }
					public static Vector4b operator %(byte a, Vector4b b) { return new Vector4b((byte)(a % b.X), (byte)(a % b.Y), (byte)(a % b.Z), (byte)(a % b.W)); }
									public static Vector4b operator &(Vector4b a, Vector4b b) { return new Vector4b((byte)(a.X & b.X), (byte)(a.Y & b.Y), (byte)(a.Z & b.Z), (byte)(a.W & b.W)); }
					public static Vector4b operator &(Vector4b a, byte b) { return new Vector4b((byte)(a.X & b), (byte)(a.Y & b), (byte)(a.Z & b), (byte)(a.W & b)); }
					public static Vector4b operator &(byte a, Vector4b b) { return new Vector4b((byte)(a & b.X), (byte)(a & b.Y), (byte)(a & b.Z), (byte)(a & b.W)); }
									public static Vector4b operator |(Vector4b a, Vector4b b) { return new Vector4b((byte)(a.X | b.X), (byte)(a.Y | b.Y), (byte)(a.Z | b.Z), (byte)(a.W | b.W)); }
					public static Vector4b operator |(Vector4b a, byte b) { return new Vector4b((byte)(a.X | b), (byte)(a.Y | b), (byte)(a.Z | b), (byte)(a.W | b)); }
					public static Vector4b operator |(byte a, Vector4b b) { return new Vector4b((byte)(a | b.X), (byte)(a | b.Y), (byte)(a | b.Z), (byte)(a | b.W)); }
									public static Vector4b operator ^(Vector4b a, Vector4b b) { return new Vector4b((byte)(a.X ^ b.X), (byte)(a.Y ^ b.Y), (byte)(a.Z ^ b.Z), (byte)(a.W ^ b.W)); }
					public static Vector4b operator ^(Vector4b a, byte b) { return new Vector4b((byte)(a.X ^ b), (byte)(a.Y ^ b), (byte)(a.Z ^ b), (byte)(a.W ^ b)); }
					public static Vector4b operator ^(byte a, Vector4b b) { return new Vector4b((byte)(a ^ b.X), (byte)(a ^ b.Y), (byte)(a ^ b.Z), (byte)(a ^ b.W)); }
										public static Vector4b operator <<(Vector4b a, int b) { return new Vector4b((byte)(a.X << b), (byte)(a.Y << b), (byte)(a.Z << b), (byte)(a.W << b)); }
											public static Vector4b operator >>(Vector4b a, int b) { return new Vector4b((byte)(a.X >> b), (byte)(a.Y >> b), (byte)(a.Z >> b), (byte)(a.W >> b)); }
					
		#endregion Operators
	}
	
	
	/// <summary>A four-dimensional vector type using <see cref="NormalizedByte"/> elements.</summary>
	[VectorTypeAttribute(typeof(NormalizedByte), 4, false)]
	public partial struct Vector4nb : IEquatable<Vector4nb>, IFormattable
	{
		#region Properties
		
							/// <summary>Compute the squared magnitude of the vector, which is the distance from the origin squared. Use <see cref="Magnitude"/> for the unsquared version, which is slower to compute.</summary>
				public double MagnitudeSquared { get { return  X * X + Y * Y + Z * Z + W * W ; } }
			
				/// <summary>Compute the magnitude of the vector, which is the distance from the origin.</summary>
				public double Magnitude
				{
					get
					{
						return (double)(							Math.Sqrt(
																	 X.Squared() 
								+									 Y.Squared() 
								+									 Z.Squared() 
								+									 W.Squared() 
															));
					}
				}

									/// <summary>Get the normalized form of this vector, with a magnitude of one.</summary>
					public Vector4nb Normalized { get { Vector4nb result; Normalize(out result); return result; } }
							
							/// <summary>Get the product of multiplying all of the axes together.</summary>
				public NormalizedByte Product { get { return (NormalizedByte)( X * Y * Z * W ); } }
			
			/// <summary>Get the sum of all of the axes.</summary>
			public NormalizedByte Sum { get { return (NormalizedByte)( X + Y + Z + W ); } }

		#endregion Properties

		#region Constructors
		
							/// <summary>Initialise a vector from double values that are converted into normalised form.</summary>
				public Vector4nb(double x, double y, double z, double w) : this((NormalizedByte)x, (NormalizedByte)y, (NormalizedByte)z, (NormalizedByte)w) { }

									public Vector4nb( Vector4d value) : this((NormalizedByte)value.X, (NormalizedByte)value.Y, (NormalizedByte)value.Z, (NormalizedByte)value.W) { }
									public Vector4nb( ref  Vector4d value) : this((NormalizedByte)value.X, (NormalizedByte)value.Y, (NormalizedByte)value.Z, (NormalizedByte)value.W) { }
									
			
			
							public static readonly Vector4nb One = new Vector4nb((NormalizedByte)1, (NormalizedByte)1, (NormalizedByte)1, (NormalizedByte)1);

									/// <summary>Get a normalized vector where X is 1 and all other axes are 0.</summary>
					public static readonly Vector4nb UnitX = new Vector4nb((NormalizedByte)1, (NormalizedByte)0, (NormalizedByte)0, (NormalizedByte)0);
									/// <summary>Get a normalized vector where Y is 1 and all other axes are 0.</summary>
					public static readonly Vector4nb UnitY = new Vector4nb((NormalizedByte)0, (NormalizedByte)1, (NormalizedByte)0, (NormalizedByte)0);
									/// <summary>Get a normalized vector where Z is 1 and all other axes are 0.</summary>
					public static readonly Vector4nb UnitZ = new Vector4nb((NormalizedByte)0, (NormalizedByte)0, (NormalizedByte)1, (NormalizedByte)0);
									/// <summary>Get a normalized vector where W is 1 and all other axes are 0.</summary>
					public static readonly Vector4nb UnitW = new Vector4nb((NormalizedByte)0, (NormalizedByte)0, (NormalizedByte)0, (NormalizedByte)1);
							

			// Constructors built up of smaller vectors.
			
			// Constructors from much smaller vectors.
			
		#endregion Constructors

		#region Methods

			// Methods that have joint ref forms.
							/// <summary>Compute the dot product of this and other vector, which is the sum of the product of each axis of each vector.</summary>
				/// <param name="other">The other vector to calculate a dot product with. </param>
				public double Dot( Vector4nb other) { return  X * other.X + Y * other.Y + Z * other.Z + W * other.W ; }
							/// <summary>Compute the dot product of this and other vector, which is the sum of the product of each axis of each vector.</summary>
				/// <param name="other">The other vector to calculate a dot product with. The value of the parameter will not be changed; <c>ref</c> is used for optimisation only.</param>
				public double Dot( ref  Vector4nb other) { return  X * other.X + Y * other.Y + Z * other.Z + W * other.W ; }
			
							public Vector4nb Clamp( Vector4nb min,  Vector4nb max )
				{
					Vector4nb result;
											result.X = X < min.X ? min.X : X > max.X ? max.X : X;
											result.Y = Y < min.Y ? min.Y : Y > max.Y ? max.Y : Y;
											result.Z = Z < min.Z ? min.Z : Z > max.Z ? max.Z : Z;
											result.W = W < min.W ? min.W : W > max.W ? max.W : W;
										return result;
				}
							public void Clamp( ref  Vector4nb min,  ref  Vector4nb max , out Vector4nb result)
				{
					
											result.X = X < min.X ? min.X : X > max.X ? max.X : X;
											result.Y = Y < min.Y ? min.Y : Y > max.Y ? max.Y : Y;
											result.Z = Z < min.Z ? min.Z : Z > max.Z ? max.Z : Z;
											result.W = W < min.W ? min.W : W > max.W ? max.W : W;
										return;
				}
			
			// Floating-point methods
												/// <summary>Get the distance between the vectors.</summary>
					public double Distance( Vector4nb other)
					{
						return (double)(							Math.Sqrt(
																	 X - other.X .Squared() 
								+									 Y - other.Y .Squared() 
								+									 Z - other.Z .Squared() 
								+									 W - other.W .Squared() 
															));
					}

					/// <summary>Get the squared distance between the vectors.</summary>
					public double DistanceSquared( Vector4nb other)
					{
						return 
															(X - other.X).Squared()
							+								(Y - other.Y).Squared()
							+								(Z - other.Z).Squared()
							+								(W - other.W).Squared()
							;
					}
									/// <summary>Get the distance between the vectors.</summary>
					public double Distance( ref  Vector4nb other)
					{
						return (double)(							Math.Sqrt(
																	 X - other.X .Squared() 
								+									 Y - other.Y .Squared() 
								+									 Z - other.Z .Squared() 
								+									 W - other.W .Squared() 
															));
					}

					/// <summary>Get the squared distance between the vectors.</summary>
					public double DistanceSquared( ref  Vector4nb other)
					{
						return 
															(X - other.X).Squared()
							+								(Y - other.Y).Squared()
							+								(Z - other.Z).Squared()
							+								(W - other.W).Squared()
							;
					}
				
									/// <summary>Get the normalized form of this vector, which has a magnitude of one.</summary>
					public void Normalize(out Vector4nb result)
					{
						double m = 1.0 / Math.Sqrt( X * X + Y * Y + Z * Z + W * W );
						 result.X = (NormalizedByte)(X * m);  result.Y = (NormalizedByte)(Y * m);  result.Z = (NormalizedByte)(Z * m);  result.W = (NormalizedByte)(W * m); 					}

					/// <summary>Normalize this vector in place, giving it a magnitude of one. An identity vector will become NaN.</summary>
					public void NormalizeInPlace()
					{
						NormalizedByte m = (NormalizedByte)(1.0 / Math.Sqrt( X * X + Y * Y + Z * Z + W * W ));
						 X *= m;  Y *= m;  Z *= m;  W *= m; 					}
				
												
		#endregion Methods

			#region Fields

			/// <summary>The first axis of the <see cref="Vector4nb"/>.</summary>
		public NormalizedByte X;
			/// <summary>The second axis of the <see cref="Vector4nb"/>.</summary>
		public NormalizedByte Y;
			/// <summary>The third axis of the <see cref="Vector4nb"/>.</summary>
		public NormalizedByte Z;
			/// <summary>The fourth axis of the <see cref="Vector4nb"/>.</summary>
		public NormalizedByte W;
	
	#endregion Fields

	#region Properties

	public static readonly Vector4nb Zero = new Vector4nb((NormalizedByte)0, (NormalizedByte)0, (NormalizedByte)0, (NormalizedByte)0);

	#endregion Properties

	#region Constructors

	/// <summary>Initialise a <see cref="Vector4nb"/> from the provided values for each factor.</summary>
	public Vector4nb(NormalizedByte x, NormalizedByte y, NormalizedByte z, NormalizedByte w)
	{
					X = x;
					Y = y;
					Z = z;
					W = w;
			}

	/// <summary>Initialise a <see cref="Vector4nb"/> from a list.</summary>
	public Vector4nb(IList<NormalizedByte> list, int index = 0) : this(list[index + 0], list[index + 1], list[index + 2], list[index + 3]) { }

	/// <summary>Initialise a <see cref="Vector4nb"/> from a single scalar that is applied to all factors.</summary>
	public Vector4nb(NormalizedByte value) : this(value, value, value, value) { }

	#endregion Constructors

	#region Methods

	/// <summary>Get whether this <see cref="Vector4nb"/> has equal factors as the other <see cref="Vector4nb"/>.</summary>
	public bool Equals(Vector4nb other) {
		return  X == other.X && Y == other.Y && Z == other.Z && W == other.W ;
	}

	/// <summary>If the other object is a <see cref="Vector4nb"/> of the same type, get whether this <see cref="Vector4nb"/> has equal factors as it; otherwise return false./summary>
	public override bool Equals(object other)
	{
		if(other is Vector4nb)
			return Equals((Vector4nb)other);
		return base.Equals(other);
	}

	/// <summary>Compute a hash code from combining the axes.</summary>
	public override int GetHashCode()
	{
		return  X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode() ^ W.GetHashCode() ;
	}

	/// <summary>Convert this <see cref="Vector4nb"/> to a string of the form "X, Y, Z, W".</summary>
	public string ToCommaSeparatedString(string format = null, IFormatProvider provider = null) {
		return X.ToString(format, provider) + ", " + Y.ToString(format, provider) + ", " + Z.ToString(format, provider) + ", " + W.ToString(format, provider);
	}

	/// <summary>Convert this <see cref="Vector4nb"/> to a string of the form "Vector4nb(X, Y, Z, W)".</summary>
	public override string ToString()
	{
		return ToString(null, null);
	}

	/// <summary>Convert this <see cref="Vector4nb"/> to a string of the form "Vector4nb(X, Y, Z, W)".</summary>
	public string ToString(string format, IFormatProvider provider)
	{
		return "Vector4nb(" + ToCommaSeparatedString(format, provider) + ")";
	}

	/// <summary>Convert this <see cref="Vector4nb"/> to a string of the form "{X, Y, Z, W".</summary>
	public string ToShortString(string format = null, IFormatProvider provider = null) { return "{" + ToCommaSeparatedString(format, provider) + "}"; }	

	#endregion Methods
	
	public static bool operator ==(Vector4nb a, Vector4nb b) { return a.X == b.X&&a.Y == b.Y&&a.Z == b.Z&&a.W == b.W; }

	public static bool operator !=(Vector4nb a, Vector4nb b) { return a.X != b.X||a.Y != b.Y||a.Z != b.Z||a.W != b.W; }

		#region Operators
		

			// Casting
							public static implicit operator Vector4f(Vector4nb a) { return new Vector4f((float)a.X, (float)a.Y, (float)a.Z, (float)a.W); }
					public static implicit operator Vector4d(Vector4nb a) { return new Vector4d((double)a.X, (double)a.Y, (double)a.Z, (double)a.W); }
					public static explicit operator Vector4i(Vector4nb a) { return new Vector4i((int)a.X, (int)a.Y, (int)a.Z, (int)a.W); }
					public static explicit operator Vector4ui(Vector4nb a) { return new Vector4ui((uint)a.X, (uint)a.Y, (uint)a.Z, (uint)a.W); }
					public static explicit operator Vector4b(Vector4nb a) { return new Vector4b((byte)a.X, (byte)a.Y, (byte)a.Z, (byte)a.W); }
					public static explicit operator Vector4nsb(Vector4nb a) { return new Vector4nsb((NormalizedSByte)a.X, (NormalizedSByte)a.Y, (NormalizedSByte)a.Z, (NormalizedSByte)a.W); }
					public static explicit operator Vector4rgba(Vector4nb a) { return new Vector4rgba((NormalizedByte)a.X, (NormalizedByte)a.Y, (NormalizedByte)a.Z, (NormalizedByte)a.W); }
					public static explicit operator Vector4h(Vector4nb a) { return new Vector4h((Float16)a.X, (Float16)a.Y, (Float16)a.Z, (Float16)a.W); }
					
				// Explicit casting to vectors with a lower order, trimming off axes.
							
			public static Vector4nb operator +(Vector4nb a) { return new Vector4nb((NormalizedByte)(+a.X), (NormalizedByte)(+a.Y), (NormalizedByte)(+a.Z), (NormalizedByte)(+a.W)); }
			public static Vector4nb operator -(Vector4nb a) { return new Vector4nb((NormalizedByte)(-a.X), (NormalizedByte)(-a.Y), (NormalizedByte)(-a.Z), (NormalizedByte)(-a.W)); }
								public static Vector4nb operator +(Vector4nb a, Vector4nb b) { return new Vector4nb((NormalizedByte)(a.X + b.X), (NormalizedByte)(a.Y + b.Y), (NormalizedByte)(a.Z + b.Z), (NormalizedByte)(a.W + b.W)); }
					public static Vector4nb operator +(Vector4nb a, NormalizedByte b) { return new Vector4nb((NormalizedByte)(a.X + b), (NormalizedByte)(a.Y + b), (NormalizedByte)(a.Z + b), (NormalizedByte)(a.W + b)); }
					public static Vector4nb operator +(NormalizedByte a, Vector4nb b) { return new Vector4nb((NormalizedByte)(a + b.X), (NormalizedByte)(a + b.Y), (NormalizedByte)(a + b.Z), (NormalizedByte)(a + b.W)); }
									public static Vector4nb operator -(Vector4nb a, Vector4nb b) { return new Vector4nb((NormalizedByte)(a.X - b.X), (NormalizedByte)(a.Y - b.Y), (NormalizedByte)(a.Z - b.Z), (NormalizedByte)(a.W - b.W)); }
					public static Vector4nb operator -(Vector4nb a, NormalizedByte b) { return new Vector4nb((NormalizedByte)(a.X - b), (NormalizedByte)(a.Y - b), (NormalizedByte)(a.Z - b), (NormalizedByte)(a.W - b)); }
					public static Vector4nb operator -(NormalizedByte a, Vector4nb b) { return new Vector4nb((NormalizedByte)(a - b.X), (NormalizedByte)(a - b.Y), (NormalizedByte)(a - b.Z), (NormalizedByte)(a - b.W)); }
									public static Vector4nb operator *(Vector4nb a, Vector4nb b) { return new Vector4nb((NormalizedByte)(a.X * b.X), (NormalizedByte)(a.Y * b.Y), (NormalizedByte)(a.Z * b.Z), (NormalizedByte)(a.W * b.W)); }
					public static Vector4nb operator *(Vector4nb a, NormalizedByte b) { return new Vector4nb((NormalizedByte)(a.X * b), (NormalizedByte)(a.Y * b), (NormalizedByte)(a.Z * b), (NormalizedByte)(a.W * b)); }
					public static Vector4nb operator *(NormalizedByte a, Vector4nb b) { return new Vector4nb((NormalizedByte)(a * b.X), (NormalizedByte)(a * b.Y), (NormalizedByte)(a * b.Z), (NormalizedByte)(a * b.W)); }
									public static Vector4nb operator /(Vector4nb a, Vector4nb b) { return new Vector4nb((NormalizedByte)(a.X / b.X), (NormalizedByte)(a.Y / b.Y), (NormalizedByte)(a.Z / b.Z), (NormalizedByte)(a.W / b.W)); }
					public static Vector4nb operator /(Vector4nb a, NormalizedByte b) { return new Vector4nb((NormalizedByte)(a.X / b), (NormalizedByte)(a.Y / b), (NormalizedByte)(a.Z / b), (NormalizedByte)(a.W / b)); }
					public static Vector4nb operator /(NormalizedByte a, Vector4nb b) { return new Vector4nb((NormalizedByte)(a / b.X), (NormalizedByte)(a / b.Y), (NormalizedByte)(a / b.Z), (NormalizedByte)(a / b.W)); }
									public static Vector4nb operator %(Vector4nb a, Vector4nb b) { return new Vector4nb((NormalizedByte)(a.X % b.X), (NormalizedByte)(a.Y % b.Y), (NormalizedByte)(a.Z % b.Z), (NormalizedByte)(a.W % b.W)); }
					public static Vector4nb operator %(Vector4nb a, NormalizedByte b) { return new Vector4nb((NormalizedByte)(a.X % b), (NormalizedByte)(a.Y % b), (NormalizedByte)(a.Z % b), (NormalizedByte)(a.W % b)); }
					public static Vector4nb operator %(NormalizedByte a, Vector4nb b) { return new Vector4nb((NormalizedByte)(a % b.X), (NormalizedByte)(a % b.Y), (NormalizedByte)(a % b.Z), (NormalizedByte)(a % b.W)); }
				
		#endregion Operators
	}
	
	
	/// <summary>A four-dimensional vector type using <see cref="NormalizedSByte"/> elements.</summary>
	[VectorTypeAttribute(typeof(NormalizedSByte), 4, false)]
	public partial struct Vector4nsb : IEquatable<Vector4nsb>, IFormattable
	{
		#region Properties
		
							/// <summary>Compute the squared magnitude of the vector, which is the distance from the origin squared. Use <see cref="Magnitude"/> for the unsquared version, which is slower to compute.</summary>
				public double MagnitudeSquared { get { return  X * X + Y * Y + Z * Z + W * W ; } }
			
				/// <summary>Compute the magnitude of the vector, which is the distance from the origin.</summary>
				public double Magnitude
				{
					get
					{
						return (double)(							Math.Sqrt(
																	 X.Squared() 
								+									 Y.Squared() 
								+									 Z.Squared() 
								+									 W.Squared() 
															));
					}
				}

									/// <summary>Get the normalized form of this vector, with a magnitude of one.</summary>
					public Vector4nsb Normalized { get { Vector4nsb result; Normalize(out result); return result; } }
							
							/// <summary>Get the product of multiplying all of the axes together.</summary>
				public NormalizedSByte Product { get { return (NormalizedSByte)( X * Y * Z * W ); } }
			
			/// <summary>Get the sum of all of the axes.</summary>
			public NormalizedSByte Sum { get { return (NormalizedSByte)( X + Y + Z + W ); } }

		#endregion Properties

		#region Constructors
		
							/// <summary>Initialise a vector from double values that are converted into normalised form.</summary>
				public Vector4nsb(double x, double y, double z, double w) : this((NormalizedSByte)x, (NormalizedSByte)y, (NormalizedSByte)z, (NormalizedSByte)w) { }

									public Vector4nsb( Vector4d value) : this((NormalizedSByte)value.X, (NormalizedSByte)value.Y, (NormalizedSByte)value.Z, (NormalizedSByte)value.W) { }
									public Vector4nsb( ref  Vector4d value) : this((NormalizedSByte)value.X, (NormalizedSByte)value.Y, (NormalizedSByte)value.Z, (NormalizedSByte)value.W) { }
									
			
			
							public static readonly Vector4nsb One = new Vector4nsb((NormalizedSByte)1, (NormalizedSByte)1, (NormalizedSByte)1, (NormalizedSByte)1);

									/// <summary>Get a normalized vector where X is 1 and all other axes are 0.</summary>
					public static readonly Vector4nsb UnitX = new Vector4nsb((NormalizedSByte)1, (NormalizedSByte)0, (NormalizedSByte)0, (NormalizedSByte)0);
									/// <summary>Get a normalized vector where Y is 1 and all other axes are 0.</summary>
					public static readonly Vector4nsb UnitY = new Vector4nsb((NormalizedSByte)0, (NormalizedSByte)1, (NormalizedSByte)0, (NormalizedSByte)0);
									/// <summary>Get a normalized vector where Z is 1 and all other axes are 0.</summary>
					public static readonly Vector4nsb UnitZ = new Vector4nsb((NormalizedSByte)0, (NormalizedSByte)0, (NormalizedSByte)1, (NormalizedSByte)0);
									/// <summary>Get a normalized vector where W is 1 and all other axes are 0.</summary>
					public static readonly Vector4nsb UnitW = new Vector4nsb((NormalizedSByte)0, (NormalizedSByte)0, (NormalizedSByte)0, (NormalizedSByte)1);
							

			// Constructors built up of smaller vectors.
			
			// Constructors from much smaller vectors.
			
		#endregion Constructors

		#region Methods

			// Methods that have joint ref forms.
							/// <summary>Compute the dot product of this and other vector, which is the sum of the product of each axis of each vector.</summary>
				/// <param name="other">The other vector to calculate a dot product with. </param>
				public double Dot( Vector4nsb other) { return  X * other.X + Y * other.Y + Z * other.Z + W * other.W ; }
							/// <summary>Compute the dot product of this and other vector, which is the sum of the product of each axis of each vector.</summary>
				/// <param name="other">The other vector to calculate a dot product with. The value of the parameter will not be changed; <c>ref</c> is used for optimisation only.</param>
				public double Dot( ref  Vector4nsb other) { return  X * other.X + Y * other.Y + Z * other.Z + W * other.W ; }
			
							public Vector4nsb Clamp( Vector4nsb min,  Vector4nsb max )
				{
					Vector4nsb result;
											result.X = X < min.X ? min.X : X > max.X ? max.X : X;
											result.Y = Y < min.Y ? min.Y : Y > max.Y ? max.Y : Y;
											result.Z = Z < min.Z ? min.Z : Z > max.Z ? max.Z : Z;
											result.W = W < min.W ? min.W : W > max.W ? max.W : W;
										return result;
				}
							public void Clamp( ref  Vector4nsb min,  ref  Vector4nsb max , out Vector4nsb result)
				{
					
											result.X = X < min.X ? min.X : X > max.X ? max.X : X;
											result.Y = Y < min.Y ? min.Y : Y > max.Y ? max.Y : Y;
											result.Z = Z < min.Z ? min.Z : Z > max.Z ? max.Z : Z;
											result.W = W < min.W ? min.W : W > max.W ? max.W : W;
										return;
				}
			
			// Floating-point methods
												/// <summary>Get the distance between the vectors.</summary>
					public double Distance( Vector4nsb other)
					{
						return (double)(							Math.Sqrt(
																	 X - other.X .Squared() 
								+									 Y - other.Y .Squared() 
								+									 Z - other.Z .Squared() 
								+									 W - other.W .Squared() 
															));
					}

					/// <summary>Get the squared distance between the vectors.</summary>
					public double DistanceSquared( Vector4nsb other)
					{
						return 
															(X - other.X).Squared()
							+								(Y - other.Y).Squared()
							+								(Z - other.Z).Squared()
							+								(W - other.W).Squared()
							;
					}
									/// <summary>Get the distance between the vectors.</summary>
					public double Distance( ref  Vector4nsb other)
					{
						return (double)(							Math.Sqrt(
																	 X - other.X .Squared() 
								+									 Y - other.Y .Squared() 
								+									 Z - other.Z .Squared() 
								+									 W - other.W .Squared() 
															));
					}

					/// <summary>Get the squared distance between the vectors.</summary>
					public double DistanceSquared( ref  Vector4nsb other)
					{
						return 
															(X - other.X).Squared()
							+								(Y - other.Y).Squared()
							+								(Z - other.Z).Squared()
							+								(W - other.W).Squared()
							;
					}
				
									/// <summary>Get the normalized form of this vector, which has a magnitude of one.</summary>
					public void Normalize(out Vector4nsb result)
					{
						double m = 1.0 / Math.Sqrt( X * X + Y * Y + Z * Z + W * W );
						 result.X = (NormalizedSByte)(X * m);  result.Y = (NormalizedSByte)(Y * m);  result.Z = (NormalizedSByte)(Z * m);  result.W = (NormalizedSByte)(W * m); 					}

					/// <summary>Normalize this vector in place, giving it a magnitude of one. An identity vector will become NaN.</summary>
					public void NormalizeInPlace()
					{
						NormalizedSByte m = (NormalizedSByte)(1.0 / Math.Sqrt( X * X + Y * Y + Z * Z + W * W ));
						 X *= m;  Y *= m;  Z *= m;  W *= m; 					}
				
												
		#endregion Methods

			#region Fields

			/// <summary>The first axis of the <see cref="Vector4nsb"/>.</summary>
		public NormalizedSByte X;
			/// <summary>The second axis of the <see cref="Vector4nsb"/>.</summary>
		public NormalizedSByte Y;
			/// <summary>The third axis of the <see cref="Vector4nsb"/>.</summary>
		public NormalizedSByte Z;
			/// <summary>The fourth axis of the <see cref="Vector4nsb"/>.</summary>
		public NormalizedSByte W;
	
	#endregion Fields

	#region Properties

	public static readonly Vector4nsb Zero = new Vector4nsb((NormalizedSByte)0, (NormalizedSByte)0, (NormalizedSByte)0, (NormalizedSByte)0);

	#endregion Properties

	#region Constructors

	/// <summary>Initialise a <see cref="Vector4nsb"/> from the provided values for each factor.</summary>
	public Vector4nsb(NormalizedSByte x, NormalizedSByte y, NormalizedSByte z, NormalizedSByte w)
	{
					X = x;
					Y = y;
					Z = z;
					W = w;
			}

	/// <summary>Initialise a <see cref="Vector4nsb"/> from a list.</summary>
	public Vector4nsb(IList<NormalizedSByte> list, int index = 0) : this(list[index + 0], list[index + 1], list[index + 2], list[index + 3]) { }

	/// <summary>Initialise a <see cref="Vector4nsb"/> from a single scalar that is applied to all factors.</summary>
	public Vector4nsb(NormalizedSByte value) : this(value, value, value, value) { }

	#endregion Constructors

	#region Methods

	/// <summary>Get whether this <see cref="Vector4nsb"/> has equal factors as the other <see cref="Vector4nsb"/>.</summary>
	public bool Equals(Vector4nsb other) {
		return  X == other.X && Y == other.Y && Z == other.Z && W == other.W ;
	}

	/// <summary>If the other object is a <see cref="Vector4nsb"/> of the same type, get whether this <see cref="Vector4nsb"/> has equal factors as it; otherwise return false./summary>
	public override bool Equals(object other)
	{
		if(other is Vector4nsb)
			return Equals((Vector4nsb)other);
		return base.Equals(other);
	}

	/// <summary>Compute a hash code from combining the axes.</summary>
	public override int GetHashCode()
	{
		return  X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode() ^ W.GetHashCode() ;
	}

	/// <summary>Convert this <see cref="Vector4nsb"/> to a string of the form "X, Y, Z, W".</summary>
	public string ToCommaSeparatedString(string format = null, IFormatProvider provider = null) {
		return X.ToString(format, provider) + ", " + Y.ToString(format, provider) + ", " + Z.ToString(format, provider) + ", " + W.ToString(format, provider);
	}

	/// <summary>Convert this <see cref="Vector4nsb"/> to a string of the form "Vector4nsb(X, Y, Z, W)".</summary>
	public override string ToString()
	{
		return ToString(null, null);
	}

	/// <summary>Convert this <see cref="Vector4nsb"/> to a string of the form "Vector4nsb(X, Y, Z, W)".</summary>
	public string ToString(string format, IFormatProvider provider)
	{
		return "Vector4nsb(" + ToCommaSeparatedString(format, provider) + ")";
	}

	/// <summary>Convert this <see cref="Vector4nsb"/> to a string of the form "{X, Y, Z, W".</summary>
	public string ToShortString(string format = null, IFormatProvider provider = null) { return "{" + ToCommaSeparatedString(format, provider) + "}"; }	

	#endregion Methods
	
	public static bool operator ==(Vector4nsb a, Vector4nsb b) { return a.X == b.X&&a.Y == b.Y&&a.Z == b.Z&&a.W == b.W; }

	public static bool operator !=(Vector4nsb a, Vector4nsb b) { return a.X != b.X||a.Y != b.Y||a.Z != b.Z||a.W != b.W; }

		#region Operators
		

			// Casting
							public static implicit operator Vector4f(Vector4nsb a) { return new Vector4f((float)a.X, (float)a.Y, (float)a.Z, (float)a.W); }
					public static implicit operator Vector4d(Vector4nsb a) { return new Vector4d((double)a.X, (double)a.Y, (double)a.Z, (double)a.W); }
					public static explicit operator Vector4i(Vector4nsb a) { return new Vector4i((int)a.X, (int)a.Y, (int)a.Z, (int)a.W); }
					public static explicit operator Vector4ui(Vector4nsb a) { return new Vector4ui((uint)a.X, (uint)a.Y, (uint)a.Z, (uint)a.W); }
					public static explicit operator Vector4b(Vector4nsb a) { return new Vector4b((byte)a.X, (byte)a.Y, (byte)a.Z, (byte)a.W); }
					public static explicit operator Vector4nb(Vector4nsb a) { return new Vector4nb((NormalizedByte)a.X, (NormalizedByte)a.Y, (NormalizedByte)a.Z, (NormalizedByte)a.W); }
					public static explicit operator Vector4rgba(Vector4nsb a) { return new Vector4rgba((NormalizedByte)a.X, (NormalizedByte)a.Y, (NormalizedByte)a.Z, (NormalizedByte)a.W); }
					public static explicit operator Vector4h(Vector4nsb a) { return new Vector4h((Float16)a.X, (Float16)a.Y, (Float16)a.Z, (Float16)a.W); }
					
				// Explicit casting to vectors with a lower order, trimming off axes.
							
			public static Vector4nsb operator +(Vector4nsb a) { return new Vector4nsb((NormalizedSByte)(+a.X), (NormalizedSByte)(+a.Y), (NormalizedSByte)(+a.Z), (NormalizedSByte)(+a.W)); }
			public static Vector4nsb operator -(Vector4nsb a) { return new Vector4nsb((NormalizedSByte)(-a.X), (NormalizedSByte)(-a.Y), (NormalizedSByte)(-a.Z), (NormalizedSByte)(-a.W)); }
								public static Vector4nsb operator +(Vector4nsb a, Vector4nsb b) { return new Vector4nsb((NormalizedSByte)(a.X + b.X), (NormalizedSByte)(a.Y + b.Y), (NormalizedSByte)(a.Z + b.Z), (NormalizedSByte)(a.W + b.W)); }
					public static Vector4nsb operator +(Vector4nsb a, NormalizedSByte b) { return new Vector4nsb((NormalizedSByte)(a.X + b), (NormalizedSByte)(a.Y + b), (NormalizedSByte)(a.Z + b), (NormalizedSByte)(a.W + b)); }
					public static Vector4nsb operator +(NormalizedSByte a, Vector4nsb b) { return new Vector4nsb((NormalizedSByte)(a + b.X), (NormalizedSByte)(a + b.Y), (NormalizedSByte)(a + b.Z), (NormalizedSByte)(a + b.W)); }
									public static Vector4nsb operator -(Vector4nsb a, Vector4nsb b) { return new Vector4nsb((NormalizedSByte)(a.X - b.X), (NormalizedSByte)(a.Y - b.Y), (NormalizedSByte)(a.Z - b.Z), (NormalizedSByte)(a.W - b.W)); }
					public static Vector4nsb operator -(Vector4nsb a, NormalizedSByte b) { return new Vector4nsb((NormalizedSByte)(a.X - b), (NormalizedSByte)(a.Y - b), (NormalizedSByte)(a.Z - b), (NormalizedSByte)(a.W - b)); }
					public static Vector4nsb operator -(NormalizedSByte a, Vector4nsb b) { return new Vector4nsb((NormalizedSByte)(a - b.X), (NormalizedSByte)(a - b.Y), (NormalizedSByte)(a - b.Z), (NormalizedSByte)(a - b.W)); }
									public static Vector4nsb operator *(Vector4nsb a, Vector4nsb b) { return new Vector4nsb((NormalizedSByte)(a.X * b.X), (NormalizedSByte)(a.Y * b.Y), (NormalizedSByte)(a.Z * b.Z), (NormalizedSByte)(a.W * b.W)); }
					public static Vector4nsb operator *(Vector4nsb a, NormalizedSByte b) { return new Vector4nsb((NormalizedSByte)(a.X * b), (NormalizedSByte)(a.Y * b), (NormalizedSByte)(a.Z * b), (NormalizedSByte)(a.W * b)); }
					public static Vector4nsb operator *(NormalizedSByte a, Vector4nsb b) { return new Vector4nsb((NormalizedSByte)(a * b.X), (NormalizedSByte)(a * b.Y), (NormalizedSByte)(a * b.Z), (NormalizedSByte)(a * b.W)); }
									public static Vector4nsb operator /(Vector4nsb a, Vector4nsb b) { return new Vector4nsb((NormalizedSByte)(a.X / b.X), (NormalizedSByte)(a.Y / b.Y), (NormalizedSByte)(a.Z / b.Z), (NormalizedSByte)(a.W / b.W)); }
					public static Vector4nsb operator /(Vector4nsb a, NormalizedSByte b) { return new Vector4nsb((NormalizedSByte)(a.X / b), (NormalizedSByte)(a.Y / b), (NormalizedSByte)(a.Z / b), (NormalizedSByte)(a.W / b)); }
					public static Vector4nsb operator /(NormalizedSByte a, Vector4nsb b) { return new Vector4nsb((NormalizedSByte)(a / b.X), (NormalizedSByte)(a / b.Y), (NormalizedSByte)(a / b.Z), (NormalizedSByte)(a / b.W)); }
									public static Vector4nsb operator %(Vector4nsb a, Vector4nsb b) { return new Vector4nsb((NormalizedSByte)(a.X % b.X), (NormalizedSByte)(a.Y % b.Y), (NormalizedSByte)(a.Z % b.Z), (NormalizedSByte)(a.W % b.W)); }
					public static Vector4nsb operator %(Vector4nsb a, NormalizedSByte b) { return new Vector4nsb((NormalizedSByte)(a.X % b), (NormalizedSByte)(a.Y % b), (NormalizedSByte)(a.Z % b), (NormalizedSByte)(a.W % b)); }
					public static Vector4nsb operator %(NormalizedSByte a, Vector4nsb b) { return new Vector4nsb((NormalizedSByte)(a % b.X), (NormalizedSByte)(a % b.Y), (NormalizedSByte)(a % b.Z), (NormalizedSByte)(a % b.W)); }
				
		#endregion Operators
	}
	
	
	/// <summary>A four-dimensional vector type using <see cref="NormalizedByte"/> elements.</summary>
	[VectorTypeAttribute(typeof(NormalizedByte), 4, false)]
	public partial struct Vector4rgba : IEquatable<Vector4rgba>, IFormattable
	{
		#region Properties
		
							/// <summary>Compute the squared magnitude of the vector, which is the distance from the origin squared. Use <see cref="Magnitude"/> for the unsquared version, which is slower to compute.</summary>
				public double MagnitudeSquared { get { return  X * X + Y * Y + Z * Z + W * W ; } }
			
				/// <summary>Compute the magnitude of the vector, which is the distance from the origin.</summary>
				public double Magnitude
				{
					get
					{
						return (double)(							Math.Sqrt(
																	 X.Squared() 
								+									 Y.Squared() 
								+									 Z.Squared() 
								+									 W.Squared() 
															));
					}
				}

									/// <summary>Get the normalized form of this vector, with a magnitude of one.</summary>
					public Vector4rgba Normalized { get { Vector4rgba result; Normalize(out result); return result; } }
							
							/// <summary>Get the product of multiplying all of the axes together.</summary>
				public NormalizedByte Product { get { return (NormalizedByte)( X * Y * Z * W ); } }
			
			/// <summary>Get the sum of all of the axes.</summary>
			public NormalizedByte Sum { get { return (NormalizedByte)( X + Y + Z + W ); } }

		#endregion Properties

		#region Constructors
		
							/// <summary>Initialise a vector from double values that are converted into normalised form.</summary>
				public Vector4rgba(double x, double y, double z, double w) : this((NormalizedByte)x, (NormalizedByte)y, (NormalizedByte)z, (NormalizedByte)w) { }

									public Vector4rgba( Vector4d value) : this((NormalizedByte)value.X, (NormalizedByte)value.Y, (NormalizedByte)value.Z, (NormalizedByte)value.W) { }
									public Vector4rgba( ref  Vector4d value) : this((NormalizedByte)value.X, (NormalizedByte)value.Y, (NormalizedByte)value.Z, (NormalizedByte)value.W) { }
									
			
			
							public static readonly Vector4rgba One = new Vector4rgba((NormalizedByte)1, (NormalizedByte)1, (NormalizedByte)1, (NormalizedByte)1);

									/// <summary>Get a normalized vector where X is 1 and all other axes are 0.</summary>
					public static readonly Vector4rgba UnitX = new Vector4rgba((NormalizedByte)1, (NormalizedByte)0, (NormalizedByte)0, (NormalizedByte)0);
									/// <summary>Get a normalized vector where Y is 1 and all other axes are 0.</summary>
					public static readonly Vector4rgba UnitY = new Vector4rgba((NormalizedByte)0, (NormalizedByte)1, (NormalizedByte)0, (NormalizedByte)0);
									/// <summary>Get a normalized vector where Z is 1 and all other axes are 0.</summary>
					public static readonly Vector4rgba UnitZ = new Vector4rgba((NormalizedByte)0, (NormalizedByte)0, (NormalizedByte)1, (NormalizedByte)0);
									/// <summary>Get a normalized vector where W is 1 and all other axes are 0.</summary>
					public static readonly Vector4rgba UnitW = new Vector4rgba((NormalizedByte)0, (NormalizedByte)0, (NormalizedByte)0, (NormalizedByte)1);
							

			// Constructors built up of smaller vectors.
			
			// Constructors from much smaller vectors.
			
		#endregion Constructors

		#region Methods

			// Methods that have joint ref forms.
							/// <summary>Compute the dot product of this and other vector, which is the sum of the product of each axis of each vector.</summary>
				/// <param name="other">The other vector to calculate a dot product with. </param>
				public double Dot( Vector4rgba other) { return  X * other.X + Y * other.Y + Z * other.Z + W * other.W ; }
							/// <summary>Compute the dot product of this and other vector, which is the sum of the product of each axis of each vector.</summary>
				/// <param name="other">The other vector to calculate a dot product with. The value of the parameter will not be changed; <c>ref</c> is used for optimisation only.</param>
				public double Dot( ref  Vector4rgba other) { return  X * other.X + Y * other.Y + Z * other.Z + W * other.W ; }
			
							public Vector4rgba Clamp( Vector4rgba min,  Vector4rgba max )
				{
					Vector4rgba result;
											result.X = X < min.X ? min.X : X > max.X ? max.X : X;
											result.Y = Y < min.Y ? min.Y : Y > max.Y ? max.Y : Y;
											result.Z = Z < min.Z ? min.Z : Z > max.Z ? max.Z : Z;
											result.W = W < min.W ? min.W : W > max.W ? max.W : W;
										return result;
				}
							public void Clamp( ref  Vector4rgba min,  ref  Vector4rgba max , out Vector4rgba result)
				{
					
											result.X = X < min.X ? min.X : X > max.X ? max.X : X;
											result.Y = Y < min.Y ? min.Y : Y > max.Y ? max.Y : Y;
											result.Z = Z < min.Z ? min.Z : Z > max.Z ? max.Z : Z;
											result.W = W < min.W ? min.W : W > max.W ? max.W : W;
										return;
				}
			
			// Floating-point methods
												/// <summary>Get the distance between the vectors.</summary>
					public double Distance( Vector4rgba other)
					{
						return (double)(							Math.Sqrt(
																	 X - other.X .Squared() 
								+									 Y - other.Y .Squared() 
								+									 Z - other.Z .Squared() 
								+									 W - other.W .Squared() 
															));
					}

					/// <summary>Get the squared distance between the vectors.</summary>
					public double DistanceSquared( Vector4rgba other)
					{
						return 
															(X - other.X).Squared()
							+								(Y - other.Y).Squared()
							+								(Z - other.Z).Squared()
							+								(W - other.W).Squared()
							;
					}
									/// <summary>Get the distance between the vectors.</summary>
					public double Distance( ref  Vector4rgba other)
					{
						return (double)(							Math.Sqrt(
																	 X - other.X .Squared() 
								+									 Y - other.Y .Squared() 
								+									 Z - other.Z .Squared() 
								+									 W - other.W .Squared() 
															));
					}

					/// <summary>Get the squared distance between the vectors.</summary>
					public double DistanceSquared( ref  Vector4rgba other)
					{
						return 
															(X - other.X).Squared()
							+								(Y - other.Y).Squared()
							+								(Z - other.Z).Squared()
							+								(W - other.W).Squared()
							;
					}
				
									/// <summary>Get the normalized form of this vector, which has a magnitude of one.</summary>
					public void Normalize(out Vector4rgba result)
					{
						double m = 1.0 / Math.Sqrt( X * X + Y * Y + Z * Z + W * W );
						 result.X = (NormalizedByte)(X * m);  result.Y = (NormalizedByte)(Y * m);  result.Z = (NormalizedByte)(Z * m);  result.W = (NormalizedByte)(W * m); 					}

					/// <summary>Normalize this vector in place, giving it a magnitude of one. An identity vector will become NaN.</summary>
					public void NormalizeInPlace()
					{
						NormalizedByte m = (NormalizedByte)(1.0 / Math.Sqrt( X * X + Y * Y + Z * Z + W * W ));
						 X *= m;  Y *= m;  Z *= m;  W *= m; 					}
				
												
		#endregion Methods

			#region Fields

			/// <summary>The first axis of the <see cref="Vector4rgba"/>.</summary>
		public NormalizedByte X;
			/// <summary>The second axis of the <see cref="Vector4rgba"/>.</summary>
		public NormalizedByte Y;
			/// <summary>The third axis of the <see cref="Vector4rgba"/>.</summary>
		public NormalizedByte Z;
			/// <summary>The fourth axis of the <see cref="Vector4rgba"/>.</summary>
		public NormalizedByte W;
	
	#endregion Fields

	#region Properties

	public static readonly Vector4rgba Zero = new Vector4rgba((NormalizedByte)0, (NormalizedByte)0, (NormalizedByte)0, (NormalizedByte)0);

	#endregion Properties

	#region Constructors

	/// <summary>Initialise a <see cref="Vector4rgba"/> from the provided values for each factor.</summary>
	public Vector4rgba(NormalizedByte x, NormalizedByte y, NormalizedByte z, NormalizedByte w)
	{
					X = x;
					Y = y;
					Z = z;
					W = w;
			}

	/// <summary>Initialise a <see cref="Vector4rgba"/> from a list.</summary>
	public Vector4rgba(IList<NormalizedByte> list, int index = 0) : this(list[index + 0], list[index + 1], list[index + 2], list[index + 3]) { }

	/// <summary>Initialise a <see cref="Vector4rgba"/> from a single scalar that is applied to all factors.</summary>
	public Vector4rgba(NormalizedByte value) : this(value, value, value, value) { }

	#endregion Constructors

	#region Methods

	/// <summary>Get whether this <see cref="Vector4rgba"/> has equal factors as the other <see cref="Vector4rgba"/>.</summary>
	public bool Equals(Vector4rgba other) {
		return  X == other.X && Y == other.Y && Z == other.Z && W == other.W ;
	}

	/// <summary>If the other object is a <see cref="Vector4rgba"/> of the same type, get whether this <see cref="Vector4rgba"/> has equal factors as it; otherwise return false./summary>
	public override bool Equals(object other)
	{
		if(other is Vector4rgba)
			return Equals((Vector4rgba)other);
		return base.Equals(other);
	}

	/// <summary>Compute a hash code from combining the axes.</summary>
	public override int GetHashCode()
	{
		return  X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode() ^ W.GetHashCode() ;
	}

	/// <summary>Convert this <see cref="Vector4rgba"/> to a string of the form "X, Y, Z, W".</summary>
	public string ToCommaSeparatedString(string format = null, IFormatProvider provider = null) {
		return X.ToString(format, provider) + ", " + Y.ToString(format, provider) + ", " + Z.ToString(format, provider) + ", " + W.ToString(format, provider);
	}

	/// <summary>Convert this <see cref="Vector4rgba"/> to a string of the form "Vector4rgba(X, Y, Z, W)".</summary>
	public override string ToString()
	{
		return ToString(null, null);
	}

	/// <summary>Convert this <see cref="Vector4rgba"/> to a string of the form "Vector4rgba(X, Y, Z, W)".</summary>
	public string ToString(string format, IFormatProvider provider)
	{
		return "Vector4rgba(" + ToCommaSeparatedString(format, provider) + ")";
	}

	/// <summary>Convert this <see cref="Vector4rgba"/> to a string of the form "{X, Y, Z, W".</summary>
	public string ToShortString(string format = null, IFormatProvider provider = null) { return "{" + ToCommaSeparatedString(format, provider) + "}"; }	

	#endregion Methods
	
	public static bool operator ==(Vector4rgba a, Vector4rgba b) { return a.X == b.X&&a.Y == b.Y&&a.Z == b.Z&&a.W == b.W; }

	public static bool operator !=(Vector4rgba a, Vector4rgba b) { return a.X != b.X||a.Y != b.Y||a.Z != b.Z||a.W != b.W; }

		#region Operators
		

			// Casting
							public static implicit operator Vector4f(Vector4rgba a) { return new Vector4f((float)a.X, (float)a.Y, (float)a.Z, (float)a.W); }
					public static implicit operator Vector4d(Vector4rgba a) { return new Vector4d((double)a.X, (double)a.Y, (double)a.Z, (double)a.W); }
					public static explicit operator Vector4i(Vector4rgba a) { return new Vector4i((int)a.X, (int)a.Y, (int)a.Z, (int)a.W); }
					public static explicit operator Vector4ui(Vector4rgba a) { return new Vector4ui((uint)a.X, (uint)a.Y, (uint)a.Z, (uint)a.W); }
					public static explicit operator Vector4b(Vector4rgba a) { return new Vector4b((byte)a.X, (byte)a.Y, (byte)a.Z, (byte)a.W); }
					public static explicit operator Vector4nb(Vector4rgba a) { return new Vector4nb((NormalizedByte)a.X, (NormalizedByte)a.Y, (NormalizedByte)a.Z, (NormalizedByte)a.W); }
					public static explicit operator Vector4nsb(Vector4rgba a) { return new Vector4nsb((NormalizedSByte)a.X, (NormalizedSByte)a.Y, (NormalizedSByte)a.Z, (NormalizedSByte)a.W); }
					public static explicit operator Vector4h(Vector4rgba a) { return new Vector4h((Float16)a.X, (Float16)a.Y, (Float16)a.Z, (Float16)a.W); }
					
				// Explicit casting to vectors with a lower order, trimming off axes.
							
			public static Vector4rgba operator +(Vector4rgba a) { return new Vector4rgba((NormalizedByte)(+a.X), (NormalizedByte)(+a.Y), (NormalizedByte)(+a.Z), (NormalizedByte)(+a.W)); }
			public static Vector4rgba operator -(Vector4rgba a) { return new Vector4rgba((NormalizedByte)(-a.X), (NormalizedByte)(-a.Y), (NormalizedByte)(-a.Z), (NormalizedByte)(-a.W)); }
								public static Vector4rgba operator +(Vector4rgba a, Vector4rgba b) { return new Vector4rgba((NormalizedByte)(a.X + b.X), (NormalizedByte)(a.Y + b.Y), (NormalizedByte)(a.Z + b.Z), (NormalizedByte)(a.W + b.W)); }
					public static Vector4rgba operator +(Vector4rgba a, NormalizedByte b) { return new Vector4rgba((NormalizedByte)(a.X + b), (NormalizedByte)(a.Y + b), (NormalizedByte)(a.Z + b), (NormalizedByte)(a.W + b)); }
					public static Vector4rgba operator +(NormalizedByte a, Vector4rgba b) { return new Vector4rgba((NormalizedByte)(a + b.X), (NormalizedByte)(a + b.Y), (NormalizedByte)(a + b.Z), (NormalizedByte)(a + b.W)); }
									public static Vector4rgba operator -(Vector4rgba a, Vector4rgba b) { return new Vector4rgba((NormalizedByte)(a.X - b.X), (NormalizedByte)(a.Y - b.Y), (NormalizedByte)(a.Z - b.Z), (NormalizedByte)(a.W - b.W)); }
					public static Vector4rgba operator -(Vector4rgba a, NormalizedByte b) { return new Vector4rgba((NormalizedByte)(a.X - b), (NormalizedByte)(a.Y - b), (NormalizedByte)(a.Z - b), (NormalizedByte)(a.W - b)); }
					public static Vector4rgba operator -(NormalizedByte a, Vector4rgba b) { return new Vector4rgba((NormalizedByte)(a - b.X), (NormalizedByte)(a - b.Y), (NormalizedByte)(a - b.Z), (NormalizedByte)(a - b.W)); }
									public static Vector4rgba operator *(Vector4rgba a, Vector4rgba b) { return new Vector4rgba((NormalizedByte)(a.X * b.X), (NormalizedByte)(a.Y * b.Y), (NormalizedByte)(a.Z * b.Z), (NormalizedByte)(a.W * b.W)); }
					public static Vector4rgba operator *(Vector4rgba a, NormalizedByte b) { return new Vector4rgba((NormalizedByte)(a.X * b), (NormalizedByte)(a.Y * b), (NormalizedByte)(a.Z * b), (NormalizedByte)(a.W * b)); }
					public static Vector4rgba operator *(NormalizedByte a, Vector4rgba b) { return new Vector4rgba((NormalizedByte)(a * b.X), (NormalizedByte)(a * b.Y), (NormalizedByte)(a * b.Z), (NormalizedByte)(a * b.W)); }
									public static Vector4rgba operator /(Vector4rgba a, Vector4rgba b) { return new Vector4rgba((NormalizedByte)(a.X / b.X), (NormalizedByte)(a.Y / b.Y), (NormalizedByte)(a.Z / b.Z), (NormalizedByte)(a.W / b.W)); }
					public static Vector4rgba operator /(Vector4rgba a, NormalizedByte b) { return new Vector4rgba((NormalizedByte)(a.X / b), (NormalizedByte)(a.Y / b), (NormalizedByte)(a.Z / b), (NormalizedByte)(a.W / b)); }
					public static Vector4rgba operator /(NormalizedByte a, Vector4rgba b) { return new Vector4rgba((NormalizedByte)(a / b.X), (NormalizedByte)(a / b.Y), (NormalizedByte)(a / b.Z), (NormalizedByte)(a / b.W)); }
									public static Vector4rgba operator %(Vector4rgba a, Vector4rgba b) { return new Vector4rgba((NormalizedByte)(a.X % b.X), (NormalizedByte)(a.Y % b.Y), (NormalizedByte)(a.Z % b.Z), (NormalizedByte)(a.W % b.W)); }
					public static Vector4rgba operator %(Vector4rgba a, NormalizedByte b) { return new Vector4rgba((NormalizedByte)(a.X % b), (NormalizedByte)(a.Y % b), (NormalizedByte)(a.Z % b), (NormalizedByte)(a.W % b)); }
					public static Vector4rgba operator %(NormalizedByte a, Vector4rgba b) { return new Vector4rgba((NormalizedByte)(a % b.X), (NormalizedByte)(a % b.Y), (NormalizedByte)(a % b.Z), (NormalizedByte)(a % b.W)); }
				
		#endregion Operators
	}
	
	
	/// <summary>A four-dimensional vector type using <see cref="Float16"/> elements.</summary>
	[VectorTypeAttribute(typeof(Float16), 4, false)]
	public partial struct Vector4h : IEquatable<Vector4h>, IFormattable
	{
		#region Properties
		
							/// <summary>Compute the squared magnitude of the vector, which is the distance from the origin squared. Use <see cref="Magnitude"/> for the unsquared version, which is slower to compute.</summary>
				public Float16 MagnitudeSquared { get { return  X * X + Y * Y + Z * Z + W * W ; } }
			
				/// <summary>Compute the magnitude of the vector, which is the distance from the origin.</summary>
				public Float16 Magnitude
				{
					get
					{
						return (Float16)(							Math.Sqrt(
																	 X.Squared() 
								+									 Y.Squared() 
								+									 Z.Squared() 
								+									 W.Squared() 
															));
					}
				}

									/// <summary>Get the normalized form of this vector, with a magnitude of one.</summary>
					public Vector4h Normalized { get { Vector4h result; Normalize(out result); return result; } }
							
							/// <summary>Get the product of multiplying all of the axes together.</summary>
				public Float16 Product { get { return (Float16)( X * Y * Z * W ); } }
			
			/// <summary>Get the sum of all of the axes.</summary>
			public Float16 Sum { get { return (Float16)( X + Y + Z + W ); } }

		#endregion Properties

		#region Constructors
		
			
			
							public static readonly Vector4h One = new Vector4h((Float16)1, (Float16)1, (Float16)1, (Float16)1);

									/// <summary>Get a normalized vector where X is 1 and all other axes are 0.</summary>
					public static readonly Vector4h UnitX = new Vector4h((Float16)1, (Float16)0, (Float16)0, (Float16)0);
									/// <summary>Get a normalized vector where Y is 1 and all other axes are 0.</summary>
					public static readonly Vector4h UnitY = new Vector4h((Float16)0, (Float16)1, (Float16)0, (Float16)0);
									/// <summary>Get a normalized vector where Z is 1 and all other axes are 0.</summary>
					public static readonly Vector4h UnitZ = new Vector4h((Float16)0, (Float16)0, (Float16)1, (Float16)0);
									/// <summary>Get a normalized vector where W is 1 and all other axes are 0.</summary>
					public static readonly Vector4h UnitW = new Vector4h((Float16)0, (Float16)0, (Float16)0, (Float16)1);
							

			// Constructors built up of smaller vectors.
			
			// Constructors from much smaller vectors.
			
		#endregion Constructors

		#region Methods

			// Methods that have joint ref forms.
							/// <summary>Compute the dot product of this and other vector, which is the sum of the product of each axis of each vector.</summary>
				/// <param name="other">The other vector to calculate a dot product with. </param>
				public Float16 Dot( Vector4h other) { return  X * other.X + Y * other.Y + Z * other.Z + W * other.W ; }
							/// <summary>Compute the dot product of this and other vector, which is the sum of the product of each axis of each vector.</summary>
				/// <param name="other">The other vector to calculate a dot product with. The value of the parameter will not be changed; <c>ref</c> is used for optimisation only.</param>
				public Float16 Dot( ref  Vector4h other) { return  X * other.X + Y * other.Y + Z * other.Z + W * other.W ; }
			
							public Vector4h Clamp( Vector4h min,  Vector4h max )
				{
					Vector4h result;
											result.X = X < min.X ? min.X : X > max.X ? max.X : X;
											result.Y = Y < min.Y ? min.Y : Y > max.Y ? max.Y : Y;
											result.Z = Z < min.Z ? min.Z : Z > max.Z ? max.Z : Z;
											result.W = W < min.W ? min.W : W > max.W ? max.W : W;
										return result;
				}
							public void Clamp( ref  Vector4h min,  ref  Vector4h max , out Vector4h result)
				{
					
											result.X = X < min.X ? min.X : X > max.X ? max.X : X;
											result.Y = Y < min.Y ? min.Y : Y > max.Y ? max.Y : Y;
											result.Z = Z < min.Z ? min.Z : Z > max.Z ? max.Z : Z;
											result.W = W < min.W ? min.W : W > max.W ? max.W : W;
										return;
				}
			
			// Floating-point methods
												/// <summary>Get the distance between the vectors.</summary>
					public Float16 Distance( Vector4h other)
					{
						return (Float16)(							Math.Sqrt(
																	 X - other.X .Squared() 
								+									 Y - other.Y .Squared() 
								+									 Z - other.Z .Squared() 
								+									 W - other.W .Squared() 
															));
					}

					/// <summary>Get the squared distance between the vectors.</summary>
					public Float16 DistanceSquared( Vector4h other)
					{
						return 
															(X - other.X).Squared()
							+								(Y - other.Y).Squared()
							+								(Z - other.Z).Squared()
							+								(W - other.W).Squared()
							;
					}
									/// <summary>Get the distance between the vectors.</summary>
					public Float16 Distance( ref  Vector4h other)
					{
						return (Float16)(							Math.Sqrt(
																	 X - other.X .Squared() 
								+									 Y - other.Y .Squared() 
								+									 Z - other.Z .Squared() 
								+									 W - other.W .Squared() 
															));
					}

					/// <summary>Get the squared distance between the vectors.</summary>
					public Float16 DistanceSquared( ref  Vector4h other)
					{
						return 
															(X - other.X).Squared()
							+								(Y - other.Y).Squared()
							+								(Z - other.Z).Squared()
							+								(W - other.W).Squared()
							;
					}
				
									/// <summary>Get the normalized form of this vector, which has a magnitude of one.</summary>
					public void Normalize(out Vector4h result)
					{
						double m = 1.0 / Math.Sqrt( X * X + Y * Y + Z * Z + W * W );
						 result.X = (Float16)(X * m);  result.Y = (Float16)(Y * m);  result.Z = (Float16)(Z * m);  result.W = (Float16)(W * m); 					}

					/// <summary>Normalize this vector in place, giving it a magnitude of one. An identity vector will become NaN.</summary>
					public void NormalizeInPlace()
					{
						Float16 m = (Float16)(1.0 / Math.Sqrt( X * X + Y * Y + Z * Z + W * W ));
						 X *= m;  Y *= m;  Z *= m;  W *= m; 					}
				
												
		#endregion Methods

			#region Fields

			/// <summary>The first axis of the <see cref="Vector4h"/>.</summary>
		public Float16 X;
			/// <summary>The second axis of the <see cref="Vector4h"/>.</summary>
		public Float16 Y;
			/// <summary>The third axis of the <see cref="Vector4h"/>.</summary>
		public Float16 Z;
			/// <summary>The fourth axis of the <see cref="Vector4h"/>.</summary>
		public Float16 W;
	
	#endregion Fields

	#region Properties

	public static readonly Vector4h Zero = new Vector4h((Float16)0, (Float16)0, (Float16)0, (Float16)0);

	#endregion Properties

	#region Constructors

	/// <summary>Initialise a <see cref="Vector4h"/> from the provided values for each factor.</summary>
	public Vector4h(Float16 x, Float16 y, Float16 z, Float16 w)
	{
					X = x;
					Y = y;
					Z = z;
					W = w;
			}

	/// <summary>Initialise a <see cref="Vector4h"/> from a list.</summary>
	public Vector4h(IList<Float16> list, int index = 0) : this(list[index + 0], list[index + 1], list[index + 2], list[index + 3]) { }

	/// <summary>Initialise a <see cref="Vector4h"/> from a single scalar that is applied to all factors.</summary>
	public Vector4h(Float16 value) : this(value, value, value, value) { }

	#endregion Constructors

	#region Methods

	/// <summary>Get whether this <see cref="Vector4h"/> has equal factors as the other <see cref="Vector4h"/>.</summary>
	public bool Equals(Vector4h other) {
		return  X == other.X && Y == other.Y && Z == other.Z && W == other.W ;
	}

	/// <summary>If the other object is a <see cref="Vector4h"/> of the same type, get whether this <see cref="Vector4h"/> has equal factors as it; otherwise return false./summary>
	public override bool Equals(object other)
	{
		if(other is Vector4h)
			return Equals((Vector4h)other);
		return base.Equals(other);
	}

	/// <summary>Compute a hash code from combining the axes.</summary>
	public override int GetHashCode()
	{
		return  X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode() ^ W.GetHashCode() ;
	}

	/// <summary>Convert this <see cref="Vector4h"/> to a string of the form "X, Y, Z, W".</summary>
	public string ToCommaSeparatedString(string format = null, IFormatProvider provider = null) {
		return X.ToString(format, provider) + ", " + Y.ToString(format, provider) + ", " + Z.ToString(format, provider) + ", " + W.ToString(format, provider);
	}

	/// <summary>Convert this <see cref="Vector4h"/> to a string of the form "Vector4h(X, Y, Z, W)".</summary>
	public override string ToString()
	{
		return ToString(null, null);
	}

	/// <summary>Convert this <see cref="Vector4h"/> to a string of the form "Vector4h(X, Y, Z, W)".</summary>
	public string ToString(string format, IFormatProvider provider)
	{
		return "Vector4h(" + ToCommaSeparatedString(format, provider) + ")";
	}

	/// <summary>Convert this <see cref="Vector4h"/> to a string of the form "{X, Y, Z, W".</summary>
	public string ToShortString(string format = null, IFormatProvider provider = null) { return "{" + ToCommaSeparatedString(format, provider) + "}"; }	

	#endregion Methods
	
	public static bool operator ==(Vector4h a, Vector4h b) { return a.X == b.X&&a.Y == b.Y&&a.Z == b.Z&&a.W == b.W; }

	public static bool operator !=(Vector4h a, Vector4h b) { return a.X != b.X||a.Y != b.Y||a.Z != b.Z||a.W != b.W; }

		#region Operators
		

			// Casting
							public static implicit operator Vector4f(Vector4h a) { return new Vector4f((float)a.X, (float)a.Y, (float)a.Z, (float)a.W); }
					public static implicit operator Vector4d(Vector4h a) { return new Vector4d((double)a.X, (double)a.Y, (double)a.Z, (double)a.W); }
					public static explicit operator Vector4i(Vector4h a) { return new Vector4i((int)a.X, (int)a.Y, (int)a.Z, (int)a.W); }
					public static explicit operator Vector4ui(Vector4h a) { return new Vector4ui((uint)a.X, (uint)a.Y, (uint)a.Z, (uint)a.W); }
					public static explicit operator Vector4b(Vector4h a) { return new Vector4b((byte)a.X, (byte)a.Y, (byte)a.Z, (byte)a.W); }
					public static explicit operator Vector4nb(Vector4h a) { return new Vector4nb((NormalizedByte)a.X, (NormalizedByte)a.Y, (NormalizedByte)a.Z, (NormalizedByte)a.W); }
					public static explicit operator Vector4nsb(Vector4h a) { return new Vector4nsb((NormalizedSByte)a.X, (NormalizedSByte)a.Y, (NormalizedSByte)a.Z, (NormalizedSByte)a.W); }
					public static explicit operator Vector4rgba(Vector4h a) { return new Vector4rgba((NormalizedByte)a.X, (NormalizedByte)a.Y, (NormalizedByte)a.Z, (NormalizedByte)a.W); }
					
				// Explicit casting to vectors with a lower order, trimming off axes.
							
			public static Vector4h operator +(Vector4h a) { return new Vector4h((Float16)(+a.X), (Float16)(+a.Y), (Float16)(+a.Z), (Float16)(+a.W)); }
			public static Vector4h operator -(Vector4h a) { return new Vector4h((Float16)(-a.X), (Float16)(-a.Y), (Float16)(-a.Z), (Float16)(-a.W)); }
								public static Vector4h operator +(Vector4h a, Vector4h b) { return new Vector4h((Float16)(a.X + b.X), (Float16)(a.Y + b.Y), (Float16)(a.Z + b.Z), (Float16)(a.W + b.W)); }
					public static Vector4h operator +(Vector4h a, Float16 b) { return new Vector4h((Float16)(a.X + b), (Float16)(a.Y + b), (Float16)(a.Z + b), (Float16)(a.W + b)); }
					public static Vector4h operator +(Float16 a, Vector4h b) { return new Vector4h((Float16)(a + b.X), (Float16)(a + b.Y), (Float16)(a + b.Z), (Float16)(a + b.W)); }
									public static Vector4h operator -(Vector4h a, Vector4h b) { return new Vector4h((Float16)(a.X - b.X), (Float16)(a.Y - b.Y), (Float16)(a.Z - b.Z), (Float16)(a.W - b.W)); }
					public static Vector4h operator -(Vector4h a, Float16 b) { return new Vector4h((Float16)(a.X - b), (Float16)(a.Y - b), (Float16)(a.Z - b), (Float16)(a.W - b)); }
					public static Vector4h operator -(Float16 a, Vector4h b) { return new Vector4h((Float16)(a - b.X), (Float16)(a - b.Y), (Float16)(a - b.Z), (Float16)(a - b.W)); }
									public static Vector4h operator *(Vector4h a, Vector4h b) { return new Vector4h((Float16)(a.X * b.X), (Float16)(a.Y * b.Y), (Float16)(a.Z * b.Z), (Float16)(a.W * b.W)); }
					public static Vector4h operator *(Vector4h a, Float16 b) { return new Vector4h((Float16)(a.X * b), (Float16)(a.Y * b), (Float16)(a.Z * b), (Float16)(a.W * b)); }
					public static Vector4h operator *(Float16 a, Vector4h b) { return new Vector4h((Float16)(a * b.X), (Float16)(a * b.Y), (Float16)(a * b.Z), (Float16)(a * b.W)); }
									public static Vector4h operator /(Vector4h a, Vector4h b) { return new Vector4h((Float16)(a.X / b.X), (Float16)(a.Y / b.Y), (Float16)(a.Z / b.Z), (Float16)(a.W / b.W)); }
					public static Vector4h operator /(Vector4h a, Float16 b) { return new Vector4h((Float16)(a.X / b), (Float16)(a.Y / b), (Float16)(a.Z / b), (Float16)(a.W / b)); }
					public static Vector4h operator /(Float16 a, Vector4h b) { return new Vector4h((Float16)(a / b.X), (Float16)(a / b.Y), (Float16)(a / b.Z), (Float16)(a / b.W)); }
									public static Vector4h operator %(Vector4h a, Vector4h b) { return new Vector4h((Float16)(a.X % b.X), (Float16)(a.Y % b.Y), (Float16)(a.Z % b.Z), (Float16)(a.W % b.W)); }
					public static Vector4h operator %(Vector4h a, Float16 b) { return new Vector4h((Float16)(a.X % b), (Float16)(a.Y % b), (Float16)(a.Z % b), (Float16)(a.W % b)); }
					public static Vector4h operator %(Float16 a, Vector4h b) { return new Vector4h((Float16)(a % b.X), (Float16)(a % b.Y), (Float16)(a % b.Z), (Float16)(a % b.W)); }
				
		#endregion Operators
	}
	
	}







