#version 330 core

layout(location = 0) in vec3 aPosition; // position attribute

layout(location = 1) in vec2 aTexCoord; // texture coordinate attribute

out vec2 texCoord; // output to the fragment shader

uniform mat4 model;
uniform mat4 projection;
uniform mat4 view;

void main(void)
{
    texCoord = aTexCoord; // pass texture coordinates to the fragment shader

    gl_Position = projection * view * model * vec4(aPosition, 1.0);     // set the position of the vertex
}