using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glare
{
		/// <summary>
	/// A sphere definition, which has a position and radius.
	/// </summary>
	public struct Sphere2f
	{
		/// <summary>Get the surface area of the <see cref="Sphere3d"/>.</summary>
		public float Area { get { return (float)((4 * Math.PI) * Radius * Radius); } }

		/// <summary>Get whether the sphere has no content.</summary>
		public bool IsEmpty { get { return Radius == 0; } }

		/// <summary>Get or set the centre of the <see cref="Sphere3d"/>.</summary>
		public Vector2f Position;

		/// <summary>Get or set the radius of the <see cref="Sphere3d"/>.</summary>
		public float Radius;

		/// <summary>Get the Radius**2.</summary>
		public float RadiusSquared { get { return Radius * Radius; } }

		/// <summary>Get the volume of the <see cref="Sphere3d"/>.</summary>
		public float Volume { get { return (float)((4.0 / 3.0 * Math.PI) * Radius * Radius * Radius); } }

		/// <summary>Initialise the <see cref="Sphere3d"/>.</summary>
		/// <param name="position">The centre of the <see cref="Sphere3d"/>.</param>
		/// <param name="radius">The radius of the <see cref="Sphere3d"/>.</param>
		public Sphere2f(Vector2f position, float radius)
		{
			Position = position;
			Radius = radius;
		}

		/// <summary>Initialise a sphere centred at origin.</summary>
		/// <param name="radius">The radius of the <see cref="Sphere3d"/>.</param>
		public Sphere2f(float radius)
		{
			Position = Vector2f.Zero;
			Radius = radius;
		}

		/// <summary>Initialise the <see cref="Sphere2f"/>.</summary>
		/// <param name="position">The centre of the <see cref="Sphere2f"/>. The value will not be modified.</param>
		/// <param name="radius">The radius of the <see cref="Sphere2f"/>.</param>
		public Sphere2f(ref Vector2f position, float radius)
		{
			Position = position;
			Radius = radius;
		}
	}
		/// <summary>
	/// A sphere definition, which has a position and radius.
	/// </summary>
	public struct Sphere2d
	{
		/// <summary>Get the surface area of the <see cref="Sphere3d"/>.</summary>
		public double Area { get { return (double)((4 * Math.PI) * Radius * Radius); } }

		/// <summary>Get whether the sphere has no content.</summary>
		public bool IsEmpty { get { return Radius == 0; } }

		/// <summary>Get or set the centre of the <see cref="Sphere3d"/>.</summary>
		public Vector2d Position;

		/// <summary>Get or set the radius of the <see cref="Sphere3d"/>.</summary>
		public double Radius;

		/// <summary>Get the Radius**2.</summary>
		public double RadiusSquared { get { return Radius * Radius; } }

		/// <summary>Get the volume of the <see cref="Sphere3d"/>.</summary>
		public double Volume { get { return (double)((4.0 / 3.0 * Math.PI) * Radius * Radius * Radius); } }

		/// <summary>Initialise the <see cref="Sphere3d"/>.</summary>
		/// <param name="position">The centre of the <see cref="Sphere3d"/>.</param>
		/// <param name="radius">The radius of the <see cref="Sphere3d"/>.</param>
		public Sphere2d(Vector2d position, double radius)
		{
			Position = position;
			Radius = radius;
		}

		/// <summary>Initialise a sphere centred at origin.</summary>
		/// <param name="radius">The radius of the <see cref="Sphere3d"/>.</param>
		public Sphere2d(double radius)
		{
			Position = Vector2d.Zero;
			Radius = radius;
		}

		/// <summary>Initialise the <see cref="Sphere2d"/>.</summary>
		/// <param name="position">The centre of the <see cref="Sphere2d"/>. The value will not be modified.</param>
		/// <param name="radius">The radius of the <see cref="Sphere2d"/>.</param>
		public Sphere2d(ref Vector2d position, double radius)
		{
			Position = position;
			Radius = radius;
		}
	}
		/// <summary>
	/// A sphere definition, which has a position and radius.
	/// </summary>
	public struct Sphere3f
	{
		/// <summary>Get the surface area of the <see cref="Sphere3d"/>.</summary>
		public float Area { get { return (float)((4 * Math.PI) * Radius * Radius); } }

		/// <summary>Get whether the sphere has no content.</summary>
		public bool IsEmpty { get { return Radius == 0; } }

		/// <summary>Get or set the centre of the <see cref="Sphere3d"/>.</summary>
		public Vector3f Position;

		/// <summary>Get or set the radius of the <see cref="Sphere3d"/>.</summary>
		public float Radius;

		/// <summary>Get the Radius**2.</summary>
		public float RadiusSquared { get { return Radius * Radius; } }

		/// <summary>Get the volume of the <see cref="Sphere3d"/>.</summary>
		public float Volume { get { return (float)((4.0 / 3.0 * Math.PI) * Radius * Radius * Radius); } }

		/// <summary>Initialise the <see cref="Sphere3d"/>.</summary>
		/// <param name="position">The centre of the <see cref="Sphere3d"/>.</param>
		/// <param name="radius">The radius of the <see cref="Sphere3d"/>.</param>
		public Sphere3f(Vector3f position, float radius)
		{
			Position = position;
			Radius = radius;
		}

		/// <summary>Initialise a sphere centred at origin.</summary>
		/// <param name="radius">The radius of the <see cref="Sphere3d"/>.</param>
		public Sphere3f(float radius)
		{
			Position = Vector3f.Zero;
			Radius = radius;
		}

		/// <summary>Initialise the <see cref="Sphere3f"/>.</summary>
		/// <param name="position">The centre of the <see cref="Sphere3f"/>. The value will not be modified.</param>
		/// <param name="radius">The radius of the <see cref="Sphere3f"/>.</param>
		public Sphere3f(ref Vector3f position, float radius)
		{
			Position = position;
			Radius = radius;
		}
	}
		/// <summary>
	/// A sphere definition, which has a position and radius.
	/// </summary>
	public struct Sphere3d
	{
		/// <summary>Get the surface area of the <see cref="Sphere3d"/>.</summary>
		public double Area { get { return (double)((4 * Math.PI) * Radius * Radius); } }

		/// <summary>Get whether the sphere has no content.</summary>
		public bool IsEmpty { get { return Radius == 0; } }

		/// <summary>Get or set the centre of the <see cref="Sphere3d"/>.</summary>
		public Vector3d Position;

		/// <summary>Get or set the radius of the <see cref="Sphere3d"/>.</summary>
		public double Radius;

		/// <summary>Get the Radius**2.</summary>
		public double RadiusSquared { get { return Radius * Radius; } }

		/// <summary>Get the volume of the <see cref="Sphere3d"/>.</summary>
		public double Volume { get { return (double)((4.0 / 3.0 * Math.PI) * Radius * Radius * Radius); } }

		/// <summary>Initialise the <see cref="Sphere3d"/>.</summary>
		/// <param name="position">The centre of the <see cref="Sphere3d"/>.</param>
		/// <param name="radius">The radius of the <see cref="Sphere3d"/>.</param>
		public Sphere3d(Vector3d position, double radius)
		{
			Position = position;
			Radius = radius;
		}

		/// <summary>Initialise a sphere centred at origin.</summary>
		/// <param name="radius">The radius of the <see cref="Sphere3d"/>.</param>
		public Sphere3d(double radius)
		{
			Position = Vector3d.Zero;
			Radius = radius;
		}

		/// <summary>Initialise the <see cref="Sphere3d"/>.</summary>
		/// <param name="position">The centre of the <see cref="Sphere3d"/>. The value will not be modified.</param>
		/// <param name="radius">The radius of the <see cref="Sphere3d"/>.</param>
		public Sphere3d(ref Vector3d position, double radius)
		{
			Position = position;
			Radius = radius;
		}
	}
		/// <summary>
	/// A sphere definition, which has a position and radius.
	/// </summary>
	public struct Sphere4f
	{
		/// <summary>Get the surface area of the <see cref="Sphere3d"/>.</summary>
		public float Area { get { return (float)((4 * Math.PI) * Radius * Radius); } }

		/// <summary>Get whether the sphere has no content.</summary>
		public bool IsEmpty { get { return Radius == 0; } }

		/// <summary>Get or set the centre of the <see cref="Sphere3d"/>.</summary>
		public Vector4f Position;

		/// <summary>Get or set the radius of the <see cref="Sphere3d"/>.</summary>
		public float Radius;

		/// <summary>Get the Radius**2.</summary>
		public float RadiusSquared { get { return Radius * Radius; } }

		/// <summary>Get the volume of the <see cref="Sphere3d"/>.</summary>
		public float Volume { get { return (float)((4.0 / 3.0 * Math.PI) * Radius * Radius * Radius); } }

		/// <summary>Initialise the <see cref="Sphere3d"/>.</summary>
		/// <param name="position">The centre of the <see cref="Sphere3d"/>.</param>
		/// <param name="radius">The radius of the <see cref="Sphere3d"/>.</param>
		public Sphere4f(Vector4f position, float radius)
		{
			Position = position;
			Radius = radius;
		}

		/// <summary>Initialise a sphere centred at origin.</summary>
		/// <param name="radius">The radius of the <see cref="Sphere3d"/>.</param>
		public Sphere4f(float radius)
		{
			Position = Vector4f.Zero;
			Radius = radius;
		}

		/// <summary>Initialise the <see cref="Sphere4f"/>.</summary>
		/// <param name="position">The centre of the <see cref="Sphere4f"/>. The value will not be modified.</param>
		/// <param name="radius">The radius of the <see cref="Sphere4f"/>.</param>
		public Sphere4f(ref Vector4f position, float radius)
		{
			Position = position;
			Radius = radius;
		}
	}
		/// <summary>
	/// A sphere definition, which has a position and radius.
	/// </summary>
	public struct Sphere4d
	{
		/// <summary>Get the surface area of the <see cref="Sphere3d"/>.</summary>
		public double Area { get { return (double)((4 * Math.PI) * Radius * Radius); } }

		/// <summary>Get whether the sphere has no content.</summary>
		public bool IsEmpty { get { return Radius == 0; } }

		/// <summary>Get or set the centre of the <see cref="Sphere3d"/>.</summary>
		public Vector4d Position;

		/// <summary>Get or set the radius of the <see cref="Sphere3d"/>.</summary>
		public double Radius;

		/// <summary>Get the Radius**2.</summary>
		public double RadiusSquared { get { return Radius * Radius; } }

		/// <summary>Get the volume of the <see cref="Sphere3d"/>.</summary>
		public double Volume { get { return (double)((4.0 / 3.0 * Math.PI) * Radius * Radius * Radius); } }

		/// <summary>Initialise the <see cref="Sphere3d"/>.</summary>
		/// <param name="position">The centre of the <see cref="Sphere3d"/>.</param>
		/// <param name="radius">The radius of the <see cref="Sphere3d"/>.</param>
		public Sphere4d(Vector4d position, double radius)
		{
			Position = position;
			Radius = radius;
		}

		/// <summary>Initialise a sphere centred at origin.</summary>
		/// <param name="radius">The radius of the <see cref="Sphere3d"/>.</param>
		public Sphere4d(double radius)
		{
			Position = Vector4d.Zero;
			Radius = radius;
		}

		/// <summary>Initialise the <see cref="Sphere4d"/>.</summary>
		/// <param name="position">The centre of the <see cref="Sphere4d"/>. The value will not be modified.</param>
		/// <param name="radius">The radius of the <see cref="Sphere4d"/>.</param>
		public Sphere4d(ref Vector4d position, double radius)
		{
			Position = position;
			Radius = radius;
		}
	}
	}






