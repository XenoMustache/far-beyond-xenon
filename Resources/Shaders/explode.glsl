uniform float iTime;
uniform vec3 iResolution;
uniform vec4 iMouse;

float seed = 0.32;
const float particles = 32.0;
float res = 32.0;
float gravity = 0.0;

void mainImage(out vec4 fragColor, in vec2 fragCoord);

void main() {
	mainImage(gl_FragColor, gl_FragCoord.xy);
}

void mainImage(out vec4 fragColor, in vec2 fragCoord) {
	vec2 uv = (-iResolution.xy + 2.0 * fragCoord.xy) / iResolution.y;

   	float clr = 0.0;  
    float timecycle = iTime - floor(iTime);  

    seed = (seed+floor(iTime));

    float invres = 1.0 / res;
    float invparticles = 0.5 / particles;
    
    for(float i = 0.0; i < particles; i += 0.1) {
		seed+=i+tan(seed);
        vec2 tPos = (vec2(cos(seed), sin(seed))) * i * invparticles;
        
        vec2 pPos = vec2(0.0, 0.0);
        pPos.x=((tPos.x) * timecycle * 2.);
		pPos.y = -gravity * (timecycle*timecycle)+tPos.y * timecycle * 2.0 + pPos.y;
        
        pPos = floor(pPos * res) * invres;

    	vec2 p1 = pPos;
    	vec4 r1 = vec4(vec2(step(p1, uv)),1.0 - vec2(step(p1 + invres, uv)));
    	float px1 = r1.x * r1.y * r1.z * r1.w;
        
	    clr += px1 * (sin(iTime + i) + 1.0);
    }
    
	fragColor = vec4(clr * (1.0 - timecycle)) * vec4(5.0, 0.5, 0.1, 1.0);
}