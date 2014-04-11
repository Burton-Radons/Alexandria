using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare
{
		/// <summary>A two-dimensional box that uses float elements.</summary>
	public struct Box2f
	{
		/// <summary>Get whether the <see cref="Min"/> and <see cref="Max"/> values are the same.</summary>
		public bool IsEmpty { get { return  Min.X == Max.X || Min.Y == Max.Y ; } }

		/// <summary>The minimum extents of the <see cref="Box2f"/>.</summary>
		public Vector2f Min;

		/// <summary>The maximum extents of the <see cref="Box2f"/>.</summary>
		public Vector2f Max;

		public Vector2f Center { get { return new Vector2f( (Max.X + Min.X) / 2 ,  (Max.Y + Min.Y) / 2 ); } }
		
		public Vector2f Centre { get { return new Vector2f( (Max.X + Min.X) / 2 ,  (Max.Y + Min.Y) / 2 ); } }

		public Vector2f Size { get { return new Vector2f( Max.X - Min.X ,  Max.Y - Min.Y ); } }

		public static readonly Box2f Zero = new Box2f((float)0, (float)0, (float)0, (float)0);

					public Box2f( Vector2f min,  Vector2f max) { this.Min = min; this.Max = max; }
					public Box2f( ref  Vector2f min,  ref  Vector2f max) { this.Min = min; this.Max = max; }
		
		public Box2f(
			 float minX ,  float minY ,
			 float maxX ,  float maxY ) {
							Min.X = minX;
				Max.X = maxX;
							Min.Y = minY;
				Max.Y = maxY;
					}

									public static Box2f Relative( Vector2f min ,  Vector2f size  ) {
					Box2f result;
											result.Min.X = min.X;
						result.Max.X = min.X + size.X;
											result.Min.Y = min.Y;
						result.Max.Y = min.Y + size.Y;
										return result;
				}
							public static Box2f Relative( ref Vector2f min ,  ref Vector2f size  ) {
					Box2f result;
											result.Min.X = min.X;
						result.Max.X = min.X + size.X;
											result.Min.Y = min.Y;
						result.Max.Y = min.Y + size.Y;
										return result;
				}
							public static Box2f Relative(  float minX ,  float minY ,   float sizeX ,  float sizeY  ) {
					Box2f result;
											result.Min.X = minX;
						result.Max.X = minX + sizeX;
											result.Min.Y = minY;
						result.Max.Y = minY + sizeY;
										return result;
				}
												public static void Relative( Vector2f min ,  Vector2f size  , out Box2f result) {
					
											result.Min.X = min.X;
						result.Max.X = min.X + size.X;
											result.Min.Y = min.Y;
						result.Max.Y = min.Y + size.Y;
										return;
				}
							public static void Relative( ref Vector2f min ,  ref Vector2f size  , out Box2f result) {
					
											result.Min.X = min.X;
						result.Max.X = min.X + size.X;
											result.Min.Y = min.Y;
						result.Max.Y = min.Y + size.Y;
										return;
				}
							public static void Relative(  float minX ,  float minY ,   float sizeX ,  float sizeY  , out Box2f result) {
					
											result.Min.X = minX;
						result.Max.X = minX + sizeX;
											result.Min.Y = minY;
						result.Max.Y = minY + sizeY;
										return;
				}
					
					/// <summary>Get a random position within the box.</summary>
			public Vector2f Random(Random rng ) {
				Vector2f result;
									result.X = (float)(rng.NextDouble() * (Max.X - Min.X) + Min.X);
									result.Y = (float)(rng.NextDouble() * (Max.Y - Min.Y) + Min.Y);
								return result;
			}
					/// <summary>Get a random position within the box.</summary>
			public void Random(Random rng , out Vector2f result) {
				
									result.X = (float)(rng.NextDouble() * (Max.X - Min.X) + Min.X);
									result.Y = (float)(rng.NextDouble() * (Max.Y - Min.Y) + Min.Y);
								return;
			}
		
					/// <summary>Get the closest distance between this <see cref="Box2f"/> and the <see cref="Vector2f"/>.</summary>
			public float Distance( Vector2f point) {
				Vector2f nearest;
				NearestPointTo(ref point, out nearest);
				return point.Distance(ref nearest);
			}

			public Containment Intersect( Vector2f point) {
				// Most points should be outside, so check that first.
				if ( point.X < Min.X || point.X > Max.X || point.Y < Min.Y || point.Y > Max.Y )
					return Containment.Disjoint;
				// Now check for boundaries, which will usually be cut short on the first axis.
				if ( (point.X == Min.X || point.X == Max.X) && (point.Y == Min.Y || point.Y == Max.Y) )
					return Containment.Intersects;
				return Containment.Contains;
			}

			/// <summary>Get whether this <see cref="Box2f"/> inclusively intersects with the <see cref="Vector2f"/>.</summary>
			public bool Overlaps( Vector2f point) {
				return  point.X >= Min.X && point.X <= Max.X && point.Y >= Min.Y && point.Y <= Max.Y ;
			}
					/// <summary>Get the closest distance between this <see cref="Box2f"/> and the <see cref="Vector2f"/>.</summary>
			public float Distance( ref  Vector2f point) {
				Vector2f nearest;
				NearestPointTo(ref point, out nearest);
				return point.Distance(ref nearest);
			}

			public Containment Intersect( ref  Vector2f point) {
				// Most points should be outside, so check that first.
				if ( point.X < Min.X || point.X > Max.X || point.Y < Min.Y || point.Y > Max.Y )
					return Containment.Disjoint;
				// Now check for boundaries, which will usually be cut short on the first axis.
				if ( (point.X == Min.X || point.X == Max.X) && (point.Y == Min.Y || point.Y == Max.Y) )
					return Containment.Intersects;
				return Containment.Contains;
			}

			/// <summary>Get whether this <see cref="Box2f"/> inclusively intersects with the <see cref="Vector2f"/>.</summary>
			public bool Overlaps( ref  Vector2f point) {
				return  point.X >= Min.X && point.X <= Max.X && point.Y >= Min.Y && point.Y <= Max.Y ;
			}
		
		
					public Vector2f NearestPointTo( Vector2f point ) {
				Vector2f result;
				Containment containment = Intersect(ref point);
				if(containment != Containment.Disjoint)
					result = point;
				else
					point.Clamp(ref Min, ref Max, out result);
				return result;
			}
					public void NearestPointTo( ref  Vector2f point , out Vector2f result) {
				
				Containment containment = Intersect(ref point);
				if(containment != Containment.Disjoint)
					result = point;
				else
					point.Clamp(ref Min, ref Max, out result);
				return;
			}
		
	}

		/// <summary>A two-dimensional box that uses double elements.</summary>
	public struct Box2d
	{
		/// <summary>Get whether the <see cref="Min"/> and <see cref="Max"/> values are the same.</summary>
		public bool IsEmpty { get { return  Min.X == Max.X || Min.Y == Max.Y ; } }

		/// <summary>The minimum extents of the <see cref="Box2d"/>.</summary>
		public Vector2d Min;

		/// <summary>The maximum extents of the <see cref="Box2d"/>.</summary>
		public Vector2d Max;

		public Vector2d Center { get { return new Vector2d( (Max.X + Min.X) / 2 ,  (Max.Y + Min.Y) / 2 ); } }
		
		public Vector2d Centre { get { return new Vector2d( (Max.X + Min.X) / 2 ,  (Max.Y + Min.Y) / 2 ); } }

		public Vector2d Size { get { return new Vector2d( Max.X - Min.X ,  Max.Y - Min.Y ); } }

		public static readonly Box2d Zero = new Box2d((double)0, (double)0, (double)0, (double)0);

					public Box2d( Vector2d min,  Vector2d max) { this.Min = min; this.Max = max; }
					public Box2d( ref  Vector2d min,  ref  Vector2d max) { this.Min = min; this.Max = max; }
		
		public Box2d(
			 double minX ,  double minY ,
			 double maxX ,  double maxY ) {
							Min.X = minX;
				Max.X = maxX;
							Min.Y = minY;
				Max.Y = maxY;
					}

									public static Box2d Relative( Vector2d min ,  Vector2d size  ) {
					Box2d result;
											result.Min.X = min.X;
						result.Max.X = min.X + size.X;
											result.Min.Y = min.Y;
						result.Max.Y = min.Y + size.Y;
										return result;
				}
							public static Box2d Relative( ref Vector2d min ,  ref Vector2d size  ) {
					Box2d result;
											result.Min.X = min.X;
						result.Max.X = min.X + size.X;
											result.Min.Y = min.Y;
						result.Max.Y = min.Y + size.Y;
										return result;
				}
							public static Box2d Relative(  double minX ,  double minY ,   double sizeX ,  double sizeY  ) {
					Box2d result;
											result.Min.X = minX;
						result.Max.X = minX + sizeX;
											result.Min.Y = minY;
						result.Max.Y = minY + sizeY;
										return result;
				}
												public static void Relative( Vector2d min ,  Vector2d size  , out Box2d result) {
					
											result.Min.X = min.X;
						result.Max.X = min.X + size.X;
											result.Min.Y = min.Y;
						result.Max.Y = min.Y + size.Y;
										return;
				}
							public static void Relative( ref Vector2d min ,  ref Vector2d size  , out Box2d result) {
					
											result.Min.X = min.X;
						result.Max.X = min.X + size.X;
											result.Min.Y = min.Y;
						result.Max.Y = min.Y + size.Y;
										return;
				}
							public static void Relative(  double minX ,  double minY ,   double sizeX ,  double sizeY  , out Box2d result) {
					
											result.Min.X = minX;
						result.Max.X = minX + sizeX;
											result.Min.Y = minY;
						result.Max.Y = minY + sizeY;
										return;
				}
					
					/// <summary>Get a random position within the box.</summary>
			public Vector2d Random(Random rng ) {
				Vector2d result;
									result.X = (double)(rng.NextDouble() * (Max.X - Min.X) + Min.X);
									result.Y = (double)(rng.NextDouble() * (Max.Y - Min.Y) + Min.Y);
								return result;
			}
					/// <summary>Get a random position within the box.</summary>
			public void Random(Random rng , out Vector2d result) {
				
									result.X = (double)(rng.NextDouble() * (Max.X - Min.X) + Min.X);
									result.Y = (double)(rng.NextDouble() * (Max.Y - Min.Y) + Min.Y);
								return;
			}
		
					/// <summary>Get the closest distance between this <see cref="Box2d"/> and the <see cref="Vector2d"/>.</summary>
			public double Distance( Vector2d point) {
				Vector2d nearest;
				NearestPointTo(ref point, out nearest);
				return point.Distance(ref nearest);
			}

			public Containment Intersect( Vector2d point) {
				// Most points should be outside, so check that first.
				if ( point.X < Min.X || point.X > Max.X || point.Y < Min.Y || point.Y > Max.Y )
					return Containment.Disjoint;
				// Now check for boundaries, which will usually be cut short on the first axis.
				if ( (point.X == Min.X || point.X == Max.X) && (point.Y == Min.Y || point.Y == Max.Y) )
					return Containment.Intersects;
				return Containment.Contains;
			}

			/// <summary>Get whether this <see cref="Box2d"/> inclusively intersects with the <see cref="Vector2d"/>.</summary>
			public bool Overlaps( Vector2d point) {
				return  point.X >= Min.X && point.X <= Max.X && point.Y >= Min.Y && point.Y <= Max.Y ;
			}
					/// <summary>Get the closest distance between this <see cref="Box2d"/> and the <see cref="Vector2d"/>.</summary>
			public double Distance( ref  Vector2d point) {
				Vector2d nearest;
				NearestPointTo(ref point, out nearest);
				return point.Distance(ref nearest);
			}

			public Containment Intersect( ref  Vector2d point) {
				// Most points should be outside, so check that first.
				if ( point.X < Min.X || point.X > Max.X || point.Y < Min.Y || point.Y > Max.Y )
					return Containment.Disjoint;
				// Now check for boundaries, which will usually be cut short on the first axis.
				if ( (point.X == Min.X || point.X == Max.X) && (point.Y == Min.Y || point.Y == Max.Y) )
					return Containment.Intersects;
				return Containment.Contains;
			}

			/// <summary>Get whether this <see cref="Box2d"/> inclusively intersects with the <see cref="Vector2d"/>.</summary>
			public bool Overlaps( ref  Vector2d point) {
				return  point.X >= Min.X && point.X <= Max.X && point.Y >= Min.Y && point.Y <= Max.Y ;
			}
		
		
					public Vector2d NearestPointTo( Vector2d point ) {
				Vector2d result;
				Containment containment = Intersect(ref point);
				if(containment != Containment.Disjoint)
					result = point;
				else
					point.Clamp(ref Min, ref Max, out result);
				return result;
			}
					public void NearestPointTo( ref  Vector2d point , out Vector2d result) {
				
				Containment containment = Intersect(ref point);
				if(containment != Containment.Disjoint)
					result = point;
				else
					point.Clamp(ref Min, ref Max, out result);
				return;
			}
		
	}

		/// <summary>A two-dimensional box that uses int elements.</summary>
	public struct Box2i
	{
		/// <summary>Get whether the <see cref="Min"/> and <see cref="Max"/> values are the same.</summary>
		public bool IsEmpty { get { return  Min.X == Max.X || Min.Y == Max.Y ; } }

		/// <summary>The minimum extents of the <see cref="Box2i"/>.</summary>
		public Vector2i Min;

		/// <summary>The maximum extents of the <see cref="Box2i"/>.</summary>
		public Vector2i Max;

		public Vector2i Center { get { return new Vector2i( (Max.X + Min.X) / 2 ,  (Max.Y + Min.Y) / 2 ); } }
		
		public Vector2i Centre { get { return new Vector2i( (Max.X + Min.X) / 2 ,  (Max.Y + Min.Y) / 2 ); } }

		public Vector2i Size { get { return new Vector2i( Max.X - Min.X ,  Max.Y - Min.Y ); } }

		public static readonly Box2i Zero = new Box2i((int)0, (int)0, (int)0, (int)0);

					public Box2i( Vector2i min,  Vector2i max) { this.Min = min; this.Max = max; }
					public Box2i( ref  Vector2i min,  ref  Vector2i max) { this.Min = min; this.Max = max; }
		
		public Box2i(
			 int minX ,  int minY ,
			 int maxX ,  int maxY ) {
							Min.X = minX;
				Max.X = maxX;
							Min.Y = minY;
				Max.Y = maxY;
					}

									public static Box2i Relative( Vector2i min ,  Vector2i size  ) {
					Box2i result;
											result.Min.X = min.X;
						result.Max.X = min.X + size.X;
											result.Min.Y = min.Y;
						result.Max.Y = min.Y + size.Y;
										return result;
				}
							public static Box2i Relative( ref Vector2i min ,  ref Vector2i size  ) {
					Box2i result;
											result.Min.X = min.X;
						result.Max.X = min.X + size.X;
											result.Min.Y = min.Y;
						result.Max.Y = min.Y + size.Y;
										return result;
				}
							public static Box2i Relative(  int minX ,  int minY ,   int sizeX ,  int sizeY  ) {
					Box2i result;
											result.Min.X = minX;
						result.Max.X = minX + sizeX;
											result.Min.Y = minY;
						result.Max.Y = minY + sizeY;
										return result;
				}
												public static void Relative( Vector2i min ,  Vector2i size  , out Box2i result) {
					
											result.Min.X = min.X;
						result.Max.X = min.X + size.X;
											result.Min.Y = min.Y;
						result.Max.Y = min.Y + size.Y;
										return;
				}
							public static void Relative( ref Vector2i min ,  ref Vector2i size  , out Box2i result) {
					
											result.Min.X = min.X;
						result.Max.X = min.X + size.X;
											result.Min.Y = min.Y;
						result.Max.Y = min.Y + size.Y;
										return;
				}
							public static void Relative(  int minX ,  int minY ,   int sizeX ,  int sizeY  , out Box2i result) {
					
											result.Min.X = minX;
						result.Max.X = minX + sizeX;
											result.Min.Y = minY;
						result.Max.Y = minY + sizeY;
										return;
				}
					
					/// <summary>Get a random position within the box.</summary>
			public Vector2i Random(Random rng ) {
				Vector2i result;
									result.X = (int)(rng.NextDouble() * (Max.X - Min.X) + Min.X);
									result.Y = (int)(rng.NextDouble() * (Max.Y - Min.Y) + Min.Y);
								return result;
			}
					/// <summary>Get a random position within the box.</summary>
			public void Random(Random rng , out Vector2i result) {
				
									result.X = (int)(rng.NextDouble() * (Max.X - Min.X) + Min.X);
									result.Y = (int)(rng.NextDouble() * (Max.Y - Min.Y) + Min.Y);
								return;
			}
		
					/// <summary>Get the closest distance between this <see cref="Box2i"/> and the <see cref="Vector2i"/>.</summary>
			public int Distance( Vector2i point) {
				Vector2i nearest;
				NearestPointTo(ref point, out nearest);
				return point.Distance(ref nearest);
			}

			public Containment Intersect( Vector2i point) {
				// Most points should be outside, so check that first.
				if ( point.X < Min.X || point.X > Max.X || point.Y < Min.Y || point.Y > Max.Y )
					return Containment.Disjoint;
				// Now check for boundaries, which will usually be cut short on the first axis.
				if ( (point.X == Min.X || point.X == Max.X) && (point.Y == Min.Y || point.Y == Max.Y) )
					return Containment.Intersects;
				return Containment.Contains;
			}

			/// <summary>Get whether this <see cref="Box2i"/> inclusively intersects with the <see cref="Vector2i"/>.</summary>
			public bool Overlaps( Vector2i point) {
				return  point.X >= Min.X && point.X <= Max.X && point.Y >= Min.Y && point.Y <= Max.Y ;
			}
					/// <summary>Get the closest distance between this <see cref="Box2i"/> and the <see cref="Vector2i"/>.</summary>
			public int Distance( ref  Vector2i point) {
				Vector2i nearest;
				NearestPointTo(ref point, out nearest);
				return point.Distance(ref nearest);
			}

			public Containment Intersect( ref  Vector2i point) {
				// Most points should be outside, so check that first.
				if ( point.X < Min.X || point.X > Max.X || point.Y < Min.Y || point.Y > Max.Y )
					return Containment.Disjoint;
				// Now check for boundaries, which will usually be cut short on the first axis.
				if ( (point.X == Min.X || point.X == Max.X) && (point.Y == Min.Y || point.Y == Max.Y) )
					return Containment.Intersects;
				return Containment.Contains;
			}

			/// <summary>Get whether this <see cref="Box2i"/> inclusively intersects with the <see cref="Vector2i"/>.</summary>
			public bool Overlaps( ref  Vector2i point) {
				return  point.X >= Min.X && point.X <= Max.X && point.Y >= Min.Y && point.Y <= Max.Y ;
			}
		
		
					public Vector2i NearestPointTo( Vector2i point ) {
				Vector2i result;
				Containment containment = Intersect(ref point);
				if(containment != Containment.Disjoint)
					result = point;
				else
					point.Clamp(ref Min, ref Max, out result);
				return result;
			}
					public void NearestPointTo( ref  Vector2i point , out Vector2i result) {
				
				Containment containment = Intersect(ref point);
				if(containment != Containment.Disjoint)
					result = point;
				else
					point.Clamp(ref Min, ref Max, out result);
				return;
			}
		
	}

		/// <summary>A three-dimensional box that uses float elements.</summary>
	public struct Box3f
	{
		/// <summary>Get whether the <see cref="Min"/> and <see cref="Max"/> values are the same.</summary>
		public bool IsEmpty { get { return  Min.X == Max.X || Min.Y == Max.Y || Min.Z == Max.Z ; } }

		/// <summary>The minimum extents of the <see cref="Box3f"/>.</summary>
		public Vector3f Min;

		/// <summary>The maximum extents of the <see cref="Box3f"/>.</summary>
		public Vector3f Max;

		public Vector3f Center { get { return new Vector3f( (Max.X + Min.X) / 2 ,  (Max.Y + Min.Y) / 2 ,  (Max.Z + Min.Z) / 2 ); } }
		
		public Vector3f Centre { get { return new Vector3f( (Max.X + Min.X) / 2 ,  (Max.Y + Min.Y) / 2 ,  (Max.Z + Min.Z) / 2 ); } }

		public Vector3f Size { get { return new Vector3f( Max.X - Min.X ,  Max.Y - Min.Y ,  Max.Z - Min.Z ); } }

		public static readonly Box3f Zero = new Box3f((float)0, (float)0, (float)0, (float)0, (float)0, (float)0);

					public Box3f( Vector3f min,  Vector3f max) { this.Min = min; this.Max = max; }
					public Box3f( ref  Vector3f min,  ref  Vector3f max) { this.Min = min; this.Max = max; }
		
		public Box3f(
			 float minX ,  float minY ,  float minZ ,
			 float maxX ,  float maxY ,  float maxZ ) {
							Min.X = minX;
				Max.X = maxX;
							Min.Y = minY;
				Max.Y = maxY;
							Min.Z = minZ;
				Max.Z = maxZ;
					}

									public static Box3f Relative( Vector3f min ,  Vector3f size  ) {
					Box3f result;
											result.Min.X = min.X;
						result.Max.X = min.X + size.X;
											result.Min.Y = min.Y;
						result.Max.Y = min.Y + size.Y;
											result.Min.Z = min.Z;
						result.Max.Z = min.Z + size.Z;
										return result;
				}
							public static Box3f Relative( ref Vector3f min ,  ref Vector3f size  ) {
					Box3f result;
											result.Min.X = min.X;
						result.Max.X = min.X + size.X;
											result.Min.Y = min.Y;
						result.Max.Y = min.Y + size.Y;
											result.Min.Z = min.Z;
						result.Max.Z = min.Z + size.Z;
										return result;
				}
							public static Box3f Relative(  float minX ,  float minY ,  float minZ ,   float sizeX ,  float sizeY ,  float sizeZ  ) {
					Box3f result;
											result.Min.X = minX;
						result.Max.X = minX + sizeX;
											result.Min.Y = minY;
						result.Max.Y = minY + sizeY;
											result.Min.Z = minZ;
						result.Max.Z = minZ + sizeZ;
										return result;
				}
												public static void Relative( Vector3f min ,  Vector3f size  , out Box3f result) {
					
											result.Min.X = min.X;
						result.Max.X = min.X + size.X;
											result.Min.Y = min.Y;
						result.Max.Y = min.Y + size.Y;
											result.Min.Z = min.Z;
						result.Max.Z = min.Z + size.Z;
										return;
				}
							public static void Relative( ref Vector3f min ,  ref Vector3f size  , out Box3f result) {
					
											result.Min.X = min.X;
						result.Max.X = min.X + size.X;
											result.Min.Y = min.Y;
						result.Max.Y = min.Y + size.Y;
											result.Min.Z = min.Z;
						result.Max.Z = min.Z + size.Z;
										return;
				}
							public static void Relative(  float minX ,  float minY ,  float minZ ,   float sizeX ,  float sizeY ,  float sizeZ  , out Box3f result) {
					
											result.Min.X = minX;
						result.Max.X = minX + sizeX;
											result.Min.Y = minY;
						result.Max.Y = minY + sizeY;
											result.Min.Z = minZ;
						result.Max.Z = minZ + sizeZ;
										return;
				}
					
					/// <summary>Get a random position within the box.</summary>
			public Vector3f Random(Random rng ) {
				Vector3f result;
									result.X = (float)(rng.NextDouble() * (Max.X - Min.X) + Min.X);
									result.Y = (float)(rng.NextDouble() * (Max.Y - Min.Y) + Min.Y);
									result.Z = (float)(rng.NextDouble() * (Max.Z - Min.Z) + Min.Z);
								return result;
			}
					/// <summary>Get a random position within the box.</summary>
			public void Random(Random rng , out Vector3f result) {
				
									result.X = (float)(rng.NextDouble() * (Max.X - Min.X) + Min.X);
									result.Y = (float)(rng.NextDouble() * (Max.Y - Min.Y) + Min.Y);
									result.Z = (float)(rng.NextDouble() * (Max.Z - Min.Z) + Min.Z);
								return;
			}
		
					/// <summary>Get the closest distance between this <see cref="Box3f"/> and the <see cref="Vector3f"/>.</summary>
			public float Distance( Vector3f point) {
				Vector3f nearest;
				NearestPointTo(ref point, out nearest);
				return point.Distance(ref nearest);
			}

			public Containment Intersect( Vector3f point) {
				// Most points should be outside, so check that first.
				if ( point.X < Min.X || point.X > Max.X || point.Y < Min.Y || point.Y > Max.Y || point.Z < Min.Z || point.Z > Max.Z )
					return Containment.Disjoint;
				// Now check for boundaries, which will usually be cut short on the first axis.
				if ( (point.X == Min.X || point.X == Max.X) && (point.Y == Min.Y || point.Y == Max.Y) && (point.Z == Min.Z || point.Z == Max.Z) )
					return Containment.Intersects;
				return Containment.Contains;
			}

			/// <summary>Get whether this <see cref="Box3f"/> inclusively intersects with the <see cref="Vector3f"/>.</summary>
			public bool Overlaps( Vector3f point) {
				return  point.X >= Min.X && point.X <= Max.X && point.Y >= Min.Y && point.Y <= Max.Y && point.Z >= Min.Z && point.Z <= Max.Z ;
			}
					/// <summary>Get the closest distance between this <see cref="Box3f"/> and the <see cref="Vector3f"/>.</summary>
			public float Distance( ref  Vector3f point) {
				Vector3f nearest;
				NearestPointTo(ref point, out nearest);
				return point.Distance(ref nearest);
			}

			public Containment Intersect( ref  Vector3f point) {
				// Most points should be outside, so check that first.
				if ( point.X < Min.X || point.X > Max.X || point.Y < Min.Y || point.Y > Max.Y || point.Z < Min.Z || point.Z > Max.Z )
					return Containment.Disjoint;
				// Now check for boundaries, which will usually be cut short on the first axis.
				if ( (point.X == Min.X || point.X == Max.X) && (point.Y == Min.Y || point.Y == Max.Y) && (point.Z == Min.Z || point.Z == Max.Z) )
					return Containment.Intersects;
				return Containment.Contains;
			}

			/// <summary>Get whether this <see cref="Box3f"/> inclusively intersects with the <see cref="Vector3f"/>.</summary>
			public bool Overlaps( ref  Vector3f point) {
				return  point.X >= Min.X && point.X <= Max.X && point.Y >= Min.Y && point.Y <= Max.Y && point.Z >= Min.Z && point.Z <= Max.Z ;
			}
		
					public Containment Intersect(Frustum frustum) {
				throw new NotImplementedException();
			}
		
					public Vector3f NearestPointTo( Vector3f point ) {
				Vector3f result;
				Containment containment = Intersect(ref point);
				if(containment != Containment.Disjoint)
					result = point;
				else
					point.Clamp(ref Min, ref Max, out result);
				return result;
			}
					public void NearestPointTo( ref  Vector3f point , out Vector3f result) {
				
				Containment containment = Intersect(ref point);
				if(containment != Containment.Disjoint)
					result = point;
				else
					point.Clamp(ref Min, ref Max, out result);
				return;
			}
		
	}

		/// <summary>A three-dimensional box that uses double elements.</summary>
	public struct Box3d
	{
		/// <summary>Get whether the <see cref="Min"/> and <see cref="Max"/> values are the same.</summary>
		public bool IsEmpty { get { return  Min.X == Max.X || Min.Y == Max.Y || Min.Z == Max.Z ; } }

		/// <summary>The minimum extents of the <see cref="Box3d"/>.</summary>
		public Vector3d Min;

		/// <summary>The maximum extents of the <see cref="Box3d"/>.</summary>
		public Vector3d Max;

		public Vector3d Center { get { return new Vector3d( (Max.X + Min.X) / 2 ,  (Max.Y + Min.Y) / 2 ,  (Max.Z + Min.Z) / 2 ); } }
		
		public Vector3d Centre { get { return new Vector3d( (Max.X + Min.X) / 2 ,  (Max.Y + Min.Y) / 2 ,  (Max.Z + Min.Z) / 2 ); } }

		public Vector3d Size { get { return new Vector3d( Max.X - Min.X ,  Max.Y - Min.Y ,  Max.Z - Min.Z ); } }

		public static readonly Box3d Zero = new Box3d((double)0, (double)0, (double)0, (double)0, (double)0, (double)0);

					public Box3d( Vector3d min,  Vector3d max) { this.Min = min; this.Max = max; }
					public Box3d( ref  Vector3d min,  ref  Vector3d max) { this.Min = min; this.Max = max; }
		
		public Box3d(
			 double minX ,  double minY ,  double minZ ,
			 double maxX ,  double maxY ,  double maxZ ) {
							Min.X = minX;
				Max.X = maxX;
							Min.Y = minY;
				Max.Y = maxY;
							Min.Z = minZ;
				Max.Z = maxZ;
					}

									public static Box3d Relative( Vector3d min ,  Vector3d size  ) {
					Box3d result;
											result.Min.X = min.X;
						result.Max.X = min.X + size.X;
											result.Min.Y = min.Y;
						result.Max.Y = min.Y + size.Y;
											result.Min.Z = min.Z;
						result.Max.Z = min.Z + size.Z;
										return result;
				}
							public static Box3d Relative( ref Vector3d min ,  ref Vector3d size  ) {
					Box3d result;
											result.Min.X = min.X;
						result.Max.X = min.X + size.X;
											result.Min.Y = min.Y;
						result.Max.Y = min.Y + size.Y;
											result.Min.Z = min.Z;
						result.Max.Z = min.Z + size.Z;
										return result;
				}
							public static Box3d Relative(  double minX ,  double minY ,  double minZ ,   double sizeX ,  double sizeY ,  double sizeZ  ) {
					Box3d result;
											result.Min.X = minX;
						result.Max.X = minX + sizeX;
											result.Min.Y = minY;
						result.Max.Y = minY + sizeY;
											result.Min.Z = minZ;
						result.Max.Z = minZ + sizeZ;
										return result;
				}
												public static void Relative( Vector3d min ,  Vector3d size  , out Box3d result) {
					
											result.Min.X = min.X;
						result.Max.X = min.X + size.X;
											result.Min.Y = min.Y;
						result.Max.Y = min.Y + size.Y;
											result.Min.Z = min.Z;
						result.Max.Z = min.Z + size.Z;
										return;
				}
							public static void Relative( ref Vector3d min ,  ref Vector3d size  , out Box3d result) {
					
											result.Min.X = min.X;
						result.Max.X = min.X + size.X;
											result.Min.Y = min.Y;
						result.Max.Y = min.Y + size.Y;
											result.Min.Z = min.Z;
						result.Max.Z = min.Z + size.Z;
										return;
				}
							public static void Relative(  double minX ,  double minY ,  double minZ ,   double sizeX ,  double sizeY ,  double sizeZ  , out Box3d result) {
					
											result.Min.X = minX;
						result.Max.X = minX + sizeX;
											result.Min.Y = minY;
						result.Max.Y = minY + sizeY;
											result.Min.Z = minZ;
						result.Max.Z = minZ + sizeZ;
										return;
				}
					
					/// <summary>Get a random position within the box.</summary>
			public Vector3d Random(Random rng ) {
				Vector3d result;
									result.X = (double)(rng.NextDouble() * (Max.X - Min.X) + Min.X);
									result.Y = (double)(rng.NextDouble() * (Max.Y - Min.Y) + Min.Y);
									result.Z = (double)(rng.NextDouble() * (Max.Z - Min.Z) + Min.Z);
								return result;
			}
					/// <summary>Get a random position within the box.</summary>
			public void Random(Random rng , out Vector3d result) {
				
									result.X = (double)(rng.NextDouble() * (Max.X - Min.X) + Min.X);
									result.Y = (double)(rng.NextDouble() * (Max.Y - Min.Y) + Min.Y);
									result.Z = (double)(rng.NextDouble() * (Max.Z - Min.Z) + Min.Z);
								return;
			}
		
					/// <summary>Get the closest distance between this <see cref="Box3d"/> and the <see cref="Vector3d"/>.</summary>
			public double Distance( Vector3d point) {
				Vector3d nearest;
				NearestPointTo(ref point, out nearest);
				return point.Distance(ref nearest);
			}

			public Containment Intersect( Vector3d point) {
				// Most points should be outside, so check that first.
				if ( point.X < Min.X || point.X > Max.X || point.Y < Min.Y || point.Y > Max.Y || point.Z < Min.Z || point.Z > Max.Z )
					return Containment.Disjoint;
				// Now check for boundaries, which will usually be cut short on the first axis.
				if ( (point.X == Min.X || point.X == Max.X) && (point.Y == Min.Y || point.Y == Max.Y) && (point.Z == Min.Z || point.Z == Max.Z) )
					return Containment.Intersects;
				return Containment.Contains;
			}

			/// <summary>Get whether this <see cref="Box3d"/> inclusively intersects with the <see cref="Vector3d"/>.</summary>
			public bool Overlaps( Vector3d point) {
				return  point.X >= Min.X && point.X <= Max.X && point.Y >= Min.Y && point.Y <= Max.Y && point.Z >= Min.Z && point.Z <= Max.Z ;
			}
					/// <summary>Get the closest distance between this <see cref="Box3d"/> and the <see cref="Vector3d"/>.</summary>
			public double Distance( ref  Vector3d point) {
				Vector3d nearest;
				NearestPointTo(ref point, out nearest);
				return point.Distance(ref nearest);
			}

			public Containment Intersect( ref  Vector3d point) {
				// Most points should be outside, so check that first.
				if ( point.X < Min.X || point.X > Max.X || point.Y < Min.Y || point.Y > Max.Y || point.Z < Min.Z || point.Z > Max.Z )
					return Containment.Disjoint;
				// Now check for boundaries, which will usually be cut short on the first axis.
				if ( (point.X == Min.X || point.X == Max.X) && (point.Y == Min.Y || point.Y == Max.Y) && (point.Z == Min.Z || point.Z == Max.Z) )
					return Containment.Intersects;
				return Containment.Contains;
			}

			/// <summary>Get whether this <see cref="Box3d"/> inclusively intersects with the <see cref="Vector3d"/>.</summary>
			public bool Overlaps( ref  Vector3d point) {
				return  point.X >= Min.X && point.X <= Max.X && point.Y >= Min.Y && point.Y <= Max.Y && point.Z >= Min.Z && point.Z <= Max.Z ;
			}
		
					public Containment Intersect(Frustum frustum) {
				throw new NotImplementedException();
			}
		
					public Vector3d NearestPointTo( Vector3d point ) {
				Vector3d result;
				Containment containment = Intersect(ref point);
				if(containment != Containment.Disjoint)
					result = point;
				else
					point.Clamp(ref Min, ref Max, out result);
				return result;
			}
					public void NearestPointTo( ref  Vector3d point , out Vector3d result) {
				
				Containment containment = Intersect(ref point);
				if(containment != Containment.Disjoint)
					result = point;
				else
					point.Clamp(ref Min, ref Max, out result);
				return;
			}
		
	}

		/// <summary>A three-dimensional box that uses Length elements.</summary>
	public struct Box3
	{
		/// <summary>Get whether the <see cref="Min"/> and <see cref="Max"/> values are the same.</summary>
		public bool IsEmpty { get { return  Min.X == Max.X || Min.Y == Max.Y || Min.Z == Max.Z ; } }

		/// <summary>The minimum extents of the <see cref="Box3"/>.</summary>
		public Vector3 Min;

		/// <summary>The maximum extents of the <see cref="Box3"/>.</summary>
		public Vector3 Max;

		public Vector3 Center { get { return new Vector3( (Max.X + Min.X) / 2 ,  (Max.Y + Min.Y) / 2 ,  (Max.Z + Min.Z) / 2 ); } }
		
		public Vector3 Centre { get { return new Vector3( (Max.X + Min.X) / 2 ,  (Max.Y + Min.Y) / 2 ,  (Max.Z + Min.Z) / 2 ); } }

		public Vector3 Size { get { return new Vector3( Max.X - Min.X ,  Max.Y - Min.Y ,  Max.Z - Min.Z ); } }

		public static readonly Box3 Zero = new Box3(Length.Zero, Length.Zero, Length.Zero, Length.Zero, Length.Zero, Length.Zero);

					public Box3( Vector3 min,  Vector3 max) { this.Min = min; this.Max = max; }
					public Box3( ref  Vector3 min,  ref  Vector3 max) { this.Min = min; this.Max = max; }
		
		public Box3(
			 Length minX ,  Length minY ,  Length minZ ,
			 Length maxX ,  Length maxY ,  Length maxZ ) {
							Min.X = minX;
				Max.X = maxX;
							Min.Y = minY;
				Max.Y = maxY;
							Min.Z = minZ;
				Max.Z = maxZ;
					}

									public static Box3 Relative( Vector3 min ,  Vector3 size  ) {
					Box3 result;
											result.Min.X = min.X;
						result.Max.X = min.X + size.X;
											result.Min.Y = min.Y;
						result.Max.Y = min.Y + size.Y;
											result.Min.Z = min.Z;
						result.Max.Z = min.Z + size.Z;
										return result;
				}
							public static Box3 Relative( ref Vector3 min ,  ref Vector3 size  ) {
					Box3 result;
											result.Min.X = min.X;
						result.Max.X = min.X + size.X;
											result.Min.Y = min.Y;
						result.Max.Y = min.Y + size.Y;
											result.Min.Z = min.Z;
						result.Max.Z = min.Z + size.Z;
										return result;
				}
							public static Box3 Relative(  Length minX ,  Length minY ,  Length minZ ,   Length sizeX ,  Length sizeY ,  Length sizeZ  ) {
					Box3 result;
											result.Min.X = minX;
						result.Max.X = minX + sizeX;
											result.Min.Y = minY;
						result.Max.Y = minY + sizeY;
											result.Min.Z = minZ;
						result.Max.Z = minZ + sizeZ;
										return result;
				}
												public static void Relative( Vector3 min ,  Vector3 size  , out Box3 result) {
					
											result.Min.X = min.X;
						result.Max.X = min.X + size.X;
											result.Min.Y = min.Y;
						result.Max.Y = min.Y + size.Y;
											result.Min.Z = min.Z;
						result.Max.Z = min.Z + size.Z;
										return;
				}
							public static void Relative( ref Vector3 min ,  ref Vector3 size  , out Box3 result) {
					
											result.Min.X = min.X;
						result.Max.X = min.X + size.X;
											result.Min.Y = min.Y;
						result.Max.Y = min.Y + size.Y;
											result.Min.Z = min.Z;
						result.Max.Z = min.Z + size.Z;
										return;
				}
							public static void Relative(  Length minX ,  Length minY ,  Length minZ ,   Length sizeX ,  Length sizeY ,  Length sizeZ  , out Box3 result) {
					
											result.Min.X = minX;
						result.Max.X = minX + sizeX;
											result.Min.Y = minY;
						result.Max.Y = minY + sizeY;
											result.Min.Z = minZ;
						result.Max.Z = minZ + sizeZ;
										return;
				}
					
					/// <summary>Get a random position within the box.</summary>
			public Vector3 Random(Random rng ) {
				Vector3 result;
									result.X = (Length)(rng.NextDouble() * (Max.X - Min.X) + Min.X);
									result.Y = (Length)(rng.NextDouble() * (Max.Y - Min.Y) + Min.Y);
									result.Z = (Length)(rng.NextDouble() * (Max.Z - Min.Z) + Min.Z);
								return result;
			}
					/// <summary>Get a random position within the box.</summary>
			public void Random(Random rng , out Vector3 result) {
				
									result.X = (Length)(rng.NextDouble() * (Max.X - Min.X) + Min.X);
									result.Y = (Length)(rng.NextDouble() * (Max.Y - Min.Y) + Min.Y);
									result.Z = (Length)(rng.NextDouble() * (Max.Z - Min.Z) + Min.Z);
								return;
			}
		
					/// <summary>Get the closest distance between this <see cref="Box3"/> and the <see cref="Vector3"/>.</summary>
			public Length Distance( Vector3 point) {
				Vector3 nearest;
				NearestPointTo(ref point, out nearest);
				return point.Distance(ref nearest);
			}

			public Containment Intersect( Vector3 point) {
				// Most points should be outside, so check that first.
				if ( point.X < Min.X || point.X > Max.X || point.Y < Min.Y || point.Y > Max.Y || point.Z < Min.Z || point.Z > Max.Z )
					return Containment.Disjoint;
				// Now check for boundaries, which will usually be cut short on the first axis.
				if ( (point.X == Min.X || point.X == Max.X) && (point.Y == Min.Y || point.Y == Max.Y) && (point.Z == Min.Z || point.Z == Max.Z) )
					return Containment.Intersects;
				return Containment.Contains;
			}

			/// <summary>Get whether this <see cref="Box3"/> inclusively intersects with the <see cref="Vector3"/>.</summary>
			public bool Overlaps( Vector3 point) {
				return  point.X >= Min.X && point.X <= Max.X && point.Y >= Min.Y && point.Y <= Max.Y && point.Z >= Min.Z && point.Z <= Max.Z ;
			}
					/// <summary>Get the closest distance between this <see cref="Box3"/> and the <see cref="Vector3"/>.</summary>
			public Length Distance( ref  Vector3 point) {
				Vector3 nearest;
				NearestPointTo(ref point, out nearest);
				return point.Distance(ref nearest);
			}

			public Containment Intersect( ref  Vector3 point) {
				// Most points should be outside, so check that first.
				if ( point.X < Min.X || point.X > Max.X || point.Y < Min.Y || point.Y > Max.Y || point.Z < Min.Z || point.Z > Max.Z )
					return Containment.Disjoint;
				// Now check for boundaries, which will usually be cut short on the first axis.
				if ( (point.X == Min.X || point.X == Max.X) && (point.Y == Min.Y || point.Y == Max.Y) && (point.Z == Min.Z || point.Z == Max.Z) )
					return Containment.Intersects;
				return Containment.Contains;
			}

			/// <summary>Get whether this <see cref="Box3"/> inclusively intersects with the <see cref="Vector3"/>.</summary>
			public bool Overlaps( ref  Vector3 point) {
				return  point.X >= Min.X && point.X <= Max.X && point.Y >= Min.Y && point.Y <= Max.Y && point.Z >= Min.Z && point.Z <= Max.Z ;
			}
		
					public Containment Intersect(Frustum frustum) {
				throw new NotImplementedException();
			}
		
					public Vector3 NearestPointTo( Vector3 point ) {
				Vector3 result;
				Containment containment = Intersect(ref point);
				if(containment != Containment.Disjoint)
					result = point;
				else
					point.Clamp(ref Min, ref Max, out result);
				return result;
			}
					public void NearestPointTo( ref  Vector3 point , out Vector3 result) {
				
				Containment containment = Intersect(ref point);
				if(containment != Containment.Disjoint)
					result = point;
				else
					point.Clamp(ref Min, ref Max, out result);
				return;
			}
		
	}

		/// <summary>A three-dimensional box that uses int elements.</summary>
	public struct Box3i
	{
		/// <summary>Get whether the <see cref="Min"/> and <see cref="Max"/> values are the same.</summary>
		public bool IsEmpty { get { return  Min.X == Max.X || Min.Y == Max.Y || Min.Z == Max.Z ; } }

		/// <summary>The minimum extents of the <see cref="Box3i"/>.</summary>
		public Vector3i Min;

		/// <summary>The maximum extents of the <see cref="Box3i"/>.</summary>
		public Vector3i Max;

		public Vector3i Center { get { return new Vector3i( (Max.X + Min.X) / 2 ,  (Max.Y + Min.Y) / 2 ,  (Max.Z + Min.Z) / 2 ); } }
		
		public Vector3i Centre { get { return new Vector3i( (Max.X + Min.X) / 2 ,  (Max.Y + Min.Y) / 2 ,  (Max.Z + Min.Z) / 2 ); } }

		public Vector3i Size { get { return new Vector3i( Max.X - Min.X ,  Max.Y - Min.Y ,  Max.Z - Min.Z ); } }

		public static readonly Box3i Zero = new Box3i((int)0, (int)0, (int)0, (int)0, (int)0, (int)0);

					public Box3i( Vector3i min,  Vector3i max) { this.Min = min; this.Max = max; }
					public Box3i( ref  Vector3i min,  ref  Vector3i max) { this.Min = min; this.Max = max; }
		
		public Box3i(
			 int minX ,  int minY ,  int minZ ,
			 int maxX ,  int maxY ,  int maxZ ) {
							Min.X = minX;
				Max.X = maxX;
							Min.Y = minY;
				Max.Y = maxY;
							Min.Z = minZ;
				Max.Z = maxZ;
					}

									public static Box3i Relative( Vector3i min ,  Vector3i size  ) {
					Box3i result;
											result.Min.X = min.X;
						result.Max.X = min.X + size.X;
											result.Min.Y = min.Y;
						result.Max.Y = min.Y + size.Y;
											result.Min.Z = min.Z;
						result.Max.Z = min.Z + size.Z;
										return result;
				}
							public static Box3i Relative( ref Vector3i min ,  ref Vector3i size  ) {
					Box3i result;
											result.Min.X = min.X;
						result.Max.X = min.X + size.X;
											result.Min.Y = min.Y;
						result.Max.Y = min.Y + size.Y;
											result.Min.Z = min.Z;
						result.Max.Z = min.Z + size.Z;
										return result;
				}
							public static Box3i Relative(  int minX ,  int minY ,  int minZ ,   int sizeX ,  int sizeY ,  int sizeZ  ) {
					Box3i result;
											result.Min.X = minX;
						result.Max.X = minX + sizeX;
											result.Min.Y = minY;
						result.Max.Y = minY + sizeY;
											result.Min.Z = minZ;
						result.Max.Z = minZ + sizeZ;
										return result;
				}
												public static void Relative( Vector3i min ,  Vector3i size  , out Box3i result) {
					
											result.Min.X = min.X;
						result.Max.X = min.X + size.X;
											result.Min.Y = min.Y;
						result.Max.Y = min.Y + size.Y;
											result.Min.Z = min.Z;
						result.Max.Z = min.Z + size.Z;
										return;
				}
							public static void Relative( ref Vector3i min ,  ref Vector3i size  , out Box3i result) {
					
											result.Min.X = min.X;
						result.Max.X = min.X + size.X;
											result.Min.Y = min.Y;
						result.Max.Y = min.Y + size.Y;
											result.Min.Z = min.Z;
						result.Max.Z = min.Z + size.Z;
										return;
				}
							public static void Relative(  int minX ,  int minY ,  int minZ ,   int sizeX ,  int sizeY ,  int sizeZ  , out Box3i result) {
					
											result.Min.X = minX;
						result.Max.X = minX + sizeX;
											result.Min.Y = minY;
						result.Max.Y = minY + sizeY;
											result.Min.Z = minZ;
						result.Max.Z = minZ + sizeZ;
										return;
				}
					
					/// <summary>Get a random position within the box.</summary>
			public Vector3i Random(Random rng ) {
				Vector3i result;
									result.X = (int)(rng.NextDouble() * (Max.X - Min.X) + Min.X);
									result.Y = (int)(rng.NextDouble() * (Max.Y - Min.Y) + Min.Y);
									result.Z = (int)(rng.NextDouble() * (Max.Z - Min.Z) + Min.Z);
								return result;
			}
					/// <summary>Get a random position within the box.</summary>
			public void Random(Random rng , out Vector3i result) {
				
									result.X = (int)(rng.NextDouble() * (Max.X - Min.X) + Min.X);
									result.Y = (int)(rng.NextDouble() * (Max.Y - Min.Y) + Min.Y);
									result.Z = (int)(rng.NextDouble() * (Max.Z - Min.Z) + Min.Z);
								return;
			}
		
					/// <summary>Get the closest distance between this <see cref="Box3i"/> and the <see cref="Vector3i"/>.</summary>
			public int Distance( Vector3i point) {
				Vector3i nearest;
				NearestPointTo(ref point, out nearest);
				return point.Distance(ref nearest);
			}

			public Containment Intersect( Vector3i point) {
				// Most points should be outside, so check that first.
				if ( point.X < Min.X || point.X > Max.X || point.Y < Min.Y || point.Y > Max.Y || point.Z < Min.Z || point.Z > Max.Z )
					return Containment.Disjoint;
				// Now check for boundaries, which will usually be cut short on the first axis.
				if ( (point.X == Min.X || point.X == Max.X) && (point.Y == Min.Y || point.Y == Max.Y) && (point.Z == Min.Z || point.Z == Max.Z) )
					return Containment.Intersects;
				return Containment.Contains;
			}

			/// <summary>Get whether this <see cref="Box3i"/> inclusively intersects with the <see cref="Vector3i"/>.</summary>
			public bool Overlaps( Vector3i point) {
				return  point.X >= Min.X && point.X <= Max.X && point.Y >= Min.Y && point.Y <= Max.Y && point.Z >= Min.Z && point.Z <= Max.Z ;
			}
					/// <summary>Get the closest distance between this <see cref="Box3i"/> and the <see cref="Vector3i"/>.</summary>
			public int Distance( ref  Vector3i point) {
				Vector3i nearest;
				NearestPointTo(ref point, out nearest);
				return point.Distance(ref nearest);
			}

			public Containment Intersect( ref  Vector3i point) {
				// Most points should be outside, so check that first.
				if ( point.X < Min.X || point.X > Max.X || point.Y < Min.Y || point.Y > Max.Y || point.Z < Min.Z || point.Z > Max.Z )
					return Containment.Disjoint;
				// Now check for boundaries, which will usually be cut short on the first axis.
				if ( (point.X == Min.X || point.X == Max.X) && (point.Y == Min.Y || point.Y == Max.Y) && (point.Z == Min.Z || point.Z == Max.Z) )
					return Containment.Intersects;
				return Containment.Contains;
			}

			/// <summary>Get whether this <see cref="Box3i"/> inclusively intersects with the <see cref="Vector3i"/>.</summary>
			public bool Overlaps( ref  Vector3i point) {
				return  point.X >= Min.X && point.X <= Max.X && point.Y >= Min.Y && point.Y <= Max.Y && point.Z >= Min.Z && point.Z <= Max.Z ;
			}
		
					public Containment Intersect(Frustum frustum) {
				throw new NotImplementedException();
			}
		
					public Vector3i NearestPointTo( Vector3i point ) {
				Vector3i result;
				Containment containment = Intersect(ref point);
				if(containment != Containment.Disjoint)
					result = point;
				else
					point.Clamp(ref Min, ref Max, out result);
				return result;
			}
					public void NearestPointTo( ref  Vector3i point , out Vector3i result) {
				
				Containment containment = Intersect(ref point);
				if(containment != Containment.Disjoint)
					result = point;
				else
					point.Clamp(ref Min, ref Max, out result);
				return;
			}
		
	}

		/// <summary>A four-dimensional box that uses float elements.</summary>
	public struct Box4f
	{
		/// <summary>Get whether the <see cref="Min"/> and <see cref="Max"/> values are the same.</summary>
		public bool IsEmpty { get { return  Min.X == Max.X || Min.Y == Max.Y || Min.Z == Max.Z || Min.W == Max.W ; } }

		/// <summary>The minimum extents of the <see cref="Box4f"/>.</summary>
		public Vector4f Min;

		/// <summary>The maximum extents of the <see cref="Box4f"/>.</summary>
		public Vector4f Max;

		public Vector4f Center { get { return new Vector4f( (Max.X + Min.X) / 2 ,  (Max.Y + Min.Y) / 2 ,  (Max.Z + Min.Z) / 2 ,  (Max.W + Min.W) / 2 ); } }
		
		public Vector4f Centre { get { return new Vector4f( (Max.X + Min.X) / 2 ,  (Max.Y + Min.Y) / 2 ,  (Max.Z + Min.Z) / 2 ,  (Max.W + Min.W) / 2 ); } }

		public Vector4f Size { get { return new Vector4f( Max.X - Min.X ,  Max.Y - Min.Y ,  Max.Z - Min.Z ,  Max.W - Min.W ); } }

		public static readonly Box4f Zero = new Box4f((float)0, (float)0, (float)0, (float)0, (float)0, (float)0, (float)0, (float)0);

					public Box4f( Vector4f min,  Vector4f max) { this.Min = min; this.Max = max; }
					public Box4f( ref  Vector4f min,  ref  Vector4f max) { this.Min = min; this.Max = max; }
		
		public Box4f(
			 float minX ,  float minY ,  float minZ ,  float minW ,
			 float maxX ,  float maxY ,  float maxZ ,  float maxW ) {
							Min.X = minX;
				Max.X = maxX;
							Min.Y = minY;
				Max.Y = maxY;
							Min.Z = minZ;
				Max.Z = maxZ;
							Min.W = minW;
				Max.W = maxW;
					}

									public static Box4f Relative( Vector4f min ,  Vector4f size  ) {
					Box4f result;
											result.Min.X = min.X;
						result.Max.X = min.X + size.X;
											result.Min.Y = min.Y;
						result.Max.Y = min.Y + size.Y;
											result.Min.Z = min.Z;
						result.Max.Z = min.Z + size.Z;
											result.Min.W = min.W;
						result.Max.W = min.W + size.W;
										return result;
				}
							public static Box4f Relative( ref Vector4f min ,  ref Vector4f size  ) {
					Box4f result;
											result.Min.X = min.X;
						result.Max.X = min.X + size.X;
											result.Min.Y = min.Y;
						result.Max.Y = min.Y + size.Y;
											result.Min.Z = min.Z;
						result.Max.Z = min.Z + size.Z;
											result.Min.W = min.W;
						result.Max.W = min.W + size.W;
										return result;
				}
							public static Box4f Relative(  float minX ,  float minY ,  float minZ ,  float minW ,   float sizeX ,  float sizeY ,  float sizeZ ,  float sizeW  ) {
					Box4f result;
											result.Min.X = minX;
						result.Max.X = minX + sizeX;
											result.Min.Y = minY;
						result.Max.Y = minY + sizeY;
											result.Min.Z = minZ;
						result.Max.Z = minZ + sizeZ;
											result.Min.W = minW;
						result.Max.W = minW + sizeW;
										return result;
				}
												public static void Relative( Vector4f min ,  Vector4f size  , out Box4f result) {
					
											result.Min.X = min.X;
						result.Max.X = min.X + size.X;
											result.Min.Y = min.Y;
						result.Max.Y = min.Y + size.Y;
											result.Min.Z = min.Z;
						result.Max.Z = min.Z + size.Z;
											result.Min.W = min.W;
						result.Max.W = min.W + size.W;
										return;
				}
							public static void Relative( ref Vector4f min ,  ref Vector4f size  , out Box4f result) {
					
											result.Min.X = min.X;
						result.Max.X = min.X + size.X;
											result.Min.Y = min.Y;
						result.Max.Y = min.Y + size.Y;
											result.Min.Z = min.Z;
						result.Max.Z = min.Z + size.Z;
											result.Min.W = min.W;
						result.Max.W = min.W + size.W;
										return;
				}
							public static void Relative(  float minX ,  float minY ,  float minZ ,  float minW ,   float sizeX ,  float sizeY ,  float sizeZ ,  float sizeW  , out Box4f result) {
					
											result.Min.X = minX;
						result.Max.X = minX + sizeX;
											result.Min.Y = minY;
						result.Max.Y = minY + sizeY;
											result.Min.Z = minZ;
						result.Max.Z = minZ + sizeZ;
											result.Min.W = minW;
						result.Max.W = minW + sizeW;
										return;
				}
					
					/// <summary>Get a random position within the box.</summary>
			public Vector4f Random(Random rng ) {
				Vector4f result;
									result.X = (float)(rng.NextDouble() * (Max.X - Min.X) + Min.X);
									result.Y = (float)(rng.NextDouble() * (Max.Y - Min.Y) + Min.Y);
									result.Z = (float)(rng.NextDouble() * (Max.Z - Min.Z) + Min.Z);
									result.W = (float)(rng.NextDouble() * (Max.W - Min.W) + Min.W);
								return result;
			}
					/// <summary>Get a random position within the box.</summary>
			public void Random(Random rng , out Vector4f result) {
				
									result.X = (float)(rng.NextDouble() * (Max.X - Min.X) + Min.X);
									result.Y = (float)(rng.NextDouble() * (Max.Y - Min.Y) + Min.Y);
									result.Z = (float)(rng.NextDouble() * (Max.Z - Min.Z) + Min.Z);
									result.W = (float)(rng.NextDouble() * (Max.W - Min.W) + Min.W);
								return;
			}
		
					/// <summary>Get the closest distance between this <see cref="Box4f"/> and the <see cref="Vector4f"/>.</summary>
			public float Distance( Vector4f point) {
				Vector4f nearest;
				NearestPointTo(ref point, out nearest);
				return point.Distance(ref nearest);
			}

			public Containment Intersect( Vector4f point) {
				// Most points should be outside, so check that first.
				if ( point.X < Min.X || point.X > Max.X || point.Y < Min.Y || point.Y > Max.Y || point.Z < Min.Z || point.Z > Max.Z || point.W < Min.W || point.W > Max.W )
					return Containment.Disjoint;
				// Now check for boundaries, which will usually be cut short on the first axis.
				if ( (point.X == Min.X || point.X == Max.X) && (point.Y == Min.Y || point.Y == Max.Y) && (point.Z == Min.Z || point.Z == Max.Z) && (point.W == Min.W || point.W == Max.W) )
					return Containment.Intersects;
				return Containment.Contains;
			}

			/// <summary>Get whether this <see cref="Box4f"/> inclusively intersects with the <see cref="Vector4f"/>.</summary>
			public bool Overlaps( Vector4f point) {
				return  point.X >= Min.X && point.X <= Max.X && point.Y >= Min.Y && point.Y <= Max.Y && point.Z >= Min.Z && point.Z <= Max.Z && point.W >= Min.W && point.W <= Max.W ;
			}
					/// <summary>Get the closest distance between this <see cref="Box4f"/> and the <see cref="Vector4f"/>.</summary>
			public float Distance( ref  Vector4f point) {
				Vector4f nearest;
				NearestPointTo(ref point, out nearest);
				return point.Distance(ref nearest);
			}

			public Containment Intersect( ref  Vector4f point) {
				// Most points should be outside, so check that first.
				if ( point.X < Min.X || point.X > Max.X || point.Y < Min.Y || point.Y > Max.Y || point.Z < Min.Z || point.Z > Max.Z || point.W < Min.W || point.W > Max.W )
					return Containment.Disjoint;
				// Now check for boundaries, which will usually be cut short on the first axis.
				if ( (point.X == Min.X || point.X == Max.X) && (point.Y == Min.Y || point.Y == Max.Y) && (point.Z == Min.Z || point.Z == Max.Z) && (point.W == Min.W || point.W == Max.W) )
					return Containment.Intersects;
				return Containment.Contains;
			}

			/// <summary>Get whether this <see cref="Box4f"/> inclusively intersects with the <see cref="Vector4f"/>.</summary>
			public bool Overlaps( ref  Vector4f point) {
				return  point.X >= Min.X && point.X <= Max.X && point.Y >= Min.Y && point.Y <= Max.Y && point.Z >= Min.Z && point.Z <= Max.Z && point.W >= Min.W && point.W <= Max.W ;
			}
		
		
					public Vector4f NearestPointTo( Vector4f point ) {
				Vector4f result;
				Containment containment = Intersect(ref point);
				if(containment != Containment.Disjoint)
					result = point;
				else
					point.Clamp(ref Min, ref Max, out result);
				return result;
			}
					public void NearestPointTo( ref  Vector4f point , out Vector4f result) {
				
				Containment containment = Intersect(ref point);
				if(containment != Containment.Disjoint)
					result = point;
				else
					point.Clamp(ref Min, ref Max, out result);
				return;
			}
		
	}

		/// <summary>A four-dimensional box that uses double elements.</summary>
	public struct Box4d
	{
		/// <summary>Get whether the <see cref="Min"/> and <see cref="Max"/> values are the same.</summary>
		public bool IsEmpty { get { return  Min.X == Max.X || Min.Y == Max.Y || Min.Z == Max.Z || Min.W == Max.W ; } }

		/// <summary>The minimum extents of the <see cref="Box4d"/>.</summary>
		public Vector4d Min;

		/// <summary>The maximum extents of the <see cref="Box4d"/>.</summary>
		public Vector4d Max;

		public Vector4d Center { get { return new Vector4d( (Max.X + Min.X) / 2 ,  (Max.Y + Min.Y) / 2 ,  (Max.Z + Min.Z) / 2 ,  (Max.W + Min.W) / 2 ); } }
		
		public Vector4d Centre { get { return new Vector4d( (Max.X + Min.X) / 2 ,  (Max.Y + Min.Y) / 2 ,  (Max.Z + Min.Z) / 2 ,  (Max.W + Min.W) / 2 ); } }

		public Vector4d Size { get { return new Vector4d( Max.X - Min.X ,  Max.Y - Min.Y ,  Max.Z - Min.Z ,  Max.W - Min.W ); } }

		public static readonly Box4d Zero = new Box4d((double)0, (double)0, (double)0, (double)0, (double)0, (double)0, (double)0, (double)0);

					public Box4d( Vector4d min,  Vector4d max) { this.Min = min; this.Max = max; }
					public Box4d( ref  Vector4d min,  ref  Vector4d max) { this.Min = min; this.Max = max; }
		
		public Box4d(
			 double minX ,  double minY ,  double minZ ,  double minW ,
			 double maxX ,  double maxY ,  double maxZ ,  double maxW ) {
							Min.X = minX;
				Max.X = maxX;
							Min.Y = minY;
				Max.Y = maxY;
							Min.Z = minZ;
				Max.Z = maxZ;
							Min.W = minW;
				Max.W = maxW;
					}

									public static Box4d Relative( Vector4d min ,  Vector4d size  ) {
					Box4d result;
											result.Min.X = min.X;
						result.Max.X = min.X + size.X;
											result.Min.Y = min.Y;
						result.Max.Y = min.Y + size.Y;
											result.Min.Z = min.Z;
						result.Max.Z = min.Z + size.Z;
											result.Min.W = min.W;
						result.Max.W = min.W + size.W;
										return result;
				}
							public static Box4d Relative( ref Vector4d min ,  ref Vector4d size  ) {
					Box4d result;
											result.Min.X = min.X;
						result.Max.X = min.X + size.X;
											result.Min.Y = min.Y;
						result.Max.Y = min.Y + size.Y;
											result.Min.Z = min.Z;
						result.Max.Z = min.Z + size.Z;
											result.Min.W = min.W;
						result.Max.W = min.W + size.W;
										return result;
				}
							public static Box4d Relative(  double minX ,  double minY ,  double minZ ,  double minW ,   double sizeX ,  double sizeY ,  double sizeZ ,  double sizeW  ) {
					Box4d result;
											result.Min.X = minX;
						result.Max.X = minX + sizeX;
											result.Min.Y = minY;
						result.Max.Y = minY + sizeY;
											result.Min.Z = minZ;
						result.Max.Z = minZ + sizeZ;
											result.Min.W = minW;
						result.Max.W = minW + sizeW;
										return result;
				}
												public static void Relative( Vector4d min ,  Vector4d size  , out Box4d result) {
					
											result.Min.X = min.X;
						result.Max.X = min.X + size.X;
											result.Min.Y = min.Y;
						result.Max.Y = min.Y + size.Y;
											result.Min.Z = min.Z;
						result.Max.Z = min.Z + size.Z;
											result.Min.W = min.W;
						result.Max.W = min.W + size.W;
										return;
				}
							public static void Relative( ref Vector4d min ,  ref Vector4d size  , out Box4d result) {
					
											result.Min.X = min.X;
						result.Max.X = min.X + size.X;
											result.Min.Y = min.Y;
						result.Max.Y = min.Y + size.Y;
											result.Min.Z = min.Z;
						result.Max.Z = min.Z + size.Z;
											result.Min.W = min.W;
						result.Max.W = min.W + size.W;
										return;
				}
							public static void Relative(  double minX ,  double minY ,  double minZ ,  double minW ,   double sizeX ,  double sizeY ,  double sizeZ ,  double sizeW  , out Box4d result) {
					
											result.Min.X = minX;
						result.Max.X = minX + sizeX;
											result.Min.Y = minY;
						result.Max.Y = minY + sizeY;
											result.Min.Z = minZ;
						result.Max.Z = minZ + sizeZ;
											result.Min.W = minW;
						result.Max.W = minW + sizeW;
										return;
				}
					
					/// <summary>Get a random position within the box.</summary>
			public Vector4d Random(Random rng ) {
				Vector4d result;
									result.X = (double)(rng.NextDouble() * (Max.X - Min.X) + Min.X);
									result.Y = (double)(rng.NextDouble() * (Max.Y - Min.Y) + Min.Y);
									result.Z = (double)(rng.NextDouble() * (Max.Z - Min.Z) + Min.Z);
									result.W = (double)(rng.NextDouble() * (Max.W - Min.W) + Min.W);
								return result;
			}
					/// <summary>Get a random position within the box.</summary>
			public void Random(Random rng , out Vector4d result) {
				
									result.X = (double)(rng.NextDouble() * (Max.X - Min.X) + Min.X);
									result.Y = (double)(rng.NextDouble() * (Max.Y - Min.Y) + Min.Y);
									result.Z = (double)(rng.NextDouble() * (Max.Z - Min.Z) + Min.Z);
									result.W = (double)(rng.NextDouble() * (Max.W - Min.W) + Min.W);
								return;
			}
		
					/// <summary>Get the closest distance between this <see cref="Box4d"/> and the <see cref="Vector4d"/>.</summary>
			public double Distance( Vector4d point) {
				Vector4d nearest;
				NearestPointTo(ref point, out nearest);
				return point.Distance(ref nearest);
			}

			public Containment Intersect( Vector4d point) {
				// Most points should be outside, so check that first.
				if ( point.X < Min.X || point.X > Max.X || point.Y < Min.Y || point.Y > Max.Y || point.Z < Min.Z || point.Z > Max.Z || point.W < Min.W || point.W > Max.W )
					return Containment.Disjoint;
				// Now check for boundaries, which will usually be cut short on the first axis.
				if ( (point.X == Min.X || point.X == Max.X) && (point.Y == Min.Y || point.Y == Max.Y) && (point.Z == Min.Z || point.Z == Max.Z) && (point.W == Min.W || point.W == Max.W) )
					return Containment.Intersects;
				return Containment.Contains;
			}

			/// <summary>Get whether this <see cref="Box4d"/> inclusively intersects with the <see cref="Vector4d"/>.</summary>
			public bool Overlaps( Vector4d point) {
				return  point.X >= Min.X && point.X <= Max.X && point.Y >= Min.Y && point.Y <= Max.Y && point.Z >= Min.Z && point.Z <= Max.Z && point.W >= Min.W && point.W <= Max.W ;
			}
					/// <summary>Get the closest distance between this <see cref="Box4d"/> and the <see cref="Vector4d"/>.</summary>
			public double Distance( ref  Vector4d point) {
				Vector4d nearest;
				NearestPointTo(ref point, out nearest);
				return point.Distance(ref nearest);
			}

			public Containment Intersect( ref  Vector4d point) {
				// Most points should be outside, so check that first.
				if ( point.X < Min.X || point.X > Max.X || point.Y < Min.Y || point.Y > Max.Y || point.Z < Min.Z || point.Z > Max.Z || point.W < Min.W || point.W > Max.W )
					return Containment.Disjoint;
				// Now check for boundaries, which will usually be cut short on the first axis.
				if ( (point.X == Min.X || point.X == Max.X) && (point.Y == Min.Y || point.Y == Max.Y) && (point.Z == Min.Z || point.Z == Max.Z) && (point.W == Min.W || point.W == Max.W) )
					return Containment.Intersects;
				return Containment.Contains;
			}

			/// <summary>Get whether this <see cref="Box4d"/> inclusively intersects with the <see cref="Vector4d"/>.</summary>
			public bool Overlaps( ref  Vector4d point) {
				return  point.X >= Min.X && point.X <= Max.X && point.Y >= Min.Y && point.Y <= Max.Y && point.Z >= Min.Z && point.Z <= Max.Z && point.W >= Min.W && point.W <= Max.W ;
			}
		
		
					public Vector4d NearestPointTo( Vector4d point ) {
				Vector4d result;
				Containment containment = Intersect(ref point);
				if(containment != Containment.Disjoint)
					result = point;
				else
					point.Clamp(ref Min, ref Max, out result);
				return result;
			}
					public void NearestPointTo( ref  Vector4d point , out Vector4d result) {
				
				Containment containment = Intersect(ref point);
				if(containment != Containment.Disjoint)
					result = point;
				else
					point.Clamp(ref Min, ref Max, out result);
				return;
			}
		
	}

		/// <summary>A four-dimensional box that uses int elements.</summary>
	public struct Box4i
	{
		/// <summary>Get whether the <see cref="Min"/> and <see cref="Max"/> values are the same.</summary>
		public bool IsEmpty { get { return  Min.X == Max.X || Min.Y == Max.Y || Min.Z == Max.Z || Min.W == Max.W ; } }

		/// <summary>The minimum extents of the <see cref="Box4i"/>.</summary>
		public Vector4i Min;

		/// <summary>The maximum extents of the <see cref="Box4i"/>.</summary>
		public Vector4i Max;

		public Vector4i Center { get { return new Vector4i( (Max.X + Min.X) / 2 ,  (Max.Y + Min.Y) / 2 ,  (Max.Z + Min.Z) / 2 ,  (Max.W + Min.W) / 2 ); } }
		
		public Vector4i Centre { get { return new Vector4i( (Max.X + Min.X) / 2 ,  (Max.Y + Min.Y) / 2 ,  (Max.Z + Min.Z) / 2 ,  (Max.W + Min.W) / 2 ); } }

		public Vector4i Size { get { return new Vector4i( Max.X - Min.X ,  Max.Y - Min.Y ,  Max.Z - Min.Z ,  Max.W - Min.W ); } }

		public static readonly Box4i Zero = new Box4i((int)0, (int)0, (int)0, (int)0, (int)0, (int)0, (int)0, (int)0);

					public Box4i( Vector4i min,  Vector4i max) { this.Min = min; this.Max = max; }
					public Box4i( ref  Vector4i min,  ref  Vector4i max) { this.Min = min; this.Max = max; }
		
		public Box4i(
			 int minX ,  int minY ,  int minZ ,  int minW ,
			 int maxX ,  int maxY ,  int maxZ ,  int maxW ) {
							Min.X = minX;
				Max.X = maxX;
							Min.Y = minY;
				Max.Y = maxY;
							Min.Z = minZ;
				Max.Z = maxZ;
							Min.W = minW;
				Max.W = maxW;
					}

									public static Box4i Relative( Vector4i min ,  Vector4i size  ) {
					Box4i result;
											result.Min.X = min.X;
						result.Max.X = min.X + size.X;
											result.Min.Y = min.Y;
						result.Max.Y = min.Y + size.Y;
											result.Min.Z = min.Z;
						result.Max.Z = min.Z + size.Z;
											result.Min.W = min.W;
						result.Max.W = min.W + size.W;
										return result;
				}
							public static Box4i Relative( ref Vector4i min ,  ref Vector4i size  ) {
					Box4i result;
											result.Min.X = min.X;
						result.Max.X = min.X + size.X;
											result.Min.Y = min.Y;
						result.Max.Y = min.Y + size.Y;
											result.Min.Z = min.Z;
						result.Max.Z = min.Z + size.Z;
											result.Min.W = min.W;
						result.Max.W = min.W + size.W;
										return result;
				}
							public static Box4i Relative(  int minX ,  int minY ,  int minZ ,  int minW ,   int sizeX ,  int sizeY ,  int sizeZ ,  int sizeW  ) {
					Box4i result;
											result.Min.X = minX;
						result.Max.X = minX + sizeX;
											result.Min.Y = minY;
						result.Max.Y = minY + sizeY;
											result.Min.Z = minZ;
						result.Max.Z = minZ + sizeZ;
											result.Min.W = minW;
						result.Max.W = minW + sizeW;
										return result;
				}
												public static void Relative( Vector4i min ,  Vector4i size  , out Box4i result) {
					
											result.Min.X = min.X;
						result.Max.X = min.X + size.X;
											result.Min.Y = min.Y;
						result.Max.Y = min.Y + size.Y;
											result.Min.Z = min.Z;
						result.Max.Z = min.Z + size.Z;
											result.Min.W = min.W;
						result.Max.W = min.W + size.W;
										return;
				}
							public static void Relative( ref Vector4i min ,  ref Vector4i size  , out Box4i result) {
					
											result.Min.X = min.X;
						result.Max.X = min.X + size.X;
											result.Min.Y = min.Y;
						result.Max.Y = min.Y + size.Y;
											result.Min.Z = min.Z;
						result.Max.Z = min.Z + size.Z;
											result.Min.W = min.W;
						result.Max.W = min.W + size.W;
										return;
				}
							public static void Relative(  int minX ,  int minY ,  int minZ ,  int minW ,   int sizeX ,  int sizeY ,  int sizeZ ,  int sizeW  , out Box4i result) {
					
											result.Min.X = minX;
						result.Max.X = minX + sizeX;
											result.Min.Y = minY;
						result.Max.Y = minY + sizeY;
											result.Min.Z = minZ;
						result.Max.Z = minZ + sizeZ;
											result.Min.W = minW;
						result.Max.W = minW + sizeW;
										return;
				}
					
					/// <summary>Get a random position within the box.</summary>
			public Vector4i Random(Random rng ) {
				Vector4i result;
									result.X = (int)(rng.NextDouble() * (Max.X - Min.X) + Min.X);
									result.Y = (int)(rng.NextDouble() * (Max.Y - Min.Y) + Min.Y);
									result.Z = (int)(rng.NextDouble() * (Max.Z - Min.Z) + Min.Z);
									result.W = (int)(rng.NextDouble() * (Max.W - Min.W) + Min.W);
								return result;
			}
					/// <summary>Get a random position within the box.</summary>
			public void Random(Random rng , out Vector4i result) {
				
									result.X = (int)(rng.NextDouble() * (Max.X - Min.X) + Min.X);
									result.Y = (int)(rng.NextDouble() * (Max.Y - Min.Y) + Min.Y);
									result.Z = (int)(rng.NextDouble() * (Max.Z - Min.Z) + Min.Z);
									result.W = (int)(rng.NextDouble() * (Max.W - Min.W) + Min.W);
								return;
			}
		
					/// <summary>Get the closest distance between this <see cref="Box4i"/> and the <see cref="Vector4i"/>.</summary>
			public int Distance( Vector4i point) {
				Vector4i nearest;
				NearestPointTo(ref point, out nearest);
				return point.Distance(ref nearest);
			}

			public Containment Intersect( Vector4i point) {
				// Most points should be outside, so check that first.
				if ( point.X < Min.X || point.X > Max.X || point.Y < Min.Y || point.Y > Max.Y || point.Z < Min.Z || point.Z > Max.Z || point.W < Min.W || point.W > Max.W )
					return Containment.Disjoint;
				// Now check for boundaries, which will usually be cut short on the first axis.
				if ( (point.X == Min.X || point.X == Max.X) && (point.Y == Min.Y || point.Y == Max.Y) && (point.Z == Min.Z || point.Z == Max.Z) && (point.W == Min.W || point.W == Max.W) )
					return Containment.Intersects;
				return Containment.Contains;
			}

			/// <summary>Get whether this <see cref="Box4i"/> inclusively intersects with the <see cref="Vector4i"/>.</summary>
			public bool Overlaps( Vector4i point) {
				return  point.X >= Min.X && point.X <= Max.X && point.Y >= Min.Y && point.Y <= Max.Y && point.Z >= Min.Z && point.Z <= Max.Z && point.W >= Min.W && point.W <= Max.W ;
			}
					/// <summary>Get the closest distance between this <see cref="Box4i"/> and the <see cref="Vector4i"/>.</summary>
			public int Distance( ref  Vector4i point) {
				Vector4i nearest;
				NearestPointTo(ref point, out nearest);
				return point.Distance(ref nearest);
			}

			public Containment Intersect( ref  Vector4i point) {
				// Most points should be outside, so check that first.
				if ( point.X < Min.X || point.X > Max.X || point.Y < Min.Y || point.Y > Max.Y || point.Z < Min.Z || point.Z > Max.Z || point.W < Min.W || point.W > Max.W )
					return Containment.Disjoint;
				// Now check for boundaries, which will usually be cut short on the first axis.
				if ( (point.X == Min.X || point.X == Max.X) && (point.Y == Min.Y || point.Y == Max.Y) && (point.Z == Min.Z || point.Z == Max.Z) && (point.W == Min.W || point.W == Max.W) )
					return Containment.Intersects;
				return Containment.Contains;
			}

			/// <summary>Get whether this <see cref="Box4i"/> inclusively intersects with the <see cref="Vector4i"/>.</summary>
			public bool Overlaps( ref  Vector4i point) {
				return  point.X >= Min.X && point.X <= Max.X && point.Y >= Min.Y && point.Y <= Max.Y && point.Z >= Min.Z && point.Z <= Max.Z && point.W >= Min.W && point.W <= Max.W ;
			}
		
		
					public Vector4i NearestPointTo( Vector4i point ) {
				Vector4i result;
				Containment containment = Intersect(ref point);
				if(containment != Containment.Disjoint)
					result = point;
				else
					point.Clamp(ref Min, ref Max, out result);
				return result;
			}
					public void NearestPointTo( ref  Vector4i point , out Vector4i result) {
				
				Containment containment = Intersect(ref point);
				if(containment != Containment.Disjoint)
					result = point;
				else
					point.Clamp(ref Min, ref Max, out result);
				return;
			}
		
	}

	}




