using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare {
		/// <summary>A Vector2f normal with a Single distance from origin.</summary>
	public struct Plane2f {
		/// <summary>Get or set the distance from origin.</summary>
		public Single Distance;

		/// <summary>Get or set the direction of the <see cref="Plane2f"/>.</summary>
		public Vector2f Normal;
		
		/// <summary>Initialise the <see cref="Plane2f"/>.</summary>
		public Plane2f(Vector2f normal, Single distance) {
			Distance = distance;
			Normal = normal;
		}

		/// <summary>Initialise the <see cref="Plane2f"/>.</summary>
		public Plane2f( Single normalX ,  Single normalY , Single distance) {
			Normal = new Vector2f( normalX ,  normalY );
			Distance = distance;
		}

		/// <summary>Normalize the <see cref="Plane2f"/> in place.</summary>
		public void NormalizeInPlace() { Normal.NormalizeInPlace(); }

			}
		/// <summary>A Vector2d normal with a Double distance from origin.</summary>
	public struct Plane2d {
		/// <summary>Get or set the distance from origin.</summary>
		public Double Distance;

		/// <summary>Get or set the direction of the <see cref="Plane2d"/>.</summary>
		public Vector2d Normal;
		
		/// <summary>Initialise the <see cref="Plane2d"/>.</summary>
		public Plane2d(Vector2d normal, Double distance) {
			Distance = distance;
			Normal = normal;
		}

		/// <summary>Initialise the <see cref="Plane2d"/>.</summary>
		public Plane2d( Double normalX ,  Double normalY , Double distance) {
			Normal = new Vector2d( normalX ,  normalY );
			Distance = distance;
		}

		/// <summary>Normalize the <see cref="Plane2d"/> in place.</summary>
		public void NormalizeInPlace() { Normal.NormalizeInPlace(); }

			}
		/// <summary>A Vector2d normal with a Length distance from origin.</summary>
	public struct Plane2 {
		/// <summary>Get or set the distance from origin.</summary>
		public Length Distance;

		/// <summary>Get or set the direction of the <see cref="Plane2"/>.</summary>
		public Vector2d Normal;
		
		/// <summary>Initialise the <see cref="Plane2"/>.</summary>
		public Plane2(Vector2d normal, Length distance) {
			Distance = distance;
			Normal = normal;
		}

		/// <summary>Initialise the <see cref="Plane2"/>.</summary>
		public Plane2( Double normalX ,  Double normalY , Length distance) {
			Normal = new Vector2d( normalX ,  normalY );
			Distance = distance;
		}

		/// <summary>Normalize the <see cref="Plane2"/> in place.</summary>
		public void NormalizeInPlace() { Normal.NormalizeInPlace(); }

			}
		/// <summary>A Vector3f normal with a Single distance from origin.</summary>
	public struct Plane3f {
		/// <summary>Get or set the distance from origin.</summary>
		public Single Distance;

		/// <summary>Get or set the direction of the <see cref="Plane3f"/>.</summary>
		public Vector3f Normal;
		
		/// <summary>Initialise the <see cref="Plane3f"/>.</summary>
		public Plane3f(Vector3f normal, Single distance) {
			Distance = distance;
			Normal = normal;
		}

		/// <summary>Initialise the <see cref="Plane3f"/>.</summary>
		public Plane3f( Single normalX ,  Single normalY ,  Single normalZ , Single distance) {
			Normal = new Vector3f( normalX ,  normalY ,  normalZ );
			Distance = distance;
		}

		/// <summary>Normalize the <see cref="Plane3f"/> in place.</summary>
		public void NormalizeInPlace() { Normal.NormalizeInPlace(); }

		
							/// <summary>Get the intersection point between the three planes.</summary>
				public Vector3f Intersect( Plane3f b,  Plane3f c ) {
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

					result.X = (Single)( (v1.X + v2.X + v3.X) / f );
					result.Y = (Single)( (v1.Y + v2.Y + v3.Y) / f );
					result.Z = (Single)( (v1.Z + v2.Z + v3.Z) / f );

					return result;
				}
							/// <summary>Get the intersection point between the three planes.</summary>
				public void Intersect( ref  Plane3f b,  ref  Plane3f c , out Vector3f result) {
					
					Vector3f v1, v2, v3;
					Vector3f cross;

					b.Normal.Cross(ref c.Normal, out cross);

					var f = -Normal.Dot(ref cross);

					v1 = cross *  Distance ;

					c.Normal.Cross(ref Normal, out cross);
					v2 = cross *  b.Distance ;

					Normal.Cross(ref b.Normal, out cross);
					v3 = cross *  c.Distance ;

					result.X = (Single)( (v1.X + v2.X + v3.X) / f );
					result.Y = (Single)( (v1.Y + v2.Y + v3.Y) / f );
					result.Z = (Single)( (v1.Z + v2.Z + v3.Z) / f );

					return;
				}
						}
		/// <summary>A Vector3d normal with a Double distance from origin.</summary>
	public struct Plane3d {
		/// <summary>Get or set the distance from origin.</summary>
		public Double Distance;

		/// <summary>Get or set the direction of the <see cref="Plane3d"/>.</summary>
		public Vector3d Normal;
		
		/// <summary>Initialise the <see cref="Plane3d"/>.</summary>
		public Plane3d(Vector3d normal, Double distance) {
			Distance = distance;
			Normal = normal;
		}

		/// <summary>Initialise the <see cref="Plane3d"/>.</summary>
		public Plane3d( Double normalX ,  Double normalY ,  Double normalZ , Double distance) {
			Normal = new Vector3d( normalX ,  normalY ,  normalZ );
			Distance = distance;
		}

		/// <summary>Normalize the <see cref="Plane3d"/> in place.</summary>
		public void NormalizeInPlace() { Normal.NormalizeInPlace(); }

		
							/// <summary>Get the intersection point between the three planes.</summary>
				public Vector3d Intersect( Plane3d b,  Plane3d c ) {
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

					result.X = (Double)( (v1.X + v2.X + v3.X) / f );
					result.Y = (Double)( (v1.Y + v2.Y + v3.Y) / f );
					result.Z = (Double)( (v1.Z + v2.Z + v3.Z) / f );

					return result;
				}
							/// <summary>Get the intersection point between the three planes.</summary>
				public void Intersect( ref  Plane3d b,  ref  Plane3d c , out Vector3d result) {
					
					Vector3d v1, v2, v3;
					Vector3d cross;

					b.Normal.Cross(ref c.Normal, out cross);

					var f = -Normal.Dot(ref cross);

					v1 = cross *  Distance ;

					c.Normal.Cross(ref Normal, out cross);
					v2 = cross *  b.Distance ;

					Normal.Cross(ref b.Normal, out cross);
					v3 = cross *  c.Distance ;

					result.X = (Double)( (v1.X + v2.X + v3.X) / f );
					result.Y = (Double)( (v1.Y + v2.Y + v3.Y) / f );
					result.Z = (Double)( (v1.Z + v2.Z + v3.Z) / f );

					return;
				}
						}
		/// <summary>A Vector3d normal with a Length distance from origin.</summary>
	public struct Plane3 {
		/// <summary>Get or set the distance from origin.</summary>
		public Length Distance;

		/// <summary>Get or set the direction of the <see cref="Plane3"/>.</summary>
		public Vector3d Normal;
		
		/// <summary>Initialise the <see cref="Plane3"/>.</summary>
		public Plane3(Vector3d normal, Length distance) {
			Distance = distance;
			Normal = normal;
		}

		/// <summary>Initialise the <see cref="Plane3"/>.</summary>
		public Plane3( Double normalX ,  Double normalY ,  Double normalZ , Length distance) {
			Normal = new Vector3d( normalX ,  normalY ,  normalZ );
			Distance = distance;
		}

		/// <summary>Normalize the <see cref="Plane3"/> in place.</summary>
		public void NormalizeInPlace() { Normal.NormalizeInPlace(); }

		
							/// <summary>Get the intersection point between the three planes.</summary>
				public Vector3 Intersect( Plane3 b,  Plane3 c ) {
					Vector3 result;
					Vector3d v1, v2, v3;
					Vector3d cross;

					b.Normal.Cross(ref c.Normal, out cross);

					var f = -Normal.Dot(ref cross);

					v1 = cross * ( Distance ).InUniversal;

					c.Normal.Cross(ref Normal, out cross);
					v2 = cross * ( b.Distance ).InUniversal;

					Normal.Cross(ref b.Normal, out cross);
					v3 = cross * ( c.Distance ).InUniversal;

					result.X = Length.Universal( (v1.X + v2.X + v3.X) / f );
					result.Y = Length.Universal( (v1.Y + v2.Y + v3.Y) / f );
					result.Z = Length.Universal( (v1.Z + v2.Z + v3.Z) / f );

					return result;
				}
							/// <summary>Get the intersection point between the three planes.</summary>
				public void Intersect( ref  Plane3 b,  ref  Plane3 c , out Vector3 result) {
					
					Vector3d v1, v2, v3;
					Vector3d cross;

					b.Normal.Cross(ref c.Normal, out cross);

					var f = -Normal.Dot(ref cross);

					v1 = cross * ( Distance ).InUniversal;

					c.Normal.Cross(ref Normal, out cross);
					v2 = cross * ( b.Distance ).InUniversal;

					Normal.Cross(ref b.Normal, out cross);
					v3 = cross * ( c.Distance ).InUniversal;

					result.X = Length.Universal( (v1.X + v2.X + v3.X) / f );
					result.Y = Length.Universal( (v1.Y + v2.Y + v3.Y) / f );
					result.Z = Length.Universal( (v1.Z + v2.Z + v3.Z) / f );

					return;
				}
						}
		/// <summary>A Vector4f normal with a Single distance from origin.</summary>
	public struct Plane4f {
		/// <summary>Get or set the distance from origin.</summary>
		public Single Distance;

		/// <summary>Get or set the direction of the <see cref="Plane4f"/>.</summary>
		public Vector4f Normal;
		
		/// <summary>Initialise the <see cref="Plane4f"/>.</summary>
		public Plane4f(Vector4f normal, Single distance) {
			Distance = distance;
			Normal = normal;
		}

		/// <summary>Initialise the <see cref="Plane4f"/>.</summary>
		public Plane4f( Single normalX ,  Single normalY ,  Single normalZ ,  Single normalW , Single distance) {
			Normal = new Vector4f( normalX ,  normalY ,  normalZ ,  normalW );
			Distance = distance;
		}

		/// <summary>Normalize the <see cref="Plane4f"/> in place.</summary>
		public void NormalizeInPlace() { Normal.NormalizeInPlace(); }

			}
		/// <summary>A Vector4d normal with a Double distance from origin.</summary>
	public struct Plane4d {
		/// <summary>Get or set the distance from origin.</summary>
		public Double Distance;

		/// <summary>Get or set the direction of the <see cref="Plane4d"/>.</summary>
		public Vector4d Normal;
		
		/// <summary>Initialise the <see cref="Plane4d"/>.</summary>
		public Plane4d(Vector4d normal, Double distance) {
			Distance = distance;
			Normal = normal;
		}

		/// <summary>Initialise the <see cref="Plane4d"/>.</summary>
		public Plane4d( Double normalX ,  Double normalY ,  Double normalZ ,  Double normalW , Double distance) {
			Normal = new Vector4d( normalX ,  normalY ,  normalZ ,  normalW );
			Distance = distance;
		}

		/// <summary>Normalize the <see cref="Plane4d"/> in place.</summary>
		public void NormalizeInPlace() { Normal.NormalizeInPlace(); }

			}
		/// <summary>A Vector4d normal with a Length distance from origin.</summary>
	public struct Plane4 {
		/// <summary>Get or set the distance from origin.</summary>
		public Length Distance;

		/// <summary>Get or set the direction of the <see cref="Plane4"/>.</summary>
		public Vector4d Normal;
		
		/// <summary>Initialise the <see cref="Plane4"/>.</summary>
		public Plane4(Vector4d normal, Length distance) {
			Distance = distance;
			Normal = normal;
		}

		/// <summary>Initialise the <see cref="Plane4"/>.</summary>
		public Plane4( Double normalX ,  Double normalY ,  Double normalZ ,  Double normalW , Length distance) {
			Normal = new Vector4d( normalX ,  normalY ,  normalZ ,  normalW );
			Distance = distance;
		}

		/// <summary>Normalize the <see cref="Plane4"/> in place.</summary>
		public void NormalizeInPlace() { Normal.NormalizeInPlace(); }

			}
	}




