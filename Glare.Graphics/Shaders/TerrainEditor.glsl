:: #include "Common.h.glsl" ::

Texture2D HeightTexture;

float TerrainSize, InverseTerrainSize;

#define TexelX vec2(InverseTerrainSize, 0)
#define TexelY vec2(0, InverseTerrainSize)

#define MakeSamplerState(TEXTURE, ADDRESSU, ADDRESSV, MAGFILTER, MINFILTER, MIPFILTER) sampler_state { texture = <TEXTURE>; AddressU = ADDRESSU; AddressV = ADDRESSV; MagFilter = MAGFILTER; MinFilter = MINFILTER; MipFilter = MIPFILTER; }
#define PointSamplerState(TEXTURE) MakeSamplerState(TEXTURE, Wrap, Wrap, Point, Point, None)

sampler heightTexture = PointSamplerState(HeightTexture);

float Height(vec2 texel) { return textureLod(heightTexture, vec4(texel, 0, 0)); }

void VertexShaderFunction(vec2 input : POSITION0, out vec4 output : POSITION0, out vec4 texel : TEXCOORD0)
{
	output = vec4(input * 2 - 1, 0, 1);
	texel = vec4(input.x, 1 - input.y, 0, 0);
}

Texture2D TemporaryTexture;

vec2 TemporarySize, TemporaryInverseSize;
sampler TemporarySampler = PointSamplerState(TemporaryTexture);

vec4 IndexTemporary(int index) { return textureLod(TemporarySampler, vec4(vec2(index % TemporarySize.x, floor(index / TemporarySize.x)) * TemporaryInverseSize, 0, 0)); }

///////////////////////////////////////////////////////////////////////////////
/// Perlin Noise //////////////////////////////////////////////////////////////

Texture2D PerlinPermutationTexture, PerlinGradientTexture;
mat4 PerlinTransform;

sampler PerlinPermutationSampler = MakeSamplerState(PerlinPermutationTexture, Wrap, Clamp, Point, Point, None);
sampler PerlinGradientSampler = MakeSamplerState(PerlinGradientTexture, Wrap, Clamp, Point, Point, None);
#define PerlinSize 256

vec3 PerlinFade(vec3 t) { return t * t * t * (t * (t * 6 - 15) + 10); }
	//return t * t * (3 - 2 * t); // old curve

vec4 PerlinPermutation(vec2 xy) { return texture(PerlinPermutationSampler, xy); }

float PerlinGradientPermutation(float x, vec3 p) { return dot(tex1D(PerlinGradientSampler, x), p); }

float PerlinNoise(vec3 p)
{
	//p = mul(vec4(p, 1), PerlinTransform).xyz;
	p = mul(p, PerlinTransform);

	vec3 P = fmod(floor(p), PerlinSize) / PerlinSize;
	p -= floor(p);
	vec3 f = PerlinFade(p);
	const float one = 1.0 / PerlinSize;

	vec4 h = PerlinPermutation(P.xy) + P.z;

	return mix3(
		PerlinGradientPermutation(h.x, p),
		PerlinGradientPermutation(h.z, p - vec3(1, 0, 0)),
		PerlinGradientPermutation(h.y, p - vec3(0, 1, 0)),
		PerlinGradientPermutation(h.w, p - vec3(1, 1, 0)),

		PerlinGradientPermutation(h.x + one, p - vec3(0, 0, 1)),
		PerlinGradientPermutation(h.z + one, p - vec3(1, 0, 1)),
		PerlinGradientPermutation(h.y + one, p - vec3(0, 1, 1)),
		PerlinGradientPermutation(h.w + one, p - vec3(1, 1, 1)), f);
}

float FractalBrownianMotion(vec3 p, int octaves, float lacunarity = 2.0, float gain = 0.5) {
	float frequency = 1.0, amplitude = 0.5;
	float sum = 0;	
	for(int octave = 0; octave < octaves; octave++) {
		sum += PerlinNoise(p * frequency) * amplitude;
		frequency *= lacunarity;
		amplitude *= gain;
	}
	return sum;
}

float Turbulence(vec3 p, int octaves, float lacunarity = 2.0, float gain = 0.5) {
	float sum = 0;
	float frequency = 1.0, amplitude = 1.0;
	for(int octave = 0; octave < octaves; octave++)
	{
		sum += abs(PerlinNoise(p * frequency)) * amplitude;
		frequency *= lacunarity;
		amplitude *= gain;
	}
	return sum;
}

float Ridge(float h, float offset) { return pow(offset - abs(h), 2); }

float RidgedMultifractal(vec3 p, int octaves, float lacunarity = 2.0, float gain = 0.5, float offset = 1.0) {
	float sum = 0;
	float frequency = 1.0, amplitude = 0.5;
	float previous = 1.0;
	for(int octave = 0; octave < octaves; octave++) {
		float n = Ridge(PerlinNoise(p * frequency), offset);
		sum += n * amplitude * previous;
		previous = n;
		frequency *= lacunarity;
		amplitude *= gain;
	}
	return sum;
}

///////////////////////////////////////////////////////////////////////////////
/// Clear /////////////////////////////////////////////////////////////////////

vec4 ClearValue;

vec4 ClearFunction(vec2 texel : TEXCOORD0) : COLOR0
{
	return ClearValue;
}

technique Clear
{
	pass
	{
		VertexShader = compile vs_3_0 VertexShaderFunction();
		PixelShader = compile ps_3_0 ClearFunction();
	}
}

///////////////////////////////////////////////////////////////////////////////
/// Fault /////////////////////////////////////////////////////////////////////

int FaultCount = 0;
float FaultPush = 0;

vec4 FaultFunction(vec2 texel : TEXCOORD0) : COLOR0
{
	vec4 value = texture(heightTexture, texel);
	vec4 fault;
	vec2 delta;
	float sum = 0;

	for(int index = 0; index < FaultCount; index++)
	{
		fault = IndexTemporary(index);
		delta = (fault.zw - fault.xy) * (texel - fault.yx);
		sum += step(delta.x, delta.y);
	}

	value += (sum * 2 - FaultCount) * FaultPush;
	return value;
}

technique Fault
{
	pass
	{
		VertexShader = compile vs_3_0 VertexShaderFunction();
		PixelShader = compile ps_3_0 FaultFunction();
	}
}

///////////////////////////////////////////////////////////////////////////////
/// Fractal Brownian Motion ///////////////////////////////////////////////////

float FractalBrownianMotionLacunarity = 2;
float FractalBrownianMotionGain = 0.5;
int FractalBrownianMotionOctaves = 4;
float FractalBrownianMotionOffset = 0;
float FractalBrownianMotionScale = 4;
float FractalBrownianMotionStrength = 1;

vec4 FractalBrownianMotionFunction(vec2 texel : TEXCOORD0) : COLOR0
{
	return texture(heightTexture, texel) +
		FractalBrownianMotion(vec3(texel * FractalBrownianMotionScale, FractalBrownianMotionOffset),
			FractalBrownianMotionOctaves, FractalBrownianMotionLacunarity,
			FractalBrownianMotionGain) * FractalBrownianMotionStrength;
}

technique FractalBrownianMotionTechnique
{
	pass
	{
		VertexShader = compile vs_3_0 VertexShaderFunction();
		PixelShader = compile ps_3_0 FractalBrownianMotionFunction();
	}
}

///////////////////////////////////////////////////////////////////////////////
/// Erosion ///////////////////////////////////////////////////////////////////

float ErosionDeltaTime, ErosionTime;
Texture2D ErosionWaterSedimentTexture, ErosionVelocityTexture, ErosionOutflowFluxTexture;
float ErosionGravity = 2;
float ErosionFluxPipeLength = 1;
float ErosionFluxPipeArea = 4;
float ErosionGridDistanceSquared = 1;
float ErosionSedimentCapacity = 0.002;
float ErosionMinimumSedimentCapacity = 0;
float ErosionDissolvingConstant = 0.02;
float ErosionDepositionConstant = 0.02;
float ErosionEvaporationConstant = 0.05;

sampler ErosionWaterSedimentSampler = sampler_state { texture = <ErosionWaterSedimentTexture>; AddressU = Wrap; AddressV = Wrap; MagFilter = Point; MinFilter = Point; MipFilter = None; };
sampler ErosionVelocitySampler = sampler_state { texture = <ErosionVelocityTexture>; AddressU = Wrap; AddressV = Wrap; MagFilter = Point; MinFilter = Point; MipFilter = None; };
sampler ErosionOutflowFluxSampler = sampler_state { texture = <ErosionOutflowFluxTexture>; AddressU = Wrap; AddressV = Wrap; MagFilter = Point; MinFilter = Point; MipFilter = None; };

float ErosionWaterFall(vec2 texel) {
	return max(0, FractalBrownianMotion(vec3(texel * 8 + vec2(ErosionTime * 41, ErosionTime * 73) / 41, ErosionTime + 64), 4, 2, 0.5) - 0.25) * 20;
}

vec2 ErosionWaterSediment(vec2 texel) { return textureLod(ErosionWaterSedimentSampler, vec4(texel, 0, 0)).xy; }
float ErosionSediment(vec2 texel) { return ErosionWaterSediment(texel).y; }
float ErosionWaterHeight(vec2 texel) { return ErosionWaterSediment(texel).x; }
vec2 ErosionVelocity(vec2 texel) { return textureLod(ErosionVelocitySampler, vec4(texel, 0, 0)).xy; }
float ErosionWaterHeightSum(vec2 texel) { return textureLod(heightTexture, vec4(texel, 0, 0)).x + ErosionWaterHeight(texel); }
vec4 ErosionOutflowFlux(vec2 texel) { return textureLod(ErosionOutflowFluxSampler, vec4(texel, 0, 0)); }

void ErosionWaterFallFunction(vec2 texel : TEXCOORD0, out vec4 outputWaterSediment : COLOR0) {
	vec2 waterSediment = ErosionWaterSediment(texel);

		waterSediment.x += ErosionDeltaTime * ErosionWaterFall(texel);

	outputWaterSediment = vec4(waterSediment, 0, 0);
}

technique ErosionWaterFallTechnique {
	pass {
		VertexShader = compile vs_3_0 VertexShaderFunction();
		PixelShader = compile ps_3_0 ErosionWaterFallFunction();
	}
}

void ErosionOutflowFluxFunction(vec2 texel : TEXCOORD0, out vec4 outputFlux : COLOR0) {
	vec4 flux = ErosionOutflowFlux(texel);
		float heightCenter = ErosionWaterHeightSum(texel);
	vec4 heightDelta;

	heightDelta = heightCenter - vec4(
		ErosionWaterHeightSum(texel - TexelX),
		ErosionWaterHeightSum(texel + TexelX),
		ErosionWaterHeightSum(texel - TexelY),
		ErosionWaterHeightSum(texel + TexelY));

	flux = max(0, flux + ErosionDeltaTime * ErosionFluxPipeArea * (ErosionGravity * heightDelta) / ErosionFluxPipeLength);
	float scale = min(1, ErosionWaterHeight(texel) * ErosionGridDistanceSquared / (axialSum(flux) * ErosionDeltaTime));

	flux *= scale;

	outputFlux = flux;
}

technique ErosionOutflowFluxTechnique {
	pass {
		VertexShader = compile vs_3_0 VertexShaderFunction();
		PixelShader = compile ps_3_0 ErosionOutflowFluxFunction();
	}
}

void ErosionWaterSurfaceVelocityFieldUpdateFunction(vec2 texel : TEXCOORD0, out vec4 outputWaterSediment : COLOR0, out vec4 outputVelocity : COLOR1) {
	vec2 waterSediment = ErosionWaterSediment(texel), velocity = ErosionVelocity(texel);
		vec4 influx = vec4(
		ErosionOutflowFlux(texel + TexelX).x,
		ErosionOutflowFlux(texel - TexelX).y,
		ErosionOutflowFlux(texel + TexelY).z,
		ErosionOutflowFlux(texel - TexelY).w);
	vec4 outflux = ErosionOutflowFlux(texel);
		float deltaVolume = ErosionDeltaTime * axialSum(influx - outflux);

	vec2 movement = vec2(influx.y - outflux.x + outflux.y - influx.x, influx.w - outflux.z + outflux.w - influx.z) / 2;
		float waterHeight = waterSediment.x, newWaterHeight = waterHeight + deltaVolume / ErosionGridDistanceSquared;

	velocity = movement;

	waterSediment.x = newWaterHeight;

	outputWaterSediment = vec4(waterSediment, 0, 0);
	outputVelocity = vec4(velocity, 0, 0);
}

technique ErosionWaterSurfaceVelocityFieldUpdateTechnique {
	pass {
		VertexShader = compile vs_3_0 VertexShaderFunction();
		PixelShader = compile ps_3_0 ErosionWaterSurfaceVelocityFieldUpdateFunction();
	}
}

float ErosionTalusAngle = 1;
float ErosionTalusFall = 1;

void ErosionDepositionFunction(vec2 texel : TEXCOORD0, out vec4 outputWaterSediment : COLOR0, out vec4 outputHeight : COLOR1) {
	vec2 waterSediment = ErosionWaterSediment(texel), velocity = ErosionVelocity(texel);
		float sediment = waterSediment.y;
	float height = Height(texel);
	float heightRight = Height(texel + TexelX) - height, heightLeft = Height(texel - TexelX) - height;
	float heightDown = Height(texel + TexelY) - height, heightUp = Height(texel - TexelY) - height;
	vec4 heightDifference = vec4(heightLeft, heightRight, heightUp, heightDown);
		vec4 angles = atan2(heightDifference, vec4(1, 1, 1, 1));
		float steepness = max(max(angles.x, angles.y), max(angles.z, angles.w));
	float capacity = max(ErosionMinimumSedimentCapacity, ErosionSedimentCapacity * sin(steepness)) * length(velocity);
	float constant = capacity > sediment ? ErosionDissolvingConstant : ErosionDepositionConstant;

	height += axialSum((max(0, angles - ErosionTalusAngle) - max(0, -angles - ErosionTalusAngle)) * ErosionTalusFall * ErosionDeltaTime);

	height += constant * (sediment - capacity);
	sediment -= constant * (sediment - capacity);

	waterSediment.y = sediment;

	outputHeight = height;
	outputWaterSediment = vec4(waterSediment, 0, 0);
}

technique ErosionDepositionTechnique {
	pass {
		VertexShader = compile vs_3_0 VertexShaderFunction();
		PixelShader = compile ps_3_0 ErosionDepositionFunction();
	}
}

float ErosionEvaporation(float waterLevel) {
	return waterLevel * (1 - ErosionEvaporationConstant * ErosionDeltaTime);
}

void ErosionSedimentTransportFunction(vec2 texel : TEXCOORD0, out vec4 outputWaterSediment : COLOR0) {
	vec2 velocity = ErosionVelocity(texel), waterSediment = ErosionWaterSediment(texel);

		waterSediment.y = textureBilinearLod(ErosionWaterSedimentSampler, texel - ErosionDeltaTime * velocity * 0.1, TerrainSize, 0).y;
	waterSediment.x = ErosionEvaporation(waterSediment.x);
	//ErosionSediment(texel - ErosionDeltaTime * velocity);
	outputWaterSediment = vec4(waterSediment, 0, 0);
}

technique ErosionSedimentTransportTechnique {
	pass {
		VertexShader = compile vs_3_0 VertexShaderFunction();
		PixelShader = compile ps_3_0 ErosionSedimentTransportFunction();
	}
}

vec4 ErosionVisualizationFunction(vec2 texel : TEXCOORD0) : COLOR0 {
	float water = ErosionWaterHeight(texel);
	float sediment = ErosionSediment(texel);
	vec2 velocity = ErosionVelocity(texel) / 10;

		return vec4(1 + sediment / ErosionDeltaTime * 80, 1, 1, 1) / 4;
	// + vec4(velocity + 0.5, 0.5, 0) / 2;
	if(water < 0)
		return vec4(1, 0, 0, 1);
	return vec4(0.25 + vec3(0, 0, log(water + 1) / log(2) / 4), 1);
}

technique ErosionVisualizationTechnique {
	pass {
		VertexShader = compile vs_3_0 VertexShaderFunction();
		PixelShader = compile ps_3_0 ErosionVisualizationFunction();
	}
}

///////////////////////////////////////////////////////////////////////////////
/// Color by elevation and tilt ///////////////////////////////////////////////

Texture2D ColorByElevationAndTilt_Texture;
mat4 ColorByElevationAndTilt_Transform;

sampler ColorByElevationAndTilt_Sampler = sampler_state { texture = <ColorByElevationAndTilt_Texture>; AddressU = clamp; AddressV = clamp; MagFilter = linear; MinFilter = linear; MipFilter = None; };

vec4 ColorByElevationAndTiltFunction(vec2 texel : TEXCOORD0) : COLOR0 {
	vec4 value = vec4(
		maxAxis(abs(HeightmapNeighborTilt(heightTexture, texel, InverseTerrainSize))),
		Height(texel), 0, 1);
	value = mul(value, ColorByElevationAndTilt_Transform);
	return textureLod(ColorByElevationAndTilt_Sampler, vec4(value.xy, 0, 0));
}

technique ColorByElevationAndTilt {
	pass {
		VertexShader = compile vs_3_0 VertexShaderFunction();
		PixelShader = compile ps_3_0 ColorByElevationAndTiltFunction();
	}
}
