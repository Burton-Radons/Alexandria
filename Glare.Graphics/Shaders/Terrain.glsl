:: #include "Common.h.glsl" ::

#define encodeNormal encodeNormalSphereMap28
#define decodeNormal decodeNormalSphereMap28

uniform mat4 world;
uniform mat4 view;
uniform mat4 projection;

//Texture2D HeightTexture, NormalTexture, RandomTexture, ColorTexture, waterHeightTexture;
uniform float skirtDepth = 20;
uniform int terrainSize = 128;
uniform float colorTextureStrength = 0;
uniform vec3 waterColor;

//SamplerState heightTextureState = sampler_state { texture = <HeightTexture>; filter = min_mag_mip_point;  AddressU = mirror; AddressV = mirror; };
//SamplerState normalTextureState = sampler_state { texture = <NormalTexture>; filter = min_mag_mip_linear; AddressU = wrap;   AddressV = wrap;   };
//SamplerState randomTextureState = sampler_state { texture = <RandomTexture>; filter = min_mag_mip_linear; AddressU = wrap;   AddressV = wrap;   };
//SamplerState colorTextureState  = sampler_state { texture = <ColorTexture>;  filter = min_mag_mip_linear; AddressU = wrap;   AddressV = wrap;   };
//SamplerState waterHeightTextureState = sampler_state { texture = <waterHeightTexture>;  filter = min_mag_mip_point; AddressU = wrap;   AddressV = wrap;   };

/*#define heightTexture CreateSampler2D(HeightTexture, heightTextureState)
#define normalTexture CreateSampler2D(NormalTexture, normalTextureState)
#define randomTexture CreateSampler2D(RandomTexture, randomTextureState)
#define colorTexture CreateSampler2D(ColorTexture, colorTextureState)
#define waterHeightTexture CreateSampler2D(waterHeightTexture, waterHeightTextureState)*/

uniform sampler2D heightTexture, normalTexture, randomTexture, colorTexture, waterHeightTexture;

struct TerrainInfo
{
	// The world position of the terrain point, one unit per texel. x is the horizontal plan, y is the height plane, and z is the cross plane.
	vec3 position;

	// The texture coordinates for this vertex.
	vec2 texel;

	// The height of this vertex. Note that position.y will not have this value if it's in the skirt.
	float height;

	// The normal at this position.
	vec3 normal;

	vec2 blockTexel;

	float lod;
};

struct TerrainVertexShaderInput
{
	vec4 position;
	vec4 parameters1;
	vec4 parameters2;
};

// A function that can return the world position based upon the input position to the vertex shader and the parameters field.
// There are two sections to this function.
// Per-vertex input:
//		inputPosition - The POSITION0 value.
// Per-instance input:
//		p1 - The BLENDWEIGHT0 value.
//		p2 - The BLENDWEIGHT1 value.
// Uniforms:
//		skirtDepth - The number of units below the terrain that the skirt should be drawn down to.
//		heightTexture - The source of the height values. Note that this sampler must have point mag/min/mipfilters.
TerrainInfo decodeTerrainInfo(vec4 inputPosition, vec4 p1, vec4 p2, float skirtDepth, sampler2D heightTexture, int terrainSize)
{
	float lod = floor(p1.z), lodBlend = smoothstep(0, 1, 1 - fract(p1.z));
	float texelScale = p1.w;
	float scale = pow(2, lod), nextScale = exp2(lod + 1);
	vec2 unitPosition = inputPosition.xz / 16;
	vec2 unitCorner = p1.xy;
	float texelSize = scale / terrainSize, nextTexelSize = nextScale / terrainSize;

	vec2 texel = unitCorner + unitPosition * texelScale;
	vec2 lodDeflect = mod(inputPosition.xz, 2) * scale * lodBlend;

	// Calculate the heights.
	float height = textureLodBilinear(heightTexture, texel, lod, lodBlend).x;
	float heightRight = textureLodBilinear2(heightTexture, texel + vec2(texelSize, 0), texel + vec2(nextTexelSize, 0), lod, lodBlend).x;
	float heightDown = textureLodBilinear2(heightTexture, texel + vec2(0, texelSize), texel + vec2(0, nextTexelSize), lod, lodBlend).x;

	// Calculate the world normal.
	vec2 normalOffset = vec2(texelSize * lodBlend * 0.5 + texelSize);
	vec3 normal = -normalize(cross(vec3(normalOffset.x, heightRight - height, 0), vec3(0, heightDown - height, normalOffset.y)));

	// Calculate the world position.
	vec3 position = inputPosition.xyz;
	position.xz *= scale;
	position.xz += unitCorner * terrainSize - lodDeflect;
	position.y = height - skirtDepth * (lod + lodBlend) * (1 - position.y);

	// Construct the result data.
	TerrainInfo info;
	info.blockTexel = inputPosition.xz - lodDeflect / scale;
	info.height = height;
	info.texel = texel - lodDeflect / terrainSize;
	info.position = position;
	info.normal = normal;
	info.lod = lod;
	return info;
}

TerrainInfo decodeTerrainInfo(TerrainVertexShaderInput data, float skirtDepth, sampler2D heightTexture, int terrainSize) { return decodeTerrainInfo(data.position, data.parameters1, data.parameters2, skirtDepth, heightTexture, terrainSize); }

TerrainInfo decodeTerrainInfo(TerrainVertexShaderInput data) { return decodeTerrainInfo(data, skirtDepth, heightTexture, terrainSize); }

struct VertexShaderOutput
{
	vec4 position;
	vec4 screenPosition;
	vec4 texel;
	vec2 blockTexel;
	vec3 viewNormal;
};

:: Vertex ::

in vec4 vertex;
out vec4 position;
out VertexShaderOutput data;

:: Fragment ::

in vec4 position;
out vec4 fragment;
in VertexShaderOutput data;

:: Common ::

///////////////////////////////////////////////////////////////////////////////
/// Draw Shader ///////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////

uniform bool tintLod = false;
uniform bool highlightEdges = false;

:: Vertex ::

in vec4 drawPosition;
in vec4 drawParameters1;
in vec4 drawParameters2;

subroutine(Shader)
void drawVertexShader()
{
	TerrainInfo info = decodeTerrainInfo(TerrainVertexShaderInput(drawPosition, drawParameters1, drawParameters2));

	vec4 worldPosition = mul(vec4(info.position, 1), world);
	vec4 viewPosition = mul(worldPosition, view);
	data.position = data.screenPosition = mul(viewPosition, projection);
	data.viewNormal = normalize(mul(mul(vec4(info.normal, 0), world), view).xyz);
	//data.viewNormal = info.normal;
	data.texel = vec4(info.texel, info.lod, 0);
	data.blockTexel = info.blockTexel;

	//data.texel = vec2(p2.x, info.height);
}

:: Fragment :: 

subroutine(Shader)
void drawFragmentShader()
{
	VertexShaderOutput data = data;
	vec3 viewNormal = normalize(data.viewNormal);
	vec4 textureNormal = texture(normalTexture, data.texel.xy);
	vec3 color = vec3(1, 1, 1);
	float lod = data.texel.z + 1;
	vec2 texel = data.texel.xy;

	color = mix(color, texture(colorTexture, data.texel.xy).xyz, colorTextureStrength);

	if(tintLod)
	{
		float d = 3;
		color = mod(floor(vec3(lod, lod / d, lod / d / d)), d) / (d - 1);
	}

	if(highlightEdges)
	{
		float trip = 1 / lod;
		color = mix(color, vec3(0.1, 0.1, 0.1), 1 - smoothstep(trip, trip + trip / 5 * lod, 
			min(min(data.blockTexel.x, data.blockTexel.y),
			min(16 - data.blockTexel.x, 16 - data.blockTexel.y))));
	}

	// This detects whether the normal texture is present and valid.
	if(length(textureNormal) >= 0.1)
		viewNormal = normalize(mul(mul(vec4(decodeNormal (textureNormal), 0), world), view)).xyz;

	//return vec4(vec3(1, 1, 1) * (data.texel.y + scale) / (scale * 2), 1);

	//return vec4(abs(viewNormal), 1);
	//return vec4(vec3(1, 1, 1) / (data.texel.x + 0), 1);
	color = dot(viewNormal, normalize(vec3(0, 0, 1))) * color;

	// Cause a random dither to hide gradient problems at 24-bit.
	color += (texture(randomTexture, data.screenPosition.xy * 4).xyz - 0.5) / 256.0;

	fragment = vec4(color, 1);
	//return vec4(abs(dot(viewNormal, vec3(0, 0, 1))) * vec3(1, 1, 1), 1);
	//return vec4(abs(data.texel) * abs(dot(viewNormal, vec3(0, 1, 0))), 1);
}

:: Common ::

///////////////////////////////////////////////////////////////////////////////
/// Water Shader //////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////

VertexShaderOutput drawWaterVertexShader(TerrainVertexShaderInput data)
{
	TerrainInfo info = decodeTerrainInfo(data, 0, heightTexture, terrainSize);
	VertexShaderOutput result;

	info.position.y +=textureLod(waterHeightTexture, info.texel, 0).x;
	vec4 worldPosition = mul(vec4(info.position, 1), world);
		vec4 viewPosition = mul(worldPosition, view);
		result.position = result.screenPosition = mul(viewPosition, projection);
	result.viewNormal = normalize(mul(mul(vec4(info.normal, 0), world), view).xyz);
	//result.viewNormal = info.normal;
	result.texel = vec4(info.texel, info.lod, 0);
	result.blockTexel = info.blockTexel;

	//result.texel = vec2(p2.x, info.height);
	return result;
}

vec4 drawWaterFragmentShader(VertexShaderOutput data)
{
	float water = texture(waterHeightTexture, data.texel.xy).x;

	return vec4(waterColor, 1 - log(4) / log(water + 1) + 0.1);
}

///////////////////////////////////////////////////////////////////////////////
/// Clean Height Shader ///////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////
// This shader takes an input texture (using HeightTexture) and for each pixel
// samples the next 16 pixels, using world to transform texel coordinate
// offsets. It records the minimum and maximum values. The sampled values
// are transformed with view, which allows it to replicate X for the first
// pass.

:: Vertex ::

void cleanVertexShader(vec2 inputPosition, out vec4 position, out vec2 texel, out vec2 deltaTexel)
{
	position = vec4(inputPosition * 2 - 1, 0, 1);
	//position.y = 1 - position.y;
	texel = inputPosition;
	texel.y = 1 - texel.y;
	deltaTexel = mul(vec4(1, 1, 1, 1), world).xy;
}

:: Fragment ::

vec4 cleanFragmentShader(vec4 position, vec2 startTexel, vec2 deltaTexel)
{
	vec2 result;
	vec4 value;

	//return vec4((startTexel + deltaTexel) * 64, 0, 1);
	result = mul(textureLod(heightTexture, startTexel, 0), view).xy;

	for(int i = 1; i < 16; i ++) {
		vec2 texel = startTexel + deltaTexel * i / 16;
			value = mul(textureLod(heightTexture, texel, 0), view);
		result.x = min(result.x, value.x);
		result.y = max(result.y, value.y);
	}

	return vec4(result, 0, 0);
}

:: Common ::

///////////////////////////////////////////////////////////////////////////////
/// Occlusion Query Shader ////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////

void occlusionVertexShader(vec4 inputPosition, out vec4 position)
{
	position = mul(mul(inputPosition, view), world);
}

vec4 occlusionFragmentShader()
{
	return vec4(1, 1, 1, 1);
}

///////////////////////////////////////////////////////////////////////////////
/// Copy Shader ///////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////

void copyVertexShader(vec2 inputPosition, out vec4 result, out vec2 texel)
{
	result = vec4(inputPosition * 2 - 1, 0, 1);
	texel = vec2(inputPosition.x, 1 - inputPosition.y);
}

vec4 copyFragmentShader(vec4 position, vec2 texel)
{ 
	return texture(heightTexture, texel);
}

///////////////////////////////////////////////////////////////////////////////
/// Normal Shader /////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////

:: Vertex ::

subroutine(Shader)
void normalVertexShader()
{
	gl_Position = position = vec4(vertex.xy * 2 - 1, 0, 1);
	data.texel = vec4(vertex.x, 1 - vertex.y, 0, 0);
}

:: Fragment ::

subroutine(Shader)
vec4 normalFragmentShader()
{ 
	vec2 texel = data.texel.xy;
	float texelSize = 1.0 / terrainSize;
	float center = textureLod(heightTexture, texel, 0).x;
	float right = textureLod(heightTexture, texel + vec2(texelSize, 0), 0).x - center;
	float left = textureLod(heightTexture, texel - vec2(texelSize, 0), 0).x - center;
	float down = textureLod(heightTexture, texel + vec2(0, texelSize), 0).x - center;
	float up = textureLod(heightTexture, texel - vec2(0, texelSize), 0).x - center;
	vec3 normalRight = cross(vec3(0, down, 1), vec3(1, right, 0));
	vec3 normalLeft = cross(vec3(0, up, -1), vec3(-1, left, 0));
	vec3 normal = normalize((normalRight + normalLeft) / 2);
	return toFloat4(encodeNormal(normal));
}

:: Common ::

subroutine void Shader();

:: Vertex ::

subroutine uniform Shader vertexShader;
void main() { vertexShader(); }

 :: Fragment ::

 subroutine uniform Shader fragmentShader;
 void main() { fragmentShader(); }