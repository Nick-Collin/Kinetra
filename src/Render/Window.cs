using Kinetra.Core;

namespace Kinetra.Render
{
    public class KinetraWindow : GameWindow
    {
        private Vector4 clearColor = new Vector4(0.0f, 0.0f, 0.0f, 0.0f);

        private Renderer renderer = new();
        public Scene scene = new();

        public void SetBgColor(float r, float g, float b, float a)
        {
            clearColor = new Vector4(r, g, b, a);
            GL.ClearColor(clearColor.X, clearColor.Y, clearColor.Z, clearColor.W);
        }

        public KinetraWindow() : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
            this.Size = new Vector2i(800, 600);
            this.Title = "Kinetra";
        }

        protected override void OnUpdateFrame(FrameEventArgs e) // When frame buffer swaps
        {
            base.OnUpdateFrame(e);
            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }
            scene.Update();
        }

        protected override void OnLoad() // First time opening the window
        {
            base.OnLoad();
            GL.ClearColor(clearColor.X, clearColor.Y, clearColor.Z, clearColor.W);
        }

        protected override void OnRenderFrame(FrameEventArgs e) // When rendering the hidden buffer
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            renderer.Draw(scene);
            SwapBuffers();
        }
        
        protected override void OnFramebufferResize(FramebufferResizeEventArgs e)
        {
            base.OnFramebufferResize(e);
            GL.Viewport(0, 0, e.Width, e.Height);
        }

        protected override void OnUnload() // When closing the window
        {
            base.OnUnload();
            scene.Dispose();
        }
        
    }
}