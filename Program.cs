using Kinetra.Render;
using Kinetra.Core;
using Kinetra.Physics;
using Kinetra.Defaults;

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
        /*
        var hidrogen1 = new Atom((0f,1.0f,0f), (1f,0f,0f), 50);
        hidrogen1.protons = 1; hidrogen1.neutrons = 1; hidrogen1.eletrons = 1;
        var hidrogen2 = new Atom((0f,-1.0f,0f), (1f,0f,0f), 50);
        hidrogen2.protons = 1; hidrogen2.neutrons = 1; hidrogen2.eletrons = 1;
        var hidrogen3 = new Atom((1f,0.0f,0f), (1f,0f,0f), 50);
        hidrogen3.protons = 1; hidrogen3.neutrons = 1; hidrogen3.eletrons = 1;
        var hidrogen4 = new Atom((-1f,0.0f,0f), (1f,0f,0f), 50);
        hidrogen4.protons = 1; hidrogen4.neutrons = 1; hidrogen4.eletrons = 1;
        
        var eletron = new Atom((0f,0.0f,0f), (0f,0f,1f), 20);
        eletron.eletrons = 1;

        window.Scene.Add(hidrogen1); 
        window.Scene.Add(hidrogen2);
        window.Scene.Add(hidrogen3);
        window.Scene.Add(hidrogen4);
        */

        var rnd = new Random();

        Atom Make(float x, float y, float z)
        {
            var a = new Atom(
                new Vector3(x, y, z),
                new Vector3(1f, 0.5f, 0.2f),
                50
            );

            a.protons = 1;
            a.neutrons = 1;
            a.eletrons = 1;

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