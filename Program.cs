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
            // positions        // colors
            0.5f, -0.5f, 0.0f,  1.0f, 0.0f, 0.0f,   // bottom right
            -0.5f, -0.5f, 0.0f,  0.0f, 1.0f, 0.0f,   // bottom left
            0.0f,  0.5f, 0.0f,  0.0f, 0.0f, 1.0f    // top 
        };

        uint[] _indices =
        {
            0, 1, 2
        };

        var mesh = new Mesh(_vertices, _indices);
        mesh.layout.AddAttribute(0, 3); // position
        mesh.layout.AddAttribute(1, 3); // color  

        var material = new Material("colored");
        var entity = new Entity(mesh, material);

        window.scene.Add(entity);

        window.Run();
    }
}