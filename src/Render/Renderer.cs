using Kinetra.Core;

namespace Kinetra.Render
{
public interface RenderPass
{
    void Execute(Scene scene);
}

public class Renderer
{
    private RenderPass? pass;

    public void SetRenderPass(RenderPass pass)
    {
        this.pass = pass;
    }

    public void Execute(Scene scene)
    {
        if (pass == null) // Default behavior if no render pass is set
            return;
        pass.Execute(scene);
    }
}
}
