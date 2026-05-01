namespace Kinetra.Graphics
{
    public class MeshBuffer
    {
        int vao, vbo, ebo;
        public MeshBuffer(Mesh mesh, BufferUsageHint mode = BufferUsageHint.StaticDraw, bool normalized = false, int vertexcount = 3, int offset = 0)
        {
            vao = GL.GenVertexArray();
            vbo = GL.GenBuffer();
            ebo = GL.GenBuffer();

            GL.BindVertexArray(vao);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);

            GL.BufferData(
                BufferTarget.ArrayBuffer,             
                mesh.vertices.Length * sizeof(float), // tamanho do VBO em bytes
                mesh.vertices,                        // dados dos vértices
                mode                                  // uso pretendido
            );

            GL.BufferData(
                BufferTarget.ElementArrayBuffer,      
                mesh.indices.Length * sizeof(uint),   // tamanho do EBO em bytes
                mesh.indices,                         // dados dos índices
                mode                                  // uso pretendido
            );

            // layout(location = 0) no shader
            GL.VertexAttribPointer(
                0,                               // localização
                vertexcount,                     // número de componentes por vértice
                VertexAttribPointerType.Float,   // tipo de dado
                normalized,                      // normalização
                vertexcount * sizeof(float),     // stride (tamanho de cada vértice)
                offset                           // offset
            );

            GL.EnableVertexAttribArray(0);
        }

        public void Bind()
        {
            GL.BindVertexArray(vao);
        }

        public void Dispose()
        {
            GL.DeleteBuffer(vbo);
            GL.DeleteBuffer(ebo);
            GL.DeleteVertexArray(vao);
        }
    }
}