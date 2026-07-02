using Kinetra.Core;

namespace Kinetra.Render
{
    public interface FrameUpdateLogic
    {
        void Execute(Scene scene, float dt, KeyboardState keyboard, MouseState mouse, Vector2 windowSize, bool isFocused);
    }

    public class KinetraWindow : GameWindow
    {
        private Vector4 clearColor = new Vector4(0.0f, 0.0f, 0.0f, 0.0f);

        private Renderer renderer = new();
        public List<FrameUpdateLogic> FrameUpdatePasses = new();
        public Scene Scene = new(); 
        

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
            foreach (FrameUpdateLogic pass in FrameUpdatePasses)
            {
                pass.Execute(Scene, (float)e.Time, KeyboardState, MouseState, new Vector2(Size.X, Size.Y), IsFocused);
            }
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
            renderer.Execute(Scene);
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
            Scene.Dispose();
        }

        public void AddRenderPass(IRenderPass pass)
        {
            renderer.SetRenderPass(pass);
        }
        
    }
}