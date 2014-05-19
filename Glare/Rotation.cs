using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare
{
			/// <summary>An angle between two lines or a rotation, depending upon usage.</summary>
		public struct Rotation4f : IEquatable<Rotation4f>, IFormattable {
				#region Fields

			/// <summary>The first axis of the <see cref="Rotation4f"/>.</summary>
		public Single X;
			/// <summary>The second axis of the <see cref="Rotation4f"/>.</summary>
		public Single Y;
			/// <summary>The third axis of the <see cref="Rotation4f"/>.</summary>
		public Single Z;
			/// <summary>The fourth axis of the <see cref="Rotation4f"/>.</summary>
		public Single W;
	
	#endregion Fields

	#region Properties

	public static readonly Rotation4f Zero = new Rotation4f((Single)0, (Single)0, (Single)0, (Single)0);

	#endregion Properties

	#region Constructors

	/// <summary>Initialise a <see cref="Rotation4f"/> from the provided values for each factor.</summary>
	public Rotation4f(Single x, Single y, Single z, Single w)
	{
					X = x;
					Y = y;
					Z = z;
					W = w;
			}

	/// <summary>Initialise a <see cref="Rotation4f"/> from a list.</summary>
	public Rotation4f(IList<Single> list, int index = 0) : this(list[index + 0], list[index + 1], list[index + 2], list[index + 3]) { }

	/// <summary>Initialise a <see cref="Rotation4f"/> from a single scalar that is applied to all factors.</summary>
	public Rotation4f(Single value) : this(value, value, value, value) { }

	#endregion Constructors

	#region Methods

	/// <summary>Get whether this <see cref="Rotation4f"/> has equal factors as the other <see cref="Rotation4f"/>.</summary>
	public bool Equals(Rotation4f other) {
		return  X == other.X && Y == other.Y && Z == other.Z && W == other.W ;
	}

	/// <summary>If the other object is a <see cref="Rotation4f"/> of the same type, get whether this <see cref="Rotation4f"/> has equal factors as it; otherwise return false./summary>
	public override bool Equals(object other)
	{
		if(other is Rotation4f)
			return Equals((Rotation4f)other);
		return base.Equals(other);
	}

	/// <summary>Compute a hash code from combining the axes.</summary>
	public override int GetHashCode()
	{
		return  X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode() ^ W.GetHashCode() ;
	}

	/// <summary>Convert this <see cref="Rotation4f"/> to a string of the form "X, Y, Z, W".</summary>
	public string ToCommaSeparatedString(string format = null, IFormatProvider provider = null) {
		return X.ToString(format, provider) + ", " + Y.ToString(format, provider) + ", " + Z.ToString(format, provider) + ", " + W.ToString(format, provider);
	}

	/// <summary>Convert this <see cref="Rotation4f"/> to a string of the form "Rotation4f(X, Y, Z, W)".</summary>
	public override string ToString()
	{
		return ToString(null, null);
	}

	/// <summary>Convert this <see cref="Rotation4f"/> to a string of the form "Rotation4f(X, Y, Z, W)".</summary>
	public string ToString(string format, IFormatProvider provider)
	{
		return "Rotation4f(" + ToCommaSeparatedString(format, provider) + ")";
	}

	/// <summary>Convert this <see cref="Rotation4f"/> to a string of the form "{X, Y, Z, W".</summary>
	public string ToShortString(string format = null, IFormatProvider provider = null) { return "{" + ToCommaSeparatedString(format, provider) + "}"; }	

	#endregion Methods
	
	public static bool operator ==(Rotation4f a, Rotation4f b) { return a.X == b.X&&a.Y == b.Y&&a.Z == b.Z&&a.W == b.W; }

	public static bool operator !=(Rotation4f a, Rotation4f b) { return a.X != b.X||a.Y != b.Y||a.Z != b.Z||a.W != b.W; }

			public static readonly Rotation4f Identity = new Rotation4f(0, 0, 0, 1); 

			public Rotation4f(Angle yaw, Angle pitch, Angle roll) {
				double hroll = roll.InRadians * 0.5f;
				double sroll = Math.Sin(hroll);
				double croll = Math.Cos(hroll);
				double hpitch = pitch.InRadians * 0.5f;
				double spitch = Math.Sin(hpitch);
				double cpitch = Math.Cos(hpitch);
				double hyaw = yaw.InRadians * 0.5f;
				double syaw = Math.Sin(hyaw);
				double cyaw = Math.Cos(hyaw);
				X = (Single)(((cyaw * spitch) * croll) + ((syaw * cpitch) * sroll));
				Y = (Single)(((syaw * cpitch) * croll) - ((cyaw * spitch) * sroll));
				Z = (Single)(((cyaw * cpitch) * sroll) - ((syaw * spitch) * croll));
				W = (Single)(((cyaw * cpitch) * croll) + ((syaw * spitch) * sroll));
			}

							public Rotation4f( Angle3 angle) : this(angle.X, angle.Y, angle.Z) { }
							public Rotation4f( ref  Angle3 angle) : this(angle.X, angle.Y, angle.Z) { }
			
								public Matrix4f ToMatrix4f() {
						Matrix4f result;
						double xx = X * X, xy = X * Y, xz = X * Z;
						double yy = Y * Y, yz = Y * Z;
						double zz = Z * Z;
						double wx = W * X, wy = W * Y, wz = W * Z;

						result.XX = (float)(1 - 2 * (yy + zz));
						result.XY = (float)(2 * (xy - wz));
						result.XZ = (float)(2 * (xz + wy));

						result.YX = (float)(2 * (xy + wz));
						result.YY = (float)(1 - 2 * (xx + zz));
						result.YZ = (float)(2 * (yz - wx));

						result.ZX = (float)(2 * (xz - wy));
						result.ZY = (float)(2 * (yz + wx));
						result.ZZ = (float)(1 - 2 * (xx + yy));

						result.WX = result.WY = result.WZ = 0;
						result.XW = result.YW = result.ZW = 0;
						result.WW = 1;
						return result;
					}

									public void ToMatrix4f(out Matrix4f result) {
						
						double xx = X * X, xy = X * Y, xz = X * Z;
						double yy = Y * Y, yz = Y * Z;
						double zz = Z * Z;
						double wx = W * X, wy = W * Y, wz = W * Z;

						result.XX = (float)(1 - 2 * (yy + zz));
						result.XY = (float)(2 * (xy - wz));
						result.XZ = (float)(2 * (xz + wy));

						result.YX = (float)(2 * (xy + wz));
						result.YY = (float)(1 - 2 * (xx + zz));
						result.YZ = (float)(2 * (yz - wx));

						result.ZX = (float)(2 * (xz - wy));
						result.ZY = (float)(2 * (yz + wx));
						result.ZZ = (float)(1 - 2 * (xx + yy));

						result.WX = result.WY = result.WZ = 0;
						result.XW = result.YW = result.ZW = 0;
						result.WW = 1;
						return;
					}

									public Matrix4d ToMatrix4d() {
						Matrix4d result;
						double xx = X * X, xy = X * Y, xz = X * Z;
						double yy = Y * Y, yz = Y * Z;
						double zz = Z * Z;
						double wx = W * X, wy = W * Y, wz = W * Z;

						result.XX = (double)(1 - 2 * (yy + zz));
						result.XY = (double)(2 * (xy - wz));
						result.XZ = (double)(2 * (xz + wy));

						result.YX = (double)(2 * (xy + wz));
						result.YY = (double)(1 - 2 * (xx + zz));
						result.YZ = (double)(2 * (yz - wx));

						result.ZX = (double)(2 * (xz - wy));
						result.ZY = (double)(2 * (yz + wx));
						result.ZZ = (double)(1 - 2 * (xx + yy));

						result.WX = result.WY = result.WZ = 0;
						result.XW = result.YW = result.ZW = 0;
						result.WW = 1;
						return result;
					}

									public void ToMatrix4d(out Matrix4d result) {
						
						double xx = X * X, xy = X * Y, xz = X * Z;
						double yy = Y * Y, yz = Y * Z;
						double zz = Z * Z;
						double wx = W * X, wy = W * Y, wz = W * Z;

						result.XX = (double)(1 - 2 * (yy + zz));
						result.XY = (double)(2 * (xy - wz));
						result.XZ = (double)(2 * (xz + wy));

						result.YX = (double)(2 * (xy + wz));
						result.YY = (double)(1 - 2 * (xx + zz));
						result.YZ = (double)(2 * (yz - wx));

						result.ZX = (double)(2 * (xz - wy));
						result.ZY = (double)(2 * (yz + wx));
						result.ZZ = (double)(1 - 2 * (xx + yy));

						result.WX = result.WY = result.WZ = 0;
						result.XW = result.YW = result.ZW = 0;
						result.WW = 1;
						return;
					}

						}
#if false
		//Funcion añadida Syderis
		public static Rotation4f Concatenate(Rotation4f value1, Rotation4f value2)
		{
			 Rotation4f Rotation4f;
		    float x = value2.X;
		    float y = value2.Y;
		    float z = value2.Z;
		    float w = value2.W;
		    float num4 = value1.X;
		    float num3 = value1.Y;
		    float num2 = value1.Z;
		    float num = value1.W;
		    float num12 = (y * num2) - (z * num3);
		    float num11 = (z * num4) - (x * num2);
		    float num10 = (x * num3) - (y * num4);
		    float num9 = ((x * num4) + (y * num3)) + (z * num2);
		    Rotation4f.X = ((x * num) + (num4 * w)) + num12;
		    Rotation4f.Y = ((y * num) + (num3 * w)) + num11;
		    Rotation4f.Z = ((z * num) + (num2 * w)) + num10;
		    Rotation4f.W = (w * num) - num9;
		    return Rotation4f;

		}
		
		//Añadida por Syderis
		public static void Concatenate(ref Rotation4f value1, ref Rotation4f value2, out Rotation4f result)
		{
		    float x = value2.X;
		    float y = value2.Y;
		    float z = value2.Z;
		    float w = value2.W;
		    float num4 = value1.X;
		    float num3 = value1.Y;
		    float num2 = value1.Z;
		    float num = value1.W;
		    float num12 = (y * num2) - (z * num3);
		    float num11 = (z * num4) - (x * num2);
		    float num10 = (x * num3) - (y * num4);
		    float num9 = ((x * num4) + (y * num3)) + (z * num2);
		    result.X = ((x * num) + (num4 * w)) + num12;
		    result.Y = ((y * num) + (num3 * w)) + num11;
		    result.Z = ((z * num) + (num2 * w)) + num10;
		    result.W = (w * num) - num9;
		}
		
		//Añadida por Syderis
		public void Conjugate()
		{
			this.X = -this.X;
			this.Y = -this.Y;
			this.Z = -this.Z;
		}
		
		//Añadida por Syderis
		public static Rotation4f Conjugate(Rotation4f value)
		{
			Rotation4f Rotation4f;
			Rotation4f.X = -value.X;
			Rotation4f.Y = -value.Y;
			Rotation4f.Z = -value.Z;
			Rotation4f.W = value.W;
			return Rotation4f;
		}
		
		//Añadida por Syderis
		public static void Conjugate(ref Rotation4f value, out Rotation4f result)
		{
			result.X = -value.X;
			result.Y = -value.Y;
			result.Z = -value.Z;
			result.W = value.W;
		}
 
        public static Rotation4f CreateFromAxisAngle(Vector3 axis, float angle)
        {
			
            Rotation4f Rotation4f;
		    float num2 = angle * 0.5f;
		    float num = (float) Math.Sin((double) num2);
		    float num3 = (float) Math.Cos((double) num2);
		    Rotation4f.X = axis.X * num;
		    Rotation4f.Y = axis.Y * num;
		    Rotation4f.Z = axis.Z * num;
		    Rotation4f.W = num3;
		    return Rotation4f;

        }


        public static void CreateFromAxisAngle(ref Vector3 axis, float angle, out Rotation4f result)
        {
            float num2 = angle * 0.5f;
		    float num = (float) Math.Sin((double) num2);
		    float num3 = (float) Math.Cos((double) num2);
		    result.X = axis.X * num;
		    result.Y = axis.Y * num;
		    result.Z = axis.Z * num;
		    result.W = num3;

        }


        public static Rotation4f CreateFromRotationMatrix(Matrix matrix)
        {
            float num8 = (matrix.M11 + matrix.M22) + matrix.M33;
		    Rotation4f Rotation4f = new Rotation4f();
		    if (num8 > 0f)
		    {
		        float num = (float) Math.Sqrt((double) (num8 + 1f));
		        Rotation4f.W = num * 0.5f;
		        num = 0.5f / num;
		        Rotation4f.X = (matrix.M23 - matrix.M32) * num;
		        Rotation4f.Y = (matrix.M31 - matrix.M13) * num;
		        Rotation4f.Z = (matrix.M12 - matrix.M21) * num;
		        return Rotation4f;
		    }
		    if ((matrix.M11 >= matrix.M22) && (matrix.M11 >= matrix.M33))
		    {
		        float num7 = (float) Math.Sqrt((double) (((1f + matrix.M11) - matrix.M22) - matrix.M33));
		        float num4 = 0.5f / num7;
		        Rotation4f.X = 0.5f * num7;
		        Rotation4f.Y = (matrix.M12 + matrix.M21) * num4;
		        Rotation4f.Z = (matrix.M13 + matrix.M31) * num4;
		        Rotation4f.W = (matrix.M23 - matrix.M32) * num4;
		        return Rotation4f;
		    }
		    if (matrix.M22 > matrix.M33)
		    {
		        float num6 = (float) Math.Sqrt((double) (((1f + matrix.M22) - matrix.M11) - matrix.M33));
		        float num3 = 0.5f / num6;
		        Rotation4f.X = (matrix.M21 + matrix.M12) * num3;
		        Rotation4f.Y = 0.5f * num6;
		        Rotation4f.Z = (matrix.M32 + matrix.M23) * num3;
		        Rotation4f.W = (matrix.M31 - matrix.M13) * num3;
		        return Rotation4f;
		    }
		    float num5 = (float) Math.Sqrt((double) (((1f + matrix.M33) - matrix.M11) - matrix.M22));
		    float num2 = 0.5f / num5;
		    Rotation4f.X = (matrix.M31 + matrix.M13) * num2;
		    Rotation4f.Y = (matrix.M32 + matrix.M23) * num2;
		    Rotation4f.Z = 0.5f * num5;
		    Rotation4f.W = (matrix.M12 - matrix.M21) * num2;
			
		    return Rotation4f;

        }


        public static void CreateFromRotationMatrix(ref Matrix matrix, out Rotation4f result)
        {
            float num8 = (matrix.M11 + matrix.M22) + matrix.M33;
		    if (num8 > 0f)
		    {
		        float num = (float) Math.Sqrt((double) (num8 + 1f));
		        result.W = num * 0.5f;
		        num = 0.5f / num;
		        result.X = (matrix.M23 - matrix.M32) * num;
		        result.Y = (matrix.M31 - matrix.M13) * num;
		        result.Z = (matrix.M12 - matrix.M21) * num;
		    }
		    else if ((matrix.M11 >= matrix.M22) && (matrix.M11 >= matrix.M33))
		    {
		        float num7 = (float) Math.Sqrt((double) (((1f + matrix.M11) - matrix.M22) - matrix.M33));
		        float num4 = 0.5f / num7;
		        result.X = 0.5f * num7;
		        result.Y = (matrix.M12 + matrix.M21) * num4;
		        result.Z = (matrix.M13 + matrix.M31) * num4;
		        result.W = (matrix.M23 - matrix.M32) * num4;
		    }
		    else if (matrix.M22 > matrix.M33)
		    {
		        float num6 = (float) Math.Sqrt((double) (((1f + matrix.M22) - matrix.M11) - matrix.M33));
		        float num3 = 0.5f / num6;
		        result.X = (matrix.M21 + matrix.M12) * num3;
		        result.Y = 0.5f * num6;
		        result.Z = (matrix.M32 + matrix.M23) * num3;
		        result.W = (matrix.M31 - matrix.M13) * num3;
		    }
		    else
		    {
		        float num5 = (float) Math.Sqrt((double) (((1f + matrix.M33) - matrix.M11) - matrix.M22));
		        float num2 = 0.5f / num5;
		        result.X = (matrix.M31 + matrix.M13) * num2;
		        result.Y = (matrix.M32 + matrix.M23) * num2;
		        result.Z = 0.5f * num5;
		        result.W = (matrix.M12 - matrix.M21) * num2;
		    }

        }

 		public static void CreateFromYawPitchRoll(float yaw, float pitch, float roll, out Rotation4f result)
		{
		    float num9 = roll * 0.5f;
		    float num6 = (float) Math.Sin((double) num9);
		    float num5 = (float) Math.Cos((double) num9);
		    float num8 = pitch * 0.5f;
		    float num4 = (float) Math.Sin((double) num8);
		    float num3 = (float) Math.Cos((double) num8);
		    float num7 = yaw * 0.5f;
		    float num2 = (float) Math.Sin((double) num7);
		    float num = (float) Math.Cos((double) num7);
		    result.X = ((num * num4) * num5) + ((num2 * num3) * num6);
		    result.Y = ((num2 * num3) * num5) - ((num * num4) * num6);
		    result.Z = ((num * num3) * num6) - ((num2 * num4) * num5);
		    result.W = ((num * num3) * num5) + ((num2 * num4) * num6);
		}

        public static Rotation4f Divide(Rotation4f Rotation4f1, Rotation4f Rotation4f2)
        {
            Rotation4f Rotation4f;
		    float x = Rotation4f1.X;
		    float y = Rotation4f1.Y;
		    float z = Rotation4f1.Z;
		    float w = Rotation4f1.W;
		    float num14 = (((Rotation4f2.X * Rotation4f2.X) + (Rotation4f2.Y * Rotation4f2.Y)) + (Rotation4f2.Z * Rotation4f2.Z)) + (Rotation4f2.W * Rotation4f2.W);
		    float num5 = 1f / num14;
		    float num4 = -Rotation4f2.X * num5;
		    float num3 = -Rotation4f2.Y * num5;
		    float num2 = -Rotation4f2.Z * num5;
		    float num = Rotation4f2.W * num5;
		    float num13 = (y * num2) - (z * num3);
		    float num12 = (z * num4) - (x * num2);
		    float num11 = (x * num3) - (y * num4);
		    float num10 = ((x * num4) + (y * num3)) + (z * num2);
		    Rotation4f.X = ((x * num) + (num4 * w)) + num13;
		    Rotation4f.Y = ((y * num) + (num3 * w)) + num12;
		    Rotation4f.Z = ((z * num) + (num2 * w)) + num11;
		    Rotation4f.W = (w * num) - num10;
		    return Rotation4f;

        }

        public static void Divide(ref Rotation4f Rotation4f1, ref Rotation4f Rotation4f2, out Rotation4f result)
        {
            float x = Rotation4f1.X;
		    float y = Rotation4f1.Y;
		    float z = Rotation4f1.Z;
		    float w = Rotation4f1.W;
		    float num14 = (((Rotation4f2.X * Rotation4f2.X) + (Rotation4f2.Y * Rotation4f2.Y)) + (Rotation4f2.Z * Rotation4f2.Z)) + (Rotation4f2.W * Rotation4f2.W);
		    float num5 = 1f / num14;
		    float num4 = -Rotation4f2.X * num5;
		    float num3 = -Rotation4f2.Y * num5;
		    float num2 = -Rotation4f2.Z * num5;
		    float num = Rotation4f2.W * num5;
		    float num13 = (y * num2) - (z * num3);
		    float num12 = (z * num4) - (x * num2);
		    float num11 = (x * num3) - (y * num4);
		    float num10 = ((x * num4) + (y * num3)) + (z * num2);
		    result.X = ((x * num) + (num4 * w)) + num13;
		    result.Y = ((y * num) + (num3 * w)) + num12;
		    result.Z = ((z * num) + (num2 * w)) + num11;
		    result.W = (w * num) - num10;

        }


        public static float Dot(Rotation4f Rotation4f1, Rotation4f Rotation4f2)
        {
            return ((((Rotation4f1.X * Rotation4f2.X) + (Rotation4f1.Y * Rotation4f2.Y)) + (Rotation4f1.Z * Rotation4f2.Z)) + (Rotation4f1.W * Rotation4f2.W));
        }


        public static void Dot(ref Rotation4f Rotation4f1, ref Rotation4f Rotation4f2, out float result)
        {
            result = (((Rotation4f1.X * Rotation4f2.X) + (Rotation4f1.Y * Rotation4f2.Y)) + (Rotation4f1.Z * Rotation4f2.Z)) + (Rotation4f1.W * Rotation4f2.W);
        }


        public override bool Equals(object obj)
        {
             bool flag = false;
		    if (obj is Rotation4f)
		    {
		        flag = this.Equals((Rotation4f) obj);
		    }
		    return flag;
        }


        public bool Equals(Rotation4f other)
        {
			return ((((this.X == other.X) && (this.Y == other.Y)) && (this.Z == other.Z)) && (this.W == other.W));
        }


        public override int GetHashCode()
        {
            return (((this.X.GetHashCode() + this.Y.GetHashCode()) + this.Z.GetHashCode()) + this.W.GetHashCode());
        }


        public static Rotation4f Inverse(Rotation4f Rotation4f)
        {
            Rotation4f Rotation4f2;
		    float num2 = (((Rotation4f.X * Rotation4f.X) + (Rotation4f.Y * Rotation4f.Y)) + (Rotation4f.Z * Rotation4f.Z)) + (Rotation4f.W * Rotation4f.W);
		    float num = 1f / num2;
		    Rotation4f2.X = -Rotation4f.X * num;
		    Rotation4f2.Y = -Rotation4f.Y * num;
		    Rotation4f2.Z = -Rotation4f.Z * num;
		    Rotation4f2.W = Rotation4f.W * num;
		    return Rotation4f2;

        }

        public static void Inverse(ref Rotation4f Rotation4f, out Rotation4f result)
        {
            float num2 = (((Rotation4f.X * Rotation4f.X) + (Rotation4f.Y * Rotation4f.Y)) + (Rotation4f.Z * Rotation4f.Z)) + (Rotation4f.W * Rotation4f.W);
		    float num = 1f / num2;
		    result.X = -Rotation4f.X * num;
		    result.Y = -Rotation4f.Y * num;
		    result.Z = -Rotation4f.Z * num;
		    result.W = Rotation4f.W * num;
        }

        public float Length()
        {
            float num = (((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z)) + (this.W * this.W);
    		return (float) Math.Sqrt((double) num);
        }


        public float LengthSquared()
        {
            return ((((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z)) + (this.W * this.W));
        }


        public static Rotation4f Lerp(Rotation4f Rotation4f1, Rotation4f Rotation4f2, float amount)
        {
            float num = amount;
		    float num2 = 1f - num;
		    Rotation4f Rotation4f = new Rotation4f();
		    float num5 = (((Rotation4f1.X * Rotation4f2.X) + (Rotation4f1.Y * Rotation4f2.Y)) + (Rotation4f1.Z * Rotation4f2.Z)) + (Rotation4f1.W * Rotation4f2.W);
		    if (num5 >= 0f)
		    {
		        Rotation4f.X = (num2 * Rotation4f1.X) + (num * Rotation4f2.X);
		        Rotation4f.Y = (num2 * Rotation4f1.Y) + (num * Rotation4f2.Y);
		        Rotation4f.Z = (num2 * Rotation4f1.Z) + (num * Rotation4f2.Z);
		        Rotation4f.W = (num2 * Rotation4f1.W) + (num * Rotation4f2.W);
		    }
		    else
		    {
		        Rotation4f.X = (num2 * Rotation4f1.X) - (num * Rotation4f2.X);
		        Rotation4f.Y = (num2 * Rotation4f1.Y) - (num * Rotation4f2.Y);
		        Rotation4f.Z = (num2 * Rotation4f1.Z) - (num * Rotation4f2.Z);
		        Rotation4f.W = (num2 * Rotation4f1.W) - (num * Rotation4f2.W);
		    }
		    float num4 = (((Rotation4f.X * Rotation4f.X) + (Rotation4f.Y * Rotation4f.Y)) + (Rotation4f.Z * Rotation4f.Z)) + (Rotation4f.W * Rotation4f.W);
		    float num3 = 1f / ((float) Math.Sqrt((double) num4));
		    Rotation4f.X *= num3;
		    Rotation4f.Y *= num3;
		    Rotation4f.Z *= num3;
		    Rotation4f.W *= num3;
		    return Rotation4f;
        }


        public static void Lerp(ref Rotation4f Rotation4f1, ref Rotation4f Rotation4f2, float amount, out Rotation4f result)
        {
            float num = amount;
		    float num2 = 1f - num;
		    float num5 = (((Rotation4f1.X * Rotation4f2.X) + (Rotation4f1.Y * Rotation4f2.Y)) + (Rotation4f1.Z * Rotation4f2.Z)) + (Rotation4f1.W * Rotation4f2.W);
		    if (num5 >= 0f)
		    {
		        result.X = (num2 * Rotation4f1.X) + (num * Rotation4f2.X);
		        result.Y = (num2 * Rotation4f1.Y) + (num * Rotation4f2.Y);
		        result.Z = (num2 * Rotation4f1.Z) + (num * Rotation4f2.Z);
		        result.W = (num2 * Rotation4f1.W) + (num * Rotation4f2.W);
		    }
		    else
		    {
		        result.X = (num2 * Rotation4f1.X) - (num * Rotation4f2.X);
		        result.Y = (num2 * Rotation4f1.Y) - (num * Rotation4f2.Y);
		        result.Z = (num2 * Rotation4f1.Z) - (num * Rotation4f2.Z);
		        result.W = (num2 * Rotation4f1.W) - (num * Rotation4f2.W);
		    }
		    float num4 = (((result.X * result.X) + (result.Y * result.Y)) + (result.Z * result.Z)) + (result.W * result.W);
		    float num3 = 1f / ((float) Math.Sqrt((double) num4));
		    result.X *= num3;
		    result.Y *= num3;
		    result.Z *= num3;
		    result.W *= num3;

        }


        public static Rotation4f Slerp(Rotation4f Rotation4f1, Rotation4f Rotation4f2, float amount)
        {
            float num2;
		    float num3;
		    Rotation4f Rotation4f;
		    float num = amount;
		    float num4 = (((Rotation4f1.X * Rotation4f2.X) + (Rotation4f1.Y * Rotation4f2.Y)) + (Rotation4f1.Z * Rotation4f2.Z)) + (Rotation4f1.W * Rotation4f2.W);
		    bool flag = false;
		    if (num4 < 0f)
		    {
		        flag = true;
		        num4 = -num4;
		    }
		    if (num4 > 0.999999f)
		    {
		        num3 = 1f - num;
		        num2 = flag ? -num : num;
		    }
		    else
		    {
		        float num5 = (float) Math.Acos((double) num4);
		        float num6 = (float) (1.0 / Math.Sin((double) num5));
		        num3 = ((float) Math.Sin((double) ((1f - num) * num5))) * num6;
		        num2 = flag ? (((float) -Math.Sin((double) (num * num5))) * num6) : (((float) Math.Sin((double) (num * num5))) * num6);
		    }
		    Rotation4f.X = (num3 * Rotation4f1.X) + (num2 * Rotation4f2.X);
		    Rotation4f.Y = (num3 * Rotation4f1.Y) + (num2 * Rotation4f2.Y);
		    Rotation4f.Z = (num3 * Rotation4f1.Z) + (num2 * Rotation4f2.Z);
		    Rotation4f.W = (num3 * Rotation4f1.W) + (num2 * Rotation4f2.W);
		    return Rotation4f;
        }


        public static void Slerp(ref Rotation4f Rotation4f1, ref Rotation4f Rotation4f2, float amount, out Rotation4f result)
        {
            float num2;
		    float num3;
		    float num = amount;
		    float num4 = (((Rotation4f1.X * Rotation4f2.X) + (Rotation4f1.Y * Rotation4f2.Y)) + (Rotation4f1.Z * Rotation4f2.Z)) + (Rotation4f1.W * Rotation4f2.W);
		    bool flag = false;
		    if (num4 < 0f)
		    {
		        flag = true;
		        num4 = -num4;
		    }
		    if (num4 > 0.999999f)
		    {
		        num3 = 1f - num;
		        num2 = flag ? -num : num;
		    }
		    else
		    {
		        float num5 = (float) Math.Acos((double) num4);
		        float num6 = (float) (1.0 / Math.Sin((double) num5));
		        num3 = ((float) Math.Sin((double) ((1f - num) * num5))) * num6;
		        num2 = flag ? (((float) -Math.Sin((double) (num * num5))) * num6) : (((float) Math.Sin((double) (num * num5))) * num6);
		    }
		    result.X = (num3 * Rotation4f1.X) + (num2 * Rotation4f2.X);
		    result.Y = (num3 * Rotation4f1.Y) + (num2 * Rotation4f2.Y);
		    result.Z = (num3 * Rotation4f1.Z) + (num2 * Rotation4f2.Z);
		    result.W = (num3 * Rotation4f1.W) + (num2 * Rotation4f2.W);
        }


        public static Rotation4f Subtract(Rotation4f Rotation4f1, Rotation4f Rotation4f2)
        {
            Rotation4f Rotation4f;
		    Rotation4f.X = Rotation4f1.X - Rotation4f2.X;
		    Rotation4f.Y = Rotation4f1.Y - Rotation4f2.Y;
		    Rotation4f.Z = Rotation4f1.Z - Rotation4f2.Z;
		    Rotation4f.W = Rotation4f1.W - Rotation4f2.W;
		    return Rotation4f;
        }


        public static void Subtract(ref Rotation4f Rotation4f1, ref Rotation4f Rotation4f2, out Rotation4f result)
        {
            result.X = Rotation4f1.X - Rotation4f2.X;
		    result.Y = Rotation4f1.Y - Rotation4f2.Y;
		    result.Z = Rotation4f1.Z - Rotation4f2.Z;
		    result.W = Rotation4f1.W - Rotation4f2.W;
        }


        public static Rotation4f Multiply(Rotation4f Rotation4f1, Rotation4f Rotation4f2)
        {
            Rotation4f Rotation4f;
		    float x = Rotation4f1.X;
		    float y = Rotation4f1.Y;
		    float z = Rotation4f1.Z;
		    float w = Rotation4f1.W;
		    float num4 = Rotation4f2.X;
		    float num3 = Rotation4f2.Y;
		    float num2 = Rotation4f2.Z;
		    float num = Rotation4f2.W;
		    float num12 = (y * num2) - (z * num3);
		    float num11 = (z * num4) - (x * num2);
		    float num10 = (x * num3) - (y * num4);
		    float num9 = ((x * num4) + (y * num3)) + (z * num2);
		    Rotation4f.X = ((x * num) + (num4 * w)) + num12;
		    Rotation4f.Y = ((y * num) + (num3 * w)) + num11;
		    Rotation4f.Z = ((z * num) + (num2 * w)) + num10;
		    Rotation4f.W = (w * num) - num9;
		    return Rotation4f;
        }


        public static Rotation4f Multiply(Rotation4f Rotation4f1, float scaleFactor)
        {
            Rotation4f Rotation4f;
		    Rotation4f.X = Rotation4f1.X * scaleFactor;
		    Rotation4f.Y = Rotation4f1.Y * scaleFactor;
		    Rotation4f.Z = Rotation4f1.Z * scaleFactor;
		    Rotation4f.W = Rotation4f1.W * scaleFactor;
		    return Rotation4f;
        }


        public static void Multiply(ref Rotation4f Rotation4f1, float scaleFactor, out Rotation4f result)
        {
            result.X = Rotation4f1.X * scaleFactor;
		    result.Y = Rotation4f1.Y * scaleFactor;
		    result.Z = Rotation4f1.Z * scaleFactor;
		    result.W = Rotation4f1.W * scaleFactor;
        }


        public static void Multiply(ref Rotation4f Rotation4f1, ref Rotation4f Rotation4f2, out Rotation4f result)
        {
            float x = Rotation4f1.X;
		    float y = Rotation4f1.Y;
		    float z = Rotation4f1.Z;
		    float w = Rotation4f1.W;
		    float num4 = Rotation4f2.X;
		    float num3 = Rotation4f2.Y;
		    float num2 = Rotation4f2.Z;
		    float num = Rotation4f2.W;
		    float num12 = (y * num2) - (z * num3);
		    float num11 = (z * num4) - (x * num2);
		    float num10 = (x * num3) - (y * num4);
		    float num9 = ((x * num4) + (y * num3)) + (z * num2);
		    result.X = ((x * num) + (num4 * w)) + num12;
		    result.Y = ((y * num) + (num3 * w)) + num11;
		    result.Z = ((z * num) + (num2 * w)) + num10;
		    result.W = (w * num) - num9;
        }


        public static Rotation4f Negate(Rotation4f Rotation4f)
        {
            Rotation4f Rotation4f2;
		    Rotation4f2.X = -Rotation4f.X;
		    Rotation4f2.Y = -Rotation4f.Y;
		    Rotation4f2.Z = -Rotation4f.Z;
		    Rotation4f2.W = -Rotation4f.W;
		    return Rotation4f2;
        }


        public static void Negate(ref Rotation4f Rotation4f, out Rotation4f result)
        {
            result.X = -Rotation4f.X;
		    result.Y = -Rotation4f.Y;
		    result.Z = -Rotation4f.Z;
		    result.W = -Rotation4f.W;
        }


        public void Normalize()
        {
            float num2 = (((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z)) + (this.W * this.W);
		    float num = 1f / ((float) Math.Sqrt((double) num2));
		    this.X *= num;
		    this.Y *= num;
		    this.Z *= num;
		    this.W *= num;
        }


        public static Rotation4f Normalize(Rotation4f Rotation4f)
        {
            Rotation4f Rotation4f2;
		    float num2 = (((Rotation4f.X * Rotation4f.X) + (Rotation4f.Y * Rotation4f.Y)) + (Rotation4f.Z * Rotation4f.Z)) + (Rotation4f.W * Rotation4f.W);
		    float num = 1f / ((float) Math.Sqrt((double) num2));
		    Rotation4f2.X = Rotation4f.X * num;
		    Rotation4f2.Y = Rotation4f.Y * num;
		    Rotation4f2.Z = Rotation4f.Z * num;
		    Rotation4f2.W = Rotation4f.W * num;
		    return Rotation4f2;
        }


        public static void Normalize(ref Rotation4f Rotation4f, out Rotation4f result)
        {
            float num2 = (((Rotation4f.X * Rotation4f.X) + (Rotation4f.Y * Rotation4f.Y)) + (Rotation4f.Z * Rotation4f.Z)) + (Rotation4f.W * Rotation4f.W);
		    float num = 1f / ((float) Math.Sqrt((double) num2));
		    result.X = Rotation4f.X * num;
		    result.Y = Rotation4f.Y * num;
		    result.Z = Rotation4f.Z * num;
		    result.W = Rotation4f.W * num;
        }


        public static Rotation4f operator +(Rotation4f Rotation4f1, Rotation4f Rotation4f2)
        {
            Rotation4f Rotation4f;
		    Rotation4f.X = Rotation4f1.X + Rotation4f2.X;
		    Rotation4f.Y = Rotation4f1.Y + Rotation4f2.Y;
		    Rotation4f.Z = Rotation4f1.Z + Rotation4f2.Z;
		    Rotation4f.W = Rotation4f1.W + Rotation4f2.W;
		    return Rotation4f;
        }


        public static Rotation4f operator /(Rotation4f Rotation4f1, Rotation4f Rotation4f2)
        {
            Rotation4f Rotation4f;
		    float x = Rotation4f1.X;
		    float y = Rotation4f1.Y;
		    float z = Rotation4f1.Z;
		    float w = Rotation4f1.W;
		    float num14 = (((Rotation4f2.X * Rotation4f2.X) + (Rotation4f2.Y * Rotation4f2.Y)) + (Rotation4f2.Z * Rotation4f2.Z)) + (Rotation4f2.W * Rotation4f2.W);
		    float num5 = 1f / num14;
		    float num4 = -Rotation4f2.X * num5;
		    float num3 = -Rotation4f2.Y * num5;
		    float num2 = -Rotation4f2.Z * num5;
		    float num = Rotation4f2.W * num5;
		    float num13 = (y * num2) - (z * num3);
		    float num12 = (z * num4) - (x * num2);
		    float num11 = (x * num3) - (y * num4);
		    float num10 = ((x * num4) + (y * num3)) + (z * num2);
		    Rotation4f.X = ((x * num) + (num4 * w)) + num13;
		    Rotation4f.Y = ((y * num) + (num3 * w)) + num12;
		    Rotation4f.Z = ((z * num) + (num2 * w)) + num11;
		    Rotation4f.W = (w * num) - num10;
		    return Rotation4f;
        }


        public static bool operator ==(Rotation4f Rotation4f1, Rotation4f Rotation4f2)
        {
            return ((((Rotation4f1.X == Rotation4f2.X) && (Rotation4f1.Y == Rotation4f2.Y)) && (Rotation4f1.Z == Rotation4f2.Z)) && (Rotation4f1.W == Rotation4f2.W));
        }


        public static bool operator !=(Rotation4f Rotation4f1, Rotation4f Rotation4f2)
        {
            if (((Rotation4f1.X == Rotation4f2.X) && (Rotation4f1.Y == Rotation4f2.Y)) && (Rotation4f1.Z == Rotation4f2.Z))
		    {
		        return (Rotation4f1.W != Rotation4f2.W);
		    }
		    return true;
        }


        public static Rotation4f operator *(Rotation4f Rotation4f1, Rotation4f Rotation4f2)
        {
            Rotation4f Rotation4f;
		    float x = Rotation4f1.X;
		    float y = Rotation4f1.Y;
		    float z = Rotation4f1.Z;
		    float w = Rotation4f1.W;
		    float num4 = Rotation4f2.X;
		    float num3 = Rotation4f2.Y;
		    float num2 = Rotation4f2.Z;
		    float num = Rotation4f2.W;
		    float num12 = (y * num2) - (z * num3);
		    float num11 = (z * num4) - (x * num2);
		    float num10 = (x * num3) - (y * num4);
		    float num9 = ((x * num4) + (y * num3)) + (z * num2);
		    Rotation4f.X = ((x * num) + (num4 * w)) + num12;
		    Rotation4f.Y = ((y * num) + (num3 * w)) + num11;
		    Rotation4f.Z = ((z * num) + (num2 * w)) + num10;
		    Rotation4f.W = (w * num) - num9;
		    return Rotation4f;
        }


        public static Rotation4f operator *(Rotation4f Rotation4f1, float scaleFactor)
        {
            Rotation4f Rotation4f;
		    Rotation4f.X = Rotation4f1.X * scaleFactor;
		    Rotation4f.Y = Rotation4f1.Y * scaleFactor;
		    Rotation4f.Z = Rotation4f1.Z * scaleFactor;
		    Rotation4f.W = Rotation4f1.W * scaleFactor;
		    return Rotation4f;
        }


        public static Rotation4f operator -(Rotation4f Rotation4f1, Rotation4f Rotation4f2)
        {
            Rotation4f Rotation4f;
		    Rotation4f.X = Rotation4f1.X - Rotation4f2.X;
		    Rotation4f.Y = Rotation4f1.Y - Rotation4f2.Y;
		    Rotation4f.Z = Rotation4f1.Z - Rotation4f2.Z;
		    Rotation4f.W = Rotation4f1.W - Rotation4f2.W;
		    return Rotation4f;

        }


        public static Rotation4f operator -(Rotation4f Rotation4f)
        {
            Rotation4f Rotation4f2;
		    Rotation4f2.X = -Rotation4f.X;
		    Rotation4f2.Y = -Rotation4f.Y;
		    Rotation4f2.Z = -Rotation4f.Z;
		    Rotation4f2.W = -Rotation4f.W;
		    return Rotation4f2;
        }


        public override string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(32);
            sb.Append("{X:");
            sb.Append(this.X);
            sb.Append(" Y:");
            sb.Append(this.Y);
            sb.Append(" Z:");
            sb.Append(this.Z);
            sb.Append(" W:");
            sb.Append(this.W);
            sb.Append("}");
            return sb.ToString();
        }

		internal Matrix ToMatrix ()
		{
			Matrix matrix = Matrix.Identity;
			ToMatrix(out matrix);
			return matrix;
		}

		internal void ToMatrix (out Matrix matrix)
		{
			Rotation4f.ToMatrix(this, out matrix);
		}

		internal static void ToMatrix(Rotation4f Rotation4f, out Matrix matrix)
		{

			// source -> http://content.gpwiki.org/index.php/OpenGL:Tutorials:Using_Rotation4fs_to_represent_rotation#Rotation4f_to_Matrix
			float x2 = Rotation4f.X * Rotation4f.X;
			float y2 = Rotation4f.Y * Rotation4f.Y;
			float z2 = Rotation4f.Z * Rotation4f.Z;
			float xy = Rotation4f.X * Rotation4f.Y;
			float xz = Rotation4f.X * Rotation4f.Z;
			float yz = Rotation4f.Y * Rotation4f.Z;
			float wx = Rotation4f.W * Rotation4f.X;
			float wy = Rotation4f.W * Rotation4f.Y;
			float wz = Rotation4f.W * Rotation4f.Z;

			// This calculation would be a lot more complicated for non-unit length Rotation4fs
			// Note: The constructor of Matrix4 expects the Matrix in column-major format like expected by
			//   OpenGL
			matrix.M11 = 1.0f - 2.0f * (y2 + z2);
			matrix.M12 = 2.0f * (xy - wz);
			matrix.M13 = 2.0f * (xz + wy);
			matrix.M14 = 0.0f;

			matrix.M21 = 2.0f * (xy + wz);
			matrix.M22 = 1.0f - 2.0f * (x2 + z2);
			matrix.M23 = 2.0f * (yz - wx);
			matrix.M24 = 0.0f;

			matrix.M31 = 2.0f * (xz - wy);
			matrix.M32 = 2.0f * (yz + wx);
			matrix.M33 = 1.0f - 2.0f * (x2 + y2);
			matrix.M34 = 0.0f;

			matrix.M41 = 2.0f * (xz - wy);
			matrix.M42 = 2.0f * (yz + wx);
			matrix.M43 = 1.0f - 2.0f * (x2 + y2);
			matrix.M44 = 0.0f;

			//return Matrix4( 1.0f - 2.0f * (y2 + z2), 2.0f * (xy - wz), 2.0f * (xz + wy), 0.0f,
			//		2.0f * (xy + wz), 1.0f - 2.0f * (x2 + z2), 2.0f * (yz - wx), 0.0f,
			//		2.0f * (xz - wy), 2.0f * (yz + wx), 1.0f - 2.0f * (x2 + y2), 0.0f,
			//		0.0f, 0.0f, 0.0f, 1.0f)
			//	}
		}

		internal Vector3 Xyz
		{
			get {
				return new Vector3(X, Y, Z);
			}

			set {
				X = value.X;
				Y = value.Y;
				Z = value.Z;
			}
		}


    }
}
		#endif
			/// <summary>An angle between two lines or a rotation, depending upon usage.</summary>
		public struct Rotation4d : IEquatable<Rotation4d>, IFormattable {
				#region Fields

			/// <summary>The first axis of the <see cref="Rotation4d"/>.</summary>
		public Double X;
			/// <summary>The second axis of the <see cref="Rotation4d"/>.</summary>
		public Double Y;
			/// <summary>The third axis of the <see cref="Rotation4d"/>.</summary>
		public Double Z;
			/// <summary>The fourth axis of the <see cref="Rotation4d"/>.</summary>
		public Double W;
	
	#endregion Fields

	#region Properties

	public static readonly Rotation4d Zero = new Rotation4d((Double)0, (Double)0, (Double)0, (Double)0);

	#endregion Properties

	#region Constructors

	/// <summary>Initialise a <see cref="Rotation4d"/> from the provided values for each factor.</summary>
	public Rotation4d(Double x, Double y, Double z, Double w)
	{
					X = x;
					Y = y;
					Z = z;
					W = w;
			}

	/// <summary>Initialise a <see cref="Rotation4d"/> from a list.</summary>
	public Rotation4d(IList<Double> list, int index = 0) : this(list[index + 0], list[index + 1], list[index + 2], list[index + 3]) { }

	/// <summary>Initialise a <see cref="Rotation4d"/> from a single scalar that is applied to all factors.</summary>
	public Rotation4d(Double value) : this(value, value, value, value) { }

	#endregion Constructors

	#region Methods

	/// <summary>Get whether this <see cref="Rotation4d"/> has equal factors as the other <see cref="Rotation4d"/>.</summary>
	public bool Equals(Rotation4d other) {
		return  X == other.X && Y == other.Y && Z == other.Z && W == other.W ;
	}

	/// <summary>If the other object is a <see cref="Rotation4d"/> of the same type, get whether this <see cref="Rotation4d"/> has equal factors as it; otherwise return false./summary>
	public override bool Equals(object other)
	{
		if(other is Rotation4d)
			return Equals((Rotation4d)other);
		return base.Equals(other);
	}

	/// <summary>Compute a hash code from combining the axes.</summary>
	public override int GetHashCode()
	{
		return  X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode() ^ W.GetHashCode() ;
	}

	/// <summary>Convert this <see cref="Rotation4d"/> to a string of the form "X, Y, Z, W".</summary>
	public string ToCommaSeparatedString(string format = null, IFormatProvider provider = null) {
		return X.ToString(format, provider) + ", " + Y.ToString(format, provider) + ", " + Z.ToString(format, provider) + ", " + W.ToString(format, provider);
	}

	/// <summary>Convert this <see cref="Rotation4d"/> to a string of the form "Rotation4d(X, Y, Z, W)".</summary>
	public override string ToString()
	{
		return ToString(null, null);
	}

	/// <summary>Convert this <see cref="Rotation4d"/> to a string of the form "Rotation4d(X, Y, Z, W)".</summary>
	public string ToString(string format, IFormatProvider provider)
	{
		return "Rotation4d(" + ToCommaSeparatedString(format, provider) + ")";
	}

	/// <summary>Convert this <see cref="Rotation4d"/> to a string of the form "{X, Y, Z, W".</summary>
	public string ToShortString(string format = null, IFormatProvider provider = null) { return "{" + ToCommaSeparatedString(format, provider) + "}"; }	

	#endregion Methods
	
	public static bool operator ==(Rotation4d a, Rotation4d b) { return a.X == b.X&&a.Y == b.Y&&a.Z == b.Z&&a.W == b.W; }

	public static bool operator !=(Rotation4d a, Rotation4d b) { return a.X != b.X||a.Y != b.Y||a.Z != b.Z||a.W != b.W; }

			public static readonly Rotation4d Identity = new Rotation4d(0, 0, 0, 1); 

			public Rotation4d(Angle yaw, Angle pitch, Angle roll) {
				double hroll = roll.InRadians * 0.5f;
				double sroll = Math.Sin(hroll);
				double croll = Math.Cos(hroll);
				double hpitch = pitch.InRadians * 0.5f;
				double spitch = Math.Sin(hpitch);
				double cpitch = Math.Cos(hpitch);
				double hyaw = yaw.InRadians * 0.5f;
				double syaw = Math.Sin(hyaw);
				double cyaw = Math.Cos(hyaw);
				X = (Double)(((cyaw * spitch) * croll) + ((syaw * cpitch) * sroll));
				Y = (Double)(((syaw * cpitch) * croll) - ((cyaw * spitch) * sroll));
				Z = (Double)(((cyaw * cpitch) * sroll) - ((syaw * spitch) * croll));
				W = (Double)(((cyaw * cpitch) * croll) + ((syaw * spitch) * sroll));
			}

							public Rotation4d( Angle3 angle) : this(angle.X, angle.Y, angle.Z) { }
							public Rotation4d( ref  Angle3 angle) : this(angle.X, angle.Y, angle.Z) { }
			
								public Matrix4f ToMatrix4f() {
						Matrix4f result;
						double xx = X * X, xy = X * Y, xz = X * Z;
						double yy = Y * Y, yz = Y * Z;
						double zz = Z * Z;
						double wx = W * X, wy = W * Y, wz = W * Z;

						result.XX = (float)(1 - 2 * (yy + zz));
						result.XY = (float)(2 * (xy - wz));
						result.XZ = (float)(2 * (xz + wy));

						result.YX = (float)(2 * (xy + wz));
						result.YY = (float)(1 - 2 * (xx + zz));
						result.YZ = (float)(2 * (yz - wx));

						result.ZX = (float)(2 * (xz - wy));
						result.ZY = (float)(2 * (yz + wx));
						result.ZZ = (float)(1 - 2 * (xx + yy));

						result.WX = result.WY = result.WZ = 0;
						result.XW = result.YW = result.ZW = 0;
						result.WW = 1;
						return result;
					}

									public void ToMatrix4f(out Matrix4f result) {
						
						double xx = X * X, xy = X * Y, xz = X * Z;
						double yy = Y * Y, yz = Y * Z;
						double zz = Z * Z;
						double wx = W * X, wy = W * Y, wz = W * Z;

						result.XX = (float)(1 - 2 * (yy + zz));
						result.XY = (float)(2 * (xy - wz));
						result.XZ = (float)(2 * (xz + wy));

						result.YX = (float)(2 * (xy + wz));
						result.YY = (float)(1 - 2 * (xx + zz));
						result.YZ = (float)(2 * (yz - wx));

						result.ZX = (float)(2 * (xz - wy));
						result.ZY = (float)(2 * (yz + wx));
						result.ZZ = (float)(1 - 2 * (xx + yy));

						result.WX = result.WY = result.WZ = 0;
						result.XW = result.YW = result.ZW = 0;
						result.WW = 1;
						return;
					}

									public Matrix4d ToMatrix4d() {
						Matrix4d result;
						double xx = X * X, xy = X * Y, xz = X * Z;
						double yy = Y * Y, yz = Y * Z;
						double zz = Z * Z;
						double wx = W * X, wy = W * Y, wz = W * Z;

						result.XX = (double)(1 - 2 * (yy + zz));
						result.XY = (double)(2 * (xy - wz));
						result.XZ = (double)(2 * (xz + wy));

						result.YX = (double)(2 * (xy + wz));
						result.YY = (double)(1 - 2 * (xx + zz));
						result.YZ = (double)(2 * (yz - wx));

						result.ZX = (double)(2 * (xz - wy));
						result.ZY = (double)(2 * (yz + wx));
						result.ZZ = (double)(1 - 2 * (xx + yy));

						result.WX = result.WY = result.WZ = 0;
						result.XW = result.YW = result.ZW = 0;
						result.WW = 1;
						return result;
					}

									public void ToMatrix4d(out Matrix4d result) {
						
						double xx = X * X, xy = X * Y, xz = X * Z;
						double yy = Y * Y, yz = Y * Z;
						double zz = Z * Z;
						double wx = W * X, wy = W * Y, wz = W * Z;

						result.XX = (double)(1 - 2 * (yy + zz));
						result.XY = (double)(2 * (xy - wz));
						result.XZ = (double)(2 * (xz + wy));

						result.YX = (double)(2 * (xy + wz));
						result.YY = (double)(1 - 2 * (xx + zz));
						result.YZ = (double)(2 * (yz - wx));

						result.ZX = (double)(2 * (xz - wy));
						result.ZY = (double)(2 * (yz + wx));
						result.ZZ = (double)(1 - 2 * (xx + yy));

						result.WX = result.WY = result.WZ = 0;
						result.XW = result.YW = result.ZW = 0;
						result.WW = 1;
						return;
					}

						}
#if false
		//Funcion añadida Syderis
		public static Rotation4d Concatenate(Rotation4d value1, Rotation4d value2)
		{
			 Rotation4d Rotation4d;
		    float x = value2.X;
		    float y = value2.Y;
		    float z = value2.Z;
		    float w = value2.W;
		    float num4 = value1.X;
		    float num3 = value1.Y;
		    float num2 = value1.Z;
		    float num = value1.W;
		    float num12 = (y * num2) - (z * num3);
		    float num11 = (z * num4) - (x * num2);
		    float num10 = (x * num3) - (y * num4);
		    float num9 = ((x * num4) + (y * num3)) + (z * num2);
		    Rotation4d.X = ((x * num) + (num4 * w)) + num12;
		    Rotation4d.Y = ((y * num) + (num3 * w)) + num11;
		    Rotation4d.Z = ((z * num) + (num2 * w)) + num10;
		    Rotation4d.W = (w * num) - num9;
		    return Rotation4d;

		}
		
		//Añadida por Syderis
		public static void Concatenate(ref Rotation4d value1, ref Rotation4d value2, out Rotation4d result)
		{
		    float x = value2.X;
		    float y = value2.Y;
		    float z = value2.Z;
		    float w = value2.W;
		    float num4 = value1.X;
		    float num3 = value1.Y;
		    float num2 = value1.Z;
		    float num = value1.W;
		    float num12 = (y * num2) - (z * num3);
		    float num11 = (z * num4) - (x * num2);
		    float num10 = (x * num3) - (y * num4);
		    float num9 = ((x * num4) + (y * num3)) + (z * num2);
		    result.X = ((x * num) + (num4 * w)) + num12;
		    result.Y = ((y * num) + (num3 * w)) + num11;
		    result.Z = ((z * num) + (num2 * w)) + num10;
		    result.W = (w * num) - num9;
		}
		
		//Añadida por Syderis
		public void Conjugate()
		{
			this.X = -this.X;
			this.Y = -this.Y;
			this.Z = -this.Z;
		}
		
		//Añadida por Syderis
		public static Rotation4d Conjugate(Rotation4d value)
		{
			Rotation4d Rotation4d;
			Rotation4d.X = -value.X;
			Rotation4d.Y = -value.Y;
			Rotation4d.Z = -value.Z;
			Rotation4d.W = value.W;
			return Rotation4d;
		}
		
		//Añadida por Syderis
		public static void Conjugate(ref Rotation4d value, out Rotation4d result)
		{
			result.X = -value.X;
			result.Y = -value.Y;
			result.Z = -value.Z;
			result.W = value.W;
		}
 
        public static Rotation4d CreateFromAxisAngle(Vector3 axis, float angle)
        {
			
            Rotation4d Rotation4d;
		    float num2 = angle * 0.5f;
		    float num = (float) Math.Sin((double) num2);
		    float num3 = (float) Math.Cos((double) num2);
		    Rotation4d.X = axis.X * num;
		    Rotation4d.Y = axis.Y * num;
		    Rotation4d.Z = axis.Z * num;
		    Rotation4d.W = num3;
		    return Rotation4d;

        }


        public static void CreateFromAxisAngle(ref Vector3 axis, float angle, out Rotation4d result)
        {
            float num2 = angle * 0.5f;
		    float num = (float) Math.Sin((double) num2);
		    float num3 = (float) Math.Cos((double) num2);
		    result.X = axis.X * num;
		    result.Y = axis.Y * num;
		    result.Z = axis.Z * num;
		    result.W = num3;

        }


        public static Rotation4d CreateFromRotationMatrix(Matrix matrix)
        {
            float num8 = (matrix.M11 + matrix.M22) + matrix.M33;
		    Rotation4d Rotation4d = new Rotation4d();
		    if (num8 > 0f)
		    {
		        float num = (float) Math.Sqrt((double) (num8 + 1f));
		        Rotation4d.W = num * 0.5f;
		        num = 0.5f / num;
		        Rotation4d.X = (matrix.M23 - matrix.M32) * num;
		        Rotation4d.Y = (matrix.M31 - matrix.M13) * num;
		        Rotation4d.Z = (matrix.M12 - matrix.M21) * num;
		        return Rotation4d;
		    }
		    if ((matrix.M11 >= matrix.M22) && (matrix.M11 >= matrix.M33))
		    {
		        float num7 = (float) Math.Sqrt((double) (((1f + matrix.M11) - matrix.M22) - matrix.M33));
		        float num4 = 0.5f / num7;
		        Rotation4d.X = 0.5f * num7;
		        Rotation4d.Y = (matrix.M12 + matrix.M21) * num4;
		        Rotation4d.Z = (matrix.M13 + matrix.M31) * num4;
		        Rotation4d.W = (matrix.M23 - matrix.M32) * num4;
		        return Rotation4d;
		    }
		    if (matrix.M22 > matrix.M33)
		    {
		        float num6 = (float) Math.Sqrt((double) (((1f + matrix.M22) - matrix.M11) - matrix.M33));
		        float num3 = 0.5f / num6;
		        Rotation4d.X = (matrix.M21 + matrix.M12) * num3;
		        Rotation4d.Y = 0.5f * num6;
		        Rotation4d.Z = (matrix.M32 + matrix.M23) * num3;
		        Rotation4d.W = (matrix.M31 - matrix.M13) * num3;
		        return Rotation4d;
		    }
		    float num5 = (float) Math.Sqrt((double) (((1f + matrix.M33) - matrix.M11) - matrix.M22));
		    float num2 = 0.5f / num5;
		    Rotation4d.X = (matrix.M31 + matrix.M13) * num2;
		    Rotation4d.Y = (matrix.M32 + matrix.M23) * num2;
		    Rotation4d.Z = 0.5f * num5;
		    Rotation4d.W = (matrix.M12 - matrix.M21) * num2;
			
		    return Rotation4d;

        }


        public static void CreateFromRotationMatrix(ref Matrix matrix, out Rotation4d result)
        {
            float num8 = (matrix.M11 + matrix.M22) + matrix.M33;
		    if (num8 > 0f)
		    {
		        float num = (float) Math.Sqrt((double) (num8 + 1f));
		        result.W = num * 0.5f;
		        num = 0.5f / num;
		        result.X = (matrix.M23 - matrix.M32) * num;
		        result.Y = (matrix.M31 - matrix.M13) * num;
		        result.Z = (matrix.M12 - matrix.M21) * num;
		    }
		    else if ((matrix.M11 >= matrix.M22) && (matrix.M11 >= matrix.M33))
		    {
		        float num7 = (float) Math.Sqrt((double) (((1f + matrix.M11) - matrix.M22) - matrix.M33));
		        float num4 = 0.5f / num7;
		        result.X = 0.5f * num7;
		        result.Y = (matrix.M12 + matrix.M21) * num4;
		        result.Z = (matrix.M13 + matrix.M31) * num4;
		        result.W = (matrix.M23 - matrix.M32) * num4;
		    }
		    else if (matrix.M22 > matrix.M33)
		    {
		        float num6 = (float) Math.Sqrt((double) (((1f + matrix.M22) - matrix.M11) - matrix.M33));
		        float num3 = 0.5f / num6;
		        result.X = (matrix.M21 + matrix.M12) * num3;
		        result.Y = 0.5f * num6;
		        result.Z = (matrix.M32 + matrix.M23) * num3;
		        result.W = (matrix.M31 - matrix.M13) * num3;
		    }
		    else
		    {
		        float num5 = (float) Math.Sqrt((double) (((1f + matrix.M33) - matrix.M11) - matrix.M22));
		        float num2 = 0.5f / num5;
		        result.X = (matrix.M31 + matrix.M13) * num2;
		        result.Y = (matrix.M32 + matrix.M23) * num2;
		        result.Z = 0.5f * num5;
		        result.W = (matrix.M12 - matrix.M21) * num2;
		    }

        }

 		public static void CreateFromYawPitchRoll(float yaw, float pitch, float roll, out Rotation4d result)
		{
		    float num9 = roll * 0.5f;
		    float num6 = (float) Math.Sin((double) num9);
		    float num5 = (float) Math.Cos((double) num9);
		    float num8 = pitch * 0.5f;
		    float num4 = (float) Math.Sin((double) num8);
		    float num3 = (float) Math.Cos((double) num8);
		    float num7 = yaw * 0.5f;
		    float num2 = (float) Math.Sin((double) num7);
		    float num = (float) Math.Cos((double) num7);
		    result.X = ((num * num4) * num5) + ((num2 * num3) * num6);
		    result.Y = ((num2 * num3) * num5) - ((num * num4) * num6);
		    result.Z = ((num * num3) * num6) - ((num2 * num4) * num5);
		    result.W = ((num * num3) * num5) + ((num2 * num4) * num6);
		}

        public static Rotation4d Divide(Rotation4d Rotation4d1, Rotation4d Rotation4d2)
        {
            Rotation4d Rotation4d;
		    float x = Rotation4d1.X;
		    float y = Rotation4d1.Y;
		    float z = Rotation4d1.Z;
		    float w = Rotation4d1.W;
		    float num14 = (((Rotation4d2.X * Rotation4d2.X) + (Rotation4d2.Y * Rotation4d2.Y)) + (Rotation4d2.Z * Rotation4d2.Z)) + (Rotation4d2.W * Rotation4d2.W);
		    float num5 = 1f / num14;
		    float num4 = -Rotation4d2.X * num5;
		    float num3 = -Rotation4d2.Y * num5;
		    float num2 = -Rotation4d2.Z * num5;
		    float num = Rotation4d2.W * num5;
		    float num13 = (y * num2) - (z * num3);
		    float num12 = (z * num4) - (x * num2);
		    float num11 = (x * num3) - (y * num4);
		    float num10 = ((x * num4) + (y * num3)) + (z * num2);
		    Rotation4d.X = ((x * num) + (num4 * w)) + num13;
		    Rotation4d.Y = ((y * num) + (num3 * w)) + num12;
		    Rotation4d.Z = ((z * num) + (num2 * w)) + num11;
		    Rotation4d.W = (w * num) - num10;
		    return Rotation4d;

        }

        public static void Divide(ref Rotation4d Rotation4d1, ref Rotation4d Rotation4d2, out Rotation4d result)
        {
            float x = Rotation4d1.X;
		    float y = Rotation4d1.Y;
		    float z = Rotation4d1.Z;
		    float w = Rotation4d1.W;
		    float num14 = (((Rotation4d2.X * Rotation4d2.X) + (Rotation4d2.Y * Rotation4d2.Y)) + (Rotation4d2.Z * Rotation4d2.Z)) + (Rotation4d2.W * Rotation4d2.W);
		    float num5 = 1f / num14;
		    float num4 = -Rotation4d2.X * num5;
		    float num3 = -Rotation4d2.Y * num5;
		    float num2 = -Rotation4d2.Z * num5;
		    float num = Rotation4d2.W * num5;
		    float num13 = (y * num2) - (z * num3);
		    float num12 = (z * num4) - (x * num2);
		    float num11 = (x * num3) - (y * num4);
		    float num10 = ((x * num4) + (y * num3)) + (z * num2);
		    result.X = ((x * num) + (num4 * w)) + num13;
		    result.Y = ((y * num) + (num3 * w)) + num12;
		    result.Z = ((z * num) + (num2 * w)) + num11;
		    result.W = (w * num) - num10;

        }


        public static float Dot(Rotation4d Rotation4d1, Rotation4d Rotation4d2)
        {
            return ((((Rotation4d1.X * Rotation4d2.X) + (Rotation4d1.Y * Rotation4d2.Y)) + (Rotation4d1.Z * Rotation4d2.Z)) + (Rotation4d1.W * Rotation4d2.W));
        }


        public static void Dot(ref Rotation4d Rotation4d1, ref Rotation4d Rotation4d2, out float result)
        {
            result = (((Rotation4d1.X * Rotation4d2.X) + (Rotation4d1.Y * Rotation4d2.Y)) + (Rotation4d1.Z * Rotation4d2.Z)) + (Rotation4d1.W * Rotation4d2.W);
        }


        public override bool Equals(object obj)
        {
             bool flag = false;
		    if (obj is Rotation4d)
		    {
		        flag = this.Equals((Rotation4d) obj);
		    }
		    return flag;
        }


        public bool Equals(Rotation4d other)
        {
			return ((((this.X == other.X) && (this.Y == other.Y)) && (this.Z == other.Z)) && (this.W == other.W));
        }


        public override int GetHashCode()
        {
            return (((this.X.GetHashCode() + this.Y.GetHashCode()) + this.Z.GetHashCode()) + this.W.GetHashCode());
        }


        public static Rotation4d Inverse(Rotation4d Rotation4d)
        {
            Rotation4d Rotation4d2;
		    float num2 = (((Rotation4d.X * Rotation4d.X) + (Rotation4d.Y * Rotation4d.Y)) + (Rotation4d.Z * Rotation4d.Z)) + (Rotation4d.W * Rotation4d.W);
		    float num = 1f / num2;
		    Rotation4d2.X = -Rotation4d.X * num;
		    Rotation4d2.Y = -Rotation4d.Y * num;
		    Rotation4d2.Z = -Rotation4d.Z * num;
		    Rotation4d2.W = Rotation4d.W * num;
		    return Rotation4d2;

        }

        public static void Inverse(ref Rotation4d Rotation4d, out Rotation4d result)
        {
            float num2 = (((Rotation4d.X * Rotation4d.X) + (Rotation4d.Y * Rotation4d.Y)) + (Rotation4d.Z * Rotation4d.Z)) + (Rotation4d.W * Rotation4d.W);
		    float num = 1f / num2;
		    result.X = -Rotation4d.X * num;
		    result.Y = -Rotation4d.Y * num;
		    result.Z = -Rotation4d.Z * num;
		    result.W = Rotation4d.W * num;
        }

        public float Length()
        {
            float num = (((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z)) + (this.W * this.W);
    		return (float) Math.Sqrt((double) num);
        }


        public float LengthSquared()
        {
            return ((((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z)) + (this.W * this.W));
        }


        public static Rotation4d Lerp(Rotation4d Rotation4d1, Rotation4d Rotation4d2, float amount)
        {
            float num = amount;
		    float num2 = 1f - num;
		    Rotation4d Rotation4d = new Rotation4d();
		    float num5 = (((Rotation4d1.X * Rotation4d2.X) + (Rotation4d1.Y * Rotation4d2.Y)) + (Rotation4d1.Z * Rotation4d2.Z)) + (Rotation4d1.W * Rotation4d2.W);
		    if (num5 >= 0f)
		    {
		        Rotation4d.X = (num2 * Rotation4d1.X) + (num * Rotation4d2.X);
		        Rotation4d.Y = (num2 * Rotation4d1.Y) + (num * Rotation4d2.Y);
		        Rotation4d.Z = (num2 * Rotation4d1.Z) + (num * Rotation4d2.Z);
		        Rotation4d.W = (num2 * Rotation4d1.W) + (num * Rotation4d2.W);
		    }
		    else
		    {
		        Rotation4d.X = (num2 * Rotation4d1.X) - (num * Rotation4d2.X);
		        Rotation4d.Y = (num2 * Rotation4d1.Y) - (num * Rotation4d2.Y);
		        Rotation4d.Z = (num2 * Rotation4d1.Z) - (num * Rotation4d2.Z);
		        Rotation4d.W = (num2 * Rotation4d1.W) - (num * Rotation4d2.W);
		    }
		    float num4 = (((Rotation4d.X * Rotation4d.X) + (Rotation4d.Y * Rotation4d.Y)) + (Rotation4d.Z * Rotation4d.Z)) + (Rotation4d.W * Rotation4d.W);
		    float num3 = 1f / ((float) Math.Sqrt((double) num4));
		    Rotation4d.X *= num3;
		    Rotation4d.Y *= num3;
		    Rotation4d.Z *= num3;
		    Rotation4d.W *= num3;
		    return Rotation4d;
        }


        public static void Lerp(ref Rotation4d Rotation4d1, ref Rotation4d Rotation4d2, float amount, out Rotation4d result)
        {
            float num = amount;
		    float num2 = 1f - num;
		    float num5 = (((Rotation4d1.X * Rotation4d2.X) + (Rotation4d1.Y * Rotation4d2.Y)) + (Rotation4d1.Z * Rotation4d2.Z)) + (Rotation4d1.W * Rotation4d2.W);
		    if (num5 >= 0f)
		    {
		        result.X = (num2 * Rotation4d1.X) + (num * Rotation4d2.X);
		        result.Y = (num2 * Rotation4d1.Y) + (num * Rotation4d2.Y);
		        result.Z = (num2 * Rotation4d1.Z) + (num * Rotation4d2.Z);
		        result.W = (num2 * Rotation4d1.W) + (num * Rotation4d2.W);
		    }
		    else
		    {
		        result.X = (num2 * Rotation4d1.X) - (num * Rotation4d2.X);
		        result.Y = (num2 * Rotation4d1.Y) - (num * Rotation4d2.Y);
		        result.Z = (num2 * Rotation4d1.Z) - (num * Rotation4d2.Z);
		        result.W = (num2 * Rotation4d1.W) - (num * Rotation4d2.W);
		    }
		    float num4 = (((result.X * result.X) + (result.Y * result.Y)) + (result.Z * result.Z)) + (result.W * result.W);
		    float num3 = 1f / ((float) Math.Sqrt((double) num4));
		    result.X *= num3;
		    result.Y *= num3;
		    result.Z *= num3;
		    result.W *= num3;

        }


        public static Rotation4d Slerp(Rotation4d Rotation4d1, Rotation4d Rotation4d2, float amount)
        {
            float num2;
		    float num3;
		    Rotation4d Rotation4d;
		    float num = amount;
		    float num4 = (((Rotation4d1.X * Rotation4d2.X) + (Rotation4d1.Y * Rotation4d2.Y)) + (Rotation4d1.Z * Rotation4d2.Z)) + (Rotation4d1.W * Rotation4d2.W);
		    bool flag = false;
		    if (num4 < 0f)
		    {
		        flag = true;
		        num4 = -num4;
		    }
		    if (num4 > 0.999999f)
		    {
		        num3 = 1f - num;
		        num2 = flag ? -num : num;
		    }
		    else
		    {
		        float num5 = (float) Math.Acos((double) num4);
		        float num6 = (float) (1.0 / Math.Sin((double) num5));
		        num3 = ((float) Math.Sin((double) ((1f - num) * num5))) * num6;
		        num2 = flag ? (((float) -Math.Sin((double) (num * num5))) * num6) : (((float) Math.Sin((double) (num * num5))) * num6);
		    }
		    Rotation4d.X = (num3 * Rotation4d1.X) + (num2 * Rotation4d2.X);
		    Rotation4d.Y = (num3 * Rotation4d1.Y) + (num2 * Rotation4d2.Y);
		    Rotation4d.Z = (num3 * Rotation4d1.Z) + (num2 * Rotation4d2.Z);
		    Rotation4d.W = (num3 * Rotation4d1.W) + (num2 * Rotation4d2.W);
		    return Rotation4d;
        }


        public static void Slerp(ref Rotation4d Rotation4d1, ref Rotation4d Rotation4d2, float amount, out Rotation4d result)
        {
            float num2;
		    float num3;
		    float num = amount;
		    float num4 = (((Rotation4d1.X * Rotation4d2.X) + (Rotation4d1.Y * Rotation4d2.Y)) + (Rotation4d1.Z * Rotation4d2.Z)) + (Rotation4d1.W * Rotation4d2.W);
		    bool flag = false;
		    if (num4 < 0f)
		    {
		        flag = true;
		        num4 = -num4;
		    }
		    if (num4 > 0.999999f)
		    {
		        num3 = 1f - num;
		        num2 = flag ? -num : num;
		    }
		    else
		    {
		        float num5 = (float) Math.Acos((double) num4);
		        float num6 = (float) (1.0 / Math.Sin((double) num5));
		        num3 = ((float) Math.Sin((double) ((1f - num) * num5))) * num6;
		        num2 = flag ? (((float) -Math.Sin((double) (num * num5))) * num6) : (((float) Math.Sin((double) (num * num5))) * num6);
		    }
		    result.X = (num3 * Rotation4d1.X) + (num2 * Rotation4d2.X);
		    result.Y = (num3 * Rotation4d1.Y) + (num2 * Rotation4d2.Y);
		    result.Z = (num3 * Rotation4d1.Z) + (num2 * Rotation4d2.Z);
		    result.W = (num3 * Rotation4d1.W) + (num2 * Rotation4d2.W);
        }


        public static Rotation4d Subtract(Rotation4d Rotation4d1, Rotation4d Rotation4d2)
        {
            Rotation4d Rotation4d;
		    Rotation4d.X = Rotation4d1.X - Rotation4d2.X;
		    Rotation4d.Y = Rotation4d1.Y - Rotation4d2.Y;
		    Rotation4d.Z = Rotation4d1.Z - Rotation4d2.Z;
		    Rotation4d.W = Rotation4d1.W - Rotation4d2.W;
		    return Rotation4d;
        }


        public static void Subtract(ref Rotation4d Rotation4d1, ref Rotation4d Rotation4d2, out Rotation4d result)
        {
            result.X = Rotation4d1.X - Rotation4d2.X;
		    result.Y = Rotation4d1.Y - Rotation4d2.Y;
		    result.Z = Rotation4d1.Z - Rotation4d2.Z;
		    result.W = Rotation4d1.W - Rotation4d2.W;
        }


        public static Rotation4d Multiply(Rotation4d Rotation4d1, Rotation4d Rotation4d2)
        {
            Rotation4d Rotation4d;
		    float x = Rotation4d1.X;
		    float y = Rotation4d1.Y;
		    float z = Rotation4d1.Z;
		    float w = Rotation4d1.W;
		    float num4 = Rotation4d2.X;
		    float num3 = Rotation4d2.Y;
		    float num2 = Rotation4d2.Z;
		    float num = Rotation4d2.W;
		    float num12 = (y * num2) - (z * num3);
		    float num11 = (z * num4) - (x * num2);
		    float num10 = (x * num3) - (y * num4);
		    float num9 = ((x * num4) + (y * num3)) + (z * num2);
		    Rotation4d.X = ((x * num) + (num4 * w)) + num12;
		    Rotation4d.Y = ((y * num) + (num3 * w)) + num11;
		    Rotation4d.Z = ((z * num) + (num2 * w)) + num10;
		    Rotation4d.W = (w * num) - num9;
		    return Rotation4d;
        }


        public static Rotation4d Multiply(Rotation4d Rotation4d1, float scaleFactor)
        {
            Rotation4d Rotation4d;
		    Rotation4d.X = Rotation4d1.X * scaleFactor;
		    Rotation4d.Y = Rotation4d1.Y * scaleFactor;
		    Rotation4d.Z = Rotation4d1.Z * scaleFactor;
		    Rotation4d.W = Rotation4d1.W * scaleFactor;
		    return Rotation4d;
        }


        public static void Multiply(ref Rotation4d Rotation4d1, float scaleFactor, out Rotation4d result)
        {
            result.X = Rotation4d1.X * scaleFactor;
		    result.Y = Rotation4d1.Y * scaleFactor;
		    result.Z = Rotation4d1.Z * scaleFactor;
		    result.W = Rotation4d1.W * scaleFactor;
        }


        public static void Multiply(ref Rotation4d Rotation4d1, ref Rotation4d Rotation4d2, out Rotation4d result)
        {
            float x = Rotation4d1.X;
		    float y = Rotation4d1.Y;
		    float z = Rotation4d1.Z;
		    float w = Rotation4d1.W;
		    float num4 = Rotation4d2.X;
		    float num3 = Rotation4d2.Y;
		    float num2 = Rotation4d2.Z;
		    float num = Rotation4d2.W;
		    float num12 = (y * num2) - (z * num3);
		    float num11 = (z * num4) - (x * num2);
		    float num10 = (x * num3) - (y * num4);
		    float num9 = ((x * num4) + (y * num3)) + (z * num2);
		    result.X = ((x * num) + (num4 * w)) + num12;
		    result.Y = ((y * num) + (num3 * w)) + num11;
		    result.Z = ((z * num) + (num2 * w)) + num10;
		    result.W = (w * num) - num9;
        }


        public static Rotation4d Negate(Rotation4d Rotation4d)
        {
            Rotation4d Rotation4d2;
		    Rotation4d2.X = -Rotation4d.X;
		    Rotation4d2.Y = -Rotation4d.Y;
		    Rotation4d2.Z = -Rotation4d.Z;
		    Rotation4d2.W = -Rotation4d.W;
		    return Rotation4d2;
        }


        public static void Negate(ref Rotation4d Rotation4d, out Rotation4d result)
        {
            result.X = -Rotation4d.X;
		    result.Y = -Rotation4d.Y;
		    result.Z = -Rotation4d.Z;
		    result.W = -Rotation4d.W;
        }


        public void Normalize()
        {
            float num2 = (((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z)) + (this.W * this.W);
		    float num = 1f / ((float) Math.Sqrt((double) num2));
		    this.X *= num;
		    this.Y *= num;
		    this.Z *= num;
		    this.W *= num;
        }


        public static Rotation4d Normalize(Rotation4d Rotation4d)
        {
            Rotation4d Rotation4d2;
		    float num2 = (((Rotation4d.X * Rotation4d.X) + (Rotation4d.Y * Rotation4d.Y)) + (Rotation4d.Z * Rotation4d.Z)) + (Rotation4d.W * Rotation4d.W);
		    float num = 1f / ((float) Math.Sqrt((double) num2));
		    Rotation4d2.X = Rotation4d.X * num;
		    Rotation4d2.Y = Rotation4d.Y * num;
		    Rotation4d2.Z = Rotation4d.Z * num;
		    Rotation4d2.W = Rotation4d.W * num;
		    return Rotation4d2;
        }


        public static void Normalize(ref Rotation4d Rotation4d, out Rotation4d result)
        {
            float num2 = (((Rotation4d.X * Rotation4d.X) + (Rotation4d.Y * Rotation4d.Y)) + (Rotation4d.Z * Rotation4d.Z)) + (Rotation4d.W * Rotation4d.W);
		    float num = 1f / ((float) Math.Sqrt((double) num2));
		    result.X = Rotation4d.X * num;
		    result.Y = Rotation4d.Y * num;
		    result.Z = Rotation4d.Z * num;
		    result.W = Rotation4d.W * num;
        }


        public static Rotation4d operator +(Rotation4d Rotation4d1, Rotation4d Rotation4d2)
        {
            Rotation4d Rotation4d;
		    Rotation4d.X = Rotation4d1.X + Rotation4d2.X;
		    Rotation4d.Y = Rotation4d1.Y + Rotation4d2.Y;
		    Rotation4d.Z = Rotation4d1.Z + Rotation4d2.Z;
		    Rotation4d.W = Rotation4d1.W + Rotation4d2.W;
		    return Rotation4d;
        }


        public static Rotation4d operator /(Rotation4d Rotation4d1, Rotation4d Rotation4d2)
        {
            Rotation4d Rotation4d;
		    float x = Rotation4d1.X;
		    float y = Rotation4d1.Y;
		    float z = Rotation4d1.Z;
		    float w = Rotation4d1.W;
		    float num14 = (((Rotation4d2.X * Rotation4d2.X) + (Rotation4d2.Y * Rotation4d2.Y)) + (Rotation4d2.Z * Rotation4d2.Z)) + (Rotation4d2.W * Rotation4d2.W);
		    float num5 = 1f / num14;
		    float num4 = -Rotation4d2.X * num5;
		    float num3 = -Rotation4d2.Y * num5;
		    float num2 = -Rotation4d2.Z * num5;
		    float num = Rotation4d2.W * num5;
		    float num13 = (y * num2) - (z * num3);
		    float num12 = (z * num4) - (x * num2);
		    float num11 = (x * num3) - (y * num4);
		    float num10 = ((x * num4) + (y * num3)) + (z * num2);
		    Rotation4d.X = ((x * num) + (num4 * w)) + num13;
		    Rotation4d.Y = ((y * num) + (num3 * w)) + num12;
		    Rotation4d.Z = ((z * num) + (num2 * w)) + num11;
		    Rotation4d.W = (w * num) - num10;
		    return Rotation4d;
        }


        public static bool operator ==(Rotation4d Rotation4d1, Rotation4d Rotation4d2)
        {
            return ((((Rotation4d1.X == Rotation4d2.X) && (Rotation4d1.Y == Rotation4d2.Y)) && (Rotation4d1.Z == Rotation4d2.Z)) && (Rotation4d1.W == Rotation4d2.W));
        }


        public static bool operator !=(Rotation4d Rotation4d1, Rotation4d Rotation4d2)
        {
            if (((Rotation4d1.X == Rotation4d2.X) && (Rotation4d1.Y == Rotation4d2.Y)) && (Rotation4d1.Z == Rotation4d2.Z))
		    {
		        return (Rotation4d1.W != Rotation4d2.W);
		    }
		    return true;
        }


        public static Rotation4d operator *(Rotation4d Rotation4d1, Rotation4d Rotation4d2)
        {
            Rotation4d Rotation4d;
		    float x = Rotation4d1.X;
		    float y = Rotation4d1.Y;
		    float z = Rotation4d1.Z;
		    float w = Rotation4d1.W;
		    float num4 = Rotation4d2.X;
		    float num3 = Rotation4d2.Y;
		    float num2 = Rotation4d2.Z;
		    float num = Rotation4d2.W;
		    float num12 = (y * num2) - (z * num3);
		    float num11 = (z * num4) - (x * num2);
		    float num10 = (x * num3) - (y * num4);
		    float num9 = ((x * num4) + (y * num3)) + (z * num2);
		    Rotation4d.X = ((x * num) + (num4 * w)) + num12;
		    Rotation4d.Y = ((y * num) + (num3 * w)) + num11;
		    Rotation4d.Z = ((z * num) + (num2 * w)) + num10;
		    Rotation4d.W = (w * num) - num9;
		    return Rotation4d;
        }


        public static Rotation4d operator *(Rotation4d Rotation4d1, float scaleFactor)
        {
            Rotation4d Rotation4d;
		    Rotation4d.X = Rotation4d1.X * scaleFactor;
		    Rotation4d.Y = Rotation4d1.Y * scaleFactor;
		    Rotation4d.Z = Rotation4d1.Z * scaleFactor;
		    Rotation4d.W = Rotation4d1.W * scaleFactor;
		    return Rotation4d;
        }


        public static Rotation4d operator -(Rotation4d Rotation4d1, Rotation4d Rotation4d2)
        {
            Rotation4d Rotation4d;
		    Rotation4d.X = Rotation4d1.X - Rotation4d2.X;
		    Rotation4d.Y = Rotation4d1.Y - Rotation4d2.Y;
		    Rotation4d.Z = Rotation4d1.Z - Rotation4d2.Z;
		    Rotation4d.W = Rotation4d1.W - Rotation4d2.W;
		    return Rotation4d;

        }


        public static Rotation4d operator -(Rotation4d Rotation4d)
        {
            Rotation4d Rotation4d2;
		    Rotation4d2.X = -Rotation4d.X;
		    Rotation4d2.Y = -Rotation4d.Y;
		    Rotation4d2.Z = -Rotation4d.Z;
		    Rotation4d2.W = -Rotation4d.W;
		    return Rotation4d2;
        }


        public override string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(32);
            sb.Append("{X:");
            sb.Append(this.X);
            sb.Append(" Y:");
            sb.Append(this.Y);
            sb.Append(" Z:");
            sb.Append(this.Z);
            sb.Append(" W:");
            sb.Append(this.W);
            sb.Append("}");
            return sb.ToString();
        }

		internal Matrix ToMatrix ()
		{
			Matrix matrix = Matrix.Identity;
			ToMatrix(out matrix);
			return matrix;
		}

		internal void ToMatrix (out Matrix matrix)
		{
			Rotation4d.ToMatrix(this, out matrix);
		}

		internal static void ToMatrix(Rotation4d Rotation4d, out Matrix matrix)
		{

			// source -> http://content.gpwiki.org/index.php/OpenGL:Tutorials:Using_Rotation4ds_to_represent_rotation#Rotation4d_to_Matrix
			float x2 = Rotation4d.X * Rotation4d.X;
			float y2 = Rotation4d.Y * Rotation4d.Y;
			float z2 = Rotation4d.Z * Rotation4d.Z;
			float xy = Rotation4d.X * Rotation4d.Y;
			float xz = Rotation4d.X * Rotation4d.Z;
			float yz = Rotation4d.Y * Rotation4d.Z;
			float wx = Rotation4d.W * Rotation4d.X;
			float wy = Rotation4d.W * Rotation4d.Y;
			float wz = Rotation4d.W * Rotation4d.Z;

			// This calculation would be a lot more complicated for non-unit length Rotation4ds
			// Note: The constructor of Matrix4 expects the Matrix in column-major format like expected by
			//   OpenGL
			matrix.M11 = 1.0f - 2.0f * (y2 + z2);
			matrix.M12 = 2.0f * (xy - wz);
			matrix.M13 = 2.0f * (xz + wy);
			matrix.M14 = 0.0f;

			matrix.M21 = 2.0f * (xy + wz);
			matrix.M22 = 1.0f - 2.0f * (x2 + z2);
			matrix.M23 = 2.0f * (yz - wx);
			matrix.M24 = 0.0f;

			matrix.M31 = 2.0f * (xz - wy);
			matrix.M32 = 2.0f * (yz + wx);
			matrix.M33 = 1.0f - 2.0f * (x2 + y2);
			matrix.M34 = 0.0f;

			matrix.M41 = 2.0f * (xz - wy);
			matrix.M42 = 2.0f * (yz + wx);
			matrix.M43 = 1.0f - 2.0f * (x2 + y2);
			matrix.M44 = 0.0f;

			//return Matrix4( 1.0f - 2.0f * (y2 + z2), 2.0f * (xy - wz), 2.0f * (xz + wy), 0.0f,
			//		2.0f * (xy + wz), 1.0f - 2.0f * (x2 + z2), 2.0f * (yz - wx), 0.0f,
			//		2.0f * (xz - wy), 2.0f * (yz + wx), 1.0f - 2.0f * (x2 + y2), 0.0f,
			//		0.0f, 0.0f, 0.0f, 1.0f)
			//	}
		}

		internal Vector3 Xyz
		{
			get {
				return new Vector3(X, Y, Z);
			}

			set {
				X = value.X;
				Y = value.Y;
				Z = value.Z;
			}
		}


    }
}
		#endif
	}







