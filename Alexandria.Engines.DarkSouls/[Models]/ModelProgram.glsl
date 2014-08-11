:: #version 140 ::
::Common::
// This is added to the beginning of all shaders.

::Uniforms::
uniform mat4 World, View, Projection;

uniform vec3 AmbientLight = vec3(0, 0, 0);
uniform vec4 DiffuseColor = vec4(1, 1, 1, 1);

#define BoneMaximum 64
uniform mat4 Bones[BoneMaximum];

uniform int DisplayMode;
#define DisplayMode_Normal 0
#define DisplayMode_ViewNormals 1
#define DisplayMode_WorldNormals 4
#define DisplayMode_Unlit 2
#define DisplayMode_Lighting 3
#define DisplayMode_BoneWeights 5

::Vertex::

in vec3 Position, Normal;
in vec2 Texel;

in vec4 BoneIndices, BoneWeights;

out vec3 ViewNormal, WorldNormal;
out vec2 OutTexel;
out vec4 OutBoneIndices, OutBoneWeights;

void main() {
	gl_Position = vec4(Position, 1.0) * World * View * Projection;
	WorldNormal = (vec4(Normal, 0.0) * World).xyz;
	ViewNormal = (vec4(Normal, 0.0) * World * View).xyz;

	// Simple transfers.
	OutBoneIndices = BoneIndices;
	OutBoneWeights = BoneWeights;
	OutTexel = Texel;
}

::Fragment::

uniform sampler2D DiffuseMap;

in vec3 ViewNormal, WorldNormal;
in vec2 OutTexel;
in vec4 OutBoneIndices, OutBoneWeights;

const vec3 GeneralColors[] = vec3[](vec3(0.67, 0, 0), vec3(0, 0.67, 0), vec3(0, 0, 0.67), vec3(0, 0.67, 0.67), vec3(0.67, 0.67, 0), vec3(0.67, 0, 0.67), vec3(0.67, 0.67, 1), vec3(0.67, 1, 0.67), vec3(1, 0.67, 0.67), vec3(1, 1, 0.67), vec3(1, 0.67, 1), vec3(0.67, 1, 1));

vec3 GetColor(int index) { return GeneralColors[index % GeneralColors.length()]; }

void main() {
	vec2 texel = OutTexel / 1024.0;
	vec3 viewNormal = normalize(ViewNormal);
	vec3 worldNormal = normalize(WorldNormal);
	vec3 lightDirection = normalize(vec3(1, 1, 1));
	vec3 light = AmbientLight;
	vec4 color = DiffuseColor;
	vec4 boneWeights = OutBoneWeights, boneIndices = OutBoneIndices;

	color *= texture(DiffuseMap, texel);
	light += dot(worldNormal, lightDirection);

	if(color.w < 0.9)
		discard;

	switch(DisplayMode) {
		default:
		case DisplayMode_Normal:
			gl_FragColor = vec4(light * color.xyz, color.w);
			if(DiffuseColor.x != 0)
				gl_FragColor = texture(DiffuseMap, abs(viewNormal).xy);
			break;

		case DisplayMode_ViewNormals:
			gl_FragColor = vec4(abs(viewNormal), 1);
			break;

		case DisplayMode_WorldNormals:
			gl_FragColor = vec4(abs(worldNormal), 1);
			break;

		case DisplayMode_Unlit:
			gl_FragColor = color;
			break;

		case DisplayMode_Lighting:
			gl_FragColor = vec4(light, color.w);
			break;

		case DisplayMode_BoneWeights:
			gl_FragColor = vec4(
				GetColor(int(boneIndices.x)) * boneWeights.x +
				GetColor(int(boneIndices.y)) * boneWeights.y +
				GetColor(int(boneIndices.z)) * boneWeights.z +
				GetColor(int(boneIndices.w)) * boneWeights.w, color.w);
			break;
	}
}
