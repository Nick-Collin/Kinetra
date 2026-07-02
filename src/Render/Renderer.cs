using Kinetra.Core;

namespace Kinetra.Render
{
public interface IRenderPass
{
    void Execute(Scene scene);
}

public class Renderer
{
    private IRenderPass? pass;

    public void SetRenderPass(IRenderPass pass)
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
