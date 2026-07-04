using Kinetra.Graphics;
using Kinetra.Core;

namespace Kinetra.Entities
{
    public class MovementComponent
    {
        public Vector3 velocity = new Vector3(0.0f, 0.0f, 0.0f);
        public Vector3 acceleration = new Vector3(0.0f, 0.0f, 0.0f);
        public Vector3 angularVelocity = new Vector3(0.0f, 0.0f, 0.0f);
        public Vector3 angularAcceleration = new Vector3(0.0f, 0.0f, 0.0f);
    }

    public class Entity(Mesh mesh, Material material)
    {
        public Mesh mesh = mesh;
        public Material material = material;
        public MeshBuffer buffer = new MeshBuffer(mesh);
        public Transform transform = new();
        public MovementComponent? Movement;
        public PrimitiveType renderMode = PrimitiveType.Triangles;

        public void Dispose()
        {
            buffer.Dispose();
        }
    }
}