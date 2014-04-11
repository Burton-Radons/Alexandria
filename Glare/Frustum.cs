using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare
{
	/// <summary>
	/// A bounding frustum class.
	/// </summary>
	public class Frustum : IEquatable<Frustum>
	{
		const int CornerCount = 8;
		const int PlaneCount = 6;

		Matrix4d transform;
		readonly Vector3d[] corners = new Vector3d[CornerCount];
		readonly Plane3d[] planes = new Plane3d[PlaneCount];


		public Plane3d Near { get { return this.planes[0]; } }

		public Plane3d Far { get { return this.planes[1]; } }

		public Plane3d Left { get { return this.planes[2]; } }

		public Plane3d Right { get { return this.planes[3]; } }

		public Plane3d Top { get { return this.planes[4]; } }

		public Plane3d Bottom { get { return this.planes[5]; } }

		public Matrix4d Transform 
		{
			get { return transform; }

			set
			{
				transform = value;
				SetupPlanes();
				SetupCorners();
			}
		}

		public Frustum() : this(Matrix4d.Identity) { }

		public Frustum(Matrix4d transform)
		{
			Transform = transform;
		}

		public override bool Equals(object obj)
		{
			if (obj is Frustum)
				return Equals((Frustum)obj);
			return base.Equals(obj);
		}

		public bool Equals(Frustum other)
		{
			if (ReferenceEquals(this, other))
				return true;
			if(ReferenceEquals(other, null))
				return false;
			return transform == other.transform;
		}

		public override int GetHashCode() { return transform.GetHashCode(); }

		void SetupCorners()
		{
			planes[0].Intersect(ref planes[2], ref planes[4], out corners[0]);
			planes[0].Intersect(ref planes[3], ref planes[4], out corners[1]);
			planes[0].Intersect(ref planes[3], ref planes[5], out corners[2]);
			planes[0].Intersect(ref planes[2], ref planes[5], out corners[3]);
			planes[1].Intersect(ref planes[2], ref planes[4], out corners[4]);
			planes[1].Intersect(ref planes[3], ref planes[4], out corners[5]);
			planes[1].Intersect(ref planes[3], ref planes[5], out corners[6]);
			planes[1].Intersect(ref planes[2], ref planes[5], out corners[7]);
		}

		void SetupPlanes()
		{
			planes[0] = new Plane3d(-transform.XZ, -transform.YZ, -transform.ZZ, -transform.WZ);
			planes[1] = new Plane3d(transform.XZ - transform.XW, transform.YZ - transform.YW, transform.ZZ - transform.ZW, transform.WZ - transform.WW);
			planes[2] = new Plane3d(-transform.XW - transform.XX, -transform.YW - transform.YX, -transform.ZW - transform.ZX, -transform.WW - transform.WX);
			planes[3] = new Plane3d(transform.XX - transform.XW, transform.YX - transform.YW, transform.ZX - transform.ZW, transform.WX - transform.WW);
			planes[4] = new Plane3d(transform.XY - transform.XW, transform.YY - transform.YW, transform.ZY - transform.ZW, transform.WY - transform.WW);
			planes[5] = new Plane3d(-transform.XW - transform.XY, -transform.YW - transform.YY, -transform.ZW - transform.ZY, -transform.WW - transform.WY);

			planes[0].NormalizeInPlace();
			planes[1].NormalizeInPlace();
			planes[2].NormalizeInPlace();
			planes[3].NormalizeInPlace();
			planes[4].NormalizeInPlace();
			planes[5].NormalizeInPlace();
		}

		public static bool operator ==(Frustum a, Frustum b) { if (ReferenceEquals(a, null)) return ReferenceEquals(b, null); return a.Equals(b); }
		public static bool operator !=(Frustum a, Frustum b) { return !(a == b); }
	}
}
