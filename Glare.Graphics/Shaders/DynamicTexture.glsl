:: Common ::
// This is added to the beginning of all shaders.
#version 430

uniform sampler2D Input; // Input texture.
uniform vec4 ClearValue;

vec4 texelFetch(sampler2D sampler, vec2 coordinates) { return texelFetch(sampler, ivec2(coordinates), 0); }
vec4 texelFetch(sampler2D sampler, vec2 coordinates, int lod) { return texelFetch(sampler, ivec2(coordinates), lod); }

struct NeighbourSamples
{
	vec4 Left, Right, Bottom, Top;	
};

struct NearbySamples
{
	vec4 Left, Right, Bottom, Top, Centre;
};

void texelFetchNeighbours(sampler2D sampler, ivec2 coordinates, int lod, out vec4 left, out vec4 right, out vec4 bottom, out vec4 top)
{
	left = texelFetch(sampler, coordinates - ivec2(1, 0), lod);
	right = texelFetch(sampler, coordinates + ivec2(1, 0), lod);
	bottom = texelFetch(sampler, coordinates + ivec2(0, 1), lod);
	top = texelFetch(sampler, coordinates - ivec2(0, 1), lod);
}

void texelFetchNeighbours(sampler2D sampler, ivec2 coordinates, out vec4 left, out vec4 right, out vec4 bottom, out vec4 top) { texelFetchNeighbours(sampler, coordinates, 0, left, right, bottom, top); }
void texelFetchNeighbours(sampler2D sampler, vec2 coordinates, int lod, out vec4 left, out vec4 right, out vec4 bottom, out vec4 top) { texelFetchNeighbours(sampler, ivec2(coordinates), lod, left, right, bottom, top); }
void texelFetchNeighbours(sampler2D sampler, vec2 coordinates, out vec4 left, out vec4 right, out vec4 bottom, out vec4 top) { texelFetchNeighbours(sampler, ivec2(coordinates), 0, left, right, bottom, top); }

NeighbourSamples texelFetchNeighbours(sampler2D sampler, ivec2 coordinates, int lod)
{
	NeighbourSamples result;
	texelFetchNeighbours(sampler, coordinates, lod, result.Left, result.Right, result.Bottom, result.Top);
	return result;
}

NeighbourSamples texelFetchNeighbours(sampler2D sampler, ivec2 coordinates) { return texelFetchNeighbours(sampler, coordinates, int(0)); }
NeighbourSamples texelFetchNeighbours(sampler2D sampler, vec2 coordinates, int lod) { return texelFetchNeighbours(sampler, ivec2(coordinates), lod); }
NeighbourSamples texelFetchNeighbours(sampler2D sampler, vec2 coordinates) { return texelFetchNeighbours(sampler, ivec2(coordinates), 0); }

NearbySamples texelFetchNearby(sampler2D sampler, ivec2 coordinates, int lod)
{
	NearbySamples result;
	texelFetchNeighbours(sampler, coordinates, lod, result.Left, result.Right, result.Bottom, result.Top);
	result.Centre = texelFetch(sampler, coordinates, lod);
	return result;
}

NearbySamples texelFetchNearby(sampler2D sampler, ivec2 coordinates) { return texelFetchNearby(sampler, coordinates, 0); }
NearbySamples texelFetchNearby(sampler2D sampler, vec2 coordinates, int lod) { return texelFetchNearby(sampler, ivec2(coordinates), lod); }
NearbySamples texelFetchNearby(sampler2D sampler, vec2 coordinates) { return texelFetchNearby(sampler, ivec2(coordinates), 0); }

vec4 texelFetchBilinear(sampler2D sampler, ivec2 coordinates, int lod)
{
	vec4 st;
	st.xy = floor(coordinates - 0.5) + 0.5;
	st.zw = st.xy + 1;

	vec2 t = coordinates - st.xy; // Interpolating factors
	vec4 tex11 = texelFetch(sampler, ivec2(st.xy), lod);
	vec4 tex21 = texelFetch(sampler, ivec2(st.zy), lod);
	vec4 tex12 = texelFetch(sampler, ivec2(st.xw), lod);
	vec4 tex22 = texelFetch(sampler, ivec2(st.zw), lod);

	return mix(mix(tex11, tex21, t.x), mix(tex12, tex22, t.x), t.y);
}

vec4 texelFetchBilinear(sampler2D sampler, ivec2 coordinates) { return texelFetchBilinear(sampler, coordinates, 0); }
vec4 texelFetchBilinear(sampler2D sampler, vec2 coordinates, int lod) { return texelFetchBilinear(sampler, ivec2(coordinates), lod); }
vec4 texelFetchBilinear(sampler2D sampler, vec2 coordinates) { return texelFetchBilinear(sampler, ivec2(coordinates), 0); }

:: Vertex ::

// Built-in attributes:
// in int gl_VertexID; // The index of the vertex currently being processed. When using non-indexed rendering, it is the effective index of the current vertex (the number of vertices processed + the first​ value). For indexed rendering, it is the index used to fetch this vertex from the buffer. Note: this​ will have the base vertex applied to it.
// in int gl_InstanceID; // The index of the current instance when doing some form of instanced rendering. The instance count always starts at 0, even when using base instance calls. When not using instanced rendering, this value will be 0.

// Predefined outputs:
//	out gl_PerVertex
//	{
//		vec4 gl_Position; // The clip-space output position of the current vertex.
//		float gl_PointSize; // The pixel width/height of the point being rasterized. It only has a meaning when rendering point primitives.
//		float gl_ClipDistance[]; // Allows the shader to set the distance from the vertex to each clip plane. A positive distance means that the vertex is inside/behind the clip plane, and a negative distance means it is outside/in front of the clip plane. Each element in the array is one clip plane. In order to use this variable, the user must manually redeclare it with an explicit size.
//	}

in vec2 Position;
out vec2 Texel; // Texel coordinates in 0-1 range.

void main()
{
	gl_Position = vec4(Position, 0, 1);
	Texel = (Position + 1) / 2;
}

:: Fragment ::

in vec2 Texel; // Texel coordinates in 0-1 range.

out vec4 Output;

vec2 Coordinates; // Texel coordinates in the 0-width/height range.

subroutine void Action();

subroutine uniform Action Act;

subroutine(Action) void Clear()
{
	Output = ClearValue;
}

subroutine(Action) void Clear2() { Output = ClearValue * 2; }

:: #include "FluidTexture.glsl" ::

void main()
{
	Coordinates = Texel * textureSize(Input, 0);
	Act();
 }
