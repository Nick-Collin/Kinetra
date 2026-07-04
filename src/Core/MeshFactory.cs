using Kinetra.Graphics;

namespace Kinetra.Core
{
public class MeshFactory
{

    public static Mesh CreateSphere(float radius, int segments)
    {
        var vertices = new List<float>();
        var indices = new List<uint>();

        for (int y = 0; y <= segments; y++)
        {
            float v = y / (float)segments;
            float phi = v * MathF.PI;

            for (int x = 0; x <= segments; x++)
            {
                float u = x / (float)segments;
                float theta = u * MathF.PI * 2;

                float px = radius * MathF.Sin(phi) * MathF.Cos(theta);
                float py = radius * MathF.Cos(phi);
                float pz = radius * MathF.Sin(phi) * MathF.Sin(theta);

                vertices.Add(px);
                vertices.Add(py);
                vertices.Add(pz);
            }
        }

        for (int y = 0; y < segments; y++)
        {
            for (int x = 0; x < segments; x++)
            {
                int i = y * (segments + 1) + x;

                indices.Add((uint)i);
                indices.Add((uint)(i + segments + 1));
                indices.Add((uint)(i + 1));

                indices.Add((uint)(i + 1));
                indices.Add((uint)(i + segments + 1));
                indices.Add((uint)(i + segments + 2));
            }
        }

        var mesh = new Mesh(vertices.ToArray(), indices.ToArray());
        mesh.layout.AddAttribute(0, 3);

        return mesh;
    }

    public static Mesh CreateDumbbell(float radius, int axis)
    {
        var sphere1 = CreateSphere(radius * 0.6f, 12);
        var sphere2 = CreateSphere(radius * 0.6f, 12);

        Vector3 dir = axis switch
        {
            0 => Vector3.UnitX,
            1 => Vector3.UnitY,
            2 => Vector3.UnitZ,
            _ => Vector3.UnitX
        };

        OffsetVertices(sphere1.vertexBuffer, dir * radius * 0.5f);
        OffsetVertices(sphere2.vertexBuffer, -dir * radius * 0.5f);

        return MergeMeshes(sphere1, sphere2);
    }

    public static void OffsetVertices(float[] vertices, Vector3 offset)
    {
        for (int i = 0; i < vertices.Length; i += 3)
        {
            vertices[i]     += offset.X;
            vertices[i + 1] += offset.Y;
            vertices[i + 2] += offset.Z;
        }
    }

    public static Mesh MergeMeshes(Mesh a, Mesh b)
    {
        var vertices = a.vertexBuffer.Concat(b.vertexBuffer).ToArray();

        var indices = new List<uint>();
        indices.AddRange(a.indices);

        uint offset = (uint)(a.vertexBuffer.Length / 3);

        foreach (var i in b.indices)
            indices.Add(i + offset);

        var mesh = new Mesh(vertices, indices.ToArray());
        mesh.layout.AddAttribute(0, 3);

        return mesh;
    }
}
}