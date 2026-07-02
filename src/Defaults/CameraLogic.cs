using Kinetra.Core;
using Kinetra.Render;

namespace Kinetra.Defaults
{
public class CameraLogic : FrameUpdateLogic
{
    private float move_speed = 1f;
    private Vector2 lastMouse;
    private bool firstMove = true;
    private float sensitivity = 0.005f;
    private float pitch_clamp = MathHelper.DegreesToRadians(89.0f);
    public void Execute(Scene scene, float dt, KeyboardState keyboard, MouseState mouse, Vector2 windowSize, bool isFocused)
    {
        if (!isFocused) // check to see if the window is focused
        {
            return;
        }

        KeyboardState input = keyboard;

        if (input.IsKeyDown(Keys.W))
        {
            scene.camera.position += scene.camera.Forward * move_speed * dt; //Forward 
        }

        if (input.IsKeyDown(Keys.S))
        {
            scene.camera.position -= scene.camera.Forward * move_speed * dt; //Backwards
        }

        if (input.IsKeyDown(Keys.A))
        {
            scene.camera.position -= Vector3.Normalize(Vector3.Cross(scene.camera.Forward, scene.camera.Up)) * move_speed * dt; //Left
        }

        if (input.IsKeyDown(Keys.D))
        {
            scene.camera.position += Vector3.Normalize(Vector3.Cross(scene.camera.Forward, scene.camera.Up)) * move_speed * dt; //Right
        }

        if (input.IsKeyDown(Keys.Space))
        {
            scene.camera.position += scene.camera.Up * move_speed * dt; //Up 
        }

        if (input.IsKeyDown(Keys.LeftShift))
        {
            scene.camera.position -= scene.camera.Up * move_speed * dt; //Down
        }

        if (firstMove)
        {
            lastMouse = mouse.Position;
            firstMove = false;
        }

        var delta = mouse.Position - lastMouse;
        lastMouse = mouse.Position;

        float yawDelta = -delta.X * sensitivity;
        float pitchDelta = -delta.Y * sensitivity;

        scene.camera.yaw += yawDelta;
        scene.camera.pitch = MathHelper.Clamp(scene.camera.pitch + pitchDelta, -pitch_clamp, pitch_clamp);

    }
}
public class Newframe : FrameUpdateLogic
{
    public void Execute(Scene scene, float dt, KeyboardState keyboard, MouseState mouse, Vector2 windowSize, bool isFocused)
    {
        scene.Update(dt);
    }
}
}
