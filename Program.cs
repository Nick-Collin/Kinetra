using Kinetra.Render;
using Kinetra.Core;
using Kinetra.Physics;
using Kinetra.Defaults;
using Kinetra.Entities;

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

        //window.Scene.Add(container);

        // Atoms
        //var c = new Atom((0.0f,2.0f,0.0f), (0.0f,1.0f,0.0f), 6, 6, 7);
        //OrbitalVisualizer.BuildElectronEntities(c, (1f,0f,0f), window);
        //OrbitalVisualizer.BuildOrbitalsEntities(c, (0f,0f,1f), 0.05f, window);
        //window.Scene.Add(c);

        var h = new Atom((0.0f,-1.0f,0.0f), (1.0f,1.0f,1.0f), 1, 1, 1);
        OrbitalVisualizer.BuildElectronEntities(h, (1f,0f,0f), window);
        OrbitalVisualizer.BuildOrbitalsEntities(h, (0f,0f,1f), 0.05f, window);
        window.Scene.Add(h);

        var h2 = new Atom((0.0f,1.0f,0.0f), (1.0f,1.0f,1.0f), 1, 1, 0);
        OrbitalVisualizer.BuildElectronEntities(h2, (1f,0f,0f), window);
        OrbitalVisualizer.BuildOrbitalsEntities(h2, (0f,0f,1f), 0.05f, window);
        window.Scene.Add(h2);

        var sim = new AtomSimulation();
        sim.container = container;
        window.FrameUpdatePasses.Add(sim);


        window.FrameUpdatePasses.Add(new CameraLogic());
        window.FrameUpdatePasses.Add(new Newframe());
        //window.Scene.passes.Add(new MovementLogic());
        window.AddRenderPass(new RenderLogic());


        window.WindowBorder = WindowBorder.Hidden;
        window.WindowState = WindowState.Fullscreen;
        window.CursorState = CursorState.Grabbed;
        window.Run();
    }
}