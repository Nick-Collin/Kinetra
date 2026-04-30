using Kinetra.Render;
using Kinetra.Objects;

public class Program
{
    static void Main()
    {
        using (var game = new Game())
        {
            float[] vertices =
            {
                0.0f,  0.5f, 0.0f,  // topo
               -0.5f, -0.5f, 0.0f,  // esquerda
                0.5f, -0.5f, 0.0f   // direita
            };

            Triangle triangle = new Triangle(vertices);

            game.scene.Add(triangle);         

            game.Run();
        }
    }
}