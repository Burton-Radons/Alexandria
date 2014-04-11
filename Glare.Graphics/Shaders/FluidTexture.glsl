/*********************************************************************************************************************/
/** Fluid Dynamics **************************************************************************************************/
/*********************************************************************************************************************/

uniform float fluidTimestep;
uniform float fluidAlpha;
uniform float fluidRBeta;

/// This program performs a semi-lagrangian advection of a passive field by a moving velocity field.  It works by tracing backwards from each fragment along the velocity field, and moving the passive value at its destination forward to the starting point.  It performs bilinear interpolation at the  destination to get a smooth resulting field.
 vec4 fluidAdvect(vec2 coordinates,
	float timestep,
	float dissipation, // mass dissipation constant
	float inverseGridScale, // 1 / grid scale
	sampler2D inputVelocity, // the velocity field
	sampler2D advectQuantity) // the field to be advected
{
	// Trace backwards along trajectory (determined by current velocity) distance = rate * time, but since the grid might not be unit-scale, we need to also scale by the grid cell size.
	vec2 position = coordinates - timestep * inverseGridScale * texelFetch(inputVelocity, coordinates).xy;

	// Example:
	//    the "particle" followed a trajectory and has landed like this:
	//
	//   (x1,y2)----(x2,y2)    (xN,yN)
	//      |          |    /----/  (trajectory: (xN,yN) = start, x = end)
	//      |          |---/
	//      |      /--/|    ^
	//      |  pos/    |     \_ v.xy (the velocity)
	//      |          |
	//      |          |
	//   (x1,y1)----(x2,y1)
	//
	// x1, y1, x2, and y2 are the coordinates of the 4 nearest grid points around the destination.  We compute these using offsets and the floor  operator.  The "-0.5" and +0.5 used below are due to the fact that the centers of texels in a TEXTURE_RECTANGLE_NV are at 0.5, 1.5, 2.5,  etc.
  
	// The function f4texRECTbilerp computes the above 4 points and interpolates a value from texture lookups at each point.Rendering this value will  effectively place the interpolated value back at the starting point of the advection.
  
	// So that we can have dissipating scalar fields (like smoke), we multiply the interpolated value by a [0, 1] dissipation scalar  (1 = lasts forever, 0 = instantly dissipates.  At high frame rates,  useful values are in [0.99, 1].
	return dissipation * texelFetchBilinear(advectQuantity, position);
}

/// This program computes the divergence of the specified vector field  "velocity". The divergence is defined as 
///
/// "grad dot v" = partial(v.x)/partial(x) + partial(v.y)/partial(y),
///
/// and it represents the quantity of "stuff" flowing in and out of a parcel of fluid.  Incompressible fluids must be divergence-free.  In other words this quantity must be zero everywhere.  
float fluidDivergence(vec2 coordinates,  // grid coordinates
	float halfInverseGridScale, // 0.5 / gridScale
	sampler2D vectorField)
{
	NeighbourSamples vector = texelFetchNeighbours(vectorField, coordinates);
	return halfInverseGridScale * (vector.Right.x - vector.Left.x + vector.Top.y - vector.Bottom.y);
}

/// This program performs a single Jacobi relaxation step for a poisson equation of the form
///
///               Laplacian(U) = b,
///
/// where U = (u, v) and Laplacian(U) is defined as 
///
///  grad(div x) = grad(grad dot x) =  partial^2(u)/(partial(x))^2 + partial^2(v)/(partial(y))^2
///
/// solution of the equation can be found iteratively, by using this iteration:
///
/// U'(i,j) = (U(i-1,j) + U(i+1,j) + U(i,j-1) + U(i,j+1) + b) * 0.25
///
/// That is what this routine does.  To maintain flexibility for slightly different poisson problems (such as viscous diffusion), we provide two parameters, centerFactor and stencilFactor.  These are useful for non-unit-scale grids, and when there is a coefficient on the RHS of the poisson equation.
///
/// This program works for both scalar and vector equations.
vec4 fluidJacobi(vec2 coordinates,
	float alpha,
	float reciprocalBeta,
	sampler2D xSampler, // x vector (Ax = b)
	sampler2D bSampler) // b vector (Ax = b)
{
	NeighbourSamples x = texelFetchNeighbours(xSampler, coordinates);
	vec4 bCentre = texelFetch(bSampler, coordinates);
	return (x.Left + x.Right + x.Bottom + x.Top + alpha * bCentre) * reciprocalBeta;
}

/// This program implements the final step in the fluid simulation.  After the poisson solver has iterated to find the pressure disturbance caused by the divergence of the velocity field, the gradient of that pressure needs to be subtracted from this divergent velocity to get a divergence-free velocity field:
///
/// v-zero-divergence = v-divergent -  grad(p)
///
/// The gradient(p) is defined: 
///    grad(p) = (partial(p)/partial(x), partial(p)/partial(y))
///
///  The discrete form of this is:
///      grad(p) = ((p(i+1,j) - p(i-1,j)) / 2dx, (p(i,j+1)-p(i,j-1)) / 2dy)
/// 
///  where dx and dy are the dimensions of a grid cell.
/// 
///  This program computes the gradient of the pressure and subtracts it from the velocity to get a divergence free velocity.

vec4 fluidGradient(vec2 coordinates, // grid coordinates
	float halfReciprocalGridScale, // 0.5 / gridScale
	sampler2D pressureSampler,
	sampler2D velocitySampler)
{
	NeighbourSamples pressure = texelFetchNeighbours(pressureSampler, coordinates);

	vec2 gradient = vec2(pressure.Right.x - pressure.Left.x, pressure.Top.x - pressure.Bottom.x) * halfReciprocalGridScale;
	vec4 velocity = texelFetch(velocitySampler, coordinates);
	return vec4(velocity.xy - gradient, velocity.zw);
} 

/// This program is used to compute neumann boundary conditions for solving poisson problems.  The neumann boundary condition for the poisson equation says that partial(u)/partial(n) = 0, where n is the normal direction of the inside of the boundary.  This simply means that the value of the field does not change across the boundary in the normal direction.
/// 
/// In the case of our simple grid, this simply means that the value of the  field at the boundary should equal the value just inside the boundary.
///
/// We allow the user to specify the direction of "just inside the boundary" by using texture coordinate 1.
///
/// Thus, to use this program on the left boundary, TEX1 = (1, 0):
///
/// LEFT:   TEX1=( 1,  0)
/// RIGHT:  TEX1=(-1,  0)
/// BOTTOM: TEX1=( 0,  1)
/// TOP:    TEX1=( 0, -1)

vec4 fluidBoundary(vec2 coordinates,
	vec2 offset,
	float scale,
	sampler2D xSampler)
{
	return scale * texelFetch(xSampler, ivec2(coordinates + offset), 0);
}

/// This program is used to compute boundary value lookup offsets for  implementing boundary conditions around arbitrary boundaries inside the  flow field.
///
/// This program is run only when the arbitrary interior boundaries change.   Each cell can either be fluid or boundary.  A zero in the boundaries texture indicates fluid, a 1 indicates boundary.
///
/// The trick here is to use the boundary (0,1) values of the neighbors of a  cell to compute a single 4-vector containing the x and y offsets needed to compute the correct boundary conditions.
///
/// A clever encoding enables this.  A "stencil" is used to multiply and add the neighbors and the center cell.  The stencil values are picked such that each configuration has a unique value:
///
///    |   |  3 |   |
///    | 7 | 17 | 1 |
///    |   |  5 |   |
///
/// The result is that we can precompute all possible configurations and store the appropriate offsets for them (see Flo : : _CreateOffsetTextures() in  flo.cpp) in a 1D lookup table texture.  Then we use this unique stencil  value as the texture coordinate.
///
/// All of these texture reads (one per neighbor) are expensive, so we only do this when the boundaries change, and then write them to an offset  texture.  Two lookups into this texture allow the arbitrary*Boundaries() programs to compute pressure and velocity boundary values efficiently.

vec4 fluidUpdateOffsets(vec2 coordinates,
	sampler2D bSampler,
	sampler2D offsetTable)
{
	NeighbourSamples b = texelFetchNeighbours(bSampler, coordinates); // get neighboring boundary values (on or off)
	float bCentre = texelFetch(bSampler, coordinates).x; // center cell

	// compute offset lookup index by adding neighbors...
	// the strange offsets ensure a unique index for each possible configuration
	float index = 3 * b.Top.x + b.Right.x + 5 * b.Bottom.x + 7 * b.Left.x + 17 * bCentre;

	// get scale and offset = (uScale, uOffset, vScale, vOffset)
	return texelFetch(offsetTable, ivec2(index, 0), 0);
}

/// This program uses the offset texture computed by the program above to implement arbitrary no-slip velocity boundaries.  It is essentially the same in operation as the edge boundary program above, but it requires an initial texture lookup to get the offsets (they can't be provided as a uniform parameter because they change at each cell).  It must then offset differently in x and y, so it requires two lookups to compute the boundary values.
vec4 fluidArbitraryVelocityBoundary(vec2 coordinates,
	sampler2D velocitySampler,
	sampler2D offsetsSampler)
{
	vec4 uNew;

	// Get scale and offset (uScale, uOffset, vScale, vOffset)
	vec4 scaleOffset = texelFetch(offsetsSampler, coordinates);

	// Compute the x boundary value
	uNew.x = scaleOffset.x * texelFetch(velocitySampler, coordinates + vec2(0, scaleOffset.y)).x;

	// Compute the y boundary value
	uNew.y = scaleOffset.z * texelFetch(velocitySampler, coordinates + vec2(scaleOffset.w, 0), 0).y;
	uNew.zw = vec2(0, 0);
	return uNew;
}

/// This program is used to implement pure-neumann pressure boundary conditions around arbitrary boundaries.  This program operates in essentially the same manner as arbitraryVelocityBoundary, above. Returns pNew.

vec4 fluidArbitraryPressureBoundary(vec2 coordinates,
	sampler2D pSampler,
	sampler2D offsetsSampler)
{
	// Get the two neighboring pressure offsets; they will be the same if this is N, E, W, or S, different if NE, SE, etc.
	vec4 offset = texelFetch(offsetsSampler, coordinates);

	return 0.5 * (texelFetch(pSampler, coordinates + offset.xy) + texelFetch(pSampler, coordinates + offset.zw));
}

/// The motion of smoke, air and other low-viscosity fluids typically contains rotational flows at a variety of scales. This rotational flow is called vorticity.  As Fedkiw et al. explained (2001), numerical dissipation caused by simulation on a coarse grid damps out these interesting features.  Therefore, they used "vorticity confinement" to restore these fine-scale motions. Vorticity confinement works by first computing the vorticity,
///                          vort = curl(u). 
/// The program vorticity() does this computation. From the vorticity we compute a normalized vorticity vector field, 
///                          F = normalize(eta),	
/// where, eta = grad(|vort|). The vectors in F point from areas of lower vorticity to areas of higher vorticity. From these vectors we compute a force that can be used to restore an approximation of the dissipated vorticity:
///                          vortForce = eps * cross(F, vort) * dx.	
/// Here eps is a user-controlled scale parameter. 
/// 
/// The operations above require two passes in the simulator.  This is because the vorticity must be computed in one pass, because computing the vector field F requires sampling multiple vorticity values for each vector. Because a texture can't be written and then read in a single pass, this is inherently a two-pass algorithm.

/// The first pass of vorticity confinement computes the (scalar) vorticity field.  See the description above.  In Flo, if vorticity confinement is disabled, but the vorticity field is being displayed, only this first pass is executed.

float fluidVorticity(vec2 coordinates,
	float halfReciprocalGridScale, // 0.5 / gridScale
	sampler2D velocitySampler)
{
	NeighbourSamples velocity = texelFetchNeighbours(velocitySampler, coordinates);
	return halfReciprocalGridScale * ((velocity.Right.y - velocity.Left.y) - (velocity.Top.x - velocity.Bottom.x));
}

/// The second pass of vorticity confinement computes a vorticity confinement force field and applies it to the velocity field to arrive at a new velocity field. Returns velocityNew.
vec4 fluidVorticityForce(vec2 coordinates,
	float halfReciprocalGridScale, // 0.5 / gridScale
	vec2 vorticityConfinementScale,
	float timestep,
	sampler2D vorticitySampler,
	sampler2D velocitySampler)
{
	NearbySamples vorticity = texelFetchNearby(vorticitySampler, coordinates);
	vec2 force = halfReciprocalGridScale * vec2(abs(vorticity.Top.x) - abs(vorticity.Bottom.x), abs(vorticity.Right.x) - abs(vorticity.Left.x));

	// Safe normalize
	float epsilon = 2.4414e-4; // 2^-12
	float magnitudeSquared = max(epsilon, dot(force, force));
	force *= inversesqrt(magnitudeSquared);
	force *= vorticityConfinementScale * vorticity.Centre.xy * vec2(1, -1);

	vec4 velocityNew = texelFetch(velocitySampler, coordinates);
	return velocityNew + timestep * vec4(force, 0, 0);
} 

/// The following four programs simply display rectangle textures.  A fragment program is required on NV3X to display floating point textures.  The scale and bias parameters allow the manipulation of the values in the texture before display.  This is useful, for example, if the values in the texture are signed.  A scale and bias of 0.5 can bring the range [-1, 1] into the range [0, 1] for  for visualization or other purposes.
///
/// The four versions of the program are for displaying with and without bilinear interpolation (smoothing), and for scalar and vector textures.

vec4 fluidDisplayScalar(vec2 coordinates,
	vec4 scale,
	vec4 bias,
	sampler2D texture)
{
	return bias + scale * texelFetch(texture, coordinates).xxxx;
}

vec4 fluidDisplayVector(vec2 coordinates,
	vec4 scale,
	vec4 bias,
	sampler2D texture)
{
	return bias + scale * texelFetch(texture, coordinates);
}

vec4 fluidDisplayScalarBilinear(vec2 coordinates,
	vec4 scale,
	vec4 bias,
	sampler2D texture)
{
	return bias + scale * texelFetchBilinear(texture, coordinates).xxxx;
}

vec4 fluidDisplayVectorBilinear(vec2 coordinates,
	vec4 scale,
	vec4 bias,
	sampler2D texture)
{
	return bias + scale * texelFetchBilinear(texture, coordinates);
}

#if false
// Apply the first 3 operators in equation 12
u = advect(u)
u = diffuse(u)
u = addForces(u)
// Now apply the projection operator to the result
p = computePressure(u)
u = subtractPressureGradient(p)

#endif
