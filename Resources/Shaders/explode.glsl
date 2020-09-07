uniform float iTime;
uniform float iSeed;
uniform float iParticles;
uniform float iPixelResolution;
uniform float iSpread;
uniform float iTravelSpeed;
uniform float iFadeIntensity;

uniform vec2 iDirection;
uniform vec3 iResolution;
// RED: 5.0, 0.5, 0.1, 1.0
// CYAN: 0.1, 0.4, 0.5, 1.0
uniform vec4 iColor;
// 0.32
float seed = iSeed;
// 32.
float particles = iParticles;
// 32.
float res = iPixelResolution;
// 0.4
float spread = iSpread;
// 2.0
float travelSpeed = iTravelSpeed;
// 1.0
float fadeIntensity = iFadeIntensity;

vec2 direction = iDirection;
vec4 color = iColor;

void mainImage(out vec4 fragColor, in vec2 fragCoord);

void main() {
	mainImage(gl_FragColor, gl_FragCoord.xy);
}

void mainImage(out vec4 fragColor, in vec2 fragCoord) {
	vec2 uv = (-iResolution.xy + 2.0 * fragCoord.xy) / iResolution.y;
   	float clr = 0.0;  
	float timecycle = iTime - floor(iTime);  
	seed = (seed + floor(iTime));
	
	float invres = 1.0 / res;
	float invparticles = spread / particles;
	
	for(float i = 0.0; i < particles; i += 0.1) {
		seed += i + tan(seed);
		vec2 tPos = (vec2(cos(seed), sin(seed))) * i * invparticles;
		
		vec2 pPos = vec2(0.0, 0.0);
		pPos.x = -direction.x * (timecycle * timecycle) + tPos.x * timecycle * travelSpeed + pPos.x;
		pPos.y = -direction.y * (timecycle * timecycle) + tPos.y * timecycle * travelSpeed + pPos.y;
		
		pPos = floor(pPos * res) * invres;
		vec2 p1 = pPos;
		vec4 r1 = vec4(vec2(step(p1, uv)), 1.0 - vec2(step(p1 + invres,uv)));
		float px1 = r1.x * r1.y * r1.z * r1.w;
		
		clr += px1 * (sin(iTime + i) + 1.0);
	}
	
	fragColor = vec4(clr * (fadeIntensity - timecycle)) * color;
}