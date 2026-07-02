using Kinetra.Render;

namespace Kinetra.Graphics
{
    public class Material
    {
        public Shader? shader;
        public Texture? texture;
        private readonly string shaders_path = "shaders\\";

        public Material(string shaderName = "default", string? texturePath = null)
        {
            LoadShader(shaderName);

            if (texturePath != null)
                texture = new Texture(texturePath);
        }

        public void Use()
        {
            shader?.Use();

            if (texture != null)
            {
                GL.ActiveTexture(TextureUnit.Texture0);
                texture.Use();
                shader?.SetInt("uTexture", 0);
            }
            
        }

        public void LoadShader(string shaderName)
        {
            string vertexPath = Path.Combine(shaders_path, shaderName + "\\" + shaderName + ".vert");
            Console.WriteLine(vertexPath);
            string fragmentPath = Path.Combine(shaders_path, shaderName + "\\" + shaderName + ".frag");
            Console.WriteLine(fragmentPath);

            if (!File.Exists(vertexPath) || !File.Exists(fragmentPath))
            {
                Console.WriteLine($"Shader files not found: {vertexPath} or {fragmentPath}");
                return;
            }

            shader?.Dispose(); // Dispose do shader antigo, se existir
            shader = new Shader(vertexPath, fragmentPath);
        }

        public void Dispose()
        {
            shader?.Dispose();
        }
    }
}