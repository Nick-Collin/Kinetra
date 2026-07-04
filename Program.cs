using Kinetra.Render;
using Kinetra.Core;
using Kinetra.Physics;
using Kinetra.Defaults;
using System.Security.Cryptography;

public class Program
{
    static void Main()
    {
        using var window = new KinetraWindow();

        Camera camera = new Camera();
        window.Scene.camera = camera;
        camera.position = new Vector3(0.0f, 0.0f, 2.0f);

        var container = new BoxContainer(
            new Vector3(-5, -5, -5),
            new Vector3(5, 5, 5)
        );

        window.Scene.Add(container);
        
        var h1 = new Atom((1.0f,0.0f,0.0f), (1.0f,0.0f,0.0f), 1, 1, 1); 
        var h2 = new Atom((-1.0f,0.0f,0.0f), (1.0f,0.0f,0.0f), 1, 0, 1); 
        var h3 = new Atom((0.0f,1.0f,0.0f), (1.0f,0.0f,0.0f), 1, 1, 1); 
        var h4 = new Atom((0.0f,-1.0f,0.0f), (1.0f,0.0f,0.0f), 1, 0, 1); 

        window.Scene.Add(h1); 
        window.Scene.Add(h2);
        window.Scene.Add(h3);
        window.Scene.Add(h4);
        

        /*
        var rnd = new Random();

        Atom Make(float x, float y, float z)
        {
            var a = new Atom(
                new Vector3(x, y, z),
                new Vector3(1f, 0.5f, 0.2f),
                1,
                1,
                1
            );

            a.velocity = new Vector3(
                Random.Shared.NextSingle() - 0.5f,
                Random.Shared.NextSingle() - 0.5f,
                Random.Shared.NextSingle() - 0.5f
            ) * 2f;

            return a;
        }

        for (int i = 0; i < 20; i++)
        {
            float x = (float)(rnd.NextDouble() * 4 - 2);
            float y = (float)(rnd.NextDouble() * 4 - 2);
            float z = (float)(rnd.NextDouble() * 4 - 2);

            window.Scene.Add(Make(x, y, z));
        }
        */

        var sim = new AtomSimulation();
        sim.container = container;
        window.FrameUpdatePasses.Add(sim);


        window.FrameUpdatePasses.Add(new CameraLogic());
        window.FrameUpdatePasses.Add(new Newframe());
        window.Scene.passes.Add(new MovementLogic());
        window.AddRenderPass(new RenderLogic());


        window.WindowBorder = WindowBorder.Hidden;
        window.WindowState = WindowState.Fullscreen;
        window.CursorState = CursorState.Grabbed;
        window.Run();
    }
}