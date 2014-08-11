using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare {
	/// <summary>
	/// A bounding frustum class.
	/// </summary>
	public class Frustum : IEquatable<Frustum> {
		const int CornerCount = 8;
		const int PlaneCount = 6;

		Matrix4d transform;
		readonly Vector3d[] corners = new Vector3d[CornerCount];
		readonly Plane3d[] planes = new Plane3d[PlaneCount];

		/// <summary>Get the near <see cref="Plane3d"/> of the <see cref="Frustum"/>.</summary>
		public Plane3d Near { get { return this.planes[0]; } }

		/// <summary>Get the far <see cref="Plane3d"/> of the <see cref="Frustum"/>.</summary>
		public Plane3d Far { get { return this.planes[1]; } }

		/// <summary>Get the left <see cref="Plane3d"/> of the <see cref="Frustum"/>.</summary>
		public Plane3d Left { get { return this.planes[2]; } }

		/// <summary>Get the right <see cref="Plane3d"/> of the <see cref="Frustum"/>.</summary>
		public Plane3d Right { get { return this.planes[3]; } }

		/// <summary>Get the top <see cref="Plane3d"/> of the <see cref="Frustum"/>.</summary>
		public Plane3d Top { get { return this.planes[4]; } }

		/// <summary>Get the bottom <see cref="Plane3d"/> of the <see cref="Frustum"/>.</summary>
		public Plane3d Bottom { get { return this.planes[5]; } }

		/// <summary>Get or set the transform for the plane.</summary>
		public Matrix4d Transform {
			get { return transform; }

			set {
				transform = value;
				SetupPlanes();
				SetupCorners();
			}
		}

		/// <summary>Initialise the frustum.</summary>
		public Frustum() : this(Matrix4d.Identity) { }

		/// <summary>Initialise the frustum.</summary>
		/// <param name="transform"></param>
		public Frustum(Matrix4d transform) {
			Transform = transform;
		}

		/// <summary>
		/// Check whether this is equal to another <see cref="Frustum"/>.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj) {
			if (obj is Frustum)
				return Equals((Frustum)obj);
			return base.Equals(obj);
		}

		/// <summary>Check whether this is equal to another frustum.</summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool Equals(Frustum other) {
			if (ReferenceEquals(this, other))
				return true;
			if (ReferenceEquals(other, null))
				return false;
			return transform == other.transform;
		}

		/// <summary>Get a hash code from the transform.</summary>
		/// <returns></returns>
		public override int GetHashCode() { return transform.GetHashCode(); }

		void SetupCorners() {
			planes[0].Intersect(ref planes[2], ref planes[4], out corners[0]);
			planes[0].Intersect(ref planes[3], ref planes[4], out corners[1]);
			planes[0].Intersect(ref planes[3], ref planes[5], out corners[2]);
			planes[0].Intersect(ref planes[2], ref planes[5], out corners[3]);
			planes[1].Intersect(ref planes[2], ref planes[4], out corners[4]);
			planes[1].Intersect(ref planes[3], ref planes[4], out corners[5]);
			planes[1].Intersect(ref planes[3], ref planes[5], out corners[6]);
			planes[1].Intersect(ref planes[2], ref planes[5], out corners[7]);
		}

		void SetupPlanes() {
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

		/// <summary>Check for equality with another frustum.</summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static bool operator ==(Frustum a, Frustum b) { if (ReferenceEquals(a, null)) return ReferenceEquals(b, null); return a.Equals(b); }

		/// <summary>
		/// Check for inequality with another frustum.
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static bool operator !=(Frustum a, Frustum b) { return !(a == b); }
	}
}
