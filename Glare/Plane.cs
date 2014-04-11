using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare
{
		/// <summary>A Vector2f normal with a float distance from origin.</summary>
	public struct Plane2f
	{
		/// <summary>Get or set the distance from origin.</summary>
		public float Distance;

		/// <summary>Get or set the direction of the <see cref="Plane2f"/>.</summary>
		public Vector2f Normal;
		
		public Plane2f(Vector2f normal, float distance)
		{
			Distance = distance;
			Normal = normal;
		}

		public Plane2f( float normalX ,  float normalY , float distance)
		{
			Normal = new Vector2f( normalX ,  normalY );
			Distance = distance;
		}

		public void NormalizeInPlace() { Normal.NormalizeInPlace(); }

			}
		/// <summary>A Vector2d normal with a double distance from origin.</summary>
	public struct Plane2d
	{
		/// <summary>Get or set the distance from origin.</summary>
		public double Distance;

		/// <summary>Get or set the direction of the <see cref="Plane2d"/>.</summary>
		public Vector2d Normal;
		
		public Plane2d(Vector2d normal, double distance)
		{
			Distance = distance;
			Normal = normal;
		}

		public Plane2d( double normalX ,  double normalY , double distance)
		{
			Normal = new Vector2d( normalX ,  normalY );
			Distance = distance;
		}

		public void NormalizeInPlace() { Normal.NormalizeInPlace(); }

			}
		/// <summary>A Vector3f normal with a float distance from origin.</summary>
	public struct Plane3f
	{
		/// <summary>Get or set the distance from origin.</summary>
		public float Distance;

		/// <summary>Get or set the direction of the <see cref="Plane3f"/>.</summary>
		public Vector3f Normal;
		
		public Plane3f(Vector3f normal, float distance)
		{
			Distance = distance;
			Normal = normal;
		}

		public Plane3f( float normalX ,  float normalY ,  float normalZ , float distance)
		{
			Normal = new Vector3f( normalX ,  normalY ,  normalZ );
			Distance = distance;
		}

		public void NormalizeInPlace() { Normal.NormalizeInPlace(); }

		
							public Vector3f Intersect( Plane3f b,  Plane3f c )
				{
					Vector3f result;
					Vector3f v1, v2, v3;
					Vector3f cross;

					b.Normal.Cross(ref c.Normal, out cross);

					var f = -Normal.Dot(ref cross);

					v1 = cross *  Distance ;

					c.Normal.Cross(ref Normal, out cross);
					v2 = cross *  b.Distance ;

					Normal.Cross(ref b.Normal, out cross);
					v3 = cross *  c.Distance ;

					result.X = (float)( (v1.X + v2.X + v3.X) / f );
					result.Y = (float)( (v1.Y + v2.Y + v3.Y) / f );
					result.Z = (float)( (v1.Z + v2.Z + v3.Z) / f );

					return result;
				}
							public void Intersect( ref  Plane3f b,  ref  Plane3f c , out Vector3f result)
				{
					
					Vector3f v1, v2, v3;
					Vector3f cross;

					b.Normal.Cross(ref c.Normal, out cross);

					var f = -Normal.Dot(ref cross);

					v1 = cross *  Distance ;

					c.Normal.Cross(ref Normal, out cross);
					v2 = cross *  b.Distance ;

					Normal.Cross(ref b.Normal, out cross);
					v3 = cross *  c.Distance ;

					result.X = (float)( (v1.X + v2.X + v3.X) / f );
					result.Y = (float)( (v1.Y + v2.Y + v3.Y) / f );
					result.Z = (float)( (v1.Z + v2.Z + v3.Z) / f );

					return;
				}
						}
		/// <summary>A Vector3d normal with a double distance from origin.</summary>
	public struct Plane3d
	{
		/// <summary>Get or set the distance from origin.</summary>
		public double Distance;

		/// <summary>Get or set the direction of the <see cref="Plane3d"/>.</summary>
		public Vector3d Normal;
		
		public Plane3d(Vector3d normal, double distance)
		{
			Distance = distance;
			Normal = normal;
		}

		public Plane3d( double normalX ,  double normalY ,  double normalZ , double distance)
		{
			Normal = new Vector3d( normalX ,  normalY ,  normalZ );
			Distance = distance;
		}

		public void NormalizeInPlace() { Normal.NormalizeInPlace(); }

		
							public Vector3d Intersect( Plane3d b,  Plane3d c )
				{
					Vector3d result;
					Vector3d v1, v2, v3;
					Vector3d cross;

					b.Normal.Cross(ref c.Normal, out cross);

					var f = -Normal.Dot(ref cross);

					v1 = cross *  Distance ;

					c.Normal.Cross(ref Normal, out cross);
					v2 = cross *  b.Distance ;

					Normal.Cross(ref b.Normal, out cross);
					v3 = cross *  c.Distance ;

					result.X = (double)( (v1.X + v2.X + v3.X) / f );
					result.Y = (double)( (v1.Y + v2.Y + v3.Y) / f );
					result.Z = (double)( (v1.Z + v2.Z + v3.Z) / f );

					return result;
				}
							public void Intersect( ref  Plane3d b,  ref  Plane3d c , out Vector3d result)
				{
					
					Vector3d v1, v2, v3;
					Vector3d cross;

					b.Normal.Cross(ref c.Normal, out cross);

					var f = -Normal.Dot(ref cross);

					v1 = cross *  Distance ;

					c.Normal.Cross(ref Normal, out cross);
					v2 = cross *  b.Distance ;

					Normal.Cross(ref b.Normal, out cross);
					v3 = cross *  c.Distance ;

					result.X = (double)( (v1.X + v2.X + v3.X) / f );
					result.Y = (double)( (v1.Y + v2.Y + v3.Y) / f );
					result.Z = (double)( (v1.Z + v2.Z + v3.Z) / f );

					return;
				}
						}
		/// <summary>A Vector4f normal with a float distance from origin.</summary>
	public struct Plane4f
	{
		/// <summary>Get or set the distance from origin.</summary>
		public float Distance;

		/// <summary>Get or set the direction of the <see cref="Plane4f"/>.</summary>
		public Vector4f Normal;
		
		public Plane4f(Vector4f normal, float distance)
		{
			Distance = distance;
			Normal = normal;
		}

		public Plane4f( float normalX ,  float normalY ,  float normalZ ,  float normalW , float distance)
		{
			Normal = new Vector4f( normalX ,  normalY ,  normalZ ,  normalW );
			Distance = distance;
		}

		public void NormalizeInPlace() { Normal.NormalizeInPlace(); }

			}
		/// <summary>A Vector4d normal with a double distance from origin.</summary>
	public struct Plane4d
	{
		/// <summary>Get or set the distance from origin.</summary>
		public double Distance;

		/// <summary>Get or set the direction of the <see cref="Plane4d"/>.</summary>
		public Vector4d Normal;
		
		public Plane4d(Vector4d normal, double distance)
		{
			Distance = distance;
			Normal = normal;
		}

		public Plane4d( double normalX ,  double normalY ,  double normalZ ,  double normalW , double distance)
		{
			Normal = new Vector4d( normalX ,  normalY ,  normalZ ,  normalW );
			Distance = distance;
		}

		public void NormalizeInPlace() { Normal.NormalizeInPlace(); }

			}
	}






