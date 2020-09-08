uniform float iTime;
uniform float iSeed;
uniform float iParticles;
uniform float iPixelResolution;
uniform float iFadeSpeed;
uniform float iSpread;
uniform float iSpreadSpeed;
uniform float iJitter;

uniform vec3 iResolution;
// RED: 5.0, 0.5, 0.1, 1.0
// CYAN: 0.1, 0.4, 0.5, 1.0
// Green: 0, 1.5, 0.1, 0.0
uniform vec4 iColor;
//float seed = 0.32;
//float particles = 4.0;
//float pixelResolution = 32.0;
//float spreadSpeed = 10.0;
//float fadeSpeed = 10.0;
//float jitter = 5.0;
//float spread = 0.2;
float seed = iSeed;
float particles = iParticles;
float pixelResolution = iPixelResolution;
float spread = iSpread;
float spreadSpeed = iSpreadSpeed;
float fadeSpeed = iFadeSpeed;
float jitter = iJitter;

vec4 color = iColor;

void mainImage(out vec4 fragColor, in vec2 fragCoord);

void main() {
	mainImage(gl_FragColor, gl_FragCoord.xy);
}

void mainImage(out vec4 fragColor, in vec2 fragCoord) {
	vec2 uv = (-iResolution.xy + 2.0 * fragCoord.xy) / iResolution.y;
	float clr = 0.0;
	float invres = 1.0 / pixelResolution;
	float invparticles = spread / particles;

	for (float i = 0.0; i < particles; i += 0.1 ) {
		seed += i + tan(seed);
		vec2 tPos = (vec2(cos(seed),sin(seed))) * i * invparticles;

		vec2 pPos = vec2(0.0, 0.0);
		pPos.x = tPos.x * iTime * spreadSpeed;
		pPos.y = tPos.y * iTime * spreadSpeed;

		pPos = floor(pPos * pixelResolution) * invres; 

		vec2 p1 = pPos;
		vec4 r1 = vec4(vec2(step(p1, uv)), 1.0 - vec2(step(p1 + invres, uv)));
		float px1 = r1.x * r1.y * r1.z * r1.w;
		float px2 = smoothstep(0.0, 500.0, (1.0 / distance(uv, pPos + .015)));
		px1=max(px1,px2);

		clr += px1 * (sin(iTime * jitter + i) + 1.0);
	}

	fragColor = vec4(clr * (1.0 - iTime * fadeSpeed)) * color;
}