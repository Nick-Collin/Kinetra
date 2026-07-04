#version 330 core

out vec4 outputColor;

uniform vec3 uColor;
uniform float alpha;

void main()
{
        outputColor = vec4(uColor, alpha);
}