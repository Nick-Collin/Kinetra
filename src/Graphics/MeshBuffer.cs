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
                mesh.vertexBuffer.Length * sizeof(float), 
                mesh.vertexBuffer,                    // dados dos vértices
                mode                                  // uso pretendido
            );

            GL.BufferData(
                BufferTarget.ElementArrayBuffer,      
                mesh.indices.Length * sizeof(uint),   // tamanho do EBO em bytes
                mesh.indices,                         // dados dos índices
                mode                                  // uso pretendido
            );

            foreach (var attr in mesh.layout.Attributes)
            {
                GL.VertexAttribPointer(
                    attr.Location,
                    attr.Size,
                     VertexAttribPointerType.Float, // Assumindo float para simplicidade, pode ser ajustado para outros tipos
                    attr.Normalized,
                    mesh.layout.Stride,
                    attr.Offset
                );

                GL.EnableVertexAttribArray(attr.Location);
            }
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