shader_type canvas_item;

uniform vec4 color32 : source_color = vec4(1.0);
uniform vec4 color48 : source_color = vec4(1.0);
uniform vec4 color64 : source_color = vec4(1.0);
uniform vec4 color128 : source_color = vec4(1.0);
uniform vec4 color160 : source_color = vec4(1.0);
uniform vec4 color192 : source_color = vec4(1.0);

uniform float threshold = 0.003; // допуск для сравнения оттенков

void fragment() {
    vec4 tex = texture(TEXTURE, UV);
    float g = tex.r; // оттенки серого → r=g=b

    // сравниваем с нормализованными оттенками
    if (abs(g - (32.0 / 255.0)) < threshold) {
        COLOR = vec4(color32.rgb, tex.a);
    }
    else if (abs(g - (48.0 / 255.0)) < threshold) {
        COLOR = vec4(color48.rgb, tex.a);
    }
    else if (abs(g - (64.0 / 255.0)) < threshold) {
        COLOR = vec4(color64.rgb, tex.a);
    }
    else if (abs(g - (128.0 / 255.0)) < threshold) {
        COLOR = vec4(color128.rgb, tex.a);
    }
    else if (abs(g - (160.0 / 255.0)) < threshold) {
        COLOR = vec4(color160.rgb, tex.a);
    }
    else if (abs(g - (192.0 / 255.0)) < threshold) {
        COLOR = vec4(color192.rgb, tex.a);
    }
    else {
        COLOR = tex;
    }
}
