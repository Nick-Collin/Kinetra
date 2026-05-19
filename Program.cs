using Kinetra.Render;
using Kinetra.Core;
using Kinetra.Graphics;

public class Program
{
    static void Main()
    {
        using var window = new KinetraWindow();
        float[] _vertices =
        {
            //Position          Texture coordinates
            0.5f,  0.5f, 0.0f, 1.0f, 1.0f, // top right
            0.5f, -0.5f, 0.0f, 1.0f, 0.0f, // bottom right
            -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, // bottom left
            -0.5f,  0.5f, 0.0f, 0.0f, 1.0f  // top left
        };

        uint[] _indices =
        {
            0, 1, 2,
            0, 2, 3
        };

        var mesh = new Mesh(_vertices, _indices);
        mesh.layout.AddAttribute(0, 3); // position
        mesh.layout.AddAttribute(1, 2); // texture coordinates

        var material = new Material("textured", "textures/d81debc8293134b05d4a940918e22505.jpg");
        var entity = new Entity(mesh, material);

        window.scene.Add(entity);

        window.Run();
    }
}