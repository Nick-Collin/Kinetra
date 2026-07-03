using Kinetra.Graphics;

namespace Kinetra.Entities
{
    public class Point : Entity
    {
        
        public Point(Vector3 position, Vector3? color = null, float radius = 10.0f) : base(CreateMesh(), CreateMaterial(color ?? new Vector3(1f, 0f, 0f), radius))
        {
            this.renderMode = PrimitiveType.Points;
            transform.translation = position;
        }

        private static Mesh CreateMesh()
        {
            float[] vertices =
            {
                0f, 0f, 0f
            };

            uint[] indices =
            {
                0
            };

            var mesh = new Mesh(vertices, indices);
            mesh.layout.AddAttribute(0, 3);

            return mesh;
        }

        private static PointMaterial CreateMaterial(Vector3? color, float radius)
        {
            return new PointMaterial(color ?? new Vector3(1f, 0f, 0f), radius);
        }
    }
}