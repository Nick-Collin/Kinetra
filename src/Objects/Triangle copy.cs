using System.Reflection.Metadata.Ecma335;
using Kinetra.Render;

namespace Kinetra.Objects
{
    public class Triangle : RenderObject
    {
        // Array linear: 3 vértices * 3 coordenadas (x,y,z)
        private float[] vertices;

        public Triangle(float[] vertices)
        {
            this.vertices = vertices;
        }

        public override void Load()
        {
            vao = GL.GenVertexArray();
            vbo = GL.GenBuffer();

            GL.BindVertexArray(vao);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);

            GL.BufferData(
                BufferTarget.ArrayBuffer,
                vertices.Length * sizeof(float), // tamanho do VBO em bytes
                vertices,
                BufferUsageHint.StaticDraw
            );

            // layout(location = 0) no shader
            GL.VertexAttribPointer(
                0,                              // localização
                3,                              // 3 floats por vértice (x,y,z)
                VertexAttribPointerType.Float,  // tipo de dado
                false,                          // normalização
                3 * sizeof(float),              // stride (tamanho de cada vértice)
                0                               // offset
            );

            GL.EnableVertexAttribArray(0);
        }

        public override void Render()
        {
            GL.BindVertexArray(vao);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
        }

        public override void Dispose()
        {
            // Libera memória da GPU
            GL.DeleteBuffer(vbo);
            GL.DeleteVertexArray(vao);
        }
    }
}