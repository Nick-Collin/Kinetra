using Kinetra.Render;

public class Program
{
    static void Main()
    {
        using (var game = new Game())
        {
            game.Run();
        }
    }
}