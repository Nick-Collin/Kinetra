namespace Kinetra.Graphics
{
    public class Mesh
    {
        public float[] vertexBuffer;
        public uint[] indices;
        public VertexLayout layout;

        public Mesh(float[] vertexBuffer, uint[] indices)
        {
            this.vertexBuffer = vertexBuffer;
            this.indices = indices;
            this.layout = new VertexLayout();
        }
    }

    public class VertexAttribute
    {
        public int Location;     
        public int Size;         
        public int Offset;       
        public bool Normalized; 
    }
    
    public class VertexLayout
    {
        public List<VertexAttribute> Attributes = [];
        public int Stride;

        public void AddAttribute(int location, int size, bool normalized = false)
        {
            if (Attributes.Any(a => a.Location == location))
                throw new Exception($"Location {location} já usada");

            Attributes.Add(new VertexAttribute
            {
                Location = location,
                Size = size,
                Normalized = normalized,
                Offset = Stride
            });

            Stride += size * sizeof(float); // Assumindo float para simplicidade, pode ser ajustado para outros tipos
        }

    }
}