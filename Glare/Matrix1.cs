using System;
using System.Runtime.InteropServices;

namespace Glare
{
	
	/// <summary>A two-dimensional matrix type using <see cref="Single"/> elements.</summary>
	[StructLayout(LayoutKind.Explicit)]
	public partial struct Matrix2f : IEquatable<Matrix2f>
	{
		/// <summary>Get a <see cref="Matrix2f"/> that would have no effect when multiplied against a <see cref="Matrix2f"/> or <see cref="Vector2f"/>.</summary>
		public static readonly Matrix2f Identity = new Matrix2f(1, 0, 0, 1);

		// T [XYZW][XYZW]
					/// <summary>Get or set the first row of the first column of the <see cref="Matrix2f"/>.</summary>
			[FieldOffset(0)]
			public Single XX;
					/// <summary>Get or set the first row of the second column of the <see cref="Matrix2f"/>.</summary>
			[FieldOffset(4)]
			public Single XY;
					/// <summary>Get or set the second row of the first column of the <see cref="Matrix2f"/>.</summary>
			[FieldOffset(8)]
			public Single YX;
					/// <summary>Get or set the second row of the second column of the <see cref="Matrix2f"/>.</summary>
			[FieldOffset(12)]
			public Single YY;
				
		// VectorR [XYZW]Row
					/// <summary>Get or set the first row of the <see cref="Matrix2f"/>.</summary>
			public Vector2f XRow
			{
				get { return new Vector2f(XX, XY); }
				set { XX = value.X;XY = value.Y; }
			}
					/// <summary>Get or set the second row of the <see cref="Matrix2f"/>.</summary>
			public Vector2f YRow
			{
				get { return new Vector2f(YX, YY); }
				set { YX = value.X;YY = value.Y; }
			}
		
		// VectorC [XYZW]Column
					/// <summary>Get or set the first column of the <see cref="Matrix2f"/>.</summary>
			public Vector2f XColumn
			{
				get { return new Vector2f(XX, YX); }
				set { XX = value.X;YX = value.Y; }
			}
					/// <summary>Get or set the second column of the <see cref="Matrix2f"/>.</summary>
			public Vector2f YColumn
			{
				get { return new Vector2f(XY, YY); }
				set { XY = value.X;YY = value.Y; }
			}
				
		/// <summary>Get or set the diagonal Vector2f of the <see cref="Matrix2f"/>.</summary>
		public Vector2f Diagonal
		{
			get { return new Vector2f(XX, YY); }
			set { XX = value.X;YY = value.Y; }
		}

		// This(T [xyzw][xyzw]...)
		/// <summary>Assign the coefficients of the <see cref="Matrix2f"/> to the values provided. The coefficients are in row-major order, so the first coefficient is XX (first row of the first column) and the second coefficient is XY (first row of the <b>second</b> column).</summary>
					/// <param name="xx">The value to assign to the first row of the first column in field <see cref="XX"/>.</param>
					/// <param name="xy">The value to assign to the first row of the second column in field <see cref="XY"/>.</param>
					/// <param name="yx">The value to assign to the second row of the first column in field <see cref="YX"/>.</param>
					/// <param name="yy">The value to assign to the second row of the second column in field <see cref="YY"/>.</param>
				public Matrix2f(Single xx, Single xy, Single yx, Single yy)
		{
			XX = xx;XY = xy;YX = yx;YY = yy;		}

		public Single this[int row, int column] {
			get {
									if(row == 0) {
													if(column == 0)
								return XX;
						 else 							if(column == 1)
								return XY;
												else
							throw new ArgumentOutOfRangeException("column");
					}
				 else 					if(row == 1) {
													if(column == 0)
								return YX;
						 else 							if(column == 1)
								return YY;
												else
							throw new ArgumentOutOfRangeException("column");
					}
								else
					throw new ArgumentOutOfRangeException("row");
			}

			set {
									if(row == 0) {
													if(column == 0)
								XX = value;
						 else 							if(column == 1)
								XY = value;
												else
							throw new ArgumentOutOfRangeException("column");
					}
				 else 					if(row == 1) {
													if(column == 0)
								YX = value;
						 else 							if(column == 1)
								YY = value;
												else
							throw new ArgumentOutOfRangeException("column");
					}
								else
					throw new ArgumentOutOfRangeException("row");
			}
		}

		#region Methods

		/// <summary>Test for equality in all coefficients between this <see cref="Matrix2f"/> and the other one.</summary>
		public bool Equals(ref Matrix2f other)
		{
			return XX == other.XX && XY == other.XY && YX == other.YX && YY == other.YY;
		}

		/// <summary>Test for equality in all coefficients between this <see cref="Matrix2f"/> and the other one.</summary>
		public bool Equals(Matrix2f other) { return Equals(ref other); }

		/// <summary>Test for equality between this <see cref="Matrix2f"/> and the provided object, which must be an equivalent <see cref="Matrix2f"/> to return <c>true</c>.</summary>
		public override bool Equals(object other)
		{
			if(other is Matrix2f)
				return Equals((Matrix2f)other);
			return base.Equals(other);
		}

		/// <summary>Compute a hash code containing all of the <see cref="Matrix2f"/>'s coefficients.</summary>
		public override int GetHashCode()
		{
			return XX.GetHashCode() ^ XY.GetHashCode() ^ YX.GetHashCode() ^ YY.GetHashCode();
		}

		// Multiply
					public Matrix2f Multiply(Matrix2f other )
			{
				Matrix2f result;
				Multiply(ref other, out result);
				return result;
			}

			public Matrix2f Multiply(ref Matrix2f other )
			{
				Matrix2f result;
																		Single vXX =   XX * other.XX  + XY * other.YX ;
											Single vXY =   XX * other.XY  + XY * other.YY ;
											Single vYX =   YX * other.XX  + YY * other.YX ;
											Single vYY =   YX * other.XY  + YY * other.YY ;
															result.XX = vXX;
											result.XY = vXY;
											result.YX = vYX;
											result.YY = vYY;
									return result;
			}
					public void Multiply(Matrix2f other , out Matrix2f result)
			{
				
				Multiply(ref other, out result);
				return;
			}

			public void Multiply(ref Matrix2f other , out Matrix2f result)
			{
				
																		Single vXX =   XX * other.XX  + XY * other.YX ;
											Single vXY =   XX * other.XY  + XY * other.YY ;
											Single vYX =   YX * other.XX  + YY * other.YX ;
											Single vYY =   YX * other.XY  + YY * other.YY ;
															result.XX = vXX;
											result.XY = vXY;
											result.YX = vYX;
											result.YY = vYY;
									return;
			}
		
		// Translate
		
		#region Square matrix methods

									
				public static Matrix2f Scale(Single amount )
				{
					Matrix2f result;
											result.XX = amount;
											result.XY = 0;
											result.YX = 0;
											result.YY = 1;
										return result;
				}
							
				public static void Scale(Single amount , out Matrix2f result)
				{
					
											result.XX = amount;
											result.XY = 0;
											result.YX = 0;
											result.YY = 1;
										return;
				}
					
		#endregion Square matrix methods

		#region 4x4 Methods

		
		#endregion 4x4 methods

		
		#endregion Methods

		#region Operators

		/// <summary>Test whether the <see cref="Matrix2f"/> objects have equivalent coefficients.</summary>
		public static bool operator ==(Matrix2f a, Matrix2f b) { return a.Equals(ref b); }

		/// <summary>Test whether the <see cref="Matrix2f"/> objects do not have equivalent coefficients.</summary>
		public static bool operator !=(Matrix2f a, Matrix2f b) { return !a.Equals(ref b); }

					/// <summary>Cast to the <see cref="Matrix2d"/>.</summary>
			public static explicit operator Matrix2d(Matrix2f a)
			{
				return new Matrix2d((Double)a.XX, (Double)a.XY, (Double)a.YX, (Double)a.YY);
			}
			
		public static Matrix2f operator *(Matrix2f a, Matrix2f b) { Matrix2f result; a.Multiply(ref b, out result); return result; }

		
		#endregion Operators
	}
	
	
	/// <summary>A two-dimensional matrix type using <see cref="Double"/> elements.</summary>
	[StructLayout(LayoutKind.Explicit)]
	public partial struct Matrix2d : IEquatable<Matrix2d>
	{
		/// <summary>Get a <see cref="Matrix2d"/> that would have no effect when multiplied against a <see cref="Matrix2d"/> or <see cref="Vector2d"/>.</summary>
		public static readonly Matrix2d Identity = new Matrix2d(1, 0, 0, 1);

		// T [XYZW][XYZW]
					/// <summary>Get or set the first row of the first column of the <see cref="Matrix2d"/>.</summary>
			[FieldOffset(0)]
			public Double XX;
					/// <summary>Get or set the first row of the second column of the <see cref="Matrix2d"/>.</summary>
			[FieldOffset(8)]
			public Double XY;
					/// <summary>Get or set the second row of the first column of the <see cref="Matrix2d"/>.</summary>
			[FieldOffset(16)]
			public Double YX;
					/// <summary>Get or set the second row of the second column of the <see cref="Matrix2d"/>.</summary>
			[FieldOffset(24)]
			public Double YY;
				
		// VectorR [XYZW]Row
					/// <summary>Get or set the first row of the <see cref="Matrix2d"/>.</summary>
			public Vector2d XRow
			{
				get { return new Vector2d(XX, XY); }
				set { XX = value.X;XY = value.Y; }
			}
					/// <summary>Get or set the second row of the <see cref="Matrix2d"/>.</summary>
			public Vector2d YRow
			{
				get { return new Vector2d(YX, YY); }
				set { YX = value.X;YY = value.Y; }
			}
		
		// VectorC [XYZW]Column
					/// <summary>Get or set the first column of the <see cref="Matrix2d"/>.</summary>
			public Vector2d XColumn
			{
				get { return new Vector2d(XX, YX); }
				set { XX = value.X;YX = value.Y; }
			}
					/// <summary>Get or set the second column of the <see cref="Matrix2d"/>.</summary>
			public Vector2d YColumn
			{
				get { return new Vector2d(XY, YY); }
				set { XY = value.X;YY = value.Y; }
			}
				
		/// <summary>Get or set the diagonal Vector2d of the <see cref="Matrix2d"/>.</summary>
		public Vector2d Diagonal
		{
			get { return new Vector2d(XX, YY); }
			set { XX = value.X;YY = value.Y; }
		}

		// This(T [xyzw][xyzw]...)
		/// <summary>Assign the coefficients of the <see cref="Matrix2d"/> to the values provided. The coefficients are in row-major order, so the first coefficient is XX (first row of the first column) and the second coefficient is XY (first row of the <b>second</b> column).</summary>
					/// <param name="xx">The value to assign to the first row of the first column in field <see cref="XX"/>.</param>
					/// <param name="xy">The value to assign to the first row of the second column in field <see cref="XY"/>.</param>
					/// <param name="yx">The value to assign to the second row of the first column in field <see cref="YX"/>.</param>
					/// <param name="yy">The value to assign to the second row of the second column in field <see cref="YY"/>.</param>
				public Matrix2d(Double xx, Double xy, Double yx, Double yy)
		{
			XX = xx;XY = xy;YX = yx;YY = yy;		}

		public Double this[int row, int column] {
			get {
									if(row == 0) {
													if(column == 0)
								return XX;
						 else 							if(column == 1)
								return XY;
												else
							throw new ArgumentOutOfRangeException("column");
					}
				 else 					if(row == 1) {
													if(column == 0)
								return YX;
						 else 							if(column == 1)
								return YY;
												else
							throw new ArgumentOutOfRangeException("column");
					}
								else
					throw new ArgumentOutOfRangeException("row");
			}

			set {
									if(row == 0) {
													if(column == 0)
								XX = value;
						 else 							if(column == 1)
								XY = value;
												else
							throw new ArgumentOutOfRangeException("column");
					}
				 else 					if(row == 1) {
													if(column == 0)
								YX = value;
						 else 							if(column == 1)
								YY = value;
												else
							throw new ArgumentOutOfRangeException("column");
					}
								else
					throw new ArgumentOutOfRangeException("row");
			}
		}

		#region Methods

		/// <summary>Test for equality in all coefficients between this <see cref="Matrix2d"/> and the other one.</summary>
		public bool Equals(ref Matrix2d other)
		{
			return XX == other.XX && XY == other.XY && YX == other.YX && YY == other.YY;
		}

		/// <summary>Test for equality in all coefficients between this <see cref="Matrix2d"/> and the other one.</summary>
		public bool Equals(Matrix2d other) { return Equals(ref other); }

		/// <summary>Test for equality between this <see cref="Matrix2d"/> and the provided object, which must be an equivalent <see cref="Matrix2d"/> to return <c>true</c>.</summary>
		public override bool Equals(object other)
		{
			if(other is Matrix2d)
				return Equals((Matrix2d)other);
			return base.Equals(other);
		}

		/// <summary>Compute a hash code containing all of the <see cref="Matrix2d"/>'s coefficients.</summary>
		public override int GetHashCode()
		{
			return XX.GetHashCode() ^ XY.GetHashCode() ^ YX.GetHashCode() ^ YY.GetHashCode();
		}

		// Multiply
					public Matrix2d Multiply(Matrix2d other )
			{
				Matrix2d result;
				Multiply(ref other, out result);
				return result;
			}

			public Matrix2d Multiply(ref Matrix2d other )
			{
				Matrix2d result;
																		Double vXX =   XX * other.XX  + XY * other.YX ;
											Double vXY =   XX * other.XY  + XY * other.YY ;
											Double vYX =   YX * other.XX  + YY * other.YX ;
											Double vYY =   YX * other.XY  + YY * other.YY ;
															result.XX = vXX;
											result.XY = vXY;
											result.YX = vYX;
											result.YY = vYY;
									return result;
			}
					public void Multiply(Matrix2d other , out Matrix2d result)
			{
				
				Multiply(ref other, out result);
				return;
			}

			public void Multiply(ref Matrix2d other , out Matrix2d result)
			{
				
																		Double vXX =   XX * other.XX  + XY * other.YX ;
											Double vXY =   XX * other.XY  + XY * other.YY ;
											Double vYX =   YX * other.XX  + YY * other.YX ;
											Double vYY =   YX * other.XY  + YY * other.YY ;
															result.XX = vXX;
											result.XY = vXY;
											result.YX = vYX;
											result.YY = vYY;
									return;
			}
		
		// Translate
		
		#region Square matrix methods

									
				public static Matrix2d Scale(Double amount )
				{
					Matrix2d result;
											result.XX = amount;
											result.XY = 0;
											result.YX = 0;
											result.YY = 1;
										return result;
				}
							
				public static void Scale(Double amount , out Matrix2d result)
				{
					
											result.XX = amount;
											result.XY = 0;
											result.YX = 0;
											result.YY = 1;
										return;
				}
					
		#endregion Square matrix methods

		#region 4x4 Methods

		
		#endregion 4x4 methods

		
		#endregion Methods

		#region Operators

		/// <summary>Test whether the <see cref="Matrix2d"/> objects have equivalent coefficients.</summary>
		public static bool operator ==(Matrix2d a, Matrix2d b) { return a.Equals(ref b); }

		/// <summary>Test whether the <see cref="Matrix2d"/> objects do not have equivalent coefficients.</summary>
		public static bool operator !=(Matrix2d a, Matrix2d b) { return !a.Equals(ref b); }

					/// <summary>Cast to the <see cref="Matrix2f"/>.</summary>
			public static explicit operator Matrix2f(Matrix2d a)
			{
				return new Matrix2f((Single)a.XX, (Single)a.XY, (Single)a.YX, (Single)a.YY);
			}
			
		public static Matrix2d operator *(Matrix2d a, Matrix2d b) { Matrix2d result; a.Multiply(ref b, out result); return result; }

		
		#endregion Operators
	}
	
	
	/// <summary>A three-dimensional matrix type using <see cref="Single"/> elements.</summary>
	[StructLayout(LayoutKind.Explicit)]
	public partial struct Matrix3f : IEquatable<Matrix3f>
	{
		/// <summary>Get a <see cref="Matrix3f"/> that would have no effect when multiplied against a <see cref="Matrix3f"/> or <see cref="Vector3f"/>.</summary>
		public static readonly Matrix3f Identity = new Matrix3f(1, 0, 0, 0, 1, 0, 0, 0, 1);

		// T [XYZW][XYZW]
					/// <summary>Get or set the first row of the first column of the <see cref="Matrix3f"/>.</summary>
			[FieldOffset(0)]
			public Single XX;
					/// <summary>Get or set the first row of the second column of the <see cref="Matrix3f"/>.</summary>
			[FieldOffset(4)]
			public Single XY;
					/// <summary>Get or set the first row of the third column of the <see cref="Matrix3f"/>.</summary>
			[FieldOffset(8)]
			public Single XZ;
					/// <summary>Get or set the second row of the first column of the <see cref="Matrix3f"/>.</summary>
			[FieldOffset(12)]
			public Single YX;
					/// <summary>Get or set the second row of the second column of the <see cref="Matrix3f"/>.</summary>
			[FieldOffset(16)]
			public Single YY;
					/// <summary>Get or set the second row of the third column of the <see cref="Matrix3f"/>.</summary>
			[FieldOffset(20)]
			public Single YZ;
					/// <summary>Get or set the third row of the first column of the <see cref="Matrix3f"/>.</summary>
			[FieldOffset(24)]
			public Single ZX;
					/// <summary>Get or set the third row of the second column of the <see cref="Matrix3f"/>.</summary>
			[FieldOffset(28)]
			public Single ZY;
					/// <summary>Get or set the third row of the third column of the <see cref="Matrix3f"/>.</summary>
			[FieldOffset(32)]
			public Single ZZ;
				
		// VectorR [XYZW]Row
					/// <summary>Get or set the first row of the <see cref="Matrix3f"/>.</summary>
			public Vector3f XRow
			{
				get { return new Vector3f(XX, XY, XZ); }
				set { XX = value.X;XY = value.Y;XZ = value.Z; }
			}
					/// <summary>Get or set the second row of the <see cref="Matrix3f"/>.</summary>
			public Vector3f YRow
			{
				get { return new Vector3f(YX, YY, YZ); }
				set { YX = value.X;YY = value.Y;YZ = value.Z; }
			}
					/// <summary>Get or set the third row of the <see cref="Matrix3f"/>.</summary>
			public Vector3f ZRow
			{
				get { return new Vector3f(ZX, ZY, ZZ); }
				set { ZX = value.X;ZY = value.Y;ZZ = value.Z; }
			}
		
		// VectorC [XYZW]Column
					/// <summary>Get or set the first column of the <see cref="Matrix3f"/>.</summary>
			public Vector3f XColumn
			{
				get { return new Vector3f(XX, YX, ZX); }
				set { XX = value.X;YX = value.Y;ZX = value.Z; }
			}
					/// <summary>Get or set the second column of the <see cref="Matrix3f"/>.</summary>
			public Vector3f YColumn
			{
				get { return new Vector3f(XY, YY, ZY); }
				set { XY = value.X;YY = value.Y;ZY = value.Z; }
			}
					/// <summary>Get or set the third column of the <see cref="Matrix3f"/>.</summary>
			public Vector3f ZColumn
			{
				get { return new Vector3f(XZ, YZ, ZZ); }
				set { XZ = value.X;YZ = value.Y;ZZ = value.Z; }
			}
				
		/// <summary>Get or set the diagonal Vector3f of the <see cref="Matrix3f"/>.</summary>
		public Vector3f Diagonal
		{
			get { return new Vector3f(XX, YY, ZZ); }
			set { XX = value.X;YY = value.Y;ZZ = value.Z; }
		}

		// This(T [xyzw][xyzw]...)
		/// <summary>Assign the coefficients of the <see cref="Matrix3f"/> to the values provided. The coefficients are in row-major order, so the first coefficient is XX (first row of the first column) and the second coefficient is XY (first row of the <b>second</b> column).</summary>
					/// <param name="xx">The value to assign to the first row of the first column in field <see cref="XX"/>.</param>
					/// <param name="xy">The value to assign to the first row of the second column in field <see cref="XY"/>.</param>
					/// <param name="xz">The value to assign to the first row of the third column in field <see cref="XZ"/>.</param>
					/// <param name="yx">The value to assign to the second row of the first column in field <see cref="YX"/>.</param>
					/// <param name="yy">The value to assign to the second row of the second column in field <see cref="YY"/>.</param>
					/// <param name="yz">The value to assign to the second row of the third column in field <see cref="YZ"/>.</param>
					/// <param name="zx">The value to assign to the third row of the first column in field <see cref="ZX"/>.</param>
					/// <param name="zy">The value to assign to the third row of the second column in field <see cref="ZY"/>.</param>
					/// <param name="zz">The value to assign to the third row of the third column in field <see cref="ZZ"/>.</param>
				public Matrix3f(Single xx, Single xy, Single xz, Single yx, Single yy, Single yz, Single zx, Single zy, Single zz)
		{
			XX = xx;XY = xy;XZ = xz;YX = yx;YY = yy;YZ = yz;ZX = zx;ZY = zy;ZZ = zz;		}

		public Single this[int row, int column] {
			get {
									if(row == 0) {
													if(column == 0)
								return XX;
						 else 							if(column == 1)
								return XY;
						 else 							if(column == 2)
								return XZ;
												else
							throw new ArgumentOutOfRangeException("column");
					}
				 else 					if(row == 1) {
													if(column == 0)
								return YX;
						 else 							if(column == 1)
								return YY;
						 else 							if(column == 2)
								return YZ;
												else
							throw new ArgumentOutOfRangeException("column");
					}
				 else 					if(row == 2) {
													if(column == 0)
								return ZX;
						 else 							if(column == 1)
								return ZY;
						 else 							if(column == 2)
								return ZZ;
												else
							throw new ArgumentOutOfRangeException("column");
					}
								else
					throw new ArgumentOutOfRangeException("row");
			}

			set {
									if(row == 0) {
													if(column == 0)
								XX = value;
						 else 							if(column == 1)
								XY = value;
						 else 							if(column == 2)
								XZ = value;
												else
							throw new ArgumentOutOfRangeException("column");
					}
				 else 					if(row == 1) {
													if(column == 0)
								YX = value;
						 else 							if(column == 1)
								YY = value;
						 else 							if(column == 2)
								YZ = value;
												else
							throw new ArgumentOutOfRangeException("column");
					}
				 else 					if(row == 2) {
													if(column == 0)
								ZX = value;
						 else 							if(column == 1)
								ZY = value;
						 else 							if(column == 2)
								ZZ = value;
												else
							throw new ArgumentOutOfRangeException("column");
					}
								else
					throw new ArgumentOutOfRangeException("row");
			}
		}

		#region Methods

		/// <summary>Test for equality in all coefficients between this <see cref="Matrix3f"/> and the other one.</summary>
		public bool Equals(ref Matrix3f other)
		{
			return XX == other.XX && XY == other.XY && XZ == other.XZ && YX == other.YX && YY == other.YY && YZ == other.YZ && ZX == other.ZX && ZY == other.ZY && ZZ == other.ZZ;
		}

		/// <summary>Test for equality in all coefficients between this <see cref="Matrix3f"/> and the other one.</summary>
		public bool Equals(Matrix3f other) { return Equals(ref other); }

		/// <summary>Test for equality between this <see cref="Matrix3f"/> and the provided object, which must be an equivalent <see cref="Matrix3f"/> to return <c>true</c>.</summary>
		public override bool Equals(object other)
		{
			if(other is Matrix3f)
				return Equals((Matrix3f)other);
			return base.Equals(other);
		}

		/// <summary>Compute a hash code containing all of the <see cref="Matrix3f"/>'s coefficients.</summary>
		public override int GetHashCode()
		{
			return XX.GetHashCode() ^ XY.GetHashCode() ^ XZ.GetHashCode() ^ YX.GetHashCode() ^ YY.GetHashCode() ^ YZ.GetHashCode() ^ ZX.GetHashCode() ^ ZY.GetHashCode() ^ ZZ.GetHashCode();
		}

		// Multiply
					public Matrix3f Multiply(Matrix3f other )
			{
				Matrix3f result;
				Multiply(ref other, out result);
				return result;
			}

			public Matrix3f Multiply(ref Matrix3f other )
			{
				Matrix3f result;
																		Single vXX =   XX * other.XX  + XY * other.YX  + XZ * other.ZX ;
											Single vXY =   XX * other.XY  + XY * other.YY  + XZ * other.ZY ;
											Single vXZ =   XX * other.XZ  + XY * other.YZ  + XZ * other.ZZ ;
											Single vYX =   YX * other.XX  + YY * other.YX  + YZ * other.ZX ;
											Single vYY =   YX * other.XY  + YY * other.YY  + YZ * other.ZY ;
											Single vYZ =   YX * other.XZ  + YY * other.YZ  + YZ * other.ZZ ;
											Single vZX =   ZX * other.XX  + ZY * other.YX  + ZZ * other.ZX ;
											Single vZY =   ZX * other.XY  + ZY * other.YY  + ZZ * other.ZY ;
											Single vZZ =   ZX * other.XZ  + ZY * other.YZ  + ZZ * other.ZZ ;
															result.XX = vXX;
											result.XY = vXY;
											result.XZ = vXZ;
											result.YX = vYX;
											result.YY = vYY;
											result.YZ = vYZ;
											result.ZX = vZX;
											result.ZY = vZY;
											result.ZZ = vZZ;
									return result;
			}
					public void Multiply(Matrix3f other , out Matrix3f result)
			{
				
				Multiply(ref other, out result);
				return;
			}

			public void Multiply(ref Matrix3f other , out Matrix3f result)
			{
				
																		Single vXX =   XX * other.XX  + XY * other.YX  + XZ * other.ZX ;
											Single vXY =   XX * other.XY  + XY * other.YY  + XZ * other.ZY ;
											Single vXZ =   XX * other.XZ  + XY * other.YZ  + XZ * other.ZZ ;
											Single vYX =   YX * other.XX  + YY * other.YX  + YZ * other.ZX ;
											Single vYY =   YX * other.XY  + YY * other.YY  + YZ * other.ZY ;
											Single vYZ =   YX * other.XZ  + YY * other.YZ  + YZ * other.ZZ ;
											Single vZX =   ZX * other.XX  + ZY * other.YX  + ZZ * other.ZX ;
											Single vZY =   ZX * other.XY  + ZY * other.YY  + ZZ * other.ZY ;
											Single vZZ =   ZX * other.XZ  + ZY * other.YZ  + ZZ * other.ZZ ;
															result.XX = vXX;
											result.XY = vXY;
											result.XZ = vXZ;
											result.YX = vYX;
											result.YY = vYY;
											result.YZ = vYZ;
											result.ZX = vZX;
											result.ZY = vZY;
											result.ZZ = vZZ;
									return;
			}
		
		// Translate
					public static Matrix3f Translate(Vector2f amount ) { Matrix3f result; Translate(ref amount, out result); return result; }

			public static Matrix3f Translate(ref Vector2f amount ) 
			{
				Matrix3f result;
				 result.XX = 1;  result.XY = 0;  result.XZ = 0;  result.YX = 0;  result.YY = 1;  result.YZ = 0;  result.ZX = amount.X;  result.ZY = amount.Y;  result.ZZ = 1; 				return result;
			}

			public static Matrix3f Translate( Single X ,  Single Y  )
			{
				Matrix3f result;
				 result.XX = 1;  result.XY = 0;  result.XZ = 0;  result.YX = 0;  result.YY = 1;  result.YZ = 0;  result.ZX = X;  result.ZY = Y;  result.ZZ = 1; 				return result;
			}
					public static void Translate(Vector2f amount , out Matrix3f result) {  Translate(ref amount, out result); return; }

			public static void Translate(ref Vector2f amount , out Matrix3f result) 
			{
				
				 result.XX = 1;  result.XY = 0;  result.XZ = 0;  result.YX = 0;  result.YY = 1;  result.YZ = 0;  result.ZX = amount.X;  result.ZY = amount.Y;  result.ZZ = 1; 				return;
			}

			public static void Translate( Single X ,  Single Y  , out Matrix3f result)
			{
				
				 result.XX = 1;  result.XY = 0;  result.XZ = 0;  result.YX = 0;  result.YY = 1;  result.YZ = 0;  result.ZX = X;  result.ZY = Y;  result.ZZ = 1; 				return;
			}
		
		#region Square matrix methods

														public static Matrix3f Scale( Vector2f amount )
					{
						Matrix3f result;
													result.XX = amount.X;
													result.XY = 0;
													result.XZ = 0;
													result.YX = 0;
													result.YY = amount.Y;
													result.YZ = 0;
													result.ZX = 0;
													result.ZY = 0;
													result.ZZ = 1;
												return result;
					}

					public static Matrix3f Scale( Single x ,  Single y  )
					{
						Matrix3f result;
													result.XX = x;
													result.XY = 0;
													result.XZ = 0;
													result.YX = 0;
													result.YY = y;
													result.YZ = 0;
													result.ZX = 0;
													result.ZY = 0;
													result.ZZ = 1;
												return result;
					}
				
				public static Matrix3f Scale(Single amount )
				{
					Matrix3f result;
											result.XX = amount;
											result.XY = 0;
											result.XZ = 0;
											result.YX = 0;
											result.YY = amount;
											result.YZ = 0;
											result.ZX = 0;
											result.ZY = 0;
											result.ZZ = 1;
										return result;
				}
												public static void Scale( ref  Vector2f amount , out Matrix3f result)
					{
						
													result.XX = amount.X;
													result.XY = 0;
													result.XZ = 0;
													result.YX = 0;
													result.YY = amount.Y;
													result.YZ = 0;
													result.ZX = 0;
													result.ZY = 0;
													result.ZZ = 1;
												return;
					}

					public static void Scale( Single x ,  Single y  , out Matrix3f result)
					{
						
													result.XX = x;
													result.XY = 0;
													result.XZ = 0;
													result.YX = 0;
													result.YY = y;
													result.YZ = 0;
													result.ZX = 0;
													result.ZY = 0;
													result.ZZ = 1;
												return;
					}
				
				public static void Scale(Single amount , out Matrix3f result)
				{
					
											result.XX = amount;
											result.XY = 0;
											result.XZ = 0;
											result.YX = 0;
											result.YY = amount;
											result.YZ = 0;
											result.ZX = 0;
											result.ZY = 0;
											result.ZZ = 1;
										return;
				}
					
		#endregion Square matrix methods

		#region 4x4 Methods

		
		#endregion 4x4 methods

														public Vector2f Multiply( Vector2f vector ) {
						Vector2f result;
													result.X = 								XX * vector.X
							+								YX * vector.Y
							 + ZX;
													result.Y = 								XY * vector.X
							+								YY * vector.Y
							 + ZY;
												return result;
					}
									public Vector2f Multiply( ref  Vector2f vector ) {
						Vector2f result;
													result.X = 								XX * vector.X
							+								YX * vector.Y
							 + ZX;
													result.Y = 								XY * vector.X
							+								YY * vector.Y
							 + ZY;
												return result;
					}
																public void Multiply( Vector2f vector , out Vector2f result) {
						
													result.X = 								XX * vector.X
							+								YX * vector.Y
							 + ZX;
													result.Y = 								XY * vector.X
							+								YY * vector.Y
							 + ZY;
												return;
					}
									public void Multiply( ref  Vector2f vector , out Vector2f result) {
						
													result.X = 								XX * vector.X
							+								YX * vector.Y
							 + ZX;
													result.Y = 								XY * vector.X
							+								YY * vector.Y
							 + ZY;
												return;
					}
									
		#endregion Methods

		#region Operators

		/// <summary>Test whether the <see cref="Matrix3f"/> objects have equivalent coefficients.</summary>
		public static bool operator ==(Matrix3f a, Matrix3f b) { return a.Equals(ref b); }

		/// <summary>Test whether the <see cref="Matrix3f"/> objects do not have equivalent coefficients.</summary>
		public static bool operator !=(Matrix3f a, Matrix3f b) { return !a.Equals(ref b); }

					/// <summary>Cast to the <see cref="Matrix3d"/>.</summary>
			public static explicit operator Matrix3d(Matrix3f a)
			{
				return new Matrix3d((Double)a.XX, (Double)a.XY, (Double)a.XZ, (Double)a.YX, (Double)a.YY, (Double)a.YZ, (Double)a.ZX, (Double)a.ZY, (Double)a.ZZ);
			}
			
		public static Matrix3f operator *(Matrix3f a, Matrix3f b) { Matrix3f result; a.Multiply(ref b, out result); return result; }

					public static Vector2f operator *(Matrix3f a, Vector2f b) { Vector2f result; a.Multiply(ref b, out result); return result; }
		
		#endregion Operators
	}
	
	
	/// <summary>A three-dimensional matrix type using <see cref="Double"/> elements.</summary>
	[StructLayout(LayoutKind.Explicit)]
	public partial struct Matrix3d : IEquatable<Matrix3d>
	{
		/// <summary>Get a <see cref="Matrix3d"/> that would have no effect when multiplied against a <see cref="Matrix3d"/> or <see cref="Vector3d"/>.</summary>
		public static readonly Matrix3d Identity = new Matrix3d(1, 0, 0, 0, 1, 0, 0, 0, 1);

		// T [XYZW][XYZW]
					/// <summary>Get or set the first row of the first column of the <see cref="Matrix3d"/>.</summary>
			[FieldOffset(0)]
			public Double XX;
					/// <summary>Get or set the first row of the second column of the <see cref="Matrix3d"/>.</summary>
			[FieldOffset(8)]
			public Double XY;
					/// <summary>Get or set the first row of the third column of the <see cref="Matrix3d"/>.</summary>
			[FieldOffset(16)]
			public Double XZ;
					/// <summary>Get or set the second row of the first column of the <see cref="Matrix3d"/>.</summary>
			[FieldOffset(24)]
			public Double YX;
					/// <summary>Get or set the second row of the second column of the <see cref="Matrix3d"/>.</summary>
			[FieldOffset(32)]
			public Double YY;
					/// <summary>Get or set the second row of the third column of the <see cref="Matrix3d"/>.</summary>
			[FieldOffset(40)]
			public Double YZ;
					/// <summary>Get or set the third row of the first column of the <see cref="Matrix3d"/>.</summary>
			[FieldOffset(48)]
			public Double ZX;
					/// <summary>Get or set the third row of the second column of the <see cref="Matrix3d"/>.</summary>
			[FieldOffset(56)]
			public Double ZY;
					/// <summary>Get or set the third row of the third column of the <see cref="Matrix3d"/>.</summary>
			[FieldOffset(64)]
			public Double ZZ;
				
		// VectorR [XYZW]Row
					/// <summary>Get or set the first row of the <see cref="Matrix3d"/>.</summary>
			public Vector3d XRow
			{
				get { return new Vector3d(XX, XY, XZ); }
				set { XX = value.X;XY = value.Y;XZ = value.Z; }
			}
					/// <summary>Get or set the second row of the <see cref="Matrix3d"/>.</summary>
			public Vector3d YRow
			{
				get { return new Vector3d(YX, YY, YZ); }
				set { YX = value.X;YY = value.Y;YZ = value.Z; }
			}
					/// <summary>Get or set the third row of the <see cref="Matrix3d"/>.</summary>
			public Vector3d ZRow
			{
				get { return new Vector3d(ZX, ZY, ZZ); }
				set { ZX = value.X;ZY = value.Y;ZZ = value.Z; }
			}
		
		// VectorC [XYZW]Column
					/// <summary>Get or set the first column of the <see cref="Matrix3d"/>.</summary>
			public Vector3d XColumn
			{
				get { return new Vector3d(XX, YX, ZX); }
				set { XX = value.X;YX = value.Y;ZX = value.Z; }
			}
					/// <summary>Get or set the second column of the <see cref="Matrix3d"/>.</summary>
			public Vector3d YColumn
			{
				get { return new Vector3d(XY, YY, ZY); }
				set { XY = value.X;YY = value.Y;ZY = value.Z; }
			}
					/// <summary>Get or set the third column of the <see cref="Matrix3d"/>.</summary>
			public Vector3d ZColumn
			{
				get { return new Vector3d(XZ, YZ, ZZ); }
				set { XZ = value.X;YZ = value.Y;ZZ = value.Z; }
			}
				
		/// <summary>Get or set the diagonal Vector3d of the <see cref="Matrix3d"/>.</summary>
		public Vector3d Diagonal
		{
			get { return new Vector3d(XX, YY, ZZ); }
			set { XX = value.X;YY = value.Y;ZZ = value.Z; }
		}

		// This(T [xyzw][xyzw]...)
		/// <summary>Assign the coefficients of the <see cref="Matrix3d"/> to the values provided. The coefficients are in row-major order, so the first coefficient is XX (first row of the first column) and the second coefficient is XY (first row of the <b>second</b> column).</summary>
					/// <param name="xx">The value to assign to the first row of the first column in field <see cref="XX"/>.</param>
					/// <param name="xy">The value to assign to the first row of the second column in field <see cref="XY"/>.</param>
					/// <param name="xz">The value to assign to the first row of the third column in field <see cref="XZ"/>.</param>
					/// <param name="yx">The value to assign to the second row of the first column in field <see cref="YX"/>.</param>
					/// <param name="yy">The value to assign to the second row of the second column in field <see cref="YY"/>.</param>
					/// <param name="yz">The value to assign to the second row of the third column in field <see cref="YZ"/>.</param>
					/// <param name="zx">The value to assign to the third row of the first column in field <see cref="ZX"/>.</param>
					/// <param name="zy">The value to assign to the third row of the second column in field <see cref="ZY"/>.</param>
					/// <param name="zz">The value to assign to the third row of the third column in field <see cref="ZZ"/>.</param>
				public Matrix3d(Double xx, Double xy, Double xz, Double yx, Double yy, Double yz, Double zx, Double zy, Double zz)
		{
			XX = xx;XY = xy;XZ = xz;YX = yx;YY = yy;YZ = yz;ZX = zx;ZY = zy;ZZ = zz;		}

		public Double this[int row, int column] {
			get {
									if(row == 0) {
													if(column == 0)
								return XX;
						 else 							if(column == 1)
								return XY;
						 else 							if(column == 2)
								return XZ;
												else
							throw new ArgumentOutOfRangeException("column");
					}
				 else 					if(row == 1) {
													if(column == 0)
								return YX;
						 else 							if(column == 1)
								return YY;
						 else 							if(column == 2)
								return YZ;
												else
							throw new ArgumentOutOfRangeException("column");
					}
				 else 					if(row == 2) {
													if(column == 0)
								return ZX;
						 else 							if(column == 1)
								return ZY;
						 else 							if(column == 2)
								return ZZ;
												else
							throw new ArgumentOutOfRangeException("column");
					}
								else
					throw new ArgumentOutOfRangeException("row");
			}

			set {
									if(row == 0) {
													if(column == 0)
								XX = value;
						 else 							if(column == 1)
								XY = value;
						 else 							if(column == 2)
								XZ = value;
												else
							throw new ArgumentOutOfRangeException("column");
					}
				 else 					if(row == 1) {
													if(column == 0)
								YX = value;
						 else 							if(column == 1)
								YY = value;
						 else 							if(column == 2)
								YZ = value;
												else
							throw new ArgumentOutOfRangeException("column");
					}
				 else 					if(row == 2) {
													if(column == 0)
								ZX = value;
						 else 							if(column == 1)
								ZY = value;
						 else 							if(column == 2)
								ZZ = value;
												else
							throw new ArgumentOutOfRangeException("column");
					}
								else
					throw new ArgumentOutOfRangeException("row");
			}
		}

		#region Methods

		/// <summary>Test for equality in all coefficients between this <see cref="Matrix3d"/> and the other one.</summary>
		public bool Equals(ref Matrix3d other)
		{
			return XX == other.XX && XY == other.XY && XZ == other.XZ && YX == other.YX && YY == other.YY && YZ == other.YZ && ZX == other.ZX && ZY == other.ZY && ZZ == other.ZZ;
		}

		/// <summary>Test for equality in all coefficients between this <see cref="Matrix3d"/> and the other one.</summary>
		public bool Equals(Matrix3d other) { return Equals(ref other); }

		/// <summary>Test for equality between this <see cref="Matrix3d"/> and the provided object, which must be an equivalent <see cref="Matrix3d"/> to return <c>true</c>.</summary>
		public override bool Equals(object other)
		{
			if(other is Matrix3d)
				return Equals((Matrix3d)other);
			return base.Equals(other);
		}

		/// <summary>Compute a hash code containing all of the <see cref="Matrix3d"/>'s coefficients.</summary>
		public override int GetHashCode()
		{
			return XX.GetHashCode() ^ XY.GetHashCode() ^ XZ.GetHashCode() ^ YX.GetHashCode() ^ YY.GetHashCode() ^ YZ.GetHashCode() ^ ZX.GetHashCode() ^ ZY.GetHashCode() ^ ZZ.GetHashCode();
		}

		// Multiply
					public Matrix3d Multiply(Matrix3d other )
			{
				Matrix3d result;
				Multiply(ref other, out result);
				return result;
			}

			public Matrix3d Multiply(ref Matrix3d other )
			{
				Matrix3d result;
																		Double vXX =   XX * other.XX  + XY * other.YX  + XZ * other.ZX ;
											Double vXY =   XX * other.XY  + XY * other.YY  + XZ * other.ZY ;
											Double vXZ =   XX * other.XZ  + XY * other.YZ  + XZ * other.ZZ ;
											Double vYX =   YX * other.XX  + YY * other.YX  + YZ * other.ZX ;
											Double vYY =   YX * other.XY  + YY * other.YY  + YZ * other.ZY ;
											Double vYZ =   YX * other.XZ  + YY * other.YZ  + YZ * other.ZZ ;
											Double vZX =   ZX * other.XX  + ZY * other.YX  + ZZ * other.ZX ;
											Double vZY =   ZX * other.XY  + ZY * other.YY  + ZZ * other.ZY ;
											Double vZZ =   ZX * other.XZ  + ZY * other.YZ  + ZZ * other.ZZ ;
															result.XX = vXX;
											result.XY = vXY;
											result.XZ = vXZ;
											result.YX = vYX;
											result.YY = vYY;
											result.YZ = vYZ;
											result.ZX = vZX;
											result.ZY = vZY;
											result.ZZ = vZZ;
									return result;
			}
					public void Multiply(Matrix3d other , out Matrix3d result)
			{
				
				Multiply(ref other, out result);
				return;
			}

			public void Multiply(ref Matrix3d other , out Matrix3d result)
			{
				
																		Double vXX =   XX * other.XX  + XY * other.YX  + XZ * other.ZX ;
											Double vXY =   XX * other.XY  + XY * other.YY  + XZ * other.ZY ;
											Double vXZ =   XX * other.XZ  + XY * other.YZ  + XZ * other.ZZ ;
											Double vYX =   YX * other.XX  + YY * other.YX  + YZ * other.ZX ;
											Double vYY =   YX * other.XY  + YY * other.YY  + YZ * other.ZY ;
											Double vYZ =   YX * other.XZ  + YY * other.YZ  + YZ * other.ZZ ;
											Double vZX =   ZX * other.XX  + ZY * other.YX  + ZZ * other.ZX ;
											Double vZY =   ZX * other.XY  + ZY * other.YY  + ZZ * other.ZY ;
											Double vZZ =   ZX * other.XZ  + ZY * other.YZ  + ZZ * other.ZZ ;
															result.XX = vXX;
											result.XY = vXY;
											result.XZ = vXZ;
											result.YX = vYX;
											result.YY = vYY;
											result.YZ = vYZ;
											result.ZX = vZX;
											result.ZY = vZY;
											result.ZZ = vZZ;
									return;
			}
		
		// Translate
					public static Matrix3d Translate(Vector2d amount ) { Matrix3d result; Translate(ref amount, out result); return result; }

			public static Matrix3d Translate(ref Vector2d amount ) 
			{
				Matrix3d result;
				 result.XX = 1;  result.XY = 0;  result.XZ = 0;  result.YX = 0;  result.YY = 1;  result.YZ = 0;  result.ZX = amount.X;  result.ZY = amount.Y;  result.ZZ = 1; 				return result;
			}

			public static Matrix3d Translate( Double X ,  Double Y  )
			{
				Matrix3d result;
				 result.XX = 1;  result.XY = 0;  result.XZ = 0;  result.YX = 0;  result.YY = 1;  result.YZ = 0;  result.ZX = X;  result.ZY = Y;  result.ZZ = 1; 				return result;
			}
					public static void Translate(Vector2d amount , out Matrix3d result) {  Translate(ref amount, out result); return; }

			public static void Translate(ref Vector2d amount , out Matrix3d result) 
			{
				
				 result.XX = 1;  result.XY = 0;  result.XZ = 0;  result.YX = 0;  result.YY = 1;  result.YZ = 0;  result.ZX = amount.X;  result.ZY = amount.Y;  result.ZZ = 1; 				return;
			}

			public static void Translate( Double X ,  Double Y  , out Matrix3d result)
			{
				
				 result.XX = 1;  result.XY = 0;  result.XZ = 0;  result.YX = 0;  result.YY = 1;  result.YZ = 0;  result.ZX = X;  result.ZY = Y;  result.ZZ = 1; 				return;
			}
		
		#region Square matrix methods

														public static Matrix3d Scale( Vector2d amount )
					{
						Matrix3d result;
													result.XX = amount.X;
													result.XY = 0;
													result.XZ = 0;
													result.YX = 0;
													result.YY = amount.Y;
													result.YZ = 0;
													result.ZX = 0;
													result.ZY = 0;
													result.ZZ = 1;
												return result;
					}

					public static Matrix3d Scale( Double x ,  Double y  )
					{
						Matrix3d result;
													result.XX = x;
													result.XY = 0;
													result.XZ = 0;
													result.YX = 0;
													result.YY = y;
													result.YZ = 0;
													result.ZX = 0;
													result.ZY = 0;
													result.ZZ = 1;
												return result;
					}
				
				public static Matrix3d Scale(Double amount )
				{
					Matrix3d result;
											result.XX = amount;
											result.XY = 0;
											result.XZ = 0;
											result.YX = 0;
											result.YY = amount;
											result.YZ = 0;
											result.ZX = 0;
											result.ZY = 0;
											result.ZZ = 1;
										return result;
				}
												public static void Scale( ref  Vector2d amount , out Matrix3d result)
					{
						
													result.XX = amount.X;
													result.XY = 0;
													result.XZ = 0;
													result.YX = 0;
													result.YY = amount.Y;
													result.YZ = 0;
													result.ZX = 0;
													result.ZY = 0;
													result.ZZ = 1;
												return;
					}

					public static void Scale( Double x ,  Double y  , out Matrix3d result)
					{
						
													result.XX = x;
													result.XY = 0;
													result.XZ = 0;
													result.YX = 0;
													result.YY = y;
													result.YZ = 0;
													result.ZX = 0;
													result.ZY = 0;
													result.ZZ = 1;
												return;
					}
				
				public static void Scale(Double amount , out Matrix3d result)
				{
					
											result.XX = amount;
											result.XY = 0;
											result.XZ = 0;
											result.YX = 0;
											result.YY = amount;
											result.YZ = 0;
											result.ZX = 0;
											result.ZY = 0;
											result.ZZ = 1;
										return;
				}
					
		#endregion Square matrix methods

		#region 4x4 Methods

		
		#endregion 4x4 methods

														public Vector2d Multiply( Vector2d vector ) {
						Vector2d result;
													result.X = 								XX * vector.X
							+								YX * vector.Y
							 + ZX;
													result.Y = 								XY * vector.X
							+								YY * vector.Y
							 + ZY;
												return result;
					}
									public Vector2d Multiply( ref  Vector2d vector ) {
						Vector2d result;
													result.X = 								XX * vector.X
							+								YX * vector.Y
							 + ZX;
													result.Y = 								XY * vector.X
							+								YY * vector.Y
							 + ZY;
												return result;
					}
																public void Multiply( Vector2d vector , out Vector2d result) {
						
													result.X = 								XX * vector.X
							+								YX * vector.Y
							 + ZX;
													result.Y = 								XY * vector.X
							+								YY * vector.Y
							 + ZY;
												return;
					}
									public void Multiply( ref  Vector2d vector , out Vector2d result) {
						
													result.X = 								XX * vector.X
							+								YX * vector.Y
							 + ZX;
													result.Y = 								XY * vector.X
							+								YY * vector.Y
							 + ZY;
												return;
					}
									
		#endregion Methods

		#region Operators

		/// <summary>Test whether the <see cref="Matrix3d"/> objects have equivalent coefficients.</summary>
		public static bool operator ==(Matrix3d a, Matrix3d b) { return a.Equals(ref b); }

		/// <summary>Test whether the <see cref="Matrix3d"/> objects do not have equivalent coefficients.</summary>
		public static bool operator !=(Matrix3d a, Matrix3d b) { return !a.Equals(ref b); }

					/// <summary>Cast to the <see cref="Matrix3f"/>.</summary>
			public static explicit operator Matrix3f(Matrix3d a)
			{
				return new Matrix3f((Single)a.XX, (Single)a.XY, (Single)a.XZ, (Single)a.YX, (Single)a.YY, (Single)a.YZ, (Single)a.ZX, (Single)a.ZY, (Single)a.ZZ);
			}
			
		public static Matrix3d operator *(Matrix3d a, Matrix3d b) { Matrix3d result; a.Multiply(ref b, out result); return result; }

					public static Vector2d operator *(Matrix3d a, Vector2d b) { Vector2d result; a.Multiply(ref b, out result); return result; }
		
		#endregion Operators
	}
	
	
	/// <summary>A four-dimensional matrix type using <see cref="Single"/> elements.</summary>
	[StructLayout(LayoutKind.Explicit)]
	public partial struct Matrix4f : IEquatable<Matrix4f>
	{
		/// <summary>Get a <see cref="Matrix4f"/> that would have no effect when multiplied against a <see cref="Matrix4f"/> or <see cref="Vector4f"/>.</summary>
		public static readonly Matrix4f Identity = new Matrix4f(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);

		// T [XYZW][XYZW]
					/// <summary>Get or set the first row of the first column of the <see cref="Matrix4f"/>.</summary>
			[FieldOffset(0)]
			public Single XX;
					/// <summary>Get or set the first row of the second column of the <see cref="Matrix4f"/>.</summary>
			[FieldOffset(4)]
			public Single XY;
					/// <summary>Get or set the first row of the third column of the <see cref="Matrix4f"/>.</summary>
			[FieldOffset(8)]
			public Single XZ;
					/// <summary>Get or set the first row of the fourth column of the <see cref="Matrix4f"/>.</summary>
			[FieldOffset(12)]
			public Single XW;
					/// <summary>Get or set the second row of the first column of the <see cref="Matrix4f"/>.</summary>
			[FieldOffset(16)]
			public Single YX;
					/// <summary>Get or set the second row of the second column of the <see cref="Matrix4f"/>.</summary>
			[FieldOffset(20)]
			public Single YY;
					/// <summary>Get or set the second row of the third column of the <see cref="Matrix4f"/>.</summary>
			[FieldOffset(24)]
			public Single YZ;
					/// <summary>Get or set the second row of the fourth column of the <see cref="Matrix4f"/>.</summary>
			[FieldOffset(28)]
			public Single YW;
					/// <summary>Get or set the third row of the first column of the <see cref="Matrix4f"/>.</summary>
			[FieldOffset(32)]
			public Single ZX;
					/// <summary>Get or set the third row of the second column of the <see cref="Matrix4f"/>.</summary>
			[FieldOffset(36)]
			public Single ZY;
					/// <summary>Get or set the third row of the third column of the <see cref="Matrix4f"/>.</summary>
			[FieldOffset(40)]
			public Single ZZ;
					/// <summary>Get or set the third row of the fourth column of the <see cref="Matrix4f"/>.</summary>
			[FieldOffset(44)]
			public Single ZW;
					/// <summary>Get or set the fourth row of the first column of the <see cref="Matrix4f"/>.</summary>
			[FieldOffset(48)]
			public Single WX;
					/// <summary>Get or set the fourth row of the second column of the <see cref="Matrix4f"/>.</summary>
			[FieldOffset(52)]
			public Single WY;
					/// <summary>Get or set the fourth row of the third column of the <see cref="Matrix4f"/>.</summary>
			[FieldOffset(56)]
			public Single WZ;
					/// <summary>Get or set the fourth row of the fourth column of the <see cref="Matrix4f"/>.</summary>
			[FieldOffset(60)]
			public Single WW;
				
		// VectorR [XYZW]Row
					/// <summary>Get or set the first row of the <see cref="Matrix4f"/>.</summary>
			public Vector4f XRow
			{
				get { return new Vector4f(XX, XY, XZ, XW); }
				set { XX = value.X;XY = value.Y;XZ = value.Z;XW = value.W; }
			}
					/// <summary>Get or set the second row of the <see cref="Matrix4f"/>.</summary>
			public Vector4f YRow
			{
				get { return new Vector4f(YX, YY, YZ, YW); }
				set { YX = value.X;YY = value.Y;YZ = value.Z;YW = value.W; }
			}
					/// <summary>Get or set the third row of the <see cref="Matrix4f"/>.</summary>
			public Vector4f ZRow
			{
				get { return new Vector4f(ZX, ZY, ZZ, ZW); }
				set { ZX = value.X;ZY = value.Y;ZZ = value.Z;ZW = value.W; }
			}
					/// <summary>Get or set the fourth row of the <see cref="Matrix4f"/>.</summary>
			public Vector4f WRow
			{
				get { return new Vector4f(WX, WY, WZ, WW); }
				set { WX = value.X;WY = value.Y;WZ = value.Z;WW = value.W; }
			}
		
		// VectorC [XYZW]Column
					/// <summary>Get or set the first column of the <see cref="Matrix4f"/>.</summary>
			public Vector4f XColumn
			{
				get { return new Vector4f(XX, YX, ZX, WX); }
				set { XX = value.X;YX = value.Y;ZX = value.Z;WX = value.W; }
			}
					/// <summary>Get or set the second column of the <see cref="Matrix4f"/>.</summary>
			public Vector4f YColumn
			{
				get { return new Vector4f(XY, YY, ZY, WY); }
				set { XY = value.X;YY = value.Y;ZY = value.Z;WY = value.W; }
			}
					/// <summary>Get or set the third column of the <see cref="Matrix4f"/>.</summary>
			public Vector4f ZColumn
			{
				get { return new Vector4f(XZ, YZ, ZZ, WZ); }
				set { XZ = value.X;YZ = value.Y;ZZ = value.Z;WZ = value.W; }
			}
					/// <summary>Get or set the fourth column of the <see cref="Matrix4f"/>.</summary>
			public Vector4f WColumn
			{
				get { return new Vector4f(XW, YW, ZW, WW); }
				set { XW = value.X;YW = value.Y;ZW = value.Z;WW = value.W; }
			}
				
		/// <summary>Get or set the diagonal Vector4f of the <see cref="Matrix4f"/>.</summary>
		public Vector4f Diagonal
		{
			get { return new Vector4f(XX, YY, ZZ, WW); }
			set { XX = value.X;YY = value.Y;ZZ = value.Z;WW = value.W; }
		}

		// This(T [xyzw][xyzw]...)
		/// <summary>Assign the coefficients of the <see cref="Matrix4f"/> to the values provided. The coefficients are in row-major order, so the first coefficient is XX (first row of the first column) and the second coefficient is XY (first row of the <b>second</b> column).</summary>
					/// <param name="xx">The value to assign to the first row of the first column in field <see cref="XX"/>.</param>
					/// <param name="xy">The value to assign to the first row of the second column in field <see cref="XY"/>.</param>
					/// <param name="xz">The value to assign to the first row of the third column in field <see cref="XZ"/>.</param>
					/// <param name="xw">The value to assign to the first row of the fourth column in field <see cref="XW"/>.</param>
					/// <param name="yx">The value to assign to the second row of the first column in field <see cref="YX"/>.</param>
					/// <param name="yy">The value to assign to the second row of the second column in field <see cref="YY"/>.</param>
					/// <param name="yz">The value to assign to the second row of the third column in field <see cref="YZ"/>.</param>
					/// <param name="yw">The value to assign to the second row of the fourth column in field <see cref="YW"/>.</param>
					/// <param name="zx">The value to assign to the third row of the first column in field <see cref="ZX"/>.</param>
					/// <param name="zy">The value to assign to the third row of the second column in field <see cref="ZY"/>.</param>
					/// <param name="zz">The value to assign to the third row of the third column in field <see cref="ZZ"/>.</param>
					/// <param name="zw">The value to assign to the third row of the fourth column in field <see cref="ZW"/>.</param>
					/// <param name="wx">The value to assign to the fourth row of the first column in field <see cref="WX"/>.</param>
					/// <param name="wy">The value to assign to the fourth row of the second column in field <see cref="WY"/>.</param>
					/// <param name="wz">The value to assign to the fourth row of the third column in field <see cref="WZ"/>.</param>
					/// <param name="ww">The value to assign to the fourth row of the fourth column in field <see cref="WW"/>.</param>
				public Matrix4f(Single xx, Single xy, Single xz, Single xw, Single yx, Single yy, Single yz, Single yw, Single zx, Single zy, Single zz, Single zw, Single wx, Single wy, Single wz, Single ww)
		{
			XX = xx;XY = xy;XZ = xz;XW = xw;YX = yx;YY = yy;YZ = yz;YW = yw;ZX = zx;ZY = zy;ZZ = zz;ZW = zw;WX = wx;WY = wy;WZ = wz;WW = ww;		}

		public Single this[int row, int column] {
			get {
									if(row == 0) {
													if(column == 0)
								return XX;
						 else 							if(column == 1)
								return XY;
						 else 							if(column == 2)
								return XZ;
						 else 							if(column == 3)
								return XW;
												else
							throw new ArgumentOutOfRangeException("column");
					}
				 else 					if(row == 1) {
													if(column == 0)
								return YX;
						 else 							if(column == 1)
								return YY;
						 else 							if(column == 2)
								return YZ;
						 else 							if(column == 3)
								return YW;
												else
							throw new ArgumentOutOfRangeException("column");
					}
				 else 					if(row == 2) {
													if(column == 0)
								return ZX;
						 else 							if(column == 1)
								return ZY;
						 else 							if(column == 2)
								return ZZ;
						 else 							if(column == 3)
								return ZW;
												else
							throw new ArgumentOutOfRangeException("column");
					}
				 else 					if(row == 3) {
													if(column == 0)
								return WX;
						 else 							if(column == 1)
								return WY;
						 else 							if(column == 2)
								return WZ;
						 else 							if(column == 3)
								return WW;
												else
							throw new ArgumentOutOfRangeException("column");
					}
								else
					throw new ArgumentOutOfRangeException("row");
			}

			set {
									if(row == 0) {
													if(column == 0)
								XX = value;
						 else 							if(column == 1)
								XY = value;
						 else 							if(column == 2)
								XZ = value;
						 else 							if(column == 3)
								XW = value;
												else
							throw new ArgumentOutOfRangeException("column");
					}
				 else 					if(row == 1) {
													if(column == 0)
								YX = value;
						 else 							if(column == 1)
								YY = value;
						 else 							if(column == 2)
								YZ = value;
						 else 							if(column == 3)
								YW = value;
												else
							throw new ArgumentOutOfRangeException("column");
					}
				 else 					if(row == 2) {
													if(column == 0)
								ZX = value;
						 else 							if(column == 1)
								ZY = value;
						 else 							if(column == 2)
								ZZ = value;
						 else 							if(column == 3)
								ZW = value;
												else
							throw new ArgumentOutOfRangeException("column");
					}
				 else 					if(row == 3) {
													if(column == 0)
								WX = value;
						 else 							if(column == 1)
								WY = value;
						 else 							if(column == 2)
								WZ = value;
						 else 							if(column == 3)
								WW = value;
												else
							throw new ArgumentOutOfRangeException("column");
					}
								else
					throw new ArgumentOutOfRangeException("row");
			}
		}

		#region Methods

		/// <summary>Test for equality in all coefficients between this <see cref="Matrix4f"/> and the other one.</summary>
		public bool Equals(ref Matrix4f other)
		{
			return XX == other.XX && XY == other.XY && XZ == other.XZ && XW == other.XW && YX == other.YX && YY == other.YY && YZ == other.YZ && YW == other.YW && ZX == other.ZX && ZY == other.ZY && ZZ == other.ZZ && ZW == other.ZW && WX == other.WX && WY == other.WY && WZ == other.WZ && WW == other.WW;
		}

		/// <summary>Test for equality in all coefficients between this <see cref="Matrix4f"/> and the other one.</summary>
		public bool Equals(Matrix4f other) { return Equals(ref other); }

		/// <summary>Test for equality between this <see cref="Matrix4f"/> and the provided object, which must be an equivalent <see cref="Matrix4f"/> to return <c>true</c>.</summary>
		public override bool Equals(object other)
		{
			if(other is Matrix4f)
				return Equals((Matrix4f)other);
			return base.Equals(other);
		}

		/// <summary>Compute a hash code containing all of the <see cref="Matrix4f"/>'s coefficients.</summary>
		public override int GetHashCode()
		{
			return XX.GetHashCode() ^ XY.GetHashCode() ^ XZ.GetHashCode() ^ XW.GetHashCode() ^ YX.GetHashCode() ^ YY.GetHashCode() ^ YZ.GetHashCode() ^ YW.GetHashCode() ^ ZX.GetHashCode() ^ ZY.GetHashCode() ^ ZZ.GetHashCode() ^ ZW.GetHashCode() ^ WX.GetHashCode() ^ WY.GetHashCode() ^ WZ.GetHashCode() ^ WW.GetHashCode();
		}

		// Multiply
					public Matrix4f Multiply(Matrix4f other )
			{
				Matrix4f result;
				Multiply(ref other, out result);
				return result;
			}

			public Matrix4f Multiply(ref Matrix4f other )
			{
				Matrix4f result;
																		Single vXX =   XX * other.XX  + XY * other.YX  + XZ * other.ZX  + XW * other.WX ;
											Single vXY =   XX * other.XY  + XY * other.YY  + XZ * other.ZY  + XW * other.WY ;
											Single vXZ =   XX * other.XZ  + XY * other.YZ  + XZ * other.ZZ  + XW * other.WZ ;
											Single vXW =   XX * other.XW  + XY * other.YW  + XZ * other.ZW  + XW * other.WW ;
											Single vYX =   YX * other.XX  + YY * other.YX  + YZ * other.ZX  + YW * other.WX ;
											Single vYY =   YX * other.XY  + YY * other.YY  + YZ * other.ZY  + YW * other.WY ;
											Single vYZ =   YX * other.XZ  + YY * other.YZ  + YZ * other.ZZ  + YW * other.WZ ;
											Single vYW =   YX * other.XW  + YY * other.YW  + YZ * other.ZW  + YW * other.WW ;
											Single vZX =   ZX * other.XX  + ZY * other.YX  + ZZ * other.ZX  + ZW * other.WX ;
											Single vZY =   ZX * other.XY  + ZY * other.YY  + ZZ * other.ZY  + ZW * other.WY ;
											Single vZZ =   ZX * other.XZ  + ZY * other.YZ  + ZZ * other.ZZ  + ZW * other.WZ ;
											Single vZW =   ZX * other.XW  + ZY * other.YW  + ZZ * other.ZW  + ZW * other.WW ;
											Single vWX =   WX * other.XX  + WY * other.YX  + WZ * other.ZX  + WW * other.WX ;
											Single vWY =   WX * other.XY  + WY * other.YY  + WZ * other.ZY  + WW * other.WY ;
											Single vWZ =   WX * other.XZ  + WY * other.YZ  + WZ * other.ZZ  + WW * other.WZ ;
											Single vWW =   WX * other.XW  + WY * other.YW  + WZ * other.ZW  + WW * other.WW ;
															result.XX = vXX;
											result.XY = vXY;
											result.XZ = vXZ;
											result.XW = vXW;
											result.YX = vYX;
											result.YY = vYY;
											result.YZ = vYZ;
											result.YW = vYW;
											result.ZX = vZX;
											result.ZY = vZY;
											result.ZZ = vZZ;
											result.ZW = vZW;
											result.WX = vWX;
											result.WY = vWY;
											result.WZ = vWZ;
											result.WW = vWW;
									return result;
			}
					public void Multiply(Matrix4f other , out Matrix4f result)
			{
				
				Multiply(ref other, out result);
				return;
			}

			public void Multiply(ref Matrix4f other , out Matrix4f result)
			{
				
																		Single vXX =   XX * other.XX  + XY * other.YX  + XZ * other.ZX  + XW * other.WX ;
											Single vXY =   XX * other.XY  + XY * other.YY  + XZ * other.ZY  + XW * other.WY ;
											Single vXZ =   XX * other.XZ  + XY * other.YZ  + XZ * other.ZZ  + XW * other.WZ ;
											Single vXW =   XX * other.XW  + XY * other.YW  + XZ * other.ZW  + XW * other.WW ;
											Single vYX =   YX * other.XX  + YY * other.YX  + YZ * other.ZX  + YW * other.WX ;
											Single vYY =   YX * other.XY  + YY * other.YY  + YZ * other.ZY  + YW * other.WY ;
											Single vYZ =   YX * other.XZ  + YY * other.YZ  + YZ * other.ZZ  + YW * other.WZ ;
											Single vYW =   YX * other.XW  + YY * other.YW  + YZ * other.ZW  + YW * other.WW ;
											Single vZX =   ZX * other.XX  + ZY * other.YX  + ZZ * other.ZX  + ZW * other.WX ;
											Single vZY =   ZX * other.XY  + ZY * other.YY  + ZZ * other.ZY  + ZW * other.WY ;
											Single vZZ =   ZX * other.XZ  + ZY * other.YZ  + ZZ * other.ZZ  + ZW * other.WZ ;
											Single vZW =   ZX * other.XW  + ZY * other.YW  + ZZ * other.ZW  + ZW * other.WW ;
											Single vWX =   WX * other.XX  + WY * other.YX  + WZ * other.ZX  + WW * other.WX ;
											Single vWY =   WX * other.XY  + WY * other.YY  + WZ * other.ZY  + WW * other.WY ;
											Single vWZ =   WX * other.XZ  + WY * other.YZ  + WZ * other.ZZ  + WW * other.WZ ;
											Single vWW =   WX * other.XW  + WY * other.YW  + WZ * other.ZW  + WW * other.WW ;
															result.XX = vXX;
											result.XY = vXY;
											result.XZ = vXZ;
											result.XW = vXW;
											result.YX = vYX;
											result.YY = vYY;
											result.YZ = vYZ;
											result.YW = vYW;
											result.ZX = vZX;
											result.ZY = vZY;
											result.ZZ = vZZ;
											result.ZW = vZW;
											result.WX = vWX;
											result.WY = vWY;
											result.WZ = vWZ;
											result.WW = vWW;
									return;
			}
		
		// Translate
					public static Matrix4f Translate(Vector3f amount ) { Matrix4f result; Translate(ref amount, out result); return result; }

			public static Matrix4f Translate(ref Vector3f amount ) 
			{
				Matrix4f result;
				 result.XX = 1;  result.XY = 0;  result.XZ = 0;  result.XW = 0;  result.YX = 0;  result.YY = 1;  result.YZ = 0;  result.YW = 0;  result.ZX = 0;  result.ZY = 0;  result.ZZ = 1;  result.ZW = 0;  result.WX = amount.X;  result.WY = amount.Y;  result.WZ = amount.Z;  result.WW = 1; 				return result;
			}

			public static Matrix4f Translate( Single X ,  Single Y ,  Single Z  )
			{
				Matrix4f result;
				 result.XX = 1;  result.XY = 0;  result.XZ = 0;  result.XW = 0;  result.YX = 0;  result.YY = 1;  result.YZ = 0;  result.YW = 0;  result.ZX = 0;  result.ZY = 0;  result.ZZ = 1;  result.ZW = 0;  result.WX = X;  result.WY = Y;  result.WZ = Z;  result.WW = 1; 				return result;
			}
					public static void Translate(Vector3f amount , out Matrix4f result) {  Translate(ref amount, out result); return; }

			public static void Translate(ref Vector3f amount , out Matrix4f result) 
			{
				
				 result.XX = 1;  result.XY = 0;  result.XZ = 0;  result.XW = 0;  result.YX = 0;  result.YY = 1;  result.YZ = 0;  result.YW = 0;  result.ZX = 0;  result.ZY = 0;  result.ZZ = 1;  result.ZW = 0;  result.WX = amount.X;  result.WY = amount.Y;  result.WZ = amount.Z;  result.WW = 1; 				return;
			}

			public static void Translate( Single X ,  Single Y ,  Single Z  , out Matrix4f result)
			{
				
				 result.XX = 1;  result.XY = 0;  result.XZ = 0;  result.XW = 0;  result.YX = 0;  result.YY = 1;  result.YZ = 0;  result.YW = 0;  result.ZX = 0;  result.ZY = 0;  result.ZZ = 1;  result.ZW = 0;  result.WX = X;  result.WY = Y;  result.WZ = Z;  result.WW = 1; 				return;
			}
		
		#region Square matrix methods

														public static Matrix4f Scale( Vector3f amount )
					{
						Matrix4f result;
													result.XX = amount.X;
													result.XY = 0;
													result.XZ = 0;
													result.XW = 0;
													result.YX = 0;
													result.YY = amount.Y;
													result.YZ = 0;
													result.YW = 0;
													result.ZX = 0;
													result.ZY = 0;
													result.ZZ = amount.Z;
													result.ZW = 0;
													result.WX = 0;
													result.WY = 0;
													result.WZ = 0;
													result.WW = 1;
												return result;
					}

					public static Matrix4f Scale( Single x ,  Single y ,  Single z  )
					{
						Matrix4f result;
													result.XX = x;
													result.XY = 0;
													result.XZ = 0;
													result.XW = 0;
													result.YX = 0;
													result.YY = y;
													result.YZ = 0;
													result.YW = 0;
													result.ZX = 0;
													result.ZY = 0;
													result.ZZ = z;
													result.ZW = 0;
													result.WX = 0;
													result.WY = 0;
													result.WZ = 0;
													result.WW = 1;
												return result;
					}
				
				public static Matrix4f Scale(Single amount )
				{
					Matrix4f result;
											result.XX = amount;
											result.XY = 0;
											result.XZ = 0;
											result.XW = 0;
											result.YX = 0;
											result.YY = amount;
											result.YZ = 0;
											result.YW = 0;
											result.ZX = 0;
											result.ZY = 0;
											result.ZZ = amount;
											result.ZW = 0;
											result.WX = 0;
											result.WY = 0;
											result.WZ = 0;
											result.WW = 1;
										return result;
				}
												public static void Scale( ref  Vector3f amount , out Matrix4f result)
					{
						
													result.XX = amount.X;
													result.XY = 0;
													result.XZ = 0;
													result.XW = 0;
													result.YX = 0;
													result.YY = amount.Y;
													result.YZ = 0;
													result.YW = 0;
													result.ZX = 0;
													result.ZY = 0;
													result.ZZ = amount.Z;
													result.ZW = 0;
													result.WX = 0;
													result.WY = 0;
													result.WZ = 0;
													result.WW = 1;
												return;
					}

					public static void Scale( Single x ,  Single y ,  Single z  , out Matrix4f result)
					{
						
													result.XX = x;
													result.XY = 0;
													result.XZ = 0;
													result.XW = 0;
													result.YX = 0;
													result.YY = y;
													result.YZ = 0;
													result.YW = 0;
													result.ZX = 0;
													result.ZY = 0;
													result.ZZ = z;
													result.ZW = 0;
													result.WX = 0;
													result.WY = 0;
													result.WZ = 0;
													result.WW = 1;
												return;
					}
				
				public static void Scale(Single amount , out Matrix4f result)
				{
					
											result.XX = amount;
											result.XY = 0;
											result.XZ = 0;
											result.XW = 0;
											result.YX = 0;
											result.YY = amount;
											result.YZ = 0;
											result.YW = 0;
											result.ZX = 0;
											result.ZY = 0;
											result.ZZ = amount;
											result.ZW = 0;
											result.WX = 0;
											result.WY = 0;
											result.WZ = 0;
											result.WW = 1;
										return;
				}
					
		#endregion Square matrix methods

		#region 4x4 Methods

														/// <summary>Create a view matrix that transforms a camera and turns it to look towards a specific location.</summary>
					/// <param name="cameraPosition">The position of the camera.</param>
					/// <param name="cameraTarget">The target to turn the camera towards.</param>
					/// <param name="cameraUpVector">The direction that is "up" from the camera's point of view. For example, <see cref="Vector3f.UnitY"/> would have the vertical aspect of the camera straight down the Y axis.</param>
					public static Matrix4f LookAt( Vector3f cameraPosition,  Vector3f cameraTarget,  Vector3f cameraUpVector )
					{
						Matrix4f result;
						Vector3f vector = (cameraPosition - cameraTarget).Normalized;
						Vector3f vector2 = cameraUpVector.Cross(vector).Normalized;
						Vector3f vector3 = vector.Cross(vector2);
						result.XX = vector2.X;
						result.XY = vector3.X;
						result.XZ = vector.X;
						result.XW = 0;
						result.YX = vector2.Y;
						result.YY = vector3.Y;
						result.YZ = vector.Y;
						result.YW = 0;
						result.ZX = vector2.Z;
						result.ZY = vector3.Z;
						result.ZZ = vector.Z;
						result.ZW = 0;
						result.WX = -vector2.Dot(cameraPosition);
						result.WY = -vector3.Dot(cameraPosition);
						result.WZ = -vector.Dot(cameraPosition);
						result.WW = 1;
						return result;
					}

					public static Matrix4f Rotate( Angle3 angle ) { Matrix4f result; Rotate(angle.X, angle.Y, angle.Z, out result); return result; }
									/// <summary>Create a view matrix that transforms a camera and turns it to look towards a specific location.</summary>
					/// <param name="cameraPosition">The position of the camera.</param>
					/// <param name="cameraTarget">The target to turn the camera towards.</param>
					/// <param name="cameraUpVector">The direction that is "up" from the camera's point of view. For example, <see cref="Vector3f.UnitY"/> would have the vertical aspect of the camera straight down the Y axis.</param>
					public static Matrix4f LookAt( ref  Vector3f cameraPosition,  ref  Vector3f cameraTarget,  ref  Vector3f cameraUpVector )
					{
						Matrix4f result;
						Vector3f vector = (cameraPosition - cameraTarget).Normalized;
						Vector3f vector2 = cameraUpVector.Cross(vector).Normalized;
						Vector3f vector3 = vector.Cross(vector2);
						result.XX = vector2.X;
						result.XY = vector3.X;
						result.XZ = vector.X;
						result.XW = 0;
						result.YX = vector2.Y;
						result.YY = vector3.Y;
						result.YZ = vector.Y;
						result.YW = 0;
						result.ZX = vector2.Z;
						result.ZY = vector3.Z;
						result.ZZ = vector.Z;
						result.ZW = 0;
						result.WX = -vector2.Dot(cameraPosition);
						result.WY = -vector3.Dot(cameraPosition);
						result.WZ = -vector.Dot(cameraPosition);
						result.WW = 1;
						return result;
					}

					public static Matrix4f Rotate( ref  Angle3 angle ) { Matrix4f result; Rotate(angle.X, angle.Y, angle.Z, out result); return result; }
				
				/// <summary>Create a perspective matrix with a field of view.</summary>
				/// <param name="fieldOfView">The vertical field of view. This must be greater than zero and below 180 degrees.</param>
				/// <param name="aspectRatio">The aspect ratio.</param>
				/// <param name="nearPlaneDistance">The nearest distance that will be visible; any object before this distance will be clipped. Ideally <paramref name="farPlaneDistance"/> / <paramref name="nearPlaneDistance"/> will be kept as low as possible in order to maximise the fidelity of the depth buffer. This may not be greater than <paramref name="farPlaneDistance"/> or negative.</param>
				/// <param name="farPlaneDistance">The farthest distance that will be visible; any object after this distance will be clipped. This may not be less than or equal to <paramref name="nearPlaneDistance"/>.</param>
				public static Matrix4f PerspectiveFieldOfView(Angle fieldOfView, Single aspectRatio, Single nearPlaneDistance, Single farPlaneDistance ) {
					Matrix4f result;
					if(fieldOfView < Angle.Zero || fieldOfView >= Angle.Flip)
						throw new ArgumentOutOfRangeException("fieldOfView");
					if(nearPlaneDistance <= 0)
						throw new ArgumentOutOfRangeException("nearPlaneDistance");
					if(farPlaneDistance <= 0)
						throw new ArgumentOutOfRangeException("farPlaneDistance");
					if(nearPlaneDistance >= farPlaneDistance)
						throw new ArgumentOutOfRangeException("nearPlaneDistance");
						result = Identity;
					double fov = 1.0 / Math.Tan(fieldOfView.InRadians * 0.5);
					double fovByAspectRatio = fov / aspectRatio;
					result.XX = (Single)fovByAspectRatio;
					result.XY = result.XZ = result.XW = 0;
					result.YY = (Single)fov;
					result.YX = result.YZ = result.YW = 0;
					result.ZX = result.ZY = 0;
					result.ZZ = (Single)(farPlaneDistance / (nearPlaneDistance - farPlaneDistance));
					result.ZW = -1;
					result.WX = result.WY = result.WW = 0;
					result.WZ = (Single)((nearPlaneDistance * farPlaneDistance) / (nearPlaneDistance - farPlaneDistance));
					return result;
				}

				public static Matrix4f Rotate(Angle yaw, Angle pitch, Angle roll ) {
					Matrix4f result;
					Quaternion quaternion = new Quaternion(yaw, pitch, roll);
					quaternion.ToMatrix4f(out result);
					return result;
				}
												/// <summary>Create a view matrix that transforms a camera and turns it to look towards a specific location.</summary>
					/// <param name="cameraPosition">The position of the camera.</param>
					/// <param name="cameraTarget">The target to turn the camera towards.</param>
					/// <param name="cameraUpVector">The direction that is "up" from the camera's point of view. For example, <see cref="Vector3f.UnitY"/> would have the vertical aspect of the camera straight down the Y axis.</param>
					public static void LookAt( Vector3f cameraPosition,  Vector3f cameraTarget,  Vector3f cameraUpVector , out Matrix4f result)
					{
						
						Vector3f vector = (cameraPosition - cameraTarget).Normalized;
						Vector3f vector2 = cameraUpVector.Cross(vector).Normalized;
						Vector3f vector3 = vector.Cross(vector2);
						result.XX = vector2.X;
						result.XY = vector3.X;
						result.XZ = vector.X;
						result.XW = 0;
						result.YX = vector2.Y;
						result.YY = vector3.Y;
						result.YZ = vector.Y;
						result.YW = 0;
						result.ZX = vector2.Z;
						result.ZY = vector3.Z;
						result.ZZ = vector.Z;
						result.ZW = 0;
						result.WX = -vector2.Dot(cameraPosition);
						result.WY = -vector3.Dot(cameraPosition);
						result.WZ = -vector.Dot(cameraPosition);
						result.WW = 1;
						return;
					}

					public static void Rotate( Angle3 angle , out Matrix4f result) {  Rotate(angle.X, angle.Y, angle.Z, out result); return; }
									/// <summary>Create a view matrix that transforms a camera and turns it to look towards a specific location.</summary>
					/// <param name="cameraPosition">The position of the camera.</param>
					/// <param name="cameraTarget">The target to turn the camera towards.</param>
					/// <param name="cameraUpVector">The direction that is "up" from the camera's point of view. For example, <see cref="Vector3f.UnitY"/> would have the vertical aspect of the camera straight down the Y axis.</param>
					public static void LookAt( ref  Vector3f cameraPosition,  ref  Vector3f cameraTarget,  ref  Vector3f cameraUpVector , out Matrix4f result)
					{
						
						Vector3f vector = (cameraPosition - cameraTarget).Normalized;
						Vector3f vector2 = cameraUpVector.Cross(vector).Normalized;
						Vector3f vector3 = vector.Cross(vector2);
						result.XX = vector2.X;
						result.XY = vector3.X;
						result.XZ = vector.X;
						result.XW = 0;
						result.YX = vector2.Y;
						result.YY = vector3.Y;
						result.YZ = vector.Y;
						result.YW = 0;
						result.ZX = vector2.Z;
						result.ZY = vector3.Z;
						result.ZZ = vector.Z;
						result.ZW = 0;
						result.WX = -vector2.Dot(cameraPosition);
						result.WY = -vector3.Dot(cameraPosition);
						result.WZ = -vector.Dot(cameraPosition);
						result.WW = 1;
						return;
					}

					public static void Rotate( ref  Angle3 angle , out Matrix4f result) {  Rotate(angle.X, angle.Y, angle.Z, out result); return; }
				
				/// <summary>Create a perspective matrix with a field of view.</summary>
				/// <param name="fieldOfView">The vertical field of view. This must be greater than zero and below 180 degrees.</param>
				/// <param name="aspectRatio">The aspect ratio.</param>
				/// <param name="nearPlaneDistance">The nearest distance that will be visible; any object before this distance will be clipped. Ideally <paramref name="farPlaneDistance"/> / <paramref name="nearPlaneDistance"/> will be kept as low as possible in order to maximise the fidelity of the depth buffer. This may not be greater than <paramref name="farPlaneDistance"/> or negative.</param>
				/// <param name="farPlaneDistance">The farthest distance that will be visible; any object after this distance will be clipped. This may not be less than or equal to <paramref name="nearPlaneDistance"/>.</param>
				public static void PerspectiveFieldOfView(Angle fieldOfView, Single aspectRatio, Single nearPlaneDistance, Single farPlaneDistance , out Matrix4f result) {
					
					if(fieldOfView < Angle.Zero || fieldOfView >= Angle.Flip)
						throw new ArgumentOutOfRangeException("fieldOfView");
					if(nearPlaneDistance <= 0)
						throw new ArgumentOutOfRangeException("nearPlaneDistance");
					if(farPlaneDistance <= 0)
						throw new ArgumentOutOfRangeException("farPlaneDistance");
					if(nearPlaneDistance >= farPlaneDistance)
						throw new ArgumentOutOfRangeException("nearPlaneDistance");
						result = Identity;
					double fov = 1.0 / Math.Tan(fieldOfView.InRadians * 0.5);
					double fovByAspectRatio = fov / aspectRatio;
					result.XX = (Single)fovByAspectRatio;
					result.XY = result.XZ = result.XW = 0;
					result.YY = (Single)fov;
					result.YX = result.YZ = result.YW = 0;
					result.ZX = result.ZY = 0;
					result.ZZ = (Single)(farPlaneDistance / (nearPlaneDistance - farPlaneDistance));
					result.ZW = -1;
					result.WX = result.WY = result.WW = 0;
					result.WZ = (Single)((nearPlaneDistance * farPlaneDistance) / (nearPlaneDistance - farPlaneDistance));
					return;
				}

				public static void Rotate(Angle yaw, Angle pitch, Angle roll , out Matrix4f result) {
					
					Quaternion quaternion = new Quaternion(yaw, pitch, roll);
					quaternion.ToMatrix4f(out result);
					return;
				}
					
		#endregion 4x4 methods

														public Vector3f Multiply( Vector3f vector ) {
						Vector3f result;
													result.X = 								XX * vector.X
							+								YX * vector.Y
							+								ZX * vector.Z
							 + WX;
													result.Y = 								XY * vector.X
							+								YY * vector.Y
							+								ZY * vector.Z
							 + WY;
													result.Z = 								XZ * vector.X
							+								YZ * vector.Y
							+								ZZ * vector.Z
							 + WZ;
												return result;
					}
									public Vector3f Multiply( ref  Vector3f vector ) {
						Vector3f result;
													result.X = 								XX * vector.X
							+								YX * vector.Y
							+								ZX * vector.Z
							 + WX;
													result.Y = 								XY * vector.X
							+								YY * vector.Y
							+								ZY * vector.Z
							 + WY;
													result.Z = 								XZ * vector.X
							+								YZ * vector.Y
							+								ZZ * vector.Z
							 + WZ;
												return result;
					}
																public void Multiply( Vector3f vector , out Vector3f result) {
						
													result.X = 								XX * vector.X
							+								YX * vector.Y
							+								ZX * vector.Z
							 + WX;
													result.Y = 								XY * vector.X
							+								YY * vector.Y
							+								ZY * vector.Z
							 + WY;
													result.Z = 								XZ * vector.X
							+								YZ * vector.Y
							+								ZZ * vector.Z
							 + WZ;
												return;
					}
									public void Multiply( ref  Vector3f vector , out Vector3f result) {
						
													result.X = 								XX * vector.X
							+								YX * vector.Y
							+								ZX * vector.Z
							 + WX;
													result.Y = 								XY * vector.X
							+								YY * vector.Y
							+								ZY * vector.Z
							 + WY;
													result.Z = 								XZ * vector.X
							+								YZ * vector.Y
							+								ZZ * vector.Z
							 + WZ;
												return;
					}
									
		#endregion Methods

		#region Operators

		/// <summary>Test whether the <see cref="Matrix4f"/> objects have equivalent coefficients.</summary>
		public static bool operator ==(Matrix4f a, Matrix4f b) { return a.Equals(ref b); }

		/// <summary>Test whether the <see cref="Matrix4f"/> objects do not have equivalent coefficients.</summary>
		public static bool operator !=(Matrix4f a, Matrix4f b) { return !a.Equals(ref b); }

					/// <summary>Cast to the <see cref="Matrix4d"/>.</summary>
			public static explicit operator Matrix4d(Matrix4f a)
			{
				return new Matrix4d((Double)a.XX, (Double)a.XY, (Double)a.XZ, (Double)a.XW, (Double)a.YX, (Double)a.YY, (Double)a.YZ, (Double)a.YW, (Double)a.ZX, (Double)a.ZY, (Double)a.ZZ, (Double)a.ZW, (Double)a.WX, (Double)a.WY, (Double)a.WZ, (Double)a.WW);
			}
			
		public static Matrix4f operator *(Matrix4f a, Matrix4f b) { Matrix4f result; a.Multiply(ref b, out result); return result; }

					public static Vector3f operator *(Matrix4f a, Vector3f b) { Vector3f result; a.Multiply(ref b, out result); return result; }
		
		#endregion Operators
	}
	
	
	/// <summary>A four-dimensional matrix type using <see cref="Double"/> elements.</summary>
	[StructLayout(LayoutKind.Explicit)]
	public partial struct Matrix4d : IEquatable<Matrix4d>
	{
		/// <summary>Get a <see cref="Matrix4d"/> that would have no effect when multiplied against a <see cref="Matrix4d"/> or <see cref="Vector4d"/>.</summary>
		public static readonly Matrix4d Identity = new Matrix4d(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);

		// T [XYZW][XYZW]
					/// <summary>Get or set the first row of the first column of the <see cref="Matrix4d"/>.</summary>
			[FieldOffset(0)]
			public Double XX;
					/// <summary>Get or set the first row of the second column of the <see cref="Matrix4d"/>.</summary>
			[FieldOffset(8)]
			public Double XY;
					/// <summary>Get or set the first row of the third column of the <see cref="Matrix4d"/>.</summary>
			[FieldOffset(16)]
			public Double XZ;
					/// <summary>Get or set the first row of the fourth column of the <see cref="Matrix4d"/>.</summary>
			[FieldOffset(24)]
			public Double XW;
					/// <summary>Get or set the second row of the first column of the <see cref="Matrix4d"/>.</summary>
			[FieldOffset(32)]
			public Double YX;
					/// <summary>Get or set the second row of the second column of the <see cref="Matrix4d"/>.</summary>
			[FieldOffset(40)]
			public Double YY;
					/// <summary>Get or set the second row of the third column of the <see cref="Matrix4d"/>.</summary>
			[FieldOffset(48)]
			public Double YZ;
					/// <summary>Get or set the second row of the fourth column of the <see cref="Matrix4d"/>.</summary>
			[FieldOffset(56)]
			public Double YW;
					/// <summary>Get or set the third row of the first column of the <see cref="Matrix4d"/>.</summary>
			[FieldOffset(64)]
			public Double ZX;
					/// <summary>Get or set the third row of the second column of the <see cref="Matrix4d"/>.</summary>
			[FieldOffset(72)]
			public Double ZY;
					/// <summary>Get or set the third row of the third column of the <see cref="Matrix4d"/>.</summary>
			[FieldOffset(80)]
			public Double ZZ;
					/// <summary>Get or set the third row of the fourth column of the <see cref="Matrix4d"/>.</summary>
			[FieldOffset(88)]
			public Double ZW;
					/// <summary>Get or set the fourth row of the first column of the <see cref="Matrix4d"/>.</summary>
			[FieldOffset(96)]
			public Double WX;
					/// <summary>Get or set the fourth row of the second column of the <see cref="Matrix4d"/>.</summary>
			[FieldOffset(104)]
			public Double WY;
					/// <summary>Get or set the fourth row of the third column of the <see cref="Matrix4d"/>.</summary>
			[FieldOffset(112)]
			public Double WZ;
					/// <summary>Get or set the fourth row of the fourth column of the <see cref="Matrix4d"/>.</summary>
			[FieldOffset(120)]
			public Double WW;
				
		// VectorR [XYZW]Row
					/// <summary>Get or set the first row of the <see cref="Matrix4d"/>.</summary>
			public Vector4d XRow
			{
				get { return new Vector4d(XX, XY, XZ, XW); }
				set { XX = value.X;XY = value.Y;XZ = value.Z;XW = value.W; }
			}
					/// <summary>Get or set the second row of the <see cref="Matrix4d"/>.</summary>
			public Vector4d YRow
			{
				get { return new Vector4d(YX, YY, YZ, YW); }
				set { YX = value.X;YY = value.Y;YZ = value.Z;YW = value.W; }
			}
					/// <summary>Get or set the third row of the <see cref="Matrix4d"/>.</summary>
			public Vector4d ZRow
			{
				get { return new Vector4d(ZX, ZY, ZZ, ZW); }
				set { ZX = value.X;ZY = value.Y;ZZ = value.Z;ZW = value.W; }
			}
					/// <summary>Get or set the fourth row of the <see cref="Matrix4d"/>.</summary>
			public Vector4d WRow
			{
				get { return new Vector4d(WX, WY, WZ, WW); }
				set { WX = value.X;WY = value.Y;WZ = value.Z;WW = value.W; }
			}
		
		// VectorC [XYZW]Column
					/// <summary>Get or set the first column of the <see cref="Matrix4d"/>.</summary>
			public Vector4d XColumn
			{
				get { return new Vector4d(XX, YX, ZX, WX); }
				set { XX = value.X;YX = value.Y;ZX = value.Z;WX = value.W; }
			}
					/// <summary>Get or set the second column of the <see cref="Matrix4d"/>.</summary>
			public Vector4d YColumn
			{
				get { return new Vector4d(XY, YY, ZY, WY); }
				set { XY = value.X;YY = value.Y;ZY = value.Z;WY = value.W; }
			}
					/// <summary>Get or set the third column of the <see cref="Matrix4d"/>.</summary>
			public Vector4d ZColumn
			{
				get { return new Vector4d(XZ, YZ, ZZ, WZ); }
				set { XZ = value.X;YZ = value.Y;ZZ = value.Z;WZ = value.W; }
			}
					/// <summary>Get or set the fourth column of the <see cref="Matrix4d"/>.</summary>
			public Vector4d WColumn
			{
				get { return new Vector4d(XW, YW, ZW, WW); }
				set { XW = value.X;YW = value.Y;ZW = value.Z;WW = value.W; }
			}
				
		/// <summary>Get or set the diagonal Vector4d of the <see cref="Matrix4d"/>.</summary>
		public Vector4d Diagonal
		{
			get { return new Vector4d(XX, YY, ZZ, WW); }
			set { XX = value.X;YY = value.Y;ZZ = value.Z;WW = value.W; }
		}

		// This(T [xyzw][xyzw]...)
		/// <summary>Assign the coefficients of the <see cref="Matrix4d"/> to the values provided. The coefficients are in row-major order, so the first coefficient is XX (first row of the first column) and the second coefficient is XY (first row of the <b>second</b> column).</summary>
					/// <param name="xx">The value to assign to the first row of the first column in field <see cref="XX"/>.</param>
					/// <param name="xy">The value to assign to the first row of the second column in field <see cref="XY"/>.</param>
					/// <param name="xz">The value to assign to the first row of the third column in field <see cref="XZ"/>.</param>
					/// <param name="xw">The value to assign to the first row of the fourth column in field <see cref="XW"/>.</param>
					/// <param name="yx">The value to assign to the second row of the first column in field <see cref="YX"/>.</param>
					/// <param name="yy">The value to assign to the second row of the second column in field <see cref="YY"/>.</param>
					/// <param name="yz">The value to assign to the second row of the third column in field <see cref="YZ"/>.</param>
					/// <param name="yw">The value to assign to the second row of the fourth column in field <see cref="YW"/>.</param>
					/// <param name="zx">The value to assign to the third row of the first column in field <see cref="ZX"/>.</param>
					/// <param name="zy">The value to assign to the third row of the second column in field <see cref="ZY"/>.</param>
					/// <param name="zz">The value to assign to the third row of the third column in field <see cref="ZZ"/>.</param>
					/// <param name="zw">The value to assign to the third row of the fourth column in field <see cref="ZW"/>.</param>
					/// <param name="wx">The value to assign to the fourth row of the first column in field <see cref="WX"/>.</param>
					/// <param name="wy">The value to assign to the fourth row of the second column in field <see cref="WY"/>.</param>
					/// <param name="wz">The value to assign to the fourth row of the third column in field <see cref="WZ"/>.</param>
					/// <param name="ww">The value to assign to the fourth row of the fourth column in field <see cref="WW"/>.</param>
				public Matrix4d(Double xx, Double xy, Double xz, Double xw, Double yx, Double yy, Double yz, Double yw, Double zx, Double zy, Double zz, Double zw, Double wx, Double wy, Double wz, Double ww)
		{
			XX = xx;XY = xy;XZ = xz;XW = xw;YX = yx;YY = yy;YZ = yz;YW = yw;ZX = zx;ZY = zy;ZZ = zz;ZW = zw;WX = wx;WY = wy;WZ = wz;WW = ww;		}

		public Double this[int row, int column] {
			get {
									if(row == 0) {
													if(column == 0)
								return XX;
						 else 							if(column == 1)
								return XY;
						 else 							if(column == 2)
								return XZ;
						 else 							if(column == 3)
								return XW;
												else
							throw new ArgumentOutOfRangeException("column");
					}
				 else 					if(row == 1) {
													if(column == 0)
								return YX;
						 else 							if(column == 1)
								return YY;
						 else 							if(column == 2)
								return YZ;
						 else 							if(column == 3)
								return YW;
												else
							throw new ArgumentOutOfRangeException("column");
					}
				 else 					if(row == 2) {
													if(column == 0)
								return ZX;
						 else 							if(column == 1)
								return ZY;
						 else 							if(column == 2)
								return ZZ;
						 else 							if(column == 3)
								return ZW;
												else
							throw new ArgumentOutOfRangeException("column");
					}
				 else 					if(row == 3) {
													if(column == 0)
								return WX;
						 else 							if(column == 1)
								return WY;
						 else 							if(column == 2)
								return WZ;
						 else 							if(column == 3)
								return WW;
												else
							throw new ArgumentOutOfRangeException("column");
					}
								else
					throw new ArgumentOutOfRangeException("row");
			}

			set {
									if(row == 0) {
													if(column == 0)
								XX = value;
						 else 							if(column == 1)
								XY = value;
						 else 							if(column == 2)
								XZ = value;
						 else 							if(column == 3)
								XW = value;
												else
							throw new ArgumentOutOfRangeException("column");
					}
				 else 					if(row == 1) {
													if(column == 0)
								YX = value;
						 else 							if(column == 1)
								YY = value;
						 else 							if(column == 2)
								YZ = value;
						 else 							if(column == 3)
								YW = value;
												else
							throw new ArgumentOutOfRangeException("column");
					}
				 else 					if(row == 2) {
													if(column == 0)
								ZX = value;
						 else 							if(column == 1)
								ZY = value;
						 else 							if(column == 2)
								ZZ = value;
						 else 							if(column == 3)
								ZW = value;
												else
							throw new ArgumentOutOfRangeException("column");
					}
				 else 					if(row == 3) {
													if(column == 0)
								WX = value;
						 else 							if(column == 1)
								WY = value;
						 else 							if(column == 2)
								WZ = value;
						 else 							if(column == 3)
								WW = value;
												else
							throw new ArgumentOutOfRangeException("column");
					}
								else
					throw new ArgumentOutOfRangeException("row");
			}
		}

		#region Methods

		/// <summary>Test for equality in all coefficients between this <see cref="Matrix4d"/> and the other one.</summary>
		public bool Equals(ref Matrix4d other)
		{
			return XX == other.XX && XY == other.XY && XZ == other.XZ && XW == other.XW && YX == other.YX && YY == other.YY && YZ == other.YZ && YW == other.YW && ZX == other.ZX && ZY == other.ZY && ZZ == other.ZZ && ZW == other.ZW && WX == other.WX && WY == other.WY && WZ == other.WZ && WW == other.WW;
		}

		/// <summary>Test for equality in all coefficients between this <see cref="Matrix4d"/> and the other one.</summary>
		public bool Equals(Matrix4d other) { return Equals(ref other); }

		/// <summary>Test for equality between this <see cref="Matrix4d"/> and the provided object, which must be an equivalent <see cref="Matrix4d"/> to return <c>true</c>.</summary>
		public override bool Equals(object other)
		{
			if(other is Matrix4d)
				return Equals((Matrix4d)other);
			return base.Equals(other);
		}

		/// <summary>Compute a hash code containing all of the <see cref="Matrix4d"/>'s coefficients.</summary>
		public override int GetHashCode()
		{
			return XX.GetHashCode() ^ XY.GetHashCode() ^ XZ.GetHashCode() ^ XW.GetHashCode() ^ YX.GetHashCode() ^ YY.GetHashCode() ^ YZ.GetHashCode() ^ YW.GetHashCode() ^ ZX.GetHashCode() ^ ZY.GetHashCode() ^ ZZ.GetHashCode() ^ ZW.GetHashCode() ^ WX.GetHashCode() ^ WY.GetHashCode() ^ WZ.GetHashCode() ^ WW.GetHashCode();
		}

		// Multiply
					public Matrix4d Multiply(Matrix4d other )
			{
				Matrix4d result;
				Multiply(ref other, out result);
				return result;
			}

			public Matrix4d Multiply(ref Matrix4d other )
			{
				Matrix4d result;
																		Double vXX =   XX * other.XX  + XY * other.YX  + XZ * other.ZX  + XW * other.WX ;
											Double vXY =   XX * other.XY  + XY * other.YY  + XZ * other.ZY  + XW * other.WY ;
											Double vXZ =   XX * other.XZ  + XY * other.YZ  + XZ * other.ZZ  + XW * other.WZ ;
											Double vXW =   XX * other.XW  + XY * other.YW  + XZ * other.ZW  + XW * other.WW ;
											Double vYX =   YX * other.XX  + YY * other.YX  + YZ * other.ZX  + YW * other.WX ;
											Double vYY =   YX * other.XY  + YY * other.YY  + YZ * other.ZY  + YW * other.WY ;
											Double vYZ =   YX * other.XZ  + YY * other.YZ  + YZ * other.ZZ  + YW * other.WZ ;
											Double vYW =   YX * other.XW  + YY * other.YW  + YZ * other.ZW  + YW * other.WW ;
											Double vZX =   ZX * other.XX  + ZY * other.YX  + ZZ * other.ZX  + ZW * other.WX ;
											Double vZY =   ZX * other.XY  + ZY * other.YY  + ZZ * other.ZY  + ZW * other.WY ;
											Double vZZ =   ZX * other.XZ  + ZY * other.YZ  + ZZ * other.ZZ  + ZW * other.WZ ;
											Double vZW =   ZX * other.XW  + ZY * other.YW  + ZZ * other.ZW  + ZW * other.WW ;
											Double vWX =   WX * other.XX  + WY * other.YX  + WZ * other.ZX  + WW * other.WX ;
											Double vWY =   WX * other.XY  + WY * other.YY  + WZ * other.ZY  + WW * other.WY ;
											Double vWZ =   WX * other.XZ  + WY * other.YZ  + WZ * other.ZZ  + WW * other.WZ ;
											Double vWW =   WX * other.XW  + WY * other.YW  + WZ * other.ZW  + WW * other.WW ;
															result.XX = vXX;
											result.XY = vXY;
											result.XZ = vXZ;
											result.XW = vXW;
											result.YX = vYX;
											result.YY = vYY;
											result.YZ = vYZ;
											result.YW = vYW;
											result.ZX = vZX;
											result.ZY = vZY;
											result.ZZ = vZZ;
											result.ZW = vZW;
											result.WX = vWX;
											result.WY = vWY;
											result.WZ = vWZ;
											result.WW = vWW;
									return result;
			}
					public void Multiply(Matrix4d other , out Matrix4d result)
			{
				
				Multiply(ref other, out result);
				return;
			}

			public void Multiply(ref Matrix4d other , out Matrix4d result)
			{
				
																		Double vXX =   XX * other.XX  + XY * other.YX  + XZ * other.ZX  + XW * other.WX ;
											Double vXY =   XX * other.XY  + XY * other.YY  + XZ * other.ZY  + XW * other.WY ;
											Double vXZ =   XX * other.XZ  + XY * other.YZ  + XZ * other.ZZ  + XW * other.WZ ;
											Double vXW =   XX * other.XW  + XY * other.YW  + XZ * other.ZW  + XW * other.WW ;
											Double vYX =   YX * other.XX  + YY * other.YX  + YZ * other.ZX  + YW * other.WX ;
											Double vYY =   YX * other.XY  + YY * other.YY  + YZ * other.ZY  + YW * other.WY ;
											Double vYZ =   YX * other.XZ  + YY * other.YZ  + YZ * other.ZZ  + YW * other.WZ ;
											Double vYW =   YX * other.XW  + YY * other.YW  + YZ * other.ZW  + YW * other.WW ;
											Double vZX =   ZX * other.XX  + ZY * other.YX  + ZZ * other.ZX  + ZW * other.WX ;
											Double vZY =   ZX * other.XY  + ZY * other.YY  + ZZ * other.ZY  + ZW * other.WY ;
											Double vZZ =   ZX * other.XZ  + ZY * other.YZ  + ZZ * other.ZZ  + ZW * other.WZ ;
											Double vZW =   ZX * other.XW  + ZY * other.YW  + ZZ * other.ZW  + ZW * other.WW ;
											Double vWX =   WX * other.XX  + WY * other.YX  + WZ * other.ZX  + WW * other.WX ;
											Double vWY =   WX * other.XY  + WY * other.YY  + WZ * other.ZY  + WW * other.WY ;
											Double vWZ =   WX * other.XZ  + WY * other.YZ  + WZ * other.ZZ  + WW * other.WZ ;
											Double vWW =   WX * other.XW  + WY * other.YW  + WZ * other.ZW  + WW * other.WW ;
															result.XX = vXX;
											result.XY = vXY;
											result.XZ = vXZ;
											result.XW = vXW;
											result.YX = vYX;
											result.YY = vYY;
											result.YZ = vYZ;
											result.YW = vYW;
											result.ZX = vZX;
											result.ZY = vZY;
											result.ZZ = vZZ;
											result.ZW = vZW;
											result.WX = vWX;
											result.WY = vWY;
											result.WZ = vWZ;
											result.WW = vWW;
									return;
			}
		
		// Translate
					public static Matrix4d Translate(Vector3d amount ) { Matrix4d result; Translate(ref amount, out result); return result; }

			public static Matrix4d Translate(ref Vector3d amount ) 
			{
				Matrix4d result;
				 result.XX = 1;  result.XY = 0;  result.XZ = 0;  result.XW = 0;  result.YX = 0;  result.YY = 1;  result.YZ = 0;  result.YW = 0;  result.ZX = 0;  result.ZY = 0;  result.ZZ = 1;  result.ZW = 0;  result.WX = amount.X;  result.WY = amount.Y;  result.WZ = amount.Z;  result.WW = 1; 				return result;
			}

			public static Matrix4d Translate( Double X ,  Double Y ,  Double Z  )
			{
				Matrix4d result;
				 result.XX = 1;  result.XY = 0;  result.XZ = 0;  result.XW = 0;  result.YX = 0;  result.YY = 1;  result.YZ = 0;  result.YW = 0;  result.ZX = 0;  result.ZY = 0;  result.ZZ = 1;  result.ZW = 0;  result.WX = X;  result.WY = Y;  result.WZ = Z;  result.WW = 1; 				return result;
			}
					public static void Translate(Vector3d amount , out Matrix4d result) {  Translate(ref amount, out result); return; }

			public static void Translate(ref Vector3d amount , out Matrix4d result) 
			{
				
				 result.XX = 1;  result.XY = 0;  result.XZ = 0;  result.XW = 0;  result.YX = 0;  result.YY = 1;  result.YZ = 0;  result.YW = 0;  result.ZX = 0;  result.ZY = 0;  result.ZZ = 1;  result.ZW = 0;  result.WX = amount.X;  result.WY = amount.Y;  result.WZ = amount.Z;  result.WW = 1; 				return;
			}

			public static void Translate( Double X ,  Double Y ,  Double Z  , out Matrix4d result)
			{
				
				 result.XX = 1;  result.XY = 0;  result.XZ = 0;  result.XW = 0;  result.YX = 0;  result.YY = 1;  result.YZ = 0;  result.YW = 0;  result.ZX = 0;  result.ZY = 0;  result.ZZ = 1;  result.ZW = 0;  result.WX = X;  result.WY = Y;  result.WZ = Z;  result.WW = 1; 				return;
			}
		
		#region Square matrix methods

														public static Matrix4d Scale( Vector3d amount )
					{
						Matrix4d result;
													result.XX = amount.X;
													result.XY = 0;
													result.XZ = 0;
													result.XW = 0;
													result.YX = 0;
													result.YY = amount.Y;
													result.YZ = 0;
													result.YW = 0;
													result.ZX = 0;
													result.ZY = 0;
													result.ZZ = amount.Z;
													result.ZW = 0;
													result.WX = 0;
													result.WY = 0;
													result.WZ = 0;
													result.WW = 1;
												return result;
					}

					public static Matrix4d Scale( Double x ,  Double y ,  Double z  )
					{
						Matrix4d result;
													result.XX = x;
													result.XY = 0;
													result.XZ = 0;
													result.XW = 0;
													result.YX = 0;
													result.YY = y;
													result.YZ = 0;
													result.YW = 0;
													result.ZX = 0;
													result.ZY = 0;
													result.ZZ = z;
													result.ZW = 0;
													result.WX = 0;
													result.WY = 0;
													result.WZ = 0;
													result.WW = 1;
												return result;
					}
				
				public static Matrix4d Scale(Double amount )
				{
					Matrix4d result;
											result.XX = amount;
											result.XY = 0;
											result.XZ = 0;
											result.XW = 0;
											result.YX = 0;
											result.YY = amount;
											result.YZ = 0;
											result.YW = 0;
											result.ZX = 0;
											result.ZY = 0;
											result.ZZ = amount;
											result.ZW = 0;
											result.WX = 0;
											result.WY = 0;
											result.WZ = 0;
											result.WW = 1;
										return result;
				}
												public static void Scale( ref  Vector3d amount , out Matrix4d result)
					{
						
													result.XX = amount.X;
													result.XY = 0;
													result.XZ = 0;
													result.XW = 0;
													result.YX = 0;
													result.YY = amount.Y;
													result.YZ = 0;
													result.YW = 0;
													result.ZX = 0;
													result.ZY = 0;
													result.ZZ = amount.Z;
													result.ZW = 0;
													result.WX = 0;
													result.WY = 0;
													result.WZ = 0;
													result.WW = 1;
												return;
					}

					public static void Scale( Double x ,  Double y ,  Double z  , out Matrix4d result)
					{
						
													result.XX = x;
													result.XY = 0;
													result.XZ = 0;
													result.XW = 0;
													result.YX = 0;
													result.YY = y;
													result.YZ = 0;
													result.YW = 0;
													result.ZX = 0;
													result.ZY = 0;
													result.ZZ = z;
													result.ZW = 0;
													result.WX = 0;
													result.WY = 0;
													result.WZ = 0;
													result.WW = 1;
												return;
					}
				
				public static void Scale(Double amount , out Matrix4d result)
				{
					
											result.XX = amount;
											result.XY = 0;
											result.XZ = 0;
											result.XW = 0;
											result.YX = 0;
											result.YY = amount;
											result.YZ = 0;
											result.YW = 0;
											result.ZX = 0;
											result.ZY = 0;
											result.ZZ = amount;
											result.ZW = 0;
											result.WX = 0;
											result.WY = 0;
											result.WZ = 0;
											result.WW = 1;
										return;
				}
					
		#endregion Square matrix methods

		#region 4x4 Methods

														/// <summary>Create a view matrix that transforms a camera and turns it to look towards a specific location.</summary>
					/// <param name="cameraPosition">The position of the camera.</param>
					/// <param name="cameraTarget">The target to turn the camera towards.</param>
					/// <param name="cameraUpVector">The direction that is "up" from the camera's point of view. For example, <see cref="Vector3d.UnitY"/> would have the vertical aspect of the camera straight down the Y axis.</param>
					public static Matrix4d LookAt( Vector3d cameraPosition,  Vector3d cameraTarget,  Vector3d cameraUpVector )
					{
						Matrix4d result;
						Vector3d vector = (cameraPosition - cameraTarget).Normalized;
						Vector3d vector2 = cameraUpVector.Cross(vector).Normalized;
						Vector3d vector3 = vector.Cross(vector2);
						result.XX = vector2.X;
						result.XY = vector3.X;
						result.XZ = vector.X;
						result.XW = 0;
						result.YX = vector2.Y;
						result.YY = vector3.Y;
						result.YZ = vector.Y;
						result.YW = 0;
						result.ZX = vector2.Z;
						result.ZY = vector3.Z;
						result.ZZ = vector.Z;
						result.ZW = 0;
						result.WX = -vector2.Dot(cameraPosition);
						result.WY = -vector3.Dot(cameraPosition);
						result.WZ = -vector.Dot(cameraPosition);
						result.WW = 1;
						return result;
					}

					public static Matrix4d Rotate( Angle3 angle ) { Matrix4d result; Rotate(angle.X, angle.Y, angle.Z, out result); return result; }
									/// <summary>Create a view matrix that transforms a camera and turns it to look towards a specific location.</summary>
					/// <param name="cameraPosition">The position of the camera.</param>
					/// <param name="cameraTarget">The target to turn the camera towards.</param>
					/// <param name="cameraUpVector">The direction that is "up" from the camera's point of view. For example, <see cref="Vector3d.UnitY"/> would have the vertical aspect of the camera straight down the Y axis.</param>
					public static Matrix4d LookAt( ref  Vector3d cameraPosition,  ref  Vector3d cameraTarget,  ref  Vector3d cameraUpVector )
					{
						Matrix4d result;
						Vector3d vector = (cameraPosition - cameraTarget).Normalized;
						Vector3d vector2 = cameraUpVector.Cross(vector).Normalized;
						Vector3d vector3 = vector.Cross(vector2);
						result.XX = vector2.X;
						result.XY = vector3.X;
						result.XZ = vector.X;
						result.XW = 0;
						result.YX = vector2.Y;
						result.YY = vector3.Y;
						result.YZ = vector.Y;
						result.YW = 0;
						result.ZX = vector2.Z;
						result.ZY = vector3.Z;
						result.ZZ = vector.Z;
						result.ZW = 0;
						result.WX = -vector2.Dot(cameraPosition);
						result.WY = -vector3.Dot(cameraPosition);
						result.WZ = -vector.Dot(cameraPosition);
						result.WW = 1;
						return result;
					}

					public static Matrix4d Rotate( ref  Angle3 angle ) { Matrix4d result; Rotate(angle.X, angle.Y, angle.Z, out result); return result; }
				
				/// <summary>Create a perspective matrix with a field of view.</summary>
				/// <param name="fieldOfView">The vertical field of view. This must be greater than zero and below 180 degrees.</param>
				/// <param name="aspectRatio">The aspect ratio.</param>
				/// <param name="nearPlaneDistance">The nearest distance that will be visible; any object before this distance will be clipped. Ideally <paramref name="farPlaneDistance"/> / <paramref name="nearPlaneDistance"/> will be kept as low as possible in order to maximise the fidelity of the depth buffer. This may not be greater than <paramref name="farPlaneDistance"/> or negative.</param>
				/// <param name="farPlaneDistance">The farthest distance that will be visible; any object after this distance will be clipped. This may not be less than or equal to <paramref name="nearPlaneDistance"/>.</param>
				public static Matrix4d PerspectiveFieldOfView(Angle fieldOfView, Double aspectRatio, Double nearPlaneDistance, Double farPlaneDistance ) {
					Matrix4d result;
					if(fieldOfView < Angle.Zero || fieldOfView >= Angle.Flip)
						throw new ArgumentOutOfRangeException("fieldOfView");
					if(nearPlaneDistance <= 0)
						throw new ArgumentOutOfRangeException("nearPlaneDistance");
					if(farPlaneDistance <= 0)
						throw new ArgumentOutOfRangeException("farPlaneDistance");
					if(nearPlaneDistance >= farPlaneDistance)
						throw new ArgumentOutOfRangeException("nearPlaneDistance");
						result = Identity;
					double fov = 1.0 / Math.Tan(fieldOfView.InRadians * 0.5);
					double fovByAspectRatio = fov / aspectRatio;
					result.XX = (Double)fovByAspectRatio;
					result.XY = result.XZ = result.XW = 0;
					result.YY = (Double)fov;
					result.YX = result.YZ = result.YW = 0;
					result.ZX = result.ZY = 0;
					result.ZZ = (Double)(farPlaneDistance / (nearPlaneDistance - farPlaneDistance));
					result.ZW = -1;
					result.WX = result.WY = result.WW = 0;
					result.WZ = (Double)((nearPlaneDistance * farPlaneDistance) / (nearPlaneDistance - farPlaneDistance));
					return result;
				}

				public static Matrix4d Rotate(Angle yaw, Angle pitch, Angle roll ) {
					Matrix4d result;
					Quaternion quaternion = new Quaternion(yaw, pitch, roll);
					quaternion.ToMatrix4d(out result);
					return result;
				}
												/// <summary>Create a view matrix that transforms a camera and turns it to look towards a specific location.</summary>
					/// <param name="cameraPosition">The position of the camera.</param>
					/// <param name="cameraTarget">The target to turn the camera towards.</param>
					/// <param name="cameraUpVector">The direction that is "up" from the camera's point of view. For example, <see cref="Vector3d.UnitY"/> would have the vertical aspect of the camera straight down the Y axis.</param>
					public static void LookAt( Vector3d cameraPosition,  Vector3d cameraTarget,  Vector3d cameraUpVector , out Matrix4d result)
					{
						
						Vector3d vector = (cameraPosition - cameraTarget).Normalized;
						Vector3d vector2 = cameraUpVector.Cross(vector).Normalized;
						Vector3d vector3 = vector.Cross(vector2);
						result.XX = vector2.X;
						result.XY = vector3.X;
						result.XZ = vector.X;
						result.XW = 0;
						result.YX = vector2.Y;
						result.YY = vector3.Y;
						result.YZ = vector.Y;
						result.YW = 0;
						result.ZX = vector2.Z;
						result.ZY = vector3.Z;
						result.ZZ = vector.Z;
						result.ZW = 0;
						result.WX = -vector2.Dot(cameraPosition);
						result.WY = -vector3.Dot(cameraPosition);
						result.WZ = -vector.Dot(cameraPosition);
						result.WW = 1;
						return;
					}

					public static void Rotate( Angle3 angle , out Matrix4d result) {  Rotate(angle.X, angle.Y, angle.Z, out result); return; }
									/// <summary>Create a view matrix that transforms a camera and turns it to look towards a specific location.</summary>
					/// <param name="cameraPosition">The position of the camera.</param>
					/// <param name="cameraTarget">The target to turn the camera towards.</param>
					/// <param name="cameraUpVector">The direction that is "up" from the camera's point of view. For example, <see cref="Vector3d.UnitY"/> would have the vertical aspect of the camera straight down the Y axis.</param>
					public static void LookAt( ref  Vector3d cameraPosition,  ref  Vector3d cameraTarget,  ref  Vector3d cameraUpVector , out Matrix4d result)
					{
						
						Vector3d vector = (cameraPosition - cameraTarget).Normalized;
						Vector3d vector2 = cameraUpVector.Cross(vector).Normalized;
						Vector3d vector3 = vector.Cross(vector2);
						result.XX = vector2.X;
						result.XY = vector3.X;
						result.XZ = vector.X;
						result.XW = 0;
						result.YX = vector2.Y;
						result.YY = vector3.Y;
						result.YZ = vector.Y;
						result.YW = 0;
						result.ZX = vector2.Z;
						result.ZY = vector3.Z;
						result.ZZ = vector.Z;
						result.ZW = 0;
						result.WX = -vector2.Dot(cameraPosition);
						result.WY = -vector3.Dot(cameraPosition);
						result.WZ = -vector.Dot(cameraPosition);
						result.WW = 1;
						return;
					}

					public static void Rotate( ref  Angle3 angle , out Matrix4d result) {  Rotate(angle.X, angle.Y, angle.Z, out result); return; }
				
				/// <summary>Create a perspective matrix with a field of view.</summary>
				/// <param name="fieldOfView">The vertical field of view. This must be greater than zero and below 180 degrees.</param>
				/// <param name="aspectRatio">The aspect ratio.</param>
				/// <param name="nearPlaneDistance">The nearest distance that will be visible; any object before this distance will be clipped. Ideally <paramref name="farPlaneDistance"/> / <paramref name="nearPlaneDistance"/> will be kept as low as possible in order to maximise the fidelity of the depth buffer. This may not be greater than <paramref name="farPlaneDistance"/> or negative.</param>
				/// <param name="farPlaneDistance">The farthest distance that will be visible; any object after this distance will be clipped. This may not be less than or equal to <paramref name="nearPlaneDistance"/>.</param>
				public static void PerspectiveFieldOfView(Angle fieldOfView, Double aspectRatio, Double nearPlaneDistance, Double farPlaneDistance , out Matrix4d result) {
					
					if(fieldOfView < Angle.Zero || fieldOfView >= Angle.Flip)
						throw new ArgumentOutOfRangeException("fieldOfView");
					if(nearPlaneDistance <= 0)
						throw new ArgumentOutOfRangeException("nearPlaneDistance");
					if(farPlaneDistance <= 0)
						throw new ArgumentOutOfRangeException("farPlaneDistance");
					if(nearPlaneDistance >= farPlaneDistance)
						throw new ArgumentOutOfRangeException("nearPlaneDistance");
						result = Identity;
					double fov = 1.0 / Math.Tan(fieldOfView.InRadians * 0.5);
					double fovByAspectRatio = fov / aspectRatio;
					result.XX = (Double)fovByAspectRatio;
					result.XY = result.XZ = result.XW = 0;
					result.YY = (Double)fov;
					result.YX = result.YZ = result.YW = 0;
					result.ZX = result.ZY = 0;
					result.ZZ = (Double)(farPlaneDistance / (nearPlaneDistance - farPlaneDistance));
					result.ZW = -1;
					result.WX = result.WY = result.WW = 0;
					result.WZ = (Double)((nearPlaneDistance * farPlaneDistance) / (nearPlaneDistance - farPlaneDistance));
					return;
				}

				public static void Rotate(Angle yaw, Angle pitch, Angle roll , out Matrix4d result) {
					
					Quaternion quaternion = new Quaternion(yaw, pitch, roll);
					quaternion.ToMatrix4d(out result);
					return;
				}
					
		#endregion 4x4 methods

														public Vector3d Multiply( Vector3d vector ) {
						Vector3d result;
													result.X = 								XX * vector.X
							+								YX * vector.Y
							+								ZX * vector.Z
							 + WX;
													result.Y = 								XY * vector.X
							+								YY * vector.Y
							+								ZY * vector.Z
							 + WY;
													result.Z = 								XZ * vector.X
							+								YZ * vector.Y
							+								ZZ * vector.Z
							 + WZ;
												return result;
					}
									public Vector3d Multiply( ref  Vector3d vector ) {
						Vector3d result;
													result.X = 								XX * vector.X
							+								YX * vector.Y
							+								ZX * vector.Z
							 + WX;
													result.Y = 								XY * vector.X
							+								YY * vector.Y
							+								ZY * vector.Z
							 + WY;
													result.Z = 								XZ * vector.X
							+								YZ * vector.Y
							+								ZZ * vector.Z
							 + WZ;
												return result;
					}
																public void Multiply( Vector3d vector , out Vector3d result) {
						
													result.X = 								XX * vector.X
							+								YX * vector.Y
							+								ZX * vector.Z
							 + WX;
													result.Y = 								XY * vector.X
							+								YY * vector.Y
							+								ZY * vector.Z
							 + WY;
													result.Z = 								XZ * vector.X
							+								YZ * vector.Y
							+								ZZ * vector.Z
							 + WZ;
												return;
					}
									public void Multiply( ref  Vector3d vector , out Vector3d result) {
						
													result.X = 								XX * vector.X
							+								YX * vector.Y
							+								ZX * vector.Z
							 + WX;
													result.Y = 								XY * vector.X
							+								YY * vector.Y
							+								ZY * vector.Z
							 + WY;
													result.Z = 								XZ * vector.X
							+								YZ * vector.Y
							+								ZZ * vector.Z
							 + WZ;
												return;
					}
									
		#endregion Methods

		#region Operators

		/// <summary>Test whether the <see cref="Matrix4d"/> objects have equivalent coefficients.</summary>
		public static bool operator ==(Matrix4d a, Matrix4d b) { return a.Equals(ref b); }

		/// <summary>Test whether the <see cref="Matrix4d"/> objects do not have equivalent coefficients.</summary>
		public static bool operator !=(Matrix4d a, Matrix4d b) { return !a.Equals(ref b); }

					/// <summary>Cast to the <see cref="Matrix4f"/>.</summary>
			public static explicit operator Matrix4f(Matrix4d a)
			{
				return new Matrix4f((Single)a.XX, (Single)a.XY, (Single)a.XZ, (Single)a.XW, (Single)a.YX, (Single)a.YY, (Single)a.YZ, (Single)a.YW, (Single)a.ZX, (Single)a.ZY, (Single)a.ZZ, (Single)a.ZW, (Single)a.WX, (Single)a.WY, (Single)a.WZ, (Single)a.WW);
			}
			
		public static Matrix4d operator *(Matrix4d a, Matrix4d b) { Matrix4d result; a.Multiply(ref b, out result); return result; }

					public static Vector3d operator *(Matrix4d a, Vector3d b) { Vector3d result; a.Multiply(ref b, out result); return result; }
		
		#endregion Operators
	}
	
	}








