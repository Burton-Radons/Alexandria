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

	/// <summary>Get the zero value of the <see cref="Rotation4f"/>.</summary>
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

	/// <summary>If the other object is a <see cref="Rotation4f"/> of the same type, get whether this <see cref="Rotation4f"/> has equal factors as it; otherwise return false.</summary>
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
	
	/// <summary>Get whether the <see cref="Rotation4f"/> values are equal.</summary>
	public static bool operator ==(Rotation4f a, Rotation4f b) { return a.X == b.X&&a.Y == b.Y&&a.Z == b.Z&&a.W == b.W; }

	/// <summary>Get whether the <see cref="Rotation4f"/> values are unequal.</summary>
	public static bool operator !=(Rotation4f a, Rotation4f b) { return a.X != b.X||a.Y != b.Y||a.Z != b.Z||a.W != b.W; }

			/// <summary>A quaternion that does nothing.</summary>
			public static readonly Rotation4f Identity = new Rotation4f(0, 0, 0, 1); 

			/// <summary>Initialise the rotation.</summary>
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

							/// <summary>Initialise the rotation.</summary>
				public Rotation4f( Angle3 angle) : this(angle.X, angle.Y, angle.Z) { }
							/// <summary>Initialise the rotation.</summary>
				public Rotation4f( ref  Angle3 angle) : this(angle.X, angle.Y, angle.Z) { }
			
								/// <summary>Convert to a matrix.</summary>
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

									/// <summary>Convert to a matrix.</summary>
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

									/// <summary>Convert to a matrix.</summary>
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

									/// <summary>Convert to a matrix.</summary>
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

	/// <summary>Get the zero value of the <see cref="Rotation4d"/>.</summary>
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

	/// <summary>If the other object is a <see cref="Rotation4d"/> of the same type, get whether this <see cref="Rotation4d"/> has equal factors as it; otherwise return false.</summary>
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
	
	/// <summary>Get whether the <see cref="Rotation4d"/> values are equal.</summary>
	public static bool operator ==(Rotation4d a, Rotation4d b) { return a.X == b.X&&a.Y == b.Y&&a.Z == b.Z&&a.W == b.W; }

	/// <summary>Get whether the <see cref="Rotation4d"/> values are unequal.</summary>
	public static bool operator !=(Rotation4d a, Rotation4d b) { return a.X != b.X||a.Y != b.Y||a.Z != b.Z||a.W != b.W; }

			/// <summary>A quaternion that does nothing.</summary>
			public static readonly Rotation4d Identity = new Rotation4d(0, 0, 0, 1); 

			/// <summary>Initialise the rotation.</summary>
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

							/// <summary>Initialise the rotation.</summary>
				public Rotation4d( Angle3 angle) : this(angle.X, angle.Y, angle.Z) { }
							/// <summary>Initialise the rotation.</summary>
				public Rotation4d( ref  Angle3 angle) : this(angle.X, angle.Y, angle.Z) { }
			
								/// <summary>Convert to a matrix.</summary>
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

									/// <summary>Convert to a matrix.</summary>
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

									/// <summary>Convert to a matrix.</summary>
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

									/// <summary>Convert to a matrix.</summary>
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
	}





