using Kinetra.Render;

namespace Kinetra.Graphics
{
    public class Material
    {
        public Shader shader;

        public Material(string vertexPath=@"shaders\shader.vert", string fragmentPath=@"shaders\shader.frag")
        {
            this.shader = new Shader(vertexPath, fragmentPath);
        }

        public void Dispose()
        {
            shader?.Dispose();
        }
    }
}