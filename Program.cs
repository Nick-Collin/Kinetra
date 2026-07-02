using Kinetra.Render;
using Kinetra.Core;
using Kinetra.Graphics;
using Kinetra.Defaults;

public class InputMovementSystem : FrameUpdateLogic
{
    public void Execute(Scene scene, float dt, KeyboardState keyboard, MouseState mouse, Vector2 windowSize, bool isFocused)
    {
        foreach (var entity in scene.Entities)
        {
            if (entity is Point p){
                if (keyboard.IsKeyDown(Keys.Up))
                    p.transform.translation += new Vector3(0f, 0.1f, 0f) * dt;

                if (keyboard.IsKeyDown(Keys.Down))
                    p.transform.translation += new Vector3(0f, -0.1f, 0f) * dt;

                //Console.Out.WriteLine($"Point position: {p.transform.translation}");
            }
            if (entity is Plane pl){
                //Console.Out.WriteLine($"Plane position: {pl.transform.translation}");
            }
        }
        //Console.Out.WriteLine($"Camera position: {scene.camera.position}");
    }
}

public class Program
{
    static void Main()
    {
        using var window = new KinetraWindow();

        var plane = new Plane(new Vector2(10.0f, 10.0f), new Vector3(0.5f, 0.5f, 0.5f));
        var point = new Point(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 0.0f, 0.0f));
        point.Movement = new MovementComponent();

        Camera camera = new Camera();
        window.Scene.camera = camera;
        camera.position = new Vector3(0.0f, 1.0f, 3.0f);

        window.Scene.Add(plane);
        window.Scene.Add(point);
        
        window.FrameUpdatePasses.Add(new CameraLogic());
        window.FrameUpdatePasses.Add(new InputMovementSystem());
        window.FrameUpdatePasses.Add(new Newframe());


        window.Scene.passes.Add(new MovementLogic());
        
        window.AddRenderPass(new RenderLogic());


        window.WindowBorder = WindowBorder.Hidden;
        window.WindowState = WindowState.Fullscreen;
        window.CursorState = CursorState.Grabbed;
        window.Run();
    }
}