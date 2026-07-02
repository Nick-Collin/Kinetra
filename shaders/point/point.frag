#version 330 core

out vec4 FragColor;

uniform vec3 uColor;
uniform float uPointSize;

void main()
{
        vec2 uv = gl_PointCoord * 2.0 - 1.0;
        float dist = dot(uv, uv);

        float alpha = smoothstep(1.0, 0.85, dist);

        FragColor = vec4(uColor, alpha);
}