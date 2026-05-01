using Kinetra.Graphics;
using Kinetra.Render;

namespace Kinetra.Core
{
    public class Entity(Mesh mesh, Material material)
    {
        public Mesh mesh = mesh;
        public Material material = material;
        public MeshBuffer buffer = new MeshBuffer(mesh);

        public void Dispose()
        {
            buffer.Dispose();
            material.Dispose();
        }
    }
}