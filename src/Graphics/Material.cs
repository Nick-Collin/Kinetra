using Kinetra.Render;

namespace Kinetra.Graphics
{
    public class Material
    {
        public Shader? shader;
        private readonly string shaders_path = "shaders\\";

        public Material(string shaderName = "default")
        {
            LoadShader(shaderName);
        }

        public virtual void Use()
        {
            shader?.Use();
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

        public class PointMaterial : Material
        {
            public Vector3 color;
            public float radius;

            public PointMaterial(Vector3 color, float radius, string shaderName = "point")
                : base(shaderName)
            {
                this.color = color;
                this.radius = radius;
            }

            public override void Use()
            {
                base.Use();
                shader?.SetVector3("uColor", color);
                shader?.SetFloat("uPointSize", radius);
            }
        }

    public class SingleColoredMaterial : Material
    {
        public Vector3 color;

        public SingleColoredMaterial(Vector3 color, string shaderName = "colored")
            : base(shaderName)
        {
            this.color = color;
        }

        public override void Use()
        {
            base.Use();
            shader?.SetVector3("uColor", color);
        }
    }

    public class TexturedMaterial : Material
    {
        public Texture texture;

        public TexturedMaterial(Texture texture, string shaderName = "textured")
            : base(shaderName)
        {
            this.texture = texture;
        }

        public override void Use()
        {
            base.Use();
                GL.ActiveTexture(TextureUnit.Texture0);
                texture.Use();
                shader?.SetInt("uTexture", 0);
        }
    }

}