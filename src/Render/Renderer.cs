using Kinetra.Core;
using Kinetra.Graphics;

namespace Kinetra.Render
{
    public class Renderer
    {
        public void Draw(Scene scene)
        {
            foreach (var entity in scene.Entities)
            {
                if (entity.material.shader == null)
                {
                    Console.WriteLine("Shader null!");
                    return;
                }
                entity.material.shader.Use();
                entity.buffer.Bind();
                GL.DrawElements(
                    PrimitiveType.Triangles,             // tipo de primitiva 
                    entity.mesh.indices.Length,          // número de índices a desenhar
                    DrawElementsType.UnsignedInt,        // tipo dos índices
                    0                                    // offset no EBO
                );
            }
        }
    }
}