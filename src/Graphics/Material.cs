using Kinetra.Render;

namespace Kinetra.Graphics
{
    static class ShaderManager
    {
        static Dictionary<string, Shader> cache = new();

        public static Shader Get(string name)
        {
            if (cache.TryGetValue(name, out var s))
            {
                Console.WriteLine($"[ShaderManager] Cache HIT -> {name}");
                return s;
            }

            Console.WriteLine($"[ShaderManager] Loading shader -> {name}");

            var vertexPath = $"shaders/{name}/{name}.vert";
            var fragmentPath = $"shaders/{name}/{name}.frag";

            var shader = new Shader(vertexPath, fragmentPath);

            cache[name] = shader;

            Console.WriteLine($"[ShaderManager] Shader loaded and cached -> {name}");

            return shader;
        }

        public static void DisposeAll()
        {
            Console.WriteLine($"[ShaderManager] Disposing all shaders ({cache.Count})");

            foreach (var kv in cache)
            {
                Console.WriteLine($"[ShaderManager] Disposing -> {kv.Key}");
                kv.Value.Dispose();
            }

            cache.Clear();

            Console.WriteLine("[ShaderManager] All shaders disposed");
        }
    }

    public class Material
    {
        public Shader? shader;

        public Material(string shaderName = "default")
        {
            shader = ShaderManager.Get(shaderName);
        }

        public virtual void Use()
        {
            shader?.Use();
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
        public float alpha;

        public SingleColoredMaterial(Vector3 color, float alpha = 1.0f,  string shaderName = "colored")
            : base(shaderName)
        {
            this.color = color;
            this.alpha = alpha;
        }

        public override void Use()
        {
            base.Use();
            shader?.SetVector3("uColor", color);
            shader?.SetFloat("alpha", alpha);
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