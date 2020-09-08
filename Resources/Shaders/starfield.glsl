uniform float iTime;
uniform float iNumLayers;
uniform float iFade;
uniform float iFlickerSpeed;
uniform float iFloatDepth;

uniform vec3 iResolution;
uniform vec4 iMouse;

void mainImage(out vec4 fragColor, in vec2 fragCoord);

// Remove when testing on ShaderToy
void main() {
    mainImage(gl_FragColor, gl_FragCoord.xy);
}

mat2 Rot(float a) {
    float s = sin(a), c=cos(a);
    return mat2(c, -s, s, c);
}

float Star(vec2 uv, float flare) {
	float d = length(uv);
    float m = 0.05 / d;
    
    float rays = max(0.0, 1.0 -abs(uv.x * uv.y * 1000.0));
    m += rays * flare;
    uv *= Rot(3.1415 / 4.0);
    rays = max(0.0, 1.0 -abs(uv.x * uv.y * 1000.0));
    m += rays * 0.3 * flare;
    
    m *= smoothstep(1.0, 0.2, d);
    return m;
}

float Hash21(vec2 p) {
    p = fract(p * vec2(123.34, 456.21));
    p += dot(p, p + 45.32);
    return fract(p.x * p.y);
}

vec3 StarLayer(vec2 uv) {
	vec3 col = vec3(0);
	
    vec2 gv = fract(uv) - 0.5;
    vec2 id = floor(uv);
    
    for(int y = -1; y <= 1;y++) {
    	for(int x = -1; x <= 1; x++) {
            vec2 offs = vec2(x, y);
            
    		float n = Hash21(id + offs);
            float size = fract(n * 345.32);
            
    		float star = Star(gv - offs - vec2(n, fract(n * 34.0)) + 0.5, smoothstep(0.95, 1.0, size) * 0.6);
            
            vec3 color = sin(vec3(0.2, 0.3, 0.9) * fract(n * 2345.2) * 123.2) * 0.5 + 0.5;
            color = color * vec3(1, 0.25, 1.0 + size) + vec3(0.2, 0.2, 0.1) * 2.0;
            
            star *= sin(iTime * iFlickerSpeed + n * 6.2831)* 0.5 + 1.0;
            col += star * size * color;
        }
    }
    return col;
}

void mainImage(out vec4 fragColor, in vec2 fragCoord) {
    vec2 uv = (fragCoord - 0.5 * iResolution.xy) / iResolution.y;
	vec2 M = (iMouse.xy - iResolution.xy * 0.5) / iResolution.y;
    vec3 col = vec3(0);
    
    for(float i = 0.0; i < 1.0; i += 1.0 / iNumLayers) {
    	float depth = iFloatDepth;
        
        float scale = 5.0 - i;
        float fade = smoothstep(0.0, iFade, 0.03 * iTime);
        fade = clamp(fade, 0.0, 0.3);

        col += StarLayer(uv * scale + i * 500.0 - M) * depth * fade;
    }
    
    col = pow(col, vec3(0.9));
    
    fragColor = vec4(col, 1.0);
}