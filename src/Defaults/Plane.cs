using System.Drawing;
using Kinetra.Graphics;

namespace Kinetra.Core
{
    public class Plane : Entity
    {
        public Plane(Vector2 size, Vector3? color = null) : base(CreateMesh(size), CreateMaterial(color))
        {
        }

        private static Mesh CreateMesh(Vector2 size)
        {
            float hx = size.X * 0.5f;
            float hz = size.Y * 0.5f;

            float[] vertices = {
                // pos            // uv    // normal
                -hx, 0f, -hz,     0f, 0f,  0f, 1f, 0f,
                 hx, 0f, -hz,     1f, 0f,  0f, 1f, 0f,
                 hx, 0f,  hz,     1f, 1f,  0f, 1f, 0f,
                -hx, 0f,  hz,     0f, 1f,  0f, 1f, 0f,
            };

            uint[] indices = {
                0, 1, 2,
                0, 2, 3
            };

            var mesh = new Mesh(vertices, indices);

            mesh.layout.AddAttribute(0, 3); // pos
            mesh.layout.AddAttribute(1, 2); // uv
            mesh.layout.AddAttribute(2, 3); // normal

            return mesh;
        }

        private static SingleColoredMaterial CreateMaterial(Vector3? color)
        {
            return new SingleColoredMaterial(color ?? new Vector3(1.0f, 1.0f, 1.0f));
        }
    }
}