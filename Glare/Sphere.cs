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
		public Single Area { get { return (Single)((4 * Math.PI) * Radius * Radius); } }

		/// <summary>Get whether the sphere has no content.</summary>
		public bool IsEmpty { get { return Radius == (Single)0; } }
		
		/// <summary>Get or set the centre of the <see cref="Sphere3d"/>.</summary>
		public Vector2f Position;

		/// <summary>Get or set the radius of the <see cref="Sphere3d"/>.</summary>
		public Single Radius;

		/// <summary>Get the Radius**2.</summary>
		public Single RadiusSquared { get { return (Single)(Radius * Radius); } }

		/// <summary>Get the volume of the <see cref="Sphere3d"/>.</summary>
		public Single Volume { get { return (Single)((4.0 / 3.0 * Math.PI) * Radius * Radius * Radius); } }

		/// <summary>Initialise the <see cref="Sphere3d"/>.</summary>
		/// <param name="position">The centre of the <see cref="Sphere3d"/>.</param>
		/// <param name="radius">The radius of the <see cref="Sphere3d"/>.</param>
		public Sphere2f(Vector2f position, Single radius)
		{
			Position = position;
			Radius = radius;
		}

		/// <summary>Initialise a sphere centred at origin.</summary>
		/// <param name="radius">The radius of the <see cref="Sphere3d"/>.</param>
		public Sphere2f(Single radius)
		{
			Position = Vector2f.Zero;
			Radius = radius;
		}

		/// <summary>Initialise the <see cref="Sphere2f"/>.</summary>
		/// <param name="position">The centre of the <see cref="Sphere2f"/>. The value will not be modified.</param>
		/// <param name="radius">The radius of the <see cref="Sphere2f"/>.</param>
		public Sphere2f(ref Vector2f position, Single radius)
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
		public Double Area { get { return (Double)((4 * Math.PI) * Radius * Radius); } }

		/// <summary>Get whether the sphere has no content.</summary>
		public bool IsEmpty { get { return Radius == (Double)0; } }
		
		/// <summary>Get or set the centre of the <see cref="Sphere3d"/>.</summary>
		public Vector2d Position;

		/// <summary>Get or set the radius of the <see cref="Sphere3d"/>.</summary>
		public Double Radius;

		/// <summary>Get the Radius**2.</summary>
		public Double RadiusSquared { get { return (Double)(Radius * Radius); } }

		/// <summary>Get the volume of the <see cref="Sphere3d"/>.</summary>
		public Double Volume { get { return (Double)((4.0 / 3.0 * Math.PI) * Radius * Radius * Radius); } }

		/// <summary>Initialise the <see cref="Sphere3d"/>.</summary>
		/// <param name="position">The centre of the <see cref="Sphere3d"/>.</param>
		/// <param name="radius">The radius of the <see cref="Sphere3d"/>.</param>
		public Sphere2d(Vector2d position, Double radius)
		{
			Position = position;
			Radius = radius;
		}

		/// <summary>Initialise a sphere centred at origin.</summary>
		/// <param name="radius">The radius of the <see cref="Sphere3d"/>.</param>
		public Sphere2d(Double radius)
		{
			Position = Vector2d.Zero;
			Radius = radius;
		}

		/// <summary>Initialise the <see cref="Sphere2d"/>.</summary>
		/// <param name="position">The centre of the <see cref="Sphere2d"/>. The value will not be modified.</param>
		/// <param name="radius">The radius of the <see cref="Sphere2d"/>.</param>
		public Sphere2d(ref Vector2d position, Double radius)
		{
			Position = position;
			Radius = radius;
		}
	}
		/// <summary>
	/// A sphere definition, which has a position and radius.
	/// </summary>
	public struct Sphere2
	{
		/// <summary>Get the surface area of the <see cref="Sphere3d"/>.</summary>
		public Area Area { get { return (Area)((4 * Math.PI) * Radius * Radius); } }

		/// <summary>Get whether the sphere has no content.</summary>
		public bool IsEmpty { get { return Radius == Length.Zero; } }
		
		/// <summary>Get or set the centre of the <see cref="Sphere3d"/>.</summary>
		public Vector2 Position;

		/// <summary>Get or set the radius of the <see cref="Sphere3d"/>.</summary>
		public Length Radius;

		/// <summary>Get the Radius**2.</summary>
		public Area RadiusSquared { get { return (Area)(Radius * Radius); } }

		/// <summary>Get the volume of the <see cref="Sphere3d"/>.</summary>
		public Volume Volume { get { return (Volume)((4.0 / 3.0 * Math.PI) * Radius * Radius * Radius); } }

		/// <summary>Initialise the <see cref="Sphere3d"/>.</summary>
		/// <param name="position">The centre of the <see cref="Sphere3d"/>.</param>
		/// <param name="radius">The radius of the <see cref="Sphere3d"/>.</param>
		public Sphere2(Vector2 position, Length radius)
		{
			Position = position;
			Radius = radius;
		}

		/// <summary>Initialise a sphere centred at origin.</summary>
		/// <param name="radius">The radius of the <see cref="Sphere3d"/>.</param>
		public Sphere2(Length radius)
		{
			Position = Vector2.Zero;
			Radius = radius;
		}

		/// <summary>Initialise the <see cref="Sphere2"/>.</summary>
		/// <param name="position">The centre of the <see cref="Sphere2"/>. The value will not be modified.</param>
		/// <param name="radius">The radius of the <see cref="Sphere2"/>.</param>
		public Sphere2(ref Vector2 position, Length radius)
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
		public Single Area { get { return (Single)((4 * Math.PI) * Radius * Radius); } }

		/// <summary>Get whether the sphere has no content.</summary>
		public bool IsEmpty { get { return Radius == (Single)0; } }
		
		/// <summary>Get or set the centre of the <see cref="Sphere3d"/>.</summary>
		public Vector3f Position;

		/// <summary>Get or set the radius of the <see cref="Sphere3d"/>.</summary>
		public Single Radius;

		/// <summary>Get the Radius**2.</summary>
		public Single RadiusSquared { get { return (Single)(Radius * Radius); } }

		/// <summary>Get the volume of the <see cref="Sphere3d"/>.</summary>
		public Single Volume { get { return (Single)((4.0 / 3.0 * Math.PI) * Radius * Radius * Radius); } }

		/// <summary>Initialise the <see cref="Sphere3d"/>.</summary>
		/// <param name="position">The centre of the <see cref="Sphere3d"/>.</param>
		/// <param name="radius">The radius of the <see cref="Sphere3d"/>.</param>
		public Sphere3f(Vector3f position, Single radius)
		{
			Position = position;
			Radius = radius;
		}

		/// <summary>Initialise a sphere centred at origin.</summary>
		/// <param name="radius">The radius of the <see cref="Sphere3d"/>.</param>
		public Sphere3f(Single radius)
		{
			Position = Vector3f.Zero;
			Radius = radius;
		}

		/// <summary>Initialise the <see cref="Sphere3f"/>.</summary>
		/// <param name="position">The centre of the <see cref="Sphere3f"/>. The value will not be modified.</param>
		/// <param name="radius">The radius of the <see cref="Sphere3f"/>.</param>
		public Sphere3f(ref Vector3f position, Single radius)
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
		public Double Area { get { return (Double)((4 * Math.PI) * Radius * Radius); } }

		/// <summary>Get whether the sphere has no content.</summary>
		public bool IsEmpty { get { return Radius == (Double)0; } }
		
		/// <summary>Get or set the centre of the <see cref="Sphere3d"/>.</summary>
		public Vector3d Position;

		/// <summary>Get or set the radius of the <see cref="Sphere3d"/>.</summary>
		public Double Radius;

		/// <summary>Get the Radius**2.</summary>
		public Double RadiusSquared { get { return (Double)(Radius * Radius); } }

		/// <summary>Get the volume of the <see cref="Sphere3d"/>.</summary>
		public Double Volume { get { return (Double)((4.0 / 3.0 * Math.PI) * Radius * Radius * Radius); } }

		/// <summary>Initialise the <see cref="Sphere3d"/>.</summary>
		/// <param name="position">The centre of the <see cref="Sphere3d"/>.</param>
		/// <param name="radius">The radius of the <see cref="Sphere3d"/>.</param>
		public Sphere3d(Vector3d position, Double radius)
		{
			Position = position;
			Radius = radius;
		}

		/// <summary>Initialise a sphere centred at origin.</summary>
		/// <param name="radius">The radius of the <see cref="Sphere3d"/>.</param>
		public Sphere3d(Double radius)
		{
			Position = Vector3d.Zero;
			Radius = radius;
		}

		/// <summary>Initialise the <see cref="Sphere3d"/>.</summary>
		/// <param name="position">The centre of the <see cref="Sphere3d"/>. The value will not be modified.</param>
		/// <param name="radius">The radius of the <see cref="Sphere3d"/>.</param>
		public Sphere3d(ref Vector3d position, Double radius)
		{
			Position = position;
			Radius = radius;
		}
	}
		/// <summary>
	/// A sphere definition, which has a position and radius.
	/// </summary>
	public struct Sphere3
	{
		/// <summary>Get the surface area of the <see cref="Sphere3d"/>.</summary>
		public Area Area { get { return (Area)((4 * Math.PI) * Radius * Radius); } }

		/// <summary>Get whether the sphere has no content.</summary>
		public bool IsEmpty { get { return Radius == Length.Zero; } }
		
		/// <summary>Get or set the centre of the <see cref="Sphere3d"/>.</summary>
		public Vector3 Position;

		/// <summary>Get or set the radius of the <see cref="Sphere3d"/>.</summary>
		public Length Radius;

		/// <summary>Get the Radius**2.</summary>
		public Area RadiusSquared { get { return (Area)(Radius * Radius); } }

		/// <summary>Get the volume of the <see cref="Sphere3d"/>.</summary>
		public Volume Volume { get { return (Volume)((4.0 / 3.0 * Math.PI) * Radius * Radius * Radius); } }

		/// <summary>Initialise the <see cref="Sphere3d"/>.</summary>
		/// <param name="position">The centre of the <see cref="Sphere3d"/>.</param>
		/// <param name="radius">The radius of the <see cref="Sphere3d"/>.</param>
		public Sphere3(Vector3 position, Length radius)
		{
			Position = position;
			Radius = radius;
		}

		/// <summary>Initialise a sphere centred at origin.</summary>
		/// <param name="radius">The radius of the <see cref="Sphere3d"/>.</param>
		public Sphere3(Length radius)
		{
			Position = Vector3.Zero;
			Radius = radius;
		}

		/// <summary>Initialise the <see cref="Sphere3"/>.</summary>
		/// <param name="position">The centre of the <see cref="Sphere3"/>. The value will not be modified.</param>
		/// <param name="radius">The radius of the <see cref="Sphere3"/>.</param>
		public Sphere3(ref Vector3 position, Length radius)
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
		public Single Area { get { return (Single)((4 * Math.PI) * Radius * Radius); } }

		/// <summary>Get whether the sphere has no content.</summary>
		public bool IsEmpty { get { return Radius == (Single)0; } }
		
		/// <summary>Get or set the centre of the <see cref="Sphere3d"/>.</summary>
		public Vector4f Position;

		/// <summary>Get or set the radius of the <see cref="Sphere3d"/>.</summary>
		public Single Radius;

		/// <summary>Get the Radius**2.</summary>
		public Single RadiusSquared { get { return (Single)(Radius * Radius); } }

		/// <summary>Get the volume of the <see cref="Sphere3d"/>.</summary>
		public Single Volume { get { return (Single)((4.0 / 3.0 * Math.PI) * Radius * Radius * Radius); } }

		/// <summary>Initialise the <see cref="Sphere3d"/>.</summary>
		/// <param name="position">The centre of the <see cref="Sphere3d"/>.</param>
		/// <param name="radius">The radius of the <see cref="Sphere3d"/>.</param>
		public Sphere4f(Vector4f position, Single radius)
		{
			Position = position;
			Radius = radius;
		}

		/// <summary>Initialise a sphere centred at origin.</summary>
		/// <param name="radius">The radius of the <see cref="Sphere3d"/>.</param>
		public Sphere4f(Single radius)
		{
			Position = Vector4f.Zero;
			Radius = radius;
		}

		/// <summary>Initialise the <see cref="Sphere4f"/>.</summary>
		/// <param name="position">The centre of the <see cref="Sphere4f"/>. The value will not be modified.</param>
		/// <param name="radius">The radius of the <see cref="Sphere4f"/>.</param>
		public Sphere4f(ref Vector4f position, Single radius)
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
		public Double Area { get { return (Double)((4 * Math.PI) * Radius * Radius); } }

		/// <summary>Get whether the sphere has no content.</summary>
		public bool IsEmpty { get { return Radius == (Double)0; } }
		
		/// <summary>Get or set the centre of the <see cref="Sphere3d"/>.</summary>
		public Vector4d Position;

		/// <summary>Get or set the radius of the <see cref="Sphere3d"/>.</summary>
		public Double Radius;

		/// <summary>Get the Radius**2.</summary>
		public Double RadiusSquared { get { return (Double)(Radius * Radius); } }

		/// <summary>Get the volume of the <see cref="Sphere3d"/>.</summary>
		public Double Volume { get { return (Double)((4.0 / 3.0 * Math.PI) * Radius * Radius * Radius); } }

		/// <summary>Initialise the <see cref="Sphere3d"/>.</summary>
		/// <param name="position">The centre of the <see cref="Sphere3d"/>.</param>
		/// <param name="radius">The radius of the <see cref="Sphere3d"/>.</param>
		public Sphere4d(Vector4d position, Double radius)
		{
			Position = position;
			Radius = radius;
		}

		/// <summary>Initialise a sphere centred at origin.</summary>
		/// <param name="radius">The radius of the <see cref="Sphere3d"/>.</param>
		public Sphere4d(Double radius)
		{
			Position = Vector4d.Zero;
			Radius = radius;
		}

		/// <summary>Initialise the <see cref="Sphere4d"/>.</summary>
		/// <param name="position">The centre of the <see cref="Sphere4d"/>. The value will not be modified.</param>
		/// <param name="radius">The radius of the <see cref="Sphere4d"/>.</param>
		public Sphere4d(ref Vector4d position, Double radius)
		{
			Position = position;
			Radius = radius;
		}
	}
		/// <summary>
	/// A sphere definition, which has a position and radius.
	/// </summary>
	public struct Sphere4
	{
		/// <summary>Get the surface area of the <see cref="Sphere3d"/>.</summary>
		public Area Area { get { return (Area)((4 * Math.PI) * Radius * Radius); } }

		/// <summary>Get whether the sphere has no content.</summary>
		public bool IsEmpty { get { return Radius == Length.Zero; } }
		
		/// <summary>Get or set the centre of the <see cref="Sphere3d"/>.</summary>
		public Vector4 Position;

		/// <summary>Get or set the radius of the <see cref="Sphere3d"/>.</summary>
		public Length Radius;

		/// <summary>Get the Radius**2.</summary>
		public Area RadiusSquared { get { return (Area)(Radius * Radius); } }

		/// <summary>Get the volume of the <see cref="Sphere3d"/>.</summary>
		public Volume Volume { get { return (Volume)((4.0 / 3.0 * Math.PI) * Radius * Radius * Radius); } }

		/// <summary>Initialise the <see cref="Sphere3d"/>.</summary>
		/// <param name="position">The centre of the <see cref="Sphere3d"/>.</param>
		/// <param name="radius">The radius of the <see cref="Sphere3d"/>.</param>
		public Sphere4(Vector4 position, Length radius)
		{
			Position = position;
			Radius = radius;
		}

		/// <summary>Initialise a sphere centred at origin.</summary>
		/// <param name="radius">The radius of the <see cref="Sphere3d"/>.</param>
		public Sphere4(Length radius)
		{
			Position = Vector4.Zero;
			Radius = radius;
		}

		/// <summary>Initialise the <see cref="Sphere4"/>.</summary>
		/// <param name="position">The centre of the <see cref="Sphere4"/>. The value will not be modified.</param>
		/// <param name="radius">The radius of the <see cref="Sphere4"/>.</param>
		public Sphere4(ref Vector4 position, Length radius)
		{
			Position = position;
			Radius = radius;
		}
	}
	}






