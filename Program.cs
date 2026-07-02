using Kinetra.Render;
using Kinetra.Core;
using Kinetra.Graphics;

public class RenderLogic : IRenderPass
{
    public void Execute(Scene scene)
    {

        Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(90.0f), 800f / 600f, 0.1f, 100.0f);
        GL.Enable(EnableCap.DepthTest);
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        foreach (var entity in scene.Entities)
        {
            var model = entity.transform.GetModelMatrix();
            entity.material?.shader?.SetMatrix4("model", model);

            entity.material?.Use();
            entity.buffer.Bind();

            if (scene.camera == null)
                entity.material?.shader?.SetMatrix4("view", Matrix4.Identity);
            else
                entity.material?.shader?.SetMatrix4("view", scene.camera.GetViewMatrix());

            entity.material?.shader?.SetMatrix4("projection", projection);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 36);
        }
    }
}

public class Newframe : FrameUpdateLogic
{
    public void Execute(Scene scene, float dt, KeyboardState keyboard, MouseState mouse, Vector2 windowSize, bool isFocused)
    {
        scene.Update(dt);
    }
}

public class CameraLogic : FrameUpdateLogic
{
    private float speed = 0.0005f;
    public void Execute(Scene scene, float dt, KeyboardState keyboard, MouseState mouse, Vector2 windowSize, bool isFocused)
    {
        if (!isFocused) // check to see if the window is focused
        {
            return;
        }

        KeyboardState input = keyboard;

        if (input.IsKeyDown(Keys.W))
        {
            scene.camera.position += scene.camera.Forward * speed; //Forward 
        }

        if (input.IsKeyDown(Keys.S))
        {
            scene.camera.position -= scene.camera.Forward * speed; //Backwards
        }

        if (input.IsKeyDown(Keys.A))
        {
            scene.camera.position -= Vector3.Normalize(Vector3.Cross(scene.camera.Forward, scene.camera.Up)) * speed; //Left
        }

        if (input.IsKeyDown(Keys.D))
        {
            scene.camera.position += Vector3.Normalize(Vector3.Cross(scene.camera.Forward, scene.camera.Up)) * speed; //Right
        }

        if (input.IsKeyDown(Keys.Space))
        {
            scene.camera.position += scene.camera.Up * speed; //Up 
        }

        if (input.IsKeyDown(Keys.LeftShift))
        {
            scene.camera.position -= scene.camera.Up * speed; //Down
        }
    }
}

public class MovementLogic : SceneUpdateLogic
{
    public void Execute(Entity entity, float dt)
    {
        var m = entity.Movement;
        if (m == null)
            return;

        m.velocity += m.acceleration * dt;
        entity.transform.translation += m.velocity * dt;

        m.angularVelocity += m.angularAcceleration * dt;
        entity.transform.rotation += m.angularVelocity * dt;
    }
}

public class Program
{
    static void Main()
    {
        using var window = new KinetraWindow();
        float[] _vertices = {
            -0.5f, -0.5f, -0.5f,  0.0f, 0.0f,
            0.5f, -0.5f, -0.5f,  1.0f, 0.0f,
            0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
            0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
            -0.5f,  0.5f, -0.5f,  0.0f, 1.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, 0.0f,

            -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
            0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
            0.5f,  0.5f,  0.5f,  1.0f, 1.0f,
            0.5f,  0.5f,  0.5f,  1.0f, 1.0f,
            -0.5f,  0.5f,  0.5f,  0.0f, 1.0f,
            -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,

            -0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
            -0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
            -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
            -0.5f,  0.5f,  0.5f,  1.0f, 0.0f,

            0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
            0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
            0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
            0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
            0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
            0.5f,  0.5f,  0.5f,  1.0f, 0.0f,

            -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
            0.5f, -0.5f, -0.5f,  1.0f, 1.0f,
            0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
            0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
            -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,

            -0.5f,  0.5f, -0.5f,  0.0f, 1.0f,
            0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
            0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
            0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
            -0.5f,  0.5f,  0.5f,  0.0f, 0.0f,
            -0.5f,  0.5f, -0.5f,  0.0f, 1.0f
        };

        uint[] _indices =
        {
            0, 1, 2,
            0, 2, 3
            
        };

        var mesh = new Mesh(_vertices, _indices);
        mesh.layout.AddAttribute(0, 3); // position
        mesh.layout.AddAttribute(1, 2); // texture coordinates

        var material = new Material("projected", "textures/d81debc8293134b05d4a940918e22505.jpg");
        var entity = new Entity(mesh, material);

        Camera camera = new Camera();
        window.Scene.camera = camera;
        camera.position = new Vector3(0.0f, 0.0f, 3.0f);

        entity.Movement = new MovementComponent();
        entity.Movement.angularVelocity = new Vector3(20.0f, 20.0f, 20.0f);
        //entity.Movement.velocity = new Vector3(0.0f, 0.0f, -1.0f);

        window.Scene.Add(entity);
        
        window.FrameUpdatePasses.Add(new CameraLogic());
        window.FrameUpdatePasses.Add(new Newframe());

        window.Scene.passes.Add(new MovementLogic());

        window.AddRenderPass(new RenderLogic());

        window.Run();
    }
}