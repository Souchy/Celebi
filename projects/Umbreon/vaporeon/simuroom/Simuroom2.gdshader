
shader_type spatial;
varying vec3 world_position;


void vertex() {
	world_position = VERTEX;
}

const float gridSize = 2.0;
void fragment() {
	vec3 pos = world_position;
	pos /= gridSize;
	pos += gridSize * 20.0;
	//to offset a bug that appears when one of the coordinate is close to 0
	//can be tweaked if the bug is visible
	pos.y += 1.0*float(fract(float(int(pos.x*2.0))/2.0));
	pos.z += float(fract(float(int(pos.y*2.0))/2.0));
	float wtf = fract(float(int(pos.z*2.0))/2.0);
	vec3 col = vec3(wtf);
	//ROUGHNESS = col.x/2.0 + 0.2;
	ALBEDO = col * vec3(0.5, 0.5, 1);
}


/*
shader_type spatial;

uniform float brightness : hint_range(0.001, 2.0) = 1.0;
uniform float anchor : hint_range(-1.0,0.0) = -0.5;
//The point where all the lines come from.
uniform float speed_scale = 0.5;
uniform float fov : hint_range(0.001, 1.0) = 0.01;
uniform float line_count = 20.0;
uniform vec4 background_color: source_color = vec4(0.0, 0.1, 0.2, 1.0);
uniform vec4 grid_color: source_color = vec4(1.0, 0.5, 1.0, 1.0);

float grid(vec2 uv, float batt) {
    vec2 size = vec2(uv.y, uv.y * uv.y * 0.2) * 0.01* (batt + 0.05);
    uv += vec2(0.0, TIME * speed_scale);
    uv = abs(fract(uv) - 0.5);
 	vec2 lines = smoothstep(size, vec2(0.0), uv);
 	lines += smoothstep(size * 5.0, vec2(0.0), uv) * 0.4 * batt;
    return lines.x + lines.y;
}
void fragment() {
	vec2 uv = UV;
	vec4 col = background_color;
    uv.y = 3.0 / (abs(uv.y + fov) + 0.05);
	uv.x += anchor;
    uv.x *= uv.y * line_count;
    float gridVal = grid(uv, brightness);
    col = mix(background_color, grid_color, gridVal);
	ALBEDO = col.rgb;
}
*/

