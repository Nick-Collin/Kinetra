using Kinetra.Render;
using Kinetra.Core;
using Kinetra.Graphics;

public class Program
{
    static void Main()
    {
        using (var window = new KinetraWindow())
        {
            float[] vertices = {
                 0.5f,  0.5f, 0.0f,  // top right
                 0.5f, -0.5f, 0.0f,  // bottom right
                -0.5f, -0.5f, 0.0f,  // bottom left
                -0.5f,  0.5f, 0.0f   // top left
            };

            uint[] indices = {  // note that we start from 0!
                0, 1, 3,   // first triangle
                1, 2, 3    // second triangle
            };
            

            var mesh = new Mesh(vertices, indices);
            var material = new Material();
            var entity = new Entity(mesh, material);

            window.scene.Add(entity);

            window.Run();
        }
    }
}