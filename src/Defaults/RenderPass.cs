using Kinetra.Core;
using Kinetra.Render;
using Kinetra.Graphics;

namespace Kinetra.Defaults
{
public class RenderLogic : RenderPass
{
    public void Execute(Scene scene)
    {

        
        GL.Enable(EnableCap.DepthTest);
        GL.Enable(EnableCap.ProgramPointSize);

        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        GL.Enable(EnableCap.Blend);
        GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        GL.Disable(EnableCap.CullFace);

        foreach (var entity in scene.Entities)
        {
            if (entity.material is SingleColoredMaterial)
                {
                    GL.DepthMask(false);
                }
            entity.material?.Use();

            var model = entity.transform.GetModelMatrix();
            entity.material?.shader?.SetMatrix4("model", model);

            if (scene.camera == null)
                entity.material?.shader?.SetMatrix4("view", Matrix4.Identity);
            else
                entity.material?.shader?.SetMatrix4("view", scene.camera.GetViewMatrix());

            if (scene.camera == null)
                entity.material?.shader?.SetMatrix4("projection", Matrix4.Identity);
            else
            entity.material?.shader?.SetMatrix4("projection", scene.camera.projection);

            entity.buffer.Bind();

            GL.DrawElements(entity.renderMode, entity.mesh.indices.Length, DrawElementsType.UnsignedInt, 0);
            GL.DepthMask(true);

        }
    }
}
}