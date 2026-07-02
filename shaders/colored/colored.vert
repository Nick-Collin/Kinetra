#version 330 core

layout(location = 0) in vec3 aPosition; // position attribute

uniform mat4 model;
uniform mat4 projection;
uniform mat4 view;

void main(void)
{
    gl_Position = projection * view * model * vec4(aPosition, 1.0);     // set the position of the vertex
}