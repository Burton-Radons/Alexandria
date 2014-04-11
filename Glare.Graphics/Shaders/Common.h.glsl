:: Vertex ::
// Vertex Shader
:: Fragment ::
// Fragment Shader
:: Common ::
#version 430

#define PI 3.14159265358979323846264338327950288419716939937510

// Two-dimensional mix.
#define mix2(AA, BA, AB, BB, T) mix(mix((AA), (BA), (T).x), mix((AB), (BB), (T).x), (T).y)

// Three-dimensional mix.
#define mix3(AAA, BAA, ABA, BBA, AAB, BAB, ABB, BBB, T) mix(mix2((AAA), (BAA), (ABA), (BBA), (T).xy), mix((AAB), (BAB), (ABB), (BBB), (T).xy), (T).z)

float gaussian(vec2 position, float radius) { return exp(-dot(position, position) / radius); }

vec4 textureBilinear(sampler2D source, vec2 coordinate, vec2 sourceSize) {
	vec2 reciprocalSourceSize = 1 / sourceSize;
	coordinate *= sourceSize;
	//coordinate = (coordinate % 1.0 + 1.0) % 1.0 * GridScale;
	vec2 partial = mod(coordinate, 1.0);
	vec2 corner = floor(coordinate) * reciprocalSourceSize;
	vec2 unitX = vec2(reciprocalSourceSize.x, 0), unitY = vec2(0, reciprocalSourceSize.y);

	return mix2(texture(source, corner), texture(source, corner + unitX),
		texture(source, corner + unitY), texture(source, corner + reciprocalSourceSize), partial);
}

vec4 textureBilinearLod(sampler2D source, vec2 coordinate, vec2 sourceSize, float lod) {
	vec2 reciprocalSourceSize = 1 / sourceSize;
	coordinate *= sourceSize;
	//coordinate = (coordinate % 1.0 + 1.0) % 1.0 * GridScale;
	vec2 partial = mod(coordinate, 1.0);
	vec2 corner = floor(coordinate) * reciprocalSourceSize;
	vec2 unitX = vec2(reciprocalSourceSize.x, 0), unitY = vec2(0, reciprocalSourceSize.y);

	return mix2(
		textureLod(source, corner, lod), textureLod(source, corner + unitX, lod),
		textureLod(source, corner + unitY, lod), textureLod(source, corner + unitX + unitY, lod), partial);
}

/// Bilinear blend between two levels of detail.
vec4 textureLodBilinear(sampler2D source, vec2 texel, float lod, float lodBlend) {
	return mix(
		textureLod(source, texel, lod),
		textureLod(source, texel, lod + 1), lodBlend);
}

/// Bilinear blend between two levels of detail with different sample points for each level.
vec4 textureLodBilinear2(sampler2D source, vec2 texela, vec2 texelb, float lod, float lodBlend) {
	return mix(
		textureLod(source, texela, lod),
		textureLod(source, texelb, lod + 1), lodBlend);
}

vec4 transformWVP(vec4 position, mat4 world, mat4 view, mat4 projection) { return position * world * view *  projection; }
vec4 transformWVP(vec3 position, mat4 world, mat4 view, mat4 projection) { return transformWVP(vec4(position, 1), world, view, projection); }
vec3 transformNormalWVP(vec3 normal, mat4 world, mat4 view, mat4 projection) { return normalize(transformWVP(vec4(normal, 0), world, view, projection).xyz); }

float axialSum(vec2 value) { return value.x + value.y; }
float axialSum(vec3 value) { return value.x + value.y + value.z; }
float axialSum(vec4 value) { return value.x + value.y + value.z + value.w; }

// Return the maximum of the axes.
float maxAxis(float value) { return value; }
float maxAxis(vec2 value) { return max(value.x, value.y); }
float maxAxis(vec3 value) { return max(value.x, max(value.y, value.z)); }
float maxAxis(vec4 value) { return max(max(value.x, value.y), max(value.z, value.w)); }

vec4 toFloat4(float value) { return vec4(value, 0, 0, 0); }
vec4 toFloat4(vec2 value) { return vec4(value, 0, 0); }
vec4 toFloat4(vec3 value) { return vec4(value, 0); }
vec4 toFloat4(vec4 value) { return value; }

///////////////////////////////////////////////////////////////////////////////
// Textures ///////////////////////////////////////////////////////////////////

// Get the height of the neighbors. x is left, y is right, z is up, and w is down.
vec4 heightmapNeighbors(sampler2D source, vec2 texel, vec2 texelSize) {
	vec2 texelX = vec2(texelSize.x, 0), texelY = vec2(0, texelSize.y);
	return vec4(
		textureLod(source, texel - texelX, 0).x,
		textureLod(source, texel + texelX, 0).x,
		textureLod(source, texel - texelY, 0).x,
		textureLod(source, texel + texelY, 0).x);
}

// Get the height of the neighbors relative to the centre.
vec4 heightmapRelativeNeighbors(sampler2D source, vec2 texel, vec2 texelSize) {
	return heightmapNeighbors(source, texel, texelSize) - textureLod(source, texel, 0).x;
}

// Get the angle of the neighbors relative to the centre.
vec4 heightmapNeighborTilt(sampler2D source, vec2 texel, vec2 texelSize) {
	return atan(heightmapRelativeNeighbors(source, texel, texelSize), vec4(1, 1, 1, 1));
}

///////////////////////////////////////////////////////////////////////////////
// Structures /////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////

struct Plane
{
	vec3 normal; // Direction of the Plane (normalized).
	float offset; // Offset along the Normal to the surface of the Plane.
};

struct Ray
{
	vec3 origin; // Origin of the Ray.
	vec3 direction; // Direction of the Ray (normalized).
};

///////////////////////////////////////////////////////////////////////////////
// Plane //////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////

// Normalize the normal for this Plane.
Plane createPlane(vec3 normal, float offset) { return Plane(normalize(normal), offset); }

///////////////////////////////////////////////////////////////////////////////
// Ray ////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////

// Create a Ray with a potentially non-unit Direction.
Ray createRay(vec3 origin, vec3 direction) { return Ray(origin, normalize(direction)); }

///////////////////////////////////////////////////////////////////////////////
// Normal Storage /////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////

// Each of these inputs expect a normalized normal.
// Each of these outputs might not produce a perfectly unit normal.
// All encoded output is in the 0-1 range.

// Code a normal as a vec3 in the 0-1 range for storage in a color.
vec3 encodeNormal(vec3 value) { return value * 0.5 + 0.5; }
vec3 decodeNormal(vec3 value) { return value * 2 - 1; }

// Code a normal as a vec2 in the 0-1 range for storage in a color.
// The Y value is dropped and then reconstructed as a positive value.
vec2 encodeNormalReconstructY(vec3 value) { return value.xz * 0.5 + 0.5; }
vec3 decodeNormalReconstructY(vec2 value) {
	vec3 normal;
	normal.xz = value * 2 - 1;
	normal.y = sqrt(1 - dot(normal.xz, normal.xz));
	return normal;
}

// Code a normal as a vec2 in the 0-1 range for storage in a color.
// The Z value is dropped and reconstructed as a positive value.
vec2 encodeNormalReconstructZ(vec3 value) { return value.xy * 0.5 + 0.5; }
vec3 decodeNormalReconstructZ(vec2 value) {
	vec3 normal;
	normal.xy = value * 2 - 1;
	normal.z = sqrt(1 - dot(normal.xy, normal.xy));
	return normal;
}

// Code a normal as a rotation and a Z value.
vec2 encodeNormalSphericalCoordinates(vec3 normal) {
	return vec2(atan(normal.y, normal.x) / PI, normal.z) * 0.5 + 0.5;
}

vec3 decodeNormalSphericalCoordinates(vec2 value) {
	vec2 angle = value * 2 - 1;
	vec2 sinCosTheta;

	sinCosTheta.x = sin(angle.x * PI);
	sinCosTheta.y = cos(angle.y * PI);
	float sinCosPhi = sqrt(1 - angle.y * angle.y);
	return vec3(sinCosTheta.y * sinCosPhi, sinCosTheta.x * sinCosPhi, angle.y);
}

// Code a normal as a spheremap.
vec2 encodeNormalSphereMap(vec3 normal) {
    return normal.xy / sqrt(normal.z * 8 + 8) + 0.5;
}

vec3 decodeNormalSphereMap(vec2 value) {
    vec2 fenc = value * 4 - 2;
    float f = dot(fenc, fenc);
    float g = sqrt(1-f/4);
    vec3 normal;

    normal.xy = fenc * g;
    normal.z = 1 - f / 2;

    return normal;
}

vec4 encode16Bit(vec2 value) { return vec4(mod(value, 1.0 / 256) * 256, value - mod(value, 1.0 / 256)); }
vec2 decode16Bit(vec4 value) { return vec2(value.xy / 256 + value.zw); }

vec4 encode14Bit(vec2 value) { return vec4(mod(value, 1.0 / 16) * 16, value - mod(value, 1.0 / 16)); }
vec2 decode14Bit(vec4 value) { return vec2(value.xy / 16 + value.zw); }

vec4 encodeNormalSphereMap28(vec3 normal) { return encode14Bit(encodeNormalSphereMap(normal)); }
vec3 decodeNormalSphereMap28(vec4 value) { return decodeNormalSphereMap(decode14Bit(value)); }