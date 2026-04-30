using System.Reflection.Metadata.Ecma335;

namespace Kinetra.Render
{
    public abstract class RenderObject
    {
        protected int vao;
        protected int vbo;

        public abstract void Load();
        public abstract void Render();
        public abstract void Dispose();
        public int GetAttribLocation(string attribName)
        {
            return GL.GetAttribLocation(Handle, attribName);
        } 
    }

}