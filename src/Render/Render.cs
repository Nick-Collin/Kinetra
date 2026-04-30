namespace Kinetra.Render
{
    public class Game : GameWindow
    {
        public Scene scene = new();
        private Shader? shader;

        public Game() : base(GameWindowSettings.Default, NativeWindowSettings.Default)
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
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            this.shader = new Shader(@"shaders\shader.vert", @"shaders\shader.frag");
        }

        protected override void OnRenderFrame(FrameEventArgs e) // When rendering the hidden buffer
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            shader?.Use();
            scene.Render();
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
            shader?.Dispose();
            scene.Dispose();
        }
        
    }
}