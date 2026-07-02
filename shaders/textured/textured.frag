#version 330 core

out vec4 outputColor;

in vec2 texCoord;

uniform sampler2D texture0;
uniform vec3 uColor;

void main()
{
   vec4 tex = texture(texture0, texCoord);

    if (tex.a < 0.01)
        outputColor = vec4(uColor, 1.0);
    else
        outputColor = tex * vec4(uColor, 1.0);
}